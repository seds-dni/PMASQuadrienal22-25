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
    public partial class RFuncionamentoCRAS : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 13 - Funcionamento dos CRAS";
            Master.WidthRelatorio = "2160px";
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
            var items = new List<FuncionamentoCRASInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetFuncionamentoCRAS(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalFamiliasReferenciadas")).Text = String.Format("{0:0,0}", items.Sum(i => i.FamiliasReferenciadas));
            ((Label)lst.FindControl("lblTotalFamiliasAtendidas")).Text = String.Format("{0:0,0}", items.Sum(i => i.FamiliasAtendidas));
            ((Label)lst.FindControl("lblTotalFuncionarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.Funcionarios));
            ((Label)lst.FindControl("lblTotalPAIF")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiPAIF == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoConvivencia6anos")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoConvivencia6anos == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoConvivencia15anos")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoConvivencia15anos == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoConvivencia17anos")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoConvivencia17anos == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoConvivencia60anos")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoConvivencia60anos == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoProtecaoPessoasDeficientes")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoProtecaoPessoasDeficientes == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoProtecaoPessoasIdosas")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoProtecaoPessoasIdosas == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoProtecaoPessoasDeficientesIdosas")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoProtecaoPessoasDeficientesIdosas == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoNaoTipificado")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoNaoTipificado == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalEquipeVolante")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiEquipeVolante == "Sim" ? 1 : 0));

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo13.xls");
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