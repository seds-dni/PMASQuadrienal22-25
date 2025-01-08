using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_REDE_SERVICOS_SOCIOASSISTENCIAIS]
    /// </summary>
    [DataContract]
    public class RedeServicoSocioassistencialInfo
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
        public String Porte { get; set; }

        [DataMember]
        public Int32 CodigoUnidade { get; set; }
        [DataMember]
        public Int32 IdTipoUnidade { get; set; }
        [DataMember]
        public String TipoUnidade { get; set; }
        [DataMember]
        public String UnidadeResponsavel { get; set; }
        [DataMember]
        public String IdLocal { get; set; }
        [DataMember]
        public String LocalExecucao { get; set; }
        [DataMember]
        public Int16 IdTipoProtecao { get; set; }
        [DataMember]
        public String ProtecaoSocial { get; set; }
        [DataMember]
        public Int32 IdTipoServico { get; set; }
        [DataMember]
        public String TipoServico { get; set; }
        [DataMember]
        public Int32 IdUsuarioTipoServico { get; set; }
        [DataMember]
        public String Usuarios { get; set; }

        public Int32 IdSituacaoEspecifica { get; set; }

        public String SituacaoEspecifica { get; set; }

        public Int32 IdSituacaoVulnerabilidade { get; set; }

        public String SituacaoVulnerabilidade { get; set; }

        [DataMember]
        public Int32 IdSexo { get; set; }
        [DataMember]
        public String Sexo { get; set; }
        [DataMember]
        public Int32 IdRegiaoMoradia { get; set; }
        [DataMember]
        public String RegiaoMoradia { get; set; }
        [DataMember]
        public Int32 IdCaracteristicasTerritorio { get; set; }
        [DataMember]
        public String CaracteristicasTerritorio { get; set; }
        [DataMember]
        public Decimal ValorFMAS { get; set; }
        [DataMember]
        public Decimal ValorFEAS { get; set; }
        [DataMember]
        public Decimal ValorFEASAnoAnterior { get; set; }
        [DataMember]
        public Decimal ValorFNAS { get; set; }
        [DataMember]
        public Decimal ValorFMDCA { get; set; }
        [DataMember]
        public Decimal ValorFEDCA { get; set; }
        [DataMember]
        public Decimal ValorFNDCA { get; set; }
        [DataMember]
        public Decimal ValorPrivado { get; set; }
        [DataMember]
        public Decimal ValorEstadualizado { get; set; }

        public Decimal Total { get { return ValorFMAS + ValorFEAS + ValorFEASAnoAnterior + ValorFNAS + ValorFMDCA + ValorFEDCA + ValorFNDCA + ValorPrivado + ValorEstadualizado + ValorFMI + ValorFEI + ValorFNI; } }




        [DataMember]
        public Int32 NumeroAtendidosAnual { get; set; }
        [DataMember]
        public Int32 NumeroAtendidosMensal { get; set; }

        [DataMember]
        public Int32 NumeroAtendidosServicoMensal { get; set; }
        [DataMember]
        public Int32 NumeroAtendidosServicoAnual { get; set; }

        public Int32 NumertoTotalAtendidosMensal { get { return NumeroAtendidosMensal + NumeroAtendidosServicoMensal; } }

        public Int32 NumertoTotalAtendidosAnual { get { return NumeroAtendidosAnual + NumeroAtendidosServicoAnual; } }
        [DataMember]
        public Int32 IdAbrangencia { get; set; }
        [DataMember]
        public String Abrangencia { get; set; }

        [DataMember]
        public Int32 TotalTrabalhadoresServico { get; set; }
        [DataMember]
        public Int32 TotalTrabalhadores { get; set; }

        [DataMember]
        public List<String> SituacoesEspecificas { get; set; }
        [DataMember]
        public List<String> SituacoesVulnerabilidade { get; set; }
        [DataMember]
        public Decimal ValorFMI { get; set; }
        [DataMember]
        public Decimal ValorFEI { get; set; }
        [DataMember]
        public Decimal ValorFNI { get; set; }

        [DataMember]
        public Decimal ValorFonteRecurso { get; set; }

        public Int32 IdAtividadeSocioassistencial { get; set; }
        public String AtividadeSocioassistencial { get; set; }
        [DataMember]
        public List<String> AtividadesSocioassistenciais { get; set; }

         
        [DataMember]
        public String CNPJ { get; set; }
        [DataMember]
        public String Cidade { get; set; }

        [DataMember]
        public Int32 IdDistritosSaoPaulo { get; set; }

        [DataMember]
        public String DistritoSaoPaulo { get; set; }

        [DataMember]
        public Boolean? ServicoNaoTipificado { get; set; }

        [DataMember]
        public Int32 MediaMensal2017 { get; set; }
        [DataMember]
        public Int32 MediaMensalPSC2017 { get; set; }
        [DataMember]
        public Int32 MediaTotal2017 { get { return MediaMensal2017 + MediaMensalPSC2017; } }
        [DataMember]
        public Int32 MediaMensal2018 { get; set; }
        [DataMember]
        public Int32 MediaMensalPSC2018 { get; set; }
        [DataMember]
        public Int32 MediaTotal2018 { get { return MediaMensal2018 + MediaMensalPSC2018; } }
        [DataMember]
        public Int32 MediaMensal2019 { get; set; }
        [DataMember]
        public Int32 MediaMensalPSC2019 { get; set; }
        [DataMember]
        public Int32 MediaTotal2019 { get { return MediaMensal2019 + MediaMensalPSC2019; } }
        [DataMember]
        public Int32 MediaMensal2020 { get; set; }
        [DataMember]
        public Int32 MediaMensalPSC2020 { get; set; }
        [DataMember]
        public Int32 MediaTotal2020 { get { return MediaMensal2020 + MediaMensalPSC2020; } }
        [DataMember]
        public Int32 Exercicio { get; set; }
    }
}

