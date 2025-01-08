using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class AcaoVigilanciaSocioAssistencialInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }

        [DataMember]
        public Int32? IdAcaoVigilanciaSocioAssistencial { get; set; }
        public AcaoVigilanciaSocioAssistencialInfo AcaoVigilanciaSocioAssistencial { get; set; }
    }
}
