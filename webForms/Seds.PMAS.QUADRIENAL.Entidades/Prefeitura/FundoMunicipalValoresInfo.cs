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
    public class FundoMunicipalValoresInfo
    {
        /// <summary>
        /// Id do Fundo Municipal
        /// </summary>
        [DataMember]
        public Int32 IdFundoMunicipal { get; set; }

        /// <summary>
        /// Id da Prefeitura
        /// </summary>
        [Key]
        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        /// <summary>
        /// Id da Prefeitura
        /// </summary>
        [Key]
        [DataMember]
        public Int32 Exercicio { get; set; }

        [DataMember]
        public Decimal? ValorFMAS { get; set; }
        [DataMember]
        public Decimal? ValorFEAS { get; set; }
        [DataMember]
        public Decimal? ValorFNAS { get; set; }

        [DataMember]
        public Decimal? ValorCusteio { get; set; }


        #region navegacao

        /// <summary>
        /// Objeto de Referência da Prefeitura
        /// </summary>
        [IgnoreDataMember]
        public FundoMunicipalInfo FundoMunicipal { get; set; }

        /// <summary>
        /// Objeto de Referência da Prefeitura
        /// </summary>
        [IgnoreDataMember]
        public PrefeituraInfo Prefeitura { get; set; }


        #endregion
    }
}
