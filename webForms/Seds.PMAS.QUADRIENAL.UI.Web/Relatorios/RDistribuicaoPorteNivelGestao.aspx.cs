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
    public partial class RDistribuicaoPorteNivelGestao : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio quantitativo 1 - Distribui&#231;&#227;o dos munic&#237;pios segundo porte e n&#237;vel de gest&#227;o";
            Master.WidthRelatorio = "800px";   
            Master.GerarExcel.Click += new EventHandler(GerarExcel_Click);
            Master.GeraXLSX.Visible = false;
        }

        void carregarDados()
        {

            var filtro = new RelatorioFiltroInfo();
            string data = Session["RELATORIO_DATA_IMPLEMENTACAO"].ToString();
            data += " 23:59:59";
            filtro.MunIDs = Session["RELATORIO_MUN_ID"] as List<int>;
            filtro.DrdIDs = Session["RELATORIO_DRD_ID"] as List<int>;
            filtro.RegIDs = Session["RELATORIO_REG_ID"] as List<int>;
            filtro.MacroRegiaoIDs = Session["RELATORIO_MACRO_REGIAO_ID"] as List<int>;
            filtro.Estado = Session["RELATORIO_ESTADO"] as Boolean?;
            filtro.DataImplantacao = data;

            Master.mostrarFiltros(filtro, ETipoRelatorio.Quantitativo);
            var items = new List<DistribuicaoPorteNivelGestaoInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetDistribuicaoMunicipiosPorteNivelGestao(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalInicial")).Text = String.Format("{0:0,0}", items.Sum(i => i.Inicial));
            ((Label)lst.FindControl("lblTotalBasica")).Text = String.Format("{0:0,0}", items.Sum(i => i.Basica));
            ((Label)lst.FindControl("lblTotalPlena")).Text = String.Format("{0:0,0}", items.Sum(i => i.Plena));
            ((Label)lst.FindControl("lblTotalNaoHabilitado")).Text = String.Format("{0:0,0}", items.Sum(i => i.NaoHabilitado));
            ((Label)lst.FindControl("lblTotal")).Text = String.Format("{0:0,0}", items.Sum(i => i.Total));
            var total = items.Sum(obj => obj.Total);
            ((Label)lst.FindControl("lblPorcentagemInicial")).Text = ((100m * items.Sum(obj => obj.Inicial)) / total).ToString("N2") + "%";
            ((Label)lst.FindControl("lblPorcentagemBasica")).Text = ((100m * items.Sum(obj => obj.Basica)) / total).ToString("N2") + "%";
            ((Label)lst.FindControl("lblPorcentagemPlena")).Text = ((100m * items.Sum(obj => obj.Plena)) / total).ToString("N2") + "%";
            ((Label)lst.FindControl("lblPorcentagemNaoHabilitado")).Text = ((100m * items.Sum(obj => obj.NaoHabilitado)) / total).ToString("N2") + "%";

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioQuantitativo1.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write(this.Master.replaceCaracteresEspeciais(stringWrite.ToString()));
            Response.End();

            tb.CellPadding = 0;
            tb.CellSpacing = 0;
            tb.Border = 0;
            tb.BorderColor = "";
        }      
    }
}