using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Prefeitura
    {
        private static IRepository<PrefeituraInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrefeituraInfo>>();
            }
        }

        public IQueryable<PrefeituraInfo> GetAll()
        {
            return _repository.GetObjectSet()
                                .Include("Situacao")
                                .Include("PrefeituraAtualizacoesAnuais")
                                .Include("PrefeiturasExerciciosBloqueio");
        }

        public List<ConsultaFluxoInfo> GetConsultaFluxo(List<Int32> idsMunicipios)
        {
            List<PrefeituraInfo> prefeitura = _repository.GetObjectSet().Include("Situacao").ToList();

            if (idsMunicipios == null || idsMunicipios.Count == 0)
            {
                List<ConsultaFluxoInfo> consultas = prefeitura.Select(c =>
                    new ConsultaFluxoInfo()
                    {
                        IdMunicipio = c.IdMunicipio
                        ,
                        IdPrefeitura = c.Id
                        ,
                        Situacao = c.Situacao
                        ,
                        DesbloquearValoresDrads = c.DesbloquearValoresDrads
                        ,
                        ReprogramarValores = c.ValoresReprogramadosDrads
                    }).ToList();

                return consultas;
            }
            else
            {
                List<ConsultaFluxoInfo> consultas = prefeitura
                    .Where(c => idsMunicipios.Contains(c.IdMunicipio)).
                    Select(c => 
                        new ConsultaFluxoInfo() { 
                            IdMunicipio = c.IdMunicipio
                            , IdPrefeitura = c.Id
                            , Situacao = c.Situacao
                            , DesbloquearValoresDrads = c.DesbloquearValoresDrads
                            , ReprogramarValores = c.ValoresReprogramadosDrads 
                        }).ToList();

                return consultas;
            }
        }

        public PrefeituraInfo GetById(int id)
        {
            return _repository.GetObjectSet()
                .Include("NivelGestao")
                .Include("Situacao")
                .Include("PrefeituraAtualizacoesAnuais")
                .Include("PrefeiturasExerciciosBloqueio")
                .Single(m => m.Id == id);
        }

        public Int32 GetRevisaoByPrefeitura(int id)
        {
            return _repository.GetQuery().Where(t => t.Id == id).Select(t => t.Revisao).FirstOrDefault();
        }

        public PrefeituraInfo GetByMunicipio(int idMunicipio)
        {

            return _repository.GetObjectSet()
               .Include("NivelGestao")
               .Include("Situacao")
               .Include("PrefeituraAtualizacoesAnuais")
               .Include("PrefeiturasExerciciosBloqueio")
               .Single(m => m.IdMunicipio == idMunicipio);
        }

        public PrefeituraInfo GetByMunicipioQuadrosFinanceiros(int idMunicipio)
        {

            PrefeituraInfo prefeitura = _repository.GetObjectSet()
               .Include("NivelGestao")
               .Include("Situacao")
               .Include("PrefeituraAtualizacoesAnuais")
               .Include("PrefeiturasExerciciosBloqueio")
               .Single(m => m.IdMunicipio == idMunicipio);
            //

               //.Include("PrefeituraSituacoesQuadros").ToList<PrefeituraInfo>();
            return prefeitura;
        }


        public void Update(PrefeituraInfo prefeitura, Boolean commit, bool validar = true)
        {
            if (validar)
            {
                new ValidadorPrefeitura().Validar(prefeitura);
            }

            _repository.Update(prefeitura);

            var propriedadesEntity = _repository.GetModifiedProperties(prefeitura);
            var propriedades = GetLabelForInfo(propriedadesEntity);
            if (propriedades.Count > 0)
            {
                String descricao = String.Empty;

                var log = new LogInfo();

                var quadro = propriedadesEntity;

                if (quadro.Any(t => t == "Caracterizacao"))
                {
                    log = Log.CreateLog(prefeitura.Id, EAcao.Update, 14, "Alterada a descrição Território e demografia.");

                }
                else if (quadro.Any(t => t == "CaracterizacaoPopulacao"))
                {
                    log = Log.CreateLog(prefeitura.Id, EAcao.Update, 92, "Alterada a descrição População e Vulnerabilidade Social.");
                }

                else if (quadro.Any(t => t == "CaracterizacaoRedeSocioassistencial"))
                {
                    log = Log.CreateLog(prefeitura.Id, EAcao.Update, 93, "Alterada a descrição da Evolução da rede de atendimento.");
                }
                else if (quadro.Any(t => t == "CaracterizacaoAnaliseInterpretacao"))
                {
                    log = Log.CreateLog(prefeitura.Id, EAcao.Update, 15, "Alterada a descrição da Análise e interpretação.");
                }
                else if (quadro.Any(t => t == "JustificativaAcaoPlanejamento"))
                {
                    log = Log.CreateLog(prefeitura.Id, EAcao.Update, 60, "Justificativa das Ações Planejadas");
                }
                else
                {
                    descricao = Log.CreateDescricaoDefaultUpdate(propriedades);
                    log = Log.CreateLog(prefeitura.Id, EAcao.Update, 1, descricao);
                }

                if (log != null)
                    new Log().Add(log, false);

                //var quadro = propriedadesEntity.Any(t => t == "Caracterizacao") ? 87 : 1;
                //if (quadro == 1)
                //    descricao = Log.CreateDescricaoDefaultUpdate(propriedades);
                //else
                //    descricao = "Alterada a descrição diagnóstica do município.";

                //var log = Log.CreateLog(pre.Id, EAcao.Update, quadro, descricao);


            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(PrefeituraInfo pre, Boolean commit)
        {
            new ValidadorPrefeitura().Validar(pre);
            _repository.Add(pre);
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
                    case "IdNivelGestao": labels.Add("gestão"); break;
                    case "DataPublicacao": labels.Add("data da última publicação de nível de gestão no DOE"); break;
                    case "CEP": labels.Add("CEP"); break;
                    case "Logradouro": labels.Add("logradouro"); break;
                    case "Numero": labels.Add("número"); break;
                    case "Complemento": labels.Add("complemento"); break;
                    case "Cidade": labels.Add("cidade"); break;
                    case "Bairro": labels.Add("bairro"); break;
                    case "Telefone": labels.Add("telefone"); break;
                    case "Celular": labels.Add("Celular"); break;
                    case "WebSite": labels.Add("web site"); break;
                    case "Email": labels.Add("e-mail institucional"); break;
                    case "Caracterizacao": labels.Add("caracterização"); break;
                    case "CaracterizacaoAnaliseInterpretacao": labels.Add("caracterizacao Analise Interpretacao"); break;
                    case "CaracterizacaoPopulacao": labels.Add("caracterizacao populacao"); break;
                    case "CaracterizacaoRedeSocioassistencial": labels.Add("caracterizacao Rede Socioassistencial"); break;
                    case "JustificativaAcaoPlanejamento": labels.Add("justificativa de ações planejadas"); break;
                }
            }
            return labels.Distinct().ToList();
        }


        public List<MotivoNaoInstalacaoInfo> ListMotivosDeNaoInstalacaoDeCRAS(Int32 idPrefeitura)
        {
            return _repository.GetQuery().Where(t => t.Id == idPrefeitura).SelectMany(t => t.MotivosNaoInstalacaoCRAS).ToList();
        }

        public List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCREASByPrefeitura(Int32 idPrefeitura)
        {
            return _repository.GetQuery().Where(t => t.Id == idPrefeitura).SelectMany(t => t.MotivosNaoInstalacaoCREAS).ToList();
        }

        public List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCentroPOPByPrefeitura(Int32 idPrefeitura)
        {
            return _repository.GetQuery().Where(t => t.Id == idPrefeitura).SelectMany(t => t.MotivosNaoInstalacaoCentroPOP).ToList();
        }

        public void SaveMotivosNaoInstalacaoCRAS(List<MotivoNaoInstalacaoInfo> lstMotivos, Int32 idPrefeitura, bool commit)
        {
            var idsMotivos = lstMotivos.Select(s => s.Id).ToList();
            var lstNew = new MotivoNaoInstalacao().GetAll().Where(s => idsMotivos.Contains(s.Id)).ToList();
            _repository.UpdateNN<MotivoNaoInstalacaoInfo>(GetById(idPrefeitura), lstNew, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.MotivosNaoInstalacaoCRAS);


            if (commit)
                ContextManager.Commit();
        }

        public void SaveMotivosNaoInstalacaoCREAS(List<MotivoNaoInstalacaoInfo> lstMotivos, Int32 idPrefeitura, bool commit)
        {
            var idsMotivos = lstMotivos.Select(s => s.Id).ToList();
            var lstNew = new MotivoNaoInstalacao().GetAll().Where(s => idsMotivos.Contains(s.Id)).ToList();
            _repository.UpdateNN<MotivoNaoInstalacaoInfo>(GetById(idPrefeitura), lstNew, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.MotivosNaoInstalacaoCREAS);
            if (commit)
                ContextManager.Commit();
        }

        public void SaveMotivosNaoInstalacaoCentroPOP(List<MotivoNaoInstalacaoInfo> lstMotivos, Int32 idPrefeitura, bool commit)
        {
            var idsMotivos = lstMotivos.Select(s => s.Id).ToList();
            var lstNew = new MotivoNaoInstalacao().GetAll().Where(s => idsMotivos.Contains(s.Id)).ToList();
            _repository.UpdateNN<MotivoNaoInstalacaoInfo>(GetById(idPrefeitura), lstNew, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.MotivosNaoInstalacaoCentroPOP);
            if (commit)
                ContextManager.Commit();
        }


        private static IRepository<PrefeituraSituacaoQuadroInfo> _repositoryPrefeituraSituacaoQuadro
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrefeituraSituacaoQuadroInfo>>();
            }
        }


        public List<PrefeituraSituacaoQuadroInfo> GetPrefeituraSituacaoQuadro(Int32 idPrefeitura, Int32 idRecurso)
        {
            return _repositoryPrefeituraSituacaoQuadro.Find(p => p.IdPrefeitura == idPrefeitura && p.IdRecurso == idRecurso && p.Exercicio >= 2021).ToList();
        }


        public void SavePrefeituraSituacaoQuadro(PrefeituraSituacaoQuadroInfo quadro, Boolean commit)
        {
            if (!_repositoryPrefeituraSituacaoQuadro.GetAll().Any(p => p.IdPrefeitura == quadro.IdPrefeitura && p.IdRecurso == quadro.IdRecurso))
                _repositoryPrefeituraSituacaoQuadro.Add(quadro);
            else
                _repositoryPrefeituraSituacaoQuadro.Update(quadro);
            if (commit)
                ContextManager.Commit();
        }



        public void SavePrefeiturasSituacoesQuadros(int idRecurso, int idSituacao, int exercicio)
        {
            (ContextManager.GetContext() as PMASContext).SavePrefeiturasSituacoesQuadros(idRecurso, idSituacao, exercicio);
        }


        public void SavePrefeiturasSituacoesQuadrosEFLO(int idRecurso, int idSituacao, int exercicio)
        {
            (ContextManager.GetContext() as PMASContext).SavePrefeiturasSituacoesQuadrosEFLO(idRecurso, idSituacao, exercicio);
        }
    }
}
