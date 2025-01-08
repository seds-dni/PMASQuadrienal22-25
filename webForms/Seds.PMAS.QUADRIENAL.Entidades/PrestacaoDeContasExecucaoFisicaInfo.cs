using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrestacaoDeContasExecucaoFisicaInfo
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdPrefeitura { get; set; }
      
        [DataMember]
        public int IdTipoProtecao { get; set; }

        [DataMember]
        public int IdServicosRecursosFinanceiros { get; set; }

        [DataMember]
        public int IdTipoBeneficioEventual { get; set; }

        [DataMember]
        public DateTime DataDeImplantacao { get; set; }

        [DataMember]
        public int NaoImplantado { get; set; }

        [DataMember]
        public Int32 QuantidadeAnualBeneficiario { get; set; }

        [DataMember]
        public Int32 QuantidadeAnualBeneficiariosConcedidos { get; set; }

        [DataMember]
        public int DemandaEstimada { get; set; }

        [DataMember]
        public int NumeroAtendidos { get; set; }

        [DataMember]
        public int Exercicio { get; set; }
    }
}
