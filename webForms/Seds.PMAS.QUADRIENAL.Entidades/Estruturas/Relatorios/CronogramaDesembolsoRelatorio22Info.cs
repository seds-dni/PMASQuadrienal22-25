using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;



namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
	[DataContract]
    [Serializable]
    public class CronogramaDesembolsoRelatorio22Info
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        
        [DataMember]
        
        public string Municipio { get; set; }
        
        [DataMember]
        public string Drads { get; set; }
        
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
        public Int16 IdTipoProtecao { get; set; }
        
        [DataMember]
        public string TipoProtecao { get; set; }
                
        [Required]
        [DataMember]
        public Decimal CusteioPublica { get; set; }

        [Required]
        [DataMember]
        public Decimal CusteioPrivada { get; set; } 

        [DataMember]
        public Decimal CusteioPublicaReprogramado { get; set; }

        [DataMember]
        public Decimal CusteioPrivadaReprogramado { get; set; }

        [DataMember]
        public Decimal RecursosHumanosPublica { get; set; }

        [DataMember]
        public Decimal RecursosHumanos { get; set; }
		
        [DataMember]
        public Decimal RecursosHumanosPublicaReprogramado { get; set; }
        
        [DataMember]
        public Decimal RecursosHumanosReprogramado { get; set; }
       
      
        [DataMember]
        public Decimal InvestimentoAquisicaoDeEquipamentosPrivado { get; set; } 

        [DataMember]
        public Decimal InvestimentoAquisicaoDeEquipamentosPublico { get; set; } 

        [DataMember]
        public Decimal InvestimentoEquipamentosPrivadoReprogramado { get; set; } 

        [DataMember]
        public Decimal InvestimentoEquipamentosPublicoReprogramado { get; set; } 


        [DataMember]
        public Decimal InvestimentoObrasPrivado { get; set; } 

        [DataMember]
        public Decimal InvestimentoObrasPublico { get; set; } 

        [DataMember]
        public Decimal InvestimentosObrasReprogramadoPublico { get; set; } 

        [DataMember]
        public Decimal InvestimentosObrasReprogramadoPrivado { get; set; }         
       
        public Int32 Exercicio { get; set; }

        public Decimal Total { get { return RecursosHumanosPublica + RecursosHumanos + CusteioPrivada + InvestimentoObrasPrivado + CusteioPublica + InvestimentoObrasPublico + RecursosHumanosPublicaReprogramado + CusteioPublicaReprogramado + InvestimentosObrasReprogramadoPublico + RecursosHumanosReprogramado + CusteioPrivadaReprogramado + InvestimentosObrasReprogramadoPrivado + InvestimentoAquisicaoDeEquipamentosPrivado + InvestimentoAquisicaoDeEquipamentosPublico + InvestimentoEquipamentosPrivadoReprogramado + InvestimentoEquipamentosPublicoReprogramado; } }
    }
}
