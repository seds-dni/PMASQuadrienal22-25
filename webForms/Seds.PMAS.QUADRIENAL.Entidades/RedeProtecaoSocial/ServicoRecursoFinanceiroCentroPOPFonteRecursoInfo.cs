using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo : FonteRecursoInfo
    {
        public int IdServicoRecursoFinanceiroFundosCentroPOP { get; set; }

        public ServicoRecursoFinanceiroFundosCentroPOPInfo ServicoRecursoFinanceiroFundosCentroPOPInfo { get; set; }

        public bool Liberado { get; set; }
    }
}
