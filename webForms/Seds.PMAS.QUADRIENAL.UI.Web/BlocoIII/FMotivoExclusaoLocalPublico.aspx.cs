using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FMotivoExclusaoLocalPublico : System.Web.UI.Page
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

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["idLocal"]))
                        load(proxy);
                }
            }
        }

        private void load(ProxyRedeProtecaoSocial proxy)
        {
            rblMotivoExclusao.DataSource = proxy.Service.GetMotivoDesativacaoLocal().Where(m => m.TipoLocal == "DIRETA" && m.IdMotivoPai == null);
            rblMotivoExclusao.DataValueField = "Id";
            rblMotivoExclusao.DataTextField = "Descricao";
            rblMotivoExclusao.DataBind();

            lblDataExclusaoRegistro.Text = DateTime.Now.Date.ToShortDateString();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var id = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
            String action = "LD";
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            try
            {
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    /// Recuperar o Local de execução da Rede Direta para atualizar os campos referente a desativação
                    var obj = proxy.Service.GetLocalExecucaoPublicoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"])));

                    if (!String.IsNullOrEmpty(rblMotivoExclusao.SelectedValue))
                    {
                        obj.IdMotivoDesativacao = Convert.ToInt32(rblMotivoExclusao.SelectedValue);
                        obj.Desativado = true;

                        if (rblMotivoExclusao.SelectedValue != "27")
                        {
                            if (!String.IsNullOrEmpty(txtDataEncerramento.Text))
                                obj.DataDesativacao = Convert.ToDateTime(txtDataEncerramento.Text);

                            obj.Detalhamento = txtDetalhamento.Text;

                            if (!String.IsNullOrEmpty(rblMotivoEncerramento.SelectedValue))
                                obj.IdMotivoEncerramento = Convert.ToInt32(rblMotivoEncerramento.SelectedValue);
                        }
                        obj.DataRegistroLog = DateTime.Now;
                    }
                    proxy.Service.UpdateLocalExecucaoPublico(obj);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.ToString()), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)) + "&msg=LD");
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }

        protected void rblMotivoExclusao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMotivoExclusao.SelectedValue == "28")
            {
                trDataEncerramento.Visible = trDetalhamento.Visible = trMotivoEncerramento.Visible = rblMotivoEncerramento.Enabled = true;
                carregarMotivosDesativacao(Convert.ToInt32(rblMotivoExclusao.SelectedValue));
            }
            else
            {
                trMotivoEncerramento.Visible = rblMotivoEncerramento.Enabled = trDataEncerramento.Visible = trDetalhamento.Visible = false;
            }
        }

        private void carregarMotivosDesativacao(int idmotivo)
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                rblMotivoEncerramento.DataSource = proxy.Service.GetMotivoDesativacaoLocal().Where(m => m.IdMotivoPai == idmotivo);
                rblMotivoEncerramento.DataValueField = "Id";
                rblMotivoEncerramento.DataTextField = "Descricao";
                rblMotivoEncerramento.DataBind();
            }
        }


    }
}