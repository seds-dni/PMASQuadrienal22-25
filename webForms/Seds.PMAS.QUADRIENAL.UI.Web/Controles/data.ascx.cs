using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Controles
{
    public partial class data : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
        }

        public string Text
        {
            get
            {
                return txtData.Text.Replace("_","");
            }
            set
            {                
                this.txtData.Text = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.txtData.Enabled;
            }
            set
            {
                this.txtData.Enabled = value;
                this.CalendarExtender1.Enabled = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return this.txtData.ReadOnly;
            }
            set
            {
                this.txtData.ReadOnly = value;
                this.CalendarExtender1.Enabled = !value;
            }
        }

        public WebControl[] Controles
        {
            get
            {                
                WebControl[] controles = { txtData, ImgBntCalc };
                return controles;
            }
        }
    }
}