using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{    
    [DataContract]
    public class ConsultaPrefeituraBeneficioEventualInfo
    {
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdTipoBeneficioEventual { get; set; }
        [DataMember]
        public Int32? Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 Aderiu { get; set; }
        [DataMember]
        public Int32 TotalServicos { get; set; }
        [DataMember]
        public Int32 TotalUnidades { get; set; }
        [DataMember]
        public Decimal PrevisaoOrcamentaria { get; set; }
        [DataMember]
        public Int32 IntegracaoRede { get; set; }

        public Int32? NumeroBeneficiarios { get; set; }

        public int Exercicio { get; set; }

    }
}
