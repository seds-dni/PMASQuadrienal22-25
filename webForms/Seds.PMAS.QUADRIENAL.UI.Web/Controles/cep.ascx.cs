using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Controles
{
    public partial class cep : System.Web.UI.UserControl
    {
        public string Txtcep
        {
            get
            {
                return this.txtCEP.Text.Replace("-", "").Replace("_", "");
            }
            set
            {
                string cep = value;
                cep = "00000000" + cep;
                cep = cep.Substring(cep.Length - 8, 8);
                cep = cep.Insert(5, "-");

                this.txtCEP.Text = cep;
            }
        }

        public string Txtendereco
        {
            get { return this.txtLogradouro.Text.Trim(); }
            set { this.txtLogradouro.Text = value; }
        }

        public string Txtnumero
        {
            get { return this.txtNumero.Text.Trim(); }
            set { this.txtNumero.Text = value; }
        }

        public string Txtcomplemento
        {
            get { return txtComplemento.Text.Trim(); }
            set { txtComplemento.Text = value; }
        }

        public string Txtbairro
        {
            get { return txtBairro.Text.Trim(); }
            set { txtBairro.Text = value; }
        }

        public string Txtcidade
        {
            get { return txtCidade.Text.Trim(); }
            set { txtCidade.Text = value; }
        }

        public bool Enabled
        {
            get
            {
                return txtCEP.Enabled;
            }
            set
            {
                txtCEP.Enabled = value;
                txtLogradouro.Enabled = value;
                txtNumero.Enabled = value;
                txtComplemento.Enabled = value;
                txtBairro.Enabled = value;
                txtCidade.Enabled = value;
                cmdPesqCEP.Enabled = value;
            }
        }

        public WebControl[] Controles
        {
            get
            {
                WebControl[] controles_cep = {
                                            txtCEP,
                                            txtLogradouro,
                                            txtNumero,
                                            txtComplemento,
                                            txtBairro,
                                            txtCidade,
                                            cmdPesqCEP
                                        };
                return controles_cep;
            }
        }

        public TextBox controleCEP { get { return txtCEP; } }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Permissao.VerificarPermissaoControles(this.Controles, Session);
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        protected void cmdPesqCEP_Click(object sender, EventArgs e)
        {
            if (txtCEP.Text.ToString() == "00000000" || txtCEP.Text.ToString() == "00000-000" || txtCEP.Text.Length < 8)
            {
                ScriptManager.RegisterStartupScript(cmdPesqCEP, this.GetType(), Guid.NewGuid().ToString(), "alert('" + "Conteúdo do campo CEP inválido." + "');", true);
                ((ScriptManager)this.Page.Master.FindControl("ScriptManager1")).SetFocus(txtCEP);
            }
            else
            {
                try
                {
                    Logradouro logradouro = Util.ConsultaCEP(this.Txtcep);

                    txtLogradouro.Text = logradouro.Nome;
                    txtBairro.Text = logradouro.Bairro;
                    txtCidade.Text = logradouro.Cidade;
                    if(!String.IsNullOrEmpty(logradouro.Nome))
                        ((ScriptManager)this.Page.Master.FindControl("ScriptManager1")).SetFocus(txtNumero);
                    else
                        ((ScriptManager)this.Page.Master.FindControl("ScriptManager1")).SetFocus(txtLogradouro);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}