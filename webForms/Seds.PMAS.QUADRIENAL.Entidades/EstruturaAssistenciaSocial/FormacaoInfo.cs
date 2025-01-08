using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados de Formação Acadêmica
    /// </summary>
    [DataContract]
    public class FormacaoInfo
    {        
        /// <summary>
        /// Id da Formação
        /// </summary>
        [DataMember]
        public Int32 Id {get;set;}        
        /// <summary>
        /// Nome da Formação
        /// </summary>
        [DataMember]
        public String Nome {get;set;}

        /// <summary>
        /// Ordem da Formação
        /// </summary>
        [DataMember]
        public Int32 Ordem { get; set; }
  

    }
}
