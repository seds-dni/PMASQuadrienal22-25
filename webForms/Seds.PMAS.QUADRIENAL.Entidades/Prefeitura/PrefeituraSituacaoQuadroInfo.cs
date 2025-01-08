using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrefeituraSituacaoQuadroInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public PrefeituraInfo Prefeitura { get; set; }

        [DataMember]
        public Int32 IdSituacaoQuadro { get; set; }
        [DataMember]
        public SituacaoQuadroInfo SituacaoQuadro { get; set; }

        [DataMember]
        public Int32 IdRecurso { get; set; }
        [DataMember]
        public RecursoInfo Recurso { get; set; }

        [DataMember]
        public Int32 Exercicio{ get; set; }
    }
}
