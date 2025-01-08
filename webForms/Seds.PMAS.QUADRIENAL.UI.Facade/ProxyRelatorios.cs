using System;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyRelatorios : IDisposable
    {
        public Seds.PMAS.QUADRIENAL.Servicos.RelatoriosService Service { get; set; }
        public ProxyRelatorios()
        {
            Service = new Servicos.RelatoriosService();
        }

        ~ProxyRelatorios()
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
