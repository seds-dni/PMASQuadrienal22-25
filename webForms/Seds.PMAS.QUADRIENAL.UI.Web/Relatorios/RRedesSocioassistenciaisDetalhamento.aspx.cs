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
    public partial class RRedesSocioassistenciaisDetalhamento : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 13 - Atendimentos espec&#237;ficos realizados pelos servi&#231;os socioassistenciais";
            Master.WidthRelatorio = Master.MainWidthRelatorio = "3050px";
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
            filtro.DataImplantacao = Session["RELATORIO_DATA_IMPLEMENTACAO"].ToString();

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            Master.DataDeReferencia(filtro.DataImplantacao);

            var items = new List<RedeServicoSocioassistencialDetalhamentoInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetRedeServicoSocioassistencialDetalhamento(filtro).ToList();
            }

            if (filtro.TipoServico.HasValue && filtro.TipoServico.Value == 141)
            {
                lstServicoPSC.Visible = true;
                lstServicoPSC.DataSource = items;
                lstServicoPSC.DataBind();
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensal2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosMensal));
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensalPSC2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosServicoMensal));
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensal2018")).Text = String.Format("{0:0,0}", 0);
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensalPSC2018")).Text = String.Format("{0:0,0}", 0);
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensal2019")).Text = String.Format("{0:0,0}", 0);
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensalPSC2019")).Text = String.Format("{0:0,0}", 0);
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensal2020")).Text = String.Format("{0:0,0}", 0);
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensalPSC2020")).Text = String.Format("{0:0,0}", 0);
                ((Label)lstServicoPSC.FindControl("lblMediaMensal2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensal2017));
                ((Label)lstServicoPSC.FindControl("lblMediaMensalPSC2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensalPSC2017));
                ((Label)lstServicoPSC.FindControl("lblMediaMensal2018")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensal2018));
                ((Label)lstServicoPSC.FindControl("lblMediaMensalPSC2018")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensal2018));
                ((Label)lstServicoPSC.FindControl("lblMediaMensal2019")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensal2019));
                ((Label)lstServicoPSC.FindControl("lblMediaMensalPSC2019")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensal2019));
                ((Label)lstServicoPSC.FindControl("lblMediaMensal2020")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensal2020));
                ((Label)lstServicoPSC.FindControl("lblMediaMensalPSC2020")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensalPSC2020));


                //((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensal")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosMensal));
                //((Label)lstServicoPSC.FindControl("lblNumeroAtendidosServicoMensal")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosServicoMensal));
                //((Label)lstServicoPSC.FindControl("lblNumeroAtendidosAnual")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosAnual));
                //((Label)lstServicoPSC.FindControl("lblNumeroAtendidosServicoAnual")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumeroAtendidosServicoAnual));
                //((Label)lstServicoPSC.FindControl("lblNumeroTrabalhadoresServico")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalTrabalhadoresServico));
                //((Label)lstServicoPSC.FindControl("lblTotal")).Text = String.Format("{0:N2}", items.Sum(i => i.Total));
                //((Label)lstServicoPSC.FindControl("lblTotalFMAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMAS));
                //((Label)lstServicoPSC.FindControl("lblTotalFEAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEAS));
                //((Label)lstServicoPSC.FindControl("lblTotalFEASAnoAnterior")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEASAnoAnterior));
                //((Label)lstServicoPSC.FindControl("lblTotalFNAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNAS));
                //((Label)lstServicoPSC.FindControl("lblTotalFMDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMDCA));
                //((Label)lstServicoPSC.FindControl("lblTotalFEDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEDCA));
                //((Label)lstServicoPSC.FindControl("lblTotalFNDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNDCA));
                //((Label)lstServicoPSC.FindControl("lblTotalFMI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMI));
                //((Label)lstServicoPSC.FindControl("lblTotalFEI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEI));
                //((Label)lstServicoPSC.FindControl("lblTotalFNI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNI));
                //((Label)lstServicoPSC.FindControl("lblTotalPrivado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorPrivado));
                //((Label)lstServicoPSC.FindControl("lblTotalEstadualizado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorEstadualizado));
            }
            else
            {
                lst.DataSource = items;
                lst.DataBind();

                if (items.Count == 0)
                    return;

                ((Label)lst.FindControl("lblNumeroAtendidosMensal2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumertoTotalAtendidosMensal));
                ((Label)lst.FindControl("lblNumeroAtendidosMensal2018")).Text = String.Format("{0:0,0}", 0);
                ((Label)lst.FindControl("lblNumeroAtendidosMensal2019")).Text = String.Format("{0:0,0}", 0);
                ((Label)lst.FindControl("lblNumeroAtendidosMensal2020")).Text = String.Format("{0:0,0}", 0);
                ((Label)lst.FindControl("lblMediaMensal2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaTotal2017));
                ((Label)lst.FindControl("lblMediaMensal2018")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaTotal2018));
                ((Label)lst.FindControl("lblMediaMensal2019")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaTotal2019));
                ((Label)lst.FindControl("lblMediaMensal2020")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaTotal2020));

                //((Label)lst.FindControl("lblNumeroAtendidosMensal")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumertoTotalAtendidosMensal));
                //((Label)lst.FindControl("lblNumeroAtendidosAnual")).Text = String.Format("{0:0,0}", items.Sum(i => i.NumertoTotalAtendidosAnual));
                ////((Label)lst.FindControl("lblNumeroTrabalhadoresLocalExecucao")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalTrabalhadores));
                //((Label)lst.FindControl("lblNumeroTrabalhadoresServico")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalTrabalhadoresServico));
                //((Label)lst.FindControl("lblTotal")).Text = String.Format("{0:N2}", items.Sum(i => i.Total));
                //((Label)lst.FindControl("lblTotalFMAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMAS));
                //((Label)lst.FindControl("lblTotalFEAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEAS));
                //((Label)lst.FindControl("lblTotalFEASAnoAnterior")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEASAnoAnterior));
                //((Label)lst.FindControl("lblTotalFNAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNAS));
                //((Label)lst.FindControl("lblTotalFMDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMDCA));
                //((Label)lst.FindControl("lblTotalFEDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEDCA));
                //((Label)lst.FindControl("lblTotalFNDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNDCA));
                //((Label)lst.FindControl("lblTotalFMI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMI));
                //((Label)lst.FindControl("lblTotalFEI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEI));
                //((Label)lst.FindControl("lblTotalFNI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNI));
                //((Label)lst.FindControl("lblTotalPrivado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorPrivado));
                //((Label)lst.FindControl("lblTotalEstadualizado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorEstadualizado));
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
            if(tb == null)
                tb = ((HtmlTable)lstServicoPSC.FindControl("tbReport"));

            tb.CellPadding = 1;
            tb.CellSpacing = 1;
            tb.Border = 1;
            tb.BorderColor = "black";
            Master.Report.RenderControl(htmlWrite);
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo13.xls");
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