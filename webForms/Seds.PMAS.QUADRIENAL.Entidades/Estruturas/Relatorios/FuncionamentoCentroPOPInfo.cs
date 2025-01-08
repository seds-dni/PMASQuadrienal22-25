using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class FuncionamentoCentroPOPInfo
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
        public Int32 Id { get; set; }
        [DataMember]
        public string IDCREAS { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Bairro { get; set; }
        [DataMember]
        public Int32 NumeroAtendidos { get; set; }       
        [DataMember]
        public Int32 DiasSemana { get; set; }
        [DataMember]
        public string HorasSemana { get; set; }
        [DataMember]
        public Int32 Funcionarios { get; set; }

        [DataMember]
        public string PossuiServicoEspecializadoSituacaoRuaCriancas { get; set; }
        [DataMember]
        public string PossuiServicoEspecializadoSituacaoRuaJovens { get; set; }
        [DataMember]
        public string PossuiServicoEspecializadoSituacaoRuaAdultos { get; set; }
        [DataMember]
        public string PossuiServicoAbordagemCriancasJovens { get; set; }
        [DataMember]
        public string PossuiServicoAbordagemJovensAdultosIdososFamilias { get; set; }
        [DataMember]
        public string PossuiServicoAbordagemCriancasJovensAdultosIdososFamilias { get; set; }
        [DataMember]
        public string PossuiServicoNaoTipificado { get; set; }
        [DataMember]
        public String DataImplantacao { get; set; }
    }
}
