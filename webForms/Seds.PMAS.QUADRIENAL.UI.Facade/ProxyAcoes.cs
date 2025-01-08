using System;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyAcoes : IDisposable
    {
        public Seds.PMAS.QUADRIENAL.Servicos.AcoesService Service { get; set; }
        public ProxyAcoes()
        {
            Service = new Servicos.AcoesService();
        }

        ~ProxyAcoes()
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
