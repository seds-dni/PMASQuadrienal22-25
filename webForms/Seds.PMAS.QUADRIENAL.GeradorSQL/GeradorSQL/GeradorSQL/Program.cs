using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace GeradorSQL
{
    class Program
    {
        static void Main(string[] args)
        {

           //GerarScriptDemografiaTerritorio();
           // // GerarScriptUPDATEDemografiaTerritorio();
           // GerarScriptDemografiaTerritorioDRADS();
           // //  GerarScriptUPDATEDemografiaTerritorioDRADS();


           // GerarScriptPopulacaoVulnerabilidade();
           // //GerarScriptUPDATEPopulacaoVulnerabilidade();
           // GerarScriptPopulacaoVulnerabilidadeDRADS();
           // //GerarScriptUPDATEPopulacaoVulnerabilidadeDRADS();
           // // GerarScriptPopulacaoVulnerabilidade();

           // GerarScriptRedeSocioAssistencial();
           // //   GerarScriptUpdateRedeSocioAssistencial();
           // GerarScriptRedeSocioAssistencialDRADS();
           // //GerarScriptUPDATERedeSocioAssistencialDRADS();
           // // GerarScriptUpdateRedeSocioAssistencial();

            GerarScriptIGD();
            //GerarScriptInsertPortal();

            //GerarScriptInsertMembrosFamilliaManobra();
            //GerarScriptAnaliseDiagnostica();

            // GerarScriptInsertProSocial();

            Console.ReadKey();
        }



        #region Gerador Script TERRITORIO DEMOGRAFIA

        static void GerarScriptDemografiaTerritorio()
        {
            Console.WriteLine("Gerador de script para [DBSEDS].[dbo].[TB_MUNICIPIO_DEMOGRAFIA_TERRITORIO_INDICADORES]\n");
            try
            {
                StringReader arquivoMunicipio = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\municipios.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\territoriodemografia.txt", UnicodeEncoding.Default));
                StringBuilder arquivoSQL = new StringBuilder("");
                string linhaMunicipio = null;
                string linhaAnalise = null;
                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaMunicipio = arquivoMunicipio.ReadLine();
                    //linhaAnalise2011 != null && linhaAnalise2012 != null
                    while (linhaAnalise != null && linhaMunicipio != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' }); // linhaAnalise.Replace("-", "0").Split(new char[] { '\t' });
                        for (int i = 0; i < 7; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaMunicipio.IndexOf(@"'") > -1)
                            linhaMunicipio = linhaMunicipio.Insert(linhaMunicipio.IndexOf("'"), "'");

                        Console.WriteLine("Gerando Script de atualização do Municipio " + linhaMunicipio);

                        arquivoSQL.Append("INSERT INTO [DBSEDS_HOMOLOGACAO].[dbo].[TB_MUNICIPIO_DEMOGRAFIA_TERRITORIO_INDICADORES] (ID_MUNICIPIO, AREA_TERRITORIAL_TOTAL_EM_KM_QUADRADO, NUMERO_HABITANTES, DENSIDADE_DEMOGRAFICA, TAXA_GEOMETRICA_CRESCIMENTO_ANUAL_POPULACAO, GRAU_URBANIZACAO,TOTAL_DOMICILIOS_PARTICULARES_PERMANENTES, NUMERO_PESSOAS_DOMICILIOS, VERSAO_SISTEMA) (")
                            .Append("select top 1 ID").Append(", ")
                            .Append("'").Append(Convert.ToDecimal(conteudoAnalise[0]).ToString().Replace(",", ".")).Append("', ")
                              .Append("").Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(", ")
                            .Append("'").Append(Convert.ToDecimal(conteudoAnalise[2]).ToString().Replace(",", ".")).Append("', ")
                            .Append("'").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".").Replace("%", "")).Append("', ")
                            .Append("'").Append(Convert.ToDecimal(conteudoAnalise[4]).ToString().Replace(",", ".").Replace("%", "")).Append("', ")
                             .Append("").Append(Convert.ToInt32(conteudoAnalise[5]).ToString()).Append(", ")
                            .Append("'").Append(Convert.ToDecimal(conteudoAnalise[6]).ToString().Replace(",", ".").Replace("%", "")).Append("', 2018 from [DBSEDS_HOMOLOGACAO].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("')")
                            .Append("\n");

                        Console.WriteLine("Linha de inserção do MUNICÍPIO " + linhaMunicipio.ToUpper() + " gerado com sucesso \r\n");

                        linhaMunicipio = arquivoMunicipio.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();

                        File.WriteAllText(@"..\..\arquivosgerados\inserirDBSEDS\SQLCARGA_TB_MUNICIPIO_DEMOGRAFIA_TERRITORIO_INDICADORES_QUADRIENAL.sql", arquivoSQL.ToString(), Encoding.Default);

                        Console.WriteLine("Finalizado.");
                    }
                    break;
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }

        static void GerarScriptUPDATEDemografiaTerritorio()
        {
            Console.WriteLine("Atualização de script para [DBSEDS].[dbo].[TB_MUNICIPIO_DEMOGRAFIA_TERRITORIO_INDICADORES]\n");
            try
            {
                StringReader arquivoMunicipio = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\municipios.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\territoriodemografia.txt", UnicodeEncoding.Default));
                StringBuilder arquivoSQL = new StringBuilder("");
                string linhaMunicipio = null;
                string linhaAnalise = null;
                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaMunicipio = arquivoMunicipio.ReadLine();
                    //linhaAnalise2011 != null && linhaAnalise2012 != null

                    while (linhaAnalise != null && linhaMunicipio != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' }); // linhaAnalise.Replace("-", "0").Split(new char[] { '\t' });
                        for (int i = 0; i < 11; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaMunicipio.IndexOf(@"'") > -1)
                            linhaMunicipio = linhaMunicipio.Insert(linhaMunicipio.IndexOf("'"), "'");

                        Console.WriteLine("Gerando Script de atualização do Municipio " + linhaMunicipio);

                        arquivoSQL.Append("UPDATE [DBSEDS].[dbo].[TB_MUNICIPIO_DEMOGRAFIA_TERRITORIO_INDICADORES] SET ")
                            .Append("[NUMERO_HABITANTES] = ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                            .Append("[AREA_TERRITORIAL_TOTAL_EM_KM_QUADRADO]= '").Append(Convert.ToDecimal(conteudoAnalise[1]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[DENSIDADE_DEMOGRAFICA] = '").Append(Convert.ToDecimal(conteudoAnalise[2]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TAXA_GEOMETRICA_CRESCIMENTO_ANUAL_POPULACAO] = '").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[SALDO_MIGRATORIO_ANUAL] = ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                            .Append("[TAXA_NATALIDADE]= '").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_PESSOAS_ABAIXO_15_ANOS_PERC] = '").Append(Convert.ToDecimal(conteudoAnalise[6]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_PESSOAS_ACIMA_60_ANOS_PERC] = '").Append(Convert.ToDecimal(conteudoAnalise[7]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_DOMICILIOS_PARTICULARES_PERMANENTES] = ").Append(Convert.ToInt32(conteudoAnalise[8]).ToString()).Append(", ")
                            .Append("[GRAU_URBANIZACAO] = '").Append(Convert.ToDecimal(conteudoAnalise[9]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_SANEAMENTO_ESGOTO_SANITARIO] = '").Append(Convert.ToDecimal(conteudoAnalise[10]).ToString().Replace(",", ".")).Append("' ")
                            .Append(" WHERE [ID_MUNICIPIO] = (select top 1 ID from [DBSEDS].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("') and [VERSAO_SISTEMA] =2017")
                            .Append("\n");
                        Console.WriteLine("Linha de inserção do MUNICÍPIO " + linhaMunicipio.ToUpper() + " gerado com sucesso \r\n");
                        linhaMunicipio = arquivoMunicipio.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();
                        File.WriteAllText(@"..\..\arquivosgerados\atualizarDBSEDS\ATUALIZAR_TB_MUNICIPIO_DEMOGRAFIA_TERRITORIO_INDICADORES.sql", arquivoSQL.ToString(), Encoding.Default);
                    }
                    break;
                }
                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }

        static void GerarScriptDemografiaTerritorioDRADS()
        {
            Console.WriteLine("Gerador de script para [DBSEDS].[dbo].[TB_MUNICIPIO_DEMOGRAFIA_TERRITORIO_INDICADORES]\n");
            try
            {
                StringReader arquivoDRADS = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\drads.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\territoriodemografiaDRADS.txt", UnicodeEncoding.Default));
                StringBuilder arquivoSQL = new StringBuilder("");
                string linhaDRADS = null;
                string linhaAnalise = null;
                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaDRADS = arquivoDRADS.ReadLine();
                    //linhaAnalise2011 != null && linhaAnalise2012 != null

                    while (linhaAnalise != null && linhaDRADS != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' }); // linhaAnalise.Replace("-", "0").Split(new char[] { '\t' });
                        for (int i = 0; i < 7; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaDRADS.IndexOf(@"'") > -1)
                            linhaDRADS = linhaDRADS.Insert(linhaDRADS.IndexOf("'"), "'");

                        Console.WriteLine("Gerando Script de atualização do Municipio " + linhaDRADS);

                        arquivoSQL.Append("INSERT INTO [DBSEDS_HOMOLOGACAO].[dbo].[TB_DRADS_DEMOGRAFIA_TERRITORIO_INDICADORES] (ID_DRADS, AREA_TERRITORIAL_TOTAL_EM_KM_QUADRADO_DRADS, NUMERO_HABITANTES_DRADS, DENSIDADE_DEMOGRAFICA_DRADS, TAXA_GEOMETRICA_CRESCIMENTO_ANUAL_POPULACAO_DRADS, GRAU_URBANIZACAO_DRADS,TOTAL_DOMICILIOS_PARTICULARES_PERMANENTES_DRADS, NUMERO_PESSOAS_DOMICILIOS_DRADS, VERSAO_SISTEMA_DRADS) (")
                            .Append("select top 1 ID").Append(", ")
                            .Append("'").Append(Convert.ToDecimal(conteudoAnalise[0]).ToString().Replace(".", "").Replace(",", ".")).Append("', ")
                             .Append("").Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(", ")
                            .Append("'").Append(Convert.ToDecimal(conteudoAnalise[2]).ToString().Replace(",", ".")).Append("', ")
                            .Append("'").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".").Replace("%", "")).Append("', ")
                           .Append("'").Append(Convert.ToDecimal(conteudoAnalise[4]).ToString().Replace(",", ".").Replace("%", "")).Append("', ")
                            .Append("").Append(Convert.ToInt32(conteudoAnalise[5]).ToString()).Append(", ")
                            .Append("'").Append(Convert.ToDecimal(conteudoAnalise[6]).ToString().Replace(",", ".").Replace("%", "")).Append("', 2018 from [DBSEDS_HOMOLOGACAO].[dbo].[TB_DRADS] where NOME like '").Append(linhaDRADS).Append("')")
                            .Append("\n");

                        Console.WriteLine("Linha de inserção dA DRADS " + linhaDRADS.ToUpper() + " gerado com sucesso \r\n");

                        linhaDRADS = arquivoDRADS.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();

                        File.WriteAllText(@"..\..\arquivosgerados\inserirDBSEDS\SQLCARGA_TB_DRADS_DEMOGRAFIA_TERRITORIO_INDICADORES_QUADRIENAL.sql", arquivoSQL.ToString(), Encoding.Default);

                        Console.WriteLine("Finalizado.");
                    }
                    break;
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }


        static void GerarScriptUPDATEDemografiaTerritorioDRADS()
        {
            Console.WriteLine("Atualização de script para [DBSEDS].[dbo].[TB_DRADS_DEMOGRAFIA_TERRITORIO_INDICADORES]\n");
            try
            {
                StringReader arquivoDRADS = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\drads.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\indicadoresDrads.txt", UnicodeEncoding.Default));
                StringBuilder arquivoSQL = new StringBuilder("");
                string linhaDRADS = null;
                string linhaAnalise = null;
                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaDRADS = arquivoDRADS.ReadLine();
                    //linhaAnalise2011 != null && linhaAnalise2012 != null

                    while (linhaAnalise != null && linhaDRADS != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' }); // linhaAnalise.Replace("-", "0").Split(new char[] { '\t' });
                        for (int i = 0; i < 11; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaDRADS.IndexOf(@"'") > -1)
                            linhaDRADS = linhaDRADS.Insert(linhaDRADS.IndexOf("'"), "'");

                        Console.WriteLine("Gerando Script de atualização da DRADS " + linhaDRADS);

                        arquivoSQL.Append("UPDATE [DBSEDS].[dbo].[TB_DRADS_DEMOGRAFIA_TERRITORIO_INDICADORES] SET ")
                            .Append("[NUMERO_HABITANTES_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                            .Append("[AREA_TERRITORIAL_TOTAL_EM_KM_QUADRADO_DRADS]= '").Append(Convert.ToDecimal(conteudoAnalise[1]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[DENSIDADE_DEMOGRAFICA_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[2]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TAXA_GEOMETRICA_CRESCIMENTO_ANUAL_POPULACAO_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[SALDO_MIGRATORIO_ANUAL_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                            .Append("[TAXA_NATALIDADE_DRADS]= '").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_PESSOAS_ABAIXO_15_ANOS_PERC_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[6]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_PESSOAS_ACIMA_60_ANOS_PERC_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[7]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_DOMICILIOS_PARTICULARES_PERMANENTES_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[8]).ToString()).Append(", ")
                            .Append("[GRAU_URBANIZACAO_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[9]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_SANEAMENTO_ESGOTO_SANITARIO_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[10]).ToString().Replace(",", ".")).Append("' ")
                            .Append(" WHERE [ID_DRADS] = (select top 1 ID from [DBSEDS].[dbo].[TB_DRADS] where NOME like '").Append(linhaDRADS).Append("') and [VERSAO_SISTEMA_DRADS] =2017")
                            .Append("\n");
                        Console.WriteLine("Linha de inserção da DRADS " + linhaDRADS.ToUpper() + " gerado com sucesso \r\n");
                        linhaDRADS = arquivoDRADS.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();
                        File.WriteAllText(@"..\..\arquivosgerados\atualizarDBSEDS\ATUALIZAR_TB_DRADS_DEMOGRAFIA_TERRITORIO_INDICADORES.sql", arquivoSQL.ToString(), Encoding.Default);
                    }
                    break;
                }
                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }

        #endregion


        #region Gerador Script População Vulnerabilidade

        static void GerarScriptPopulacaoVulnerabilidade()
        {
            Console.WriteLine("Gerador de script para [DBPMAS].[dbo].[TB_MUNICIPIO_POPULACAO_VULNERABILIDADE_INDICADORES] \n");
            try
            {
                StringReader arquivoMunicipio = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\municipios.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\dadospopulacaovulnerabilidade.txt", UnicodeEncoding.Default));
                StringBuilder arquivoSQL = new StringBuilder("");
                string linhaMunicipio = null;
                string linhaAnalise = null;
                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaMunicipio = arquivoMunicipio.ReadLine();
                    while (linhaAnalise != null && linhaMunicipio != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 6; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaMunicipio.IndexOf(@"'") > -1)
                            linhaMunicipio = linhaMunicipio.Insert(linhaMunicipio.IndexOf("'"), "'");

                        Console.WriteLine("Gerando Script de inserção do município " + linhaMunicipio);

                        arquivoSQL.Append("INSERT [DBSEDS_HOMOLOGACAO].[dbo].[TB_MUNICIPIO_POPULACAO_VULNERABILIDADE_INDICADORES] (ID_MUNICIPIO, TOTAL_PESSOAS_ABAIXO_15_ANOS_NUMERO, TOTAL_PESSOAS_ABAIXO_15_ANOS_PERC,TOTAL_PESSOAS_ACIMA_60_ANOS_NUMERO,TOTAL_PESSOAS_ACIMA_60_ANOS_PERC,INDICE_ENVELHECIMENTO, TOTAL_RAZAO_DEPENDENCIA_PERC, VERSAO_SISTEMA) ")
                           .Append("SELECT TOP 1 ID").Append(", ")
                            .Append("").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                              .Append("'").Append(Convert.ToDecimal(conteudoAnalise[1]).ToString().Replace(",", ".").Replace("%", "")).Append("', ")
                             .Append(Convert.ToInt32(conteudoAnalise[2]).ToString()).Append(", ")
                              .Append("'").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".").Replace("%", "")).Append("', ")
                              .Append("'").Append(Convert.ToDecimal(conteudoAnalise[4]).ToString().Replace(",", ".")).Append("', ")
                              .Append("'").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append("', 2018 ")
                            .Append("from [DBSEDS].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("'")
                            .Append("\n");


                        Console.WriteLine("Linha de inserção do MUNICÍPIO " + linhaMunicipio.ToUpper() + " gerado com sucesso \r\n");

                        linhaMunicipio = arquivoMunicipio.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();
                        File.WriteAllText(@"..\..\arquivosgerados\inserirDBSEDS\SQLCARGA_TB_MUNICIPIO_POPULACAO_VULNERABILIDADE_INDICADORES_2018.sql", arquivoSQL.ToString(), Encoding.Default);
                    }
                    break;
                }
                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }

        }

        static void GerarScriptUPDATEPopulacaoVulnerabilidade()
        {
            Console.WriteLine("Gerador de script para [DBPMAS].[dbo].[TB_MUNICIPIO_POPULACAO_VULNERABILIDADE_INDICADORES] \n");
            try
            {
                StringReader arquivoMunicipio = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\municipios.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\dadospopulacaovulnerabilidade.txt", UnicodeEncoding.Default));
                StringBuilder arquivoSQL = new StringBuilder("");
                string linhaMunicipio = null;
                string linhaAnalise = null;
                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaMunicipio = arquivoMunicipio.ReadLine();
                    while (linhaAnalise != null && linhaMunicipio != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 15; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaMunicipio.IndexOf(@"'") > -1)
                            linhaMunicipio = linhaMunicipio.Insert(linhaMunicipio.IndexOf("'"), "'");

                        Console.WriteLine("Gerando Script de inserção do município " + linhaMunicipio);

                        arquivoSQL.Append("UPDATE [DBSEDS].[dbo].[TB_MUNICIPIO_POPULACAO_VULNERABILIDADE_INDICADORES] SET ")
                            .Append("[TOTAL_DOMICILIOS_RPC_IGUAL_INFERIOR_UM_QUARTO_SM_NUMERO] = ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                            .Append("[TOTAL_DOMICILIOS_RPC_IGUAL_INFERIOR_UM_QUARTO_SM_PERC]= '").Append(Convert.ToDecimal(conteudoAnalise[1]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_DOMICILIO_RPC_IGUAL_INFERIOR_METADE_SALARIO_NUMERO] = ").Append(Convert.ToInt32(conteudoAnalise[2]).ToString()).Append(", ")
                            .Append("[TOTAL_DOMICILIO_RPC_IGUAL_INFERIOR_METADE_SALARIO_PERC] = '").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_EMPREGOS_FORMAIS_NUMERO] = ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                            .Append("[TOTAL_CRIANCAS_SEIS_QUATORZE_ANOS_FORA_ESCOLA_PERC]= '").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_PESSOAS_DEFICIENCIAS_PERC] = '").Append(Convert.ToDecimal(conteudoAnalise[6]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[TOTAL_RAZAO_DEPENDENCIA_PERC] = '").Append(Convert.ToDecimal(conteudoAnalise[7]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[IRPS_2010_GRUPOS] = '").Append(conteudoAnalise[8]).Append("', ")
                            .Append("[IRPS_2012_GRUPOS] = '").Append(conteudoAnalise[9]).Append("', ")
                            .Append("[INDICE_GINI_2000] = '").Append(Convert.ToDecimal(conteudoAnalise[10]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[INDICE_GINI_2010] = '").Append(Convert.ToDecimal(conteudoAnalise[11]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[IPVS_GRUPO_5_PERC] = '").Append(Convert.ToDecimal(conteudoAnalise[12]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[IPVS_GRUPO_6_PERC] = '").Append(Convert.ToDecimal(conteudoAnalise[13]).ToString().Replace(",", ".")).Append("', ")
                            .Append("[IPVS_GRUPO_7_PERC]= '").Append(Convert.ToDecimal(conteudoAnalise[14]).ToString().Replace(",", ".")).Append("' ")
                            .Append(" WHERE [ID_MUNICIPIO] = (select top 1 ID from [DBSEDS].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("') and [VERSAO_SISTEMA] =2017")
                            .Append("\n");
                        Console.WriteLine("Linha de inserção do MUNICÍPIO " + linhaMunicipio.ToUpper() + " gerado com sucesso \r\n");
                        linhaMunicipio = arquivoMunicipio.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();
                        File.WriteAllText(@"..\..\arquivosgerados\atualizarDBSEDS\ATUALIZAR_TB_MUNICIPIO_POPULACAO_VULNERABILIDADE_INDICADORES.sql", arquivoSQL.ToString(), Encoding.Default);

                    }
                    break;
                }
                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }

        }

        static void GerarScriptPopulacaoVulnerabilidadeDRADS()
        {
            Console.WriteLine("Gerador de script para [DBPMAS].[dbo].[TB_MUNICIPIO_POPULACAO_VULNERABILIDADE_INDICADORES] \n");
            try
            {
                StringReader arquivoDrads = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\DRADS.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\dadospopulacaovulnerabilidadedrads.txt", UnicodeEncoding.Default));
                StringBuilder arquivoSQL = new StringBuilder("");
                string linhaDRADS = null;
                string linhaAnalise = null;
                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaDRADS = arquivoDrads.ReadLine();
                    while (linhaAnalise != null && linhaDRADS != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 6; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaDRADS.IndexOf(@"'") > -1)
                            linhaDRADS = linhaDRADS.Insert(linhaDRADS.IndexOf("'"), "'");

                        Console.WriteLine("Gerando Script de inserção do DRADS " + linhaDRADS);

                        arquivoSQL.Append("INSERT [DBSEDS_HOMOLOGACAO].[dbo].[TB_DRADS_POPULACAO_VULNERABILIDADE_INDICADORES] (ID_DRADS, TOTAL_PESSOAS_ABAIXO_15_ANOS_NUMERO_DRADS, TOTAL_PESSOAS_ABAIXO_15_ANOS_PERC_DRADS,TOTAL_PESSOAS_ACIMA_60_ANOS_NUMERO_DRADS,TOTAL_PESSOAS_ACIMA_60_ANOS_PERC_DRADS,INDICE_ENVELHECIMENTO_DRADS, TOTAL_RAZAO_DEPENDENCIA_PERC_DRADS, VERSAO_SISTEMA_DRADS) ")
                           .Append("SELECT TOP 1 ID").Append(", ")
                            .Append("").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                              .Append("'").Append(Convert.ToDecimal(conteudoAnalise[1]).ToString().Replace(",", ".").Replace("%", "")).Append("', ")
                             .Append(Convert.ToInt32(conteudoAnalise[2]).ToString()).Append(", ")
                              .Append("'").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".").Replace("%", "")).Append("', ")
                              .Append("'").Append(Convert.ToDecimal(conteudoAnalise[4]).ToString().Replace(",", ".")).Append("', ")
                              .Append("'").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append("', 2018 ")
                            .Append("from [DBSEDS].[dbo].[TB_DRADS] where NOME like '").Append(linhaDRADS).Append("'")
                            .Append("\n");


                        Console.WriteLine("Linha de inserção dA DRADS " + linhaDRADS.ToUpper() + " gerado com sucesso \r\n");

                        linhaDRADS = arquivoDrads.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();
                        File.WriteAllText(@"..\..\arquivosgerados\inserirDBSEDS\SQLCARGA_TB_DRADS_POPULACAO_VULNERABILIDADE_INDICADORES_2018.sql", arquivoSQL.ToString(), Encoding.Default);
                    }
                    break;
                }
                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }

        }

        static void GerarScriptUPDATEPopulacaoVulnerabilidadeDRADS()
        {
            Console.WriteLine("Gerador de script para TB_MUNICIPIO_REDE_SOCIOASSISTENCIAL_INDICADORES\n");
            try
            {
                StringReader arquivoMunicipio = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\drads.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\dadospopulacaovulnerabilidadedrads.txt", UnicodeEncoding.Default));

                StringBuilder arquivoSQL = new StringBuilder("");

                string linhaDRADS = null;
                string linhaAnalise = null;

                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaDRADS = arquivoMunicipio.ReadLine();
                    //linhaAnalise2011 != null && linhaAnalise2012 != null

                    while (linhaAnalise != null && linhaDRADS != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Replace(" ", "0").Split(new char[] { '\t' });
                        //string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 13; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaDRADS.IndexOf(@"'") > -1)
                            linhaDRADS = linhaDRADS.Insert(linhaDRADS.IndexOf("'"), "'");
                        Console.WriteLine("Gerando Script de atualização do drads " + linhaDRADS);

                        arquivoSQL.Append("UPDATE [DBSEDS].[dbo].[TB_DRADS_POPULACAO_VULNERABILIDADE_INDICADORES] SET ")
                         .Append("[TOTAL_DOMICILIOS_RPC_IGUAL_INFERIOR_UM_QUARTO_SM_NUMERO_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                         .Append("[TOTAL_DOMICILIOS_RPC_IGUAL_INFERIOR_UM_QUARTO_SM_PERC_DRADS]= '").Append(Convert.ToDecimal(conteudoAnalise[1]).ToString().Replace(",", ".")).Append("', ")
                         .Append("[TOTAL_DOMICILIO_RPC_IGUAL_INFERIOR_METADE_SALARIO_NUMERO_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[2]).ToString()).Append(", ")
                         .Append("[TOTAL_DOMICILIO_RPC_IGUAL_INFERIOR_METADE_SALARIO_PERC_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".")).Append("', ")
                         .Append("[TOTAL_EMPREGOS_FORMAIS_NUMERO_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                         .Append("[TOTAL_CRIANCAS_SEIS_QUATORZE_ANOS_FORA_ESCOLA_PERC_DRADS]= '").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append("', ")
                         .Append("[TOTAL_PESSOAS_DEFICIENCIAS_PERC_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[6]).ToString().Replace(",", ".")).Append("', ")
                         .Append("[TOTAL_RAZAO_DEPENDENCIA_PERC_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[7]).ToString().Replace(",", ".")).Append("', ")
                            //.Append("[IRPS_2010_GRUPOS_DRADS] = '").Append(conteudoAnalise[8]).Append("', ")
                            //.Append("[IRPS_2012_GRUPOS_DRADS] = '").Append(conteudoAnalise[9]).Append("', ")
                         .Append("[INDICE_GINI_2000_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[8]).ToString().Replace(",", ".")).Append("', ")
                         .Append("[INDICE_GINI_2010_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[9]).ToString().Replace(",", ".")).Append("', ")
                         .Append("[IPVS_GRUPO_5_PERC_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[10]).ToString().Replace(",", ".")).Append("', ")
                         .Append("[IPVS_GRUPO_6_PERC_DRADS] = '").Append(Convert.ToDecimal(conteudoAnalise[11]).ToString().Replace(",", ".")).Append("', ")
                         .Append("[IPVS_GRUPO_7_PERC_DRADS]= '").Append(Convert.ToDecimal(conteudoAnalise[12]).ToString().Replace(",", ".")).Append("' ")
                         .Append(" WHERE [ID_DRADS] = (select top 1 ID from [DBSEDS].[dbo].[TB_DRADS] where NOME like '").Append(linhaDRADS).Append("') and [VERSAO_SISTEMA_DRADS] = 2017")
                         .Append("\n");
                        Console.WriteLine("Linha de inserção da DRADS " + linhaDRADS.ToUpper() + " gerado com sucesso \r\n");
                        linhaDRADS = arquivoMunicipio.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();
                        File.WriteAllText(@"..\..\arquivosgerados\atualizarDBSEDS\ATUALIZAR_TB_DRADS_POPULACAO_VULNERABILIDADE_INDICADORES.sql", arquivoSQL.ToString(), Encoding.Default);

                    }

                    break;
                }

                File.WriteAllText(@"..\..\ATUALIZAR_TB_MUNICIPIO_REDE_SOCIOASSISTENCIAL_INDICADORES.sql", arquivoSQL.ToString(), Encoding.Default);

                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }

        #endregion

        #region Gerador Script Evolução Rede Socioassistencial

        static void GerarScriptRedeSocioAssistencial()
        {

            Console.WriteLine("Gerador de script para TB_MUNICIPIO_REDE_SOCIOASSISTENCIAL_INDICADORES\n");

            try
            {

                StringReader arquivoMunicipio = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\municipios.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\evolucaoredesocioassistencial.txt", UnicodeEncoding.Default));

                StringBuilder arquivoSQL = new StringBuilder("");

                string linhaMunicipio = null;
                string linhaAnalise = null;

                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaMunicipio = arquivoMunicipio.ReadLine();
                    //linhaAnalise2011 != null && linhaAnalise2012 != null

                    while (linhaAnalise != null && linhaMunicipio != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Replace(" ", "0").Split(new char[] { '\t' });
                        //string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 27; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaMunicipio.IndexOf(@"'") > -1)
                            linhaMunicipio = linhaMunicipio.Insert(linhaMunicipio.IndexOf("'"), "'");
                        Console.WriteLine("Gerando Script de inserção do município " + linhaMunicipio);

                        arquivoSQL.Append(@"INSERT INTO [DBSEDS_HOMOLOGACAO].[dbo].[TB_MUNICIPIO_REDE_SOCIOASSISTENCIAL_INDICADORES]  (ID_MUNICIPIO, NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2013, NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2014, NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2015,  NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2013, NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2014, NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2015,
                            NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2013, NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2014, NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2015, NUMERO_SERVICOS_NAO_TIPIFICADOS_2013,   NUMERO_SERVICOS_NAO_TIPIFICADOS_2014, NUMERO_SERVICOS_NAO_TIPIFICADOS_2015, NUMERO_CRAS_IMPLANTADOS_MUNICIPIOS_2013, NUMERO_CRAS_IMPLANTADOS_MUNICIPIOS_2014, NUMERO_CRAS_IMPLANTADOS_MUNICIPIOS_2015,
                            NUMERO_CREAS_IMPLANTADOS_MUNICIPIOS_2013, NUMERO_CREAS_IMPLANTADOS_MUNICIPIOS_2014, NUMERO_CREAS_IMPLANTADOS_MUNICIPIOS_2015, NUMERO_CENTRO_POP_IMPLANTADOS_MUNICIPIOS_2013, NUMERO_CENTRO_POP_IMPLANTADOS_MUNICIPIOS_2014, NUMERO_CENTRO_POP_IMPLANTADOS_MUNICIPIOS_2015,
                            TOTAL_BENEFICIARIOS_BPC_IDOSO_2013, TOTAL_BENEFICIARIOS_BPC_IDOSO_2014,TOTAL_BENEFICIARIOS_BPC_IDOSO_2015, TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2013, TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2014, TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2015, VERSAO_SISTEMA) ")
                            .Append("SELECT TOP 1 ID, ")
                            .Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[2]).ToString()).Append(", ")
                             .Append(Convert.ToInt32(conteudoAnalise[3]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[5]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[6]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[7]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[8]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[9]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[10]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[11]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[12]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[13]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[14]).ToString()).Append(", ")
                            .Append(Convert.ToDecimal(conteudoAnalise[15]).ToString().Replace(",", ".")).Append(", ")
                            .Append(Convert.ToDecimal(conteudoAnalise[16]).ToString().Replace(",", ".")).Append(", ")
                            .Append(Convert.ToDecimal(conteudoAnalise[17]).ToString().Replace(",", ".")).Append(", ")
                            .Append(Convert.ToDecimal(conteudoAnalise[18]).ToString().Replace(",", ".")).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[19]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[20]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[21]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[22]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[23]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[24]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[25]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[26]).ToString()).Append(", ")
                            .Append("2018")
                            .Append(" From [DBSEDS_HOMOLOGACAO].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("'")
                            .Append("\n");

                        Console.WriteLine("Linha de inserção do MUNICÍPIO " + linhaMunicipio.ToUpper() + " gerado com sucesso \r\n");

                        linhaMunicipio = arquivoMunicipio.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();
                    }

                    break;
                }

                File.WriteAllText(@"..\..\arquivosgerados\inserirDBSEDS\SQLCARGA_TB_MUNICIPIO_REDE_SOCIOASSISTENCIAL_INDICADORES.sql", arquivoSQL.ToString(), Encoding.Default);

                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }

        static void GerarScriptUpdateRedeSocioAssistencial()
        {

            Console.WriteLine("Gerador de script para ATUALIZAÇÃO TB_MUNICIPIO_REDE_SOCIOASSISTENCIAL_INDICADORES\n");

            try
            {

                StringReader arquivoMunicipio = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\municipios.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\indicadoresRedeSocioAssistencial2.txt", UnicodeEncoding.Default));

                StringBuilder arquivoSQL = new StringBuilder("");

                string linhaMunicipio = null;
                string linhaAnalise = null;
                arquivoSQL.Append("UPDATE TB_MUNICIPIO_REDE_SOCIOASSISTENCIAL_INDICADORES SET TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2013 = TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2014, TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2014 = TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2015").Append("\n");
                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaMunicipio = arquivoMunicipio.ReadLine();
                    //linhaAnalise2011 != null && linhaAnalise2012 != null

                    while (linhaAnalise != null && linhaMunicipio != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Replace(" ", "0").Split(new char[] { '\t' });
                        //string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 2; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaMunicipio.IndexOf(@"'") > -1)
                            linhaMunicipio = linhaMunicipio.Insert(linhaMunicipio.IndexOf("'"), "'");
                        Console.WriteLine("Gerando Script de inserção do município " + linhaMunicipio);

                        arquivoSQL.Append("UPDATE [DBSEDS].[dbo].[TB_MUNICIPIO_REDE_SOCIOASSISTENCIAL_INDICADORES] SET ")
                            //.Append("[NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2013]= ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                            //    .Append("[NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2014]= ").Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(", ")
                            //   .Append("[NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2015]= ").Append(Convert.ToInt32(conteudoAnalise[2]).ToString()).Append(", ")
                            //   .Append("[NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2013]= ").Append(Convert.ToInt32(conteudoAnalise[3]).ToString()).Append(", ")
                            //   .Append("[NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2014]= ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                            //   .Append("[NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2015]= ").Append(Convert.ToInt32(conteudoAnalise[5]).ToString()).Append(", ")
                            //   .Append("[NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2013]= ").Append(Convert.ToInt32(conteudoAnalise[6]).ToString()).Append(", ")
                            //   .Append("[NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2014]= ").Append(Convert.ToInt32(conteudoAnalise[7]).ToString()).Append(", ")
                            //   .Append("[NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2015]= ").Append(Convert.ToInt32(conteudoAnalise[8]).ToString()).Append(", ")
                            //   .Append("[NUMERO_SERVICOS_NAO_TIPIFICADOS_2013]= ").Append(Convert.ToInt32(conteudoAnalise[9]).ToString()).Append(", ")
                            //   .Append("[NUMERO_SERVICOS_NAO_TIPIFICADOS_2014]= ").Append(Convert.ToInt32(conteudoAnalise[10]).ToString()).Append(", ")
                            //   .Append("[NUMERO_SERVICOS_NAO_TIPIFICADOS_2015]= ").Append(Convert.ToInt32(conteudoAnalise[11]).ToString()).Append(", ")
                            //   .Append("[NUMERO_ENTIDADES_SOCIOASSISTENCIAIS_CMAS_2013]= ").Append(Convert.ToInt32(conteudoAnalise[12]).ToString()).Append(", ")
                            //   .Append("[NUMERO_ENTIDADES_SOCIOASSISTENCIAIS_CMAS_2014]= ").Append(Convert.ToInt32(conteudoAnalise[13]).ToString()).Append(", ")
                            //   .Append("[NUMERO_ENTIDADES_SOCIOASSISTENCIAIS_CMAS_2015]= ").Append(Convert.ToInt32(conteudoAnalise[14]).ToString()).Append(", ")
                            //   .Append("[NUMERO_CRAS_IMPLANTADOS_MUNICIPIOS_2013]= ").Append(Convert.ToInt32(conteudoAnalise[15]).ToString()).Append(", ")
                            //   .Append("[NUMERO_CRAS_IMPLANTADOS_MUNICIPIOS_2014]= ").Append(Convert.ToInt32(conteudoAnalise[16]).ToString()).Append(", ")
                            //   .Append("[NUMERO_CRAS_IMPLANTADOS_MUNICIPIOS_2015]= ").Append(Convert.ToInt32(conteudoAnalise[17]).ToString()).Append(", ")
                            //   .Append("[NUMERO_CREAS_IMPLANTADOS_MUNICIPIOS_2013]= ").Append(Convert.ToInt32(conteudoAnalise[18]).ToString()).Append(", ")
                            //   .Append("[NUMERO_CREAS_IMPLANTADOS_MUNICIPIOS_2014]= ").Append(Convert.ToInt32(conteudoAnalise[19]).ToString()).Append(", ")
                            //   .Append("[NUMERO_CREAS_IMPLANTADOS_MUNICIPIOS_2015]= ").Append(Convert.ToInt32(conteudoAnalise[20]).ToString()).Append(", ")
                            //   .Append("[NUMERO_CENTRO_POP_IMPLANTADOS_MUNICIPIOS_2013]= ").Append(Convert.ToInt32(conteudoAnalise[21]).ToString()).Append(", ")
                            //   .Append("[NUMERO_CENTRO_POP_IMPLANTADOS_MUNICIPIOS_2014]= ").Append(Convert.ToInt32(conteudoAnalise[22]).ToString()).Append(", ")
                            //   .Append("[NUMERO_CENTRO_POP_IMPLANTADOS_MUNICIPIOS_2015]= ").Append(Convert.ToInt32(conteudoAnalise[23]).ToString()).Append(", ")
                            //   .Append("[MEDIA_MENSAL_ACOMPANHAMENTO_PAIF_2012]= ").Append(Convert.ToDecimal(conteudoAnalise[24]).ToString().Replace(",", ".")).Append(", ")
                            //   .Append("[MEDIA_MENSAL_ACOMPANHAMENTO_PAIF_2013]= ").Append(Convert.ToDecimal(conteudoAnalise[25]).ToString().Replace(",", ".")).Append(", ")
                            //   .Append("[MEDIA_MENSAL_ACOMPANHAMENTO_PAIF_2014]= ").Append(Convert.ToDecimal(conteudoAnalise[26]).ToString().Replace(",", ".")).Append(", ")
                            //   .Append("[MEDIA_MENSAL_ACOMPANHAMENTO_PAEFI_2012]= ").Append(Convert.ToDecimal(conteudoAnalise[27]).ToString().Replace(",", ".")).Append(", ")
                            //   .Append("[MEDIA_MENSAL_ACOMPANHAMENTO_PAEFI_2013]= ").Append(Convert.ToDecimal(conteudoAnalise[28]).ToString().Replace(",", ".")).Append(", ")
                            //   .Append("[MEDIA_MENSAL_ACOMPANHAMENTO_PAEFI_2014]= ").Append(Convert.ToDecimal(conteudoAnalise[29]).ToString().Replace(",", ".")).Append(", ")
                            //   .Append("[TOTAL_FAMILIAS_CADUNICO_2013]= ").Append(Convert.ToInt32(conteudoAnalise[30]).ToString()).Append(", ")
                            //   .Append("[TOTAL_FAMILIAS_CADUNICO_2014]= ").Append(Convert.ToInt32(conteudoAnalise[31]).ToString()).Append(", ")
                            //   .Append("[TOTAL_FAMILIAS_CADUNICO_2015]= ").Append(Convert.ToInt32(conteudoAnalise[32]).ToString()).Append(", ")
                            //   .Append("[TOTAL_BENEFICIARIOS_BPC_IDOSO_2013]= ").Append(Convert.ToInt32(conteudoAnalise[33]).ToString()).Append(", ")
                            //   .Append("[TOTAL_BENEFICIARIOS_BPC_IDOSO_2014]= ").Append(Convert.ToInt32(conteudoAnalise[34]).ToString()).Append(", ")
                               .Append("[TOTAL_BENEFICIARIOS_BPC_IDOSO_2015]= ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                            //.Append("[TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2013]= ").Append(Convert.ToInt32(conteudoAnalise[36]).ToString()).Append(", ")
                            //.Append("[TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2014]= ").Append(Convert.ToInt32(conteudoAnalise[37]).ToString()).Append(", ")
                               .Append("[TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2015]= ").Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(" ")
                            .Append(" WHERE [ID_MUNICIPIO] = (select top 1 ID from [DBSEDS].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("' ) AND VERSAO_SISTEMA = 2017")
                            .Append("\n");
                        Console.WriteLine("Linha de atualizacao do MUNICÍPIO " + linhaMunicipio.ToUpper() + " gerado com sucesso \r\n");
                        linhaMunicipio = arquivoMunicipio.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();
                    }

                    break;
                }

                File.WriteAllText(@"..\..\arquivosgerados\atualizarDBSEDS\ATUALIZAR_TB_REDE_SOCIOASSISTENCIAL_INDICADORES_2017.sql", arquivoSQL.ToString(), Encoding.Default);

                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }

        static void GerarScriptRedeSocioAssistencialDRADS()
        {


            Console.WriteLine("Gerador de script para TB_MUNICIPIO_REDE_SOCIOASSISTENCIAL_INDICADORES\n");

            try
            {

                StringReader arquivoDrads = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\drads.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\evolucaoredesocioassistencialDRADS.txt", UnicodeEncoding.Default));

                StringBuilder arquivoSQL = new StringBuilder("");

                string linhaDrads = null;
                string linhaAnalise = null;

                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaDrads = arquivoDrads.ReadLine();
                    //linhaAnalise2011 != null && linhaAnalise2012 != null

                    while (linhaAnalise != null && linhaDrads != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Replace(" ", "0").Split(new char[] { '\t' });
                        //string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 27; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaDrads.IndexOf(@"'") > -1)
                            linhaDrads = linhaDrads.Insert(linhaDrads.IndexOf("'"), "'");
                        Console.WriteLine("Gerando Script de inserção do município " + linhaDrads);

                        arquivoSQL.Append(@"INSERT INTO [DBSEDS_HOMOLOGACAO].[dbo].[TB_DRADS_REDE_SOCIOASSISTENCIAL_INDICADORES]  (ID_DRADS, NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2013_DRADS, NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2014_DRADS, NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2015_DRADS,  NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2013_DRADS, NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2014_DRADS, NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2015_DRADS,
                            NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2013_DRADS, NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2014_DRADS, NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2015_DRADS, NUMERO_SERVICOS_NAO_TIPIFICADOS_2013_DRADS,   NUMERO_SERVICOS_NAO_TIPIFICADOS_2014_DRADS, NUMERO_SERVICOS_NAO_TIPIFICADOS_2015_DRADS, NUMERO_CRAS_IMPLANTADOS_MUNICIPIOS_2013_DRADS, NUMERO_CRAS_IMPLANTADOS_MUNICIPIOS_2014_DRADS, NUMERO_CRAS_IMPLANTADOS_MUNICIPIOS_2015_DRADS,
                            NUMERO_CREAS_IMPLANTADOS_MUNICIPIOS_2013_DRADS, NUMERO_CREAS_IMPLANTADOS_MUNICIPIOS_2014_DRADS, NUMERO_CREAS_IMPLANTADOS_MUNICIPIOS_2015_DRADS, NUMERO_CENTRO_POP_IMPLANTADOS_MUNICIPIOS_2013_DRADS, NUMERO_CENTRO_POP_IMPLANTADOS_MUNICIPIOS_2014_DRADS, NUMERO_CENTRO_POP_IMPLANTADOS_MUNICIPIOS_2015_DRADS,
                            TOTAL_BENEFICIARIOS_BPC_IDOSO_2013_DRADS, TOTAL_BENEFICIARIOS_BPC_IDOSO_2014_DRADS,TOTAL_BENEFICIARIOS_BPC_IDOSO_2015_DRADS, TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2013_DRADS, TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2014_DRADS, TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2015_DRADS, VERSAO_SISTEMA_DRADS) ")
                            .Append("SELECT TOP 1 ID, ")
                            .Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[2]).ToString()).Append(", ")
                             .Append(Convert.ToInt32(conteudoAnalise[3]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[5]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[6]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[7]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[8]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[9]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[10]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[11]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[12]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[13]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[14]).ToString()).Append(", ")
                            .Append(Convert.ToDecimal(conteudoAnalise[15]).ToString().Replace(",", ".")).Append(", ")
                            .Append(Convert.ToDecimal(conteudoAnalise[16]).ToString().Replace(",", ".")).Append(", ")
                            .Append(Convert.ToDecimal(conteudoAnalise[17]).ToString().Replace(",", ".")).Append(", ")
                            .Append(Convert.ToDecimal(conteudoAnalise[18]).ToString().Replace(",", ".")).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[19]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[20]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[21]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[22]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[23]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[24]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[25]).ToString()).Append(", ")
                            .Append(Convert.ToInt32(conteudoAnalise[26]).ToString()).Append(", ")
                            .Append("2018")
                            .Append(" From [DBSEDS_HOMOLOGACAO].[dbo].[TB_DRADS] where NOME like '").Append(linhaDrads).Append("'")
                            .Append("\n");

                        Console.WriteLine("Linha de inserção da DRADS " + linhaDrads.ToUpper() + " gerado com sucesso \r\n");

                        linhaDrads = arquivoDrads.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();
                    }

                    break;
                }

                File.WriteAllText(@"..\..\arquivosgerados\inserirDBSEDS\SQLCARGA_TB_DRADS_REDE_SOCIOASSISTENCIAL_INDICADORES.sql", arquivoSQL.ToString(), Encoding.Default);

                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }

        //static void GerarScriptUpdateRedeSocioAssistencialDRADS()
        //{

        //    Console.WriteLine("Gerador de script para TB_DRADS_REDE_SOCIOASSISTENCIAL_INDICADORES\n");

        //    try
        //    {

        //        StringReader arquivoDRADS = new StringReader(File.ReadAllText(@"..\..\drads.txt", UnicodeEncoding.Default));
        //        StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\bpcDrads.txt", UnicodeEncoding.Default));

        //        StringBuilder arquivoSQL = new StringBuilder("");

        //        string linhaDRADS = null;
        //        string linhaAnalise = null;

        //        while (true)
        //        {
        //            linhaAnalise = arquivoAnalise.ReadLine();
        //            linhaDRADS = arquivoDRADS.ReadLine();
        //            //linhaAnalise2011 != null && linhaAnalise2012 != null

        //            while (linhaAnalise != null && linhaDRADS != null)
        //            {
        //                string[] conteudoAnalise = linhaAnalise.Replace(" ", "0").Split(new char[] { '\t' });
        //                //string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
        //                for (int i = 0; i < 2; i++)
        //                {
        //                    conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
        //                }
        //                if (linhaDRADS.IndexOf(@"'") > -1)
        //                    linhaDRADS = linhaDRADS.Insert(linhaDRADS.IndexOf("'"), "'");

        //                arquivoSQL.Append("UPDATE [DBSEDS].[dbo].[TB_DRADS_REDE_SOCIOASSISTENCIAL_INDICADORES] SET ")
        //                    .Append("[TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2015_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
        //                    .Append("[TOTAL_BENEFICIARIOS_BPC_IDOSO_2015_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(" ")
        //                    .Append(" WHERE [ID_DRADS] = (select top 1 ID from [DBSEDS].[dbo].[TB_DRADS] where NOME like '").Append(linhaDRADS).Append("' ) AND VERSAO_SISTEMA_DRADS = 2017")
        //                    .Append("\n");

        //                linhaDRADS = arquivoDRADS.ReadLine();
        //                linhaAnalise = arquivoAnalise.ReadLine();
        //            }

        //            break;
        //        }

        //        File.WriteAllText(@"..\..\UPDADE_TB_REDE_SOCIOASSISTENCIAL_INDICADORES_DRADS.sql", arquivoSQL.ToString(), Encoding.Default);

        //        Console.WriteLine("Finalizado.");
        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        Console.WriteLine("Erro: {0}", ex.Message);
        //    }
        //}

        static void GerarScriptUPDATERedeSocioAssistencialDRADS()
        {

            Console.WriteLine("Gerador de script para TB_DRADS_REDE_SOCIOASSISTENCIAL_INDICADORES\n");

            try
            {

                StringReader arquivoDRADS = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\DRADS.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\evolucaoredesocioassistencialDRADS.txt", UnicodeEncoding.Default));

                StringBuilder arquivoSQL = new StringBuilder("");

                string linhaDRADS = null;
                string linhaAnalise = null;

                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaDRADS = arquivoDRADS.ReadLine();
                    //linhaAnalise2011 != null && linhaAnalise2012 != null

                    while (linhaAnalise != null && linhaDRADS != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Replace(" ", "0").Split(new char[] { '\t' });
                        //string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 28; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaDRADS.IndexOf(@"'") > -1)
                            linhaDRADS = linhaDRADS.Insert(linhaDRADS.IndexOf("'"), "'");

                        arquivoSQL.Append("UPDATE [DBSEDS].[dbo].[TB_DRADS_REDE_SOCIOASSISTENCIAL_INDICADORES] SET ")
                            .Append("[NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2013] = ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2014] = ").Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_SOCIOASSISTENCIAIS_PSB_2015] = ").Append(Convert.ToInt32(conteudoAnalise[2]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2013] = ").Append(Convert.ToInt32(conteudoAnalise[3]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2014] = ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_PSE_MEDIA_COMPLEXIDADE_2015] = ").Append(Convert.ToInt32(conteudoAnalise[5]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2013] = ").Append(Convert.ToInt32(conteudoAnalise[6]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2014] = ").Append(Convert.ToInt32(conteudoAnalise[7]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_PSE_ALTA_COMPLEXIDADE_2015] = ").Append(Convert.ToInt32(conteudoAnalise[8]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_NAO_TIPIFICADOS_2013] = ").Append(Convert.ToInt32(conteudoAnalise[9]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_NAO_TIPIFICADOS_2014] = ").Append(Convert.ToInt32(conteudoAnalise[10]).ToString()).Append(", ")
                            .Append("[NUMERO_SERVICOS_NAO_TIPIFICADOS_2015] = ").Append(Convert.ToInt32(conteudoAnalise[11]).ToString()).Append(", ")
                            .Append("[NUMERO_ENTIDADES_SOCIOASSISTENCIAIS_CMAS_2013] =").Append(Convert.ToInt32(conteudoAnalise[12]).ToString()).Append(", ")
                            .Append("[NUMERO_ENTIDADES_SOCIOASSISTENCIAIS_CMAS_2014] =").Append(Convert.ToInt32(conteudoAnalise[13]).ToString()).Append(", ")
                            .Append("[NUMERO_ENTIDADES_SOCIOASSISTENCIAIS_CMAS_2015] =").Append(Convert.ToInt32(conteudoAnalise[14]).ToString()).Append(", ")
                            .Append("[MEDIA_MENSAL_ACOMPANHAMENTO_PAIF_2013] = ").Append(Convert.ToDecimal(conteudoAnalise[15]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[MEDIA_MENSAL_ACOMPANHAMENTO_PAIF_2014] = ").Append(Convert.ToDecimal(conteudoAnalise[16]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[MEDIA_MENSAL_ACOMPANHAMENTO_PAEFI_2013] = ").Append(Convert.ToDecimal(conteudoAnalise[17]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[MEDIA_MENSAL_ACOMPANHAMENTO_PAEFI_2014] = ").Append(Convert.ToDecimal(conteudoAnalise[18]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[TOTAL_FAMILIAS_CADUNICO_2013] = ").Append(Convert.ToInt32(conteudoAnalise[19]).ToString()).Append(", ")
                            .Append("[TOTAL_FAMILIAS_CADUNICO_2014] = ").Append(Convert.ToInt32(conteudoAnalise[20]).ToString()).Append(", ")
                            .Append("[TOTAL_FAMILIAS_CADUNICO_2015] = ").Append(Convert.ToInt32(conteudoAnalise[21]).ToString()).Append(", ")
                            .Append("[TOTAL_BENEFICIARIOS_BPC_IDOSO_2013] = ").Append(Convert.ToInt32(conteudoAnalise[22]).ToString()).Append(", ")
                            .Append("[TOTAL_BENEFICIARIOS_BPC_IDOSO_2014] = ").Append(Convert.ToInt32(conteudoAnalise[23]).ToString()).Append(", ")
                            .Append("[TOTAL_BENEFICIARIOS_BPC_IDOSO_2015] = ").Append(Convert.ToInt32(conteudoAnalise[24]).ToString()).Append(", ")
                            .Append("[TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2013] = ").Append(Convert.ToInt32(conteudoAnalise[25]).ToString()).Append(", ")
                            .Append("[TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2014] = ").Append(Convert.ToInt32(conteudoAnalise[26]).ToString()).Append(", ")
                            .Append("[TOTAL_BENEFICIARIOS_BPC_DEFICIENTES_2015] = ").Append(Convert.ToInt32(conteudoAnalise[27]).ToString()).Append(" ")
                            .Append(" WHERE [ID_DRADS] = (select top 1 ID from [DBSEDS].[dbo].[TB_DRADS] where NOME like ' ").Append(linhaDRADS).Append("')")
                            .Append("\n");

                        linhaDRADS = arquivoDRADS.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();
                    }

                    break;
                }

                File.WriteAllText(@"..\..\UPDADE_TB_DRADS_REDE_SOCIOASSISTENCIAL_INDICADORES.sql", arquivoSQL.ToString(), Encoding.Default);

                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }


        #endregion

        #region Gerador Script Família Paulista
        static void GerarScriptFamiliaPaulista()
        {
            Console.WriteLine("Gerador de script para [DBPMAS].[dbo].[TB_METAS_FAMILIA_PAULISTA] \n");
            try
            {
                StringReader arquivoMunicipio = new StringReader(File.ReadAllText(@"..\..\municipios.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\dadosfamiliapaulista.txt", UnicodeEncoding.Default));

                StringBuilder arquivoSQL = new StringBuilder("");

                string linhaMunicipio = null;
                string linhaAnalise = null;

                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaMunicipio = arquivoMunicipio.ReadLine();
                    while (linhaAnalise != null && linhaMunicipio != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 1; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaMunicipio.IndexOf(@"'") > -1)
                            linhaMunicipio = linhaMunicipio.Insert(linhaMunicipio.IndexOf("'"), "'");

                        arquivoSQL.Append("INSERT [DBPMAS2016].[dbo].[TB_META_FAMILIA_PAULISTA] SET ")
                            //.Append("[TOTAL_DOMICILIOS_RPC_IGUAL_INFERIOR_UM_QUARTO_SM_NUMERO_DRADS] =").AppendFormat(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                       .Append("[META_ATENDIMENTO] = ").Append(Convert.ToDecimal(conteudoAnalise[0]).ToString().Replace(",", ".")).Append("")
                            //.Append("[TOTAL_DOMICILIO_RPC_IGUAL_INFERIOR_METADE_SALARIO_NUMERO_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[2]).ToString().Replace(",", ".")).Append(", ")
                            //.Append("[TOTAL_DOMICILIO_RPC_IGUAL_INFERIOR_METADE_SALARIO_PERC_DRADS] = ").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".")).Append(", ")
                            //.Append("[TOTAL_EMPREGOS_FORMAIS_NUMERO_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                            //.Append("[TOTAL_CRIANCAS_SEIS_QUATORZE_ANOS_FORA_ESCOLA_PERC_DRADS] = ").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append(", ")
                            //.Append("[TOTAL_PESSOAS_DEFICIENCIAS_NUM_DRADS] = ").Append(Convert.ToDecimal(conteudoAnalise[6]).ToString().Replace(",", ".")).Append(", ")
                            //.Append("[TOTAL_RAZAO_DEPENDENCIA_PERC_DRADS] = ").Append(Convert.ToDecimal(conteudoAnalise[7]).ToString().Replace(",", ".")).Append(", ")
                       .Append(" WHERE [ID_MUNICIPIO] = (select top 1 ID from [DBSEDS].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("')")
                       .Append("\n");

                        linhaMunicipio = arquivoMunicipio.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();

                        File.WriteAllText(@"..\..\arquivoSQLCARGA_TB_META_FAMILIA_PAULISTA.sql", arquivoSQL.ToString(), Encoding.Default);

                    }
                    break;
                }
                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }

        }

        static void GerarScriptUpdadeFamiliaPaulista()
        {

            Console.WriteLine("Gerador de script para [DBPMAS].[dbo].[TB_METAS_FAMILIA_PAULISTA] \n");
            try
            {
                StringReader arquivoMunicipio = new StringReader(File.ReadAllText(@"..\..\municipios.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\dadosfamiliapaulista.txt", UnicodeEncoding.Default));

                StringBuilder arquivoSQL = new StringBuilder("");

                string linhaMunicipio = null;
                string linhaAnalise = null;

                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaMunicipio = arquivoMunicipio.ReadLine();
                    while (linhaAnalise != null && linhaMunicipio != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 1; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaMunicipio.IndexOf(@"'") > -1)
                            linhaMunicipio = linhaMunicipio.Insert(linhaMunicipio.IndexOf("'"), "'");

                        arquivoSQL.Append("INSERT [DBPMAS2016].[dbo].[TB_META_FAMILIA_PAULISTA] SET ")
                            //.Append("[TOTAL_DOMICILIOS_RPC_IGUAL_INFERIOR_UM_QUARTO_SM_NUMERO_DRADS] =").AppendFormat(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                       .Append("[META_ATENDIMENTO] = ").Append(Convert.ToDecimal(conteudoAnalise[0]).ToString().Replace(",", ".")).Append("")
                            //.Append("[TOTAL_DOMICILIO_RPC_IGUAL_INFERIOR_METADE_SALARIO_NUMERO_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[2]).ToString().Replace(",", ".")).Append(", ")
                            //.Append("[TOTAL_DOMICILIO_RPC_IGUAL_INFERIOR_METADE_SALARIO_PERC_DRADS] = ").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".")).Append(", ")
                            //.Append("[TOTAL_EMPREGOS_FORMAIS_NUMERO_DRADS] = ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                            //.Append("[TOTAL_CRIANCAS_SEIS_QUATORZE_ANOS_FORA_ESCOLA_PERC_DRADS] = ").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append(", ")
                            //.Append("[TOTAL_PESSOAS_DEFICIENCIAS_NUM_DRADS] = ").Append(Convert.ToDecimal(conteudoAnalise[6]).ToString().Replace(",", ".")).Append(", ")
                            //.Append("[TOTAL_RAZAO_DEPENDENCIA_PERC_DRADS] = ").Append(Convert.ToDecimal(conteudoAnalise[7]).ToString().Replace(",", ".")).Append(", ")
                       .Append(" WHERE [ID_MUNICIPIO] = (select top 1 ID from [DBSEDS].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("')")
                       .Append("\n");

                        linhaMunicipio = arquivoMunicipio.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();

                        File.WriteAllText(@"..\..\arquivoSQLCARGA_TB_META_FAMILIA_PAULISTA.sql", arquivoSQL.ToString(), Encoding.Default);

                    }
                    break;
                }
                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }

        }
        #endregion

        #region Gerador Script Analise Diagnóstica
        static void GerarScriptAnaliseDiagnostica()
        {
            Console.WriteLine("Gerador de script para indicadores do peti\n");

            try
            {

                StringReader arquivoMunicipio = new StringReader(File.ReadAllText(@"..\..\municipios.txt", UnicodeEncoding.Default));
                // StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\indicadoresmunicipio.txt", UnicodeEncoding.Default));
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\dadospopulacaovulnerabilidade.txt", UnicodeEncoding.Default));

                StringBuilder arquivoSQL = new StringBuilder("");

                string linhaMunicipio = null;
                string linhaAnalise = null;

                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    linhaMunicipio = arquivoMunicipio.ReadLine();
                    //linhaAnalise2011 != null && linhaAnalise2012 != null

                    while (linhaAnalise != null && linhaMunicipio != null)
                    {
                        //string[] conteudoAnalise = linhaAnalise.Replace("-", "0").Split(new char[] { '\t' });
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 15; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }
                        if (linhaMunicipio.IndexOf(@"'") > -1)
                            linhaMunicipio = linhaMunicipio.Insert(linhaMunicipio.IndexOf("'"), "'");

                        //arquivoSQL.Append("UPDATE [DBSEDS].[dbo].[TB_MUNICIPIO_DEMOGRAFIA_TERRITORIO_INDICADORES] SET ")
                        //    .Append("[NUMERO_HABITANTES] = ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                        //    .Append("[AREA_TERRITORIAL_TOTAL_EM_KM_QUADRADO] = ").Append(Convert.ToDecimal(conteudoAnalise[1]).ToString().Replace(",", ".")).Append(", ")
                        //    .Append("[DENSIDADE_DEMOGRAFICA] = ").Append(Convert.ToDecimal(conteudoAnalise[2]).ToString().Replace(",", ".")).Append(", ")
                        //    .Append("[TAXA_GEOMETRICA_CRESCIMENTO_ANUAL_POPULACAO] = ").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".")).Append(", ")
                        //    .Append("[SALDO_MIGRATORIO_ANUAL]= ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                        //    .Append("[TAXA_NATALIDADE] = ").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append(", ")
                        //    .Append("[TOTAL_PESSOAS_ABAIXO_15_ANOS_PERC] = ").Append(Convert.ToDecimal(conteudoAnalise[0]).ToString().Replace(",", ".")).Append(", ")
                        //    .Append("[TOTAL_PESSOAS_ACIMA_60_ANOS_PERC] = ").Append(Convert.ToDecimal(conteudoAnalise[1]).ToString().Replace(",", ".")).Append(", ")
                        //    .Append("[TOTAL_DOMICILIOS_PARTICULARES_PERMANENTES] = ").Append(Convert.ToInt32(conteudoAnalise[8]).ToString()).Append(", ")
                        //    .Append("[GRAU_URBANIZACAO] = ").Append(Convert.ToDecimal(conteudoAnalise[9]).ToString().Replace(",", ".")).Append(", ")
                        //    .Append("[TOTAL_SANEAMENTO_ESGOTO_SANITARIO] = ").Append(Convert.ToDecimal(conteudoAnalise[10]).ToString().Replace(",", ".")).Append(" ")
                        //    .Append(" WHERE [ID_MUNICIPIO] = (select top 1 ID from [DBSEDS].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("')")
                        //    .Append("\n");


                        arquivoSQL.Append("UPDATE [DBSEDS].[dbo].[TB_MUNICIPIO_POPULACAO_VULNERABILIDADE_INDICADORES] SET ")
                            .Append("[TOTAL_DOMICILIOS_RPC_IGUAL_INFERIOR_70_NUMERO] = ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                             .Append("[TOTAL_DOMICILIOS_RPC_IGUAL_INFERIOR_70_PERC] = ").Append(Convert.ToDecimal(conteudoAnalise[1]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[TOTAL_DOMICILIOS_RPC_IGUAL_INFERIOR_UM_QUARTO_SM_PERC] = ").Append(Convert.ToDecimal(conteudoAnalise[2]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[TOTAL_DOMICILIO_RPC_IGUAL_INFERIOR_METADE_SALARIO_PERC] = ").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[TOTAL_EMPREGOS_FORMAIS_NUMERO] = ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                            .Append("[TOTAL_CRIANCAS_SEIS_QUATORZE_ANOS_FORA_ESCOLA_PERC] = ").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[TOTAL_PESSOAS_DEFICIENCIAS_NUM] = ").Append(Convert.ToDecimal(conteudoAnalise[6]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[TOTAL_RAZAO_DEPENDENCIA_PERC] = ").Append(Convert.ToDecimal(conteudoAnalise[7]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[IRPS_2010_GRUPOS] = '").Append(Convert.ToString(conteudoAnalise[8]).ToString()).Append("', ")
                            .Append("[IRPS_2012_GRUPOS] = '").Append(Convert.ToString(conteudoAnalise[9]).ToString()).Append("', ")
                            .Append("[INDICE_GINI_2000] = ").Append(Convert.ToDecimal(conteudoAnalise[10]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[INDICE_GINI_2010] = ").Append(Convert.ToDecimal(conteudoAnalise[11]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[IPVS_GRUPO_5_PERC] = ").Append(Convert.ToDecimal(conteudoAnalise[12]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[IPVS_GRUPO_6_PERC] = ").Append(Convert.ToDecimal(conteudoAnalise[13]).ToString().Replace(",", ".")).Append(", ")
                            .Append("[IPVS_GRUPO_7_PERC] = ").Append(Convert.ToDecimal(conteudoAnalise[14]).ToString().Replace(",", ".")).Append(" ")
                            .Append(" WHERE [ID_MUNICIPIO] = (select top 1 ID from [DBSEDS].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("')")
                            .Append("\n");
                        //arquivoSQL.Append("UPDATE [DBPMAS2015].[dbo].[TB_PETI_INDICADORES] SET ")
                        // .Append("[IDADE_10_13_ANO_2010] = ").Append(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                        //  .Append("[IDADE_14_15_ANO_2010] = ").Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(", ")
                        // .Append("[IDADE_16_17_ANO_2010] = ").Append(Convert.ToInt32(conteudoAnalise[2]).ToString()).Append(", ")
                        // .Append("[IDADE_10_13_ANO_2011] = ").Append(Convert.ToInt32(conteudoAnalise[3]).ToString()).Append(", ")
                        //  .Append("[IDADE_14_15_ANO_2011] = ").Append(Convert.ToInt32(conteudoAnalise[4]).ToString()).Append(", ")
                        // .Append("[IDADE_16_17_ANO_2011] = ").Append(Convert.ToInt32(conteudoAnalise[5]).ToString()).Append(", ")
                        // .Append("[IDADE_10_13_ANO_2012] = ").Append(Convert.ToInt32(conteudoAnalise[6]).ToString()).Append(", ")
                        //  .Append("[IDADE_14_15_ANO_2012] = ").Append(Convert.ToInt32(conteudoAnalise[7]).ToString()).Append(", ")
                        // .Append("[IDADE_16_17_ANO_2012] = ").Append(Convert.ToInt32(conteudoAnalise[8]).ToString())
                        //  .Append(" WHERE [ID_MUNICIPIO] = (select top 1 ID from [DBSEDS].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("')")
                        // .Append("\n");
                        linhaMunicipio = arquivoMunicipio.ReadLine();
                        linhaAnalise = arquivoAnalise.ReadLine();


                        //+ arquivoSQL.Append(Convert.ToInt32(conteudoAnalise2010[1]).ToString()).Append(", ")
                        //+ arquivoSQL.Append(Convert.ToInt32(conteudoAnalise2010[2]).ToString()).Append(", ")
                        //+ arquivoSQL.Append(Convert.ToInt32(conteudoAnalise2010[3]).ToString()).Append(", ")
                        // + arquivoSQL.Append(Convert.ToInt32(conteudoAnalise2010[4]).ToString()).Append(")")

                        //arquivoSQL.Append("UPDATE [DBSEDS].[dbo].[TB_MUNICIPIO_INDICADORES] SET ")
                        //    //.Append("[AREA_TERRITORIAL_EM_KM_QUADRADO] = ").Append(Convert.ToDecimal(conteudoAnalise[0]).ToString().Replace(",", ".")).Append(", ")
                        //    //.Append("[POPULACAO_NUM_HAB_2010] = ").Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(", ")
                        //    //.Append("[TOTAL_FAMILIAS_2010] = ").Append(Convert.ToInt32(conteudoAnalise[2]).ToString()).Append(", ")
                        //    //.Append("[DENSIDADE_DEMOGRAFICA_2010] = ").Append(Convert.ToDecimal(conteudoAnalise[3]).ToString().Replace(",", ".")).Append(", ")
                        //    //.Append("[GRAU_URBANIZACAO_PERC_2010] = ").Append(Convert.ToDecimal(conteudoAnalise[4]).ToString().Replace(",", ".")).Append(", ")
                        //    //.Append("[TOTAL_DOMICILIOS_SANEAMENTO_ADEQUADO_PERC_2010] = ").Append(Convert.ToDecimal(conteudoAnalise[5]).ToString().Replace(",", ".")).Append(", ")
                        //    .Append("[TOTAL_PESSOAS_ABAIXO_15_ANOS_PERC_2010] = ").Append(Convert.ToDecimal(conteudoAnalise[0]).ToString().Replace(",", ".")).Append(", ")
                        //    .Append("[TOTAL_IDOSOS_PERC_2010] = ").Append(Convert.ToDecimal(conteudoAnalise[1]).ToString().Replace(",", ".")).Append(", ")
                        //    //.Append("[TOTAL_MAES_ADOLESCENTES_PERC_2009] = ").Append(Convert.ToDecimal(conteudoAnalise[8]).ToString().Replace(",", ".")).Append(", ")
                        //    //.Append("[RENDIMENTO_MENSAL_DOMICILIAR_PER_CAPITA_ATE_70_PERC_2010] = ").Append(Convert.ToDecimal(conteudoAnalise[9]).ToString().Replace(",", ".")).Append(", ")
                        //    //.Append("[IDHM_2000] = ").Append(conteudoAnalise[10].ToString()).Append(", ")
                        //    //.Append("[IDHM_2010] = ").Append(conteudoAnalise[11].ToString()).Append(", ")
                        //    //.Append("[INDICE_GINI_2000] = ").Append(conteudoAnalise[12].ToString()).Append(", ")
                        //    //.Append("[INDICE_GINI_2010] = ").Append(conteudoAnalise[13].ToString())
                        //    .Append(" WHERE [ID_MUNICIPIO] = (select top 1 ID from [DBSEDS].[dbo].[TB_MUNICIPIOS] where NOME like '").Append(linhaMunicipio).Append("')")
                        //    .Append("\n");

                    }

                    break;
                }

                File.WriteAllText(@"..\..\SQLCARGA_TB_MUNICIPIO_DEMOGRAFIA_TERRITORIO_INDICADORES.sql", arquivoSQL.ToString(), Encoding.Default);

                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }
        #endregion

        #region Gerador Script IGD
        static void GerarScriptIGD()
        {
            //Console.WriteLine("Gerador de script para Gestão Descentralizada IGD-PBF e IGD-SUAS\n");
            Console.WriteLine("Gerador de script para Gestão \n");
            try
            {
                StringReader arquivoIGD = new StringReader(File.ReadAllText(@"..\..\arquivosentrada\igd2018.txt", UnicodeEncoding.Default));

                StringBuilder arquivoSQL = new StringBuilder("");

                string linhaIGD = null;

                while (true)
                {
                    linhaIGD = arquivoIGD.ReadLine();
                    while (linhaIGD != null)
                    {
                        string[] conteudoIGD = linhaIGD.Split(new char[] { '\t' });
                        for (int i = 1; i <= 2; i++)
                        {
                            conteudoIGD[i] = conteudoIGD[i].Replace(",", ".");
                        }
                       

                        arquivoSQL.Append("UPDATE [DBPMAS_QUADRIENAL].[dbo].[TB_INDICE_GESTAO_DESCENTRALIZADA] SET ")
                            .Append("[IGD_SUAS] = ").Append(conteudoIGD[1]).Append(", ")
                            .Append("[IGD_PBF] = ").Append(conteudoIGD[2])
                            .Append(" WHERE [ID_PREFEITURA] = (select top 1 ID from [DBPMAS_QUADRIENAL].[dbo].[TB_PREFEITURA] where ID_MUNICIPIO = ").Append(conteudoIGD[0]).Append(")")
                            .Append("\n");

                        linhaIGD = arquivoIGD.ReadLine();
                    }

                    break;
                }

                File.WriteAllText(@"..\..\arquivosgerados\arquivoSQLIGD.sql", arquivoSQL.ToString(), Encoding.Default);

                Console.WriteLine("Finalizado.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }
        }
        #endregion

        #region Gerador Script Pró-Social
        static void GerarScriptInsertProSocial()
        {
            Console.WriteLine("Gerador de script para [DBPROSOCIAL].[dbo].[TB_FAMILIA_PROGRAMA]\n");
            try
            {
                StringReader arquivoAnalise = new StringReader(File.ReadAllText(@"..\..\prosocialcarga.txt", UnicodeEncoding.Default));
                StringBuilder arquivoSQL = new StringBuilder("");
                //string linhaMunicipio = null;
                string linhaAnalise = null;
                int counter = 0;
                while (true)
                {
                    linhaAnalise = arquivoAnalise.ReadLine();
                    //linhaMunicipio = arquivoMunicipio.ReadLine();
                    while (linhaAnalise != null)
                    {
                        string[] conteudoAnalise = linhaAnalise.Split(new char[] { '\t' });
                        for (int i = 0; i < 4; i++)
                        {
                            conteudoAnalise[i] = conteudoAnalise[i].Replace(".", "");
                        }

                        arquivoSQL.Append("INSERT [DBPROSOCIAL].[dbo].[TB_FAMILIA_PROGRAMA] values (")
                           .AppendFormat(Convert.ToInt32(conteudoAnalise[0]).ToString()).Append(", ")
                           .Append(Convert.ToInt32(conteudoAnalise[1]).ToString()).Append(", ")
                            .Append("").Append(Convert.ToInt32(conteudoAnalise[2]).ToString().Replace(",", ".")).Append(", ")
                            .Append("").Append(Convert.ToInt32(conteudoAnalise[3]).ToString().Replace(",", ".")).Append(")")
                            .Append("\n");

                        linhaAnalise = arquivoAnalise.ReadLine();
                        File.WriteAllText(@"..\..\arquivoSQLCARGA_TB_FAMILIA_PROGRAMA.sql", arquivoSQL.ToString(), Encoding.Default);
                        counter++;
                    }
                    break;
                }
                Console.WriteLine("Finalizado, gerados " + counter + "para inserção de dados.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }

        }
        #endregion


        #region Gerador de Script Carga Portal
        static void GerarScriptInsertPortal()
        {
            try
            {
                var connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["SedsContext"].ConnectionString;
                connection.Open();
                //RENDA CIDADA
                SqlCommand cmd = new SqlCommand(@"SELECT * from TB_ELEGIVEL_BENEFICIARIO where DT_BENEFICIO >= '2017-06-22'", connection);
                //ACAO JOVEM
                //SqlCommand cmd = new SqlCommand(@"SELECT * from TB_ELEGIVEL_BENEFICIARIO where cod_programa = 5", connection);

                var reader = cmd.ExecuteReader();
                StringBuilder arquivoSQL = new StringBuilder("");
                int counter = 0;
                while (reader.Read())
                {
                    Console.WriteLine("Gerando script de insert para o beneficiario " + (String)reader["NOM_PESSOA"]);
                    arquivoSQL.Append(@"INSERT [DBSEDS].[dbo].[TB_ELEGIVEL_BENEFICIARIO] ([COD_PESSOA_PORTAL], [COD_FAMILIA]  ,[NOM_PESSOA] ,[DT_BENEFICIO] ,[NOME_PROGRAMA] ,[NUM_IDENTIDADE_PESSOA] ,[NUM_CPF_PESSOA] ,[DTA_NASC_PESSOA], [NOM_COMPLETO_MAE_PESSOA] ,[DT_DESVINCULO] ,[DT_FIM_DESVINCULACAO] ,[NOM_TIP_LOGRADOURO_FAM], [NOM_TITULO_LOGRADOURO_FAM] ,[NOM_LOGRADOURO_FAM]  ,[DES_COMPLEMENTO_FAM] ,[DES_COMPLEMENTO_ADIC_FAM], [NUM_CEP_LOGRADOURO_FAM] ,[COD_IBGE_FAM] ,[COD_INSTITUICAO] ,[CNPJ], [COD_PROGRAMA], [COD_FAMILIAR_FAM] ,[NOM_LOCALIDADE_FAM] ,[NUM_LOGRADOURO_FAM], [OIN_ID_PROSOCIAL] ,[MANT_RAZAO_SOCIAL], [NUM_NIS_PESSOA_ATUAL] ,[FLAG_BENEFICIARIO], [cod_censo_inep_memb] ,[nom_escola_memb], [NUM_DDD_CONTATO_1_FAM] ,[NUM_TEL_CONTATO_1_FAM], [NUM_DDD_CONTATO_2_FAM], [NUM_TEL_CONTATO_2_FAM], [NUM_MEMBRO_FMLA] ,[COD_BENEFICIARIO_PORTAL]  ,[COD_CICLO],[COD_ELEGIVEL_MOTIVO_DESVINCULACAO],[USU_LG_USUARIO_RENDA_CIDADA]) 
                                       values (").Append((Int32)reader["COD_PESSOA_PORTAL"]).Append(", ")
                                                              .Append((Int32)reader["COD_FAMILIA"]).Append(", ")
                                                              .Append("'").Append((String)reader["NOM_PESSOA"]).Append("',")
                                                              .Append("CONVERT(datetime,'").Append(Convert.ToDateTime(reader["DT_BENEFICIO"])).Append("', 103), ")
                                                              .Append("'").Append((String)reader["NOME_PROGRAMA"]).Append("', ")
                                                              .Append("'").Append((String)reader["NUM_IDENTIDADE_PESSOA"]).Append("', ")
                                                              .Append("'").Append((String)reader["NUM_CPF_PESSOA"]).Append("', ")
                                                              .Append("CONVERT(datetime,'").Append((DateTime)reader["DTA_NASC_PESSOA"]).Append("' ,103), ")
                                                              .Append("'").Append((String)reader["NOM_COMPLETO_MAE_PESSOA"]).Append("', ");
                    if (!reader.IsDBNull(reader.GetOrdinal("DT_DESVINCULO")))
                    {
                        arquivoSQL.Append("CONVERT(datetime,'").Append((DateTime)reader["DT_DESVINCULO"]).Append("', 103), ");
                    }
                    else
                    {
                        arquivoSQL.Append("null").Append(", ");
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DT_FIM_DESVINCULACAO")))
                    {
                        arquivoSQL.Append("CONVERT(datetime,'").Append((DateTime)reader["DT_FIM_DESVINCULACAO"]).Append("' ,103), ");
                    }
                    else
                    {
                        arquivoSQL.Append("null").Append(", ");
                    }
                    arquivoSQL.Append("'").Append((String)reader["NOM_TIP_LOGRADOURO_FAM"]).Append("', ")
                    .Append("'").Append((String)reader["NOM_TITULO_LOGRADOURO_FAM"]).Append("', ")
                    .Append("'").Append((String)reader["NOM_LOGRADOURO_FAM"]).Append("', ")
                     .Append("'").Append((String)reader["DES_COMPLEMENTO_FAM"]).Append("', ")
                     .Append("'").Append((String)reader["DES_COMPLEMENTO_ADIC_FAM"]).Append("', ")
                     .Append("'").Append((String)reader["NUM_CEP_LOGRADOURO_FAM"]).Append("', ")
                     .Append((Int32)reader["COD_IBGE_FAM"]).Append(", ")
                     .Append((Int32)reader["COD_INSTITUICAO"]).Append(", ")
                     .Append((Int64)reader["CNPJ"]).Append(", ")
                     .Append((Int32)reader["COD_PROGRAMA"]).Append(", ")
                     .Append((Int64)reader["COD_FAMILIAR_FAM"]).Append(", ")
                     .Append("'").Append((String)reader["NOM_LOCALIDADE_FAM"]).Append("', ")
                     .Append("'").Append((String)reader["NUM_LOGRADOURO_FAM"]).Append("', ")
                     .Append((Int32)reader["OIN_ID_PROSOCIAL"]).Append(",")
                     .Append("'").Append((String)reader["MANT_RAZAO_SOCIAL"]).Append("', ")
                     .Append((Int64)reader["NUM_NIS_PESSOA_ATUAL"]).Append(", ")
                     .Append((Int32)reader["FLAG_BENEFICIARIO"]).Append(", ");
                    if (!reader.IsDBNull(reader.GetOrdinal("cod_censo_inep_memb")))
                    {
                        arquivoSQL.Append((Int32)reader["cod_censo_inep_memb"]).Append(", ");
                    }
                    else
                    {
                        arquivoSQL.Append("null").Append(", ");
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("nom_escola_memb")))
                    {
                        arquivoSQL.Append("'").Append((String)reader["nom_escola_memb"]).Append("', ");
                    }
                    else
                    {
                        arquivoSQL.Append("null").Append(", ");
                    }
                    arquivoSQL.Append("'").Append((String)reader["NUM_DDD_CONTATO_1_FAM"]).Append("', ")
                    .Append("'").Append((String)reader["NUM_TEL_CONTATO_1_FAM"]).Append("', ")
                    .Append("'").Append((String)reader["NUM_DDD_CONTATO_2_FAM"]).Append("', ")
                    .Append("'").Append((String)reader["NUM_TEL_CONTATO_2_FAM"]).Append("', ");
                    if (!reader.IsDBNull(reader.GetOrdinal("NUM_MEMBRO_FMLA")))
                    {
                        arquivoSQL.Append((Int64)reader["NUM_MEMBRO_FMLA"]).Append(", ");
                    }
                    else
                    {
                        arquivoSQL.Append("null").Append(", ");
                    }
                    arquivoSQL.Append((Int32)reader["COD_BENEFICIARIO_PORTAL"]).Append(", ")
                    .Append((Int32)reader["COD_CICLO"]).Append(", ");
                    if (!reader.IsDBNull(reader.GetOrdinal("COD_ELEGIVEL_MOTIVO_DESVINCULACAO")))
                    {
                        arquivoSQL.Append((Int32)reader["COD_ELEGIVEL_MOTIVO_DESVINCULACAO"]).Append(", ");
                    }
                    else
                    {
                        arquivoSQL.Append("null").Append(", ");
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("USU_LG_USUARIO_RENDA_CIDADA")))
                    {
                        arquivoSQL.Append((String)reader["USU_LG_USUARIO_RENDA_CIDADA"]).Append(", ");
                    }
                    else
                    {
                        arquivoSQL.Append("null").Append(")\n");
                    }

                    //                    File.WriteAllText(@"..\..\arquivoSQLCARGA_TB_ELEGIVEL_BENEFICIARIO_ACAO_JOVEM.sql", arquivoSQL.ToString(), Encoding.Default);
                    File.WriteAllText(@"..\..\arquivoSQLCARGA_TB_ELEGIVEL_BENEFICIARIO_RENDA_CIDADA.sql", arquivoSQL.ToString(), Encoding.Default);
                    counter++;
                }
                connection.Close();


                Console.WriteLine("Finalizado, foram gerados " + counter + " linhas para inserção de dados");


            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }

        }

        static void GerarScriptInsertMembrosFamilliaManobra()
        {
            try
            {
                var connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ElegivelContext"].ConnectionString;
                connection.Open();
                //                SqlCommand cmd = new SqlCommand(@"SELECT   [COD_PESSOA]
                //                                                          ,[COD_FAMILIA]
                //                                                          ,[COD_FAMILIAR_FAM]
                //                                                          ,[NOM_PESSOA]
                //                                                          ,[DTA_NASC_PESSOA]
                //                                                          ,[NOM_GRAU_PARENTESCO]
                //                                                      FROM [dbseds_teste].[dbo].[TB_ELEGIVEL_BENEFICIARIO_MEMBROS_FAMILIA_manobra] where cod_familia in (select COD_FAMILIA from [dbseds].[dbo].[TB_ELEGIVEL_BENEFICIARIO] where DT_BENEFICIO >= '2017-05-08')", connection);

                SqlCommand cmd = new SqlCommand(@"SELECT   [COD_PESSOA]
                                                          ,[COD_FAMILIA]
                                                          ,[COD_FAMILIAR_FAM]
                                                          ,[NOM_PESSOA]
                                                          ,[DTA_NASC_PESSOA]
                                                          ,[NOM_GRAU_PARENTESCO]
                                                      FROM [dbseds_teste].[dbo].[TB_ELEGIVEL_BENEFICIARIO_MEMBROS_FAMILIA_manobra]", connection);

                var reader = cmd.ExecuteReader();
                StringBuilder arquivoSQL = new StringBuilder("");
                int counter = 0;
                while (reader.Read())
                {
                    Console.WriteLine("Gerando script de insert para o membro " + (String)reader["NOM_PESSOA"]);
                    arquivoSQL.Append(@"INSERT [DBSEDS_TESTE].[dbo].[TB_ELEGIVEL_BENEFICIARIO_MEMBROS_FAMILIA] ([COD_PESSOA], [COD_FAMILIA]  ,[COD_FAMILIAR_FAM] ,[NOM_PESSOA] ,[DTA_NASC_PESSOA] ,[NOM_GRAU_PARENTESCO]) 
                                       values (").Append((Int32)reader["COD_PESSOA"]).Append(", ")
                                                              .Append((Int32)reader["COD_FAMILIA"]).Append(", ")
                                                               .Append((Int64)reader["COD_FAMILIAR_FAM"]).Append(", ")
                                                              .Append("'").Append((String)reader["NOM_PESSOA"]).Append("',")
                                                              .Append("CONVERT(datetime,'").Append(Convert.ToDateTime(reader["DTA_NASC_PESSOA"])).Append("', 103), ")
                                                              .Append("'").Append((String)reader["NOM_GRAU_PARENTESCO"]).Append("')").Append("\n");

                    File.WriteAllText(@"..\..\arquivoSQLCARGA_TB_ELEGIVEL_BENEFICIARIO_MEMBROS_FAMILIA_Teste.sql", arquivoSQL.ToString(), Encoding.Default);
                    counter++;
                }
                connection.Close();


                Console.WriteLine("Finalizado, foram gerados " + counter + " linhas para inserção de dados.");
                Console.ReadKey();


            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Erro: {0}", ex.Message);
            }

        }
        #endregion

    }
}
