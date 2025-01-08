using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class LogInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }
        [DataMember]
        public Int32 Revisao { get; set; }
        [DataMember]
        public Int32 IdAcao { get; set; }
        public EAcao EnumAcao { get { return (EAcao)IdAcao; } set { IdAcao = Convert.ToInt32(value); } }
        [DataMember]
        public String Descricao { get; set; }
        [DataMember]
        public Int32 IdUsuario { get; set; }
        [DataMember]
        public DateTime DataHorario { get; set; }
        [DataMember]
        public Int32 IdQuadro { get; set; }
        [DataMember]
        public QuadroInfo Quadro { get; set; }
        [DataMember]
        public Int32? IdForeignKey { get; set; }
        [DataMember]
        public Int32? IdForeignKeyPai { get; set; }
    }
}
