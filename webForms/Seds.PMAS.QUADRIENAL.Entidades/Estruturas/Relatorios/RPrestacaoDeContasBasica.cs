using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; 

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class RPrestacaoDeContasBasica
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
        public Decimal SomaProtecaoBasica { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosBasica { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoBasica { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosBasica { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoBasica { get; set; }


        [DataMember]
        public Decimal SomaProtecaoBasicaReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosBasicaReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoBasicaReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosBasicaReprogramacao { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoBasicaReprogramacao { get; set; }


        [DataMember]
        public Decimal SomaProtecaoBasicaDemandas { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosBasicaDemandas { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoBasicaDemandas { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosBasicaDemandas { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoBasicaDemandas { get; set; }


        [DataMember]
        public Decimal SomaProtecaoBasicaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosBasicaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoBasicaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosBasicaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoBasicaReprogramacaoDemandas { get; set; }

    }
}
