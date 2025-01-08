using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Reflection;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Microsoft.Reporting.WebForms;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    rpt.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            //    rpt.LocalReport.LoadReportDefinition(Seds.PMAS.QUADRIENAL.Impressao.Reports.GetReport("Seds.PMAS.QUADRIENAL.Impressao.rptBlocoIV.rdlc"));
            //    var lst = new List<ConsultaPrefeituraAcaoPlanejamentoInfo>();
            //    var lstAcao = new List<PrefeituraAcaoPlanejamentoInfo>();
            //    using (var proxy = new ProxyAcoes())
            //    {
            //        lst = proxy.Service.GetConsultaPlanejamentoAcoesByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id).ToList();
            //        lstAcao = proxy.Service.GetPlanejamentoAcoesByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id).ToList();
            //    }

            //    rpt.LocalReport.DataSources.Clear();
            //    var dt = new Microsoft.Reporting.WebForms.ReportDataSource("dsAcoes", lst);
            //    rpt.LocalReport.DataSources.Add(dt);
            //    var dtAcao = new Microsoft.Reporting.WebForms.ReportDataSource("dsAcao", lstAcao);
            //    rpt.LocalReport.DataSources.Add(dtAcao);

            //    rpt.ZoomMode = ZoomMode.Percent;
            //    rpt.ZoomPercent = 100;

            //    foreach (RenderingExtension extension in rpt.LocalReport.ListRenderingExtensions())
            //    {
            //        if (extension.Name == "Excel" || extension.Name == "WORD")
            //        {
            //            var info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
            //            info.SetValue(extension, false);
            //        }
            //    }

            //}
        }

        
    }
}