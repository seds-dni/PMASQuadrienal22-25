using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados de Status
    /// </summary>
    [DataContract]
    public class StatusInfo
    {        
        /// <summary>
        /// Id do Status
        /// </summary>
        [DataMember]
        public Int16 Id { get; set; }
        /// <summary>
        /// Descrição do Status
        /// </summary>
        [DataMember]
        public String Nome { get; set; }               
    }
}
