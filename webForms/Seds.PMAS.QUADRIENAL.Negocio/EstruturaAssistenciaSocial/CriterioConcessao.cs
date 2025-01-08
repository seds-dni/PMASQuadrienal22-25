using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class CriterioConcessao
    {
        private static IRepository<CriterioConcessaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CriterioConcessaoInfo>>();
            }
        }

        public IQueryable<CriterioConcessaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public CriterioConcessaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
