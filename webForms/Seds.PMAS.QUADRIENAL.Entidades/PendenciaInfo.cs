using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS2013.Entidades
{
    [DataContract]
    public class PendenciaInfo
    {
        #region Bloco I
        [DataMember]
        public Boolean InformacoesPrefeitura { get; set; }
        [DataMember]
        public Boolean InformacoesPrefeito { get; set; }
        [DataMember]
        public Boolean InformacoesOrgaoGestor { get; set; }
        [DataMember]
        public Boolean InformacoesGestorMunicipal { get; set; }
        [DataMember]
        public Boolean InformacoesFundoMunicipal { get; set; }
        [DataMember]
        public Boolean InformacoesConselhosMunicipais { get; set; }
        #endregion

        #region BlocoII
        [DataMember]
        public Boolean AnaliseDiagnostica { get; set; }
        [DataMember]
        public Boolean RedeProtecaoSocialPublica { get; set; }
        [DataMember]
        public Boolean RedeProtecaoSocialPrivada { get; set; }
        [DataMember]
        public Boolean CRAS { get; set; }
        [DataMember]
        public Boolean CREAS { get; set; }
        [DataMember]
        public Boolean CentroPOP { get; set; }
        #endregion

        #region BlocoIII
        [DataMember]
        public Boolean ProgramasProjetos { get; set; }
        [DataMember]
        public Boolean TransferenciaRenda { get; set; }
        [DataMember]
        public Boolean BeneficiosEventuais { get; set; }
        #endregion

        #region Bloco IV
        [DataMember]
        public Boolean AcoesPlanejadas { get; set; }
        #endregion

        #region Bloco V
        [DataMember]
        public Boolean CronogramaDesembolsoProtecaoBasica { get; set; }
        [DataMember]
        public Boolean CronogramaDesembolsoProtecaoMedia { get; set; }
        [DataMember]
        public Boolean CronogramaDesembolsoProtecaoAlta { get; set; }
        #endregion

        #region Bloco VI
        [DataMember]
        public Boolean VigilanciaSocioAssistencial { get; set; }
        [DataMember]
        public Boolean Monitoramento { get; set; }
        [DataMember]
        public Boolean Avaliacao { get; set; }
        [DataMember]
        public Boolean AspectosGerais { get; set; }
        #endregion

        #region Bloco VII
        [DataMember]
        public Boolean ConselhoMunicipal { get; set; }
        [DataMember]
        public Boolean ParecerConselhoMunicipal { get; set; }
        #endregion        

        [DataMember]
        public List<String> Pendencias { get; set; }
    }
}
