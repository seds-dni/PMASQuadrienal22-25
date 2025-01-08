using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados das Situações Específicas de Vulnerabilidade Social ou risco social
    /// </summary>
    [DataContract]
    public class SituacaoEspecificaInfo
    {
        /// <summary>
        /// Id da Situação Específica
        /// </summary>
        [DataMember]
        public Int32 Id { get; set; }
        /// <summary>
        /// Nome da Situação Específica
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
        /// <summary>
        /// Id da Situação de Vulnerabilidade
        /// </summary>
        [DataMember]
        public Int32 IdSituacaoVulnerabilidade { get; set; }        

        /// <summary>
        /// Objeto de Referência da Situação de Vulnerabilidade
        /// </summary>
        public SituacaoVulnerabilidadeInfo SituacaoVulnerabilidade { get; set; }

        /// <summary>
        /// Lista de Usuários dos Tipos de Serviços
        /// </summary>
        public List<UsuarioTipoServicoInfo> Usuarios { get; set; }
    }
}
