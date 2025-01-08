using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class LogCreasInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdLog { get; set; }

        [DataMember]
        public Int32 IdCreas { get; set; }

        [DataMember]
        public Int32 IdUnidade { get; set; }

        [DataMember]
        public DateTime DataCriacao { get; set; }
    }
}
