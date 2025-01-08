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
    public partial class RAcoesVigilancia : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 22 - A&#231;&#245;es de vigilância socioassistencial";
            Master.WidthRelatorio = "1660";
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
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;
            filtro.NiveisGestao = Session["RELATORIO_NIVEL_GESTAO_ID"] as List<int>;
            filtro.Estado = Session["RELATORIO_ESTADO"] as Boolean?;            
            
            Master.mostrarFiltros(filtro,ETipoRelatorio.Descritivo);
            var items = new List<RelAcaoVigilanciaInfo>();
            using(var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetAcoesVigilanciaSocioassistencial(filtro);
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalOfereceVigilancia")).Text = String.Format("{0:0,0}", items.Sum(i => i.OfereceVigilancia == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalPossuiEquipeVigilanciaSocioassistencial")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiEquipeVigilanciaSocioassistencial == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalEquipeVigilanciaSocioassistencial")).Text = String.Format("{0:0,0}", items.Sum(i => i.EquipeVigilanciaSocioassistencial));
            ((Label)lst.FindControl("lblTotalVigilanciaRiscos")).Text = String.Format("{0:0,0}", items.Sum(i => i.VigilanciaRiscos == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalVigilanciaPadroesServicos")).Text = String.Format("{0:0,0}", items.Sum(i => i.VigilanciaPadroesServicos == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalPossuiSistemaInformaizadoProprio")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiSistemaInformaizadoProprio == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCadUnico")).Text = String.Format("{0:0,0}", items.Sum(i => i.CadUnico == "Sim" ? 1 : 0));
            //((Label)lst.FindControl("lblTotalCensoSUAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.CensoSUAS == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalPMASWeb")).Text = String.Format("{0:0,0}", items.Sum(i => i.PMASWeb == "Sim" ? 1 : 0));
            //((Label)lst.FindControl("lblTotalSisPETI")).Text = String.Format("{0:0,0}", items.Sum(i => i.SisPETI == "Sim" ? 1 : 0));
            //((Label)lst.FindControl("lblTotalSisJovem")).Text = String.Format("{0:0,0}", items.Sum(i => i.SisJovem == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalProSocial")).Text = String.Format("{0:0,0}", items.Sum(i => i.ProSocial == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalOutrosAplicativosSUAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.OutrosAplicativosSUAS == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalInstrumentaisProprios")).Text = String.Format("{0:0,0}", items.Sum(i => i.InstrumentaisProprios == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalSistemaInformatizadoMunicipal")).Text = String.Format("{0:0,0}", items.Sum(i => i.SistemaInformatizadoMunicipal == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalOutrosOrgaosMunicipais")).Text = String.Format("{0:0,0}", items.Sum(i => i.OutrosOrgaosMunicipais == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalSEADE")).Text = String.Format("{0:0,0}", items.Sum(i => i.SEADE == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAplicativosSAGIMDS")).Text = String.Format("{0:0,0}", items.Sum(i => i.AplicativosSAGIMDS == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAplicativosBolsaFamilia")).Text = String.Format("{0:0,0}", items.Sum(i => i.AplicativosBolsaFamilia == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalIBGE")).Text = String.Format("{0:0,0}", items.Sum(i => i.IBGE == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalSisc")).Text = String.Format("{0:0,0}", items.Sum(i => i.SISC == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCensoSuas")).Text = String.Format("{0:0,0}", items.Sum(i => i.CensoSUAS == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCneas")).Text = String.Format("{0:0,0}", items.Sum(i => i.CNEAS == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblCadSuas")).Text = String.Format("{0:0,0}", items.Sum(i => i.CadSUAS == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblRMA")).Text = String.Format("{0:0,0}", items.Sum(i => i.RMAS == "Sim" ? 1 : 0));
            
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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo22.xls");
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