using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ServicoRecursoFinanceiroCREASFonteRecursoInfo : FonteRecursoInfo
    {
        public int IdServicoRecursoFinanceiroFundosCREAS { get; set; }

        public ServicoRecursoFinanceiroFundosCREASInfo ServicoRecursoFinanceiroFundosCREASInfo { get; set; }

        public bool Liberado { get; set; }

    }
}
