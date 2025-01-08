using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class ConsultaLogInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }        
        [DataMember]
        public Int32 Revisao { get; set; }
        [DataMember]
        public Int32 IdAcao { get; set; }
        [DataMember]
        public String Acao { get; set; }
        [DataMember]
        public String Descricao { get; set; }
        [DataMember]
        public Int32 IdUsuario { get; set; }
        [DataMember]
        public String Usuario { get; set; }
        [DataMember]
        public DateTime DataHorario { get; set; }
        [DataMember]
        public Int32 IdQuadro { get; set; }
        [DataMember]
        public String Quadro { get; set; }
        [DataMember]
        public Int32? IdForeignKey { get; set; }
        [DataMember]
        public Int32? IdForeignKeyPai { get; set; }
    }
}
