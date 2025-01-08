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
    public interface IPlanoMunicipalService
    {        
        #region Impressão
        [OperationContract]        
        byte[] GetImpressaoBlocoIByPrefeitura(int idPrefeitura);

        [OperationContract]
        byte[] GetImpressaoBlocoIIByPrefeitura(int idPrefeitura);

        [OperationContract]
        byte[] GetImpressaoBlocoIIIByPrefeitura(int idPrefeitura);

        [OperationContract]
        byte[] GetImpressaoBlocoIVByPrefeitura(int idPrefeitura);

        [OperationContract]
        byte[] GetImpressaoBlocoVByPrefeitura(int idPrefeitura);

        [OperationContract]
        byte[] GetImpressaoBlocoVIByPrefeitura(int idPrefeitura);

        [OperationContract]
        byte[] GetImpressaoBlocoVIIByPrefeitura(int idPrefeitura);
        #endregion

        //[OperationContract]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        //ValidacaoPMASInfo ValidarPlanoMunicipalByPrefeitura(Int32 idPrefeitura, EPerfil perfil, Object dadosExtras = null);

        #region Fluxo
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConsultaPlanoMunicipalHistoricoInfo> GetHistoricoPlanoMunicipalByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ConsultaPlanoMunicipalHistoricoInfo GetHistoricoPlanoMunicipalById(Int32 id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void EnviarPlanoMunicipalParaDrads(Int32 idPrefeitura, String comentario, EPerfil perfil);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void EnviarPlanoMunicipalParaFinalizacao(Int32 idPrefeitura, String comentario, Decimal ValorProtecaoSocialBasica = 0M, Decimal ValorProtecaoSocialMedia = 0M,
                                                 Decimal ValorProtecaoSocialAlta = 0M, Decimal ValorBeneficioEventual = 0M, Decimal ValorSPSolidario = 0M);  

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DevolverPlanoMunicipalDradsParaOrgaoGestor(Int32 idPrefeitura, String motivo);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DevolverPlanoMunicipalCMASParaOrgaoGestor(Int32 idPrefeitura, String motivo);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DevolverPlanoMunicipalDradsParaCAS(Int32 idPrefeitura, String motivo);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void FinalizarPlanoMunicipal(Int32 idPrefeitura, String comentario);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DesbloqueiarPlanoMunicipalParaOrgaoGestor(Int32 idPrefeitura, String motivo);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DesbloqueiarPlanoMunicipalParaCMAS(Int32 idPrefeitura, String motivo);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AutorizarDesbloqueioPlanoMunicipalParaCMAS(Int32 idPrefeitura, String motivo);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AutorizarDesbloqueioPlanoMunicipalParaOrgaoGestor(Int32 idPrefeitura, String motivo, bool? valorReprogramado);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ConselhoMunicipalParecerInfo GetParecerConselhoMunicipalByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveParecerConselhoMunicipal(ConselhoMunicipalParecerInfo parecer);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveParecerConselhoMunicipalSobreAlteracoes(Int32 idPrefeitura, String parecer, Boolean aprovado);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AprovarRejeitarPlanoMunicipal(Int32 idPrefeitura, Boolean aprovado);
        #endregion

        [OperationContract]
        Boolean GetQuadroFinanceiroBloqueado();

        [OperationContract]
        void SaveDesbloqueioQuadroFinanceiro(Boolean desbloqueiar);

        #region alterações
        [OperationContract]
        Boolean ExisteAlteracoesNoPlanoMunicipalByQuadro(Int32 idPrefeitura, Int32 idQuadro);

        [OperationContract]
        Boolean ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(Int32 idPrefeitura, Int32 idQuadro, Int32 idItemCadastro);

        [OperationContract]
        List<ConsultaLogInfo> GetAlteracoesNoPlanoMunicipalByQuadro(Int32 idPrefeitura, Int32 idQuadro);

        [OperationContract]
        List<ConsultaLogInfo> GetAlteracoesNoPlanoMunicipalByQuadroCadastro(Int32 idPrefeitura, Int32 idQuadro, Int32 idItemCadastro);

        [OperationContract]
        List<ConsultaLogInfo> GetAlteracoesNoPlanoMunicipalByPrefeitura(Int32 idPrefeitura);

        [OperationContract]
        List<ConsultaLogInfo> GetAlteracoesNoPlanoMunicipalByPrefeituraUltimaRevisao(Int32 idPrefeitura);
        #endregion
    }
}
