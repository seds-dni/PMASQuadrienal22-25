using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class HistoricoPrestacaoDeContasInfo
    {
        [Key]
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        [DataMember]
        public Int32 IdPerfil { get; set; }

        [DataMember]
        public Int32 IdSituacaoQuadro { get; set; }

        [DataMember]
        public String NomeResponsavel { get; set; }

        [DataMember]
        public string CPFResponsavel { get; set; }

        [DataMember]
        public String DescricaoMotivo { get; set; }

        [DataMember]
        public Int32 PosicaoFinal { get; set; }

        [DataMember]
        public Int32 DeAcordo { get; set; }

        [DataMember]
        public DateTime Data { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }

        public string SituacaoStatus { get { return IdSituacaoQuadro == 1 ? "Em Preenchimento" : IdSituacaoQuadro == 3 ? "Em Análise pelo CMAS" : IdSituacaoQuadro == 6 ? "Devolvido pelo CMAS" : IdSituacaoQuadro == 4 ? "Aprovado CMAS e em Análise DRADS" : IdSituacaoQuadro == 8 ? "Aprovado CMAS e DRADS Favorável" : IdSituacaoQuadro == 9 ? "Aprovado CMAS e DRADS desfavorável" : IdSituacaoQuadro == 10 ? "Rejeitado CMAS" : IdSituacaoQuadro == 5 ? "Devolvido DRADS" : ""; } }
    }
}
