using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seds.PMAS.Dominio.Core.Eventos;
using Seds.PMAS.Dominio.Core.Comandos;
using Seds.PMAS.Dominio.Core.Notificacoes;

namespace Seds.PMAS.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus
    {
        public static Func<IServiceProvider> ContainerAccessor { get; set; }

        public static IServiceProvider Container;
       
        private readonly IEventoStore _eventoStore;

         public InMemoryBus(IEventoStore eventoStore)
        {
            _eventoStore = eventoStore;
        }

        public void SendCommand<T>(T theCommand) where T : Comando
        {
            Publish(theCommand);
        }

        public void RaiseEvent<T>(T oEvento) where T : Evento
        {
            if(!oEvento.TipoMensagem.Equals("NotificacaoDominio")){
                _eventoStore.Save(oEvento);
            }
            Publish(oEvento);
        }

        private static void Publish<T>(T message) where T : Mensagem
        {
            if (Container == null) return;

            var obj = Container.GetService(message.TipoMensagem.Equals("NotificacaoDominio")
                ? typeof(INotificacaoDominioHandler<T>)
                : typeof(IHandler<T>));

            ((IHandler<T>)obj).Handle(message);
        }

        private object GetService(Type serviceType)
        {
            return Container.GetService(serviceType);
        }

        private T GetService<T>()
        {
            return (T)Container.GetService(typeof(T));
        }


    }
}
