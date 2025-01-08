using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroPublicoMediaMensalLAInfo 
    {
        [DataMember]
        [Key]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdServicoRecursoFinanceiroPublico { get; set; }

        [DataMember]
        public Int32? MediaMensal { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }

        public ServicoRecursoFinanceiroPublicoInfo ServicoRecursoFinanceiroPublico { get; set; }

    }
}
