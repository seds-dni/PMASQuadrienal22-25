using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public enum ETipoUnidade : int
    {
        [EnumMember]
        Publica = 1,
        [EnumMember]
        Privada = 2,
        [EnumMember]
        CRAS = 3,
        [EnumMember]
        CREAS = 4,
        [EnumMember]
        CentroPOP = 5

    }
}
