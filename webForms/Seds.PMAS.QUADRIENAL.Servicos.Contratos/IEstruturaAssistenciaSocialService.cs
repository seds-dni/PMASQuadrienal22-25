using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.Servicos.Contratos
{
    [ServiceContract]
    public interface IEstruturaAssistenciaSocialService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<FormacaoInfo> GetFormacoesAcademicas();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<CargoInfo> GetCargosAdministrativos();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<EscolaridadeInfo> GetEscolaridades();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<EstruturaInfo> GetEstruturasOrgaoGestor();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ConselhosInfo> GetTiposConselhos();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<TipoProtecaoSocialInfo> GetTiposProtecaoSocial();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<TipoServicoInfo> GetTiposServico();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<TipoServicoInfo> GetTiposServicoByTipoProtecaoSocial(Int32 idTipoProtecao);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<TipoServicoInfo> GetTiposServicoNaoTipificadoByTipoProtecaoSocial(Int32 idTipoProtecao);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TipoServicoInfo GetTiposServicoById(Int32 idTipoServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<SituacaoVulnerabilidadeInfo> GetSituacoesVulnerabilidade();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<SituacaoEspecificaInfo> GetSituacoesEspecificas();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<SituacaoEspecificaInfo> GetSituacoesEspecificasByUsuario(Int32 idTipoServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<SituacaoEspecificaInfo> GetSituacoesEspecificasBySituacaoVulnerabilidade(Int32 idSituacaoVulnerabilidade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AbrangenciaServicoInfo> GetAbrangenciasServico();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<UsuarioTipoServicoInfo> GetUsuariosByTipoServico(Int32 idTipoServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UsuarioTipoServicoInfo GetUsuarioById(Int32 idUsuarioTipoServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AcaoSocioAssistencialInfo> GetAcoesSocioAssistenciaisCRAS();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AcaoSocioAssistencialInfo> GetAcoesSocioAssistenciaisCREAS();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AcaoSocioAssistencialInfo> GetAcoesSocioAssistenciaisCentroPOP();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AtividadeSocioAssistencialInfo> GetAtividadesSocioAssistenciaisByTipoServico(Int32 idTipoServico);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<TipoParceriaInfo> GetTiposParceria();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ParceriaInfo> GetParcerias();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<TipoTransferenciaRendaInfo> GetTipoTransferenciaRenda();
        
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<UsuarioTransferenciaRendaInfo> GetUsuarioTransferenciaRenda();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AcaoSocioAssistencialComplementarInfo> GetAcoesSocioAssistenciaisComplementares();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<FormaAtuacaoInfo> GetFormaAtuacao();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<DistritosSaoPauloInfo> GetDistritosSP();

        #region Motivos de não instalação CRAS, CREAS e Centro Pop
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCRAS();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCREAS();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<MotivoNaoInstalacaoInfo> GetMotivoNaoInstalacaoCentroPOP();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<TipoDemandaAtendimentoInfo> GetTipoDemandaAtendimento();

        #endregion

        #region Beneficios Eventuais
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<CriterioConcessaoInfo> GetCriteriosConcessaoParaBeneficiosEventuais();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<OrgaoResponsavelInfo> GetOrgaosReponsaveisParaBeneficiosEventuais();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<BeneficioEventualInfo> GetBeneficiosEventuaisByTipoBeneficioEventual(int idTipoBeneficioEventual);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<TipoBeneficioEventualInfo> GetTiposBeneficiosEventuais();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<NecessidadeBeneficioEventualInfo> GetNecessidadesBeneficiosEventuaisByTipoBeneficioEventual(int idTipoBeneficioEventual);
        #endregion

        #region Ações        
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AcaoPlanejamentoInfo> GetAcoesPlanejamentoByEixo(int idEixoAcaoPlanejamento);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<EixoAcaoPlanejamentoInfo> GetEixosAcaoPlanejamento();
        #endregion

        #region Vigilância, Monitoramento e Avaliação
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AcaoVigilanciaSocioAssistencialInfo> GetAcoesVigilanciaSocioAssistencialByEixo(Int32 idEixo);
        
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<AprimoramentoAcaoInfo> GetAprimoramentosAcoes();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ObjetivoAvaliacaoInfo> GetObjetivosAvaliacao();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ProcedimentoAvaliacaoInfo> GetProcedimentosAvaliacao();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<MotivoNaoAvaliacaoInfo> GetMotivosNaoAvaliacao();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ProcedimentoMonitoramentoInfo> GetProcedimentosMonitoramento();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<InstrumentoMonitoramentoInfo> GetInstrumentosMonitoramentoByProcedimento(Int32 idProcedimento);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<FocoMonitoramentoInfo> GetFocosMonitoramento();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<MeioDivulgacaoInfo> GetMeiosDivulgacao();
        #endregion    
        
    }
}
