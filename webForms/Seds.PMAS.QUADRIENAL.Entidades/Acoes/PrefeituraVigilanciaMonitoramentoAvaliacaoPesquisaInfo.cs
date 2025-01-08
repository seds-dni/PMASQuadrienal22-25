using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrefeituraVigilanciaMonitoramentoAvaliacaoPesquisaInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeituraVigilanciaMonitoramentoAvaliacao { get; set; }
        public PrefeituraVigilanciaMonitoramentoAvaliacaoInfo PrefeituraVigilanciaMonitoramentoAvaliacao { get; set; }
        [DataMember]
        public String Periodo { get; set; }
        [DataMember]
        public String Objetivo { get; set; }
        [DataMember]
        public String Metodologia { get; set; }
        [DataMember]
        public String Resultados { get; set; }
    }
}
