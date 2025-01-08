using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Seds.PMAS.Dominio.Interfaces.Repositories
{
    public interface IEntityBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();

        void Update(TEntity obj);
        void Remove(TEntity obj);

        void Attach(TEntity entity);
        List<String> GetModifiedProperties(TEntity entity);
        bool UpdateNN<U>(TEntity objOiginal, List<U> lstNew, Func<U, List<U>, bool> any, Expression<Func<TEntity, object>> propertyNN);
        bool UpdateList<U>(TEntity objOriginal, List<U> lstNew, Func<U, List<U>, bool> any, Expression<Func<TEntity, object>> propertList) where U : class;
        void Dispose();

    }
}
