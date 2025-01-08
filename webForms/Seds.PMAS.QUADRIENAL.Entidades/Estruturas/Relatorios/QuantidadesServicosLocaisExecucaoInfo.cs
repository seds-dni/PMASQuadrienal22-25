using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class QuantidadesServicosLocaisExecucaoInfo
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
        public Int32 TotalUnidadesPublicas { get; set; }
        [DataMember]
        public Int32 TotalLocaisPublicos { get; set; }
        [DataMember]
        public Int32 TotalServicosPublicos { get; set; }
        [DataMember]
        public Int32 TotalUnidadesPrivadas { get; set; }
        [DataMember]
        public Int32 TotalLocaisPrivados { get; set; }
        [DataMember]
        public Int32 TotalServicosPrivados { get; set; }
        [DataMember]
        public Int32 TotalCRAS { get; set; }        
        [DataMember]
        public Int32 TotalServicosCRAS { get; set; }
        [DataMember]
        public Int32 TotalCREAS { get; set; }
        [DataMember]
        public Int32 TotalServicosCREAS { get; set; }
        [DataMember]
        public Int32 TotalCentroPOP { get; set; }
        [DataMember]
        public Int32 TotalServicosCentroPOP { get; set; }
       
    }
}
