using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using StructureMap;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class TipoParceria
    {
        private static IRepository<TipoParceriaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TipoParceriaInfo>>();
            }
        }

        public IQueryable<TipoParceriaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public TipoParceriaInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
