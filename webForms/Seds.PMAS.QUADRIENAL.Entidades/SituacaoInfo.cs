using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [Serializable]
    [DataContract]
    public class SituacaoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }


        #region navegacao
        //TB_PREFEITURA_ATUALIZACAO_ANUAL
        public List<PrefeituraAtualizacaoAnualInfo> PrefeituraAtualizacoesAnuais { get; set; }
        //Execucoes Financeiras
        public List<ExecucaoFinanceiraInfo> ExecucoesFinanceiras { get; set; }
        public List<ComentarioExecucaoFinanceiraInfo> ComentariosExecucoesFinanceiras { get; set; }
        public List<PrefeituraExercicioBloqueioInfo> PrefeiturasExerciciosBloqueio { get; set; }
        #endregion

    }
}
