using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados do Fundo Municipal da Prefeitura (FMAS)
    /// </summary>
    [DataContract]
    public class FundoMunicipalInfo
    {
        /// <summary>
        /// Id do Fundo Municipal
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
        /// CNPJ do Fundo Municipal
        /// </summary>
        [DataMember]
        public String CNPJ { get; set; }
        /// <summary>
        /// Lei de Criação do Fundo Municipal
        /// </summary>
        [DataMember]
        public String Lei { get; set; }
        /// <summary>
        /// Nome do Gestor do Fundo Municipal
        /// </summary>
        [DataMember]
        public String NomeGestor { get; set; }
        /// <summary>
        /// Data de Criação do Fundo Municipal
        /// </summary>
        [DataMember]
        public DateTime? DataCriacao { get; set; }
        /// <summary>
        /// O FMAS já está legalmente regulamentado?
        /// </summary>
        [DataMember]
        public Boolean Regulamenta { get; set; }
        /// <summary>
        /// Número do Decreto de regulamentação do Fundo Municipal
        /// </summary>
        [DataMember]
        public String NumeroDecreto { get; set; }
        /// <summary>
        /// Data de regulamentação do Fundo Municipal
        /// </summary>
        [DataMember]
        public DateTime? DataDecreto { get; set; }
        /// <summary>
        /// O FMAS constitui-se como Unidade Orçamentária?
        /// </summary>
        [DataMember]
        public Boolean? Orcamentaria { get; set; }
        /// <summary>
        /// Indica se o FMAS é uma filial da Prefeitura
        /// </summary>
        [DataMember]
        public Boolean Filial { get; set; }


        [DataMember]
        public Boolean? AlteracaoLei { get; set; }

        [DataMember]
        public String LeiAlterada { get; set; }

        [DataMember]
        public DateTime? DataLeiAlterada { get; set; }

        public Int32 Bloco { get; set; }

        public List<FundoMunicipalValoresInfo> FundosMunicipaisValoresInfo { get; set; }

    }
}
