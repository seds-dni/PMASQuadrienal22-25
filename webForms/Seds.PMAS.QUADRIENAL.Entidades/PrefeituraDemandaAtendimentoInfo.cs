using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrefeituraDemandaAtendimentoInfo
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdDemandaAtendimento { get; set; }
        [DataMember]
        public TipoDemandaAtendimentoInfo TipoDemandaAtendimento { get; set; }
        [DataMember]
        public Int32 QuantidadeDemanda { get; set; }

        public PrefeituraInfo Prefeitura { get; set; }
    }
}
