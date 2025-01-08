using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class CentroPOPMunicipioInfo
    {
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdCentroPop { get; set; }
        public CentroPOPInfo CentroPOP { get; set; }

        [DataMember]
        public Int32 IdMunicipio { get; set; }
        public String Municipio { get; set; }

        [DataMember]
        public Int32? NumeroAtendidos { get; set; }

        [DataMember]
        public Int32 IdTipoAtendimento { get; set; }
        public TipoAtendimentoInfo TipoAtendimento { get; set; }



    }
}
