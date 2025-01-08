using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class BaseDados
    {
        private static IRepository<BaseDadosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<BaseDadosInfo>>();
            }
        }

        public IQueryable<BaseDadosInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public BaseDadosInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
