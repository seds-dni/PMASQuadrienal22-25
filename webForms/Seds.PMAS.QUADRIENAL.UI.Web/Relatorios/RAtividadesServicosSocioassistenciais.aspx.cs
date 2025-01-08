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
    public partial class RAtividadesServicosSocioassistenciais : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 9 - Atividades desenvolvidas pelos serviços socioassistenciais";
            Master.WidthRelatorio = "2600";   
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
            

            filtro.TipoExecutora = Session["RELATORIO_TIPO_EXECUTORA"] as List<ETipoUnidade>;
            filtro.TipoProtecaoSocial = Session["RELATORIO_TIPO_PROTECAO_ID"] as int?;
            filtro.TipoServico = Session["RELATORIO_TIPO_SERVICO_ID"] as int?;
            filtro.Usuario = Session["RELATORIO_PUBLICO_ALVO_ID"] as int?;
            
            Master.mostrarFiltros(filtro,ETipoRelatorio.Descritivo);
            var items = new List<AtividadeServicoInfo>();
            using(var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetAtividadesServicosSocioassistenciais(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;
            
            ((Label)lst.FindControl("lblTotalNumeroAtendidosMensal")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosMensal));
            ((Label)lst.FindControl("lblTotalNumeroAtendidosAnual")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosAnual));
            ((Label)lst.FindControl("lblTotalAtividade1")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade1 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade2")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade2 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade3")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade3 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade4")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade4 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade5")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade5 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade6")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade6 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade7")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade7 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade8")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade8 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade9")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade9 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade10")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade10 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade11")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade11 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade12")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade12 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade13")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade13 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade14")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade14 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade15")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade15 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade16")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade16 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade17")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade17 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade18")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade18 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade19")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade19 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade20")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade20 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade21")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade21 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade22")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade22 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade23")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade23 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade24")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade24 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade25")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade25 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade26")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade26 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade27")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade27 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade28")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade28 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade29")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade29 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade30")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade30 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade31")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade31 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade32")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade32 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade33")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade33 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade34")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade34 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade35")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade35 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade36")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade36 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade37")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade37 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade38")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade38 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade39")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade39 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade40")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade40 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade41")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade41 == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalAtividade42")).Text = String.Format("{0:0,0}", items.Sum(i => i.Atividade42 == "Sim" ? 1 : 0));


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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo9.xls");
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