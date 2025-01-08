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
    public partial class RIndicadores : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 31 - Diagn&#243;tico Socioterritorial";
            Master.WidthRelatorio = "3050";
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

            filtro.Abrangencias = Session["ABRANGENCIA_SERVICO"] as List<int>;

            filtro.TipoExecutora = Session["RELATORIO_TIPO_EXECUTORA"] as List<ETipoUnidade>;
            filtro.TipoProtecaoSocial = Session["RELATORIO_TIPO_PROTECAO_ID"] as int?;
            filtro.TipoServico = Session["RELATORIO_TIPO_SERVICO_ID"] as int?;
            filtro.Usuario = Session["RELATORIO_PUBLICO_ALVO_ID"] as int?;

            filtro.SituacoesVulnerabilidade = Session["SITUACOES_VULNERABILIDADE"] as List<int>;
            filtro.SituacoesEspecificas = Session["SITUACOES_ESPECIFICAS"] as List<int>;

            filtro.Sexo = Session["SEXO"] as int?;
            filtro.RegiaoMoradia = Session["REGIAOMORADIA"] as int?;
            filtro.CaracteristicasTerritorio = Session["CARACTERISTICASTERRITORIO"] as int?;

            //Master.mostrarFiltros(filtro,ETipoRelatorio.Descritivo);
            var items = new List<DiagnosticoSocioterritorialInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetDiagnosticoSocioterritorial(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            //foreach (var c in items)
            //{
            //    ListView list = (ListView)lst.Items[i].FindControl("lstIdentificacoesTerritorios");
            //    list.DataSource = c.IdentificacoesTerritorios.Where(t => t.Id == c.IdIdentificacao);
            //    list.DataBind();
            //}

            if (items.Count == 0)
                return;

            //((Label)lst.FindControl("lblTotalMetaSEDS")).Text = String.Format("{0:0,0}", items.Sum(i => i.MetaSeds));
            //((Label)lst.FindControl("lblTotalMetaMunicipio")).Text = String.Format("{0:0,0}", items.Sum(i => i.MetaMunicipio));
            //((Label)lst.FindControl("lblTotalFamiliasBeneficiarias")).Text = String.Format("{0:0,0}", items.Sum(i => i.IdentificacoesTerritorios.Sum(c => c.NumeroBeneficiarios)));
            //((Label)lst.FindControl("lblTotalFMAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMAS));
            //((Label)lst.FindControl("lblTotalOrcamentoMunicipal")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorOrcamentoMunicipal));
            //((Label)lst.FindControl("lblTotalFundoMunicipal")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFundoMunicipal));
            //((Label)lst.FindControl("lblTotalFEAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEAS));
            //((Label)lst.FindControl("lblTotalOrcamentoEstadual")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorOrcamentoEstadual));
            //((Label)lst.FindControl("lblTotalFundoEstadual")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFundoEstadual));
            //((Label)lst.FindControl("lblTotalFNAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNAS));
            //((Label)lst.FindControl("lblTotalOrcamentoFederal")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorOrcamentoFederal));
            //((Label)lst.FindControl("lblTotalFundoFederal")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFundoFederal));
            //((Label)lst.FindControl("lblTotalIGDPBF")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorIGDPBF));
            //((Label)lst.FindControl("lblTotalIGDSUAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorIGDSUAS));
            //((Label)lst.FindControl("lblTotalRecursos")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorTotal));

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo100.xls");
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