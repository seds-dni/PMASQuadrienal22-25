using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class RPrestacaoDeContasProgramasProjetos
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
        public Decimal SomaProtecaoProgramasProjetos { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosProgramasProjetos { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoProgramasProjetos { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosProgramasProjetos { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoProgramasProjetos { get; set; }


        [DataMember]
        public Decimal SomaProtecaoProgramasProjetosReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresExecutadosProgramasProjetosReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresPassiveisReprogramacaoProgramasProjetosReprogramacao { get; set; }

        [DataMember]
        public Decimal ValoresDevolvidosProgramasProjetosReprogramacao { get; set; }

        [DataMember]
        public Decimal PorcentagensExecucaoProgramasProjetosReprogramacao { get; set; }

    }
}
