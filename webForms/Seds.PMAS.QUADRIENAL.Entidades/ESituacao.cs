using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public enum ESituacao : int
    {
        Desbloqueado = 1,
        EmAnaliseDrads = 2,
        DevolvidoDrads = 3,
        Parafinalizacao = 4,
        EmAnalisedoCMAS = 5,
        DevolvidopeloCMAS = 6,
        Rejeitado = 7,
        Aprovado = 8,
        AutorizaDesbloqueioGestor = 9,
        AutorizaDesbloqueioCMAS = 10,
        AutorizaDesbloqueioReprogramacao = 11,
        AutorizaDesbloqueioDemandas = 12,
        DevolverParaCas = 71
    }
}
