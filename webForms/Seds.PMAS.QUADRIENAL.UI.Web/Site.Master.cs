using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.Entidades;
using Seds.PMAS.QUADRIENAL.Processos;
using AjaxControlToolkit;
using System.Reflection;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {

        public ToolkitScriptManager ScriptManagerControl { get { return ScriptManager1; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (SessaoPmas.UsuarioLogado.TrocarSenha && !Request.RawUrl.Contains("AlterarSenha.aspx"))
                {
                    Response.Redirect("~/Usuario/AlterarSenha.aspx");
                }
               lblPerfil.Text = SessaoPmas.UsuarioLogado.Perfil == "Convidados" ? "Consulta" : SessaoPmas.UsuarioLogado.Perfil;  
                carregarMenu();
                lblMenu.Text = Util.RetonaDescricaoPagina(SessaoPmas.UsuarioLogado.Recursos, Request.Url.LocalPath.Substring(1));
                CarregaNavegacao(Request.Url.LocalPath.Substring(1));
                CarregarDadosPlano();
            }
            LoadVersion();

        }

        private void LoadVersion()
        {
            Version versao = Assembly.GetExecutingAssembly().GetName().Version;
            txtVersion.Text = string.Format("V. {0}.{1}.{2}.{3}", versao.Major, versao.Minor, versao.Build, versao.Revision);

        }

        public void CarregarDadosPlano()
        {
            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.Id > 0)
            {
                lblPrefeitura.Text = "Município : " + SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Nome + " / Plano " + SessaoPmas.UsuarioLogado.Prefeitura.Situacao.Nome;
                lblDrads.Text = "DRADS " + SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Drads.Nome + " - ";
            }
            else if (SessaoPmas.UsuarioLogado.Drads != null)
                lblDrads.Text = "DRADS " + SessaoPmas.UsuarioLogado.Drads.Nome;
        }

        void carregarMenu()
        {
            menu.Items.Clear();
            foreach (MenuItem item in RetornaMenu())
            {
                bool podeAcessar = true;
                switch (SessaoPmas.UsuarioLogado.EnumPerfil)
                {
                    case EPerfil.OrgaoGestor:
                        break;
                    case EPerfil.DRADSAdministrador:
                    case EPerfil.SEDS:
                    case EPerfil.CAS:
                    case EPerfil.Administrador:
                    case EPerfil.DRADS:
                    case EPerfil.CMAS:
                    case EPerfil.Convidados:
                        if (SessaoPmas.UsuarioLogado.Prefeitura == null && item.Value != "1" && item.Value != "2")
                            podeAcessar = false;                        
                        break;
                    default:
                        break;
                }

                item.Enabled = podeAcessar;
                menu.Items.Add(item);

            }
        }

        List<MenuItem> RetornaMenu()
        {
            List<MenuItem> listaMenu = new List<MenuItem>();            
            try
            {
                var resultado = SessaoPmas.UsuarioLogado.Recursos.Where(r => !r.IdPai.HasValue).OrderBy(r => r.Ordem).ToList();                            
                foreach (var item in resultado)
                {
                    MenuItem itemPai = new MenuItem(item.Nome);
                    itemPai.Value = item.Id.ToString();
                    itemPai.NavigateUrl = item.Pagina;                    
                    this.PreencheMenuFilho(item.Id, itemPai);
                    listaMenu.Add(itemPai);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaMenu;
        }

        void PreencheMenuFilho(int idPai, MenuItem menuPai)
        {
            try
            {

                var resultado = SessaoPmas.UsuarioLogado.Recursos.Where(r => r.IdPai.HasValue && r.IdPai.Value == idPai && r.Id != 151 && r.Id != 159 /*&& r.Id != 126*/).OrderBy(r => r.Ordem).ToList();
                foreach (var item in resultado)
                {   if(item.Id != 151 || item.Id != 159)
                    {
                    MenuItem itemFilho = new MenuItem(item.Nome);
                    itemFilho.Value = item.Id.ToString();
                    itemFilho.NavigateUrl = item.Pagina;
                    if (item.Id == 123 || item.Id == 159 || item.Id == 161)
                        itemFilho.Target = "_blank";
                    menuPai.ChildItems.Add(itemFilho);
                    this.PreencheMenuFilho(item.Id, itemFilho);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void menu_MenuItemClick(object sender, MenuEventArgs e)
        {
            try
            {                
                if (!string.IsNullOrEmpty(e.Item.Target))
                {                   
                    if (!String.IsNullOrEmpty(e.Item.Value))
                        lblMenu.Text = Util.RetonaDescricaoPagina(SessaoPmas.UsuarioLogado.Recursos, Convert.ToInt32(e.Item.Value));                    

                    CarregaNavegacao(e.Item.Target.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CarregaNavegacao(string url)
        {
            string anterior = Util.RetornaRecursoAnterior(SessaoPmas.UsuarioLogado.Recursos, url);
            string proximo = Util.RetornaRecursoProximo(SessaoPmas.UsuarioLogado.Recursos, url);

            ViewState["Anterior"] = anterior;
            ViewState["Proximo"] = proximo;
        }

        protected void UrlProximo_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["Proximo"].ToString());            
        }

        protected void UrlAnterior_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["Anterior"].ToString());
        }
    }

     
}
