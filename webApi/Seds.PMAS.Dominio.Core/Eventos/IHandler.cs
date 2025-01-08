using System;


namespace Seds.PMAS.Dominio.Core.Eventos
{
    public interface IHandler<in T> where T : Mensagem
    {
        void Handle(T message);
    }
}
