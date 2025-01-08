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
    public interface IAcoesService
    {
        #region Planejamento de Ações
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaPrefeituraAcaoPlanejamentoInfo> GetConsultaPlanejamentoAcoesByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PrefeituraAcaoPlanejamentoInfo> GetPlanejamentoAcoesByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PrefeituraAcaoPlanejamentoInfo GetAcaoPlanejamentoById(Int32 idPrefeituraAcaoPlanejamento);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddAcaoPlanejamento(PrefeituraAcaoPlanejamentoInfo acao);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateAcaoPlanejamento(PrefeituraAcaoPlanejamentoInfo acao);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAcaoPlanejamento(Int32 idPrefeituraAcaoPlanejamento);     
        #endregion            

        #region Vigilância
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        VigilanciaSocioAssistencialInfo GetVigilanciaByPrefeitura(int idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveVigilancia(VigilanciaSocioAssistencialInfo obj);        
        #endregion

        #region Avaliação
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AvaliacaoInfo GetAvaliacaoByPrefeitura(int idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveAvaliacao(AvaliacaoInfo obj);
        #endregion

        #region Monitoramento
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MonitoramentoInfo GetMonitoramentoByPrefeitura(int idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveMonitoramento(MonitoramentoInfo obj);        
        #endregion

        #region Aspectos Gerais        
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PrefeituraVigilanciaMonitoramentoAvaliacaoInfo GetAspectosGeraisVigilanciaMonitoramentoAvaliacaoByPrefeitura(int idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Boolean PrefeituraPossuiVigilanciaMonitoramentoAvalicao(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveAspectosGeraisVigilanciaMonitoramentoAvaliacao(PrefeituraVigilanciaMonitoramentoAvaliacaoInfo obj);        
        #endregion
    }
}
