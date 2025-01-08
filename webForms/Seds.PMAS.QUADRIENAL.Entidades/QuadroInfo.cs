using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class QuadroInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 Numeracao { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 IdPai { get; set; }
        [DataMember]
        public QuadroInfo QuadroPai { get; set; }
        [DataMember]
        public Int32 IdBloco { get; set; }
        [DataMember]
        public BlocoInfo Bloco { get; set; }
    }
}
