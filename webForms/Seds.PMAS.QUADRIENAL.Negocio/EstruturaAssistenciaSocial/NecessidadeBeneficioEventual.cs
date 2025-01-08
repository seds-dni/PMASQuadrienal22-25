using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class NecessidadeBeneficioEventual
    {
        private static IRepository<NecessidadeBeneficioEventualInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<NecessidadeBeneficioEventualInfo>>();
            }
        }

        public IQueryable<NecessidadeBeneficioEventualInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<NecessidadeBeneficioEventualInfo> GetByTipoBeneficioEventual(Int32 idTipoBeneficioEventual)
        {
            return _repository.GetQuery().Where(b=> b.IdTipoBeneficioEventual == idTipoBeneficioEventual);
        }

        public NecessidadeBeneficioEventualInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
