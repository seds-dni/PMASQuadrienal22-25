using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Controles
{
    public partial class cnpj : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        public string Text
        {
            get
            {
                string cnpj = this.txtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");
                return cnpj;
            }
            set
            {
                string cnpj = value;
                cnpj = "00000000000000" + cnpj;
                cnpj = cnpj.Substring(cnpj.Length - 14, 14);
                cnpj = cnpj.Insert(2, ".");
                cnpj = cnpj.Insert(6, ".");
                cnpj = cnpj.Insert(10, "/");
                cnpj = cnpj.Insert(15, "-");

                this.txtCNPJ.Text = cnpj;
            }
        }
        public bool Enabled
        {
            get
            {
                return this.txtCNPJ.Enabled;
            }
            set
            {
                this.txtCNPJ.Enabled = value;
            }
        }

        public WebControl[] Controles
        {
            get
            {
                WebControl[] controles_cnpj = { txtCNPJ };
                return controles_cnpj;
            }
        }

        public TextBox controleCNPJ { get { return txtCNPJ; } }
    }
}