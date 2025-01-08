using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    /// <summary>
    /// Classe de Dados da Identificação dos Conselhos Existentes no Município
    /// Resultado da View VW_IDENTIFICACAO_CONSELHOS_EXISTENTES
    /// </summary>
    [DataContract]
    public class IdentificacaoConselhoExistenteInfo
    {
        /// <summary>
        /// Id do Conselho
        /// </summary>
        [DataMember]
        public Int32 Id {get;set;}
        /// <summary>
        /// Nome do Conselho
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
        /// <summary>
        /// Id do Tipo de Conselho
        /// </summary>
        [DataMember]
        public Int32 IdConselho { get; set; }
        /// <summary>
        /// Id da Prefeitura
        /// </summary>
        [DataMember]
        public Int32 IdPrefeitura { get; set; }

    }
}
