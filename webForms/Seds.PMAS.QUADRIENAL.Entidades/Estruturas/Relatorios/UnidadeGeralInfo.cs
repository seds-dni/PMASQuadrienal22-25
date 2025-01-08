using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_UNIDADES]
    /// </summary>
    [DataContract]
    [Serializable]
    public class UnidadeGeralInfo
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdUnidade { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Drads { get; set; }
        [DataMember]
        public Int32 IdTipoRede { get; set; }
        [DataMember]
        public string TipoRede { get; set; }
        [DataMember]
        public string CNPJ { get; set; }
        [DataMember]
        public string RazaoSocial { get; set; }
        [DataMember]
        public string InscricaoCmas { get; set; }
        [DataMember]
        public Int32 IdSituacaoInscricao { get; set; }
        [DataMember]
        public string SituacaoInscricao { get; set; }
        [DataMember]
        public Int32 IdSituacaoAtualInscricao { get; set; }
        [DataMember]
        public string SituacaoAtualInscricao { get; set; }
        [DataMember]
        public Int32 IdAreaAtuacao { get; set; }
        [DataMember]
        public string AreaAtuacao { get; set; }
        [DataMember]
        public Int32 IdFormaAtuacao { get; set; }
        [DataMember]
        public string FormaAtuacao { get; set; }
        [DataMember]
        public Int32 TotalLocais { get; set; }
        [DataMember]
        public string SituacaoProSocial { get; set; }
        [DataMember]
        public string ServicosSocioAssistenciais { get; set; }
        [DataMember]
        public string BeneficiosEventuais { get; set; }
        [DataMember]
        public string VivaleiteBomPrato { get; set; }


    }
}
