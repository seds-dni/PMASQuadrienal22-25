using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class QuestoesDRADSInfo
    {
        [Key]
        [DataMember]
        public Int32 Id { get; set; }

        [Key]
        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        [DataMember]
        public Int32? QuestaoUm { get; set; }
        public string QuestaoUmEscrita { get; set; }
        public Int32? QuestaoDois { get; set; }
        public Int32? QuestaoTres { get; set; }
        public Int32? QuestaoQuatro { get; set; }
        public Int32? QuestaoCinco { get; set; }
        public string QuestaoCincoEscrita { get; set; }
        public Int32? Exercicio { get; set; }
    }
}
