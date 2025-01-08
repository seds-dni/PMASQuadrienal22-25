using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class MonitoramentoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }

        [DataMember]
        public Boolean RealizaMonitoramento { get; set; }
        [DataMember]
        public Boolean PretendeRealizarProximoAno { get; set; }
        [DataMember]
        public Boolean InformacoesSistematizadas { get; set; }
        [DataMember]
        public Boolean ResultadosDivulgados { get; set; }
        [DataMember]
        public Boolean PMASObjetoMonitoramento { get; set; }
        [DataMember]
        public Boolean NaoHaMonitoramentoRedeSocioAssistencial { get; set; }
        
        [DataMember]
        public Boolean OperacionalizadoOrgaoGestor { get; set; }
        [DataMember]
        public Boolean OperacionalizadoTerceirizado { get; set; }
        [DataMember]
        public Boolean OperacionalizadoOrgaoGestorEquipeEspecifica { get; set; }
        [DataMember]
        public Boolean OperacionalizadoOrgaoGestorEquipeTecnicoProtecaoSocial { get; set; }
        [DataMember]
        public Boolean OperacionalizadoOrgaoGestorTecnicosOutrasEquipes { get; set; }            
       
        [DataMember]
        public List<MeioDivulgacaoInfo> MeiosDivulgacao { get; set; }
        [DataMember]
        public List<ProcedimentoMonitoramentoInfo> Procedimentos { get; set; }
        [DataMember]
        public List<InstrumentoMonitoramentoInfo> Instrumentos { get; set; }
        [DataMember]
        public List<PrefeituraMonitoramentoFocoInfo> Focos { get; set; }
    }
}
