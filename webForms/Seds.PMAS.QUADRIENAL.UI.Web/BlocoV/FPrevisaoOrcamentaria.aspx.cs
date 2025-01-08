using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoV
{
    public partial class FPrevisaoOrcamentaria : System.Web.UI.Page
    {

        #region exercicio
        public static List<int> Exercicios = new List<int> { 2022, 2023, 2024, 2025 };
        #endregion

        public decimal transferenciaRendaMunicipal = 0;

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
                    var prefeituras = new Prefeituras(proxy);
                    carregarPrevisaoOrcamentaria(prefeituras);
                    carregarPrevisaoOrcamentaria2017(prefeituras);
                    carregarProgramasExercicio1(prefeituras);
                    carregarProgramasExercicio2(prefeituras);
                    carregarProgramasExercicio3(prefeituras);
                    carregarProgramasExercicio4(prefeituras);

                    carregarCofinanciamentoExercicio1(prefeituras);
                    carregarCofinanciamentoExercicio2(prefeituras);
                    carregarCofinanciamentoExercicio3(prefeituras);
                    carregarCofinanciamentoExercicio4(prefeituras);
                    carregarProgramas2017(prefeituras);
                }
            }
        }

        private void carregarCofinanciamentoExercicio1(Prefeituras prefeituras)
        {
            int exercicio = FPrevisaoOrcamentaria.Exercicios[0];
            decimal ProgramasMunicipal = 0;
            decimal ProgramasEstadual = 0;
            decimal ProgramasFederal = 0;
            decimal ProgramasPrivado = 0;

            decimal TranferenciaMunicipal = 0;
            decimal TranferenciaEstadual = 0;
            decimal TranferenciaFederal = 0;
            decimal TranferenciaPrivado = 0;
            decimal TranferenciaTotal = 0;


            using (var programaProjeto = new ProxyProgramas())
            {
                #region Programas

                #region obtem programas federais
                var programasProjetosFederais = programaProjeto.Service.GetConsultaProgramasProjetosFederaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);

                #region acessuas

                if (programasProjetosFederais.SingleOrDefault(t => t.Nome.ToLower().Contains("acessuas")) != null)
                {
                   var programaProjetoFederalAcessuas = programaProjeto.Service.GetProgramaProjetoById(programasProjetosFederais.SingleOrDefault(t => t.Nome.ToLower().Contains("acessuas")).Id);

                   programaProjetoFederalAcessuas.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetoFederalAcessuas.Id);

                   if (programaProjetoFederalAcessuas != null)
                   {
                       var recursoExercicio1 = programaProjetoFederalAcessuas.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                       if (recursoExercicio1 != null)
                       {
                           if (recursoExercicio1.ValorFMAS.HasValue)
                           {
                               ProgramasMunicipal += (recursoExercicio1.ValorFMAS.HasValue ? recursoExercicio1.ValorFMAS.Value : (0M))
                                                   + (recursoExercicio1.ValorOrcamentoMunicipal.HasValue ? recursoExercicio1.ValorOrcamentoMunicipal.Value : (0M))
                                                   + (recursoExercicio1.ValorFundoMunicipal.HasValue ? recursoExercicio1.ValorFundoMunicipal.Value : (0M));
                           }
                       }

                       if (programaProjetoFederalAcessuas.PrevisaoAnual != null)
                       {
                           if (programaProjetoFederalAcessuas.AderenciaACESSUAS.HasValue && programaProjetoFederalAcessuas.AderenciaACESSUAS.Value)
                           {
                               ProgramasFederal += programaProjetoFederalAcessuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio1;
                           }
                       }
                   }
                }
                #endregion

                #region primeira infancia no sua
                var programasProjetosFederal = programasProjetosFederais.Where(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ"));
                var programaProjetoFederal = programasProjetosFederal.SingleOrDefault();

                int id = (programaProjetoFederal != null) ? programaProjetoFederal.Id : 0;
                if (id > 0)
                {
                    var programaProjetoPrimeiraInfancia = programaProjeto.Service.GetProgramaProjetoById(id);
                    programaProjetoPrimeiraInfancia.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetoPrimeiraInfancia.Id);


                    if (programaProjetoPrimeiraInfancia != null)
                    {
                        var recursoPrimeiraInfancia = programaProjetoPrimeiraInfancia.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();


                        if (recursoPrimeiraInfancia != null)
                        {
                            
                           ProgramasMunicipal += ((recursoPrimeiraInfancia.FonteFMAS.HasValue
                            && recursoPrimeiraInfancia.FonteFMAS.Value
                            && recursoPrimeiraInfancia.ValorFMAS.HasValue ? recursoPrimeiraInfancia.ValorFMAS.Value : (0M))

                                                   + (recursoPrimeiraInfancia.FonteOrcamentoMunicipal.HasValue
                                                   && recursoPrimeiraInfancia.FonteOrcamentoMunicipal.Value
                                                   && recursoPrimeiraInfancia.ValorOrcamentoMunicipal.HasValue ? recursoPrimeiraInfancia.ValorOrcamentoMunicipal.Value : (0M))
                                                   + (recursoPrimeiraInfancia.FonteFundoMunicipal.HasValue
                                                   && recursoPrimeiraInfancia.FonteFundoMunicipal.Value
                                                   && recursoPrimeiraInfancia.ValorFundoMunicipal.HasValue ? recursoPrimeiraInfancia.ValorFundoMunicipal.Value : (0M))
                                                   );
                        }
                        else
                        {
                            ProgramasMunicipal = 0;
                        }



                        if (programaProjetoPrimeiraInfancia.PrevisaoAnual != null)
                        {
                            ProgramasFederal += programaProjetoPrimeiraInfancia.ExecutaPrograma ? programaProjetoPrimeiraInfancia.PrevisaoAnual.PrevisaoAnualRepasseExercicio1 : 0M;
                        }
                    }
                }
                #endregion

                #region obtem transferencia de renda
                var transferencia = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                #region PETI - Programa de Erradicação do Trabalho Infantil

                #region acoes peti
                var acoesPeti = programaProjeto.Service.GetTransferenciaRendaById(transferencia.SingleOrDefault(t => t.Nome.ToLower().Contains("peti")).Id);
                if (acoesPeti != null)
                {
                    ProgramasFederal += (acoesPeti.ValorAEPETI.HasValue ? acoesPeti.ValorAEPETI.Value * 12 : (0M));
                }
                #endregion
                #endregion
                #endregion

                #endregion

                #region obtem programas estaduais
                var programasProjetosEstadual = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region Amigo do idoso

                if (programasProjetosEstadual.SingleOrDefault(t => t.Nome.ToLower().Contains("amigo do idoso")) != null)
                {
                   
                ProgramaProjetoInfo programaProjetoAmigoIdoso = programaProjeto.Service.GetProgramaProjetoById(programasProjetosEstadual.SingleOrDefault(t => t.Nome.ToLower().Contains("amigo do idoso")).Id);
                //ProgramaProjetoInfo programaProjetoAmigoIdoso = null;
                if (programaProjetoAmigoIdoso != null)
                {
                    var exercicio1 = FPrevisaoOrcamentaria.Exercicios[0];
                    var exercicio2 = FPrevisaoOrcamentaria.Exercicios[1];

                    var parcela1 = programaProjetoAmigoIdoso.ProgramasProjetosParcelasInfo.Where(x => x.Exercicio == exercicio1).FirstOrDefault();
                    var parcela2 = programaProjetoAmigoIdoso.ProgramasProjetosParcelasInfo.Where(x => x.Exercicio == exercicio2).FirstOrDefault();

                    if (parcela1 != null)
                    {
                        ProgramasEstadual += (parcela1.ValorDiaIdoso.HasValue
                                                    && parcela1.AnoRepasseDiaIdoso.Value == exercicio1 ?
                                                    parcela1.ValorDiaIdoso.Value : 0M) + (parcela1.ValorConvivenciaIdoso.HasValue
                                                    && parcela1.AnoRepasseConvivenciaIdoso.Value == exercicio1 ? parcela1.ValorConvivenciaIdoso.Value : 0M);
                    }

                    if (parcela2 != null)
                    {
                        ProgramasEstadual += (parcela2.ValorDiaIdoso.HasValue
                                                    && parcela2.AnoRepasseDiaIdoso.Value == exercicio2 ?
                                                    parcela2.ValorDiaIdoso.Value : 0M) + (parcela2.ValorConvivenciaIdoso.HasValue
                                                    && parcela2.AnoRepasseConvivenciaIdoso.Value == exercicio2 ? parcela2.ValorConvivenciaIdoso.Value : 0M);
                    }
                }
                }
                #endregion

                #endregion

                #region Projeto federal

                var programasProjetosPrefeitura = programaProjeto.Service.GetConsultaProgramasProjetosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                foreach (var pm in programasProjetosPrefeitura)
                {
                    var programaProjetoPrefeitura = programaProjeto.Service.GetProgramaProjetoById(pm.Id);
                    var recursoProgramaProjeto = programaProjetoPrefeitura.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                    if (recursoProgramaProjeto != null)
                    {

                        ProgramasMunicipal += (recursoProgramaProjeto.ValorFMAS.HasValue ? recursoProgramaProjeto.ValorFMAS.Value : 0M)
                                              + (recursoProgramaProjeto.ValorOrcamentoMunicipal.HasValue ? recursoProgramaProjeto.ValorOrcamentoMunicipal.Value : 0M)
                                              + (recursoProgramaProjeto.ValorFundoMunicipal.HasValue ? recursoProgramaProjeto.ValorFundoMunicipal.Value : 0M);

                        ProgramasEstadual += (recursoProgramaProjeto.ValorFEAS.HasValue ? recursoProgramaProjeto.ValorFEAS.Value : 0M)
                                             + (recursoProgramaProjeto.ValorOrcamentoEstadual.HasValue ? recursoProgramaProjeto.ValorOrcamentoEstadual.Value : 0M)
                                             + (recursoProgramaProjeto.ValorFundoEstadual.HasValue ? recursoProgramaProjeto.ValorFundoEstadual.Value : 0M);

                        ProgramasFederal +=
                                             (recursoProgramaProjeto.ValorFNAS.HasValue ? recursoProgramaProjeto.ValorFNAS.Value : 0M)
                                            + (recursoProgramaProjeto.ValorOrcamentoFederal.HasValue ? recursoProgramaProjeto.ValorOrcamentoFederal.Value : 0M)
                                            + (recursoProgramaProjeto.ValorFundoFederal.HasValue ? recursoProgramaProjeto.ValorFundoFederal.Value : 0M)
                                            + (recursoProgramaProjeto.ValorIGDPBF.HasValue ? recursoProgramaProjeto.ValorIGDPBF.Value : 0M)
                                            + (recursoProgramaProjeto.ValorIGDSUAS.HasValue ? recursoProgramaProjeto.ValorIGDSUAS.Value : 0M);
                    }
                    else
                    {
                        ProgramasMunicipal = 0m;
                        ProgramasEstadual = 0m;
                        ProgramasFederal = 0m;
                    }

                    if (pm.TipoProgramaTransferencia == 2)
                    {
                        //pp = programaProjeto.Service.Get(pm.Id);
                        programaProjetoPrefeitura.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pm.Id);
                        if (programaProjetoPrefeitura.PrevisaoAnual != null)
                        {
                            TranferenciaMunicipal += programaProjetoPrefeitura.PrevisaoAnual.PrevisaoAnualMunicipalExercicio1;
                        }
                    }
                #endregion

                }
                                
                using (var proxy = new ProxyPrefeitura())
                {
                    var indice = proxy.Service.GetIndiceGestaoDescentralizadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                    if (indice != null)
                    {
                        var valorAnualIGD = indice.IGDPBFValorAnual.HasValue ? indice.IGDPBFValorAnual.Value : 0M;
                        var valorAnualSUAS = indice.IGDSUASValorAnual.HasValue ? indice.IGDSUASValorAnual.Value : 0M;

                        var incentivo = valorAnualIGD + valorAnualSUAS;
                        lblIncentivoGestaoFederal.Text = incentivo.ToString("N2");
                        lblIncentivoGestaoTotal.Text = incentivo.ToString("N2");
                    }
                }
                #endregion

                var lst = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio).ToList();

                var transferenciaRenda = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                var idoso = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso);
                var pcd = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia);

                #region Servicos
                decimal totalServicosSocioAssistenciaisMunicipal = 0;
                decimal totalServicosSocioAssistenciaisEstadual = 0;
                decimal totalServicosSocioAssistenciaisFederal = 0;
                decimal totalServicosSocioAssistenciaisPrivados = 0;
                decimal totalServicosSocioAssistenciaisTotal = 0;

                foreach (var item in lst)
                {
                    totalServicosSocioAssistenciaisEstadual += item.RedePrivadaEstadual + item.RedePublicaEstadual;
                    totalServicosSocioAssistenciaisMunicipal += item.RedePrivadaMunicipal + item.RedePublicaMunicipal;
                    totalServicosSocioAssistenciaisFederal += item.RedePrivadaFederal + item.RedePublicaFederal;
                    totalServicosSocioAssistenciaisPrivados += item.Privado;
                }


                totalServicosSocioAssistenciaisTotal = totalServicosSocioAssistenciaisMunicipal
                                                        + totalServicosSocioAssistenciaisEstadual
                                                        + totalServicosSocioAssistenciaisFederal
                                                        + totalServicosSocioAssistenciaisPrivados;


                lblServicosSocioAssMunicipal.Text = totalServicosSocioAssistenciaisMunicipal.ToString("N2");
                lblServicosSocioAssEstadual.Text = totalServicosSocioAssistenciaisEstadual.ToString("N2");
                lblServicosSocioAssFederal.Text = totalServicosSocioAssistenciaisFederal.ToString("N2");
                lblServicosSocioAssPrivados.Text = totalServicosSocioAssistenciaisPrivados.ToString("N2");
                lblServicosSocioAssTotal.Text = totalServicosSocioAssistenciaisTotal.ToString("N2");
                #endregion

                #region Beneficio
                decimal BeneficiosMunicipal = 0;
                decimal BeneficiosEstadual = 0;
                decimal BeneficiosFederal = 0;
                decimal BeneficiosPrivados = 0;
                decimal BeneficiosTotal = 0;

                var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(x => x.Exercicio == exercicio).FirstOrDefault();
                if (beneficio != null)
                {
                    BeneficiosMunicipal += beneficio.ValorAnualMunicipal;
                    BeneficiosEstadual += beneficio.ValorAnualEstadual;
                    BeneficiosFederal += beneficio.ValorAnualFederal;
                    BeneficiosPrivados += beneficio.ValorAnualPrivado;
                    BeneficiosMunicipal += pcd.ValorAnualMunicipal;
                    BeneficiosEstadual += pcd.ValorAnualEstadual;
                    BeneficiosFederal += pcd.ValorAnualFederal;
                    BeneficiosPrivados += pcd.ValorAnualPrivado;
                    if (idoso != null)
                    {
                        BeneficiosMunicipal += idoso.ValorAnualMunicipal;
                        BeneficiosEstadual += idoso.ValorAnualEstadual;
                        BeneficiosFederal += idoso.ValorAnualFederal;
                        BeneficiosPrivados += idoso.ValorAnualPrivado;
                    }
                }


                BeneficiosTotal = BeneficiosMunicipal + BeneficiosEstadual + BeneficiosFederal + BeneficiosPrivados;

                lblBeneficiosMunicipal.Text = BeneficiosMunicipal.ToString("N2");
                lblBeneficiosEstadual.Text = BeneficiosEstadual.ToString("N2");
                lblBeneficiosFederal.Text = BeneficiosFederal.ToString("N2");
                lblBeneficiosPrivados.Text = BeneficiosPrivados.ToString("N2");
                lblBeneficiosTotal.Text = BeneficiosTotal.ToString("N2");

                #endregion

                #region Transferencia

                #region Previsão Ação Jovem?

                var previsaoAcaoJovem = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.AcaoJovem).Id);
                if (previsaoAcaoJovem != null)
                {
                    TranferenciaEstadual += previsaoAcaoJovem.CalculoAcaoRendaPrevisaoAnualExercicio1.HasValue ? previsaoAcaoJovem.CalculoAcaoRendaPrevisaoAnualExercicio1.Value : (0M);
                } 
                #endregion

                #region Previsão Renda Cidadã ?

                var renda = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.RendaCidada).Id);
                if (renda != null)
                {
                    TranferenciaEstadual += renda.CalculoAcaoRendaPrevisaoAnualExercicio1.HasValue ? renda.CalculoAcaoRendaPrevisaoAnualExercicio1.Value : (0M);
                } 
                #endregion

                #region Prospera Familia
                var prosperaFamilia = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.ProsperaFamilia).Id);
            
                if (prosperaFamilia != null)
                {
                    TranferenciaEstadual += prosperaFamilia.ValorRepasseEstadual2022.HasValue ? prosperaFamilia.ValorRepasseEstadual2022.Value : (0M);   
                }
                #endregion

                #region Previsão Amigo do Idoso (ESTADUAL ??) (MetaPactuadaExercicio1 ??)

                using (var programaIdoso = new ProxyProgramas())
                {
                    var pIdoso = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);

                    //DBM:: Verificar
                    var programaIdosos = pIdoso.Where(t => t.Nome.ToLower().Contains("amigo do idoso")).SingleOrDefault();
                    if (programaIdosos != null)
                    {
                        var ppIdoso = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaIdosos.Id);

                        if (ppIdoso != null)
                        {
                            TranferenciaEstadual += ((ppIdoso.MetaPactuadaExercicio1) * 100M * 12);
                        }
                    }
                } 
                #endregion

                #region Previsão BPC - idoso (FEDERAL) (2018 ? BPCPrevisaoAnual2018)
                var previsaoAmigoIdoso = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso).Id);
                if (previsaoAmigoIdoso != null)
                {
                    TranferenciaFederal += previsaoAmigoIdoso.CalculoBPCPrevisaoAnualExercicio1.HasValue ? previsaoAmigoIdoso.CalculoBPCPrevisaoAnualExercicio1.Value : 0M;
                } 
                #endregion

                #region Previsão Bolsa Família (2021 ? RepasseMensal2021)

                if (transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BolsaFamilia) != null)
                {
                    var bolsa = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BolsaFamilia).Id);
                    if (bolsa != null)
                    {
                        TranferenciaFederal += bolsa.RepasseMensal2021.HasValue ? (bolsa.RepasseMensal2021.Value * 12) : 0M;
                    }
                }
                #endregion

                #region Previsão BPC Pessoa Deficiencia  (2018 ? BPCPrevisaoAnual2018)
                var bpcdeficientes = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia).Id);
                if (bpcdeficientes != null)
                {
                    TranferenciaFederal += bpcdeficientes.CalculoBPCPrevisaoAnualExercicio1.HasValue ? bpcdeficientes.CalculoBPCPrevisaoAnualExercicio1.Value : 0M;
                }

                #endregion

                #region Previsão PETI (Extinto?)
                var peti = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 4);
                if (peti != null)
                {
                    TranferenciaFederal += peti.ValorAnualFederal;
                } 
                #endregion

                #region totais transferencia
                TranferenciaTotal = TranferenciaMunicipal + TranferenciaEstadual + TranferenciaFederal + TranferenciaPrivado;
                lblTransferenciaRendaMunicipal.Text = TranferenciaMunicipal.ToString("N2");
                lblTransferenciaRendaEstadual.Text = TranferenciaEstadual.ToString("N2");
                lblTransferenciaRendaFederal.Text = TranferenciaFederal.ToString("N2");
                lblTransferenciaRendaPrivados.Text = TranferenciaPrivado.ToString("N2");
                lblTransferenciaRendaTotal.Text = TranferenciaTotal.ToString("N2");

                #endregion
                #endregion



                #region Totais


                ProgramasEstadual = +Convert.ToDecimal(lblProgDesenvTotalEstadual.Text);

                lblProgramasMunicipal.Text = ProgramasMunicipal.ToString("N2");
                lblProgramasEstadual.Text = ProgramasEstadual.ToString("N2");
                lblProgramasFederal.Text = ProgramasFederal.ToString("N2");
                lblProgramasPrivado.Text = ProgramasPrivado.ToString("N2");
                
                
                var totalMunicipal = Convert.ToDecimal(String.IsNullOrEmpty(lblServicosSocioAssMunicipal.Text) ? "0" : lblServicosSocioAssMunicipal.Text)
                                              + Convert.ToDecimal(String.IsNullOrEmpty(lblBeneficiosMunicipal.Text) ? "0" : lblBeneficiosMunicipal.Text)
                                              + Convert.ToDecimal(String.IsNullOrEmpty(lblTransferenciaRendaMunicipal.Text) ? "0" : lblTransferenciaRendaMunicipal.Text)
                                              + Convert.ToDecimal(String.IsNullOrEmpty(lblIncentivoGestaoMunicipal.Text) ? "0" : lblIncentivoGestaoMunicipal.Text)
                                              + Convert.ToDecimal(String.IsNullOrEmpty(lblProgramasMunicipal.Text) ? "0" : lblProgramasMunicipal.Text);

                var totalEstado = Convert.ToDecimal(lblServicosSocioAssEstadual.Text)
                                    + Convert.ToDecimal(lblBeneficiosEstadual.Text)
                                    + Convert.ToDecimal(lblTransferenciaRendaEstadual.Text)
                                    + Convert.ToDecimal(lblIncentivoGestaoEstadual.Text)
                                    + Convert.ToDecimal(lblProgramasEstadual.Text);

                var totalFederal = Convert.ToDecimal(lblServicosSocioAssFederal.Text)
                    + Convert.ToDecimal(lblBeneficiosFederal.Text)
                    + Convert.ToDecimal(lblTransferenciaRendaFederal.Text)
                    + Convert.ToDecimal(lblIncentivoGestaoFederal.Text)
                    + Convert.ToDecimal(lblProgramasFederal.Text);

                var totalPrivados = Convert.ToDecimal(lblServicosSocioAssPrivados.Text)
                    + Convert.ToDecimal(lblBeneficiosPrivados.Text)
                    + Convert.ToDecimal(lblTransferenciaRendaPrivados.Text)
                    + Convert.ToDecimal(lblIncentivoGestaoPrivados.Text)
                    + Convert.ToDecimal(lblProgramasPrivado.Text);
                
                
                var total = ProgramasMunicipal + ProgramasEstadual + ProgramasFederal + ProgramasPrivado;


                lblProgramasTotal.Text = total.ToString("N2");

                //totalEstado = totalEstado + ProgramasEstadual;

                lblTotalResumoGeralMunicipal.Text = totalMunicipal.ToString("N2");
                lblTotalResumoGeralEstadual.Text = totalEstado.ToString("N2");
                lblTotalResumoGeralFederal.Text = totalFederal.ToString("N2");
                lblTotalResumoGeralPrivados.Text = totalPrivados.ToString("N2");
                lblTotalResumoGeralTotal.Text = (totalMunicipal + totalEstado + totalFederal + totalPrivados).ToString("N2");


                #endregion

            }
        }

        private void carregarCofinanciamentoExercicio2(Prefeituras prefeituras)
        {
            int exercicio = FPrevisaoOrcamentaria.Exercicios[1];

            decimal programasMunicipalExercicio2 = 0;
            decimal programasEstadualExercicio2 = 0;
            decimal programasFederalExercicio2 = 0;
            decimal programasPrivadoExercicio2 = 0;

            decimal tranferenciaMunicipalExercicio2 = 0;
            decimal tranferenciaEstadualExercicio2 = 0;
            decimal tranferenciaFederalExercicio2 = 0;
            decimal tranferenciaPrivadoExercicio2 = 0;
            decimal tranferenciaTotalExercicio2 = 0;


            using (var programaProjeto = new ProxyProgramas())
            {
                #region Programas

                #region obtem programas federais
                var programasProjetosFederais = programaProjeto.Service.GetConsultaProgramasProjetosFederaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region acessuas

                if (programasProjetosFederais.SingleOrDefault(t => t.Nome.ToLower().Contains("acessuas")) != null)
                {
                    var programaProjetoFederalAcessuas = programaProjeto.Service.GetProgramaProjetoById(programasProjetosFederais.SingleOrDefault(t => t.Nome.ToLower().Contains("acessuas")).Id);

                    programaProjetoFederalAcessuas.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetoFederalAcessuas.Id);

                    if (programaProjetoFederalAcessuas != null)
                    {
                        var recursoExercicio2 = programaProjetoFederalAcessuas.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                        if (recursoExercicio2 != null)
                        {
                            if (recursoExercicio2.ValorFMAS.HasValue)
                            {
                                programasMunicipalExercicio2 += (recursoExercicio2.ValorFMAS.HasValue ? recursoExercicio2.ValorFMAS.Value : (0M))
                                                    + (recursoExercicio2.ValorOrcamentoMunicipal.HasValue ? recursoExercicio2.ValorOrcamentoMunicipal.Value : (0M))
                                                    + (recursoExercicio2.ValorFundoMunicipal.HasValue ? recursoExercicio2.ValorFundoMunicipal.Value : (0M));
                            }
                        }

                        if (programaProjetoFederalAcessuas.PrevisaoAnual != null)
                        {
                            if (programaProjetoFederalAcessuas.AderenciaACESSUAS.HasValue && programaProjetoFederalAcessuas.AderenciaACESSUAS.Value)
                            {
                                programasFederalExercicio2 += programaProjetoFederalAcessuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio2;
                            }
                        }
                    }
                }
                #endregion

                #region primeira infancia no sua

                if (programasProjetosFederais.SingleOrDefault(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ")) != null)
                {

                    var programaProjetoPrimeiraInfancia = programaProjeto.Service.GetProgramaProjetoById(programasProjetosFederais.SingleOrDefault(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ")).Id);

                    programaProjetoPrimeiraInfancia.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetoPrimeiraInfancia.Id);

                    if (programaProjetoPrimeiraInfancia != null)
                    {
                        var recursoPrimeiraInfancia = programaProjetoPrimeiraInfancia.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                        if (recursoPrimeiraInfancia != null)
                        {
                            programasMunicipalExercicio2 += ((recursoPrimeiraInfancia.FonteFMAS.HasValue
                                && recursoPrimeiraInfancia.FonteFMAS.Value
                                && recursoPrimeiraInfancia.ValorFMAS.HasValue ? recursoPrimeiraInfancia.ValorFMAS.Value : (0M))

                                                       + (recursoPrimeiraInfancia.FonteOrcamentoMunicipal.HasValue
                                                       && recursoPrimeiraInfancia.FonteOrcamentoMunicipal.Value
                                                       && recursoPrimeiraInfancia.ValorOrcamentoMunicipal.HasValue ? recursoPrimeiraInfancia.ValorOrcamentoMunicipal.Value : (0M))
                                                       + (recursoPrimeiraInfancia.FonteFundoMunicipal.HasValue
                                                       && recursoPrimeiraInfancia.FonteFundoMunicipal.Value
                                                       && recursoPrimeiraInfancia.ValorFundoMunicipal.HasValue ? recursoPrimeiraInfancia.ValorFundoMunicipal.Value : (0M))
                                                       );
                        }
                        if (programaProjetoPrimeiraInfancia.PrevisaoAnual != null)
                        {
                            programasFederalExercicio2 += programaProjetoPrimeiraInfancia.ExecutaPrograma ? programaProjetoPrimeiraInfancia.PrevisaoAnual.PrevisaoAnualRepasseExercicio2 : 0M;
                        }
                    }
                }
                #endregion

                #region obtem transferencia de renda
                var transferencia = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                #region PETI - Programa de Erradicação do Trabalho Infantil

                #region acoes peti
                var acoesPeti = programaProjeto.Service.GetTransferenciaRendaById(transferencia.SingleOrDefault(t => t.Nome.ToLower().Contains("peti")).Id);
                if (acoesPeti != null)
                {
                    programasFederalExercicio2 += (acoesPeti.ValorAEPETI.HasValue ? acoesPeti.ValorAEPETI.Value * 12 : (0M));
                }
                #endregion
                #endregion
                #endregion

                #endregion

                #region obtem programas estaduais
                var programasProjetosEstadual = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region Amigo do idoso

                var exercicio2 = FPrevisaoOrcamentaria.Exercicios[1];

                ConsultaProgramaProjetoInfo consultaProgramaProjetoAmigoIdoso = programasProjetosEstadual.Where(p => p.Nome.ToLower().Contains("amigo do idoso")).FirstOrDefault();
                if (consultaProgramaProjetoAmigoIdoso != null)
                {
                    ProgramaProjetoInfo programaProjetoAmigoIdoso = programaProjeto.Service.GetProgramaProjetoById(consultaProgramaProjetoAmigoIdoso.Id);
                    if (programaProjetoAmigoIdoso != null)
                    {
                        
                        var parcela2 = programaProjetoAmigoIdoso.ProgramasProjetosParcelasInfo.Where(x => x.Exercicio == exercicio2).FirstOrDefault();
                        if (parcela2 != null)
                        {
                            programasEstadualExercicio2 += (parcela2.ValorDiaIdoso.HasValue
                                                        && parcela2.AnoRepasseDiaIdoso.Value == exercicio2 ?
                                                        parcela2.ValorDiaIdoso.Value : 0M) + (parcela2.ValorConvivenciaIdoso.HasValue
                                                        && parcela2.AnoRepasseConvivenciaIdoso.Value == exercicio2 ? parcela2.ValorConvivenciaIdoso.Value : 0M);
                        }
                    }
                }
                #endregion

                #endregion

                #region Projeto federal

                var programasProjetosPrefeitura = programaProjeto.Service.GetConsultaProgramasProjetosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                foreach (var pm in programasProjetosPrefeitura)
                {
                    var programaProjetoPrefeitura = programaProjeto.Service.GetProgramaProjetoById(pm.Id);
                    var recursoProgramaProjeto = programaProjetoPrefeitura.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                    if (recursoProgramaProjeto != null)
                    {
                        programasMunicipalExercicio2 += (recursoProgramaProjeto.ValorFMAS.HasValue ? recursoProgramaProjeto.ValorFMAS.Value : 0M)
                                              + (recursoProgramaProjeto.ValorOrcamentoMunicipal.HasValue ? recursoProgramaProjeto.ValorOrcamentoMunicipal.Value : 0M)
                                              + (recursoProgramaProjeto.ValorFundoMunicipal.HasValue ? recursoProgramaProjeto.ValorFundoMunicipal.Value : 0M);

                        programasEstadualExercicio2 += (recursoProgramaProjeto.ValorFEAS.HasValue ? recursoProgramaProjeto.ValorFEAS.Value : 0M)
                                             + (recursoProgramaProjeto.ValorOrcamentoEstadual.HasValue ? recursoProgramaProjeto.ValorOrcamentoEstadual.Value : 0M)
                                             + (recursoProgramaProjeto.ValorFundoEstadual.HasValue ? recursoProgramaProjeto.ValorFundoEstadual.Value : 0M);

                        programasFederalExercicio2 +=
                                             (recursoProgramaProjeto.ValorFNAS.HasValue ? recursoProgramaProjeto.ValorFNAS.Value : 0M)
                                            + (recursoProgramaProjeto.ValorOrcamentoFederal.HasValue ? recursoProgramaProjeto.ValorOrcamentoFederal.Value : 0M)
                                            + (recursoProgramaProjeto.ValorFundoFederal.HasValue ? recursoProgramaProjeto.ValorFundoFederal.Value : 0M)
                                            + (recursoProgramaProjeto.ValorIGDPBF.HasValue ? recursoProgramaProjeto.ValorIGDPBF.Value : 0M)
                                            + (recursoProgramaProjeto.ValorIGDSUAS.HasValue ? recursoProgramaProjeto.ValorIGDSUAS.Value : 0M);
                    }

                    if (pm.TipoProgramaTransferencia == 2)
                    {
                        //pp = programaProjeto.Service.Get(pm.Id);
                        programaProjetoPrefeitura.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pm.Id);
                        if (programaProjetoPrefeitura.PrevisaoAnual != null)
                        {
                            tranferenciaMunicipalExercicio2 += programaProjetoPrefeitura.PrevisaoAnual.PrevisaoAnualMunicipalExercicio2;
                        }
                    }
                #endregion

                }

                using (var proxy = new ProxyPrefeitura())
                {
                    var indice = proxy.Service.GetIndiceGestaoDescentralizadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                    if (indice != null)
                    {
                        var valorAnualIGD = indice.IGDPBFValorAnual.HasValue ? indice.IGDPBFValorAnual.Value : 0M;
                        var valorAnualSUAS = indice.IGDSUASValorAnual.HasValue ? indice.IGDSUASValorAnual.Value : 0M;

                        var incentivo = valorAnualIGD + valorAnualSUAS;
                        lblIncentivoGestaoFederalExercicio2.Text = incentivo.ToString("N2");
                        lblIncentivoGestaoTotalExercicio2.Text = incentivo.ToString("N2");
                    }
                }
                #endregion

                var lst = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id,exercicio) ;

                var transferenciaRenda = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                var idoso = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 1);
                var pcd = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 2);

                #region Servicos
                decimal totalServicosSocioAssistenciaisMunicipal = 0;
                decimal totalServicosSocioAssistenciaisEstadual = 0;
                decimal totalServicosSocioAssistenciaisFederal = 0;
                decimal totalServicosSocioAssistenciaisPrivados = 0;
                decimal totalServicosSocioAssistenciaisTotal = 0;

                foreach (var item in lst)
                {
                    totalServicosSocioAssistenciaisEstadual += item.RedePrivadaEstadual + item.RedePublicaEstadual;
                    totalServicosSocioAssistenciaisMunicipal += item.RedePrivadaMunicipal + item.RedePublicaMunicipal;
                    totalServicosSocioAssistenciaisFederal += item.RedePrivadaFederal + item.RedePublicaFederal;
                    totalServicosSocioAssistenciaisPrivados += item.Privado;
                }


                totalServicosSocioAssistenciaisTotal = totalServicosSocioAssistenciaisMunicipal
                                                        + totalServicosSocioAssistenciaisEstadual
                                                        + totalServicosSocioAssistenciaisFederal
                                                        + totalServicosSocioAssistenciaisPrivados;


                lblServicosSocioAssMunicipalExercicio2.Text = totalServicosSocioAssistenciaisMunicipal.ToString("N2");
                lblServicosSocioAssEstadualExercicio2.Text = totalServicosSocioAssistenciaisEstadual.ToString("N2");
                lblServicosSocioAssFederalExercicio2.Text = totalServicosSocioAssistenciaisFederal.ToString("N2");
                lblServicosSocioAssPrivadosExercicio2.Text = totalServicosSocioAssistenciaisPrivados.ToString("N2");
                lblServicosSocioAssTotalExercicio2.Text = totalServicosSocioAssistenciaisTotal.ToString("N2");
                #endregion


                #region Beneficio
                decimal BeneficiosMunicipal = 0;
                decimal BeneficiosEstadual = 0;
                decimal BeneficiosFederal = 0;
                decimal BeneficiosPrivados = 0;
                decimal BeneficiosTotal = 0;

                //var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(x => x.Exercicio == exercicio).FirstOrDefault();

                if (beneficio != null)
                {
                    BeneficiosMunicipal += beneficio.ValorAnualMunicipal;
                    BeneficiosEstadual += beneficio.ValorAnualEstadual;
                    BeneficiosFederal += beneficio.ValorAnualFederal;
                    BeneficiosPrivados += beneficio.ValorAnualPrivado;
                    BeneficiosMunicipal += pcd.ValorAnualMunicipal;
                    BeneficiosEstadual += pcd.ValorAnualEstadual;
                    BeneficiosFederal += pcd.ValorAnualFederal;
                    BeneficiosPrivados += pcd.ValorAnualPrivado;

                    if (idoso != null)
                    {
                        BeneficiosMunicipal += idoso.ValorAnualMunicipal;
                        BeneficiosEstadual += idoso.ValorAnualEstadual;
                        BeneficiosFederal += idoso.ValorAnualFederal;
                        BeneficiosPrivados += idoso.ValorAnualPrivado;
                    }


                    BeneficiosTotal = BeneficiosMunicipal + BeneficiosEstadual + BeneficiosFederal + BeneficiosPrivados;

                    lblBeneficiosMunicipalExercicio2.Text = BeneficiosMunicipal.ToString("N2");
                    lblBeneficiosEstadualExercicio2.Text = BeneficiosEstadual.ToString("N2");
                    lblBeneficiosFederalExercicio2.Text = BeneficiosFederal.ToString("N2");
                    lblBeneficiosPrivadosExercicio2.Text = BeneficiosPrivados.ToString("N2");
                    lblBeneficiosTotalExercicio2.Text = BeneficiosTotal.ToString("N2");
                }

                #endregion


                #region Transferencia

                #region Previsão Acao Jovem (CalculoAcaoRendaPrevisaoAnualExercicio1)
                var previsaoAcaoJovem = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.AcaoJovem).Id);
                if (previsaoAcaoJovem != null)
                {
                    tranferenciaEstadualExercicio2 += previsaoAcaoJovem.CalculoAcaoRendaPrevisaoAnualExercicio2.HasValue ? previsaoAcaoJovem.CalculoAcaoRendaPrevisaoAnualExercicio2.Value : (0M);
                }

                #endregion

                #region Previsão Renda Cidadã (CalculoAcaoRendaPrevisaoAnualExercicio1)
                var previsaoRendaCidada = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.RendaCidada).Id);
                if (previsaoRendaCidada != null)
                {
                    tranferenciaEstadualExercicio2 += previsaoRendaCidada.CalculoAcaoRendaPrevisaoAnualExercicio2.HasValue ? previsaoRendaCidada.CalculoAcaoRendaPrevisaoAnualExercicio2.Value : (0M);
                } 
                #endregion

               #region ProsperaFamilia

                var prosperaFamilia = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.ProsperaFamilia).Id);

                if (prosperaFamilia != null)
                {
                    tranferenciaEstadualExercicio2 += prosperaFamilia.ValorRepasseEstadual2023.HasValue ? prosperaFamilia.ValorRepasseEstadual2023.Value : (0M);
                }

               #endregion


                #region Previsão Amigo Idoso (ESTADUAL) (MetaPactuadaExercicio2)
                using (var programaIdoso = new ProxyProgramas())
                {
                    var pIdoso = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                    if (pIdoso != null)
                    {
                        var pIdosoSingle = pIdoso.Where(t => t.Nome.ToLower().Contains("amigo do idoso")).FirstOrDefault();
                        if (pIdosoSingle != null)
                        {
                            int pIdosoSingleId = pIdosoSingle.Id;

                            var ppIdoso = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pIdosoSingleId);
                            if (ppIdoso != null)
                            {
                                tranferenciaEstadualExercicio2 += ((ppIdoso.MetaPactuadaExercicio2) * 100M * 12);
                            }
                        }
                    }
                } 
                #endregion

                #region Previsão BPC - Idoso (FEDERAL) (CalculoBPCPrevisaoAnualExercicio2)
                var previsaoAmigoIdoso = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso).Id);
                if (previsaoAmigoIdoso != null)
                {
                    tranferenciaFederalExercicio2 += previsaoAmigoIdoso.CalculoBPCPrevisaoAnualExercicio2.HasValue ? previsaoAmigoIdoso.CalculoBPCPrevisaoAnualExercicio2.Value : 0M;
                } 
                #endregion

                #region Previsao BPC Deficientes (CalculoBPCPrevisaoAnualExercicio2)
                var previsaoBPCdeficientes = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia).Id);
                if (previsaoBPCdeficientes != null)
                {
                    tranferenciaFederalExercicio2 += previsaoBPCdeficientes.CalculoBPCPrevisaoAnualExercicio2.HasValue ? previsaoBPCdeficientes.CalculoBPCPrevisaoAnualExercicio2.Value : 0M;
                }
                #endregion

                #region Previsão Bolsa Família (RepasseMensal2022)
                if (transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BolsaFamilia) != null)
                {
                    var previsaoBolsaFamilia = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BolsaFamilia).Id);
                    if (previsaoBolsaFamilia != null)
                    {
                        tranferenciaFederalExercicio2 += previsaoBolsaFamilia.RepasseMensal2022.HasValue ? (previsaoBolsaFamilia.RepasseMensal2022.Value * 12) : 0M;
                    }
                }
                #endregion

                #region Previsão PETI
                var peti = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 4);
                if (peti != null)
                {
                    tranferenciaFederalExercicio2 += peti.ValorAnualFederal;
                } 
                #endregion

                #region totais transferencia
                tranferenciaTotalExercicio2 = tranferenciaMunicipalExercicio2 + tranferenciaEstadualExercicio2 + tranferenciaFederalExercicio2 + tranferenciaPrivadoExercicio2;
                lblTransferenciaRendaMunicipalExercicio2.Text = tranferenciaMunicipalExercicio2.ToString("N2");
                lblTransferenciaRendaEstadualExercicio2.Text = tranferenciaEstadualExercicio2.ToString("N2");
                lblTransferenciaRendaFederalExercicio2.Text = tranferenciaFederalExercicio2.ToString("N2");
                lblTransferenciaRendaPrivadosExercicio2.Text = tranferenciaPrivadoExercicio2.ToString("N2");
                lblTransferenciaRendaTotalExercicio2.Text = tranferenciaTotalExercicio2.ToString("N2"); 
                #endregion

                #endregion


                programasEstadualExercicio2 = +Convert.ToDecimal(lblProgDesenvTotalEstadualExercicio2.Text);

                lblProgramasMunicipalExercicio2.Text = programasMunicipalExercicio2.ToString("N2");
                lblProgramasEstadualExercicio2.Text = programasEstadualExercicio2.ToString("N2");
                lblProgramasFederalExercicio2.Text = programasFederalExercicio2.ToString("N2");
                lblProgramasPrivadoExercicio2.Text = programasPrivadoExercicio2.ToString("N2");

                var totalMunicipal = string.IsNullOrEmpty(lblServicosSocioAssMunicipalExercicio2.Text) ? 0M : Convert.ToDecimal(lblServicosSocioAssMunicipalExercicio2.Text)
                                        + (string.IsNullOrEmpty(lblBeneficiosMunicipalExercicio2.Text) ? 0M : Convert.ToDecimal(lblBeneficiosMunicipalExercicio2.Text))
                                        + (string.IsNullOrEmpty(lblTransferenciaRendaMunicipalExercicio2.Text) ? 0M : Convert.ToDecimal(lblTransferenciaRendaMunicipalExercicio2.Text))
                                        + (string.IsNullOrEmpty(lblIncentivoGestaoMunicipalExercicio2.Text) ? 0M : Convert.ToDecimal(lblIncentivoGestaoMunicipalExercicio2.Text))
                                        + (string.IsNullOrEmpty(lblProgramasMunicipalExercicio2.Text) ? 0M : Convert.ToDecimal(lblProgramasMunicipalExercicio2.Text));

                var totalEstado = string.IsNullOrEmpty(lblServicosSocioAssEstadualExercicio2.Text) ? 0M : Convert.ToDecimal(lblServicosSocioAssEstadualExercicio2.Text)
                                    + (string.IsNullOrEmpty(lblBeneficiosEstadualExercicio2.Text) ? 0M : Convert.ToDecimal(lblBeneficiosEstadualExercicio2.Text))
                                    + (string.IsNullOrEmpty(lblTransferenciaRendaEstadualExercicio2.Text) ? 0M : Convert.ToDecimal(lblTransferenciaRendaEstadualExercicio2.Text))
                                    + (string.IsNullOrEmpty(lblIncentivoGestaoEstadualExercicio2.Text) ? 0M : Convert.ToDecimal(lblIncentivoGestaoEstadualExercicio2.Text))
                                    + (string.IsNullOrEmpty(lblProgramasEstadualExercicio2.Text) ? 0M : Convert.ToDecimal(lblProgramasEstadualExercicio2.Text));

                var totalFederal = Convert.ToDecimal(lblServicosSocioAssFederalExercicio2.Text)
                    + Convert.ToDecimal(lblBeneficiosFederalExercicio2.Text)
                    + Convert.ToDecimal(lblTransferenciaRendaFederalExercicio2.Text)
                    + Convert.ToDecimal(lblIncentivoGestaoFederalExercicio2.Text)
                    + Convert.ToDecimal(lblProgramasFederalExercicio2.Text);

                var totalPrivados = Convert.ToDecimal(lblServicosSocioAssPrivadosExercicio2.Text)
                    + Convert.ToDecimal(lblBeneficiosPrivadosExercicio2.Text)
                    + Convert.ToDecimal(lblTransferenciaRendaPrivadosExercicio2.Text)
                    + Convert.ToDecimal(lblIncentivoGestaoPrivadosExercicio2.Text)
                    + Convert.ToDecimal(lblProgramasPrivadoExercicio2.Text);






                var total = programasMunicipalExercicio2 + programasEstadualExercicio2 + programasFederalExercicio2 + programasPrivadoExercicio2;

                lblProgramasTotalExercicio2.Text = total.ToString("N2");

                //totalEstado = totalEstado + programasEstadualExercicio2;

                lblTotalResumoGeralMunicipalExercicio2.Text = totalMunicipal.ToString("N2");
                lblTotalResumoGeralEstadualExercicio2.Text = totalEstado.ToString("N2");
                lblTotalResumoGeralFederalExercicio2.Text = totalFederal.ToString("N2");
                lblTotalResumoGeralPrivadosExercicio2.Text = totalPrivados.ToString("N2");
                lblTotalResumoGeralTotalExercicio2.Text = (totalMunicipal + totalEstado + totalFederal + totalPrivados).ToString("N2");

            }
        }

        private void carregarCofinanciamentoExercicio3(Prefeituras prefeituras)
        {
            int exercicio = FPrevisaoOrcamentaria.Exercicios[2];

            decimal programasMunicipalExercicio3 = 0;
            decimal programasEstadualExercicio3 = 0;
            decimal programasFederalExercicio3 = 0;
            decimal programasPrivadoExercicio3 = 0;

            decimal tranferenciaMunicipalExercicio3 = 0;
            decimal tranferenciaEstadualExercicio3 = 0;
            decimal tranferenciaFederalExercicio3 = 0;
            decimal tranferenciaPrivadoExercicio3 = 0;
            decimal tranferenciaTotalExercicio3 = 0;


            using (var programaProjeto = new ProxyProgramas())
            {
                #region Programas

                #region obtem programas federais
                var programasProjetosFederais = programaProjeto.Service.GetConsultaProgramasProjetosFederaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region acessuas

                if (programasProjetosFederais.SingleOrDefault(t => t.Nome.ToLower().Contains("acessuas")) != null)
                {
                    var programaProjetoFederalAcessuas = programaProjeto.Service.GetProgramaProjetoById(programasProjetosFederais.SingleOrDefault(t => t.Nome.ToLower().Contains("acessuas")).Id);

                    programaProjetoFederalAcessuas.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetoFederalAcessuas.Id);

                    if (programaProjetoFederalAcessuas != null)
                    {
                        var recursoExercicio3 = programaProjetoFederalAcessuas.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                        if (recursoExercicio3 != null)
                        {
                            if (recursoExercicio3.ValorFMAS.HasValue)
                            {
                                programasMunicipalExercicio3 += (recursoExercicio3.ValorFMAS.HasValue ? recursoExercicio3.ValorFMAS.Value : (0M))
                                                    + (recursoExercicio3.ValorOrcamentoMunicipal.HasValue ? recursoExercicio3.ValorOrcamentoMunicipal.Value : (0M))
                                                    + (recursoExercicio3.ValorFundoMunicipal.HasValue ? recursoExercicio3.ValorFundoMunicipal.Value : (0M));
                            }
                        }

                        if (programaProjetoFederalAcessuas.PrevisaoAnual != null)
                        {
                            if (programaProjetoFederalAcessuas.AderenciaACESSUAS.HasValue && programaProjetoFederalAcessuas.AderenciaACESSUAS.Value)
                            {
                                programasFederalExercicio3 += programaProjetoFederalAcessuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio3;
                            }
                        }
                    }
                }
                #endregion

                #region primeira infancia no sua

                if (programasProjetosFederais.SingleOrDefault(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ")) != null)
                {
                    var programaProjetoPrimeiraInfancia = programaProjeto.Service.GetProgramaProjetoById(programasProjetosFederais.SingleOrDefault(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ")).Id);

                        programaProjetoPrimeiraInfancia.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetoPrimeiraInfancia.Id);

                        if (programaProjetoPrimeiraInfancia != null)
                        {
                            var recursoPrimeiraInfancia = programaProjetoPrimeiraInfancia.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                            if (recursoPrimeiraInfancia != null)
                            {
                                programasMunicipalExercicio3 += ((recursoPrimeiraInfancia.FonteFMAS.HasValue
                                    && recursoPrimeiraInfancia.FonteFMAS.Value
                                    && recursoPrimeiraInfancia.ValorFMAS.HasValue ? recursoPrimeiraInfancia.ValorFMAS.Value : (0M))

                                                           + (recursoPrimeiraInfancia.FonteOrcamentoMunicipal.HasValue
                                                           && recursoPrimeiraInfancia.FonteOrcamentoMunicipal.Value
                                                           && recursoPrimeiraInfancia.ValorOrcamentoMunicipal.HasValue ? recursoPrimeiraInfancia.ValorOrcamentoMunicipal.Value : (0M))
                                                           + (recursoPrimeiraInfancia.FonteFundoMunicipal.HasValue
                                                           && recursoPrimeiraInfancia.FonteFundoMunicipal.Value
                                                           && recursoPrimeiraInfancia.ValorFundoMunicipal.HasValue ? recursoPrimeiraInfancia.ValorFundoMunicipal.Value : (0M))
                                                           );

                                if (programaProjetoPrimeiraInfancia.PrevisaoAnual != null)
                                {
                                    programasFederalExercicio3 += programaProjetoPrimeiraInfancia.ExecutaPrograma ? programaProjetoPrimeiraInfancia.PrevisaoAnual.PrevisaoAnualRepasseExercicio3 : 0M;
                                }
                            }

                        }

                }
                #endregion

                #region obtem transferencia de renda
                var transferencia = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                #region PETI - Programa de Erradicação do Trabalho Infantil

                #region acoes peti
                var acoesPeti = programaProjeto.Service.GetTransferenciaRendaById(transferencia.SingleOrDefault(t => t.Nome.ToLower().Contains("peti")).Id);
                if (acoesPeti != null)
                {
                    programasFederalExercicio3 += 0;//(acoesPeti.ValorAEPETI.HasValue ? acoesPeti.ValorAEPETI.Value * 12 : (0M));
                }
                #endregion
                #endregion
                #endregion

                #endregion

                #region obtem programas estaduais
                var programasProjetosEstadual = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region Amigo do idoso

                var Exercicio3 = FPrevisaoOrcamentaria.Exercicios[2];

                ConsultaProgramaProjetoInfo consultaProgramaProjetoAmigoIdoso = programasProjetosEstadual.Where(p => p.Nome.ToLower().Contains("amigo do idoso")).FirstOrDefault();
                if (consultaProgramaProjetoAmigoIdoso != null)
                {
                    ProgramaProjetoInfo programaProjetoAmigoIdoso = programaProjeto.Service.GetProgramaProjetoById(consultaProgramaProjetoAmigoIdoso.Id);
                    if (programaProjetoAmigoIdoso != null)
                    {

                        var parcela2 = programaProjetoAmigoIdoso.ProgramasProjetosParcelasInfo.Where(x => x.Exercicio == Exercicio3).FirstOrDefault();
                        if (parcela2 != null)
                        {
                            programasEstadualExercicio3 += (parcela2.ValorDiaIdoso.HasValue
                                                        && parcela2.AnoRepasseDiaIdoso.Value == Exercicio3 ?
                                                        parcela2.ValorDiaIdoso.Value : 0M) + (parcela2.ValorConvivenciaIdoso.HasValue
                                                        && parcela2.AnoRepasseConvivenciaIdoso.Value == Exercicio3 ? parcela2.ValorConvivenciaIdoso.Value : 0M);
                        }
                    }
                }
                #endregion

                #endregion

                #region Projeto federal

                var programasProjetosPrefeitura = programaProjeto.Service.GetConsultaProgramasProjetosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                foreach (var pm in programasProjetosPrefeitura)
                {
                    var programaProjetoPrefeitura = programaProjeto.Service.GetProgramaProjetoById(pm.Id);
                    var recursoProgramaProjeto = programaProjetoPrefeitura.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                    if (recursoProgramaProjeto != null)
                    {
                        programasMunicipalExercicio3 += (recursoProgramaProjeto.ValorFMAS.HasValue ? recursoProgramaProjeto.ValorFMAS.Value : 0M)
                                              + (recursoProgramaProjeto.ValorOrcamentoMunicipal.HasValue ? recursoProgramaProjeto.ValorOrcamentoMunicipal.Value : 0M)
                                              + (recursoProgramaProjeto.ValorFundoMunicipal.HasValue ? recursoProgramaProjeto.ValorFundoMunicipal.Value : 0M);

                        programasEstadualExercicio3 += (recursoProgramaProjeto.ValorFEAS.HasValue ? recursoProgramaProjeto.ValorFEAS.Value : 0M)
                                             + (recursoProgramaProjeto.ValorOrcamentoEstadual.HasValue ? recursoProgramaProjeto.ValorOrcamentoEstadual.Value : 0M)
                                             + (recursoProgramaProjeto.ValorFundoEstadual.HasValue ? recursoProgramaProjeto.ValorFundoEstadual.Value : 0M);

                        programasFederalExercicio3 +=
                                             (recursoProgramaProjeto.ValorFNAS.HasValue ? recursoProgramaProjeto.ValorFNAS.Value : 0M)
                                            + (recursoProgramaProjeto.ValorOrcamentoFederal.HasValue ? recursoProgramaProjeto.ValorOrcamentoFederal.Value : 0M)
                                            + (recursoProgramaProjeto.ValorFundoFederal.HasValue ? recursoProgramaProjeto.ValorFundoFederal.Value : 0M)
                                            + (recursoProgramaProjeto.ValorIGDPBF.HasValue ? recursoProgramaProjeto.ValorIGDPBF.Value : 0M)
                                            + (recursoProgramaProjeto.ValorIGDSUAS.HasValue ? recursoProgramaProjeto.ValorIGDSUAS.Value : 0M);
                    }

                    if (pm.TipoProgramaTransferencia == 2)
                    {
                        //pp = programaProjeto.Service.Get(pm.Id);
                        programaProjetoPrefeitura.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pm.Id);
                        if (programaProjetoPrefeitura.PrevisaoAnual != null)
                        {
                            tranferenciaMunicipalExercicio3 += programaProjetoPrefeitura.PrevisaoAnual.PrevisaoAnualMunicipalExercicio3;
                        }
                    }
                #endregion

                }


                using (var proxy = new ProxyPrefeitura())
                {
                    var indice = proxy.Service.GetIndiceGestaoDescentralizadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                    if (indice != null)
                    {
                        var valorAnualIGD = indice.IGDPBFValorAnual.HasValue ? indice.IGDPBFValorAnual.Value : 0M;
                        var valorAnualSUAS = indice.IGDSUASValorAnual.HasValue ? indice.IGDSUASValorAnual.Value : 0M;

                        var incentivo = valorAnualIGD + valorAnualSUAS;
                        lblIncentivoGestaoFederalExercicio3.Text = incentivo.ToString("N2");
                        lblIncentivoGestaoTotalExercicio3.Text = incentivo.ToString("N2");
                    }
                }
                #endregion

                var lst = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                var transferenciaRenda = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                var idoso = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso);
                var pcd = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia);

                #region Servicos
                decimal totalServicosSocioAssistenciaisMunicipal = 0;
                decimal totalServicosSocioAssistenciaisEstadual = 0;
                decimal totalServicosSocioAssistenciaisFederal = 0;
                decimal totalServicosSocioAssistenciaisPrivados = 0;
                decimal totalServicosSocioAssistenciaisTotal = 0;

                foreach (var item in lst)
                {
                    totalServicosSocioAssistenciaisEstadual += item.RedePrivadaEstadual + item.RedePublicaEstadual;
                    totalServicosSocioAssistenciaisMunicipal += item.RedePrivadaMunicipal + item.RedePublicaMunicipal;
                    totalServicosSocioAssistenciaisFederal += item.RedePrivadaFederal + item.RedePublicaFederal;
                    totalServicosSocioAssistenciaisPrivados += item.Privado;
                }


                totalServicosSocioAssistenciaisTotal = totalServicosSocioAssistenciaisMunicipal
                                                        + totalServicosSocioAssistenciaisEstadual
                                                        + totalServicosSocioAssistenciaisFederal
                                                        + totalServicosSocioAssistenciaisPrivados;


                lblServicosSocioAssMunicipalExercicio3.Text = totalServicosSocioAssistenciaisMunicipal.ToString("N2");
                lblServicosSocioAssEstadualExercicio3.Text = totalServicosSocioAssistenciaisEstadual.ToString("N2");
                lblServicosSocioAssFederalExercicio3.Text = totalServicosSocioAssistenciaisFederal.ToString("N2");
                lblServicosSocioAssPrivadosExercicio3.Text = totalServicosSocioAssistenciaisPrivados.ToString("N2");
                lblServicosSocioAssTotalExercicio3.Text = totalServicosSocioAssistenciaisTotal.ToString("N2");
                #endregion

                #region Beneficio
                decimal BeneficiosMunicipal = 0;
                decimal BeneficiosEstadual = 0;
                decimal BeneficiosFederal = 0;
                decimal BeneficiosPrivados = 0;
                decimal BeneficiosTotal = 0;

                //var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(x => x.Exercicio == exercicio).FirstOrDefault();

                if (beneficio != null)
                {
                    BeneficiosMunicipal += beneficio.ValorAnualMunicipal;
                    BeneficiosEstadual += beneficio.ValorAnualEstadual;
                    BeneficiosFederal += beneficio.ValorAnualFederal;
                    BeneficiosPrivados += beneficio.ValorAnualPrivado;
                    BeneficiosMunicipal += pcd.ValorAnualMunicipal;
                    BeneficiosEstadual += pcd.ValorAnualEstadual;
                    BeneficiosFederal += pcd.ValorAnualFederal;
                    BeneficiosPrivados += pcd.ValorAnualPrivado;

                    if (idoso != null)
                    {
                        BeneficiosMunicipal += idoso.ValorAnualMunicipal;
                        BeneficiosEstadual += idoso.ValorAnualEstadual;
                        BeneficiosFederal += idoso.ValorAnualFederal;
                        BeneficiosPrivados += idoso.ValorAnualPrivado;
                    }


                    BeneficiosTotal = BeneficiosMunicipal + BeneficiosEstadual + BeneficiosFederal + BeneficiosPrivados;

                    lblBeneficiosMunicipalExercicio3.Text = BeneficiosMunicipal.ToString("N2");
                    lblBeneficiosEstadualExercicio3.Text = BeneficiosEstadual.ToString("N2");
                    lblBeneficiosFederalExercicio3.Text = BeneficiosFederal.ToString("N2");
                    lblBeneficiosPrivadosExercicio3.Text = BeneficiosPrivados.ToString("N2");
                    lblBeneficiosTotalExercicio3.Text = BeneficiosTotal.ToString("N2");
                }

                #endregion

                #region Transferencia

                #region Previsão Ação Jovem (CalculoAcaoRendaPrevisaoAnualExercicio2)
                var previsaoAcaoJovem = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.AcaoJovem).Id);
                if (previsaoAcaoJovem != null)
                {
                   // tranferenciaEstadualExercicio3 += previsaoAcaoJovem.CalculoAcaoRendaPrevisaoAnualExercicio2.HasValue ? previsaoAcaoJovem.CalculoAcaoRendaPrevisaoAnualExercicio2.Value : (0M);
                    tranferenciaEstadualExercicio3 = (Convert.ToDecimal(lblAcaoJovemEstadualExercicio3.Text) + Convert.ToDecimal(lblRendaCidadaEstadualExercicio3.Text) + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoEstadualExercicio3.Text) + Convert.ToDecimal(lblPFEstadualExercicio3.Text));
                } 
                #endregion

                #region Previsão Renda Cidadã (CalculoAcaoRendaPrevisaoAnualExercicio2)
                var previsaoRendaCidada = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.RendaCidada).Id);
                if (previsaoRendaCidada != null)
                {
                    //tranferenciaEstadualExercicio3 += previsaoRendaCidada.CalculoAcaoRendaPrevisaoAnualExercicio3.HasValue ? previsaoRendaCidada.CalculoAcaoRendaPrevisaoAnualExercicio3.Value : (0M);
                    tranferenciaEstadualExercicio3 = (Convert.ToDecimal(lblAcaoJovemEstadualExercicio3.Text) + Convert.ToDecimal(lblRendaCidadaEstadualExercicio3.Text) + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoEstadualExercicio3.Text) + Convert.ToDecimal(lblPFEstadualExercicio3.Text));
                } 
                #endregion

                #region Previsão Amigo do Idoso (ESTADUAL ??) (MetaPactuadaExercicio3) ??
               /* using (var programaIdoso = new ProxyProgramas())
                {
                    var previsaoAmigoIdosoEstadual = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                    if (previsaoAmigoIdosoEstadual != null)
                    {
                        var pIdosoSingle = previsaoAmigoIdosoEstadual.Where(t => t.Nome.ToLower().Contains("amigo do idoso")).FirstOrDefault();
                        if (pIdosoSingle != null)
                        {
                            int pIdosoSingleId = pIdosoSingle.Id;

                            var ppIdoso = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pIdosoSingleId);
                            if (ppIdoso != null)
                            {
                                tranferenciaEstadualExercicio3 += ((ppIdoso.MetaPactuadaExercicio3) * 100M * 12);
                            }
                        }
                    }
                } */
                #endregion

                #region Previsão BPC - Idoso (FEDERAL) (CalculoBPCPrevisaoAnualExercicio3)
                var previsaoBPCFederal = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso).Id);
                if (previsaoBPCFederal != null)
                {
                    tranferenciaFederalExercicio3 += previsaoBPCFederal.CalculoBPCPrevisaoAnualExercicio3.HasValue ? previsaoBPCFederal.CalculoBPCPrevisaoAnualExercicio3.Value : 0M;
                }
                #endregion

                #region Previsão BPC Deficientes (CalculoBPCPrevisaoAnualExercicio3)
                var previsaoBPCdeficientes = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia).Id);
                if (previsaoBPCdeficientes != null)
                {
                    tranferenciaFederalExercicio3 += previsaoBPCdeficientes.CalculoBPCPrevisaoAnualExercicio3.HasValue ? previsaoBPCdeficientes.CalculoBPCPrevisaoAnualExercicio3.Value : 0M;
                }
                #endregion

                #region Previsão Bolsa Familia (RepasseMensal2023)
                if (transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BolsaFamilia) != null)
                {
                    var previsaoBolsaFamilia = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BolsaFamilia).Id);
                    if (previsaoBolsaFamilia != null)
                    {
                        tranferenciaFederalExercicio3 += previsaoBolsaFamilia.RepasseMensal2023.HasValue ? (previsaoBolsaFamilia.RepasseMensal2023.Value * 12) : 0M;
                    }
                }
                #endregion

                #region Previsão PETI
                var peti = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 4);
                if (peti != null)
                {
                    tranferenciaFederalExercicio3 += peti.ValorAnualFederal;
                }

                #endregion

                #region Totais Transferencias
                tranferenciaTotalExercicio3 = tranferenciaMunicipalExercicio3 + tranferenciaEstadualExercicio3 + tranferenciaFederalExercicio3 + tranferenciaPrivadoExercicio3;
                lblTransferenciaRendaMunicipalExercicio3.Text = tranferenciaMunicipalExercicio3.ToString("N2");
                lblTransferenciaRendaEstadualExercicio3.Text = tranferenciaEstadualExercicio3.ToString("N2");
                lblTransferenciaRendaFederalExercicio3.Text = tranferenciaFederalExercicio3.ToString("N2");
                lblTransferenciaRendaPrivadosExercicio3.Text = tranferenciaPrivadoExercicio3.ToString("N2");
                lblTransferenciaRendaTotalExercicio3.Text = tranferenciaTotalExercicio3.ToString("N2");

                #endregion

                #endregion

                #region Totais

                var totalMunicipal = string.IsNullOrEmpty(lblServicosSocioAssMunicipalExercicio3.Text) ? 0M : Convert.ToDecimal(lblServicosSocioAssMunicipalExercicio3.Text)
                                        + (string.IsNullOrEmpty(lblBeneficiosMunicipalExercicio3.Text) ? 0M : Convert.ToDecimal(lblBeneficiosMunicipalExercicio3.Text))
                                        + (string.IsNullOrEmpty(lblTransferenciaRendaMunicipalExercicio3.Text) ? 0M : Convert.ToDecimal(lblTransferenciaRendaMunicipalExercicio3.Text))
                                        + (string.IsNullOrEmpty(lblIncentivoGestaoMunicipalExercicio3.Text) ? 0M : Convert.ToDecimal(lblIncentivoGestaoMunicipalExercicio3.Text))
                                        + (string.IsNullOrEmpty(lblProgramasMunicipalExercicio3.Text) ? 0M : Convert.ToDecimal(lblProgramasMunicipalExercicio3.Text));

                var totalEstado = string.IsNullOrEmpty(lblServicosSocioAssEstadualExercicio3.Text) ? 0M : Convert.ToDecimal(lblServicosSocioAssEstadualExercicio3.Text)
                                    + (string.IsNullOrEmpty(lblBeneficiosEstadualExercicio3.Text) ? 0M : Convert.ToDecimal(lblBeneficiosEstadualExercicio3.Text))
                                    + (string.IsNullOrEmpty(lblTransferenciaRendaEstadualExercicio3.Text) ? 0M : Convert.ToDecimal(lblTransferenciaRendaEstadualExercicio3.Text))
                                    + (string.IsNullOrEmpty(lblIncentivoGestaoEstadualExercicio3.Text) ? 0M : Convert.ToDecimal(lblIncentivoGestaoEstadualExercicio3.Text))
                                    + (string.IsNullOrEmpty(lblProgramasEstadualExercicio3.Text) ? 0M : Convert.ToDecimal(lblProgramasEstadualExercicio3.Text));

                var totalFederal = Convert.ToDecimal(lblServicosSocioAssFederalExercicio3.Text)
                    + Convert.ToDecimal(lblBeneficiosFederalExercicio3.Text)
                    + Convert.ToDecimal(lblTransferenciaRendaFederalExercicio3.Text)
                    + Convert.ToDecimal(lblIncentivoGestaoFederalExercicio3.Text)
                    + Convert.ToDecimal(lblProgramasFederalExercicio3.Text);

                var totalPrivados = Convert.ToDecimal(lblServicosSocioAssPrivadosExercicio3.Text)
                    + Convert.ToDecimal(lblBeneficiosPrivadosExercicio3.Text)
                    + Convert.ToDecimal(lblTransferenciaRendaPrivadosExercicio3.Text)
                    + Convert.ToDecimal(lblIncentivoGestaoPrivadosExercicio3.Text)
                    + Convert.ToDecimal(lblProgramasPrivadoExercicio3.Text);

                programasEstadualExercicio3 =+ Convert.ToDecimal(lblProgDesenvTotalEstadualExercicio3.Text);

                var total = programasMunicipalExercicio3 + programasEstadualExercicio3 + programasFederalExercicio3 + programasPrivadoExercicio3;
                programasEstadualExercicio3 =+ Convert.ToDecimal(lblProgDesenvTotalEstadualExercicio3.Text);


                totalMunicipal += programasMunicipalExercicio3;
                totalEstado += programasEstadualExercicio3;
                totalFederal += programasFederalExercicio3;
                totalPrivados += programasPrivadoExercicio3;

                lblProgramasMunicipalExercicio3.Text = programasMunicipalExercicio3.ToString("N2");
                lblProgramasEstadualExercicio3.Text = programasEstadualExercicio3.ToString("N2");
                lblProgramasFederalExercicio3.Text = programasFederalExercicio3.ToString("N2");
                lblProgramasPrivadoExercicio3.Text = programasPrivadoExercicio3.ToString("N2");

                lblProgramasTotalExercicio3.Text = total.ToString("N2");

                lblTotalResumoGeralMunicipalExercicio3.Text = (totalMunicipal).ToString("N2");
                lblTotalResumoGeralEstadualExercicio3.Text = (totalEstado).ToString("N2");
                lblTotalResumoGeralFederalExercicio3.Text =  (totalFederal).ToString("N2");
                lblTotalResumoGeralPrivadosExercicio3.Text =  (totalPrivados).ToString("N2");
                lblTotalResumoGeralTotalExercicio3.Text = (totalMunicipal + totalEstado + totalFederal + totalPrivados).ToString("N2"); 
                #endregion

            }
        }


        private void carregarCofinanciamentoExercicio4(Prefeituras prefeituras)
        {
            int exercicio = FPrevisaoOrcamentaria.Exercicios[3];

            decimal programasMunicipalExercicio4 = 0;
            decimal programasEstadualExercicio4 = 0;
            decimal programasFederalExercicio4 = 0;
            decimal programasPrivadoExercicio4 = 0;

            decimal tranferenciaMunicipalExercicio4 = 0;
            decimal tranferenciaEstadualExercicio4 = 0;
            decimal tranferenciaFederalExercicio4 = 0;
            decimal tranferenciaPrivadoExercicio4 = 0;
            decimal tranferenciaTotalExercicio4 = 0;


            using (var programaProjeto = new ProxyProgramas())
            {
                #region Programas

                #region obtem programas federais
                var programasProjetosFederais = programaProjeto.Service.GetConsultaProgramasProjetosFederaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region acessuas


                if (programasProjetosFederais.SingleOrDefault(t => t.Nome.ToLower().Contains("acessuas")) != null)
                {
                    var programaProjetoFederalAcessuas = programaProjeto.Service.GetProgramaProjetoById(programasProjetosFederais.SingleOrDefault(t => t.Nome.ToLower().Contains("acessuas")).Id);

                    programaProjetoFederalAcessuas.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetoFederalAcessuas.Id);

                    if (programaProjetoFederalAcessuas != null)
                    {
                        var recursoExercicio4 = programaProjetoFederalAcessuas.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                        if (recursoExercicio4 != null)
                        {
                            if (recursoExercicio4.ValorFMAS.HasValue)
                            {
                                programasMunicipalExercicio4 += (recursoExercicio4.ValorFMAS.HasValue ? recursoExercicio4.ValorFMAS.Value : (0M))
                                                    + (recursoExercicio4.ValorOrcamentoMunicipal.HasValue ? recursoExercicio4.ValorOrcamentoMunicipal.Value : (0M))
                                                    + (recursoExercicio4.ValorFundoMunicipal.HasValue ? recursoExercicio4.ValorFundoMunicipal.Value : (0M));
                            }
                        }

                        if (programaProjetoFederalAcessuas.PrevisaoAnual != null)
                        {
                            if (programaProjetoFederalAcessuas.AderenciaACESSUAS.HasValue && programaProjetoFederalAcessuas.AderenciaACESSUAS.Value)
                            {
                                programasFederalExercicio4 +=  programaProjetoFederalAcessuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio4;
                            }
                        }
                    }
                }                


                #endregion

                #region primeira infancia no sua
                if (programasProjetosFederais.SingleOrDefault(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ")) != null)
                {
                    var programaProjetoPrimeiraInfancia = programaProjeto.Service.GetProgramaProjetoById(programasProjetosFederais.SingleOrDefault(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ")).Id);

                    programaProjetoPrimeiraInfancia.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetoPrimeiraInfancia.Id);

                    if (programaProjetoPrimeiraInfancia != null)
                    {
                        var recursoPrimeiraInfancia = programaProjetoPrimeiraInfancia.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                        if (recursoPrimeiraInfancia != null)
                        {
                            programasMunicipalExercicio4 += ((recursoPrimeiraInfancia.FonteFMAS.HasValue
                                && recursoPrimeiraInfancia.FonteFMAS.Value
                                && recursoPrimeiraInfancia.ValorFMAS.HasValue ? recursoPrimeiraInfancia.ValorFMAS.Value : (0M))

                                                       + (recursoPrimeiraInfancia.FonteOrcamentoMunicipal.HasValue
                                                       && recursoPrimeiraInfancia.FonteOrcamentoMunicipal.Value
                                                       && recursoPrimeiraInfancia.ValorOrcamentoMunicipal.HasValue ? recursoPrimeiraInfancia.ValorOrcamentoMunicipal.Value : (0M))
                                                       + (recursoPrimeiraInfancia.FonteFundoMunicipal.HasValue
                                                       && recursoPrimeiraInfancia.FonteFundoMunicipal.Value
                                                       && recursoPrimeiraInfancia.ValorFundoMunicipal.HasValue ? recursoPrimeiraInfancia.ValorFundoMunicipal.Value : (0M))
                                                       );

                            if (programaProjetoPrimeiraInfancia.PrevisaoAnual != null)
                            {
                                programasFederalExercicio4 += programaProjetoPrimeiraInfancia.ExecutaPrograma ? programaProjetoPrimeiraInfancia.PrevisaoAnual.PrevisaoAnualRepasseExercicio4 : 0M;
                            }
                        }
                    }
                }
                #endregion

                #region obtem transferencia de renda
                var transferencia = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                #region PETI - Programa de Erradicação do Trabalho Infantil

                #region acoes peti
                var acoesPeti = programaProjeto.Service.GetTransferenciaRendaById(transferencia.SingleOrDefault(t => t.Nome.ToLower().Contains("peti")).Id);
                if (acoesPeti != null)
                {
                    programasFederalExercicio4 += 0; //(acoesPeti.ValorAEPETI.HasValue ? acoesPeti.ValorAEPETI.Value * 12 : (0M));
                }
                #endregion
                #endregion
                #endregion

                #endregion

                #region obtem programas estaduais
                var programasProjetosEstadual = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region Amigo do idoso

                var Exercicio4 = FPrevisaoOrcamentaria.Exercicios[3];

                ConsultaProgramaProjetoInfo consultaProgramaProjetoAmigoIdoso = programasProjetosEstadual.Where(p => p.Nome.ToLower().Contains("amigo do idoso")).FirstOrDefault();
                if (consultaProgramaProjetoAmigoIdoso != null)
                {
                    ProgramaProjetoInfo programaProjetoAmigoIdoso = programaProjeto.Service.GetProgramaProjetoById(consultaProgramaProjetoAmigoIdoso.Id);
                    if (programaProjetoAmigoIdoso != null)
                    {

                        var parcela2 = programaProjetoAmigoIdoso.ProgramasProjetosParcelasInfo.Where(x => x.Exercicio == Exercicio4).FirstOrDefault();
                        if (parcela2 != null)
                        {
                            programasEstadualExercicio4 += (parcela2.ValorDiaIdoso.HasValue
                                                        && parcela2.AnoRepasseDiaIdoso.Value == Exercicio4 ?
                                                        parcela2.ValorDiaIdoso.Value : 0M) + (parcela2.ValorConvivenciaIdoso.HasValue
                                                        && parcela2.AnoRepasseConvivenciaIdoso.Value == Exercicio4 ? parcela2.ValorConvivenciaIdoso.Value : 0M);
                        }
                    }
                }
                #endregion

                #endregion

                #region Projeto federal

                var programasProjetosPrefeitura = programaProjeto.Service.GetConsultaProgramasProjetosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                foreach (var pm in programasProjetosPrefeitura)
                {
                    var programaProjetoPrefeitura = programaProjeto.Service.GetProgramaProjetoById(pm.Id);
                    var recursoProgramaProjeto = programaProjetoPrefeitura.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                    if (recursoProgramaProjeto != null)
                    {
                        programasMunicipalExercicio4 += (recursoProgramaProjeto.ValorFMAS.HasValue ? recursoProgramaProjeto.ValorFMAS.Value : 0M)
                                              + (recursoProgramaProjeto.ValorOrcamentoMunicipal.HasValue ? recursoProgramaProjeto.ValorOrcamentoMunicipal.Value : 0M)
                                              + (recursoProgramaProjeto.ValorFundoMunicipal.HasValue ? recursoProgramaProjeto.ValorFundoMunicipal.Value : 0M);

                        programasEstadualExercicio4 += (recursoProgramaProjeto.ValorFEAS.HasValue ? recursoProgramaProjeto.ValorFEAS.Value : 0M)
                                             + (recursoProgramaProjeto.ValorOrcamentoEstadual.HasValue ? recursoProgramaProjeto.ValorOrcamentoEstadual.Value : 0M)
                                             + (recursoProgramaProjeto.ValorFundoEstadual.HasValue ? recursoProgramaProjeto.ValorFundoEstadual.Value : 0M);

                        programasFederalExercicio4 +=
                                             (recursoProgramaProjeto.ValorFNAS.HasValue ? recursoProgramaProjeto.ValorFNAS.Value : 0M)
                                            + (recursoProgramaProjeto.ValorOrcamentoFederal.HasValue ? recursoProgramaProjeto.ValorOrcamentoFederal.Value : 0M)
                                            + (recursoProgramaProjeto.ValorFundoFederal.HasValue ? recursoProgramaProjeto.ValorFundoFederal.Value : 0M)
                                            + (recursoProgramaProjeto.ValorIGDPBF.HasValue ? recursoProgramaProjeto.ValorIGDPBF.Value : 0M)
                                            + (recursoProgramaProjeto.ValorIGDSUAS.HasValue ? recursoProgramaProjeto.ValorIGDSUAS.Value : 0M);
                    }

                    if (pm.TipoProgramaTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia)
                    {
                        programaProjetoPrefeitura.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pm.Id);
                        if (programaProjetoPrefeitura.PrevisaoAnual != null)
                        {
                            tranferenciaMunicipalExercicio4 += programaProjetoPrefeitura.PrevisaoAnual.PrevisaoAnualMunicipalExercicio4;
                        }
                    }
                #endregion

                }


                using (var proxy = new ProxyPrefeitura())
                {
                    var indice = proxy.Service.GetIndiceGestaoDescentralizadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                    if (indice != null)
                    {
                        var valorAnualIGD = indice.IGDPBFValorAnual.HasValue ? indice.IGDPBFValorAnual.Value : 0M;
                        var valorAnualSUAS = indice.IGDSUASValorAnual.HasValue ? indice.IGDSUASValorAnual.Value : 0M;

                        var incentivo = valorAnualIGD + valorAnualSUAS;
                        lblIncentivoGestaoFederalExercicio4.Text = incentivo.ToString("N2");
                        lblIncentivoGestaoTotalExercicio4.Text = incentivo.ToString("N2");
                    }
                }
                #endregion

                var lst = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                var transferenciaRenda = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                var idoso = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso);
                var pcd = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia);

                #region Servicos
                decimal totalServicosSocioAssistenciaisMunicipal = 0;
                decimal totalServicosSocioAssistenciaisEstadual = 0;
                decimal totalServicosSocioAssistenciaisFederal = 0;
                decimal totalServicosSocioAssistenciaisPrivados = 0;
                decimal totalServicosSocioAssistenciaisTotal = 0;

                foreach (var item in lst)
                {
                    totalServicosSocioAssistenciaisEstadual += item.RedePrivadaEstadual + item.RedePublicaEstadual;
                    totalServicosSocioAssistenciaisMunicipal += item.RedePrivadaMunicipal + item.RedePublicaMunicipal;
                    totalServicosSocioAssistenciaisFederal += item.RedePrivadaFederal + item.RedePublicaFederal;
                    totalServicosSocioAssistenciaisPrivados += item.Privado;
                }


                totalServicosSocioAssistenciaisTotal = totalServicosSocioAssistenciaisMunicipal
                                                        + totalServicosSocioAssistenciaisEstadual
                                                        + totalServicosSocioAssistenciaisFederal
                                                        + totalServicosSocioAssistenciaisPrivados;


                lblServicosSocioAssMunicipalExercicio4.Text = totalServicosSocioAssistenciaisMunicipal.ToString("N2");
                lblServicosSocioAssEstadualExercicio4.Text = totalServicosSocioAssistenciaisEstadual.ToString("N2");
                lblServicosSocioAssFederalExercicio4.Text = totalServicosSocioAssistenciaisFederal.ToString("N2");
                lblServicosSocioAssPrivadosExercicio4.Text = totalServicosSocioAssistenciaisPrivados.ToString("N2");
                lblServicosSocioAssTotalExercicio4.Text = totalServicosSocioAssistenciaisTotal.ToString("N2");
                #endregion

                #region Beneficio
                decimal BeneficiosMunicipal = 0;
                decimal BeneficiosEstadual = 0;
                decimal BeneficiosFederal = 0;
                decimal BeneficiosPrivados = 0;
                decimal BeneficiosTotal = 0;

                //var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(x => x.Exercicio == exercicio).FirstOrDefault();

                if (beneficio != null)
                {
                    BeneficiosMunicipal += beneficio.ValorAnualMunicipal;
                    BeneficiosEstadual += beneficio.ValorAnualEstadual;
                    BeneficiosFederal += beneficio.ValorAnualFederal;
                    BeneficiosPrivados += beneficio.ValorAnualPrivado;
                    BeneficiosMunicipal += pcd.ValorAnualMunicipal;
                    BeneficiosEstadual += pcd.ValorAnualEstadual;
                    BeneficiosFederal += pcd.ValorAnualFederal;
                    BeneficiosPrivados += pcd.ValorAnualPrivado;

                    if (idoso != null)
                    {
                        BeneficiosMunicipal += idoso.ValorAnualMunicipal;
                        BeneficiosEstadual += idoso.ValorAnualEstadual;
                        BeneficiosFederal += idoso.ValorAnualFederal;
                        BeneficiosPrivados += idoso.ValorAnualPrivado;
                    }


                    BeneficiosTotal = BeneficiosMunicipal + BeneficiosEstadual + BeneficiosFederal + BeneficiosPrivados;

                    lblBeneficiosMunicipalExercicio4.Text = BeneficiosMunicipal.ToString("N2");
                    lblBeneficiosEstadualExercicio4.Text = BeneficiosEstadual.ToString("N2");
                    lblBeneficiosFederalExercicio4.Text = BeneficiosFederal.ToString("N2");
                    lblBeneficiosPrivadosExercicio4.Text = BeneficiosPrivados.ToString("N2");
                    lblBeneficiosTotalExercicio4.Text = BeneficiosTotal.ToString("N2");
                }

                #endregion

                #region Transferencia

                #region Previsão Ação Jovem (CalculoAcaoRendaPrevisaoAnualExercicio2)
                var previsaoAcaoJovem = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.AcaoJovem).Id);
                if (previsaoAcaoJovem != null)
                {
                    tranferenciaEstadualExercicio4 += previsaoAcaoJovem.CalculoAcaoRendaPrevisaoAnualExercicio4.HasValue ? previsaoAcaoJovem.CalculoAcaoRendaPrevisaoAnualExercicio4.Value : (0M);
                }
                #endregion

                #region Previsão Renda Cidadã (CalculoAcaoRendaPrevisaoAnualExercicio2)
                var previsaoRendaCidada = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.RendaCidada).Id);
                if (previsaoRendaCidada != null)
                {
                    tranferenciaEstadualExercicio4 += previsaoRendaCidada.CalculoAcaoRendaPrevisaoAnualExercicio4.HasValue ? previsaoRendaCidada.CalculoAcaoRendaPrevisaoAnualExercicio4.Value : (0M);
                }
                #endregion

                #region Previsão Amigo do Idoso (ESTADUAL ??) (MetaPactuadaExercicio4) ??
                using (var programaIdoso = new ProxyProgramas())
                {
                    var previsaoAmigoIdosoEstadual = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                    if (previsaoAmigoIdosoEstadual != null)
                    {
                        var pIdosoSingle = previsaoAmigoIdosoEstadual.Where(t => t.Nome.ToLower().Contains("amigo do idoso")).FirstOrDefault();
                        if (pIdosoSingle != null)
                        {
                            int pIdosoSingleId = pIdosoSingle.Id;

                            var ppIdoso = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pIdosoSingleId);
                            if (ppIdoso != null)
                            {
                                tranferenciaEstadualExercicio4 += ((ppIdoso.MetaPactuadaExercicio4) * 100M * 12);
                            }
                        }
                    }
                }
                #endregion

                #region Previsão BPC - Idoso (FEDERAL) (CalculoBPCPrevisaoAnualExercicio4)
                var previsaoBPCFederal = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso).Id);
                if (previsaoBPCFederal != null)
                {
                    tranferenciaFederalExercicio4 += previsaoBPCFederal.CalculoBPCPrevisaoAnualExercicio4.HasValue ? previsaoBPCFederal.CalculoBPCPrevisaoAnualExercicio4.Value : 0M;
                }
                #endregion

                #region Previsão BPC Deficientes (CalculoBPCPrevisaoAnualExercicio4)
                var previsaoBPCdeficientes = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia).Id);
                if (previsaoBPCdeficientes != null)
                {
                    tranferenciaFederalExercicio4 += previsaoBPCdeficientes.CalculoBPCPrevisaoAnualExercicio4.HasValue ? previsaoBPCdeficientes.CalculoBPCPrevisaoAnualExercicio4.Value : 0M;
                }
                #endregion

                #region Previsão Bolsa Familia (RepasseMensal2023)

                if (transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BolsaFamilia) != null)
                {
                    var previsaoBolsaFamilia = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == (int)ETipoTransferenciaRenda.BolsaFamilia).Id);
                    if (previsaoBolsaFamilia != null)
                    {
                        tranferenciaFederalExercicio4 += previsaoBolsaFamilia.RepasseMensal2024.HasValue ? (previsaoBolsaFamilia.RepasseMensal2024.Value * 12) : 0M;
                    }
                }
                #endregion

                #region Previsão PETI
                var peti = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 4);
                if (peti != null)
                {
                    tranferenciaFederalExercicio4 += peti.ValorAnualFederal;
                }

                #endregion

                #region Totais Transferencias
                tranferenciaTotalExercicio4 = tranferenciaMunicipalExercicio4 + tranferenciaEstadualExercicio4 + tranferenciaFederalExercicio4 + tranferenciaPrivadoExercicio4;
                lblTransferenciaRendaMunicipalExercicio4.Text = tranferenciaMunicipalExercicio4.ToString("N2");
                lblTransferenciaRendaEstadualExercicio4.Text = tranferenciaEstadualExercicio4.ToString("N2");
                lblTransferenciaRendaFederalExercicio4.Text = tranferenciaFederalExercicio4.ToString("N2");
                lblTransferenciaRendaPrivadosExercicio4.Text = tranferenciaPrivadoExercicio4.ToString("N2");
                lblTransferenciaRendaTotalExercicio4.Text = tranferenciaTotalExercicio4.ToString("N2");

                #endregion

                #endregion

                #region Totais

                var totalMunicipal = string.IsNullOrEmpty(lblServicosSocioAssMunicipalExercicio4.Text) ? 0M : Convert.ToDecimal(lblServicosSocioAssMunicipalExercicio4.Text)
                                        + (string.IsNullOrEmpty(lblBeneficiosMunicipalExercicio4.Text) ? 0M : Convert.ToDecimal(lblBeneficiosMunicipalExercicio4.Text))
                                        + (string.IsNullOrEmpty(lblTransferenciaRendaMunicipalExercicio4.Text) ? 0M : Convert.ToDecimal(lblTransferenciaRendaMunicipalExercicio4.Text))
                                        + (string.IsNullOrEmpty(lblIncentivoGestaoMunicipalExercicio4.Text) ? 0M : Convert.ToDecimal(lblIncentivoGestaoMunicipalExercicio4.Text))
                                        + (string.IsNullOrEmpty(lblProgramasMunicipalExercicio4.Text) ? 0M : Convert.ToDecimal(lblProgramasMunicipalExercicio4.Text));

                var totalEstado = string.IsNullOrEmpty(lblServicosSocioAssEstadualExercicio4.Text) ? 0M : Convert.ToDecimal(lblServicosSocioAssEstadualExercicio4.Text)
                                    + (string.IsNullOrEmpty(lblBeneficiosEstadualExercicio4.Text) ? 0M : Convert.ToDecimal(lblBeneficiosEstadualExercicio4.Text))
                                    + (string.IsNullOrEmpty(lblTransferenciaRendaEstadualExercicio4.Text) ? 0M : Convert.ToDecimal(lblTransferenciaRendaEstadualExercicio4.Text))
                                    + (string.IsNullOrEmpty(lblIncentivoGestaoEstadualExercicio4.Text) ? 0M : Convert.ToDecimal(lblIncentivoGestaoEstadualExercicio4.Text))
                                    + (string.IsNullOrEmpty(lblProgramasEstadualExercicio4.Text) ? 0M : Convert.ToDecimal(lblProgramasEstadualExercicio4.Text));

                var totalFederal = Convert.ToDecimal(lblServicosSocioAssFederalExercicio4.Text)
                    + Convert.ToDecimal(lblBeneficiosFederalExercicio4.Text)
                    + Convert.ToDecimal(lblTransferenciaRendaFederalExercicio4.Text)
                    + Convert.ToDecimal(lblIncentivoGestaoFederalExercicio4.Text)
                    + Convert.ToDecimal(lblProgramasFederalExercicio4.Text);

                var totalPrivados = Convert.ToDecimal(lblServicosSocioAssPrivadosExercicio4.Text)
                    + Convert.ToDecimal(lblBeneficiosPrivadosExercicio4.Text)
                    + Convert.ToDecimal(lblTransferenciaRendaPrivadosExercicio4.Text)
                    + Convert.ToDecimal(lblIncentivoGestaoPrivadosExercicio4.Text)
                    + Convert.ToDecimal(lblProgramasPrivadoExercicio4.Text);


                var total = programasMunicipalExercicio4 + programasEstadualExercicio4 + programasFederalExercicio4 + programasPrivadoExercicio4;
                programasEstadualExercicio4 = +Convert.ToDecimal(lblProgDesenvTotalEstadualExercicio4.Text);


                totalMunicipal += programasMunicipalExercicio4;
                totalEstado += programasEstadualExercicio4;
                totalFederal += programasFederalExercicio4;
                totalPrivados += programasPrivadoExercicio4;

                lblProgramasMunicipalExercicio4.Text = programasMunicipalExercicio4.ToString("N2");
                lblProgramasEstadualExercicio4.Text = programasEstadualExercicio4.ToString("N2");
                lblProgramasFederalExercicio4.Text = programasFederalExercicio4.ToString("N2");
                lblProgramasPrivadoExercicio4.Text = programasPrivadoExercicio4.ToString("N2");

                lblProgramasTotalExercicio4.Text = total.ToString("N2");

                lblTotalResumoGeralMunicipalExercicio4.Text = totalMunicipal.ToString("N2");
                lblTotalResumoGeralEstadualExercicio4.Text = totalEstado.ToString("N2");
                lblTotalResumoGeralFederalExercicio4.Text = totalFederal.ToString("N2");
                lblTotalResumoGeralPrivadosExercicio4.Text = totalPrivados.ToString("N2");
                lblTotalResumoGeralTotalExercicio4.Text = (totalMunicipal + totalEstado + totalFederal + totalPrivados).ToString("N2");
                #endregion

            }
        }

        #region Programas desenvolvidos no município
        void carregarProgramas2017(Prefeituras prefeituras)
        {
            //Programas desenvolvidos no município
            var programas = prefeituras.GetProgramasDesenvolvidosMunicipio2016(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            lblACESSUASFederalExercicio0.Text = programas.ValorFederalACESSUAS.ToString("N2");
            lblACESSUASTotalExercicio0.Text = programas.ValorFederalACESSUAS.ToString("N2");

            lblSPSMunicipalExercicio0.Text = programas.ValorMunicipalSPSolidario.ToString("N2");
            lblSPSEstadualExercicio0.Text = programas.ValorEstadualSPSolidario.ToString("N2");
            lblSPSFederalExercicio0.Text = programas.ValorFederalSPSolidario.ToString("N2");

            lblSPSTotalExercicio0.Text = (programas.ValorMunicipalSPSolidario + programas.ValorEstadualSPSolidario + programas.ValorFederalSPSolidario).ToString("N2");

            lblSPAIEstadualExercicio0.Text = programas.ValorEstadualAmigoIdoso.ToString("N2");
            lblSPAITotalExercicio0.Text = programas.ValorEstadualAmigoIdoso.ToString("N2");

            lblPMMunicipalExercicio0.Text = programas.ValorMunicipalProgramas.ToString("N2");
            lblPMEstadualExercicio0.Text = programas.ValorEstadualProgramas.ToString("N2");
            lblPMFederalExercicio0.Text = programas.ValorFederalProgramas.ToString("N2");

            lblPMTotalExercicio0.Text = (programas.ValorMunicipalProgramas + programas.ValorEstadualProgramas + programas.ValorFederalProgramas).ToString("N2");



            var transferenciaRendaProgramas = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            using (var programaProjeto = new ProxyProgramas())
            {

                #region ProsperaFamilia
                var prospera = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 10).Id);

                if (prospera != null)
                {
                    lblPFEstadualExercicio0.Text = prospera.ValorRepasseEstadual2021.HasValue ? prospera.ValorRepasseEstadual2021.Value.ToString("N2") : "0,00";
                }

                #endregion
            }


            lblProgDesenvTotalMunicipalExercicio0.Text = (
                                                Convert.ToDecimal(lblACESSUASMunicipalExercicio0.Text)
                                                + Convert.ToDecimal(lblSPSMunicipalExercicio0.Text)
                                                + Convert.ToDecimal(lblSPAIMunicipalExercicio0.Text)
                                                + Convert.ToDecimal(lblPMMunicipalExercicio0.Text)
                                                ).ToString("N2");

            lblProgDesenvTotalEstadualExercicio0.Text = (
                                                Convert.ToDecimal(lblACESSUASEstadualExercicio0.Text)
                                                + Convert.ToDecimal(lblSPSEstadualExercicio0.Text)
                                                + Convert.ToDecimal(lblSPAIEstadualExercicio0.Text)
                                                + Convert.ToDecimal(lblPMEstadualExercicio0.Text)
                                                + Convert.ToDecimal(lblPFEstadualExercicio0.Text)
                                                ).ToString("N2");

            lblProgDesenvTotalFederalExercicio0.Text = (
                                                Convert.ToDecimal(lblACESSUASFederalExercicio0.Text)
                                                + Convert.ToDecimal(lblSPSFederalExercicio0.Text)
                                                + Convert.ToDecimal(lblSPAIFederalExercicio0.Text)
                                                + Convert.ToDecimal(lblPMFederalExercicio0.Text)
                                                ).ToString("N2");

            lblProgDesenvTotalPrivadosExercicio0.Text = (
                                                Convert.ToDecimal(lblACESSUASPrivadoExercicio0.Text)
                                                + Convert.ToDecimal(lblSPSPrivadosExercicio0.Text)
                                                + Convert.ToDecimal(lblSPAIPrivadosExercicio0.Text)
                                                + Convert.ToDecimal(lblPMPrivadosExercicio0.Text)
                                                ).ToString("N2");

            lblProgDesenvTotalGeralExercicio0.Text = (
                                            Convert.ToDecimal(lblACESSUASTotalExercicio0.Text)
                                            + Convert.ToDecimal(lblSPSTotalExercicio0.Text)
                                            + Convert.ToDecimal(lblSPAITotalExercicio0.Text)
                                            + Convert.ToDecimal(lblPMTotalExercicio0.Text)
                                            ).ToString("N2");

            //BENEFICIO EVENTUAL
            var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(x => x.Exercicio == 2021).FirstOrDefault();
            if (beneficio != null)
            {
                lblBEvEstadualExercicio0.Text = beneficio.ValorAnualEstadual.ToString("N2");
                lblBEvMunicipalExercicio0.Text = beneficio.ValorAnualMunicipal.ToString("N2");
                lblBEvFederalExercicio0.Text = beneficio.ValorAnualFederal.ToString("N2");
                lblBEvPrivadosExercicio0.Text = beneficio.ValorAnualPrivado.ToString("N2");
                lblBEvTotalExercicio0.Text = (beneficio.ValorAnualEstadual + beneficio.ValorAnualFederal + beneficio.ValorAnualMunicipal + beneficio.ValorAnualPrivado).ToString("N2");
            }

            var transferenciaRenda = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            //IDOSO
            var idoso = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 1);
            if (idoso != null)
            {
                lblBPCIdososFederalExercicio0.Text = idoso.ValorAnualFederal.ToString("N2");
                lblBPCIdososTotalExercicio0.Text = idoso.ValorAnualFederal.ToString("N2");
            }

            //Pessoa com deficiência
            var pcd = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 2);
            if (pcd != null)
            {
                lblBPCPCDFederalExercicio0.Text = pcd.ValorAnualFederal.ToString("N2");
                lblBPCPCDTotalExercicio0.Text = pcd.ValorAnualFederal.ToString("N2");
            }

            //Ação Jovem
            var acao = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 5);
            if (acao != null)
            {
                lblAcaoJovemEstadualExercicio0.Text = acao.ValorAnualEstadual.ToString("N2");
                lblAcaoJovemTotalExercicio0.Text = acao.ValorAnualEstadual.ToString("N2");
            }

            //Renda Cidadã
            var renda = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 6);
            if (renda != null)
            {
                lblRendaCidadaEstadualExercicio0.Text = renda.ValorAnualEstadual.ToString("N2");
                lblRendaCidadaTotalExercicio0.Text = renda.ValorAnualEstadual.ToString("N2");
            }

            //Renda Cidadã - Benefício Idoso
            lblRendaCidadaBeneficioIdosoEstadualExercicio0.Text = programas.ValorEstadualRendaCidadaBeneficioIdoso.ToString("N2");
            lblRendaCidadaBeneficioIdosoTotalExercicio0.Text = programas.ValorEstadualRendaCidadaBeneficioIdoso.ToString("N2");


            //Bolsa Família
            var bolsa = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 3);
            if (bolsa != null)
            {
                lblBolsaFamiliaFederalExercicio0.Text = bolsa.ValorAnualFederal.ToString("N2");
                lblBolsaFamiliaTotalExercicio0.Text = bolsa.ValorAnualFederal.ToString("N2");
            }

            //PETI
            var peti = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 4);

            //Programa Municipais
            var municipal = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 8);
            if (municipal != null)
            {
                lblProgramaMunicipalExercicio0.Text = municipal.ValorAnualMunicipal.ToString("N2");
                lblProgramaMunicipalTotalExercicio0.Text = municipal.ValorAnualMunicipal.ToString("N2");
            }

            /**/
            lblProgramaTotalMunicipalExercicio0.Text = (Convert.ToDecimal(lblBEvMunicipalExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCIdososMunicipalExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCPCDMunicipalExercicio0.Text)
                                            + Convert.ToDecimal(lblAcaoJovemMunicipalExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaMunicipalExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoMunicipalExercicio0.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaMunicipalExercicio0.Text)
                                            + Convert.ToDecimal(lblProgramaMunicipalExercicio0.Text)).ToString("n2");

            lblProgramaTotalEstadualExercicio0.Text = (Convert.ToDecimal(lblBEvEstadualExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCIdososEstadualExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCPCDEstadualExercicio0.Text)
                                            + Convert.ToDecimal(lblAcaoJovemEstadualExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaEstadualExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoEstadualExercicio0.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaEstadualExercicio0.Text)
                                            + Convert.ToDecimal(lblProgramaEstadualExercicio0.Text)).ToString("n2");

            lblProgramaTotalFederalExercicio0.Text = (Convert.ToDecimal(lblBEvFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCIdososFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCPCDFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblAcaoJovemFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblProgramaFederalExercicio0.Text)).ToString("n2");

            lblProgramaTotalPrivadosExercicio0.Text = (Convert.ToDecimal(lblBEvPrivadosExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCIdososPrivadosExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCPCDPrivadosExercicio0.Text)
                                            + Convert.ToDecimal(lblAcaoJovemPrivadosExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaPrivadosExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoPrivadosExercicio0.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaPrivadosExercicio0.Text)
                                            + Convert.ToDecimal(lblProgramaPrivadosExercicio0.Text)).ToString("n2");

            lblProgramaTotalGeralExercicio0.Text = (Convert.ToDecimal(lblBEvTotalExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCIdososTotalExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCPCDTotalExercicio0.Text)
                                            + Convert.ToDecimal(lblAcaoJovemTotalExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaTotalExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoTotalExercicio0.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaTotalExercicio0.Text)
                                            + Convert.ToDecimal(lblProgramaMunicipalTotalExercicio0.Text)).ToString("n2");

            var indice = prefeituras.GetIndiceGestaoDescentralizada(SessaoPmas.UsuarioLogado.Prefeitura.Id,2021);
            if (indice != null)
            {
                var IGD = 0m;
                if (indice.IGDPBFValorAnual.HasValue)
                    IGD += indice.IGDPBFValorAnual.Value;
                if (indice.IGDSUASValorAnual.HasValue)
                    IGD += indice.IGDSUASValorAnual.Value;
                lblIncentivoGestaoFederalExercicio0.Text = IGD.ToString("n2");
            }

            lblBeneficiosMunicipalExercicio0.Text = (Convert.ToDecimal(lblBEvMunicipalExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCIdososMunicipalExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCPCDMunicipalExercicio0.Text)
                                            ).ToString("n2");
            lblBeneficiosEstadualExercicio0.Text = (Convert.ToDecimal(lblBEvEstadualExercicio0.Text)
                                            ).ToString("n2");

            lblBeneficiosFederalExercicio0.Text = (Convert.ToDecimal(lblBEvFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCIdososFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblBPCPCDFederalExercicio0.Text)
                                            ).ToString("n2");
            lblBeneficiosPrivadosExercicio0.Text = (Convert.ToDecimal(lblBEvPrivadosExercicio0.Text)
                                            ).ToString("n2");
            lblBeneficiosTotalExercicio0.Text = (Convert.ToDecimal(lblBeneficiosMunicipalExercicio0.Text)
                                        + Convert.ToDecimal(lblBeneficiosEstadualExercicio0.Text)
                                        + Convert.ToDecimal(lblBeneficiosFederalExercicio0.Text)
                                        + Convert.ToDecimal(lblBeneficiosPrivadosExercicio0.Text)
                                            ).ToString("n2");

            lblIncentivoGestaoTotalExercicio0.Text = (Convert.ToDecimal(lblIncentivoGestaoFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblIncentivoGestaoEstadualExercicio0.Text)
                                            ).ToString("n2");

            lblTransferenciaRendaMunicipalExercicio0.Text = (Convert.ToDecimal(lblProgramaMunicipalTotalExercicio0.Text)).ToString("n2");

            lblTransferenciaRendaEstadualExercicio0.Text = (
                                             Convert.ToDecimal(lblAcaoJovemEstadualExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaEstadualExercicio0.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoEstadualExercicio0.Text)
                                            ).ToString("n2");

            lblTransferenciaRendaFederalExercicio0.Text = (
                                            Convert.ToDecimal(lblBolsaFamiliaFederalExercicio0.Text)
                //+ Convert.ToDecimal(lblPETIFederal2017.Text)
                                            ).ToString("n2");
            lblTransferenciaRendaPrivadosExercicio0.Text = (0
                                            ).ToString("n2");

            lblTransferenciaRendaTotalExercicio0.Text = (
                                             Convert.ToDecimal(lblTransferenciaRendaMunicipalExercicio0.Text)
                                            + Convert.ToDecimal(lblTransferenciaRendaEstadualExercicio0.Text)
                                            + Convert.ToDecimal(lblTransferenciaRendaFederalExercicio0.Text)
                                            + Convert.ToDecimal(lblTransferenciaRendaPrivadosExercicio0.Text)
                                            ).ToString("n2");
            if (((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalMunicipalExercicio0")) != null)
                lblServicosSocioAssMunicipalExercicio0.Text = ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalMunicipalExercicio0")).Text;
            if (((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalEstadualExercicio0")) != null)
                lblServicosSocioAssEstadualExercicio0.Text = ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalEstadualExercicio0")).Text;
            if (((Label)lvPrevisaoOrcamentaria.FindControl("lblTotalFederalExercicio0")) != null)
                lblServicosSocioAssFederalExercicio0.Text = ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalFederalExercicio0")).Text;
            if (((Label)lvPrevisaoOrcamentaria.FindControl("lblTotalPrivadoExercicio0")) != null)
                lblServicosSocioAssPrivadosExercicio0.Text = ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalPrivadoExercicio0")).Text;
            if (((Label)lvPrevisaoOrcamentaria.FindControl("lblTotalGeralExercicio0")) != null)
                lblServicosSocioAssTotalExercicio0.Text = ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalGeralExercicio0")).Text;

            lblProgramasMunicipalExercicio0.Text = lblProgDesenvTotalMunicipalExercicio0.Text;
            lblProgramasEstadualExercicio0.Text = lblProgDesenvTotalEstadualExercicio0.Text;
            lblProgramasFederalExercicio0.Text = lblProgDesenvTotalFederalExercicio0.Text;
            lblProgramasPrivadoExercicio0.Text = lblProgDesenvTotalPrivadosExercicio0.Text;
            lblProgramasTotalExercicio0.Text = lblProgDesenvTotalGeralExercicio0.Text;

            lblTotalResumoGeralMunicipalExercicio0.Text = (Convert.ToDecimal(lblServicosSocioAssMunicipalExercicio0.Text)
                            + Convert.ToDecimal(lblBeneficiosMunicipalExercicio0.Text)
                            + Convert.ToDecimal(lblTransferenciaRendaMunicipalExercicio0.Text)
                            + Convert.ToDecimal(lblIncentivoGestaoMunicipalExercicio0.Text)
                            + Convert.ToDecimal(lblProgramasMunicipalExercicio0.Text)).ToString("n2");

            lblTotalResumoGeralEstadualExercicio0.Text = (Convert.ToDecimal(lblServicosSocioAssEstadualExercicio0.Text)
                            + Convert.ToDecimal(lblBeneficiosEstadualExercicio0.Text)
                            + Convert.ToDecimal(lblTransferenciaRendaEstadualExercicio0.Text)
                            + Convert.ToDecimal(lblIncentivoGestaoEstadualExercicio0.Text)
                            + Convert.ToDecimal(lblProgramasEstadualExercicio0.Text)).ToString("n2");

            lblTotalResumoGeralFederalExercicio0.Text = (Convert.ToDecimal(lblServicosSocioAssFederalExercicio0.Text)
            + Convert.ToDecimal(lblBeneficiosFederal.Text)
            + Convert.ToDecimal(lblTransferenciaRendaFederal.Text)
            + Convert.ToDecimal(lblIncentivoGestaoFederal.Text)
            + Convert.ToDecimal(lblProgramasFederal.Text)).ToString("n2");

            lblTotalResumoGeralPrivadosExercicio0.Text = (Convert.ToDecimal(lblServicosSocioAssPrivadosExercicio0.Text)
            + Convert.ToDecimal(lblBeneficiosPrivadosExercicio0.Text)
            + Convert.ToDecimal(lblTransferenciaRendaPrivadosExercicio0.Text)
            + Convert.ToDecimal(lblIncentivoGestaoPrivadosExercicio0.Text)
            + Convert.ToDecimal(lblProgramasPrivadoExercicio0.Text)).ToString("n2");

            lblTotalResumoGeralTotalExercicio0.Text = (Convert.ToDecimal(lblServicosSocioAssTotalExercicio0.Text)
            + Convert.ToDecimal(lblBeneficiosTotalExercicio0.Text)
            + Convert.ToDecimal(lblTransferenciaRendaTotalExercicio0.Text)
            + Convert.ToDecimal(lblIncentivoGestaoTotalExercicio0.Text)
            + Convert.ToDecimal(lblProgramasTotalExercicio0.Text)).ToString("n2");
        }

        void carregarProgramasExercicio1(Prefeituras prefeituras)
        {
            int exercicio = FPrevisaoOrcamentaria.Exercicios[0];
            //Programas desenvolvidos no município
            using (var programaProjeto = new ProxyProgramas())
            {
                var programasProjetosFederais = programaProjeto.Service.GetConsultaProgramasProjetosFederaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);

                var programasProjetosFederaisAcessuasExercicio1 = programasProjetosFederais.Where(t => t.Nome.ToLower().Contains("acessuas"));
                if (programasProjetosFederaisAcessuasExercicio1 != null)
                {
                    var programaAcessua = programasProjetosFederaisAcessuasExercicio1.FirstOrDefault();

                    var programaProjetoAcessua = programaProjeto.Service.GetProgramaProjetoById(programaAcessua.Id);

                    if (programaProjetoAcessua != null)
                    {
                        programaProjetoAcessua.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetoAcessua.Id);

                        var recursoAcessuas = programaProjetoAcessua.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                        if (recursoAcessuas != null)
                        {
                            lblACESSUASMunicipal.Text = ((recursoAcessuas.FonteFMAS.HasValue && recursoAcessuas.FonteFMAS.Value
                                && recursoAcessuas.ValorFMAS.HasValue ? recursoAcessuas.ValorFMAS.Value : (0M))
                                                        + (recursoAcessuas.FonteOrcamentoMunicipal.HasValue
                                                        && recursoAcessuas.FonteOrcamentoMunicipal.Value && recursoAcessuas.ValorOrcamentoMunicipal.HasValue
                                                        ? recursoAcessuas.ValorOrcamentoMunicipal.Value : (0M))
                                                        + (recursoAcessuas.FonteFundoMunicipal.HasValue && recursoAcessuas.FonteFundoMunicipal.Value
                                                        && recursoAcessuas.ValorFundoMunicipal.HasValue ? recursoAcessuas.ValorFundoMunicipal.Value : (0M))
                                                        ).ToString("N2");

                            if (programaProjetoAcessua.PrevisaoAnual != null)
                            {
                                lblACESSUASFederal.Text = (programaProjetoAcessua.PrevisaoAnual.PrevisaoAnualRepasseExercicio1).ToString("N2");
                                lblACESSUASTotal.Text = (
                                                        (recursoAcessuas.FonteFMAS.HasValue && recursoAcessuas.FonteFMAS.Value
                                                        && recursoAcessuas.ValorFMAS.HasValue ? recursoAcessuas.ValorFMAS.Value : (0M)) +
                                                        (recursoAcessuas.FonteOrcamentoMunicipal.HasValue && recursoAcessuas.FonteOrcamentoMunicipal.Value
                                                        && recursoAcessuas.ValorOrcamentoMunicipal.HasValue ? recursoAcessuas.ValorOrcamentoMunicipal.Value : (0M)) +
                                                        (recursoAcessuas.FonteFundoMunicipal.HasValue && recursoAcessuas.FonteFundoMunicipal.Value
                                                        && recursoAcessuas.ValorFundoMunicipal.HasValue ? recursoAcessuas.ValorFundoMunicipal.Value : (0M))
                                                         + (programaProjetoAcessua.PrevisaoAnual.PrevisaoAnualRepasseExercicio1)
                                                        ).ToString("N2");
                            }
                            else
                            {
                                lblACESSUASFederal.Text = "0,00";
                            }
                        }
                        else
                        {
                            lblACESSUASMunicipal.Text = "0,00";
                        }

                    }
                }


                var programas = programasProjetosFederais.Where(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ"));
                var programa = programas.FirstOrDefault();

                if (programa != null)
                {
                    var programasProjetosPrimeiraInfanciaSuas = programaProjeto.Service.GetProgramaProjetoById(programa.Id);

                    if (programasProjetosPrimeiraInfanciaSuas != null)
                    {
                        programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programasProjetosPrimeiraInfanciaSuas.Id);

                        if (programasProjetosPrimeiraInfanciaSuas.ProgramasProjetosRecursoFinanceiro != null)
                        {
                            var recursoInfanciaSuas = programasProjetosPrimeiraInfanciaSuas.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                            if (recursoInfanciaSuas != null)
                            {
                                lblSPSMunicipal.Text = ((recursoInfanciaSuas.FonteFMAS.HasValue && recursoInfanciaSuas.FonteFMAS.Value
                                    && recursoInfanciaSuas.ValorFMAS.HasValue ? recursoInfanciaSuas.ValorFMAS.Value : (0M))
                                                           + (recursoInfanciaSuas.FonteOrcamentoMunicipal.HasValue
                                                           && recursoInfanciaSuas.FonteOrcamentoMunicipal.Value
                                                           && recursoInfanciaSuas.ValorOrcamentoMunicipal.HasValue ? recursoInfanciaSuas.ValorOrcamentoMunicipal.Value : (0M))
                                                           + (recursoInfanciaSuas.FonteFundoMunicipal.HasValue
                                                           && recursoInfanciaSuas.FonteFundoMunicipal.Value
                                                           && recursoInfanciaSuas.ValorFundoMunicipal.HasValue ? recursoInfanciaSuas.ValorFundoMunicipal.Value : (0M))
                                                           ).ToString("N2");
                                lblSPSEstadual.Text = "0,00";

                                if (programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual != null)
                                {
                                    lblSPSFederal.Text = programasProjetosPrimeiraInfanciaSuas.ExecutaPrograma ? programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio1.ToString("N2") : "0,00";

                                    lblSPSTotal.Text = ((recursoInfanciaSuas.FonteFMAS.HasValue
                                        && recursoInfanciaSuas.FonteFMAS.Value
                                        && recursoInfanciaSuas.ValorFMAS.HasValue ? recursoInfanciaSuas.ValorFMAS.Value : (0M))
                                                               + (recursoInfanciaSuas.FonteOrcamentoMunicipal.HasValue
                                                               && recursoInfanciaSuas.FonteOrcamentoMunicipal.Value
                                                               && recursoInfanciaSuas.ValorOrcamentoMunicipal.HasValue ? recursoInfanciaSuas.ValorOrcamentoMunicipal.Value : (0M))
                                                               + (recursoInfanciaSuas.FonteFundoMunicipal.HasValue
                                                               && recursoInfanciaSuas.FonteFundoMunicipal.Value
                                                               && recursoInfanciaSuas.ValorFundoMunicipal.HasValue ? recursoInfanciaSuas.ValorFundoMunicipal.Value : (0M))
                                                               + (programasProjetosPrimeiraInfanciaSuas.ExecutaPrograma ? programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio1 : (0M))).ToString("N2");
                                }
                                else
                                {
                                    lblSPSFederal.Text = "0,00";
                                }
                            }
                            else
                            {
                                lblSPSMunicipal.Text = "0,00";
                            }
                        }
                    }
                }

                var programasEstaduais = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                #region Amigo do idoso

                var programasAmigoDoIdoso = programasEstaduais.Where(t => t.Nome.ToLower().Contains("amigo do idoso") && t.ProgramaEstadual == true);

                if (programasAmigoDoIdoso.Count() != 0)
                {
                    int idProgramaDoIdoso = programasAmigoDoIdoso.First().Id;

                    ProgramaProjetoInfo amigoIdoso = programaProjeto.Service.GetProgramaProjetoById(idProgramaDoIdoso);

                    if (amigoIdoso != null)
                    {
                        var parcelas = amigoIdoso.ProgramasProjetosParcelasInfo.ToList();
                        decimal SPAIEstadual = 0;

                        foreach (var parcela in parcelas)
                        {
                            if (amigoIdoso.ConvenioCentroDiaIdoso && parcela.AnoRepasseDiaIdoso.HasValue && parcela.AnoRepasseDiaIdoso.Value == exercicio)
                            {
                                SPAIEstadual += ((parcela.ValorDiaIdoso.HasValue ? parcela.ValorDiaIdoso.Value : 0M));
                            }

                            if (amigoIdoso.ConvenioCentroConvivenciaIdoso && parcela.AnoRepasseConvivenciaIdoso.HasValue && parcela.AnoRepasseConvivenciaIdoso.Value == exercicio)
                            {
                                SPAIEstadual += ((parcela.ValorConvivenciaIdoso.HasValue ? parcela.ValorConvivenciaIdoso.Value : 0M));
                            }

                            if (parcela.AnoRepasseDiaIdoso.HasValue && parcela.AnoRepasseDiaIdoso.Value == exercicio &&
                                parcela.AnoRepasseConvivenciaIdoso.HasValue && parcela.AnoRepasseConvivenciaIdoso.Value == exercicio)
                            {
                                lblSPAIEstadual.Text = (
                                                          (parcela.ValorDiaIdoso.HasValue ? parcela.ValorDiaIdoso.Value : 0M)
                                                          + (parcela.ValorConvivenciaIdoso.HasValue ? parcela.ValorConvivenciaIdoso.Value : 0M)
                                                          ).ToString("N2");
                            }

                        }

                        lblSPAIEstadual.Text = SPAIEstadual.ToString("N2");

                        lblSPAITotal.Text = (
                                                  Convert.ToDecimal(String.IsNullOrEmpty(lblSPAIMunicipal.Text) ? "0" : lblSPAIMunicipal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblSPAIEstadual.Text) ? "0" : lblSPAIEstadual.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblSPAIFederal.Text) ? "0" : lblSPAIFederal.Text)
                                            ).ToString("N2");
                    }
                #endregion

                }
                var transferencia = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                var acoesPeti = programaProjeto.Service.GetTransferenciaRendaById(transferencia.SingleOrDefault(t => t.Nome.ToLower().Contains("peti")).Id);
                if (acoesPeti != null)
                {
                    lblAPETIMunicipal.Text = "0,00";
                    lblAPETIEstadual.Text = "0,00";

                    if (acoesPeti.ValorAEPETI != null)
                    {
                        lblAPETIFederal.Text = (string.IsNullOrEmpty(acoesPeti.PETIAderiuCofinanciamentoFederal.ToString()) && string.IsNullOrEmpty(acoesPeti.PETIAderiuCofinanciamentoFederal.Value.ToString()) ? acoesPeti.ValorAEPETI.Value * 12 : 0M).ToString("N2");    
                    }

                    

                    lblAPETITotal.Text = (Convert.ToDecimal(lblAPETIFederal.Text)).ToString("N2");


                }

                var transferenciaRendaProgramas = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region ProsperaFamilia
                var prospera = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 10).Id);

                if (prospera != null)
                {
                    lblPFEstadual.Text = prospera.ValorRepasseEstadual2022.HasValue ? prospera.ValorRepasseEstadual2022.Value.ToString("N2") : "0,00";
                }

                #endregion

                #region FortalecimentoCadUnico
                var fortalecimento = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 11).Id);

                if (fortalecimento != null)
                {
                    lblFCEstadual.Text = fortalecimento.ValorRepasseEstadual2022.HasValue ? fortalecimento.ValorRepasseEstadual2022.Value.ToString("N2") : "0,00";
                }

                #endregion

                #region FortalecimentoDaVigilânciaSocioassistencial
                var fortalecimentoVigilancia = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 12).Id);

                if (fortalecimentoVigilancia != null)
                {
                    lblFVEstadual.Text = fortalecimentoVigilancia.ValorRepasseEstadual2022.HasValue ? fortalecimentoVigilancia.ValorRepasseEstadual2022.Value.ToString("N2") : "0,00";
                }

                #endregion

                var programasProjetos = programaProjeto.Service.GetConsultaProgramasProjetosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                foreach (var pm in programasProjetos)
                {
                    var programasProjetosExercicio1 = programaProjeto.Service.GetProgramaProjetoById(pm.Id);
                    if (programasProjetosExercicio1.ProgramasProjetosRecursoFinanceiro != null)
                    {
                        var recursoAcessuas = programasProjetosExercicio1.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                        if (recursoAcessuas != null)
                        {
                            lblPMMunicipal.Text = (
                                                  Convert.ToDecimal(lblPMMunicipal.Text)
                                                  + (recursoAcessuas.ValorFMAS.HasValue ? recursoAcessuas.ValorFMAS.Value : 0M)
                                                  + (recursoAcessuas.ValorOrcamentoMunicipal.HasValue ? recursoAcessuas.ValorOrcamentoMunicipal.Value : 0M)
                                                  + (recursoAcessuas.ValorFundoMunicipal.HasValue ? recursoAcessuas.ValorFundoMunicipal.Value : 0M)
                                                  ).ToString("N2");

                            lblPMEstadual.Text = (
                                                 Convert.ToDecimal(lblPMEstadual.Text)
                                                 + (recursoAcessuas.ValorFEAS.HasValue ? recursoAcessuas.ValorFEAS.Value : 0M)
                                                 + (recursoAcessuas.ValorOrcamentoEstadual.HasValue ? recursoAcessuas.ValorOrcamentoEstadual.Value : 0M)
                                                 + (recursoAcessuas.ValorFundoEstadual.HasValue ? recursoAcessuas.ValorFundoEstadual.Value : 0M)
                                                 ).ToString("N2");

                            lblPMFederal.Text = (
                                                Convert.ToDecimal(lblPMFederal.Text)
                                                + (recursoAcessuas.ValorFNAS.HasValue ? recursoAcessuas.ValorFNAS.Value : 0M)
                                                + (recursoAcessuas.ValorOrcamentoFederal.HasValue ? recursoAcessuas.ValorOrcamentoFederal.Value : 0M)
                                                + (recursoAcessuas.ValorFundoFederal.HasValue ? recursoAcessuas.ValorFundoFederal.Value : 0M)
                                                + (recursoAcessuas.ValorIGDPBF.HasValue ? recursoAcessuas.ValorIGDPBF.Value : 0M)
                                                + (recursoAcessuas.ValorIGDSUAS.HasValue ? recursoAcessuas.ValorIGDSUAS.Value : 0M)
                                                ).ToString("N2");
                        }
                        else
                        { 
                            lblPMMunicipal.Text = "0,00";
                            lblPMEstadual.Text = "0,00";
                            lblPMFederal.Text = "0,00";
                        }
                    }

                    lblPMTotal.Text = (
                                      Convert.ToDecimal(lblPMMunicipal.Text)
                                      + Convert.ToDecimal(lblPMEstadual.Text)
                                      + Convert.ToDecimal(lblPMFederal.Text)
                                      ).ToString("N2");

                    if (pm.TipoProgramaTransferencia == 2)
                    {
                        //pp = programaProjeto.Service.Get(pm.Id);
                        programasProjetosExercicio1.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pm.Id);
                        if (programasProjetosExercicio1.PrevisaoAnual != null)
                        {
                            lblProgramaMunicipal.Text = (Convert.ToDecimal(lblProgramaMunicipal.Text) + Convert.ToDecimal(programasProjetosExercicio1.PrevisaoAnual.PrevisaoAnualMunicipalExercicio1)).ToString("N2");
                            lblProgramaMunicipalTotal.Text = Convert.ToDecimal(lblProgramaMunicipal.Text).ToString("N2");
                        }
                    }
                }

                lblFCTotal.Text = lblFCEstadual.Text;
                lblFVTotal.Text = lblFVEstadual.Text;

                lblProgDesenvTotalMunicipal.Text = (
                                                     Convert.ToDecimal(String.IsNullOrEmpty(lblACESSUASMunicipal.Text) ? "0" : lblACESSUASMunicipal.Text)
                                                   + Convert.ToDecimal(String.IsNullOrEmpty(lblSPSMunicipal.Text) ? "0" : lblSPSMunicipal.Text)
                                                   + Convert.ToDecimal(String.IsNullOrEmpty(lblSPAIMunicipal.Text) ? "0" : lblSPAIMunicipal.Text)
                                                   + Convert.ToDecimal(String.IsNullOrEmpty(lblPMMunicipal.Text) ? "0" : lblPMMunicipal.Text)
                                                   + Convert.ToDecimal(String.IsNullOrEmpty(lblAPETIMunicipal.Text) ? "0" : lblAPETIMunicipal.Text)
                                                   ).ToString("N2");

                lblProgDesenvTotalEstadual.Text = (
                                                    Convert.ToDecimal(String.IsNullOrEmpty(lblACESSUASEstadual.Text) ? "0" : lblACESSUASEstadual.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblSPSEstadual.Text) ? "0" : lblSPSEstadual.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblSPAIEstadual.Text) ? "0" : lblSPAIEstadual.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblPMEstadual.Text) ? "0" : lblPMEstadual.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblAPETIEstadual.Text) ? "0" : lblAPETIEstadual.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblFCEstadual.Text) ? "0" : lblFCEstadual.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblFVEstadual.Text) ? "0" : lblFVEstadual.Text)
                                                  ).ToString("N2");

                lblProgDesenvTotalFederal.Text = (
                                                   Convert.ToDecimal(String.IsNullOrEmpty(lblACESSUASFederal.Text) ? "0" : lblACESSUASFederal.Text)
                                                 + Convert.ToDecimal(String.IsNullOrEmpty(lblSPSFederal.Text) ? "0" : lblSPSFederal.Text)
                                                 + Convert.ToDecimal(String.IsNullOrEmpty(lblAPETIFederal.Text) ? "0" : lblAPETIFederal.Text)
                                                 + Convert.ToDecimal(String.IsNullOrEmpty(lblSPAIFederal.Text) ? "0" : lblSPAIFederal.Text)
                                                 + Convert.ToDecimal(String.IsNullOrEmpty(lblPMFederal.Text) ? "0" : lblPMFederal.Text)
                                                 ).ToString("N2");

                lblProgDesenvTotalGeral.Text = (
                                                 Convert.ToDecimal(String.IsNullOrEmpty(lblACESSUASTotal.Text) ? "0" : lblACESSUASTotal.Text)
                                               + Convert.ToDecimal(String.IsNullOrEmpty(lblSPSTotal.Text) ? "0" : lblSPSTotal.Text)
                                               + Convert.ToDecimal(String.IsNullOrEmpty(lblSPAITotal.Text) ? "0" : lblSPAITotal.Text)
                                               + Convert.ToDecimal(String.IsNullOrEmpty(lblPMTotal.Text) ? "0" : lblPMTotal.Text)
                                               + Convert.ToDecimal(String.IsNullOrEmpty(lblAPETITotal.Text) ? "0" : lblAPETITotal.Text)
                                               + Convert.ToDecimal(String.IsNullOrEmpty(lblFCTotal.Text) ? "0" : lblFCTotal.Text)
                                               + Convert.ToDecimal(String.IsNullOrEmpty(lblFVEstadual.Text) ? "0" : lblFVEstadual.Text)
                                               ).ToString("N2");
            }

            //BENEFICIO EVENTUAL
            //var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(x => x.Exercicio == exercicio).FirstOrDefault();


            if (beneficio != null)
            {
                lblBEvEstadual.Text = beneficio.ValorAnualEstadual != null ? beneficio.ValorAnualEstadual.ToString("N2") : "0,00";
                lblBEvMunicipal.Text = beneficio.ValorAnualMunicipal != null? beneficio.ValorAnualMunicipal.ToString("N2") : "0,00";
                lblBEvFederal.Text = beneficio.ValorAnualFederal !=null ? beneficio.ValorAnualFederal.ToString("N2") : "0,00";
                lblBEvTotal.Text = (beneficio.ValorAnualEstadual + beneficio.ValorAnualFederal + beneficio.ValorAnualMunicipal + beneficio.ValorAnualPrivado).ToString("N2");
            }


            //IDOSO   
            var transferenciaRenda = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            using (var programaProjeto = new ProxyProgramas())
            {
                //Ação Jovem
                var acao = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 5).Id);
                if (acao != null)
                {
                    lblAcaoJovemEstadual.Text = acao.CalculoAcaoRendaPrevisaoAnualExercicio1.HasValue ? acao.CalculoAcaoRendaPrevisaoAnualExercicio1.Value.ToString("N2") : "0,00";
                    lblAcaoJovemTotal.Text = acao.CalculoAcaoRendaPrevisaoAnualExercicio1.HasValue ? acao.CalculoAcaoRendaPrevisaoAnualExercicio1.Value.ToString("N2") : "0,00";
                }
                else
                {
                    lblAcaoJovemEstadual.Text = "0,00";
                    lblAcaoJovemTotal.Text = "0,00";
                }

                //Renda Cidadã
                var renda = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 6).Id);
                if (renda != null)
                {
                    lblRendaCidadaEstadual.Text = renda.CalculoAcaoRendaPrevisaoAnualExercicio1.HasValue ? renda.CalculoAcaoRendaPrevisaoAnualExercicio1.Value.ToString("N2") : "0,00";
                    lblRendaCidadaTotal.Text = renda.CalculoAcaoRendaPrevisaoAnualExercicio1.HasValue ? renda.CalculoAcaoRendaPrevisaoAnualExercicio1.Value.ToString("N2") : "0,00";
                }
                else { 
                    lblRendaCidadaEstadual.Text = "0,00";
                    lblRendaCidadaTotal.Text = "0,00";
                }

                var idoso = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 1).Id);
                if (idoso != null)
                {

                    lblBPCIdososFederal.Text = idoso.CalculoBPCPrevisaoAnualExercicio1.HasValue ? idoso.CalculoBPCPrevisaoAnualExercicio1.Value.ToString("N2") : "0,00";
                    lblBPCIdososTotal.Text = idoso.CalculoBPCPrevisaoAnualExercicio1.HasValue ? idoso.CalculoBPCPrevisaoAnualExercicio1.Value.ToString("N2") : "0,00";
                }
                else
                { 
                     lblBPCIdososFederal.Text= "0,00";
                     lblBPCIdososTotal.Text = "0,00";
                }

                //Pessoa com deficiência
                var pcd = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 2).Id); //transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 2);
                if (pcd != null)
                {
                    lblBPCPCDFederal.Text = pcd.CalculoBPCPrevisaoAnualExercicio1.HasValue ? pcd.CalculoBPCPrevisaoAnualExercicio1.Value.ToString("N2") : "0,00";
                    lblBPCPCDTotal.Text = pcd.CalculoBPCPrevisaoAnualExercicio1.HasValue ? pcd.CalculoBPCPrevisaoAnualExercicio1.Value.ToString("N2") : "0,00";
                }
                else
                { 
                    lblBPCPCDFederal.Text = "0,00";
                    lblBPCPCDTotal.Text = "0,00";
                }

                var p = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                if (p != null)
                {
                    var amigoIdoso = p.Where(t => t.Nome.ToLower().Contains("amigo do idoso"));
                    if (amigoIdoso != null)
                    {
                        var programaAmigoDoIdosoMunicipal = amigoIdoso.FirstOrDefault();

                        if (programaAmigoDoIdosoMunicipal != null)
                        {
                            ProgramaProjetoPrevisaoAnualBeneficiariosInfo pp = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaAmigoDoIdosoMunicipal.Id);
                            if (pp != null)
                            {
                                lblRendaCidadaBeneficioIdosoEstadual.Text = (pp.MetaPactuadaExercicio1 * 100M * 12).ToString("N2");
                                lblRendaCidadaBeneficioIdosoTotal.Text = lblRendaCidadaBeneficioIdosoEstadual.Text;
                            }
                            else
                            {
                                lblRendaCidadaBeneficioIdosoEstadual.Text = "0,00";
                                lblRendaCidadaBeneficioIdosoTotal.Text = "0,00";
                            }
                        }
                        else
                        { 
                            lblRendaCidadaBeneficioIdosoEstadual.Text ="0,00";
                            lblRendaCidadaBeneficioIdosoTotal.Text = "0,00";
                        }
                    }
                }

                //Prospera familia

                lblPFTotal.Text = lblPFEstadual.Text;

                //Bolsa Família

                if (transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 3) != null)
                {

                    var bolsa = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 3).Id); //transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 3);

                    if (bolsa != null)
                    {


                        lblBolsaFamiliaFederal.Text = bolsa.RepasseMensal2021.HasValue ? (bolsa.RepasseMensal2021.Value * 12).ToString("N2") : String.Empty;
                        lblBolsaFamiliaTotal.Text = bolsa.RepasseMensal2021.HasValue ? (bolsa.RepasseMensal2021.Value * 12).ToString("N2") : String.Empty;
                    }
                }
            }



            //PETI
            var peti = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 4);

            lblProgramaTotalMunicipal.Text = (
                                                  Convert.ToDecimal(String.IsNullOrEmpty(lblBEvMunicipal.Text) ? "0" : lblBEvMunicipal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBPCIdososMunicipal.Text) ? "0" : lblBPCIdososMunicipal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBPCPCDMunicipal.Text) ? "0" : lblBPCPCDMunicipal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblAcaoJovemMunicipal.Text) ? "0" : lblAcaoJovemMunicipal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblRendaCidadaMunicipal.Text) ? "0" : lblRendaCidadaMunicipal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBolsaFamiliaMunicipal.Text) ? "0" : lblBolsaFamiliaMunicipal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblProgramaMunicipal.Text) ? "0" : lblProgramaMunicipal.Text)
                                            ).ToString("n2");

            lblProgramaTotalEstadual.Text = (
                                                  Convert.ToDecimal(String.IsNullOrEmpty(lblBEvEstadual.Text) ? "0" : lblBEvEstadual.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBPCIdososEstadual.Text) ? "0" : lblBPCIdososEstadual.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBPCPCDEstadual.Text) ? "0" : lblBPCPCDEstadual.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblAcaoJovemEstadual.Text) ? "0" : lblAcaoJovemEstadual.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblRendaCidadaEstadual.Text) ? "0" : lblRendaCidadaEstadual.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblRendaCidadaBeneficioIdosoEstadual.Text) ? "0" : lblRendaCidadaBeneficioIdosoEstadual.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBolsaFamiliaEstadual.Text) ? "0" : lblBolsaFamiliaEstadual.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblPFEstadual.Text) ? "0" : lblPFEstadual.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblProgramaEstadual.Text) ? "0" : lblProgramaEstadual.Text)
                                            ).ToString("n2");

            lblProgramaTotalFederal.Text = (
                                                  Convert.ToDecimal(String.IsNullOrEmpty(lblBEvFederal.Text) ? "0" : lblBEvFederal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBPCIdososFederal.Text) ? "0" : lblBPCIdososFederal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBPCPCDFederal.Text) ? "0" : lblBPCPCDFederal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblAcaoJovemFederal.Text) ? "0" : lblAcaoJovemFederal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblRendaCidadaFederal.Text) ? "0" : lblRendaCidadaFederal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBolsaFamiliaFederal.Text) ? "0" : lblBolsaFamiliaFederal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblProgramaFederal.Text) ? "0" : lblProgramaFederal.Text)
                                            ).ToString("n2");

            lblProgramaTotalGeral.Text =     (
                                                  Convert.ToDecimal(String.IsNullOrEmpty(lblBEvTotal.Text) ? "0" : lblBEvTotal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBPCIdososTotal.Text) ? "0" : lblBPCIdososTotal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBPCPCDTotal.Text) ? "0" : lblBPCPCDTotal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblAcaoJovemTotal.Text) ? "0" : lblAcaoJovemTotal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblRendaCidadaTotal.Text) ? "0" : lblRendaCidadaTotal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblRendaCidadaBeneficioIdosoTotal.Text) ? "0" : lblRendaCidadaBeneficioIdosoTotal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblBolsaFamiliaTotal.Text) ? "0" : lblBolsaFamiliaTotal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblProgramaMunicipalTotal.Text) ? "0" : lblProgramaMunicipalTotal.Text)
                                                + Convert.ToDecimal(String.IsNullOrEmpty(lblPFTotal.Text) ? "0" : lblPFTotal.Text)
                                            ).ToString("n2");

            var indice = prefeituras.GetIndiceGestaoDescentralizada(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
            if (indice != null)
            {
                var IGD = 0m;
                if (indice.IGDPBFValorAnual.HasValue)
                    IGD += indice.IGDPBFValorAnual.Value;
                if (indice.IGDSUASValorAnual.HasValue)
                    IGD += indice.IGDSUASValorAnual.Value;
            }

        }

        void carregarProgramasExercicio2(Prefeituras prefeituras)
        {
            int exercicio = FPrevisaoOrcamentaria.Exercicios[1];
            decimal valorCriancaFeliz = 0;
            //Programas desenvolvidos no município
            using (var programaProjeto = new ProxyProgramas())
            {
                var programasProjetosFederais = programaProjeto.Service
                                                               .GetConsultaProgramasProjetosFederaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);

                #region Programas Projetos Acessuas

                var programasProjetosAcessuasExercicio2 = programasProjetosFederais.Where(t => t.Nome.ToLower().Contains("acessuas"));
                if (programasProjetosAcessuasExercicio2 != null)
                {
                    var programaProjetoExercicio2 = programasProjetosAcessuasExercicio2.Single();
                    var programaProjetosAcessuaExercicio2 = programaProjeto.Service.GetProgramaProjetoById(programaProjetoExercicio2.Id);

                    if (programaProjetosAcessuaExercicio2 != null)
                    {
                        programaProjetosAcessuaExercicio2.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetosAcessuaExercicio2.Id);

                        if (programasProjetosAcessuasExercicio2 != null)
                        {
                            var recursoAcessuas = programaProjetosAcessuaExercicio2.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();

                            if (recursoAcessuas != null)
                            {
                                lblACESSUASMunicipalExercicio2.Text = ((recursoAcessuas.FonteFMAS.HasValue && recursoAcessuas.FonteFMAS.Value
                                    && recursoAcessuas.ValorFMAS.HasValue ? recursoAcessuas.ValorFMAS.Value : (0M))
                                                            + (recursoAcessuas.FonteOrcamentoMunicipal.HasValue
                                                            && recursoAcessuas.FonteOrcamentoMunicipal.Value && recursoAcessuas.ValorOrcamentoMunicipal.HasValue
                                                            ? recursoAcessuas.ValorOrcamentoMunicipal.Value : (0M))
                                                            + (recursoAcessuas.FonteFundoMunicipal.HasValue && recursoAcessuas.FonteFundoMunicipal.Value
                                                            && recursoAcessuas.ValorFundoMunicipal.HasValue ? recursoAcessuas.ValorFundoMunicipal.Value : (0M))
                                                            ).ToString("N2");

                                if (programaProjetosAcessuaExercicio2.PrevisaoAnual != null)
                                {
                                    lblACESSUASFederalExercicio2.Text = (programaProjetosAcessuaExercicio2.PrevisaoAnual.PrevisaoAnualRepasseExercicio2).ToString("N2");
                                    lblACESSUASTotalExercicio2.Text = (
                                                            (recursoAcessuas.FonteFMAS.HasValue && recursoAcessuas.FonteFMAS.Value
                                                            && recursoAcessuas.ValorFMAS.HasValue ? recursoAcessuas.ValorFMAS.Value : (0M)) +
                                                            (recursoAcessuas.FonteOrcamentoMunicipal.HasValue && recursoAcessuas.FonteOrcamentoMunicipal.Value
                                                            && recursoAcessuas.ValorOrcamentoMunicipal.HasValue ? recursoAcessuas.ValorOrcamentoMunicipal.Value : (0M)) +
                                                            (recursoAcessuas.FonteFundoMunicipal.HasValue && recursoAcessuas.FonteFundoMunicipal.Value
                                                            && recursoAcessuas.ValorFundoMunicipal.HasValue ? recursoAcessuas.ValorFundoMunicipal.Value : (0M))
                                                             + (programaProjetosAcessuaExercicio2.PrevisaoAnual.PrevisaoAnualRepasseExercicio2)
                                                            ).ToString("N2");
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Programas Projetos Primeira Infancia Suas

                var programa = programasProjetosFederais.SingleOrDefault(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ"));
                if (programa != null)
                {
                    var programasProjetosPrimeiraInfanciaSuas = programaProjeto.Service.GetProgramaProjetoById(programa.Id);

                    if (programasProjetosPrimeiraInfanciaSuas != null)
                    {

                        programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programasProjetosPrimeiraInfanciaSuas.Id);

                        if (programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual != null)
                        {
                            valorCriancaFeliz = programasProjetosPrimeiraInfanciaSuas.ExecutaPrograma ? programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio2 : (0M);

                            lblSPSTotalExercicio2.Text = lblSPSFederalExercicio2.Text = valorCriancaFeliz.ToString("N2");
                        }


                        if (programasProjetosPrimeiraInfanciaSuas.ProgramasProjetosRecursoFinanceiro != null)
                        {
                            var recursoInfanciaSuas = programasProjetosPrimeiraInfanciaSuas.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                            if (recursoInfanciaSuas != null)
                            {
                                lblSPSMunicipalExercicio2.Text = ((recursoInfanciaSuas.FonteFMAS.HasValue && recursoInfanciaSuas.FonteFMAS.Value
                                    && recursoInfanciaSuas.ValorFMAS.HasValue ? recursoInfanciaSuas.ValorFMAS.Value : (0M))
                                                           + (recursoInfanciaSuas.FonteOrcamentoMunicipal.HasValue
                                                           && recursoInfanciaSuas.FonteOrcamentoMunicipal.Value
                                                           && recursoInfanciaSuas.ValorOrcamentoMunicipal.HasValue ? recursoInfanciaSuas.ValorOrcamentoMunicipal.Value : (0M))
                                                           + (recursoInfanciaSuas.FonteFundoMunicipal.HasValue
                                                           && recursoInfanciaSuas.FonteFundoMunicipal.Value
                                                           && recursoInfanciaSuas.ValorFundoMunicipal.HasValue ? recursoInfanciaSuas.ValorFundoMunicipal.Value : (0M))
                                                           ).ToString("N2");
                                lblSPSEstadualExercicio2.Text = "0,00";

                                if (programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual != null)
                                {
                             

                                    lblSPSTotalExercicio2.Text = ((recursoInfanciaSuas.FonteFMAS.HasValue
                                        && recursoInfanciaSuas.FonteFMAS.Value
                                        && recursoInfanciaSuas.ValorFMAS.HasValue ? recursoInfanciaSuas.ValorFMAS.Value : (0M))
                                                               + (recursoInfanciaSuas.FonteOrcamentoMunicipal.HasValue
                                                               && recursoInfanciaSuas.FonteOrcamentoMunicipal.Value
                                                               && recursoInfanciaSuas.ValorOrcamentoMunicipal.HasValue ? recursoInfanciaSuas.ValorOrcamentoMunicipal.Value : (0M))
                                                               + (recursoInfanciaSuas.FonteFundoMunicipal.HasValue
                                                               && recursoInfanciaSuas.FonteFundoMunicipal.Value
                                                               && recursoInfanciaSuas.ValorFundoMunicipal.HasValue ? recursoInfanciaSuas.ValorFundoMunicipal.Value : (0M))
                                                               + valorCriancaFeliz).ToString("N2");
                                }

                            }
                        }
                    }
                }
                #endregion

                var programasEstaduais = programaProjeto.Service
                                                        .GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                //.Where(x => x.Exercicio == exercicio);

                #region Amigo do idoso

                var programasAmigoDoIdoso = programasEstaduais.Where(t => t.Nome.ToLower().Contains("amigo do idoso") && t.ProgramaEstadual == true);

                if (programasAmigoDoIdoso != null && programasAmigoDoIdoso.Any())
                {
                    int idProgramaDoIdoso = programasAmigoDoIdoso.First().Id;

                    ProgramaProjetoInfo amigoIdoso = programaProjeto.Service.GetProgramaProjetoById(idProgramaDoIdoso);
                    if (amigoIdoso != null)
                    {
                        var parcelas = amigoIdoso.ProgramasProjetosParcelasInfo.ToList();
                        decimal SPAIEstadualExercicio2 = 0;

                        foreach (var parcela in parcelas)
                        {
                            if (amigoIdoso.ConvenioCentroDiaIdoso && parcela.AnoRepasseDiaIdoso.HasValue && parcela.AnoRepasseDiaIdoso.Value == exercicio)
                            {
                                SPAIEstadualExercicio2 += ((parcela.ValorDiaIdoso.HasValue ? parcela.ValorDiaIdoso.Value : 0M));
                            }

                            if (amigoIdoso.ConvenioCentroConvivenciaIdoso && parcela.AnoRepasseConvivenciaIdoso.HasValue && parcela.AnoRepasseConvivenciaIdoso.Value == exercicio)
                            {
                                SPAIEstadualExercicio2 += ((parcela.ValorConvivenciaIdoso.HasValue ? parcela.ValorConvivenciaIdoso.Value : 0M));
                            }

                            if (parcela.AnoRepasseDiaIdoso.HasValue && parcela.AnoRepasseDiaIdoso.Value == exercicio &&
                                parcela.AnoRepasseConvivenciaIdoso.HasValue && parcela.AnoRepasseConvivenciaIdoso.Value == exercicio)
                            {
                                lblSPAIEstadualExercicio2.Text = (
                                                          (parcela.ValorDiaIdoso.HasValue ? parcela.ValorDiaIdoso.Value : 0M)
                                                          + (parcela.ValorConvivenciaIdoso.HasValue ? parcela.ValorConvivenciaIdoso.Value : 0M)
                                                          ).ToString("N2");
                            }
                        }

                        lblSPAIEstadualExercicio2.Text = SPAIEstadualExercicio2.ToString("N2");

                        lblSPAITotalExercicio2.Text = (
                                            Convert.ToDecimal(lblSPAIMunicipalExercicio2.Text)
                                            + Convert.ToDecimal(lblSPAIEstadualExercicio2.Text)
                                            + Convert.ToDecimal(lblSPAIFederalExercicio2.Text)
                                            ).ToString("N2");
                    }
                }
                #endregion

                var transferencia = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region peti
                var acoesPeti = programaProjeto.Service.GetTransferenciaRendaById(transferencia.SingleOrDefault(t => t.Nome.ToLower().Contains("peti")).Id);
                if (acoesPeti != null)
                {
                    lblAPETIMunicipalExercicio2.Text = "0,00";

                    lblAPETIEstadualExercicio2.Text = "0,00";

                    lblAPETIFederalExercicio2.Text = (0M).ToString("N2");

                    lblAPETITotalExercicio2.Text = (Convert.ToDecimal(lblAPETIFederalExercicio2.Text)).ToString("N2");
                }
                #endregion peti

                #region programa e projetos municipais
                var programasProjetos = programaProjeto.Service.GetConsultaProgramasProjetosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                if (programasProjetos != null)
                {
                    foreach (var pm in programasProjetos)
                    {

                        var programasProjetoMunicipal = programaProjeto.Service.GetProgramaProjetoById(pm.Id);
                        if (programasProjetoMunicipal.ProgramasProjetosRecursoFinanceiro != null)
                        {
                            var programaRecursoFinanceiroExercicio = programasProjetoMunicipal.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                            if (programaRecursoFinanceiroExercicio != null)
                            {
                                lblPMMunicipalExercicio2.Text = (
                                                      Convert.ToDecimal(lblPMMunicipalExercicio2.Text)
                                                      + (programaRecursoFinanceiroExercicio.ValorFMAS.HasValue ? programaRecursoFinanceiroExercicio.ValorFMAS.Value : 0M)
                                                      + (programaRecursoFinanceiroExercicio.ValorOrcamentoMunicipal.HasValue ? programaRecursoFinanceiroExercicio.ValorOrcamentoMunicipal.Value : 0M)
                                                      + (programaRecursoFinanceiroExercicio.ValorFundoMunicipal.HasValue ? programaRecursoFinanceiroExercicio.ValorFundoMunicipal.Value : 0M)
                                                      ).ToString("N2");

                                lblPMEstadualExercicio2.Text = (
                                                     Convert.ToDecimal(lblPMEstadualExercicio2.Text)
                                                     + (programaRecursoFinanceiroExercicio.ValorFEAS.HasValue ? programaRecursoFinanceiroExercicio.ValorFEAS.Value : 0M)
                                                     + (programaRecursoFinanceiroExercicio.ValorOrcamentoEstadual.HasValue ? programaRecursoFinanceiroExercicio.ValorOrcamentoEstadual.Value : 0M)
                                                     + (programaRecursoFinanceiroExercicio.ValorFundoEstadual.HasValue ? programaRecursoFinanceiroExercicio.ValorFundoEstadual.Value : 0M)
                                                     ).ToString("N2");

                                lblPMFederalExercicio2.Text = (
                                                    Convert.ToDecimal(lblPMFederalExercicio2.Text)
                                                    + (programaRecursoFinanceiroExercicio.ValorFNAS.HasValue ? programaRecursoFinanceiroExercicio.ValorFNAS.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorOrcamentoFederal.HasValue ? programaRecursoFinanceiroExercicio.ValorOrcamentoFederal.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorFundoFederal.HasValue ? programaRecursoFinanceiroExercicio.ValorFundoFederal.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorIGDPBF.HasValue ? programaRecursoFinanceiroExercicio.ValorIGDPBF.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorIGDSUAS.HasValue ? programaRecursoFinanceiroExercicio.ValorIGDSUAS.Value : 0M)
                                                    ).ToString("N2");
                            }
                        }

                        lblPMTotalExercicio2.Text = (
                                          Convert.ToDecimal(lblPMMunicipalExercicio2.Text)
                                          + Convert.ToDecimal(lblPMEstadualExercicio2.Text)
                                          + Convert.ToDecimal(lblPMFederalExercicio2.Text)
                                          ).ToString("N2");
                        if (pm.TipoProgramaTransferencia == 2)
                        {

                            var metaPactuada2019 = programasProjetoMunicipal.PrevisaoAnual.MetaPactuadaExercicio2.ToString();
                            var previsaoAnual2019 = programasProjetoMunicipal.PrevisaoAnual.PrevisaoAnualRepasseExercicio2.ToString("N2");
                            //if (!String.IsNullOrEmpty(previsaoAnual2019) && !String.IsNullOrEmpty(metaPactuada2019))
                            //{
                            //    lblPrevisaoAnualTotal2019.Text = (programasProjetoMunicipal.PrevisaoAnual.PrevisaoAnualMunicipal2019).ToString("N2");
                            //}

                            programasProjetoMunicipal.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pm.Id);
                            if (programasProjetoMunicipal.PrevisaoAnual != null)
                            {
                                lblProgramaMunicipalExercicio2.Text = (Convert.ToDecimal(lblProgramaMunicipalExercicio2.Text) + Convert.ToDecimal(programasProjetoMunicipal.PrevisaoAnual.PrevisaoAnualMunicipalExercicio2)).ToString("N2");
                                lblProgramaMunicipalTotalExercicio2.Text = Convert.ToDecimal(lblProgramaMunicipalExercicio2.Text).ToString("N2");
                            }
                        }
                    }
                }
                #endregion programa e projetos municipais

                #region totais

                var transferenciaRendaProgramas = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                #region ProsperaFamilia
                var prospera = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 10).Id);

                if (prospera != null)
                {
                    lblPFEstadualExercicio2.Text = prospera.ValorRepasseEstadual2023.HasValue ? prospera.ValorRepasseEstadual2023.Value.ToString("N2") : "0,00";
                    lblPFTotalExercicio2.Text = lblPFEstadualExercicio2.Text;
                }

                #endregion

                #region FortalecimentoCadUnico
                var fortalecimento = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 11).Id);

                if (fortalecimento != null)
                {
                    lblFCEstadualExercicio2.Text = fortalecimento.ValorRepasseEstadual2023.HasValue ? fortalecimento.ValorRepasseEstadual2023.Value.ToString("N2") : "0,00";
                    lblFCTotalExercicio2.Text = lblFCEstadualExercicio2.Text;
                }

                #endregion

                #region FortalecimentoDaVigilânciaSocioassistencial
                var fortalecimentoVigilancia = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 12).Id);

                if (fortalecimentoVigilancia != null)
                {
                    lblFVEstadualExercicio2.Text = fortalecimentoVigilancia.ValorRepasseEstadual2023.HasValue ? fortalecimentoVigilancia.ValorRepasseEstadual2023.Value.ToString("N2") : "0,00";
                    lblFVTotalExercicio2.Text = lblFVEstadualExercicio2.Text;
                }

                #endregion

                lblProgDesenvTotalMunicipalExercicio2.Text = (
                                                   Convert.ToDecimal(lblACESSUASMunicipalExercicio2.Text)
                                                   + Convert.ToDecimal(lblSPSMunicipalExercicio2.Text)
                                                   + Convert.ToDecimal(lblSPAIMunicipalExercicio2.Text)
                                                   + Convert.ToDecimal(lblPMMunicipalExercicio2.Text)
                                                   + Convert.ToDecimal(lblAPETIMunicipalExercicio2.Text)
                                                   ).ToString("N2");

                lblProgDesenvTotalEstadualExercicio2.Text = (
                                                  Convert.ToDecimal(lblACESSUASEstadualExercicio2.Text)
                                                  + Convert.ToDecimal(lblSPSEstadualExercicio2.Text)
                                                  + Convert.ToDecimal(lblSPAIEstadualExercicio2.Text)
                                                  + Convert.ToDecimal(lblPMEstadualExercicio2.Text)
                                                  + Convert.ToDecimal(lblAPETIEstadualExercicio2.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblFCEstadualExercicio2.Text) ? "0" : lblFCEstadualExercicio2.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblFVEstadualExercicio2.Text) ? "0" : lblFVEstadualExercicio2.Text)
                                                  ).ToString("N2");

                lblProgDesenvTotalFederalExercicio2.Text = (
                                                 Convert.ToDecimal(lblACESSUASFederalExercicio2.Text)
                                                 + Convert.ToDecimal(lblSPSFederalExercicio2.Text)
                                                 + Convert.ToDecimal(lblAPETIFederalExercicio2.Text)
                                                 + Convert.ToDecimal(lblSPAIFederalExercicio2.Text)
                                                 + Convert.ToDecimal(lblPMFederalExercicio2.Text)
                                                 ).ToString("N2");

                lblProgDesenvTotalGeralExercicio2.Text = (
                                               Convert.ToDecimal(lblACESSUASTotalExercicio2.Text)
                                               + Convert.ToDecimal(lblSPSTotalExercicio2.Text)
                                               + Convert.ToDecimal(lblSPAITotalExercicio2.Text)
                                               + Convert.ToDecimal(lblPMTotalExercicio2.Text)
                                               + Convert.ToDecimal(lblAPETITotalExercicio2.Text)
                                               + Convert.ToDecimal(lblFCTotalExercicio2.Text)
                                               + Convert.ToDecimal(lblFVTotalExercicio2.Text)
                                               ).ToString("N2");
                #endregion totais

            }




            #region programas

            #region Beneficio Eventual
            //var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(x => x.Exercicio == exercicio).FirstOrDefault();


            if (beneficio != null)
            {
                lblBEvEstadualExercicio2.Text = beneficio.ValorAnualEstadual.ToString("N2");
                lblBEvMunicipalExercicio2.Text = beneficio.ValorAnualMunicipal.ToString("N2");
                lblBEvFederalExercicio2.Text = beneficio.ValorAnualFederal.ToString("N2");
                lblBEvTotalExercicio2.Text = (beneficio.ValorAnualEstadual + beneficio.ValorAnualFederal + beneficio.ValorAnualMunicipal + beneficio.ValorAnualPrivado).ToString("N2");
            }

            #endregion

            var transferenciaRenda = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            using (var programaProjeto = new ProxyProgramas())
            {
                #region Acao Jovem
                var acao = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 5).Id);
                if (acao != null)
                {
                    lblAcaoJovemEstadualExercicio2.Text = acao.CalculoAcaoRendaPrevisaoAnualExercicio2.HasValue ? acao.CalculoAcaoRendaPrevisaoAnualExercicio2.Value.ToString("N2") : "0,00";
                    lblAcaoJovemTotalExercicio2.Text = acao.CalculoAcaoRendaPrevisaoAnualExercicio2.HasValue ? acao.CalculoAcaoRendaPrevisaoAnualExercicio2.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region Renda Cidadã
                //Renda Cidadã
                var renda = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 6).Id);
                if (renda != null)
                {
                    lblRendaCidadaEstadualExercicio2.Text = renda.CalculoAcaoRendaPrevisaoAnualExercicio2.HasValue ? renda.CalculoAcaoRendaPrevisaoAnualExercicio2.Value.ToString("N2") : "0,00";
                    lblRendaCidadaTotalExercicio2.Text = renda.CalculoAcaoRendaPrevisaoAnualExercicio2.HasValue ? renda.CalculoAcaoRendaPrevisaoAnualExercicio2.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region BPC Idosos

                var idoso = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 1).Id);
                if (idoso != null)
                {
                    lblBPCIdososFederalExercicio2.Text = idoso.CalculoBPCPrevisaoAnualExercicio2.HasValue ? idoso.CalculoBPCPrevisaoAnualExercicio2.Value.ToString("N2") : "0,00";
                    lblBPCIdososTotalExercicio2.Text = idoso.CalculoBPCPrevisaoAnualExercicio2.HasValue ? idoso.CalculoBPCPrevisaoAnualExercicio2.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region BPC - PCD [Pessoa com deficiência]
                var pcd = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 2).Id);
                if (pcd != null)
                {
                    lblBPCPCDFederalExercicio2.Text = pcd.CalculoBPCPrevisaoAnualExercicio2.HasValue ? pcd.CalculoBPCPrevisaoAnualExercicio2.Value.ToString("N2") : "0,00";
                    lblBPCPCDTotalExercicio2.Text = pcd.CalculoBPCPrevisaoAnualExercicio2.HasValue ? pcd.CalculoBPCPrevisaoAnualExercicio2.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region Renda cidadã - Benificio Idoso
                var pIdoso = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                if (pIdoso != null && pIdoso.Any())
                {
                    var pIdosoSingle = pIdoso.Where(t => t.Nome.ToLower().Contains("amigo do idoso")).FirstOrDefault();
                    if (pIdosoSingle != null && pIdosoSingle != null)
                    {
                        int idPIdoso = pIdosoSingle.Id;

                        var ppIdoso = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(idPIdoso);
                        if (ppIdoso != null)
                        {
                            lblRendaCidadaBeneficioIdosoEstadualExercicio2.Text = (ppIdoso.MetaPactuadaExercicio2 * 100M * 12).ToString("N2");
                            lblRendaCidadaBeneficioIdosoTotalExercicio2.Text = lblRendaCidadaBeneficioIdosoEstadualExercicio2.Text;
                        }
                    }
                }
                #endregion

                #region Bolsa familia
                //Bolsa Família
                if (transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 3) != null)
                {
                    var bolsa = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 3).Id);
                    if (bolsa != null)
                    {
                        lblBolsaFamiliaFederalExercicio2.Text = bolsa.RepasseMensal2022.HasValue ? (bolsa.RepasseMensal2022.Value * 12).ToString("N2") : "0,00";
                        lblBolsaFamiliaTotalExercicio2.Text = bolsa.RepasseMensal2022.HasValue ? (bolsa.RepasseMensal2022.Value * 12).ToString("N2") : "0,00";
                    }
                }
                #endregion

            }
            #endregion

            #region PETI

            var peti = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 4);

            lblProgramaTotalMunicipalExercicio2.Text = (Convert.ToDecimal(lblBEvMunicipalExercicio2.Text)
                                            + Convert.ToDecimal(lblBPCIdososMunicipalExercicio2.Text)
                                            + Convert.ToDecimal(lblBPCPCDMunicipalExercicio2.Text)
                                            + Convert.ToDecimal(lblAcaoJovemMunicipalExercicio2.Text)
                                            + Convert.ToDecimal(lblRendaCidadaMunicipalExercicio2.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaMunicipalExercicio2.Text)
                                            + Convert.ToDecimal(lblProgramaMunicipalExercicio2.Text)).ToString("n2");

            lblProgramaTotalEstadualExercicio2.Text = (Convert.ToDecimal(lblBEvEstadualExercicio2.Text)
                                            + Convert.ToDecimal(lblBPCIdososEstadualExercicio2.Text)
                                            + Convert.ToDecimal(lblBPCPCDEstadualExercicio2.Text)
                                            + Convert.ToDecimal(lblAcaoJovemEstadualExercicio2.Text)
                                            + Convert.ToDecimal(lblRendaCidadaEstadualExercicio2.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoEstadualExercicio2.Text)
                                            + Convert.ToDecimal(String.IsNullOrEmpty(lblPFEstadualExercicio2.Text) ? "0" : lblPFEstadualExercicio2.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaEstadualExercicio2.Text)
                                            + Convert.ToDecimal(lblProgramaEstadualExercicio2.Text)
                                            ).ToString("n2");

            lblProgramaTotalFederalExercicio2.Text = (Convert.ToDecimal(lblBEvFederalExercicio2.Text)
                                            + Convert.ToDecimal(lblBPCIdososFederalExercicio2.Text)
                                            + Convert.ToDecimal(lblBPCPCDFederalExercicio2.Text)
                                            + Convert.ToDecimal(lblAcaoJovemFederalExercicio2.Text)
                                            + Convert.ToDecimal(lblRendaCidadaFederalExercicio2.Text)
                                            + (String.IsNullOrEmpty(lblBolsaFamiliaFederalExercicio2.Text) ? 0M : Convert.ToDecimal(lblBolsaFamiliaFederalExercicio2.Text))
                                            + Convert.ToDecimal(lblProgramaFederalExercicio2.Text)).ToString("n2");

            lblProgramaTotalGeralExercicio2.Text = (Convert.ToDecimal(lblBEvTotalExercicio2.Text)
                                            + Convert.ToDecimal(lblBPCIdososTotalExercicio2.Text)
                                            + Convert.ToDecimal(lblBPCPCDTotalExercicio2.Text)
                                            + Convert.ToDecimal(lblAcaoJovemTotalExercicio2.Text)
                                            + Convert.ToDecimal(lblRendaCidadaTotalExercicio2.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoTotalExercicio2.Text)
                                            + Convert.ToDecimal(lblPFTotalExercicio2.Text)
                                            + (String.IsNullOrEmpty(lblBolsaFamiliaTotalExercicio2.Text) ? 0M : Convert.ToDecimal(lblBolsaFamiliaTotalExercicio2.Text))
                                            + Convert.ToDecimal(lblProgramaMunicipalTotalExercicio2.Text)).ToString("n2");
            #endregion

            #region Indices [IGD, IGDPBF, IGDSUAS]
            var indice = prefeituras.GetIndiceGestaoDescentralizada(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
            if (indice != null)
            {
                var IGD = 0m;
                if (indice.IGDPBFValorAnual.HasValue)
                    IGD += indice.IGDPBFValorAnual.Value;
                if (indice.IGDSUASValorAnual.HasValue)
                    IGD += indice.IGDSUASValorAnual.Value;
            }
            #endregion

        }

        void carregarProgramasExercicio3(Prefeituras prefeituras)
        {
            int exercicio = FPrevisaoOrcamentaria.Exercicios[2];
            //Programas desenvolvidos no município
            using (var programaProjeto = new ProxyProgramas())
            {
                var programasProjetosFederais = programaProjeto.Service
                                                               .GetConsultaProgramasProjetosFederaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);

                #region Programas Projetos Acessuas

                var programasProjetosAcessuasExercicio3 = programasProjetosFederais.Where(t => t.Nome.ToLower().Contains("acessuas"));
                if (programasProjetosAcessuasExercicio3 != null)
                {
                    var programaProjetoExercicio3 = programasProjetosAcessuasExercicio3.Single();
                    var programaProjetosAcessuaExercicio3 = programaProjeto.Service.GetProgramaProjetoById(programaProjetoExercicio3.Id);

                    if (programaProjetosAcessuaExercicio3 != null)
                    {
                        programaProjetosAcessuaExercicio3.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetosAcessuaExercicio3.Id);

                        if (programasProjetosAcessuasExercicio3 != null)
                        {
                            var recursoAcessuas = programaProjetosAcessuaExercicio3.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();

                            if (recursoAcessuas != null)
                            {
                                lblACESSUASMunicipalExercicio3.Text = ((recursoAcessuas.FonteFMAS.HasValue && recursoAcessuas.FonteFMAS.Value
                                    && recursoAcessuas.ValorFMAS.HasValue ? recursoAcessuas.ValorFMAS.Value : (0M))
                                                            + (recursoAcessuas.FonteOrcamentoMunicipal.HasValue
                                                            && recursoAcessuas.FonteOrcamentoMunicipal.Value && recursoAcessuas.ValorOrcamentoMunicipal.HasValue
                                                            ? recursoAcessuas.ValorOrcamentoMunicipal.Value : (0M))
                                                            + (recursoAcessuas.FonteFundoMunicipal.HasValue && recursoAcessuas.FonteFundoMunicipal.Value
                                                            && recursoAcessuas.ValorFundoMunicipal.HasValue ? recursoAcessuas.ValorFundoMunicipal.Value : (0M))
                                                            ).ToString("N2");

                                if (programaProjetosAcessuaExercicio3.PrevisaoAnual != null)
                                {
                                    lblACESSUASFederalExercicio3.Text = (programaProjetosAcessuaExercicio3.PrevisaoAnual.PrevisaoAnualRepasseExercicio3).ToString("N2");
                                    lblACESSUASTotalExercicio3.Text = (
                                                            (recursoAcessuas.FonteFMAS.HasValue && recursoAcessuas.FonteFMAS.Value
                                                            && recursoAcessuas.ValorFMAS.HasValue ? recursoAcessuas.ValorFMAS.Value : (0M)) +
                                                            (recursoAcessuas.FonteOrcamentoMunicipal.HasValue && recursoAcessuas.FonteOrcamentoMunicipal.Value
                                                            && recursoAcessuas.ValorOrcamentoMunicipal.HasValue ? recursoAcessuas.ValorOrcamentoMunicipal.Value : (0M)) +
                                                            (recursoAcessuas.FonteFundoMunicipal.HasValue && recursoAcessuas.FonteFundoMunicipal.Value
                                                            && recursoAcessuas.ValorFundoMunicipal.HasValue ? recursoAcessuas.ValorFundoMunicipal.Value : (0M))
                                                             + (programaProjetosAcessuaExercicio3.PrevisaoAnual.PrevisaoAnualRepasseExercicio3)
                                                            ).ToString("N2");
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Programas Projetos Primeira Infancia Suas

                var programa = programasProjetosFederais.SingleOrDefault(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ"));
                if (programa != null)
                {
                    var programasProjetosPrimeiraInfanciaSuas = programaProjeto.Service.GetProgramaProjetoById(programa.Id);

                    if (programasProjetosPrimeiraInfanciaSuas != null)
                    {
                        programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programasProjetosPrimeiraInfanciaSuas.Id);

                        if (programasProjetosPrimeiraInfanciaSuas.ProgramasProjetosRecursoFinanceiro != null)
                        {
                            var recursoInfanciaSuas = programasProjetosPrimeiraInfanciaSuas.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                            if (recursoInfanciaSuas != null)
                            {
                                lblSPSMunicipalExercicio3.Text = ((recursoInfanciaSuas.FonteFMAS.HasValue && recursoInfanciaSuas.FonteFMAS.Value
                                    && recursoInfanciaSuas.ValorFMAS.HasValue ? recursoInfanciaSuas.ValorFMAS.Value : (0M))
                                                           + (recursoInfanciaSuas.FonteOrcamentoMunicipal.HasValue
                                                           && recursoInfanciaSuas.FonteOrcamentoMunicipal.Value
                                                           && recursoInfanciaSuas.ValorOrcamentoMunicipal.HasValue ? recursoInfanciaSuas.ValorOrcamentoMunicipal.Value : (0M))
                                                           + (recursoInfanciaSuas.FonteFundoMunicipal.HasValue
                                                           && recursoInfanciaSuas.FonteFundoMunicipal.Value
                                                           && recursoInfanciaSuas.ValorFundoMunicipal.HasValue ? recursoInfanciaSuas.ValorFundoMunicipal.Value : (0M))
                                                           ).ToString("N2");
                                lblSPSEstadualExercicio3.Text = "0,00";

                                if (programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual != null)
                                {
                                    lblSPSFederalExercicio3.Text = programasProjetosPrimeiraInfanciaSuas.ExecutaPrograma ? programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio3.ToString("N2") : "0,00";

                                    lblSPSTotalExercicio3.Text = ((recursoInfanciaSuas.FonteFMAS.HasValue
                                        && recursoInfanciaSuas.FonteFMAS.Value
                                        && recursoInfanciaSuas.ValorFMAS.HasValue ? recursoInfanciaSuas.ValorFMAS.Value : (0M))
                                                               + (recursoInfanciaSuas.FonteOrcamentoMunicipal.HasValue
                                                               && recursoInfanciaSuas.FonteOrcamentoMunicipal.Value
                                                               && recursoInfanciaSuas.ValorOrcamentoMunicipal.HasValue ? recursoInfanciaSuas.ValorOrcamentoMunicipal.Value : (0M))
                                                               + (recursoInfanciaSuas.FonteFundoMunicipal.HasValue
                                                               && recursoInfanciaSuas.FonteFundoMunicipal.Value
                                                               && recursoInfanciaSuas.ValorFundoMunicipal.HasValue ? recursoInfanciaSuas.ValorFundoMunicipal.Value : (0M))
                                                               + (programasProjetosPrimeiraInfanciaSuas.ExecutaPrograma ? programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio3 : (0M))).ToString("N2");
                                }
                            }
                        }
                    }
                }
                #endregion

                var programasEstaduais = programaProjeto.Service
                                                        .GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                //.Where(x => x.Exercicio == exercicio);

                #region Amigo do idoso

                var programasAmigoDoIdoso = programasEstaduais.Where(t => t.Nome.ToLower().Contains("amigo do idoso") && t.ProgramaEstadual == true);

                if (programasAmigoDoIdoso != null && programasAmigoDoIdoso.Any())
                {
                    int idProgramaDoIdoso = programasAmigoDoIdoso.First().Id;

                    ProgramaProjetoInfo amigoIdoso = programaProjeto.Service.GetProgramaProjetoById(idProgramaDoIdoso);
                    if (amigoIdoso != null)
                    {
                        var parcelas = amigoIdoso.ProgramasProjetosParcelasInfo.ToList();
                        decimal SPAIEstadualExercicio3 = 0;

                        foreach (var parcela in parcelas)
                        {
                            if (amigoIdoso.ConvenioCentroDiaIdoso && parcela.AnoRepasseDiaIdoso.HasValue && parcela.AnoRepasseDiaIdoso.Value == exercicio)
                            {
                                SPAIEstadualExercicio3 += ((parcela.ValorDiaIdoso.HasValue ? parcela.ValorDiaIdoso.Value : 0M));
                            }

                            if (amigoIdoso.ConvenioCentroConvivenciaIdoso && parcela.AnoRepasseConvivenciaIdoso.HasValue && parcela.AnoRepasseConvivenciaIdoso.Value == exercicio)
                            {
                                SPAIEstadualExercicio3 += ((parcela.ValorConvivenciaIdoso.HasValue ? parcela.ValorConvivenciaIdoso.Value : 0M));
                            }

                            if (parcela.AnoRepasseDiaIdoso.HasValue && parcela.AnoRepasseDiaIdoso.Value == exercicio &&
                                parcela.AnoRepasseConvivenciaIdoso.HasValue && parcela.AnoRepasseConvivenciaIdoso.Value == exercicio)
                            {
                                lblSPAIEstadualExercicio3.Text = (
                                                          (parcela.ValorDiaIdoso.HasValue ? parcela.ValorDiaIdoso.Value : 0M)
                                                          + (parcela.ValorConvivenciaIdoso.HasValue ? parcela.ValorConvivenciaIdoso.Value : 0M)
                                                          ).ToString("N2");
                            }
                        }

                        lblSPAIEstadualExercicio3.Text = SPAIEstadualExercicio3.ToString("N2");

                        lblSPAITotalExercicio3.Text = (
                                            Convert.ToDecimal(lblSPAIMunicipalExercicio3.Text)
                                            + Convert.ToDecimal(lblSPAIEstadualExercicio3.Text)
                                            + Convert.ToDecimal(lblSPAIFederalExercicio3.Text)
                                            ).ToString("N2");
                    }
                }
                #endregion

                var transferencia = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region peti
                var acoesPeti = programaProjeto.Service.GetTransferenciaRendaById(transferencia.SingleOrDefault(t => t.Nome.ToLower().Contains("peti")).Id);
                if (acoesPeti != null)
                {
                    lblAPETIMunicipalExercicio3.Text = "0,00";

                    lblAPETIEstadualExercicio3.Text = "0,00";
                    if (acoesPeti.ValorAEPETI != null)
                    {
                        lblAPETIFederalExercicio3.Text = (!string.IsNullOrEmpty(acoesPeti.PETIAderiuCofinanciamentoFederal.HasValue.ToString()) && !string.IsNullOrEmpty(acoesPeti.PETIAderiuCofinanciamentoFederal.Value.ToString()) ? acoesPeti.ValorAEPETI.Value : 0M).ToString("N2");    
                    }
                    

                    lblAPETITotalExercicio3.Text = (Convert.ToDecimal(lblAPETIFederalExercicio3.Text)).ToString("N2");
                }
                #endregion peti

                #region programa e projetos municipais
                var programasProjetos = programaProjeto.Service.GetConsultaProgramasProjetosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                if (programasProjetos != null)
                {
                    foreach (var pm in programasProjetos)
                    {

                        var programasProjetoMunicipal = programaProjeto.Service.GetProgramaProjetoById(pm.Id);
                        if (programasProjetoMunicipal.ProgramasProjetosRecursoFinanceiro != null)
                        {
                            var programaRecursoFinanceiroExercicio = programasProjetoMunicipal.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                            if (programaRecursoFinanceiroExercicio != null)
                            {
                                lblPMMunicipalExercicio3.Text = (
                                                      Convert.ToDecimal(lblPMMunicipalExercicio3.Text)
                                                      + (programaRecursoFinanceiroExercicio.ValorFMAS.HasValue ? programaRecursoFinanceiroExercicio.ValorFMAS.Value : 0M)
                                                      + (programaRecursoFinanceiroExercicio.ValorOrcamentoMunicipal.HasValue ? programaRecursoFinanceiroExercicio.ValorOrcamentoMunicipal.Value : 0M)
                                                      + (programaRecursoFinanceiroExercicio.ValorFundoMunicipal.HasValue ? programaRecursoFinanceiroExercicio.ValorFundoMunicipal.Value : 0M)
                                                      ).ToString("N2");

                                lblPMEstadualExercicio3.Text = (
                                                     Convert.ToDecimal(lblPMEstadualExercicio3.Text)
                                                     + (programaRecursoFinanceiroExercicio.ValorFEAS.HasValue ? programaRecursoFinanceiroExercicio.ValorFEAS.Value : 0M)
                                                     + (programaRecursoFinanceiroExercicio.ValorOrcamentoEstadual.HasValue ? programaRecursoFinanceiroExercicio.ValorOrcamentoEstadual.Value : 0M)
                                                     + (programaRecursoFinanceiroExercicio.ValorFundoEstadual.HasValue ? programaRecursoFinanceiroExercicio.ValorFundoEstadual.Value : 0M)
                                                     ).ToString("N2");

                                lblPMFederalExercicio3.Text = (
                                                    Convert.ToDecimal(lblPMFederalExercicio3.Text)
                                                    + (programaRecursoFinanceiroExercicio.ValorFNAS.HasValue ? programaRecursoFinanceiroExercicio.ValorFNAS.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorOrcamentoFederal.HasValue ? programaRecursoFinanceiroExercicio.ValorOrcamentoFederal.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorFundoFederal.HasValue ? programaRecursoFinanceiroExercicio.ValorFundoFederal.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorIGDPBF.HasValue ? programaRecursoFinanceiroExercicio.ValorIGDPBF.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorIGDSUAS.HasValue ? programaRecursoFinanceiroExercicio.ValorIGDSUAS.Value : 0M)
                                                    ).ToString("N2");
                            }
                        }

                        lblPMTotalExercicio3.Text = (
                                          Convert.ToDecimal(lblPMMunicipalExercicio3.Text)
                                          + Convert.ToDecimal(lblPMEstadualExercicio3.Text)
                                          + Convert.ToDecimal(lblPMFederalExercicio3.Text)
                                          ).ToString("N2");
                        if (pm.TipoProgramaTransferencia == 2)
                        {

                            var metaPactuadaExercicio3 = programasProjetoMunicipal.PrevisaoAnual.MetaPactuadaExercicio3.ToString();
                            var previsaoAnualExercicio3 = programasProjetoMunicipal.PrevisaoAnual.PrevisaoAnualRepasseExercicio3.ToString("N2");

                            programasProjetoMunicipal.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pm.Id);
                            if (programasProjetoMunicipal.PrevisaoAnual != null)
                            {
                                lblProgramaMunicipalExercicio3.Text = (Convert.ToDecimal(lblProgramaMunicipalExercicio3.Text) + Convert.ToDecimal(programasProjetoMunicipal.PrevisaoAnual.PrevisaoAnualMunicipalExercicio3)).ToString("N2");
                                lblProgramaMunicipalTotalExercicio3.Text = Convert.ToDecimal(lblProgramaMunicipalExercicio3.Text).ToString("N2");
                            }
                        }
                    }
                }
                #endregion programa e projetos municipais

                var transferenciaRendaProgramas = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region ProsperaFamilia
                var prospera = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 10).Id);

                if (prospera != null)
                {
                    lblPFEstadualExercicio3.Text = prospera.ValorRepasseEstadual2024.HasValue ? prospera.ValorRepasseEstadual2024.Value.ToString("N2") : "0,00";
                    lblPFTotalExercicio3.Text = lblPFEstadualExercicio3.Text;
                }

                #endregion

                #region FortalecimentoCadUnico
                var fortalecimento = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 11).Id);

                if (fortalecimento != null)
                {
                    lblFCEstadualExercicio3.Text = fortalecimento.ValorRepasseEstadual2024.HasValue ? fortalecimento.ValorRepasseEstadual2024.Value.ToString("N2") : "0,00";
                    lblFCTotalExercicio3.Text = lblFCEstadualExercicio3.Text;
                }

                #endregion

                #region FortalecimentoDaVigilânciaSocioassistencial
                var fortalecimentoVigilancia = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 12).Id);

                if (fortalecimentoVigilancia != null)
                {
                    lblFVEstadualExercicio3.Text = fortalecimentoVigilancia.ValorRepasseEstadual2024.HasValue ? fortalecimentoVigilancia.ValorRepasseEstadual2024.Value.ToString("N2") : "0,00";
                    lblFVTotalExercicio3.Text = lblFVEstadualExercicio3.Text;
                }

                #endregion

                #region totais

                lblProgDesenvTotalMunicipalExercicio3.Text = (
                                                   Convert.ToDecimal(lblACESSUASMunicipalExercicio3.Text)
                                                   + Convert.ToDecimal(lblSPSMunicipalExercicio3.Text)
                                                   + Convert.ToDecimal(lblSPAIMunicipalExercicio3.Text)
                                                   + Convert.ToDecimal(lblPMMunicipalExercicio3.Text)
                                                   + Convert.ToDecimal(lblAPETIMunicipalExercicio3.Text)
                                                   ).ToString("N2");

                lblProgDesenvTotalEstadualExercicio3.Text = (
                                                  Convert.ToDecimal(lblACESSUASEstadualExercicio3.Text)
                                                  + Convert.ToDecimal(lblSPSEstadualExercicio3.Text)
                                                  + Convert.ToDecimal(lblSPAIEstadualExercicio3.Text)
                                                  + Convert.ToDecimal(lblPMEstadualExercicio3.Text)
                                                  + Convert.ToDecimal(lblAPETIEstadualExercicio3.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblFCEstadualExercicio3.Text) ? "0" : lblFCEstadualExercicio3.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblFVEstadualExercicio3.Text) ? "0" : lblFVEstadualExercicio3.Text)
                                                  ).ToString("N2");

                lblProgDesenvTotalFederalExercicio3.Text = (
                                                 Convert.ToDecimal(lblACESSUASFederalExercicio3.Text)
                                                 + Convert.ToDecimal(lblSPSFederalExercicio3.Text)
                                                 + Convert.ToDecimal(lblAPETIFederalExercicio3.Text)
                                                 + Convert.ToDecimal(lblSPAIFederalExercicio3.Text)
                                                 + Convert.ToDecimal(lblPMFederalExercicio3.Text)
                                                 ).ToString("N2");

                lblProgDesenvTotalGeralExercicio3.Text = (
                                               Convert.ToDecimal(lblACESSUASTotalExercicio3.Text)
                                               + Convert.ToDecimal(lblSPSTotalExercicio3.Text)
                                               + Convert.ToDecimal(lblSPAITotalExercicio3.Text)
                                               + Convert.ToDecimal(lblPMTotalExercicio3.Text)
                                               + Convert.ToDecimal(lblAPETITotalExercicio3.Text)
                                               + Convert.ToDecimal(lblFCTotalExercicio3.Text)
                                               + Convert.ToDecimal(lblFCEstadualExercicio3.Text)
                                               + Convert.ToDecimal(lblFVEstadualExercicio3.Text)
                                               ).ToString("N2");
                #endregion totais

            }




            #region programas

            #region Beneficio Eventual
            //var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(x => x.Exercicio == exercicio).FirstOrDefault();


            if (beneficio != null)
            {
                lblBEvEstadualExercicio3.Text = beneficio.ValorAnualEstadual.ToString("N2");
                lblBEvMunicipalExercicio3.Text = beneficio.ValorAnualMunicipal.ToString("N2");
                lblBEvFederalExercicio3.Text = beneficio.ValorAnualFederal.ToString("N2");
                lblBEvTotalExercicio3.Text = (beneficio.ValorAnualEstadual + beneficio.ValorAnualFederal + beneficio.ValorAnualMunicipal + beneficio.ValorAnualPrivado).ToString("N2");
            }

            #endregion

            var transferenciaRenda = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            using (var programaProjeto = new ProxyProgramas())
            {
                #region Acao Jovem
                var acao = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 5).Id);
                if (acao != null)
                {
                    lblAcaoJovemEstadualExercicio3.Text = acao.CalculoAcaoRendaPrevisaoAnualExercicio3.HasValue ? acao.CalculoAcaoRendaPrevisaoAnualExercicio3.Value.ToString("N2") : "0,00";
                    lblAcaoJovemTotalExercicio3.Text = acao.CalculoAcaoRendaPrevisaoAnualExercicio3.HasValue ? acao.CalculoAcaoRendaPrevisaoAnualExercicio3.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region Renda Cidadã
                //Renda Cidadã
                var renda = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 6).Id);
                if (renda != null)
                {
                    lblRendaCidadaEstadualExercicio3.Text = renda.CalculoAcaoRendaPrevisaoAnualExercicio3.HasValue ? renda.CalculoAcaoRendaPrevisaoAnualExercicio3.Value.ToString("N2") : "0,00";
                    lblRendaCidadaTotalExercicio3.Text = renda.CalculoAcaoRendaPrevisaoAnualExercicio3.HasValue ? renda.CalculoAcaoRendaPrevisaoAnualExercicio3.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region BPC Idosos

                var idoso = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 1).Id);
                if (idoso != null)
                {
                    lblBPCIdososFederalExercicio3.Text = idoso.CalculoBPCPrevisaoAnualExercicio3.HasValue ? idoso.CalculoBPCPrevisaoAnualExercicio3.Value.ToString("N2") : "0,00";
                    lblBPCIdososTotalExercicio3.Text = idoso.CalculoBPCPrevisaoAnualExercicio3.HasValue ? idoso.CalculoBPCPrevisaoAnualExercicio3.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region BPC - PCD [Pessoa com deficiência]
                var pcd = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 2).Id);
                if (pcd != null)
                {
                    lblBPCPCDFederalExercicio3.Text = pcd.CalculoBPCPrevisaoAnualExercicio3.HasValue ? pcd.CalculoBPCPrevisaoAnualExercicio3.Value.ToString("N2") : "0,00";
                    lblBPCPCDTotalExercicio3.Text = pcd.CalculoBPCPrevisaoAnualExercicio3.HasValue ? pcd.CalculoBPCPrevisaoAnualExercicio3.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region Renda cidadã - Benificio Idoso
                var pIdoso = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                if (pIdoso != null && pIdoso.Any())
                {
                    var pIdosoSingle = pIdoso.Where(t => t.Nome.ToLower().Contains("amigo do idoso")).FirstOrDefault();
                    if (pIdosoSingle != null && pIdosoSingle != null)
                    {
                        int idPIdoso = pIdosoSingle.Id;

                        var ppIdoso = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(idPIdoso);
                        if (ppIdoso != null)
                        {
                            lblRendaCidadaBeneficioIdosoEstadualExercicio3.Text = (ppIdoso.MetaPactuadaExercicio3 * 100M * 12).ToString("N2");
                            lblRendaCidadaBeneficioIdosoTotalExercicio3.Text = lblRendaCidadaBeneficioIdosoEstadualExercicio3.Text;
                        }
                    }
                }
                #endregion

                #region Bolsa familia
                //Bolsa Família


                if (transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 3) != null)
                {
                    var bolsa = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 3).Id);
                    if (bolsa != null)
                    {
                        lblBolsaFamiliaFederalExercicio3.Text = bolsa.RepasseMensal2023.HasValue ? (bolsa.RepasseMensal2023.Value * 12).ToString("N2") : "0,00";
                        lblBolsaFamiliaTotalExercicio3.Text = bolsa.RepasseMensal2023.HasValue ? (bolsa.RepasseMensal2023.Value * 12).ToString("N2") : "0,00";
                    }
                }
                #endregion
            }
            #endregion

            #region PETI

            var peti = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 4);

            lblProgramaTotalMunicipalExercicio3.Text = (Convert.ToDecimal(lblBEvMunicipalExercicio3.Text)
                                            + Convert.ToDecimal(lblBPCIdososMunicipalExercicio3.Text)
                                            + Convert.ToDecimal(lblBPCPCDMunicipalExercicio3.Text)
                                            + Convert.ToDecimal(lblAcaoJovemMunicipalExercicio3.Text)
                                            + Convert.ToDecimal(lblRendaCidadaMunicipalExercicio3.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaMunicipalExercicio3.Text)
                                            + Convert.ToDecimal(lblProgramaMunicipalExercicio3.Text)).ToString("n2");

            lblProgramaTotalEstadualExercicio3.Text = (Convert.ToDecimal(lblBEvEstadualExercicio3.Text)
                                            + Convert.ToDecimal(lblBPCIdososEstadualExercicio3.Text)
                                            + Convert.ToDecimal(lblBPCPCDEstadualExercicio3.Text)
                                            + Convert.ToDecimal(lblAcaoJovemEstadualExercicio3.Text)
                                            + Convert.ToDecimal(lblRendaCidadaEstadualExercicio3.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoEstadualExercicio3.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaEstadualExercicio3.Text)
                                            + Convert.ToDecimal(lblProgramaEstadualExercicio3.Text)
                                            + Convert.ToDecimal(lblPFEstadualExercicio3.Text)
                                            ).ToString("n2");

            lblProgramaTotalFederalExercicio3.Text = (Convert.ToDecimal(lblBEvFederalExercicio3.Text)
                                            + Convert.ToDecimal(lblBPCIdososFederalExercicio3.Text)
                                            + Convert.ToDecimal(lblBPCPCDFederalExercicio3.Text)
                                            + Convert.ToDecimal(lblAcaoJovemFederalExercicio3.Text)
                                            + Convert.ToDecimal(lblRendaCidadaFederalExercicio3.Text)
                                            + (String.IsNullOrEmpty(lblBolsaFamiliaFederalExercicio3.Text) ? 0M : Convert.ToDecimal(lblBolsaFamiliaFederalExercicio3.Text))
                                            + Convert.ToDecimal(lblProgramaFederalExercicio3.Text)).ToString("n2");

            lblProgramaTotalGeralExercicio3.Text = (Convert.ToDecimal(lblBEvTotalExercicio3.Text)
                                            + Convert.ToDecimal(lblBPCIdososTotalExercicio3.Text)
                                            + Convert.ToDecimal(lblBPCPCDTotalExercicio3.Text)
                                            + Convert.ToDecimal(lblAcaoJovemTotalExercicio3.Text)
                                            + Convert.ToDecimal(lblRendaCidadaTotalExercicio3.Text)
                                            + Convert.ToDecimal(lblPFTotalExercicio3.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoTotalExercicio3.Text)
                                            + (String.IsNullOrEmpty(lblBolsaFamiliaTotalExercicio3.Text) ? 0M : Convert.ToDecimal(lblBolsaFamiliaTotalExercicio3.Text))
                                            + Convert.ToDecimal(lblProgramaMunicipalTotalExercicio3.Text)).ToString("n2");
            #endregion

            #region Indices [IGD, IGDPBF, IGDSUAS]
            var indice = prefeituras.GetIndiceGestaoDescentralizada(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
            if (indice != null)
            {
                var IGD = 0m;
                if (indice.IGDPBFValorAnual.HasValue)
                    IGD += indice.IGDPBFValorAnual.Value;
                if (indice.IGDSUASValorAnual.HasValue)
                    IGD += indice.IGDSUASValorAnual.Value;
            }
            #endregion

        }

        void carregarProgramasExercicio4(Prefeituras prefeituras)
        {
            int exercicio = FPrevisaoOrcamentaria.Exercicios[3];
            //Programas desenvolvidos no município
            using (var programaProjeto = new ProxyProgramas())
            {
                var programasProjetosFederais = programaProjeto.Service
                                                               .GetConsultaProgramasProjetosFederaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);

                #region Programas Projetos Acessuas

                var programasProjetosAcessuasExercicio4 = programasProjetosFederais.Where(t => t.Nome.ToLower().Contains("acessuas"));
                if (programasProjetosAcessuasExercicio4 != null)
                {
                    var programaProjetoExercicio4 = programasProjetosAcessuasExercicio4.Single();
                    var programaProjetosAcessuaExercicio4 = programaProjeto.Service.GetProgramaProjetoById(programaProjetoExercicio4.Id);

                    if (programaProjetosAcessuaExercicio4 != null)
                    {
                        programaProjetosAcessuaExercicio4.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programaProjetosAcessuaExercicio4.Id);

                        if (programasProjetosAcessuasExercicio4 != null)
                        {
                            var recursoAcessuas = programaProjetosAcessuaExercicio4.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();

                            if (recursoAcessuas != null)
                            {
                                lblACESSUASMunicipalExercicio4.Text = ((recursoAcessuas.FonteFMAS.HasValue && recursoAcessuas.FonteFMAS.Value
                                    && recursoAcessuas.ValorFMAS.HasValue ? recursoAcessuas.ValorFMAS.Value : (0M))
                                                            + (recursoAcessuas.FonteOrcamentoMunicipal.HasValue
                                                            && recursoAcessuas.FonteOrcamentoMunicipal.Value && recursoAcessuas.ValorOrcamentoMunicipal.HasValue
                                                            ? recursoAcessuas.ValorOrcamentoMunicipal.Value : (0M))
                                                            + (recursoAcessuas.FonteFundoMunicipal.HasValue && recursoAcessuas.FonteFundoMunicipal.Value
                                                            && recursoAcessuas.ValorFundoMunicipal.HasValue ? recursoAcessuas.ValorFundoMunicipal.Value : (0M))
                                                            ).ToString("N2");

                                if (programaProjetosAcessuaExercicio4.PrevisaoAnual != null)
                                {
                                    lblACESSUASFederalExercicio4.Text = (programaProjetosAcessuaExercicio4.PrevisaoAnual.PrevisaoAnualRepasseExercicio4).ToString("N2");
                                    lblACESSUASTotalExercicio4.Text = (
                                                            (recursoAcessuas.FonteFMAS.HasValue && recursoAcessuas.FonteFMAS.Value
                                                            && recursoAcessuas.ValorFMAS.HasValue ? recursoAcessuas.ValorFMAS.Value : (0M)) +
                                                            (recursoAcessuas.FonteOrcamentoMunicipal.HasValue && recursoAcessuas.FonteOrcamentoMunicipal.Value
                                                            && recursoAcessuas.ValorOrcamentoMunicipal.HasValue ? recursoAcessuas.ValorOrcamentoMunicipal.Value : (0M)) +
                                                            (recursoAcessuas.FonteFundoMunicipal.HasValue && recursoAcessuas.FonteFundoMunicipal.Value
                                                            && recursoAcessuas.ValorFundoMunicipal.HasValue ? recursoAcessuas.ValorFundoMunicipal.Value : (0M))
                                                             + (programaProjetosAcessuaExercicio4.PrevisaoAnual.PrevisaoAnualRepasseExercicio4)
                                                            ).ToString("N2");
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Programas Projetos Primeira Infancia Suas

                var programa = programasProjetosFederais.SingleOrDefault(t => t.Nome.Contains("PROGRAMA CRIANÇA FELIZ"));
                if (programa != null)
                {
                    var programasProjetosPrimeiraInfanciaSuas = programaProjeto.Service.GetProgramaProjetoById(programa.Id);

                    if (programasProjetosPrimeiraInfanciaSuas != null)
                    {
                        programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(programasProjetosPrimeiraInfanciaSuas.Id);

                        if (programasProjetosPrimeiraInfanciaSuas.ProgramasProjetosRecursoFinanceiro != null)
                        {
                            var recursoInfanciaSuas = programasProjetosPrimeiraInfanciaSuas.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                            if (recursoInfanciaSuas != null)
                            {
                                lblSPSMunicipalExercicio4.Text = ((recursoInfanciaSuas.FonteFMAS.HasValue && recursoInfanciaSuas.FonteFMAS.Value
                                    && recursoInfanciaSuas.ValorFMAS.HasValue ? recursoInfanciaSuas.ValorFMAS.Value : (0M))
                                                           + (recursoInfanciaSuas.FonteOrcamentoMunicipal.HasValue
                                                           && recursoInfanciaSuas.FonteOrcamentoMunicipal.Value
                                                           && recursoInfanciaSuas.ValorOrcamentoMunicipal.HasValue ? recursoInfanciaSuas.ValorOrcamentoMunicipal.Value : (0M))
                                                           + (recursoInfanciaSuas.FonteFundoMunicipal.HasValue
                                                           && recursoInfanciaSuas.FonteFundoMunicipal.Value
                                                           && recursoInfanciaSuas.ValorFundoMunicipal.HasValue ? recursoInfanciaSuas.ValorFundoMunicipal.Value : (0M))
                                                           ).ToString("N2");
                                lblSPSEstadualExercicio4.Text = "0,00";

                                if (programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual != null)
                                {
                                    lblSPSFederalExercicio4.Text = programasProjetosPrimeiraInfanciaSuas.ExecutaPrograma ? programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio4.ToString("N2") : "0,00";

                                    lblSPSTotalExercicio4.Text = ((recursoInfanciaSuas.FonteFMAS.HasValue
                                        && recursoInfanciaSuas.FonteFMAS.Value
                                        && recursoInfanciaSuas.ValorFMAS.HasValue ? recursoInfanciaSuas.ValorFMAS.Value : (0M))
                                                               + (recursoInfanciaSuas.FonteOrcamentoMunicipal.HasValue
                                                               && recursoInfanciaSuas.FonteOrcamentoMunicipal.Value
                                                               && recursoInfanciaSuas.ValorOrcamentoMunicipal.HasValue ? recursoInfanciaSuas.ValorOrcamentoMunicipal.Value : (0M))
                                                               + (recursoInfanciaSuas.FonteFundoMunicipal.HasValue
                                                               && recursoInfanciaSuas.FonteFundoMunicipal.Value
                                                               && recursoInfanciaSuas.ValorFundoMunicipal.HasValue ? recursoInfanciaSuas.ValorFundoMunicipal.Value : (0M))
                                                               + (programasProjetosPrimeiraInfanciaSuas.ExecutaPrograma ? programasProjetosPrimeiraInfanciaSuas.PrevisaoAnual.PrevisaoAnualRepasseExercicio4 : (0M))).ToString("N2");
                                }
                            }
                        }
                    }
                }
                #endregion

                var programasEstaduais = programaProjeto.Service
                                                        .GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                //.Where(x => x.Exercicio == exercicio);

                #region Amigo do idoso

                var programasAmigoDoIdoso = programasEstaduais.Where(t => t.Nome.ToLower().Contains("amigo do idoso") && t.ProgramaEstadual == true);

                if (programasAmigoDoIdoso != null && programasAmigoDoIdoso.Any())
                {
                    int idProgramaDoIdoso = programasAmigoDoIdoso.First().Id;

                    ProgramaProjetoInfo amigoIdoso = programaProjeto.Service.GetProgramaProjetoById(idProgramaDoIdoso);

                    

                    if (amigoIdoso != null)
                    {
                        var parcelas = amigoIdoso.ProgramasProjetosParcelasInfo.ToList();

                        decimal SPAIEstadualExercicio4 = 0;    
                        
                        foreach (var parcela in parcelas)
                        {
                            if (amigoIdoso.ConvenioCentroDiaIdoso && parcela.AnoRepasseDiaIdoso.HasValue && parcela.AnoRepasseDiaIdoso.Value == exercicio)
                            {
                                SPAIEstadualExercicio4 += ((parcela.ValorDiaIdoso.HasValue ? parcela.ValorDiaIdoso.Value : 0M));
                            }

                            if (amigoIdoso.ConvenioCentroConvivenciaIdoso && parcela.AnoRepasseConvivenciaIdoso.HasValue && parcela.AnoRepasseConvivenciaIdoso.Value == exercicio)
                            {
                                SPAIEstadualExercicio4 += ((parcela.ValorConvivenciaIdoso.HasValue ? parcela.ValorConvivenciaIdoso.Value : 0M));
                            }

                            if (parcela.AnoRepasseDiaIdoso.HasValue && parcela.AnoRepasseDiaIdoso.Value == exercicio &&
                                parcela.AnoRepasseConvivenciaIdoso.HasValue && parcela.AnoRepasseConvivenciaIdoso.Value == exercicio)
                            {
                                lblSPAIEstadualExercicio4.Text = (
                                                          (parcela.ValorDiaIdoso.HasValue ? parcela.ValorDiaIdoso.Value : 0M)
                                                          + (parcela.ValorConvivenciaIdoso.HasValue ? parcela.ValorConvivenciaIdoso.Value : 0M)
                                                          ).ToString("N2");
                            }
                        }


                        lblSPAIEstadualExercicio4.Text = SPAIEstadualExercicio4.ToString("N2");


                        lblSPAITotalExercicio4.Text = (
                                            Convert.ToDecimal(lblSPAIMunicipalExercicio4.Text)
                                            + Convert.ToDecimal(lblSPAIEstadualExercicio4.Text)
                                            + Convert.ToDecimal(lblSPAIFederalExercicio4.Text)
                                            ).ToString("N2");
                    }
                }
                #endregion

                var transferencia = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region peti
                var acoesPeti = programaProjeto.Service.GetTransferenciaRendaById(transferencia.SingleOrDefault(t => t.Nome.ToLower().Contains("peti")).Id);
                if (acoesPeti != null)
                {
                    lblAPETIMunicipalExercicio4.Text = "0,00";

                    lblAPETIEstadualExercicio4.Text = "0,00";
                   
                    if (acoesPeti.ValorAEPETI2 != null)
                    {
                        lblAPETIFederalExercicio4.Text = (acoesPeti.PETIAderiuCofinanciamentoFederal.HasValue && acoesPeti.PETIAderiuCofinanciamentoFederal.Value ? acoesPeti.ValorAEPETI2.Value : 0M).ToString("N2");    
                    }
                   
                    lblAPETITotalExercicio4.Text = (Convert.ToDecimal(lblAPETIFederalExercicio4.Text)).ToString("N2");
                }
                #endregion peti

                #region programa e projetos municipais
                var programasProjetos = programaProjeto.Service.GetConsultaProgramasProjetosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                if (programasProjetos != null)
                {
                    foreach (var pm in programasProjetos)
                    {

                        var programasProjetoMunicipal = programaProjeto.Service.GetProgramaProjetoById(pm.Id);
                        if (programasProjetoMunicipal.ProgramasProjetosRecursoFinanceiro != null)
                        {
                            var programaRecursoFinanceiroExercicio = programasProjetoMunicipal.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                            if (programaRecursoFinanceiroExercicio != null)
                            {
                                lblPMMunicipalExercicio4.Text = (
                                                      Convert.ToDecimal(lblPMMunicipalExercicio4.Text)
                                                      + (programaRecursoFinanceiroExercicio.ValorFMAS.HasValue ? programaRecursoFinanceiroExercicio.ValorFMAS.Value : 0M)
                                                      + (programaRecursoFinanceiroExercicio.ValorOrcamentoMunicipal.HasValue ? programaRecursoFinanceiroExercicio.ValorOrcamentoMunicipal.Value : 0M)
                                                      + (programaRecursoFinanceiroExercicio.ValorFundoMunicipal.HasValue ? programaRecursoFinanceiroExercicio.ValorFundoMunicipal.Value : 0M)
                                                      ).ToString("N2");

                                lblPMEstadualExercicio4.Text = (
                                                     Convert.ToDecimal(lblPMEstadualExercicio4.Text)
                                                     + (programaRecursoFinanceiroExercicio.ValorFEAS.HasValue ? programaRecursoFinanceiroExercicio.ValorFEAS.Value : 0M)
                                                     + (programaRecursoFinanceiroExercicio.ValorOrcamentoEstadual.HasValue ? programaRecursoFinanceiroExercicio.ValorOrcamentoEstadual.Value : 0M)
                                                     + (programaRecursoFinanceiroExercicio.ValorFundoEstadual.HasValue ? programaRecursoFinanceiroExercicio.ValorFundoEstadual.Value : 0M)
                                                     ).ToString("N2");

                                lblPMFederalExercicio4.Text = (
                                                    Convert.ToDecimal(lblPMFederalExercicio4.Text)
                                                    + (programaRecursoFinanceiroExercicio.ValorFNAS.HasValue ? programaRecursoFinanceiroExercicio.ValorFNAS.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorOrcamentoFederal.HasValue ? programaRecursoFinanceiroExercicio.ValorOrcamentoFederal.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorFundoFederal.HasValue ? programaRecursoFinanceiroExercicio.ValorFundoFederal.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorIGDPBF.HasValue ? programaRecursoFinanceiroExercicio.ValorIGDPBF.Value : 0M)
                                                    + (programaRecursoFinanceiroExercicio.ValorIGDSUAS.HasValue ? programaRecursoFinanceiroExercicio.ValorIGDSUAS.Value : 0M)
                                                    ).ToString("N2");
                            }
                        }

                        lblPMTotalExercicio4.Text = (
                                          Convert.ToDecimal(lblPMMunicipalExercicio4.Text)
                                          + Convert.ToDecimal(lblPMEstadualExercicio4.Text)
                                          + Convert.ToDecimal(lblPMFederalExercicio4.Text)
                                          ).ToString("N2");
                        if (pm.TipoProgramaTransferencia == 2)
                        {

                            var metaPactuadaExercicio4 = programasProjetoMunicipal.PrevisaoAnual.MetaPactuadaExercicio4.ToString();
                            var previsaoAnualExercicio4 = programasProjetoMunicipal.PrevisaoAnual.PrevisaoAnualRepasseExercicio4.ToString("N2");

                            programasProjetoMunicipal.PrevisaoAnual = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(pm.Id);
                            if (programasProjetoMunicipal.PrevisaoAnual != null)
                            {
                                decimal programaMunicipal = Convert.ToDecimal(lblProgramaMunicipal.Text) + Convert.ToDecimal(programasProjetoMunicipal.PrevisaoAnual.PrevisaoAnualMunicipalExercicio4);
                                decimal previsaoAnual = Convert.ToDecimal(lblProgramaMunicipal.Text);
         
                                transferenciaRendaMunicipal += programaMunicipal;

                                lblProgramaMunicipalExercicio4.Text = (transferenciaRendaMunicipal).ToString("N2");
                                lblProgramaMunicipalTotalExercicio4.Text = Convert.ToDecimal(lblProgramaMunicipal.Text).ToString("N2");
                            }
                        }
                    }
                }
                #endregion programa e projetos municipais

                var transferenciaRendaProgramas = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                #region ProsperaFamilia
                var prospera = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 10).Id);

                if (prospera != null)
                {
                    lblPFEstadualExercicio4.Text = prospera.ValorRepasseEstadual2025.HasValue ? prospera.ValorRepasseEstadual2025.Value.ToString("N2") : "0,00";
                }

                #endregion

                #region FortalecimentoCadUnico
                var fortalecimento = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 11).Id);

                if (fortalecimento != null)
                {
                    lblFCEstadualExercicio4.Text = fortalecimento.ValorRepasseEstadual2025.HasValue ? fortalecimento.ValorRepasseEstadual2025.Value.ToString("N2") : "0,00";
                }

                #endregion

                #region FortalecimentoDaVigilânciaSocioassistencial
                var fortalecimentoVigilancia = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRendaProgramas.FirstOrDefault(t => t.TipoTransferencia == 11).Id);

                if (fortalecimentoVigilancia != null)
                {
                    lblFVEstadualExercicio4.Text = fortalecimentoVigilancia.ValorRepasseEstadual2025.HasValue ? fortalecimentoVigilancia.ValorRepasseEstadual2025.Value.ToString("N2") : "0,00";
                    lblFVTotalExercicio4.Text = lblFVEstadualExercicio4.Text;
                }

                #endregion

                #region totais

                lblProgDesenvTotalMunicipalExercicio4.Text = (
                                                   Convert.ToDecimal(lblACESSUASMunicipalExercicio4.Text)
                                                   + Convert.ToDecimal(lblSPSMunicipalExercicio4.Text)
                                                   + Convert.ToDecimal(lblSPAIMunicipalExercicio4.Text)
                                                   + Convert.ToDecimal(lblPMMunicipalExercicio4.Text)
                                                   + Convert.ToDecimal(lblAPETIMunicipalExercicio4.Text)
                                                   ).ToString("N2");

                lblProgDesenvTotalEstadualExercicio4.Text = (
                                                  Convert.ToDecimal(lblACESSUASEstadualExercicio4.Text)
                                                  + Convert.ToDecimal(lblSPSEstadualExercicio4.Text)
                                                  + Convert.ToDecimal(lblSPAIEstadualExercicio4.Text)
                                                  + Convert.ToDecimal(lblPMEstadualExercicio4.Text)
                                                  + Convert.ToDecimal(lblAPETIEstadualExercicio4.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblPFEstadualExercicio4.Text) ? "0" : lblPFEstadualExercicio4.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblFCEstadualExercicio4.Text) ? "0" : lblFCEstadualExercicio4.Text)
                                                  + Convert.ToDecimal(String.IsNullOrEmpty(lblFVEstadualExercicio4.Text) ? "0" : lblFVEstadualExercicio4.Text)
                                                  ).ToString("N2");

                lblProgDesenvTotalFederalExercicio4.Text = (
                                                 Convert.ToDecimal(lblACESSUASFederalExercicio4.Text)
                                                 + Convert.ToDecimal(lblSPSFederalExercicio4.Text)
                                                 + Convert.ToDecimal(lblAPETIFederalExercicio4.Text)
                                                 + Convert.ToDecimal(lblSPAIFederalExercicio4.Text)
                                                 + Convert.ToDecimal(lblPMFederalExercicio4.Text)
                                                 ).ToString("N2");

                lblProgDesenvTotalGeralExercicio4.Text = (
                                               Convert.ToDecimal(lblACESSUASTotalExercicio4.Text)
                                               + Convert.ToDecimal(lblSPSTotalExercicio4.Text)
                                               + Convert.ToDecimal(lblSPAITotalExercicio4.Text)
                                               + Convert.ToDecimal(lblPMTotalExercicio4.Text)
                                               + Convert.ToDecimal(lblAPETITotalExercicio4.Text)
                                               + Convert.ToDecimal(lblFCEstadualExercicio4.Text)
                                               + Convert.ToDecimal(lblFVEstadualExercicio4.Text)
                                               ).ToString("N2");
                #endregion totais

            }




            #region programas

            #region Beneficio Eventual
            //var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(x => x.Exercicio == exercicio).FirstOrDefault();


            if (beneficio != null)
            {
                lblBEvEstadualExercicio4.Text = beneficio.ValorAnualEstadual.ToString("N2");
                lblBEvMunicipalExercicio4.Text = beneficio.ValorAnualMunicipal.ToString("N2");
                lblBEvFederalExercicio4.Text = beneficio.ValorAnualFederal.ToString("N2");
                lblBEvTotalExercicio4.Text = (beneficio.ValorAnualEstadual + beneficio.ValorAnualFederal + beneficio.ValorAnualMunicipal + beneficio.ValorAnualPrivado).ToString("N2");
            }

            #endregion

            var transferenciaRenda = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            using (var programaProjeto = new ProxyProgramas())
            {
                #region Acao Jovem
                var acao = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 5).Id);
                if (acao != null)
                {
                    lblAcaoJovemEstadualExercicio4.Text = acao.CalculoAcaoRendaPrevisaoAnualExercicio4.HasValue ? acao.CalculoAcaoRendaPrevisaoAnualExercicio4.Value.ToString("N2") : "0,00";
                    lblAcaoJovemTotalExercicio4.Text = acao.CalculoAcaoRendaPrevisaoAnualExercicio4.HasValue ? acao.CalculoAcaoRendaPrevisaoAnualExercicio4.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region Renda Cidadã
                //Renda Cidadã
                var renda = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 6).Id);
                if (renda != null)
                {
                    lblRendaCidadaEstadualExercicio4.Text = renda.CalculoAcaoRendaPrevisaoAnualExercicio4.HasValue ? renda.CalculoAcaoRendaPrevisaoAnualExercicio4.Value.ToString("N2") : "0,00";
                    lblRendaCidadaTotalExercicio4.Text = renda.CalculoAcaoRendaPrevisaoAnualExercicio4.HasValue ? renda.CalculoAcaoRendaPrevisaoAnualExercicio4.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region BPC Idosos

                var idoso = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 1).Id);
                if (idoso != null)
                {
                    lblBPCIdososFederalExercicio4.Text = idoso.CalculoBPCPrevisaoAnualExercicio4.HasValue ? idoso.CalculoBPCPrevisaoAnualExercicio4.Value.ToString("N2") : "0,00";
                    lblBPCIdososTotalExercicio4.Text = idoso.CalculoBPCPrevisaoAnualExercicio4.HasValue ? idoso.CalculoBPCPrevisaoAnualExercicio4.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region BPC - PCD [Pessoa com deficiência]
                var pcd = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 2).Id);
                if (pcd != null)
                {
                    lblBPCPCDFederalExercicio4.Text = pcd.CalculoBPCPrevisaoAnualExercicio4.HasValue ? pcd.CalculoBPCPrevisaoAnualExercicio4.Value.ToString("N2") : "0,00";
                    lblBPCPCDTotalExercicio4.Text = pcd.CalculoBPCPrevisaoAnualExercicio4.HasValue ? pcd.CalculoBPCPrevisaoAnualExercicio4.Value.ToString("N2") : "0,00";
                }
                #endregion

                #region Renda cidadã - Benificio Idoso
                var pIdoso = programaProjeto.Service.GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                if (pIdoso != null && pIdoso.Any())
                {
                    var pIdosoSingle = pIdoso.Where(t => t.Nome.ToLower().Contains("amigo do idoso")).FirstOrDefault();
                    if (pIdosoSingle != null && pIdosoSingle != null)
                    {
                        int idPIdoso = pIdosoSingle.Id;

                        var ppIdoso = programaProjeto.Service.GetPrevisaoAnualByProgramaProjeto(idPIdoso);
                        if (ppIdoso != null)
                        {
                            lblRendaCidadaBeneficioIdosoEstadualExercicio4.Text = (ppIdoso.MetaPactuadaExercicio4 * 100M * 12).ToString("N2");
                            lblRendaCidadaBeneficioIdosoTotalExercicio4.Text = lblRendaCidadaBeneficioIdosoEstadualExercicio4.Text;
                        }
                    }
                }
                #endregion

                #region Bolsa familia
                //Bolsa Família


                if (transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 3) != null)
                {
                    var bolsa = programaProjeto.Service.GetPrevisaoAnualByTransferenciaRenda(transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 3).Id);
                    if (bolsa != null)
                    {
                        lblBolsaFamiliaFederalExercicio4.Text = bolsa.RepasseMensal2024.HasValue ? (bolsa.RepasseMensal2024.Value * 12).ToString("N2") : "0,00";
                        lblBolsaFamiliaTotalExercicio4.Text = bolsa.RepasseMensal2024.HasValue ? (bolsa.RepasseMensal2024.Value * 12).ToString("N2") : "0,00";
                    }
                }
                #endregion
                
            }
            #endregion

            #region PETI

            var peti = transferenciaRenda.FirstOrDefault(t => t.TipoTransferencia == 4);

            lblProgramaTotalMunicipalExercicio4.Text = (Convert.ToDecimal(lblBEvMunicipalExercicio4.Text)
                                            + Convert.ToDecimal(lblBPCIdososMunicipalExercicio4.Text)
                                            + Convert.ToDecimal(lblBPCPCDMunicipalExercicio4.Text)
                                            + Convert.ToDecimal(lblAcaoJovemMunicipalExercicio4.Text)
                                            + Convert.ToDecimal(lblRendaCidadaMunicipalExercicio4.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaMunicipalExercicio4.Text)
                                            + Convert.ToDecimal(lblProgramaMunicipalExercicio4.Text)).ToString("n2");

            lblProgramaTotalEstadualExercicio4.Text = (Convert.ToDecimal(lblBEvEstadualExercicio4.Text)
                                            + Convert.ToDecimal(lblBPCIdososEstadualExercicio4.Text)
                                            + Convert.ToDecimal(lblBPCPCDEstadualExercicio4.Text)
                                            + Convert.ToDecimal(lblAcaoJovemEstadualExercicio4.Text)
                                            + Convert.ToDecimal(lblRendaCidadaEstadualExercicio4.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoEstadualExercicio4.Text)
                                            + Convert.ToDecimal(lblBolsaFamiliaEstadualExercicio4.Text)
                                            + Convert.ToDecimal(lblProgramaEstadualExercicio4.Text)
                                            ).ToString("n2");

            lblProgramaTotalFederalExercicio4.Text = (Convert.ToDecimal(lblBEvFederalExercicio4.Text)
                                            + Convert.ToDecimal(lblBPCIdososFederalExercicio4.Text)
                                            + Convert.ToDecimal(lblBPCPCDFederalExercicio4.Text)
                                            + Convert.ToDecimal(lblAcaoJovemFederalExercicio4.Text)
                                            + Convert.ToDecimal(lblRendaCidadaFederalExercicio4.Text)
                                            + (String.IsNullOrEmpty(lblBolsaFamiliaFederalExercicio4.Text) ? 0M : Convert.ToDecimal(lblBolsaFamiliaFederalExercicio4.Text))
                                            + Convert.ToDecimal(lblProgramaFederalExercicio4.Text)).ToString("n2");

            lblProgramaTotalGeralExercicio4.Text = (Convert.ToDecimal(lblBEvTotalExercicio4.Text)
                                            + Convert.ToDecimal(lblBPCIdososTotalExercicio4.Text)
                                            + Convert.ToDecimal(lblBPCPCDTotalExercicio4.Text)
                                            + Convert.ToDecimal(lblAcaoJovemTotalExercicio4.Text)
                                            + Convert.ToDecimal(lblRendaCidadaTotalExercicio4.Text)
                                            + Convert.ToDecimal(lblRendaCidadaBeneficioIdosoTotalExercicio4.Text)
                                            + (String.IsNullOrEmpty(lblBolsaFamiliaTotalExercicio4.Text) ? 0M : Convert.ToDecimal(lblBolsaFamiliaTotalExercicio4.Text))
                                            + Convert.ToDecimal(lblProgramaMunicipalTotalExercicio4.Text)).ToString("n2");
            #endregion

            #region Indices [IGD, IGDPBF, IGDSUAS]
            var indice = prefeituras.GetIndiceGestaoDescentralizada(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
            if (indice != null)
            {
                var IGD = 0m;
                if (indice.IGDPBFValorAnual.HasValue)
                    IGD += indice.IGDPBFValorAnual.Value;
                if (indice.IGDSUASValorAnual.HasValue)
                    IGD += indice.IGDSUASValorAnual.Value;
            }
            #endregion

        }
        #endregion


        #region Quadro 1 - Serviços socioassistenciais [Previsão Orçamentaria]
        void carregarPrevisaoOrcamentaria2017(Prefeituras prefeituras)
        {
            var lst = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id,2021); //GetPrevisaoOrcamentaria2016(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            lvPrevisaoOrcamentariaExercicio0.DataSource = lst;
            lvPrevisaoOrcamentariaExercicio0.DataBind();

            var lei = prefeituras.GetLeiOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id,2021);  //GetLeiOrcamentaria2016(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (lei != null)
            {
                lblLei.Text = lei.Lei;
                lblValorAprovadoLei.Text = String.Format("{0:C}", lei.ValorAprovado);
                lblDataLei.Text = lei.DataPublicacao.ToShortDateString();
            }

            //SUB TOTAL
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblSubTotalRedePublicaMunicipal")).Text = String.Format("{0:N2}", lst.Sum(p => p.RedePublicaMunicipal));
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblSubTotalRedePrivadaMunicipal")).Text = String.Format("{0:N2}", lst.Sum(p => p.RedePrivadaMunicipal));
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblSubTotalRedePublicaEstadual")).Text = String.Format("{0:N2}", lst.Sum(p => p.RedePublicaEstadual));
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblSubTotalRedePrivadaEstadual")).Text = String.Format("{0:N2}", lst.Sum(p => p.RedePrivadaEstadual));
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblSubTotalRedePublicaFederal")).Text = String.Format("{0:N2}", lst.Sum(p => p.RedePublicaFederal));
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblSubTotalRedePrivadaFederal")).Text = String.Format("{0:N2}", lst.Sum(p => p.RedePrivadaFederal));
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblSubTotalPrivado")).Text = ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalPrivadoExercicio0")).Text = String.Format("{0:N2}", lst.Sum(p => p.Privado));
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblSubTotal")).Text = ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalGeralExercicio0")).Text = String.Format("{0:N2}", lst.Sum(p => p.Privado + p.RedePublicaMunicipal + p.RedePrivadaMunicipal + p.RedePublicaEstadual + p.RedePrivadaEstadual + p.RedePublicaFederal + p.RedePrivadaFederal));

            //TOTAL
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalMunicipalExercicio0")).Text = String.Format("{0:N2}", lst.Sum(p => p.RedePublicaMunicipal + p.RedePrivadaMunicipal));
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalEstadualExercicio0")).Text = String.Format("{0:N2}", lst.Sum(p => p.RedePublicaEstadual + p.RedePrivadaEstadual));
            ((Label)lvPrevisaoOrcamentariaExercicio0.FindControl("lblTotalFederalExercicio0")).Text = String.Format("{0:N2}", lst.Sum(p => p.RedePublicaFederal + p.RedePrivadaFederal));

        }

        protected void lstPrevisaoOrcamentariaExercicio0_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (PrevisaoOrcamentariaInfo)e.Item.DataItem;
                var total = item.Privado + item.RedePublicaMunicipal + item.RedePrivadaMunicipal + item.RedePublicaEstadual + item.RedePrivadaEstadual + item.RedePublicaFederal + item.RedePrivadaFederal;
                ((Label)e.Item.FindControl("lblTotal")).Text = String.Format("{0:N2}", total);
            }
        }
        void carregarPrevisaoOrcamentaria(Prefeituras prefeituras)
        {

            List<PrevisaoOrcamentariaInfo> previsaoOrcamentariaExercicio1 = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id, FPrevisaoOrcamentaria.Exercicios[0]).ToList();
            List<PrevisaoOrcamentariaInfo> previsaoOrcamentariaExercicio2 = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id, FPrevisaoOrcamentaria.Exercicios[1]).ToList();
            List<PrevisaoOrcamentariaInfo> previsaoOrcamentariaExercicio3 = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id, FPrevisaoOrcamentaria.Exercicios[2]).ToList();
            List<PrevisaoOrcamentariaInfo> previsaoOrcamentariaExercicio4 = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id, FPrevisaoOrcamentaria.Exercicios[3]).ToList();


            carregarPrevisaoOrcamentariaExercicio1(previsaoOrcamentariaExercicio1);
            carregarPrevisaoOrcamentariaExercicio2(previsaoOrcamentariaExercicio2);
            carregarPrevisaoOrcamentariaExercicio3(previsaoOrcamentariaExercicio3);
            carregarPrevisaoOrcamentariaExercicio4(previsaoOrcamentariaExercicio4);
        }

        void carregarPrevisaoOrcamentariaExercicio1(List<PrevisaoOrcamentariaInfo> previsoesOrcamentarias)
        {
            lvPrevisaoOrcamentaria.DataSource = previsoesOrcamentarias;
            lvPrevisaoOrcamentaria.DataBind();

            if (lvPrevisaoOrcamentaria.Items.Count > 0)
            {
                //SUB TOTAL
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblSubTotalRedePublicaMunicipal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaMunicipal));
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblSubTotalRedePrivadaMunicipal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaMunicipal));
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblSubTotalRedePublicaEstadual")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaEstadual));
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblSubTotalRedePrivadaEstadual")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaEstadual));
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblSubTotalRedePublicaFederal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaFederal));
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblSubTotalRedePrivadaFederal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaFederal));
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblSubTotalPrivado")).Text = ((Label)lvPrevisaoOrcamentaria.FindControl("lblTotalPrivado")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado));
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblSubTotal")).Text = ((Label)lvPrevisaoOrcamentaria.FindControl("lblTotalGeral")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado + p.RedePublicaMunicipal + p.RedePrivadaMunicipal + p.RedePublicaEstadual + p.RedePrivadaEstadual + p.RedePublicaFederal + p.RedePrivadaFederal));

                //TOTAL
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblTotalMunicipal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaMunicipal + p.RedePrivadaMunicipal));
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblTotalEstadual")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaEstadual + p.RedePrivadaEstadual));
                ((Label)lvPrevisaoOrcamentaria.FindControl("lblTotalFederal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaFederal + p.RedePrivadaFederal));
            }
        }

        void carregarPrevisaoOrcamentariaExercicio2(List<PrevisaoOrcamentariaInfo> previsoesOrcamentarias)
        {
            lvPrevisaoOrcamentariaExercicio2.DataSource = previsoesOrcamentarias;
            lvPrevisaoOrcamentariaExercicio2.DataBind();

            if (lvPrevisaoOrcamentariaExercicio2.Items.Count > 0)
            {
                //SUB TOTAL
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblSubTotalRedePublicaMunicipal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaMunicipal));
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblSubTotalRedePrivadaMunicipal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaMunicipal));
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblSubTotalRedePublicaEstadual")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaEstadual));
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblSubTotalRedePrivadaEstadual")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaEstadual));
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblSubTotalRedePublicaFederal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaFederal));
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblSubTotalRedePrivadaFederal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaFederal));
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblSubTotalPrivado")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado));
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblTotalPrivado")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado));

                var sumValoresRedeExercicio2 = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado + p.RedePublicaMunicipal + p.RedePrivadaMunicipal + p.RedePublicaEstadual + p.RedePrivadaEstadual + p.RedePublicaFederal + p.RedePrivadaFederal));

                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblSubTotal")).Text = sumValoresRedeExercicio2;
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblTotalGeral")).Text = sumValoresRedeExercicio2;


                //TOTAL
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblTotalMunicipal")).Text = String.Format("{0:N2}"
                    , previsoesOrcamentarias.Sum(p => p.RedePublicaMunicipal + p.RedePrivadaMunicipal));
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblTotalEstadual")).Text = String.Format("{0:N2}"
                    , previsoesOrcamentarias.Sum(p => p.RedePublicaEstadual + p.RedePrivadaEstadual));
                ((Label)lvPrevisaoOrcamentariaExercicio2.FindControl("lblTotalFederal")).Text = String.Format("{0:N2}"
                    , previsoesOrcamentarias.Sum(p => p.RedePublicaFederal + p.RedePrivadaFederal));
            }
        }

        void carregarPrevisaoOrcamentariaExercicio3(List<PrevisaoOrcamentariaInfo> previsoesOrcamentarias)
        {
            lvPrevisaoOrcamentariaExercicio3.DataSource = previsoesOrcamentarias;
            lvPrevisaoOrcamentariaExercicio3.DataBind();

            if (lvPrevisaoOrcamentariaExercicio3.Items.Count > 0)
            {
                //SUB TOTAL
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblSubTotalRedePublicaMunicipal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaMunicipal));
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblSubTotalRedePrivadaMunicipal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaMunicipal));
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblSubTotalRedePublicaEstadual")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaEstadual));
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblSubTotalRedePrivadaEstadual")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaEstadual));
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblSubTotalRedePublicaFederal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaFederal));
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblSubTotalRedePrivadaFederal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaFederal));
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblSubTotalPrivado")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado));
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblTotalPrivado")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado));

                var sumValoresRedeExercicio3 = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado + p.RedePublicaMunicipal + p.RedePrivadaMunicipal + p.RedePublicaEstadual + p.RedePrivadaEstadual + p.RedePublicaFederal + p.RedePrivadaFederal));

                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblSubTotal")).Text = sumValoresRedeExercicio3;
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblTotalGeral")).Text = sumValoresRedeExercicio3;


                //TOTAL
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblTotalMunicipal")).Text = String.Format("{0:N2}"
                    , previsoesOrcamentarias.Sum(p => p.RedePublicaMunicipal + p.RedePrivadaMunicipal));
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblTotalEstadual")).Text = String.Format("{0:N2}"
                    , previsoesOrcamentarias.Sum(p => p.RedePublicaEstadual + p.RedePrivadaEstadual));
                ((Label)lvPrevisaoOrcamentariaExercicio3.FindControl("lblTotalFederal")).Text = String.Format("{0:N2}"
                    , previsoesOrcamentarias.Sum(p => p.RedePublicaFederal + p.RedePrivadaFederal));
            }
        }

        void carregarPrevisaoOrcamentariaExercicio4(List<PrevisaoOrcamentariaInfo> previsoesOrcamentarias)
        {
            lvPrevisaoOrcamentariaExercicio4.DataSource = previsoesOrcamentarias;
            lvPrevisaoOrcamentariaExercicio4.DataBind();

            if (lvPrevisaoOrcamentariaExercicio4.Items.Count > 0)
            {
                //SUB TOTAL
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblSubTotalRedePublicaMunicipal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaMunicipal));
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblSubTotalRedePrivadaMunicipal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaMunicipal));
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblSubTotalRedePublicaEstadual")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaEstadual));
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblSubTotalRedePrivadaEstadual")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaEstadual));
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblSubTotalRedePublicaFederal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaFederal));
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblSubTotalRedePrivadaFederal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePrivadaFederal));
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblSubTotalPrivado")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado));
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblTotalPrivado")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado));

                var sumValoresRedeExercicio4 = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.Privado + p.RedePublicaMunicipal + p.RedePrivadaMunicipal + p.RedePublicaEstadual + p.RedePrivadaEstadual + p.RedePublicaFederal + p.RedePrivadaFederal));

                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblSubTotal")).Text = sumValoresRedeExercicio4;
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblTotalGeral")).Text = sumValoresRedeExercicio4;


                //TOTAL
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblTotalMunicipal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaMunicipal + p.RedePrivadaMunicipal));
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblTotalEstadual")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaEstadual + p.RedePrivadaEstadual));
                ((Label)lvPrevisaoOrcamentariaExercicio4.FindControl("lblTotalFederal")).Text = String.Format("{0:N2}", previsoesOrcamentarias.Sum(p => p.RedePublicaFederal + p.RedePrivadaFederal));
            }
        }

        protected void lstPrevisaoOrcamentariaExercicio1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (PrevisaoOrcamentariaInfo)e.Item.DataItem;
                var total = item.Privado
                    + item.RedePublicaMunicipal
                    + item.RedePrivadaMunicipal
                    + item.RedePublicaEstadual
                    + item.RedePrivadaEstadual
                    + item.RedePublicaFederal
                    + item.RedePrivadaFederal;
                ((Label)e.Item.FindControl("lblTotal")).Text = String.Format("{0:N2}", total);
            }
        }

        protected void lstPrevisaoOrcamentariaExercicio2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (PrevisaoOrcamentariaInfo)e.Item.DataItem;
                var total = item.Privado
                    + item.RedePublicaMunicipal
                    + item.RedePrivadaMunicipal
                    + item.RedePublicaEstadual
                    + item.RedePrivadaEstadual
                    + item.RedePublicaFederal
                    + item.RedePrivadaFederal;
                ((Label)e.Item.FindControl("lblTotal")).Text = String.Format("{0:N2}", total);
            }
        }

        protected void lstPrevisaoOrcamentariaExercicio3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (PrevisaoOrcamentariaInfo)e.Item.DataItem;
                var total = item.Privado
                    + item.RedePublicaMunicipal
                    + item.RedePrivadaMunicipal
                    + item.RedePublicaEstadual
                    + item.RedePrivadaEstadual
                    + item.RedePublicaFederal
                    + item.RedePrivadaFederal;
                ((Label)e.Item.FindControl("lblTotal")).Text = String.Format("{0:N2}", total);
            }
        }

        protected void lstPrevisaoOrcamentariaExercicio4_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (PrevisaoOrcamentariaInfo)e.Item.DataItem;
                var total = item.Privado
                    + item.RedePublicaMunicipal
                    + item.RedePrivadaMunicipal
                    + item.RedePublicaEstadual
                    + item.RedePrivadaEstadual
                    + item.RedePublicaFederal
                    + item.RedePrivadaFederal;
                ((Label)e.Item.FindControl("lblTotal")).Text = String.Format("{0:N2}", total);
            }
        }

        #endregion
    }
}