using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ConsultaMunicipioIndicadoresInfo
    {
        [DataMember]
        public double AreaTerritorial { get; set; }
        [DataMember]
        public string ClassificacaoIndiceFuturidade2008 { get; set; }
        [DataMember]
        public double? DensidadeDemografica2010 { get; set; }
        [DataMember]
        public int GrupoIPRS2006 { get; set; }
        [DataMember]
        public int GrupoIPRS2008 { get; set; }
        [DataMember]
        public double Habitantes2000 { get; set; }
        [DataMember]
        public double Habitantes2010 { get; set; }
        [DataMember]
        public double? IDHM2000 { get; set; }
        [DataMember]
        public double? IDHM2010 { get; set; }
        [DataMember]
        public int IdMunicipio { get; set; }
        [DataMember]
        public double? IndiceGini2000 { get; set; }
        [DataMember]
        public double? IndiceGini2010 { get; set; }
        [DataMember]
        public double PercentualGrauUrbanizacao2000 { get; set; }
        [DataMember]
        public double PercentualGrauUrbanizacao2010 { get; set; }
        [DataMember]
        public double? PercentualRendimentoMensalDomiciliarPerCapitaAte255Reais2010 { get; set; }
        [DataMember]
        public double? PercentualRendimentoMensalDomiciliarPerCapitaAte70Reais2010 { get; set; }
        [DataMember]
        public double SUAS2008 { get; set; }
        [DataMember]
        public double SUAS2010 { get; set; }
        [DataMember]
        public int? TotalFamilias2010 { get; set; }
        [DataMember]
        public int TotalFamiliasUmQuartoSm2010 { get; set; }
        [DataMember]
        public double? TotalPercentualDomiciliosComSaneamentoAdequado2010 { get; set; }
        [DataMember]
        public double? TotalPercentualFamiliasBolsaFamiliaCadUnico2010 { get; set; }
        [DataMember]
        public double? TotalPercentualFamiliasBolsaFamiliaCadUnico2012 { get; set; }
        [DataMember]
        public double TotalPercentualFamiliasUmQuartoSm2010 { get; set; }
        [DataMember]
        public double TotalPercentualIdosos { get; set; }
        [DataMember]
        public double TotalPercentualMaesAdolescentes2009 { get; set; }
        [DataMember]
        public double TotalPercentualPessoasAbaixo15Anos2010 { get; set; }
    }
}
