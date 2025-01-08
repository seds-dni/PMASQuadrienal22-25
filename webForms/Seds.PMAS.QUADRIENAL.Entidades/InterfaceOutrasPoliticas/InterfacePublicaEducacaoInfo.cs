using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class InterfacePublicaEducacaoInfo
    {
        public int Id { get; set; }
        public Int32 IdPrefeitura { get; set; }
        public Boolean? ExisteProtocoloFormal { get; set; }
        public String DescricaoEncaminhamentoEducacao { get; set; }
        public Boolean? ExisteIntervencaoBolsaFamilia { get; set; }
        public String DescricaoIntervencaoBolsaFamilia { get; set; }
        public Boolean? ExisteIntervencaoBPC { get; set; }
        public String DescricaoIntervencaoBPC { get; set; }
        public Boolean? ExisteIntervencaoAcaoJovem { get; set; }
        public String DescricaoIntervencaAcaoJovem { get; set; }
        public Boolean? ExisteOutrasArticulacoes { get; set; }
        public String DescricaoOutrasArticulacoes { get; set; }
        public Int32? AnoVigente { get; set; }
    }
}
