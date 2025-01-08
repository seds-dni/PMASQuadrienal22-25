using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Controles
{
    public partial class celular : System.Web.UI.UserControl
    {
        public TextBox DDD
        {
            get
            {
                return txtDDDCelular;
            }
            set
            {
                txtDDDCelular = value;
            }
        }
        public TextBox CELULAR
        {
            get
            {
                return txtCelular;
            }
            set
            {
                txtCelular = value;
            }
        }
        public string Text
        {
            get
            {
                string sCelular = txtDDDCelular.Text + txtCelular.Text.Replace("-", "").Replace("_", "");
                return sCelular;
            }
            set
            {
                string sCelularCompleto = value;
                string sDDDCelular, sCelular = "";

                if (!string.IsNullOrEmpty(sCelularCompleto))
                {
                    sCelularCompleto = "00000000000" + sCelularCompleto;
                    sCelularCompleto = sCelularCompleto.Substring(sCelularCompleto.Length - 11, 11);

                    sDDDCelular = sCelularCompleto.Substring(0, 2);
                    sCelular = sCelularCompleto.Substring(2, 9);
                    sCelular = sCelular.Insert(5, "-");

                    txtDDDCelular.Text = sDDDCelular;
                    txtCelular.Text = sCelular;
                }
                else
                {
                    txtDDDCelular.Text = string.Empty;
                    txtCelular.Text = string.Empty;
                }


            }
        }

        public WebControl[] Controles
        {
            get
            {
                WebControl[] controles_cel = {
                                            txtDDDCelular,
                                            txtCelular
                                        };
                return controles_cel;
            }
        }

        public bool Enabled
        {
            get
            {
                return txtDDDCelular.Enabled;
            }
            set
            {
                txtDDDCelular.Enabled = value;
                txtCelular.Enabled = value;
            }
        }

        public void SetFocus()
        {
            ((ScriptManager)this.Page.Master.FindControl("ScriptManager1")).SetFocus(txtDDDCelular);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS)
                Permissao.VerificarPermissaoCMAS(this.Controles, Session);
            else
                Permissao.VerificarPermissaoControles(this.Controles, Session);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtCelular.Attributes.Add("onkeypress", "MascaraCelular(this);");
        }
    }
}