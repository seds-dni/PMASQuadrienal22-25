using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{    
    /// <summary>
    /// VW_CENTRO_POP
    /// </summary>
    [DataContract]
    public class ConsultaCentroPOPInfo
    {
        public Int32 IdUnidade { get; set; }
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public String Coordenador { get; set; }
        [DataMember]
        public String CofinanciadoPeloEstado { get; set; }
        [DataMember]
        public Int32 TotalServicos { get; set; }
        [DataMember]
        public Decimal PrevisaoOrcamentaria { get; set; }
        [DataMember]
        public Decimal ValorCofinanciamentoEstadual { get; set; }
        [DataMember]
        public Int32 NumeroAtendidos { get; set; }
        [DataMember]
        public String IdCREAS { get; set; }

        public Boolean Desativado { get; set; }

        public Int32 TotalServicosDesativados { get; set; }

        public Int32? IdMotivoDesativacao { get; set; }

        public String MotivoDesativacao { get; set; }

        public Int32? IdMotivoEncerramento { get; set; }

        public String MotivoEncerramento { get; set; }

        public string Detalhamento { get; set; }

        public String Descricao { get { return Id + " - " + Nome; } }

        public DateTime? DataEncerramento { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }
    }
}
