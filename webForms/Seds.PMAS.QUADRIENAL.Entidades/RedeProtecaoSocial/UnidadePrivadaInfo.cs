using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class UnidadePrivadaInfo : UnidadeInfo
    {
        [DataMember]
        public Int32? IdCaracterizacaoAtividade { get; set; }
        [DataMember]
        public Int32? IdAreaAtuacao { get; set; }
        [DataMember]
        public Int32? IdFormaAtuacao { get; set; }
        [DataMember]
        public String PublicoAlvo { get; set; }
        [DataMember]
        public String InscricaoCMAS { get; set; }
        [DataMember]
        public DateTime? DataPublicacao { get; set; }
        [DataMember]
        public DateTime? DataValidade { get; set; }
        [DataMember]
        public Boolean? PossuiSite { get; set; }

        [DataMember]
        public Int32? IdSituacaoInscricao { get; set; }
        [DataMember]
        public String NomeFantasia { get; set; }
        [DataMember]
        public String Logradouro { get; set; }
        [DataMember]
        public String CEP { get; set; }
        [DataMember]
        public String Numero { get; set; }
        [DataMember]
        public String Complemento { get; set; }
        [DataMember]
        public String Bairro { get; set; }
        [DataMember]
        public String Cidade { get; set; }
        [DataMember]
        public String Telefone { get; set; }
        [DataMember]
        public String Celular { get; set; }
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public String Site { get; set; }
        [DataMember]
        public String Responsavel { get; set; }
        [DataMember]
        public String Cargo { get; set; }
        [DataMember]
        public DateTime? DataInicio { get; set; }
        [DataMember]
        public DateTime? DataTermino { get; set; }

        [DataMember]
        public Int32? IdSituacaoAtualInscricao { get; set; }

        public CaracterizacaoAtividadesInfo CaracterizacaoAtividade { get; set; }

        public List<CaracterizacaoAtividadesInfo> CaracterizacaoAtividades { get; set; }

        public SituacaoInscricaoInfo SituacaoInscricao { get; set; }

        public SituacaoAtualInscricaoInfo SituacaoAtualInscricao { get; set; }

        [DataMember]
        public Int32? IdTipoUnidadeAtendimento { get; set; }

        public List<UnidadeTipoAtendimentoInfo> UnidadesTipoAtendimentos { get; set; }

        public List<FormaAtuacaoInfo> FormasAtuacoes { get; set; }

        public List<PublicoAlvoInfo> PublicoAlvos { get; set; }
        //public List<PrefeituraBeneficioEventualInfo> PrefeituraBeneficiosEventuais { get; set; }
    }
}
