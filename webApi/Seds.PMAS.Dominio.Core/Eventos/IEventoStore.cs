
namespace Seds.PMAS.Dominio.Core.Eventos
{
    public interface IEventoStore
    {
        void Save<T>(T oEvento) where T : Evento;
    }
}
