using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    [DataContract]
    public abstract class LocalExecucaoInfo
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Nome { get; set; }
        [DataMember]
        public Int32 IdUnidade { get; set; }
        [DataMember]
        public bool PossuiTecnicoResponsavel { get; set; }
        [DataMember]
        public string TecnicoResponsavel { get; set; }
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
        public int CapacidadeAtendimento { get; set; }
        [DataMember]
        public int NumeroAtendidos { get; set; }

        [DataMember]
        public int TotalFuncionariosSemEscolaridade { get; set; }
        [DataMember]
        public int TotalFuncionariosNivelFundamental { get; set; }
        [DataMember]
        public int TotalFuncionariosNivelMedio { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorServicoSocial { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorPsicologia { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorPedagogia { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorSociologia { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorDireito { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorTerapiaOcupacional { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorMusicoterapia { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorAntropologia { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorEconomia { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorEconomiaDomestica { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperior { get; set; }
        [DataMember]
        public int TotalFuncionariosSuperiorPosGraduacao { get; set; }
        [DataMember]
        public int TotalEstagiarios { get; set; }
        [DataMember]
        public int TotalVoluntarios { get; set; }

        [DataMember]
        public int IdHorasSemana { get; set; }
        [DataMember]
        public HorasSemanaInfo HorasSemana { get; set; }

        [DataMember]
        public int QuantidadeDiasSemana { get; set; }
        [DataMember]
        public Int16? IdTipoImovel { get; set; }
        [DataMember]
        public TipoImovelInfo TipoImovel { get; set; }

        [DataMember]
        public Boolean? DemandaAtendimento { get; set; }
        [DataMember]
        public Boolean? MelhoriaAspecto { get; set; }
        [DataMember]
        public Boolean? NecessitaReforma { get; set; }

        public Boolean Desativado { get; set; }

        public Int32? IdMotivoDesativacao { get; set; }

        public Int32? IdMotivoEncerramento { get; set; }

        public DateTime? DataDesativacao { get; set; }

        public String Detalhamento { get; set; }

        public DateTime? DataRegistroLog { get; set; }

        //Welington P.
        //Tipo de Oferta de atendimento oferecido pelo local de Execução
        // 1 - Oferta de Serviço SocioAssistenciais 
        // 3 - Bom Prato ou Viva Leite
        [DataMember]
        public Int32? TipoOfertaAtendimento { get; set; }

        //PMAS 2016
        [DataMember]
        public Int32? IdDistritoSaoPaulo { get; set; }

        [DataMember]
        public Int32? IdAvaliacaoLocalExecucao { get; set; }
        public AvaliacaoLocalExecucaoInfo AvaliacaoLocalExecucao { get; set; }
    }
}
