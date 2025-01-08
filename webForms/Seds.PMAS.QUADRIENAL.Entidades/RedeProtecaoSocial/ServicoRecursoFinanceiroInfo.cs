using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public abstract class ServicoRecursoFinanceiroInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdUsuarioTipoServico { get; set; }
        [DataMember]
        public UsuarioTipoServicoInfo UsuarioTipoServico { get; set; }

        [DataMember]
        public Int32 PrevisaoAnualNumeroAtendidos { get; set; }
        [DataMember]
        public Int32 PrevisaoMensalNumeroAtendidos { get; set; }

        [DataMember]
        public Int32 IdAbrangenciaServico { get; set; }
        [DataMember]
        public AbrangenciaServicoInfo Abrangencia { get; set; }

        [DataMember]
        public String DescricaoServicoNaoTipificado { get; set; }
        [DataMember]
        public String ObjetivoServicoNaoTipificado { get; set; }

        [DataMember]
        public Int32 TotalFuncionarios { get; set; }

        [DataMember]
        public Int32? IdRegiaoMoradia { get; set; }
        public RegiaoMoradiaInfo RegiaoMoradia { get; set; }

        [DataMember]
        public Int32 IdCaracteristicasTerritorio { get; set; }
        public CaracteristicasTerritorioInfo CaracteristicasTerritorio { get; set; }

        [DataMember]
        public Int32? IdSexo { get; set; }
        public SexoInfo Sexo { get; set; }

        [DataMember]
        public List<SituacaoEspecificaInfo> SituacoesEspecificas { get; set; }
        [DataMember]
        public List<AtividadeSocioAssistencialInfo> AtividadesSocioAssistenciais { get; set; }

        [DataMember]
        public Boolean? PossuiTecnicoResponsavel { get; set; }
        [DataMember]
        public String NomeTecnicoResponsavel { get; set; }

        [DataMember]
        public Int32? IdTipoServicoNaoTipificado { get; set; }
        [DataMember]
        public Int32? NumeroAtendidosCentroMensal { get; set; }
        [DataMember]
        public Int32? NumeroAtendidosServicoAnual { get; set; }
        [DataMember]
        public Int32? TipoLocal { get; set; }
        [DataMember]
        public Int32? NumeroAtendidosServico { get; set; }
        [DataMember]
        public Int32? NumeroAtendidosMensalServico { get; set; }


        [DataMember]
        public Int32? MediaMensalAtendimento2017 { get; set; }
        [DataMember]
        public Int32? MediaMensalAtendimento2018 { get; set; }
        [DataMember]
        public Int32? MediaMensalAtendimento2019 { get; set; }
        [DataMember]
        public Int32? MediaMensalAtendimento2020 { get; set; }



        public Int32? MediaMensalAtendimentoPSC2017 { get; set; }
        [DataMember]
        public Int32? MediaMensalAtendimentoPSC2018 { get; set; }
        [DataMember]
        public Int32? MediaMensalAtendimentoPSC2019 { get; set; }
        [DataMember]
        public Int32? MediaMensalAtendimentoPSC2020 { get; set; }

        [DataMember]
        public DateTime? DataFuncionamentoServico { get; set; }
        [DataMember]
        public Boolean? AtendeDependentes { get; set; }
        [DataMember]
        public Boolean? AtendeProgramaRecomeco { get; set; }

        [DataMember]
        public int? IdAvaliacaoServico { get; set; }

        [DataMember]
        public int? IdHorasSemana { get; set; }
        [DataMember]
        public HorasSemanaInfo HorasSemana { get; set; }

        [DataMember]
        public int? QuantidadeDiasSemana { get; set; }

        [DataMember]
        public Boolean? MunicipioSedeServico { get; set; }

        [DataMember]
        public Int32? IdFormaJuridica { get; set; }

        [DataMember]
        public String IndicaMunicipiosParticipamOfertaServico { get; set; }

        [DataMember]
        public String IndicaMunicipiosSedeServico { get; set; }

        public Boolean Desativado { get; set; }

        public int? IdMotivoDesativacao { get; set; }

        public DateTime? DataDesativacao { get; set; }

        public String Detalhamento { get; set; }

        public DateTime? DataRegistroLog { get; set; }

        [DataMember]
        public Int32 IdCaracteristicaOfertaServico { get; set; }

        [DataMember]
        public Boolean? AtendeCriancasAuxilioReclusao { get; set; }
        
        [DataMember]
        public int CriancaAuxilioReclusaoFeitos { get; set; }
        
        [DataMember]
        public int CriancaAuxilioReclusaoAprovados { get; set; }
        
        [DataMember]
        public int CriancaAuxilioReclusaoNegado { get; set; }
        
        [DataMember]
        public Boolean? AtendeCriancasPensaoMorte { get; set; }

        [DataMember]
        public int CriancaPensaoMorteFeitos { get; set; }

        [DataMember]
        public int CriancaPensaoMorteAprovados { get; set; }

        [DataMember]
        public int CriancaPensaoMorteNegado { get; set; }


        [DataMember]
        public Boolean? AtendeCriancasAuxilioReclusaoExercicio2025 { get; set; }

        [DataMember]
        public int CriancaAuxilioReclusaoFeitosExercicio2025 { get; set; }

        [DataMember]
        public int CriancaAuxilioReclusaoAprovadosExercicio2025 { get; set; }

        [DataMember]
        public int CriancaAuxilioReclusaoNegadoExercicio2025 { get; set; }


        [DataMember]
        public Boolean? AtendeCriancasPensaoMorteExercicio2025 { get; set; }

        [DataMember]
        public int CriancaPensaoMorteFeitosExercicio2025 { get; set; }

        [DataMember]
        public int CriancaPensaoMorteAprovadosExercicio2025 { get; set; }

        [DataMember]
        public int CriancaPensaoMorteNegadoExercicio2025 { get; set; }

    }
}
