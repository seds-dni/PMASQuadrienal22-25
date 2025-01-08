using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using System.Web.UI.HtmlControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Relatorios
{
    public partial class RRHLocalExecucao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            carregarPagina();

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);
                carregarDados();
            }
        }

        void carregarPagina()
        {
            Master.Titulo = "Relat&#243;rio descritivo 5 - Recursos humanos dos serviços";
            Master.MainWidthRelatorio = Master.WidthRelatorio = "3060px";
            Master.GerarExcel.Click += new EventHandler(GerarExcel_Click);
            Master.GeraXLSX.Visible = false;
        }

        void carregarDados()
        {

            var filtro = new RelatorioFiltroInfo();
            filtro.MunIDs = Session["RELATORIO_MUN_ID"] as List<int>;
            filtro.DrdIDs = Session["RELATORIO_DRD_ID"] as List<int>;
            filtro.RegIDs = Session["RELATORIO_REG_ID"] as List<int>;
            filtro.MacroRegiaoIDs = Session["RELATORIO_MACRO_REGIAO_ID"] as List<int>;
            filtro.Portes = Session["RELATORIO_PORTE_ID"] as List<int>;
            filtro.NiveisGestao = Session["RELATORIO_NIVEL_GESTAO_ID"] as List<int>;
            filtro.Estado = Session["RELATORIO_ESTADO"] as Boolean?;

            filtro.TipoExecutora = Session["RELATORIO_TIPO_EXECUTORA"] as List<ETipoUnidade>;
            filtro.TipoProtecaoSocial = Session["RELATORIO_TIPO_PROTECAO_ID"] as int?;
            filtro.TipoServico = Session["RELATORIO_TIPO_SERVICO_ID"] as int?;
            filtro.ServicoSubtificado = Session["RELATORIO_SERVICO_SUBTIFICADO_ID"] as int?;
            filtro.Usuario = Session["RELATORIO_PUBLICO_ALVO_ID"] as int?;

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<RHRedeExecutoraInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetRHRedeExecutora(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalFuncionarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionarios));
            ((Label)lst.FindControl("lblTotalFuncionariosSemEscolaridade")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSemEscolaridade));
            ((Label)lst.FindControl("lblTotalFuncionariosNivelFundamental")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosNivelFundamental));
            ((Label)lst.FindControl("lblTotalFuncionariosNivelMedio")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosNivelMedio));
            ((Label)lst.FindControl("lblTotalFuncionariosNivelSuperior")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperior));
            ((Label)lst.FindControl("lblTotalFuncionariosServicoSocial")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperiorServicoSocial));
            ((Label)lst.FindControl("lblTotalFuncionariosPsicologia")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperiorPsicologia));
            ((Label)lst.FindControl("lblTotalFuncionariosPedagogia")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperiorPedagogia));
            ((Label)lst.FindControl("lblTotalFuncionariosSociologia")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperiorSociologia));
            ((Label)lst.FindControl("lblTotalFuncionariosAntropologia")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperiorAntropologia));
            ((Label)lst.FindControl("lblTotalFuncionariosTerapiaOcupacional")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperiorTerapiaOcupacional));
            ((Label)lst.FindControl("lblTotalFuncionariosDireito")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperiorDireito));
            ((Label)lst.FindControl("lblTotalFuncionariosMusicoterapia")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperiorMusicoterapia));
            ((Label)lst.FindControl("lblTotalFuncionariosEconomia")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperiorEconomia));
            ((Label)lst.FindControl("lblTotalFuncionariosEconomiaDomestica")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosSuperiorEconomiaDomestica));
            ((Label)lst.FindControl("lblTotalTrabalhadoresExclusivo")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalExclusivoServico));
            ((Label)lst.FindControl("lblTotalTrabalhadoresCompartilhados")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalOutrosServicosAssistenciais));
            ((Label)lst.FindControl("lblTotalEstagiarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalEstagiarios));
            ((Label)lst.FindControl("lblTotalVoluntarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalVoluntarios));
            ((Label)lst.FindControl("lblTotalFuncionariosOutrasAreas")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionariosOutrasAreas));

        }

        protected void GerarExcel_Click(object sender, EventArgs e)
        {
            gerarExcel();
        }

        void gerarExcel()
        {
            carregarDados();
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            var tb = ((HtmlTable)lst.FindControl("tbReport"));
            tb.CellPadding = 1;
            tb.CellSpacing = 1;
            tb.Border = 1;
            tb.BorderColor = "black";

            Master.Report.RenderControl(htmlWrite);
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo5.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write(this.Master.replaceCaracteresEspeciais(stringWrite.ToString()));
            Response.End();

            tb.CellPadding = 0;
            tb.CellSpacing = 0;
            tb.Border = 0;
            tb.BorderColor = "";
        }

        protected void lst_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
    }
}