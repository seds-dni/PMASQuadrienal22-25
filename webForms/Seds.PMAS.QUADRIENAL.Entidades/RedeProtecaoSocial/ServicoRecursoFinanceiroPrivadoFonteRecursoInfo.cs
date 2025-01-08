using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ServicoRecursoFinanceiroPrivadoFonteRecursoInfo : FonteRecursoInfo
    {

        public Int32 IdServicoRecursoFinanceiroFundosPrivado { get; set; }

        public ServicoRecursoFinanceiroFundosPrivadoInfo ServicoRecursoFinanceiroFundosPrivadoInfo { get; set; }

        public bool Liberado { get; set; }
    }
}
