using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PlanoAcaoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdProgramaProjeto { get; set; }
        public ProgramaProjetoInfo ProgramaProjeto { get; set; }
        [DataMember]
        public Boolean PossuiPlanoAcao { get; set; }
        [DataMember]
        public DateTime? DataAprovacao { get; set; }

    }
}
