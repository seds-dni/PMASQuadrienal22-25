using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class VigilanciaSocioAssistencialInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }

        [DataMember]
        public Boolean OfereceVigilancia { get; set; }
        [DataMember]
        public String OutraBaseDados { get; set; }        
        
        [DataMember]
        public List<BaseDadosInfo> BasesDados { get; set; }
        [DataMember]
        public List<AcaoVigilanciaSocioAssistencialInfo> Acoes { get; set; }

    }
}
