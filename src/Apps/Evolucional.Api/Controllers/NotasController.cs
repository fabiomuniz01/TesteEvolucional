using System.Threading.Tasks;
using Evolucional.Application.Common.Models;
using Evolucional.Application.Dto;
using Evolucional.Application.Notas.Commands.Create;
using Evolucional.Application.Notas.Queries.GetNotasComPaginacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evolucional.Api.Controllers
{
    [Authorize]
    public class NotasController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<NotaDto>>>> GetAllNotasComPaginacao(GetAllNotasComPaginacaoQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<NotaDto>>> Create(CreateNotaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
