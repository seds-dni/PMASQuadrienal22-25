using Seds.PMAS.Aplicacao.Interface;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Seds.PMAS.Aplicacao
{
    public class RecursoAppService : IRecursoService
    {
        private readonly IRecursoService _recursoServico;
        public RecursoAppService(IRecursoService recursoServico)
        //    : base(recursoServico)
        {
            _recursoServico = recursoServico;
        }

        public void Dispose()
        {
            _recursoServico.Dispose();
        }

        public new List<RecursoEntity> GetByIdPerfil(int idPerfil)
        {
            return _recursoServico.GetByIdPerfil(idPerfil).GroupBy(t => t.IdPai).SelectMany(gr => gr).ToList();
        }
    }
}
