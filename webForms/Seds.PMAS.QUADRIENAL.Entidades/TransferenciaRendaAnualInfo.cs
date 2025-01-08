using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class TransferenciaRendaAnualInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 TipoTransferencia { get; set; }
        [DataMember]
        public Decimal ValorAnualMunicipal { get; set; }
        [DataMember]
        public Decimal ValorAnualEstadual { get; set; }
        [DataMember]
        public Decimal ValorAnualFederal { get; set; }
        [DataMember]
        public Decimal ValorAnualPrivado { get; set; }
    }
}
