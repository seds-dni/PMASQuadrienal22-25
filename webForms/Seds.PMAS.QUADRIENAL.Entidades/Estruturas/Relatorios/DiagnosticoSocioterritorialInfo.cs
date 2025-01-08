using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    public class DiagnosticoSocioterritorialInfo
    {
        public Int32 IdMunicipio { get; set; }

        public String Municipio { get; set; }

        public Int32 IdDrads { get; set; }

        public String Drads { get; set; }


        public Int32 NumeroHabitantes { get; set; }
        public Int32 NumeroHabitantesDrads { get; set; }
        public Double AreaTerritorial { get; set; }
        public Double AreaTerritorialDrads { get; set; }
        public Double DensidadeDemografica { get; set; }
        public Double CrescimentoAnual { get; set; }
        public Int32 SaldoMigratorioAnual { get; set; }
        public Double TaxaNatalidade { get; set; }
        public Double TaxaNatalidadeDrads { get; set; }
        public Double PessoasAbaixo15Anos { get; set; }
        public Double PessoaAbaixo15AnosDrads { get; set; }
        public Double PessoasAcima60Anos { get; set; }
        public Double PessoasAcima60AnosDrads { get; set; }
        public Int32 DomiciliosPermanentes { get; set; }
        public Int32 DomiciliosPermanentesDrads { get; set; }
        public Double GrauUrbanizacao { get; set; }
        public Double GrauUrbanizacaoDrads { get; set; }
        public Double SaneamentoBasico { get; set; }
        public Double SaneamentoBasicoDrads { get; set; }
        public Double DensidadeDemograficaDrads { get; set; }
        public Double CrescimentoAnualDrads { get; set; }
        public Int32 SaldoMigratorioAnualDrads { get; set; }
        public Int32 VersaoSistema { get; set; }
        public Int32 VersaoSistemaDrads { get; set; }

        //public Int32 DomiciliosInferior70Numero { get; set; }
        //public Int32 DomiciliosInferior70NumeroDrads { get; set; }
        //public Double DomiciliosInferior70Percentual { get; set; }
        //public Double DomiciliosInferior70PercentualDrads { get; set; }
        public Int32 DomiciliosInferiorUmQuartoNumero { get; set; }
        public Int32 DomiciliosInferiorUmQuartoNumeroDrads { get; set; }
        public Double DomiciliosInferiorUmQuartoPercentual { get; set; }
        public Double DomiciliosInferiorUmQuartoDrads { get; set; }
        public Int32 DomiciliosInferiorMetadeSalarioNumero { get; set; }
        public Int32 DomiciliosInferiorMetadeSalarioNumeroDrads { get; set; }
        public Double DomiciliosInferiorMetadeSalarioPercentual { get; set; }
        public Double DomiciliosInferiorMetadeSalarioPercentualDrads { get; set; }
        public Int32 EmpregosFormais { get; set; }
        public Int32 EmpregosFormaisDrads { get; set; }
        public Double Menores15Anos { get; set; }
        public Double Menores15AnosDrads { get; set; }
        public Int32 PessoasDeficientesNumero { get; set; }
        public Int32 PessoasDeficienteNumeroDrads { get; set; }
        public Double RazaoDependencia { get; set; }
        public Double RazaoDependenciaDrads { get; set; }
        public String IRPS2010 { get; set; }
        public String IRPS2010Drads { get; set; }
        public String IRPS2012 { get; set; }
        public String IRPS2012Drads { get; set; }
        public Double GINI2000 { get; set; }
        public Double GINI2000Drads { get; set; }
        public Double GINI2010 { get; set; }
        public Double GINI2010Drads { get; set; }
        public Double IPVSGrupo5 { get; set; }
        public Double IPVSGrupo5Drads { get; set; }
        public Double IPVSGrupo6 { get; set; }
        public Double IPVSGrupo6Drads { get; set; }
        public Double IPVSGrupo7 { get; set; }
        public Double IPVSGrupo7Drads { get; set; }
        public Double? PessoasDeficiencias { get; set; }
        public Double? PessoasDeficienciasDrads { get; set; }

    }
}
