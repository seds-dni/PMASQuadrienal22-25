using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PETIAcaoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdTransferenciaRenda { get; set; }
        [IgnoreDataMember]
        public TransferenciaRendaInfo TransferenciaRenda { get; set; }

        [DataMember]
        public Int32 IdPETIEixoAtuacao { get; set; }
        [IgnoreDataMember]
        public PETIEixoAtuacaoInfo PETIEixoAtuacao { get; set; }

        [DataMember]
        public Int32 IdPETITipoAcao { get; set; }
        [IgnoreDataMember]
        public PETITipoAcaoInfo PETITipoAcao { get; set; }

        [DataMember]
        public String PeriodoRealizacao { get; set; }

        [DataMember]
        public Int32? IdPETISituacaoAcao { get; set; }
        [IgnoreDataMember]
        public PETISituacaoAcaoInfo PETISituacaoAcao { get; set; }

        [IgnoreDataMember]
        public Int32 ListViewIndex { get; set; }
    }
}