using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class LogCrasInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdLog { get; set; }

        [DataMember]
        public Int32 IdCras { get; set; }

        [DataMember]
        public Int32 IdUnidade { get; set; }

        [DataMember]
        public DateTime DataCriacao { get; set; }
    }
}
