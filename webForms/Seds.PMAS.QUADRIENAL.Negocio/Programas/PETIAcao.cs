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
    public class PETIAcao
    {
        private static IRepository<PETIAcaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PETIAcaoInfo>>();
            }
        }

        public IQueryable<PETIAcaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public PETIAcaoInfo GetById(int id)
        {
            var centro = _repository.Single(a => a.Id == id);
            return centro;
        }

        public IQueryable<PETIAcaoInfo> GetByTransferenciaRenda(int idTransferenciaRenda)
        {
            return _repository.GetObjectSet().Include("PETIEixoAtuacao").Include("PETITipoAcao").Where(a => a.IdTransferenciaRenda == idTransferenciaRenda);
            //return _repository.GetObjectSet().Include("PETIEixoAtuacao").Include("PETITipoAcao").Include("PETISituacaoAcao").Where(a => a.IdTransferenciaRenda == idTransferenciaRenda);
        }

        public void Update(PETIAcaoInfo obj, Boolean commit)
        {
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(PETIAcaoInfo obj, Boolean commit)
        {
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(PETIAcaoInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}