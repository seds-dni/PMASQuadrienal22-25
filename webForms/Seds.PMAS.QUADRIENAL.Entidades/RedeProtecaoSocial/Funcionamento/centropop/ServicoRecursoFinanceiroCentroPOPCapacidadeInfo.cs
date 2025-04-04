﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroCentroPOPCapacidadeInfo 
    {
        [DataMember]
        [Key]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdServicoRecursoFinanceiroCentroPOP{ get; set; }

        [DataMember]
        public Int32? Capacidade { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }

        public ServicoRecursoFinanceiroCentroPOPInfo ServicoRecursoFinanceiroCentroPOP { get; set; }

    }
}
