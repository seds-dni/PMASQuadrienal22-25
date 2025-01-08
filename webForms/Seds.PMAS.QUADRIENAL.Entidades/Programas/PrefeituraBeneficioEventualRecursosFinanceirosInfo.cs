using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrefeituraBeneficioEventualRecursosFinanceirosInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [Key]
        [DataMember]
        public Int32 IdPrefeituraBeneficioEventual { get; set; }

        [Key]
        [DataMember]
        public int Exercicio { get; set; }

        [DataMember]
        public Decimal ValorFMAS { get; set; }
        [DataMember]
        public Decimal ValorFundoMunicipalSolidariedade { get; set; }
        [DataMember]
        public Decimal ValorOrcamentoMunicipal { get; set; }
        [DataMember]
        public Decimal ValorFEAS { get; set; }
        [DataMember]
        public Decimal ValorFundoEstadualSolidariedade { get; set; }
        [DataMember]
        public Decimal ValorFNAS { get; set; }

        [DataMember]
        public Boolean Desbloqueado { get; set; }

        [DataMember]
        public Decimal ValorReprogramacaoAnoAnterior { get; set; }

        [DataMember]
        public Decimal ValorDemandasParlamentares { get; set; }

        [DataMember]
        public Decimal ValorReprogramacaoDemandasParlamentares { get; set; }

        [DataMember]
        public String CodigoDemandaParlamentar { get; set; }
        
        [DataMember]
        public String ObjetoDemandaParlamentar { get; set; }
        
        [DataMember]
        public Decimal? ValorContrapartidaMunicipal { get; set; }
        
        [DataMember]
        public Boolean? ContrapartidaMunicipal { get; set; }


        #region navegacao
        public PrefeituraBeneficioEventualInfo PrefeituraBeneficioEventual { get; set; } 
        #endregion

    }
}
