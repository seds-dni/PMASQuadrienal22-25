using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class HistoricoAlteracoes : System.Web.UI.Page
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
                if (!String.IsNullOrEmpty(Request.QueryString["idForeignKey"]))
                    lstHistorico.DataSource = proxy.Service.GetAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idQuadro"])), Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idForeignKey"])));
                else if (!String.IsNullOrEmpty(Request.QueryString["idQuadro"]))
                    lstHistorico.DataSource = proxy.Service.GetAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idQuadro"])));
                else
                {
                    lstHistorico.DataSource = proxy.Service.GetAlteracoesNoPlanoMunicipalByPrefeituraUltimaRevisao(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    btnVoltar.Visible = false;
                }
                lstHistorico.DataBind();
            }

        }      
    }
}