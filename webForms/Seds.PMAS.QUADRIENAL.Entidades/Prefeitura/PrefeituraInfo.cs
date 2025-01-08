using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Seds.Entidades;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrefeituraInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdMunicipio { get; set; }
        

        [DataMember]
        public Int32 IdSituacao { get; set; }
        
        [DataMember]
        public Int32 Revisao { get; set; }

        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        
        [DataMember]
        public String CNPJ { get; set; }

        [DataMember]
        public DateTime DataPublicacao { get; set; }

        [DataMember]
        public String CEP { get; set; }

        [DataMember]
        public String Logradouro { get; set; }

        [DataMember]
        public String Numero { get; set; }

        [DataMember]
        public String Complemento { get; set; }

        [DataMember]
        public String Cidade { get; set; }

        [DataMember]
        public String Bairro { get; set; }

        [DataMember]
        public String Telefone { get; set; }

        [DataMember]
        public String Celular { get; set; }

        [DataMember]
        public String WebSite { get; set; }

        [DataMember]
        public String Email { get; set; }

        [DataMember]
        public Int32 Populacao { get; set; }

        [DataMember]
        public Int32 IdPrefeituraAnoAnterior { get; set; }

        [DataMember]
        public Boolean Bloqueada { get; set; }

        [DataMember]
        public String Caracterizacao { get; set; }

        [DataMember]
        public String CaracterizacaoPopulacao { get; set; }

        [DataMember]
        public String CaracterizacaoRedeSocioassistencial { get; set; }

        [DataMember]
        public String CaracterizacaoAnaliseInterpretacao { get; set; }

        [DataMember]
        public String JustificativaAcaoPlanejamento { get; set; }

        [DataMember]
        public Boolean PossuiSite { get; set; }



        public Boolean? ValoresReprogramadosDrads { get; set; }

        public Boolean? ValoresDemandasDrads { get; set; }

        public List<MotivoNaoInstalacaoInfo> MotivosNaoInstalacaoCRAS { get; set; }

        public List<MotivoNaoInstalacaoInfo> MotivosNaoInstalacaoCREAS { get; set; }

        public List<MotivoNaoInstalacaoInfo> MotivosNaoInstalacaoCentroPOP { get; set; }

        public List<PrefeituraDemandaAtendimentoInfo> PrefeituraDemandaAtendimento { get; set; }


        /// <summary>
        /// Objeto de Referência do Status
        /// </summary>
        [DataMember]
        public Boolean? DesbloquearValoresDrads { get; set; }


        #region navegacao
        [IgnoreDataMember]
        public MunicipioInfo Municipio { get; set; }

        [DataMember]
        public SituacaoInfo Situacao { get; set; }

        [DataMember]
        public NivelGestaoInfo NivelGestao { get; set; }

        [DataMember]
        public List<PrefeituraAtualizacaoAnualInfo> PrefeituraAtualizacoesAnuais { get; set; }

        [DataMember]
        public List<PrefeituraExercicioBloqueioInfo> PrefeiturasExerciciosBloqueio { get; set; }

        [DataMember]
        public List<FundoMunicipalValoresInfo> FundoMunicipalValores { get; set; }
        
        [DataMember]
        public List<PlanoMunicipalHistoricoConsolidadoInfo> PlanosMunicipaisHistoricoConsolidados { get; set; }

        
        #endregion

    }
}


