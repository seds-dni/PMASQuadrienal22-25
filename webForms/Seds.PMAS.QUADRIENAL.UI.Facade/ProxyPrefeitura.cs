using System;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyPrefeitura : IDisposable
    {           
        public Seds.PMAS.QUADRIENAL.Servicos.PrefeituraService Service { get; set; }
        public ProxyPrefeitura()
        {
            Service = new Servicos.PrefeituraService();
        }

        ~ProxyPrefeitura()
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
