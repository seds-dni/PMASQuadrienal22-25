using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoII
{
    public partial class FPopulacaoVulnerabilidade : System.Web.UI.Page
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

                //using (var proxy = new ProxyRedeProtecaoSocial())
                //{
                //    load(proxy);
                //}

                loadCaracterizacao();

                carregarIndicadores();
                //carregarCombos();
                verificarAlteracoes();

                

                #region Bloqueia , Desbloqueia e ordena Controles
                WebControl[] controles = {
                                                //btnSalvar, 
                                                 btnSalvarCaracterizacao 

                                                 //ddlSituacaoVulnerabilidade,                                                  
                                                 //ddlClassificacao,                                                  
                                                 //btnSalvar 
                                         };
                Permissao.VerificarPermissaoControles(controles, Session);
       
                #endregion
            }
        }

        void loadCaracterizacao()
        {
            using (var proxy = new ProxyPrefeitura())
            {
                var pre = proxy.Service.GetPrefeituraById(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                if (pre == null)
                    return;
                txtCaracterizacaoPopulacao.Text = pre.CaracterizacaoPopulacao;
                txtCaracterizacaoPopulacao.ReadOnly = true;
                btnSalvarCaracterizacao.Enabled = false;
            }

        }
                        
                
        void carregarIndicadores()
        {
            //ConsultaMunicipioIndicadoresInfo ind;
            //ind = new Seds.PMAS.QUADRIENAL.Negocio.AnaliseDiagnostica().GetMunicipioIndicadoresByMunicipio(SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio);
            NumberFormatInfo nfi_nodigit = new CultureInfo("pt-BR", false).NumberFormat;
            nfi_nodigit.NumberDecimalDigits = 0;

            ConsultaMunicipioPopulacaoVulnerabilidadeIndicadoresInfo populacaoVulnerabilidade;
            populacaoVulnerabilidade = new Seds.PMAS.QUADRIENAL.Negocio.AnaliseDiagnostica().GetPopulacaoVulnerabilidadeByMunicipio(SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio, Convert.ToInt32("2022"));
            if (populacaoVulnerabilidade == null)
                return;

            lblRazaoDependenciaMunicipio.Text = populacaoVulnerabilidade.PercTotalRazaoDependencia.HasValue ? populacaoVulnerabilidade.PercTotalRazaoDependencia.Value.ToString("N2") : "-";
            lblRazaoDependenciaDRADS.Text = populacaoVulnerabilidade.PercTotalRazaoDependenciaDRADS.HasValue ? populacaoVulnerabilidade.PercTotalRazaoDependenciaDRADS.Value.ToString("N2") : "-";
            lblPercIdade15AnosDRADS.Text = populacaoVulnerabilidade.PessoasAbaixo15AnosPercentualDRADS.Value.ToString();
            lblPercIdade15Anos.Text = populacaoVulnerabilidade.PessoasAbaixo15AnosPercentual.Value.ToString();
            lblNumeroPopulacao15.Text = populacaoVulnerabilidade.PessoasAbaixo15Anos.Value.ToString("N",nfi_nodigit);
            lblNumeroPopulacao15DRADS.Text = populacaoVulnerabilidade.PessoasAbaixo15AnosDRADS.Value.ToString("N", nfi_nodigit);
            lblIdososNumero.Text = populacaoVulnerabilidade.PessoasAcima60Anos.Value.ToString("N", nfi_nodigit);
            lblIdososNumeroDRADS.Text = populacaoVulnerabilidade.PessoasAcima60AnosDRADS.Value.ToString("N", nfi_nodigit);
            lblIdososPerc.Text = populacaoVulnerabilidade.PessoasAcima60AnosPercentual.Value.ToString();
            lblIdososPercDRADS.Text = populacaoVulnerabilidade.PessoasAcima60AnosPercentualDRADS.Value.ToString();
            lblEnvelhecimentoMunicipio.Text = populacaoVulnerabilidade.IndiceEnvelhecimento.Value.ToString("N2");
            lblEnvelhecimentoDRADS.Text = populacaoVulnerabilidade.IndiceEnvelhecimentoDRADS.Value.ToString("N2");
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro2.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 92);
                    linkAlteracoesQuadro2.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("92"));
                    //linkAlteracoesQuadro15.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 15);
                    //linkAlteracoesQuadro15.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("15"));
                }
            }
        }

        protected void btnSalvarCaracterizacao_Click(object sender, EventArgs e)
        {
            var msg = String.Empty;


            if (String.IsNullOrEmpty(txtCaracterizacaoPopulacao.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("O preenchimento do campo de caracterização do município em relação à evolução da rede de atendimento é obrigatório!"), true);
                return;
            }
            string regexCaracterizacao = Regex.Replace(txtCaracterizacaoPopulacao.Text.Trim(), @"\n", "");



            if (regexCaracterizacao.ToString().Length > 4000)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("O texto digitado ultrapassou o limite de 4000 caracteres e não poderá ser salvo desta forma. Por favor, reduza-o até o limite de 4000 caracteres."), true);
                return;
            }

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var blocoII = new Seds.PMAS.QUADRIENAL.UI.Processos.Prefeituras(proxy);
                    PrefeituraInfo pre = blocoII.GetPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    pre.CaracterizacaoPopulacao = txtCaracterizacaoPopulacao.Text;
                    new ValidadorPrefeitura().ValidarCaracterizacaoPopulacao(pre);
                    blocoII.UpdatePrefeitura(pre, false);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                lblInconsistencias.Text = msg;
                tbInconsistencias.Visible = true;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Caracterização da População e das vulnerabilidades sociais registrada com sucesso!";
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = String.Empty;
                tbInconsistencias.Visible = false;
                return;
            }

        }


    }
}