using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class TipoDemandaAtendimento
    {
        private static IRepository<TipoDemandaAtendimentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TipoDemandaAtendimentoInfo>>();
            }
        }

        public IQueryable<TipoDemandaAtendimentoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public TipoDemandaAtendimentoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        

    }
}
