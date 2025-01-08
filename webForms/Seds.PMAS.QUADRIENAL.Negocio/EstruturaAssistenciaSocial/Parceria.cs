using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using StructureMap;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Parceria
    {
        private static IRepository<ParceriaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ParceriaInfo>>();
            }
        }

        public IQueryable<ParceriaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ParceriaInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
