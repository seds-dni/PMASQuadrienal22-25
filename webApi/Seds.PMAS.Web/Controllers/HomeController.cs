using Microsoft.IdentityModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seds.PMAS.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Sessao.VerificarSessao();
            return View(Sessao.UsuarioLogado);
        }

        public ActionResult LogOff()
        {
            if (Session != null)
            {
                Session.Clear();
                Session.Abandon();
            }
            WSFederationAuthenticationModule authModule = FederatedAuthentication.WSFederationAuthenticationModule;
            string signoutUrl = (WSFederationAuthenticationModule.GetFederationPassiveSignOutUrl(authModule.Issuer, authModule.Realm, null));

            return Redirect(signoutUrl);
        }


    }
}
