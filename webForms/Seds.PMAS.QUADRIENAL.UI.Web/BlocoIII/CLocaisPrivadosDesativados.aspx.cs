using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class CLocaisPrivadosDesativados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "UE")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Unidade da Rede Indireta excluída com sucesso!"), true);
                    if (Request.QueryString["msg"] == "UD")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Unidade da Rede Indireta desativada com sucesso!"), true);
                }

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
            var idUnidade = Request.QueryString["IdUnidade"];
            if (!String.IsNullOrEmpty(idUnidade))
            {
                var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(idUnidade));
                using (var proxy = new ProxyRedeProtecaoSocial())
                {


                    var locais = proxy.Service.GetLocaisExecucaoPrivadoByIdUnidade(id).Where(c => c.Desativado == true).ToList();


                    var locaisSource = locais.GroupBy(x => x.Id).Select(g => new
                    {
                        Id = g.First().Id
                        ,
                        obj = g.First()
                        ,
                        Nome = g.First().Nome
                        ,
                        Responsavel = g.First().Responsavel
                        ,
                        TotalServicosDesativados = g.First().TotalServicosDesativados
                        ,
                        DataEncerramento = g.First().DataEncerramento
                        ,
                        Cofinanciamentos = locais.Where(p => p.Id == g.First().Id).Select(x => new
                        {
                            ValorCofinanciamentoEstadual = x.ValorCofinanciamentoEstadual
                                                                        ,
                            Exercicio = x.Exercicio
                        })
                    }).ToList();

                    lstUnidades.DataSource = locaisSource;

                    lstUnidades.DataBind();
                }
            }
        }

        protected void lstUnidades_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
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
                    Response.Redirect("~/BlocoIII/VLocalExecucaoPrivado.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())) + "&idUnidade=" + Server.UrlEncode(Request.QueryString["IdUnidade"]));
                    break;
                case "Servicos":
                    //Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPrivado.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPrivadoDesativado.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Request.QueryString["IdUnidade"]));
                    
                    break;
                default:
                    break;
            }
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["IdUnidade"]);
            Response.Redirect("~/BlocoIII/FUnidadePrivada.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }
    }
}