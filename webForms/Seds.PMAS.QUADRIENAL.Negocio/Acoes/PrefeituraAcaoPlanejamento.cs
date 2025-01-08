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
using Seds.PMAS.QUADRIENAL.Entidades.Acoes;

namespace Seds.PMAS.QUADRIENAL.Negocio
{

    public class PrefeituraAcaoPlanejamento
    {
        public static IRepository<ConsultaIntencaoAcaoServicoSocioassistencialInfo> _repositoryConsultaServico
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaIntencaoAcaoServicoSocioassistencialInfo>>();
            }
        }

        private static IRepository<ConsultaIntencaoAcaoInfo> _repositoryConsultaIntencaoAcao
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaIntencaoAcaoInfo>>();
            }
        }

        private static IRepository<PrefeituraAcaoPlanejamentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrefeituraAcaoPlanejamentoInfo>>();
            }
        }

        private static IRepository<ConsultaPrefeituraAcaoPlanejamentoInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaPrefeituraAcaoPlanejamentoInfo>>();
            }
        }

        public IQueryable<PrefeituraAcaoPlanejamentoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public PrefeituraAcaoPlanejamentoInfo GetById(int id)
        {
            var p = _repository.GetObjectSet().Include("AcaoPlanejamento").Single(m => m.Id == id);
            return p;
        }

        public IQueryable<PrefeituraAcaoPlanejamentoInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("AcaoPlanejamento").Include("AcaoPlanejamento.EixoAcaoPlanejamento").Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<ConsultaPrefeituraAcaoPlanejamentoInfo> GetConsultaByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<ConsultaIntencaoAcaoInfo> GetByIntencaoAcaoPrefeitura(int idPrefeitura)
        {
            return _repositoryConsultaIntencaoAcao.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        private static IRepository<RecursosPrefeituraAcaoPlanejamentoInfo> _repositoryRecursos
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<RecursosPrefeituraAcaoPlanejamentoInfo>>();
            }
        }

        public IQueryable<ConsultaIntencaoAcaoServicoSocioassistencialInfo> GetByIntencaoServicoPrefeitura(int idPrefeitura) 
        {
            return _repositoryConsultaServico.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.IdAvaliacaoServico != 1);
        }

        public void Update(PrefeituraAcaoPlanejamentoInfo obj, Boolean commit)
        {
            new ValidadorPrefeituraAcaoPlanejamento().Validar(obj);

            _repository.Update(obj);

            //QUADRO 61
            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity, obj);
            if (propriedades.Count > 0)
            {
                String descricao = "Ação: " + obj.Nome + "." + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 61, descricao, obj.Id);
                if (log != null)
                    new Log().Add(log, false);

            }
            if (commit)
                ContextManager.Commit();
        }

        public void Add(PrefeituraAcaoPlanejamentoInfo obj, Boolean commit)
        {
            new ValidadorPrefeituraAcaoPlanejamento().Validar(obj);

            _repository.Add(obj);

            String descricao = "Incluída a Ação " + obj.Nome + ".";
            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 61, descricao);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(PrefeituraAcaoPlanejamentoInfo obj, Boolean commit)
        {
            String descricao = "Excluída a Ação " + obj.Nome + ".";
            DeleteRecurso(obj.Id, obj.IdPrefeitura, true);
           
                
            _repository.Delete(obj);

            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Remove, 61, descricao);
            if (log != null)
                new Log().Add(log, false);
            if (commit)
                ContextManager.Commit();
        }

        public List<String> GetLabelForInfo(List<String> propriedades, PrefeituraAcaoPlanejamentoInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "IdAcaoPlanejamento": labels.Add("identificação da ação"); break;
                    case "Nome": labels.Add("nome"); break;
                    case "OutrosEnvolvidos": labels.Add("outros envolvidos na realização da ação"); break;
                    case "Objetivos": labels.Add("objetivos a serem alcançados"); break;
                    case "Descricao": labels.Add("descrição"); break;
                    case "FonteFMAS":
                    case "FonteOrcamentoMunicipal":
                    case "FonteOutrosFundosMunicipais":
                    case "FonteFEAS":
                    case "FonteOrcamentoEstadual":
                    case "FonteOutrosFundosEstaduais":
                    case "FonteFNAS":
                    case "FonteOrcamentoFederal":
                    case "FonteOutrosFundosFederais":
                    case "FonteIGDPBF":
                    case "FonteIGDSUAS":
                        labels.Add("fonte(s) dos recursos financeiros"); break;
                    case "ValorFMAS": if (obj.FonteFMAS) labels.Add("valor dos recursos financeiros do FMAS"); break;
                    case "ValorOrcamentoMunicipal": if (obj.FonteOrcamentoMunicipal) labels.Add("valor dos recursos financeiros do Orçamento Municipal"); break;
                    case "ValorOutrosFundosMunicipais": if (obj.FonteOutrosFundosMunicipais) labels.Add("valor dos recursos financeiros de Outros Fundos Municipais"); break;
                    case "ValorFEAS": if (obj.FonteFEAS) labels.Add("valor dos recursos financeiros do FEAS"); break;
                    case "ValorOrcamentoEstadual": if (obj.FonteOrcamentoEstadual) labels.Add("valor dos recursos financeiros do Orçamento Estadual"); break;
                    case "ValorOutrosFundosEstaduais": if (obj.FonteOutrosFundosEstaduais) labels.Add("valor dos recursos financeiros de Outros Fundos Estaduais"); break;
                    case "ValorFNAS": if (obj.FonteFNAS) labels.Add("valor dos recursos financeiros do FNAS"); break;
                    case "ValorOrcamentoFederal": if (obj.FonteOrcamentoFederal) labels.Add("valor dos recursos financeiros do Orçamento Federal"); break;
                    case "ValorOutrosFundosFederais": if (obj.FonteOutrosFundosFederais) labels.Add("valor dos recursos financeiros de Outros Fundos Federais"); break;
                    case "ValorIGDPBF": if (obj.FonteIGDPBF) labels.Add("valor dos recursos financeiros do IGD-PBF"); break;
                    case "ValorIGDSUAS": if (obj.FonteIGDSUAS) labels.Add("valor dos recursos financeiros do IGD-SUAS"); break;
                     
                    case "ValorEstimativaCusto": labels.Add("valor de estimativa de custo total da ação"); break;
                    case "MesPrevistoInicio":
                    case "AnoPrevistoInicio":
                        labels.Add("data de início da realização da ação"); break;
                    case "MesPrevistoTermino":
                    case "AnoPrevistoTermino":
                        labels.Add("data de término da realização da ação"); break;
                }
            }
            return labels.Distinct().ToList();
        }

        public IQueryable<RecursosPrefeituraAcaoPlanejamentoInfo> GetRecursosById(int idAcaoPlanejamento, int idPrefeitura)
        {
            var p = _repositoryRecursos.GetQuery().Where(s => s.IdPrefeituraAcaoPlanejamento == idAcaoPlanejamento && s.IdPrefeitura == idPrefeitura);
            return p;
        }


        public void SaveRecursosPrefeituraAcaoPlanejamentoInfo(RecursosPrefeituraAcaoPlanejamentoInfo r, Boolean commit)
        {
            if (!_repositoryRecursos.GetQuery().Any(s => s.IdPrefeituraAcaoPlanejamento == r.IdPrefeituraAcaoPlanejamento && s.IdPrefeitura == r.IdPrefeitura && s.Exercicio == r.Exercicio))
            {
                _repositoryRecursos.Add(
                     new RecursosPrefeituraAcaoPlanejamentoInfo
                     {
                         IdPrefeitura = r.IdPrefeitura,
                         IdPrefeituraAcaoPlanejamento = r.IdPrefeituraAcaoPlanejamento,
                         FonteFMAS = r.FonteFMAS,
                         ValorFMAS = r.ValorFMAS,
                         FonteOrcamentoMunicipal = r.FonteOrcamentoMunicipal,
                         ValorOrcamentoMunicipal = r.ValorOrcamentoMunicipal,
                         FonteOutrosFundosMunicipais = r.FonteOutrosFundosMunicipais,
                         ValorOutrosFundosMunicipais = r.ValorOutrosFundosMunicipais,
                         FonteFEAS = r.FonteFEAS,
                         ValorFEAS = r.ValorFEAS,
                         FonteFEASReprogramado = r.FonteFEASReprogramado,
                         ValorFEASReprogramado = r.ValorFEASReprogramado,
                         FonteOrcamentoEstadual = r.FonteOrcamentoEstadual,
                         ValorOrcamentoEstadual = r.ValorOrcamentoEstadual,
                         FonteOutrosFundosEstaduais = r.FonteOutrosFundosEstaduais,
                         ValorOutrosFundosEstaduais = r.ValorOutrosFundosEstaduais,
                         FonteFNAS = r.FonteFNAS,
                         ValorFNAS = r.ValorFNAS,
                         FonteOrcamentoFederal = r.FonteOrcamentoFederal,
                         ValorOrcamentoFederal = r.ValorOrcamentoFederal,
                         FonteOutrosFundosFederais = r.FonteOutrosFundosFederais,
                         ValorOutrosFundosFederais = r.ValorOutrosFundosFederais,
                         FonteIGDPBF = r.FonteIGDPBF,
                         ValorIGDPBF = r.ValorIGDPBF,
                         FonteIGDSUAS = r.FonteIGDSUAS,
                         ValorIGDSUAS = r.ValorIGDSUAS,
                         Exercicio = r.Exercicio,

                     });
            }
            else
            {
                var obj = _repositoryRecursos.GetQuery().Where(s => s.IdPrefeituraAcaoPlanejamento == r.IdPrefeituraAcaoPlanejamento && s.IdPrefeitura == r.IdPrefeitura && s.Exercicio == r.Exercicio);

                r.Id = obj.FirstOrDefault().Id;

                _repositoryRecursos.Update(r);
            }




            if (commit)
                ContextManager.Commit();
        }

       public void DeleteRecurso(int idPrefeituraAcaoPlanejamento, int idPrefeitura, Boolean commit)
        {
            String descricao = "";

            List<RecursosPrefeituraAcaoPlanejamentoInfo> obj;

            obj = _repositoryRecursos.GetQuery().Where(s => s.IdPrefeituraAcaoPlanejamento == idPrefeituraAcaoPlanejamento && s.IdPrefeitura == idPrefeitura).ToList();

            if (obj == null)
                return;

            foreach (var r in obj)
            {

                _repositoryRecursos.Delete(r);           

            }
           /* var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Remove, 61, descricao);
             if (log != null)
                 new Log().Add(log, false);*/

            if (commit)
                ContextManager.Commit();
        }

    }
}
