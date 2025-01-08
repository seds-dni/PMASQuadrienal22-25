using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroCREASInfo : ServicoRecursoFinanceiroInfo
    {
        [DataMember]
        public Int32 IdCREAS { get; set; }
        public CREASInfo CREAS { get; set; }
        [DataMember]
        public Boolean? PossuiProgramaBeneficio { get; set; }
        
        public List<ServicoRecursoFinanceiroFundosCREASInfo> ServicosRecursosFinanceirosFundosCREASInfo { get; set; }
        public ServicoRecursoFinanceiroCREASRecursosHumanosInfo RecursosHumanos { get; set; }

        public List<ServicoRecursoFinanceiroCREASCapacidadeInfo> ServicosRecursosFinanceiroCREASCapacidade { get; set; }
        public List<ServicoRecursoFinanceiroCREASCapacidadeLAInfo> ServicosRecursosFinanceiroCREASCapacidadeLA { get; set; }
        public List<ServicoRecursoFinanceiroCREASCapacidadePSCInfo> ServicosRecursosFinanceiroCREASCapacidadePSC { get; set; }

        public List<ServicoRecursoFinanceiroCREASMediaMensalInfo> ServicosRecursosFinanceiroCREASMediaMensal { get; set; }
        public List<ServicoRecursoFinanceiroCREASMediaMensalLAInfo> ServicosRecursosFinanceiroCREASMediaMensalLA { get; set; }
        public List<ServicoRecursoFinanceiroCREASMediaMensalPSCInfo> ServicosRecursosFinanceiroCREASMediaMensalPSC { get; set; }

        public List<ServicoRecursoFinanceiroTransferenciaRendaInfo> ServicosRecursosFinanceirosTransferenciaRenda { get; set; }
        
    }
}
