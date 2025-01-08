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
    public partial class RPrestacaoDeContasProtecaoAlta : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 35 - Presta&#231;&#227; de Contas Proteção Social de Alta Complexidade";
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
            var items = new List<RPrestacaoDeContasAlta>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetPrestacaDeContasProtecaoAlta(filtro).ToList();
            }
            lstAlta.DataSource = items;
            lstAlta.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lstAlta.FindControl("lblTotalSomaProtecaoAlta")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoAlta));
            ((Label)lstAlta.FindControl("lblTotalValoresExecutadosAlta")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosAlta));
            ((Label)lstAlta.FindControl("lblTotalValoresPassiveisReprogramacaoAlta")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoAlta));
            ((Label)lstAlta.FindControl("lblTotalValoresDevolvidosAlta")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosAlta));
            ((Label)lstAlta.FindControl("lblTotalPorcentagensExecucaoAlta")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoAlta));

            ((Label)lstAlta.FindControl("lblTotalSomaProtecaoAltaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoAltaReprogramacao));
            ((Label)lstAlta.FindControl("lblTotalValoresExecutadosAltaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosAltaReprogramacao));
            ((Label)lstAlta.FindControl("lblTotalValoresPassiveisReprogramacaoAltaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoAltaReprogramacao));
            ((Label)lstAlta.FindControl("lblTotalValoresDevolvidosAltaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosAltaReprogramacao));
            ((Label)lstAlta.FindControl("lblTotalPorcentagensExecucaoAltaReprogramacao")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoAltaReprogramacao));

            ((Label)lstAlta.FindControl("lblTotalSomaProtecaoAltaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoAltaDemandas));
            ((Label)lstAlta.FindControl("lblTotalValoresExecutadosAltaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosAltaDemandas));
            ((Label)lstAlta.FindControl("lblTotalValoresPassiveisReprogramacaoAltaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoAltaDemandas));
            ((Label)lstAlta.FindControl("lblTotalValoresDevolvidosAltaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosAltaDemandas));
            ((Label)lstAlta.FindControl("lblTotalPorcentagensExecucaoAltaDemandas")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoAltaDemandas));

            ((Label)lstAlta.FindControl("lblTotalSomaProtecaoAltaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoAltaReprogramacaoDemandas));
            ((Label)lstAlta.FindControl("lblTotalValoresExecutadosAltaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosAltaReprogramacaoDemandas));
            ((Label)lstAlta.FindControl("lblTotalValoresPassiveisReprogramacaoAltaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoAltaReprogramacaoDemandas));
            ((Label)lstAlta.FindControl("lblTotalValoresDevolvidosAltaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosAltaReprogramacaoDemandas));
            ((Label)lstAlta.FindControl("lblTotalPorcentagensExecucaoAltaReprogramacaoDemandas")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoAltaReprogramacaoDemandas));


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

            var tb = ((HtmlTable)lstAlta.FindControl("tbReport"));
            tb.CellPadding = 1;
            tb.CellSpacing = 1;
            tb.Border = 1;
            tb.BorderColor = "black";
            Master.Report.RenderControl(htmlWrite);
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo35.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write(this.Master.replaceCaracteresEspeciais(stringWrite.ToString()));
            Response.End();

            tb.CellPadding = 0;
            tb.CellSpacing = 0;
            tb.Border = 0;
            tb.BorderColor = "";
        }

        protected void lstAlta_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
    }
}