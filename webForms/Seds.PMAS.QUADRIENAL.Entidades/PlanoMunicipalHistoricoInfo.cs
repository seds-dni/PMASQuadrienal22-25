using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{


    [DataContract]
    public class PlanoMunicipalHistoricoInfo
    {

        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public PrefeituraInfo Prefeitura { get; set; }
        [DataMember]
        public Int32 Revisao { get; set; }

        [DataMember]
        public Int32 IdUsuario { get; set; }
        [DataMember]
        public String Descricao { get; set; }
        [DataMember]
        public DateTime Data { get; set; }

        [DataMember]
        public Int32 IdSituacao { get; set; }
        [DataMember]
        public SituacaoInfo Situacao { get; set; }

        public List<PlanoMunicipalHistoricoConsolidadoInfo> PlanosMunicipaisHistoricoConsolidados { get; set; }
    }
}
