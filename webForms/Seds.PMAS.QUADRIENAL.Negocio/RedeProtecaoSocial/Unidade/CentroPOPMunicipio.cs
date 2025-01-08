using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class CentroPOPMunicipio
    {
        private static IRepository<CentroPOPMunicipioInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CentroPOPMunicipioInfo>>();
            }
        }

        public IQueryable<CentroPOPMunicipioInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public CentroPOPMunicipioInfo GetById(int id) 
        {
            return _repository.GetQuery().Where(m => m.Id == id).SingleOrDefault();
        }

        public IQueryable<CentroPOPMunicipioInfo> GetByCentroPop(int idCentroPop)
        {
            return _repository.GetObjectSet().Include("TipoAtendimento").Where(m => m.IdCentroPop == idCentroPop);
        }

        public void Update(CentroPOPMunicipioInfo obj, Boolean commit)
        {
            //new ValidadorTransferenciaRendaParceria().Validar(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }
        public void Add(CentroPOPMunicipioInfo obj, Boolean commit)
        {
            Add(obj, commit, false);
        }

        public void Add(CentroPOPMunicipioInfo obj, Boolean commit, Boolean validar)
        {
            //new ValidadorTransferenciaRendaParceria().Validar(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(CentroPOPMunicipioInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
