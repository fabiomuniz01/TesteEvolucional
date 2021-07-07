using Evolucional.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Evolucional.WebApp.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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
                    _ = await notasClient.CreateAsync(new CreateNotaCommand()
                    {
                        AlunoId = aluno.Id,
                        DisciplinaId = disciplina.Id,
                        Valor = new Random().Next(11)
                    });
                }
            }

            return RedirectToAction(nameof(LogedIn),
                new { notify = 0, message = $"1000 alunos foram cadastrados com sucesso!!!", autenticado = true });
        }

        public async Task<FileResult> BotaoDois(string token, CancellationToken stoppingToken)
        {
            string baseUrl = "https://localhost:44308";
            AlunosClient alunosClient = new AlunosClient(baseUrl);
            alunosClient.SetBearerToken(token);

            var result = alunosClient.GetAlunosExportAsync(stoppingToken);
            return null;
            //return File(Encoding.UTF8.GetBytes(result.Result.ToString()), "text/csv", "Relatório.csv");

        }

    }

}
