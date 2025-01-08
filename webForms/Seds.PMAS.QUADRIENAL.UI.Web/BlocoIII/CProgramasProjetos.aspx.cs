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
    public partial class CProgramasProjetos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "PI")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Programa/Projeto registrado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "PU")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Programa/Projeto atualizado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "PE")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Programa/Projeto excluído com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "TI")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Transferência de Renda registrada com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "TU")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Transferência de Renda atualizada com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "TE")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Transferência de Renda excluída com sucesso!"), true);
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

                WebControl[] controles = { btnIncluir };
                Permissao.VerificarPermissaoControles(controles, Session);
            }

        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro40.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 40);
                    linkAlteracoesQuadro40.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("40"));

                    linkAlteracoesQuadroEstaduais.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 83);
                    linkAlteracoesQuadroEstaduais.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("83"));

                    linkAlteracoesQuadroFederais.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 84);
                    linkAlteracoesQuadroFederais.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("84"));
                }
            }
        }

        void load(ProxyProgramas proxy)
        {
             
            var lstFederais = proxy.Service.GetConsultaProgramasProjetosFederaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            lstProgramasFederais.DataSource = lstFederais;
            lstProgramasFederais.DataBind();

            var programasEstaduais = proxy.Service.GetConsultaProgramasProjetosEstaduaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(s => s.Nome.Contains("São Paulo Amigo do Idoso") || s.Nome.Contains("Ação Jovem") || s.Nome.Contains("Renda Cidadã") || s.Nome.Contains("Prospera Família") || s.Nome.Contains("Auxílio Aluguel"))
                .Distinct()
                .GroupBy(x => x.Id)
                .Select(g => new
                    {
                        Id = g.First().Id
                        ,
                        obj = g.First()
                    }
                ).ToList();

            lstProgramasEstaduais.DataSource = programasEstaduais;
            lstProgramasEstaduais.DataBind();

            var programasEstaduaisUm = proxy.Service.GetConsultaProgramasProjetosEstaduaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(s => s.Nome.Contains("Fortalecimento do CadÚnico") || s.Nome.Contains("Fortalecimento da Vigilância Socioassistencial"))
                .Distinct()
                .GroupBy(x => x.Id)
                .Select(g => new
                {
                    Id = g.First().Id
                    ,
                    obj = g.First()
                }
                ).ToList();

            lstProgramasEstaduaisUm.DataSource = programasEstaduaisUm;
            lstProgramasEstaduaisUm.DataBind();

            var programasProjetosMunicipais = proxy.Service.GetConsultaProgramasProjetosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            lstProgramas.DataSource = programasProjetosMunicipais;
            lstProgramas.DataBind();
        }

        protected void lstProgramasMunicipais_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };

                var item = (ConsultaProgramaProjetoInfo)e.Item.DataItem;
                if (item == null)
                    return;

                ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = item.TotalServicos == 0;
                ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                ((HtmlTableCell)e.Item.FindControl("tdTotalServicos")).Visible = item.TotalServicos > 0;
                ((HiddenField)e.Item.FindControl("hdfNome")).Value = item.Nome;
                ((HiddenField)e.Item.FindControl("hdfTipoProgramaTransferencia")).Value = item.TipoProgramaTransferencia.ToString();

                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void lstProgramasMunicipais_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstProgramas.DataKeys[e.Item.DataItemIndex];
            var tipoProgramaTransferencia = ((HiddenField)e.Item.FindControl("hdfTipoProgramaTransferencia")).Value;
            switch (e.CommandName)
            {
                case "Visualizar":
                    {
                        Response.Redirect(string.Format("~/BlocoIII/FProgramaProjetoCadastro.aspx?id={0}&p=m", Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())) ));
                    }
                    break;
                case "Servicos":
                        Response.Redirect("~/BlocoIII/FProgramaProjetoCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyProgramas())
                        {                         
                            proxy.Service.DeleteProgramaProjetoPrevisaoAnualBeneficiarios(Convert.ToInt32(key["Id"].ToString()));
                            proxy.Service.DeleteProgramaProjeto(Convert.ToInt32(key["Id"].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    Response.Redirect("~/BlocoIII/CProgramasProjetos.aspx?msg=PE");
                    break;

                default:
                    break;
            }
        }

        protected void lstProgramasEstaduais_ItemDataBound(object sender, ListViewItemEventArgs e)  
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };

                var item = (dynamic)e.Item.DataItem;
                if (item == null)
                    return;
                ((HiddenField)e.Item.FindControl("hdfNomeEstadual")).Value = item.obj.Nome;
                ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = item.obj.TotalServicos == 0; //!Convert.ToBoolean(item.PossuiProgramaBeneficio);
                ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                ((HtmlTableCell)e.Item.FindControl("tdTotalServicos")).Visible = item.obj.TotalServicos > 0; // Convert.ToBoolean(item.PossuiProgramaBeneficio);

                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void lstProgramasEstaduais_ItemCommand(object sender, ListViewCommandEventArgs e)  
        {
            var key = lstProgramasEstaduais.DataKeys[e.Item.DataItemIndex];
            var nome = ((HiddenField)e.Item.FindControl("hdfNomeEstadual")).Value;
            switch (e.CommandName)
            {
                case "Visualizar":

                    if (nome.ToUpper().Contains("AÇÃO JOVEM") || nome.ToUpper().Contains("RENDA CIDADÃ") || nome.ToUpper().Contains("PROSPERA FAMÍLIA") || nome.Contains("Auxílio Aluguel"))
                    {
                        Response.Redirect("~/BlocoIII/FTransferenciaRenda.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    }
                    else
                    {
                        Response.Redirect(string.Format("~/BlocoIII/FProgramaProjeto.aspx?id={0}&p=e", Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString()))));
                    }
                    break;

                case "Servicos":

                    if (nome.ToUpper().Contains("AÇÃO JOVEM") || nome.ToUpper().Contains("RENDA CIDADÃ") || nome.ToUpper().Contains("PROSPERA FAMÍLIA") || nome.Contains("Auxílio Aluguel"))
                    {
                        Response.Redirect("~/BlocoIII/FTransferenciaRendaCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    }
                   
                    else
                    {
                        Response.Redirect("~/BlocoIII/FProgramaProjetoCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    }
                    break;

                case "Excluir":
                    try
                    {

                        using (var proxy = new ProxyProgramas())
                        {
                            if (nome.ToUpper().Contains("AÇÃO JOVEM") || nome.ToUpper().Contains("RENDA CIDADÃ") || nome.ToUpper().Contains("PROSPERA FAMÍLIA") || nome.Contains("Auxílio Aluguel"))
                            {
                                proxy.Service.DeleteTransferenciaRenda(Convert.ToInt32(key["Id"].ToString()));
                            }
                            else
                            {
                                proxy.Service.DeleteProgramaProjeto(Convert.ToInt32(key["Id"].ToString()));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    Response.Redirect("~/BlocoIII/CProgramasProjetos.aspx?msg=PE");
                    break;

                default:
                    break;
            }
        }

        protected void lstProgramasFederais_ItemDataBound(object sender, ListViewItemEventArgs e)  
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };

                var item = (ConsultaProgramaProjetoInfo)e.Item.DataItem;
                if (item == null)
                    return;

                if (item.Nome.ToLower().Contains("bpc na escola"))
                {
                    if (item.AderiuBPCNaEscola.HasValue)
                    {
                       
                        if (item.AderiuBPCNaEscola.Value == true)
                        {
                            item.Aderiu = 1;
                            item.PossuiProgramaBeneficio = item.PossuiProgramaBeneficio;
                        }
                        else
                        {
                            item.Aderiu = Convert.ToInt32(item.AderiuBPCNaEscola.Value);
                            item.PossuiProgramaBeneficio = "False";
                        }
                    }
                    else 
                    {
                        item.Aderiu = 0;
                    }
                }

                ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = item.TotalServicos == 0; // !Convert.ToBoolean(item.PossuiProgramaBeneficio);
                ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                ((HtmlTableCell)e.Item.FindControl("tdTotalServicos")).Visible = item.TotalServicos > 0; //Convert.ToBoolean(item.PossuiProgramaBeneficio);

                ((HiddenField)e.Item.FindControl("hdfNome")).Value = item.Nome;

                Permissao.VerificarPermissaoControles(controles, Session);
            }


        }

        protected void lstProgramasFederais_ItemCommand(object sender, ListViewCommandEventArgs e)  
        {
            var key = lstProgramasFederais.DataKeys[e.Item.DataItemIndex];
            var nome = ((HiddenField)e.Item.FindControl("hdfNome")).Value;

            switch (e.CommandName)
            {
                case "Visualizar":
                    if (nome.ToLower().Contains("bpc na escola"))
                    {
                        Response.Redirect("~/BlocoIII/VProgramaProjetoDetalhe.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    }
                    if (nome.ToUpper().Contains("ACESSUAS") || nome.ToUpper().Contains("PROGRAMA CRIANÇA FELIZ"))
                    {
                        Response.Redirect(string.Format("~/BlocoIII/FProgramaProjeto.aspx?id={0}&p=f", Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString()))));
                    }
                    else
                    {
                        Response.Redirect("~/BlocoIII/FTransferenciaRenda.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    }
                    break;

                case "Servicos":
                    if (nome.ToUpper().Contains("ACESSUAS") || nome.ToUpper().Contains("PROGRAMA CRIANÇA FELIZ"))
                    {
                        Response.Redirect("~/BlocoIII/FProgramaProjetoCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    }
                    else
                    {
                        Response.Redirect("~/BlocoIII/FTransferenciaRendaCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    }
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyProgramas())
                        {
                            if (nome.ToUpper().Contains("ACESSUAS") || nome.ToUpper().Contains("PROGRAMA CRIANÇA FELIZ"))
                            {
                                proxy.Service.DeleteProgramaProjeto(Convert.ToInt32(key["Id"].ToString()));
                            }
                            else
                            {
                                proxy.Service.DeleteTransferenciaRenda(Convert.ToInt32(key["Id"].ToString()));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    Response.Redirect("~/BlocoIII/CProgramasProjetos.aspx?msg=PE");
                    break;

                default:
                    break;
            }
        }

        protected void lstProgramasEstaduaisUm_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };

                var item = (dynamic)e.Item.DataItem;
                if (item == null)
                    return;
                ((HiddenField)e.Item.FindControl("hdfNomeEstadual")).Value = item.obj.Nome;
                ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = item.obj.TotalServicos == 0; //!Convert.ToBoolean(item.PossuiProgramaBeneficio);
                ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                ((HtmlTableCell)e.Item.FindControl("tdTotalServicos")).Visible = item.obj.TotalServicos > 0; // Convert.ToBoolean(item.PossuiProgramaBeneficio);

                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void lstProgramasEstaduaisUm_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstProgramasEstaduaisUm.DataKeys[e.Item.DataItemIndex];
            var nome = ((HiddenField)e.Item.FindControl("hdfNomeEstadual")).Value;
            switch (e.CommandName)
            {
                case "Visualizar":

                    if (nome.ToUpper().Contains("FORTALECIMENTO DO CADÚNICO") || nome.Contains("Fortalecimento da Vigilância Socioassistencial"))
                    {
                        Response.Redirect("~/BlocoIII/FTransferenciaRenda.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    }
                    else
                    {
                        Response.Redirect(string.Format("~/BlocoIII/FProgramaProjeto.aspx?id={0}&p=e", Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString()))));
                    }
                    break;

                case "Servicos":

                    if (nome.ToUpper().Contains("FORTALECIMENTO DO CADÚNICO") || nome.Contains("Fortalecimento da Vigilância Socioassistencial"))
                    {
                        Response.Redirect("~/BlocoIII/FTransferenciaRendaCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    }

                    else
                    {
                        Response.Redirect("~/BlocoIII/FProgramaProjetoCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    }
                    break;

                case "Excluir":
                    try
                    {

                        using (var proxy = new ProxyProgramas())
                        {
                            if (nome.ToUpper().Contains("FORTALECIMENTO DO CADÚNICO") || nome.Contains("Fortalecimento da Vigilância Socioassistencial"))
                            {
                                proxy.Service.DeleteTransferenciaRenda(Convert.ToInt32(key["Id"].ToString()));
                            }
                            else
                            {
                                proxy.Service.DeleteProgramaProjeto(Convert.ToInt32(key["Id"].ToString()));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    Response.Redirect("~/BlocoIII/CProgramasProjetos.aspx?msg=PE");
                    break;

                default:
                    break;
            }


        }
    }
}