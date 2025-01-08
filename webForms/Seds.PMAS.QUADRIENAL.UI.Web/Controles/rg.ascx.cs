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
    public partial class rg : System.Web.UI.UserControl
    {
        public string Txtrg
        {
            get
            {
                string sRg = txtrg.Text.Replace(".", "");
                return sRg;
            }
            set
            {
                txtrg.Text = value.Trim();
            }
        }

        public string Txtdigito
        {
            get
            {
                string sDigitoRG = txtdigito.Text;
                return sDigitoRG;
            }
            set
            {
                string sDigitoRG = value;

                if (string.IsNullOrEmpty(sDigitoRG))
                    txtdigito.Text = "";
                else
                    txtdigito.Text = sDigitoRG;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.txtrg.Enabled;
            }
            set
            {
                this.txtrg.Enabled = value;
                this.txtdigito.Enabled = value;
            }
        }

        public WebControl[] Controles
        {
            get
            {
                WebControl[] controles_rg = {
                                            txtrg,
                                            txtdigito
                                        };
                return controles_rg;
            }
        }

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
    }
}