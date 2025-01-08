using System.Linq;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class MotivoNaoAvaliacao
    {
        private static IRepository<MotivoNaoAvaliacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<MotivoNaoAvaliacaoInfo>>();
            }
        }

        public IQueryable<MotivoNaoAvaliacaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public MotivoNaoAvaliacaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
