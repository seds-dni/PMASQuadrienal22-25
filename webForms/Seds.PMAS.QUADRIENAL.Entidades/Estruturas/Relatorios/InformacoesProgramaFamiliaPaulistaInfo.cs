using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    public class InformacoesProgramaFamiliaPaulistaInfo
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public String Municipio { get; set; }
        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public string Drads { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }
        [DataMember]
        public String Porte { get; set; }
        [DataMember]
        public DateTime DataAprovacao { get; set; }
        [DataMember]
        public DateTime DataAdesaoPrograma { get; set; }
        [DataMember]
        public Int32 MetaSeds { get; set; }
        [DataMember]
        public Int32 MetaMunicipio { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public String Telefone { get; set; }
        [DataMember]
        public Int32 NumeroIdentificacao { get; set; }
        [DataMember]
        public Int32 NumeroFamilias { get; set; }
        [DataMember]
        public String Bairros { get; set; }
        [DataMember]
        public String NomeResponsavel { get; set; }
        [DataMember]
        public Decimal ValorFMAS { get; set; }
        [DataMember]
        public Decimal ValorOrcamentoMunicipal { get; set; }
        [DataMember]
        public Decimal ValorFundoMunicipal { get; set; }
        [DataMember]
        public Decimal ValorFEAS { get; set; }
        [DataMember]
        public Decimal ValorOrcamentoEstadual { get; set; }
        [DataMember]
        public Decimal ValorFundoEstadual { get; set; }
        [DataMember]
        public Decimal ValorFNAS { get; set; }
        [DataMember]
        public Decimal ValorOrcamentoFederal { get; set; }
        [DataMember]
        public Decimal ValorFundoFederal { get; set; }
        [DataMember]
        public Decimal ValorIGDPBF { get; set; }
        [DataMember]
        public Decimal ValorIGDSUAS { get; set; }
        [DataMember]
        public Int32 IdIdentificacao { get; set; }
        public Decimal ValorTotal { get { return ValorFMAS + ValorOrcamentoMunicipal + ValorFundoMunicipal + ValorFEAS + ValorOrcamentoEstadual + ValorFundoEstadual + ValorFNAS + ValorOrcamentoFederal + ValorFundoFederal + ValorIGDPBF + ValorIGDSUAS; } }

        public List<IdentificacaoTerritorioInfo> IdentificacoesTerritorios { get; set; }
    }
}
