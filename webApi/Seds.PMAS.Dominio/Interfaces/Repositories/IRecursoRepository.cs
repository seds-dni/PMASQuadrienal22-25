using Seds.PMAS.Dominio.Entities;
using System;
using System.Collections.Generic;


namespace Seds.PMAS.Dominio.Interfaces.Repositories
{
    public interface IRecursoRepository : IDisposable
    {
        List<RecursoEntity> GetByIdPerfil(int idPerfil);
    }
}
