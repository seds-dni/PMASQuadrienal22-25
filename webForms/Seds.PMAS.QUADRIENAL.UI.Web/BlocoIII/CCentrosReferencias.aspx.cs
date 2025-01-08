using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoII
{
    public partial class CCentrosReferencias : System.Web.UI.Page
    {
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                mostrarMensagens();

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    loadCRAS(proxy);
                    loadCREAS(proxy);
                    loadCentroPOP(proxy);
                }

                verificarAlteracoes();

                WebControl[] controles = { btnIncluirCRAS, btnIncluirCentroPOP, btnIncluirCREAS };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro20.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 20);
                    linkAlteracoesQuadro20.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("20"));
                    linkAlteracoesQuadro25.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 25);
                    linkAlteracoesQuadro25.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("25"));
                    linkAlteracoesQuadro31.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 31);
                    linkAlteracoesQuadro31.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("31"));
                }
            }
        }
               
        protected void lstCRAS_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void lstCREAS_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void lstCentroPOP_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void btnIncluirCRAS_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoII/FCRAS.aspx");
        }

        protected void btnIncluirCREAS_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoII/FCREAS.aspx");
        }

        protected void btnIncluirCentroPOP_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoII/FCentroPOP.aspx");
        }

        protected void btnLocalizarCRAS_Click(object sender, EventArgs e)
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCRAS(proxy);
            }
        }

        protected void btnLocalizarCREAS_Click(object sender, EventArgs e)
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCREAS(proxy);
            }
        }

        protected void btnLocalizarCentroPOP_Click(object sender, EventArgs e)
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCentroPOP(proxy);
            }
        }

        protected void lstCRAS_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstCRAS.DataKeys[e.Item.DataItemIndex];

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoII/FCRAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoII/CServicosRecursosFinanceirosCRAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            proxy.Service.DeleteCRAS(Convert.ToInt32(key["Id"].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }

                    Response.Redirect("~/BlocoII/CCentrosReferencias.aspx?msg=CRASE");
                    break;

                default:
                    break;
            }
        }

        protected void lstCREAS_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstCREAS.DataKeys[e.Item.DataItemIndex];

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoII/FCREAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoII/CServicosRecursosFinanceirosCREAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            proxy.Service.DeleteCREAS(Convert.ToInt32(key["Id"].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }

                    Response.Redirect("~/BlocoII/CCentrosReferencias.aspx?msg=CREASE");
                    break;

                default:
                    break;
            }
        }

        protected void lstCentroPOP_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstCentroPOP.DataKeys[e.Item.DataItemIndex];

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoII/FCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoII/CServicosRecursosFinanceirosCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            proxy.Service.DeleteCentroPOP(Convert.ToInt32(key["Id"].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }

                    Response.Redirect("~/BlocoII/CCentrosReferencias.aspx?msg=POPE");
                    break;

                default:
                    break;
            }
        }
        
        protected void btnLimparBuscaCRAS_Click(object sender, EventArgs e)
        {
            txtLocalizarCRAS.Text = String.Empty;
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCRAS(proxy);
            }
        }

        protected void btnLimparBuscaCREAS_Click(object sender, EventArgs e)
        {
            txtLocalizarCREAS.Text = String.Empty;
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCREAS(proxy);
            }
        }

        protected void btnLimparBuscaCentroPOP_Click(object sender, EventArgs e)
        {
            txtLocalizarCentroPOP.Text = String.Empty;
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCentroPOP(proxy);
            }
        }
        #endregion

        #region methods
        void loadCRAS(ProxyRedeProtecaoSocial proxy)
        {

            lstCRAS.DataSource = proxy.Service.GetIdentificacaoCRASByUnidade(SessaoPmas.UsuarioLogado.Prefeitura.Id, txtLocalizarCRAS.Text);
            lstCRAS.DataBind();
        }

        void loadCREAS(ProxyRedeProtecaoSocial proxy)
        {

            lstCREAS.DataSource = proxy.Service.GetIdentificacoesCREASByUnidade(SessaoPmas.UsuarioLogado.Prefeitura.Id, txtLocalizarCREAS.Text);
            lstCREAS.DataBind();
        }

        void loadCentroPOP(ProxyRedeProtecaoSocial proxy)
        {

            lstCentroPOP.DataSource = proxy.Service.GetIdentificacaoCentroPOPByUnidade(SessaoPmas.UsuarioLogado.Prefeitura.Id, txtLocalizarCentroPOP.Text);
            lstCentroPOP.DataBind();
        }

        void mostrarMensagens()
        {
            if (Request.QueryString.AllKeys.Any(t => t == "msg"))
            {
                if (Request.QueryString["msg"] == "CRASI")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CRAS registrado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CRASU")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CRAS atualizado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CRASE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CRAS excluído com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CREASI")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CREAS registrado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CREASU")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CREAS atualizado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CREASE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CREAS excluído com sucesso!"), true);
                else if (Request.QueryString["msg"] == "POPI")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Centro POP registrado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "POPU")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Centro POP atualizado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "POPE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Centro POP excluído com sucesso!"), true);                
            }
        }
        #endregion

        protected void btnPrevisaoInstalacaoCRAS_Click(object sender, EventArgs e)
        {
            Response.Redirect("FPrevisaoInstalacaoCRAS.aspx");
        }

        protected void btnPrevisaoInstalacaoCentroPOP_Click(object sender, EventArgs e)
        {
            Response.Redirect("FPrevisaoInstalacaoCentroPOP.aspx");
        }

        protected void btnPrevisaoInstalacaoCREAS_Click(object sender, EventArgs e)
        {
            Response.Redirect("FPrevisaoInstalacaoCREAS.aspx");
        }

        
    }
}