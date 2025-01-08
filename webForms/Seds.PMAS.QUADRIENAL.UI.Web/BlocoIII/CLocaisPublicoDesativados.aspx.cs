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
    public partial class CLocaisPublicoDesativados : System.Web.UI.Page
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


                WebControl[] controles = { btnVoltar };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        void load()
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["IdUnidade"]));

                var locais = proxy.Service.GetIdentificacaoLocalExecucaoPublicoInativoByUnidade(id, String.Empty).Where(c => c.Desativado == true);

                var locaisSource = locais.GroupBy(x => x.Id).Select(g => new
                {
                    Id = g.First().Id
                    ,
                    obj = g.First()
                    ,
                    Nome = g.First().Nome
                    ,
                    Responsavel = g.First().Responsavel
                    ,
                    TotalServicosDesativados = g.First().TotalServicosDesativados
                    ,
                    DataEncerramento = g.First().DataEncerramento
                    ,
                    Cofinanciamentos = locais.Where(p => p.Id == g.First().Id).Select(x => new
                    {
                        ValorCofinanciamentoEstadual = x.ValorCofinanciamentoEstadual
                                                                    ,
                        Exercicio = x.Exercicio
                    })
                }).ToList();

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

        protected void lstUnidades_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstUnidades.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["IdUnidade"]);
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

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }
    }
}