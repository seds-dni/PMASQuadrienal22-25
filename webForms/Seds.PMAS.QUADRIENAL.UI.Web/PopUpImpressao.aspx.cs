using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class PopUpImpressao : System.Web.UI.Page
    {
        private static List<int> Exercicios = new List<int>() {2022, 2023, 2024, 2025};
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Convert.ToString(Request.QueryString["Bloco"]) == "RelatorioGestao2017")
            {
                try
                {
                    var lei = new Prefeituras(new ProxyPrefeitura()).GetLeiOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id,2017);
                    if (lei != null)
                    {
                        new ValidadorLeiOrcamentaria().Validar(lei);
                    }
                    else
                    {
                        throw new Exception();
                    }

                    var lst = new Prefeituras(new ProxyPrefeitura()).GetExecucaoFinanceira(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    if (lst == null)
                    {
                        throw new Exception();
                    }

                    
                }
                catch
                {
                    trImprimir.Visible = false;
                    trGestao.Visible = true;
                    trOrgaoGestor.Visible = false;
                }
            }
            
            var orgaoGestor = new Prefeituras(new ProxyPrefeitura()).GetOrgaoGestor(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (orgaoGestor == null)
            {
                trImprimir.Visible = false;
                trGestao.Visible = false;
                trOrgaoGestor.Visible = true;
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            byte[] strResult = null;
            String exercicio = Request.QueryString["Ex"];
            //using (var proxy = new ProxyPlanoMunicipal())
            //{                
                    switch (Request.QueryString["Bloco"])
                    {

                        case "1":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoI(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;

                        case "2":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoII(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;

                        case "2-2022":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoII(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;
                        case "2-2023":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoII(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[1].ToString());
                            break;
                        case "2-2024":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoII(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[2].ToString());
                            break;
                        case "2-2025":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoII(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[3].ToString());
                            break;

                        case "301":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoIII_EX1(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;
                        case "302":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoIII_EX2(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[1].ToString());
                            break;
                        case "303":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoIII(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;
                        case "304":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoIII(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[1].ToString());
                            break;
                        case "305":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoIII(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[2].ToString());
                            break;
                        case "306":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoIII(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[3].ToString());
                            break;
                        case "307":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoIII_EX2(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[2].ToString());
                            break;
                        case "308":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoIII_EX2(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[3].ToString());
                            break;
                        case "4":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoIV(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[1].ToString());
                            break;
                        case "501":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoV(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;
                        case "502":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoV(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[1].ToString());
                            break;
                        case "503":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoV(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[2].ToString());
                            break;
                        case "504":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoV(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[3].ToString());
                            break;

                        case "6-2022":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoVI(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;
                        case "6-2023":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoVI(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[1].ToString());
                            break;
                        case "6-2024":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoVI(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[2].ToString());
                            break;
                        case "6-2025":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoVI(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[3].ToString());
                            break;

                        case "7":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoVII(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                            break;
                        case "8":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoVIII(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                            break;
                        case "9":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoVPrestacaoDeConatas(SessaoPmas.UsuarioLogado.Prefeitura.Id,"2021");
                            break;
                        case "10":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoVPrestacaoDeConatas(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;
                        case "11":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoVPrestacaoDeConatas(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[1].ToString());
                            break;
                        case "12":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetBlocoVPrestacaoDeConatas(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[2].ToString());
                            break;
                        case "RelatorioGestao2017":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetRelatorioGestao2014(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;

                        case "Reprogramacao":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetRelatorioReprogramacao(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;
                        case "Reprogramacao2":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetRelatorioReprogramacao(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[1].ToString());
                            break;
                        case "Reprogramacao3":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetRelatorioReprogramacao(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[2].ToString());
                            break;
                        case "Reprogramacao4":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetRelatorioReprogramacao(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[3].ToString());
                            break;

                        case "Fluxo PMAS":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetFluxoPMAS(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                            break;

                        case "Rel-Gabinete-ex1":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetRelatorioGabinete(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[0].ToString());
                            break;

                        case "Rel-Gabinete-ex2":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetRelatorioGabinete(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[1].ToString());
                            break;

                        case "Rel-Gabinete-ex3":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetRelatorioGabinete(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[2].ToString());
                            break;

                        case "Rel-Gabinete-ex4":
                            strResult = new Seds.PMAS.QUADRIENAL.UI.Processos.Reports.Impressao().GetRelatorioGabinete(SessaoPmas.UsuarioLogado.Prefeitura.Id, PopUpImpressao.Exercicios[3].ToString());
                            break;

                    }                
            //}

            Response.ClearContent();
            Response.AppendHeader("content-length", strResult.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(strResult);
            Response.Flush();
            Response.Close();

            System.GC.Collect();
        }
    }
}