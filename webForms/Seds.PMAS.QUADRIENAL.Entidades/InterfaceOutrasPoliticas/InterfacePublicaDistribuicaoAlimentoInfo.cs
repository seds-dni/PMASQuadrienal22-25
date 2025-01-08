using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class InterfacePublicaDistribuicaoAlimentoInfo
    {

        public Int32 Id { get; set; }

        public Int32 IdUnidadePrivada { get; set; }

        public Int32 IdInterfacePublicaAlimentacao { get; set; }

        public UnidadePrivadaInfo UnidadePrivada { get; set; }

        public InterfacePublicaAlimentacaoInfo InterfacePublicaAlimentacao { get; set; }
    }
}
