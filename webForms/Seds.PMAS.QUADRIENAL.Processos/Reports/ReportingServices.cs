using System;
using System.Web.Configuration;

using Genericos;
using Seds.PMAS.QUADRIENAL.UI.Processos.wsRS;

namespace Seds.PMAS.QUADRIENAL.UI.Processos.Reports
{
    public class ReportingServices
    {
        private string _strBloco;
        private ParameterValue[] _prParams;

        public string pathBloco
        {
            get
            {
                return _strBloco;
            }
            set
            {
                _strBloco = value;
            }
        }

        public ParameterValue[] Parametros
        {
            get
            {
                return _prParams;
            }
            set
            {
                _prParams = value;
            }
        }

        public ReportingServices()
        {
            _strBloco = "";
            _prParams = null;
        }

         //<summary>
         //Acesso aos métodos do ReportingServices
         //</summary>
         //<returns>Objeto</returns>
        public static wsRS.ReportExecutionService objReport()
        {
            wsRS.ReportExecutionService objReport = new wsRS.ReportExecutionService();

            string strSenha = clsCrypto.Decrypt(WebConfigurationManager.AppSettings["RelUser"].ToString());
            string strPass = clsCrypto.Decrypt(WebConfigurationManager.AppSettings["RelPass"].ToString());

            objReport.Credentials = new System.Net.NetworkCredential(strSenha, strPass);

            objReport.Url = WebConfigurationManager.AppSettings["URLwsReporting"].ToString();
            return objReport;
        }

        public byte[] Gerar()
        {
            try
            {
                ReportExecutionService objRS = ReportingServices.objReport();

                // Render arguments
                byte[] result = null;
                string reportPath = this.pathBloco; //"/impressao/rptBlocoI";
                string format = "PDF";
                string historyID = null;
                string devInfo = @"<DeviceInfo><StreamRoot>/RSWebServiceXS/</StreamRoot></DeviceInfo>";

                DataSourceCredentials[] credentials = null;
                string showHideToggle = null;
                string encoding;
                string mimeType;
                string extension;
                Warning[] warnings = null;
                ParameterValue[] reportHistoryParameters = null;
                string[] streamIDs = null;

                ExecutionInfo execInfo = new ExecutionInfo();
                execInfo.NeedsProcessing = true;
                ExecutionHeader execHeader = new ExecutionHeader();
                objRS.ExecutionHeaderValue = execHeader;
                execInfo = objRS.LoadReport(reportPath, historyID);
                objRS.SetExecutionParameters(this.Parametros, "en-us");
                String SessionId = objRS.ExecutionHeaderValue.ExecutionID;

                result = objRS.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
             
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }

        }

        public byte[] GerarExcel()
        {
            string strSenha = WebConfigurationManager.AppSettings["RelUser"].ToString();
            string strPass = WebConfigurationManager.AppSettings["RelPass"].ToString();
            ReportExecutionService objRS = new ReportExecutionService();
            objRS.Credentials = new System.Net.NetworkCredential(strSenha, strPass);

            objRS.Url = WebConfigurationManager.AppSettings["URLwsReporting"].ToString();

            // Render arguments
            byte[] result = null;
            string reportPath = this.pathBloco;
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><StreamRoot>/RSWebServiceXS/</StreamRoot></DeviceInfo>";

            DataSourceCredentials[] credentials = null;
            string showHideToggle = null;
            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            ParameterValue[] reportHistoryParameters = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            objRS.SetExecutionParameters(this.Parametros, "pt-br");
            String SessionId = objRS.ExecutionHeaderValue.ExecutionID;
            result = objRS.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

            return result;
        }

    }
}
