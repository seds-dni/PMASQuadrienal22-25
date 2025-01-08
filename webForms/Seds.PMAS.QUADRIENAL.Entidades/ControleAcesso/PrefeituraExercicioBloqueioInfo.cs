using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class PrefeituraExercicioBloqueioInfo
    {
        #region chave multipla
        [Key]
        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        [Key]
        [DataMember]
        public Int32 Exercicio { get; set; }

        [Key]
        [DataMember]
        public Int32 IdRefBloqueio { get; set; } 
        #endregion

        [DataMember]
        public Boolean? Desbloqueado { get; set; }

        #region navigacao
        public PrefeituraInfo PrefeituraInfo { get; set; }
        #endregion
    }
}
