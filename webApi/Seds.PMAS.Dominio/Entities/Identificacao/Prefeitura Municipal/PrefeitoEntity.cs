using System;
using System.Collections.Generic;

namespace Seds.PMAS.Dominio.Entities
{
    public class PrefeitoEntity
    {
        public int Id { get; set; }
        public int IdPrefeitura { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public string RGDigito { get; set; }
        public DateTime? DataEmissao { get; set; }
        public Int16 IdUFRG { get; set; }
        public string SiglaEmissor { get; set; }
        public string CPF { get; set; }
        public DateTime InicioMandato { get; set; }
        public DateTime TerminoMandato { get; set; }
        public string Email { get; set; }
        public Int16 Status { get; set; }
    }
}
