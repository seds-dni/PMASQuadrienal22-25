using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class UnidadeExecutoraInfo
    {
        [DataMember]
        public Int32 IdUnidadeExecutora { get; set; }
        [DataMember]
        public String Nome { get; set; }
    }
}
