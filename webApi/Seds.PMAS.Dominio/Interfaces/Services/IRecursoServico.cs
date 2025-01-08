using Seds.PMAS.Dominio.Entities;
using System;
using System.Collections.Generic;

namespace Seds.PMAS.Dominio.Interfaces.Services
{
    public interface IRecursoService : IDisposable
    {
        List<RecursoEntity> GetByIdPerfil(int idPerfil);
    }
}
