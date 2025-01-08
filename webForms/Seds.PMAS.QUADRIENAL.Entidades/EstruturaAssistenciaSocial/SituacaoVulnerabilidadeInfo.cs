using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados das Situações de Vulnerabilidade Social ou risco social
    /// </summary>
    [DataContract]
    public class SituacaoVulnerabilidadeInfo
    {
        /// <summary>
        /// Id da Situação de Vulnerabilidade
        /// </summary>
        [DataMember]
        public Int32 Id { get; set; }
        /// <summary>
        /// Nome da Situação de Vulnerabilidade
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 Ordem { get; set; }
    }
}
