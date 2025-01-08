using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class SaoPauloSolidarioInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
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
        public String BuscaAtivaInicio { get; set; }
        [DataMember]
        public String BuscaAtivaTermino { get; set; }        

        [DataMember]
        public String BuscaAtivaOrgaoGestorExecuta { get; set; }
        [DataMember]
        public String BuscaAtivaCRASExecuta { get; set; }
        [DataMember]
        public String BuscaAtivaCREASExecuta { get; set; }
        [DataMember]
        public String BuscaAtivaUnidadePrivadaExecuta { get; set; }

        [DataMember]
        public Decimal BuscaAtivaValorFMAS { get; set; }
        [DataMember]
        public Decimal BuscaAtivaValorOrcamentoMunicipal { get; set; }
        [DataMember]
        public Decimal BuscaAtivaValorFundoMunicipal { get; set; }
        [DataMember]
        public Decimal BuscaAtivaValorFEAS { get; set; }
        [DataMember]
        public Decimal BuscaAtivaValorOrcamentoEstadual { get; set; }
        [DataMember]
        public Decimal BuscaAtivaValorFundoEstadual { get; set; }
        [DataMember]
        public Decimal BuscaAtivaValorFNAS { get; set; }
        [DataMember]
        public Decimal BuscaAtivaValorOrcamentoFederal { get; set; }
        [DataMember]
        public Decimal BuscaAtivaValorFundoFederal { get; set; }
        [DataMember]
        public Decimal BuscaAtivaValorIGDPBF { get; set; }
        [DataMember]
        public Decimal BuscaAtivaValorIGDSUAS { get; set; }

        [DataMember]
        public String AgendaFamiliaOrgaoGestorExecuta { get; set; }
        [DataMember]
        public String AgendaFamiliaCRASExecuta { get; set; }
        [DataMember]
        public String AgendaFamiliaCREASExecuta { get; set; }

        [DataMember]
        public Int32 AgendaFamiliaNumeroFamilias2012 { get; set; }
        [DataMember]
        public Int32 AgendaFamiliaNumeroFamilias2013 { get; set; }
        [DataMember]
        public Int32 AgendaFamiliaNumeroFamilias2014 { get; set; }
        [DataMember]
        public Int32 AgendaFamiliaNumeroFamiliasTotal { get { return AgendaFamiliaNumeroFamilias2012 + AgendaFamiliaNumeroFamilias2013 + AgendaFamiliaNumeroFamilias2014; } }

        [DataMember]
        public Decimal AgendaFamiliaValorFMAS { get; set; }
        [DataMember]
        public Decimal AgendaFamiliaValorOrcamentoMunicipal { get; set; }
        [DataMember]
        public Decimal AgendaFamiliaValorFundoMunicipal { get; set; }
        [DataMember]
        public Decimal AgendaFamiliaValorFEAS { get; set; }
        [DataMember]
        public Decimal AgendaFamiliaValorOrcamentoEstadual { get; set; }
        [DataMember]
        public Decimal AgendaFamiliaValorFundoEstadual { get; set; }
        [DataMember]
        public Decimal AgendaFamiliaValorFNAS { get; set; }
        [DataMember]
        public Decimal AgendaFamiliaValorOrcamentoFederal { get; set; }
        [DataMember]
        public Decimal AgendaFamiliaValorFundoFederal { get; set; }
        [DataMember]
        public Decimal AgendaFamiliaValorIGDPBF { get; set; }
        [DataMember]
        public Decimal AgendaFamiliaValorIGDSUAS { get; set; }

        [DataMember]
        public String PossuiParceriaFormal { get; set; }
        [DataMember]
        public Int32 TotalParcerias { get; set; }

        [DataMember]
        public String PossuiParceriaFormalAgendaFamilia { get; set; }
        [DataMember]
        public Int32 TotalParceriasAgendaFamilia { get; set; }
        [DataMember]
        public String IntegracaoServicos { get; set; }
        [DataMember]
        public Int32 TotalServicosAssociados { get; set; }
    }
}
