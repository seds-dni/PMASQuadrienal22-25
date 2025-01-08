using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class TipoTransferenciaRenda
    {
        private static IRepository<TipoTransferenciaRendaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TipoTransferenciaRendaInfo>>();
            }
        }

        public IQueryable<TipoTransferenciaRendaInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public TipoTransferenciaRendaInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
