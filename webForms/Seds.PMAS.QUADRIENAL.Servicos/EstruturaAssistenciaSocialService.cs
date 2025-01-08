using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Servicos.Contratos;
using System.ServiceModel;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio;
using System.Security.Permissions;
using Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial;
using Seds.PMAS.QUADRIENAL.Negocio.EstruturaAssistenciaSocial;


namespace Seds.PMAS.QUADRIENAL.Servicos
{
    /// <summary>
    /// Serviço Responsável por fornecer informações sobre a Estrutura da Assistência Social utilizada no PMAS 2017
    /// </summary>
    [ServiceBehavior(Namespace = "http://seds.sp.gov.br/estruturaassistenciasocial",
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerSession,
    TransactionIsolationLevel = IsolationLevel.ReadCommitted,
    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class EstruturaAssistenciaSocialService : IEstruturaAssistenciaSocialService
    {
        /// <summary>
        /// Selecionar Formações Acadêmicas
        /// </summary>        
        /// <returns>Dados da Formação Acadêmica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<FormacaoInfo> GetFormacoesAcademicas()
        {
            ContextManager.OpenConnection();
            var lst = new Formacao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Cargos Administrativos
        /// </summary>        
        /// <returns>Dados do Cargo Administrativo</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<CargoInfo> GetCargosAdministrativos()
        {
            ContextManager.OpenConnection();
            var lst = new Cargo().GetAll().OrderBy(t => t.Ordem).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Escolaridades
        /// </summary>        
        /// <returns>Dados da Escolaridade</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<EscolaridadeInfo> GetEscolaridades()
        {
            ContextManager.OpenConnection();
            var lst = new Escolaridade().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Estruturas de Órgãos Gestores
        /// </summary>        
        /// <returns>Dados da Estrutura</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<EstruturaInfo> GetEstruturasOrgaoGestor()
        {
            ContextManager.OpenConnection();
            var lst = new Estrutura().GetAll().OrderBy(t => t.Ordem).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Tipos de Conselhos
        /// </summary>        
        /// <returns>Dados do Tipo de Conselho</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ConselhosInfo> GetTiposConselhos()
        {
            ContextManager.OpenConnection();
            var lst = new Conselhos().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Tipos de Proteção Social
        /// </summary>        
        /// <returns>Dados do Tipo de Proteção Social</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TipoProtecaoSocialInfo> GetTiposProtecaoSocial()
        {
            ContextManager.OpenConnection();
            var lst = new TipoProtecaoSocial().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }



        /// <summary>
        /// Selecionar Tipos de Serviço Social
        /// </summary>        
        /// <returns>Dados do Tipo de Serviço Social</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TipoServicoInfo> GetTiposServico()
        {
            ContextManager.OpenConnection();
            var lst = new TipoServico().GetAll().OrderBy(t => t.IdTipoProtecaoSocial).ThenBy(t => t.Ordem).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Tipos de Serviço Social por Proteção Social
        /// </summary>
        /// <param name="idTipoProtecao">Id do Tipo de Proteção Social</param>
        /// <returns>Dados do Tipo de Serviço Social</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TipoServicoInfo> GetTiposServicoByTipoProtecaoSocial(Int32 idTipoProtecao)
        {
            ContextManager.OpenConnection();
            var lst = new TipoServico().GetAll().Where(t => t.IdTipoProtecaoSocial == idTipoProtecao && (!t.NaoTipificado.HasValue || (t.NaoTipificado.HasValue && !t.NaoTipificado.Value)))
                .OrderBy(t => t.IdTipoProtecaoSocial).ThenBy(t => t.Ordem).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Tipos de Serviço Social por Proteção Social que forem Não Tipificados
        /// </summary>
        /// <param name="idTipoProtecao">Id do Tipo de Proteção Social</param>
        /// <returns>Dados do Tipo de Serviço Social</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TipoServicoInfo> GetTiposServicoNaoTipificadoByTipoProtecaoSocial(Int32 idTipoProtecao)
        {
            ContextManager.OpenConnection();
            var lst = new TipoServico().GetAll().Where(t => t.IdTipoProtecaoSocial == idTipoProtecao && t.NaoTipificado.HasValue && t.NaoTipificado.Value)
                .OrderBy(t => t.IdTipoProtecaoSocial).ThenBy(t => t.Ordem).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Tipo de Serviço Social através do Identificador
        /// </summary>        
        /// <returns>Dados do Tipo de Serviço Social</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public TipoServicoInfo GetTiposServicoById(Int32 idTipoServico)
        {
            ContextManager.OpenConnection();
            var obj = new TipoServico().GetById(idTipoServico);
            ContextManager.CloseConnection();
            return obj;
        }


        /// <summary>
        /// Selecionar Tipo de Serviço Social através do Identificador
        /// </summary>        
        /// <returns>Dados do Tipo de Serviço Social</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public FormaAtuacaoInfo GetFormaAtuacaoById(Int32 idFormaAtuacao)
        {
            ContextManager.OpenConnection();
            var obj = new FormaAtuacao().GetFormaAtuacaoById(idFormaAtuacao);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Tipo de Avaliação do Local de Execução
        /// </summary>        
        /// <returns>Dados do Tipo de Serviço Social</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public List<AvaliacaoLocalExecucaoInfo> GetAvaliacoesLocal()
        {
            ContextManager.OpenConnection();
            var lst = new AvaliacaoLocalExecucao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Tipo de Avaliacao
        /// </summary>        
        /// <returns>Dados do Tipo de Serviço Social</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public List<AvaliacaoServicoRecursoFinanceiroInfo> GetAvaliacoes()
        {
            ContextManager.OpenConnection();
            var lst = new AvaliacaoServicoRecursoFinanceiro().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        /// <summary>
        /// Selecionar Situações de Vulnerabilidade Social ou Risco Social
        /// </summary>        
        /// <returns>Dados da Situação de Vulnerabilidade Social</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<SituacaoVulnerabilidadeInfo> GetSituacoesVulnerabilidade()
        {
            ContextManager.OpenConnection();
            var lst = new SituacaoVulnerabilidade().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }



        /// <summary>
        /// Selecionar Situações Específicas de Vulnerabilidade
        /// </summary>
        /// <returns>Dados da Situação Específica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<SituacaoEspecificaInfo> GetSituacoesEspecificas()
        {
            ContextManager.OpenConnection();
            var lst = new SituacaoEspecifica().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Situações Específicas de Vulnerabilidade por Tipo de Serviço
        /// </summary>
        /// <returns>Dados da Situação Específica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<SituacaoEspecificaInfo> GetSituacoesEspecificasByUsuario(Int32 idUsuarioTipoServico)
        {
            ContextManager.OpenConnection();
            var lst = new SituacaoEspecifica().GetByUsuarios(idUsuarioTipoServico).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Situações Específicas de Vulnerabilidade por Situação de Vulnerabilidade
        /// </summary>
        /// <param name="idSituacaoVulnerabilidade">Id da Situação de Vulnerabilidade</param>
        /// <returns>Dados da Situação Específica</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<SituacaoEspecificaInfo> GetSituacoesEspecificasBySituacaoVulnerabilidade(Int32 idSituacaoVulnerabilidade)
        {
            ContextManager.OpenConnection();
            var lst = new SituacaoEspecifica().GetBySituacaoVulnerabilidade(idSituacaoVulnerabilidade).ToList();
            ContextManager.CloseConnection();
            return lst;

        }

        /// <summary>
        /// Selecionar Abrangência dos Serviços
        /// </summary>
        /// <returns>Dados da Abrangência dos Serviços</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AbrangenciaServicoInfo> GetAbrangenciasServico()
        {
            ContextManager.OpenConnection();
            var lst = new AbrangenciaServico().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<FormaJuridicaInfo> GetFormaJuridica()
        {
            ContextManager.OpenConnection();

            var lst = new FormaJuridica().GetAll().ToList();

            ContextManager.CloseConnection();

            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ConsorcioPublicoInfo GetConsorcioPublico(int idServicoRecursoFinanceiroPublico)
        {
            ContextManager.OpenConnection();

            var lst = new ConsorcioPublico().GetIdServico(idServicoRecursoFinanceiroPublico).FirstOrDefault();

            ContextManager.CloseConnection();

            return lst;
        }

        public void SalvarConsorcio(ConsorcioPublicoInfo consorcio) 
        {
            ContextManager.OpenConnection();

            ConsorcioPublico consorcioPublico = new ConsorcioPublico();

            consorcioPublico.SalvarConsorcioPublico(consorcio);

            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ConsorcioCRASInfo GetConsorcioCRAS(int idServicoRecursoFinanceiroCRAS)
        {
            ContextManager.OpenConnection();

            var lst = new ConsorcioCRAS().GetIdServico(idServicoRecursoFinanceiroCRAS).FirstOrDefault();

            ContextManager.CloseConnection();

            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ConsorcioCREASInfo GetConsorcioCREAS(int idServicoRecursoFinanceiroCREAS)
        {
            ContextManager.OpenConnection();

            var lst = new ConsorcioCREAS().GetIdServico(idServicoRecursoFinanceiroCREAS).FirstOrDefault();

            ContextManager.CloseConnection();

            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ConsorcioCentroPOPInfo GetConsorcioCentroPOP(int idServicoRecursoFinanceiroCentroPOP)
        {
            ContextManager.OpenConnection();

            var lst = new ConsorcioCentroPOP().GetIdServico(idServicoRecursoFinanceiroCentroPOP).FirstOrDefault();

            ContextManager.CloseConnection();

            return lst;
        }

        public void SalvarConsorcioCRAS(ConsorcioCRASInfo consorcio)
        {
            ContextManager.OpenConnection();

            ConsorcioCRAS consorcioCRAS = new ConsorcioCRAS();

            consorcioCRAS.SalvarConsorcioCRAS(consorcio);

            ContextManager.CloseConnection();
        }

        public void SalvarConsorcioCREAS(ConsorcioCREASInfo consorcio)
        {
            ContextManager.OpenConnection();

            ConsorcioCREAS consorcioCREAS = new ConsorcioCREAS();

            consorcioCREAS.SalvarConsorcioCREAS(consorcio);

            ContextManager.CloseConnection();
        }

        public void SalvarConsorcioCentroPOP(ConsorcioCentroPOPInfo consorcio)
        {
            ContextManager.OpenConnection();

            ConsorcioCentroPOP consorcioCentroPOP = new ConsorcioCentroPOP();

            consorcioCentroPOP.SalvarConsorcioCentroPOP(consorcio);

            ContextManager.CloseConnection();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ConsorcioPrivadoInfo GetConsorcioPrivado(int idServicoRecursoFinanceiroPrivado)
        {
            ContextManager.OpenConnection();

            var lst = new ConsorcioPrivado().GetIdServico(idServicoRecursoFinanceiroPrivado).FirstOrDefault();

            ContextManager.CloseConnection();

            return lst;
        }

        public void SalvarConsorcioPrivado(ConsorcioPrivadoInfo consorcio)
        {
            ContextManager.OpenConnection();

            ConsorcioPrivado consorcioPrivado = new ConsorcioPrivado();

            consorcioPrivado.SalvarConsorcioPrivado(consorcio);

            ContextManager.CloseConnection();
        }

        /// <summary>
        /// Selecionar Usuários (Público Alvo) por Tipo de Serviço
        /// </summary>
        /// <param name="idTipoServico">Id do Tipo de Serviço</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<UsuarioTipoServicoInfo> GetUsuariosByTipoServico(Int32 idTipoServico)
        {
            ContextManager.OpenConnection();
            var lst = new UsuarioTipoServico().GetByTipoServico(idTipoServico).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Usuários (Público Alvo) pelo Identificador
        /// </summary>
        /// <param name="idUsuarioTipoServico">Id do Tipo de Usuário</param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public UsuarioTipoServicoInfo GetUsuarioById(Int32 idUsuarioTipoServico)
        {
            ContextManager.OpenConnection();
            var obj = new UsuarioTipoServico().GetById(idUsuarioTipoServico);
            ContextManager.CloseConnection();
            return obj;
        }

        /// <summary>
        /// Selecionar Ações Socioassistenciais do CRAS
        /// </summary>
        /// <returns>Dados da Ação Socio Assistencial</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcaoSocioAssistencialInfo> GetAcoesSocioAssistenciaisCRAS()
        {
            ContextManager.OpenConnection();
            var lst = new AcaoSocioAssistencial().GetCRAS().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Ações Socioassistenciais do CREAS
        /// </summary>
        /// <returns>Dados da Ação Socio Assistencial</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcaoSocioAssistencialInfo> GetAcoesSocioAssistenciaisCREAS()
        {
            ContextManager.OpenConnection();
            var lst = new AcaoSocioAssistencial().GetCREAS().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Ações Socioassistenciais do Centro POP
        /// </summary>
        /// <returns>Dados da Ação Socio Assistencial</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcaoSocioAssistencialInfo> GetAcoesSocioAssistenciaisCentroPOP()
        {
            ContextManager.OpenConnection();
            var lst = new AcaoSocioAssistencial().GetCentroPOP().ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<UnidadeTipoAtendimentoInfo> GetTiposAtendimentos()
        {
            ContextManager.OpenConnection();
            var lst = new UnidadeTipoAtendimento().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Ações Socioassistenciais da Unidades de Serviços Socioassistenciais (Publica e Privada)
        /// </summary>
        /// <returns>Dados da Ação Socio Assistencial</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcaoSocioAssistencialInfo> GetAcoesSocioAssistenciaisUnidades()
        {
            ContextManager.OpenConnection();
            var lst = new AcaoSocioAssistencial().GetUnidadesSocioAssistenciais().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Atividades Socioassistenciais por Tipo de Serviço
        /// </summary>
        /// <param name="idTipoServico">Id do Tipo de Serviço</param>
        /// <returns>Dados da Atividade Socioassistencial</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AtividadeSocioAssistencialInfo> GetAtividadesSocioAssistenciaisByTipoServico(Int32 idTipoServico)
        {
            ContextManager.OpenConnection();
            var lst = new AtividadeSocioAssistencial().GetByTipoServico(idTipoServico).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar os tipos de Parcerias feitas para os Programas/Projetos
        /// </summary>
        /// <returns>Dados do Tipo de Parceria</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TipoParceriaInfo> GetTiposParceria()
        {
            ContextManager.OpenConnection();
            var lst = new TipoParceria().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }



        /// <summary>
        /// Selecionar Opções de Parcerias para os Programas/Projetos
        /// </summary>
        /// <returns>Dados das Opções de Parcerias</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ParceriaInfo> GetParcerias()
        {
            ContextManager.OpenConnection();
            var lst = new Parceria().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        /// <summary>
        /// Selecionar Opções de Eixo Tecnologico
        /// </summary>
        /// <returns>Dados das Opções de Eixo Tecnologico</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<EixoTecnologicoInfo> GetEixosTecnologicos()
        {
            ContextManager.OpenConnection();
            var lst = new EixoTecnologico().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        /// <summary>
        /// Selecionar Opções de Caracterização dos Usuarios
        /// </summary>
        /// <returns>Dados das Opções de Caracterização dos Usuarios</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<CaracterizacaoUsuariosInfo> GetCaracterizacaoUsuarios()
        {
            ContextManager.OpenConnection();
            var lst = new CaracterizacaoUsuario().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcoesDesenvolvidaProgramasInfo> GetAcoesDesenvolvidaProgramas()
        {
            ContextManager.OpenConnection();
            var lst = new AcoesDesenvolvidaProgramas().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar os Tipos de Transferência de Renda
        /// </summary>
        /// <returns>Dados do Tipo de Transferência de Renda</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TipoTransferenciaRendaInfo> GetTipoTransferenciaRenda()
        {
            ContextManager.OpenConnection();
            var lst = new TipoTransferenciaRenda().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar os Tipos de Usuários para Transferência Renda
        /// </summary>
        /// <returns>Dados dos Tipos de Usuários para Transferência Renda</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<UsuarioTransferenciaRendaInfo> GetUsuarioTransferenciaRenda()
        {
            ContextManager.OpenConnection();
            var lst = new UsuarioTransferenciaRenda().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Buscar os Tipos de Usuários para Programas Projetos
        /// </summary>
        /// <returns>Dados dos  os Tipos de Usuários para o Programa Viva Leite</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<UsuarioProgramaProjetoInfo> GetUsuariosVivaLeite()
        {
            ContextManager.OpenConnection();
            var lst = new UsuarioProgramaProjeto().GetUsuariosVivaLeite().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Ações Socio Assistenciais Complementares
        /// </summary>
        /// <returns>Dados das Ações Socio Assistenciais Complementares</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcaoSocioAssistencialComplementarInfo> GetAcoesSocioAssistenciaisComplementares()
        {
            ContextManager.OpenConnection();
            var lst = new AcaoSocioAssistencialComplementar().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Motivos de serviço estadualizado
        /// </summary>        
        /// <returns>Lista de Motivos de serviço estadualizado</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MotivoEstadualizadoInfo> GetMotivosEstadualizado()
        {
            ContextManager.OpenConnection();
            var lst = new MotivoEstadualizado().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        /// <summary>
        /// Selecionar Caracterizações de Atividades
        /// </summary>        
        /// <returns>Dados das Caracterizações de Atividades</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<CaracterizacaoAtividadesInfo> GetCaracterizacaoAtividades()  
        {
            ContextManager.OpenConnection();
            var lst = new CaracterizacaoAtividades().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<PublicoAlvoInfo> GetPublicoAlvos()
        {
            ContextManager.OpenConnection();
            var lst = new PublicoAlvo().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }


        /// <summary>
        /// Selecionar Lista das Formas de Atuação 
        /// </summary>        
        /// <returns>Lista com dados das Formas de Atuação</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<FormaAtuacaoInfo> GetFormaAtuacao()
        {
            ContextManager.OpenConnection();
            var lst = new FormaAtuacao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Lista dos distritos de São Paulo
        /// </summary>        
        /// <returns>Lista com dados dos distritos de São Paulo</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<DistritosSaoPauloInfo> GetDistritosSP()
        {
            ContextManager.OpenConnection();
            var lst = new DistritosSaoPaulo().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        #region Beneficios Eventuais
        /// <summary>
        /// Selecionar Critérios de Concessão para os Benefícios Eventuais
        /// </summary>
        /// <returns>Dados do Critério de Concessão</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<CriterioConcessaoInfo> GetCriteriosConcessaoParaBeneficiosEventuais()
        {
            ContextManager.OpenConnection();
            var lst = new CriterioConcessao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar Órgãos Responsáveis para os Benefícios Eventuais
        /// </summary>
        /// <returns>Dados do Órgao Responsável</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<OrgaoResponsavelInfo> GetOrgaosReponsaveisParaBeneficiosEventuais()  
        {
            ContextManager.OpenConnection();
            var lst = new OrgaoResponsavel().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar os benefícios eventuais por tipo de benefício eventual
        /// </summary>
        /// <param name="idTipoBeneficioEventual">Id do Tipo de Benefício Eventual</param>
        /// <returns>Dados dos Benefícios Eventuais</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<BeneficioEventualInfo> GetBeneficiosEventuaisByTipoBeneficioEventual(int idTipoBeneficioEventual)
        {
            ContextManager.OpenConnection();
            var lst = new BeneficioEventual().GetByTipoBeneficioEventual(idTipoBeneficioEventual).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TipoBeneficioEventualInfo> GetTiposBeneficiosEventuais()
        {
            ContextManager.OpenConnection();
            var lst = new TipoBeneficioEventual().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Selecionar as necessidades do benefício eventual por tipo de benefício eventual
        /// </summary>
        /// <param name="idTipoBeneficioEventual">Id do Tipo de Benefício Eventual</param>
        /// <returns>Dados das Necessidades</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<NecessidadeBeneficioEventualInfo> GetNecessidadesBeneficiosEventuaisByTipoBeneficioEventual(int idTipoBeneficioEventual)
        {
            ContextManager.OpenConnection();
            var lst = new NecessidadeBeneficioEventual().GetByTipoBeneficioEventual(idTipoBeneficioEventual).ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        #endregion

        #region Motivo Não Instalação CRAS, CREAS e Centro POP
        /// <summary>
        /// Retorna listas com os motivos de não instalação do CRAS, CREAS e Centro POP
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCRAS()
        {
            ContextManager.OpenConnection();
            var lst = new MotivoNaoInstalacao().GetCRAS().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCREAS()
        {
            ContextManager.OpenConnection();
            var lst = new MotivoNaoInstalacao().GetCREAS().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCentroPOP()
        {
            ContextManager.OpenConnection();
            var lst = new MotivoNaoInstalacao().GetPOP().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<TipoDemandaAtendimentoInfo> GetTipoDemandaAtendimento()
        {
            ContextManager.OpenConnection();
            var lst = new TipoDemandaAtendimento().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        #endregion

        #region Ações
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcaoPlanejamentoInfo> GetAcoesPlanejamentoByEixo(int idEixoAcaoPlanejamento)
        {
            ContextManager.OpenConnection();
            var lst = new AcaoPlanejamento().GetByEixo(idEixoAcaoPlanejamento).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<EixoAcaoPlanejamentoInfo> GetEixosAcaoPlanejamento()
        {
            ContextManager.OpenConnection();
            var lst = new EixoAcaoPlanejamento().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        #endregion

        #region Vigilância, Monitoramento e Avaliação
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AcaoVigilanciaSocioAssistencialInfo> GetAcoesVigilanciaSocioAssistencialByEixo(Int32 idEixo)
        {
            ContextManager.OpenConnection();
            var lst = new AcaoVigilanciaSocioAssistencial().GetByEixo(idEixo).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<AprimoramentoAcaoInfo> GetAprimoramentosAcoes()
        {
            ContextManager.OpenConnection();
            var lst = new AprimoramentoAcao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ObjetivoAvaliacaoInfo> GetObjetivosAvaliacao()
        {
            ContextManager.OpenConnection();
            var lst = new ObjetivoAvaliacao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ProcedimentoAvaliacaoInfo> GetProcedimentosAvaliacao()
        {
            ContextManager.OpenConnection();
            var lst = new ProcedimentoAvaliacao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MotivoNaoAvaliacaoInfo> GetMotivosNaoAvaliacao()
        {
            ContextManager.OpenConnection();
            var lst = new MotivoNaoAvaliacao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<ProcedimentoMonitoramentoInfo> GetProcedimentosMonitoramento()
        {
            ContextManager.OpenConnection();
            var lst = new ProcedimentoMonitoramento().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<InstrumentoMonitoramentoInfo> GetInstrumentosMonitoramentoByProcedimento(Int32 idProcedimento)
        {
            ContextManager.OpenConnection();
            var lst = new InstrumentoMonitoramento().GetByProcedimento(idProcedimento).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<FocoMonitoramentoInfo> GetFocosMonitoramento()
        {
            ContextManager.OpenConnection();
            var lst = new FocoMonitoramento().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<MeioDivulgacaoInfo> GetMeiosDivulgacao()
        {
            ContextManager.OpenConnection();
            var lst = new MeioDivulgacao().GetAll().ToList();
            ContextManager.CloseConnection();
            return lst;
        }
        #endregion
    }

}
