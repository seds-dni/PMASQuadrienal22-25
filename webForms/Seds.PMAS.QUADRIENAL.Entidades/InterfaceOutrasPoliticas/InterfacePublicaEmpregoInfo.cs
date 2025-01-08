using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class InterfacePublicaEmpregoInfo
    {
        public int Id { get; set; }

        public Int32 IdPrefeitura { get; set; }
        public Boolean? ExisteIntervencaoJovens { get; set; }
        public String DescricaoIntervencaoJovens { get; set; }
        public Boolean? ExisteIntervencaoPCD { get; set; }
        public String DescricaoIntervencaoPCD { get; set; }
        public Boolean? ExisteOutrasAcoesArticuladas { get; set; }
        public String DescricaoOutrasAcoesArticuladas { get; set; }
        public Int32? AnoVigente { get; set; }
    }
}
