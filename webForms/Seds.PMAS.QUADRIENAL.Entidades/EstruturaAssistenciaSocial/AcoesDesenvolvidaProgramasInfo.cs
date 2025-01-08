using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class AcoesDesenvolvidaProgramasInfo
    {
        public Int32 Id { get; set; }

        public String Descricao { get; set; }

        public Boolean Exibe { get; set; }

        public Int32 IdProgramaProjeto { get; set; }

        public Int32 TipoPrograma { get; set; }

        public ProgramaProjetoInfo ProgramaProjeto { get; set; }
    }
}
