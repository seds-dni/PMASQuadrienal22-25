using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class DeliberacaoPrestacaoDeContasDRADSInfo
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [Key]
        [DataMember]
        public int IdPrefeitura { get; set; }

        [DataMember]
        public Int32 QuestaoDeliberacao { get; set; }
        public DateTime DataReuniao { get; set; }
        public string NumeroConselheiros { get; set; }
        public string NumeroAta { get; set; }
        public string NumeroResolucao { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int Exercicio { get; set; }

    }
}
