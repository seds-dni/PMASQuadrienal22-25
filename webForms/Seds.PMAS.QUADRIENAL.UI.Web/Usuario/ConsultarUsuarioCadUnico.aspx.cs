using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Seds.PMAS.QUADRIENAL.Processos;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Usuario
{
    public partial class ConsultarUsuarioCadUnico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lstUsuarios_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataSet ds = new Usuarios().PesquisarCadastroUnicoUsuarios(txtNome.Text, txtRg.Text);
            btnIncluir.Visible = ds.Tables[0].Rows.Count == 0;
            
            lstUsuarios.DataSource = ds.Tables[0];
            lstUsuarios.DataBind();
        }
    }
}