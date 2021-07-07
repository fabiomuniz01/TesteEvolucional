using Evolucional.Application.Dto;

namespace Evolucional.Application.ApplicationUser.Queries.GetToken
{
    public class LoginResponse
    {
        public UsuarioAplicacaoDto User { get; set; }

        public string Token { get; set; }
    }
}
