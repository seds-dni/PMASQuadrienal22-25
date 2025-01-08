using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ProgramaProjetoPrestacaoContasInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        [DataMember]
        public String Nome { get; set; }

        [DataMember]
        public int DemandaEstimada { get; set; }

        [DataMember]
        public int NumeroAtendidos { get; set; }

        [DataMember]
        public Decimal CofinanciamentoEstadual { get; set; }

        [DataMember]
        public Decimal RecursosReprogramadosAnoAnterior { get; set; }

        [DataMember]
        public Decimal ValorAplicacoesFinanceiras { get; set; }

        [DataMember]
        public Decimal MaterialDeConsumo { get; set; }

        [DataMember]
        public Decimal OutrasDespesas { get; set; }

        [DataMember]
        public Decimal RecursosHumanos { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }
    }
}
