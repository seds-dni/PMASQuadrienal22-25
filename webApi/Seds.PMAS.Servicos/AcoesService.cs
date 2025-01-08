using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.ServiceModel;
using System.Transactions;
using Seds.PMAS.Servicos.Contratos;


namespace Seds.PMAS.Servicos
{
    /// <summary>
    /// Serviço Responsável por fornecer informações sobre os Programas Sociais do PMAS 2016
    /// </summary>
    [ServiceBehavior(Namespace = "http://seds.sp.gov.br/acoes",
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerSession,
    TransactionIsolationLevel = IsolationLevel.ReadCommitted,
    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class AcoesService : IAcoesService
    {
        //#region Planejamento de Ações
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public List<ConsultaPrefeituraAcaoPlanejamentoInfo> GetConsultaPlanejamentoAcoesByPrefeitura(Int32 idPrefeitura)
        //{
        //    ContextManager.OpenConnection();
        //    var lst = new PrefeituraAcaoPlanejamento().GetConsultaByPrefeitura(idPrefeitura).ToList();
        //    ContextManager.CloseConnection();
        //    return lst;
        //}
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public List<PrefeituraAcaoPlanejamentoInfo> GetPlanejamentoAcoesByPrefeitura(Int32 idPrefeitura)
        //{
        //    ContextManager.OpenConnection();
        //    var lst = new PrefeituraAcaoPlanejamento().GetByPrefeitura(idPrefeitura).ToList();
        //    ContextManager.CloseConnection();
        //    return lst;
        //}

        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public PrefeituraAcaoPlanejamentoInfo GetAcaoPlanejamentoById(Int32 idPrefeituraAcaoPlanejamento)
        //{
        //    ContextManager.OpenConnection();
        //    var obj = new PrefeituraAcaoPlanejamento().GetById(idPrefeituraAcaoPlanejamento);
        //    ContextManager.CloseConnection();
        //    return obj;
        //}

        ///// <summary>
        ///// Adicionar Ação de Planejamento da Prefeitura
        ///// </summary>
        ///// <param name="acao">Dados da Ação</param>
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Orgão Gestor")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public void AddAcaoPlanejamento(PrefeituraAcaoPlanejamentoInfo acao)
        //{
        //    ContextManager.OpenConnection();
        //    try
        //    {
        //        new PrefeituraAcaoPlanejamento().Add(acao, true);
        //        ContextManager.CloseConnection();
        //    }
        //    catch (Exception ex)
        //    {
        //        ContextManager.CloseConnection();
        //        throw new Exception(Extensions.GetExceptionMessage(ex));
        //    }
        //}

        ///// <summary>
        ///// Atualizar Ação de Planejamento da Prefeitura
        ///// </summary>
        ///// <param name="acao">Dados da Ação</param>
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Orgão Gestor")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public void UpdateAcaoPlanejamento(PrefeituraAcaoPlanejamentoInfo acao)
        //{
        //    ContextManager.OpenConnection();
        //    try
        //    {
        //        new PrefeituraAcaoPlanejamento().Update(acao, true);
        //        ContextManager.CloseConnection();
        //    }
        //    catch (Exception ex)
        //    {
        //        ContextManager.CloseConnection();
        //        throw new Exception(Extensions.GetExceptionMessage(ex));
        //    }
        //}

        ///// <summary>
        ///// Remover Ação de Planejamento da Prefeitura
        ///// </summary>
        ///// <param name="idPrefeituraAcaoPlanejamento">Id do Ação de Planejamento da Prefeitura</param>
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Orgão Gestor")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public void DeleteAcaoPlanejamento(Int32 idPrefeituraAcaoPlanejamento)
        //{
        //    ContextManager.OpenConnection();
        //    try
        //    {
        //        var p = new PrefeituraAcaoPlanejamento();
        //        p.Delete(p.GetById(idPrefeituraAcaoPlanejamento), true);
        //        ContextManager.CloseConnection();
        //    }
        //    catch (Exception ex)
        //    {
        //        ContextManager.CloseConnection();
        //        throw new Exception(Extensions.GetExceptionMessage(ex));
        //    }
        //}
        //#endregion

        //#region Vigilância
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public VigilanciaSocioAssistencialInfo GetVigilanciaByPrefeitura(int idPrefeitura)
        //{
        //    ContextManager.OpenConnection();
        //    var obj = new VigilanciaSocioAssistencial().GetByPrefeitura(idPrefeitura);
        //    ContextManager.CloseConnection();
        //    return obj;
        //}

        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Orgão Gestor")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public void SaveVigilancia(VigilanciaSocioAssistencialInfo obj)
        //{
        //    try
        //    {
        //        TransactionOptions tsOptions = new TransactionOptions();
        //        tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
        //        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
        //        {
        //            ContextManager.OpenConnection();
        //            if (obj.Id == 0)
        //                new VigilanciaSocioAssistencial().Add(obj, true);
        //            else
        //                new VigilanciaSocioAssistencial().Update(obj, true);

        //            new PrefeituraVigilanciaMonitoramentoAvaliacao().ConsistirExistencia(obj.IdPrefeitura, true);
        //            ContextManager.CloseConnection();
        //            ts.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ContextManager.CloseConnection();
        //        throw new Exception(Extensions.GetExceptionMessage(ex));
        //    }
        //}
        //#endregion

        //#region Avaliação
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public AvaliacaoInfo GetAvaliacaoByPrefeitura(int idPrefeitura)
        //{
        //    ContextManager.OpenConnection();
        //    var obj = new Avaliacao().GetByPrefeitura(idPrefeitura);
        //    ContextManager.CloseConnection();
        //    return obj;
        //}

        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Orgão Gestor")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public void SaveAvaliacao(AvaliacaoInfo obj)
        //{
        //    try
        //    {
        //        TransactionOptions tsOptions = new TransactionOptions();
        //        tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
        //        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
        //        {
        //            ContextManager.OpenConnection();
        //            if (obj.Id == 0)
        //                new Avaliacao().Add(obj, true);
        //            else
        //                new Avaliacao().Update(obj, true);

        //            new PrefeituraVigilanciaMonitoramentoAvaliacao().ConsistirExistencia(obj.IdPrefeitura, true);
        //            ContextManager.CloseConnection();
        //            ts.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ContextManager.CloseConnection();
        //        throw new Exception(Extensions.GetExceptionMessage(ex));
        //    }
        //}
        //#endregion

        //#region Monitoramento
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public MonitoramentoInfo GetMonitoramentoByPrefeitura(int idPrefeitura)
        //{
        //    ContextManager.OpenConnection();
        //    var obj = new Monitoramento().GetByPrefeitura(idPrefeitura);
        //    ContextManager.CloseConnection();
        //    return obj;
        //}

        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Orgão Gestor")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public void SaveMonitoramento(MonitoramentoInfo obj)
        //{
        //    try
        //    {
        //        TransactionOptions tsOptions = new TransactionOptions();
        //        tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
        //        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
        //        {
        //            ContextManager.OpenConnection();
        //            if (obj.Id == 0)                    
        //                new Monitoramento().Add(obj, true);                                            
        //            else                    
        //                new Monitoramento().Update(obj, true);                        

        //            new PrefeituraVigilanciaMonitoramentoAvaliacao().ConsistirExistencia(obj.IdPrefeitura, true);
        //            ContextManager.CloseConnection();
        //            ts.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ContextManager.CloseConnection();
        //        throw new Exception(Extensions.GetExceptionMessage(ex));
        //    }
        //}
        //#endregion

        //#region Aspectos Gerais
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public PrefeituraVigilanciaMonitoramentoAvaliacaoInfo GetAspectosGeraisVigilanciaMonitoramentoAvaliacaoByPrefeitura(int idPrefeitura)
        //{
        //    ContextManager.OpenConnection();
        //    var obj = new PrefeituraVigilanciaMonitoramentoAvaliacao().GetByPrefeitura(idPrefeitura);
        //    ContextManager.CloseConnection();
        //    return obj;
        //}

        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public Boolean PrefeituraPossuiVigilanciaMonitoramentoAvalicao(Int32 idPrefeitura)
        //{
        //    ContextManager.OpenConnection();
        //    var possui = new PrefeituraVigilanciaMonitoramentoAvaliacao().VerificarExistenciaNoMunicipio(idPrefeitura);
        //    ContextManager.CloseConnection();
        //    return possui;
        //}

        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Orgão Gestor")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public void SaveAspectosGeraisVigilanciaMonitoramentoAvaliacao(PrefeituraVigilanciaMonitoramentoAvaliacaoInfo obj)
        //{
        //    ContextManager.OpenConnection();
        //    try
        //    {
        //        if (obj.Id == 0)
        //        {
        //            new PrefeituraVigilanciaMonitoramentoAvaliacao().Add(obj, true);
        //            ContextManager.CloseConnection();
        //            return;
        //        }

        //        new PrefeituraVigilanciaMonitoramentoAvaliacao().Update(obj, true);
        //        ContextManager.CloseConnection();
        //    }
        //    catch (Exception ex)
        //    {
        //        ContextManager.CloseConnection();
        //        throw new Exception(Extensions.GetExceptionMessage(ex));
        //    }
        //}
        //#endregion
    }
}
