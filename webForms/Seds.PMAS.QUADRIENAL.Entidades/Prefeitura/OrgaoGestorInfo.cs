using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    /// <summary>
    /// Classe de Dados do Órgão Gestor Municipal
    /// </summary>
    [DataContract]
    public class OrgaoGestorInfo
    {
        /// <summary>
        /// Id do Órgão Gestor
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
        /// CNPJ do Órgão Gestor
        /// </summary>
        [DataMember]
        public String CNPJ { get; set; }
        /// <summary>
        /// Nome do Órgão Gestor da Assistência Social
        /// </summary>
        [DataMember]
        public String Nome { get; set; }
        /// <summary>
        /// Id da Estrutura do Órgão Gestor
        /// </summary>
        [DataMember]
        public Int16 IdEstrutura { get; set; }
        /// <summary>
        /// Objeto de Referência da Estrutura do Órgão Gestor
        /// </summary>
        [DataMember]
        public EstruturaInfo Estrutura { get; set; }
        /// <summary>
        /// Descrição da outra estrutura do Órgão Gestor, caso o mesmo não selecione uma estrutura da lista
        /// </summary>
        [DataMember]
        public String EstruturaOutros { get; set; }
        /// <summary>
        /// CEP do Órgão Gestor
        /// </summary>
        [DataMember]
        public String CEP { get; set; }
        /// <summary>
        /// Logradouro onde está localizado o Órgão Gestor
        /// </summary>
        [DataMember]
        public String Logradouro { get; set; }
        /// <summary>
        /// Número onde está localizado o Órgão Gestor
        /// </summary>
        [DataMember]
        public String Numero { get; set; }
        /// <summary>
        /// Complemento onde está localizado o Órgão Gestor
        /// </summary>
        [DataMember]
        public String Complemento { get; set; }
        /// <summary>
        /// Bairro onde está localizado o Órgão Gestor
        /// </summary>
        [DataMember]
        public String Bairro { get; set; }
        /// <summary>
        /// Cidade onde está localizado o Órgão Gestor
        /// </summary>
        [DataMember]
        public String Cidade { get; set; }
        /// <summary>
        /// Telefone do Órgão Gestor
        /// </summary>
        [DataMember]
        public String Telefone { get; set; }
        /// <summary>
        /// Celular do Órgão Gestor
        /// </summary>
        [DataMember]
        public String Celular { get; set; }
        /// <summary>
        /// Email do Órgão Gestor
        /// </summary>
        [DataMember]
        public String Email { get; set; }
        /// <summary>
        /// Orgão Gestor possui site?
        /// </summary>
        [DataMember]
        public Boolean PossuiSite { get; set; }
        /// <summary>
        /// Site do Órgão Gestor
        /// </summary>
        [DataMember]
        public String Site { get; set; }
        /// <summary>
        /// Lei de criação do Órgão Gestor
        /// </summary>
        [DataMember]
        public String Lei { get; set; }
        /// <summary>
        /// Data de criação do Órgão Gestor
        /// </summary>
        [DataMember]
        public DateTime DataLei { get; set; }
        /// <summary>
        /// Houve alteração na Lei de criação?
        /// </summary>
        [DataMember]
        public Boolean? AlteracaoLei { get; set; }
        /// <summary>
        /// Número da Lei Alterada
        /// </summary>
        [DataMember]
        public String LeiAlterada { get; set; }
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
        public Boolean? PossuiLeiSuas { get; set; }

        [DataMember]
        public String LeiSuas { get; set; }

        [DataMember]
        public DateTime? DataPublicacaoLeiSuas { get; set; }



        #region navegacao
        public List<IntencaoEstruturacaoEquipeInfo> IntencoesEstruturacaoEquipe { get; set; }

        public List<EquipeEspecificaInfo> EquipesEspecificas { get; set; }

        public List<EquipeEspecificaTotaisInfo> EquipesEspecificasTotais { get; set; }

        #endregion

        #region Não Mapeado ef
        public int TotalTrabalhadoresSuperior { get; set; }
        public int TotalTrabalhadores { get; set; }
        public int Exercicio { get; set; } 
        #endregion
    }
}






//REMOVIDO - TRANSFERIDO PARA EquipeEspecificaTotaisInfo
        //[DataMember]
        //public Int32? PossuiEquipeProtecaoBasica { get; set; }

        ///// <summary>
        ///// O Órgão Gestor mantém equipe específica para coordenação da Proteção Especial Básica
        ///// </summary>
        //[DataMember]
        //public Int32? PossuiEquipeProtecaoEspecial { get; set; }

        ///// <summary>
        ///// O Órgão Gestor mantém equipe específica para coordenação da Vigilância Socioassistencial
        ///// </summary>
        //[DataMember]
        //public Int32? PossuiEquipeVigilanciaSocioassistencial { get; set; }

        ///// <summary>
        ///// O Órgão Gestor mantém equipe específica para gestão de Beneficios/Transferência de renda
        ///// </summary>
        //[DataMember]
        //public Int32? PossuiEquipeGestaoTransferenciaRenda { get; set; }

        ///// <summary>
        ///// O Órgão Gestor mantém equipe específica para gestão do Cadastro Único
        ///// </summary>
        //public Int32? PossuiEquipeCadUnico { get; set; }

        ///// <summary>
        ///// O Órgão Gestor mantém equipe específica para gestão financeira e orçamentária
        ///// </Int32>
        //public Int32? PossuiEquipeGestaoFinanceira { get; set; }
        //public Int32 TotalTrabalhadores { get; set; }

        //public Int32 TotalTrabalhadoresSuperior { get; set; }

        //public Int32? PossuiEquipeGestaoSUAS { get; set; }

        //public Int32? PossuiEquipeRegulacaoSUAS { get; set; }

        //public Int32? PossuiEquipeRedeDireta { get; set; }

        //public Int32? PossuiOutrasEquipes { get; set; }

        //public Int32 TotalEstatutarios { get; set; }

        //public Int32 TotalCeletistas { get; set; }

        //public Int32 TotalComissionados { get; set; }

        //public Int32 TotalOutrosVinculos { get; set; }

        //public Int32 TotalVoluntarios { get; set; }




  //[DataMember]
  //      public Int16 TotalFuncionarios { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Nível Fundamental
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosNivelFundamental { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Nível Médio 
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosNivelMedio { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Curso Superior em Serviço Social
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorServicoSocial { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Curso Superior em Psicologia
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorPsicologia { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Curso Superior em Pedagogia
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorPedagogia { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Curso Superior em Sociologia
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorSociologia { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Formação Superior
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperior { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Curso de Pós-Graduação
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosPosGraduacao { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Curso Superior em Direito
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorDireito { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que não possui escolaridade
  //      /// </summary>        
  //      [DataMember]
  //      public Int32 TotalFuncionariosSemEscolaridade { get; set; }
  //      /// <summary>
  //      /// Total de Estagiários do Órgão Gestor
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalEstagiarios { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Formação Superior em Administração
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorAdministracao { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Formação Superior em Antropologia
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorAntropologia { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Formação Superior em Contabilidade
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorContabilidade { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Formação Superior em Economia
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorEconomia { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Formação Superior em Economia Doméstica
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorEconomiaDomestica { get; set; }
  //      /// <summary>
  //      /// Total de Funcionários do Órgão Gestor que possui Formação Superior em Terapia Ocupacional
  //      /// </summary>
  //      [DataMember]
  //      public Int32 TotalFuncionariosSuperiorTerapiaOcupacional { get; set; }