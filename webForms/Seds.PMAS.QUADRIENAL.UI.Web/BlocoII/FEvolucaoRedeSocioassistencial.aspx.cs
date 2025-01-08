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
    public partial class FEvolucaoRedeSocioassistencial : System.Web.UI.Page
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
                //carregarCombos();
                verificarAlteracoes();
                txtCaracterizacao.ReadOnly = true;
                btnSalvarCaracterizacao.Enabled = false;

                #region Bloqueia , Desbloqueia e ordena Controles
                WebControl[] controles = { txtCaracterizacao
                                             , btnSalvarCaracterizacao 
                                         };
                Permissao.VerificarPermissaoControles(controles, Session);
                #endregion
            }
        }

        void carregarIndicadores()
        {
            ConsultaMunicipioRedeSocioAssistencialIndicadoresInfo ind;
            NumberFormatInfo nfi_nodigit = new CultureInfo("pt-BR", false).NumberFormat;
            nfi_nodigit.NumberDecimalDigits = 0;
            ind = new Seds.PMAS.QUADRIENAL.Negocio.AnaliseDiagnostica().GetIndicadoresRedeSocioAssistencial(SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio, Convert.ToInt32("2022"));
            if (ind == null)
                return;
            lblNumServicosBasica2018.Text = ind.NumeroServicosSociosAssisteciaisPSB2018.Equals(-1) ? "-" : ind.NumeroServicosSociosAssisteciaisPSB2018.Value.ToString("N", nfi_nodigit);
            lblNumServicosBasica2019.Text = ind.NumeroServicosSociosAssisteciaisPSB2019.Equals(-1) ? "-" : ind.NumeroServicosSociosAssisteciaisPSB2019.Value.ToString("N", nfi_nodigit);
            lblNumServicosBasica2020.Text = ind.NumeroServicosSociosAssisteciaisPSB2020.Equals(-1) ? "-" : ind.NumeroServicosSociosAssisteciaisPSB2020.Value.ToString("N", nfi_nodigit);
            lblNumServicosBasica2021.Text = ind.NumeroServicosSociosAssisteciaisPSB2021.Equals(-1) ? "-" : ind.NumeroServicosSociosAssisteciaisPSB2021.Value.ToString("N", nfi_nodigit);
            
            lblNumServicosMedia2018.Text = ind.NumeroServicosPSEMediaComplexidade2018.Equals(-1) ? "-" : ind.NumeroServicosPSEMediaComplexidade2018.Value.ToString("N", nfi_nodigit);
            lblNumServicosMedia2019.Text = ind.NumeroServicosPSEMediaComplexidade2019.Equals(-1) ? "-" : ind.NumeroServicosPSEMediaComplexidade2019.Value.ToString("N", nfi_nodigit);
            lblNumServicosMedia2020.Text = ind.NumeroServicosPSEMediaComplexidade2020.Equals(-1) ? "-" : ind.NumeroServicosPSEMediaComplexidade2020.Value.ToString("N", nfi_nodigit);
            lblNumServicosMedia2021.Text = ind.NumeroServicosPSEMediaComplexidade2021.Equals(-1) ? "-" : ind.NumeroServicosPSEMediaComplexidade2021.Value.ToString("N", nfi_nodigit);

            lblNumServicosAlta2018.Text = ind.NumeroServicoPSEAltaComplexidade2018.Equals(-1) ? "-" : ind.NumeroServicoPSEAltaComplexidade2018.Value.ToString("N", nfi_nodigit);
            lblNumServicosAlta2019.Text = ind.NumeroServicoPSEAltaComplexidade2019.Equals(-1) ? "-" : ind.NumeroServicoPSEAltaComplexidade2019.Value.ToString("N", nfi_nodigit);
            lblNumServicosAlta2020.Text = ind.NumeroServicoPSEAltaComplexidade2020.Equals(-1) ? "-" : ind.NumeroServicoPSEAltaComplexidade2020.Value.ToString("N", nfi_nodigit);
            lblNumServicosAlta2021.Text = ind.NumeroServicoPSEAltaComplexidade2021.Equals(-1) ? "-" : ind.NumeroServicoPSEAltaComplexidade2021.Value.ToString("N", nfi_nodigit);
            
            lblServicoNaoTipificados2018.Text = ind.NumeroServicoNaoTipificados2018.Equals(-1) ? "" : ind.NumeroServicoNaoTipificados2018.Value.ToString("N", nfi_nodigit);
            lblServicoNaoTipificados2019.Text = ind.NumeroServicoNaoTipificados2019.Equals(-1) ? "-" : ind.NumeroServicoNaoTipificados2019.Value.ToString("N", nfi_nodigit);
            lblServicoNaoTipificados2020.Text = ind.NumeroServicoNaoTipificados2020.Equals(-1) ? "-" : ind.NumeroServicoNaoTipificados2020.Value.ToString("N", nfi_nodigit);
            lblServicoNaoTipificados2021.Text = ind.NumeroServicoNaoTipificados2021.Equals(-1) ? "-" : ind.NumeroServicoNaoTipificados2021.Value.ToString("N", nfi_nodigit);
            //lblEnidadesCMAS2013.Text = ind.NumeroEntidadesSocioAssistenciaisCMAS2013.Equals(-1) ? "-" : ind.NumeroEntidadesSocioAssistenciaisCMAS2013.ToString();
            //lblEnidadesCMAS2014.Text = ind.NumeroEntidadesSocioAssistenciaisCMAS2014.Equals(-1) ? "-" : ind.NumeroEntidadesSocioAssistenciaisCMAS2014.ToString();
            //lblEnidadesCMAS2015.Text = ind.NumeroEntidadesSocioAssistenciaisCMAS2015.Equals(-1) ? "-" : ind.NumeroEntidadesSocioAssistenciaisCMAS2015.ToString();
            lblCRASImplantados2018.Text = ind.NumeroCRASImplantadosMunicipios2018.Equals(-1.0) ? "NC" : ind.NumeroCRASImplantadosMunicipios2018.Value.ToString("N", nfi_nodigit);
            lblCRASImplantados2019.Text = ind.NumeroCRASImplantadosMunicipios2019.Equals(-1.0) ? "NC" : ind.NumeroCRASImplantadosMunicipios2019.Value.ToString("N", nfi_nodigit);
            lblCRASImplantados2020.Text = ind.NumeroCRASImplantadosMunicipios2020.Equals(-1.0) ? "NC" : ind.NumeroCRASImplantadosMunicipios2020.Value.ToString("N", nfi_nodigit);
            lblCRASImplantados2021.Text = ind.NumeroCRASImplantadosMunicipios2021.Equals(-1.0) ? "NC" : ind.NumeroCRASImplantadosMunicipios2021.Value.ToString("N", nfi_nodigit);

            lblCREASImplantados2018.Text = ind.NumeroCREASImplantadosMunicipios2018.Equals(-1.0) ? "NC" : ind.NumeroCREASImplantadosMunicipios2018.ToString();
            lblCREASImplantados2019.Text = ind.NumeroCREASImplantadosMunicipios2019.Equals(-1.0) ? "NC" : ind.NumeroCREASImplantadosMunicipios2019.Value.ToString("N", nfi_nodigit);
            lblCREASImplantados2020.Text = ind.NumeroCREASImplantadosMunicipios2020.Equals(-1.0) ? "NC" : ind.NumeroCREASImplantadosMunicipios2020.Value.ToString("N", nfi_nodigit);
            lblCREASImplantados2021.Text = ind.NumeroCREASImplantadosMunicipios2021.Equals(-1.0) ? "NC" : ind.NumeroCREASImplantadosMunicipios2021.Value.ToString("N", nfi_nodigit);

            lblCentroPOPImplantados2018.Text = ind.NumeroCENTROPOPImplantadosMunicipios2018.Equals(-1) ? "-" : ind.NumeroCENTROPOPImplantadosMunicipios2018.Value.ToString("N", nfi_nodigit);
            lblCentroPOPImplantados2019.Text = ind.NumeroCENTROPOPImplantadosMunicipios2019.Equals(-1) ? "-" : ind.NumeroCENTROPOPImplantadosMunicipios2019.Value.ToString("N", nfi_nodigit);
            lblCentroPOPImplantados2020.Text = ind.NumeroCENTROPOPImplantadosMunicipios2020.Equals(-1) ? "-" : ind.NumeroCENTROPOPImplantadosMunicipios2020.Value.ToString("N", nfi_nodigit);
            lblCentroPOPImplantados2021.Text = ind.NumeroCENTROPOPImplantadosMunicipios2021.Equals(-1) ? "-" : ind.NumeroCENTROPOPImplantadosMunicipios2021.Value.ToString("N", nfi_nodigit);


            lblBPCIdosos2018.Text = ind.TotalBeneficiariosBPCIdoso2018.Equals(-1) ? "-" : ind.TotalBeneficiariosBPCIdoso2018.Value.ToString("N", nfi_nodigit);
            lblBPCIdosos2019.Text = ind.TotalBeneficiariosBPCIdoso2019.Equals(-1) ? "-" : ind.TotalBeneficiariosBPCIdoso2019.Value.ToString("N", nfi_nodigit);
            lblBPCIdosos2020.Text = ind.TotalBeneficiariosBPCIdoso2020.Equals(-1) ? "-" : ind.TotalBeneficiariosBPCIdoso2020.Value.ToString("N", nfi_nodigit);
            lblBPCIdosos2021.Text = ind.TotalBeneficiariosBPCIdoso2021.Equals(-1) ? "-" : ind.TotalBeneficiariosBPCIdoso2021.Value.ToString("N", nfi_nodigit);
            
            lblBPCDeficentes2018.Text = ind.TotalBeneficiariosBPCDeficientes2018.Equals(-1) ? "-" : ind.TotalBeneficiariosBPCDeficientes2018.Value.ToString("N", nfi_nodigit);
            lblBPCDeficentes2019.Text = ind.TotalBeneficiariosBPCDeficientes2019.Equals(-1) ? "-" : ind.TotalBeneficiariosBPCDeficientes2019.Value.ToString("N", nfi_nodigit);
            lblBPCDeficentes2020.Text = ind.TotalBeneficiariosBPCDeficientes2020.Equals(-1) ? "-" : ind.TotalBeneficiariosBPCDeficientes2020.Value.ToString("N", nfi_nodigit);
            lblBPCDeficentes2021.Text = ind.TotalBeneficiariosBPCDeficientes2021.Equals(-1) ? "-" : ind.TotalBeneficiariosBPCDeficientes2021.Value.ToString("N", nfi_nodigit);

        }
        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro3.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 93);
                    linkAlteracoesQuadro3.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("93"));
                    //linkAlteracoesQuadro15.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 15);
                    //linkAlteracoesQuadro15.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("15"));
                }
            }
        }

        void loadCaracterizacao()
        {
            using (var proxy = new ProxyPrefeitura())
            {
                var pre = proxy.Service.GetPrefeituraById(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                if (pre == null)
                    return;
                txtCaracterizacao.Text = pre.CaracterizacaoRedeSocioassistencial;
            }

        }

        protected void btnSalvarCaracterizacao_Click(object sender, EventArgs e)
        {
            var msg = String.Empty;
            
            if (String.IsNullOrEmpty(txtCaracterizacao.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("O preenchimento do campo de caracterização do município em relação à evolução da rede de atendimento é obrigatório!"), true);
                return;
            }
            string regexCaracterizacao = Regex.Replace(txtCaracterizacao.Text.Trim(), @"\n", "");

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
                    pre.CaracterizacaoRedeSocioassistencial = txtCaracterizacao.Text;
                    new ValidadorPrefeitura().ValidarCaracterizacaoRedeSocioassistencial(pre);
                    blocoII.UpdatePrefeitura(pre, false);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Caracterização da Evolução da rede de atendimento registrada com sucesso!";
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                //lblInconsistencias.Text = String.Empty;
                //tbInconsistencias.Visible = false;
                return;
            }
            else
            {
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }

        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }
    }
}