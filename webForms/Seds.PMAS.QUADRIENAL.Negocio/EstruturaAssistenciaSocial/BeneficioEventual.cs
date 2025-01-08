using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class BeneficioEventual
    {
        private static IRepository<BeneficioEventualInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<BeneficioEventualInfo>>();
            }
        }

        public IQueryable<BeneficioEventualInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<BeneficioEventualInfo> GetByTipoBeneficioEventual(Int32 idTipoBeneficioEventual)
        {
            return _repository.GetQuery().Where(b=> b.IdTipoBeneficioEventual == idTipoBeneficioEventual);
        }

        public BeneficioEventualInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
