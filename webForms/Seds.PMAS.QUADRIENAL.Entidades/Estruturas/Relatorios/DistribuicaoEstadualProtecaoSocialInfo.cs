using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_DISTRIBUICAO_ESTADUAL_PROTECAO_SOCIAL]
    /// </summary>
    [DataContract]
    public class DistribuicaoEstadualProtecaoSocialInfo
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Drads { get; set; }
        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }


        [DataMember]
        public Decimal RedePublicaBasica { get; set; }
        [DataMember]
        public Decimal RedePrivadaBasica { get; set; }
        [DataMember]
        public Decimal RedePublicaEspecialMedia { get; set; }
        [DataMember]
        public Decimal RedePrivadaEspecialMedia { get; set; }
        [DataMember]
        public Decimal RedePublicaEspecialAlta { get; set; }
        [DataMember]
        public Decimal RedePrivadaEspecialAlta { get; set; }

        [DataMember]
        public Decimal BuscaAtiva { get; set; }
        [DataMember]
        public Decimal AgendaFamilia { get; set; }

        public Decimal SaoPauloSolidario { get { return BuscaAtiva + AgendaFamilia; } }

        public Decimal FamiliaPaulista { get; set; }

        public Decimal ImplantacaoCRAS { get; set; }

        //public Decimal Total { get { return RedePrivadaBasica + RedePublicaBasica + RedePrivadaEspecialMedia + RedePublicaEspecialMedia + RedePrivadaEspecialAlta + RedePublicaEspecialAlta + BuscaAtiva + AgendaFamilia + FamiliaPaulista; } }
        //public Decimal TotalBasica { get { return RedePrivadaBasica + RedePublicaBasica + BuscaAtiva + AgendaFamilia + FamiliaPaulista; } }
        public Decimal Total { get { return RedePrivadaBasica + RedePublicaBasica + RedePrivadaEspecialMedia + RedePublicaEspecialMedia + RedePrivadaEspecialAlta + RedePublicaEspecialAlta + FamiliaPaulista; } }
        public Decimal TotalBasica { get { return RedePrivadaBasica + RedePublicaBasica + FamiliaPaulista + ImplantacaoCRAS; } }
        public Decimal TotalEspecialMedia { get { return RedePrivadaEspecialMedia + RedePublicaEspecialMedia; } }
        public Decimal TotalEspecialAlta { get { return RedePrivadaEspecialAlta + RedePublicaEspecialAlta; } }

        //public Decimal TotalRedePublica { get { return RedePublicaBasica + RedePublicaEspecialMedia + RedePublicaEspecialAlta + BuscaAtiva + AgendaFamilia + FamiliaPaulista; } }

        public Decimal TotalRedePublica { get { return RedePublicaBasica + RedePublicaEspecialMedia + RedePublicaEspecialAlta + FamiliaPaulista; } }
        public Decimal TotalRedePrivada { get { return RedePrivadaBasica + RedePrivadaEspecialMedia + RedePrivadaEspecialAlta; } }
    }
}

