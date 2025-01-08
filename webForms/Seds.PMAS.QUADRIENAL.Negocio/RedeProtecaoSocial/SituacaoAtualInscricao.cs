using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class SituacaoAtualInscricao
    {
        private static IRepository<SituacaoAtualInscricaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<SituacaoAtualInscricaoInfo>>();
            }
        }

        public IQueryable<SituacaoAtualInscricaoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public SituacaoAtualInscricaoInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("Nome").Single(m => m.Id == id);
        }
    }
}
