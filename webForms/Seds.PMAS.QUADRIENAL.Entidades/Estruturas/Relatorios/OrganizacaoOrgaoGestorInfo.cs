using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_ORGANIZACAO_ORGAO_GESTOR]
    /// </summary>
    [DataContract]
    public class OrganizacaoOrgaoGestorInfo
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
        public String Estrutura { get; set; }
        [DataMember]
        public Int16 TotalFuncionarios { get; set; }
        [DataMember]
        public String FormacaoGestor { get; set; }
        [DataMember]
        public String EscolaridadeGestor { get; set; }

        [DataMember]
        public Int32 PossuiEquipeProtecaoBasica { get; set; }
        [DataMember]
        public Int32 PossuiEquipeProtecaoEspecial { get; set; }
        [DataMember]
        public Int32 PossuiEquipeVigilanciaSocioassistencial { get; set; }
        [DataMember]
        public Int32 PossuiEquipeTransferenciaRenda { get; set; }


     [DataMember]
        public Int32 Exercicio { get; set; }


        public Int32 PossuiEquipeCadUnico { get; set; }

        public Int32 PossuiEquipeGestaoFinanceira { get; set; }

        public Int32 PossuiEquipeGestaoSUAS { get; set; }

        public Int32 PossuiEquipeRegulacaoSUAS { get; set; }

        public Int32 PossuiEquipeRedeDireta { get; set; }

        public Int32 PossuiOutrasEquipes { get; set; }
    }
}

