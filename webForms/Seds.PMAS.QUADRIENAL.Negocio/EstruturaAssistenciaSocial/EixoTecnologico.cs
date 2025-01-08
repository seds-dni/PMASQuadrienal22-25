using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class EixoTecnologico
    {
        private static IRepository<EixoTecnologicoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<EixoTecnologicoInfo>>();
            }
        }

        public IQueryable<EixoTecnologicoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public EixoTecnologicoInfo GetById(int Id)
        {
            return _repository.Single(m => m.Id == Id);
        }
    }
}
