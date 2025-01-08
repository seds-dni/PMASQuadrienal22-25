using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class CronogramaDesembolsoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdTipoUnidade { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }

        [DataMember]
        public Int16 IdTipoProtecaoSocial { get; set; }
        [DataMember]
        public TipoProtecaoSocialInfo TipoProtecaoSocial { get; set; }


        [DataMember]
        public Int32 IdAbrangencia { get; set; }
        [DataMember]
        public String Abrangencia { get; set; }

        [DataMember]
        public Decimal ValorRHMes1 { get; set; }
        [DataMember]
        public Decimal ValorRHMes2 { get; set; }
        [DataMember]
        public Decimal ValorRHMes3 { get; set; }
        [DataMember]
        public Decimal ValorRHMes4 { get; set; }
        [DataMember]
        public Decimal ValorRHMes5 { get; set; }
        [DataMember]
        public Decimal ValorRHMes6 { get; set; }
        [DataMember]
        public Decimal ValorRHMes7 { get; set; }
        [DataMember]
        public Decimal ValorRHMes8 { get; set; }
        [DataMember]
        public Decimal ValorRHMes9 { get; set; }
        [DataMember]
        public Decimal ValorRHMes10 { get; set; }
        [DataMember]
        public Decimal ValorRHMes11 { get; set; }
        [DataMember]
        public Decimal ValorRHMes12 { get; set; }

        [DataMember]
        public Decimal ValorMaterialConsumoMes1 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes2 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes3 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes4 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes5 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes6 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes7 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes8 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes9 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes10 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes11 { get; set; }
        [DataMember]
        public Decimal ValorMaterialConsumoMes12 { get; set; }

        [DataMember]
        public Decimal ValorServicosTerceirosMes1 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes2 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes3 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes4 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes5 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes6 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes7 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes8 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes9 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes10 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes11 { get; set; }
        [DataMember]
        public Decimal ValorServicosTerceirosMes12 { get; set; }

         
        [DataMember]
        public Decimal? ValorInvestimentoMes1 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes2 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes3 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes4 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes5 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes6 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes7 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes8 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes9 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes10 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes11 { get; set; }
        [DataMember]
        public Decimal? ValorInvestimentoMes12 { get; set; }

         
        [DataMember]
        public Decimal? ObrasMes1 { get; set; }
        [DataMember]
        public Decimal? ObrasMes2 { get; set; }
        [DataMember]
        public Decimal? ObrasMes3 { get; set; }
        [DataMember]
        public Decimal? ObrasMes4 { get; set; }
        [DataMember]
        public Decimal? ObrasMes5 { get; set; }
        [DataMember]
        public Decimal? ObrasMes6 { get; set; }
        [DataMember]
        public Decimal? ObrasMes7 { get; set; }
        [DataMember]
        public Decimal? ObrasMes8 { get; set; }
        [DataMember]
        public Decimal? ObrasMes9 { get; set; }
        [DataMember]
        public Decimal? ObrasMes10 { get; set; }
        [DataMember]
        public Decimal? ObrasMes11 { get; set; }
        [DataMember]
        public Decimal? ObrasMes12 { get; set; }

        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes01 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes02 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes03 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes04 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes05 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes06 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes07 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes08 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes09 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes10 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes11 { get; set; }
        [DataMember]
        public Decimal? ValorOutrasDespesasCusteioMes12 { get; set; }


        [DataMember]
        public Decimal? ReprogramacaoRecursosDisponibilizados { get; set; }
        [DataMember]
        public Decimal? RecursosHumanosReprogramados { get; set; }
        [DataMember]
        public Decimal? OutrasDespesasReprogramados { get; set; }
        [DataMember]
        public Decimal? ReprogramacaoEquipamentosInvestimento { get; set; }
        [DataMember]
        public Decimal? ReprogramacaoObras { get; set; }


        [DataMember]
        public Decimal? DemandasParlamentaresDisponibilizados { get; set; }
        [DataMember]
        public Decimal? RecursosHumanosDemandasParlamentares { get; set; }
        [DataMember]
        public Decimal? OutrasDespesasDemandasParlamentares { get; set; }
        [DataMember]
        public Decimal? DemandasParlamentaresEquipamentosInvestimento { get; set; }
        [DataMember]
        public Decimal? DemandasParlamentaresObras { get; set; }

        [DataMember]
        public Decimal? ReprogramacaoDemandasParlamentaresDisponibilizados { get; set; }
        [DataMember]
        public Decimal? RecursosHumanosReprogramacaoDemandasParlamentares { get; set; }
        [DataMember]
        public Decimal? OutrasDespesasReprogramacaoDemandasParlamentares { get; set; }
        [DataMember]
        public Decimal? ReprogramacaoDemandasParlamentaresEquipamentosInvestimento { get; set; }
        [DataMember]
        public Decimal? ReprogramacaoDemandasParlamentaresObras { get; set; }

        public Int32 Exercicio { get; set; }
    }
}
