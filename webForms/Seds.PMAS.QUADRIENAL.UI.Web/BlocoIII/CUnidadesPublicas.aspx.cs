using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class CUnidadesPublicas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "UE")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Unidade Pública excluída com sucesso!"), true);
                    if (Request.QueryString["msg"] == "UD")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Unidade da Rede direta desativada com sucesso!"), true);
                }

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                load();

                verificarAlteracoes();

                WebControl[] controles = { btnIncluir };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro16.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 16);
                    linkAlteracoesQuadro16.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("16"));
                }
            }
        }

        void load()
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {

                var locais = proxy.Service.GetIdentificacaoUnidadesPublicaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, String.Empty).Where(c => c.Desativado != true);

                #region Exibicao Recursos e pivotagem dos cofinanciamentos

                var locaisSource = locais.GroupBy(x => x.Id).Select(g => new
                {
                    Id = g.First().Id
                    ,
                    obj = g.First()
                    ,
                    Cofinanciamentos = locais.Where(p => p.Id == g.First().Id).Select(x => new
                    {
                        ValorCofinanciamentoEstadual = x.ValorCofinanciamentoEstadual,
                        Exercicio = x.Exercicio
                    })
                }).ToList();
                #endregion


                lstUnidades.DataSource = locaisSource;
                lstUnidades.DataBind();
            }
        }

        protected void lstUnidades_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };
                Permissao.VerificarPermissaoControles(controles, Session);

            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx");
        }

        protected void btnLocalizar_Click(object sender, EventArgs e)
        {
            load();
        }

        protected void lstUnidades_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstUnidades.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    break;
                case "Visualizar_Locais":
                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?action=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("locais")) + "&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())));
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            var s = proxy.Service.GetLocaisPublicosByUnidade(Convert.ToInt32(Genericos.clsCrypto.Decrypt(id))).Where(c => c.Desativado != true);
                            if (s.Count() > 0)
                                throw new Exception("Essa unidade pública ainda possui locais de execução ativos.<br/>Desative primeiro os locais para desativar a unidade pública.");
                            Response.Redirect("~/BlocoIII/FMotivoExclusaoUnidadePublica.aspx?id=" + Server.UrlEncode(id));
                            
                           // proxy.Service.DeleteUnidadePublica(Convert.ToInt32(key["Id"].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx?msg=UE");
                    break;

                default:
                    break;
            }
        }

        //protected string MontarBotaoExcluir(ConsultaUnidadePublicaInfo item)
        //{
        //    if (!Util.VerificaPermissao())
        //        return null;
        //    if (!item.Desativado)
        //    {
        //        //// var idLocal = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"])));
        //        // var idUnidade = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
        //        return "<a href='FMotivoExclusaoUnidadePublica.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.Id.ToString())) + "'><img src='../Styles/Icones/editdelete.png' alt='Editar Serviço' border='0' /></a>";
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        protected void btnLimparBusca_Click(object sender, EventArgs e)
        {
            // txtLocalizar.Text = String.Empty;
            btnLocalizar_Click(null, null);
        }

        protected void btnUnidadesDesativadas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CUnidadesPublicasDesativadas.aspx?msg=UE");
        }
    }
}