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
    public class ConselhosMunicipaisExistentesInfo
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
        public Int32 IdPorte { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Boolean CMAS { get; set; }
        [DataMember]
        public Boolean CMDCA { get; set; }
        [DataMember]
        public Int32 CT { get; set; }
        [DataMember]
        public Boolean CMI { get; set; }
        [DataMember]
        public Boolean PCD { get; set; }
        [DataMember]
        public Boolean CONSEA { get; set; }
        [DataMember]
        public Boolean CJ { get; set; }
        [DataMember]
        public Boolean CME { get; set; }
        [DataMember]
        public Int32 Outros { get; set; }
    }
}

