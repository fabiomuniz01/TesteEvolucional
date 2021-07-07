using System.Security.Claims;
using Evolucional.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Evolucional.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UsuarioId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UsuarioId { get; }
    }
}
