using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class CUnidadesPrivadasDesativadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                load();
            }
        }

        void load()
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                lstUnidades.DataSource = proxy.Service.GetIdentificacaoUnidadesPrivadaDesativadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, txtLocalizar.Text).OrderBy(c => c.IdFormaAtuacao);
                lstUnidades.DataBind();
            }
        }

        protected void lstUnidades_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/FUnidadePrivada.aspx");
        }

        protected void btnLocalizar_Click(object sender, EventArgs e)
        {
            load();
        }

        protected void lstUnidades_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstUnidades.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/VUnidadePrivada.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    break;


                default:
                    break;
            }
        }

        protected void btnLimparBusca_Click(object sender, EventArgs e)
        {
            txtLocalizar.Text = String.Empty;
            btnLocalizar_Click(null, null);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CUnidadesPrivadas.aspx");
        }
    }
}