using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class SPSolidarioPlanejamentoBensInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdProgramaProjeto { get; set; }
        public ProgramaProjetoInfo ProgramaProjeto { get; set; }

        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 Quantidade { get; set; }
        [DataMember]
        public Decimal EstimativaCusto { get; set; }
    }
}