using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class PlanoAcao
    {
        private static IRepository<PlanoAcaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PlanoAcaoInfo>>();
            }
        }

        public IQueryable<PlanoAcaoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public PlanoAcaoInfo GetByIdProgramaProjeto(Int32 idProgramaProjeto)
        {
            return _repository.Single(t => t.IdProgramaProjeto == idProgramaProjeto);
        }

        public PlanoAcaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public void Delete(PlanoAcaoInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }     

        public void Add(PlanoAcaoInfo obj, Boolean commit)
        {
            _repository.Add(obj);

            //var log = Log.CreateLog(obj.I, EAcao.Add, 38, "Incluída a Unidade Privada " + obj.Nome + ".");
            //if (log != null)
            //    new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }


        public void Update(PlanoAcaoInfo obj, Boolean commit)
        {

            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
