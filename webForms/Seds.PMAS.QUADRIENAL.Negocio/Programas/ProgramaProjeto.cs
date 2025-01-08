using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ProgramaProjeto
    {
        private static IRepository<ProgramaProjetoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ProgramaProjetoInfo>>();
            }
        }
        private static IRepository<ConsultaProgramaProjetoInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaProgramaProjetoInfo>>();
            }
        }

        private static IRepository<ConsultaProgramaProjetoExercicioInfo> _repositoryConsultaExercicio
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaProgramaProjetoExercicioInfo>>();
            }
        }

        private static IRepository<MetaFamiliaPaulistaInfo> _repositoryConsultaMeta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<MetaFamiliaPaulistaInfo>>();
            }
        }

        private static IRepository<PlanoAcaoInfo> _repositoryPlanoAcao
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PlanoAcaoInfo>>();
            }
        }


        public MetaFamiliaPaulistaInfo GetMetaFamiliaPaulista(int idPrefeitura)
        {
            return _repositoryConsultaMeta.Single(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<ProgramaProjetoInfo> GetAll()
        {
            return _repository.GetQuery().Where(p => p.Ativo == true);

        }

        public ProgramaProjetoInfo GetById(int id)
        {
            var p = _repository.GetObjectSet().Include("ProgramasProjetosRecursoFinanceiro").Include("ProgramasProjetosParcelasInfo").Single(m => m.Id == id);
            if (p == null)
                return null;
            


            if (p.PossuiInterlocutorMunicipal.HasValue)
            {
                if (p.PossuiInterlocutorMunicipal.Value == true)
                {
                    p.InterlocutorMunicipal = new InterlocutorMunicipal().GetByIdProgramaProjeto(id);
                }
            }

            //p.CaracterizacaoUsuarios = new ProgramaProjeto().GetCaracterizacaoUsuariosByPrograma(p.Id);
            p.AcoesDesenvolvidasPrograma = new ProgramaProjeto().GetAcoesDesenvolvidasByPrograma(p.Id);
            //p.UnidadeOfertante = new ProgramaProjeto().GetUnidadesOfertantesProgramaProjeto(p.Id);
            //p.UnidadesPrivadas = new ProgramaProjeto().GetUnidadesPrivadasByPrograma(p.Id);
            //p.AcoesSocioAssistenciais = new ProgramaProjeto().GetAcoesSocioAssistenciaisByPrograma(p.Id);

             p.PrevisaoAnual =new ProgramaProjetoPrevisaoAnualBeneficiarios().GetByProgramaProjeto(id);

       
            if (p.PossuiParceriaFormal)
                p.Parcerias = new ProgramaProjetoParceria().GetByProgramaProjeto(p.Id).ToList();
            if (p.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.HasValue && p.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.Value)  
                p.ParceriasSaoPauloSolidarioAgendaFamilia = new SPSolidarioAgendaFamiliaParceria().GetByProgramaProjeto(p.Id).ToList();

            if (p.PossuiParceriaFormalSaoPauloSolidarioAlemDaRenda.HasValue && p.PossuiParceriaFormalSaoPauloSolidarioAlemDaRenda.Value)
                p.ParceriasSaoPauloSolidarioAlemdaRenda = new SPSolidarioAlemdaRendaParceria().GetByProgramaProjeto(p.Id).ToList();

            if (p.SaoPauloSolidarioPossuiPlanejamentoBens.HasValue && p.SaoPauloSolidarioPossuiPlanejamentoBens.Value)
                p.SaoPauloSolidarioPlanejamentoBens = new SPSolidarioPlanejamentoBens().GetByProgramaProjeto(p.Id).ToList();
            if (p.SaoPauloSolidarioPossuiPlanejamentoServicos.HasValue && p.SaoPauloSolidarioPossuiPlanejamentoServicos.Value)
                p.SaoPauloSolidarioPlanejamentoServicos = new SPSolidarioPlanejamentoServicos().GetByProgramaProjeto(p.Id).ToList();

            if (p.ProgramaEstadual.HasValue)
            {
                if (p.ProgramaEstadual.Value && p.Nome.ToLower().Contains("família paulista"))
                {
                    p.GrupoGestores = new GrupoGestor().GetByProgramaProjeto(p.Id).ToList();
                    p.IdentificacoesTerritorio = new IdentificacaoTerritorio().GetByProgramaProjeto(p.Id).ToList();
                    p.PlanoAcao = new PlanoAcao().GetByIdProgramaProjeto(p.Id);
                }
            }
            return p;
        }


        public ProgramaProjetoInfo GetByIdParcelas(int id)
        {
            var p = _repository.GetObjectSet().Include("ProgramasProjetosRecursoFinanceiro").Include("ProgramasProjetosParcelasInfo").Single(m => m.Id == id);
            if (p == null)
                return null;



            if (p.PossuiInterlocutorMunicipal.HasValue)
            {
                if (p.PossuiInterlocutorMunicipal.Value == true)
                {
                    p.InterlocutorMunicipal = new InterlocutorMunicipal().GetByIdProgramaProjeto(id);
                }
            }


            p.PrevisaoAnual = new ProgramaProjetoPrevisaoAnualBeneficiarios().GetByProgramaProjeto(id);


            if (p.PossuiParceriaFormal)
                p.Parcerias = new ProgramaProjetoParceria().GetByProgramaProjeto(p.Id).ToList();
            if (p.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.HasValue && p.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.Value)
                p.ParceriasSaoPauloSolidarioAgendaFamilia = new SPSolidarioAgendaFamiliaParceria().GetByProgramaProjeto(p.Id).ToList();

            if (p.PossuiParceriaFormalSaoPauloSolidarioAlemDaRenda.HasValue && p.PossuiParceriaFormalSaoPauloSolidarioAlemDaRenda.Value)
                p.ParceriasSaoPauloSolidarioAlemdaRenda = new SPSolidarioAlemdaRendaParceria().GetByProgramaProjeto(p.Id).ToList();

            if (p.SaoPauloSolidarioPossuiPlanejamentoBens.HasValue && p.SaoPauloSolidarioPossuiPlanejamentoBens.Value)
                p.SaoPauloSolidarioPlanejamentoBens = new SPSolidarioPlanejamentoBens().GetByProgramaProjeto(p.Id).ToList();
            if (p.SaoPauloSolidarioPossuiPlanejamentoServicos.HasValue && p.SaoPauloSolidarioPossuiPlanejamentoServicos.Value)
                p.SaoPauloSolidarioPlanejamentoServicos = new SPSolidarioPlanejamentoServicos().GetByProgramaProjeto(p.Id).ToList();

            if (p.ProgramaEstadual.HasValue)
            {
                if (p.ProgramaEstadual.Value && p.Nome.ToLower().Contains("família paulista"))
                {
                    p.GrupoGestores = new GrupoGestor().GetByProgramaProjeto(p.Id).ToList();
                    p.IdentificacoesTerritorio = new IdentificacaoTerritorio().GetByProgramaProjeto(p.Id).ToList();
                    p.PlanoAcao = new PlanoAcao().GetByIdProgramaProjeto(p.Id);
                }
            }
            return p;
        }


        public List<CaracterizacaoUsuariosInfo> GetCaracterizacaoUsuariosByPrograma(int idProgramaProjeto)
        {
            return _repository.GetQuery().Where(t => t.Id == idProgramaProjeto).SelectMany(t => t.CaracterizacaoUsuarios).ToList();
        }

        public List<AcoesDesenvolvidaProgramasInfo> GetAcoesDesenvolvidasByPrograma(int idProgramaProjeto)
        {
            return _repository.GetQuery().Where(t => t.Id == idProgramaProjeto).SelectMany(t => t.AcoesDesenvolvidasPrograma).ToList();
        }

        public List<UnidadeOfertanteInfo> GetUnidadesOfertantesProgramaProjeto(int idProgramaProjeto)
        {
            return _repository.GetQuery().Where(t => t.Id == idProgramaProjeto).SelectMany(t => t.UnidadeOfertante).ToList();
        }

        public List<AcaoSocioAssistencialInfo> GetAcoesSocioAssistenciaisByPrograma(int idProgramaProjeto)
        {
            return _repository.GetQuery().Where(t => t.Id == idProgramaProjeto).SelectMany(t => t.AcoesSocioAssistenciais).ToList();
        }

        public List<UnidadePrivadaInfo> GetUnidadesPrivadasByPrograma(int idProgramaProjeto)
        {
            return _repository.GetQuery().Where(t => t.Id == idProgramaProjeto).SelectMany(t => t.UnidadesPrivadas).ToList();
        }

        public IQueryable<ProgramaProjetoInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("ProgramasProjetosRecursoFinanceiro")
                                             .Where(m => m.IdPrefeitura == idPrefeitura && m.Ativo == true); //&& m.ProgramaMunicipal.HasValue && m.ProgramaMunicipal.Value == true);
        }

        public IQueryable<ConsultaProgramaProjetoInfo> GetProgramasByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.Ativo == true).OrderBy(m => m.TipoAbrangencia).ThenBy(m => m.Nome);
        }

        public IQueryable<ConsultaProgramaProjetoInfo> GetConsultaByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.ProgramaMunicipal.HasValue && m.ProgramaMunicipal.Value == true).OrderBy(m => m.Id);
        }

        public IQueryable<ProgramaProjetoInfo> GetEstadualByPrefeitura(int idPrefeitura)  
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.ProgramaEstadual.HasValue && m.ProgramaEstadual.Value == true);
        }

        public IQueryable<ConsultaProgramaProjetoInfo> GetConsultaEstadualByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.ProgramaEstadual.Value == true && m.Ativo == true);
        }

        public IEnumerable<ConsultaProgramaProjetoExercicioInfo> GetConsultaEstadualExercicioByPrefeitura(int idPrefeitura, int exercicio)
        {
            //return _repositoryConsultaExercicio.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.ProgramaEstadual.Value == true && m.Ativo == true);
            return (ContextManager.GetContext() as PMASContext).GetConsultaEstadualExercicioByPrefeitura(idPrefeitura, exercicio)
                .Where(x => x.IdPrefeitura == idPrefeitura 
                && x.ProgramaEstadual.Value == true 
                && x.Ativo == true);
        }

        public IQueryable<ProgramaProjetoInfo> GetFederalByPrefeitura(int idPrefeitura) 
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.ProgramaFederal.HasValue && m.ProgramaFederal.Value == true && m.Ativo == true);
        }

        public IQueryable<ConsultaProgramaProjetoInfo> GetConsultaFederalByPrefeitura(int idPrefeitura) 
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.ProgramaFederal.HasValue && m.ProgramaFederal.Value == true);
        }

        public List<ConsultaProgramaProjetoExercicioInfo> GetConsultaFederalExercicioByPrefeitura(int idPrefeitura, int exercicio)
        {
            return (ContextManager.GetContext() as PMASContext).GetConsultaFederalExercicioByPrefeitura(idPrefeitura, exercicio)
                .Where(x => x.IdPrefeitura == idPrefeitura 
                    && x.ProgramaFederal.HasValue 
                    && x.ProgramaFederal.Value == true).ToList();
                
        }

        public void Update(ProgramaProjetoInfo projeto, Boolean commit)
        {

            #region validacao
            new ValidadorProgramaProjeto().Validar(projeto); 
            #endregion

            #region set ativo flag
            if (projeto.ProgramaMunicipal.HasValue && projeto.ProgramaMunicipal.Value)
            {
                projeto.Ativo = true;
            } 
            #endregion

            #region update
            _repository.Update(projeto); 
            #endregion

            //QUADRO 41
            var propriedadesEntity = _repository.GetModifiedProperties(projeto);
            var propriedades = new List<string>();

            propriedades = GetLabelForInfo(propriedadesEntity);

            #region plano acao
            if (projeto.PlanoAcao != null)
            {
                var planoAcao = new PlanoAcao().GetByIdProgramaProjeto(projeto.Id);

                if (planoAcao == null)
                {
                    planoAcao = new PlanoAcaoInfo();
                    planoAcao.IdProgramaProjeto = projeto.Id;
                    planoAcao.PossuiPlanoAcao = projeto.PlanoAcao.PossuiPlanoAcao;
                    if (planoAcao.PossuiPlanoAcao)
                    {
                        planoAcao.DataAprovacao = projeto.PlanoAcao.DataAprovacao;
                    }
                    new PlanoAcao().Add(planoAcao, true);
                }
                else
                {
                    planoAcao.PossuiPlanoAcao = projeto.PlanoAcao.PossuiPlanoAcao;
                    if (planoAcao.PossuiPlanoAcao)
                    {
                        if (projeto.PlanoAcao.DataAprovacao.Value != planoAcao.DataAprovacao)
                        {
                            planoAcao.DataAprovacao = projeto.PlanoAcao.DataAprovacao;
                            propriedades.Add("Data de aprovação do Plano de Ação pelo CMAS");
                        }
                    }
                    new PlanoAcao().Update(planoAcao, true);
                }
            } 
            #endregion

            #region Preencher previsao anual
            if (projeto.ProgramaFederal != null && projeto.ProgramaFederal.Value && projeto.Nome.ToLower().Contains("acessuas"))
            {
                PreencherPrevisaoAnual(projeto);
            }
            if (projeto.ProgramaFederal != null && projeto.ProgramaFederal.Value && projeto.Nome.ToUpper().Contains("PROGRAMA CRIANÇA FELIZ"))
            {
                PreencherPrevisaoAnual(projeto);
            }

            if (projeto.ProgramaEstadual != null && projeto.ProgramaEstadual.Value && projeto.Nome.ToLower().Contains("idoso"))
            {
                PreencherPrevisaoAnual(projeto);
            }

            if (projeto.ProgramaMunicipal != null && projeto.ProgramaMunicipal.Value && projeto.TransferenciaRendaDireta.HasValue && projeto.TransferenciaRendaDireta.Value)
            {
                PreencherPrevisaoAnual(projeto);
            }

            #endregion

            #region programa idoso parcelas
            if (projeto.ProgramasProjetosParcelasInfo != null)
            {
                PreencherProgramaProjetoParcelas(projeto);
            } 
            #endregion

            #region parcerias
            var lstDeleted = new List<ProgramaProjetoParceriaInfo>();
            var ppp = new ProgramaProjetoParceria();
            var lst = ppp.GetByProgramaProjeto(projeto.Id);
            projeto.Parcerias = projeto.Parcerias ?? new List<ProgramaProjetoParceriaInfo>();
            var hasChangeParcerias = false;

            foreach (var p in lst)
            {
                if (!projeto.Parcerias.Any(t => t.Id == p.Id))
                {
                    hasChangeParcerias = true;
                    lstDeleted.Add(p);
                }
            }

            foreach (var p in lstDeleted)
            {
                ppp.Delete(p, false);
            }

            foreach (var p in projeto.Parcerias)
            {
                p.TipoParceria = null;
                p.Parceria = null;
                p.IdProgramaProjeto = projeto.Id;
                if (p.Id == 0)
                {
                    ppp.Add(p, false);
                    hasChangeParcerias = true;
                }
                else
                {
                    ppp.Update(p, false);
                }
            }

            if (hasChangeParcerias && !propriedades.Any(t => t == "parcerias"))
            {
                propriedades.Add("parcerias");
            }

            #endregion

            #region Interlocutor Municipal - TB_INTERLOCUTOR_MUNICIPAL
            if (projeto.PossuiInterlocutorMunicipal != null && projeto.PossuiInterlocutorMunicipal.Value == true)
            {
                var interlocutor = new InterlocutorMunicipal().GetByIdProgramaProjeto(projeto.Id);
                if (interlocutor == null)
                {
                    interlocutor = new InterlocutorMunicipalInfo();
                    interlocutor.IdProgramaProjeto = projeto.Id;
                    interlocutor.Nome = projeto.InterlocutorMunicipal.Nome;
                    interlocutor.Telefone = projeto.InterlocutorMunicipal.Telefone;
                    interlocutor.Email = projeto.InterlocutorMunicipal.Email;
                    interlocutor.Celular = projeto.InterlocutorMunicipal.Celular;
                    new InterlocutorMunicipal().Add(interlocutor, true);
                }
                else
                {
                    if (interlocutor.Nome != projeto.InterlocutorMunicipal.Nome)
                    {
                        interlocutor.Nome = projeto.InterlocutorMunicipal.Nome;
                        if (projeto.Nome.ToLower().Equals("família paulista") && projeto.ProgramaEstadual.Value == true)
                        {
                            propriedades.Add("Nome do(a) coordenador(a) do programa no município");
                        }
                        else
                        {
                            propriedades.Add("Nome do técnico responsável pelo programa");
                        }
                    }
                    if (interlocutor.Celular != projeto.InterlocutorMunicipal.Celular)
                    {
                        interlocutor.Celular = projeto.InterlocutorMunicipal.Celular;
                        propriedades.Add("Celular");
                    }
                    if (interlocutor.Telefone != projeto.InterlocutorMunicipal.Telefone)
                    {
                        interlocutor.Telefone = projeto.InterlocutorMunicipal.Telefone;
                        propriedades.Add("Telefone");
                    }
                    if (interlocutor.Email != projeto.InterlocutorMunicipal.Email)
                    {
                        interlocutor.Email = projeto.InterlocutorMunicipal.Email;
                        propriedades.Add("E-mail institucional");
                    }
                    new InterlocutorMunicipal().Update(interlocutor, true);

                }
            } 
            #endregion

            var original = GetByIdECaracteristicasUsuarios(projeto.Id);

            var hasChangeCaracterizacaoUsuarios = _repository.UpdateNN<CaracterizacaoUsuariosInfo>(original, projeto.CaracterizacaoUsuarios, (a, lista) => lista.Any(t => t.Id == a.Id), p => p.CaracterizacaoUsuarios);
            var hasChangeAcoesDesenvolvidasProgramas = _repository.UpdateNN<AcoesDesenvolvidaProgramasInfo>(original, projeto.AcoesDesenvolvidasPrograma, (a, lista) => lista.Any(t => t.Id == a.Id), p => p.AcoesDesenvolvidasPrograma);
            var hasChangeUnidadesExecutoras = _repository.UpdateNN<UnidadePrivadaInfo>(original, projeto.UnidadesPrivadas, (a, lista) => lista.Any(t => t.Id == a.Id), p => p.UnidadesPrivadas);
            var hasChangeAcoesSocioAssistenciais = _repository.UpdateNN<AcaoSocioAssistencialInfo>(original, projeto.AcoesSocioAssistenciais, (a, lista) => lista.Any(t => t.Id == a.Id), p => p.AcoesSocioAssistenciais);

            if (original.ProgramasProjetosRecursoFinanceiro == null)
            {
                original.ProgramasProjetosRecursoFinanceiro = projeto.ProgramasProjetosRecursoFinanceiro;
            }
            else { 
                var recursoNovo = projeto.ProgramasProjetosRecursoFinanceiro.FirstOrDefault();
                var recurso = original.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == recursoNovo.Exercicio).FirstOrDefault();
                if (recurso == null)
                {
                    original.ProgramasProjetosRecursoFinanceiro.Add(recursoNovo);
                }
                else 
                {
                    recurso.FonteFMAS = recursoNovo.FonteFMAS;
                    recurso.ValorFMAS = recursoNovo.ValorFMAS;

                    recurso.FonteOrcamentoMunicipal = recursoNovo.FonteOrcamentoMunicipal;
                    recurso.ValorOrcamentoMunicipal = recursoNovo.ValorOrcamentoMunicipal;

                    recurso.FonteFundoMunicipal = recursoNovo.FonteFundoMunicipal;
                    recurso.ValorFundoMunicipal = recursoNovo.ValorFundoMunicipal;

                    recurso.FonteFEAS = recursoNovo.FonteFEAS;
                    recurso.ValorFEAS = recursoNovo.ValorFEAS;

                    #region Valores projetos Estaduais
                    #region estadual
                    recurso.FonteOrcamentoEstadual = recursoNovo.FonteOrcamentoEstadual;
                    recurso.ValorOrcamentoEstadual = recursoNovo.ValorOrcamentoEstadual;

                    recurso.FonteFundoEstadual = recursoNovo.FonteFundoEstadual;
                    recurso.ValorFundoEstadual = recursoNovo.ValorFundoEstadual;
                    #endregion

                    #region federal
                    recurso.FonteFNAS = recursoNovo.FonteFNAS;
                    recurso.ValorFNAS = recursoNovo.ValorFNAS;

                    recurso.FonteOrcamentoFederal = recursoNovo.FonteOrcamentoFederal;
                    recurso.ValorOrcamentoFederal = recursoNovo.ValorOrcamentoFederal;

                    recurso.FonteFundoFederal = recursoNovo.FonteFundoFederal;
                    recurso.ValorFundoFederal = recursoNovo.ValorFundoFederal;
                    #endregion
                    #region indices
                    recurso.FonteIGDPBF = recursoNovo.FonteIGDPBF;
                    recurso.ValorIGDPBF = recursoNovo.ValorIGDPBF;

                    recurso.FonteIGDSUAS = recursoNovo.FonteIGDSUAS;
                    recurso.ValorIGDSUAS = recursoNovo.ValorIGDSUAS;
                    #endregion
                    #endregion
                }
            }

            #region Unidade Ofertante
            var lstUnidadesDeleted = new List<UnidadeOfertanteInfo>();
            var unidadeOfertante = new UnidadeOfertante();
            var listUnidades = unidadeOfertante.GetByIdProgramaProjeto(projeto.Id);
            projeto.UnidadeOfertante = projeto.UnidadeOfertante ?? new List<UnidadeOfertanteInfo>();
            var hasChangeUnidades = false;

            foreach (var p in listUnidades)
            {
                if (!projeto.UnidadeOfertante.Any(t => t.Id == p.Id))
                {
                    hasChangeUnidades = true;
                    lstUnidadesDeleted.Add(p);
                }
            }

            foreach (var p in lstUnidadesDeleted)
            {
                unidadeOfertante.Delete(p, true);
            }

            foreach (var p in projeto.UnidadeOfertante)
            {
                p.IdProgramaProjeto = projeto.Id;
                if (p.Id == 0)
                {
                    unidadeOfertante.Add(p, true);
                    hasChangeUnidades = true;
                }
                else
                    unidadeOfertante.Update(p, true);
            } 
            #endregion


            _repository.Update(original); 


            #region Grupo Gestores
            var lstDeletedGrupoGestores = new List<ProgramaProjetoGrupoGestorInfo>();
            var ppGrupoGestor = new GrupoGestor();
            var lstGrupoGestores = ppGrupoGestor.GetByProgramaProjeto(projeto.Id);
            projeto.GrupoGestores = projeto.GrupoGestores ?? new List<ProgramaProjetoGrupoGestorInfo>();
            var hasChangeGrupoGestores = false;
            foreach (var p in lstGrupoGestores)
            {
                if (!projeto.GrupoGestores.Any(t => t.Id == p.Id))
                {
                    hasChangeGrupoGestores = true;
                    lstDeletedGrupoGestores.Add(p);
                }
            }
            foreach (var p in lstDeletedGrupoGestores)
                ppGrupoGestor.Delete(p, true);

            foreach (var p in projeto.GrupoGestores)
            {
                p.Parceria = null;
                p.IdProgramaProjeto = projeto.Id;
                if (p.Id == 0)
                {
                    ppGrupoGestor.Add(p, true);
                    hasChangeGrupoGestores = true;
                }
                else
                {
                    ppGrupoGestor.Update(p, true);
                }
            }
            if (hasChangeGrupoGestores)
            {
                propriedades.Add("Grupo Gestores");
            } 
            #endregion

            #region Identificacao Territorio
            var lstDeletedIdentificacaoTerritorio = new List<IdentificacaoTerritorioInfo>();
            var ppIdentificacaoTerritorio = new IdentificacaoTerritorio();
            var lstIdentificacaoTerritorios = ppIdentificacaoTerritorio.GetByProgramaProjeto(projeto.Id);
            projeto.IdentificacoesTerritorio = projeto.IdentificacoesTerritorio ?? new List<IdentificacaoTerritorioInfo>();
            var hasChangeIdentificacaoTerritorios = false;
            foreach (var p in lstIdentificacaoTerritorios)
            {
                if (!projeto.IdentificacoesTerritorio.Any(t => t.Id == p.Id))
                {
                    hasChangeIdentificacaoTerritorios = true;
                    lstDeletedIdentificacaoTerritorio.Add(p);
                }
            }
            foreach (var p in lstDeletedIdentificacaoTerritorio)
            {
                ppIdentificacaoTerritorio.Delete(p, true);
            }

            foreach (var p in projeto.IdentificacoesTerritorio)
            {
                p.IdProgramaProjeto = projeto.Id;
                if (p.Id == 0)
                {
                    ppIdentificacaoTerritorio.Add(p, true);
                    hasChangeIdentificacaoTerritorios = true;
                }
                else
                {
                    ppIdentificacaoTerritorio.Update(p, true);
                }
            }
            if (hasChangeIdentificacaoTerritorios)
            {
                propriedades.Add("Identificação dos Territórios");
            } 
            #endregion

            #region log
            if (propriedades.Count > 0)
            {
                String descricao = "Programa/Projeto: " + projeto.Nome + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);

                int idQuadro = 0;
                if (projeto.ProgramaFederal.HasValue && projeto.Nome.ToLower().Contains("acessuas"))
                {
                    idQuadro = 80;
                }
                else if (projeto.ProgramaEstadual.HasValue && projeto.Nome.ToLower().Contains("amigo do idoso"))
                {
                    idQuadro = 81;
                }
                else
                {
                    idQuadro = 41;
                }

                var log = Log.CreateLog(projeto.IdPrefeitura, EAcao.Update, idQuadro, descricao, projeto.Id);
                if (log != null)
                {
                    new Log().Add(log, false);
                }
            }

            #endregion

            if (commit)
            {
                ContextManager.Commit();
            }
        }

        private void PreencherProgramaProjetoParcelas(ProgramaProjetoInfo projeto)
        {
            ProgramaProjetoInfo existente = GetByIdParcelas(projeto.Id);
            var parcelaExistente1 = existente.ProgramasProjetosParcelasInfo.Where(x =>x.Exercicio == 2022).FirstOrDefault();
            var parcelaExistente2 = existente.ProgramasProjetosParcelasInfo.Where(x =>x.Exercicio == 2023).FirstOrDefault();

            var parcelaNovo1 = projeto.ProgramasProjetosParcelasInfo.Where(x =>x.Exercicio == 2022).FirstOrDefault();
            var parcelaNovo2 = projeto.ProgramasProjetosParcelasInfo.Where(x =>x.Exercicio == 2023).FirstOrDefault();
            

            #region Amigo do idoso [Parcelas]
            existente.ConvenioCentroDiaIdoso = projeto.ConvenioCentroDiaIdoso;
            existente.ConvenioCentroConvivenciaIdoso = projeto.ConvenioCentroConvivenciaIdoso;

            #region Centro Dia do Idoso

            #region parcela 1
            if(parcelaExistente1 != null)
            {
                if(parcelaNovo1 != null)
                {
                    parcelaExistente1.ValorDiaIdoso = parcelaNovo1.ValorDiaIdoso;
                    parcelaExistente1.MesRepasseDiaIdoso = parcelaNovo1.MesRepasseDiaIdoso;
                    parcelaExistente1.AnoRepasseDiaIdoso = parcelaNovo1.AnoRepasseDiaIdoso;
                }
            }
            #endregion

            #region parcela 2
            if(parcelaExistente2 != null)
            {
                if(parcelaNovo2 != null)
                {
                    parcelaExistente2.ValorDiaIdoso = parcelaNovo2.ValorDiaIdoso;
                    parcelaExistente2.MesRepasseDiaIdoso = parcelaNovo2.MesRepasseDiaIdoso;
                    parcelaExistente2.AnoRepasseDiaIdoso = parcelaNovo2.AnoRepasseDiaIdoso;
                }
            }
            #endregion

            #endregion

            #region Centro de Convivencia do Idoso

            #region parcela 1
            if(parcelaExistente1 != null)
            {
                if(parcelaNovo1 != null)
                {
                    parcelaExistente1.ValorConvivenciaIdoso = parcelaNovo1.ValorConvivenciaIdoso;
                    parcelaExistente1.MesRepasseConvivenciaIdoso = parcelaNovo1.MesRepasseConvivenciaIdoso;
                    parcelaExistente1.AnoRepasseConvivenciaIdoso = parcelaNovo1.AnoRepasseConvivenciaIdoso;
                }
            }
            #endregion

            #region parcela 2
            if(parcelaExistente2 != null)
            {
                if(parcelaNovo2 != null)
                {
                    parcelaExistente2.ValorConvivenciaIdoso = parcelaNovo2.ValorConvivenciaIdoso;
                    parcelaExistente2.MesRepasseConvivenciaIdoso = parcelaNovo2.MesRepasseConvivenciaIdoso;
                    parcelaExistente2.AnoRepasseConvivenciaIdoso = parcelaNovo2.AnoRepasseConvivenciaIdoso;
                }
            }
            #endregion
            #endregion

            if (parcelaExistente1 == null)
            {
                if (existente.ProgramasProjetosParcelasInfo != null)
                {
                    existente.ProgramasProjetosParcelasInfo.Add(parcelaNovo1);
                }
            }

            if (parcelaExistente2 == null)
            {
                if (existente.ProgramasProjetosParcelasInfo != null)
                {
                    existente.ProgramasProjetosParcelasInfo.Add(parcelaNovo2);
                }
            }

            #endregion
        }

        void PreencherPrevisaoAnual(ProgramaProjetoInfo projeto)
        {
            if (projeto.PrevisaoAnual != null)
            {
                var previsao = new ProgramaProjetoPrevisaoAnualBeneficiarios().GetByProgramaProjeto(projeto.Id);
                if (previsao == null)
                {
                    previsao = new ProgramaProjetoPrevisaoAnualBeneficiariosInfo();
                    previsao.IdPrograma = projeto.Id;
                    previsao.MetaPactuadaExercicio1 = projeto.PrevisaoAnual.MetaPactuadaExercicio1;
                    previsao.MetaPactuadaExercicio2 = projeto.PrevisaoAnual.MetaPactuadaExercicio2;
                    previsao.MetaPactuadaExercicio3 = projeto.PrevisaoAnual.MetaPactuadaExercicio3;
                    previsao.MetaPactuadaExercicio4 = projeto.PrevisaoAnual.MetaPactuadaExercicio4;

                    previsao.PrevisaoAnualRepasseExercicio1 = projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio1;
                    previsao.PrevisaoAnualRepasseExercicio2 = projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio2;
                    previsao.PrevisaoAnualRepasseExercicio3 = projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio3;
                    previsao.PrevisaoAnualRepasseExercicio4 = projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio4;
                    new ProgramaProjetoPrevisaoAnualBeneficiarios().Add(previsao, true);
                }
                else
                {
                    previsao.MetaPactuadaExercicio1 = projeto.PrevisaoAnual.MetaPactuadaExercicio1;
                    previsao.MetaPactuadaExercicio2 = projeto.PrevisaoAnual.MetaPactuadaExercicio2;
                    previsao.MetaPactuadaExercicio3 = projeto.PrevisaoAnual.MetaPactuadaExercicio3;
                    previsao.MetaPactuadaExercicio4 = projeto.PrevisaoAnual.MetaPactuadaExercicio4;

                    previsao.PrevisaoAnualRepasseExercicio1 = projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio1;
                    previsao.PrevisaoAnualRepasseExercicio2 = projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio2;
                    previsao.PrevisaoAnualRepasseExercicio3 = projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio3;
                    previsao.PrevisaoAnualRepasseExercicio4 = projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio4;

                    new ProgramaProjetoPrevisaoAnualBeneficiarios().Update(previsao, true);
                }

            }
        }

        private void VerificarAlteracoesInterlocutor(InterlocutorMunicipalInfo interlocutor)
        {

        }

        public ProgramaProjetoInfo GetByIdECaracteristicasUsuarios(int idProgramaProjeto)
        {
            return _repository.GetObjectSet().Include("ProgramasProjetosRecursoFinanceiro").Include("CaracterizacaoUsuarios").Include("AcoesDesenvolvidasPrograma").Single(m => m.Id == idProgramaProjeto);
        }

        public void Add(ProgramaProjetoInfo projeto, Boolean commit)
        {
            new ValidadorProgramaProjeto().Validar(projeto);
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                if (projeto.ProgramaMunicipal.HasValue && projeto.ProgramaMunicipal.Value)
                    projeto.Ativo = true;

               

                ContextManager.OpenConnection();
                _repository.Add(projeto);

                var log = Log.CreateLog(projeto.IdPrefeitura, EAcao.Add, 40, "Incluído o Programma/Projeto " + projeto.Nome + ".");
                if (log != null)
                    new Log().Add(log, false);

                ContextManager.Commit();
                if (projeto.PossuiParceriaFormal && projeto.Parcerias != null && projeto.Parcerias.Count > 0)
                {
                    var ppp = new ProgramaProjetoParceria();
                    projeto.Parcerias.ForEach(p =>
                    {
                        p.TipoParceria = null;
                        p.Parceria = null;
                        p.IdProgramaProjeto = projeto.Id;
                        ppp.Add(p, true);
                    });
                }
                //PARCERIAS SÃO PAULO SOLIDÁRIO
                if (projeto.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.HasValue && projeto.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.Value
                    && projeto.ParceriasSaoPauloSolidarioAgendaFamilia != null && projeto.ParceriasSaoPauloSolidarioAgendaFamilia.Count > 0)
                {
                    var ppp = new SPSolidarioAgendaFamiliaParceria();
                    projeto.ParceriasSaoPauloSolidarioAgendaFamilia.ForEach(p =>
                    {
                        p.TipoParceria = null;
                        p.Parceria = null;
                        p.IdProgramaProjeto = projeto.Id;
                        ppp.Add(p, true);
                    });
                }
                //PLANEJAMENTO DE BENS SÃO PAULO SOLIDÁRIO
                if (projeto.SaoPauloSolidarioPossuiPlanejamentoBens.HasValue && projeto.SaoPauloSolidarioPossuiPlanejamentoBens.Value
                    && projeto.SaoPauloSolidarioPlanejamentoBens != null && projeto.SaoPauloSolidarioPlanejamentoBens.Count > 0)
                {
                    var ppp = new SPSolidarioPlanejamentoBens();
                    projeto.SaoPauloSolidarioPlanejamentoBens.ForEach(p =>
                    {
                        p.ProgramaProjeto = null;
                        p.IdProgramaProjeto = projeto.Id;
                        ppp.Add(p, true);
                    });
                }
                //PLANEJAMENTO DE SERVIÇOS SÃO PAULO SOLIDÁRIO
                if (projeto.SaoPauloSolidarioPossuiPlanejamentoServicos.HasValue && projeto.SaoPauloSolidarioPossuiPlanejamentoServicos.Value
                    && projeto.SaoPauloSolidarioPlanejamentoServicos != null && projeto.SaoPauloSolidarioPlanejamentoServicos.Count > 0)
                {
                    var ppp = new SPSolidarioPlanejamentoServicos();
                    projeto.SaoPauloSolidarioPlanejamentoServicos.ForEach(p =>
                    {
                        p.ProgramaProjeto = null;
                        p.IdProgramaProjeto = projeto.Id;
                        ppp.Add(p, true);
                    });
                }

                if (projeto.ProgramaMunicipal.Value && projeto.TransferenciaRendaDireta.HasValue && projeto.TransferenciaRendaDireta.Value)
                    PreencherPrevisaoAnual(projeto);
                //

                ContextManager.CloseConnection();
                ts.Complete();
            }
        }

        public void Delete(ProgramaProjetoInfo projeto, Boolean commit)
        {
            var l = new ProgramaProjetoParceria();
            var parcerias = l.GetByProgramaProjeto(projeto.Id).ToList();
            if (parcerias.Count > 0)
                foreach (var p in parcerias)
                    l.Delete(p, false);

            //if (projeto.Nome.ToLower().Contains("são paulo solidário"))
            //{
            //    var ll = new SPSolidarioAgendaFamiliaParceria();
            //    var parceriasAgendaFamilia = ll.GetByProgramaProjeto(projeto.Id).ToList();
            //    if (parceriasAgendaFamilia.Count > 0)
            //        foreach (var pc in parceriasAgendaFamilia)
            //            ll.Delete(pc, false);
            //}

            //if (projeto.Nome.ToLower().Contains("são paulo solidário"))
            //{
            //    var ll = new SPSolidarioPlanejamentoBens();
            //    var parceriasPlanejamentoBens = ll.GetByProgramaProjeto(projeto.Id).ToList();
            //    if (parceriasPlanejamentoBens.Count > 0)
            //        foreach (var pc in parceriasPlanejamentoBens)
            //            ll.Delete(pc, false);
            //}

            //if (projeto.Nome.ToLower().Contains("são paulo solidário"))
            //{
            //    var ll = new SPSolidarioPlanejamentoServicos();
            //    var parceriasPlanejamentoServicos = ll.GetByProgramaProjeto(projeto.Id).ToList();
            //    if (parceriasPlanejamentoServicos.Count > 0)
            //        foreach (var pc in parceriasPlanejamentoServicos)
            //            ll.Delete(pc, false);
            //}

            var c = new ProgramaProjetoCofinanciamento();
            var cofinanciamento = c.GetByProgramaProjeto(projeto.Id).ToList();
            if (cofinanciamento.Count > 0)
                foreach (var ppc in cofinanciamento)
                    c.Delete(ppc, false, false);

            var pa = new PlanoAcao();
            var planoAcao = pa.GetByIdProgramaProjeto(projeto.Id);
            if (planoAcao != null)
            {
                if (planoAcao.Id != null)
                {
                    pa.Delete(planoAcao, false);
                }
            }


            String descricao = "Excluído o Programa/Projeto " + projeto.Nome + ".";

            var log = Log.CreateLog(projeto.IdPrefeitura, EAcao.Remove, 40, descricao);
            if (log != null)
                new Log().Add(log, false);

            if ((projeto.ProgramaEstadual.HasValue && projeto.ProgramaEstadual.Value) || (projeto.ProgramaFederal.HasValue && projeto.ProgramaFederal.Value))
            {
                projeto.AderenciaACESSUAS = null;
                projeto.AnoInicio = null;

                projeto.ProgramasProjetosParcelasInfo = null;

                //projeto.AnoRepasseConvivenciaIdoso = null;
                //projeto.AnoRepasseDiaIdoso = null;
                //projeto.MesRepasseConvivenciaIdoso = null;
                //projeto.MesRepasseDiaIdoso = null;
                //projeto.ValorConvivenciaIdoso = null;
                //projeto.ValorDiaIdoso = null;

                projeto.AnoTermino = null;
                projeto.BeneficiarioAtendidoRedeSocioassistencial = null;

                #region Programa Projetos - Recursos financeiros Exercicio1
                var recursoExercicio1 = projeto.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == 2022).FirstOrDefault();
                if (recursoExercicio1 != null)
                {
                    recursoExercicio1.FonteFEAS = null;
                    recursoExercicio1.FonteFMAS = null;
                    recursoExercicio1.FonteFNAS = null;
                    recursoExercicio1.FonteFundoEstadual = null;
                    recursoExercicio1.FonteFundoFederal = null;
                    recursoExercicio1.FonteFundoMunicipal = null;
                    recursoExercicio1.FonteIGDPBF = null;
                    recursoExercicio1.FonteIGDSUAS = null;
                    recursoExercicio1.FonteOrcamentoEstadual = null;
                    recursoExercicio1.FonteOrcamentoFederal = null;
                    recursoExercicio1.FonteOrcamentoMunicipal = null;
                    recursoExercicio1.ValorFEAS = null;
                    recursoExercicio1.ValorFMAS = null;
                    recursoExercicio1.ValorFNAS = null;
                    recursoExercicio1.ValorFundoEstadual = null;
                    recursoExercicio1.ValorFundoFederal = null;
                    recursoExercicio1.ValorFundoMunicipal = null;
                    recursoExercicio1.ValorIGDPBF = null;
                    recursoExercicio1.ValorIGDSUAS = null;
                    recursoExercicio1.ValorOrcamentoEstadual = null;
                    recursoExercicio1.ValorOrcamentoFederal = null;
                    recursoExercicio1.ValorOrcamentoMunicipal = null;
                }
                #endregion

                #region Programa Projetos - Recursos financeiros Exercicio2
                var recursoExercicio2 = projeto.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == 2023).FirstOrDefault();
                if (recursoExercicio2 != null)
                {
                    recursoExercicio2.FonteFEAS = null;
                    recursoExercicio2.FonteFMAS = null;
                    recursoExercicio2.FonteFNAS = null;
                    recursoExercicio2.FonteFundoEstadual = null;
                    recursoExercicio2.FonteFundoFederal = null;
                    recursoExercicio2.FonteFundoMunicipal = null;
                    recursoExercicio2.FonteIGDPBF = null;
                    recursoExercicio2.FonteIGDSUAS = null;
                    recursoExercicio2.FonteOrcamentoEstadual = null;
                    recursoExercicio2.FonteOrcamentoFederal = null;
                    recursoExercicio2.FonteOrcamentoMunicipal = null;
                    recursoExercicio2.ValorFEAS = null;
                    recursoExercicio2.ValorFMAS = null;
                    recursoExercicio2.ValorFNAS = null;
                    recursoExercicio2.ValorFundoEstadual = null;
                    recursoExercicio2.ValorFundoFederal = null;
                    recursoExercicio2.ValorFundoMunicipal = null;
                    recursoExercicio2.ValorIGDPBF = null;
                    recursoExercicio2.ValorIGDSUAS = null;
                    recursoExercicio2.ValorOrcamentoEstadual = null;
                    recursoExercicio2.ValorOrcamentoFederal = null;
                    recursoExercicio2.ValorOrcamentoMunicipal = null;
                }
                #endregion

                #region Programa Projetos - Recursos financeiros Exercicio3
                var recursoExercicio3 = projeto.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == 2024).FirstOrDefault();
                if (recursoExercicio3 != null)
                {
                    recursoExercicio3.FonteFEAS = null;
                    recursoExercicio3.FonteFMAS = null;
                    recursoExercicio3.FonteFNAS = null;
                    recursoExercicio3.FonteFundoEstadual = null;
                    recursoExercicio3.FonteFundoFederal = null;
                    recursoExercicio3.FonteFundoMunicipal = null;
                    recursoExercicio3.FonteIGDPBF = null;
                    recursoExercicio3.FonteIGDSUAS = null;
                    recursoExercicio3.FonteOrcamentoEstadual = null;
                    recursoExercicio3.FonteOrcamentoFederal = null;
                    recursoExercicio3.FonteOrcamentoMunicipal = null;
                    recursoExercicio3.ValorFEAS = null;
                    recursoExercicio3.ValorFMAS = null;
                    recursoExercicio3.ValorFNAS = null;
                    recursoExercicio3.ValorFundoEstadual = null;
                    recursoExercicio3.ValorFundoFederal = null;
                    recursoExercicio3.ValorFundoMunicipal = null;
                    recursoExercicio3.ValorIGDPBF = null;
                    recursoExercicio3.ValorIGDSUAS = null;
                    recursoExercicio3.ValorOrcamentoEstadual = null;
                    recursoExercicio3.ValorOrcamentoFederal = null;
                    recursoExercicio3.ValorOrcamentoMunicipal = null;
                }
                #endregion

                #region Programa Projetos - Recursos financeiros Exercicio4
                var recursoExercicio4 = projeto.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == 2025).FirstOrDefault();
                if (recursoExercicio4 != null)
                {
                    recursoExercicio4.FonteFEAS = null;
                    recursoExercicio4.FonteFMAS = null;
                    recursoExercicio4.FonteFNAS = null;
                    recursoExercicio4.FonteFundoEstadual = null;
                    recursoExercicio4.FonteFundoFederal = null;
                    recursoExercicio4.FonteFundoMunicipal = null;
                    recursoExercicio4.FonteIGDPBF = null;
                    recursoExercicio4.FonteIGDSUAS = null;
                    recursoExercicio4.FonteOrcamentoEstadual = null;
                    recursoExercicio4.FonteOrcamentoFederal = null;
                    recursoExercicio4.FonteOrcamentoMunicipal = null;
                    recursoExercicio4.ValorFEAS = null;
                    recursoExercicio4.ValorFMAS = null;
                    recursoExercicio4.ValorFNAS = null;
                    recursoExercicio4.ValorFundoEstadual = null;
                    recursoExercicio4.ValorFundoFederal = null;
                    recursoExercicio4.ValorFundoMunicipal = null;
                    recursoExercicio4.ValorIGDPBF = null;
                    recursoExercicio4.ValorIGDSUAS = null;
                    recursoExercicio4.ValorOrcamentoEstadual = null;
                    recursoExercicio4.ValorOrcamentoFederal = null;
                    recursoExercicio4.ValorOrcamentoMunicipal = null;
                }
                #endregion

                projeto.IdFaseProgramaSaoPauloSolidario = null;


                #region previsão anual repasse
                projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio1 = 0;
                projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio2 = 0;
                projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio3 = 0;
                projeto.PrevisaoAnual.PrevisaoAnualRepasseExercicio4 = 0;
                #endregion

                #region Meta pactuada

                projeto.PrevisaoAnual.MetaPactuadaExercicio1 = 0;
                projeto.PrevisaoAnual.MetaPactuadaExercicio2 = 0;
                projeto.PrevisaoAnual.MetaPactuadaExercicio3 = 0;
                projeto.PrevisaoAnual.MetaPactuadaExercicio4 = 0;

                #endregion

                #region Interlocultor municipal

                if (projeto.InterlocutorMunicipal != null)
                {
                    projeto.InterlocutorMunicipal.Nome = String.Empty;
                    projeto.InterlocutorMunicipal.Telefone = String.Empty;
                    projeto.InterlocutorMunicipal.Celular = String.Empty;
                    projeto.InterlocutorMunicipal.Email = String.Empty;
                    projeto.PossuiInterlocutorMunicipal = null;                    
                }


                #endregion


                projeto.ConvenioCentroDiaIdoso = false;
                projeto.ConvenioCentroConvivenciaIdoso = false;
                projeto.DataInauguracaoConvivenciaIdoso = null;
                projeto.IdCRASReferencia = 0;

                if (projeto.ProgramasProjetosParcelasInfo != null)
                {
                    foreach (var item in projeto.ProgramasProjetosParcelasInfo)
                    {
                        item.MesRepasseConvivenciaIdoso = null;
                        item.MesRepasseDiaIdoso = null;
                        item.AnoRepasseConvivenciaIdoso = null;
                        item.AnoRepasseDiaIdoso = null;
                        item.ValorConvivenciaIdoso = null;
                        item.ValorDiaIdoso = null;
                    }                    
                }

                projeto.DataAdesaoPrograma = null;

                projeto.Acoes = String.Empty;
                projeto.AbrangenciaTerritorial = null;
                projeto.Parcerias = null;

                projeto.ExecutaPrograma = false;

                projeto.MetaPactuada = null;
                projeto.PossuiParceriaFormal = false;
                projeto.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia = null;
                projeto.SaoPauloSolidarioAnoInicioBuscaAtiva = null;
                projeto.SaoPauloSolidarioAnoRepasseFEASAgendaFamilia = null;
                projeto.SaoPauloSolidarioAnoRepasseFEASBuscaAtiva = null;
                projeto.SaoPauloSolidarioAnoTerminoBuscaAtiva = null;
                projeto.SaoPauloSolidarioCRASExecutaAgendaFamilia = null;
                projeto.SaoPauloSolidarioCRASExecutaBuscaAtiva = null;
                projeto.SaoPauloSolidarioCREASExecutaAgendaFamilia = null;
                projeto.SaoPauloSolidarioCREASExecutaBuscaAtiva = null;
                projeto.SaoPauloSolidarioMesInicioBuscaAtiva = null;
                projeto.SaoPauloSolidarioMesRepasseFEASAgendaFamilia = null;
                projeto.SaoPauloSolidarioMesRepasseFEASBuscaAtiva = null;
                projeto.SaoPauloSolidarioMesTerminoBuscaAtiva = null;
                projeto.SaoPauloSolidarioMeta = null;
                projeto.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013 = null;
                projeto.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2014 = null;
                projeto.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2015 = null;
                projeto.SaoPauloSolidarioOrgaoGestorExecutaAgendaFamilia = null;
                projeto.SaoPauloSolidarioOrgaoGestorExecutaBuscaAtiva = null;
                projeto.SaoPauloSolidarioRepasseAnual = null;
                projeto.SaoPauloSolidarioUnidadePrivadaExecutaBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorFEASAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorFEASBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorFEASRetidoFMAS2014 = null;
                projeto.SaoPauloSolidarioValorFMASAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorFMASBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorFNASAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorFNASBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorFundoEstadualAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorFundoEstadualBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorFundoFederalAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorFundoFederalBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorFundoMunicipalAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorFundoMunicipalBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorIGDPBFAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorIGDPBFBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorIGDSUASAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorIGDSUASBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorOrcamentoEstadualAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorOrcamentoEstadualBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorOrcamentoFederalAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorOrcamentoFederalBuscaAtiva = null;
                projeto.SaoPauloSolidarioValorOrcamentoMunicipalAgendaFamilia = null;
                projeto.SaoPauloSolidarioValorOrcamentoMunicipalBuscaAtiva = null;

               

                projeto.ValorPrevisaoAnualACESSUAS = null;
                projeto.SaoPauloSolidarioPossuiPlanejamentoBens = null;
                projeto.SaoPauloSolidarioPossuiPlanejamentoServicos = null;
                if (projeto.PossuiUnidadePrivada.HasValue)
                    projeto.PossuiUnidadePrivada = false;
                projeto.NumeroBeneficiariosMensal = null;


                if (projeto.AcoesDesenvolvidasPrograma != null)
                {
                    (ContextManager.GetContext() as PMASContext).DeletarAcoesDesenvolvidaProgramas(projeto.Id);
                    projeto.AcoesDesenvolvidasPrograma = null;
                }

                _repository.Update(projeto);

                if (commit)
                    ContextManager.Commit();

                return;
            }

            _repository.Delete(projeto);
            if (commit)
                ContextManager.Commit();

        }

        public List<String> GetLabelForInfo(List<String> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "Nome": labels.Add("nome"); break;
                    case "Objetivo": labels.Add("objetivo"); break;
                    case "Acoes": labels.Add("principais ações e atividades realizadas"); break;
                    case "IdAbrangenciaTerritorial": labels.Add("abrângencia territorial"); break;
                    case "PossuiParceriaFormal": labels.Add("parcerias"); break;
                    case "MesInicio":
                    case "AnoInicio":
                        labels.Add("início do programa/projeto"); break;
                    case "MesTermino":
                    case "AnoTermino":
                        labels.Add("previsão de término do programa/projeto"); break;
                     
                    case "IdUsuarioTransferenciaRenda":
                        labels.Add("beneficiários"); break;
                    case "AderenciaACESSUAS":
                        labels.Add("aderência ACESSUAS"); break;

                    case "MetaPactuada":
                        labels.Add("meta pactuada"); break;
                    case "ValorPrevisaoAnualACESSUAS":
                        labels.Add("previsão anual do valor do repasse ACESSUAS"); break;
                    case "ValorDiaIdoso":
                        labels.Add("valor recebido Centro Dia do Idoso"); break;
                    case "MesRepasseDiaIdoso":
                    case "AnoRepasseDiaIdoso":
                        labels.Add("data repasse Centro Dia do Idoso"); break;
                    case "ValorConvivenciaIdoso":
                        labels.Add("valor recebido Centro de Convivência do Idoso"); break;
                    case "MesRepasseConvivenciaIdoso":
                    case "AnoRepasseConvivenciaIdoso":
                        labels.Add("data repasse Centro de Convivência do Idoso"); break;
                    case "ValorFMAS":
                        labels.Add("valor FMAS"); break;
                    case "ValorOrcamentoMunicipal":
                        labels.Add("valor orçamento municipal"); break;
                    case "ValorFundoMunicipal":
                        labels.Add("valor outros fundos municipais"); break;
                    case "ValorFEAS":
                        labels.Add("valor FEAS"); break;
                    case "ValorOrcamentoEstadual":
                        labels.Add("valor orçamento estadual"); break;
                    case "ValorFundoEstadual":
                        labels.Add("valor outros fundos estaduais"); break;
                    case "ValorFNAS":
                        labels.Add("valor FNAS"); break;
                    case "ValorOrcamentoFederal":
                        labels.Add("valor orçamento federal"); break;
                    case "ValorFundoFederal":
                        labels.Add("valor outros fundos nacionais"); break;
                    case "ValorIGDPBF":
                        labels.Add("valor IGD-PBF"); break;
                    case "ValorIGDSUAS":
                        labels.Add("valor IGD-SUAS"); break;
                    case "IdFaseProgramaSaoPauloSolidario":
                        labels.Add("fase programa São Paulo Solidário"); break;
                    case "SaoPauloSolidarioMesInicioBuscaAtiva":
                    case "SaoPauloSolidarioAnoInicioBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("data início da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioMesTerminoBuscaAtiva":
                    case "SaoPauloSolidarioAnoTerminoBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("data término da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorFMASBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor FMAS da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorOrcamentoMunicipalBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor orçamento municipal da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorFundoMunicipalBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor outros fundos municipais da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorFEASBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor FEAS da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorOrcamentoEstadualBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor orçamento estadual da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorFundoEstadualBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor outros fundos estaduais da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorFNASBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor FNAS da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorOrcamentoFederalBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor orçamento federal da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorFundoFederalBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor outros fundos nacionais da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorIGDPBFBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor IGD-PBF da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioValorIGDSUASBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor IGD-SUAS da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioOrgaoGestorExecutaBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("orgãos executores (Órgão Gestor) da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioCRASExecutaBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("orgãos executores (CRAS) da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioCREASExecutaBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("orgãos executores (CREAS) da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioUnidadePrivadaExecutaBuscaAtiva":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("orgãos executores (Unidade Privada) da etapa Busca Ativa"); break;
                    case "SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("número de famílias da etapa Agenda da Família em 2013"); break;
                    case "SaoPauloSolidarioNumeroFamiliasAgendaFamilia2014":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("número de famílias da etapa Agenda da Família em 2014"); break;
                    case "SaoPauloSolidarioOrgaoGestorExecutaAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("orgãos executores (Órgão Gestor) da etapa Agenda da Família"); break;
                    case "SaoPauloSolidarioCRASExecutaAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("orgãos executores (CRAS) da etapa Agenda da Família"); break;
                    case "SaoPauloSolidarioCREASExecutaAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("orgãos executores (CREAS) da etapa Agenda da Família"); break;
                    case "PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("parcerias da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorFMASAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor FMAS da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorOrcamentoMunicipalAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor orçamento municipal da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorFundoMunicipalAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor outros fundos municipais da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorFEASAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor FEAS da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorOrcamentoEstadualAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor orçamento estadual da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorFundoEstadualAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor outros fundos estaduais da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorFNASAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor FNAS da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorOrcamentoFederalAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor orçamento federal da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorFundoFederalAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor outros fundos nacionais da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorIGDPBFAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor IGD-PBF da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioValorIGDSUASAgendaFamilia":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("valor IGD-SUAS da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioPossuiPlanejamentoBens":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("planejamento de bens da etapa Além da Renda"); break;
                    case "SaoPauloSolidarioPossuiPlanejamentoServicos":
                        if (!labels.Contains("fase programa São Paulo Solidário"))
                            labels.Add("planejamento de serviços da etapa Além da Renda"); break;

                }
            }
            return labels.Distinct().ToList();
        }

        public List<String> GetLabelForFamiliaPaulistaInfo(List<String> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "AderiuFamiliaPaulista":
                        labels.Add("aderiu ao Programa Família Paulista"); break;
                    case "PossuiInterlocutorMunicipal": break;
                    case "MetaPactuada":
                        labels.Add("Meta definida pelo município"); break;
                    case "DataAdesaoPrograma":
                        labels.Add("Data de assinatura do termo de Adesão do Programa Família Paulista");
                        break;
                    case "ValorFEAS":
                        labels.Add("Valor FEAS 1° parcela"); break;
                    case "MesRepasse":
                        labels.Add("Mês de Repasse da 1° Parcela");
                        break;
                    case "AnoRepasse":
                        labels.Add("Ano de Repasse da 1° Parcela");
                        break;
                    case "ValorFMAS":
                        labels.Add("valor FMAS"); break;
                    case "ValorOrcamentoMunicipal":
                        labels.Add("valor orçamento municipal"); break;
                    case "ValorFundoMunicipal":
                        labels.Add("valor outros fundos municipais"); break;
                    case "ValorOrcamentoEstadual":
                        labels.Add("valor orçamento estadual"); break;
                    case "ValorFundoEstadual":
                        labels.Add("valor outros fundos estaduais"); break;
                    case "ValorFNAS":
                        labels.Add("valor FNAS"); break;
                    case "ValorOrcamentoFederal":
                        labels.Add("valor orçamento federal"); break;
                    case "ValorFundoFederal":
                        labels.Add("valor outros fundos nacionais"); break;
                    case "ValorIGDPBF":
                        labels.Add("valor IGD-PBF"); break;
                    case "ValorIGDSUAS":
                        labels.Add("valor IGD-SUAS"); break;
                }
            }
            return labels.Distinct().ToList();
        }

        public List<String> GetLabelForPlanoAcao(List<String> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "Data":
                        labels.Add("Data Plano Acao"); break;
                }
            }
            return labels.Distinct().ToList();

        }
    }
}
