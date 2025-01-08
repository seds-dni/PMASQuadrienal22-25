using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class FuncionamentoCRASInfo
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
        public String IDCRAS { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Bairro { get; set; }
        [DataMember]
        public Int32 FamiliasReferenciadas { get; set; }
        [DataMember]
        public Int32 FamiliasAtendidas { get; set; }
        [DataMember]
        public Int32 DiasSemana { get; set; }
        [DataMember]
        public string HorasSemana { get; set; }
        [DataMember]
        public Int32 Funcionarios { get; set; }

        [DataMember]
        public string PossuiPAIF { get; set; }
        [DataMember]
        public string PossuiServicoConvivencia6anos { get; set; }
        [DataMember]
        public string PossuiServicoConvivencia15anos { get; set; }
        [DataMember]
        public string PossuiServicoConvivencia17anos { get; set; }
        [DataMember]
        public string PossuiServicoConvivencia60anos { get; set; }
        [DataMember]
        public string PossuiServicoProtecaoPessoasDeficientes { get; set; }
        [DataMember]
        public string PossuiServicoProtecaoPessoasIdosas { get; set; }
        [DataMember]
        public string PossuiServicoProtecaoPessoasDeficientesIdosas { get; set; }
        [DataMember]
        public string PossuiServicoNaoTipificado { get; set; }

        [DataMember]
        public string PossuiEquipeVolante { get; set; }
        [DataMember]
        public string DataImplantacao { get; set; }
    }
}
