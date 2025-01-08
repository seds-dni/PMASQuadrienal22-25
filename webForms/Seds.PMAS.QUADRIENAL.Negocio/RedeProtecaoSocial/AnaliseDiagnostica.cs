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
using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AnaliseDiagnostica
    {
        private static IRepository<AnaliseDiagnosticaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AnaliseDiagnosticaInfo>>();
            }
        }

        private static IRepository<ConsultaAnaliseDiagnosticaInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaAnaliseDiagnosticaInfo>>();
            }
        }

        private static IRepository<ConsultaAnaliseDiagnosticaServicosInfo> _repositoryConsultaServicos
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaAnaliseDiagnosticaServicosInfo>>();
            }
        }

        public IQueryable<AnaliseDiagnosticaInfo> GetAll()
        {            
            return _repository.GetQuery();

        }

        public AnaliseDiagnosticaInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ConsultaAnaliseDiagnosticaInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<ConsultaAnaliseDiagnosticaServicosInfo> GetByPrefeituraServicos(int idPrefeitura)
        {
            return _repositoryConsultaServicos.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<ConsultaAnaliseDiagnosticaInfo> GetByMunicipio(int idMunicipio) 
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdMunicipio == idMunicipio);
        }

        public void Update(AnaliseDiagnosticaInfo obj, Boolean commit)
        {
            new ValidadorAnaliseDiagnostica().Validar(obj);
            if (_repository.GetQuery().Where(t => t.IdPrefeitura == obj.IdPrefeitura && t.Id != obj.Id).Any(t => t.IdSituacaoVulnerabilidade == obj.IdSituacaoVulnerabilidade))
                throw new Exception("Já existe cadastrado um item com esta situação de vulnerabilidade");
            if (obj.Demanda > new Prefeitura().GetAll().Where(t => t.Id == obj.IdPrefeitura).Select(t => t.Populacao).First())
                throw new Exception("A demanda estimada não pode ser maior que o número de habitantes do próprio município.");
            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity);
            if (propriedades.Count > 0)
            {
                var sv = new SituacaoVulnerabilidade().GetById(obj.IdSituacaoVulnerabilidade);
                String descricao = "Situação de Vulnerabilidade: " + sv.Nome + "." + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 15, descricao, obj.Id);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(AnaliseDiagnosticaInfo obj, Boolean commit)
        {
            new ValidadorAnaliseDiagnostica().Validar(obj);
            if (_repository.GetQuery().Where(t => t.IdPrefeitura == obj.IdPrefeitura && t.IdExercicio == obj.IdExercicio).Any(t => t.IdSituacaoVulnerabilidade == obj.IdSituacaoVulnerabilidade))
                throw new Exception("Já existe cadastrado um item com esta situação de vulnerabilidade");
            if(obj.Demanda > new Prefeitura().GetAll().Where(t=> t.Id == obj.IdPrefeitura).Select(t=> t.Populacao).First())
                throw new Exception("A demanda estimada não pode ser maior que o número de habitantes do próprio município.");
            _repository.Add(obj);

            var sv = new SituacaoVulnerabilidade().GetById(obj.IdSituacaoVulnerabilidade);
            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 15, "Incluída a Situação de Vulnerabilidade " + sv.Nome + ".");
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(AnaliseDiagnosticaInfo obj, Boolean commit)
        {
            var sv = new SituacaoVulnerabilidade().GetById(obj.IdSituacaoVulnerabilidade);
            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Remove, 15, "Excluída a Situação de Vulnerabilidade " + sv.Nome + ".");
            
            _repository.Delete(obj);

            if (log != null)
                new Log().Add(log, false);

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
                    case "Classificacao": labels.Add("classificação"); break;
                    case "Demanda": labels.Add("demanda estimada para 2013"); break;                    
                }
            }
            return labels.Distinct().ToList();
        }

         
        public ConsultaMunicipioIndicadoresInfo GetMunicipioIndicadoresByMunicipio(Int32 idMunicipio)
        {
            return (ContextManager.GetContext() as PMASContext).GetMunicipioIndicadoresByMunicipio(idMunicipio);
        }

        //Welington P.

        public List<ConsultaAnaliseDiagnosticaPrefeituraExercicioInfo> GetAnaliseDiagnosticaPrefeituraExercicio(Int32 idPrefeitura,Int32 Exercicio)
        {
            return (ContextManager.GetContext() as PMASContext).GetAnaliseDiagnosticaPrefeituraExercicio(idPrefeitura,Exercicio);
        }

        public ConsultaDemografiaTerritorioIndicadoresInfo GetDemografiaIndicadoresByMunicipio(Int32 idMunicipio, Int32 versaoSistema) 
        {
            return (ContextManager.GetContext() as PMASContext).GetDemografiaIndicadoresByMunicipio(idMunicipio, versaoSistema);
        }
        public ConsultaMunicipioPopulacaoVulnerabilidadeIndicadoresInfo GetPopulacaoVulnerabilidadeByMunicipio(Int32 idMunicipio, Int32 versaoSistema) 
        {
            return (ContextManager.GetContext() as PMASContext).GetPopulacaoVulnerabilidadeByMunicipio(idMunicipio, versaoSistema);
        }

        public ConsultaMunicipioRedeSocioAssistencialIndicadoresInfo GetIndicadoresRedeSocioAssistencial(Int32 idMunicipio, Int32 versaoSistema) 
        {
            return (ContextManager.GetContext() as PMASContext).GetIndicadoresRedeSocioAssistencial(idMunicipio, versaoSistema);
        }
    }
}
