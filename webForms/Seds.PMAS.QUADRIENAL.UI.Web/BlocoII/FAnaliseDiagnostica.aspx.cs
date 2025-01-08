using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using System.Globalization;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoII
{
    public partial class FAnaliseDiagnostica : System.Web.UI.Page
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
                loadCaracterizacao();

                carregarIndicadores();

                verificarAlteracoes();

                txtCaracterizacao.ReadOnly = true;
                btnSalvarCaracterizacao.Enabled = false;
                #region Bloqueia , Desbloqueia e ordena Controles
                WebControl[] controles = { txtCaracterizacao,
                                             btnSalvarCaracterizacao
                                         };
                Permissao.VerificarPermissaoControles(controles, Session);
                #endregion
            }

        }

       

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro14.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 14);
                    linkAlteracoesQuadro14.HRef = "./HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("14"));
                }
            }
        }

        void carregarIndicadores()
        {
            ConsultaDemografiaTerritorioIndicadoresInfo demografia;
            demografia = new Seds.PMAS.QUADRIENAL.Negocio.AnaliseDiagnostica().GetDemografiaIndicadoresByMunicipio(SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio, Convert.ToInt32("2022"));
            if (demografia == null)
                return;

            NumberFormatInfo nfi_nodigit = new CultureInfo("pt-BR", false).NumberFormat;
            nfi_nodigit.NumberDecimalDigits = 0;


            lblAreaTerritorial.Text = demografia.AreaTerritorial.ToString();
            lblNumeroHabitantes.Text = demografia.NumeroHabitantes.ToString("N",nfi_nodigit);
            lblTaxaGeometrica.Text = demografia.TaxaGeometricaCrescimento.HasValue ? demografia.TaxaGeometricaCrescimento.Value.ToString("N2") : "Sem informação";
            lblDomicilios.Text = demografia.TotalDomiciliosParticularesPermanentes.ToString("N",nfi_nodigit);
            lblDensidade.Text = demografia.DensidadeDemografica.HasValue ? demografia.DensidadeDemografica.Value.ToString() : "-";
            lblGrauUrbanizacao.Text = demografia.GrauUrbanizacao.HasValue ? demografia.GrauUrbanizacao.Value.ToString("N2") : "-";
            lblAreaTerritorialDRADS.Text = demografia.AreaTerritorialDRADS.Value.ToString("N3");
            lblNumeroHabitantesDRADS.Text = demografia.NumeroHabitantesDRADS.ToString("N",nfi_nodigit);
            lblTaxaGeometricaDRADS.Text = demografia.TaxaGeometricaCrescimentoDRADS.Value != 0 ? demografia.TaxaGeometricaCrescimentoDRADS.Value.ToString("N2") : "Sem informação";
            lblDomiciliosDRADS.Text = demografia.TotalDomiciliosParticularesPermanentesDRADS.ToString("N",nfi_nodigit);
            lblDensidadeDRADS.Text = demografia.DensidadeDemograficaDRADS.HasValue ? demografia.DensidadeDemograficaDRADS.Value.ToString() : "-";
            lblGrauUrbanizacaoDRADS.Text = demografia.GrauUrbanizacaoDRADS.HasValue ? demografia.GrauUrbanizacaoDRADS.Value.ToString("N2") : "-";
            lblPessoasDomicilios.Text = demografia.NumeroPessoasDomicilios.HasValue ? demografia.NumeroPessoasDomicilios.Value.ToString("N1") : "-";
            lblPessoasDomiciliosDRADS.Text = demografia.NumeroPessoasDomiciliosDRADS.HasValue ? demografia.NumeroPessoasDomiciliosDRADS.Value.ToString("N1") : "-";
            //MunicipioIndicadoresInfo ind;
            //using (var proxy = new ProxyDivisaoAdministrativa())
            //{
            //    ind = proxy.Service.GetIndicadoresByMunicipio(SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio);
            //}

            //if (ind == null)
            //    return;
            //lblFamiliasCadUnico2010.Text = ind.TotalPercentualFamiliasBolsaFamiliaCadUnico2010.HasValue ? ind.TotalPercentualFamiliasBolsaFamiliaCadUnico2010.Value.ToString("N2") : "-";
            //lblFamiliasCadUnico2012.Text = ind.TotalPercentualFamiliasBolsaFamiliaCadUnico2012.HasValue ? ind.TotalPercentualFamiliasBolsaFamiliaCadUnico2012.Value.ToString("N2") : "-";

            //lblNumeroFamilias.Text = ind.TotalFamilias2010.HasValue ? ind.TotalFamilias2010.Value.ToString("0,000") : "-";

            //lblDomicilios.Text = demografia.TotalSaneamento.HasValue ? demografia.TotalSaneamento.Value.ToString("N2") : "-";
            //ind.TotalPercentualDomiciliosComSaneamentoAdequado2010.Value.ToString("N2") : "-";

            //lblRendimentoAte255.Text = ind.PercentualRendimentoMensalDomiciliarPerCapitaAte255Reais2010.HasValue ? ind.PercentualRendimentoMensalDomiciliarPerCapitaAte255Reais2010.Value.ToString("N2") : "-";
            //lblRendimentoAte70.Text = ind.PercentualRendimentoMensalDomiciliarPerCapitaAte70Reais2010.HasValue ? ind.PercentualRendimentoMensalDomiciliarPerCapitaAte70Reais2010.Value.ToString("N2") : "-";


            // lblIdososPerc.Text = ind.TotalPercentualIdosos.ToString("N2");
            //lblMaesAdolescentesPerc.Text = ind.TotalPercentualMaesAdolescentes2009.ToString("N2");

            //lblIndiceGini2010.Text = ind.IndiceGini2010.HasValue ? ind.IndiceGini2010.Value.ToString().Replace(",", ".") : "";
            //lblIndiceGini2000.Text = ind.IndiceGini2000.HasValue ? ind.IndiceGini2000.Value.ToString().Replace(",", ".") : "";

            //lblIDHM2010.Text = ind.IDHM2010.HasValue ? ind.IDHM2010.Value.ToString().Replace(",", ".") : "";
            //lblIDHM2000.Text = ind.IDHM2000.HasValue ? ind.IDHM2000.Value.ToString().Replace(",", ".") : "";
        }

        //void carregarCombos()
        //{
        //    using (var proxy = new ProxyEstruturaAssistenciaSocial())
        //    {
        //        ddlSituacaoVulnerabilidade.DataSource = proxy.Service.GetSituacoesVulnerabilidade().OrderBy(t=> t.Ordem);
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
        //    lstAnaliseDiagnostica.DataSource = lst;
        //    lstAnaliseDiagnostica.DataBind();
        //    carregarComboClassificacao(lst.ToList());
        //}

        void loadCaracterizacao()
        {
            using (var proxy = new ProxyPrefeitura())
            {
                var pre = proxy.Service.GetPrefeituraById(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                if (pre == null)
                    return;
                txtCaracterizacao.Text = pre.Caracterizacao;
            }

        }

        protected void btnSalvarCaracterizacao_Click(object sender, EventArgs e)
        {
            var msg = String.Empty;

            if (String.IsNullOrEmpty(txtCaracterizacao.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("O preenchimento do campo de caracterização do município em relação a território e demografia é obrigatório!"), true);
                return;
            }

            if (txtCaracterizacao.Text.Trim().Length > 4000)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("O texto digitado ultrapassou o limite de 4000 caracteres e não poderá ser salvo desta forma. Por favor, reduza-o até o limite de 4000 caracteres."), true);
                return;
            }

            try
            {

                using (var proxy = new ProxyPrefeitura())
                {
                    var blocoI = new Seds.PMAS.QUADRIENAL.UI.Processos.Prefeituras(proxy);
                    PrefeituraInfo pre = blocoI.GetPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    pre.Caracterizacao = txtCaracterizacao.Text;
                    new ValidadorPrefeitura().ValidarCaracterizacao(pre);
                    blocoI.UpdatePrefeitura(pre, false);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Caracterização do município em relação a território e demografia registrada com sucesso!";
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                //lblInconsistencias.Text = String.Empty;
                //tbInconsistencias.Visible = false;
                return;
            }
            else
            {
                lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
            }
        }

        //protected void btnSalvar_Click(object sender, EventArgs e)
        //{
        //    var msg = String.Empty;

        //    try
        //    {
        //        var analise = new AnaliseDiagnosticaInfo();                
        //        analise.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
        //        if(!String.IsNullOrEmpty(ddlSituacaoVulnerabilidade.SelectedValue))
        //            analise.IdSituacaoVulnerabilidade = Convert.ToInt32(ddlSituacaoVulnerabilidade.SelectedValue);
        //        if (!String.IsNullOrEmpty(ddlClassificacao.SelectedValue))
        //            analise.Classificacao = Convert.ToInt32(ddlClassificacao.SelectedValue);
        //        if(!String.IsNullOrEmpty(txtDemanda.Text))
        //            analise.Demanda = Convert.ToInt32(txtDemanda.Text);

        //        new ValidadorAnaliseDiagnostica().Validar(analise);

        //        using (var proxy = new ProxyRedeProtecaoSocial())
        //        {
        //           // if (analise.Id == 0)
        //                proxy.Service.AddAnaliseDiagnostica(analise);
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
        //        switch (e.CommandName)Caracterização so municipio em relação a Território e demografia registrada com sucesso!
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
    }
}