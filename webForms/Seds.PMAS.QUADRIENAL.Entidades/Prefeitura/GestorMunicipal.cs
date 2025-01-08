using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados do Gestor Municipal
    /// </summary>
    [DataContract]
    public class GestorMunicipalInfo
    {
        /// <summary>
        /// Id do Gestor Municipal
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
        /// Nome do Gestor Municipal
        /// (Oriundo do Cadastro Único de Usuários - Apenas é persistido pois foram migrados gestores anteriores)
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
        /// <summary>
        /// Id do Usuário do Cadastro Único que representa o Gestor Municipal        
        /// </summary>
        [DataMember]
        public Int32? IdUsuarioGestor { get; set; }
        /// <summary>
        /// Id do Cargo do Gestor Municipal
        /// </summary>
        [DataMember]
        public Int16 IdCargo { get; set; }
        /// <summary>
        /// Objeto de Referência do Cargo do Gestor Municipal
        /// </summary>
        [DataMember]
        public CargoInfo Cargo { get; set; }
        /// <summary>
        /// Descrição do Outro Cargo do Gestor Municipal, caso o mesmo não selecionar nenhum cargo da lista
        /// </summary>
        [DataMember]
        public String OutroCargo { get; set; }

        /// <summary>
        /// Data de Nomeação do Gestor Municipal
        /// </summary>
        [DataMember]
        public DateTime DataNomeacao { get; set; }

        /// <summary>
        /// Id da Escolaridade do Gestor Municipal
        /// </summary>
        [DataMember]
        public Int32 IdEscolaridade { get; set; }
        /// <summary>
        /// Objeto de Referência da Escolaridade do Gestor Municipal 
        /// </summary>
        [DataMember]
        public EscolaridadeInfo Escolaridade { get; set; }
        /// <summary>
        /// Id da Formação Acadêmica do Gestor Municipal
        /// </summary>
        [DataMember]
        public Int32? IdFormacao { get; set; }
        /// <summary>
        /// Objeto de Referência de Formação Acadêmica do Gestor Municipal 
        /// </summary>
        [DataMember]
        public FormacaoInfo Formacao { get; set; }
        /// <summary>
        /// Descrição da outra formação acadêmica do Gestor Municipal, caso o mesmo não selecionar nenhuma formação da lista
        /// </summary>
        [DataMember]
        public String OutraFormacao { get; set; }

        /// <summary>
        /// Telefone do Gabinete do Gestor Municipal
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
        /// Data Final de Gestão
        /// </summary>        
        [DataMember]
        public DateTime? DataTerminoGestao { get; set; }

        public String RG { get; set; }

        public String RGDigito { get; set; }

        public DateTime? DataEmissao { get; set; }

        public Int16 UFRG { get; set; }

        public String Emissor { get; set; }

        public String CPF { get; set; }


        public String NumeroDecreto { get; set; }

        public DateTime? DataDecreto { get; set; }

        public String Lei { get; set; }

        public DateTime? DataPublicacao { get; set; }
    }
}
