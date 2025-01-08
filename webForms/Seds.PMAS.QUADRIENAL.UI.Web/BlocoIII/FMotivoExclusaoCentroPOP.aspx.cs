using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FMotivoExclusaoCentroPOP : System.Web.UI.Page
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
            rblMotivoExclusao.DataSource = proxy.Service.GetMotivoDesativacaoLocal().Where(m => m.TipoLocal == "POP" && m.IdMotivoPai == null);
            rblMotivoExclusao.DataValueField = "Id";
            rblMotivoExclusao.DataTextField = "Descricao";
            rblMotivoExclusao.DataBind();



            lblDataExclusaoRegistro.Text = DateTime.Now.Date.ToShortDateString();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var id = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
            String action = "POPD";
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            try
            {
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    var obj = proxy.Service.GetCentroPOPById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"])));

                    obj.Desativado = true;
                    if (!String.IsNullOrEmpty(txtDataEncerramento.Text))
                        obj.DataDesativacao = Convert.ToDateTime(txtDataEncerramento.Text);
                    if (!String.IsNullOrEmpty(rblMotivoExclusao.SelectedValue))
                    {
                        obj.IdMotivoDesativacao = Convert.ToInt32(rblMotivoExclusao.SelectedValue);
                        obj.DataRegistroLog = DateTime.Now;
                        if (rblMotivoExclusao.SelectedValue != "21")
                        {
                            if (!String.IsNullOrEmpty(rblMotivoEncerramento.SelectedValue))
                            {
                                obj.IdMotivoEncerramento = Convert.ToInt32(rblMotivoEncerramento.SelectedValue);
                                obj.Detalhamento = txtDetalhamento.Text;
                            }
                        }
                    }
                    proxy.Service.UpdateCentroPOP(obj);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.ToString()), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)) + "&msg=POPD");
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }

        protected void rblMotivoExclusao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMotivoExclusao.SelectedValue == "22")
            {

                rblMotivoEncerramento.Enabled = trMotivoEncerramento.Visible = trDataEncerramento.Visible = trDetalhamento.Visible = true;
                var idmotivo = Convert.ToInt32(rblMotivoExclusao.SelectedValue);
                carregarMotivosDesativacao(idmotivo);
                trDataEncerramento.Visible = trDetalhamento.Visible = true;
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