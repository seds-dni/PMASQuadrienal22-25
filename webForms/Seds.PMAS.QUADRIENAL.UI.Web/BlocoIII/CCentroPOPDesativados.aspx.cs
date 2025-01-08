using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class CCentroPOPDesativados : System.Web.UI.Page
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


                WebControl[] controles = { btnVoltar };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        void load()
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["IdUnidade"]));
                lstCentroPOP.DataSource = proxy.Service.GetIdentificacaoCentroPOPByUnidade(id, String.Empty).Where(c => c.Desativado == true);
                lstCentroPOP.DataBind();
            }
        }

        protected void lstCentroPOP_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

        protected void lstCentroPOP_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstCentroPOP.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["IdUnidade"]);
            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/VCentroPOP.aspx?id=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCentroPOPDesativado.aspx?id=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;
                default:
                    break;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }
    }
}