using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FBeneficioEventualServicos : System.Web.UI.Page
    {
        private List<Int32> _lstTipoUnidade = new List<int>();  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["lstTipoUnidade"] != null)  
            {
                _lstTipoUnidade = (List<Int32>)ViewState["lstTipoUnidade"];
            }

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
                    Response.Redirect("~/BlocoIII/CBeneficioEventual.aspx");
                    return;
                }
                using (var proxy = new ProxyProgramas())
                {
                    load(proxy);
                }

            }

        }

        void load(ProxyProgramas proxy)
        {
            var idPrefeituraBeneficioEventual = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            var beneficioEventualServicos = proxy.Service.GetConsultaBeneficioEventualServicosByBeneficioEventual(idPrefeituraBeneficioEventual)
                .OrderBy(s => s.IdTipoProtecao)
                .OrderBy(s => s.IdTipoUnidade)
                .OrderBy(s => s.Exercicio)
                .GroupBy(s => s.Id)
                .Select(g => new 
                {
                    key = g.First().ProtecaoSocial,
                    Items = g.GroupBy(i => i.Id)
                        .Select(g1 => new ConsultaPrefeituraBeneficioEventualRecursoFinanceiroInfo()
                        {
                            Id = g1.First().Id
                         ,
                            TipoUnidade = g.First().TipoUnidade
                         ,
                            Unidade = g.First().Unidade
                         ,
                            TipoServico = g.First().TipoServico
                         ,
                            Usuario = g.First().Usuario
                         ,
                            NumerosAtendidos = g.Select(s1 => new KeyValuePair<Int32, Int32>(s1.Exercicio, s1.NumeroAtendidos.HasValue ? s1.NumeroAtendidos.Value : 0)).ToList<KeyValuePair<Int32, Int32>>()
                         ,
                            NumeroBeneficiarios = g.First().NumeroBeneficiarios
                         ,
                            IdServicoRecursoFinanceiro = g.First().IdServicoRecursoFinanceiro
                        })
                }).ToList();


            lstRecursos.DataSource = beneficioEventualServicos;
            lstRecursos.DataBind();

            verificarAlteracoes(idPrefeituraBeneficioEventual);
        }

        void verificarAlteracoes(Int32 idPrefeituraBeneficioEventual)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro78.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 78, idPrefeituraBeneficioEventual);
                    linkAlteracoesQuadro78.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("78")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idPrefeituraBeneficioEventual.ToString()));
                }
            }
        }

        protected void lstRecursos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                using (var proxy = new ProxyProgramas())
                {
                    proxy.Service.DeleteBeneficioEventualServico(Convert.ToInt32(e.CommandArgument));
                    load(proxy);
                }
            }
        }

        protected void lstItems_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Permissao.VerificarPermissaoControles(new[] { ((ImageButton)e.Item.FindControl("btnExcluir")) }, Session);
            }
        }

        protected void ddlTipoExecutora_SelectedIndexChanged(object sender, EventArgs e)
        {          
          
        }

        void carregarUnidadesPublicas(ProxyRedeProtecaoSocial proxy)
        {
         
        }

        void carregarUnidadesPrivadas(ProxyRedeProtecaoSocial proxy)
        {
         
        }

        void carregarLocalExecucaoPrivado(ProxyRedeProtecaoSocial proxy)
        {
         
        }

        void carregarLocalExecucaoPublico(ProxyRedeProtecaoSocial proxy)
        {
            var listaGeral = new[] { new { Id = 0, Descricao = "0", Tipo = 0 } }.ToList();
            listaGeral.Clear();
            _lstTipoUnidade.Clear();
            for (int i = 0; i < listaGeral.Count; i++)
            {
                _lstTipoUnidade.Add(listaGeral[i].Tipo);
            }
            ViewState["lstTipoUnidade"] = _lstTipoUnidade;
        }

        void carregarCRAS(ProxyRedeProtecaoSocial proxy)
        {
        }

        void carregarCREAS(ProxyRedeProtecaoSocial proxy)
        {
        }

        void carregarCentroPOP(ProxyRedeProtecaoSocial proxy)
        {
        }

        void carregarServicosCRAS(ProxyRedeProtecaoSocial proxy)
        {
        }

        void carregarServicosCREAS(ProxyRedeProtecaoSocial proxy)
        {
        }

        void carregarServicosCentroPOP(ProxyRedeProtecaoSocial proxy)
        {
        }

        void carregarServicosLocalExecucaoPrivado(ProxyRedeProtecaoSocial proxy)
        {
   
        }

        void carregarServicosLocalExecucaoPublico(ProxyRedeProtecaoSocial proxy)
        {
   
        }

        protected void ddlExecutora_SelectedIndexChanged(object sender, EventArgs e)  
        {
   
        }

        protected void ddlLocalExecucao_SelectedIndexChanged(object sender, EventArgs e)  
        {
           
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
           
        }

        protected string MontarBotao(ConsultaPrefeituraBeneficioEventualRecursoFinanceiroInfo item)
        {
            var idProjeto = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var page = String.Empty;
            switch (item.TipoUnidade)
            {
                case "Unidade Pública": page = "../BlocoIII/VServicoRecursoFinanceiroPublico.aspx"; break;
                case "Unidade Privada": page = "../BlocoIII/VServicoRecursoFinanceiroPrivado.aspx"; break;
                case "CRAS": page = "../BlocoIII/VServicoRecursoFinanceiroCRAS.aspx"; break;
                case "CREAS": page = "../BlocoIII/VServicoRecursoFinanceiroCREAS.aspx"; break;
                case "Centro Pop": page = "../BlocoIII/VServicoRecursoFinanceiroCentroPOP.aspx"; break;
            }
            return "<a href='" + page + "?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.IdServicoRecursoFinanceiro.ToString())) + "&idProjeto=" + idProjeto + "'><img src='../Styles/Icones/find.png' alt='Visualizar' border='0' /></a>";
        }
    }
}