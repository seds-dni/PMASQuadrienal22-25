using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class AnaliseDiagnosticaInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }

        [DataMember]
        public Int32 Classificacao { get; set; }
        [DataMember]
        public Int32 Demanda { get; set; }

        [DataMember]
        public Int32 IdSituacaoVulnerabilidade { get; set; }
        [DataMember]
        public SituacaoVulnerabilidadeInfo SituacaoVulnerabilidade { get; set; }

        [DataMember]
        public Int32 IdExercicio { get; set; }
    }
}
