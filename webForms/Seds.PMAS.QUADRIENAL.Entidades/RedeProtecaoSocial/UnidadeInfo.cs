using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public abstract class UnidadeInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String CNPJ { get; set; }
        [DataMember]
        public String RazaoSocial { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }

        public Boolean Desativado { get; set; }

        public Int32? IdMotivoDesativacao { get; set; }

        public Int32? IdMotivoEncerramento { get; set; }

        public DateTime? DataDesativacao { get; set; }

        public String Detalhamento { get; set; }

        public DateTime? DataRegistroLog { get; set; }

    }
}
