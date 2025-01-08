using System;
using System.Web.Configuration;
using Seds.PMAS.QUADRIENAL.UI.Processos.wsRS;

namespace Seds.PMAS.QUADRIENAL.UI.Processos.Reports
{
    public class Impressao
    {
        public byte[] GetBlocoI(Int32 IdPrefeitura, String exercicio)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoI";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoII(Int32 IdPrefeitura, String exercicio)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoII";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        

        public byte[] GetBlocoIII_EX1(Int32 IdPrefeitura, String exercicio)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoIII_I";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoIII_EX2(Int32 IdPrefeitura, String exercicio)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoIII_I";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoIII(Int32 IdPrefeitura, String exercicio)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoIII";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }



        public byte[] GetBlocoIV(Int32 IdPrefeitura, String exercicio)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoIV";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoV(Int32 IdPrefeitura, String exercicio)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoV";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoVI(Int32 IdPrefeitura, String exercicio)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoVI";
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

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoVII";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoVIII(Int32 IdPrefeitura)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[1];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptBlocoVIII";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetBlocoVPrestacaoDeConatas(Int32 IdPrefeitura, String exercicio) 
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();


            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptDemoFisicFinanc";
            wsRS.Parametros = parameters;
            return wsRS.Gerar(); 
        }

        public byte[] GetRelatorioGestao2014(Int32 IdPrefeitura, String exercicio)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            
            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptGestao2017";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetRelatorioReprogramacao(Int32 IdPrefeitura, String exercicio)
        {

            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptReprogramacaoValores";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }

        public byte[] GetRelatorioGabinete(Int32 IdPrefeitura, String exercicio)
        {

            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[2];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            parameters[1] = new ParameterValue();
            parameters[1].Name = "ID_EXERCICIO";
            parameters[1].Value = exercicio;

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptRelInformativo";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }


         
        public byte[] GetFluxoPMAS(Int32 IdPrefeitura)
        {
            ReportingServices wsRS = new ReportingServices();
            ParameterValue[] parameters = new ParameterValue[1];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "ID_PREFEITURA";
            parameters[0].Value = IdPrefeitura.ToString();

            wsRS.pathBloco = WebConfigurationManager.AppSettings["CaminhoRelatorio"].ToString() + "/rptFluxoPMAS";
            wsRS.Parametros = parameters;
            return wsRS.Gerar();
        }
    }
}
