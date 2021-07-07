using Evolucional.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Evolucional.WebApp.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace Evolucional.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Readme()
        {
            return View();
        }

        public IActionResult Erro()
        {
            return View();
        }

        public IActionResult Login(int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            ViewBag.Autenticado = false;
            return View();
        }

        [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
        public IActionResult LogedIn(int? notify, string message, bool autenticado, string token)
        {
            SetNotifyMessage(notify, message);

            ViewBag.Autenticado = autenticado;
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(IFormCollection collection, CancellationToken stoppingToken)
        {
            try
            {
                string? returnUrl = null;
                returnUrl = returnUrl ?? Url.Content("~/");
                string baseUrl = "https://localhost:44308";
                LoginClient loginClient = new LoginClient(baseUrl);
                var result = await loginClient.CreateAsync(new GetTokenQuery
                {
                    Email = collection["usuario"].ToString(),
                    Password = collection["senha"].ToString()
                }, stoppingToken);

                return result.Succeeded
                    ? RedirectToAction(nameof(LogedIn),
                        new
                        {
                            notify = 0,
                            message = $"Bem Vindo {result.Data.User.UserName}!!!",
                            autenticado = result.Succeeded,
                            token = result.Data.Token
                        })
                    : RedirectToAction(nameof(Login),
                        new { notify = 2, message = "Usuário nao possui permissâo para acessar a aplicação" });
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                Console.WriteLine(e);
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void SetNotifyMessage(int? notify, string message)
        {
            switch (notify)
            {
                case 0:
                    ViewBag.NotifyMessage = 0;
                    ViewBag.Notify = message;
                    break;
                case 1:
                    ViewBag.NotifyMessage = 1;
                    ViewBag.Notify = message;
                    break;
                case 2:
                    ViewBag.NotifyMessage = 2;
                    ViewBag.Notify = message;
                    break;
                default:
                    ViewBag.NotifyMessage = -1;
                    ViewBag.Notify = "null";
                    break;
            }
        }

        public async Task<IActionResult> BotaoUm(string token, CancellationToken stoppingToken)
        {
            try
            {
                string baseUrl = "https://localhost:44308";
                AlunosClient alunosClient = new AlunosClient(baseUrl);
                alunosClient.SetBearerToken(token);
                //            var resultAlunos = alunosClient.GetAllAlunosAsync(stoppingToken);
                //            if (resultAlunos.Result != null)
                //            {
                //                return RedirectToAction(nameof(LogedIn),
                //new {notify =2, message = $"Já existem 1000 alunos cadastrados!!!", autenticado = true, token = token});
                //            }


                for (int i = 1; i <= 1000; i++)
                {
                    var command = new CreateAlunoCommand()
                    {
                        Nome = $"Aluno{i}"
                    };
                    _ = await alunosClient.CreateAsync(command);
                }

                DisciplinasClient disciplinasClient = new DisciplinasClient(baseUrl);
                disciplinasClient.SetBearerToken(token);

                var list = new List<string>
            {
                "Matemática",
                "Português",
                "História",
                "Geografia",
                "Inglês",
                "Biologia",
                "Filosofia",
                "Física",
                "Química"
            };

                foreach (var item in list)
                {
                    _ = await disciplinasClient.CreateAsync(new CreateDisciplinaCommand { Nome = item });
                }

                disciplinasClient.SetBearerToken(token);
                var listDisciplinas =
                    disciplinasClient.GetAllDisciplinasComPaginacaoAsync(
                        new GetAllDisciplinasComPaginacaoQuery()
                        {
                            PageNumber = 1,
                            PageSize = 10
                        });

                alunosClient.SetBearerToken(token);
                var listAlunos =
                    alunosClient.GetAllAlunosComPaginacaoAsync(
                    new GetAllAlunosComPaginacaoQuery
                    {
                        PageNumber = 1,
                        PageSize = 2000
                    });

                NotasClient notasClient = new NotasClient(baseUrl);
                notasClient.SetBearerToken(token);

                foreach (var aluno in listAlunos.Result.Data.Items)
                {
                    foreach (var disciplina in listDisciplinas.Result.Data.Items)
                    {
                        var commandNota = new CreateNotaCommand()
                        {
                            AlunoId = aluno.Id,
                            DisciplinaId = disciplina.Id,
                            Valor = new Random().Next(1, 11)
                        };

                        _ = await notasClient.CreateAsync(commandNota);
                    }
                }

                return RedirectToAction(nameof(LogedIn),
                    new { notify = 0, message = $"1000 alunos foram cadastrados com sucesso!!!", autenticado = true, token = token });
            }
            catch (Exception e)
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Root));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(((Evolucional.WebApp.Client.SwaggerException)e).Response));
                Root obj = (Root)serializer.ReadObject(ms);
                var msg = string.Empty;
                if (obj.error.code == 900)
                {
                    msg = obj.data.Nome == null ? "" : string.Join(",", obj.data.Nome);
                    msg = msg + obj.data.Valor == null ? "" : string.Join(",", obj.data.Valor);
                }
                else
                {
                    msg = obj.error.message;
                }
                return RedirectToAction(nameof(LogedIn),
                    new { notify = 2, message = msg, autenticado = true, token = token });

                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<FileResult> BotaoDois(string token, CancellationToken stoppingToken)
        {
            string baseUrl = "https://localhost:44308";
            AlunosClient alunosClient = new AlunosClient(baseUrl);
            alunosClient.SetBearerToken(token);

            var listAlunos = alunosClient.GetAllAlunosComPaginacaoAsync(
                new GetAllAlunosComPaginacaoQuery
                {
                    PageNumber = 1,
                    PageSize = 2000
                }).Result;

            NotasClient notasClient = new NotasClient(baseUrl);
            notasClient.SetBearerToken(token);

            StringBuilder sb = new StringBuilder();

            sb.Append("AlunoId;Nome;Matemática;Português;História;Geografia;Inglês;Biologia;Filosofia;Física;Química;MÉDIA");
            sb.Append("\r\n");

            foreach (var aluno in listAlunos.Data.Items)
            {
                var resultNotas = notasClient.GetAllNotasComPaginacaoAsync(new GetAllNotasComPaginacaoQuery()
                {
                    PageNumber = 1,
                    PageSize = 9200,
                    AlunoId = aluno.Id
                }).Result;

                sb.Append(
                    $"{aluno.Id};" +
                    $"{aluno.Nome};" +
                    $"{string.Join("", resultNotas.Data.Items.Where(x => x.Disciplina.Nome.Equals("Matemática")).ToList().Select(s => s.Valor).ToList())};" +
                    $"{string.Join("", resultNotas.Data.Items.Where(x => x.Disciplina.Nome.Equals("Português")).ToList().Select(s => s.Valor).ToList())};" +
                    $"{string.Join("", resultNotas.Data.Items.Where(x => x.Disciplina.Nome.Equals("História")).ToList().Select(s => s.Valor).ToList())};" +
                    $"{string.Join("", resultNotas.Data.Items.Where(x => x.Disciplina.Nome.Equals("Geografia")).ToList().Select(s => s.Valor).ToList())};" +
                    $"{string.Join("", resultNotas.Data.Items.Where(x => x.Disciplina.Nome.Equals("Inglês")).ToList().Select(s => s.Valor).ToList())};" +
                    $"{string.Join("", resultNotas.Data.Items.Where(x => x.Disciplina.Nome.Equals("Biologia")).ToList().Select(s => s.Valor).ToList())};" +
                    $"{string.Join("", resultNotas.Data.Items.Where(x => x.Disciplina.Nome.Equals("Filosofia")).ToList().Select(s => s.Valor).ToList())};" +
                    $"{string.Join("", resultNotas.Data.Items.Where(x => x.Disciplina.Nome.Equals("Física")).ToList().Select(s => s.Valor).ToList())};" +
                    $"{string.Join("", resultNotas.Data.Items.Where(x => x.Disciplina.Nome.Equals("Química")).ToList().Select(s => s.Valor).ToList())};" +
                    $"{ Convert.ToDecimal(resultNotas.Data.Items.Sum(s => s.Valor) / 9).ToString("F")};");
                sb.Append("\r\n");
            }

            return File(Encoding.Unicode.GetBytes(sb.ToString()), "text/csv", "Relatório.csv");

        }

    }

    public class Data
    {
        public List<string> Nome { get; set; }
        public List<string> Valor { get; set; }
    }

    public class Error
    {
        public string message { get; set; }
        public int code { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
        public bool succeeded { get; set; }
        public Error error { get; set; }
    }

}
