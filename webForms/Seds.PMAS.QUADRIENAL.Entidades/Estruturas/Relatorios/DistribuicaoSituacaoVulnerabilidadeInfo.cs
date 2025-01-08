using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_DISTRIBUICAO_SITUACAO_VULNERABILIDADE]
    /// </summary>
    [DataContract]
    public class DistribuicaoSituacaoVulnerabilidadeInfo
    {
        
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String SituacaoVulnerabilidade { get; set; }                
        [DataMember]
        public Int32 Gravidade1 { get; set; }
        [DataMember]
        public Int32 Gravidade2 { get; set; }
        [DataMember]
        public Int32 Gravidade3 { get; set; }
        [DataMember]
        public Int32 Gravidade4 { get; set; }
        [DataMember]
        public Int32 Gravidade5 { get; set; }
        [DataMember]
        public Int32 Gravidade6 { get; set; }
        [DataMember]
        public Int32 Gravidade7 { get; set; }
        [DataMember]
        public Int32 Gravidade8 { get; set; }
        [DataMember]
        public Int32 Gravidade9 { get; set; }
        [DataMember]
        public Int32 Gravidade10 { get; set; }

        private Int32 _total;
        public Int32 Total { get { _total = Gravidade1 + Gravidade2 + Gravidade3 + Gravidade4 + Gravidade5 + Gravidade6 + Gravidade7 + Gravidade8 + Gravidade9 + Gravidade10; return _total; } set { _total = value; } }
        
        [DataMember]
        public Decimal Porcentagem { get; set; }

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
        public Int32 IdPorte { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public String Municipio { get; set; }
        [DataMember]
        public String Drads { get; set; }
    }

    public class DistribuicaoSituacaoVulnerabilidadeInfoComparer : IEqualityComparer<DistribuicaoSituacaoVulnerabilidadeInfo>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(DistribuicaoSituacaoVulnerabilidadeInfo x, DistribuicaoSituacaoVulnerabilidadeInfo y)
        {
            if (Object.ReferenceEquals(x, y))
                return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.Id == y.Id;
        }


        public int GetHashCode(DistribuicaoSituacaoVulnerabilidadeInfo obj)
        {
            return this.GetHashCode();
        }
    }
}

