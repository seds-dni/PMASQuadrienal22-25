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
    public class GrupoGestor
    {
        private static IRepository<ProgramaProjetoGrupoGestorInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ProgramaProjetoGrupoGestorInfo>>();
            }
        }

        public IQueryable<ProgramaProjetoGrupoGestorInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ProgramaProjetoGrupoGestorInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);
            return centro;
        }

        public IQueryable<ProgramaProjetoGrupoGestorInfo> GetByTransferenciaRenda(int idProgramaProjeto)
        {
            return _repository.GetObjectSet().Include("Parceria").Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public IQueryable<ProgramaProjetoGrupoGestorInfo> GetByProgramaProjeto(int idProgramaProjeto)  
        {
            return _repository.GetObjectSet().Include("Parceria").Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public void Update(ProgramaProjetoGrupoGestorInfo obj, Boolean commit)
        {
            new ValidadorProgramaProjetoGrupoGestor().Validar(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(ProgramaProjetoGrupoGestorInfo obj, Boolean commit)
        {
            new ValidadorProgramaProjetoGrupoGestor().Validar(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(ProgramaProjetoGrupoGestorInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }    
    }
}
