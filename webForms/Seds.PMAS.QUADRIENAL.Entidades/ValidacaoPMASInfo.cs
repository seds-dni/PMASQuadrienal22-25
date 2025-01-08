using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ValidacaoPMASInfo
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
        public Boolean TerritorioDemografia { get; set; }
        [DataMember]
        public Boolean PopulacaoVulnerabilidade { get; set; }
        [DataMember]
        public Boolean EvolucaoRedeAtendimento { get; set; }
        [DataMember]
        public Boolean SituacoesVulnerabilidade { get; set; }
        [DataMember]
        public Boolean AnaliseInterpretacao { get; set; }

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
        [DataMember]
        public Boolean SituacaoInscricaoCMAS { get; set; }
        [DataMember]
        public Boolean SituacaoInscricaoAtualCMAS { get; set; }
        [DataMember]
        public Boolean InscricaoCMAS { get; set; }
        [DataMember]
        public Boolean DataInscricaoCMAS { get; set; }

        #endregion

        #region BlocoIII
        [DataMember]
        public Boolean ProgramasProjetos { get; set; }
        [DataMember]
        public Boolean TransferenciaRenda { get; set; }
        [DataMember]
        public Boolean BeneficiosContinuados { get; set; }
        [DataMember]
        public Boolean BeneficiosEventuais { get; set; }
        #endregion

        #region Bloco IV
        public Boolean Educacao { get; set; }
        public Boolean Saude { get; set; }
        public Boolean SegurancaAlimentar { get; set; }
        public Boolean Emprego { get; set; }
        public Boolean OutrasPoliticas { get; set; }
        #endregion


        #region Bloco V
        [DataMember]
        public Boolean FontesFinanciamento { get; set; }
        [DataMember]
        public Boolean CronogramaDesembolsoProtecaoBasica { get; set; }
        [DataMember]
        public Boolean CronogramaDesembolsoProtecaoMedia { get; set; }
        [DataMember]
        public Boolean CronogramaDesembolsoProtecaoAlta { get; set; }
        [DataMember]
        public Boolean CronogramaDesembolsoProgramaProjeto { get; set; }
        [DataMember]
        public Boolean CronogramaDesembolsoBeneficioEventual { get; set; }


        #endregion

        #region Bloco V
        [DataMember]
        public Boolean AcoesPlanejadas { get; set; }
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
        [DataMember]
        public List<String> Alertas { get; set; }
    }
}
