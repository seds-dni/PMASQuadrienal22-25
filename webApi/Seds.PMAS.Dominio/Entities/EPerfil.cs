using System.Runtime.Serialization;

namespace Seds.PMAS.Dominio.Entities
{
    
        public enum EPerfil : short
        {
            Inexistente = 0,
            //Bruno V.
            [EnumMember]
            DRADSAdministrador = 65,
            [EnumMember]
            SEDS = 66,
            [EnumMember]
            CAS = 67,
            [EnumMember]
            Administrador = 68,
            [EnumMember]
            Convidados = 69,
            [EnumMember]
            DRADS = 70,
            [EnumMember]
            OrgaoGestor = 64,
            [EnumMember]
            CMAS = 71,
        }
}
