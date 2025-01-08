using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_EXECUCAO_RECURSOS_COFINANCIAMENTO_ESTADUAL]
    /// </summary>
    [DataContract]
    public class ExecucaoRecursosCofinanciamentoEstadualInfo
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Drads { get; set; }
        [DataMember]
        public string ProtecaoSocial { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }

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
        #endregion

        #region Calculos
        public Decimal PorcentagemExecucaoFEAS { get { return ValoresExecutadosFEAS / (RecursoDisponibilizadoFEAS != 0 || ResultadoAplicacaoFinanceiraFEAS != 0 ? RecursoDisponibilizadoFEAS + ResultadoAplicacaoFinanceiraFEAS : 1); } }
        #endregion

    }
}
