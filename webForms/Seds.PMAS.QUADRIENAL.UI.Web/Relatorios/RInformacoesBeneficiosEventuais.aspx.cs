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
using System.Data;
using SpreadsheetLight;
using System.ComponentModel;






namespace Seds.PMAS.QUADRIENAL.UI.Web.Relatorios
{
    public partial class RInformacoesBeneficiosEventuais : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 19 - Informa&#231;&#245;es sobre os benef&#237;cios eventuais";
            Master.MainWidthRelatorio = Master.WidthRelatorio = "2600px";
            Master.GerarExcel.Click += new EventHandler(GerarExcel_Click);
            Master.GeraXLSX.Click += new EventHandler(GeraXLSX_Click);
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

            filtro.TipoBeneficioEventual = Session["TIPO_BENEFICIO_EVENTUAL"] as Int32?;

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<InformacoesBeneficiosEventuaisInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetInformacoesBeneficiosEventuais(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalFMAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFMAS));
            ((Label)lst.FindControl("lblTotalFEAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFEAS));
            ((Label)lst.FindControl("lblTotalFNAS")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFNAS));
            ((Label)lst.FindControl("lblValorDemandasParlamentares")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorDemandasParlamentares));
            ((Label)lst.FindControl("lblValorReprogramacaoAnoAnterior")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorReprogramacaoAnoAnterior));
            ((Label)lst.FindControl("lblTotalOrcamentoMunicipal")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorOrcamentoMunicipal));
            ((Label)lst.FindControl("lblTotalFundoSocialSolidariedadeMunicipal")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFundoMunicipalSolidariedade));
            ((Label)lst.FindControl("lblTotalFundoSocialSolidariedadeEstadual")).Text = String.Format("{0:N2}", items.Sum(i => i.ValorFundoEstadualSolidariedade));
            ((Label)lst.FindControl("lblTotal")).Text = String.Format("{0:N2}", items.Sum(i => i.Total));

            ((Label)lst.FindControl("lblTotalMediaSemestralBeneficiarios")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaSemestralBeneficiarios));
            ((Label)lst.FindControl("lblTotalMediaSemestralBeneficios")).Text = String.Format("{0:0,0}", items.Sum(i => i.MediaSemestralBeneficiosConcedidos));

            ((Label)lst.FindControl("lblTotalRegulamentado")).Text = String.Format("{0:0,0}", items.Sum(i => i.Regulamentado ? 1 : 0));
            ((Label)lst.FindControl("lblTotalOrgaoGestor")).Text = String.Format("{0:0,0}", items.Sum(i => i.OrgaoGestorResponsavel ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCRAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.CRASResponsavel ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCREAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.CREASResponsavel ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCentroPOP")).Text = String.Format("{0:0,0}", items.Sum(i => i.CentroPOPResponsavel ? 1 : 0));
            ((Label)lst.FindControl("lblTotalUnidadePrivada")).Text = String.Format("{0:0,0}", items.Sum(i => i.UnidadePrivadaResponsavel ? 1 : 0));
            ((Label)lst.FindControl("lblTotalFundoSocialSolidariedade")).Text = String.Format("{0:0,0}", items.Sum(i => i.FundoSocialSolidariedadeResponsavel ? 1 : 0));

            ((Label)lst.FindControl("lblTotalIntegracaoServicos")).Text = String.Format("{0:0,0}", items.Sum(i => i.IntegracaoServicos == "Sim" ? 1 : 0));
            ((Label)lst.FindControl("lblTotalServicosAssociados")).Text = String.Format("{0:0,0}", items.Sum(i => i.TotalServicosAssociados));
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
            filtro.Exercicio = Session["RELATORIO_EXERCICIO"] as int?;

            filtro.TipoBeneficioEventual = Session["TIPO_BENEFICIO_EVENTUAL"] as Int32?;

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<InformacoesBeneficiosEventuaisInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetInformacoesBeneficiosEventuais(filtro).ToList();
            }

            DataTable dt = new DataTable();

            dt = ConvertTo(items);

            GerarXLSX(dt);
        }

        protected void GerarExcel_Click(object sender, EventArgs e)
        {
            gerarExcel();
        }

        protected void GeraXLSX_Click(object sender, EventArgs e)
        {
            MetodoImportacao();
        }

        protected static DataTable ConvertTo(IList<InformacoesBeneficiosEventuaisInfo> list)
        {

            DataTable dt = new DataTable();

            DataTable table = CreateTable();

            Type entityType = typeof(InformacoesBeneficiosEventuaisInfo);

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (InformacoesBeneficiosEventuaisInfo item in list)
            {
                DataRow row = table.NewRow();


                foreach (PropertyDescriptor prop in properties)
                {

                    if (prop.Name == "Municipio" || prop.Name == "Drads" || prop.Name == "Beneficio"
                     || prop.Name == "Regulamentado" || prop.Name == "PossuiLeiMunicipal" || prop.Name == "NumeroLegislacao"
                     || prop.Name == "DataPublicacaoLei" || prop.Name == "PossuiResolucao" || prop.Name == "NumeroResolucao"
                     || prop.Name == "DataResolucao" || prop.Name == "PossuiDecreto" || prop.Name == "NumeroDecreto"
                     || prop.Name == "DataDecreto" || prop.Name == "MediaSemestralBeneficiarios" || prop.Name == "MediaSemestralBeneficiosConcedidos"
                     || prop.Name == "FormaAuxilio" || prop.Name == "OrgaoGestorResponsavel" || prop.Name == "CRASResponsavel"
                     || prop.Name == "UnidadePrivadaResponsavel" || prop.Name == "CREASResponsavel" || prop.Name == "CentroPOPResponsavel"
                     || prop.Name == "FundoSocialSolidariedadeResponsavel" || prop.Name == "IntegracaoServicos" || prop.Name == "TotalServicosAssociados"
                     || prop.Name == "ValorFMAS" || prop.Name == "ValorFundoMunicipalSolidariedade" || prop.Name == "ValorOrcamentoMunicipal"
                     || prop.Name == "ValorFEAS" || prop.Name == "ValorReprogramacaoAnoAnterior" || prop.Name == "ValorFundoEstadualSolidariedade"
                     || prop.Name == "ValorFNAS" || prop.Name == "ValorDemandasParlamentares" || prop.Name == "Total")
                    {
                        switch (prop.Name)
                        {

                            case "Municipio":

                                row[prop.Name] = item.Municipio.ToString();
                                break;

                            case "Drads":

                                row[prop.Name] = item.Drads.ToString();
                                break;

                            case "Beneficio":

                                row[prop.Name] = item.Beneficio;
                                break;

                            case "Regulamentado":

                                if (item.Regulamentado)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não";
                                }
                                break;

                            case "PossuiLeiMunicipal":

                                    row[prop.Name] = item.PossuiLeiMunicipal;

                                break;

                            case "NumeroLegislacao":

                                    row[prop.Name] = item.NumeroLegislacao;

                                break;

                            case "DataPublicacaoLei":

                                row[prop.Name] = item.DataPublicacaoLei;

                                break;

                            case "PossuiResolucao":

                                row[prop.Name] = item.PossuiResolucao;

                                break;

                            case "NumeroResolucao":

                                row[prop.Name] = item.NumeroResolucao;

                                break;

                            case "DataResolucao":

                                row[prop.Name] = item.DataResolucao;

                                break;

                            case "PossuiDecreto":

                                row[prop.Name] = item.PossuiDecreto;
                                
                                break;

                            case "NumeroDecreto":

                                row[prop.Name] = item.NumeroDecreto;

                                break;

                            case "DataDecreto":

                                row[prop.Name] = item.DataDecreto;

                                break;

                            case "MediaSemestralBeneficiarios":

                                row[prop.Name] = item.MediaSemestralBeneficiarios;

                                break;

                            case "MediaSemestralBeneficiosConcedidos":

                                row[prop.Name] = item.MediaSemestralBeneficiosConcedidos;

                                break;

                            case "FormaAuxilio":

                                row[prop.Name] = item.FormaAuxilio;

                                break;

                            case "OrgaoGestorResponsavel":

                                if (item.OrgaoGestorResponsavel)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não";
                                }
                                
                                break;

                            case "CRASResponsavel":

                                if (item.CRASResponsavel)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não";
                                }

                                break;

                            case "UnidadePrivadaResponsavel":

                                if (item.UnidadePrivadaResponsavel)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não";
                                }

                                break;

                            case "CREASResponsavel":

                                if (item.CREASResponsavel)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não";
                                }

                                break;

                            case "CentroPOPResponsavel":

                                if (item.CentroPOPResponsavel)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não";
                                }

                                break;

                            case "FundoSocialSolidariedadeResponsavel":

                                if (item.FundoSocialSolidariedadeResponsavel)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não";
                                }

                                break;

                            case "IntegracaoServicos":

                                row[prop.Name] = item.IntegracaoServicos;

                                break;

                            case "TotalServicosAssociados":

                                row[prop.Name] = item.TotalServicosAssociados;

                                break;

                            case "ValorFMAS":

                                row[prop.Name] = item.ValorFMAS;

                                break;

                            case "ValorFundoMunicipalSolidariedade":

                                row[prop.Name] = item.ValorFundoMunicipalSolidariedade;

                                break;

                            case "ValorOrcamentoMunicipal":

                                row[prop.Name] = item.ValorOrcamentoMunicipal;

                                break;

                            case "ValorFEAS":

                                row[prop.Name] = item.ValorFEAS;

                                break;

                            case "ValorReprogramacaoAnoAnterior":

                                row[prop.Name] = item.ValorReprogramacaoAnoAnterior;

                                break;

                            case "ValorFundoEstadualSolidariedade":

                                row[prop.Name] = item.ValorFundoEstadualSolidariedade;

                                break;

                            case "ValorFNAS":

                                row[prop.Name] = item.ValorFNAS;

                                break;

                            case "ValorDemandasParlamentares":

                                row[prop.Name] = item.ValorDemandasParlamentares;

                                break;

                            case "Total":

                                row[prop.Name] = item.Total;

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


        protected static DataTable CreateTable()
        {
            Type entityType = typeof(InformacoesBeneficiosEventuaisInfo);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.Name == "Municipio" || prop.Name == "Drads" || prop.Name == "Beneficio"
                     || prop.Name == "Regulamentado" || prop.Name == "PossuiLeiMunicipal" || prop.Name == "NumeroLegislacao"
                     || prop.Name == "DataPublicacaoLei" || prop.Name == "PossuiResolucao" || prop.Name == "NumeroResolucao"
                     || prop.Name == "DataResolucao" || prop.Name == "PossuiDecreto" || prop.Name == "NumeroDecreto"
                     || prop.Name == "DataDecreto" || prop.Name == "MediaSemestralBeneficiarios" || prop.Name == "MediaSemestralBeneficiosConcedidos"
                     || prop.Name == "FormaAuxilio" || prop.Name == "OrgaoGestorResponsavel" || prop.Name == "CRASResponsavel"
                     || prop.Name == "UnidadePrivadaResponsavel" || prop.Name == "CREASResponsavel" || prop.Name == "CentroPOPResponsavel"
                     || prop.Name == "FundoSocialSolidariedadeResponsavel" || prop.Name == "IntegracaoServicos" || prop.Name == "TotalServicosAssociados"
                     || prop.Name == "ValorFMAS" || prop.Name == "ValorFundoMunicipalSolidariedade" || prop.Name == "ValorOrcamentoMunicipal"
                     || prop.Name == "ValorFEAS" || prop.Name == "ValorReprogramacaoAnoAnterior" || prop.Name == "ValorFundoEstadualSolidariedade"
                     || prop.Name == "ValorFNAS" || prop.Name == "ValorDemandasParlamentares" || prop.Name == "Total")
                {
                    if (prop.Name == "Regulamentado" || prop.Name == "OrgaoGestorResponsavel" || prop.Name == "CRASResponsavel" || prop.Name == "UnidadePrivadaResponsavel" || prop.Name == "CREASResponsavel" || prop.Name == "CentroPOPResponsavel"
                        || prop.Name == "FundoSocialSolidariedadeResponsavel")
                    {
                        table.Columns.Add(prop.Name, typeof(String));
                    }
                    else
                    {
                        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }
                }

            }

            return table;
        }

        void GerarXLSX(DataTable dt)
        {


            SLDocument sl = new SLDocument();
            sl.ApplyNamedCellStyle(2, 2, SLNamedCellStyleValues.Good);
            sl.ImportDataTable(1, 1, dt, true);
            Response.Clear();
            sl.DocumentProperties.Subject = "RelatorioDescritivo19";

            sl.SetCellValue("A1", "Municipio");
            sl.SetCellValue("B1", "Drads");
            sl.SetCellValue("C1", "Tipo de benefício");
            sl.SetCellValue("D1", "Existe regulamentação municipal?");
            sl.SetCellValue("E1", "Lei Municipal");
            sl.SetCellValue("F1", "Número da Lei Municipal");
            sl.SetCellValue("G1", "Data de Publicação da Lei");
            sl.SetCellValue("H1", "Resolução");
            sl.SetCellValue("I1", "Número da Resolução");
            sl.SetCellValue("J1", "Data de Publicação da Resolução");
            sl.SetCellValue("K1", "Decreto/Portaria");
            sl.SetCellValue("L1", "Número do Decreto/Portaria");
            sl.SetCellValue("M1", "Data de Publicação da Decreto/Portaria");
            sl.SetCellValue("N1", "Média anual de beneficiários");
            sl.SetCellValue("O1", "Média anual de benefícios concedidos");
            sl.SetCellValue("P1", "Forma de auxílio");
            sl.SetCellValue("Q1", "Responsável pela execução");
            sl.SetCellValue("R1", "Integração com serviços");
            sl.SetCellValue("S1", "Origem dos recursos financeiros");
            sl.SetCellValue("T1", "Total de recursos");
            sl.SetCellValue("U1", "Órgão Gestor");
            sl.SetCellValue("V1", "CRAS");
            sl.SetCellValue("W1", "CRAS");
            sl.SetCellValue("X1", "Unidade socioassistencial privada");
            sl.SetCellValue("Y1", "CRAS");
            sl.SetCellValue("Z1", "CREAS");
            sl.SetCellValue("AA1", "Centro POP");
            sl.SetCellValue("AB1", "Fundo Social de Solidariedade");
            sl.SetCellValue("AC1", "FMAS (R$)");
            sl.SetCellValue("AD1", "Fundo Social de Solidariedade (municipal) (R$)");
            sl.SetCellValue("AE1", "Orçamento Municipal (R$)");
            sl.SetCellValue("AF1", "FEAS (R$)");
            sl.SetCellValue("AG1", "Valor Reprogramação FEAS (R$)");
            sl.SetCellValue("AH1", "Fundo Social de Solidariedade (estadual) (R$)");
            sl.SetCellValue("AI1", "FNAS (R$)");
            sl.SetCellValue("AJ1", "Demandas Parlamentares (R$)");


            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=RelatorioDescritivo19.xlsx");
            sl.SaveAs(Response.OutputStream);
            Response.End();
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