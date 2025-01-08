using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class TransferenciaRendaParceria
    {
        private static IRepository<TransferenciaRendaParceriaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TransferenciaRendaParceriaInfo>>();
            }
        }

        public IQueryable<TransferenciaRendaParceriaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public TransferenciaRendaParceriaInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);            
            return centro;
        }

        public IQueryable<TransferenciaRendaParceriaInfo> GetByTransferenciaRenda(int idTransferenciaRenda)
        {
            return _repository.GetObjectSet().Include("TipoParceria").Include("Parceria").Where(m => m.IdTransferenciaRenda == idTransferenciaRenda);
        }

        public void Update(TransferenciaRendaParceriaInfo obj, Boolean commit)
        {
            new ValidadorTransferenciaRendaParceria().Validar(obj);           
            _repository.Update(obj);            
            if (commit)
                ContextManager.Commit();
        }

        public void Add(TransferenciaRendaParceriaInfo obj, Boolean commit)
        {
            new ValidadorTransferenciaRendaParceria().Validar(obj);           
            _repository.Add(obj);
            if(commit)
                ContextManager.Commit();                
        }

        public void Delete(TransferenciaRendaParceriaInfo obj, Boolean commit)
        {           
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }       
    }
}
