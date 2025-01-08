using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Acoes
{
    [DataContract]
    [Serializable]
    public class RecursosPrefeituraAcaoPlanejamentoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeituraAcaoPlanejamento { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }

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
        public Boolean FonteFEASReprogramado { get; set; }
        [DataMember]
        public Decimal? ValorFEASReprogramado { get; set; }

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
        public Int32 Exercicio { get; set; }
    }
}
