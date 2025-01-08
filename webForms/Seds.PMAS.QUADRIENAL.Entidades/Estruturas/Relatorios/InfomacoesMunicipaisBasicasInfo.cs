using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS2013.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_INFORMACOES_MUNICIPAIS_BASICAS]
    /// </summary>
    public class InfomacoesMunicipaisBasicasInfo
    {        	    					
		public Int32 Id {get;set;}		
		public string Municipio {get;set;}		
		public string DRADS {get;set;}		
		public string Situacao {get;set;}		
		public string NivelGestao {get;set;}		
		public string Porte {get;set;}		
		public Int32 Habitantes {get;set;}		
		public int CRASImplantados {get;set;}
        public int CRASPrevistos {get;set;}		
		public DateTime? CRASProximaInstalacao {get;set;}
        public int CREASImplantados { get; set; }
        public int CREASPrevistos { get; set; }
        public DateTime? CREASProximaInstalacao { get; set; }
        public int CentroPOPImplantados { get; set; }
        public int CentroPOPPrevistos { get; set; }
        public DateTime? CentroPOPProximaInstalacao { get; set; }
			
		public int IdDrads {get;set;}						
		public int IdMunicipio {get;set;}		
		public Int32 IdRegiaoMetropolitana {get;set;}		
		public int IdMacroRegiao {get;set;}		
		public int IdNivelGestao {get;set;}		
		public int IdPorte {get;set;}		
    }
}
