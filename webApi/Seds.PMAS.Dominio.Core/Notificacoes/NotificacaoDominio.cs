using Seds.PMAS.Dominio.Core.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.PMAS.Dominio.Core.Notificacoes
{
    public class NotificacaoDominio : Evento
    {
        public Guid NotificacaoDominioId { get; private set; }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public int Versao { get; private set; }

        public NotificacaoDominio(string key, string value)
        {
            NotificacaoDominioId = Guid.NewGuid();
            Versao = 1;
            Key = key;
            Value = value;
        }
    }
}
