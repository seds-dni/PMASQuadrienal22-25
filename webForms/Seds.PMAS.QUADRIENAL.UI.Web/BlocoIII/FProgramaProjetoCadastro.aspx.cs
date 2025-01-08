using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FProgramaProjetoCadastro : System.Web.UI.Page
    {
        #region properties
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        public ValidacaoProgramaProjetoEstrutura GrupoASerValidadoProgramaProjetoEstrutura { get; set; }
        #endregion

        #region sessao
        #region sessao parceria
        protected List<ProgramaProjetoParceriaInfo> SessaoParcerias
        {
            get { return Session["PARCERIAS"] as List<ProgramaProjetoParceriaInfo>; }
            set { Session["PARCERIAS"] = value; }
        }


        protected List<IdentificacaoTerritorioInfo> IdentificacoesTerritorio
        {
            get { return Session["IDENTIFICACOESTERRITORIOS"] as List<IdentificacaoTerritorioInfo>; }
            set { Session["IDENTIFICACOESTERRITORIOS"] = value; }
        }

        protected List<UnidadeOfertanteInfo> UnidadesOfertantes
        {
            get { return Session["UNIDADESOFERTANTES"] as List<UnidadeOfertanteInfo>; }
            set { Session["UNIDADESOFERTANTES"] = value; }
        }

        protected Boolean? ProgramaMunicipal { get; set; }

        private bool Botao { get; set; }

        #endregion
        #endregion sessao

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);
                Session["PARCERIAS"] = null;


                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                AdicionarEventos();

                using (var proxy = new ProxyProgramas())
                {
                    LoadInicial(proxy);
                }

                BloquearControles();

            }

            CarregarExercicios();

        }

        private void LoadInicial(ProxyProgramas proxy)
        {
            hdfAno.Value = String.IsNullOrEmpty(hdfAno.Value) ? Exercicios[0].ToString() : hdfAno.Value;
            SelecionarCorAba();
            int exercicio = Convert.ToInt32(hdfAno.Value);

            var codPrograma = Request.QueryString["p"];
            
            this.CarregarProgramaSelecionado(exercicio);
            this.AplicarRegraBloqueioDesbloqueioRecursosFinanceiros();
            this.AplicarRegraBloqueioDesbloqueioMetaPactuada();
            this.AplicarRegraBloqueioDesbloqueioPrevisaoAnual();


        }

        #region Carregamento [Programas]
        private void CarregarProgramaSelecionado(int exercicio)
        {
            if(!Botao)
            CarregarCombosPorPrograma();

            using (var proxy = new ProxyProgramas())
            {
                int idPrograma = 0;
                if (Request.QueryString["id"] != null)
                {
                    idPrograma = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                }
                ProgramaProjetoInfo programaProjetoInfo = null;
                if(idPrograma != 0){
                    programaProjetoInfo = proxy.Service.GetProgramaProjetoById(idPrograma);
                    var ppRecursoFinanceiroExercicio = programaProjetoInfo.ProgramasProjetosRecursoFinanceiro.FirstOrDefault(x => x.Exercicio == exercicio);
                    this.CarregarRecursosPorProgramaEExercicio(programaProjetoInfo, ppRecursoFinanceiroExercicio, exercicio);
                }

                #region Verifica e carrega: ProgramaMunicipal
                if (rblTransferenciaRendaDireta.SelectedValue == "0")
                {
                    this.HelperEsconderTodosAsEstruturas();
                    this.HelperLimparTodasAsEstruturasERecursos();
                    this.ExibirEstruturaProgramasMunicipais(exercicio);
                }
                else
                {
                    this.ExibirEstruturaProgramasMunicipais(exercicio);
                }


                if(programaProjetoInfo != null)
                this.CarregarProgramasMunicipais(proxy, programaProjetoInfo);

                #region #region Carregar Recursos Financeiros [Federal | Estadual | Municipal]
                trRecursosFinanceiros.Visible = true;
                trRecursosFinanceirosAbas.Visible = true;

                trRecursosFinanceirosExercicio1.Visible = (exercicio == Exercicios[0]);
                trRecursosFinanceirosExercicio2.Visible = (exercicio == Exercicios[1]);
                trRecursosFinanceirosExercicio3.Visible = (exercicio == Exercicios[2]);
                trRecursosFinanceirosExercicio4.Visible = (exercicio == Exercicios[3]);

                trRecursosFinanceirosFederalExercicio1.Visible = (exercicio == Exercicios[0]);
                trRecursosFinanceirosFederalExercicio2.Visible = (exercicio == Exercicios[1]);
                trRecursosFinanceirosFederalExercicio3.Visible = (exercicio == Exercicios[2]);
                trRecursosFinanceirosFederalExercicio4.Visible = (exercicio == Exercicios[3]);

                trRecursosFinanceirosEstadualExercicio1.Visible = (exercicio == Exercicios[0]);
                trRecursosFinanceirosEstadualExercicio2.Visible = (exercicio == Exercicios[1]);
                trRecursosFinanceirosEstadualExercicio3.Visible = (exercicio == Exercicios[2]);
                trRecursosFinanceirosEstadualExercicio4.Visible = (exercicio == Exercicios[3]);

                trRecursosFinanceirosMunicipalExercicio1.Visible = (exercicio == Exercicios[0]);
                trRecursosFinanceirosMunicipalExercicio2.Visible = (exercicio == Exercicios[1]);
                trRecursosFinanceirosMunicipalExercicio3.Visible = (exercicio == Exercicios[2]);
                trRecursosFinanceirosMunicipalExercicio4.Visible = (exercicio == Exercicios[3]);
                #endregion

                #endregion
            }
        }

        private void CarregarProgramasMunicipais(ProxyProgramas proxy, ProgramaProjetoInfo programaProjetoInfo)
        {
            #region Labels
            lblNumeracao.Text = "3.23";
            txtNome.Text = programaProjetoInfo.Nome;
            txtObjetivo.Text = programaProjetoInfo.Objetivo;
            #endregion


            lblQuadro.Visible = true;

            this.CarregarEstruturaNomePrograma(programaProjetoInfo);
            this.CarregarEstruturaTipoPrograma(programaProjetoInfo);
            this.CarregarEstruturaPrevisaoAnual(proxy, programaProjetoInfo);
            this.CarregarEstruturaAtividadesRealizadas(programaProjetoInfo);
            this.CarregarEstruturaBeneficiarios(programaProjetoInfo);
            this.CarregarEstruturaAbrangencia(programaProjetoInfo);
            this.CarregarEstruturaParcerias(programaProjetoInfo);

        }

        #endregion

        #region Carregamento [Etc]
        private void carregarIdentificacoesTerritorios()
        {
            lstIdentificacaoTerritorio.DataSource = IdentificacoesTerritorio;
            lstIdentificacaoTerritorio.DataBind();
        }
        private void carregarUnidadesOfertantes()
        {
            lstUnidadeOfertante.DataSource = UnidadesOfertantes;
            lstUnidadeOfertante.DataBind();
        }
        private void carregarPlanejamentoBens()
        {
        }
        private void carregarPlanejamentoServicos()
        {
        }
        private void CarregarCombosPorPrograma()
        {
            using (var proxyEstruturaAssistenciaSocial = new ProxyEstruturaAssistenciaSocial())
            {

                ddlParceria.Items.Clear();
                ddlParceria.SelectedValue = null;
                ddlParceria.DataValueField = "Id";
                ddlParceria.DataTextField = "Nome";
                ddlParceria.DataSource = proxyEstruturaAssistenciaSocial.Service.GetParcerias();
                Util.InserirItemEscolha(ddlParceria);
                ddlParceria.DataBind();

                ddlTipoParceria.Items.Clear();
                ddlTipoParceria.SelectedValue = null;
                ddlTipoParceria.DataValueField = "Id";
                ddlTipoParceria.DataTextField = "Nome";
                ddlTipoParceria.DataSource = proxyEstruturaAssistenciaSocial.Service.GetTiposParceria();
                ddlTipoParceria.DataBind();


                ddlEixoTecnologico.Items.Clear();
                ddlEixoTecnologico.DataSource = proxyEstruturaAssistenciaSocial.Service.GetEixosTecnologicos().ToList();
                ddlEixoTecnologico.DataValueField = "Id";
                ddlEixoTecnologico.DataTextField = "Descricao";
                ddlEixoTecnologico.DataBind();
                Util.InserirItemEscolha(ddlEixoTecnologico);

                chkCaracterizacaoUsuarios.Items.Clear();
                chkCaracterizacaoUsuarios.SelectedValue = null;
                chkCaracterizacaoUsuarios.AppendDataBoundItems = true;
                chkCaracterizacaoUsuarios.DataValueField = "Id";
                chkCaracterizacaoUsuarios.DataTextField = "Descricao";
                chkCaracterizacaoUsuarios.DataSource = proxyEstruturaAssistenciaSocial.Service.GetCaracterizacaoUsuarios().ToList();
                chkCaracterizacaoUsuarios.DataBind();

                //chkAcoesSocioassistenciais.Items.Clear();
                //chkAcoesSocioassistenciais.SelectedValue = null;
                //chkAcoesSocioassistenciais.AppendDataBoundItems = true;
                //chkAcoesSocioassistenciais.DataSource = proxyEstruturaAssistenciaSocial.Service.GetAcoesSocioAssistenciaisUnidades().ToList();
                //chkAcoesSocioassistenciais.DataValueField = "Id";
                //chkAcoesSocioassistenciais.DataTextField = "Nome";
                //chkAcoesSocioassistenciais.DataBind();

                //ddlParceria.DataBind();
                //Util.InserirItemEscolha(ddlParceria);

                //ddlTipoParceria.DataBind();
                //Util.InserirItemEscolha(ddlTipoParceria);
                ddlBeneficiarios.Items.Clear();
                ddlBeneficiarios.DataSource = proxyEstruturaAssistenciaSocial.Service.GetUsuarioTransferenciaRenda().ToList();
                ddlBeneficiarios.DataValueField = "Id";
                ddlBeneficiarios.DataTextField = "Nome";
               
                ddlBeneficiarios.DataBind();
                Util.InserirItemEscolha(ddlBeneficiarios);
                ddlBeneficiarios.Items.RemoveAt(17);

                //chkAcoesDesenvolvida.Items.Clear();
                //chkAcoesDesenvolvida.SelectedValue = null;
                //chkAcoesDesenvolvida.AppendDataBoundItems = true;
                //chkAcoesDesenvolvida.DataValueField = "Id";
                //chkAcoesDesenvolvida.DataTextField = "Descricao";
                //chkAcoesDesenvolvida.DataBind();
            }
        }
        #endregion

        #region SALVAR
        #region SALVAR: preencher
        private void PreencherRecursosFinanceirosDoPrograma(ProgramaProjetoInfo obj)
        {
            int exercicio = String.IsNullOrEmpty(hdfAno.Value) ? 2018 : Convert.ToInt32(hdfAno.Value);

            if (obj.ProgramasProjetosRecursoFinanceiro != null)
            {
                var recurso = obj.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).SingleOrDefault();
                if (recurso != null)
                {
                    #region Exercicio 1

                    if (exercicio == Exercicios[0])
                    {
                        recurso.FonteOrcamentoEstadual = chkOrcamentoEstadualExercicio1.Checked;
                        recurso.ValorOrcamentoEstadual = chkOrcamentoEstadualExercicio1.Checked ? Convert.ToDecimal(txtOrcamentoEstadualExercicio1.Text) : 0M;
                        recurso.FonteFundoEstadual = chkOutrosFundosEstaduaisExercicio1.Checked;
                        recurso.ValorFundoEstadual = chkOutrosFundosEstaduaisExercicio1.Checked ? Convert.ToDecimal(txtOutrosFundosEstaduaisExercicio1.Text) : 0M;
                        recurso.FonteFNAS = chkFNASExercicio1.Checked;
                        recurso.ValorFNAS = chkFNASExercicio1.Checked ? Convert.ToDecimal(txtFNASExercicio1.Text) : 0M;
                        recurso.FonteOrcamentoFederal = chkOrcamentoFederalExercicio1.Checked;
                        recurso.ValorOrcamentoFederal = chkOrcamentoFederalExercicio1.Checked ? Convert.ToDecimal(txtOrcamentoFederalExercicio1.Text) : 0M;
                        recurso.FonteFundoFederal = chkOutrosFundosFederaisExercicio1.Checked;
                        recurso.ValorFundoFederal = chkOutrosFundosFederaisExercicio1.Checked ? Convert.ToDecimal(txtOutrosFundosFederaisExercicio1.Text) : 0M;
                        recurso.FonteIGDPBF = chkIGDPBFExercicio1.Checked;
                        recurso.ValorIGDPBF = chkIGDPBFExercicio1.Checked ? Convert.ToDecimal(txtIGDPBFExercicio1.Text) : 0M;
                        recurso.FonteIGDSUAS = chkIGDSUASExercicio1.Checked;
                        recurso.ValorIGDSUAS = chkIGDSUASExercicio1.Checked ? Convert.ToDecimal(txtIGDSUASExercicio1.Text) : 0M;

                    }
                    #endregion

                    #region Exercicio 2

                    if (exercicio == Exercicios[1])
                    {
                        recurso.FonteOrcamentoEstadual = chkOrcamentoEstadualExercicio2.Checked;
                        recurso.ValorOrcamentoEstadual = chkOrcamentoEstadualExercicio2.Checked ? Convert.ToDecimal(txtOrcamentoEstadualExercicio2.Text) : 0M;
                        recurso.FonteFundoEstadual = chkOutrosFundosEstaduaisExercicio2.Checked;
                        recurso.ValorFundoEstadual = chkOutrosFundosEstaduaisExercicio2.Checked ? Convert.ToDecimal(txtOutrosFundosEstaduaisExercicio2.Text) : 0M;
                        recurso.FonteFNAS = chkFNASExercicio2.Checked;
                        recurso.ValorFNAS = chkFNASExercicio2.Checked ? Convert.ToDecimal(txtFNASExercicio2.Text) : 0M;
                        recurso.FonteOrcamentoFederal = chkOrcamentoFederalExercicio2.Checked;
                        recurso.ValorOrcamentoFederal = chkOrcamentoFederalExercicio2.Checked ? Convert.ToDecimal(txtOrcamentoFederalExercicio2.Text) : 0M;
                        recurso.FonteFundoFederal = chkOutrosFundosFederaisExercicio2.Checked;
                        recurso.ValorFundoFederal = chkOutrosFundosFederaisExercicio2.Checked ? Convert.ToDecimal(txtOutrosFundosFederaisExercicio2.Text) : 0M;
                        recurso.FonteIGDPBF = chkIGDPBFExercicio2.Checked;
                        recurso.ValorIGDPBF = chkIGDPBFExercicio2.Checked ? Convert.ToDecimal(txtIGDPBFExercicio2.Text) : 0M;
                        recurso.FonteIGDSUAS = chkIGDSUASExercicio2.Checked;
                        recurso.ValorIGDSUAS = chkIGDSUASExercicio2.Checked ? Convert.ToDecimal(txtIGDSUASExercicio2.Text) : 0M;
                    }
                    #endregion

                    #region Exercicio 3

                    if (exercicio == Exercicios[2])
                    {
                        recurso.FonteOrcamentoEstadual = chkOrcamentoEstadualExercicio3.Checked;
                        recurso.ValorOrcamentoEstadual = chkOrcamentoEstadualExercicio3.Checked ? Convert.ToDecimal(txtOrcamentoEstadualExercicio3.Text) : 0M;

                        recurso.FonteFundoEstadual = chkOutrosFundosEstaduaisExercicio3.Checked;
                        recurso.ValorFundoEstadual = chkOutrosFundosEstaduaisExercicio3.Checked ? Convert.ToDecimal(txtOutrosFundosEstaduaisExercicio3.Text) : 0M;

                        recurso.FonteFNAS = chkFNASExercicio3.Checked;
                        recurso.ValorFNAS = chkFNASExercicio3.Checked ? Convert.ToDecimal(txtFNASExercicio3.Text) : 0M;

                        //Federal
                        recurso.FonteOrcamentoFederal = chkOrcamentoFederalExercicio3.Checked;
                        recurso.ValorOrcamentoFederal = chkOrcamentoFederalExercicio3.Checked ? Convert.ToDecimal(txtOrcamentoFederalExercicio3.Text) : 0M;

                        recurso.FonteFundoFederal = chkOutrosFundosFederaisExercicio3.Checked;
                        recurso.ValorFundoFederal = chkOutrosFundosFederaisExercicio3.Checked ? Convert.ToDecimal(txtOutrosFundosFederaisExercicio3.Text) : 0M;

                        //indices
                        recurso.FonteIGDPBF = chkIGDPBFExercicio3.Checked;
                        recurso.ValorIGDPBF = chkIGDPBFExercicio3.Checked ? Convert.ToDecimal(txtIGDPBFExercicio3.Text) : 0M;
                        recurso.FonteIGDSUAS = chkIGDSUASExercicio3.Checked;
                        recurso.ValorIGDSUAS = chkIGDSUASExercicio3.Checked ? Convert.ToDecimal(txtIGDSUASExercicio3.Text) : 0M;
                    }
                    #endregion

                    #region Exercicio 4

                    if (exercicio == Exercicios[3])
                    {
                        recurso.FonteOrcamentoEstadual = chkOrcamentoEstadualExercicio4.Checked;
                        recurso.ValorOrcamentoEstadual = chkOrcamentoEstadualExercicio4.Checked ? Convert.ToDecimal(txtOrcamentoEstadualExercicio4.Text) : 0M;
                        recurso.FonteFundoEstadual = chkOutrosFundosEstaduaisExercicio4.Checked;
                        recurso.ValorFundoEstadual = chkOutrosFundosEstaduaisExercicio4.Checked ? Convert.ToDecimal(txtOutrosFundosEstaduaisExercicio4.Text) : 0M;
                        recurso.FonteFNAS = chkFNASExercicio4.Checked;
                        recurso.ValorFNAS = chkFNASExercicio4.Checked ? Convert.ToDecimal(txtFNASExercicio4.Text) : 0M;
                        recurso.FonteOrcamentoFederal = chkOrcamentoFederalExercicio4.Checked;
                        recurso.ValorOrcamentoFederal = chkOrcamentoFederalExercicio4.Checked ? Convert.ToDecimal(txtOrcamentoFederalExercicio4.Text) : 0M;
                        recurso.FonteFundoFederal = chkOutrosFundosFederaisExercicio4.Checked;
                        recurso.ValorFundoFederal = chkOutrosFundosFederaisExercicio4.Checked ? Convert.ToDecimal(txtOutrosFundosFederaisExercicio4.Text) : 0M;
                        recurso.FonteIGDPBF = chkIGDPBFExercicio4.Checked;
                        recurso.ValorIGDPBF = chkIGDPBFExercicio4.Checked ? Convert.ToDecimal(txtIGDPBFExercicio4.Text) : 0M;
                        recurso.FonteIGDSUAS = chkIGDSUASExercicio4.Checked;
                        recurso.ValorIGDSUAS = chkIGDSUASExercicio4.Checked ? Convert.ToDecimal(txtIGDSUASExercicio4.Text) : 0M;
                    }
                    #endregion

                }
            }
        }
        private void PreencherRecursosFinanceirosRepassados(ProgramaProjetoInfo obj)
        {
            obj.PrevisaoAnual = new ProgramaProjetoPrevisaoAnualBeneficiariosInfo();
            obj.PrevisaoAnual.IdPrograma = obj.Id;

            if (!String.IsNullOrEmpty(txtRecursosFNASExercicio1.Text))
                obj.PrevisaoAnual.PrevisaoAnualRepasseExercicio1 = Convert.ToDecimal(txtRecursosFNASExercicio1.Text);

            if (!String.IsNullOrEmpty(txtRecursosFNASExercicio2.Text))
                obj.PrevisaoAnual.PrevisaoAnualRepasseExercicio2 = Convert.ToDecimal(txtRecursosFNASExercicio2.Text);

            if (!String.IsNullOrEmpty(txtRecursosFNASExercicio3.Text))
                obj.PrevisaoAnual.PrevisaoAnualRepasseExercicio3 = Convert.ToDecimal(txtRecursosFNASExercicio3.Text);

            if (!String.IsNullOrEmpty(txtRecursosFNASExercicio4.Text))
                obj.PrevisaoAnual.PrevisaoAnualRepasseExercicio4 = Convert.ToDecimal(txtRecursosFNASExercicio4.Text);

        }
        private void PreencherPrevisaoAnualBeneficiarios(ProgramaProjetoInfo obj)
        {
            obj.PrevisaoAnual = new ProgramaProjetoPrevisaoAnualBeneficiariosInfo();
            obj.PrevisaoAnual.IdPrograma = obj.Id;

            if (!String.IsNullOrEmpty(txtMetaPactuadaExercicio1.Text))
                obj.PrevisaoAnual.MetaPactuadaExercicio1 = Convert.ToInt32(txtMetaPactuadaExercicio1.Text);
            if (!String.IsNullOrEmpty(txtPrevisaoAnualExercicio1.Text))
                obj.PrevisaoAnual.PrevisaoAnualRepasseExercicio1 = Convert.ToDecimal(txtPrevisaoAnualExercicio1.Text);

            if (!String.IsNullOrEmpty(txtMetaPactuadaExercicio2.Text))
                obj.PrevisaoAnual.MetaPactuadaExercicio2 = Convert.ToInt32(txtMetaPactuadaExercicio2.Text);
            if (!String.IsNullOrEmpty(txtPrevisaoAnualExercicio2.Text))
                obj.PrevisaoAnual.PrevisaoAnualRepasseExercicio2 = Convert.ToDecimal(txtPrevisaoAnualExercicio2.Text);

            if (!String.IsNullOrEmpty(txtMetaPactuadaExercicio3.Text))
                obj.PrevisaoAnual.MetaPactuadaExercicio3 = Convert.ToInt32(txtMetaPactuadaExercicio3.Text);
            if (!String.IsNullOrEmpty(txtPrevisaoAnualExercicio3.Text))
                obj.PrevisaoAnual.PrevisaoAnualRepasseExercicio3 = Convert.ToDecimal(txtPrevisaoAnualExercicio3.Text);

            if (!String.IsNullOrEmpty(txtMetaPactuadaExercicio4.Text))
                obj.PrevisaoAnual.MetaPactuadaExercicio4 = Convert.ToInt32(txtMetaPactuadaExercicio4.Text);
            if (!String.IsNullOrEmpty(txtPrevisaoAnualExercicio4.Text))
                obj.PrevisaoAnual.PrevisaoAnualRepasseExercicio4 = Convert.ToDecimal(txtPrevisaoAnualExercicio4.Text);

        }

        private void PreencherDataProgramaProjeto(ProgramaProjetoInfo programaProjetoInfo)
        {
            #region inicio - término

            if (!String.IsNullOrEmpty(txtAnoInicio.Text))
            {
                programaProjetoInfo.AnoInicio = Convert.ToInt32(txtAnoInicio.Text);
            }
            if (!String.IsNullOrEmpty(txtAnoTermino.Text))
            {
                programaProjetoInfo.AnoTermino = Convert.ToInt32(txtAnoTermino.Text);
            }
            if (ddlMesInicio.SelectedValue != "0")
            {
                programaProjetoInfo.MesInicio = Convert.ToInt32(ddlMesInicio.SelectedValue);
            }
            if (ddlMesTermino.SelectedValue != "0")
            {
                programaProjetoInfo.MesTermino = Convert.ToInt32(ddlMesTermino.SelectedValue);
            }
            #endregion
        }


        #endregion
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int exercicio = String.IsNullOrEmpty(hdfAno.Value) ? 2018 : Convert.ToInt32(hdfAno.Value);
            SessaoPmas.VerificarSessao(this);


            var programaProjetoInfo = new ProgramaProjetoInfo();

            this.PreencherNaturezaRecursos(programaProjetoInfo);


            programaProjetoInfo.InterlocutorMunicipal = new InterlocutorMunicipalInfo();
            programaProjetoInfo.UnidadeOfertante = new List<UnidadeOfertanteInfo>();
            programaProjetoInfo.AcoesDesenvolvidasPrograma = new List<AcoesDesenvolvidaProgramasInfo>();
            programaProjetoInfo.CaracterizacaoUsuarios = new List<CaracterizacaoUsuariosInfo>();
            programaProjetoInfo.UnidadesPrivadas = new List<UnidadePrivadaInfo>();
            programaProjetoInfo.AcoesSocioAssistenciais = new List<AcaoSocioAssistencialInfo>();
            programaProjetoInfo.PlanoAcao = new PlanoAcaoInfo();

            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                programaProjetoInfo.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            }

            programaProjetoInfo.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            programaProjetoInfo.IdAbrangenciaTerritorial = Convert.ToInt32(rblAbrangencia.SelectedValue != "" ? rblAbrangencia.SelectedValue : "3");


            #region Nome do programa: Editar - Exibir
            if (!String.IsNullOrEmpty(txtNome.Text))
            {
                programaProjetoInfo.Nome = txtNome.Text;
            }
            else
            {
                if (!String.IsNullOrEmpty(lblNome.Text))
                    programaProjetoInfo.Nome = lblNome.Text;
            }
            #endregion

            if (!String.IsNullOrEmpty(txtObjetivo.Text))
            {
                programaProjetoInfo.Objetivo = txtObjetivo.Text;
            }
            else
            {
                if (!String.IsNullOrEmpty(lblObjetivo.Text))
                {
                    programaProjetoInfo.Objetivo = lblObjetivo.Text;
                }
            }

            if (txtAcoes.Text.Length > 600)
            {
                txtAcoes.Text = txtAcoes.Text.Substring(0, 600);
            }

            programaProjetoInfo.Acoes = txtAcoes.Text;


            if (rblParcerias.SelectedValue == "1")
            {
                var parcerias = ((List<ProgramaProjetoParceriaInfo>)Session["parcerias"]);
                programaProjetoInfo.PossuiParceriaFormal = parcerias != null ? parcerias.Count > 0 : false;
                programaProjetoInfo.Parcerias = parcerias;

            }
            else
            {
                Session["parcerias"] = new List<ProgramaProjetoParceriaInfo>();
                programaProjetoInfo.Parcerias = new List<ProgramaProjetoParceriaInfo>();
            }




            programaProjetoInfo.IdUsuarioTransferenciaRenda = Convert.ToInt32(ddlBeneficiarios.SelectedValue) > 0 ? Convert.ToInt32(ddlBeneficiarios.SelectedValue) : new Nullable<Int32>();
            programaProjetoInfo.ProgramasProjetosRecursoFinanceiro = new List<ProgramaProjetoRecursoFinanceiroInfo>();
            ProgramaProjetoRecursoFinanceiroInfo recurso = new ProgramaProjetoRecursoFinanceiroInfo();

            #region Recursos: Programa: [Municipal]

            if (exercicio == Exercicios[0])
            {

                recurso.Exercicio = exercicio;
                recurso.FonteFMAS = chkFMASExercicio1.Checked;
                recurso.ValorFMAS = chkFMASExercicio1.Checked ? Convert.ToDecimal(txtFMASExercicio1.Text) : 0.0M;

                recurso.FonteOrcamentoMunicipal = chkOrcamentoMunicipalExercicio1.Checked;
                recurso.ValorOrcamentoMunicipal = chkOrcamentoMunicipalExercicio1.Checked ? String.IsNullOrEmpty(txtOrcamentoMunicipalExercicio1.Text) ? 0M : Convert.ToDecimal(txtOrcamentoMunicipalExercicio1.Text) : 0M;

                recurso.FonteFundoMunicipal = chkOutrosFundosMunicipaisExercicio1.Checked;
                recurso.ValorFundoMunicipal = chkOutrosFundosMunicipaisExercicio1.Checked ? String.IsNullOrEmpty(txtOutrosFundosMunicipaisExercicio1.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosMunicipaisExercicio1.Text) : 0M;
            }

            if (exercicio == Exercicios[1])
            {

                recurso.Exercicio = exercicio;
                recurso.FonteFMAS = chkFMASExercicio2.Checked;
                recurso.ValorFMAS = chkFMASExercicio2.Checked ? Convert.ToDecimal(txtFMASExercicio2.Text) : 0.0M;

                recurso.FonteOrcamentoMunicipal = chkOrcamentoMunicipalExercicio2.Checked;
                recurso.ValorOrcamentoMunicipal = chkOrcamentoMunicipalExercicio2.Checked ? String.IsNullOrEmpty(txtOrcamentoMunicipalExercicio2.Text) ? 0M : Convert.ToDecimal(txtOrcamentoMunicipalExercicio2.Text) : 0M;

                recurso.FonteFundoMunicipal = chkOutrosFundosMunicipaisExercicio2.Checked;
                recurso.ValorFundoMunicipal = chkOutrosFundosMunicipaisExercicio2.Checked ? String.IsNullOrEmpty(txtOutrosFundosMunicipaisExercicio2.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosMunicipaisExercicio2.Text) : 0M;
            }

            if (exercicio == Exercicios[2])
            {

                recurso.Exercicio = exercicio;
                recurso.FonteFMAS = chkFMASExercicio3.Checked;
                recurso.ValorFMAS = chkFMASExercicio3.Checked ? Convert.ToDecimal(txtFMASExercicio3.Text) : 0.0M;

                recurso.FonteOrcamentoMunicipal = chkOrcamentoMunicipalExercicio3.Checked;
                recurso.ValorOrcamentoMunicipal = chkOrcamentoMunicipalExercicio3.Checked ? String.IsNullOrEmpty(txtOrcamentoMunicipalExercicio3.Text) ? 0M : Convert.ToDecimal(txtOrcamentoMunicipalExercicio3.Text) : 0M;

                recurso.FonteFundoMunicipal = chkOutrosFundosMunicipaisExercicio3.Checked;
                recurso.ValorFundoMunicipal = chkOutrosFundosMunicipaisExercicio3.Checked ? String.IsNullOrEmpty(txtOutrosFundosMunicipaisExercicio3.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosMunicipaisExercicio3.Text) : 0M;
            }


            if (exercicio == Exercicios[3])
            {

                recurso.Exercicio = exercicio;
                recurso.FonteFMAS = chkFMASExercicio4.Checked;
                recurso.ValorFMAS = chkFMASExercicio4.Checked ? Convert.ToDecimal(txtFMASExercicio4.Text) : 0.0M;

                recurso.FonteOrcamentoMunicipal = chkOrcamentoMunicipalExercicio4.Checked;
                recurso.ValorOrcamentoMunicipal = chkOrcamentoMunicipalExercicio4.Checked ? String.IsNullOrEmpty(txtOrcamentoMunicipalExercicio4.Text) ? 0M : Convert.ToDecimal(txtOrcamentoMunicipalExercicio4.Text) : 0M;

                recurso.FonteFundoMunicipal = chkOutrosFundosMunicipaisExercicio4.Checked;
                recurso.ValorFundoMunicipal = chkOutrosFundosMunicipaisExercicio4.Checked ? String.IsNullOrEmpty(txtOutrosFundosMunicipaisExercicio4.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosMunicipaisExercicio4.Text) : 0M;
            }
            #endregion


            #region Recursos: Programa: [Estadual]
            if (exercicio == Exercicios[0])
            {
                recurso.FonteOrcamentoEstadual = chkOrcamentoEstadualExercicio1.Checked;
                recurso.ValorOrcamentoEstadual = chkOrcamentoEstadualExercicio1.Checked ? String.IsNullOrEmpty(txtOrcamentoEstadualExercicio1.Text) ? 0M : Convert.ToDecimal(txtOrcamentoEstadualExercicio1.Text) : 0M;

                recurso.FonteFundoEstadual = chkOutrosFundosEstaduaisExercicio1.Checked;
                recurso.ValorFundoEstadual = chkOutrosFundosEstaduaisExercicio1.Checked ? String.IsNullOrEmpty(txtOutrosFundosEstaduaisExercicio1.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosEstaduaisExercicio1.Text) : 0M;

                recurso.FonteFEAS = chkFEASExercicio1.Checked;
                recurso.ValorFEAS = chkFEASExercicio1.Checked ? String.IsNullOrEmpty(txtFEASExercicio1.Text) ? 0M : Convert.ToDecimal(txtFEASExercicio1.Text) : 0M;
            }

            if (exercicio == Exercicios[1])
            {
                recurso.FonteOrcamentoEstadual = chkOrcamentoEstadualExercicio2.Checked;
                recurso.ValorOrcamentoEstadual = chkOrcamentoEstadualExercicio2.Checked ? String.IsNullOrEmpty(txtOrcamentoEstadualExercicio2.Text) ? 0M : Convert.ToDecimal(txtOrcamentoEstadualExercicio2.Text) : 0M;

                recurso.FonteFundoEstadual = chkOutrosFundosEstaduaisExercicio2.Checked;
                recurso.ValorFundoEstadual = chkOutrosFundosEstaduaisExercicio2.Checked ? String.IsNullOrEmpty(txtOutrosFundosEstaduaisExercicio2.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosEstaduaisExercicio2.Text) : 0M;

                recurso.FonteFEAS = chkFEASExercicio2.Checked;
                recurso.ValorFEAS = chkFEASExercicio2.Checked ? String.IsNullOrEmpty(txtFEASExercicio2.Text) ? 0M : Convert.ToDecimal(txtFEASExercicio2.Text) : 0M;
            }

            if (exercicio == Exercicios[2])
            {
                recurso.FonteOrcamentoEstadual = chkOrcamentoEstadualExercicio3.Checked;
                recurso.ValorOrcamentoEstadual = chkOrcamentoEstadualExercicio3.Checked ? String.IsNullOrEmpty(txtOrcamentoEstadualExercicio3.Text) ? 0M : Convert.ToDecimal(txtOrcamentoEstadualExercicio3.Text) : 0M;

                recurso.FonteFundoEstadual = chkOutrosFundosEstaduaisExercicio3.Checked;
                recurso.ValorFundoEstadual = chkOutrosFundosEstaduaisExercicio3.Checked ? String.IsNullOrEmpty(txtOutrosFundosEstaduaisExercicio3.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosEstaduaisExercicio3.Text) : 0M;

                recurso.FonteFEAS = chkFEASExercicio3.Checked;
                recurso.ValorFEAS = chkFEASExercicio3.Checked ? String.IsNullOrEmpty(txtFEASExercicio3.Text) ? 0M : Convert.ToDecimal(txtFEASExercicio3.Text) : 0M;
            }

            if (exercicio == Exercicios[3])
            {
                recurso.FonteOrcamentoEstadual = chkOrcamentoEstadualExercicio4.Checked;
                recurso.ValorOrcamentoEstadual = chkOrcamentoEstadualExercicio4.Checked ? String.IsNullOrEmpty(txtOrcamentoEstadualExercicio4.Text) ? 0M : Convert.ToDecimal(txtOrcamentoEstadualExercicio4.Text) : 0M;

                recurso.FonteFundoEstadual = chkOutrosFundosEstaduaisExercicio4.Checked;
                recurso.ValorFundoEstadual = chkOutrosFundosEstaduaisExercicio4.Checked ? String.IsNullOrEmpty(txtOutrosFundosEstaduaisExercicio4.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosEstaduaisExercicio4.Text) : 0M;

                recurso.FonteFEAS = chkFEASExercicio4.Checked;
                recurso.ValorFEAS = chkFEASExercicio4.Checked ? String.IsNullOrEmpty(txtFEASExercicio4.Text) ? 0M : Convert.ToDecimal(txtFEASExercicio4.Text) : 0M;
            }
            #endregion


            #region Recursos: Programa: [Federal]
            if (exercicio == Exercicios[0])
            {
                recurso.FonteFNAS = chkFNASExercicio1.Checked;
                recurso.ValorFNAS = chkFNASExercicio1.Checked ? (String.IsNullOrEmpty(txtFNASExercicio1.Text) ? 0M : Convert.ToDecimal(txtFNASExercicio1.Text)) : 0M;

                recurso.FonteOrcamentoFederal = chkOrcamentoFederalExercicio1.Checked;
                recurso.ValorOrcamentoFederal = chkOrcamentoFederalExercicio1.Checked ? String.IsNullOrEmpty(txtOutrosFundosFederaisExercicio1.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosFederaisExercicio1.Text) : 0M;

                recurso.FonteFundoFederal = chkOutrosFundosFederaisExercicio1.Checked;
                recurso.ValorFundoFederal = chkOutrosFundosFederaisExercicio1.Checked ? String.IsNullOrEmpty(txtOutrosFundosFederaisExercicio1.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosFederaisExercicio1.Text) : 0M;
            }

            if (exercicio == Exercicios[1])
            {
                recurso.FonteFNAS = chkFNASExercicio2.Checked;
                recurso.ValorFNAS = chkFNASExercicio2.Checked ? (String.IsNullOrEmpty(txtFNASExercicio2.Text) ? 0M : Convert.ToDecimal(txtFNASExercicio2.Text)) : 0M;

                recurso.FonteOrcamentoFederal = chkOrcamentoFederalExercicio2.Checked;
                recurso.ValorOrcamentoFederal = chkOrcamentoFederalExercicio2.Checked ? String.IsNullOrEmpty(txtOutrosFundosFederaisExercicio2.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosFederaisExercicio2.Text) : 0M;

                recurso.FonteFundoFederal = chkOutrosFundosFederaisExercicio2.Checked;
                recurso.ValorFundoFederal = chkOutrosFundosFederaisExercicio2.Checked ? String.IsNullOrEmpty(txtOutrosFundosFederaisExercicio2.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosFederaisExercicio2.Text) : 0M;
            }

            if (exercicio == Exercicios[2])
            {
                recurso.FonteFNAS = chkFNASExercicio3.Checked;
                recurso.ValorFNAS = chkFNASExercicio3.Checked ? (String.IsNullOrEmpty(txtFNASExercicio3.Text) ? 0M : Convert.ToDecimal(txtFNASExercicio3.Text)) : 0M;

                recurso.FonteOrcamentoFederal = chkOrcamentoFederalExercicio3.Checked;
                recurso.ValorOrcamentoFederal = chkOrcamentoFederalExercicio3.Checked ? String.IsNullOrEmpty(txtOutrosFundosFederaisExercicio3.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosFederaisExercicio3.Text) : 0M;

                recurso.FonteFundoFederal = chkOutrosFundosFederaisExercicio3.Checked;
                recurso.ValorFundoFederal = chkOutrosFundosFederaisExercicio3.Checked ? String.IsNullOrEmpty(txtOutrosFundosFederaisExercicio3.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosFederaisExercicio3.Text) : 0M;
            }

            if (exercicio == Exercicios[3])
            {
                recurso.FonteFNAS = chkFNASExercicio4.Checked;
                recurso.ValorFNAS = chkFNASExercicio4.Checked ? (String.IsNullOrEmpty(txtFNASExercicio4.Text) ? 0M : Convert.ToDecimal(txtFNASExercicio4.Text)) : 0M;

                recurso.FonteOrcamentoFederal = chkOrcamentoFederalExercicio4.Checked;
                recurso.ValorOrcamentoFederal = chkOrcamentoFederalExercicio4.Checked ? String.IsNullOrEmpty(txtOutrosFundosFederaisExercicio4.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosFederaisExercicio4.Text) : 0M;

                recurso.FonteFundoFederal = chkOutrosFundosFederaisExercicio4.Checked;
                recurso.ValorFundoFederal = chkOutrosFundosFederaisExercicio4.Checked ? String.IsNullOrEmpty(txtOutrosFundosFederaisExercicio4.Text) ? 0M : Convert.ToDecimal(txtOutrosFundosFederaisExercicio4.Text) : 0M;
            }
            #endregion


            #region indices
            if (exercicio == Exercicios[0])
            {
                recurso.FonteIGDPBF = chkIGDPBFExercicio1.Checked;
                recurso.ValorIGDPBF = chkIGDPBFExercicio1.Checked ? String.IsNullOrEmpty(txtIGDPBFExercicio1.Text) ? 0M : Convert.ToDecimal(txtIGDPBFExercicio1.Text) : 0M;

                recurso.FonteIGDSUAS = chkIGDSUASExercicio1.Checked;
                recurso.ValorIGDSUAS = chkIGDSUASExercicio1.Checked ? String.IsNullOrEmpty(txtIGDSUASExercicio1.Text) ? 0M : Convert.ToDecimal(txtIGDSUASExercicio1.Text) : 0M;
            }

            if (exercicio == Exercicios[1])
            {
                recurso.FonteIGDPBF = chkIGDPBFExercicio2.Checked;
                recurso.ValorIGDPBF = chkIGDPBFExercicio2.Checked ? String.IsNullOrEmpty(txtIGDPBFExercicio2.Text) ? 0M : Convert.ToDecimal(txtIGDPBFExercicio2.Text) : 0M;

                recurso.FonteIGDSUAS = chkIGDSUASExercicio2.Checked;
                recurso.ValorIGDSUAS = chkIGDSUASExercicio2.Checked ? String.IsNullOrEmpty(txtIGDSUASExercicio2.Text) ? 0M : Convert.ToDecimal(txtIGDSUASExercicio2.Text) : 0M;
            }

            if (exercicio == Exercicios[2])
            {
                recurso.FonteIGDPBF = chkIGDPBFExercicio3.Checked;
                recurso.ValorIGDPBF = chkIGDPBFExercicio3.Checked ? String.IsNullOrEmpty(txtIGDPBFExercicio3.Text) ? 0M : Convert.ToDecimal(txtIGDPBFExercicio3.Text) : 0M;

                recurso.FonteIGDSUAS = chkIGDSUASExercicio3.Checked;
                recurso.ValorIGDSUAS = chkIGDSUASExercicio3.Checked ? String.IsNullOrEmpty(txtIGDSUASExercicio3.Text) ? 0M : Convert.ToDecimal(txtIGDSUASExercicio3.Text) : 0M;
            }

            if (exercicio == Exercicios[3])
            {
                recurso.FonteIGDPBF = chkIGDPBFExercicio4.Checked;
                recurso.ValorIGDPBF = chkIGDPBFExercicio4.Checked ? String.IsNullOrEmpty(txtIGDPBFExercicio4.Text) ? 0M : Convert.ToDecimal(txtIGDPBFExercicio4.Text) : 0M;

                recurso.FonteIGDSUAS = chkIGDSUASExercicio4.Checked;
                recurso.ValorIGDSUAS = chkIGDSUASExercicio4.Checked ? String.IsNullOrEmpty(txtIGDSUASExercicio4.Text) ? 0M : Convert.ToDecimal(txtIGDSUASExercicio4.Text) : 0M;
            }


            #endregion

            programaProjetoInfo.ProgramasProjetosRecursoFinanceiro.Add(recurso);

            programaProjetoInfo.MesRepasse = Convert.ToInt32(ddlRepassePrimeiraParcela.SelectedValue);
            programaProjetoInfo.AnoRepasse = !String.IsNullOrEmpty(txtAnoRepassePrimeiraParcela.Text) ? Convert.ToInt32(txtAnoRepassePrimeiraParcela.Text) : Convert.ToInt32(0);

            PreencherRecursosFinanceirosDoPrograma(programaProjetoInfo);
            CarregarDadosTransferenciaRenda(programaProjetoInfo);

            if (!String.IsNullOrEmpty(rblTransferenciaRendaDireta.SelectedValue))
            {
                programaProjetoInfo.TransferenciaRendaDireta = rblTransferenciaRendaDireta.SelectedValue == "1" ? true : false;
                if (programaProjetoInfo.TransferenciaRendaDireta.Value == true)
                {
                    PreencherPrevisaoAnualBeneficiarios(programaProjetoInfo);
                    PreencherDataProgramaProjeto(programaProjetoInfo);

                }

            }


            String action = "PI";
            try
            {

                new ValidadorProgramaProjeto().Validar(programaProjetoInfo);

                using (var proxy = new ProxyProgramas())
                {
                    if (programaProjetoInfo.Id == 0)
                    {
                        proxy.Service.AddProgramaProjeto(programaProjetoInfo);
                    }
                    else
                    {
                        action = "PU";
                        proxy.Service.UpdateProgramaProjeto(programaProjetoInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.ToString()), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }


            Response.Redirect("~/BlocoIII/CProgramasProjetos.aspx?msg=" + action);
        }

        private void PreencherNaturezaRecursos(ProgramaProjetoInfo programaProjetoInfo)
        {
            programaProjetoInfo.ProgramaMunicipal = true;
        }
        #endregion

        #region Eventos

        #region parcerias
        protected void lstParcerias_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                var btn = ((ImageButton)e.Item.FindControl("btnExcluirParceria"));
                WebControl[] controles = { btn };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }
        protected void lstParcerias_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            using (var proxy = new ProxyProgramas())
            {
                int codigoPrograma = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                var programaProjetoInfo = proxy.Service.GetProgramaProjetoById(codigoPrograma);

                try
                {
                    switch (e.CommandName)
                    {
                        case "Excluir":
                            if (SessaoParcerias == null || SessaoParcerias.Count == 0)
                                break;
                            SessaoParcerias.RemoveAt(e.Item.DataItemIndex);
                            lstParcerias.DataSource = SessaoParcerias;
                            lstParcerias.DataBind();

                            var script = Util.GetJavaScriptDialogOK("Parceria excluída com sucesso!");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    var script = Util.GetJavaScriptDialogOK(ex.Message);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                }
            }
        }
        protected void btnAdicionarParceria_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            List<ProgramaProjetoParceriaInfo> parcerias = Session["parcerias"] as List<ProgramaProjetoParceriaInfo>;
            if (parcerias == null)
            {
                parcerias = new List<ProgramaProjetoParceriaInfo>();
            }



            var parceria = new ProgramaProjetoParceriaInfo();
            //programaProjetoInfo.PossuiParceriaFormal
            if (ddlTipoParceria.SelectedValue != string.Empty)
            {
                try
                {
                    if (parcerias.Exists(x => (x.NomeOrgao.ToUpper() == txtNomeOrgao.Text.ToUpper())
                                && (x.TipoParceria.Id == Convert.ToInt32(ddlTipoParceria.SelectedValue)) && (x.IdParceria == Convert.ToInt32(ddlParceria.SelectedValue))
                    ))
                    {
                        var lstMsg = new List<string>();
                        lstMsg.Add("Já existe uma parceria com mesmo tipo e orgão.");
                        throw new Exception(Extensions.Concat(lstMsg, System.Environment.NewLine));
                    }
                    parceria.IdTipoParceria = Convert.ToInt32(ddlTipoParceria.SelectedValue);
                    parceria.NomeOrgao = txtNomeOrgao.Text;
                    parceria.IdParceria = Convert.ToInt32(ddlParceria.SelectedValue);
                    parceria.Parceria = new ParceriaInfo() { Nome = ddlParceria.SelectedItem.Text };
                    parceria.TipoParceria = new TipoParceriaInfo() { Nome = ddlTipoParceria.SelectedItem.Text };
                    parcerias.Add(parceria);

                    new ValidadorProgramaProjetoParceria().Validar(parceria);
                }
                catch (Exception ex)
                {
                    lblInconsistenciasParceria.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                    tbInconsistenciasParceria.Visible = true;
                    return;
                }

                lstParcerias.DataSource = parcerias;
                lstParcerias.DataBind();
                lstParcerias.Visible = true;
            }

            Session["parcerias"] = parcerias;

            txtNomeOrgao.Text = String.Empty;
            lblInconsistenciasParceria.Text = string.Empty;
            tbInconsistenciasParceria.Visible = false;

            ddlTipoParceria.SelectedIndex = ddlParceria.SelectedIndex = 0;
            tbInconsistencias.Visible = false;
        }
        protected void rblParcerias_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbParcerias.Visible = rblParcerias.SelectedValue == "1";
        }
        protected void ddlParceria_SelectedIndexChanged(object sender, EventArgs e)
        {
            //using (var proxy = new ProxyEstruturaAssistenciaSocial())
            //{
            //    proxy.Service.GetTiposParceria();
            //}
        }
        #endregion

        #region Planejamento Bens
        protected void lstPlanejamentoBens_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluirPlanejamentoBem")) };
                if (!(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador))
                    Permissao.VerificarPermissaoControles(controles, Session);
            }
        }
        #endregion

        #region Planejamento Servicos
        protected void lstPlanejamentoServicos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluirPlanejamentoServico")) };
                if (!(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador))
                {
                    Permissao.VerificarPermissaoControles(controles, Session);
                }
            }
        }
        #endregion

        #region Unidades Ofertantes
        protected void btnAdicionarUnidadeOfertante_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            var unidadeOfertante = new UnidadeOfertanteInfo();

            unidadeOfertante.IdEixoTecnologico = Convert.ToInt32(ddlEixoTecnologico.SelectedValue);
            unidadeOfertante.EixoTecnologico = new EixoTecnologicoInfo() { Descricao = ddlEixoTecnologico.SelectedItem.Text };
            unidadeOfertante.NomeCurso = txtNomeCurso.Text;
            unidadeOfertante.UnidadeOfertante = txtUnidadeOfertante.Text;

            try
            {
                new ValidadorUnidadeOfertante().Validar(unidadeOfertante);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            UnidadesOfertantes = UnidadesOfertantes ?? new List<UnidadeOfertanteInfo>();
            UnidadesOfertantes.Add(unidadeOfertante);

            carregarUnidadesOfertantes();

            txtUnidadeOfertante.Text = String.Empty;
            ddlEixoTecnologico.SelectedIndex = ddlEixoTecnologico.SelectedIndex = 0;
            txtNomeCurso.Text = String.Empty;

        }
        protected void lstUnidadeOfertante_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Excluir":
                        if (UnidadesOfertantes == null || UnidadesOfertantes.Count == 0)
                            break;
                        UnidadesOfertantes.RemoveAt(e.Item.DataItemIndex);
                        carregarUnidadesOfertantes();
                        var script = Util.GetJavaScriptDialogOK("Unidade excluída com sucesso!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var script = Util.GetJavaScriptDialogOK(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
        }
        protected void lstUnidadeOfertante_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int sequencia = e.Item.DataItemIndex + 1;
            ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluirUnidadeOfertante")) };
            if (!(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador))
            {
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }
        #endregion

        protected void btnAdicionarIdentificacao_Click(object sender, EventArgs e)
        {
            var identificacaoTerritorio = new IdentificacaoTerritorioInfo();
            identificacaoTerritorio.NumeroIdentificacao = !String.IsNullOrEmpty(txtNumeroIdentificacaoTerritorio.Text) ? Convert.ToInt32(txtNumeroIdentificacaoTerritorio.Text) : default(Int32?);
            identificacaoTerritorio.IdentificacaoTerritorio = txtIdentificacaoTerritorio.Text;
            identificacaoTerritorio.NomeResponsavel = txtNomeResponsavelTerritorio.Text;
            identificacaoTerritorio.NumeroBeneficiarios = !String.IsNullOrEmpty(txtNumeroBeneficiariosTerritorio.Text) ? Convert.ToInt32(txtNumeroBeneficiariosTerritorio.Text.Replace(".", "")) : 0;
            try
            {
                new ValidadorIdentificacaoTerritorio().Validar(identificacaoTerritorio);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }
            IdentificacoesTerritorio = IdentificacoesTerritorio ?? new List<IdentificacaoTerritorioInfo>();
            IdentificacoesTerritorio.Add(identificacaoTerritorio);
            carregarIdentificacoesTerritorios();
            txtIdentificacaoTerritorio.Text = string.Empty;
            txtNomeResponsavelTerritorio.Text = string.Empty;
            txtNumeroBeneficiariosTerritorio.Text = string.Empty;
            return;
        }

        protected void lstGrupoGestor_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int sequencia = e.Item.DataItemIndex + 1;
            ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluirGrupoGestor")) };
            if (!(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador))
            {
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }
        protected void lstIdentificacaoTerritorio_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Excluir":
                        if (IdentificacoesTerritorio == null || IdentificacoesTerritorio.Count == 0)
                            break;
                        IdentificacoesTerritorio.RemoveAt(e.Item.DataItemIndex);
                        carregarIdentificacoesTerritorios();
                        var script = Util.GetJavaScriptDialogOK("Território excluído com sucesso!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var script = Util.GetJavascriptDialogError(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
        }
        protected void lstIdentificacaoTerritorio_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int sequencia = e.Item.DataItemIndex + 1;
            ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluirIdentificacaoTerritorio")) };
            if (!(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador))
            {
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        ////Infancia SUAS
        //protected void rblExecutaPrograma_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    bool exibirInfanciaSuas = (rblExecutaPrograma.SelectedValue == "1");
        //    int exercicio = String.IsNullOrEmpty(hdfAno.Value) ? 2018 : Convert.ToInt32(hdfAno.Value);

        //    this.ClearValoresEstruturaTipoPrograma();
        //    this.ClearValoresEstruturaExecutaPrograma();
        //    this.ClearValoresEstruturaDataProgramaProjeto();
        //    this.ClearValoresEstruturaDataAdesao();
        //    this.ClearValoresEstruturaInterlocutorMunicipal();
        //    this.ClearValoresEstruturaPrevisaoAnual();
        //    this.ClearValoresEstruturaMetaPactuada();
        //    this.ClearValoresEstruturaCaracterizacaoUsuarios();
        //    this.ClearValoresEstruturaAcoesDesenvolvidas();
        //    this.ClearValoresEstruturaAtividadesRealizadas();
        //    this.ClearValoresEstruturaAbrangencia();
        //    this.ClearValoresEstruturaParcerias();
        //    this.HelperLimparValoresRecursosFinanceiros();

        //    this.HelperEsconderTodosAsEstruturas();
        //}
        protected void rblTransferenciaRendaDireta_SelectedIndexChanged(object sender, EventArgs e)
        {

            int exercicio = String.IsNullOrEmpty(hdfAno.Value) ? 2018 : Convert.ToInt32(hdfAno.Value);
            this.ExibirTransferenciaRendaEstrutura();
        }

        private void ExibirTransferenciaRendaEstrutura()
        {
            if (rblTransferenciaRendaDireta.SelectedValue == "1")
            {
                trMetaPactuada.Visible = true;
                trDataProgramaProjeto.Visible = true;
                trAtividadesRealizadas.Visible = true;
                trParcerias.Visible = true;
                trAbrangencia.Visible = true;
                trPrevisaoAnual.Visible = true;
                trBeneficiarios.Visible = true;
                ddlBeneficiarios.Visible = true;
                trParcerias.Visible = true;
                lblQuadro.Visible = true;
                thPrevisaoAnualTotal.Visible = true;
                tdPrevisaoAnualTotalExercicio1.Visible = true;
                tdPrevisaoAnualTotalExercicio2.Visible = true;
                tdPrevisaoAnualTotalExercicio3.Visible = true;
                tdPrevisaoAnualTotalExercicio4.Visible = true;

                lblQuadro.Text = "Previsão anual de número de beneficiários de transferência direta de renda e valor de repasse";
                lblMetaPactuada.Text = "Previsão do número de beneficiários";
                lblHeadtbPrevisaoAnual.Text = "Previsão mensal do valor do repasse";
                lblDescricaoRecursosFinanceiros.Text = "Informe a(s) fonte(s) e valores dos recursos financeiros utilizados para a gestão e execução deste programa no município, excetuando-se aqueles que são repassados diretamente aos beneficiários.";
            }
            else if (rblTransferenciaRendaDireta.SelectedValue == "0")
            {
                trDataProgramaProjeto.Visible = false;
                trAtividadesRealizadas.Visible = true;
                trParcerias.Visible = true;
                trRecursosFinanceirosAbas.Visible = true;
                trRecursosFinanceiros.Visible = true;
                trAbrangencia.Visible = true;
                trBeneficiarios.Visible = true;
                ddlBeneficiarios.Visible = true;
                thPrevisaoAnualTotal.Visible =
                tdPrevisaoAnualTotalExercicio1.Visible = false;
                tdPrevisaoAnualTotalExercicio2.Visible = false;
                tdPrevisaoAnualTotalExercicio3.Visible = false;
                tdPrevisaoAnualTotalExercicio4.Visible = false;

                trPrevisaoAnual.Visible = trMetaPactuada.Visible = false;
            }
        }

        protected void btnLoadExercicio1_Click(object sender, EventArgs e)
        {
            #region QueryString
            int exercicio = Convert.ToInt32(btnExercicio1.Text);
            var naturezaDosRecursos = Request.QueryString["p"]; //F - Federal, E - Estadual, M - Municipal 
            #endregion
            hdfAno.Value = btnExercicio1.Text;

            #region reload

            Botao = true;

            if (rblTransferenciaRendaDireta.SelectedValue == "0")
            {
                this.HelperEsconderTodosAsEstruturas();
                this.HelperLimparTodasAsEstruturasERecursos();
                this.HelperLimparValoresRecursosFinanceiros();
                this.CarregarProgramaSelecionado(exercicio);
            }
            else
            {
                this.HelperLimparValoresRecursosFinanceiros();
                this.CarregarProgramaSelecionado(exercicio);
            }

            #endregion

            Botao = false;
            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }
        protected void btnLoadExercicio2_Click(object sender, EventArgs e)
        {
            #region QueryString
            int exercicio = Convert.ToInt32(btnExercicio2.Text);
            var naturezaDosRecursos = Request.QueryString["p"]; //F - Federal, E - Estadual, M - Municipal
            #endregion

            hdfAno.Value = btnExercicio2.Text;

            #region reload

            Botao = true;

            if (rblTransferenciaRendaDireta.SelectedValue == "0")
            {
                this.HelperEsconderTodosAsEstruturas();
                this.HelperLimparTodasAsEstruturasERecursos();
                this.HelperLimparValoresRecursosFinanceiros();
                this.CarregarProgramaSelecionado(exercicio);
            }
            else
            {
                this.HelperLimparValoresRecursosFinanceiros();
                this.CarregarProgramaSelecionado(exercicio);
            }


            #endregion

            Botao = false;
            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }
        protected void btnLoadExercicio3_Click(object sender, EventArgs e)
        {
            #region QueryString
            int exercicio = Convert.ToInt32(btnExercicio3.Text);
            var naturezaDosRecursos = Request.QueryString["p"]; //F - Federal, E - Estadual, M - Municipal
            #endregion
            hdfAno.Value = btnExercicio3.Text;

            #region reload
            Botao = true;
            if (rblTransferenciaRendaDireta.SelectedValue == "0")
            {
                this.HelperEsconderTodosAsEstruturas();
                this.HelperLimparTodasAsEstruturasERecursos();
                this.HelperLimparValoresRecursosFinanceiros();
                this.CarregarProgramaSelecionado(exercicio);
            }
            else
            {
                this.HelperLimparValoresRecursosFinanceiros();
                this.CarregarProgramaSelecionado(exercicio);
            }

            #endregion

            Botao = false;
            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }
        protected void btnLoadExercicio4_Click(object sender, EventArgs e)
        {
            #region QueryString
            int exercicio = Convert.ToInt32(btnExercicio4.Text);
            var naturezaDosRecursos = Request.QueryString["p"]; //F - Federal, E - Estadual, M - Municipal
            #endregion
            hdfAno.Value = btnExercicio4.Text;

            #region reload

            Botao = true;

            if (rblTransferenciaRendaDireta.SelectedValue == "0")
            {
                this.HelperEsconderTodosAsEstruturas();
                this.HelperLimparTodasAsEstruturasERecursos();
                this.HelperLimparValoresRecursosFinanceiros();
                this.CarregarProgramaSelecionado(exercicio);
            }
            else
            {
                this.HelperLimparValoresRecursosFinanceiros();
                this.CarregarProgramaSelecionado(exercicio);
            }

            #endregion

            Botao = false;
            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }



        #region Actions: Exercicio1

        protected void chkFMASExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFMASExercicio1.Checked)
            {
                txtFMASExercicio1.Text = "0,00";
            }
            txtFMASExercicio1.Enabled = chkFMASExercicio1.Checked;
            SelecionarCorAba();
        }
        protected void chkFEASExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFEASExercicio1.Checked)
            {
                txtFEASExercicio1.Text = "0,00";
            }
            txtFEASExercicio1.Enabled = chkFEASExercicio1.Checked;
        }
        protected void chkFNASExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFNASExercicio1.Checked)
            {
                txtFNASExercicio1.Text = "0,00";
            }
            txtFNASExercicio1.Enabled = chkFNASExercicio1.Checked;
        }

        protected void chkOrcamentoMunicipalExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoMunicipalExercicio1.Checked)
            {
                txtOrcamentoMunicipalExercicio1.Text = "0,00";
            }

            txtOrcamentoMunicipalExercicio1.Enabled = chkOrcamentoMunicipalExercicio1.Checked;
        }
        protected void chkOutrosFundosMunicipaisExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosMunicipaisExercicio1.Checked)
            {
                txtOutrosFundosMunicipaisExercicio1.Text = "0,00";
            }

            txtOutrosFundosMunicipaisExercicio1.Enabled = chkOutrosFundosMunicipaisExercicio1.Checked;
        }
        protected void chkOrcamentoEstadualExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoEstadualExercicio1.Checked)
            {
                txtOrcamentoEstadualExercicio1.Text = "0,00";
            }
            txtOrcamentoEstadualExercicio1.Enabled = chkOrcamentoEstadualExercicio1.Checked;
        }
        protected void chkOutrosFundosEstaduaisExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosEstaduaisExercicio1.Checked)
            {
                txtOutrosFundosEstaduaisExercicio1.Text = "0,00";
            }
            txtOutrosFundosEstaduaisExercicio1.Enabled = chkOutrosFundosEstaduaisExercicio1.Checked;
        }
        protected void chkOrcamentoFederalExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoFederalExercicio1.Checked)
            {
                txtOrcamentoFederalExercicio1.Text = "0,00";
            }
            txtOrcamentoFederalExercicio1.Enabled = chkOrcamentoFederalExercicio1.Checked;
        }
        protected void chkOutrosFundosFederaisExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosFederaisExercicio1.Checked)
            {
                txtOutrosFundosFederaisExercicio1.Text = "0,00";
            }
            txtOutrosFundosFederaisExercicio1.Enabled = chkOutrosFundosFederaisExercicio1.Checked;
        }
        protected void chkIGDPAIFExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkIGDPBFExercicio1.Checked)
            {
                txtIGDPBFExercicio1.Text = "0,00";
            }
            txtIGDPBFExercicio1.Enabled = chkIGDPBFExercicio1.Checked;
        }
        protected void chkIGDSUASExercicio1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkIGDSUASExercicio1.Checked)
            {
                txtIGDSUASExercicio1.Text = "0,00";
            }
            txtIGDSUASExercicio1.Enabled = chkIGDSUASExercicio1.Checked;
        }
        #endregion

        #region Actions: Exercicio2
        protected void chkFMASExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFMASExercicio2.Checked)
            {
                txtFMASExercicio2.Text = "0,00";
            }
            txtFMASExercicio2.Enabled = chkFMASExercicio2.Checked;
        }
        protected void chkFEASExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFEASExercicio2.Checked)
            {
                txtFEASExercicio2.Text = "0,00";
            }
            txtFEASExercicio2.Enabled = chkFEASExercicio2.Checked;

        }
        protected void chkFNASExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFNASExercicio2.Checked)
            {
                txtFNASExercicio2.Text = "0,00";
            }
            txtFNASExercicio2.Enabled = chkFNASExercicio2.Checked;
        }

        protected void chkOrcamentoMunicipalExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoMunicipalExercicio2.Checked)
            {
                txtOrcamentoMunicipalExercicio2.Text = "0,00";
            }

            txtOrcamentoMunicipalExercicio2.Enabled = chkOrcamentoMunicipalExercicio2.Checked;
        }
        protected void chkOutrosFundosMunicipaisExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosMunicipaisExercicio2.Checked)
            {
                txtOutrosFundosMunicipaisExercicio2.Text = "0,00";
            }

            txtOutrosFundosMunicipaisExercicio2.Enabled = chkOutrosFundosMunicipaisExercicio2.Checked;
        }
        protected void chkOrcamentoEstadualExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoEstadualExercicio2.Checked)
            {
                txtOrcamentoEstadualExercicio2.Text = "0,00";
            }
            txtOrcamentoEstadualExercicio2.Enabled = chkOrcamentoEstadualExercicio2.Checked;
        }
        protected void chkOutrosFundosEstaduaisExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosEstaduaisExercicio2.Checked)
            {
                txtOutrosFundosEstaduaisExercicio2.Text = "0,00";
            }
            txtOutrosFundosEstaduaisExercicio2.Enabled = chkOutrosFundosEstaduaisExercicio2.Checked;
        }
        protected void chkOrcamentoFederalExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoFederalExercicio2.Checked)
            {
                txtOrcamentoFederalExercicio2.Text = "0,00";
            }
            txtOrcamentoFederalExercicio2.Enabled = chkOrcamentoFederalExercicio2.Checked;
        }
        protected void chkOutrosFundosFederaisExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosFederaisExercicio2.Checked)
            {
                txtOutrosFundosFederaisExercicio2.Text = "0,00";
            }
            txtOutrosFundosFederaisExercicio2.Enabled = chkOutrosFundosFederaisExercicio2.Checked;
        }
        protected void chkIGDPAIFExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkIGDPBFExercicio2.Checked)
            {
                txtIGDPBFExercicio2.Text = "0,00";
            }
            txtIGDPBFExercicio2.Enabled = chkIGDPBFExercicio2.Checked;
        }
        protected void chkIGDSUASExercicio2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkIGDSUASExercicio2.Checked)
            {
                txtIGDSUASExercicio2.Text = "0,00";
            }
            txtIGDSUASExercicio2.Enabled = chkIGDSUASExercicio2.Checked;
        }
        #endregion

        #region Actions: Exercicio3
        protected void chkFMASExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFMASExercicio3.Checked)
            {
                txtFMASExercicio3.Text = "0,00";
            }
            txtFMASExercicio3.Enabled = chkFMASExercicio3.Checked;
        }
        protected void chkFEASExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFEASExercicio3.Checked)
            {
                txtFEASExercicio3.Text = "0,00";
            }
            txtFEASExercicio3.Enabled = chkFEASExercicio3.Checked;

        }
        protected void chkFNASExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFNASExercicio3.Checked)
            {
                txtFNASExercicio3.Text = "0,00";
            }
            txtFNASExercicio3.Enabled = chkFNASExercicio3.Checked;
        }

        protected void chkOrcamentoMunicipalExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoMunicipalExercicio3.Checked)
            {
                txtOrcamentoMunicipalExercicio3.Text = "0,00";
            }

            txtOrcamentoMunicipalExercicio3.Enabled = chkOrcamentoMunicipalExercicio3.Checked;
        }
        protected void chkOutrosFundosMunicipaisExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosMunicipaisExercicio3.Checked)
            {
                txtOutrosFundosMunicipaisExercicio3.Text = "0,00";
            }

            txtOutrosFundosMunicipaisExercicio3.Enabled = chkOutrosFundosMunicipaisExercicio3.Checked;
        }
        protected void chkOrcamentoEstadualExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoEstadualExercicio3.Checked)
            {
                txtOrcamentoEstadualExercicio3.Text = "0,00";
            }
            txtOrcamentoEstadualExercicio3.Enabled = chkOrcamentoEstadualExercicio3.Checked;
        }
        protected void chkOutrosFundosEstaduaisExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosEstaduaisExercicio3.Checked)
            {
                txtOutrosFundosEstaduaisExercicio3.Text = "0,00";
            }
            txtOutrosFundosEstaduaisExercicio3.Enabled = chkOutrosFundosEstaduaisExercicio3.Checked;
        }
        protected void chkOrcamentoFederalExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoFederalExercicio3.Checked)
            {
                txtOrcamentoFederalExercicio3.Text = "0,00";
            }
            txtOrcamentoFederalExercicio3.Enabled = chkOrcamentoFederalExercicio3.Checked;
        }
        protected void chkOutrosFundosFederaisExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosFederaisExercicio3.Checked)
            {
                txtOutrosFundosFederaisExercicio3.Text = "0,00";
            }
            txtOutrosFundosFederaisExercicio3.Enabled = chkOutrosFundosFederaisExercicio3.Checked;
        }
        protected void chkIGDPAIFExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkIGDPBFExercicio3.Checked)
            {
                txtIGDPBFExercicio3.Text = "0,00";
            }
            txtIGDPBFExercicio3.Enabled = chkIGDPBFExercicio3.Checked;
        }
        protected void chkIGDSUASExercicio3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkIGDSUASExercicio3.Checked)
            {
                txtIGDSUASExercicio3.Text = "0,00";
            }
            txtIGDSUASExercicio3.Enabled = chkIGDSUASExercicio3.Checked;
        }
        #endregion

        #region Actions: Exercicio4
        protected void chkFMASExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFMASExercicio4.Checked)
            {
                txtFMASExercicio4.Text = "0,00";
            }
            txtFMASExercicio4.Enabled = chkFMASExercicio4.Checked;
        }
        protected void chkFEASExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFEASExercicio4.Checked)
            {
                txtFEASExercicio4.Text = "0,00";
            }
            txtFEASExercicio4.Enabled = chkFEASExercicio4.Checked;

        }
        protected void chkFNASExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFNASExercicio4.Checked)
            {
                txtFNASExercicio4.Text = "0,00";
            }
            txtFNASExercicio4.Enabled = chkFNASExercicio4.Checked;
        }

        protected void chkOrcamentoMunicipalExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoMunicipalExercicio4.Checked)
            {
                txtOrcamentoMunicipalExercicio4.Text = "0,00";
            }

            txtOrcamentoMunicipalExercicio4.Enabled = chkOrcamentoMunicipalExercicio4.Checked;
        }
        protected void chkOutrosFundosMunicipaisExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosMunicipaisExercicio4.Checked)
            {
                txtOutrosFundosMunicipaisExercicio4.Text = "0,00";
            }

            txtOutrosFundosMunicipaisExercicio4.Enabled = chkOutrosFundosMunicipaisExercicio4.Checked;
        }
        protected void chkOrcamentoEstadualExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoEstadualExercicio4.Checked)
            {
                txtOrcamentoEstadualExercicio4.Text = "0,00";
            }
            txtOrcamentoEstadualExercicio4.Enabled = chkOrcamentoEstadualExercicio4.Checked;
        }
        protected void chkOutrosFundosEstaduaisExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosEstaduaisExercicio4.Checked)
            {
                txtOutrosFundosEstaduaisExercicio4.Text = "0,00";
            }
            txtOutrosFundosEstaduaisExercicio4.Enabled = chkOutrosFundosEstaduaisExercicio4.Checked;
        }
        protected void chkOrcamentoFederalExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOrcamentoFederalExercicio4.Checked)
            {
                txtOrcamentoFederalExercicio4.Text = "0,00";
            }
            txtOrcamentoFederalExercicio4.Enabled = chkOrcamentoFederalExercicio4.Checked;
        }
        protected void chkOutrosFundosFederaisExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOutrosFundosFederaisExercicio4.Checked)
            {
                txtOutrosFundosFederaisExercicio4.Text = "0,00";
            }
            txtOutrosFundosFederaisExercicio4.Enabled = chkOutrosFundosFederaisExercicio4.Checked;
        }
        protected void chkIGDPAIFExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkIGDPBFExercicio4.Checked)
            {
                txtIGDPBFExercicio4.Text = "0,00";
            }
            txtIGDPBFExercicio4.Enabled = chkIGDPBFExercicio4.Checked;
        }
        protected void chkIGDSUASExercicio4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkIGDSUASExercicio4.Checked)
            {
                txtIGDSUASExercicio4.Text = "0,00";
            }
            txtIGDSUASExercicio4.Enabled = chkIGDSUASExercicio4.Checked;
        }
        #endregion
        #endregion

        private void ExibirEstruturaProgramasMunicipais(int exercicio)
        {
            trNomePrograma.Visible = true;
            txtNome.Visible = true;

            txtObjetivo.Visible = true;
            trBeneficiarios.Visible = true;
            ddlBeneficiarios.Visible = true;

            trTipoPrograma.Visible = true;
            trAtividadesRealizadas.Visible = true;
            trAbrangencia.Visible = true;
            trParcerias.Visible = true;
            trRecursosFinanceiros.Visible = true;
            trRecursosFinanceirosAbas.Visible = true;

            #region Recursos Federal
            if (exercicio == Exercicios[0])
                trRecursosFinanceirosFederalExercicio1.Visible = true;
            if (exercicio == Exercicios[1])
                trRecursosFinanceirosFederalExercicio2.Visible = true;
            if (exercicio == Exercicios[2])
                trRecursosFinanceirosFederalExercicio3.Visible = true;
            if (exercicio == Exercicios[3])
                trRecursosFinanceirosFederalExercicio4.Visible = true;
            #endregion

            #region Recursos Estadual
            if (exercicio == Exercicios[0])
                trRecursosFinanceirosEstadualExercicio1.Visible = true;
            if (exercicio == Exercicios[1])
                trRecursosFinanceirosEstadualExercicio2.Visible = true;
            if (exercicio == Exercicios[2])
                trRecursosFinanceirosEstadualExercicio3.Visible = true;
            if (exercicio == Exercicios[3])
                trRecursosFinanceirosEstadualExercicio4.Visible = true;
            #endregion

            #region Recursos Municipal
            if (exercicio == Exercicios[0])
                trRecursosFinanceirosMunicipalExercicio1.Visible = true;
            if (exercicio == Exercicios[1])
                trRecursosFinanceirosMunicipalExercicio2.Visible = true;
            if (exercicio == Exercicios[2])
                trRecursosFinanceirosMunicipalExercicio3.Visible = true;
            if (exercicio == Exercicios[3])
                trRecursosFinanceirosMunicipalExercicio4.Visible = true;
            #endregion
        }

        #region Validacao [TODO:Implementando]
        public void GrupoASerValidado(bool infanciaSuas, bool amigoIdoso)
        {
            this.GrupoASerValidadoProgramaProjetoEstrutura = new ValidacaoProgramaProjetoEstrutura();
        }
        #endregion

        #region helper
        private void CarregarExercicios()
        {
            this.btnExercicio1.Text = Exercicios[0].ToString();
            this.btnExercicio2.Text = Exercicios[1].ToString();
            this.btnExercicio3.Text = Exercicios[2].ToString();
            this.btnExercicio4.Text = Exercicios[3].ToString();

            this.SelecionarCorAba();
        }
        private void SelecionarCorAba()
        {
            if (Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";

            }

            if (Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-info-seds";
            }

        }
        private void AdicionarEventos()
        {
            #region Recursos Financeiros: [Exercicio 1]
            txtFMASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoMunicipalExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoEstadualExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoFederalExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosMunicipaisExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosEstaduaisExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosFederaisExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDSUASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDPBFExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            #endregion

            #region Recursos Financeiros: [Exercicio 2]
            txtFMASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoMunicipalExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoEstadualExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoFederalExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosMunicipaisExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosEstaduaisExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosFederaisExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDSUASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDPBFExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            #endregion

            #region Recursos Financeiros: [Exercicio 3]
            txtFMASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoMunicipalExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoEstadualExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoFederalExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosMunicipaisExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosEstaduaisExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosFederaisExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDSUASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDPBFExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            #endregion

            #region Recursos Financeiros: [Exercicio 4]
            txtFMASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoMunicipalExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoEstadualExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoFederalExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosMunicipaisExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosEstaduaisExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosFederaisExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDSUASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDPBFExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            #endregion

            //Familia Paulista
            txtValorFEASPrimeiraParcela.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtNumeroBeneficiariosTerritorio.Attributes.Add("onkeyup", "formatNumber(this)");
            txtNumeroBeneficiariosTerritorio.Attributes.Add("onchange", "formatNumber(this)");

            txtPrevisaoAnualExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtPrevisaoAnualExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtPrevisaoAnualExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtPrevisaoAnualExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtValorProgramadoAnoAtual.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorProgramadoProximoAno.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtPrevisaoAnualExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtPrevisaoAnualExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtPrevisaoAnualExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtPrevisaoAnualExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRecursosFNASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRecursosFNASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRecursosFNASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRecursosFNASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");


        }
        #endregion

        /// <summary>
        /// acessuas: idQuadro = 80;
        /// idoso : idQuadro = 81;
        /// </summary>
        private void VerificarAlteracoes(Int32 idProgramaProjeto, int idQuadro = 41)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadroProgramaProjeto.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, idQuadro, idProgramaProjeto);
                    linkAlteracoesQuadroProgramaProjeto.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idQuadro.ToString())) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idProgramaProjeto.ToString()));
                }
            }
        }
        #region Helper [Recursos Financeiros]
        private void ExibicaoRecursosPorProgramaEExercicio(ProgramaProjetoInfo programaProjetoInfo, int exercicio)
        {


            trRecursosFinanceiros.Visible = true;
            trRecursosFinanceirosAbas.Visible = true;

            trRecursosFinanceirosExercicio1.Visible = (exercicio == Exercicios[0]);
            trRecursosFinanceirosExercicio2.Visible = (exercicio == Exercicios[1]);
            trRecursosFinanceirosExercicio3.Visible = (exercicio == Exercicios[2]);
            trRecursosFinanceirosExercicio4.Visible = (exercicio == Exercicios[3]);

            trRecursosFinanceirosFederalExercicio1.Visible = (exercicio == Exercicios[0]);
            trRecursosFinanceirosFederalExercicio2.Visible = (exercicio == Exercicios[1]);
            trRecursosFinanceirosFederalExercicio3.Visible = (exercicio == Exercicios[2]);
            trRecursosFinanceirosFederalExercicio4.Visible = (exercicio == Exercicios[3]);

            trRecursosFinanceirosEstadualExercicio1.Visible = (exercicio == Exercicios[0]);
            trRecursosFinanceirosEstadualExercicio2.Visible = (exercicio == Exercicios[1]);
            trRecursosFinanceirosEstadualExercicio3.Visible = (exercicio == Exercicios[2]);
            trRecursosFinanceirosEstadualExercicio4.Visible = (exercicio == Exercicios[3]);

            trRecursosFinanceirosMunicipalExercicio1.Visible = (exercicio == Exercicios[0]);
            trRecursosFinanceirosMunicipalExercicio2.Visible = (exercicio == Exercicios[1]);
            trRecursosFinanceirosMunicipalExercicio3.Visible = (exercicio == Exercicios[2]);
            trRecursosFinanceirosMunicipalExercicio4.Visible = (exercicio == Exercicios[3]);

        }
        private void CarregarRecursosPorProgramaEExercicio(ProgramaProjetoInfo programaProjetoInfo, ProgramaProjetoRecursoFinanceiroInfo ppRecursoFinanceiro, int exercicio)
        {
            #region Flags Definicao Programa

            #region Servico Recurso Financeiro
            if (ppRecursoFinanceiro != null)
            {
                this.CarregarRecursoFederal(ppRecursoFinanceiro, exercicio);
                this.CarregarRecursoEstadual(ppRecursoFinanceiro, exercicio);
                this.CarregarRecursoMunicipal(ppRecursoFinanceiro, exercicio);
            }
            #endregion
        }
        private void CarregarRecursoFederal(ProgramaProjetoRecursoFinanceiroInfo ppRecursoFinanceiro, int exercicio)
        {
            if (ppRecursoFinanceiro != null)
            {
                if (exercicio == Exercicios[0])
                {
                    if (ppRecursoFinanceiro != null)
                    {
                        //CheckBoxs
                        chkFNASExercicio1.Checked = (ppRecursoFinanceiro.FonteFNAS != null) ? (ppRecursoFinanceiro.FonteFNAS.HasValue ? ppRecursoFinanceiro.FonteFNAS.Value : false) : false;
                        chkOrcamentoFederalExercicio1.Checked = (ppRecursoFinanceiro.FonteOrcamentoFederal != null) ? (ppRecursoFinanceiro.FonteOrcamentoFederal.HasValue ? ppRecursoFinanceiro.FonteOrcamentoFederal.Value : false) : false; ;
                        chkOutrosFundosFederaisExercicio1.Checked = (ppRecursoFinanceiro.FonteFundoFederal != null) ? (ppRecursoFinanceiro.FonteFundoFederal.HasValue ? ppRecursoFinanceiro.FonteFundoFederal.Value : false) : false; ;
                        //indices
                        chkIGDSUASExercicio1.Checked = (ppRecursoFinanceiro.FonteIGDSUAS != null) ? (ppRecursoFinanceiro.FonteIGDSUAS.HasValue ? ppRecursoFinanceiro.FonteIGDSUAS.Value : false) : false; ;
                        chkIGDPBFExercicio1.Checked = (ppRecursoFinanceiro.FonteIGDPBF != null) ? (ppRecursoFinanceiro.FonteIGDPBF.HasValue ? ppRecursoFinanceiro.FonteIGDPBF.Value : false) : false; ;


                        //Valores
                        txtFNASExercicio1.Text = ppRecursoFinanceiro.ValorFNAS != null ? ppRecursoFinanceiro.ValorFNAS.Value.ToString("N2") : string.Empty;
                        txtOrcamentoFederalExercicio1.Text = ppRecursoFinanceiro.ValorOrcamentoFederal != null ? ppRecursoFinanceiro.ValorOrcamentoFederal.Value.ToString("N2") : string.Empty;
                        txtOutrosFundosFederaisExercicio1.Text = ppRecursoFinanceiro.ValorFundoFederal != null ? ppRecursoFinanceiro.ValorFundoFederal.Value.ToString("N2") : string.Empty;
                        //indices
                        txtIGDSUASExercicio1.Text = ppRecursoFinanceiro.ValorIGDSUAS != null ? ppRecursoFinanceiro.ValorIGDSUAS.Value.ToString("N2") : string.Empty;
                        txtIGDPBFExercicio1.Text = ppRecursoFinanceiro.ValorIGDPBF != null ? ppRecursoFinanceiro.ValorIGDPBF.Value.ToString("N2") : string.Empty;
                    }
                }
                if (exercicio == Exercicios[1])
                {
                    if (ppRecursoFinanceiro != null)
                    {
                        //CheckBoxs
                        chkFNASExercicio2.Checked = (ppRecursoFinanceiro.FonteFNAS != null) ? (ppRecursoFinanceiro.FonteFNAS.HasValue ? ppRecursoFinanceiro.FonteFNAS.Value : false) : false;
                        chkOutrosFundosFederaisExercicio2.Checked = (ppRecursoFinanceiro.FonteFundoFederal != null) ? (ppRecursoFinanceiro.FonteFundoFederal.HasValue ? ppRecursoFinanceiro.FonteFundoFederal.Value : false) : false; ;
                        chkOrcamentoFederalExercicio2.Checked = (ppRecursoFinanceiro.FonteOrcamentoFederal != null) ? (ppRecursoFinanceiro.FonteOrcamentoFederal.HasValue ? ppRecursoFinanceiro.FonteOrcamentoFederal.Value : false) : false; ;
                        //indices
                        chkIGDSUASExercicio2.Checked = (ppRecursoFinanceiro.FonteIGDSUAS != null) ? (ppRecursoFinanceiro.FonteIGDSUAS.HasValue ? ppRecursoFinanceiro.FonteIGDSUAS.Value : false) : false; ;
                        chkIGDPBFExercicio2.Checked = (ppRecursoFinanceiro.FonteIGDPBF != null) ? (ppRecursoFinanceiro.FonteIGDPBF.HasValue ? ppRecursoFinanceiro.FonteIGDPBF.Value : false) : false; ;

                        //Valores
                        txtFNASExercicio2.Text = ppRecursoFinanceiro.ValorFNAS != null ? ppRecursoFinanceiro.ValorFNAS.Value.ToString("N2") : string.Empty;
                        txtOrcamentoFederalExercicio2.Text = ppRecursoFinanceiro.ValorOrcamentoFederal != null ? ppRecursoFinanceiro.ValorOrcamentoFederal.Value.ToString("N2") : string.Empty;
                        txtOutrosFundosFederaisExercicio2.Text = ppRecursoFinanceiro.ValorFundoFederal != null ? ppRecursoFinanceiro.ValorFundoFederal.Value.ToString("N2") : string.Empty;
                        //indices
                        txtIGDSUASExercicio2.Text = ppRecursoFinanceiro.ValorIGDSUAS != null ? ppRecursoFinanceiro.ValorIGDSUAS.Value.ToString("N2") : string.Empty;
                        txtIGDPBFExercicio2.Text = ppRecursoFinanceiro.ValorIGDPBF != null ? ppRecursoFinanceiro.ValorIGDPBF.Value.ToString("N2") : string.Empty;
                    }
                }

                if (exercicio == Exercicios[2])
                {
                    if (ppRecursoFinanceiro != null)
                    {
                        //CheckBoxs
                        chkFNASExercicio3.Checked = (ppRecursoFinanceiro.FonteFNAS != null) ? (ppRecursoFinanceiro.FonteFNAS.HasValue ? ppRecursoFinanceiro.FonteFNAS.Value : false) : false;
                        chkOutrosFundosFederaisExercicio3.Checked = (ppRecursoFinanceiro.FonteFundoFederal != null) ? (ppRecursoFinanceiro.FonteFundoFederal.HasValue ? ppRecursoFinanceiro.FonteFundoFederal.Value : false) : false; ;
                        chkOrcamentoFederalExercicio3.Checked = (ppRecursoFinanceiro.FonteOrcamentoFederal != null) ? (ppRecursoFinanceiro.FonteOrcamentoFederal.HasValue ? ppRecursoFinanceiro.FonteOrcamentoFederal.Value : false) : false; ;
                        chkIGDSUASExercicio3.Checked = (ppRecursoFinanceiro.FonteIGDSUAS != null) ? (ppRecursoFinanceiro.FonteIGDSUAS.HasValue ? ppRecursoFinanceiro.FonteIGDSUAS.Value : false) : false; ;
                        chkIGDPBFExercicio3.Checked = (ppRecursoFinanceiro.FonteIGDPBF != null) ? (ppRecursoFinanceiro.FonteIGDPBF.HasValue ? ppRecursoFinanceiro.FonteIGDPBF.Value : false) : false; ;
                        //Valores
                        txtFNASExercicio3.Text = ppRecursoFinanceiro.ValorFNAS != null ? ppRecursoFinanceiro.ValorFNAS.Value.ToString("N2") : string.Empty;
                        txtOrcamentoFederalExercicio3.Text = ppRecursoFinanceiro.ValorOrcamentoFederal != null ? ppRecursoFinanceiro.ValorOrcamentoFederal.Value.ToString("N2") : string.Empty;
                        txtOutrosFundosFederaisExercicio3.Text = ppRecursoFinanceiro.ValorFundoFederal != null ? ppRecursoFinanceiro.ValorFundoFederal.Value.ToString("N2") : string.Empty;
                        txtIGDSUASExercicio3.Text = ppRecursoFinanceiro.ValorIGDSUAS != null ? ppRecursoFinanceiro.ValorIGDSUAS.Value.ToString("N2") : string.Empty;
                        txtIGDPBFExercicio3.Text = ppRecursoFinanceiro.ValorIGDPBF != null ? ppRecursoFinanceiro.ValorIGDPBF.Value.ToString("N2") : string.Empty;
                    }
                }

                if (exercicio == Exercicios[3])
                {
                    if (ppRecursoFinanceiro != null)
                    {
                        //CheckBoxs
                        chkFNASExercicio4.Checked = (ppRecursoFinanceiro.FonteFNAS != null) ? (ppRecursoFinanceiro.FonteFNAS.HasValue ? ppRecursoFinanceiro.FonteFNAS.Value : false) : false;
                        chkOutrosFundosFederaisExercicio4.Checked = (ppRecursoFinanceiro.FonteFundoFederal != null) ? (ppRecursoFinanceiro.FonteFundoFederal.HasValue ? ppRecursoFinanceiro.FonteFundoFederal.Value : false) : false; ;
                        chkOrcamentoFederalExercicio4.Checked = (ppRecursoFinanceiro.FonteOrcamentoFederal != null) ? (ppRecursoFinanceiro.FonteOrcamentoFederal.HasValue ? ppRecursoFinanceiro.FonteOrcamentoFederal.Value : false) : false; ;
                        chkIGDSUASExercicio4.Checked = (ppRecursoFinanceiro.FonteIGDSUAS != null) ? (ppRecursoFinanceiro.FonteIGDSUAS.HasValue ? ppRecursoFinanceiro.FonteIGDSUAS.Value : false) : false; ;
                        chkIGDPBFExercicio4.Checked = (ppRecursoFinanceiro.FonteIGDPBF != null) ? (ppRecursoFinanceiro.FonteIGDPBF.HasValue ? ppRecursoFinanceiro.FonteIGDPBF.Value : false) : false; ;
                        //Valores
                        txtFNASExercicio4.Text = ppRecursoFinanceiro.ValorFNAS != null ? ppRecursoFinanceiro.ValorFNAS.Value.ToString("N2") : string.Empty;
                        txtOrcamentoFederalExercicio4.Text = ppRecursoFinanceiro.ValorOrcamentoFederal != null ? ppRecursoFinanceiro.ValorOrcamentoFederal.Value.ToString("N2") : string.Empty;
                        txtOutrosFundosFederaisExercicio4.Text = ppRecursoFinanceiro.ValorFundoFederal != null ? ppRecursoFinanceiro.ValorFundoFederal.Value.ToString("N2") : string.Empty;
                        txtIGDSUASExercicio4.Text = ppRecursoFinanceiro.ValorIGDSUAS != null ? ppRecursoFinanceiro.ValorIGDSUAS.Value.ToString("N2") : string.Empty;
                        txtIGDPBFExercicio4.Text = ppRecursoFinanceiro.ValorIGDPBF != null ? ppRecursoFinanceiro.ValorIGDPBF.Value.ToString("N2") : string.Empty;
                    }
                }
            }
        }



        private void CarregarRecursoEstadual(ProgramaProjetoRecursoFinanceiroInfo ppRecursoFinanceiro, int exercicio)
        {
            if (ppRecursoFinanceiro != null)
            {
                if (exercicio == Exercicios[0])
                {
                    //CheckBoxs
                    chkFEASExercicio1.Checked = ppRecursoFinanceiro.FonteFEAS.HasValue ? ppRecursoFinanceiro.FonteFEAS.Value : false;
                    chkOrcamentoEstadualExercicio1.Checked = ppRecursoFinanceiro.FonteOrcamentoEstadual.HasValue ? ppRecursoFinanceiro.FonteOrcamentoEstadual.Value : false;
                    chkOutrosFundosEstaduaisExercicio1.Checked = ppRecursoFinanceiro.FonteFundoEstadual.HasValue ? ppRecursoFinanceiro.FonteFundoEstadual.Value : false;
                    //Valores
                    txtFEASExercicio1.Text = ppRecursoFinanceiro.ValorFEAS.Value.ToString("N2");
                    txtOrcamentoEstadualExercicio1.Text = ppRecursoFinanceiro.ValorOrcamentoEstadual.Value.ToString("N2");
                    txtOutrosFundosEstaduaisExercicio1.Text = ppRecursoFinanceiro.ValorFundoEstadual.Value.ToString("N2");

                }
                if (exercicio == Exercicios[1])
                {
                    //CheckBoxs
                    chkFEASExercicio2.Checked = ppRecursoFinanceiro.FonteFEAS.HasValue ? ppRecursoFinanceiro.FonteFEAS.Value : false;
                    chkOrcamentoEstadualExercicio2.Checked = ppRecursoFinanceiro.FonteOrcamentoEstadual.HasValue ? ppRecursoFinanceiro.FonteOrcamentoEstadual.Value : false;
                    chkOutrosFundosEstaduaisExercicio2.Checked = ppRecursoFinanceiro.FonteFundoEstadual.HasValue ? ppRecursoFinanceiro.FonteFundoEstadual.Value : false;
                    //Valores
                    txtFEASExercicio2.Text = ppRecursoFinanceiro.ValorFEAS.Value.ToString("N2");
                    txtOrcamentoEstadualExercicio2.Text = ppRecursoFinanceiro.ValorOrcamentoEstadual.Value.ToString("N2");
                    txtOutrosFundosEstaduaisExercicio2.Text = ppRecursoFinanceiro.ValorFundoEstadual.Value.ToString("N2");
                }
                if (exercicio == Exercicios[2])
                {
                    chkFEASExercicio3.Checked = ppRecursoFinanceiro.FonteFEAS.HasValue ? ppRecursoFinanceiro.FonteFEAS.Value : false;
                    chkOrcamentoEstadualExercicio3.Checked = ppRecursoFinanceiro.FonteOrcamentoEstadual.HasValue ? ppRecursoFinanceiro.FonteOrcamentoEstadual.Value : false;
                    chkOutrosFundosEstaduaisExercicio3.Checked = ppRecursoFinanceiro.FonteFundoEstadual.HasValue ? ppRecursoFinanceiro.FonteFundoEstadual.Value : false;
                    //Valores
                    txtFEASExercicio3.Text = ppRecursoFinanceiro.ValorFEAS.Value.ToString("N2");
                    txtOrcamentoEstadualExercicio3.Text = ppRecursoFinanceiro.ValorOrcamentoEstadual.Value.ToString("N2");
                    txtOutrosFundosEstaduaisExercicio3.Text = ppRecursoFinanceiro.ValorFundoEstadual.Value.ToString("N2");
                }
                if (exercicio == Exercicios[3])
                {
                    chkFEASExercicio4.Checked = ppRecursoFinanceiro.FonteFEAS.HasValue ? ppRecursoFinanceiro.FonteFEAS.Value : false;
                    chkOrcamentoEstadualExercicio4.Checked = ppRecursoFinanceiro.FonteOrcamentoEstadual.HasValue ? ppRecursoFinanceiro.FonteOrcamentoEstadual.Value : false;
                    chkOutrosFundosEstaduaisExercicio4.Checked = ppRecursoFinanceiro.FonteFundoEstadual.HasValue ? ppRecursoFinanceiro.FonteFundoEstadual.Value : false;
                    //Valores
                    txtFEASExercicio4.Text = ppRecursoFinanceiro.ValorFEAS.Value.ToString("N2");
                    txtOrcamentoEstadualExercicio4.Text = ppRecursoFinanceiro.ValorOrcamentoEstadual.Value.ToString("N2");
                    txtOutrosFundosEstaduaisExercicio4.Text = ppRecursoFinanceiro.ValorFundoEstadual.Value.ToString("N2");
                }
            }
        }

        private void CarregarRecursoMunicipal(ProgramaProjetoRecursoFinanceiroInfo ppRecursoFinanceiro, int exercicio)
        {
            if (ppRecursoFinanceiro != null)
            {
                if (exercicio == Exercicios[0])
                {
                    //CheckBoxs
                    chkFMASExercicio1.Checked = ppRecursoFinanceiro.FonteFMAS != null ? (ppRecursoFinanceiro.FonteFMAS.HasValue ? ppRecursoFinanceiro.FonteFMAS.Value : false) : false;
                    chkOrcamentoMunicipalExercicio1.Checked = ppRecursoFinanceiro.FonteOrcamentoMunicipal != null ? (ppRecursoFinanceiro.FonteOrcamentoMunicipal.HasValue ? ppRecursoFinanceiro.FonteOrcamentoMunicipal.Value : false) : false;
                    chkOutrosFundosMunicipaisExercicio1.Checked = ppRecursoFinanceiro.FonteFundoMunicipal != null ? (ppRecursoFinanceiro.FonteFundoMunicipal.HasValue ? ppRecursoFinanceiro.FonteFundoMunicipal.Value : false) : false;

                    //Valores
                    txtFMASExercicio1.Text = ppRecursoFinanceiro.ValorFMAS != null ? ppRecursoFinanceiro.ValorFMAS.Value.ToString("N2") : string.Empty;
                    txtOrcamentoMunicipalExercicio1.Text = ppRecursoFinanceiro.ValorOrcamentoMunicipal != null ? ppRecursoFinanceiro.ValorOrcamentoMunicipal.Value.ToString("N2") : string.Empty;
                    txtOutrosFundosMunicipaisExercicio1.Text = ppRecursoFinanceiro.ValorFundoMunicipal != null ? ppRecursoFinanceiro.ValorFundoMunicipal.Value.ToString("N2") : string.Empty;

                }
                if (exercicio == Exercicios[1])
                {
                    //CheckBoxs
                    chkFMASExercicio2.Checked = ppRecursoFinanceiro.FonteFMAS != null ? ppRecursoFinanceiro.FonteFMAS.HasValue : false;
                    chkOrcamentoMunicipalExercicio2.Checked = ppRecursoFinanceiro.FonteOrcamentoMunicipal != null ? ppRecursoFinanceiro.FonteOrcamentoMunicipal.HasValue ? ppRecursoFinanceiro.FonteOrcamentoMunicipal.Value : false : false;
                    chkOutrosFundosMunicipaisExercicio2.Checked = ppRecursoFinanceiro.FonteFundoMunicipal != null ? ppRecursoFinanceiro.FonteFundoMunicipal.HasValue ? ppRecursoFinanceiro.FonteFundoMunicipal.Value : false : false;

                    //Valores
                    txtFMASExercicio2.Text = ppRecursoFinanceiro.ValorFMAS != null ? ppRecursoFinanceiro.ValorFMAS.Value.ToString("N2") : string.Empty;
                    txtOrcamentoMunicipalExercicio2.Text = ppRecursoFinanceiro.ValorOrcamentoMunicipal != null ? ppRecursoFinanceiro.ValorOrcamentoMunicipal.Value.ToString("N2") : string.Empty;
                    txtOutrosFundosMunicipaisExercicio2.Text = ppRecursoFinanceiro.ValorFundoMunicipal != null ? ppRecursoFinanceiro.ValorFundoMunicipal.Value.ToString("N2") : string.Empty;
                }
                if (exercicio == Exercicios[2])
                {
                    //CheckBoxs
                    chkFMASExercicio3.Checked = ppRecursoFinanceiro.FonteFMAS != null ? ppRecursoFinanceiro.FonteFMAS.HasValue ? ppRecursoFinanceiro.FonteFMAS.Value : false : false;
                    chkOrcamentoMunicipalExercicio3.Checked = ppRecursoFinanceiro.FonteOrcamentoMunicipal != null ? ppRecursoFinanceiro.FonteOrcamentoMunicipal.HasValue ? ppRecursoFinanceiro.FonteOrcamentoMunicipal.Value : false : false;
                    chkOutrosFundosMunicipaisExercicio3.Checked = ppRecursoFinanceiro.FonteFundoMunicipal != null ? ppRecursoFinanceiro.FonteFundoMunicipal.HasValue ? ppRecursoFinanceiro.FonteFundoMunicipal.Value : false : false;

                    //Valores
                    txtFMASExercicio3.Text = ppRecursoFinanceiro.ValorFMAS != null ? ppRecursoFinanceiro.ValorFMAS.Value.ToString("N2") : string.Empty;
                    txtOrcamentoMunicipalExercicio3.Text = ppRecursoFinanceiro.ValorOrcamentoMunicipal != null ? ppRecursoFinanceiro.ValorOrcamentoMunicipal.Value.ToString("N2") : string.Empty;
                    txtOutrosFundosMunicipaisExercicio3.Text = ppRecursoFinanceiro.ValorFundoMunicipal != null ? ppRecursoFinanceiro.ValorFundoMunicipal.Value.ToString("N2") : string.Empty;
                }
                if (exercicio == Exercicios[3])
                {
                    //CheckBoxs
                    chkFMASExercicio4.Checked = ppRecursoFinanceiro.FonteFMAS != null ? ppRecursoFinanceiro.FonteFMAS.HasValue ? ppRecursoFinanceiro.FonteFMAS.Value : false : false;
                    chkOrcamentoMunicipalExercicio4.Checked = ppRecursoFinanceiro.FonteOrcamentoMunicipal != null ? ppRecursoFinanceiro.FonteOrcamentoMunicipal.HasValue ? ppRecursoFinanceiro.FonteOrcamentoMunicipal.Value : false : false;
                    chkOutrosFundosMunicipaisExercicio4.Checked = ppRecursoFinanceiro.FonteFundoMunicipal != null ? ppRecursoFinanceiro.FonteFundoMunicipal.HasValue ? ppRecursoFinanceiro.FonteFundoMunicipal.Value : false : false;

                    //Valores
                    txtFMASExercicio4.Text = ppRecursoFinanceiro.ValorFMAS != null ? ppRecursoFinanceiro.ValorFMAS.Value.ToString("N2") : string.Empty;
                    txtOrcamentoMunicipalExercicio4.Text = ppRecursoFinanceiro.ValorOrcamentoMunicipal != null ? ppRecursoFinanceiro.ValorOrcamentoMunicipal.Value.ToString("N2") : string.Empty;
                    txtOutrosFundosMunicipaisExercicio4.Text = ppRecursoFinanceiro.ValorFundoMunicipal != null ? ppRecursoFinanceiro.ValorFundoMunicipal.Value.ToString("N2") : string.Empty;
                }
            }
        }
            #endregion

        private void HelperEsconderTodosAsEstruturas()
        {
            trTipoPrograma.Visible = false;
            //trExecutaPrograma.Visible = false;
            trDataProgramaProjeto.Visible = false;
            //trDataAdesao.Visible = false;
            trPrevisaoAnual.Visible = false;
            trMetaPactuada.Visible = false;
            trCaracterizacaoUsuarios.Visible = false;
            //trAcoesDesenvolvidas.Visible = false;
            trAtividadesRealizadas.Visible = false;
            trAbrangencia.Visible = false;
            trParcerias.Visible = false;
            trRecursosFinanceiros.Visible = false;
            trRecursosFinanceirosAbas.Visible = false;

            trRecursosFinanceirosFederalExercicio1.Visible = false;
            trRecursosFinanceirosFederalExercicio2.Visible = false;
            trRecursosFinanceirosFederalExercicio3.Visible = false;
            trRecursosFinanceirosFederalExercicio4.Visible = false;

            trRecursosFinanceirosEstadualExercicio1.Visible = false;
            trRecursosFinanceirosEstadualExercicio2.Visible = false;
            trRecursosFinanceirosEstadualExercicio3.Visible = false;
            trRecursosFinanceirosEstadualExercicio4.Visible = false;

            trRecursosFinanceirosMunicipalExercicio1.Visible = false;
            trRecursosFinanceirosMunicipalExercicio2.Visible = false;
            trRecursosFinanceirosMunicipalExercicio3.Visible = false;
            trRecursosFinanceirosMunicipalExercicio4.Visible = false;

        }
        private void HelperLimparTodasAsEstruturasERecursos()
        {
            this.ClearValoresEstruturaTipoPrograma();
            //this.ClearValoresEstruturaExecutaPrograma();
            this.ClearValoresEstruturaDataProgramaProjeto();
            //this.ClearValoresEstruturaDataAdesao();
            this.ClearValoresEstruturaPrevisaoAnual();
            this.ClearValoresEstruturaMetaPactuada();
            this.ClearValoresEstruturaCaracterizacaoUsuarios();
            //this.ClearValoresEstruturaAcoesDesenvolvidas();
            this.ClearValoresEstruturaAtividadesRealizadas();
            this.ClearValoresEstruturaAbrangencia();
            this.ClearValoresEstruturaParcerias();
        }
        private void HelperLimparValoresRecursosFinanceiros()
        {
            #region Exercicio 1
            chkFMASExercicio1.Checked = false;
            txtFMASExercicio1.Text = "0,00";

            txtFMASExercicio1.Enabled = false;

            chkOrcamentoMunicipalExercicio1.Checked = false;
            txtOrcamentoMunicipalExercicio1.Text = "0,00";
            txtOrcamentoMunicipalExercicio1.Enabled = false;

            chkOutrosFundosMunicipaisExercicio1.Checked = false;
            txtOutrosFundosMunicipaisExercicio1.Text = "0,00";
            txtOutrosFundosMunicipaisExercicio1.Enabled = false;


            chkFEASExercicio1.Checked = false;
            txtFEASExercicio1.Text = "0,00";
            txtFEASExercicio1.Enabled = false;

            chkOrcamentoEstadualExercicio1.Checked = false;
            txtOrcamentoEstadualExercicio1.Text = "0,00";
            txtOrcamentoEstadualExercicio1.Enabled = false;

            chkOutrosFundosEstaduaisExercicio1.Checked = false;
            txtOutrosFundosEstaduaisExercicio1.Text = "0,00";
            txtOutrosFundosEstaduaisExercicio1.Enabled = false;

            chkFNASExercicio1.Checked = false;
            txtFNASExercicio1.Text = "0,00";
            txtFNASExercicio1.Enabled = false;

            chkOrcamentoFederalExercicio1.Checked = false;
            txtOrcamentoFederalExercicio1.Text = "0,00";
            txtOrcamentoFederalExercicio1.Enabled = false;

            chkOutrosFundosFederaisExercicio1.Checked = false;
            txtOutrosFundosFederaisExercicio1.Text = "0,00";
            txtOutrosFundosFederaisExercicio1.Enabled = false;

            chkIGDPBFExercicio1.Checked = false;
            txtIGDPBFExercicio1.Text = "0,00";
            txtIGDPBFExercicio1.Enabled = false;


            chkIGDSUASExercicio1.Checked = false;
            txtIGDSUASExercicio1.Text = "0,00";
            txtIGDSUASExercicio1.Enabled = false;
            #endregion

            #region Exercicio 2
            chkFMASExercicio2.Checked = false;
            txtFMASExercicio2.Text = "0,00";

            txtFMASExercicio2.Enabled = false;

            chkOrcamentoMunicipalExercicio2.Checked = false;
            txtOrcamentoMunicipalExercicio2.Text = "0,00";
            txtOrcamentoMunicipalExercicio2.Enabled = false;

            chkOutrosFundosMunicipaisExercicio2.Checked = false;
            txtOutrosFundosMunicipaisExercicio2.Text = "0,00";
            txtOutrosFundosMunicipaisExercicio2.Enabled = false;


            chkFEASExercicio2.Checked = false;
            txtFEASExercicio2.Text = "0,00";
            txtFEASExercicio2.Enabled = false;

            chkOrcamentoEstadualExercicio2.Checked = false;
            txtOrcamentoEstadualExercicio2.Text = "0,00";
            txtOrcamentoEstadualExercicio2.Enabled = false;

            chkOutrosFundosEstaduaisExercicio2.Checked = false;
            txtOutrosFundosEstaduaisExercicio2.Text = "0,00";
            txtOutrosFundosEstaduaisExercicio2.Enabled = false;

            chkFNASExercicio2.Checked = false;
            txtFNASExercicio2.Text = "0,00";
            txtFNASExercicio2.Enabled = false;

            chkOrcamentoFederalExercicio2.Checked = false;
            txtOrcamentoFederalExercicio2.Text = "0,00";
            txtOrcamentoFederalExercicio2.Enabled = false;

            chkOutrosFundosFederaisExercicio2.Checked = false;
            txtOutrosFundosFederaisExercicio2.Text = "0,00";
            txtOutrosFundosFederaisExercicio2.Enabled = false;

            chkIGDPBFExercicio2.Checked = false;
            txtIGDPBFExercicio2.Text = "0,00";
            txtIGDPBFExercicio2.Enabled = false;


            chkIGDSUASExercicio2.Checked = false;
            txtIGDSUASExercicio2.Text = "0,00";
            txtIGDSUASExercicio2.Enabled = false;
            #endregion

            #region Exercicio 3
            chkFMASExercicio3.Checked = false;
            txtFMASExercicio3.Text = "0,00";

            txtFMASExercicio3.Enabled = false;

            chkOrcamentoMunicipalExercicio3.Checked = false;
            txtOrcamentoMunicipalExercicio3.Text = "0,00";
            txtOrcamentoMunicipalExercicio3.Enabled = false;

            chkOutrosFundosMunicipaisExercicio3.Checked = false;
            txtOutrosFundosMunicipaisExercicio3.Text = "0,00";
            txtOutrosFundosMunicipaisExercicio3.Enabled = false;


            chkFEASExercicio3.Checked = false;
            txtFEASExercicio3.Text = "0,00";
            txtFEASExercicio3.Enabled = false;

            chkOrcamentoEstadualExercicio3.Checked = false;
            txtOrcamentoEstadualExercicio3.Text = "0,00";
            txtOrcamentoEstadualExercicio3.Enabled = false;

            chkOutrosFundosEstaduaisExercicio3.Checked = false;
            txtOutrosFundosEstaduaisExercicio3.Text = "0,00";
            txtOutrosFundosEstaduaisExercicio3.Enabled = false;

            chkFNASExercicio3.Checked = false;
            txtFNASExercicio3.Text = "0,00";
            txtFNASExercicio3.Enabled = false;

            chkOrcamentoFederalExercicio3.Checked = false;
            txtOrcamentoFederalExercicio3.Text = "0,00";
            txtOrcamentoFederalExercicio3.Enabled = false;

            chkOutrosFundosFederaisExercicio3.Checked = false;
            txtOutrosFundosFederaisExercicio3.Text = "0,00";
            txtOutrosFundosFederaisExercicio3.Enabled = false;

            chkIGDPBFExercicio3.Checked = false;
            txtIGDPBFExercicio3.Text = "0,00";
            txtIGDPBFExercicio3.Enabled = false;


            chkIGDSUASExercicio3.Checked = false;
            txtIGDSUASExercicio3.Text = "0,00";
            txtIGDSUASExercicio3.Enabled = false;
            #endregion

            #region Exercicio 4
            chkFMASExercicio4.Checked = false;
            txtFMASExercicio4.Text = "0,00";

            txtFMASExercicio4.Enabled = false;

            chkOrcamentoMunicipalExercicio4.Checked = false;
            txtOrcamentoMunicipalExercicio4.Text = "0,00";
            txtOrcamentoMunicipalExercicio4.Enabled = false;

            chkOutrosFundosMunicipaisExercicio4.Checked = false;
            txtOutrosFundosMunicipaisExercicio4.Text = "0,00";
            txtOutrosFundosMunicipaisExercicio4.Enabled = false;


            chkFEASExercicio4.Checked = false;
            txtFEASExercicio4.Text = "0,00";
            txtFEASExercicio4.Enabled = false;

            chkOrcamentoEstadualExercicio4.Checked = false;
            txtOrcamentoEstadualExercicio4.Text = "0,00";
            txtOrcamentoEstadualExercicio4.Enabled = false;

            chkOutrosFundosEstaduaisExercicio4.Checked = false;
            txtOutrosFundosEstaduaisExercicio4.Text = "0,00";
            txtOutrosFundosEstaduaisExercicio4.Enabled = false;

            chkFNASExercicio4.Checked = false;
            txtFNASExercicio4.Text = "0,00";
            txtFNASExercicio4.Enabled = false;

            chkOrcamentoFederalExercicio4.Checked = false;
            txtOrcamentoFederalExercicio4.Text = "0,00";
            txtOrcamentoFederalExercicio4.Enabled = false;

            chkOutrosFundosFederaisExercicio4.Checked = false;
            txtOutrosFundosFederaisExercicio4.Text = "0,00";
            txtOutrosFundosFederaisExercicio4.Enabled = false;

            chkIGDPBFExercicio4.Checked = false;
            txtIGDPBFExercicio4.Text = "0,00";
            txtIGDPBFExercicio4.Enabled = false;


            chkIGDSUASExercicio4.Checked = false;
            txtIGDSUASExercicio4.Text = "0,00";
            txtIGDSUASExercicio4.Enabled = false;
            #endregion
        }
        #endregion

        #region Bloqueio E Desbloqueio
        #region Controles
        private WebControl[] ObterControlesPrevisaoExercicio1()
        {
            WebControl[] controles = {
            txtPrevisaoAnualExercicio1
            ,txtRecursosFNASExercicio1
            };
            return controles;
        }
        private WebControl[] ObterControlesPrevisaoExercicio2()
        {
            WebControl[] controles = {
            txtPrevisaoAnualExercicio2
            ,txtRecursosFNASExercicio2
            };
            return controles;
        }
        private WebControl[] ObterControlesPrevisaoExercicio3()
        {
            WebControl[] controles = {
            txtPrevisaoAnualExercicio3
            ,txtRecursosFNASExercicio3
            };
            return controles;
        }
        private WebControl[] ObterControlesPrevisaoExercicio4()
        {
            WebControl[] controles = {
            txtPrevisaoAnualExercicio4
            ,txtRecursosFNASExercicio4
            };
            return controles;
        }

        private WebControl[] ObterControlesRecursosFinanceirosExercicio1()
        {
            WebControl[] controles = {
                            //CheckBoxs
                              chkFNASExercicio1
                            , chkOutrosFundosMunicipaisExercicio1
                            , chkOrcamentoFederalExercicio1
                            , chkIGDSUASExercicio1
                            , chkIGDPBFExercicio1
                            //Valores
                            , txtFNASExercicio1
                            , txtOrcamentoFederalExercicio1
                            , txtOutrosFundosFederaisExercicio1
                            , txtIGDSUASExercicio1
                            , txtIGDPBFExercicio1
                            //CheckBoxs
                            , chkFEASExercicio1
                            , chkOrcamentoEstadualExercicio1
                            , chkOutrosFundosMunicipaisExercicio1
                            //Valores
                            , txtFEASExercicio1
                            , txtOrcamentoEstadualExercicio1
                            , txtOutrosFundosMunicipaisExercicio1
                            //CheckBoxs
                            , chkFMASExercicio1
                            , chkOrcamentoMunicipalExercicio1
                            , chkOutrosFundosMunicipaisExercicio1
                            //Valores
                            , txtFMASExercicio1
                            , txtOrcamentoMunicipalExercicio1
                            , txtOutrosFundosMunicipaisExercicio1
                                    };

            return controles;
        }
        private WebControl[] ObterControlesRecursosFinanceirosExercicio2()
        {
            WebControl[] controles = {
                            //CheckBoxs
                              chkFNASExercicio2
                            , chkOutrosFundosMunicipaisExercicio2
                            , chkOrcamentoFederalExercicio2
                            , chkIGDSUASExercicio2
                            , chkIGDPBFExercicio2
                            //Valores
                            , txtFNASExercicio2
                            , txtOrcamentoFederalExercicio2
                            , txtOutrosFundosFederaisExercicio2
                            , txtIGDSUASExercicio2
                            , txtIGDPBFExercicio2
                            //CheckBoxs
                            , chkFEASExercicio2
                            , chkOrcamentoEstadualExercicio2
                            , chkOutrosFundosMunicipaisExercicio2
                            //Valores
                            , txtFEASExercicio2
                            , txtOrcamentoEstadualExercicio2
                            , txtOutrosFundosMunicipaisExercicio2
                            //CheckBoxs
                            , chkFMASExercicio2
                            , chkOrcamentoMunicipalExercicio2
                            , chkOutrosFundosMunicipaisExercicio2
                            //Valores
                            , txtFMASExercicio2
                            , txtOrcamentoMunicipalExercicio2
                            , txtOutrosFundosMunicipaisExercicio2
                                    };

            return controles;
        }
        private WebControl[] ObterControlesRecursosFinanceirosExercicio3()
        {
            WebControl[] controles = {
                            //CheckBoxs
                              chkFNASExercicio3
                            , chkOutrosFundosMunicipaisExercicio3
                            , chkOrcamentoFederalExercicio3
                            , chkIGDSUASExercicio3
                            , chkIGDPBFExercicio3
                            //Valores
                            , txtFNASExercicio3
                            , txtOrcamentoFederalExercicio3
                            , txtOutrosFundosFederaisExercicio3
                            , txtIGDSUASExercicio3
                            , txtIGDPBFExercicio3
                            //CheckBoxs
                            , chkFEASExercicio3
                            , chkOrcamentoEstadualExercicio3
                            , chkOutrosFundosMunicipaisExercicio3
                            //Valores
                            , txtFEASExercicio3
                            , txtOrcamentoEstadualExercicio3
                            , txtOutrosFundosMunicipaisExercicio3
                            //CheckBoxs
                            , chkFMASExercicio3
                            , chkOrcamentoMunicipalExercicio3
                            , chkOutrosFundosMunicipaisExercicio3
                            //Valores
                            , txtFMASExercicio3
                            , txtOrcamentoMunicipalExercicio3
                            , txtOutrosFundosMunicipaisExercicio3
                                    };

            return controles;
        }
        private WebControl[] ObterControlesRecursosFinanceirosExercicio4()
        {
            WebControl[] controles = {
                            //CheckBoxs
                              chkFNASExercicio4
                            , chkOutrosFundosMunicipaisExercicio4
                            , chkOrcamentoFederalExercicio4
                            , chkIGDSUASExercicio4
                            , chkIGDPBFExercicio4
                            //Valores
                            , txtFNASExercicio4
                            , txtOrcamentoFederalExercicio4
                            , txtOutrosFundosFederaisExercicio4
                            , txtIGDSUASExercicio4
                            , txtIGDPBFExercicio4
                            //CheckBoxs
                            , chkFEASExercicio4
                            , chkOrcamentoEstadualExercicio4
                            , chkOutrosFundosMunicipaisExercicio4
                            //Valores
                            , txtFEASExercicio4
                            , txtOrcamentoEstadualExercicio4
                            , txtOutrosFundosMunicipaisExercicio4
                            //CheckBoxs
                            , chkFMASExercicio4
                            , chkOrcamentoMunicipalExercicio4
                            , chkOutrosFundosMunicipaisExercicio4
                            //Valores
                            , txtFMASExercicio4
                            , txtOrcamentoMunicipalExercicio4
                            , txtOutrosFundosMunicipaisExercicio4
                                    };

            return controles;
        }

        private WebControl[] ObterControlesMetaPactuadaExercicio1()
        {
            WebControl[] controles = {
                                       txtMetaPactuadaExercicio1
                                     , txtPrevisaoAnualExercicio1
                                     , txtRecursosFNASExercicio1


                                    };

            return controles;
        }

        private WebControl[] ObterControlesMetaPactuadaExercicio2()
        {
            WebControl[] controles = {
                                        txtMetaPactuadaExercicio2
                                        , txtPrevisaoAnualExercicio2
                                        , txtRecursosFNASExercicio2
                                    };

            return controles;
        }

        private WebControl[] ObterControlesMetaPactuadaExercicio3()
        {
            WebControl[] controles = {
                                        txtMetaPactuadaExercicio3
                                        , txtPrevisaoAnualExercicio3
                                        , txtRecursosFNASExercicio3
                                    };

            return controles;
        }

        private WebControl[] ObterControlesMetaPactuadaExercicio4()
        {
            WebControl[] controles = {
                                        txtMetaPactuadaExercicio4
                                        , txtPrevisaoAnualExercicio4
                                        , txtRecursosFNASExercicio4
                                    };

            return controles;
        }


        #endregion
        private void AplicarRegraBloqueioDesbloqueioRecursosFinanceiros()
        {

            WebControl[] controlesExercicioRecursosFinanceirosCheckedBoxesExercicio1 = ObterControlesRecursosFinanceirosExercicio1();
            WebControl[] controlesExercicioRecursosFinanceirosCheckedBoxesExercicio2 = ObterControlesRecursosFinanceirosExercicio2();
            WebControl[] controlesExercicioRecursosFinanceirosCheckedBoxesExercicio3 = ObterControlesRecursosFinanceirosExercicio3();
            WebControl[] controlesExercicioRecursosFinanceirosCheckedBoxesExercicio4 = ObterControlesRecursosFinanceirosExercicio4();

            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesExercicioRecursosFinanceirosCheckedBoxesExercicio1, Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesExercicioRecursosFinanceirosCheckedBoxesExercicio2, Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesExercicioRecursosFinanceirosCheckedBoxesExercicio3, Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesExercicioRecursosFinanceirosCheckedBoxesExercicio4, Exercicios[3]);

        }


        private void AplicarRegraBloqueioDesbloqueioMetaPactuada()
        {
            WebControl[] controlesMetaPactuadaExercicio1 = ObterControlesMetaPactuadaExercicio1();
            WebControl[] controlesMetaPactuadaExercicio2 = ObterControlesMetaPactuadaExercicio2();
            WebControl[] controlesMetaPactuadaExercicio3 = ObterControlesMetaPactuadaExercicio3();
            WebControl[] controlesMetaPactuadaExercicio4 = ObterControlesMetaPactuadaExercicio4();

            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesMetaPactuadaExercicio1, Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesMetaPactuadaExercicio2, Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesMetaPactuadaExercicio3, Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesMetaPactuadaExercicio4, Exercicios[3]);

        }

        private void AplicarRegraBloqueioDesbloqueioPrevisaoAnual()
        {
            WebControl[] controlesExercicioPrevisaoExercicio1 = ObterControlesPrevisaoExercicio1();
            WebControl[] controlesExercicioPrevisaoExercicio2 = ObterControlesPrevisaoExercicio2();
            WebControl[] controlesExercicioPrevisaoExercicio3 = ObterControlesPrevisaoExercicio3();
            WebControl[] controlesExercicioPrevisaoExercicio4 = ObterControlesPrevisaoExercicio4();

            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesExercicioPrevisaoExercicio1, Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesExercicioPrevisaoExercicio2, Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesExercicioPrevisaoExercicio3, Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBlocoIII(controlesExercicioPrevisaoExercicio4, Exercicios[3]);
        }
        private void BloquearControles()
        {
            WebControl[] controles = {
                                             rblTransferenciaRendaDireta
                                             , ddlParceria
                                             , ddlTipoParceria
                                             , btnAdicionarParceria
                                             , rblParcerias
                                             , txtNome
                                             , txtNomeOrgao

                                             , ddlMesInicio
                                             , ddlMesTermino
                                             , txtAnoInicio
                                             , txtAnoTermino
                                             , txtAcoes
                                             , txtObjetivo
                                             , rblAbrangencia
                                             
                                             , btnSalvar
                                             , ddlBeneficiarios


                                             , chkCaracterizacaoUsuarios
                                             //, chkAcoesDesenvolvida
                                             //, chkAcoesSocioassistenciais
                                             , txtValorFEASPrimeiraParcela
                                             //, btnAdicionarGrupoGestor
                                             , ddlRepassePrimeiraParcela
                                             , txtAnoRepassePrimeiraParcela
                                             , txtValorProgramadoAnoAtual
                                             , txtValorProgramadoProximoAno
                                            

                                             //, rblExecutaPrograma
                                             , txtUnidadeOfertante
                                             , ddlEixoTecnologico
                                             , txtNomeCurso
                                             , btnAdicionarUnidadeOfertante
                                             ,lstUnidadeOfertante
                                         };


            Permissao.VerificarPermissaoControles(controles, Session);
            //Permissao.VerificarPermissaoControles(txtDataAdesao.Controles, Session);
        }
        #endregion

        #region Estruturas

        #region Estruturas [Clear/Limpar]

        public void ClearValoresEstruturaTipoPrograma()
        {
            rblTransferenciaRendaDireta.SelectedValue = "0";
        }

        //public void ClearValoresEstruturaExecutaPrograma()
        //{
        //    rblExecutaPrograma.SelectedValue = "0";
        //}

        public void ClearValoresEstruturaDataProgramaProjeto()
        {
            ddlMesInicio.SelectedValue = "0";
            txtAnoInicio.Text = String.Empty;
            ddlMesTermino.SelectedValue = "0";
            txtAnoTermino.Text = String.Empty;
        }

        //public void ClearValoresEstruturaDataAdesao()
        //{
        //    txtDataAdesao.Text = DateTime.Now.ToShortDateString();
        //}

        public void ClearValoresEstruturaPrevisaoAnual()
        {
            txtMetaPactuadaExercicio1.Text = string.Empty;
            txtMetaPactuadaExercicio2.Text = string.Empty;
            txtMetaPactuadaExercicio3.Text = string.Empty;
            txtMetaPactuadaExercicio4.Text = string.Empty;

            txtPrevisaoAnualExercicio1.Text = string.Empty;
            txtPrevisaoAnualExercicio2.Text = string.Empty;
            txtPrevisaoAnualExercicio3.Text = string.Empty;
            txtPrevisaoAnualExercicio4.Text = string.Empty;
        }

        public void ClearValoresEstruturaMetaPactuada()
        {
            txtRecursosFNASExercicio1.Text = string.Empty;
            txtRecursosFNASExercicio2.Text = string.Empty;
            txtRecursosFNASExercicio3.Text = string.Empty;
            txtRecursosFNASExercicio4.Text = string.Empty;
            //??
        }

        public void ClearValoresEstruturaCaracterizacaoUsuarios()
        {
            chkCaracterizacaoUsuarios.SelectedValue = null;
        }

        //public void ClearValoresEstruturaAcoesDesenvolvidas()
        //{
        //    chkAcoesDesenvolvida.SelectedValue = null;
        //}

        public void ClearValoresEstruturaAtividadesRealizadas()
        {
            txtAcoes.Text = string.Empty;
        }

        public void ClearValoresEstruturaAbrangencia()
        {
            //rblAbrangencia.SelectedValue = "0";
            rblAbrangencia.Items.Clear();
        }

        public void ClearValoresEstruturaParcerias()
        {
            rblParcerias.SelectedValue = "0";
            tbParcerias.Visible = false;
            if (ddlParceria != null)
            {
                ddlParceria.DataSource = null;
                ddlParceria.DataBind();
            }
            if (ddlTipoParceria != null)
            {
                ddlTipoParceria.DataSource = null;
                ddlTipoParceria.DataBind();
            }
            //trGrupoGestor.Visible = false;
            lstParcerias.Items.Clear();

        }
        #endregion

        #region Estruturas [Carregar]

        private void CarregarEstruturaNomePrograma(ProgramaProjetoInfo programaProjetoInfo)
        {
            //lblNome.Text = programaProjetoInfo.Nome;
            //lblObjetivo.Text = programaProjetoInfo.Objetivo;
        }

        private void CarregarEstruturaBeneficiarios(ProgramaProjetoInfo programaProjetoInfo)
        {
            ddlBeneficiarios.SelectedValue = programaProjetoInfo.IdUsuarioTransferenciaRenda != null ? programaProjetoInfo.IdUsuarioTransferenciaRenda.Value.ToString() : null;
        }

        public void CarregarEstruturaTipoPrograma(ProgramaProjetoInfo programaProjetoInfo)
        {
            rblTransferenciaRendaDireta.SelectedValue = (programaProjetoInfo.TransferenciaRendaDireta != null ? (programaProjetoInfo.TransferenciaRendaDireta.HasValue ? (programaProjetoInfo.TransferenciaRendaDireta.Value ? "1" : "0") : "0") : "0");
            this.ExibirTransferenciaRendaEstrutura();
            this.ClearEstruturaDataProgramaProjeto();

            this.CarregarEstruturaDataProgramaProjeto(programaProjetoInfo);

        }

        private void CarregarDadosTransferenciaRenda(ProgramaProjetoInfo programaProjetoInfo)
        {
            if (rblTransferenciaRendaDireta.SelectedValue == "1")
            {
                if (!String.IsNullOrEmpty(txtAnoInicio.Text))
                {
                    programaProjetoInfo.AnoInicio = Convert.ToInt32(txtAnoInicio.Text);
                }
                if (!String.IsNullOrEmpty(txtAnoTermino.Text))
                {
                    programaProjetoInfo.AnoTermino = Convert.ToInt32(txtAnoTermino.Text);
                }
                if (ddlMesInicio.SelectedValue != "0")
                {
                    programaProjetoInfo.MesInicio = Convert.ToInt32(ddlMesInicio.SelectedValue);
                }
                if (ddlMesTermino.SelectedValue != "0")
                {
                    programaProjetoInfo.MesTermino = Convert.ToInt32(ddlMesTermino.SelectedValue);
                }
            }
            else
            {
                programaProjetoInfo.AnoInicio = 0;
                programaProjetoInfo.AnoTermino = 0;
                programaProjetoInfo.MesInicio = 0;
                programaProjetoInfo.MesTermino = 0;
            }
        }

        public void CarregarEstruturaDataProgramaProjeto(ProgramaProjetoInfo programaProjetoInfo)
        {
            if (rblTransferenciaRendaDireta.SelectedValue == "1")
            {
                if (programaProjetoInfo.AnoInicio.HasValue)
                {
                    txtAnoInicio.Text = programaProjetoInfo.AnoInicio.Value.ToString();
                }
                if (programaProjetoInfo.AnoTermino.HasValue)
                {
                    txtAnoTermino.Text = programaProjetoInfo.AnoTermino.Value.ToString();
                }
                if (programaProjetoInfo.MesInicio.HasValue)
                {
                    ddlMesInicio.SelectedValue = programaProjetoInfo.MesInicio.Value.ToString();
                }
                if (programaProjetoInfo.MesTermino.HasValue)
                {
                    ddlMesTermino.SelectedValue = programaProjetoInfo.MesTermino.Value.ToString();
                }
            }
        }

        public void ClearEstruturaDataProgramaProjeto()
        {
            txtAnoInicio.Text = string.Empty;
            txtAnoTermino.Text = string.Empty;
            ddlMesInicio.SelectedValue = "0";
            ddlMesTermino.SelectedValue = "0";
        }

        //public void CarregarEstruturaDataAdesao(ProgramaProjetoInfo programaProjetoInfo)
        //{
        //    if (programaProjetoInfo.DataAdesaoPrograma.HasValue)
        //    {
        //        txtDataAdesao.Text = programaProjetoInfo.DataAdesaoPrograma.Value.ToShortDateString();
        //    }
        //}


        public void CarregarEstruturaPrevisaoAnual(ProxyProgramas proxy, ProgramaProjetoInfo programaProjetoInfo)
        {
            programaProjetoInfo.PrevisaoAnual = proxy.Service.GetPrevisaoAnualByProgramaProjeto(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));

            if (programaProjetoInfo.PrevisaoAnual != null)
            {
                txtMetaPactuadaExercicio1.Text = programaProjetoInfo.PrevisaoAnual.MetaPactuadaExercicio1.ToString();
                txtMetaPactuadaExercicio2.Text = programaProjetoInfo.PrevisaoAnual.MetaPactuadaExercicio2.ToString();
                txtMetaPactuadaExercicio3.Text = programaProjetoInfo.PrevisaoAnual.MetaPactuadaExercicio3.ToString();
                txtMetaPactuadaExercicio4.Text = programaProjetoInfo.PrevisaoAnual.MetaPactuadaExercicio4.ToString();

                txtPrevisaoAnualExercicio1.Text = programaProjetoInfo.PrevisaoAnual.PrevisaoAnualRepasseExercicio1.ToString("N2");
                txtPrevisaoAnualExercicio2.Text = programaProjetoInfo.PrevisaoAnual.PrevisaoAnualRepasseExercicio2.ToString("N2");
                txtPrevisaoAnualExercicio3.Text = programaProjetoInfo.PrevisaoAnual.PrevisaoAnualRepasseExercicio3.ToString("N2");
                txtPrevisaoAnualExercicio4.Text = programaProjetoInfo.PrevisaoAnual.PrevisaoAnualRepasseExercicio4.ToString("N2");


                if (!String.IsNullOrEmpty(txtPrevisaoAnualExercicio1.Text) && !String.IsNullOrEmpty(txtMetaPactuadaExercicio1.Text))
                {
                    lblPrevisaoAnualTotalExercicio1.Text = (programaProjetoInfo.PrevisaoAnual.PrevisaoAnualMunicipalExercicio1).ToString("N2");
                }
                if (!String.IsNullOrEmpty(txtPrevisaoAnualExercicio2.Text) && !String.IsNullOrEmpty(txtMetaPactuadaExercicio2.Text))
                {
                    lblPrevisaoAnualTotalExercicio2.Text = (programaProjetoInfo.PrevisaoAnual.PrevisaoAnualMunicipalExercicio2).ToString("N2");
                }
                if (!String.IsNullOrEmpty(txtPrevisaoAnualExercicio3.Text) && !String.IsNullOrEmpty(txtMetaPactuadaExercicio3.Text))
                {
                    lblPrevisaoAnualTotalExercicio3.Text = (programaProjetoInfo.PrevisaoAnual.PrevisaoAnualMunicipalExercicio3).ToString("N2");
                }
                if (!String.IsNullOrEmpty(txtPrevisaoAnualExercicio4.Text) && !String.IsNullOrEmpty(txtMetaPactuadaExercicio4.Text))
                {
                    lblPrevisaoAnualTotalExercicio4.Text = (programaProjetoInfo.PrevisaoAnual.PrevisaoAnualMunicipalExercicio4).ToString("N2");
                }

            }
        }

        public void CarregarEstruturaMetaPactuada(ProgramaProjetoInfo programaProjetoInfo)
        {
            txtRecursosFNASExercicio1.Text = (programaProjetoInfo.PrevisaoAnual != null) ? programaProjetoInfo.PrevisaoAnual.PrevisaoAnualRepasseExercicio1.ToString("N2") : (0).ToString("N2");
            txtRecursosFNASExercicio2.Text = (programaProjetoInfo.PrevisaoAnual != null) ? programaProjetoInfo.PrevisaoAnual.PrevisaoAnualRepasseExercicio2.ToString("N2") : (0).ToString("N2");
            txtRecursosFNASExercicio3.Text = (programaProjetoInfo.PrevisaoAnual != null) ? programaProjetoInfo.PrevisaoAnual.PrevisaoAnualRepasseExercicio3.ToString("N2") : (0).ToString("N2");
            txtRecursosFNASExercicio4.Text = (programaProjetoInfo.PrevisaoAnual != null) ? programaProjetoInfo.PrevisaoAnual.PrevisaoAnualRepasseExercicio4.ToString("N2") : (0).ToString("N2");
        }

        public void CarregarEstruturaAtividadesRealizadas(ProgramaProjetoInfo programaProjetoInfo)
        {
            txtAcoes.Text = programaProjetoInfo.Acoes;
        }

        public void CarregarEstruturaAbrangencia(ProgramaProjetoInfo programaProjetoInfo)
        {
            rblAbrangencia.SelectedValue = programaProjetoInfo.IdAbrangenciaTerritorial.ToString();
        }

        public void CarregarEstruturaParcerias(ProgramaProjetoInfo programaProjetoInfo)
        {
            Session["parcerias"] = programaProjetoInfo.PossuiParceriaFormal ? programaProjetoInfo.Parcerias : new List<ProgramaProjetoParceriaInfo>();

            bool existemParcerias = programaProjetoInfo.Parcerias != null && programaProjetoInfo.Parcerias.Count() > 0;

            if (existemParcerias)
            {
                lstParcerias.DataSource = programaProjetoInfo.Parcerias;
                lstParcerias.DataBind();
                rblParcerias.SelectedValue = "1";
                tbParcerias.Visible = true;
                lstParcerias.Visible = true;
            }
            else
            {
                rblParcerias.SelectedValue = "0";
            }
        }

        #endregion



        #endregion
    }
}
