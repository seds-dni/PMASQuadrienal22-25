using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public abstract class RecursosHumanosInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 SemEscolarizacao { get; set; }
        [DataMember]
        public Int32 NivelFundamental { get; set; }
        [DataMember]
        public Int32 NivelMedio { get; set; }
        [DataMember]
        public Int32 NivelSuperior { get; set; }
        [DataMember]
        public Int32 ServicoSocial { get; set; }
        [DataMember]
        public Int32 Psicologia { get; set; }
        [DataMember]
        public Int32 Pedagogia { get; set; }
        [DataMember]
        public Int32 Sociologia { get; set; }
        [DataMember]
        public Int32 TerapiaOcupacional { get; set; }
        [DataMember]
        public Int32 Direito { get; set; }
        [DataMember]
        public Int32 Antropologia { get; set; }
        [DataMember]
        public Int32 Economia { get; set; }
        [DataMember]
        public Int32 Musicoterapia { get; set; }
        [DataMember]
        public Int32 EconomiaDomestica { get; set; }
        [DataMember]
        public Int32 PosGraduacao { get; set; }
        [DataMember]
        public Int32 Estagiarios { get; set; }
        [DataMember]
        public Int32 Voluntarios { get; set; }
        [DataMember]
        public Int32 ExclusivoServico { get; set; }
        [DataMember]
        public Int32 OutrosServicosAssistenciais { get; set; }
    }
}
