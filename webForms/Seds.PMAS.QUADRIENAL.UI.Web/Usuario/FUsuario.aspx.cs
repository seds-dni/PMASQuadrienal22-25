using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Data;
using System.Security.Permissions;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Usuario
{        
    public partial class FUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "ADD")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Usuário registrado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "UPD")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Usuário atualizado com sucesso!"), true);                    
                }

                load();
                loadUsuario();
            }
        }

        void bloquearControles()
        {
            ddlPerfil.Enabled = false;
            ddlDrads.Enabled = false;
            ddlMunicipio.Enabled = false;
            txtNome.Enabled = false;
            txtEmail.Enabled = false;
            txtRG.Enabled = false;
            txtOrgaoEmissor.Enabled = false;
            txtUF.Enabled = false;
            txtCPF.Enabled = false;
            controleCep.Enabled = false;
            txtTelefone.Enabled = false;
            txtInstituicao.Enabled = false;
            txtCargo.Enabled = false;
            txtLogin.Enabled = false;
            rblSituacao.Enabled = false;
            btnSalvar.Enabled = false;
        }

        void loadUsuario()
        {
            string idUsuario = null;
            if (Request.QueryString["id"] != null)
            {
                idUsuario = Genericos.clsCrypto.Decrypt(Request.QueryString["id"].ToString());

                UsuarioPMASInfo usuarioPmas;

                using (var proxy = new ProxyUsuarioPMAS())
                {
                    usuarioPmas = new Usuarios().GetUsuarioById(Convert.ToInt32(idUsuario), proxy);
                }

                if (usuarioPmas == null)
                {
                    hdfAction.Value = "ADD";
                    usuarioPmas = new Usuarios().GetUsuarioFromCadastroUnico(Convert.ToInt32(idUsuario));
                }
                else 
                {
                    hdfAction.Value = "UPD";
                }
                if (usuarioPmas != null)
                {
                    txtCPF.Text = usuarioPmas.CPF;
                    txtInstituicao.Text = usuarioPmas.Instituicao;
                    txtCargo.Text = usuarioPmas.Cargo;
                    txtNome.Text = usuarioPmas.Nome;
                    txtEmail.Text = usuarioPmas.Email;
                    txtCPF.Text = usuarioPmas.CPF;
                    txtOrgaoEmissor.Text = usuarioPmas.OrgaoEmissor;
                    txtRG.Txtrg = usuarioPmas.RG;
                    txtUF.Text = usuarioPmas.UFRG;
                    txtTelefone.Text = usuarioPmas.Telefone;
                    txtLogin.Text = usuarioPmas.Login;

                    controleCep.Txtcep = usuarioPmas.CEP;
                    controleCep.Txtendereco = usuarioPmas.Endereco;
                    controleCep.Txtcomplemento = usuarioPmas.Complemento;
                    controleCep.Txtnumero = usuarioPmas.Numero;
                    controleCep.Txtbairro = usuarioPmas.Bairro;
                    controleCep.Txtcidade = usuarioPmas.Cidade;

                    ddlPerfil.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador || SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador;

                    ddlPerfil.SelectedValue = usuarioPmas.EnumPerfil.HasValue ? ((int)usuarioPmas.EnumPerfil).ToString() : "0";

                    ddlPerfil_SelectedIndexChanged(null, null);


                    if (usuarioPmas.EnumPerfil.HasValue)
                    {
                        switch (((int)usuarioPmas.EnumPerfil).ToString())
                        {
                            case "64": 
                                
                                Session["idPerfilAntigo"] = EPerfil.OrgaoGestor; 
                                
                                break;
                            case "65":

                                Session["idPerfilAntigo"] = EPerfil.DRADSAdministrador; 
                                
                                break;
                            case "66":

                                Session["idPerfilAntigo"] = EPerfil.SEDS; 
                                
                                break;
                            case "67":

                                Session["idPerfilAntigo"] = EPerfil.CAS; 
                                
                                break;
                            case "68":

                                Session["idPerfilAntigo"] = EPerfil.Administrador; 
                                
                                break;
                            case "69":

                                Session["idPerfilAntigo"] = EPerfil.Convidados; 
                                
                                break;
                            case "70":

                                Session["idPerfilAntigo"] = EPerfil.DRADS; 
                                
                                break;
                            case "71":

                                Session["idPerfilAntigo"] = EPerfil.CMAS;
 
                                break;
                            case"72":

                                Session["idPerfilAntigo"] = EPerfil.Gabinete;
 
                                break;
                        }                        
                    }

                    if (usuarioPmas.IdDrads.HasValue)
                    {
                        ddlDrads.SelectedValue = usuarioPmas.IdDrads.ToString();
                    }
                    if (usuarioPmas.IdPrefeitura.HasValue)
                    {
                        ddlMunicipio.SelectedValue = usuarioPmas.IdPrefeitura.ToString();
                    }

                    rblSituacao.SelectedIndex = usuarioPmas.Ativo;
                    txtLogin.Enabled = false;

                    if (usuarioPmas.EnumPerfil == EPerfil.DRADSAdministrador
                        && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador)
                    {
                        bloquearControles();
                    }

                    if (usuarioPmas.EnumPerfil == null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogWarning("Usuário encontrado no cadastro único sem perfil"), true);
                    }
                    
                }
                else {
                    bloquearControles();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("Usuário não encontrado no PMAS e no Cadastro Único"), true);
                }
            }
            else
            {
                hdfAction.Value = "ADD";
            }
        }

        void load()
        {
            var prefeituras = new List<ConsultaFluxoInfo>();
            if (SessaoPmas.UsuarioLogado.IdDrads.HasValue)
            {
                ddlDrads.DataSource = ProxyDivisaoAdministrativa.Drads.Where(d => d.Id == SessaoPmas.UsuarioLogado.IdDrads.Value);
                using (var proxy = new ProxyPrefeitura())
                {
                    prefeituras = proxy.Service.GetConsultaFluxo(ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.IdDrads == SessaoPmas.UsuarioLogado.IdDrads.Value).Select(m => m.Id).ToList()).ToList();
                }
            }
            else
            {
                ddlDrads.DataSource = ProxyDivisaoAdministrativa.Drads;
                using (var proxy = new ProxyPrefeitura())
                {
                    prefeituras = proxy.Service.GetConsultaFluxo(ProxyDivisaoAdministrativa.MunicipiosEstaduais.Select(m => m.Id).ToList()).ToList();
                }
            }

            prefeituras.ForEach(c => c.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.First(m => m.Id == c.IdMunicipio).Nome);

            ddlMunicipio.DataSource = prefeituras.OrderBy(p=> p.Municipio);

            ddlDrads.DataTextField = "Nome";
            ddlDrads.DataValueField = "Id";
            ddlDrads.DataBind();
            Util.InserirItemEscolha(ddlDrads);

            ddlMunicipio.DataTextField = "Municipio";
            ddlMunicipio.DataValueField = "IdPrefeitura";
            ddlMunicipio.DataBind();
            Util.InserirItemEscolha(ddlMunicipio);
            
            switch (SessaoPmas.UsuarioLogado.EnumPerfil)
            {
                case EPerfil.Administrador:
                    ddlPerfil.DataSource = Util.GetPerfis();

                    break;
                case EPerfil.DRADSAdministrador:
                    ddlPerfil.DataSource = Util.GetPerfisDradsAdministrador();
                    break;
                case EPerfil.OrgaoGestor:
                case EPerfil.SEDS:
                case EPerfil.CAS:
                case EPerfil.Convidados:
                case EPerfil.DRADS:
                case EPerfil.CMAS:
                default:
                    Response.Redirect("../LogOff.aspx");
                    break;
            }
            ddlPerfil.DataTextField = "Nome";
            ddlPerfil.DataValueField = "Id";
            ddlPerfil.DataBind();
            Util.InserirItemEscolha(ddlPerfil);                      
        }

        void salvar()
        {
            var u = new UsuarioPMASInfo();
            if (Request.QueryString["id"] != null)
            u.IdUsuario = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            u.Nome = txtNome.Text.Trim();
            u.Instituicao = txtInstituicao.Text;
            u.Email = txtEmail.Text;
            u.Cargo = txtCargo.Text;
            u.Login = txtLogin.Text;
            u.CEP = controleCep.Txtcep;
            u.Endereco = controleCep.Txtendereco;            
            u.Numero = controleCep.Txtnumero;
            u.Bairro = controleCep.Txtbairro;
            u.Complemento = controleCep.Txtcomplemento;
            u.Cidade = controleCep.Txtcidade;
            u.CPF = txtCPF.Text;
            u.RG = txtRG.Txtrg;            
            u.OrgaoEmissor = txtOrgaoEmissor.Text;
            u.Telefone = u.Celular = txtTelefone.Text;                       
            u.UFRG = u.UFCidade = txtUF.Text;
            u.IdPrefeitura = ddlMunicipio.SelectedIndex == 0 ? new Nullable<Int32>() : Convert.ToInt32(ddlMunicipio.SelectedValue);
            u.IdDrads = ddlDrads.SelectedIndex == 0 ? new Nullable<Int16>() : Convert.ToInt16(ddlDrads.SelectedValue);
            u.EnumPerfil = (EPerfil)Convert.ToInt32(ddlPerfil.SelectedValue);
            u.Ativo = rblSituacao.SelectedIndex;
            u.idPerfilAntigo = Convert.ToInt32(Session["idPerfilAntigo"]); 

            var msg = String.Empty;

            try
            {
                
                using (var proxy = new ProxyUsuarioPMAS())
                {
                    u.IdUsuario = new Usuarios().SaveUsuario(u, hdfAction.Value, proxy);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;                             
            }

            if (String.IsNullOrEmpty(msg))
            {
                Response.Redirect("~/Usuario/FUsuario.aspx?msg="+ hdfAction.Value +"&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(u.IdUsuario.ToString())));                
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);                
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine,"<br/>");
            tbInconsistencias.Visible = true;
            
        }

        protected void ddlPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMunicipio.ClearSelection();
            ddlDrads.ClearSelection();
            EPerfil tipo = (EPerfil)Convert.ToInt32(ddlPerfil.SelectedValue);
            switch (tipo)
            {
                case EPerfil.DRADSAdministrador:
                case EPerfil.DRADS:
                    ddlDrads.Enabled = true;
                    ddlMunicipio.Enabled = false;
                    break;
                case EPerfil.OrgaoGestor:
                    ddlDrads.Enabled = false;
                    ddlMunicipio.Enabled = true;
                    break;
                case EPerfil.SEDS:
                    ddlDrads.Enabled = false;
                    ddlMunicipio.Enabled = false;
                    break;
                case EPerfil.CAS:
                    ddlDrads.Enabled = false;
                    ddlMunicipio.Enabled = false;
                    break;
                case EPerfil.Convidados:
                    ddlDrads.Enabled = true;
                    ddlMunicipio.Enabled = true;
                    break;
                case EPerfil.Administrador:
                    ddlDrads.Enabled = false;
                    ddlMunicipio.Enabled = false;
                    break;

                case EPerfil.CMAS:
                    ddlDrads.Enabled = false;
                    ddlMunicipio.Enabled = true;
                    break;

                case EPerfil.Gabinete:
                    ddlDrads.Enabled = false;
                    ddlMunicipio.Enabled = false;
                    ddlMunicipio.SelectedValue = "7741";
                    break;

                default:
                    ddlDrads.Enabled = false;
                    ddlMunicipio.Enabled = false;
                    break;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            salvar();
        }
    }
}