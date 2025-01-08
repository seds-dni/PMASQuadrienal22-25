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
    public class SPSolidarioPlanejamentoBens
    {
        private static IRepository<SPSolidarioPlanejamentoBensInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<SPSolidarioPlanejamentoBensInfo>>();
            }
        }

        public IQueryable<SPSolidarioPlanejamentoBensInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public SPSolidarioPlanejamentoBensInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);
            return centro;
        }

        public IQueryable<SPSolidarioPlanejamentoBensInfo> GetByProgramaProjeto(int idProgramaProjeto)  
        {
            return _repository.GetObjectSet().Include("ProgramaProjeto").Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public void Update(SPSolidarioPlanejamentoBensInfo obj, Boolean commit)
        {
            new ValidadorPlanejamentoBens().Validar(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(SPSolidarioPlanejamentoBensInfo obj, Boolean commit)
        {
            new ValidadorPlanejamentoBens().Validar(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(SPSolidarioPlanejamentoBensInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
