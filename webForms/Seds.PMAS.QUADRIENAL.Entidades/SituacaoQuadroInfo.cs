using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados da Situação do Quadro (Recurso)
    /// </summary>
    [DataContract]
    public class SituacaoQuadroInfo
    {
        /// <summary>
        /// Id da Situação
        /// </summary>
        [DataMember]
        public Int32 Id { get; set; }

        /// <summary>
        /// Nome da Situação
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
    }
}
