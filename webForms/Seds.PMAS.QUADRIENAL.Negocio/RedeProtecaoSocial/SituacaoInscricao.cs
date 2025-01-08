using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;

namespace Seds.PMAS.QUADRIENAL.Negocio
{

    public class SituacaoInscricao
    {
        private static IRepository<SituacaoInscricaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<SituacaoInscricaoInfo>>();
            }
        }

        public IQueryable<SituacaoInscricaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public SituacaoInscricaoInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("Nome").Single(m => m.Id == id);
        }
    }
}
