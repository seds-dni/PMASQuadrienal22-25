using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo : RecursosHumanosInfo
    {
        public Int32 IdServicosRecursosFinanceirosPrivado { get; set; }
        public ServicoRecursoFinanceiroPrivadoInfo servicoRecursoFinanceiroPrivado { get; set; }
    }
}
