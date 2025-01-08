using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroPublicoInfo : ServicoRecursoFinanceiroInfo
    {
        [DataMember]
        public Int32 IdLocalExecucao { get; set; }
        public LocalExecucaoPublicoInfo LocalExecucao { get; set; }
        public Boolean? PossuiProgramaBeneficio { get; set; }
        
        public List<ServicoRecursoFinanceiroFundosPublicoInfo> ServicosRecursosFinanceirosFundosPublicoInfo { get; set; }
        public ServicoRecursoFinanceiroPublicoRecursosHumanosInfo RecursosHumanos { get; set; }

        public List<ServicoRecursoFinanceiroPublicoCapacidadeInfo> ServicosRecursosFinanceiroPublicoCapacidade { get; set; }
        public List<ServicoRecursoFinanceiroPublicoCapacidadeLAInfo> ServicosRecursosFinanceiroPublicoCapacidadeLA { get; set; }
        public List<ServicoRecursoFinanceiroPublicoCapacidadePSCInfo> ServicosRecursosFinanceiroPublicoCapacidadePSC { get; set; }

        public List<ServicoRecursoFinanceiroPublicoMediaMensalInfo> ServicosRecursosFinanceiroPublicoMediaMensal { get; set; }
        public List<ServicoRecursoFinanceiroPublicoMediaMensalLAInfo> ServicosRecursosFinanceiroPublicoMediaMensalLA { get; set; }
        public List<ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo> ServicosRecursosFinanceiroPublicoMediaMensalPSC { get; set; }

        public List<ServicoRecursoFinanceiroTransferenciaRendaInfo> ServicosRecursosFinanceirosTransferenciaRenda { get; set; }
        



    }
}
