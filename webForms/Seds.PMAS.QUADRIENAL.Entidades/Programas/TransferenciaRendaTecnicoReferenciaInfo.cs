using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Programas
{
    [DataContract]
    public class TransferenciaRendaTecnicoReferenciaInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        
        [DataMember]
        public Int32 IdTransferenciaRenda { get; set; }
        public TransferenciaRendaInfo TransferenciaRenda { get; set; }

        public String NomeTecnico { get; set; }

        public String NomeEmail { get; set; }

        public String NomeUnidadeLotacao { get; set; }

    }
}
