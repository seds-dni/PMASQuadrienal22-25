using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS2013.Entidades
{
    [DataContract]
    public class RecursoFinanceiroPrivadoInfo : RecursoFinanceiroInfo
    {
        public LocalExecucaoPrivadoInfo LocalExecucao { get; set; }
        [DataMember]
        public Decimal ValorPrivadoEmpresas { get; set; }
        [DataMember]
        public Decimal ValorPrivadoOrganizacoes { get; set; }
        [DataMember]
        public Decimal ValorPrivadoPessoasFisicas { get; set; }
        [DataMember]
        public Decimal ValorPrivadoProprios { get; set; }
    }
}
