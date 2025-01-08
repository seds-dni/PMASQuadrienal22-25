using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados do Tipo de Proteção Social
    /// </summary>
    [DataContract]
    public class TipoProtecaoSocialInfo
    {
        /// <summary>
        /// Id do Tipo de Proteção Social
        /// </summary>
        [DataMember]
        public Int16 Id { get; set; }
        /// <summary>
        /// Nome da Proteção Social
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
    }
}
