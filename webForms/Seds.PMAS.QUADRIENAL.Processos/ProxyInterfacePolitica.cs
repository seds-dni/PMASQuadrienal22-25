using System;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyInterfacePolitica : IDisposable
    {

        public Seds.PMAS.QUADRIENAL.Servicos.InterfacePoliticaService Service { get; set; }

        public ProxyInterfacePolitica()
        {
            Service = new Servicos.InterfacePoliticaService();
        }
        ~ProxyInterfacePolitica()
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
