using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo
    {
        public Int32 Id { get; set; }

        public Int32 IdInterfacePublicaAlimentacao { get; set; }

        public String Descricao { get; set; }

        public String Responsavel { get; set; }

        public InterfacePublicaAlimentacaoInfo InterfacePublicaAlimentacao { get; set; }
    }
}
