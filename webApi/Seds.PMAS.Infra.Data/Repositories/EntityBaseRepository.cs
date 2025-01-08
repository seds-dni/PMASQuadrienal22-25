using System;
using System.Collections.Generic;
using Seds.PMAS.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using Seds.PMAS.Dominio.Interfaces.Repositories;

namespace Seds.PMAS.Infra.Data.Repositories
{
    public class EntityBaseRepository<TEntity> : IDisposable, IEntityBaseRepository<TEntity> where TEntity : class
    {
        protected DBPMASContext DbContext = new DBPMASContext();

        public void Add(TEntity obj)
        {
            DbContext.Set<TEntity>().Add(obj);
            DbContext.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().ToList();
        }

        public void Update(TEntity obj)
        {
            DbContext.Entry(obj).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void Remove(TEntity obj)
        {
            DbContext.Set<TEntity>().Remove(obj);
            DbContext.SaveChanges();
        }

        public List<String> GetModifiedProperties(TEntity entity)
        {
            //var key = this.DbContext.CreateEntityKey(entity.GetType().Name, entity);
            //object original = null;
            //if (this.DbContext.TryGetObjectByKey(key, out original))
            //{
            //    return this.DbContext.ObjectStateManager.GetObjectStateEntry(key).GetModifiedProperties().ToList();
            //}
            //return new List<string>();
            throw new NotImplementedException();
        }



        public void Attach(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateNN<U>(TEntity objOiginal, List<U> lstNew, Func<U, List<U>, bool> any, System.Linq.Expressions.Expression<Func<TEntity, object>> propertyNN)
        {
            throw new NotImplementedException();
        }

        public bool UpdateList<U>(TEntity objOriginal, List<U> lstNew, Func<U, List<U>, bool> any, System.Linq.Expressions.Expression<Func<TEntity, object>> propertList) where U : class
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
