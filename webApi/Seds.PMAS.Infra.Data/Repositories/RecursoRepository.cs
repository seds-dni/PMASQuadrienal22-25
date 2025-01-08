using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;
using Seds.PMAS.Infra.Data.Context;

namespace Seds.PMAS.Infra.Data.Repositories
{
    public class RecursoRepository 
    {
        protected DBPMASContext DbContext = new DBPMASContext();

        //public RecursoRepository(DBPMASContext context)
        //{
        //    _context = context;
        //}

        //public void Dispose()
        //{
        //    _context.Dispose();
        //}

        public List<RecursoEntity> GetByIdPerfil(int idPerfil)
        {
            return DbContext.Recursos.Where(r => r.Perfis.Any(p => p.IdPerfil == idPerfil)).ToList();
        }
    }
}
