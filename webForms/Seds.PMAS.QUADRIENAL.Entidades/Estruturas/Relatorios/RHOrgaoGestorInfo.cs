using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_CONSELHOS_MUNICIPIOS]
    /// </summary>
    [DataContract]
    public class RHOrgaoGestorInfo
    {
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
        public string Denominacao { get; set; }

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
        public Int32 TotalFuncionariosSuperiorDireito { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSemEscolaridade { get; set; }
        [DataMember]
        public Int32 TotalEstagiarios { get; set; }

        public Int32 TotalEstatutarios { get; set; }

        public Int32 TotalCeletistas { get; set; }

        public Int32 TotalComissionados { get; set; }

        public Int32 TotalOutrosVinculos { get; set; }

        public Int32 TotalVoluntarios { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorAdministracao { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorAntropologia { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorContabilidade { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorEconomia { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorEconomiaDomestica { get; set; }
        [DataMember]
        public Int32 TotalFuncionariosSuperiorTerapiaOcupacional { get; set; }

        [DataMember]
        public Int32 TotalFuncionariosOutrasAreas { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }

    }
}

