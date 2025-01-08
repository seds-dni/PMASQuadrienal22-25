using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class CREASMunicipio
    {
        private static IRepository<CREASMunicipioInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CREASMunicipioInfo>>();
            }
        }

        private static IRepository<TipoAtendimentoInfo> _repositoryAtendimento
        {
            get {
                return ObjectFactory.GetInstance<IRepository<TipoAtendimentoInfo>>();
            }
        }

        public TipoAtendimentoInfo GetTipoAtendimentoById(int id) 
        {
            return _repositoryAtendimento.Single(m => m.Id == id);

        }

        public IQueryable<CREASMunicipioInfo> GetAll()
        {
            return _repository.GetQuery();
        }



        public IQueryable<CREASMunicipioInfo> GetByCREAS(int idCreas)
        {
            return _repository.GetObjectSet().Include("TipoAtendimento").Where(m => m.IdCREAS == idCreas);
        }

        public void Update(CREASMunicipioInfo obj, Boolean commit)
        {
            //new ValidadorTransferenciaRendaParceria().Validar(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(CREASMunicipioInfo obj, Boolean commit)
        {
            //new ValidadorTransferenciaRendaParceria().Validar(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(CREASMunicipioInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        } 
    }
}
