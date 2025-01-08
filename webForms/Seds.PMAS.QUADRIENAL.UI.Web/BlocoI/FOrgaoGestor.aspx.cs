using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using System.Web.UI.HtmlControls;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoI
{
    public partial class FOrgaoGestor : System.Web.UI.Page
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
                carregarCombos();
                carregarUsuarios();

                using (var proxy = new ProxyPrefeitura())
                {
                    AdicionarEventos();

                    load(new Prefeituras(proxy));
                    loadGestor(new Prefeituras(proxy));
                }

                verificarAlteracoes();

                this.Master.ScriptManagerControl.SetFocus(txtCNPJ.controleCNPJ);

                #region Bloqueia , Desbloqueia e ordena Controles

                Permissao.VerificarPermissaoControles(cep1.Controles, Session);
                Permissao.VerificarPermissaoControles(txtDataDecretoGestor.Controles, Session);
                Permissao.VerificarPermissaoControles(telefone.Controles, Session);
                Permissao.VerificarPermissaoControles(celular.Controles, Session);
                Permissao.VerificarPermissaoControles(txtCNPJ.Controles, Session);
                Permissao.VerificarPermissaoControles(txtDtCriacaoOrgaoGestor.Controles, Session);
                Permissao.VerificarPermissaoControles(txtDataAlteracao.Controles, Session);
                Permissao.VerificarPermissaoControles(txtRG.Controles, Session);
                Permissao.VerificarPermissaoControles(txtCPF.Controles, Session);
                Permissao.VerificarPermissaoControles(txtDataEmissao.Controles, Session);
                Permissao.VerificarPermissaoControles(txtDataPublicacaoLei.Controles, Session);
                Permissao.VerificarPermissaoControles(new WebControl[] { txtNome, ddlEstruturaOrgaoGestor, txtNumeroLeiCriacaoOrgaoGestor, txtAnoLeiCriacaoOrgaoGestor, txtNumeroLei, txtAnoLei, txtNumeroLeiSuas, txtAnoLeiSuas }, Session);


                #endregion
                fraOrgaoGestor.Attributes.Add("Class", "frame");
                fraRH.Attributes.Add("Class", "frame");
                fraGestor.Attributes.Add("Class", "frame");
            }

            
            LoadExercicios();
            

            Button btn = (Button)cep1.FindControl("cmdPesqCEP");
            btn.Click += new EventHandler(this.CEPBtn_Click);

        }



        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    alteracoesQuadro5.Visible = linkAlteracoesQuadro5.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 5);
                    linkAlteracoesQuadro5.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("5"));
                    //rblEquipeRedeDireta.Visible = false;
                }
            }
        }

        void AdicionarEventos()
        {
            txtEscolarizacaoBasica.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalBasica.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioBasica.Attributes.Add("onkeyup", "CalculateTotal()");
            txtSuperiorBasica.Attributes.Add("onkeyup", "CalculateTotal()");

            txtEscolarizacaoEspecial.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalEspecial.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioEspecial.Attributes.Add("onkeyup", "CalculateTotal()");
            txtSuperiorEspecial.Attributes.Add("onkeyup", "CalculateTotal()");

            txtEscolarizacaoSocioassistencial.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalSocioassistencial.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioSocioassistencial.Attributes.Add("onkeyup", "CalculateTotal()");
            txtSuperiorSocioassistencial.Attributes.Add("onkeyup", "CalculateTotal()");

            txtEscolarizacaoGestaoSuas.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalGestaoSuas.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioGestaoSuas.Attributes.Add("onkeyup", "CalculateTotal()");
            txtSuperiorGestaoSuas.Attributes.Add("onkeyup", "CalculateTotal()");
            
            txtEscolarizacaoTransferencia.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalTransferencia.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioTransferencia.Attributes.Add("onkeyup", "CalculateTotal()");
            txtSuperiorTransferencia.Attributes.Add("onkeyup", "CalculateTotal()");

            txtEscolarizacaoCadUnico.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalCadUnico.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioCadUnico.Attributes.Add("onkeyup", "CalculateTotal()");
            txtSuperiorCadUnico.Attributes.Add("onkeyup", "CalculateTotal()");

            txtEscolarizacaoGestaoFinanceira.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalGestaoFinanceira.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioGestaoFinanceira.Attributes.Add("onkeyup", "CalculateTotal()");
            txtSuperiorGestaoFinanceira.Attributes.Add("onkeyup", "CalculateTotal()");


            txtEscolarizacaoSUAS.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalSUAS.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioSUAS.Attributes.Add("onkeyup", "CalculateTotal()");
            txtSuperiorSUAS.Attributes.Add("onkeyup", "CalculateTotal()");

            txtEscolarizacaoRegulacaoSUAS.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalRegulacaoSUAS.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioRegulacaoSUAS.Attributes.Add("onkeyup", "CalculateTotal()");
            txtSuperiorRegulacaoSUAS.Attributes.Add("onkeyup", "CalculateTotal()");

            txtSuperiorGestaoSuas.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioGestaoSuas.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalGestaoSuas.Attributes.Add("onkeyup", "CalculateTotal()");
            txtEscolarizacaoGestaoSuas.Attributes.Add("onkeyup", "CalculateTotal()");

            //txtSuperiorServicoSocial.Attributes.Add("onkeyup", "calculaTotais()");
            //txtSuperiorPsicologia.Attributes.Add("onkeyup", "calculaTotais()");
            //txtSuperiorPedagogia.Attributes.Add("onkeyup", "calculaTotais()");
            //txtSociologia.Attributes.Add("onkeyup", "calculaTotais()");
            //txtDireito.Attributes.Add("onkeyup", "calculaTotais()");
            //txtSuperiorEconomiaDomestica.Attributes.Add("onkeyup", "calculaTotais()");

            //txtSuperiorAdministracao.Attributes.Add("onkeyup", "calculaTotais()");
            //txtSuperiorAntropologia.Attributes.Add("onkeyup", "calculaTotais()");
            //txtSuperiorContabilidade.Attributes.Add("onkeyup", "calculaTotais()");
            //txtSuperiorEconomia.Attributes.Add("onkeyup", "calculaTotais()");
            //txtSuperiorTerapiaOcupacional.Attributes.Add("onkeyup", "calculaTotais()");
            //txtOutros.Attributes.Add("onkeyup", "calculaTotais()" );

            //txtEstatutarios.Attributes.Add("onkeyup", "calculaTotais()");
            //txtCeletistas.Attributes.Add("onkeyup", "calculaTotais()");
            //txtComissionados.Attributes.Add("onkeyup", "calculaTotais()");
            //txtOutrosVinculos.Attributes.Add("onkeyup", "calculaTotais()");
            //txtEstagiarios.Attributes.Add("onkeyup", "calculaTotais()");
            //txtVoluntarios.Attributes.Add("onkeyup", "calculaTotais()");


           // txtEscolarizacaoRedeDireta.Attributes.Add("onkeyup", "CalculateTotal()");
            //txtFundamentalRedeDireta.Attributes.Add("onkeyup", "CalculateTotal()");
            //txtMedioRedeDireta.Attributes.Add("onkeyup", "CalculateTotal()");
            //txtSuperiorRedeDireta.Attributes.Add("onkeyup", "CalculateTotal()");

            txtEscolarizacaoOutraEquipe.Attributes.Add("onkeyup", "CalculateTotal()");
            txtFundamentalOutraEquipe.Attributes.Add("onkeyup", "CalculateTotal()");
            txtMedioOutraEquipe.Attributes.Add("onkeyup", "CalculateTotal()");
            txtSuperiorOutraEquipe.Attributes.Add("onkeyup", "CalculateTotal()");
            txtOutros.Attributes.Add("onkeyup", "hideOrShow()");
            txtOutros.Attributes.Add("onload", "hideOrShow()");
            btnExercicio1.Attributes.Add("onclick", "StartHideOrShow()");
            btnExercicio2.Attributes.Add("onclick", "StartHideOrShow()");
            btnExercicio3.Attributes.Add("onclick", "StartHideOrShow()");
            btnExercicio4.Attributes.Add("onclick", "StartHideOrShow()");
        }


        #region gestor
        void loadGestor(Prefeituras prefeituras)
        {
            var gestor = prefeituras.GetAtualGestorMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            carregarGestoresAnteriores(prefeituras);

            if (gestor != null)
            {
                hdfIdGestor.Value = Genericos.clsCrypto.Encrypt(gestor.Id.ToString());

                if (gestor.IdUsuarioGestor.HasValue)
                {
                    ddlUsuario.SelectedValue = gestor.IdUsuarioGestor.ToString();
                }
                txtdata.Text = gestor.DataNomeacao.ToShortDateString();
                ddlCargo.SelectedValue = gestor.IdCargo.ToString();
                //Outro Cargo
                if (gestor.IdCargo == 7)
                {
                    txtCargoOutro.Text = gestor.OutroCargo;
                }

                ddlEscolaridade.SelectedValue = gestor.IdEscolaridade.ToString();
                tdFormacaoAcademica.Visible = gestor.IdEscolaridade == 4;
                if (gestor.IdEscolaridade == 4)
                {
                    ddlFormacaoAcademica.SelectedValue = gestor.IdFormacao.ToString();

                    //Outra Formação
                    if (gestor.IdFormacao == 7)
                    {
                        txtOutraAreaFormacao.Text = gestor.OutraFormacao;
                    }
                }

                txtTelefone.Text = gestor.Telefone;
                txtCelular.Text = gestor.Celular;
                txtEmailGestor.Text = gestor.Email;

                if (!String.IsNullOrEmpty(gestor.RG))
                    txtRG.Txtrg = gestor.RG;
                if (!String.IsNullOrEmpty(gestor.RGDigito))
                    txtRG.Txtdigito = gestor.RGDigito;

                txtDataEmissao.Text = gestor.DataEmissao.HasValue ? gestor.DataEmissao.Value.ToShortDateString() : String.Empty;
                if (!String.IsNullOrEmpty(gestor.CPF))
                    txtCPF.Text = gestor.CPF;

                ddlUFEmissor.SelectedValue = gestor.UFRG.ToString();

                txtOrgEmissor.Text = gestor.Emissor;

                txtDecretoPortariaGestor.Text = !String.IsNullOrEmpty(gestor.NumeroDecreto) ? gestor.NumeroDecreto : String.Empty;
                txtDataDecretoGestor.Text = gestor.DataDecreto.HasValue ? gestor.DataDecreto.Value.ToShortDateString() : null;

                InibirCampos();
            }
            else
            {
                hdfIdGestor.Value = Genericos.clsCrypto.Encrypt("0");
                ExibirCampos();
            }


            this.ddlCargo_SelectedIndexChanged(null, null);
            this.ddlFormacaoProfissional_SelectedIndexChanged(null, null);


            #region Bloqueia , Desbloqueia e ordena Controles
            WebControl[] controles = { ddlUsuario  , 
                                         ddlCargo , 
                                         txtCargoOutro ,                                          
                                         ddlFormacaoAcademica , 
                                         ddlEscolaridade,                                        
                                         txtEmail , 
                                         btnSalvarGestor , 
                                         btnSubstituir ,
                                         btnEditar,
                                         lstGestores, 
                                         txtEmail
                                         };

            Permissao.VerificarPermissaoControles(txtTelefone.Controles, Session);
            Permissao.VerificarPermissaoControles(txtCelular.Controles, Session);
            Permissao.VerificarPermissaoControles(txtdata.Controles, Session);

            Permissao.VerificarPermissaoControles(controles, Session);
            #endregion
        }
        void carregarGestoresAnteriores(Prefeituras prefeituras)
        {
            lstGestores.DataSource = prefeituras.GetGestoresAnteriores(SessaoPmas.UsuarioLogado.Prefeitura.Id).OrderByDescending(t => t.DataNomeacao);
            lstGestores.DataBind();
        }
        #endregion

        void carregarCombos()
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                ddlEstruturaOrgaoGestor.DataSource = proxy.Service.GetEstruturasOrgaoGestor().OrderBy(t => t.Ordem);

                //Cargo
                ddlCargo.DataTextField = "Nome";
                ddlCargo.DataValueField = "Id";
                ddlCargo.DataSource = proxy.Service.GetCargosAdministrativos();
                ddlCargo.DataBind();
                ddlCargo.Items.Insert(0, new ListItem("[Escolha uma Opção]", "0"));

                //Escolaridade
                ddlEscolaridade.DataTextField = "Nome";
                ddlEscolaridade.DataValueField = "Id";
                ddlEscolaridade.DataSource = proxy.Service.GetEscolaridades();
                ddlEscolaridade.DataBind();
                ddlEscolaridade.Items.Insert(0, new ListItem("[Escolha uma Opção]", "0"));

                //Formação Acadêmica
                ddlFormacaoAcademica.DataTextField = "Nome";
                ddlFormacaoAcademica.DataValueField = "Id";
                ddlFormacaoAcademica.DataSource = proxy.Service.GetFormacoesAcademicas().OrderBy(f => f.Ordem);
                ddlFormacaoAcademica.DataBind();
                ddlFormacaoAcademica.Items.Insert(0, new ListItem("[Escolha uma Opção]", "0"));
            }
            ddlEstruturaOrgaoGestor.DataTextField = "Nome";
            ddlEstruturaOrgaoGestor.DataValueField = "Id";
            ddlEstruturaOrgaoGestor.DataBind();

            Util.InserirItemEscolha(ddlEstruturaOrgaoGestor);
        }
        void carregarUsuarios()
        {
            Int32? idMunicipio = SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio;
            Int32? idPerfil = (Int32)EPerfil.OrgaoGestor;
            using (var proxy = new ProxyUsuarioPMAS())
            {
                ddlUsuario.DataTextField = "Nome";
                ddlUsuario.DataValueField = "IdUsuario";
                ddlUsuario.DataSource = new Usuarios().GetConsultaUsuariosCadastrados("", "", null, idPerfil, idMunicipio, "", proxy).OrderBy(u => u.Nome);
                ddlUsuario.DataBind();
                ddlUsuario.Items.Insert(0, new ListItem("[Indique o nome do gestor]", "0"));
            }
        }

        private void CEPBtn_Click(object sender, EventArgs e)
        {
            fraOrgaoGestor.Attributes.Add("class", "frame active");
        }
        protected void rblAlteracaoLei_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            fraOrgaoGestor.Attributes.Add("Class", "frame active");

            tdDataLeiAlterada.Visible = tdLeiAlterada.Visible = rblAlteracaoLei.SelectedValue == "1";
            if (rblAlteracaoLei.SelectedValue == "0")
            {
                txtNumeroLei.Text = string.Empty;
                txtDataAlteracao.Text = string.Empty;
                //this.Master.ScriptManagerControl.SetFocus(txtSemEscolaridade);
                return;
            }
            this.Master.ScriptManagerControl.SetFocus(txtNumeroLei);
        }
        protected void ddlEstruturaOrgaoGestor_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            fraOrgaoGestor.Attributes.Add("class", "frame active");
          
            lblOutroOrgaoGestor.Visible = txtOutroOrgaoGestor.Visible = ddlEstruturaOrgaoGestor.SelectedValue == "7";
            if (ddlEstruturaOrgaoGestor.SelectedValue != "7")
            {
                txtOutroOrgaoGestor.Text = String.Empty;
                this.Master.ScriptManagerControl.SetFocus(cep1.controleCEP);
                return;
            }

            this.Master.ScriptManagerControl.SetFocus(txtOutroOrgaoGestor);

        }
        protected void chkPossuiSite_CheckedChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            txtSite.Enabled = !chkPossuiSite.Checked;
            if (!chkPossuiSite.Checked)
            {
                this.Master.ScriptManagerControl.SetFocus(txtSite);
                return;
            }
            else txtSite.Text = String.Empty;
            fraOrgaoGestor.Attributes.Add("class", "frame active");
            this.Master.ScriptManagerControl.SetFocus(txtNumeroLeiCriacaoOrgaoGestor);
        }
        protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trOutros.Visible = ddlCargo.SelectedValue == "7";
            fraGestor.Attributes.Add("class", "frame active");
            if (!trOutros.Visible)
            {
                txtCargoOutro.Text = String.Empty;
                this.Master.ScriptManagerControl.SetFocus(txtdata);
                return;
            }
            this.Master.ScriptManagerControl.SetFocus(txtCargoOutro);
        }
        protected void ddlFormacaoProfissional_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            //Outra
            trOutraFormacao.Visible = Convert.ToInt32(ddlFormacaoAcademica.SelectedValue) == 7;
            if (!trOutraFormacao.Visible)
            {
                txtOutraAreaFormacao.Text = string.Empty;
                txtTelefone.SetFocus();
                return;
            }
            this.Master.ScriptManagerControl.SetFocus(txtOutraAreaFormacao);
        }
        protected void btnSubstituir_Click(object sender, EventArgs e)
        {
            txtDataTerminoGestao.Enabled = btnSalvarTerminoGestao.Enabled = true;
            fraGestor.Attributes.Add("Class", "frame active");
            fraRH.Attributes.Add("Class", "frame");
            fraOrgaoGestor.Attributes.Add("Class", "frame");
            var script = Util.GetJavaScriptDialogWarning("Para finalizar a substituição do novo gestor, preencha o campo Data final da gestão.");

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
        protected void btnSalvarTerminoGestao_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            if (Convert.ToDateTime(txtDataTerminoGestao.Text) > DateTime.Today)
            {
                lblInconsistencias.Text = "A data de final da gestão não pode ser posterior à data atual!<br />";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(lblInconsistencias.Text), true);
                tbInconsistencias.Visible = true;
                return;
            }

            if (Convert.ToDateTime(txtDataTerminoGestao.Text) < Convert.ToDateTime(txtdata.Text))
            {
                lblInconsistencias.Text = "A data de final da gestão não pode ser inferior à data de nomeação!<br />";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(lblInconsistencias.Text), true);
                tbInconsistencias.Visible = true;
                return;
            }

            String msg = String.Empty;
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    prefeituras.SubstituirGestor(SessaoPmas.UsuarioLogado.Prefeitura.Id, Convert.ToDateTime(txtDataTerminoGestao.Text));
                    carregarGestoresAnteriores(prefeituras);
                }

                btnSubstituir.Enabled = false;
                btnEditar.Enabled = false;
                btnSalvar.Enabled = true;

                hdfIdGestor.Value = "0";

                ddlUsuario.SelectedIndex = 0;
                txtdata.Text = string.Empty;
                txtCPF.Text = String.Empty;
                txtRG.Txtrg = txtRG.Txtdigito = String.Empty;
                ddlCargo.SelectedIndex = 0;
                ddlCargo_SelectedIndexChanged(null, null);
                ddlFormacaoAcademica.SelectedIndex = 0;
                ddlFormacaoProfissional_SelectedIndexChanged(null, null);
                ddlEscolaridade.SelectedIndex = 0;
                ddlEscolaridade_SelectedIndexChanged(null, null);
                txtDataEmissao.Text = String.Empty;
                txtOrgEmissor.Text = String.Empty;
                ddlUFEmissor.SelectedIndex = 0;
                txtTelefone.Text = string.Empty;
                txtCelular.Text = string.Empty;
                txtEmailGestor.Text = string.Empty;
                txtDataTerminoGestao.Text = String.Empty;
                AbrirCampos();

                btnSalvarTerminoGestao.Enabled = txtDataTerminoGestao.Enabled = false;
            }

            catch (Exception ex)
            {
                msg = ex.Message;
                var script = Util.GetJavascriptDialogError(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }

            if (String.IsNullOrEmpty(msg))
            {
                //msg = "Gestor Municipal substítuido com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                //var script = Util.GetJavaScriptDialogOK(msg);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                this.Master.ScriptManagerControl.SetFocus(ddlUsuario);
                return;
            }

            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }
        protected void lstGestores_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstGestores.DataKeys[e.Item.DataItemIndex];
            try
            {
                switch (e.CommandName)
                {
                    case "Excluir_Gestor":
                        using (var proxy = new ProxyPrefeitura())
                        {
                            var prefeituras = new Prefeituras(proxy);
                            prefeituras.DeleteGestor(Convert.ToInt32(key["Id"]));
                            carregarGestoresAnteriores(prefeituras);
                            var script = Util.GetJavaScriptDialogOK("Gestor Municipal removido com sucesso!");
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
        protected void lstGestores_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void ddlEscolaridade_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            //Outra
            tdFormacaoAcademica.Visible = Convert.ToInt32(ddlEscolaridade.SelectedValue) == 4;
            if (!tdFormacaoAcademica.Visible)
            {
                txtTelefone.SetFocus();
                return;
            }
            this.Master.ScriptManagerControl.SetFocus(ddlFormacaoAcademica);
        }


        #region equipes especiais - exercicio

        private void LoadExercicios()
        {
            this.btnExercicio1.Text = FOrgaoGestor.Exercicios[0].ToString();
            this.btnExercicio2.Text = FOrgaoGestor.Exercicios[1].ToString();
            this.btnExercicio3.Text = FOrgaoGestor.Exercicios[2].ToString();
            this.btnExercicio4.Text = FOrgaoGestor.Exercicios[3].ToString();
            this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? Exercicios[0].ToString() : this.hdfAno.Value;
            if (FOrgaoGestor.Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                
                this.btnExercicio1.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";

            }
            if (FOrgaoGestor.Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";

            }

            if (FOrgaoGestor.Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";

            }

            if (FOrgaoGestor.Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-info-seds";

            }

        }

        protected void btnLoadExercicio1_Click(object sender, EventArgs e)
        {
            
            fraRH.Attributes.Add("Class", "frame active");
            hdfAno.Value = btnExercicio1.Text;

            #region reload
            Clear();
            int exercicioSolicitado = (String.IsNullOrEmpty(hdfAno.Value)) ? 2022 : Convert.ToInt32(hdfAno.Value);
            using (var proxy = new ProxyPrefeitura())
            {
                //AdicionarEventos();
                load(new Prefeituras(proxy));
            }
            #endregion

            fraOrgaoGestor.Attributes.Add("class", "frame");
            fraRH.Attributes.Add("class", "frame active");
            btnExercicio1.CssClass = "btn btn-info-seds";
            btnExercicio2.CssClass = "btn btn-primary-seds";
            btnExercicio3.CssClass = "btn btn-primary-seds";
            btnExercicio4.CssClass = "btn btn-primary-seds";
            tbInconsistencias.Visible = false;

            
        }
        protected void btnLoadExercicio2_Click(object sender, EventArgs e)
        {
            fraRH.Attributes.Add("class", "frame active");
            hdfAno.Value = btnExercicio2.Text;

            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                //AdicionarEventos();
                load(new Prefeituras(proxy));
            }
            #endregion

            fraOrgaoGestor.Attributes.Add("class", "frame");
            btnExercicio1.CssClass = "btn btn-primary-seds";
            btnExercicio2.CssClass = "btn btn-info-seds";
            btnExercicio3.CssClass = "btn btn-primary-seds";
            btnExercicio4.CssClass = "btn btn-primary-seds";
            tbInconsistencias.Visible = false;
      
      
            
        }
        protected void btnLoadExercicio3_Click(object sender, EventArgs e)
        {

            fraRH.Attributes.Add("class", "frame active");
            hdfAno.Value = btnExercicio3.Text;

            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                //AdicionarEventos();
                load(new Prefeituras(proxy));
            }
            #endregion

            fraOrgaoGestor.Attributes.Add("class", "frame");
            btnExercicio1.CssClass = "btn btn-primary-seds";
            btnExercicio2.CssClass = "btn btn-primary-seds";
            btnExercicio3.CssClass = "btn btn-info-seds";
            btnExercicio4.CssClass = "btn btn-primary-seds";
            tbInconsistencias.Visible = false;
           
        }
        protected void btnLoadExercicio4_Click(object sender, EventArgs e)
        {
            fraRH.Attributes.Add("class", "frame active");
            hdfAno.Value = btnExercicio4.Text;

            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                //AdicionarEventos();
                load(new Prefeituras(proxy));
            }
            #endregion

            fraOrgaoGestor.Attributes.Add("class", "frame");
            btnExercicio1.CssClass = "btn btn-primary-seds";
            btnExercicio2.CssClass = "btn btn-primary-seds";
            btnExercicio3.CssClass = "btn btn-primary-seds";
            btnExercicio4.CssClass = "btn btn-info-seds";
            tbInconsistencias.Visible = false;       
            
           
        }

        #endregion


        #region radion buttons

        protected void rblProtecaoBasica_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            //trProtecaoBasica.Visible = rblProtecaoBasica.SelectedValue == "1";
            //if (!trProtecaoBasica.Visible)
            //{
            //    txtEquipeProtecaoBasica.Text = String.Empty;
            //    this.Master.ScriptManagerControl.SetFocus(rblProtecaoEspecial);
            //    return;
            //}
            //this.Master.ScriptManagerControl.SetFocus(txtEquipeProtecaoBasica);
        }
        protected void rblProtecaoEspecial_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            //trProtecaoEspecial.Visible = rblProtecaoEspecial.SelectedValue == "1";
            //if (!trProtecaoEspecial.Visible)
            //{
            //    txtEquipeProtecaoEspecial.Text = String.Empty;
            //    this.Master.ScriptManagerControl.SetFocus(rblVigilanciaSocioassistencial);
            //    return;
            //}
            //this.Master.ScriptManagerControl.SetFocus(txtEquipeProtecaoEspecial);
        }
        protected void rblVigilanciaSocioassistencial_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            //trVigilanciaSocioassistencial.Visible = rblVigilanciaSocioassistencial.SelectedValue == "1";
            //if (!trVigilanciaSocioassistencial.Visible)
            //{
            //    txtEquipeVigilanciaSocioassistencial.Text = String.Empty;
            //    this.Master.ScriptManagerControl.SetFocus(btnSalvar);
            //    return;
            //}
            //this.Master.ScriptManagerControl.SetFocus(txtEquipeVigilanciaSocioassistencial);
        }
        protected void rblEquipeGestaoBeneficios_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            //trEquipeGestaoBeneficios.Visible = rblEquipeGestaoBeneficios.SelectedValue == "1";
            //if (!trEquipeGestaoBeneficios.Visible)
            //{

            //    txtEquipeGestaoBeneficio.Text = String.Empty;
            //    this.Master.ScriptManagerControl.SetFocus(btnSalvar);
            //    return;
            //}
            //this.Master.ScriptManagerControl.SetFocus(txtEquipeGestaoBeneficio);
        }
        protected void rblEquipeBasica_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trEstruturarBasica.Visible = rblEquipeBasica.SelectedValue == "0";
            if (rblEquipeBasica.SelectedValue == "1")
            {
                txtEscolarizacaoBasica.Enabled = txtFundamentalBasica.Enabled =
                txtMedioBasica.Enabled = txtSuperiorBasica.Enabled = true;
                //this.Master.ScriptManagerControl.SetFocus(txtEscolarizacaoBasica);
                fraRH.Attributes.Add("Class", "frame active");
                return;
            }
            else
            {
                txtEscolarizacaoBasica.Enabled = txtFundamentalBasica.Enabled =
              txtMedioBasica.Enabled = txtSuperiorBasica.Enabled = false;
                txtEscolarizacaoBasica.Text = txtFundamentalBasica.Text =
            txtMedioBasica.Text = txtSuperiorBasica.Text = lblTotalBasica.Text = "0";
                fraRH.Attributes.Add("Class", "frame active");
            }


        }
      
        protected void rblEquipeEspecial_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trEstruturarEspecial.Visible = rblEquipeEspecial.SelectedValue == "0";
            if (rblEquipeEspecial.SelectedValue == "1")
            {
                txtEscolarizacaoEspecial.Enabled = txtFundamentalEspecial.Enabled =
                txtMedioEspecial.Enabled = txtSuperiorEspecial.Enabled = true;
                //this.Master.ScriptManagerControl.SetFocus(txtEscolarizacaoEspecial);
                fraRH.Attributes.Add("Class", "frame active");
                return;
            }
            else
            {
                txtEscolarizacaoEspecial.Enabled = txtFundamentalEspecial.Enabled =
                   txtMedioEspecial.Enabled = txtSuperiorEspecial.Enabled = false;
                txtEscolarizacaoEspecial.Text = txtFundamentalEspecial.Text =
                txtMedioEspecial.Text = txtSuperiorEspecial.Text = lblTotalEspecial.Text = "0";
                fraRH.Attributes.Add("Class", "frame active");
            }

        }

        protected void rblGestaoSuas_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trGestaoDoSuas.Visible = rblGestaoSuas.SelectedValue == "0";
            
            if (rblGestaoSuas.SelectedValue == "1")
            {
                txtEscolarizacaoGestaoSuas.Enabled = txtFundamentalGestaoSuas.Enabled =
                txtMedioGestaoSuas.Enabled = txtSuperiorGestaoSuas.Enabled = true;
                fraRH.Attributes.Add("Class", "frame active");
                return;
            }
            else
            {
                txtEscolarizacaoGestaoSuas.Enabled = txtFundamentalGestaoSuas.Enabled =
               txtMedioGestaoSuas.Enabled = txtSuperiorGestaoSuas.Enabled = false;

                txtEscolarizacaoGestaoSuas.Text = txtFundamentalGestaoSuas.Text =
                txtMedioGestaoSuas.Text = txtSuperiorGestaoSuas.Text = lblTotalGestaoSuas.Text = "0";

                fraRH.Attributes.Add("Class", "frame active");
            }

        }

        protected void rdlEquipeTransferencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trEstruturarTransferencia.Visible = rdlEquipeTransferencia.SelectedValue == "0";
            if (rdlEquipeTransferencia.SelectedValue == "1")
            {
                txtEscolarizacaoTransferencia.Enabled = txtFundamentalTransferencia.Enabled =
                txtMedioTransferencia.Enabled = txtSuperiorTransferencia.Enabled = true;
                fraRH.Attributes.Add("Class", "frame active");
                //this.Master.ScriptManagerControl.SetFocus(txtEscolarizacaoTransferencia);
                return;
            }
            else
            {
                txtEscolarizacaoTransferencia.Enabled = txtFundamentalTransferencia.Enabled =
                 txtMedioTransferencia.Enabled = txtSuperiorTransferencia.Enabled = false;
                txtEscolarizacaoTransferencia.Text = txtFundamentalTransferencia.Text =
              txtMedioTransferencia.Text = txtSuperiorTransferencia.Text = lblTotalTransferencia.Text = "0";
                fraRH.Attributes.Add("Class", "frame active");
            }
        }
        protected void rblEquipeCadUnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trEstruturarCadUnico.Visible = rblEquipeCadUnico.SelectedValue == "0";
            if (rblEquipeCadUnico.SelectedValue == "1")
            {
                txtEscolarizacaoCadUnico.Enabled = txtFundamentalCadUnico.Enabled =
                txtMedioCadUnico.Enabled = txtSuperiorCadUnico.Enabled = true;
                //this.Master.ScriptManagerControl.SetFocus(txtEscolarizacaoCadUnico);
                fraRH.Attributes.Add("Class", "frame active");
                return;
            }
            else
            {
                txtEscolarizacaoCadUnico.Enabled = txtFundamentalCadUnico.Enabled =
                    txtMedioCadUnico.Enabled = txtSuperiorCadUnico.Enabled = false;

                txtEscolarizacaoCadUnico.Text = txtFundamentalCadUnico.Text =
              txtMedioCadUnico.Text = txtSuperiorCadUnico.Text = lblTotalCadUnico.Text = "0";
                fraRH.Attributes.Add("Class", "frame active");
            }
        }
        protected void rblEquipeGestaoFinanceira_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trEstruturarGestaoFinanceira.Visible = rblEquipeGestaoFinanceira.SelectedValue == "0";
            if (rblEquipeGestaoFinanceira.SelectedValue == "1")
            {
                txtEscolarizacaoGestaoFinanceira.Enabled = txtFundamentalGestaoFinanceira.Enabled =
                txtMedioGestaoFinanceira.Enabled = txtSuperiorGestaoFinanceira.Enabled = true;
                fraRH.Attributes.Add("Class", "frame active");
                //this.Master.ScriptManagerControl.SetFocus(txtEscolarizacaoGestaoFinanceira);
                return;
            }
            else
            {
                txtEscolarizacaoGestaoFinanceira.Enabled = txtFundamentalGestaoFinanceira.Enabled =
                 txtMedioGestaoFinanceira.Enabled = txtSuperiorGestaoFinanceira.Enabled = false;

                txtEscolarizacaoGestaoFinanceira.Text = txtFundamentalGestaoFinanceira.Text =
                 txtMedioGestaoFinanceira.Text = txtSuperiorGestaoFinanceira.Text = lblTotalGestaoFinanceira.Text = "0"; 
                fraRH.Attributes.Add("Class", "frame active");
            }
        }
        protected void rblEquipeGestaoSuas_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trEstruturarSuas.Visible = rblEquipeGestaoSuas.SelectedValue == "0";
            if (rblEquipeGestaoSuas.SelectedValue == "1")
            {
                txtEscolarizacaoSUAS.Enabled = txtFundamentalSUAS.Enabled =
                txtMedioSUAS.Enabled = txtSuperiorSUAS.Enabled = true;
                fraRH.Attributes.Add("Class", "frame active");
                //this.Master.ScriptManagerControl.SetFocus(txtEscolarizacaoSUAS);
                return;
            }
            else
            {
                txtEscolarizacaoSUAS.Enabled = txtFundamentalSUAS.Enabled =
                txtMedioSUAS.Enabled = txtSuperiorSUAS.Enabled = false;

                txtEscolarizacaoSUAS.Text = txtFundamentalSUAS.Text =
                txtMedioSUAS.Text = txtSuperiorSUAS.Text = lblTotalSUAS.Text = "0"; 
                fraRH.Attributes.Add("Class", "frame active");
            }
        }
        protected void rblEquipeSocioassistencial_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trEstruturarSocioassistencial.Visible = rblEquipeSocioassistencial.SelectedValue == "0";
            if (rblEquipeSocioassistencial.SelectedValue == "1")
            {
                txtEscolarizacaoSocioassistencial.Enabled = txtFundamentalSocioassistencial.Enabled =
                txtMedioSocioassistencial.Enabled = txtSuperiorSocioassistencial.Enabled = true;
                fraRH.Attributes.Add("Class", "frame active");
                //this.Master.ScriptManagerControl.SetFocus(txtEscolarizacaoSocioassistencial);
                return;
            }
            else
            {
                txtEscolarizacaoSocioassistencial.Enabled = txtFundamentalSocioassistencial.Enabled =
               txtMedioSocioassistencial.Enabled = txtSuperiorSocioassistencial.Enabled = false;

                txtEscolarizacaoSocioassistencial.Text = txtFundamentalSocioassistencial.Text =
                txtMedioSocioassistencial.Text = txtSuperiorSocioassistencial.Text = lblTotalSocioassistencial.Text = "0";
                fraRH.Attributes.Add("Class", "frame active");
            }
        }
        protected void rblEquipeRegulacaoSUAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trEstruturarRegulacaoSUAS.Visible = rblEquipeRegulacaoSUAS.SelectedValue == "0";
            if (rblEquipeRegulacaoSUAS.SelectedValue == "1")
            {
                txtEscolarizacaoRegulacaoSUAS.Enabled = txtFundamentalRegulacaoSUAS.Enabled =
                txtMedioRegulacaoSUAS.Enabled = txtSuperiorRegulacaoSUAS.Enabled = true;
                //this.Master.ScriptManagerControl.SetFocus(txtEscolarizacaoRegulacaoSUAS);
                fraRH.Attributes.Add("Class", "frame active");
                return;
            }
            else
            {
                txtEscolarizacaoRegulacaoSUAS.Enabled = txtFundamentalRegulacaoSUAS.Enabled =
                txtMedioRegulacaoSUAS.Enabled = txtSuperiorRegulacaoSUAS.Enabled = false;

                txtEscolarizacaoRegulacaoSUAS.Text = txtFundamentalRegulacaoSUAS.Text =
                txtMedioRegulacaoSUAS.Text = txtSuperiorRegulacaoSUAS.Text = lblTotalRegulacaoSUAS.Text = "0";
                fraRH.Attributes.Add("Class", "frame active");
            }
        }

        void VerificarOutros() 
        {
            if (txtVoluntarios.Text != "0")
            {
                lblEspecificarOutros.Visible = true;
            }
            else
            {
                lblEspecificarOutros.Visible = false;
            }

        }

        //protected void rblEquipeRedeDireta_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SessaoPmas.VerificarSessao(this);
        //    trEstruturarRedeDireta.Visible = rblEquipeRedeDireta.SelectedValue == "0";
        //    rblEquipeRedeDireta.Visible = false;
        //    if (rblEquipeRedeDireta.SelectedValue == "1")
        //    {
        //        txtEscolarizacaoRedeDireta.Enabled = txtFundamentalRedeDireta.Enabled =
        //        txtMedioRedeDireta.Enabled = txtSuperiorRedeDireta.Enabled = true;
        //        //this.Master.ScriptManagerControl.SetFocus(txtEscolarizacaoRedeDireta);

        //        return;
        //    }
        //    else
        //    {
        //        txtEscolarizacaoRedeDireta.Enabled = txtFundamentalRedeDireta.Enabled =
        //        txtMedioRedeDireta.Enabled = txtSuperiorRedeDireta.Enabled = false;

        //        txtEscolarizacaoRedeDireta.Text = txtFundamentalRedeDireta.Text =
        //        txtMedioRedeDireta.Text = txtSuperiorRedeDireta.Text = lblTotalRedeDireta.Text = "0";
        //        fraRH.Attributes.Add("Class", "frame active");
        //    }
        //    fraRH.Attributes.Add("Class", "frame active");
        //}
        protected void rblOutrasEquipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            fraRH.Attributes.Add("Class", "frame active");
            trEstruturarOutraEquipe.Visible = rblOutrasEquipes.SelectedValue == "0";

            if (rblOutrasEquipes.SelectedValue == "1")
            {
               // trEstruturarOutraEquipe.Visible = true;
                txtEscolarizacaoOutraEquipe.Enabled = txtFundamentalOutraEquipe.Enabled =
                txtMedioOutraEquipe.Enabled = txtSuperiorOutraEquipe.Enabled = true;
                //this.Master.ScriptManagerControl.SetFocus(txtEscolarizacaoOutraEquipe);
                return;
            }
            else
            {
                //  trEstruturarOutraEquipe.Visible = true;
                txtEscolarizacaoOutraEquipe.Enabled = txtFundamentalOutraEquipe.Enabled =
                txtMedioOutraEquipe.Enabled = txtSuperiorOutraEquipe.Enabled = false;
                txtEscolarizacaoOutraEquipe.Text = txtFundamentalOutraEquipe.Text = txtMedioOutraEquipe.Text = txtSuperiorOutraEquipe.Text = lblTotalOutraEquipe.Text = "0";
            }
            AdicionarEventos();
        }
        #endregion


        #region web methods

        [System.Web.Services.WebMethod]
        public static String CalcularProtecaoBasica(string escolarizacao, string fundamental, string medio, string superior)
        {
            int total = 0;
            total = Convert.ToInt32(!String.IsNullOrEmpty(escolarizacao) ? escolarizacao : "0") + Convert.ToInt32(!String.IsNullOrEmpty(fundamental) ? fundamental : "0") + Convert.ToInt32(!String.IsNullOrEmpty(medio) ? medio : "0") + Convert.ToInt32(!String.IsNullOrEmpty(superior) ? superior : "0");
            return total.ToString();
        }

        [System.Web.Services.WebMethod]
        public static String CalcularProtecaoEspecial(string escolarizacao, string fundamental, string medio, string superior)
        {
            int total= 0;
            total = Convert.ToInt32(!String.IsNullOrEmpty(escolarizacao) ? escolarizacao : "0") + Convert.ToInt32(!String.IsNullOrEmpty(fundamental) ? fundamental : "0") + Convert.ToInt32(!String.IsNullOrEmpty(medio) ? medio : "0") + Convert.ToInt32(!String.IsNullOrEmpty(superior) ? superior : "0");
            return total.ToString();
        }

        [System.Web.Services.WebMethod]
        public static String CalcularVigilancia(string escolarizacao, string fundamental, string medio, string superior)
        {
            int total = 0;
            total = Convert.ToInt32(!String.IsNullOrEmpty(escolarizacao) ? escolarizacao : "0") + Convert.ToInt32(!String.IsNullOrEmpty(fundamental) ? fundamental : "0") + Convert.ToInt32(!String.IsNullOrEmpty(medio) ? medio : "0") + Convert.ToInt32(!String.IsNullOrEmpty(superior) ? superior : "0");
            return total.ToString();
        }

        [System.Web.Services.WebMethod]
        public static String CalcularGestaoTransferencia(string escolarizacao, string fundamental, string medio, string superior)
        {
            int total = 0;
            total = Convert.ToInt32(!String.IsNullOrEmpty(escolarizacao) ? escolarizacao : "0") + Convert.ToInt32(!String.IsNullOrEmpty(fundamental) ? fundamental : "0") + Convert.ToInt32(!String.IsNullOrEmpty(medio) ? medio : "0") + Convert.ToInt32(!String.IsNullOrEmpty(superior) ? superior : "0");
            return total.ToString();
        }

        [System.Web.Services.WebMethod]
        public static String CalcularGestaoCadUnico(string escolarizacao, string fundamental, string medio, string superior)
        {
            int total = 0;
            total = Convert.ToInt32(!String.IsNullOrEmpty(escolarizacao) ? escolarizacao : "0") + Convert.ToInt32(!String.IsNullOrEmpty(fundamental) ? fundamental : "0") + Convert.ToInt32(!String.IsNullOrEmpty(medio) ? medio : "0") + Convert.ToInt32(!String.IsNullOrEmpty(superior) ? superior : "0");
            return total.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string AcessaGestorAC(Prefeituras prefeituras)
        {
            var gestor = prefeituras.GetAtualGestorMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            var nomeGestor = gestor.IdUsuarioGestor.ToString();

            return nomeGestor;
        }

        [System.Web.Services.WebMethod]
        public static String CalcularGestaoFinanceira(string escolarizacao, string fundamental, string medio, string superior)
        {
            int total = 0;
            total = Convert.ToInt32(!String.IsNullOrEmpty(escolarizacao) ? escolarizacao : "0") + Convert.ToInt32(!String.IsNullOrEmpty(fundamental) ? fundamental : "0") + Convert.ToInt32(!String.IsNullOrEmpty(medio) ? medio : "0") + Convert.ToInt32(!String.IsNullOrEmpty(superior) ? superior : "0");
            return total.ToString();
        }

        [System.Web.Services.WebMethod]
        public static String CalcularTrabalhoSUAS(string escolarizacao, string fundamental, string medio, string superior)
        {
            int total = 0;
            total = Convert.ToInt32(!String.IsNullOrEmpty(escolarizacao) ? escolarizacao : "0") + Convert.ToInt32(!String.IsNullOrEmpty(fundamental) ? fundamental : "0") + Convert.ToInt32(!String.IsNullOrEmpty(medio) ? medio : "0") + Convert.ToInt32(!String.IsNullOrEmpty(superior) ? superior : "0");
            return total.ToString();
        }

        [System.Web.Services.WebMethod]
        public static String CalcularRedeDireta(string escolarizacao, string fundamental, string medio, string superior)
        {
            int total = 0;
            total = Convert.ToInt32(!String.IsNullOrEmpty(escolarizacao) ? escolarizacao : "0") + Convert.ToInt32(!String.IsNullOrEmpty(fundamental) ? fundamental : "0") + Convert.ToInt32(!String.IsNullOrEmpty(medio) ? medio : "0") + Convert.ToInt32(!String.IsNullOrEmpty(superior) ? superior : "0");
            return total.ToString();
        }

        [System.Web.Services.WebMethod]
        public static String CalcularOutraEquipe(string escolarizacao, string fundamental, string medio, string superior)
        {
            int total = 0;
            total = Convert.ToInt32(!String.IsNullOrEmpty(escolarizacao) ? escolarizacao : "0") + Convert.ToInt32(!String.IsNullOrEmpty(fundamental) ? fundamental : "0") + Convert.ToInt32(!String.IsNullOrEmpty(medio) ? medio : "0") + Convert.ToInt32(!String.IsNullOrEmpty(superior) ? superior : "0");
            return total.ToString();
        }

        //[System.Web.Services.WebMethod]
        //public static String CalcularTotal




        [System.Web.Services.WebMethod]
        public static String CalcularValores(String[] valores)
        {
            int total = 0;
            foreach (String val in valores)
            {
                string valor = !String.IsNullOrEmpty(val) ? val : "0";
                total += Convert.ToInt32(valor);
            }

            

            return total.ToString();
        }
        #endregion


        #region CRUD: [1.3 - Identificacao "Gestor da AS" e 1.4 estruturacao/recursos]

        void load(Prefeituras prefeituras)
        {
            int exercicioSolicitado = (String.IsNullOrEmpty(hdfAno.Value)) ? 2022 : Convert.ToInt32(hdfAno.Value);

            OrgaoGestorInfo orgao = prefeituras.GetOrgaoGestorExercicio(SessaoPmas.UsuarioLogado.Prefeitura.Id,exercicioSolicitado);

            if (orgao != null)
            {
                #region dados gestor
                hdfIdOrgaoGestor.Value = orgao.Id.ToString();
                txtCNPJ.Text = orgao.CNPJ;
                txtNome.Text = orgao.Nome;
                ddlEstruturaOrgaoGestor.SelectedValue = orgao.IdEstrutura.ToString();
                if (orgao.IdEstrutura == 7)
                    txtOutroOrgaoGestor.Text = orgao.EstruturaOutros;

                cep1.Txtcep = orgao.CEP;
                cep1.Txtendereco = orgao.Logradouro;
                cep1.Txtnumero = orgao.Numero;
                cep1.Txtcomplemento = orgao.Complemento;
                cep1.Txtbairro = orgao.Bairro;
                cep1.Txtcidade = orgao.Cidade;

                telefone.Text = orgao.Telefone;
                celular.Text = orgao.Celular;
                if (!String.IsNullOrEmpty(orgao.Lei))
                {
                    txtNumeroLeiCriacaoOrgaoGestor.Text = orgao.Lei.Split('/')[0];
                    txtAnoLeiCriacaoOrgaoGestor.Text = orgao.Lei.Split('/')[1];
                }


                txtDtCriacaoOrgaoGestor.Text = orgao.DataLei.ToShortDateString();
                txtEmail.Text = orgao.Email;
                rblAlteracaoLei.SelectedValue = Convert.ToByte(orgao.AlteracaoLei).ToString();
                #endregion

                bool existeEquipeEspecificaTotais = orgao.EquipesEspecificasTotais != null;
                if (existeEquipeEspecificaTotais)
                {
                    var equipeEspecificaTotal = orgao.EquipesEspecificasTotais.Where(x => x.Exercicio == Convert.ToInt32(hdfAno.Value)).FirstOrDefault();
                    bool existeEquipeEspecificaTotalExercicioSolicitado = (equipeEspecificaTotal != null);

                    if (existeEquipeEspecificaTotalExercicioSolicitado)
                    {

                        #region Indique o tipo de vínculo dos trabalhadores:
                        txtEstatutarios.Text = equipeEspecificaTotal.TotalEstatutarios.ToString();
                        txtCeletistas.Text = equipeEspecificaTotal.TotalCeletistas.ToString();
                        txtEstagiarios.Text = equipeEspecificaTotal.TotalEstagiarios.ToString();
                        txtComissionados.Text = equipeEspecificaTotal.TotalComissionados.ToString();
                        txtOutrosVinculos.Text = equipeEspecificaTotal.TotalOutrosVinculos.ToString();
                        txtVoluntarios.Text = equipeEspecificaTotal.TotalVoluntarios.ToString();
                        #endregion

                        #region Area de formacao: Indique a área de formação dos trabalhadores que possuem nível superior
                        txtSuperiorServicoSocial.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorServicoSocial.ToString();
                        txtSuperiorPsicologia.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorPsicologia.ToString();
                        txtSuperiorPedagogia.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorPedagogia.ToString();
                        txtSociologia.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorSociologia.ToString();
                        txtDireito.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorDireito.ToString();
                        txtSuperiorAdministracao.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorAdministracao.ToString();
                        txtSuperiorAntropologia.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorAntropologia.ToString();
                        txtSuperiorContabilidade.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorContabilidade.ToString();
                        txtSuperiorEconomia.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorEconomia.ToString();
                        txtSuperiorEconomiaDomestica.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorEconomiaDomestica.ToString();
                        txtSuperiorTerapiaOcupacional.Text = equipeEspecificaTotal.TotalFuncionariosSuperiorTerapiaOcupacional.ToString();
                        txtOutros.Text = equipeEspecificaTotal.Outros.ToString();
                        lblEspecificarOutros.Text = equipeEspecificaTotal.EspecificarOutros;
                    

                        //if (equipeEspecificaTotal.Outros != null && equipeEspecificaTotal.Outros == 0)
                        //{
                        //    txtEspecificarOutros.Visible = false;
                        //}
                        //else 
                        //{
                        //    txtEspecificarOutros.Visible = true;
                        //}
                        #endregion

                        #region possui equipe especifica
                        rblEquipeBasica.SelectedValue = equipeEspecificaTotal.PossuiEquipeProtecaoBasica.HasValue
                            ? equipeEspecificaTotal.PossuiEquipeProtecaoBasica.Value.ToString() : String.Empty;

                        rblEquipeEspecial.SelectedValue = equipeEspecificaTotal.PossuiEquipeProtecaoEspecial.HasValue
                            ? equipeEspecificaTotal.PossuiEquipeProtecaoEspecial.Value.ToString() : String.Empty;

                        rblEquipeSocioassistencial.SelectedValue = equipeEspecificaTotal.PossuiEquipeVigilanciaSocioassistencial.HasValue
                            ? equipeEspecificaTotal.PossuiEquipeVigilanciaSocioassistencial.Value.ToString() : String.Empty;

                        rdlEquipeTransferencia.SelectedValue = equipeEspecificaTotal.PossuiEquipeGestaoTransferenciaRenda.HasValue
                            ? equipeEspecificaTotal.PossuiEquipeGestaoTransferenciaRenda.Value.ToString() : String.Empty;

                        rblEquipeGestaoFinanceira.SelectedValue = equipeEspecificaTotal.PossuiEquipeGestaoFinanceira.HasValue
                            ? equipeEspecificaTotal.PossuiEquipeGestaoFinanceira.Value.ToString() : String.Empty;

                       // rblEquipeRedeDireta.SelectedValue = equipeEspecificaTotal.PossuiEquipeRedeDireta.HasValue
                           // ? equipeEspecificaTotal.PossuiEquipeRedeDireta.Value.ToString() : String.Empty;

                        rblEquipeGestaoSuas.SelectedValue = equipeEspecificaTotal.PossuiEquipeGestaoSUAS.HasValue
                            ? equipeEspecificaTotal.PossuiEquipeGestaoSUAS.Value.ToString() : String.Empty;

                        rblOutrasEquipes.SelectedValue = equipeEspecificaTotal.PossuiOutrasEquipes.HasValue
                            ? equipeEspecificaTotal.PossuiOutrasEquipes.Value.ToString() : String.Empty;

                        rblEquipeCadUnico.SelectedValue = equipeEspecificaTotal.PossuiEquipeCadUnico.HasValue
                            ? equipeEspecificaTotal.PossuiEquipeCadUnico.Value.ToString() : String.Empty;

                        rblEquipeRegulacaoSUAS.SelectedValue = equipeEspecificaTotal.PossuiEquipeRegulacaoSUAS.HasValue
                            ? equipeEspecificaTotal.PossuiEquipeRegulacaoSUAS.Value.ToString() : String.Empty;

                        rblGestaoSuas.SelectedValue = equipeEspecificaTotal.PossuiEquipeGestorSUAS.HasValue
                            ? equipeEspecificaTotal.PossuiEquipeGestorSUAS.Value.ToString() : "0";


                        #endregion

                        #region popula equipes
                        if (equipeEspecificaTotal.PossuiEquipeProtecaoBasica.HasValue)
                        {
                            if (equipeEspecificaTotal.PossuiEquipeProtecaoBasica.Value == 1)
                            {
                                var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 1 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();
                                if (item != null)
                                {
                                    txtEscolarizacaoBasica.Text = item.SemEscolaridade.ToString();
                                    txtFundamentalBasica.Text = item.NivelFundamental.ToString();
                                    txtMedioBasica.Text = item.NivelMedio.ToString();
                                    txtSuperiorBasica.Text = item.NivelSuperior.ToString();
                                    lblTotalBasica.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);
                                }
                            }
                        }

                        if (equipeEspecificaTotal.PossuiEquipeProtecaoEspecial.HasValue)
                        {
                            if (equipeEspecificaTotal.PossuiEquipeProtecaoEspecial.Value == 1)
                            {
                                var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 2 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();
                                if (item != null)
                                {
                                    txtEscolarizacaoEspecial.Text = item.SemEscolaridade.ToString();
                                    txtFundamentalEspecial.Text = item.NivelFundamental.ToString();
                                    txtMedioEspecial.Text = item.NivelMedio.ToString();
                                    txtSuperiorEspecial.Text = item.NivelSuperior.ToString();
                                    lblTotalEspecial.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);
                                }
                            }
                        }

                        if (equipeEspecificaTotal.PossuiEquipeVigilanciaSocioassistencial.HasValue)
                        {
                            if (equipeEspecificaTotal.PossuiEquipeVigilanciaSocioassistencial.Value == 1)
                            {
                                var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 3 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();
                                if (item != null)
                                {

                                    txtEscolarizacaoSocioassistencial.Text = item.SemEscolaridade.ToString();
                                    txtFundamentalSocioassistencial.Text = item.NivelFundamental.ToString();
                                    txtMedioSocioassistencial.Text = item.NivelMedio.ToString();
                                    txtSuperiorSocioassistencial.Text = item.NivelSuperior.ToString();
                                    lblTotalSocioassistencial.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);
                                }
                            }
                        }

                        if (equipeEspecificaTotal.PossuiEquipeGestaoTransferenciaRenda.HasValue)
                        {
                            if (equipeEspecificaTotal.PossuiEquipeGestaoTransferenciaRenda.Value == 1)
                            {

                                var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 4 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();
                                if (item != null)
                                {
                                    txtEscolarizacaoTransferencia.Text = item.SemEscolaridade.ToString();
                                    txtFundamentalTransferencia.Text = item.NivelFundamental.ToString();
                                    txtMedioTransferencia.Text = item.NivelMedio.ToString();
                                    txtSuperiorTransferencia.Text = item.NivelSuperior.ToString();
                                    lblTotalTransferencia.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);
                                }
                            }
                        }

                        if (equipeEspecificaTotal.PossuiEquipeCadUnico.HasValue)
                        {
                            if (equipeEspecificaTotal.PossuiEquipeCadUnico.Value == 1)
                            {
                                var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 5 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();
                                if (item != null)
                                {
                                    txtEscolarizacaoCadUnico.Text = item.SemEscolaridade.ToString();
                                    txtFundamentalCadUnico.Text = item.NivelFundamental.ToString();
                                    txtMedioCadUnico.Text = item.NivelMedio.ToString();
                                    txtSuperiorCadUnico.Text = item.NivelSuperior.ToString();
                                    lblTotalCadUnico.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);
                                }
                            }
                        }

                        if (equipeEspecificaTotal.PossuiEquipeGestaoFinanceira.HasValue)
                        {
                            if (equipeEspecificaTotal.PossuiEquipeGestaoFinanceira.Value == 1)
                            {
                                var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 6 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();
                                if (item != null)
                                {
                                    txtEscolarizacaoGestaoFinanceira.Text = item.SemEscolaridade.ToString();
                                    txtFundamentalGestaoFinanceira.Text = item.NivelFundamental.ToString();
                                    txtMedioGestaoFinanceira.Text = item.NivelMedio.ToString();
                                    txtSuperiorGestaoFinanceira.Text = item.NivelSuperior.ToString();
                                    lblTotalGestaoFinanceira.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);
                                }
                            }
                        }

                        if (equipeEspecificaTotal.PossuiEquipeGestaoSUAS.HasValue)
                        {
                            if (equipeEspecificaTotal.PossuiEquipeGestaoSUAS.Value == 1)
                            {
                                var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 7 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();
                                if (item != null)
                                {
                                    txtEscolarizacaoSUAS.Text = item.SemEscolaridade.ToString();
                                    txtFundamentalSUAS.Text = item.NivelFundamental.ToString();
                                    txtMedioSUAS.Text = item.NivelMedio.ToString();
                                    txtSuperiorSUAS.Text = item.NivelSuperior.ToString();
                                    lblTotalSUAS.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);
                                }
                            }
                        }

                        if (equipeEspecificaTotal.PossuiEquipeRegulacaoSUAS.HasValue)
                        {
                            if (equipeEspecificaTotal.PossuiEquipeRegulacaoSUAS.Value == 1)
                            {
                                var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 8 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();
                                if (item != null)
                                {
                                    txtEscolarizacaoRegulacaoSUAS.Text = item.SemEscolaridade.ToString();
                                    txtFundamentalRegulacaoSUAS.Text = item.NivelFundamental.ToString();
                                    txtMedioRegulacaoSUAS.Text = item.NivelMedio.ToString();
                                    txtSuperiorRegulacaoSUAS.Text = item.NivelSuperior.ToString();
                                    lblTotalRegulacaoSUAS.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);
                                }
                            }
                        }

                        if (equipeEspecificaTotal.PossuiEquipeGestorSUAS.HasValue)
                        {
                            if (equipeEspecificaTotal.PossuiEquipeGestorSUAS.Value == 1)
                            {
                                if (orgao.EquipesEspecificas != null)
                                {
                                    var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 11 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();

                                    if (item != null)
                                    {

                                        txtEscolarizacaoGestaoSuas.Text = item.SemEscolaridade.ToString();
                                        txtFundamentalGestaoSuas.Text = item.NivelFundamental.ToString();
                                        txtMedioGestaoSuas.Text = item.NivelMedio.ToString();
                                        txtSuperiorGestaoSuas.Text = item.NivelSuperior.ToString();
                                        lblTotalGestaoSuas.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);
                                    }
                                    else
                                    {
                                        txtEscolarizacaoGestaoSuas.Text = "0";
                                        txtFundamentalGestaoSuas.Text = "0";
                                        txtMedioGestaoSuas.Text = "0";
                                        txtSuperiorGestaoSuas.Text = "0";
                                        lblTotalGestaoSuas.Text = "0";
                                    }
                                }
                            }
                        }

                        //if (equipeEspecificaTotal.PossuiEquipeRedeDireta.HasValue)
                        //{
                        //    if (equipeEspecificaTotal.PossuiEquipeRedeDireta.Value == 1)
                        //    {
                        //        var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 9 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();
                        //        if (item != null)
                        //        {
                        //            txtEscolarizacaoRedeDireta.Text = item.SemEscolaridade.ToString();
                        //            txtFundamentalRedeDireta.Text = item.NivelFundamental.ToString();
                        //            txtMedioRedeDireta.Text = item.NivelMedio.ToString();
                        //            txtSuperiorRedeDireta.Text = item.NivelSuperior.ToString();
                        //            lblTotalRedeDireta.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);
                        //        }

                        //    }
                        //}

                        if (equipeEspecificaTotal.PossuiOutrasEquipes.HasValue)
                        {
                            if (equipeEspecificaTotal.PossuiOutrasEquipes.Value == 1)
                            {
                                var item = orgao.EquipesEspecificas.Where(s => s.IdTipoEquipe == 10 && s.Exercicio == Convert.ToInt32(hdfAno.Value)).SingleOrDefault();
                                if (item != null)
                                {
                                    txtEscolarizacaoOutraEquipe.Text = item.SemEscolaridade.ToString();
                                    txtFundamentalOutraEquipe.Text = item.NivelFundamental.ToString();
                                    txtMedioOutraEquipe.Text = item.NivelMedio.ToString();
                                    txtSuperiorOutraEquipe.Text = item.NivelSuperior.ToString();
                                    lblTotalOutraEquipe.Text = Convert.ToString(item.SemEscolaridade + item.NivelFundamental + item.NivelMedio + item.NivelSuperior);                                    
                                }

                            }
                        }

                        #endregion
                    }


                    if (orgao.EquipesEspecificas != null && orgao.EquipesEspecificas.Count > 0)
                    {
                        var equipesEspecificasPorExercicio = orgao.EquipesEspecificas.Where(x => x.Exercicio == Convert.ToInt32(hdfAno.Value)).ToList();

                        lblTotalEscolarizacao.Text = equipesEspecificasPorExercicio.Where(S => S.IdTipoEquipe != 9).Sum(s => s.SemEscolaridade).ToString();
                        lblTotalFundamental.Text = equipesEspecificasPorExercicio.Where(S => S.IdTipoEquipe != 9).Sum(s => s.NivelFundamental).ToString();
                        lblTotalMedio.Text = equipesEspecificasPorExercicio.Where(S => S.IdTipoEquipe != 9).Sum(s => s.NivelMedio).ToString();
                        lblTotalSuperior.Text = equipesEspecificasPorExercicio.Where(S => S.IdTipoEquipe != 9).Sum(s => s.NivelSuperior).ToString();
                        lblTotal.Text = equipesEspecificasPorExercicio.Where(S => S.IdTipoEquipe != 9).Sum(s => s.NivelSuperior + s.NivelMedio + s.NivelFundamental + s.SemEscolaridade).ToString();
                    }
                }


                #region intencoes
                if (orgao.IntencoesEstruturacaoEquipe != null)
                {
                    var intencaoPorExercicio = orgao.IntencoesEstruturacaoEquipe.Where(x => x.Exercicio == Convert.ToInt32(hdfAno.Value)).FirstOrDefault();

                    if (intencaoPorExercicio != null) //pode ainda nao ter sido preenchido para o ano
                    {
                        rblEstruturacaoBasica.SelectedValue = intencaoPorExercicio.IntencaoAcaoEquipeBasica.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoEquipeBasica.Value).ToString() : "0";

                        rblEstruturacaoEspecial.SelectedValue = intencaoPorExercicio.IntencaoAcaoEquipeEspecial.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoEquipeEspecial.Value).ToString() : "0";

                        rblEstruturacaSocioassistencial.SelectedValue = intencaoPorExercicio.IntencaoAcaoEquipeVigilanciaSocioAssistencial.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoEquipeVigilanciaSocioAssistencial.Value).ToString() : "0";

                        rblEstuturacaoTransferencia.SelectedValue = intencaoPorExercicio.IntencaoAcaoEquipeGestaoBeneficios.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoEquipeGestaoBeneficios.Value).ToString() : "0";

                        rblEstruturacaoCadUnico.SelectedValue = intencaoPorExercicio.IntencaoAcaoEquipeGestaoCadUnico.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoEquipeGestaoCadUnico.Value).ToString() : "0";

                        rblEstruturarGestaoFinanceira.SelectedValue = intencaoPorExercicio.IntencaoAcaoEquipeGestaoFinanceira.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoEquipeGestaoFinanceira.Value).ToString() : "0";

                        rblEstruturarGestaoSuas.SelectedValue = intencaoPorExercicio.IntencaoAcaoEquipeGestaoSUAS.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoEquipeGestaoSUAS.Value).ToString() : "0";

                        rblGestaoDoSuas.SelectedValue = intencaoPorExercicio.IntencaoAcaoEquipeGestorSUAS.HasValue
                                                        ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoEquipeGestorSUAS.Value).ToString() : "0";

                        rblEstruturarRegulacaoSUAS.SelectedValue = intencaoPorExercicio.IntencaoAcaoEquipeRegulacaoSUAS.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoEquipeRegulacaoSUAS).ToString() : "0";

                        rblEstruturarRedeDireta.SelectedValue = intencaoPorExercicio.IntencaoAcaoEquipeRedeDireta.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoEquipeRedeDireta).ToString() : "0";

                        rblEstruturarOutraEquipe.SelectedValue = intencaoPorExercicio.IntencaoAcaoOutraEquipe.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoOutraEquipe).ToString() : "0";

                        rblAumentarEquipe.SelectedValue = intencaoPorExercicio.IntencaoAcaoOrgaoGestor.HasValue
                                                                ? Convert.ToInt32(intencaoPorExercicio.IntencaoAcaoOrgaoGestor).ToString() : "0";
                    }
                }
                else
                {
                    rblEstruturacaoBasica.SelectedValue = "0";

                    rblEstruturacaoEspecial.SelectedValue = "0";
                    rblGestaoDoSuas.SelectedValue = "0";
                    rblEstruturacaSocioassistencial.SelectedValue = "0";
                    rblEstuturacaoTransferencia.SelectedValue = "0";
                    rblEstruturacaoCadUnico.SelectedValue = "0";
                    rblEstruturarGestaoFinanceira.SelectedValue = "0";
                    rblEstruturarGestaoSuas.SelectedValue = "0";
                    rblEstruturarRegulacaoSUAS.SelectedValue = "0";
                    rblEstruturarRedeDireta.SelectedValue = "0";
                    rblEstruturarOutraEquipe.SelectedValue = "0";
                    rblAumentarEquipe.SelectedValue = "0";
                }
                #endregion

                #region site
                txtSite.Text = orgao.Site;
                chkPossuiSite.Checked = !orgao.PossuiSite;
                #endregion

                #region lei / alteracao
                rblAlteracaoLei.SelectedValue = Convert.ToByte(orgao.AlteracaoLei).ToString();
                if (orgao.AlteracaoLei.HasValue && orgao.AlteracaoLei.Value)
                {
                    if (!String.IsNullOrEmpty(orgao.LeiAlterada))
                    {
                        txtNumeroLei.Text = orgao.LeiAlterada.Split('/')[0];
                        txtAnoLei.Text = orgao.LeiAlterada.Split('/')[1];
                    }
                    if (orgao.DataLeiAlterada.HasValue)
                        txtDataAlteracao.Text = orgao.DataLeiAlterada.Value.ToShortDateString();
                }
                #endregion


                #region lei SUAS
                rblLeiDoSuas.SelectedValue = Convert.ToByte(orgao.PossuiLeiSuas).ToString();
                if (orgao.PossuiLeiSuas.HasValue)
                {
                    if (!String.IsNullOrEmpty(orgao.LeiSuas))
                    {
                        txtNumeroLeiSuas.Text = orgao.LeiSuas.Split('/')[0];
                        txtAnoLeiSuas.Text = orgao.LeiSuas.Split('/')[1];
                    }
                    if (orgao.DataPublicacaoLeiSuas != null)
                        txtDataPublicacaoLei.Text = orgao.DataPublicacaoLeiSuas.Value.ToShortDateString();
                }
                #endregion
            }


            this.AplicarBloqueioCamposPorRadioButtonSelecionado();

            this.AplicarBloqueioDesbloqueio();

            chkPossuiSite_CheckedChanged(null, null);
            rblAlteracaoLei_SelectedIndexChanged(null, null);
            rblLeiDoSuas_SelectedIndexChanged(null, null);
            ddlEstruturaOrgaoGestor_SelectedIndexChanged(null, null);

        }

        void CalcularTotais() 
        {
            int totalBasica = 0;
            int TotalEspecial = 0; 
            int TotalSocioassistencial = 0; 
            int TotalGestaoSuas = 0; 
            int TotalTransferencia = 0; 
            int TotalCadUnico = 0;  
            int TotalGestaoFinanceira = 0; 
            int TotalSUAS = 0; 
            int TotalRegulacaoSUAS = 0;
            int TotalOutraEquipe = 0;  
            int TotalEscolarizacao = 0;
            int TotalFundamental = 0;
            int TotalMedio = 0;
            int TotalSuperior = 0;
            

            totalBasica = Convert.ToInt32(txtEscolarizacaoBasica.Text) + Convert.ToInt32(txtFundamentalBasica.Text) + Convert.ToInt32(txtMedioBasica.Text) + Convert.ToInt32(txtSuperiorBasica.Text);
            lblTotalBasica.Text = totalBasica.ToString();

            TotalEspecial = Convert.ToInt32(txtEscolarizacaoEspecial.Text) + Convert.ToInt32(txtFundamentalEspecial.Text) + Convert.ToInt32(txtMedioEspecial.Text) + Convert.ToInt32(txtSuperiorEspecial.Text);
            lblTotalEspecial.Text = TotalEspecial.ToString();

            TotalSocioassistencial = Convert.ToInt32(txtEscolarizacaoSocioassistencial.Text) + Convert.ToInt32(txtFundamentalSocioassistencial.Text) + Convert.ToInt32(txtMedioSocioassistencial.Text) + Convert.ToInt32(txtSuperiorSocioassistencial.Text);
            lblTotalSocioassistencial.Text = TotalSocioassistencial.ToString();

            TotalGestaoSuas = Convert.ToInt32(txtEscolarizacaoGestaoSuas.Text) + Convert.ToInt32(txtFundamentalGestaoSuas.Text) + Convert.ToInt32(txtMedioGestaoSuas.Text) + Convert.ToInt32(txtSuperiorGestaoSuas.Text);
            lblTotalGestaoSuas.Text = TotalGestaoSuas.ToString();

            TotalTransferencia = Convert.ToInt32(txtEscolarizacaoTransferencia.Text) + Convert.ToInt32(txtFundamentalTransferencia.Text) + Convert.ToInt32(txtMedioTransferencia.Text) + Convert.ToInt32(txtSuperiorTransferencia.Text);
            lblTotalTransferencia.Text = TotalTransferencia.ToString();

            TotalCadUnico = Convert.ToInt32(txtEscolarizacaoCadUnico.Text) + Convert.ToInt32(txtFundamentalCadUnico.Text) + Convert.ToInt32(txtMedioCadUnico.Text) + Convert.ToInt32(txtSuperiorCadUnico.Text);
            lblTotalCadUnico.Text = TotalCadUnico.ToString();


            TotalGestaoFinanceira = Convert.ToInt32(txtEscolarizacaoGestaoFinanceira.Text) + Convert.ToInt32(txtFundamentalGestaoFinanceira.Text) + Convert.ToInt32(txtMedioGestaoFinanceira.Text) + Convert.ToInt32(txtSuperiorGestaoFinanceira.Text);
            lblTotalGestaoFinanceira.Text = TotalGestaoFinanceira.ToString();

            TotalSUAS = Convert.ToInt32(txtEscolarizacaoSUAS.Text) + Convert.ToInt32(txtFundamentalSUAS.Text) + Convert.ToInt32(txtMedioSUAS.Text) + Convert.ToInt32(txtSuperiorSUAS.Text);
            lblTotalSUAS.Text = TotalSUAS.ToString();


            TotalRegulacaoSUAS = Convert.ToInt32(txtEscolarizacaoRegulacaoSUAS.Text) + Convert.ToInt32(txtFundamentalRegulacaoSUAS.Text) + Convert.ToInt32(txtMedioRegulacaoSUAS.Text) + Convert.ToInt32(txtSuperiorRegulacaoSUAS.Text);
            lblTotalRegulacaoSUAS.Text = "";

            TotalOutraEquipe = Convert.ToInt32(txtEscolarizacaoOutraEquipe.Text) + Convert.ToInt32(txtFundamentalOutraEquipe.Text) + Convert.ToInt32(txtMedioOutraEquipe.Text) + Convert.ToInt32(txtSuperiorOutraEquipe.Text);
            lblTotalOutraEquipe.Text = TotalOutraEquipe.ToString();

            TotalEscolarizacao = Convert.ToInt32(txtEscolarizacaoBasica.Text) + Convert.ToInt32(txtEscolarizacaoEspecial.Text) + Convert.ToInt32(txtEscolarizacaoSocioassistencial.Text) + Convert.ToInt32(txtEscolarizacaoGestaoSuas.Text)
            + Convert.ToInt32(txtEscolarizacaoTransferencia.Text) + Convert.ToInt32(txtEscolarizacaoCadUnico.Text) + Convert.ToInt32(txtEscolarizacaoGestaoFinanceira.Text) + Convert.ToInt32(txtEscolarizacaoSUAS.Text) + Convert.ToInt32(txtEscolarizacaoRegulacaoSUAS.Text) + Convert.ToInt32(txtEscolarizacaoOutraEquipe.Text);
            lblTotalEscolarizacao.Text = TotalEscolarizacao.ToString();

            TotalFundamental = Convert.ToInt32(txtFundamentalBasica.Text) + Convert.ToInt32(txtFundamentalEspecial.Text) + Convert.ToInt32(txtFundamentalSocioassistencial.Text) + Convert.ToInt32(txtFundamentalGestaoSuas.Text) + Convert.ToInt32(txtFundamentalTransferencia.Text)
            + Convert.ToInt32(txtFundamentalCadUnico.Text) + Convert.ToInt32(txtFundamentalGestaoFinanceira.Text) + Convert.ToInt32(txtFundamentalSUAS.Text) + Convert.ToInt32(txtFundamentalRegulacaoSUAS.Text) + Convert.ToInt32(txtFundamentalOutraEquipe.Text);
            lblTotalFundamental.Text = TotalFundamental.ToString();

            TotalMedio = Convert.ToInt32(txtMedioBasica.Text) + Convert.ToInt32(txtMedioEspecial.Text) + Convert.ToInt32(txtMedioSocioassistencial.Text) + Convert.ToInt32(txtMedioGestaoSuas.Text) + Convert.ToInt32(txtMedioTransferencia.Text) + Convert.ToInt32(txtMedioCadUnico.Text)
            + Convert.ToInt32(txtMedioGestaoFinanceira.Text) + Convert.ToInt32(txtMedioSUAS.Text) + Convert.ToInt32(txtMedioRegulacaoSUAS.Text) + Convert.ToInt32(txtMedioOutraEquipe.Text);
            lblTotalMedio.Text = TotalMedio.ToString();

            TotalSuperior = Convert.ToInt32(txtSuperiorBasica.Text) + Convert.ToInt32(txtSuperiorEspecial.Text) + Convert.ToInt32(txtSuperiorSocioassistencial.Text) + Convert.ToInt32(txtSuperiorGestaoSuas.Text) 
            + Convert.ToInt32(txtSuperiorTransferencia.Text) + Convert.ToInt32(txtSuperiorCadUnico.Text) + Convert.ToInt32(txtSuperiorGestaoFinanceira.Text) + Convert.ToInt32(txtSuperiorSUAS.Text) + Convert.ToInt32(txtSuperiorRegulacaoSUAS.Text) + Convert.ToInt32(txtSuperiorOutraEquipe.Text);
            lblTotalSuperior.Text = TotalSuperior.ToString();
            
            lblTotal.Text = Convert.ToString(TotalEscolarizacao + TotalFundamental + TotalMedio + TotalSuperior);
        } 


        protected void btnSalvarOrgaoGestorDadoBasico_Click(object sender, EventArgs e)
        {

            #region propriedades
            DateTime dt;
            String msg = String.Empty;
            #endregion

            #region sessao
            SessaoPmas.VerificarSessao(this);
            #endregion

            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);

                OrgaoGestorInfo orgao = prefeituras.GetOrgaoGestorExercicio(SessaoPmas.UsuarioLogado.Prefeitura.Id,Convert.ToInt32(hdfAno.Value));

                orgao.Exercicio = Convert.ToInt32(hdfAno.Value);
                orgao.Id = Convert.ToInt32(hdfIdOrgaoGestor.Value);
                orgao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                orgao.CNPJ = txtCNPJ.Text;
                orgao.Nome = txtNome.Text;
                orgao.IdEstrutura = Convert.ToInt16(ddlEstruturaOrgaoGestor.SelectedValue);
                if (orgao.IdEstrutura == 7)
                {
                    orgao.EstruturaOutros = txtOutroOrgaoGestor.Text;
                }
                orgao.Telefone = telefone.Text.Trim();
                orgao.Celular = celular.Text.Trim();
                orgao.Logradouro = cep1.Txtendereco;
                orgao.Numero = cep1.Txtnumero;
                orgao.Bairro = cep1.Txtbairro;
                orgao.CEP = cep1.Txtcep;
                orgao.Complemento = cep1.Txtcomplemento;
                orgao.Cidade = cep1.Txtcidade;
                orgao.Email = txtEmail.Text;

                #region Lei de criação do Órgão Gestor
                if (!String.IsNullOrEmpty(txtNumeroLeiCriacaoOrgaoGestor.Text) && !String.IsNullOrEmpty(txtAnoLeiCriacaoOrgaoGestor.Text))
                {
                    orgao.Lei = txtNumeroLeiCriacaoOrgaoGestor.Text + "/" + txtAnoLeiCriacaoOrgaoGestor.Text;
                }
                if (!String.IsNullOrEmpty(txtDtCriacaoOrgaoGestor.Text) && DateTime.TryParse(txtDtCriacaoOrgaoGestor.Text, out dt))
                {
                    orgao.DataLei = Convert.ToDateTime(txtDtCriacaoOrgaoGestor.Text);
                }

                



                orgao.AlteracaoLei = Convert.ToBoolean(Convert.ToInt32(rblAlteracaoLei.SelectedValue));

                if (orgao.AlteracaoLei.Value)
                {
                    if (!String.IsNullOrEmpty(txtNumeroLei.Text) && !String.IsNullOrEmpty(txtAnoLei.Text))
                        orgao.LeiAlterada = txtNumeroLei.Text + "/" + txtAnoLei.Text;
                    if (!String.IsNullOrEmpty(txtDataAlteracao.Text) && DateTime.TryParse(txtDataAlteracao.Text, out dt))
                        orgao.DataLeiAlterada = Convert.ToDateTime(txtDataAlteracao.Text);
                }



                orgao.PossuiLeiSuas = Convert.ToBoolean(Convert.ToInt32(rblLeiDoSuas.SelectedValue));

                if (orgao.PossuiLeiSuas.Value)
                {
                    if (!String.IsNullOrEmpty(txtNumeroLeiSuas.Text) && !String.IsNullOrEmpty(txtAnoLeiSuas.Text))
                        orgao.LeiSuas = txtNumeroLeiSuas.Text + "/" + txtAnoLeiSuas.Text;
                    if (!String.IsNullOrEmpty(txtDataPublicacaoLei.Text))
                        orgao.DataPublicacaoLeiSuas = Convert.ToDateTime(txtDataPublicacaoLei.Text);
                }


                #endregion


                try
                {
                    prefeituras.SaveOrgaoGestorIdentificacao(orgao);
                    load(prefeituras);

                }
                catch (Exception ex)
                {
                    msg = ex.Message;

                }

                #region tratamento inconsistencias
                if (String.IsNullOrEmpty(msg))
                {
                    msg = orgao.Id == 0 ? "Dados do Órgão Gestor registrado com sucesso!" : "Dados do Órgão Gestor atualizados com sucesso!";
                    //msg += "<br/>O campo Nome do Órgão Gestor será exibido no cabeçalho do plano impresso.";
                    lblInconsistenciaIdentificacaoOrgaoGestorAS.Text = "";
                    tblInconsistenciaIdentificacaoOrgaoGestorAS.Visible = false;
                    fraOrgaoGestor.Attributes.Add("class", "frame");
                    fraGestor.Attributes.Add("class", "frame");
                    fraRH.Attributes.Add("class", "frame");
                    var script = Util.GetJavaScriptDialogOK(msg);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    return;
                }
                else {
                    lblInconsistenciaIdentificacaoOrgaoGestorAS.Text = msg;
                    tblInconsistenciaIdentificacaoOrgaoGestorAS.Visible = true;
                    var script = Util.GetJavascriptDialogError(msg);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                }
                #endregion
            }

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            #region propriedades
            DateTime dt;
            String msg = String.Empty;
            #endregion

            #region sessao
            SessaoPmas.VerificarSessao(this);
            #endregion

            #region identificacao
            var orgao = ObterIdentificacaoOrgaoGestor();
            #endregion


            #region Lei de criação do Órgão Gestor
            if (!String.IsNullOrEmpty(txtNumeroLeiCriacaoOrgaoGestor.Text) && !String.IsNullOrEmpty(txtAnoLeiCriacaoOrgaoGestor.Text))
            {
                orgao.Lei = txtNumeroLeiCriacaoOrgaoGestor.Text + "/" + txtAnoLeiCriacaoOrgaoGestor.Text;
            }
            if (!String.IsNullOrEmpty(txtDtCriacaoOrgaoGestor.Text) && DateTime.TryParse(txtDtCriacaoOrgaoGestor.Text, out dt))
            {
                orgao.DataLei = Convert.ToDateTime(txtDtCriacaoOrgaoGestor.Text);
            }

            orgao.AlteracaoLei = Convert.ToBoolean(Convert.ToInt32(rblAlteracaoLei.SelectedValue));

            if (orgao.AlteracaoLei.Value)
            {
                if (!String.IsNullOrEmpty(txtNumeroLei.Text) && !String.IsNullOrEmpty(txtAnoLei.Text))
                    orgao.LeiAlterada = txtNumeroLei.Text + "/" + txtAnoLei.Text;
                if (!String.IsNullOrEmpty(txtDataAlteracao.Text) && DateTime.TryParse(txtDataAlteracao.Text, out dt))
                    orgao.DataLeiAlterada = Convert.ToDateTime(txtDataAlteracao.Text);
            }

            #endregion

            #region site
            orgao.PossuiSite = !chkPossuiSite.Checked;
            if (orgao.PossuiSite)
            {
                orgao.Site = txtSite.Text;
            }
            #endregion

            #region área de formação/tipo de vínculo

            orgao.EquipesEspecificasTotais = new List<EquipeEspecificaTotaisInfo>();
            EquipeEspecificaTotaisInfo equipeEspecificaTotaisInfo = new EquipeEspecificaTotaisInfo();
            equipeEspecificaTotaisInfo.IdOrgaoGestor = Convert.ToInt32(hdfIdOrgaoGestor.Value);
            equipeEspecificaTotaisInfo.Exercicio = orgao.Exercicio;
            equipeEspecificaTotaisInfo.Desbloqueado = true;
            equipeEspecificaTotaisInfo.IdSituacao = 1;

            if (!String.IsNullOrEmpty(txtEstatutarios.Text))
                equipeEspecificaTotaisInfo.TotalEstatutarios = Convert.ToInt32(txtEstatutarios.Text);

            if (!String.IsNullOrEmpty(txtCeletistas.Text))
                equipeEspecificaTotaisInfo.TotalCeletistas = Convert.ToInt32(txtCeletistas.Text);

            if (!String.IsNullOrEmpty(txtComissionados.Text))
                equipeEspecificaTotaisInfo.TotalComissionados = Convert.ToInt32(txtComissionados.Text);

            if (!String.IsNullOrEmpty(txtOutrosVinculos.Text))
                equipeEspecificaTotaisInfo.TotalOutrosVinculos = Convert.ToInt32(txtOutrosVinculos.Text);

            if (!String.IsNullOrEmpty(txtEstagiarios.Text))
                equipeEspecificaTotaisInfo.TotalEstagiarios = Convert.ToInt32(txtEstagiarios.Text);

            if (!String.IsNullOrEmpty(txtVoluntarios.Text))
                equipeEspecificaTotaisInfo.TotalVoluntarios = Convert.ToInt32(txtVoluntarios.Text);

            if (!String.IsNullOrEmpty(txtSuperiorServicoSocial.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorServicoSocial = Convert.ToInt32(txtSuperiorServicoSocial.Text);
            if (!String.IsNullOrEmpty(txtSuperiorPsicologia.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorPsicologia = Convert.ToInt32(txtSuperiorPsicologia.Text);
            if (!String.IsNullOrEmpty(txtSuperiorPedagogia.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorPedagogia = Convert.ToInt32(txtSuperiorPedagogia.Text);
            if (!String.IsNullOrEmpty(txtSociologia.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorSociologia = Convert.ToInt32(txtSociologia.Text);
            if (!String.IsNullOrEmpty(txtDireito.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorDireito = Convert.ToInt32(txtDireito.Text);

            if (!String.IsNullOrEmpty(txtSuperiorAdministracao.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorAdministracao = Convert.ToInt32(txtSuperiorAdministracao.Text);
            if (!String.IsNullOrEmpty(txtSuperiorAntropologia.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorAntropologia = Convert.ToInt32(txtSuperiorAntropologia.Text);
            if (!String.IsNullOrEmpty(txtSuperiorContabilidade.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorContabilidade = Convert.ToInt32(txtSuperiorContabilidade.Text);
            if (!String.IsNullOrEmpty(txtSuperiorEconomia.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorEconomia = Convert.ToInt32(txtSuperiorEconomia.Text);
            if (!String.IsNullOrEmpty(txtSuperiorEconomiaDomestica.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorEconomiaDomestica = Convert.ToInt32(txtSuperiorEconomiaDomestica.Text);
            if (!String.IsNullOrEmpty(txtSuperiorTerapiaOcupacional.Text))
                equipeEspecificaTotaisInfo.TotalFuncionariosSuperiorTerapiaOcupacional = Convert.ToInt32(txtSuperiorTerapiaOcupacional.Text);
            if (!String.IsNullOrEmpty(txtOutros.Text))
                equipeEspecificaTotaisInfo.Outros = Convert.ToInt32(txtOutros.Text);

            if (!String.IsNullOrEmpty(lblEspecificarOutros.Text))
            {
                equipeEspecificaTotaisInfo.EspecificarOutros = lblEspecificarOutros.Text;
            }
            else
            {
                equipeEspecificaTotaisInfo.EspecificarOutros = String.Empty;
            }

            #endregion

            #region equipes especificadas
            #region Numero de Trabalhadores por equipe específica

            orgao.EquipesEspecificas = new List<EquipeEspecificaInfo>();
            equipeEspecificaTotaisInfo.PossuiEquipeProtecaoBasica = !String.IsNullOrEmpty(rblEquipeBasica.SelectedValue)
                ? Convert.ToInt32(rblEquipeBasica.SelectedValue) : -1;

            equipeEspecificaTotaisInfo.PossuiEquipeProtecaoEspecial = !String.IsNullOrEmpty(rblEquipeEspecial.SelectedValue)
                ? Convert.ToInt32(rblEquipeEspecial.SelectedValue) : -1;

            equipeEspecificaTotaisInfo.PossuiEquipeVigilanciaSocioassistencial = !String.IsNullOrEmpty(rblEquipeSocioassistencial.SelectedValue)
                ? Convert.ToInt32(rblEquipeSocioassistencial.SelectedValue) : -1;

            equipeEspecificaTotaisInfo.PossuiEquipeGestaoTransferenciaRenda = !String.IsNullOrEmpty(rdlEquipeTransferencia.SelectedValue)
                ? Convert.ToInt32(rdlEquipeTransferencia.SelectedValue) : -1;

            equipeEspecificaTotaisInfo.PossuiEquipeCadUnico = !String.IsNullOrEmpty(rblEquipeCadUnico.SelectedValue)
                ? Convert.ToInt32(rblEquipeCadUnico.SelectedValue) : -1;

            equipeEspecificaTotaisInfo.PossuiEquipeGestaoFinanceira = !String.IsNullOrEmpty(rblEquipeGestaoFinanceira.SelectedValue)
                ? Convert.ToInt32(rblEquipeGestaoFinanceira.SelectedValue) : -1;

            equipeEspecificaTotaisInfo.PossuiEquipeGestaoSUAS = !String.IsNullOrEmpty(rblEquipeGestaoSuas.SelectedValue)
                ? Convert.ToInt32(rblEquipeGestaoSuas.SelectedValue) : -1;

            equipeEspecificaTotaisInfo.PossuiEquipeGestorSUAS = !String.IsNullOrEmpty(rblGestaoSuas.SelectedValue)
                ? Convert.ToInt32(rblGestaoSuas.SelectedValue) : -1;

            equipeEspecificaTotaisInfo.PossuiEquipeRegulacaoSUAS = !String.IsNullOrEmpty(rblEquipeRegulacaoSUAS.SelectedValue)
                ? Convert.ToInt32(rblEquipeRegulacaoSUAS.SelectedValue) : -1;

            //equipeEspecificaTotaisInfo.PossuiEquipeRedeDireta = !String.IsNullOrEmpty(rblEquipeRedeDireta.SelectedValue)
            //    ? Convert.ToInt32(rblEquipeRedeDireta.SelectedValue) : -1;

            equipeEspecificaTotaisInfo.PossuiOutrasEquipes = !String.IsNullOrEmpty(rblOutrasEquipes.SelectedValue)
                ? Convert.ToInt32(rblOutrasEquipes.SelectedValue) : -1;



            #endregion

            if (equipeEspecificaTotaisInfo.PossuiEquipeProtecaoBasica.Value == 1)
            {
                EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
                equipe.IdOrgaoGestor = orgao.Id;
                equipe.IdPrefeitura = orgao.IdPrefeitura;
                equipe.IdTipoEquipe = 1;
                equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoBasica.Text) ? Convert.ToInt32(txtEscolarizacaoBasica.Text) : 0;
                equipe.NivelFundamental = !String.IsNullOrEmpty(txtFundamentalBasica.Text) ? Convert.ToInt32(txtFundamentalBasica.Text) : 0;
                equipe.NivelMedio = !String.IsNullOrEmpty(txtMedioBasica.Text) ? Convert.ToInt32(txtMedioBasica.Text) : 0;
                equipe.NivelSuperior = !String.IsNullOrEmpty(txtSuperiorBasica.Text) ? Convert.ToInt32(txtSuperiorBasica.Text) : 0;
                //Exercicio
                equipe.Exercicio = orgao.Exercicio;
                equipe.Desbloqueado = true;
                orgao.EquipesEspecificas.Add(equipe);

            }
            if (equipeEspecificaTotaisInfo.PossuiEquipeProtecaoEspecial.Value == 1)
            {
                EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
                equipe.IdOrgaoGestor = orgao.Id;
                equipe.IdPrefeitura = orgao.IdPrefeitura;
                equipe.IdTipoEquipe = 2;
                equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoEspecial.Text) ? Convert.ToInt32(txtEscolarizacaoEspecial.Text) : 0;
                equipe.NivelFundamental = !String.IsNullOrEmpty(txtFundamentalEspecial.Text) ? Convert.ToInt32(txtFundamentalEspecial.Text) : 0;
                equipe.NivelMedio = !String.IsNullOrEmpty(txtFundamentalEspecial.Text) ? Convert.ToInt32(txtMedioEspecial.Text) : 0;
                equipe.NivelSuperior = !String.IsNullOrEmpty(txtFundamentalEspecial.Text) ? Convert.ToInt32(txtSuperiorEspecial.Text) : 0;
                //Exercicio
                equipe.Exercicio = orgao.Exercicio; //por ano
                equipe.Desbloqueado = true;
                orgao.EquipesEspecificas.Add(equipe);
            }
            if (equipeEspecificaTotaisInfo.PossuiEquipeVigilanciaSocioassistencial.Value == 1)
            {
                EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
                equipe.IdOrgaoGestor = orgao.Id;
                equipe.IdPrefeitura = orgao.IdPrefeitura;
                equipe.IdTipoEquipe = 3;
                equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoSocioassistencial.Text) ? Convert.ToInt32(txtEscolarizacaoSocioassistencial.Text) : 0;
                equipe.NivelFundamental = !String.IsNullOrEmpty(txtEscolarizacaoSocioassistencial.Text) ? Convert.ToInt32(txtFundamentalSocioassistencial.Text) : 0;
                equipe.NivelMedio = !String.IsNullOrEmpty(txtEscolarizacaoSocioassistencial.Text) ? Convert.ToInt32(txtMedioSocioassistencial.Text) : 0;
                equipe.NivelSuperior = !String.IsNullOrEmpty(txtEscolarizacaoSocioassistencial.Text) ? Convert.ToInt32(txtSuperiorSocioassistencial.Text) : 0;
                //Exercicio
                equipe.Exercicio = orgao.Exercicio; //por ano
                equipe.Desbloqueado = true;
                orgao.EquipesEspecificas.Add(equipe);
            }
            if (equipeEspecificaTotaisInfo.PossuiEquipeGestorSUAS.Value == 1)
            {
                EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
                equipe.IdOrgaoGestor = orgao.Id;
                equipe.IdPrefeitura = orgao.IdPrefeitura;
                equipe.IdTipoEquipe = 11;
                equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoGestaoSuas.Text) ? Convert.ToInt32(txtEscolarizacaoGestaoSuas.Text) : 0;
                equipe.NivelFundamental = !String.IsNullOrEmpty(txtFundamentalGestaoSuas.Text) ? Convert.ToInt32(txtFundamentalGestaoSuas.Text) : 0;
                equipe.NivelMedio = !String.IsNullOrEmpty(txtMedioGestaoSuas.Text) ? Convert.ToInt32(txtMedioGestaoSuas.Text) : 0;
                equipe.NivelSuperior = !String.IsNullOrEmpty(txtSuperiorGestaoSuas.Text) ? Convert.ToInt32(txtSuperiorGestaoSuas.Text) : 0;
                //Exercicio
                equipe.Exercicio = orgao.Exercicio; //por ano
                equipe.Desbloqueado = true;
                orgao.EquipesEspecificas.Add(equipe);                
            }
            if (equipeEspecificaTotaisInfo.PossuiEquipeGestaoTransferenciaRenda.Value == 1)
            {
                EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
                equipe.IdOrgaoGestor = orgao.Id;
                equipe.IdPrefeitura = orgao.IdPrefeitura;
                equipe.IdTipoEquipe = 4;
                equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoTransferencia.Text) ? Convert.ToInt32(txtEscolarizacaoTransferencia.Text) : 0;
                equipe.NivelFundamental = !String.IsNullOrEmpty(txtFundamentalTransferencia.Text) ? Convert.ToInt32(txtFundamentalTransferencia.Text) : 0;
                equipe.NivelMedio = !String.IsNullOrEmpty(txtMedioTransferencia.Text) ? Convert.ToInt32(txtMedioTransferencia.Text) : 0;
                equipe.NivelSuperior = !String.IsNullOrEmpty(txtSuperiorTransferencia.Text) ? Convert.ToInt32(txtSuperiorTransferencia.Text) : 0;
                //Exercicio
                equipe.Exercicio = orgao.Exercicio; //por ano
                equipe.Desbloqueado = true;
                orgao.EquipesEspecificas.Add(equipe);
            }
            if (equipeEspecificaTotaisInfo.PossuiEquipeCadUnico.Value == 1)
            {
                EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
                equipe.IdOrgaoGestor = orgao.Id;
                equipe.IdPrefeitura = orgao.IdPrefeitura;
                equipe.IdTipoEquipe = 5;
                equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoCadUnico.Text) ? Convert.ToInt32(txtEscolarizacaoCadUnico.Text) : 0;
                equipe.NivelFundamental = !String.IsNullOrEmpty(txtFundamentalCadUnico.Text) ? Convert.ToInt32(txtFundamentalCadUnico.Text) : 0;
                equipe.NivelMedio = !String.IsNullOrEmpty(txtMedioCadUnico.Text) ? Convert.ToInt32(txtMedioCadUnico.Text) : 0;
                equipe.NivelSuperior = !String.IsNullOrEmpty(txtSuperiorCadUnico.Text) ? Convert.ToInt32(txtSuperiorCadUnico.Text) : 0;
                //Exercicio
                equipe.Exercicio = orgao.Exercicio; //por ano
                equipe.Desbloqueado = true;
                orgao.EquipesEspecificas.Add(equipe);
            }
            if (equipeEspecificaTotaisInfo.PossuiEquipeGestaoFinanceira.Value == 1)
            {
                EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
                equipe.IdOrgaoGestor = orgao.Id;
                equipe.IdPrefeitura = orgao.IdPrefeitura;
                equipe.IdTipoEquipe = 6;
                equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoGestaoFinanceira.Text) ? Convert.ToInt32(txtEscolarizacaoGestaoFinanceira.Text) : 0;
                equipe.NivelFundamental = !String.IsNullOrEmpty(txtFundamentalGestaoFinanceira.Text) ? Convert.ToInt32(txtFundamentalGestaoFinanceira.Text) : 0;
                equipe.NivelMedio = !String.IsNullOrEmpty(txtMedioGestaoFinanceira.Text) ? Convert.ToInt32(txtMedioGestaoFinanceira.Text) : 0;
                equipe.NivelSuperior = !String.IsNullOrEmpty(txtSuperiorGestaoFinanceira.Text) ? Convert.ToInt32(txtSuperiorGestaoFinanceira.Text) : 0;
                //Exercicio
                equipe.Exercicio = orgao.Exercicio; //por ano
                equipe.Desbloqueado = true;
                orgao.EquipesEspecificas.Add(equipe);
            }
            if (equipeEspecificaTotaisInfo.PossuiEquipeGestaoSUAS.Value == 1)
            {
                EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
                equipe.IdOrgaoGestor = orgao.Id;
                equipe.IdPrefeitura = orgao.IdPrefeitura;
                equipe.IdTipoEquipe = 7;
                equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoSUAS.Text) ? Convert.ToInt32(txtEscolarizacaoSUAS.Text) : 0;
                equipe.NivelFundamental = !String.IsNullOrEmpty(txtFundamentalSUAS.Text) ? Convert.ToInt32(txtFundamentalSUAS.Text) : 0;
                equipe.NivelMedio = !String.IsNullOrEmpty(txtMedioSUAS.Text) ? Convert.ToInt32(txtMedioSUAS.Text) : 0;
                equipe.NivelSuperior = !String.IsNullOrEmpty(txtSuperiorSUAS.Text) ? Convert.ToInt32(txtSuperiorSUAS.Text) : 0;
                //Exercicio
                equipe.Exercicio = orgao.Exercicio; //por ano
                equipe.Desbloqueado = true;
                orgao.EquipesEspecificas.Add(equipe);
            }

            if (equipeEspecificaTotaisInfo.PossuiEquipeRegulacaoSUAS.Value == 1)
            {
                EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
                equipe.IdOrgaoGestor = orgao.Id;
                equipe.IdPrefeitura = orgao.IdPrefeitura;
                equipe.IdTipoEquipe = 8;
                equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoRegulacaoSUAS.Text) ? Convert.ToInt32(txtEscolarizacaoRegulacaoSUAS.Text) : 0;
                equipe.NivelFundamental = !String.IsNullOrEmpty(txtFundamentalRegulacaoSUAS.Text) ? Convert.ToInt32(txtFundamentalRegulacaoSUAS.Text) : 0;
                equipe.NivelMedio = !String.IsNullOrEmpty(txtMedioRegulacaoSUAS.Text) ? Convert.ToInt32(txtMedioRegulacaoSUAS.Text) : 0;
                equipe.NivelSuperior = !String.IsNullOrEmpty(txtSuperiorRegulacaoSUAS.Text) ? Convert.ToInt32(txtSuperiorRegulacaoSUAS.Text) : 0;
                //Exercicio
                equipe.Exercicio = orgao.Exercicio; //por ano
                equipe.Desbloqueado = true;
                orgao.EquipesEspecificas.Add(equipe);
            }

            //if (equipeEspecificaTotaisInfo.PossuiEquipeRedeDireta.Value == 1)
            //{
            //    EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
            //    equipe.IdOrgaoGestor = orgao.Id;
            //    equipe.IdPrefeitura = orgao.IdPrefeitura;
            //    equipe.IdTipoEquipe = 9;
            //    equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoRedeDireta.Text) ? Convert.ToInt32(txtEscolarizacaoRedeDireta.Text) : 0;
            //    equipe.NivelFundamental = !String.IsNullOrEmpty(txtFundamentalRedeDireta.Text) ? Convert.ToInt32(txtFundamentalRedeDireta.Text) : 0;
            //    equipe.NivelMedio = !String.IsNullOrEmpty(txtMedioRedeDireta.Text) ? Convert.ToInt32(txtMedioRedeDireta.Text) : 0;
            //    equipe.NivelSuperior = !String.IsNullOrEmpty(txtSuperiorRedeDireta.Text) ? Convert.ToInt32(txtSuperiorRedeDireta.Text) : 0;
            //    //Exercicio
            //    equipe.Exercicio = orgao.Exercicio; //por ano
            //    equipe.Desbloqueado = true;
            //    orgao.EquipesEspecificas.Add(equipe);
            //}

            if (equipeEspecificaTotaisInfo.PossuiOutrasEquipes.Value == 1)
            {
                EquipeEspecificaInfo equipe = new EquipeEspecificaInfo();
                equipe.IdOrgaoGestor = orgao.Id;
                equipe.IdPrefeitura = orgao.IdPrefeitura;
                equipe.IdTipoEquipe = 10;
                equipe.SemEscolaridade = !String.IsNullOrEmpty(txtEscolarizacaoOutraEquipe.Text) ? Convert.ToInt32(txtEscolarizacaoOutraEquipe.Text) : 0;
                equipe.NivelFundamental = !String.IsNullOrEmpty(txtFundamentalOutraEquipe.Text) ? Convert.ToInt32(txtFundamentalOutraEquipe.Text) : 0;
                equipe.NivelMedio = !String.IsNullOrEmpty(txtMedioOutraEquipe.Text) ? Convert.ToInt32(txtMedioOutraEquipe.Text) : 0;
                equipe.NivelSuperior = !String.IsNullOrEmpty(txtSuperiorOutraEquipe.Text) ? Convert.ToInt32(txtSuperiorOutraEquipe.Text) : 0;
                //Exercicio
                equipe.Exercicio = orgao.Exercicio; //por ano
                equipe.Desbloqueado = true;
                orgao.EquipesEspecificas.Add(equipe);
            }

            orgao.EquipesEspecificasTotais.Add(equipeEspecificaTotaisInfo);

            #endregion

            #region equipes serao estruturadas (próximos anos)?
            //var intencaoPorExercicio = orgao.IntencoesEstruturacaoEquipe.Where(x => x.Exercicio == Convert.ToInt32(hdfAno.Value)).FirstOrDefault();
            IntencaoEstruturacaoEquipeInfo intencaoEstruturacaoEquipeInfo = new IntencaoEstruturacaoEquipeInfo();
            intencaoEstruturacaoEquipeInfo.IdOrgaoGestor = Convert.ToInt32(hdfIdOrgaoGestor.Value);
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoEquipeBasica = rblEstruturacaoBasica.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoEquipeEspecial = rblEstruturacaoEspecial.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoEquipeVigilanciaSocioAssistencial = rblEstruturacaSocioassistencial.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoEquipeGestorSUAS = rblGestaoDoSuas.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoEquipeGestaoBeneficios = rblEstuturacaoTransferencia.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoEquipeGestaoCadUnico = rblEstruturacaoCadUnico.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoEquipeGestaoFinanceira = rblEstruturarGestaoFinanceira.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoEquipeGestaoSUAS = rblEstruturarGestaoSuas.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoEquipeRegulacaoSUAS = rblEstruturarRegulacaoSUAS.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoEquipeRedeDireta = rblEstruturarRedeDireta.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoOutraEquipe = rblEstruturarOutraEquipe.SelectedValue == "1";
            intencaoEstruturacaoEquipeInfo.IntencaoAcaoOrgaoGestor = rblAumentarEquipe.SelectedValue == "1";
            //Exercicio
            intencaoEstruturacaoEquipeInfo.Exercicio = orgao.Exercicio;


            orgao.IntencoesEstruturacaoEquipe = new List<IntencaoEstruturacaoEquipeInfo>();
            orgao.IntencoesEstruturacaoEquipe.Add(intencaoEstruturacaoEquipeInfo);
            #endregion

            #region validar/atualizar
            try
            {
                new ValidadorOrgaoGestor().ValidarOrgaoExercicio(orgao);

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    prefeituras.SaveOrgaoGestor(orgao);

                    load(prefeituras);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                CalcularTotais();
            }
            #endregion

            #region tratamento inconsistencias
            if (String.IsNullOrEmpty(msg))
            {
                msg = orgao.Id == 0 ? "Dados do Órgão Gestor registrado com sucesso!" : "Dados do Órgão Gestor atualizados com sucesso!";
                msg += "<br/>O campo Nome do Órgão Gestor será exibido no cabeçalho do plano impresso.";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                fraOrgaoGestor.Attributes.Add("class", "frame");
                fraGestor.Attributes.Add("class", "frame");
                fraRH.Attributes.Add("class", "frame");
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                return;
            }
            #endregion

            #region tratamento abas
            fraOrgaoGestor.Attributes.Add("class", "frame");
            fraGestor.Attributes.Add("class", "frame");
            fraRH.Attributes.Add("class", "frame active");
            #endregion

            #region exibicao inconsistencias/sucesso
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
            //this.Master.ScriptManagerControl.SetFocus(lblInconsistencias);
            #endregion
        }

        private OrgaoGestorInfo ObterIdentificacaoOrgaoGestor()
        {

            var orgao = new OrgaoGestorInfo();
            orgao.Exercicio = Convert.ToInt32(hdfAno.Value);
            orgao.Id = Convert.ToInt32(hdfIdOrgaoGestor.Value);
            orgao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            orgao.CNPJ = txtCNPJ.Text;
            orgao.Nome = txtNome.Text;
            orgao.IdEstrutura = Convert.ToInt16(ddlEstruturaOrgaoGestor.SelectedValue);
            if (orgao.IdEstrutura == 7)
            {
                orgao.EstruturaOutros = txtOutroOrgaoGestor.Text;
            }
            orgao.Telefone = telefone.Text.Trim();
            orgao.Celular = celular.Text.Trim();
            orgao.Logradouro = cep1.Txtendereco;
            orgao.Numero = cep1.Txtnumero;
            orgao.Bairro = cep1.Txtbairro;
            orgao.CEP = cep1.Txtcep;
            orgao.Complemento = cep1.Txtcomplemento;
            orgao.Cidade = cep1.Txtcidade;
            orgao.Email = txtEmail.Text;

            return orgao;
        }

        #endregion


        #region CRUD: [1.5 - Identificacao "Gestor municipal de AS" ]
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            EditarCampos();
        }
        protected void btnSalvarGestor_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var gestor = new GestorMunicipalInfo();
            gestor.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            gestor.Id = (hdfIdGestor.Value == "0") ? 0 : Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdGestor.Value));

            if (!String.IsNullOrEmpty(ddlUsuario.SelectedValue) && ddlUsuario.SelectedValue != "0")
            {
                gestor.IdUsuarioGestor = Convert.ToInt32(ddlUsuario.SelectedValue);
                gestor.Nome = ddlUsuario.SelectedItem.Text;
            }

            gestor.IdCargo = Convert.ToInt16(ddlCargo.SelectedValue);
            if (gestor.IdCargo == 7)
                gestor.OutroCargo = txtCargoOutro.Text;

            DateTime dt;
            if (!String.IsNullOrEmpty(txtdata.Text) && DateTime.TryParse(txtdata.Text, out dt))
                gestor.DataNomeacao = Convert.ToDateTime(txtdata.Text);

            gestor.IdEscolaridade = Convert.ToInt32(ddlEscolaridade.SelectedValue);
            if (gestor.IdEscolaridade == 4)
            {
                gestor.IdFormacao = Convert.ToInt32(ddlFormacaoAcademica.SelectedValue);
                if (gestor.IdFormacao == 7)
                    gestor.OutraFormacao = txtOutraAreaFormacao.Text;
            }
            if (!String.IsNullOrEmpty(txtCPF.Text))
                gestor.CPF = txtCPF.Text;

            gestor.Telefone = txtTelefone.Text;
            gestor.Celular = txtCelular.Text;

            if (!String.IsNullOrEmpty(txtEmailGestor.Text))
                gestor.Email = txtEmailGestor.Text;
            if (!String.IsNullOrEmpty(txtRG.Txtrg.ToString()))
                gestor.RG = txtRG.Txtrg;
            if (!String.IsNullOrEmpty(txtRG.Txtdigito.ToString()))
                gestor.RGDigito = txtRG.Txtdigito;
            if (!String.IsNullOrEmpty(txtOrgEmissor.Text))
                gestor.Emissor = txtOrgEmissor.Text;
            if (ddlUFEmissor.SelectedValue != "0")
                gestor.UFRG = Convert.ToInt16(ddlUFEmissor.SelectedValue);
            if (!String.IsNullOrEmpty(txtDataEmissao.Text))
                gestor.DataEmissao = Convert.ToDateTime(txtDataEmissao.Text);



            if (!String.IsNullOrEmpty(txtDecretoPortariaGestor.Text))
                gestor.NumeroDecreto = txtDecretoPortariaGestor.Text;

            if (!String.IsNullOrEmpty(txtDataDecretoGestor.Text))
                gestor.DataDecreto = Convert.ToDateTime(txtDataDecretoGestor.Text);
            //if (!String.IsNullOrEmpty(txtNumeroLeiCriacaoGestor.Text) && !String.IsNullOrEmpty(txtAnoLeiCriacaoGestor.Text))
            //    gestor.Lei = txtNumeroLeiCriacaoGestor.Text + "/" + txtAnoLeiCriacaoGestor.Text;
            //if (!String.IsNullOrEmpty(txtPublicacaoLeiGestor.Text) && DateTime.TryParse(txtPublicacaoLeiGestor.Text, out dt))
            //    gestor.DataPublicacao = Convert.ToDateTime(txtPublicacaoLeiGestor.Text);


            gestor.IdStatus = 1;

            String msg = String.Empty;
            try
            {
                new ValidadorGestorMunicipal().Validar(gestor);

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    foreach (var anterior in prefeituras.GetGestoresAnteriores(SessaoPmas.UsuarioLogado.Prefeitura.Id).OrderByDescending(t => t.DataNomeacao))
                    {
                        if ((anterior.DataNomeacao > Convert.ToDateTime(txtdata.Text))
                            || (anterior.DataTerminoGestao > Convert.ToDateTime(txtdata.Text)))
                        {
                            throw new Exception("O período de mandato do gestor anterior não pode ser superior ao período de mandato do gestor atual!");
                        }
                    }

                    if (gestor.Id == 0)
                    {
                        hdfIdGestor.Value = Genericos.clsCrypto.Encrypt(prefeituras.AddGestor(gestor).ToString());
                        msg = "Dados do Gestor Municipal registrado com sucesso!";
                        load(prefeituras);
                    }
                    else
                    {
                        prefeituras.UpdateGestor(gestor);
                        msg = "Dados do Gestor Municipal atualizados com sucesso!";
                    }

                    InibirCampos();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>")), true);
                lblInconsistenciasGestor.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistenciasGestor.Visible = true;
                return;
            }
            lblInconsistenciasGestor.Text = "";
            tbInconsistenciasGestor.Visible = false;
            fraOrgaoGestor.Attributes.Add("class", "frame");
            fraGestor.Attributes.Add("class", "frame");
            fraRH.Attributes.Add("class", "frame");
            var script = Util.GetJavaScriptDialogOK(msg);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
        #endregion


        #region helpers
        private void ExibirCampos()
        {
            ddlUsuario.Enabled = true;
            ddlCargo.Enabled = true;
            txtCargoOutro.Enabled = true;
            txtdata.Enabled = true;
            txtEmailGestor.Enabled = true;
            ddlEscolaridade.Enabled = true;
            ddlFormacaoAcademica.Enabled = true;
            txtOutraAreaFormacao.Enabled = true;
            txtTelefone.Enabled = true;
            txtCelular.Enabled = true;
            //lblEspecificarOutros.Enabled = true;
            btnSalvarGestor.Enabled = true;
            btnEditar.Enabled = false;
            btnSubstituir.Enabled = false;
        }
        private void InibirCampos()
        {
            txtCPF.Enabled = false;
            txtRG.Enabled = false;
            txtDataEmissao.Enabled = false;
            txtOrgEmissor.Enabled = false;
            ddlUFEmissor.Enabled = false;
            txtEmailGestor.Enabled = false;
            ddlUsuario.Enabled = false;
            ddlCargo.Enabled = false;
            txtCargoOutro.Enabled = false;
            txtdata.Enabled = false;
            ddlEscolaridade.Enabled = false;
            ddlFormacaoAcademica.Enabled = false;
            txtOutraAreaFormacao.Enabled = false;
            txtTelefone.Enabled = false;
            txtCelular.Enabled = false;
            //txtPublicacaoLeiGestor.Enabled = false;
            txtDecretoPortariaGestor.Enabled = false;
            //txtNumeroLeiCriacaoGestor.Enabled = false;
            //txtAnoLeiCriacaoGestor.Enabled = false;
            txtDataDecretoGestor.Enabled = false;
           //lblEspecificarOutros.Enabled = false;
            btnSalvarGestor.Enabled = false;
            btnEditar.Enabled = true;
            btnSubstituir.Enabled = true;

        }
        private void EditarCampos()
        {
            ddlUsuario.Enabled = false;
            ddlCargo.Enabled =
            txtCargoOutro.Enabled =
            txtdata.Enabled =
            txtEmailGestor.Enabled =
            ddlEscolaridade.Enabled =
            ddlFormacaoAcademica.Enabled =
            txtOutraAreaFormacao.Enabled =
            txtTelefone.Enabled =
            txtCelular.Enabled =
            txtRG.Enabled =
            txtDataEmissao.Enabled =
            txtOrgEmissor.Enabled =
            ddlUFEmissor.Enabled =
                //txtPublicacaoLeiGestor.Enabled =
             txtDecretoPortariaGestor.Enabled =
                //txtNumeroLeiCriacaoGestor.Enabled =
                //txtAnoLeiCriacaoGestor.Enabled =
            txtDataDecretoGestor.Enabled =
            btnSalvarGestor.Enabled =
            btnSalvarGestor.Enabled = true;
            btnEditar.Enabled = false;
            btnSubstituir.Enabled = false;
            fraGestor.Attributes.Add("Class", "frame active");
            fraRH.Attributes.Add("Class", "frame");
            fraOrgaoGestor.Attributes.Add("Class", "frame");
        }
        private void AbrirCampos()
        {
            ddlUsuario.Enabled =
            ddlCargo.Enabled =
            txtCargoOutro.Enabled =
            txtdata.Enabled =
            txtEmailGestor.Enabled =
            ddlEscolaridade.Enabled =
            ddlFormacaoAcademica.Enabled =
            txtOutraAreaFormacao.Enabled =
            txtTelefone.Enabled =
            txtCelular.Enabled =
            txtRG.Enabled =
            txtCPF.Enabled =
            txtDataEmissao.Enabled =
            txtOrgEmissor.Enabled =
            ddlUFEmissor.Enabled =
            btnSalvarGestor.Enabled = true;
            btnEditar.Enabled = false;
            btnSubstituir.Enabled = false;
            fraGestor.Attributes.Add("Class", "frame active");
            fraRH.Attributes.Add("Class", "frame");
            fraOrgaoGestor.Attributes.Add("Class", "frame");
        }



        void Clear()
        {
            #region Indique o tipo de vínculo dos trabalhadores
            txtEstatutarios.Text = "0";
            txtCeletistas.Text = "0";
            txtEstagiarios.Text = "0";
            txtComissionados.Text = "0";
            txtOutrosVinculos.Text = "0";
            txtVoluntarios.Text = "0";
            #endregion

            #region Area de formacao: Indique a área de formação dos trabalhadores que possuem nível superior
            txtSuperiorServicoSocial.Text = "0";
            txtSuperiorPsicologia.Text = "0";
            txtSuperiorPedagogia.Text = "0";
            txtSociologia.Text = "0";
            txtDireito.Text = "0";
            txtSuperiorAdministracao.Text = "0";
            txtSuperiorAntropologia.Text = "0";
            txtSuperiorContabilidade.Text = "0";
            txtSuperiorEconomia.Text = "0";
            txtSuperiorEconomiaDomestica.Text = "0";
            txtSuperiorTerapiaOcupacional.Text = "0";
            txtOutros.Text = "0";
            #endregion

            #region equipe/estruturacao da equipe

            rblEquipeBasica.SelectedValue = "0";
            
            rblEquipeEspecial.SelectedValue = "0";
            
            rblEquipeSocioassistencial.SelectedValue = "0";
            
            rblGestaoSuas.SelectedValue = "0";
            
            rdlEquipeTransferencia.SelectedValue = "0";
            
            rblEquipeCadUnico.SelectedValue = "0";
            
            rblEquipeGestaoFinanceira.SelectedValue = "0";
            
            rblEquipeGestaoSuas.SelectedValue = "0";
            
            rblEquipeRegulacaoSUAS.SelectedValue = "0";
            
           //rblEquipeRedeDireta.SelectedValue = "0";

            rblOutrasEquipes.SelectedValue = "0";

            rblEstruturacaoBasica.SelectedValue = "0";

            rblEstruturacaoEspecial.SelectedValue = "0";

            rblEstruturacaSocioassistencial.SelectedValue = "0";

            rblGestaoDoSuas.SelectedValue = "0";

            rblEstuturacaoTransferencia.SelectedValue = "0";

            rblEstruturacaoCadUnico.SelectedValue = "0";

            rblEstruturarGestaoFinanceira.SelectedValue = "0";

            rblEstruturarGestaoSuas.SelectedValue = "0";

            rblEstruturarRegulacaoSUAS.SelectedValue = "0";

            rblEstruturarRedeDireta.SelectedValue = "0";

            rblEstruturarOutraEquipe.SelectedValue = "0";

            #region Existe intenção de aumentar o número de trabalhadores do órgão gestor nos próximos anos?
            rblAumentarEquipe.SelectedValue = "0";
            #endregion

            #endregion

            #region equipes especificas (sim/nao e valores)
            txtEscolarizacaoBasica.Text = "0";
            txtFundamentalBasica.Text = "0";
            txtMedioBasica.Text = "0";
            txtSuperiorBasica.Text = "0";
            lblTotalBasica.Text = "0";


            txtEscolarizacaoEspecial.Text = "0";
            txtFundamentalEspecial.Text = "0";
            txtMedioEspecial.Text = "0";
            txtSuperiorEspecial.Text = "0";
            lblTotalEspecial.Text = "0";


            txtEscolarizacaoSocioassistencial.Text = "0";
            txtFundamentalSocioassistencial.Text = "0";
            txtMedioSocioassistencial.Text = "0";
            txtSuperiorSocioassistencial.Text = "0";
            lblTotalSocioassistencial.Text = "0";

            txtEscolarizacaoGestaoSuas.Text = "0";
            txtFundamentalGestaoSuas.Text = "0";
            txtMedioGestaoSuas.Text = "0";
            txtSuperiorGestaoSuas.Text = "0";
            lblTotalGestaoSuas.Text = "0";

            txtEscolarizacaoTransferencia.Text = "0";
            txtFundamentalTransferencia.Text = "0";
            txtMedioTransferencia.Text = "0";
            txtSuperiorTransferencia.Text = "0";
            lblTotalTransferencia.Text = "0";

            txtEscolarizacaoCadUnico.Text = "0";
            txtFundamentalCadUnico.Text = "0";
            txtMedioCadUnico.Text = "0";
            txtSuperiorCadUnico.Text = "0";
            lblTotalCadUnico.Text = "0";

            txtEscolarizacaoGestaoFinanceira.Text = "0";
            txtFundamentalGestaoFinanceira.Text = "0";
            txtMedioGestaoFinanceira.Text = "0";
            txtSuperiorGestaoFinanceira.Text = "0";
            lblTotalGestaoFinanceira.Text = "0";

            txtEscolarizacaoSUAS.Text = "0";
            txtFundamentalSUAS.Text = "0";
            txtMedioSUAS.Text = "0";
            txtSuperiorSUAS.Text = "0";
            lblTotalSUAS.Text = "0";

            txtEscolarizacaoRegulacaoSUAS.Text = "0";
            txtFundamentalRegulacaoSUAS.Text = "0";
            txtMedioRegulacaoSUAS.Text = "0";
            txtSuperiorRegulacaoSUAS.Text = "0";
            lblTotalRegulacaoSUAS.Text = "0";

            //txtEscolarizacaoRedeDireta.Text = "0";
            //txtFundamentalRedeDireta.Text = "0";
            //txtMedioRedeDireta.Text = "0";
            //txtSuperiorRedeDireta.Text = "0";
            //lblTotalRedeDireta.Text = "0";

            txtEscolarizacaoOutraEquipe.Text = "0";
            txtFundamentalOutraEquipe.Text = "0";
            txtMedioOutraEquipe.Text = "0";
            txtSuperiorOutraEquipe.Text = "0";
            lblTotalOutraEquipe.Text = "0";

            lblTotalEscolarizacao.Text = "0";
            lblTotalFundamental.Text = "0";
            lblTotalMedio.Text = "0";
            lblTotalSuperior.Text = "0";
            lblTotal.Text = "0,0";
            #endregion
            lblTotalEscolarizacao.Text = "0";
            lblTotalFundamental.Text = "0";
            lblTotalMedio.Text = "0";
            lblTotalSuperior.Text = "0";
            lblTotal.Text = "0";
        }

        #region controles
        public WebControl[] ObterControlesNaoBaseadoEmExercicio()
        {
            WebControl[] controles = {   txtNome , 
                                             ddlEstruturaOrgaoGestor , 
                                             txtEmail , 
                                             txtSite , 
                                             chkPossuiSite ,
                                             //txtNumeroDecreto,
                                             txtNumeroLeiCriacaoOrgaoGestor,
                                             txtAnoLeiCriacaoOrgaoGestor ,                                              
                                             rblAlteracaoLei ,
                                             rblLeiDoSuas,
                                             txtNumeroLei ,                                      
                                             ddlUsuario, 
                                             ddlCargo, 
                                             txtCargoOutro ,                                          
                                             ddlFormacaoAcademica , 
                                             ddlEscolaridade,                                        
                                             txtEmailGestor ,
                                             ddlUFEmissor,
                                             txtOrgEmissor,
                                             btnSalvarGestor , 
                                             btnSubstituir ,
                                             btnEditar,
                                             lstGestores,
                                             txtDecretoPortariaGestor,
                                             txtAnoLei
                                             //txtNumeroLeiCriacaoGestor,
                                             //txtAnoLeiCriacaoGestor,
                                             };
            return controles;
        }
        private WebControl[] ObterControlesEstruturasRhExercicio()
        {
            WebControl[] controlesExercicio = {
                                             //rblEquipeRedeDireta,
                                             rblEstruturarRedeDireta,
                                             rblEquipeBasica,
                                             rblEstruturacaoBasica,
                                             rblEquipeEspecial,
                                             rblEstruturacaoEspecial,
                                             rblEquipeSocioassistencial,
                                             rblEstruturacaSocioassistencial,
                                             rblGestaoSuas,
                                             rblGestaoDoSuas,
                                             rdlEquipeTransferencia,
                                             rblEstuturacaoTransferencia,
                                             rblEquipeCadUnico,
                                             rblEstruturacaoCadUnico,
                                             rblEquipeGestaoFinanceira,
                                             rblEstruturarGestaoFinanceira,
                                             rblEquipeGestaoSuas,
                                             rblEstruturarGestaoSuas,
                                             rblEquipeRegulacaoSUAS,
                                             rblEstruturarRegulacaoSUAS,
                                             rblOutrasEquipes,
                                             rblAumentarEquipe,
                                             txtEscolarizacaoBasica,
                                             txtFundamentalBasica,
                                             txtMedioBasica,
                                             txtSuperiorBasica,
                                             txtEscolarizacaoEspecial,
                                             txtEscolarizacaoGestaoSuas,
                                             txtFundamentalGestaoSuas,
                                             txtMedioGestaoSuas,
                                             txtSuperiorGestaoSuas,
                                             txtFundamentalEspecial,
                                             txtMedioEspecial,
                                             txtSuperiorEspecial,
                                             txtEscolarizacaoSocioassistencial,
                                             txtFundamentalSocioassistencial,
                                             txtMedioSocioassistencial,
                                             txtSuperiorSocioassistencial,
                                             txtEscolarizacaoTransferencia,
                                             txtFundamentalTransferencia,
                                             txtMedioTransferencia,
                                             txtSuperiorTransferencia,
                                             txtEscolarizacaoCadUnico,
                                             txtFundamentalCadUnico,
                                             txtMedioCadUnico,
                                             txtSuperiorCadUnico,
                                             txtEscolarizacaoGestaoFinanceira,
                                             txtFundamentalGestaoFinanceira,
                                             txtMedioGestaoFinanceira,
                                             txtSuperiorGestaoFinanceira,
                                             txtEscolarizacaoSUAS,
                                             txtFundamentalSUAS,
                                             txtMedioSUAS,
                                             txtSuperiorSUAS,
                                             txtEscolarizacaoRegulacaoSUAS,
                                             txtFundamentalRegulacaoSUAS,
                                             txtMedioRegulacaoSUAS,
                                             txtSuperiorRegulacaoSUAS,
                                             //txtEscolarizacaoRedeDireta,
                                             //txtFundamentalRedeDireta,
                                             //txtMedioRedeDireta,
                                             //txtSuperiorRedeDireta,
                                             txtEscolarizacaoOutraEquipe,
                                             txtFundamentalOutraEquipe,
                                             txtMedioOutraEquipe,
                                             txtSuperiorOutraEquipe,
                                             txtSuperiorServicoSocial ,
                                             txtSuperiorPsicologia ,
                                             txtSuperiorPedagogia ,
                                             txtSociologia , 
                                             txtDireito ,
                                             txtSuperiorAdministracao,
                                             txtSuperiorAntropologia,
                                             txtSuperiorContabilidade,
                                             txtSuperiorEconomia,
                                             txtSuperiorEconomiaDomestica,
                                             txtSuperiorTerapiaOcupacional,
                                             txtOutros,
                                             txtEstagiarios,
                                             txtEstatutarios,
                                             txtCeletistas,
                                             txtComissionados,
                                             txtOutrosVinculos,
                                             txtVoluntarios,
                                             txtSociologia ,
                                             txtDireito ,
                                             btnSalvar,

                                         };
            return controlesExercicio;
        }
        #endregion

        #region Orgao Gestor: Aplicar: [Bloqueio]
        
        private void AplicarBloqueioDesbloqueio()
        {
            var exercicio = Convert.ToInt32(hdfAno.Value);
            WebControl[] controlesExercicio = ObterControlesEstruturasRhExercicio();
            Permissao.BlocoI.VerificaPermissaoExercicioBlocoI(controlesExercicio, exercicio);
        }
        #endregion

        #region regra de tela
        private void AplicarBloqueioCamposPorRadioButtonSelecionado()
        {
            //rblEquipeCadUnico 
            //rblEquipeRedeDireta 
            //rblOutrasEquipes

            rblProtecaoBasica_SelectedIndexChanged(null, null);
            rblProtecaoEspecial_SelectedIndexChanged(null, null);
            rblVigilanciaSocioassistencial_SelectedIndexChanged(null, null);
            rblEquipeGestaoBeneficios_SelectedIndexChanged(null, null);
            rblEquipeBasica_SelectedIndexChanged(null, null);
            rblEquipeEspecial_SelectedIndexChanged(null, null);
            rdlEquipeTransferencia_SelectedIndexChanged(null, null);
            rblEquipeCadUnico_SelectedIndexChanged(null, null);
            rblEquipeGestaoFinanceira_SelectedIndexChanged(null, null);
            rblEquipeGestaoSuas_SelectedIndexChanged(null, null);
            rblEquipeSocioassistencial_SelectedIndexChanged(null, null);
            rblEquipeRegulacaoSUAS_SelectedIndexChanged(null, null);
            //rblEquipeRedeDireta_SelectedIndexChanged(null, null);
            rblOutrasEquipes_SelectedIndexChanged(null, null);
            rblGestaoSuas_SelectedIndexChanged(null,null);

        }
        #endregion

        protected void rblLeiDoSuas_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            fraOrgaoGestor.Attributes.Add("Class", "frame active");

            tdDataLeiSuas.Visible = tdLeiDoSuas.Visible = rblLeiDoSuas.SelectedValue == "1";
            if (rblLeiDoSuas.SelectedValue == "0")
            {
                txtNumeroLeiSuas.Text = string.Empty;
                txtDataPublicacaoLei.Text = string.Empty;
                //this.Master.ScriptManagerControl.SetFocus(txtSemEscolaridade);
                return;
            }
            this.Master.ScriptManagerControl.SetFocus(txtNumeroLeiSuas);
        }

        #endregion






        internal static void AcessaGestorAC()
        {
            throw new NotImplementedException();
        }
    }
}

