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
    public partial class RProgramasTransferenciaRenda : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 17 - N&#186; de benefici&#225;rios e recursos financeiros dos programas de transfer&#234;ncia de renda";
            Master.WidthRelatorio = "2960px";
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
            var items = new List<ResumoTransferenciaRendaInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetResumoTransferenciaRenda(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalAcaoJovemMeta")).Text = String.Format("{0:0,0}", items.Sum(i => i.AcaoJovemMeta));
            ((Label)lst.FindControl("lblTotalAcaoJovemRepasse")).Text = String.Format("{0:C2}", items.Sum(i => i.AcaoJovemRepasse));
            ((Label)lst.FindControl("lblTotalAcaoJovemParceria")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiParceriaAcaoJovem == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAcaoJovemParcerias")).Text = String.Format("{0:0,0}", items.Sum(i => i.AcaoJovemTotalParcerias));
            ((Label)lst.FindControl("lblTotalAcaoJovemIntegracao")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiIntegracaoAcaoJovem == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAcaoJovemServicosAssociados")).Text = String.Format("{0:0,0}", items.Sum(i => i.AcaoJovemTotalServicosAssociados));
            ((Label)lst.FindControl("lblTotalRendaCidadaMeta")).Text = String.Format("{0:0,0}", items.Sum(i => i.RendaCidadaMeta));            
            ((Label)lst.FindControl("lblTotalRendaCidadaRepasse")).Text = String.Format("{0:C2}", items.Sum(i => i.RendaCidadaRepasse));
            ((Label)lst.FindControl("lblTotalRendaCidadaParceria")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiParceriaRendaCidada == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalRendaCidadaParcerias")).Text = String.Format("{0:0,0}", items.Sum(i => i.RendaCidadaTotalParcerias));
            ((Label)lst.FindControl("lblTotalRendaCidadaIntegracao")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiIntegracaoRendaCidada == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalRendaCidadaServicosAssociados")).Text = String.Format("{0:0,0}", items.Sum(i => i.RendaCidadaTotalServicosAssociados));
            ((Label)lst.FindControl("lblTotalRendaCidadaIdosoMeta")).Text = String.Format("{0:0,0}", items.Sum(i => i.RendaCidadaIdosoMeta));
            ((Label)lst.FindControl("lblTotalRendaCidadaIdosoRepasse")).Text = String.Format("{0:C2}", items.Sum(i => i.RendaCidadaIdosoRepasse));
            ((Label)lst.FindControl("lblTotalRendaCidadaIdosoParceria")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiParceriaRendaCidadaIdoso == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalRendaCidadaIdosoParcerias")).Text = String.Format("{0:0,0}", items.Sum(i => i.RendaCidadaIdosoTotalParcerias));
            ((Label)lst.FindControl("lblTotalRendaCidadaIdosoIntegracao")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiIntegracaoRendaCidadaIdoso == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalRendaCidadaIdosoServicosAssociados")).Text = String.Format("{0:0,0}", items.Sum(i => i.RendaCidadaIdosoTotalServicosAssociados));
            ((Label)lst.FindControl("lblTotalBolsaFamiliaBeneficiarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.BolsaFamiliaBeneficiarios));
            ((Label)lst.FindControl("lblTotalBolsaFamiliaRepasse")).Text = String.Format("{0:C2}", items.Sum(i => i.BolsaFamiliaRepasse));
            ((Label)lst.FindControl("lblTotalBolsaFamiliaParceria")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiParceriaBolsaFamilia == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalBolsaFamiliaParcerias")).Text = String.Format("{0:0,0}", items.Sum(i => i.BolsaFamiliaTotalParcerias));
            ((Label)lst.FindControl("lblTotalBolsaFamiliaIntegracao")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiIntegracaoBolsaFamilia == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalBolsaFamiliaServicosAssociados")).Text = String.Format("{0:0,0}", items.Sum(i => i.BolsaFamiliaTotalServicosAssociados));
            ((Label)lst.FindControl("lblTotalPETIBeneficiarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.PETIBeneficiarios));
            ((Label)lst.FindControl("lblTotalPETIRepasse")).Text = String.Format("{0:C2}", items.Sum(i => i.PETIRepasse));
            ((Label)lst.FindControl("lblTotalPETIParceria")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiParceriaPETI == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalPETIParcerias")).Text = String.Format("{0:0,0}", items.Sum(i => i.PETITotalParcerias));
            ((Label)lst.FindControl("lblTotalPETIIntegracao")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiIntegracaoPETI == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalPETIServicosAssociados")).Text = String.Format("{0:0,0}", items.Sum(i => i.PETITotalServicosAssociados));
            ((Label)lst.FindControl("lblTotalMunicipaisBeneficiarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.MunicipaisBeneficiarios));
            ((Label)lst.FindControl("lblTotalMunicipaisRepasse")).Text = String.Format("{0:C2}", items.Sum(i => i.MunicipaisRepasse));
            ((Label)lst.FindControl("lblTotal")).Text = String.Format("{0:C2}", items.Sum(i => i.TotalRepasse));

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo17.xls");
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