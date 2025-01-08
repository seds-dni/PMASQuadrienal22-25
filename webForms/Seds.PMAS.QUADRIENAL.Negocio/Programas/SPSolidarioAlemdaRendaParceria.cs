using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class SPSolidarioAlemdaRendaParceria
    {
        private static IRepository<SPSolidarioAlemdaRendaParceriaInfo> _repository
         {
            get
            {
                return ObjectFactory.GetInstance<IRepository<SPSolidarioAlemdaRendaParceriaInfo>>();
            }
        }

        public IQueryable<SPSolidarioAlemdaRendaParceriaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public SPSolidarioAlemdaRendaParceriaInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);            
            return centro;
        }

        public IQueryable<SPSolidarioAlemdaRendaParceriaInfo> GetByTransferenciaRenda(int idTransferenciaRenda)
        {
            return _repository.GetObjectSet().Include("TipoParceria").Include("Parceria").Where(m => m.IdTransferenciaRenda == idTransferenciaRenda);
        }

        public IQueryable<SPSolidarioAlemdaRendaParceriaInfo> GetByProgramaProjeto(int idProgramaProjeto)  
        {
            return _repository.GetObjectSet().Include("TipoParceria").Include("Parceria").Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public void Update(SPSolidarioAlemdaRendaParceriaInfo obj, Boolean commit)
        {
            new ValidadorTransferenciaRendaParceria().ValidarParceriaAlemdaRenda(obj);           
            _repository.Update(obj);            
            if (commit)
                ContextManager.Commit();
        }

        public void Add(SPSolidarioAlemdaRendaParceriaInfo obj, Boolean commit)
        {
            new ValidadorTransferenciaRendaParceria().ValidarParceriaAlemdaRenda(obj);           
            _repository.Add(obj);
            if(commit)
                ContextManager.Commit();                
        }

        public void Delete(SPSolidarioAlemdaRendaParceriaInfo obj, Boolean commit)
        {           
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }      
    }
}
