using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados dos Tipos de Conselho existente nos Municípios
    /// </summary>
    [DataContract]
    public class ConselhosInfo
    {        
        /// <summary>
        /// Id do Tipo de Conselho
        /// </summary>
        [DataMember]        
        public Int32 Id {get;set;}
        /// <summary>
        /// Nome do Tipo de Conselho
        /// </summary>
        [DataMember]
        public String Nome {get;set;}                
    }
}
