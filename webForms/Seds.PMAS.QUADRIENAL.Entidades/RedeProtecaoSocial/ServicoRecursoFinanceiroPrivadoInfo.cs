using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroPrivadoInfo : ServicoRecursoFinanceiroInfo
    {
        [DataMember]
        public Int32 IdLocalExecucao { get; set; }
        public LocalExecucaoPrivadoInfo LocalExecucao { get; set; }
        [DataMember]
        public Decimal ValorPrivadoEmpresas { get; set; }
        [DataMember]
        public Decimal ValorPrivadoOrganizacoes { get; set; }
        [DataMember]
        public Decimal ValorPrivadoPessoasFisicas { get; set; }
        [DataMember]
        public Decimal ValorPrivadoProprios { get; set; }
        [DataMember]
        public Boolean? PossuiProgramaBeneficio { get; set; }


        public List<ServicoRecursoFinanceiroFundosPrivadoInfo> ServicosRecursosFinanceirosFundosPrivadoInfo { get; set; }
        public ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo RecursosHumanos { get; set; }


        public List<ServicoRecursoFinanceiroPrivadoCapacidadeInfo> ServicosRecursosFinanceiroPrivadoCapacidade { get; set; }
        public List<ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo> ServicosRecursosFinanceiroPrivadoCapacidadeLA { get; set; }
        public List<ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo> ServicosRecursosFinanceiroPrivadoCapacidadePSC { get; set; }

        public List<ServicoRecursoFinanceiroPrivadoMediaMensalInfo> ServicosRecursosFinanceiroPrivadoMediaMensal { get; set; }
        public List<ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo> ServicosRecursosFinanceiroPrivadoMediaMensalLA { get; set; }
        public List<ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo> ServicosRecursosFinanceiroPrivadoMediaMensalPSC { get; set; }


        public List<ServicoRecursoFinanceiroTransferenciaRendaInfo> ServicosRecursosFinanceirosTransferenciaRenda { get; set; }
        
    }
}
