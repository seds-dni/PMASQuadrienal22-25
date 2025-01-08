using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial
{
    public class PrefeituraDemandaAtendimento
    {
        private static IRepository<PrefeituraDemandaAtendimentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrefeituraDemandaAtendimentoInfo>>();
            }
        }
        public IQueryable<PrefeituraDemandaAtendimentoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public PrefeituraDemandaAtendimentoInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.IdDemandaAtendimento == id);
        }

        public IQueryable<PrefeituraDemandaAtendimentoInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Add(PrefeituraDemandaAtendimentoInfo previsao, Boolean commit)
        {
            _repository.Add(previsao);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(PrefeituraDemandaAtendimentoInfo previsao, Boolean commit)
        {
            _repository.Update(previsao);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(PrefeituraDemandaAtendimentoInfo previsao, Boolean commit)
        {
            _repository.Delete(previsao);
            if (commit)
                ContextManager.Commit();
        }
    }
}
