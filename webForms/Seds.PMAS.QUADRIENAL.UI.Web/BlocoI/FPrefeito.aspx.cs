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

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoI
{
    public partial class FPrefeito : System.Web.UI.Page
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
                    linkAlteracoesQuadro2.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 2);
                    linkAlteracoesQuadro2.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("2"));
                    linkAlteracoesQuadro3.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 3);
                    linkAlteracoesQuadro3.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("3"));
                }
            }
        }

        void load(ProxyPrefeitura proxy)
        {
            ddlUFPrefeito.ClearSelection();

            var prefeituras = new Prefeituras(proxy);

            var prefeito = prefeituras.GetPrefeitoAtual(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (prefeito != null)
            {
                txtCPF.Text = prefeito.CPF;
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
                                          ddlUFPrefeito , 
                                          txtOrgEmissor ,                                          
                                          txtEmailPrefeito,
                                          telefonePrefeito.DDD,
                                          telefonePrefeito.TELEFONE,
                                          celularPrefeito.DDD,
                                          celularPrefeito.CELULAR,
                                          btnSalvarPrefeito, 
                                          btnSubstituir,
                                         btnEditar};

            Permissao.VerificarPermissaoControles(txtRG.Controles, Session);
            Permissao.VerificarPermissaoControles(txtCPF.Controles, Session);
            Permissao.VerificarPermissaoControles(controles, Session);
            Permissao.VerificarPermissaoControles(txtDataEmissao.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataInicio.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataTermino.Controles, Session);
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
            prefeito.CPF = txtCPF.Text;
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
                        load(proxy);
                    }
                    else
                    {
                        prefeituras.UpdatePrefeito(prefeito);
                        msg = "Dados do Prefeito atualizados com sucesso!";
                    }

                    InibirCampos();
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            EditarCampos();
            this.Master.ScriptManagerControl.SetFocus(txtNome);
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
                    prefeituras.SubstituirPrefeito(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    carregarPrefeitosAnteriores(prefeituras);
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

                ExibirCampos();
            }

            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                lblInconsistenciasPrefeito.Text = "";
                tbInconsistenciasPrefeito.Visible = false;
                this.Master.ScriptManagerControl.SetFocus(txtNome);
                return;
            }

            lblInconsistenciasPrefeito.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistenciasPrefeito.Visible = true;
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