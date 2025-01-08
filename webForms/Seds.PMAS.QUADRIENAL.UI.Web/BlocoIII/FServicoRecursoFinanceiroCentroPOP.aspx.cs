using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FServicoRecursoFinanceiroCentroPOP : System.Web.UI.Page
    {
        #region propriedades
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        #endregion

        #region sessao
        protected List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> FontesRecursosExercicio1
        {
            get { return Session["FONTES_RECURSOS_CentroPOP_Exercicio1"] as List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_CentroPOP_Exercicio1"] = value; }
        }

        protected List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> FontesRecursosExercicio2
        {
            get { return Session["FONTES_RECURSOS_CentroPOP_Exercicio2"] as List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_CentroPOP_Exercicio2"] = value; }
        }

        protected List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> FontesRecursosExercicio3
        {
            get { return Session["FONTES_RECURSOS_CentroPOP_Exercicio3"] as List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_CentroPOP_Exercicio3"] = value; }
        }

        protected List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> FontesRecursosExercicio4
        {
            get { return Session["FONTES_RECURSOS_CentroPOP_Exercicio4"] as List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_CentroPOP_Exercicio4"] = value; }
        }


        protected List<ProgramaProjetoCofinanciamentoInfo> ProgramaProjetoCofinanciamentoExercicio
        {
            get { return Session["PROGRAMA_PROJETO_CO_FINANCIAMENTOINFO_CentroPOP1"] as List<ProgramaProjetoCofinanciamentoInfo>; }
            set { Session["PROGRAMA_PROJETO_CO_FINANCIAMENTOINFO_CentroPOP1"] = value; }
        }
        protected List<PrefeituraBeneficioEventualServicoInfo> PrefeituraBeneficioEventualServicoExercicio
        {
            get { return Session["PREFEITURA_BENEFICIO_EVENTUALSERVICOINFO_CentroPOP1"] as List<PrefeituraBeneficioEventualServicoInfo>; }
            set { Session["PREFEITURA_BENEFICIO_EVENTUALSERVICOINFO_CentroPOP1"] = value; }
        }
        protected List<ServicoRecursoFinanceiroTransferenciaRendaInfo> TransferenciaRendaCofinanciamentoExercicio
        {
            get { return Session["TRANSFERENCIA_RENDA_CO_FINANCIAMENTOINFO_CentroPOP1"] as List<ServicoRecursoFinanceiroTransferenciaRendaInfo>; }
            set { Session["TRANSFERENCIA_RENDA_CO_FINANCIAMENTOINFO_CentroPOP1"] = value; }
        }
        protected List<ConsultaProgramaProjetoServicoCofinanciamentoInfo> SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio
        {
            get { return Session["CONSULTA_PROGRAMA_PROJETO_SERVICOCO_FINANCIAMENTOINFO"] as List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>; }
            set { Session["CONSULTA_PROGRAMA_PROJETO_SERVICOCO_FINANCIAMENTOINFO"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                if (String.IsNullOrEmpty(Request.QueryString["idCentro"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx");
                    return;
                }
                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "A")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço atualizado com sucesso!"), true);
                    }
                    else if (Request.QueryString["msg"] == "I")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço registrado com sucesso!<br/>"), true);
                    }
                }

                if (String.IsNullOrEmpty(hdnExercicio.Value))
                {
                    hdnExercicio.Value = "2022";
                }

                adicionarEventos();

                SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = null;
                using (var proxyEstrutura = new ProxyEstruturaAssistenciaSocial())
                {
                    carregarAvaliacoes(proxyEstrutura);
                    carregarCombos(proxyEstrutura, String.IsNullOrEmpty(Request.QueryString["id"]));
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            load(proxy, proxyEstrutura);
                        }
                    }
                    else
                    {
                        this.AplicarRegraBloqueioDesbloqueio();
                    }
                }

                ValidaBloqueioDesbloqueio();

                frame1_1.Attributes.Add("class", "frame active");
                frame1_2.Attributes.Remove("class");
                frame1_3.Attributes.Remove("class");
                frame1_4.Attributes.Remove("class");
                frame1_5.Attributes.Remove("class");
                ProgramaProjetoCofinanciamentoExercicio = null;
                TransferenciaRendaCofinanciamentoExercicio = null;
                SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = null;
                PrefeituraBeneficioEventualServicoExercicio = null;
                rptProgramaTemp.DataSource = null;
                rptProgramaTemp.DataBind();
            }
        }


        void adicionarEventos()
        {

            #region Recursos financeiros
            #region Exercicio 1
            txtFMASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            //txtValorEstadualizado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #region Exercicio 2
            txtFMASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            #endregion

            #region Exercicio 3
            txtFMASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFMASExercicio3.Attributes.Add("onclick", "this.select()");
            txtFNASExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEASExercicio3.Attributes.Add("onclick", "this.select()");
            txtFMDCAExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEDCAExercicio3.Attributes.Add("onclick", "this.select()");
            txtFNDCAExercicio3.Attributes.Add("onclick", "this.select()");
            txtValorContraExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEASAnoAnteriorExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEASDemandasExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEASReprogramacaoDemandasParlamentaresExercicio3.Attributes.Add("onclick", "this.select()");
            txtFMIExercicio3.Attributes.Add("onclick", "this.select()");
            txtFEIExercicio3.Attributes.Add("onclick", "this.select()");
            txtFNIExercicio3.Attributes.Add("onclick", "this.select()");
            txtValorRecursoExercicio3.Attributes.Add("onclick", "this.select()");
            #endregion

            #region Exercicio 4
            txtFMASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFMASExercicio4.Attributes.Add("onclick", "this.select()");
            txtFNASExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEASExercicio4.Attributes.Add("onclick", "this.select()");
            txtFMDCAExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEDCAExercicio4.Attributes.Add("onclick", "this.select()");
            txtFNDCAExercicio4.Attributes.Add("onclick", "this.select()");
            txtValorContraExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEASAnoAnteriorExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEASDemandasExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEASReprogramacaoDemandasParlamentaresExercicio4.Attributes.Add("onclick", "this.select()");
            txtFMIExercicio4.Attributes.Add("onclick", "this.select()");
            txtFEIExercicio4.Attributes.Add("onclick", "this.select()");
            txtFNIExercicio4.Attributes.Add("onclick", "this.select()");
            txtValorRecursoExercicio4.Attributes.Add("onclick", "this.select()");
            #endregion


            #region Exercicio 4
            txtFMASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtFNASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtFEASExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtFMDCAExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtFEDCAExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtFNDCAExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtFEASAnoAnteriorExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtFEASDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtFMIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtFEIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtFNIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");

            txtValorRecursoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio4.Attributes.Add("ondblclick", "return( currencyFormatDoubleClick( this, '.', ',', event, true ) );");
            #endregion

            #endregion


            txtNivelFundamental.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNivelMedio.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperior.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtEstagiarios.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorServicoSocial.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorPsicologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorPedagogia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSociologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtDireito.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorAntropologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorEconomia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorEconomiaDomestica.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorTerapiaOcupacional.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSuperiorMusicoTerapia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtSemEscolaridade.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

            txtSemEscolaridade.Attributes.Add("onblur", "CalculateTotal()");
            txtNivelFundamental.Attributes.Add("onblur", "CalculateTotal()");
            txtNivelMedio.Attributes.Add("onblur", "CalculateTotal()");
            txtSuperior.Attributes.Add("onblur", "CalculateTotal()");

        }


        [System.Web.Services.WebMethod]
        public static String CalcularValores(String[] valores)
        {
            Int32 total = 0;
            foreach (String val in valores)
            {
                string valor = !String.IsNullOrEmpty(val) ? val : "0";
                total += Convert.ToInt32(valor);
            }
            return total.ToString();
        }
        void load(ProxyRedeProtecaoSocial proxy, ProxyEstruturaAssistenciaSocial proxyEstrutura)
        {
            int idServico = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            var entidade = proxy.Service.GetServicoRecursoFinanceiroCentroPOPById(idServico);
            if (entidade == null)
            {
                return;
            }

            this.CarregarCaracterizacaoServico(proxyEstrutura, entidade);
            this.CarregarSituacoes(proxyEstrutura);
            this.CarregarUsuarios(entidade, proxyEstrutura);
            this.CarregarRecursosHumanos(proxy, entidade);
            this.CarregarFuncionamento(proxyEstrutura, entidade);
            this.CarregarRecursosFinanceiros(entidade);
            this.CarregaDemandasParlamentares(entidade);

            if (entidade.Id != 0)
            {
                trAssociacaoProgramas.Visible = true;
            }

            using (var proxyprogramas = new ProxyProgramas())
            {
                carregarComboProgramas(proxyprogramas);
                CarregarProgramas(proxyprogramas, entidade.Id, entidade.IdCentroPOP);
            }

        }

        private void ValidaBloqueioDesbloqueio()
        {
            WebControl[] controles1 = SelecionarControlesRecursosFinanceirosBloqueioExercicio1();
            WebControl[] controles2 = SelecionarControlesRecursosFinanceirosBloqueioExercicio2();
            WebControl[] controles3 = SelecionarControlesRecursosFinanceirosBloqueioExercicio3();
            WebControl[] controles4 = SelecionarControlesRecursosFinanceirosBloqueioExercicio4();

            var validaBloqueio2022 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroCentroPOP.Exercicios[0]);
            var validaBloqueio2023 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroCentroPOP.Exercicios[1]);
            var validaBloqueio2024 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroCentroPOP.Exercicios[2]);
            var validaBloqueio2025 = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroCentroPOP.Exercicios[3]);

            if (validaBloqueio2022 == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio1()", "exibeBtnExcluirExercicio1();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio1()", "ocultaBtnExcluirExercicio1();", true);
            }

            if (validaBloqueio2023 == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio2()", "exibeBtnExcluirExercicio2();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio2()", "ocultaBtnExcluirExercicio2();", true);
            }

            if (validaBloqueio2024 == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio3()", "exibeBtnExcluirExercicio3();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio3()", "ocultaBtnExcluirExercicio3();", true);
            }

            if (validaBloqueio2025 == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio4()", "exibeBtnExcluirExercicio4();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio4()", "ocultaBtnExcluirExercicio4();", true);
            }

        }

        private bool RetornaValidacaoBloqueioDesbloqueio(int exercicio)
        {
            WebControl[] controles1 = SelecionarControlesRecursosFinanceirosBloqueioExercicio1();
            WebControl[] controles2 = SelecionarControlesRecursosFinanceirosBloqueioExercicio2();
            WebControl[] controles3 = SelecionarControlesRecursosFinanceirosBloqueioExercicio3();
            WebControl[] controles4 = SelecionarControlesRecursosFinanceirosBloqueioExercicio4();

            bool validacao = false;

            if (exercicio == 2022)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroCentroPOP.Exercicios[0]);
            }

            if (exercicio == 2023)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroCentroPOP.Exercicios[1]);
            }

            if (exercicio == 2024)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroCentroPOP.Exercicios[2]);
            }

            if (exercicio == 2025)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroCentroPOP.Exercicios[3]);
            }

            return validacao;

        }

        void CarregarCaracterizacaoServico(ProxyEstruturaAssistenciaSocial proxyEstrutura, ServicoRecursoFinanceiroCentroPOPInfo servico)
        {
            if (new List<Int32>() { 154, 155, 156 }.Contains(servico.UsuarioTipoServico.IdTipoServico))
                servico.UsuarioTipoServico.IdTipoServico = 138;

            if (new List<Int32>() { 157, 158, 159 }.Contains(servico.UsuarioTipoServico.IdTipoServico))
                servico.UsuarioTipoServico.IdTipoServico = 145;

            rblTipoProtecao.SelectedValue = servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial.ToString();
            carregarTiposServicos(proxyEstrutura, servico.UsuarioTipoServico.IdTipoServico != 144);
            ddlTipoServico.SelectedValue = servico.UsuarioTipoServico.IdTipoServico.ToString();
            carregarTiposServicosNaoTipificados(proxyEstrutura);
            ddlTipoServicoNaoTipificado.SelectedValue = servico.IdTipoServicoNaoTipificado.HasValue ? servico.IdTipoServicoNaoTipificado.Value.ToString() : "0";

            if (servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial == 2)
            {
                CarregarOfertaServico(true);

                rblCaracteristicaOferta.SelectedValue = servico.IdCaracteristicaOfertaServico.ToString();
            }
            else
            {
                CarregarOfertaServico(false);
            }

            //SERVIÇO NÃO TIPIFICADO
            if (servico.UsuarioTipoServico.IdTipoServico == 138 || servico.UsuarioTipoServico.IdTipoServico == 145)
            {
                tbNaoTipificado.Visible = true;
                tbNaoTipificadoObjetivo.Visible = tbNaoTipificadoDetalhado.Visible = ddlTipoServicoNaoTipificado.SelectedItem.Text == "Outro";
            }

            if (servico.UsuarioTipoServico.IdTipoServico == 153)
                tbNaoTipificadoObjetivo.Visible = tbNaoTipificadoDetalhado.Visible = true;

            txtNaotipificado.Text = !String.IsNullOrWhiteSpace(servico.DescricaoServicoNaoTipificado) ? servico.DescricaoServicoNaoTipificado : String.Empty;
            txtObjetivoNaoTipificado.Text = !String.IsNullOrWhiteSpace(servico.ObjetivoServicoNaoTipificado) ? servico.ObjetivoServicoNaoTipificado : String.Empty;

            if (servico.IdTipoServicoNaoTipificado.HasValue)
                carregarUsuarios(proxyEstrutura, true);
            else
                carregarUsuarios(proxyEstrutura);

            ddlPublicoAlvo.SelectedValue = servico.IdUsuarioTipoServico.ToString();
            if (servico.UsuarioTipoServico.IdTipoServico == 144)
            {
                ddlTipoServico.Enabled = rblTipoProtecao.Enabled = false;
            }

            CarregarSituacoes(proxyEstrutura);


            if (servico.IdTipoServicoNaoTipificado.HasValue)
                carregarAtividades(proxyEstrutura, true);
            else
                carregarAtividades(proxyEstrutura);

            if (servico.AtividadesSocioAssistenciais != null && servico.AtividadesSocioAssistenciais.Count > 0)
            {
                foreach (ListItem i in lstAtividades.Items)
                    i.Selected = servico.AtividadesSocioAssistenciais.Any(s => s.Id == Convert.ToInt32(i.Value));
            }

            chkNaoPossuiTecnicoResponsavel.Checked = servico.PossuiTecnicoResponsavel.HasValue ? !servico.PossuiTecnicoResponsavel.Value : false;
            txtTecnicoResponsavel.Text = chkNaoPossuiTecnicoResponsavel.Checked ? "" : !String.IsNullOrWhiteSpace(servico.NomeTecnicoResponsavel) ? servico.NomeTecnicoResponsavel : String.Empty;
            txtTecnicoResponsavel.Enabled = !chkNaoPossuiTecnicoResponsavel.Checked;

            ddlPublicoAlvo_SelectedIndexChanged(null, null);

            ddlAbrangencia.SelectedValue = servico.IdAbrangenciaServico.ToString();

            if (servico.IdAbrangenciaServico == 4)
            {
                trSedeServico.Visible = true;
                trFormaJuridica.Visible = true;

                if (servico.MunicipioSedeServico != null)
                {
                    rblAbrangencia.SelectedValue = servico.MunicipioSedeServico == true ? "1" : "0";

                    if (servico.MunicipioSedeServico == true)
                    {
                        trMunicipioParticipaOferta.Visible = true;
                        trMunicipioSede.Visible = false;
                        txtMunicipioParticipaOferta.Text = servico.IndicaMunicipiosParticipamOfertaServico;
                    }
                    else
                    {
                        trMunicipioParticipaOferta.Visible = false;
                        trMunicipioSede.Visible = true;
                        txtMunicipioSede.Text = servico.IndicaMunicipiosSedeServico;
                    }
                }

                if (servico.IdFormaJuridica != null)
                {
                    ddlFormaJuridica.SelectedValue = servico.IdFormaJuridica.ToString();

                    if (servico.IdFormaJuridica == 1)
                    {
                        trConsorcioPublico.Visible = true;

                        var consorcio = proxyEstrutura.Service.GetConsorcioCentroPOP(servico.Id);

                        if (consorcio != null)
                        {
                            txtNomeConsorcio.Text = consorcio.NomeConsorcio;
                            txtCNPJConsorcio.Text = consorcio.CNPJ;
                            txtMunicipioSedeConsorcio.Text = consorcio.MunicipioSede;
                            txtMunicipiosEnvolvidos.Text = consorcio.MunicipioEnvolvido;
                        }

                    }
                    else
                    {
                        trConsorcioPublico.Visible = false;
                    }
                }
            }
            else
            {
                trSedeServico.Visible = false;
                trFormaJuridica.Visible = false;
            }

            AposSalvoNaoPermitirEdicaoCamposCaracterizacao(servico);
        }

        private void CarregarOfertaServico(bool p)
        {
            trCaracteristicaOferta.Visible = p;
        }

        void CarregarUsuarios(ServicoRecursoFinanceiroCentroPOPInfo obj, ProxyEstruturaAssistenciaSocial proxyEstrutura)
        {
            rblCaracteristicasTerritorio.SelectedValue = obj.IdCaracteristicasTerritorio.ToString();
            rblMoradiaUsuarios.SelectedValue = obj.IdRegiaoMoradia.HasValue ? obj.IdRegiaoMoradia.Value.ToString() : String.Empty;
            rblSexo.SelectedValue = obj.IdSexo.HasValue ? obj.IdSexo.Value.ToString() : String.Empty;
            if (obj.SituacoesEspecificas != null && obj.SituacoesEspecificas.Count > 0)
            {
                foreach (ListItem i in lstSituacoesEspecificas.Items)
                    i.Selected = obj.SituacoesEspecificas.Any(s => s.Id == Convert.ToInt32(i.Value));
            }
        }
        void CarregarRecursosHumanos(ProxyRedeProtecaoSocial proxy, ServicoRecursoFinanceiroCentroPOPInfo obj)
        {
            var recursoshumanos = proxy.Service.GetRecursosHumanosCentroPOPByIdServicoRecursoFinanceiro(obj.Id);
            if (recursoshumanos != null)
            {
                hdfIdRecursosHumanos.Value = recursoshumanos.Id.ToString();

                txtSemEscolaridade.Text = recursoshumanos.SemEscolarizacao.ToString();
                txtNivelFundamental.Text = recursoshumanos.NivelFundamental.ToString();
                txtNivelMedio.Text = recursoshumanos.NivelMedio.ToString();
                txtSuperior.Text = recursoshumanos.NivelSuperior.ToString();

                txtSuperiorServicoSocial.Text = recursoshumanos.ServicoSocial.ToString();
                txtSuperiorPsicologia.Text = recursoshumanos.Psicologia.ToString();
                txtSuperiorPedagogia.Text = recursoshumanos.Pedagogia.ToString();
                txtSuperiorAntropologia.Text = recursoshumanos.Antropologia.ToString();
                txtSuperiorMusicoTerapia.Text = recursoshumanos.Musicoterapia.ToString();
                txtSuperiorTerapiaOcupacional.Text = recursoshumanos.TerapiaOcupacional.ToString();
                txtSuperiorEconomia.Text = recursoshumanos.Economia.ToString();
                txtSuperiorEconomiaDomestica.Text = recursoshumanos.EconomiaDomestica.ToString();
                txtSociologia.Text = recursoshumanos.Sociologia.ToString();
                txtDireito.Text = recursoshumanos.Direito.ToString();
                txtEstagiarios.Text = recursoshumanos.Estagiarios.ToString();
                txtVoluntarios.Text = recursoshumanos.Voluntarios.ToString();
                txtExclusivoServico.Text = recursoshumanos.ExclusivoServico.ToString();
                txtOutroServicos.Text = recursoshumanos.OutrosServicosAssistenciais.ToString();
            }
            this.CarregarTotalRh();
        }

        void CarregarFuncionamento(ProxyEstruturaAssistenciaSocial proxyEstrutura, ServicoRecursoFinanceiroCentroPOPInfo servico)
        {
            #region Carregar: Avaliacoes
            carregarAvaliacoes(proxyEstrutura);
            #endregion

            #region Carregar: data Inicio
            txtDataInicio.Text = servico.DataFuncionamentoServico.HasValue ? servico.DataFuncionamentoServico.Value.ToShortDateString() : String.Empty;
            #endregion

            rblHorasSemana.SelectedValue = servico.IdHorasSemana.ToString();
            rblDiasSemana.SelectedValue = servico.QuantidadeDiasSemana.ToString();

            #region capacidade, capacidade [la|psc]
            this.DistribuirCapacidade(servico);
            this.DistribuirMediaMensal(servico);
            this.AplicarRegraExibicaoLayoutServicosChanged();
            #endregion

            #region Carregar: Avaliacao Gestor
            rblAvaliacaoGestor.SelectedValue = servico.IdAvaliacaoServico.HasValue ? servico.IdAvaliacaoServico.ToString() : String.Empty;
            #endregion
        }

        private void AplicarRegraExibicaoLayoutServicosChanged()
        {
            layout_capacidade.Visible = true;
            layout_media_mensal.Visible = true;

            #region Servico [NÃO TIPIFICADO]
            tbNaoTipificado.Visible = tbNaoTipificadoObjetivo.Visible = ddlTipoServico.SelectedValue == "138" || ddlTipoServico.SelectedValue == "145";
            tbNaoTipificadoDetalhado.Visible = tbNaoTipificadoObjetivo.Visible = ddlTipoServico.SelectedValue == "153";
            #endregion

        }


        #region Helper [Funcionamento] [Servicos]

        private void DistribuirCapacidade(ServicoRecursoFinanceiroCentroPOPInfo servico)
        {
            ServicoRecursoFinanceiroCentroPOPCapacidadeInfo capacidadeExercicio1 = servico.ServicosRecursosFinanceiroCentroPOPCapacidade.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtCapacidadeExercicio1.Text = capacidadeExercicio1 != null ? capacidadeExercicio1.Capacidade != null ? capacidadeExercicio1.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCentroPOPCapacidadeInfo capacidadeExercicio2 = servico.ServicosRecursosFinanceiroCentroPOPCapacidade.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtCapacidadeExercicio2.Text = capacidadeExercicio2 != null ? capacidadeExercicio2.Capacidade != null ? capacidadeExercicio2.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCentroPOPCapacidadeInfo capacidadeExercicio3 = servico.ServicosRecursosFinanceiroCentroPOPCapacidade.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtCapacidadeExercicio3.Text = capacidadeExercicio3 != null ? capacidadeExercicio3.Capacidade != null ? capacidadeExercicio3.Capacidade.ToString() : string.Empty : string.Empty;

            ServicoRecursoFinanceiroCentroPOPCapacidadeInfo capacidadeExercicio4 = servico.ServicosRecursosFinanceiroCentroPOPCapacidade.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtCapacidadeExercicio4.Text = capacidadeExercicio4 != null ? capacidadeExercicio4.Capacidade != null ? capacidadeExercicio4.Capacidade.ToString() : string.Empty : string.Empty;
        }

        private void DistribuirMediaMensal(ServicoRecursoFinanceiroCentroPOPInfo servico)
        {
            ServicoRecursoFinanceiroCentroPOPMediaMensalInfo mediaMensalExercicio1 = servico.ServicosRecursosFinanceiroCentroPOPMediaMensal.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtMediaMensalExercicio1.Text = mediaMensalExercicio1 != null ? (mediaMensalExercicio1.MediaMensal != null ? mediaMensalExercicio1.MediaMensal.ToString() : string.Empty) : string.Empty;

            ServicoRecursoFinanceiroCentroPOPMediaMensalInfo mediaMensalExercicio2 = servico.ServicosRecursosFinanceiroCentroPOPMediaMensal.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtMediaMensalExercicio2.Text = mediaMensalExercicio2 != null ? (mediaMensalExercicio2.MediaMensal != null ? mediaMensalExercicio2.MediaMensal.ToString() : string.Empty) : string.Empty;

            ServicoRecursoFinanceiroCentroPOPMediaMensalInfo mediaMensalExercicio3 = servico.ServicosRecursosFinanceiroCentroPOPMediaMensal.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtMediaMensalExercicio3.Text = mediaMensalExercicio3 != null ? (mediaMensalExercicio3.MediaMensal != null ? mediaMensalExercicio3.MediaMensal.ToString() : string.Empty) : string.Empty;

            ServicoRecursoFinanceiroCentroPOPMediaMensalInfo mediaMensalExercicio4 = servico.ServicosRecursosFinanceiroCentroPOPMediaMensal.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtMediaMensalExercicio4.Text = mediaMensalExercicio4 != null ? (mediaMensalExercicio4.MediaMensal != null ? mediaMensalExercicio4.MediaMensal.ToString() : string.Empty) : string.Empty;
        }

        #endregion

        private void CarregarRecursosFinanceiros(ServicoRecursoFinanceiroCentroPOPInfo entidade)
        {

            int idService = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            this.FontesRecursosExercicio1 = null;
            this.FontesRecursosExercicio2 = null;
            this.FontesRecursosExercicio3 = null;
            this.FontesRecursosExercicio4 = null;

            #region Exercicios
            #region Exercicio 1
            var fundoExercicio1 = entidade.ServicosRecursosFinanceirosFundosCentroPOPInfo
                    .Where(x => x.ServicoRecursoFinanceiroCentroPOPInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroCentroPOP.Exercicios[0]).FirstOrDefault();
            if (fundoExercicio1 != null)
            {
                txtFMASExercicio1.Text = fundoExercicio1.ValorMunicipalAssistencia.ToString("N2");
                txtFMDCAExercicio1.Text = fundoExercicio1.ValorMunicipalFMDCA.ToString("N2");
                txtFEASExercicio1.Text = fundoExercicio1.ValorEstadualAssistencia.ToString("N2");
                txtFEDCAExercicio1.Text = fundoExercicio1.ValorEstadualFEDCA.ToString("N2");
                txtFNASExercicio1.Text = fundoExercicio1.ValorFederalAssistencia.ToString("N2");
                txtFNDCAExercicio1.Text = fundoExercicio1.ValorFederalFNDCA.ToString("N2");
                txtFMIExercicio1.Text = fundoExercicio1.ValorMunicipalFMI.ToString("N2");
                txtFEIExercicio1.Text = fundoExercicio1.ValorEstadualFEI.ToString("N2");
                txtFNIExercicio1.Text = fundoExercicio1.ValorFederalFNI.ToString("N2");
                txtFEASDemandasExercicio1.Text = fundoExercicio1.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio1.Text = fundoExercicio1.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");

                txtFEASAnoAnteriorExercicio1.Text = fundoExercicio1.ValorEstadualAssistenciaAnoAnterior.ToString("N2");

                rblOutrasFontesExercicio1.SelectedValue = fundoExercicio1.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio1.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio1_SelectedIndexChanged(null, null);
                if (fundoExercicio1.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio1.ExisteOutraFonteFinanciamento.Value)
                {
                    //Sessao
                    foreach (var item in fundoExercicio1.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2022);
                    }
                    this.FontesRecursosExercicio1 = fundoExercicio1.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo;
                    carregarRecursosFinanceirosFonteRecursosExercicio1();
                }
            }
            #endregion

            #region Exercicio 2
            var fundoExercicio2 = entidade.ServicosRecursosFinanceirosFundosCentroPOPInfo
                    .Where(x => x.ServicoRecursoFinanceiroCentroPOPInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroCentroPOP.Exercicios[1]).FirstOrDefault();
            if (fundoExercicio2 != null)
            {
                txtFMASExercicio2.Text = fundoExercicio2.ValorMunicipalAssistencia.ToString("N2");
                txtFMDCAExercicio2.Text = fundoExercicio2.ValorMunicipalFMDCA.ToString("N2");
                txtFEASExercicio2.Text = fundoExercicio2.ValorEstadualAssistencia.ToString("N2");
                txtFEDCAExercicio2.Text = fundoExercicio2.ValorEstadualFEDCA.ToString("N2");
                txtFNASExercicio2.Text = fundoExercicio2.ValorFederalAssistencia.ToString("N2");
                txtFNDCAExercicio2.Text = fundoExercicio2.ValorFederalFNDCA.ToString("N2");
                txtFMIExercicio2.Text = fundoExercicio2.ValorMunicipalFMI.ToString("N2");
                txtFEIExercicio2.Text = fundoExercicio2.ValorEstadualFEI.ToString("N2");
                txtFNIExercicio2.Text = fundoExercicio2.ValorFederalFNI.ToString("N2");
                txtFEASDemandasExercicio2.Text = fundoExercicio2.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio2.Text = fundoExercicio2.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");
                txtFEASAnoAnteriorExercicio2.Text = fundoExercicio2.ValorEstadualAssistenciaAnoAnterior.ToString("N2");

                rblOutrasFontesExercicio2.SelectedValue = fundoExercicio2.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio2.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio2_SelectedIndexChanged(null, null);
                if (fundoExercicio2.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio2.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var item in fundoExercicio2.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2023);
                    }
                    this.FontesRecursosExercicio2 = fundoExercicio2.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo;
                    carregarRecursosFinanceirosFonteRecursosExercicio2();
                }
            }
            #endregion

            #region Exercicio 3
            var fundoExercicio3 = entidade.ServicosRecursosFinanceirosFundosCentroPOPInfo
                    .Where(x => x.ServicoRecursoFinanceiroCentroPOPInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroCentroPOP.Exercicios[2]).FirstOrDefault();
            if (fundoExercicio3 != null)
            {
                txtFMASExercicio3.Text = fundoExercicio3.ValorMunicipalAssistencia.ToString("N2");
                txtFMDCAExercicio3.Text = fundoExercicio3.ValorMunicipalFMDCA.ToString("N2");
                txtFEASExercicio3.Text = fundoExercicio3.ValorEstadualAssistencia.ToString("N2");
                txtFEDCAExercicio3.Text = fundoExercicio3.ValorEstadualFEDCA.ToString("N2");
                txtFNASExercicio3.Text = fundoExercicio3.ValorFederalAssistencia.ToString("N2");
                txtFNDCAExercicio3.Text = fundoExercicio3.ValorFederalFNDCA.ToString("N2");
                txtFMIExercicio3.Text = fundoExercicio3.ValorMunicipalFMI.ToString("N2");
                txtFEIExercicio3.Text = fundoExercicio3.ValorEstadualFEI.ToString("N2");
                txtFNIExercicio3.Text = fundoExercicio3.ValorFederalFNI.ToString("N2");
                txtFEASDemandasExercicio3.Text = fundoExercicio3.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio3.Text = fundoExercicio3.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");
                txtFEASAnoAnteriorExercicio3.Text = fundoExercicio3.ValorEstadualAssistenciaAnoAnterior.ToString("N2");

                rblOutrasFontesExercicio3.SelectedValue = fundoExercicio3.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio3.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio3_SelectedIndexChanged(null, null);
                if (fundoExercicio3.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio3.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var item in fundoExercicio3.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2024);
                    }
                    this.FontesRecursosExercicio3 = fundoExercicio3.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo;
                    carregarRecursosFinanceirosFonteRecursosExercicio3();
                }
            }
            #endregion

            #region Exercicio 4
            var fundoExercicio4 = entidade.ServicosRecursosFinanceirosFundosCentroPOPInfo
                    .Where(x => x.ServicoRecursoFinanceiroCentroPOPInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroCentroPOP.Exercicios[3]).FirstOrDefault();
            if (fundoExercicio4 != null)
            {
                txtFMASExercicio4.Text = fundoExercicio4.ValorMunicipalAssistencia.ToString("N2");
                txtFMDCAExercicio4.Text = fundoExercicio4.ValorMunicipalFMDCA.ToString("N2");
                txtFEASExercicio4.Text = fundoExercicio4.ValorEstadualAssistencia.ToString("N2");
                txtFEDCAExercicio4.Text = fundoExercicio4.ValorEstadualFEDCA.ToString("N2");
                txtFNASExercicio4.Text = fundoExercicio4.ValorFederalAssistencia.ToString("N2");
                txtFNDCAExercicio4.Text = fundoExercicio4.ValorFederalFNDCA.ToString("N2");
                txtFMIExercicio4.Text = fundoExercicio4.ValorMunicipalFMI.ToString("N2");
                txtFEIExercicio4.Text = fundoExercicio4.ValorEstadualFEI.ToString("N2");
                txtFNIExercicio4.Text = fundoExercicio4.ValorFederalFNI.ToString("N2");
                txtFEASDemandasExercicio4.Text = fundoExercicio4.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio4.Text = fundoExercicio4.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");
                txtFEASAnoAnteriorExercicio4.Text = fundoExercicio4.ValorEstadualAssistenciaAnoAnterior.ToString("N2");

                rblOutrasFontesExercicio4.SelectedValue = fundoExercicio4.ExisteOutraFonteFinanciamento.HasValue ? Convert.ToInt32(fundoExercicio4.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio4_SelectedIndexChanged(null, null);
                if (fundoExercicio4.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio4.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var item in fundoExercicio4.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2025);
                    }
                    this.FontesRecursosExercicio4 = fundoExercicio4.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo;
                    carregarRecursosFinanceirosFonteRecursosExercicio4();
                }
            }
            #endregion


            #endregion

            using (var proxyprefeitura = new ProxyPrefeitura())
            {
                var pre = proxyprefeitura.Service.GetPrefeituraById(Convert.ToInt32(SessaoPmas.UsuarioLogado.Prefeitura.Id));
                if (pre.ValoresReprogramadosDrads.HasValue && pre.ValoresReprogramadosDrads.Value == true)
                {
                    trFeasAnterior.Visible = true;

                }
            }
            this.AplicarRegraBloqueioDesbloqueio();

        }

        private void carregarRecursosFinanceirosFonteRecursosExercicio1()
        {
            lstRecursosAdicionadosExercicio1.DataSource = this.FontesRecursosExercicio1;
            lstRecursosAdicionadosExercicio1.DataBind();

            if (lstRecursosAdicionadosExercicio1.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio1.Visible = lstRecursosAdicionadosExercicio1.Visible = true;
            }
        }

        private void carregarRecursosFinanceirosFonteRecursosExercicio2()
        {
            lstRecursosAdicionadosExercicio2.DataSource = this.FontesRecursosExercicio2;
            lstRecursosAdicionadosExercicio2.DataBind();

            if (lstRecursosAdicionadosExercicio2.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio2.Visible = lstRecursosAdicionadosExercicio2.Visible = true;
            }
        }

        private void carregarRecursosFinanceirosFonteRecursosExercicio3()
        {
            lstRecursosAdicionadosExercicio3.DataSource =  this.FontesRecursosExercicio3;
            lstRecursosAdicionadosExercicio3.DataBind();

            if (lstRecursosAdicionadosExercicio3.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio3.Visible = lstRecursosAdicionadosExercicio3.Visible = true;
            }
        }

        private void carregarRecursosFinanceirosFonteRecursosExercicio4()
        {
            lstRecursosAdicionadosExercicio4.DataSource = this.FontesRecursosExercicio4;
            lstRecursosAdicionadosExercicio4.DataBind();

            if (lstRecursosAdicionadosExercicio4.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio4.Visible = lstRecursosAdicionadosExercicio4.Visible = true;
            }
        }

        protected void CarregaDemandasParlamentares(ServicoRecursoFinanceiroCentroPOPInfo Servico)
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {


                var demandas = Servico.ServicosRecursosFinanceirosFundosCentroPOPInfo.Where(s => s.Exercicio >= 2022);

                if (demandas.Count() != 0)
                {

                    var demandas1 = demandas.Where(d => d.Exercicio == 2022).FirstOrDefault();
                    if (demandas1 != null)
                    {
                        txtCodigoDemandaExercicio1.Text = demandas1.CodigoDemandaParlamentar.ToString();
                        txtObjetoDemandaExercicio1.Text = demandas1.ObjetoDemandaParlamentar.ToString();

                        if (demandas1.ContrapartidaMunicipal == true)
                        {
                            trValorContraExercicio1.Visible = true;
                            rblContraPartida1.SelectedValue = "1";
                            txtValorContraExercicio1.Text = demandas1.ValorContrapartidaMunicipal.HasValue ? demandas1.ValorContrapartidaMunicipal.Value.ToString("N2") : "0,00";
                        }
                        else
                        {
                            rblContraPartida1.SelectedValue = "0";
                            trValorContraExercicio1.Visible = false;
                        }
                    }

                    var demandas2 = demandas.Where(d => d.Exercicio == 2023).FirstOrDefault();

                    if (demandas2 != null)
                    {
                        txtCodigoDemandaExercicio2.Text = demandas2.CodigoDemandaParlamentar.ToString();
                        txtObjetoDemandaExercicio2.Text = demandas2.ObjetoDemandaParlamentar.ToString();

                        if (demandas2.ContrapartidaMunicipal == true)
                        {
                            trValorContraExercicio2.Visible = true;
                            rblContraPartida1.SelectedValue = "1";
                            txtValorContraExercicio2.Text = demandas2.ValorContrapartidaMunicipal.HasValue ? demandas2.ValorContrapartidaMunicipal.Value.ToString("N2") : "0,00";
                        }
                        else
                        {
                            rblContraPartida2.SelectedValue = "0";
                            trValorContraExercicio2.Visible = false;
                        }
                    }

                    var demandas3 = demandas.Where(d => d.Exercicio == 2024).FirstOrDefault();
                    if (demandas3 != null)
                    {
                        txtCodigoDemandaExercicio3.Text = demandas3.CodigoDemandaParlamentar.ToString();
                        txtObjetoDemandaExercicio3.Text = demandas3.ObjetoDemandaParlamentar.ToString();

                        if (demandas3.ContrapartidaMunicipal == true)
                        {
                            trValorContraExercicio3.Visible = true;
                            rblContraPartida3.SelectedValue = "1";
                            txtValorContraExercicio3.Text = demandas3.ValorContrapartidaMunicipal.HasValue ? demandas3.ValorContrapartidaMunicipal.Value.ToString("N2") : "0,00";
                        }
                        else
                        {
                            rblContraPartida1.SelectedValue = "0";
                            trValorContraExercicio1.Visible = false;
                        }
                    }

                    var demandas4 = demandas.Where(d => d.Exercicio == 2025).FirstOrDefault();

                    if (demandas4 != null)
                    {
                        txtCodigoDemandaExercicio4.Text = demandas4.CodigoDemandaParlamentar.ToString();
                        txtObjetoDemandaExercicio4.Text = demandas4.ObjetoDemandaParlamentar.ToString();

                        if (demandas4.ContrapartidaMunicipal == true)
                        {
                            trValorContraExercicio4.Visible = true;
                            rblContraPartida4.SelectedValue = "1";
                            txtValorContraExercicio4.Text = demandas4.ValorContrapartidaMunicipal.HasValue ? demandas4.ValorContrapartidaMunicipal.Value.ToString("N2") : "0,00";
                        }
                        else
                        {
                            rblContraPartida4.SelectedValue = "0";
                            trValorContraExercicio4.Visible = false;
                        }
                    }
                }
            }

        }


        ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo PreencherRH()
        {
            ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo recursosHumanos = new ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo();
            recursosHumanos.Id = Convert.ToInt32(hdfIdRecursosHumanos.Value);

            if (!String.IsNullOrEmpty(txtSemEscolaridade.Text))
                recursosHumanos.SemEscolarizacao = Convert.ToInt32(txtSemEscolaridade.Text);
            if (!String.IsNullOrEmpty(txtNivelFundamental.Text))
                recursosHumanos.NivelFundamental = Convert.ToInt32(txtNivelFundamental.Text);
            if (!String.IsNullOrEmpty(txtNivelMedio.Text))
                recursosHumanos.NivelMedio = Convert.ToInt32(txtNivelMedio.Text);
            if (!String.IsNullOrEmpty(txtSuperior.Text))
                recursosHumanos.NivelSuperior = Convert.ToInt32(txtSuperior.Text);
            //if (!String.IsNullOrEmpty(txtPosGraduacao.Text))
            //    recursosHumanos.PosGraduacao = Convert.ToInt32(txtPosGraduacao.Text);
            if (!String.IsNullOrEmpty(txtEstagiarios.Text))
                recursosHumanos.Estagiarios = Convert.ToInt32(txtEstagiarios.Text);
            if (!String.IsNullOrEmpty(txtVoluntarios.Text))
                recursosHumanos.Voluntarios = Convert.ToInt32(txtVoluntarios.Text);


            if (!String.IsNullOrEmpty(txtSuperiorServicoSocial.Text))
                recursosHumanos.ServicoSocial = Convert.ToInt32(txtSuperiorServicoSocial.Text);
            if (!String.IsNullOrEmpty(txtSuperiorPsicologia.Text))
                recursosHumanos.Psicologia = Convert.ToInt32(txtSuperiorPsicologia.Text);
            if (!String.IsNullOrEmpty(txtSuperiorPedagogia.Text))
                recursosHumanos.Pedagogia = Convert.ToInt32(txtSuperiorPedagogia.Text);
            if (!String.IsNullOrEmpty(txtSociologia.Text))
                recursosHumanos.Sociologia = Convert.ToInt32(txtSociologia.Text);
            if (!String.IsNullOrEmpty(txtDireito.Text))
                recursosHumanos.Direito = Convert.ToInt32(txtDireito.Text);
            if (!String.IsNullOrEmpty(txtSuperiorTerapiaOcupacional.Text))
                recursosHumanos.TerapiaOcupacional = Convert.ToInt32(txtSuperiorTerapiaOcupacional.Text);
            if (!String.IsNullOrEmpty(txtSuperiorMusicoTerapia.Text))
                recursosHumanos.Musicoterapia = Convert.ToInt32(txtSuperiorMusicoTerapia.Text);
            if (!String.IsNullOrEmpty(txtSuperiorEconomia.Text))
                recursosHumanos.Economia = Convert.ToInt32(txtSuperiorEconomia.Text);
            if (!String.IsNullOrEmpty(txtSuperiorEconomiaDomestica.Text))
                recursosHumanos.EconomiaDomestica = Convert.ToInt32(txtSuperiorEconomiaDomestica.Text);
            if (!String.IsNullOrEmpty(txtSuperiorAntropologia.Text))
                recursosHumanos.Antropologia = Convert.ToInt32(txtSuperiorAntropologia.Text);

            if (!String.IsNullOrEmpty(txtExclusivoServico.Text))
                recursosHumanos.ExclusivoServico = Convert.ToInt32(txtExclusivoServico.Text);
            if (!String.IsNullOrEmpty(txtOutroServicos.Text))
                recursosHumanos.OutrosServicosAssistenciais = Convert.ToInt32(txtOutroServicos.Text);

            return recursosHumanos;
        }
        void CarregarTotalRh()
        {
            try
            {
                int?[] array = {Util.TryParseInt32(txtSemEscolaridade.Text)
                               ,Util.TryParseInt32(txtNivelFundamental.Text)
                               ,Util.TryParseInt32(txtNivelMedio.Text)
                               ,Util.TryParseInt32(txtSuperior.Text)                          
                               };
                int?[] arrayEstVol = { Util.TryParseInt32(txtEstagiarios.Text) };
                lblTotalRh.Text = array.Sum().ToString(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void carregarCombos(ProxyEstruturaAssistenciaSocial proxy, Boolean carregarTipoServicos)
        {
            ddlAbrangencia.DataTextField = "Nome";
            ddlAbrangencia.DataValueField = "Id";
            ddlAbrangencia.DataSource = proxy.Service.GetAbrangenciasServico().Where(S => S.Id >= 3);
            ddlAbrangencia.DataBind();
            Util.InserirItemEscolha(ddlAbrangencia);

            ddlFormaJuridica.DataTextField = "NomeForma";
            ddlFormaJuridica.DataValueField = "Id";
            ddlFormaJuridica.DataSource = proxy.Service.GetFormaJuridica();
            ddlFormaJuridica.DataBind();
            Util.InserirItemEscolha(ddlFormaJuridica);

            //SOMENTE MÉDIA COMPLEXIDADE
            rblTipoProtecao.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(t => t.Id == 2);
            rblTipoProtecao.DataValueField = "Id";
            rblTipoProtecao.DataTextField = "Nome";
            rblTipoProtecao.DataBind();
            rblTipoProtecao.SelectedValue = "2";
            if (carregarTipoServicos)
                carregarTiposServicos(proxy, true);
        }
        void carregarTiposServicos(ProxyEstruturaAssistenciaSocial proxy, Boolean retirarSituacaoRua)
        {
            ddlTipoServico.DataTextField = "Nome";
            ddlTipoServico.DataValueField = "Id";
            var tipos = proxy.Service.GetTiposServicoByTipoProtecaoSocial(Convert.ToInt32(rblTipoProtecao.SelectedValue)).Where(t => t.Id == 144 || t.Id == 145 || t.Id == 140);
            if (retirarSituacaoRua)
                ddlTipoServico.DataSource = tipos.Where(t => t.Id != 144);
            else
                ddlTipoServico.DataSource = tipos;
            ddlTipoServico.DataBind();
            Util.InserirItemEscolha(ddlTipoServico);
        }
        void CarregarSituacoes(ProxyEstruturaAssistenciaSocial proxy)
        {
            lstSituacoesEspecificas.DataTextField = "Nome";
            lstSituacoesEspecificas.DataValueField = "Id";
            lstSituacoesEspecificas.DataSource = proxy.Service.GetSituacoesEspecificasByUsuario(Convert.ToInt32(ddlPublicoAlvo.SelectedValue));
            lstSituacoesEspecificas.DataBind();
        }
        void carregarAvaliacoes(ProxyEstruturaAssistenciaSocial proxy)
        {
            rblAvaliacaoGestor.DataTextField = "Descricao";
            rblAvaliacaoGestor.DataValueField = "Id";
            rblAvaliacaoGestor.DataSource = proxy.Service.GetAvaliacoes();
            rblAvaliacaoGestor.DataBind();
        }
        void carregarAtividades(ProxyEstruturaAssistenciaSocial proxy, Boolean naoTipificado = false)
        {
            lstAtividades.DataValueField = "Id";
            lstAtividades.DataTextField = "Nome";
            if (!naoTipificado)
                lstAtividades.DataSource = proxy.Service.GetAtividadesSocioAssistenciaisByTipoServico(Convert.ToInt32(ddlTipoServico.SelectedValue));
            else
                lstAtividades.DataSource = proxy.Service.GetAtividadesSocioAssistenciaisByTipoServico(Convert.ToInt32(ddlTipoServicoNaoTipificado.SelectedValue));
            lstAtividades.DataBind();
        }

        void carregarUsuarios(ProxyEstruturaAssistenciaSocial proxy, Boolean naoTipificado = false)
        {
            ddlPublicoAlvo.DataTextField = "Nome";
            ddlPublicoAlvo.DataValueField = "Id";
            if (!naoTipificado)
                ddlPublicoAlvo.DataSource = proxy.Service.GetUsuariosByTipoServico(Convert.ToInt32(ddlTipoServico.SelectedValue));
            else
                ddlPublicoAlvo.DataSource = proxy.Service.GetUsuariosByTipoServico(Convert.ToInt32(ddlTipoServicoNaoTipificado.SelectedValue));
            ddlPublicoAlvo.DataBind();
            Util.InserirItemEscolha(ddlPublicoAlvo);
        }
        
        private bool textoVazioNuloComEspaco(string texto)
        {
            if (String.IsNullOrEmpty(texto) || String.IsNullOrWhiteSpace(texto))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void ddlTipoServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                carregarUsuarios(proxy);
                carregarAtividades(proxy);
                carregarTiposServicosNaoTipificados(proxy);
            }
            this.AplicarRegraExibicaoLayoutServicosChanged();
            this.ClearSituacoes();
        }


        protected void btnSalvarRecursoPrograma_Click(object sender, EventArgs e)
        {

            this.AplicarRegraSalvarRecursoPrograma();
            this.btnSalvar_Click(sender, e);
        }
        private void AplicarRegraSalvarRecursoPrograma()
        {
            #region Regra Salvar Recurso programa
            //Considerando salvar os Recursos financeiros para 2019 quando na aba Caracterizacao
            var desbloqueado2018 = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .Single(x => x.Exercicio == 2018 && x.IdRefBloqueio == 19).Desbloqueado.Value;

            var desbloqueado2019 = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .Single(x => x.Exercicio == 2019 && x.IdRefBloqueio == 19).Desbloqueado.Value;
            if (desbloqueado2018 && desbloqueado2019)
            {
                hdnExercicio.Value = FServicoRecursoFinanceiroCentroPOP.Exercicios[0].ToString();
            }
            else if (desbloqueado2018)
            {
                hdnExercicio.Value = FServicoRecursoFinanceiroCentroPOP.Exercicios[0].ToString();
            }
            else if (desbloqueado2019)
            {
                hdnExercicio.Value = FServicoRecursoFinanceiroCentroPOP.Exercicios[1].ToString();
            }
            #endregion
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();

            var idServico = !String.IsNullOrEmpty(Request.QueryString["id"]) ? Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])) : 0;
            var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));

            var servico = new ServicoRecursoFinanceiroCentroPOPInfo();
            string action = (idServico == 0) ? "I" : "A";
            try
            {
                if (action == "A")
                {
                    servico.Id = idServico;
                }

                #region Caracterizacao do Servico
                servico.UsuarioTipoServico = new UsuarioTipoServicoInfo();
                if (ddlTipoServico.SelectedIndex != -1)
                {
                    servico.UsuarioTipoServico.IdTipoServico = Convert.ToInt32(ddlTipoServico.SelectedValue);
                }

                servico.UsuarioTipoServico.TipoServico = new TipoServicoInfo();

                if (rblTipoProtecao.SelectedIndex != -1)
                {
                    servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial = Convert.ToInt16(rblTipoProtecao.SelectedValue);
                }

                if (ddlPublicoAlvo.SelectedIndex != -1)
                {
                    servico.IdUsuarioTipoServico = Convert.ToInt32(ddlPublicoAlvo.SelectedValue);
                }

                servico.PossuiTecnicoResponsavel = !chkNaoPossuiTecnicoResponsavel.Checked;
                servico.NomeTecnicoResponsavel = txtTecnicoResponsavel.Text;

                servico.IdCentroPOP = idCentro;


                if (ddlTipoServico.SelectedValue == "138" || ddlTipoServico.SelectedValue == "145")
                {
                    servico.IdTipoServicoNaoTipificado = ddlTipoServicoNaoTipificado.SelectedValue != "0" ? Convert.ToInt32(ddlTipoServicoNaoTipificado.SelectedValue) : new Nullable<Int32>();
                    //Outro tipo de serviço não tipificado
                    if (servico.IdTipoServicoNaoTipificado.HasValue &&
                       (servico.IdTipoServicoNaoTipificado.Value == 156
                         || servico.IdTipoServicoNaoTipificado.Value == 159)
                       )
                    {
                        servico.DescricaoServicoNaoTipificado = txtNaotipificado.Text;
                        servico.ObjetivoServicoNaoTipificado = txtObjetivoNaoTipificado.Text;
                    }
                }

                if (ddlTipoServico.SelectedValue == "153")
                {
                    servico.DescricaoServicoNaoTipificado = txtNaotipificado.Text;
                    servico.ObjetivoServicoNaoTipificado = txtObjetivoNaoTipificado.Text;
                }


                if (!String.IsNullOrEmpty(ddlAbrangencia.SelectedValue))
                {
                    servico.IdAbrangenciaServico = Convert.ToInt32(ddlAbrangencia.SelectedValue);
                }


                if (rblAbrangencia.SelectedValue != null)
                {
                    servico.MunicipioSedeServico = rblAbrangencia.SelectedValue == "1" ? true : false;
                }

                if (ddlFormaJuridica.SelectedValue != null)
                {
                    servico.IdFormaJuridica = Convert.ToInt32(ddlFormaJuridica.SelectedValue);
                }

                servico.IndicaMunicipiosParticipamOfertaServico = txtMunicipioParticipaOferta.Text;

                servico.IndicaMunicipiosSedeServico = txtMunicipioSede.Text;

                servico.IdCaracteristicasTerritorio = Convert.ToInt32(rblCaracteristicasTerritorio.SelectedValue);

                servico.IdCaracteristicaOfertaServico = Convert.ToInt32(rblCaracteristicaOferta.SelectedValue);	

                servico.PossuiTecnicoResponsavel = !chkNaoPossuiTecnicoResponsavel.Checked;

                servico.NomeTecnicoResponsavel = txtTecnicoResponsavel.Text;



                if (!String.IsNullOrEmpty(rblCaracteristicasTerritorio.SelectedValue))
                {
                    servico.IdCaracteristicasTerritorio = Convert.ToInt32(rblCaracteristicasTerritorio.SelectedValue);
                }
                #endregion

                #region consorcio
                var consorcio = new ConsorcioCentroPOPInfo();

                consorcio.IdServicosRecursosFinanceirosCentroPOP = servico.Id;
                consorcio.NomeConsorcio = txtNomeConsorcio.Text;
                consorcio.MunicipioSede = txtMunicipioSedeConsorcio.Text;
                consorcio.MunicipioEnvolvido = txtMunicipiosEnvolvidos.Text;
                consorcio.CNPJ = txtCNPJConsorcio.Text;
                #endregion 

                #region Caracterização do Usuario
                if (!String.IsNullOrEmpty(rblMoradiaUsuarios.SelectedValue))
                {
                    servico.IdRegiaoMoradia = Convert.ToInt32(rblMoradiaUsuarios.SelectedValue);
                }

                if (!String.IsNullOrEmpty(rblSexo.SelectedValue))
                {
                    servico.IdSexo = Convert.ToInt32(rblSexo.SelectedValue);
                }

                servico.SituacoesEspecificas = new List<SituacaoEspecificaInfo>();
                foreach (ListItem situacao in lstSituacoesEspecificas.Items)
                {
                    if (situacao.Selected)
                    {
                        servico.SituacoesEspecificas.Add(new SituacaoEspecificaInfo() { Id = Convert.ToInt32(situacao.Value) });
                    }
                }
                #endregion


                #region Carregar: Capacidade
                servico.ServicosRecursosFinanceiroCentroPOPCapacidade = new List<ServicoRecursoFinanceiroCentroPOPCapacidadeInfo>();
                ServicoRecursoFinanceiroCentroPOPCapacidadeInfo servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio1 = new ServicoRecursoFinanceiroCentroPOPCapacidadeInfo();
                servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio1.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio1.Text) ? Convert.ToInt32(txtCapacidadeExercicio1.Text) : 0;
                servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio1.Exercicio = FServicoRecursoFinanceiroCentroPOP.Exercicios[0];

                ServicoRecursoFinanceiroCentroPOPCapacidadeInfo servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio2 = new ServicoRecursoFinanceiroCentroPOPCapacidadeInfo();
                servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio2.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio2.Text) ? Convert.ToInt32(txtCapacidadeExercicio2.Text) : 0;
                servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio2.Exercicio = FServicoRecursoFinanceiroCentroPOP.Exercicios[1];

                ServicoRecursoFinanceiroCentroPOPCapacidadeInfo servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio3 = new ServicoRecursoFinanceiroCentroPOPCapacidadeInfo();
                servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio3.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio3.Text) ? Convert.ToInt32(txtCapacidadeExercicio3.Text) : 0;
                servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio3.Exercicio = FServicoRecursoFinanceiroCentroPOP.Exercicios[2];

                ServicoRecursoFinanceiroCentroPOPCapacidadeInfo servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio4 = new ServicoRecursoFinanceiroCentroPOPCapacidadeInfo();
                servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio4.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio4.Text) ? Convert.ToInt32(txtCapacidadeExercicio4.Text) : 0;
                servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio4.Exercicio = FServicoRecursoFinanceiroCentroPOP.Exercicios[3];

                servico.ServicosRecursosFinanceiroCentroPOPCapacidade.Add(servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio1);
                servico.ServicosRecursosFinanceiroCentroPOPCapacidade.Add(servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio2);
                servico.ServicosRecursosFinanceiroCentroPOPCapacidade.Add(servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio3);
                servico.ServicosRecursosFinanceiroCentroPOPCapacidade.Add(servicoRecursoFinanceiroCentroPOPCapacidadeInfoExercicio4);
                #endregion

                #region Carregar: MM
                servico.ServicosRecursosFinanceiroCentroPOPMediaMensal = new List<ServicoRecursoFinanceiroCentroPOPMediaMensalInfo>();
                
                #region Media Mensal Exercicio 1
                ServicoRecursoFinanceiroCentroPOPMediaMensalInfo servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio1 = new ServicoRecursoFinanceiroCentroPOPMediaMensalInfo();
                if (!String.IsNullOrEmpty(txtMediaMensalExercicio1.Text))
                {
                    servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio1.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio1.Text);
                }
                servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio1.Exercicio = FServicoRecursoFinanceiroCentroPOP.Exercicios[0]; 
                #endregion

                #region Media Mensal Exercicio 2
                ServicoRecursoFinanceiroCentroPOPMediaMensalInfo servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio2 = new ServicoRecursoFinanceiroCentroPOPMediaMensalInfo();
                if (!String.IsNullOrEmpty(txtMediaMensalExercicio2.Text))
                {
                    servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio2.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio2.Text);
                }
                servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio2.Exercicio = FServicoRecursoFinanceiroCentroPOP.Exercicios[1]; 
                #endregion


                #region Media Mensal Exercicio 3
                ServicoRecursoFinanceiroCentroPOPMediaMensalInfo servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio3 = new ServicoRecursoFinanceiroCentroPOPMediaMensalInfo();
                if (!String.IsNullOrEmpty(txtMediaMensalExercicio3.Text))
                {
                    servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio3.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio3.Text);
                }
                servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio3.Exercicio = FServicoRecursoFinanceiroCentroPOP.Exercicios[2];
                #endregion

                #region Media Mensal Exercicio 4
                ServicoRecursoFinanceiroCentroPOPMediaMensalInfo servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio4 = new ServicoRecursoFinanceiroCentroPOPMediaMensalInfo();
                if (!String.IsNullOrEmpty(txtMediaMensalExercicio4.Text))
                {
                    servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio4.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio4.Text);
                }
                #endregion
                servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio4.Exercicio = FServicoRecursoFinanceiroCentroPOP.Exercicios[3]; 


                servico.ServicosRecursosFinanceiroCentroPOPMediaMensal.Add(servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio1);
                servico.ServicosRecursosFinanceiroCentroPOPMediaMensal.Add(servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio2);
                servico.ServicosRecursosFinanceiroCentroPOPMediaMensal.Add(servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio3);
                servico.ServicosRecursosFinanceiroCentroPOPMediaMensal.Add(servicoRecursoFinanceiroCentroPOPMediaMensalInfoExercicio4);
                #endregion

                #region Recursos Financeiros

                #region Exercicio 1
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroCentroPOP.Exercicios[0])
                {
                    ServicoRecursoFinanceiroFundosCentroPOPInfo fundo = new ServicoRecursoFinanceiroFundosCentroPOPInfo();
                    fundo.ServicoRecursoFinanceiroCentroPOPInfoId = servico.Id;
                    fundo.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio1.Text)) ? Convert.ToDecimal(txtFEASExercicio1.Text) : 0M;
                    fundo.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio1.Text)) ? Convert.ToDecimal(txtFEDCAExercicio1.Text) : 0M;
                    fundo.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio1.Text)) ? Convert.ToDecimal(txtFMASExercicio1.Text) : 0M;
                    fundo.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio1.Text)) ? Convert.ToDecimal(txtFMDCAExercicio1.Text) : 0M;
                    fundo.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio1.Text)) ? Convert.ToDecimal(txtFNASExercicio1.Text) : 0M;
                    fundo.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio1.Text)) ? Convert.ToDecimal(txtFNDCAExercicio1.Text) : 0M;
                    fundo.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio1.Text) ? Convert.ToDecimal(txtFMIExercicio1.Text) : 0M;
                    fundo.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio1.Text) ? Convert.ToDecimal(txtFEIExercicio1.Text) : 0M;
                    fundo.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio1.Text) ? Convert.ToDecimal(txtFNIExercicio1.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio1.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio1.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio1.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio1.Text) : 0M;
                    fundo.ValorEstadualAssistenciaAnoAnterior = Convert.ToDecimal(txtFEASAnoAnteriorExercicio1.Text);
                    //Define o merge (adiciona quando nao encontra / atualiza quando encontra)
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio1.Text) ?  string.Empty : txtObjetoDemandaExercicio1.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio1.Text) ? string.Empty : txtCodigoDemandaExercicio1.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio1.Text) ? Convert.ToDecimal(txtValorContraExercicio1.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;

                    servico.ServicosRecursosFinanceirosFundosCentroPOPInfo = servico.ServicosRecursosFinanceirosFundosCentroPOPInfo ?? new List<ServicoRecursoFinanceiroFundosCentroPOPInfo>();
                    servico.ServicosRecursosFinanceirosFundosCentroPOPInfo.Add(fundo);

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio1.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        fundo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo = FontesRecursosExercicio1;
                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosCentroPOP = fundo.Id;
                        }
                    }
                }
                #endregion

                #region Exercicio 2
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroCentroPOP.Exercicios[1])
                {
                    ServicoRecursoFinanceiroFundosCentroPOPInfo fundo = new ServicoRecursoFinanceiroFundosCentroPOPInfo();
                    fundo.ServicoRecursoFinanceiroCentroPOPInfoId = servico.Id;
                    fundo.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio2.Text)) ? Convert.ToDecimal(txtFEASExercicio2.Text) : 0M;
                    fundo.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio2.Text)) ? Convert.ToDecimal(txtFEDCAExercicio2.Text) : 0M;
                    fundo.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio2.Text)) ? Convert.ToDecimal(txtFMASExercicio2.Text) : 0M;
                    fundo.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio2.Text)) ? Convert.ToDecimal(txtFMDCAExercicio2.Text) : 0M;
                    fundo.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio2.Text)) ? Convert.ToDecimal(txtFNASExercicio2.Text) : 0M;
                    fundo.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio2.Text)) ? Convert.ToDecimal(txtFNDCAExercicio2.Text) : 0M;
                    fundo.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio2.Text) ? Convert.ToDecimal(txtFMIExercicio2.Text) : 0M;
                    fundo.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio2.Text) ? Convert.ToDecimal(txtFEIExercicio2.Text) : 0M;
                    fundo.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio2.Text) ? Convert.ToDecimal(txtFNIExercicio2.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio2.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio2.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio2.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio2.Text) : 0M;
                    fundo.ValorEstadualAssistenciaAnoAnterior = !String.IsNullOrEmpty(txtFEASAnoAnteriorExercicio2.Text) ? Convert.ToDecimal(txtFEASAnoAnteriorExercicio2.Text) : 0M;
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio2.Text) ? string.Empty : txtObjetoDemandaExercicio2.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio2.Text) ? string.Empty : txtCodigoDemandaExercicio2.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio2.Text) ? Convert.ToDecimal(txtValorContraExercicio2.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;

                    var totalLinhasExercicio2 = lstRecursosAdicionadosExercicio2.Items.Count;

                    servico.ServicosRecursosFinanceirosFundosCentroPOPInfo = servico.ServicosRecursosFinanceirosFundosCentroPOPInfo ?? new List<ServicoRecursoFinanceiroFundosCentroPOPInfo>();
                    servico.ServicosRecursosFinanceirosFundosCentroPOPInfo.Add(fundo);

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio2.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        if (totalLinhasExercicio2 > 0)
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(true);
                        else
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(false);

                        fundo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo = this.FontesRecursosExercicio2;
                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosCentroPOP = fundo.Id;
                        }
                    }
                }
                #endregion

                #region Exercicio 3

                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroCentroPOP.Exercicios[2])
                {
                    ServicoRecursoFinanceiroFundosCentroPOPInfo fundo = new ServicoRecursoFinanceiroFundosCentroPOPInfo();
                    fundo.ServicoRecursoFinanceiroCentroPOPInfoId = servico.Id;
                    fundo.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio3.Text)) ? Convert.ToDecimal(txtFEASExercicio3.Text) : 0M;
                    fundo.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio3.Text)) ? Convert.ToDecimal(txtFEDCAExercicio3.Text) : 0M;
                    fundo.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio3.Text)) ? Convert.ToDecimal(txtFMASExercicio3.Text) : 0M;
                    fundo.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio3.Text)) ? Convert.ToDecimal(txtFMDCAExercicio3.Text) : 0M;
                    fundo.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio3.Text)) ? Convert.ToDecimal(txtFNASExercicio3.Text) : 0M;
                    fundo.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio3.Text)) ? Convert.ToDecimal(txtFNDCAExercicio3.Text) : 0M;
                    fundo.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio3.Text) ? Convert.ToDecimal(txtFMIExercicio3.Text) : 0M;
                    fundo.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio3.Text) ? Convert.ToDecimal(txtFEIExercicio3.Text) : 0M;
                    fundo.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio3.Text) ? Convert.ToDecimal(txtFNIExercicio3.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio3.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio3.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio3.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio3.Text) : 0M;
                    fundo.ValorEstadualAssistenciaAnoAnterior = !String.IsNullOrEmpty(txtFEASAnoAnteriorExercicio3.Text) ? Convert.ToDecimal(txtFEASAnoAnteriorExercicio3.Text) : 0M;
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio3.Text) ? string.Empty : txtObjetoDemandaExercicio3.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio3.Text) ? string.Empty : txtCodigoDemandaExercicio3.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio3.Text) ? Convert.ToDecimal(txtValorContraExercicio3.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida3.SelectedValue == "1" ? true : false;

                    var totalLinhasExercicio3 = lstRecursosAdicionadosExercicio3.Items.Count;

                    servico.ServicosRecursosFinanceirosFundosCentroPOPInfo = servico.ServicosRecursosFinanceirosFundosCentroPOPInfo ?? new List<ServicoRecursoFinanceiroFundosCentroPOPInfo>();
                    servico.ServicosRecursosFinanceirosFundosCentroPOPInfo.Add(fundo);

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio3.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        if (totalLinhasExercicio3 > 0)
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(true);
                        else
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(false);


                        fundo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo = this.FontesRecursosExercicio3;
                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosCentroPOP = fundo.Id;
                        }
                    }
                }
                #endregion

                #region Exercicio 4

                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroCentroPOP.Exercicios[3])
                {
                    ServicoRecursoFinanceiroFundosCentroPOPInfo fundo = new ServicoRecursoFinanceiroFundosCentroPOPInfo();
                    fundo.ServicoRecursoFinanceiroCentroPOPInfoId = servico.Id;
                    fundo.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio4.Text)) ? Convert.ToDecimal(txtFEASExercicio4.Text) : 0M;
                    fundo.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio4.Text)) ? Convert.ToDecimal(txtFEDCAExercicio4.Text) : 0M;
                    fundo.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio4.Text)) ? Convert.ToDecimal(txtFMASExercicio4.Text) : 0M;
                    fundo.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio4.Text)) ? Convert.ToDecimal(txtFMDCAExercicio4.Text) : 0M;
                    fundo.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio4.Text)) ? Convert.ToDecimal(txtFNASExercicio4.Text) : 0M;
                    fundo.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio4.Text)) ? Convert.ToDecimal(txtFNDCAExercicio4.Text) : 0M;
                    fundo.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio4.Text) ? Convert.ToDecimal(txtFMIExercicio4.Text) : 0M;
                    fundo.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio4.Text) ? Convert.ToDecimal(txtFEIExercicio4.Text) : 0M;
                    fundo.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio4.Text) ? Convert.ToDecimal(txtFNIExercicio4.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio4.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio4.Text) : 0M;
                    fundo.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio4.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio4.Text) : 0M;
                    fundo.ValorEstadualAssistenciaAnoAnterior = !String.IsNullOrEmpty(txtFEASAnoAnteriorExercicio4.Text) ? Convert.ToDecimal(txtFEASAnoAnteriorExercicio4.Text) : 0M;
                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio4.Text) ? string.Empty : txtObjetoDemandaExercicio4.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio4.Text) ? string.Empty : txtCodigoDemandaExercicio4.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio4.Text) ? Convert.ToDecimal(txtValorContraExercicio4.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida4.SelectedValue == "1" ? true : false;

                    var totalLinhasExercicio4 = lstRecursosAdicionadosExercicio4.Items.Count;

                    servico.ServicosRecursosFinanceirosFundosCentroPOPInfo = servico.ServicosRecursosFinanceirosFundosCentroPOPInfo ?? new List<ServicoRecursoFinanceiroFundosCentroPOPInfo>();
                    servico.ServicosRecursosFinanceirosFundosCentroPOPInfo.Add(fundo);

                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio4.SelectedValue == "1" ? true : false;
                    if (fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        if (totalLinhasExercicio4 > 0)
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(true);
                        else
                            new ValidadorServicosFinanceirosFonteRecursos().ValidarColecao(false);

                        fundo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo = this.FontesRecursosExercicio4;

                        foreach (var fonteNova in fundo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo)
                        {
                            fonteNova.IdServicoRecursoFinanceiroFundosCentroPOP = fundo.Id;
                        }
                    }
                }
                #endregion

                #endregion


                #region  Preencher: [Atividade Socio Assistenciais]
                servico.AtividadesSocioAssistenciais = new List<AtividadeSocioAssistencialInfo>();
                foreach (ListItem atividade in lstAtividades.Items)
                {
                    if (atividade.Selected)
                    {
                        servico.AtividadesSocioAssistenciais.Add(new AtividadeSocioAssistencialInfo() { Id = Convert.ToInt32(atividade.Value) });
                    }
                }
                #endregion

                #region  Preencher: [Hora semana]
                servico.IdHorasSemana = Convert.ToInt32(rblHorasSemana.SelectedValue);
                servico.QuantidadeDiasSemana = Convert.ToInt32(rblDiasSemana.SelectedValue);
                #endregion

                #region Preencher: [Gestor]
                if (!String.IsNullOrEmpty(rblAvaliacaoGestor.SelectedValue))
                {
                    servico.IdAvaliacaoServico = Convert.ToInt32(rblAvaliacaoGestor.SelectedValue);
                }
                #endregion

                #region Preencher: [Data Funcionamento]
                DateTime dt;
                if (!String.IsNullOrEmpty(txtDataInicio.Text) && DateTime.TryParse(txtDataInicio.Text, out dt))
                {
                    servico.DataFuncionamentoServico = Convert.ToDateTime(txtDataInicio.Text);
                }
                #endregion

                #region Aplicar: Validacao
                new ValidadorServicoRecursoFinanceiro().ValidarCentroPOP(servico);
                var recursosHumanos = PreencherRH();
                var validaRh = new ValidadorRecursosHumanos().ValidaPop(recursosHumanos);
                if (validaRh.Count > 0)
                {
                    throw new Exception(Extensions.Concat(validaRh, System.Environment.NewLine));
                }
                #endregion

                using (var proxy = new ProxyRedeProtecaoSocial())
                {

                    if (action == "I")
                    {
                        proxy.Service.AddServicoRecursoFinanceiroCentroPOP(servico);

                        recursosHumanos.IdServicosRecursosFinanceirosCentroPOP = servico.Id;

                        consorcio.IdServicosRecursosFinanceirosCentroPOP = servico.Id;

                        using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
                        {
                            proxySocial.Service.SalvarConsorcioCentroPOP(consorcio);
                        }


                        if (recursosHumanos.Id == 0)
                        {
                            proxy.Service.AddServicoRecursoFinanceiroCentroPOPRH(recursosHumanos);
                        }
                        #region Adicionar: Programas e Projetos
                        bool existemProgramasEProjetos = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio != null;
                        if (existemProgramasEProjetos)
                        {
                            foreach (var item in SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio)
                            {
                                if (item.Nome.ToLower().Contains("beneficio de prestação continuada - bpc idosos")
                                      || item.Nome.ToLower().Contains("beneficio de prestação continuada - bpc pessoas com deficiência")
                                      || item.Nome.Contains("Bolsa Família")
                                      || item.Nome.ToLower().Contains("peti")
                                      || item.Nome.ToLower().Contains("ação jovem")
                                      || item.Nome.ToLower().Contains("renda cidadã")
                                      || item.Nome.ToLower().Contains("teste transferencia renda")
                                      )
                                {
                                    AdicionaTransferenciaRenda(idCentro, servico.Id, item.IdProgramaProjeto);

                                }
                                else if (item.Nome.ToLower().Contains("auxílio natalidade")
                                   || item.Nome.ToLower().Contains("auxílio funeral")
                                   || item.Nome.ToLower().Contains("calamidades públicas e emergências")
                                   || item.Nome.ToLower().Contains("vulnerabilidade temporária")
                               )
                                {
                                    AdicionaPrefeituraBeneficio(idCentro, servico.Id, item.IdProgramaProjeto);
                                }
                                else
                                {
                                    AdicionarProgramaConfinamento(idCentro, servico.Id, item.IdProgramaProjeto);
                                }

                            }
                        }
                        #endregion


                        PrefeituraBeneficioEventualServicoExercicio = null;
                        ProgramaProjetoCofinanciamentoExercicio = null;
                        TransferenciaRendaCofinanciamentoExercicio = null;
                        SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = null;
                        PrefeituraBeneficioEventualServicoExercicio = null;
                        rptProgramaTemp.DataSource = null;
                        rptProgramaTemp.DataBind();
                    }


                    if (action == "A")
                    {
                        //update 
                        proxy.Service.UpdateServicoRecursoFinanceiroCentroPOP(servico);
                        //seta servicos id
                        recursosHumanos.IdServicosRecursosFinanceirosCentroPOP = servico.Id;
                        //verifica veracidade do id
                        if (recursosHumanos.Id == 0)
                        {
                            proxy.Service.AddServicoRecursoFinanceiroCentroPOPRH(recursosHumanos);
                        }
                        else
                        {
                            proxy.Service.UpdateServicoRecursoFinanceiroCentroPOPRH(recursosHumanos);
                        }

                        consorcio.IdServicosRecursosFinanceirosCentroPOP = servico.Id;
                        using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
                        {
                            proxySocial.Service.SalvarConsorcioCentroPOP(consorcio);
                        }


                    }
                }

                if (action == "A")
                {
                    // caso usuario não beneficiário de algum programa, projeto ou benefício, limpar serviços
                    if (!Convert.ToBoolean(Convert.ToInt32(rblIntegracaoRede.SelectedValue)))
                    {
                        var idServicoBeneficio = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                        using (var proxyProgramas = new ProxyProgramas())
                        {

                            var programas = proxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(idServicoBeneficio, servico.IdCentroPOP);
                            if (programas != null)
                            {
                                var lst = programas.OrderBy(t => t.IdTipoProtecao)
                                                             .GroupBy(s => s.ProtecaoSocial)
                                                             .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) })
                                                             .ToList();

                                foreach (var tab in lst)
                                {
                                    foreach (var item in tab.Items)
                                    {
                                        proxyProgramas.Service.DeleteProgramaProjetoCofinanciamento(Convert.ToInt32(item.Id), Convert.ToInt32(item.TipoCofinanciamento));
                                    }

                                }

                                trRendaCidadaBeneficioIdoso.Visible = true;
                                lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
                                lstRecursos.DataSource = lst;
                                lstRecursos.DataBind();
                            }

                        }
                    }
                }
                FontesRecursosExercicio1 = null;
                FontesRecursosExercicio2 = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }


            var id = servico.Id;
            var idCentroDecrypt = Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]);
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            if (action == "I")
            {
                Response.Redirect("~/BlocoIII/FServicoRecursoFinanceiroCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(servico.Id.ToString())) + "&idCentro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentroDecrypt)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)) + "&msg=" + action);
            }
            else
            {
                Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentroDecrypt)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)) + "&msg=" + action);
            }

        }

        protected void rblEstadualizado_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trValorEstadualizado.Visible = rblEstadualizado.SelectedValue == "1";
        }

        protected void rblTipoProtecao_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                carregarTiposServicos(proxy, true);
                carregarTiposServicosNaoTipificados(proxy);
                tbNaoTipificado.Visible = false;
                tbNaoTipificadoDetalhado.Visible = false;
                txtNaotipificado.Text = "";
                txtObjetivoNaoTipificado.Text = "";
            }

            if (rblTipoProtecao.SelectedValue == "2")
            {
                CarregarOfertaServico(true);
            }
            else
            {
                CarregarOfertaServico(false);
            }	
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idCentro = Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]);
            Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro)) + "&idUnidade=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }
        protected void chkNaoPossuiTecnicoResponsavel_CheckedChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            txtTecnicoResponsavel.Enabled = !chkNaoPossuiTecnicoResponsavel.Checked;
            if (!chkNaoPossuiTecnicoResponsavel.Checked)
            {
                this.Master.ScriptManagerControl.SetFocus(txtTecnicoResponsavel);
            }
            else
            {
                txtTecnicoResponsavel.Text = String.Empty;
            }
        }

        protected void ddlPublicoAlvo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {

                if (ddlTipoServicoNaoTipificado.SelectedValue == "159" && ddlPublicoAlvo.SelectedValue == "87")
                {                    
                    lblCapacidade.Text = "famílias";
                }
                else
                {
                    if (ddlPublicoAlvo.SelectedValue != "0")
                    {
                        lblCapacidade.Text = "pessoas";
                    }
                }
                CarregarSituacoes(proxy);
            }
        }



        protected void ddlTipoServicoNaoTipificado_SelectedIndexChanged(object sender, EventArgs e) //PMAS 2016
        {
            SessaoPmas.VerificarSessao(this);

            if (ddlTipoServicoNaoTipificado.SelectedItem.Text == "Outro")
            {
                tbNaoTipificadoObjetivo.Visible = tbNaoTipificadoDetalhado.Visible = true;
            }
            else
            {
                tbNaoTipificadoObjetivo.Visible = tbNaoTipificadoDetalhado.Visible = false;
                txtObjetivoNaoTipificado.Text = txtNaotipificado.Text = "";
            }

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                carregarUsuarios(proxy, true);
                carregarAtividades(proxy, true);
            }

            //LIMPAR SITUAÇÕES
            lstSituacoesEspecificas.DataTextField = "Nome";
            lstSituacoesEspecificas.DataValueField = "Id";
            lstSituacoesEspecificas.DataSource = new List<SituacaoEspecificaInfo>();
            lstSituacoesEspecificas.DataBind();
        }

        void carregarTiposServicosNaoTipificados(ProxyEstruturaAssistenciaSocial proxy) //PMAS 2016
        {
            ddlTipoServicoNaoTipificado.DataTextField = "Nome";
            ddlTipoServicoNaoTipificado.DataValueField = "Id";
            ddlTipoServicoNaoTipificado.DataSource = rblTipoProtecao.SelectedValue != "3" ?
                proxy.Service.GetTiposServicoNaoTipificadoByTipoProtecaoSocial(Convert.ToInt32(rblTipoProtecao.SelectedValue)) : new List<TipoServicoInfo>();
            ddlTipoServicoNaoTipificado.DataBind();

            ListItem itemToRemove = ddlTipoServicoNaoTipificado.Items.FindByValue("157");
            if (itemToRemove != null)
                ddlTipoServicoNaoTipificado.Items.Remove(itemToRemove);

            ListItem itemRemove = ddlTipoServicoNaoTipificado.Items.FindByValue("158");
            if (itemRemove != null)
                ddlTipoServicoNaoTipificado.Items.Remove(itemRemove);

            Util.InserirItemEscolha(ddlTipoServicoNaoTipificado);
        }
        protected void rblIntegracaoRede_SelectedIndexChanged(object sender, EventArgs e)
        {
            trProgramasBeneficios.Visible = false;
            if (rblIntegracaoRede.SelectedValue == "1")
            {
                trProgramasBeneficios.Visible = true;
                using (var proxy = new ProxyProgramas())
                {
                    carregarComboProgramas(proxy);
                }
            }
        }

        void CarregarProgramas(ProxyProgramas proxy, int idServicosRecursosFinanceiros, int idCentro)
        {
            var programas = proxy.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(idServicosRecursosFinanceiros, idCentro);
            if (programas != null)
            {
                var lst = programas.OrderBy(t => t.IdTipoProtecao)
                                   .GroupBy(s => s.ProtecaoSocial)
                                   .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) }).ToList();

                if (lst != null)
                {
                    lstRecursos.DataSource = lst;
                    lstRecursos.DataBind();
                    if (lstRecursos.Items.Count > 0)
                    {
                        rblIntegracaoRede.SelectedValue = "1";
                        trProgramasBeneficios.Visible = true;
                    }
                    else
                    {
                        rblIntegracaoRede.SelectedValue = "0";
                        trProgramasBeneficios.Visible = false;
                    }
                }
            }

            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";

            btnSalvarRecursoPrograma.Visible = lstRecursos.Items.Count > 0;
        }

        void carregarComboProgramas(ProxyProgramas proxy)
        {
            ddlProgramaBeneficio.DataValueField = "Id";
            ddlProgramaBeneficio.DataTextField = "Nome";
            ddlProgramaBeneficio.DataSource = proxy.Service.GetProgramasByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            ddlProgramaBeneficio.DataBind();
            Util.InserirItemEscolha(ddlProgramaBeneficio);
        }


        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            lblInconsistencias.Text = String.Empty;
            tbInconsistencias.Visible = false;
            frame1_1.Attributes.Add("class", "frame active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
            try
            {
                if (SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio != null)
                {
                    var existe = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.Where(x => x.IdProgramaProjeto == Convert.ToInt32(ddlProgramaBeneficio.SelectedValue)).ToList();
                    if (existe.Count > 0)
                    {
                        throw new Exception(String.Concat("Programa já existente, caso deseja é possivel remover e inserir novamente.", System.Environment.NewLine));
                    }
                }

                if (ddlTipoServico.SelectedValue == "0")
                {
                    throw new Exception(String.Concat("Informe o tipo de serviço.", System.Environment.NewLine));
                }

                if (ddlPublicoAlvo.SelectedValue == "0")
                {
                    throw new Exception(String.Concat("Informe o tipo de beneficiários deste serviço.", System.Environment.NewLine));
                }
                if (trRendaCidadaBeneficioIdoso.Visible && String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
                {
                    throw new Exception(String.Concat("Informe o numero de beneficiários deste serviço.", System.Environment.NewLine));
                }
                if (ddlProgramaBeneficio.SelectedValue != "0" || ddlProgramaBeneficio.SelectedValue != "")
                {
                    if (ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("beneficio de prestação continuada - bpc idosos")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("beneficio de prestação continuada - bpc pessoas com deficiência")
                        || ddlProgramaBeneficio.SelectedItem.Text.Contains("BOLSA FAMÍLIA")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("peti")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("ação jovem")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("renda cidadã")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("teste transferencia renda")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("fortalecimento da vigilância socioassistencial")
                        )
                    {
                        AtualizaTransferenciaRenda(idCentro);
                    }
                    else if (ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("auxílio natalidade")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("auxílio funeral")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("calamidades públicas e emergências")
                        || ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("vulnerabilidade temporária")
                    )
                    {
                        AtualizaPrefeituraBeneficio(idCentro);
                    }
                    else
                    {
                        AtualizaProgramaConfinamento(idCentro);
                    }
                }
                else
                {
                    throw new Exception(String.Concat("Informe um programa ou benefício para este serviço.", System.Environment.NewLine));
                }

            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

        }
        private void AdicionarProgramaConfinamento(int idCentro, int IdServicosRecursosFinanceirosCentroPOP, int idPrograma)
        {
            var item1 = ProgramaProjetoCofinanciamentoExercicio.Where(x => x.IdProgramaProjeto == idPrograma).SingleOrDefault();

            var obj = new ProgramaProjetoCofinanciamentoInfo();
            obj.IdProgramaProjeto = Convert.ToInt32(item1.IdProgramaProjeto);
            obj.IdServicosRecursosFinanceirosCentroPOP = IdServicosRecursosFinanceirosCentroPOP;
            obj.NumeroUsuarios = item1.NumeroUsuarios;
            using (var proxy = new ProxyProgramas())
            {
                bool programaExistente = false;
                var programaprojeto = proxy.Service.GetProgramaProjetoById(Convert.ToInt32(obj.IdProgramaProjeto));
                if (programaprojeto != null)
                {
                    if (programaprojeto.IdPrefeitura == SessaoPmas.UsuarioLogado.Prefeitura.Id)
                    {
                        proxy.Service.AddProgramaProjetoCofinanciamento(obj);
                        CarregarProgramas(proxy, obj.IdServicosRecursosFinanceirosCentroPOP.Value, idCentro);
                    }
                }
                var idServicoBeneficio = IdServicosRecursosFinanceirosCentroPOP;

                using (var ProxyProgramas = new ProxyProgramas())
                {
                    var programas = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(obj.IdServicosRecursosFinanceirosCentroPOP.Value, idCentro);

                    if (programas != null)
                    {
                        var programasSelecionados = programas.OrderBy(t => t.IdTipoProtecao)
                                    .GroupBy(s => s.ProtecaoSocial)
                                    .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) })
                                    .ToList();

                        foreach (var programa in programasSelecionados)
                        {
                            foreach (var item in programa.Items)
                            {
                                if (item.IdProgramaProjeto == obj.IdProgramaProjeto)
                                {
                                    programaExistente = true;
                                }
                            }
                        }
                    }
                }

                if (!programaExistente)
                {
                    var transferenciaRendaCofinanciamento = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

                    transferenciaRendaCofinanciamento.IdTransferenciaRenda = Convert.ToInt32(obj.IdProgramaProjeto);
                    transferenciaRendaCofinanciamento.IdServicosRecursosFinanceirosCentroPOP = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                    transferenciaRendaCofinanciamento.NumeroUsuarios = Convert.ToInt32(item1.NumeroUsuarios);

                    if (trRendaCidadaBeneficioIdoso.Visible && transferenciaRendaCofinanciamento.NumeroUsuarios <= 0)
                    {
                        throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                    }
                    using (var proxyProgramas = new ProxyProgramas())
                    {
                        proxyProgramas.Service.AddTransferenciaRendaCofinanciamento(transferenciaRendaCofinanciamento);
                        CarregarProgramas(proxyProgramas, transferenciaRendaCofinanciamento.IdServicosRecursosFinanceirosCentroPOP.Value, idCentro);
                    }
                }
            }




        }
        private void AtualizaProgramaConfinamento(int idCentro)
        {
            var obj = new ProgramaProjetoCofinanciamentoInfo();
            obj.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);

            if (Request.QueryString["id"] != null)
            {
                obj.IdServicosRecursosFinanceirosCentroPOP = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));


                if (!String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
                {
                    obj.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }

                if (trRendaCidadaBeneficioIdoso.Visible && !obj.NumeroUsuarios.HasValue)
                {
                    throw new Exception(String.Concat("Informe o numero de beneficiários deste serviço.", System.Environment.NewLine));
                }
                if (trRendaCidadaBeneficioIdoso.Visible && obj.NumeroUsuarios.Value <= 0)
                {
                    throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                }


                using (var proxy = new ProxyProgramas())
                {
                    bool programaExistente = false;

                    var programaProjeto = proxy.Service.GetProgramaProjetoById(Convert.ToInt32(ddlProgramaBeneficio.SelectedValue));

                    if (programaProjeto != null)
                    {
                        if (programaProjeto.IdPrefeitura == SessaoPmas.UsuarioLogado.Prefeitura.Id)
                        {
                            proxy.Service.AddProgramaProjetoCofinanciamento(obj);
                            CarregarProgramas(proxy, obj.IdServicosRecursosFinanceirosCentroPOP.Value, idCentro);
                        }
                    }
                    var idServicoBeneficio = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                    using (var ProxyProgramas = new ProxyProgramas())
                    {
                        var programaProjetoServicoCofinanciamento = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(obj.IdServicosRecursosFinanceirosCentroPOP.Value, idCentro);
                        if (programaProjetoServicoCofinanciamento != null)
                        {
                            var programasProjetosServicoSelecionado = programaProjetoServicoCofinanciamento.OrderBy(t => t.IdTipoProtecao)
                                        .GroupBy(s => s.ProtecaoSocial)
                                        .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) })
                                        .ToList();

                            foreach (var programaSelecionado in programasProjetosServicoSelecionado)
                            {
                                foreach (var item in programaSelecionado.Items)
                                {
                                    if (item.IdProgramaProjeto == obj.IdProgramaProjeto)
                                    {
                                        programaExistente = true;
                                    }
                                }
                            }
                        }
                    }

                    if (!programaExistente)
                    {
                        var transferenciaRendaCofinanciamento = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

                        transferenciaRendaCofinanciamento.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
                        transferenciaRendaCofinanciamento.IdServicosRecursosFinanceirosCentroPOP = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                        if (!String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
                        {
                            transferenciaRendaCofinanciamento.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                        }
                        else
                        {
                            throw new Exception(String.Concat("Informe o numero de beneficiários deste serviço.", System.Environment.NewLine));
                        }
                        if (trRendaCidadaBeneficioIdoso.Visible && transferenciaRendaCofinanciamento.NumeroUsuarios <= 0)
                        {
                            throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                        }

                        using (var proxyProgramas = new ProxyProgramas())
                        {
                            proxyProgramas.Service.AddTransferenciaRendaCofinanciamento(transferenciaRendaCofinanciamento);
                            CarregarProgramas(proxyProgramas, transferenciaRendaCofinanciamento.IdServicosRecursosFinanceirosCentroPOP.Value, idCentro);
                        }
                    }
                }
            }
            else
            {
                AdicionaListaProgramaProjetoCofinanciamentoInfo();

            }


        }
        void AdicionaListaProgramaProjetoCofinanciamentoInfo()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
            var obj = new ProgramaProjetoCofinanciamentoInfo();
            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();

            ProgramaProjetoCofinanciamentoExercicio = ProgramaProjetoCofinanciamentoExercicio ?? new List<ProgramaProjetoCofinanciamentoInfo>();

            consultaProgramaProjetoServicoCofinanciamentoInfo.Unidade = "ii";
            consultaProgramaProjetoServicoCofinanciamentoInfo.TipoServico = ddlTipoServico.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.NumeroAtendidos = Convert.ToInt32(txtNumeroUsuarios.Text);
            consultaProgramaProjetoServicoCofinanciamentoInfo.Usuario = ddlPublicoAlvo.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Nome = ddlProgramaBeneficio.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Id = rptProgramaTemp.Items.Count + 1;
            consultaProgramaProjetoServicoCofinanciamentoInfo.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.Add(consultaProgramaProjetoServicoCofinanciamentoInfo);
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.ToList();
            rptProgramaTemp.DataBind();
            obj.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            obj.Id = consultaProgramaProjetoServicoCofinanciamentoInfo.Id;
            obj.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            this.ProgramaProjetoCofinanciamentoExercicio.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
            //btnSalvarRecursoPrograma.Visible = lstRecursos.Items.Count > 0;
        }
        private void AtualizaPrefeituraBeneficio(int idCentro)
        {
            if (Request.QueryString["id"] != null)
            {

                var entidadeBeneficio = new PrefeituraBeneficioEventualServicoInfo();
                if (!String.IsNullOrEmpty(txtNumeroUsuarios.Text))
                {
                    entidadeBeneficio.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }

                entidadeBeneficio.IdPrefeituraBeneficioEventual = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
                entidadeBeneficio.IdServicosRecursosFinanceirosCentroPOP = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                if (!String.IsNullOrEmpty(txtNumeroUsuarios.Text))
                {
                    entidadeBeneficio.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }

                new ValidadorServicoBeneficioEventual().Validar(entidadeBeneficio);

                using (var proxy = new ProxyProgramas())
                {
                    proxy.Service.AddBeneficioEventualServico(entidadeBeneficio);
                    CarregarProgramas(proxy, entidadeBeneficio.IdServicosRecursosFinanceirosCentroPOP.Value, idCentro);
                }
            }
            else
            {

                AdicionaListaPrefeituraBeneficio();


            }
        }
        void AdicionaListaPrefeituraBeneficio()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();
            PrefeituraBeneficioEventualServicoExercicio = PrefeituraBeneficioEventualServicoExercicio ?? new List<PrefeituraBeneficioEventualServicoInfo>();


            consultaProgramaProjetoServicoCofinanciamentoInfo.Unidade = "ii";
            consultaProgramaProjetoServicoCofinanciamentoInfo.TipoServico = ddlTipoServico.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.NumeroAtendidos = Convert.ToInt32(txtNumeroUsuarios.Text);
            consultaProgramaProjetoServicoCofinanciamentoInfo.Usuario = ddlPublicoAlvo.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Nome = ddlProgramaBeneficio.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Id = rptProgramaTemp.Items.Count + 1;
            consultaProgramaProjetoServicoCofinanciamentoInfo.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);

            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.Add(consultaProgramaProjetoServicoCofinanciamentoInfo);
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.ToList();
            rptProgramaTemp.DataBind();

            var obj = new PrefeituraBeneficioEventualServicoInfo();
            obj.IdPrefeituraBeneficioEventual = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            obj.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            obj.Id = consultaProgramaProjetoServicoCofinanciamentoInfo.Id;
            this.PrefeituraBeneficioEventualServicoExercicio.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";

        }
        private void AdicionaPrefeituraBeneficio(int idCentro, int IdServicosRecursosFinanceirosCentroPOP, int idPrograma)
        {

            var item2 = PrefeituraBeneficioEventualServicoExercicio.Where(x => x.IdPrefeituraBeneficioEventual == idPrograma).SingleOrDefault();

            var entidadeBeneficio = new PrefeituraBeneficioEventualServicoInfo();

            entidadeBeneficio.NumeroBeneficiarios = Convert.ToInt32(item2.NumeroBeneficiarios);


            entidadeBeneficio.IdPrefeituraBeneficioEventual = item2.IdPrefeituraBeneficioEventual;
            entidadeBeneficio.IdServicosRecursosFinanceirosCentroPOP = IdServicosRecursosFinanceirosCentroPOP;


            new ValidadorServicoBeneficioEventual().Validar(entidadeBeneficio);

            using (var proxy = new ProxyProgramas())
            {
                proxy.Service.AddBeneficioEventualServico(entidadeBeneficio);
                CarregarProgramas(proxy, entidadeBeneficio.IdServicosRecursosFinanceirosCentroPOP.Value, idCentro);
            }
        }


        private void AtualizaTransferenciaRenda(int idCentro)
        {

            if (Request.QueryString["id"] != null)
            {

                var entidadeTransferenciaRenda = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

                entidadeTransferenciaRenda.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
                entidadeTransferenciaRenda.IdServicosRecursosFinanceirosCentroPOP = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                if (!String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
                {
                    entidadeTransferenciaRenda.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }
                else
                {
                    throw new Exception(String.Concat("Informe o numero de beneficiários deste serviço.", System.Environment.NewLine));
                }
                if (trRendaCidadaBeneficioIdoso.Visible && entidadeTransferenciaRenda.NumeroUsuarios <= 0)
                {
                    throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                }
                // var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
                using (var proxy = new ProxyProgramas())
                {
                    proxy.Service.AddTransferenciaRendaCofinanciamento(entidadeTransferenciaRenda);
                    CarregarProgramas(proxy, entidadeTransferenciaRenda.IdServicosRecursosFinanceirosCentroPOP.Value, idCentro);
                }
            }
            else
            {

                AdicionaListaTransferenciaRenda();


            }
        }
        private void AdicionaTransferenciaRenda(int idCentro, int IdServicosRecursosFinanceirosCentroPOP, int idPrograma)
        {

            var item2 = TransferenciaRendaCofinanciamentoExercicio.Where(x => x.IdTransferenciaRenda == idPrograma).SingleOrDefault();

            var entidadeTransferenciaRenda = new ServicoRecursoFinanceiroTransferenciaRendaInfo();
            entidadeTransferenciaRenda.IdTransferenciaRenda = item2.IdTransferenciaRenda;
            entidadeTransferenciaRenda.IdServicosRecursosFinanceirosCentroPOP = IdServicosRecursosFinanceirosCentroPOP;
            entidadeTransferenciaRenda.NumeroUsuarios = Convert.ToInt32(item2.NumeroUsuarios);

            if (trRendaCidadaBeneficioIdoso.Visible && entidadeTransferenciaRenda.NumeroUsuarios <= 0)
            {
                throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
            }

            using (var proxy = new ProxyProgramas())
            {
                proxy.Service.AddTransferenciaRendaCofinanciamento(entidadeTransferenciaRenda);
                CarregarProgramas(proxy, entidadeTransferenciaRenda.IdServicosRecursosFinanceirosCentroPOP.Value, idCentro);
            }



        }
        void AdicionaListaTransferenciaRenda()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();
            TransferenciaRendaCofinanciamentoExercicio = TransferenciaRendaCofinanciamentoExercicio ?? new List<ServicoRecursoFinanceiroTransferenciaRendaInfo>();


            consultaProgramaProjetoServicoCofinanciamentoInfo.Unidade = "ii";
            consultaProgramaProjetoServicoCofinanciamentoInfo.TipoServico = ddlTipoServico.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.NumeroAtendidos = Convert.ToInt32(txtNumeroUsuarios.Text);
            consultaProgramaProjetoServicoCofinanciamentoInfo.Usuario = ddlPublicoAlvo.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Nome = ddlProgramaBeneficio.SelectedItem.Text;
            consultaProgramaProjetoServicoCofinanciamentoInfo.Id = rptProgramaTemp.Items.Count + 1;
            consultaProgramaProjetoServicoCofinanciamentoInfo.IdProgramaProjeto = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);

            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.Add(consultaProgramaProjetoServicoCofinanciamentoInfo);
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.ToList();
            rptProgramaTemp.DataBind();

            var obj = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

            obj.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            obj.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            obj.Id = consultaProgramaProjetoServicoCofinanciamentoInfo.Id;
            this.TransferenciaRendaCofinanciamentoExercicio.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";

        }

        protected string MontarBotao(ConsultaProgramaProjetoServicoCofinanciamentoInfo item)
        {
            var idProjeto = item.IdServicoRecursoFinanceiro;
            var page = String.Empty;
            switch (item.TipoUnidade)
            {
                case "Rede direta": page = "../BlocoIII/VServicoRecursoFinanceiroPublico.aspx"; break;
                case "Rede indireta": page = "../BlocoIII/VServicoRecursoFinanceiroPrivado.aspx"; break;
                case "CRAS": page = "../BlocoIII/VServicoRecursoFinanceiroCRAS.aspx"; break;
                case "CREAS": page = "../BlocoIII/VServicoRecursoFinanceiroCREAS.aspx"; break;
                case "Centro Pop": page = "../BlocoIII/VServicoRecursoFinanceiroCentroPOP.aspx"; break;
            }
            return "<a href='" + page + "?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.IdServicoRecursoFinanceiro.ToString())) + "&idProjeto=" + idProjeto + "'><img src='../Styles/Icones/find.png' alt='Visualizar' border='0' /></a>";
        }

        protected void ddlProgramaBeneficio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgramaBeneficio.SelectedValue != "0" && ddlProgramaBeneficio.SelectedItem.Text.ToLower().Contains("amigo do idoso"))
            {
                lblBenificiarios.Text = "Quantos beneficiários do Renda Cidadã - Benefício Idoso são usuários deste serviço?";
                trRendaCidadaBeneficioIdoso.Visible = true;
            }
            else
            {
                trRendaCidadaBeneficioIdoso.Visible = true;
                lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
            }
        }

        protected void lstItems_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Permissao.VerificarPermissaoControles(new[] { ((ImageButton)e.Item.FindControl("btnExcluir")) }, Session);
            }
        }

        protected void lstRecursos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var idServicosRecursosFinanceiros = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
            if (e.CommandName == "Excluir")
            {
                using (var proxy = new ProxyProgramas())
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string id = commandArgs[0];
                    string tipoCofinanciamento = commandArgs[1];
                    proxy.Service.DeleteProgramaProjetoCofinanciamento(Convert.ToInt32(id), Convert.ToInt32(tipoCofinanciamento));
                    CarregarProgramas(proxy, idServicosRecursosFinanceiros, idCentro);
                }
            }
        }
        protected void btnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            int idPrograma = Convert.ToInt32(btn.CommandArgument);
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.RemoveAll(x => x.Id == idPrograma);
            if (TransferenciaRendaCofinanciamentoExercicio != null)
            {
                var transferencia = TransferenciaRendaCofinanciamentoExercicio.Where(x => x.Id == idPrograma);
                if (transferencia != null)
                {
                    TransferenciaRendaCofinanciamentoExercicio.RemoveAll(x => x.Id == idPrograma);
                }
            }
            if (PrefeituraBeneficioEventualServicoExercicio != null)
            {
                var prefeitura = PrefeituraBeneficioEventualServicoExercicio.Where(x => x.Id == idPrograma);
                if (prefeitura != null)
                {
                    PrefeituraBeneficioEventualServicoExercicio.RemoveAll(x => x.Id == idPrograma);
                }
            }
            if (ProgramaProjetoCofinanciamentoExercicio != null)
            {
                var programa = ProgramaProjetoCofinanciamentoExercicio.Where(x => x.Id == idPrograma);
                if (programa != null)
                {
                    ProgramaProjetoCofinanciamentoExercicio.RemoveAll(x => x.Id == idPrograma);
                }
            }
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.ToList();
            rptProgramaTemp.DataBind();
        }




        #region Recursos Financeiros - Métodos
        #region Exercicio 1
        protected void btnAdicionarRecursoExercicio1_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo();
            recurso.NomeFonteRecurso = txtNomeRecursoExercicio1.Text;
            recurso.ValorFonteRecurso = String.IsNullOrEmpty(txtValorRecursoExercicio1.Text) ? 0M : Convert.ToDecimal(txtValorRecursoExercicio1.Text);
            recurso.Liberado = RetornaValidacaoBloqueioDesbloqueio(2022);

            frame1_5.Attributes.Add("class", "active");
            try
            {
                new ValidadorServicosFinanceirosFonteRecursos().Validar(recurso);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            FontesRecursosExercicio1 = FontesRecursosExercicio1 ?? new List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo>();
            FontesRecursosExercicio1.Add(recurso);

            carregarRecursosFinanceirosFonteRecursosExercicio1();

            txtNomeRecursoExercicio1.Text = String.Empty;
            txtValorRecursoExercicio1.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio1.Visible = true;

        }
        protected void rblOutrasFontesExercicio1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio1.SelectedValue == "0")
            {
                txtValorRecursoExercicio1.Text = "0,00";
                txtNomeRecursoExercicio1.Text = String.Empty;
                FontesRecursosExercicio1 = null;
                lstRecursosAdicionadosExercicio1.DataSource = FontesRecursosExercicio1;
                lstRecursosAdicionadosExercicio1.DataBind();
                lstRecursosAdicionadosExercicio1.Visible = false;
            }
            trAddRecursoExercicio1.Visible = trMotivoEstadualizadoExercicio1.Visible = rblOutrasFontesExercicio1.SelectedValue == "1";
        }
        protected void lstRecursosAdicionadosExercicio1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequenciaExercicio1")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void lstRecursosAdicionadosExercicio1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    FontesRecursosExercicio1.RemoveAt(e.Item.DataItemIndex);
                    carregarRecursosFinanceirosFonteRecursosExercicio1();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Exercicio 2
        protected void btnAdicionarRecursoExercicio2_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo();
            recurso.NomeFonteRecurso = txtNomeRecursoExercicio2.Text;
            recurso.ValorFonteRecurso = String.IsNullOrEmpty(txtValorRecursoExercicio2.Text) ? 0M : Convert.ToDecimal(txtValorRecursoExercicio2.Text);
            recurso.Liberado = RetornaValidacaoBloqueioDesbloqueio(2023);

            CarregarAbaInicialRecursosFinanceiros();
            try
            {
                new ValidadorServicosFinanceirosFonteRecursos().Validar(recurso);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            FontesRecursosExercicio2 = FontesRecursosExercicio2 ?? new List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo>();
            FontesRecursosExercicio2.Add(recurso);

            carregarRecursosFinanceirosFonteRecursosExercicio2();

            txtNomeRecursoExercicio2.Text = String.Empty;
            txtValorRecursoExercicio2.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio2.Visible = true;
            ValidaBloqueioDesbloqueio();

        }
        protected void rblOutrasFontesExercicio2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio2.SelectedValue == "0")
            {
                txtValorRecursoExercicio2.Text = "0,00";
                txtNomeRecursoExercicio2.Text = String.Empty;
                FontesRecursosExercicio2 = null;
                lstRecursosAdicionadosExercicio2.DataSource = FontesRecursosExercicio2;
                lstRecursosAdicionadosExercicio2.DataBind();
                lstRecursosAdicionadosExercicio2.Visible = false;
            }
            trAddRecursoExercicio2.Visible = trMotivoEstadualizadoExercicio2.Visible = rblOutrasFontesExercicio2.SelectedValue == "1";
            ValidaBloqueioDesbloqueio();
        }
        protected void lstRecursosAdicionadosExercicio2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequenciaExercicio2")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void lstRecursosAdicionadosExercicio2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    FontesRecursosExercicio2.RemoveAt(e.Item.DataItemIndex);
                    carregarRecursosFinanceirosFonteRecursosExercicio2();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        #endregion


        #region Exercicio 3
        protected void btnAdicionarRecursoExercicio3_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo();
            recurso.NomeFonteRecurso = txtNomeRecursoExercicio3.Text;
            recurso.ValorFonteRecurso = String.IsNullOrEmpty(txtValorRecursoExercicio3.Text) ? 0M : Convert.ToDecimal(txtValorRecursoExercicio3.Text);
            recurso.Liberado = RetornaValidacaoBloqueioDesbloqueio(2024);

            int idServico = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            CarregarAbaInicialRecursosFinanceiros();
                        
            try
            {
                new ValidadorServicosFinanceirosFonteRecursos().Validar(recurso);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            FontesRecursosExercicio3 = FontesRecursosExercicio3 ?? new List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo>();
            FontesRecursosExercicio3.Add(recurso);

            carregarRecursosFinanceirosFonteRecursosExercicio3();

            txtNomeRecursoExercicio3.Text = String.Empty;
            txtValorRecursoExercicio3.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio3.Visible = true;
            ValidaBloqueioDesbloqueio();

        }



        protected void rblOutrasFontesExercicio3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio3.SelectedValue == "0")
            {
                txtValorRecursoExercicio3.Text = "0,00";
                txtNomeRecursoExercicio3.Text = String.Empty;
                FontesRecursosExercicio3 = null;
                lstRecursosAdicionadosExercicio3.DataSource = FontesRecursosExercicio3;
                lstRecursosAdicionadosExercicio3.DataBind();
                lstRecursosAdicionadosExercicio3.Visible = false;
            }
            trAddRecursoExercicio3.Visible = trMotivoEstadualizadoExercicio3.Visible = rblOutrasFontesExercicio3.SelectedValue == "1";
            ValidaBloqueioDesbloqueio();
        }
        protected void lstRecursosAdicionadosExercicio3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequenciaExercicio3")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void lstRecursosAdicionadosExercicio3_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    FontesRecursosExercicio3.RemoveAt(e.Item.DataItemIndex);
                    carregarRecursosFinanceirosFonteRecursosExercicio3();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        #endregion


        #region Exercicio 4
        protected void btnAdicionarRecursoExercicio4_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo();
            recurso.NomeFonteRecurso = txtNomeRecursoExercicio4.Text;
            recurso.ValorFonteRecurso = String.IsNullOrEmpty(txtValorRecursoExercicio4.Text) ? 0M : Convert.ToDecimal(txtValorRecursoExercicio4.Text);
            recurso.Liberado = RetornaValidacaoBloqueioDesbloqueio(2025);

            CarregarAbaInicialRecursosFinanceiros();
            try
            {
                new ValidadorServicosFinanceirosFonteRecursos().Validar(recurso);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            FontesRecursosExercicio4 = FontesRecursosExercicio4 ?? new List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo>();
            FontesRecursosExercicio4.Add(recurso);

            carregarRecursosFinanceirosFonteRecursosExercicio4();

            txtNomeRecursoExercicio4.Text = String.Empty;
            txtValorRecursoExercicio4.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio4.Visible = true;
            ValidaBloqueioDesbloqueio();

        }
        protected void rblOutrasFontesExercicio4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio4.SelectedValue == "0")
            {
                txtValorRecursoExercicio4.Text = "0,00";
                txtNomeRecursoExercicio4.Text = String.Empty;
                FontesRecursosExercicio4 = null;
                lstRecursosAdicionadosExercicio4.DataSource = FontesRecursosExercicio4;
                lstRecursosAdicionadosExercicio4.DataBind();
                lstRecursosAdicionadosExercicio4.Visible = false;
            }
            trAddRecursoExercicio4.Visible = trMotivoEstadualizadoExercicio4.Visible = rblOutrasFontesExercicio4.SelectedValue == "1";
            ValidaBloqueioDesbloqueio();
        }
        protected void lstRecursosAdicionadosExercicio4_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequenciaExercicio4")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void lstRecursosAdicionadosExercicio4_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    FontesRecursosExercicio4.RemoveAt(e.Item.DataItemIndex);
                    carregarRecursosFinanceirosFonteRecursosExercicio2();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        #endregion


        #endregion

        #region helper
        private void ClearSituacoes()
        {
            lstSituacoesEspecificas.DataTextField = "Nome";
            lstSituacoesEspecificas.DataValueField = "Id";
            lstSituacoesEspecificas.DataSource = new List<SituacaoEspecificaInfo>();
            lstSituacoesEspecificas.DataBind();
        }
        private void CarregarAbaInicialRecursosFinanceiros()
        {
            hdnExercicio.Value = (hdnExercicio.Value == string.Empty) ? FServicoRecursoFinanceiroCentroPOP.Exercicios[0].ToString() : hdnExercicio.Value;
            frame1_5.Attributes.Add("class", "active");

            if (hdnExercicio.Value == Exercicios[0].ToString())
            {
                frame1_5_Ano1.Attributes.Add("class", "active");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Remove("class");
            }
            else if (hdnExercicio.Value == Exercicios[1].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Add("class", "active");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Remove("class");
            }
            else if (hdnExercicio.Value == Exercicios[2].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Add("class", "active");
                frame1_5_Ano4.Attributes.Remove("class");
            }
            else if (hdnExercicio.Value == Exercicios[3].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Add("class", "active");
            }
        }
        private WebControl[] SelecionarControlesRecursosFinanceirosBloqueioExercicio1()
        {
            WebControl[] controlesExercicio1 = {
                                         txtFMASExercicio1,
                                         txtFMDCAExercicio1,
                                         txtFMIExercicio1,
                                         txtFEASExercicio1,
                                         txtFEDCAExercicio1, 
                                         txtFEIExercicio1,
                                         txtFNASExercicio1,
                                         txtFNDCAExercicio1,
                                         txtFNIExercicio1,
                                         rblOutrasFontesExercicio1,
                                         txtNomeRecursoExercicio1,
                                         txtValorRecursoExercicio1,
                                         btnAdicionarRecursoExercicio1,
 
                                         #region Funcionamento
		                                 txtCapacidadeExercicio1,
                                         txtMediaMensalExercicio1
                                         #endregion
                                         };
            return controlesExercicio1;
        }
        private WebControl[] SelecionarControlesRecursosFinanceirosBloqueioExercicio2()
        {

            WebControl[] controlesExercicio2 = {
                                         txtFMASExercicio2,
                                         txtFMDCAExercicio2,
                                         txtFMIExercicio2,
                                         txtFEASExercicio2,
                                         txtFEDCAExercicio2, 
                                         txtFEIExercicio2,
                                         txtFNASExercicio2,
                                         txtFNDCAExercicio2,
                                         txtFNIExercicio2,
                                         rblOutrasFontesExercicio2,
                                         txtNomeRecursoExercicio2,
                                         txtValorRecursoExercicio2,
                                         btnAdicionarRecursoExercicio2,
                                         
                                         #region Funcionamento
		                                 txtCapacidadeExercicio2,
                                         txtMediaMensalExercicio2
                                         #endregion
                                         };
            return controlesExercicio2;
        }
        private WebControl[] SelecionarControlesRecursosFinanceirosBloqueioExercicio3()
        {

            WebControl[] controlesExercicio3 = {
                                         txtFMASExercicio3,
                                         txtFMDCAExercicio3,
                                         txtFMIExercicio3,
                                         txtFEASExercicio3,
                                         txtFEDCAExercicio3, 
                                         txtFEIExercicio3,
                                         txtFNASExercicio3,
                                         txtFNDCAExercicio3,
                                         txtFNIExercicio3,
                                         rblOutrasFontesExercicio3,
                                         txtNomeRecursoExercicio3,
                                         txtValorRecursoExercicio3,
                                         btnAdicionarRecursoExercicio3,
                                         
                                         #region Funcionamento
		                                 txtCapacidadeExercicio3,
                                         txtMediaMensalExercicio3
                                         #endregion
                                         };
            return controlesExercicio3;
        }
        private WebControl[] SelecionarControlesRecursosFinanceirosBloqueioExercicio4()
        {

            WebControl[] controlesExercicio4 = {
                                         txtFMASExercicio4,
                                         txtFMDCAExercicio4,
                                         txtFMIExercicio4,
                                         txtFEASExercicio4,
                                         txtFEDCAExercicio4, 
                                         txtFEIExercicio4,
                                         txtFNASExercicio4,
                                         txtFNDCAExercicio4,
                                         txtFNIExercicio4,
                                         rblOutrasFontesExercicio4,
                                         txtNomeRecursoExercicio4,
                                         txtValorRecursoExercicio4,
                                         btnAdicionarRecursoExercicio4,
                                         
                                         #region Funcionamento
		                                 txtCapacidadeExercicio4,
                                         txtMediaMensalExercicio4
                                         #endregion
                                         };
            return controlesExercicio4;
        }
        private WebControl[] SelecionarControlesReprogramacaoBloqueioExercicio1()
        {
            WebControl[] controlesExercicio1 = {
                                                   txtFEASAnoAnteriorExercicio1,
                                                   txtFEASReprogramacaoDemandasParlamentaresExercicio1
                                               };
            return controlesExercicio1;
        }
        private WebControl[] SelecionarControlesReprogramacaoBloqueioExercicio2()
        {
            WebControl[] controlesExercicio2 = {
                                                   txtFEASAnoAnteriorExercicio2,
                                                   txtFEASReprogramacaoDemandasParlamentaresExercicio2
                                               };
            return controlesExercicio2;

        }
        private WebControl[] SelecionarControlesReprogramacaoBloqueioExercicio3()
        {
            WebControl[] controlesExercicio3 = {
                                                   txtFEASAnoAnteriorExercicio3,
                                                   txtFEASReprogramacaoDemandasParlamentaresExercicio3
                                               };
            return controlesExercicio3;

        }
        private WebControl[] SelecionarControlesReprogramacaoBloqueioExercicio4()
        {
            WebControl[] controlesExercicio4 = {
                                                   txtFEASAnoAnteriorExercicio4,
                                                   txtFEASReprogramacaoDemandasParlamentaresExercicio4
                                               };
            return controlesExercicio4;

        }

        private WebControl[] SelecionarControlesDemandasExercicio1()
        {
            WebControl[] controlesExercicio1 = {
                                                   txtFEASDemandasExercicio1,
                                                   txtCodigoDemandaExercicio1,
                                                   txtValorContraExercicio1,
                                                   txtObjetoDemandaExercicio1,
                                                   rblContraPartida1
                                               };
            return controlesExercicio1;
        }

        private WebControl[] SelecionarControlesDemandasExercicio2()
        {
            WebControl[] controlesExercicio2 = {
                                                   txtFEASDemandasExercicio2,
                                                   txtCodigoDemandaExercicio2,
                                                   txtValorContraExercicio2,
                                                   txtObjetoDemandaExercicio2,
                                                   rblContraPartida2
                                               };
            return controlesExercicio2;
        }

        private WebControl[] SelecionarControlesDemandasExercicio3()
        {
            WebControl[] controlesExercicio3 = {
                                                   txtFEASDemandasExercicio3,
                                                   txtCodigoDemandaExercicio3,
                                                   txtValorContraExercicio3,
                                                   txtObjetoDemandaExercicio3,
                                                   rblContraPartida3
                                               };
            return controlesExercicio3;
        }

        private WebControl[] SelecionarControlesDemandasExercicio4()
        {
            WebControl[] controlesExercicio4 = {
                                                   txtFEASDemandasExercicio4,
                                                   txtCodigoDemandaExercicio4,
                                                   txtValorContraExercicio4,
                                                   txtObjetoDemandaExercicio4,
                                                   rblContraPartida4
                                               };
            return controlesExercicio4;
        }

        private void AposSalvoNaoPermitirEdicaoCamposCaracterizacao(ServicoRecursoFinanceiroCentroPOPInfo servico)
        {
            if (servico.Id != 0)
            {
                rblTipoProtecao.Enabled = false;
                ddlTipoServico.Enabled = false;
                ddlPublicoAlvo.Enabled = false;
            }
            else
            {
                rblTipoProtecao.Enabled = true;
                ddlTipoServico.Enabled = true;
                ddlPublicoAlvo.Enabled = true;
            }
        }

        #endregion

        #region bloqueio e desbloqueio

        private void AplicarRegraBloqueioDesbloqueio()
        {
            #region Seleciona: Campos Recursos financeiros
            WebControl[] controles1 = SelecionarControlesRecursosFinanceirosBloqueioExercicio1();
            WebControl[] controles2 = SelecionarControlesRecursosFinanceirosBloqueioExercicio2();
            WebControl[] controles3 = SelecionarControlesRecursosFinanceirosBloqueioExercicio3();
            WebControl[] controles4 = SelecionarControlesRecursosFinanceirosBloqueioExercicio4();
            #endregion

            #region Seleciona: Campos Reprogramacao
            WebControl[] controlesReprogramacaoExercicio1 = SelecionarControlesReprogramacaoBloqueioExercicio1();
            WebControl[] controlesReprogramacaoExercicio2 = SelecionarControlesReprogramacaoBloqueioExercicio2();
            WebControl[] controlesReprogramacaoExercicio3 = SelecionarControlesReprogramacaoBloqueioExercicio3();
            WebControl[] controlesReprogramacaoExercicio4 = SelecionarControlesReprogramacaoBloqueioExercicio4();
            #endregion

            #region Seleciona: Campos Demandas
            WebControl[] controlesDemandasExercicio1 = SelecionarControlesDemandasExercicio1();
            WebControl[] controlesDemandasExercicio2 = SelecionarControlesDemandasExercicio2();
            WebControl[] controlesDemandasExercicio3 = SelecionarControlesDemandasExercicio3();
            WebControl[] controlesDemandasExercicio4 = SelecionarControlesDemandasExercicio4();
            #endregion 

            #region Regra: Bloqueio: Campos Recursos Financeiros
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroCentroPOP.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroCentroPOP.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroCentroPOP.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroCentroPOP.Exercicios[3]);
            #endregion
            #region Regra: Bloqueio: Campos Reprogramacao
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio1, FServicoRecursoFinanceiroCentroPOP.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio2, FServicoRecursoFinanceiroCentroPOP.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio3, FServicoRecursoFinanceiroCentroPOP.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio4, FServicoRecursoFinanceiroCentroPOP.Exercicios[3]);

            #endregion
            #region Regra: Bloqueio: Campos Demandas
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio1, FServicoRecursoFinanceiroCentroPOP.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio2, FServicoRecursoFinanceiroCentroPOP.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio3, FServicoRecursoFinanceiroCentroPOP.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandasExercicio4, FServicoRecursoFinanceiroCentroPOP.Exercicios[3]);
            #endregion 

            #region Regra: Bloqueio: Botao Salvar
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio1, FServicoRecursoFinanceiroCentroPOP.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio2, FServicoRecursoFinanceiroCentroPOP.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio3, FServicoRecursoFinanceiroCentroPOP.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoSalvar(btnSalvarExercicio4, FServicoRecursoFinanceiroCentroPOP.Exercicios[3]);
            #endregion
        }

        #endregion

        protected void ddlAbrangencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAbrangencia.SelectedValue == "4")
            {
                trSedeServico.Visible = true;
                trFormaJuridica.Visible = true;
            }
            else
            {
                trSedeServico.Visible = false;
                trFormaJuridica.Visible = false;
                trConsorcioPublico.Visible = false;
            }
        }

        protected void rblAbrangencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAbrangencia.SelectedValue == "1")
            {
                trMunicipioParticipaOferta.Visible = true;
                trMunicipioSede.Visible = false;
            }
            else
            {
                trMunicipioParticipaOferta.Visible = false;
                trMunicipioSede.Visible = true;
            }
        }

        protected void ddlFormaJuridica_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormaJuridica.SelectedValue == "1")
            {
                trConsorcioPublico.Visible = true;
            }
            else
            {
                trConsorcioPublico.Visible = false;
            }
        }

        protected void rblContraPartida1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Add("class", "active");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Remove("class");

            if (rblContraPartida1.SelectedValue == "1")
            {
                trValorContraExercicio1.Visible = true;
            }
            else
            {
                trValorContraExercicio1.Visible = false;
            }
        }

        protected void rblContraPartida2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Add("class", "active");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Remove("class");

            if (rblContraPartida2.SelectedValue == "1")
            {
                trValorContraExercicio2.Visible = true;
            }
            else
            {
                trValorContraExercicio2.Visible = false;
            }
        }

        protected void rblContraPartida3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Add("class", "active");
            frame1_5_Ano4.Attributes.Remove("class");

            if (rblContraPartida3.SelectedValue == "1")
            {
                trValorContraExercicio3.Visible = true;
            }
            else
            {
                trValorContraExercicio3.Visible = false;
            }
        }

        protected void rblContraPartida4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Add("class", "active");

            if (rblContraPartida4.SelectedValue == "1")
            {
                trValorContraExercicio4.Visible = true;
            }
            else
            {
                trValorContraExercicio4.Visible = false;
            }		
        }

    }
}

