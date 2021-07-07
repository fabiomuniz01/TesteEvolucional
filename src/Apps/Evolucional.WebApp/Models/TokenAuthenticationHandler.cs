using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Evolucional.WebApp.Models
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
    {
        public IServiceProvider ServiceProvider { get; set; }

        public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IServiceProvider serviceProvider)
            : base(options, logger, encoder, clock)
        {
            ServiceProvider = serviceProvider;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var headers = Request.Headers;
            var token = Request.QueryString.Value.Split('&')[3].Split('=')[1];

            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Token is null"));
            }

            bool isValidToken = Request.QueryString.HasValue && Convert.ToBoolean(Request.QueryString.Value.Split('&')[2].Split('=')[1]); // check token here

            if (!isValidToken)
            {
                return Task.FromResult(AuthenticateResult.Fail($"Balancer not authorize token : for token={token}"));
            }

            var claims = new List<Claim>();
            claims.Add(new Claim("token", token));

            var identity = new ClaimsIdentity(claims, nameof(TokenAuthenticationHandler));
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Set current principal
            Thread.CurrentPrincipal = claimsPrincipal;

            //var claims = new[] { new Claim("token", token) };
            //var identity = new ClaimsIdentity(claims, nameof(TokenAuthenticationHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), this.Scheme.Name);


            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
