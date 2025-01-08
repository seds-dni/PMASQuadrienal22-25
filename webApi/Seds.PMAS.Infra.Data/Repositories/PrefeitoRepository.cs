using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Seds.PMAS.Infra.Data.Context;

namespace Seds.PMAS.Infra.Data.Repositories
{
    public class PrefeitoRepository : IPrefeitoRepository
    {
        private DBPMASContext _context;
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Create(PrefeitoEntity prefeito)
        {
            _context.Prefeitos.Add(prefeito);
            _context.SaveChanges();
        }

        public void Delete(PrefeitoEntity prefeito)
        {
            throw new NotImplementedException();
        }

        public PrefeitoEntity GetById(int id)
        {
            return _context.Prefeitos.Where(p => p.Id == id).FirstOrDefault();
        }

        public PrefeitoEntity GetByIdPrefeitura(int idPrefeitura)
        {
            return _context.Prefeitos.Where(u => u.IdPrefeitura == idPrefeitura).FirstOrDefault();
        }

        public void Update(PrefeitoEntity prefeito)
        {
            _context.Entry<PrefeitoEntity>(prefeito).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
