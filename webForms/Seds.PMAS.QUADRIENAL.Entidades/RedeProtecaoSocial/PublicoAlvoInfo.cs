using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class PublicoAlvoInfo
    {
        public int Id { get; set; }

        public String Nome { get; set; }

        public Int32 IdFormaAtuacao { get; set; }

        public FormaAtuacaoInfo FormaAtuacao { get; set; }
    }
}
