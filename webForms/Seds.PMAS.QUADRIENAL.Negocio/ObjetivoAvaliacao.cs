using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ObjetivoAvaliacao
    {
        private static IRepository<ObjetivoAvaliacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ObjetivoAvaliacaoInfo>>();
            }
        }

        public IQueryable<ObjetivoAvaliacaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ObjetivoAvaliacaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }
    }
}
