using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class NivelGestaoInfo
    {
        [DataMember]
        public Int16 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
    }
}
