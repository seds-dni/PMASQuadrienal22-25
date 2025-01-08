using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Data.Objects;
using Seds.PMAS.QUADRIENAL.Persistencia;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.Seguranca.Token;
using System.Threading;
using Microsoft.IdentityModel.Claims;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class RecursosFinanceiros
    {
        public Boolean GetBloqueio()
        {
            var obj = (ContextManager.GetContext() as PMASContext).GetBloqueioQuadroFinanceiro();
            return obj.FirstOrDefault();
        }

        public int GetBloqueioLeiOrcamentaria()  
        {
            int situacao = (ContextManager.GetContext() as PMASContext).GetBloqueioQuadroLeiOrcamentaria().First();
            return situacao;
        }

        public void SaveBloqueio(Boolean desbloquear)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;
            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            (ContextManager.GetContext() as PMASContext).SaveBloqueioQuadroFinanceiro(!desbloquear, id, DateTime.Now);
        }

        public void SaveBloqueioLeiOrcamentaria(Boolean desbloquear)  
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;
            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            (ContextManager.GetContext() as PMASContext).SaveBloqueioQuadroLeiOrcamentaria(!desbloquear, id, DateTime.Now);
        }

        #region Lei Orcamentaria
        private static IRepository<LeiOrcamentariaInfo> _repositoryLeiOrcamentaria
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<LeiOrcamentariaInfo>>();
            }
        }

        public LeiOrcamentariaInfo GetLeiOrcamentariaByPrefeitura(int idPrefeitura, int exercicio)
        {
            return _repositoryLeiOrcamentaria.Single(m => m.IdPrefeitura == idPrefeitura && m.Exercicio== exercicio);
        }

        public LeiOrcamentariaInfo GetLeiOrcamentaria2016ByPrefeitura(int idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetLeiOrcamentaria2016ByPrefeitura(idPrefeitura);
        }

        public void AddLeiOrcamentaria(LeiOrcamentariaInfo pre, Boolean commit)
        {
            new ValidadorLeiOrcamentaria().Validar(pre);

            _repositoryLeiOrcamentaria.Add(pre);
            if (commit)
                ContextManager.Commit();
        }

        public void UpdateLeiOrcamentaria(LeiOrcamentariaInfo pre, Boolean commit)
        {
            new ValidadorLeiOrcamentaria().Validar(pre);
            _repositoryLeiOrcamentaria.Update(pre);
            if (commit)
                ContextManager.Commit();
        }

        #endregion

        #region Execucao Financeira
        private static IRepository<ExecucaoFinanceiraInfo> _repositoryExecucaoFinanceira
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ExecucaoFinanceiraInfo>>();
            }
        }
        private static IRepository<ComentarioExecucaoFinanceiraInfo> _repositoryComentarioExecucaoFinanceira
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ComentarioExecucaoFinanceiraInfo>>();
            }
        }
        private static IRepository<PrestacaoDeContasExecucaoFisicaInfo> _repositoryPrestacaoDeContasExecucaoFisica
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrestacaoDeContasExecucaoFisicaInfo>>();
            }
        }


        private static IRepository<PrestacaoDeContasExecucaoFisicaProgramasProjetosInfo> _repositoryPrestacaoDeContasExecucaoFisicaProgramaProjeto
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrestacaoDeContasExecucaoFisicaProgramasProjetosInfo>>();
            }
        }

        private static IRepository<PrestacaoDeContasDespesasInfo> _repositoryPrestacaoDeContasDespesas
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrestacaoDeContasDespesasInfo>>();
            }
        }


        private static IRepository<PrestacaoDeContasAplicacoesFinanceirasInfo> _repositoryPrestacaoDeContasAplicacoesFinanceiras
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrestacaoDeContasAplicacoesFinanceirasInfo>>();
            }
        }


        private static IRepository<LocaisExecucaoPrestacaoDeContasInfo> _repositoryLocaisExecucaoPrestacaoDeContas
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<LocaisExecucaoPrestacaoDeContasInfo>>();
            }
        }

        private static IRepository<ComentarioPrestacaoDeContasInfo> _repositoryComentarioPrestacaoDeContas
        {
            get 
            {
                return ObjectFactory.GetInstance<IRepository<ComentarioPrestacaoDeContasInfo>>();
            }
        }

        private static IRepository<ProgramaProjetoPrestacaoContasInfo> _repositoryPretacaoDeContasProgramasProjetos
        {
            get 
            {
                return ObjectFactory.GetInstance<IRepository<ProgramaProjetoPrestacaoContasInfo>>();
            }
        }

        private static IRepository<PrestacaoDeContasBeneficiosEventuaisInfo> _repositoryPrestacaoDeContasBeneficiosEventuais
        {
            get 
            {
                return ObjectFactory.GetInstance<IRepository<PrestacaoDeContasBeneficiosEventuaisInfo>>();
            }
        }

        private static IRepository<QuestoesDRADSInfo> _repositoryQuestoesDRADS
        {
            get 
            {
                return ObjectFactory.GetInstance<IRepository<QuestoesDRADSInfo>>();
            }
        }

        private static IRepository<HistoricoPrestacaoDeContasInfo> _repositoryHistoricoPrestacaoDeContas
        {
            get 
            {
                return ObjectFactory.GetInstance<IRepository<HistoricoPrestacaoDeContasInfo>>();
            }
        }
        private static IRepository<QuestoesCMASinfo> _repositoryQuestoesCMAS
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<QuestoesCMASinfo>>();
            }
        }

        private static IRepository<ComentarioPrestacaoDeContasCMASInfo>_repositoryComentarioPrestacaoDeContasCMAS
        {
            get 
            {
                return ObjectFactory.GetInstance<IRepository<ComentarioPrestacaoDeContasCMASInfo>>();
            }
        }

        private static IRepository<ComentarioPrestacaoDeContasDRADSInfo> _repositoryComentarioPrestacaoDeContasDRADS
        {
            get 
            {
                return ObjectFactory.GetInstance<IRepository<ComentarioPrestacaoDeContasDRADSInfo>>();
            }
        }

        private static IRepository<DeliberacaoPrestacaoDeContasCMASInfo> _repositoryDeliberacaoPrestacaoDeContasCMAS
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DeliberacaoPrestacaoDeContasCMASInfo>>();
            }
        }

        private static IRepository<DeliberacaoPrestacaoDeContasDRADSInfo> _repositoryDeliberacaoPrestacaoDeContasDRADS
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DeliberacaoPrestacaoDeContasDRADSInfo>>();
            }
        }

        private static IRepository<ComentarioExecucaoFinanceiraCMASInfo> _repositoryComentarioExecucaoFinanceiraCMAS
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ComentarioExecucaoFinanceiraCMASInfo>>();
            }
        }

       private static IRepository<DeliberacaoCMASInfo> _repositoryDeliberacaoCMAS
       {
           get 
           { 
               return ObjectFactory.GetInstance<IRepository<DeliberacaoCMASInfo>>();
           }
       }

        private static IRepository<ExecucaoRecursosCofinanciamentoEstadualInfo> _repositoryExecucaoRecursosCofinanciamentoEstadual
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ExecucaoRecursosCofinanciamentoEstadualInfo>>();
            }
        }
        public IQueryable<ExecucaoFinanceiraInfo> GetExecucaoFinanceiraByPrefeitura(int idPrefeitura)
        {
            return _repositoryExecucaoFinanceira.GetObjectSet().Include("TipoProtecaoSocial").Where(m => m.IdPrefeitura == idPrefeitura);
        }
        public IQueryable<ExecucaoRecursosCofinanciamentoEstadualInfo> GetExecucaoRecursosCofinanciamentoEstadualInfo(int idPrefeitura)
        {
            return _repositoryExecucaoRecursosCofinanciamentoEstadual.GetObjectSet().Include("TipoProtecaoSocial").Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public void SaveExecucaoFinanceira(ExecucaoFinanceiraInfo execucao, Boolean commit)
        {
            ValidarExecucaoFinanceira(execucao);

            if (!_repositoryExecucaoFinanceira.GetQuery()
                    .Any(e => e.IdPrefeitura == execucao.IdPrefeitura 
                      && e.IdTipoProtecao == execucao.IdTipoProtecao
                      && e.Exercicio == execucao.Exercicio))
            {
                _repositoryExecucaoFinanceira.Add(execucao);
            }
            else
            {
                _repositoryExecucaoFinanceira.Update(execucao);
            }
            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public void SavePrestacaoDeContasDespesas(PrestacaoDeContasDespesasInfo d,Boolean commit) 
        {
            if (!_repositoryPrestacaoDeContasDespesas.GetQuery().Any(e => e.IdPrefeitura == d.IdPrefeitura && e.IdTipoProtecao == d.IdTipoProtecao && e.Exercicio == d.Exercicio && e.IdServicosRecursosFinanceiros == d.IdServicosRecursosFinanceiros))
            {
                _repositoryPrestacaoDeContasDespesas.Add(d);
            }
            else
            {
                _repositoryPrestacaoDeContasDespesas.Update(d);
            }
            if (commit)
            {
                ContextManager.Commit();
            }
           
        }


        public void SavePrestacaoDeContasAplicacoesFinanceiras(PrestacaoDeContasAplicacoesFinanceirasInfo d, Boolean commit)
        {
            if (!_repositoryPrestacaoDeContasAplicacoesFinanceiras.GetQuery().Any(e => e.IdPrefeitura == d.IdPrefeitura && e.IdTipoProtecao == d.IdTipoProtecao && e.Exercicio == d.Exercicio && e.IdServicosRecursosFinanceiros == d.IdServicosRecursosFinanceiros))
            {
                _repositoryPrestacaoDeContasAplicacoesFinanceiras.Add(d);
            }
            else
            {
                _repositoryPrestacaoDeContasAplicacoesFinanceiras.Update(d);
            }
            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public void SavePrestacaoDeContasExecucaoFisica(PrestacaoDeContasExecucaoFisicaInfo e, Boolean commit)
        {
            if (!_repositoryPrestacaoDeContasExecucaoFisica.GetQuery().Any(ex => ex.IdPrefeitura == e.IdPrefeitura && ex.IdTipoProtecao == e.IdTipoProtecao  && ex.IdServicosRecursosFinanceiros == e.IdServicosRecursosFinanceiros && ex.IdTipoBeneficioEventual == e.IdTipoBeneficioEventual && ex.Exercicio == e.Exercicio))
            {
                _repositoryPrestacaoDeContasExecucaoFisica.Add(e);
            }
            else
            {
                _repositoryPrestacaoDeContasExecucaoFisica.Update(e);
            }
            if (commit)
            {
                ContextManager.Commit();
            }

        }

        public void SavePrestacaoDeContasExecucaoFisicaProgramaProjeto(PrestacaoDeContasExecucaoFisicaProgramasProjetosInfo e, Boolean commit)
        {
            if (!_repositoryPrestacaoDeContasExecucaoFisicaProgramaProjeto.GetQuery().Any(ep => ep.IdPrefeitura == e.IdPrefeitura && ep.IdTipoProtecao == e.IdTipoProtecao && ep.Exercicio == e.Exercicio && ep.IdServicosRecursosFinanceiros == e.IdServicosRecursosFinanceiros))
            {
                _repositoryPrestacaoDeContasExecucaoFisicaProgramaProjeto.Add(e);
            }
            else
            {
                _repositoryPrestacaoDeContasExecucaoFisicaProgramaProjeto.Update(e);
            }
            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public IQueryable<HistoricoPrestacaoDeContasInfo> GetHistoricoPrestacaoDeContas(int idPrefeitura, int idPerfil, int exercicio)
        {
            var historico = _repositoryHistoricoPrestacaoDeContas.GetQuery().Where(h => h.IdPrefeitura == idPrefeitura && h.IdPerfil == idPerfil && h.Exercicio == exercicio);
            return historico;
        }

        public IQueryable<HistoricoPrestacaoDeContasInfo> GetHistoricoPrestacaoDeContasDetalhes(int idPrefeitura, int exercicio)
        {
            var historico = _repositoryHistoricoPrestacaoDeContas.GetQuery().Where(h => h.IdPrefeitura == idPrefeitura && h.Exercicio == exercicio);

            return historico;
        }

        public IQueryable<HistoricoPrestacaoDeContasInfo> GetHistoricoPrestacaoDeContasID(int id)
        {
            var historico = _repositoryHistoricoPrestacaoDeContas.GetQuery().Where(h => h.Id == id);
            return historico;
        }

        public IQueryable<LocaisExecucaoPrestacaoDeContasInfo> GetLocaisDeExecucaoPrestacaoDeContas(int idPrefeitura,int idTipoProtecao,int exercicio)
        {
            var locais = _repositoryLocaisExecucaoPrestacaoDeContas.GetQuery().Where(l => l.IdPrefeitura == idPrefeitura && l.IdTipoProtecao == idTipoProtecao && l.Exercicio == exercicio);
            return locais;
        }

        public IQueryable<LocaisExecucaoPrestacaoDeContasInfo> GetLocaisDeExecucaoPrestacaoDeContasDespesas(int idServicosRecursosFinaceiros,int idPrefeitura)
        {
            var locais = _repositoryLocaisExecucaoPrestacaoDeContas.GetQuery().Where(l =>  l.Id == idServicosRecursosFinaceiros && l.IdPrefeitura == idPrefeitura);
            return locais;
        }

        public IQueryable<QuestoesCMASinfo> GetQuestionarioPrestacaoDeContasCMAS(int IdPrefeitura, int exercicio)
        {
            var comentarios = _repositoryQuestoesCMAS.GetQuery().Where(q => q.IdPrefeitura == IdPrefeitura && q.Exercicio == exercicio);
            return comentarios;
        }

        public IQueryable<QuestoesDRADSInfo> GetQuestionarioPrestacaoDeContasDRADS(int IdPrefeitura, int exercicio)
        {
            var comentarios = _repositoryQuestoesDRADS.GetQuery().Where(q => q.IdPrefeitura == IdPrefeitura && q.Exercicio == exercicio);

            return comentarios;
        }

        public IQueryable<ProgramaProjetoPrestacaoContasInfo> GetPrestacaoDeContasProgramaProjeto(int idPrefeitura, int exercicio)
        {
            var programa = _repositoryPretacaoDeContasProgramasProjetos.GetQuery().Where(p => p.IdPrefeitura == idPrefeitura && p.Exercicio == exercicio);
            return programa;
        }

        public IQueryable<ProgramaProjetoPrestacaoContasInfo> GetPrestacaoDeContasProgramaProjetoDespesas(int idProgramaProjeto, int idPrefeitura,int exercicio)
        {
            var programa = _repositoryPretacaoDeContasProgramasProjetos.GetQuery().Where(p => p.IdPrefeitura == idPrefeitura && p.Exercicio == exercicio && p.Id == idProgramaProjeto);
            return programa;
        }

        public IQueryable<PrestacaoDeContasBeneficiosEventuaisInfo> GetPrestacaoDeContasBeneficiosEventuais(int idPrefeitura, int exercicio) 
        {
            var beneficios = _repositoryPrestacaoDeContasBeneficiosEventuais.GetQuery().Where(b => b.IdPrefeitura == idPrefeitura && b.Exercicio == exercicio);
            return beneficios;
        }

        public IQueryable<PrestacaoDeContasBeneficiosEventuaisInfo> GetPrestacaoDeContasBeneficiosEventuaisDespesas(int idBeneficiosEventuais,int idPrefeitura, int exercicio)
        {
            var beneficios = _repositoryPrestacaoDeContasBeneficiosEventuais.GetQuery().Where(b => b.IdPrefeitura == idPrefeitura && b.Exercicio == exercicio && b.Id == idBeneficiosEventuais);
            return beneficios;
        }

        public IQueryable<PrestacaoDeContasDespesasInfo> GetPrestacaoDeContasDespesas(int idPrefeitura, int exercicio) 
        {
            var despesas = _repositoryPrestacaoDeContasDespesas.GetQuery().Where(b => b.IdPrefeitura == idPrefeitura && b.Exercicio == exercicio);
            return despesas;
        }

        public IQueryable<PrestacaoDeContasAplicacoesFinanceirasInfo> GetPrestacaoDeContasAplicacoesFinanceiras(int idPrefeitura, int exercicio)
        {
            var aplicacoes = _repositoryPrestacaoDeContasAplicacoesFinanceiras.GetQuery().Where(b => b.IdPrefeitura == idPrefeitura && b.Exercicio == exercicio);
            return aplicacoes;
        }

        public IQueryable<ComentarioExecucaoFinanceiraInfo> GetComentarioExecucaoFinanceiraByPrefeitura(int idPrefeitura)
        {
            var comentarios = _repositoryComentarioExecucaoFinanceira.GetQuery().Where(com => com.IdPrefeitura == idPrefeitura);
            return comentarios;
        }

        public IQueryable<ComentarioPrestacaoDeContasCMASInfo> GetComentarioPrestacaoDeContasCMAS (int idPrefeitura,int exercicio)
        {
            var comentarios = _repositoryComentarioPrestacaoDeContasCMAS.GetQuery().Where(com => com.IdPrefeitura == idPrefeitura && com.Exercicio == exercicio);
            return comentarios;
        }

        public IQueryable<PrestacaoDeContasExecucaoFisicaInfo> GetPrestacaoDeContasExecucaoFisica(int idPrefeitura, int exercicio)
        {
            var comentario = _repositoryPrestacaoDeContasExecucaoFisica.GetQuery().Where(e => e.IdPrefeitura == idPrefeitura && e.Exercicio == exercicio);

            return comentario;
        }

        public IQueryable<PrestacaoDeContasExecucaoFisicaProgramasProjetosInfo> GetPrestacaoDeContasExecucaoFisicaProgramaProjeto(int idPrefeitura, int exercicio)
        {
            var comentario = _repositoryPrestacaoDeContasExecucaoFisicaProgramaProjeto.GetQuery().Where(e => e.IdPrefeitura == idPrefeitura && e.Exercicio == exercicio);

            return comentario;
        }

        public IQueryable<ComentarioPrestacaoDeContasDRADSInfo> GetComentarioPrestacaoDeContasDRADS (int idPrefeitura ,int exercicio)
        {
            var comentarios = _repositoryComentarioPrestacaoDeContasDRADS.GetQuery().Where(com => com.IdPrefeitura == idPrefeitura && com.Exercicio == exercicio);
            return comentarios;
        }

        public IQueryable<ComentarioPrestacaoDeContasInfo> GetComentarioPrestacaoDeContas(int idPrefeitura, int exercicio) 
        {
            return _repositoryComentarioPrestacaoDeContas.GetQuery().Where(c => c.IdPrefeitura == idPrefeitura && c.Exercicio == exercicio);
        }

        public IQueryable<DeliberacaoCMASInfo> GetDeliberacao(int idPrefeitura, int exercicio)
        {
            return _repositoryDeliberacaoCMAS.GetQuery().Where(c => c.IdPrefeitura == idPrefeitura && c.Exercicio == exercicio);
             
        }

        public IQueryable<DeliberacaoPrestacaoDeContasCMASInfo> GetDeliberacaoPrestacaoDeContasCMAS(int idPrefeitura, int exercicio)
        {
            return _repositoryDeliberacaoPrestacaoDeContasCMAS.GetQuery().Where(c => c.IdPrefeitura == idPrefeitura && c.Exercicio == exercicio);

        }

        public IQueryable<DeliberacaoPrestacaoDeContasDRADSInfo> GetDeliberacaoPrestacaoDeContasDRADS(int idPrefeitura, int exercicio)
        {
            return _repositoryDeliberacaoPrestacaoDeContasDRADS.GetQuery().Where(c => c.IdPrefeitura == idPrefeitura && c.Exercicio == exercicio);
        }

        public IQueryable<ComentarioExecucaoFinanceiraCMASInfo> GetComentarioCMAS(int idPrefeitura, int exercicio) 
        {
            return _repositoryComentarioExecucaoFinanceiraCMAS.GetQuery().Where(c => c.IdPrefeitura == idPrefeitura && c.Exercicio == exercicio);   
        }

        public void SaveHistoricoPrestacaoDeContas(HistoricoPrestacaoDeContasInfo h, Boolean commit)
        {
                _repositoryHistoricoPrestacaoDeContas.Add(
                            new HistoricoPrestacaoDeContasInfo()
                            {
                                IdPrefeitura = h.IdPrefeitura
                                ,
                                IdPerfil = h.IdPerfil
                                ,
                                IdSituacaoQuadro = h.IdSituacaoQuadro
                                ,
                                NomeResponsavel = h.NomeResponsavel
                                ,
                                CPFResponsavel = h.CPFResponsavel
                                ,
                                PosicaoFinal = h.PosicaoFinal
                                ,
                                DescricaoMotivo = h.DescricaoMotivo
                                ,
                                DeAcordo = h.DeAcordo
                                ,
                                Data = h.Data
                                ,
                                Exercicio = h.Exercicio
                               
                            });            

            if (commit)
                ContextManager.Commit();
        }

        public void SaveComentarioExecucaoFinanceira(ComentarioExecucaoFinanceiraInfo comentarioExecucao, int idPrefeitura, Boolean commit)
        {
            var comentario = _repositoryComentarioExecucaoFinanceira.GetQuery()
                                                                    .FirstOrDefault(c => c.IdPrefeitura == idPrefeitura
                                                                                      && c.Exercicio == comentarioExecucao.Exercicio);
            if (comentario == null)
            {
                _repositoryComentarioExecucaoFinanceira.Add(
                            new ComentarioExecucaoFinanceiraInfo()
                            { 
                                  IdPrefeitura = idPrefeitura
                                , Comentario = comentarioExecucao.Comentario
                                , Exercicio = comentarioExecucao.Exercicio
                                , Desbloqueado = comentarioExecucao.Desbloqueado
                                , IdSituacao = comentarioExecucao.IdSituacao
                            });
            }
            else
            {
                comentario.Comentario = comentarioExecucao.Comentario;
                _repositoryComentarioExecucaoFinanceira.Update(comentario);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void SaveComentarioPrestacaoDeContas(ComentarioPrestacaoDeContasInfo comentarioExecucao, int idPrefeitura, Boolean commit)
        {
            var comentario = _repositoryComentarioPrestacaoDeContas.GetQuery()
                                                                    .FirstOrDefault(c => c.IdPrefeitura == idPrefeitura
                                                                                      && c.Exercicio == comentarioExecucao.Exercicio);
            if (comentario == null)
            {
                _repositoryComentarioPrestacaoDeContas.Add(
                            new ComentarioPrestacaoDeContasInfo()
                            {
                                IdPrefeitura = idPrefeitura
                                ,
                                Comentario = comentarioExecucao.Comentario
                                ,
                                Exercicio = comentarioExecucao.Exercicio
                                ,
                                Desbloqueado = comentarioExecucao.Desbloqueado
                                ,
                                IdSituacao = comentarioExecucao.IdSituacao
                            });
            }
            else
            {
                comentario.Comentario = comentarioExecucao.Comentario;
                _repositoryComentarioPrestacaoDeContas.Update(comentario);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void SaveComentarioPrestacaoDeContasCMAS(ComentarioPrestacaoDeContasCMASInfo comentarioExecucao, int idPrefeitura, Boolean commit)
        {
            var comentario = _repositoryComentarioPrestacaoDeContasCMAS.GetQuery().FirstOrDefault(c => c.IdPrefeitura == idPrefeitura && c.Exercicio == comentarioExecucao.Exercicio);

            if (comentario == null)
            {
                _repositoryComentarioPrestacaoDeContasCMAS.Add(
                            new ComentarioPrestacaoDeContasCMASInfo()
                            {
                                IdPrefeitura = idPrefeitura
                                ,
                                Comentario = comentarioExecucao.Comentario
                                ,
                                Exercicio = comentarioExecucao.Exercicio
                                ,
                                Desbloqueado = comentarioExecucao.Desbloqueado
                                ,
                                IdSituacao = comentarioExecucao.IdSituacao
                            });
            }
            else
            {
                comentario.Comentario = comentarioExecucao.Comentario;
                _repositoryComentarioPrestacaoDeContasCMAS.Update(comentario);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void SaveComentarioPrestacaoDeContasDRADS(ComentarioPrestacaoDeContasDRADSInfo comentarioExecucao, int idPrefeitura, Boolean commit)
        {
            var comentario = _repositoryComentarioPrestacaoDeContasDRADS.GetQuery().FirstOrDefault(c => c.IdPrefeitura == idPrefeitura && c.Exercicio == comentarioExecucao.Exercicio);

            if (comentario == null)
            {
                _repositoryComentarioPrestacaoDeContasDRADS.Add(
                            new ComentarioPrestacaoDeContasDRADSInfo()
                            {
                                IdPrefeitura = idPrefeitura
                                ,
                                Comentario = comentarioExecucao.Comentario
                                ,
                                Exercicio = comentarioExecucao.Exercicio
                                ,
                                Desbloqueado = comentarioExecucao.Desbloqueado
                                ,
                                IdSituacao = comentarioExecucao.IdSituacao
                            });
            }
            else
            {
                comentario.Comentario = comentarioExecucao.Comentario;
                _repositoryComentarioPrestacaoDeContasDRADS.Update(comentario);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void SaveQuestionarioPrestacaoDeContasCMAS(QuestoesCMASinfo questionarioCMAS,int idPrefeitura,Boolean commit) 
        {
            var questionario = _repositoryQuestoesCMAS.GetQuery().FirstOrDefault(q => q.IdPrefeitura == idPrefeitura && q.Exercicio == questionarioCMAS.Exercicio);

            if (questionario == null)
            {
                _repositoryQuestoesCMAS.Add(
                    new QuestoesCMASinfo()
                    {

                        IdPrefeitura = idPrefeitura
                        ,
                        QuestaoUm = questionarioCMAS.QuestaoUm
                        ,
                        QuestaoDois = questionarioCMAS.QuestaoDois
                        ,
                        QuestaoTres = questionarioCMAS.QuestaoTres
                        ,
                        QuestaoQuatro = questionarioCMAS.QuestaoQuatro
                        ,
                        QuestaoCinco = questionarioCMAS.QuestaoCinco
                        ,
                        QuestaoSeis = questionarioCMAS.QuestaoSeis
                        ,
                        QuestaoSeisEscrita = questionarioCMAS.QuestaoSeisEscrita
                        ,
                        QuestaoSete = questionarioCMAS.QuestaoSete
                        ,
                        QuestaoSeteEscrita = questionarioCMAS.QuestaoSeteEscrita
                        ,
                        QuestaoOito = questionarioCMAS.QuestaoOito
                        ,
                        QuestaoNove = questionarioCMAS.QuestaoNove
                        ,
                        Exercicio = questionarioCMAS.Exercicio

                    });
            }
            else
            {
                        questionario.QuestaoUm = questionarioCMAS.QuestaoUm
                        ;
                        questionario.QuestaoDois = questionarioCMAS.QuestaoDois
                        ;
                        questionario.QuestaoTres = questionarioCMAS.QuestaoTres
                        ;
                        questionario.QuestaoQuatro = questionarioCMAS.QuestaoQuatro
                        ;
                        questionario.QuestaoCinco = questionarioCMAS.QuestaoCinco
                        ;
                        questionario.QuestaoSeis = questionarioCMAS.QuestaoSeis
                        ;
                        questionario.QuestaoSeisEscrita = questionarioCMAS.QuestaoSeisEscrita
                        ;
                        questionario.QuestaoSete = questionarioCMAS.QuestaoSete
                        ;
                        questionario.QuestaoSeteEscrita = questionarioCMAS.QuestaoSeteEscrita
                        ;
                        questionario.QuestaoOito = questionarioCMAS.QuestaoOito
                        ;
                        questionario.QuestaoNove = questionarioCMAS.QuestaoNove
                        ;
                        _repositoryQuestoesCMAS.Update(questionario);
            }

        }

        public void SaveQuestionarioPrestacaoDeContasDRADS(QuestoesDRADSInfo questionatioDRADS, int idPrefeitura, Boolean commit)
        {
            var questionario = _repositoryQuestoesDRADS.GetQuery().FirstOrDefault(q => q.IdPrefeitura == idPrefeitura && q.Exercicio == questionatioDRADS.Exercicio);

            if (questionario == null)
            {
                _repositoryQuestoesDRADS.Add(
                    new QuestoesDRADSInfo() 
                    { 
                    
                      IdPrefeitura = idPrefeitura
                      ,
                      QuestaoUm = questionatioDRADS.QuestaoUm
                      ,
                      QuestaoUmEscrita = questionatioDRADS.QuestaoUmEscrita
                      ,
                      QuestaoDois = questionatioDRADS.QuestaoDois
                      ,
                      QuestaoTres = questionatioDRADS.QuestaoTres
                      ,
                      QuestaoQuatro = questionatioDRADS.QuestaoQuatro
                      ,
                      QuestaoCinco = questionatioDRADS.QuestaoCinco
                      ,
                      QuestaoCincoEscrita = questionatioDRADS.QuestaoCincoEscrita
                      ,
                      Exercicio = questionatioDRADS.Exercicio
                    
                    });
            }
            else
            {
                      questionario.QuestaoUm = questionatioDRADS.QuestaoUm
                      ;
                      questionario.QuestaoUmEscrita = questionatioDRADS.QuestaoUmEscrita
                      ;
                      questionario.QuestaoDois = questionatioDRADS.QuestaoDois
                      ;
                      questionario.QuestaoTres = questionatioDRADS.QuestaoTres
                      ;
                      questionario.QuestaoQuatro = questionatioDRADS.QuestaoQuatro
                      ;
                      questionario.QuestaoCinco = questionatioDRADS.QuestaoCinco
                      ;
                      questionario.QuestaoCincoEscrita = questionatioDRADS.QuestaoCincoEscrita
                      ;
                      _repositoryQuestoesDRADS.Update(questionario);
            }
        }

        public void SaveComentarioExecucaoFinanceiraCMAS(ComentarioExecucaoFinanceiraCMASInfo comentarioExecucao,int idPrefeitura,Boolean commit) 
        {
            var comentario = _repositoryComentarioExecucaoFinanceiraCMAS.GetQuery()
                                                        .FirstOrDefault(c => c.IdPrefeitura == idPrefeitura
                                                                          && c.Exercicio == comentarioExecucao.Exercicio);
            if (comentario == null)
            {
                _repositoryComentarioExecucaoFinanceiraCMAS.Add(
                            new ComentarioExecucaoFinanceiraCMASInfo()
                            {
                                IdPrefeitura = idPrefeitura
                                ,
                                Comentario = comentarioExecucao.Comentario
                                ,
                                Exercicio = comentarioExecucao.Exercicio
                                ,
                                Desbloqueado = comentarioExecucao.Desbloqueado
                                ,
                                IdSituacao = comentarioExecucao.IdSituacao
                            });
            }
            else
            {
                comentario.Comentario = comentarioExecucao.Comentario;
                _repositoryComentarioExecucaoFinanceiraCMAS.Update(comentario);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void SaveDeliberacao(DeliberacaoCMASInfo D, Boolean commit) 
        {

           int idPrefeitura = D.IdPrefeitura;
           int exercicio = D.Exercicio;

            var deliberacao = _repositoryDeliberacaoCMAS.GetQuery().FirstOrDefault(c => c.IdPrefeitura == idPrefeitura && c.Exercicio == exercicio);

            if (deliberacao == null)
            {
                _repositoryDeliberacaoCMAS.Add(
                            new DeliberacaoCMASInfo()
                            {
                                IdPrefeitura = idPrefeitura
                                ,
                                Exercicio = D.Exercicio
                                ,
                                DataReuniao = D.DataReuniao
                                ,
                                DataPublicacao = D.DataPublicacao
                                ,
                                NumeroConselheiros = D.NumeroConselheiros
                                ,
                                NumeroAta = D.NumeroAta
                                ,
                                NumeroResolucao = D.NumeroResolucao
                            });
            }
            else
            {
                deliberacao.DataReuniao = D.DataReuniao;
                deliberacao.DataPublicacao = D.DataPublicacao;
                deliberacao.NumeroAta = D.NumeroAta;
                deliberacao.NumeroConselheiros = D.NumeroConselheiros;
                deliberacao.NumeroResolucao = D.NumeroResolucao;
                _repositoryDeliberacaoCMAS.Update(deliberacao);
            }

            if (commit)
                ContextManager.Commit();
        }


        public void SaveDeliberacaoPrestacaoDeContasCMAS(DeliberacaoPrestacaoDeContasCMASInfo D, Boolean commit)
        {

            int idPrefeitura = D.IdPrefeitura;
            int exercicio = D.Exercicio;

            var deliberacao = _repositoryDeliberacaoPrestacaoDeContasCMAS.GetQuery().FirstOrDefault(c => c.IdPrefeitura == idPrefeitura && c.Exercicio == exercicio);

            if (deliberacao == null)
            {
                _repositoryDeliberacaoPrestacaoDeContasCMAS.Add(
                            new DeliberacaoPrestacaoDeContasCMASInfo()
                            {
                                IdPrefeitura = idPrefeitura
                                ,
                                Exercicio = D.Exercicio
                                ,
                                QuestaoDeliberacao = D.QuestaoDeliberacao
                                ,
                                DataReuniao = D.DataReuniao
                                ,
                                DataPublicacao = D.DataPublicacao
                                ,
                                NumeroConselheiros = D.NumeroConselheiros
                                ,
                                NumeroAta = D.NumeroAta
                                ,
                                NumeroResolucao = D.NumeroResolucao
                            });
            }
            else
            {
                deliberacao.DataReuniao = D.DataReuniao;
                deliberacao.DataPublicacao = D.DataPublicacao;
                deliberacao.QuestaoDeliberacao = D.QuestaoDeliberacao;
                deliberacao.NumeroAta = D.NumeroAta;
                deliberacao.NumeroConselheiros = D.NumeroConselheiros;
                deliberacao.NumeroResolucao = D.NumeroResolucao;
                _repositoryDeliberacaoPrestacaoDeContasCMAS.Update(deliberacao);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void SaveDeliberacaoPrestacaoDeContasDRADS(DeliberacaoPrestacaoDeContasDRADSInfo D, Boolean commit)
        {

            int idPrefeitura = D.IdPrefeitura;
            int exercicio = D.Exercicio;

            var deliberacao = _repositoryDeliberacaoPrestacaoDeContasDRADS.GetQuery().FirstOrDefault(c => c.IdPrefeitura == idPrefeitura && c.Exercicio == exercicio);

            if (deliberacao == null)
            {
                _repositoryDeliberacaoPrestacaoDeContasDRADS.Add(
                            new DeliberacaoPrestacaoDeContasDRADSInfo()
                            {
                                IdPrefeitura = idPrefeitura
                                ,
                                Exercicio = D.Exercicio
                                ,
                                QuestaoDeliberacao = D.QuestaoDeliberacao
                                ,
                                DataReuniao = D.DataReuniao
                                ,
                                DataPublicacao = D.DataPublicacao
                                ,
                                NumeroConselheiros = D.NumeroConselheiros
                                ,
                                NumeroAta = D.NumeroAta
                                ,
                                NumeroResolucao = D.NumeroResolucao
                            });
            }
            else
            {
                deliberacao.DataReuniao = D.DataReuniao;
                deliberacao.DataPublicacao = D.DataPublicacao;
                deliberacao.QuestaoDeliberacao = D.QuestaoDeliberacao;
                deliberacao.NumeroAta = D.NumeroAta;
                deliberacao.NumeroConselheiros = D.NumeroConselheiros;
                deliberacao.NumeroResolucao = deliberacao.NumeroResolucao;
                _repositoryDeliberacaoPrestacaoDeContasDRADS.Update(deliberacao);
            }

            if (commit)
                ContextManager.Commit();
        }


        public void ValidarExecucaoFinanceira(ExecucaoFinanceiraInfo f)
        {
            var lstMsg = new List<string>();
            
            int exercicio = f.Exercicio;

            if (exercicio >= 2020)
            {
                if (f.ValoresReprogramadosFMAS > (f.RecursoDisponibilizadoFMAS + f.ResultadoAplicacaoFinanceiraFMAS - f.ValoresExecutadosFMAS))
                    lstMsg.Add("O valor Reprogramado do FMAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : (f.IdTipoProtecao == 3 ? "Alta Complexidade" : (f.IdTipoProtecao == 4 ? "Programas e Projetos" : (f.IdTipoProtecao == 5 ? "Beneficios Eventuais" : (f.IdTipoProtecao == 6 ? "Proteção Social Especial" : (f.IdTipoProtecao == 7 ? "Incentivo a Gestão" : (f.IdTipoProtecao == 8 ? "Reprogramação exercício anterior" : (f.IdTipoProtecao == 9 ? "Reprogramação Basica" : (f.IdTipoProtecao == 10 ? "Reprogramação Media" : (f.IdTipoProtecao == 11 ? "Reprogramação alta" : (f.IdTipoProtecao == 12 ? "Reprogramação Beneficios Eventuais" : (f.IdTipoProtecao == 13 ? "Reprogramação Programas e Projetos" : (f.IdTipoProtecao == 14 ? "Demandas Parlamentares Básica" : (f.IdTipoProtecao == 15 ? "Reprogramação Demandas Parlamentares Básica" : (f.IdTipoProtecao == 16 ? "Demandas Parlamentares Média" : (f.IdTipoProtecao == 17 ? "Reprogramação Demandas Parlamentares Média" : (f.IdTipoProtecao == 18 ? "Demandas Parlamentares Alta" : (f.IdTipoProtecao == 19 ? "Reprogramação Demandas Parlamentares Alta" : (f.IdTipoProtecao == 20 ? "Demandas Parlamentares Benefícios Eventuais" : (f.IdTipoProtecao == 21 ? "Reprogramação Demandas Parlamentares Benefícios Eventuais" : "Indefinido"))))))))))))))))))))) + " deve ser menor ou igual a soma dos Recursos Disponibilizados + Resultados de Aplicações Financeiras - Valores Executados.");
                if (f.ValoresReprogramadosFNAS > (f.RecursoDisponibilizadoFNAS + f.ResultadoAplicacaoFinanceiraFNAS - f.ValoresExecutadosFNAS))
                    lstMsg.Add("O valor Reprogramado do FNAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : (f.IdTipoProtecao == 3 ? "Alta Complexidade" : (f.IdTipoProtecao == 4 ? "Programas e Projetos" : (f.IdTipoProtecao == 5 ? "Beneficios Eventuais" : (f.IdTipoProtecao == 6 ? "Proteção Social Especial" : (f.IdTipoProtecao == 7 ? "Incentivo a Gestão" : (f.IdTipoProtecao == 8 ? "Reprogramação exercício anterior" : (f.IdTipoProtecao == 9 ? "Reprogramação Basica" : (f.IdTipoProtecao == 10 ? "Reprogramação Media" : (f.IdTipoProtecao == 11 ? "Reprogramação alta" : (f.IdTipoProtecao == 12 ? "Reprogramação Beneficios Eventuais" : (f.IdTipoProtecao == 13 ? "Reprogramação Programas e Projetos" : (f.IdTipoProtecao == 14 ? "Demandas Parlamentares Básica" : (f.IdTipoProtecao == 15 ? "Reprogramação Demandas Parlamentares Básica" : (f.IdTipoProtecao == 16 ? "Demandas Parlamentares Média" : (f.IdTipoProtecao == 17 ? "Reprogramação Demandas Parlamentares Média" : (f.IdTipoProtecao == 18 ? "Demandas Parlamentares Alta" : (f.IdTipoProtecao == 19 ? "Reprogramação Demandas Parlamentares Alta" : (f.IdTipoProtecao == 20 ? "Demandas Parlamentares Benefícios Eventuais" : (f.IdTipoProtecao == 21 ? "Reprogramação Demandas Parlamentares Benefícios Eventuais" : "Indefinido"))))))))))))))))))))) + " deve ser menor ou igual a soma dos Recursos Disponibilizados + Resultados de Aplicações Financeiras - Valores Executados.");
                if (f.ValoresExecutadosFMAS > (f.RecursoDisponibilizadoFMAS + f.ResultadoAplicacaoFinanceiraFMAS))
                    lstMsg.Add("O valor Executado do FMAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : (f.IdTipoProtecao == 3 ? "Alta Complexidade" : (f.IdTipoProtecao == 4 ? "Programas e Projetos" : (f.IdTipoProtecao == 5 ? "Beneficios Eventuais" : (f.IdTipoProtecao == 6 ? "Proteção Social Especial" : (f.IdTipoProtecao == 7 ? "Incentivo a Gestão" : (f.IdTipoProtecao == 8 ? "Reprogramação exercício anterior" : (f.IdTipoProtecao == 9 ? "Reprogramação Basica" : (f.IdTipoProtecao == 10 ? "Reprogramação Media" : (f.IdTipoProtecao == 11 ? "Reprogramação alta" : (f.IdTipoProtecao == 12 ? "Reprogramação Beneficios Eventuais" : (f.IdTipoProtecao == 13 ? "Reprogramação Programas e Projetos" : (f.IdTipoProtecao == 14 ? "Demandas Parlamentares Básica" : (f.IdTipoProtecao == 15 ? "Reprogramação Demandas Parlamentares Básica" : (f.IdTipoProtecao == 16 ? "Demandas Parlamentares Média" : (f.IdTipoProtecao == 17 ? "Reprogramação Demandas Parlamentares Média" : (f.IdTipoProtecao == 18 ? "Demandas Parlamentares Alta" : (f.IdTipoProtecao == 19 ? "Reprogramação Demandas Parlamentares Alta" : (f.IdTipoProtecao == 20 ? "Demandas Parlamentares Benefícios Eventuais" : (f.IdTipoProtecao == 21 ? "Reprogramação Demandas Parlamentares Benefícios Eventuais" : "Indefinido"))))))))))))))))))))) + " deve ser menor ou igual a soma dos Recursos Disponibilizados + Resultados de Aplicações Financeiras.");
                if (f.ValoresExecutadosFNAS > (f.RecursoDisponibilizadoFNAS + f.ResultadoAplicacaoFinanceiraFNAS))
                    lstMsg.Add("O valor Executado do FNAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : (f.IdTipoProtecao == 3 ? "Alta Complexidade" : (f.IdTipoProtecao == 4 ? "Programas e Projetos" : (f.IdTipoProtecao == 5 ? "Beneficios Eventuais" : (f.IdTipoProtecao == 6 ? "Proteção Social Especial" : (f.IdTipoProtecao == 7 ? "Incentivo a Gestão" : (f.IdTipoProtecao == 8 ? "Reprogramação exercício anterior" : (f.IdTipoProtecao == 9 ? "Reprogramação Basica" : (f.IdTipoProtecao == 10 ? "Reprogramação Media" : (f.IdTipoProtecao == 11 ? "Reprogramação alta" : (f.IdTipoProtecao == 12 ? "Reprogramação Beneficios Eventuais" : (f.IdTipoProtecao == 13 ? "Reprogramação Programas e Projetos" : (f.IdTipoProtecao == 14 ? "Demandas Parlamentares Básica" : (f.IdTipoProtecao == 15 ? "Reprogramação Demandas Parlamentares Básica" : (f.IdTipoProtecao == 16 ? "Demandas Parlamentares Média" : (f.IdTipoProtecao == 17 ? "Reprogramação Demandas Parlamentares Média" : (f.IdTipoProtecao == 18 ? "Demandas Parlamentares Alta" : (f.IdTipoProtecao == 19 ? "Reprogramação Demandas Parlamentares Alta" : (f.IdTipoProtecao == 20 ? "Demandas Parlamentares Benefícios Eventuais" : (f.IdTipoProtecao == 21 ? "Reprogramação Demandas Parlamentares Benefícios Eventuais" : "Indefinido"))))))))))))))))))))) + " deve ser menor ou igual a soma dos Recursos Disponibilizados + Resultados de Aplicações Financeiras.");
                if (f.ValoresDevolvidosFEAS < 0)
                    lstMsg.Add("O valor devolvido do FEAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : (f.IdTipoProtecao == 3 ? "Alta Complexidade" : (f.IdTipoProtecao == 4 ? "Programas e Projetos" : (f.IdTipoProtecao == 5 ? "Beneficios Eventuais" : (f.IdTipoProtecao == 6 ? "Proteção Social Especial":(f.IdTipoProtecao == 7 ? "Incentivo a Gestão":(f.IdTipoProtecao == 8 ? "Reprogramação exercício anterior": (f.IdTipoProtecao == 9 ? "Reprogramação Basica":(f.IdTipoProtecao == 10 ? "Reprogramação Media":(f.IdTipoProtecao == 11 ? "Reprogramação alta":(f.IdTipoProtecao == 12 ? "Reprogramação Beneficios Eventuais":(f.IdTipoProtecao == 13 ? "Reprogramação Programas e Projetos":(f.IdTipoProtecao == 14 ? "Demandas Parlamentares Básica":(f.IdTipoProtecao == 15 ? "Reprogramação Demandas Parlamentares Básica":(f.IdTipoProtecao == 16 ? "Demandas Parlamentares Média":(f.IdTipoProtecao == 17 ? "Reprogramação Demandas Parlamentares Média":(f.IdTipoProtecao == 18 ? "Demandas Parlamentares Alta":(f.IdTipoProtecao == 19 ? "Reprogramação Demandas Parlamentares Alta":(f.IdTipoProtecao == 20 ? "Demandas Parlamentares Benefícios Eventuais":(f.IdTipoProtecao == 21 ? "Reprogramação Demandas Parlamentares Benefícios Eventuais":"Indefinido"))))))))))))))))))))) + " deve ser maior ou igual a Zero.");
            }
            else
            {
                if (f.ValoresReprogramadosFMAS > (f.RecursoDisponibilizadoFMAS + f.ResultadoAplicacaoFinanceiraFMAS - f.ValoresExecutadosFMAS))
                    lstMsg.Add("O valor Reprogramado do FMAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : "Alta Complexidade")) + " deve ser menor ou igual a soma dos Recursos Disponibilizados + Resultados de Aplicações Financeiras - Valores Executados.");
                if (f.ValoresReprogramadosFEAS > (f.RecursoDisponibilizadoFEAS + f.ResultadoAplicacaoFinanceiraFEAS - f.ValoresExecutadosFEAS))
                    lstMsg.Add("O valor Reprogramado do FEAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : "Alta Complexidade")) + " deve ser menor ou igual a soma dos Recursos Disponibilizados + Resultados de Aplicações Financeiras - Valores Executados.");
                if (f.ValoresReprogramadosFNAS > (f.RecursoDisponibilizadoFNAS + f.ResultadoAplicacaoFinanceiraFNAS - f.ValoresExecutadosFNAS))
                    lstMsg.Add("O valor Reprogramado do FNAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : "Alta Complexidade")) + " deve ser menor ou igual a soma dos Recursos Disponibilizados + Resultados de Aplicações Financeiras - Valores Executados.");
                if (f.ValoresExecutadosFMAS > (f.RecursoDisponibilizadoFMAS + f.ResultadoAplicacaoFinanceiraFMAS))
                    lstMsg.Add("O valor Executado do FMAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : "Alta Complexidade")) + " deve ser menor ou igual a soma dos Recursos Disponibilizados + Resultados de Aplicações Financeiras.");
                if (f.ValoresExecutadosFEAS > (f.RecursoDisponibilizadoFEAS + f.ResultadoAplicacaoFinanceiraFEAS))
                    lstMsg.Add("O valor Executado do FEAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : "Alta Complexidade")) + " deve ser menor ou igual a soma dos Recursos Disponibilizados + Resultados de Aplicações Financeiras.");
                if (f.ValoresExecutadosFNAS > (f.RecursoDisponibilizadoFNAS + f.ResultadoAplicacaoFinanceiraFNAS))
                    lstMsg.Add("O valor Executado do FNAS para Proteção Social " + (f.IdTipoProtecao == 1 ? "Básica" : (f.IdTipoProtecao == 2 ? "Média Complexidade" : "Alta Complexidade")) + " deve ser menor ou igual a soma dos Recursos Disponibilizados + Resultados de Aplicações Financeiras.");
            }

            if (lstMsg.Count > 0)
                throw new Exception(Util.Concat(lstMsg, System.Environment.NewLine));
        }

        public ProgramasDesenvolvidosMunicipio2016Info GetProgramasDesenvolvidosMunicipio2016(Int32 idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetProgramasDesenvolvidosMunicipio2016(idPrefeitura);
        }

        public ProgramasDesenvolvidosMunicipio2016Info GetProgramasDesenvolvidosMunicipio2021(Int32 idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetProgramasDesenvolvidosMunicipio2021(idPrefeitura);
        }

        #endregion

        public List<PrevisaoOrcamentaria2016Info> GetPrevisaoOrcamentaria2016(Int32 idPrefeitura)

        {
            return (ContextManager.GetContext() as PMASContext).GetPrevisaoOrcamentaria2016(idPrefeitura);
        }

        public List<PrevisaoOrcamentariaInfo> GetPrevisaoOrcamentaria(Int32 idPrefeitura, int exercicio)
        {
            return (ContextManager.GetContext() as PMASContext).GetPrevisaoOrcamentaria(idPrefeitura, exercicio);
        }

        public List<PrevisaoOrcamentariaMunicipalInfo> GetPrevisaoOrcamentariaMunicipal(Int32 idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetPrevisaoOrcamentariaMunicipal(idPrefeitura);
        } 

        public BeneficioEventual2016Info GetBeneficioEventual2016(Int32 idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetBeneficioEventual2016ByPrefeitura(idPrefeitura);
        }

        //public BeneficioEventualAnualInfo GetBeneficioEventual(Int32 idPrefeitura)
        public List<BeneficioEventualAnualInfo> GetBeneficioEventual(Int32 idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetBeneficioEventualByPrefeitura(idPrefeitura);
        }

        public TransferenciaRenda2016Info GeTransferenciaRenda2016(Int32 idPrefeitura, Int32 idTipoTransferencia)
        {
            return (ContextManager.GetContext() as PMASContext).GetTransferenciaRenda2016ByPrefeitura(idPrefeitura, idTipoTransferencia);
        }

        public SaoPauloSolidario2016Info GetSaoPauloSolidario2016(Int32 idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetSaoPauloSolidario2016ByPrefeitura(idPrefeitura);
        }

        public List<TransferenciaRenda2016Info> GetTransferenciaRenda2016(Int32 idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetTransferenciaRenda2016ByPrefeitura(idPrefeitura);
        }

        public TransferenciaRendaAnualInfo GeTransferenciaRenda(Int32 idPrefeitura, Int32 idTipoTransferencia)
        {
            return (ContextManager.GetContext() as PMASContext).GetTransferenciaRendaByPrefeitura(idPrefeitura, idTipoTransferencia);
        }

        public PrefeituraValoresReprogramadosAnoAnteriorInfo GetValoresReprogramadosAnoAnterior(Int32 idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetValoresReprogramadosAnoAnterior(idPrefeitura);
        }

        public List<TransferenciaRendaAnualInfo> GetTransferenciaRenda(Int32 idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetTransferenciaRendaByPrefeitura(idPrefeitura);
        }

        public Int32 GetMetaPrevista(int idPrograma, string CNPJ)
        {
            return (ContextManager.GetContext() as PMASContext).GetMetaPrevista(idPrograma, CNPJ);

        }

        public List<ConsultaCofinanciamentoEstadualInfo> GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(Int32 idPrefeitura, Int32 IdTipoProtecaoSocial, Int32 exercicio)
        {
            return (ContextManager.GetContext() as PMASContext).GetCofinanciamentoEstadualByPrefeituraETipoProtecaoSocial(idPrefeitura, IdTipoProtecaoSocial, exercicio);
        }

        public Decimal GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePublica(Int32 idPrefeitura, Int32 IdTipoProtecaoSocial, Int32 exercicio)
        {
            //TIPOS DE UNIDADES: 1 - UNIDADE PUBLICA, 3 - CRAS , 4 - CREAS|| t.IdTipoUnidade == 4, 5 - CENTRO POP
            var resultado = (ContextManager.GetContext() as PMASContext).GetCofinanciamentoEstadualByPrefeituraETipoProtecaoSocial(idPrefeitura, IdTipoProtecaoSocial, exercicio)
                .Where(t => t.IdTipoUnidade == 1 || t.IdTipoUnidade == 3 || t.IdTipoUnidade == 4 || t.IdTipoUnidade == 5);
            return resultado.Sum(t => t.PrevisaoOrcamentaria + t.RecursoReprogramadoAnoAnterior + t.ValorEstadualDemandasParlamentares + t.ValorEstadualReprogramacaoDemandasParlamentares);
        }

        public Decimal GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePrivada(Int32 idPrefeitura, Int32 IdTipoProtecaoSocial, Int32 exercicio)
        {
            //TIPOS DE UNIDADES: 2 - UNIDADE PRIVADA
            return (ContextManager.GetContext() as PMASContext).GetCofinanciamentoEstadualByPrefeituraETipoProtecaoSocial(idPrefeitura, IdTipoProtecaoSocial, exercicio)
                .Where(t => t.IdTipoUnidade == 2)
                .Sum(t => t.PrevisaoOrcamentaria + t.RecursoReprogramadoAnoAnterior + t.ValorEstadualDemandasParlamentares + t.ValorEstadualReprogramacaoDemandasParlamentares);
        }
    }
}
