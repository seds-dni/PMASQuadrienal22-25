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
    public interface IPrefeituraService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PrefeituraInfo GetPrefeituraById(Int32 id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdatePrefeitura(PrefeituraInfo pre, bool validar = true);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateDesbloquearRecursosIgnorandoOsReprogramadosPrefeituraExercicios(Boolean? desbloquear, int exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateDesbloquearRecursosReprogramadosPrefeituraExercicios(Boolean? desbloquear, int exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PrefeitoInfo GetAtualPrefeitoByPrefeitura(int idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SavePrefeiturasSituacoesQuadros(int idRecurso, int idSituacao, int exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SavePrefeiturasSituacoesQuadrosEFLO(int idRecurso, int idSituacao, int exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PrefeitoInfo> GetPrefeitosAnterioresByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdatePrefeito(PrefeitoInfo prefeito);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddPrefeito(PrefeitoInfo prefeito);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePrefeito(Int32 idPrefeito);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        //void SubstituirPrefeito(Int32 idPrefeitura);
        void SubstituirPrefeito(Int32 idPrefeitura, string dataTerminoNova);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FundoMunicipalInfo GetFMAS(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 SaveFMAS(FundoMunicipalInfo fmas);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 SaveFontesRecursosFMAS(FundoMunicipalInfo fmas, List<PrevisaoOrcamentariaInfo> previsoesOrcamentarias, int exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ConselhoMunicipalInfo GetConselhoMunicipalByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 SaveConselhoMunicipal(ConselhoMunicipalInfo cmas, Boolean ignorarValidacao = false);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConselhoMunicipalPresidenteAnteriorInfo> GetPresidentesAnterioresByConselhoMunicipal(int idConselhoMunicipal);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SavePresidenteAnteriorConselhoMunicipal(ConselhoMunicipalPresidenteAnteriorInfo presidente);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePresidenteAnteriorConselhoMunicipal(ConselhoMunicipalPresidenteAnteriorInfo presidente);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LeiOrcamentariaInfo GetLeiOrcamentariaByPrefeitura(Int32 idPrefeitura, Int32 exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveLeiOrcamentaria(LeiOrcamentariaInfo lei);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OrgaoGestorInfo GetOrgaoGestorByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 SaveOrgaoGestor(OrgaoGestorInfo org);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 SaveOrgaoGestorIdentificacao(OrgaoGestorInfo org);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GestorMunicipalInfo GetAtualGestorMunicipalByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<GestorMunicipalInfo> GetGestoresMunicipaisAnterioresByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateGestorMunicipal(GestorMunicipalInfo gestor);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddGestorMunicipal(GestorMunicipalInfo gestor);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGestorMunicipal(Int32 idGestor);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SubstituirGestorMunicipal(Int32 idPrefeitura, DateTime dataTerminoGestao);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaFluxoInfo> GetConsultaFluxo(List<Int32> idsMunicipios);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ConselhoExistenteInfo GetConselhoExistenteById(int id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<IdentificacaoConselhoExistenteInfo> GetIdentificacaoConselhosExistentesByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateConselhoExistente(ConselhoExistenteInfo conselho);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddConselhoExistente(ConselhoExistenteInfo conselho);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteConselhoExistente(Int32 idConselho);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PrevisaoOrcamentaria2016Info> GetPrevisaoOrcamentaria2016ByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PrevisaoOrcamentariaInfo> GetPrevisaoOrcamentariaByPrefeitura(Int32 idPrefeitura, Int32 exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<PrevisaoOrcamentariaMunicipalInfo> GetPrevisaoOrcamentariaMunicipalByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LeiOrcamentariaInfo GetLeiOrcamentaria2016ByPrefeitura(Int32 idPrefeitura);


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BeneficioEventual2016Info GetBeneficioEventual2016ByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SaoPauloSolidario2016Info GetSaoPauloSolidario2016ByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        //BeneficioEventualAnualInfo GetBeneficioEventualByPrefeitura(Int32 idPrefeitura);
        List<BeneficioEventualAnualInfo> GetBeneficioEventualByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ExecucaoFinanceiraInfo> GetExecucaoFinanceiraByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveExecucaoFinanceira(ComentarioExecucaoFinanceiraInfo comentario
                                  , ExecucaoFinanceiraInfo basica
                                  , ExecucaoFinanceiraInfo reprogramacaoBasica
                                  , ExecucaoFinanceiraInfo especialMedia
                                  , ExecucaoFinanceiraInfo reprogramacaoMedia
                                  , ExecucaoFinanceiraInfo especialAlta
                                  , ExecucaoFinanceiraInfo reprogramacaoAlta
                                  , ExecucaoFinanceiraInfo beneficiosEventuais
                                  , ExecucaoFinanceiraInfo reprogramacaoBeneficiosEventuais
                                  , ExecucaoFinanceiraInfo protecaoSocialEspecial
                                  , ExecucaoFinanceiraInfo programaProjeto
                                  , ExecucaoFinanceiraInfo incentivoGestao
                                  , ExecucaoFinanceiraInfo exercicioAnterior
            );

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<TransferenciaRenda2016Info> GetTransferenciaRenda2016ByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<TransferenciaRendaAnualInfo> GetTransferenciaRendaByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ComentarioExecucaoFinanceiraInfo> GetComentarioExecucaoFinanceiraByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PrefeituraValoresReprogramadosAnoAnteriorInfo GetValoresReprogramadosAnoAnterior(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveComentarioExecucaoFinanceira2016(String comentario, Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaCofinanciamentoEstadualInfo> GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(Int32 idPrefeitura, Int32 IdTipoProtecaoSocial, Int32 exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PrefeituraInfo GetByIdMunicipio(Int32 idMunicipio);

        #region Cronograma de Desembolso
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CronogramaDesembolsoInfo GetCronogramaDesembolsoRedePublicaByPrefeituraETipoProtecaoSocial(Int32 idPrefeitura, Int32 idTipoProtecaoSocial, Int32 exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CronogramaDesembolsoInfo GetCronogramaDesembolsoRedePrivadaByPrefeituraETipoProtecaoSocial(Int32 idPrefeitura, Int32 idTipoProtecaoSocial, Int32 exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveCronogramaDesembolsoRedePublica(CronogramaDesembolsoInfo cronograma, Int32 exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveCronogramaDesembolsoRedePrivada(CronogramaDesembolsoInfo cronograma, Int32 exercicio);
        #endregion


        #region Índice de Gestão Descentralizada
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IndiceGestaoDescentralizadaInfo GetIndiceGestaoDescentralizadaByPrefeitura(Int32 idPrefeitura, int exercicio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveIndiceGestaoDescentralizada(IndiceGestaoDescentralizadaInfo obj);
        #endregion

    }
}
