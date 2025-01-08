
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Linq;
namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class FormaDistribuicaoAlimento
    {

        private static IRepository<InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo>>();
            }
        }

        public IQueryable<InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo GetById(int id)
        {
            var restaurante = _repository.Single(m => m.Id == id);
            return restaurante;
        }

        public IQueryable<InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo> GetByInterfaceAlimentacao(int idInterfaceAlimentacao)
        {

            return _repository.GetObjectSet().Where(r => r.IdInterfacePublicaAlimentacao == idInterfaceAlimentacao);
        }

        public void Add(InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarOutraFormaDistribuicao(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarOutraFormaDistribuicao(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
