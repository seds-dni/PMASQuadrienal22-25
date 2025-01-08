using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{

    [DataContract]
    public class DradsPlanoMunicipalBeneficioProgramaRecursosInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        
        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        [DataMember]
        public Decimal? ValorBeneficiosEventuais { get; set; }

        //Extinto! 
        [DataMember]
        public Decimal? ValorProgramaProjeto { get; set; }

        //[DataMember]
        //public Decimal? ValorBeneficioEventuaisReprogramado { get; set; }

        //[DataMember]
        //public Decimal? ValorProgramaProjetoReprogramado { get; set; } 


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
