﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroPrivadoMediaMensalInfo 
    {
        [DataMember]
        [Key]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdServicoRecursoFinanceiroPrivado { get; set; }

        [DataMember]
        public Int32? MediaMensal { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }

        public ServicoRecursoFinanceiroPrivadoInfo ServicoRecursoFinanceiroPrivado { get; set; }

    }
}
