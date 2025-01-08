using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class ConsultaAnaliseDiagnosticaServicosInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 IdLocalExecucao { get; set; }
        [DataMember]
        public String SituacaoVulnerabilidade { get; set; }
        [DataMember]
        public Int32 Classificacao { get; set; }
        [DataMember]
        public Int32 Demanda { get; set; }
        [DataMember]
        public Int32 TotalServicos { get; set; }
        [DataMember]
        public String Porte { get; set; }
        [DataMember]
        public Int32 IdExercicio { get; set; }
    }
}
