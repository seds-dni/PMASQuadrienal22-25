using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ProgramaProjetoCofinanciamentoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdProgramaProjeto { get; set; }
        [DataMember]
        public Int32? IdServicosRecursosFinanceirosCRAS { get; set; }
        [DataMember]
        public Int32? IdServicosRecursosFinanceirosCREAS { get; set; }
        [DataMember]
        public Int32? IdServicosRecursosFinanceirosCentroPOP { get; set; }
        [DataMember]
        public Int32? IdServicosRecursosFinanceirosPublico { get; set; }
        [DataMember]
        public Int32? IdServicosRecursosFinanceirosPrivado { get; set; }

        [DataMember]
        public Int32? NumeroUsuarios { get; set; }

        public ProgramaProjetoInfo ProgramaProjeto { get; set; }
        public TransferenciaRendaInfo TransferenciaRenda { get; set; }
        public ServicoRecursoFinanceiroCRASInfo ServicosRecursosFinanceirosCRAS { get; set; }
        public ServicoRecursoFinanceiroCREASInfo ServicosRecursosFinanceirosCREAS { get; set; }
        public ServicoRecursoFinanceiroCentroPOPInfo ServicosRecursosFinanceirosCentroPOP { get; set; }
        public ServicoRecursoFinanceiroPublicoInfo ServicosRecursosFinanceirosPublico { get; set; }
        public ServicoRecursoFinanceiroPrivadoInfo ServicosRecursosFinanceirosPrivado { get; set; }

        public ServicoRecursoFinanceiroInfo Servico
        {
            get
            {
                if (
                ServicosRecursosFinanceirosCentroPOP != null && ServicosRecursosFinanceirosCentroPOP.Desativado != true)
                    return ServicosRecursosFinanceirosCentroPOP;
                if (ServicosRecursosFinanceirosCRAS != null && ServicosRecursosFinanceirosCRAS.Desativado != true)
                    return ServicosRecursosFinanceirosCRAS;
                if (ServicosRecursosFinanceirosCREAS != null && ServicosRecursosFinanceirosCREAS.Desativado != true)
                    return ServicosRecursosFinanceirosCREAS;
                if (ServicosRecursosFinanceirosPrivado != null && ServicosRecursosFinanceirosPrivado.Desativado != true)
                    return ServicosRecursosFinanceirosPrivado;
                if (ServicosRecursosFinanceirosPublico != null && ServicosRecursosFinanceirosPublico.Desativado != true)
                    return ServicosRecursosFinanceirosPublico;
                return null;
            }
        }

    }
}
