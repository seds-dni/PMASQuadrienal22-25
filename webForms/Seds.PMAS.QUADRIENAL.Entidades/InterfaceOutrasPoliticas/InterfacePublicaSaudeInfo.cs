using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class InterfacePublicaSaudeInfo
    {
        public Int32 Id { get; set; }

        public Int32 IdPrefeitura { get; set; }

        public Boolean? ExisteProtocoloAtendimentoSaude { get; set; }

        public String DescricaoProtocoloAtendimentoSaude { get; set; }

        public Boolean? ExisteIntervencaoBolsaFamilia { get; set; }

        public String DescricaoIntervencaoBolsaFamilia { get; set; }

        public Boolean? ExisteIntervencaoBPC { get; set; }

        public String DescricaoIntervencaoBPC { get; set; }

        public Boolean? ExisteIntervencaoVitimas { get; set; }

        public String DescricaoIntervencaoVitimas { get; set; }

        public Boolean? ExisteIntervencaoIdosoPCD { get; set; }

        public String DescricaoIntervencaoIdosoPCD { get; set; }

        public Int32? AnoVigente { get; set; }
    }
}
