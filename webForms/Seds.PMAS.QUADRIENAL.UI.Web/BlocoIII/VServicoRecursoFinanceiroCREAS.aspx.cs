using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using System.Web.UI.HtmlControls;
using Seds.PMAS.QUADRIENAL.Resource;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class VServicoRecursoFinanceiroCREAS : System.Web.UI.Page
    {
        #region Propriedades
        private List<int> CREASExercicios = new List<int> { 2022, 2023, 2024, 2025 }; 
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                if (String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx");
                    return;
                }

                using (var proxyEstrutura = new ProxyEstruturaAssistenciaSocial())
                {
                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {

                        using (var proxyProgramas = new ProxyProgramas())
                        {
                            load(proxy, proxyEstrutura, proxyProgramas);
                        }
                    }
                }
            }
        }

        void load(ProxyRedeProtecaoSocial proxy, ProxyEstruturaAssistenciaSocial proxyEstrutura, ProxyProgramas proxyProgramas)
        {
            int idServico = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

            var servico = proxy.Service.GetServicoRecursoFinanceiroCREASById(idServico);
            if (servico == null)
                return;

            lblTipoProtecao.Text = servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial == 1 ? "Básica" : servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial == 2 ? "Média Complexidade" : "Alta Complexidade";
            //TODO:DBM - /ANO
            //lblFeasAnoAnterior.Text = entidade.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
            //SERVIÇO NÃO TIPIFICADO
            if (servico.IdTipoServicoNaoTipificado != null)
            {
                lblTipoServico.Text += "Serviço não tipificado pela Resolução n° 109 do CNAS, de 11/11/2009 - ";
                if (servico.UsuarioTipoServico.IdTipoServico == 153 || servico.UsuarioTipoServico.IdTipoServico == 156 || servico.UsuarioTipoServico.IdTipoServico == 159)
                {
                    tbNaoTipificado.Visible = true;
                    lblTipoServico.Text += servico.UsuarioTipoServico.TipoServico.Nome;
                    lblNaoTipificado.Text = servico.DescricaoServicoNaoTipificado;
                    lblObjetivoNaoTipificado.Text = servico.ObjetivoServicoNaoTipificado;
                }
                else
                {
                    lblTipoServico.Text += servico.UsuarioTipoServico.TipoServico.Nome;
                }
            }
            else
            {
                lblTipoServico.Text = servico.UsuarioTipoServico.TipoServico.Nome;
            }

            lblPublicoAlvo.Text = servico.UsuarioTipoServico.Nome;

            if (servico.SituacoesEspecificas != null && servico.SituacoesEspecificas.Count > 0)
            {
                lblSituacoesEspecificas.Text = Util.Concat(servico.SituacoesEspecificas.Select(s => s.Nome).ToList(), "<br/>");
            }

            if (servico.AtividadesSocioAssistenciais != null && servico.AtividadesSocioAssistenciais.Count > 0)
            {
                lblAtividades.Text = Util.Concat(servico.AtividadesSocioAssistenciais.Select(s => s.Nome).ToList(), "<br/>");
            }


            var abrangencias = proxyEstrutura.Service.GetAbrangenciasServico();
            lblAbrangencia.Text = abrangencias.First(a => a.Id == servico.IdAbrangenciaServico).Abreviacao;

            if (servico.IdAbrangenciaServico == 4)
            {
                trAbrangencia.Visible = true;
                trOfertaOuSede.Visible = true;

                if (servico.MunicipioSedeServico == true)
                {
                    lblSedeServico.Text = "Sim, municipio é sede do serviço.";
                    lblOfertaOuSede.Text = "Municípios que participam da oferta do serviço :";
                    txtOfertaOuSede.Text = servico.IndicaMunicipiosParticipamOfertaServico != String.Empty ? servico.IndicaMunicipiosParticipamOfertaServico : "Não há municípios cadastrados.";
                }
                else
                {
                    lblSedeServico.Text = "Municipio não é sede do serviço.";
                    lblOfertaOuSede.Text = "Municípios indicados como Sede do serviço :";
                    txtOfertaOuSede.Text = servico.IndicaMunicipiosSedeServico != String.Empty ? servico.IndicaMunicipiosSedeServico : "Não há municípios cadastrados.";
                }


                var juridica = proxyEstrutura.Service.GetFormaJuridica();

                if (servico.IdFormaJuridica == 1)
                {
                    var consorcio = proxyEstrutura.Service.GetConsorcioCREAS(servico.Id);

                    lblJuridica.Text = juridica.First(a => a.Id == servico.IdFormaJuridica).NomeForma;

                    trTituloFormaJuridica.Visible = true;
                    trJuridica.Visible = true;

                    if (consorcio != null)
                    {
                        trJuridica.Visible = true;
                        lblNomeDoConsorcio.Text = "Nome Do Consórcio : " + consorcio.NomeConsorcio;
                        lblCNPJ.Text = "CNPJ : " + consorcio.CNPJ;
                        lblMunicipioSede.Text = "Município sede : " + consorcio.MunicipioSede;
                        txtMunicipiosEnvolvidos.Text = consorcio.MunicipioEnvolvido;
                    }
                }
                else if (servico.IdFormaJuridica != 0)
                {
                    lblJuridica.Text = juridica.First(a => a.Id == servico.IdFormaJuridica).NomeForma;
                    trTituloFormaJuridica.Visible = true;
                    trJuridica.Visible = false;
                }
            }
            else
            {
                trTituloFormaJuridica.Visible = false;
                trJuridica.Visible = false;
                trAbrangencia.Visible = false;
                trOfertaOuSede.Visible = false;
            }


            switch (servico.IdCaracteristicasTerritorio)
            {
                case 1: lblCaracteristicasTerritorio.Text = "Ciganos"; break;
                case 2: lblCaracteristicasTerritorio.Text = "Extrativistas"; break;
                case 3: lblCaracteristicasTerritorio.Text = "Pescadores artesanais"; break;
                case 4: lblCaracteristicasTerritorio.Text = "Comunidade tradicional de matriz africana"; break;
                case 5: lblCaracteristicasTerritorio.Text = "Comunidade ribeirinha"; break;
                case 6: lblCaracteristicasTerritorio.Text = "Indígenas"; break;
                case 7: lblCaracteristicasTerritorio.Text = "Quilombolas"; break;
                case 8: lblCaracteristicasTerritorio.Text = "Agricultores familiares"; break;
                case 9: lblCaracteristicasTerritorio.Text = "Acampamentos"; break;
                case 10: lblCaracteristicasTerritorio.Text = "População flutuante decorrente de instalação prisional"; break;
                case 11: lblCaracteristicasTerritorio.Text = "Trabalhadores sazonais"; break;
                case 12: lblCaracteristicasTerritorio.Text = "Aglomerados subnormais"; break;
                case 13: lblCaracteristicasTerritorio.Text = "Assentamentos"; break;
                case 14: lblCaracteristicasTerritorio.Text = "Nenhuma das condições socioterritoriais"; break;
                case 15: lblCaracteristicasTerritorio.Text = "População em situação de rua"; break;
                case 16: lblCaracteristicasTerritorio.Text = "Pessoas com deficiência"; break;
            }
            if (servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial == 2)
            {
                switch (servico.IdCaracteristicaOfertaServico)
                {

                    case 1: lblOfertaServico.Text = "Nenhuma das alternativas"; break;
                    case 2: lblOfertaServico.Text = "CDPCD – Centro Dia para Pessoas com Deficiência"; break;
                    case 3: lblOfertaServico.Text = "CDI – Centro Dia Idoso"; break;
                    case 4: lblOfertaServico.Text = "CDIPCD – Centro Dia para Idosos e Pessoas com Deficiência"; break;
                    case 5: lblOfertaServico.Text = "Domicílio"; break;

                }
            }
            else
            {
                tdServicoOfertado.Visible = false;
            }

            switch (servico.IdRegiaoMoradia)
            {
                case 1: lblMoradiaUsuarios.Text = "Zona Urbana"; break;
                case 2: lblMoradiaUsuarios.Text = "Zona Rural"; break;
                case 3: lblMoradiaUsuarios.Text = "Zona Urbana e Rural"; break;
            }
            switch (servico.IdSexo)
            {
                case 1: lblSexo.Text = "Feminino"; break;
                case 2: lblSexo.Text = "Masculino"; break;
                case 3: lblSexo.Text = "Ambos os sexos"; break;
            }

            #region Exibir: Funcionamento

            #region Capacidade
            var servicosRecursosFinanceiroCREASCapacidadeExercicio1 = servico.ServicosRecursosFinanceiroCREASCapacidade.Where(x => x.Exercicio == this.CREASExercicios[0]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASCapacidadeLAExercicio1 = servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Where(x => x.Exercicio == this.CREASExercicios[0]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASCapacidadePSCExercicio1 = servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Where(x => x.Exercicio == this.CREASExercicios[0]).FirstOrDefault();

            var servicosRecursosFinanceiroCREASCapacidadeExercicio2 = servico.ServicosRecursosFinanceiroCREASCapacidade.Where(x => x.Exercicio == this.CREASExercicios[1]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASCapacidadeLAExercicio2 = servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Where(x => x.Exercicio == this.CREASExercicios[1]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASCapacidadePSCExercicio2 = servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Where(x => x.Exercicio == this.CREASExercicios[1]).FirstOrDefault();

            var servicosRecursosFinanceiroCREASCapacidadeExercicio3 = servico.ServicosRecursosFinanceiroCREASCapacidade.Where(x => x.Exercicio == this.CREASExercicios[2]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASCapacidadeLAExercicio3 = servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Where(x => x.Exercicio == this.CREASExercicios[2]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASCapacidadePSCExercicio3 = servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Where(x => x.Exercicio == this.CREASExercicios[2]).FirstOrDefault();

            var servicosRecursosFinanceiroCREASCapacidadeExercicio4 = servico.ServicosRecursosFinanceiroCREASCapacidade.Where(x => x.Exercicio == this.CREASExercicios[3]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASCapacidadeLAExercicio4 = servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Where(x => x.Exercicio == this.CREASExercicios[3]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASCapacidadePSCExercicio4 = servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Where(x => x.Exercicio == this.CREASExercicios[3]).FirstOrDefault();
            #endregion

            #region Media Mensal
            var servicosRecursosFinanceiroCREASMediaMensalExercicio1 = servico.ServicosRecursosFinanceiroCREASMediaMensal.Where(x => x.Exercicio == this.CREASExercicios[0]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASMediaMensalLAExercicio1 = servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Where(x => x.Exercicio == this.CREASExercicios[0]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASMediaMensalPSCExercicio1 = servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Where(x => x.Exercicio == this.CREASExercicios[0]).FirstOrDefault();

            var servicosRecursosFinanceiroCREASMediaMensalExercicio2 = servico.ServicosRecursosFinanceiroCREASMediaMensal.Where(x => x.Exercicio == this.CREASExercicios[1]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASMediaMensalLAExercicio2 = servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Where(x => x.Exercicio == this.CREASExercicios[1]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASMediaMensalPSCExercicio2 = servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Where(x => x.Exercicio == this.CREASExercicios[1]).FirstOrDefault();

            var servicosRecursosFinanceiroCREASMediaMensalExercicio3 = servico.ServicosRecursosFinanceiroCREASMediaMensal.Where(x => x.Exercicio == this.CREASExercicios[2]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASMediaMensalLAExercicio3 = servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Where(x => x.Exercicio == this.CREASExercicios[2]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASMediaMensalPSCExercicio3 = servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Where(x => x.Exercicio == this.CREASExercicios[2]).FirstOrDefault();

            var servicosRecursosFinanceiroCREASMediaMensalExercicio4 = servico.ServicosRecursosFinanceiroCREASMediaMensal.Where(x => x.Exercicio == this.CREASExercicios[3]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASMediaMensalLAExercicio4 = servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Where(x => x.Exercicio == this.CREASExercicios[3]).FirstOrDefault();
            var servicosRecursosFinanceiroCREASMediaMensalPSCExercicio4 = servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Where(x => x.Exercicio == this.CREASExercicios[3]).FirstOrDefault();
            #endregion

            if (servico.UsuarioTipoServico.TipoServico.Id == R_TIPO_SERVICO.SERVICO_PROTECAO_SOCIAL_ADOLESC_CUMPR_MEDIDA_SOCIOEDUCATIVA_LA_PSC)
            {
                layout_capacidade_media_mensal_la_psc.Visible = true;
                layout_capacidade_media_mensal.Visible = false;

                #region Capacidade [LA]
                lblCapacidadeLAExercicio1.Text = servicosRecursosFinanceiroCREASCapacidadeLAExercicio1 != null ? servicosRecursosFinanceiroCREASCapacidadeLAExercicio1.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadeLAExercicio1.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeLAExercicio2.Text = servicosRecursosFinanceiroCREASCapacidadeLAExercicio2 != null ? servicosRecursosFinanceiroCREASCapacidadeLAExercicio2.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadeLAExercicio2.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeLAExercicio3.Text = servicosRecursosFinanceiroCREASCapacidadeLAExercicio3 != null ? servicosRecursosFinanceiroCREASCapacidadeLAExercicio3.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadeLAExercicio3.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeLAExercicio4.Text = servicosRecursosFinanceiroCREASCapacidadeLAExercicio4 != null ? servicosRecursosFinanceiroCREASCapacidadeLAExercicio4.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadeLAExercicio4.Capacidade.Value.ToString() : "0" : "0";

                lblCapacidadePSCExercicio1.Text = servicosRecursosFinanceiroCREASCapacidadePSCExercicio1 != null ? servicosRecursosFinanceiroCREASCapacidadePSCExercicio1.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadePSCExercicio1.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadePSCExercicio2.Text = servicosRecursosFinanceiroCREASCapacidadePSCExercicio2 != null ? servicosRecursosFinanceiroCREASCapacidadePSCExercicio2.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadePSCExercicio2.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadePSCExercicio3.Text = servicosRecursosFinanceiroCREASCapacidadePSCExercicio3 != null ? servicosRecursosFinanceiroCREASCapacidadePSCExercicio3.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadePSCExercicio3.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadePSCExercicio4.Text = servicosRecursosFinanceiroCREASCapacidadePSCExercicio4 != null ? servicosRecursosFinanceiroCREASCapacidadePSCExercicio4.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadePSCExercicio4.Capacidade.Value.ToString() : "0" : "0";
                #endregion

                #region Media Mensal [LA]
                lblMediaMensalLAExercicio1.Text = servicosRecursosFinanceiroCREASMediaMensalLAExercicio1 != null ? servicosRecursosFinanceiroCREASMediaMensalLAExercicio1.MediaMensal != null ? servicosRecursosFinanceiroCREASMediaMensalLAExercicio1.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalLAExercicio2.Text = servicosRecursosFinanceiroCREASMediaMensalLAExercicio2 != null ? servicosRecursosFinanceiroCREASMediaMensalLAExercicio2.MediaMensal != null ? servicosRecursosFinanceiroCREASMediaMensalLAExercicio2.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalLAExercicio3.Text = servicosRecursosFinanceiroCREASMediaMensalLAExercicio3 != null ? servicosRecursosFinanceiroCREASMediaMensalLAExercicio3.MediaMensal != null ? servicosRecursosFinanceiroCREASMediaMensalLAExercicio3.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalLAExercicio4.Text = servicosRecursosFinanceiroCREASMediaMensalLAExercicio4 != null ? servicosRecursosFinanceiroCREASMediaMensalLAExercicio4.MediaMensal != null ? servicosRecursosFinanceiroCREASMediaMensalLAExercicio4.MediaMensal.Value.ToString() : "0" : "0";

                lblMediaMensalPSCExercicio1.Text = servicosRecursosFinanceiroCREASMediaMensalPSCExercicio1 != null ? servicosRecursosFinanceiroCREASMediaMensalPSCExercicio1.MediaMensal !=null ? servicosRecursosFinanceiroCREASMediaMensalPSCExercicio1.MediaMensal.Value.ToString() : "0": "0";
                lblMediaMensalPSCExercicio2.Text = servicosRecursosFinanceiroCREASMediaMensalPSCExercicio2 != null ? servicosRecursosFinanceiroCREASMediaMensalPSCExercicio2.MediaMensal !=null ? servicosRecursosFinanceiroCREASMediaMensalPSCExercicio2.MediaMensal.Value.ToString() : "0": "0";
                lblMediaMensalPSCExercicio3.Text = servicosRecursosFinanceiroCREASMediaMensalPSCExercicio3 != null ? servicosRecursosFinanceiroCREASMediaMensalPSCExercicio3.MediaMensal !=null ? servicosRecursosFinanceiroCREASMediaMensalPSCExercicio3.MediaMensal.Value.ToString() : "0": "0";
                lblMediaMensalPSCExercicio4.Text = servicosRecursosFinanceiroCREASMediaMensalPSCExercicio4 != null ? servicosRecursosFinanceiroCREASMediaMensalPSCExercicio4.MediaMensal !=null ? servicosRecursosFinanceiroCREASMediaMensalPSCExercicio4.MediaMensal.Value.ToString() : "0": "0";
                #endregion
            }
            else
            {
                layout_capacidade_media_mensal.Visible = true;
                layout_capacidade_media_mensal_la_psc.Visible = false;
                #region Capacidade [LA]
                lblCapacidadeExercicio1.Text = servicosRecursosFinanceiroCREASCapacidadeExercicio1 != null ? servicosRecursosFinanceiroCREASCapacidadeExercicio1.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadeExercicio1.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeExercicio2.Text = servicosRecursosFinanceiroCREASCapacidadeExercicio2 != null ? servicosRecursosFinanceiroCREASCapacidadeExercicio2.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadeExercicio2.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeExercicio3.Text = servicosRecursosFinanceiroCREASCapacidadeExercicio3 != null ? servicosRecursosFinanceiroCREASCapacidadeExercicio3.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadeExercicio3.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeExercicio4.Text = servicosRecursosFinanceiroCREASCapacidadeExercicio4 != null ? servicosRecursosFinanceiroCREASCapacidadeExercicio4.Capacidade != null ? servicosRecursosFinanceiroCREASCapacidadeExercicio4.Capacidade.Value.ToString() : "0" : "0";
                #endregion

                #region Media Mensal [LA]
                lblMediaMensalExercicio1.Text = servicosRecursosFinanceiroCREASMediaMensalExercicio1 != null ? servicosRecursosFinanceiroCREASMediaMensalExercicio1.MediaMensal != null ? servicosRecursosFinanceiroCREASMediaMensalExercicio1.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalExercicio2.Text = servicosRecursosFinanceiroCREASMediaMensalExercicio2 != null ? servicosRecursosFinanceiroCREASMediaMensalExercicio2.MediaMensal != null ? servicosRecursosFinanceiroCREASMediaMensalExercicio2.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalExercicio3.Text = servicosRecursosFinanceiroCREASMediaMensalExercicio3 != null ? servicosRecursosFinanceiroCREASMediaMensalExercicio3.MediaMensal != null ? servicosRecursosFinanceiroCREASMediaMensalExercicio3.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalExercicio4.Text = servicosRecursosFinanceiroCREASMediaMensalExercicio4 != null ? servicosRecursosFinanceiroCREASMediaMensalExercicio4.MediaMensal != null ? servicosRecursosFinanceiroCREASMediaMensalExercicio4.MediaMensal.Value.ToString() : "0" : "0";
                #endregion                                                                                                                                                         
            }

            #endregion

            lblDataInicio.Text = servico.DataFuncionamentoServico.HasValue ? servico.DataFuncionamentoServico.Value.ToShortDateString() : String.Empty;
            if (servico.IdHorasSemana.HasValue)
            {
                switch (servico.IdHorasSemana)
                {

                    case 1: lblHorasSemana.Text = "Até 20 horas"; break;
                    case 2: lblHorasSemana.Text = "de 21 a 39 horas"; break;
                    case 3: lblHorasSemana.Text = "40 horas"; break;
                    case 4: lblHorasSemana.Text = "mais de 40 horas"; break;
                    case 5: lblHorasSemana.Text = "ininterrupto (24 horas / 7 dias)"; break;
                }
            }

            lblDiasSemana.Text = servico.QuantidadeDiasSemana.HasValue ? servico.QuantidadeDiasSemana.Value.ToString() + " dia(s)" : String.Empty;

            if (servico.Desativado)
            {
                lblMotivoEncerramento.Text = proxy.Service.GetMotivoDesativacaoServicoById(servico.IdMotivoDesativacao.Value).Descricao;
                if (servico.IdMotivoDesativacao.HasValue)
                {
                    trMotivoEncerramento.Visible = tdEncerramento.Visible = true;
                    switch (servico.IdMotivoDesativacao)
                    {
                        case 1:
                            lblDataEncerramento.Text = servico.DataRegistroLog.Value.ToShortDateString();
                            trDetalhamentoDesativacao.Visible = false;
                            break;
                        case 2:
                            lblDataEncerramento.Text = servico.DataDesativacao.HasValue ? servico.DataDesativacao.Value.ToShortDateString() : servico.DataRegistroLog.Value.ToShortDateString();
                            lblDataEncerramentoServico.Text = "Data do encerramento das atividades deste serviço:";
                            lblDescricaoDetalhamento.Text = "Detalhamento sobre o motivo do encerramento das atividades deste serviço:";
                            lblDetalhamento.Text = servico.Detalhamento;
                            trDetalhamentoDesativacao.Visible = true;
                            break;
                        case 3:
                            lblDataEncerramento.Text = servico.DataDesativacao.HasValue ? servico.DataDesativacao.Value.ToShortDateString() : servico.DataRegistroLog.Value.ToShortDateString();
                            lblDataEncerramentoServico.Text = "Data de vigência das alterações na oferta deste serviço:";
                            lblDescricaoDetalhamento.Text = "Detalhamento sobre as alterações na oferta deste serviço:";
                            lblDetalhamento.Text = servico.Detalhamento;
                            trDetalhamentoDesativacao.Visible = true;
                            break;
                    }
                }
            }


            if (servico.IdAvaliacaoServico.HasValue)
            {
                switch (servico.IdAvaliacaoServico)
                {
                    case 1: lblAvaliacaoGestor.Text = "Está completamente de acordo com as normativas existentes para seu funcionamento, em especial a Tipificação Nacional de Serviços Socioassistenciais."; break;
                    case 2: lblAvaliacaoGestor.Text = "Apesar de se organizar conforme as normativas existentes, este serviço ainda necessita de algumas adequações."; break;
                    case 3: lblAvaliacaoGestor.Text = "Este serviço ainda não está funcionando conforme as normativas existentes e necessita de um reordenamento"; break;
                }
            }


            #region recursos Financeiros
            if (servico.ServicosRecursosFinanceirosFundosCREASInfo != null
                              && servico.ServicosRecursosFinanceirosFundosCREASInfo.Count > 0)
            {

                rptFundosAbas.DataSource = servico.ServicosRecursosFinanceirosFundosCREASInfo.Where(s => s.Exercicio >= 2022) ;
                rptFundosAbas.DataBind();

                rptFundosConteudo.DataSource = servico.ServicosRecursosFinanceirosFundosCREASInfo;
                rptFundosConteudo.DataBind();

                foreach (Control listItem in rptFundosAbas.Controls)
                {
                    if (listItem is HtmlGenericControl)
                    {
                        HtmlGenericControl li = listItem as HtmlGenericControl;
                        if (li.ID == "frame1_5_" + hdfAno)
                        {
                            li.Attributes.Add("class", "active");
                        }
                        else
                        {
                            li.Attributes.Remove("class");
                        }
                    }
                }
                trRecursosFinanceiros.Visible = true;
            }
            #endregion


            var recursoshumanos = proxy.Service.GetRecursosHumanosCREASByIdServicoRecursoFinanceiro(servico.Id);

            if (recursoshumanos != null)
            {

                lblSemEscolaridade.Text = recursoshumanos.SemEscolarizacao.ToString();
                lblFundamental.Text = recursoshumanos.NivelFundamental.ToString();
                lblMedio.Text = recursoshumanos.NivelMedio.ToString();
                lblSuperior.Text = recursoshumanos.NivelSuperior.ToString();


                lblSuperiorServicoSocial.Text = recursoshumanos.ServicoSocial.ToString();
                lblSuperiorPsicologia.Text = recursoshumanos.Psicologia.ToString();
                lblSuperiorPedagogia.Text = recursoshumanos.Pedagogia.ToString();
                lblSuperiorAntropologia.Text = recursoshumanos.Antropologia.ToString();
                lblSuperiorMusicoTerapia.Text = recursoshumanos.Musicoterapia.ToString();
                lblSuperiorTerapiaOcupacional.Text = recursoshumanos.TerapiaOcupacional.ToString();
                lblSuperiorEconomia.Text = recursoshumanos.Economia.ToString();
                lblSuperiorEconomiaDomestica.Text = recursoshumanos.EconomiaDomestica.ToString();
                lblSociologia.Text = recursoshumanos.Sociologia.ToString();
                lblDireito.Text = recursoshumanos.Direito.ToString();

                //lblPosGraduacao.Text = recursoshumanos.PosGraduacao.ToString();
                lblEstagiarios.Text = recursoshumanos.Estagiarios.ToString();
                lblVoluntarios.Text = recursoshumanos.Voluntarios.ToString();

                lblExclusivoServico.Text = recursoshumanos.ExclusivoServico.ToString();
                lblOutroServicos.Text = recursoshumanos.OutrosServicosAssistenciais.ToString();

                int total = recursoshumanos.SemEscolarizacao + recursoshumanos.NivelFundamental + recursoshumanos.NivelMedio + recursoshumanos.NivelSuperior;
                lblTotal.Text = total.ToString();
            }

            lblNomeTecnicoResponsavel.Text = servico.PossuiTecnicoResponsavel.HasValue && servico.PossuiTecnicoResponsavel.Value ? !String.IsNullOrWhiteSpace(servico.NomeTecnicoResponsavel) ?
                servico.NomeTecnicoResponsavel : "-" : "-";

            var lstProgramas = proxyProgramas.Service.GetProgramaProjetoCofinanciamentoByServicoRecursoFinanceiro(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])), 4);
            fldProgramas.Visible = lstProgramas.Count > 0;
            foreach (ProgramaProjetoCofinanciamentoInfo p in lstProgramas)
            {
                lblServicosProgramas.Text += p.ProgramaProjeto.Nome + "<br />";
            }

            var lstTransferencias = proxyProgramas.Service.GetTransferenciaRendaCofinanciamentoByServicoRecursoFinanceiro(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])), 4);
            fldTransferenciaRenda.Visible = lstTransferencias.Where(l => l.TransferenciaRenda.IdTipoTransferenciaRenda != 1 && l.TransferenciaRenda.IdTipoTransferenciaRenda != 2).ToList().Count > 0;
            foreach (ServicoRecursoFinanceiroTransferenciaRendaInfo t in lstTransferencias.Where(l => l.TransferenciaRenda.IdTipoTransferenciaRenda != 1 && l.TransferenciaRenda.IdTipoTransferenciaRenda != 2).ToList())
            {
                lblServicosTransferenciaRenda.Text += t.TransferenciaRenda.Nome + "<br />";
            }

            var lstBeneficios = proxyProgramas.Service.GetBeneficioEventualServicosByServicoRecursoFinanceiro(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])), 4);
            fldBeneficiosEventuais.Visible = lstBeneficios.Count > 0;
            foreach (PrefeituraBeneficioEventualServicoInfo b in lstBeneficios)
            {
                lblServicosBeneficiosEventuais.Text += b.PrefeituraBeneficioEventual.TipoBeneficioEventual.Nome + "<br />";
            }

            fldBeneficiosContinuados.Visible = lstTransferencias.Where(l => l.TransferenciaRenda.IdTipoTransferenciaRenda == 1 || l.TransferenciaRenda.IdTipoTransferenciaRenda == 2).ToList().Count > 0;
            foreach (ServicoRecursoFinanceiroTransferenciaRendaInfo t in lstTransferencias.Where(l => l.TransferenciaRenda.IdTipoTransferenciaRenda == 1 || l.TransferenciaRenda.IdTipoTransferenciaRenda == 2).ToList())
            {
                lblServicosBeneficiosContinuados.Text += t.TransferenciaRenda.Nome + "<br />";
            }

            trServicosAssociados.Visible = fldProgramas.Visible || fldTransferenciaRenda.Visible || fldBeneficiosEventuais.Visible || fldBeneficiosContinuados.Visible;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["idCentro"]))
            {
                var idCentro = Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]);
                Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCREAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro)));
                return;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["idProjeto"]))
            {
                var idProjeto = Genericos.clsCrypto.Decrypt(Request.QueryString["idProjeto"]);
                Response.Redirect("~/BlocoIII/FProgramaProjetoCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idProjeto)));
                return;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["idTransferenciaRenda"]))
            {
                var idTransferenciaRenda = Genericos.clsCrypto.Decrypt(Request.QueryString["idTransferenciaRenda"]);
                Response.Redirect("~/BlocoIII/FTransferenciaRendaCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idTransferenciaRenda)));
                return;
            }
        }
    }
}