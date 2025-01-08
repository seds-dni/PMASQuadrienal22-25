using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados de Cargo
    /// </summary>
    [DataContract]
    public class CargoInfo
    {        
        /// <summary>
        /// Código do Cargo
        /// </summary>
        [DataMember]
        public Int16 Id {get;set;}  
        /// <summary>
        /// Nome do Cargo
        /// </summary>
        [DataMember]
        public String Nome{get;set;}
        [DataMember]
        public Int32 Ordem { get; set; }
    }
}
