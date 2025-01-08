using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_DISTRIBUICAO_ESTADUAL_PROGRAMA_TRABALHO]
    /// </summary>
    [DataContract]
    public class DistribuicaoEstadualProgramaTrabalhoInfo
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Drads { get; set; }
        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }



        [DataMember]
        public Decimal ValorProtecaoSocialBasica { get; set; }
        [DataMember]
        public Decimal ValorProtecaoSocialMedia { get; set; }
        [DataMember]
        public Decimal ValorProtecaoSocialAlta { get; set; }
        [DataMember]
        public Decimal ValorBeneficiosEventuais { get; set; }
        [DataMember]
        public Decimal ValorSPSolidario
        { get; set; }


        [DataMember]
        public Decimal ValorProtecaoSocialBasicaReprogramado { get; set; }
        [DataMember]
        public Decimal ValorProtecaoSocialMediaReprogramado { get; set; }
        [DataMember]
        public Decimal ValorProtecaoSocialAltaReprogramado { get; set; }
        [DataMember]
        public Decimal ValorBeneficiosEventuaisReprogramado { get; set; }
        [DataMember]
        public Decimal ValorSPSolidarioReprogramado
        { get; set; }

        [DataMember]
        public Int32 NumeroAtendidosAnualBeneficios { get; set; }
        [DataMember]
        public Int32 NumeroAtendidosAnualBasica { get; set; }
        [DataMember]
        public Int32 NumeroAtendidosAnualMedia { get; set; }
        [DataMember]
        public Int32 NumeroAtendidosAnualAlta { get; set; }


        public Decimal SubTotalValorProtecaoSocialBasica { get { return ValorProtecaoSocialBasica + ValorProtecaoSocialBasicaReprogramado; } }

        public Decimal SubTotalValorProtecaoSocialMedia { get { return ValorProtecaoSocialMedia + ValorProtecaoSocialMediaReprogramado; } }

        public Decimal SubTotalValorProtecaoSocialAlta { get { return ValorProtecaoSocialAlta + ValorProtecaoSocialAltaReprogramado; } }

        public Decimal SubTotalValorSPSolidario { get { return ValorSPSolidario + ValorSPSolidarioReprogramado; } }

        public Decimal SubTotalValorBeneficiosEventuais { get { return ValorBeneficiosEventuais + ValorBeneficiosEventuaisReprogramado; } }

        public Decimal TotalExercicioAtual { get { return ValorProtecaoSocialBasica + ValorProtecaoSocialMedia + ValorProtecaoSocialAlta + ValorBeneficiosEventuais + ValorSPSolidario; } }

        public Decimal TotalReprogramado { get { return ValorProtecaoSocialBasicaReprogramado + ValorProtecaoSocialMediaReprogramado + ValorProtecaoSocialAltaReprogramado + ValorBeneficiosEventuaisReprogramado + ValorSPSolidarioReprogramado; } }

        public Decimal Total { get { return SubTotalValorProtecaoSocialBasica + SubTotalValorProtecaoSocialMedia + SubTotalValorProtecaoSocialAlta + SubTotalValorSPSolidario + SubTotalValorBeneficiosEventuais; } }

        public Int32 TotalPrevisaoAnualAtendidos { get { return NumeroAtendidosAnualBeneficios + NumeroAtendidosAnualBasica + NumeroAtendidosAnualMedia + NumeroAtendidosAnualAlta; } }
        //public Decimal Total { get { return ValorBeneficiosEventuais + ValorProtecaoSocialBasica + ValorProtecaoSocialMedia + ValorProtecaoSocialAlta + ValorSPSolidario
        //                                  + ValorProtecaoSocialBasicaReprogramado + ValorProtecaoSocialMediaReprogramado + ValorProtecaoSocialAltaReprogramado + ValorBeneficiosEventuaisReprogramado + ValorSPSolidarioReprogramado; } }
        [DataMember]
        public int? Exercicio { get; set; }
        [DataMember]
        public int? IdHistorico { get; set; }

    }
}

