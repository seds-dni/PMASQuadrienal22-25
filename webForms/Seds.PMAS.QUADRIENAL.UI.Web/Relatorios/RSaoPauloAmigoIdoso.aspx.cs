﻿using System;
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
    public partial class RSaoPauloAmigoIdoso : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 16 - Programa São Paulo Amigo do Idoso";
            Master.WidthRelatorio = "1200";
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
            var items = new List<SaoPauloAmigoIdosoInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetProgramaSaoPauloAmigoIdoso(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalMetaPactuada")).Text = String.Format("{0:0,0}", items.Sum(i => i.MetaPactuada));
            ((Label)lst.FindControl("lblTotalPrevisaoAnualRepasse")).Text = String.Format("{0:C2}", items.Sum(i => i.ValorPrevisaoAnualRepasse));
            ((Label)lst.FindControl("lblTotalCentroDiaIdoso")).Text = String.Format("{0:C2}", items.Sum(i => i.ValorDiaIdoso));
            ((Label)lst.FindControl("lblTotalCentroConvivenciaIdoso")).Text = String.Format("{0:C2}", items.Sum(i => i.ValorConvivenciaIdoso));
            ((Label)lst.FindControl("lblTotalPossuiParceria")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiParceriaFormal == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalParcerias")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalParcerias));
            ((Label)lst.FindControl("lblTotalPossuiServicosAssociados")).Text = String.Format("{0:0,0}", items.Sum(i => i.IntegracaoServicos == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicosAssociados")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalServicosAssociados));
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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo16.xls");
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