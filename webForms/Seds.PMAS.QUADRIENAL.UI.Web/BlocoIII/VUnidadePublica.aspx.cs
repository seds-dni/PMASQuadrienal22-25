using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class VUnidadePublica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);
                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                load();
            }
        }



        void verificarAlteracoes(Int32 idUnidade)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro17.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 17, idUnidade);
                    linkAlteracoesQuadro17.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("17")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade.ToString()));

                    linkAlteracoesQuadroLocalPublico.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 18, idUnidade);
                    linkAlteracoesQuadroLocalPublico.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("18")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade.ToString()));

                    linkAlteracoesQuadroCRAS.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 20);
                    linkAlteracoesQuadroCRAS.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("20"));

                    linkAlteracoesQuadroCREAS.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 25);
                    linkAlteracoesQuadroCREAS.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("25"));

                    linkAlteracoesQuadroCentroPOP.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 31);
                    linkAlteracoesQuadroCentroPOP.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("31"));
                }
            }
        }

        void load()
        {
            #region Bloqueia , Desbloqueia e ordena Controles
            WebControl[] controles = {  txtNome
                                        };
            Permissao.VerificarPermissaoControles(controles, Session);
            Permissao.VerificarPermissaoControles(txtCNPJ.Controles, Session);

            #endregion

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                // trLocalizar.Visible = false;
                trLocaisExecucao.Visible = false;
                return;
            }

            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            UnidadePublicaInfo unidade;
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                unidade = proxy.Service.GetUnidadePublicaById(id);
                if (unidade.IdPrefeitura != SessaoPmas.UsuarioLogado.Prefeitura.Id)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                if (unidade == null)
                    return;
                carregarLocaisExecucao(proxy, id);
                loadCRAS(proxy, id);
                loadCREAS(proxy, id);
                loadCentroPOP(proxy, id);
            }

            verificarAlteracoes(unidade.Id);

            txtCNPJ.Text = unidade.CNPJ;
            txtNome.Text = unidade.RazaoSocial;
            lblCodigoUnidade.Text = unidade.Id.ToString();
        }

        void carregarLocaisExecucao(ProxyRedeProtecaoSocial proxy, Int32 idUnidade, String nome = null)  
        {
            lstLocais.DataSource = proxy.Service.GetIdentificacaoLocalExecucaoPublicoByUnidade(idUnidade, nome).Where(c => c.Desativado == true);
            lstLocais.DataBind();
        }

        protected void lstLocais_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstLocais.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/VLocalExecucaoPublico.aspx?id=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPublicoDesativado.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;
                default:
                    break;
            }
        }


        void loadCRAS(ProxyRedeProtecaoSocial proxy, Int32 idUnidade, String nome = null)  
        {
            lstCRAS.DataSource = proxy.Service.GetIdentificacaoCRASByUnidade(idUnidade, nome).Where(c => c.Desativado == true);//txtLocalizarCRAS.Text);
            lstCRAS.DataBind();
        }

        void loadCREAS(ProxyRedeProtecaoSocial proxy, Int32 idUnidade, String nome = null)  
        {
            lstCREAS.DataSource = proxy.Service.GetIdentificacoesCREASByUnidade(idUnidade, nome).Where(c => c.Desativado == true); //txtLocalizarCREAS.Text);
            lstCREAS.DataBind();
        }

        void loadCentroPOP(ProxyRedeProtecaoSocial proxy, Int32 idUnidade, String nome = null)  
        {
            lstCentroPOP.DataSource = proxy.Service.GetIdentificacaoCentroPOPByUnidade(idUnidade, nome).Where(c => c.Desativado == true);//txtLocalizarCentroPOP.Text);
            lstCentroPOP.DataBind();
        }

        protected void lstCRAS_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

        protected void lstCREAS_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

        protected void lstCentroPOP_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

       
       

        protected void lstCRAS_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstCRAS.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/VCRAS.aspx?id=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCRASDesativado.aspx?id=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;
                default:
                    break;
            }
        }

        protected void lstCREAS_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstCREAS.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/VCREAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCREASDesativado.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

               
                default:
                    break;
            }
        }

        protected void lstCentroPOP_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstCentroPOP.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/VCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCentroPOPDesativado.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                default:
                    break;
            }
        }

        protected void btnLimparBuscaCRAS_Click(object sender, EventArgs e)
        {
            //txtLocalizarCRAS.Text = String.Empty;
            var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCRAS(proxy, idUnidade);
            }
        }

        protected void btnLimparBuscaCREAS_Click(object sender, EventArgs e)
        {
            //txtLocalizarCREAS.Text = String.Empty;
            var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCREAS(proxy, idUnidade);
            }
        }

        protected void btnLimparBuscaCentroPOP_Click(object sender, EventArgs e)
        {
            //txtLocalizarCentroPOP.Text = String.Empty;
            var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCentroPOP(proxy, idUnidade);
            }
        }

         
        protected void btnLocalizar_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                carregarLocaisExecucao(proxy, id, txtLocalizar.Text);
                loadCRAS(proxy, id, txtLocalizar.Text);
                loadCREAS(proxy, id, txtLocalizar.Text);
                loadCentroPOP(proxy, id, txtLocalizar.Text);
            }
        }

        protected void btnLimparBusca_Click(object sender, EventArgs e)
        {
            txtLocalizar.Text = String.Empty;
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                carregarLocaisExecucao(proxy, id, txtLocalizar.Text);
                loadCRAS(proxy, id, txtLocalizar.Text);
                loadCREAS(proxy, id, txtLocalizar.Text);
                loadCentroPOP(proxy, id, txtLocalizar.Text);
            }
        }

        protected void btnLocalizarLocal_Click(object sender, EventArgs e)
        {
            trLocalizar.Visible = true;
        }

        protected void lstLocais_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

    }
}