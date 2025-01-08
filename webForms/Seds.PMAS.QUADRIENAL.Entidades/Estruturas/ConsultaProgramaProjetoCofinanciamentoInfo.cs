using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class ConsultaProgramaProjetoCofinanciamentoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdUnidade { get; set; }
        [DataMember]
        public Int32 IdServicoRecursoFinanceiro { get; set; }        
        [DataMember]
        public Int32 IdTipoUnidade { get; set; }
        [DataMember]
        public Int32 IdProgramaProjeto { get; set; }
        [DataMember]
        public String TipoUnidade { get; set; }
        [DataMember]
        public String Unidade { get; set; }
        [DataMember]
        public Int32 IdUsuarioTipoServico { get; set; }
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
        [DataMember]
        public Boolean ServicoEstadualizado { get; set; }
        [DataMember]
        public Decimal PrevisaoOrcamentaria { get; set; }
        [DataMember]
        public Int32 NumeroUsuarios { get; set; }
    }
}
