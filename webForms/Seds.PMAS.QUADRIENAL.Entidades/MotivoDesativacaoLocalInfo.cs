using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class MotivoDesativacaoLocalInfo
    {
        public int Id { get; set; }

        public String Descricao { get; set; }

        public Int32? IdMotivoPai { get; set; }

        public Boolean Ativo { get; set; }

        public String TipoLocal { get; set; }
    }
}
