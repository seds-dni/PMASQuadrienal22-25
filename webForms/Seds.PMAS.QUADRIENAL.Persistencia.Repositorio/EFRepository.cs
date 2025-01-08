using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Objects;
using StructureMap;
using System.Xml.Serialization;

namespace Seds.PMAS.QUADRIENAL.Persistencia.Repositorio
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private ObjectContext _context;
        private ObjectSet<T> _objectSet;      
        
        protected ObjectContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = GetCurrentUnitOfWork<EFUnitOfWork>().Context;
                }

                return _context;
            }
        }

        protected ObjectSet<T> ObjectSet
        {
            get
            {
                if (_objectSet == null)
                {
                    _objectSet = this.Context.CreateObjectSet<T>();
                }

                return _objectSet;
            }
        }

        public TUnitOfWork GetCurrentUnitOfWork<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
        {
            return (TUnitOfWork)UnitOfWork.Current;
        }

        public IQueryable<T> GetQuery()
        {
            return ObjectSet;
        }

        public ObjectSet<T> GetObjectSet()
        {
            return ObjectSet;
        }

        public IEnumerable<T> GetAll()
        {
            return GetQuery().ToList();
        }

        public IEnumerable<T> Find(Func<T, bool> where)
        {
            return this.ObjectSet.Where<T>(where);
        }

        public T Single(Func<T, bool> where)
        {
            return this.ObjectSet.FirstOrDefault<T>(where);
        }

        public T First(Func<T, bool> where)
        {
            return this.ObjectSet.First<T>(where);
        }

        public virtual void Delete(T entity)
        {
            this.ObjectSet.DeleteObject(entity);
        }

        public virtual void Add(T entity)
        {
            this.ObjectSet.AddObject(entity);
        }

        public void Attach(T entity)
        {
            this.ObjectSet.Attach(entity);            
        }

        public void Update(T entity)
        {            
            var key = this.Context.CreateEntityKey(entity.GetType().Name, entity);            
            object original = null;
            if (this.Context.TryGetObjectByKey(key, out original))
            {                
                this.Context.ApplyCurrentValues(key.EntitySetName, entity);                
                //var lst = this.Context.ObjectStateManager.GetObjectStateEntry(key).GetModifiedProperties();
            }            
        }

        public List<String> GetModifiedProperties(T entity)
        {
            var key = this.Context.CreateEntityKey(entity.GetType().Name, entity);            
            object original = null;
            if (this.Context.TryGetObjectByKey(key, out original))
            {
                return this.Context.ObjectStateManager.GetObjectStateEntry(key).GetModifiedProperties().ToList();
            }
            return new List<string>();
        }

        /// <summary>
        /// Atualiza a NN e retorna se houve mudança na lista
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="objOriginal"></param>
        /// <param name="lstNew"></param>
        /// <param name="any"></param>
        /// <param name="propertyNN"></param>
        /// <returns></returns>
        public bool UpdateNN<U>(T objOriginal, List<U> lstNew, Func<U, List<U>, bool> any, Expression<Func<T, object>> propertyNN)
        {
            ObjectStateEntry objectState;            
            this.Context.LoadProperty<T>(objOriginal, propertyNN);
            var lst = propertyNN.Compile().Invoke(objOriginal) as List<U>;
            var lstDeleted = new List<U>();
            var hasChange = false;
            foreach (var i in lst)
            {
                if (!any.Invoke(i, lstNew))
                {
                    lstDeleted.Add(i);
                    hasChange = true;
                }
            }

            foreach (var i in lstDeleted)
            {
                if (!this.Context.ObjectStateManager.TryGetObjectStateEntry(i, out objectState))
                    this.Context.AttachTo(i.GetType().Name, i);
                this.Context.ObjectStateManager.ChangeRelationshipState<T>(objOriginal, i, propertyNN, System.Data.EntityState.Deleted);
            }

            foreach (var i in lstNew)
            {
                if (!any.Invoke(i, lst))
                {
                    if (!this.Context.ObjectStateManager.TryGetObjectStateEntry(i, out objectState))
                        this.Context.AttachTo(i.GetType().Name, i);
                    this.Context.ObjectStateManager.ChangeRelationshipState<T>(objOriginal, i, propertyNN, System.Data.EntityState.Added);
                    hasChange = true;
                }
            }

            return hasChange;
        }

        public bool UpdateList<U>(T objOriginal, List<U> lstNew, Func<U, List<U>, bool> any, Expression<Func<T, object>> propertList) where U : class
        {
            ObjectStateEntry objectState;
            this.Context.LoadProperty<T>(objOriginal, propertList);
            var lst = propertList.Compile().Invoke(objOriginal) as List<U>;
            var lstDeleted = new List<U>();
            var hasChange = false;
            foreach (var i in lst)
            {
                if (!any.Invoke(i, lstNew))
                {
                    lstDeleted.Add(i);
                    hasChange = true;
                }                
            }

            foreach (var i in lstDeleted)
            {
                if (!this.Context.ObjectStateManager.TryGetObjectStateEntry(i, out objectState))
                    this.Context.AttachTo(i.GetType().Name, i);
                this.Context.ObjectStateManager.ChangeObjectState(i, System.Data.EntityState.Deleted);
            }

            foreach (var i in lstNew)
            {
                if (!any.Invoke(i, lst))
                {
                    if (!this.Context.ObjectStateManager.TryGetObjectStateEntry(i, out objectState))
                        this.Context.AttachTo(i.GetType().Name, i);
                    this.Context.ObjectStateManager.ChangeObjectState(i, System.Data.EntityState.Added);
                    hasChange = true;
                }
                else
                {
                    var repository = ObjectFactory.GetInstance<IRepository<U>>();
                    repository.Update(i);
                    //if (repository.GetModifiedProperties(i).Count > 0)
                    //    hasChange = true;
                }
            }
            return hasChange;
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
