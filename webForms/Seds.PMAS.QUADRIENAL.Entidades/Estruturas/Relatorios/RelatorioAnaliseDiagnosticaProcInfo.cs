using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class RelatorioAnaliseDiagnosticaProcInfo
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
        public Int32 IdSituacaoVulnerabilidade { get; set; }
        [DataMember]
        public String SituacaoVulnerabilidade { get; set; }
        [DataMember]
        public Int32 Classificacao { get; set; }
        [DataMember]
        public Int32 Demanda { get; set; }
        [DataMember]
        public Int32 TotalServicos { get; set; }
        [DataMember]
        public Int32 Exercicio { get; set; }
        [DataMember]
        public String Porte { get; set; }
    }
}
