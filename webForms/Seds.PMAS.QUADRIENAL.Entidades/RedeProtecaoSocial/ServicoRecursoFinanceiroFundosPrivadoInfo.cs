using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroFundosPrivadoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 ServicoRecursoFinanceiroPrivadoInfoId { get; set; }
        [DataMember]
        public Int32? MotivoEstadualizadoInfoID { get; set; }

        [DataMember]
        public Decimal ValorMunicipalAssistencia { get; set; }
        [DataMember]
        public Decimal ValorMunicipalFMDCA { get; set; }
        [DataMember]
        public Decimal ValorEstadualAssistencia { get; set; }
        [DataMember]
        public Decimal ValorEstadualFEDCA { get; set; }
        [DataMember]
        public Decimal ValorFederalAssistencia { get; set; }
        [DataMember]
        public Decimal ValorFederalFNDCA { get; set; }
        [DataMember]
        public Decimal ValorMunicipalFMI { get; set; }
        [DataMember]
        public Decimal ValorEstadualFEI { get; set; }
        [DataMember]
        public Decimal ValorFederalFNI { get; set; }
        [DataMember]
        public Decimal ValorEstadualAssistenciaAnoAnterior { get; set; }
        [DataMember]
        public Decimal ValorEstadualDemandasParlamentares { get; set; }
        [DataMember]
        public Decimal ValorEstadualDemandasParlamentaresReprogramacao { get; set; }
        [DataMember]
        public Int32 Exercicio { get; set; }
        [DataMember]
        public Int32 IdSituacao { get; set; }
        [DataMember]
        public Boolean? Desbloqueado { get; set; }
        [DataMember]
        public String CodigoDemandaParlamentar { get; set; }
        [DataMember]
        public String ObjetoDemandaParlamentar { get; set; }
        [DataMember]
        public Decimal? ValorContrapartidaMunicipal { get; set; }
        [DataMember]
        public Boolean? ContrapartidaMunicipal { get; set; }
        
        #region Valor Exclusivo
        [DataMember]
        public Decimal? ValorRecursoExclusivoServico { get; set; } 
        #endregion

        [DataMember]
        public Boolean? ExisteOutraFonteFinanciamento { get; set; }
        public List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> ServicoRecursoFinanceiroPrivadoFontesRecursosInfo { get; set; }


        #region convenio firmado com o Estado
        [DataMember]
        public Boolean? ConvenioEstadualizado { get; set; } 
        [DataMember]
        public Decimal? ValorEstadualizado { get; set; }
        #endregion

        #region Mantido
        public Boolean? ServicoEstadualizado { get; set; }
        #endregion


        public MotivoEstadualizadoInfo MotivoEstadualizadoInfo { get; set; }

        public ServicoRecursoFinanceiroPrivadoInfo ServicoRecursoFinanceiroPrivadoInfo { get; set; }


        
    }
}
