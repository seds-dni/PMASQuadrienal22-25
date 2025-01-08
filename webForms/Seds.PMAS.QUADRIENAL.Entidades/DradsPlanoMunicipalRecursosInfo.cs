using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{

    [DataContract]
    public class DradsPlanoMunicipalRecursosInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        
        //[DataMember]
        //public Int32 IdDradsPlanoMunicipalRecursosInfo { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }


        [DataMember]
        public Decimal? ValorProtecaoSocialBasica { get; set; }

        /// Alterar regra: O valor digitado deve ser igual à soma do cofinanciamento estadual (FEAS) de todos os serviços da Proteção Social Especial de Média Complexidade 
        [DataMember]
        public Decimal? ValorProtecaoSocialMediaComplexidade { get; set; }


        //Alterar regra: O valor digitado deve ser igual à soma do cofinanciamento estadual (FEAS) de todos os serviços da Proteção Social Especial de Alta Complexidade 
        [DataMember]
        public Decimal? ValorProtecaoSocialAltaComplexidade { get; set; }

        //- Benefícios Eventuais:
        //Alterar regra: O valor digitado deve ser igual à soma do cofinanciamento estadual (FEAS) de cada um dos benefícios eventuais 
        [DataMember]
        public Decimal? ValorBeneficiosEventuais { get; set; }

        //Extinto! 
        [DataMember]
        public Decimal? ValorProgramaProjetoSolidario { get; set; }

        //#region reprogramacao

        //[DataMember]
        //public Decimal? ValorProtecaoSocialBasicaReprogramado { get; set; }

        //[DataMember]
        //public Decimal? ValorProtecaoSocialMediaReprogramado { get; set; }

        //[DataMember]
        //public Decimal? ValorProtecaoSocialAltaReprogramado { get; set; }

        //[DataMember]
        //public Decimal? ValorBeneficioEventuaisReprogramado { get; set; }

        //[DataMember]
        //public Decimal? ValorProgramaProjetoReprogramado { get; set; } 
        //#endregion


        #region Navegacao
        [DataMember]
        public PrefeituraInfo Prefeitura { get; set; }

        //[DataMember]
        //public Data DradsPlanoMunicipal { get; set; }
        #endregion

        [DataMember]
        public Boolean Desbloqueado { get; set; }

        [DataMember]
        public Int32 Exercicio { get; set; }

    }
}
