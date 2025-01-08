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
    public partial class RPrestacaoDeContas : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 33 - Presta&#231;&#227; de Contas Proteção Social Básica";
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
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<RPrestacaoDeContasBasica>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetPrestacaDeContasProtecaoBasica(filtro).ToList();
            }
            lstBasica.DataSource = items;
            lstBasica.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lstBasica.FindControl("lblTotalSomaProtecaoBasica")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoBasica));
            ((Label)lstBasica.FindControl("lblTotalValoresExecutadosBasica")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosBasica));
            ((Label)lstBasica.FindControl("lblTotalValoresPassiveisReprogramacaoBasica")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoBasica));
            ((Label)lstBasica.FindControl("lblTotalValoresDevolvidosBasica")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosBasica));
            ((Label)lstBasica.FindControl("lblTotalPorcentagensExecucaoBasica")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoBasica));

            ((Label)lstBasica.FindControl("lblTotalSomaProtecaoBasicaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoBasicaReprogramacao));
            ((Label)lstBasica.FindControl("lblTotalValoresExecutadosBasicaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosBasicaReprogramacao));
            ((Label)lstBasica.FindControl("lblTotalValoresPassiveisReprogramacaoBasicaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoBasicaReprogramacao));
            ((Label)lstBasica.FindControl("lblTotalValoresDevolvidosBasicaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosBasicaReprogramacao));
            ((Label)lstBasica.FindControl("lblTotalPorcentagensExecucaoBasicaReprogramacao")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoBasicaReprogramacao));

            ((Label)lstBasica.FindControl("lblTotalSomaProtecaoBasicaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoBasicaDemandas));
            ((Label)lstBasica.FindControl("lblTotalValoresExecutadosBasicaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosBasicaDemandas));
            ((Label)lstBasica.FindControl("lblTotalValoresPassiveisReprogramacaoBasicaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoBasicaDemandas));
            ((Label)lstBasica.FindControl("lblTotalValoresDevolvidosBasicaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosBasicaDemandas));
            ((Label)lstBasica.FindControl("lblTotalPorcentagensExecucaoBasicaDemandas")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoBasicaDemandas));

            ((Label)lstBasica.FindControl("lblTotalSomaProtecaoBasicaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoBasicaReprogramacaoDemandas));
            ((Label)lstBasica.FindControl("lblTotalValoresExecutadosBasicaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosBasicaReprogramacaoDemandas));
            ((Label)lstBasica.FindControl("lblTotalValoresPassiveisReprogramacaoBasicaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoBasicaReprogramacaoDemandas));
            ((Label)lstBasica.FindControl("lblTotalValoresDevolvidosBasicaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosBasicaReprogramacaoDemandas));
            ((Label)lstBasica.FindControl("lblTotalPorcentagensExecucaoBasicaReprogramacaoDemandas")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoBasicaReprogramacaoDemandas));


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

            var tb = ((HtmlTable)lstBasica.FindControl("tbReport"));
            tb.CellPadding = 1;
            tb.CellSpacing = 1;
            tb.Border = 1;
            tb.BorderColor = "black";
            Master.Report.RenderControl(htmlWrite);
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo33.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write(this.Master.replaceCaracteresEspeciais(stringWrite.ToString()));
            Response.End();

            tb.CellPadding = 0;
            tb.CellSpacing = 0;
            tb.Border = 0;
            tb.BorderColor = "";
        }

        protected void lstBasica_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }


    }
}