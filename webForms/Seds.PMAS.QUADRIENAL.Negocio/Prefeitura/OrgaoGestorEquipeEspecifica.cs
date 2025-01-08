using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class OrgaoGestorEquipeEspecifica
    {
        private static IRepository<EquipeEspecificaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<EquipeEspecificaInfo>>();
            }
        }

        public IQueryable<EquipeEspecificaInfo> GetByOrgaoGestorEquipeEspecifica(Int32 idOrgaoGestor)
        {
            return _repository.GetObjectSet().Include("TipoEquipe").Where(t => t.IdOrgaoGestor == idOrgaoGestor);
        }

        public IQueryable<EquipeEspecificaInfo> GetByOrgaoGestorEquipeEspecificaByExercicio(Int32 idOrgaoGestor, int exercicio)
        {
            return _repository.GetObjectSet().Include("TipoEquipe").Where(t => t.IdOrgaoGestor == idOrgaoGestor && t.Exercicio == exercicio);
        }

        public void Add(EquipeEspecificaInfo obj, Boolean commit)
        {
            _repository.Add(obj);

            if (commit)
                ContextManager.Commit();
        }


        public void Update(EquipeEspecificaInfo obj, Boolean commit)
        {
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(EquipeEspecificaInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
