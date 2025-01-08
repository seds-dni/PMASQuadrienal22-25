using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados de Estrutura dos Orgãos Gestores
    /// </summary>
    [DataContract]
    public class EstruturaInfo
    {
        /// <summary>
        /// Id da Estrutura
        /// </summary>
        [DataMember]
        public Int16 Id{get;set;}
        /// <summary>
        /// Nome da Estrutura
        /// </summary>
        [DataMember]
        public String Nome {get;set;}
        /// <summary>
        /// Ordem da Estrutura para os Combos
        /// </summary>
        [DataMember]
        public Int32? Ordem {get;set;}        
    }
}
