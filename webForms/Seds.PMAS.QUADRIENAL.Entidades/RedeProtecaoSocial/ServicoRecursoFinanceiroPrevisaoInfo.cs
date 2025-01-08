using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ServicoRecursoFinanceiroPrevisaoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 PrevisaoMensalNumeroAtendidos { get; set; }

        public Int32 Exercicio { get; set; }
        
    }
}
