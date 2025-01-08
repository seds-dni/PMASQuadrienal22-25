using Seds.PMAS.Dominio.Core.Eventos;
using System;
using FluentValidation.Results;

namespace Seds.PMAS.Dominio.Core.Comandos
{
    public abstract class Comando : Mensagem
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Comando()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
