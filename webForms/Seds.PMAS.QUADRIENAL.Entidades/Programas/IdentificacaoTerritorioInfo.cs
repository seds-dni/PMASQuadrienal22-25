using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class IdentificacaoTerritorioInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdProgramaProjeto { get; set; }
        public ProgramaProjetoInfo ProgramaProjeto { get; set; }
        [DataMember]
        public String IdentificacaoTerritorio { get; set; }
        [DataMember]
        public String NomeResponsavel { get; set; }
        [DataMember]
        public Int32 NumeroBeneficiarios { get; set; }
        [DataMember]
        public Int32? NumeroIdentificacao { get; set; }

    }
}
