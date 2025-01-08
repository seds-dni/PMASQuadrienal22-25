using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_ACOES_AVALIACAO]
    /// </summary>
    [DataContract]
    public class AcaoAvaliacaoInfo
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
        public String AvaliaAcoes { get; set; }        
        [DataMember]
        public String AvaliadoOrgaoGestor { get; set; }
        [DataMember]
        public String AvaliadoTerceirizado { get; set; }
        [DataMember]
        public String RealizaPesquisa { get; set; }
        [DataMember]
        public String LevantamentoOpiniao { get; set; }
        [DataMember]
        public String LevantamentoDados { get; set; }
        [DataMember]
        public String AnaliseRegistros { get; set; }
        [DataMember]
        public String UtilizacaoIndicadores { get; set; }
        
    }
}

