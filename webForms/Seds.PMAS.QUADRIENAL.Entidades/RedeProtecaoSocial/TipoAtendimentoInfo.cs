using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class TipoAtendimentoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String TipoAtendimento { get; set; }
    }
}
