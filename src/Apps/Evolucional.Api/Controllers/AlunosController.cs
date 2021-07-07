using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Evolucional.Application.Alunos.Commands.Create;
using Evolucional.Application.Alunos.Queries;
using Evolucional.Application.Alunos.Queries.GetAlunosComPaginacao;
using Evolucional.Application.Common.Models;
using Evolucional.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evolucional.Api.Controllers
{
    [Authorize]
    public class AlunosController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<AlunoDto>>>> GetAllAlunosComPaginacao(GetAllAlunosComPaginacaoQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<AlunoDto>>> Create(CreateAlunoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetAlunosExport")]
        public async Task<FileResult> GetAlunosExport()
        {
            var vm = await Mediator.Send(new ExportAlunosQuery());

            return File(vm.Content, vm.ContentType, vm.FileName);
        }
    }
}
