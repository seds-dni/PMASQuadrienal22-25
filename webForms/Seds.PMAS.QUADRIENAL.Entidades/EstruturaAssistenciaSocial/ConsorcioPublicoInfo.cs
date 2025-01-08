using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial
{
    [DataContract]
    public class ConsorcioPublicoInfo
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public int IdServicosRecursosFinanceirosPublico { get; set; }
        
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
