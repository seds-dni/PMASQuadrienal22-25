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
using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Servicos
{
    /// <summary>
    /// Serviço Responsável por fornecer informações sobre a Rede de Proteção Social utilizada no PMAS QUADRIENAL
    /// </summary>
    [ServiceBehavior(Namespace = "http://seds.sp.gov.br/redeprotecaosocial",
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerSession,
    TransactionIsolationLevel = IsolationLevel.ReadCommitted,
    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class RedeProtecaoSocialService : IRedeProtecaoSocialService
    {
        #region Unidade Pública
        /// <summary>
        /// Selecionar Unidade Pública pelo Identificador
        /// </summary>        
        /// <param name="idUnidadePublica">Id da Unidade Pública</param>
        /// <returns>Dados da Unidade Pública</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public UnidadePublicaInfo GetUnidadePublicaById(Int32 idUnidadePublica)
        {
            ContextManager.OpenConnection();
            var obj = new UnidadePublica().GetById(idUnidadePublica);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Unidades Públicas por Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados das Unidades Públicas</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaUnidadePublicaInfo> GetIdentificacaoUnidadesPublicaByPrefeitura(Int32 idPrefeitura, String nome)
        {
            ContextManager.OpenConnection();
            var query = new UnidadePublica().GetConsultaByPrefeitura(idPrefeitura);
            if (!String.IsNullOrEmpty(nome))
            {
                Int64 id;
                if (Int64.TryParse(nome, out id))
                {
                    query = query.Where(t => t.CNPJ.Contains(nome));
                }
                else
                {
                    query = query.Where(c => c.RazaoSocial.Contains(nome));
                }
            }
            var lstUnidades = query.ToList();
            ContextManager.CloseConnection();
            return lstUnidades;
        }

        /// <summary>
        /// Selecionar Unidades Públicas por Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados das Unidades Públicas</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaUnidadePublicaInfo> GetIdentificacaoUnidadesPublicaByPrefeituraExercicio(Int32 idPrefeitura, String nome,Int32 Exercicio)
        {
            ContextManager.OpenConnection();
            var query = new UnidadePublica().GetConsultaByPrefeituraExercicio(idPrefeitura,Exercicio); 
            if (!String.IsNullOrEmpty(nome))
            {
                Int64 id;
                if (Int64.TryParse(nome, out id))
                {
                    query = query.Where(t => t.CNPJ.Contains(nome));
                }
                else
                {
                    query = query.Where(c => c.RazaoSocial.Contains(nome));
                }
            }
            var lstUnidades = query.ToList();
            ContextManager.CloseConnection();
            return lstUnidades;
        }


        public List<ConsultaLocalPublicoGeral> GetLocaisPublicosByUnidade(int idUnidade)
        {
            ContextManager.OpenConnection();
            var query = new UnidadePublica().GetLocaisPublicosByUnidade(idUnidade);

            var lstUnidades = query.ToList();
            ContextManager.CloseConnection();
            return lstUnidades;
        }

        /// <summary>
        /// Atualizar Dados da Unidade Pública
        /// </summary>
        /// <param name="unidade">Dados da Unidade Pública</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateUnidadePublica(UnidadePublicaInfo unidade)
        {
            ContextManager.OpenConnection();
            try
            {
                new UnidadePublica().Update(unidade, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Adicionar uma Unidade Pública à Prefeitura
        /// </summary>
        /// <param name="unidade">Dados da Unidade Pública</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddUnidadePublica(UnidadePublicaInfo unidade)
        {
            ContextManager.OpenConnection();
            try
            {
                new UnidadePublica().Add(unidade, true);
                ContextManager.CloseConnection();
                return unidade.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Excluir Unidade Pública
        /// </summary>
        /// <param name="idUnidade">Id da Unidade</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUnidadePublica(Int32 idUnidade)
        {
            ContextManager.OpenConnection();
            try
            {
                var u = new UnidadePublica();
                u.Delete(u.GetById(idUnidade), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #region Local de Execução Público
        /// <summary>
        /// Selecionar Local de Execução dos serviços de uma Unidade Pública pelo Identificador
        /// </summary>        
        /// <param name="idLocalExecucao">Id da Unidade Pública</param>
        /// <returns>Dados do Local de Execução</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public LocalExecucaoPublicoInfo GetLocalExecucaoPublicoById(Int32 idLocalExecucao)
        {
            ContextManager.OpenConnection();
            var obj = new LocalExecucaoPublico().GetById(idLocalExecucao);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Identificação dos Locais de Execução do serviços de uma Unidade Pública
        /// </summary>
        /// <param name="idUnidade">Id da Unidade</param>
        /// <returns>Identificação do Local de Execução</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaLocalExecucaoPublicoInfo>  GetIdentificacaoLocalExecucaoPublicoByUnidade(Int32 idUnidade, String nome = null)  
        {
            ContextManager.OpenConnection();
            if (!String.IsNullOrEmpty(nome))
            {
                var lstLocal = new LocalExecucaoPublico().GetConsultaByUnidade(idUnidade).Where(c => c.Nome.Contains(nome)).ToList();
                ContextManager.CloseConnection();
                return lstLocal;
            }
            var lst = new LocalExecucaoPublico().GetConsultaByUnidade(idUnidade).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Identificação dos Locais de Execução do serviços de uma Unidade Pública Inativa
        /// </summary>
        /// <param name="idUnidade">Id da Unidade</param>
        /// <returns>Identificação do Local de Execução</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaLocalExecucaoPublicoInativoInfo> GetIdentificacaoLocalExecucaoPublicoInativoByUnidade(Int32 idUnidade, String nome = null)
        {
            ContextManager.OpenConnection();
            if (!String.IsNullOrEmpty(nome))
            {
                var lstLocal = new LocalExecucaoPublico().GetConsultaByUnidadeInativa(idUnidade).Where(c => c.Nome.Contains(nome)).ToList();
                ContextManager.CloseConnection();
                return lstLocal;
            }
            var lst = new LocalExecucaoPublico().GetConsultaByUnidadeInativa(idUnidade).ToList();
            ContextManager.CloseConnection();
            return lst;
        }	

        /// <summary>
        /// Atualizar Dados do Local Execução dos serviços da Unidade Pública
        /// </summary>
        /// <param name="local">Dados do Local de Execução</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateLocalExecucaoPublico(LocalExecucaoPublicoInfo local)
        {
            ContextManager.OpenConnection();
            try
            {
                new LocalExecucaoPublico().Update(local, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Adicionar um Local de Execução dos serviços à uma Unidade Pública à Prefeitura
        /// </summary>
        /// <param name="local">Dados do Local de Execução</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddLocalExecucaoPublico(LocalExecucaoPublicoInfo local)
        {
            ContextManager.OpenConnection();
            try
            {
                new LocalExecucaoPublico().Add(local, true);
                ContextManager.CloseConnection();
                return local.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Excluir Local de Execução de uma Unidade Pública
        /// </summary>
        /// <param name="idLocal">Id do Local de Execução</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLocalExecucaoPublico(Int32 idLocal)
        {
            ContextManager.OpenConnection();
            try
            {
                var l = new LocalExecucaoPublico();
                l.Delete(l.GetById(idLocal), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #region Serviços e Recursos Financeiros
        /// <summary>
        /// Adicionar Serviço e Recursos Financeiros à um Local de Execução Público
        /// </summary>
        /// <param name="servico">Dados do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddServicoRecursoFinanceiroPublico(ServicoRecursoFinanceiroPublicoInfo servico)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroPublico().Add(servico, true);
                ContextManager.CloseConnection();
                return servico.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Atualizar Serviço e Recursos Financeiros do Local de Execução Público
        /// </summary>
        /// <param name="servico">Dados do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateServicoRecursoFinanceiroPublico(ServicoRecursoFinanceiroPublicoInfo servico)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroPublico().Update(servico, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        public void AddServicoRecursoFinanceiroPublicoFonteRecurso(ServicoRecursoFinanceiroPublicoFonteRecursoInfo fonteRecurso)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroPublicoFonteRecurso().Add(fonteRecurso, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="recursoshumanos"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddServicoRecursoFinanceiroPublicoRH(ServicoRecursoFinanceiroPublicoRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroPublicoRecursosHumanos().Add(recursoshumanos, true);
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
        public ServicoRecursoFinanceiroPublicoRecursosHumanosInfo GetRecursosHumanosPublicoByIdServicoRecursoFinanceiro(Int32 IdServicoRecursoFinanceiro)
        {
            ContextManager.OpenConnection();
            var obj = new ServicoRecursoFinanceiroPublicoRecursosHumanos().GetByServicoRecursoFinanceiro(IdServicoRecursoFinanceiro);
            ContextManager.CloseConnection();
            return obj;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateServicoRecursoFinanceiroPublicoRH(ServicoRecursoFinanceiroPublicoRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroPublicoRecursosHumanos().Update(recursoshumanos, true);
                ContextManager.CloseConnection();

            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Remover Serviço e Recurso Financeiro do Local de Execução Público
        /// </summary>
        /// <param name="idServico"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteServicoRecursoFinanceiroPublico(Int32 idServico)
        {
            ContextManager.OpenConnection();
            try
            {
                var negocio = new ServicoRecursoFinanceiroPublico();
                negocio.Delete(negocio.GetById(idServico), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Selecionar Serviço e Recursos Financeiros do Local de Execução Público através do Identificador
        /// </summary>
        /// <param name="idServico">Id do Serviço e Recurso Financeiro</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ServicoRecursoFinanceiroPublicoInfo GetServicoRecursoFinanceiroPublicoById(Int32 idServico)
        {
            ContextManager.OpenConnection();
            var obj = new ServicoRecursoFinanceiroPublico().GetById(idServico);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaServicosRecursosFinanceirosPublicoInfo> GetConsultaServicosRecursosFinanceirosPublicoByLocalExecucao(Int32 idLocalExecucao)
        {
            ContextManager.OpenConnection();
            var lst = new ServicoRecursoFinanceiroPublico().GetConsultaByLocalExecucao(idLocalExecucao).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public MotivoEstadualizadoInfo GetMotivoEstadualizadoById(int idMotivo)
        {
            ContextManager.OpenConnection();
            var obj = new MotivoEstadualizado().GetById(idMotivo);
            ContextManager.CloseConnection();
            return obj;
        }
        #endregion
        #endregion
        #endregion

        #region Unidade Privada
        /// <summary>
        /// Selecionar Unidade Privada pelo Identificador
        /// </summary>
        /// <param name="idUnidadePrivada"></param>
        /// <returns>Dados das Unidades Privada</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public UnidadePrivadaInfo GetUnidadePrivadaById(int idUnidadePrivada)
        {
            ContextManager.OpenConnection();
            var obj = new UnidadePrivada().GetById(idUnidadePrivada);
            ContextManager.CloseConnection();
            return obj;
        }


        public List<UnidadePrivadaInfo> GetUnidadesDistrituicaoVivaleite(int idPrefeitura)
        {

            ContextManager.OpenConnection();
            var query = new UnidadePrivada().GetByPrefeitura(idPrefeitura);
            query = query.Where(c => c.CaracterizacaoAtividades.Any(t => t.Id == 3));
            var lst = query.ToList();
            ContextManager.CloseConnection();
            return lst;

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<DemandasParlamentaresServicosPublicosInfo> GetDemandasByServicoRecursoFinanceiroPublico(int idServico)
        {
            ContextManager.OpenConnection();
            var query = new DemandasParlamentaresServicosPublicos().GetIdServico(idServico);

            var lstDemandas =  query.ToList();

            ContextManager.CloseConnection();

            return lstDemandas;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SalvarDemandasServicosPublicos(DemandasParlamentaresServicosPublicosInfo demandas) 
        {
            try
            {
                ContextManager.OpenConnection();

                new DemandasParlamentaresServicosPublicos().SalvarDemandasServicosPublicos(demandas);

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
        public List<DemandasParlamentaresServicosPrivadosInfo> GetDemandasByServicoRecursoFinanceiroPrivados(int idServicoRecursoFinanceiroPrivado)
        {
            ContextManager.OpenConnection();
            var query = new DemandasParlamentaresServicosPrivado().GetIdServico(idServicoRecursoFinanceiroPrivado);

            var lstDemandas = query.ToList();

            ContextManager.CloseConnection();

            return lstDemandas;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SalvarDemandasServicosPrivados(DemandasParlamentaresServicosPrivadosInfo demandas)
        {
            try
            {
                ContextManager.OpenConnection();

                new DemandasParlamentaresServicosPrivado().SalvarDemandasServicosPrivados(demandas);

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
        public List<DemandasParlamentaresServicosCRASInfo> GetDemandasByServicoRecursoFinanceiroCRAS(int idServicoRecursoFinanceiroCRAS)
        {
            ContextManager.OpenConnection();
            var query = new DemandasParlamentaresServicosCRAS().GetIdServico(idServicoRecursoFinanceiroCRAS);

            var lstDemandas = query.ToList();

            ContextManager.CloseConnection();

            return lstDemandas;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SalvarDemandasServicosCRAS(DemandasParlamentaresServicosCRASInfo demandas)
        {
            try
            {
                ContextManager.OpenConnection();

                new DemandasParlamentaresServicosCRAS().SalvarDemandasServicosCRAS(demandas);

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
        public List<DemandasParlamentaresServicosCREASInfo> GetDemandasByServicoRecursoFinanceiroCREAS(int idServicoRecursoFinanceiroCREAS)
        {
            ContextManager.OpenConnection();
            var query = new DemandasParlamentaresServicosCREAS().GetIdServico(idServicoRecursoFinanceiroCREAS);

            var lstDemandas = query.ToList();

            ContextManager.CloseConnection();

            return lstDemandas;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SalvarDemandasServicosCREAS(DemandasParlamentaresServicosCREASInfo demandas)
        {
            try
            {
                ContextManager.OpenConnection();

                new DemandasParlamentaresServicosCREAS().SalvarDemandasServicosCREAS(demandas);

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
        public List<DemandasParlamentaresServicosCentroPOPInfo> GetDemandasByServicoRecursoFinanceiroCentroPOP(int idServicoRecursoFinanceiroCentroPOP)
        {
            ContextManager.OpenConnection();
            var query = new DemandasParlamentaresServicosCentroPOP().GetIdServico(idServicoRecursoFinanceiroCentroPOP);

            var lstDemandas = query.ToList();

            ContextManager.CloseConnection();

            return lstDemandas;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SalvarDemandasServicosCentroPOP(DemandasParlamentaresServicosCentroPOPInfo demandas)
        {
            try
            {
                ContextManager.OpenConnection();

                new DemandasParlamentaresServicosCentroPOP().SalvarDemandasServicosCentroPOP(demandas);

                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }



        /// <summary>
        /// Selecionar Unidades Privadas por Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <param name="nome">Razão Social da Prefeitura</param>
        /// <returns>Dados das Unidades Privada</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaUnidadePrivadaInfo> GetIdentificacaoUnidadesPrivadaByPrefeitura(Int32 idPrefeitura, String nome)
        {
            ContextManager.OpenConnection();
            var query = new UnidadePrivada().GetConsultaByPrefeitura(idPrefeitura);

            if (!String.IsNullOrEmpty(nome))
            {
                Int64 id;
                if (Int64.TryParse(nome, out id))
                    query = query.Where(t => t.CNPJ.Contains(nome));
                else
                    query = query.Where(c => c.RazaoSocial.Contains(nome));
            }
            var lstUnidades = query.ToList();
            ContextManager.CloseConnection();

            return lstUnidades;
        }

        /// <summary>
        /// Selecionar Unidades Privadas Desativadas por Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <param name="nome">Razão Social da Prefeitura</param>
        /// <returns>Dados das Unidades Privada Desativadas</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaUnidadePrivadaDesativadaInfo> GetIdentificacaoUnidadesPrivadaDesativadaByPrefeitura(Int32 idPrefeitura, String nome)
        {
            ContextManager.OpenConnection();
            var query = new UnidadePrivada().GetConsultaDesativadasByPrefeitura(idPrefeitura);

            if (!String.IsNullOrEmpty(nome))
            {
                Int64 id;
                if (Int64.TryParse(nome, out id))
                    query = query.Where(t => t.CNPJ.Contains(nome));
                else
                    query = query.Where(c => c.RazaoSocial.Contains(nome));
            }

            var lstUnidades = query.ToList();
            ContextManager.CloseConnection();

            return lstUnidades;
        }

        /// <summary>
        /// Selecionar Unidades Privadas por Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <param name="nome">Razão Social da Prefeitura</param>
        /// <returns>Dados das Unidades Privada</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaUnidadePrivadaInfo> GetIdentificacaoUnidadesPrivadaByPrefeitura(Int32 idPrefeitura, String nome, Int32 IdUnidadeTipoAtendimento)
        {
            ContextManager.OpenConnection();
            var query = new UnidadePrivada().GetConsultaByPrefeitura(idPrefeitura);
            //if (IdUnidadeTipoAtendimento != null)
            //{
            //    query = query.Where(t => t.IdTipoUnidadeAtendimento == IdUnidadeTipoAtendimento);
            //}
            if (!String.IsNullOrEmpty(nome))
            {
                Int32 id;
                if (Int32.TryParse(nome, out id))
                    query = query.Where(t => t.CNPJ.Contains(nome));
                else
                    query = query.Where(c => c.RazaoSocial.Contains(nome));
            }
            var lstUnidades = query.ToList();
            ContextManager.CloseConnection();
            return lstUnidades;
        }



        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateServicoRecursoFinanceiroPublicoRH(ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroPrivadoRecursosHumanos().Update(recursoshumanos, true);
                ContextManager.CloseConnection();

            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Selecionar Unidades Privadas por Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <param name="nome">Razão Social da Prefeitura</param>
        /// <returns>Dados das Unidades Privada</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<UnidadePrivadaInfo> GetUnidadesOfertantesByPrefeitura(Int32 idPrefeitura, String nome)
        {
            ContextManager.OpenConnection();
            var query = new UnidadePrivada().GetByPrefeitura(idPrefeitura);
            if (!String.IsNullOrEmpty(nome))
            {
                Int32 id;
                if (Int32.TryParse(nome, out id))
                    query = query.Where(t => t.CNPJ.Contains(nome));
                else
                    query = query.Where(c => c.RazaoSocial.Contains(nome));
            }
            var lstUnidades = query.ToList();
            ContextManager.CloseConnection();
            return lstUnidades;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@CMAS")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateUnidadePrivada(UnidadePrivadaInfo unidade)
        {
            ContextManager.OpenConnection();
            try
            {
                new UnidadePrivada().Update(unidade, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        /// <summary>
        /// Adicionar unidade privada
        /// </summary>
        /// <param name="unidade">Dados da Unidade Privada</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddUnidadePrivada(UnidadePrivadaInfo unidade)
        {
            ContextManager.OpenConnection();
            try
            {
                new UnidadePrivada().Add(unidade, true);
                ContextManager.CloseConnection();
                return unidade.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        /// <summary>
        /// Excluir unidade privada
        /// </summary>
        /// <param name="idUnidade">Id da Unidade</param>

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUnidadePrivada(Int32 idUnidade)
        {
            ContextManager.OpenConnection();
            try
            {
                var u = new UnidadePrivada();
                u.Delete(u.GetById(idUnidade), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #region Local de Execução Privado

        public List<ConsultaLocalExecucaoPrivadoInfo> GetLocaisExecucaoPrivadoByIdUnidade(Int32 IdUnidadePrivada)
        {
            ContextManager.OpenConnection();
            var lst = new LocalExecucaoPrivado().GetConsultaByUnidade(IdUnidadePrivada).ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        /// <summary>
        /// Selecionar Local de Execução dos serviços de uma Unidade Privada pelo Identificador
        /// </summary>        
        /// <param name="idLocalExecucao">Id do Local de Execução</param>
        /// <returns>Dados do Local de Execução</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public LocalExecucaoPrivadoInfo GetLocalExecucaoPrivadoById(Int32 idLocalExecucao)
        {
            ContextManager.OpenConnection();
            var obj = new LocalExecucaoPrivado().GetById(idLocalExecucao);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Identificação dos Locais de Execução do serviços de uma Unidade Privada
        /// </summary>
        /// <param name="idUnidade">Id da Unidade</param>
        /// <returns>Identificação do Local de Execução</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaLocalExecucaoPrivadoInfo> GetIdentificacaoLocalExecucaoPrivadoByUnidade(Int32 idUnidade)
        {
            ContextManager.OpenConnection();
            var obj = new LocalExecucaoPrivado().GetConsultaByUnidade(idUnidade).ToList();
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Atualizar Dados do Local Execução dos serviços da Unidade Privada
        /// </summary>
        /// <param name="local">Dados do Local de Execução</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateLocalExecucaoPrivado(LocalExecucaoPrivadoInfo local)
        {
            ContextManager.OpenConnection();
            try
            {
                new LocalExecucaoPrivado().Update(local, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Adicionar um Local de Execução dos serviços à uma Unidade Privada à Prefeitura
        /// </summary>
        /// <param name="local">Dados do Local de Execução</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddLocalExecucaoPrivado(LocalExecucaoPrivadoInfo local)
        {
            ContextManager.OpenConnection();
            try
            {
                new LocalExecucaoPrivado().Add(local, true);
                ContextManager.CloseConnection();
                return local.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdServicoRecursoFinanceiro"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo GetRecursosHumanosPrivadoByIdServicoRecursoFinanceiro(Int32 IdServicoRecursoFinanceiro)
        {
            ContextManager.OpenConnection();
            var obj = new ServicoRecursoFinanceiroPrivadoRecursosHumanos().GetByServicoRecursoFinanceiro(IdServicoRecursoFinanceiro);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Excluir Local de Execução de uma Unidade Privada
        /// </summary>
        /// <param name="idLocal">Id do Local de Execução</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLocalExecucaoPrivado(Int32 idLocal)
        {
            ContextManager.OpenConnection();
            try
            {
                var l = new LocalExecucaoPrivado();
                l.Delete(l.GetById(idLocal), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #region Serviços e Recursos Financeiros
        /// <summary>
        /// Adicionar Serviço e Recursos Financeiros à um Local de Execução Privado
        /// </summary>
        /// <param name="servico">Dados do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddServicoRecursoFinanceiroPrivado(ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroPrivado().Add(servico, true);
                ContextManager.CloseConnection();
                return servico.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Atualizar Serviço e Recursos Financeiros do Local de Execução Privado
        /// </summary>
        /// <param name="servico">Dados do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateServicoRecursoFinanceiroPrivado(ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroPrivado().Update(servico, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Remover Serviço e Recurso Financeiro do Local de Execução Privado
        /// </summary>
        /// <param name="idServico"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteServicoRecursoFinanceiroPrivado(Int32 idServico)
        {
            ContextManager.OpenConnection();
            try
            {
                var negocio = new ServicoRecursoFinanceiroPrivado();
                negocio.Delete(negocio.GetById(idServico), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Selecionar Serviço e Recursos Financeiros do Local de Execução Privado através do Identificador
        /// </summary>
        /// <param name="idServico">Id do Serviço e Recurso Financeiro</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ServicoRecursoFinanceiroPrivadoInfo GetServicoRecursoFinanceiroPrivadoById(Int32 idServico)
        {
            ContextManager.OpenConnection();
            var obj = new ServicoRecursoFinanceiroPrivado().GetById(idServico);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaServicosRecursosFinanceirosPrivadoInfo> GetConsultaServicosRecursosFinanceirosPrivadoByLocalExecucao(Int32 idLocalExecucao)
        {
            ContextManager.OpenConnection();
            var lst = new ServicoRecursoFinanceiroPrivado().GetConsultaByLocalExecucao(idLocalExecucao).ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddServicoRecursoFinanceiroPrivadoRH(ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroPrivadoRecursosHumanos().Add(recursoshumanos, true);
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
        public void UpdateServicoRecursoFinanceiroPrivadoRH(ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroPrivadoRecursosHumanos().Update(recursoshumanos, true);
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
        #endregion

        #region CRAS
        /// <summary>
        /// Selecionar CRAS pelo Identificador
        /// </summary>        
        /// <param name="idCRAS">Id do CRAS</param>
        /// <returns>Dados do CRAS</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public CRASInfo GetCRASById(Int32 idCRAS)
        {
            ContextManager.OpenConnection();
            var obj = new CRAS().GetById(idCRAS);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Nome do CRAS pelo Identificador
        /// </summary>
        /// <param name="idCRAS">Id do CRAS</param>
        /// <returns>Nome do CRAS</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public String GetCRASNomeById(Int32 idCRAS)
        {
            ContextManager.OpenConnection();
            var obj = new CRAS().GetAll().Where(t => t.Id == idCRAS).Select(t => t.Nome).FirstOrDefault();
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Obter a Lista de CRAS pela Prefeitura
        /// </summary>
        /// <param name="idPrefeitura"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<CRASInfo> GetCRASByIdPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new CRAS().GetAll().Where(t => t.UnidadePublica.IdPrefeitura == idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar CRAS por Prefeitura
        /// </summary>
        /// <param name="idUnidade">Id da Prefeitura</param>
        /// <param name="nome">Nome do CRAS</param>
        /// <returns>Dados dos CRAS</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaCRASInfo> GetIdentificacaoCRASByUnidade(Int32 idUnidade, String nome)
        {
            ContextManager.OpenConnection();
            if (!String.IsNullOrEmpty(nome))
            {
                var lst = new CRAS().GetConsultaByUnidade(idUnidade).Where(c => c.Nome.Contains(nome)).ToList();
                ContextManager.CloseConnection();
                return lst;
            }
            var lstCRAS = new CRAS().GetConsultaByUnidade(idUnidade).ToList();
            ContextManager.CloseConnection();
            return lstCRAS;

        }

        /// <summary>
        /// Atualizar Dados do CRAS
        /// </summary>
        /// <param name="cras">Dados do CRAS</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateCRAS(CRASInfo cras)
        {
            ContextManager.OpenConnection();
            try
            {
                new CRAS().Update(cras, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Adicionar um CRAS à Prefeitura
        /// </summary>
        /// <param name="cras">Dados do CRAS</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddCRAS(CRASInfo cras)
        {

            try
            {
                new CRAS().Add(cras);
                return cras.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Excluir CRAS
        /// </summary>
        /// <param name="idCRAS">Id do CRAS</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCRAS(Int32 idCRAS)
        {
            ContextManager.OpenConnection();
            try
            {
                var u = new CRAS();
                u.Delete(u.GetById(idCRAS), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #region Serviços e Recursos Financeiros
        /// <summary>
        /// Adicionar Serviço e Recursos Financeiros à um CRAS
        /// </summary>
        /// <param name="servico">Dados do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddServicoRecursoFinanceiroCRAS(ServicoRecursoFinanceiroCRASInfo servico)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroCRAS().Add(servico, true);
                ContextManager.CloseConnection();
                return servico.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Atualizar Serviço e Recursos Financeiros do CRAS
        /// </summary>
        /// <param name="servico">Dados do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateServicoRecursoFinanceiroCRAS(ServicoRecursoFinanceiroCRASInfo servico)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroCRAS().Update(servico, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Remover Serviço e Recurso Financeiro do CRAS
        /// </summary>
        /// <param name="idServico">Id do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteServicoRecursoFinanceiroCRAS(Int32 idServico)
        {
            ContextManager.OpenConnection();
            try
            {
                var negocio = new ServicoRecursoFinanceiroCRAS();
                negocio.Delete(negocio.GetById(idServico), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Selecionar Serviço e Recursos Financeiros do CRAS através do Identificador
        /// </summary>
        /// <param name="idServico">Id do Serviço e Recurso Financeiro</param>
        /// <returns>Dados do Serviço e Recurso Financeiro</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ServicoRecursoFinanceiroCRASInfo GetServicoRecursoFinanceiroCRASById(Int32 idServico)
        {
            try
            {
                ContextManager.OpenConnection();
                var entidade = new ServicoRecursoFinanceiroCRAS().GetById(idServico);
                ContextManager.CloseConnection();
                return entidade;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaServicosRecursosFinanceirosCRASInfo> GetConsultaServicosRecursosFinanceirosByCRAS(Int32 idCRAS)
        {
            try
            {
                ContextManager.OpenConnection();
                var entidades = new ServicoRecursoFinanceiroCRAS().GetConsultaByCRAS(idCRAS).ToList();
                ContextManager.CloseConnection();
                return entidades;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddServicoRecursoFinanceiroCRASRH(ServicoRecursoFinanceiroCRASRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroCRASRecursosHumanos().Add(recursoshumanos, true);
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
        public ServicoRecursoFinanceiroCRASRecursosHumanosInfo GetRecursosHumanosCRASByIdServicoRecursoFinanceiro(Int32 IdServicoRecursoFinanceiro)
        {
            ContextManager.OpenConnection();
            var obj = new ServicoRecursoFinanceiroCRASRecursosHumanos().GetByServicoRecursoFinanceiro(IdServicoRecursoFinanceiro);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateServicoRecursoFinanceiroCRASRH(ServicoRecursoFinanceiroCRASRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroCRASRecursosHumanos().Update(recursoshumanos, true);
                ContextManager.CloseConnection();

            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #endregion

        #region Previsao de instalacao CRAS
        /// <summary>
        /// Adicionar uma Previsão de Instalação de CRAS à Prefeitura
        /// </summary>
        /// <param name="previsoesInstalacaoCRAS">Dados da previsão de instalação do CRAS</param>
        /// <param name="idPrefeitura">Id da prefeitura</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SalvarPrevisaoInstalacaoCRAS(Boolean existePrevisao, List<PrevisaoInstalacaoCRASInfo> previsoesInstalacaoCRAS, List<MotivoNaoInstalacaoInfo> motivos, Int32 idPrefeitura, Int32 idUnidade)
        {
            ContextManager.OpenConnection();
            try
            {
                string descricaoDefault = string.Empty;
                if (existePrevisao)
                {
                    motivos = new List<MotivoNaoInstalacaoInfo>();
                }
                else
                {
                    previsoesInstalacaoCRAS = new List<PrevisaoInstalacaoCRASInfo>();
                }

                new ValidadorPrevisaoInstalacaoCRAS().Validar(existePrevisao, motivos, previsoesInstalacaoCRAS);

                var previsoesDeletadas = new List<PrevisaoInstalacaoCRASInfo>();
                var previsaoInstalacaoCRASBusiness = new PrevisaoInstalacaoCRAS();
                var entidades = previsaoInstalacaoCRASBusiness.GetByPrefeitura(idPrefeitura);

                foreach (var entidade in entidades)
                {
                    if (!previsoesInstalacaoCRAS.Any(t => t.Id == entidade.Id))
                    {
                        previsoesDeletadas.Add(entidade);
                    }
                }

                foreach (var previsaoDeletada in previsoesDeletadas)
                {
                    previsaoInstalacaoCRASBusiness.Delete(previsaoDeletada, false);
                    descricaoDefault = Log.DeleteDescricaoDefaultData(previsoesDeletadas.Select(t => t.Data.ToShortDateString()).ToList());
                }


                if (previsoesInstalacaoCRAS.Count > 0)
                {
                    foreach (var previsaoInstalacaoCRAS in previsoesInstalacaoCRAS)
                    {
                        if (previsaoInstalacaoCRAS.Id == 0)
                        {
                            previsaoInstalacaoCRASBusiness.Add(previsaoInstalacaoCRAS, false);
                        }
                    }
                    descricaoDefault = Log.CreateDescricaoDefaultData(previsoesInstalacaoCRAS.Select(t => t.Data.ToShortDateString()).ToList());
                }

                if (motivos.Count > 0)
                {
                    descricaoDefault += Log.CreateMotivosDefault(motivos.Select(t => t.Nome.ToString()).ToList());
                }

                new Prefeitura().SaveMotivosNaoInstalacaoCRAS(motivos, idPrefeitura, false);

                var log = Log.CreateLog(idPrefeitura, EAcao.Update, 24, "Implantação do CRAS: " + descricaoDefault, idUnidade);
                if (log != null)
                {
                    new Log().Add(log, false);
                }

                ContextManager.Commit();
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Listar as previsões de Instalação de CRAS
        /// </summary>
        /// <param name="idPrefeitura">Id da prefeitura</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrevisaoInstalacaoCRASInfo> GetPrevisoesCRASByPrefeitura(Int32 idPrefeitura)
        {
            try
            {
                ContextManager.OpenConnection();
                var previsoes = new PrevisaoInstalacaoCRAS().GetByPrefeitura(idPrefeitura).ToList();
                ContextManager.CloseConnection();
                return previsoes;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(string.Format("Erro ao Listar Previsao CRAS Pelo id da prefeitura StackTrace: {0} - InnerException: {1}", ex.StackTrace, ex.InnerException));
            }
        }

        /// <summary>
        /// Listar os motivos de não instalação de CRAS da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da prefeitura</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MotivoNaoInstalacaoInfo> GetMotivosDeNaoInstalacaoDeCRAS(Int32 idPrefeitura)
        {
            try
            {
                ContextManager.OpenConnection();
                var previsoes = new Prefeitura().ListMotivosDeNaoInstalacaoDeCRAS(idPrefeitura);
                ContextManager.CloseConnection();
                return previsoes;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(string.Format("Erro ao Listar Motivos de Não Instalação de CRAS Pelo id da prefeitura StackTrace: {0} - InnerException: {1}", ex.StackTrace, ex.InnerException));
            }


        }


        #endregion

        #endregion

        #region CREAS
        /// <summary>
        /// Selecionar CREAS pelo Identificador
        /// </summary>        
        /// <param name="idCREAS">Id do CREAS</param>
        /// <returns>Dados do CREAS</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public CREASInfo GetCREASPorId(Int32 idCREAS)
        {
            try
            {
                ContextManager.OpenConnection();
                var entidade = new CREAS().GetById(idCREAS);
                if (entidade.AtendeOutrosMunicipios)
                {
                    entidade.AbrangenciaMunicipios = new CREASMunicipio().GetByCREAS(entidade.Id).ToList();
                }
                ContextManager.CloseConnection();
                return entidade;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(string.Format("Erro ao Obter CREAS Pelo id StackTrace: {0} - InnerException: {1}", ex.StackTrace, ex.InnerException));
            }
        }

        /// <summary>
        /// Listar os CREAS pela Prefeitura
        /// </summary>        
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do CREAS</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<CREASInfo> GetCREASPeloIdPrefeitura(int idPrefeitura)
        {
            try
            {
                ContextManager.OpenConnection();
                var entidades = new CREAS().GetAll().Where(m => m.UnidadePublica.IdPrefeitura == idPrefeitura).ToList();
                ContextManager.CloseConnection();
                return entidades;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(string.Format("Erro ao Listar CREAS Pelo id da prefeitura StackTrace: {0} - InnerException: {1}", ex.StackTrace, ex.InnerException));
            }

        }

        /// <summary>
        /// Selecionar Nome do CREAS pelo Identificador
        /// </summary>
        /// <param name="idCREAS">Id do CREAS</param>
        /// <returns>Nome do CREAS</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public String GetCREASNomeById(Int32 idCREAS)
        {
            try
            {
                ContextManager.OpenConnection();
                var obj = new CREAS().GetAll().Where(t => t.Id == idCREAS).Select(t => t.Nome).FirstOrDefault();
                ContextManager.CloseConnection();
                return obj;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(string.Format("Erro ao Obter CREAS Pelo id da prefeitura StackTrace: {0} - InnerException: {1}", ex.StackTrace, ex.InnerException));
            }

        }

        /// <summary>
        /// Selecionar CREAS por Prefeitura
        /// </summary>
        /// <param name="idUnidade">Id da Prefeitura</param>
        /// <param name="nome">Nome do CREAS</param>
        /// <returns>Dados dos CREAS</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaCREASInfo> GetIdentificacoesCREASByUnidade(Int32 idUnidade, String nome)
        {
            try
            {
                ContextManager.OpenConnection();
                if (!String.IsNullOrEmpty(nome))
                {
                    var entidades = new CREAS().GetConsultaByUnidade(idUnidade).Where(c => c.Nome.Contains(nome)).ToList();
                    ContextManager.CloseConnection();
                    return entidades;
                }
                var entidade = new CREAS().GetConsultaByUnidade(idUnidade).ToList();
                ContextManager.CloseConnection();
                return entidade;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(string.Format("Erro ao Obter Identificacões do CREAS Pelo id da unidade StackTrace: {0} - InnerException: {1}", ex.StackTrace, ex.InnerException));
            }
        }

        /// <summary>
        /// Atualizar Dados do CREAS
        /// </summary>
        /// <param name="creas">Dados do CREAS</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateCREAS(CREASInfo creas)
        {
            ContextManager.OpenConnection();
            try
            {
                new CREAS().Update(creas, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Adicionar um CREAS à Prefeitura
        /// </summary>
        /// <param name="creas">Dados do CREAS</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddCREAS(CREASInfo creas)
        {
            try
            {
                new CREAS().Add(creas, true);
                return creas.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Excluir CREAS
        /// </summary>
        /// <param name="idCREAS">Id do CREAS</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCREAS(Int32 idCREAS)
        {
            try
            {
                var u = new CREAS();
                u.Delete(u.GetById(idCREAS), true);
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #region Serviços e Recursos Financeiros
        /// <summary>
        /// Adicionar Serviço e Recursos Financeiros à um CREAS
        /// </summary>
        /// <param name="servico">Dados do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddServicoRecursoFinanceiroCREAS(ServicoRecursoFinanceiroCREASInfo servico)
        {
            try
            {
                new ServicoRecursoFinanceiroCREAS().Add(servico, true);
                return servico.Id;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception(ex.Message + "\n" + ex.InnerException.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Atualizar Serviço e Recursos Financeiros do CREAS
        /// </summary>
        /// <param name="servico">Dados do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateServicoRecursoFinanceiroCREAS(ServicoRecursoFinanceiroCREASInfo servico)
        {
            try
            {
                new ServicoRecursoFinanceiroCREAS().Update(servico, true);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception(ex.Message + "\n" + ex.InnerException.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Remover Serviço e Recurso Financeiro do CREAS
        /// </summary>
        /// <param name="idServico">Id do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteServicoRecursoFinanceiroCREAS(Int32 idServico)
        {
            try
            {
                var negocio = new ServicoRecursoFinanceiroCREAS();
                negocio.Delete(negocio.GetById(idServico), true);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception(ex.Message + "\n" + ex.InnerException.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Selecionar Serviço e Recursos Financeiros do CREAS através do Identificador
        /// </summary>
        /// <param name="idServico">Id do Serviço e Recurso Financeiro</param>
        /// <returns>Dados do Serviço e Recurso Financeiro</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ServicoRecursoFinanceiroCREASInfo GetServicoRecursoFinanceiroCREASById(Int32 idServico)
        {
            return new ServicoRecursoFinanceiroCREAS().GetById(idServico);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaServicosRecursosFinanceirosCREASInfo> GetConsultaServicosRecursosFinanceirosByCREAS(Int32 idCREAS)
        {
            return new ServicoRecursoFinanceiroCREAS().GetConsultaByCREAS(idCREAS).ToList();
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddServicoRecursoFinanceiroCREASRH(ServicoRecursoFinanceiroCREASRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroCREASRecursosHumanos().Add(recursoshumanos, true);
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
        public ServicoRecursoFinanceiroCREASRecursosHumanosInfo GetRecursosHumanosCREASByIdServicoRecursoFinanceiro(Int32 IdServicoRecursoFinanceiro)
        {
            ContextManager.OpenConnection();
            var obj = new ServicoRecursoFinanceiroCREASRecursosHumanos().GetByServicoRecursoFinanceiro(IdServicoRecursoFinanceiro);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateServicoRecursoFinanceiroCREASRH(ServicoRecursoFinanceiroCREASRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroCREASRecursosHumanos().Update(recursoshumanos, true);
                ContextManager.CloseConnection();

            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #endregion

        #region Previsao de instalacao CREAS
        /// <summary>
        /// Adicionar uma Previsão de Instalação de CREAS à Prefeitura
        /// </summary>
        /// <param name="previsaoInstalacaoCREAS">Dados da previsão de instalação do CREAS</param>
        /// <param name="idPrefeitura">Dados da previsão de instalação do CREAS</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SavePrevisaoInstalacaoCREAS(Boolean haPrevisao, List<PrevisaoInstalacaoCREASInfo> previsaoInstalacaoCREAS, List<MotivoNaoInstalacaoInfo> lstMotivos, Int32 idPrefeitura, Boolean naoHaDemanda, List<PrefeituraDemandaAtendimentoInfo> lstDemandas, Int32 idUnidade)
        {
            ContextManager.OpenConnection();

            try
            {
                string descricaoDefault = string.Empty;
                if (haPrevisao)
                    lstMotivos = new List<MotivoNaoInstalacaoInfo>();
                else
                    previsaoInstalacaoCREAS = new List<PrevisaoInstalacaoCREASInfo>();

                new ValidadorPrevisaoInstalacaoCREAS().Validar(haPrevisao, lstMotivos, previsaoInstalacaoCREAS, naoHaDemanda, lstDemandas);

                var lstDeleted = new List<PrevisaoInstalacaoCREASInfo>();
                var ppp = new PrevisaoInstalacaoCREAS();
                var lst = ppp.GetByPrefeitura(idPrefeitura);

                foreach (var p in lst)
                {
                    if (!previsaoInstalacaoCREAS.Any(t => t.Id == p.Id))
                    {
                        lstDeleted.Add(p);
                        descricaoDefault = Log.DeleteDescricaoDefaultData(lstDeleted.Select(t => t.Data.ToShortDateString()).ToList());
                    }
                }

                foreach (var p in lstDeleted)
                    ppp.Delete(p, false);

                if (previsaoInstalacaoCREAS.Count > 0)
                {
                    foreach (var p in previsaoInstalacaoCREAS)
                    {
                        if (p.Id == 0)
                            ppp.Add(p, false);
                    }
                    descricaoDefault = Log.CreateDescricaoDefaultData(previsaoInstalacaoCREAS.Select(t => t.Data.ToShortDateString()).ToList());
                }


                if (lstMotivos.Count > 0)
                {
                    descricaoDefault += Log.CreateMotivosDefault(lstMotivos.Select(t => t.Nome.ToString()).ToList());
                }

                new Prefeitura().SaveMotivosNaoInstalacaoCREAS(lstMotivos, idPrefeitura, false);

                var log = Log.CreateLog(idPrefeitura, EAcao.Update, 30, descricaoDefault, idUnidade);
                if (log != null)
                    new Log().Add(log, false);

                if (naoHaDemanda)
                    lstDemandas = new List<PrefeituraDemandaAtendimentoInfo>();

                var lstDeletedDemanda = new List<PrefeituraDemandaAtendimentoInfo>();
                var pppDemanda = new PrefeituraDemandaAtendimento();
                var lstdem = pppDemanda.GetByPrefeitura(idPrefeitura);

                foreach (var p in lstdem)
                    lstDeletedDemanda.Add(p);

                foreach (var p in lstDeletedDemanda)
                    pppDemanda.Delete(p, false);

                foreach (var p in lstDemandas)
                    pppDemanda.Add(p, false);

                ContextManager.Commit();
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Listar as previsões de Instalação de CREAS da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da prefeitura</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrevisaoInstalacaoCREASInfo> GetPrevisaoCREASByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lstPrevisao = new PrevisaoInstalacaoCREAS().GetByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lstPrevisao;
        }

        /// <summary>
        /// Listar os motivos de não instalação de CREAS da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da prefeitura</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCREASByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lstPrevisao = new Prefeitura().GetMotivoNaoInstalacaoCREASByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return lstPrevisao;
        }


        /// <summary>
        /// Listar os motivos de não instalação de CREAS da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da prefeitura</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrefeituraDemandaAtendimentoInfo> GetDemandaAtendimentoCREASByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lstDemanda = new PrefeituraDemandaAtendimento().GetByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lstDemanda;
        }


        #endregion
        #endregion

        #region Tipo Atendimento Centro Referencia
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public TipoAtendimentoInfo GetTipoAtendimentoCentroById(int id)
        {
            ContextManager.OpenConnection();
            var obj = new CREASMunicipio().GetTipoAtendimentoById(id);
            ContextManager.CloseConnection();
            return obj;
        }
        #endregion

        #region Centro POP
        /// <summary>
        /// Selecionar Centro POP pelo Identificador
        /// </summary>        
        /// <param name="idCentroPOP">Id do Centro POP</param>
        /// <returns>Dados do Centro POP</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public CentroPOPInfo GetCentroPOPById(Int32 idCentroPOP)
        {
            ContextManager.OpenConnection();
            var obj = new CentroPOP().GetById(idCentroPOP);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Nome do Centro POP pelo Identificador
        /// </summary>
        /// <param name="idCentroPOP">Id do Centro POP</param>
        /// <returns>Nome do Centro POP</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public String GetCentroPOPNomeById(Int32 idCentroPOP)
        {
            ContextManager.OpenConnection();
            var obj = new CentroPOP().GetAll().Where(t => t.Id == idCentroPOP).Select(t => t.Nome).FirstOrDefault();
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Centro POP por Prefeitura
        /// </summary>
        /// <param name="idUnidade">Id da Prefeitura</param>
        /// <param name="nome">Nome do Centro POP</param>
        /// <returns>Dados dos Centro POP</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaCentroPOPInfo> GetIdentificacaoCentroPOPByUnidade(Int32 idUnidade, String nome)
        {
            ContextManager.OpenConnection();
            if (!String.IsNullOrEmpty(nome))
            {
                var lst = new CentroPOP().GetConsultaByUnidade(idUnidade).Where(c => c.Nome.Contains(nome)).ToList();
                ContextManager.CloseConnection();
                return lst;
            }
            var lstCentroPOP = new CentroPOP().GetConsultaByUnidade(idUnidade).ToList();
            ContextManager.CloseConnection();
            return lstCentroPOP;
        }

        /// <summary>
        /// Atualizar Dados do Centro POP
        /// </summary>
        /// <param name="Centro POP">Dados do Centro POP</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateCentroPOP(CentroPOPInfo centro)
        {
            ContextManager.OpenConnection();
            try
            {
                new CentroPOP().Update(centro, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Adicionar um Centro POP à Prefeitura
        /// </summary>
        /// <param name="Centro POP">Dados do Centro POP</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddCentroPOPMunicipio(CentroPOPMunicipioInfo centro)
        {

            try
            {
                new CentroPOPMunicipio().Add(centro, true);

            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        public List<CentroPOPMunicipioInfo> GetMunicipiosAssociadosCentroPOP(int idCentroPop)
        {
            ContextManager.OpenConnection();

            try
            {
                var lst = new CentroPOPMunicipio().GetByCentroPop(idCentroPop).ToList();
                ContextManager.CloseConnection();
                return lst;
            }
            catch (Exception ex)
            {

                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo GetRecursosHumanosCentroPOPByIdServicoRecursoFinanceiro(Int32 IdServicoRecursoFinanceiro)
        {
            ContextManager.OpenConnection();
            var obj = new ServicoRecursoFinanceiroCentroPOPRecursosHumanos().GetByServicoRecursoFinanceiro(IdServicoRecursoFinanceiro);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Adicionar um Centro POP à Prefeitura
        /// </summary>
        /// <param name="Centro POP">Dados do Centro POP</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddCentroPOP(CentroPOPInfo centro)
        {

            try
            {
                new CentroPOP().Add(centro, true);
                return centro.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Excluir Centro POP
        /// </summary>
        /// <param name="idCentro POP">Id do Centro POP</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCentroPopMunicipios(Int32 idCentroPOP)
        {
            ContextManager.OpenConnection();
            try
            {
                var u = new CentroPOPMunicipio();
                var centroPopMunicipios = u.GetByCentroPop(idCentroPOP);
                foreach (var item in centroPopMunicipios)
                {
                    u.Delete(u.GetById(item.Id), true);
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
        /// Excluir Centro POP
        /// </summary>
        /// <param name="idCentro POP">Id do Centro POP</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCentroPOP(Int32 idCentroPOP)
        {
            ContextManager.OpenConnection();
            try
            {
                var u = new CentroPOP();
                u.Delete(u.GetById(idCentroPOP), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #region Serviços e Recursos Financeiros
        /// <summary>
        /// Adicionar Serviço e Recursos Financeiros à um CentroPOP
        /// </summary>
        /// <param name="servico">Dados do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddServicoRecursoFinanceiroCentroPOP(ServicoRecursoFinanceiroCentroPOPInfo servico)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroCentroPOP().Add(servico, true);
                ContextManager.CloseConnection();
                return servico.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Atualizar Serviço e Recursos Financeiros do CentroPOP
        /// </summary>
        /// <param name="servico">Dados do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateServicoRecursoFinanceiroCentroPOP(ServicoRecursoFinanceiroCentroPOPInfo servico)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroCentroPOP().Update(servico, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Remover Serviço e Recurso Financeiro do CentroPOP
        /// </summary>
        /// <param name="idServico">Id do Serviço e Recurso Financeiro</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteServicoRecursoFinanceiroCentroPOP(Int32 idServico)
        {
            ContextManager.OpenConnection();
            try
            {
                var negocio = new ServicoRecursoFinanceiroCentroPOP();
                negocio.Delete(negocio.GetById(idServico), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Selecionar Serviço e Recursos Financeiros do CentroPOP através do Identificador
        /// </summary>
        /// <param name="idServico">Id do Serviço e Recurso Financeiro</param>
        /// <returns>Dados do Serviço e Recurso Financeiro</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ServicoRecursoFinanceiroCentroPOPInfo GetServicoRecursoFinanceiroCentroPOPById(Int32 idServico)
        {
            ContextManager.OpenConnection();
            var obj = new ServicoRecursoFinanceiroCentroPOP().GetById(idServico);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaServicosRecursosFinanceirosCentroPOPInfo> GetConsultaServicosRecursosFinanceirosByCentroPOP(Int32 idCentroPOP)
        {
            ContextManager.OpenConnection();
            var lst = new ServicoRecursoFinanceiroCentroPOP().GetConsultaByCentroPOP(idCentroPOP).ToList();

            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddServicoRecursoFinanceiroCentroPOPRH(ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroCentroPOPRecursosHumanos().Add(recursoshumanos, true);
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
        public void UpdateServicoRecursoFinanceiroCentroPOPRH(ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo recursoshumanos)
        {
            ContextManager.OpenConnection();
            try
            {
                new ServicoRecursoFinanceiroCentroPOPRecursosHumanos().Update(recursoshumanos, true);
                ContextManager.CloseConnection();

            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        #endregion

        #region Previsao de instalacao Centro POP
        /// <summary>
        /// Adicionar uma Previsão de Instalação de Centro POP à Prefeitura
        /// </summary>
        /// <param name="cras">Dados da previsão de instalação do Centro POP</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SavePrevisaoInstalacaoCentroPOP(Boolean haPrevisao, List<PrevisaoInstalacaoCentroPOPInfo> previsaoInstalacaoCentroPOP, List<MotivoNaoInstalacaoInfo> lstMotivos, Int32 idPrefeitura, Int32 idUnidade)
        {
            ContextManager.OpenConnection();
            try
            {
                string descricaoDefault = string.Empty;
                if (haPrevisao)
                    lstMotivos = new List<MotivoNaoInstalacaoInfo>();
                else
                    previsaoInstalacaoCentroPOP = new List<PrevisaoInstalacaoCentroPOPInfo>();

                new ValidadorPrevisaoInstalacaoCentroPOP().Validar(haPrevisao, lstMotivos, previsaoInstalacaoCentroPOP);


                var lstDeleted = new List<PrevisaoInstalacaoCentroPOPInfo>();
                var ppp = new PrevisaoInstalacaoCentroPOP();
                var lst = ppp.GetByPrefeitura(idPrefeitura);

                foreach (var p in lst)
                    if (!previsaoInstalacaoCentroPOP.Any(t => t.Id == p.Id))
                        lstDeleted.Add(p);

                foreach (var p in lstDeleted)
                {
                    ppp.Delete(p, false);
                    descricaoDefault = Log.DeleteDescricaoDefaultData(lstDeleted.Select(t => t.Data.ToShortDateString()).ToList());
                }

                if (previsaoInstalacaoCentroPOP.Count > 0)
                {
                    foreach (var p in previsaoInstalacaoCentroPOP)
                    {
                        if (p.Id == 0)
                            ppp.Add(p, false);
                    }
                    descricaoDefault = Log.CreateDescricaoDefaultData(previsaoInstalacaoCentroPOP.Select(t => t.Data.ToShortDateString()).ToList());
                }

                if (lstMotivos.Count > 0)
                    descricaoDefault += Log.CreateMotivosDefault(lstMotivos.Select(t => t.Nome.ToString()).ToList());

                new Prefeitura().SaveMotivosNaoInstalacaoCentroPOP(lstMotivos, idPrefeitura, false);

                var log = Log.CreateLog(idPrefeitura, EAcao.Update, 35, descricaoDefault, idUnidade);
                if (log != null)
                    new Log().Add(log, false);
                ContextManager.Commit();
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Listar as previsões de Instalação de Centro POP da Prefeitura
        /// </summary>
        /// <param name="cras">Id da prefeitura</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PrevisaoInstalacaoCentroPOPInfo> GetPrevisaoCentroPOPByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lstPrevisao = new PrevisaoInstalacaoCentroPOP().GetByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lstPrevisao;
        }

        /// <summary>
        /// Listar os motivos de não instalação do Centro POP da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da prefeitura</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCentroPOPByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lstPrevisao = new Prefeitura().GetMotivoNaoInstalacaoCentroPOPByPrefeitura(idPrefeitura);
            ContextManager.CloseConnection();
            return lstPrevisao;
        }

        #endregion
        #endregion

        #region Analise Diagnostica
        /// <summary>
        /// Selecionar Análise Diagnóstica pelo Identificador
        /// </summary>
        /// <param name="IdAnaliseDiagnostica">Id da Análise Diagnóstica</param>
        /// <returns>Dados da Análise Diagnóstica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public AnaliseDiagnosticaInfo GetAnaliseDiagnosticaById(Int32 IdAnaliseDiagnostica)
        {
            ContextManager.OpenConnection();
            var obj = new AnaliseDiagnostica().GetById(IdAnaliseDiagnostica);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Análise Diagnóstica do Município
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Lista de Análise Diagnóstica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaAnaliseDiagnosticaInfo> GetConsultaAnaliseDiagnosticaByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new AnaliseDiagnostica().GetByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Análise Diagnóstica do Município
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Lista de Análise Diagnóstica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaAnaliseDiagnosticaPrefeituraExercicioInfo> GetAnaliseDiagnosticaByPrefeituraExercicio(int idPrefeitura, int Exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new AnaliseDiagnostica().GetAnaliseDiagnosticaPrefeituraExercicio(idPrefeitura, Exercicio);
            ContextManager.CloseConnection();
            return lst;
        }        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMunicipio"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaAnaliseDiagnosticaInfo> GetConsultaAnaliseDiagnosticaByMunicipio(int idMunicipio)
        {
            ContextManager.OpenConnection();
            var lst = new AnaliseDiagnostica().GetByMunicipio(idMunicipio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Atualizar Dados da Análise Diagnóstica
        /// </summary>
        /// <param name="analise">Dados da Análise Diagnóstica</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateAnaliseDiagnostica(AnaliseDiagnosticaInfo analise)
        {
            ContextManager.OpenConnection();
            try
            {
                new AnaliseDiagnostica().Update(analise, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Adicionar Análise Diagnóstica à Prefeitura
        /// </summary>
        /// <param name="analise">Dados da Analise Diagnostica</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Int32 AddAnaliseDiagnostica(AnaliseDiagnosticaInfo analise)
        {
            ContextManager.OpenConnection();
            try
            {
                new AnaliseDiagnostica().Add(analise, true);
                ContextManager.CloseConnection();
                return analise.Id;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Excluir Analise Diagnostica
        /// </summary>
        /// <param name="idAnaliseDiagnostica">Id da Analise Diagnostica</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAnaliseDiagnostica(Int32 idAnaliseDiagnostica)
        {
            ContextManager.OpenConnection();
            try
            {
                var u = new AnaliseDiagnostica();
                u.Delete(u.GetById(idAnaliseDiagnostica), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }
        #endregion

        #region Integração Pró Social / Instituições

        /// <summary>
        /// Selecionar Mantenedora pelo CNPJ
        /// </summary>        
        /// <param name="cnpj">CNPJ da Mantenedora</param>
        /// <returns>Dados da Mantenedora</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public MantenedoraProSocialInfo GetMantenedoraByCNPJ(String cnpj)
        {
            ContextManager.OpenConnection();
            var obj = new MantenedoraProSocial().GetByCNPJ(cnpj).FirstOrDefault();
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Lista de Mantenedoras
        /// </summary>        
        /// <returns>Lista com dados das Mantenedoras</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MantenedoraProSocialInfo> GetMantenedoras()
        {
            ContextManager.OpenConnection();
            var lst = new MantenedoraProSocial().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Lista das Áreas de Atuação
        /// </summary>        
        /// <returns>Lista com dados das Áreas de Atuação</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AreaAtuacaoProSocialInfo> GetAreaAtuacao()
        {
            ContextManager.OpenConnection();
            var lst = new AreaAtuacaoProSocial().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Lista da Situação da Inscrição
        /// </summary>        
        /// <returns>Lista com dados da Situação da Inscrição</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<SituacaoInscricaoInfo> GetSituacaoInscricao()
        {
            ContextManager.OpenConnection();
            var lst = new SituacaoInscricao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Lista da Situação da Inscrição
        /// </summary>        
        /// <returns>Lista com dados da Situação Atual da Inscrição</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<SituacaoAtualInscricaoInfo> GetSituacaoAtualInscricao()
        {
            ContextManager.OpenConnection();
            var lst = new SituacaoAtualInscricao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;

        }

        /// <summary>
        /// Selecionar Lista da Situação da Inscrição
        /// </summary>        
        /// <returns>Lista com dados da Situação Atual da Inscrição</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<UnidadeTipoAtendimentoInfo> GetTipoAtendimento()
        {
            ContextManager.OpenConnection();
            var lst = new UnidadeTipoAtendimento().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }



        #endregion

        #region Intenção de ação
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaIntencaoAcaoInfo> GetConsultaConsultaIntencaoAcoesByPrefeitura(int idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new PrefeituraAcaoPlanejamento().GetByIntencaoAcaoPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaIntencaoAcaoServicoSocioassistencialInfo> GetConsultaIntecaoServicosByPrefeitura(int idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new PrefeituraAcaoPlanejamento().GetByIntencaoServicoPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        #endregion

        #region comunidade

        /// <summary>
        /// Selecionar Análise Diagnóstica pelo Identificador
        /// </summary>
        /// <param name="IdAnaliseDiagnostica">Id da Análise Diagnóstica</param>
        /// <returns>Dados da Análise Diagnóstica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public  AnaliseDiagnosticaComunidadeInfo GetAnaliseDiagnosticaComunidadeByPrefeitura(Int32 IdPrefeitura,int idExercicio)
        {
            ContextManager.OpenConnection();
            var obj = new AnaliseDiagnosticaComunidade().GetByPrefeitura(IdPrefeitura,idExercicio);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Análise Diagnóstica pelo ID_PREFEITURA E ID_EXERCICIO
        /// </summary>
        /// <param name="IdAnaliseDiagnostica">Id da Análise Diagnóstica</param>
        /// <returns>Dados da Análise Diagnóstica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public AnaliseDiagnosticaComunidadeInfo GetAnaliseDiagnosticaComunidadeByPrefeituraExercicio(Int32 IdPrefeitura, Int32 IdExercicio)
        {
            ContextManager.OpenConnection();
            var obj = new AnaliseDiagnosticaComunidade().GetByPrefeituraExercicio(IdPrefeitura,IdExercicio);
            ContextManager.CloseConnection();
            return obj;
        }


        /// <summary>
        /// Selecionar Análise Diagnóstica pelo Identificador
        /// </summary>
        /// <param name="IdAnaliseDiagnostica">Id da Análise Diagnóstica</param>
        /// <returns>Dados da Análise Diagnóstica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddAnaliseDiagnosticaComunidade(AnaliseDiagnosticaComunidadeInfo obj)
        {
            ContextManager.OpenConnection();
            new AnaliseDiagnosticaComunidade().Add(obj, true);
            ContextManager.CloseConnection();
        }

        /// <summary>
        /// Selecionar Análise Diagnóstica pelo Identificador
        /// </summary>
        /// <param name="IdAnaliseDiagnostica">Id da Análise Diagnóstica</param>
        /// <returns>Dados da Análise Diagnóstica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateAnaliseDiagnosTicaComunidade(AnaliseDiagnosticaComunidadeInfo obj)
        {
            ContextManager.OpenConnection();
            new AnaliseDiagnosticaComunidade().Update(obj, true);
            ContextManager.CloseConnection();
        }
        #endregion comunidade

        #region Motivo Desativacao

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MotivoDesativacaoServicoInfo> GetMotivoDesativacaoServico()
        {
            ContextManager.OpenConnection();
            var lst = new MotivoDesativacaoServico().GetAll().Where(m => m.Ativo == true).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MotivoDesativacaoLocalInfo> GetMotivoDesativacaoLocal()
        {
            ContextManager.OpenConnection();
            var lst = new MotivoDesativacaoLocal().GetAll().Where(m => m.Ativo == true).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        public MotivoDesativacaoServicoInfo GetMotivoDesativacaoServicoById(int id)
        {
            ContextManager.OpenConnection();
            var obj = new MotivoDesativacaoServico().GetById(id);
            ContextManager.CloseConnection();
            return obj;
        }

        public MotivoDesativacaoLocalInfo GetMotivoDesativacaoLocalById(int id)
        {
            ContextManager.OpenConnection();
            var obj = new MotivoDesativacaoLocal().GetById(id);
            ContextManager.CloseConnection();
            return obj;
        }
        #endregion Motivo Desativacao


    }
}
