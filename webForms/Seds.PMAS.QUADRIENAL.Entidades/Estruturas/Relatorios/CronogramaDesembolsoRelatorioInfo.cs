using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    //[VW_RELATORIO_CRONOGRAMA_DESEMBOLSO]
    [DataContract]
    [Serializable]
    public class CronogramaDesembolsoRelatorioInfo
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
                
        [DataMember]
        public Decimal RecursosHumanosPublica { get; set; }

        [DataMember]
        public Decimal CusteioPublica { get; set; }

        [DataMember]
        public Decimal InvestimentoPublica { get; set; }

        [DataMember]
        public Decimal RecursosHumanos { get; set; }

        [DataMember]
        public Decimal CusteioPrivada { get; set; }

        [DataMember]
        public Decimal InvestimentoPrivada { get; set; }

        [DataMember]
        public Decimal RecursosHumanosPublicaReprogramado { get; set; }

        [DataMember]
        public Decimal CusteioPublicaReprogramado { get; set; }
        
        [DataMember]
        public Decimal InvestimentoPublicaReprogramado { get; set; }
        
        [DataMember]
        public Decimal RecursosHumanosReprogramado { get; set; }
        
        [DataMember]
        public Decimal CusteioPrivadaReprogramado { get; set; }
        
        [DataMember]
        public Decimal InvestimentoPrivadaReprogramado { get; set; }
        
       
        public Int32 Exercicio { get; set; }

        public Decimal Total { get { return RecursosHumanos + CusteioPrivada + InvestimentoPrivada + CusteioPublica + InvestimentoPublica + RecursosHumanosPublicaReprogramado + CusteioPublicaReprogramado + InvestimentoPublicaReprogramado + RecursosHumanosReprogramado + CusteioPrivadaReprogramado + InvestimentoPrivadaReprogramado; } }
    }


}

