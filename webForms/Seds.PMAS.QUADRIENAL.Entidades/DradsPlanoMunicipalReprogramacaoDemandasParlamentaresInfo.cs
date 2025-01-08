using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }


        #region Reprogramado Demandas

        [DataMember]
        public Decimal? ValorProtecaoSocialBasicaReprogramadoDemandas { get; set; }

        [DataMember]
        public Decimal? ValorProtecaoSocialMediaReprogramadoDemandas { get; set; }

        [DataMember]
        public Decimal? ValorProtecaoSocialAltaReprogramadoDemandas { get; set; }

        [DataMember]
        public Decimal? ValorBeneficioEventuaisReprogramadoDemandas { get; set; }

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
