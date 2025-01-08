using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class InterfacePublicaOutroServicoInfo
    {
        public Int32 Id { get; set; }

        public Int32 IdInterfacePublicaOutraPolitica { get; set; }

        public String NomeProgramaProjeto { get; set; }

        public String PoliticaPublica { get; set; }

        public Decimal? ValorRepassePoliticaAssistencia { get; set; }

        public InterfacePublicaOutraPoliticaInfo InterfacePublicaOutraPolitica { get; set; }

    }
}
