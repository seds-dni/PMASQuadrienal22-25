using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FMotivoExclusaoUnidadePublica : System.Web.UI.Page
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
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                        load(proxy);
                }
            }
        }

        private void load(ProxyRedeProtecaoSocial proxy)
        {
            rblMotivoExclusao.DataSource = proxy.Service.GetMotivoDesativacaoLocal().Where(m => m.TipoLocal == "ODIRETA" && m.IdMotivoPai == null);
            rblMotivoExclusao.DataValueField = "Id";
            rblMotivoExclusao.DataTextField = "Descricao";
            rblMotivoExclusao.DataBind();

            lblDataExclusaoRegistro.Text = DateTime.Now.Date.ToShortDateString();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var id = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
            String action = "UD";
            try
            {
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    var unidade = proxy.Service.GetUnidadePublicaById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));

                    unidade.Desativado = true;

                    if (!String.IsNullOrEmpty(rblMotivoExclusao.SelectedValue))
                    {
                        unidade.IdMotivoDesativacao = Convert.ToInt32(rblMotivoExclusao.SelectedValue);

                        if (!String.IsNullOrEmpty(txtDataEncerramento.Text))
                        {
                            unidade.DataDesativacao = Convert.ToDateTime(txtDataEncerramento.Text);
                        }
                        unidade.Detalhamento = txtDetalhamento.Text;
                    }
                    unidade.DataRegistroLog = DateTime.Now;

                    proxy.Service.UpdateUnidadePublica(unidade);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.ToString()), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx?&msg=UD");
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["idUnidade"]))
            {
                var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
                Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx");
            }
            else
            {
                Response.Redirect("~/");
            }
        }

        protected void rblMotivoExclusao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMotivoExclusao.SelectedValue == "35")
            {
                trDataEncerramento.Visible = trDetalhamento.Visible = true;
            }
            else
            {
                txtDataEncerramento.Text = string.Empty;
                txtDetalhamento.Text = string.Empty;
                trDataEncerramento.Visible = trDetalhamento.Visible = false;
            }
        }


    }
}