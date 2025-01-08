using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial
{
    [DataContract]
    public class DemandasParlamentaresServicosPublicosInfo
    {
        [DataMember]
        [Key]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdServicoRecursoFinanceiroFundosPublico { get; set; }

        [DataMember]
        public String CodigoDemandaParlamentar { get; set; }

        [DataMember]
        public String ObjetoDemandaParlamentar { get; set; }

        [DataMember]
        public Decimal? ValorContrapartidaMunicipal { get; set; }

        [DataMember]
        public Int32? Exercicio { get; set; }

        [DataMember]
        public bool ContrapartidaMunicipal { get; set; }

    }
}
