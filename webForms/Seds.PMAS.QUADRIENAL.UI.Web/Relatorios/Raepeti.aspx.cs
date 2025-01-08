using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Relatorios
{
    public partial class Raepeti : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 32 - AEPETI";
            Master.MainWidthRelatorio = Master.WidthRelatorio = "1300px";
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
            var items = new List<RelatorioAEPETIInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetAEPETIRelatorio(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalValorMensalDoCofinanciamento")).Text = String.Format("{0:0,0}", items.Sum(i => i.ValorAepeti));
            ((Label)lst.FindControl("lblTotalIdade10a13Ano2021")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade10a13Ano2021));
            ((Label)lst.FindControl("lblTotalIdade10a13Ano2022")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade10a13Ano2022));
            ((Label)lst.FindControl("lblTotalIdade10a13Ano2023")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade10a13Ano2023));
            ((Label)lst.FindControl("lblTotalIdade10a13Ano2024")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade10a13Ano2024));

            ((Label)lst.FindControl("lblTotalIdade14a15Ano2021")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade14a15Ano2021));
            ((Label)lst.FindControl("lblTotalIdade14a15Ano2022")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade14a15Ano2022));
            ((Label)lst.FindControl("lblTotalIdade14a15Ano2023")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade14a15Ano2023));
            ((Label)lst.FindControl("lblTotalIdade14a15Ano2024")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade14a15Ano2024));

            ((Label)lst.FindControl("lblTotalIdade16a17Ano2021")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade16a17Ano2021));
            ((Label)lst.FindControl("lblTotalIdade16a17Ano2022")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade16a17Ano2022));
            ((Label)lst.FindControl("lblTotalIdade16a17Ano2023")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade16a17Ano2023));
            ((Label)lst.FindControl("lblTotalIdade16a17Ano2024")).Text = String.Format("{0:0,0}", items.Sum(i => i.Idade16a17Ano2024));

            ((Label)lst.FindControl("lblTotalMetaMunicipal2021")).Text = String.Format("{0:0,0}", items.Sum(i => i.MetaMunicipal2021));
            ((Label)lst.FindControl("lblTotalMetaMunicipal2022")).Text = String.Format("{0:0,0}", items.Sum(i => i.MetaMunicipal2022));
            ((Label)lst.FindControl("lblTotalMetaMunicipal2023")).Text = String.Format("{0:0,0}", items.Sum(i => i.MetaMunicipal2023));
            ((Label)lst.FindControl("lblTotalMetaMunicipal2024")).Text = String.Format("{0:0,0}", items.Sum(i => i.MetaMunicipal2024));

            

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

            if (tb == null)
                tb = ((HtmlTable)lst.FindControl("tbReport"));
            tb.CellPadding = 1;
            tb.CellSpacing = 1;
            tb.Border = 1;
            tb.BorderColor = "black";
            
            Master.Report.RenderControl(htmlWrite);
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo32.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write(this.Master.replaceCaracteresEspeciais(stringWrite.ToString()));
            Response.End();

            //tb.CellPadding = 0;
            //tb.CellSpacing = 0;
            //tb.Border = 0;
            //tb.BorderColor = "";
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