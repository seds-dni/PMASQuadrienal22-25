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
    public partial class RServicosEstadualizados : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 24 - Servi&#231;os Estadualizados";
            Master.MainWidthRelatorio = Master.WidthRelatorio = "4200px";
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
            var items = new List<ServicoEstadualizadoInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetServicosEstadualizados(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotal")).Text = String.Format("{0:C2}", items.Sum(i => i.Total));
            ((Label)lst.FindControl("lblTotalFMAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMAS));
            ((Label)lst.FindControl("lblTotalFEAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEAS));
            ((Label)lst.FindControl("lblTotalFNAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNAS));
            ((Label)lst.FindControl("lblTotalFMDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMDCA));
            ((Label)lst.FindControl("lblTotalFEDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEDCA));
            ((Label)lst.FindControl("lblTotalFNDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNDCA));
            ((Label)lst.FindControl("lblTotalFMI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMI));
            ((Label)lst.FindControl("lblTotalFEI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEI));
            ((Label)lst.FindControl("lblTotalFNI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNI));
            ((Label)lst.FindControl("lblTotalPrivado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorPrivado));
            ((Label)lst.FindControl("lblTotalEstadualizado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorEstadualizado));
            ((Label)lst.FindControl("lblNumeroAtendidosMensal2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroMensalAtendidos));
            ((Label)lst.FindControl("lblTotalTrabalhadores")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalTrabalhadores));
            
            //((Label)lst.FindControl("lblNumeroAtendidosMensal2018")).Text = String.Format("{0:0,0}", 0);
            //((Label)lst.FindControl("lblNumeroAtendidosMensal2019")).Text = String.Format("{0:0,0}", 0);
            //((Label)lst.FindControl("lblNumeroAtendidosMensal2020")).Text = String.Format("{0:0,0}", 0);
            ((Label)lst.FindControl("lblMediaMensal2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaTotal2017));
            ((Label)lst.FindControl("lblMediaMensal2018")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaTotal2018));
            ((Label)lst.FindControl("lblMediaMensal2019")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaTotal2019));
            ((Label)lst.FindControl("lblMediaMensal2020")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaTotal2020));
            ((Label)lst.FindControl("lblTotalOutrasFontes")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFonteRecurso));

            //((Label)lst.FindControl("lblTotalNumeroAnualAtendidos")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAnualAtendidos));            
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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo24.xls");
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