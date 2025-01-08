using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{

    [DataContract]
    public class GestorFundoMunicipalInfo
    {

        /// <summary>
        /// Id do Gestor Fundo Municipal
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
        /// Nome do Gestor do Fundo Municipal
        /// </summary>
        [DataMember]
        public String Nome { get; set; }

        /// <summary>
        /// CPF do Gestor Fundo Municipal
        /// </summary>
        [DataMember]
        public String CPF { get; set; }

        /// <summary>
        /// RG do Gestor do Fundo Municipal
        /// </summary>
        public String RG { get; set; }
        /// <summary>
        /// Digito do RG do Gestor do Fundo Municipal
        /// </summary>
        public String RGDigito { get; set; }

        /// <summary>
        /// Data de Emissão do RG do Gestor do Fundo Municipal 
        /// </summary>
        public DateTime? DataEmissao { get; set; }

        /// <summary>
        /// Orgão Emissor do RG
        /// </summary>
        public String SiglaEmissor { get; set; }

        /// <summary>
        /// UF do 
        /// </summary>
        public Int32 IdUFRG { get; set; }

        /// <summary>
        /// Data de Nomeação do Gestor Municipal
        /// </summary>
        [DataMember]
        public DateTime InicioGestao { get; set; }

        /// <summary>
        /// Data Final de Gestão
        /// </summary>        
        [DataMember]
        public DateTime? TerminoGestao { get; set; }

        /// <summary>
        /// Telefone do Gabinete do Gestor do Fundo Municipal
        /// </summary>
        [DataMember]
        public String Telefone { get; set; }

        /// <summary>
        /// Celular do Gabinete do Gestor Municipal
        /// </summary>
        [DataMember]
        public String Celular { get; set; }

        /// <summary>
        /// Email executivo do Gestor Municipal
        /// </summary>
        [DataMember]
        public String Email { get; set; }

        /// <summary>
        /// Id do Status do Gestor Municipal
        /// </summary>
        [DataMember]
        public Int16 IdStatus { get; set; }
        /// <summary>
        /// Objeto de Referência do Status do Gestor Municipal
        /// </summary>        
        [DataMember]
        public StatusInfo Status { get; set; }
        /// <summary>
        /// Objeto de Referência do Status do Gestor Municipal
        /// </summary>
        [DataMember]
        public Int32 IdTipoGestor { get; set; }

        public TipoGestorMunicipalInfo TipoGestor { get; set; }

        public String NumeroDecreto { get; set; }
        
        public DateTime? DataDecreto { get; set; }
    }
}
