using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ConsultaIndicadoresPETIInfo
    {
        [DataMember]
        public int IdMunicipio { get; set; }
        [DataMember]
        public int? Idade1013Ano2010 { get; set; }
        [DataMember]
        public int? Idade1415Ano2010 { get; set; }
        [DataMember]
        public int? idade1617Ano2010 { get; set; }
        [DataMember]
        public int? Idade1013Ano2011 { get; set; }
        [DataMember]
        public int? Idade1415Ano2011 { get; set; }
        [DataMember]
        public int? Idade1617Ano2011 { get; set; }
        [DataMember]
        public int? Idade1013Ano2012 { get; set; }
        [DataMember]
        public int? Idade1415Ano2012 { get; set; }
        [DataMember]
        public int? Idade1617Ano2012 { get; set; }
    }
}
