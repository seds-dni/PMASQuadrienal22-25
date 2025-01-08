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
    public class RestaurantePopular
    {
        private static IRepository<InterfacePublicaAlimentacaoRestauranteInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InterfacePublicaAlimentacaoRestauranteInfo>>();
            }
        }

        public IQueryable<InterfacePublicaAlimentacaoRestauranteInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public InterfacePublicaAlimentacaoRestauranteInfo GetById(int id)
        {
            var restaurante = _repository.Single(m => m.Id == id);
            return restaurante;
        }

        public IQueryable<InterfacePublicaAlimentacaoRestauranteInfo> GetByInterfaceAlimentacao(int idInterfaceAlimentacao)
        {

            return _repository.GetObjectSet().Where(r => r.IdInterfacePublicaAlimentacao == idInterfaceAlimentacao);
        }

        public void Add(InterfacePublicaAlimentacaoRestauranteInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarRestaurantePopular(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(InterfacePublicaAlimentacaoRestauranteInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarRestaurantePopular(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(InterfacePublicaAlimentacaoRestauranteInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
