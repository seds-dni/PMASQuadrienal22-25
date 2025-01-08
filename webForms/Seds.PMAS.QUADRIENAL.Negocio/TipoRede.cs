using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class TipoRede
    {
        private static IRepository<TipoRedeInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TipoRedeInfo>>();
            }
        }

        public IQueryable<TipoRedeInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public TipoRedeInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
