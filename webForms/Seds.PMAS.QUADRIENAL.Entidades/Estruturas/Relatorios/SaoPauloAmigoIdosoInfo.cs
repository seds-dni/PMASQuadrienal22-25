using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class SaoPauloAmigoIdosoInfo
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
        public Int32 MetaPactuada { get; set; }
        [DataMember]
        public Decimal ValorPrevisaoAnualRepasse { get; set; }
        [DataMember]
        public Decimal ValorDiaIdoso { get; set; }
        [DataMember]
        public Decimal ValorConvivenciaIdoso { get; set; }

        [DataMember]
        public String PossuiParceriaFormal { get; set; }
        [DataMember]
        public Int32 TotalParcerias { get; set; }
        [DataMember]
        public String IntegracaoServicos { get; set; }
        [DataMember]
        public Int32 TotalServicosAssociados { get; set; }
    }
}
