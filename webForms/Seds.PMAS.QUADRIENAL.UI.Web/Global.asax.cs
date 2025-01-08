using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security;
using System.Threading;
using Microsoft.IdentityModel.Claims;
using System.Globalization;
using System.Web.Configuration;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Microsoft.IdentityModel.Web;
using Microsoft.IdentityModel.Web.Configuration;
using Seds.PMAS.QUADRIENAL.Negocio;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            FederatedAuthentication.ServiceConfigurationCreated += OnServiceConfigurationCreated;
            ContextManager.Initialize();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_EndRequest(object sender, EventArgs e)
        {
            ContextManager.Dispose();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {                               
                HttpContext.Current.Session["UsuarioPMAS"] = GetUsuarioPMAS();
            }

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }
              

        public static UsuarioPMASInfo GetUsuarioPMAS()
        {
            UsuarioPMASInfo u;
            using (var proxy = new ProxyUsuarioPMAS())
            {
                u = proxy.Service.GetUsuarioLogado();                
            }
            if (u == null)
                return null;
            
            if (u.IdPrefeitura.HasValue || u.IdDrads.HasValue)
            {
                using (var proxy = new ProxyDivisaoAdministrativa())
                {
                    if(u.Prefeitura != null)
                        u.Prefeitura.Municipio = proxy.Service.GetMunicipioById(u.Prefeitura.IdMunicipio);
                    if (u.IdDrads.HasValue)
                        u.Drads = proxy.Service.GetDradsById(u.IdDrads.Value);
                }
            }
            return u;
        }

        private void OnServiceConfigurationCreated(object sender, ServiceConfigurationCreatedEventArgs e)
        {
            // Use the <serviceCertificate> to protect the cookies that 
            // are sent to the client.
            List<CookieTransform> sessionTransforms =
                new List<CookieTransform>(
                    new CookieTransform[] 
            {
                new DeflateCookieTransform(), 
                new RsaEncryptionCookieTransform(
                    e.ServiceConfiguration.ServiceCertificate),
                new RsaSignatureCookieTransform(
                    e.ServiceConfiguration.ServiceCertificate)  
            });
            SessionSecurityTokenHandler sessionHandler =
             new
              SessionSecurityTokenHandler(sessionTransforms.AsReadOnly());

            e.ServiceConfiguration.SecurityTokenHandlers.AddOrReplace(
                sessionHandler);
        }

    }
}
