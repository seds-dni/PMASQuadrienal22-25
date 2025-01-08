using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
   public class ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo : RecursosHumanosInfo
    {
       public Int32 IdServicosRecursosFinanceirosCentroPOP { get; set; }

       public ServicoRecursoFinanceiroCentroPOPInfo servicoRecursoFinanceiroCentroPOP { get; set; }
    }
}
