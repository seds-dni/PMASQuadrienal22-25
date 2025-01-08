using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Web.UI.HtmlControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Relatorios
{
    public partial class RCronogramaDesembolso : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 29 - Cronogramas de desembolso (INCLUINDO VALORES REPROGRAMADOS DO EXERCÍCIO ANTERIOR)";
            Master.WidthRelatorio = "1500";
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
            filtro.TipoProtecaoSocial = Session["RELATORIO_TIPO_PROTECAO_ID"] as int?;
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;
            filtro.Portes = Session["RELATORIO_PORTE_ID"] as List<int>;
            filtro.NiveisGestao = Session["RELATORIO_NIVEL_GESTAO_ID"] as List<int>;
            filtro.Estado = Session["RELATORIO_ESTADO"] as Boolean?;
            filtro.TipoExecutora = Session["RELATORIO_TIPO_EXECUTORA"] as List<ETipoUnidade>;
            filtro.CronogramasEscolhidos = Session["RELATORIO_TIPO_CRONOGRAMA"] as List<int>;
            filtro.TotalCronogramas = Session["RELATORIO_TOTAL_CRONOGRAMAS"] as int?;
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;
            filtro.IdPrefeitura = 7936;

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<CronogramaDesembolsoRelatorio22Info>();
            using (var proxy = new ProxyRelatorios())
            {
                if (filtro.TotalCronogramas == 1)
                {
                    items = proxy.Service.GetTotalCronogramaDesembolso(filtro).Where(p => p.Total > 0).ToList();
                    lst.Visible = true;
                    lst.DataSource = items;
                    lst.DataBind();
                    if (items.Count == 0)
                        return;
                    ((Label)lst.FindControl("lblTotalRecursosHumanosPublica")).Text = String.Format("{0:N2}", items.Sum(i => i.RecursosHumanosPublica));
                    ((Label)lst.FindControl("lblTotalCusteioOutrasDespesasPublica")).Text = String.Format("{0:N2}", items.Sum(i => i.CusteioPublica));
                    ((Label)lst.FindControl("lblTotalInvestimentoAquisicaoPublica")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoAquisicaoDeEquipamentosPublico));
                    ((Label)lst.FindControl("lblTotalRecursosHumanos")).Text = String.Format("{0:N2}", items.Sum(i => i.RecursosHumanos));
                    ((Label)lst.FindControl("lblTotalCusteioOutrasDespesasPrivada")).Text = String.Format("{0:0,0}", items.Sum(i => i.CusteioPrivada));
                    ((Label)lst.FindControl("lblTotalInvestimentoAquisicaoPrivada")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoAquisicaoDeEquipamentosPrivado));
                    ((Label)lst.FindControl("lblTotalRecursosHumanosPublicaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.RecursosHumanosPublicaReprogramado));
                    ((Label)lst.FindControl("lblTotalCusteioOutrasDespesasPublicaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.CusteioPublicaReprogramado));
                    ((Label)lst.FindControl("lblTotalInvestimentoAquisicaoPublicaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoEquipamentosPublicoReprogramado));
                    ((Label)lst.FindControl("lblTotalRecursosHumanosReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.RecursosHumanosReprogramado));
                    ((Label)lst.FindControl("lblTotalCusteioOutrasDespesasPrivadaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.CusteioPrivadaReprogramado));
                    ((Label)lst.FindControl("lblTotalInvestimentoAquisicaoPrivadaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoEquipamentosPrivadoReprogramado));

                    ((Label)lst.FindControl("lblTotalInvestimentosObrasPublica")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoObrasPublico));
                    ((Label)lst.FindControl("lblTotalInvestimentosObrasPublicaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentosObrasReprogramadoPublico));

                    ((Label)lst.FindControl("lblTotalInvestimentosObrasPrivada")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoObrasPrivado));
                    ((Label)lst.FindControl("lblTotalInvestimentosObrasPrivadaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoEquipamentosPrivadoReprogramado));

                    ((Label)lst.FindControl("lblTotalGeral")).Text = String.Format("{0:N2}", items.Sum(i => i.Total));
                }
                else
                {
                    items = proxy.Service.GetCronogramaDesembolso(filtro).Where(p => p.Total > 0).ToList();
                    // tbReport.Visible = false;
                    lst.Visible = true;
                    lst.DataSource = items;
                    lst.DataBind();
                    if (items.Count == 0)
                        return;
                    ((Label)lst.FindControl("lblTotalRecursosHumanosPublica")).Text = String.Format("{0:N2}", items.Sum(i => i.RecursosHumanosPublica));
                    ((Label)lst.FindControl("lblTotalCusteioOutrasDespesasPublica")).Text = String.Format("{0:N2}", items.Sum(i => i.CusteioPublica));
                    ((Label)lst.FindControl("lblTotalInvestimentoAquisicaoPublica")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoAquisicaoDeEquipamentosPublico));
                    ((Label)lst.FindControl("lblTotalRecursosHumanos")).Text = String.Format("{0:N2}", items.Sum(i => i.RecursosHumanos));
                    ((Label)lst.FindControl("lblTotalCusteioOutrasDespesasPrivada")).Text = String.Format("{0:0,0}", items.Sum(i => i.CusteioPrivada));
                    ((Label)lst.FindControl("lblTotalInvestimentoAquisicaoPrivada")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoAquisicaoDeEquipamentosPrivado));
                    ((Label)lst.FindControl("lblTotalRecursosHumanosPublicaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.RecursosHumanosPublicaReprogramado));
                    ((Label)lst.FindControl("lblTotalCusteioOutrasDespesasPublicaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.CusteioPublicaReprogramado));
                    ((Label)lst.FindControl("lblTotalInvestimentoAquisicaoPublicaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoEquipamentosPublicoReprogramado));
                    ((Label)lst.FindControl("lblTotalRecursosHumanosReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.RecursosHumanosReprogramado));
                    ((Label)lst.FindControl("lblTotalCusteioOutrasDespesasPrivadaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.CusteioPrivadaReprogramado));
                    ((Label)lst.FindControl("lblTotalInvestimentoAquisicaoPrivadaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoEquipamentosPrivadoReprogramado));

                    ((Label)lst.FindControl("lblTotalInvestimentosObrasPublica")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoObrasPublico));
                    ((Label)lst.FindControl("lblTotalInvestimentosObrasPublicaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentosObrasReprogramadoPublico));

                    ((Label)lst.FindControl("lblTotalInvestimentosObrasPrivada")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentoObrasPrivado));
                    ((Label)lst.FindControl("lblTotalInvestimentosObrasPrivadaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.InvestimentosObrasReprogramadoPrivado));


                    ((Label)lst.FindControl("lblTotalGeral")).Text = String.Format("{0:N2}", items.Sum(i => i.Total));
                }
            }


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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo28.xls");
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