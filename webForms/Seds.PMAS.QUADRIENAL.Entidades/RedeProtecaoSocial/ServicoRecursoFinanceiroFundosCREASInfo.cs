using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroFundosCREASInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 ServicoRecursoFinanceiroCREASInfoId { get; set; }
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
        public Int32 Exercicio { get; set; }
        [DataMember]
        public Int32 IdSituacao { get; set; }
        [DataMember]
        public Boolean? ExisteOutraFonteFinanciamento { get; set; }
        [DataMember]
        public Decimal ValorEstadualAssistenciaAnoAnterior { get; set; }
        [DataMember]
        public Decimal ValorEstadualDemandasParlamentares { get; set; }
        [DataMember]
        public Decimal ValorEstadualDemandasParlamentaresReprogramacao { get; set; }
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

        public ServicoRecursoFinanceiroCREASInfo ServicoRecursoFinanceiroCREASInfo { get; set; }

        public List<ServicoRecursoFinanceiroCREASFonteRecursoInfo> ServicoRecursoFinanceiroCREASFontesRecursosInfo { get; set; }
    }
}
