using Seds.PMAS.QUADRIENAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Servicos.Contratos
{
    public interface IInterfacePoliticaService
    {

        #region Educacao
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InterfacePublicaEducacaoInfo GetInterfacePublicaEducacaoById(Int32 id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InterfacePublicaEducacaoInfo GetInterfacePublicaEducacaoByPrefeitura(Int32 id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddInterfacePublicaEducacao(InterfacePublicaEducacaoInfo projeto);


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateInterfacePublicaEducacao(InterfacePublicaEducacaoInfo projeto);
        #endregion

        #region Saude
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InterfacePublicaSaudeInfo GetInterfacePublicaSaudeById(Int32 id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InterfacePublicaSaudeInfo GetInterfacePublicaSaudeByPrefeitura(Int32 id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddInterfacePublicaSaude(InterfacePublicaSaudeInfo projeto);


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateInterfacePublicaSaude(InterfacePublicaSaudeInfo projeto);
        #endregion

        #region Emprego
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InterfacePublicaEmpregoInfo GetInterfacePublicaEmpregoById(Int32 id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InterfacePublicaEmpregoInfo GetInterfacePublicaEmpregoByPrefeitura(Int32 id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Int32 AddInterfacePublicaEmprego(InterfacePublicaEmpregoInfo projeto);


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateInterfacePublicaEmprego(InterfacePublicaEmpregoInfo projeto);
        #endregion


    }
}
