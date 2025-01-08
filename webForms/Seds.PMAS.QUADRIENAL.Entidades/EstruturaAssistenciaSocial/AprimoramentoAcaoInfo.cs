using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class AprimoramentoAcaoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
    }
}
