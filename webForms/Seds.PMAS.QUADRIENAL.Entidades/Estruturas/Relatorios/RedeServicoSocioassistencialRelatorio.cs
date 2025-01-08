using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [PR_RELATORIO_REDE_SERVICOS_SOCIOASSISTENCIAIS_SIMPLES]
    /// </summary>
    [DataContract]
    public class RedeServicoSocioassistencialRelatorio
    {
        [DataMember]
        public Int32 IdPrefeitura { get; set; }

        [DataMember]
        public Int32 CodigoUnidade { get; set; }
        [DataMember]
        public Int32 IdTipoUnidade { get; set; }

        [DataMember]
        public String IdLocal { get; set; }


        [DataMember]
        public String Municipio { get; set; }

        [DataMember]
        public Int32 IdPorte { get; set; }
        [DataMember]
        public String Porte { get; set; }

        [DataMember]
        public Int32 IdDistritosSaoPaulo { get; set; }
        [DataMember]
        public String DistritoSaoPaulo { get; set; }

        [DataMember]
        public String Drads { get; set; }
        [DataMember]
        public Int32 IdDrads { get; set; }
        [DataMember]
        public Int32 IdMunicipio { get; set; }
        [DataMember]
        public Int32 IdRegiaoMetropolitana { get; set; }
        [DataMember]
        public Int32 IdMacroRegiao { get; set; }
        [DataMember]
        public Int32 IdNivelGestao { get; set; }



        [DataMember]
        public String TipoUnidade { get; set; }
        [DataMember]
        public String UnidadeResponsavel { get; set; }

        [DataMember]
        public String LocalExecucao { get; set; }

        [DataMember]
        public Int32 IdTipoProtecao { get; set; }
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

        [DataMember]
        public String DataFuncionamentoServico { get; set; }
        [DataMember]
        public String DataDesativacao { get; set; }
        [DataMember]
        public String Abrangencia { get; set; }

        [DataMember]
        public Int32 CapacidadeMensalAtendimento2022 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimento2023 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimento2024 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimento2025 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimentoLA2022 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimentoLA2023 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimentoLA2024 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimentoLA2025 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimentoPSC2022 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimentoPSC2023 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimentoPSC2024 { get; set; }
        [DataMember]
        public Int32 CapacidadeMensalAtendimentoPSC2025 { get; set; }


        public Int32 CapacidadeMensalAtendimentoTotal2022 { get { return CapacidadeMensalAtendimento2022 + CapacidadeMensalAtendimentoPSC2022 + CapacidadeMensalAtendimentoLA2022; } }
        public Int32 CapacidadeMensalAtendimentoTotal2023 { get { return CapacidadeMensalAtendimento2023 + CapacidadeMensalAtendimentoPSC2023 + CapacidadeMensalAtendimentoLA2023; } }
        public Int32 CapacidadeMensalAtendimentoTotal2024 { get { return CapacidadeMensalAtendimento2024 + CapacidadeMensalAtendimentoPSC2024 + CapacidadeMensalAtendimentoLA2024; } }
        public Int32 CapacidadeMensalAtendimentoTotal2025 { get { return CapacidadeMensalAtendimento2025 + CapacidadeMensalAtendimentoPSC2025 + CapacidadeMensalAtendimentoLA2025; } }


        [DataMember]
        public Int32 MediaMensal2021 { get; set; }
        [DataMember]
        public Int32 MediaMensalPSC2021 { get; set; }
        [DataMember]
        public Int32 MediaMensalLA2021 { get; set; }
        [DataMember]
        public Int32 MediaTotal2021 { get { return MediaMensal2021 + MediaMensalPSC2021 + MediaMensalLA2021; } }
        [DataMember]
        public Int32 MediaMensal2022 { get; set; }
        [DataMember]
        public Int32 MediaMensalPSC2022 { get; set; }
        [DataMember]
        public Int32 MediaMensalLA2022 { get; set; }
        [DataMember]
        public Int32 MediaTotal2022 { get { return MediaMensal2022 + MediaMensalPSC2022 + MediaMensalLA2022; } }
        [DataMember]
        public Int32 MediaMensal2023 { get; set; }
        [DataMember]
        public Int32 MediaMensalPSC2023 { get; set; }
        [DataMember]
        public Int32 MediaMensalLA2023 { get; set; }
        [DataMember]
        public Int32 MediaTotal2023 { get { return MediaMensal2023 + MediaMensalPSC2023 + MediaMensalLA2023; } }
        [DataMember]
        public Int32 MediaMensal2024 { get; set; }
        [DataMember]
        public Int32 MediaMensalPSC2024 { get; set; }
        [DataMember]
        public Int32 MediaMensalLA2024 { get; set; }
        [DataMember]
        public Int32 MediaTotal2024 { get { return MediaMensal2024 + MediaMensalPSC2024 + MediaMensalLA2024; } }


        [DataMember]
        public Int32 IdSexo { get; set; }
        [DataMember]
        public String Sexo { get; set; }

        [DataMember]
        public Int32 IdRegiaoMoradia { get; set; }
        [DataMember]
        public String RegiaoMoradia { get; set; }

        [DataMember]
        public Int32 TotalTrabalhadores { get; set; }

        [DataMember]
        public Int32 IdCaracteristicasTerritorio { get; set; }
        [DataMember]
        public String CaracteristicasTerritorio { get; set; }

        [DataMember]
        public Decimal ValorFMAS { get; set; }

        [DataMember]
        public Decimal ValorFMDCA { get; set; }

        [DataMember]
        public Decimal ValorFMI { get; set; }

        [DataMember]
        public Decimal ValorFEAS { get; set; }

        [DataMember]
        public Decimal ValorFEASAnoAnterior { get; set; }

        [DataMember]
        public Decimal DemandasParlamentares { get; set; }

        [DataMember]
        public Decimal DemandasParlamentaresReprogramacao { get; set; }

        [DataMember]
        public Decimal ValorFEDCA { get; set; }

        [DataMember]
        public Decimal ValorFEI { get; set; }

        [DataMember]
        public Decimal ValorFNAS { get; set; }


        [DataMember]
        public Decimal ValorFNDCA { get; set; }

        [DataMember]
        public Decimal ValorFNI { get; set; }

        [DataMember]
        public Decimal ValorPrivado { get; set; }

        [DataMember]
        public Decimal ValorEstadualizado { get; set; }

        [DataMember]
        public Decimal ValorFonteRecurso { get; set; }


        public Decimal Total { get { return ValorFMAS + ValorFEAS + ValorFEASAnoAnterior + DemandasParlamentares + DemandasParlamentaresReprogramacao + ValorFNAS + ValorFMDCA + ValorFEDCA + ValorFNDCA + ValorPrivado + ValorEstadualizado + ValorFMI + ValorFEI + ValorFNI + ValorFonteRecurso; } }


        [DataMember]
        public Int32 NumeroAtendidosAnual { get; set; }

        [DataMember]
        public Int32 NumeroAtendidosServicoMensal { get; set; }
        [DataMember]
        public Int32 NumeroAtendidosServicoAnual { get; set; }

        public Int32 NumertoTotalAtendidosMensal { get { return CapacidadeMensalAtendimento2022 + CapacidadeMensalAtendimento2023 + CapacidadeMensalAtendimento2024 + CapacidadeMensalAtendimento2025 + NumeroAtendidosServicoMensal; } }

        public Int32 NumertoTotalAtendidosAnual { get { return NumeroAtendidosAnual + NumeroAtendidosServicoAnual; } }

        [DataMember]
        public Int32 IdAbrangencia { get; set; }


        [DataMember]
        public Int32 TotalTrabalhadoresServico { get; set; }

        [DataMember]
        public String CNPJ { get; set; }
        [DataMember]
        public String Cidade { get; set; }

        //Welington P.

        [DataMember]
        public Boolean? ServicoNaoTipificado { get; set; }

        [DataMember]
        public int? Exercicio { get; set; }
    }
}

