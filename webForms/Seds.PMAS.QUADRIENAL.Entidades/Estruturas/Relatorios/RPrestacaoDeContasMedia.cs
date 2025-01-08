using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class RPrestacaoDeContasMedia
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
        public Decimal SomaProtecaoMedia { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosMedia { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoMedia { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosMedia { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoMedia { get; set; }


        [DataMember]
        public Decimal SomaProtecaoMediaReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosMediaReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoMediaReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosMediaReprogramacao { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoMediaReprogramacao { get; set; }


        [DataMember]
        public Decimal SomaProtecaoMediaDemandas { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosMediaDemandas { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoMediaDemandas { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosMediaDemandas { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoMediaDemandas { get; set; }


        [DataMember]
        public Decimal SomaProtecaoMediaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosMediaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoMediaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosMediaReprogramacaoDemandas { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoMediaReprogramacaoDemandas { get; set; }
    }
}
