﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    /// <summary>
    /// VW_LOCAL_EXECUCAO_PRIVADO
    /// </summary>
    [DataContract]
    public class ConsultaLocalExecucaoPrivadoInfo
    {
        public Int32 IdUnidade { get; set; }
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 Exercicio { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public String Responsavel { get; set; }
        [DataMember]
        public Int32 TotalServicos { get; set; }
        [DataMember]
        public Decimal PrevisaoOrcamentaria { get; set; }
        [DataMember]
        public Decimal ValorCofinanciamentoEstadual { get; set; }

        public String FORMA_ATUACAO { get; set; }

        public String Descricao { get { return Id + " - " + Nome; } }

        
        public Boolean Desativado { get; set; }

        public Int32? IdMotivoDesativacao { get; set; }

        public String DescricaoDesativacao { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public String DetalhamentoEncerramento { get; set; }

        public Int32 TotalServicosDesativados { get; set; }

    }
}
