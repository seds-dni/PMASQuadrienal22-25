using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Seds.PMAS.QUADRIENAL.Persistencia.Repositorio
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        ObjectContext Context { get; }        
    }
}
