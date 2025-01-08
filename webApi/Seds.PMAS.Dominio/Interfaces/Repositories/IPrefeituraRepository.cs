using Seds.PMAS.Dominio.Entities;
using System;

namespace Seds.PMAS.Dominio.Interfaces.Repositories
{
    public interface IPrefeituraRepository : IDisposable
    {
        PrefeituraEntity GetById(int id);
        void Create(PrefeituraEntity prefeito);
        void Update(PrefeituraEntity prefeito);

    }
}
