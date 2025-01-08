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
    public partial class RSaoPauloSolidario : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 15 - Programa São Paulo Solid&#225;rio";
            Master.WidthRelatorio = "1720";
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
            var items = new List<SaoPauloSolidarioInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetProgramaSaoPauloSolidario(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalPossuiParceria")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiParceriaFormal == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalParceria")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalParcerias));
            ((Label)lst.FindControl("lblTotalBuscaAtivaOrgaoGestorExecuta")).Text = String.Format("{0:0,0}", items.Sum(i => i.BuscaAtivaOrgaoGestorExecuta == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalBuscaAtivaCRASExecuta")).Text = String.Format("{0:0,0}", items.Sum(i => i.BuscaAtivaCRASExecuta == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalBuscaAtivaUnidadePrivadaExecuta")).Text = String.Format("{0:0,0}", items.Sum(i => i.BuscaAtivaUnidadePrivadaExecuta == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalValorFMAS")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorFMAS));
            ((Label)lst.FindControl("lblTotalValorOrcamentoMunicipal")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorOrcamentoMunicipal));
            ((Label)lst.FindControl("lblTotalValorFundosMunicipais")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorFundoMunicipal));
            ((Label)lst.FindControl("lblTotalValorFEAS")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorFEAS));
            ((Label)lst.FindControl("lblTotalValorOrcamentoEstadual")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorOrcamentoEstadual));
            ((Label)lst.FindControl("lblTotalValorFundosEstaduais")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorFundoEstadual));
            ((Label)lst.FindControl("lblTotalValorFNAS")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorFNAS));
            ((Label)lst.FindControl("lblTotalValorOrcamentoFederal")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorOrcamentoFederal));
            ((Label)lst.FindControl("lblTotalValorFundosFederais")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorFundoFederal));
            ((Label)lst.FindControl("lblTotalValorIGDPBF")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorIGDPBF));
            ((Label)lst.FindControl("lblTotalValorIGDSUAS")).Text = String.Format("{0:N2}", items.Sum(i => i.BuscaAtivaValorIGDSUAS));

            ((Label)lst.FindControl("lblTotalAgendaFamiliaNumeroFamilias2012")).Text = String.Format("{0:0,0}", items.Sum(i => i.AgendaFamiliaNumeroFamilias2012));
            ((Label)lst.FindControl("lblTotalAgendaFamiliaNumeroFamilias2013")).Text = String.Format("{0:0,0}", items.Sum(i => i.AgendaFamiliaNumeroFamilias2013));
            ((Label)lst.FindControl("lblTotalAgendaFamiliaNumeroFamilias2014")).Text = String.Format("{0:0,0}", items.Sum(i => i.AgendaFamiliaNumeroFamilias2014));
            ((Label)lst.FindControl("lblTotalAgendaFamiliaNumeroFamilias")).Text = String.Format("{0:0,0}", items.Sum(i => i.AgendaFamiliaNumeroFamiliasTotal));
            ((Label)lst.FindControl("lblTotalAgendaFamiliaOrgaoGestorExecuta")).Text = String.Format("{0:0,0}", items.Sum(i => i.AgendaFamiliaOrgaoGestorExecuta == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAgendaFamiliaCRASExecuta")).Text = String.Format("{0:0,0}", items.Sum(i => i.AgendaFamiliaCRASExecuta == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalPossuiParceriaAgendaFamilia")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiParceriaFormalAgendaFamilia == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalParceriaAgendaFamilia")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalParceriasAgendaFamilia));
            ((Label)lst.FindControl("lblTotalValorFMASAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorFMAS));
            ((Label)lst.FindControl("lblTotalValorOrcamentoMunicipalAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorOrcamentoMunicipal));
            ((Label)lst.FindControl("lblTotalValorFundosMunicipaisAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorFundoMunicipal));
            ((Label)lst.FindControl("lblTotalValorFEASAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorFEAS));
            ((Label)lst.FindControl("lblTotalValorOrcamentoEstadualAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorOrcamentoEstadual));
            ((Label)lst.FindControl("lblTotalValorFundosEstaduaisAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorFundoEstadual));
            ((Label)lst.FindControl("lblTotalValorFNASAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorFNAS));
            ((Label)lst.FindControl("lblTotalValorOrcamentoFederalAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorOrcamentoFederal));
            ((Label)lst.FindControl("lblTotalValorFundosFederaisAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorFundoFederal));
            ((Label)lst.FindControl("lblTotalValorIGDPBFAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorIGDPBF));
            ((Label)lst.FindControl("lblTotalValorIGDSUASAgendaFamilia")).Text = String.Format("{0:N2}", items.Sum(i => i.AgendaFamiliaValorIGDSUAS));
            ((Label)lst.FindControl("lblTotalPossuiServicosAssociados")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalServicosAssociados > 0 ? 1 : 0));
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