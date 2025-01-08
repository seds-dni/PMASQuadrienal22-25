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
    public class ProgramaProjetoParceria
    {
        private static IRepository<ProgramaProjetoParceriaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ProgramaProjetoParceriaInfo>>();
            }
        }

        public IQueryable<ProgramaProjetoParceriaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ProgramaProjetoParceriaInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);            
            return centro;
        }

        public IQueryable<ProgramaProjetoParceriaInfo> GetByProgramaProjeto(int idProgramaProjeto)
        {
            return _repository.GetObjectSet().Include("TipoParceria").Include("Parceria").Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public void Update(ProgramaProjetoParceriaInfo obj, Boolean commit)
        {
            new ValidadorProgramaProjetoParceria().Validar(obj);           
            _repository.Update(obj);            
            if (commit)
                ContextManager.Commit();
        }

        public void Add(ProgramaProjetoParceriaInfo obj, Boolean commit)
        {
            new ValidadorProgramaProjetoParceria().Validar(obj);           
            _repository.Add(obj);
            if(commit)
                ContextManager.Commit();                
        }

        public void Delete(ProgramaProjetoParceriaInfo obj, Boolean commit)
        {           
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }       
    }
}
