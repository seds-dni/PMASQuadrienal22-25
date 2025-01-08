using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.Servicos.Contratos
{
    [ServiceContract]
    public interface IRedeProtecaoSocialService
    {

        #region MotivoDesvinculacaoServico
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<MotivoDesativacaoServicoInfo> GetMotivoDesativacaoServico();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MotivoDesativacaoServicoInfo GetMotivoDesativacaoServicoById(int id);
        #endregion

        #region MotivoDesvincalacaoLocal
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<MotivoDesativacaoLocalInfo> GetMotivoDesativacaoLocal();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MotivoDesativacaoLocalInfo GetMotivoDesativacaoLocalById(int id);
        #endregion

        #region Unidade Publica
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaLocalPublicoGeral> GetLocaisPublicosByUnidade(int idUnidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UnidadePublicaInfo GetUnidadePublicaById(Int32 idUnidadePublica);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaUnidadePublicaInfo> GetIdentificacaoUnidadesPublicaByPrefeitura(Int32 idPrefeitura, String nome);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateUnidadePublica(UnidadePublicaInfo unidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddUnidadePublica(UnidadePublicaInfo unidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUnidadePublica(Int32 idUnidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LocalExecucaoPublicoInfo GetLocalExecucaoPublicoById(Int32 idLocalExecucao);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaLocalExecucaoPublicoInfo> GetIdentificacaoLocalExecucaoPublicoByUnidade(Int32 idUnidade, String nome = null);  

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateLocalExecucaoPublico(LocalExecucaoPublicoInfo local);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddLocalExecucaoPublico(LocalExecucaoPublicoInfo local);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLocalExecucaoPublico(Int32 idLocal);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddServicoRecursoFinanceiroPublico(ServicoRecursoFinanceiroPublicoInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateServicoRecursoFinanceiroPublico(ServicoRecursoFinanceiroPublicoInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteServicoRecursoFinanceiroPublico(Int32 idServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ServicoRecursoFinanceiroPublicoInfo GetServicoRecursoFinanceiroPublicoById(Int32 idServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaServicosRecursosFinanceirosPublicoInfo> GetConsultaServicosRecursosFinanceirosPublicoByLocalExecucao(Int32 idLocalExecucao);
        #endregion

        #region Unidade Privada

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UnidadePrivadaInfo GetUnidadePrivadaById(Int32 idUnidadePrivada);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaUnidadePrivadaInfo> GetIdentificacaoUnidadesPrivadaByPrefeitura(Int32 idPrefeitura, String nome);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateUnidadePrivada(UnidadePrivadaInfo unidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddUnidadePrivada(UnidadePrivadaInfo unidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUnidadePrivada(Int32 idUnidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LocalExecucaoPrivadoInfo GetLocalExecucaoPrivadoById(Int32 idLocalExecucao);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaLocalExecucaoPrivadoInfo> GetIdentificacaoLocalExecucaoPrivadoByUnidade(Int32 idUnidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateLocalExecucaoPrivado(LocalExecucaoPrivadoInfo local);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddLocalExecucaoPrivado(LocalExecucaoPrivadoInfo local);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLocalExecucaoPrivado(Int32 idLocal);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddServicoRecursoFinanceiroPrivado(ServicoRecursoFinanceiroPrivadoInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateServicoRecursoFinanceiroPrivado(ServicoRecursoFinanceiroPrivadoInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteServicoRecursoFinanceiroPrivado(Int32 idServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ServicoRecursoFinanceiroPrivadoInfo GetServicoRecursoFinanceiroPrivadoById(Int32 idServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaServicosRecursosFinanceirosPrivadoInfo> GetConsultaServicosRecursosFinanceirosPrivadoByLocalExecucao(Int32 idLocalExecucao);

        #endregion

        #region CRAS
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CRASInfo GetCRASById(Int32 idCRAS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        String GetCRASNomeById(Int32 idCRAS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaCRASInfo> GetIdentificacaoCRASByUnidade(Int32 idUnidade, String nome);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateCRAS(CRASInfo cras);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddCRAS(CRASInfo cras);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCRAS(Int32 idCRAS);

        #region Serviços e Recursos Financeiros
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddServicoRecursoFinanceiroCRAS(ServicoRecursoFinanceiroCRASInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateServicoRecursoFinanceiroCRAS(ServicoRecursoFinanceiroCRASInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteServicoRecursoFinanceiroCRAS(Int32 idServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ServicoRecursoFinanceiroCRASInfo GetServicoRecursoFinanceiroCRASById(Int32 idServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaServicosRecursosFinanceirosCRASInfo> GetConsultaServicosRecursosFinanceirosByCRAS(Int32 idCRAS);
        #endregion

        #region Previsão de Instalação
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SalvarPrevisaoInstalacaoCRAS(Boolean haPrevisao, List<PrevisaoInstalacaoCRASInfo> previsaoInstalacaoCRAS, List<MotivoNaoInstalacaoInfo> lstMotivos, Int32 idPrefeitura, Int32 idUnidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PrevisaoInstalacaoCRASInfo> GetPrevisoesCRASByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<MotivoNaoInstalacaoInfo> GetMotivosDeNaoInstalacaoDeCRAS(Int32 idPrefeitura);

        #endregion

        #endregion

        #region CREAS
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CREASInfo GetCREASPorId(Int32 idCREAS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        String GetCREASNomeById(Int32 idCREAS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaCREASInfo> GetIdentificacoesCREASByUnidade(Int32 idUnidade, String nome);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateCREAS(CREASInfo creas);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddCREAS(CREASInfo creas);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCREAS(Int32 idCREAS);

        #region Serviços e Recursos Financeiros
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddServicoRecursoFinanceiroCREAS(ServicoRecursoFinanceiroCREASInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateServicoRecursoFinanceiroCREAS(ServicoRecursoFinanceiroCREASInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteServicoRecursoFinanceiroCREAS(Int32 idServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ServicoRecursoFinanceiroCREASInfo GetServicoRecursoFinanceiroCREASById(Int32 idServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaServicosRecursosFinanceirosCREASInfo> GetConsultaServicosRecursosFinanceirosByCREAS(Int32 idCREAS);
        #endregion

        #region Previsão de Instalação
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SavePrevisaoInstalacaoCREAS(Boolean haPrevisao, List<PrevisaoInstalacaoCREASInfo> previsaoInstalacaoCREAS, List<MotivoNaoInstalacaoInfo> lstMotivos, Int32 idPrefeitura, Boolean haDemanda, List<PrefeituraDemandaAtendimentoInfo> lstDemandas, Int32 idUnidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PrevisaoInstalacaoCREASInfo> GetPrevisaoCREASByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCREASByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PrefeituraDemandaAtendimentoInfo> GetDemandaAtendimentoCREASByPrefeitura(Int32 idPrefeitura);

        #endregion

        #endregion

        #region CentroPOP
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CentroPOPInfo GetCentroPOPById(Int32 idCentroPOP);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        String GetCentroPOPNomeById(Int32 idCentroPOP);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaCentroPOPInfo> GetIdentificacaoCentroPOPByUnidade(Int32 idUnidade, String nome);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateCentroPOP(CentroPOPInfo centro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddCentroPOP(CentroPOPInfo centro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCentroPOP(Int32 idCentroPOP);

        #region Serviços e Recursos Financeiros
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddServicoRecursoFinanceiroCentroPOP(ServicoRecursoFinanceiroCentroPOPInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateServicoRecursoFinanceiroCentroPOP(ServicoRecursoFinanceiroCentroPOPInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteServicoRecursoFinanceiroCentroPOP(Int32 idServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ServicoRecursoFinanceiroCentroPOPInfo GetServicoRecursoFinanceiroCentroPOPById(Int32 idServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaServicosRecursosFinanceirosCentroPOPInfo> GetConsultaServicosRecursosFinanceirosByCentroPOP(Int32 idCentroPOP);
        #endregion
        #region Previsão de Instalação
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SavePrevisaoInstalacaoCentroPOP(Boolean haPrevisao, List<PrevisaoInstalacaoCentroPOPInfo> previsaoInstalacaoCentroPOP, List<MotivoNaoInstalacaoInfo> lstMotivos, Int32 idPrefeitura, Int32 idUnidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PrevisaoInstalacaoCentroPOPInfo> GetPrevisaoCentroPOPByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCentroPOPByPrefeitura(Int32 idPrefeitura);

        #endregion
        #endregion

        #region Analise Diagnostica
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AnaliseDiagnosticaInfo GetAnaliseDiagnosticaById(Int32 IdAnaliseDiagnostica);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaAnaliseDiagnosticaInfo> GetConsultaAnaliseDiagnosticaByPrefeitura(Int32 idPrefeitura);


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaAnaliseDiagnosticaInfo> GetConsultaAnaliseDiagnosticaByMunicipio(Int32 idMunicipio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateAnaliseDiagnostica(AnaliseDiagnosticaInfo analise);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddAnaliseDiagnostica(AnaliseDiagnosticaInfo analise);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAnaliseDiagnostica(Int32 idAnaliseDiagnostica);
        #endregion

        #region Analise Diagnostica Comunidade
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AddAnaliseDiagnosticaComunidade(AnaliseDiagnosticaComunidadeInfo obj);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateAnaliseDiagnosTicaComunidade(AnaliseDiagnosticaComunidadeInfo obj);
        #endregion


        #region IntencaoAcao

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaIntencaoAcaoInfo> GetConsultaConsultaIntencaoAcoesByPrefeitura(Int32 idPrefeitura);

        #endregion
    }
}
