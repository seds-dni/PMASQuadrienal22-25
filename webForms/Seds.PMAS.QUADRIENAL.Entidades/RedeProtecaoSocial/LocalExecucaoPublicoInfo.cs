using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class LocalExecucaoPublicoInfo : LocalExecucaoInfo
    {
        [DataMember]
        public UnidadePublicaInfo Unidade { get; set; }
    }
}
