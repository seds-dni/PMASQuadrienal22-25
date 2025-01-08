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
    public class PlanoMunicipalService : IPlanoMunicipalService
    {

        #region Impressão
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public byte[] GetImpressaoBlocoIByPrefeitura(int idPrefeitura)
        {
            return new Impressao().GetBlocoI(idPrefeitura);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public byte[] GetImpressaoBlocoIIByPrefeitura(int idPrefeitura)
        {
            return new Impressao().GetBlocoII(idPrefeitura);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public byte[] GetImpressaoBlocoIIIByPrefeitura(int idPrefeitura)
        {
            return new Impressao().GetBlocoIII(idPrefeitura);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public byte[] GetImpressaoBlocoIVByPrefeitura(int idPrefeitura)
        {
            return new Impressao().GetBlocoIV(idPrefeitura);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public byte[] GetImpressaoBlocoVByPrefeitura(int idPrefeitura)
        {
            return new Impressao().GetBlocoV(idPrefeitura);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public byte[] GetImpressaoBlocoVIByPrefeitura(int idPrefeitura)
        {
            return new Impressao().GetBlocoVI(idPrefeitura);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public byte[] GetImpressaoBlocoVIIByPrefeitura(int idPrefeitura)
        {
            return new Impressao().GetBlocoVII(idPrefeitura);
        }
        #endregion

        #region Fluxo
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaPlanoMunicipalHistoricoInfo> GetHistoricoPlanoMunicipalByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            List<ConsultaPlanoMunicipalHistoricoInfo> lst = new List<ConsultaPlanoMunicipalHistoricoInfo>();
            lst = new PlanoMunicipalHistorico().GetConsultaByPrefeitura(idPrefeitura).ToList();  
            //var lst = new PlanoMunicipalHistorico().GetConsultaByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ConsultaPlanoMunicipalHistoricoInfo GetHistoricoPlanoMunicipalById(Int32 id)
        {
            ContextManager.OpenConnection();
            var obj = new PlanoMunicipalHistorico().GetConsultaById(id);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public PlanoMunicipalHistoricoInfo GetHistoricoPlanoMunicipalFullById(Int32 id)  
        {
            ContextManager.OpenConnection();
            var obj = new PlanoMunicipalHistorico().GetById(id);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public PlanoMunicipalHistoricoInfo GetHistoricoPlanoMunicipalFullByIdPrefeitura(Int32 idPrefeitura) //Welington P.
        {
            ContextManager.OpenConnection();
            var obj = new PlanoMunicipalHistorico().GetByIdPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }


        public PlanoMunicipalHistoricoInfo GetHistoricoPlanoMunicipalFullByIdPrefeituraSituacao(Int32 idPrefeitura, Int32 idSituacao)
        {
            ContextManager.OpenConnection();
            var obj = new PlanoMunicipalHistorico().GetByIdPrefeituraSituacao(idPrefeitura, idSituacao);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void EnviarPlanoMunicipalParaDrads(Int32 idPrefeitura, String comentario, EPerfil perfil)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
                //if (new ValidacaoPMAS().PlanoMunicipalPossuiPendencia(idPrefeitura, perfil))
                //{
                //    ContextManager.CloseConnection();
                //    throw new Exception("O Plano Municipal possui pendências!");
                //}
                //else
                //{
                    new FluxoPMAS().EnviarParaDrads(comentario, idPrefeitura, id, true);
                    ContextManager.CloseConnection();
                //}
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void EnviarPlanoMunicipalParaFinalizacao(Int32 idPrefeitura, String comentario,
                                                        Decimal ValorProtecaoSocialBasica = 0M, Decimal ValorProtecaoSocialMedia = 0M,
                                                        Decimal ValorProtecaoSocialAlta = 0M, Decimal ValorBeneficioEventual = 0M, Decimal ValorSPSolidario = 0M)  
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
               
                new FluxoPMAS().EnviarParaFinalizacao(comentario, idPrefeitura, id, true, ValorProtecaoSocialBasica, ValorProtecaoSocialMedia, ValorProtecaoSocialAlta, ValorBeneficioEventual, ValorSPSolidario);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void EnviarPlanoMunicipalParaFinalizacao(Int32 idPrefeitura, String comentario,List < PlanoMunicipalHistoricoConsolidadoInfo > planos)
        {
            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            ContextManager.OpenConnection();
            try
            {

                //new FluxoPMAS().EnviarParaFinalizacao(comentario, idPrefeitura, id, true, ValorProtecaoSocialBasica, ValorProtecaoSocialMedia,
                //                                                                ValorProtecaoSocialAlta, ValorBeneficioEventual, ValorSPSolidario,
                //                                                               ValorProtecaoSocialBasicaReprogramado, ValorProtecaoSocialMediaReprogramado, ValorProtecaoSocialAltaReprogramado,
                //                                                               ValorSPSolidarioReprogramado, ValorBeneficioEventualReprogramado);
                new FluxoPMAS().EnviarParaFinalizacao(comentario, idPrefeitura, id, true, planos);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DevolverPlanoMunicipalDradsParaOrgaoGestor(Int32 idPrefeitura, String motivo)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);


            ContextManager.OpenConnection();
            try
            {
                new FluxoPMAS().Devolver(ESituacao.DevolvidoDrads, motivo, idPrefeitura, id, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DevolverPlanoMunicipalDradsParaCAS(Int32 idPrefeitura,String motivo)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
                new FluxoPMAS().DevolverCAS(idPrefeitura, motivo,id, true);
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
        public void FinalizarPlanoMunicipal(Int32 idPrefeitura, String comentario)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
                new FluxoPMAS().Finalizar(comentario, idPrefeitura, id, true);
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
        public void DevolverPlanoMunicipalCMASParaOrgaoGestor(Int32 idPrefeitura, String motivo)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
                new FluxoPMAS().Devolver(ESituacao.DevolvidopeloCMAS, motivo, idPrefeitura, id, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DesbloqueiarPlanoMunicipalParaOrgaoGestor(Int32 idPrefeitura, String motivo)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
                new FluxoPMAS().Devolver(ESituacao.Desbloqueado, motivo, idPrefeitura, id, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DesbloqueiarPlanoMunicipalParaCMAS(Int32 idPrefeitura, String motivo)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
                new FluxoPMAS().Devolver(ESituacao.EmAnalisedoCMAS, motivo, idPrefeitura, id, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@CAS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AutorizarDesbloqueioPlanoMunicipalParaCMAS(Int32 idPrefeitura, String motivo)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
                new FluxoPMAS().AutorizarDesbloqueio(ESituacao.AutorizaDesbloqueioCMAS, motivo, idPrefeitura, id, true, null,null);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@CAS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AutorizarDesbloqueioPlanoMunicipalParaOrgaoGestor(Int32 idPrefeitura, String motivo, bool? valorReprogramado)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            ContextManager.OpenConnection();
            try
            {
                new FluxoPMAS().AutorizarDesbloqueio(ESituacao.AutorizaDesbloqueioGestor, motivo, idPrefeitura, id, true, valorReprogramado,null);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@CAS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AutorizarDesbloqueioDemandasPlanoMunicipalParaOrgaoGestor(Int32 idPrefeitura, String motivo, bool? valorDemandas) 
        {
            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            ContextManager.OpenConnection();
            try
            {
                new FluxoPMAS().AutorizarDesbloqueio(ESituacao.AutorizaDesbloqueioGestor, motivo, idPrefeitura, id, true, null, valorDemandas);
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
        public ConselhoMunicipalParecerInfo GetParecerConselhoMunicipalByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var parecer = new ConselhoMunicipalParecer().GetByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return parecer;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@CMAS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveParecerConselhoMunicipal(ConselhoMunicipalParecerInfo parecer)
        {
            ContextManager.OpenConnection();
            try
            {
                if (parecer.Id == 0)
                    new ConselhoMunicipalParecer().Add(parecer, false);
                else
                    new ConselhoMunicipalParecer().Update(parecer, false);

                ContextManager.Commit();
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
        public void SaveHistoricoPlano(PlanoMunicipalHistoricoInfo plano)
        {
            ContextManager.OpenConnection();
            try
            {
                new PlanoMunicipalHistorico().Add(plano, false);

                ContextManager.Commit();
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
        public void AprovarRejeitarPlanoMunicipal(Int32 idPrefeitura, Int32 idSituacao, Boolean aprovado, EPerfil perfil)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
                var parecer = new ConselhoMunicipalParecer().GetByPrefeitura(idPrefeitura);
                parecer.AprovaPMAS = aprovado;
                new ConselhoMunicipalParecer().Update(parecer, false);

                if (aprovado)
                    new FluxoPMAS().Aprovar(null, idPrefeitura, id, false);
                else
                    new FluxoPMAS().Rejeitar(null, idPrefeitura, id, false);

                ContextManager.Commit();

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
        public void AprovarRejeitarPlanoMunicipalCMAS(Int32 idPrefeitura, Int32 idSituacao, Boolean aprovado, EPerfil perfil)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
                var parecer = new ConselhoMunicipalParecer().GetByPrefeitura(idPrefeitura);
                parecer.AprovaPMAS = aprovado;
                new ConselhoMunicipalParecer().Update(parecer, false);

                ContextManager.Commit();

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
        public void SaveParecerConselhoMunicipalSobreAlteracoes(Int32 idPrefeitura, String parecer, Boolean aprovado)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            //if (identity.Id == 0)
            //    return;

            ContextManager.OpenConnection();
            try
            {
                if (aprovado)
                    new FluxoPMAS().Aprovar(parecer, idPrefeitura, id, true);
                else
                    new FluxoPMAS().Rejeitar(parecer, idPrefeitura, id, true);

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
        public Boolean GetQuadroFinanceiroBloqueado()
        {
            try
            {
                return new RecursosFinanceiros().GetBloqueio();
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]  
        public int GetQuadroLeiOrcamentariaBloqueado()
        {
            try
            {
                return new RecursosFinanceiros().GetBloqueioLeiOrcamentaria();
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        public void SaveDesbloqueioQuadroFinanceiro(Boolean desbloqueiar)
        {
            try
            {
                new RecursosFinanceiros().SaveBloqueio(desbloqueiar);
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]  
        public void SaveDesbloqueioQuadroLeiOrcamentaria(Boolean desbloqueiar)
        {
            try
            {
                new RecursosFinanceiros().SaveBloqueioLeiOrcamentaria(desbloqueiar);
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }


        #region alterações
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Boolean ExisteAlteracoesNoPlanoMunicipalByQuadro(Int32 idPrefeitura, Int32 idQuadro)
        {

            ContextManager.OpenConnection();
            try
            {
                var v = new Log().GetByQuadro(idPrefeitura, idQuadro).Count() > 0;
                ContextManager.CloseConnection();
                return v;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Boolean ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(Int32 idPrefeitura, Int32 idQuadro, Int32 idItemCadastro)
        {

            ContextManager.OpenConnection();
            try
            {
                var v = new Log().GetByQuadroEItem(idPrefeitura, idQuadro, idItemCadastro).Count() > 0;
                ContextManager.CloseConnection();
                return v;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaLogInfo> GetAlteracoesNoPlanoMunicipalByQuadro(Int32 idPrefeitura, Int32 idQuadro)
        {

            ContextManager.OpenConnection();
            try
            {
                var lst = new Log().GetConsultaByQuadro(idPrefeitura, idQuadro).OrderByDescending(t => t.DataHorario).ThenBy(t => t.IdQuadro).ToList();
                ContextManager.CloseConnection();
                return lst;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaLogInfo> GetAlteracoesNoPlanoMunicipalByQuadroCadastro(Int32 idPrefeitura, Int32 idQuadro, Int32 idItemCadastro)
        {
            ContextManager.OpenConnection();
            try
            {
                var lst = new Log().GetConsultaByQuadroEItem(idPrefeitura, idQuadro, idItemCadastro).OrderByDescending(t => t.DataHorario).ThenBy(t => t.IdQuadro).ToList();
                ContextManager.CloseConnection();
                return lst;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaLogInfo> GetAlteracoesNoPlanoMunicipalByPrefeitura(Int32 idPrefeitura)
        {

            ContextManager.OpenConnection();
            try
            {
                var lst = new Log().GetConsultaByPrefeitura(idPrefeitura).OrderByDescending(t => t.DataHorario).ThenBy(t => t.IdQuadro).ToList();
                ContextManager.CloseConnection();
                return lst;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaLogInfo> GetAlteracoesNoPlanoMunicipalByPrefeituraUltimaRevisao(Int32 idPrefeitura)
        {

            ContextManager.OpenConnection();
            try
            {
                var lst = new Log().GetConsultaByPrefeituraUltimaRevisao(idPrefeitura).OrderByDescending(t => t.DataHorario).ThenBy(t => t.IdQuadro).ToList();
                ContextManager.CloseConnection();
                return lst;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion


        public void AprovarRejeitarPlanoMunicipal(int idPrefeitura, bool aprovado)
        {
            throw new NotImplementedException();
        }

    }
}
