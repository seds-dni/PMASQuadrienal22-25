using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Seds.Entidades;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados de Usuário PMAS
    /// </summary>
    [DataContract]
    public class UsuarioPMASInfo
    {      
        /// <summary>
        /// Id do Usuário
        /// (Oriundo do Cadastro Único de Usuários)
        /// </summary>
        [DataMember]
        public Int32 IdUsuario {get;set;}
        /// <summary>
        /// Id da Prefeitura 
        /// Caso o Usuário seja do Perfil Órgão Gestor, já é vinculado qual prefeitura o mesmo poderá ter acesso
        /// </summary>
        [DataMember]        
        public Int32? IdPrefeitura {get;set;}
        /// <summary>
        /// Objeto de Referência da Prefeitura, caso o Usuário seja do Perfil Órgão Gestor
        /// </summary>
        [DataMember]
        public PrefeituraInfo Prefeitura { get; set; }

        /// <summary>
        /// CPF do Usuário
        /// </summary>
        [DataMember]
        public String CPF {get;set;}
        /// <summary>
        /// Id da Drads
        /// (Oriundo do Serviço Divisão Administrativa)
        /// Caso o Usuário seja do Perfil DRADS, já é vinculado qual a DRADS do mesmo, para que seja acessado apenas os planos das prefeituras pertences à DRADS.
        /// </summary>
        [DataMember]
        public Int16? IdDrads {get;set;}
        /// <summary>
        /// Objeto de Referência da Drads
        /// </summary>        
        [IgnoreDataMember]
        public DradsInfo Drads { get; set; }
        /// <summary>
        /// Id do Status do Usuário
        /// </summary>
        [DataMember]
        public Int16 IdStatus {get;set;}
        /// <summary>
        /// Objeto de Referência do Status
        /// </summary>
        [DataMember]
        public StatusInfo Status { get; set; }        
        /// <summary>
        /// Flag de Usuário Ativo
        /// </summary>
        [DataMember]
        public Int32 Ativo {get;set; }
        /// <summary>
        /// Instituição onde Usuário trabalha
        /// </summary>
        [DataMember]
        public String Instituicao { get; set; }
        /// <summary>
        /// Cargo/Função do Usuário
        /// </summary>
        [DataMember]
        public String Cargo { get; set; }

        #region Cadastro Único
        /// <summary>
        /// Nome do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
        /// <summary>
        /// Email do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String Email { get; set; }
        /// <summary>
        /// RG do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String RG { get; set; }
        /// <summary>
        /// Orgão Emissor do RG do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String OrgaoEmissor { get; set; }
        /// <summary>
        /// UF do RG do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String UFRG { get; set; }
        /// <summary>
        /// Telefone do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String Telefone { get; set; }
        /// <summary>
        /// Celular do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String Celular { get; set; }
        /// <summary>
        /// Endereço do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String Endereco { get; set; }
        /// <summary>
        /// Número do Endereço do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String Numero { get; set; }
        /// <summary>
        /// Complemento do Endereço do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String Complemento { get; set; }
        /// <summary>
        /// Bairro do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String Bairro { get; set; }
        /// <summary>
        /// Cidade do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String Cidade { get; set; }
        /// <summary>
        /// CEP do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String CEP { get; set; }
        /// <summary>
        /// UF da Cidade do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String UFCidade { get; set; }
        /// <summary>
        /// Login  do Usuário
        /// (Oriundo do Cadastro Único de Usuário)
        /// </summary>
        [DataMember]
        public String Login { get; set; }
        [DataMember]
        public Boolean TrocarSenha { get; set; }
        #endregion


        #region Not Mapped
        [DataMember]
        public String Perfil { get; set; }
        [DataMember]
        public EPerfil? EnumPerfil { get; set; }
        [DataMember]
        public List<RecursoInfo> Recursos { get; set; }
        [DataMember]
        public Int32 idPerfilAntigo { get; set; }
        #endregion
    }
}
