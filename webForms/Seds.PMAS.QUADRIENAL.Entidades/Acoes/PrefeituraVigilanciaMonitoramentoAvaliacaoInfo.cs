using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrefeituraVigilanciaMonitoramentoAvaliacaoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }
        [DataMember]
        public Boolean PossuiSistemaInformatizadoProprio { get; set; }
        [DataMember]
        public Boolean PossuiAdesaoMse { get; set; }
        [DataMember]
        public String DetalhamentoNaoAdesaoMse { get; set; }
        [DataMember]
        public List<PrefeituraVigilanciaMonitoramentoAvaliacaoPesquisaInfo> Pesquisas { get; set; }
        [DataMember]
        public List<AprimoramentoAcaoInfo> Aprimoramentos { get; set; }
    }
}
