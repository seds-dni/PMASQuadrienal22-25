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
    public partial class CServicosRecursosFinanceirosPrivado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "A")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço atualizado com sucesso!"), true);
                    }
                    else if (Request.QueryString["msg"] == "I")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço registrado com sucesso!"), true);
                    }
                    else if (Request.QueryString["msg"] == "D")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço desativado com sucesso!"), true);
                    }
                }

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                if (String.IsNullOrEmpty(Request.QueryString["idLocal"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPrivadas.aspx");
                    return;
                }
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    load(proxy);
                    carregarLocalExecucao(proxy);
                }

                WebControl[] controles = { btnAdicionarServico };
                Permissao.VerificarPermissaoControles(controles, Session);
            }

        }

        void verificarAlteracoes(Int32 idLocal)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro39.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 39, idLocal);
                    linkAlteracoesQuadro39.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("39")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal.ToString()));
                }
            }
        }

        void load(ProxyRedeProtecaoSocial proxy)
        {
            var idLocal = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));

            #region Exibicao Recursos e pivotagem dos cofinanciamentos

            var protecoes = proxy.Service.GetConsultaServicosRecursosFinanceirosPrivadoByLocalExecucao(idLocal).Where(c => c.Desativado != true);
            var protecoesSource = protecoes.GroupBy(x => x.Id).Select(g => new
            {
                id = g.First().Id
                ,
                obj = g.First()
                ,
                Cofinanciamentos = protecoes.Where(p => p.Id == g.First().Id).Select(x => new
                {
                    ValorCofinanciamentoEstadual = x.ValorCofinanciamentoEstadual,
                    CapacidadeMensalAtendimento = x.PrevisaoMensalNumeroAtendidos,  
                    Exercicio = x.Exercicio
                })
            }).ToList();

            var gruposDeProtecao = protecoesSource.GroupBy(s => s.obj.ProtecaoSocial)
                                   .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.obj.TipoServico) })
                                   .OrderBy(s => s.Key).ToList();
            #endregion

            lstRecursos.DataSource = gruposDeProtecao;
            lstRecursos.DataBind();

            verificarAlteracoes(idLocal);
        }

        void carregarLocalExecucao(ProxyRedeProtecaoSocial proxy)
        {
            var local = proxy.Service.GetLocalExecucaoPrivadoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"])));
            if (local == null)
                return;
            lblLocalExecucao.Text = local.Nome + " (" + (local.Unidade != null ? local.Unidade.RazaoSocial : "") + ")";
        }

        protected void btnAdicionarServico_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            var idLocal = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
            Response.Redirect("~/BlocoIII/FServicoRecursoFinanceiroPrivado.aspx?idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            Response.Redirect("~/BlocoIII/FUnidadePrivada.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }

        protected void lstRecursos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                try
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    var idLocal = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"])));
                    var idUnidade = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
                    if (e.CommandName == "Excluir")
                        Response.Redirect("FMotivoExclusaoServicoPrivado.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString())) + "&idLocal=" + idLocal + "&idUnidade=" + idUnidade);
                }
                catch (Exception ex)
                {
                    var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                }
            }
        }
        protected void lstItems_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ImageButton btnExcluir = (ImageButton)e.Item.FindControl("btnExcluir");
                Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIBotaoExcluir(btnExcluir);
            }
        }

        protected string MontarBotaoExcluir(ConsultaServicosRecursosFinanceirosPrivadoInfo item)
        {
            if (!Permissao.VerificarPermissao())
                return null;
            if (!item.Desativado)
            {
                var idLocal = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"])));
                var idUnidade = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
                return "<a href='FMotivoExclusaoServicoPrivado.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.Id.ToString())) + "&idLocal=" + idLocal + "&idUnidade=" + idUnidade + "'><img src='../Styles/Icones/editdelete.png' alt='Editar Serviço' border='0' /></a>";
            }
            {
                return null;
            }
        }

        protected string MontarBotaoEditar(String id)
        {
            if (!Permissao.VerificarPermissao())
                return null;
            var idLocal = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"])));
            var idUnidade = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
            return "<a href='FServicoRecursoFinanceiroPrivado.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id)) + "&idLocal=" + idLocal + "&idUnidade=" + idUnidade + "'><img src='../Styles/Icones/edit.png' alt='Editar Serviço' border='0' /></a>";
        }

        protected void btnServicosDesativados_Click(object sender, EventArgs e)
        {
            var idLocal = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPrivadoDesativado.aspx?idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }



    }
}