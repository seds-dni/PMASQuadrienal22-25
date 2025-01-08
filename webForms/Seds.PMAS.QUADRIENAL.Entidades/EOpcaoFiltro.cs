using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public enum EOpcaoFiltro : int
    {
        Nenhum = 0,
        Estado = 1,
        Drads = 2,
        Municipio = 3,
        MacroRegiao = 4,
        RegioesMetropolitanas = 5,
        PorteMunicipios = 6,
        NivelGestao = 7
    }
}
