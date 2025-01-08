using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class ConsultaUsuariosInfo
    {        
        [DataMember]
        public Int32 IdUsuario {get;set;}
        [DataMember]
        public Int32 IdPerfil {get;set;}
        [DataMember]
        public String Perfil {get;set;}
        [DataMember]
        public String Nome {get;set;}
        [DataMember]
        public String Login {get;set;}
        [DataMember]
        public String RG {get;set;}
        [DataMember]
        public String Municipio {get;set;}
        [DataMember]
        public String Drads {get;set;}
        [DataMember]
        public Int32? IdMunicipio {get;set;}
        [DataMember]
        public Int32? IdDrads {get;set;}
        [DataMember]
        public String Situacao { get; set; }
        [DataMember]
        public String Instituicao { get; set; }
        [DataMember]
        public String Cargo { get; set; }
    }
}
