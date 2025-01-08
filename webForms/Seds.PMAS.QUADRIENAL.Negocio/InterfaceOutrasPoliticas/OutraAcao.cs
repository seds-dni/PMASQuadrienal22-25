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
    public class OutraAcao
    {
        private static IRepository<InterfacePublicaAlimentacaoOutraAcaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InterfacePublicaAlimentacaoOutraAcaoInfo>>();
            }
        }

        public IQueryable<InterfacePublicaAlimentacaoOutraAcaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public InterfacePublicaAlimentacaoOutraAcaoInfo GetById(int id)
        {
            var restaurante = _repository.Single(m => m.Id == id);
            return restaurante;
        }

        public IQueryable<InterfacePublicaAlimentacaoOutraAcaoInfo> GetByInterfaceAlimentacao(int idInterfaceAlimentacao)
        {

            return _repository.GetObjectSet().Where(r => r.IdInterfacePublicaAlimentacao == idInterfaceAlimentacao);
        }

        public void Add(InterfacePublicaAlimentacaoOutraAcaoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarOutraAcao(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(InterfacePublicaAlimentacaoOutraAcaoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarOutraAcao(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(InterfacePublicaAlimentacaoOutraAcaoInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
