using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_ACOES_MONITORAMENTO]
    /// </summary>
    [DataContract]
    public class AcaoMonitoramentoInfo
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
        public String RealizaMonitoramento { get; set; }        
        [DataMember]
        public String OperacionalizadoOrgaoGestor { get; set; }
        [DataMember]
        public String OperacionalizadoTerceirizado { get; set; }
        [DataMember]
        public String MonitoradoRedePublica { get; set; }
        [DataMember]
        public String MonitoradoRedePrivada { get; set; }
        [DataMember]
        public String EnvioInformacoes { get; set; }
        [DataMember]
        public String ReunioesExecutores { get; set; }
        [DataMember]
        public String ReuniaoUsuarios { get; set; }
        [DataMember]
        public String VisitasSupervisao { get; set; }
        
    }
}

