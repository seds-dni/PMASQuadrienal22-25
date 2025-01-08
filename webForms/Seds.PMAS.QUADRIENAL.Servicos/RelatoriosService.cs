using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Servicos.Contratos;
using System.ServiceModel;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio;
using System.Security.Permissions;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;

namespace Seds.PMAS.QUADRIENAL.Servicos
{
    /// <summary>
    /// Serviço Responsável por fornecer Relatórios do PMAS 2017
    /// </summary>
    [ServiceBehavior(Namespace = "http://seds.sp.gov.br/relatorios",
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerSession,
    TransactionIsolationLevel = IsolationLevel.ReadCommitted,
    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class RelatoriosService : IRelatoriosService
    {
        #region Relatórios Descritivos
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<InformacoesMunicipaisBasicasInfo> GetInformacoesMunicipaisBasicas(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetInformacoesMunicipaisBasicas(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<InformacoesBasicasDradsInfo> GetInformacoesBasicasDrads(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetInformacoesBasicasDrads(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<InformacoesFMASInfo> GetInformacoesFMAS(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetInformacoesFMAS(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<FuncionamentoCRASInfo> GetFuncionamentoCRAS(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetFuncionamentoCRAS(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ComunidadesPovosGruposEspecificosInfo> GetAnaliseDiagnosticaComunidades(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetAnaliseDiagnosticaComunidades(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<FuncionamentoCREASInfo> GetFuncionamentoCREAS(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetFuncionamentoCREAS(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ExecucaoRecursosCofinanciamentoEstadualInfo> GetExecucaoRecursosCofinanciamentoEstadual(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetExecucaoRecursosCofinanciamentoEstadual(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<FuncionamentoCentroPOPInfo> GetFuncionamentoCentroPOP(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetFuncionamentoCentroPOP(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConselhosMunicipaisExistentesInfo> GetConselhosMunicipaisExistentes(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetConselhosMunicipaisExistentes(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<OrganizacaoOrgaoGestorInfo> GetOrganizacaoOrgaoGestor(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetOrganizacaoOrgaoGestor(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RHOrgaoGestorInfo> GetRHOrgaoGestor(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetRHOrgaoGestor(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RHRedeExecutoraInfo> GetRHRedeExecutora(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetRHRedeExecutora(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ProgramasMunicipaisTransferenciaRendaInfo> GetProgramasMunicipaisTransferenciaRenda(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetProgramasMunicipaisTransferenciaRenda(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ProgramaProjetoGeralInfo> GetProgramaProjetoGeral(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetProgramaProjetoGeral(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<InformacoesProgramaFamiliaPaulistaInfo> GetInformacoesProgramaFamiliaPaulista(RelatorioFiltroInfo filtro) 
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetInformacoesProgramaFamiliaPaulista(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<DiagnosticoSocioterritorialInfo> GetDiagnosticoSocioterritorial(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetGetDiagnosticoSocioterritorial(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<SaoPauloAmigoIdosoInfo> GetProgramaSaoPauloAmigoIdoso(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetProgramaSaoPauloAmigoIdoso(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<SaoPauloSolidarioInfo> GetProgramaSaoPauloSolidario(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetProgramaSaoPauloSolidario(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<IntegracaoServicoInfo> GetIntegracaoServicos(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetIntegracaoServico(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<DistribuicaoEstadualProtecaoSocialInfo> GetDistribuicaoEstadualProtecaoSocial(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetDistribuicaoEstadualProtecaoSocial(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<DistribuicaoEstadualProgramaTrabalhoInfo> GetDistribuicaoEstadualProgramaTrabalho(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetDistribuicaoEstadualProgramaTrabalho(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ResumoTransferenciaRendaInfo> GetResumoTransferenciaRenda(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetResumoTransferenciaRenda(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RelatorioAnaliseDiagnosticaProcInfo> GetAnaliseDiagnostica(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetAnaliseDiagnostica(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ServicoEstadualizadoInfo> GetServicosEstadualizados(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetServicosEstadualizados(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RedeServicoSocioassistencialRelatorio> GetRedeServicoSocioassistencialSimples(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetRedeServicoSocioassistencialRelatorio(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RPrestacaoDeContasBasica> GetPrestacaDeContasProtecaoBasica(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetPrestacaDeContasProtecaoBasica(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RPrestacaoDeContasMedia> GetPrestacaDeContasProtecaoMedia(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetPrestacaDeContasProtecaoMedia(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RPrestacaoDeContasAlta> GetPrestacaDeContasProtecaoAlta(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetPrestacaDeContasProtecaoAlta(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RPrestacaoDeContasBeneficiosEventuais> GetPrestacaDeContasProtecaoBeneficiosEventuais(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetPrestacaDeContasBeneficiosEventuais(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RPrestacaoDeContasProgramasProjetos> GetPrestacaDeContasProtecaoProgramasProjetos(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetPrestacaDeContasProtecaoProgramasProjetos(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RStatusPrestacaoDeContasInfo> GetStatusPrestacaoDeContas(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetStatusPrestacaoDeContas(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RAuxilioReclusaoPensaoMorteInfo> GetAuxilioReclusaoPensaoMorte(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetAuxilioReclusaoPensaoMorte(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RStatusLeiOrcamentariaInfo> GetStatusLeiOrcamentaria(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetStatusLeiOrcamentaria(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RStatusExecucaoFinanceiraInfo> GetStatusExecucaoFinanceira(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetStatusExecucaoFinanceira(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RedeServicoSocioassistencialRegionalizadosRelatorio> GetRedeServicoSocioassistencialRegionalizados(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetRedeServicoRegionalizadosRelatorio(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RelatorioAEPETIInfo> GetAEPETIRelatorio(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetAEPETIRelatorio(filtro);
            ContextManager.CloseConnection();
            return lst;
        }


        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RedeServicoSocioassistencialInfo> GetRedeServicoSocioassistencial(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetRedeServicoSocioassistencial(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RedeServicoSocioassistencialDetalhamentoInfo> GetRedeServicoSocioassistencialDetalhamento(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetRedeServicoSocioassistencialDetalhamento(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RedeServicoSocioassistencialInfo> GetServicosIntermunicipais(RelatorioFiltroInfo filtro)  
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetServicosIntermunicipais(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<InformacoesBeneficiosEventuaisInfo> GetInformacoesBeneficiosEventuais(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetInformacoesBeneficiosEventuais(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcaoPlanejadaInfo> GetAcoesPlanejadas(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetAcoesPlanejadas(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcaoMonitoramentoInfo> GetAcoesMonitoramento(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetAcoesMonitoramento(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcaoAvaliacaoInfo> GetAcoesAvaliacao(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetAcoesAvaliacao(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<RelAcaoVigilanciaInfo> GetAcoesVigilanciaSocioassistencial(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetAcoesVigilanciaSocioassistencial(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AtividadeServicoInfo> GetAtividadesServicosSocioassistenciais(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetAtividadesServicosSocioassistenciais(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<UnidadeGeralInfo> GetUnidades(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetUnidades(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        #endregion

        #region Relatórios Quantitativos
        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<DistribuicaoPorteNivelGestaoInfo> GetDistribuicaoMunicipiosPorteNivelGestao(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioQuantitativo().GetDistribuicaoMunicipiosPorteNivelGestao(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<QuantidadesServicosLocaisExecucaoInfo> GetQuantidadeServicosLocaisExecucao(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioQuantitativo().GetQuantidadeServicosLocaisExecucao(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<DistribuicaoSituacaoVulnerabilidadeInfo> GetDistribuicaoSituacaoVulnerabilidade(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioQuantitativo().GetDistribuicaoSituacaoVulnerabilidade(filtro);
            ContextManager.CloseConnection();
            return lst;
        }
        #endregion

        #region Relatórios Cadastrais
        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<InformacoesCadastraisPrefeiturasInfo> GetInformacoesCadastraisPrefeituras(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioCadastral().GetInformacoesCadastraisPrefeituras(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<InformacoesCadastraisOrgaoGestorInfo> GetInformacoesCadastraisOrgaoGestor(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioCadastral().GetInformacoesCadastraisOrgaoGestor(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<InformacoesCadastraisConselhoMunicipalInfo> GetInformacoesCadastraisConselhoMunicipal(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioCadastral().GetInformacoesCadastraisConselhoMunicipal(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<InformacoesCadastraisLocalExecucaoInfo> GetInformacoesCadastraisLocalExecucao(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioCadastral().GetInformacoesCadastraisLocalExecucao(filtro);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<CronogramaDesembolsoRelatorio22Info> GetCronogramaDesembolso(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetCronogramaDesembolso(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        public List<CronogramaDesembolsoRelatorio22Info> GetTotalCronogramaDesembolso(RelatorioFiltroInfo filtro)
        {
            ContextManager.OpenConnection();
            var lst = new RelatorioDescritivo().GetTotalCronogramaDesembolso(filtro).ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        #endregion



       
    }
}
