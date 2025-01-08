using System;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyUsuarioPMAS : IDisposable
    {
        public Seds.PMAS.QUADRIENAL.Servicos.UsuarioPMASService Service { get; set; }
        public ProxyUsuarioPMAS()
        {
            Service = new Servicos.UsuarioPMASService();
        }

        ~ProxyUsuarioPMAS()
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
