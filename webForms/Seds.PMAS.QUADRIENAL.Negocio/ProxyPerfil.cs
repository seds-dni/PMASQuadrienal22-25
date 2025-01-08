using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Protocols.WSTrust;
using System.ServiceModel;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using Microsoft.IdentityModel.SecurityTokenService;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols.WSTrust.Bindings;
using System.Web;
using Seds.PMAS.QUADRIENAL.Negocio.PerfilService;
using System.Web.Configuration;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ProxyPerfil : Proxy<IPerfilService>
    {
        public ProxyPerfil()
            : base("WS2007HttpBinding_IPerfilService")
        {
            
            this.clientPassword = WebConfigurationManager.AppSettings["Seds.Seguranca.Servicos"];            
        }        
    }
}
