using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class IndiceGestaoDescentralizada
    {
        private static IRepository<IndiceGestaoDescentralizadaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<IndiceGestaoDescentralizadaInfo>>();
            }
        }

        public IQueryable<IndiceGestaoDescentralizadaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public IndiceGestaoDescentralizadaInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public IndiceGestaoDescentralizadaInfo GetByPrefeitura(int idPrefeitura, int exercicio)
        {
            return _repository.Single(m => m.IdPrefeitura == idPrefeitura && m.Exercicio == exercicio);
        }

        public IndiceGestaoDescentralizadaInfo GetByPrefeituraByExercicio(int idPrefeitura, int exercicio)
        {
            return _repository.Single(m => m.IdPrefeitura == idPrefeitura && m.Exercicio == exercicio);
        }
        
        
        public IndiceGestaoDescentralizadaInfo Get2016ByPrefeitura(int idPrefeitura)
        {
            return (ContextManager.GetContext() as PMASContext).GetIndiceGestaoDescentralizada2016ByPrefeitura(idPrefeitura);
        }
        public void Add(IndiceGestaoDescentralizadaInfo obj, Boolean commit)
        {
            _repository.Add(obj);

            if (commit)
                ContextManager.Commit();
        }
        public void Update(IndiceGestaoDescentralizadaInfo obj, Boolean commit)
        {
            //new ValidadorIndiceGestaoDescentralizada().Validar(obj);
            _repository.Update(obj);
            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity, obj);
            if (propriedadesEntity.Count > 0)
            {
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 9, Log.CreateDescricaoDefaultUpdate(propriedades));
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }


        public List<String> GetLabelForInfo(List<String> propriedades, IndiceGestaoDescentralizadaInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "IGDPBFValorMensal": labels.Add("Índice de Gestão Descentralizada do Programa Bolsa Família (IGD-PBF) valor mensal do Recurso:"); break;
                    case "IGDSUASValorMensal": labels.Add("Índice de Gestão Descentralizada do Sistema Único de Assistência Social (IGD-SUAS) valor mensal do Recurso:"); break;
                    case "ComentariosExecucaoFinanceira": labels.Add("Comentários do Órgão Gestor:"); break;
                }
            }
            return labels.Distinct().ToList();
        }
    }
}
