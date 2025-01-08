using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ConselhoMunicipalParecerInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }

        [DataMember]
        public Boolean AvaliandoExecucao { get; set; }
        [DataMember]
        public Boolean AcompanhaRepasseRecursoFinanceiro { get; set; }
        [DataMember]
        public Boolean AcompanhaPrestacaoConta { get; set; }
        [DataMember]
        public Boolean MonitoraRedeExecutora { get; set; }
        [DataMember]
        public Boolean? HouveParticipacaoPlanejamentoAcoes { get; set; } 
        [DataMember]
        public String ParecerCMAS { get; set; }
        [DataMember]
        public Boolean AtaRegistrada { get; set; }
        [DataMember]
        public DateTime? Data { get; set; }
        [DataMember]
        public Int32 NumeroConselheiros { get; set; }
        [DataMember]
        public String PresidenteRepresentanteLegal { get; set; }
        [DataMember]
        public Boolean? AprovaPMAS { get; set; }
        [DataMember]
        public String ComentarioAvaliandoExecucao { get; set; }
        [DataMember]
        public String ComentarioAcompanhaRepasseRecursoFinanceiro { get; set; }
        [DataMember]
        public String ComentarioAcompanhaPrestacaoConta { get; set; }
        [DataMember]
        public String ComentarioMonitoraRedeExecutora { get; set; }
        [DataMember]
        public String ComentarioParticipacaoPlanejamentoAcoes { get; set; }

        
        
    }
}
