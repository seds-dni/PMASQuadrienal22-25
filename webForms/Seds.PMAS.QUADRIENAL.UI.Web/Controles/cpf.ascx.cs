using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Controles
{
    public partial class cpf : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS)
                {
                    Permissao.VerificarPermissaoCMAS(this.Controles, Session);
                }
                else
                {
                    Permissao.VerificarPermissaoControles(this.Controles, Session);
                }
            }
        }

        public string Text
        {
            get
            {
                string cpf = this.txtCPF.Text.Replace(".", "").Replace("-", "").Replace("_", "");
                return cpf;
            }
            set
            {
                string cpf = value;
                cpf = "00000000000" + cpf;
                cpf = cpf.Substring(cpf.Length - 11, 11);
                cpf = cpf.Insert(3, ".");
                cpf = cpf.Insert(7, ".");
                cpf = cpf.Insert(11, "-");

                this.txtCPF.Text = cpf;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.txtCPF.Enabled;
            }
            set
            {
                this.txtCPF.Enabled = value;
            }
        }

        public WebControl[] Controles
        {
            get
            {
                WebControl[] controles_cpf = { txtCPF };
                return controles_cpf;
            }
        }
    }
}