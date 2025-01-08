using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio;
using Seds.PMAS.QUADRIENAL.Servicos.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Transactions;

namespace Seds.PMAS.QUADRIENAL.Servicos
{
    [ServiceBehavior(Namespace = "http://seds.sp.gov.br/interfacespoliticas",
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerSession,
    TransactionIsolationLevel = IsolationLevel.ReadCommitted,
    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class InterfacePoliticaService : IInterfacePoliticaService
    {


        #region educação
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public InterfacePublicaEducacaoInfo GetInterfacePublicaEducacaoById(int id)
        {
            ContextManager.OpenConnection();
            var obj = new InterfacePublicaEducacao().GetById(id);
            ContextManager.CloseConnection();
            return obj; ;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public InterfacePublicaEducacaoInfo GetInterfacePublicaEducacaoByPrefeitura(int id)
        {
            ContextManager.OpenConnection();
            var obj = new InterfacePublicaEducacao().GetByPrefeitura(id);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddInterfacePublicaEducacao(InterfacePublicaEducacaoInfo educacao)
        {
            ContextManager.OpenConnection();
            try
            {
                new InterfacePublicaEducacao().Add(educacao, true);
                ContextManager.CloseConnection();
                return educacao.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateInterfacePublicaEducacao(InterfacePublicaEducacaoInfo educacao)
        {
            ContextManager.OpenConnection();
            try
            {
                new InterfacePublicaEducacao().Update(educacao, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion

        #region Saude
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public InterfacePublicaSaudeInfo GetInterfacePublicaSaudeById(int id)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public InterfacePublicaSaudeInfo GetInterfacePublicaSaudeByPrefeitura(int id)
        {
            ContextManager.OpenConnection();
            var obj = new InterfacePublicaSaude().GetByPrefeitura(id);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddInterfacePublicaSaude(InterfacePublicaSaudeInfo educacao)
        {
            ContextManager.OpenConnection();
            try
            {
                new InterfacePublicaSaude().Add(educacao, true);
                ContextManager.CloseConnection();
                return educacao.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateInterfacePublicaSaude(InterfacePublicaSaudeInfo educacao)
        {
            ContextManager.OpenConnection();
            try
            {
                new InterfacePublicaSaude().Update(educacao, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion

        #region Emprego
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public InterfacePublicaEmpregoInfo GetInterfacePublicaEmpregoById(int id)
        {
            ContextManager.OpenConnection();
            var obj = new InterfacePublicaEmprego().GetById(id);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public InterfacePublicaEmpregoInfo GetInterfacePublicaEmpregoByPrefeitura(int id)
        {
            ContextManager.OpenConnection();
            var obj = new InterfacePublicaEmprego().GetByPrefeitura(id);
            ContextManager.CloseConnection();
            return obj;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddInterfacePublicaEmprego(InterfacePublicaEmpregoInfo obj)
        {
            ContextManager.OpenConnection();
            try
            {
                new InterfacePublicaEmprego().Add(obj, true);
                ContextManager.CloseConnection();
                return obj.Id;
               
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateInterfacePublicaEmprego(InterfacePublicaEmpregoInfo obj)
        {
            ContextManager.OpenConnection();
            try
            {
                new InterfacePublicaEmprego().Update(obj, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #endregion

        #region Alimentacao

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public InterfacePublicaAlimentacaoInfo GetInterfacePublicaAlimentacaoByPrefeitura(int idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new InterfacePublicaAlimentacao().GetByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddInterfacePublicaAlimentacao(InterfacePublicaAlimentacaoInfo obj)
        {
            ContextManager.OpenConnection();
            try
            {
                new InterfacePublicaAlimentacao().Add(obj, true);
                ContextManager.CloseConnection();
                return obj.Id;
               
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateInterfacePublicaAlimentacao(InterfacePublicaAlimentacaoInfo obj)
        {
            ContextManager.OpenConnection();
            try
            {
                new InterfacePublicaAlimentacao().Update(obj, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion

        #region Outras Politicas

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public InterfacePublicaOutraPoliticaInfo GetInterfacePublicaOutraPoliticaById(int id)
        {
            ContextManager.OpenConnection();
            var obj = new InterfacePublicaOutraPolitica().GetById(id);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public InterfacePublicaOutraPoliticaInfo GetInterfacePublicaOutraPoliticaByPrefeitura(int id)
        {
            ContextManager.OpenConnection();
            var obj = new InterfacePublicaOutraPolitica().GetByPrefeitura(id);
            ContextManager.CloseConnection();
            return obj;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddInterfacePublicaOutraPolitica(InterfacePublicaOutraPoliticaInfo obj)
        {
            ContextManager.OpenConnection();
            try
            {
                new InterfacePublicaOutraPolitica().Add(obj, true);
                return obj.Id;
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateInterfacePublicaOutraPolitica(InterfacePublicaOutraPoliticaInfo obj)
        {
            ContextManager.OpenConnection();
            try
            {
                new InterfacePublicaOutraPolitica().Update(obj, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #endregion
    }
}
