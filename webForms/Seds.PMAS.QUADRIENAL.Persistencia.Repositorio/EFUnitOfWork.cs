using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Seds.PMAS.QUADRIENAL.Persistencia.Repositorio
{
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        public ObjectContext Context { get; private set; }        

        public EFUnitOfWork(ObjectContext context)
        {
            Context = context;            
            context.ContextOptions.LazyLoadingEnabled = true;
        }

        public void Commit()
        {
          //  Context.AcceptAllChanges();
             Context.SaveChanges();
        }

       

        public void Dispose()
        {
            if (Context != null)
            {                
                Context.Dispose();
                Context = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
