using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Negocio.wsReport;
using Genericos;
using System.Configuration;

namespace Seds.PMAS.QUADRIENAL.Negocio.Reports
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

        /// <summary>
        /// Acesso aos métodos do ReportingServices
        /// </summary>
        /// <returns>Objeto</returns>
        public static wsReport.ReportingService objReport()
        {
            wsReport.ReportingService objReport = new wsReport.ReportingService();

            string strSenha = clsCrypto.Decrypt(ConfigurationManager.AppSettings["RelUser"].ToString());
            string strPass = clsCrypto.Decrypt(ConfigurationManager.AppSettings["RelPass"].ToString());

            objReport.Credentials = new System.Net.NetworkCredential(strSenha, strPass);

            objReport.Url = ConfigurationManager.AppSettings["URLwsReporting"].ToString();
            return objReport;
        }

        public byte[] Gerar()
        {
            ReportingService objRS = ReportingServices.objReport();

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
            Warning[] warnings = null;
            ParameterValue[] reportHistoryParameters = null;
            string[] streamIDs = null;


            result = objRS.Render(reportPath, format, historyID, devInfo, this.Parametros, credentials,
                showHideToggle, out encoding, out mimeType, out reportHistoryParameters, out warnings,
                out streamIDs);

            return result;
        }

        public byte[] GerarExcel()
        {
            string strSenha = ConfigurationManager.AppSettings["RelUser"].ToString();
            string strPass = ConfigurationManager.AppSettings["RelPass"].ToString();
            ReportingService objRS = new ReportingService();
            objRS.Credentials = new System.Net.NetworkCredential(strSenha, strPass);

            objRS.Url = ConfigurationManager.AppSettings["URLwsReporting"].ToString();

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
            Warning[] warnings = null;
            ParameterValue[] reportHistoryParameters = null;
            string[] streamIDs = null;

            result = objRS.Render(reportPath, format, historyID, devInfo, this.Parametros, credentials,
                showHideToggle, out encoding, out mimeType, out reportHistoryParameters, out warnings,
                out streamIDs);

            return result;
        }
    }
}
