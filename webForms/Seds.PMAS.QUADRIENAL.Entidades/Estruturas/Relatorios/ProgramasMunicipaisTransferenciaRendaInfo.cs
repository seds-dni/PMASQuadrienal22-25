using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_PROGRAMAS_TRANSFERENCIA_RENDA_MUNICIPAL]
    /// </summary>
    [DataContract]
    public class ProgramasMunicipaisTransferenciaRendaInfo
    {
        [DataMember]
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
        public String NomePrograma { get; set; }
        [DataMember]
        public String Beneficiarios { get; set; }
        [DataMember]
        public Int32 NumeroBeneficiarios { get; set; }
        [DataMember]
        public Decimal Repasse { get; set; }

        [DataMember]
        public String PossuiParceria { get; set; }
        [DataMember]
        public Int32 TotalParcerias { get; set; }

        [DataMember]
        public String PossuiIntegracao { get; set; }
        [DataMember]
        public Int32 TotalServicosAssociados { get; set; }
    }
}

