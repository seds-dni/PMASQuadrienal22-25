using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados de Comentário sobre Execução Financeira de 2012
    /// </summary>
    [DataContract]
    public class ComentarioExecucaoFinanceiraInfo
    {
        [Key]
        [DataMember]
        public Int32 IdPrefeitura {get;set;}
        [Key]
        [DataMember]
        public Int32 Exercicio { get; set; }

        [DataMember]
        public String Comentario { get; set; }
        public Int32? IdSituacao { get; set; }
        public bool? Desbloqueado { get; set; }

        #region navegacao
        public PrefeituraInfo Prefeitura { get; set; }
        public SituacaoInfo SituacaoInfo { get; set; }
        #endregion
    }
}
