using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class ConsultaUsuarios : System.Web.UI.Page
    {
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "SA")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Senha do Usuário alterada com sucesso!"), true);                    
                }

                SessaoPmas.VerificarSessao(this);
                carregarCombos();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregarUsuarios();
        }

        protected void ddlDrads_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregarComboMunicipioByDrads();
        }

        protected void lstUsuarios_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {                
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        #endregion

        #region methods
        void carregarUsuarios()
        {
            Int32? idDrads = ddlDrads.SelectedIndex != 0 ? Convert.ToInt32(ddlDrads.SelectedValue) : new Nullable<Int32>();
            Int32? idMunicipio = ddlMunicipio.SelectedIndex != 0 ? Convert.ToInt32(ddlMunicipio.SelectedValue) : new Nullable<Int32>();
            Int32? idPerfil = ddlPerfil.SelectedIndex != 0 ? Convert.ToInt32(ddlPerfil.SelectedValue) : new Nullable<Int32>();
            using (var proxy = new ProxyUsuarioPMAS())
            {
                var temp = new Usuarios().GetConsultaUsuariosCadastrados(txtNome.Text.Trim(), txtRg.Text.Trim(), idDrads, idPerfil, idMunicipio, txtInstituicao.Text, proxy);
                foreach (var t in temp)
                {
                    t.Perfil = t.Perfil == "Convidados" ? "Consulta" : t.Perfil;
                }
                lstUsuarios.DataSource = temp;
                lstUsuarios.DataBind();
            }
        }

        void carregarCombos()
        {
            ddlDrads.DataSource = ProxyDivisaoAdministrativa.Drads;
            ddlDrads.DataTextField = "Nome";
            ddlDrads.DataValueField = "Id";
            ddlDrads.DataBind();
            ddlDrads.Items.Insert(0, new ListItem("Todos", "0", true));
            if (SessaoPmas.UsuarioLogado.IdDrads.HasValue)
            {
                ddlDrads.SelectedValue = SessaoPmas.UsuarioLogado.IdDrads.Value.ToString();
                ddlDrads.Enabled = false;
                ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.IdDrads == SessaoPmas.UsuarioLogado.IdDrads);
            }
            else
            {
                ddlDrads.SelectedIndex = 0;
                ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
            }

            ddlMunicipio.DataTextField = "Nome";
            ddlMunicipio.DataValueField = "Id";
            ddlMunicipio.DataBind();
            ddlMunicipio.Items.Insert(0, new ListItem("Todos", "0", true));
            ddlMunicipio.SelectedIndex = 0;
        }

        void carregarComboMunicipioByDrads()
        {
            if (ddlDrads.SelectedIndex == 0)
                ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
            else
                ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.IdDrads == Convert.ToInt32(ddlDrads.SelectedValue)).ToList();
            ddlMunicipio.DataBind();
            ddlMunicipio.Items.Insert(0, new ListItem("Todos", "0", true));
            ddlMunicipio.SelectedIndex = 0;
        }
        #endregion

    }
}