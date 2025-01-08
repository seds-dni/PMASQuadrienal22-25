using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoII
{
    public partial class CServicosRecursosFinanceirosCRASDesativado : System.Web.UI.Page
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

                if (String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx");
                    return;
                }

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "A")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço atualizado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "I")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço registrado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "D")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço desativado com sucesso!"), true);
                }

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    load(proxy);
                }

                //WebControl[] controles = { btnAdicionarServico };
                //Util.VerificaPermissao(controles, Session);
            }
        }


        void load(ProxyRedeProtecaoSocial proxy)
        {
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            lblCRAS.Text = proxy.Service.GetCRASNomeById(id);

            //var lst = proxy.Service.GetConsultaServicosRecursosFinanceirosByCRAS(id).Where(c =>c.Desativado == true).GroupBy(s => s.ProtecaoSocial).Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) }).OrderBy(s => s.Key).ToList();

            #region Exibicao Recursos e pivotagem dos cofinanciamentos

            var recursos = proxy.Service.GetConsultaServicosRecursosFinanceirosByCRAS(id)
                                   .Where(c => c.Desativado == true);
            var recursosSource = recursos.GroupBy(x => x.Id).Select(g => new
            {
                id = g.First().Id
                ,
                obj = g.First()
                ,
                Cofinanciamentos = recursos.Where(p => p.Id == g.First().Id).Select(x => new
                {
                    ValorCofinanciamentoEstadual = x.ValorCofinanciamentoEstadual
                                                                ,
                    Exercicio = x.Exercicio
                })
            }).ToList();

            var grupoDeRecursos = recursosSource.GroupBy(s => s.obj.ProtecaoSocial)
                                   .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.obj.TipoServico) })
                                   .OrderBy(s => s.Key).ToList();
            #endregion

            lstRecursos.DataSource = grupoDeRecursos;
            lstRecursos.DataBind();
        }

        protected void btnAdicionarServico_Click(object sender, EventArgs e)
        {
            var idCentro = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
            Response.Redirect("~/BlocoIII/FServicoRecursoFinanceiroCRAS.aspx?idCentro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro)) + "&idUnidade=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }

        protected void lstRecursos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                try
                {
                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {
                        proxy.Service.DeleteServicoRecursoFinanceiroCRAS(Convert.ToInt32(e.CommandArgument));
                        load(proxy);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço excluído com sucesso!"), true);
                    }
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
                //if (SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0)
                //{
                //    var btnExcluir = (ImageButton)e.Item.FindControl("btnExcluir");
                //    btnExcluir.Visible = false;
                //}
                //else
                //{
                Permissao.VerificarPermissaoControles(new[] { ((ImageButton)e.Item.FindControl("btnExcluir")) }, Session);
                //}
            }
        }



        protected string MontarBotaoExcluir(ConsultaServicosRecursosFinanceirosCRASInfo item)
        {
            if (!Permissao.VerificarPermissao())
                return null;
            if (SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0 && Permissao.VerificarPermissao())
                if (!item.Desativado)
                {
                    var idCentro = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
                    var idUnidade = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
                    return "<a href='FMotivoExclusaoServicoCRAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.Id.ToString())) + "&idCentro=" + idCentro + "&idUnidade=" + idUnidade + "'><img src='../Styles/Icones/editdelete.png' alt='Editar Serviço' border='0' /></a>";
                }
                else
                    return null;
            else
                return null;
        }


        protected string MontarBotaoEditar(ConsultaServicosRecursosFinanceirosCRASInfo item)
        {
            if (!Permissao.VerificarPermissao())
                return null;
            var idCentro = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var idUnidade = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
            return "<a href='FServicoRecursoFinanceiroCRAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.Id.ToString())) + "&idCentro=" + idCentro + "&idUnidade=" + idUnidade + "'><img src='../Styles/Icones/edit.png' alt='Editar Serviço' border='0' /></a>";
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CCRASDesativados.aspx?idUnidade=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }
    }
}