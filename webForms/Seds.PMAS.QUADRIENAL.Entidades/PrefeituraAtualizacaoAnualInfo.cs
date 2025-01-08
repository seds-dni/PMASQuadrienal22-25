using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Seds.Entidades;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrefeituraAtualizacaoAnualInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdSituacao { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32? Exercicio { get; set; }
        [DataMember]
        public String AtualizacaoAnual{ get; set; }

        #region navegacao
        [DataMember]
        public SituacaoInfo Situacao { get; set; }

        [DataMember]
        public PrefeituraInfo Prefeitura { get; set; } 
        #endregion
    }
}

