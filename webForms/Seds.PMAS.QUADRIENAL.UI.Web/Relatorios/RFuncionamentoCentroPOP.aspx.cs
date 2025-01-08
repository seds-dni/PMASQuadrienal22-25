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
    public partial class RFuncionamentoCentroPOP : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 15 - Funcionamento dos Centros POP";
            Master.WidthRelatorio = "2600px";
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
            var items = new List<FuncionamentoCentroPOPInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetFuncionamentoCentroPOP(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;
            
            ((Label)lst.FindControl("lblTotalNumeroAtendidos")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidos));
            ((Label)lst.FindControl("lblTotalFuncionarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.Funcionarios));
            ((Label)lst.FindControl("lblTotalServicoEspecializadoSituacaoRuaCriancas")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoEspecializadoSituacaoRuaCriancas == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoEspecializadoSituacaoRuaJovens")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoEspecializadoSituacaoRuaJovens == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoEspecializadoSituacaoRuaAdultos")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoEspecializadoSituacaoRuaAdultos == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoAbordagemCriancasAdolescentes")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoAbordagemCriancasJovens == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoAbordagemJovensAdultosIdososFamilias")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoAbordagemJovensAdultosIdososFamilias == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoAbordagemCriancasJovensAdultosIdososFamilias")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoAbordagemCriancasJovensAdultosIdososFamilias == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicoNaoTipificado")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiServicoNaoTipificado == "Sim" ? 1 : 0));
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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo15.xls");
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