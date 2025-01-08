using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrefeituraBeneficioEventualInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdTipoBeneficioEventual { get; set; }
        [DataMember]
        public Boolean Regulamentacao { get; set; }
        [DataMember]
        public Int32? IdTipoLegislacao { get; set; }
        [DataMember]
        public String Lei { get; set; }
        [DataMember]
        public DateTime? DataPublicacaoLei { get; set; }
        [DataMember]
        public Int32? IdFormaAuxilio { get; set; }
        [DataMember]
        public Int32? MediaSemestralBeneficiarios { get; set; }
        [DataMember]
        public Int32? MediaSemestralBeneficiariosConcedidos { get; set; }
        [DataMember]
        public Boolean BeneficiarioAtendidoRedeSocioAssistencial { get; set; }
        [DataMember]
        public Boolean OrgaoGestorResponsavel { get; set; }       
        [DataMember]
        public Boolean CRASResponsavel { get; set; }        
        [DataMember]
        public Boolean UnidadePrivadaResponsavel { get; set; }
        [DataMember]
        public Boolean CREASResponsavel { get; set; }
        [DataMember]
        public Boolean CentroPOPResponsavel { get; set; }
        [DataMember]
        public Boolean FundoSocialSolidariedadeResponsavel { get; set; }        
        
        [DataMember]
        public Int32? IdUnidadeExecutora { get; set; }

        [DataMember]
        public Boolean? Decreto { get; set; }

        [DataMember]
        public String NumeroDecreto { get; set; }

        [DataMember]
        public DateTime? DataDecreto { get; set; }

        [DataMember]
        public Boolean? Resolucao { get; set; }

        [DataMember]
        public String NumeroResolucao { get; set; }

        [DataMember]
        public DateTime? DataResolucao { get; set; }


        [DataMember]
        public Boolean? AlterouLei { get; set; }

        [DataMember]
        public String NumeroLeiAlterada { get; set; }

        [DataMember]
        public DateTime? DataAlteracaoLei { get; set; }


        [DataMember]
        public Boolean? AlterouDecreto { get; set; }

        [DataMember]
        public String NumeroDecretoAlterado { get; set; }

        [DataMember]
        public DateTime? DataAlteracaoDecreto { get; set; }


        [DataMember]
        public Boolean? AlterouResolucao { get; set; }

        [DataMember]
        public String NumeroResolucaoAlterada { get; set; }

        [DataMember]
        public DateTime? DataAlteracaoResolucao { get; set; }




        #region navegacao

        public PrefeituraInfo Prefeitura { get; set; }
        public TipoLegislacaoInfo TipoLegislacao { get; set; }
        public TipoBeneficioEventualInfo TipoBeneficioEventual { get; set; }
        public FormaAuxilioInfo FormaAuxilio { get; set; }

        [DataMember]
        public UnidadePrivadaInfo UnidadePrivada { get; set; } //welington p
        [DataMember]
        public List<UnidadePrivadaInfo> UnidadesExecutoras { get; set; } //Welington P.
        [DataMember]
        public List<BeneficioEventualInfo> BeneficiosOferecidos { get; set; }
        [DataMember]
        public List<CriterioConcessaoInfo> Criterios { get; set; }
        [DataMember]
        public List<NecessidadeBeneficioEventualInfo> Necessidades { get; set; }
        [DataMember]
        public List<OrgaoResponsavelInfo> OrgaosResponsaveis { get; set; }  
        [DataMember]
        public List<PrefeituraBeneficioEventualRecursosFinanceirosInfo> PrefeituraBeneficiosEventuaisRecursosFinanceiros { get; set; }
        #endregion

    }
}
