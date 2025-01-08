using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public enum ESituacaoQuadro : int
    {
        Pendente = 1,
        Preenchido = 2,
        EmAnaliseCMAS = 3,
        AprovadoCMAS = 4,
        DevolvidoDRADS = 5,
        DevolvidoCMAS = 6,
        BloqueioInicialAdministrativo = 7,
        AprovadoDRADS = 8,
        RejeitadoDRADS = 9,
        RejeitadoCMAS = 10
    }
}
