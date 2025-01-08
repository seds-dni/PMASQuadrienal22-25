using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Resource;
using Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FServicoRecursoFinanceiroCREAS : System.Web.UI.Page
    {

        #region propriedades
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        #endregion

        #region sessao
        protected List<ServicoRecursoFinanceiroCREASFonteRecursoInfo> SessaoFontesRecursosExercicio1
        {
            get { return Session["FONTES_RECURSOS_CREAS_EXERCICIO1"] as List<ServicoRecursoFinanceiroCREASFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_CREAS_EXERCICIO1"] = value; }
        }
        protected List<ServicoRecursoFinanceiroCREASFonteRecursoInfo> SessaoFontesRecursosExercicio2
        {
            get { return Session["FONTES_RECURSOS_CREAS_EXERCICIO2"] as List<ServicoRecursoFinanceiroCREASFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_CREAS_EXERCICIO2"] = value; }
        }
        protected List<ServicoRecursoFinanceiroCREASFonteRecursoInfo> SessaoFontesRecursosExercicio3
        {
            get { return Session["FONTES_RECURSOS_CREAS_EXERCICIO3"] as List<ServicoRecursoFinanceiroCREASFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_CREAS_EXERCICIO3"] = value; }
        }
        protected List<ServicoRecursoFinanceiroCREASFonteRecursoInfo> SessaoFontesRecursosExercicio4
        {
            get { return Session["FONTES_RECURSOS_CREAS_EXERCICIO4"] as List<ServicoRecursoFinanceiroCREASFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_CREAS_EXERCICIO4"] = value; }
        }


        protected List<ProgramaProjetoCofinanciamentoInfo> SessaoProgramaProjetoCofinanciamentoExercicio
        {
            get { return Session["PROGRAMA_PROJETO_CO_FINANCIAMENTOINFO_CRAS1"] as List<ProgramaProjetoCofinanciamentoInfo>; }
            set { Session["PROGRAMA_PROJETO_CO_FINANCIAMENTOINFO_CRAS1"] = value; }
        }
        protected List<PrefeituraBeneficioEventualServicoInfo> SessaoPrefeituraBeneficioEventualServicoExercicio
        {
            get { return Session["PREFEITURA_BENEFICIO_EVENTUALSERVICOINFO_CRAS1"] as List<PrefeituraBeneficioEventualServicoInfo>; }
            set { Session["PREFEITURA_BENEFICIO_EVENTUALSERVICOINFO_CRAS1"] = value; }
        }
        protected List<ServicoRecursoFinanceiroTransferenciaRendaInfo> SessaoTransferenciaRendaCofinanciamentoExercicio
        {
            get { return Session["TRANSFERENCIA_RENDA_CO_FINANCIAMENTOINFO_CRAS1"] as List<ServicoRecursoFinanceiroTransferenciaRendaInfo>; }
            set { Session["TRANSFERENCIA_RENDA_CO_FINANCIAMENTOINFO_CRAS1"] = value; }
        }
        protected List<ConsultaProgramaProjetoServicoCofinanciamentoInfo> SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio
        {
            get { return Session["CONSULTA_PROGRAMA_PROJETO_SERVICOCO_FINANCIAMENTOINFO"] as List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>; }
            set { Session["CONSULTA_PROGRAMA_PROJETO_SERVICOCO_FINANCIAMENTOINFO"] = value; }
        }
        #endregion

        #region Carregamento
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                #region Validar: [Se Existe usuario com Prefeitura]
                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                #endregion

                #region Verificar: [Se usuario Possui CREAS]
                if (String.IsNullOrEmpty(Request.QueryString["idCentro"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx");
                    return;
                }
                #endregion

                #region Exibe: [Mensagem após salvar | atualizar]
                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "A")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço atualizado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "I")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço registrado com sucesso!<br/>"), true);
                }
                #endregion

                #region Verifica se há exercicio

                    if (String.IsNullOrEmpty(hdnExercicio.Value))
                    {
                        hdnExercicio.Value = "2022";
                    }

                #endregion

                #region Carregamento
                this.CamposBindEventos();
                this.CarregarCampos();
                this.ExibirFrameInicialmenteAtivo();
                ValidaBloqueioDesbloqueio();
                #endregion

                #region Bloqueia, Desbloqueia
                WebControl[] controles ={rblTipoProtecao,
                                         ddlTipoServico,
                                         ddlPublicoAlvo,
                                         ddlAbrangencia,
                                         txtTecnicoResponsavel,
                                         chkNaoPossuiTecnicoResponsavel,
                                         rblCaracteristicasTerritorio,
                                         rblIntegracaoRede,
                                         rblSexo,
                                         rblMoradiaUsuarios,
                                         lstSituacoesEspecificas,
                                         txtSemEscolaridade,
                                         txtNivelFundamental,
                                         txtNivelMedio,
                                         txtSuperior,
                                         txtSuperiorAntropologia,
                                         txtSuperiorEconomia,
                                         txtSuperiorEconomiaDomestica,
                                         txtSuperiorMusicoTerapia,
                                         txtSuperiorPedagogia,
                                         txtSuperiorPsicologia,
                                         txtSuperiorServicoSocial,
                                         txtSuperiorTerapiaOcupacional,
                                         txtEstagiarios,
                                         txtVoluntarios,
                                         txtExclusivoServico,
                                         txtOutroServicos,
                                         rblHorasSemana,
                                         rblDiasSemana,
                                         lstAtividades,
                                         rblAvaliacaoGestor
                                        };


                Permissao.VerificarPermissaoControles(controles, Session);
                Permissao.VerificarPermissaoControles(txtDataInicio.Controles, Session);
                #endregion

                this.ClearSessao();
            }



        }
        private void load(ProxyRedeProtecaoSocial proxy, ProxyEstruturaAssistenciaSocial proxyEstrutura)
        {
            var servico = proxy.Service.GetServicoRecursoFinanceiroCREASById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            if (servico == null)
                return;

            CarregarCaracterizacaoServico(proxyEstrutura, servico);
            CarregarSituacoes(proxyEstrutura);
            CarregarUsuarios(servico, proxyEstrutura);
            CarregarRecursosHumanos(proxy, servico);
            CarregarFuncionamento(servico);
            CarregarRecursosFinanceiros(servico);
            CarregaDemandasParlamentares(servico);

            //if (obj.PossuiProgramaBeneficio.HasValue)
            //{
            //rblIntegracaoRede.SelectedValue = Convert.ToInt32(obj.PossuiProgramaBeneficio.Value).ToString();
            //trProgramasBeneficios.Visible = obj.PossuiProgramaBeneficio.Value;
            //if (obj.PossuiProgramaBeneficio.Value == true)
            //{
            var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
            using (var proxyprogramas = new ProxyProgramas())
            {
                CarregarProgramas(proxyprogramas);
                LoadProgramas(proxyprogramas, servico.Id, idCentro);
            }
            //}
            //}
        }
        private void LoadProgramas(ProxyProgramas proxy, int idServicosRecursosFinanceiros, int idCentro)
        {
            var programas = proxy.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(idServicosRecursosFinanceiros, idCentro);
            if (programas != null)
            {
                var programasAgrupados = programas
                                   .OrderBy(t => t.IdTipoProtecao)
                                   .GroupBy(s => s.ProtecaoSocial)
                                   .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) })
                                   .ToList();

                if (programasAgrupados != null)
                {
                    lstRecursos.DataSource = programasAgrupados;
                    lstRecursos.DataBind();
                    if (lstRecursos.Items.Count > 0)
                    {
                        rblIntegracaoRede.SelectedValue = "1";
                        trProgramasBeneficios.Visible = true;
                    }
                    else
                    {
                        rblIntegracaoRede.SelectedValue = "0";
                        trProgramasBeneficios.Visible = false;
                    }
                }
            }

            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";

            btnSalvarRecursoPrograma.Visible = lstRecursos.Items.Count > 0;
        }

        private void ValidaBloqueioDesbloqueio()
        {
            WebControl[] controles1 = SelecionarControlesRecursosFinanceirosBloqueioExercicio1();
            WebControl[] controles2 = SelecionarControlesRecursosFinanceirosBloqueioExercicio2();
            WebControl[] controles3 = SelecionarControlesRecursosFinanceirosBloqueioExercicio3();
            WebControl[] controles4 = SelecionarControlesRecursosFinanceirosBloqueioExercicio4();

            var validaBloqueio2022 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroCREAS.Exercicios[0]);
            var validaBloqueio2023 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroCREAS.Exercicios[1]);
            var validaBloqueio2024 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroCREAS.Exercicios[2]);
            var validaBloqueio2025 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroCREAS.Exercicios[3]);

            if (validaBloqueio2022 == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio1()", "exibeBtnExcluirExercicio1();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio1()", "ocultaBtnExcluirExercicio1();", true);
            }

            if (validaBloqueio2023 == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio2()", "exibeBtnExcluirExercicio2();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio2()", "ocultaBtnExcluirExercicio2();", true);
            }

            if (validaBloqueio2024 == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio3()", "exibeBtnExcluirExercicio3();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio3()", "ocultaBtnExcluirExercicio3();", true);
            }

            if (validaBloqueio2025 == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio4()", "exibeBtnExcluirExercicio4();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio4()", "ocultaBtnExcluirExercicio4();", true);
            }

        }

        private bool RetornaValidacaoBloqueioDesbloqueio(int exercicio)
        {
            WebControl[] controles1 = SelecionarControlesRecursosFinanceirosBloqueioExercicio1();
            WebControl[] controles2 = SelecionarControlesRecursosFinanceirosBloqueioExercicio2();
            WebControl[] controles3 = SelecionarControlesRecursosFinanceirosBloqueioExercicio3();
            WebControl[] controles4 = SelecionarControlesRecursosFinanceirosBloqueioExercicio4();

            bool validacao = false;

            if (exercicio == 2022)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroCREAS.Exercicios[0]);
            }

            if (exercicio == 2023)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroCREAS.Exercicios[1]);
            }

            if (exercicio == 2024)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroCREAS.Exercicios[2]);
            }

            if (exercicio == 2025)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroCREAS.Exercicios[3]);
            }

            return validacao;

        }

        private void CarregarProgramas(ProxyProgramas proxy)
        {
            ddlProgramaBeneficio.DataValueField = "Id";
            ddlProgramaBeneficio.DataTextField = "Nome";
            ddlProgramaBeneficio.DataSource = proxy.Service.GetProgramasByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            ddlProgramaBeneficio.DataBind();
            Util.InserirItemEscolha(ddlProgramaBeneficio);
        }
        private void CarregarCaracterizacaoServico(ProxyEstruturaAssistenciaSocial proxyEstrutura, ServicoRecursoFinanceiroCREASInfo servico)
        {
            if (new List<Int32>() { 154, 155, 156 }.Contains(servico.UsuarioTipoServico.IdTipoServico))
            {
                servico.UsuarioTipoServico.IdTipoServico = R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_138;
            }
            if (new List<Int32>() { 157, 158, 159 }.Contains(servico.UsuarioTipoServico.IdTipoServico))
            {
                servico.UsuarioTipoServico.IdTipoServico = R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_145;
            }

            rblTipoProtecao.SelectedValue = servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial.ToString();
            CarregarTiposServicos(proxyEstrutura, servico.UsuarioTipoServico.IdTipoServico != R_TIPO_SERVICO.SERVICO_PROTECAO_ATENDIMENTO_ESPECIALIZADO_FAMILIAS_INDIVIDUOS_PAEFI);
            ddlTipoServico.SelectedValue = servico.UsuarioTipoServico.IdTipoServico.ToString();
            CarregarTiposServicosNaoTipificados(proxyEstrutura);
            ddlTipoServicoNaoTipificado.SelectedValue = "159";

            if (servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial == 2)
            {
                CarregarOfertaServico(true);

                rblCaracteristicaOferta.SelectedValue = servico.IdCaracteristicaOfertaServico.ToString();
            }
            else
            {
                CarregarOfertaServico(false);
            }

            #region serviço não tipificado
            if (servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_138 || servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_145)
            {
                tbNaoTipificado.Visible = true;
                tbNaoTipificadoDetalhado.Visible = ddlTipoServicoNaoTipificado.SelectedItem.Text == "Outro";
                lblIndicadorPeriodoCapacidade.Text = " famílias";
                lblIndicadorPeriodoMediaMensal.Text = " famílias";
            }
            else
            {
                lblIndicadorPeriodoCapacidadeLAPSC.Text = " famílias";
                lblIndicadorPeriodoMediaMensalLAPSC.Text = " famílias";
            }
            #endregion

            if (servico.UsuarioTipoServico.IdTipoServico == 153)
                tbNaoTipificadoDetalhado.Visible = true;

            txtNaotipificado.Text = !String.IsNullOrWhiteSpace(servico.DescricaoServicoNaoTipificado) ? servico.DescricaoServicoNaoTipificado : String.Empty;
            txtObjetivoNaoTipificado.Text = !String.IsNullOrWhiteSpace(servico.ObjetivoServicoNaoTipificado) ? servico.ObjetivoServicoNaoTipificado : String.Empty;

            chkNaoPossuiTecnicoResponsavel.Checked = servico.PossuiTecnicoResponsavel.HasValue ? !servico.PossuiTecnicoResponsavel.Value : false;
            txtTecnicoResponsavel.Text = chkNaoPossuiTecnicoResponsavel.Checked ? "" : !String.IsNullOrWhiteSpace(servico.NomeTecnicoResponsavel) ? servico.NomeTecnicoResponsavel : String.Empty;
            txtTecnicoResponsavel.Enabled = !chkNaoPossuiTecnicoResponsavel.Checked;

            if (servico.IdTipoServicoNaoTipificado.HasValue)
                CarregarUsuarios(proxyEstrutura, true);
            else
                CarregarUsuarios(proxyEstrutura);

            ddlPublicoAlvo.SelectedValue = servico.IdUsuarioTipoServico.ToString();

            if (servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_PROTECAO_ATENDIMENTO_ESPECIALIZADO_FAMILIAS_INDIVIDUOS_PAEFI)
            {
                lblIndicadorPeriodoCapacidade.Text = " famílias";
                lblIndicadorPeriodoMediaMensal.Text = " famílias";
                ddlPublicoAlvo.Enabled = ddlTipoServico.Enabled = rblTipoProtecao.Enabled = false;
            }

            if (servico.IdTipoServicoNaoTipificado.HasValue)
            {
                CarregarAtividades(proxyEstrutura, true);
            }
            else
            {
                CarregarAtividades(proxyEstrutura);
            }

            ddlAbrangencia.SelectedValue = servico.IdAbrangenciaServico.ToString();


            if (servico.IdAbrangenciaServico == 4)
            {
                trSedeServico.Visible = true;
                trFormaJuridica.Visible = true;

                if (servico.MunicipioSedeServico != null)
                {
                    rblAbrangencia.SelectedValue = servico.MunicipioSedeServico == true ? "1" : "0";

                    if (servico.MunicipioSedeServico == true)
                    {
                        trMunicipioParticipaOferta.Visible = true;
                        trMunicipioSede.Visible = false;

                        txtMunicipioParticipaOferta.Text = servico.IndicaMunicipiosParticipamOfertaServico;

                    }
                    else
                    {
                        trMunicipioParticipaOferta.Visible = false;
                        trMunicipioSede.Visible = true;
                        txtMunicipioSede.Text = servico.IndicaMunicipiosSedeServico;
                    }

                }

                if (servico.IdFormaJuridica != null)
                {
                    ddlFormaJuridica.SelectedValue = servico.IdFormaJuridica.ToString();

                    if (servico.IdFormaJuridica == 1)
                    {
                        trConsorcioPublico.Visible = true;

                        var consorcio = proxyEstrutura.Service.GetConsorcioCREAS(servico.Id);

                        if (consorcio != null)
                        {
                            txtNomeConsorcio.Text = consorcio.NomeConsorcio;
                            txtCNPJConsorcio.Text = consorcio.CNPJ;
                            txtMunicipioSedeConsorcio.Text = consorcio.MunicipioSede;
                            txtMunicipiosEnvolvidos.Text = consorcio.MunicipioEnvolvido;
                        }

                    }
                    else
                    {
                        trConsorcioPublico.Visible = false;
                    }
                }
            }
            else
            {
                trSedeServico.Visible = false;
                trFormaJuridica.Visible = false;
            }


            rblCaracteristicasTerritorio.SelectedValue = servico.IdCaracteristicasTerritorio.ToString();

            ddlPublicoAlvo_SelectedIndexChanged(null, null);
            //Welington p
            if (servico.Id != 0)
            {
                trAssociacaoProgramas.Visible = true;
            }

            this.AplicarRegraBloqueioDesbloqueio();

            AposSalvoNaoPermitirEdicaoCamposCaracterizacao(servico);
        }

        private void CarregarOfertaServico(bool p)
        {
            trCaracteristicaOferta.Visible = p;
        }
        
        private void CarregarUsuarios(ServicoRecursoFinanceiroCREASInfo obj, ProxyEstruturaAssistenciaSocial proxyEstrutura)
        {
            rblMoradiaUsuarios.SelectedValue = obj.IdRegiaoMoradia.HasValue ? obj.IdRegiaoMoradia.Value.ToString() : String.Empty;
            rblSexo.SelectedValue = obj.IdSexo.HasValue ? obj.IdSexo.Value.ToString() : String.Empty;
            if (obj.SituacoesEspecificas != null && obj.SituacoesEspecificas.Count > 0)
                foreach (ListItem i in lstSituacoesEspecificas.Items)
                    i.Selected = obj.SituacoesEspecificas.Any(s => s.Id == Convert.ToInt32(i.Value));
        }
        private void CarregarRecursosHumanos(ProxyRedeProtecaoSocial proxy, ServicoRecursoFinanceiroCREASInfo obj)
        {
            //RECURSOS HUMANOS
            var recursoshumanos = proxy.Service.GetRecursosHumanosCREASByIdServicoRecursoFinanceiro(obj.Id);
            if (recursoshumanos != null)
            {
                hdfIdRecursosHumanos.Value = recursoshumanos.Id.ToString();

                txtSemEscolaridade.Text = recursoshumanos.SemEscolarizacao.ToString();
                txtNivelFundamental.Text = recursoshumanos.NivelFundamental.ToString();
                txtNivelMedio.Text = recursoshumanos.NivelMedio.ToString();
                txtSuperior.Text = recursoshumanos.NivelSuperior.ToString();

                txtSuperiorServicoSocial.Text = recursoshumanos.ServicoSocial.ToString();
                txtSuperiorPsicologia.Text = recursoshumanos.Psicologia.ToString();
                txtSuperiorPedagogia.Text = recursoshumanos.Pedagogia.ToString();
                txtSuperiorAntropologia.Text = recursoshumanos.Antropologia.ToString();
                txtSuperiorMusicoTerapia.Text = recursoshumanos.Musicoterapia.ToString();
                txtSuperiorTerapiaOcupacional.Text = recursoshumanos.TerapiaOcupacional.ToString();
                txtSuperiorEconomia.Text = recursoshumanos.Economia.ToString();
                txtSuperiorEconomiaDomestica.Text = recursoshumanos.EconomiaDomestica.ToString();
                txtSociologia.Text = recursoshumanos.Sociologia.ToString();
                txtDireito.Text = recursoshumanos.Direito.ToString();

                //txtPosGraduacao.Text = recursoshumanos.PosGraduacao.ToString();
                txtEstagiarios.Text = recursoshumanos.Estagiarios.ToString();
                txtVoluntarios.Text = recursoshumanos.Voluntarios.ToString();

                txtExclusivoServico.Text = recursoshumanos.ExclusivoServico.ToString();
                txtOutroServicos.Text = recursoshumanos.OutrosServicosAssistenciais.ToString();
            }

            CarregarTotalRh();
        }
        private void CarregarFuncionamento(ServicoRecursoFinanceiroCREASInfo servico)
        {
            #region Carrega: [Data de início de funcionamento]
            txtDataInicio.Text = servico.DataFuncionamentoServico.HasValue ? servico.DataFuncionamentoServico.Value.ToShortDateString() : String.Empty;
            #endregion

            #region Carregar: Atividades Socioassistenciais
            if (servico.AtividadesSocioAssistenciais != null && servico.AtividadesSocioAssistenciais.Count > 0)
            {
                foreach (ListItem atividadeSocioAssistencial in lstAtividades.Items)
                {
                    atividadeSocioAssistencial.Selected = servico.AtividadesSocioAssistenciais.Any(s => s.Id == Convert.ToInt32(atividadeSocioAssistencial.Value));
                }
            }
            #endregion


            #region capacidade, capacidade [la|psc]
            if (servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_PROTECAO_SOCIAL_ADOLESC_CUMPR_MEDIDA_SOCIOEDUCATIVA_LA_PSC)
            {
                DistribuirCapacidadeLA(servico);
                DistribuirCapacidadePSC(servico);

                DistribuirMediaMensalLA(servico);
                DistribuirMediaMensalPSC(servico);
            }
            else
            {
                DistribuirCapacidade(servico);
                DistribuirMediaMensal(servico);
            }
            this.AplicarRegraExibicaoLayoutServicosChanged();
            #endregion

            #region Carregar: Quadro [Este serviço funciona quantas horas por semana?]
            rblHorasSemana.SelectedValue = servico.IdHorasSemana.ToString();
            rblDiasSemana.SelectedValue = servico.QuantidadeDiasSemana.ToString();
            rblAvaliacaoGestor.SelectedValue = servico.IdAvaliacaoServico.ToString();
            #endregion
        }
        private void CarregarRecursosFinanceiros(ServicoRecursoFinanceiroCREASInfo entidade)
        {
            int idService = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

            #region Exercicios
            #region Exercicio 1
            var fundoExercicio1 = entidade.ServicosRecursosFinanceirosFundosCREASInfo
                    .Where(x => x.ServicoRecursoFinanceiroCREASInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroCREAS.Exercicios[0]).FirstOrDefault();
            if (fundoExercicio1 != null)
            {
                txtFMASExercicio1.Text = fundoExercicio1.ValorMunicipalAssistencia.ToString("N2");
                txtFMDCAExercicio1.Text = fundoExercicio1.ValorMunicipalFMDCA.ToString("N2");
                txtFEASExercicio1.Text = fundoExercicio1.ValorEstadualAssistencia.ToString("N2");
                txtFEDCAExercicio1.Text = fundoExercicio1.ValorEstadualFEDCA.ToString("N2");
                txtFNASExercicio1.Text = fundoExercicio1.ValorFederalAssistencia.ToString("N2");
                txtFNDCAExercicio1.Text = fundoExercicio1.ValorFederalFNDCA.ToString("N2");
                txtFMIExercicio1.Text = fundoExercicio1.ValorMunicipalFMI.ToString("N2");
                txtFEIExercicio1.Text = fundoExercicio1.ValorEstadualFEI.ToString("N2");
                txtFNIExercicio1.Text = fundoExercicio1.ValorFederalFNI.ToString("N2");
                txtFEASDemandasExercicio1.Text = fundoExercicio1.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio1.Text = fundoExercicio1.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");
                txtFEASAnoAnteriorExercicio1.Text = fundoExercicio1.ValorEstadualAssistenciaAnoAnterior.ToString("N2");

                rblOutrasFontesExercicio1.SelectedValue = fundoExercicio1.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio1.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio1_SelectedIndexChanged(null, null);
                if (fundoExercicio1.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio1.ExisteOutraFonteFinanciamento.Value)
                {
                    //Sessao
                    foreach (var item in fundoExercicio1.ServicoRecursoFinanceiroCREASFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2022);
                    }
                    this.SessaoFontesRecursosExercicio1 = fundoExercicio1.ServicoRecursoFinanceiroCREASFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio1();
                }
            }
            #endregion

            #region Exercicio 2
            var fundoExercicio2 = entidade.ServicosRecursosFinanceirosFundosCREASInfo
                    .Where(x => x.ServicoRecursoFinanceiroCREASInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroCREAS.Exercicios[1]).FirstOrDefault();
            if (fundoExercicio2 != null)
            {
                txtFMASExercicio2.Text = fundoExercicio2.ValorMunicipalAssistencia.ToString("N2");
                txtFMDCAExercicio2.Text = fundoExercicio2.ValorMunicipalFMDCA.ToString("N2");
                txtFEASExercicio2.Text = fundoExercicio2.ValorEstadualAssistencia.ToString("N2");
                txtFEDCAExercicio2.Text = fundoExercicio2.ValorEstadualFEDCA.ToString("N2");
                txtFNASExercicio2.Text = fundoExercicio2.ValorFederalAssistencia.ToString("N2");
                txtFNDCAExercicio2.Text = fundoExercicio2.ValorFederalFNDCA.ToString("N2");
                txtFMIExercicio2.Text = fundoExercicio2.ValorMunicipalFMI.ToString("N2");
                txtFEIExercicio2.Text = fundoExercicio2.ValorEstadualFEI.ToString("N2");
                txtFNIExercicio2.Text = fundoExercicio2.ValorFederalFNI.ToString("N2");
                txtFEASDemandasExercicio2.Text = fundoExercicio2.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio2.Text = fundoExercicio2.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");
                txtFEASAnoAnteriorExercicio2.Text = (fundoExercicio2 == null) ? "0,00" : fundoExercicio2.ValorEstadualAssistenciaAnoAnterior.ToString("N2");

                rblOutrasFontesExercicio2.SelectedValue = fundoExercicio2.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio2.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio2_SelectedIndexChanged(null, null);
                if (fundoExercicio2.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio2.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var item in fundoExercicio2.ServicoRecursoFinanceiroCREASFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2023);
                    }
                    this.SessaoFontesRecursosExercicio2 = fundoExercicio2.ServicoRecursoFinanceiroCREASFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio2();
                }
            }
            #endregion

            #region Exercicio 3
            var fundoExercicio3 = entidade.ServicosRecursosFinanceirosFundosCREASInfo
                    .Where(x => x.ServicoRecursoFinanceiroCREASInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroCREAS.Exercicios[2]).FirstOrDefault();
            if (fundoExercicio3 != null)
            {
                txtFMASExercicio3.Text = fundoExercicio3.ValorMunicipalAssistencia.ToString("N2");
                txtFMDCAExercicio3.Text = fundoExercicio3.ValorMunicipalFMDCA.ToString("N2");
                txtFEASExercicio3.Text = fundoExercicio3.ValorEstadualAssistencia.ToString("N2");
                txtFEDCAExercicio3.Text = fundoExercicio3.ValorEstadualFEDCA.ToString("N2");
                txtFNASExercicio3.Text = fundoExercicio3.ValorFederalAssistencia.ToString("N2");
                txtFNDCAExercicio3.Text = fundoExercicio3.ValorFederalFNDCA.ToString("N2");
                txtFMIExercicio3.Text = fundoExercicio3.ValorMunicipalFMI.ToString("N2");
                txtFEIExercicio3.Text = fundoExercicio3.ValorEstadualFEI.ToString("N2");
                txtFNIExercicio3.Text = fundoExercicio3.ValorFederalFNI.ToString("N2");
                txtFEASDemandasExercicio3.Text = fundoExercicio3.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio3.Text = fundoExercicio3.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");
                txtFEASAnoAnteriorExercicio3.Text = (fundoExercicio3 == null) ? "0,00" : fundoExercicio3.ValorEstadualAssistenciaAnoAnterior.ToString("N2");

                rblOutrasFontesExercicio3.SelectedValue = fundoExercicio3.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio3.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio3_SelectedIndexChanged(null, null);
                if (fundoExercicio3.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio3.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var item in fundoExercicio3.ServicoRecursoFinanceiroCREASFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2024);
                    }
                    this.SessaoFontesRecursosExercicio3 = fundoExercicio3.ServicoRecursoFinanceiroCREASFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio3();
                }
            }
            #endregion

            #region Exercicio 4
            var fundoExercicio4 = entidade.ServicosRecursosFinanceirosFundosCREASInfo
                    .Where(x => x.ServicoRecursoFinanceiroCREASInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroCREAS.Exercicios[3]).FirstOrDefault();
            if (fundoExercicio4 != null)
            {
                txtFMASExercicio4.Text = fundoExercicio4.ValorMunicipalAssistencia.ToString("N2");
                txtFMDCAExercicio4.Text = fundoExercicio4.ValorMunicipalFMDCA.ToString("N2");
                txtFEASExercicio4.Text = fundoExercicio4.ValorEstadualAssistencia.ToString("N2");
                txtFEDCAExercicio4.Text = fundoExercicio4.ValorEstadualFEDCA.ToString("N2");
                txtFNASExercicio4.Text = fundoExercicio4.ValorFederalAssistencia.ToString("N2");
                txtFNDCAExercicio4.Text = fundoExercicio4.ValorFederalFNDCA.ToString("N2");
                txtFMIExercicio4.Text = fundoExercicio4.ValorMunicipalFMI.ToString("N2");
                txtFEIExercicio4.Text = fundoExercicio4.ValorEstadualFEI.ToString("N2");
                txtFNIExercicio4.Text = fundoExercicio4.ValorFederalFNI.ToString("N2");
                txtFEASDemandasExercicio4.Text = fundoExercicio4.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio4.Text = fundoExercicio4.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");
                txtFEASAnoAnteriorExercicio4.Text = (fundoExercicio4 == null) ? "0,00" : fundoExercicio4.ValorEstadualAssistenciaAnoAnterior.ToString("N2");

                rblOutrasFontesExercicio4.SelectedValue = fundoExercicio4.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio4.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio4_SelectedIndexChanged(null, null);
                if (fundoExercicio4.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio4.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var item in fundoExercicio4.ServicoRecursoFinanceiroCREASFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2025);
                    }
                    this.SessaoFontesRecursosExercicio4 = fundoExercicio4.ServicoRecursoFinanceiroCREASFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio4();
                }
            }
            #endregion

            #endregion

            using (var proxyprefeitura = new ProxyPrefeitura())
            {
                var pre = proxyprefeitura.Service.GetPrefeituraById(Convert.ToInt32(SessaoPmas.UsuarioLogado.Prefeitura.Id));
                if (pre.ValoresReprogramadosDrads.HasValue && pre.ValoresReprogramadosDrads.Value == true)
                {
                    trFeasAnterior.Visible = true;
                }
            }
            this.AplicarRegraBloqueioDesbloqueio();
        }

        protected void CarregaDemandasParlamentares(ServicoRecursoFinanceiroCREASInfo Servico)
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                var demandas = Servico.ServicosRecursosFinanceirosFundosCREASInfo.Where(s => s.Exercicio >= 2022);

                if (demandas.Count() != 0)
                {

                    var demandas1 = demandas.Where(d => d.Exercicio == 2022).FirstOrDefault();
                    if (demandas1 != null)
                    {
                        txtCodigoDemandaExercicio1.Text = demandas1.CodigoDemandaParlamentar.ToString();
                        txtObjetoDemandaExercicio1.Text = demandas1.ObjetoDemandaParlamentar.ToString();

                        if (demandas1.ContrapartidaMunicipal == true)
                        {
                            trValorContraExercicio1.Visible = true;
                            rblContraPartida1.SelectedValue = "1";
                            txtValorContraExercicio1.Text = demandas1.ValorContrapartidaMunicipal.HasValue ? demandas1.ValorContrapartidaMunicipal.Value.ToString("N2") : "0,00";
                        }
                        else
                        {
                            rblContraPartida1.SelectedValue = "0";
                            trValorContraExercicio1.Visible = false;
                        }
                    }

                    var demandas2 = demandas.Where(d => d.Exercicio == 2023).FirstOrDefault();

                    if (demandas2 != null)
                    {
                        txtCodigoDemandaExercicio2.Text = demandas2.CodigoDemandaParlamentar.ToString();
                        txtObjetoDemandaExercicio2.Text = demandas2.ObjetoDemandaParlamentar.ToString();

                        if (demandas2.ContrapartidaMunicipal == true)
                        {
                            trValorContraExercicio2.Visible = true;
                            rblContraPartida1.SelectedValue = "1";
                            txtValorContraExercicio2.Text = demandas2.ValorContrapartidaMunicipal.HasValue ? demandas2.ValorContrapartidaMunicipal.Value.ToString("N2") : "0,00";
                        }
                        else
                        {
                            rblContraPartida2.SelectedValue = "0";
                            trValorContraExercicio2.Visible = false;
                        }
                    }

                    var demandas3 = demandas.Where(d => d.Exercicio == 2024).FirstOrDefault();
                    if (demandas3 != null)
                    {
                        txtCodigoDemandaExercicio3.Text = demandas3.CodigoDemandaParlamentar.ToString();
                        txtObjetoDemandaExercicio3.Text = demandas3.ObjetoDemandaParlamentar.ToString();

                        if (demandas3.ContrapartidaMunicipal == true)
                        {
                            trValorContraExercicio3.Visible = true;
                            rblContraPartida3.SelectedValue = "1";
                            txtValorContraExercicio3.Text = demandas3.ValorContrapartidaMunicipal.HasValue ? demandas3.ValorContrapartidaMunicipal.Value.ToString("N2") : "0,00";
                        }
                        else
                        {
                            rblContraPartida1.SelectedValue = "0";
                            trValorContraExercicio1.Visible = false;
                        }
                    }

                    var demandas4 = demandas.Where(d => d.Exercicio == 2025).FirstOrDefault();

                    if (demandas4 != null)
                    {
                        txtCodigoDemandaExercicio4.Text = demandas4.CodigoDemandaParlamentar.ToString();
                        txtObjetoDemandaExercicio4.Text = demandas4.ObjetoDemandaParlamentar.ToString();

                        if (demandas4.ContrapartidaMunicipal == true)
                        {
                            trValorContraExercicio4.Visible = true;
                            rblContraPartida4.SelectedValue = "1";
                            txtValorContraExercicio4.Text = demandas4.ValorContrapartidaMunicipal.HasValue ? demandas4.ValorContrapartidaMunicipal.Value.ToString("N2") : "0,00";
                        }
                        else
                        {
                            rblContraPartida4.SelectedValue = "0";
                            trValorContraExercicio4.Visible = false;
                        }
                    }
                }
            }

        }

        private void CarregarCombos(ProxyEstruturaAssistenciaSocial proxy, Boolean carregarTipoServicos)
        {
            ddlAbrangencia.DataTextField = "Nome";
            ddlAbrangencia.DataValueField = "Id";
            ddlAbrangencia.DataSource = proxy.Service.GetAbrangenciasServico().Where(S => S.Id >= 3);
            ddlAbrangencia.DataBind();
            Util.InserirItemEscolha(ddlAbrangencia);

            ddlFormaJuridica.DataTextField = "NomeForma";
            ddlFormaJuridica.DataValueField = "Id";
            ddlFormaJuridica.DataSource = proxy.Service.GetFormaJuridica();
            ddlFormaJuridica.DataBind();
            Util.InserirItemEscolha(ddlFormaJuridica);

            //SOMENTE ESPECIAL DE MÉDIA COMPLEXIDADE
            rblTipoProtecao.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(t => t.Id == 2);
            rblTipoProtecao.DataValueField = "Id";
            rblTipoProtecao.DataTextField = "Nome";
            rblTipoProtecao.DataBind();
            rblTipoProtecao.SelectedValue = "2";

            if (carregarTipoServicos)
                CarregarTiposServicos(proxy, true);
        }
        private void CarregarTiposServicos(ProxyEstruturaAssistenciaSocial proxy, Boolean retirarPAEFI)
        {
            ddlTipoServico.DataTextField = "Nome";
            ddlTipoServico.DataValueField = "Id";
            var tipos = proxy.Service.GetTiposServicoByTipoProtecaoSocial(Convert.ToInt32(rblTipoProtecao.SelectedValue));
            if (retirarPAEFI)
                ddlTipoServico.DataSource = tipos.Where(t => t.Id != R_TIPO_SERVICO.SERVICO_PROTECAO_ATENDIMENTO_ESPECIALIZADO_FAMILIAS_INDIVIDUOS_PAEFI && t.Id != R_TIPO_SERVICO.SERVICO_ESPECIALIZADO_PESSOAS_SITUACAO_RUA);
            else
                ddlTipoServico.DataSource = tipos.Where(t => t.Id != R_TIPO_SERVICO.SERVICO_ESPECIALIZADO_PESSOAS_SITUACAO_RUA);
            ddlTipoServico.DataBind();
            ListItem itemToRemove = ddlTipoServico.Items.FindByValue(R_TIPO_SERVICO.SERVICO_PROTECAO_SOCIAL_ADOLESC_CUMPR_MEDIDA_SOCIOEDUCATIVA_PSC.ToString());
            if (itemToRemove != null)
            {
                ddlTipoServico.Items.Remove(itemToRemove);
            }
            Util.InserirItemEscolha(ddlTipoServico);
        }
        private void CarregarSituacoes(ProxyEstruturaAssistenciaSocial proxy)
        {
            lstSituacoesEspecificas.DataTextField = "Nome";
            lstSituacoesEspecificas.DataValueField = "Id";
            lstSituacoesEspecificas.DataSource = proxy.Service.GetSituacoesEspecificasByUsuario(Convert.ToInt32(ddlPublicoAlvo.SelectedValue));
            lstSituacoesEspecificas.DataBind();
        }
        private void CarregarUsuarios(ProxyEstruturaAssistenciaSocial proxy, Boolean naoTipificado = false)
        {
            ddlPublicoAlvo.DataTextField = "Nome";
            ddlPublicoAlvo.DataValueField = "Id";
            if (!naoTipificado)
                ddlPublicoAlvo.DataSource = proxy.Service.GetUsuariosByTipoServico(Convert.ToInt32(ddlTipoServico.SelectedValue));
            else
                ddlPublicoAlvo.DataSource = proxy.Service.GetUsuariosByTipoServico(Convert.ToInt32(ddlTipoServicoNaoTipificado.SelectedValue));
            ddlPublicoAlvo.DataBind();
            Util.InserirItemEscolha(ddlPublicoAlvo);
        }
        private void CarregarAtividades(ProxyEstruturaAssistenciaSocial proxy, Boolean naoTipificado = false)
        {
            lstAtividades.DataValueField = "Id";
            lstAtividades.DataTextField = "Nome";
            if (!naoTipificado)
                lstAtividades.DataSource = proxy.Service.GetAtividadesSocioAssistenciaisByTipoServico(Convert.ToInt32(ddlTipoServico.SelectedValue));
            else
                lstAtividades.DataSource = proxy.Service.GetAtividadesSocioAssistenciaisByTipoServico(Convert.ToInt32(ddlTipoServicoNaoTipificado.SelectedValue));
            lstAtividades.DataBind();
        }
        private void CarregarAvaliacoes(ProxyEstruturaAssistenciaSocial proxy)
        {
            rblAvaliacaoGestor.DataTextField = "Descricao";
            rblAvaliacaoGestor.DataValueField = "Id";
            rblAvaliacaoGestor.DataSource = proxy.Service.GetAvaliacoes();
            rblAvaliacaoGestor.DataBind();
        }

        #region Serviços Recursos Financeiros
        private void CarregarRecursosFinanceirosFonteRecursosExercicio1()
        {
            lstRecursosAdicionadosExercicio1.DataSource = this.SessaoFontesRecursosExercicio1;
            lstRecursosAdicionadosExercicio1.DataBind();

            if (lstRecursosAdicionadosExercicio1.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio1.Visible = lstRecursosAdicionadosExercicio1.Visible = true;
            }
        }
        private void CarregarRecursosFinanceirosFonteRecursosExercicio2()
        {
            lstRecursosAdicionadosExercicio2.DataSource = this.SessaoFontesRecursosExercicio2;
            lstRecursosAdicionadosExercicio2.DataBind();

            if (lstRecursosAdicionadosExercicio2.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio2.Visible = lstRecursosAdicionadosExercicio2.Visible = true;
            }
        }
        private void CarregarRecursosFinanceirosFonteRecursosExercicio3()
        {
            lstRecursosAdicionadosExercicio3.DataSource = this.SessaoFontesRecursosExercicio3;
            lstRecursosAdicionadosExercicio3.DataBind();

            if (lstRecursosAdicionadosExercicio3.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio3.Visible = lstRecursosAdicionadosExercicio3.Visible = true;
            }
        }
        private void CarregarRecursosFinanceirosFonteRecursosExercicio4()
        {
            lstRecursosAdicionadosExercicio4.DataSource = this.SessaoFontesRecursosExercicio4;
            lstRecursosAdicionadosExercicio4.DataBind();

            if (lstRecursosAdicionadosExercicio4.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio4.Visible = lstRecursosAdicionadosExercicio4.Visible = true;
            }
        }
        #endregion


        private ServicoRecursoFinanceiroCREASRecursosHumanosInfo CarregarRH()
        {
            ServicoRecursoFinanceiroCREASRecursosHumanosInfo recursosHumanos = new ServicoRecursoFinanceiroCREASRecursosHumanosInfo();
            recursosHumanos.Id = Convert.ToInt32(hdfIdRecursosHumanos.Value);
            if (!String.IsNullOrEmpty(txtSemEscolaridade.Text))
                recursosHumanos.SemEscolarizacao = Convert.ToInt32(txtSemEscolaridade.Text);
            if (!String.IsNullOrEmpty(txtNivelFundamental.Text))
                recursosHumanos.NivelFundamental = Convert.ToInt32(txtNivelFundamental.Text);
            if (!String.IsNullOrEmpty(txtNivelMedio.Text))
                recursosHumanos.NivelMedio = Convert.ToInt32(txtNivelMedio.Text);
            if (!String.IsNullOrEmpty(txtSuperior.Text))
                recursosHumanos.NivelSuperior = Convert.ToInt32(txtSuperior.Text);
            //if (!String.IsNullOrEmpty(txtPosGraduacao.Text))
            //    recursosHumanos.PosGraduacao = Convert.ToInt32(txtPosGraduacao.Text);
            if (!String.IsNullOrEmpty(txtEstagiarios.Text))
                recursosHumanos.Estagiarios = Convert.ToInt32(txtEstagiarios.Text);
            if (!String.IsNullOrEmpty(txtVoluntarios.Text))
                recursosHumanos.Voluntarios = Convert.ToInt32(txtVoluntarios.Text);


            if (!String.IsNullOrEmpty(txtSuperiorServicoSocial.Text))
                recursosHumanos.ServicoSocial = Convert.ToInt32(txtSuperiorServicoSocial.Text);
            if (!String.IsNullOrEmpty(txtSuperiorPsicologia.Text))
                recursosHumanos.Psicologia = Convert.ToInt32(txtSuperiorPsicologia.Text);
            if (!String.IsNullOrEmpty(txtSuperiorPedagogia.Text))
                recursosHumanos.Pedagogia = Convert.ToInt32(txtSuperiorPedagogia.Text);
            if (!String.IsNullOrEmpty(txtSociologia.Text))
                recursosHumanos.Sociologia = Convert.ToInt32(txtSociologia.Text);
            if (!String.IsNullOrEmpty(txtDireito.Text))
                recursosHumanos.Direito = Convert.ToInt32(txtDireito.Text);
            if (!String.IsNullOrEmpty(txtSuperiorTerapiaOcupacional.Text))
                recursosHumanos.TerapiaOcupacional = Convert.ToInt32(txtSuperiorTerapiaOcupacional.Text);
            if (!String.IsNullOrEmpty(txtSuperiorMusicoTerapia.Text))
                recursosHumanos.Musicoterapia = Convert.ToInt32(txtSuperiorMusicoTerapia.Text);
            if (!String.IsNullOrEmpty(txtSuperiorEconomia.Text))
                recursosHumanos.Economia = Convert.ToInt32(txtSuperiorEconomia.Text);
            if (!String.IsNullOrEmpty(txtSuperiorEconomiaDomestica.Text))
                recursosHumanos.EconomiaDomestica = Convert.ToInt32(txtSuperiorEconomiaDomestica.Text);
            if (!String.IsNullOrEmpty(txtSuperiorAntropologia.Text))
                recursosHumanos.Antropologia = Convert.ToInt32(txtSuperiorAntropologia.Text);
            if (!String.IsNullOrEmpty(txtExclusivoServico.Text))
                recursosHumanos.ExclusivoServico = Convert.ToInt32(txtExclusivoServico.Text);
            if (!String.IsNullOrEmpty(txtOutroServicos.Text))
                recursosHumanos.OutrosServicosAssistenciais = Convert.ToInt32(txtOutroServicos.Text);



            return recursosHumanos;
        }
        private void CarregarTotalRh()
        {
            try
            {
                int?[] array = {Util.TryParseInt32(txtSemEscolaridade.Text)
                               ,Util.TryParseInt32(txtNivelFundamental.Text)
                               ,Util.TryParseInt32(txtNivelMedio.Text)
                               ,Util.TryParseInt32(txtSuperior.Text)                          
                               };
                int?[] arrayEstVol = { Util.TryParseInt32(txtEstagiarios.Text) };
                lblTotalRh.Text = array.Sum().ToString(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CarregarTiposServicosNaoTipificados(ProxyEstruturaAssistenciaSocial proxy) //PMAS 2016
        {
            ddlTipoServicoNaoTipificado.DataTextField = "Nome";
            ddlTipoServicoNaoTipificado.DataValueField = "Id";
            //Não carregar "Serviço de atendimento a famílias e indivíduos em situação de risco social realizado fora do CREAS"
            //Não carregar "Serviço de atendimento especializado complementar ao PAEFI ofertado fora do CREAS"
            ddlTipoServicoNaoTipificado.DataSource = rblTipoProtecao.SelectedValue != "3" ?
                proxy.Service.GetTiposServicoNaoTipificadoByTipoProtecaoSocial(Convert.ToInt32(rblTipoProtecao.SelectedValue)).Where(t => t.Id != 157 && t.Id != 158).ToList() : new List<TipoServicoInfo>();
            ddlTipoServicoNaoTipificado.DataBind();
            Util.InserirItemEscolha(ddlTipoServicoNaoTipificado);
        }

        #region Helper [Funcionamento] [Servicos]
        private void DistribuirCapacidade(ServicoRecursoFinanceiroCREASInfo servico)
        {
            ServicoRecursoFinanceiroCREASCapacidadeInfo capacidadeExercicio1 = servico.ServicosRecursosFinanceiroCREASCapacidade.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtCapacidadeExercicio1.Text = capacidadeExercicio1 != null ? capacidadeExercicio1.Capacidade != null ? capacidadeExercicio1.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASCapacidadeInfo capacidadeExercicio2 = servico.ServicosRecursosFinanceiroCREASCapacidade.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtCapacidadeExercicio2.Text = capacidadeExercicio2 != null ? capacidadeExercicio2.Capacidade != null ? capacidadeExercicio2.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASCapacidadeInfo capacidadeExercicio3 = servico.ServicosRecursosFinanceiroCREASCapacidade.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtCapacidadeExercicio3.Text = capacidadeExercicio3 != null ? capacidadeExercicio3.Capacidade != null ? capacidadeExercicio3.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASCapacidadeInfo capacidadeExercicio4 = servico.ServicosRecursosFinanceiroCREASCapacidade.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtCapacidadeExercicio4.Text = capacidadeExercicio4 != null ? capacidadeExercicio4.Capacidade != null ? capacidadeExercicio4.Capacidade.ToString() : string.Empty : string.Empty;
        }
        private void DistribuirCapacidadeLA(ServicoRecursoFinanceiroCREASInfo servico)
        {
            ServicoRecursoFinanceiroCREASCapacidadeLAInfo capacidadeLAExercicio1 = servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtCapacidadeLAExercicio1.Text = capacidadeLAExercicio1 != null ? capacidadeLAExercicio1.Capacidade != null ? capacidadeLAExercicio1.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASCapacidadeLAInfo capacidadeLAExercicio2 = servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtCapacidadeLAExercicio2.Text = capacidadeLAExercicio2 != null ? capacidadeLAExercicio2.Capacidade != null ? capacidadeLAExercicio2.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASCapacidadeLAInfo capacidadeLAExercicio3 = servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtCapacidadeLAExercicio3.Text = capacidadeLAExercicio3 != null ? capacidadeLAExercicio3.Capacidade != null ? capacidadeLAExercicio3.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASCapacidadeLAInfo capacidadeLAExercicio4 = servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtCapacidadeLAExercicio4.Text = capacidadeLAExercicio4 != null ? capacidadeLAExercicio4.Capacidade != null ? capacidadeLAExercicio4.Capacidade.ToString() : string.Empty : string.Empty;
        }
        private void DistribuirCapacidadePSC(ServicoRecursoFinanceiroCREASInfo servico)
        {
            ServicoRecursoFinanceiroCREASCapacidadePSCInfo capacidadePSCExercicio1 = servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtCapacidadePSCExercicio1.Text = capacidadePSCExercicio1 != null ? capacidadePSCExercicio1.Capacidade != null ? capacidadePSCExercicio1.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASCapacidadePSCInfo capacidadePSCExercicio2 = servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtCapacidadePSCExercicio2.Text = capacidadePSCExercicio2 != null ? capacidadePSCExercicio2.Capacidade != null ? capacidadePSCExercicio2.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASCapacidadePSCInfo capacidadePSCExercicio3 = servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtCapacidadePSCExercicio3.Text = capacidadePSCExercicio3 != null ? capacidadePSCExercicio3.Capacidade != null ? capacidadePSCExercicio3.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASCapacidadePSCInfo capacidadePSCExercicio4 = servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtCapacidadePSCExercicio4.Text = capacidadePSCExercicio4 != null ? capacidadePSCExercicio4.Capacidade != null ? capacidadePSCExercicio4.Capacidade.ToString() : string.Empty : string.Empty;
        }

        private void DistribuirMediaMensal(ServicoRecursoFinanceiroCREASInfo servico)
        {
            ServicoRecursoFinanceiroCREASMediaMensalInfo mediaMensalExercicio1 = servico.ServicosRecursosFinanceiroCREASMediaMensal.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtMediaMensalExercicio1.Text = mediaMensalExercicio1 != null ? mediaMensalExercicio1.MediaMensal != null ? mediaMensalExercicio1.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASMediaMensalInfo mediaMensalExercicio2 = servico.ServicosRecursosFinanceiroCREASMediaMensal.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtMediaMensalExercicio2.Text = mediaMensalExercicio2 != null ? mediaMensalExercicio2.MediaMensal != null ? mediaMensalExercicio2.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASMediaMensalInfo mediaMensalExercicio3 = servico.ServicosRecursosFinanceiroCREASMediaMensal.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtMediaMensalExercicio3.Text = mediaMensalExercicio3 != null ? mediaMensalExercicio3.MediaMensal != null ? mediaMensalExercicio3.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASMediaMensalInfo mediaMensalExercicio4 = servico.ServicosRecursosFinanceiroCREASMediaMensal.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtMediaMensalExercicio4.Text = mediaMensalExercicio4 != null ? mediaMensalExercicio4.MediaMensal != null ? mediaMensalExercicio4.MediaMensal.ToString() : string.Empty : string.Empty;
        }
        private void DistribuirMediaMensalLA(ServicoRecursoFinanceiroCREASInfo servico)
        {
            ServicoRecursoFinanceiroCREASMediaMensalLAInfo mediaMensalLAExercicio1 = servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtMediaMensalLAExercicio1.Text = mediaMensalLAExercicio1 != null ? mediaMensalLAExercicio1.MediaMensal != null ? mediaMensalLAExercicio1.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASMediaMensalLAInfo mediaMensalLAExercicio2 = servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtMediaMensalLAExercicio2.Text = mediaMensalLAExercicio2 != null ? mediaMensalLAExercicio2.MediaMensal != null ? mediaMensalLAExercicio2.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASMediaMensalLAInfo mediaMensalLAExercicio3 = servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtMediaMensalLAExercicio3.Text = mediaMensalLAExercicio3 != null ? mediaMensalLAExercicio3.MediaMensal != null ? mediaMensalLAExercicio3.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASMediaMensalLAInfo mediaMensalLAExercicio4 = servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtMediaMensalLAExercicio4.Text = mediaMensalLAExercicio4 != null ? mediaMensalLAExercicio4.MediaMensal != null ? mediaMensalLAExercicio4.MediaMensal.ToString() : string.Empty : string.Empty;

        }
        private void DistribuirMediaMensalPSC(ServicoRecursoFinanceiroCREASInfo servico)
        {
            ServicoRecursoFinanceiroCREASMediaMensalPSCInfo mediaMensalPSCExercicio1 = servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtMediaMensalPSCExercicio1.Text = mediaMensalPSCExercicio1 != null ? mediaMensalPSCExercicio1.MediaMensal != null ? mediaMensalPSCExercicio1.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASMediaMensalPSCInfo mediaMensalPSCExercicio2 = servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtMediaMensalPSCExercicio2.Text = mediaMensalPSCExercicio2 != null ? mediaMensalPSCExercicio2.MediaMensal != null ? mediaMensalPSCExercicio2.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASMediaMensalPSCInfo mediaMensalPSCExercicio3 = servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtMediaMensalPSCExercicio3.Text = mediaMensalPSCExercicio3 != null ? mediaMensalPSCExercicio3.MediaMensal != null ? mediaMensalPSCExercicio3.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCREASMediaMensalPSCInfo mediaMensalPSCExercicio4 = servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtMediaMensalPSCExercicio4.Text = mediaMensalPSCExercicio4 != null ? mediaMensalPSCExercicio4.MediaMensal != null ? mediaMensalPSCExercicio4.MediaMensal.ToString() : string.Empty : string.Empty;
        }
        #endregion

        #endregion

        #region [Adicionar |  Salvar | Voltar | Excluir]
        protected void btnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            int idPrograma = Convert.ToInt32(btn.CommandArgument);
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.RemoveAll(x => x.Id == idPrograma);
            if (SessaoTransferenciaRendaCofinanciamentoExercicio != null)
            {
                var transferencia = SessaoTransferenciaRendaCofinanciamentoExercicio.Where(x => x.Id == idPrograma);
                if (transferencia != null)
                {
                    SessaoTransferenciaRendaCofinanciamentoExercicio.RemoveAll(x => x.Id == idPrograma);
                }
            }
            if (SessaoPrefeituraBeneficioEventualServicoExercicio != null)
            {
                var prefeitura = SessaoPrefeituraBeneficioEventualServicoExercicio.Where(x => x.Id == idPrograma);
                if (prefeitura != null)
                {
                    SessaoPrefeituraBeneficioEventualServicoExercicio.RemoveAll(x => x.Id == idPrograma);
                }
            }
            if (SessaoProgramaProjetoCofinanciamentoExercicio != null)
            {
                var programa = SessaoProgramaProjetoCofinanciamentoExercicio.Where(x => x.Id == idPrograma);
                if (programa != null)
                {
                    SessaoProgramaProjetoCofinanciamentoExercicio.RemoveAll(x => x.Id == idPrograma);
                }
            }
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.ToList();
            rptProgramaTemp.DataBind();
        }

        private bool textoVazioNuloComEspaco(string texto)
        {
            if (String.IsNullOrEmpty(texto) || String.IsNullOrWhiteSpace(texto))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();

            String action = "I";
            var servico = new ServicoRecursoFinanceiroCREASInfo();

            try
            {
                var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));

                using (var p = new ProxyRedeProtecaoSocial())
                {
                    var idServico = !String.IsNullOrEmpty(Request.QueryString["id"]) ? Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])) : 0;
                    servico.NumeroAtendidosCentroMensal = p.Service.GetCREASPorId(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]))).NumeroAtendidos;
                    servico.TipoLocal = 3;
                }

                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    servico.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                }

                #region Preencher: Caracterizacao do Servico
                servico.UsuarioTipoServico = new UsuarioTipoServicoInfo();
                if (ddlTipoServico.SelectedIndex != -1)
                    servico.UsuarioTipoServico.IdTipoServico = Convert.ToInt32(ddlTipoServico.SelectedValue);

                servico.UsuarioTipoServico.TipoServico = new TipoServicoInfo();
                if (rblTipoProtecao.SelectedIndex != -1)
                    servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial = Convert.ToInt16(rblTipoProtecao.SelectedValue);

                servico.IdUsuarioTipoServico = Convert.ToInt32(ddlPublicoAlvo.SelectedValue);
                servico.IdCREAS = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
                servico.PossuiTecnicoResponsavel = !chkNaoPossuiTecnicoResponsavel.Checked;
                servico.NomeTecnicoResponsavel = txtTecnicoResponsavel.Text;

                if (ddlTipoServico.SelectedValue == "138" || ddlTipoServico.SelectedValue == R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_145.ToString())
                {
                    servico.IdTipoServicoNaoTipificado = ddlTipoServicoNaoTipificado.SelectedValue != "0" ? Convert.ToInt32(ddlTipoServicoNaoTipificado.SelectedValue) : new Nullable<Int32>();
                    if (servico.IdTipoServicoNaoTipificado.HasValue &&
                        (servico.IdTipoServicoNaoTipificado.Value == 156 || servico.IdTipoServicoNaoTipificado.Value == 159)) //Outro tipo de serviço não tipificado
                    {
                        servico.DescricaoServicoNaoTipificado = txtNaotipificado.Text;
                        servico.ObjetivoServicoNaoTipificado = txtObjetivoNaoTipificado.Text;
                    }
                }

                if (ddlTipoServico.SelectedValue == "153")
                {
                    servico.DescricaoServicoNaoTipificado = txtNaotipificado.Text;
                    servico.ObjetivoServicoNaoTipificado = txtObjetivoNaoTipificado.Text;
                }

                //servico.IdAbrangenciaServico = Convert.ToInt32(ddlAbrangencia.SelectedValue);


                if (ddlAbrangencia.SelectedValue != null)
                {
                    servico.IdAbrangenciaServico = Convert.ToInt32(ddlAbrangencia.SelectedValue);
                }

                if (rblAbrangencia.SelectedValue != null)
                {
                    servico.MunicipioSedeServico = rblAbrangencia.SelectedValue == "1" ? true : false;
                }

                if (ddlFormaJuridica.SelectedValue != null)
                {
                    servico.IdFormaJuridica = Convert.ToInt32(ddlFormaJuridica.SelectedValue);
                }

                servico.IndicaMunicipiosParticipamOfertaServico = txtMunicipioParticipaOferta.Text;

                servico.IndicaMunicipiosSedeServico = txtMunicipioSede.Text;

                servico.IdCaracteristicasTerritorio = Convert.ToInt32(rblCaracteristicasTerritorio.SelectedValue);

                servico.IdCaracteristicaOfertaServico = Convert.ToInt32(rblCaracteristicaOferta.SelectedValue);

                servico.PossuiTecnicoResponsavel = !chkNaoPossuiTecnicoResponsavel.Checked;

                servico.NomeTecnicoResponsavel = txtTecnicoResponsavel.Text;



                #endregion

                var consorcio = new ConsorcioCREASInfo();

                consorcio.IdServicosRecursosFinanceirosCREAS = servico.Id;
                consorcio.NomeConsorcio = txtNomeConsorcio.Text;
                consorcio.MunicipioSede = txtMunicipioSedeConsorcio.Text;
                consorcio.MunicipioEnvolvido = txtMunicipiosEnvolvidos.Text;
                consorcio.CNPJ = txtCNPJConsorcio.Text;

                #endregion

                #region Preencher: caracterização dos usuarios

                if (!String.IsNullOrEmpty(rblMoradiaUsuarios.SelectedValue))
                    servico.IdRegiaoMoradia = Convert.ToInt32(rblMoradiaUsuarios.SelectedValue);

                if (!String.IsNullOrEmpty(rblSexo.SelectedValue))
                    servico.IdSexo = Convert.ToInt32(rblSexo.SelectedValue);

                servico.SituacoesEspecificas = new List<SituacaoEspecificaInfo>();
                foreach (ListItem i in lstSituacoesEspecificas.Items)
                    if (i.Selected)
                        servico.SituacoesEspecificas.Add(new SituacaoEspecificaInfo() { Id = Convert.ToInt32(i.Value) });

                #endregion

                #region Preencher: Funcionamento [Capacidade | Media Mensal]

                if (servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_PROTECAO_SOCIAL_ADOLESC_CUMPR_MEDIDA_SOCIOEDUCATIVA_LA_PSC)
                {

                    #region Carregar: Capacidade  LA
                    servico.ServicosRecursosFinanceiroCREASCapacidadeLA = new List<ServicoRecursoFinanceiroCREASCapacidadeLAInfo>();
                    ServicoRecursoFinanceiroCREASCapacidadeLAInfo servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio1 = new ServicoRecursoFinanceiroCREASCapacidadeLAInfo();
                    servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio1.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio1.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio1.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio1.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[0];

                    ServicoRecursoFinanceiroCREASCapacidadeLAInfo servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio2 = new ServicoRecursoFinanceiroCREASCapacidadeLAInfo();
                    servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio2.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio2.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio2.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio2.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[1];

                    ServicoRecursoFinanceiroCREASCapacidadeLAInfo servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio3 = new ServicoRecursoFinanceiroCREASCapacidadeLAInfo();
                    servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio3.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio3.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio3.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio3.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[2];

                    ServicoRecursoFinanceiroCREASCapacidadeLAInfo servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio4 = new ServicoRecursoFinanceiroCREASCapacidadeLAInfo();
                    servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio4.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio4.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio4.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio4.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[3];

                    servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Add(servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio1);
                    servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Add(servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio2);
                    servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Add(servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio3);
                    servico.ServicosRecursosFinanceiroCREASCapacidadeLA.Add(servicoRecursoFinanceiroCREASCapacidadeLAInfoExercicio4);
                    #endregion

                    #region Carregar: Capacidade PSC
                    servico.ServicosRecursosFinanceiroCREASCapacidadePSC = new List<ServicoRecursoFinanceiroCREASCapacidadePSCInfo>();
                    ServicoRecursoFinanceiroCREASCapacidadePSCInfo servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio1 = new ServicoRecursoFinanceiroCREASCapacidadePSCInfo();
                    servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio1.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio1.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio1.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio1.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[0];

                    ServicoRecursoFinanceiroCREASCapacidadePSCInfo servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio2 = new ServicoRecursoFinanceiroCREASCapacidadePSCInfo();
                    servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio2.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio2.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio2.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio2.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[1];

                    ServicoRecursoFinanceiroCREASCapacidadePSCInfo servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio3 = new ServicoRecursoFinanceiroCREASCapacidadePSCInfo();
                    servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio3.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio3.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio3.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio3.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[2];

                    ServicoRecursoFinanceiroCREASCapacidadePSCInfo servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio4 = new ServicoRecursoFinanceiroCREASCapacidadePSCInfo();
                    servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio4.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio4.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio4.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio4.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[3];

                    servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Add(servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio1);
                    servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Add(servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio2);
                    servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Add(servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio3);
                    servico.ServicosRecursosFinanceiroCREASCapacidadePSC.Add(servicoRecursoFinanceiroCREASCapacidadePSCInfoExercicio4);
                    #endregion

                    #region Carregar: MM LA
                    servico.ServicosRecursosFinanceiroCREASMediaMensalLA = new List<ServicoRecursoFinanceiroCREASMediaMensalLAInfo>();

                    #region Exercicio 1
                    ServicoRecursoFinanceiroCREASMediaMensalLAInfo servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio1 = new ServicoRecursoFinanceiroCREASMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio1.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio1.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio1.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio1.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[0];
                    #endregion

                    #region Exercicio 2
                    ServicoRecursoFinanceiroCREASMediaMensalLAInfo servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio2 = new ServicoRecursoFinanceiroCREASMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio2.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio2.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio2.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio2.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[1];
                    #endregion

                    #region Exercicio 3
                    ServicoRecursoFinanceiroCREASMediaMensalLAInfo servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio3 = new ServicoRecursoFinanceiroCREASMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio3.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio3.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio3.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio3.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[2];
                    #endregion

                    #region Exercicio 4
                    ServicoRecursoFinanceiroCREASMediaMensalLAInfo servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio4 = new ServicoRecursoFinanceiroCREASMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio4.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio4.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio4.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio4.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[3];
                    #endregion

                    servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Add(servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio1);
                    servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Add(servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio2);
                    servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Add(servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio3);
                    servico.ServicosRecursosFinanceiroCREASMediaMensalLA.Add(servicoRecursoFinanceiroCREASMediaMensalLAInfoExercicio4);
                    #endregion

                    #region Carregar: MM PSC
                    servico.ServicosRecursosFinanceiroCREASMediaMensalPSC = new List<ServicoRecursoFinanceiroCREASMediaMensalPSCInfo>();

                    #region Exercicio 1
                    ServicoRecursoFinanceiroCREASMediaMensalPSCInfo servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio1 = new ServicoRecursoFinanceiroCREASMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio1.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio1.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio1.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio1.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[0];
                    #endregion

                    #region Exercicio 2
                    ServicoRecursoFinanceiroCREASMediaMensalPSCInfo servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio2 = new ServicoRecursoFinanceiroCREASMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio2.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio2.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio2.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio2.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[1];
                    #endregion

                    #region Exercicio 3
                    ServicoRecursoFinanceiroCREASMediaMensalPSCInfo servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio3 = new ServicoRecursoFinanceiroCREASMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio3.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio3.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio3.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio3.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[2];
                    #endregion

                    #region Exercicio 4
                    ServicoRecursoFinanceiroCREASMediaMensalPSCInfo servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio4 = new ServicoRecursoFinanceiroCREASMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio4.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio4.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio4.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio4.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[3];
                    #endregion

                    servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Add(servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio1);
                    servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Add(servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio2);
                    servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Add(servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio3);
                    servico.ServicosRecursosFinanceiroCREASMediaMensalPSC.Add(servicoRecursoFinanceiroCREASMediaMensalPSCInfoExercicio4);
                    #endregion
                }
                else
                {

                    #region Carregar: Capacidade
                    servico.ServicosRecursosFinanceiroCREASCapacidade = new List<ServicoRecursoFinanceiroCREASCapacidadeInfo>();
                    ServicoRecursoFinanceiroCREASCapacidadeInfo servicoRecursoFinanceiroCREASCapacidadeInfoExercicio1 = new ServicoRecursoFinanceiroCREASCapacidadeInfo();
                    servicoRecursoFinanceiroCREASCapacidadeInfoExercicio1.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio1.Text) ? Convert.ToInt32(txtCapacidadeExercicio1.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadeInfoExercicio1.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[0];

                    ServicoRecursoFinanceiroCREASCapacidadeInfo servicoRecursoFinanceiroCREASCapacidadeInfoExercicio2 = new ServicoRecursoFinanceiroCREASCapacidadeInfo();
                    servicoRecursoFinanceiroCREASCapacidadeInfoExercicio2.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio2.Text) ? Convert.ToInt32(txtCapacidadeExercicio2.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadeInfoExercicio2.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[1];

                    ServicoRecursoFinanceiroCREASCapacidadeInfo servicoRecursoFinanceiroCREASCapacidadeInfoExercicio3 = new ServicoRecursoFinanceiroCREASCapacidadeInfo();
                    servicoRecursoFinanceiroCREASCapacidadeInfoExercicio3.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio3.Text) ? Convert.ToInt32(txtCapacidadeExercicio3.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadeInfoExercicio3.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[2];

                    ServicoRecursoFinanceiroCREASCapacidadeInfo servicoRecursoFinanceiroCREASCapacidadeInfoExercicio4 = new ServicoRecursoFinanceiroCREASCapacidadeInfo();
                    servicoRecursoFinanceiroCREASCapacidadeInfoExercicio4.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio4.Text) ? Convert.ToInt32(txtCapacidadeExercicio4.Text) : 0;
                    servicoRecursoFinanceiroCREASCapacidadeInfoExercicio4.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[3];

                    servico.ServicosRecursosFinanceiroCREASCapacidade.Add(servicoRecursoFinanceiroCREASCapacidadeInfoExercicio1);
                    servico.ServicosRecursosFinanceiroCREASCapacidade.Add(servicoRecursoFinanceiroCREASCapacidadeInfoExercicio2);
                    servico.ServicosRecursosFinanceiroCREASCapacidade.Add(servicoRecursoFinanceiroCREASCapacidadeInfoExercicio3);
                    servico.ServicosRecursosFinanceiroCREASCapacidade.Add(servicoRecursoFinanceiroCREASCapacidadeInfoExercicio4);
                    #endregion

                    #region Carregar: MM
                    servico.ServicosRecursosFinanceiroCREASMediaMensal = new List<ServicoRecursoFinanceiroCREASMediaMensalInfo>();
                    #region Exercicio 1
                    ServicoRecursoFinanceiroCREASMediaMensalInfo servicoRecursoFinanceiroCREASMediaMensalInfoExercicio1 = new ServicoRecursoFinanceiroCREASMediaMensalInfo();

                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio1.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalInfoExercicio1.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio1.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalInfoExercicio1.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[0];
                    #endregion

                    #region Exercicio 2
                    ServicoRecursoFinanceiroCREASMediaMensalInfo servicoRecursoFinanceiroCREASMediaMensalInfoExercicio2 = new ServicoRecursoFinanceiroCREASMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio2.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalInfoExercicio2.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio2.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalInfoExercicio2.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[1];
                    #endregion

                    #region Exercicio 3
                    ServicoRecursoFinanceiroCREASMediaMensalInfo servicoRecursoFinanceiroCREASMediaMensalInfoExercicio3 = new ServicoRecursoFinanceiroCREASMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio3.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalInfoExercicio3.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio3.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalInfoExercicio3.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[2];
                    #endregion

                    #region Exercicio 4
                    ServicoRecursoFinanceiroCREASMediaMensalInfo servicoRecursoFinanceiroCREASMediaMensalInfoExercicio4 = new ServicoRecursoFinanceiroCREASMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio4.Text))
                    {
                        servicoRecursoFinanceiroCREASMediaMensalInfoExercicio4.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio4.Text);
                    }
                    servicoRecursoFinanceiroCREASMediaMensalInfoExercicio4.Exercicio = FServicoRecursoFinanceiroCREAS.Exercicios[3];
                    #endregion

                    servico.ServicosRecursosFinanceiroCREASMediaMensal.Add(servicoRecursoFinanceiroCREASMediaMensalInfoExercicio1);
                    servico.ServicosRecursosFinanceiroCREASMediaMensal.Add(servicoRecursoFinanceiroCREASMediaMensalInfoExercicio2);
                    servico.ServicosRecursosFinanceiroCREASMediaMensal.Add(servicoRecursoFinanceiroCREASMediaMensalInfoExercicio3);
                    servico.ServicosRecursosFinanceiroCREASMediaMensal.Add(servicoRecursoFinanceiroCREASMediaMensalInfoExercicio4);
                    #endregion

                }



                #endregion

                #region Preencher: Funcionamento[Hora|Qtd|Atividade]
                servico.IdHorasSemana = Convert.ToInt32(rblHorasSemana.SelectedValue);
                servico.QuantidadeDiasSemana = Convert.ToInt32(rblDiasSemana.SelectedValue);
                #endregion

                #region Preencher: Atividades Socioassistenciais
                servico.AtividadesSocioAssistenciais = new List<AtividadeSocioAssistencialInfo>();
                foreach (ListItem atividade in lstAtividades.Items)
                {
                    if (atividade.Selected)
                    {
                        servico.AtividadesSocioAssistenciais.Add(new AtividadeSocioAssistencialInfo() { Id = Convert.ToInt32(atividade.Value) });
                    }
                }

                #endregion

                #region Preencher: Avaliacao do Servico
                if (!String.IsNullOrEmpty(rblAvaliacaoGestor.SelectedValue))
                {
                    servico.IdAvaliacaoServico = Convert.ToInt32(rblAvaliacaoGestor.SelectedValue);
                }
                #endregion

                #region Preencher: Recursos Financeiros

                #region Exercicio 1
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroCREAS.Exercicios[0])
                {
                    ServicoRecursoFinanceiroFundosCREASInfo fundo = new ServicoRecursoFinanceiroFundosCREASInfo();
                    fundo.ServicoRecursoFinanceiroCREASInfoId = servico.Id;
                    fundo.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio1.Text)) ? Convert.ToDecimal(txtFEASExercicio1.Text) : 0M;
                    fundo.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio1.Text)) ? Convert.ToDecimal(txtFEDCAExercicio1.Text) : 0M;
                    fundo.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio1.Text)) ? Convert.ToDecimal(txtFMASExercicio1.Text) : 0M;
                    fundo.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio1.Text)) ? Convert.ToDecimal(txtFMDCAExercicio1.Text) : 0M;
                    fundo.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio1.Text)) ? Convert.ToDecimal(txtFNASExercicio1.Text) : 0M;
                    fundo.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio1.Text)) ? Convert.ToDecimal(txtFNDCAExercicio1.Text) : 0M;
                    fundo.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio1.Text) ? Convert.ToDecimal(txtFMIExercicio1.Text) : 0M;
                    fundo.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio1.Text) ? Convert.ToDecimal(txtFEIExercicio1.Text) : 0M;
                    fundo.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio1.Text) ? Convert.ToDecimal(txtFNIExercicio1.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio1.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio1.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio1.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio1.Text) : 0M;
                    fundo.ValorEstadualAssistenciaAnoAnterior = Convert.ToDecimal(txtFEASAnoAnteriorExercicio1.Text);
                    //Define o merge (adiciona quando nao encontra / atualiza quando encontra)
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio1.Text) ? string.Empty : txtObjetoDemandaExercicio1.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio1.Text) ? string.Empty : txtCodigoDemandaExercicio1.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio1.Text) ? Convert.ToDecimal(txtValorContraExercicio1.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;

                    servico.ServicosRecursosFinanceirosFundosCREASInfo = servico.ServicosRecursosFinanceirosFundosCREASInfo ?? new List<ServicoRecursoFinanceiroFundosCREASInfo>();
                    servico.ServicosRecursosFinanceirosFundosCREASInfo.Add(fundo);

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio1.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        fundo.ServicoRecursoFinanceiroCREASFontesRecursosInfo = SessaoFontesRecursosExercicio1;
                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroCREASFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosCREAS = fundo.Id;
                        }
                    }
                    else
                    {

                    }

                }
                #endregion

                #region Exercicio 2
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroCREAS.Exercicios[1])
                {
                    ServicoRecursoFinanceiroFundosCREASInfo fundo = new ServicoRecursoFinanceiroFundosCREASInfo();
                    fundo.ServicoRecursoFinanceiroCREASInfoId = servico.Id;
                    fundo.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio2.Text)) ? Convert.ToDecimal(txtFEASExercicio2.Text) : 0M;
                    fundo.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio2.Text)) ? Convert.ToDecimal(txtFEDCAExercicio2.Text) : 0M;
                    fundo.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio2.Text)) ? Convert.ToDecimal(txtFMASExercicio2.Text) : 0M;
                    fundo.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio2.Text)) ? Convert.ToDecimal(txtFMDCAExercicio2.Text) : 0M;
                    fundo.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio2.Text)) ? Convert.ToDecimal(txtFNASExercicio2.Text) : 0M;
                    fundo.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio2.Text)) ? Convert.ToDecimal(txtFNDCAExercicio2.Text) : 0M;
                    fundo.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio2.Text) ? Convert.ToDecimal(txtFMIExercicio2.Text) : 0M;
                    fundo.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio2.Text) ? Convert.ToDecimal(txtFEIExercicio2.Text) : 0M;
                    fundo.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio2.Text) ? Convert.ToDecimal(txtFNIExercicio2.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio2.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio2.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio2.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio2.Text) : 0M;
                    fundo.ValorEstadualAssistenciaAnoAnterior = !String.IsNullOrEmpty(txtFEASAnoAnteriorExercicio2.Text) ? Convert.ToDecimal(txtFEASAnoAnteriorExercicio2.Text) : 0M;
                    //Define o merge (adiciona quando nao encontra / atualiza quando encontra)
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio2.Text) ? string.Empty : txtObjetoDemandaExercicio2.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio2.Text) ? string.Empty : txtCodigoDemandaExercicio2.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio2.Text) ? Convert.ToDecimal(txtValorContraExercicio2.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;

                    var totalLinhasExercicio2 = lstRecursosAdicionadosExercicio2.Items.Count;

                    servico.ServicosRecursosFinanceirosFundosCREASInfo = servico.ServicosRecursosFinanceirosFundosCREASInfo ?? new List<ServicoRecursoFinanceiroFundosCREASInfo>();
                    servico.ServicosRecursosFinanceirosFundosCREASInfo.Add(fundo);

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio2.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        if (totalLinhasExercicio2 > 0)
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(true);
                        else
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(false);

                        fundo.ServicoRecursoFinanceiroCREASFontesRecursosInfo = this.SessaoFontesRecursosExercicio2;
                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroCREASFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosCREAS = fundo.Id;
                        }
                    }
                }
                #endregion

                #region Exercicio 3

                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroCREAS.Exercicios[2])
                {
                    ServicoRecursoFinanceiroFundosCREASInfo fundo = new ServicoRecursoFinanceiroFundosCREASInfo();
                    fundo.ServicoRecursoFinanceiroCREASInfoId = servico.Id;
                    fundo.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio3.Text)) ? Convert.ToDecimal(txtFEASExercicio3.Text) : 0M;
                    fundo.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio3.Text)) ? Convert.ToDecimal(txtFEDCAExercicio3.Text) : 0M;
                    fundo.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio3.Text)) ? Convert.ToDecimal(txtFMASExercicio3.Text) : 0M;
                    fundo.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio3.Text)) ? Convert.ToDecimal(txtFMDCAExercicio3.Text) : 0M;
                    fundo.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio3.Text)) ? Convert.ToDecimal(txtFNASExercicio3.Text) : 0M;
                    fundo.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio3.Text)) ? Convert.ToDecimal(txtFNDCAExercicio3.Text) : 0M;
                    fundo.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio3.Text) ? Convert.ToDecimal(txtFMIExercicio3.Text) : 0M;
                    fundo.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio3.Text) ? Convert.ToDecimal(txtFEIExercicio3.Text) : 0M;
                    fundo.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio3.Text) ? Convert.ToDecimal(txtFNIExercicio3.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio3.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio3.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio3.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio3.Text) : 0M;
                    fundo.ValorEstadualAssistenciaAnoAnterior = !String.IsNullOrEmpty(txtFEASAnoAnteriorExercicio3.Text) ? Convert.ToDecimal(txtFEASAnoAnteriorExercicio3.Text) : 0M;
                    //Define o merge (adiciona quando nao encontra / atualiza quando encontra)
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio3.Text) ? string.Empty : txtObjetoDemandaExercicio3.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio3.Text) ? string.Empty : txtCodigoDemandaExercicio3.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio3.Text) ? Convert.ToDecimal(txtValorContraExercicio3.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida3.SelectedValue == "1" ? true : false;

                    var totalLinhasExercicio3 = lstRecursosAdicionadosExercicio3.Items.Count;

                    servico.ServicosRecursosFinanceirosFundosCREASInfo = servico.ServicosRecursosFinanceirosFundosCREASInfo ?? new List<ServicoRecursoFinanceiroFundosCREASInfo>();
                    servico.ServicosRecursosFinanceirosFundosCREASInfo.Add(fundo);

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio3.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        if (totalLinhasExercicio3 > 0)
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(true);
                        else
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(false);

                        fundo.ServicoRecursoFinanceiroCREASFontesRecursosInfo = this.SessaoFontesRecursosExercicio3;
                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroCREASFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosCREAS = fundo.Id;
                        }
                    }
                }
                #endregion

                #region Exercicio 4

                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroCREAS.Exercicios[3])
                {
                    ServicoRecursoFinanceiroFundosCREASInfo fundo = new ServicoRecursoFinanceiroFundosCREASInfo();
                    fundo.ServicoRecursoFinanceiroCREASInfoId = servico.Id;
                    fundo.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio4.Text)) ? Convert.ToDecimal(txtFEASExercicio4.Text) : 0M;
                    fundo.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio4.Text)) ? Convert.ToDecimal(txtFEDCAExercicio4.Text) : 0M;
                    fundo.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio4.Text)) ? Convert.ToDecimal(txtFMASExercicio4.Text) : 0M;
                    fundo.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio4.Text)) ? Convert.ToDecimal(txtFMDCAExercicio4.Text) : 0M;
                    fundo.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio4.Text)) ? Convert.ToDecimal(txtFNASExercicio4.Text) : 0M;
                    fundo.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio4.Text)) ? Convert.ToDecimal(txtFNDCAExercicio4.Text) : 0M;
                    fundo.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio4.Text) ? Convert.ToDecimal(txtFMIExercicio4.Text) : 0M;
                    fundo.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio4.Text) ? Convert.ToDecimal(txtFEIExercicio4.Text) : 0M;
                    fundo.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio4.Text) ? Convert.ToDecimal(txtFNIExercicio4.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio4.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio4.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio4.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio4.Text) : 0M;
                    fundo.ValorEstadualAssistenciaAnoAnterior = !String.IsNullOrEmpty(txtFEASAnoAnteriorExercicio4.Text) ? Convert.ToDecimal(txtFEASAnoAnteriorExercicio4.Text) : 0M;
                    //Define o merge (adiciona quando nao encontra / atualiza quando encontra)
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio4.Text) ? string.Empty : txtObjetoDemandaExercicio4.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio4.Text) ? string.Empty : txtCodigoDemandaExercicio4.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio4.Text) ? Convert.ToDecimal(txtValorContraExercicio4.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida4.SelectedValue == "1" ? true : false;

                    var totalLinhasExercicio4 = lstRecursosAdicionadosExercicio4.Items.Count;

                    servico.ServicosRecursosFinanceirosFundosCREASInfo = servico.ServicosRecursosFinanceirosFundosCREASInfo ?? new List<ServicoRecursoFinanceiroFundosCREASInfo>();
                    servico.ServicosRecursosFinanceirosFundosCREASInfo.Add(fundo);

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio4.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        if (totalLinhasExercicio4 > 0)
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(true);
                        else
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(false);

                        fundo.ServicoRecursoFinanceiroCREASFontesRecursosInfo = this.SessaoFontesRecursosExercicio4;
                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroCREASFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosCREAS = fundo.Id;
                        }
                    }
                }
                #endregion


                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (Convert.ToBoolean(Convert.ToInt32(rblIntegracaoRede.SelectedIndex != -1 ? false : true)))
                    {
                        new ValidadorServicoRecursoFinanceiro().ValidarServicoCREAS(servico);
                    }
                }

                #endregion




                #region Preencher: [Data Funcionamento]
                DateTime dt;
                if (!String.IsNullOrEmpty(txtDataInicio.Text) && DateTime.TryParse(txtDataInicio.Text, out dt))
                {
                    servico.DataFuncionamentoServico = Convert.ToDateTime(txtDataInicio.Text);
                }
                #endregion

                #region Preencher: Integracao
                if (servico.Id != 0)
                {
                    if (rblIntegracaoRede.SelectedIndex == -1)
                    {
                        throw new Exception("Informe se o usuário deste serviço é beneficiário de algum programa, projeto ou benefício!");
                    }
                    else
                    {
                        servico.PossuiProgramaBeneficio = Convert.ToBoolean(Convert.ToInt32(rblIntegracaoRede.SelectedValue));
                    }
                }

                #endregion

                #region Aplicar: [Validacao]
                new ValidadorServicoRecursoFinanceiro().ValidarServicoCREAS(servico);

                var recursosHumanos = CarregarRH();
                var validaRh = new ValidadorRecursosHumanos().ValidaCREAS(recursosHumanos);
                if (validaRh.Count > 0)
                {
                    throw new Exception(Extensions.Concat(validaRh, System.Environment.NewLine));
                }
                #endregion

                #region Aplicar Ação [Incluir/Alterar]
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    if (servico.Id == 0)
                    {
                        proxy.Service.AddServicoRecursoFinanceiroCREAS(servico);
                        recursosHumanos.IdServicosRecursosFinanceirosCREAS = servico.Id;

                        if (recursosHumanos.Id == 0)
                        {
                            proxy.Service.AddServicoRecursoFinanceiroCREASRH(recursosHumanos);
                        }


                        consorcio.IdServicosRecursosFinanceirosCREAS = servico.Id;
                        if (!String.IsNullOrEmpty(txtNomeConsorcio.Text) || !String.IsNullOrEmpty(txtMunicipioSedeConsorcio.Text) || !String.IsNullOrEmpty(txtMunicipiosEnvolvidos.Text) || !String.IsNullOrEmpty(txtCNPJConsorcio.Text))
                        {

                        #region Adicionar: Programa e Projeto
                        if (SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio != null)
                        {
                            foreach (var item in SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio)
                            {
                                if (item.Nome.ToLower().Contains("beneficio de prestação continuada - bpc idosos")
                                      || item.Nome.ToLower().Contains("beneficio de prestação continuada - bpc pessoas com deficiência")
                                      || item.Nome.Contains("Auxílio Brasil")
                                      || item.Nome.ToLower().Contains("peti")
                                      || item.Nome.ToLower().Contains("ação jovem")
                                      || item.Nome.ToLower().Contains("renda cidadã")
                                      || item.Nome.ToLower().Contains("teste transferencia renda")
                                      )
                                {
                                    AdicionaTransferenciaRenda(idCentro, servico.Id, item.IdProgramaProjeto);

                                }
                                else if (item.Nome.ToLower().Contains("auxílio natalidade")
                                   || item.Nome.ToLower().Contains("auxílio funeral")
                                   || item.Nome.ToLower().Contains("calamidades públicas e emergências")
                                   || item.Nome.ToLower().Contains("vulnerabilidade temporária")
                               )
                                {
                                    AdicionaPrefeituraBeneficio(idCentro, servico.Id, item.IdProgramaProjeto);
                                }
                                else
                                {
                                    AdicionarProgramaConfinamento(idCentro, servico.Id, item.IdProgramaProjeto);
                                }

                            }
                        }
                        #endregion

                        this.ClearSessao();
                        this.ClearPrograma();
                            using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
                            {
                                proxySocial.Service.SalvarConsorcioCREAS(consorcio);
                            }
                        }

                        action = "I";
                    }
                    else
                    {
                        proxy.Service.UpdateServicoRecursoFinanceiroCREAS(servico);

                        #region Atualizar: Recursos Humanos
                        recursosHumanos.IdServicosRecursosFinanceirosCREAS = servico.Id;
                        new ValidadorRecursosHumanos().ValidarRHCREAS(recursosHumanos);
                        if (recursosHumanos.Id == 0)
                        {
                            proxy.Service.AddServicoRecursoFinanceiroCREASRH(recursosHumanos);
                        }
                        else
                        {
                            proxy.Service.UpdateServicoRecursoFinanceiroCREASRH(recursosHumanos);
                        }
                        #endregion

                        consorcio.IdServicosRecursosFinanceirosCREAS = servico.Id;
                        if (!String.IsNullOrEmpty(txtNomeConsorcio.Text) || !String.IsNullOrEmpty(txtMunicipioSedeConsorcio.Text) || !String.IsNullOrEmpty(txtMunicipiosEnvolvidos.Text) || !String.IsNullOrEmpty(txtCNPJConsorcio.Text))
                        {
                            using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
                            {
                                proxySocial.Service.SalvarConsorcioCREAS(consorcio);
                            }
                        }

                        using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
                        {
                            proxySocial.Service.SalvarConsorcioCREAS(consorcio);
                        }

                        action = "A";
                    }
                }
                #endregion

                #region Programa Beneficios

                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    // caso usuario não beneficiário de algum programa, projeto ou benefício, limpar serviços
                    if (!Convert.ToBoolean(Convert.ToInt32(rblIntegracaoRede.SelectedValue)))
                    {
                        var idServicoBeneficio = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                        using (var ProxyProgramas = new ProxyProgramas())
                        {
                            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
                            var programas = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(idServicoBeneficio, servico.IdCREAS);

                            var programasAgrupado = programas
                                                             .OrderBy(t => t.IdTipoProtecao)
                                                             .GroupBy(s => s.ProtecaoSocial)
                                                             .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) });

                            foreach (var programaAgrupado in programasAgrupado)
                            {
                                foreach (var item in programaAgrupado.Items)
                                {
                                    ProxyProgramas.Service.DeleteProgramaProjetoCofinanciamento(Convert.ToInt32(item.Id), Convert.ToInt32(item.TipoCofinanciamento));
                                }

                            }

                            trRendaCidadaBeneficioIdoso.Visible = true;
                            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
                            lstRecursos.DataSource = programasAgrupado;
                            lstRecursos.DataBind();
                        }
                    }
                }
                SessaoFontesRecursosExercicio1 = null;
                SessaoFontesRecursosExercicio2 = null;
                SessaoFontesRecursosExercicio3 = null;
                SessaoFontesRecursosExercicio4 = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.ToString()), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }
                #endregion

            #region Exibir Mensagem Operação [Incluir/Alterar] (Obs.:Reload de tela, modelo a ser mudado)
            var id = servico.Id;
            var idCentro1 = Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]);
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            if (action == "I")
            {
                Response.Redirect("~/BlocoIII/FServicoRecursoFinanceiroCREAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(servico.Id.ToString())) + "&idCentro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro1)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)) + "&msg=" + action);
            }
            else if (action == "A")
            {
                Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCREAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro1)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)) + "&msg=" + action);
            }
            #endregion

        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            lblInconsistencias.Text = String.Empty;
            tbInconsistencias.Visible = false;
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
            try
            {
                if (SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio != null)
                {
                    var existe = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.Where(x => x.IdProgramaProjeto == Convert.ToInt32(ddlProgramaBeneficio.SelectedValue)).ToList();
                    if (existe.Count > 0)
                    {
                        throw new Exception(String.Concat("Programa já existente, caso deseja é possivel remover e inserir novamente.", System.Environment.NewLine));
                    }
                }

                if (ddlTipoServico.SelectedValue == "0")
                {
                    throw new Exception(String.Concat("Informe o tipo de serviço.", System.Environment.NewLine));
                }

                if (ddlPublicoAlvo.SelectedValue == "0")
                {
                    throw new Exception(String.Concat("Informe o tipo de beneficiários deste serviço.", System.Environment.NewLine));
                }
                if (trRendaCidadaBeneficioIdoso.Visible && String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
                {
                    throw new Exception(String.Concat("Informe o numero de beneficiários deste serviço.", System.Environment.NewLine));
                }
                if (ddlProgramaBeneficio.SelectedValue != "0")
                {
                    if (ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("beneficio de prestação continuada - bpc idosos")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("beneficio de prestação continuada - bpc pessoas com deficiência")
                        || ddlProgramaBeneficio.SelectedItem.Text.Contains("BOLSA FAMÍLIA")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("peti")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("ação jovem")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("renda cidadã")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("teste transferencia renda")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("fortalecimento da vigilância socioassistencial")
                        )
                    {
                        AtualizaTransferenciaRenda(idCentro);
                    }
                    else if (ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("auxílio natalidade")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("auxílio funeral")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("calamidades públicas e emergências")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("vulnerabilidade temporária")
                    )
                    {
                        AtualizaPrefeituraBeneficio(idCentro);
                    }
                    else
                    {
                        AtualizaProgramaConfinamento(idCentro);
                    }
                }
                else
                {
                    throw new Exception(String.Concat("Informe um programa ou benefício para este serviço.", System.Environment.NewLine));
                }

            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idCentro = Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]);
            Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCREAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro)) + "&idUnidade=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }
 

        protected void ddlTipoServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefinirFrame1ComoPrincipal();

            txtCapacidadePSCExercicio1.Text = String.Empty;
            txtCapacidadePSCExercicio2.Text = String.Empty;
            txtCapacidadePSCExercicio3.Text = String.Empty;
            txtCapacidadePSCExercicio4.Text = String.Empty;

            SessaoPmas.VerificarSessao(this);
            lblCapacidade.Text = String.Empty;
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarUsuarios(proxy);
                CarregarAtividades(proxy);
                CarregarTiposServicosNaoTipificados(proxy);
            }

            tbNaoTipificado.Visible = ddlTipoServico.SelectedValue == R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_145.ToString();
            AplicarRegraExibicaoLayoutServicosChanged();
            this.ClearSituacoes();
        }

        protected void btnSalvarRecursoPrograma_Click(object sender, EventArgs e)
        {
            this.btnSalvar_Click(sender, e);
        }
        protected void rblEstadualizado_SelectedIndexChanged(object sender, EventArgs e)
        {
            trValorEstadualizado.Visible = rblEstadualizado.SelectedValue == "1";
        }
        protected void rblTipoProtecao_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarTiposServicos(proxy, true);
                CarregarTiposServicosNaoTipificados(proxy);
                tbNaoTipificado.Visible = false;
                tbNaoTipificadoDetalhado.Visible = false;
                txtNaotipificado.Text = "";
                txtObjetivoNaoTipificado.Text = "";
            }

            if (rblTipoProtecao.SelectedValue == "2")
            {
                CarregarOfertaServico(true);
            }
            else
            {
                CarregarOfertaServico(false);
            }		
        }
        protected void chkNaoPossuiTecnicoResponsavel_CheckedChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            txtTecnicoResponsavel.Enabled = !chkNaoPossuiTecnicoResponsavel.Checked;
            if (!chkNaoPossuiTecnicoResponsavel.Checked)
            {
                this.Master.ScriptManagerControl.SetFocus(txtTecnicoResponsavel);
            }
            else
            {
                txtTecnicoResponsavel.Text = String.Empty;
            }
        }
        protected void ddlPublicoAlvo_SelectedIndexChanged(object sender, EventArgs e)
        {

            DefinirFrame1ComoPrincipal();
            SessaoPmas.VerificarSessao(this);

            #region Exercicio 1
            txtMediaMensalExercicio1.Text = String.Empty;
            txtMediaMensalPSCExercicio1.Text = String.Empty;

            txtCapacidadeExercicio1.Text = String.Empty;
            txtCapacidadePSCExercicio1.Text = String.Empty;
            #endregion

            #region Exercicio 2
            txtMediaMensalExercicio2.Text = String.Empty;
            txtMediaMensalPSCExercicio2.Text = String.Empty;

            txtCapacidadeExercicio2.Text = String.Empty;
            txtCapacidadePSCExercicio2.Text = String.Empty;
            #endregion


            #region Exercicio 3
            txtMediaMensalExercicio3.Text = String.Empty;
            txtMediaMensalPSCExercicio3.Text = String.Empty;

            txtCapacidadeExercicio3.Text = String.Empty;
            txtCapacidadePSCExercicio3.Text = String.Empty;
            #endregion

            #region Exercicio 4
            txtMediaMensalExercicio4.Text = String.Empty;
            txtMediaMensalPSCExercicio4.Text = String.Empty;

            txtCapacidadeExercicio4.Text = String.Empty;
            txtCapacidadePSCExercicio4.Text = String.Empty;
            #endregion


            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                if (ddlTipoServico.SelectedValue == R_TIPO_SERVICO.SERVICO_PROTECAO_ATENDIMENTO_ESPECIALIZADO_FAMILIAS_INDIVIDUOS_PAEFI.ToString() && ddlPublicoAlvo.SelectedValue == "18" || ddlTipoServicoNaoTipificado.SelectedValue == "159" && ddlPublicoAlvo.SelectedValue == "87")
                {
                    lblCapacidade.Text = "famílias";
                    lblMediaMensal.Text = "famílias";
                }
                else
                {
                    if (ddlPublicoAlvo.SelectedValue != "0")
                    {
                        lblCapacidade.Text = "pessoas";
                        lblMediaMensal.Text = "pessoas";
                    }
                    //lblAtendidosMensalPSC.Text = 
                }
                if (ddlTipoServico.SelectedValue == R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_145.ToString() && ddlTipoServicoNaoTipificado.SelectedItem.Text == "Outro")
                    tbNaoTipificadoDetalhado.Visible = true;
                else
                {
                    tbNaoTipificadoDetalhado.Visible = false;
                    txtNaotipificado.Text = txtObjetivoNaoTipificado.Text = String.Empty;
                }
                CarregarSituacoes(proxy);
            }
        }

        protected void ddlTipoServicoNaoTipificado_SelectedIndexChanged(object sender, EventArgs e) //PMAS 2016
        {
            DefinirFrame1ComoPrincipal();

            SessaoPmas.VerificarSessao(this);

            if (ddlTipoServicoNaoTipificado.SelectedItem.Text == "Outro")
            {
                tbNaoTipificadoDetalhado.Visible = true;
            }
            else
            {
                tbNaoTipificadoDetalhado.Visible = false;
                txtNaotipificado.Text = "";
                txtObjetivoNaoTipificado.Text = "";
            }

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarUsuarios(proxy, true);
                CarregarAtividades(proxy, true);
            }

            //LIMPAR SITUAÇÕES
            lstSituacoesEspecificas.DataTextField = "Nome";
            lstSituacoesEspecificas.DataValueField = "Id";
            lstSituacoesEspecificas.DataSource = new List<SituacaoEspecificaInfo>();
            lstSituacoesEspecificas.DataBind();
        }
        protected void rblIntegracaoRede_SelectedIndexChanged(object sender, EventArgs e)
        {

            DefinirFrame1ComoPrincipal();

            trProgramasBeneficios.Visible = false;
            if (rblIntegracaoRede.SelectedValue == "1")
            {
                trProgramasBeneficios.Visible = true;
                using (var proxy = new ProxyProgramas())
                {
                    CarregarProgramas(proxy);
                }
            }
        }
        protected void ddlProgramaBeneficio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgramaBeneficio.SelectedValue != "0" && ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("amigo do idoso"))
            {
                lblBenificiarios.Text = "Quantos beneficiários do Renda Cidadã - Benefício Idoso são usuários deste serviço?";
                trRendaCidadaBeneficioIdoso.Visible = true;
                //lstRecursosAmigoIdoso.DataSource = lst;
                //lstRecursosAmigoIdoso.DataBind();
            }
            else
            {
                trRendaCidadaBeneficioIdoso.Visible = true;
                lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
            }
        }
        protected void lstItems_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Permissao.VerificarPermissaoControles(new[] { ((ImageButton)e.Item.FindControl("btnExcluir")) }, Session);
            }
        }
        protected void lstRecursos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var idServicosRecursosFinanceiros = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
            if (e.CommandName == "Excluir")
            {
                using (var proxy = new ProxyProgramas())
                {
                    //   var tipoCofinanciamento = Convert.ToInt32(e.CommandArgument);

                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string id = commandArgs[0];
                    string tipoCofinanciamento = commandArgs[1];
                    proxy.Service.DeleteProgramaProjetoCofinanciamento(Convert.ToInt32(id), Convert.ToInt32(tipoCofinanciamento));
                    LoadProgramas(proxy, idServicosRecursosFinanceiros, idCentro);
                }
            }
        }

        #region Evento [Helper]
        private void AplicarRegraExibicaoLayoutServicosChanged()
        {
            #region Servico [LA PSC]
            bool oServicoEhDeLAPSC = (ddlTipoServico.SelectedValue == R_TIPO_SERVICO.SERVICO_PROTECAO_SOCIAL_ADOLESC_CUMPR_MEDIDA_SOCIOEDUCATIVA_LA_PSC.ToString());
            if (oServicoEhDeLAPSC)
            {
                layout_capacidade_la_psc.Visible = true;
                layout_media_mensal_la_psc.Visible = true;

                layout_capacidade.Visible = false;
                layout_media_mensal.Visible = false;
            }
            else
            {
                layout_capacidade_la_psc.Visible = false;
                layout_media_mensal_la_psc.Visible = false;

                layout_capacidade.Visible = true;
                layout_media_mensal.Visible = true;
            }
            #endregion
        }

        private void AdicionarProgramaConfinamento(int idCentro, int IdServicosRecursosFinanceirosCREAS, int idPrograma)
        {
            var item1 = SessaoProgramaProjetoCofinanciamentoExercicio.Where(x => x.IdProgramaProjeto == idPrograma).SingleOrDefault();

            var obj = new ProgramaProjetoCofinanciamentoInfo();
            obj.IdProgramaProjeto = Convert.ToInt32(item1.IdProgramaProjeto);
            obj.IdServicosRecursosFinanceirosCREAS = IdServicosRecursosFinanceirosCREAS;
            obj.NumeroUsuarios = item1.NumeroUsuarios;
            using (var proxy = new ProxyProgramas())
            {
                bool programaExistente = false;
                var programaprojeto = proxy.Service.GetProgramaProjetoById(Convert.ToInt32(obj.IdProgramaProjeto));
                if (programaprojeto != null)
                {
                    if (programaprojeto.IdPrefeitura == SessaoPmas.UsuarioLogado.Prefeitura.Id)
                    {
                        proxy.Service.AddProgramaProjetoCofinanciamento(obj);
                        LoadProgramas(proxy, obj.IdServicosRecursosFinanceirosCREAS.Value, idCentro);
                    }
                }
                var idServicoBeneficio = IdServicosRecursosFinanceirosCREAS;

                using (var ProxyProgramas = new ProxyProgramas())
                {
                    //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
                    var programas = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(obj.IdServicosRecursosFinanceirosCREAS.Value, idCentro);
                    var programasProjetosConfinamento = programas.Where(s1 => s1.Exercicio == 2018);
                    if (programasProjetosConfinamento.Count() == 0)
                    {
                        programasProjetosConfinamento = programas.Where(s1 => s1.Exercicio == 2019);
                    }

                    var lst = programasProjetosConfinamento.OrderBy(t => t.IdTipoProtecao)
                                .GroupBy(s => s.ProtecaoSocial)
                                .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) })
                                .ToList();

                    foreach (var programa in lst)
                    {
                        foreach (var item in programa.Items)
                        {
                            if (item.IdProgramaProjeto == obj.IdProgramaProjeto)
                            {
                                programaExistente = true;
                            }
                        }
                    }
                }

                if (!programaExistente)
                {
                    var obj2 = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

                    obj2.IdTransferenciaRenda = Convert.ToInt32(obj.IdProgramaProjeto);
                    obj2.IdServicosRecursosFinanceirosCREAS = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                    obj2.NumeroUsuarios = Convert.ToInt32(item1.NumeroUsuarios);
                    if (trRendaCidadaBeneficioIdoso.Visible && obj2.NumeroUsuarios <= 0)
                    {
                        throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                    }
                    using (var proxy2 = new ProxyProgramas())
                    {
                        proxy2.Service.AddTransferenciaRendaCofinanciamento(obj2);
                        LoadProgramas(proxy2, obj2.IdServicosRecursosFinanceirosCREAS.Value, idCentro);
                    }
                }
            }



        }
        private void AtualizaProgramaConfinamento(int idCentro)
        {
            var programaProjetoNovo = new ProgramaProjetoCofinanciamentoInfo();
            programaProjetoNovo.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);

            if (Request.QueryString["id"] != null)
            {
                programaProjetoNovo.IdServicosRecursosFinanceirosCREAS = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));


                if (!String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
                {
                    programaProjetoNovo.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }

                if (trRendaCidadaBeneficioIdoso.Visible && !programaProjetoNovo.NumeroUsuarios.HasValue)
                {
                    throw new Exception(String.Concat("Informe o numero de beneficiários deste serviço.", System.Environment.NewLine));
                }
                if (trRendaCidadaBeneficioIdoso.Visible && programaProjetoNovo.NumeroUsuarios.Value <= 0)
                {
                    throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                }


                using (var proxy = new ProxyProgramas())
                {
                    bool ehProgramaTransferenciaRenda = false;
                    var programaExistente = proxy.Service.GetProgramaProjetoById(Convert.ToInt32(ddlProgramaBeneficio.SelectedValue));

                    if (programaExistente != null)
                    {
                        if (programaExistente.IdPrefeitura == SessaoPmas.UsuarioLogado.Prefeitura.Id)
                        {
                            proxy.Service.AddProgramaProjetoCofinanciamento(programaProjetoNovo);
                            LoadProgramas(proxy, programaProjetoNovo.IdServicosRecursosFinanceirosCREAS.Value, idCentro);
                        }
                    }
                    var idServicoBeneficio = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                    using (var ProxyProgramas = new ProxyProgramas())
                    {

                        var programas = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(programaProjetoNovo.IdServicosRecursosFinanceirosCREAS.Value, idCentro);

                        var programasProjetosConfinamentos = programas.OrderBy(t => t.IdTipoProtecao)
                                    .GroupBy(s => s.ProtecaoSocial)
                                    .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) })
                                    .ToList();

                        foreach (var programa in programasProjetosConfinamentos)
                        {
                            foreach (var item in programa.Items)
                            {
                                if (item.IdProgramaProjeto == programaProjetoNovo.IdProgramaProjeto)
                                {
                                    ehProgramaTransferenciaRenda = true;
                                }
                            }
                        }
                    }

                    if (!ehProgramaTransferenciaRenda)
                    {
                        var transferenciaRendaNovo = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

                        transferenciaRendaNovo.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
                        transferenciaRendaNovo.IdServicosRecursosFinanceirosCREAS = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                        if (!String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
                        {
                            transferenciaRendaNovo.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                        }
                        else
                        {
                            throw new Exception(String.Concat("Informe o numero de beneficiários deste serviço.", System.Environment.NewLine));
                        }
                        if (trRendaCidadaBeneficioIdoso.Visible && transferenciaRendaNovo.NumeroUsuarios <= 0)
                        {
                            throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                        }

                        using (var proxyProgramas = new ProxyProgramas())
                        {
                            proxyProgramas.Service.AddTransferenciaRendaCofinanciamento(transferenciaRendaNovo);

                            LoadProgramas(proxyProgramas, transferenciaRendaNovo.IdServicosRecursosFinanceirosCREAS.Value, idCentro);
                        }
                    }
                }
            }
            else
            {
                AdicionaListaProgramaProjetoCofinanciamentoInfo();

            }


        }
        private void AdicionaListaProgramaProjetoCofinanciamentoInfo()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
            var obj = new ProgramaProjetoCofinanciamentoInfo();
            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();

            SessaoProgramaProjetoCofinanciamentoExercicio = SessaoProgramaProjetoCofinanciamentoExercicio ?? new List<ProgramaProjetoCofinanciamentoInfo>();

            consultaProgramaProjetoServicoCofinanciamentoInfo.Unidade = "ii";
            consultaProgramaProjetoServicoCofinanciamentoInfo.TipoServico = ddlTipoServico.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.NumeroAtendidos = Convert.ToInt32(txtNumeroUsuarios.Text);
            consultaProgramaProjetoServicoCofinanciamentoInfo.Usuario = ddlPublicoAlvo.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Nome = ddlProgramaBeneficio.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Id = rptProgramaTemp.Items.Count + 1;
            consultaProgramaProjetoServicoCofinanciamentoInfo.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.Add(consultaProgramaProjetoServicoCofinanciamentoInfo);
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.ToList();
            rptProgramaTemp.DataBind();
            obj.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            obj.Id = consultaProgramaProjetoServicoCofinanciamentoInfo.Id;
            obj.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            this.SessaoProgramaProjetoCofinanciamentoExercicio.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
            //btnSalvarRecursoPrograma.Visible = lstRecursos.Items.Count > 0;
        }
        private void AtualizaPrefeituraBeneficio(int idCentro)
        {
            if (Request.QueryString["id"] != null)
            {

                var entidadeBeneficio = new PrefeituraBeneficioEventualServicoInfo();
                if (!String.IsNullOrEmpty(txtNumeroUsuarios.Text))
                {
                    entidadeBeneficio.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }

                entidadeBeneficio.IdPrefeituraBeneficioEventual = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
                entidadeBeneficio.IdServicosRecursosFinanceirosCREAS = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                if (!String.IsNullOrEmpty(txtNumeroUsuarios.Text))
                {
                    entidadeBeneficio.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }

                new ValidadorServicoBeneficioEventual().Validar(entidadeBeneficio);

                using (var proxy = new ProxyProgramas())
                {
                    proxy.Service.AddBeneficioEventualServico(entidadeBeneficio);
                    LoadProgramas(proxy, entidadeBeneficio.IdServicosRecursosFinanceirosCREAS.Value, idCentro);
                }
            }
            else
            {

                AdicionaListaPrefeituraBeneficio();


            }
        }
        private void AdicionaListaPrefeituraBeneficio()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();
            SessaoPrefeituraBeneficioEventualServicoExercicio = SessaoPrefeituraBeneficioEventualServicoExercicio ?? new List<PrefeituraBeneficioEventualServicoInfo>();


            consultaProgramaProjetoServicoCofinanciamentoInfo.Unidade = "ii";
            consultaProgramaProjetoServicoCofinanciamentoInfo.TipoServico = ddlTipoServico.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.NumeroAtendidos = Convert.ToInt32(txtNumeroUsuarios.Text);
            consultaProgramaProjetoServicoCofinanciamentoInfo.Usuario = ddlPublicoAlvo.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Nome = ddlProgramaBeneficio.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Id = rptProgramaTemp.Items.Count + 1;
            consultaProgramaProjetoServicoCofinanciamentoInfo.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);

            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.Add(consultaProgramaProjetoServicoCofinanciamentoInfo);
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.ToList();
            rptProgramaTemp.DataBind();

            var obj = new PrefeituraBeneficioEventualServicoInfo();
            obj.IdPrefeituraBeneficioEventual = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            obj.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            obj.Id = consultaProgramaProjetoServicoCofinanciamentoInfo.Id;
            this.SessaoPrefeituraBeneficioEventualServicoExercicio.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";

        }
        private void AdicionaPrefeituraBeneficio(int idCentro, int IdServicosRecursosFinanceirosCREAS, int idPrograma)
        {

            var item2 = SessaoPrefeituraBeneficioEventualServicoExercicio.Where(x => x.IdPrefeituraBeneficioEventual == idPrograma).SingleOrDefault();

            var entidadeBeneficio = new PrefeituraBeneficioEventualServicoInfo();


            entidadeBeneficio.NumeroBeneficiarios = Convert.ToInt32(item2.NumeroBeneficiarios);


            entidadeBeneficio.IdPrefeituraBeneficioEventual = item2.IdPrefeituraBeneficioEventual;
            entidadeBeneficio.IdServicosRecursosFinanceirosCREAS = IdServicosRecursosFinanceirosCREAS;


            new ValidadorServicoBeneficioEventual().Validar(entidadeBeneficio);

            using (var proxy = new ProxyProgramas())
            {
                proxy.Service.AddBeneficioEventualServico(entidadeBeneficio);
                LoadProgramas(proxy, entidadeBeneficio.IdServicosRecursosFinanceirosCREAS.Value, idCentro);
            }
        }
        private void AtualizaTransferenciaRenda(int idCentro)
        {

            if (Request.QueryString["id"] != null)
            {

                var entidadeTransferenciaRenda = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

                entidadeTransferenciaRenda.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
                entidadeTransferenciaRenda.IdServicosRecursosFinanceirosCREAS = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                if (!String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
                {
                    entidadeTransferenciaRenda.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }
                else
                {
                    throw new Exception(String.Concat("Informe o numero de beneficiários deste serviço.", System.Environment.NewLine));
                }
                if (trRendaCidadaBeneficioIdoso.Visible && entidadeTransferenciaRenda.NumeroUsuarios <= 0)
                {
                    throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                }
                // var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
                using (var proxy = new ProxyProgramas())
                {
                    proxy.Service.AddTransferenciaRendaCofinanciamento(entidadeTransferenciaRenda);
                    LoadProgramas(proxy, entidadeTransferenciaRenda.IdServicosRecursosFinanceirosCREAS.Value, idCentro);
                }
            }
            else
            {

                AdicionaListaTransferenciaRenda();


            }
        }
        private void AdicionaTransferenciaRenda(int idCentro, int IdServicosRecursosFinanceirosCREAS, int idPrograma)
        {
            var item2 = SessaoTransferenciaRendaCofinanciamentoExercicio.Where(x => x.IdTransferenciaRenda == idPrograma).SingleOrDefault();

            var entidadeTransferenciaRenda = new ServicoRecursoFinanceiroTransferenciaRendaInfo();
            entidadeTransferenciaRenda.IdTransferenciaRenda = item2.IdTransferenciaRenda;

            entidadeTransferenciaRenda.IdServicosRecursosFinanceirosCREAS = IdServicosRecursosFinanceirosCREAS;


            entidadeTransferenciaRenda.NumeroUsuarios = Convert.ToInt32(item2.NumeroUsuarios);

            if (trRendaCidadaBeneficioIdoso.Visible && entidadeTransferenciaRenda.NumeroUsuarios <= 0)
            {
                throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
            }
            // var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
            using (var proxy = new ProxyProgramas())
            {
                proxy.Service.AddTransferenciaRendaCofinanciamento(entidadeTransferenciaRenda);
                LoadProgramas(proxy, entidadeTransferenciaRenda.IdServicosRecursosFinanceirosCREAS.Value, idCentro);
            }




        }
        private void AdicionaListaTransferenciaRenda()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano

            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();
            SessaoTransferenciaRendaCofinanciamentoExercicio = SessaoTransferenciaRendaCofinanciamentoExercicio ?? new List<ServicoRecursoFinanceiroTransferenciaRendaInfo>();


            consultaProgramaProjetoServicoCofinanciamentoInfo.Unidade = "ii";
            consultaProgramaProjetoServicoCofinanciamentoInfo.TipoServico = ddlTipoServico.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.NumeroAtendidos = Convert.ToInt32(txtNumeroUsuarios.Text);
            consultaProgramaProjetoServicoCofinanciamentoInfo.Usuario = ddlPublicoAlvo.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Nome = ddlProgramaBeneficio.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Id = rptProgramaTemp.Items.Count + 1;
            consultaProgramaProjetoServicoCofinanciamentoInfo.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);

            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.Add(consultaProgramaProjetoServicoCofinanciamentoInfo);
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.ToList();
            rptProgramaTemp.DataBind();

            var obj = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

            obj.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            obj.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            obj.Id = consultaProgramaProjetoServicoCofinanciamentoInfo.Id;
            this.SessaoTransferenciaRendaCofinanciamentoExercicio.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
            //btnSalvarRecursoPrograma.Visible = lstRecursos.Items.Count > 0;
        }
        #endregion

        #region Recursos Financeiros - Métodos
        #region Exercicio 1
        protected void btnAdicionarRecursoExercicio1_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroCREASFonteRecursoInfo();
            recurso.NomeFonteRecurso = txtNomeRecursoExercicio1.Text;
            recurso.ValorFonteRecurso = String.IsNullOrEmpty(txtValorRecursoExercicio1.Text) ? 0M : Convert.ToDecimal(txtValorRecursoExercicio1.Text);
            recurso.Liberado = RetornaValidacaoBloqueioDesbloqueio(2022);


            frame1_5.Attributes.Add("class", "active");
            try
            {
                new ValidadorServicosFinanceirosFonteRecursos().Validar(recurso);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            SessaoFontesRecursosExercicio1 = SessaoFontesRecursosExercicio1 ?? new List<ServicoRecursoFinanceiroCREASFonteRecursoInfo>();
            SessaoFontesRecursosExercicio1.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio1();

            txtNomeRecursoExercicio1.Text = String.Empty;
            txtValorRecursoExercicio1.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio1.Visible = true;

        }
        protected void rblOutrasFontesExercicio1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio1.SelectedValue == "0")
            {
                txtValorRecursoExercicio1.Text = "0,00";
                txtNomeRecursoExercicio1.Text = String.Empty;
                SessaoFontesRecursosExercicio1 = null;
                lstRecursosAdicionadosExercicio1.DataSource = SessaoFontesRecursosExercicio1;
                lstRecursosAdicionadosExercicio1.DataBind();
                lstRecursosAdicionadosExercicio1.Visible = false;
            }
            trAddRecursoExercicio1.Visible = trMotivoEstadualizadoExercicio1.Visible = rblOutrasFontesExercicio1.SelectedValue == "1";
        }
        protected void lstRecursosAdicionadosExercicio1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequenciaExercicio1")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void lstRecursosAdicionadosExercicio1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    SessaoFontesRecursosExercicio1.RemoveAt(e.Item.DataItemIndex);
                    CarregarRecursosFinanceirosFonteRecursosExercicio1();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Exercicio 2
        protected void btnAdicionarRecursoExercicio2_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroCREASFonteRecursoInfo();
            recurso.NomeFonteRecurso = txtNomeRecursoExercicio2.Text;
            recurso.ValorFonteRecurso = String.IsNullOrEmpty(txtValorRecursoExercicio2.Text) ? 0M : Convert.ToDecimal(txtValorRecursoExercicio2.Text);
            recurso.Liberado = RetornaValidacaoBloqueioDesbloqueio(2023);


            CarregarAbaInicialRecursosFinanceiros();
            try
            {
                new ValidadorServicosFinanceirosFonteRecursos().Validar(recurso);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            SessaoFontesRecursosExercicio2 = SessaoFontesRecursosExercicio2 ?? new List<ServicoRecursoFinanceiroCREASFonteRecursoInfo>();
            SessaoFontesRecursosExercicio2.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio2();

            txtNomeRecursoExercicio2.Text = String.Empty;
            txtValorRecursoExercicio2.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio2.Visible = true;
            ValidaBloqueioDesbloqueio();

        }
        protected void rblOutrasFontesExercicio2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio2.SelectedValue == "0")
            {
                txtValorRecursoExercicio2.Text = "0,00";
                txtNomeRecursoExercicio2.Text = String.Empty;
                SessaoFontesRecursosExercicio2 = null;
                lstRecursosAdicionadosExercicio2.DataSource = SessaoFontesRecursosExercicio2;
                lstRecursosAdicionadosExercicio2.DataBind();
                lstRecursosAdicionadosExercicio2.Visible = false;
            }
            trAddRecursoExercicio2.Visible = trMotivoEstadualizadoExercicio2.Visible = rblOutrasFontesExercicio2.SelectedValue == "1";
            ValidaBloqueioDesbloqueio();
        }
        protected void lstRecursosAdicionadosExercicio2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequenciaExercicio2")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void lstRecursosAdicionadosExercicio2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    SessaoFontesRecursosExercicio2.RemoveAt(e.Item.DataItemIndex);
                    CarregarRecursosFinanceirosFonteRecursosExercicio2();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Exercicio 3
        protected void btnAdicionarRecursoExercicio3_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroCREASFonteRecursoInfo();
            recurso.NomeFonteRecurso = txtNomeRecursoExercicio3.Text;
            recurso.ValorFonteRecurso = String.IsNullOrEmpty(txtValorRecursoExercicio3.Text) ? 0M : Convert.ToDecimal(txtValorRecursoExercicio3.Text);
            recurso.Liberado = RetornaValidacaoBloqueioDesbloqueio(2024);

            CarregarAbaInicialRecursosFinanceiros();
            try
            {
                new ValidadorServicosFinanceirosFonteRecursos().Validar(recurso);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            SessaoFontesRecursosExercicio3 = SessaoFontesRecursosExercicio3 ?? new List<ServicoRecursoFinanceiroCREASFonteRecursoInfo>();
            SessaoFontesRecursosExercicio3.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio3();

            txtNomeRecursoExercicio3.Text = String.Empty;
            txtValorRecursoExercicio3.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio3.Visible = true;
            ValidaBloqueioDesbloqueio();

        }
        protected void rblOutrasFontesExercicio3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio3.SelectedValue == "0")
            {
                txtValorRecursoExercicio3.Text = "0,00";
                txtNomeRecursoExercicio3.Text = String.Empty;
                SessaoFontesRecursosExercicio3 = null;
                lstRecursosAdicionadosExercicio3.DataSource = SessaoFontesRecursosExercicio3;
                lstRecursosAdicionadosExercicio3.DataBind();
                lstRecursosAdicionadosExercicio3.Visible = false;
            }
            trAddRecursoExercicio3.Visible = trMotivoEstadualizadoExercicio3.Visible = rblOutrasFontesExercicio3.SelectedValue == "1";
            ValidaBloqueioDesbloqueio();
        }
        protected void lstRecursosAdicionadosExercicio3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequenciaExercicio3")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void lstRecursosAdicionadosExercicio3_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    SessaoFontesRecursosExercicio3.RemoveAt(e.Item.DataItemIndex);
                    CarregarRecursosFinanceirosFonteRecursosExercicio3();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Exercicio 4
        protected void btnAdicionarRecursoExercicio4_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroCREASFonteRecursoInfo();
            recurso.NomeFonteRecurso = txtNomeRecursoExercicio4.Text;
            recurso.ValorFonteRecurso = String.IsNullOrEmpty(txtValorRecursoExercicio4.Text) ? 0M : Convert.ToDecimal(txtValorRecursoExercicio4.Text);
            recurso.Liberado = RetornaValidacaoBloqueioDesbloqueio(2025);

            CarregarAbaInicialRecursosFinanceiros();
            try
            {
                new ValidadorServicosFinanceirosFonteRecursos().Validar(recurso);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            SessaoFontesRecursosExercicio4 = SessaoFontesRecursosExercicio4 ?? new List<ServicoRecursoFinanceiroCREASFonteRecursoInfo>();
            SessaoFontesRecursosExercicio4.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio4();

            txtNomeRecursoExercicio4.Text = String.Empty;
            txtValorRecursoExercicio4.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio4.Visible = true;
            ValidaBloqueioDesbloqueio();

        }
        protected void rblOutrasFontesExercicio4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio4.SelectedValue == "0")
            {
                txtValorRecursoExercicio4.Text = "0,00";
                txtNomeRecursoExercicio4.Text = String.Empty;
                SessaoFontesRecursosExercicio4 = null;
                lstRecursosAdicionadosExercicio4.DataSource = SessaoFontesRecursosExercicio4;
                lstRecursosAdicionadosExercicio4.DataBind();
                lstRecursosAdicionadosExercicio4.Visible = false;
            }
            trAddRecursoExercicio4.Visible = trMotivoEstadualizadoExercicio4.Visible = rblOutrasFontesExercicio4.SelectedValue == "1";
            ValidaBloqueioDesbloqueio();
        }
        protected void lstRecursosAdicionadosExercicio4_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequenciaExercicio4")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void lstRecursosAdicionadosExercicio4_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    SessaoFontesRecursosExercicio4.RemoveAt(e.Item.DataItemIndex);
                    CarregarRecursosFinanceirosFonteRecursosExercicio4();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        #endregion

        #endregion

        #region helper
        protected string MontarBotao(ConsultaProgramaProjetoServicoCofinanciamentoInfo item)
        {
            var idProjeto = item.IdServicoRecursoFinanceiro;
            var page = String.Empty;
            switch (item.TipoUnidade)
            {
                case "Rede direta": page = "../BlocoIII/VServicoRecursoFinanceiroPublico.aspx"; break;
                case "Rede indireta": page = "../BlocoIII/VServicoRecursoFinanceiroPrivado.aspx"; break;
                case "CRAS": page = "../BlocoIII/VServicoRecursoFinanceiroCRAS.aspx"; break;
                case "CREAS": page = "../BlocoIII/VServicoRecursoFinanceiroCREAS.aspx"; break;
                case "Centro Pop": page = "../BlocoIII/VServicoRecursoFinanceiroCentroPOP.aspx"; break;
            }
            return "<a href='" + page + "?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.IdServicoRecursoFinanceiro.ToString())) + "&idProjeto=" + idProjeto + "'><img src='../Styles/Icones/find.png' alt='Visualizar' border='0' /></a>";
        }
        private void ExibirFrameInicialmenteAtivo()
        {
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
        }
        private void CarregarCampos()
        {
            using (var proxyEstrutura = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarAvaliacoes(proxyEstrutura);
                CarregarCombos(proxyEstrutura, String.IsNullOrEmpty(Request.QueryString["id"]));
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {
                        load(proxy, proxyEstrutura);
                    }
                }
                else
                {
                    this.AplicarRegraExibicaoLayoutServicosChanged();
                    this.AplicarRegraBloqueioDesbloqueio();
                }

            }
        }
        private void ClearPrograma()
        {
            rptProgramaTemp.DataSource = null;
            rptProgramaTemp.DataBind();
        }
        private void ClearSessao()
        {
            this.SessaoProgramaProjetoCofinanciamentoExercicio = null;
            this.SessaoTransferenciaRendaCofinanciamentoExercicio = null;
            this.SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = null;
            this.SessaoPrefeituraBeneficioEventualServicoExercicio = null;
        }

        protected void CamposBindEventos()
        {

            #region Recursos financeiros
            #region Exercicio 1
            txtFMASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #region Exercicio 2
            txtFMASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #region Exercicio 3
            txtFMASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFMASExercicio3.Attributes.Add("onclick", "this.select()");
            txtFNASExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEASExercicio3.Attributes.Add("onclick", "this.select()");
            txtFMDCAExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEDCAExercicio3.Attributes.Add("onclick", "this.select()");
            txtFNDCAExercicio3.Attributes.Add("onclick", "this.select()");
            txtValorContraExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEASAnoAnteriorExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEASDemandasExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEASReprogramacaoDemandasParlamentaresExercicio3.Attributes.Add("onclick", "this.select()");
            txtFMIExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEIExercicio3.Attributes.Add("onclick", "this.select()");
            txtFNIExercicio3.Attributes.Add("onclick", "this.select()");
            txtValorRecursoExercicio3.Attributes.Add("onclick", "this.select()");
            #endregion

            #region Exercicio 4
            txtFMASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFMASExercicio4.Attributes.Add("onclick", "this.select()");
            txtFNASExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEASExercicio4.Attributes.Add("onclick", "this.select()");
            txtFMDCAExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEDCAExercicio4.Attributes.Add("onclick", "this.select()");
            txtFNDCAExercicio4.Attributes.Add("onclick", "this.select()");
            txtValorContraExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEASAnoAnteriorExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEASDemandasExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEASReprogramacaoDemandasParlamentaresExercicio4.Attributes.Add("onclick", "this.select()");
            txtFMIExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEIExercicio4.Attributes.Add("onclick", "this.select()");
            txtFNIExercicio4.Attributes.Add("onclick", "this.select()");
            txtValorRecursoExercicio4.Attributes.Add("onclick", "this.select()");
            #endregion

            #endregion

            txtValorEstadualizado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtNivelFundamental.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNivelFundamental.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtNivelMedio.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNivelMedio.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtSuperior.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperior.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtSuperiorServicoSocial.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorServicoSocial.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtSuperiorPsicologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorPsicologia.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtSuperiorPedagogia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorPedagogia.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtSociologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSociologia.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtDireito.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtDireito.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtSuperiorAntropologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorEconomia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorEconomiaDomestica.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorTerapiaOcupacional.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorMusicoTerapia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSemEscolaridade.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

            txtSemEscolaridade.Attributes.Add("onblur", "CalculateTotal()");
            txtNivelFundamental.Attributes.Add("onblur", "CalculateTotal()");
            txtNivelMedio.Attributes.Add("onblur", "CalculateTotal()");
            txtSuperior.Attributes.Add("onblur", "CalculateTotal()");

        }
        private void ClearSituacoes()
        {
            lstSituacoesEspecificas.DataTextField = "Nome";
            lstSituacoesEspecificas.DataValueField = "Id";
            lstSituacoesEspecificas.DataSource = new List<SituacaoEspecificaInfo>();
            lstSituacoesEspecificas.DataBind();
        }
        private void CarregarAbaInicialRecursosFinanceiros()
        {
            hdnExercicio.Value = (hdnExercicio.Value == string.Empty) ? FServicoRecursoFinanceiroCREAS.Exercicios[0].ToString() : hdnExercicio.Value;
            frame1_5.Attributes.Add("class", "active");

            if (hdnExercicio.Value == FServicoRecursoFinanceiroCREAS.Exercicios[0].ToString())
            {
                frame1_5_Ano1.Attributes.Add("class", "active");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Remove("class");
            }
            else if (hdnExercicio.Value == FServicoRecursoFinanceiroCREAS.Exercicios[1].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Add("class", "active");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Remove("class");
            }
            else if (hdnExercicio.Value == FServicoRecursoFinanceiroCREAS.Exercicios[2].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Add("class", "active");
                frame1_5_Ano4.Attributes.Remove("class");
            }
            else if (hdnExercicio.Value == FServicoRecursoFinanceiroCREAS.Exercicios[3].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Add("class", "active");
            }

        }
        private void AposSalvoNaoPermitirEdicaoCamposCaracterizacao(ServicoRecursoFinanceiroCREASInfo servico)
        {

            if (servico.Id != 0)
            {
                rblTipoProtecao.Enabled = false;
                ddlTipoServico.Enabled = false;
                ddlPublicoAlvo.Enabled = false;
            }
            else
            {
                rblTipoProtecao.Enabled = true;
                ddlTipoServico.Enabled = true;
                ddlPublicoAlvo.Enabled = true;
            }
        }
        private void DefinirFrame1ComoPrincipal()
        {
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
        }
        #endregion

        #region bloqueio e desbloqueio

        #region WebControls : Selecionar : Bloqueio Desbloqueio
        private WebControl[] SelecionarControlesRecursosFinanceirosBloqueioExercicio1()
        {
            WebControl[] controlesExercicio1 = {
                                         txtFMASExercicio1,
                                         txtFMDCAExercicio1,
                                         txtFMIExercicio1,
                                         txtFEASExercicio1,
                                         txtFEDCAExercicio1, 
                                         txtFEIExercicio1,
                                         txtFNASExercicio1,
                                         txtFNDCAExercicio1,
                                         txtFNIExercicio1,
                                         rblOutrasFontesExercicio1,
                                         txtNomeRecursoExercicio1,
                                         txtValorRecursoExercicio1,
                                         btnAdicionarRecursoExercicio1,

                                         txtCapacidadeExercicio1,
                                         txtCapacidadePSCExercicio1,
                                         txtCapacidadeLAExercicio1,

                                         txtMediaMensalExercicio1,
                                         txtMediaMensalLAExercicio1,
                                         txtMediaMensalPSCExercicio1
                                         };
            return controlesExercicio1;
        }
        private WebControl[] SelecionarControlesRecursosFinanceirosBloqueioExercicio2()
        {

            WebControl[] controlesExercicio2 = {
                                         txtFMASExercicio2,
                                         txtFMDCAExercicio2,
                                         txtFMIExercicio2,
                                         txtFEASExercicio2,
                                         txtFEDCAExercicio2, 
                                         txtFEIExercicio2,
                                         txtFNASExercicio2,
                                         txtFNDCAExercicio2,
                                         txtFNIExercicio2,
                                         rblOutrasFontesExercicio2,
                                         txtNomeRecursoExercicio2,
                                         txtValorRecursoExercicio2,
                                         btnAdicionarRecursoExercicio2,

                                         txtCapacidadeExercicio2,
                                         txtCapacidadePSCExercicio2,
                                         txtCapacidadeLAExercicio2,

                                         txtMediaMensalExercicio2,
                                         txtMediaMensalLAExercicio2,
                                         txtMediaMensalPSCExercicio2
                                         };
            return controlesExercicio2;
        }
        private WebControl[] SelecionarControlesRecursosFinanceirosBloqueioExercicio3()
        {

            WebControl[] controlesExercicio3 = {
                                       txtFMASExercicio3,
                                         txtFMDCAExercicio3,
                                         txtFMIExercicio3,
                                         txtFEASExercicio3,
                                         txtFEDCAExercicio3, 
                                         txtFEIExercicio3,
                                         txtFNASExercicio3,
                                         txtFNDCAExercicio3,
                                         txtFNIExercicio3,
                                         rblOutrasFontesExercicio3,
                                         txtNomeRecursoExercicio3,
                                         txtValorRecursoExercicio3,
                                         btnAdicionarRecursoExercicio3,

                                         txtCapacidadeExercicio3,
                                         txtCapacidadePSCExercicio3,
                                         txtCapacidadeLAExercicio3,

                                         txtMediaMensalExercicio3,
                                         txtMediaMensalLAExercicio3,
                                         txtMediaMensalPSCExercicio3
                                         };
            return controlesExercicio3;
        }
        private WebControl[] SelecionarControlesRecursosFinanceirosBloqueioExercicio4()
        {

            WebControl[] controlesExercicio4 = {
                                     txtFMASExercicio4,
                                         txtFMDCAExercicio4,
                                         txtFMIExercicio4,
                                         txtFEASExercicio4,
                                         txtFEDCAExercicio4, 
                                         txtFEIExercicio4,
                                         txtFNASExercicio4,
                                         txtFNDCAExercicio4,
                                         txtFNIExercicio4,
                                         rblOutrasFontesExercicio4,
                                         txtNomeRecursoExercicio4,
                                         txtValorRecursoExercicio4,
                                         btnAdicionarRecursoExercicio4,

                                         txtCapacidadeExercicio4,
                                         txtCapacidadePSCExercicio4,
                                         txtCapacidadeLAExercicio4,

                                         txtMediaMensalExercicio4,
                                         txtMediaMensalLAExercicio4,
                                         txtMediaMensalPSCExercicio4
                                         };
            return controlesExercicio4;
        }

        private WebControl[] SelecionarControlesReprogramacaoBloqueioExercicio1()
        {
            WebControl[] controlesExercicio1 = {
                                                   txtFEASAnoAnteriorExercicio1,
                                                   txtFEASReprogramacaoDemandasParlamentaresExercicio1
                                               };
            return controlesExercicio1;
        }
        private WebControl[] SelecionarControlesReprogramacaoBloqueioExercicio2()
        {
            WebControl[] controlesExercicio2 = {
                                                   txtFEASAnoAnteriorExercicio2,
                                                   txtFEASReprogramacaoDemandasParlamentaresExercicio2
                                               };
            return controlesExercicio2;

        }

        private WebControl[] SelecionarControlesReprogramacaoBloqueioExercicio3()
        {
            WebControl[] controlesExercicio3 = {
                                                   txtFEASAnoAnteriorExercicio3,
                                                   txtFEASReprogramacaoDemandasParlamentaresExercicio3
                                               };
            return controlesExercicio3;

        }

        private WebControl[] SelecionarControlesReprogramacaoBloqueioExercicio4()
        {
            WebControl[] controlesExercicio4 = {
                                                   txtFEASAnoAnteriorExercicio4,
                                                   txtFEASReprogramacaoDemandasParlamentaresExercicio4
                                               };
            return controlesExercicio4;

        }

        private WebControl[] SelecionarControlesDemandasExercicio1()
        {
            WebControl[] controlesExercicio1 = {
                                                   txtFEASDemandasExercicio1,
                                                   txtCodigoDemandaExercicio1,
                                                   txtValorContraExercicio1,
                                                   txtObjetoDemandaExercicio1,
                                                   rblContraPartida1
                                               };
            return controlesExercicio1;
        }

        private WebControl[] SelecionarControlesDemandasExercicio2()
        {
            WebControl[] controlesExercicio2 = {
                                                   txtFEASDemandasExercicio2,
                                                   txtCodigoDemandaExercicio2,
                                                   txtValorContraExercicio2,
                                                   txtObjetoDemandaExercicio2,
                                                   rblContraPartida2
                                               };
            return controlesExercicio2;
        }

        private WebControl[] SelecionarControlesDemandasExercicio3()
        {
            WebControl[] controlesExercicio3 = {
                                                   txtFEASDemandasExercicio3,
                                                   txtCodigoDemandaExercicio3,
                                                   txtValorContraExercicio3,
                                                   txtObjetoDemandaExercicio3,
                                                   rblContraPartida3
                                               };
            return controlesExercicio3;
        }

        private WebControl[] SelecionarControlesDemandasExercicio4()
        {
            WebControl[] controlesExercicio4 = {
                                                   txtFEASDemandasExercicio4,
                                                   txtCodigoDemandaExercicio4,
                                                   txtValorContraExercicio4,
                                                   txtObjetoDemandaExercicio4,
                                                   rblContraPartida4
                                               };
            return controlesExercicio4;
        }

        #endregion

        private void AplicarRegraBloqueioDesbloqueio()
        {
            #region Seleciona: Campos Recursos financeiros
            WebControl[] controles1 = SelecionarControlesRecursosFinanceirosBloqueioExercicio1();
            WebControl[] controles2 = SelecionarControlesRecursosFinanceirosBloqueioExercicio2();
            WebControl[] controles3 = SelecionarControlesRecursosFinanceirosBloqueioExercicio3();
            WebControl[] controles4 = SelecionarControlesRecursosFinanceirosBloqueioExercicio4();
            #endregion

            #region Seleciona: Campos Reprogramacao
            WebControl[] controlesReprogramacaoExercicio1 = SelecionarControlesReprogramacaoBloqueioExercicio1();
            WebControl[] controlesReprogramacaoExercicio2 = SelecionarControlesReprogramacaoBloqueioExercicio2();
            WebControl[] controlesReprogramacaoExercicio3 = SelecionarControlesReprogramacaoBloqueioExercicio3();
            WebControl[] controlesReprogramacaoExercicio4 = SelecionarControlesReprogramacaoBloqueioExercicio4();
            #endregion

            #region Seleciona: Campos Demandas
            WebControl[] controlesDemandasExercicio1 = SelecionarControlesDemandasExercicio1();
            WebControl[] controlesDemandasExercicio2 = SelecionarControlesDemandasExercicio2();
            WebControl[] controlesDemandasExercicio3 = SelecionarControlesDemandasExercicio3();
            WebControl[] controlesDemandasExercicio4 = SelecionarControlesDemandasExercicio4();
            #endregion 

            #region Regra: Bloqueio: Campos Recursos Financeiros
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroCREAS.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroCREAS.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroCREAS.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroCREAS.Exercicios[3]);
            #endregion

            #region Regra: Bloqueio: Campos Reprogramacao
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio1, FServicoRecursoFinanceiroCREAS.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio2, FServicoRecursoFinanceiroCREAS.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio3, FServicoRecursoFinanceiroCREAS.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio4, FServicoRecursoFinanceiroCREAS.Exercicios[3]);

            #endregion

            #region Regras: Bloqueio: Campos Demandas
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio1,FServicoRecursoFinanceiroCREAS.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio2, FServicoRecursoFinanceiroCREAS.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio3, FServicoRecursoFinanceiroCREAS.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio4, FServicoRecursoFinanceiroCREAS.Exercicios[3]);
            #endregion

            #region Regra: Bloqueio: Botao Salvar
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio1, FServicoRecursoFinanceiroCREAS.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio2, FServicoRecursoFinanceiroCREAS.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio3, FServicoRecursoFinanceiroCREAS.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio4, FServicoRecursoFinanceiroCREAS.Exercicios[3]);
            #endregion

        }

        #endregion

        [System.Web.Services.WebMethod]
        public static String CalcularValores(String[] valores)
        {
            Int32 total = 0;
            foreach (String val in valores)
            {
                string valor = !String.IsNullOrEmpty(val) ? val : "0";
                total += Convert.ToInt32(valor);
            }
            return total.ToString();
        }

        protected void ddlAbrangencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAbrangencia.SelectedValue == "4")
            {
                trSedeServico.Visible = true;
                trFormaJuridica.Visible = true;
            }
            else
            {
                trSedeServico.Visible = false;
                trFormaJuridica.Visible = false;
                trConsorcioPublico.Visible = false;
            }
        }

        protected void rblAbrangencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAbrangencia.SelectedValue == "1")
            {
                trMunicipioParticipaOferta.Visible = true;
                trMunicipioSede.Visible = false;
            }
            else
            {
                trMunicipioParticipaOferta.Visible = false;
                trMunicipioSede.Visible = true;
            }
        }

        protected void ddlFormaJuridica_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormaJuridica.SelectedValue == "1")
            {
                trConsorcioPublico.Visible = true;
            }
            else
            {
                trConsorcioPublico.Visible = false;
            }
        }

        protected void rblContraPartida1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Add("class", "active");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Remove("class");


            if (rblContraPartida1.SelectedValue == "1")
            {
                trValorContraExercicio1.Visible = true;
            }
            else
            {
                trValorContraExercicio1.Visible = false;
            }
        }

        protected void rblContraPartida2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Add("class", "active");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Remove("class");

            if (rblContraPartida2.SelectedValue == "1")
            {
                trValorContraExercicio2.Visible = true;
            }
            else
            {
                trValorContraExercicio2.Visible = false;
            }
        }

        protected void rblContraPartida3_SelectedIndexChanged(object sender, EventArgs e)
        {

            SessaoPmas.VerificarSessao(this);
            frame1_5.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Add("class", "active");
            frame1_5_Ano4.Attributes.Remove("class");

            if (rblContraPartida3.SelectedValue == "1")
            {
                trValorContraExercicio3.Visible = true;
            }
            else
            {
                trValorContraExercicio3.Visible = false;
            }
        }

        protected void rblContraPartida4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Add("class", "active");

            if (rblContraPartida4.SelectedValue == "1")
            {
                trValorContraExercicio4.Visible = true;
            }
            else
            {
                trValorContraExercicio4.Visible = false;
            }		
        }


    }
}
