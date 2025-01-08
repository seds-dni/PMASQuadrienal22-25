using System;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyDradsPlanoMunicipal : IDisposable
    {
       public Seds.PMAS.QUADRIENAL.Servicos.DradsPlanoMunicipalService Service { get; set; }
        public ProxyDradsPlanoMunicipal()
        {
            Service = new Servicos.DradsPlanoMunicipalService();
        }

        ~ProxyDradsPlanoMunicipal()
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
