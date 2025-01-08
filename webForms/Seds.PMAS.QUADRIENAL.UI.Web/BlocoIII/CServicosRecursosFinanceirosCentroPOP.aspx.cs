using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Data;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoII
{
    public partial class CServicosRecursosFinanceirosCentroPOP : System.Web.UI.Page
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

                WebControl[] controles = { btnAdicionarServico };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        void verificarAlteracoes(Int32 idCentro)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro34.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 34, idCentro);
                    linkAlteracoesQuadro34.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("34")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro.ToString()));
                }
            }
        }

        void load(ProxyRedeProtecaoSocial proxy)
        {
            var idLocal = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            lblCentroPOP.Text = proxy.Service.GetCentroPOPNomeById(idLocal);

            //var lst = proxy.Service.GetConsultaServicosRecursosFinanceirosByCentroPOP(id).Where(c => c.Desativado != true).GroupBy(s => s.ProtecaoSocial).Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) }).OrderBy(s => s.Key).ToList();

            #region Exibicao Recursos e pivotagem dos cofinanciamentos

            var protecoes = proxy.Service.GetConsultaServicosRecursosFinanceirosByCentroPOP(idLocal)
                                   .Where(c => c.Desativado != true);
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
                }),
                Previsoes = protecoes.Where(p => p.Id == g.First().Id).Select(x => new
                {
                    ValorPrevisaoOrcamentaria = x.PrevisaoOrcamentaria
                                                                ,
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

        protected void btnAdicionarServico_Click(object sender, EventArgs e)
        {
            var idCentro = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
            Response.Redirect("~/BlocoIII/FServicoRecursoFinanceiroCentroPOP.aspx?idCentro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro)) + "&idUnidade=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }

        protected void lstRecursos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Excluir")
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    var idCentro = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
                    var idUnidade = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
                    if (e.CommandName == "Excluir")
                    {
                        Response.Redirect("FMotivoExclusaoServicoCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString())) + "&idCentro=" + idCentro + "&idUnidade=" + idUnidade);
                    }
                }
            }
            catch (Exception ex)
            {
                var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
        }

        protected void lstItems_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ImageButton btnExcluir = (ImageButton)e.Item.FindControl("btnExcluir");
                Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoExcluir(btnExcluir);
                ConsultaServicosRecursosFinanceirosCentroPOPInfo servico = ((ConsultaServicosRecursosFinanceirosCentroPOPInfo)((dynamic)e.Item.DataItem).obj);
                if (servico != null)
                {
                    int idTipoServico = Convert.ToInt32(servico.IdTipoServico);
                    if (idTipoServico == 144) //Situacao de rua só é removido via CENTRO POP EDICAO
                    {
                        btnExcluir.Visible = false;
                        btnExcluir.Enabled = false;
                    }
                }
                
            }
        }

        protected string MontarBotaoEditar(string id)
        {
            if (!Permissao.VerificarPermissao())
                return null;
            var idCentro = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var idUnidade = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
            return "<a href='FServicoRecursoFinanceiroCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id)) + "&idCentro=" + idCentro + "&idUnidade=" + idUnidade + "'><img src='../Styles/Icones/edit.png' alt='Editar Serviço' border='0' /></a>";
        }

        protected string MontarBotaoExcluir(ConsultaServicosRecursosFinanceirosCentroPOPInfo item)
        {
            var botao = String.Empty;
            if (SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0 && Permissao.VerificarPermissao())
            {
                if (!item.Desativado)
                {
                    //if (item.IdTipoServico != 144)
                    //{
                    var idCentro = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
                    var idUnidade = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
                    botao = "<a href='FMotivoExclusaoServicoCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.Id.ToString())) + "&idCentro=" + idCentro + "&idUnidade=" + idUnidade + "'><img src='../Styles/Icones/editdelete.png' alt='Excluir Serviço' border='0' /></a>";
                }
            }
            return botao;
        }

        protected void btnServicosDesativados_Click(object sender, EventArgs e)
        {
            var idLocal = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCentroPOPDesativado.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }
    }
}