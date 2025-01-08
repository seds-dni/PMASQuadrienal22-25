using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class SaoPauloSolidario2016Info
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }        
        [DataMember]
        public Int32 IdTipoTransferenciaRenda { get; set; }        
        [DataMember]
        public Int32 IdUsuarioTransferenciaRenda { get; set; }        
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public String Objetivo { get; set; }
        [DataMember]
        public Boolean BeneficiarioAtendidoRedeSocioAssistencial { get; set; }        

        [DataMember]
        public Int32? IdFasePrograma { get; set; }
        [DataMember]
        public Int32? MesInicioBuscaAtiva { get; set; }
        [DataMember]
        public Int32? AnoInicioBuscaAtiva { get; set; }
        [DataMember]
        public Int32? MesTerminoBuscaAtiva { get; set; }
        [DataMember]
        public Int32? AnoTerminoBuscaAtiva { get; set; }

        [DataMember]
        public Boolean? OrgaoGestorExecutaBuscaAtiva { get; set; }
        [DataMember]
        public Boolean? CRASExecutaBuscaAtiva { get; set; }
        [DataMember]
        public Boolean? CREASExecutaBuscaAtiva { get; set; }
        [DataMember]
        public Boolean? UnidadePrivadaExecutaBuscaAtiva { get; set; }

        [DataMember]
        public Decimal? ValorFMASBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoMunicipalBuscaAtiva { get; set; }
        [DataMember]
        public Boolean? ValorFEASRetidoFMAS2014 { get; set; }
        [DataMember]
        public Int32? MesRepasseFEASBuscaAtiva { get; set; }
        [DataMember]
        public Int32? AnoRepasseFEASBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? ValorFEASBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? ValorFNASBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? ValorIGDPBFBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? ValorIGDSUASBuscaAtiva { get; set; }

        [DataMember]
        public Boolean? OrgaoGestorExecutaAgendaFamilia { get; set; }
        [DataMember]
        public Boolean? CRASExecutaAgendaFamilia { get; set; }
        [DataMember]
        public Boolean? CREASExecutaAgendaFamilia { get; set; }

        [DataMember]
        public Int32? MesRepasseFEASAgendaFamilia { get; set; }
        [DataMember]
        public Int32? AnoRepasseFEASAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? ValorFEASAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? ValorFMASAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoMunicipalAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? ValorFNASAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? ValorIGDPBFAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? ValorIGDSUASAgendaFamilia { get; set; }

        [DataMember]
        public Int32? NumeroFamiliasAgendaFamilia2013 { get; set; }
        [DataMember]
        public Int32? NumeroFamiliasAgendaFamilia2014 { get; set; }         
    }
}
