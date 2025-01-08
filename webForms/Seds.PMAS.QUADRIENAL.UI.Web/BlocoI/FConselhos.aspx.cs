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
    public partial class FConselhos : System.Web.UI.Page
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
                carregarCombos();
                using (var proxy = new ProxyPrefeitura())
                {
                    carregarConselhos(new Prefeituras(proxy));
                }
                verificarAlteracoes();
                WebControl[] controles = { ddlConselhos };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        void carregarCombos()
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                ddlConselhos.DataTextField = "Nome";
                ddlConselhos.DataValueField = "Id";
                ddlConselhos.DataSource = proxy.Service.GetTiposConselhos();
                ddlConselhos.DataBind();
                Util.InserirItemEscolha(ddlConselhos);
            }
        }

        void carregarConselhos(Prefeituras prefeituras)
        {
            lstConselhos.DataSource = prefeituras.GetIdentificacaoConselhosExistentesByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            lstConselhos.DataBind();
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro10.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 10);
                    linkAlteracoesQuadro10.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("10"));
                }
            }
        }

        void load(ConselhoExistenteInfo conselho)
        {
            hdfIdConselho.Value = conselho.Id.ToString();
            ddlConselhos.SelectedValue = conselho.IdConselho.ToString();
            ddlConselhos.Enabled = false;
            if (conselho.IdConselho == 9 || conselho.IdConselho == 3)
                txtNome.Text = conselho.Nome;
            if (!String.IsNullOrEmpty(conselho.Lei))
            {
                txtNumeroLeiCriacao.Text = conselho.Lei.Split('/')[0];
                txtAnoLeiCriacao.Text = conselho.Lei.Split('/')[1];
            }
            if (conselho.DataCriacao.HasValue)
                txtDataCriacao.Text = conselho.DataCriacao.Value.ToShortDateString();
            cep1.Txtcep = conselho.CEP;
            cep1.Txtendereco = conselho.Logradouro;
            cep1.Txtbairro = conselho.Bairro;
            cep1.Txtcomplemento = conselho.Complemento;
            cep1.Txtnumero = conselho.Numero;
            cep1.Txtcidade = conselho.Cidade;
            txtEmail.Text = conselho.Email;
            txtTelefone.Text = conselho.Telefone;
            txtcelular.Text = conselho.Celular;
            using (var proxy = new ProxyPrefeitura())
            {
                carregarPresidente(new Prefeituras(proxy), conselho.Id);
                carregarPresidentesAnteriores(new Prefeituras(proxy), conselho.Id);
            }
        }

        private void carregarPresidente(Prefeituras prefeituras, int idConselho)
        {
            var presidente = prefeituras.GetPresidenteConselhoByIdConselhoCollection(idConselho) ;
            
            if (presidente != null)
            {
                foreach (var p in presidente)
                {
                    hdfIdPresidente.Value = p.Id.ToString();
                    txtPresidente.Text = p.Nome;
                    txtCPF.Text = p.Cpf;
                    txtRG.Txtrg = p.RG;
                    txtRG.Txtdigito = p.RGDigito;
                    if (p.DataEmissao.HasValue)
                        txtDataEmissao.Text = p.DataEmissao.Value.ToShortDateString();
                    txtOrgEmissor.Text = p.SiglaEmissor;
                    ddlUF.SelectedValue = p.IDUFRG.ToString();
                    txtDataInicio.Text = String.Empty;
                    txtDataTermino.Text = String.Empty;
                    if (p.MandatoInicio.HasValue)
                        txtDataInicio.Text = p.MandatoInicio.Value.ToShortDateString();
                    if (p.MandatoTermino.HasValue)
                        txtDataTermino.Text = p.MandatoTermino.Value.ToShortDateString();                    
                }

                btnEditar.Enabled = btnSubstituir.Enabled = true;
                btnSalvarPresidente.Enabled = false;
            }
            else
            {
                btnEditar.Enabled = btnSubstituir.Enabled = btnSalvarPresidente.Enabled = false;
            }

        }

        private void carregarPresidentesAnteriores(Prefeituras prefeituras, int idConselho)
        {
            var prefeitosAnteriores = prefeituras.GetPresidentesConselhoExistenteByIdConselhoPrefeitura(
                                                                                                                    idConselho
                                                                                                                    , SessaoPmas.UsuarioLogado.Prefeitura.Id
                                                                                                                    ).OrderByDescending(t => t.MandatoTermino);

            lstPresidentesAnteriores.DataSource = prefeitosAnteriores;
            lstPresidentesAnteriores.DataBind();
        }



        private ConselhoExistenteInfo PreencherConselho()
        {
            var conselho = new ConselhoExistenteInfo();
            conselho.Id = Convert.ToInt32(hdfIdConselho.Value.Replace(",", "").ToString());

            conselho.IdConselho = Convert.ToInt32(ddlConselhos.SelectedValue);
            conselho.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            if (conselho.IdConselho == 9 || conselho.IdConselho == 3)
                conselho.Nome = txtNome.Text;

            conselho.NomePresidente = txtPresidente.Text;

            if (!String.IsNullOrEmpty(txtNumeroLeiCriacao.Text) && !String.IsNullOrEmpty(txtAnoLeiCriacao.Text))
                conselho.Lei = txtNumeroLeiCriacao.Text + "/" + txtAnoLeiCriacao.Text;

            DateTime dt;
            if (!String.IsNullOrEmpty(txtDataCriacao.Text) && DateTime.TryParse(txtDataCriacao.Text, out dt))
                conselho.DataCriacao = Convert.ToDateTime(txtDataCriacao.Text);

            if (!String.IsNullOrEmpty(txtDataInicio.Text) && DateTime.TryParse(txtDataInicio.Text, out dt))
                conselho.MandatoInicio = Convert.ToDateTime(txtDataInicio.Text);
            if (!String.IsNullOrEmpty(txtDataTermino.Text) && DateTime.TryParse(txtDataTermino.Text, out dt))
                conselho.MandatoTerminio = Convert.ToDateTime(txtDataTermino.Text);

            conselho.CEP = cep1.Txtcep;
            conselho.Logradouro = cep1.Txtendereco;
            conselho.Bairro = cep1.Txtbairro;
            conselho.Complemento = cep1.Txtcomplemento;
            conselho.Numero = cep1.Txtnumero;
            conselho.Cidade = cep1.Txtcidade;
            conselho.Email = txtEmail.Text;
            conselho.Telefone = txtTelefone.Text.Trim();
            conselho.Celular = txtcelular.Text.Trim();
            conselho.RG = txtRG.Txtrg.Trim();
            conselho.RGDigito = txtRG.Txtdigito.Trim();
            conselho.SiglaEmissor = txtOrgEmissor.Text;
            conselho.CPF = txtCPF.Text.Trim();
            conselho.IDUFRG = Convert.ToInt16(ddlUF.SelectedValue);
            if (!String.IsNullOrEmpty(txtDataEmissao.Text) && DateTime.TryParse(txtDataEmissao.Text, out dt))
                conselho.DataEmissao = Convert.ToDateTime(txtDataEmissao.Text);

            return conselho;
        }
        ConselhoMunicipalExistentePresidenteInfo PreencherPresidente()
        {
            ConselhoMunicipalExistentePresidenteInfo conselho = new ConselhoMunicipalExistentePresidenteInfo();
            conselho.Id = Convert.ToInt32(hdfIdPresidente.Value);
            conselho.IdConselho = Convert.ToInt32(hdfIdConselho.Value);
            conselho.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            conselho.Nome = txtPresidente.Text;
            conselho.IdStatus = 1;
            DateTime dt;
            if (!String.IsNullOrEmpty(txtDataInicio.Text) && DateTime.TryParse(txtDataInicio.Text, out dt))
                conselho.MandatoInicio = Convert.ToDateTime(txtDataInicio.Text);
            if (!String.IsNullOrEmpty(txtDataTermino.Text) && DateTime.TryParse(txtDataTermino.Text, out dt))
                conselho.MandatoTermino = Convert.ToDateTime(txtDataTermino.Text);

            conselho.RG = txtRG.Txtrg.Trim();
            conselho.RGDigito = txtRG.Txtdigito.Trim();
            conselho.SiglaEmissor = txtOrgEmissor.Text;
            conselho.Cpf = txtCPF.Text.Trim();
            conselho.IDUFRG = Convert.ToInt16(ddlUF.SelectedValue);
            if (!String.IsNullOrEmpty(txtDataEmissao.Text) && DateTime.TryParse(txtDataEmissao.Text, out dt))
                conselho.DataEmissao = Convert.ToDateTime(txtDataEmissao.Text);

            return conselho;

        }
        void InibirCampos()
        {
            pnlDados.Visible = false;
            trNome.Visible = false;
        }
        void LimparCampos()
        {
            txtNome.Text = String.Empty;
            txtAnoLeiCriacao.Text = String.Empty;
            txtDataCriacao.Text = String.Empty;
            txtNumeroLeiCriacao.Text = String.Empty;

            cep1.Txtcep = String.Empty;
            cep1.Txtendereco = String.Empty;
            cep1.Txtbairro = String.Empty;
            cep1.Txtcomplemento = String.Empty;
            cep1.Txtnumero = String.Empty;
            cep1.Txtcidade = String.Empty;
            txtEmail.Text = String.Empty;
            txtTelefone.Text = String.Empty;
            txtcelular.Text = String.Empty;
            txtPresidente.Text = String.Empty;
            txtCPF.Text = String.Empty;
            txtRG.Txtrg = String.Empty;
            txtRG.Txtdigito = String.Empty;
            txtDataEmissao.Text = String.Empty;
            txtOrgEmissor.Text = String.Empty;
            ddlUF.SelectedIndex = 0;
            txtDataInicio.Text = String.Empty;
            txtDataTermino.Text = String.Empty;
            hdfIdConselho.Value = "0";
            hdfIdPresidente.Value = "0";
            btnSalvarPresidente.Enabled = false;
            BloquearCampos(true);
            ddlConselhos.SelectedIndex = 0;
            ddlConselhos_SelectedIndexChanged(null, null);
            ddlConselhos.Enabled = true;
            lblInconsistencias.Text = String.Empty;
            tbInconsistencias.Visible = false;


        }
        private void EditarCampos()
        {
            txtNome.Enabled = false;
            txtRG.Enabled = true;
            txtDataEmissao.Enabled = true;
            //txtOrgEmissor.Enabled = true;
            //txtCPF.Enabled = true;
            txtDataInicio.Enabled = true;
            txtDataTermino.Enabled = true;
            txtEmail.Enabled = true;
            ddlUF.Enabled = true;
            btnSalvarPresidente.Enabled = false;
            btnEditar.Enabled = false;
            btnSubstituir.Enabled = false;
            btnSalvar.Enabled = true;
            btnSubstituir.Enabled = false;
            btnEditar.Enabled = false;
        }
        private void EditarCamposPresidente()
        {
            ExibirCampos();
            txtNome.Enabled = btnSubstituir.Enabled =
            btnEditar.Enabled = false;
            txtRG.Enabled = txtCPF.Enabled =
            txtDataEmissao.Enabled = txtDataInicio.Enabled =
            txtDataTermino.Enabled = txtEmail.Enabled =
            txtOrgEmissor.Enabled = ddlUF.Enabled =
            btnSalvarPresidente.Enabled = true;

        }
        void ExibirCampos()
        {
            pnlDados.Visible = true;
            trNome.Visible = ddlConselhos.SelectedValue == "9" || ddlConselhos.SelectedValue == "3";
            //trRG.Visible = trEmissor.Visible = ddlConselhos.SelectedValue == "2";
            if (!trNome.Visible)
                txtNome.Text = String.Empty;
        }
        void BloquearCampos(Boolean enabled)
        {
            txtNome.Enabled = txtAnoLeiCriacao.Enabled = txtDataCriacao.Enabled = enabled;
            txtNumeroLeiCriacao.Enabled = txtPresidente.Enabled = enabled;
            txtDataInicio.Enabled = txtDataTermino.Enabled = enabled;
            txtTelefone.Enabled = txtcelular.Enabled = txtEmail.Enabled = enabled;
            btnSalvar.Enabled = enabled;

            cep1.Controles.ToList().ForEach(c => c.Enabled = enabled);
        }
        void BloquearCamposPresidente(Boolean enabled)
        {
            txtPresidente.Enabled = txtCPF.Enabled = txtRG.Enabled =
            txtDataEmissao.Enabled = txtOrgEmissor.Enabled = ddlUF.Enabled = txtDataInicio.Enabled = txtDataTermino.Enabled = enabled;
            btnSalvarPresidente.Enabled = btnSubstituir.Enabled = btnEditar.Enabled = enabled;
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            EditarCampos();
            EditarCamposPresidente();
            //this.Master.ScriptManagerControl.SetFocus(txtNome);
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var conselho = PreencherConselho();
            String msg = String.Empty;
            var script = Util.GetJavaScriptDialogOK(msg);
            try
            {
                new ValidadorConselhoExistente().Validar(conselho);

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    if (conselho.Id == 0)
                    {
                        var idConselho = prefeituras.AddConselhoExistente(conselho);
                        var presidenteConselho = PreencherPresidente();
                        presidenteConselho.IdConselho = idConselho;
                        prefeituras.AddPresidenteConselhoExistente(presidenteConselho);
                        msg = "Conselho Incluído com sucesso!";
                    }
                    else
                    {
                        prefeituras.UpdateConselhoExistente(conselho);
                        msg = "Conselho Atualizado com sucesso!";
                    }
                    carregarConselhos(prefeituras);
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                msg = "Verifique as inconsistências!";
                script = Util.GetJavascriptDialogError(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            msg = "Conselho(s) existente(s) no município registrado(s) com sucesso!";
            lblInconsistencias.Text = "";
            tbInconsistencias.Visible = false;
            script = Util.GetJavaScriptDialogOK(msg);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);

        }
        protected void btnSubstituir_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var presidente = PreencherPresidente();
            presidente.Id = Convert.ToInt32(hdfIdPresidente.Value.Replace(",", ""));
            presidente.IdConselho = Convert.ToInt32(hdfIdConselho.Value.Replace(",", ""));
            String msg = String.Empty;
            try
            {
                new ValidadorConselhoExistente().ValidarPresidente(presidente);

                foreach (var anterior in new Prefeituras(new ProxyPrefeitura()).GetPresidentesConselhoExistenteByIdConselhoPrefeitura(presidente.IdConselho, SessaoPmas.UsuarioLogado.Prefeitura.Id).OrderByDescending(t => t.MandatoTermino))
                {
                    if (anterior.MandatoTermino > presidente.MandatoInicio)
                    {
                        throw new Exception("A data de mandato do prefeito anterior sobrepõe a data de mandato do presidente atual!");
                    }
                }

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    prefeituras.SubstituirPresidenteConselhoExistente(presidente);
                    msg = "Dados do Presidente do conselho registrados com sucesso!";
                    carregarPresidentesAnteriores(prefeituras, presidente.IdConselho);
                    LimparCamposPresidente();
                    BloquearCamposPresidente(true);
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
            LimparCamposPresidente();
            return;

        }
        private void LimparCamposPresidente()
        {
            hdfIdPresidente.Value = "0";
            txtPresidente.Text = String.Empty;
            txtCPF.Text = String.Empty;
            txtRG.Txtrg = String.Empty;
            txtRG.Txtdigito = String.Empty;
            txtDataEmissao.Text = String.Empty;
            ddlUF.SelectedIndex = 0;
            txtDataInicio.Text = String.Empty;
            txtDataTermino.Text = String.Empty;
            btnSalvarPresidente.Enabled = true;
            btnSubstituir.Enabled = btnEditar.Enabled = false;
        }
        protected void btnSalvarPresidente_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            var presidente = PreencherPresidente();

            String msg = String.Empty;
            try
            {
                new ValidadorConselhoExistente().ValidarPresidente(presidente);

                foreach (var anterior in new Prefeituras(new ProxyPrefeitura()).GetPresidentesConselhoExistenteByIdConselhoPrefeitura(presidente.IdConselho, SessaoPmas.UsuarioLogado.Prefeitura.Id).OrderByDescending(t => t.MandatoTermino))
                {
                    if (anterior.MandatoTermino > presidente.MandatoInicio)
                    {
                        throw new Exception("A data de mandato do prefeito anterior sobrepõe a data de mandato do presidente atual!");
                    }
                }

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    if (presidente.Id == 0)
                    {
                        prefeituras.AddPresidenteConselhoExistente(presidente);
                        msg = "Dados do Presidente do conselho registrados com sucesso!";
                        carregarPresidente(prefeituras, presidente.IdConselho);
                        carregarPresidentesAnteriores(prefeituras, presidente.IdConselho);
                    }
                    else
                    {
                        prefeituras.UpdatePresidenteConselhoExistente(presidente);
                        msg = "Dados do Presidente do conselho atualizados com sucesso!";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>")), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }
            btnSalvarPresidente.Enabled = false;
            btnEditar.Enabled = true;
            btnSubstituir.Enabled = true;
            lblInconsistencias.Text = "";
            tbInconsistencias.Visible = false;
            var script = Util.GetJavaScriptDialogOK(msg);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);

        }
        protected void lstConselhos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")), ((ImageButton)e.Item.FindControl("btnEditar")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void ddlConselhos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlConselhos.SelectedIndex == 0)
            {
                InibirCampos();
                return;
            }

            BloquearCamposPresidente(true);
            btnSalvarPresidente.Enabled = btnSubstituir.Enabled = btnEditar.Enabled = false;
            ExibirCampos();
            trEmissor.Visible = trRG.Visible = txtOrgEmissor.Visible = txtDataEmissao.Visible = txtRG.Visible = ddlUF.Visible = false;
        }

        protected void lstConselhos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstConselhos.DataKeys[e.Item.DataItemIndex];
            try
            {
                switch (e.CommandName)
                {
                    case "Visualizar_Conselho":
                        using (var proxy = new ProxyPrefeitura())
                        {
                            var prefeituras = new Prefeituras(proxy);
                            load(prefeituras.GetConselhoExistenteById(Convert.ToInt32(key["Id"])));
                            ExibirCampos();
                            BloquearCampos(false);
                            BloquearCamposPresidente(false);
                            tbInconsistencias.Visible = false;
                        }
                        break;
                    case "Editar_Conselho":
                        using (var proxy = new ProxyPrefeitura())
                        {
                            var prefeituras = new Prefeituras(proxy);
                            load(prefeituras.GetConselhoExistenteById(Convert.ToInt32(key["Id"])));
                            ExibirCampos();
                            BloquearCampos(true);
                            BloquearCamposPresidente(false);
                            btnEditar.Enabled = btnSubstituir.Enabled = true;
                            tbInconsistencias.Visible = false;
                        }
                        break;

                    case "Excluir_Conselho":
                        using (var proxy = new ProxyPrefeitura())
                        {
                            var prefeituras = new Prefeituras(proxy);
                            var presidentes = prefeituras.GetPresidentesConselhoExistenteByIdConselho(Convert.ToInt32(key["Id"]));
                            if (presidentes.Count() > 0)
                            {
                                foreach (var p in presidentes)
                                {
                                    prefeituras.DeletePresidenteConselhoExistente(p.Id);
                                }
                            }
                            prefeituras.DeleteConselhoExistente(Convert.ToInt32(key["Id"]));
                            carregarConselhos(prefeituras);
                            var script = Util.GetJavaScriptDialogOK("Conselho removido com sucesso!");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        }
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
        

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        protected void lstPresidentesAnteriores_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")), ((ImageButton)e.Item.FindControl("btnEditar")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }

        }

        protected void lstPresidentesAnteriores_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstPresidentesAnteriores.DataKeys[e.Item.DataItemIndex];
            try
            {
                switch (e.CommandName)
                {
                    case "Excluir_Presidente":
                        using (var proxy = new ProxyPrefeitura())
                        {
                            var prefeituras = new Prefeituras(proxy);
                            prefeituras.DeletePresidenteConselhoExistente(Convert.ToInt32(key["Id"]));
                            carregarPresidentesAnteriores(prefeituras, Convert.ToInt32(hdfIdConselho.Value));
                            var script = Util.GetJavaScriptDialogOK("Presidente removido com sucesso!");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        }
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

    }
}