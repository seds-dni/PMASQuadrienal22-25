using System;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyRedeProtecaoSocial : IDisposable
    {
        public Seds.PMAS.QUADRIENAL.Servicos.RedeProtecaoSocialService Service { get; set; }
        public ProxyRedeProtecaoSocial()
        {
            Service = new Servicos.RedeProtecaoSocialService();
        }

        ~ProxyRedeProtecaoSocial()
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
