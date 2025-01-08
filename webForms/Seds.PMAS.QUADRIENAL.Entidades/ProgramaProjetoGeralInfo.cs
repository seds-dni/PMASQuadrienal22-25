using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ProgramaProjetoGeralInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
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
        public string Beneficiarios { get; set; }
        [DataMember]
        public Int32 MetaPactuada { get; set; }

        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public String NivelAbrangencia { get; set; }
        [DataMember]
        public String DataInicio { get; set; }
        [DataMember]
        public Int32 TotalServicosAssociados { get; set; }
        [DataMember]
        public String PossuiParceriaFormal { get; set; }
        [DataMember]
        public Int32 QuantidadeParcerias { get; set; }

        [DataMember]
        public Decimal ValorFMAS { get; set; }
        [DataMember]
        public Decimal ValorOrcamentoMunicipal { get; set; }
        [DataMember]
        public Decimal ValorFundoMunicipal { get; set; }
        [DataMember]
        public Decimal ValorFEAS { get; set; }
        [DataMember]
        public Decimal ValorOrcamentoEstadual { get; set; }
        [DataMember]
        public Decimal ValorFundoEstadual { get; set; }
        [DataMember]
        public Decimal ValorFNAS { get; set; }
        [DataMember]
        public Decimal ValorOrcamentoFederal { get; set; }
        [DataMember]
        public Decimal ValorFundoFederal { get; set; }
        [DataMember]
        public Decimal ValorIGDPBF { get; set; }
        [DataMember]
        public Decimal ValorIGDSUAS { get; set; }
        [DataMember]
        public Decimal ValorTotalRecursos { get; set; }
         [DataMember]
        public Int32 Aderiu { get; set; }
         [DataMember]
         public bool Ativo { get; set; }
    }
}