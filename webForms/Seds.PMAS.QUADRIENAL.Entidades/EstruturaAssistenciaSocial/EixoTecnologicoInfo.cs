using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class EixoTecnologicoInfo
    {
        public Int32 Id { get; set; }

        public String Descricao { get; set; }

        public Boolean Exibe { get; set; }

        public EixoTecnologicoInfo EixoTecnologico { get; set; }
    }
}
