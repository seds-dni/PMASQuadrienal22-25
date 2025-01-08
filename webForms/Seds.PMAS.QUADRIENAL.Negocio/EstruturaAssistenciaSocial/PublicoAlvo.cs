using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class PublicoAlvo
    {
        private static IRepository<PublicoAlvoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PublicoAlvoInfo>>();
            }
        }

        public IQueryable<PublicoAlvoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

       
        public PublicoAlvoInfo GetById(int id)
        {
            return _repository.GetQuery().SingleOrDefault(m => m.Id == id);
        }
    }
}
