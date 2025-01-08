using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    /// <summary>
    /// VW_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO
    /// </summary>
    [DataContract]
    public class ConsultaServicosRecursosFinanceirosPublicoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdLocalExecucao { get; set; }
        [DataMember]
        public Int32 IdUsuarioTipoServico { get; set; }
        [DataMember]
        public String Usuario { get; set; }
        [DataMember]
        public Int32 IdTipoServico { get; set; }
        [DataMember]
        public String TipoServico { get; set; }
        [DataMember]
        public Int16 IdTipoProtecao { get; set; }
        [DataMember]
        public String ProtecaoSocial { get; set; }
        [DataMember]
        public Int32 PrevisaoAnualNumeroAtendidos { get; set; }
        [DataMember]
        public Int32 PrevisaoMensalNumeroAtendidos { get; set; }
        [DataMember]
        public String Abrangencia { get; set; }
        //[DataMember]
        //public Boolean ServicoEstadualizado { get; set; }
        [DataMember]
        public Decimal PrevisaoOrcamentaria { get; set; }
        [DataMember]
        public Decimal ValorCofinanciamentoEstadual { get; set; }

        [DataMember]
        public Decimal ValorCofinanciamentoEstadualReprogramado { get; set; }

        public DateTime? DataFuncionamentoServico { get; set; }


        public Boolean Desativado { get; set; }

        public DateTime? DataExclusaoServico { get; set; }

        public Int32? IdMotivoDesativacao { get; set; }

        public String MotivoDesativacao { get; set; }

        public DateTime? DataEncerramentoServico { get; set; }

        public String DetalhamentoEncerramento { get; set; }


        [DataMember]
        public int TotalServicosAssociados { get; set; }

        public String Descricao { get { return "Proteção " + ProtecaoSocial + " - " + TipoServico + " - " + Usuario; } }

        [DataMember]
        public Int32 Exercicio { get; set; }
    }
}
