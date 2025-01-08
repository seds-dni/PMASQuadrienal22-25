using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ProgramaProjetoRecursoFinanceiroInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [Key]
        [DataMember]
        public Int32 IdProgramaProjeto { get; set; }
        [Key]
        [DataMember]
        public Int32 Exercicio { get; set; }

        [DataMember]
        public Boolean? FonteFMAS { get; set; }
        [DataMember]
        public Decimal? ValorFMAS { get; set; }
        [DataMember]
        public Boolean? FonteOrcamentoMunicipal { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoMunicipal { get; set; }
        [DataMember]
        public Boolean? FonteFundoMunicipal { get; set; }
        [DataMember]
        public Decimal? ValorFundoMunicipal { get; set; }
        [DataMember]
        public Boolean? FonteFEAS { get; set; }
        [DataMember]
        public Decimal? ValorFEAS { get; set; }
        //[DataMember]
        //public Decimal? ValorFEASSegunda { get; set; }
        [DataMember]
        public Boolean? FonteOrcamentoEstadual { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoEstadual { get; set; }
        [DataMember]
        public Boolean? FonteFundoEstadual { get; set; }
        [DataMember]
        public Decimal? ValorFundoEstadual { get; set; }
        [DataMember]
        public Boolean? FonteFNAS { get; set; }
        [DataMember]
        public Decimal? ValorFNAS { get; set; }
        [DataMember]
        public Boolean? FonteOrcamentoFederal { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoFederal { get; set; }
        [DataMember]
        public Boolean? FonteFundoFederal { get; set; }
        [DataMember]
        public Decimal? ValorFundoFederal { get; set; }
        [DataMember]
        public Boolean? FonteIGDPBF { get; set; }
        [DataMember]
        public Decimal? ValorIGDPBF { get; set; }
        [DataMember]
        public Boolean? FonteIGDSUAS { get; set; }
        [DataMember]
        public Decimal? ValorIGDSUAS { get; set; }

        public bool Desbloqueado { get; set; }

        public ProgramaProjetoInfo ProgramaProjeto { get; set; }
    }
}
