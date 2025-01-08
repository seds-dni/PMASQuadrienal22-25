using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Seds.Entidades;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class ConsultaFluxoInfo
    {

        public ConsultaFluxoInfo()
        {
            this.QuadrosExecucaoFinanceiraInner = new List<QuadroExecucaoFinanceiraInner>();
            this.QuadrosLeiOrcamentariaInner = new List<QuadroLeiOrcamentariaInner>();
            this.QuadrosPrestacaoDeContasInner = new List<QuadroPrestacaoDeContasInner>();
        }


        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [IgnoreDataMember]
        public String Municipio { get; set; }
        [DataMember]
        public SituacaoInfo Situacao { get; set; }

         
        [IgnoreDataMember]
        public String Drads { get; set; }

        public Boolean? DesbloquearValoresDrads { get; set; }

        public Boolean? ReprogramarValores { get; set; }


        [IgnoreDataMember]
        public List<QuadroExecucaoFinanceiraInner> QuadrosExecucaoFinanceiraInner { get; set; }

        [IgnoreDataMember]
        public List<QuadroLeiOrcamentariaInner> QuadrosLeiOrcamentariaInner { get; set; }

        [IgnoreDataMember]
        public List<QuadroPrestacaoDeContasInner> QuadrosPrestacaoDeContasInner { get; set; }

        public class QuadroExecucaoFinanceiraInner
        {
            [IgnoreDataMember]
            public String SituacaoQuadroExecucaoFinanceira { get; set; }
            [IgnoreDataMember]
            public Int32 Exercicio { get; set; }
        }

        public class QuadroLeiOrcamentariaInner
        {
            [IgnoreDataMember]
            public String SituacaoQuadroLeiOrcamentaria { get; set; }
            [IgnoreDataMember]
            public Int32 Exercicio { get; set; }
        }

        public class QuadroPrestacaoDeContasInner
        {
            [IgnoreDataMember]
            public String SituacaoQuadroPrestacaoDeContas { get; set; }
            [IgnoreDataMember]
            public Int32 Exercicio { get; set; }
        }

    }
}
