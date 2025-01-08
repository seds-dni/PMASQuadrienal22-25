using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.UI.Processos
{
    public class ProxyDivisaoAdministrativa : IDisposable
    {
        public Seds.WebApiClient.DivisaoAdministrativaClient Service { get; set; }
        public ProxyDivisaoAdministrativa()
        {
            Service = new Seds.WebApiClient.DivisaoAdministrativaClient();
        }

        ~ProxyDivisaoAdministrativa()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Service = null;
        }
    }
}
