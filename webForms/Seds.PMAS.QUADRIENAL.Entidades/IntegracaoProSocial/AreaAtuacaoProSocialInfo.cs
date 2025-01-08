using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class AreaAtuacaoProSocialInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Descricao { get; set; }
        [DataMember]
        public String InformacoesAdicionais { get; set; }
        [DataMember]
        public Boolean Exibe { get; set; }
    }
}