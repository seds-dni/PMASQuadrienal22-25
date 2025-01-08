using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class TipoBeneficioEventual
    {
        private static IRepository<TipoBeneficioEventualInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TipoBeneficioEventualInfo>>();
            }
        }

        public IQueryable<TipoBeneficioEventualInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public TipoBeneficioEventualInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
