using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class QuestoesCMASinfo
    {
        [Key]
        [DataMember]
        public Int32 Id {get;set;}

        [Key]
        [DataMember]
        public Int32 IdPrefeitura {get;set;}
        
        [DataMember]
        public Int32? QuestaoUm {get;set;}    
        public Int32? QuestaoDois {get;set;}       
        public Int32? QuestaoTres {get;set;}     
        public Int32? QuestaoQuatro {get;set;}     
        public Int32? QuestaoCinco {get;set;}   
        public Int32? QuestaoSeis {get;set;}    
        public string QuestaoSeisEscrita {get;set;}
        public Int32? QuestaoSete  {get;set;}
        public string  QuestaoSeteEscrita {get;set;}
        public Int32? QuestaoOito {get;set;}
        public Int32? QuestaoNove {get;set;}
        public Int32? Exercicio { get; set; }

    }
}
