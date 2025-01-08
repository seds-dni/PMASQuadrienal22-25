using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class AcaoSocioAssistencialInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
        /// <summary>
        /// Tipo da Ação : CRAS | CREAS | POP (CENTRO POP)
        /// </summary>
        public String Tipo { get; set; }
        [DataMember]
        public Int32 Ordem { get; set; }
    }
}
