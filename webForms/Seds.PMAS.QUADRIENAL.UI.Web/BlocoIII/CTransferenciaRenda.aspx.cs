using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Web.UI.HtmlControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class CTransferenciaRenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "TI")
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

                txtFEAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtFEASAgendaFamilia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
               

                WebControl[] controles = { btnIncluir};
                Permissao.VerificarPermissaoControles(controles, Session);
               
                txtFEAS.ReadOnly = txtFEASAgendaFamilia.ReadOnly = !(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);

                ddlMesRepasseFEAS.Enabled = ddlAnoRepasseFEAS.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);
                ddlMesRepasseFEASAgendaFamilia.Enabled = ddlAnoRepasseFEASAgendaFamilia.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);
                rblRepasseFEAS.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);
                btnSalvar.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);                
            }

        }

        void load(ProxyProgramas proxy)
        {
            lstProgramasFederais.DataSource = proxy.Service.GetConsultaTransferenciasRendaFederalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            lstProgramasFederais.DataBind();

            lstProgramasEstaduais.DataSource = proxy.Service.GetConsultaTransferenciasRendaEstadualByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            lstProgramasEstaduais.DataBind();

            lstProgramasMunicipais.DataSource = proxy.Service.GetConsultaTransferenciasRendaMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            lstProgramasMunicipais.DataBind();

            var obj = proxy.Service.GetTransferenciaRendaById(Convert.ToInt32(hfSPSolidario.Value));
            if (obj == null)
                return;                        

            if (obj.SaoPauloSolidarioMesRepasseFEASBuscaAtiva.HasValue)
                ddlMesRepasseFEAS.SelectedValue = obj.SaoPauloSolidarioMesRepasseFEASBuscaAtiva.Value.ToString();
            if (obj.SaoPauloSolidarioAnoRepasseFEASBuscaAtiva.HasValue)
            {
                ddlAnoRepasseFEAS.SelectedValue = obj.SaoPauloSolidarioAnoRepasseFEASBuscaAtiva.Value.ToString();
                trRepasseFEAS.Visible = obj.SaoPauloSolidarioAnoRepasseFEASBuscaAtiva.Value == 2012;
            }

            if (obj.SaoPauloSolidarioValorFEASRetidoFMAS2013.HasValue)
                rblRepasseFEAS.SelectedValue = Convert.ToSByte(obj.SaoPauloSolidarioValorFEASRetidoFMAS2013).ToString();

            if (obj.SaoPauloSolidarioValorFEASBuscaAtiva.HasValue)
                txtFEAS.Text = obj.SaoPauloSolidarioValorFEASBuscaAtiva.Value.ToString("N2");            

            if (obj.SaoPauloSolidarioMesRepasseFEASAgendaFamilia.HasValue)
                ddlMesRepasseFEASAgendaFamilia.SelectedValue = obj.SaoPauloSolidarioMesRepasseFEASAgendaFamilia.Value.ToString();
            if (obj.SaoPauloSolidarioAnoRepasseFEASAgendaFamilia.HasValue)
                ddlAnoRepasseFEASAgendaFamilia.SelectedValue = obj.SaoPauloSolidarioAnoRepasseFEASAgendaFamilia.Value.ToString();

            if (obj.SaoPauloSolidarioValorFEASAgendaFamilia.HasValue)
                txtFEASAgendaFamilia.Text = obj.SaoPauloSolidarioValorFEASAgendaFamilia.Value.ToString("N2");
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro43.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 43);
                    linkAlteracoesQuadro43.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("43"));
                    linkAlteracoesQuadro46.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 46);
                    linkAlteracoesQuadro46.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("46"));                    
                    linkAlteracoesQuadro50.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 50);
                    linkAlteracoesQuadro50.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("50"));
                }
            }
        }

        protected void lstProgramas_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = e.CommandArgument.ToString();
                        
            switch (e.CommandName)
            {
                case "Visualizar":
                    var tipo = Convert.ToInt32(((ListView)sender).DataKeys[e.Item.DataItemIndex]["IdTipoTransferenciaRenda"]);
                    if (tipo == 9)
                    {
                        Response.Redirect("~/BlocoIII/FSaoPauloSolidario.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key)));
                        break;
                    }

                    Response.Redirect("~/BlocoIII/FTransferenciaRenda.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/FTransferenciaRendaCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key)));
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
                    Response.Redirect("~/BlocoIII/CTransferenciaRenda.aspx?msg=TE");
                    break;
                default:
                    break;
            }
        }

        protected void lstProgramas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (ConsultaTransferenciaRendaInfo)e.Item.DataItem;
                if(item == null)
                    return;

                if (((ETipoTransferenciaRenda)item.IdTipoTransferenciaRenda) == ETipoTransferenciaRenda.SaoPauloSolidario)
                    hfSPSolidario.Value = item.Id.ToString();
                
                var btn = ((ImageButton)e.Item.FindControl("btnExcluir"));                

                if (item.Aderiu == 0)
                {
                    ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = true;
                    ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                    ((HtmlTableCell)e.Item.FindControl("tdUnidades")).Visible =
                    ((HtmlTableCell)e.Item.FindControl("tdServicos")).Visible = false;
                    if (item.IdTipoTransferenciaRenda != 8)//OUTROS
                        btn.Visible = false;
                }
                else
                {
                    ((HtmlTableCell)e.Item.FindControl("tdNao")).Visible = item.IntegracaoRede == 0;
                    ((HtmlTableCell)e.Item.FindControl("tdVisualizarServicos")).Visible =
                    ((HtmlTableCell)e.Item.FindControl("tdUnidades")).Visible =
                    ((HtmlTableCell)e.Item.FindControl("tdServicos")).Visible = item.IntegracaoRede > 0;
                    Permissao.VerificarPermissaoControles(new[] { btn }, Session);
                }
            }
        }

        protected void ddlAnoRepasseFEAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            trRepasseFEAS.Visible = ddlAnoRepasseFEAS.SelectedValue == "2012";
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            var obj = new TransferenciaRendaInfo();  
            using(ProxyProgramas proxy = new ProxyProgramas())
            {
                obj = proxy.Service.GetTransferenciaRendaById(Convert.ToInt32(hfSPSolidario.Value));
            }

            obj.SaoPauloSolidarioValorFEASBuscaAtiva = null;
            obj.SaoPauloSolidarioMesRepasseFEASBuscaAtiva = null;
            obj.SaoPauloSolidarioAnoRepasseFEASBuscaAtiva = null;
            obj.SaoPauloSolidarioValorFEASRetidoFMAS2013 = null;
            obj.SaoPauloSolidarioValorFEASAgendaFamilia = null;
            obj.SaoPauloSolidarioMesRepasseFEASAgendaFamilia = null;
            obj.SaoPauloSolidarioAnoRepasseFEASAgendaFamilia = null;

            if (!String.IsNullOrEmpty(txtFEAS.Text))
            {
                obj.SaoPauloSolidarioValorFEASBuscaAtiva = Convert.ToDecimal(txtFEAS.Text);
                if (obj.SaoPauloSolidarioValorFEASBuscaAtiva.Value == 0)
                    obj.SaoPauloSolidarioValorFEASBuscaAtiva = null;
            }
            
            if (obj.SaoPauloSolidarioValorFEASBuscaAtiva.HasValue && obj.SaoPauloSolidarioValorFEASBuscaAtiva.Value > 0)
            {
                if (ddlMesRepasseFEAS.SelectedIndex != 0)
                    obj.SaoPauloSolidarioMesRepasseFEASBuscaAtiva = Convert.ToInt32(ddlMesRepasseFEAS.SelectedValue);
                if (ddlAnoRepasseFEAS.SelectedIndex != 0)
                    obj.SaoPauloSolidarioAnoRepasseFEASBuscaAtiva = Convert.ToInt32(ddlAnoRepasseFEAS.SelectedValue);
                if (ddlAnoRepasseFEAS.SelectedValue == "2012")
                    obj.SaoPauloSolidarioValorFEASRetidoFMAS2013 = Convert.ToBoolean(Convert.ToInt32(rblRepasseFEAS.SelectedValue));
            }


            if (!String.IsNullOrEmpty(txtFEASAgendaFamilia.Text))
            {
                obj.SaoPauloSolidarioValorFEASAgendaFamilia = Convert.ToDecimal(txtFEASAgendaFamilia.Text);
                if (obj.SaoPauloSolidarioValorFEASAgendaFamilia.Value == 0)
                    obj.SaoPauloSolidarioValorFEASAgendaFamilia = null;
            }

            if (obj.SaoPauloSolidarioValorFEASAgendaFamilia.HasValue && obj.SaoPauloSolidarioValorFEASAgendaFamilia.Value > 0)
            {
                if (ddlMesRepasseFEASAgendaFamilia.SelectedIndex != 0)
                    obj.SaoPauloSolidarioMesRepasseFEASAgendaFamilia = Convert.ToInt32(ddlMesRepasseFEASAgendaFamilia.SelectedValue);
                if (ddlAnoRepasseFEASAgendaFamilia.SelectedIndex != 0)
                    obj.SaoPauloSolidarioAnoRepasseFEASAgendaFamilia = Convert.ToInt32(ddlAnoRepasseFEASAgendaFamilia.SelectedValue);
            }
                       

            try
            {
                var lstMsg = new List<string>();
                var feas = obj.SaoPauloSolidarioValorFEASBuscaAtiva.HasValue ? obj.SaoPauloSolidarioValorFEASBuscaAtiva.Value : 0m;                

                if (feas > 0 && !obj.SaoPauloSolidarioMesRepasseFEASBuscaAtiva.HasValue || obj.SaoPauloSolidarioMesRepasseFEASBuscaAtiva == 0)
                {
                    lstMsg.Add("O campo Mês em que foi realizado o repasse da Busca Ativa é obrigatório!");
                }

                if (feas > 0 && !obj.SaoPauloSolidarioAnoRepasseFEASBuscaAtiva.HasValue || obj.SaoPauloSolidarioAnoRepasseFEASBuscaAtiva == 0)
                {
                    lstMsg.Add("O campo Ano em que foi realizado o repasse da Busca Ativa é obrigatório!");
                }

                var feasAgendaFamilia = obj.SaoPauloSolidarioValorFEASAgendaFamilia.HasValue ? obj.SaoPauloSolidarioValorFEASAgendaFamilia.Value : 0m;                
                if (feasAgendaFamilia > 0 && !obj.SaoPauloSolidarioMesRepasseFEASAgendaFamilia.HasValue || obj.SaoPauloSolidarioMesRepasseFEASAgendaFamilia == 0)
                {
                    lstMsg.Add("O campo Mês em que foi realizado o repasse da Agenda da Família é obrigatório!");
                }

                if (feasAgendaFamilia > 0 && !obj.SaoPauloSolidarioAnoRepasseFEASAgendaFamilia.HasValue || obj.SaoPauloSolidarioAnoRepasseFEASAgendaFamilia == 0)
                {
                    lstMsg.Add("O campo Ano em que foi realizado o repasse da Agenda da Família é obrigatório!");
                }

                if (lstMsg.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK(Extensions.Concat(lstMsg, "<br/>")), true);                    
                    return;
                }

                using (var proxy = new ProxyProgramas())
                {
                    proxy.Service.SaveValoresSaoPauloSolidario(obj);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>")), true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Valores registrados com sucesso!"), true);
        }
    }
}