using Seds.PMAS.Dominio.Entities;
using System;

namespace Seds.PMAS.Dominio.Interfaces.Services
{
    public interface IPrefeituraService : IDisposable
    {
        PrefeituraEntity GetById(int id);
        void update(PrefeituraEntity obj);

    }
}
