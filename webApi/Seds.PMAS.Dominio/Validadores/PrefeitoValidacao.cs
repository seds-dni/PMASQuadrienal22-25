using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Seds.PMAS.Dominio.Entidades;

namespace Seds.PMAS.Dominio.Validadores
{
    public abstract class PrefeitoValidacao<PrefeitoEntity> : AbstractValidator<PrefeitoEntity> where PrefeitoEntity : PrefeitoEntity
    {
        protected void ValidarNome()
        {
            throw new NotImplementedException();
            //RuleFor(c => c.Nome).NotEmpty().WithMessage("Certifique-se de que introduziu o nome")
            //    .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres");
        }

        protected void ValidarRG()
        {
            throw new NotImplementedException();
            //RuleFor(c => c.Nome).NotEmpty().WithMessage("Certifique-se de que introduziu o RG")
            //    .Length(9, 11).WithMessage("O Campo RG deve ter entre 9 e 11 caracteres");
        }

        public void ValidarSigla()
        {
            throw new NotImplementedException();
            //RuleFor(c => c.SiglaEmissor).NotEmpty().WithMessage("Certifique-se de que introduziu o Órgão Emissor")
            //        .Length(9, 11).WithMessage("O Campo Órgão Emissor deve ter entre 3 e 5 caracteres");
        }

        public void ValidarEmail()
        {
            throw new NotImplementedException();
            //RuleFor(c => c.Email).NotEmpty().WithMessage("Certifique-se de que introduziu o e-mail")
            //    .EmailAddress().WithMessage("Certique-se de que informou um e-mail válido")
            //    .Length(10, 80).WithMessage("O campo E-mail deve ter entre 10 e 80 caracteres");
        }

    }
}
