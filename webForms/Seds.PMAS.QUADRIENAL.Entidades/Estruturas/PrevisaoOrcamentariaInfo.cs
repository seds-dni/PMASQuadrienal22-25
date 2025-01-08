using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class PrevisaoOrcamentariaInfo
    {               
        [DataMember]
        public String TipoProtecao { get;set;}
        [DataMember]
        public Int16 IdTipoProtecao { get; set; }
        [DataMember]
        public Decimal RedePublicaMunicipal {get;set;}
        [DataMember]
        public Decimal RedePrivadaMunicipal {get;set;}
        [DataMember]
        public Decimal RedePublicaEstadual {get;set;}
        [DataMember]
        public Decimal RedePrivadaEstadual {get;set;}
        [DataMember]        
        public Decimal RedePublicaEstadualizado {get;set;}
        [DataMember]
        public Decimal RedePrivadaEstadualizado { get; set; }
        [DataMember]
        public Decimal RedePublicaFederal { get; set; }
        [DataMember]
        public Decimal RedePrivadaFederal { get; set; }
        [DataMember]                
        public Decimal Privado {get;set;}
        [DataMember]                
        public Int32 Exercicio { get; set; }   
    }
}
