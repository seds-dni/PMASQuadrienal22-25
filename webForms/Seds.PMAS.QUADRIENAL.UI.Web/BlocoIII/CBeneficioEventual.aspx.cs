using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Web.UI.HtmlControls;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class CBeneficioEventual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "BI")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Benefício Eventual registrado com sucesso!"), true);
                    }
                    else if (Request.QueryString["msg"] == "BU")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Benefício Eventual atualizado com sucesso!"), true);
                    }
                    else if (Request.QueryString["msg"] == "BE")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Benefício Eventual excluído com sucesso!"), true);
                    }
                }

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                using (var proxy = new ProxyProgramas())
                {
                    load(proxy);                    
                }
                
                verificarAlteracoes();
            }

        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro52.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 52);
                    linkAlteracoesQuadro52.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("52"));
                }
            }
        }

        void load(ProxyProgramas proxy)
        {
            List<ConsultaPrefeituraBeneficioEventualInfo> beneficios = proxy.Service.GetConsultaBeneficiosEventuaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            var grupos = beneficios.Distinct();

            #region Exibicao Recursos e pivotagem dos cofinanciamentos

            var locaisSource = grupos.GroupBy(x => x.Nome).Select(g => new
            {
                Id = g.First().Id
                ,
                obj = g.First()
                ,
                Cofinanciamentos = grupos.Where(p => p.Id == g.First().Id).Select(x => new
                {
                    ValorCofinanciamentoEstadual = x.PrevisaoOrcamentaria,
                    Exercicio = x.Exercicio

                })
            }).ToList();

            #endregion

            lst.DataSource = locaisSource;
            lst.DataBind();            
        }       

        protected void lst_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/FBeneficioEventual.aspx?idTipo=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/FBeneficioEventualServicos.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key)));
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyProgramas())
                        {
                            proxy.Service.DeleteBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id,Convert.ToInt32(key));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    Response.Redirect("~/BlocoIII/CBeneficioEventual.aspx?msg=BE");
                    break;

                default:
                    break;
            }
        }

        protected void lst_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = lst.DataKeys[e.Item.DataItemIndex]; //(ConsultaPrefeituraBeneficioEventualInfo)e.Item.DataItem;
                var id = Convert.ToInt32(item["Id"]);



                if(item == null)
                    return;
                var btn = ((ImageButton)e.Item.FindControl("btnExcluir"));
                
                if (id == 0)
                {
                    ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = true;
                    ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                    ((HtmlTableCell)e.Item.FindControl("tdUnidades")).Visible =
                    ((HtmlTableCell)e.Item.FindControl("tdServicos")).Visible = false;
                    btn.Visible = false;
                }
                else
                {
                    ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = false; //item.TotalServicos == 0;
                    ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                    ((HtmlTableCell)e.Item.FindControl("tdUnidades")).Visible =
                    ((HtmlTableCell)e.Item.FindControl("tdServicos")).Visible = true; //item.TotalServicos > 0;
                    Permissao.VerificarPermissaoControles(new[] { btn }, Session);
                }
            }
        }
    }
}