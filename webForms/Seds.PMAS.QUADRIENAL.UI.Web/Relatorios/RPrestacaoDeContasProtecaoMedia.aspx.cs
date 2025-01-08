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
    public partial class RPrestacaoDeContasProtecaoMedia : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 34 - Presta&#231;&#227; de Contas Proteção Social de Média Complexidade";
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
            var items = new List<RPrestacaoDeContasMedia>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetPrestacaDeContasProtecaoMedia(filtro).ToList();
            }
            lstMedia.DataSource = items;
            lstMedia.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lstMedia.FindControl("lblTotalSomaProtecaoMedia")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoMedia));
            ((Label)lstMedia.FindControl("lblTotalValoresExecutadosMedia")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosMedia));
            ((Label)lstMedia.FindControl("lblTotalValoresPassiveisReprogramacaoMedia")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoMedia));
            ((Label)lstMedia.FindControl("lblTotalValoresDevolvidosMedia")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosMedia));
            ((Label)lstMedia.FindControl("lblTotalPorcentagensExecucaoMedia")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoMedia));

            ((Label)lstMedia.FindControl("lblTotalSomaProtecaoMediaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoMediaReprogramacao));
            ((Label)lstMedia.FindControl("lblTotalValoresExecutadosMediaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosMediaReprogramacao));
            ((Label)lstMedia.FindControl("lblTotalValoresPassiveisReprogramacaoMediaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoMediaReprogramacao));
            ((Label)lstMedia.FindControl("lblTotalValoresDevolvidosMediaReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosMediaReprogramacao));
            ((Label)lstMedia.FindControl("lblTotalPorcentagensExecucaoMediaReprogramacao")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoMediaReprogramacao));

            ((Label)lstMedia.FindControl("lblTotalSomaProtecaoMediaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoMediaDemandas));
            ((Label)lstMedia.FindControl("lblTotalValoresExecutadosMediaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosMediaDemandas));
            ((Label)lstMedia.FindControl("lblTotalValoresPassiveisReprogramacaoMediaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoMediaDemandas));
            ((Label)lstMedia.FindControl("lblTotalValoresDevolvidosMediaDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosMediaDemandas));
            ((Label)lstMedia.FindControl("lblTotalPorcentagensExecucaoMediaDemandas")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoMediaDemandas));

            ((Label)lstMedia.FindControl("lblTotalSomaProtecaoMediaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoMediaReprogramacaoDemandas));
            ((Label)lstMedia.FindControl("lblTotalValoresExecutadosMediaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosMediaReprogramacaoDemandas));
            ((Label)lstMedia.FindControl("lblTotalValoresPassiveisReprogramacaoMediaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoMediaReprogramacaoDemandas));
            ((Label)lstMedia.FindControl("lblTotalValoresDevolvidosMediaReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosMediaReprogramacaoDemandas));
            ((Label)lstMedia.FindControl("lblTotalPorcentagensExecucaoMediaReprogramacaoDemandas")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoMediaReprogramacaoDemandas));


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

            var tb = ((HtmlTable)lstMedia.FindControl("tbReport"));
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

        protected void lstMedia_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
    }
}