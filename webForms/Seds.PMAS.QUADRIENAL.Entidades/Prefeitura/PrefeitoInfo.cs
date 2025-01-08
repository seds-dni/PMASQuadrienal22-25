using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados do Prefeito Municipal
    /// </summary>
    [DataContract]
    public class PrefeitoInfo
    {
        /// <summary>
        /// Id do Prefeito
        /// </summary>
        [DataMember]
        public Int32 Id { get; set; }

        /// <summary>
        /// Id da Prefeitura
        /// </summary>
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        /// <summary>
        /// Objeto de Referência da Prefeitura
        /// </summary>
        [IgnoreDataMember]
        public PrefeituraInfo Prefeitura { get; set; }

        /// <summary>
        /// Nome do Prefeito
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
        /// <summary>
        /// Número do RG do Prefeito
        /// </summary>
        [DataMember]
        public String RG { get; set; }
        /// <summary>
        /// Dígito do RG do Prefeito
        /// </summary>
        [DataMember]
        public String RGDigito { get; set; }
        /// <summary>
        /// Data de Emissão do RG do Prefeito
        /// </summary>
        [DataMember]
        public DateTime? RGDataEmissao { get; set; }
        /// <summary>
        /// Id do Estado onde foi emitido o RG
        /// (Oriundo do Serviço de Divisão Administrativa)
        /// </summary>
        [DataMember]
        public Int16 IdUFRG { get; set; }
        /// <summary>
        /// Sigla do Emissor do RG do Prefeito
        /// </summary>
        [DataMember]
        public String SiglaEmissor { get; set; }
        /// <summary>
        /// Número do CPF do Prefeito
        /// </summary>
        [DataMember]
        public String CPF { get; set; }
        /// <summary>
        /// Data de Início do Mandato
        /// </summary>
        [DataMember]
        public DateTime MandatoInicio { get; set; }
        /// <summary>
        /// Data de Termínio do Mandato
        /// </summary>
        [DataMember]
        public DateTime MandatoTerminio { get; set; }
        /// <summary>
        /// Email executivo do Prefeito        
        /// </summary>
        [DataMember]
        public String Email { get; set; }
        /// <summary>
        /// Id do Status
        /// </summary>
        [DataMember]
        public Int16 IdStatus { get; set; }
        /// <summary>
        /// Objeto de Referência do Status
        /// </summary>
        [DataMember]
        public StatusInfo Status { get; set; }
        [DataMember]
        public String Telefone { get; set; }
        [DataMember]
        public String Celular { get; set; }

        /// <summary>
        /// Objeto de Referência do Status
        /// </summary>
        [DataMember]
        public Boolean? DesbloquearValoresDRADS { get; set; }
    }
}
