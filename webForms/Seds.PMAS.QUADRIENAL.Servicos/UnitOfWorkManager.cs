using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Negocio;

namespace Seds.PMAS.QUADRIENAL.Servicos
{
    public class UnitOfWorkManager
    {
        public static void Initialize()
        {
            ContextManager.Initialize();
        }

        public static void Commit()
        {
            ContextManager.Commit();
        }

        public static void Dispose()
        {
            ContextManager.Dispose();
        }
    }
}
