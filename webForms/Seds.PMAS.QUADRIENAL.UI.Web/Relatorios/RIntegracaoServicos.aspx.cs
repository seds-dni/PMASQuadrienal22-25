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
    public partial class RIntegracaoServicos : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 19 - Integra&#231;&#227;o entre  servi&#231;os, programas e benefícios";
            Master.WidthRelatorio = "2940";
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
            var items = new List<IntegracaoServicoInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetIntegracaoServicos(filtro).ToList();
            }
            var group = items.GroupBy(t => new { t.IdPrefeitura, t.IdServicoRecursoFinanceiro, t.IdLocal, t.IdUsuarioTipoServico, t.IdTipoUnidade, t.CodigoUnidade })
            .Select(s => new
            {
                Servico = s.FirstOrDefault(),
                AcaoJovem = s.Where(t => t.IdTipoPrograma == 5).Sum(t => t.NumeroAtendidosIntegracao),
                RendaCidada = s.Where(t => t.IdTipoPrograma == 6).Sum(t => t.NumeroAtendidosIntegracao),
                SaoPauloSolidario = s.Where(t => t.IdTipoPrograma == 9).Sum(t => t.NumeroAtendidosIntegracao),
                BolsaFamilia = s.Where(t => t.IdTipoPrograma == 3).Sum(t => t.NumeroAtendidosIntegracao),
                PETI = s.Where(t => t.IdTipoPrograma == 4).Sum(t => t.NumeroAtendidosIntegracao),
                PTRMunicipal = s.Where(t => t.IdTipoPrograma == 8).Sum(t => t.NumeroAtendidosIntegracao),
                BPCIdosos = s.Where(t => t.IdTipoPrograma == 1).Sum(t => t.NumeroAtendidosIntegracao),
                BPCPCD = s.Where(t => t.IdTipoPrograma == 2).Sum(t => t.NumeroAtendidosIntegracao),
                AuxilioNatalidade = s.Where(t => t.IdTipoPrograma == 10).Sum(t => t.NumeroAtendidosIntegracao),
                AuxilioFuneral = s.Where(t => t.IdTipoPrograma == 11).Sum(t => t.NumeroAtendidosIntegracao),
                CalamidadeEmergencia = s.Where(t => t.IdTipoPrograma == 12).Sum(t => t.NumeroAtendidosIntegracao),
                VulnerabilidadeTemporaria = s.Where(t => t.IdTipoPrograma == 13).Sum(t => t.NumeroAtendidosIntegracao),
                RendaCidadaBeneficioIdoso = s.Where(t => t.IdTipoPrograma == 14).Sum(t => t.NumeroAtendidosIntegracao),
                Acessuas = s.Where(t => t.IdTipoPrograma == 15).Sum(t => t.NumeroAtendidosIntegracao),
                BomPrato = s.Where(t => t.IdTipoPrograma == 16).Sum(t => t.NumeroAtendidosIntegracao),
                Vivaleite = s.Where(t => t.IdTipoPrograma == 17).Sum(t => t.NumeroAtendidosIntegracao),
                ProgramaMunicipal = s.Where(t => t.IdTipoPrograma == 18).Sum(t => t.NumeroAtendidosIntegracao),
                FamiliaPaulista = s.Where(t => t.IdTipoPrograma == 19).Sum(t => t.NumeroAtendidosIntegracao)

            })
            .OrderBy(s => s.Servico.Municipio).ToList();

            lst.DataSource = group;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalNumeroAtendidosMensal")).Text = String.Format("{0:0,0}", group.Sum(i => i.Servico.NumeroAtendidosMensal));
            ((Label)lst.FindControl("lblTotalNumeroAtendidosAnual")).Text = String.Format("{0:0,0}", group.Sum(i => i.Servico.NumeroAtendidosAnual));
            ((Label)lst.FindControl("lblTotalAcaoJovem")).Text = String.Format("{0:0,0}", group.Sum(i => i.AcaoJovem));
            ((Label)lst.FindControl("lblTotalACESSUAS")).Text = String.Format("{0:0,0}", group.Sum(i => i.Acessuas));
            ((Label)lst.FindControl("lblTotalProgramaMunicipal")).Text = String.Format("{0:0,0}", group.Sum(i => i.ProgramaMunicipal));
            ((Label)lst.FindControl("lblTotalSPSolidario")).Text = String.Format("{0:0,0}", group.Sum(i => i.SaoPauloSolidario));
            ((Label)lst.FindControl("lblTotalVivaleite")).Text = String.Format("{0:0,0}", group.Sum(i => i.Vivaleite));
            ((Label)lst.FindControl("lblTotalBomprato")).Text = String.Format("{0:0,0}", group.Sum(i => i.BomPrato));
            ((Label)lst.FindControl("lblTotalRendaCidada")).Text = String.Format("{0:0,0}", group.Sum(i => i.RendaCidada));
            ((Label)lst.FindControl("lblTotalRendaCidadaBeneficioIdoso")).Text = String.Format("{0:0,0}", group.Sum(i => i.RendaCidadaBeneficioIdoso));
            ((Label)lst.FindControl("lblTotalBolsaFamilia")).Text = String.Format("{0:0,0}", group.Sum(i => i.BolsaFamilia));
            ((Label)lst.FindControl("lblTotalPETI")).Text = String.Format("{0:0,0}", group.Sum(i => i.PETI));
            ((Label)lst.FindControl("lblTotalPTRMunicipal")).Text = String.Format("{0:0,0}", group.Sum(i => i.PTRMunicipal));
            ((Label)lst.FindControl("lblTotalBPCIdosos")).Text = String.Format("{0:0,0}", group.Sum(i => i.BPCIdosos));
            ((Label)lst.FindControl("lblTotalBPCPCD")).Text = String.Format("{0:0,0}", group.Sum(i => i.BPCPCD));
            ((Label)lst.FindControl("lblTotalAuxilioNatalidade")).Text = String.Format("{0:0,0}", group.Sum(i => i.AuxilioNatalidade));
            ((Label)lst.FindControl("lblTotalAuxilioFuneral")).Text = String.Format("{0:0,0}", group.Sum(i => i.AuxilioFuneral));
            ((Label)lst.FindControl("lblTotalCalamidadeEmergencia")).Text = String.Format("{0:0,0}", group.Sum(i => i.CalamidadeEmergencia));
            ((Label)lst.FindControl("lblTotalVulnerabilidadeTemporaria")).Text = String.Format("{0:0,0}", group.Sum(i => i.VulnerabilidadeTemporaria));
            ((Label)lst.FindControl("lblTotalFamiliaPaulista")).Text = String.Format("{0:0,0}", group.Sum(i => i.FamiliaPaulista));
            

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo19.xls");
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