using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Seds.Entidades;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class CREASMunicipioInfo
    {
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdCREAS { get; set; }
        public CREASInfo CREAS { get; set; }

        [DataMember]
        public Int32 IdMunicipio { get; set; }
        public String Municipio { get; set; }

        [DataMember]
        public Int32? NumeroAtendidos { get; set; }
        
        [DataMember]
        public Int32 IdTipoAtendimento {get; set; }
        public TipoAtendimentoInfo TipoAtendimento { get; set; }
    }
}
