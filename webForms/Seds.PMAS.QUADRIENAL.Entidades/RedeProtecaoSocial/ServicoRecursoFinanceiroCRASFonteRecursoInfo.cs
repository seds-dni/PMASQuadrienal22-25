using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ServicoRecursoFinanceiroCRASFonteRecursoInfo : FonteRecursoInfo
    {
        public int IdServicoRecursoFinanceiroFundosCRAS { get; set; }

        public ServicoRecursoFinanceiroFundosCRASInfo ServicoRecursoFinanceiroFundosCRASInfo { get; set; }

        public bool Liberado { get; set; }
    }
}
