using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS2013.UI.Web
{
    public partial class Cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void Executar()
        {
            using (var client = new Seds.PMAS2013.UI.Processos.UsuarioPMASService.UsuarioPMASServiceClient("WS2007HttpBinding_IUsuarioPMASService"))
            {
                client.ClientCredentials.UserName.UserName = "zSHc5LBdKkRaIWAGDJzmLrK1UFq0buoYE+1uD9YVS207UNIOp5wtCc5eaodaB8lj";
                client.ClientCredentials.UserName.Password = "aiaukDwYn1vKZG1ls/HvdYR7fOKMcx/te4o49WFDZdM=";
                client.Open();
                try
                {
                   client.RunCadastro(txtComando.Text);
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }

                client.Close();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Executar();
        }
    }
}