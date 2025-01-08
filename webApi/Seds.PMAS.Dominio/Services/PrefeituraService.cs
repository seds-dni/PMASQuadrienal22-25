using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using Seds.PMAS.Dominio.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Seds.PMAS.Dominio.Services
{
    public class PrefeituraService : IPrefeituraService
    {
        private readonly IPrefeituraRepository _prefeituraRepositorio;
        public PrefeituraService(IPrefeituraRepository prefeituraRepositorio)
        {
            _prefeituraRepositorio = prefeituraRepositorio;
        }

        public void Dispose()
        {
            _prefeituraRepositorio.Dispose();
        }

        public PrefeituraEntity GetById(int id)
        {
            return _prefeituraRepositorio.GetById(id);
        }

        public void update(PrefeituraEntity obj)
        {
            _prefeituraRepositorio.Update(obj);
        }
    }
}
