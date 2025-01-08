using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class OrgaoGestorIntencaoEstruturacaoEquipe
    {
        private static IRepository<IntencaoEstruturacaoEquipeInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<IntencaoEstruturacaoEquipeInfo>>();
            }
        }

        public IQueryable<IntencaoEstruturacaoEquipeInfo> GetByOrgaoGestorByExercicio(Int32 idOrgaoGestor, Int32 exercicio)
        {
            return _repository.GetObjectSet().Where(intencao => intencao.IdOrgaoGestor == idOrgaoGestor && intencao.Exercicio == exercicio);
        }

        public void Add(IntencaoEstruturacaoEquipeInfo obj, Boolean commit)
        {
            _repository.Add(obj);

            if (commit)
                ContextManager.Commit();
        }


        public void Update(IntencaoEstruturacaoEquipeInfo obj, Boolean commit)
        {
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(IntencaoEstruturacaoEquipeInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
