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
    public partial class RComunidadesGrupos : System.Web.UI.Page
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

        private void carregarDados()
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
            var items = new List<ComunidadesPovosGruposEspecificosInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetAnaliseDiagnosticaComunidades(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();


            ((Label)lst.FindControl("lblTotalPresencaCiganos")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteCiganos ? 1 : 0));
            ((Label)lst.FindControl("lblTotalFamiliasCiganos")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroCiganos));
            ((Label)lst.FindControl("lblTotalPresencaExtrativistas")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteExtrativistas ? 1 : 0));
            ((Label)lst.FindControl("lblTotalFamiliasExtrativistas")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroExtrativistas));
            ((Label)lst.FindControl("lblTotalPresencaPescadores")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExistePescadores ? 1 : 0));
            ((Label)lst.FindControl("lblTotalFamiliaPescadores")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroPescadores));
            ((Label)lst.FindControl("lblTotalPresencaAfro")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteAfros ? 1 : 0));
            ((Label)lst.FindControl("lblTotalFamiliaAfros")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAfros));
            ((Label)lst.FindControl("lblPresencaRibeirinha")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteRibeirinha ? 1 : 0));
            ((Label)lst.FindControl("lblTotalFamiliaRibeirinha")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroRibeirinhas));
            ((Label)lst.FindControl("lblPresencaIndigena")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteIndigena ? 1 : 0));
            ((Label)lst.FindControl("lblTotalFamiliaIndigena")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroIndigenas));
            ((Label)lst.FindControl("lblPresencaQuilombola")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteQuilombola ? 1 : 0));
            ((Label)lst.FindControl("lblTotalFamiliaQuilombola")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroQuilombolas));
            ((Label)lst.FindControl("lblPresencaAgricultores")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteAgricultor ? 1 : 0));
            ((Label)lst.FindControl("lblTotalFamiliaAgricultores")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAgricultores)); 
            ((Label)lst.FindControl("lblPresencaAcampamentos")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteAcampamento ? 1 : 0));
            ((Label)lst.FindControl("lblTotalFamiliaAcampamentos")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAcampamentos));
            ((Label)lst.FindControl("lblPresencaInstalacaoPrisional")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteInstalacaoPrisional ? 1 : 0));
            ((Label)lst.FindControl("lblTotalInstalacaoPrisional")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroInstalacoesPrisionais));
            ((Label)lst.FindControl("lblPresencaTrabalhoSazonal")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteTrabalhoSazonal ? 1 : 0));
            ((Label)lst.FindControl("lblTotalTrabalhoSazonal")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroTrabalhoSazonais));
            ((Label)lst.FindControl("lblPresencaAglomeradosSubnormais")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteAglomeradoSubnormal ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAglomeradosSubnormais")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAglomeradoSubnormais));
            ((Label)lst.FindControl("lblPresencaAssentamentosPrecarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.ExisteAssentamentoPrecario ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAssentamentosPrecarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAssentamentosPrecarios));
        }

        private void carregarPagina()
        {
            Master.Titulo = "Relat&#243;rio descritivo 10 - Presen&#231;a de povos tradicionais e&#47;ou grupos espec&#237;ficos nos munic&#237;pios";
            Master.MainWidthRelatorio =  Master.WidthRelatorio = "2000px";
            Master.GerarExcel.Click += new EventHandler(GerarExcel_Click);
            Master.GeraXLSX.Visible = false;
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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo10.xls");
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