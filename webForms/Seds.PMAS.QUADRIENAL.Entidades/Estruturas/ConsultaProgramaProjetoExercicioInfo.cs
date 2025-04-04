﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas
{
    [DataContract]
    public class ConsultaProgramaProjetoExercicioInfo
    {
        #region 
        [DataMember]
        
        public Int32 IdPrefeitura { get; set; }

        
        [DataMember]
        public String CofinanciadoPeloEstado { get; set; }

        
        [DataMember]
        public String Estadualizado { get; set; }
        #endregion 


        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public String Nome { get; set; }

        [DataMember]
        public Int32 TotalServicos { get; set; }

        [DataMember]
        public Decimal PrevisaoOrcamentaria { get; set; }
         
        [DataMember]
        public Boolean? ProgramaMunicipal { get; set; }
        [DataMember]
        public Boolean? ProgramaEstadual { get; set; }
        [DataMember]
        public Boolean? ProgramaFederal { get; set; }

        public Int32? TipoAbrangencia { get; set; }

        [DataMember]
        public Boolean? BeneficiarioAtendidoRedeSocioassistencial { get; set; }

        
        [DataMember]
        public Int32? Aderiu { get; set; }

        
        [DataMember]
        public String PossuiProgramaBeneficio { get; set; }

        [DataMember]
        public Boolean? AderiuBPCNaEscola { get; set; }

        //Welington P.
        //TIPO_PROGRAMA_TRANSFERENCIA - Esse campo verifica se o tipo é programa ou transferencia de renda
        // Valor 1 para Programa
        // Valor 2 para Transferencia de Renda
        
        [DataMember]
        public Int32? TipoProgramaTransferencia { get; set; }

        public Boolean? Ativo { get; set; }

        [DataMember]
        public Int32? Exercicio { get; set; }
    }
}
