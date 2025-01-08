using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class InterfacePublicaAlimentacaoOutraAcaoInfo
    {
        public Int32 Id { get; set; }

        public Int32 IdInterfacePublicaAlimentacao { get; set; }

        public String AcaoDesenvolvida { get; set; }

        public String OrgaoResponsavel { get; set; }

        public InterfacePublicaAlimentacaoInfo InterfacePublicaAlimentacao { get; set; }
    }
}
