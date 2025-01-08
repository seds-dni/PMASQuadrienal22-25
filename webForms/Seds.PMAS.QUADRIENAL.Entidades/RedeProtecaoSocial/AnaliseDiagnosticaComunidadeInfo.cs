using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class AnaliseDiagnosticaComunidadeInfo
    {
        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }
        [DataMember]
        public Boolean ExisteCigano { get; set; }
        public Int32? NumeroCiganos { get; set; }
        [DataMember]
        public Boolean ExisteExtrativista { get; set; }
        public Int32? NumeroExtrativistas { get; set; }
        [DataMember]
        public Boolean ExistePescador { get; set; }
        public Int32? NumeroPescadores { get; set; }
        [DataMember]
        public Boolean ExisteAfro { get; set; }
        public Int32? NumeroAfros { get; set; }
        [DataMember]
        public Boolean ExisteRibeirinha { get; set; }
        public Int32? NumeroRibeirinhas { get; set; }
        [DataMember]
        public Boolean ExisteIndigena { get; set; }
        public Int32? NumeroIndigenas { get; set; }
        [DataMember]
        public Boolean ExisteQuilombola { get; set; }
        public Int32? NumeroQuilombolas { get; set; }
        [DataMember]
        public Boolean ExisteAgricultor { get; set; }
        public Int32? NumeroAgricultores { get; set; }
        [DataMember]
        public Boolean ExisteAcampamento { get; set; }
        public Int32? NumeroAcampamentos { get; set; }
        [DataMember]
        public Boolean ExisteInstalacaoPrisional { get; set; }
        public Int32? NumeroInstalacoesPrisionais { get; set; }
        [DataMember]
        public Boolean ExisteTrabalhoSazonal { get; set; }
        public Int32? NumeroTrabalhoSazonais { get; set; }
        [DataMember]
        public Boolean ExisteAglomeradoSubnormal { get; set; }
        public Int32? NumeroAglomeradoSubnormais { get; set; }
        [DataMember]
        public Boolean ExisteAssentamentoPrecario { get; set; }
        public Int32? NumeroAssentamentoPrecarios { get; set; }

        public Boolean NaoExisteComunidade { get; set; }

        public Boolean NaoExisteGrupo { get; set; }

        [DataMember]
        public Int32 IdExercicio { get; set; }
    }
}
