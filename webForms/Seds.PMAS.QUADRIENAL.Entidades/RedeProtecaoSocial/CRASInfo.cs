using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class CRASInfo : CentroReferenciaInfo
    {
        [DataMember]
        public String IDCRAS { get; set; }
        [DataMember]
        public Boolean PossuiPAIF { get; set; }
        [DataMember]
        public String JustificativaPAIF { get; set; }

        [DataMember]
        public Boolean PossuiEquipeVolante { get; set; }
        [DataMember]
        public DateTime? DataPrevisaoEquipeVolante { get; set; }
        [DataMember]
        public Boolean SemPrevisaoEquipeVolante { get; set; }
        [DataMember]
        public String NomeLocaisAbrangenciaEquipeVolante { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteNivelMedio { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteNivelSuperior { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteSuperiorServicoSocial { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteSuperiorPsicologia { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteSuperiorPedagogia { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteSuperiorSociologia { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteSuperiorDireito { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteSuperiorTerapiaOcupacional { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteSuperiorMusicoterapia { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteSuperiorAntropologia { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteSuperiorEconomia { get; set; }
        [DataMember]
        public int TotalFuncionariosVolanteSuperiorEconomiaDomestica { get; set; }

        public bool PAIFAtivo { get; set; }
    }
}
