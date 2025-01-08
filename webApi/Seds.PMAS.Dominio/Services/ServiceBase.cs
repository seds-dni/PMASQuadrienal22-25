using Seds.PMAS.Dominio.Interfaces.Repositories;
using Seds.PMAS.Dominio.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Seds.PMAS.Dominio.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IEntityBaseRepository<TEntity> _repositorio;

        public ServiceBase(IEntityBaseRepository<TEntity> repositorio)
        {
            _repositorio = repositorio;
        }
        public void Add(TEntity obj)
        {
            _repositorio.Add(obj);
        }

        public TEntity GetById(int id)
        {
            return _repositorio.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repositorio.GetAll();
        }

        public void Update(TEntity obj)
        {
            _repositorio.Update(obj);
        }

        public void Remove(TEntity obj)
        {
            _repositorio.Remove(obj);
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }
    }
}
