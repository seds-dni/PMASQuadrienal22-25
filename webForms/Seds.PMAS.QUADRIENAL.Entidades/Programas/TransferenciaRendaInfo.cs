using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Seds.PMAS.QUADRIENAL.Entidades.Programas;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class TransferenciaRendaInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdPrefeitura { get; set; }
        public PrefeituraInfo Prefeitura { get; set; }
        [DataMember]
        public Int32 IdTipoTransferenciaRenda { get; set; }
        [DataMember]
        public TipoTransferenciaRendaInfo TipoTransferenciaRenda { get; set; }
        [DataMember]
        public Int32 IdUsuarioTransferenciaRenda { get; set; }
        [DataMember]
        public UsuarioTransferenciaRendaInfo UsuarioTransferenciaRenda { get; set; }

        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public String Objetivo { get; set; }
        [DataMember]
        public Boolean BeneficiarioAtendidoRedeSocioAssistencial { get; set; }
        [DataMember]
        public Int32? BPCNumeroBeneficiarios { get; set; }
        public Decimal? BPCRepasseAnual { get { return BPCNumeroBeneficiarios.HasValue ? 880 * 12 * BPCNumeroBeneficiarios.Value : new Nullable<Decimal>(); } } //788 - Welington P. //724 - Bruno V.

        [DataMember]
        public Int32? BolsaFamiliaEstimativaFamilias { get; set; }
        [DataMember]
        public Int32? BolsaFamiliaNumeroFamilias { get; set; }
        [DataMember]
        public Decimal? BolsaFamiliaRepasseMensal { get; set; }
        public Decimal? BolsaFamiliaRepasseAnual { get { return BolsaFamiliaRepasseMensal.HasValue ? BolsaFamiliaRepasseMensal.Value * 12 : new Nullable<Decimal>(); } }

        [DataMember]
        public Int32? PETINumeroBeneficiarios { get; set; }
        [DataMember]
        public Decimal? PETIPrevisaoMensal { get; set; }
        public Decimal? PETIPrevisaoAnual { get { return PETIPrevisaoMensal.HasValue ? PETIPrevisaoMensal.Value * 12 : new Nullable<Decimal>(); } }

        [DataMember]
        public Int32? AcaoRendaMeta { get; set; }
        public Decimal? AcaoRendaPrevisaoAnual { get { return AcaoRendaMeta.HasValue ? AcaoRendaMeta.Value * 80 * 12 : new Nullable<Decimal>(); } }

        [DataMember]
        public Int32? MunicipaisNumeroBeneficiarios { get; set; }
        [DataMember]
        public Decimal? MunicipaisRepasse { get; set; }
        public Decimal? MunicipaisRepasseAnual { get { return MunicipaisRepasse.HasValue ? MunicipaisRepasse.Value * 12 : new Nullable<Decimal>(); } }

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
        public Boolean? SaoPauloSolidarioValorFEASRetidoFMAS2013 { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioMesRepasseFEASBuscaAtiva { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioAnoRepasseFEASBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFEASBuscaAtiva { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFNASBuscaAtiva { get; set; }
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
        public Decimal? SaoPauloSolidarioValorFMASAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorOrcamentoMunicipalAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorFNASAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorIGDPBFAgendaFamilia { get; set; }
        [DataMember]
        public Decimal? SaoPauloSolidarioValorIGDSUASAgendaFamilia { get; set; }

        [DataMember]
        public Int32? SaoPauloSolidarioNumeroFamiliasAgendaFamilia2012 { get; set; }
        [DataMember]
        public Int32? SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013 { get; set; }
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
        public Boolean PossuiParceriaFormal { get; set; }

        [DataMember]
        public List<TransferenciaRendaParceriaInfo> Parcerias { get; set; }

        [DataMember]
        public List<TransferenciaRendaTecnicoReferenciaInfo> TecnicoReferencia { get; set; }

        [DataMember]
        public List<AcaoSocioAssistencialComplementarInfo> AcoesSocioAssistenciaisComplementares { get; set; }

        //PMAS 2016
        [DataMember]
        public Int32? PETINumeroTrabalhoInfantilCadUnico { get; set; }
        [DataMember]
        public Boolean? PETIBeneficiarioTransferenciaRenda { get; set; }
        [DataMember]
        public Boolean? PETIBeneficiarioBolsaFamilia { get; set; }
        [DataMember]
        public Int32? PETINumeroBeneficiarioBolsaFamilia { get; set; }
        [DataMember]
        public Boolean? PETIBeneficiarioPETIPuro { get; set; }
        [DataMember]
        public Int32? PETINumeroBeneficiarioPETIPuroUrbano { get; set; }
        [DataMember]
        public Int32? PETINumeroBeneficiarioPETIPuroRural { get; set; }
        [DataMember]
        public Boolean? PETIBeneficiarioProgramaMunicipal { get; set; }
        [DataMember]
        public Int32? PETINumeroBeneficiarioProgramaMunicipal { get; set; }
        [DataMember]
        public Boolean? PETIAderiuCofinanciamentoFederal { get; set; }
        [DataMember]
        public DateTime? PETIDataAdesao { get; set; }
        [DataMember]
        public Boolean? PETIAcoesTrabalhoInfantil { get; set; }
        [DataMember]
        public List<PETIAcaoInfo> AcoesPETI { get; set; }

        [DataMember]
        public Decimal? ValorFMAS { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoMunicipal { get; set; }
        [DataMember]
        public Decimal? ValorFundoMunicipal { get; set; }
        [DataMember]
        public Decimal? ValorFEAS { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoEstadual { get; set; }
        [DataMember]
        public Decimal? ValorFundoEstadual { get; set; }
        [DataMember]
        public Decimal? ValorFNAS { get; set; }
        [DataMember]
        public Decimal? ValorOrcamentoFederal { get; set; }
        [DataMember]
        public Decimal? ValorFundoFederal { get; set; }
        [DataMember]
        public Decimal? ValorIGDPBF { get; set; }
        [DataMember]
        public Decimal? ValorIGDSUAS { get; set; }

        public Decimal? ValorAEPETI { get; set; }

        public Decimal? ValorAEPETI2 { get; set; }

        public Boolean? AderiuBPCNaEscola { get; set; }

        public DateTime? DataAdesaoBPCNaEscola { get; set; }

        public Int32? NumeroBeneficiariosBPCNaEscola { get; set; }

        public Boolean Ativo { get; set; }

        public Boolean? ExecutaPrograma { get; set; }

        public DateTime? DataAdesaoPrograma { get; set; }

        public TransferenciaRendaPrevisaoAnualInfo TransferenciaRendaPrevisaoAnual { get; set; }


        public Boolean NaoPossuiTecnicoAcao { get; set; }


        public Boolean? NaoPossuiTecnicoReferencia { get; set; }


        public PETIIndicadoresInfo PetiIndicadores { get; set; }

        public TransferenciaRendaGestorAcaoInfo GestorAcao { get; set; }

        public List<ServicoRecursoFinanceiroTransferenciaRendaInfo> ServicosRecursosFinanceirosTransferenciaRenda { get; set; }

        [DataMember]
        public String NomeTecnico { get; set; }
        
        [DataMember]
        public Boolean NaoHaTecnico { get; set; }
        
        [DataMember]
        public String Telefone { get; set; }
        
        [DataMember]
        public String Celular { get; set; }
        
        [DataMember]
        public String Email { get; set; }

    }
}
