using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class PrestacaoDeContasExecucaoFisicaProgramasProjetosInfo
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdPrefeitura { get; set; }

        [DataMember]
        public int IdTipoProtecao { get; set; }

        [DataMember]
        public int IdServicosRecursosFinanceiros { get; set; }

        [DataMember]
        public int IdTipoProgramaProjeto { get; set; }

        [DataMember]
        public int DemandaEstimada { get; set; }

        [DataMember]
        public int NumeroAtendidos { get; set; }

        [DataMember]
        public int Exercicio { get; set; }

    }
}
