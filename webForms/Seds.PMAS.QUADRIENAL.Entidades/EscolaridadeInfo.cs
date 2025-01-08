using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados de Escolaridade
    /// </summary>
    [DataContract]
    public class EscolaridadeInfo
    {        
        /// <summary>
        /// Código do Escolaridade
        /// </summary>
        [DataMember]
        public Int32 Id {get;set;}  
        /// <summary>
        /// Nome do Escolaridade
        /// </summary>
        [DataMember]
        public String Nome{get;set;}        
    }
}
