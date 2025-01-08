using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class OrgaoGestor
    {
        private static IRepository<OrgaoGestorInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<OrgaoGestorInfo>>();
            }
        }

        public IQueryable<OrgaoGestorInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public OrgaoGestorInfo GetById(int id)
        {
            var orgao = _repository.Single(m => m.Id == id);
            OrgaoGestorEquipeEspecifica orgaoGestorEquipeEspecifica = new OrgaoGestorEquipeEspecifica();
            if (orgao != null)
            {
                orgao.EquipesEspecificasTotais = new OrgaoGestorEquipeEspecificaTotais().GetByOrgaoGestorEquipeEspecificaTotais(orgao.Id).ToList();
                OrgaoGestorIntencaoEstruturacaoEquipe orgaoGestorIntencaoEstruturacaoEquipe = new OrgaoGestorIntencaoEstruturacaoEquipe();

                if (orgao.EquipesEspecificasTotais != null)
                    foreach (var total in orgao.EquipesEspecificasTotais)
                    {

                        if ((total.PossuiEquipeProtecaoBasica.Value == 1)
                            || (total.PossuiEquipeProtecaoEspecial.Value == 1)
                            || (total.PossuiEquipeVigilanciaSocioassistencial.Value == 1)
                            || (total.PossuiEquipeGestaoTransferenciaRenda.Value == 1)
                            || (total.PossuiEquipeCadUnico.Value == 1)
                            || (total.PossuiEquipeGestaoFinanceira.Value == 1)
                            || (total.PossuiEquipeGestaoSUAS.Value == 1)
                            || (total.PossuiEquipeRegulacaoSUAS.Value == 1)
                            || (total.PossuiEquipeRedeDireta.Value == 1)
                            || (total.PossuiOutrasEquipes.Value == 1))
                        {
                            orgaoGestorEquipeEspecifica.GetByOrgaoGestorEquipeEspecifica(orgao.Id).Where(x => x.Exercicio == total.Exercicio).ToList();
                            orgaoGestorIntencaoEstruturacaoEquipe.GetByOrgaoGestorByExercicio(orgao.Id, total.Exercicio.Value).ToList();
                        }
                    }
            }
                
            return orgao;
        }

        public OrgaoGestorInfo GetByPrefeitura(int idPrefeitura)
        {
            var orgao = _repository.Single(m => m.IdPrefeitura == idPrefeitura);
            OrgaoGestorEquipeEspecifica orgaoGestorEquipeEspecifica = new OrgaoGestorEquipeEspecifica();
            OrgaoGestorIntencaoEstruturacaoEquipe orgaoGestorIntencaoEstruturacaoEquipe = new OrgaoGestorIntencaoEstruturacaoEquipe();

            if (orgao != null)
            {
                orgao.EquipesEspecificasTotais = new OrgaoGestorEquipeEspecificaTotais().GetByOrgaoGestorEquipeEspecificaTotais(orgao.Id).Where(s => s.Exercicio >= 2022).ToList();
                if (orgao.EquipesEspecificasTotais != null)
                {
                    foreach (var total in orgao.EquipesEspecificasTotais)
                    {
                        if ((total.PossuiEquipeProtecaoBasica.HasValue && total.PossuiEquipeProtecaoBasica.Value == 1)
                            || (total.PossuiEquipeProtecaoEspecial.HasValue && total.PossuiEquipeProtecaoEspecial.Value == 1)
                            || (total.PossuiEquipeVigilanciaSocioassistencial.HasValue && total.PossuiEquipeVigilanciaSocioassistencial.Value == 1)
                            || (total.PossuiEquipeGestaoTransferenciaRenda.HasValue && total.PossuiEquipeGestaoTransferenciaRenda.Value == 1)
                            || (total.PossuiEquipeCadUnico.HasValue && total.PossuiEquipeCadUnico.Value == 1)
                            || (total.PossuiEquipeGestaoFinanceira.HasValue && total.PossuiEquipeGestaoFinanceira.Value == 1)
                            || (total.PossuiEquipeGestaoSUAS.HasValue && total.PossuiEquipeGestaoSUAS.Value == 1)
                            || (total.PossuiEquipeRegulacaoSUAS.HasValue && total.PossuiEquipeRegulacaoSUAS.Value == 1)
                            || (total.PossuiEquipeRedeDireta.HasValue && total.PossuiEquipeRedeDireta.Value == 1)
                            || (total.PossuiOutrasEquipes.HasValue && total.PossuiOutrasEquipes.Value == 1)
                           )
                        {
                            orgaoGestorEquipeEspecifica.GetByOrgaoGestorEquipeEspecifica(orgao.Id).Where(x => x.Exercicio == total.Exercicio).ToList();
                            orgaoGestorIntencaoEstruturacaoEquipe.GetByOrgaoGestorByExercicio(orgao.Id, total.Exercicio.Value).ToList();
                        }
                        else 
                        { 
                            orgaoGestorIntencaoEstruturacaoEquipe.GetByOrgaoGestorByExercicio(orgao.Id, total.Exercicio.Value).ToList(); 
                        }
                    }
                }
            }
            return orgao;

        }

        public OrgaoGestorInfo GetByPrefeituraExercicio(int idPrefeitura, int exercicio)
        {
            var orgao = _repository.Single(m => m.IdPrefeitura == idPrefeitura);
            OrgaoGestorEquipeEspecifica orgaoGestorEquipeEspecifica = new OrgaoGestorEquipeEspecifica();
            OrgaoGestorIntencaoEstruturacaoEquipe orgaoGestorIntencaoEstruturacaoEquipe = new OrgaoGestorIntencaoEstruturacaoEquipe();

            if (orgao != null)
            {
                orgao.EquipesEspecificasTotais = new OrgaoGestorEquipeEspecificaTotais().GetByOrgaoGestorEquipeEspecificaTotais(orgao.Id).Where(s => s.Exercicio >= 2022).ToList();
                if (orgao.EquipesEspecificasTotais != null)
                {
                    foreach (var total in orgao.EquipesEspecificasTotais)
                    {
                        if ((total.PossuiEquipeProtecaoBasica.HasValue && total.PossuiEquipeProtecaoBasica.Value == 1)
                            || (total.PossuiEquipeProtecaoEspecial.HasValue && total.PossuiEquipeProtecaoEspecial.Value == 1)
                            || (total.PossuiEquipeVigilanciaSocioassistencial.HasValue && total.PossuiEquipeVigilanciaSocioassistencial.Value == 1)
                            || (total.PossuiEquipeGestaoTransferenciaRenda.HasValue && total.PossuiEquipeGestaoTransferenciaRenda.Value == 1)
                            || (total.PossuiEquipeCadUnico.HasValue && total.PossuiEquipeCadUnico.Value == 1)
                            || (total.PossuiEquipeGestaoFinanceira.HasValue && total.PossuiEquipeGestaoFinanceira.Value == 1)
                            || (total.PossuiEquipeGestaoSUAS.HasValue && total.PossuiEquipeGestaoSUAS.Value == 1)
                            || (total.PossuiEquipeGestorSUAS.HasValue && total.PossuiEquipeGestorSUAS.Value == 1)
                            || (total.PossuiEquipeRegulacaoSUAS.HasValue && total.PossuiEquipeRegulacaoSUAS.Value == 1)
                            || (total.PossuiEquipeRedeDireta.HasValue && total.PossuiEquipeRedeDireta.Value == 1)
                            || (total.PossuiOutrasEquipes.HasValue && total.PossuiOutrasEquipes.Value == 1)
                           )
                        {

                            orgaoGestorEquipeEspecifica.GetByOrgaoGestorEquipeEspecifica(orgao.Id).Where(x => x.Exercicio == exercicio).ToList();
                            orgaoGestorIntencaoEstruturacaoEquipe.GetByOrgaoGestorByExercicio(orgao.Id, exercicio).ToList();
                        }
                    }
                }
            }
            return orgao;

        }

        public void Update(OrgaoGestorInfo orgaoAlterado, Boolean commit)
        {
            #region validacao
            new ValidadorOrgaoGestor().ValidarOrgaoExercicio(orgaoAlterado); 
            #endregion

            #region propriedades
            var equipeEspecifica = new OrgaoGestorEquipeEspecifica();
            var equipeEspecificaTotais = new OrgaoGestorEquipeEspecificaTotais();
            var intencaoEstruturacaoEquipe = new OrgaoGestorIntencaoEstruturacaoEquipe();

            var hasChangeEquipes = false;
            var hasChangeEquipesTotais = false;
            var hasChangeIntencaoEstruturacaoEquipe = false; 
            #endregion

            var equipesExistentes = equipeEspecifica.GetByOrgaoGestorEquipeEspecificaByExercicio(orgaoAlterado.Id, orgaoAlterado.Exercicio);
            var equipesTotaisExistentes = equipeEspecificaTotais.GetByOrgaoGestorEquipeEspecificaTotaisByExercicio(orgaoAlterado.Id, orgaoAlterado.Exercicio);
            var intencoesEstruturacao = intencaoEstruturacaoEquipe.GetByOrgaoGestorByExercicio(orgaoAlterado.Id, orgaoAlterado.Exercicio);

            orgaoAlterado.EquipesEspecificas = orgaoAlterado.EquipesEspecificas ?? new List<EquipeEspecificaInfo>();


            #region merge equipes existentes
            hasChangeEquipes = MergeEquipesExistentes(orgaoAlterado, equipeEspecifica, equipesExistentes);
            #endregion

            #region merge equipes especificas totais
            hasChangeEquipesTotais = MergeEquipesTotaisExistentes(orgaoAlterado, equipeEspecificaTotais, equipesTotaisExistentes);
            #endregion

            #region merge intencao de extruturacao equipe
            hasChangeIntencaoEstruturacaoEquipe = MergeEquipesIntencaoEstruturacao(orgaoAlterado, intencaoEstruturacaoEquipe, intencoesEstruturacao);
            #endregion

            #region Atualizar
            _repository.Update(orgaoAlterado); 
            #endregion

            var propriedadesEntity = _repository.GetModifiedProperties(orgaoAlterado);

            #region quadro 4
            var propriedades = GetLabelForInfo(propriedadesEntity.Where(t => !(t.StartsWith("Total") || t.StartsWith("Equipe") || t.StartsWith("Possui"))).ToList(), orgaoAlterado);
            if (propriedades.Count > 0)
            {
                var log = Log.CreateLog(orgaoAlterado.IdPrefeitura, EAcao.Update, 4, Log.CreateDescricaoDefaultUpdate(propriedades));
                if (log != null)
                    new Log().Add(log, false);
            } 
            #endregion

            #region quadro 5
            propriedades = GetLabelForInfo(propriedadesEntity.Where(t => t.StartsWith("Total")).ToList(), orgaoAlterado);
            if (propriedades.Count > 0)
            {
                var log = Log.CreateLog(orgaoAlterado.IdPrefeitura, EAcao.Update, 5, Log.CreateDescricaoDefaultUpdate(propriedades));
                if (log != null)
                    new Log().Add(log, false);
            }
            #endregion

            #region quadro 6

            propriedades = GetLabelForInfo(propriedadesEntity.Where(t => t.StartsWith("Possui") || t.StartsWith("Equipe")).ToList(), orgaoAlterado);
            if (propriedades.Count > 0)
            {
                var log = Log.CreateLog(orgaoAlterado.IdPrefeitura, EAcao.Update, 6, Log.CreateDescricaoDefaultUpdate(propriedades));
                if (log != null)
                    new Log().Add(log, false);
            } 
            #endregion

            #region finalizar
            if (commit)
            {
                ContextManager.Commit();
            } 
            #endregion
        }

        public void UpdateIdentificacao(OrgaoGestorInfo orgaoAlterado, Boolean commit)
        {
            #region validacao
            new ValidadorOrgaoGestor().ValidarOrgaoIdentificacao(orgaoAlterado);
            #endregion

            #region Atualizar
            _repository.Update(orgaoAlterado);
            #endregion

            var propriedadesEntity = _repository.GetModifiedProperties(orgaoAlterado);

            #region quadro 1.3 Identificação 
            var propriedades = GetLabelForInfo(propriedadesEntity.ToList(), orgaoAlterado);
            if (propriedades.Count > 0)
            {
                var log = Log.CreateLog(orgaoAlterado.IdPrefeitura, EAcao.Update, 4, Log.CreateDescricaoDefaultUpdate(propriedades));
                if (log != null)
                    new Log().Add(log, false);
            }
            #endregion

            
            #region finalizar
            if (commit)
            {
                ContextManager.Commit();
            }
            #endregion
        }

        public void AddIdentificacao(OrgaoGestorInfo obj, Boolean commit)
        {
            new ValidadorOrgaoGestor().ValidarOrgaoIdentificacao(obj);
            if (_repository.GetQuery().Any(t => t.IdPrefeitura == obj.IdPrefeitura))
                throw new Exception("já existe um cadastro do Órgão Gestor");
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(OrgaoGestorInfo obj, Boolean commit)
        {
            new ValidadorOrgaoGestor().ValidarOrgaoExercicio(obj);
            if (_repository.GetQuery().Any(t => t.IdPrefeitura == obj.IdPrefeitura))
                throw new Exception("já existe um cadastro do Órgão Gestor");
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public List<String> GetLabelForInfo(List<String> propriedades, OrgaoGestorInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "CNPJ": labels.Add("CNPJ"); break;
                    case "Nome": labels.Add("nome"); break;
                    case "IdEstrutura":
                    case "EstruturaOutros":
                        labels.Add("estrutura"); break;
                    case "Lei": labels.Add("número da lei"); break;
                    case "DataLei": labels.Add("data de publicação da lei"); break;
                    case "AlteracaoLei": labels.Add("houve alteração na lei"); break;
                    case "LeiAlterada": if (obj.AlteracaoLei.HasValue && obj.AlteracaoLei.Value) labels.Add("número da lei alterada"); break;
                    case "DataLeiAlterada": if (obj.AlteracaoLei.HasValue && obj.AlteracaoLei.Value) labels.Add("data de publicação da lei alterada"); break;
                    case "CEP": labels.Add("CEP"); break;
                    case "Logradouro": labels.Add("logradouro"); break;
                    case "Numero": labels.Add("número"); break;
                    case "Complemento": labels.Add("complemento"); break;
                    case "Cidade": labels.Add("cidade"); break;
                    case "Bairro": labels.Add("bairro"); break;
                    case "Telefone": labels.Add("telefone"); break;
                    case "Celular": labels.Add("Celular"); break;
                    case "Site": labels.Add("web site"); break;
                    case "Email": labels.Add("e-mail institucional"); break;
                    case "TotalFuncionarios":
                    case "TotalFuncionariosNivelFundamental":
                    case "TotalFuncionariosNivelMedio":
                    case "TotalFuncionariosSuperiorServicoSocial":
                    case "TotalFuncionariosSuperiorPsicologia":
                    case "TotalFuncionariosSuperiorPedagogia":
                    case "TotalFuncionariosSuperiorSociologia":
                    case "TotalFuncionariosSuperior":
                    case "TotalFuncionariosPosGraduacao":
                    case "TotalFuncionariosSuperiorDireito":
                    case "TotalFuncionariosSemEscolaridade":
                    case "TotalEstagiarios":
                    case "TotalFuncionariosSuperiorAdministracao":
                    case "TotalFuncionariosSuperiorAntropologia":
                    case "TotalFuncionariosSuperiorContabilidade":
                    case "TotalFuncionariosSuperiorEconomia":
                    case "TotalFuncionariosSuperiorEconomiaDomestica":
                    case "TotalFuncionariosSuperiorTerapiaOcupacional":
                        labels.Add("total de trabalhadores segundo a escolaridade"); break;
                    case "PossuiEquipeProtecaoBasica":
                    case "EquipeProtecaoBasica":
                        labels.Add("equipe específica para coordenação da Proteção Social Básica"); break;
                    case "PossuiEquipeProtecaoEspecial":
                    case "EquipeProtecaoEspecial":
                        labels.Add("equipe específica para coordenação da Proteção Social Especial"); break;
                    case "PossuiEquipeVigilanciaSocioassistencial":
                    case "EquipeVigilanciaSocioassistencial":
                        labels.Add("equipe específica para coordenação da Vigilância Socioassistencial"); break;
                    case "PossuiEquipeGestaoTransferenciaRenda":
                    case "EquipeGestaoTransferenciaRenda":
                    case "PossuiEquipeGestaoBeneficiosProgramas":
                    case "EquipeGestaoBeneficiosProgramas":
                        labels.Add("equipe específica para gestão dos programas de transferência de Renda"); break;
                }
            }
            return labels.Distinct().ToList();
        }

        #region helper

        #region [Equipes Especificas - merge]
        private static bool MergeEquipesExistentes(
            OrgaoGestorInfo orgaoAlterado, OrgaoGestorEquipeEspecifica equipeEspecifica, IQueryable<EquipeEspecificaInfo> equipesExistentes)
        {
            bool hasChangeEquipes = false;
            if (equipesExistentes.Count() > 0)
            {
                hasChangeEquipes = DeletarEquipesRemovidasNaAlteracaoDoOrgaoExistente(orgaoAlterado, equipeEspecifica, equipesExistentes);
            }
            hasChangeEquipes = AdicionarAtualizarEquipesNovasNoOrgaoExistente(orgaoAlterado, equipeEspecifica, equipesExistentes);
            return hasChangeEquipes;
        }

        private static bool AdicionarAtualizarEquipesNovasNoOrgaoExistente(
            OrgaoGestorInfo orgaoAlterado, OrgaoGestorEquipeEspecifica equipeEspecifica, IQueryable<EquipeEspecificaInfo> equipesExistentes)
        {
            bool hasChangeEquipes = false;
            foreach (var e in orgaoAlterado.EquipesEspecificas)
            {
                e.Id = equipesExistentes.Where(l => l.IdOrgaoGestor == e.IdOrgaoGestor && l.IdTipoEquipe == e.IdTipoEquipe).Select(l => l.Id).FirstOrDefault();
                e.IdOrgaoGestor = orgaoAlterado.Id;
                if (e.Id == 0)
                {
                    equipeEspecifica.Add(e, true);
                    hasChangeEquipes = true;
                }
                else
                {
                    equipeEspecifica.Update(e, true);
                }
            }
            return hasChangeEquipes;
        }

        private static bool DeletarEquipesRemovidasNaAlteracaoDoOrgaoExistente(
            OrgaoGestorInfo orgaoAlterado, OrgaoGestorEquipeEspecifica equipeEspecifica, IQueryable<EquipeEspecificaInfo> equipesExistentes)
        {
            var deletados = new List<EquipeEspecificaInfo>();
            bool hasChangeEquipes = false;
            foreach (var equipeExistente in equipesExistentes)
            {
                if (!orgaoAlterado.EquipesEspecificas
                    .Any(equipeAlterada => (equipeAlterada.IdTipoEquipe == equipeExistente.IdTipoEquipe)
                                        && (equipeAlterada.IdOrgaoGestor == equipeExistente.IdOrgaoGestor)
                                        && (equipeAlterada.Exercicio == equipeExistente.Exercicio))
                        )
                {
                    hasChangeEquipes = true;
                    deletados.Add(equipeExistente);
                }
            }

            foreach (var e in deletados)
            {
                equipeEspecifica.Delete(e, true);
            }
            return hasChangeEquipes;
        }
        #endregion

        #region [Equipes Especificas Totais - merge]
        private static bool MergeEquipesTotaisExistentes(
           OrgaoGestorInfo orgaoAlterado, OrgaoGestorEquipeEspecificaTotais orgaoGestorEquipeEspecificaTotais, IQueryable<EquipeEspecificaTotaisInfo> equipesExistentes)
        {
            bool hasChangeEquipesTotais = false;
            if (equipesExistentes.Count() > 0)
            {
                hasChangeEquipesTotais = DeletarEquipesTotaisRemovidasNaAlteracaoDoOrgaoExistente(orgaoAlterado, orgaoGestorEquipeEspecificaTotais, equipesExistentes);
            }
            hasChangeEquipesTotais = AdicionarAtualizarEquipesTotaisNovasNoOrgaoExistente(orgaoAlterado, orgaoGestorEquipeEspecificaTotais, equipesExistentes);
            return hasChangeEquipesTotais;
        }

        private static bool AdicionarAtualizarEquipesTotaisNovasNoOrgaoExistente(
              OrgaoGestorInfo orgaoAlterado, OrgaoGestorEquipeEspecificaTotais orgaoGestorEquipeEspecificaTotais, IQueryable<EquipeEspecificaTotaisInfo> equipesExistentesTotais)
        {
            bool hasChangeEquipesTotais = false;
            foreach (var equipeTotais in orgaoAlterado.EquipesEspecificasTotais)
            {
                equipeTotais.Id = equipesExistentesTotais.Where(equipeExistenteTotais => equipeExistenteTotais.IdOrgaoGestor == equipeTotais.IdOrgaoGestor 
                                                                                        && equipeTotais.Exercicio == equipeTotais.Exercicio)
                                                                                        .Select(l => l.Id).FirstOrDefault();
                equipeTotais.IdOrgaoGestor = orgaoAlterado.Id;
                if (equipeTotais.Id == 0)
                {
                    orgaoGestorEquipeEspecificaTotais.Add(equipeTotais, true);
                    hasChangeEquipesTotais = true;
                }
                else
                {
                    orgaoGestorEquipeEspecificaTotais.Update(equipeTotais, true);
                }
            }
            return hasChangeEquipesTotais;
        }

        private static bool DeletarEquipesTotaisRemovidasNaAlteracaoDoOrgaoExistente(
            OrgaoGestorInfo orgaoAlterado, OrgaoGestorEquipeEspecificaTotais equipeEspecificaTotais, IQueryable<EquipeEspecificaTotaisInfo> equipesExistentes)
        {
            var deletados = new List<EquipeEspecificaTotaisInfo>();
            bool hasChangeEquipesTotais = false;
            foreach (var equipeExistente in equipesExistentes)
            {
                if (!orgaoAlterado.EquipesEspecificas
                    .Any(equipeAlterada => (equipeAlterada.IdOrgaoGestor == equipeExistente.IdOrgaoGestor)
                                        && (equipeAlterada.Exercicio == equipeExistente.Exercicio))
                        )
                {
                    hasChangeEquipesTotais = true;
                    deletados.Add(equipeExistente);
                }
            }

            foreach (var e in deletados)
            {
                equipeEspecificaTotais.Delete(e, true);
            }
            return hasChangeEquipesTotais;
        }
        #endregion

        #region Equipe Especifica - Intencao de Estruturacao
        private static bool MergeEquipesIntencaoEstruturacao(
          OrgaoGestorInfo orgaoAlterado, OrgaoGestorIntencaoEstruturacaoEquipe orgaoGestorIntencaoEstruturacaoEquipe, IQueryable<IntencaoEstruturacaoEquipeInfo> intencaoEstruturacaoEquipeExistentes)
        {
            bool hasChangeIntencoesEstruturacao = false;
            if (intencaoEstruturacaoEquipeExistentes.Count() > 0)
            {
                hasChangeIntencoesEstruturacao = DeletarEquipesIntencoesRemovidasNaAlteracaoDoOrgaoExistente(orgaoAlterado
                                                                                                           , orgaoGestorIntencaoEstruturacaoEquipe
                                                                                                           , intencaoEstruturacaoEquipeExistentes);
            }
            hasChangeIntencoesEstruturacao = AdicionarAtualizarEquipesIntencoesNovasNoOrgaoExistente(orgaoAlterado, orgaoGestorIntencaoEstruturacaoEquipe, intencaoEstruturacaoEquipeExistentes);
            return hasChangeIntencoesEstruturacao;
        }


        private static bool AdicionarAtualizarEquipesIntencoesNovasNoOrgaoExistente(
              OrgaoGestorInfo orgaoAlterado
            , OrgaoGestorIntencaoEstruturacaoEquipe orgaoGestorIntencaoEstruturacaoEquipe
            , IQueryable<IntencaoEstruturacaoEquipeInfo> intencoesEstruturacaoEquipeInfo)
        {
            bool hasChangeEquipesTotais = false;
            foreach (var intencaoEstruturada in orgaoAlterado.IntencoesEstruturacaoEquipe.ToList())
            {
                intencaoEstruturada.Id = intencoesEstruturacaoEquipeInfo.Where(intencao => intencao.IdOrgaoGestor == intencao.IdOrgaoGestor
                                                                                        && intencao.Exercicio == intencao.Exercicio)
                                                                                        .Select(l => l.Id).FirstOrDefault();
                intencaoEstruturada.IdOrgaoGestor = orgaoAlterado.Id;
                if (intencaoEstruturada.Id == 0)
                {
                    orgaoGestorIntencaoEstruturacaoEquipe.Add(intencaoEstruturada, true);
                    hasChangeEquipesTotais = true;
                }
                else
                {
                    orgaoGestorIntencaoEstruturacaoEquipe.Update(intencaoEstruturada, true);
                }
            }
            return hasChangeEquipesTotais;
        }

        private static bool DeletarEquipesIntencoesRemovidasNaAlteracaoDoOrgaoExistente(
            OrgaoGestorInfo orgaoAlterado
            , OrgaoGestorIntencaoEstruturacaoEquipe orgaoGestorIntencaoEstruturacaoEquipe
            , IQueryable<IntencaoEstruturacaoEquipeInfo> intencoesEstruturacoes)
        {
            var deletados = new List<IntencaoEstruturacaoEquipeInfo>();
            bool hasChangeIntencoesEstruturacao = false;
            foreach (var intencaoEstruturacao in intencoesEstruturacoes.ToList())
            {
                if (!orgaoAlterado.IntencoesEstruturacaoEquipe
                    .Any(intencaoAlterada => (intencaoAlterada.IdOrgaoGestor == intencaoEstruturacao.IdOrgaoGestor)
                                        && (intencaoAlterada.Exercicio == intencaoEstruturacao.Exercicio))
                        )
                {
                    hasChangeIntencoesEstruturacao = true;
                    deletados.Add(intencaoEstruturacao);
                }
            }

            foreach (var e in deletados)
            {
                orgaoGestorIntencaoEstruturacaoEquipe.Delete(e, true);
            }
            return hasChangeIntencoesEstruturacao;
        }
        #endregion
        #endregion


    }
}
