using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoI
{
    public partial class FPrefeitura : System.Web.UI.Page
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

                using (var proxy = new ProxyPrefeitura())
                {
                    load(proxy);
                    loadPrefeito(proxy);
                }
            }
            //else
            //{
            Button btn = (Button)cep1.FindControl("cmdPesqCEP");
            btn.Click += new EventHandler(this.CEPBtn_Click);
            //}
        }

        private void CEPBtn_Click(object sender, EventArgs e)
        {
            fraPrefeitura.Attributes.Add("class", "frame active");
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    alteracoesQuadro.Visible = linkAlteracoes.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 1);
                    linkAlteracoes.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("1"));

                    alteracoesQuadro2.Visible = linkAlteracoesQuadro2.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 2);
                    linkAlteracoesQuadro2.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("2"));
                }
            }
        }

        void load(ProxyPrefeitura proxy)
        {
            PrefeituraInfo pre;

            var blocoI = new Seds.PMAS.QUADRIENAL.UI.Processos.Prefeituras(proxy);
            pre = blocoI.GetPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            if (pre == null)
                return;
            txtMunicipio.Text = pre.Municipio.Nome;
            txtNHabitantes.Text = String.Format("{0:0,0}", pre.Populacao);
            txtEmail.Text = pre.Email;
            txtDrads.Text = pre.Municipio.Drads.Nome;
            txtEmail.Text = pre.Email;
            chkPossuiSite.Checked = !pre.PossuiSite;
            txtSite.Enabled = pre.PossuiSite;
            if (pre.PossuiSite)
                txtSite.Text = pre.WebSite;
            lblCNPJ.Text = Util.FormatarCNPJ(pre.CNPJ);
            txtDataPublicacao.Text = pre.DataPublicacao.ToString("dd/MM/yyyy") == "01/01/2000" ? "" : pre.DataPublicacao.ToShortDateString();
            rblGestao.SelectedValue = pre.IdNivelGestao.ToString();
            cep1.Txtcep = pre.CEP;
            cep1.Txtendereco = pre.Logradouro;
            cep1.Txtbairro = pre.Bairro;
            cep1.Txtcomplemento = pre.Complemento;
            cep1.Txtcidade = pre.Cidade;
            cep1.Txtnumero = pre.Numero;
            telefone.Text = pre.Telefone;
            celular.Text = pre.Celular;
            if (pre.Populacao <= 20000)
                lblPorte.Text = "Pequeno I - Até 20.000";
            else if (pre.Populacao >= 20001 && pre.Populacao <= 50000)
                lblPorte.Text = "Pequeno II - de 20.001 a 50.000";
            else if (pre.Populacao >= 50001 && pre.Populacao <= 100000)
                lblPorte.Text = "Médio - de 50.001 a 100.000";
            else if (pre.Populacao >= 100001 && pre.Populacao <= 900000)
                lblPorte.Text = "Grande - de 100.001 a 900.000";
            else
                lblPorte.Text = "Metrópole - mais de 900.001";

            WebControl[] controles = { rblGestao, txtSite, chkPossuiSite, celular.DDD, celular.CELULAR, telefone.DDD, telefone.TELEFONE, txtEmail, btnSalvar };

            Permissao.VerificarPermissaoControles(controles, Session);
            Permissao.VerificarPermissaoControles(txtDataPublicacao.Controles, Session);

            if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador)
            {
                rblGestao.Enabled = true;
                txtDataPublicacao.ReadOnly = false;
                txtDataPublicacao.Enabled = true;
                btnSalvar.Enabled = true;

            }

            verificarAlteracoes();

        }

        void loadPrefeito(ProxyPrefeitura proxy)
        {
            ddlUFPrefeito.ClearSelection();

            var prefeituras = new Prefeituras(proxy);

            var prefeito = prefeituras.GetPrefeitoAtual(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (prefeito != null)
            {
                txtCPF.Text = Convert.ToUInt64(prefeito.CPF).ToString(@"000\.000\.000\-00"); 
                txtEmailPrefeito.Text = prefeito.Email;
                txtNome.Text = prefeito.Nome;
                txtOrgEmissor.Text = prefeito.SiglaEmissor;
                txtRG.Txtrg = prefeito.RG;
                txtRG.Txtdigito = prefeito.RGDigito;
                txtDataEmissao.Text = prefeito.RGDataEmissao.HasValue ? prefeito.RGDataEmissao.Value.ToShortDateString() : "";
                txtDataInicio.Text = prefeito.MandatoInicio.ToShortDateString();
                txtDataTermino.Text = prefeito.MandatoTerminio.ToShortDateString();
                ddlUFPrefeito.SelectedValue = prefeito.IdUFRG.ToString();
                hdfIdPrefeito.Value = prefeito.Id.ToString();
                telefonePrefeito.Text = prefeito.Telefone;
                celularPrefeito.Text = prefeito.Celular;
                InibirCampos();
            }
            else
                ExibirCampos();

            carregarPrefeitosAnteriores(prefeituras);

            WebControl[] controles = {lstPrefeitos,
                                          txtNome , 
                                          telefonePrefeito.DDD,
                                          telefonePrefeito.TELEFONE,
                                          celularPrefeito.DDD,
                                          celularPrefeito.CELULAR,
                                          ddlUFPrefeito , 
                                          txtOrgEmissor ,                                          
                                          txtEmailPrefeito,
                                          telefonePrefeito.DDD,
                                          telefonePrefeito.TELEFONE,
                                          celularPrefeito.DDD,
                                          celularPrefeito.CELULAR,
                                          btnSalvarPrefeito, 
                                          btnSubstituir,
                                         btnEditar
                                     };
            WebControl[] cpfControle = {
                               txtCPF
                               };

            Permissao.VerificarPermissaoControles(txtRG.Controles, Session);
            Permissao.VerificarPermissaoControles(cpfControle, Session);
            Permissao.VerificarPermissaoControles(controles, Session);
            Permissao.VerificarPermissaoControles(txtDataEmissao.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataInicio.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataTermino.Controles, Session);

            if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Convidados)
            {
                trRG.Visible = tdCPF.Visible = false;

            }
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var msg = String.Empty;

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var blocoI = new Seds.PMAS.QUADRIENAL.UI.Processos.Prefeituras(proxy);
                    PrefeituraInfo pre = blocoI.GetPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                    pre.Telefone = telefone.Text.Trim();
                    pre.Celular = celular.Text.Trim();
                    pre.Logradouro = cep1.Txtendereco;
                    pre.Numero = cep1.Txtnumero;
                    pre.Bairro = cep1.Txtbairro;
                    pre.CEP = cep1.Txtcep;
                    pre.Complemento = cep1.Txtcomplemento;
                    pre.Cidade = cep1.Txtcidade;
                    DateTime dt;
                    if (!String.IsNullOrEmpty(txtDataPublicacao.Text) && DateTime.TryParse(txtDataPublicacao.Text, out dt))
                        pre.DataPublicacao = Convert.ToDateTime(txtDataPublicacao.Text);
                    else
                        pre.DataPublicacao = DateTime.MinValue;

                    pre.Email = txtEmail.Text;
                    pre.IdNivelGestao = Convert.ToInt16(rblGestao.SelectedValue);
                    pre.PossuiSite = !chkPossuiSite.Checked;
                    if (pre.PossuiSite)
                        pre.WebSite = txtSite.Text;
                    new ValidadorPrefeitura().Validar(pre);
                    blocoI.UpdatePrefeitura(pre);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Prefeitura atualizada com sucesso!";
                lblInconsistencias.Text = "";
                lblInconsistencias.Visible = false;
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }

        protected void chkPossuiSite_CheckedChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            txtSite.Enabled = !chkPossuiSite.Checked;
            if (chkPossuiSite.Checked)
            {
                txtSite.Text = "";
                fraPrefeitura.Attributes.Add("class", "frame active");
            }
            else
            {
                fraPrefeitura.Attributes.Add("class", "frame active");
                this.Master.ScriptManagerControl.SetFocus(txtSite);
            }
        }

        protected void lstPrefeitos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

        protected void btnSalvarPrefeito_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var prefeito = new PrefeitoInfo();
            prefeito.Nome = txtNome.Text.Trim();
            prefeito.CPF = txtCPF.Text.Replace(".","").Replace("-", "");
            prefeito.Email = txtEmailPrefeito.Text;
            prefeito.IdStatus = 1;
            prefeito.IdUFRG = Convert.ToInt16(ddlUFPrefeito.SelectedValue);
            DateTime dt;
            if (!String.IsNullOrEmpty(txtDataInicio.Text) && DateTime.TryParse(txtDataInicio.Text, out dt))
                prefeito.MandatoInicio = Convert.ToDateTime(txtDataInicio.Text);
            if (!String.IsNullOrEmpty(txtDataTermino.Text) && DateTime.TryParse(txtDataTermino.Text, out dt))
                prefeito.MandatoTerminio = Convert.ToDateTime(txtDataTermino.Text);

            prefeito.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            prefeito.RG = txtRG.Txtrg;
            prefeito.RGDigito = txtRG.Txtdigito;
            prefeito.Telefone = telefonePrefeito.Text;
            prefeito.Celular = celularPrefeito.Text;

            if (!String.IsNullOrEmpty(txtDataEmissao.Text) && DateTime.TryParse(txtDataEmissao.Text, out dt))
                prefeito.RGDataEmissao = Convert.ToDateTime(txtDataEmissao.Text);

            prefeito.SiglaEmissor = txtOrgEmissor.Text;
            prefeito.Id = Convert.ToInt32(hdfIdPrefeito.Value);

            String msg = String.Empty;
            try
            {
                new ValidadorPrefeito().Validar(prefeito);

                foreach (var anterior in new Prefeituras(new ProxyPrefeitura()).GetPrefeitoAnteriores(SessaoPmas.UsuarioLogado.Prefeitura.Id).OrderByDescending(t => t.MandatoTerminio))
                {
                    if (anterior.MandatoTerminio > prefeito.MandatoInicio)
                    {
                        throw new Exception("A data de mandato do prefeito anterior sobrepõe a data de mandato do prefeito atual!");
                    }
                }

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    if (prefeito.Id == 0)
                    {
                        prefeituras.AddPrefeito(prefeito);
                        msg = "Dados do Prefeito registrados com sucesso!";
                        loadPrefeito(proxy);
                    }
                    else
                    {
                        prefeituras.UpdatePrefeito(prefeito);
                        msg = "Dados do Prefeito atualizados com sucesso!";
                    }

                    InibirCampos();
                    frmPrefeito.Attributes.Add("class", "frame active");
                    btnSalvarPrefeito.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>")), true);
                lblInconsistenciasPrefeito.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistenciasPrefeito.Visible = true;
                return;
            }


            lblInconsistenciasPrefeito.Text = "";
            tbInconsistenciasPrefeito.Visible = false;
            var script = Util.GetJavaScriptDialogOK(msg);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);

        }
        void carregarPrefeitosAnteriores(Prefeituras prefeituras)
        {
            lstPrefeitos.DataSource = prefeituras.GetPrefeitoAnteriores(SessaoPmas.UsuarioLogado.Prefeitura.Id).OrderByDescending(t => t.MandatoTerminio);
            lstPrefeitos.DataBind();
        }

        private void InibirCampos()
        {
            txtNome.Enabled = false;
            txtRG.Enabled = false;
            celularPrefeito.Enabled = false;
            telefonePrefeito.Enabled = false;
            txtDataEmissao.Enabled = false;
            txtOrgEmissor.Enabled = false;
            txtCPF.Enabled = false;
            txtDataInicio.Enabled = false;
            txtDataTermino.Enabled = false;
            txtEmailPrefeito.Enabled = false;
            ddlUFPrefeito.Enabled = false;

            btnSalvarPrefeito.Enabled = false;
            btnSubstituir.Enabled = true;
            btnEditar.Enabled = true;
        }
        private void ExibirCampos()
        {
            txtNome.Enabled = true;
            txtRG.Enabled = true;
            txtDataEmissao.Enabled = true;
            txtOrgEmissor.Enabled = true;
            txtCPF.Enabled = true;
            txtDataInicio.Enabled = true;
            txtDataTermino.Enabled = true;
            txtEmailPrefeito.Enabled = true;
            ddlUFPrefeito.Enabled = true;

            btnSalvarPrefeito.Enabled = true;
            btnSubstituir.Enabled = false;
            btnEditar.Enabled = false;
        }
        private void EditarCampos()
        {
            txtNome.Enabled = false;
            txtRG.Enabled = true;
            celularPrefeito.Enabled = true;
            telefonePrefeito.Enabled = true;
            txtDataEmissao.Enabled = true;
            txtOrgEmissor.Enabled = true;
            txtCPF.Enabled = true;
            txtDataInicio.Enabled = true;
            txtDataTermino.Enabled = true;
            txtEmailPrefeito.Enabled = true;
            ddlUFPrefeito.Enabled = true;

            btnSalvarPrefeito.Enabled = true;
            btnSubstituir.Enabled = false;
            btnEditar.Enabled = false;


        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            EditarCampos();
            frmPrefeito.Attributes.Add("class", "frame active");
            this.Master.ScriptManagerControl.SetFocus(txtNome);
        }

        protected void btnSubstituirConfirmacao_Click(object sender, EventArgs e)
        {
            //foreach (var controle in txtDataTermino.Controles)
            //{
            //    if (controle is TextBox)
            //    {
            //        TextBox input = (TextBox)controle;
            //        input.Enabled = true;
            //    }
            //}

            txtDataTermino.Enabled = true;
            frmPrefeito.Attributes.Add("class", "frame active");
        }

        protected void btnSubstituir_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            String msg = String.Empty;
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    prefeituras.SubstituirPrefeito(SessaoPmas.UsuarioLogado.Prefeitura.Id, txtDataTermino.Text);
                    carregarPrefeitosAnteriores(prefeituras);
                }

                foreach (var controle in telefonePrefeito.Controles)
                {
                    TextBox input = (TextBox)controle;
                    input.Enabled = true;
                    input.Text = string.Empty;
                }

                foreach (var controle in celularPrefeito.Controles)
                {
                    TextBox input = (TextBox)controle;
                    input.Enabled = true;
                    input.Text = string.Empty;
                }


                btnSubstituir.Enabled = false;
                btnEditar.Enabled = false;
                btnSalvarPrefeito.Enabled = true;

                hdfIdPrefeito.Value = "0";

                txtNome.Text = string.Empty;
                txtRG.Txtrg = string.Empty;
                txtRG.Txtdigito = string.Empty;
                txtDataEmissao.Text = string.Empty;
                txtOrgEmissor.Text = string.Empty;
                txtCPF.Text = String.Empty;
                txtDataInicio.Text = string.Empty;
                txtDataTermino.Text = string.Empty;
                txtEmailPrefeito.Text = string.Empty;
                ddlUFPrefeito.ClearSelection();

                frmPrefeito.Attributes.Add("class", "frame active");
                btnSubstituir.Focus();

                ExibirCampos();

            }

            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                this.Master.ScriptManagerControl.SetFocus(txtNome);
                return;
            }

            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }

        protected void lstPrefeitos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstPrefeitos.DataKeys[e.Item.DataItemIndex];
            try
            {
                switch (e.CommandName)
                {
                    case "Excluir_Prefeito":
                        using (var proxy = new ProxyPrefeitura())
                        {
                            var prefeituras = new Prefeituras(proxy);
                            prefeituras.DeletePrefeito(Convert.ToInt32(key["Id"]));
                            carregarPrefeitosAnteriores(prefeituras);
                            var script = Util.GetJavaScriptDialogOK("Dados do Prefeito excluídos com sucesso!");
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

    }
}