using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Conselhos
    {
        private static IRepository<ConselhosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConselhosInfo>>();
            }
        }

        public IQueryable<ConselhosInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ConselhosInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
