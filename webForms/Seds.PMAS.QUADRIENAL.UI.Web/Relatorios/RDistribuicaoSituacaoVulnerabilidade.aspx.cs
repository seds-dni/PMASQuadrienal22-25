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
    public partial class RDistribuicaoSituacaoVulnerabilidade : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio quantitativo 3 - Distribui&#231;&#227;o dos munic&#237;pios de acordo com as situa&#231;&#245;es de vulnerabilidade apontadas";
            Master.WidthRelatorio = "1000";   
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
            var items = new List<DistribuicaoSituacaoVulnerabilidadeInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetDistribuicaoSituacaoVulnerabilidade(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalGravidade1")).Text = String.Format("{0:0,0}", items.Sum(i => i.Gravidade1));
            ((Label)lst.FindControl("lblTotalGravidade2")).Text = String.Format("{0:0,0}", items.Sum(i => i.Gravidade2));
            ((Label)lst.FindControl("lblTotalGravidade3")).Text = String.Format("{0:0,0}", items.Sum(i => i.Gravidade3));
            ((Label)lst.FindControl("lblTotalGravidade4")).Text = String.Format("{0:0,0}", items.Sum(i => i.Gravidade4));
            ((Label)lst.FindControl("lblTotalGravidade5")).Text = String.Format("{0:0,0}", items.Sum(i => i.Gravidade5));
            ((Label)lst.FindControl("lblTotalGravidade6")).Text = String.Format("{0:0,0}", items.Sum(i => i.Gravidade6));
            ((Label)lst.FindControl("lblTotalGravidade7")).Text = String.Format("{0:0,0}", items.Sum(i => i.Gravidade7));
            ((Label)lst.FindControl("lblTotalGravidade8")).Text = String.Format("{0:0,0}", items.Sum(i => i.Gravidade8));
            ((Label)lst.FindControl("lblTotalGravidade9")).Text = String.Format("{0:0,0}", items.Sum(i => i.Gravidade9));
            ((Label)lst.FindControl("lblTotalGravidade10")).Text = String.Format("{0:0,0}", items.Sum(i => i.Gravidade10));
            ((Label)lst.FindControl("lblTotal")).Text = String.Format("{0:0,0}", items.Sum(i => i.Total));            

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioQuantitativo3.xls");
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