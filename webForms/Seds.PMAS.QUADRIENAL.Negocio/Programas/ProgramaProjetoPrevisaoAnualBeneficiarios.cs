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
    public class ProgramaProjetoPrevisaoAnualBeneficiarios
    {
        private static IRepository<ProgramaProjetoPrevisaoAnualBeneficiariosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ProgramaProjetoPrevisaoAnualBeneficiariosInfo>>();
            }
        }

        public IQueryable<ProgramaProjetoPrevisaoAnualBeneficiariosInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ProgramaProjetoPrevisaoAnualBeneficiariosInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);
            return centro;
        }

        public ProgramaProjetoPrevisaoAnualBeneficiariosInfo GetByProgramaProjeto(int idProgramaProjeto)
        {
            return _repository.GetQuery().Where(m => m.IdPrograma == idProgramaProjeto).SingleOrDefault();
        }

        public void Update(ProgramaProjetoPrevisaoAnualBeneficiariosInfo obj, Boolean commit)
        {
            //new ValidadorProgramaProjetoPrevisaoAnualBeneficiarios().Validar(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(ProgramaProjetoPrevisaoAnualBeneficiariosInfo obj, Boolean commit)
        {
       //     new ValidadorProgramaProjetoPrevisaoAnualBeneficiarios().Validar(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(ProgramaProjetoPrevisaoAnualBeneficiariosInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
