using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Web.UI.HtmlControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Relatorios
{
    public partial class RQuantidadeUnidadesLocaisServicos : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio quantitativo 2 - Quantidade de unidades p&#250;blicas, organiza&#231;&#245;es, locais de execu&#231;&#227;o e servi&#231;os";
            Master.WidthRelatorio = "1500";
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

            Master.mostrarFiltros(filtro, ETipoRelatorio.Quantitativo);
            var items = new List<QuantidadesServicosLocaisExecucaoInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetQuantidadeServicosLocaisExecucao(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalUnidadesPublicas")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalUnidadesPublicas));
            ((Label)lst.FindControl("lblTotalLocaisPublicos")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalLocaisPublicos));
            ((Label)lst.FindControl("lblTotalServicosPublicos")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalServicosPublicos));
            ((Label)lst.FindControl("lblTotalUnidadesPrivadas")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalUnidadesPrivadas));
            ((Label)lst.FindControl("lblTotalLocaisPrivados")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalLocaisPrivados));
            ((Label)lst.FindControl("lblTotalServicosPrivados")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalServicosPrivados));
            ((Label)lst.FindControl("lblTotalCRAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalCRAS));
            ((Label)lst.FindControl("lblTotalServicosCRAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalServicosCRAS));
            ((Label)lst.FindControl("lblTotalCREAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalCREAS));
            ((Label)lst.FindControl("lblTotalServicosCREAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalServicosCREAS));
            ((Label)lst.FindControl("lblTotalCentroPOP")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalCentroPOP));
            ((Label)lst.FindControl("lblTotalServicosCentroPOP")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalServicosCentroPOP));
            ((Label)lst.FindControl("lblTotalUnidades")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalUnidadesPublicas + i.TotalUnidadesPrivadas)); // + i.TotalCRAS + i.TotalCREAS + i.TotalCentroPOP));
            ((Label)lst.FindControl("lblTotalLocais")).Text = String.Format("{0:0,0}", items.Sum(i => + i.TotalCRAS + i.TotalCREAS + i.TotalCentroPOP + i.TotalLocaisPublicos + i.TotalLocaisPrivados));
            ((Label)lst.FindControl("lblTotalServicos")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalServicosPublicos + i.TotalServicosPrivados + i.TotalServicosCRAS + i.TotalServicosCREAS + i.TotalServicosCentroPOP));

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioQuantitativo2.xls");
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