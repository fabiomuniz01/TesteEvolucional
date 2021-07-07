using System.Threading.Tasks;
using Evolucional.Application.ApplicationUser.Queries.GetToken;
using Evolucional.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Evolucional.Api.Controllers
{
    public class LoginController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Create(GetTokenQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
