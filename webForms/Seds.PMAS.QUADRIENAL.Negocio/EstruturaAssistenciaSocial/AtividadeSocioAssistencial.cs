using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AtividadeSocioAssistencial
    {
        private static IRepository<AtividadeSocioAssistencialInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AtividadeSocioAssistencialInfo>>();
            }
        }

        public IQueryable<AtividadeSocioAssistencialInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<AtividadeSocioAssistencialInfo> GetByTipoServico(Int32 idTipoServico)
        {
            return _repository.GetQuery().Where(a=> a.TiposServicos.Any(t=> t.Id == idTipoServico));
        }

        public AtividadeSocioAssistencialInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }  
    }
}
