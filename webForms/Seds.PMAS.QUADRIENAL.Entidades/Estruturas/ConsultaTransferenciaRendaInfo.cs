using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{    
    [DataContract]
    public class ConsultaTransferenciaRendaInfo
    {
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public Int32 IdTipoTransferenciaRenda { get; set; }
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 Aderiu { get; set; }
        [DataMember]
        public Int32 TotalServicos { get; set; }
        [DataMember]
        public Int32 TotalUnidades { get; set; }
        [DataMember]
        public Decimal PrevisaoAnualRepasse { get; set; }
        [DataMember]
        public Int32 IntegracaoRede { get; set; }
        [DataMember]
        public Int32 NumeroBeneficiarios { get; set; }

        public Int32? ValorMetaPactuada { get; set; }

        public Int32? Exercicio { get; set; }
    }
}
