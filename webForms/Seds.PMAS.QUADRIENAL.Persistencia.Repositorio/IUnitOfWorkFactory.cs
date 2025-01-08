using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Persistencia.Repositorio
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
