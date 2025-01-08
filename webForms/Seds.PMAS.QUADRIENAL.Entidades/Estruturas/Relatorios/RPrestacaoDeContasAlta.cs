using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class RPrestacaoDeContasAlta
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
        public Decimal SomaProtecaoAlta { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosAlta { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoAlta { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosAlta { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoAlta { get; set; }


        [DataMember]
        public Decimal SomaProtecaoAltaReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosAltaReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoAltaReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosAltaReprogramacao { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoAltaReprogramacao { get; set; }


        [DataMember]
        public Decimal SomaProtecaoAltaDemandas { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosAltaDemandas { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoAltaDemandas { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosAltaDemandas { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoAltaDemandas { get; set; }


        [DataMember]
        public Decimal SomaProtecaoAltaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosAltaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoAltaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosAltaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoAltaReprogramacaoDemandas { get; set; }
    }
}
