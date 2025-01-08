using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Recurso
    {
        private static IRepository<RecursoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<RecursoInfo>>();
            }
        }

        public IQueryable<RecursoInfo> GetRecursosByPerfil(int idPerfil)
        {
            return _repository.GetQuery().Where(r => r.Perfis.Any(p => p.IdPerfil == idPerfil));
        }
    }
}
