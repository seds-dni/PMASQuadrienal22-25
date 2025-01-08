using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ProgramaProjetoParcelasInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [Key]
        [DataMember]
        public Int32 IdProgramaProjeto { get; set; }

        #region Amigo do Idoso - Dia do idoso
        [DataMember]
        public Decimal? ValorDiaIdoso { get; set; }
        [DataMember]
        public Int16? MesRepasseDiaIdoso { get; set; }
        [DataMember]
        public Int16? AnoRepasseDiaIdoso { get; set; }
        #endregion

        #region Amigo do Idoso - Convivência do idoso
        [DataMember]
        public Decimal? ValorConvivenciaIdoso { get; set; }
        [DataMember]
        public Int16? MesRepasseConvivenciaIdoso { get; set; }
        [DataMember]
        public Int16? AnoRepasseConvivenciaIdoso { get; set; }
        #endregion


        [Key]
        [DataMember]
        public Int32 Exercicio { get; set; }


        [DataMember]
        public ProgramaProjetoInfo ProgramaProjeto { get; set; }
    }
}
