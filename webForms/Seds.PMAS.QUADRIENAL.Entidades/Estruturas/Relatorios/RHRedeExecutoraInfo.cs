using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_RH_LOCAL_EXECUCAO]
    /// </summary>
    [DataContract]
    public class RHRedeExecutoraInfo
    {
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Drads { get; set; }
        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }

        [DataMember]
        public Int32 CodigoUnidade { get; set; }
        [DataMember]
        public Int32 IdTipoUnidade { get; set; }
        [DataMember]
        public String TipoUnidade { get; set; }
        [DataMember]
        public String UnidadeResponsavel { get; set; }
        [DataMember]
        public string IdLocal { get; set; }
        [DataMember]
        public String LocalExecucao { get; set; }
        [DataMember]
        public Int32? IdUsuarioTipoServico { get; set; }
        [DataMember]
        public Int32? IdTipoServico { get; set; }
        [DataMember]
        public Boolean? ServicoNaoTipificado { get; set; }
        [DataMember]
        public Int16? IdTipoProtecao { get; set; }



        public Int32 TotalFuncionarios { get { return TotalFuncionariosNivelFundamental + TotalFuncionariosNivelMedio + TotalFuncionariosSuperior + TotalFuncionariosSemEscolaridade; } }

        [DataMember]
        public Int32 TotalFuncionariosNivelFundamental { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosNivelMedio { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorServicoSocial { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorPsicologia { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorPedagogia { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorSociologia { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperior { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosPosGraduacao { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorDireito { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSemEscolaridade { get; set; }
        [DataMember]
        public Int32 TotalEstagiarios { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorMusicoterapia { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorAntropologia { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorEconomia { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorEconomiaDomestica { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorTerapiaOcupacional { get; set; }
        [DataMember]
        public Int32 TotalVoluntarios { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosOutrasAreas { get; set; }

        public Int32 TotalExclusivoServico { get; set; }

        public Int32 TotalOutrosServicosAssistenciais { get; set; }

        public string TipoServico { get; set; }

        public string Usuario { get; set; }


    }
}

