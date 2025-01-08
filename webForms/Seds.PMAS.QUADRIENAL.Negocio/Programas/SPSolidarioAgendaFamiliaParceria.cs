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
    public class SPSolidarioAgendaFamiliaParceria
    {
        private static IRepository<SPSolidarioAgendaFamiliaParceriaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<SPSolidarioAgendaFamiliaParceriaInfo>>();
            }
        }

        public IQueryable<SPSolidarioAgendaFamiliaParceriaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public SPSolidarioAgendaFamiliaParceriaInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);            
            return centro;
        }

        public IQueryable<SPSolidarioAgendaFamiliaParceriaInfo> GetByTransferenciaRenda(int idTransferenciaRenda)
        {
            return _repository.GetObjectSet().Include("TipoParceria").Include("Parceria").Where(m => m.IdTransferenciaRenda == idTransferenciaRenda);
        }

        public IQueryable<SPSolidarioAgendaFamiliaParceriaInfo> GetByProgramaProjeto(int idProgramaProjeto)  
        {
            return _repository.GetObjectSet().Include("TipoParceria").Include("Parceria").Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public void Update(SPSolidarioAgendaFamiliaParceriaInfo obj, Boolean commit)
        {
            new ValidadorTransferenciaRendaParceria().Validar(obj);           
            _repository.Update(obj);            
            if (commit)
                ContextManager.Commit();
        }

        public void Add(SPSolidarioAgendaFamiliaParceriaInfo obj, Boolean commit)
        {
            new ValidadorTransferenciaRendaParceria().Validar(obj);           
            _repository.Add(obj);
            if(commit)
                ContextManager.Commit();                
        }

        public void Delete(SPSolidarioAgendaFamiliaParceriaInfo obj, Boolean commit)
        {           
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }       
    }
}
