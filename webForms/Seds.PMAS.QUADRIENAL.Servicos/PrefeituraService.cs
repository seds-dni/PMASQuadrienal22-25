using System;
using System.Collections.Generic;
using System.Linq;
using Seds.PMAS.QUADRIENAL.Servicos.Contratos;
using System.ServiceModel;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio;
using System.Security.Permissions;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.Servicos
{
    /// <summary>
    /// Serviço Responsável por fornecer informações sobre as 645 Prefeituras do Estado de São Paulo
    /// </summary>
    [ServiceBehavior(Namespace = "http://seds.sp.gov.br/prefeitura",
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerSession,
    TransactionIsolationLevel = IsolationLevel.ReadCommitted,
    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class PrefeituraService : IPrefeituraService
    {
        #region Prefeitura
        public List<PrefeituraInfo> GetPrefeituras()
        {
            ContextManager.OpenConnection();
            var lst = new Prefeitura().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Prefeitura através do Identificador
        /// </summary>SaveComentarioExecucaoFinanceira2013
        /// <param name="id">Id da Prefeitura</param>
        /// <returns>Dados da Prefeitura</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public PrefeituraInfo GetPrefeituraById(int id)
        {
            ContextManager.OpenConnection();
            var obj = new Prefeitura().GetById(id);
            ContextManager.CloseConnection();
            return obj;
        }



        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public PrefeituraInfo GetByIdMunicipio(int idMunicipio)
        {
            ContextManager.OpenConnection();
            var obj = new Prefeitura().GetByMunicipio(idMunicipio);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public PrefeituraInfo GetByMunicipioQuadrosFinanceiros(int idMunicipio)
        {
            ContextManager.OpenConnection();
            var obj = new Prefeitura().GetByMunicipioQuadrosFinanceiros(idMunicipio);
            ContextManager.CloseConnection();
            return obj;
        }
        


        /// <summary>
        /// Atualizar Dados da Prefeitura
        /// </summary>
        /// <param name="pre">Dados da Prefeitura</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdatePrefeitura(PrefeituraInfo pre, bool validar = true)
        {
            ContextManager.OpenConnection();
            try
            {
                new Prefeitura().Update(pre, true, validar);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        //public bool ValidarPlanoMunicipalPendenciaOrgaoGestor(Int32 idPrefeitura, EPerfil perfil)  
        //{
         //return new ValidacaoPMAS().PlanoMunicipalPossuiPendenciaOrgaoGestor(idPrefeitura, perfil);
        //}


        #region Prefeitura exercicios bloqueioDesbloqueio
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateDesbloquearRecursosIgnorandoOsReprogramadosPrefeituraExercicios(Boolean? desbloquear, int exercicio)
        {
            ContextManager.OpenConnection();
            try
            {
                new PrefeituraExercicioBloqueio().UpdateDesbloqueiaTodosMenosReprogramados(desbloquear, exercicio);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion

         #region Prefeitura exercicios bloqueioDesbloqueio
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateDesbloquearRecursosReprogramadosPrefeituraExercicios(Boolean? desbloquear, int exercicio)
        {
            ContextManager.OpenConnection();
            try
            {
                new PrefeituraExercicioBloqueio().UpdateDesbloqueiaReprogramados(desbloquear, exercicio);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion


        #region Prefeitura exercicios bloqueioDesbloqueio Demandas Parlamentares
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateDesbloquearDemandasParlamentaresPrefeituraExercicios(Boolean? desbloquear, int exercicio)
        {
            ContextManager.OpenConnection();
            try
            {
                new PrefeituraExercicioBloqueio().UpdateDesbloqueiaDemandasParlamentares(desbloquear, exercicio);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion

        

        #endregion

        #region Prefeitos
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public PrefeitoInfo GetAtualPrefeitoByPrefeitura(int idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new Prefeito().GetByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrefeitoInfo> GetPrefeitosAnterioresByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new Prefeito().GetAnterioresByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdatePrefeito(PrefeitoInfo prefeito)
        {
            ContextManager.OpenConnection();
            try
            {
                new Prefeito().Update(prefeito, true);
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
        public Int32 AddPrefeito(PrefeitoInfo prefeito)
        {
            ContextManager.OpenConnection();
            try
            {
                new Prefeito().Add(prefeito, true);
                ContextManager.CloseConnection();
                return prefeito.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePrefeito(Int32 idPrefeito)
        {
            ContextManager.OpenConnection();
            try
            {
                new Prefeito().Delete(new Prefeito().GetById(idPrefeito), true);
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
        public void SubstituirPrefeito(Int32 idPrefeitura, string dataTerminoNova = "")
        {
            ContextManager.OpenConnection();
            try
            {
                new Prefeito().Substituir(idPrefeitura, dataTerminoNova);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion

        #region Fundo Municipal
         /// <summary>
        /// Atualizar Dados do Fundo Municipal
        /// </summary>
        /// <param name="fmas">Dados do Fundo Municipal</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 SaveGestorFundoMunicipal(GestorFundoMunicipalInfo gestorFMAS) 
        {
            ContextManager.OpenConnection();
            try
            {
                new GestorFundoMunicipal().Add(gestorFMAS, true);
                ContextManager.CloseConnection();
                return gestorFMAS.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateGestorFundoMunicipal(GestorFundoMunicipalInfo gestor)
        {
            ContextManager.OpenConnection();
            try
            {
                new GestorFundoMunicipal().Update(gestor, true);
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
        public void DeleteGestorFundoMunicipal(Int32 idGestor)
        {
            ContextManager.OpenConnection();
            try
            {
                new GestorFundoMunicipal().Delete(new GestorFundoMunicipal().GetById(idGestor), true);
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
        public void SubstituirGestorFundoMunicipal(Int32 idPrefeitura, DateTime dataTerminoGestao)
        {
            ContextManager.OpenConnection();
            try
            {
                new GestorFundoMunicipal().Substituir(idPrefeitura, dataTerminoGestao);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GestorFundoMunicipalInfo GetGestorFundoMunicipalByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new GestorFundoMunicipal().GetByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return lst;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<GestorFundoMunicipalInfo> GetGestoresFundoMunicipalAnterioresByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new GestorFundoMunicipal().GetAnterioresByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Fundo Muninicipal da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Fundo Municipal</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public FundoMunicipalInfo GetFMAS(int idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new FundoMunicipal().GetByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }


        /// <summary>
        /// Atualizar Dados do Fundo Municipal
        /// </summary>
        /// <param name="fmas">Dados do Fundo Municipal</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 SaveFMAS(FundoMunicipalInfo fmas)
        {
            try
            {
                ContextManager.OpenConnection();
                if (fmas.Id == 0)
                {
                    new FundoMunicipal().Add(fmas, true);
                }
                else
                {
                    new FundoMunicipal().Update(fmas, true);
                }
                return fmas.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
            finally
            {
                ContextManager.CloseConnection();
            }
        }

        /// <summary>
        /// Atualizar Dados do Fundo Municipal
        /// </summary>
        /// <param name="fmas">Dados do Fundo Municipal</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 SaveFontesRecursosFMAS(FundoMunicipalInfo fmas, List<PrevisaoOrcamentariaInfo> previsoesOrcamentarias, int exercicio)
        {
            try
            {
                ContextManager.OpenConnection();
                if (fmas.Id == 0)
                {
                    new FundoMunicipal().AddFontesRecursosFMAS(fmas, previsoesOrcamentarias, exercicio, true);
                }
                else
                {
                    new FundoMunicipal().UpdateFontesRecursosFMAS(fmas, previsoesOrcamentarias, exercicio, true);
                }

                return fmas.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
            finally
            {
                ContextManager.CloseConnection();
            }
        }

        #endregion

        #region Conselho Municipal
        /// <summary>
        /// Selecionar Conselho Muninicipal da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Conselho Municipal</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ConselhoMunicipalInfo GetConselhoMunicipalByPrefeitura(int idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new ConselhoMunicipal().GetByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Atualizar Dados do Conselho Municipal
        /// </summary>
        /// <param name="cmas">Dados do Conselho Municipal</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@CMAS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 SaveConselhoMunicipal(ConselhoMunicipalInfo cmas, Boolean ignorarValidacao = false)
        {
            ContextManager.OpenConnection();
            try
            {
                if (cmas.Id == 0)
                {
                    new ConselhoMunicipal().Add(cmas, true, ignorarValidacao);
                    ContextManager.CloseConnection();
                    return cmas.Id;
                }

                new ConselhoMunicipal().Update(cmas, true, ignorarValidacao);
                ContextManager.CloseConnection();
                return cmas.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Selecionar Presidentes Anteriores do Conselho Muninicipal da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id do Conselho Municipal</param>
        /// <returns>Lista de Presidentes Anteriores do Conselho Municipal</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConselhoMunicipalPresidenteAnteriorInfo> GetPresidentesAnterioresByConselhoMunicipal(int idConselhoMunicipal)
        {
            ContextManager.OpenConnection();
            var lst = new ConselhoMunicipalPresidenteAnterior().GetByConselhoMunicipal(idConselhoMunicipal).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@CMAS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SavePresidenteAnteriorConselhoMunicipal(ConselhoMunicipalPresidenteAnteriorInfo presidente)
        {
            ContextManager.OpenConnection();
            try
            {
                new ConselhoMunicipalPresidenteAnterior().Add(presidente, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@CMAS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePresidenteAnteriorConselhoMunicipal(ConselhoMunicipalPresidenteAnteriorInfo presidente)
        {
            ContextManager.OpenConnection();
            try
            {
                new ConselhoMunicipalPresidenteAnterior().Delete(presidente, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #endregion

        #region Orgao Gestor

        /// <summary>
        /// Selecionar Órgão Gestor da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Órgão Gestor</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public OrgaoGestorInfo GetOrgaoGestorByPrefeitura(int idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new OrgaoGestor().GetByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public OrgaoGestorInfo GetOrgaoGestorByPrefeituraExrcicio(int idPrefeitura,int exercicio)
        {
            ContextManager.OpenConnection();
            var obj = new OrgaoGestor().GetByPrefeituraExercicio(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Órgão Gestor da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Órgão Gestor</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public OrgaoGestorInfo GetOrgaoGestorByPrefeituraByExercicio(int idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new OrgaoGestor().GetByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }


        /// <summary>
        /// Atualizar Dados do Órgão Gestor
        /// </summary>
        /// <param name="org">Dados do Órgão Gestor</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 SaveOrgaoGestor(OrgaoGestorInfo org)
        {
            ContextManager.OpenConnection();
            try
            {
                if (org.Id == 0)
                {
                    new OrgaoGestor().Add(org, true);
                    ContextManager.CloseConnection();
                    return org.Id;
                }

                new OrgaoGestor().Update(org, true);
                ContextManager.CloseConnection();
                return org.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }


        /// <summary>
        /// Atualizar Dados do Órgão Gestor
        /// </summary>
        /// <param name="org">Dados do Órgão Gestor</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 SaveOrgaoGestorIdentificacao(OrgaoGestorInfo org)
        {
            ContextManager.OpenConnection();
            try
            {
                if (org.Id == 0)
                {
                    new OrgaoGestor().AddIdentificacao(org, true);
                    ContextManager.CloseConnection();
                    return org.Id;
                }

                new OrgaoGestor().UpdateIdentificacao(org, true);
                ContextManager.CloseConnection();
                return org.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Atualizar Dados do Órgão Gestor
        /// </summary>
        /// <param name="org">Dados do Órgão Gestor</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 SaveOrgaoGestorPorExercicio(OrgaoGestorInfo org)
        {
            ContextManager.OpenConnection();
            try
            {
                if (org.Id == 0)
                {
                    new OrgaoGestor().Add(org, true);
                    ContextManager.CloseConnection();
                    return org.Id;
                }

                new OrgaoGestor().Update(org, true);
                ContextManager.CloseConnection();
                return org.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }


        #endregion

        #region Gestor Municipal
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TipoGestorMunicipalInfo> GetTiposGestoresMunicipal() 
        {
            ContextManager.OpenConnection();
            var lst = new TipoGestorMunicipal().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GestorMunicipalInfo GetAtualGestorMunicipalByPrefeitura(int idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new GestorMunicipal().GetByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GestorMunicipalInfo GetAtualGestorMunicipalPrestacaoDeContas(int idPrefeitura, int idUsuario)
        {
            ContextManager.OpenConnection();
            var obj = new GestorMunicipal().GetGestorPrestacaoContas(idPrefeitura, idUsuario);
            ContextManager.CloseConnection();
            return obj;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<GestorMunicipalInfo> GetGestoresMunicipaisAnterioresByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new GestorMunicipal().GetAnterioresByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateGestorMunicipal(GestorMunicipalInfo gestor)
        {
            ContextManager.OpenConnection();
            try
            {
                new GestorMunicipal().Update(gestor, true);
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
        public Int32 AddGestorMunicipal(GestorMunicipalInfo gestor)
        {
            ContextManager.OpenConnection();
            try
            {
                new GestorMunicipal().Add(gestor, true);
                ContextManager.CloseConnection();
                return gestor.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGestorMunicipal(Int32 idGestor)
        {
            ContextManager.OpenConnection();
            try
            {
                new GestorMunicipal().Delete(new GestorMunicipal().GetById(idGestor), true);
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
        public void SubstituirGestorMunicipal(Int32 idPrefeitura, DateTime dataTerminoGestao)
        {
            ContextManager.OpenConnection();
            try
            {
                new GestorMunicipal().Substituir(idPrefeitura, dataTerminoGestao);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaFluxoInfo> GetConsultaFluxo(List<Int32> idsMunicipios)
        {
            ContextManager.OpenConnection();
            var lst = new Prefeitura().GetConsultaFluxo(idsMunicipios);
            ContextManager.CloseConnection();
            return lst;
        }

        #region Conselhos Existentes
        /// <summary>
        /// Selecionar Conselho Existente no Município através do identificador
        /// </summary>
        /// <param name="id">Id do Conselho</param>
        /// <returns>Dados do Conselho</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ConselhoExistenteInfo GetConselhoExistenteById(int id)
        {
            ContextManager.OpenConnection();
            var obj = new ConselhoExistente().GetById(id);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar identificação dos conselhos existentes no Município
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Identificação dos Conselhos</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<IdentificacaoConselhoExistenteInfo> GetIdentificacaoConselhosExistentesByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new ConselhoExistente().GetIdentificacaoByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Atualizar Dados do Conselho Existente
        /// </summary>
        /// <param name="conselho">Dados do Conselho</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateConselhoExistente(ConselhoExistenteInfo conselho)
        {
            ContextManager.OpenConnection();
            try
            {
                new ConselhoExistente().Update(conselho, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Adicionar um Conselho Existente no Município
        /// </summary>
        /// <param name="conselho">Dados do Conselho</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddConselhoExistente(ConselhoExistenteInfo conselho)
        {
            ContextManager.OpenConnection();
            try
            {
                new ConselhoExistente().Add(conselho, true);
                ContextManager.CloseConnection();
                return conselho.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Remover um Conselho Existente no Município
        /// </summary>
        /// <param name="idConselho">Id do Conselho</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteConselhoExistente(Int32 idConselho)
        {
            ContextManager.OpenConnection();
            try
            {
                new ConselhoExistente().Delete(new ConselhoExistente().GetById(idConselho), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Adicionar o Presidente do Conselho Existente no Município
        /// </summary>
        /// <param name="presidente">Dados do Presidente</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddPresidenteConselhoExistente(ConselhoMunicipalExistentePresidenteInfo presidente)
        {
            ContextManager.OpenConnection();
            try
            {
                new ConselhoExistentePresidente().Add(presidente, true);
                ContextManager.CloseConnection();
                return presidente.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Adicionar o Presidente do Conselho Existente no Município
        /// </summary>
        /// <param name="presidente">Dados do Presidente</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SubstituirPresidenteConselhoExistente(ConselhoMunicipalExistentePresidenteInfo presidente)
        {
            ContextManager.OpenConnection();
            try
            {
                new ConselhoExistentePresidente().Substituir(presidente, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }

        }

        /// <summary>
        /// Atualizar Dados do Conselho Existente
        /// </summary>
        /// <param name="conselho">Dados do Conselho</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdatePresidenteConselhoExistente(ConselhoMunicipalExistentePresidenteInfo presidente)
        {
            ContextManager.OpenConnection();
            try
            {
                new ConselhoExistentePresidente().Update(presidente, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Remover um Presidente do Conselho Existente no Município
        /// </summary>
        /// <param name="idPresidente"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePresidenteConselhoExistente(Int32 idPresidente)
        {
            ContextManager.OpenConnection();
            try
            {
                new ConselhoExistentePresidente().Delete(new ConselhoExistentePresidente().GetById(idPresidente), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ConselhoMunicipalExistentePresidenteInfo GetPresidenteConselhoByIdConselho(Int32 idConselho)
        {
            ContextManager.OpenConnection();
            var obj = new ConselhoExistentePresidente().GetPresidenteConselhoByIdConselho(idConselho);
            ContextManager.CloseConnection();
            return obj;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConselhoMunicipalExistentePresidenteInfo> GetPresidenteConselhoByIdConselhoCollection(Int32 idConselho)
        {
            ContextManager.OpenConnection();
            var obj = new ConselhoExistentePresidente().GetPresidenteConselhoByIdConselhoCollection(idConselho).ToList();
            ContextManager.CloseConnection();
            return obj;
        }


        
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConselhoMunicipalExistentePresidenteInfo> GetPresidentesByIdConselhoExistente(Int32 idConselho)
        {
            ContextManager.OpenConnection();
            var lst = new ConselhoExistentePresidente().GetPresidentesByIdConselhoExistente(idConselho).ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConselhoMunicipalExistentePresidenteInfo> GetPresidentesConselhoExistenteByIdConselhoPrefeitura(Int32 idConselho, Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new ConselhoExistentePresidente().GetPresidentesConselhoExistenteByIdConselhoPrefeitura(idConselho, idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        #endregion

        #region AtualizacaoAnual
        public List<PrefeituraAtualizacaoAnualInfo> GetPrefeituraAtualizacaoAnual()
        {
            ContextManager.OpenConnection();
            var lst = new PrefeituraAtualizacaoAnual().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        public void SavePrefeituraAtualizacaoAnual(PrefeituraAtualizacaoAnualInfo PrefeituraAtualizacaoAnualInfo, bool commit)
        {
            ContextManager.OpenConnection();
            try
            {
                new PrefeituraAtualizacaoAnual().Update(PrefeituraAtualizacaoAnualInfo, commit);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #endregion


        #region Recursos Financeiros

        /// <summary>
        /// Selecionar Lei Orçamentária Municipal da Assistência Social referente à 2016
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados da Lei Orçamentaria</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public LeiOrcamentariaInfo GetLeiOrcamentariaByPrefeitura(int idPrefeitura, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            var obj = new RecursosFinanceiros().GetLeiOrcamentariaByPrefeitura(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Atualizar Dados da Lei Orçamentária para 2016
        /// </summary>
        /// <param name="pre">Dados da Prefeitura</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveLeiOrcamentaria(LeiOrcamentariaInfo lei)
        {
            ContextManager.OpenConnection();
            try
            {

                if (new RecursosFinanceiros().GetLeiOrcamentariaByPrefeitura(lei.IdPrefeitura, lei.Exercicio) == null)
                {
                    new RecursosFinanceiros().AddLeiOrcamentaria(lei, true);
                    ContextManager.CloseConnection();
                    return;
                }

                new RecursosFinanceiros().UpdateLeiOrcamentaria(lei, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Seleciona Previsão Orçamentária de 2013 do Município
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrevisaoOrcamentaria2016Info> GetPrevisaoOrcamentaria2016ByPrefeitura(Int32 idPrefeitura)

        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrevisaoOrcamentaria2016(idPrefeitura);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrevisaoOrcamentariaInfo> GetPrevisaoOrcamentariaByPrefeitura(Int32 idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrevisaoOrcamentaria(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return lst;
        }



        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrevisaoOrcamentariaMunicipalInfo> GetPrevisaoOrcamentariaMunicipalByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrevisaoOrcamentariaMunicipal(idPrefeitura);
            ContextManager.CloseConnection();
            return lst;
        }


        /// <summary>
        /// Selecionar Lei Orçamentária Municipal da Assistência Social referente à 2014
        /// </summary>
        /// <param name="idPrefeitura"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public LeiOrcamentariaInfo GetLeiOrcamentaria2016ByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new RecursosFinanceiros().GetLeiOrcamentaria2016ByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar valores do Benefício Eventual referente a 2014
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Valores do Benefício Eventual</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public BeneficioEventual2016Info GetBeneficioEventual2016ByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new RecursosFinanceiros().GetBeneficioEventual2016(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        //public BeneficioEventualAnualInfo GetBeneficioEventualByPrefeitura(Int32 idPrefeitura)
        public List<BeneficioEventualAnualInfo> GetBeneficioEventualByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new RecursosFinanceiros().GetBeneficioEventual(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Valores da Execução Financeira da Prefeitura referente ao ano de 2013
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Valores da Execução Financeira</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ExecucaoFinanceiraInfo> GetExecucaoFinanceiraByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetExecucaoFinanceiraByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<LocaisExecucaoPrestacaoDeContasInfo> GetLocaisExecucaoPrestacaoDeContas(int idPrefeitura, int idTipoProtecao,int exercicio) 
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetLocaisDeExecucaoPrestacaoDeContas(idPrefeitura, idTipoProtecao,exercicio).ToList();
            ContextManager.CloseConnection();
            return lst; 
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<LocaisExecucaoPrestacaoDeContasInfo> GetLocaisExecucaoPrestacaoDeContasDespesas(int idServicosRecursosFinanceiros,int idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetLocaisDeExecucaoPrestacaoDeContasDespesas(idServicosRecursosFinanceiros,idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ComentarioExecucaoFinanceiraCMASInfo> GetComentarioCMAS(int idPrefeitura,int exercicio) 
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetComentarioCMAS(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ComentarioPrestacaoDeContasCMASInfo> GetComentarioPrestacaodeContasCMAS(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetComentarioPrestacaoDeContasCMAS(idPrefeitura,exercicio).ToList();  //.GetComentarioPrestacaoDeContasCMAS(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ComentarioPrestacaoDeContasInfo> GetComentarioPrestacaodeContas(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetComentarioPrestacaoDeContas(idPrefeitura, exercicio).ToList();  //.GetComentarioPrestacaoDeContasCMAS(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ComentarioPrestacaoDeContasDRADSInfo> GetComentarioPrestacaodeContasDRADS(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetComentarioPrestacaoDeContasDRADS(idPrefeitura, exercicio).ToList();  //.GetComentarioPrestacaoDeContasCMAS(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<DeliberacaoCMASInfo> GetDeliberacaoCMAS(int idPrefeitura,int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetDeliberacao(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<DeliberacaoPrestacaoDeContasCMASInfo> GetDeliberacaoPrestacaoDeContasCMAS(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetDeliberacaoPrestacaoDeContasCMAS(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<DeliberacaoPrestacaoDeContasDRADSInfo> GetDeliberacaoPrestacaoDeContasDRADS(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetDeliberacaoPrestacaoDeContasDRADS(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;

        }
        
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ProgramaProjetoPrestacaoContasInfo> GetPrestacaoDeContasProgramaProjeto(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrestacaoDeContasProgramaProjeto(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ProgramaProjetoPrestacaoContasInfo> GetPrestacaoDeContasProgramaProjetoDespesas(int idProgramaProjeto,int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrestacaoDeContasProgramaProjetoDespesas(idProgramaProjeto,idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrestacaoDeContasBeneficiosEventuaisInfo> GetPrestacaoDeContasBeneficiosEventuais(int idPrefeitura, int exercicio)
        { 
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrestacaoDeContasBeneficiosEventuais(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrestacaoDeContasBeneficiosEventuaisInfo> GetPrestacaoDeContasBeneficiosEventuaisDespesas(int idBeneficiosEventuais,int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrestacaoDeContasBeneficiosEventuaisDespesas(idBeneficiosEventuais,idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrestacaoDeContasDespesasInfo> GetPrestacaoDeContasDespesas(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrestacaoDeContasDespesas(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            
            return lst;

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrestacaoDeContasAplicacoesFinanceirasInfo> GetPrestacaoDeContasAplicacoesFinanceiras(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrestacaoDeContasAplicacoesFinanceiras(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();

            return lst;

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<QuestoesCMASinfo> GetQuestoesPrestacaoDeContasCMAS(int idPrefeitura, int exercicio) 
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetQuestionarioPrestacaoDeContasCMAS(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<QuestoesDRADSInfo> GetQuestoesPrestacaoDeContasDRADS(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetQuestionarioPrestacaoDeContasDRADS(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<HistoricoPrestacaoDeContasInfo> GetHistoricoPrestacaoDeContas(int idPrefeitura, int idPerfil ,int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetHistoricoPrestacaoDeContas(idPrefeitura, idPerfil,exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<HistoricoPrestacaoDeContasInfo> GetHistoricoPrestacaoDeContasDetalhes(int idPrefeitura,  int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetHistoricoPrestacaoDeContasDetalhes(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<HistoricoPrestacaoDeContasInfo> GetHistoricoPrestacaoDeContasID(int id)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetHistoricoPrestacaoDeContasID(id).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrestacaoDeContasExecucaoFisicaInfo> GetPrestacaoDecontasExecucaoFisica(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrestacaoDeContasExecucaoFisica(idPrefeitura,exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrestacaoDeContasExecucaoFisicaProgramasProjetosInfo> GetPrestacaoDecontasExecucaoFisicaProgramaProjeto(int idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetPrestacaoDeContasExecucaoFisicaProgramaProjeto(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Salvar Dados da Execução Financeira de 2012
        /// </summary>
        /// <param name="execucaoFinanceira">Valores da Execução Financeira</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveExecucaoFinanceira(ComentarioExecucaoFinanceiraInfo comentario
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
            , ExecucaoFinanceiraInfo ExercicioAnterior
            )
        {

            try
            {
                TransactionOptions tsOptions = new TransactionOptions();
                tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
                {
                    ContextManager.OpenConnection();
                    new RecursosFinanceiros().SaveExecucaoFinanceira(basica, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(reprogramacaoBasica, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(especialMedia, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(reprogramacaoMedia, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(especialAlta, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(reprogramacaoAlta, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(beneficiosEventuais, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(reprogramacaoBeneficiosEventuais, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(protecaoSocialEspecial, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(programaProjeto, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(incentivoGestao, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(ExercicioAnterior, true);
                    new RecursosFinanceiros().SaveComentarioExecucaoFinanceira(comentario, basica.IdPrefeitura, true);
                    ts.Complete();
                }

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
        public void SavePrestacaoDeContas(ComentarioPrestacaoDeContasInfo comentario
                                             , ExecucaoFinanceiraInfo basica
                                             , ExecucaoFinanceiraInfo reprogramacaoBasica
                                             , ExecucaoFinanceiraInfo basicaDemandas
                                             , ExecucaoFinanceiraInfo basicaReprogramacaoDemandas
                                             , ExecucaoFinanceiraInfo especialMedia
                                             , ExecucaoFinanceiraInfo reprogramacaoMedia
                                             , ExecucaoFinanceiraInfo mediaDemandas
                                             , ExecucaoFinanceiraInfo mediaReprogramacaoDemandas
                                             , ExecucaoFinanceiraInfo especialAlta
                                             , ExecucaoFinanceiraInfo reprogramacaoAlta
                                             , ExecucaoFinanceiraInfo altaDemandas
                                             , ExecucaoFinanceiraInfo altaReprogramacaoDemandas
                                             , ExecucaoFinanceiraInfo beneficiosEventuais
                                             , ExecucaoFinanceiraInfo reprogramacaoBeneficiosEventuais
                                             , ExecucaoFinanceiraInfo beneficiosEventuaisDemandas
                                             , ExecucaoFinanceiraInfo beneficiosEventuaisReprogramacaoDemandas
                                             , ExecucaoFinanceiraInfo programaProjeto
                                             , ExecucaoFinanceiraInfo reprogramacaoProgramaProjeto
                                             , ExecucaoFinanceiraInfo exercicioAnterior
                                             , ExecucaoFinanceiraInfo protecaoEspecial
                                             , ExecucaoFinanceiraInfo incentivo
            )
        {

            try
            {
                TransactionOptions tsOptions = new TransactionOptions();
                tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
                {
                    ContextManager.OpenConnection();
                    new RecursosFinanceiros().SaveExecucaoFinanceira(basica, true);                    
                    new RecursosFinanceiros().SaveExecucaoFinanceira(reprogramacaoBasica, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(basicaDemandas, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(basicaReprogramacaoDemandas, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(especialMedia, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(reprogramacaoMedia, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(mediaDemandas, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(mediaReprogramacaoDemandas, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(especialAlta, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(reprogramacaoAlta, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(altaDemandas, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(altaReprogramacaoDemandas, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(beneficiosEventuais, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(reprogramacaoBeneficiosEventuais, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(beneficiosEventuaisDemandas, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(beneficiosEventuaisReprogramacaoDemandas, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(programaProjeto, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(reprogramacaoProgramaProjeto, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(exercicioAnterior, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(protecaoEspecial, true);
                    new RecursosFinanceiros().SaveExecucaoFinanceira(incentivo, true);
                    new RecursosFinanceiros().SaveComentarioPrestacaoDeContas(comentario, basica.IdPrefeitura, true);
                    ts.Complete();
                }

                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Salvar Dados da Execução Financeira de 2012
        /// </summary>
        /// <param name="execucaoFinanceira">Valores da Execução Financeira</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveComentariosEDeliberacaoCMAS(ComentarioExecucaoFinanceiraCMASInfo comentatioCMAS) 
        {

            int idPrefeitura = comentatioCMAS.IdPrefeitura;
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SaveComentarioExecucaoFinanceiraCMAS(comentatioCMAS, idPrefeitura, true);

                ts.Complete();
            }
            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SavePrestacaoDeContasDespesas(PrestacaoDeContasDespesasInfo despesas)
        {

            int idPrefeitura = despesas.IdPrefeitura;
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SavePrestacaoDeContasDespesas(despesas, true);

                ts.Complete();
            }
            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SavePrestacaoDeContasAplicacoesFinanceiras(PrestacaoDeContasAplicacoesFinanceirasInfo aplicacoesFinanceiras)
        {

            int idPrefeitura = aplicacoesFinanceiras.IdPrefeitura;
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SavePrestacaoDeContasAplicacoesFinanceiras(aplicacoesFinanceiras, true);

                ts.Complete();
            }
            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SavePrestacaoDeContasExecucaoFisica(PrestacaoDeContasExecucaoFisicaInfo execucao)
        {

            int idPrefeitura = execucao.IdPrefeitura;
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SavePrestacaoDeContasExecucaoFisica(execucao, true);

                ts.Complete();
            }
            ContextManager.CloseConnection();
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SavePrestacaoDeContasExecucaoFisicaProgramaProjeto(PrestacaoDeContasExecucaoFisicaProgramasProjetosInfo execucao)
        {

            int idPrefeitura = execucao.IdPrefeitura;
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SavePrestacaoDeContasExecucaoFisicaProgramaProjeto(execucao, true);

                ts.Complete();
            }
            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveHistoricoPrestacaoDeContas(HistoricoPrestacaoDeContasInfo historico)
        {

            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SaveHistoricoPrestacaoDeContas(historico, true);

                ts.Complete();
            }
            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveQuestionarioPrestacaoDeContasCMAS(QuestoesCMASinfo q)
        {
            int idPrefeitura = q.IdPrefeitura;
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SaveQuestionarioPrestacaoDeContasCMAS(q, idPrefeitura, true);
                ts.Complete();
            }
            ContextManager.CloseConnection();

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveQuestionarioPrestacaoDeContasDRADS(QuestoesDRADSInfo q)
        {
            int idPrefeitura = q.IdPrefeitura;
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SaveQuestionarioPrestacaoDeContasDRADS(q, idPrefeitura, true);
                ts.Complete();
            }
            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveDeliberacaoPrestacaoDeContasCMAS(DeliberacaoPrestacaoDeContasCMASInfo d)
        {
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SaveDeliberacaoPrestacaoDeContasCMAS(d, true);
                ts.Complete();
            }
            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveDeliberacaoPrestacaoDeContasDRADS(DeliberacaoPrestacaoDeContasDRADSInfo d)
        {
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SaveDeliberacaoPrestacaoDeContasDRADS(d, true);
                ts.Complete();
            }
            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveComentarioPrestacaoDeContasCMAS(ComentarioPrestacaoDeContasCMASInfo c)
        {
            int idPrefeitura = c.IdPrefeitura;
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SaveComentarioPrestacaoDeContasCMAS(c, idPrefeitura, true);
                ts.Complete();
            }

            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveComentarioPrestacaoDeContasDRADS(ComentarioPrestacaoDeContasDRADSInfo c)
        {
            int idPrefeitura = c.IdPrefeitura;
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SaveComentarioPrestacaoDeContasDRADS(c, idPrefeitura, true);
                ts.Complete();
            }

            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveDeliberacaoCMAS(DeliberacaoCMASInfo D)
        {

            int idPrefeitura = D.IdPrefeitura;

            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                new RecursosFinanceiros().SaveDeliberacao(D, true);

                ts.Complete();
            }
        }

        /// <summary>
        /// Selecionar valores de Transferência de Renda dos Programas referente à 2013
        /// </summary>
        /// <param name="idPrefeitura"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TransferenciaRenda2016Info> GetTransferenciaRenda2016ByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetTransferenciaRenda2016(idPrefeitura);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public SaoPauloSolidario2016Info GetSaoPauloSolidario2016ByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new RecursosFinanceiros().GetSaoPauloSolidario2016(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TransferenciaRendaAnualInfo> GetTransferenciaRendaByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetTransferenciaRenda(idPrefeitura);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public int GetMetaPrevista(Int32 idPrograma, string CNPJ)
        {
            ContextManager.OpenConnection();
            var obj = new RecursosFinanceiros().GetMetaPrevista(idPrograma, CNPJ);
            ContextManager.CloseConnection();
            return obj;
        }


        /// <summary>
        /// Selecionar Comentário do Órgão Gestor sobre a Execução Financeira de 2013
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ComentarioExecucaoFinanceiraInfo> GetComentarioExecucaoFinanceiraByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new RecursosFinanceiros().GetComentarioExecucaoFinanceiraByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return obj;

        }

        /// <summary>
        /// Salvar Comentário do Órgão Gestor sobre a Execução Financeira de 2013
        /// </summary>
        /// <param name="comentario">Comentário</param>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveComentarioExecucao(ComentarioExecucaoFinanceiraInfo comentario, Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            try
            {
                new RecursosFinanceiros().SaveComentarioExecucaoFinanceira(comentario, idPrefeitura, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaCofinanciamentoEstadualInfo> GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(Int32 idPrefeitura, Int32 IdTipoProtecaoSocial, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new RecursosFinanceiros().GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, IdTipoProtecaoSocial, exercicio);
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ProgramasDesenvolvidosMunicipio2016Info GetProgramasDesenvolvidosMunicipio2016(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new RecursosFinanceiros().GetProgramasDesenvolvidosMunicipio2016(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ProgramasDesenvolvidosMunicipio2016Info GetProgramasDesenvolvidosMunicipio2021(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new RecursosFinanceiros().GetProgramasDesenvolvidosMunicipio2021(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }

        #endregion

        #region Cronograma de Desembolso
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public CronogramaDesembolsoInfo GetCronogramaDesembolsoRedePublicaByPrefeituraETipoProtecaoSocial(Int32 idPrefeitura, Int32 idTipoProtecaoSocial, int exercicio)
        {
            ContextManager.OpenConnection();
            var obj = new CronogramaDesembolso().GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, 1, exercicio);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public CronogramaDesembolsoInfo GetCronogramaDesembolsoRedePrivadaByPrefeituraETipoProtecaoSocial(Int32 idPrefeitura, Int32 idTipoProtecaoSocial, int exercicio)
        {
            ContextManager.OpenConnection();
            var obj = new CronogramaDesembolso().GetByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, idTipoProtecaoSocial, 2, exercicio);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveCronogramaDesembolsoRedePublica(CronogramaDesembolsoInfo cronograma, int exercicio)
        {
            ContextManager.OpenConnection();
            try
            {

                var idCronograma = new CronogramaDesembolso().GetAll()
                    .Where(t => t.IdPrefeitura == cronograma.IdPrefeitura 
                             && t.IdTipoProtecaoSocial == cronograma.IdTipoProtecaoSocial 
                             && t.IdTipoUnidade == 1
                             && t.Exercicio == exercicio)
                    .Select(t => new { t.Id }).FirstOrDefault();
                if (idCronograma == null)
                {
                    if (cronograma.IdTipoProtecaoSocial == 4 && cronograma.IdTipoProtecaoSocial == 5)
                    {
                        ContextManager.CloseConnection();
                        return;
                    }
                    else
                    {
                        new CronogramaDesembolso().SaveCronogramaDesembolsoProgramaBeneficios(cronograma, true);
                        return;
                    }
                }
                else
                {

                }

                cronograma.Id = idCronograma.Id;
                cronograma.IdTipoUnidade = 1;

                new CronogramaDesembolso().Update(cronograma, true);
                ContextManager.CloseConnection();
                return;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveCronogramaDesembolsoRedePrivada(CronogramaDesembolsoInfo cronograma, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            try
            {

                var idCronograma = new CronogramaDesembolso().GetAll().Where(t => t.IdPrefeitura == cronograma.IdPrefeitura && t.IdTipoProtecaoSocial == cronograma.IdTipoProtecaoSocial && t.IdTipoUnidade == 2 && t.Exercicio == exercicio).Select(t => new { t.Id }).FirstOrDefault();

                if (idCronograma == null)
                {
                    new CronogramaDesembolso().SaveCronogramaDesembolsoProgramaBeneficios(cronograma,true);
                    ContextManager.CloseConnection();
                    return;
                }
                cronograma.Id = idCronograma.Id;
                cronograma.IdTipoUnidade = 2;
                new CronogramaDesembolso().Update(cronograma, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion

        #region Índice de Gestão Descentralizada
        /// <summary>
        /// Selecionar Indice de Gestão Descentralizada do Município
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Indice de Gestão Descentralizada do Município</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public IndiceGestaoDescentralizadaInfo GetIndiceGestaoDescentralizadaByPrefeitura(Int32 idPrefeitura, int exercicio)
        {
            ContextManager.OpenConnection();
            var obj = new IndiceGestaoDescentralizada().GetByPrefeitura(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Indice de Gestão Descentralizada do Município
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Indice de Gestão Descentralizada do Município</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public IndiceGestaoDescentralizadaInfo GetIndiceGestaoDescentralizadaByPrefeituraByExercicio(Int32 idPrefeitura, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            var obj = new IndiceGestaoDescentralizada().GetByPrefeituraByExercicio(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Indice de Gestão Descentralizada do Município de 2013
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Indice de Gestão Descentralizada do Município</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public IndiceGestaoDescentralizadaInfo GetIndiceGestaoDescentralizada2016ByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new IndiceGestaoDescentralizada().Get2016ByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }


        /// <summary>
        /// Salvar Indice de Gestão Descentralizada do Município
        /// </summary>        
        /// <param name="obj">Dados do Indice de Gestão Descentralizada </param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveIndiceGestaoDescentralizada(IndiceGestaoDescentralizadaInfo obj)
        {
            ContextManager.OpenConnection();
            try
            {
                IndiceGestaoDescentralizada _repositorio = new IndiceGestaoDescentralizada();
                var indice = _repositorio.GetByPrefeituraByExercicio(obj.IdPrefeitura, obj.Exercicio);
                if (indice == null)
                {
                    _repositorio.Add(obj, true);
                    return;
                }
                indice.IGDPBF = obj.IGDPBF;
                indice.IGDPBFValorMensal = obj.IGDPBFValorMensal;
                indice.IGDSUAS = obj.IGDSUAS;
                indice.IGDSUASValorMensal = obj.IGDSUASValorMensal;
                indice.ComentariosExecucaoFinanceira = obj.ComentariosExecucaoFinanceira;
                indice.Exercicio = obj.Exercicio;

                new IndiceGestaoDescentralizada().Update(indice, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion

         
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrefeituraSituacaoQuadroInfo> GetPrefeituraSituacaoQuadro(Int32 idPrefeitura, Int32 idRecurso)
        {
            ContextManager.OpenConnection();
            var quadro = new Prefeitura().GetPrefeituraSituacaoQuadro(idPrefeitura, idRecurso);
            ContextManager.CloseConnection();
            return quadro;
        }

         
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SavePrefeituraSituacaoQuadro(PrefeituraSituacaoQuadroInfo quadro)
        {
            ContextManager.OpenConnection();
            try
            {
                new Prefeitura().SavePrefeituraSituacaoQuadro(quadro, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SavePrefeiturasSituacoesQuadros(int idRecurso, int idSituacao, int exercicio)
        {
            ContextManager.OpenConnection();
            try
            {
                new Prefeitura().SavePrefeiturasSituacoesQuadros(idRecurso, idSituacao, exercicio);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        

        public void SaveComentarioExecucaoFinanceira2016(string comentario, int idPrefeitura)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public PrefeituraValoresReprogramadosAnoAnteriorInfo GetValoresReprogramadosAnoAnterior(int idPrefeitura)
        {
            ContextManager.OpenConnection();
            try
            {
                var valoresReprogramadosAnoAnterior = new RecursosFinanceiros().GetValoresReprogramadosAnoAnterior(idPrefeitura);
                ContextManager.CloseConnection();
                return valoresReprogramadosAnoAnterior;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SavePrefeiturasSituacoesQuadrosEFLO(int idRecurso, int idSituacao, int exercicio)
        {
            ContextManager.OpenConnection();
            try
            {
                new Prefeitura().SavePrefeiturasSituacoesQuadrosEFLO(idRecurso, idSituacao, exercicio);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        

    }
}
