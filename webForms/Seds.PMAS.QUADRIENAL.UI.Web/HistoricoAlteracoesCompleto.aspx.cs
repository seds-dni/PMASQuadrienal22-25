using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class HistoricoAlteracoesCompleto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);                

                load();
            }
        }

        void load()
        {

            using (var proxy = new ProxyPlanoMunicipal())
            {               
                lstHistorico.DataSource = proxy.Service.GetAlteracoesNoPlanoMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);                               
                lstHistorico.DataBind();
            }

        }      
    }
}