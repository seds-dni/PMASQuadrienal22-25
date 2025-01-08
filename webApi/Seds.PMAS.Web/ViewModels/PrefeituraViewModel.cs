using Seds.PMAS.Dominio.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.Web.ViewModels
{
    public class PrefeituraViewModel
    {
        [Key]
        public Int32 Id { get; set; }

        public Int32 IdMunicipio { get; set; }


        [Required(ErrorMessage = "Certifique-se de informou o nível de gestão.")]
        public Int64 IdNivelGestao { get; set; }

        public Int32 IdSituacao { get; set; }

        [Required(ErrorMessage = "Certifique-se de introduziu o CNPJ.")]
        public String CNPJ { get; set; }

        public DateTime? DataPublicacao { get; set; }

        [Required(ErrorMessage = "Certifique-se de introduziu o CEP.")]
        public String Cep { get; set; }

        [Required(ErrorMessage = "Certifique-se de introduziu o Logradouro.")]
        public String Logradouro { get; set; }

        [Required(ErrorMessage = "Certifique-se de introduziu o Número.")]
        public String Numero { get; set; }

        public String Complemento { get; set; }

        [Required(ErrorMessage = "Certifique-se de introduziu o Bairro.")]
        public String Bairro { get; set; }

        [Required(ErrorMessage = "Certifique-se de introduziu o Telefone.")]
        public String Telefone { get; set; }

        public String WebSite { get; set; }

        [Required(ErrorMessage = "Certifique-se de introduziu o e-mail.")]
        [MaxLength(80, ErrorMessage = "O email deve ter no máximo {0} caracteres")]
        [MinLength(10, ErrorMessage = "O email deve ter no minimo {0} caracteres")]
        [EmailAddress(ErrorMessage = "Certifique-se de introduziu um e-mail Válido")]
        public String Email { get; set; }


        public Int32 Populacao { get; set; }

        public Int32? IdPrefeituraAnoAnterior { get; set; }

        public Boolean Bloqueado { get; set; }

        public String Caracterizacao { get; set; }

        public Boolean PossuiSite { get; set; }

        public Int32 Revisao { get; set; }

        public String Cidade { get; set; }

        public String CaracterizacaoPopulacao { get; set; }

        public String CaracterizacaoRedeSocioassistencial { get; set; }

        public String CaracterizacaoAnaliseInterpretacao { get; set; }

        public String JustificativaAcaoPlanejamento { get; set; }

        public Boolean? DesbloquearValoresDrads { get; set; }

        public bool? ValoresReprogramadosDrads { get; set; }

        public string Celular { get; set; }

        public virtual NivelGestaoEntity NivelGestao { get; set; }

        public virtual SituacaoEntity Situacao { get; set; }
    }
}