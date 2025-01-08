using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{

    [DataContract]
    public class DradsPlanoMunicipalRecursosReprogramadoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        
        //[DataMember]
        //public Int32 IdDradsPlanoMunicipalRecursosInfo { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        #region reprogramacao

        [DataMember]
        public Decimal? ValorProtecaoSocialBasicaReprogramado { get; set; }

        [DataMember]
        public Decimal? ValorProtecaoSocialMediaReprogramado { get; set; }

        [DataMember]
        public Decimal? ValorProtecaoSocialAltaReprogramado { get; set; }

        [DataMember]
        public Decimal? ValorBeneficioEventuaisReprogramado { get; set; }

        [DataMember]
        public Decimal? ValoresProgramasEProjetosReprogramado { get; set; }
        #endregion


        #region Navegacao
        [DataMember]
        public PrefeituraInfo Prefeitura { get; set; }
        #endregion

        [DataMember]
        public Boolean Desbloqueado { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }

    }
}
