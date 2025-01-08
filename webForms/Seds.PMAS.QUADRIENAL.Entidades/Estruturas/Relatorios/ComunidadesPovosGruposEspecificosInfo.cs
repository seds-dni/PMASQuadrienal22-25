using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    public class ComunidadesPovosGruposEspecificosInfo
    {
        public Int32 Id { get; set; }

        public Int32 IdPrefeitura { get; set; }

        public Int32 IdMunicipio { get; set; }

        public Int32 IdDrads { get; set; }

        public String Municipio { get; set; }

        public String Drads { get; set; }

        public Int32 IdRegiaoMetropolitana { get; set; }

        public Int32 IdMacroRegiao { get; set; }

        public Int16 IdNivelGestao { get; set; }

        public String NivelGestao { get; set; }

        public Int32 IdPorte { get; set; }

        public String Porte { get; set; }

        public Boolean ExisteCiganos { get; set; }

        public Int32 NumeroCiganos { get; set; }

        public Boolean ExisteExtrativistas { get; set; }

        public Int32 NumeroExtrativistas { get; set; }

        public Boolean ExistePescadores { get; set; }

        public Int32 NumeroPescadores { get; set; }

        public Boolean ExisteAfros { get; set; }

        public Int32 NumeroAfros { get; set; }

        public Boolean ExisteRibeirinha { get; set; }

        public Int32 NumeroRibeirinhas { get; set; }

        public Boolean ExisteIndigena { get; set; }

        public Int32 NumeroIndigenas { get; set; }

        public Boolean ExisteQuilombola { get; set; }

        public Int32 NumeroQuilombolas { get; set; }

        public Boolean ExisteAgricultor { get; set; }

        public Int32 NumeroAgricultores { get; set; }

        public Boolean ExisteAcampamento { get; set; }

        public Int32 NumeroAcampamentos { get; set; }

        public Boolean ExisteInstalacaoPrisional { get; set; }

        public Int32 NumeroInstalacoesPrisionais { get; set; }

        public Boolean ExisteTrabalhoSazonal { get; set; }

        public Int32 NumeroTrabalhoSazonais { get; set; }

        public Boolean ExisteAglomeradoSubnormal { get; set; }

        public Int32 NumeroAglomeradoSubnormais { get; set; }

        public Boolean ExisteAssentamentoPrecario { get; set; }

        public Int32 NumeroAssentamentosPrecarios { get; set; }

        public Boolean NaoExisteComunidade { get; set; }

        public Boolean NaoExisteGrupo { get; set; }

        public Int32 Populacao { get; set; }
    }
}
