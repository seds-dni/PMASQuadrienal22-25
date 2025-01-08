using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial
{
    [DataContract]
    public class ConsorcioPrivadoInfo
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdServicosRecursosFinanceirosPrivado { get; set; }

        [DataMember]
        public String NomeConsorcio { get; set; }

        [DataMember]
        public String CNPJ { get; set; }

        [DataMember]
        public String MunicipioSede { get; set; }

        [DataMember]
        public String MunicipioEnvolvido { get; set; }
    }
}
