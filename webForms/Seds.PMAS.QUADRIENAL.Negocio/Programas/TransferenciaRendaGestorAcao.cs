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
    public class TransferenciaRendaGestorAcao
    {
        private static IRepository<TransferenciaRendaGestorAcaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TransferenciaRendaGestorAcaoInfo>>();
            }
        }


        public IQueryable<TransferenciaRendaGestorAcaoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public TransferenciaRendaGestorAcaoInfo GetById(Int32 id) 
        {
            return _repository.GetQuery().Where(m => m.Id == id).SingleOrDefault();
        }

        public TransferenciaRendaGestorAcaoInfo GetByIdTransferenciaRenda(Int32 idTransferenciaRenda)
        {
            return _repository.GetQuery().Where(m => m.IdTransferenciaRenda == idTransferenciaRenda).SingleOrDefault();
        }

        public void Add(TransferenciaRendaGestorAcaoInfo gestorAcao, Boolean commit)
        {
            new ValidadorTransferenciaRendaGestorAcao().Validar(gestorAcao);
            _repository.Add(gestorAcao);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(TransferenciaRendaGestorAcaoInfo gestorAcao, Boolean commit)
        {
            new ValidadorTransferenciaRendaGestorAcao().Validar(gestorAcao);
            _repository.Update(gestorAcao);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(TransferenciaRendaGestorAcaoInfo obj)
        {
            _repository.Delete(obj);
        }

    }
}
