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
    public partial class CBeneficiosContinuados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "TI")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Benefício Continuado registrado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "TU")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Benefício Continuado atualizado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "TE")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Benefício Continuado excluído com sucesso!"), true);
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
                    linkAlteracoesQuadro57.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 57);
                    linkAlteracoesQuadro57.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("57"));
                }
            }
        }

        void load(ProxyProgramas proxy)
        {
            var beneficios = proxy.Service.GetConsultaBeneficiosContinuadosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);


            #region Exibicao Recursos e pivotagem dos cofinanciamentos

            var locaisSource = beneficios.GroupBy(x => x.Id).Select(g => new
            {
                Id = g.First().Id
                ,
                obj = g.First()
                ,
                Continuados = beneficios.Where(p => p.Id == g.First().Id).Select(x => new
                {
                    ValorPrevisaoAnual = x.PrevisaoAnualRepasse,
                    Exercicio = x.Exercicio
                })
                ,
                Beneficiarios = beneficios.Where(p => p.Id == g.First().Id).Select(x => new
                {
                    NumeroBeneficiarios = x.NumeroBeneficiarios,
                    Exercicio = x.Exercicio
                })

            }).ToList();

            #endregion


            lstProgramasFederais.DataSource = locaisSource;
            lstProgramasFederais.DataBind();           
        }       

        protected void lstProgramas_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/FTransferenciaRenda.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/FTransferenciaRendaCofinanciamento.aspx?acao=BC&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key)));
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyProgramas())
                        {
                            proxy.Service.DeleteTransferenciaRenda(Convert.ToInt32(key));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    Response.Redirect("~/BlocoIII/CBeneficiosContinuados.aspx?msg=TE");
                    break;

                default:
                    break;
            }
        }

        protected void lstProgramas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                
                var item = lstProgramasFederais.DataKeys[e.Item.DataItemIndex];
                var id = Convert.ToInt32(item["Id"]);
                var idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

                ProxyProgramas proxy = new ProxyProgramas();
                    
                var beneficios = proxy.Service.GetConsultaBeneficiosContinuadosByPrefeitura(idPrefeitura);

                var itens = beneficios.Where(s => s.Id == id).First();

                if(item == null)
                    return;
               var btn = ((ImageButton)e.Item.FindControl("btnExcluir"));                

                if (itens.Aderiu == 0)
                {
                    if (itens.Nome == "Beneficio de Prestação Continuada - BPC Idosos" || itens.Nome == "Beneficio de Prestação Continuada - BPC Pessoas com Deficiência")
                    {
                        ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = itens.TotalServicos == 0;
                        ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                        ((HtmlTableCell)e.Item.FindControl("tdUnidades")).Visible =
                        ((HtmlTableCell)e.Item.FindControl("tdServicos")).Visible = itens.TotalServicos > 0;
                        Permissao.VerificarPermissaoControles(new[] { btn }, Session);
                    }
                    else
                    {
                        ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = true;
                        ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                        ((HtmlTableCell)e.Item.FindControl("tdUnidades")).Visible =
                        ((HtmlTableCell)e.Item.FindControl("tdServicos")).Visible = false;
                        btn.Visible = false;
                    }
                }
                else
                {
                    ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = itens.TotalServicos == 0;
                    ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                    ((HtmlTableCell)e.Item.FindControl("tdUnidades")).Visible =
                    ((HtmlTableCell)e.Item.FindControl("tdServicos")).Visible = itens.TotalServicos > 0;
                    Permissao.VerificarPermissaoControles(new[] { btn }, Session);
                }
            }            
        }
    }
}