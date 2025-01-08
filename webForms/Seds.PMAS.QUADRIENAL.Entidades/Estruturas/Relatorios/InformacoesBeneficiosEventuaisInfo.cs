using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class InformacoesBeneficiosEventuaisInfo
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Drads { get; set; }
        [DataMember]
        public String Beneficio { get; set; }
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
        public Boolean Regulamentado { get; set; }
        [DataMember]
        public String DataRegulamentacao { get; set; }

        [DataMember]
        public String PossuiLeiMunicipal { get; set; }
        [DataMember]
        public String NumeroLegislacao { get; set; }
        [DataMember]
        public String DataPublicacaoLei { get; set; }

        [DataMember]
        public String PossuiResolucao { get; set; }
        [DataMember]
        public String NumeroResolucao { get; set; }
        [DataMember]
        public String DataResolucao { get; set; }   
        

        [DataMember]
        public String PossuiDecreto { get; set; }
        [DataMember]
        public String NumeroDecreto { get; set; }
        [DataMember]
        public String DataDecreto { get; set; }



        [DataMember]
        public Int32 IdTipoBeneficio { get; set; }
        [DataMember]
        public Int32 MediaSemestralBeneficiarios { get; set; }
        [DataMember]
        public Int32 MediaSemestralBeneficiosConcedidos { get; set; }
        [DataMember]
        public String FormaAuxilio { get; set; }
        [DataMember]
        public Boolean OrgaoGestorResponsavel { get; set; }
        [DataMember]
        public Boolean CRASResponsavel { get; set; }
        [DataMember]
        public Boolean UnidadePrivadaResponsavel { get; set; }
        [DataMember]
        public Boolean CREASResponsavel { get; set; }
        [DataMember]
        public Boolean CentroPOPResponsavel { get; set; }
        [DataMember]
        public Boolean FundoSocialSolidariedadeResponsavel { get; set; }
        
        [DataMember]
        public String IntegracaoServicos { get; set; }
        [DataMember]
        public Int32 TotalServicosAssociados { get; set; }

        [DataMember]
        public Decimal ValorFMAS { get; set; }

        [DataMember]
        public Decimal ValorFundoMunicipalSolidariedade { get; set; }
        [DataMember]
        public Decimal ValorOrcamentoMunicipal { get; set; }
        [DataMember]
        public Decimal ValorFEAS { get; set; }
        [DataMember]
        public Decimal ValorReprogramacaoAnoAnterior { get; set; }
        [DataMember]
        public Decimal ValorFundoEstadualSolidariedade { get; set; }
        [DataMember]
        public Decimal ValorFNAS { get; set; }
        [DataMember]
        public Decimal ValorDemandasParlamentares { get; set; }



        public Decimal Total { get { return ValorFMAS + ValorFEAS + ValorFNAS + ValorOrcamentoMunicipal + ValorFundoMunicipalSolidariedade + ValorFundoEstadualSolidariedade + ValorDemandasParlamentares + ValorReprogramacaoAnoAnterior; } }


        public Int32 Exercicio { get; set; }
    }
}
