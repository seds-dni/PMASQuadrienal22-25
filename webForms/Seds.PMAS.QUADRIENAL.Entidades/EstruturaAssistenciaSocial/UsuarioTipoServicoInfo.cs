using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class UsuarioTipoServicoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 IdTipoServico { get; set; }
        [DataMember]
        public TipoServicoInfo TipoServico { get; set; }

        /// <summary>
        /// Lista de Situações Específicas de Vulnerabilidade
        /// </summary>
        public List<SituacaoEspecificaInfo> SituacoesEspecificas { get; set; }
    }
}
