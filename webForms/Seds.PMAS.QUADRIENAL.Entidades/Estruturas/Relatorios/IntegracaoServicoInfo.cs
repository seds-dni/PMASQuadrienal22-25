using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_INTEGRACAO_SERVICOS]
    /// </summary>
    [DataContract]
    public class IntegracaoServicoInfo
    {
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string Drads { get; set; }
        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int16 IdNivelGestao { get; set; }
        [DataMember]
        public Int32 IdPorte { get; set; }

        [DataMember]
        public Int32 CodigoUnidade { get; set; }
        [DataMember]
        public Int32 IdTipoUnidade { get; set; }
        [DataMember]
        public String TipoUnidade { get; set; }
        [DataMember]
        public String UnidadeResponsavel { get; set; }
        [DataMember]
        public String IdLocal { get; set; }
        [DataMember]
        public String LocalExecucao { get; set; }
        [DataMember]
        public Int16 IdTipoProtecao { get; set; }
        [DataMember]
        public String ProtecaoSocial { get; set; }
        [DataMember]
        public Int32 IdTipoServico { get; set; }
        [DataMember]
        public String TipoServico { get; set; }
        [DataMember]
        public Int32 IdUsuarioTipoServico { get; set; }
        [DataMember]
        public String Usuarios { get; set; }

        [DataMember]
        public Int32 IdServicoRecursoFinanceiro { get; set; }

        [DataMember]
        public Int32 NumeroAtendidosAnual { get; set; }
        [DataMember]
        public Int32 NumeroAtendidosMensal { get; set; }
        [DataMember]
        public Int32 NumeroAtendidosIntegracao { get; set; }

        [DataMember]
        public Int32 IdTipoPrograma { get; set; }
        [DataMember]
        public String NomePrograma { get; set; }

        public Int32 Peti { get; set; }
    }
}

