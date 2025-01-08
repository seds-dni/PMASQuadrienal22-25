using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios
{
    /// <summary>
    /// [VW_SERVICOS_ESTADUALIZADOS]
    /// </summary>
    [DataContract]
    public class ServicoEstadualizadoInfo
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
        public Int32 CodigoUnidade { get; set; }
        [DataMember]
        public Int32 IdTipoUnidade { get; set; }
        [DataMember]
        public String TipoUnidade { get; set; }
        [DataMember]
        public String UnidadeResponsavel { get; set; }
        [DataMember]
        public Int32 IdLocal { get; set; }
        [DataMember]
        public String LocalExecucao { get; set; }
        [DataMember]
        public String Endereco { get; set; }
        [DataMember]
        public String Bairro { get; set; }
        [DataMember]
        public String Telefone { get; set; }
        [DataMember]
        public String ProtecaoSocial { get; set; }
        [DataMember]
        public String TipoServico { get; set; }
        [DataMember]
        public String Usuarios { get; set; }
        [DataMember]
        public Int32 NumeroMensalAtendidos { get; set; }
        [DataMember]
        public Int32 NumeroAnualAtendidos { get; set; }

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
        public String DataFuncionamentoServico { get; set; }
        [DataMember]
        public String DataDesativacao { get; set; }
        [DataMember]
        public Int32 IdCaracteristicasTerritorio { get; set; }
        [DataMember]
        public String CaracteristicasTerritorio { get; set; }
        [DataMember]
        public Int32 TotalTrabalhadores { get; set; }


        [DataMember]
        public Decimal ValorFMAS { get; set; }
        [DataMember]
        public Decimal ValorFEAS { get; set; }
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
        [DataMember]
        public Decimal ValorFonteRecurso { get; set; }

        [DataMember]
        public Decimal ValorFMI { get; set; }
        [DataMember]
        public Decimal ValorFEI { get; set; }
        [DataMember]
        public Decimal ValorFNI { get; set; }

        public Decimal Total { get { return ValorFMAS + ValorFEAS + ValorFNAS + ValorFMDCA + ValorFEDCA + ValorFNDCA + ValorPrivado + ValorEstadualizado + ValorFMI + ValorFEI + ValorFNI; } }
    }
}

