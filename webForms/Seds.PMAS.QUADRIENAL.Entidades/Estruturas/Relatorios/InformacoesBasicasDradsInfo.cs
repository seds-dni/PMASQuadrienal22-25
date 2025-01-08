using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class InformacoesBasicasDradsInfo
    {
        [DataMember]
        public String Drads { get; set; }
        [DataMember]
        public Int32 Habitantes { get; set; }
        [DataMember]
        public Int32 CRASImplantados { get; set; }
        [DataMember]
        public Int32 CRASPrevistos { get; set; }
        [DataMember]
        public Int32 CREASImplantados { get; set; }
        [DataMember]
        public Int32 CREASPrevistos { get; set; }
        [DataMember]
        public Int32 CentroPOPImplantados { get; set; }
        [DataMember]
        public Int32 CentroPOPPrevistos { get; set; }

        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }        
    }
}
