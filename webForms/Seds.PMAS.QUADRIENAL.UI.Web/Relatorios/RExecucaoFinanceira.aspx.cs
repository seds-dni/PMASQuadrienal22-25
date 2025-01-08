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
    public partial class RExecucaoFinanceira : System.Web.UI.Page
    {
        Int32 _counter = 0;
        Int32 _index = 0;
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
            Master.Titulo = "Relat&#243;rio descritivo 30 - Execu&#231;&#227;o dos recursos de cofinanciamento estadual";
            Master.MainWidthRelatorio = Master.WidthRelatorio = "1300";
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

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<ExecucaoRecursosCofinanciamentoEstadualInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetExecucaoRecursosCofinanciamentoEstadual(filtro).ToList();
            }
            lst.DataSource = items.GroupBy(t => t.Municipio).Select(t =>
                new
                {
                    Key = t.Key,
                    Items = t,
                    PrevisaoInicialFEAS = t.Sum(obj => obj.PrevisaoInicialFEAS),
                    RecursoDisponibilizadoFEAS = t.Sum(obj => obj.RecursoDisponibilizadoFEAS),
                    ResultadoAplicacaoFinanceiraFEAS = t.Sum(obj => obj.ResultadoAplicacaoFinanceiraFEAS),
                    ValoresExecutadosFEAS = t.Sum(obj => obj.ValoresExecutadosFEAS),
                    ValoresReprogramadosFEAS = t.Sum(obj => obj.ValoresReprogramadosFEAS),
                    ValoresDevolvidosFEAS = t.Sum(obj => obj.ValoresDevolvidosFEAS),
                    PorcentagemExecucaoFEAS = String.Format("{0:P2}", t.Sum(obj => obj.ValoresExecutadosFEAS) / (t.Sum(obj => obj.RecursoDisponibilizadoFEAS) != 0 || t.Sum(obj => obj.ResultadoAplicacaoFinanceiraFEAS) != 0 ? t.Sum(obj => obj.RecursoDisponibilizadoFEAS) + t.Sum(obj => obj.ResultadoAplicacaoFinanceiraFEAS) : 1))
                });
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalPrevisaoInicialRepasse")).Text = String.Format("{0:N2}", items.Sum(i => i.PrevisaoInicialFEAS));
            ((Label)lst.FindControl("lblTotalRecursosDisponibilizados")).Text = String.Format("{0:N2}", items.Sum(i => i.RecursoDisponibilizadoFEAS));
            ((Label)lst.FindControl("lblTotalResultadoAplicacoesFinanceiras")).Text = String.Format("{0:N2}", items.Sum(i => i.ResultadoAplicacaoFinanceiraFEAS));
            ((Label)lst.FindControl("lblTotalValoresExecutados")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosFEAS));
            ((Label)lst.FindControl("lblTotalValoresReprogramados")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresReprogramadosFEAS));
            ((Label)lst.FindControl("lblTotalValoresDevolvidos")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosFEAS));
            //((Label)lst.FindControl("lblTotalPorcentagemExecucao")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagemExecucaoFEAS));
            ((Label)lst.FindControl("lblTotalPorcentagemExecucao")).Text = String.Format("{0:P2}", items.Sum(i => i.ValoresExecutadosFEAS) / (items.Sum(i => i.RecursoDisponibilizadoFEAS) != 0 || items.Sum(i => i.ResultadoAplicacaoFinanceiraFEAS) != 0 ? items.Sum(i => i.RecursoDisponibilizadoFEAS) + items.Sum(i => i.ResultadoAplicacaoFinanceiraFEAS) : 1));

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo30.xls");
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
            _counter++;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                if (_counter % 3 == 2)
                {
                    _index++;
                    ((Label)e.Item.FindControl("lblSequencia")).Text = _index.ToString();
                }
                else
                {
                    ((Label)e.Item.FindControl("lblSequencia")).Text = "";
                }
            }
        }

    }
}