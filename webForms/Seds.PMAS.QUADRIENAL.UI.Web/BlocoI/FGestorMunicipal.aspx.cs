using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoI
{
    public partial class FGestorMunicipal : System.Web.UI.Page
    {
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
                carregarUsuarios();
                carregarCombos();
                using (var proxy = new ProxyPrefeitura())
                {
                    load(new Prefeituras(proxy));
                }

                verificarAlteracoes();

            }
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro7.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 7);
                    linkAlteracoesQuadro7.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("7"));
                    linkAlteracoesQuadro8.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 8);
                    linkAlteracoesQuadro8.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("8"));
                }
            }
        }
        
        void load(Prefeituras prefeituras)
        {
            var gestor = prefeituras.GetAtualGestorMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            carregarGestoresAnteriores(prefeituras);

            if (gestor != null)
            {
                hdfIdGestor.Value = gestor.Id.ToString();

                if(gestor.IdUsuarioGestor.HasValue)
                    ddlUsuario.SelectedValue = "teste";
                
                txtdata.Text = gestor.DataNomeacao.ToShortDateString();
                
                ddlCargo.SelectedValue = gestor.IdCargo.ToString();
                //Outro Cargo
                if(gestor.IdCargo == 7)
                    txtCargoOutro.Text = gestor.OutroCargo;

                ddlEscolaridade.SelectedValue = gestor.IdEscolaridade.ToString();
                tdFormacaoAcademica.Visible = gestor.IdEscolaridade == 4;
                if (gestor.IdEscolaridade == 4)
                {
                    ddlFormacaoAcademica.SelectedValue = gestor.IdFormacao.ToString();
                    //Outra Formação
                    if (gestor.IdFormacao == 7)
                        txtOutraAreaFormacao.Text = gestor.OutraFormacao;
                }

                txtTelefone.Text = gestor.Telefone;
                txtCelular.Text = gestor.Celular;
                txtEmail.Text = gestor.Email;

                InibirCampos();
            }
            else
            {
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
                                         btnSalvar , 
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

        void carregarUsuarios()
        {
            Int32? idMunicipio = SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio;
            Int32? idPerfil = (Int32)EPerfil.OrgaoGestor;
            using (var proxy = new ProxyUsuarioPMAS())
            {
                ddlUsuario.DataTextField = "Nome";
                ddlUsuario.DataValueField = "IdUsuario";
                ddlUsuario.DataSource = new Usuarios().GetConsultaUsuariosCadastrados("", "",null , idPerfil, idMunicipio, "", proxy).OrderBy(u=> u.Nome);
                ddlUsuario.DataBind();
                ddlUsuario.Items.Insert(0, new ListItem("[Indique o nome do gestor]", "0"));
            }
        }

        void carregarCombos()
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
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
                ddlFormacaoAcademica.DataSource = proxy.Service.GetFormacoesAcademicas().OrderBy(f=> f.Ordem);
                ddlFormacaoAcademica.DataBind();
                ddlFormacaoAcademica.Items.Insert(0, new ListItem("[Escolha uma Opção]", "0"));
            }
        }

        void carregarGestoresAnteriores(Prefeituras prefeituras)
        {
            lstGestores.DataSource = prefeituras.GetGestoresAnteriores(SessaoPmas.UsuarioLogado.Prefeitura.Id).OrderByDescending(t=> t.DataNomeacao);
            lstGestores.DataBind();
        }

        protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trOutros.Visible = ddlCargo.SelectedValue == "7";
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

        private void ExibirCampos()
        {
            ddlUsuario.Enabled = true;
            ddlCargo.Enabled = true;
            txtCargoOutro.Enabled = true;
            txtdata.Enabled = true;
            txtEmail.Enabled = true;
            ddlEscolaridade.Enabled = true;
            ddlFormacaoAcademica.Enabled = true;
            txtOutraAreaFormacao.Enabled = true;
            txtTelefone.Enabled = true;
            txtCelular.Enabled = true;

            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnSubstituir.Enabled = false;
        }

        private void InibirCampos()
        {
            ddlUsuario.Enabled = false;
            ddlCargo.Enabled = false;
            txtCargoOutro.Enabled = false;
            txtdata.Enabled = false;
            txtEmail.Enabled = false;
            ddlEscolaridade.Enabled = false;
            ddlFormacaoAcademica.Enabled = false;
            txtOutraAreaFormacao.Enabled = false;
            txtTelefone.Enabled = false;
            txtCelular.Enabled = false;

            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            btnSubstituir.Enabled = true;
        }

        protected void btnSubstituir_Click(object sender, EventArgs e)
        {
            trDataTerminoGestao.Visible = true;
            var script = Util.GetJavaScriptDialogWarning("Para finalizar a substituição do novo gestor, preencha o campo Data final da gestão.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        protected void btnSalvarTerminoGestao_Click(object sender, EventArgs e)  
        {
            SessaoPmas.VerificarSessao(this);

            if (Convert.ToDateTime(txtDataTerminoGestao.Text) > DateTime.Today)
            {
                lblInconsistencias.Text = "A data de final da gestão não pode ser posterior à data atual!<br />";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError( lblInconsistencias.Text), true);
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

                ddlCargo.SelectedIndex = 0;
                ddlCargo_SelectedIndexChanged(null, null);
                ddlFormacaoAcademica.SelectedIndex = 0;
                ddlFormacaoProfissional_SelectedIndexChanged(null, null);
                ddlEscolaridade.SelectedIndex = 0;
                ddlEscolaridade_SelectedIndexChanged(null, null);

                txtTelefone.Text = string.Empty;
                txtCelular.Text = string.Empty;
                txtEmail.Text = string.Empty;

                ExibirCampos();

                trDataTerminoGestao.Visible = false;
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            EditarCampos();
            //this.Master.ScriptManagerControl.SetFocus(ddlUsuario);
            //ddlUsuario.Enabled = false;
        }

        private void EditarCampos()
        {
            ddlUsuario.Enabled = false;
            ddlCargo.Enabled = true;
            txtCargoOutro.Enabled = true;
            txtdata.Enabled = true;
            txtEmail.Enabled = true;
            ddlEscolaridade.Enabled = true;
            ddlFormacaoAcademica.Enabled = true;
            txtOutraAreaFormacao.Enabled = true;
            txtTelefone.Enabled = true;
            txtCelular.Enabled = true;

            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnSubstituir.Enabled = false;
        }

        protected void btnSalvarGestor_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var gestor = new GestorMunicipalInfo();
            gestor.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            gestor.Id = Convert.ToInt32(hdfIdGestor.Value);

            if (!String.IsNullOrEmpty(ddlUsuario.SelectedValue) && ddlUsuario.SelectedValue != "0")
            {
                gestor.IdUsuarioGestor = Convert.ToInt32(ddlUsuario.SelectedValue);
                gestor.Nome = ddlUsuario.SelectedItem.Text;
            }
                         
            gestor.IdCargo = Convert.ToInt16(ddlCargo.SelectedValue);
            if(gestor.IdCargo == 7)
                gestor.OutroCargo = txtCargoOutro.Text;
            
            DateTime dt;
            if(!String.IsNullOrEmpty(txtdata.Text) && DateTime.TryParse(txtdata.Text,out dt))
                gestor.DataNomeacao = Convert.ToDateTime(txtdata.Text);

            gestor.IdEscolaridade = Convert.ToInt32(ddlEscolaridade.SelectedValue);
            if (gestor.IdEscolaridade == 4)
            {
                gestor.IdFormacao = Convert.ToInt32(ddlFormacaoAcademica.SelectedValue);
                if (gestor.IdFormacao == 7)
                    gestor.OutraFormacao = txtOutraAreaFormacao.Text;
            }
            
            gestor.Telefone = txtTelefone.Text;
            gestor.Celular = txtCelular.Text;
            gestor.Email = txtEmail.Text;

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
                        prefeituras.AddGestor(gestor);
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
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }


            lblInconsistencias.Text = "";
            tbInconsistencias.Visible = false;
            var script = Util.GetJavaScriptDialogOK(msg);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true); 
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
    }
}