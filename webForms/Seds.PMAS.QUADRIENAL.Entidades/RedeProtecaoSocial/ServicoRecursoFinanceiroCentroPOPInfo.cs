using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroCentroPOPInfo : ServicoRecursoFinanceiroInfo
    {
        [DataMember]
        public Int32 IdCentroPOP { get; set; }
        public CentroPOPInfo CentroPOP { get; set; }
        public Boolean? PossuiProgramaBeneficio { get; set; }

        public List<ServicoRecursoFinanceiroFundosCentroPOPInfo> ServicosRecursosFinanceirosFundosCentroPOPInfo { get; set; }

        public List<ServicoRecursoFinanceiroCentroPOPCapacidadeInfo> ServicosRecursosFinanceiroCentroPOPCapacidade { get; set; }
        public List<ServicoRecursoFinanceiroCentroPOPMediaMensalInfo> ServicosRecursosFinanceiroCentroPOPMediaMensal { get; set; }

        public ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo RecursosHumanos { get; set; }
        public List<ServicoRecursoFinanceiroTransferenciaRendaInfo> ServicosRecursosFinanceirosTransferenciaRenda { get; set; }
    }
}
