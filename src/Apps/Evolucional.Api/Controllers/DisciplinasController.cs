using System.Threading.Tasks;
using Evolucional.Application.Alunos.Queries;
using Evolucional.Application.Common.Models;
using Evolucional.Application.Disciplinas.Commands.Create;
using Evolucional.Application.Disciplinas.Queries;
using Evolucional.Application.Disciplinas.Queries.GetDisciplinasComPaginacao;
using Evolucional.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evolucional.Api.Controllers
{
    [Authorize]
    public class DisciplinasController: BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<DisciplinaDto>>>> GetAllDisciplinasComPaginacao(GetAllDisciplinasComPaginacaoQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<DisciplinaDto>>> Create(CreateDisciplinaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
