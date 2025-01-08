using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AbrangenciaTerritorial
    {
        private static IRepository<AbrangenciaTerritorialInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AbrangenciaTerritorialInfo>>();
            }
        }

        public IQueryable<AbrangenciaTerritorialInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public AbrangenciaTerritorialInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
