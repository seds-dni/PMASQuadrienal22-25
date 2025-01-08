using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Objects;

namespace Seds.PMAS.QUADRIENAL.Persistencia.Repositorio
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetQuery();
        ObjectSet<TEntity> GetObjectSet();

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Func<TEntity, bool> where);
        TEntity Single(Func<TEntity, bool> where);
        TEntity First(Func<TEntity, bool> where);

        void Delete(TEntity entity);
        void Add(TEntity entity);
        void Attach(TEntity entity);
        void Update(TEntity entity);
        List<String> GetModifiedProperties(TEntity entity);
        bool UpdateNN<U>(TEntity objOiginal, List<U> lstNew, Func<U, List<U>, bool> any, Expression<Func<TEntity, object>> propertyNN);
        bool UpdateList<U>(TEntity objOriginal, List<U> lstNew, Func<U, List<U>, bool> any, Expression<Func<TEntity, object>> propertList) where U : class;
        void SaveChanges();
    }
}
