using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroCRASInfo : ServicoRecursoFinanceiroInfo
    {
        [DataMember]
        public Int32 IdCRAS { get; set; }
        public CRASInfo CRAS { get; set; }
        [DataMember]
        public Boolean OfertadoPelaEquipeVolante { get; set; }
        public Boolean? PossuiProgramaBeneficio { get; set; }

        public List<ServicoRecursoFinanceiroFundosCRASInfo> ServicosRecursosFinanceirosFundosCRASInfo { get; set; }
        public ServicoRecursoFinanceiroCRASRecursosHumanosInfo RecursosHumanos { get; set; }

        public List<ServicoRecursoFinanceiroCRASCapacidadeInfo> ServicosRecursosFinanceiroCRASCapacidade { get; set; }
        public List<ServicoRecursoFinanceiroCRASMediaMensalInfo> ServicosRecursosFinanceiroCRASMediaMensal { get; set; }

        public List<ServicoRecursoFinanceiroTransferenciaRendaInfo> ServicosRecursosFinanceirosTransferenciaRenda{ get; set; }
    }
}
