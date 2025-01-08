using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;

namespace Seds.PMAS.QUADRIENAL.Servicos.Contratos
{
    [ServiceContract]
    public interface IRelatoriosService
    {        
        #region Relatórios Descritivos
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<InformacoesMunicipaisBasicasInfo> GetInformacoesMunicipaisBasicas(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ComunidadesPovosGruposEspecificosInfo> GetAnaliseDiagnosticaComunidades(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<InformacoesBasicasDradsInfo> GetInformacoesBasicasDrads(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<InformacoesFMASInfo> GetInformacoesFMAS(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<FuncionamentoCRASInfo> GetFuncionamentoCRAS(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<FuncionamentoCREASInfo> GetFuncionamentoCREAS(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<FuncionamentoCentroPOPInfo> GetFuncionamentoCentroPOP(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConselhosMunicipaisExistentesInfo> GetConselhosMunicipaisExistentes(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<OrganizacaoOrgaoGestorInfo> GetOrganizacaoOrgaoGestor(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<RHOrgaoGestorInfo> GetRHOrgaoGestor(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<RHRedeExecutoraInfo> GetRHRedeExecutora(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ProgramasMunicipaisTransferenciaRendaInfo> GetProgramasMunicipaisTransferenciaRenda(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<SaoPauloSolidarioInfo> GetProgramaSaoPauloSolidario(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<IntegracaoServicoInfo> GetIntegracaoServicos(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<DistribuicaoEstadualProtecaoSocialInfo> GetDistribuicaoEstadualProtecaoSocial(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<DistribuicaoEstadualProgramaTrabalhoInfo> GetDistribuicaoEstadualProgramaTrabalho(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ResumoTransferenciaRendaInfo> GetResumoTransferenciaRenda(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<RelatorioAnaliseDiagnosticaProcInfo> GetAnaliseDiagnostica(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ServicoEstadualizadoInfo> GetServicosEstadualizados(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<RedeServicoSocioassistencialInfo> GetRedeServicoSocioassistencial(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<RedeServicoSocioassistencialDetalhamentoInfo> GetRedeServicoSocioassistencialDetalhamento(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<InformacoesBeneficiosEventuaisInfo> GetInformacoesBeneficiosEventuais(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AcaoPlanejadaInfo> GetAcoesPlanejadas(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AcaoMonitoramentoInfo> GetAcoesMonitoramento(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AcaoAvaliacaoInfo> GetAcoesAvaliacao(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<RelAcaoVigilanciaInfo> GetAcoesVigilanciaSocioassistencial(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AtividadeServicoInfo> GetAtividadesServicosSocioassistenciais(RelatorioFiltroInfo filtro);
        #endregion

        #region Relatórios Quantitativos
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<DistribuicaoPorteNivelGestaoInfo> GetDistribuicaoMunicipiosPorteNivelGestao(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<QuantidadesServicosLocaisExecucaoInfo> GetQuantidadeServicosLocaisExecucao(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<DistribuicaoSituacaoVulnerabilidadeInfo> GetDistribuicaoSituacaoVulnerabilidade(RelatorioFiltroInfo filtro);
        #endregion

        #region Relatórios Cadastrais
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<InformacoesCadastraisPrefeiturasInfo> GetInformacoesCadastraisPrefeituras(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<InformacoesCadastraisOrgaoGestorInfo> GetInformacoesCadastraisOrgaoGestor(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<InformacoesCadastraisConselhoMunicipalInfo> GetInformacoesCadastraisConselhoMunicipal(RelatorioFiltroInfo filtro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<InformacoesCadastraisLocalExecucaoInfo> GetInformacoesCadastraisLocalExecucao(RelatorioFiltroInfo filtro);
        #endregion
    }
}
