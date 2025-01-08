using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using Seds.PMAS.Dominio.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Seds.PMAS.Dominio.Services
{
    public class RecursoService : IRecursoService
    {
        private readonly IRecursoRepository _recursoRepositorio;


        public RecursoService(IRecursoRepository recursoRepositorio)
        // : base(recursoRepositorio) 
        {
            _recursoRepositorio = recursoRepositorio;
        }

        public void Dispose()
        {
            _recursoRepositorio.Dispose();
        }

        public List<RecursoEntity> GetByIdPerfil(int idPerfil)
        {
            return _recursoRepositorio.GetByIdPerfil(idPerfil).OrderBy(r => r.Ordem).ToList();
        }
    }
}
