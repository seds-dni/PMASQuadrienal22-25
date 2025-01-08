using System;
using System.Linq;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class PETIIndicadores
    {
        private static IRepository<PETIIndicadoresInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PETIIndicadoresInfo>>();
            }
        }

        public IQueryable<PETIIndicadoresInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public PETIIndicadoresInfo GetByMunicipio(Int32 idMunicipio)
        {
            return _repository.Single(a => a.IdMunicipio == idMunicipio);
        }

        public void Update(PETIIndicadoresInfo obj, Boolean commit)
        {
            new ValidadorPETIIndicadores().Validar(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}