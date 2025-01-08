using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class MetaPrevistaProgramasTransferenciasInfo
    {
        [DataMember]
        public PrefeitoInfo prefeitura { get; set; }
        [DataMember]
        public ProgramaProjetoInfo programa { get; set; }
        [DataMember]
        public int metaPrevista { get; set; }
    }
}
