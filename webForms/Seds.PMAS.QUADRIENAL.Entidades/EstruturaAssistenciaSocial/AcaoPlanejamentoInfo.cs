using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{    
    [DataContract]
    [Serializable]
    public class AcaoPlanejamentoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 IdEixoAcaoPlanejamento { get; set; }
        [DataMember]
        public EixoAcaoPlanejamentoInfo EixoAcaoPlanejamento { get; set; }
    }
}
