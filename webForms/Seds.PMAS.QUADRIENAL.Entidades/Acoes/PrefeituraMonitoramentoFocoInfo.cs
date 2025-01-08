using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrefeituraMonitoramentoFocoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdMonitoramento { get; set; }
        public MonitoramentoInfo Monitoramento { get; set; }

        [DataMember]
        public Int32 IdFocoMonitoramento { get; set; }
        public FocoMonitoramentoInfo FocoMonitoramento { get; set; }

        [DataMember]
        public Int32 IdPeriodicidade { get; set; }
        public PeriodicidadeInfo Periodicidade { get; set; }

        [DataMember]
        public Int32 IdTipoRede { get; set; }
        public TipoRedeInfo TipoRede { get; set; }
    }
}
