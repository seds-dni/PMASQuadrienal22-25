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
    public partial class RDistribuicaoEstadualProtecaoSocial : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 27 - Distribui&#231;&#227;o dos recursos do cofinanciamento estadual, segundo as prote&#231;&#245;es sociais (INCLUINDO VALORES REPROGRAMADOS DO EXERC&#314;CIO ANTERIOR)";
            Master.MainWidthRelatorio =  Master.WidthRelatorio = "2000px";
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
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;
            filtro.Estado = Session["RELATORIO_ESTADO"] as Boolean?;

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<DistribuicaoEstadualProtecaoSocialInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetDistribuicaoEstadualProtecaoSocial(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalRedePublicaBasica")).Text = String.Format("{0:N2}", items.Sum(i => i.RedePublicaBasica ));
            ((Label)lst.FindControl("lblTotalRedePrivadaBasica")).Text = String.Format("{0:N2}", items.Sum(i => i.RedePrivadaBasica));
            //((Label)lst.FindControl("lblTotalSaoPauloSolidario")).Text = String.Format("{0:N2}", items.Sum(i => i.SaoPauloSolidario));
            ((Label)lst.FindControl("lblTotalImplantacaoCRAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ImplantacaoCRAS));
            //((Label)lst.FindControl("lblTotalFamiliaPaulista")).Text = String.Format("{0:N2}", items.Sum(i => i.FamiliaPaulista));
            ((Label)lst.FindControl("lblTotalBasica")).Text = String.Format("{0:N2}", items.Sum(i => i.TotalBasica));

            ((Label)lst.FindControl("lblTotalRedePublicaMedia")).Text = String.Format("{0:N2}", items.Sum(i => i.RedePublicaEspecialMedia));
            ((Label)lst.FindControl("lblTotalRedePrivadaMedia")).Text = String.Format("{0:N2}", items.Sum(i => i.RedePrivadaEspecialMedia));
            ((Label)lst.FindControl("lblTotalMedia")).Text = String.Format("{0:N2}", items.Sum(i => i.TotalEspecialMedia));

            ((Label)lst.FindControl("lblTotalRedePublicaAlta")).Text = String.Format("{0:N2}", items.Sum(i => i.RedePublicaEspecialAlta));
            ((Label)lst.FindControl("lblTotalRedePrivadaAlta")).Text = String.Format("{0:N2}", items.Sum(i => i.RedePrivadaEspecialAlta));
            ((Label)lst.FindControl("lblTotalAlta")).Text = String.Format("{0:N2}", items.Sum(i => i.TotalEspecialAlta));

            ((Label)lst.FindControl("lblTotalRedePublica")).Text = String.Format("{0:N2}", items.Sum(i => i.TotalRedePublica));
            ((Label)lst.FindControl("lblTotalRedePrivada")).Text = String.Format("{0:N2}", items.Sum(i => i.TotalRedePrivada));
            ((Label)lst.FindControl("lblTotal")).Text = String.Format("{0:N2}", items.Sum(i => i.Total));            

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo26.xls");
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