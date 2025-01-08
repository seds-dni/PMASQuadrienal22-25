using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class ConsultaPrefeituraAcaoPlanejamentoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdEixoAcaoPlanejamento { get; set; }
        [DataMember]
        public Int32 IdAcaoPlanejamento { get; set; }
        [DataMember]
        public String Eixo { get; set; }
        [DataMember]
        public String Identificacao { get; set; }
        [DataMember]
        public Decimal PrevisaoOrcamentaria { get; set; }

        public String PrevisaoExecucao { get; set; }

        public Int32 Situacao { get; set; }
        public String SituacaoComentario { get; set; }


        public Int32 anoInicial { get { return Convert.ToInt32(PrevisaoExecucao.Substring(PrevisaoExecucao.IndexOf('2'), 4));} }
        public Int32 Exercicio { get; set; }

        public bool Liberar { get; set; }


    }
}
