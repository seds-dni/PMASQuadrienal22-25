using System;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.Web.ViewModels
{
    public class PrefeitoViewModel
    {
        [Key]
        public int Id { get; set; }


        public int IdPrefeitura { get; set; }


        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo RG")]
        [MaxLength(11, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(9, ErrorMessage = "Minimo {0} caracteres")]
        public string RG { get; set; }

        public string RGDigito { get; set; }

        [Required(ErrorMessage = "Preencha o campo data da emissão do RG")]
        public String DataEmissao { get; set; }

        [Required(ErrorMessage = "Preencha o campo Sigla do órgão emissor")]
        [MaxLength(5, ErrorMessage = "Máximo {0 caracteres")]
        [MinLength(3, ErrorMessage = "Minimo {0} caracteres")]
        public string SiglaEmissor { get; set; }

        [Required(ErrorMessage = "Preencha o campo CPF")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Preencha o campo UF")]
        public string UF { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public String InicioMandato { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Range(double.Parse(DateTime.MinValue.Year.ToString()), double.Parse(DateTime.Now.AddYears(4).Year.ToString()))]
        public String TerminoMandato { get; set; }

        [Required(ErrorMessage = "Preencha o campo E-mail")]
        [MaxLength(80, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(10, ErrorMessage = "Minimo {0} caracteres")]
        [EmailAddress(ErrorMessage = "Preencha um e-mail Válido")]
        public string Email { get; set; }

        public Int16 Status { get; set; }
    }
}