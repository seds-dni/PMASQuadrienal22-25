using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ConselhoMunicipalPresidenteAnteriorInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdConselhoMunicipal { get; set; }
        [DataMember]
        public ConselhoMunicipalInfo ConselhoMunicipal { get; set; }
        [DataMember]
        public Int32 IdUsuarioPresidente { get; set; }
        [DataMember]
        public DateTime DataInicioMandato { get; set; }
        [DataMember]
        public DateTime DataTerminoMandato { get; set; }
     
        public String Nome { get; set; }

        public String CPF { get; set; }

        public String RG { get; set; }

        public String RGDigito { get; set; }

        public DateTime? DataEmissao { get; set; }

        public Int16? IdUF { get; set; }

        public String SiglaEmissor { get; set; }
    }
}
