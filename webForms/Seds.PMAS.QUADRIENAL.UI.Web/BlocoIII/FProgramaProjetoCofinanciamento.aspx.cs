using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FProgramaProjetoCofinanciamento : System.Web.UI.Page
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
                    Response.Redirect("~/BlocoIII/CProgramasProjetos.aspx");
                    return;
                }
                using (var proxy = new ProxyProgramas())
                {
                    load(proxy);
                }
            }

        }

        void verificarAlteracoes(Int32 idProgramaProjeto)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro42.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 42, idProgramaProjeto);
                    linkAlteracoesQuadro42.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("42")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idProgramaProjeto.ToString()));
                }
            }
        }

        void load(ProxyProgramas proxy)
        {
            var idProgramaProjeto = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            var programaProjeto = proxy.Service.GetProgramaProjetoById(idProgramaProjeto);
            var lst = proxy.Service.GetConsultaProgramaProjetoCofinanciamentoByProgramaProjeto(idProgramaProjeto).OrderBy(t => t.IdTipoProtecao).GroupBy(s => s.ProtecaoSocial).Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) }).ToList();
            lblProgramaProjeto.Text = programaProjeto.Nome;

            if (programaProjeto != null && programaProjeto.Nome.ToLower().Contains("amigo do idoso") && programaProjeto.ProgramaEstadual.Value == true)
            {
                lstRecursosAmigoIdoso.DataSource = lst;
                lstRecursosAmigoIdoso.DataBind();
            }
            else
            {
                lstRecursos.DataSource = lst;
                lstRecursos.DataBind();
            }
            verificarAlteracoes(idProgramaProjeto);
        }

        protected void lstRecursos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                using (var proxy = new ProxyProgramas())
                {
                    var key = Convert.ToInt32(e.Item.DataItemIndex.ToString());
                    proxy.Service.DeleteProgramaProjetoCofinanciamento(Convert.ToInt32(e.CommandArgument), key);
                    load(proxy);
                }
            }
        }

        protected void lstItems_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
                Permissao.VerificarPermissaoControles(new[] { ((ImageButton)e.Item.FindControl("btnExcluir")) }, Session);
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


        protected void btnAdicionar_Click(object sender, EventArgs e)  
        {
            var obj = new ProgramaProjetoCofinanciamentoInfo();

            try
            {

                obj.IdProgramaProjeto = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                using (var proxy = new ProxyProgramas())
                {
                    proxy.Service.AddProgramaProjetoCofinanciamento(obj);
                    load(proxy);
                }
            }
            catch (Exception)
            {
                //lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                //tbInconsistencias.Visible = true;
                return;
            }
        }

        protected string MontarBotao(ConsultaProgramaProjetoCofinanciamentoInfo item)
        {
            var idProjeto = 0; //Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var page = String.Empty;

            switch (item.TipoUnidade)
            {
                case "Rede Direta": page = "../BlocoIII/VServicoRecursoFinanceiroPublico.aspx"; break;
                case "Rede Indireta": page = "../BlocoIII/VServicoRecursoFinanceiroPrivado.aspx"; break;
                case "CRAS": page = "../BlocoIII/VServicoRecursoFinanceiroCRAS.aspx"; break;
                case "CREAS": page = "../BlocoIII/VServicoRecursoFinanceiroCREAS.aspx"; break;
                case "Centro Pop": page = "../BlocoIII/VServicoRecursoFinanceiroCentroPOP.aspx"; break;
            }
            return "<a href='" + page + "?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.IdServicoRecursoFinanceiro.ToString())) + "&idProjeto=" + idProjeto + "'><img src='../Styles/Icones/find.png' alt='Visualizar' border='0' /></a>";
        }
    }
}