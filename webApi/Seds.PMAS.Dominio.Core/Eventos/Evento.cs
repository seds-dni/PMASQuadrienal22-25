using System;
namespace Seds.PMAS.Dominio.Core.Eventos
{
    public abstract class Evento : Mensagem
    {
        public DateTime Timestamp { get; private set; }
        protected Evento()
        {
            Timestamp = DateTime.Now;
        }
    }
}
