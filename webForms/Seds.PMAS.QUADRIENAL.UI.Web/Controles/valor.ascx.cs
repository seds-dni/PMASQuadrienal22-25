using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Controles
{
    public partial class valor : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                 
        }

        public string Text
        {
            get
            {
                var v = txtValor.Text.Replace("_","");
                return String.IsNullOrEmpty(v) ? "0,00" : v;
            }
            set
            {                
                this.txtValor.Text = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.txtValor.Enabled;
            }
            set
            {
                this.txtValor.Enabled = value;                
            }
        }

        public bool ReadOnly
        {
            get
            {
                return this.txtValor.ReadOnly;
            }
            set
            {
                this.txtValor.ReadOnly = value;                
            }
        }

        public WebControl[] Controles
        {
            get
            {                
                WebControl[] controles = { txtValor };
                return controles;
            }
        }       
    }
}