using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class ProgramaProjetoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }

        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public String Objetivo { get; set; }
        [DataMember]
        public String Acoes { get; set; }
        [DataMember]
        public Int32 IdAbrangenciaTerritorial { get; set; }
        [DataMember]
        public AbrangenciaTerritorialInfo AbrangenciaTerritorial { get; set; }
        [DataMember]
        public Boolean PossuiParceriaFormal { get; set; }
        [DataMember]
        public Int32? MesInicio { get; set; }
        [DataMember]
        public Int32? AnoInicio { get; set; }
        [DataMember]
        public Int32? MesTermino { get; set; }
        [DataMember]
        public Int32? AnoTermino { get; set; }

        [DataMember]
        public List<ProgramaProjetoParceriaInfo> Parcerias { get; set; }

         
        [DataMember]
        public Boolean? ProgramaMunicipal { get; set; }
        [DataMember]
        public Boolean? ProgramaEstadual { get; set; }
        [DataMember]
        public Boolean? ProgramaFederal { get; set; }

        [DataMember]
        public Boolean? AderenciaACESSUAS { get; set; }

        [DataMember]
        public Int32? MetaPactuada { get; set; }

        [DataMember]
        public Boolean? BeneficiarioAtendidoRedeSocioassistencial { get; set; }




        //#region Amigo do Idoso - Dia do idoso

        //[DataMember]
        //public Decimal? ValorDiaIdoso { get; set; }
        //[DataMember]
        //public Int16? MesRepasseDiaIdoso { get; set; }
        //[DataMember]
        //public Int16? AnoRepasseDiaIdoso { get; set; }

        //#endregion

        //#region Amigo do Idoso - Convivência do idoso

        //[DataMember]
        //public Decimal? ValorConvivenciaIdoso { get; set; }
        //[DataMember]
        //public Int16? MesRepasseConvivenciaIdoso { get; set; }
        //[DataMember]
        //public Int16? AnoRepasseConvivenciaIdoso { get; set; }

        //public Int32 Exercicio { get; set; }
        //#endregion



        [DataMember]
        public Decimal? ValorFEASSegunda { get; set; }
     
        [DataMember]
        public Int32? IdUsuarioTransferenciaRenda { get; set; }
        [DataMember]
        public UsuarioTransferenciaRendaInfo UsuarioTransferenciaRenda { get; set; }

        [DataMember]
        public Int32? IdFaseProgramaSaoPauloSolidario { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioMesInicioBuscaAtiva { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioAnoInicioBuscaAtiva { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioMesTerminoBuscaAtiva { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioAnoTerminoBuscaAtiva { get; set; }

        [DataMember]
        public Boolean? SaoPauloSolidarioOrgaoGestorExecutaBuscaAtiva { get; set; }
        [DataMember]
        public Boolean? SaoPauloSolidarioCRASExecutaBuscaAtiva { get; set; }
        [DataMember]
        public Boolean? SaoPauloSolidarioCREASExecutaBuscaAtiva { get; set; }
        [DataMember]
        public Boolean? SaoPauloSolidarioUnidadePrivadaExecutaBuscaAtiva { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorFMASBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorOrcamentoMunicipalBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFundoMunicipalBuscaAtiva { get; set; }
        [DataMember]
        public Boolean? SaoPauloSolidarioValorFEASRetidoFMAS2014 { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioMesRepasseFEASBuscaAtiva { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioAnoRepasseFEASBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFEASBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorOrcamentoEstadualBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFundoEstadualBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFNASBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorOrcamentoFederalBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFundoFederalBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorIGDPBFBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorIGDSUASBuscaAtiva { get; set; }

        [DataMember]
        public Boolean? SaoPauloSolidarioOrgaoGestorExecutaAgendaFamilia { get; set; }
        [DataMember]
        public Boolean? SaoPauloSolidarioCRASExecutaAgendaFamilia { get; set; }
        [DataMember]
        public Boolean? SaoPauloSolidarioCREASExecutaAgendaFamilia { get; set; }

        [DataMember]
        public Int32? SaoPauloSolidarioMesRepasseFEASAgendaFamilia { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioAnoRepasseFEASAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFEASAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorOrcamentoEstadualAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFundoEstadualAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFMASAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorOrcamentoMunicipalAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFundoMunicipalAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFNASAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorOrcamentoFederalAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFundoFederalAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorIGDPBFAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorIGDSUASAgendaFamilia { get; set; }

        [DataMember]
        public Int32? SaoPauloSolidarioNumeroFamiliasAgendaFamilia2012 { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013 { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioNumeroFamiliasAgendaFamilia2014 { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioNumeroFamiliasAgendaFamilia2015 { get; set; }

        [DataMember]
        public Int32? SaoPauloSolidarioNumeroFamiliasAgendaFamilia2016 { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioMeta { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioRepasseAnual { get; set; }

        [DataMember]
        public List<SPSolidarioAgendaFamiliaParceriaInfo> ParceriasSaoPauloSolidarioAgendaFamilia { get; set; }

        [DataMember]
        public List<SPSolidarioAlemdaRendaParceriaInfo> ParceriasSaoPauloSolidarioAlemdaRenda { get; set; }
        [DataMember]
        public Boolean? PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia { get; set; }

        [DataMember]
        public Decimal? ValorPrevisaoAnualACESSUAS { get; set; }

        [DataMember]
        public Boolean? SaoPauloSolidarioValorFEASRetidoFMAS2014AgendaFamilia { get; set; }

        [DataMember]
        public Boolean? SaoPauloSolidarioPossuiPlanejamentoBens { get; set; }
        [DataMember]
        public Boolean? SaoPauloSolidarioPossuiPlanejamentoServicos { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorFMASAlemDaRenda { get; set; }

        [DataMember]
        public Int32? SaoPauloSolidarioMesRepasseFEASAlemDaRenda { get; set; }

        [DataMember]
        public Int32? SaoPauloSolidarioAnoRepasseFEASAlemDaRenda { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorOrcamentoMunicipalAlemDaRenda { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorFundoMunicipalAlemDaRenda { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorFEASAlemDaRenda { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorOrcamentoEstadualAlemDaRenda { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorFundoEstadualAlemDaRenda { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorFNASAlemDaRenda { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorOrcamentoFederalAlemDaRenda { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorFundoFederalAlemDaRenda { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorIGDPBFAlemDaRenda { get; set; }

        [DataMember]
        public Decimal? SaoPauloSolidarioValorIGDSUASAlemDaRenda { get; set; }
        [DataMember]
        public Boolean? PossuiParceriaFormalSaoPauloSolidarioAlemDaRenda { get; set; }
        [DataMember]
        public Boolean? SaoPauloSolidarioAlemDaRendaHouvePlanejamentoAquisicaoBens { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorCusteioAlemDaRenda { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorObrasAlemDaRenda { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorEquipamentosAlemDaRenda { get; set; }

        [DataMember]
        public List<SPSolidarioPlanejamentoBensInfo> SaoPauloSolidarioPlanejamentoBens { get; set; }
        [DataMember]
        public List<SPSolidarioPlanejamentoServicosInfo> SaoPauloSolidarioPlanejamentoServicos { get; set; }

        public Boolean? SaoPauloSolidarioValorFEASRetidoFMASAlemdaRenda { get; set; }

        public Boolean? ExecutaAcaoVivaleite { get; set; }

        public Boolean? PossuiInterlocutorMunicipal { get; set; }

        public Boolean? PossuiUnidadePublica { get; set; }

        public Boolean? PossuiUnidadePrivada { get; set; }

        public Int32? NumeroBeneficiariosMensal { get; set; }

        public Int32? IdUsuarioProgramaProjeto { get; set; }

        public List<UnidadeOfertanteInfo> UnidadeOfertante { get; set; }

        public List<AcoesDesenvolvidaProgramasInfo> AcoesDesenvolvidasPrograma { get; set; }

        public List<CaracterizacaoUsuariosInfo> CaracterizacaoUsuarios { get; set; }

        public InterlocutorMunicipalInfo InterlocutorMunicipal { get; set; }

        public List<AcaoSocioAssistencialInfo> AcoesSocioAssistenciais { get; set; }

        public List<UnidadePrivadaInfo> UnidadesPrivadas { get; set; }

        public Int32? NumeroRefeicoesBomPrato { get; set; }


        public Boolean? AderiuFamiliaPaulista { get; set; }

        public Boolean? AderiuPlanoAcao { get; set; }

        //Campos adicionados para atuarem de forma genérica 

        public Boolean ExecutaPrograma { get; set; }

        public DateTime? DataAdesaoPrograma { get; set; }

        public Int32? MesRepasse { get; set; }

        public Int32? AnoRepasse { get; set; }

        public Int32? MesRepasseSegunda { get; set; }

        public Int32? AnoRepasseSegunda { get; set; }


        [DataMember]
        public List<ProgramaProjetoGrupoGestorInfo> GrupoGestores { get; set; }

        public PlanoAcaoInfo PlanoAcao { get; set; }

        [DataMember]
        public List<IdentificacaoTerritorioInfo> IdentificacoesTerritorio { get; set; }

        [DataMember]
        public Decimal? ValorProgramadoAnoAtual { get; set; }
        [DataMember]
        public Decimal? ValorProgramadoProximoAno { get; set; }

        public Boolean Ativo { get; set; }

        public ProgramaProjetoPrevisaoAnualBeneficiariosInfo PrevisaoAnual { get; set; }

        public DateTime? DataInauguracaoCentroDiaIdoso { get; set; }

        public Int32? IdCREASReferencia { get; set; }

        public DateTime? DataInauguracaoConvivenciaIdoso { get; set; }

        public Int32? IdCRASReferencia { get; set; }

        public Boolean? TransferenciaRendaDireta { get; set; }

        public Int32? NumeroBeneficiarios { get; set; }

        public Decimal? PrevisaoMensalRepasse { get; set; }

        public Boolean ConvenioCentroDiaIdoso { get; set; }

        public Boolean ConvenioCentroConvivenciaIdoso { get; set; }

        public Boolean NaoExisteCRASReferencia { get; set; }

        public Boolean NaoExisteCREASReferencia { get; set; }

        public List<ProgramaProjetoRecursoFinanceiroInfo> ProgramasProjetosRecursoFinanceiro { get; set; }
        public List<ProgramaProjetoParcelasInfo> ProgramasProjetosParcelasInfo { get; set; }

    }
}
