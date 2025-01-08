using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ConsultaDemografiaTerritorioIndicadoresInfo
    {
        [DataMember]
        public Int32 NumeroHabitantes { get; set; }
        [DataMember]
        public double? AreaTerritorial { get; set; }
        [DataMember]
        public double? DensidadeDemografica { get; set; }
        [DataMember]
        public double? TaxaGeometricaCrescimento { get; set; }
        [DataMember]
        public Int32? SaldoMigratorioAnual { get; set; }
        [DataMember]
        public double? TaxaNatalidade { get; set; }
        [DataMember]
        public double? PercentualPessoasAbaixo15Anos { get; set; }
       [DataMember]
        public double? PercentualPessoasAcima60Anos { get; set; }
        [DataMember]
        public Int32 TotalDomiciliosParticularesPermanentes { get; set; }
        [DataMember]
        public double? GrauUrbanizacao { get; set; }
        [DataMember]
        public double? TotalSaneamento { get; set; }
        [DataMember]
        public Int32 NumeroHabitantesDRADS { get; set; }
        [DataMember]
        public double? AreaTerritorialDRADS { get; set; }
        [DataMember]
        public double? DensidadeDemograficaDRADS  { get; set; }
        [DataMember]
        public double? TaxaGeometricaCrescimentoDRADS  { get; set; }
        [DataMember]
        public Int32? SaldoMigratorioAnualDRADS  { get; set; }
        [DataMember]
        public double? TaxaNatalidadeDRADS  { get; set; }
        [DataMember]
        public double? PercentualPessoasAbaixo15AnosDRADS  { get; set; }
        [DataMember]
        public double? PercentualPessoasAcima60AnosDRADS  { get; set; }
        [DataMember]
        public Int32 TotalDomiciliosParticularesPermanentesDRADS  { get; set; }
        [DataMember]
        public double? GrauUrbanizacaoDRADS  { get; set; }
        [DataMember]
        public double? TotalSaneamentoDRADS  { get; set; }

        public double? NumeroPessoasDomicilios { get; set; }

        public double? NumeroPessoasDomiciliosDRADS { get; set; }

        //,[TOTAL_DOMICILIOS_PARTICULARES_PERMANENTES]
        //,[GRAU_URBANIZACAO]
        //,[TOTAL_SANEAMENTO_ESGOTO_SANITARIO]

    }
}
