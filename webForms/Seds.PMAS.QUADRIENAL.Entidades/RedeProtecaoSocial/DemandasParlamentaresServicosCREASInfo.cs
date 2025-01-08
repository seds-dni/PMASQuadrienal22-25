using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial
{
    [DataContract]
    public class DemandasParlamentaresServicosCREASInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 ServicoRecursoFinanceiroCREAS { get; set; }

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
