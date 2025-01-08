using Seds.PMAS.Dominio.Entities;
using System;
using System.Collections.Generic;

namespace Seds.PMAS.Dominio.Interfaces.Repositories
{
    public interface IPrefeitoRepository : IDisposable
    {
        //IEnumerable<PrefeitoEntity> GetPrefeitosAnteriores();
        PrefeitoEntity GetByIdPrefeitura(int idPrefeitura);
        PrefeitoEntity GetById(int id);
        void Create(PrefeitoEntity prefeito);
        void Update(PrefeitoEntity prefeito);
        void Delete(PrefeitoEntity prefeito);
    }
}
