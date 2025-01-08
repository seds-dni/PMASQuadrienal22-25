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

namespace Seds.PMAS.QUADRIENAL.Servicos
{
    /// <summary>
    /// Serviço Responsável por fornecer informações sobre os Programas Sociais do PMAS 2017
    /// </summary>
    [ServiceBehavior(Namespace = "http://seds.sp.gov.br/programas",
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerSession,
    TransactionIsolationLevel = IsolationLevel.ReadCommitted,
    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class ProgramasService : IProgramasService
    {
        #region Programas/Projetos
        /// <summary>
        /// Selecionar Programas e Projetos da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Programas e Projetos</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaProgramaProjetoInfo> GetConsultaProgramasProjetosByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjeto().GetConsultaByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Programas e Projetos da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Programas e Projetos</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public MetaFamiliaPaulistaInfo GetMetaFamiliaPaulista(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var obj = new ProgramaProjeto().GetMetaFamiliaPaulista(idPrefeitura);
            ContextManager.CloseConnection();
            return obj;
        }



        /// <summary>
        /// Selecionar todos Programas e  Projetos da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Programas</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaProgramaProjetoInfo> GetProgramasByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjeto().GetProgramasByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Programas e Projetos Estaduais da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Programas e Projetos Estaduais</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaProgramaProjetoInfo> GetConsultaProgramasProjetosEstaduaisByPrefeitura(Int32 idPrefeitura)  
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjeto().GetConsultaEstadualByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Programas e Projetos Estaduais da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Programas e Projetos Estaduais</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaProgramaProjetoExercicioInfo> GetConsultaProgramasProjetosEstaduaisExercicioByPrefeitura(Int32 idPrefeitura, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjeto().GetConsultaEstadualExercicioByPrefeitura(idPrefeitura, exercicio).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Programas e Projetos Federais da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Programas e Projetos Federais</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaProgramaProjetoInfo> GetConsultaProgramasProjetosFederaisByPrefeitura(Int32 idPrefeitura)  
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjeto().GetConsultaFederalByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        /// <summary>
        /// Selecionar Programas e Projetos Federais da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados do Programas e Projetos Federais</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaProgramaProjetoExercicioInfo> GetConsultaProgramasProjetosFederaisExercicioByPrefeitura(Int32 idPrefeitura, Int32 exercicio)
        {
            ContextManager.OpenConnection();
            List<ConsultaProgramaProjetoExercicioInfo> lst = new ProgramaProjeto().GetConsultaFederalExercicioByPrefeitura(idPrefeitura, exercicio);
            ContextManager.CloseConnection();
            return lst;
        }


        /// <summary>
        /// Selecionar Programa/Projeto pelo Identificador
        /// </summary>
        /// <param name="idProgramaProjeto">Id do Programa/Projeto</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ProgramaProjetoInfo GetProgramaProjetoById(Int32 idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            var obj = new ProgramaProjeto().GetById(idProgramaProjeto);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Adicionar Programa/Projeto
        /// </summary>
        /// <param name="projeto">Dados do Programa/Projeto</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddProgramaProjeto(ProgramaProjetoInfo projeto)
        {
            //A CONEXÃO É ABERTA DENTRO DO TRANSACTION SCOPE DO MÉTODO ADD DO PROGRAMA/PROJETO
            try
            {
                new ProgramaProjeto().Add(projeto, true);
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Atualizar Programa/Projeto
        /// </summary>
        /// <param name="projeto">Dados do Programa/Projeto</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateProgramaProjeto(ProgramaProjetoInfo projeto)
        {
            ContextManager.OpenConnection();
            try
            {
                new ProgramaProjeto().Update(projeto, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Remover Programa/Projeto
        /// </summary>
        /// <param name="idProgramaProjeto">Id do Programa/Projeto</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProgramaProjeto(Int32 idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            try
            {
                var p = new ProgramaProjeto();
                p.Delete(p.GetById(idProgramaProjeto), true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Buscar as caracterizações do usuários do Programa 
        /// </summary>
        /// <param name="idProgramaProjeto"></param>
        /// <returns>retorna lista de caracterizações marcadas pelo usuário</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public List<CaracterizacaoUsuariosInfo> GetCaracterizacaoUsuariosByPrograma(int idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjeto().GetCaracterizacaoUsuariosByPrograma(idProgramaProjeto);
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Busca as Ações desenvolvida pelo programa: pela entidade 
        /// </summary>
        /// <param name="idProgramaProjeto"></param>
        /// <returns>lista de Ações desenvolvida selecionadas pelo usuário</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public List<AcoesDesenvolvidaProgramasInfo> GetAcoesDesenvolvidasByPrograma(int idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjeto().GetAcoesDesenvolvidasByPrograma(idProgramaProjeto);
            ContextManager.CloseConnection();
            return lst;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProgramaProjeto"></param>
        /// <returns></returns>
        public List<UnidadeOfertanteInfo> GetUnidadesOfertantesProgramaProjeto(int idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjeto().GetUnidadesOfertantesProgramaProjeto(idProgramaProjeto);
            ContextManager.CloseConnection();
            return lst;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProgramaProjeto"></param>
        /// <returns></returns>
        public List<AcaoSocioAssistencialInfo> GetAcoesSocioAssistenciaisByPrograma(int idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjeto().GetAcoesSocioAssistenciaisByPrograma(idProgramaProjeto);
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Busca as unidades privadas selecionadas pelo usuário
        /// </summary>
        /// <param name="idProgramaProjeto"></param>
        /// <returns>lista de unidades privadas selecionadas pelo usuario</returns>
        public List<UnidadePrivadaInfo> GetUnidadesPrivadasByPrograma(int idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjeto().GetUnidadesPrivadasByPrograma(idProgramaProjeto);
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Cofinanciamento do Programa/Projeto pelo Identificador
        /// </summary>
        /// <param name="idProgramaProjetoCofinanciamento">Id do Cofinanciamento</param>
        /// <returns>Dados do Cofinanciamento</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ProgramaProjetoCofinanciamentoInfo GetProgramaProjetoCofinanciamentoById(Int32 idProgramaProjetoCofinanciamento)
        {
            ContextManager.OpenConnection();
            var obj = new ProgramaProjetoCofinanciamento().GetById(idProgramaProjetoCofinanciamento);
            ContextManager.CloseConnection();
            return obj;
        }




        /// <summary>
        /// Selecionar Cofinanciamentos do Programa/Projetos
        /// </summary>
        /// <param name="idProgramaProjeto"></param>
        /// <returns>Dados do Cofinanciamento</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaProgramaProjetoServicoCofinanciamentoInfo> GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(Int32 idServicosRecursosFinanceiros, Int32 idLocal)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjetoCofinanciamento().GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(idServicosRecursosFinanceiros, idLocal);
            ContextManager.CloseConnection();
            return lst;
        }


        /// <summary>
        /// Selecionar Cofinanciamentos do Programa/Projetos
        /// </summary>
        /// <param name="idProgramaProjeto"></param>
        /// <returns>Dados do Cofinanciamento</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaProgramaProjetoServicoCofinanciamentoFundosInfo> GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceirosFundos(Int32 idServicosRecursosFinanceiros, Int32 idLocal)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjetoCofinanciamento().GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceirosFundos(idServicosRecursosFinanceiros, idLocal);
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Cofinanciamentos do Programa/Projetos
        /// </summary>
        /// <param name="idProgramaProjeto"></param>
        /// <returns>Dados do Cofinanciamento</returns>
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        //[OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaProgramaProjetoCofinanciamentoInfo> GetConsultaProgramaProjetoCofinanciamentoByProgramaProjeto(Int32 idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjetoCofinanciamento().GetConsultaByProgramaProjeto(idProgramaProjeto);
            ContextManager.CloseConnection();
            return lst;
        }

        public List<ConsultarServicosDiretrizesInfo> GetConsultaServicosDiretrizes(Int32 idPrefeitura, Int32 idAnalise,Int32 Exercicio)
        {
            ContextManager.OpenConnection();
            var lst = new ProgramaProjetoCofinanciamento().GetConsultaByServicosDiretrizes(idPrefeitura,idAnalise, Exercicio);
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Adicionar Cofinanciamento do Programa/Projeto
        /// </summary>
        /// <param name="cofinanciamento">Dados do Cofinanciamento</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddProgramaProjetoCofinanciamento(ProgramaProjetoCofinanciamentoInfo cofinanciamento)
        {
            ContextManager.OpenConnection();
            try
            {
                new ProgramaProjetoCofinanciamento().Add(cofinanciamento, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Atualizar Cofinanciamento do Programa/Projeto
        /// </summary>
        /// <param name="cofinanciamento">Dados do Cofinanciamento</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateProgramaProjetoCofinanciamento(ProgramaProjetoCofinanciamentoInfo cofinanciamento)
        {
            ContextManager.OpenConnection();
            try
            {
                new ProgramaProjetoCofinanciamento().Update(cofinanciamento, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }


        public ProgramaProjetoPrevisaoAnualBeneficiariosInfo GetPrevisaoAnualByProgramaProjeto(Int32 idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            try
            {
                var previsao = new ProgramaProjetoPrevisaoAnualBeneficiarios().GetByProgramaProjeto(idProgramaProjeto);
                ContextManager.CloseConnection();
                return previsao;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }

        }
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProgramaProjetoPrevisaoAnualBeneficiarios(int idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            try
            {
                var previsao = new ProgramaProjetoPrevisaoAnualBeneficiarios();
                var obj = previsao.GetByProgramaProjeto(idProgramaProjeto);
                if (obj != null)
                    previsao.Delete(obj, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }

        }

        /// <summary>
        /// Remover Cofinanciamento do Programa/Projeto
        /// </summary>
        /// <param name="idProgramaProjetoCofinanciamento">Id do Cofinanciamento</param>
        /// <param name="tipoConfinanciamento">Tipo do Cofinanciamento  1- ProgramaProjeto /  2 - Transferencia de Renda </param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProgramaProjetoCofinanciamento(Int32 idProgramaProjetoCofinanciamento, Int32 tipoCofinanciamento)
        {
            ContextManager.OpenConnection();
            try
            {
                if (tipoCofinanciamento == 1)
                {
                    var p = new ProgramaProjetoCofinanciamento();
                    p.Delete(p.GetFullById(idProgramaProjetoCofinanciamento), true, true);
                }
                else if (tipoCofinanciamento == 2)
                {
                    var t = new TransferenciaRendaCofinanciamento();
                    t.Delete(t.GetFullById(idProgramaProjetoCofinanciamento), true, true);
                }
                else
                {
                    var b = new PrefeituraBeneficioEventualServico();
                    b.Delete(b.GetFullById(idProgramaProjetoCofinanciamento), true, true);

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
        /// Selecionar Cofinanciamentos dos Programas/Projetos por Recurso Financeiro
        /// </summary>
        ///// <param name="idProgramaProjetoCofinanciamento">Id do Recurso Financeiro</param>
        /// <returns>Dados do Cofinanciamento</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ProgramaProjetoCofinanciamentoInfo> GetProgramaProjetoCofinanciamentoByServicoRecursoFinanceiro(Int32 idServicoRecursoFinanceiro, Int32 idTipoRecurso)
        {
            ContextManager.OpenConnection();
            var programaProjetoCofinanciamento = new ProgramaProjetoCofinanciamento();
            List<ProgramaProjetoCofinanciamentoInfo> lst = null;
            switch (idTipoRecurso)
            {
                case 1:
                    lst = programaProjetoCofinanciamento.GetAll().Where(p => p.IdServicosRecursosFinanceirosPublico == idServicoRecursoFinanceiro).ToList();
                    break;
                case 2:
                    lst = programaProjetoCofinanciamento.GetAll().Where(p => p.IdServicosRecursosFinanceirosPrivado == idServicoRecursoFinanceiro).ToList();
                    break;
                case 3:
                    lst = programaProjetoCofinanciamento.GetAll().Where(p => p.IdServicosRecursosFinanceirosCRAS == idServicoRecursoFinanceiro).ToList();
                    break;
                case 4:
                    lst = programaProjetoCofinanciamento.GetAll().Where(p => p.IdServicosRecursosFinanceirosCREAS == idServicoRecursoFinanceiro).ToList();
                    break;
                case 5:
                    lst = programaProjetoCofinanciamento.GetAll().Where(p => p.IdServicosRecursosFinanceirosCentroPOP == idServicoRecursoFinanceiro).ToList();
                    break;
            }
            for (int i = 0; i < lst.Count; i++)
            {
                lst[i] = programaProjetoCofinanciamento.GetFull(lst[i]);
            }
            ContextManager.CloseConnection();
            return lst;
        }
        #endregion

        #region Transferência Renda
        /// <summary>
        /// Selecionar Transferências de Renda de Programas Estaduais da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados da Transferências de Renda</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaTransferenciaRendaInfo> GetConsultaTransferenciasRendaEstadualByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new TransferenciaRenda().GetConsultaProgramasEstaduaisByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Transferências de Renda de Programas Federais da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados da Transferências de Renda</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaTransferenciaRendaInfo> GetConsultaTransferenciasRendaFederalByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new TransferenciaRenda().GetConsultaProgramasFederaisByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Transferências de Renda de Programas Municipais da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura"></param>
        /// <returns>Dados da Transferências de Renda</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaTransferenciaRendaInfo> GetConsultaTransferenciasRendaMunicipalByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new TransferenciaRenda().GetConsultaProgramasMunicipaisByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }



        /// <summary>
        /// Selecionar Benefícios Continuados da Prefeitura
        /// </summary>
        /// <param name="idPrefeitura">Id da Prefeitura</param>
        /// <returns>Dados dos Benefícios Continuados</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaTransferenciaRendaInfo> GetConsultaBeneficiosContinuadosByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new TransferenciaRenda().GetConsultaBeneficiosContinuadosByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Transferência Renda pelo Identificador
        /// </summary>
        /// <param name="idTransferenciaRenda">Id do Transferência Renda</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public TransferenciaRendaInfo GetTransferenciaRendaById(Int32 idTransferenciaRenda)
        {
            ContextManager.OpenConnection();
            var obj = new TransferenciaRenda().GetById(idTransferenciaRenda);
            ContextManager.CloseConnection();
            return obj;
        }



        /// <summary>
        /// Adicionar Transferência Renda
        /// </summary>
        /// <param name="projeto">Dados do Transferência Renda</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddTransferenciaRenda(TransferenciaRendaInfo projeto)
        {
            //A CONEXÃO É ABERTA DENTRO DO TRANSACTION SCOPE DO MÉTODO ADD DA TRANSFERENCIA DE RENDA
            try
            {
                new TransferenciaRenda().Add(projeto, true);
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Atualizar Transferência Renda
        /// </summary>
        /// <param name="projeto">Dados do Transferência Renda</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateTransferenciaRenda(TransferenciaRendaInfo projeto)
        {
            ContextManager.OpenConnection();
            try
            {
                new TransferenciaRenda().Update(projeto, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveValoresSaoPauloSolidario(TransferenciaRendaInfo t)
        {
            ContextManager.OpenConnection();
            try
            {
                new TransferenciaRenda().SaveRecursosSaoPauloSolidario(t, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Remover Transferência Renda
        /// </summary>
        /// <param name="idTransferenciaRenda">Id do Transferência Renda</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTransferenciaRenda(Int32 idTransferenciaRenda)
        {
            ContextManager.OpenConnection();
            try
            {
              var p = new TransferenciaRenda();
              p.Delete(p.GetById(idTransferenciaRenda), true);
              ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Selecionar Cofinanciamento do Transferência Renda pelo Identificador
        /// </summary>
        /// <param name="idTransferenciaRendaCofinanciamento">Id do Cofinanciamento</param>
        /// <returns>Dados do Cofinanciamento</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ServicoRecursoFinanceiroTransferenciaRendaInfo GetTransferenciaRendaCofinanciamentoById(Int32 idTransferenciaRendaCofinanciamento)
        {
            ContextManager.OpenConnection();
            var obj = new TransferenciaRendaCofinanciamento().GetById(idTransferenciaRendaCofinanciamento);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Cofinanciamentos da Transferência de Renda
        /// </summary>
        /// <param name="idTransferenciaRenda">Id da Transferência de Renda</param>
        /// <returns>Dados do Cofinanciamento</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaTransferenciaRendaCofinanciamentoInfo> GetConsultaTransferenciaRendaCofinanciamentoByTransferenciaRenda(Int32 idTransferenciaRenda)
        {
            ContextManager.OpenConnection();
            var obj = new TransferenciaRendaCofinanciamento().GetConsultaByTransferenciaRenda(idTransferenciaRenda);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Adicionar Cofinanciamento da Transferência Renda
        /// </summary>
        /// <param name="cofinanciamento">Dados do Cofinanciamento</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddTransferenciaRendaCofinanciamento(ServicoRecursoFinanceiroTransferenciaRendaInfo cofinanciamento)
        {
            ContextManager.OpenConnection();
            try
            {
                new TransferenciaRendaCofinanciamento().Add(cofinanciamento, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Atualizar Cofinanciamento da Transferência Renda
        /// </summary>
        /// <param name="cofinanciamento">Dados do Cofinanciamento</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateTransferenciaRendaCofinanciamento(ServicoRecursoFinanceiroTransferenciaRendaInfo cofinanciamento)
        {
            ContextManager.OpenConnection();
            try
            {
                new TransferenciaRendaCofinanciamento().Update(cofinanciamento, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        /// <summary>
        /// Remover Cofinanciamento da Transferência Renda
        /// </summary>
        /// <param name="idTransferenciaRendaCofinanciamento">Id do Cofinanciamento</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTransferenciaRendaCofinanciamento(Int32 idTransferenciaRendaCofinanciamento)
        {
            ContextManager.OpenConnection();
            try
            {
                var p = new TransferenciaRendaCofinanciamento();
                p.Delete(p.GetFullById(idTransferenciaRendaCofinanciamento), true, true);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

         
        /// <summary>
        /// Selecionar Cofinanciamentos da Transferência de Renda por Recurso Financeiro
        /// </summary>
        /// <param name="idTransferenciaRenda">Id do Recurso Financeiro</param>
        /// <returns>Dados do Cofinanciamento</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ServicoRecursoFinanceiroTransferenciaRendaInfo> GetTransferenciaRendaCofinanciamentoByServicoRecursoFinanceiro(Int32 idServicoRecursoFinanceiro, Int32 idTipoRecurso)
        {
            ContextManager.OpenConnection();
            var transferenciaRendaCofinanciamento = new TransferenciaRendaCofinanciamento();
            List<ServicoRecursoFinanceiroTransferenciaRendaInfo> lst = null;
            switch (idTipoRecurso)
            {
                case 1:
                    lst = transferenciaRendaCofinanciamento.GetAll().Where(t => t.IdServicosRecursosFinanceirosPublico == idServicoRecursoFinanceiro).ToList();
                    break;
                case 2:
                    lst = transferenciaRendaCofinanciamento.GetAll().Where(t => t.IdServicosRecursosFinanceirosPrivado == idServicoRecursoFinanceiro).ToList();
                    break;
                case 3:
                    lst = transferenciaRendaCofinanciamento.GetAll().Where(t => t.IdServicosRecursosFinanceirosCRAS == idServicoRecursoFinanceiro).ToList();
                    break;
                case 4:
                    lst = transferenciaRendaCofinanciamento.GetAll().Where(t => t.IdServicosRecursosFinanceirosCREAS == idServicoRecursoFinanceiro).ToList();
                    break;
                case 5:
                    lst = transferenciaRendaCofinanciamento.GetAll().Where(t => t.IdServicosRecursosFinanceirosCentroPOP == idServicoRecursoFinanceiro).ToList();
                    break;
            }
            for (int i = 0; i < lst.Count; i++)
            {
                lst[i] = transferenciaRendaCofinanciamento.CriarTransferenciaRendaCofinanciamentoCompleto(lst[i]);
            }
            ContextManager.CloseConnection();
            return lst;
        }

        #region Estruturas PETI
         
        /// <summary>
        /// Selecionar Eixos de Atuação do PETI
        /// </summary>
        /// <returns>Eixos de Atuação do PETI</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PETIEixoAtuacaoInfo> GetPETIEixosAtuacao()
        {
            ContextManager.OpenConnection();
            var lst = new PETIEixoAtuacao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

         
        /// <summary>
        /// Selecionar Tipos de Ação do PETI
        /// </summary>
        /// <returns>Tipos de Ação do PETI</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PETITipoAcaoInfo> GetPETITiposAcao()
        {
            ContextManager.OpenConnection();
            var lst = new PETITipoAcao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

         
        /// <summary>
        /// Selecionar Tipos de Ação do PETI
        /// </summary>
        /// <param name="idEixoAtuacao">Id do Eixo de Atuação do PETI</param>
        /// <returns>Tipos de Ação do PETI</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PETITipoAcaoInfo> GetPETITiposAcaoByEixoAtuacao(Int32 idEixoAtuacao)
        {
            ContextManager.OpenConnection();
            var lst = new PETITipoAcao().GetByEixoAtuacao(idEixoAtuacao).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

         
        /// <summary>
        /// Selecionar Situações das Ações do PETI
        /// </summary>
        /// <returns>Situações das Ações do PETI</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PETISituacaoAcaoInfo> GetPETISituacoesAcao()
        {
            ContextManager.OpenConnection();
            var lst = new PETISituacaoAcao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

         
        /// <summary>
        /// Selecionar Indicadores do PETI do municipio
        /// <param name="idMunicipio">Id do município</param>
        /// </summary>
        /// <returns>Indicadores do PETI do municipio</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public PETIIndicadoresInfo GetPETIIndicadoresByMunicipio(Int32 idMunicipio)
        {
            ContextManager.OpenConnection();
            var obj = new PETIIndicadores().GetByMunicipio(idMunicipio);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Atualizar os Indicadores do PETI do municipio
        /// <param name="idMunicipio">Id do município</param>
        /// </summary>
        /// <returns>Indicadores do PETI do municipio</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdatePETIIndicadores(PETIIndicadoresInfo obj)
        {
            ContextManager.OpenConnection();
            new PETIIndicadores().Update(obj, true);
            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddTransferenciaRendaGestorAcao(TransferenciaRendaGestorAcaoInfo obj)
        {
            ContextManager.OpenConnection();
            new TransferenciaRendaGestorAcao().Add(obj, true);
            ContextManager.CloseConnection();

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateTransferenciaRendaGestorAcao(TransferenciaRendaGestorAcaoInfo obj)
        {
            ContextManager.OpenConnection();
            new TransferenciaRendaGestorAcao().Update(obj, true);
            ContextManager.CloseConnection();

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTransferenciaRendaGestorAcao(TransferenciaRendaGestorAcaoInfo obj)
        {
            ContextManager.OpenConnection();
            new TransferenciaRendaGestorAcao().Delete(obj);
            ContextManager.CloseConnection();
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public TransferenciaRendaGestorAcaoInfo GetTransferenciaRendaGestorAcaoByTransferenciaRenda(Int32 idTransferenciaRenda)
        {

            ContextManager.OpenConnection();
            var obj = new TransferenciaRendaGestorAcao().GetByIdTransferenciaRenda(idTransferenciaRenda);
            ContextManager.CloseConnection();
            return obj;

        }
        #endregion

        #endregion

        #region Beneficios Eventuais
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConsultaPrefeituraBeneficioEventualInfo> GetConsultaBeneficiosEventuaisByPrefeitura(Int32 idPrefeitura)
        {
            ContextManager.OpenConnection();
            var lst = new PrefeituraBeneficioEventual().GetConsultaByPrefeitura(idPrefeitura).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public PrefeituraBeneficioEventualInfo GetBeneficioEventualByPrefeituraETipoBeneficioEventual(Int32 idPrefeitura, Int32 idTipoBeneficioEventual)
        {
            ContextManager.OpenConnection();
            var obj = new PrefeituraBeneficioEventual().GetByPrefeituraETipoBeneficioEventual(idPrefeitura, idTipoBeneficioEventual);
            ContextManager.CloseConnection();
            return obj;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveBeneficioEventual(PrefeituraBeneficioEventualInfo obj)
        {
            ContextManager.OpenConnection();
            try
            {
                var idPrefeituraBeneficioEventual = new PrefeituraBeneficioEventual().GetAll().Where(t => t.IdPrefeitura == obj.IdPrefeitura && t.IdTipoBeneficioEventual == obj.IdTipoBeneficioEventual).Select(t => new { t.Id }).FirstOrDefault();
                if (idPrefeituraBeneficioEventual == null)
                {
                    new PrefeituraBeneficioEventual().Add(obj, true);
                    ContextManager.CloseConnection();
                    return;
                }
                obj.Id = idPrefeituraBeneficioEventual.Id;
                new PrefeituraBeneficioEventual().Update(obj, true);
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
        public void DeleteBeneficioEventual(Int32 idPrefeitura, Int32 idTipoBeneficioEventual)
        {
            ContextManager.OpenConnection();
            try
            {
                var idPrefeituraBeneficioEventual = new PrefeituraBeneficioEventual().GetAll().Where(t => t.IdPrefeitura == idPrefeitura && t.IdTipoBeneficioEventual == idTipoBeneficioEventual).Select(t => new { t.Id }).FirstOrDefault();
                if (idPrefeituraBeneficioEventual == null)
                {
                    ContextManager.CloseConnection();
                    return;
                }
                var p = new PrefeituraBeneficioEventual();
                p.Delete(p.GetById(idPrefeituraBeneficioEventual.Id), true);
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
        public List<ConsultaPrefeituraBeneficioEventualRecursoFinanceiroInfo> GetConsultaBeneficioEventualServicosByBeneficioEventual(Int32 idPrefeituraBeneficioEventual)
        {
            ContextManager.OpenConnection();
            var lst = new PrefeituraBeneficioEventualServico().GetConsultaByBeneficioEventual(idPrefeituraBeneficioEventual).OrderBy(S => S.Id).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Orgão Gestor")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddBeneficioEventualServico(PrefeituraBeneficioEventualServicoInfo servico)
        {
            ContextManager.OpenConnection();
            try
            {
                new PrefeituraBeneficioEventualServico().Add(servico, true);
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
        public void UpdateBeneficioEventualServico(PrefeituraBeneficioEventualServicoInfo servico)
        {
            ContextManager.OpenConnection();
            try
            {
                new PrefeituraBeneficioEventualServico().Update(servico, true);
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
        public void DeleteBeneficioEventualServico(Int32 idPrefeituraBeneficioEventualServico)
        {
            ContextManager.OpenConnection();
            try
            {
                var p = new PrefeituraBeneficioEventualServico();
                p.Delete(p.GetFullById(idPrefeituraBeneficioEventualServico), true, true);
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
        public List<PrefeituraBeneficioEventualServicoInfo> GetBeneficioEventualServicosByServicoRecursoFinanceiro(Int32 idServicoRecursoFinanceiro, Int32 idTipoRecurso)
        {
            ContextManager.OpenConnection();
            var beneficioEventualServico = new PrefeituraBeneficioEventualServico();
            List<PrefeituraBeneficioEventualServicoInfo> lst = null;
            switch (idTipoRecurso)
            {
                case 1:
                    lst = beneficioEventualServico.GetAll().Where(t => t.IdServicosRecursosFinanceirosPublico == idServicoRecursoFinanceiro).ToList();
                    break;
                case 2:
                    lst = beneficioEventualServico.GetAll().Where(t => t.IdServicosRecursosFinanceirosPrivado == idServicoRecursoFinanceiro).ToList();
                    break;
                case 3:
                    lst = beneficioEventualServico.GetAll().Where(t => t.IdServicosRecursosFinanceirosCRAS == idServicoRecursoFinanceiro).ToList();
                    break;
                case 4:
                    lst = beneficioEventualServico.GetAll().Where(t => t.IdServicosRecursosFinanceirosCREAS == idServicoRecursoFinanceiro).ToList();
                    break;
                case 5:
                    lst = beneficioEventualServico.GetAll().Where(t => t.IdServicosRecursosFinanceirosCentroPOP == idServicoRecursoFinanceiro).ToList();
                    break;
            }
            for (int i = 0; i < lst.Count; i++)
            {
                lst[i] = beneficioEventualServico.GetFull(lst[i]);
            }
            ContextManager.CloseConnection();
            return lst;
        }
        #endregion



        #region InterlocutorMunicipal
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public InterlocutorMunicipalInfo GetInterlocutorByProgramaProjeto(Int32 idProgramaProjeto)
        {
            ContextManager.OpenConnection();
            var obj = new InterlocutorMunicipal().GetByIdProgramaProjeto(idProgramaProjeto);
            ContextManager.CloseConnection();
            return obj;

        }
        #endregion




        public TransferenciaRendaPrevisaoAnualInfo GetPrevisaoAnualByTransferenciaRenda(int idTransferenciaRenda)
        {
            ContextManager.OpenConnection();
            var obj = new TransferenciaRendaPrevisaoAnual().GetByTransferenciaRenda(idTransferenciaRenda);
            ContextManager.CloseConnection();
            return obj;
        }
    }
}
