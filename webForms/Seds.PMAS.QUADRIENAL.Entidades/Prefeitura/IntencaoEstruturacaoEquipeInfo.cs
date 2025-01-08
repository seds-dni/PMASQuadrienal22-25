using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Existe inteção de estruturar esta equipe no orgão gestor nos próximos anos?
    /// </summary>
    public class IntencaoEstruturacaoEquipeInfo
    {
        #region chaves
        public Int32 Id { get; set; }
        public Int32 IdOrgaoGestor { get; set; }
        #endregion
        
        public Boolean? IntencaoAcaoEquipeBasica { get; set; }

        public Boolean? IntencaoAcaoEquipeEspecial { get; set; }

        public Boolean? IntencaoAcaoEquipeVigilanciaSocioAssistencial { get; set; }

        public Boolean? IntencaoAcaoEquipeGestaoBeneficios { get; set; }

        public Boolean? IntencaoAcaoEquipeGestaoCadUnico { get; set; }

        public Boolean? IntencaoAcaoEquipeGestaoFinanceira { get; set; }

        public Boolean? IntencaoAcaoEquipeGestorSUAS { get; set; }

        public Boolean? IntencaoAcaoEquipeGestaoSUAS { get; set; }

        public Boolean? IntencaoAcaoEquipeRegulacaoSUAS { get; set; }

        public Boolean? IntencaoAcaoEquipeRedeDireta { get; set; }

        public Boolean? IntencaoAcaoOutraEquipe { get; set; }

        public Boolean? IntencaoAcaoOrgaoGestor { get; set; }

        public int? Exercicio { get; set; }
        public Boolean? Desbloqueado { get; set; }


        #region navegacao
        public OrgaoGestorInfo OrgaoGestor { get; set; }
        #endregion

    }
}
