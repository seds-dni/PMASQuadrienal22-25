using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class OrgaoGestorEquipeEspecificaTotais
    {
        private static IRepository<EquipeEspecificaTotaisInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<EquipeEspecificaTotaisInfo>>();
            }
        }

        public IQueryable<EquipeEspecificaTotaisInfo> GetByOrgaoGestorEquipeEspecificaTotaisByExercicio(Int32 idOrgaoGestor, int exercicio)
        {
            return _repository.GetObjectSet().Where(t => t.IdOrgaoGestor == idOrgaoGestor && t.Exercicio == exercicio);
        }

        public IQueryable<EquipeEspecificaTotaisInfo> GetByOrgaoGestorEquipeEspecificaTotais(Int32 idOrgaoGestor)
        {
            return _repository.GetObjectSet().Where(t => t.IdOrgaoGestor == idOrgaoGestor);
        }

        public void Add(EquipeEspecificaTotaisInfo obj, Boolean commit)
        {
            _repository.Add(obj);

            if (commit)
                ContextManager.Commit();
        }


        public void Update(EquipeEspecificaTotaisInfo obj, Boolean commit)
        {
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(EquipeEspecificaTotaisInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
