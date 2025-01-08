using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ServicoRecursoFinanceiroPublicoFonteRecursoInfo : FonteRecursoInfo
    {
        public int IdServicoRecursoFinanceiroFundosPublico { get; set; }

        public ServicoRecursoFinanceiroFundosPublicoInfo ServicoRecursoFinanceiroFundosPublicoInfo { get; set; }

        public bool Liberado { get; set; }
    }
}
