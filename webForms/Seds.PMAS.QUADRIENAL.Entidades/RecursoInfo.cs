using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class RecursoInfo
    {
        [DataMember]
        public Int32 Id {get;set;}        
        [DataMember]
        public String Nome {get;set;}
        [DataMember]
        public String Pagina {get;set;}
        [DataMember]
        public Int32? IdPai {get;set;}
        [DataMember]
        public Int32 Ordem {get;set;}
        [IgnoreDataMember]
        public List<RecursoPerfilInfo> Perfis { get; set; }

        [DataMember]
        public Int32 RefBloqueio { get; set; }

    }
}
