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
    public partial class RServicosIntermunicipais : System.Web.UI.Page
    {
        Int32 _sequencia = 1;

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
            Master.Titulo = "Relat&#243;rio descritivo 25 - Servi&#231;os Intermunicipais";
            Master.WidthRelatorio = "3550";
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

            //filtro.SituacaoVulnerabilidade = Session["SITUACAO_VULNERABILIDADE"] as int?;
            //filtro.SituacaoEspecifica = Session["SITUACAO_ESPECIFICA"] as int?;

            filtro.Sexo = Session["SEXO"] as int?;
            filtro.RegiaoMoradia = Session["REGIAOMORADIA"] as int?;
            filtro.CaracteristicasTerritorio = Session["CARACTERISTICASTERRITORIO"] as int?;

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<RedeServicoSocioassistencialInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetServicosIntermunicipais(filtro).ToList();
            }
            lst.DataSource = items.GroupBy(t => t.CNPJ).Select(t =>
                new
                {
                    Key = t.Key,
                    Items = t,
                    TotalTrabalhadores = t.Sum(s => s.TotalTrabalhadores),
                    TotalTrabalhadoresServico = t.Sum(s => s.TotalTrabalhadoresServico),
                    NumeroAtendidosMensal = t.Sum(s => s.NumeroAtendidosMensal),
                    NumeroAtendidosAnual = t.Sum(s => s.NumeroAtendidosAnual),
                    ValorFMAS = t.Sum(s => s.ValorFMAS),
                    ValorFMDCA = t.Sum(s => s.ValorFMDCA),
                    ValorFMI = t.Sum(s => s.ValorFMI),
                    ValorFEAS = t.Sum(s => s.ValorFEAS),
                    ValorFEASAnoAnterior = t.Sum(s => s.ValorFEASAnoAnterior),
                    ValorFEDCA = t.Sum(s => s.ValorFEDCA),
                    ValorFEI = t.Sum(s => s.ValorFEI),
                    ValorFNAS = t.Sum(s => s.ValorFNAS),
                    ValorFNDCA = t.Sum(s => s.ValorFNDCA),
                    ValorFNI = t.Sum(s => s.ValorFNI),
                    ValorPrivado = t.Sum(s => s.ValorPrivado),
                    Total = t.Sum(s => s.Total)
                });
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblNumeroAtendidosMensal")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosMensal));
            ((Label)lst.FindControl("lblNumeroAtendidosAnual")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosAnual));
            //((Label)lst.FindControl("lblNumeroTrabalhadoresLocalExecucao")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalTrabalhadores));
            //((Label)lst.FindControl("lblNumeroTrabalhadoresServico")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalTrabalhadoresServico));
            ((Label)lst.FindControl("lblTotal")).Text = String.Format("{0:N2}", items.Sum(i => i.Total));
            ((Label)lst.FindControl("lblTotalFMAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMAS));
            ((Label)lst.FindControl("lblTotalFEAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEAS));
            ((Label)lst.FindControl("lblTotalFEASAnoAnterior")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEASAnoAnterior));
            ((Label)lst.FindControl("lblTotalFNAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNAS));
            ((Label)lst.FindControl("lblTotalFMDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMDCA));
            ((Label)lst.FindControl("lblTotalFEDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEDCA));
            ((Label)lst.FindControl("lblTotalFNDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNDCA));
            ((Label)lst.FindControl("lblTotalFMI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMI));
            ((Label)lst.FindControl("lblTotalFEI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEI));
            ((Label)lst.FindControl("lblTotalFNI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNI));
            ((Label)lst.FindControl("lblTotalPrivado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorPrivado));
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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo25.xls");
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
                ((Label)e.Item.FindControl("lblSequencia")).Text = _sequencia.ToString() + "/" + (e.Item.DataItemIndex + 1).ToString();
            }
        }

        protected void lst_ItemDataBoundGrupo(object sender, ListViewItemEventArgs e)
        {
            _sequencia++;
        }
    }
}