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
    public partial class RDistribuicaoEstadualProgramaTrabalho : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 28 - Distribui&#231;&#227;o dos recursos do cofinanciamento estadual, segundo os programas de trabalho ";
            Master.WidthRelatorio = "2360px";
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
            var items = new List<DistribuicaoEstadualProgramaTrabalhoInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                var grupo = proxy.Service.GetDistribuicaoEstadualProgramaTrabalho(filtro).ToList().OrderByDescending(x => x.IdHistorico).GroupBy(x => x.IdPrefeitura);

                var consulta = grupo.Select(g => new DistribuicaoEstadualProgramaTrabalhoInfo()
                {
                    Exercicio = g.First().Exercicio
                    ,
                    IdDrads = g.First().IdDrads
                    ,
                    Drads = g.First().Drads
                    ,
                    IdHistorico = g.First().IdHistorico
                    ,
                    IdMacroRegiao = g.First().IdMacroRegiao
                    ,
                    IdMunicipio = g.First().IdMunicipio
                    ,
                    IdNivelGestao = g.First().IdNivelGestao
                    ,
                    IdPorte = g.First().IdPorte
                    ,
                    IdPrefeitura = g.First().IdPrefeitura
                    ,
                    IdRegiaoMetropolitana = g.First().IdRegiaoMetropolitana
                    ,
                    Municipio = g.First().Municipio
                    ,
                    NumeroAtendidosAnualAlta = g.First().NumeroAtendidosAnualAlta
                    ,
                    NumeroAtendidosAnualBasica = g.First().NumeroAtendidosAnualBasica
                    ,
                    NumeroAtendidosAnualBeneficios = g.First().NumeroAtendidosAnualBeneficios
                    ,
                    NumeroAtendidosAnualMedia = g.First().NumeroAtendidosAnualMedia
                    ,
                    ValorBeneficiosEventuais = g.First().ValorBeneficiosEventuais
                    ,
                    ValorBeneficiosEventuaisReprogramado = g.First().ValorBeneficiosEventuaisReprogramado
                    ,
                    ValorProtecaoSocialAlta = g.First().ValorProtecaoSocialAlta
                    ,
                    ValorProtecaoSocialAltaReprogramado = g.First().ValorProtecaoSocialAltaReprogramado
                    ,
                    ValorProtecaoSocialBasica = g.First().ValorProtecaoSocialBasica
                    ,
                    ValorProtecaoSocialBasicaReprogramado = g.First().ValorProtecaoSocialBasicaReprogramado
                    ,
                    ValorProtecaoSocialMedia = g.First().ValorProtecaoSocialMedia
                    ,
                    ValorProtecaoSocialMediaReprogramado = g.First().ValorProtecaoSocialMediaReprogramado
                    ,
                    ValorSPSolidario = g.First().ValorSPSolidario
                    ,
                    ValorSPSolidarioReprogramado = g.First().ValorSPSolidarioReprogramado
                }).OrderBy(x => x.Municipio).ToList();
                items = consulta;
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalProtecaoSocialBasica")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorProtecaoSocialBasica));
            ((Label)lst.FindControl("lblTotalProtecaoSocialBasicaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorProtecaoSocialBasicaReprogramado));
            ((Label)lst.FindControl("lblValorTotalProtecaoSocialBasica")).Text = String.Format("{0:N2}", items.Sum(i => i.SubTotalValorProtecaoSocialBasica));
            //((Label)lst.FindControl("lblTotalNumeroAtendidosAnualBasica")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosAnualBasica));
            ((Label)lst.FindControl("lblTotalProtecaoSocialMedia")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorProtecaoSocialMedia));
            ((Label)lst.FindControl("lblTotalProtecaoSocialMediaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorProtecaoSocialMediaReprogramado));
            ((Label)lst.FindControl("lblValorTotalProtecaoSocialMedia")).Text = String.Format("{0:N2}", items.Sum(i => i.SubTotalValorProtecaoSocialMedia));
            //((Label)lst.FindControl("lblTotalNumeroAtendidosAnualMedia")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosAnualMedia));
            ((Label)lst.FindControl("lblTotalProtecaoSocialAlta")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorProtecaoSocialAlta));
            ((Label)lst.FindControl("lblTotalProtecaoSocialAltaReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorProtecaoSocialAltaReprogramado));
            ((Label)lst.FindControl("lblValorTotalProtecaoSocialAlta")).Text = String.Format("{0:N2}", items.Sum(i => i.SubTotalValorProtecaoSocialAlta));
            //((Label)lst.FindControl("lblTotalNumeroAtendidosAnualAlta")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosAnualAlta));
            ((Label)lst.FindControl("lblTotalBeneficios")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorBeneficiosEventuais));
            ((Label)lst.FindControl("lblTotalBeneficiosReprogramado")).Text = String.Format("{0:0,0}", items.Sum(i => i.ValorBeneficiosEventuaisReprogramado));
            ((Label)lst.FindControl("lblTotalBeneficiosReprogramado")).Text = String.Format("{0:0,0}", items.Sum(i => i.ValorBeneficiosEventuaisReprogramado));
            ((Label)lst.FindControl("lblValorTotalBeneficios")).Text = String.Format("{0:0,0}", items.Sum(i => i.SubTotalValorBeneficiosEventuais));
            ((Label)lst.FindControl("lblTotalSPSolidario")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorSPSolidario));
            ((Label)lst.FindControl("lblTotalSPSolidarioReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorSPSolidarioReprogramado));
            ((Label)lst.FindControl("lblValorTotalSPSolidario")).Text = String.Format("{0:N2}", items.Sum(i => i.SubTotalValorSPSolidario));
            // ((Label)lst.FindControl("lblTotalNumeroAtendidosAnualAlta")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosAnualAlta));
            ((Label)lst.FindControl("lblTotalAtual")).Text = String.Format("{0:N2}", items.Sum(i => i.TotalExercicioAtual));
            ((Label)lst.FindControl("lblTotalReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.TotalReprogramado));
            ((Label)lst.FindControl("lblTotal")).Text = String.Format("{0:N2}", items.Sum(i => i.Total));
            //((Label)lst.FindControl("lblTotalPrevisaoAnualAtendidos")).Text = String.Format("{0:N2}", items.Sum(i => i.TotalPrevisaoAnualAtendidos));

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo27.xls");
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