using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ExecucaoFinanceiraInfo
    {
        [Key]
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [Key]
        [DataMember]
        public Int16 IdTipoProtecao { get; set; }
        [Key]
        [DataMember]
        public Int32 Exercicio { get; set; }

        public Int32? IdSituacao { get; set; }


        #region FMAS
        /// <summary>
        /// Previsão Inicial de Repasse do FMAS
        /// </summary>
        [DataMember]
        public Decimal PrevisaoInicialFMAS { get; set; }
        /// <summary>
        /// Valor do Recurso Disponilizado pelo FMAS
        /// </summary>
        [DataMember]
        public Decimal RecursoDisponibilizadoFMAS { get; set; }
        /// <summary>
        /// Valor do Resultado de Aplicações Financeiras (Poupança) do FMAS
        /// </summary>
        [DataMember]
        public Decimal ResultadoAplicacaoFinanceiraFMAS { get; set; }
        /// <summary>
        /// Valores Executados pelo FMAS
        /// </summary>
        [DataMember]
        public Decimal ValoresExecutadosFMAS { get; set; }
        /// <summary>
        /// Valores Reprogramados pelo FMAS
        /// </summary>
        [DataMember]
        public Decimal ValoresReprogramadosFMAS { get; set; }
        /// <summary>
        /// Valores Devolvidos pelo FMAS
        /// </summary>
        [DataMember]
        public Decimal ValoresDevolvidosFMAS { get; set; }
        #endregion

        #region FEAS
        /// <summary>
        /// Previsão Inicial de Repasse do FEAS
        /// </summary>
        [DataMember]
        public Decimal PrevisaoInicialFEAS { get; set; }
        /// <summary>
        /// Valor do Recurso Disponilizado pelo FEAS
        /// </summary>
        [DataMember]
        public Decimal RecursoDisponibilizadoFEAS { get; set; }
        /// <summary>
        /// Valor do Resultado de Aplicações Financeiras (Poupança) do FEAS
        /// </summary>
        [DataMember]
        public Decimal ResultadoAplicacaoFinanceiraFEAS { get; set; }
        /// <summary>
        /// Valores Executados pelo FEAS
        /// </summary>
        [DataMember]
        public Decimal ValoresExecutadosFEAS { get; set; }
        /// <summary>
        /// Valores Reprogramados pelo FEAS
        /// </summary>
        [DataMember]
        public Decimal ValoresReprogramadosFEAS { get; set; }
        /// <summary>
        /// Valores Devolvidos pelo FEAS
        /// </summary>
        [DataMember]
        public Decimal ValoresDevolvidosFEAS { get; set; }


        public Decimal? PrevisaoInicialFEASReprogramado { get; set; }

        public Decimal? RecursoDisponibilizadoFEASReprogramado { get; set; }

        public Decimal? ResultadoAplicacaoFinanceiraFEASReprogramado { get; set; }

        public Decimal? ValoresExecutadosFEASReprogramado { get; set; }

        public Decimal? ValoresReprogramadosFEASReprogramado { get; set; }

        public Decimal? ValoresDevolvidosFEASReprogramado { get; set; }

        #endregion

        #region FNAS
        /// <summary>
        /// Previsão Inicial de Repasse do FNAS
        /// </summary>
        [DataMember]
        public Decimal PrevisaoInicialFNAS { get; set; }
        /// <summary>
        /// Valor do Recurso Disponilizado pelo FNAS
        /// </summary>
        [DataMember]
        public Decimal RecursoDisponibilizadoFNAS { get; set; }
        /// <summary>
        /// Valor do Resultado de Aplicações Financeiras (Poupança) do FNAS
        /// </summary>
        [DataMember]
        public Decimal ResultadoAplicacaoFinanceiraFNAS { get; set; }
        /// <summary>
        /// Valores Executados pelo FNAS
        /// </summary>
        [DataMember]
        public Decimal ValoresExecutadosFNAS { get; set; }
        /// <summary>
        /// Valores Reprogramados pelo FNAS
        /// </summary>
        [DataMember]
        public Decimal ValoresReprogramadosFNAS { get; set; }
        /// <summary>
        /// Valores Devolvidos pelo FNAS
        /// </summary>
        [DataMember]
        public Decimal ValoresDevolvidosFNAS { get; set; }
        #endregion

        #region Calculos
        public Decimal PorcentagemDevolucaoFMAS { get { return ValoresDevolvidosFMAS / (RecursoDisponibilizadoFMAS != 0 || ResultadoAplicacaoFinanceiraFMAS != 0 ? RecursoDisponibilizadoFMAS + ResultadoAplicacaoFinanceiraFMAS : 1); } }

        public Decimal PorcentagemPrestacaoDeContasFMAS { get { return ValoresExecutadosFMAS / (RecursoDisponibilizadoFMAS != 0 || ResultadoAplicacaoFinanceiraFMAS != 0 ? RecursoDisponibilizadoFMAS + ResultadoAplicacaoFinanceiraFMAS : 1); } }

        //public Decimal PorcentagemDevolucaoFEASReprogramado { get { return ValoresDevolvidosFEASReprogramado.HasValue && ValoresDevolvidosFEASReprogramado.Value != 0 ? ValoresDevolvidosFEASReprogramado.Value / ((RecursoDisponibilizadoFEASReprogramado.HasValue && RecursoDisponibilizadoFEASReprogramado.Value != 0 ? RecursoDisponibilizadoFEASReprogramado.Value : 1) + (ResultadoAplicacaoFinanceiraFEASReprogramado.HasValue && ResultadoAplicacaoFinanceiraFEASReprogramado.Value != 0 ? ResultadoAplicacaoFinanceiraFEASReprogramado.Value : 1)) : 1; } }
        public Decimal PorcentagemDevolucaoFEASReprogramado { get { return ValoresDevolvidosFEASReprogramado.HasValue && ValoresDevolvidosFEASReprogramado.Value != 0 ? ValoresDevolvidosFEASReprogramado.Value / 
                                                                    ((RecursoDisponibilizadoFEASReprogramado.HasValue && RecursoDisponibilizadoFEASReprogramado.Value != 0 ? RecursoDisponibilizadoFEASReprogramado.Value : 1) 
                                                                    + (ResultadoAplicacaoFinanceiraFEASReprogramado.HasValue && ResultadoAplicacaoFinanceiraFEASReprogramado.Value != 0 ? ResultadoAplicacaoFinanceiraFEASReprogramado.Value : 1)) : 0; } }
        public Decimal PorcentagemDevolucaoFEAS { get { return ValoresDevolvidosFEAS / (RecursoDisponibilizadoFEAS != 0 || ResultadoAplicacaoFinanceiraFEAS != 0 ? RecursoDisponibilizadoFEAS + ResultadoAplicacaoFinanceiraFEAS : 1); } }
        public Decimal PorcentagemDevolucaoFNAS { get { return ValoresDevolvidosFNAS / (RecursoDisponibilizadoFNAS != 0 || ResultadoAplicacaoFinanceiraFNAS != 0 ? RecursoDisponibilizadoFNAS + ResultadoAplicacaoFinanceiraFNAS : 1); } }

        public Decimal PorcentagemDevolucaoPrestacaoDeContasFMAS { get { return ValoresExecutadosFMAS / (RecursoDisponibilizadoFMAS != 0 || ResultadoAplicacaoFinanceiraFMAS != 0 ? RecursoDisponibilizadoFMAS + ResultadoAplicacaoFinanceiraFMAS : 1); } }

        public Decimal PorcentagemPrestacaoDeContasFNAS { get { return ValoresExecutadosFNAS / (RecursoDisponibilizadoFNAS != 0 || ResultadoAplicacaoFinanceiraFNAS != 0 ? RecursoDisponibilizadoFNAS + ResultadoAplicacaoFinanceiraFNAS : 1); } }
        
        public Decimal PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado { get { return ValoresExecutadosFEAS != 0 && ValoresExecutadosFEAS != null ? ValoresExecutadosFEAS /
                                                                    ((RecursoDisponibilizadoFEAS != 0 && RecursoDisponibilizadoFEAS != null ? RecursoDisponibilizadoFEAS : 1)
                                                                    + (ResultadoAplicacaoFinanceiraFEAS != 0 && ResultadoAplicacaoFinanceiraFEAS != null ? ResultadoAplicacaoFinanceiraFEAS : 1)) : 0;
        }
        }

        public Decimal PorcentagemDevolucaoPrestacaoDeContasFEAS { get { return ValoresExecutadosFEAS / (RecursoDisponibilizadoFEAS != 0 || ResultadoAplicacaoFinanceiraFEAS != 0 ? RecursoDisponibilizadoFEAS + ResultadoAplicacaoFinanceiraFEAS : 1); } }

        #endregion
        
        public bool? Desbloqueado { get; set; }

        public bool? Atualizado { get; set; }

        #region navegacao
        [DataMember]
        public TipoProtecaoSocialInfo TipoProtecaoSocial { get; set; }
        [DataMember]
        public PrefeituraInfo Prefeitura { get; set; }
        [DataMember]
        public SituacaoInfo Situacao { get; set; }
        #endregion

    }
}
