using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class AvaliacaoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }

        [DataMember]
        public Boolean AvaliaAcoes { get; set; }
        [DataMember]
        public Boolean UtilizaDadosMonitoramento { get; set; }
        
        [DataMember]
        public Boolean AvaliadoOrgaoGestor { get; set; }
        [DataMember]
        public Boolean AvaliadoTerceirizado { get; set; }
        [DataMember]
        public Boolean AvaliadoOrgaoGestorEquipeEspecifica { get; set; }
        [DataMember]
        public Boolean AvaliadoOrgaoGestorEquipeTecnicoProtecaoSocial { get; set; }
        [DataMember]
        public Boolean AvaliadoOrgaoGestorTecnicosOutrasEquipes { get; set; }
        
        [DataMember]
        public Boolean AvaliacaoGovernoEstadual { get; set; }
        [DataMember]
        public Boolean AvaliacaoSedsDrads { get; set; }
        [DataMember]
        public Boolean AvaliacaoTribunalDeContasDoEstado { get; set; }
        [DataMember]
        public Boolean AvaliacaoSecretariaDaFazenda { get; set; }
        [DataMember]
        public Boolean AvaliacaoMinisterioPublicoEstado { get; set; }
        [DataMember]
        public Boolean AvaliacaoDefensoriaPublicaEstado { get; set; }
        [DataMember]
        public Boolean AvaliacaoOutrosConselhosEstaduais { get; set; }
        [DataMember]
        public String AvaliacaoOutrosQuais { get; set; }
        
        [DataMember]
        public Boolean AvaliacaoGovernoFederal { get; set; }
        [DataMember]
        public Boolean AvaliacaoConselhosMunicipais { get; set; }
        [DataMember]
        public Boolean AvaliacaoCMAS { get; set; }
        [DataMember]
        public Boolean AvaliacaoCMDCA { get; set; }
        [DataMember]
        public Boolean AvaliacaoOutrosConselhosMunicipais { get; set; }
        [DataMember]
        public Boolean AvaliacaoEmpresasPrivadas { get; set; }
        [DataMember]
        public Boolean AvaliacaoONGs { get; set; }
       
        [DataMember]
        public List<ObjetivoAvaliacaoInfo> Objetivos { get; set; }
        [DataMember]
        public List<ProcedimentoAvaliacaoInfo> Procedimentos { get; set; }
        [DataMember]
        public List<MotivoNaoAvaliacaoInfo> MotivosNaoAvaliacao { get; set; }

    }
}
