using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_RELATORIO_TRANSFERENCIA_RENDA]
    /// </summary>
    [DataContract]
    public class ResumoTransferenciaRendaInfo
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
        public Int32 AcaoJovemMeta { get; set; }
        [DataMember]
        public Decimal AcaoJovemRepasse { get; set; }
        [DataMember]
        public String PossuiParceriaAcaoJovem { get; set; }
        [DataMember]
        public String PossuiIntegracaoAcaoJovem { get; set; }
        [DataMember]
        public Int32 AcaoJovemTotalParcerias { get; set; }
        [DataMember]
        public Int32 AcaoJovemTotalServicosAssociados { get; set; }
        [DataMember]
        public Int32 RendaCidadaMeta { get; set; }
        [DataMember]
        public Decimal RendaCidadaRepasse { get; set; }
        [DataMember]
        public String PossuiParceriaRendaCidada { get; set; }
        [DataMember]
        public String PossuiIntegracaoRendaCidada { get; set; }
        [DataMember]
        public Int32 RendaCidadaTotalParcerias { get; set; }
        [DataMember]
        public Int32 RendaCidadaTotalServicosAssociados { get; set; }
        [DataMember]
        public Int32 SaoPauloSolidarioMeta { get; set; }
        [DataMember]
        public Decimal SaoPauloSolidarioRepasse { get; set; }
        [DataMember]
        public String PossuiParceriaSaoPauloSolidario { get; set; }
        [DataMember]
        public String PossuiIntegracaoSaoPauloSolidario { get; set; }
        [DataMember]
        public Int64 BPCIdososBeneficiarios { get; set; }
        [DataMember]
        public Decimal BPCIdososRepasse { get; set; }
        [DataMember]
        public String PossuiParceriaBPCIdosos { get; set; }
        [DataMember]
        public String PossuiIntegracaoBPCIdosos { get; set; }
        [DataMember]
        public Int32 BPCIdososTotalParcerias { get; set; }
        [DataMember]
        public Int32 BPCIdososTotalServicosAssociados { get; set; }
        [DataMember]
        public Int64 BPCPCDBeneficiarios { get; set; }
        [DataMember]
        public Decimal BPCPCDRepasse { get; set; }
        [DataMember]
        public String PossuiParceriaBPCPCD { get; set; }
        [DataMember]
        public String PossuiIntegracaoBPCPCD { get; set; }
        [DataMember]
        public Int32 BPCPCDTotalParcerias { get; set; }
        [DataMember]
        public Int32 BPCPCDTotalServicosAssociados { get; set; }
        [DataMember]
        public Int32 BolsaFamiliaBeneficiarios { get; set; }
        [DataMember]
        public Decimal BolsaFamiliaRepasse { get; set; }
        [DataMember]
        public String PossuiParceriaBolsaFamilia { get; set; }
        [DataMember]
        public String PossuiIntegracaoBolsaFamilia { get; set; }
        [DataMember]
        public Int32 BolsaFamiliaTotalParcerias { get; set; }
        [DataMember]
        public Int32 BolsaFamiliaTotalServicosAssociados { get; set; }
        [DataMember]
        public Int32 PETIBeneficiarios { get; set; }
        [DataMember]
        public Decimal PETIRepasse { get; set; }
        [DataMember]
        public String PossuiParceriaPETI { get; set; }
        [DataMember]
        public String PossuiIntegracaoPETI { get; set; }
        [DataMember]
        public Int32 PETITotalParcerias { get; set; }
        [DataMember]
        public Int32 PETITotalServicosAssociados { get; set; }
        [DataMember]
        public Int32 MunicipaisBeneficiarios { get; set; }
        [DataMember]
        public Decimal MunicipaisRepasse { get; set; }

        public Decimal TotalRepasse { get { return AcaoJovemRepasse + RendaCidadaRepasse + RendaCidadaIdosoRepasse + BolsaFamiliaRepasse + PETIRepasse + MunicipaisRepasse; } }
        public Decimal TotalRepasseBPC { get { return BPCIdososRepasse + BPCPCDRepasse; } }

        [DataMember]
        public Int32 RendaCidadaIdosoMeta { get; set; }
        [DataMember]
        public Decimal RendaCidadaIdosoRepasse { get; set; }
        [DataMember]
        public String PossuiParceriaRendaCidadaIdoso { get; set; }
        [DataMember]
        public String PossuiIntegracaoRendaCidadaIdoso { get; set; }
        [DataMember]
        public Int32 RendaCidadaIdosoTotalParcerias { get; set; }
        [DataMember]
        public Int32 RendaCidadaIdosoTotalServicosAssociados { get; set; }
    }
}

