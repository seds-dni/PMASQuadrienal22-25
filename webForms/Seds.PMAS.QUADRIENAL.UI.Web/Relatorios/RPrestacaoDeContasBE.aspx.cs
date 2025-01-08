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
    public partial class RPrestacaoDeContasBE : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 36 - Presta&#231;&#227; de Contas Proteção Social Beneficios Eventuais";
            Master.MainWidthRelatorio = Master.WidthRelatorio = "1300";
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
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<RPrestacaoDeContasBeneficiosEventuais>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetPrestacaDeContasProtecaoBeneficiosEventuais(filtro).ToList();
            }
            lstBeneficiosEventuais.DataSource = items;
            lstBeneficiosEventuais.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lstBeneficiosEventuais.FindControl("lblTotalSomaProtecaoBeneficiosEventuais")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoBeneficiosEventuais));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresExecutadosBeneficiosEventuais")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosBeneficiosEventuais));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresPassiveisReprogramacaoBeneficiosEventuais")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoBeneficiosEventuais));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresDevolvidosBeneficiosEventuais")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosBeneficiosEventuais));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalPorcentagensExecucaoBeneficiosEventuais")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoBeneficiosEventuais));

            ((Label)lstBeneficiosEventuais.FindControl("lblTotalSomaProtecaoBeneficiosEventuaisReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoBeneficiosEventuaisReprogramacao));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresExecutadosBeneficiosEventuaisReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosBeneficiosEventuaisReprogramacao));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacao));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresDevolvidosBeneficiosEventuaisReprogramacao")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosBeneficiosEventuaisReprogramacao));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalPorcentagensExecucaoBeneficiosEventuaisReprogramacao")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoBeneficiosEventuaisReprogramacao));

            ((Label)lstBeneficiosEventuais.FindControl("lblTotalSomaProtecaoBeneficiosEventuaisDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoBeneficiosEventuaisDemandas));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresExecutadosBeneficiosEventuaisDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosBeneficiosEventuaisDemandas));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresPassiveisReprogramacaoBeneficiosEventuaisDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoBeneficiosEventuaisDemandas));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresDevolvidosBeneficiosEventuaisDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosBeneficiosEventuaisDemandas));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalPorcentagensExecucaoBeneficiosEventuaisDemandas")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoBeneficiosEventuaisDemandas));

            ((Label)lstBeneficiosEventuais.FindControl("lblTotalSomaProtecaoBeneficiosEventuaisReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.SomaProtecaoBeneficiosEventuaisReprogramacaoDemandas));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresExecutadosBeneficiosEventuaisReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresExecutadosBeneficiosEventuaisReprogramacaoDemandas));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacaoDemandas));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalValoresDevolvidosBeneficiosEventuaisReprogramacaoDemandas")).Text = String.Format("{0:N2}", items.Sum(i => i.ValoresDevolvidosBeneficiosEventuaisReprogramacaoDemandas));
            ((Label)lstBeneficiosEventuais.FindControl("lblTotalPorcentagensExecucaoBeneficiosEventuaisReprogramacaoDemandas")).Text = String.Format("{0:P2}", items.Sum(i => i.PorcentagensExecucaoBeneficiosEventuaisReprogramacaoDemandas));
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

            var tb = ((HtmlTable)lstBeneficiosEventuais.FindControl("tbReport"));
            tb.CellPadding = 1;
            tb.CellSpacing = 1;
            tb.Border = 1;
            tb.BorderColor = "black";
            Master.Report.RenderControl(htmlWrite);
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo36.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write(this.Master.replaceCaracteresEspeciais(stringWrite.ToString()));
            Response.End();

            tb.CellPadding = 0;
            tb.CellSpacing = 0;
            tb.Border = 0;
            tb.BorderColor = "";
        }

        protected void lstBeneficiosEventuais_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
    }
}