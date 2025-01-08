using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Periodicidade
    {
        private static IRepository<PeriodicidadeInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PeriodicidadeInfo>>();
            }
        }

        public IQueryable<PeriodicidadeInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public PeriodicidadeInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
