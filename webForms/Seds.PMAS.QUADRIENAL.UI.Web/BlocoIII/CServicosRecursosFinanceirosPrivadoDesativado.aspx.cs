using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoII
{
    public partial class CServicosRecursosFinanceirosPrivadoDesativado : System.Web.UI.Page
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
            }
        }

        void load(ProxyRedeProtecaoSocial proxy)
        {
            var idLocal = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            var recursosFinanceirosPrivado = proxy.Service.GetConsultaServicosRecursosFinanceirosPrivadoByLocalExecucao(idLocal).Where(c => c.Desativado == true).ToList();

            #region Exibicao Recursos e pivotagem dos cofinanciamentos

            var recursos = recursosFinanceirosPrivado.GroupBy(x => x.Id).Select(g => new
            {
                Id = g.First().Id
                ,
                ProtecaoSocial = g.First().ProtecaoSocial
                ,
                obj = g.First()
                ,
                Cofinanciamentos = recursosFinanceirosPrivado.Where(p => p.Id == g.First().Id).Select(x => new
                {
                    ValorCofinanciamentoEstadual = x.ValorCofinanciamentoEstadual
                                                                ,
                    Exercicio = x.Exercicio
                })
            }).ToList();


            var recursosAgrupados = recursos.GroupBy(s => s.ProtecaoSocial).OrderBy(s => s.Key).Select(g => 
                new { 
                    NomeProtecaoSocial = g.Key
                  , Recursos = g.OrderBy(i => i.obj.TipoServico) 
                }
            );

            #endregion

            lstRecursos.DataSource = recursosAgrupados;
                
            lstRecursos.DataBind();

        }

        void carregarLocalExecucao(ProxyRedeProtecaoSocial proxy)
        {
            var local = proxy.Service.GetLocalExecucaoPrivadoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"])));
            if (local == null)
                return;
            lblLocalExecucao.Text = local.Nome + " (" + (local.Unidade != null ? local.Unidade.RazaoSocial : "") + ")";
        }


        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            Response.Redirect("~/BlocoIII/VUnidadePrivada.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }
    }
}