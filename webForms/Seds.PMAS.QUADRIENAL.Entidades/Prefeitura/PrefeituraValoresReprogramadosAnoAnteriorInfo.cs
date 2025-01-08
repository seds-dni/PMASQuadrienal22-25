using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class PrefeituraValoresReprogramadosAnoAnteriorInfo
    {
        public Int32 Id { get; set; }
        public Int32 IdPrefeitura { get; set; }
        public Int32 Revisao { get; set; }
        public DateTime Data { get; set; }
        public Decimal ValorProtecaoSocialBasicaReprogramado { get; set; }
        public Decimal ValorProtecaoSocialEspecialReprogramado { get; set; }
        public Decimal ValorLiberdadeAssistidaReprogramado { get; set; }
        public Decimal ValorCreasReprogramado { get; set; }
        public Decimal ValorSPSolidarioReprogramado { get; set; }

    }
}
