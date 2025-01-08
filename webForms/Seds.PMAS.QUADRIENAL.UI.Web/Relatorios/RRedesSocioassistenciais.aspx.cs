using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using System.Web.UI.HtmlControls;
using Aspose.Cells;
using Aspose.Cells.Saving;
using DocumentFormat.OpenXml;
using SpreadsheetLight;
using System.Data;
using System.ComponentModel;


namespace Seds.PMAS.QUADRIENAL.UI.Web.Relatorios
{
    public partial class RRedesSocioassistenciais : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 12 - Rede de servi&#231;os socioassistenciais";
            Master.MainWidthRelatorio = Master.WidthRelatorio = "4300px";
            Master.GerarExcel.Click += new EventHandler(GerarExcel_Click);
            Master.GeraXLSX.Click += new EventHandler(GeraXLSX_Click);
        }

        void carregarDados()
        {
            var filtro = new RelatorioFiltroInfo();
            filtro.MunIDs             = Session["RELATORIO_MUN_ID"] as List<int>;
            filtro.DrdIDs             = Session["RELATORIO_DRD_ID"] as List<int>;
            filtro.RegIDs             = Session["RELATORIO_REG_ID"] as List<int>;
            filtro.MacroRegiaoIDs     = Session["RELATORIO_MACRO_REGIAO_ID"] as List<int>;
            filtro.Portes             = Session["RELATORIO_PORTE_ID"] as List<int>;
            filtro.NiveisGestao       = Session["RELATORIO_NIVEL_GESTAO_ID"] as List<int>;
            filtro.Estado             = Session["RELATORIO_ESTADO"] as Boolean?;
            filtro.TipoFinanciamento  = Session["TIPO_FINANCIAMENTO"] as int?;

            filtro.Abrangencias       = Session["ABRANGENCIA_SERVICO"] as List<int>;

            filtro.TipoExecutora      = Session["RELATORIO_TIPO_EXECUTORA"] as List<ETipoUnidade>;
            filtro.TipoProtecaoSocial = Session["RELATORIO_TIPO_PROTECAO_ID"] as int?;
            filtro.TipoServico        = Session["RELATORIO_TIPO_SERVICO_ID"] as int?;
            filtro.Usuario            = Session["RELATORIO_PUBLICO_ALVO_ID"] as int?;
            filtro.ServicoSubtificado = Session["RELATORIO_SERVICO_SUBTIFICADO_ID"] as int?;

            filtro.Sexo               = Session["SEXO"] as int?;
            filtro.RegiaoMoradia      = Session["REGIAOMORADIA"] as int?;
            filtro.CaracteristicasTerritorio = Session["CARACTERISTICASTERRITORIO"] as int?;
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;
            filtro.ehAtivo = Convert.ToBoolean(Session["ATIVO"]);
            filtro.ehDesativo = Convert.ToBoolean(Session["DESATIVO"]);

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<RedeServicoSocioassistencialRelatorio>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetRedeServicoSocioassistencialSimples(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            if (filtro.TipoServico.HasValue && filtro.TipoServico.Value == 141)
            {
                lst.Visible = false;
                lstServicoPSC.Visible = true;
                lstServicoPSC.DataSource = items;
                lstServicoPSC.DataBind();
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensal2018")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimentoLA2022));
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensal2019")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimentoLA2023));
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensal2020")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimentoLA2024));
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensal2021")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimentoLA2025));
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensalPSC2018")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimentoPSC2022));                
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensalPSC2019")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimentoPSC2023));                
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensalPSC2020")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimentoPSC2024));                
                ((Label)lstServicoPSC.FindControl("lblNumeroAtendidosMensalPSC2021")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimentoPSC2025));
                ((Label)lstServicoPSC.FindControl("lblMediaMensal2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensalLA2021));
                ((Label)lstServicoPSC.FindControl("lblMediaMensalPSC2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensalPSC2021));
                ((Label)lstServicoPSC.FindControl("lblMediaMensal2018")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensalLA2022));
                ((Label)lstServicoPSC.FindControl("lblMediaMensalPSC2018")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensalPSC2022));
                ((Label)lstServicoPSC.FindControl("lblMediaMensal2019")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensalLA2023));
                ((Label)lstServicoPSC.FindControl("lblMediaMensalPSC2019")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensalPSC2023));
                ((Label)lstServicoPSC.FindControl("lblMediaMensal2020")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensalLA2024));
                ((Label)lstServicoPSC.FindControl("lblMediaMensalPSC2020")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensalPSC2024));
                ((Label)lstServicoPSC.FindControl("lblNumeroTrabalhadoresServico")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalTrabalhadores));
                ((Label)lstServicoPSC.FindControl("lblTotal")).Text = String.Format("{0:N2}", items.Sum(i => i.Total));
                ((Label)lstServicoPSC.FindControl("lblTotalFMAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMAS));
                ((Label)lstServicoPSC.FindControl("lblTotalFEAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEAS));
                ((Label)lstServicoPSC.FindControl("lblTotalFEASReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEASAnoAnterior));
                ((Label)lstServicoPSC.FindControl("lblTotalDemandasParlamentares")).Text = String.Format("{0:N2}", items.Sum(i => i.DemandasParlamentares));
                ((Label)lstServicoPSC.FindControl("lblTotalDemandasParlamentaresReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.DemandasParlamentaresReprogramacao));
                ((Label)lstServicoPSC.FindControl("lblTotalFNAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNAS));
                ((Label)lstServicoPSC.FindControl("lblTotalFMDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMDCA));
                ((Label)lstServicoPSC.FindControl("lblTotalFEDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEDCA));
                ((Label)lstServicoPSC.FindControl("lblTotalFNDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNDCA));
                ((Label)lstServicoPSC.FindControl("lblTotalFMI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMI));
                ((Label)lstServicoPSC.FindControl("lblTotalFEI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEI));
                ((Label)lstServicoPSC.FindControl("lblTotalFNI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNI));
                ((Label)lstServicoPSC.FindControl("lblTotalPrivado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorPrivado));
                ((Label)lstServicoPSC.FindControl("lblTotalEstadualizado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorEstadualizado));
                ((Label)lstServicoPSC.FindControl("lblValorFonteRecurso")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFonteRecurso));
                
            }
            else
            {


                if (items.Count() >= 2000)
                {
                    (lst.FindControl("dpLista") as DataPager).Visible = true;
                }
                else
                {
                    (lst.FindControl("dpLista") as DataPager).Visible = false;
                }

                ((Label)lst.FindControl("lblNumeroAtendidosMensal2018")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimento2022 + i.CapacidadeMensalAtendimentoLA2022 + i.CapacidadeMensalAtendimentoPSC2022));
                ((Label)lst.FindControl("lblNumeroAtendidosMensal2019")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimento2023 + i.CapacidadeMensalAtendimentoLA2023 + i.CapacidadeMensalAtendimentoPSC2023));
                ((Label)lst.FindControl("lblNumeroAtendidosMensal2020")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimento2024 + i.CapacidadeMensalAtendimentoLA2024 + i.CapacidadeMensalAtendimentoPSC2024));
                ((Label)lst.FindControl("lblNumeroAtendidosMensal2021")).Text = String.Format("{0:0,0}", items.Sum(i => i.CapacidadeMensalAtendimento2025 + i.CapacidadeMensalAtendimentoLA2025 + i.CapacidadeMensalAtendimentoPSC2025));
                ((Label)lst.FindControl("lblMediaMensal2017")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensal2021 + i.MediaMensalLA2021 + i.MediaMensalPSC2021));
                ((Label)lst.FindControl("lblMediaMensal2018")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensal2022 + i.MediaMensalLA2022 + i.MediaMensalPSC2022));
                ((Label)lst.FindControl("lblMediaMensal2019")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensal2023 + i.MediaMensalLA2023 + i.MediaMensalPSC2023));
                ((Label)lst.FindControl("lblMediaMensal2020")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaMensal2024 + i.MediaMensalLA2024 + i.MediaMensalPSC2024));
                ((Label)lst.FindControl("lblNumeroTrabalhadoresServico")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalTrabalhadores));
                ((Label)lst.FindControl("lblTotal")).Text = String.Format("{0:N2}", items.Sum(i => i.Total));
                ((Label)lst.FindControl("lblTotalFMAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMAS));
                ((Label)lst.FindControl("lblTotalFEAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEAS));
                ((Label)lst.FindControl("lblTotalFEASReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEASAnoAnterior));
                ((Label)lst.FindControl("lblTotalDemandasParlamentares")).Text = String.Format("{0:N2}", items.Sum(i => i.DemandasParlamentares));
                ((Label)lst.FindControl("lblTotalDemandasParlamentaresReprogramado")).Text = String.Format("{0:N2}", items.Sum(i => i.DemandasParlamentaresReprogramacao));
                ((Label)lst.FindControl("lblTotalFNAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNAS));
                ((Label)lst.FindControl("lblTotalFMDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMDCA));
                ((Label)lst.FindControl("lblTotalFEDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEDCA));
                ((Label)lst.FindControl("lblTotalFNDCA")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNDCA));
                ((Label)lst.FindControl("lblTotalFMI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMI));
                ((Label)lst.FindControl("lblTotalFEI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEI));
                ((Label)lst.FindControl("lblTotalFNI")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNI));
                ((Label)lst.FindControl("lblTotalPrivado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorPrivado));
                ((Label)lst.FindControl("lblTotalEstadualizado")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorEstadualizado));
                ((Label)lst.FindControl("lblValorFonteRecurso")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFonteRecurso));
            }
        }

        protected void GerarExcel_Click(object sender, EventArgs e)
        {
            MetodoImportacao(false);
        }

        protected void GeraXLSX_Click(object sender, EventArgs e)
        {
            MetodoImportacao(true);
        }

        private void MetodoImportacao(bool xlsx) 
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
            filtro.ehAtivo =  Convert.ToBoolean(Session["ATIVO"]);
            filtro.ehDesativo = Convert.ToBoolean(Session["DESATIVO"]);

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<RedeServicoSocioassistencialRelatorio>();
            
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetRedeServicoSocioassistencialSimples(filtro).ToList();
            }

            if (xlsx)
            {

                DataTable dt = new DataTable();

                dt = ConvertTo(items);

                gerarExcel(dt);
            }
            else
            {
                ImportarDados(items);
            }
        }

        public static DataTable ConvertTo(IList<RedeServicoSocioassistencialRelatorio> list)
        {

            DataTable dt = new DataTable();

            DataTable table = CreateTable();

            Type entityType = typeof(RedeServicoSocioassistencialRelatorio);

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (RedeServicoSocioassistencialRelatorio item in list)
            {
                DataRow row = table.NewRow();


                foreach (PropertyDescriptor prop in properties)
                {

                    if (prop.Name == "CodigoUnidade" || prop.Name == "IdLocal" || prop.Name == "Municipio" || prop.Name == "Porte" || prop.Name == "DistritoSaoPaulo" || prop.Name == "Drads" || prop.Name == "TipoUnidade" || prop.Name == "UnidadeResponsavel" || prop.Name == "LocalExecucao" || prop.Name == "ProtecaoSocial"
                     || prop.Name == "TipoServico" || prop.Name == "Usuarios" || prop.Name == "DataFuncionamentoServico" || prop.Name == "DataDesativacao" || prop.Name == "Abrangencia" || prop.Name == "CapacidadeMensalAtendimentoTotal2022" || prop.Name == "CapacidadeMensalAtendimentoTotal2023" || prop.Name == "CapacidadeMensalAtendimentoTotal2024" || prop.Name == "CapacidadeMensalAtendimentoTotal2025" || prop.Name == "MediaTotal2021"
                     || prop.Name == "MediaTotal2022" || prop.Name == "MediaTotal2023" || prop.Name == "MediaTotal2024" || prop.Name == "Sexo" || prop.Name == "RegiaoMoradia" || prop.Name == "TotalTrabalhadores" || prop.Name == "CaracteristicasTerritorio" || prop.Name == "ValorFMAS" || prop.Name == "ValorFMDCA" || prop.Name == "ValorFMI"
                     || prop.Name == "ValorFEAS" || prop.Name == "ValorFEASAnoAnterior" || prop.Name == "DemandasParlamentares" || prop.Name == "DemandasParlamentaresReprogramacao" || prop.Name == "ValorFEDCA" || prop.Name == "ValorFEI" || prop.Name == "ValorFNAS" || prop.Name == "ValorFNDCA" || prop.Name == "ValorFNI" || prop.Name == "ValorPrivado"
                     || prop.Name == "ValorEstadualizado" || prop.Name == "ValorFonteRecurso" || prop.Name == "Total"
                        )
                    {
                        switch (prop.Name)
                        {
                            case "CodigoUnidade":

                                row[prop.Name] = Convert.ToInt32(item.CodigoUnidade);
                                break;

                            case "IdLocal":

                                row[prop.Name] = item.IdLocal.ToString();
                                break;

                            case "Municipio":

                                row[prop.Name] = item.Municipio.ToString();
                                break;

                            case "Porte":

                                row[prop.Name] = item.Porte.ToString();
                                break;

                            case "DistritoSaoPaulo":

                                row[prop.Name] = item.DistritoSaoPaulo.ToString();
                                break;

                            case "Drads":

                                row[prop.Name] = item.Drads.ToString();
                                break;

                            case "TipoUnidade":

                                row[prop.Name] = item.TipoUnidade.ToString();
                                break;

                            case "UnidadeResponsavel":

                                row[prop.Name] = item.UnidadeResponsavel.ToString();
                                break;

                            case "LocalExecucao":

                                row[prop.Name] = item.LocalExecucao.ToString();
                                break;

                            case "ProtecaoSocial":

                                row[prop.Name] = item.ProtecaoSocial.ToString();
                                break;

                            case "TipoServico":

                                row[prop.Name] = item.TipoServico.ToString();
                                break;

                            case "Usuarios":

                                row[prop.Name] = item.Usuarios.ToString();
                                break;

                            case "DataFuncionamentoServico":

                                row[prop.Name] = item.DataFuncionamentoServico.ToString();
                                break;

                            case "DataDesativacao":

                                row[prop.Name] = item.DataDesativacao.ToString();
                                break;

                            case "Abrangencia":

                                row[prop.Name] = item.Abrangencia.ToString();
                                break;

                            case "CapacidadeMensalAtendimentoTotal2022":

                                row[prop.Name] = Convert.ToInt32(item.CapacidadeMensalAtendimentoTotal2022);
                                break;

                            case "CapacidadeMensalAtendimentoTotal2023":

                                row[prop.Name] = Convert.ToInt32(item.CapacidadeMensalAtendimentoTotal2023);
                                break;

                            case "CapacidadeMensalAtendimentoTotal2024":
                                row[prop.Name] = Convert.ToInt32(item.CapacidadeMensalAtendimentoTotal2024);
                                break;
                            case "CapacidadeMensalAtendimentoTotal2025":
                                row[prop.Name] = Convert.ToInt32(item.CapacidadeMensalAtendimentoTotal2025);
                                break;
                            case "MediaTotal2021":
                                row[prop.Name] = Convert.ToInt32(item.MediaTotal2021);
                                break;
                            case "MediaTotal2022":
                                row[prop.Name] = Convert.ToInt32(item.MediaTotal2022);
                                break;
                            case "MediaTotal2023":
                                row[prop.Name] = Convert.ToInt32(item.MediaTotal2023);
                                break;
                            case "MediaTotal2024":
                                row[prop.Name] = Convert.ToInt32(item.MediaTotal2024);
                                break;
                            case "Sexo":
                                row[prop.Name] = item.Sexo.ToString();
                                break;
                            case "RegiaoMoradia":
                                row[prop.Name] = item.RegiaoMoradia.ToString();
                                break;
                            case "TotalTrabalhadores":
                                row[prop.Name] = Convert.ToInt32(item.TotalTrabalhadores);
                                break;
                            case "CaracteristicasTerritorio":
                                row[prop.Name] = item.CaracteristicasTerritorio.ToString();
                                break;
                            case "ValorFMAS":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFMAS);
                                break;
                            case "ValorFMDCA":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFMDCA);
                                break;
                            case "ValorFMI":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFMI);
                                break;
                            case "ValorFEAS":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFEAS);
                                break;
                            case "ValorFEASAnoAnterior":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFEASAnoAnterior);
                                break;
                            case "DemandasParlamentares":
                                row[prop.Name] = Convert.ToDecimal(item.DemandasParlamentares);
                                break;
                            case "DemandasParlamentaresReprogramacao":
                                row[prop.Name] = Convert.ToDecimal(item.DemandasParlamentaresReprogramacao);
                                break;
                            case "ValorFEDCA":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFEDCA);
                                break;
                            case "ValorFEI":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFEI);
                                break;
                            case "ValorFNAS":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFNAS);
                                break;
                            case "ValorFNDCA":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFNDCA);
                                break;
                            case "ValorFNI":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFNI);
                                break;
                            case "ValorPrivado":
                                row[prop.Name] = Convert.ToDecimal(item.ValorPrivado);
                                break;
                            case "ValorEstadualizado":
                                row[prop.Name] = Convert.ToDecimal(item.ValorEstadualizado);
                                break;
                            case "ValorFonteRecurso":
                                row[prop.Name] = Convert.ToDecimal(item.ValorFonteRecurso);
                                break;
                            case "Total":
                                row[prop.Name] = String.Format("{0:N2}", item.Total);
                                break;
                            default:
                                break;
                        }
                    }
                }



                table.Rows.Add(row);
            }

            return table;
        }


        public static DataTable CreateTable()
        {
            Type entityType = typeof(RedeServicoSocioassistencialRelatorio);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.Name == "CodigoUnidade" || prop.Name == "IdLocal" || prop.Name == "Municipio" || prop.Name == "Porte" || prop.Name == "DistritoSaoPaulo" || prop.Name == "Drads" || prop.Name == "TipoUnidade" || prop.Name == "UnidadeResponsavel" || prop.Name == "LocalExecucao" || prop.Name == "ProtecaoSocial"
                 || prop.Name == "TipoServico" || prop.Name == "Usuarios" || prop.Name == "DataFuncionamentoServico" || prop.Name == "DataDesativacao" || prop.Name == "Abrangencia" || prop.Name == "CapacidadeMensalAtendimentoTotal2022" || prop.Name == "CapacidadeMensalAtendimentoTotal2023" || prop.Name == "CapacidadeMensalAtendimentoTotal2024" || prop.Name == "CapacidadeMensalAtendimentoTotal2025" || prop.Name == "MediaTotal2021"
                 || prop.Name == "MediaTotal2022" || prop.Name == "MediaTotal2023" || prop.Name == "MediaTotal2024" || prop.Name == "Sexo" || prop.Name == "RegiaoMoradia" || prop.Name == "TotalTrabalhadores" || prop.Name == "CaracteristicasTerritorio" || prop.Name == "ValorFMAS" || prop.Name == "ValorFMDCA" || prop.Name == "ValorFMI"
                 || prop.Name == "ValorFEAS" || prop.Name == "ValorFEASAnoAnterior" || prop.Name == "DemandasParlamentares" || prop.Name == "DemandasParlamentaresReprogramacao" || prop.Name == "ValorFEDCA" || prop.Name == "ValorFEI" || prop.Name == "ValorFNAS" || prop.Name == "ValorFNDCA" || prop.Name == "ValorFNI" || prop.Name == "ValorPrivado"
                 || prop.Name == "ValorEstadualizado" || prop.Name == "ValorFonteRecurso" || prop.Name == "Total"
                    )
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }


            return table;
        }

        void gerarExcel(DataTable dt)
        {


            SLDocument sl = new SLDocument();
            sl.ApplyNamedCellStyle(2, 2, SLNamedCellStyleValues.Good);
            sl.ImportDataTable(1, 1, dt, true);
            Response.Clear();
            sl.DocumentProperties.Subject = "RelatorioDescritivo12";

            sl.SetCellValue("B1", "Local de execução ou ID-SUAS");
            sl.SetCellValue("A1", "Organização/Unidade");
            sl.SetCellValue("E1", "Distrito");
            sl.SetCellValue("G1", "TipoDeRede");
            sl.SetCellValue("H1", "NomeDaOrganizaçãoUnidade");
            sl.SetCellValue("I1", "LocalDeExecuçãoDosServiços");
            sl.SetCellValue("L1", "Usuários");
            sl.SetCellValue("M1", "InicioFuncionamentoServiço");
            sl.SetCellValue("N1", "EncerramentoFuncionamentoServiço");
            sl.SetCellValue("Y1", "RegiãoDeMoradia");
            sl.SetCellValue("Z1", "TotalDeTrabalhadoresDoServiço");
            sl.SetCellValue("AA1", "EsteServiçoAtendeAlgumaComunidadeTradicionalOuGrupoEspecífico");
            sl.SetCellValue("AB1", "FMAS");
            sl.SetCellValue("AC1", "FMDCA");
            sl.SetCellValue("AD1", "FMI");
            sl.SetCellValue("AE1", "FEAS");
            sl.SetCellValue("AF1", "FEASReprogramado");
            sl.SetCellValue("AH1", "DemandasParlamentaresReprogramado");
            sl.SetCellValue("AI1", "FEDCA");
            sl.SetCellValue("AJ1", "FEI");
            sl.SetCellValue("AK1", "FNAS");
            sl.SetCellValue("AL1", "FNDCA");
            sl.SetCellValue("AM1", "FNI");
            sl.SetCellValue("AN1", "RecursosPropriosDaOrganização");
            sl.SetCellValue("AO1", "ValorDoConvenioEstadualizado");
            sl.SetCellValue("AP1", "RecursosDeOutrasFontes");
            sl.SetCellValue("AQ1", "TotalDeRecursos");

            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=RelatorioDescritivo12.xlsx");
            sl.SaveAs(Response.OutputStream);
            Response.End();


        }

        private void ImportarDados(List<RedeServicoSocioassistencialRelatorio> lista) 
        {
            var listCarregaExcel = (from x in lista 
                              select new { OrganizacaoUnidade = x.CodigoUnidade,
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
                                      CapacidadeMensalDeAtendimento2022 = String.Format("{0:0,0}", x.CapacidadeMensalAtendimento2022 + x.CapacidadeMensalAtendimentoLA2022 + x.CapacidadeMensalAtendimentoPSC2022),
                                      CapacidadeMensalDeAtendimento2023 = String.Format("{0:0,0}", x.CapacidadeMensalAtendimento2023 + x.CapacidadeMensalAtendimentoLA2023 + x.CapacidadeMensalAtendimentoPSC2023),
                                      CapacidadeMensalDeAtendimento2024 = String.Format("{0:0,0}", x.CapacidadeMensalAtendimento2024 + x.CapacidadeMensalAtendimentoLA2024 + x.CapacidadeMensalAtendimentoPSC2024),
                                      CapacidadeMensalDeAtendimento2025 = String.Format("{0:0,0}", x.CapacidadeMensalAtendimento2025 + x.CapacidadeMensalAtendimentoLA2025 + x.CapacidadeMensalAtendimentoPSC2025),
                                      MediaMensalDeAtendimento2021 = String.Format("{0:0,0}", x.MediaMensal2021 + x.MediaMensalLA2021 + x.MediaMensalPSC2021),
                                      MediaMensalDeAtendimento2022 = String.Format("{0:0,0}", x.MediaMensal2022 + x.MediaMensalLA2022 + x.MediaMensalPSC2022),
                                      MediaMensalDeAtendimento2023 = String.Format("{0:0,0}", x.MediaMensal2023 + x.MediaMensalLA2023 + x.MediaMensalPSC2023),
                                      MediaMensalDeAtendimento2024 = String.Format("{0:0,0}", x.MediaMensal2024 + x.MediaMensalLA2024 + x.MediaMensalPSC2024),
                                      Sexo = x.Sexo,
                                      RegiãoDeMoradia = x.RegiaoMoradia,
                                      TotalDeTrabalhadoresDoServiço = x.TotalTrabalhadores,
                                      EsteServiçoAtendeAlgumaComunidadeTradicionalOuGrupoEspecífico = x.CaracteristicasTerritorio,
                                      FMAS = String.Format("{0:N2}", x.ValorFMAS),
                                      FMDCA = String.Format("{0:N2}", x.ValorFMDCA),
                                      FMI = String.Format("{0:N2}", x.ValorFMI),
                                      FEAS = String.Format("{0:N2}", x.ValorFEAS),
                                      FEASReprogramado = String.Format("{0:N2}", x.ValorFEASAnoAnterior),
                                      DemandasParlamentares = String.Format("{0:N2}", x.DemandasParlamentares),
                                      DemandasParlamentaresReprogramado = String.Format("{0:N2}", x.DemandasParlamentaresReprogramacao),
                                      FEDCA = String.Format("{0:N2}", x.ValorFEDCA),
                                      FEI = String.Format("{0:N2}", x.ValorFEI),
                                      FNAS = String.Format("{0:N2}", x.ValorFNAS),
                                      FNDCA = String.Format("{0:N2}", x.ValorFNDCA),
                                      FNI = String.Format("{0:N2}", x.ValorFNI),
                                      RecursosPropriosDaOrganização = String.Format("{0:N2}", x.ValorPrivado),
                                      ValorDoConvenioEstadualizado = String.Format("{0:N2}", x.ValorEstadualizado),
                                      RecursosDeOutrasFontes = String.Format("{0:N2}",x.ValorFonteRecurso),
                                      TotalDeRecursos = String.Format("{0:N2}", x.Total)
                              });

            if (listCarregaExcel != null)
            {
                DataGrid rptExcel = new DataGrid();

                rptExcel.DataSource = listCarregaExcel;

                rptExcel.DataBind();

                Response.Clear();

                Response.Buffer = true;

                Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

                //Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo12.xlsx");
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RelatorioDescritivo12.xls"));
                Response.Charset = "";

                Response.ContentType = "application/vnd.xlx";

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