using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrestacaoDeContasDespesasInfo
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdPrefeitura { get; set; }

        public int IdTipoProtecao { get; set; }

        public int IdServicosRecursosFinanceiros { get; set; }

        public decimal RecursosHumanos { get; set; }

        public decimal MaterialDeConsumo { get; set; }

        public decimal OutrasDespesas { get; set; }

        public int Exercicio { get; set; }

    }
}
