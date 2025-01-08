using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class InterfacePublicaAlimentacaoInfo
    {
        public int Id { get; set; }
        public int IdPrefeitura { get; set; }
        public Boolean RestaurantePopular { get; set; }
        public Boolean DistribuicaoAlimentos { get; set; }
        public Boolean NaoPossuiInformacaoRestaurante { get; set; }
        public Boolean ExecutaDistribuicaoVivaleite { get; set; }
        public Boolean? OutraFormaDistribuicao { get; set; }
        public Boolean? OutraFormaAcao { get; set; }
        public List<InterfacePublicaAlimentacaoRestauranteInfo> Restaurantes { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }
        public List<InterfacePublicaDistribuicaoAlimentoInfo> DistribuicoesAlimentos { get; set; }
        public List<InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo> FormasDistribuicoesAlimentos { get; set; }
        public List<InterfacePublicaAlimentacaoOutraAcaoInfo> OutrasAcoes { get; set; }
        public Boolean? GestaoVivaleiteOrgaoGestor { get; set; }
        public Boolean ExecutaAcaoAlimentar { get; set; }
        public Boolean GestaoDiretaEstado { get; set; }
        public Int32? TipoDistribuidor { get; set; }
        public String DecricaoOutraPoliticaDistribuicao { get; set; }
    }
}
