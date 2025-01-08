using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Cargo
    {
        private static IRepository<CargoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CargoInfo>>();
            }
        }

        public IQueryable<CargoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public CargoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
