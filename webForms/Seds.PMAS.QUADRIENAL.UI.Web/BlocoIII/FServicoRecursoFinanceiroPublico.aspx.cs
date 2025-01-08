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
    public partial class FServicoRecursoFinanceiroPublico : System.Web.UI.Page
    {
        #region propriedades
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        private static int exercicioDesbloqueado;
        #endregion

        #region sessao
        protected List<ConsultaProgramaProjetoServicoCofinanciamentoInfo> SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo
        {
            get { return Session["CONSULTA_PROGRAMA_PROJETO_SERVICOCO_FINANCIAMENTOINFO"] as List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>; }
            set { Session["CONSULTA_PROGRAMA_PROJETO_SERVICOCO_FINANCIAMENTOINFO"] = value; }
        }

        protected List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> SessaoFontesRecursosExercicio1
        {
            get { return Session["FONTES_RECURSOS_EXERCICIO1"] as List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_EXERCICIO1"] = value; }
        }

        protected List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> SessaoFontesRecursosExercicio2
        {
            get { return Session["FONTES_RECURSOS_EXERCICIO2"] as List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_EXERCICIO2"] = value; }
        }

        protected List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> SessaoFontesRecursosExercicio3
        {
            get { return Session["FONTES_RECURSOS_EXERCICIO3"] as List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_EXERCICIO3"] = value; }
        }

        protected List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> SessaoFontesRecursosExercicio4
        {
            get { return Session["FONTES_RECURSOS_EXERCICIO4"] as List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_EXERCICIO4"] = value; }
        }


        protected List<PrefeituraBeneficioEventualServicoInfo> SessaoPrefeituraBeneficioEventualServicoInfo1
        {
            get { return Session["PREFEITURA_BENEFICIO_EVENTUALSERVICOINFO_CRAS1"] as List<PrefeituraBeneficioEventualServicoInfo>; }
            set { Session["PREFEITURA_BENEFICIO_EVENTUALSERVICOINFO_CRAS1"] = value; }
        }

        protected List<ProgramaProjetoCofinanciamentoInfo> SessaoProgramaProjetoCofinanciamentoInfo1
        {
            get { return Session["PROGRAMA_PROJETO_CO_FINANCIAMENTOINFO_CRAS1"] as List<ProgramaProjetoCofinanciamentoInfo>; }
            set { Session["PROGRAMA_PROJETO_CO_FINANCIAMENTOINFO_CRAS1"] = value; }
        }

        protected List<ServicoRecursoFinanceiroTransferenciaRendaInfo> SessaoTransferenciaRendaCofinanciamentoInfo1
        {
            get { return Session["TRANSFERENCIA_RENDA_CO_FINANCIAMENTOINFO_CRAS1"] as List<ServicoRecursoFinanceiroTransferenciaRendaInfo>; }
            set { Session["TRANSFERENCIA_RENDA_CO_FINANCIAMENTOINFO_CRAS1"] = value; }
        }
        #endregion

        #region Obter
        private void ObterLocalExecucaoPublico(ServicoRecursoFinanceiroPublicoInfo servico, int idLocalExecucao)
        {
            using (var proxyRede = new ProxyRedeProtecaoSocial())
            {
                servico.NumeroAtendidosCentroMensal = proxyRede.Service.GetLocalExecucaoPublicoById(idLocalExecucao).CapacidadeAtendimento;
                servico.TipoLocal = 1;
            }
        }
        #endregion

        #region Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               

                #region Valida: [Existe usuario com prefeitura]
                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                } 
                #endregion

                #region Valida: [Existe Local Execucao]
                if (String.IsNullOrEmpty(Request.QueryString["idLocal"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx");
                    return;
                } 
                #endregion

                #region Exibe: [Mensagem após salvar | atualizar]
                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "A")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço atualizado com sucesso!"), true);
                    }
                    else if (Request.QueryString["msg"] == "I")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço registrado com sucesso!<br/>"), true);
                    }
                } 
                #endregion

                #region Carregamento
                this.PreparaCodigoObjeto();
                this.CamposBindEventos();
                this.CamposCarregarValores();
                this.ExibirFrameInicialmenteAtivo();
                this.ValidaBloqueioDesbloqueio();
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
                                         situacoesEspecificas,
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
                                         txtCapacidadeExercicio1,
                                         rblHorasSemana,
                                         rblDiasSemana,
                                         lstAtividades,
                                         rblAvaliacaoGestor,
                                         btnSalvarExercicio1, 
                                         btnSalvarExercicio2,
                                        };

                Permissao.VerificarPermissaoControles(controles, Session);
                Permissao.VerificarPermissaoControles(txtDataInicio.Controles, Session);

                #endregion

                this.ClearSessao();
            }

        }

        private void ValidaBloqueioDesbloqueio()
        {
            WebControl[] controles1 = SelecionarControlesRecursosFinanceirosBloqueioExercicio1();
            WebControl[] controles2 = SelecionarControlesRecursosFinanceirosBloqueioExercicio2();
            WebControl[] controles3 = SelecionarControlesRecursosFinanceirosBloqueioExercicio3();
            WebControl[] controles4 = SelecionarControlesRecursosFinanceirosBloqueioExercicio4();

           var validaBloqueio2022 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroPublico.Exercicios[0]);
           var validaBloqueio2023 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroPublico.Exercicios[1]);
           var validaBloqueio2024 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroPublico.Exercicios[2]);
           var validaBloqueio2025 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroPublico.Exercicios[3]);

           if (validaBloqueio2022 == true)
           {
               exercicioDesbloqueado = 2022;
               ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio1()", "exibeBtnExcluirExercicio1();", true);
              
           }
           else
           {
               ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio1()", "ocultaBtnExcluirExercicio1();", true);
               
           }

           if (validaBloqueio2023 == true)
           {
               exercicioDesbloqueado = 2023;
               ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio2()", "exibeBtnExcluirExercicio2();", true);
           }
           else
           {
               ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio2()", "ocultaBtnExcluirExercicio2();", true);
           }

           if (validaBloqueio2024 == true)
           {
               exercicioDesbloqueado = 2024;
               ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio3()", "exibeBtnExcluirExercicio3();", true);
           }
           else
           {
               ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio3()", "ocultaBtnExcluirExercicio3();", true);
           }

           if (validaBloqueio2025 == true)
           {
               exercicioDesbloqueado = 2025;
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
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroPublico.Exercicios[0]);
            }

            if (exercicio == 2023)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroPublico.Exercicios[1]);
            }

            if (exercicio == 2024)
            {
                validacao =  Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroPublico.Exercicios[2]);
            }

            if (exercicio == 2025)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroPublico.Exercicios[3]);
            }

            return validacao;
            
        }

        private void PreparaCodigoObjeto()
        {  
            txtObjetoDemandaExercicio1.Text = string.Empty;
            txtObjetoDemandaExercicio2.Text = string.Empty;
            txtObjetoDemandaExercicio3.Text = string.Empty;
            txtObjetoDemandaExercicio4.Text = string.Empty;

            txtCodigoDemandaExercicio1.Text = string.Empty;
            txtCodigoDemandaExercicio2.Text = string.Empty;
            txtCodigoDemandaExercicio3.Text = string.Empty;
            txtCodigoDemandaExercicio4.Text = string.Empty;                   
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

        protected void CamposCarregarValores()
        {
            using (var proxyEstrutura = new ProxyEstruturaAssistenciaSocial())
            {
                hdfIdRecursosHumanos.Value = "0";
                this.CarregarCombos(proxyEstrutura);
                this.CarregarAvaliacoes(proxyEstrutura);

           

                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {
                        this.CarregarRecursoFinanceiroPublico(proxy, proxyEstrutura);
                    }
                }
                else
                {
                    this.AplicarRegraExibicaoLayoutServicosChanged();
                    this.AplicarRegraBloqueioDesbloqueio();
                }
            }
        }
        protected void CarregarRecursoFinanceiroPublico(ProxyRedeProtecaoSocial proxy, ProxyEstruturaAssistenciaSocial proxyEstrutura)
        {
            var servico = proxy.Service.GetServicoRecursoFinanceiroPublicoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            if (servico == null) return;

            this.CarregarCaracterizacaoServico(proxyEstrutura, servico);
            this.CarregarUsuarios(servico, proxyEstrutura);
            this.CarregarRecursosHumanos(proxy, servico);
            this.CarregarFuncionamento(proxyEstrutura, servico);
            this.CarregarRecursosFinanceiros(servico);
            this.CarregaDemandasParlamentares(servico);

        }
        protected void ExibirFrameInicialmenteAtivo()
        {
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
        }
        protected void CarregarProgramas(ProxyProgramas proxy, int idServicosRecursosFinanceiros, int idCentro)
        {
            var programas = proxy.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(idServicosRecursosFinanceiros, idCentro);

            if (programas != null)
            {
                var lst = programas.OrderBy(t => t.IdTipoProtecao)
                                   .GroupBy(s => s.ProtecaoSocial)
                                   .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) }).ToList();

                if (lst != null)
                {
                    lstRecursos.DataSource = lst;
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
        protected void CarregarUsuarios(ServicoRecursoFinanceiroPublicoInfo obj, ProxyEstruturaAssistenciaSocial proxyEstrutura)
        {

            if (obj.Id != 0)
                trAssociacaoProgramas.Visible = true;

            using (var proxyprogramas = new ProxyProgramas())
            {
                CarregarProgramas(proxyprogramas);
                CarregarProgramas(proxyprogramas, obj.Id, obj.IdLocalExecucao);
            }
           

            rblMoradiaUsuarios.SelectedValue = obj.IdRegiaoMoradia.HasValue ? obj.IdRegiaoMoradia.Value.ToString() : String.Empty;
            rblSexo.SelectedValue = obj.IdSexo.HasValue ? obj.IdSexo.Value.ToString() : String.Empty;
            CarregarSituacoes(proxyEstrutura);
            if (obj.SituacoesEspecificas != null && obj.SituacoesEspecificas.Count > 0)
                foreach (ListItem i in situacoesEspecificas.Items)
                    i.Selected = obj.SituacoesEspecificas.Any(s => s.Id == Convert.ToInt32(i.Value));

        }


        protected void CarregarCaracterizacaoServico(ProxyEstruturaAssistenciaSocial proxyEstrutura, ServicoRecursoFinanceiroPublicoInfo servico)
        {
            if (new List<Int32>() {   R_TIPO_SERVICO.SERVICO_ATENDIMENTO_FAMILIAS_REALIZADO_FORA_CRAS
                                    , R_TIPO_SERVICO.SERVICO_EXCLUSIVO_ACOES_COMP_PROG_ESTAD_TRANSF_RENDA
                                    , R_TIPO_SERVICO.OUTRO_COD_156
                                    , R_TIPO_SERVICO.SCFV_PESSOAS_DEFIC_INTELEC_ACIMA_30_ANOS_EGRESSAS}
                                    .Contains(servico.UsuarioTipoServico.IdTipoServico))
            {
                servico.UsuarioTipoServico.IdTipoServico = R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_138;
            }

            if (new List<Int32>() {  R_TIPO_SERVICO.SERVICO_ATEND_FAMIL_INDIV_SIT_RISCO_S_REALIZADO_FORA_CREAS
                                   , R_TIPO_SERVICO.SERVICO_ATEND_ESPEC_COMPLEMENTAR_PAEFI_OFER_FORA_CREAS 
                                   , R_TIPO_SERVICO.OUTRO_COD_159 }.Contains(servico.UsuarioTipoServico.IdTipoServico))
            {
                servico.UsuarioTipoServico.IdTipoServico = R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_145;
            }

            rblTipoProtecao.SelectedValue = servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial.ToString();
            CarregarTiposServicos(proxyEstrutura);


            ddlTipoServico.SelectedValue = servico.UsuarioTipoServico.IdTipoServico.ToString();
            ddlPublicoAlvo.SelectedValue = servico.IdUsuarioTipoServico.ToString();

            rblCaracteristicasTerritorio.SelectedValue = servico.IdCaracteristicasTerritorio.ToString();
            ddlAbrangencia.SelectedValue = servico.IdAbrangenciaServico.ToString();

            if (servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial == 2)
            {
                CarregarOfertaServico(true);

                rblCaracteristicaOferta.SelectedValue = servico.IdCaracteristicaOfertaServico.ToString();
            }
            else
            {
                CarregarOfertaServico(false);
            }


                if (servico.IdUsuarioTipoServico == 37 && servico.UsuarioTipoServico.IdTipoServico == 146)
                {
                    trAuxilioReclusaoPensaoMorte.Visible = true;


                    if (servico.AtendeCriancasAuxilioReclusao == true)
                    {
                        rblCriancasAuxilioReclusao.SelectedValue = "1";
                        trValCriancasAuxilioReclusao.Visible = true;

                        txtCriancaAuxilioReclusaoFeitos.Text = servico.CriancaAuxilioReclusaoFeitos.ToString();
                        txtCriancaAuxilioReclusaoAprovados.Text = servico.CriancaAuxilioReclusaoAprovados.ToString();
                        txtCriancaAuxilioReclusaoNegado.Text = servico.CriancaAuxilioReclusaoNegado.ToString();
                    }
                    else if (servico.AtendeCriancasAuxilioReclusao == false)
                    {
                        rblCriancasAuxilioReclusao.SelectedValue = "0";
                        trValCriancasAuxilioReclusao.Visible = false;
                    }
                    else if (servico.AtendeCriancasAuxilioReclusao == null)
                    {
                        trValCriancasAuxilioReclusao.Visible = false;
                    }


                    if (servico.AtendeCriancasAuxilioReclusaoExercicio2025 == true)
                    {
                        rblCriancasAuxilioReclusaoExercicio2025.SelectedValue = "1";
                        trValCriancasAuxilioReclusaoExercicio2025.Visible = true;

                        txtCriancaAuxilioReclusaoFeitosExercicio2025.Text = servico.CriancaAuxilioReclusaoFeitosExercicio2025.ToString();
                        txtCriancaAuxilioReclusaoAprovadosExercicio2025.Text = servico.CriancaAuxilioReclusaoAprovadosExercicio2025.ToString();
                        txtCriancaAuxilioReclusaoNegadoExercicio2025.Text = servico.CriancaAuxilioReclusaoNegadoExercicio2025.ToString();
                    }
                    else if (servico.AtendeCriancasAuxilioReclusaoExercicio2025 == false)
                    {
                        rblCriancasAuxilioReclusaoExercicio2025.SelectedValue = "0";
                        trValCriancasAuxilioReclusaoExercicio2025.Visible = false;
                    }
                    else if (servico.AtendeCriancasAuxilioReclusaoExercicio2025 == null)
                    {
                        trValCriancasAuxilioReclusaoExercicio2025.Visible = false;
                    }


                    if (servico.AtendeCriancasPensaoMorte == true)
                    {
                        rblCriancasPensaoMorte.SelectedValue = "1";
                        trValCriancasPensaoMorte.Visible = true;

                        txtCriancasPensaoMorteFeitos.Text = servico.CriancaPensaoMorteFeitos.ToString();
                        txtCriancasPensaoMorteAprovados.Text = servico.CriancaPensaoMorteAprovados.ToString();
                        txtCriancasPensaoMorteNegado.Text = servico.CriancaPensaoMorteNegado.ToString();
                    }
                    else if (servico.AtendeCriancasPensaoMorte == false)
                    {
                        rblCriancasPensaoMorte.SelectedValue = "0";
                        trValCriancasPensaoMorte.Visible = false;
                    }
                    else if (servico.AtendeCriancasPensaoMorte == null)
                    {
                        trValCriancasPensaoMorte.Visible = false;
                    }


                    if (servico.AtendeCriancasPensaoMorteExercicio2025 == true)
                    {
                        rblCriancasPensaoMorteExercicio2025.SelectedValue = "1";
                        trValCriancasPensaoMorteExercicio2025.Visible = true;

                        txtCriancasPensaoMorteFeitosExercicio2025.Text = servico.CriancaPensaoMorteFeitosExercicio2025.ToString();
                        txtCriancasPensaoMorteAprovadosExercicio2025.Text = servico.CriancaPensaoMorteAprovadosExercicio2025.ToString();
                        txtCriancasPensaoMorteNegadoExercicio2025.Text = servico.CriancaPensaoMorteNegadoExercicio2025.ToString();
                    }
                    else if (servico.AtendeCriancasPensaoMorteExercicio2025 == false)
                    {
                        rblCriancasPensaoMorteExercicio2025.SelectedValue = "0";
                        trValCriancasPensaoMorteExercicio2025.Visible = false;
                    }
                    else if (servico.AtendeCriancasPensaoMorteExercicio2025 == null)
                    {
                        trValCriancasPensaoMorteExercicio2025.Visible = false;
                    }

                }
                else
                {
                    trAuxilioReclusaoPensaoMorte.Visible = false;
                }
            

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

                        var consorcio = proxyEstrutura.Service.GetConsorcioPublico(servico.Id);
            
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
            

            chkNaoPossuiTecnicoResponsavel.Checked = servico.PossuiTecnicoResponsavel.HasValue ? !servico.PossuiTecnicoResponsavel.Value : false;
            txtTecnicoResponsavel.Text = chkNaoPossuiTecnicoResponsavel.Checked ? "" : !String.IsNullOrWhiteSpace(servico.NomeTecnicoResponsavel) ? servico.NomeTecnicoResponsavel : String.Empty;
            txtTecnicoResponsavel.Enabled = !chkNaoPossuiTecnicoResponsavel.Checked;


            CarregarTiposServicosNaoTipificados(proxyEstrutura);
            ddlTipoServicoNaoTipificado.SelectedValue = servico.IdTipoServicoNaoTipificado.HasValue ? servico.IdTipoServicoNaoTipificado.Value.ToString() : "0";


            #region Carregar: serviço não tipificado
            if (servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_138
                || servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_145)
            {
                tbNaoTipificado.Visible = true;
                tbNaoTipificadoDetalhado.Visible = tbNaoTipificadoObjetivo.Visible = ddlTipoServicoNaoTipificado.SelectedItem.Text == "Outro";
            } 
            #endregion

            if (servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_NAO_TIPIF_RESOLUCAO_No_109_CNAS_11_11_2009_COD_153)
            {
                tbNaoTipificadoDetalhado.Visible = tbNaoTipificadoObjetivo.Visible = true;
            }

            txtNaotipificado.Text = !String.IsNullOrWhiteSpace(servico.DescricaoServicoNaoTipificado) ? servico.DescricaoServicoNaoTipificado : String.Empty;
            txtObjetivoNaoTipificado.Text = !String.IsNullOrWhiteSpace(servico.ObjetivoServicoNaoTipificado) ? servico.ObjetivoServicoNaoTipificado : String.Empty;

            if (servico.IdTipoServicoNaoTipificado.HasValue)
                CarregarUsuarios(proxyEstrutura, true);
            else
                CarregarUsuarios(proxyEstrutura);

            if (servico.UsuarioTipoServico.IdTipoServico == 146 && servico.IdUsuarioTipoServico == 40 ||
                servico.UsuarioTipoServico.IdTipoServico == 148 && servico.IdUsuarioTipoServico == 43 ||
                servico.UsuarioTipoServico.IdTipoServico == 150 && servico.IdUsuarioTipoServico == 47 ||
                servico.UsuarioTipoServico.IdTipoServico == 150 && servico.IdUsuarioTipoServico == 48)
            {
                tdAtendimentoDependente.Visible = true;
                if (servico.AtendeDependentes.Value)
                {
                    rblAtendeDependentes.SelectedValue = servico.AtendeDependentes.Value == true ? "1":"0";
                    tdProgramaRecomeco.Visible = true;
                    rblProgramaRecomeco.SelectedValue = servico.AtendeProgramaRecomeco.Value == true ? "1": "0";
                }
            }
            ddlPublicoAlvo_SelectedIndexChanged(null, null);
            AposSalvoNaoPermitirEdicaoCamposCaracterizacao(servico);

        }

        private void CarregarOfertaServico(bool p)
        {
            trCaracteristicaOferta.Visible = p;
        }

        
        protected void CarregarFuncionamento(ProxyEstruturaAssistenciaSocial proxyEstrutura, ServicoRecursoFinanceiroPublicoInfo servico)
        {
            #region Carrega: [Data de início de funcionamento]
            txtDataInicio.Text = servico.DataFuncionamentoServico.HasValue ? servico.DataFuncionamentoServico.Value.ToShortDateString() : String.Empty; 
            #endregion

            CarregarAvaliacoes(proxyEstrutura);

            if (servico.IdTipoServicoNaoTipificado.HasValue)
            {
                CarregarAtividades(proxyEstrutura, true);
            }
            else
            {
                CarregarAtividades(proxyEstrutura);
            }

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
                this.DistribuirCapacidadeLA(servico);
                this.DistribuirCapacidadePSC(servico);

                this.DistribuirMediaMensalLA(servico);
                this.DistribuirMediaMensalPSC(servico);
            }
            else
            {
                this.DistribuirCapacidade(servico);
                this.DistribuirMediaMensal(servico);
            }
            this.AplicarRegraExibicaoLayoutServicosChanged();
            #endregion

            #region Carregar: Quadro [Este serviço funciona quantas horas por semana?]
            rblHorasSemana.SelectedValue = servico.IdHorasSemana.ToString();
            rblDiasSemana.SelectedValue = servico.QuantidadeDiasSemana.ToString();
            rblAvaliacaoGestor.SelectedValue = servico.IdAvaliacaoServico.ToString();
            #endregion
        }
        protected void CarregarRecursosFinanceiros(ServicoRecursoFinanceiroPublicoInfo entidade)
        {
            int idService = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));


            #region Exercicios
            

            #region Exercicio 4
            var fundoExercicio4 = entidade.ServicosRecursosFinanceirosFundosPublicoInfo
                    .Where(x => x.ServicoRecursoFinanceiroPublicoInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroPublico.Exercicios[3]).FirstOrDefault();
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
                txtFEASAnoAnteriorExercicio4.Text = fundoExercicio4.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
                txtFEASDemandasExercicio4.Text = fundoExercicio4.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio4.Text = fundoExercicio4.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");
                CarregarReprogramacaoExercicio4(fundoExercicio4);
                CarregarDemandasExercicio4(fundoExercicio4);

                rblOutrasFontesExercicio4.SelectedValue = fundoExercicio4.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio4.ExisteOutraFonteFinanciamento.Value).ToString() : "0";
                rblOutrasFontesExercicio4_SelectedIndexChanged(null, null);
                if (fundoExercicio4.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio4.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var item in fundoExercicio4.ServicoRecursoFinanceiroPublicoFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2025);
                    }

                    this.SessaoFontesRecursosExercicio4 = fundoExercicio4.ServicoRecursoFinanceiroPublicoFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio4();
                }
            }
            #endregion

            #region Exercicio 3
            var fundoExercicio3 = entidade.ServicosRecursosFinanceirosFundosPublicoInfo
                    .Where(x => x.ServicoRecursoFinanceiroPublicoInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroPublico.Exercicios[2]).FirstOrDefault();
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
                txtFEASAnoAnteriorExercicio3.Text = fundoExercicio3.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
                txtFEASDemandasExercicio3.Text = fundoExercicio3.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio3.Text = fundoExercicio3.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");
                CarregarReprogramacaoExercicio3(fundoExercicio3);
                CarregarDemandasExercicio3(fundoExercicio3);

                rblOutrasFontesExercicio3.SelectedValue = fundoExercicio3.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio3.ExisteOutraFonteFinanciamento.Value).ToString() : "0";
                rblOutrasFontesExercicio3_SelectedIndexChanged(null, null);

                if (fundoExercicio3.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio3.ExisteOutraFonteFinanciamento.Value)
                {
                    
                    foreach (var item in fundoExercicio3.ServicoRecursoFinanceiroPublicoFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2024);
                    }
                    
                    this.SessaoFontesRecursosExercicio3 = fundoExercicio3.ServicoRecursoFinanceiroPublicoFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio3();
                }
            }
            #endregion

            #region Exercicio 2
            var fundoExercicio2 = entidade.ServicosRecursosFinanceirosFundosPublicoInfo
                    .Where(x => x.ServicoRecursoFinanceiroPublicoInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroPublico.Exercicios[1]).FirstOrDefault();
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
                txtFEASAnoAnteriorExercicio2.Text = fundoExercicio2.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
                txtFEASDemandasExercicio2.Text = fundoExercicio2.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio2.Text = fundoExercicio2.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");
                CarregarReprogramacaoExercicio2(fundoExercicio2);
                CarregarDemandasExercicio2(fundoExercicio2);

                rblOutrasFontesExercicio2.SelectedValue = fundoExercicio2.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio2.ExisteOutraFonteFinanciamento.Value).ToString() : "0";
                rblOutrasFontesExercicio2_SelectedIndexChanged(null, null);
                if (fundoExercicio2.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio2.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var item in fundoExercicio2.ServicoRecursoFinanceiroPublicoFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2023);
                    }
                    this.SessaoFontesRecursosExercicio2 = fundoExercicio2.ServicoRecursoFinanceiroPublicoFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio2();
                }
            }
            #endregion

            #region Exercicio 1
            var fundoExercicio1 = entidade.ServicosRecursosFinanceirosFundosPublicoInfo
                    .Where(x => x.ServicoRecursoFinanceiroPublicoInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroPublico.Exercicios[0]).FirstOrDefault();
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

                CarregarReprogramacaoExercicio1(fundoExercicio1);

                rblOutrasFontesExercicio1.SelectedValue = fundoExercicio1.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio1.ExisteOutraFonteFinanciamento.Value).ToString() : "0";
                rblOutrasFontesExercicio1_SelectedIndexChanged(null, null);
        
                if (fundoExercicio1.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio1.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var item in fundoExercicio1.ServicoRecursoFinanceiroPublicoFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2022);
                    }
                    this.SessaoFontesRecursosExercicio1 = fundoExercicio1.ServicoRecursoFinanceiroPublicoFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio1();
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
        
       
        protected void CarregarReprogramacaoExercicio1(ServicoRecursoFinanceiroFundosPublicoInfo fundoExercicio1)
        {
            txtFEASAnoAnteriorExercicio1.Text = fundoExercicio1.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
        }
        protected void CarregarReprogramacaoExercicio2(ServicoRecursoFinanceiroFundosPublicoInfo fundoExercicio2)
        {
            txtFEASAnoAnteriorExercicio2.Text = fundoExercicio2.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
        }
        protected void CarregarReprogramacaoExercicio3(ServicoRecursoFinanceiroFundosPublicoInfo fundoExercicio3)
        {
            txtFEASAnoAnteriorExercicio3.Text = fundoExercicio3.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
        }
        protected void CarregarReprogramacaoExercicio4(ServicoRecursoFinanceiroFundosPublicoInfo fundoExercicio4)
        {
            txtFEASAnoAnteriorExercicio4.Text = fundoExercicio4.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
        }

        protected void CarregarDemandasExercicio1(ServicoRecursoFinanceiroFundosPublicoInfo fundoExercicio1) 
        {
            txtFEASDemandasExercicio1.Text = fundoExercicio1.ValorEstadualDemandasParlamentares.ToString("N2");
        }
        protected void CarregarDemandasExercicio2(ServicoRecursoFinanceiroFundosPublicoInfo fundoExercicio2)
        {
            txtFEASDemandasExercicio2.Text = fundoExercicio2.ValorEstadualDemandasParlamentares.ToString("N2");
        }
        protected void CarregarDemandasExercicio3(ServicoRecursoFinanceiroFundosPublicoInfo fundoExercicio3)
        {
            txtFEASDemandasExercicio3.Text = fundoExercicio3.ValorEstadualDemandasParlamentares.ToString("N2");
        }
        protected void CarregarDemandasExercicio4(ServicoRecursoFinanceiroFundosPublicoInfo fundoExercicio4)
        {
            txtFEASDemandasExercicio4.Text = fundoExercicio4.ValorEstadualDemandasParlamentares.ToString("N2");
        }

        protected void CarregaDemandasParlamentares(ServicoRecursoFinanceiroPublicoInfo Servico)
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {


             var demandas = Servico.ServicosRecursosFinanceirosFundosPublicoInfo.Where(s => s.Exercicio >= 2022);
             
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


        protected void CarregarRecursosFinanceirosFonteRecursosExercicio1()
        {
            lstRecursosAdicionadosExercicio1.DataSource = this.SessaoFontesRecursosExercicio1;
            lstRecursosAdicionadosExercicio1.DataBind();

            if (lstRecursosAdicionadosExercicio1.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio1.Visible = lstRecursosAdicionadosExercicio1.Visible = true;
            }
        }
        protected void CarregarRecursosFinanceirosFonteRecursosExercicio2()
        {
            lstRecursosAdicionadosExercicio2.DataSource = this.SessaoFontesRecursosExercicio2;
            lstRecursosAdicionadosExercicio2.DataBind();

            if (lstRecursosAdicionadosExercicio2.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio2.Visible = lstRecursosAdicionadosExercicio2.Visible = true;
            }
        }
        protected void CarregarRecursosFinanceirosFonteRecursosExercicio3()
        {
            lstRecursosAdicionadosExercicio3.DataSource = this.SessaoFontesRecursosExercicio3;
            lstRecursosAdicionadosExercicio3.DataBind();
            

            if (lstRecursosAdicionadosExercicio3.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio3.Visible = lstRecursosAdicionadosExercicio3.Visible = true;
               
            }
        }
        protected void CarregarRecursosFinanceirosFonteRecursosExercicio4()
        {
            lstRecursosAdicionadosExercicio4.DataSource = this.SessaoFontesRecursosExercicio4;
            lstRecursosAdicionadosExercicio4.DataBind();

            if (lstRecursosAdicionadosExercicio4.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio4.Visible = lstRecursosAdicionadosExercicio4.Visible = true;
            }
        }

        protected void CarregarCombos(ProxyEstruturaAssistenciaSocial proxy)
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

            rblTipoProtecao.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(s => s.Id == 1 || s.Id == 2 || s.Id == 3);
            rblTipoProtecao.DataValueField = "Id";
            rblTipoProtecao.DataTextField = "Nome";
            rblTipoProtecao.DataBind();
            ListItem itemToRemove = rblTipoProtecao.Items.FindByValue("5");
            ListItem itemToRemoveBeneficios = rblTipoProtecao.Items.FindByValue("4");
            if (itemToRemove != null)
                rblTipoProtecao.Items.Remove(itemToRemove);

            if (itemToRemoveBeneficios != null)
                rblTipoProtecao.Items.Remove(itemToRemoveBeneficios);
        }

        protected void CarregarTiposServicos(ProxyEstruturaAssistenciaSocial proxy)
        {
            ddlTipoServico.DataTextField = "Nome";
            ddlTipoServico.DataValueField = "Id";
            
            ddlTipoServico.DataSource = proxy.Service.GetTiposServicoByTipoProtecaoSocial(Convert.ToInt32(rblTipoProtecao.SelectedValue)).Where(t => t.Id != 135 && t.Id != 139 && t.Id != 144);
            ddlTipoServico.DataBind();
            

            ListItem itemToRemove = ddlTipoServico.Items.FindByValue("142");
            if (itemToRemove != null)
            {
                ddlTipoServico.Items.Remove(itemToRemove);
            }

            Util.InserirItemEscolha(ddlTipoServico);
        }
        protected void CarregarSituacoes(ProxyEstruturaAssistenciaSocial proxy)
        {
            situacoesEspecificas.DataTextField = "Nome";
            situacoesEspecificas.DataValueField = "Id";
            situacoesEspecificas.DataSource = proxy.Service.GetSituacoesEspecificasByUsuario(Convert.ToInt32(ddlPublicoAlvo.SelectedValue));
            situacoesEspecificas.DataBind();
        }
        protected void CarregarAvaliacoes(ProxyEstruturaAssistenciaSocial proxy)
        {
            rblAvaliacaoGestor.DataTextField = "Descricao";
            rblAvaliacaoGestor.DataValueField = "Id";
            rblAvaliacaoGestor.DataSource = proxy.Service.GetAvaliacoes();
            rblAvaliacaoGestor.DataBind();
        }
        protected void CarregarAtividades(ProxyEstruturaAssistenciaSocial proxy, Boolean naoTipificado = false)
        {
            lstAtividades.DataValueField = "Id";
            lstAtividades.DataTextField = "Nome";
            if (!naoTipificado)
                lstAtividades.DataSource = proxy.Service.GetAtividadesSocioAssistenciaisByTipoServico(Convert.ToInt32(ddlTipoServico.SelectedValue));
            else
                lstAtividades.DataSource = proxy.Service.GetAtividadesSocioAssistenciaisByTipoServico(Convert.ToInt32(ddlTipoServicoNaoTipificado.SelectedValue));
            lstAtividades.DataBind();
        }
        protected void CarregarUsuarios(ProxyEstruturaAssistenciaSocial proxy, Boolean naoTipificado = false)
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

        protected void CarregarProgramas(ProxyProgramas proxy)
        {
            ddlProgramaBeneficio.DataValueField = "Id";
            ddlProgramaBeneficio.DataTextField = "Nome";
            ddlProgramaBeneficio.DataSource = proxy.Service.GetProgramasByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            ddlProgramaBeneficio.DataBind();
            Util.InserirItemEscolha(ddlProgramaBeneficio);
        }


        protected void CarregarTiposServicosNaoTipificados(ProxyEstruturaAssistenciaSocial proxy) //PMAS 2016
        {
            ddlTipoServicoNaoTipificado.DataTextField = "Nome";
            ddlTipoServicoNaoTipificado.DataValueField = "Id";
            ddlTipoServicoNaoTipificado.DataSource = rblTipoProtecao.SelectedValue != "3" ?
            proxy.Service.GetTiposServicoNaoTipificadoByTipoProtecaoSocial(Convert.ToInt32(rblTipoProtecao.SelectedValue)).Where(t => t.Id != 160).ToList() : new List<TipoServicoInfo>();
            ddlTipoServicoNaoTipificado.DataBind();
            Util.InserirItemEscolha(ddlTipoServicoNaoTipificado);
        }
        protected void CarregarRecursosHumanos(ProxyRedeProtecaoSocial proxy, ServicoRecursoFinanceiroPublicoInfo obj)
        {
            var recursoshumanos = proxy.Service.GetRecursosHumanosPublicoByIdServicoRecursoFinanceiro(obj.Id);
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

                txtEstagiarios.Text = recursoshumanos.Estagiarios.ToString();
                txtVoluntarios.Text = recursoshumanos.Voluntarios.ToString();

                txtExclusivoServico.Text = recursoshumanos.ExclusivoServico.ToString();
                txtOutroServicos.Text = recursoshumanos.OutrosServicosAssistenciais.ToString();
            }

            this.CarregarTotalRh();
        }
        protected ServicoRecursoFinanceiroPublicoRecursosHumanosInfo CarregarRH()
        {
            ServicoRecursoFinanceiroPublicoRecursosHumanosInfo recursosHumanos = new ServicoRecursoFinanceiroPublicoRecursosHumanosInfo();
            recursosHumanos.Id = Convert.ToInt32(hdfIdRecursosHumanos.Value);
            if (!String.IsNullOrEmpty(txtSemEscolaridade.Text))
                recursosHumanos.SemEscolarizacao = Convert.ToInt32(txtSemEscolaridade.Text);
            if (!String.IsNullOrEmpty(txtNivelFundamental.Text))
                recursosHumanos.NivelFundamental = Convert.ToInt32(txtNivelFundamental.Text);
            if (!String.IsNullOrEmpty(txtNivelMedio.Text))
                recursosHumanos.NivelMedio = Convert.ToInt32(txtNivelMedio.Text);
            if (!String.IsNullOrEmpty(txtSuperior.Text))
                recursosHumanos.NivelSuperior = Convert.ToInt32(txtSuperior.Text);
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
        protected void CarregarTotalRh()
        {
            try
            {
                int?[] array = {Util.TryParseInt32(txtSemEscolaridade.Text)
                               ,Util.TryParseInt32(txtNivelFundamental.Text)
                          , Util.TryParseInt32(txtNivelMedio.Text)
                          , Util.TryParseInt32(txtSuperior.Text)                          
                       };


                int?[] arrayEstVol = {Util.TryParseInt32(txtEstagiarios.Text)
                                , Util.TryParseInt32(txtVoluntarios.Text)
                       };

                lblTotalRh.Text = array.Sum().ToString(); ;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region Helper [Funcionamento] [Servicos]
        private void DistribuirCapacidade(ServicoRecursoFinanceiroPublicoInfo servico)
        {
            ServicoRecursoFinanceiroPublicoCapacidadeInfo capacidadeExercicio1 = servico.ServicosRecursosFinanceiroPublicoCapacidade.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtCapacidadeExercicio1.Text = capacidadeExercicio1 != null ? capacidadeExercicio1.Capacidade != null ? capacidadeExercicio1.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoCapacidadeInfo capacidadeExercicio2 = servico.ServicosRecursosFinanceiroPublicoCapacidade.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtCapacidadeExercicio2.Text = capacidadeExercicio2 != null ? capacidadeExercicio2.Capacidade != null ? capacidadeExercicio2.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoCapacidadeInfo capacidadeExercicio3 = servico.ServicosRecursosFinanceiroPublicoCapacidade.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtCapacidadeExercicio3.Text = capacidadeExercicio3 != null ? capacidadeExercicio3.Capacidade != null ? capacidadeExercicio3.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoCapacidadeInfo capacidadeExercicio4 = servico.ServicosRecursosFinanceiroPublicoCapacidade.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtCapacidadeExercicio4.Text = capacidadeExercicio4 != null && capacidadeExercicio4.Exercicio < 2025? capacidadeExercicio4.Capacidade != null ? capacidadeExercicio4.Capacidade.ToString() : string.Empty : string.Empty;
        }
        private void DistribuirCapacidadeLA(ServicoRecursoFinanceiroPublicoInfo servico)
        {
            ServicoRecursoFinanceiroPublicoCapacidadeLAInfo capacidadeLAExercicio1 = servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtCapacidadeLAExercicio1.Text = capacidadeLAExercicio1 != null ? capacidadeLAExercicio1.Capacidade != null ? capacidadeLAExercicio1.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoCapacidadeLAInfo capacidadeLAExercicio2 = servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtCapacidadeLAExercicio2.Text = capacidadeLAExercicio2 != null ? capacidadeLAExercicio2.Capacidade != null ? capacidadeLAExercicio2.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoCapacidadeLAInfo capacidadeLAExercicio3 = servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtCapacidadeLAExercicio3.Text = capacidadeLAExercicio3 != null ? capacidadeLAExercicio3.Capacidade != null ? capacidadeLAExercicio3.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoCapacidadeLAInfo capacidadeLAExercicio4 = servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtCapacidadeLAExercicio4.Text = capacidadeLAExercicio4 != null ? capacidadeLAExercicio4.Capacidade != null ? capacidadeLAExercicio4.Capacidade.ToString() : string.Empty : string.Empty;
        }
        private void DistribuirCapacidadePSC(ServicoRecursoFinanceiroPublicoInfo servico)
        {
            ServicoRecursoFinanceiroPublicoCapacidadePSCInfo capacidadePSCExercicio1 = servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtCapacidadePSCExercicio1.Text = capacidadePSCExercicio1 != null ? capacidadePSCExercicio1.Capacidade != null ? capacidadePSCExercicio1.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoCapacidadePSCInfo capacidadePSCExercicio2 = servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtCapacidadePSCExercicio2.Text = capacidadePSCExercicio2 != null ? capacidadePSCExercicio2.Capacidade != null ? capacidadePSCExercicio2.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoCapacidadePSCInfo capacidadePSCExercicio3 = servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtCapacidadePSCExercicio3.Text = capacidadePSCExercicio3 != null ? capacidadePSCExercicio3.Capacidade != null ? capacidadePSCExercicio3.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoCapacidadePSCInfo capacidadePSCExercicio4 = servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtCapacidadePSCExercicio4.Text = capacidadePSCExercicio4 != null ? capacidadePSCExercicio4.Capacidade != null ? capacidadePSCExercicio4.Capacidade.ToString() : string.Empty : string.Empty;
        }

        private void DistribuirMediaMensal(ServicoRecursoFinanceiroPublicoInfo servico)
        {
            ServicoRecursoFinanceiroPublicoMediaMensalInfo mediaMensalExercicio1 = servico.ServicosRecursosFinanceiroPublicoMediaMensal.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtMediaMensalExercicio1.Text = mediaMensalExercicio1 != null ? mediaMensalExercicio1.MediaMensal != null ? mediaMensalExercicio1.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoMediaMensalInfo mediaMensalExercicio2 = servico.ServicosRecursosFinanceiroPublicoMediaMensal.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtMediaMensalExercicio2.Text = mediaMensalExercicio2 != null ? mediaMensalExercicio2.MediaMensal != null ? mediaMensalExercicio2.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoMediaMensalInfo mediaMensalExercicio3 = servico.ServicosRecursosFinanceiroPublicoMediaMensal.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtMediaMensalExercicio3.Text = mediaMensalExercicio3 != null ? mediaMensalExercicio3.MediaMensal != null ? mediaMensalExercicio3.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoMediaMensalInfo mediaMensalExercicio4 = servico.ServicosRecursosFinanceiroPublicoMediaMensal.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtMediaMensalExercicio4.Text = mediaMensalExercicio4 != null ? mediaMensalExercicio4.MediaMensal != null ? mediaMensalExercicio4.MediaMensal.ToString() : string.Empty : string.Empty;
        }
        private void DistribuirMediaMensalLA(ServicoRecursoFinanceiroPublicoInfo servico)
        {
            ServicoRecursoFinanceiroPublicoMediaMensalLAInfo mediaMensalLAExercicio1 = servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtMediaMensalLAExercicio1.Text = mediaMensalLAExercicio1 != null ? mediaMensalLAExercicio1.MediaMensal != null ? mediaMensalLAExercicio1.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoMediaMensalLAInfo mediaMensalLAExercicio2 = servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtMediaMensalLAExercicio2.Text = mediaMensalLAExercicio2 != null ? mediaMensalLAExercicio2.MediaMensal != null ? mediaMensalLAExercicio2.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoMediaMensalLAInfo mediaMensalLAExercicio3 = servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtMediaMensalLAExercicio3.Text = mediaMensalLAExercicio3 != null ? mediaMensalLAExercicio3.MediaMensal != null ? mediaMensalLAExercicio3.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoMediaMensalLAInfo mediaMensalLAExercicio4 = servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtMediaMensalLAExercicio4.Text = mediaMensalLAExercicio4 != null ? mediaMensalLAExercicio4.MediaMensal != null ? mediaMensalLAExercicio4.MediaMensal.ToString() : string.Empty : string.Empty;

        }
        private void DistribuirMediaMensalPSC(ServicoRecursoFinanceiroPublicoInfo servico)
        {
            ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo mediaMensalPSCExercicio1 = servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtMediaMensalPSCExercicio1.Text = mediaMensalPSCExercicio1 != null ? mediaMensalPSCExercicio1.MediaMensal != null ? mediaMensalPSCExercicio1.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo mediaMensalPSCExercicio2 = servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtMediaMensalPSCExercicio2.Text = mediaMensalPSCExercicio2 != null ? mediaMensalPSCExercicio2.MediaMensal != null ? mediaMensalPSCExercicio2.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo mediaMensalPSCExercicio3 = servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtMediaMensalPSCExercicio3.Text = mediaMensalPSCExercicio3 != null ? mediaMensalPSCExercicio3.MediaMensal != null ? mediaMensalPSCExercicio3.MediaMensal.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo mediaMensalPSCExercicio4 = servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtMediaMensalPSCExercicio4.Text = mediaMensalPSCExercicio4 != null ? mediaMensalPSCExercicio4.MediaMensal != null ? mediaMensalPSCExercicio4.MediaMensal.ToString() : string.Empty : string.Empty;
        }
        #endregion

        #endregion // FIM DO CARREGAR

        #region Eventos 

      
        #region [Adicionar |  Salvar | Voltar | Excluir]
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            lblInconsistencias.Text = String.Empty;
            tbInconsistencias.Visible = false;
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));
            try
            {
                if (SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo != null)
                {
                    var existe = SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo.Where(x => x.IdProgramaProjeto == Convert.ToInt32(ddlProgramaBeneficio.SelectedValue)).ToList();
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
                        || ddlProgramaBeneficio.SelectedItem.Text.Contains("Prospera Família")
                        || ddlProgramaBeneficio.SelectedItem.Text.Contains("Auxílio Aluguel")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("ação jovem")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("renda cidadã")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("teste transferencia renda")
                        || ddlProgramaBeneficio.SelectedItem.Text.Contains("Fortalecimento do CadÚnico")
                        //|| ddlProgramaBeneficio.SelectedItem.Text.Contains("Fortalecimento da Vigilância Socioassistencial")
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


        protected void ValidaTextBoxAuxilioReclusao()
        {
            var erros = new List<string>();

            if (exercicioDesbloqueado == 2024)
            {
                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoFeitos.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos feitos em 2024', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoAprovados.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram aprovados', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoNegado.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram negados', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }

            }
            else if (exercicioDesbloqueado == 2025)
            {
                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoFeitosExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos feitos em 2025', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoAprovadosExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram aprovados', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoNegadoExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram negados', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }
            }


            if (erros.Count > 0)
                throw new Exception(Extensions.Concat(erros, System.Environment.NewLine));
        }


        private void ValidaTextBoxPensaoMorte()
        {
            var erros = new List<string>();

            if (exercicioDesbloqueado == 2024)
            {
                if (String.IsNullOrEmpty(txtCriancasPensaoMorteFeitos.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos feitos em 2024', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancasPensaoMorteAprovados.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram aprovados', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancasPensaoMorteNegado.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram negados', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }
            }
            else if (exercicioDesbloqueado == 2025)
            {

                if (String.IsNullOrEmpty(txtCriancasPensaoMorteFeitosExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos feitos em 2025', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancasPensaoMorteAprovadosExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram aprovados', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancasPensaoMorteNegadoExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram negados', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }
            }

            if (erros.Count > 0)
                throw new Exception(Extensions.Concat(erros, System.Environment.NewLine));
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            this.CarregarAbaInicialRecursosFinanceiros();

            #region propriedades
            var servico = new ServicoRecursoFinanceiroPublicoInfo();
            var idServico = !String.IsNullOrEmpty(Request.QueryString["id"]) ? Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])) : 0;
            var idUnidadeDecrypt = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            var idLocalDecrypt = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
            var idLocalExecucao = Convert.ToInt32(idLocalDecrypt);
            var action = (idServico == 0) ? "I" : "A";
            #endregion

            try
            {
                this.ObterLocalExecucaoPublico(servico, idLocalExecucao);

                servico.Id = idServico;
                servico.IdLocalExecucao = idLocalExecucao;

                #region Preencher: Tipo Servico
                servico.UsuarioTipoServico = new UsuarioTipoServicoInfo();
                if (ddlTipoServico.SelectedIndex != -1)
                {
                    servico.UsuarioTipoServico.IdTipoServico = Convert.ToInt32(ddlTipoServico.SelectedValue);
                }
                servico.UsuarioTipoServico.TipoServico = new TipoServicoInfo();
                if (rblTipoProtecao.SelectedIndex != -1)
                {
                    servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial = Convert.ToInt16(rblTipoProtecao.SelectedValue);
                }
                #endregion Tipo Servico

                #region Preencher: Publico Alvo
                if (ddlPublicoAlvo.SelectedIndex != -1)
                {
                    servico.IdUsuarioTipoServico = Convert.ToInt32(ddlPublicoAlvo.SelectedValue);
                }

                #endregion

                #region  Preencher: Caracterizacao do Servico
                if (ddlTipoServico.SelectedValue == "138" || ddlTipoServico.SelectedValue == "145")
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


                if (ddlTipoServico.SelectedValue == "146" && ddlPublicoAlvo.SelectedValue == "40" ||
                    ddlTipoServico.SelectedValue == "148" && ddlPublicoAlvo.SelectedValue == "43" ||
                    ddlTipoServico.SelectedValue == "150" && ddlPublicoAlvo.SelectedValue == "47" ||
                    ddlTipoServico.SelectedValue == "150" && ddlPublicoAlvo.SelectedValue == "48")
                {
                    if (!String.IsNullOrEmpty(rblAtendeDependentes.SelectedValue))
                    {
                        servico.AtendeDependentes = rblAtendeDependentes.SelectedValue == "1";
                        if (servico.AtendeDependentes.Value)
                        {
                            if (!String.IsNullOrEmpty(rblProgramaRecomeco.SelectedValue))
                            {
                                servico.AtendeProgramaRecomeco = rblProgramaRecomeco.SelectedValue == "1" ? true:false;
                            }
                            else
                            {
                                servico.AtendeProgramaRecomeco = false;
                            }
                             
                        }
                    }
                }

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
                
                servico.SituacoesEspecificas = new List<SituacaoEspecificaInfo>();
                
                foreach (ListItem situacaoEspecifica in situacoesEspecificas.Items)
                {
                    if (situacaoEspecifica.Selected)
                    {
                        servico.SituacoesEspecificas.Add(new SituacaoEspecificaInfo() { Id = Convert.ToInt32(situacaoEspecifica.Value) });
                    }
                }
                #endregion

                #region  Preencher: Usuarios

                if (!String.IsNullOrEmpty(rblSexo.SelectedValue))
                {
                    servico.IdSexo = Convert.ToInt32(rblSexo.SelectedValue);
                }

                if (!String.IsNullOrEmpty(rblMoradiaUsuarios.SelectedValue))
                {
                    servico.IdRegiaoMoradia = Convert.ToInt32(rblMoradiaUsuarios.SelectedValue);
                }

                if (servico.Id != 0)
                {
                    if (String.IsNullOrEmpty(rblIntegracaoRede.SelectedValue))
                    {
                        throw new Exception("Informe se o usuário deste serviço é beneficiário de algum programa, projeto ou benefício!");
                    }
                    else
                    {
                        servico.PossuiProgramaBeneficio = Convert.ToBoolean(Convert.ToInt32(rblIntegracaoRede.SelectedValue));
                    }
                }
                #endregion

                if (exercicioDesbloqueado == 2024)
                {
                    #region Crianças auxilio reclusao
                    if (!String.IsNullOrEmpty(rblCriancasAuxilioReclusao.SelectedValue))
                    {
                        if (rblCriancasAuxilioReclusao.SelectedValue == "1")
                        {
                            ValidaTextBoxAuxilioReclusao();
                            servico.AtendeCriancasAuxilioReclusao = true;

                            servico.CriancaAuxilioReclusaoFeitos = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoFeitos.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoFeitos.Text);
                            servico.CriancaAuxilioReclusaoAprovados = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoAprovados.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoAprovados.Text);
                            servico.CriancaAuxilioReclusaoNegado = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoNegado.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoNegado.Text);

                        }
                        else
                        {
                            servico.AtendeCriancasAuxilioReclusao = false;

                            servico.CriancaAuxilioReclusaoFeitos = 0;
                            servico.CriancaAuxilioReclusaoAprovados = 0;
                            servico.CriancaAuxilioReclusaoNegado = 0;

                        }
                    }
                    else
                    {
                        servico.AtendeCriancasAuxilioReclusao = null;

                        servico.CriancaAuxilioReclusaoFeitos = 0;
                        servico.CriancaAuxilioReclusaoAprovados = 0;
                        servico.CriancaAuxilioReclusaoNegado = 0;
                    }
                    #endregion  
                }

                if (exercicioDesbloqueado == 2024)
                {
                    #region Crianças Pensão Morte
                    if (!String.IsNullOrEmpty(rblCriancasPensaoMorte.SelectedValue))
                    {
                        if (rblCriancasPensaoMorte.SelectedValue == "1")
                        {
                            ValidaTextBoxPensaoMorte();
                            servico.AtendeCriancasPensaoMorte = true;
                            servico.CriancaPensaoMorteFeitos = String.IsNullOrEmpty(txtCriancasPensaoMorteFeitos.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteFeitos.Text);
                            servico.CriancaPensaoMorteAprovados = String.IsNullOrEmpty(txtCriancasPensaoMorteAprovados.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteAprovados.Text);
                            servico.CriancaPensaoMorteNegado = String.IsNullOrEmpty(txtCriancasPensaoMorteNegado.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteNegado.Text);
                        }
                        else
                        {
                            servico.AtendeCriancasPensaoMorte = false;

                            servico.CriancaPensaoMorteFeitos = 0;
                            servico.CriancaPensaoMorteAprovados = 0;
                            servico.CriancaPensaoMorteNegado = 0;
                        }
                    }
                    else
                    {
                        servico.AtendeCriancasPensaoMorte = null;

                        servico.CriancaPensaoMorteFeitos = 0;
                        servico.CriancaPensaoMorteAprovados = 0;
                        servico.CriancaPensaoMorteNegado = 0;
                    }
                    #endregion              
                }

                if (exercicioDesbloqueado == 2025)
                {

                    #region Crianças auxilio reclusao Exercicio 2025
                    if (!String.IsNullOrEmpty(rblCriancasAuxilioReclusaoExercicio2025.SelectedValue))
                    {
                        if (rblCriancasAuxilioReclusaoExercicio2025.SelectedValue == "1")
                        {
                            ValidaTextBoxAuxilioReclusao();
                            servico.AtendeCriancasAuxilioReclusaoExercicio2025 = true;

                            servico.CriancaAuxilioReclusaoFeitosExercicio2025 = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoFeitosExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoFeitosExercicio2025.Text);
                            servico.CriancaAuxilioReclusaoAprovadosExercicio2025 = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoAprovadosExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoAprovadosExercicio2025.Text);
                            servico.CriancaAuxilioReclusaoNegadoExercicio2025 = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoNegadoExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoNegadoExercicio2025.Text);

                        }
                        else
                        {
                            servico.AtendeCriancasAuxilioReclusaoExercicio2025 = false;

                            servico.CriancaAuxilioReclusaoFeitosExercicio2025 = 0;
                            servico.CriancaAuxilioReclusaoAprovadosExercicio2025 = 0;
                            servico.CriancaAuxilioReclusaoNegadoExercicio2025 = 0;

                        }
                    }
                    else
                    {
                        servico.AtendeCriancasAuxilioReclusaoExercicio2025 = null;

                        servico.CriancaAuxilioReclusaoFeitosExercicio2025 = 0;
                        servico.CriancaAuxilioReclusaoAprovadosExercicio2025 = 0;
                        servico.CriancaAuxilioReclusaoNegadoExercicio2025 = 0;
                    }
                    #endregion

                }

                if (exercicioDesbloqueado == 2025)
                {
                    #region Crianças Pensão Morte Exercicio 2025
                    if (!String.IsNullOrEmpty(rblCriancasPensaoMorteExercicio2025.SelectedValue))
                    {
                        if (rblCriancasPensaoMorteExercicio2025.SelectedValue == "1")
                        {
                            ValidaTextBoxPensaoMorte();
                            servico.AtendeCriancasPensaoMorteExercicio2025 = true;

                            servico.CriancaPensaoMorteFeitosExercicio2025 = String.IsNullOrEmpty(txtCriancasPensaoMorteFeitosExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteFeitosExercicio2025.Text);
                            servico.CriancaPensaoMorteAprovadosExercicio2025 = String.IsNullOrEmpty(txtCriancasPensaoMorteAprovadosExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteAprovadosExercicio2025.Text);
                            servico.CriancaPensaoMorteNegadoExercicio2025 = String.IsNullOrEmpty(txtCriancasPensaoMorteNegadoExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteNegadoExercicio2025.Text);
                        }
                        else
                        {
                            servico.AtendeCriancasPensaoMorteExercicio2025 = false;

                            servico.CriancaPensaoMorteFeitosExercicio2025 = 0;
                            servico.CriancaPensaoMorteAprovadosExercicio2025 = 0;
                            servico.CriancaPensaoMorteNegadoExercicio2025 = 0;
                        }
                    }
                    else
                    {
                        servico.AtendeCriancasPensaoMorteExercicio2025 = null;

                        servico.CriancaPensaoMorteFeitosExercicio2025 = 0;
                        servico.CriancaPensaoMorteAprovadosExercicio2025 = 0;
                        servico.CriancaPensaoMorteNegadoExercicio2025 = 0;
                    }
                    #endregion
                }


                #region Preencher: Funcionamento [Capacidade | Media Mensal]

                if (servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_PROTECAO_SOCIAL_ADOLESC_CUMPR_MEDIDA_SOCIOEDUCATIVA_LA_PSC)
                {

                    #region Carregar: Capacidade  LA
                    servico.ServicosRecursosFinanceiroPublicoCapacidadeLA = new List<ServicoRecursoFinanceiroPublicoCapacidadeLAInfo>();
                    ServicoRecursoFinanceiroPublicoCapacidadeLAInfo servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio1 = new ServicoRecursoFinanceiroPublicoCapacidadeLAInfo();
                    servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio1.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio1.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio1.Text) : 0;
                    servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[0];

                    ServicoRecursoFinanceiroPublicoCapacidadeLAInfo servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio2 = new ServicoRecursoFinanceiroPublicoCapacidadeLAInfo();
                    servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio2.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio2.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio2.Text) : 0;
                    servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[1];

                    ServicoRecursoFinanceiroPublicoCapacidadeLAInfo servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio3 = new ServicoRecursoFinanceiroPublicoCapacidadeLAInfo();
                    servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio3.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio3.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio3.Text) : 0;
                    servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[2];

                    ServicoRecursoFinanceiroPublicoCapacidadeLAInfo servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio4 = new ServicoRecursoFinanceiroPublicoCapacidadeLAInfo();
                    servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio4.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio4.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio4.Text) : 0;
                    servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[3];

                    servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Add(servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Add(servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Add(servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Add(servicoRecursoFinanceiroPublicoCapacidadeLAInfoExercicio4);
                    #endregion

                    #region Carregar: Capacidade PSC
                    servico.ServicosRecursosFinanceiroPublicoCapacidadePSC = new List<ServicoRecursoFinanceiroPublicoCapacidadePSCInfo>();
                    ServicoRecursoFinanceiroPublicoCapacidadePSCInfo servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio1 = new ServicoRecursoFinanceiroPublicoCapacidadePSCInfo();
                    servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio1.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio1.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio1.Text) : 0;
                    servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[0];

                    ServicoRecursoFinanceiroPublicoCapacidadePSCInfo servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio2 = new ServicoRecursoFinanceiroPublicoCapacidadePSCInfo();
                    servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio2.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio2.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio2.Text) : 0;
                    servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[1];

                    ServicoRecursoFinanceiroPublicoCapacidadePSCInfo servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio3 = new ServicoRecursoFinanceiroPublicoCapacidadePSCInfo();
                    servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio3.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio3.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio3.Text) : 0;
                    servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[2];

                    ServicoRecursoFinanceiroPublicoCapacidadePSCInfo servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio4 = new ServicoRecursoFinanceiroPublicoCapacidadePSCInfo();
                    servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio4.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio4.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio4.Text) : 0;
                    servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[3];

                    servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Add(servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Add(servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Add(servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Add(servicoRecursoFinanceiroPublicoCapacidadePSCInfoExercicio4);
                    #endregion

                    #region Carregar: MM LA
                    servico.ServicosRecursosFinanceiroPublicoMediaMensalLA = new List<ServicoRecursoFinanceiroPublicoMediaMensalLAInfo>();

                    #region Exercicio 1
                    ServicoRecursoFinanceiroPublicoMediaMensalLAInfo servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio1 = new ServicoRecursoFinanceiroPublicoMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio1.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio1.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio1.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[0]; 
                    #endregion

                    #region Exercicio 2
                    ServicoRecursoFinanceiroPublicoMediaMensalLAInfo servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio2 = new ServicoRecursoFinanceiroPublicoMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio2.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio2.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio2.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[1]; 
                    #endregion

                    #region Exercicio 3
                    ServicoRecursoFinanceiroPublicoMediaMensalLAInfo servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio3 = new ServicoRecursoFinanceiroPublicoMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio3.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio3.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio3.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[2]; 
                    #endregion

                    #region Exercicio 4
                    ServicoRecursoFinanceiroPublicoMediaMensalLAInfo servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio4 = new ServicoRecursoFinanceiroPublicoMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio4.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio4.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio4.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[3]; 
                    #endregion

                    servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Add(servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Add(servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Add(servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Add(servicoRecursoFinanceiroPublicoMediaMensalLAInfoExercicio4);
                    #endregion

                    #region Carregar: MM PSC
                    servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC = new List<ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo>();

                    #region Exercicio 1 
                    ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio1 = new ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio1.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio1.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio1.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[0];
                    
                    #endregion
                    ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio2 = new ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio2.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio2.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio2.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[1];

                    ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio3 = new ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio3.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio3.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio3.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[2];

                    ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio4 = new ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio4.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio4.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio4.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[3];

                    servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Add(servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Add(servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Add(servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Add(servicoRecursoFinanceiroPublicoMediaMensalPSCInfoExercicio4);
                    #endregion
                }
                else
                {

                    #region Carregar: Capacidade
                    servico.ServicosRecursosFinanceiroPublicoCapacidade = new List<ServicoRecursoFinanceiroPublicoCapacidadeInfo>();

                    #region Exercicio 1
                    ServicoRecursoFinanceiroPublicoCapacidadeInfo servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio1 = new ServicoRecursoFinanceiroPublicoCapacidadeInfo();
                    if (!String.IsNullOrEmpty(txtCapacidadeExercicio1.Text))
                    {
                        servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio1.Capacidade = Convert.ToInt32(txtCapacidadeExercicio1.Text);    
                    }
                    servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[0];
                    #endregion

                    #region Exercicio 2
                    ServicoRecursoFinanceiroPublicoCapacidadeInfo servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio2 = new ServicoRecursoFinanceiroPublicoCapacidadeInfo();
                    if (!String.IsNullOrEmpty(txtCapacidadeExercicio2.Text))
                    {
                        servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio2.Capacidade = Convert.ToInt32(txtCapacidadeExercicio2.Text);    
                    }
                    servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[1];
                    #endregion

                    #region Exercicio 3
                    ServicoRecursoFinanceiroPublicoCapacidadeInfo servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio3 = new ServicoRecursoFinanceiroPublicoCapacidadeInfo();
                    if (!String.IsNullOrEmpty(txtCapacidadeExercicio3.Text))
                    {
                        servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio3.Capacidade = Convert.ToInt32(txtCapacidadeExercicio3.Text);    
                    }
                    servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[2];
                    #endregion

                    #region Exercicio 4
                    ServicoRecursoFinanceiroPublicoCapacidadeInfo servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio4 = new ServicoRecursoFinanceiroPublicoCapacidadeInfo();
                    if (!String.IsNullOrEmpty(txtCapacidadeExercicio4.Text))
                    {
                        servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio4.Capacidade = Convert.ToInt32(txtCapacidadeExercicio4.Text);    
                    }
                    servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[3];
                    #endregion

                    servico.ServicosRecursosFinanceiroPublicoCapacidade.Add(servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPublicoCapacidade.Add(servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPublicoCapacidade.Add(servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPublicoCapacidade.Add(servicoRecursoFinanceiroPublicoCapacidadeInfoExercicio4);
                    #endregion

                    #region Carregar: MM
                    servico.ServicosRecursosFinanceiroPublicoMediaMensal = new List<ServicoRecursoFinanceiroPublicoMediaMensalInfo>();

                    #region Exercicio 1
                    ServicoRecursoFinanceiroPublicoMediaMensalInfo servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio1 = new ServicoRecursoFinanceiroPublicoMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio1.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio1.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio1.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[0]; 
                    #endregion

                    #region Exercicio 2
                    ServicoRecursoFinanceiroPublicoMediaMensalInfo servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio2 = new ServicoRecursoFinanceiroPublicoMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio2.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio2.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio2.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[1]; 
                    #endregion

                    #region Exercicio 3
                    ServicoRecursoFinanceiroPublicoMediaMensalInfo servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio3 = new ServicoRecursoFinanceiroPublicoMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio3.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio3.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio3.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[2]; 
                    #endregion

                    #region Exercicio 4
                    ServicoRecursoFinanceiroPublicoMediaMensalInfo servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio4 = new ServicoRecursoFinanceiroPublicoMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio4.Text))
                    {
                        servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio4.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio4.Text);
                    }
                    servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPublico.Exercicios[3]; 
                    #endregion

                    servico.ServicosRecursosFinanceiroPublicoMediaMensal.Add(servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPublicoMediaMensal.Add(servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPublicoMediaMensal.Add(servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPublicoMediaMensal.Add(servicoRecursoFinanceiroPublicoMediaMensalInfoExercicio4);
                    #endregion

                }



                #endregion

                #region ConsorcioPublico

                var consorcio = new ConsorcioPublicoInfo();

                consorcio.IdServicosRecursosFinanceirosPublico = idServico;
                consorcio.NomeConsorcio = txtNomeConsorcio.Text;
                consorcio.MunicipioSede = txtMunicipioSedeConsorcio.Text;
                consorcio.MunicipioEnvolvido = txtMunicipiosEnvolvidos.Text;
                consorcio.CNPJ = txtCNPJConsorcio.Text;

                #endregion

                #region  Preencher: [Hora semana]
                servico.IdHorasSemana = Convert.ToInt32(rblHorasSemana.SelectedValue);
                servico.QuantidadeDiasSemana = Convert.ToInt32(rblDiasSemana.SelectedValue);
                #endregion

                #region  Preencher: [Atividade Socio Assistenciais]

                servico.AtividadesSocioAssistenciais = new List<AtividadeSocioAssistencialInfo>();
                foreach (ListItem atividade in lstAtividades.Items)
                {
                    if (atividade.Selected)
                    {
                        servico.AtividadesSocioAssistenciais.Add(new AtividadeSocioAssistencialInfo() { Id = Convert.ToInt32(atividade.Value) });
                    }
                }

                #endregion

                #region Preencher: [Gestor]
                if (!String.IsNullOrEmpty(rblAvaliacaoGestor.SelectedValue))
                {
                    servico.IdAvaliacaoServico = Convert.ToInt32(rblAvaliacaoGestor.SelectedValue);
                }
                #endregion

                #region Preencher: [Data Funcionamento]
                DateTime dt;
                if (!String.IsNullOrEmpty(txtDataInicio.Text) && DateTime.TryParse(txtDataInicio.Text, out dt))
                {
                    servico.DataFuncionamentoServico = Convert.ToDateTime(txtDataInicio.Text);
                }
                #endregion

                #region Preencher: Recursos Financeiros

                #region Fonte Exercicio1
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPublico.Exercicios[0])
                {
                    ServicoRecursoFinanceiroFundosPublicoInfo fundo = new ServicoRecursoFinanceiroFundosPublicoInfo();
                    fundo.ServicoRecursoFinanceiroPublicoInfoId = idServico;
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
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio1.Text) ? string.Empty : txtObjetoDemandaExercicio1.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio1.Text) ? string.Empty : txtCodigoDemandaExercicio1.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio1.Text) ? Convert.ToDecimal(txtValorContraExercicio1.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;
                    ValidaBloqueioDesbloqueio();

                    servico.ServicosRecursosFinanceirosFundosPublicoInfo = servico.ServicosRecursosFinanceirosFundosPublicoInfo ?? new List<ServicoRecursoFinanceiroFundosPublicoInfo>();
                    servico.ServicosRecursosFinanceirosFundosPublicoInfo.Add(fundo);

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio1.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        fundo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo = SessaoFontesRecursosExercicio1;
                        if (fundo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo != null)
                        {
                            foreach (var fonteNova in fundo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo)
                            {
                                fonteNova.IdServicoRecursoFinanceiroFundosPublico = fundo.Id;
                            }
                        }
                    }
                    else
                    {

                    }

                } 
                #endregion

                #region Fonte Exercicio2
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPublico.Exercicios[1])
                {
                    ServicoRecursoFinanceiroFundosPublicoInfo fundo = new ServicoRecursoFinanceiroFundosPublicoInfo();
                    fundo.ServicoRecursoFinanceiroPublicoInfoId = idServico;
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
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio2.Text) ? string.Empty : txtObjetoDemandaExercicio2.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio2.Text) ? string.Empty : txtCodigoDemandaExercicio2.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio2.Text) ? Convert.ToDecimal(txtValorContraExercicio2.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;
              
                    servico.ServicosRecursosFinanceirosFundosPublicoInfo = servico.ServicosRecursosFinanceirosFundosPublicoInfo ?? new List<ServicoRecursoFinanceiroFundosPublicoInfo>();
                    servico.ServicosRecursosFinanceirosFundosPublicoInfo.Add(fundo);
                    ValidaBloqueioDesbloqueio();

                    var totalLinhasExercicio2 = lstRecursosAdicionadosExercicio2.Items.Count;

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio2.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        if (totalLinhasExercicio2 > 0)
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(true);
                        else
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(false);

                        fundo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo = this.SessaoFontesRecursosExercicio2;
                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosPublico = fundo.Id;
                        }
                    }
                } 
                #endregion

                #region Fonte Exercicio3
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPublico.Exercicios[2])
                {
                    ServicoRecursoFinanceiroFundosPublicoInfo fundo = new ServicoRecursoFinanceiroFundosPublicoInfo();
                    fundo.ServicoRecursoFinanceiroPublicoInfoId = idServico;
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
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio3.Text) ? string.Empty : txtObjetoDemandaExercicio3.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio3.Text) ? string.Empty : txtCodigoDemandaExercicio3.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio3.Text) ? Convert.ToDecimal(txtValorContraExercicio3.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida3.SelectedValue == "1" ? true : false;
                    ValidaBloqueioDesbloqueio();

                    var totalLinhasExercicio3 = lstRecursosAdicionadosExercicio3.Items.Count;

                    servico.ServicosRecursosFinanceirosFundosPublicoInfo = servico.ServicosRecursosFinanceirosFundosPublicoInfo ?? new List<ServicoRecursoFinanceiroFundosPublicoInfo>();
                    servico.ServicosRecursosFinanceirosFundosPublicoInfo.Add(fundo);

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio3.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        if (totalLinhasExercicio3 > 0)
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(true);
                        else
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(false);

                        fundo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo = this.SessaoFontesRecursosExercicio3;

                        if (fundo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo != null)
                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosPublico = fundo.Id;
                        }
                    }
                } 
                #endregion

                #region Fonte Exercicio4
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPublico.Exercicios[3])
                {
                    ServicoRecursoFinanceiroFundosPublicoInfo fundo = new ServicoRecursoFinanceiroFundosPublicoInfo();
                    fundo.ServicoRecursoFinanceiroPublicoInfoId = idServico;
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
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio4.Text) ? string.Empty : txtObjetoDemandaExercicio4.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio4.Text) ? string.Empty : txtCodigoDemandaExercicio4.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio4.Text) ? Convert.ToDecimal(txtValorContraExercicio4.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida4.SelectedValue == "1" ? true : false;

                    servico.ServicosRecursosFinanceirosFundosPublicoInfo = servico.ServicosRecursosFinanceirosFundosPublicoInfo ?? new List<ServicoRecursoFinanceiroFundosPublicoInfo>();
                    servico.ServicosRecursosFinanceirosFundosPublicoInfo.Add(fundo);
                    ValidaBloqueioDesbloqueio();

                    var totalLinhasExercicio4 = lstRecursosAdicionadosExercicio4.Items.Count;
                   
                    
                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio4.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        if (totalLinhasExercicio4 > 0)
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(true);
                        else
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(false);

                        fundo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo = this.SessaoFontesRecursosExercicio4;
                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosPublico = fundo.Id;
                        }
                    }
                } 
                #endregion

                #endregion


                #region Preencher: Demandas 

                var demandas = new ServicoRecursoFinanceiroFundosPublicoInfo();

                #region Exercicio1
                //if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPublico.Exercicios[0])
                //{
                //    ServicoRecursoFinanceiroFundosPublicoInfo demandas1 = new ServicoRecursoFinanceiroFundosPublicoInfo();
                //    
                //    demandas1.ObjetoDemandaParlamentar = !String.IsNullOrEmpty(txtObjetoDemandaExercicio1.Text) ? txtObjetoDemandaExercicio1.Text : "";
                //    demandas1.CodigoDemandaParlamentar = !String.IsNullOrEmpty(txtCodigoDemandaExercicio1.Text) ? txtCodigoDemandaExercicio1.Text : "";
                //    demandas1.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio1.Text) ? Convert.ToDecimal(txtValorContraExercicio1.Text) : 0M;
                //    demandas1.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;
                //    demandas = demandas1;
                //}
                #endregion

                #region Exercicio2
                //if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPublico.Exercicios[1])
                //{
                //    ServicoRecursoFinanceiroFundosPublicoInfo demandas2 = new ServicoRecursoFinanceiroFundosPublicoInfo();
                //    
                //    demandas2.ObjetoDemandaParlamentar = !String.IsNullOrEmpty(txtObjetoDemandaExercicio2.Text) ? txtObjetoDemandaExercicio2.Text : "";
                //    demandas2.CodigoDemandaParlamentar = !String.IsNullOrEmpty(txtCodigoDemandaExercicio2.Text) ? txtCodigoDemandaExercicio2.Text : "";
                //    demandas2.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio2.Text) ? Convert.ToDecimal(txtValorContraExercicio2.Text) : 0M;
                //    demandas2.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;
                //    demandas = demandas2;
                //
                //}
                #endregion
                #region Exercicio3
                //if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPublico.Exercicios[2])
                //{
                //    ServicoRecursoFinanceiroFundosPublicoInfo demandas3 = new ServicoRecursoFinanceiroFundosPublicoInfo();
                //
                //    demandas3.ObjetoDemandaParlamentar = !String.IsNullOrEmpty(txtObjetoDemandaExercicio3.Text) ? txtObjetoDemandaExercicio3.Text : "";
                //    demandas3.CodigoDemandaParlamentar = !String.IsNullOrEmpty(txtCodigoDemandaExercicio3.Text) ? txtCodigoDemandaExercicio3.Text : "";
                //    demandas3.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio3.Text) ? Convert.ToDecimal(txtValorContraExercicio3.Text) : 0M;
                //    demandas3.ContrapartidaMunicipal = rblContraPartida3.SelectedValue == "1" ? true : false;
                //    demandas = demandas3;
                //}
                #endregion
                #region Exercicio4
                //if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPublico.Exercicios[3])
                //{
                //    ServicoRecursoFinanceiroFundosPublicoInfo demandas4 = new ServicoRecursoFinanceiroFundosPublicoInfo();
                //    
                //    demandas4.ObjetoDemandaParlamentar = !String.IsNullOrEmpty(txtObjetoDemandaExercicio4.Text) ? txtObjetoDemandaExercicio4.Text : "";
                //    demandas4.CodigoDemandaParlamentar = !String.IsNullOrEmpty(txtCodigoDemandaExercicio4.Text) ? txtCodigoDemandaExercicio4.Text : "";
                //    demandas4.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio4.Text) ? Convert.ToDecimal(txtValorContraExercicio4.Text) : 0M;
                //    demandas4.ContrapartidaMunicipal = rblContraPartida4.SelectedValue == "1" ? true : false;
                //    demandas = demandas4;
                //}
                #endregion

                #endregion

                #region Preencher: RH
                var recursosHumanos = this.CarregarRH();
                #endregion

                
                #region Aplicar: [Validacao]
                new ValidadorServicoRecursoFinanceiro().ValidarServicoPublico(servico);
                new ValidadorRecursosHumanos().ValidarRecursosHumanosPublico(recursosHumanos);
                var validaRh = new ValidadorRecursosHumanos().ValidaPublico(recursosHumanos);
                if (validaRh.Count > 0)
                {
                    throw new Exception(Extensions.Concat(validaRh, System.Environment.NewLine));
                }
                #endregion

                #region Aplicar Ação [Incluir/Alterar]
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    if (action == "I")
                    {
                        AdicionarServico(servico, idLocalExecucao, recursosHumanos, proxy,consorcio);
                    }

                    if (action == "A")
                    {
                        proxy.Service.UpdateServicoRecursoFinanceiroPublico(servico);

                        var fundos = proxy.Service.GetServicoRecursoFinanceiroPublicoById(servico.Id);

                        int idFundos = fundos.ServicosRecursosFinanceirosFundosPublicoInfo.Where(s => s.Exercicio == Convert.ToInt32(hdnExercicio.Value)).First().Id;


                        if (!String.IsNullOrEmpty(txtNomeConsorcio.Text) || !String.IsNullOrEmpty(txtMunicipioSedeConsorcio.Text) || !String.IsNullOrEmpty(txtMunicipiosEnvolvidos.Text) || !String.IsNullOrEmpty(txtCNPJConsorcio.Text))
                        {
                            using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
                            {
                                proxySocial.Service.SalvarConsorcio(consorcio);
                            }                            
                        }


                        using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
                        {
                           proxySocial.Service.SalvarConsorcio(consorcio);
                        }

                      

                        #region Merge Recursos Humanos
                        if (recursosHumanos.Id == 0)
                        {
                            recursosHumanos.IdServicosRecursosFinanceirosPublico = servico.Id;
                            proxy.Service.AddServicoRecursoFinanceiroPublicoRH(recursosHumanos);
                        }
                        else
                        {
                            recursosHumanos.IdServicosRecursosFinanceirosPublico = servico.Id;
                            proxy.Service.UpdateServicoRecursoFinanceiroPublicoRH(recursosHumanos);
                        }
                        #endregion
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
                            var programas = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(idServicoBeneficio, idLocalExecucao);

                            var lst = programas.OrderBy(t => t.IdTipoProtecao)
                                .GroupBy(s => s.ProtecaoSocial)
                                .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) })
                                .ToList();

                            foreach (var tab in lst)
                            {
                                foreach (var item in tab.Items)
                                {
                                    ProxyProgramas.Service.DeleteProgramaProjetoCofinanciamento(Convert.ToInt32(item.Id), Convert.ToInt32(item.TipoCofinanciamento));
                                }

                            }
                            trRendaCidadaBeneficioIdoso.Visible = true;
                            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
                            lstRecursos.DataSource = lst;
                            lstRecursos.DataBind();
                        }
                    }
                }
                SessaoFontesRecursosExercicio1 = null;
                SessaoFontesRecursosExercicio2 = null;
                SessaoFontesRecursosExercicio3 = null;
                SessaoFontesRecursosExercicio4 = null;
                #endregion
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }
            var id = servico.Id;
            


            if (action == "I")
            {

                Response.Redirect("~/BlocoIII/FServicoRecursoFinanceiroPublico.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString())) + "&idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocalDecrypt)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidadeDecrypt)) + "&msg=" + action);
                //Response.Redirect("~/BlocoIII/FServicoRecursoFinanceiroPrivado.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString())) + "&idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)) + "&msg=" + action);
            }
            else if (action == "A")
            {

                Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPublico.aspx?idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocalDecrypt)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocalDecrypt)) + "&msg=" + action);
            }

        }

        protected void btnSalvarRecursoPrograma_Click(object sender, EventArgs e)
        {
            this.btnSalvar_Click(sender, e);
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            var idLocal = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
            SessaoFontesRecursosExercicio1 = null;
            SessaoFontesRecursosExercicio2 = null;
            SessaoFontesRecursosExercicio3 = null;
            SessaoFontesRecursosExercicio4 = null;
            Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPublico.aspx?idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }
        protected void btnExcluir_Click(object sender, ImageClickEventArgs e)
        {


            ImageButton btn = (ImageButton)sender;
            int idPrograma = Convert.ToInt32(btn.CommandArgument);
            SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo.RemoveAll(x => x.Id == idPrograma);
            if (SessaoTransferenciaRendaCofinanciamentoInfo1 != null)
            {
                var transferencia = SessaoTransferenciaRendaCofinanciamentoInfo1.Where(x => x.Id == idPrograma);
                if (transferencia != null)
                {
                    SessaoTransferenciaRendaCofinanciamentoInfo1.RemoveAll(x => x.Id == idPrograma);
                }
            }
            if (SessaoPrefeituraBeneficioEventualServicoInfo1 != null)
            {
                var prefeitura = SessaoPrefeituraBeneficioEventualServicoInfo1.Where(x => x.Id == idPrograma);
                if (prefeitura != null)
                {
                    SessaoPrefeituraBeneficioEventualServicoInfo1.RemoveAll(x => x.Id == idPrograma);
                }
            }
            if (SessaoProgramaProjetoCofinanciamentoInfo1 != null)
            {
                var programa = SessaoProgramaProjetoCofinanciamentoInfo1.Where(x => x.Id == idPrograma);
                if (programa != null)
                {
                    SessaoProgramaProjetoCofinanciamentoInfo1.RemoveAll(x => x.Id == idPrograma);
                }
            }
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo.ToList();
            rptProgramaTemp.DataBind();
        }
        #endregion

        #region Changed [Tipo Servico]
        protected void ddlTipoServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            #region Exercicio 1
            ClearFuncionamento();
            #endregion

            #region Exercicio 2
            txtCapacidadePSCExercicio2.Text = string.Empty;
            txtCapacidadeExercicio2.Text = string.Empty;
            txtMediaMensalExercicio2.Text = string.Empty;
            txtMediaMensalPSCExercicio2.Text = string.Empty;

            #endregion

            #region Exercicio 3
            txtCapacidadePSCExercicio3.Text = string.Empty;
            txtCapacidadeExercicio3.Text = string.Empty;
            txtMediaMensalExercicio3.Text = string.Empty;
            txtMediaMensalPSCExercicio3.Text = string.Empty;
            #endregion

            #region Exercicio 4
            txtCapacidadePSCExercicio4.Text = string.Empty;
            txtCapacidadeExercicio4.Text = string.Empty;
            txtMediaMensalExercicio4.Text = string.Empty;
            txtMediaMensalPSCExercicio4.Text = string.Empty;
            #endregion

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarUsuarios(proxy);
                CarregarAtividades(proxy);
                CarregarTiposServicosNaoTipificados(proxy);
            }

            this.AplicarTipoServicoChanged();
            this.ClearSituacoes();
        }

        private void ClearFuncionamento()
        {
            #region Clear: Capacidade
            txtCapacidadeExercicio1.Text = string.Empty;
            txtCapacidadeLAExercicio1.Text = string.Empty;
            txtCapacidadePSCExercicio1.Text = string.Empty;

            txtCapacidadeExercicio2.Text = string.Empty;
            txtCapacidadeLAExercicio2.Text = string.Empty;
            txtCapacidadePSCExercicio2.Text = string.Empty;

            txtCapacidadeExercicio3.Text = string.Empty;
            txtCapacidadeLAExercicio3.Text = string.Empty;
            txtCapacidadePSCExercicio3.Text = string.Empty;


            txtCapacidadeExercicio4.Text = string.Empty;
            txtCapacidadeLAExercicio4.Text = string.Empty;
            txtCapacidadePSCExercicio4.Text = string.Empty; 
            #endregion

            #region Clear: Media Mensal
            txtMediaMensalExercicio1.Text = string.Empty;
            txtMediaMensalLAExercicio1.Text = string.Empty;
            txtMediaMensalPSCExercicio1.Text = string.Empty;

            txtMediaMensalExercicio2.Text = string.Empty;
            txtMediaMensalLAExercicio2.Text = string.Empty;
            txtMediaMensalPSCExercicio2.Text = string.Empty;

            txtMediaMensalExercicio3.Text = string.Empty;
            txtMediaMensalLAExercicio3.Text = string.Empty;
            txtMediaMensalPSCExercicio3.Text = string.Empty;


            txtMediaMensalExercicio4.Text = string.Empty;
            txtMediaMensalLAExercicio4.Text = string.Empty;
            txtMediaMensalPSCExercicio4.Text = string.Empty;
            #endregion

        }
        private void AplicarTipoServicoChanged()
        {
            this.AplicarRegraExibicaoLayoutServicosChanged();
            this.AplicarRegraExibicaoDeLabels();
        }
        private void AplicarRegraExibicaoDeLabels()
        {

            if (ddlTipoServico.SelectedValue == "138" || ddlTipoServico.SelectedValue == "153" || ddlTipoServico.SelectedValue == "145")
            {
                lblIndicadorPeriodoCapacidade.Text = " famílias";
                lblIndicadorPeriodoMediaMensal.Text = " famílias";
            }
            else {
                lblIndicadorPeriodoCapacidadeLAPSC.Text = " pessoas";
                lblIndicadorPeriodoMediaMensalLAPSC.Text = " pessoas";
            }
        }
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

            #region Servico [NÃO TIPIFICADO]
            tbNaoTipificado.Visible = tbNaoTipificadoObjetivo.Visible = ddlTipoServico.SelectedValue == "138" || ddlTipoServico.SelectedValue == "145";
            tbNaoTipificadoDetalhado.Visible = tbNaoTipificadoObjetivo.Visible = ddlTipoServico.SelectedValue == "153";
            #endregion

        } 
        #endregion

        #region Changed [Tipo Servico [Ñ Tipificado]]
        protected void ddlTipoServicoNaoTipificado_SelectedIndexChanged(object sender, EventArgs e) //PMAS 2016
        {
            SessaoPmas.VerificarSessao(this);

            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            if (ddlTipoServicoNaoTipificado.SelectedItem.Text == "Outro")
            {
                tbNaoTipificadoDetalhado.Visible = tbNaoTipificadoObjetivo.Visible = true;
                lblIndicadorPeriodoCapacidade.Text = " pessoas";
                lblIndicadorPeriodoMediaMensal.Text = " pessoas";
            }
            else
            {
                tbNaoTipificadoDetalhado.Visible = tbNaoTipificadoObjetivo.Visible = false;
                txtNaotipificado.Text = "";
                txtObjetivoNaoTipificado.Text = "";
            }

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarUsuarios(proxy, true);
                CarregarAtividades(proxy, true);
            }

            //LIMPAR SITUAÇÕES
            situacoesEspecificas.DataTextField = "Nome";
            situacoesEspecificas.DataValueField = "Id";
            situacoesEspecificas.DataSource = new List<SituacaoEspecificaInfo>();
            situacoesEspecificas.DataBind();
        } 
        #endregion

        #region DDL [Publico Alvo]
        protected void ddlPublicoAlvo_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            SessaoPmas.VerificarSessao(this);

            if (ddlTipoServico.SelectedValue == "146" && ddlPublicoAlvo.SelectedValue == "40"
                || ddlTipoServico.SelectedValue == "148" && ddlPublicoAlvo.SelectedValue == "43"
                || ddlTipoServico.SelectedValue == "150" && ddlPublicoAlvo.SelectedValue == "47"
                || ddlTipoServico.SelectedValue == "150" && ddlPublicoAlvo.SelectedValue == "48")
            {
                tdAtendimentoDependente.Visible = true;
            }
            else
            {
                tdAtendimentoDependente.Visible = false;
                tdProgramaRecomeco.Visible = false;
            }

            if (ddlTipoServico.SelectedValue == "146" && ddlPublicoAlvo.SelectedValue == "37") 
            {
                trAuxilioReclusaoPensaoMorte.Visible = true;
            }
            else
            {
                trAuxilioReclusaoPensaoMorte.Visible = false;
            }

            if (ddlTipoServicoNaoTipificado.SelectedValue == "155" && ddlPublicoAlvo.SelectedValue == "68" ||
                ddlTipoServicoNaoTipificado.SelectedValue == "157" && ddlPublicoAlvo.SelectedValue == "70" ||
                ddlTipoServicoNaoTipificado.SelectedValue == "158" && ddlPublicoAlvo.SelectedValue == "71" ||
                ddlTipoServicoNaoTipificado.SelectedValue == "154" && ddlPublicoAlvo.SelectedValue == "67" ||
                ddlTipoServicoNaoTipificado.SelectedValue == "153" && ddlPublicoAlvo.SelectedValue == "54" ||
                ddlTipoServicoNaoTipificado.SelectedValue == "138" && ddlPublicoAlvo.SelectedValue == "79" ||
                ddlTipoServicoNaoTipificado.SelectedValue == "159" && ddlPublicoAlvo.SelectedValue == "87" ||
                ddlTipoServico.SelectedValue == "152" && ddlPublicoAlvo.SelectedValue == "53")
            {
                lblIndicadorPeriodoCapacidade.Text = " famílias";
                lblIndicadorPeriodoMediaMensal.Text = " famílias";
            }
            else
            {
                if (ddlPublicoAlvo.SelectedValue != "0")
                {
                    lblIndicadorPeriodoCapacidadeLAPSC.Text = " pessoas";
                    lblIndicadorPeriodoMediaMensalLAPSC.Text = " pessoas";
                }
            }

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarSituacoes(proxy);
            }
        }
        #endregion

        #region DDL | RBL | DATABOUND [Caracterização do Serviço]
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
        protected void rblTipoProtecao_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            SessaoPmas.VerificarSessao(this);

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarTiposServicos(proxy);
                CarregarTiposServicosNaoTipificados(proxy);
                tbNaoTipificado.Visible = false;
                tbNaoTipificadoDetalhado.Visible = false;
                tbNaoTipificadoObjetivo.Visible = false;
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

            using (var proxyprefeitura = new ProxyPrefeitura())
            {
                var pre = proxyprefeitura.Service.GetPrefeituraById(Convert.ToInt32(SessaoPmas.UsuarioLogado.Prefeitura.Id));

                trFeasAnterior.Visible = true;

            }
        }
        protected void rblIntegracaoRede_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            trProgramasBeneficios.Visible = false;
            if (rblIntegracaoRede.SelectedValue == "1")
            {
                trProgramasBeneficios.Visible = true;
                using (var proxy = new ProxyProgramas())
                {
                    CarregarProgramas(proxy);

                }
                //this.Master.ScriptManagerControl.SetFocus(frame1_2);
            }
        }
        protected void rblAtendeDependentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAtendeDependentes.SelectedValue == "1")
                tdProgramaRecomeco.Visible = true;
            else
                tdProgramaRecomeco.Visible = false;
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
            var idLocal = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));
            if (e.CommandName == "Excluir")
            {
                using (var proxy = new ProxyProgramas())
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string id = commandArgs[0];
                    string tipoCofinanciamento = commandArgs[1];
                    proxy.Service.DeleteProgramaProjetoCofinanciamento(Convert.ToInt32(id), Convert.ToInt32(tipoCofinanciamento));
                    CarregarProgramas(proxy, idServicosRecursosFinanceiros, idLocal);
                }
            }
        }
        #endregion

        #region Demais Actions: BTN | RBL | DATABOUND [Recursos | Exercicio 1]
        protected void btnAdicionarRecursoExercicio1_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroPublicoFonteRecursoInfo();
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

            SessaoFontesRecursosExercicio1 = SessaoFontesRecursosExercicio1 ?? new List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>();
            SessaoFontesRecursosExercicio1.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio1();

            txtNomeRecursoExercicio1.Text = String.Empty;
            txtValorRecursoExercicio1.Text = "0";
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
                //SessaoFontesRecursosExercicio1 = null;
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

        #region Demais Actions: BTN | RBL | DATABOUND [Recursos | Exercicio 2]
        protected void btnAdicionarRecursoExercicio2_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroPublicoFonteRecursoInfo();
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

            SessaoFontesRecursosExercicio2 = SessaoFontesRecursosExercicio2 ?? new List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>();
            SessaoFontesRecursosExercicio2.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio2();

            txtNomeRecursoExercicio2.Text = String.Empty;
            txtValorRecursoExercicio2.Text = "0";
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

        #region Demais Actions: BTN | RBL | DATABOUND [Recursos | Exercicio 3]
        protected void btnAdicionarRecursoExercicio3_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroPublicoFonteRecursoInfo();
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


            SessaoFontesRecursosExercicio3 = SessaoFontesRecursosExercicio3 ?? new List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>();
            
                       
            SessaoFontesRecursosExercicio3.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio3();

            txtNomeRecursoExercicio3.Text = String.Empty;
            txtValorRecursoExercicio3.Text = "0";
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio3.Visible = true;
            ValidaBloqueioDesbloqueio();

        }
        protected void rblOutrasFontesExercicio3_SelectedIndexChanged(object sender, EventArgs e)
        {

            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();
            ValidaBloqueioDesbloqueio();


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

        #region Demais Actions: BTN | RBL | DATABOUND [Recursos | Exercicio 4]
        protected void btnAdicionarRecursoExercicio4_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroPublicoFonteRecursoInfo();
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

            SessaoFontesRecursosExercicio4 = SessaoFontesRecursosExercicio4 ?? new List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>();
            SessaoFontesRecursosExercicio4.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio4();

            txtNomeRecursoExercicio4.Text = String.Empty;
            txtValorRecursoExercicio4.Text = "0";
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
                    {
                        SessaoFontesRecursosExercicio4.RemoveAt(e.Item.DataItemIndex);
                        CarregarRecursosFinanceirosFonteRecursosExercicio4();
                        var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion



        #endregion // END Eventos

        #region Chkbox [Tecnico Responsavel]
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
        #endregion

        #region Eventos [helper]
        private void AdicionarServico(ServicoRecursoFinanceiroPublicoInfo servico, int idLocalExecucao, ServicoRecursoFinanceiroPublicoRecursosHumanosInfo recursosHumanos, ProxyRedeProtecaoSocial proxy, ConsorcioPublicoInfo consorcio)
        {
            proxy.Service.AddServicoRecursoFinanceiroPublico(servico);

            recursosHumanos.IdServicosRecursosFinanceirosPublico = servico.Id;

            consorcio.IdServicosRecursosFinanceirosPublico = servico.Id;

            using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
            {
                proxySocial.Service.SalvarConsorcio(consorcio);
            }

            if (recursosHumanos.Id == 0)
            {
                proxy.Service.AddServicoRecursoFinanceiroPublicoRH(recursosHumanos);
            }

            if (SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo != null)
            {
                foreach (var item in SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo)
                {
                    if (item.Nome.ToLower().Contains("beneficio de prestação continuada - bpc idosos")
                          || item.Nome.ToLower().Contains("beneficio de prestação continuada - bpc pessoas com deficiência")
                          || item.Nome.Contains("Bolsa Família")
                          || item.Nome.ToLower().Contains("peti")
                          || item.Nome.Contains("Prospera Família")
                          || item.Nome.ToLower().Contains("ação jovem")
                          || item.Nome.ToLower().Contains("renda cidadã")
                          || item.Nome.ToLower().Contains("teste transferencia renda")
                          || item.Nome.Contains("Fortalecimento do CadÚnico")
                          //|| item.Nome.Contains("Fortalecimento da Vigilância Socioassistencial")
                          )
                    {
                        AdicionaTransferenciaRenda(idLocalExecucao, servico.Id, item.IdProgramaProjeto);

                    }
                    else if (item.Nome.ToLower().Contains("auxílio natalidade")
                       || item.Nome.ToLower().Contains("auxílio funeral")
                       || item.Nome.ToLower().Contains("calamidades públicas e emergências")
                       || item.Nome.ToLower().Contains("vulnerabilidade temporária")
                   )
                    {
                        AdicionaPrefeituraBeneficio(idLocalExecucao, servico.Id, item.IdProgramaProjeto);
                    }
                    else
                    {
                        AdicionarProgramaConfinamento(idLocalExecucao, servico.Id, item.IdProgramaProjeto);
                    }

                }

            }

            SessaoPrefeituraBeneficioEventualServicoInfo1 = null;
            SessaoProgramaProjetoCofinanciamentoInfo1 = null;
            SessaoTransferenciaRendaCofinanciamentoInfo1 = null;
            SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo = null;
            SessaoPrefeituraBeneficioEventualServicoInfo1 = null;
            rptProgramaTemp.DataSource = null;
            rptProgramaTemp.DataBind();
        }

        private void AdicionarProgramaConfinamento(int idCentro, int IdServicosRecursosFinanceirosPublico, int idPrograma)
        {
            var item1 = SessaoProgramaProjetoCofinanciamentoInfo1.Where(x => x.IdProgramaProjeto == idPrograma).SingleOrDefault();
            var obj = new ProgramaProjetoCofinanciamentoInfo();
            obj.IdProgramaProjeto = Convert.ToInt32(item1.IdProgramaProjeto);
            obj.IdServicosRecursosFinanceirosPublico = IdServicosRecursosFinanceirosPublico;
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
                        CarregarProgramas(proxy, obj.IdServicosRecursosFinanceirosPublico.Value, idCentro);
                    }
                }
                var idServicoBeneficio = IdServicosRecursosFinanceirosPublico;

                using (var ProxyProgramas = new ProxyProgramas())
                {
                    //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
                    var programas = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(obj.IdServicosRecursosFinanceirosPublico.Value, idCentro);
                    var programasProjetosConfinamento = programas.Where(s1 => s1.Exercicio == 2022);
                    if (programasProjetosConfinamento.Count() == 0)
                    {
                        programasProjetosConfinamento = programas.Where(s1 => s1.Exercicio == 2023);
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

                /*if (!programaExistente)
                {
                    var obj2 = new ServicoRecursoFinanceiroTransferenciaRendaInfo();
                    var sr = new ServicoRecursoFinanceiroPublicoInfo();

                    obj2.IdTransferenciaRenda = Convert.ToInt32(obj.IdProgramaProjeto);
                    obj2.IdServicosRecursosFinanceirosPublico = !String.IsNullOrEmpty(Request.QueryString["id"]) ? Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])) : 0;
                    sr.IdLocalExecucao = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));
                    obj2.NumeroUsuarios = Convert.ToInt32(item1.NumeroUsuarios);
                    if (trRendaCidadaBeneficioIdoso.Visible && obj2.NumeroUsuarios <= 0)
                    {
                        throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                    }
                    using (var proxy2 = new ProxyProgramas())
                    {
                        proxy2.Service.AddTransferenciaRendaCofinanciamento(obj2);
                        CarregarProgramas(proxy2, obj2.IdServicosRecursosFinanceirosPublico.Value, idCentro);
                    }
                }*/
            }




        }
        private void AdicionaListaProgramaProjetoCofinanciamentoInfo()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
            var obj = new ProgramaProjetoCofinanciamentoInfo();
            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo = SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();

            SessaoProgramaProjetoCofinanciamentoInfo1 = SessaoProgramaProjetoCofinanciamentoInfo1 ?? new List<ProgramaProjetoCofinanciamentoInfo>();

            consultaProgramaProjetoServicoCofinanciamentoInfo.Unidade = "ii";
            consultaProgramaProjetoServicoCofinanciamentoInfo.TipoServico = ddlTipoServico.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.NumeroAtendidos = Convert.ToInt32(txtNumeroUsuarios.Text);
            consultaProgramaProjetoServicoCofinanciamentoInfo.Usuario = ddlPublicoAlvo.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Nome = ddlProgramaBeneficio.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Id = rptProgramaTemp.Items.Count + 1;
            consultaProgramaProjetoServicoCofinanciamentoInfo.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo.Add(consultaProgramaProjetoServicoCofinanciamentoInfo);
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo.ToList();
            rptProgramaTemp.DataBind();
            obj.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            obj.Id = consultaProgramaProjetoServicoCofinanciamentoInfo.Id;
            obj.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            this.SessaoProgramaProjetoCofinanciamentoInfo1.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
            //btnSalvarRecursoPrograma.Visible = lstRecursos.Items.Count > 0;
        }
        private void AdicionaListaPrefeituraBeneficio()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo = SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();
            SessaoPrefeituraBeneficioEventualServicoInfo1 = SessaoPrefeituraBeneficioEventualServicoInfo1 ?? new List<PrefeituraBeneficioEventualServicoInfo>();


            consultaProgramaProjetoServicoCofinanciamentoInfo.Unidade = "ii";
            consultaProgramaProjetoServicoCofinanciamentoInfo.TipoServico = ddlTipoServico.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.NumeroAtendidos = Convert.ToInt32(txtNumeroUsuarios.Text);
            consultaProgramaProjetoServicoCofinanciamentoInfo.Usuario = ddlPublicoAlvo.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Nome = ddlProgramaBeneficio.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Id = rptProgramaTemp.Items.Count + 1;
            consultaProgramaProjetoServicoCofinanciamentoInfo.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);

            SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo.Add(consultaProgramaProjetoServicoCofinanciamentoInfo);
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo.ToList();
            rptProgramaTemp.DataBind();

            var obj = new PrefeituraBeneficioEventualServicoInfo();
            obj.IdPrefeituraBeneficioEventual = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            obj.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            obj.Id = consultaProgramaProjetoServicoCofinanciamentoInfo.Id;
            this.SessaoPrefeituraBeneficioEventualServicoInfo1.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";

        }
        private void AdicionaPrefeituraBeneficio(int idCentro, int IdServicosRecursosFinanceirosPublico, int idPrograma)
        {
            var item2 = SessaoPrefeituraBeneficioEventualServicoInfo1.Where(x => x.IdPrefeituraBeneficioEventual == idPrograma).SingleOrDefault();


            var entidadeBeneficio = new PrefeituraBeneficioEventualServicoInfo();
            if (!String.IsNullOrEmpty(txtNumeroUsuarios.Text))
            {
                entidadeBeneficio.NumeroBeneficiarios = Convert.ToInt32(item2.NumeroBeneficiarios);
            }

            entidadeBeneficio.IdPrefeituraBeneficioEventual = item2.IdPrefeituraBeneficioEventual;
            entidadeBeneficio.IdServicosRecursosFinanceirosPublico = IdServicosRecursosFinanceirosPublico;


            new ValidadorServicoBeneficioEventual().Validar(entidadeBeneficio);

            using (var proxy = new ProxyProgramas())
            {
                proxy.Service.AddBeneficioEventualServico(entidadeBeneficio);
                CarregarProgramas(proxy, entidadeBeneficio.IdServicosRecursosFinanceirosPublico.Value, idCentro);
            }


        }
        private void AdicionaTransferenciaRenda(int idCentro, int IdServicosRecursosFinanceirosPublico, int idPrograma)
        {
            var item2 = SessaoTransferenciaRendaCofinanciamentoInfo1.Where(x => x.IdTransferenciaRenda == idPrograma).SingleOrDefault();

            var entidadeTransferenciaRenda = new ServicoRecursoFinanceiroTransferenciaRendaInfo();
            entidadeTransferenciaRenda.IdTransferenciaRenda = item2.IdTransferenciaRenda;

            entidadeTransferenciaRenda.IdServicosRecursosFinanceirosPublico = IdServicosRecursosFinanceirosPublico;

            entidadeTransferenciaRenda.NumeroUsuarios = Convert.ToInt32(item2.NumeroUsuarios);
            if (trRendaCidadaBeneficioIdoso.Visible && entidadeTransferenciaRenda.NumeroUsuarios <= 0)
            {
                throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
            }
            // var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
            using (var proxy = new ProxyProgramas())
            {
                proxy.Service.AddTransferenciaRendaCofinanciamento(entidadeTransferenciaRenda);
                CarregarProgramas(proxy, entidadeTransferenciaRenda.IdServicosRecursosFinanceirosPublico.Value, idCentro);


            }

        }
        private void AdicionaListaTransferenciaRenda()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano

            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo = SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();
            SessaoTransferenciaRendaCofinanciamentoInfo1 = SessaoTransferenciaRendaCofinanciamentoInfo1 ?? new List<ServicoRecursoFinanceiroTransferenciaRendaInfo>();


            consultaProgramaProjetoServicoCofinanciamentoInfo.Unidade = "ii";
            consultaProgramaProjetoServicoCofinanciamentoInfo.TipoServico = ddlTipoServico.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.NumeroAtendidos = Convert.ToInt32(txtNumeroUsuarios.Text);
            consultaProgramaProjetoServicoCofinanciamentoInfo.Usuario = ddlPublicoAlvo.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Nome = ddlProgramaBeneficio.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Id = rptProgramaTemp.Items.Count + 1;
            consultaProgramaProjetoServicoCofinanciamentoInfo.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);

            SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo.Add(consultaProgramaProjetoServicoCofinanciamentoInfo);
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo.ToList();
            rptProgramaTemp.DataBind();

            var obj = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

            obj.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            obj.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            obj.Id = consultaProgramaProjetoServicoCofinanciamentoInfo.Id;
            this.SessaoTransferenciaRendaCofinanciamentoInfo1.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
            //btnSalvarRecursoPrograma.Visible = lstRecursos.Items.Count > 0;
        }
        private void AtualizaProgramaConfinamento(int idCentro)
        {
            var obj = new ProgramaProjetoCofinanciamentoInfo();
            obj.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);

            if (Request.QueryString["id"] != null)
            {
                obj.IdServicosRecursosFinanceirosPublico = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));


                if (!String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
                {
                    obj.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }

                if (trRendaCidadaBeneficioIdoso.Visible && !obj.NumeroUsuarios.HasValue)
                {
                    throw new Exception(String.Concat("Informe o numero de beneficiários deste serviço.", System.Environment.NewLine));
                }
                if (trRendaCidadaBeneficioIdoso.Visible && obj.NumeroUsuarios.Value <= 0)
                {
                    throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                }


                using (var proxy = new ProxyProgramas())
                {
                    bool programaExistente = false;
                    var programaprojeto = proxy.Service.GetProgramaProjetoById(Convert.ToInt32(ddlProgramaBeneficio.SelectedValue));

                    if (programaprojeto != null)
                    {
                        if (programaprojeto.IdPrefeitura == SessaoPmas.UsuarioLogado.Prefeitura.Id)
                        {
                            proxy.Service.AddProgramaProjetoCofinanciamento(obj);
                            CarregarProgramas(proxy, obj.IdServicosRecursosFinanceirosPublico.Value, idCentro);
                        }
                    }
                    var idServicoBeneficio = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                    using (var ProxyProgramas = new ProxyProgramas())
                    {
                        //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
                        var programas = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(obj.IdServicosRecursosFinanceirosPublico.Value, idCentro);
                        var programasProjetosConfinamento = programas.Where(s1 => s1.Exercicio == 2022);
                        if (programasProjetosConfinamento.Count() == 0)
                        {
                            programasProjetosConfinamento = programas.Where(s1 => s1.Exercicio == 2023);
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

                    /*if (!programaExistente)
                    {
                        var obj2 = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

                        obj2.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
                        obj2.IdServicosRecursosFinanceirosPublico = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                        if (!String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
                        {
                            obj2.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                        }
                        else
                        {
                            throw new Exception(String.Concat("Informe o numero de beneficiários deste serviço.", System.Environment.NewLine));
                        }
                        if (trRendaCidadaBeneficioIdoso.Visible && obj2.NumeroUsuarios <= 0)
                        {
                            throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                        }

                        using (var proxy2 = new ProxyProgramas())
                        {
                            proxy2.Service.AddTransferenciaRendaCofinanciamento(obj2);
                            CarregarProgramas(proxy2, obj2.IdServicosRecursosFinanceirosPublico.Value, idCentro);
                        }
                    }*/
                }
            }
            else
            {
                AdicionaListaProgramaProjetoCofinanciamentoInfo();

            }


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
                entidadeBeneficio.IdServicosRecursosFinanceirosPublico = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                if (!String.IsNullOrEmpty(txtNumeroUsuarios.Text))
                {
                    entidadeBeneficio.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }

                new ValidadorServicoBeneficioEventual().Validar(entidadeBeneficio);

                using (var proxy = new ProxyProgramas())
                {
                    proxy.Service.AddBeneficioEventualServico(entidadeBeneficio);
                    CarregarProgramas(proxy, entidadeBeneficio.IdServicosRecursosFinanceirosPublico.Value, idCentro);
                }
            }
            else
            {

                AdicionaListaPrefeituraBeneficio();


            }
        }
        private void AtualizaTransferenciaRenda(int idCentro)
        {

            if (Request.QueryString["id"] != null)
            {

                var entidadeTransferenciaRenda = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

                entidadeTransferenciaRenda.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
                entidadeTransferenciaRenda.IdServicosRecursosFinanceirosPublico = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

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
                    CarregarProgramas(proxy, entidadeTransferenciaRenda.IdServicosRecursosFinanceirosPublico.Value, idCentro);
                }
            }
            else
            {

                AdicionaListaTransferenciaRenda();


            }
        }
        #endregion //end Eventos [helper]


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

                                         #region Funcionamento
		                                 txtCapacidadeExercicio1,
                                         txtCapacidadeLAExercicio1,
                                         txtCapacidadePSCExercicio1, 
	                                     
                                         txtMediaMensalExercicio1,
                                         txtMediaMensalLAExercicio1,
                                         txtMediaMensalPSCExercicio1
                                         #endregion



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
                                         //txtFEASAnoAnteriorExercicio2,
                                         txtFEDCAExercicio2, 
                                         txtFEIExercicio2,
                                         txtFNASExercicio2,
                                         txtFNDCAExercicio2,
                                         txtFNIExercicio2,
                                         rblOutrasFontesExercicio2,
                                         txtNomeRecursoExercicio2,
                                         txtValorRecursoExercicio2,
                                         btnAdicionarRecursoExercicio2,
                                         #region Funcionamento
		                                 txtCapacidadeExercicio2,
                                         txtCapacidadeLAExercicio2,
                                         txtCapacidadePSCExercicio2, 
	                                     
                                         txtMediaMensalExercicio2,
                                         txtMediaMensalLAExercicio2,
                                         txtMediaMensalPSCExercicio2
                                         #endregion
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
                                         //txtFEASAnoAnteriorExercicio3,
                                         txtFEDCAExercicio3, 
                                         txtFEIExercicio3,
                                         txtFNASExercicio3,
                                         txtFNDCAExercicio3,
                                         txtFNIExercicio3,
                                         rblOutrasFontesExercicio3,
                                         txtNomeRecursoExercicio3,
                                         txtValorRecursoExercicio3,
                                         btnAdicionarRecursoExercicio3,
                                         //btnSalvarExercicio3,
                                         
                                         rblCriancasAuxilioReclusao,
                                         txtCriancaAuxilioReclusaoFeitos,
                                         txtCriancaAuxilioReclusaoAprovados,
                                         txtCriancaAuxilioReclusaoNegado,
                                         
                                         rblCriancasPensaoMorte,
                                         txtCriancasPensaoMorteFeitos,
                                         txtCriancasPensaoMorteAprovados,
                                         txtCriancasPensaoMorteNegado,
                                         #region Funcionamento
		                                 txtCapacidadeExercicio3,
                                         txtCapacidadeLAExercicio3,
                                         txtCapacidadePSCExercicio3, 
	                                     
                                         txtMediaMensalExercicio3,
                                         txtMediaMensalLAExercicio3,
                                         txtMediaMensalPSCExercicio3
                                         #endregion

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
                                         //txtFEASAnoAnteriorExercicio4,
                                         txtFEDCAExercicio4, 
                                         txtFEIExercicio4,
                                         txtFNASExercicio4,
                                         txtFNDCAExercicio4,
                                         txtFNIExercicio4,
                                         rblOutrasFontesExercicio4,
                                         txtNomeRecursoExercicio4,
                                         txtValorRecursoExercicio4,
                                         btnAdicionarRecursoExercicio4,
                                         //btnSalvarExercicio4,

			                             rblCriancasAuxilioReclusaoExercicio2025,
                                         txtCriancaAuxilioReclusaoFeitosExercicio2025,
                                         txtCriancaAuxilioReclusaoAprovadosExercicio2025,
                                         txtCriancaAuxilioReclusaoNegadoExercicio2025,
                                         
                                         rblCriancasPensaoMorteExercicio2025,
                                         txtCriancasPensaoMorteFeitosExercicio2025,
                                         txtCriancasPensaoMorteAprovadosExercicio2025,
                                         txtCriancasPensaoMorteNegadoExercicio2025,

                                         #region Funcionamento
		                                 txtCapacidadeExercicio4,
                                         txtCapacidadeLAExercicio4,
                                         txtCapacidadePSCExercicio4, 
	                                     
                                         txtMediaMensalExercicio4,
                                         txtMediaMensalLAExercicio4,
                                         txtMediaMensalPSCExercicio4
                                         #endregion
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
            #region Seleciona: Campos Recursos Demandas
            WebControl[] controlesDemandasExercicio1 = SelecionarControlesDemandasExercicio1();
            WebControl[] controlesDemandasExercicio2 = SelecionarControlesDemandasExercicio2();
            WebControl[] controlesDemandasExercicio3 = SelecionarControlesDemandasExercicio3();
            WebControl[] controlesDemandasExercicio4 = SelecionarControlesDemandasExercicio4();
            #endregion

            #region Regra: Bloqueio: Campos Recursos Financeiros
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroPublico.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroPublico.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroPublico.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroPublico.Exercicios[3]);
            #endregion
            
            #region Regra: Bloqueio: Campos Reprogramacao
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio1, FServicoRecursoFinanceiroPublico.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio2, FServicoRecursoFinanceiroPublico.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio3, FServicoRecursoFinanceiroPublico.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio4, FServicoRecursoFinanceiroPublico.Exercicios[3]);
            #endregion

            #region Regra: Bloqueio: Campos Demandas
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio1,FServicoRecursoFinanceiroPublico.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio2, FServicoRecursoFinanceiroPublico.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio3, FServicoRecursoFinanceiroPublico.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio4, FServicoRecursoFinanceiroPublico.Exercicios[3]);
            #endregion


            #region Regra: Bloqueio: Botao Salvar
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio1, FServicoRecursoFinanceiroPublico.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio2, FServicoRecursoFinanceiroPublico.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio3, FServicoRecursoFinanceiroPublico.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio4, FServicoRecursoFinanceiroPublico.Exercicios[3]);
            #endregion

        }

        #endregion

        #region webmethod
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
        #endregion

        #region helpers
        private void ClearSituacoes()
        {
            situacoesEspecificas.DataTextField = "Nome";
            situacoesEspecificas.DataValueField = "Id";
            situacoesEspecificas.DataSource = new List<SituacaoEspecificaInfo>();
            situacoesEspecificas.DataBind();

        }

        private void CarregarAbaInicialRecursosFinanceiros()
        {
            hdnExercicio.Value = (hdnExercicio.Value == string.Empty) ? FServicoRecursoFinanceiroPublico.Exercicios[0].ToString() : hdnExercicio.Value;
            hdnAuxilioReclusaoExercicio.Value = (hdnAuxilioReclusaoExercicio.Value == String.Empty) ? FServicoRecursoFinanceiroPublico.Exercicios[0].ToString() : hdnAuxilioReclusaoExercicio.Value;
            frame1_5.Attributes.Add("class", "active");

            if (hdnExercicio.Value == FServicoRecursoFinanceiroPublico.Exercicios[0].ToString())
            {
                frame1_5_Ano1.Attributes.Add("class", "active");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Remove("class");
            }
            else if (hdnExercicio.Value == FServicoRecursoFinanceiroPublico.Exercicios[1].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Add("class", "active");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Remove("class");
            }

            else if (hdnExercicio.Value == FServicoRecursoFinanceiroPublico.Exercicios[2].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Add("class", "active"); 
                frame1_5_Ano4.Attributes.Remove("class");
            }
            else if (hdnExercicio.Value == FServicoRecursoFinanceiroPublico.Exercicios[3].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Add("class", "active"); 
            }
        }
        protected string MontarBotao(ConsultaProgramaProjetoServicoCofinanciamentoInfo item)
        {
            var idProjeto = item.Id;
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
        private void CamposBindEventos()
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

            txtFMASExercicio1.Attributes.Add("onclick", "this.select()");
            txtFNASExercicio1.Attributes.Add("onclick", "this.select()");
            txtFEASExercicio1.Attributes.Add("onclick", "this.select()");
            txtFMDCAExercicio1.Attributes.Add("onclick", "this.select()");
            txtFEDCAExercicio1.Attributes.Add("onclick", "this.select()");
            txtFNDCAExercicio1.Attributes.Add("onclick", "this.select()");
            txtValorContraExercicio1.Attributes.Add("onclick", "this.select()");
            txtFEASAnoAnteriorExercicio1.Attributes.Add("onclick", "this.select()");
            txtFEASDemandasExercicio1.Attributes.Add("onclick", "this.select()");
            txtFEASReprogramacaoDemandasParlamentaresExercicio1.Attributes.Add("onclick", "this.select()");
            txtFMIExercicio1.Attributes.Add("onclick", "this.select()");
            txtFEIExercicio1.Attributes.Add("onclick", "this.select()");
            txtFNIExercicio1.Attributes.Add("onclick", "this.select()");
            txtValorRecursoExercicio1.Attributes.Add("onclick", "this.select()");
           
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

            txtFMASExercicio2.Attributes.Add("onclick", "this.select()");
            txtFNASExercicio2.Attributes.Add("onclick", "this.select()");
            txtFEASExercicio2.Attributes.Add("onclick", "this.select()");
            txtFMDCAExercicio2.Attributes.Add("onclick", "this.select()");
            txtFEDCAExercicio2.Attributes.Add("onclick", "this.select()");
            txtFNDCAExercicio2.Attributes.Add("onclick", "this.select()");
            txtValorContraExercicio2.Attributes.Add("onclick", "this.select()");
            txtFEASAnoAnteriorExercicio2.Attributes.Add("onclick", "this.select()");
            txtFEASDemandasExercicio2.Attributes.Add("onclick", "this.select()");
            txtFEASReprogramacaoDemandasParlamentaresExercicio2.Attributes.Add("onclick", "this.select()");
            txtFMIExercicio2.Attributes.Add("onclick", "this.select()");
            txtFEIExercicio2.Attributes.Add("onclick", "this.select()");
            txtFNIExercicio2.Attributes.Add("onclick", "this.select()");
            txtValorRecursoExercicio2.Attributes.Add("onclick", "this.select()");
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


            txtNivelFundamental.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNivelMedio.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperior.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            //txtPosGraduacao.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtEstagiarios.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVoluntarios.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorServicoSocial.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorPsicologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorPedagogia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSociologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtDireito.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
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
        private void ClearSessao()
        {
            SessaoProgramaProjetoCofinanciamentoInfo1 = null;
            SessaoTransferenciaRendaCofinanciamentoInfo1 = null;
            SessaoConsultaProgramaProjetoServicoCofinanciamentoInfo = null;
            SessaoPrefeituraBeneficioEventualServicoInfo1 = null;
            rptProgramaTemp.DataSource = null;
            rptProgramaTemp.DataBind();
        }

        private void AposSalvoNaoPermitirEdicaoCamposCaracterizacao(ServicoRecursoFinanceiroPublicoInfo servico)
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

        #endregion

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
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

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


        protected void rblCriancasAuxilioReclusao_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Add("class", "frame active");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            if (rblCriancasAuxilioReclusao.SelectedValue == "1")
            {
                trValCriancasAuxilioReclusao.Visible = true;
            }
            else
            {
                trValCriancasAuxilioReclusao.Visible = false;
            }
        }


        protected void rblCriancasPensaoMorte_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Add("class", "frame active");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            if (rblCriancasPensaoMorte.SelectedValue == "1")
            {
                trValCriancasPensaoMorte.Visible = true;
            }
            else
            {
                trValCriancasPensaoMorte.Visible = false;
            }
        }

        protected void rblCriancasAuxilioReclusaoExercicio2025_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Add("class", "frame active");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            frame2_5_ano2.Attributes.Add("class", "active");

            if (rblCriancasAuxilioReclusaoExercicio2025.SelectedValue == "1")
            {
                trValCriancasAuxilioReclusaoExercicio2025.Visible = true;
            }
            else
            {
                trValCriancasAuxilioReclusaoExercicio2025.Visible = false;
            }
        }

        protected void rblCriancasPensaoMorteExercicio2025_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Add("class", "frame active");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            frame2_5_ano2.Attributes.Add("class", "active");

            if (rblCriancasPensaoMorteExercicio2025.SelectedValue == "1")
            {
                trValCriancasPensaoMorteExercicio2025.Visible = true;
            }
            else
            {
                trValCriancasPensaoMorteExercicio2025.Visible = false;
            }  

        }

    }
}