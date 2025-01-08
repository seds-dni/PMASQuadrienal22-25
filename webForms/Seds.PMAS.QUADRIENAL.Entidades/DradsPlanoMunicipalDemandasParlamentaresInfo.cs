using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class DradsPlanoMunicipalDemandasParlamentaresInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        //[DataMember]
        //public Int32 IdDradsPlanoMunicipalRecursosInfo { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        #region Demandas

        [DataMember]
        public Decimal? ValorProtecaoSocialBasicaDemandas { get; set; }

        [DataMember]
        public Decimal? ValorProtecaoSocialMediaDemandas { get; set; }

        [DataMember]
        public Decimal? ValorProtecaoSocialAltaDemandas { get; set; }

        [DataMember]
        public Decimal? ValorBeneficioEventuaisDemandas { get; set; }

        //[DataMember]
        //public Decimal? ValorProgramaProjetoDemandas { get; set; }
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
