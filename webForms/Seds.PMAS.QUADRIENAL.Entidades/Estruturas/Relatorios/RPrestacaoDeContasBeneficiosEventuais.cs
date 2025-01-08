using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class RPrestacaoDeContasBeneficiosEventuais
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Drads { get; set; }
        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }
        [DataMember]
        public String Porte { get; set; }

        [DataMember]
        public Decimal SomaProtecaoBeneficiosEventuais { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosBeneficiosEventuais { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoBeneficiosEventuais { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosBeneficiosEventuais { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoBeneficiosEventuais { get; set; }


        [DataMember]
        public Decimal SomaProtecaoBeneficiosEventuaisReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosBeneficiosEventuaisReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosBeneficiosEventuaisReprogramacao { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoBeneficiosEventuaisReprogramacao { get; set; }


        [DataMember]
        public Decimal SomaProtecaoBeneficiosEventuaisDemandas { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosBeneficiosEventuaisDemandas { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoBeneficiosEventuaisDemandas { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosBeneficiosEventuaisDemandas { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoBeneficiosEventuaisDemandas { get; set; }


        [DataMember]
        public Decimal SomaProtecaoBeneficiosEventuaisReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosBeneficiosEventuaisReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosBeneficiosEventuaisReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoBeneficiosEventuaisReprogramacaoDemandas { get; set; }
    }
}
