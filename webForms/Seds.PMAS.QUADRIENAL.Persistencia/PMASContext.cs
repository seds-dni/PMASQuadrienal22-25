using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.EntityClient;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;

namespace Seds.PMAS.QUADRIENAL.Persistencia
{
    public class PMASContext : ObjectContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new ProSocialContext object using the connection string found in the 'ProSocialContext' section of the application configuration file.
        /// </summary>
        public PMASContext()
            : base("name=PMASContext", "PMASContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// Initialize a new ProSocialContext object.
        /// </summary>
        public PMASContext(string connectionString)
            : base(connectionString, "PMASContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// Initialize a new ProSocialContext object.
        /// </summary>
        public PMASContext(EntityConnection connection)
            : base(connection, "PMASContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        #endregion

        #region Functions Imports
        public List<PrevisaoOrcamentaria2016Info> GetPrevisaoOrcamentaria2016(Int32 idPrefeitura)
        {
            return this.ExecuteFunction<PrevisaoOrcamentaria2016Info>("PR_PREVISAO_ORCAMENTARIA_2016", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).ToList();
        }

        public List<PrevisaoOrcamentariaInfo> GetPrevisaoOrcamentaria(Int32 idPrefeitura , int exercicio)
        {
            return this.ExecuteFunction<PrevisaoOrcamentariaInfo>("PR_PREVISAO_ORCAMENTARIA", new ObjectParameter("ID_PREFEITURA", idPrefeitura), new ObjectParameter("EXERCICIO", exercicio)).ToList();
        }

        public List<PrevisaoOrcamentariaMunicipalInfo> GetPrevisaoOrcamentariaMunicipal(Int32 idPrefeitura)
        {
            return this.ExecuteFunction<PrevisaoOrcamentariaMunicipalInfo>("PR_PREVISAO_ORCAMENTARIA_MUNICIPAL", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).ToList();
        }

        public LeiOrcamentariaInfo GetLeiOrcamentaria2016ByPrefeitura(int idPrefeitura)
        {
            var lei = this.ExecuteFunction<LeiOrcamentariaInfo>("PR_LEI_ORCAMENTARIA_2016", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).FirstOrDefault();
            if (lei != null)
                lei.IdPrefeitura = idPrefeitura;
            return lei;
        }

        public BeneficioEventual2016Info GetBeneficioEventual2016ByPrefeitura(int idPrefeitura)
        {
            return this.ExecuteFunction<BeneficioEventual2016Info>("PR_BENEFICIO_EVENTUAL_2016", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).FirstOrDefault();
        }

        public List<BeneficioEventualAnualInfo> GetBeneficioEventualByPrefeitura(int idPrefeitura)
        {
            return this.ExecuteFunction<BeneficioEventualAnualInfo>("PR_BENEFICIO_EVENTUAL_ANUAL", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).ToList();
        }

        public TransferenciaRenda2016Info GetTransferenciaRenda2016ByPrefeitura(int idPrefeitura, int idTipoTransferencia)
        {
            return this.ExecuteFunction<TransferenciaRenda2016Info>("PR_TRANSFERENCIA_RENDA_ANUAL_2016", new ObjectParameter("ID_PREFEITURA", idPrefeitura), new ObjectParameter("ID_TIPO_TRANSFERENCIA_RENDA", idTipoTransferencia)).FirstOrDefault();
        }

        public IndiceGestaoDescentralizadaInfo GetIndiceGestaoDescentralizada2016ByPrefeitura(int idPrefeitura)
        {
            return this.ExecuteFunction<IndiceGestaoDescentralizadaInfo>("PR_INDICE_GESTAO_DESCENTRALIZADA_2016", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).FirstOrDefault();
        }

        public SaoPauloSolidario2016Info GetSaoPauloSolidario2016ByPrefeitura(int idPrefeitura)
        {
            return this.ExecuteFunction<SaoPauloSolidario2016Info>("PR_SAO_PAULO_SOLIDARIO_2016", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).FirstOrDefault();
        }

        public List<TransferenciaRenda2016Info> GetTransferenciaRenda2016ByPrefeitura(int idPrefeitura)
        {
            return this.ExecuteFunction<TransferenciaRenda2016Info>("PR_TRANSFERENCIA_RENDA_ANUAL_2016", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).ToList();
        }

        public TransferenciaRendaAnualInfo GetTransferenciaRendaByPrefeitura(int idPrefeitura, int idTipoTransferencia)
        {
            return this.ExecuteFunction<TransferenciaRendaAnualInfo>("PR_TRANSFERENCIA_RENDA_ANUAL", new ObjectParameter("ID_PREFEITURA", idPrefeitura), new ObjectParameter("ID_TIPO_TRANSFERENCIA_RENDA", idTipoTransferencia)).FirstOrDefault();
        }

        public PrefeituraValoresReprogramadosAnoAnteriorInfo GetValoresReprogramadosAnoAnterior (int idPrefeitura)
        {
         return this.ExecuteFunction<PrefeituraValoresReprogramadosAnoAnteriorInfo>("PR_RECURSOS_REPROGRAMADO_ANO_ANTERIOR", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).FirstOrDefault();
        }

        public List<TransferenciaRendaAnualInfo> GetTransferenciaRendaByPrefeitura(int idPrefeitura)
        {
            return this.ExecuteFunction<TransferenciaRendaAnualInfo>("PR_TRANSFERENCIA_RENDA_ANUAL", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).ToList();
        }

        public List<ConsultaProgramaProjetoServicoCofinanciamentoInfo> GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(int IdServicosRecursosFinanceiros, int IdLocal)
        {
            return this.ExecuteFunction<ConsultaProgramaProjetoServicoCofinanciamentoInfo>("PR_PROGRAMA_PROJETO_SERVICO_COFINANCIAMENTO", new ObjectParameter("ID_SERVICOS_RECURSOS_FINANCEIROS", IdServicosRecursosFinanceiros), new ObjectParameter("ID_LOCAL", IdLocal)).ToList();
        }

        public List<ConsultaProgramaProjetoServicoCofinanciamentoFundosInfo> GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceirosFundos(int IdServicosRecursosFinanceiros, int IdLocal)
        {
            return this.ExecuteFunction<ConsultaProgramaProjetoServicoCofinanciamentoFundosInfo>("PR_PROGRAMA_PROJETO_SERVICO_COFINANCIAMENTO_FUNDOS", new ObjectParameter("ID_SERVICOS_RECURSOS_FINANCEIROS", IdServicosRecursosFinanceiros), new ObjectParameter("ID_LOCAL", IdLocal)).ToList();
        }

        public List<ConsultaProgramaProjetoCofinanciamentoInfo> GetProgramaProjetoCofinanciamentoByProgramaProjeto(int idProgramaProjeto)
        {
            return this.ExecuteFunction<ConsultaProgramaProjetoCofinanciamentoInfo>("PR_PROGRAMA_PROJETO_COFINANCIAMENTO", new ObjectParameter("ID_PROGRAMA_PROJETO", idProgramaProjeto)).ToList();
        }

        public List<ConsultarServicosDiretrizesInfo> GetServicosDiretrizesByIdPrefeitura(int idPrefeitura,int idAnalise,int exercicio)
        {
            return this.ExecuteFunction<ConsultarServicosDiretrizesInfo>("PR_SERVICOS_DIRETRIZES", new ObjectParameter("ID_PREFEITURA", idPrefeitura), new ObjectParameter("ID_ANALISE", idAnalise), new ObjectParameter("EXERCICIO", exercicio)).ToList(); 
        }

        public List<ConsultaTransferenciaRendaCofinanciamentoInfo> GetTransferenciaRendaCofinanciamentoByTransferenciaRenda(int idTransferenciaRenda)
        {
            
            var transferenciaRendaCofinanciamento = this.ExecuteFunction<ConsultaTransferenciaRendaCofinanciamentoInfo>("PR_TRANSFERENCIA_RENDA_COFINANCIAMENTO", new ObjectParameter("ID_TRANSFERENCIA_RENDA", idTransferenciaRenda));
            if (transferenciaRendaCofinanciamento != null)
            {
                return transferenciaRendaCofinanciamento.ToList();
            }
            else {
                return new List<ConsultaTransferenciaRendaCofinanciamentoInfo>();   
            }
        }

        public List<ConsultaPrefeituraBeneficioEventualRecursoFinanceiroInfo> GetPrefeituraBeneficioEventualServicosByBeneficioEventual(int idPrefeituraBeneficioEventual)
        {
            return this.ExecuteFunction<ConsultaPrefeituraBeneficioEventualRecursoFinanceiroInfo>("PR_PREFEITURA_BENEFICIO_EVENTUAL_SERVICOS", new ObjectParameter("ID_PREFEITURA_BENEFICIO_EVENTUAL", idPrefeituraBeneficioEventual)).ToList();
        }

        public List<ConsultaCofinanciamentoEstadualInfo> GetCofinanciamentoEstadualByPrefeituraETipoProtecaoSocial(int idPrefeitura, int idTipoProtecaoSocial, int exercicio)
        {
            return this.ExecuteFunction<ConsultaCofinanciamentoEstadualInfo>("PR_COFINANCIAMENTO_ESTADUAL"
                , new ObjectParameter("ID_TIPO_PROTECAO", idTipoProtecaoSocial)
                , new ObjectParameter("ID_PREFEITURA", idPrefeitura)
                , new ObjectParameter("EXERCICIO", exercicio)).ToList();
        }


        public Int32 GetMetaPrevista(int idPrograma, string CNPJ)
        {
            Int32 meta;
            var obj = this.ExecuteFunction<Int32>("PR_META_PREVISTA_LST", new ObjectParameter("PGM_ID_PROGRAMA", idPrograma), new ObjectParameter("CNPJ", CNPJ)).FirstOrDefault();
            meta = obj;
            return meta;
        }

        public ObjectResult<Boolean> GetBloqueioQuadroFinanceiro()
        {
            return this.ExecuteStoreQuery<Boolean>("SELECT DBO.GET_BLOQUEIO_QUADRO_FINANCEIRO()");
        }

        public ObjectResult<Int32> GetBloqueioQuadroLeiOrcamentaria()  
        {
            return this.ExecuteFunction<Int32>("PR_SITUACAO_BLOQUEIO_QUADROS_LO_EF");
        }

        
        public void SaveBloqueioQuadroFinanceiro(Boolean bloqueiado, Int32 idUsuario, DateTime date)
        {
            this.ExecuteFunction("PR_BLOQUEIO_QUADRO_FINANCEIRO", new ObjectParameter("BLOQUEADO", bloqueiado), new ObjectParameter("ID_USUARIO", idUsuario), new ObjectParameter("DATA", date));
        }

        public void SaveBloqueioQuadroLeiOrcamentaria(Boolean bloqueiado, Int32 idUsuario, DateTime date)  
        {
            this.ExecuteFunction("PR_BLOQUEIO_QUADRO_LEI_ORCAMENTARIA", new ObjectParameter("BLOQUEADO", bloqueiado), new ObjectParameter("ID_USUARIO", idUsuario), new ObjectParameter("DATA", date));
        }

        public void SaveBloqueioQuadroParecerDrads(Boolean bloqueado, Int32 idUsuario, DateTime date) //Welington P.
        {
            this.ExecuteFunction("PR_BLOQUEIO_QUADRO_PARECER_DRADS", new ObjectParameter("BLOQUEADO", bloqueado), new ObjectParameter("ID_USUARIO", idUsuario), new ObjectParameter("DATA", date));
        }

        public void DeletarAcoesDesenvolvidaProgramas(int idProgramaProjeto) 
        {
           var retorno = this.ExecuteFunction("PR_DELETA_ACOES_DEVOLVIDAS_PROGRAMAS", new ObjectParameter("ID_PROGRAMA_PROJETO", idProgramaProjeto));
        }

        public void InserirPerfilUsuario(int idUsuario, int idPerfil) 
        {
            var retorno = this.ExecuteFunction("INSERIR_PERFIL", new ObjectParameter("ID_USUARIO", idUsuario), new ObjectParameter("ID_PERFIL", idPerfil));
        }

        public void AtualizarPerfilUsuario(int idUsuario, int idPerfil, int idPerfilAntigo)
        {
            var retorno = this.ExecuteFunction("ATUALIZA_PERFIL", new ObjectParameter("ID_USUARIO", idUsuario), new ObjectParameter("ID_PERFIL", idPerfil), new ObjectParameter("ID_PERFIL_ANTIGO", idPerfilAntigo));
        }

        public ConsultaMunicipioIndicadoresInfo GetMunicipioIndicadoresByMunicipio(Int32 idMunicipio)
        {
            return this.ExecuteFunction<ConsultaMunicipioIndicadoresInfo>("PR_MUNICIPIO_INDICADORES", new ObjectParameter("ID_MUNICIPIO", idMunicipio)).FirstOrDefault();
        }

        //Welington P.
        public ConsultaDemografiaTerritorioIndicadoresInfo GetDemografiaIndicadoresByMunicipio(Int32 idMunicipio, Int32 versaoSistema) 
        {
            return this.ExecuteFunction<ConsultaDemografiaTerritorioIndicadoresInfo>("PR_MUNICIPIO_DEMOGRAFIA_TERRITORIO_INDICADORES", new ObjectParameter("ID_MUNICIPIO", idMunicipio), new ObjectParameter("VERSAO_SISTEMA", versaoSistema)).FirstOrDefault();
        }

        public List<ConsultaAnaliseDiagnosticaPrefeituraExercicioInfo> GetAnaliseDiagnosticaPrefeituraExercicio(Int32 idPrefeitura, Int32 Exercicio) 
        {
            return this.ExecuteFunction<ConsultaAnaliseDiagnosticaPrefeituraExercicioInfo>("PR_ANALISE_DIAGNOSTICA", new ObjectParameter("ID_PREFEITURA", idPrefeitura), new ObjectParameter("EXERCICIO", Exercicio), new ObjectParameter("ID_EXERCICIO", 0)).ToList();
        }

        public ConsultaMunicipioPopulacaoVulnerabilidadeIndicadoresInfo GetPopulacaoVulnerabilidadeByMunicipio(Int32 idMunicipio, Int32 versaoSistema) 
        {
            return this.ExecuteFunction<ConsultaMunicipioPopulacaoVulnerabilidadeIndicadoresInfo>("PR_POPULACAO_VULNERABILIDADE_INDICADORES", new ObjectParameter("ID_MUNICIPIO", idMunicipio), new ObjectParameter("VERSAO_SISTEMA", versaoSistema)).FirstOrDefault();
        }

        //Welington P.
        public ConsultaMunicipioRedeSocioAssistencialIndicadoresInfo GetIndicadoresRedeSocioAssistencial(Int32 idMunicipio, Int32 versaoSistema) {
            return this.ExecuteFunction<ConsultaMunicipioRedeSocioAssistencialIndicadoresInfo>("PR_MUNICIPIO_REDE_SOCIOASSISTENCIAL_INDICADORES", new ObjectParameter("ID_MUNICIPIO", idMunicipio), new ObjectParameter("VERSAO_SISTEMA", versaoSistema)).FirstOrDefault();
        }

         
        public ProgramasDesenvolvidosMunicipio2016Info GetProgramasDesenvolvidosMunicipio2016(Int32 idPrefeitura)
        {
            return this.ExecuteFunction<ProgramasDesenvolvidosMunicipio2016Info>("PR_PROGRAMAS_DESENV_ANO_ANTERIOR", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).FirstOrDefault();
        }

        public ProgramasDesenvolvidosMunicipio2016Info GetProgramasDesenvolvidosMunicipio2021(Int32 idPrefeitura)
        {
            return this.ExecuteFunction<ProgramasDesenvolvidosMunicipio2016Info>("PR_PROGRAMAS_DESENV_ANO_ANTERIOR", new ObjectParameter("ID_PREFEITURA", idPrefeitura)).FirstOrDefault();
        }

        //Welington P.
        public ConsultaIndicadoresPETIInfo GetIndicadoresPETI(Int32 idMunicipio)
        {
            return this.ExecuteFunction<ConsultaIndicadoresPETIInfo>("PR_CONSULTA_INDICADORES_PETI", new ObjectParameter("ID_MUNICIPIO", idMunicipio)).FirstOrDefault();
        }



        #endregion

        public ObjectResult<ConsultaProgramaProjetoExercicioInfo> GetConsultaEstadualExercicioByPrefeitura(int idPrefeitura, int exercicio)
        {
            return this.ExecuteFunction<ConsultaProgramaProjetoExercicioInfo>("PR_PROGRAMA_PROJETO_PREVISAO_ORCAMENTARIA"
                , new ObjectParameter("ID_PREFEITURA", idPrefeitura)
                , new ObjectParameter("ID_EXERCICIO", exercicio));
        }


        public ObjectResult<ConsultaProgramaProjetoExercicioInfo> GetConsultaFederalExercicioByPrefeitura(int idPrefeitura, int exercicio)
        {
            var retorno = this.ExecuteFunction<ConsultaProgramaProjetoExercicioInfo>("PR_PROGRAMA_PROJETO_PREVISAO_ORCAMENTARIA"
                , new ObjectParameter("ID_PREFEITURA", idPrefeitura)
                , new ObjectParameter("ID_EXERCICIO", exercicio));

            return retorno;
        }

        public DradsPlanoMunicipalRecursosInfo GetResumoCofinanciamentoDradsBy(Int32 idPrefeitura, Int32 exercicio)
        {
            var lista = this.ExecuteFunction<DradsPlanoMunicipalRecursosInfo>("PR_DRADS_COFINANCIAMENTOS_PMAS"
                , new ObjectParameter("ID_PREFEITURA", idPrefeitura)
                , new ObjectParameter("ID_EXERCICIO", exercicio));

                return lista.FirstOrDefault();
        }

        public DradsPlanoMunicipalRecursosReprogramadoInfo GetResumoCofinanciamentoReprogramadoDradsBy(Int32 idPrefeitura, Int32 exercicio)
        {
            return this.ExecuteFunction<DradsPlanoMunicipalRecursosReprogramadoInfo>("PR_DRADS_COFINANCIAMENTOS_REPROGRAMADOS_PMAS_NOVO"
                , new ObjectParameter("ID_PREFEITURA", idPrefeitura)
                , new ObjectParameter("ID_EXERCICIO", exercicio)).FirstOrDefault();
        }

        public DradsPlanoMunicipalDemandasParlamentaresInfo GetResumoCofinanciamentoDemandasDradsBy(Int32 idPrefeitura, Int32 exercicio)
        {
            return this.ExecuteFunction<DradsPlanoMunicipalDemandasParlamentaresInfo>("PR_DRADS_COFINANCIAMENTOS_DEMANDAS_PMAS_NOVO"
                , new ObjectParameter("ID_PREFEITURA", idPrefeitura)
                , new ObjectParameter("ID_EXERCICIO", exercicio)).FirstOrDefault();
        }


        public DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo GetResumoCofinanciamentoReprogramadoDemandasDradsBy(Int32 idPrefeitura, Int32 exercicio)
        {
            return this.ExecuteFunction<DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo>("PR_DRADS_COFINANCIAMENTOS_REPROGRAMACAO_DEMANDAS_PMAS"
                , new ObjectParameter("ID_PREFEITURA", idPrefeitura)
                , new ObjectParameter("ID_EXERCICIO", exercicio)).FirstOrDefault();
        }

        public DradsPlanoMunicipalBeneficioProgramaRecursosInfo GetResumoCofinanciamentoBeneficioProgramaDradsBy(Int32 idPrefeitura, Int32 exercicio)
        {
            var retorno = this.ExecuteFunction<DradsPlanoMunicipalBeneficioProgramaRecursosInfo>("PR_DRADS_COFINANCIAMENTOS_BENEFICIO_PROGRAMA_PMAS"
                , new ObjectParameter("ID_PREFEITURA", idPrefeitura)
                , new ObjectParameter("ID_EXERCICIO", exercicio)).FirstOrDefault();
            return (retorno != null) ? retorno : new DradsPlanoMunicipalBeneficioProgramaRecursosInfo();
        }

        public IEnumerable<RedeServicoSocioassistencialRelatorio> GetRedeServicoSocioassistencialRelatorio(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<RedeServicoSocioassistencialRelatorio>("PR_RELATORIO_REDE_SERVICOS_SOCIOASSISTENCIAIS_SIMPLES_PSC_LA"
                , new ObjectParameter("ID_EXERCICIO", exercicio));
            return lista;
        }
      
        /*Prestação de contas-----------------------------------------------------------------------------*/

        public IEnumerable<RPrestacaoDeContasBasica> GetPretacaoDeContasBasicaRelatorio(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<RPrestacaoDeContasBasica>("PR_RELATORIO_PRESTACAO_DE_CONTAS_PROTECAO_BASICA"
                , new ObjectParameter("EXERCICIO", exercicio));
            return lista;
        }

        public IEnumerable<RPrestacaoDeContasMedia> GetPretacaoDeContasMediaRelatorio(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<RPrestacaoDeContasMedia>("PR_RELATORIO_PRESTACAO_DE_CONTAS_PROTECAO_MEDIA"
                , new ObjectParameter("EXERCICIO", exercicio)); 
            return lista;
        }

        public IEnumerable<RPrestacaoDeContasAlta> GetPretacaoDeContasAltaRelatorio(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<RPrestacaoDeContasAlta>("PR_RELATORIO_PRESTACAO_DE_CONTAS_PROTECAO_ALTA"
                , new ObjectParameter("EXERCICIO", exercicio));
            return lista;
        }

        public IEnumerable<RPrestacaoDeContasBeneficiosEventuais> GetPretacaoDeContasBeneficiosEventuaisRelatorio(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<RPrestacaoDeContasBeneficiosEventuais>("PR_RELATORIO_PRESTACAO_DE_CONTAS_BENEFICIOS_EVENTUAIS"
                , new ObjectParameter("EXERCICIO", exercicio));
            return lista;
        }

        public IEnumerable<RPrestacaoDeContasProgramasProjetos> GetPretacaoDeContasProgramasProjetosRelatorio(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<RPrestacaoDeContasProgramasProjetos>("PR_RELATORIO_PRESTACAO_DE_CONTAS_PROGRAMAS_PROJETOS"
                , new ObjectParameter("EXERCICIO", exercicio));
            return lista;
        }
        
        /*-------------------------------------------------------------------------------------------------------*/

        public IQueryable<RelatorioAnaliseDiagnosticaProcInfo> GetAnaliseDiagnosticaExercicio(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<RelatorioAnaliseDiagnosticaProcInfo>("PR_ANALISE_DIAGNOSTICA_RELATORIO_NOVE"
             , new ObjectParameter("EXERCICIO", exercicio));
            return lista.AsQueryable();
        }

        public IEnumerable<RedeServicoSocioassistencialRegionalizadosRelatorio> GetRedeServicosRegionalizadosRelatorio(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<RedeServicoSocioassistencialRegionalizadosRelatorio>("PR_RELATORIO_REDE_SERVICOS_SOCIOASSISTENCIAIS_REGIONALIZADOS"
                , new ObjectParameter("ID_EXERCICIOS", exercicio));
            return lista;
        }

        public IEnumerable<RelAcaoVigilanciaInfo> GetRelatorioAcaoVigilancia(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<RelAcaoVigilanciaInfo>("PR_ACOES_VIGILANCIA_SOCIOASSISTENCIAL"
                , new ObjectParameter("ID_EXERCICIO", exercicio));
            return lista;
        }



        public IEnumerable<RelatorioAEPETIInfo> GetAEPETIRelatorio()
        {
            var lista = this.ExecuteFunction<RelatorioAEPETIInfo>("PR_RELATORIO_AEPETI");
            return lista;
        }

        public IEnumerable<RedeServicoSocioassistencialDetalhamentoInfo> GetRedeServicoSocioassistencialRelatorioDetalhamento(DateTime dataImplementacao)
        {
            var lista = this.ExecuteFunction<RedeServicoSocioassistencialDetalhamentoInfo>("PR_RELATORIO_REDE_SERVICOS_SOCIOASSISTENCIAIS_DETALHAMENTO"
                , new ObjectParameter("DATA_IMPLANTACAO", dataImplementacao));
            return lista;
        }


        public List<InformacoesMunicipaisBasicasInfo> GetInformacoesMunicipaisBasicas(DateTime dataImplementacao)
        {
            var lista = this.ExecuteFunction<InformacoesMunicipaisBasicasInfo>("PR_RELATORIO_INFORMACOES_MUNICIPAIS_BASICAS", new ObjectParameter("DATA_CRIACAO", dataImplementacao)).ToList();
            return lista;
        }


        public IQueryable<InformacoesBasicasDradsInfo> GetInformacoesBasicasDrads(DateTime dataImplementacao)
        {
            var lista = this.ExecuteFunction<InformacoesBasicasDradsInfo>("PR_RELATORIO_INFORMACOES_BASICAS_DRADS", new ObjectParameter("DATA_CRIACAO", dataImplementacao));
            return lista.AsQueryable();
        }

        public IQueryable<RHOrgaoGestorInfo> GetRHOrgaoGestor(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<RHOrgaoGestorInfo>("PR_RELATORIO_RH_ORGAO_GESTOR", new ObjectParameter("EXERCICIO", exercicio));
            return lista.AsQueryable();
        }


        public void SavePrefeiturasSituacoesQuadros(int idRecurso, int idSituacao, int exercicio)
        {
            var retorno = this.ExecuteFunction("PR_BLOQUEIO_DESBLOQUEIO_GERAL"
                , new ObjectParameter("ID_RECURSO", idRecurso)
                , new ObjectParameter("ID_SITUACAO", idSituacao)
                , new ObjectParameter("EXERCICIO", exercicio));
        }

        public void SavePrefeiturasSituacoesQuadrosEFLO(int idRecurso, int idSituacao, int exercicio)
        {
            var retorno = this.ExecuteFunction("PR_PREFEITURA_BLOQUEIO_QUADRO_FINANCEIRO_GERAL"
                , new ObjectParameter("ID_SITUACAO_QUADRO", idSituacao)
                , new ObjectParameter("ID_RECURSO", idRecurso)
                , new ObjectParameter("EXERCICIO", exercicio));
        }


        public IQueryable<CronogramaDesembolsoRelatorio22Info> GetCronogramaDesembolso(Int32 exercicio,Int32 idPrefeitura)
        {
            var lista = this.ExecuteFunction<CronogramaDesembolsoRelatorio22Info>("PR_RELATORIO_CRONOGRAMA_DESEMBOLSO_22", new ObjectParameter("EXERCICIO", exercicio), new ObjectParameter("ID_PREFEITURA", idPrefeitura));
            return lista.AsQueryable();
        }

        public IQueryable<DistribuicaoEstadualProtecaoSocialInfo> GetDistribuicaoEstadualProtecaoSocialProc(Int32 exercicio)
        {
            var lista = this.ExecuteFunction<DistribuicaoEstadualProtecaoSocialInfo>("PR_DISTRIBUICAO_ESTADUAL_PROTECAO_SOCIAL", new ObjectParameter("EXERCICIO", exercicio));
            return lista.AsQueryable();
        }
    }
}
