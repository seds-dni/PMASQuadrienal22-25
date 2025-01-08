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
    public partial class telefone : System.Web.UI.UserControl
    {
        public TextBox DDD
        {
            get
            {
                return txtDDD;
            }
            set
            {
                txtDDD = value;
            }
        }
        public TextBox TELEFONE
        {
            get
            {
                return txtTelefone;
            }
            set
            {
                txtTelefone = value;
            }
        }
        public string Text
        {
            get
            {
                string sTelefone = txtDDD.Text + txtTelefone.Text.Replace("-", "").Replace("_", "");
                return sTelefone;
            }
            set
            {
                string sTelefoneCompleto = value;
                string sDDD, sTelefone = "";

                if (!string.IsNullOrEmpty(sTelefoneCompleto))
                {
                    sTelefoneCompleto = "0000000000" + sTelefoneCompleto;
                    sTelefoneCompleto = sTelefoneCompleto.Substring(sTelefoneCompleto.Length - 10, 10);

                    sDDD = sTelefoneCompleto.Substring(0, 2);
                    sTelefone = sTelefoneCompleto.Substring(2, 8);
                    sTelefone = sTelefone.Insert(4, "-");

                    txtDDD.Text = sDDD;
                    txtTelefone.Text = sTelefone;
                }
                else
                {
                    txtDDD.Text = string.Empty;
                    txtTelefone.Text = string.Empty;
                }


            }
        }

        public WebControl[] Controles
        {
            get
            {
                WebControl[] controles_tel = {
                                            txtDDD,
                                            txtTelefone
                                        };
                return controles_tel;
            }
        }

        public bool Enabled
        {
            get
            {
                return txtDDD.Enabled;
            }
            set
            {
                txtDDD.Enabled = value;
                txtTelefone.Enabled = value;
            }
        }

        public void SetFocus()
        {
            ((ScriptManager)this.Page.Master.FindControl("ScriptManager1")).SetFocus(txtDDD);
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
            txtTelefone.Attributes.Add("onkeypress", "MascaraTelefone(this);");
        }
    }
}