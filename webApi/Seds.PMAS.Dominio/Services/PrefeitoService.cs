using System;
using System.Collections.Generic;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using Seds.PMAS.Dominio.Interfaces.Services;

namespace Seds.PMAS.Dominio.Services
{
    public class PrefeitoService : IPrefeitoService
    {
        private readonly IPrefeitoRepository _prefeitoRepositorio;

        public PrefeitoService(IPrefeitoRepository prefeitoRepositorio)
        {
            _prefeitoRepositorio = prefeitoRepositorio;
        }

        public void Add(PrefeitoEntity obj)
        {
            _prefeitoRepositorio.Create(obj);
        }

        public void Create(PrefeitoEntity prefeito)
        {
            _prefeitoRepositorio.Create(prefeito);
        }

        public void Delete(PrefeitoEntity prefeito)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _prefeitoRepositorio.Dispose();
        }

        public IEnumerable<PrefeitoEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public PrefeitoEntity GetById(int id)
        {
            return _prefeitoRepositorio.GetById(id);
        }

        public PrefeitoEntity GetByIdPrefeitura(int IdPrefeitura)
        {
            return _prefeitoRepositorio.GetByIdPrefeitura(IdPrefeitura);
        }

        public void Remove(PrefeitoEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Update(PrefeitoEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
