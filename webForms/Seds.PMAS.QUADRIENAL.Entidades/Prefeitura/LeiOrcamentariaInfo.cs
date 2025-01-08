using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados da Lei Orçamentária de Aprovação para Assistência Social do Município
    /// </summary>
    [DataContract]
    public class LeiOrcamentariaInfo
    {
        #region keys
        /// <summary>
        /// Id da Prefeitura
        /// </summary>
        [Key]
        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        [Key]
        [DataMember]
        public int Exercicio { get; set; } 
        #endregion

        /// <summary>
        /// Objeto de Referência da Prefeitura
        /// </summary>
        public PrefeituraInfo Prefeitura { get; set; }
        /// <summary>
        /// Valor Aprovado na Lei
        /// </summary>
        [DataMember]
        public Decimal ValorAprovado { get; set; }
        /// <summary>
        /// Número da Lei
        /// </summary>
        [DataMember]
        public String Lei { get; set; }
        /// <summary>
        /// Data de Publicação da Lei
        /// </summary>
        [DataMember]
        public DateTime DataPublicacao { get; set; }

         

        /// <summary>
        /// Utilização de outros recursos para Assistência Social
        /// </summary>
        [DataMember]
        public Boolean? OutrosRecursos { get; set; }
        /// <summary>
        /// Valor de Recursos Humanos
        /// </summary>
        [DataMember]
        public Decimal? ValorRecursosHumanos { get; set; }
        /// <summary>
        /// Valor de manutenção e/ou reforma de equipamentos
        /// </summary>
        [DataMember]
        public Decimal? ValorManutencaoEquipamentos { get; set; }
        /// <summary>
        /// Valor de construção de novas unidades
        /// </summary>
        [DataMember]
        public Decimal? ValorConstrucaoUnidades { get; set; }
        /// <summary>
        /// Valor de aquisição de bens permanentes
        /// </summary>
        [DataMember]
        public Decimal? ValorAquisicaoBens { get; set; }

        [DataMember]
        public Decimal? ValorRecursosFMAS { get; set; }

        [DataMember]
        public Decimal? ValorRecursosNaoAlocadosFMAS { get; set; }

        [DataMember]
        public String NomeVeiculoComunicacao { get; set; }


        public Decimal? TotalFMAS { get; set; }


        public String ComentariosOrgaoGestor { get; set; }

        public int Situacao { get; set; }
        
    }
}
