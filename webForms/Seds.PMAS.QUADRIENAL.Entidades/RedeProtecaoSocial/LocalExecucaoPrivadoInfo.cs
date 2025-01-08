using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class LocalExecucaoPrivadoInfo : LocalExecucaoInfo
    {
        [DataMember]
        public UnidadePrivadaInfo Unidade { get; set; }
        [DataMember]
        public Boolean PossuiTaxasTributos { get; set; }
        [DataMember]
        public Boolean PossuiCessaoImoveis { get; set; }
        [DataMember]
        public Boolean PossuiTributoFederal { get; set; }
        [DataMember]
        public Boolean PossuiTributoEstadual { get; set; }
        [DataMember]
        public Boolean PossuiTributoMunicipal { get; set; }

    }
}
