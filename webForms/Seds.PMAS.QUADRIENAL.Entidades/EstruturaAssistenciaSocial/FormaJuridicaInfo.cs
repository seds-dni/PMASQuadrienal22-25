using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial
{
    [DataContract]
    public class FormaJuridicaInfo
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String NomeForma { get; set; }

    }
}
