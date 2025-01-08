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
    public partial class RServicosRegionalizados : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 31 - Rede de servi&#231;os regionalizados";
            Master.MainWidthRelatorio = Master.WidthRelatorio = "4300px";
            Master.GerarExcel.Click += new EventHandler(GerarExcel_Click);
            Master.GeraXLSX.Visible = false;
        }


        protected void GerarExcel_Click(object sender, EventArgs e)
        {
            MetodoImportacao();
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
            filtro.TipoFinanciamento = Session["TIPO_FINANCIAMENTO"] as int?;

            filtro.Abrangencias = Session["ABRANGENCIA_SERVICO"] as List<int>;

            filtro.TipoExecutora = Session["RELATORIO_TIPO_EXECUTORA"] as List<ETipoUnidade>;
            filtro.TipoProtecaoSocial = Session["RELATORIO_TIPO_PROTECAO_ID"] as int?;
            filtro.TipoServico = Session["RELATORIO_TIPO_SERVICO_ID"] as int?;
            filtro.Usuario = Session["RELATORIO_PUBLICO_ALVO_ID"] as int?;
            filtro.ServicoSubtificado = Session["RELATORIO_SERVICO_SUBTIFICADO_ID"] as int?;

            filtro.Sexo = Session["SEXO"] as int?;
            filtro.RegiaoMoradia = Session["REGIAOMORADIA"] as int?;
            filtro.CaracteristicasTerritorio = Session["CARACTERISTICASTERRITORIO"] as int?;
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;
            filtro.ehAtivo = Convert.ToBoolean(Session["ATIVO"]);
            filtro.ehDesativo = Convert.ToBoolean(Session["DESATIVO"]);

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<RedeServicoSocioassistencialRegionalizadosRelatorio>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetRedeServicoSocioassistencialRegionalizados(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            if (filtro.TipoServico.HasValue && filtro.TipoServico.Value == 141)
            {

                if (items.Count() >= 2000)
                {
                    (lst.FindControl("dpLista") as DataPager).Visible = true;
                }
                else
                {
                    (lst.FindControl("dpLista") as DataPager).Visible = false;
                }
            }
        }


        private void MetodoImportacao()
        {
            var filtro = new RelatorioFiltroInfo();
            filtro.MunIDs = Session["RELATORIO_MUN_ID"] as List<int>;
            filtro.DrdIDs = Session["RELATORIO_DRD_ID"] as List<int>;
            filtro.RegIDs = Session["RELATORIO_REG_ID"] as List<int>;
            filtro.MacroRegiaoIDs = Session["RELATORIO_MACRO_REGIAO_ID"] as List<int>;
            filtro.Portes = Session["RELATORIO_PORTE_ID"] as List<int>;
            filtro.NiveisGestao = Session["RELATORIO_NIVEL_GESTAO_ID"] as List<int>;
            filtro.Estado = Session["RELATORIO_ESTADO"] as Boolean?;
            filtro.TipoFinanciamento = Session["TIPO_FINANCIAMENTO"] as int?;

            filtro.Abrangencias = Session["ABRANGENCIA_SERVICO"] as List<int>;

            filtro.TipoExecutora = Session["RELATORIO_TIPO_EXECUTORA"] as List<ETipoUnidade>;
            filtro.TipoProtecaoSocial = Session["RELATORIO_TIPO_PROTECAO_ID"] as int?;
            filtro.TipoServico = Session["RELATORIO_TIPO_SERVICO_ID"] as int?;
            filtro.Usuario = Session["RELATORIO_PUBLICO_ALVO_ID"] as int?;
            filtro.ServicoSubtificado = Session["RELATORIO_SERVICO_SUBTIFICADO_ID"] as int?;

            filtro.Sexo = Session["SEXO"] as int?;
            filtro.RegiaoMoradia = Session["REGIAOMORADIA"] as int?;
            filtro.CaracteristicasTerritorio = Session["CARACTERISTICASTERRITORIO"] as int?;
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;
            filtro.ehAtivo = Convert.ToBoolean(Session["ATIVO"]);
            filtro.ehDesativo = Convert.ToBoolean(Session["DESATIVO"]);

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<RedeServicoSocioassistencialRegionalizadosRelatorio>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetRedeServicoSocioassistencialRegionalizados(filtro).ToList();
            }

            if (filtro.TipoServico.HasValue && filtro.TipoServico.Value == 141)
            {
                gerarExcel();
            }
            else
            {
                if (items.Count() <= 2000)
                {
                    gerarExcel();
                }
                else
                {
                    ImportarDados(items);
                }
            }

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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioRegionalizado31.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write(this.Master.replaceCaracteresEspeciais(stringWrite.ToString()));
            Response.End();

            /*tb.CellPadding = 0;
            tb.CellSpacing = 0;
            tb.Border = 0;
            tb.BorderColor = "";*/
        }

        private void ImportarDados(List<RedeServicoSocioassistencialRegionalizadosRelatorio> lista)
        {
            var listCarregaExcel = (from x in lista
                                    select new
                                    {
                                        OrganizacaoUnidade = x.CodigoUnidade,
                                        LocaExecucaoIdSuas = x.IdLocal,
                                        Municipio = x.Municipio,
                                        Porte = x.Porte,
                                        Distrito = x.DistritoSaoPaulo,
                                        Drads = x.Drads,
                                        TipoDeRede = x.TipoUnidade,
                                        NomeDaOrganizaçãoUnidade = x.UnidadeResponsavel,
                                        LocalDeExecuçãoDosServiços = x.LocalExecucao,
                                        ProteçãoSocial = x.ProtecaoSocial,
                                        TipoServico = x.TipoServico,
                                        Usuários = x.Usuarios,
                                        InicioFuncionamentoServiço = x.DataFuncionamentoServico,
                                        EncerramentoFuncionamentoServiço = x.DataDesativacao,
                                        Abrangencia = x.Abrangencia,
                                        MunicipioESedeServico = x.MunicipioSedeServico,
                                        MunicipioSedeServico = x.SedeServico,
                                        MunicipiosQueParticipamDaOfertaServico = x.IndicaMunicipiosParticipamOfertaServico,
                                        FormaJuridicaRegulamentaOfertaServico = x.FormaJuridica,
                                        NomeConsorcio =x.NomeConsorcio,
                                        CNPJ = x.CNPJ,
                                        MunicipioSede = x.MunicipioSede,
                                        MunicipiosEnvolvidos = x.MunicipiosEnvolvidos
   
                                    });

            if (listCarregaExcel != null)
            {
                DataGrid rptExcel = new DataGrid();

                rptExcel.DataSource = listCarregaExcel;

                rptExcel.DataBind();

                Response.Clear();

                Response.Buffer = true;

                Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

                Response.AddHeader("content-disposition", "attachment;filename=RelatorioRegionalizado31.xls");

                Response.Charset = "";

                Response.ContentType = "application/vnd.xls";

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();

                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                rptExcel.RenderControl(htmlWrite);

                Response.Write(stringWrite.ToString());

                rptExcel = null;

                listCarregaExcel = null;

                Response.End();

            }
        }


        protected void lst_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

        protected void lst_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (lst.FindControl("dpLista") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.carregarDados();
        }

        protected void lstExcel_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

    }
}