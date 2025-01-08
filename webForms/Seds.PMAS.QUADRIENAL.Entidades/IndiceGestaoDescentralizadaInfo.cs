using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados do Indice de Gestão Descentralizada
    /// </summary>
    [DataContract]
    public class IndiceGestaoDescentralizadaInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [Key]
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }

        [Key]
        [DataMember]
        public Int32 Exercicio { get; set; }

        [DataMember]
        public Double? IGDPBF { get; set; }
        [DataMember]
        public Decimal? IGDPBFValorMensal { get; set; }
        public Decimal? IGDPBFValorAnual { get { return IGDPBFValorMensal.HasValue  ? IGDPBFValorMensal * 12 : new Nullable<Decimal>(); } }

        [DataMember]
        public Double? IGDSUAS { get; set; }
        [DataMember]
        public Decimal? IGDSUASValorMensal { get; set; }
        public Decimal? IGDSUASValorAnual { get { return IGDSUASValorMensal.HasValue ? IGDSUASValorMensal * 12 : new Nullable<Decimal>(); } }
        [DataMember]
        public String ComentariosExecucaoFinanceira { get; set; }

        [DataMember]
        public String ComentariosLeiOrcamentaria { get; set; }
    }
}
