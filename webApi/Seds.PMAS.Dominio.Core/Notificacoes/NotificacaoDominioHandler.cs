using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.PMAS.Dominio.Core.Notificacoes
{
    public class NotificacaoDominioHandler : INotificacaoDominioHandler<NotificacaoDominio>
    {
        public List<NotificacaoDominio> _notificacoes;
        public NotificacaoDominioHandler() 
        {
            _notificacoes = new List<NotificacaoDominio>();
        }
        public void Handle(NotificacaoDominio mensagem) 
        {
            _notificacoes.Add(mensagem);
        }

        public List<NotificacaoDominio> ObterNotificacoes() 
        {
            return _notificacoes;
        }

        public bool TemNotificacoes() 
        {
            return ObterNotificacoes().Any();
        }

        public void Dispose() 
        {
            _notificacoes = new List<NotificacaoDominio>();
        }
    }
}
