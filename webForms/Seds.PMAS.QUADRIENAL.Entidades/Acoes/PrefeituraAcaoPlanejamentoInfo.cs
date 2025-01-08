using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{    
    [DataContract]
    [Serializable]
    public class PrefeituraAcaoPlanejamentoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }

        [DataMember]
        public Int32 IdAcaoPlanejamento { get; set; }
        [DataMember]
        public AcaoPlanejamentoInfo AcaoPlanejamento { get; set; }

        [DataMember]
        public String Objetivos { get; set; }
        [DataMember]
        public String Descricao { get; set; }
        [DataMember]
        public String OutrosEnvolvidos { get; set; }

        [DataMember]
        public Boolean FonteFMAS { get; set; }
        [DataMember]
        public Decimal? ValorFMAS { get; set; }
        [DataMember]
        public Boolean FonteOrcamentoMunicipal { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoMunicipal { get; set; }
        [DataMember]
        public Boolean FonteOutrosFundosMunicipais { get; set; }
        [DataMember]
        public Decimal? ValorOutrosFundosMunicipais { get; set; }
        [DataMember]
        public Boolean FonteFEAS { get; set; }
        [DataMember]
        public Decimal? ValorFEAS { get; set; }
        [DataMember]
        public Boolean FonteOrcamentoEstadual { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoEstadual { get; set; }
        [DataMember]
        public Boolean FonteOutrosFundosEstaduais { get; set; }
        [DataMember]
        public Decimal? ValorOutrosFundosEstaduais { get; set; }
        [DataMember]
        public Boolean FonteFNAS { get; set; }
        [DataMember]
        public Decimal? ValorFNAS { get; set; }
        [DataMember]
        public Boolean FonteOrcamentoFederal { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoFederal { get; set; }
        [DataMember]
        public Boolean FonteOutrosFundosFederais { get; set; }
        [DataMember]
        public Decimal? ValorOutrosFundosFederais { get; set; }
        [DataMember]
        public Boolean FonteIGDPBF { get; set; }
        [DataMember]
        public Decimal? ValorIGDPBF { get; set; }
        [DataMember]
        public Boolean FonteIGDSUAS { get; set; }
        [DataMember]
        public Decimal? ValorIGDSUAS { get; set; }

         
        [DataMember]
        public Decimal? ValorEstimativaCusto { get; set; }
        [DataMember]
        public Int32? MesPrevistoInicio { get; set; }
        [DataMember]
        public Int32? AnoPrevistoInicio { get; set; }
        [DataMember]
        public Int32? MesPrevistoTermino { get; set; }
        [DataMember]
        public Int32? AnoPrevistoTermino { get; set; }

        [DataMember]
        public int Situacao { get; set; }
        public String SituacaoComentario { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }
    }
}
