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
    public partial class AlterarSenhaUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var msg = String.Empty;

            try
            {
                new Usuarios().AlterarSenha(Genericos.clsCrypto.Decrypt(Request.QueryString["id"].ToString()), txtNovaSenha.Text, txtConfirmacaoSenha.Text);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                Response.Redirect("~/ConsultaUsuarios.aspx?msg=SA");
                return;
            }

            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }
    }
}