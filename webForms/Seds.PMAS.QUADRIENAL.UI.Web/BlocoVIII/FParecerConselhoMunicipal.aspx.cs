using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Processos;
using Microsoft.IdentityModel.Claims;
using System.Threading;
using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Pendencia;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoVIII
{
    public partial class FParecerConselhoMunicipal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "PA")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Plano aprovado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "PR")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Plano rejeitado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "OK")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Parecer registrado com sucesso! Por favor, registre no sistema a deliberação final do CMAS sobre o Plano Municipal no quadro 81 no final desta tela."), true);

                }


                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                using (var proxy = new ProxyPlanoMunicipal())
                {
                    load(proxy);
                }
            }
        }

        void load(ProxyPlanoMunicipal proxy)
        {
            ConselhoMunicipalParecerInfo conselhoMunicipalParecer = proxy.Service.GetParecerConselhoMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (conselhoMunicipalParecer != null)
            {
                hdfId.Value = conselhoMunicipalParecer.Id.ToString();
                txtParecer.Text = conselhoMunicipalParecer.ParecerCMAS;
                if (conselhoMunicipalParecer.AprovaPMAS.HasValue)
                {
                    rblAprovacao.SelectedValue = Convert.ToSByte(conselhoMunicipalParecer.AprovaPMAS.Value).ToString();
                }
                rblAvaliandoExecucao.SelectedValue = Convert.ToSByte(conselhoMunicipalParecer.AvaliandoExecucao).ToString();
                rblprestacaoconta.SelectedValue = Convert.ToSByte(conselhoMunicipalParecer.AcompanhaPrestacaoConta).ToString();
                rblrecursofinanceiro.SelectedValue = Convert.ToSByte(conselhoMunicipalParecer.AcompanhaRepasseRecursoFinanceiro).ToString();
                rblredeexecutora.SelectedValue = Convert.ToSByte(conselhoMunicipalParecer.MonitoraRedeExecutora).ToString();
                rblRegistradoAta.SelectedValue = Convert.ToSByte(conselhoMunicipalParecer.AtaRegistrada).ToString();
                if (conselhoMunicipalParecer.HouveParticipacaoPlanejamentoAcoes.HasValue)
                {
                    rblParticipacaoPlanejamentoAcoes.SelectedValue = Convert.ToSByte(conselhoMunicipalParecer.HouveParticipacaoPlanejamentoAcoes.Value).ToString();
                }
                txtParticipacaoPlanejamentoAcoes.Text = conselhoMunicipalParecer.ComentarioParticipacaoPlanejamentoAcoes;
                txtPresidente.Text = conselhoMunicipalParecer.PresidenteRepresentanteLegal;
                if (conselhoMunicipalParecer.Data.HasValue)
                {
                    txtData.Text = conselhoMunicipalParecer.Data.Value.ToShortDateString();
                }
                txtNumeroConselheiros.Text = conselhoMunicipalParecer.NumeroConselheiros.ToString();
                txtComentarioAvaliandoExecucao.Text = conselhoMunicipalParecer.ComentarioAvaliandoExecucao;
                txtComentarioRecursoFinanceiro.Text = conselhoMunicipalParecer.ComentarioAcompanhaRepasseRecursoFinanceiro;
                txtPrestacaoConta.Text = conselhoMunicipalParecer.ComentarioAcompanhaPrestacaoConta;
                txtRedeExecutora.Text = conselhoMunicipalParecer.ComentarioMonitoraRedeExecutora;

                tbAprovacao.Visible = true;
            }


            #region Bloqueia , Desbloqueia e ordena Controles
            WebControl[] controles = {  txtParecer,
                                        rblAprovacao,
                                        rblAvaliandoExecucao,
                                        rblprestacaoconta,
                                        rblrecursofinanceiro,
                                        rblredeexecutora,
                                        rblParticipacaoPlanejamentoAcoes,
                                        rblRegistradoAta,
                                        txtPresidente,
                                        txtNumeroConselheiros,
                                        txtComentarioAvaliandoExecucao,
                                        txtComentarioRecursoFinanceiro,
                                        txtPrestacaoConta,
                                        txtRedeExecutora,
                                        txtParticipacaoPlanejamentoAcoes,
                                        btnSalvar,
                                        btnSalvarParecer,                                        
                                             };

            controles = controles.Union(txtData.Controles).ToArray<WebControl>();
            Permissao.BlocoVIII.VerificarPermissaoCMASParecer(controles);

            if (btnSalvarParecer.Enabled)
            {
                IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
                IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
                var id = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault();

                using (var proxyPrefeitura = new ProxyPrefeitura())
                {
                    var presidente = proxyPrefeitura.Service.GetConselhoMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    btnSalvar.Enabled = btnSalvarParecer.Enabled = presidente != null && presidente.IdUsuarioPresidente == Convert.ToInt32(id.Value);
                }
            }
            #endregion
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            String msg = String.Empty;

            if (rblAprovacao.SelectedIndex == -1)
            {
                msg = "Assinale uma das opções (Favorável ou Desfavorável)";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
                lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            try
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                 
                    if (SessaoPmas.UsuarioLogado.EnumPerfil.Value == EPerfil.CMAS)
                    {
                        //if (new VerificadorPendenciaPMAS().PlanoMunicipalPossuiPendenciaCMAS(SessaoPmas.UsuarioLogado.Prefeitura.Id, EPerfil.CMAS))
                        //{
                        //    throw new Exception("O Plano Municipal possui pendências!");
                        //}
                        //proxy.Service.AprovarRejeitarPlanoMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id, SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao, rblAprovacao.SelectedValue == "1", EPerfil.CMAS);
                        proxy.Service.AprovarRejeitarPlanoMunicipalCMAS(SessaoPmas.UsuarioLogado.Prefeitura.Id, SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao, rblAprovacao.SelectedValue == "1", EPerfil.CMAS);
                    }
                    else {
                        //if (new VerificadorPendenciaPMAS().PlanoMunicipalPossuiPendenciaCMAS(SessaoPmas.UsuarioLogado.Prefeitura.Id, EPerfil.Inexistente))
                        //{
                        //    throw new Exception("O Plano Municipal possui pendências!");
                        //}
                        //proxy.Service.AprovarRejeitarPlanoMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id, SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao,rblAprovacao.SelectedValue == "1", EPerfil.Inexistente);
                        proxy.Service.AprovarRejeitarPlanoMunicipalCMAS(SessaoPmas.UsuarioLogado.Prefeitura.Id, SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao, rblAprovacao.SelectedValue == "1", EPerfil.CMAS);
                    }
                    Util.CarregarPrefeitura();
                    this.Master.CarregarDadosPlano();
                    load(proxy);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                Response.Redirect("~/BlocoVIII/FParecerConselhoMunicipal.aspx?msg=" + (rblAprovacao.SelectedValue == "1" ? "PA" : "PR"));
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }

        protected void btnSalvarParecer_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var obj = new ConselhoMunicipalParecerInfo();
            obj.Id = Convert.ToInt32(hdfId.Value);
            obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            obj.AcompanhaRepasseRecursoFinanceiro = rblrecursofinanceiro.SelectedValue == "1";
            obj.AcompanhaPrestacaoConta = rblprestacaoconta.SelectedValue == "1";
            obj.AvaliandoExecucao = rblAvaliandoExecucao.SelectedValue == "1";
            obj.MonitoraRedeExecutora = rblredeexecutora.SelectedValue == "1";
            obj.HouveParticipacaoPlanejamentoAcoes = rblParticipacaoPlanejamentoAcoes.SelectedValue == "1";

            if (!String.IsNullOrEmpty(txtComentarioAvaliandoExecucao.Text))
            {
                if (txtComentarioAvaliandoExecucao.Text.Length > 500)
                    txtComentarioAvaliandoExecucao.Text = txtComentarioAvaliandoExecucao.Text.Substring(0, 500);
                obj.ComentarioAvaliandoExecucao = txtComentarioAvaliandoExecucao.Text;
            }
            if (!String.IsNullOrEmpty(txtComentarioRecursoFinanceiro.Text))
            {
                if (txtComentarioRecursoFinanceiro.Text.Length > 500)
                    txtComentarioRecursoFinanceiro.Text = txtComentarioRecursoFinanceiro.Text.Substring(0, 500);
                obj.ComentarioAcompanhaRepasseRecursoFinanceiro = txtComentarioRecursoFinanceiro.Text;
            }
            if (!String.IsNullOrEmpty(txtPrestacaoConta.Text))
            {
                if (txtPrestacaoConta.Text.Length > 500)
                    txtPrestacaoConta.Text = txtPrestacaoConta.Text.Substring(0, 500);
                obj.ComentarioAcompanhaPrestacaoConta = txtPrestacaoConta.Text;
            }
            if (!String.IsNullOrEmpty(txtRedeExecutora.Text))
            {
                if (txtRedeExecutora.Text.Length > 500)
                    txtRedeExecutora.Text = txtRedeExecutora.Text.Substring(0, 500);
                obj.ComentarioMonitoraRedeExecutora = txtRedeExecutora.Text;
            }

            if (!String.IsNullOrEmpty(txtParticipacaoPlanejamentoAcoes.Text))
            {
                if (txtParticipacaoPlanejamentoAcoes.Text.Length > 500)
                    txtParticipacaoPlanejamentoAcoes.Text = txtParticipacaoPlanejamentoAcoes.Text.Substring(0, 500);
                obj.ComentarioParticipacaoPlanejamentoAcoes = txtParticipacaoPlanejamentoAcoes.Text;
            }

            if (!String.IsNullOrEmpty(txtNumeroConselheiros.Text))
                obj.NumeroConselheiros = Convert.ToInt32(txtNumeroConselheiros.Text);

            if (!String.IsNullOrEmpty(txtPresidente.Text))
                obj.PresidenteRepresentanteLegal = txtPresidente.Text;

            if (!String.IsNullOrEmpty(txtParecer.Text))
            {
                if (txtParecer.Text.Length > 8000)
                    txtParecer.Text = txtParecer.Text.Substring(0, 800);
                obj.ParecerCMAS = txtParecer.Text;
            }

            obj.AtaRegistrada = rblRegistradoAta.SelectedValue == "1";

            DateTime dt;
            if (!String.IsNullOrEmpty(txtData.Text) && DateTime.TryParse(txtData.Text, out dt))
                obj.Data = Convert.ToDateTime(txtData.Text);

            String msg = String.Empty;
            try
            {
                new ValidadorConselhoMunicipalParecer().Validar(obj);

                using (var proxy = new ProxyPlanoMunicipal())
                {
                    proxy.Service.SaveParecerConselhoMunicipal(obj);
                    Util.CarregarPrefeitura();
                    this.Master.CarregarDadosPlano();
                    load(proxy);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                Response.Redirect("~/BlocoVIII/FParecerConselhoMunicipal.aspx?msg=OK");
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }
    }
}