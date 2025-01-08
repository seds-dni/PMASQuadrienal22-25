using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.IdentityModel.Web;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class LogOff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            WSFederationAuthenticationModule authModule = FederatedAuthentication.WSFederationAuthenticationModule;
            string signoutUrl = (WSFederationAuthenticationModule.GetFederationPassiveSignOutUrl(authModule.Issuer, authModule.Realm, null));
            WSFederationAuthenticationModule.FederatedSignOut(new Uri(authModule.Issuer), new Uri(authModule.Realm.TrimEnd(@"/".ToCharArray()))); 
        }
    }
}