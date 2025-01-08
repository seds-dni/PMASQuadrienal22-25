using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class InterfacePublicaOutraPoliticaInfo
    {
        public Int32 Id { get; set; }

        public Int32 IdPrefeitura { get; set; }

        public Boolean? ExisteOutraPoliticaPublica { get; set; }

        public String Descricao { get; set; }

        public String DescricaoPrincipaisObstaculos { get; set; } 


        public Boolean? ExistemServicosFinanciados { get; set; }

        public List<InterfacePublicaOutroServicoInfo> OutrosServicos { get; set; }
    }
}
