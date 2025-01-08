using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    
    [DataContract]
    public class InformacoesCadastraisConselhoMunicipalInfo
    {        	    					
		[DataMember]
        public Int32 IdPrefeitura {get;set;}
        [DataMember]
		public string Municipio {get;set;}
        [DataMember]
		public string Drads {get;set;}        
        [DataMember]
        public String Endereco { get; set; }
        [DataMember]
        public String Bairro { get; set; }
        [DataMember]
        public String CEP { get; set; }
        [DataMember]
        public String Telefone { get; set; }
        [DataMember]
        public String Celular { get; set; }        
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public String NomePresidente { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 IdTipoConselho { get; set; }

        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }
        [DataMember]
        public String TerminoMandato { get; set; }

        [DataMember]
        public Int32 IdConselho { get; set; }
    }
}

