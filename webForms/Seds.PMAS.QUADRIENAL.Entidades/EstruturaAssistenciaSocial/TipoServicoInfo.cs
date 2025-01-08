using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados do Tipo de Serviço Social
    /// </summary>
    [DataContract]
    public class TipoServicoInfo
    {
        /// <summary>
        /// Id do Tipo de Serviço Social
        /// </summary>
        [DataMember]
        public Int32 Id { get; set; }
        /// <summary>
        /// Nome do Tipo de Serviço Social
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
        /// <summary>
        /// Ordem do Tipo de Serviço Social
        /// </summary>
        [DataMember]
        public Int32 Ordem { get; set; }
        /// <summary>
        /// Id do Tipo de Proteção Social
        /// </summary>
        [DataMember]
        public Int16 IdTipoProtecaoSocial { get; set; }

        /// <summary>
        /// Objeto de Referência do Tipo de Proteção Social
        /// </summary>
        public TipoProtecaoSocialInfo TipoProtecaoSocial { get; set; }        

        /// <summary>
        /// Lista de Atividades SocioAssistenciais
        /// </summary>
        public List<AtividadeSocioAssistencialInfo> AtividadesSocioAssistenciais { get; set; }

        //PMAS 2016
        /// <summary>
        /// Aponta se o Tipo de Serviço é Tipificado ou Não Tipificado
        /// </summary>
        [DataMember]
        public Boolean? NaoTipificado { get; set; }
    }
}
