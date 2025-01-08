using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.UI.Processos;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Usuario
{
    public partial class AlterarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var msg = String.Empty;
            var script = Util.GetJavaScriptDialogOK(msg);

            try
            {                
                new Usuarios().AlterarSenha(SessaoPmas.UsuarioLogado.IdUsuario.ToString(),txtSenhaAtual.Text, txtNovaSenha.Text, txtConfirmacaoSenha.Text);

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            SessaoPmas.UsuarioLogado.TrocarSenha = false;

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Senha alterada com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                return;
            }

        }
    }
}