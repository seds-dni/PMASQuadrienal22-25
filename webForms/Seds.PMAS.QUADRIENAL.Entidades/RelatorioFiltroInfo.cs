using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class RelatorioFiltroInfo
    {
        //FILTROS
        [DataMember]
        public List<int> MunIDs { get; set; }
        [DataMember]
        public List<int> DrdIDs { get; set; }
        [DataMember]
        public List<int> RegIDs { get; set; }
        [DataMember]
        public List<int> MacroRegiaoIDs { get; set; }
        [DataMember]
        public List<int> Portes { get; set; }
        [DataMember]
        public EPerfil? UsuarioLogado { get; set; }
        [DataMember]
        public List<int> NiveisGestao { get; set; }
        [DataMember]
        public List<int> TipoProgramas { get; set; }
        [DataMember]
        public List<ETipoUnidade> TipoExecutora { get; set; }
        [DataMember]
        public int? TipoProtecaoSocial { get; set; }
        [DataMember]
        public int? TipoServico { get; set; }

        public int IdPrefeitura { get; set; }

        [DataMember]
        public int? ServicoSubtificado { get; set; }

        [DataMember]
        public int? Usuarios { get; set; }
        [DataMember]
        public int? SituacaoVulnerabilidade { get; set; }
        [DataMember]
        public int? SituacaoEspecifica { get; set; }

        [DataMember]
        public List<int> SituacoesVulnerabilidade { get; set; }
        [DataMember]
        public List<int> SituacoesEspecificas { get; set; }

        [DataMember]
        public String SituacaoVulnerabilidadeCondicao { get; set; }

        [DataMember]
        public int? TipoBeneficioEventual { get; set; }
        [DataMember]
        public Boolean? Estado { get; set; }
        [DataMember]
        public int? Usuario { get; set; }
        [DataMember]
        public List<int> Abrangencias { get; set; }

        [DataMember]
        public int? Sexo { get; set; }
        [DataMember]
        public int? RegiaoMoradia { get; set; }
        [DataMember]
        public int? CaracteristicasTerritorio { get; set; }

        [DataMember]
        public int? TipoConselho { get; set; }

        [DataMember]
        public int? TipoFinanciamento { get; set; }

        [DataMember]
        public int? TipoUnidade { get; set; }

        [DataMember]
        public List<int> FormasAtuacoes { get; set; }

        [DataMember]
        public List<int> CronogramasEscolhidos { get; set; }

        [DataMember]
        public int? TotalCronogramas { get; set; }

        public int? IdMunicipio { get; set; }
        [DataMember]
        public List<string> AbrangenciasProgramas { get; set; }

        [DataMember]
        public int? Exercicio { get; set; }
        public int IdDrad { get; set; }
        
        [DataMember]
        public String DataImplantacao { get; set; }

        [DataMember]
        public bool ehAtivo { get; set; }

        [DataMember]
        public bool ehDesativo { get; set; }
    }
}
