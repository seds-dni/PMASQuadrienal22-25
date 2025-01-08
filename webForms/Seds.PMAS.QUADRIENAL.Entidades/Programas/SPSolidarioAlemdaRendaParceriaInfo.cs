using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class SPSolidarioAlemdaRendaParceriaInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32? IdTransferenciaRenda { get; set; }
        public TransferenciaRendaInfo TransferenciaRenda { get; set; }

        [DataMember]
        public Int32 IdTipoParceria { get; set; }
        [DataMember]
        public TipoParceriaInfo TipoParceria { get; set; }
        [DataMember]
        public Int32 IdParceria { get; set; }
        [DataMember]
        public ParceriaInfo Parceria { get; set; }
        [DataMember]
        public String NomeOrgao { get; set; }

         
        [DataMember]
        public Int32? IdProgramaProjeto { get; set; }
        public ProgramaProjetoInfo ProgramaProjeto { get; set; }
    }
}
