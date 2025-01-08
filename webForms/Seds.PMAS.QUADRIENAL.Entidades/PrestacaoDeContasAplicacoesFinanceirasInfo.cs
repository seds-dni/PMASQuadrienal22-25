using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrestacaoDeContasAplicacoesFinanceirasInfo
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdPrefeitura { get; set; }

        public int IdTipoProtecao { get; set; }

        public int IdServicosRecursosFinanceiros { get; set; }

        public Decimal ValorAplicacoesFinanceiras { get; set; }

        public int Exercicio { get; set; }
    }
}
