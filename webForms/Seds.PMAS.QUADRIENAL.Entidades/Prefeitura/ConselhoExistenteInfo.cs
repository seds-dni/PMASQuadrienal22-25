using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados dos Conselhos Existentes na Prefeitura
    /// </summary>
    [DataContract]
    public class ConselhoExistenteInfo    
    {
        /// <summary>
        /// Id do Conselho
        /// </summary>
        [DataMember]
        public Int32 Id { get; set; }
        /// <summary>
        /// Id da Prefeitura
        /// </summary>
        [DataMember]
        public Int32 IdPrefeitura {get;set;}
        /// <summary>
        /// Objeto de Referência da Prefeitura
        /// </summary>
        [IgnoreDataMember]
        public PrefeituraInfo Prefeitura { get; set; }        

        /// <summary>
        /// Id do Tipo de Conselho
        /// </summary>
        [DataMember]
        public Int32 IdConselho {get;set;}
        /// <summary>
        /// Objeto de Referência do Tipo de Conselho
        /// </summary>
        [DataMember]
        public ConselhosInfo Conselho { get; set; }
        /// <summary>
        /// Nome do Conselho , caso seja do tipo 'Outro'
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
        /// <summary>
        /// Nome do Presidente do Conselho
        /// </summary>
        [DataMember]
        public String NomePresidente {get;set;}
        /// <summary>
        /// Lei de Criação do Conselho
        /// </summary>
        [DataMember]
        public String Lei {get;set;}
        /// <summary>
        /// Data de Criação do Conselho
        /// </summary>
        [DataMember]
        public DateTime? DataCriacao {get;set;}
        /// <summary>
        /// CEP do Conselho
        /// </summary>
        [DataMember]
        public String CEP {get;set;}
        /// <summary>
        /// Logradouro do Conselho
        /// </summary>
        [DataMember]
        public String Logradouro {get;set;}
        /// <summary>
        /// Número do Logradouro do Conselho
        /// </summary>
        [DataMember]
        public String Numero {get;set;}
        /// <summary>
        /// Complemento do Logradouro do Conselho        
        /// </summary>
        [DataMember]
        public String Complemento {get;set;}
        /// <summary>
        /// Bairro onde está localizado o Conselho
        /// </summary>
        [DataMember]
        public String Bairro {get;set;}
        /// <summary>
        /// Cidade onde está localizado o Conselho
        /// </summary>
        [DataMember]
        public String Cidade { get; set; }
        /// <summary>
        /// Email do Conselho
        /// </summary>
        [DataMember]
        public String Email { get; set; }
        /// <summary>
        /// Telefone do Conselho
        /// </summary>
        [DataMember]
        public String Telefone { get; set; }

        /// <summary>
        /// Celular do Conselho
        /// </summary>
        [DataMember]
        public String Celular { get; set; }
        /// <summary>
        /// Data de Início do Mandato
        /// </summary>
        [DataMember]
        public DateTime? MandatoInicio { get; set; }
        /// <summary>
        /// Data de Termínio do Mandato
        /// </summary>
        [DataMember]
        public DateTime? MandatoTerminio { get; set; }

        public String CPF { get; set; }

        public String RG { get; set; }

        public String RGDigito { get; set; }

        public Int16 IDUFRG { get; set; }

        public String SiglaEmissor { get; set; }

        public DateTime? DataEmissao { get; set; }

    }
}
