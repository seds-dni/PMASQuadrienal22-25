using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoII
{
    public partial class FAnaliseInterpretacao : System.Web.UI.Page
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

                using (var proxy = new ProxyPrefeitura())
                {
                   // load(proxy);
                    loadCaracterizacao(proxy);
                }

            
                //   carregarIndicadores();
                //carregarCombos();
                //verificarAlteracoes();
                txtAnaliseInterpretacao.ReadOnly = true;
                btnSalvarCaracterizacao.Enabled = false;
                #region Bloqueia , Desbloqueia e ordena Controles
                //WebControl[] controles = { txtAnaliseInterpretacao, btnSalvarCaracterizacao };
                WebControl[] controles = { txtAnaliseInterpretacao, btnSalvarCaracterizacao };
                Permissao.VerificarPermissaoControles(controles, Session);
                #endregion
            }

        }

        void loadCaracterizacao(ProxyPrefeitura proxy)
        {
            var pre = proxy.Service.GetPrefeituraById(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (pre == null)
                return;
            txtCaracterizacaoDemografia.Text = pre.Caracterizacao;
            txtCaracterizacaoPopulacao.Text = pre.CaracterizacaoPopulacao;
            txtCaracterizacaoRedeSocioAssistencial.Text = pre.CaracterizacaoRedeSocioassistencial;
            txtAnaliseInterpretacao.Text = pre.CaracterizacaoAnaliseInterpretacao;
        }

        void carregarIndicadores()
        {
            ConsultaMunicipioIndicadoresInfo ind;
            ind = new Seds.PMAS.QUADRIENAL.Negocio.AnaliseDiagnostica().GetMunicipioIndicadoresByMunicipio(SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio);
            if (ind == null)
                return;
        }

        //void carregarCombos()
        //{
        //    using (var proxy = new ProxyEstruturaAssistenciaSocial())
        //    {
        //        ddlSituacaoVulnerabilidade.DataSource = proxy.Service.GetSituacoesVulnerabilidade().OrderBy(t => t.Ordem);
        //        ddlSituacaoVulnerabilidade.DataValueField = "Id";
        //        ddlSituacaoVulnerabilidade.DataTextField = "Nome";
        //        ddlSituacaoVulnerabilidade.DataBind();
        //        ddlSituacaoVulnerabilidade.Items.Insert(0, new ListItem(" Selecione ", "0"));
        //    }

        //}

        //void carregarComboClassificacao(List<ConsultaAnaliseDiagnosticaInfo> analise)
        //{
        //    ddlClassificacao.DataSource = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.Where(t => !analise.Any(a => a.Classificacao == t));
        //    ddlClassificacao.DataBind();
        //    ddlClassificacao.Items.Insert(0, new ListItem(" Selecione ", "0"));
        //    ddlClassificacao.ClearSelection();
        //}

        //void load(ProxyRedeProtecaoSocial proxy)
        //{
        //    var lst = proxy.Service.GetConsultaAnaliseDiagnosticaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id).OrderBy(t => t.Classificacao);
        //    //lstAnaliseDiagnostica.DataSource = lst;
        //    //lstAnaliseDiagnostica.DataBind();
        //    //carregarComboClassificacao(lst.ToList());
        //}


        //protected void btnSalvar_Click(object sender, EventArgs e)
        //{
        //    var msg = String.Empty;



        //    try
        //    {
        //        var analise = new AnaliseDiagnosticaInfo();
        //        analise.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
        //        if (!String.IsNullOrEmpty(ddlSituacaoVulnerabilidade.SelectedValue))
        //            analise.IdSituacaoVulnerabilidade = Convert.ToInt32(ddlSituacaoVulnerabilidade.SelectedValue);
        //        if (!String.IsNullOrEmpty(ddlClassificacao.SelectedValue))
        //            analise.Classificacao = Convert.ToInt32(ddlClassificacao.SelectedValue);
        //        if (!String.IsNullOrEmpty(txtDemanda.Text))
        //            analise.Demanda = Convert.ToInt32(txtDemanda.Text);

        //        new ValidadorAnaliseDiagnostica().Validar(analise);

        //        using (var proxy = new ProxyRedeProtecaoSocial())
        //        {
        //            // if (analise.Id == 0)
        //            proxy.Service.AddAnaliseDiagnostica(analise);
        //            //else
        //            //    proxy.Service.UpdateAnaliseDiagnostica(analise);

        //            load(proxy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //    }

        //    if (String.IsNullOrEmpty(msg))
        //    {
        //        msg = "Situação de vulnerabilidade e/ou risco registrada com sucesso!";
        //        lblInconsistencias.Text = "";
        //        tbInconsistencias.Visible = false;
        //        var script = Util.GetJavaScriptDialogOK(msg);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
        //        txtDemanda.Text = String.Empty;
        //        ddlSituacaoVulnerabilidade.SelectedIndex = 0;
        //        return;
        //    }

        //    lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
        //    tbInconsistencias.Visible = true;
        //}

        //protected void lstAnaliseDiagnostica_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    var key = lstAnaliseDiagnostica.DataKeys[e.Item.DataItemIndex];
        //    try
        //    {
        //        switch (e.CommandName)
        //        {
        //            case "Excluir":
        //                using (var proxy = new ProxyRedeProtecaoSocial())
        //                {
        //                    proxy.Service.DeleteAnaliseDiagnostica(Convert.ToInt32(key["Id"]));
        //                    load(proxy);
        //                    var script = Util.GetJavaScriptDialogOK("Registro excluído com sucesso!");
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
        //                }
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var script = Util.GetJavaScriptDialogOK(ex.Message);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
        //    }
        //}

        //protected void lstAnaliseDiagnostica_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };

        //        var item = (ConsultaAnaliseDiagnosticaInfo)e.Item.DataItem;
        //        if (item == null)
        //            return;

        //        Util.VerificaPermissao(controles, Session);
        //    }
        //}


        //void verificarAlteracoes()
        //{
        //    if (Util.VerificarAlteracoes())
        //    {
        //        using (var proxy = new ProxyPlanoMunicipal())
        //            //{
        //            //    linkAlteracoesQuadro14.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 14);
        //            //    linkAlteracoesQuadro14.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("14"));
        //            linkAlteracoesQuadro15.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 15);
        //        linkAlteracoesQuadro15.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("15"));
        //    }
        //}

        protected void btnSalvarCaracterizacao_Click(object sender, EventArgs e)
        {
            var msg = String.Empty;
            if (String.IsNullOrEmpty(txtAnaliseInterpretacao.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("O preenchimento do campo de análise e interpretação é obrigatório!"), true);
                return;
            }

            if (txtAnaliseInterpretacao.Text.Trim().Length > 4000)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("O texto digitado ultrapassou o limite de 4000 caracteres e não poderá ser salvo desta forma. Por favor, reduza-o até o limite de 4000 caracteres."), true);
                return;
            }

            try
            {

                using (var proxy = new ProxyPrefeitura())
                {
                    var blocoI = new Seds.PMAS.QUADRIENAL.UI.Processos.Prefeituras(proxy);
                    PrefeituraInfo pre = blocoI.GetPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    pre.CaracterizacaoAnaliseInterpretacao = txtAnaliseInterpretacao.Text;
                    new ValidadorPrefeitura().ValidarCaracterizacaoAnaliseInterpretacao(pre);
                    blocoI.UpdatePrefeitura(pre, false);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Diagnóstico Socioterritorial registrado com sucesso!";
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                //lblInconsistencias.Text = String.Empty;
                //tbInconsistencias.Visible = false;
                return;
            }
        }


    }

}
