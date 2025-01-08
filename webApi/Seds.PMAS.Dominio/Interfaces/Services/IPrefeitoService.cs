using Seds.PMAS.Dominio.Entities;
using System;

namespace Seds.PMAS.Dominio.Interfaces.Services
{
    public interface IPrefeitoService : IDisposable
    {
        PrefeitoEntity GetByIdPrefeitura(int IdPrefeitura);
        PrefeitoEntity GetById(int id);
        void Create(PrefeitoEntity prefeito);
        void Update(PrefeitoEntity prefeito);
        void Delete(PrefeitoEntity prefeito);
    }
}
