using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    public class ConsultarServicosDiretrizesInfo
    {
        [DataMember]
        public Int32 IdServicoRecursosFinanceiros { get; set; }

        [DataMember]
        public Int32 IdUnidade { get; set; }

        [DataMember]
        public Int32 IdTipoUnidade { get; set; }

        [DataMember]
        public String TipoUnidade { get; set; }

        [DataMember]
        public String Unidade { get; set; }

        [DataMember]
        public Int32 IdUsuario { get; set; }

        [DataMember]
        public String Usuario { get; set; }

        [DataMember]
        public Int32 IdTipoServico { get; set; }

        [DataMember]
        public String TipoServico { get; set; }

        [DataMember]
        public Int16 IdTipoProtecao { get; set; }

        [DataMember]
        public String ProtecaoSocial { get; set; }

        [DataMember]
        public Int32 NumeroAtendidos { get; set; }

        [DataMember]
        public String Abrangencia { get; set; }
    }
}
