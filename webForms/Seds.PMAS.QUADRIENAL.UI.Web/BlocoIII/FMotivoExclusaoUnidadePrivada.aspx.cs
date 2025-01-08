using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Linq;
using System.Web.UI;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FMotivoExclusaoUnidadePrivada : System.Web.UI.Page
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
            rblMotivoExclusao.DataSource = proxy.Service.GetMotivoDesativacaoLocal().Where(m => m.TipoLocal == null && m.IdMotivoPai == null);
            rblMotivoExclusao.DataValueField = "Id";
            rblMotivoExclusao.DataTextField = "Descricao";
            rblMotivoExclusao.DataBind();

            lblDataExclusaoRegistro.Text = DateTime.Now.Date.ToShortDateString();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            String action = "LD";
            try
            {
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    var obj = proxy.Service.GetUnidadePrivadaById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));

                    obj.Desativado = true;

                    if (!String.IsNullOrEmpty(rblMotivoExclusao.SelectedValue))
                    {
                      
                        obj.IdMotivoDesativacao = Convert.ToInt32(rblMotivoExclusao.SelectedValue);
                        obj.DataRegistroLog = DateTime.Now;
                        if (rblMotivoExclusao.SelectedValue != "1")
                        {
                            if (rblMotivoExclusao.SelectedValue == "2")
                                if (!String.IsNullOrEmpty(rblMotivoEncerramento.SelectedValue))
                                    obj.IdMotivoEncerramento = Convert.ToInt32(rblMotivoEncerramento.SelectedValue);
                            
                            if (!String.IsNullOrEmpty(txtDataEncerramento.Text))
                                obj.DataDesativacao = Convert.ToDateTime(txtDataEncerramento.Text);

                            obj.Detalhamento = txtDetalhamento.Text;
                        }
                    }
                    proxy.Service.UpdateUnidadePrivada(obj);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.ToString()), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Response.Redirect("~/BlocoIII/CUnidadesPrivadas.aspx?&msg=UD");
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CUnidadesPrivadas.aspx");
        }

        protected void rblMotivoExclusao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMotivoExclusao.SelectedValue == "2")
            {

                rblMotivoEncerramento.Enabled = trMotivoEncerramento.Visible = trDataEncerramento.Visible = trDetalhamento.Visible = true;
                var idmotivo = Convert.ToInt32(rblMotivoExclusao.SelectedValue);
                carregarMotivosDesativacao(idmotivo);
                lblDataEncerramentoServico.Text = "Data do encerramento do termo de convênio, parceria ou fomento:";
                lblDetalhamento.Text = "Detalhamento sobre o motivo do encerramento termo de convênio, parceria ou fomento:";
            }
            else if (rblMotivoExclusao.SelectedValue == "3")
            {
                trDataEncerramento.Visible = trDetalhamento.Visible = true;
                trMotivoEncerramento.Visible = rblMotivoEncerramento.Enabled = false;
                lblDataEncerramentoServico.Text = "Data do encerramento das atividades da organização:";
                lblDetalhamento.Text = "Detalhamento sobre o motivo do encerramento das atividades da organização:";
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