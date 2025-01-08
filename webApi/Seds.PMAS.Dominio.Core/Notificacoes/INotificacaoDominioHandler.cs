using System.Collections.Generic;
using Seds.PMAS.Dominio.Core.Eventos;

namespace Seds.PMAS.Dominio.Core.Notificacoes
{
    public interface INotificacaoDominioHandler<T> : IHandler<T> where T : Mensagem
    {
       bool TemNotificacoes();
       List<T> ObterNotificacoes();
    }
}
