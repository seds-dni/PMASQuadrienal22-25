using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AbrangenciaServico
    {
        private static IRepository<AbrangenciaServicoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AbrangenciaServicoInfo>>();
            }
        }

        public IQueryable<AbrangenciaServicoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public AbrangenciaServicoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
