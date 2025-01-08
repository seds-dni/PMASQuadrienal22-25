using Seds.PMAS.Dominio.Entities;
using System;
using System.Collections.Generic;

namespace Seds.PMAS.Aplicacao.Interface
{
    public interface IRecursoAppService : IDisposable
    {
        IEnumerable<RecursoEntity> GetByIdPerfil(int idPerfil);
    }
}
