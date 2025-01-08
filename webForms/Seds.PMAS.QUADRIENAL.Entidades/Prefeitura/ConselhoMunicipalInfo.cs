using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados do Conselho Municipal
    /// </summary>
    [DataContract]
    public class ConselhoMunicipalInfo
    {
        /// <summary>
        /// Id do Conselho Municipal
        /// </summary>
        [DataMember]
        public Int32 Id { get; set; }

        /// <summary>
        /// Id da Prefeitura
        /// </summary>
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        /// <summary>
        /// Objeto de Referência da Prefeitura
        /// </summary>
        [IgnoreDataMember]
        public PrefeituraInfo Prefeitura { get; set; }

        /// <summary>
        /// CEP do CMAS
        /// </summary>
        [DataMember]
        public String CEP { get; set; }
        /// <summary>
        /// Logradouro onde está localizado o CMAS
        /// </summary>
        [DataMember]
        public String Logradouro { get; set; }
        /// <summary>
        /// Número onde está localizado o CMAS
        /// </summary>
        [DataMember]
        public String Numero { get; set; }
        /// <summary>
        /// Complemento onde está localizado o CMAS
        /// </summary>
        [DataMember]
        public String Complemento { get; set; }
        /// <summary>
        /// Cidade onde está localizado o CMAS
        /// </summary>
        [DataMember]
        public String Cidade { get; set; }
        /// <summary>
        /// Bairro onde está localizado o CMAS
        /// </summary>
        [DataMember]
        public String Bairro { get; set; }
        /// <summary>
        /// Telefone do CMAS
        /// </summary>
        [DataMember]
        public String Telefone { get; set; }
        /// <summary>
        /// Celular do CMAS
        /// </summary>
        [DataMember]
        public String Celular { get; set; }
        /// <summary>
        /// Email do CMAS
        /// </summary>
        [DataMember]
        public String Email { get; set; }
        /// <summary>
        /// Lei de criação do CMAS
        /// </summary>
        [DataMember]
        public String NumeroLei { get; set; }
        /// <summary>
        /// Data de criação do CMAS
        /// </summary>
        [DataMember]
        public DateTime DataLei { get; set; }
        /// <summary>
        /// Houve alteração na Lei de criação?
        /// </summary>
        [DataMember]
        public Boolean? AlteracaoNaLei { get; set; }
        /// <summary>
        /// Número da Lei Alterada
        /// </summary>
        [DataMember]
        public String NumeroLeiAlterada { get; set; }
        /// <summary>
        /// Data de Alteração da Lei
        /// </summary>
        [DataMember]
        public DateTime? DataLeiAlterada { get; set; }
        [DataMember]
        public String NumeroDecreto { get; set; }
        [DataMember]
        public DateTime? DataDecreto { get; set; }

        [DataMember]
        public int NumeroTrabalhadores { get; set; }

        [DataMember]
        public int NumeroUsuarios { get; set; }

        [DataMember]
        public int NumeroEntidades { get; set; }

        [DataMember]
        public String NomeSecretarioExecutivo { get; set; }

        [DataMember]
        public Int32 IdUsuarioPresidente { get; set; }

        [DataMember]
        public Int32 NumeroRepresentanteGovernamentais { get; set; }
        [DataMember]
        public Int32 NumeroRepresentanteSociedadeCivil { get; set; }

        [DataMember]
        public DateTime? DataMandatoInicio { get; set; }
        
        [DataMember]
        public DateTime? DataMandatoTerminio { get; set; }

        [DataMember]
        public Boolean? PossuiSecretariaExecutivaEstruturada { get; set; }
        
        [DataMember]
        public Int32? TotalFuncionariosTecnicoSecretariaExecutiva { get; set; }
        
        [DataMember]
        public Int32? TotalFuncionariosAdministrativoSecretariaExecutiva { get; set; }

        [DataMember]
        public Int32? ComposicaoServicoSocial { get; set; }
        
        [DataMember]
        public Int32? ComposicaoPsicologia { get; set; }
        
        [DataMember]
        public Int32? ComposicaoPedagogia { get; set; }
        
        [DataMember]
        public Int32? ComposicaoSociologia { get; set; }
        
        [DataMember]
        public Int32? ComposicaoDireito { get; set; }
        
        [DataMember]
        public Int32? ComposicaoEconomiaDomestica { get; set; }
        
        [DataMember]
        public Int32? ComposicaoAdministracao { get; set; }
        
        [DataMember]
        public Int32? ComposicaoAntropologia { get; set; }
        
        [DataMember]
        public Int32? ComposicaoContabilidade { get; set; }
        
        [DataMember]
        public Int32? ComposicaoEconomia { get; set; }
        
        [DataMember]
        public Int32? ComposicaoTerapiaOcupacional { get; set; }
        
        [DataMember]
        public Int32? ComposicaoMusicoterapia { get; set; }

        [DataMember]
        public List<ConselhoMunicipalPresidenteAnteriorInfo> PresidentesAnteriores { get; set; }

        public String CPF { get; set; }

        public String RG { get; set; }

        public String RGDigito { get; set; }

        public DateTime? DataEmissao { get; set; }

        public Int16? IdUf { get; set; }

        public String SiglaEmissor { get; set; }

        public String EmailPresidente { get; set; }

        public String TelefonePresidente { get; set; }

        public String CelularPresidente { get; set; }
    }
}
