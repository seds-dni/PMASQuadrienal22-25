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
    public partial class ROrganizacaoOrgaoGestor : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 3 - Estrutura&#231;&#227;o do &#243;rg&#227;o gestor da Assist&#234;ncia Social";
            Master.MainWidthRelatorio = Master.WidthRelatorio = "1300px";
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
            filtro.Estado = Session["RELATORIO_ESTADO"] as Boolean?;
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;

            //int ProtecaoBasica = 0;
            //int Protecaoespecial = 0;
            //int VigilanciaSocioassistencial = 0;
            //int TransferenciaRenda = 0;
            //int CadUnico = 0;
            //int GestaoFinanceira = 0;
            //int GestaoSuas = 0;
            //int RegulacaoSuas = 0;
            //int RedeDireta = 0;
            //int OutrasEquipes = 0;



            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<OrganizacaoOrgaoGestorInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetOrganizacaoOrgaoGestor(filtro).ToList();                
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            //foreach (var i in items)
            //{
            //   
            //    if (i.PossuiEquipeProtecaoBasica != null)
            //    {
            //        ProtecaoBasica = ProtecaoBasica + 1;
            //    }
            //
            //    if (i.PossuiEquipeProtecaoEspecial != null)
            //    {
            //        Protecaoespecial = Protecaoespecial + 1;
            //    }
            //
            //    if (i.PossuiEquipeVigilanciaSocioassistencial != null)
            //    {
            //        VigilanciaSocioassistencial = VigilanciaSocioassistencial + 1;
            //    }
            //
            //    if (i.PossuiEquipeTransferenciaRenda != null)
            //    {
            //        TransferenciaRenda = TransferenciaRenda + 1;
            //    }
            //
            //    if (i.PossuiEquipeCadUnico != null)
            //    {
            //        CadUnico = CadUnico + 1;
            //    }
            //
            //    if (i.PossuiEquipeGestaoFinanceira != null)
            //    {
            //        GestaoFinanceira = GestaoFinanceira + 1;
            //    }
            //
            //    if (i.PossuiEquipeGestaoSUAS != null)
            //    {
            //        GestaoSuas = GestaoSuas + 1;
            //    }
            //
            //    if (i.PossuiEquipeRegulacaoSUAS != null)
            //    {
            //        RegulacaoSuas = RegulacaoSuas + 1;
            //    }
            //
            //    if (i.PossuiEquipeRedeDireta != null)
            //    {
            //        RedeDireta = RedeDireta + 1;
            //    }
            //
            //    if (i.PossuiOutrasEquipes != null)
            //    {
            //        OutrasEquipes = OutrasEquipes + 1;
            //    }
            //}

            ((Label)lst.FindControl("lblTotalFuncionarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalFuncionarios - i.PossuiEquipeRedeDireta));
            ((Label)lst.FindControl("lblTotalEquipeProtecaoBasica")).Text = String.Format("{0:0,0}",items.Sum(i => i.PossuiEquipeProtecaoBasica));
            ((Label)lst.FindControl("lblTotalEquipeProtecaoEspecial")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiEquipeProtecaoEspecial));
            ((Label)lst.FindControl("lblTotalEquipeVigilancia")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiEquipeVigilanciaSocioassistencial));
            ((Label)lst.FindControl("lblTotalEquipeTransferenciaRenda")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiEquipeTransferenciaRenda));

            ((Label)lst.FindControl("lblTotalEquipeCadUnico")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiEquipeCadUnico));
            ((Label)lst.FindControl("lblTotalEquipeGestaoFinanceira")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiEquipeGestaoFinanceira));
            ((Label)lst.FindControl("lblTotalEquipeGSUAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiEquipeGestaoSUAS));
            ((Label)lst.FindControl("lblTotalRegulacaoSUAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiEquipeRegulacaoSUAS));
            //((Label)lst.FindControl("lblTotalRedeDireta")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiEquipeRedeDireta));
            ((Label)lst.FindControl("lblTotalOutrasEquipes")).Text = String.Format("{0:0,0}", items.Sum(i => i.PossuiOutrasEquipes));
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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo3.xls");
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