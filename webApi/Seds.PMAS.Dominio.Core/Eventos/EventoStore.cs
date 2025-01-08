using System;

namespace Seds.PMAS.Dominio.Core.Eventos
{
    public class EventoStore : Evento
    {
        public EventoStore(Evento oEvento, string dados, string usuario)
        {
            IdAgregado = oEvento.IdAgregado;
            TipoMensagem = oEvento.TipoMensagem;
            Dados = dados;
            Usuario = usuario;
        }

        protected EventoStore() { }

        public Guid Id { get; private set; }

        public string Dados { get; private set; }

        public string Usuario { get; private set; }
    }
}
