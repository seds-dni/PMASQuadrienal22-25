using Seds.PMAS.Dominio.Core.Eventos;

namespace Seds.PMAS.Dominio.Core.Bus
{
    public interface IBus
    {
        void RaiseEvento<T>(T oEvento) where T : Evento;
    }
}
