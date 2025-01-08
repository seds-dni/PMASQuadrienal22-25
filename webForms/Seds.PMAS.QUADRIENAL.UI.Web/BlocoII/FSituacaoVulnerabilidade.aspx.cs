using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoII
{
    public partial class FSituacaoVulnerabilidade : System.Web.UI.Page
    {

        #region properties
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
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
                adicionarEventos();
                ValidaBloqueioDesbloqueio();
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    load(proxy);
                }

                // loadCaracterizacao();
                //   carregarIndicadores();
                carregarCombos();
                verificarAlteracoes();

               
            }

            #region Bloqueia , Desbloqueia e ordena Controles


            

            #endregion


            LoadExercicios();

        }

        private void AplicarBloqueioTodosOsCampos(Control parent, bool State)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is DropDownList)
                {
                    ((DropDownList)(c)).Enabled = State;
                }
                if (c is CheckBox)
                {
                    ((CheckBox)(c)).Enabled = State;
                }
                if (c is TextBox)
                {
                    ((TextBox)(c)).Enabled = State;
                }
                if (c is Button)
                {
                    ((Button)(c)).Enabled = State;
                }
                AplicarBloqueioTodosOsCampos(c, State);
            }
        }

        void carregarIndicadores()
        {
            ConsultaMunicipioIndicadoresInfo ind;
            ind = new Seds.PMAS.QUADRIENAL.Negocio.AnaliseDiagnostica().GetMunicipioIndicadoresByMunicipio(SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio);
            if (ind == null)
                return;
        }

        void carregarCombos()
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                ddlSituacaoVulnerabilidade.DataSource = proxy.Service.GetSituacoesVulnerabilidade().OrderBy(t => t.Ordem);
                ddlSituacaoVulnerabilidade.DataValueField = "Id";
                ddlSituacaoVulnerabilidade.DataTextField = "Nome";
                ddlSituacaoVulnerabilidade.DataBind();
                ddlSituacaoVulnerabilidade.Items.Insert(0, new ListItem(" Selecione ", "0"));
            }

        }


        void carregarComboClassificacao(List<ConsultaAnaliseDiagnosticaInfo> analise)
        {
            ddlClassificacao.DataSource = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.Where(t => !analise.Any(a => a.Classificacao == t));
            ddlClassificacao.DataBind();
            ddlClassificacao.Items.Insert(0, new ListItem(" Selecione ", "0"));
            ddlClassificacao.ClearSelection();
        }


        private WebControl[] ObterControlesEstruturasRhExercicio()
        {

            WebControl[] controles = { 
                                             chkCigano,
                                             txtNumeroCiganos,
                                             chkExtrativistas,
                                             txtNumeroExtrativistas,
                                             chkPescadores,
                                             txtNumeroPescadores,
                                             chkAfro,
                                             txtNumeroAfro,
                                             chkRibeirinha,
                                             txtNumeroRibeirinha,
                                             chkQuilombolas,
                                             txtNumeroQuilombolas,
                                             chkIndigenas, 
                                             txtNumeroIndigenas,
                                             chkAgricultores,
                                             txtNumeroAgricultores,
                                             chkIndigenas,
                                             txtNumeroIndigenas,
                                             chkAcampamentos,
                                             txtNumeroAcampamentos,
                                             chkInstalacaoPrisional,
                                             txtNumeroInstalacaoPrisional,
                                             chkTrabalhoSazonal,
                                             txtNumeroTrabalhoSazonal,
                                             chkAglomerado,
                                             txtNumeroAglomerado,
                                             chkOutroAssentamento,
                                             txtNumeroOutroAssentamento,
                                             chkNaoExisteComunidadeCitada,
                                             chkNaoExisteComunidade,
                                             btnSalvarComunidades,
                                             txtDemanda,
                                             ddlSituacaoVulnerabilidade,                                                  
                                             ddlClassificacao,
                                             btnSalvar
                                         };

            return controles;
        }

        private void AplicarBloqueioDesbloqueio()
        {
            var exercicio = Convert.ToInt32(hdfAno.Value);
            WebControl[] controlesExercicio = ObterControlesEstruturasRhExercicio();
            Permissao.BlocoII.VerificaPermissaoExercicioBlocoII(controlesExercicio, exercicio);
            
        }

        private void ValidaBloqueioDesbloqueio()
        {
            var exercicio = Convert.ToInt32(hdfAno.Value);
            WebControl[] controlesExercicio = ObterControlesEstruturasRhExercicio();
            var validaBloqueio = Permissao.BlocoII.VerificaPermissaoExercicioBlocoII(controlesExercicio, exercicio);

            if (validaBloqueio == true) 
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluir()", "exibeBtnExcluir();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluir()", "ocultaBtnExcluir();", true);
            }
        }
        private void LoadExercicios()
        {
            this.btnExercicio1.Text = FSituacaoVulnerabilidade.Exercicios[0].ToString();
            this.btnExercicio2.Text = FSituacaoVulnerabilidade.Exercicios[1].ToString();
            this.btnExercicio3.Text = FSituacaoVulnerabilidade.Exercicios[2].ToString();
            this.btnExercicio4.Text = FSituacaoVulnerabilidade.Exercicios[3].ToString();

            this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? Exercicios[0].ToString() : this.hdfAno.Value;
            if (FSituacaoVulnerabilidade.Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }
            if (FSituacaoVulnerabilidade.Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FSituacaoVulnerabilidade.Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FSituacaoVulnerabilidade.Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-info-seds";
            }

        }

        void load(ProxyRedeProtecaoSocial proxy)
        {

            int exercicio = Convert.ToInt32(hdfAno.Value);
            int idExercicio = 0;
            
            if(exercicio == 2022)
            {
                idExercicio = 5;
            }else if(exercicio == 2023)
            {
                idExercicio = 6;
            }
            else if (exercicio == 2024)
            {
                idExercicio = 7;
            }
            else if (exercicio == 2025)
            {
                idExercicio = 8;
            }


            var lst = proxy.Service.GetConsultaAnaliseDiagnosticaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(s => s.IdExercicio == idExercicio ).OrderBy(t => t.Classificacao);
            lstAnaliseDiagnostica.DataSource = lst;
            lstAnaliseDiagnostica.DataBind();
            carregarComboClassificacao(lst.ToList());

            var obj = proxy.Service.GetAnaliseDiagnosticaComunidadeByPrefeituraExercicio(SessaoPmas.UsuarioLogado.Prefeitura.Id,idExercicio);

            if (obj == null)
            {
                hdfIdComunidade.Value = Genericos.clsCrypto.Encrypt("0");
                this.AplicarBloqueioDesbloqueio();
                return;
            }

            hdfIdComunidade.Value = Genericos.clsCrypto.Encrypt(obj.Id.ToString());
            chkNaoExisteComunidade.Checked = obj.NaoExisteComunidade;
            chkNaoExisteComunidade_CheckedChanged(null, null);

            chkNaoExisteComunidadeCitada.Checked = obj.NaoExisteGrupo;
            chkNaoExisteComunidadeCitada_CheckedChanged(null, null);

            chkCigano.Checked = obj.ExisteCigano;
            chkCigano_CheckedChanged(null, null);
            txtNumeroCiganos.Text = obj.NumeroCiganos.HasValue ? obj.NumeroCiganos.ToString() : String.Empty;

            chkExtrativistas.Checked = obj.ExisteExtrativista;
            chkExtrativistas_CheckedChanged(null, null);
            txtNumeroExtrativistas.Text = obj.NumeroExtrativistas.HasValue ? obj.NumeroExtrativistas.Value.ToString() : String.Empty;

            chkPescadores.Checked = obj.ExistePescador;
            chkPescadores_CheckedChanged(null, null);
            txtNumeroPescadores.Text = obj.NumeroPescadores.HasValue ? obj.NumeroPescadores.Value.ToString() : String.Empty;

            chkAfro.Checked = obj.ExisteAfro;
            chkAfro_CheckedChanged(null, null);
            txtNumeroAfro.Text = obj.NumeroAfros.HasValue ? obj.NumeroAfros.Value.ToString() : String.Empty;

            chkRibeirinha.Checked = obj.ExisteRibeirinha;
            chkRibeirinha_CheckedChanged(null, null);
            txtNumeroRibeirinha.Text = obj.NumeroRibeirinhas.HasValue ? obj.NumeroRibeirinhas.Value.ToString() : String.Empty;

            chkIndigenas.Checked = obj.ExisteIndigena;
            chkIndigenas_CheckedChanged(null, null);
            txtNumeroIndigenas.Text = obj.NumeroIndigenas.HasValue ? obj.NumeroIndigenas.Value.ToString() : String.Empty;

            chkQuilombolas.Checked = obj.ExisteQuilombola;
            chkQuilombolas_CheckedChanged(null, null);
            txtNumeroQuilombolas.Text = obj.NumeroQuilombolas.HasValue ? obj.NumeroQuilombolas.Value.ToString() : String.Empty;

            chkAgricultores.Checked = obj.ExisteAgricultor;
            chkAgricultores_CheckedChanged(null, null);
            txtNumeroAgricultores.Text = obj.NumeroAgricultores.HasValue ? obj.NumeroAgricultores.Value.ToString() : String.Empty;

            chkAcampamentos.Checked = obj.ExisteAcampamento;
            chkAcampamentos_CheckedChanged(null, null);
            txtNumeroAcampamentos.Text = obj.NumeroAcampamentos.HasValue ? obj.NumeroAcampamentos.Value.ToString() : String.Empty;

            chkInstalacaoPrisional.Checked = obj.ExisteInstalacaoPrisional;
            chkInstalacaoPrisional_CheckedChanged(null, null);
            txtNumeroInstalacaoPrisional.Text = obj.NumeroInstalacoesPrisionais.HasValue ? obj.NumeroInstalacoesPrisionais.Value.ToString() : String.Empty;


            chkTrabalhoSazonal.Checked = obj.ExisteTrabalhoSazonal;
            chkTrabalhoSazonal_CheckedChanged(null, null);
            txtNumeroTrabalhoSazonal.Text = obj.NumeroTrabalhoSazonais.HasValue ? obj.NumeroTrabalhoSazonais.Value.ToString() : String.Empty;

            chkAglomerado.Checked = obj.ExisteAglomeradoSubnormal;
            chkAglomerado_CheckedChanged(null, null);
            txtNumeroAglomerado.Text = obj.NumeroAglomeradoSubnormais.HasValue ? obj.NumeroAglomeradoSubnormais.Value.ToString() : String.Empty;

            chkOutroAssentamento.Checked = obj.ExisteAssentamentoPrecario;
            chkOutroAssentamento_CheckedChanged(null, null);
            txtNumeroOutroAssentamento.Text = obj.NumeroAssentamentoPrecarios.HasValue ? obj.NumeroAssentamentoPrecarios.Value.ToString() : String.Empty;

            this.AplicarBloqueioDesbloqueio();

        }
        
        void adicionarEventos()
        {
            txtNumeroCiganos.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroExtrativistas.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroPescadores.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroAfro.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroRibeirinha.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroIndigenas.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroAgricultores.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroAcampamentos.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroInstalacaoPrisional.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroTrabalhoSazonal.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroAglomerado.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroOutroAssentamento.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtDemanda.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var msg = String.Empty;
            
            try
            {
                var analise = new AnaliseDiagnosticaInfo();
                analise.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                if (!String.IsNullOrEmpty(ddlSituacaoVulnerabilidade.SelectedValue))
                    analise.IdSituacaoVulnerabilidade = Convert.ToInt32(ddlSituacaoVulnerabilidade.SelectedValue);
                if (!String.IsNullOrEmpty(ddlClassificacao.SelectedValue))
                    analise.Classificacao = Convert.ToInt32(ddlClassificacao.SelectedValue);
                if (!String.IsNullOrEmpty(txtDemanda.Text))
                    analise.Demanda = Convert.ToInt32(txtDemanda.Text);

                int exercicio = Convert.ToInt32(hdfAno.Value);


                if (exercicio == 2022)
                {
                    analise.IdExercicio = 5;
                }
                else if (exercicio == 2023)
                {
                    analise.IdExercicio = 6;
                }
                else if (exercicio == 2024)
                {
                    analise.IdExercicio = 7;
                }
                else if (exercicio == 2025)
                {
                    analise.IdExercicio = 8;
                }


                new ValidadorAnaliseDiagnostica().Validar(analise);

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    if (analise.Id == 0)
                    {
                        proxy.Service.AddAnaliseDiagnostica(analise);
                        load(proxy);
                    }
                    else
                    {
                        proxy.Service.UpdateAnaliseDiagnostica(analise);
                    }
                    load(proxy);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                var script = Util.GetJavascriptDialogError(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Situação de vulnerabilidade e/ou risco registrada com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                txtDemanda.Text = String.Empty;
                ddlSituacaoVulnerabilidade.SelectedIndex = 0;
                return;
            }


        }

        protected void lstAnaliseDiagnostica_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstAnaliseDiagnostica.DataKeys[e.Item.DataItemIndex];
            try
            {
                switch (e.CommandName)
                {
                    case "Excluir":
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            proxy.Service.DeleteAnaliseDiagnostica(Convert.ToInt32(key["Id"]));
                            load(proxy);
                            var script = Util.GetJavaScriptDialogOK("Registro excluído com sucesso!");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        }
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



        protected void lstAnaliseDiagnostica_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
               WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };


                var item = (ConsultaAnaliseDiagnosticaInfo)e.Item.DataItem;
                if (item == null)
                    return;

                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }
        
        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                    //{
                    //    linkAlteracoesQuadro14.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 14);
                    //    linkAlteracoesQuadro14.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("14"));
                    linkAlteracoesQuadro15.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 15);
                linkAlteracoesQuadro15.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("15"));
            }
        }

        protected void chkCigano_CheckedChanged(object sender, EventArgs e)
        {
            trQtdCiganos.Visible = false;
            if (chkCigano.Checked)
                trQtdCiganos.Visible = true;

        }

        protected void chkExtrativistas_CheckedChanged(object sender, EventArgs e)
        {
            trQtdExtrativistas.Visible = false;
            if (chkExtrativistas.Checked)
                trQtdExtrativistas.Visible = true;
        }

        protected void chkPescadores_CheckedChanged(object sender, EventArgs e)
        {
            trQtdPescadores.Visible = false;
            if (chkPescadores.Checked)
                trQtdPescadores.Visible = true;
        }

        protected void chkAfro_CheckedChanged(object sender, EventArgs e)
        {
            trQtdAfros.Visible = false;
            if (chkAfro.Checked)
                trQtdAfros.Visible = true;
        }

        protected void chkRibeirinha_CheckedChanged(object sender, EventArgs e)
        {
            trQtdRibeirinha.Visible = false;
            if (chkRibeirinha.Checked)
                trQtdRibeirinha.Visible = true;
        }

        protected void chkIndigenas_CheckedChanged(object sender, EventArgs e)
        {
            trQtdIndigenas.Visible = false;
            if (chkIndigenas.Checked)
                trQtdIndigenas.Visible = true;

        }

        protected void chkAgricultores_CheckedChanged(object sender, EventArgs e)
        {
            trQtdAgricultores.Visible = false;
            if (chkAgricultores.Checked)
                trQtdAgricultores.Visible = true;
        }

        protected void chkAcampamentos_CheckedChanged(object sender, EventArgs e)
        {
            trQtdAcampamentos.Visible = false;
            if (chkAcampamentos.Checked)
                trQtdAcampamentos.Visible = true;
        }

        protected void chkInstalacaoPrisional_CheckedChanged(object sender, EventArgs e)
        {
            trQtdInstalacaoPrisional.Visible = false;
            if (chkInstalacaoPrisional.Checked)
                trQtdInstalacaoPrisional.Visible = true;
        }

        protected void chkTrabalhoSazonal_CheckedChanged(object sender, EventArgs e)
        {
            trQtdTrabalhoSazonal.Visible = false;
            if (chkTrabalhoSazonal.Checked)
                trQtdTrabalhoSazonal.Visible = true;
        }

        protected void chkAglomerado_CheckedChanged(object sender, EventArgs e)
        {
            trQtdAglomerado.Visible = false;
            if (chkAglomerado.Checked)
                trQtdAglomerado.Visible = true;
        }

        protected void chkOutroAssentamento_CheckedChanged(object sender, EventArgs e)
        {
            trQtdOutroAssentamento.Visible = false;
            if (chkOutroAssentamento.Checked)
                trQtdOutroAssentamento.Visible = true;
        }


        protected void chkQuilombolas_CheckedChanged(object sender, EventArgs e)
        {
            trQtdQuilombolas.Visible = false;
            if (chkQuilombolas.Checked)
                trQtdQuilombolas.Visible = true;

        }

        protected void btnSalvarComunidades_Click(object sender, EventArgs e)
        {

            SessaoPmas.VerificarSessao(this);
            try
            {
                var obj = new AnaliseDiagnosticaComunidadeInfo();
                //obj.Id = Convert.ToInt32(hdfIdComunidade.Value);
                obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                obj.ExisteCigano = chkCigano.Checked;

                if (!String.IsNullOrEmpty(txtNumeroCiganos.Text))
                    obj.NumeroCiganos = Convert.ToInt32(txtNumeroCiganos.Text);

                obj.ExisteExtrativista = chkExtrativistas.Checked;
                if (!String.IsNullOrEmpty(txtNumeroExtrativistas.Text))
                    obj.NumeroExtrativistas = Convert.ToInt32(txtNumeroExtrativistas.Text);

                obj.ExistePescador = chkPescadores.Checked;
                if (!String.IsNullOrEmpty(txtNumeroPescadores.Text))
                    obj.NumeroPescadores = Convert.ToInt32(txtNumeroPescadores.Text);

                obj.ExisteAfro = chkAfro.Checked;
                if (!String.IsNullOrEmpty(txtNumeroAfro.Text))
                    obj.NumeroAfros = Convert.ToInt32(txtNumeroAfro.Text);

                obj.ExisteRibeirinha = chkRibeirinha.Checked;
                if (!String.IsNullOrEmpty(txtNumeroRibeirinha.Text))
                    obj.NumeroRibeirinhas = Convert.ToInt32(txtNumeroRibeirinha.Text);

                obj.ExisteIndigena = chkIndigenas.Checked;
                if (!String.IsNullOrEmpty(txtNumeroIndigenas.Text))
                    obj.NumeroIndigenas = Convert.ToInt32(txtNumeroIndigenas.Text);

                obj.ExisteQuilombola = chkQuilombolas.Checked;
                if (!String.IsNullOrEmpty(txtNumeroQuilombolas.Text))
                    obj.NumeroQuilombolas = Convert.ToInt32(txtNumeroQuilombolas.Text);

                obj.ExisteAgricultor = chkAgricultores.Checked;
                if (!String.IsNullOrEmpty(txtNumeroAgricultores.Text))
                    obj.NumeroAgricultores = Convert.ToInt32(txtNumeroAgricultores.Text);

                obj.ExisteAcampamento = chkAcampamentos.Checked;
                if (!String.IsNullOrEmpty(txtNumeroAcampamentos.Text))
                    obj.NumeroAcampamentos = Convert.ToInt32(txtNumeroAcampamentos.Text);

                obj.ExisteInstalacaoPrisional = chkInstalacaoPrisional.Checked;
                if (!String.IsNullOrEmpty(txtNumeroInstalacaoPrisional.Text))
                    obj.NumeroInstalacoesPrisionais = Convert.ToInt32(txtNumeroInstalacaoPrisional.Text);


                obj.ExisteTrabalhoSazonal = chkTrabalhoSazonal.Checked;
                if (!String.IsNullOrEmpty(txtNumeroTrabalhoSazonal.Text))
                    obj.NumeroTrabalhoSazonais = Convert.ToInt32(txtNumeroTrabalhoSazonal.Text);

                obj.ExisteAglomeradoSubnormal = chkAglomerado.Checked;
                if (!String.IsNullOrEmpty(txtNumeroAglomerado.Text))
                    obj.NumeroAglomeradoSubnormais = Convert.ToInt32(txtNumeroAglomerado.Text);

                obj.ExisteAssentamentoPrecario = chkOutroAssentamento.Checked;
                if (!String.IsNullOrEmpty(txtNumeroOutroAssentamento.Text))
                    obj.NumeroAssentamentoPrecarios = Convert.ToInt32(txtNumeroOutroAssentamento.Text);

                obj.NaoExisteComunidade = chkNaoExisteComunidade.Checked;

                obj.NaoExisteGrupo = chkNaoExisteComunidadeCitada.Checked;

                int exercicio = Convert.ToInt32(hdfAno.Value);


                if (exercicio == 2022)
                {
                    obj.IdExercicio = 5;
                }
                else if (exercicio == 2023)
                {
                    obj.IdExercicio = 6;
                }
                else if (exercicio == 2024)
                {
                    obj.IdExercicio = 7;
                }
                else if (exercicio == 2025)
                {
                    obj.IdExercicio = 8;
                }



                new ValidadorAnaliseDiagnostica().ValidarComunidade(obj);

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    Int32 idComunidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdComunidade.Value));
                    obj.Id = idComunidade;
                    if (idComunidade == 0)
                        proxy.Service.AddAnaliseDiagnosticaComunidade(obj);
                    else
                        proxy.Service.UpdateAnaliseDiagnosTicaComunidade(obj);
                  
                    load(proxy);
                }
                var script = Util.GetJavaScriptDialogOK("Comunidade/grupos específicos registrado com sucesso");
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
            catch (Exception ex)
            {
                var script = Util.GetJavascriptDialogError(ex.Message.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
            }


        }

        protected void chkNaoExisteComunidadeCitada_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNaoExisteComunidadeCitada.Checked)
            {
                chkAgricultores.Checked = chkAcampamentos.Checked = chkInstalacaoPrisional.Checked = chkTrabalhoSazonal.Checked = chkAglomerado.Checked = chkOutroAssentamento.Checked = false;
                chkAgricultores.Enabled = chkAcampamentos.Enabled = chkInstalacaoPrisional.Enabled = chkTrabalhoSazonal.Enabled = chkAglomerado.Enabled = chkOutroAssentamento.Enabled = false;
                chkAgricultores_CheckedChanged(null, null);
                chkAcampamentos_CheckedChanged(null, null);
                chkInstalacaoPrisional_CheckedChanged(null, null);
                chkTrabalhoSazonal_CheckedChanged(null, null);
                chkAglomerado_CheckedChanged(null, null);
                chkInstalacaoPrisional_CheckedChanged(null, null);
                chkInstalacaoPrisional_CheckedChanged(null, null);
                chkAglomerado_CheckedChanged(null, null);
                chkOutroAssentamento_CheckedChanged(null, null);
                txtNumeroAgricultores.Text = txtNumeroAcampamentos.Text = txtNumeroInstalacaoPrisional.Text = txtNumeroTrabalhoSazonal.Text = txtNumeroAglomerado.Text = txtNumeroOutroAssentamento.Text = String.Empty;
            }
            else
            {
                chkAgricultores.Enabled = chkAcampamentos.Enabled = chkInstalacaoPrisional.Enabled = chkTrabalhoSazonal.Enabled = chkAglomerado.Enabled = chkOutroAssentamento.Enabled = true;
            }
        }

        protected void chkNaoExisteComunidade_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNaoExisteComunidade.Checked)
            {
                chkCigano.Checked = chkExtrativistas.Checked = chkPescadores.Checked = chkAfro.Checked = chkRibeirinha.Checked = chkIndigenas.Checked = chkQuilombolas.Checked = false;
                chkCigano.Enabled = chkExtrativistas.Enabled = chkPescadores.Enabled = chkAfro.Enabled = chkRibeirinha.Enabled = chkIndigenas.Enabled = chkQuilombolas.Enabled = false;
                chkCigano_CheckedChanged(null, null);
                chkExtrativistas_CheckedChanged(null, null);
                chkIndigenas_CheckedChanged(null, null);
                chkPescadores_CheckedChanged(null, null);
                chkAfro_CheckedChanged(null, null);
                chkRibeirinha_CheckedChanged(null, null);
                chkIndigenas_CheckedChanged(null, null);
                chkQuilombolas_CheckedChanged(null, null);
                txtNumeroCiganos.Text = txtNumeroExtrativistas.Text = txtNumeroIndigenas.Text = txtNumeroPescadores.Text = txtNumeroAfro.Text = txtNumeroRibeirinha.Text = txtNumeroQuilombolas.Text = String.Empty;
            }
            else
            {
                chkCigano.Enabled = chkExtrativistas.Enabled = chkIndigenas.Enabled = chkPescadores.Enabled = chkAfro.Enabled = chkRibeirinha.Enabled = chkIndigenas.Enabled = chkQuilombolas.Enabled = true;
            }
        }

      
        protected void btnExercicio1_Click(object sender, EventArgs e)
        {
            
            hdfAno.Value = btnExercicio1.Text;

            #region reload
            Clear();
            int exercicioSolicitado = (String.IsNullOrEmpty(hdfAno.Value)) ? 2022 : Convert.ToInt32(hdfAno.Value);
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
               

                load(proxy);
            }
            #endregion
            ValidaBloqueioDesbloqueio();
            btnExercicio1.CssClass = "btn btn-info-seds";
            btnExercicio2.CssClass = "btn btn-primary-seds";
            btnExercicio3.CssClass = "btn btn-primary-seds";
            btnExercicio4.CssClass = "btn btn-primary-seds";
            tbInconsistencias.Visible = false;
        }

        protected void btnExercicio2_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio2.Text;

            #region reload
            Clear();
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                //AdicionarEventos();
                load(proxy);
            }
            #endregion
            ValidaBloqueioDesbloqueio();
            btnExercicio1.CssClass = "btn btn-primary-seds";
            btnExercicio2.CssClass = "btn btn-info-seds";
            btnExercicio3.CssClass = "btn btn-primary-seds";
            btnExercicio4.CssClass = "btn btn-primary-seds";
            tbInconsistencias.Visible = false;
        }

        protected void btnExercicio3_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio3.Text;

            #region reload
            Clear();
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                //AdicionarEventos();
                load(proxy);
            }
            #endregion
            ValidaBloqueioDesbloqueio();        
            btnExercicio1.CssClass = "btn btn-primary-seds";
            btnExercicio2.CssClass = "btn btn-primary-seds";
            btnExercicio3.CssClass = "btn btn-info-seds";
            btnExercicio4.CssClass = "btn btn-primary-seds";
            tbInconsistencias.Visible = false;
        }

        protected void btnExercicio4_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio4.Text;

            #region reload
            Clear();
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                //AdicionarEventos();
                load(proxy);
            }
            #endregion
            ValidaBloqueioDesbloqueio();
            btnExercicio1.CssClass = "btn btn-primary-seds";
            btnExercicio2.CssClass = "btn btn-primary-seds";
            btnExercicio3.CssClass = "btn btn-primary-seds";
            btnExercicio4.CssClass = "btn btn-info-seds";
            tbInconsistencias.Visible = false;
        }

        private void Clear()
        {
             chkCigano.Checked = false;
             txtNumeroCiganos.Text = "";

             chkExtrativistas.Checked = false;
             txtNumeroExtrativistas.Text = "";

             chkPescadores.Checked = false;
             txtNumeroPescadores.Text = "";
             
             chkAfro.Checked = false;
             txtNumeroAfro.Text = "";
             
             chkRibeirinha.Checked = false;
             txtNumeroRibeirinha.Text = "";

             chkQuilombolas.Checked = false;
             txtNumeroQuilombolas.Text = "";

             chkIndigenas.Checked = false; 
             txtNumeroIndigenas.Text = "";

             chkAgricultores.Checked = false;
             txtNumeroAgricultores.Text = "";

             chkIndigenas.Checked = false;
             txtNumeroIndigenas.Text = "";

             chkAcampamentos.Checked = false;
             txtNumeroAcampamentos.Text = "";

             chkInstalacaoPrisional.Checked = false;
             txtNumeroInstalacaoPrisional.Text = "";

             chkTrabalhoSazonal.Checked = false;
             txtNumeroTrabalhoSazonal.Text = "";

             chkAglomerado.Checked = false;
             txtNumeroAglomerado.Text = "";

             chkOutroAssentamento.Checked = false;
             txtNumeroOutroAssentamento.Text = "";

             chkNaoExisteComunidadeCitada.Checked = false;
             chkNaoExisteComunidade.Checked = false;

             txtDemanda.Text = "";
             ddlSituacaoVulnerabilidade.ClearSelection();
             ddlClassificacao.ClearSelection();
             
           
        }

    }

}
