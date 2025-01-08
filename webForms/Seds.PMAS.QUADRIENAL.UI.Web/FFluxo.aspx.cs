using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Microsoft.IdentityModel.Claims;
using System.Threading;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Negocio;
using Seds.PMAS.QUADRIENAL.Pendencia;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{

    public partial class FFluxo : System.Web.UI.Page
    {
        #region propriedades
        private static List<int> Exercicios = new List<int> { 2022, 2023, 2024, 2025 };
        public bool ExercicioLiberado2022 { get; set; }
        public bool ExercicioLiberado2023 { get; set; }
        public bool ExercicioLiberado2024 { get; set; }
        public bool ExercicioLiberado2025 { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (String.IsNullOrEmpty(Request.QueryString["idPrefeitura"]) || String.IsNullOrEmpty(Request.QueryString["idSituacao"]))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                AdicionarEventosExercicio1();
                AdicionarEventosExercicio2();
                AdicionarEventosExercicio3();
                AdicionarEventosExercicio4();
                load();
            }
        }

        void load()
        {
            int exercicio1 = FFluxo.Exercicios[0];
            int exercicio2 = FFluxo.Exercicios[1];
            int exercicio3 = FFluxo.Exercicios[2];
            int exercicio4 = FFluxo.Exercicios[3];

            var situacaoAtual = 0;
            var situacao = (ESituacao)Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idSituacao"]));
            using (var proxy = new ProxyPrefeitura())
            {
                #region prefeitura
                int idPrefeitura = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idPrefeitura"]));
                var prefeitura = proxy.Service.GetPrefeituraById(idPrefeitura);
                lblMunicipio.Text = ProxyDivisaoAdministrativa.MunicipiosEstaduais.First(m => m.Id == prefeitura.IdMunicipio).Nome;
                lblSituacaoAtual.Text = prefeitura.Situacao.Nome;
                #endregion prefeitura

                if (prefeitura != null)
                {
                    var cofinancimentosExercicio1 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 4, exercicio1);
                    var cofinancimentosExercicio2 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 4, exercicio2);
                    var cofinancimentosExercicio3 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 4, exercicio3);
                    var cofinancimentosExercicio4 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 4, exercicio4);
                    

                    using (var proxyDrads = new ProxyDradsPlanoMunicipal())
                    {
                        #region Buscar
                        //cofinanciamento
                        var cofinanciamentosExercicio1 = proxyDrads.Service.GetResumoCofinanciamentoDradsBy(idPrefeitura, exercicio1);
                        var cofinanciamentosExercicio2 = proxyDrads.Service.GetResumoCofinanciamentoDradsBy(idPrefeitura, exercicio2);
                        var cofinanciamentosExercicio3 = proxyDrads.Service.GetResumoCofinanciamentoDradsBy(idPrefeitura, exercicio3);
                        var cofinanciamentosExercicio4 = proxyDrads.Service.GetResumoCofinanciamentoDradsBy(idPrefeitura, exercicio4);
                        
                        //reprogramacao
                        var cofinanciamentosReprogramadoExercicio1 = proxyDrads.Service.GetResumoCofinanciamentoReprogramadoDradsBy(idPrefeitura, exercicio1);
                        var cofinanciamentosReprogramadoExercicio2 = proxyDrads.Service.GetResumoCofinanciamentoReprogramadoDradsBy(idPrefeitura, exercicio2);
                        var cofinanciamentosReprogramadoExercicio3 = proxyDrads.Service.GetResumoCofinanciamentoReprogramadoDradsBy(idPrefeitura, exercicio3);
                        var cofinanciamentosReprogramadoExercicio4 = proxyDrads.Service.GetResumoCofinanciamentoReprogramadoDradsBy(idPrefeitura, exercicio4);

                        //Demandas
                        var cofinanciamentosDemandasExercicio1 = proxyDrads.Service.GetResumoCofinanciamentoDemandasDradsBy(idPrefeitura, exercicio1);
                        var cofinanciamentosDemandasExercicio2 = proxyDrads.Service.GetResumoCofinanciamentoDemandasDradsBy(idPrefeitura, exercicio2);
                        var cofinanciamentosDemandasExercicio3 = proxyDrads.Service.GetResumoCofinanciamentoDemandasDradsBy(idPrefeitura, exercicio3);
                        var cofinanciamentosDemandasExercicio4 = proxyDrads.Service.GetResumoCofinanciamentoDemandasDradsBy(idPrefeitura, exercicio4);

                        //Demandas Reprogramacao
                        var cofinanciamentosReprogramacaoDemandasExercicio1 = proxyDrads.Service.GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(idPrefeitura, exercicio1);
                        var cofinanciamentosReprogramacaoDemandasExercicio2 = proxyDrads.Service.GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(idPrefeitura, exercicio2);
                        var cofinanciamentosReprogramacaoDemandasExercicio3 = proxyDrads.Service.GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(idPrefeitura, exercicio3);
                        var cofinanciamentosReprogramacaoDemandasExercicio4 = proxyDrads.Service.GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(idPrefeitura, exercicio4);

                        //programa e beneficio
                        var cofinanciamentosBeneficioProgramaExercicio1 = proxyDrads.Service.GetResumoCofinanciamentoBeneficioProgramaDradsBy(idPrefeitura, exercicio1);
                        var cofinanciamentosBeneficioProgramaExercicio2 = proxyDrads.Service.GetResumoCofinanciamentoBeneficioProgramaDradsBy(idPrefeitura, exercicio2);
                        var cofinanciamentosBeneficioProgramaExercicio3 = proxyDrads.Service.GetResumoCofinanciamentoBeneficioProgramaDradsBy(idPrefeitura, exercicio3);
                        var cofinanciamentosBeneficioProgramaExercicio4 = proxyDrads.Service.GetResumoCofinanciamentoBeneficioProgramaDradsBy(idPrefeitura, exercicio4);


                        decimal cofExercicio1 = 0;
                        decimal cofExercicio2 = 0;
                        decimal cofExercicio3 = 0;
                        decimal cofExercicio4 = 0;

                        decimal cofReprogramadoExercicio1 = 0;
                        decimal cofReprogramadoExercicio2 = 0;
                        decimal cofReprogramadoExercicio3 = 0;
                        decimal cofReprogramadoExercicio4 = 0;

                        decimal cofDemandasExercicio1 = 0;
                        decimal cofDemandasExercicio2 = 0;
                        decimal cofDemandasExercicio3 = 0;
                        decimal cofDemandasExercicio4 = 0;

                        decimal cofReprogramacaoDemandasExercicio1 = 0;
                        decimal cofReprogramacaoDemandasExercicio2 = 0;
                        decimal cofReprogramacaoDemandasExercicio3 = 0;
                        decimal cofReprogramacaoDemandasExercicio4 = 0;

                        #endregion

                        this.CarregarDoHistorico(prefeitura);

                        if (cofinanciamentosExercicio1 != null && cofinanciamentosBeneficioProgramaExercicio1 != null)
                        {
                             cofExercicio1 = SomatoriaCofinanciamentosExercicio1(cofinanciamentosExercicio1, cofinanciamentosBeneficioProgramaExercicio1, cofinancimentosExercicio1.Sum(x => x.PrevisaoOrcamentaria));    
                        }

                        if (cofinanciamentosExercicio2 != null && cofinanciamentosBeneficioProgramaExercicio2 != null)
                        {
                            cofExercicio2 = SomatoriaCofinanciamentosExercicio2(cofinanciamentosExercicio2, cofinanciamentosBeneficioProgramaExercicio2, cofinancimentosExercicio2.Sum(x => x.PrevisaoOrcamentaria));    
                        }

                        if (cofinanciamentosExercicio3 != null && cofinanciamentosBeneficioProgramaExercicio3 != null)
                        {
                            cofExercicio3 = SomatoriaCofinanciamentosExercicio3(cofinanciamentosExercicio3, cofinanciamentosBeneficioProgramaExercicio3, cofinancimentosExercicio3.Sum(x => x.PrevisaoOrcamentaria));    
                        }

                        if (cofinanciamentosExercicio4 != null && cofinanciamentosBeneficioProgramaExercicio4 != null)
                        {
                            cofExercicio4 = SomatoriaCofinanciamentosExercicio4(cofinanciamentosExercicio4, cofinanciamentosBeneficioProgramaExercicio4, cofinancimentosExercicio4.Sum(x => x.PrevisaoOrcamentaria));    
                        }


                        if (cofinanciamentosReprogramadoExercicio1 != null)
                        {
                            cofReprogramadoExercicio1 = SomatoriaCofinanciamentosReprogramacaoExercicio1(cofinanciamentosReprogramadoExercicio1);
                        }


                        if (cofinanciamentosReprogramadoExercicio2 != null)
                        {
                            cofReprogramadoExercicio2 = SomatoriaCofinanciamentosReprogramacaoExercicio2(cofinanciamentosReprogramadoExercicio2);
                        }


                        if (cofinanciamentosReprogramadoExercicio3 != null)
                        {
                            cofReprogramadoExercicio3 = SomatoriaCofinanciamentosReprogramacaoExercicio3(cofinanciamentosReprogramadoExercicio3);
                        }


                        if (cofinanciamentosReprogramadoExercicio4 != null)
                        {
                            cofReprogramadoExercicio4 = SomatoriaCofinanciamentosReprogramacaoExercicio4(cofinanciamentosReprogramadoExercicio4);
                        }



                        if (cofinanciamentosDemandasExercicio1 != null)
                        {
                            cofDemandasExercicio1 = SomatoriaCofinanciamentosDemandasExercicio1(cofinanciamentosDemandasExercicio1);
                        }


                        if (cofinanciamentosDemandasExercicio2 != null)
                        {
                            cofDemandasExercicio2 = SomatoriaCofinanciamentosDemandasExercicio2(cofinanciamentosDemandasExercicio2);
                        }


                        if (cofinanciamentosDemandasExercicio3 != null)
                        {
                            cofDemandasExercicio3 = SomatoriaCofinanciamentosDemandasExercicio3(cofinanciamentosDemandasExercicio3);
                        }


                        if (cofinanciamentosDemandasExercicio4 != null)
                        {
                            cofDemandasExercicio4 = SomatoriaCofinanciamentosDemandasExercicio4(cofinanciamentosDemandasExercicio4);
                        }


                        if (cofinanciamentosReprogramacaoDemandasExercicio1 != null)
                        {
                            cofReprogramacaoDemandasExercicio1 = SomatoriaCofinanciamentosReprogramacaoDemandasExercicio1(cofinanciamentosReprogramacaoDemandasExercicio1);
                        }

                        if (cofinanciamentosReprogramacaoDemandasExercicio2 != null)
                        {
                            cofReprogramacaoDemandasExercicio2 = SomatoriaCofinanciamentosReprogramacaoDemandasExercicio2(cofinanciamentosReprogramacaoDemandasExercicio2);
                        }

                        if (cofinanciamentosReprogramacaoDemandasExercicio3 != null)
                        {
                            cofReprogramacaoDemandasExercicio3 = SomatoriaCofinanciamentosReprogramacaoDemandasExercicio3(cofinanciamentosReprogramacaoDemandasExercicio3);
                        }

                        if (cofinanciamentosReprogramacaoDemandasExercicio4 != null)
                        {
                            cofReprogramacaoDemandasExercicio4 = SomatoriaCofinanciamentosReprogramacaoDemandasExercicio4(cofinanciamentosReprogramacaoDemandasExercicio4);
                        }

                        lblTotalCofinanciamentoEstadual.Text = (cofExercicio1 + cofReprogramadoExercicio1 + cofDemandasExercicio1 + cofReprogramacaoDemandasExercicio1 + 0).ToString("N2");
                        lblTotalCofinanciamentoEstadualExercicio2.Text = (cofExercicio2 + cofReprogramadoExercicio2 + cofDemandasExercicio2 + cofReprogramacaoDemandasExercicio2 + 0).ToString("N2");
                        lblTotalCofinanciamentoEstadualExercicio3.Text = (cofExercicio3 + cofReprogramadoExercicio3 + cofDemandasExercicio3 + cofReprogramacaoDemandasExercicio3 + 0).ToString("N2");
                        lblTotalCofinanciamentoEstadualExercicio4.Text = (cofExercicio4 + cofReprogramadoExercicio4 + cofDemandasExercicio4 + cofReprogramacaoDemandasExercicio4 + 0).ToString("N2");


                    }
                }

            }

            #region Rotulo Situacao

            if (situacao != (ESituacao)Convert.ToInt32(situacaoAtual))
            {
                switch (situacao)
                {
                    case ESituacao.DevolvidoDrads:
                    case ESituacao.DevolvidopeloCMAS:
                        {
                            lblSituacao.Text = "Devolver Plano para Órgão Gestor"; break;
                        }
                    case ESituacao.AutorizaDesbloqueioReprogramacao:
                        {
                            lblSituacao.Text = "Autorizar desbloqueio do Plano para Órgão Gestor para reprogramação de recursos estaduais";
                            txtDescricao.Enabled = true;
                            break;
                        }
                    case ESituacao.AutorizaDesbloqueioDemandas:
                        {
                            lblSituacao.Text = "Autorizar desbloqueio do Plano para Órgão Gestor para demandas parlamentares de recursos estaduais";
                            txtDescricao.Enabled = true;
                            break;
                        }
                    case ESituacao.AutorizaDesbloqueioGestor:
                        {
                            lblSituacao.Text = "Autorizar Desbloqueio do Plano para Órgão Gestor";
                            txtDescricao.Enabled = true;
                            break;
                        }
                    case ESituacao.AutorizaDesbloqueioCMAS:
                        {
                            lblSituacao.Text = "Autorizar Desbloqueio do Plano para CMAS";
                            txtDescricao.Enabled = true;
                            break;
                        }
                    case ESituacao.EmAnalisedoCMAS:
                        {
                            lblSituacao.Text = "Desbloquear Plano para CMAS";
                            txtDescricao.Enabled = true;
                            break;
                        }
                    case ESituacao.Desbloqueado:
                        {
                            lblSituacao.Text = "Desbloquear Plano para Órgão Gestor"; break;
                        }
                    case ESituacao.Aprovado:
                    case ESituacao.Rejeitado:
                        {
                            lblSituacao.Text = "Parecer sobre as alterações do Plano";
                            lblDescricao.Text = "Parecer:";
                            trAprovacao.Visible = true;
                            break;
                        }
                    case ESituacao.DevolverParaCas:
                        {
                            lblSituacao.Text = "Motivo da Devolução"; 
                            break;
                        }
                    case ESituacao.Parafinalizacao:
                        {
                            lblSituacao.Text = "Enviar plano para finalização";
                            lblDescricao.Text = "Comentários:";
                            lblParecerDrads.Visible = true;
                            lblParecerDrads.Text = "Considerando as informações registradas no PMASweb 2022-2025 sobre a estrutura organizacional, o diagnóstico socioterritorial e os recursos previstos para cofinanciamento dos serviços da rede socioassistencial, esta DRADS é favorável ao repasse de recursos estaduais pelo Sistema Fundo a Fundo, conforme os valores aqui apontados.";
                            //txtDescricao.ReadOnly = true;
                            btnSalvar.Text = "Enviar";
                            trRecursosCofinanciamento.Visible = true;
                            trRecursosCofinanciamentoParte2.Visible = true;
                            break;
                        }
                }
            #endregion

            }
            else
            {
                btnSalvar.Enabled = false;
            }

            this.AplicarBloqueioDesbloqueio();

        }



        #region somatorias

        private Decimal SomatoriaCofinanciamentosReprogramacaoExercicio1(DradsPlanoMunicipalRecursosReprogramadoInfo cofinanciamentosReprogramadoExercicio1)
        {
            Decimal total = ((cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialBasicaReprogramado.Value : 0M)
                        + (cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialMediaReprogramado.Value : 0M)
                        + (cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialAltaReprogramado.Value : 0M)
                        + (cofinanciamentosReprogramadoExercicio1.ValorBeneficioEventuaisReprogramado != null ? cofinanciamentosReprogramadoExercicio1.ValorBeneficioEventuaisReprogramado.Value : 0M));
            lblValorReprogramacao.Text = (total).ToString("N2");

            return total;
        }

        private Decimal SomatoriaCofinanciamentosReprogramacaoExercicio2(DradsPlanoMunicipalRecursosReprogramadoInfo cofinanciamentosReprogramadoExercicio2)
        {
            if (cofinanciamentosReprogramadoExercicio2 != null)
            {
                var total = ((cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialBasicaReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialMediaReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialAltaReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio2.ValorBeneficioEventuaisReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValorBeneficioEventuaisReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio2.ValoresProgramasEProjetosReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValoresProgramasEProjetosReprogramado.Value : 0M));

                lblValorReprogramacaoExercicio2.Text = (total).ToString("N2");

                return total;
            }
            else return 0M;

        }

        private Decimal SomatoriaCofinanciamentosReprogramacaoExercicio3(DradsPlanoMunicipalRecursosReprogramadoInfo cofinanciamentosReprogramadoExercicio3)
        {
            if (cofinanciamentosReprogramadoExercicio3 != null)
            {
                var total = ((cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialBasicaReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialMediaReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialAltaReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio3.ValorBeneficioEventuaisReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValorBeneficioEventuaisReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio3.ValoresProgramasEProjetosReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValoresProgramasEProjetosReprogramado.Value : 0M));
                lblValorReprogramacaoExercicio3.Text = (total).ToString("N2");

                return total;
            }
            else return 0M;

        }

        private Decimal SomatoriaCofinanciamentosReprogramacaoExercicio4(DradsPlanoMunicipalRecursosReprogramadoInfo cofinanciamentosReprogramadoExercicio4)
        {
            if (cofinanciamentosReprogramadoExercicio4 != null)
            {
                var total = ((cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialBasicaReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialMediaReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialAltaReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio4.ValorBeneficioEventuaisReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValorBeneficioEventuaisReprogramado.Value : 0M)
                            + (cofinanciamentosReprogramadoExercicio4.ValoresProgramasEProjetosReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValoresProgramasEProjetosReprogramado.Value : 0M));
                lblValorReprogramacaoExercicio4.Text = (total).ToString("N2");

                return total;
            }
            else return 0M;

        }


        private Decimal SomatoriaCofinanciamentosDemandasExercicio1(DradsPlanoMunicipalDemandasParlamentaresInfo cofinanciamentosDemandasExercicio1)
        {
            Decimal total = ((cofinanciamentosDemandasExercicio1.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialBasicaDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio1.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialMediaDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio1.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialAltaDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio1.ValorBeneficioEventuaisDemandas != null ?  cofinanciamentosDemandasExercicio1.ValorBeneficioEventuaisDemandas.Value : 0M)
                        );
            lblValorDemandas.Text = (total).ToString("N2");

            return total;
        }

        private Decimal SomatoriaCofinanciamentosDemandasExercicio2(DradsPlanoMunicipalDemandasParlamentaresInfo cofinanciamentosDemandasExercicio2)
        {
            if (cofinanciamentosDemandasExercicio2 != null)
            {
                var total = ((cofinanciamentosDemandasExercicio2.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialBasicaDemandas.Value : 0M)
                            + (cofinanciamentosDemandasExercicio2.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialMediaDemandas.Value : 0M)
                            + (cofinanciamentosDemandasExercicio2.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialAltaDemandas.Value : 0M)
                            + (cofinanciamentosDemandasExercicio2.ValorBeneficioEventuaisDemandas != null ? cofinanciamentosDemandasExercicio2.ValorBeneficioEventuaisDemandas.Value : 0M)
                            );
                lblValorDemandasExercicio2.Text = (total).ToString("N2");

                return total;
            }
            else return 0M;

        }

        private Decimal SomatoriaCofinanciamentosDemandasExercicio3(DradsPlanoMunicipalDemandasParlamentaresInfo cofinanciamentosDemandasExercicio3)
        {
            if (cofinanciamentosDemandasExercicio3 != null)
            {
                var total = ((cofinanciamentosDemandasExercicio3.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialBasicaDemandas.Value : 0M)
                            + (cofinanciamentosDemandasExercicio3.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialMediaDemandas.Value : 0M)
                            + (cofinanciamentosDemandasExercicio3.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialAltaDemandas.Value : 0M)
                            + (cofinanciamentosDemandasExercicio3.ValorBeneficioEventuaisDemandas != null ? cofinanciamentosDemandasExercicio3.ValorBeneficioEventuaisDemandas.Value : 0M)
                            );
                lblValorDemandasExercicio3.Text = (total).ToString("N2");

                return total;
            }
            else return 0M;

        }

        private Decimal SomatoriaCofinanciamentosDemandasExercicio4(DradsPlanoMunicipalDemandasParlamentaresInfo cofinanciamentosDemandasExercicio4)
        {
            if (cofinanciamentosDemandasExercicio4 != null)
            {
                var total = ((cofinanciamentosDemandasExercicio4.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialBasicaDemandas.Value : 0M)
                            + (cofinanciamentosDemandasExercicio4.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialMediaDemandas.Value : 0M)
                            + (cofinanciamentosDemandasExercicio4.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialAltaDemandas.Value : 0M)
                            + (cofinanciamentosDemandasExercicio4.ValorBeneficioEventuaisDemandas != null ? cofinanciamentosDemandasExercicio4.ValorBeneficioEventuaisDemandas.Value : 0M));
                lblValorDemandasExercicio4.Text = (total).ToString("N2");

                return total;
            }
            else return 0M;

        }

        private Decimal SomatoriaCofinanciamentosReprogramacaoDemandasExercicio1(DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo cofinanciamentosDemandasExercicio1)
        {
            Decimal total = ((cofinanciamentosDemandasExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M));
            lblValorReprogramacaoDemandas.Text = (total).ToString("N2");

            return total;
        }

        private Decimal SomatoriaCofinanciamentosReprogramacaoDemandasExercicio2(DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo cofinanciamentosDemandasExercicio2)
        {
            Decimal total = ((cofinanciamentosDemandasExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio2.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio2.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M));
            lblValorReprogramacaoDemandasExercicio2.Text = (total).ToString("N2");

            return total;
        }

        private Decimal SomatoriaCofinanciamentosReprogramacaoDemandasExercicio3(DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo cofinanciamentosDemandasExercicio3)
        {
            Decimal total = ((cofinanciamentosDemandasExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio3.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio3.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M));
            lblValorReprogramadoDemandasExercicio3.Text = (total).ToString("N2");

            return total;
        }

        private Decimal SomatoriaCofinanciamentosReprogramacaoDemandasExercicio4(DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo cofinanciamentosDemandasExercicio4)
        {
            Decimal total = ((cofinanciamentosDemandasExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M)
                        + (cofinanciamentosDemandasExercicio4.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosDemandasExercicio4.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M));
            lblValorReprogramadoDemandasExercicio4.Text = (total).ToString("N2");

            return total;
        }


        private Decimal SomatoriaCofinanciamentosExercicio1(DradsPlanoMunicipalRecursosInfo cofinanciamentosExercicio1, DradsPlanoMunicipalBeneficioProgramaRecursosInfo cofinanciamentosBeneficioProgramaExercicio1, Decimal previsaoCofinanciamentoEstadualExercicio1)
        {
            //var total = ((cofinanciamentosExercicio1.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio1.ValorProtecaoSocialBasica.Value : 0M)
            //                            + (cofinanciamentosExercicio1.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio1.ValorProtecaoSocialMediaComplexidade.Value : 0M)
            //                            + (cofinanciamentosExercicio1.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio1.ValorProtecaoSocialAltaComplexidade.Value : 0M)
            //                            + (cofinanciamentosBeneficioProgramaExercicio1.ValorBeneficiosEventuais != null ? cofinanciamentosBeneficioProgramaExercicio1.ValorBeneficiosEventuais.Value : 0M)
            //                            + (cofinanciamentosBeneficioProgramaExercicio1.ValorProgramaProjeto != null ? cofinanciamentosBeneficioProgramaExercicio1.ValorProgramaProjeto.Value : 0M));

            var total = ((cofinanciamentosExercicio1.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio1.ValorProtecaoSocialBasica.Value : 0M)
                                        + (cofinanciamentosExercicio1.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio1.ValorProtecaoSocialMediaComplexidade.Value : 0M)
                                        + (cofinanciamentosExercicio1.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio1.ValorProtecaoSocialAltaComplexidade.Value : 0M)
                                        + (cofinanciamentosBeneficioProgramaExercicio1.ValorBeneficiosEventuais != null ? cofinanciamentosBeneficioProgramaExercicio1.ValorBeneficiosEventuais.Value : 0M)
                                        //+ (cofinanciamentosBeneficioProgramaExercicio1.ValorProgramaProjeto != null ? cofinanciamentosBeneficioProgramaExercicio1.ValorProgramaProjeto.Value : 0M)
                                        + previsaoCofinanciamentoEstadualExercicio1
                                        );

            lblValorCofinanciamento.Text = (total).ToString("N2");

            return total;
        }

        private Decimal SomatoriaCofinanciamentosExercicio2(DradsPlanoMunicipalRecursosInfo cofinanciamentosExercicio2, DradsPlanoMunicipalBeneficioProgramaRecursosInfo cofinanciamentosBeneficioProgramaExercicio2, Decimal previsaoCofinanciamentoEstadualExercicio2)
        {
            if (cofinanciamentosExercicio2 != null)
            {
                //var total = ((cofinanciamentosExercicio2.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio2.ValorProtecaoSocialBasica.Value : 0M)
                //                           + (cofinanciamentosExercicio2.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio2.ValorProtecaoSocialMediaComplexidade.Value : 0M)
                //                           + (cofinanciamentosExercicio2.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio2.ValorProtecaoSocialAltaComplexidade.Value : 0M)
                //                           + (cofinanciamentosBeneficioProgramaExercicio2.ValorBeneficiosEventuais != null ? cofinanciamentosBeneficioProgramaExercicio2.ValorBeneficiosEventuais.Value : 0M)
                //                           + (cofinanciamentosBeneficioProgramaExercicio2.ValorProgramaProjeto != null ? cofinanciamentosBeneficioProgramaExercicio2.ValorProgramaProjeto.Value : 0M));

                var total = ((cofinanciamentosExercicio2.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio2.ValorProtecaoSocialBasica.Value : 0M)
                                           + (cofinanciamentosExercicio2.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio2.ValorProtecaoSocialMediaComplexidade.Value : 0M)
                                           + (cofinanciamentosExercicio2.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio2.ValorProtecaoSocialAltaComplexidade.Value : 0M)
                                           + (cofinanciamentosBeneficioProgramaExercicio2.ValorBeneficiosEventuais != null ? cofinanciamentosBeneficioProgramaExercicio2.ValorBeneficiosEventuais.Value : 0M)
                                           //+ (cofinanciamentosBeneficioProgramaExercicio2.ValorProgramaProjeto != null ? cofinanciamentosBeneficioProgramaExercicio2.ValorProgramaProjeto.Value : 0M)
                                           + previsaoCofinanciamentoEstadualExercicio2
                                           );



                lblValorCofinanciamentoExercicio2.Text =
                           (total).ToString("N2");

                return total;
            }
            else return 0M;
        }

        private Decimal SomatoriaCofinanciamentosExercicio3(DradsPlanoMunicipalRecursosInfo cofinanciamentosExercicio3, DradsPlanoMunicipalBeneficioProgramaRecursosInfo cofinanciamentosBeneficioProgramaExercicio3, Decimal previsaoCofinanciamentoEstadualExercicio3)
        {
            if (cofinanciamentosExercicio3 != null)
            {
                //var total = ((cofinanciamentosExercicio2.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio2.ValorProtecaoSocialBasica.Value : 0M)
                //                           + (cofinanciamentosExercicio2.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio2.ValorProtecaoSocialMediaComplexidade.Value : 0M)
                //                           + (cofinanciamentosExercicio2.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio2.ValorProtecaoSocialAltaComplexidade.Value : 0M)
                //                           + (cofinanciamentosBeneficioProgramaExercicio2.ValorBeneficiosEventuais != null ? cofinanciamentosBeneficioProgramaExercicio2.ValorBeneficiosEventuais.Value : 0M)
                //                           + (cofinanciamentosBeneficioProgramaExercicio2.ValorProgramaProjeto != null ? cofinanciamentosBeneficioProgramaExercicio2.ValorProgramaProjeto.Value : 0M));

                var total = ((cofinanciamentosExercicio3.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio3.ValorProtecaoSocialBasica.Value : 0M)
                                           + (cofinanciamentosExercicio3.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio3.ValorProtecaoSocialMediaComplexidade.Value : 0M)
                                           + (cofinanciamentosExercicio3.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio3.ValorProtecaoSocialAltaComplexidade.Value : 0M)
                                           + (cofinanciamentosBeneficioProgramaExercicio3.ValorBeneficiosEventuais != null ? cofinanciamentosBeneficioProgramaExercicio3.ValorBeneficiosEventuais.Value : 0M)
                                           //+ (cofinanciamentosBeneficioProgramaExercicio3.ValorProgramaProjeto != null ? cofinanciamentosBeneficioProgramaExercicio3.ValorProgramaProjeto.Value : 0M)
                                           + previsaoCofinanciamentoEstadualExercicio3
                                           );



                lblValorCofinanciamentoExercicio3.Text =
                           (total).ToString("N2");

                return total;
            }
            else return 0M;
        }


        private Decimal SomatoriaCofinanciamentosExercicio4(DradsPlanoMunicipalRecursosInfo cofinanciamentosExercicio4, DradsPlanoMunicipalBeneficioProgramaRecursosInfo cofinanciamentosBeneficioProgramaExercicio4, Decimal previsaoCofinanciamentoEstadualExercicio4)
        {
            if (cofinanciamentosExercicio4 != null)
            {
                //var total = ((cofinanciamentosExercicio2.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio2.ValorProtecaoSocialBasica.Value : 0M)
                //                           + (cofinanciamentosExercicio2.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio2.ValorProtecaoSocialMediaComplexidade.Value : 0M)
                //                           + (cofinanciamentosExercicio2.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio2.ValorProtecaoSocialAltaComplexidade.Value : 0M)
                //                           + (cofinanciamentosBeneficioProgramaExercicio2.ValorBeneficiosEventuais != null ? cofinanciamentosBeneficioProgramaExercicio2.ValorBeneficiosEventuais.Value : 0M)
                //                           + (cofinanciamentosBeneficioProgramaExercicio2.ValorProgramaProjeto != null ? cofinanciamentosBeneficioProgramaExercicio2.ValorProgramaProjeto.Value : 0M));

                var total = ((cofinanciamentosExercicio4.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio4.ValorProtecaoSocialBasica.Value : 0M)
                                           + (cofinanciamentosExercicio4.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio4.ValorProtecaoSocialMediaComplexidade.Value : 0M)
                                           + (cofinanciamentosExercicio4.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio4.ValorProtecaoSocialAltaComplexidade.Value : 0M)
                                           + (cofinanciamentosBeneficioProgramaExercicio4.ValorBeneficiosEventuais != null ? cofinanciamentosBeneficioProgramaExercicio4.ValorBeneficiosEventuais.Value : 0M)
                                           //+ (cofinanciamentosBeneficioProgramaExercicio4.ValorProgramaProjeto != null ? cofinanciamentosBeneficioProgramaExercicio4.ValorProgramaProjeto.Value : 0M)
                                           + previsaoCofinanciamentoEstadualExercicio4
                                           );



                lblValorCofinanciamentoExercicio4.Text =
                           (total).ToString("N2");

                return total;
            }
            else return 0M;
        }


        #endregion


        #region Bloqueio e Desbloqueio
        private void AplicarBloqueioDesbloqueio()
        {
            using (var proxy = new ProxyPrefeitura())
            {
                int idPrefeitura = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idPrefeitura"]));
                var prefeitura = proxy.Service.GetPrefeituraById(idPrefeitura);


                WebControl[] controlesExercicio1 = this.ObterControlesBloqueioDesbloqueioExercicio1();
                WebControl[] controlesExercicio2 = this.ObterControlesBloqueioDesbloqueioExercicio2();
                WebControl[] controlesExercicio3 = this.ObterControlesBloqueioDesbloqueioExercicio3();
                WebControl[] controlesExercicio4 = this.ObterControlesBloqueioDesbloqueioExercicio4();

                WebControl[] controlesExercicio1Reprogramacao = this.ObterControlesBloqueioDesbloqueioExercicio1Reprogramacao();
                WebControl[] controlesExercicio2Reprogramacao = this.ObterControlesBloqueioDesbloqueioExercicio2Reprogramacao();
                WebControl[] controlesExercicio3Reprogramacao = this.ObterControlesBloqueioDesbloqueioExercicio3Reprogramacao();
                WebControl[] controlesExercicio4Reprogramacao = this.ObterControlesBloqueioDesbloqueioExercicio4Reprogramacao();

                WebControl[] controlesExercicio1Demandas = this.ObterControlesBloqueioDesbloqueioExercicio1Demandas();
                WebControl[] controlesExercicio2Demandas = this.ObterControlesBloqueioDesbloqueioExercicio1DemandasExercicio2();
                WebControl[] controlesExercicio3Demandas = this.ObterControlesBloqueioDesbloqueioExercicio1DemandasExercicio3();
                WebControl[] controlesExercicio4Demandas = this.ObterControlesBloqueioDesbloqueioExercicio1DemandasExercicio4();

                #region Valor Repassado

                if (Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0Inicio(controlesExercicio1, Exercicios[0], prefeitura))
                {
                    ExercicioLiberado2022 = true;
                }
                else
                {
                    ExercicioLiberado2022 = false;
                }

                if (Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0Inicio(controlesExercicio2, Exercicios[1], prefeitura))
                {
                    ExercicioLiberado2023 = true;
                }
                else
                {
                    ExercicioLiberado2023 = false;
                }                    

                if (Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0Inicio(controlesExercicio3, Exercicios[2], prefeitura))
                {
                    ExercicioLiberado2024 = true;
                }
                else
                {
                    ExercicioLiberado2024 = false;
                }

                if (Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0Inicio(controlesExercicio4, Exercicios[3], prefeitura))
                {
                    ExercicioLiberado2025 = true;
                }
                else
                {
                    ExercicioLiberado2025 = false;
                }                    

                #endregion

                #region reprogramacao
                //Removido a validação do bloqueio 2018 - Viável validação para 2022
                Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0InicioReprogramacao(controlesExercicio1Reprogramacao,  Exercicios[0], prefeitura);
                Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0InicioReprogramacao(controlesExercicio2Reprogramacao, Exercicios[1], prefeitura);
                Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0InicioReprogramacao(controlesExercicio3Reprogramacao, Exercicios[2], prefeitura);
                Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0InicioReprogramacao(controlesExercicio4Reprogramacao, Exercicios[3], prefeitura);
                #endregion

                #region Demandas Parlamentares
                Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0InicioDemandas(controlesExercicio1Demandas,Exercicios[0],prefeitura);
                Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0InicioDemandas(controlesExercicio2Demandas, Exercicios[1], prefeitura);
                Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0InicioDemandas(controlesExercicio3Demandas, Exercicios[2], prefeitura);
                Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0InicioDemandas(controlesExercicio4Demandas, Exercicios[3], prefeitura);
                #endregion


            }
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio1()
        {
            WebControl[] controles = {
                                       txtProtecaoSocialBasica
                                     , txtProtecaoSocialMedia
                                     , txtProtecaoSocialAlta
                                     , txtBeneficiosEventuais
                                     , txtSaoPauloSolidario
                                     };

            return controles;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio2()
        {
            WebControl[] controles = {
                                       txtProtecaoSocialBasicaExercicio2
                                     , txtProtecaoSocialMediaExercicio2
                                     , txtProtecaoSocialAltaExercicio2
                                     , txtBeneficiosEventuaisExercicio2
                                     , txtSaoPauloSolidarioExercicio2
                                     };

            return controles;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio3()
        {
            WebControl[] controles = {
                                       txtProtecaoSocialBasicaExercicio3
                                     , txtProtecaoSocialMediaExercicio3
                                     , txtProtecaoSocialAltaExercicio3
                                     , txtBeneficiosEventuaisExercicio3
                                     , txtSaoPauloSolidarioExercicio3
                                     };

            return controles;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio4()
        {
            WebControl[] controles = {
                                       txtProtecaoSocialBasicaExercicio4
                                     , txtProtecaoSocialMediaExercicio4
                                     , txtProtecaoSocialAltaExercicio4
                                     , txtBeneficiosEventuaisExercicio4
                                     , txtSaoPauloSolidarioExercicio4
                                     };

            return controles;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio1Reprogramacao()
        {
            WebControl[] controles = {

                                         txtProtecaoBasicaReprogramado
                                       , txtProtecaoMediaReprogramado
                                       , txtProtecaoAltaReprogramado
                                       , txtBeneficiosEventuaisReprogramado
                                       , txtSaoPauloSolidarioReprogramado
                                       , txtProtecaoBasicaReprogramacaoDemandas
                                       , txtProtecaoMediaReprogramacaoDemandas
                                       , txtProtecaoAltaReprogramacaoDemandas
                                       , txtBeneficiosEventuaisDemandasReprogramado

                                     };

            return controles;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio2Reprogramacao()
        {
            WebControl[] controles = {
                                         txtProtecaoBasicaReprogramadoExercicio2
                                       , txtProtecaoMediaReprogramadoExercicio2
                                       , txtProtecaoAltaReprogramadoExercicio2
                                       , txtBeneficiosEventuaisReprogramadoExercicio2
                                       , txtSaoPauloSolidarioReprogramadoExercicio2
                                       , txtProtecaoBasicaReprogramacaoDemandasExercicio2
                                       , txtProtecaoMediaReprogramacaoDemandasExercicio2
                                       , txtProtecaoAltaReprogramacaoDemandasExercicio2
                                       ,txtBeneficiosEventuaisDemandasReprogramadoExercicio2
                                     };

            return controles;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio3Reprogramacao()
        {
            WebControl[] controles = {
                                         txtProtecaoBasicaReprogramadoExercicio3
                                       , txtProtecaoMediaReprogramadoExercicio3
                                       , txtProtecaoAltaReprogramadoExercicio3
                                       , txtBeneficiosEventuaisReprogramadoExercicio3
                                       , txtSaoPauloSolidarioReprogramadoExercicio3
                                       , txtProtecaoBasicaReprogramacaoDemandasExercicio3
                                       , txtProtecaoMediaReprogramacaoDemandasExercicio3
                                       , txtProtecaoAltaReprogramacaoDemandasExercicio3
                                       ,txtBeneficiosEventuaisDemandasReprogramadoExercicio3
                                     };

            return controles;
        }
        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio4Reprogramacao()
        {
            WebControl[] controles = {
                                         txtProtecaoBasicaReprogramadoExercicio4
                                       , txtProtecaoMediaReprogramadoExercicio4
                                       , txtProtecaoAltaReprogramadoExercicio4
                                       , txtBeneficiosEventuaisReprogramadoExercicio4
                                       , txtSaoPauloSolidarioReprogramadoExercicio4
                                       , txtProtecaoBasicaReprogramacaoDemandasExercicio4
                                       , txtProtecaoMediaReprogramacaoDemandasExercicio4
                                       , txtProtecaoAltaReprogramacaoDemandasExercicio4
                                       , txtBeneficiosEventuaisDemandasReprogramadoExercicio4
                                     };

            return controles;
        }
        #endregion

        #region Controles Demandas
        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio1Demandas()
        {
            WebControl[] controles = { 
                                         txtProtecaoBasicaDemandas
                                       , txtProtecaoMediaDemandas
                                       , txtProtecaoAltaDemandas
                                       , txtBeneficiosEventuaisDemandas
                                       , txtSaoPauloSolidarioDemandas
                                     };
            return controles;
        }


        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio1DemandasExercicio2()
        {
            WebControl[] controles = { 
                                         txtProtecaoBasicaDemandasExercicio2
                                       , txtProtecaoMediaDemandasExercicio2
                                       , txtProtecaoAltaDemandasExercicio2
                                       , txtBeneficiosEventuaisDemandasExercicio2
                                       , txtSaoPauloSolidarioDemandasExercicio2
                                     };
            return controles;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio1DemandasExercicio3()
        {
            WebControl[] controles = { 
                                         txtProtecaoBasicaDemandasExercicio3
                                       , txtProtecaoMediaDemandasExercicio3
                                       , txtProtecaoAltaDemandasExercicio3
                                       , txtBeneficiosEventuaisDemandasExercicio3
                                       , txtSaoPauloSolidarioDemandasExercicio3
                                     };
            return controles;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioExercicio1DemandasExercicio4()
        {
            WebControl[] controles = { 
                                         txtProtecaoBasicaDemandasExercicio4
                                       , txtProtecaoMediaDemandasExercicio4
                                       , txtProtecaoAltaDemandasExercicio4
                                       , txtBeneficiosEventuaisDemandasExercicio4
                                       , txtSaoPauloSolidarioDemandasExercicio4
	
                                     };
            return controles;
        }        
        #endregion


        #region crud
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            bool reprogramacao = false;

            int exercicio1 = FFluxo.Exercicios[0];
            int exercicio2 = FFluxo.Exercicios[1];
            int exercicio3 = FFluxo.Exercicios[2];
            int exercicio4 = FFluxo.Exercicios[3];

            var idPrefeitura = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idPrefeitura"]));
            var situacao = (ESituacao)Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idSituacao"]));

            if (txtDescricao.Text.Length > 8000)
            {
                txtDescricao.Text = txtDescricao.Text.Substring(0, 8000);
            }

            var motivo = txtDescricao.Text;

            if (String.IsNullOrEmpty(motivo))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError("O campo " + lblDescricao.Text + " é obrigatório!"), true);
                btnSalvar.Enabled = true;
                return;
            }

            if (situacao == ESituacao.Parafinalizacao)
            {
                //using (var proxy = new ProxyPrefeitura())
                //{
                //    //Load Valores
                //    reprogramacao = proxy.Service.GetPrefeituraById(idPrefeitura).ValoresReprogramadosDrads.HasValue
                //        ? proxy.Service.GetPrefeituraById(idPrefeitura).ValoresReprogramadosDrads.Value : false;
                //}
                if (!ValidarValoresBloco5(idPrefeitura))
                {
                    return;
                };

            }
           
            switch (situacao)
            {
                #region Devolvido Drads
                case ESituacao.DevolvidoDrads:
                    using (var proxy = new ProxyPlanoMunicipal())
                    {
                        proxy.Service.DevolverPlanoMunicipalDradsParaOrgaoGestor(idPrefeitura, motivo);
                    }
                    Response.Redirect("~/ConsultaFluxoPMASDRADS.aspx");
                    break;
                #endregion
                #region Desbloqueado
                case ESituacao.Desbloqueado:
                    using (var proxy = new ProxyPlanoMunicipal())
                    {
                        proxy.Service.DesbloqueiarPlanoMunicipalParaOrgaoGestor(idPrefeitura, motivo);
                    }
                    Response.Redirect("~/ConsultaFluxoPMASDRADS.aspx");
                    break;
                #endregion
                #region Em Analise do CMAS
                case ESituacao.EmAnalisedoCMAS:
                    using (var proxy = new ProxyPlanoMunicipal())
                    {
                        proxy.Service.DesbloqueiarPlanoMunicipalParaCMAS(idPrefeitura, motivo);
                    }
                    Response.Redirect("~/ConsultaFluxoPMASDRADS.aspx");
                    break;
                #endregion
                #region Autoriza Desbloqueio CMAS
                case ESituacao.AutorizaDesbloqueioCMAS:
                    using (var proxy = new ProxyPlanoMunicipal())
                    {
                        proxy.Service.AutorizarDesbloqueioPlanoMunicipalParaCMAS(idPrefeitura, motivo);
                    }
                    Response.Redirect("~/ConsultaFluxoPMASCAS.aspx?msg=DC");
                    break;
                #endregion
                #region Autoriza Desbloqueio Reprogramacao
                case ESituacao.AutorizaDesbloqueioReprogramacao:
                    using (var proxy = new ProxyPlanoMunicipal())
                    {
                        proxy.Service.AutorizarDesbloqueioPlanoMunicipalParaOrgaoGestor(idPrefeitura, motivo, true);
                    }
                    Response.Redirect("~/ConsultaFluxoPMASCAS.aspx?msg=DO");
                    break;
                #endregion
                #region Autoriza desbloqueio Demandas
                case ESituacao.AutorizaDesbloqueioDemandas:
                    using (var proxy = new ProxyPlanoMunicipal())
                    {
                        proxy.Service.AutorizarDesbloqueioDemandasPlanoMunicipalParaOrgaoGestor(idPrefeitura, motivo, true);
                    }
                    Response.Redirect("~/ConsultaFluxoPMASCAS.aspx?msg=DO");
                    break;
                #endregion
                #region Autoriza Desbloqueio Gestor
                case ESituacao.AutorizaDesbloqueioGestor:
                    using (var proxy = new ProxyPlanoMunicipal())
                    {
                        proxy.Service.AutorizarDesbloqueioPlanoMunicipalParaOrgaoGestor(idPrefeitura, motivo, null);
                    }
                    Response.Redirect("~/ConsultaFluxoPMASCAS.aspx?msg=DO");
                    break;
                #endregion
                #region Devolvido pelo CMAS
                case ESituacao.DevolvidopeloCMAS:
                    using (var proxy = new ProxyPlanoMunicipal())
                    {
                        proxy.Service.DevolverPlanoMunicipalCMASParaOrgaoGestor(idPrefeitura, motivo);
                    }
                    Seds.PMAS.QUADRIENAL.UI.Processos.Util.CarregarPrefeitura();
                    Response.Redirect("~/ConsultaFluxoPMASCMAS.aspx");
                    break;

                #endregion

                #region DevolvidoCas
                case ESituacao.DevolverParaCas:
                    using (var proxy = new ProxyPlanoMunicipal())
                    {
                        proxy.Service.DevolverPlanoMunicipalDradsParaCAS(idPrefeitura, motivo);
                    }
                    Seds.PMAS.QUADRIENAL.UI.Processos.Util.CarregarPrefeitura();
                    Response.Redirect("~/ConsultaFluxoPMASCMAS.aspx");
                    break;
                #endregion

                #region Para finalizacao
                case ESituacao.Parafinalizacao:
                    try
                    {

                        using (var proxy = new ProxyPlanoMunicipal())
                        {
                            var fluxoPrefeituraId = idPrefeitura;
                            List<PlanoMunicipalHistoricoConsolidadoInfo> planos = new List<PlanoMunicipalHistoricoConsolidadoInfo>();
                            String fluxoMotivo = motivo;

                            #region exercicio 1
                            PlanoMunicipalHistoricoConsolidadoInfo planoConsolidadoExercicio1 = new PlanoMunicipalHistoricoConsolidadoInfo();
                            planoConsolidadoExercicio1.Exercicio = exercicio1;
                            planoConsolidadoExercicio1.IdPrefeitura = idPrefeitura;
                            planoConsolidadoExercicio1.ValorProtecaoSocialBasica = Convert.ToDecimal(txtProtecaoSocialBasica.Text);
                            planoConsolidadoExercicio1.ValorProtecaoSocialMediaComplexidade = Convert.ToDecimal(txtProtecaoSocialMedia.Text);
                            planoConsolidadoExercicio1.ValorProtecaoSocialAltaComplexidade = Convert.ToDecimal(txtProtecaoSocialAlta.Text);

                            planoConsolidadoExercicio1.ValorProgramaProjetoSolidario = Convert.ToDecimal(txtSaoPauloSolidario.Text); //??programas e projetos??
                            planoConsolidadoExercicio1.ValorBeneficiosEventuais = Convert.ToDecimal(txtBeneficiosEventuais.Text);

                            planoConsolidadoExercicio1.ValorProtecaoSocialBasicaReprogramado = Convert.ToDecimal(txtProtecaoBasicaReprogramado.Text);
                            planoConsolidadoExercicio1.ValorProtecaoSocialMediaReprogramado = Convert.ToDecimal(txtProtecaoMediaReprogramado.Text);
                            planoConsolidadoExercicio1.ValorProtecaoSocialAltaReprogramado = Convert.ToDecimal(txtProtecaoAltaReprogramado.Text);
                            planoConsolidadoExercicio1.ValorProgramaProjetoReprogramado = Convert.ToDecimal(txtSaoPauloSolidarioReprogramado.Text);
                            planoConsolidadoExercicio1.ValorBeneficioEventuaisReprogramado = Convert.ToDecimal(txtBeneficiosEventuaisReprogramado.Text);

                            planoConsolidadoExercicio1.ValorProtecaoSocialBasicaDemandas = Convert.ToDecimal(txtProtecaoBasicaDemandas.Text);
                            planoConsolidadoExercicio1.ValorProtecaoSocialMediaDemandas = Convert.ToDecimal(txtProtecaoMediaDemandas.Text);
                            planoConsolidadoExercicio1.ValorProtecaoSocialAltaDemandas = Convert.ToDecimal(txtProtecaoAltaDemandas.Text);
                            planoConsolidadoExercicio1.ValorProgramaProjetoDemandas = Convert.ToDecimal(txtSaoPauloSolidarioDemandas.Text);
                            planoConsolidadoExercicio1.ValorBeneficioEventuaisDemandas = Convert.ToDecimal(txtBeneficiosEventuaisDemandas.Text);

                            planoConsolidadoExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoBasicaReprogramacaoDemandas.Text);
                            planoConsolidadoExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoMediaReprogramacaoDemandas.Text);
                            planoConsolidadoExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoAltaReprogramacaoDemandas.Text);
                            planoConsolidadoExercicio1.ValorBeneficioEventuaisReprogramadoDemandas = Convert.ToDecimal(txtBeneficiosEventuaisDemandasReprogramado.Text);
                            planoConsolidadoExercicio1.ValorProgramaProjetoReprogramadoDemandas = Convert.ToDecimal("0");




                            #endregion

                            #region exercicio 2
                            PlanoMunicipalHistoricoConsolidadoInfo planoConsolidadoExercicio2 = new PlanoMunicipalHistoricoConsolidadoInfo();
                            planoConsolidadoExercicio2.Exercicio = exercicio2;
                            planoConsolidadoExercicio2.ValorProtecaoSocialBasica = Convert.ToDecimal(txtProtecaoSocialBasicaExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProtecaoSocialMediaComplexidade = Convert.ToDecimal(txtProtecaoSocialMediaExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProtecaoSocialAltaComplexidade = Convert.ToDecimal(txtProtecaoSocialAltaExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProgramaProjetoSolidario = Convert.ToDecimal(txtSaoPauloSolidarioExercicio2.Text);//??programas e projetos??
                            planoConsolidadoExercicio2.ValorBeneficiosEventuais = Convert.ToDecimal(txtBeneficiosEventuaisExercicio2.Text);

                            planoConsolidadoExercicio2.ValorProtecaoSocialBasicaReprogramado = Convert.ToDecimal(txtProtecaoBasicaReprogramadoExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProtecaoSocialMediaReprogramado = Convert.ToDecimal(txtProtecaoMediaReprogramadoExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProtecaoSocialAltaReprogramado = Convert.ToDecimal(txtProtecaoAltaReprogramadoExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProgramaProjetoReprogramado = Convert.ToDecimal(txtSaoPauloSolidarioReprogramadoExercicio2.Text);
                            planoConsolidadoExercicio2.ValorBeneficioEventuaisReprogramado = Convert.ToDecimal(txtBeneficiosEventuaisReprogramadoExercicio2.Text);

                            planoConsolidadoExercicio2.ValorProtecaoSocialBasicaDemandas = Convert.ToDecimal(txtProtecaoBasicaDemandasExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProtecaoSocialMediaDemandas = Convert.ToDecimal(txtProtecaoMediaDemandasExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProtecaoSocialAltaDemandas = Convert.ToDecimal(txtProtecaoAltaDemandasExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProgramaProjetoDemandas = Convert.ToDecimal(txtSaoPauloSolidarioDemandasExercicio2.Text);
                            planoConsolidadoExercicio2.ValorBeneficioEventuaisDemandas = Convert.ToDecimal(txtBeneficiosEventuaisDemandasExercicio2.Text);

                            planoConsolidadoExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoBasicaReprogramacaoDemandasExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoMediaReprogramacaoDemandasExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoAltaReprogramacaoDemandasExercicio2.Text);
                            planoConsolidadoExercicio2.ValorBeneficioEventuaisReprogramadoDemandas = Convert.ToDecimal(txtBeneficiosEventuaisDemandasReprogramadoExercicio2.Text);
                            planoConsolidadoExercicio2.ValorProgramaProjetoReprogramadoDemandas = Convert.ToDecimal("0");

                            #endregion

                            #region exercicio 3
                            PlanoMunicipalHistoricoConsolidadoInfo planoConsolidadoExercicio3 = new PlanoMunicipalHistoricoConsolidadoInfo();
                            planoConsolidadoExercicio3.Exercicio = exercicio3;
                            planoConsolidadoExercicio3.ValorProtecaoSocialBasica = Convert.ToDecimal(txtProtecaoSocialBasicaExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProtecaoSocialMediaComplexidade = Convert.ToDecimal(txtProtecaoSocialMediaExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProtecaoSocialAltaComplexidade = Convert.ToDecimal(txtProtecaoSocialAltaExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProgramaProjetoSolidario = Convert.ToDecimal(txtSaoPauloSolidarioExercicio3.Text);//??programas e projetos??
                            planoConsolidadoExercicio3.ValorBeneficiosEventuais = Convert.ToDecimal(txtBeneficiosEventuaisExercicio3.Text);

                            planoConsolidadoExercicio3.ValorProtecaoSocialBasicaReprogramado = Convert.ToDecimal(txtProtecaoBasicaReprogramadoExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProtecaoSocialMediaReprogramado = Convert.ToDecimal(txtProtecaoMediaReprogramadoExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProtecaoSocialAltaReprogramado = Convert.ToDecimal(txtProtecaoAltaReprogramadoExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProgramaProjetoReprogramado = Convert.ToDecimal(txtSaoPauloSolidarioReprogramadoExercicio3.Text);
                            planoConsolidadoExercicio3.ValorBeneficioEventuaisReprogramado = Convert.ToDecimal(txtBeneficiosEventuaisReprogramadoExercicio3.Text);

                            planoConsolidadoExercicio3.ValorProtecaoSocialBasicaDemandas = Convert.ToDecimal(txtProtecaoBasicaDemandasExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProtecaoSocialMediaDemandas = Convert.ToDecimal(txtProtecaoMediaDemandasExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProtecaoSocialAltaDemandas = Convert.ToDecimal(txtProtecaoAltaDemandasExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProgramaProjetoDemandas = Convert.ToDecimal(txtSaoPauloSolidarioDemandasExercicio3.Text);
                            planoConsolidadoExercicio3.ValorBeneficioEventuaisDemandas = Convert.ToDecimal(txtBeneficiosEventuaisDemandasExercicio3.Text);

                            planoConsolidadoExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoBasicaReprogramacaoDemandasExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoMediaReprogramacaoDemandasExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoAltaReprogramacaoDemandasExercicio3.Text);
                            planoConsolidadoExercicio3.ValorBeneficioEventuaisReprogramadoDemandas = Convert.ToDecimal(txtBeneficiosEventuaisDemandasReprogramadoExercicio3.Text);
                            planoConsolidadoExercicio3.ValorProgramaProjetoReprogramadoDemandas = Convert.ToDecimal("0");


                            #endregion

                            #region exercicio 4
                            PlanoMunicipalHistoricoConsolidadoInfo planoConsolidadoExercicio4 = new PlanoMunicipalHistoricoConsolidadoInfo();
                            planoConsolidadoExercicio4.Exercicio = exercicio4;
                            planoConsolidadoExercicio4.ValorProtecaoSocialBasica = Convert.ToDecimal(txtProtecaoSocialBasicaExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProtecaoSocialMediaComplexidade = Convert.ToDecimal(txtProtecaoSocialMediaExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProtecaoSocialAltaComplexidade = Convert.ToDecimal(txtProtecaoSocialAltaExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProgramaProjetoSolidario = Convert.ToDecimal(txtSaoPauloSolidarioExercicio4.Text);//??programas e projetos??
                            planoConsolidadoExercicio4.ValorBeneficiosEventuais = Convert.ToDecimal(txtBeneficiosEventuaisExercicio4.Text);

                            planoConsolidadoExercicio4.ValorProtecaoSocialBasicaReprogramado = Convert.ToDecimal(txtProtecaoBasicaReprogramadoExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProtecaoSocialMediaReprogramado = Convert.ToDecimal(txtProtecaoMediaReprogramadoExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProtecaoSocialAltaReprogramado = Convert.ToDecimal(txtProtecaoAltaReprogramadoExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProgramaProjetoReprogramado = Convert.ToDecimal(txtSaoPauloSolidarioReprogramadoExercicio4.Text);
                            planoConsolidadoExercicio4.ValorBeneficioEventuaisReprogramado = Convert.ToDecimal(txtBeneficiosEventuaisReprogramadoExercicio4.Text);

                            planoConsolidadoExercicio4.ValorProtecaoSocialBasicaDemandas = Convert.ToDecimal(txtProtecaoBasicaDemandasExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProtecaoSocialMediaDemandas = Convert.ToDecimal(txtProtecaoMediaDemandasExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProtecaoSocialAltaDemandas = Convert.ToDecimal(txtProtecaoAltaDemandasExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProgramaProjetoDemandas = Convert.ToDecimal(txtSaoPauloSolidarioDemandasExercicio4.Text);
                            planoConsolidadoExercicio4.ValorBeneficioEventuaisDemandas = Convert.ToDecimal(txtBeneficiosEventuaisDemandasExercicio4.Text);

                            planoConsolidadoExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoBasicaReprogramacaoDemandasExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoMediaReprogramacaoDemandasExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas = Convert.ToDecimal(txtProtecaoAltaReprogramacaoDemandasExercicio4.Text);
                            planoConsolidadoExercicio4.ValorBeneficioEventuaisReprogramadoDemandas = Convert.ToDecimal(txtBeneficiosEventuaisDemandasReprogramadoExercicio4.Text);
                            planoConsolidadoExercicio4.ValorProgramaProjetoReprogramadoDemandas = Convert.ToDecimal("0");

                            #endregion

                            planos.Add(planoConsolidadoExercicio1);
                            planos.Add(planoConsolidadoExercicio2);
                            planos.Add(planoConsolidadoExercicio3);
                            planos.Add(planoConsolidadoExercicio4);

                            if (new VerificadorPendenciaPMAS().PlanoMunicipalPossuiPendenciaOrgaoGestor(idPrefeitura, EPerfil.OrgaoGestor))
                            {
                                throw new Exception("O Plano Municipal possui pendências! O mesmo não pode ser enviado para finalização.");
                            }

                            proxy.Service.EnviarPlanoMunicipalParaFinalizacao(fluxoPrefeituraId, fluxoMotivo, planos);
                            btnSalvar.Enabled = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        var script = Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    Response.Redirect("~/ConsultaFluxoPMASDRADS.aspx?msg=DO");
                    break;
                #endregion

                #region aprovado e rejeitado
                case ESituacao.Aprovado:
                case ESituacao.Rejeitado:
                    if (rblAprovacao.SelectedIndex == -1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType()
                                    , Guid.NewGuid().ToString()
                                    , Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError("Selecione uma das opções sobre o parecer - Favorável ou Desfavorável."), true);
                        break;
                    }
                    if (rblAprovacao.SelectedValue == "1")
                    {
                        using (var p = new ProxyRedeProtecaoSocial())
                        {
                            var lstUnidades = p.Service.GetIdentificacaoUnidadesPrivadaByPrefeitura(idPrefeitura, "", 0);
                            int inconsistenciaCMAS = 0;
                            lstUnidades.ForEach(l =>
                            {
                                var u = p.Service.GetUnidadePrivadaById(l.Id);
                                if (u.Desativado == false)
                                {
                                    switch (u.IdSituacaoInscricao)
                                    {
                                        case 1:
                                            if (String.IsNullOrWhiteSpace(u.InscricaoCMAS) || !u.DataPublicacao.HasValue || !u.IdSituacaoAtualInscricao.HasValue)
                                                inconsistenciaCMAS++;
                                            break;
                                        case 2:
                                            if (!u.DataPublicacao.HasValue)
                                                inconsistenciaCMAS++;
                                            break;
                                        case null:
                                            inconsistenciaCMAS++;
                                            break;
                                        default:

                                            break;
                                    }
                                }
                            });

                            if (inconsistenciaCMAS > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType()
                                            , Guid.NewGuid().ToString()
                                            , Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError("O(s) campo(s) de uma ou mais Unidades Privadas relacionados ao CMAS não estão preenchidos."), true);
                                break;
                            }
                        }
                    }

                    #region salve parecer
                    using (var proxy = new ProxyPlanoMunicipal())
                    {
                        proxy.Service.SaveParecerConselhoMunicipalSobreAlteracoes(idPrefeitura, motivo, rblAprovacao.SelectedValue == "1");
                    }
                    #endregion

                    Seds.PMAS.QUADRIENAL.UI.Processos.Util.CarregarPrefeitura();
                    Response.Redirect("~/ConsultaFluxoPMASCMAS.aspx");
                    break;

                #endregion
            }
        }

        private bool ValidarValoresBloco5(Int32 idPrefeitura)
        {
            int exercicio1 = FFluxo.Exercicios[0];
            int exercicio2 = FFluxo.Exercicios[1];
            int exercicio3 = FFluxo.Exercicios[2];
            int exercicio4 = FFluxo.Exercicios[3];

            this.AplicarBloqueioDesbloqueio();

            if (idPrefeitura != null)
            {
                using (var proxyDrads = new ProxyDradsPlanoMunicipal())
                {
                    #region Buscas [bloco 5]
                    //cofinanciamento
                    var cofinanciamentosExercicio1 = proxyDrads.Service.GetResumoCofinanciamentoDradsBy(idPrefeitura, exercicio1);
                    var cofinanciamentosExercicio2 = proxyDrads.Service.GetResumoCofinanciamentoDradsBy(idPrefeitura, exercicio2);
                    var cofinanciamentosExercicio3 = proxyDrads.Service.GetResumoCofinanciamentoDradsBy(idPrefeitura, exercicio3);
                    var cofinanciamentosExercicio4 = proxyDrads.Service.GetResumoCofinanciamentoDradsBy(idPrefeitura, exercicio4);

                    //reprogramacao
                    var cofinanciamentosReprogramadoExercicio1 = proxyDrads.Service.GetResumoCofinanciamentoReprogramadoDradsBy(idPrefeitura, exercicio1);
                    var cofinanciamentosReprogramadoExercicio2 = proxyDrads.Service.GetResumoCofinanciamentoReprogramadoDradsBy(idPrefeitura, exercicio2);
                    var cofinanciamentosReprogramadoExercicio3 = proxyDrads.Service.GetResumoCofinanciamentoReprogramadoDradsBy(idPrefeitura, exercicio3);
                    var cofinanciamentosReprogramadoExercicio4 = proxyDrads.Service.GetResumoCofinanciamentoReprogramadoDradsBy(idPrefeitura, exercicio4);
                    //programa e beneficio
                    //var cofinanciamentosBeneficioProgramaExercicio1 = proxyDrads.Service.GetResumoCofinanciamentoBeneficioProgramaDradsBy(idPrefeitura, exercicio1);
                    //var cofinanciamentosBeneficioProgramaExercicio2 = proxyDrads.Service.GetResumoCofinanciamentoBeneficioProgramaDradsBy(idPrefeitura, exercicio2);

                    //Demandas Parlamentares
                    var cofinanciamentosDemandasExercicio1 = proxyDrads.Service.GetResumoCofinanciamentoDemandasDradsBy(idPrefeitura, exercicio1);
                    var cofinanciamentosDemandasExercicio2 = proxyDrads.Service.GetResumoCofinanciamentoDemandasDradsBy(idPrefeitura, exercicio2);
                    var cofinanciamentosDemandasExercicio3 = proxyDrads.Service.GetResumoCofinanciamentoDemandasDradsBy(idPrefeitura, exercicio3);
                    var cofinanciamentosDemandasExercicio4 = proxyDrads.Service.GetResumoCofinanciamentoDemandasDradsBy(idPrefeitura, exercicio4);

                    //Demandas Reprogramacao
                    var cofinanciamentosReprogramadosDemandasExercicio1 = proxyDrads.Service.GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(idPrefeitura, exercicio1);
                    var cofinanciamentosReprogramadosDemandasExercicio2 = proxyDrads.Service.GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(idPrefeitura, exercicio2);
                    var cofinanciamentosReprogramadosDemandasExercicio3 = proxyDrads.Service.GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(idPrefeitura, exercicio3);
                    var cofinanciamentosReprogramadosDemandasExercicio4 = proxyDrads.Service.GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(idPrefeitura, exercicio4);

                    #region Cofinanciamento Programas Projetos

                    var cofinanciamentosBeneficioProgramaExercicio1 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 4, exercicio1);

                        decimal programaProjetoExercicio1 = 0;
                        decimal programaProjetoReprogramadoExercicio1 = 0;

                        foreach (var item in cofinanciamentosBeneficioProgramaExercicio1)
                        {
                            programaProjetoExercicio1 += item.PrevisaoOrcamentaria;
                            programaProjetoReprogramadoExercicio1 += item.RecursoReprogramadoAnoAnterior;
                           
                        }


                    var cofinanciamentosBeneficioProgramaExercicio2 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 4, exercicio2);

                        decimal programaProjetoExercicio2 = 0;
                        decimal programaProjetoReprogramadoExercicio2 = 0;

                        foreach (var item in cofinanciamentosBeneficioProgramaExercicio2)
                        {
                            programaProjetoExercicio2 += item.PrevisaoOrcamentaria;
                            programaProjetoReprogramadoExercicio2 += item.RecursoReprogramadoAnoAnterior;
                        }


                    var cofinanciamentosBeneficioProgramaExercicio3 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 4, exercicio3);
                    
                        decimal programaProjetoExercicio3 = 0;
                        decimal programaProjetoReprogramadoExercicio3 = 0;

                        foreach (var item in cofinanciamentosBeneficioProgramaExercicio3)
                        {
                            programaProjetoExercicio3 += item.PrevisaoOrcamentaria;
                            programaProjetoReprogramadoExercicio3 += item.RecursoReprogramadoAnoAnterior;
                        }                    
                    

                    var cofinanciamentosBeneficioProgramaExercicio4 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 4, exercicio4);

                        decimal programaProjetoExercicio4 = 0;
                        decimal programaProjetoReprogramadoExercicio4 = 0;

                        foreach (var item in cofinanciamentosBeneficioProgramaExercicio4)
                        {
                            programaProjetoExercicio4 += item.PrevisaoOrcamentaria;
                            programaProjetoReprogramadoExercicio4 += item.RecursoReprogramadoAnoAnterior;
                        }
                    
                    #endregion


                    #region Cofinanciamento BeneficiosEventuais

                    var cofinanciamentosBeneficioEventuaisExercicio1 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 5, exercicio1);

                        decimal beneficiosEventuaisExercicio1 = 0;
                        decimal beneficiosEventuaisReprogramadoExercicio1 = 0;

                        foreach (var item in cofinanciamentosBeneficioEventuaisExercicio1)
                        {
                            beneficiosEventuaisExercicio1 += item.PrevisaoOrcamentaria;
                            beneficiosEventuaisReprogramadoExercicio1 += item.RecursoReprogramadoAnoAnterior;
                        }


                    var cofinanciamentosBeneficioEventuaisExercicio2 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 5, exercicio2);

                        decimal beneficiosEventuaisExercicio2 = 0;
                        decimal beneficiosEventuaisReprogramadoExercicio2 = 0;

                        foreach (var item in cofinanciamentosBeneficioEventuaisExercicio2)
                        {
                            beneficiosEventuaisExercicio2 += item.PrevisaoOrcamentaria;
                            beneficiosEventuaisReprogramadoExercicio2 += item.RecursoReprogramadoAnoAnterior;
                        }


                    var cofinanciamentosBeneficioEventuaisExercicio3 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 5, exercicio3);

                       decimal beneficiosEventuaisExercicio3 = 0;
                       decimal beneficiosEventuaisReprogramadoExercicio3 = 0;

                       foreach (var item in cofinanciamentosBeneficioEventuaisExercicio3)
                       {
                           beneficiosEventuaisExercicio3 += item.PrevisaoOrcamentaria;
                           beneficiosEventuaisReprogramadoExercicio3 += item.RecursoReprogramadoAnoAnterior;
                       }


                    var cofinanciamentosBeneficioEventuaisExercicio4 = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 5, exercicio4);

                       decimal beneficiosEventuaisExercicio4 = 0;
                       decimal beneficiosEventuaisReprogramadoExercicio4 = 0;   

                       foreach (var item in cofinanciamentosBeneficioProgramaExercicio4)
                       {
                           beneficiosEventuaisExercicio4 += item.PrevisaoOrcamentaria;
                           beneficiosEventuaisReprogramadoExercicio4 += item.RecursoReprogramadoAnoAnterior;
                       }

                    #endregion


                    //DBM: VERIFICAR
                    //var valorBeneficioEventualExercicio1 = cofinanciamentosBeneficioProgramaExercicio1.Sum(x => 0);

                    var valorBeneficioProgramaExercicio1 = cofinanciamentosBeneficioProgramaExercicio1 !=null  ? cofinanciamentosBeneficioProgramaExercicio1.Sum(x => x.PrevisaoOrcamentaria) : 0;
                    var valorBeneficioProgramaExercicio2 = cofinanciamentosBeneficioProgramaExercicio2 !=null  ? cofinanciamentosBeneficioProgramaExercicio2.Sum(x => x.PrevisaoOrcamentaria) : 0;
                    var valorBeneficioProgramaExercicio3 = cofinanciamentosBeneficioProgramaExercicio3 != null ? cofinanciamentosBeneficioProgramaExercicio3.Sum(x => x.PrevisaoOrcamentaria) : 0;
                    var valorBeneficioProgramaExercicio4 = cofinanciamentosBeneficioProgramaExercicio4 != null ? cofinanciamentosBeneficioProgramaExercicio4.Sum(x => x.PrevisaoOrcamentaria) : 0;

                    #endregion

                    #region Exercicio 1

                    #region Do sistema - bloco 5 - exercicio 1



                    Decimal valorBasicaExercicio1 = cofinanciamentosExercicio1.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio1.ValorProtecaoSocialBasica.Value : 0M;
                    Decimal valorMediaExercicio1 = cofinanciamentosExercicio1.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio1.ValorProtecaoSocialMediaComplexidade.Value : 0M;
                    Decimal valorAltaExercicio1 = cofinanciamentosExercicio1.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio1.ValorProtecaoSocialAltaComplexidade.Value : 0M;

                    Decimal valorBasicaReprogramadoExercicio1 = cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialBasicaReprogramado.Value : 0M;
                    Decimal valorMediaReprogramadoExercicio1 = cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialMediaReprogramado.Value : 0M;
                    Decimal valorAltaReprogramadoExercicio1 = cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialAltaReprogramado.Value : 0M;

                    Decimal valorBasicaDemandasExercicio1 = cofinanciamentosDemandasExercicio1.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialBasicaDemandas.Value : 0M;
                    Decimal valorMediaDemandasExercicio1 = cofinanciamentosDemandasExercicio1.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialMediaDemandas.Value : 0M;
                    Decimal valorAltaDemandasExercicio1 = cofinanciamentosDemandasExercicio1.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialAltaDemandas.Value : 0M;

                    Decimal valorBeneficiosEventuaisDemandasExercicio1 = cofinanciamentosDemandasExercicio1.ValorBeneficioEventuaisDemandas != null ? cofinanciamentosDemandasExercicio1.ValorBeneficioEventuaisDemandas.Value : 0M;

                    Decimal valorBasicaReprogramadoDemandasExercicio1 = cofinanciamentosReprogramadosDemandasExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M;
                    Decimal valorMediaReprogramadoDemandasExercicio1 = cofinanciamentosReprogramadosDemandasExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M;
                    Decimal valorAltaReprogramadoDemandasExercicio1 = cofinanciamentosReprogramadosDemandasExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M;
                    Decimal valorBeneficiosEventuaisDemandasReprogramadoExercicio1 = cofinanciamentosReprogramadosDemandasExercicio1.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio1.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M;
                    

                    //Decimal valorBeneficioEventualExercicio1 = cofinanciamentosBeneficioProgramaExercicio1.ValorBeneficiosEventuais != null ? cofinanciamentosBeneficioProgramaExercicio1.ValorBeneficiosEventuais.Value : 0M;
                    //Decimal valorBeneficioProgramaExercicio1 = cofinanciamentosBeneficioProgramaExercicio1.ValorProgramaProjeto != null ? cofinanciamentosBeneficioProgramaExercicio1.ValorProgramaProjeto.Value : 0M;
                    #endregion

                    #region informado manualmente - exercicio 1
                    Decimal totalProtecaoBasicaExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialBasica.Text) ? txtProtecaoSocialBasica.Text : "0,00");
                    Decimal totalProtecaoBasicaReprogramadoExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaReprogramado.Text) ? txtProtecaoBasicaReprogramado.Text : "0,00");

                    Decimal totalProtecaoMediaExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialMedia.Text) ? txtProtecaoSocialMedia.Text : "0,00");
                    Decimal totalProtecaoMediaReprogramadoExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaReprogramado.Text) ? txtProtecaoMediaReprogramado.Text : "0,00");

                    Decimal totalProtecaoAltaExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialAlta.Text) ? txtProtecaoSocialAlta.Text : "0,00");
                    Decimal totalProtecaoAltaReprogramadoExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaReprogramado.Text) ? txtProtecaoAltaReprogramado.Text : "0,00");

                    Decimal totalBeneficiosEventuaisExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuais.Text) ? txtBeneficiosEventuais.Text : "0,00");
                    Decimal totalBeneficiosEventuaisReprogramadoExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisReprogramado.Text) ? txtBeneficiosEventuaisReprogramado.Text : "0,00");

                    Decimal totalProgramaProjetoExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtSaoPauloSolidario.Text) ? txtSaoPauloSolidario.Text : "0,00");
                    Decimal totalProgramaProjetoReprogramadoExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtSaoPauloSolidarioReprogramado.Text) ? txtSaoPauloSolidarioReprogramado.Text : "0,00");

                    Decimal totalDemandasProtecaoBasicaExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaDemandas.Text) ? txtProtecaoBasicaDemandas.Text : "0,00");
                    Decimal totalDemandasProtecaoBasicaReprogramacaoExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaReprogramacaoDemandas.Text) ? txtProtecaoBasicaReprogramacaoDemandas.Text : "0,00");

                    Decimal totalDemandasProtecaoMediaExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaDemandas.Text) ? txtProtecaoMediaDemandas.Text : "0,00");
                    Decimal totalDemandasProtecaoMediaReprogramacaoExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaReprogramacaoDemandas.Text) ? txtProtecaoMediaReprogramacaoDemandas.Text : "0,00");

                    Decimal totalDemandasProtecaoAltaExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaDemandas.Text) ? txtProtecaoAltaDemandas.Text : "0,00");
                    Decimal totalDemandasProtecaoAltaReprogramacaoExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaReprogramacaoDemandas.Text) ? txtProtecaoAltaReprogramacaoDemandas.Text : "0,00");

                    Decimal totalDemandasBeneficiosEventuaisExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisDemandas.Text) ? txtBeneficiosEventuaisDemandas.Text : "0,00");

                    Decimal totalDemandasBeneficiosEventuaisReprogramacaoExercicio1 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisDemandasReprogramado.Text) ? txtBeneficiosEventuaisDemandasReprogramado.Text : "0,00");

                    #endregion informado manualmente - exercicio 1

                    #region comparar [dados do bloco 5 vs informados manualmente - ex 1]

                    if (ExercicioLiberado2022)
                    {
                        String erroValores2022 = String.Empty;


                        if (valorBasicaExercicio1 != totalProtecaoBasicaExercicio1)
                        {
                            erroValores2022 = (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da proteção Social Básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (valorMediaExercicio1 != totalProtecaoMediaExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da proteção Social Média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (valorAltaExercicio1 != totalProtecaoAltaExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da proteção Social Alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (beneficiosEventuaisExercicio1 != totalBeneficiosEventuaisExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor dos beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (programaProjetoExercicio1 != totalProgramaProjetoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da proteção Social Programa Projeto não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                           
                        }



                        if (valorBasicaReprogramadoExercicio1 != totalProtecaoBasicaReprogramadoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da proteção Social Básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (valorMediaReprogramadoExercicio1 != totalProtecaoMediaReprogramadoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da proteção Social Média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (valorAltaReprogramadoExercicio1 != totalProtecaoAltaReprogramadoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da proteção Social Alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (beneficiosEventuaisReprogramadoExercicio1 != totalBeneficiosEventuaisReprogramadoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor dos beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (programaProjetoReprogramadoExercicio1 != totalProgramaProjetoReprogramadoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da proteção Social Programa Projeto não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }



                        if (valorBasicaDemandasExercicio1 != totalDemandasProtecaoBasicaExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (valorBasicaReprogramadoDemandasExercicio1 != totalDemandasProtecaoBasicaReprogramacaoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }


                        if (valorMediaDemandasExercicio1 != totalDemandasProtecaoMediaExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (valorMediaReprogramadoDemandasExercicio1 != totalDemandasProtecaoMediaReprogramacaoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social media não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }


                        if (valorAltaDemandasExercicio1 != totalDemandasProtecaoAltaExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (valorBeneficiosEventuaisDemandasExercicio1 != totalDemandasBeneficiosEventuaisExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor das demandas beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }
                        
                        if (valorAltaReprogramadoDemandasExercicio1 != totalDemandasProtecaoAltaReprogramacaoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);
                            
                        }

                        if (valorBeneficiosEventuaisDemandasReprogramadoExercicio1 != totalDemandasBeneficiosEventuaisReprogramacaoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor reprogramado das demandas beneficios eventuais  não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);

                        }

                        if (!String.IsNullOrEmpty(erroValores2022))
                        {
                            return false;
                        }
                        
                        var totalGeralProtecoesExercicio1 = totalProtecaoBasicaExercicio1 + totalProtecaoMediaExercicio1 + totalProtecaoAltaExercicio1 + totalProgramaProjetoExercicio1 + totalBeneficiosEventuaisExercicio1;

                        var totalGeralReprogramacaoExercicio1 = totalProtecaoBasicaReprogramadoExercicio1 + totalProtecaoMediaReprogramadoExercicio1 + totalProtecaoAltaReprogramadoExercicio1 + totalBeneficiosEventuaisReprogramadoExercicio1;

                        var totalGeralDemandasExercicio1 = totalDemandasProtecaoBasicaExercicio1 + totalDemandasProtecaoMediaExercicio1 + totalDemandasProtecaoAltaExercicio1 + totalDemandasBeneficiosEventuaisExercicio1;

                        var totalGeralDemandasReprogramacaoExercicio1 = totalDemandasProtecaoBasicaReprogramacaoExercicio1 + totalDemandasProtecaoMediaReprogramacaoExercicio1 + totalDemandasProtecaoAltaReprogramacaoExercicio1 + totalDemandasBeneficiosEventuaisReprogramacaoExercicio1;


                        var valorGeralProtecoesExercicio1 = valorBasicaExercicio1 + valorMediaExercicio1 + valorAltaExercicio1 + programaProjetoExercicio1 + beneficiosEventuaisExercicio1;

                        var valorGeralReprogramacaoExercicio1 = valorBasicaReprogramadoExercicio1 + valorMediaReprogramadoExercicio1 + valorAltaReprogramadoExercicio1 + beneficiosEventuaisReprogramadoExercicio1;

                        var valorGeralDemandasExercicio1 = valorBasicaDemandasExercicio1 + valorMediaDemandasExercicio1 + valorAltaDemandasExercicio1 + valorBeneficiosEventuaisDemandasExercicio1;

                        var valorGeralDemandasReprogramacaoExercicio1 = valorBasicaReprogramadoDemandasExercicio1 + valorMediaReprogramadoDemandasExercicio1 + valorAltaReprogramadoDemandasExercicio1 + valorBeneficiosEventuaisDemandasReprogramadoExercicio1;


                        if (totalGeralProtecoesExercicio1 != valorGeralProtecoesExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor total do repasse no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);

                            return false;
                        }

                        if (totalGeralReprogramacaoExercicio1 != valorGeralReprogramacaoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor total reprogramado no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", "2021");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);

                            return false;
                        }


                        if (totalGeralDemandasExercicio1 != valorGeralDemandasExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor total das demandas no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);

                            return false;
                        }


                        if (totalGeralDemandasReprogramacaoExercicio1 != valorGeralDemandasReprogramacaoExercicio1)
                        {
                            erroValores2022 += (String.IsNullOrEmpty(erroValores2022) ? "" : "<br/>") + String.Format("O valor total reprogramado das demandas no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", "2021");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2022), true);

                            return false;
                        }



                        /*if (Convert.ToDecimal(hidTotalRecursos.Value) != Convert.ToDecimal(lblValorCofinanciamento.Text)
                           || valorBasicaExercicio1 != totalProtecaoBasicaExercicio1
                           || valorMediaExercicio1 != totalProtecaoMediaExercicio1
                           || valorAltaExercicio1 != totalProtecaoAltaExercicio1
                           || valorBeneficioProgramaExercicio1 != totalProgramaProjetoExercicio1
                           //|| valorBeneficioEventualExercicio1 != totalBeneficiosEventuaisExercicio1 DBM: VERIFICAR 
                            //----------------Reprogramado
                           || valorBasicaReprogramadoExercicio1 != totalProtecaoBasicaReprogramadoExercicio1
                           || valorMediaReprogramadoExercicio1 != totalProtecaoMediaReprogramadoExercicio1
                           || valorAltaReprogramadoExercicio1 != totalProtecaoAltaReprogramadoExercicio1
                             )
                        {
                            String ErroValores = String.Format("O valor total do cofinanciamento não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[0].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(ErroValores), true);

                            return false;
                        }*/
                    }
                    #endregion
                    #endregion

                    #region exercicio 2
                    #region Do sistema - bloco 5 - exercicio 2
                    Decimal valorBasicaExercicio2 = cofinanciamentosExercicio2.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio2.ValorProtecaoSocialBasica.Value : 0M;
                    Decimal valorMediaExercicio2 = cofinanciamentosExercicio2.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio2.ValorProtecaoSocialMediaComplexidade.Value : 0M;
                    Decimal valorAltaExercicio2 = cofinanciamentosExercicio2.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio2.ValorProtecaoSocialAltaComplexidade.Value : 0M;

                    Decimal valorBasicaReprogramadoExercicio2 = cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialBasicaReprogramado.Value : 0M;
                    Decimal valorMediaReprogramadoExercicio2 = cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialMediaReprogramado.Value : 0M;
                    Decimal valorAltaReprogramadoExercicio2 = cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialAltaReprogramado.Value : 0M;

                    Decimal valorBasicaDemandasExercicio2 = cofinanciamentosDemandasExercicio2.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialBasicaDemandas.Value : 0M;
                    Decimal valorMediaDemandasExercicio2 = cofinanciamentosDemandasExercicio2.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialMediaDemandas.Value : 0M;
                    Decimal valorAltaDemandasExercicio2 = cofinanciamentosDemandasExercicio2.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialAltaDemandas.Value : 0M;

                    Decimal valorBeneficiosEventuaisDemandasExercicio2 = cofinanciamentosDemandasExercicio2.ValorBeneficioEventuaisDemandas != null ? cofinanciamentosDemandasExercicio2.ValorBeneficioEventuaisDemandas.Value : 0M;

                    Decimal valorBasicaReprogramadoDemandasExercicio2 = cofinanciamentosReprogramadosDemandasExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M;
                    Decimal valorMediaReprogramadoDemandasExercicio2 = cofinanciamentosReprogramadosDemandasExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M;
                    Decimal valorAltaReprogramadoDemandasExercicio2 = cofinanciamentosReprogramadosDemandasExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M;
                    Decimal valorBeneficiosEventuaisReprogramadoDemandasExercicio2 = cofinanciamentosReprogramadosDemandasExercicio2.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio2.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M;      
                    #endregion

                    #region informado manualmente - exercicio2
                    Decimal totalProtecaoBasicaExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialBasicaExercicio2.Text) ? txtProtecaoSocialBasicaExercicio2.Text : "0,00");
                    Decimal totalProtecaoBasicaReprogramadoExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaReprogramadoExercicio2.Text) ? txtProtecaoBasicaReprogramadoExercicio2.Text : "0,00");

                    Decimal totalProtecaoMediaExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialMediaExercicio2.Text) ? txtProtecaoSocialMediaExercicio2.Text : "0,00");
                    Decimal totalProtecaoMediaReprogramadoExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaReprogramadoExercicio2.Text) ? txtProtecaoMediaReprogramadoExercicio2.Text : "0,00");

                    Decimal totalProtecaoAltaExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialAltaExercicio2.Text) ? txtProtecaoSocialAltaExercicio2.Text : "0,00");
                    Decimal totalProtecaoAltaReprogramadoExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaReprogramadoExercicio2.Text) ? txtProtecaoAltaReprogramadoExercicio2.Text : "0,00");

                    Decimal totalBeneficiosEventuaisExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisExercicio2.Text) ? txtBeneficiosEventuaisExercicio2.Text : "0,00");
                    Decimal totalBeneficiosEventuaisReprogramadoExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisReprogramadoExercicio2.Text) ? txtBeneficiosEventuaisReprogramadoExercicio2.Text : "0,00");

                    Decimal totalProgramaProjetoExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtSaoPauloSolidarioExercicio2.Text) ? txtSaoPauloSolidarioExercicio2.Text : "0,00");
                    Decimal totalProgramaProjetoReprogramadoExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtSaoPauloSolidarioReprogramadoExercicio2.Text) ? txtSaoPauloSolidarioReprogramadoExercicio2.Text : "0,00");

                    Decimal totalDemandasProtecaoBasicaExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaDemandasExercicio2.Text) ? txtProtecaoBasicaDemandasExercicio2.Text : "0,00");
                    Decimal totalDemandasProtecaoBasicaReprogramacaoExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaReprogramacaoDemandasExercicio2.Text) ? txtProtecaoBasicaReprogramacaoDemandasExercicio2.Text : "0,00");

                    Decimal totalDemandasProtecaoMediaExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaDemandasExercicio2.Text) ? txtProtecaoMediaDemandasExercicio2.Text : "0,00");
                    Decimal totalDemandasProtecaoMediaReprogramacaoExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaReprogramacaoDemandasExercicio2.Text) ? txtProtecaoMediaReprogramacaoDemandasExercicio2.Text : "0,00");

                    Decimal totalDemandasProtecaoAltaExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaDemandasExercicio2.Text) ? txtProtecaoAltaDemandasExercicio2.Text : "0,00");
                    Decimal totalDemandasProtecaoAltaReprogramacaoExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaReprogramacaoDemandasExercicio2.Text) ? txtProtecaoAltaReprogramacaoDemandasExercicio2.Text : "0,00");

                    Decimal totalDemandasBeneficiosEventuaisExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisDemandasExercicio2.Text) ? txtBeneficiosEventuaisDemandasExercicio2.Text : "0,00");

                    Decimal totalDemandasBeneficiosEventuaisReprogramacaoExercicio2 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisDemandasReprogramadoExercicio2.Text) ? txtBeneficiosEventuaisDemandasReprogramadoExercicio2.Text : "0,00");


                    #endregion informado manualmente - exercicio2

                    #region comparar [dados do bloco 5 vs informados manualmente - ex 2]
                    if (ExercicioLiberado2023)
                    {                        
                        String erroValores2023 = String.Empty;

                        if (valorBasicaExercicio2 != totalProtecaoBasicaExercicio2)
                        {
                            erroValores2023 = (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da proteção Social Básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);
                            
                           
                        }

                        if (valorMediaExercicio2 != totalProtecaoMediaExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da proteção Social Média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);
                            
                          
                        }

                        if (valorAltaExercicio2 != totalProtecaoAltaExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da proteção Social Alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);
                            
                            
                        }

                        if (beneficiosEventuaisExercicio2 != totalBeneficiosEventuaisExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor dos beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);
                            
                           
                        }

                        if (programaProjetoExercicio2 != totalProgramaProjetoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da proteção Social Programa Projeto não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);
                        }



                        if (valorBasicaReprogramadoExercicio2 != totalProtecaoBasicaReprogramadoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da proteção Social Básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);
                            
                            
                        }

                        if (valorMediaReprogramadoExercicio2 != totalProtecaoMediaReprogramadoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da proteção Social Média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                           
                        }

                        if (valorAltaReprogramadoExercicio2 != totalProtecaoAltaReprogramadoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da proteção Social Alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                            
                        }

                        if (beneficiosEventuaisReprogramadoExercicio2 != totalBeneficiosEventuaisReprogramadoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor dos beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                           
                        }

                        if (programaProjetoReprogramadoExercicio2 != totalProgramaProjetoReprogramadoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da proteção Social Programa Projeto não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                            
                        }



                        if (valorBasicaDemandasExercicio2 != totalDemandasProtecaoBasicaExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                           
                        }

                        if (valorBasicaReprogramadoDemandasExercicio2 != totalDemandasProtecaoBasicaReprogramacaoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                            
                        }


                        if (valorMediaDemandasExercicio2 != totalDemandasProtecaoMediaExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                          
                        }

                        if (valorMediaReprogramadoDemandasExercicio2 != totalDemandasProtecaoMediaReprogramacaoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social media não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                            
                        }


                        if (valorAltaDemandasExercicio2 != totalDemandasProtecaoAltaExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                           
                        }

                        if (valorBeneficiosEventuaisDemandasExercicio2 != totalDemandasBeneficiosEventuaisExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor das demandas beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                            
                        }

                        if (valorAltaReprogramadoDemandasExercicio2 != totalDemandasProtecaoAltaReprogramacaoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                            
                        }


                        if (valorBeneficiosEventuaisReprogramadoDemandasExercicio2 != totalDemandasBeneficiosEventuaisReprogramacaoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor reprogramado das demandas beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);


                        }


                        if (!String.IsNullOrEmpty(erroValores2023))
                        {
                            return false;
                        }


                       var totalGeralProtecoesExercicio2 = totalProtecaoBasicaExercicio2 + totalProtecaoMediaExercicio2 + totalProtecaoAltaExercicio2 + totalProgramaProjetoExercicio2 + totalBeneficiosEventuaisExercicio2;
                        
                        var totalGeralReprogramacaoExercicio2 = totalProtecaoBasicaReprogramadoExercicio2 + totalProtecaoMediaReprogramadoExercicio2 + totalProtecaoAltaReprogramadoExercicio2 + totalBeneficiosEventuaisReprogramadoExercicio2;
                        
                        var totalGeralDemandasExercicio2 = totalDemandasProtecaoBasicaExercicio2 + totalDemandasProtecaoMediaExercicio2 + totalDemandasProtecaoAltaExercicio2 + totalDemandasBeneficiosEventuaisExercicio2;

                        var totalGeralDemandasReprogramacaoExercicio2 = totalDemandasProtecaoBasicaReprogramacaoExercicio2 + totalDemandasProtecaoMediaReprogramacaoExercicio2 + totalDemandasProtecaoAltaReprogramacaoExercicio2 + totalDemandasBeneficiosEventuaisReprogramacaoExercicio2;
                       
                       
                        var valorGeralProtecoesExercicio2 = valorBasicaExercicio2 + valorMediaExercicio2 + valorAltaExercicio2 + programaProjetoExercicio2 + beneficiosEventuaisExercicio2;

                        var valorGeralReprogramacaoExercicio2 = valorBasicaReprogramadoExercicio2 + valorMediaReprogramadoExercicio2 + valorAltaReprogramadoExercicio2 + beneficiosEventuaisReprogramadoExercicio2;

                        var valorGeralDemandasExercicio2 = valorBasicaDemandasExercicio2 + valorMediaDemandasExercicio2 + valorAltaDemandasExercicio2 + valorBeneficiosEventuaisDemandasExercicio2;

                        var valorGeralDemandasReprogramacaoExercicio2 = valorBasicaReprogramadoDemandasExercicio2 + valorMediaReprogramadoDemandasExercicio2 + valorAltaReprogramadoDemandasExercicio2 + valorBeneficiosEventuaisReprogramadoDemandasExercicio2;


                        if (totalGeralProtecoesExercicio2 != valorGeralProtecoesExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor total do repasse no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                            return false;
                        }

                        if (totalGeralReprogramacaoExercicio2 != valorGeralReprogramacaoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor total reprogramado no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                            return false;
                        }


                        if (totalGeralDemandasExercicio2 != valorGeralDemandasExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor total das demandas no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                            return false;
                        }


                        if (totalGeralDemandasReprogramacaoExercicio2 != valorGeralDemandasReprogramacaoExercicio2)
                        {
                            erroValores2023 += (String.IsNullOrEmpty(erroValores2023) ? "" : "<br/>") + String.Format("O valor total reprogramado das demandas no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2023), true);

                            return false;
                        }



                        /*if (Convert.ToDecimal(hidTotalRecursosExercicio2.Value) != Convert.ToDecimal(lblValorCofinanciamentoExercicio2.Text)
                           || valorBasicaExercicio2 != totalProtecaoBasicaExercicio2
                           || valorMediaExercicio2 != totalProtecaoMediaExercicio2
                           || valorAltaExercicio2 != totalProtecaoAltaExercicio2
                           || valorBeneficioProgramaExercicio2 != totalProgramaProjetoExercicio2


                           || valorBasicaReprogramadoExercicio2 != totalProtecaoBasicaReprogramadoExercicio2
                           || valorMediaReprogramadoExercicio2 != totalProtecaoMediaReprogramadoExercicio2
                           || valorAltaReprogramadoExercicio2 != totalProtecaoAltaReprogramadoExercicio2
                             )
                        {
                            String ErroValores = String.Format("O valor total do cofinanciamento não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(ErroValores), true);

                            return false;
                        }*/
                    }
                    #endregion

                    #endregion

                    #region exercicio 3
                    #region Do sistema - bloco 5 - exercicio 3
                    Decimal valorBasicaExercicio3 = cofinanciamentosExercicio3.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio3.ValorProtecaoSocialBasica.Value : 0M;
                    Decimal valorMediaExercicio3 = cofinanciamentosExercicio3.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio3.ValorProtecaoSocialMediaComplexidade.Value : 0M;
                    Decimal valorAltaExercicio3 = cofinanciamentosExercicio3.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio3.ValorProtecaoSocialAltaComplexidade.Value : 0M;

                    Decimal valorBasicaReprogramadoExercicio3 = cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialBasicaReprogramado.Value : 0M;
                    Decimal valorMediaReprogramadoExercicio3 = cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialMediaReprogramado.Value : 0M;
                    Decimal valorAltaReprogramadoExercicio3 = cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialAltaReprogramado.Value : 0M;

                    Decimal valorBasicaDemandasExercicio3 = cofinanciamentosDemandasExercicio3.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialBasicaDemandas.Value : 0M;
                    Decimal valorMediaDemandasExercicio3 = cofinanciamentosDemandasExercicio3.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialMediaDemandas.Value : 0M;
                    Decimal valorAltaDemandasExercicio3 = cofinanciamentosDemandasExercicio3.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialAltaDemandas.Value : 0M;

                    Decimal valorBeneficiosEventuaisDemandasExercicio3 = cofinanciamentosDemandasExercicio3.ValorBeneficioEventuaisDemandas != null ? cofinanciamentosDemandasExercicio3.ValorBeneficioEventuaisDemandas.Value : 0M;

                    Decimal valorBasicaReprogramadoDemandasExercicio3 = cofinanciamentosReprogramadosDemandasExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M;
                    Decimal valorMediaReprogramadoDemandasExercicio3 = cofinanciamentosReprogramadosDemandasExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M;
                    Decimal valorAltaReprogramadoDemandasExercicio3 = cofinanciamentosReprogramadosDemandasExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M;
                    Decimal valorBeneficiosEventuaisReprogramadoDemandasExercicio3 = cofinanciamentosReprogramadosDemandasExercicio3.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio3.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M;      

                    #endregion

                    #region informado manualmente - exercicio3
                    Decimal totalProtecaoBasicaExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialBasicaExercicio3.Text) ? txtProtecaoSocialBasicaExercicio3.Text : "0,00");
                    Decimal totalProtecaoBasicaReprogramadoExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaReprogramadoExercicio3.Text) ? txtProtecaoBasicaReprogramadoExercicio3.Text : "0,00");

                    Decimal totalProtecaoMediaExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialMediaExercicio3.Text) ? txtProtecaoSocialMediaExercicio3.Text : "0,00");
                    Decimal totalProtecaoMediaReprogramadoExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaReprogramadoExercicio3.Text) ? txtProtecaoMediaReprogramadoExercicio3.Text : "0,00");

                    Decimal totalProtecaoAltaExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialAltaExercicio3.Text) ? txtProtecaoSocialAltaExercicio3.Text : "0,00");
                    Decimal totalProtecaoAltaReprogramadoExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaReprogramadoExercicio3.Text) ? txtProtecaoAltaReprogramadoExercicio3.Text : "0,00");

                    Decimal totalBeneficiosEventuaisExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisExercicio3.Text) ? txtBeneficiosEventuaisExercicio3.Text : "0,00");
                    Decimal totalBeneficiosEventuaisReprogramadoExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisReprogramadoExercicio3.Text) ? txtBeneficiosEventuaisReprogramadoExercicio3.Text : "0,00");

                    Decimal totalProgramaProjetoExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtSaoPauloSolidarioExercicio3.Text) ? txtSaoPauloSolidarioExercicio3.Text : "0,00");
                    Decimal totalProgramaProjetoReprogramadoExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtSaoPauloSolidarioReprogramadoExercicio3.Text) ? txtSaoPauloSolidarioReprogramadoExercicio3.Text : "0,00");

                    Decimal totalDemandasProtecaoBasicaExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaDemandasExercicio3.Text) ? txtProtecaoBasicaDemandasExercicio3.Text : "0,00");
                    Decimal totalDemandasProtecaoBasicaReprogramacaoExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaReprogramacaoDemandasExercicio3.Text) ? txtProtecaoBasicaReprogramacaoDemandasExercicio3.Text : "0,00");

                    Decimal totalDemandasProtecaoMediaExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaDemandasExercicio3.Text) ? txtProtecaoMediaDemandasExercicio3.Text : "0,00");
                    Decimal totalDemandasProtecaoMediaReprogramacaoExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaReprogramacaoDemandasExercicio3.Text) ? txtProtecaoMediaReprogramacaoDemandasExercicio3.Text : "0,00");

                    Decimal totalDemandasProtecaoAltaExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaDemandasExercicio3.Text) ? txtProtecaoAltaDemandasExercicio3.Text : "0,00");
                    Decimal totalDemandasProtecaoAltaReprogramacaoExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaReprogramacaoDemandasExercicio3.Text) ? txtProtecaoAltaReprogramacaoDemandasExercicio3.Text : "0,00");

                    Decimal totalDemandasBeneficiosEventuaisExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisDemandasExercicio3.Text) ? txtBeneficiosEventuaisDemandasExercicio3.Text : "0,00");

                    Decimal totalDemandasBeneficiosEventuaisReprogramacaoExercicio3 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisDemandasReprogramadoExercicio3.Text) ? txtBeneficiosEventuaisDemandasReprogramadoExercicio3.Text : "0,00");


                    #endregion informado manualmente - exercicio3

                    #region comparar [dados do bloco 5 vs informados manualmente - ex 3]
                    if (ExercicioLiberado2024)
                    {


                        String erroValores2024 = String.Empty;

                        if (valorBasicaExercicio3 != totalProtecaoBasicaExercicio3)
                        {
                            erroValores2024 = (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor da proteção Social Básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                         
                        }

                        if (valorMediaExercicio3 != totalProtecaoMediaExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor da proteção Social Média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                          
                        }

                        if (valorAltaExercicio3 != totalProtecaoAltaExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor da proteção Social Alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                        }

                        if (beneficiosEventuaisExercicio3 != totalBeneficiosEventuaisExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor dos beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);


                        }

                        if (programaProjetoExercicio3 != totalProgramaProjetoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor da proteção Social Programa Projeto não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                       
                        }



                        if (valorBasicaReprogramadoExercicio3 != totalProtecaoBasicaReprogramadoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor reprogramado da proteção Social Básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                         
                        }

                        if (valorMediaReprogramadoExercicio3 != totalProtecaoMediaReprogramadoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor reprogramado da proteção Social Média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                           
                        }

                        if (valorAltaReprogramadoExercicio3 != totalProtecaoAltaReprogramadoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor reprogramado da proteção Social Alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                           
                        }

                        if (beneficiosEventuaisReprogramadoExercicio3 != totalBeneficiosEventuaisReprogramadoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor reprogramado dos beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                      
                        }

                        if (programaProjetoReprogramadoExercicio3 != totalProgramaProjetoReprogramadoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor reprogramado da proteção Social Programa Projeto não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                        }



                        if (valorBasicaDemandasExercicio3 != totalDemandasProtecaoBasicaExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                        }

                        if (valorBasicaReprogramadoDemandasExercicio3 != totalDemandasProtecaoBasicaReprogramacaoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                        }


                        if (valorMediaDemandasExercicio3 != totalDemandasProtecaoMediaExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                        }

                        if (valorMediaReprogramadoDemandasExercicio3 != totalDemandasProtecaoMediaReprogramacaoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social media não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                        }


                        if (valorAltaDemandasExercicio3 != totalDemandasProtecaoAltaExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                        }

                        if (valorBeneficiosEventuaisDemandasExercicio3 != totalDemandasBeneficiosEventuaisExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor das demandas beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                        }

                        if (valorAltaReprogramadoDemandasExercicio3 != totalDemandasProtecaoAltaReprogramacaoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                        }


                        if (valorBeneficiosEventuaisReprogramadoDemandasExercicio3 != totalDemandasBeneficiosEventuaisReprogramacaoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor reprogramado das demandas beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                        }


                        if (!String.IsNullOrEmpty(erroValores2024))
                        {
                            return false;
                        }



                        var totalGeralProtecoesExercicio3 = totalProtecaoBasicaExercicio3 + totalProtecaoMediaExercicio3 + totalProtecaoAltaExercicio3 + totalProgramaProjetoExercicio3 + totalBeneficiosEventuaisExercicio3;

                        var totalGeralReprogramacaoExercicio3 = totalProtecaoBasicaReprogramadoExercicio3 + totalProtecaoMediaReprogramadoExercicio3 + totalProtecaoAltaReprogramadoExercicio3 + totalBeneficiosEventuaisReprogramadoExercicio3;

                        var totalGeralDemandasExercicio3 = totalDemandasProtecaoBasicaExercicio3 + totalDemandasProtecaoMediaExercicio3 + totalDemandasProtecaoAltaExercicio3 + totalDemandasBeneficiosEventuaisExercicio3;

                        var totalGeralDemandasReprogramacaoExercicio3 = totalDemandasProtecaoBasicaReprogramacaoExercicio3 + totalDemandasProtecaoMediaReprogramacaoExercicio3 + totalDemandasProtecaoAltaReprogramacaoExercicio3 + totalDemandasBeneficiosEventuaisReprogramacaoExercicio3;


                        var valorGeralProtecoesExercicio3 = valorBasicaExercicio3 + valorMediaExercicio3 + valorAltaExercicio3 + programaProjetoExercicio3 + beneficiosEventuaisExercicio3;

                        var valorGeralReprogramacaoExercicio3 = valorBasicaReprogramadoExercicio3 + valorMediaReprogramadoExercicio3 + valorAltaReprogramadoExercicio3 + beneficiosEventuaisReprogramadoExercicio3;

                        var valorGeralDemandasExercicio3 = valorBasicaDemandasExercicio3 + valorMediaDemandasExercicio3 + valorAltaDemandasExercicio3 + valorBeneficiosEventuaisDemandasExercicio3;

                        var valorGeralDemandasReprogramacaoExercicio3 = valorBasicaReprogramadoDemandasExercicio3 + valorMediaReprogramadoDemandasExercicio3 + valorAltaReprogramadoDemandasExercicio3 + valorBeneficiosEventuaisReprogramadoDemandasExercicio3;


                        if (totalGeralProtecoesExercicio3 != valorGeralProtecoesExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor total do repasse no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                            return false;
                        }

                        if (totalGeralReprogramacaoExercicio3 != valorGeralReprogramacaoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor total reprogramado no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                            return false;
                        }


                        if (totalGeralDemandasExercicio3 != valorGeralDemandasExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor total das demandas no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                            return false;
                        }


                        if (totalGeralDemandasReprogramacaoExercicio3 != valorGeralDemandasReprogramacaoExercicio3)
                        {
                            erroValores2024 += (String.IsNullOrEmpty(erroValores2024) ? "" : "<br/>") + String.Format("O valor total reprogramado das demandas no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2024), true);

                            return false;
                        }




                           /* if (Convert.ToDecimal(hidTotalRecursosExercicio3.Value) != Convert.ToDecimal(lblValorCofinanciamentoExercicio3.Text)
                               || valorBasicaExercicio3 != totalProtecaoBasicaExercicio3
                               || valorMediaExercicio3 != totalProtecaoMediaExercicio3
                               || valorAltaExercicio3 != totalProtecaoAltaExercicio3
                               || valorBeneficioProgramaExercicio3 != totalProgramaProjetoExercicio3
                                //|| valorBeneficioEventualExercicio2 != totalBeneficiosEventuaisExercicio2
                                //----------------Reprogramado
                               || valorBasicaReprogramadoExercicio3 != totalProtecaoBasicaReprogramadoExercicio3
                               || valorMediaReprogramadoExercicio3 != totalProtecaoMediaReprogramadoExercicio3
                               || valorAltaReprogramadoExercicio3 != totalProtecaoAltaReprogramadoExercicio3
                                 )
                            {
                                String ErroValores = String.Format("O valor total do cofinanciamento não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[2].ToString());
                                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(ErroValores), true);

                                return false;
                            }*/
                    }
                    #endregion

                    #endregion

                    #region exercicio 4
                    #region Do sistema - bloco 5 - exercicio 4
                    Decimal valorBasicaExercicio4 = cofinanciamentosExercicio4.ValorProtecaoSocialBasica != null ? cofinanciamentosExercicio4.ValorProtecaoSocialBasica.Value : 0M;
                    Decimal valorMediaExercicio4 = cofinanciamentosExercicio4.ValorProtecaoSocialMediaComplexidade != null ? cofinanciamentosExercicio4.ValorProtecaoSocialMediaComplexidade.Value : 0M;
                    Decimal valorAltaExercicio4 = cofinanciamentosExercicio4.ValorProtecaoSocialAltaComplexidade != null ? cofinanciamentosExercicio4.ValorProtecaoSocialAltaComplexidade.Value : 0M;

                    Decimal valorBasicaReprogramadoExercicio4 = cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialBasicaReprogramado.Value : 0M;
                    Decimal valorMediaReprogramadoExercicio4 = cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialMediaReprogramado.Value : 0M;
                    Decimal valorAltaReprogramadoExercicio4 = cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialAltaReprogramado.Value : 0M;

                    Decimal valorBasicaDemandasExercicio4 = cofinanciamentosDemandasExercicio4.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialBasicaDemandas.Value : 0M;
                    Decimal valorMediaDemandasExercicio4 = cofinanciamentosDemandasExercicio4.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialMediaDemandas.Value : 0M;
                    Decimal valorAltaDemandasExercicio4 = cofinanciamentosDemandasExercicio4.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialAltaDemandas.Value : 0M;

                    Decimal valorBeneficiosEventuaisDemandasExercicio4 = cofinanciamentosDemandasExercicio4.ValorBeneficioEventuaisDemandas != null ? cofinanciamentosDemandasExercicio4.ValorBeneficioEventuaisDemandas.Value : 0M;

                    Decimal valorBasicaReprogramadoDemandasExercicio4 = cofinanciamentosReprogramadosDemandasExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M;
                    Decimal valorMediaReprogramadoDemandasExercicio4 = cofinanciamentosReprogramadosDemandasExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M;
                    Decimal valorAltaReprogramadoDemandasExercicio4 = cofinanciamentosReprogramadosDemandasExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M;
                    Decimal valorBeneficiosEventuaisReprogramadoDemandasExercicio4 = cofinanciamentosReprogramadosDemandasExercicio4.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosReprogramadosDemandasExercicio4.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M;      
                    #endregion

                    #region informado manualmente - exercicio4
                    Decimal totalProtecaoBasicaExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialBasicaExercicio4.Text) ? txtProtecaoSocialBasicaExercicio4.Text : "0,00");
                    Decimal totalProtecaoBasicaReprogramadoExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaReprogramadoExercicio4.Text) ? txtProtecaoBasicaReprogramadoExercicio4.Text : "0,00");

                    Decimal totalProtecaoMediaExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialMediaExercicio4.Text) ? txtProtecaoSocialMediaExercicio4.Text : "0,00");
                    Decimal totalProtecaoMediaReprogramadoExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaReprogramadoExercicio4.Text) ? txtProtecaoMediaReprogramadoExercicio4.Text : "0,00");

                    Decimal totalProtecaoAltaExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoSocialAltaExercicio4.Text) ? txtProtecaoSocialAltaExercicio4.Text : "0,00");
                    Decimal totalProtecaoAltaReprogramadoExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaReprogramadoExercicio4.Text) ? txtProtecaoAltaReprogramadoExercicio4.Text : "0,00");

                    Decimal totalBeneficiosEventuaisExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisExercicio4.Text) ? txtBeneficiosEventuaisExercicio4.Text : "0,00");
                    Decimal totalBeneficiosEventuaisReprogramadoExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisReprogramadoExercicio4.Text) ? txtBeneficiosEventuaisReprogramadoExercicio4.Text : "0,00");

                    Decimal totalProgramaProjetoExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtSaoPauloSolidarioExercicio4.Text) ? txtSaoPauloSolidarioExercicio4.Text : "0,00");
                    Decimal totalProgramaProjetoReprogramadoExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtSaoPauloSolidarioReprogramadoExercicio4.Text) ? txtSaoPauloSolidarioReprogramadoExercicio4.Text : "0,00");

                    Decimal totalDemandasProtecaoBasicaExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaDemandasExercicio4.Text) ? txtProtecaoBasicaDemandasExercicio4.Text : "0,00");
                    Decimal totalDemandasProtecaoBasicaReprogramacaoExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoBasicaReprogramacaoDemandasExercicio4.Text) ? txtProtecaoBasicaReprogramacaoDemandasExercicio4.Text : "0,00");

                    Decimal totalDemandasProtecaoMediaExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaDemandasExercicio4.Text) ? txtProtecaoMediaDemandasExercicio4.Text : "0,00");
                    Decimal totalDemandasProtecaoMediaReprogramacaoExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoMediaReprogramacaoDemandasExercicio4.Text) ? txtProtecaoMediaReprogramacaoDemandasExercicio4.Text : "0,00");

                    Decimal totalDemandasProtecaoAltaExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaDemandasExercicio4.Text) ? txtProtecaoAltaDemandasExercicio4.Text : "0,00");
                    Decimal totalDemandasProtecaoAltaReprogramacaoExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtProtecaoAltaReprogramacaoDemandasExercicio4.Text) ? txtProtecaoAltaReprogramacaoDemandasExercicio4.Text : "0,00");

                    Decimal totalDemandasBeneficiosEventuaisExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisDemandasExercicio4.Text) ? txtBeneficiosEventuaisDemandasExercicio4.Text : "0,00");

                    Decimal totalDemandasBeneficiosEventuaisReprogramacaoExercicio4 = Convert.ToDecimal(!String.IsNullOrEmpty(txtBeneficiosEventuaisDemandasReprogramadoExercicio4.Text) ? txtBeneficiosEventuaisDemandasReprogramadoExercicio4.Text : "0,00");

                    #endregion informado manualmente - exercicio4

                    #region comparar [dados do bloco 5 vs informados manualmente - ex 4]
                    if (ExercicioLiberado2025)
                    {

                        String erroValores2025 = String.Empty;

                        if (valorBasicaExercicio4 != totalProtecaoBasicaExercicio4)
                        {
                            erroValores2025 = (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da proteção Social Básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            
                        }

                        if (valorMediaExercicio4 != totalProtecaoMediaExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da proteção Social Média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            
                        }

                        if (valorAltaExercicio4 != totalProtecaoAltaExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da proteção Social Alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                           
                        }

                        if (beneficiosEventuaisExercicio4 != totalBeneficiosEventuaisExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor dos beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            
                        }

                        if (programaProjetoExercicio4 != totalProgramaProjetoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da proteção Social Programa Projeto não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                           
                        }



                        if (valorBasicaReprogramadoExercicio4 != totalProtecaoBasicaReprogramadoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da proteção Social Básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            
                        }

                        if (valorMediaReprogramadoExercicio4 != totalProtecaoMediaReprogramadoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da proteção Social Média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                           
                        }

                        if (valorAltaReprogramadoExercicio4 != totalProtecaoAltaReprogramadoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da proteção Social Alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            
                        }

                        if (beneficiosEventuaisReprogramadoExercicio4 != totalBeneficiosEventuaisReprogramadoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor dos beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                           
                        }

                        if (programaProjetoReprogramadoExercicio4 != totalProgramaProjetoReprogramadoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da proteção Social Programa Projeto não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                           
                        }



                        if (valorBasicaDemandasExercicio4 != totalDemandasProtecaoBasicaExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            
                        }

                        if (valorBasicaReprogramadoDemandasExercicio4 != totalDemandasProtecaoBasicaReprogramacaoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social básica não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                           
                        }


                        if (valorMediaDemandasExercicio4 != totalDemandasProtecaoMediaExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social média não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                           
                        }

                        if (valorMediaReprogramadoDemandasExercicio4 != totalDemandasProtecaoMediaReprogramacaoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social media não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                           
                        }


                        if (valorAltaDemandasExercicio4 != totalDemandasProtecaoAltaExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor das demandas proteção Social alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            
                        }

                        if (valorBeneficiosEventuaisDemandasExercicio4 != totalDemandasBeneficiosEventuaisExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor das demandas beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);                           
                        }

                        if (valorAltaReprogramadoDemandasExercicio4 != totalDemandasProtecaoAltaReprogramacaoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor da reprogramação das demandas proteção Social alta não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);
                        }

                        if (valorBeneficiosEventuaisReprogramadoDemandasExercicio4 != totalDemandasBeneficiosEventuaisReprogramacaoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor reprogramado das demandas beneficios eventuais não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);
                        }


                        if (!String.IsNullOrEmpty(erroValores2025))
                        {
                            return false;
                        }


                        var totalGeralProtecoesExercicio4 = totalProtecaoBasicaExercicio4 + totalProtecaoMediaExercicio4 + totalProtecaoAltaExercicio4 + totalProgramaProjetoExercicio4 + totalBeneficiosEventuaisExercicio4;

                        var totalGeralReprogramacaoExercicio4 = totalProtecaoBasicaReprogramadoExercicio4 + totalProtecaoMediaReprogramadoExercicio4 + totalProtecaoAltaReprogramadoExercicio4 + totalBeneficiosEventuaisReprogramadoExercicio4;

                        var totalGeralDemandasExercicio4 = totalDemandasProtecaoBasicaExercicio4 + totalDemandasProtecaoMediaExercicio4 + totalDemandasProtecaoAltaExercicio4 + totalDemandasBeneficiosEventuaisExercicio4;

                        var totalGeralDemandasReprogramacaoExercicio4 = totalDemandasProtecaoBasicaReprogramacaoExercicio4 + totalDemandasProtecaoMediaReprogramacaoExercicio4 + totalDemandasProtecaoAltaReprogramacaoExercicio4;


                        var valorGeralProtecoesExercicio4 = valorBasicaExercicio4 + valorMediaExercicio4 + valorAltaExercicio4 + programaProjetoExercicio4 + beneficiosEventuaisExercicio4;

                        var valorGeralReprogramacaoExercicio4 = valorBasicaReprogramadoExercicio4 + valorMediaReprogramadoExercicio4 + valorAltaReprogramadoExercicio4 + beneficiosEventuaisReprogramadoExercicio4;

                        var valorGeralDemandasExercicio4 = valorBasicaDemandasExercicio4 + valorMediaDemandasExercicio4 + valorAltaDemandasExercicio4 + valorBeneficiosEventuaisDemandasExercicio4;

                        var valorGeralDemandasReprogramacaoExercicio4 = valorBasicaReprogramadoDemandasExercicio4 + valorMediaReprogramadoDemandasExercicio4 + valorAltaReprogramadoDemandasExercicio4;


                        if (totalGeralProtecoesExercicio4 != valorGeralProtecoesExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor total do repasse no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            return false;
                        }

                        if (totalGeralReprogramacaoExercicio4 != valorGeralReprogramacaoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor total reprogramado no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            return false;
                        }


                        if (totalGeralDemandasExercicio4 != valorGeralDemandasExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor total das demandas no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            return false;
                        }


                        if (totalGeralDemandasReprogramacaoExercicio4 != valorGeralDemandasReprogramacaoExercicio4)
                        {
                            erroValores2025 += (String.IsNullOrEmpty(erroValores2025) ? "" : "<br/>") + String.Format("O valor total reprogramado das demandas no exercício de {0} não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício.", Exercicios[2].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(erroValores2025), true);

                            return false;
                        }				

                        /*if (Convert.ToDecimal(hidTotalRecursosExercicio4.Value) != Convert.ToDecimal(lblValorCofinanciamentoExercicio4.Text)
                           || valorBasicaExercicio4 != totalProtecaoBasicaExercicio4
                           || valorMediaExercicio4 != totalProtecaoMediaExercicio4
                           || valorAltaExercicio4 != totalProtecaoAltaExercicio4
                           || valorBeneficioProgramaExercicio4 != totalProgramaProjetoExercicio4
                            //|| valorBeneficioEventualExercicio2 != totalBeneficiosEventuaisExercicio2
                            //----------------Reprogramado
                           || valorBasicaReprogramadoExercicio4 != totalProtecaoBasicaReprogramadoExercicio4
                           || valorMediaReprogramadoExercicio4 != totalProtecaoMediaReprogramadoExercicio4
                           || valorAltaReprogramadoExercicio4 != totalProtecaoAltaReprogramadoExercicio4
                             )
                        {
                            String ErroValores = String.Format("O valor total do cofinanciamento não confere com valor registrado nos cronogramas de desembolso no bloco V para exercício {0}.", Exercicios[3].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Seds.PMAS.QUADRIENAL.UI.Processos.Util.GetJavascriptDialogError(ErroValores), true);

                            return false;
                        }*/
                    }
                    #endregion

                    #endregion

                }
            }

            return true;

        }



        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var situacao = (ESituacao)Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idSituacao"]));
            switch (situacao)
            {
                case ESituacao.DevolvidoDrads:
                case ESituacao.Desbloqueado:
                case ESituacao.EmAnalisedoCMAS:
                case ESituacao.Parafinalizacao:
                    Response.Redirect("~/ConsultaFluxoPMASDRADS.aspx");
                    break;
                case ESituacao.AutorizaDesbloqueioCMAS:
                case ESituacao.AutorizaDesbloqueioGestor:
                case ESituacao.AutorizaDesbloqueioReprogramacao:
                    Response.Redirect("~/ConsultaFluxoPMASCAS.aspx");
                    break;
                case ESituacao.DevolvidopeloCMAS:
                case ESituacao.Aprovado:
                case ESituacao.Rejeitado:
                    Response.Redirect("~/ConsultaFluxoPMASCMAS.aspx");
                    break;
            }
        }
        #endregion

        #region Web Methods
        [System.Web.Services.WebMethod]
        public static String CalcularValores(String[] valores)
        {
            decimal total = 0M;
            foreach (String val in valores)
            {
                string valor = !String.IsNullOrEmpty(val) ? val : "0,00";
                total += Convert.ToDecimal(valor);
            }
            return total.ToString("N2");
        }

        [System.Web.Services.WebMethod]
        public static String CalcularProtecaoBasica(String protecaobasica, string protecaobasicareprogramado, string protecaobasicaDemandas, string protecaobasicaReprogramadoDemandas)
        {
            decimal total = 0M;
            total = Convert.ToDecimal(!String.IsNullOrEmpty(protecaobasica) ? protecaobasica : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(protecaobasicareprogramado) ? protecaobasicareprogramado : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(protecaobasicaDemandas) ? protecaobasicaDemandas : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(protecaobasicaReprogramadoDemandas) ? protecaobasicaReprogramadoDemandas : "0,00");
            return total.ToString("N2");
        }
        [System.Web.Services.WebMethod]
        public static String CalcularProtecaoMedia(String protecaomedia, string protecaomediareprogramado, string protecaomediaDemandas, string protecaomediaReprogramadoDemandas)
        {
            decimal total = 0M;
            total = Convert.ToDecimal(!String.IsNullOrEmpty(protecaomedia) ? protecaomedia : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(protecaomediareprogramado) ? protecaomediareprogramado : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(protecaomediaDemandas) ? protecaomediaDemandas : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(protecaomediaReprogramadoDemandas) ? protecaomediaReprogramadoDemandas : "0,00");
            return total.ToString("N2");
        }
        [System.Web.Services.WebMethod]
        public static String CalcularProtecaoAlta(String protecaoalta, string protecaoaltareprogramado, string protecaoaltaDemandas, string protecaoaltaReprogramadoDemandas)
        {
            decimal total = 0M;
            total = Convert.ToDecimal(!String.IsNullOrEmpty(protecaoalta) ? protecaoalta : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(protecaoaltareprogramado) ? protecaoaltareprogramado : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(protecaoaltaDemandas) ? protecaoaltaDemandas : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(protecaoaltaReprogramadoDemandas) ? protecaoaltaReprogramadoDemandas : "0,00");
            return total.ToString("N2");
        }
        [System.Web.Services.WebMethod]
        public static String CalcularBeneficios(String beneficios, string beneficiosreprogramado, string beneficiosDemandas, string beneficiosReprogramadoDemandas)
        {
            decimal total = 0M;
            total = Convert.ToDecimal(!String.IsNullOrEmpty(beneficios) ? beneficios : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(beneficiosreprogramado) ? beneficiosreprogramado : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(beneficiosDemandas) ? beneficiosDemandas : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(beneficiosReprogramadoDemandas) ? beneficiosReprogramadoDemandas : "0,00");
            return total.ToString("N2");
        }
        [System.Web.Services.WebMethod]
        public static String CalcularSPSolidario(String spsolidario, string spsolidarioreprogramado)
        {
            decimal total = 0M;
            total = Convert.ToDecimal(!String.IsNullOrEmpty(spsolidario) ? spsolidario : "0,00") + Convert.ToDecimal(!String.IsNullOrEmpty(spsolidarioreprogramado) ? spsolidarioreprogramado : "0,00");
            return total.ToString("N2");
        }
        [System.Web.Services.WebMethod]
        public static String CalcularValoresReprogramados(String[] valores)
        {

            decimal totalReprogramado = 0M;
            foreach (String val in valores)
            {
                totalReprogramado += Convert.ToDecimal(val);
            }
            return totalReprogramado.ToString("N2");
        }

        public static String CalcularValoresDemandas(String[] valores)
        {

            decimal totalDemandas = 0M;
            foreach (String val in valores)
            {
                totalDemandas += Convert.ToDecimal(val);
            }
            return totalDemandas.ToString("N2");
        }


        #endregion

        #region helpers


        private void AdicionarEventosExercicio1()
        {
            txtProtecaoSocialBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialBasica.Attributes.Add("onblur", "CalculateTotal()");

            txtProtecaoSocialMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialMedia.Attributes.Add("onblur", "CalculateTotal()");

            txtProtecaoSocialAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialAlta.Attributes.Add("onblur", "CalculateTotal()");

            txtBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtBeneficiosEventuais.Attributes.Add("onblur", "CalculateTotal()");

            txtSaoPauloSolidario.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtSaoPauloSolidario.Attributes.Add("onblur", "CalculateTotal()");


            txtProtecaoBasicaReprogramado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoBasicaReprogramado.Attributes.Add("onblur", "CalculateTotal()");

            txtProtecaoMediaReprogramado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaReprogramado.Attributes.Add("onblur", "CalculateTotal()");

            txtProtecaoAltaReprogramado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaReprogramado.Attributes.Add("onblur", "CalculateTotal()");

            txtBeneficiosEventuaisReprogramado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtBeneficiosEventuaisReprogramado.Attributes.Add("onblur", "CalculateTotal()");

            txtSaoPauloSolidarioReprogramado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtSaoPauloSolidarioReprogramado.Attributes.Add("onblur", "CalculateTotal()");


            txtProtecaoBasicaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoBasicaDemandas.Attributes.Add("onblur", "CalculateTotal()");

            txtProtecaoMediaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaDemandas.Attributes.Add("onblur", "CalculateTotal()");

            txtProtecaoAltaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaDemandas.Attributes.Add("onblur", "CalculateTotal()");

            txtBeneficiosEventuaisDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtBeneficiosEventuaisDemandas.Attributes.Add("onblur", "CalculateTotal()");

            txtSaoPauloSolidarioDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtSaoPauloSolidarioDemandas.Attributes.Add("onblur", "CalculateTotal()");


            txtProtecaoBasicaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoBasicaReprogramacaoDemandas.Attributes.Add("onblur", "CalculateTotal()");

            txtProtecaoMediaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaReprogramacaoDemandas.Attributes.Add("onblur", "CalculateTotal()");

            txtProtecaoAltaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaReprogramacaoDemandas.Attributes.Add("onblur", "CalculateTotal()");

            txtBeneficiosEventuaisDemandasReprogramado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtBeneficiosEventuaisDemandasReprogramado.Attributes.Add("onblur", "CalculateTotal()");

        }

        private void AdicionarEventosExercicio2()
        {
            txtProtecaoSocialBasicaExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialBasicaExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtProtecaoSocialMediaExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialMediaExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtProtecaoSocialAltaExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialAltaExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtBeneficiosEventuaisExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtBeneficiosEventuaisExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtSaoPauloSolidarioExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtSaoPauloSolidarioExercicio2.Attributes.Add("onblur", "CalculateTotal2()");


            txtProtecaoBasicaReprogramadoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtProtecaoBasicaReprogramadoExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtProtecaoMediaReprogramadoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaReprogramadoExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtProtecaoAltaReprogramadoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaReprogramadoExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtBeneficiosEventuaisReprogramadoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtBeneficiosEventuaisReprogramadoExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtSaoPauloSolidarioReprogramadoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtSaoPauloSolidarioReprogramadoExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtProtecaoBasicaDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtProtecaoBasicaDemandasExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtProtecaoMediaDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaDemandasExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtProtecaoAltaDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaDemandasExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtBeneficiosEventuaisDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtBeneficiosEventuaisDemandasExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtSaoPauloSolidarioDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtSaoPauloSolidarioDemandasExercicio2.Attributes.Add("onblur", "CalculateTotal2()");


            txtProtecaoBasicaReprogramacaoDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoBasicaReprogramacaoDemandasExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtProtecaoMediaReprogramacaoDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaReprogramacaoDemandasExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtProtecaoAltaReprogramacaoDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaReprogramacaoDemandasExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

            txtBeneficiosEventuaisDemandasReprogramadoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtBeneficiosEventuaisDemandasReprogramadoExercicio2.Attributes.Add("onblur", "CalculateTotal2()");

        }

        private void AdicionarEventosExercicio3()
        {
            txtProtecaoSocialBasicaExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialBasicaExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtProtecaoSocialMediaExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialMediaExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtProtecaoSocialAltaExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialAltaExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtBeneficiosEventuaisExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtBeneficiosEventuaisExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtSaoPauloSolidarioExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtSaoPauloSolidarioExercicio3.Attributes.Add("onblur", "CalculateTotal3()");


            txtProtecaoBasicaReprogramadoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtProtecaoBasicaReprogramadoExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtProtecaoMediaReprogramadoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaReprogramadoExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtProtecaoAltaReprogramadoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaReprogramadoExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtBeneficiosEventuaisReprogramadoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtBeneficiosEventuaisReprogramadoExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtSaoPauloSolidarioReprogramadoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtSaoPauloSolidarioReprogramadoExercicio3.Attributes.Add("onblur", "CalculateTotal3()");



            txtProtecaoBasicaDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtProtecaoBasicaDemandasExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtProtecaoMediaDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaDemandasExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtProtecaoAltaDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaDemandasExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtBeneficiosEventuaisDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtBeneficiosEventuaisDemandasExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtSaoPauloSolidarioDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtSaoPauloSolidarioDemandasExercicio3.Attributes.Add("onblur", "CalculateTotal3()");


            txtProtecaoBasicaReprogramacaoDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoBasicaReprogramacaoDemandasExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtProtecaoMediaReprogramacaoDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaReprogramacaoDemandasExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtProtecaoAltaReprogramacaoDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaReprogramacaoDemandasExercicio3.Attributes.Add("onblur", "CalculateTotal3()");

            txtBeneficiosEventuaisDemandasReprogramadoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtBeneficiosEventuaisDemandasReprogramadoExercicio3.Attributes.Add("onblur", "CalculateTotal3()");


        }

        private void AdicionarEventosExercicio4()
        {
            txtProtecaoSocialBasicaExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialBasicaExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtProtecaoSocialMediaExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialMediaExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtProtecaoSocialAltaExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoSocialAltaExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtBeneficiosEventuaisExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtBeneficiosEventuaisExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtSaoPauloSolidarioExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtSaoPauloSolidarioExercicio4.Attributes.Add("onblur", "CalculateTotal4()");


            txtProtecaoBasicaReprogramadoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtProtecaoBasicaReprogramadoExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtProtecaoMediaReprogramadoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaReprogramadoExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtProtecaoAltaReprogramadoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaReprogramadoExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtBeneficiosEventuaisReprogramadoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtBeneficiosEventuaisReprogramadoExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtSaoPauloSolidarioReprogramadoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtSaoPauloSolidarioReprogramadoExercicio4.Attributes.Add("onblur", "CalculateTotal4()");


            txtProtecaoBasicaDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtProtecaoBasicaDemandasExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtProtecaoMediaDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaDemandasExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtProtecaoAltaDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaDemandasExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtBeneficiosEventuaisDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtBeneficiosEventuaisDemandasExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtSaoPauloSolidarioDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtSaoPauloSolidarioDemandasExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtProtecaoBasicaReprogramacaoDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoBasicaReprogramacaoDemandasExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtProtecaoMediaReprogramacaoDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoMediaReprogramacaoDemandasExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtProtecaoAltaReprogramacaoDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtProtecaoAltaReprogramacaoDemandasExercicio4.Attributes.Add("onblur", "CalculateTotal4()");

            txtBeneficiosEventuaisDemandasReprogramadoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtBeneficiosEventuaisDemandasReprogramadoExercicio4.Attributes.Add("onblur", "CalculateTotal4()");
        }

        private void CarregarCofinanciamentosReprogramacaoExercicio1(DradsPlanoMunicipalRecursosReprogramadoInfo cofinanciamentosReprogramadoExercicio1)
        {
            txtProtecaoBasicaReprogramado.Text = (cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialBasicaReprogramado.Value : 0M).ToString("N2");

            txtProtecaoMediaReprogramado.Text = (cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialMediaReprogramado.Value : 0M).ToString("N2");

            txtProtecaoAltaReprogramado.Text = (cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio1.ValorProtecaoSocialAltaReprogramado.Value : 0M).ToString("N2");

        }

        private void CarregarCofinanciamentosReprogramacaoExercicio2(DradsPlanoMunicipalRecursosReprogramadoInfo cofinanciamentosReprogramadoExercicio2)
        {
            if (cofinanciamentosReprogramadoExercicio2 != null)
            {
                txtProtecaoBasicaReprogramadoExercicio2.Text = (cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialBasicaReprogramado.Value : 0M).ToString("N2");

                txtProtecaoMediaReprogramadoExercicio2.Text = (cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialMediaReprogramado.Value : 0M).ToString("N2");

                txtProtecaoAltaReprogramadoExercicio2.Text = (cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio2.ValorProtecaoSocialAltaReprogramado.Value : 0M).ToString("N2");
            }
        }

        private void CarregarCofinanciamentosReprogramacaoExercicio3(DradsPlanoMunicipalRecursosReprogramadoInfo cofinanciamentosReprogramadoExercicio3)
        {
            if (cofinanciamentosReprogramadoExercicio3 != null)
            {
                txtProtecaoBasicaReprogramadoExercicio3.Text = (cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialBasicaReprogramado.Value : 0M).ToString("N2");

                txtProtecaoMediaReprogramadoExercicio3.Text = (cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialMediaReprogramado.Value : 0M).ToString("N2");

                txtProtecaoAltaReprogramadoExercicio3.Text = (cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio3.ValorProtecaoSocialAltaReprogramado.Value : 0M).ToString("N2");
            }
        }

        private void CarregarCofinanciamentosReprogramacaoExercicio4(DradsPlanoMunicipalRecursosReprogramadoInfo cofinanciamentosReprogramadoExercicio4)
        {
            if (cofinanciamentosReprogramadoExercicio4 != null)
            {
                txtProtecaoBasicaReprogramadoExercicio4.Text = (cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialBasicaReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialBasicaReprogramado.Value : 0M).ToString("N2");

                txtProtecaoMediaReprogramadoExercicio4.Text = (cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialMediaReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialMediaReprogramado.Value : 0M).ToString("N2");

                txtProtecaoAltaReprogramadoExercicio4.Text = (cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialAltaReprogramado != null ? cofinanciamentosReprogramadoExercicio4.ValorProtecaoSocialAltaReprogramado.Value : 0M).ToString("N2");
            }
        }


        private void CarregarCofinanciamentosDemandasExercicio1(DradsPlanoMunicipalDemandasParlamentaresInfo cofinanciamentosDemandasExercicio1)
        {
            txtProtecaoBasicaDemandas.Text = (cofinanciamentosDemandasExercicio1.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialBasicaDemandas.Value : 0M).ToString("N2");

            txtProtecaoMediaDemandas.Text = (cofinanciamentosDemandasExercicio1.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialMediaDemandas.Value : 0M).ToString("N2");

            txtProtecaoAltaDemandas.Text = (cofinanciamentosDemandasExercicio1.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio1.ValorProtecaoSocialAltaDemandas.Value : 0M).ToString("N2");

        }

        private void CarregarCofinanciamentosDemandasExercicio2(DradsPlanoMunicipalDemandasParlamentaresInfo cofinanciamentosDemandasExercicio2)
        {
            if (cofinanciamentosDemandasExercicio2 != null)
            {
                txtProtecaoBasicaDemandasExercicio2.Text = (cofinanciamentosDemandasExercicio2.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialBasicaDemandas.Value : 0M).ToString("N2");

                txtProtecaoMediaDemandasExercicio2.Text = (cofinanciamentosDemandasExercicio2.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialMediaDemandas.Value : 0M).ToString("N2");

                txtProtecaoAltaDemandasExercicio2.Text = (cofinanciamentosDemandasExercicio2.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio2.ValorProtecaoSocialAltaDemandas.Value : 0M).ToString("N2");
            }
        }

        private void CarregarCofinanciamentosDemandasExercicio3(DradsPlanoMunicipalDemandasParlamentaresInfo cofinanciamentosDemandasExercicio3)
        {
            if (cofinanciamentosDemandasExercicio3 != null)
            {
                txtProtecaoBasicaDemandasExercicio3.Text = (cofinanciamentosDemandasExercicio3.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialBasicaDemandas.Value : 0M).ToString("N2");

                txtProtecaoMediaDemandasExercicio3.Text = (cofinanciamentosDemandasExercicio3.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialMediaDemandas.Value : 0M).ToString("N2");

                txtProtecaoAltaDemandasExercicio3.Text = (cofinanciamentosDemandasExercicio3.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio3.ValorProtecaoSocialAltaDemandas.Value : 0M).ToString("N2");
            }
        }

        private void CarregarCofinanciamentosDemandasExercicio4(DradsPlanoMunicipalDemandasParlamentaresInfo cofinanciamentosDemandasExercicio4)
        {
            if (cofinanciamentosDemandasExercicio4 != null)
            {
                txtProtecaoBasicaDemandasExercicio4.Text = (cofinanciamentosDemandasExercicio4.ValorProtecaoSocialBasicaDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialBasicaDemandas.Value : 0M).ToString("N2");

                txtProtecaoMediaDemandasExercicio4.Text = (cofinanciamentosDemandasExercicio4.ValorProtecaoSocialMediaDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialMediaDemandas.Value : 0M).ToString("N2");

                txtProtecaoAltaDemandasExercicio4.Text = (cofinanciamentosDemandasExercicio4.ValorProtecaoSocialAltaDemandas != null ? cofinanciamentosDemandasExercicio4.ValorProtecaoSocialAltaDemandas.Value : 0M).ToString("N2");
            }
        }



        private void CarregarCofinanciamentosReprogramadoDemandasExercicio1(DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo cofinanciamentosReprogramadoDemandasExercicio1)
        {
            txtProtecaoBasicaReprogramacaoDemandas.Text = (cofinanciamentosReprogramadoDemandasExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtProtecaoMediaReprogramacaoDemandas.Text = (cofinanciamentosReprogramadoDemandasExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtProtecaoAltaReprogramacaoDemandas.Text = (cofinanciamentosReprogramadoDemandasExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtBeneficiosEventuaisDemandasReprogramado.Text = (cofinanciamentosReprogramadoDemandasExercicio1.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio1.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M).ToString("N2");

        }

        private void CarregarCofinanciamentosReprogramadoDemandasExercicio2(DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo cofinanciamentosReprogramadoDemandasExercicio2)
        {
            txtProtecaoBasicaReprogramacaoDemandasExercicio2.Text = (cofinanciamentosReprogramadoDemandasExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtProtecaoMediaReprogramacaoDemandasExercicio2.Text = (cofinanciamentosReprogramadoDemandasExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtProtecaoAltaReprogramacaoDemandasExercicio2.Text = (cofinanciamentosReprogramadoDemandasExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtBeneficiosEventuaisDemandasReprogramadoExercicio2.Text = (cofinanciamentosReprogramadoDemandasExercicio2.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio2.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M).ToString("N2");

        }

        private void CarregarCofinanciamentosReprogramadoDemandasExercicio3(DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo cofinanciamentosReprogramadoDemandasExercicio3)
        {
            txtProtecaoBasicaReprogramacaoDemandasExercicio3.Text = (cofinanciamentosReprogramadoDemandasExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtProtecaoMediaReprogramacaoDemandasExercicio3.Text = (cofinanciamentosReprogramadoDemandasExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtProtecaoAltaReprogramacaoDemandasExercicio3.Text = (cofinanciamentosReprogramadoDemandasExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtBeneficiosEventuaisDemandasReprogramadoExercicio3.Text = (cofinanciamentosReprogramadoDemandasExercicio3.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio3.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M).ToString("N2");
        }

        private void CarregarCofinanciamentosReprogramadoDemandasExercicio4(DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo cofinanciamentosReprogramadoDemandasExercicio4)
        {
            txtProtecaoBasicaReprogramacaoDemandasExercicio4.Text = (cofinanciamentosReprogramadoDemandasExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtProtecaoMediaReprogramacaoDemandasExercicio4.Text = (cofinanciamentosReprogramadoDemandasExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtProtecaoAltaReprogramacaoDemandasExercicio4.Text = (cofinanciamentosReprogramadoDemandasExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas.Value : 0M).ToString("N2");

            txtBeneficiosEventuaisDemandasReprogramadoExercicio4.Text = (cofinanciamentosReprogramadoDemandasExercicio4.ValorBeneficioEventuaisReprogramadoDemandas != null ? cofinanciamentosReprogramadoDemandasExercicio4.ValorBeneficioEventuaisReprogramadoDemandas.Value : 0M).ToString("N2");
        }

        private void CarregarConfinanciamentoConsolidadoExercicio1(int exercicio1, DradsPlanoMunicipalRecursosInfo vProtSocialExercicio1)
        {
            if (vProtSocialExercicio1 != null)
            {
                txtProtecaoSocialBasica.Text = vProtSocialExercicio1.ValorProtecaoSocialBasica.HasValue
                    ? vProtSocialExercicio1.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                txtProtecaoSocialMedia.Text = vProtSocialExercicio1.ValorProtecaoSocialMediaComplexidade.HasValue
                    ? vProtSocialExercicio1.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                txtProtecaoSocialAlta.Text = vProtSocialExercicio1.ValorProtecaoSocialAltaComplexidade.HasValue
                    ? vProtSocialExercicio1.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                txtBeneficiosEventuais.Text = vProtSocialExercicio1.ValorBeneficiosEventuais.HasValue
                    ? vProtSocialExercicio1.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                txtSaoPauloSolidario.Text = vProtSocialExercicio1.ValorProgramaProjetoSolidario.HasValue
                    ? vProtSocialExercicio1.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");



                lblValorCofinanciamento.Text = lblTotalCofinanciamento.Text = Convert.ToDecimal(
                          vProtSocialExercicio1.ValorProtecaoSocialBasica != null ? vProtSocialExercicio1.ValorProtecaoSocialBasica.Value : 0M
                        + vProtSocialExercicio1.ValorProtecaoSocialMediaComplexidade != null ? vProtSocialExercicio1.ValorProtecaoSocialMediaComplexidade.Value : 0M
                        + vProtSocialExercicio1.ValorProtecaoSocialAltaComplexidade != null ? vProtSocialExercicio1.ValorProtecaoSocialAltaComplexidade.Value : 0M).ToString("N2");

                
                if (vProtSocialExercicio1.ValorBeneficiosEventuais != null)
                {
                    lblValorCofinanciamento.Text += Convert.ToDecimal(vProtSocialExercicio1.ValorBeneficiosEventuais.Value).ToString("N2");
                }
                if (vProtSocialExercicio1.ValorProgramaProjetoSolidario != null)
                {
                    lblValorCofinanciamento.Text += Convert.ToDecimal(vProtSocialExercicio1.ValorProgramaProjetoSolidario.Value).ToString("N2");
                }
            }
        }

        private void CarregarConfinanciamentoConsolidadoExercicio2(int exercicio2, DradsPlanoMunicipalRecursosInfo vProtSocialExercicio2)
        {

            if (vProtSocialExercicio2 != null)
            {


                txtProtecaoSocialBasicaExercicio2.Text = vProtSocialExercicio2.ValorProtecaoSocialBasica.HasValue
                    ? vProtSocialExercicio2.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                txtProtecaoSocialMediaExercicio2.Text = vProtSocialExercicio2.ValorProtecaoSocialMediaComplexidade.HasValue
                    ? vProtSocialExercicio2.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                txtProtecaoSocialAltaExercicio2.Text = vProtSocialExercicio2.ValorProtecaoSocialAltaComplexidade.HasValue
                    ? vProtSocialExercicio2.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                txtBeneficiosEventuaisExercicio2.Text = vProtSocialExercicio2.ValorBeneficiosEventuais.HasValue
                    ? vProtSocialExercicio2.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                txtSaoPauloSolidarioExercicio2.Text = vProtSocialExercicio2.ValorProgramaProjetoSolidario.HasValue
                    ? vProtSocialExercicio2.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");

                lblValorCofinanciamentoExercicio2.Text = lblTotalCofinanciamentoExercicio2.Text = Convert.ToDecimal(vProtSocialExercicio2.ValorProtecaoSocialBasica.Value
                    + vProtSocialExercicio2.ValorProtecaoSocialMediaComplexidade != null ? vProtSocialExercicio2.ValorProtecaoSocialMediaComplexidade.Value : 0M
                    + vProtSocialExercicio2.ValorProtecaoSocialAltaComplexidade != null ? vProtSocialExercicio2.ValorProtecaoSocialAltaComplexidade.Value : 0M).ToString("N2");

                if (vProtSocialExercicio2.ValorBeneficiosEventuais != null)
                {
                    lblValorCofinanciamentoExercicio2.Text += Convert.ToDecimal(vProtSocialExercicio2.ValorBeneficiosEventuais.Value).ToString("N2");
                }
                if (vProtSocialExercicio2.ValorProgramaProjetoSolidario != null)
                {
                    lblValorCofinanciamentoExercicio2.Text += Convert.ToDecimal(vProtSocialExercicio2.ValorProgramaProjetoSolidario.Value).ToString("N2");
                }
            }
        }

        private void CarregarConfinanciamentoConsolidadoExercicio3(int exercicio3, DradsPlanoMunicipalRecursosInfo vProtSocialExercicio3)
        {
            if (vProtSocialExercicio3 != null)
            {


                txtProtecaoSocialBasicaExercicio3.Text = vProtSocialExercicio3.ValorProtecaoSocialBasica.HasValue
                    ? vProtSocialExercicio3.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                txtProtecaoSocialMediaExercicio3.Text = vProtSocialExercicio3.ValorProtecaoSocialMediaComplexidade.HasValue
                    ? vProtSocialExercicio3.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                txtProtecaoSocialAltaExercicio3.Text = vProtSocialExercicio3.ValorProtecaoSocialAltaComplexidade.HasValue
                    ? vProtSocialExercicio3.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                txtBeneficiosEventuaisExercicio3.Text = vProtSocialExercicio3.ValorBeneficiosEventuais.HasValue
                    ? vProtSocialExercicio3.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                txtSaoPauloSolidarioExercicio3.Text = vProtSocialExercicio3.ValorProgramaProjetoSolidario.HasValue
                    ? vProtSocialExercicio3.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");

                lblValorCofinanciamentoExercicio3.Text = lblTotalCofinanciamentoExercicio3.Text = Convert.ToDecimal(vProtSocialExercicio3.ValorProtecaoSocialBasica.Value
                    + vProtSocialExercicio3.ValorProtecaoSocialMediaComplexidade != null ? vProtSocialExercicio3.ValorProtecaoSocialMediaComplexidade.Value : 0M
                    + vProtSocialExercicio3.ValorProtecaoSocialAltaComplexidade != null ? vProtSocialExercicio3.ValorProtecaoSocialAltaComplexidade.Value : 0M).ToString("N2");

                if (vProtSocialExercicio3.ValorBeneficiosEventuais != null)
                {
                    lblValorCofinanciamentoExercicio3.Text += Convert.ToDecimal(vProtSocialExercicio3.ValorBeneficiosEventuais.Value).ToString("N2");
                }
                if (vProtSocialExercicio3.ValorProgramaProjetoSolidario != null)
                {
                    lblValorCofinanciamentoExercicio2.Text += Convert.ToDecimal(vProtSocialExercicio3.ValorProgramaProjetoSolidario.Value).ToString("N2");
                }

            }
        }

        private void CarregarConfinanciamentoConsolidadoReprogramadoExercicio1(int exercicio1
                                                                              , DradsPlanoMunicipalRecursosReprogramadoInfo vProtSocialExercicio1)
        {
            if (vProtSocialExercicio1 != null)
            {
                txtProtecaoBasicaReprogramado.Text = (vProtSocialExercicio1.ValorProtecaoSocialBasicaReprogramado.HasValue) ? vProtSocialExercicio1.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : "0,00";
                txtProtecaoMediaReprogramado.Text = (vProtSocialExercicio1.ValorProtecaoSocialMediaReprogramado.HasValue) ? vProtSocialExercicio1.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : "0,00";
                txtProtecaoAltaReprogramado.Text = (vProtSocialExercicio1.ValorProtecaoSocialAltaReprogramado.HasValue) ? vProtSocialExercicio1.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : "0,00";

                ///
                /// TODO:DBM:Beneficios Eventuais
                ///
                //txtBeneficiosEventuaisReprogramado.Text = (vProtSocialExercicio1.ValorBeneficioEventuaisReprogramado.HasValue) ? vProtSocialExercicio1.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : "0,00";

                txtProtecaoBasicaReprogramado.Enabled = false;
                txtProtecaoMediaReprogramado.Enabled = false;
                txtProtecaoAltaReprogramado.Enabled = false;
                txtBeneficiosEventuaisReprogramado.Enabled = false;
                txtSaoPauloSolidarioReprogramado.Enabled = false;
            }
        }

        private void CarregarConfinanciamentoConsolidadoReprogramadoExercicio2(int exercicio2, DradsPlanoMunicipalRecursosReprogramadoInfo vProtSocialExercicio2)
        {
            //var vProtSocialExercicio2 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == exercicio2).SingleOrDefault();

            if (vProtSocialExercicio2 != null)
            {
                txtProtecaoBasicaReprogramadoExercicio2.Text = (vProtSocialExercicio2.ValorProtecaoSocialBasicaReprogramado.HasValue)
                    ? vProtSocialExercicio2.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : "0,00";
                txtProtecaoMediaReprogramadoExercicio2.Text = (vProtSocialExercicio2.ValorProtecaoSocialMediaReprogramado.HasValue)
                    ? vProtSocialExercicio2.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : "0,00";
                txtProtecaoAltaReprogramadoExercicio2.Text = (vProtSocialExercicio2.ValorProtecaoSocialAltaReprogramado.HasValue)
                    ? vProtSocialExercicio2.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : "0,00";

                //Beneficios eventuais
                //txtBeneficiosEventuaisReprogramadoExercicio2.Text = (vProtSocialExercicio2.ValorBeneficioEventuaisReprogramado.HasValue)
                //    ? vProtSocialExercicio2.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : "0,00";

                txtProtecaoBasicaReprogramadoExercicio2.Enabled = false;
                txtProtecaoMediaReprogramadoExercicio2.Enabled = false;
                txtProtecaoAltaReprogramadoExercicio2.Enabled = false;
                txtBeneficiosEventuaisReprogramadoExercicio2.Enabled = false;
                txtSaoPauloSolidarioReprogramadoExercicio2.Enabled = false;
                //txtDescricao.Text = vProtSocialExercicio2.Descricao; //TODO:DBM - Comentei!

            }
        }

        private void CarregarConfinanciamentoConsolidadoReprogramadoExercicio3(int exercicio3, DradsPlanoMunicipalRecursosReprogramadoInfo vProtSocialExercicio3)
        {
            //var vProtSocialExercicio2 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == exercicio2).SingleOrDefault();

            if (vProtSocialExercicio3 != null)
            {
                txtProtecaoBasicaReprogramadoExercicio3.Text = (vProtSocialExercicio3.ValorProtecaoSocialBasicaReprogramado.HasValue)
                    ? vProtSocialExercicio3.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : "0,00";
                txtProtecaoMediaReprogramadoExercicio2.Text = (vProtSocialExercicio3.ValorProtecaoSocialMediaReprogramado.HasValue)
                    ? vProtSocialExercicio3.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : "0,00";
                txtProtecaoAltaReprogramadoExercicio2.Text = (vProtSocialExercicio3.ValorProtecaoSocialAltaReprogramado.HasValue)
                    ? vProtSocialExercicio3.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : "0,00";

                //Beneficios eventuais
                //txtBeneficiosEventuaisReprogramadoExercicio2.Text = (vProtSocialExercicio2.ValorBeneficioEventuaisReprogramado.HasValue)
                //    ? vProtSocialExercicio2.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : "0,00";

                txtProtecaoBasicaReprogramadoExercicio3.Enabled = false;
                txtProtecaoMediaReprogramadoExercicio3.Enabled = false;
                txtProtecaoAltaReprogramadoExercicio3.Enabled = false;
                txtBeneficiosEventuaisReprogramadoExercicio3.Enabled = false;
                txtSaoPauloSolidarioReprogramadoExercicio3.Enabled = false;
                //txtDescricao.Text = vProtSocialExercicio2.Descricao; //TODO:DBM - Comentei!

            }
        }

        private void CarregarConfinanciamentoConsolidadoDemandasExercicio1(int exercicio1
                                                                              , DradsPlanoMunicipalDemandasParlamentaresInfo vProtSocialExercicio1)
        {
            if (vProtSocialExercicio1 != null)
            {
                txtProtecaoBasicaDemandas.Text = (vProtSocialExercicio1.ValorProtecaoSocialBasicaDemandas.HasValue) ? vProtSocialExercicio1.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoMediaDemandas.Text = (vProtSocialExercicio1.ValorProtecaoSocialMediaDemandas.HasValue) ? vProtSocialExercicio1.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoAltaDemandas.Text = (vProtSocialExercicio1.ValorProtecaoSocialAltaDemandas.HasValue) ? vProtSocialExercicio1.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : "0,00";
                txtBeneficiosEventuaisDemandas.Text = (vProtSocialExercicio1.ValorBeneficioEventuaisDemandas.HasValue) ? vProtSocialExercicio1.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : "0,00";
                txtSaoPauloSolidarioDemandas.Text = "0,00";

                ///
                /// TODO:DBM:Beneficios Eventuais
                ///
                //txtBeneficiosEventuaisDemandas.Text = (vProtSocialExercicio1.ValorBeneficioEventuaisDemandas.HasValue) ? vProtSocialExercicio1.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : "0,00";

                txtProtecaoBasicaDemandas.Enabled = false;
                txtProtecaoMediaDemandas.Enabled = false;
                txtProtecaoAltaDemandas.Enabled = false;
                txtBeneficiosEventuaisDemandas.Enabled = false;
                txtSaoPauloSolidarioDemandas.Enabled = false;
            }
        }

        private void CarregarConfinanciamentoConsolidadoDemandasExercicio2(int exercicio2, DradsPlanoMunicipalDemandasParlamentaresInfo vProtSocialExercicio2)
        {
            //var vProtSocialExercicio2 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == exercicio2).SingleOrDefault();

            if (vProtSocialExercicio2 != null)
            {
                txtProtecaoBasicaDemandasExercicio2.Text = (vProtSocialExercicio2.ValorProtecaoSocialBasicaDemandas.HasValue)
                    ? vProtSocialExercicio2.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoMediaDemandasExercicio2.Text = (vProtSocialExercicio2.ValorProtecaoSocialMediaDemandas.HasValue)
                    ? vProtSocialExercicio2.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoAltaDemandasExercicio2.Text = (vProtSocialExercicio2.ValorProtecaoSocialAltaDemandas.HasValue)
                    ? vProtSocialExercicio2.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : "0,00";

                //Beneficios eventuais
                //txtBeneficiosEventuaisDemandasExercicio2.Text = (vProtSocialExercicio2.ValorBeneficioEventuaisDemandas.HasValue)
                //    ? vProtSocialExercicio2.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : "0,00";

                txtProtecaoBasicaDemandasExercicio2.Enabled = false;
                txtProtecaoMediaDemandasExercicio2.Enabled = false;
                txtProtecaoAltaDemandasExercicio2.Enabled = false;
                txtBeneficiosEventuaisDemandasExercicio2.Enabled = false;
                txtSaoPauloSolidarioDemandasExercicio2.Enabled = false;
                //txtDescricao.Text = vProtSocialExercicio2.Descricao; //TODO:DBM - Comentei!

            }
        }

        private void CarregarConfinanciamentoConsolidadoDemandasExercicio3(int exercicio3, DradsPlanoMunicipalDemandasParlamentaresInfo vProtSocialExercicio3)
        {
            //var vProtSocialExercicio2 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == exercicio2).SingleOrDefault();

            if (vProtSocialExercicio3 != null)
            {
                txtProtecaoBasicaDemandasExercicio3.Text = (vProtSocialExercicio3.ValorProtecaoSocialBasicaDemandas.HasValue)
                    ? vProtSocialExercicio3.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoMediaDemandasExercicio2.Text = (vProtSocialExercicio3.ValorProtecaoSocialMediaDemandas.HasValue)
                    ? vProtSocialExercicio3.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoAltaDemandasExercicio2.Text = (vProtSocialExercicio3.ValorProtecaoSocialAltaDemandas.HasValue)
                    ? vProtSocialExercicio3.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : "0,00";

                //Beneficios eventuais
                //txtBeneficiosEventuaisDemandasExercicio2.Text = (vProtSocialExercicio2.ValorBeneficioEventuaisDemandas.HasValue)
                //    ? vProtSocialExercicio2.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : "0,00";

                txtProtecaoBasicaDemandasExercicio3.Enabled = false;
                txtProtecaoMediaDemandasExercicio3.Enabled = false;
                txtProtecaoAltaDemandasExercicio3.Enabled = false;
                txtBeneficiosEventuaisDemandasExercicio3.Enabled = false;
                txtSaoPauloSolidarioDemandasExercicio3.Enabled = false;
                //txtDescricao.Text = vProtSocialExercicio2.Descricao; //TODO:DBM - Comentei!

            }
        }

        private void CarregarConfinanciamentoConsolidadoReprogramadoDemandasExercicio1(int exercicio1
                                                                              , DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo vProtSocialExercicio1)
        {
            if (vProtSocialExercicio1 != null)
            {
                txtProtecaoBasicaDemandas.Text = (vProtSocialExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue) ? vProtSocialExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoMediaDemandas.Text = (vProtSocialExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue) ? vProtSocialExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoAltaDemandas.Text = (vProtSocialExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue) ? vProtSocialExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : "0,00";

                ///
                /// TODO:DBM:Beneficios Eventuais
                ///
                //txtBeneficiosEventuaisDemandas.Text = (vProtSocialExercicio1.ValorBeneficioEventuaisDemandas.HasValue) ? vProtSocialExercicio1.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : "0,00";

                txtProtecaoBasicaDemandas.Enabled = false;
                txtProtecaoMediaDemandas.Enabled = false;
                txtProtecaoAltaDemandas.Enabled = false;
                txtBeneficiosEventuaisDemandas.Enabled = false;
                txtSaoPauloSolidarioDemandas.Enabled = false;
            }
        }

        private void CarregarConfinanciamentoConsolidadoReprogramadoDemandasExercicio2(int exercicio1
                                                                              , DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo vProtSocialExercicio2)
        {
            if (vProtSocialExercicio2 != null)
            {
                txtProtecaoBasicaDemandasExercicio2.Text = (vProtSocialExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue) ? vProtSocialExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoMediaDemandasExercicio2.Text = (vProtSocialExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue) ? vProtSocialExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoAltaDemandasExercicio2.Text = (vProtSocialExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue) ? vProtSocialExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : "0,00";

                ///
                /// TODO:DBM:Beneficios Eventuais
                ///
                //txtBeneficiosEventuaisDemandas.Text = (vProtSocialExercicio1.ValorBeneficioEventuaisDemandas.HasValue) ? vProtSocialExercicio1.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : "0,00";

                txtProtecaoBasicaDemandas.Enabled = false;
                txtProtecaoMediaDemandas.Enabled = false;
                txtProtecaoAltaDemandas.Enabled = false;
                txtBeneficiosEventuaisDemandas.Enabled = false;
                txtSaoPauloSolidarioDemandas.Enabled = false;
            }
        }

        private void CarregarConfinanciamentoConsolidadoReprogramadoDemandasExercicio3(int exercicio1
                                                                              , DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo vProtSocialExercicio3)
        {
            if (vProtSocialExercicio3 != null)
            {
                txtProtecaoBasicaDemandasExercicio3.Text = (vProtSocialExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue) ? vProtSocialExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoMediaDemandasExercicio3.Text = (vProtSocialExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue) ? vProtSocialExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : "0,00";
                txtProtecaoAltaDemandasExercicio3.Text = (vProtSocialExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue) ? vProtSocialExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : "0,00";

                ///
                /// TODO:DBM:Beneficios Eventuais
                ///
                //txtBeneficiosEventuaisDemandas.Text = (vProtSocialExercicio1.ValorBeneficioEventuaisDemandas.HasValue) ? vProtSocialExercicio1.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : "0,00";

                txtProtecaoBasicaDemandas.Enabled = false;
                txtProtecaoMediaDemandas.Enabled = false;
                txtProtecaoAltaDemandas.Enabled = false;
                txtBeneficiosEventuaisDemandas.Enabled = false;
                txtSaoPauloSolidarioDemandas.Enabled = false;
            }
        }


        private void CarregarConfinanciamentoConsolidadoProgramaBeneficioExercicio1(int exercicio1
                                                                              , DradsPlanoMunicipalBeneficioProgramaRecursosInfo vProtSocialExercicio1)
        {
            if (vProtSocialExercicio1 != null)
            {
                txtBeneficiosEventuais.Text = (vProtSocialExercicio1.ValorBeneficiosEventuais != null ? vProtSocialExercicio1.ValorBeneficiosEventuais.Value : 0M).ToString("N2");
                txtSaoPauloSolidario.Text = (vProtSocialExercicio1.ValorProgramaProjeto != null ? vProtSocialExercicio1.ValorProgramaProjeto.Value : 0M).ToString("N2");

                //txtBeneficiosEventuais.Text = (vProtSocialExercicio1.ValorBeneficioEventuaisReprogramado.HasValue) ? vProtSocialExercicio1.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : "0,00";
                txtBeneficiosEventuaisReprogramado.Enabled = false;
                txtSaoPauloSolidarioReprogramado.Enabled = false;
            }
        }

        private void CarregarConfinanciamentoConsolidadoProgramaBeneficioExercicio2(int exercicio2
                                                                                    , DradsPlanoMunicipalBeneficioProgramaRecursosInfo vProtSocialExercicio2)
        {
            if (vProtSocialExercicio2 != null)
            {
                txtBeneficiosEventuaisExercicio2.Text = (vProtSocialExercicio2.ValorBeneficiosEventuais != null ? vProtSocialExercicio2.ValorBeneficiosEventuais.Value : 0M).ToString("N2");
                txtSaoPauloSolidarioExercicio2.Text = (vProtSocialExercicio2.ValorProgramaProjeto != null ? vProtSocialExercicio2.ValorProgramaProjeto.Value : 0M).ToString("N2");

                //Beneficios eventuais
                //txtBeneficiosEventuaisReprogramadoExercicio2.Text = (vProtSocialExercicio2.ValorBeneficioEventuaisReprogramado.HasValue)
                //    ? vProtSocialExercicio2.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : "0,00";

                //txtBeneficiosEventuaisReprogramadoExercicio2.Enabled = false;
                //txtSaoPauloSolidarioReprogramadoExercicio2.Enabled = false;
            }
        }

        private void CarregarConfinanciamentoConsolidadoProgramaBeneficioExercicio3(int exercicio3
                                                                            , DradsPlanoMunicipalBeneficioProgramaRecursosInfo vProtSocialExercicio3)
        {
            if (vProtSocialExercicio3 != null)
            {
                txtBeneficiosEventuaisExercicio3.Text = (vProtSocialExercicio3.ValorBeneficiosEventuais != null ? vProtSocialExercicio3.ValorBeneficiosEventuais.Value : 0M).ToString("N2");
                txtSaoPauloSolidarioExercicio3.Text = (vProtSocialExercicio3.ValorProgramaProjeto != null ? vProtSocialExercicio3.ValorProgramaProjeto.Value : 0M).ToString("N2");

                //Beneficios eventuais
                //txtBeneficiosEventuaisReprogramadoExercicio2.Text = (vProtSocialExercicio2.ValorBeneficioEventuaisReprogramado.HasValue)
                //    ? vProtSocialExercicio2.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : "0,00";

                //txtBeneficiosEventuaisReprogramadoExercicio2.Enabled = false;
                //txtSaoPauloSolidarioReprogramadoExercicio2.Enabled = false;
            }
        }

        #endregion



        #region Buscar do Historico

        public void CarregarDoHistorico(PrefeituraInfo prefeitura)
        {
            using (var proxy = new ProxyPlanoMunicipal())
            {
                List<ConsultaPlanoMunicipalHistoricoInfo> historico = proxy.Service.GetHistoricoPlanoMunicipalByPrefeitura(prefeitura.Id);

                if (!historico.Any()){ return; }
                var itensParaFinalizacao = historico.Where(x => x.IdSituacao == 4); //Para finalização
                if (itensParaFinalizacao == null){ return; }
                var itensParaFinalizacaoOrdenadoPorData = itensParaFinalizacao.OrderByDescending(x => x.Data);

                var historicoCompleto = proxy.Service.GetHistoricoPlanoMunicipalFullById(itensParaFinalizacaoOrdenadoPorData.First().Id);

                if (historicoCompleto.PlanosMunicipaisHistoricoConsolidados != null)
                {


                    #region Exercicio 1
                    var historicoValoresExercicio1 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == Exercicios[0]).SingleOrDefault();

                    if (historicoValoresExercicio1 != null)
                    {
                        txtProtecaoSocialBasica.Text = historicoValoresExercicio1.ValorProtecaoSocialBasica.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoSocialMedia.Text = historicoValoresExercicio1.ValorProtecaoSocialMediaComplexidade.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoSocialAlta.Text = historicoValoresExercicio1.ValorProtecaoSocialAltaComplexidade.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                        txtBeneficiosEventuais.Text = historicoValoresExercicio1.ValorBeneficiosEventuais.HasValue
                            ? historicoValoresExercicio1.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                        txtSaoPauloSolidario.Text = historicoValoresExercicio1.ValorProgramaProjetoSolidario.HasValue
                            ? historicoValoresExercicio1.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoBasicaReprogramado.Text = historicoValoresExercicio1.ValorProtecaoSocialBasicaReprogramado.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoMediaReprogramado.Text = historicoValoresExercicio1.ValorProtecaoSocialMediaReprogramado.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoAltaReprogramado.Text = historicoValoresExercicio1.ValorProtecaoSocialAltaReprogramado.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                        txtBeneficiosEventuaisReprogramado.Text = historicoValoresExercicio1.ValorBeneficioEventuaisReprogramado.HasValue
                            ? historicoValoresExercicio1.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                        txtSaoPauloSolidarioReprogramado.Text = historicoValoresExercicio1.ValorProgramaProjetoReprogramado.HasValue
                            ? historicoValoresExercicio1.ValorProgramaProjetoReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoBasicaDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialBasicaDemandas.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoMediaDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialMediaDemandas.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoAltaDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialAltaDemandas.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoBasicaReprogramacaoDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoMediaReprogramacaoDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        txtProtecaoAltaReprogramacaoDemandas.Text = historicoValoresExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue
                            ? historicoValoresExercicio1.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        txtBeneficiosEventuaisDemandas.Text = historicoValoresExercicio1.ValorBeneficioEventuaisDemandas.HasValue
                            ? historicoValoresExercicio1.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        txtBeneficiosEventuaisDemandasReprogramado.Text = historicoValoresExercicio1.ValorBeneficioEventuaisReprogramadoDemandas.HasValue
                            ? historicoValoresExercicio1.ValorBeneficioEventuaisReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        txtSaoPauloSolidarioDemandas.Text = historicoValoresExercicio1.ValorProgramaProjetoDemandas.HasValue
                            ? historicoValoresExercicio1.ValorProgramaProjetoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                    }

                    #endregion

                    #region Exercicio 2
                    var historicoValoresExercicio2 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == Exercicios[1]).SingleOrDefault();

                    if (historicoValoresExercicio2 != null)
                    {
                        if (!Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0DradsValores(FFluxo.Exercicios[1], prefeitura))
                        {
                            txtProtecaoSocialBasicaExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialBasica.HasValue
                              ? historicoValoresExercicio2.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoSocialMediaExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialMediaComplexidade.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoSocialAltaExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialAltaComplexidade.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisExercicio2.Text = historicoValoresExercicio2.ValorBeneficiosEventuais.HasValue
                                ? historicoValoresExercicio2.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                            txtSaoPauloSolidarioExercicio2.Text = historicoValoresExercicio2.ValorProgramaProjetoSolidario.HasValue
                                ? historicoValoresExercicio2.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");
                        }

                        if (!Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0DradsReprogramado(FFluxo.Exercicios[1], prefeitura))
                        {
                            txtProtecaoBasicaReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialBasicaReprogramado.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoMediaReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialMediaReprogramado.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoAltaReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialAltaReprogramado.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorBeneficioEventuaisReprogramado.HasValue
                                ? historicoValoresExercicio2.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtSaoPauloSolidarioReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorProgramaProjetoReprogramado.HasValue
                                ? historicoValoresExercicio2.ValorProgramaProjetoReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoBasicaReprogramacaoDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoMediaReprogramacaoDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoAltaReprogramacaoDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisDemandasReprogramadoExercicio2.Text = historicoValoresExercicio2.ValorBeneficioEventuaisReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorBeneficioEventuaisReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        }

                        if (!Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0DradsDemandas(FFluxo.Exercicios[1], prefeitura))
                        {
                            txtProtecaoBasicaDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialBasicaDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoMediaDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialMediaDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoAltaDemandasExercicio2.Text = historicoValoresExercicio2.ValorProtecaoSocialAltaDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisDemandasExercicio2.Text = historicoValoresExercicio2.ValorBeneficioEventuaisDemandas.HasValue
                                ? historicoValoresExercicio2.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtSaoPauloSolidarioDemandasExercicio2.Text = historicoValoresExercicio2.ValorProgramaProjetoDemandas.HasValue
                                ? historicoValoresExercicio2.ValorProgramaProjetoDemandas.Value.ToString("N2") : (0M).ToString("N2");
                        }


                    }

                    #endregion

                    #region Exercicio 3
                    var historicoValoresExercicio3 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == Exercicios[2]).SingleOrDefault();

                    if (historicoValoresExercicio3 != null)
                    {
                        if (!Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0DradsValores(FFluxo.Exercicios[2], prefeitura))
                        {
                            txtProtecaoSocialBasicaExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialBasica.HasValue
                              ? historicoValoresExercicio3.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoSocialMediaExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialMediaComplexidade.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoSocialAltaExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialAltaComplexidade.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisExercicio3.Text = historicoValoresExercicio3.ValorBeneficiosEventuais.HasValue
                                ? historicoValoresExercicio3.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                            txtSaoPauloSolidarioExercicio3.Text = historicoValoresExercicio3.ValorProgramaProjetoSolidario.HasValue
                                ? historicoValoresExercicio3.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");
                        }

                        if (!Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0DradsReprogramado(FFluxo.Exercicios[2], prefeitura))
                        {
                            txtProtecaoBasicaReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialBasicaReprogramado.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoMediaReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialMediaReprogramado.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoAltaReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialAltaReprogramado.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorBeneficioEventuaisReprogramado.HasValue
                                ? historicoValoresExercicio3.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtSaoPauloSolidarioReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorProgramaProjetoReprogramado.HasValue
                                ? historicoValoresExercicio3.ValorProgramaProjetoReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoBasicaReprogramacaoDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoMediaReprogramacaoDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoAltaReprogramacaoDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisDemandasReprogramadoExercicio3.Text = historicoValoresExercicio3.ValorBeneficioEventuaisReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorBeneficioEventuaisReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        }
                        if (!Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0DradsDemandas(FFluxo.Exercicios[2],prefeitura))
                        {
                            txtProtecaoBasicaDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialBasicaDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoMediaDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialMediaDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoAltaDemandasExercicio3.Text = historicoValoresExercicio3.ValorProtecaoSocialAltaDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisDemandasExercicio3.Text = historicoValoresExercicio3.ValorBeneficioEventuaisDemandas.HasValue
                                ? historicoValoresExercicio3.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtSaoPauloSolidarioDemandasExercicio3.Text = historicoValoresExercicio3.ValorProgramaProjetoDemandas.HasValue
                                ? historicoValoresExercicio3.ValorProgramaProjetoDemandas.Value.ToString("N2") : (0M).ToString("N2");                            
                        }

                    }

                    #endregion

                    #region Exercicio4
                    var historicoValoresExercicio4 = historicoCompleto.PlanosMunicipaisHistoricoConsolidados.Where(x => x.Exercicio == Exercicios[3]).SingleOrDefault();

                    if (historicoValoresExercicio4 != null)
                    {
                        if (!Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0DradsValores(FFluxo.Exercicios[2], prefeitura))
                        {
                            txtProtecaoSocialBasicaExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialBasica.HasValue
                              ? historicoValoresExercicio4.ValorProtecaoSocialBasica.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoSocialMediaExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialMediaComplexidade.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialMediaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoSocialAltaExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialAltaComplexidade.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialAltaComplexidade.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisExercicio4.Text = historicoValoresExercicio4.ValorBeneficiosEventuais.HasValue
                                ? historicoValoresExercicio4.ValorBeneficiosEventuais.Value.ToString("N2") : (0M).ToString("N2");

                            txtSaoPauloSolidarioExercicio4.Text = historicoValoresExercicio4.ValorProgramaProjetoSolidario.HasValue
                                ? historicoValoresExercicio4.ValorProgramaProjetoSolidario.Value.ToString("N2") : (0M).ToString("N2");
                        }

                        if (!Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0DradsReprogramado(FFluxo.Exercicios[2], prefeitura))
                        {
                            txtProtecaoBasicaReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialBasicaReprogramado.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialBasicaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoMediaReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialMediaReprogramado.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialMediaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoAltaReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialAltaReprogramado.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialAltaReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorBeneficioEventuaisReprogramado.HasValue
                                ? historicoValoresExercicio4.ValorBeneficioEventuaisReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtSaoPauloSolidarioReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorProgramaProjetoReprogramado.HasValue
                                ? historicoValoresExercicio4.ValorProgramaProjetoReprogramado.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoBasicaReprogramacaoDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialBasicaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoMediaReprogramacaoDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialMediaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoAltaReprogramacaoDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialAltaReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisDemandasReprogramadoExercicio4.Text = historicoValoresExercicio4.ValorBeneficioEventuaisReprogramadoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorBeneficioEventuaisReprogramadoDemandas.Value.ToString("N2") : (0M).ToString("N2");

                        }
                        if (!Permissao.Bloco0Inicio.VerificaPermissaoExercicioBloco0DradsDemandas(FFluxo.Exercicios[2], prefeitura))
                        {
                            txtProtecaoBasicaDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialBasicaDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialBasicaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoMediaDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialMediaDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialMediaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtProtecaoAltaDemandasExercicio4.Text = historicoValoresExercicio4.ValorProtecaoSocialAltaDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProtecaoSocialAltaDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtBeneficiosEventuaisDemandasExercicio4.Text = historicoValoresExercicio4.ValorBeneficioEventuaisDemandas.HasValue
                                ? historicoValoresExercicio4.ValorBeneficioEventuaisDemandas.Value.ToString("N2") : (0M).ToString("N2");

                            txtSaoPauloSolidarioDemandasExercicio4.Text = historicoValoresExercicio4.ValorProgramaProjetoDemandas.HasValue
                                ? historicoValoresExercicio4.ValorProgramaProjetoDemandas.Value.ToString("N2") : (0M).ToString("N2");
                        }
                    }
                    #endregion


                }

            }

        }

        #endregion

    }
}
