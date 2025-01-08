using Microsoft.Owin.Security.OAuth;
using Seds.PMAS.Dominio.Interfaces.Services;
using Seds.PMAS.Resource.Resources;
using Seds.Seguranca.Negocio;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Seds.PMAS.Api.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUsuarioService _service;

        public AuthorizationServerProvider(IUsuarioService service)
        {
            this._service = service;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //context.OwinContext.Response.Headers.Add("Allow", new[] { "GET, POST, PUT, DELETE, HEAD, OPTIONS" });

            try
            {
                var user = _service.Autenticar(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", Errors.InvalidCredentials);
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("id", user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Nome));
             //   identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Nome));

                GenericPrincipal principal = new GenericPrincipal(identity, null);
                Thread.CurrentPrincipal = principal;

                context.Validated(identity);
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.Message.ToString());
            }
        }
    }
}