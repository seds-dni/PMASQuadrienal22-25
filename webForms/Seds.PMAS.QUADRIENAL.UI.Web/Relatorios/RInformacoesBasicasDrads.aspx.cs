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
    public partial class RInformacoesBasicasDrads : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 2 - Informa&#231;&#245;es b&#225;sicas por DRADS";
            Master.MainWidthRelatorio = Master.WidthRelatorio = "800px";
            Master.GerarExcel.Click += new EventHandler(GerarExcel_Click);
            Master.GeraXLSX.Visible = false;
        }

        void carregarDados()
        {

            var filtro = new RelatorioFiltroInfo();
            string data = Session["RELATORIO_DATA_IMPLEMENTACAO"].ToString();
            data += " 23:59:59";
            filtro.DrdIDs = Session["RELATORIO_DRD_ID"] as List<int>;            
            filtro.MacroRegiaoIDs = Session["RELATORIO_MACRO_REGIAO_ID"] as List<int>;            
            filtro.Estado = Session["RELATORIO_ESTADO"] as Boolean?;
            filtro.DataImplantacao = data;

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            Master.DataDeReferencia(filtro.DataImplantacao);
            var items = new List<InformacoesBasicasDradsInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetInformacoesBasicasDrads(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalHabitantes")).Text = String.Format("{0:0,0}", items.Sum(i => i.Habitantes));
            ((Label)lst.FindControl("lblTotalCRASImplantados")).Text = String.Format("{0:0,0}", items.Sum(i => i.CRASImplantados));
           // ((Label)lst.FindControl("lblTotalCRASPrevistos")).Text = String.Format("{0:0,0}", items.Sum(i => i.CRASPrevistos));
            ((Label)lst.FindControl("lblTotalCREASImplantados")).Text = String.Format("{0:0,0}", items.Sum(i => i.CREASImplantados));
          //  ((Label)lst.FindControl("lblTotalCREASPrevistos")).Text = String.Format("{0:0,0}", items.Sum(i => i.CREASPrevistos));
            ((Label)lst.FindControl("lblTotalCentroPOPImplantados")).Text = String.Format("{0:0,0}", items.Sum(i => i.CentroPOPImplantados));
          //  ((Label)lst.FindControl("lblTotalCentroPOPPrevistos")).Text = String.Format("{0:0,0}", items.Sum(i => i.CentroPOPPrevistos));

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo2.xls");
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