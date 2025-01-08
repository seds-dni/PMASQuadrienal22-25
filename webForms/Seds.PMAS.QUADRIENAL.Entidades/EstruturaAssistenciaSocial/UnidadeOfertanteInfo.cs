using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class UnidadeOfertanteInfo
    {
        public Int32 Id { get; set; }

        public Int32 IdProgramaProjeto { get; set; }

        public String UnidadeOfertante { get; set; }

        public Int32 IdEixoTecnologico { get; set; }

        public String NomeCurso { get; set; }

        public EixoTecnologicoInfo EixoTecnologico { get; set; }

        public ProgramaProjetoInfo ProgramaProjeto { get; set; }

    }
}
