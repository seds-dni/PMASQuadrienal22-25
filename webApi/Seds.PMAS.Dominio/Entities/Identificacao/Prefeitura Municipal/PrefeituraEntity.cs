using System;
using System.Collections.Generic;

namespace Seds.PMAS.Dominio.Entities
{
    public partial class PrefeituraEntity
    {
        public int Id { get; set; }
        public int IdMunicipio { get; set; }
        public Int32 IdNivelGestao { get; set; }
        public int IdSituacao { get; set; }
        public string CNPJ { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Telefone { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public int Populacao { get; set; }
        public int? IdPrefeituraAnoAnterior { get; set; }
        public bool Bloqueado { get; set; }
        public string Caracterizacao { get; set; }
        public bool PossuiSite { get; set; }
        public int Revisao { get; set; }
        public string Cidade { get; set; }
        public string CaracterizacaoPopulacao { get; set; }
        public string CaracterizacaoRedeSocioassistencial { get; set; }
        public string CaracterizacaoAnaliseInterpretacao { get; set; }
        public string JustificativaAcaoPlanejamento { get; set; }
        public bool? DesbloquearValoresDrads { get; set; }
        public bool? ValoresReprogramadosDrads { get; set; }
        public string Celular { get; set; }
        public virtual NivelGestaoEntity NivelGestao { get; set; }
        public virtual SituacaoEntity Situacao { get; set; }
    }
}
