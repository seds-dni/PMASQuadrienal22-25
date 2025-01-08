using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Genericos;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Microsoft.IdentityModel.Claims;
using System.Threading;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class VHistorico : System.Web.UI.Page
    {

        #region propriedades
        private static List<int> VHistoricoExercicios = new List<int>() { 2022, 2023, 2024, 2025 }; 
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                load();
            }
        }

        void load()
        {
            int exercicio1 = VHistorico.VHistoricoExercicios[0];
            int exercicio2 = VHistorico.VHistoricoExercicios[1];
            int exercicio3 = VHistorico.VHistoricoExercicios[2];
            int exercicio4 = VHistorico.VHistoricoExercicios[3];

            using (var proxy = new ProxyPlanoMunicipal())
            {
                //btnDesbloquear.Enabled = false;
                var obj = proxy.Service.GetHistoricoPlanoMunicipalById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
                if (obj == null)
                    return;
                lblData.Text = obj.Data.ToString("dd/MM/yyyy HH:mm");


                lblSituacao.Text = obj.Situacao;
                lblResponsavel.Text = obj.Usuario;
                int idSituacaoPrefeitura;
                bool reprogramado;
                using (var proxyPrefeitura = new ProxyPrefeitura())
                {
                    var prefeitura = proxyPrefeitura.Service.GetPrefeituraById(obj.IdPrefeitura);
                    idSituacaoPrefeitura = prefeitura.IdSituacao;
                    reprogramado = prefeitura.ValoresReprogramadosDrads.HasValue ? prefeitura.ValoresReprogramadosDrads.Value : false;
                }

                if (obj.IdSituacao == 4)
                {
                    trParecerDrads.Visible = true;
                    lblParecerDrads.Text = "Considerando as informações registradas no PMASweb 2022-2025 sobre a estrutura organizacional, o diagnóstico socioterritorial e os recursos previstos para cofinanciamento dos serviços da rede socioassistencial, esta DRADS é favorável ao repasse de recursos estaduais pelo Sistema Fundo a Fundo, conforme os valores aqui apontados.";
                    lblTituloTipoParecer.Text = "Outras considerações:";
                    //if (idSituacaoPrefeitura == 8)
                    //{
                    //    trDesbloqueiovalores.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                    //    btnDesbloquear.Enabled = true;
                    //}
                }
                else
                {
                    lblTituloTipoParecer.Text = "Descrição/Motivo";
                }
                lblDescricao.Text = obj.Descricao;

                trValoresCofinanciamento.Visible = obj.IdSituacao == (int)ESituacao.Parafinalizacao;
                trValoresCofinanciamentoExercicio2.Visible = obj.IdSituacao == (int)ESituacao.Parafinalizacao;

                if (trValoresCofinanciamento.Visible)
                {
                    var historicoCompleto = proxy.Service.GetHistoricoPlanoMunicipalFullById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));

                    if (historicoCompleto.PlanosMunicipaisHistoricoConsolidados != null)
                    {
                        #region Exercicio 1 
                        var historicoValoresExercicio1 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == exercicio1).SingleOrDefault();

                        if (historicoValoresExercicio1 != null)
                        {
                            trValoresCofinanciamento.Visible = true;
                            lblProtecaoSocialBasica.Text = historicoValoresExercicio1.ValorProtecaoSocialBasica.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecial.Text = historicoValoresExercicio1.ValorProtecaoSocialMediaComplexidade.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistida.Text = historicoValoresExercicio1.ValorProtecaoSocialAltaComplexidade.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREAS.Text = historicoValoresExercicio1.ValorBeneficiosEventuais.HasValue
                                ? historicoValoresExercicio1.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidario.Text = historicoValoresExercicio1.ValorProgramaProjetoSolidario.HasValue
                                ? historicoValoresExercicio1.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaReprogramado.Text = historicoValoresExercicio1.ValorProtecaoSocialBasicaReprogramado.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialReprogramado.Text = historicoValoresExercicio1.ValorProtecaoSocialMediaReprogramado.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaReprogramado.Text = historicoValoresExercicio1.ValorProtecaoSocialAltaReprogramado.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASReprogramado.Text = historicoValoresExercicio1.ValorBeneficioEventuaisReprogramado.HasValue
                                ? historicoValoresExercicio1.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioReprogramado.Text = historicoValoresExercicio1.ValorProgramaProjetoReprogramado.HasValue
                                ? historicoValoresExercicio1.ValorProgramaProjetoReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialBasicaDemandas.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialMediaDemandas.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialAltaDemandas.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASDemandas.Text = historicoValoresExercicio1.ValorBeneficioEventuaisDemandas.HasValue
                                ? historicoValoresExercicio1.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioDemandas.Text = historicoValoresExercicio1.ValorProgramaProjetoDemandas.HasValue
                                ? historicoValoresExercicio1.ValorProgramaProjetoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaReprogramadoDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialReprogramadoDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaReprogramadoDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASReprogramadoDemandas.Text = historicoValoresExercicio1.ValorBeneficioEventuaisReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio1.ValorBeneficioEventuaisReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioReprogramadoDemandas.Text = historicoValoresExercicio1.ValorProgramaProjetoReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio1.ValorProgramaProjetoReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");



                            lblTotalProtecaoSocialBasica.Text = (Convert.ToDecimal(lblProtecaoSocialBasica.Text)
                                                                 + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramado.Text) + Convert.ToDecimal(lblProtecaoSocialBasicaDemandas.Text) + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandas.Text)).ToString("N2");

                            lblTotalProtecaoSocialEspecial.Text = (Convert.ToDecimal(lblProtecaoSocialEspecial.Text)
                                                                 + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramado.Text) + Convert.ToDecimal(lblProtecaoSocialEspecialDemandas.Text) + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoDemandas.Text)).ToString("N2");
                            
                            lblTotalLiberdadeAssistida.Text = (Convert.ToDecimal(lblLiberdadeAssistida.Text)
                                                                 + Convert.ToDecimal(lblLiberdadeAssistidaReprogramado.Text) + Convert.ToDecimal(lblLiberdadeAssistidaDemandas.Text) + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandas.Text)).ToString("N2");
                            
                            lblTotalCREAS.Text = (Convert.ToDecimal(lblCREAS.Text)
                                                + Convert.ToDecimal(lblCREASReprogramado.Text) + Convert.ToDecimal(lblCREASDemandas.Text)).ToString("N2");
                            
                            lblTotalSaoPauloSolidario.Text = (Convert.ToDecimal(lblSaoPauloSolidario.Text)
                                                             + Convert.ToDecimal(lblSaoPauloSolidarioReprogramado.Text) + Convert.ToDecimal(lblSaoPauloSolidarioDemandas.Text)).ToString("N2");

                            lblTotalCofinanciamento.Text = (Convert.ToDecimal(lblProtecaoSocialBasica.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecial.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistida.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidario.Text)
                                                    + Convert.ToDecimal(lblCREAS.Text)).ToString("N2");


                            lblTotalReprogramacao.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaReprogramado.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramado.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramado.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramado.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramado.Text)).ToString("N2");

                            lblTotalDemandas.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaDemandas.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialDemandas.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaDemandas.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioDemandas.Text)
                                                    + Convert.ToDecimal(lblCREASDemandas.Text)).ToString("N2");

                            lblTotalReprogramadoDemandas.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandas.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoDemandas.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandas.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoDemandas.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoDemandas.Text)).ToString("N2");


                            lblTotalRecursos.Text = (Convert.ToDecimal(lblProtecaoSocialBasica.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecial.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistida.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidario.Text)
                                                    + Convert.ToDecimal(lblCREAS.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramado.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramado.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramado.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramado.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramado.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaDemandas.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialDemandas.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaDemandas.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioDemandas.Text)
                                                    + Convert.ToDecimal(lblCREASDemandas.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandas.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoDemandas.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandas.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoDemandas.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoDemandas.Text)                                                         
                                                    ).ToString("N2");
                        }
                        else
                        {
                            trValoresCofinanciamento.Visible = false;
                        }

                        #endregion

                        #region Exercicio 2
                        var historicoValoresExercicio2 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == exercicio2).SingleOrDefault();

                        if (historicoValoresExercicio2 != null)
                        {
                            trValoresCofinanciamentoExercicio2.Visible = true;

                            lblProtecaoSocialBasicaExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialBasica.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialMediaComplexidade.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialAltaComplexidade.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASExercicio2.Text = historicoValoresExercicio2.ValorBeneficiosEventuais.HasValue
                                ? historicoValoresExercicio2.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioExercicio2.Text = historicoValoresExercicio2.ValorProgramaProjetoSolidario.HasValue
                                ? historicoValoresExercicio2.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialBasicaReprogramado.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialMediaReprogramado.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialAltaReprogramado.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorBeneficioEventuaisReprogramado.HasValue
                                ? historicoValoresExercicio2.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorProgramaProjetoReprogramado.HasValue
                                ? historicoValoresExercicio2.ValorProgramaProjetoReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialBasicaDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialMediaDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialAltaDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASDemandasExercicio2.Text = historicoValoresExercicio2.ValorBeneficioEventuaisDemandas.HasValue
                                ? historicoValoresExercicio2.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioDemandasExercicio2.Text = historicoValoresExercicio2.ValorProgramaProjetoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProgramaProjetoDemandas.Value.ToString("N2") : (0M).ToString("N2");


                            lblProtecaoSocialBasicaReprogramadoDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialReprogramadoDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaReprogramadoDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASReprogramadoDemandasExercicio2.Text = historicoValoresExercicio2.ValorBeneficioEventuaisReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorBeneficioEventuaisReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioReprogramadoDemandasExercicio2.Text = historicoValoresExercicio2.ValorProgramaProjetoReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProgramaProjetoReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");



                            lblTotalProtecaoSocialBasicaExercicio2.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaExercicio2.Text)
                                                                 + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoExercicio2.Text) + Convert.ToDecimal(lblProtecaoSocialBasicaDemandasExercicio2.Text) + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandasExercicio2.Text)).ToString("N2");
                            lblTotalProtecaoSocialEspecialExercicio2.Text = (Convert.ToDecimal(lblProtecaoSocialEspecialExercicio2.Text)
                                                                 + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoExercicio2.Text) + Convert.ToDecimal(lblProtecaoSocialEspecialDemandasExercicio2.Text) + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoDemandasExercicio2.Text)).ToString("N2");
                            lblTotalLiberdadeAssistidaExercicio2.Text = (Convert.ToDecimal(lblLiberdadeAssistidaExercicio2.Text)
                                                                 + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoExercicio2.Text) + Convert.ToDecimal(lblLiberdadeAssistidaDemandasExercicio2.Text) + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandasExercicio2.Text)).ToString("N2");

                            lblTotalCREASExercicio2.Text = (Convert.ToDecimal(lblCREASExercicio2.Text)
                                                + Convert.ToDecimal(lblCREASReprogramadoExercicio2.Text) + Convert.ToDecimal(lblCREASDemandasExercicio2.Text) + Convert.ToDecimal(lblCREASReprogramadoDemandasExercicio2.Text)).ToString("N2");

                            lblTotalSaoPauloSolidarioExercicio2.Text = (Convert.ToDecimal(lblSaoPauloSolidarioExercicio2.Text)
                                                             + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoExercicio2.Text) + Convert.ToDecimal(lblSaoPauloSolidarioDemandasExercicio2.Text) + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoDemandasExercicio2.Text)).ToString("N2");

                            lblTotalCofinanciamentoExercicio2.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialExercicio2.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaExercicio2.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioExercicio2.Text)
                                                    + Convert.ToDecimal(lblCREASExercicio2.Text)).ToString("N2");


                            lblTotalReprogramacaoExercicio2.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoExercicio2.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoExercicio2.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoExercicio2.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoExercicio2.Text)).ToString("N2");

                            lblTotalDemandasExercicio2.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblCREASDemandasExercicio2.Text)).ToString("N2");

                            lblTotalReprogramadoDemandasExercicio2.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoDemandasExercicio2.Text)).ToString("N2");

                            lblTotalRecursosExercicio2.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialExercicio2.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaExercicio2.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioExercicio2.Text)
                                                    + Convert.ToDecimal(lblCREASExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoExercicio2.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoExercicio2.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoExercicio2.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblCREASDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoDemandasExercicio2.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoDemandasExercicio2.Text)                                                    
                                                    ).ToString("N2");
                        }
                        else {
                            trValoresCofinanciamentoExercicio2.Visible = false;
                        }
                        #endregion

                        #region Exercicio 3
                        var historicoValoresExercicio3 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == exercicio3).SingleOrDefault();

                        if (historicoValoresExercicio3 != null)
                        {
                            trValoresCofinanciamentoExercicio3.Visible = true;

                            lblProtecaoSocialBasicaExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialBasica.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialMediaComplexidade.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialAltaComplexidade.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASExercicio3.Text = historicoValoresExercicio3.ValorBeneficiosEventuais.HasValue
                                ? historicoValoresExercicio3.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioExercicio3.Text = historicoValoresExercicio3.ValorProgramaProjetoSolidario.HasValue
                                ? historicoValoresExercicio3.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialBasicaReprogramado.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialMediaReprogramado.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialAltaReprogramado.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorBeneficioEventuaisReprogramado.HasValue
                                ? historicoValoresExercicio3.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorProgramaProjetoReprogramado.HasValue
                                ? historicoValoresExercicio3.ValorProgramaProjetoReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialBasicaDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialMediaDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialAltaDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASDemandasExercicio3.Text = historicoValoresExercicio3.ValorBeneficioEventuaisDemandas.HasValue
                                ? historicoValoresExercicio3.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioDemandasExercicio3.Text = historicoValoresExercicio3.ValorProgramaProjetoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProgramaProjetoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaReprogramadoDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialReprogramadoDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaReprogramadoDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASReprogramadoDemandasExercicio3.Text = historicoValoresExercicio3.ValorBeneficioEventuaisReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorBeneficioEventuaisReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioReprogramadoDemandasExercicio3.Text = historicoValoresExercicio3.ValorProgramaProjetoReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProgramaProjetoReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblTotalProtecaoSocialBasicaExercicio3.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaExercicio3.Text)
                                                                 + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoExercicio3.Text) + Convert.ToDecimal(lblProtecaoSocialBasicaDemandasExercicio3.Text) + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandasExercicio3.Text)).ToString("N2");
                            lblTotalProtecaoSocialEspecialExercicio3.Text = (Convert.ToDecimal(lblProtecaoSocialEspecialExercicio3.Text)
                                                                 + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoExercicio3.Text) + Convert.ToDecimal(lblProtecaoSocialEspecialDemandasExercicio3.Text) + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoDemandasExercicio3.Text)).ToString("N2");
                            lblTotalLiberdadeAssistidaExercicio3.Text = (Convert.ToDecimal(lblLiberdadeAssistidaExercicio3.Text)
                                                                 + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoExercicio3.Text) + Convert.ToDecimal(lblLiberdadeAssistidaDemandasExercicio3.Text) + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandasExercicio3.Text)).ToString("N2");

                            lblTotalCREASExercicio3.Text = (Convert.ToDecimal(lblCREASExercicio3.Text)
                                                + Convert.ToDecimal(lblCREASReprogramadoExercicio3.Text) + Convert.ToDecimal(lblCREASDemandasExercicio3.Text) + Convert.ToDecimal(lblCREASReprogramadoDemandasExercicio3.Text)).ToString("N2");

                            lblTotalSaoPauloSolidarioExercicio3.Text = (Convert.ToDecimal(lblSaoPauloSolidarioExercicio3.Text)
                                                             + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoExercicio3.Text) + Convert.ToDecimal(lblSaoPauloSolidarioDemandasExercicio3.Text) + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoDemandasExercicio3.Text)).ToString("N2");

                            lblTotalCofinanciamentoExercicio3.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaExercicio3.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialExercicio3.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaExercicio3.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioExercicio3.Text)
                                                    + Convert.ToDecimal(lblCREASExercicio3.Text)).ToString("N2");

                            lblTotalDemandasExercicio3.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblCREASDemandasExercicio3.Text)).ToString("N2");


                            lblTotalReprogramadoDemandasExercicio3.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoDemandasExercicio3.Text)).ToString("N2");


                            lblTotalReprogramacaoExercicio3.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoExercicio3.Text)).ToString("N2");

                            lblTotalRecursosExercicio3.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaExercicio3.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialExercicio3.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaExercicio3.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioExercicio3.Text)
                                                    + Convert.ToDecimal(lblCREASExercicio3.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioDemandasExercicio3.Text)
                                                    + Convert.ToDecimal(lblCREASDemandasExercicio3.Text) 
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoExercicio3.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoExercicio3.Text)                                                   
                                                    ).ToString("N2");
                        }
                        else
                        {
                            trValoresCofinanciamentoExercicio3.Visible = false;
                        }
                        #endregion

                        #region Exercicio 4
                        var historicoValoresExercicio4 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == exercicio4).SingleOrDefault();

                        if (historicoValoresExercicio4 != null)
                        {
                            trValoresCofinanciamentoExercicio4.Visible = true;

                            lblProtecaoSocialBasicaExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialBasica.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialMediaComplexidade.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialAltaComplexidade.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASExercicio4.Text = historicoValoresExercicio4.ValorBeneficiosEventuais.HasValue
                                ? historicoValoresExercicio4.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioExercicio4.Text = historicoValoresExercicio4.ValorProgramaProjetoSolidario.HasValue
                                ? historicoValoresExercicio4.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialBasicaReprogramado.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialMediaReprogramado.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialAltaReprogramado.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorBeneficioEventuaisReprogramado.HasValue
                                ? historicoValoresExercicio4.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorProgramaProjetoReprogramado.HasValue
                                ? historicoValoresExercicio4.ValorProgramaProjetoReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialBasicaDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialMediaDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialAltaDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASDemandasExercicio4.Text = historicoValoresExercicio4.ValorBeneficioEventuaisDemandas.HasValue
                                ? historicoValoresExercicio4.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioDemandasExercicio4.Text = historicoValoresExercicio4.ValorProgramaProjetoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProgramaProjetoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialBasicaReprogramadoDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblProtecaoSocialEspecialReprogramadoDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblLiberdadeAssistidaReprogramadoDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblCREASReprogramadoDemandasExercicio4.Text = historicoValoresExercicio4.ValorBeneficioEventuaisReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorBeneficioEventuaisReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblSaoPauloSolidarioReprogramadoDemandasExercicio4.Text = historicoValoresExercicio4.ValorProgramaProjetoReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProgramaProjetoReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            lblTotalProtecaoSocialBasicaExercicio4.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaExercicio4.Text)
                                                                 + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoExercicio4.Text) + Convert.ToDecimal(lblProtecaoSocialBasicaDemandasExercicio4.Text) + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandasExercicio4.Text)).ToString("N2");
                            lblTotalProtecaoSocialEspecialExercicio4.Text = (Convert.ToDecimal(lblProtecaoSocialEspecialExercicio4.Text)
                                                                 + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoExercicio4.Text) + Convert.ToDecimal(lblProtecaoSocialEspecialDemandasExercicio4.Text) + Convert.ToDecimal(lblProtecaoSocialEspecialDemandasExercicio4.Text)).ToString("N2");
                            lblTotalLiberdadeAssistidaExercicio4.Text = (Convert.ToDecimal(lblLiberdadeAssistidaExercicio4.Text)
                                                                 + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoExercicio4.Text) + Convert.ToDecimal(lblLiberdadeAssistidaDemandasExercicio4.Text) + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandasExercicio4.Text)).ToString("N2");
                            lblTotalCREASExercicio4.Text = (Convert.ToDecimal(lblCREASExercicio4.Text)
                                                + Convert.ToDecimal(lblCREASReprogramadoExercicio4.Text) + Convert.ToDecimal(lblCREASDemandasExercicio4.Text) + Convert.ToDecimal(lblCREASReprogramadoDemandasExercicio4.Text)).ToString("N2");
                            lblTotalSaoPauloSolidarioExercicio4.Text = (Convert.ToDecimal(lblSaoPauloSolidarioExercicio4.Text)
                                                             + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoExercicio4.Text) + Convert.ToDecimal(lblSaoPauloSolidarioDemandasExercicio4.Text) + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoDemandasExercicio4.Text)).ToString("N2");


                            lblTotalCofinanciamentoExercicio4.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialExercicio4.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaExercicio4.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioExercicio4.Text)
                                                    + Convert.ToDecimal(lblCREASExercicio4.Text)).ToString("N2");


                            lblTotalReprogramacaoExercicio4.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoExercicio4.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoExercicio4.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoExercicio4.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoExercicio4.Text)).ToString("N2");

                            lblTotalDemandasExercicio4.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblCREASDemandasExercicio4.Text)).ToString("N2");

                            lblTotalReprogramadoDemandasExercicio4.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoDemandasExercicio4.Text)).ToString("N2");

                            lblTotalRecursosExercicio4.Text = (Convert.ToDecimal(lblProtecaoSocialBasicaExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialExercicio4.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaExercicio4.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioExercicio4.Text)
                                                    + Convert.ToDecimal(lblCREASExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoExercicio4.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoExercicio4.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoExercicio4.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblCREASDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialBasicaReprogramadoDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblProtecaoSocialEspecialReprogramadoDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblLiberdadeAssistidaReprogramadoDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblSaoPauloSolidarioReprogramadoDemandasExercicio4.Text)
                                                    + Convert.ToDecimal(lblCREASReprogramadoDemandasExercicio4.Text)                                                    
                                                    ).ToString("N2");
                        }
                        else
                        {
                            trValoresCofinanciamentoExercicio4.Visible = false;
                        }
                        #endregion
                    }
                }
            }
        }

        protected void btnDesbloquear_Click(object sender, EventArgs e)
        {
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var quadro = proxy.Service.GetPrefeituraById(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    if (quadro != null)
                    {
                        quadro.DesbloquearValoresDrads = true;
                        proxy.Service.UpdatePrefeitura(quadro);
                    }
                }


                var script = Util.GetJavaScriptDialogOK("Desbloqueio do Parecer da Drads realizado com sucesso");
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}