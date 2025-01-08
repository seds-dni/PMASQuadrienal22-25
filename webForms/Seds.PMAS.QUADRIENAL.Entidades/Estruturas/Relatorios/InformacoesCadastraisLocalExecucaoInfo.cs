using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    [DataContract]
    public class InformacoesCadastraisLocalExecucaoInfo
    {
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
        public Int32 IdTipoUnidade { get; set; }
        [DataMember]
        public String TipoUnidade { get; set; }
        [DataMember]
        public Int32 CodigoUnidade { get; set; }
        [DataMember]
        public String UnidadeResponsavel { get; set; }
        [DataMember]
        public Int32 IdLocal { get; set; }
        [DataMember]
        public String LocalExecucao { get; set; }
        [DataMember]
        public String Coordenador { get; set; }
        [DataMember]
        public String CNPJ { get; set; }
        [DataMember]
        public String Endereco { get; set; }
        [DataMember]
        public String Bairro { get; set; }
        [DataMember]
        public String CEP { get; set; }
        [DataMember]
        public String Cidade { get; set; }
        [DataMember]
        public String Telefone { get; set; }
        [DataMember]
        public String Celular { get; set; }
        [DataMember]
        public String Email { get; set; }

        [DataMember]
        public Int32? IdTipoServico { get; set; }
        [DataMember]
        public Int32? IdUsuarioTipoServico { get; set; }
        [DataMember]
        public Int16? IdTipoProtecao { get; set; }
        [DataMember]
        public String TipoServico { get; set; }

        [DataMember]
        public Boolean? ServicoNaoTipificado { get; set; }

        public Int32? IdDistritosSaoPaulo { get; set; }

        public String DistritosSaoPaulo { get; set; }

    }
}
