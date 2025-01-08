using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class InterfaceFormaDistribuicao
    {
        private static IRepository<InterfacePublicaDistribuicaoAlimentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InterfacePublicaDistribuicaoAlimentoInfo>>();
            }
        }

        public IQueryable<InterfacePublicaDistribuicaoAlimentoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public InterfacePublicaDistribuicaoAlimentoInfo GetById(int id)
        {
            var restaurante = _repository.Single(m => m.Id == id);
            return restaurante;
        }

        public IQueryable<InterfacePublicaDistribuicaoAlimentoInfo> GetByInterfaceAlimentacao(int idInterfaceAlimentacao)
        {

            return _repository.GetObjectSet().Where(r => r.IdInterfacePublicaAlimentacao == idInterfaceAlimentacao);
        }

        public void Add(InterfacePublicaDistribuicaoAlimentoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarLocalDistribuicao(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(InterfacePublicaDistribuicaoAlimentoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarLocalDistribuicao(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(InterfacePublicaDistribuicaoAlimentoInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
