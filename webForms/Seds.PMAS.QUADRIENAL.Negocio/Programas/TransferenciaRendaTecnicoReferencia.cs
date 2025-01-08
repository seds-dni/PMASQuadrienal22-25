using Seds.PMAS.QUADRIENAL.Entidades.Programas;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.Programas
{
    public class TransferenciaRendaTecnicoReferencia
    {
        private static IRepository<TransferenciaRendaTecnicoReferenciaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TransferenciaRendaTecnicoReferenciaInfo>>();
            }
        }

        public IQueryable<TransferenciaRendaTecnicoReferenciaInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public TransferenciaRendaTecnicoReferenciaInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);
            return centro;
        }

        public IQueryable<TransferenciaRendaTecnicoReferenciaInfo> GetByTransferenciaRenda(int idTransferenciaRenda)
        {
            return _repository.GetObjectSet().Where(m => m.IdTransferenciaRenda == idTransferenciaRenda);
        }

        public void Update(TransferenciaRendaTecnicoReferenciaInfo obj, Boolean commit)
        {
            //new ValidadorTransferenciaRendaParceria().Validar(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(TransferenciaRendaTecnicoReferenciaInfo obj, Boolean commit)
        {
            //new ValidadorTransferenciaRendaParceria().Validar(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(TransferenciaRendaTecnicoReferenciaInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
