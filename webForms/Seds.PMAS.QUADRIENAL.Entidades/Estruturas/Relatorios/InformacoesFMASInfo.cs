using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class InformacoesFMASInfo
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
        public string Lei { get; set; }
        [DataMember]
        public string Regulamentado { get; set; }
        [DataMember]
        public string Decreto { get; set; }
        [DataMember]
        public string Orcamentaria { get; set; }

        [DataMember]
        public string CNPJ { get; set; }
        [DataMember]
        public string CondicaoCNPJ { get; set; }
        [DataMember]
        public string NomeGestor { get; set; }

        [DataMember]
        public Decimal ValorFMAS { get; set; }
        [DataMember]
        public Decimal ValorFEAS { get; set; }
        [DataMember]
        public Decimal ValorFNAS { get; set; }
        [DataMember]
        public Decimal ValorNaoAlocadoFMAS { get; set; }
        [DataMember]
        public Decimal ValorRecursosFMAS { get; set; }
        
        
        
        
        [DataMember]
        public int? EXERCICIO { get; set; }
        [DataMember]
        public String TipoGestor { get; set; }
        public Decimal TotalRecursosFMAS { get { return ValorNaoAlocadoFMAS + ValorRecursosFMAS; } }
        public Decimal Total { get { return ValorFMAS + ValorFEAS + ValorFNAS; } }

    }
}
