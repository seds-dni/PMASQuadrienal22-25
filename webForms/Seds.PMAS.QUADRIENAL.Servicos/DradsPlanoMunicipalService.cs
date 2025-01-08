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
using Seds.PMAS.QUADRIENAL.Negocio.Reports;
using Seds.Seguranca.Token;
using System.Threading;
using Microsoft.IdentityModel.Claims;

namespace Seds.PMAS.QUADRIENAL.Servicos
{
    /// <summary>
    /// Serviço Responsável por fornecer informações sobre os Planos Municipais
    /// </summary>
    [ServiceBehavior(Namespace = "http://seds.sp.gov.br/planomunicipal",
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerSession,
    TransactionIsolationLevel = IsolationLevel.ReadCommitted,
    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class DradsPlanoMunicipalService : IDradsPlanoMunicipalService
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public DradsPlanoMunicipalRecursosInfo GetResumoCofinanciamentoDradsBy(Int32 idPrefeitura, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            DradsPlanoMunicipalRecursosInfo cofinanciamentos = new DradsPlanoMunicipalRecursos().GetResumoCofinanciamentoDradsBy(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return cofinanciamentos;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public DradsPlanoMunicipalRecursosReprogramadoInfo GetResumoCofinanciamentoReprogramadoDradsBy(Int32 idPrefeitura, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            DradsPlanoMunicipalRecursosReprogramadoInfo cofinanciamentos = new DradsPlanoMunicipalRecursos().GetResumoCofinanciamentoReprogramadoDradsBy(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return cofinanciamentos;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public DradsPlanoMunicipalDemandasParlamentaresInfo GetResumoCofinanciamentoDemandasDradsBy(Int32 idPrefeitura, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            DradsPlanoMunicipalDemandasParlamentaresInfo cofinanciamentos = new DradsPlanoMunicipalRecursos().GetResumoCofinanciamentoDemandasDradsBy(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return cofinanciamentos;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(Int32 idPrefeitura, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo cofinanciamentos = new DradsPlanoMunicipalRecursos().GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return cofinanciamentos;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public DradsPlanoMunicipalBeneficioProgramaRecursosInfo GetResumoCofinanciamentoBeneficioProgramaDradsBy(Int32 idPrefeitura, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            DradsPlanoMunicipalBeneficioProgramaRecursosInfo cofinanciamentos = new DradsPlanoMunicipalRecursos().GetResumoCofinanciamentoBeneficioProgramaDradsBy(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return cofinanciamentos;
        }

        

    }
}
