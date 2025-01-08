using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class ConsultaUnidadePrivadaDesativadaInfo
    {
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String RazaoSocial { get; set; }
        [DataMember]
        public String InscricaoCMAS { get; set; }
        [DataMember]
        public String CNPJ { get; set; }
        [DataMember]
        public Int32 TotalLocais { get; set; }
        [DataMember]
        public String CofinanciadoPeloEstado { get; set; }
        [DataMember]
        public String Estadualizado { get; set; }
        [DataMember]
        public Decimal PrevisaoOrcamentaria { get; set; }
        [DataMember]
        public Decimal ValorCofinanciamentoEstadual { get; set; }
        [DataMember]
        public Int32 NumeroAtendidos { get; set; }
        [DataMember]
        public Int32 IdFormaAtuacao { get; set; }
        [DataMember]
        public String FormaAtuacao { get; set; }
        public String Descricao { get { return Id + " - " + RazaoSocial; } }

        public Boolean Desativado { get; set; }

        public Int32? IdMotivoDesativacao { get; set; }

        public Int32? IdMotivoEncerramento { get; set; }

        public DateTime? DataDesativacao { get; set; }

        public String Detalhamento { get; set; }

        public DateTime? DataRegistroLog { get; set; }

        public Int32 TotalLocaisDesativados { get; set; }

    }


}
