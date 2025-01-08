using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ConsultaIntencaoAcaoInfo
    {
        public Int32 IdPrefeitura { get; set; }

        public Int32 IdUnidade { get; set; }

        public String TipoLocal { get; set; }

        public String Nome { get; set; }

        public Int32 IdAvaliacaoLocalExecucao { get; set; }

        public String IntencaoAcao { get; set; }

        public Boolean Desativado { get; set; }

    }
}
