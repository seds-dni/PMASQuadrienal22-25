using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// PR_ACOES_VIGILANCIA_SOCIOASSISTENCIAL
    /// </summary>

    [DataContract]
    public class RelAcaoVigilanciaInfo
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
        public String OfereceVigilancia { get; set; }
        [DataMember]
        public String PossuiEquipeVigilanciaSocioassistencial { get; set; }
        [DataMember]
        public Int32 EquipeVigilanciaSocioassistencial { get; set; }
        [DataMember]
        public String VigilanciaRiscos { get; set; }
        [DataMember]
        public String VigilanciaPadroesServicos { get; set; }
        [DataMember]
        public String PossuiSistemaInformaizadoProprio { get; set; }
        [DataMember]
        public String CadUnico { get; set; }
        [DataMember]
        public String OutrosAplicativosSUAS { get; set; }
        [DataMember]
        public String PMASWeb { get; set; }
        [DataMember]
        public String SisPETI { get; set; }
        [DataMember]
        public String SisJovem { get; set; }
        [DataMember]
        public String OutraBase { get; set; }
        [DataMember]
        public String ProSocial { get; set; }

        [DataMember]
        public String InstrumentaisProprios { get; set; }
        [DataMember]
        public String SistemaInformatizadoMunicipal { get; set; }
        [DataMember]
        public String OutrosOrgaosMunicipais { get; set; }
        [DataMember]
        public String SEADE { get; set; }
        [DataMember]
        public String AplicativosSAGIMDS { get; set; }
        [DataMember]
        public String AplicativosBolsaFamilia { get; set; }
        [DataMember]
        public String IBGE { get; set; }
        [DataMember]
        public String SISC { get; set; }
        [DataMember]
        public String CensoSUAS { get; set; }
        [DataMember]
        public String CNEAS { get; set; }
        [DataMember]
        public String CadSUAS { get; set; }
        [DataMember]
        public String RMAS { get; set; }
    }
}
