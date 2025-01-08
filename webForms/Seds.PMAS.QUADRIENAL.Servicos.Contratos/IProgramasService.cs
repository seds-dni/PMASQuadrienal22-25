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
    public interface IProgramasService
    {
        #region Programas/Projetos
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaProgramaProjetoInfo> GetConsultaProgramasProjetosByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProgramaProjetoInfo GetProgramaProjetoById(Int32 idProgramaProjeto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AddProgramaProjeto(ProgramaProjetoInfo projeto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateProgramaProjeto(ProgramaProjetoInfo projeto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProgramaProjeto(Int32 idProgramaProjeto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProgramaProjetoCofinanciamentoInfo GetProgramaProjetoCofinanciamentoById(Int32 idProgramaProjetoCofinanciamento);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaProgramaProjetoCofinanciamentoInfo> GetConsultaProgramaProjetoCofinanciamentoByProgramaProjeto(Int32 idProgramaProjeto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AddProgramaProjetoCofinanciamento(ProgramaProjetoCofinanciamentoInfo cofinanciamento);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateProgramaProjetoCofinanciamento(ProgramaProjetoCofinanciamentoInfo cofinanciamento);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProgramaProjetoCofinanciamento(Int32 idProgramaProjetoCofinanciamento, int tipoCofinanciamento);


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaProgramaProjetoExercicioInfo> GetConsultaProgramasProjetosFederaisExercicioByPrefeitura(Int32 idPrefeitura, Int32 idExercicio);
        #endregion

        #region Transferência de Renda
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaTransferenciaRendaInfo> GetConsultaTransferenciasRendaEstadualByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaTransferenciaRendaInfo> GetConsultaTransferenciasRendaFederalByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaTransferenciaRendaInfo> GetConsultaTransferenciasRendaMunicipalByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaTransferenciaRendaInfo> GetConsultaBeneficiosContinuadosByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TransferenciaRendaInfo GetTransferenciaRendaById(Int32 idTransferenciaRenda);        

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AddTransferenciaRenda(TransferenciaRendaInfo projeto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateTransferenciaRenda(TransferenciaRendaInfo projeto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveValoresSaoPauloSolidario(TransferenciaRendaInfo t);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTransferenciaRenda(Int32 idTransferenciaRenda);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ServicoRecursoFinanceiroTransferenciaRendaInfo GetTransferenciaRendaCofinanciamentoById(Int32 idTransferenciaRendaCofinanciamento);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaTransferenciaRendaCofinanciamentoInfo> GetConsultaTransferenciaRendaCofinanciamentoByTransferenciaRenda(Int32 idTransferenciaRenda);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AddTransferenciaRendaCofinanciamento(ServicoRecursoFinanceiroTransferenciaRendaInfo cofinanciamento);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateTransferenciaRendaCofinanciamento(ServicoRecursoFinanceiroTransferenciaRendaInfo cofinanciamento);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTransferenciaRendaCofinanciamento(Int32 idTransferenciaRendaCofinanciamento);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TransferenciaRendaPrevisaoAnualInfo GetPrevisaoAnualByTransferenciaRenda(Int32 idTransferenciaRenda);

        #region Estruturas PETI
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PETIEixoAtuacaoInfo> GetPETIEixosAtuacao();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PETITipoAcaoInfo> GetPETITiposAcao();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PETITipoAcaoInfo> GetPETITiposAcaoByEixoAtuacao(Int32 idEixoAtuacao);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PETISituacaoAcaoInfo> GetPETISituacoesAcao();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PETIIndicadoresInfo GetPETIIndicadoresByMunicipio(Int32 idMunicipio);
        #endregion

        #endregion

        #region Beneficios Eventuais
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaPrefeituraBeneficioEventualInfo> GetConsultaBeneficiosEventuaisByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PrefeituraBeneficioEventualInfo GetBeneficioEventualByPrefeituraETipoBeneficioEventual(Int32 idPrefeitura, Int32 idTipoBeneficioEventual);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveBeneficioEventual(PrefeituraBeneficioEventualInfo obj);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBeneficioEventual(Int32 idPrefeitura, Int32 idTipoBeneficioEventual);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaPrefeituraBeneficioEventualRecursoFinanceiroInfo> GetConsultaBeneficioEventualServicosByBeneficioEventual(Int32 idPrefeituraBeneficioEventual);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AddBeneficioEventualServico(PrefeituraBeneficioEventualServicoInfo servico);
        
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateBeneficioEventualServico(PrefeituraBeneficioEventualServicoInfo servico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBeneficioEventualServico(Int32 idPrefeituraBeneficioEventualServico);        
        #endregion
    }
}
