using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Seds.PMAS.QUADRIENAL.Persistencia.Repositorio
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static Func<ObjectContext> _objectContextDelegate;        
        private static readonly Object _lockObject = new object();

        public static void SetObjectContext(Func<ObjectContext> objectContextDelegate)
        {
            _objectContextDelegate = objectContextDelegate;
        }        

        public IUnitOfWork Create()
        {
            ObjectContext context;

            lock (_lockObject)
            {
                context = _objectContextDelegate();
            }            

            return new EFUnitOfWork(context);
        }
    }
}
