using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class DistribuicaoPorteNivelGestaoInfo
    {
        [DataMember]
        public String Porte { get; set; }
        [DataMember]
        public Int32 Inicial { get; set; }
        [DataMember]
        public Int32 Basica { get; set; }
        [DataMember]
        public Int32 Plena { get; set; }
        [DataMember]
        public Int32 NaoHabilitado { get; set; }
        [DataMember]
        public Int32 Total { get; set; }
        [DataMember]
        public Decimal Porcentagem { get; set; }
    }
}
