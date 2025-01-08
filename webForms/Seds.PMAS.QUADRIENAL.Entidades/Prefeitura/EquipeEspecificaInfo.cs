using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class EquipeEspecificaInfo
    {
        public Int32 Id { get; set; }

        public Int32 IdOrgaoGestor { get; set; }

        public Int32 IdPrefeitura { get; set; }

        public Int32 IdTipoEquipe { get; set; }

        public Int32 SemEscolaridade { get; set; }

        public Int32 NivelFundamental { get; set; }

        public Int32 NivelMedio { get; set; }

        public Int32 NivelSuperior { get; set; }

        public OrgaoGestorInfo OrgaoGestor { get; set; }

        public PrefeituraInfo Prefeitura { get; set; }

        public TipoEquipeInfo TipoEquipe { get; set; }

        public Int32? Exercicio { get; set; }

        public Boolean? Desbloqueado { get; set; }
    }
}
