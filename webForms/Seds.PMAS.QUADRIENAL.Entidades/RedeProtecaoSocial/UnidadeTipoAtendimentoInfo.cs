using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class UnidadeTipoAtendimentoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public string Nome { get; set; }
    }
}
