using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class ConsultaPlanoMunicipalHistoricoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }                
        [DataMember]
        public Int32 Revisao { get; set; }

        [DataMember]
        public Int32 IdUsuario { get; set; }
        [DataMember]
        public String Usuario { get; set; }
        [DataMember]
        public String Descricao {get;set;}
        [DataMember]
        public DateTime Data { get; set; }

        [DataMember]
        public Int32 IdSituacao { get; set; }
        [DataMember]
        public String Situacao { get; set; }
        [DataMember]
        public String Comentarios { get; set; }
    }
}
