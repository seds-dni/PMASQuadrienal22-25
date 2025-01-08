using System;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyPlanoMunicipal : IDisposable
    {
       public Seds.PMAS.QUADRIENAL.Servicos.PlanoMunicipalService Service { get; set; }
        public ProxyPlanoMunicipal()
        {
            Service = new Servicos.PlanoMunicipalService();
        }

        ~ProxyPlanoMunicipal()
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
