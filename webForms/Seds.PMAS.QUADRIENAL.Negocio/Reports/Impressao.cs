using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Seds.PMAS.QUADRIENAL.Negocio.wsReport;

namespace Seds.PMAS.QUADRIENAL.Negocio.Reports
{
    public class Impressao
    {
        public byte[] GetBlocoI(Int32 IdPrefeitura)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[1];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();            

            wsRS.pathBloco = ConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoI";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoII(Int32 IdPrefeitura)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[1];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            wsRS.pathBloco = ConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoII";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoIII(Int32 IdPrefeitura)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[1];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            wsRS.pathBloco = ConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoIII";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoIV(Int32 IdPrefeitura)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[1];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            wsRS.pathBloco = ConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoIV";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoV(Int32 IdPrefeitura)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[1];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            wsRS.pathBloco = ConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoV";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoVI(Int32 IdPrefeitura)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[1];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            wsRS.pathBloco = ConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoVI";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoVII(Int32 IdPrefeitura)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[1];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            wsRS.pathBloco = ConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoVII";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }
    }
}
