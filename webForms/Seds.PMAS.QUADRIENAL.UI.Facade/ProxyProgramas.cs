using System;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyProgramas : IDisposable
    {
         public Seds.PMAS.QUADRIENAL.Servicos.ProgramasService Service { get; set; }
         public ProxyProgramas()
        {
            Service = new Servicos.ProgramasService();
        }

        ~ProxyProgramas()
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
