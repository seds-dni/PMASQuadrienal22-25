using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class LocaisExecucaoPrestacaoDeContasInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        [DataMember]
        public Int32 IdTipoUnidade { get; set; }

        [DataMember]
        public Int32 CapacidadeDeAtendimento { get; set; }

        [DataMember]
        public Int32 MediaMensalDeAtendimento { get; set; }

        [DataMember]
        public Decimal CofinanciamentoEstadual { get; set; }

        [DataMember]
        public Decimal MaterialDeConsumo { get; set; }

        [DataMember]
        public Decimal OutrasDespesas { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }

        [DataMember]
        public Decimal RecursosHumanos { get; set; }

        [DataMember]
        public Decimal RecursosReprogramadosAnoAnterior { get; set; }

        [DataMember]
        public String TipoUnidade { get; set; }

        [DataMember]
        public String UnidadeResponsavel { get; set; }

        [DataMember]
        public Int16 IdTipoProtecao { get; set; }
        
        [DataMember]
        public Int32 IdTipoServico { get; set; }       

        [DataMember]
        public String TipoServico { get; set; }

        [DataMember]
        public String Usuario { get; set; }

        [DataMember]
        public Decimal ValorAplicacoesFinanceiras { get; set; }

        [DataMember]
        public Decimal ValoresDemandasParlamentares { get; set; }

        [DataMember]
        public Decimal ValoresDemandasParlamentaresReprogramados { get; set; }

        public String ConcatID { get {return "IdServicosRecursosFinanceiros: "+ Convert.ToString(Id)+" IdTipoProtecao: "+Convert.ToString(IdTipoProtecao) ;} }


    }
}
