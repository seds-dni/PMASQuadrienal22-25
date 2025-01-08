using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados do Órgão Gestor Municipal
    /// </summary>
    [DataContract]
    public class EquipeEspecificaTotaisInfo
    {
        #region chaves
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdOrgaoGestor { get; set; } 
        #endregion

        #region totais
        [DataMember]
        public Int16 TotalFuncionarios { get; set; }
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
        public Int32 TotalEstatutarios { get; set; }
        [DataMember]
        public Int32 TotalCeletistas { get; set; }
        [DataMember]
        public Int32 TotalComissionados { get; set; }
        [DataMember]
        public Int32 TotalOutrosVinculos { get; set; }
        [DataMember]
        public Int32 TotalVoluntarios { get; set; } 
        #endregion

        #region possui equipe especifica
        [DataMember]
        public Int32? PossuiEquipeGestaoSUAS { get; set; }
        [DataMember]
        public Int32? PossuiEquipeRegulacaoSUAS { get; set; }
        [DataMember]
        public Int32? PossuiEquipeRedeDireta { get; set; }
        [DataMember]
        public Int32? PossuiOutrasEquipes { get; set; }
        [DataMember]
        public Int32? PossuiEquipeProtecaoBasica { get; set; }
        [DataMember]
        public Int32? PossuiEquipeProtecaoEspecial { get; set; }
        [DataMember]
        public Int32? PossuiEquipeVigilanciaSocioassistencial { get; set; }
        [DataMember]
        public Int32? PossuiEquipeGestaoTransferenciaRenda { get; set; }
        [DataMember]
        public Int32? PossuiEquipeCadUnico { get; set; }
        [DataMember]
        public Int32? PossuiEquipeGestaoFinanceira { get; set; }
        #endregion

        #region extras
        public Int32 TotalTrabalhadores { get; set; }
        public Int32 TotalTrabalhadoresSuperior { get; set; }
        #endregion

        #region flags
        [DataMember]
        public Int32? IdSituacao { get; set; }
        [DataMember]
        public Int32? Exercicio { get; set; }
        [DataMember]
        public Boolean? Desbloqueado { get; set; } 
        #endregion


        #region navegacao
        public OrgaoGestorInfo OrgaoGestorInfo { get; set; }
        #endregion
    }
}
