using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class CREASInfo : CentroReferenciaInfo
    {
        [DataMember]
        public String IDCREAS { get; set; }
        [DataMember]
        public Boolean PossuiPAEFI { get; set; }
        [DataMember]
        public String JustificativaPAEFI { get; set; }
        [DataMember]
        public Boolean AtendeOutrosMunicipios { get; set; }
        [DataMember]
        public Int32? NumeroAtendidosOutrosMunicipios { get; set; }
        //public List<CREASMunicipioInfo> CreasMunicipios { get; set; }
        /// <summary>
        /// Lista de IDs dos Municípios oriundos do Servico DivisaoAdministravica.svc (DBSEDS)
        /// </summary>
        [DataMember]
        public List<CREASMunicipioInfo> AbrangenciaMunicipios { get; set; }

        public bool PAEFIAtivo { get; set; }
    }
}
