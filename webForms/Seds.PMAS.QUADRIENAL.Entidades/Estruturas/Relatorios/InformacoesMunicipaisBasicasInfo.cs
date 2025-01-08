using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_INFORMACOES_MUNICIPAIS_BASICAS]
    /// </summary>
    [DataContract]
    public class InformacoesMunicipaisBasicasInfo
    {        	    					
		[DataMember]
        public Int32 Id {get;set;}
        [DataMember]
        public Int32 IBGE { get; set; }
        [DataMember]
		public string Municipio {get;set;}
        [DataMember]
		public string Drads {get;set;}
        [DataMember]
		public string Situacao {get;set;}
        [DataMember]
		public string NivelGestao {get;set;}
        [DataMember]
		public string Porte {get;set;}
        [DataMember]
		public Int32 Habitantes {get;set;}
        [DataMember]
        public Int32 CRASImplantados { get; set; }
        [DataMember]
        public Int32 CRASPrevistos { get; set; }
        [DataMember]
		public DateTime? CRASProximaInstalacao {get;set;}
        [DataMember]
        public Int32 CREASImplantados { get; set; }
        [DataMember]
        public Int32 CREASPrevistos { get; set; }
        [DataMember]
        public DateTime? CREASProximaInstalacao { get; set; }
        [DataMember]
        public Int32 CentroPOPImplantados { get; set; }
        [DataMember]
        public Int32 CentroPOPPrevistos { get; set; }
        [DataMember]
        public DateTime? CentroPOPProximaInstalacao { get; set; }

        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
		public Int32 IdRegiaoMetropolitana {get;set;}
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }		
    }
}

