using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public class CentroReferenciaInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public Int32 IdUnidade { get; set; }
        public UnidadePublicaInfo UnidadePublica { get; set; }

        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public bool PossuiCoordenador { get; set; }
        [DataMember]
        public string Coordenador { get; set; }
        [DataMember]
        public Int32? IdFormacaoCoordenador { get; set; }
        public FormacaoInfo Formacao { get; set; }
        [DataMember]
        public String OutraFormacaoCoordenador { get; set; }
        [DataMember]
        public Int32? IdEscolaridadeCoordenador { get; set; }
        public EscolaridadeInfo Escolaridade { get; set; }

        [DataMember]
        public string CEP { get; set; }
        [DataMember]
        public string Logradouro { get; set; }
        [DataMember]
        public string Numero { get; set; }
        [DataMember]
        public string Complemento { get; set; }
        [DataMember]
        public string Bairro { get; set; }
        [DataMember]
        public string Cidade { get; set; }
        [DataMember]
        public string Telefone { get; set; }
        [DataMember]
        public string Celular { get; set; }
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public int? CapacidadeAtendimento { get; set; }
        [DataMember]
        public int? NumeroAtendidos { get; set; }


        public Boolean Desativado { get; set; }

        public Int32? IdMotivoDesativacao { get; set; }

        public Int32? IdMotivoEncerramento { get; set; }

        public DateTime? DataDesativacao { get; set; }

        public String Detalhamento { get; set; }

        public DateTime? DataRegistroLog { get; set; }

        //[DataMember]
        //public int SemEscolaridade { get; set; }
        //[DataMember]
        //public int NivelFundamental { get; set; }
        //[DataMember]
        //public int TotalFuncionariosNivelMedio { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorServicoSocial { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorPsicologia { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorPedagogia { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorSociologia { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorDireito { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorTerapiaOcupacional { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorMusicoterapia { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorAntropologia { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorEconomia { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorEconomiaDomestica { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperior { get; set; }
        //[DataMember]
        //public int TotalFuncionariosSuperiorPosGraduacao { get; set; }
        [DataMember]
        public int? TotalVoluntarios { get; set; }
        [DataMember]
        public int? TotalEstagiarios { get; set; }
        [DataMember]
        public int? TotalRemunerados { get; set; }

        //[DataMember]
        //public int IdHorasSemana { get; set; }
        //[DataMember]
        //public HorasSemanaInfo HorasSemana { get; set; }

        //[DataMember]
        //public int QuantidadeDiasSemana { get; set; }
        [DataMember]
        public Int16? IdTipoImovel { get; set; }
        [DataMember]
        public TipoImovelInfo TipoImovel { get; set; }

        [DataMember]
        public List<AcaoSocioAssistencialInfo> AcoesSocioAssistenciais { get; set; }

        //PMAS 2016
        [DataMember]
        public DateTime? DataImplantacao { get; set; }

        [DataMember]
        public Int32? IdDistritoSaoPaulo { get; set; }

       
        [DataMember]
        public Int32? IdAvaliacaoLocalExecucao { get; set; }
        public AvaliacaoLocalExecucaoInfo AvaliacaoLocalExecucao { get; set; }
    }
}
