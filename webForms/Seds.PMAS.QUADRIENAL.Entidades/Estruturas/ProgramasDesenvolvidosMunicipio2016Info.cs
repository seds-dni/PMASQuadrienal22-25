using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class ProgramasDesenvolvidosMunicipio2016Info  
    {
        [DataMember]
        public Decimal ValorFederalACESSUAS { get; set; }
        [DataMember]
        public Decimal ValorMunicipalSPSolidario { get; set; }
        [DataMember]
        public Decimal ValorEstadualSPSolidario { get; set; }
        [DataMember]
        public Decimal ValorFederalSPSolidario { get; set; }
        [DataMember]
        public Decimal ValorEstadualAmigoIdoso { get; set; }
        [DataMember]
        public Decimal ValorMunicipalProgramas { get; set; }
        [DataMember]
        public Decimal ValorEstadualProgramas { get; set; }
        [DataMember]
        public Decimal ValorFederalProgramas { get; set; }
        [DataMember]
        public Decimal ValorEstadualRendaCidadaBeneficioIdoso { get; set; }
    }
}
