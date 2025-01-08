using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.PMAS.UI.Processos
{
    public class ProxyUsuarioPMAS : IDisposable
    {
        public Seds.PMAS.Servicos.UsuarioPMASServico Service { get; set; }
        public ProxyUsuarioPMAS()
        {
            Service = new Servicos.UsuarioPMASServico();
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
