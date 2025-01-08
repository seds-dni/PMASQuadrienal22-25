using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.Entidades;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class UsuarioInfo
    {

        [DataMember]
        public Int32 IdUsuario { get; set; }
        [DataMember]
        public Int32? IdPrefeitura { get; set; }
        [DataMember]
        public PrefeituraInfo Prefeitura { get; set; }

        [DataMember]
        public Int32? IdDrads { get; set; }
        [DataMember]
        public DradsInfo Drads { get; set; }

        [DataMember]
        public Int32 IdPerfil { get; set; }
    }
}
