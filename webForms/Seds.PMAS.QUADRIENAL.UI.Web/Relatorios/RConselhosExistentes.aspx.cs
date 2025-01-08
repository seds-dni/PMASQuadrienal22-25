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
    public partial class RConselhosExistentes : System.Web.UI.Page
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
            Master.Titulo = "Relat&#243;rio descritivo 7 - Conselhos existentes nos munic&#237;pios";
            Master.WidthRelatorio = "1420";
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

            Master.mostrarFiltros(filtro,ETipoRelatorio.Descritivo);
            var items = new List<ConselhosMunicipaisExistentesInfo>();
            using(var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetConselhosMunicipaisExistentes(filtro).ToList();
            }
            lst.DataSource = items;
            lst.DataBind();

            if (items.Count == 0)
                return;

            ((Label)lst.FindControl("lblTotalCMAS")).Text = String.Format("{0:0,0}", items.Sum(i => i.CMAS ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCMDCA")).Text = String.Format("{0:0,0}", items.Sum(i => i.CMDCA ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCME")).Text = String.Format("{0:0,0}", items.Sum(i => i.CME ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCMI")).Text = String.Format("{0:0,0}", items.Sum(i => i.CMI ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCONSEA")).Text = String.Format("{0:0,0}", items.Sum(i => i.CONSEA ? 1 : 0));
            ((Label)lst.FindControl("lblTotalCT")).Text = String.Format("{0:0,0}", items.Sum(i => i.CT));
            ((Label)lst.FindControl("lblTotalCJ")).Text = String.Format("{0:0,0}", items.Sum(i => i.CJ ? 1 : 0));
            ((Label)lst.FindControl("lblTotalPCD")).Text = String.Format("{0:0,0}", items.Sum(i => i.PCD ? 1 : 0));
            ((Label)lst.FindControl("lblTotalOutros")).Text = String.Format("{0:0,0}", items.Sum(i => i.Outros));
            
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

            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            var items = new List<ConselhosMunicipaisExistentesInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetConselhosMunicipaisExistentes(filtro).ToList();
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

        public static DataTable ConvertTo(IList<ConselhosMunicipaisExistentesInfo> list)
        {

            DataTable dt = new DataTable();

            DataTable table = CreateTable();

            Type entityType = typeof(ConselhosMunicipaisExistentesInfo);

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (ConselhosMunicipaisExistentesInfo item in list)
            {
                DataRow row = table.NewRow();


                foreach (PropertyDescriptor prop in properties)
                {

                    if (prop.Name == "Municipio" || prop.Name == "Drads" || prop.Name == "CMAS" 
                     || prop.Name == "CMDCA" || prop.Name == "CT" || prop.Name == "CMI" 
                     || prop.Name == "PCD"   || prop.Name == "CONSEA" || prop.Name == "CJ" 
                     || prop.Name == "CME"   || prop.Name == "Outros")
                    {
                        switch (prop.Name)
                        {

                            case "Municipio":

                                row[prop.Name] = item.Municipio.ToString();
                                break;

                            case "Drads":

                                row[prop.Name] = item.Drads.ToString();
                                break;

                            case "CMAS":


                                if (item.CMAS)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não há registro";
                                }

                                break;

                            case "CMDCA":

                                if (item.CMDCA)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não há registro";
                                }
                                break;

                            case "CT":

                                if (item.CT > 0)
                                {
                                    row[prop.Name] = "Sim total de: " + item.CT.ToString();
                                }
                                else
                                {
                                    row[prop.Name] = "Não há registro";
                                }

                                break;

                            case "CMI":
                                
                                if (item.CMI)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não há registro";
                                }

                                break;

                            case "PCD":


                                if (item.PCD)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não há registro";
                                }

                                break;

                            case "CONSEA":

                                if (item.CONSEA)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não há registro";
                                }

                                break;

                            case "CJ":

                                if (item.CJ)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não há registro";
                                }

                                break;

                            case "CME":

                                if (item.CME)
                                {
                                    row[prop.Name] = "Sim";
                                }
                                else
                                {
                                    row[prop.Name] = "Não há registro";
                                }

                                row[prop.Name] = item.CME.ToString();
                                break;

                            case "Outros":

                                if (item.Outros > 0)
                                {
                                    row[prop.Name] = "Sim total de: " + item.Outros.ToString();
                                }
                                else
                                {
                                    row[prop.Name] = "Não há registro";
                                }

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
            Type entityType = typeof(ConselhosMunicipaisExistentesInfo);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.Name == "Municipio" || prop.Name == "Drads" || prop.Name == "CMAS"
                 || prop.Name == "CMDCA" || prop.Name == "CT" || prop.Name == "CMI"
                 || prop.Name == "PCD" || prop.Name == "CONSEA" || prop.Name == "CJ"
                 || prop.Name == "CME" || prop.Name == "Outros")
                {
                    if (prop.Name == "CMAS" || prop.Name == "CMDCA" || prop.Name == "CT" || prop.Name == "CMI" || prop.Name == "PCD"|| prop.Name == "CONSEA" 
                        || prop.Name == "CJ" || prop.Name == "CME" || prop.Name == "Outros")
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
            sl.DocumentProperties.Subject = "RelatorioDescritivo7";

            sl.SetCellValue("A1", "Municipio");
            sl.SetCellValue("B1", "Drads");
            sl.SetCellValue("C1", "CMAS");
            sl.SetCellValue("D1", "CMDCA");
            sl.SetCellValue("E1", "Conselho Tutelar");
            sl.SetCellValue("F1", "Conselho do Idoso");
            sl.SetCellValue("G1", "Conselho da Pessoa com Deficiência");
            sl.SetCellValue("H1", "Conselho da Segurança Alimentar e Nutricional");
            sl.SetCellValue("I1", "Conselho da Juventude");
            sl.SetCellValue("J1", "Conselho de Políticas sobre Drogas");
            sl.SetCellValue("K1", "Outros Conselhos");
            
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=RelatorioDescritivo7.xlsx");
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
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo7.xls");
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