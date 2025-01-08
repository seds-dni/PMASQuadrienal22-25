using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class TipoGestorMunicipal
    {
        private static IRepository<TipoGestorMunicipalInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TipoGestorMunicipalInfo>>();
            }
        }

        public IQueryable<TipoGestorMunicipalInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public TipoGestorMunicipalInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }
    }
}
