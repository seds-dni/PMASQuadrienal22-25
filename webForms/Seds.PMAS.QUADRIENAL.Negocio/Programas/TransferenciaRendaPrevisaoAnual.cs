using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class TransferenciaRendaPrevisaoAnual
    {
        private static IRepository<TransferenciaRendaPrevisaoAnualInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TransferenciaRendaPrevisaoAnualInfo>>();
            }
        }

        public IQueryable<TransferenciaRendaPrevisaoAnualInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public TransferenciaRendaPrevisaoAnualInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);
            return centro;
        }

        public TransferenciaRendaPrevisaoAnualInfo GetByTransferenciaRenda(int idTransferenciaRenda)
        {
            return _repository.Single(m => m.IdTransferenciaRenda == idTransferenciaRenda);
        }

        public TransferenciaRendaPrevisaoAnualInfo GetByTransferenciaRendaByIdPrefeitura(int idTransferenciaRenda,int idPrefeitura)
        {
            return _repository.Single(m => m.IdTransferenciaRenda == idTransferenciaRenda && m.IdPrefeitura == idPrefeitura);
        }

        public void Update(TransferenciaRendaPrevisaoAnualInfo obj, Boolean commit)
        {
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(TransferenciaRendaPrevisaoAnualInfo obj, Boolean commit)
        {
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(TransferenciaRendaPrevisaoAnualInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }     
    }
}
