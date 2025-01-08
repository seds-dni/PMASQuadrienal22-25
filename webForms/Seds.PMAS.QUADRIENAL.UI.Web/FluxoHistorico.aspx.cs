using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Genericos;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Microsoft.IdentityModel.Claims;
using System.Threading;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class FluxoHistorico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);                

                if (String.IsNullOrEmpty(Request.QueryString["idPrefeitura"]))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                load();
            }
        }

        void load()
        {
            lblMunicipio.Text = SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Nome;
            using (var proxy = new ProxyPlanoMunicipal())
            {
                lstHistorico.DataSource = proxy.Service.GetHistoricoPlanoMunicipalByPrefeitura(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idPrefeitura"])));
                lstHistorico.DataBind();
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador)
            {
                Session["CarregaFluxo"] = true;
                Response.Redirect("~/ConsultaFluxoPMASCAS.aspx");
            }
        }
    }
}