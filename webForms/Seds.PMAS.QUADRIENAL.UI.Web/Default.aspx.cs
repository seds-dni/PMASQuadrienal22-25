using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using System.Text;
using System.Globalization;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Claims;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using Microsoft.IdentityModel.Protocols.WSTrust;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.UI.Processos;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);              
            }
                      
        }

    
    }
}
