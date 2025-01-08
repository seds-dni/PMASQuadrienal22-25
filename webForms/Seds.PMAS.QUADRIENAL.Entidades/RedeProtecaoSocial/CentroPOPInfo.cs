using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class CentroPOPInfo : CentroReferenciaInfo
    {
        [DataMember]
        public String IDCREAS { get; set; }
        [DataMember]
        public Boolean? PossuiServicoEspecializadoSituacaoRua { get; set; }
        [DataMember]
        public String JustificativaServicoEspecializadoSituacaoRua { get; set; }

        [DataMember]
        public List<UsuarioTipoServicoInfo> Usuarios { get; set; }

        [DataMember]
        public Boolean AtendeOutrosMunicipios { get; set; }
        [DataMember]
        public Int32? NumeroAtendidosOutrosMunicipios { get; set; }
        [DataMember]
        public List<CentroPOPMunicipioInfo> AbrangenciaMunicipios { get; set; }

        public bool SituacaoRuaAtivo { get; set; }
    }
}
