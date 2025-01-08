using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Resource;
using Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FServicoRecursoFinanceiroPrivado : System.Web.UI.Page
    {

        #region propriedades
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        private static int exercicioDesbloqueado;
        #endregion

        #region sessao
        protected List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> SessaoFontesRecursosExercicio1
        {
            get { return Session["FONTES_RECURSOS_PRIVADO_EXERCICIO1"] as List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_PRIVADO_EXERCICIO1"] = value; }
        }
        protected List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> SessaoFontesRecursosExercicio2
        {
            get { return Session["FONTES_RECURSOS_PRIVADO_EXERCICIO2"] as List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_PRIVADO_EXERCICIO2"] = value; }
        }

        protected List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> SessaoFontesRecursosExercicio3
        {
            get { return Session["FONTES_RECURSOS_PRIVADO_EXERCICIO3"] as List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_PRIVADO_EXERCICIO3"] = value; }
        }

        protected List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> SessaoFontesRecursosExercicio4
        {
            get { return Session["FONTES_RECURSOS_PRIVADO_EXERCICIO4"] as List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>; }
            set { Session["FONTES_RECURSOS_PRIVADO_EXERCICIO4"] = value; }
        }




        protected List<ProgramaProjetoCofinanciamentoInfo> SessaoProgramaProjetoCofinanciamentoExercicio
        {
            get { return Session["PROGRAMA_PROJETO_CO_FINANCIAMENTOINFO_CRAS1"] as List<ProgramaProjetoCofinanciamentoInfo>; }
            set { Session["PROGRAMA_PROJETO_CO_FINANCIAMENTOINFO_CRAS1"] = value; }
        }
        protected List<PrefeituraBeneficioEventualServicoInfo> SessaoPrefeituraBeneficioEventualServicoExercicio
        {
            get { return Session["PREFEITURA_BENEFICIO_EVENTUALSERVICOINFO_CRAS1"] as List<PrefeituraBeneficioEventualServicoInfo>; }
            set { Session["PREFEITURA_BENEFICIO_EVENTUALSERVICOINFO_CRAS1"] = value; }
        }
        protected List<ServicoRecursoFinanceiroTransferenciaRendaInfo> SessaoTransferenciaRendaCofinanciamentoExercicio
        {
            get { return Session["TRANSFERENCIA_RENDA_CO_FINANCIAMENTOINFO_CRAS1"] as List<ServicoRecursoFinanceiroTransferenciaRendaInfo>; }
            set { Session["TRANSFERENCIA_RENDA_CO_FINANCIAMENTOINFO_CRAS1"] = value; }
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

                hdnExercicio.Value = String.IsNullOrEmpty(hdnExercicio.Value) ? FServicoRecursoFinanceiroPrivado.Exercicios[0].ToString() : hdnExercicio.Value;

                #region Validar: [Se Existe usuario com Prefeitura]
                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                #endregion

                #region Validar: [Se Existe Local Execucao]
                if (String.IsNullOrEmpty(Request.QueryString["idLocal"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPrivadas.aspx");
                    return;
                }
                #endregion

                #region Exibe: [Mensagem após salvar | atualizar]

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "A")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço atualizado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "I")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço registrado com sucesso! <br/>"), true);
                }

                #endregion

                #region Carregamento
                this.CamposBindEventos();
                this.CarregarCamposValores();
                this.DefinirFrame1ComoPrincipal();
                ValidaBloqueioDesbloqueio();
                #endregion

                this.ClearSessao();
                this.ClearPrograma();
            }
        }

        protected void CarregarProgramas(ProxyProgramas proxy, int idServicosRecursosFinanceiros, int idLocal)
        {
            var programasProjetosCofinanciamento =
                        proxy.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(idServicosRecursosFinanceiros, idLocal)
                                   .OrderBy(t => t.IdTipoProtecao)
                                   .GroupBy(s => s.ProtecaoSocial)
                                   .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) })
                                   .ToList();

            if (programasProjetosCofinanciamento != null && programasProjetosCofinanciamento.Count() > 0)
            {
                trProgramasBeneficios.Visible = true;
                rblIntegracaoRede.SelectedValue = "1";
            }
            else
            {
                trProgramasBeneficios.Visible = false;
                rblIntegracaoRede.SelectedValue = "0";
            }

            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
            lstRecursos.DataSource = programasProjetosCofinanciamento;
            lstRecursos.DataBind();
            btnSalvarRecursoPrograma.Visible = lstRecursos.Items.Count > 0;
        }

        private void ValidaBloqueioDesbloqueio()
        {
            WebControl[] controles1 = SelecionarControlesRecursosFinanceirosBloqueioExercicio1();
            WebControl[] controles2 = SelecionarControlesRecursosFinanceirosBloqueioExercicio2();
            WebControl[] controles3 = SelecionarControlesRecursosFinanceirosBloqueioExercicio3();
            WebControl[] controles4 = SelecionarControlesRecursosFinanceirosBloqueioExercicio4();

            var validaBloqueio2022 = Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroPrivado.Exercicios[0]);
            var validaBloqueio2023 = Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroPrivado.Exercicios[1]);
            var validaBloqueio2024 = Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroPrivado.Exercicios[2]);
            var validaBloqueio2025 = Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroPrivado.Exercicios[3]);

            if (validaBloqueio2022 == true)
            {
                exercicioDesbloqueado = 2022;
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio1()", "exibeBtnExcluirExercicio1();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio1()", "ocultaBtnExcluirExercicio1();", true);
            }

            if (validaBloqueio2023 == true)
            {
                exercicioDesbloqueado = 2023;
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio2()", "exibeBtnExcluirExercicio2();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio2()", "ocultaBtnExcluirExercicio2();", true);
            }

            if (validaBloqueio2024 == true)
            {
                exercicioDesbloqueado = 2024;
                ScriptManager.RegisterStartupScript(this, GetType(), "exibeBtnExcluirExercicio3()", "exibeBtnExcluirExercicio3();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaBtnExcluirExercicio3()", "ocultaBtnExcluirExercicio3();", true);
            }

            if (validaBloqueio2025 == true)
            {
                exercicioDesbloqueado = 2025;
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
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroPrivado.Exercicios[0]);
            }

            if (exercicio == 2023)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroPrivado.Exercicios[1]);
            }

            if (exercicio == 2024)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroPrivado.Exercicios[2]);
            }

            if (exercicio == 2025)
            {
                validacao = Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroPrivado.Exercicios[3]);
            }

            return validacao;

        }


        private void CarregarCamposValores()
        {
            using (var proxyEstrutura = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarCombos(proxyEstrutura);
                CarregarMotivosEstadualizadoExercicio1(proxyEstrutura);
                CarregarMotivosEstadualizadoExercicio2(proxyEstrutura);
                CarregarMotivosEstadualizadoExercicio3(proxyEstrutura);
                CarregarMotivosEstadualizadoExercicio4(proxyEstrutura);
                CarregarAvaliacoes(proxyEstrutura);

                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {
                        load(proxy, proxyEstrutura);
                    }
                }
                else
                {
                    this.AplicarRegraExibicaoLayoutServicosChanged();
                    this.AplicarRegraBloqueioDesbloqueio();
                }
            }
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

        #region Carregamento
        protected void load(ProxyRedeProtecaoSocial proxy, ProxyEstruturaAssistenciaSocial proxyEstrutura)
        {
            var servicosRecursos = proxy.Service.GetServicoRecursoFinanceiroPrivadoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var idLocal = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));
            if (servicosRecursos == null)
                return;

            this.CarregarCaracterizacaoServico(proxyEstrutura, servicosRecursos);
            this.CarregarCaracterizacaoUsuarios(servicosRecursos, proxyEstrutura);
            this.CarregarFuncionamento(proxyEstrutura, servicosRecursos);
            this.CarregarRecursosHumanos(proxy, servicosRecursos);
            this.CarregarRecursosFinanceiros(servicosRecursos);
            this.CarregaDemandasParlamentares(servicosRecursos);

            using (var proxyprefeitura = new ProxyPrefeitura())
            {
                var pre = proxyprefeitura.Service.GetPrefeituraById(Convert.ToInt32(SessaoPmas.UsuarioLogado.Prefeitura.Id));
                if (pre.ValoresReprogramadosDrads.HasValue && pre.ValoresReprogramadosDrads.Value == true)
                {
                    trFeasAnterior.Visible = true;
                }
            }

       
            if (servicosRecursos.Id != 0)
            {
                trAssociacaoProgramas.Visible = true;
            }

            using (var proxyProgramas = new ProxyProgramas())
            {
                CarregarComboProgramas(proxyProgramas);
                CarregarProgramas(proxyProgramas, servicosRecursos.Id, idLocal);
            }
     

            chkNaoPossuiTecnicoResponsavel.Checked = servicosRecursos.PossuiTecnicoResponsavel.HasValue
                ? !servicosRecursos.PossuiTecnicoResponsavel.Value : false;

            txtTecnicoResponsavel.Text = chkNaoPossuiTecnicoResponsavel.Checked
                ? string.Empty : !String.IsNullOrWhiteSpace(servicosRecursos.NomeTecnicoResponsavel)
                ? servicosRecursos.NomeTecnicoResponsavel : String.Empty;

            txtTecnicoResponsavel.Enabled = !chkNaoPossuiTecnicoResponsavel.Checked;
        }
        private void CarregarFuncionamento(ProxyEstruturaAssistenciaSocial proxyEstrutura, ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            #region Carrega: [Data de início de funcionamento]
            txtDataInicio.Text = servico.DataFuncionamentoServico.HasValue ? servico.DataFuncionamentoServico.Value.ToShortDateString() : String.Empty;
            #endregion

            CarregarAvaliacoes(proxyEstrutura);

            if (servico.IdTipoServicoNaoTipificado.HasValue)
            {
                CarregarAtividades(proxyEstrutura, true);
            }
            else
            {
                CarregarAtividades(proxyEstrutura);
            }

            #region Carregar: Atividades Socioassistenciais
            if (servico.AtividadesSocioAssistenciais != null && servico.AtividadesSocioAssistenciais.Count > 0)
            {
                foreach (ListItem atividadeSocioAssistencial in lstAtividades.Items)
                {
                    atividadeSocioAssistencial.Selected = servico.AtividadesSocioAssistenciais.Any(s => s.Id == Convert.ToInt32(atividadeSocioAssistencial.Value));
                }
            }
            #endregion


            #region capacidade, capacidade [la|psc]
            if (servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_PROTECAO_SOCIAL_ADOLESC_CUMPR_MEDIDA_SOCIOEDUCATIVA_LA_PSC)
            {
                DistribuirCapacidadeLA(servico);
                DistribuirCapacidadePSC(servico);

                DistribuirMediaMensalLA(servico);
                DistribuirMediaMensalPSC(servico);
            }
            else
            {
                DistribuirCapacidade(servico);
                DistribuirMediaMensal(servico);
            }
            this.AplicarRegraExibicaoLayoutServicosChanged();
            #endregion

            #region Carregar: Quadro [Este serviço funciona quantas horas por semana?]
            rblHorasSemana.SelectedValue = servico.IdHorasSemana.ToString();
            rblDiasSemana.SelectedValue = servico.QuantidadeDiasSemana.ToString();
            rblAvaliacaoGestor.SelectedValue = servico.IdAvaliacaoServico.ToString();
            #endregion
        }
        private void CarregarRecursosFinanceiros(ServicoRecursoFinanceiroPrivadoInfo entidade)
        {

            int idService = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

            #region Exercicios
            
            #region Exercicio 4
            var fundoExercicio4 = entidade.ServicosRecursosFinanceirosFundosPrivadoInfo
                    .Where(x => x.ServicoRecursoFinanceiroPrivadoInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroPrivado.Exercicios[3]).FirstOrDefault();
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

                CarregarReprogramacaoExercicio4(fundoExercicio4);


                txtValorRecursoExclusivoExercicio4.Text = (fundoExercicio4.ValorRecursoExclusivoServico.HasValue)
                    ? fundoExercicio4.ValorRecursoExclusivoServico.Value.ToString("N2") : (0M).ToString("N2");

                rblOutrasFontesExercicio4.SelectedValue = fundoExercicio4.ExisteOutraFonteFinanciamento.HasValue
                    ? Convert.ToInt32(fundoExercicio4.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio4_SelectedIndexChanged(null, null);
                if (fundoExercicio4.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio4.ExisteOutraFonteFinanciamento.Value)
                {
                    //Sessao
                    foreach (var item in fundoExercicio4.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2025);
                    }

                    this.SessaoFontesRecursosExercicio4 = fundoExercicio4.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio2();
                }
                if (fundoExercicio4.ConvenioEstadualizado.HasValue && fundoExercicio4.ConvenioEstadualizado.Value)
                {
                    txtValorEstadualizadoExercicio4.Text = (fundoExercicio4.ValorEstadualizado.HasValue) ? fundoExercicio4.ValorEstadualizado.Value.ToString("N2") : "0,00";
                    trValorAnualConvenioExercicio4.Visible = true;
                    rblConvenioEstadualizadoExercicio4.SelectedValue = fundoExercicio4.ConvenioEstadualizado.HasValue
                    ? Convert.ToInt32(fundoExercicio4.ConvenioEstadualizado.Value).ToString() : String.Empty;
                    trMotivoConvenioEstadualizadoExercicio4.Visible = true;
                    rblMotivoEstadualizadoExercicio4.SelectedValue = fundoExercicio4.MotivoEstadualizadoInfoID.ToString();
                }
            }
            #endregion

            #region Exercicio 3
            var fundoExercicio3 = entidade.ServicosRecursosFinanceirosFundosPrivadoInfo
                    .Where(x => x.ServicoRecursoFinanceiroPrivadoInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroPrivado.Exercicios[2]).FirstOrDefault();
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
                CarregarReprogramacaoExercicio3(fundoExercicio3);


                txtValorRecursoExclusivoExercicio3.Text = (fundoExercicio3.ValorRecursoExclusivoServico.HasValue)
                    ? fundoExercicio3.ValorRecursoExclusivoServico.Value.ToString("N2") : (0M).ToString("N2");

                rblOutrasFontesExercicio3.SelectedValue = fundoExercicio3.ExisteOutraFonteFinanciamento.HasValue
                    ? Convert.ToInt32(fundoExercicio3.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;

                rblOutrasFontesExercicio3_SelectedIndexChanged(null, null);

                if (fundoExercicio3.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio3.ExisteOutraFonteFinanciamento.Value)
                {
                    //Sessao
                    foreach (var item in fundoExercicio3.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2024);
                    }


                    this.SessaoFontesRecursosExercicio3 = fundoExercicio3.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio3();
                }
                if (fundoExercicio3.ConvenioEstadualizado.HasValue && fundoExercicio3.ConvenioEstadualizado.Value)
                {
                    txtValorEstadualizadoExercicio3.Text = (fundoExercicio3.ValorEstadualizado.HasValue) ? fundoExercicio3.ValorEstadualizado.Value.ToString("N2") : "0,00";
                    trValorAnualConvenioExercicio3.Visible = true;
                    rblConvenioEstadualizadoExercicio3.SelectedValue = fundoExercicio3.ConvenioEstadualizado.HasValue
                    ? Convert.ToInt32(fundoExercicio3.ConvenioEstadualizado.Value).ToString() : String.Empty;
                    trMotivoConvenioEstadualizadoExercicio3.Visible = true;
                    rblMotivoEstadualizadoExercicio3.SelectedValue = fundoExercicio3.MotivoEstadualizadoInfoID.ToString();
                }
            }
            #endregion

            #region Exercicio 2
            var fundoExercicio2 = entidade.ServicosRecursosFinanceirosFundosPrivadoInfo
                    .Where(x => x.ServicoRecursoFinanceiroPrivadoInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroPrivado.Exercicios[1]).FirstOrDefault();
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
                CarregarReprogramacaoExercicio2(fundoExercicio2);


                txtValorRecursoExclusivoExercicio2.Text = (fundoExercicio2.ValorRecursoExclusivoServico.HasValue)
                    ? fundoExercicio2.ValorRecursoExclusivoServico.Value.ToString("N2") : (0M).ToString("N2");

                rblOutrasFontesExercicio2.SelectedValue = fundoExercicio2.ExisteOutraFonteFinanciamento.HasValue
                    ? Convert.ToInt32(fundoExercicio2.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio2_SelectedIndexChanged(null, null);
                if (fundoExercicio2.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio2.ExisteOutraFonteFinanciamento.Value)
                {
                    //Sessao
                    foreach (var item in fundoExercicio2.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2023);
                    }


                    this.SessaoFontesRecursosExercicio2 = fundoExercicio2.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio2();
                }
                if (fundoExercicio2.ConvenioEstadualizado.HasValue && fundoExercicio2.ConvenioEstadualizado.Value)
                {
                    txtValorEstadualizadoExercicio2.Text = (fundoExercicio2.ValorEstadualizado.HasValue) ? fundoExercicio2.ValorEstadualizado.Value.ToString("N2") : "0,00";
                    trValorAnualConvenioExercicio2.Visible = true;
                    rblConvenioEstadualizadoExercicio2.SelectedValue = fundoExercicio2.ConvenioEstadualizado.HasValue
                    ? Convert.ToInt32(fundoExercicio2.ConvenioEstadualizado.Value).ToString() : String.Empty;
                    trMotivoConvenioEstadualizadoExercicio2.Visible = true;
                    rblMotivoEstadualizadoExercicio2.SelectedValue = fundoExercicio2.MotivoEstadualizadoInfoID.ToString();
                }
            }
            #endregion

            #region Exercicio 1
            var fundoExercicio1 = entidade.ServicosRecursosFinanceirosFundosPrivadoInfo
                    .Where(x => x.ServicoRecursoFinanceiroPrivadoInfoId == idService
                        && x.Exercicio == FServicoRecursoFinanceiroPrivado.Exercicios[0]).FirstOrDefault();
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
                txtFEASAnoAnteriorExercicio1.Text = fundoExercicio1.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
                txtFEASDemandasExercicio1.Text = fundoExercicio1.ValorEstadualDemandasParlamentares.ToString("N2");
                txtFEASReprogramacaoDemandasParlamentaresExercicio1.Text = fundoExercicio1.ValorEstadualDemandasParlamentaresReprogramacao.ToString("N2");

                txtValorRecursoExclusivoExercicio1.Text = (fundoExercicio1.ValorRecursoExclusivoServico.HasValue)
                    ? fundoExercicio1.ValorRecursoExclusivoServico.Value.ToString("N2") : (0M).ToString("N2");

                rblOutrasFontesExercicio1.SelectedValue = fundoExercicio1.ExisteOutraFonteFinanciamento.HasValue
                    ? Convert.ToInt32(fundoExercicio1.ExisteOutraFonteFinanciamento.Value).ToString() : String.Empty;
                rblOutrasFontesExercicio1_SelectedIndexChanged(null, null);
                if (fundoExercicio1.ExisteOutraFonteFinanciamento.HasValue && fundoExercicio1.ExisteOutraFonteFinanciamento.Value)
                {
                    //Sessao
                    foreach (var item in fundoExercicio1.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo)
                    {
                        item.Liberado = RetornaValidacaoBloqueioDesbloqueio(2022);
                    }

                    this.SessaoFontesRecursosExercicio1 = fundoExercicio1.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo;
                    CarregarRecursosFinanceirosFonteRecursosExercicio1();
                }

                if (fundoExercicio1.ConvenioEstadualizado.HasValue && fundoExercicio1.ConvenioEstadualizado.Value)
                {
                    txtValorEstadualizadoExercicio1.Text = (fundoExercicio1.ValorEstadualizado.HasValue) ? fundoExercicio1.ValorEstadualizado.Value.ToString("N2") : "0,00";
                    trValorAnualConvenioExercicio1.Visible = true;
                    rblConvenioEstadualizadoExercicio1.SelectedValue = fundoExercicio1.ConvenioEstadualizado.HasValue
                    ? Convert.ToInt32(fundoExercicio1.ConvenioEstadualizado.Value).ToString() : String.Empty;
                    trMotivoConvenioEstadualizadoExercicio1.Visible = true;
                    rblMotivoEstadualizadoExercicio1.SelectedValue = fundoExercicio1.MotivoEstadualizadoInfoID.ToString();
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
        private void CarregarReprogramacaoExercicio2(ServicoRecursoFinanceiroFundosPrivadoInfo fundoExercicio2)
        {
            txtFEASAnoAnteriorExercicio2.Text = fundoExercicio2.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
        }
        private void CarregarReprogramacaoExercicio3(ServicoRecursoFinanceiroFundosPrivadoInfo fundoExercicio3)
        {
            txtFEASAnoAnteriorExercicio3.Text = fundoExercicio3.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
        }
        private void CarregarReprogramacaoExercicio4(ServicoRecursoFinanceiroFundosPrivadoInfo fundoExercicio4)
        {
            txtFEASAnoAnteriorExercicio4.Text = fundoExercicio4.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
        }


        private void CarregaDemandasExercicio1(ServicoRecursoFinanceiroFundosPrivadoInfo fundoExercicio1) 
        {
            txtFEASDemandasExercicio1.Text = fundoExercicio1.ValorEstadualDemandasParlamentares.ToString("N2");
        }

        private void CarregaDemandasExercicio2(ServicoRecursoFinanceiroFundosPrivadoInfo fundoExercicio2)
        {
            txtFEASDemandasExercicio2.Text = fundoExercicio2.ValorEstadualDemandasParlamentares.ToString("N2");
        }

        private void CarregaDemandasExercicio3(ServicoRecursoFinanceiroFundosPrivadoInfo fundoExercicio3)
        {
            txtFEASDemandasExercicio3.Text = fundoExercicio3.ValorEstadualDemandasParlamentares.ToString("N2");
        }

        private void CarregaDemandasExercicio4(ServicoRecursoFinanceiroFundosPrivadoInfo fundoExercicio4)
        {
            txtFEASDemandasExercicio4.Text = fundoExercicio4.ValorEstadualDemandasParlamentares.ToString("N2");
        }

        protected void CarregaDemandasParlamentares(ServicoRecursoFinanceiroPrivadoInfo Servico)
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {


                var demandas = Servico.ServicosRecursosFinanceirosFundosPrivadoInfo.Where(s => s.Exercicio >= 2022);

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
                            rblContraPartida2.SelectedValue = "1";
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
                            rblContraPartida3.SelectedValue = "0";
                            trValorContraExercicio3.Visible = false;
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

        private void CarregarMotivosEstadualizadoExercicio1(ProxyEstruturaAssistenciaSocial proxy)
        {
            List<MotivoEstadualizadoInfo> motivos = proxy.Service.GetMotivosEstadualizado();

            rblMotivoEstadualizadoExercicio1.DataTextField = "Nome";
            rblMotivoEstadualizadoExercicio1.DataValueField = "Id";
            rblMotivoEstadualizadoExercicio1.DataSource = proxy.Service.GetMotivosEstadualizado().Where(s => s.Id == 1 || s.Id == 2);
            rblMotivoEstadualizadoExercicio1.DataBind();
        }
        private void CarregarMotivosEstadualizadoExercicio2(ProxyEstruturaAssistenciaSocial proxy)
        {
            rblMotivoEstadualizadoExercicio2.DataTextField = "Nome";
            rblMotivoEstadualizadoExercicio2.DataValueField = "Id";
            rblMotivoEstadualizadoExercicio2.DataSource = proxy.Service.GetMotivosEstadualizado().Where(s => s.Id == 1 || s.Id == 2);
            rblMotivoEstadualizadoExercicio2.DataBind();
        }

        private void CarregarMotivosEstadualizadoExercicio3(ProxyEstruturaAssistenciaSocial proxy)
        {
            rblMotivoEstadualizadoExercicio3.DataTextField = "Nome";
            rblMotivoEstadualizadoExercicio3.DataValueField = "Id";
            rblMotivoEstadualizadoExercicio3.DataSource = proxy.Service.GetMotivosEstadualizado().Where(s => s.Id == 1 || s.Id == 2);
            rblMotivoEstadualizadoExercicio3.DataBind();
        }

        private void CarregarMotivosEstadualizadoExercicio4(ProxyEstruturaAssistenciaSocial proxy)
        {
            rblMotivoEstadualizadoExercicio4.DataTextField = "Nome";
            rblMotivoEstadualizadoExercicio4.DataValueField = "Id";

            rblMotivoEstadualizadoExercicio4.DataSource = proxy.Service.GetMotivosEstadualizado().Where(s =>s.Id == 1 || s.Id ==2);
            rblMotivoEstadualizadoExercicio4.DataBind();
        }


        private void CarregarCaracterizacaoUsuarios(ServicoRecursoFinanceiroPrivadoInfo obj, ProxyEstruturaAssistenciaSocial proxyEstrutura)
        {
            rblCaracteristicasTerritorio.SelectedValue = obj.IdCaracteristicasTerritorio.ToString();
            rblMoradiaUsuarios.SelectedValue = obj.IdRegiaoMoradia.HasValue ? obj.IdRegiaoMoradia.Value.ToString() : String.Empty;
            rblSexo.SelectedValue = obj.IdSexo.HasValue ? obj.IdSexo.Value.ToString() : String.Empty;

            CarregarSituacoes(proxyEstrutura);

            if (obj.SituacoesEspecificas != null && obj.SituacoesEspecificas.Count > 0)
                foreach (ListItem i in lstSituacoesEspecificas.Items)
                    i.Selected = obj.SituacoesEspecificas.Any(s => s.Id == Convert.ToInt32(i.Value));
        }
        private void CarregarRecursosHumanos(ProxyRedeProtecaoSocial proxy, ServicoRecursoFinanceiroPrivadoInfo obj)
        {
            var recursoshumanos = proxy.Service.GetRecursosHumanosPrivadoByIdServicoRecursoFinanceiro(obj.Id);

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

                //txtPosGraduacao.Text = recursoshumanos.PosGraduacao.ToString();
                txtEstagiarios.Text = recursoshumanos.Estagiarios.ToString();
                txtVoluntarios.Text = recursoshumanos.Voluntarios.ToString();

                txtExclusivoServico.Text = recursoshumanos.ExclusivoServico.ToString();
                txtOutroServicos.Text = recursoshumanos.OutrosServicosAssistenciais.ToString();

            }

            CarregarTotalRh();
        }
        private void CarregarCaracterizacaoServico(ProxyEstruturaAssistenciaSocial proxyEstrutura, ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            if (new List<Int32>() { 154, 155, 156 }.Contains(servico.UsuarioTipoServico.IdTipoServico))
                servico.UsuarioTipoServico.IdTipoServico = 138;

            if (new List<Int32>() { 157, 158, 159 }.Contains(servico.UsuarioTipoServico.IdTipoServico))
                servico.UsuarioTipoServico.IdTipoServico = 145;

            rblTipoProtecao.SelectedValue = servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial.ToString();
            CarregarTiposServicos(proxyEstrutura);
            ddlTipoServico.SelectedValue = servico.UsuarioTipoServico.IdTipoServico.ToString();
            CarregarTiposServicosNaoTipificados(proxyEstrutura);
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

            ddlAbrangencia.SelectedValue = servico.IdAbrangenciaServico.ToString();

            if (servico.IdUsuarioTipoServico == 37 && servico.UsuarioTipoServico.IdTipoServico == 146)
            {
                trAuxilioReclusaoPensaoMorte.Visible = true;


                if (servico.AtendeCriancasAuxilioReclusao == true)
                {
                    rblCriancasAuxilioReclusao.SelectedValue = "1";
                    trValCriancasAuxilioReclusao.Visible = true;

                    txtCriancaAuxilioReclusaoFeitos.Text = servico.CriancaAuxilioReclusaoFeitos.ToString();
                    txtCriancaAuxilioReclusaoAprovados.Text = servico.CriancaAuxilioReclusaoAprovados.ToString();
                    txtCriancaAuxilioReclusaoNegado.Text = servico.CriancaAuxilioReclusaoNegado.ToString();
                }
                else if (servico.AtendeCriancasAuxilioReclusao == false)
                {
                    rblCriancasAuxilioReclusao.SelectedValue = "0";
                    trValCriancasAuxilioReclusao.Visible = false;
                }
                else if (servico.AtendeCriancasAuxilioReclusao == null)
                {
                    trValCriancasAuxilioReclusao.Visible = false;
                }


                if (servico.AtendeCriancasAuxilioReclusaoExercicio2025 == true)
                {
                    rblCriancasAuxilioReclusaoExercicio2025.SelectedValue = "1";
                    trValCriancasAuxilioReclusaoExercicio2025.Visible = true;

                    txtCriancaAuxilioReclusaoFeitosExercicio2025.Text = servico.CriancaAuxilioReclusaoFeitosExercicio2025.ToString();
                    txtCriancaAuxilioReclusaoAprovadosExercicio2025.Text = servico.CriancaAuxilioReclusaoAprovadosExercicio2025.ToString();
                    txtCriancaAuxilioReclusaoNegadoExercicio2025.Text = servico.CriancaAuxilioReclusaoNegadoExercicio2025.ToString();
                }
                else if (servico.AtendeCriancasAuxilioReclusaoExercicio2025 == false)
                {
                    rblCriancasAuxilioReclusaoExercicio2025.SelectedValue = "0";
                    trValCriancasAuxilioReclusaoExercicio2025.Visible = false;
                }
                else if (servico.AtendeCriancasAuxilioReclusaoExercicio2025 == null)
                {
                    trValCriancasAuxilioReclusaoExercicio2025.Visible = false;
                }


                if (servico.AtendeCriancasPensaoMorte == true)
                {
                    rblCriancasPensaoMorte.SelectedValue = "1";
                    trValCriancasPensaoMorte.Visible = true;

                    txtCriancasPensaoMorteFeitos.Text = servico.CriancaPensaoMorteFeitos.ToString();
                    txtCriancasPensaoMorteAprovados.Text = servico.CriancaPensaoMorteAprovados.ToString();
                    txtCriancasPensaoMorteNegado.Text = servico.CriancaPensaoMorteNegado.ToString();
                }
                else if (servico.AtendeCriancasPensaoMorte == false)
                {
                    rblCriancasPensaoMorte.SelectedValue = "0";
                    trValCriancasPensaoMorte.Visible = false;
                }
                else if (servico.AtendeCriancasPensaoMorte == null)
                {
                    trValCriancasPensaoMorte.Visible = false;
                }


                if (servico.AtendeCriancasPensaoMorteExercicio2025 == true)
                {
                    rblCriancasPensaoMorteExercicio2025.SelectedValue = "1";
                    trValCriancasPensaoMorteExercicio2025.Visible = true;

                    txtCriancasPensaoMorteFeitosExercicio2025.Text = servico.CriancaPensaoMorteFeitosExercicio2025.ToString();
                    txtCriancasPensaoMorteAprovadosExercicio2025.Text = servico.CriancaPensaoMorteAprovadosExercicio2025.ToString();
                    txtCriancasPensaoMorteNegadoExercicio2025.Text = servico.CriancaPensaoMorteNegadoExercicio2025.ToString();
                }
                else if (servico.AtendeCriancasPensaoMorteExercicio2025 == false)
                {
                    rblCriancasPensaoMorteExercicio2025.SelectedValue = "0";
                    trValCriancasPensaoMorteExercicio2025.Visible = false;
                }
                else if (servico.AtendeCriancasPensaoMorteExercicio2025 == null)
                {
                    trValCriancasPensaoMorteExercicio2025.Visible = false;
                }

            }
            else
            {
                trAuxilioReclusaoPensaoMorte.Visible = false;
            }

            if (servico.IdAbrangenciaServico == 4)
            {
                trSedeServico.Visible = true;
                trFormaJuridica.Visible = true;
                trMunicipioSede.Visible = false;

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
                        trConsorcioPrivado.Visible = true;

                        var consorcio = proxyEstrutura.Service.GetConsorcioPublico(servico.Id);

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
                        trConsorcioPrivado.Visible = false;
                    }
                }

            }
            else
            {
                trSedeServico.Visible = false;
                trFormaJuridica.Visible = false;
            }


            if (servico.MunicipioSedeServico != null)
            {
                if (servico.MunicipioSedeServico == true)
                {
                    trMunicipioParticipaOferta.Visible = true;
                    trMunicipioSede.Visible = false;

                    txtMunicipioParticipaOferta.Text = servico.IndicaMunicipiosParticipamOfertaServico;

                }
                else
                {
                    trMunicipioParticipaOferta.Visible = false;
                    trMunicipioSede.Visible = false;

                    txtMunicipioSede.Text = servico.IndicaMunicipiosSedeServico;
                }
            }



            if (servico.IdFormaJuridica != null)
            {
                ddlFormaJuridica.SelectedValue = servico.IdFormaJuridica.ToString();
            }

            if (servico.IdFormaJuridica == 1)
            {
                trConsorcioPrivado.Visible = true;

                var consorcio = proxyEstrutura.Service.GetConsorcioPrivado(servico.Id);

                if (consorcio != null)
                {
                    if (!String.IsNullOrEmpty(consorcio.NomeConsorcio))
                    {
                        txtNomeConsorcio.Text = consorcio.NomeConsorcio;
                    }
                    else
                    {
                        txtNomeConsorcio.Text = "";
                    }

                    if (!String.IsNullOrEmpty(consorcio.CNPJ))
                    {
                        txtCNPJConsorcio.Text = consorcio.CNPJ;
                    }
                    else
                    {
                        txtCNPJConsorcio.Text = "";
                    }

                    if (!String.IsNullOrEmpty(consorcio.MunicipioSede))
                    {
                        txtMunicipioSedeConsorcio.Text = consorcio.MunicipioSede;
                    }
                    else
                    {
                        txtMunicipioSedeConsorcio.Text = "";
                    }
                     
                    if (!String.IsNullOrEmpty(consorcio.MunicipioEnvolvido))
                    {
                        txtMunicipiosEnvolvidos.Text = consorcio.MunicipioEnvolvido;
                    }
                    else
                    {
                        txtMunicipiosEnvolvidos.Text = "";
                    }

                }
            }
            else
            {
                trConsorcioPrivado.Visible = false;
            }


            if (servico.UsuarioTipoServico.IdTipoServico == 153)
                tbNaoTipificadoObjetivo.Visible = tbNaoTipificadoDetalhado.Visible = true;

            txtNaotipificado.Text = !String.IsNullOrWhiteSpace(servico.DescricaoServicoNaoTipificado) ? servico.DescricaoServicoNaoTipificado : String.Empty;
            txtObjetivoNaoTipificado.Text = !String.IsNullOrWhiteSpace(servico.ObjetivoServicoNaoTipificado) ? servico.ObjetivoServicoNaoTipificado : String.Empty;

            if (servico.IdTipoServicoNaoTipificado.HasValue)
            {
                CarregarUsuarios(proxyEstrutura, true);
            }
            else
            {
                CarregarUsuarios(proxyEstrutura);
            }

            if (servico.UsuarioTipoServico.IdTipoServico == 146 && servico.IdUsuarioTipoServico == 40 ||
                servico.UsuarioTipoServico.IdTipoServico == 148 && servico.IdUsuarioTipoServico == 43 ||
                servico.UsuarioTipoServico.IdTipoServico == 150 && servico.IdUsuarioTipoServico == 47 ||
                servico.UsuarioTipoServico.IdTipoServico == 150 && servico.IdUsuarioTipoServico == 48)
            {

                tdAtendimentoDependente.Visible = true;

                if (servico.AtendeDependentes != null)
                {
                    rblAtendeDependentes.SelectedValue = servico.AtendeDependentes.Value == true ? "1" : "0";
                }
                else 
                {
                    rblAtendeDependentes.SelectedValue = "0";
                }

                if (servico.AtendeProgramaRecomeco != null)
                {
                    tdProgramaRecomeco.Visible = true;
                    rblProgramaRecomeco.SelectedValue = servico.AtendeProgramaRecomeco.Value == true ? "1" : "0";
                }
                else
                {
                    tdProgramaRecomeco.Visible = true;
                    rblProgramaRecomeco.SelectedValue = "0";
                }
            }

            ddlPublicoAlvo.SelectedValue = servico.IdUsuarioTipoServico.ToString();
            ddlPublicoAlvo_SelectedIndexChanged(null, null);


            AposSalvoNaoPermitirEdicaoCamposCaracterizacao(servico);

        }

        private void CarregarOfertaServico(bool p)
        {
            trCaracteristicaOferta.Visible = p;
        }

        private void CarregarCombos(ProxyEstruturaAssistenciaSocial proxy)
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

            rblTipoProtecao.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(s => s.Id == 1 || s.Id == 2 || s.Id == 3);
            rblTipoProtecao.DataValueField = "Id";
            rblTipoProtecao.DataTextField = "Nome";
            rblTipoProtecao.DataBind();
            ListItem itemToRemove = rblTipoProtecao.Items.FindByValue("5");
            if (itemToRemove != null)
            {
                rblTipoProtecao.Items.Remove(itemToRemove);
            }
            itemToRemove = rblTipoProtecao.Items.FindByValue("4");
            if (itemToRemove != null)
            {
                rblTipoProtecao.Items.Remove(itemToRemove);
            }

        }
        private void CarregarTiposServicos(ProxyEstruturaAssistenciaSocial proxy)
        {
            ddlTipoServico.DataTextField = "Nome";
            ddlTipoServico.DataValueField = "Id";
            //O PAIF, PAEFI e Serviço para Pessoa em Situação de Rua, não deverão ser disponibilizadas nos combos das unidades Públicas e Privadas.
            ddlTipoServico.DataSource = proxy.Service.GetTiposServicoByTipoProtecaoSocial(Convert.ToInt32(rblTipoProtecao.SelectedValue)).Where(t => t.Id != 135 && t.Id != 139 && t.Id != 144);
            ddlTipoServico.DataBind();
            ListItem itemToRemove = ddlTipoServico.Items.FindByValue("142");
            if (itemToRemove != null)
            {
                ddlTipoServico.Items.Remove(itemToRemove);
            }
            Util.InserirItemEscolha(ddlTipoServico);
        }
        private void CarregarLocalExecucaoPublico(ProxyRedeProtecaoSocial proxy)
        {
            var listaGeral = new[] { new { Id = 0, Descricao = "0", Tipo = 0 } }.ToList();
            listaGeral.Clear();
        }
        private void CarregarSituacoes(ProxyEstruturaAssistenciaSocial proxy)
        {
            lstSituacoesEspecificas.DataTextField = "Nome";
            lstSituacoesEspecificas.DataValueField = "Id";
            lstSituacoesEspecificas.DataSource = proxy.Service.GetSituacoesEspecificasByUsuario(Convert.ToInt32(ddlPublicoAlvo.SelectedValue));
            lstSituacoesEspecificas.DataBind();
        }
        private void CarregarAtividades(ProxyEstruturaAssistenciaSocial proxy, Boolean naoTipificado = false)
        {
            lstAtividades.DataValueField = "Id";
            lstAtividades.DataTextField = "Nome";
            if (!naoTipificado)
                lstAtividades.DataSource = proxy.Service.GetAtividadesSocioAssistenciaisByTipoServico(Convert.ToInt32(ddlTipoServico.SelectedValue));
            else
                lstAtividades.DataSource = proxy.Service.GetAtividadesSocioAssistenciaisByTipoServico(Convert.ToInt32(ddlTipoServicoNaoTipificado.SelectedValue));
            lstAtividades.DataBind();
        }
        private void CarregarUsuarios(ProxyEstruturaAssistenciaSocial proxy, Boolean naoTipificado = false)
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
        private void CarregarComboProgramas(ProxyProgramas proxy)
        {
            ddlProgramaBeneficio.DataValueField = "Id";
            ddlProgramaBeneficio.DataTextField = "Nome";
            ddlProgramaBeneficio.DataSource = proxy.Service.GetProgramasByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            ddlProgramaBeneficio.DataBind();
            Util.InserirItemEscolha(ddlProgramaBeneficio);
        }
        private void CarregarRecursosHumanos(ProxyRedeProtecaoSocial proxy, ServicoRecursoFinanceiroPublicoInfo obj)
        {
            var recursoshumanos = proxy.Service.GetRecursosHumanosPublicoByIdServicoRecursoFinanceiro(obj.Id);

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

                //txtPosGraduacao.Text = recursoshumanos.PosGraduacao.ToString();
                txtEstagiarios.Text = recursoshumanos.Estagiarios.ToString();
                txtVoluntarios.Text = recursoshumanos.Voluntarios.ToString();

                txtExclusivoServico.Text = recursoshumanos.ExclusivoServico.ToString();
                txtOutroServicos.Text = recursoshumanos.OutrosServicosAssistenciais.ToString();
            }

            CarregarTotalRh();
        }
        private void CarregarAvaliacoes(ProxyEstruturaAssistenciaSocial proxy)
        {
            rblAvaliacaoGestor.DataTextField = "Descricao";
            rblAvaliacaoGestor.DataValueField = "Id";
            rblAvaliacaoGestor.DataSource = proxy.Service.GetAvaliacoes();
            rblAvaliacaoGestor.DataBind();
        }
        private void CarregarTotalRh()
        {
            try
            {
                int?[] array = {Util.TryParseInt32(txtSemEscolaridade.Text)
                               ,Util.TryParseInt32(txtNivelFundamental.Text)
                          , Util.TryParseInt32(txtNivelMedio.Text)
                          , Util.TryParseInt32(txtSuperior.Text)                          
                       };
                int?[] arrayEstVol = {Util.TryParseInt32(txtEstagiarios.Text)
                                , Util.TryParseInt32(txtVoluntarios.Text)
                       };
                lblTotalRh.Text = array.Sum().ToString(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CarregarRecursosFinanceirosFonteRecursosExercicio1()
        {
            lstRecursosAdicionadosExercicio1.DataSource = this.SessaoFontesRecursosExercicio1;
            lstRecursosAdicionadosExercicio1.DataBind();

            if (lstRecursosAdicionadosExercicio1.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio1.Visible = lstRecursosAdicionadosExercicio1.Visible = true;
            }
        }
        private void CarregarRecursosFinanceirosFonteRecursosExercicio2()
        {
            lstRecursosAdicionadosExercicio2.DataSource = this.SessaoFontesRecursosExercicio2;
            lstRecursosAdicionadosExercicio2.DataBind();

            if (lstRecursosAdicionadosExercicio2.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio2.Visible = lstRecursosAdicionadosExercicio2.Visible = true;
            }
        }

        private void CarregarRecursosFinanceirosFonteRecursosExercicio3()
        {
            lstRecursosAdicionadosExercicio3.DataSource = this.SessaoFontesRecursosExercicio3;
            lstRecursosAdicionadosExercicio3.DataBind();

            if (lstRecursosAdicionadosExercicio3.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio3.Visible = lstRecursosAdicionadosExercicio3.Visible = true;
            }
        }

        private void CarregarRecursosFinanceirosFonteRecursosExercicio4()
        {
            lstRecursosAdicionadosExercicio4.DataSource = this.SessaoFontesRecursosExercicio4;
            lstRecursosAdicionadosExercicio4.DataBind();

            if (lstRecursosAdicionadosExercicio4.Items.Count > 0)
            {
                tdlstRecursosAdicionadosExercicio4.Visible = lstRecursosAdicionadosExercicio4.Visible = true;
            }
        }

        private void CarregarTiposServicosNaoTipificados(ProxyEstruturaAssistenciaSocial proxy) //PMAS 2016
        {
            ddlTipoServicoNaoTipificado.DataTextField = "Nome";
            ddlTipoServicoNaoTipificado.DataValueField = "Id";
            ddlTipoServicoNaoTipificado.DataSource = rblTipoProtecao.SelectedValue != "3" ?
                proxy.Service.GetTiposServicoNaoTipificadoByTipoProtecaoSocial(Convert.ToInt32(rblTipoProtecao.SelectedValue)).Where(t => t.Id != 160).ToList() : new List<TipoServicoInfo>();
            ddlTipoServicoNaoTipificado.DataBind();
            Util.InserirItemEscolha(ddlTipoServicoNaoTipificado);
        }
        protected ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo CarregarRH()
        {
            ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo recursosHumanos = new ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo();
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
        #endregion

        #region [Adicionar |  Salvar | Voltar | Excluir]
        protected void btnSalvarRecursoPrograma_Click(object sender, EventArgs e)
        {

            this.AplicarRegraSalvarRecursoPrograma();
            this.btnSalvar_Click(sender, e);
        }
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            lblInconsistencias.Text = String.Empty;
            tbInconsistencias.Visible = false;
            frame1_1_1.Attributes.Add("class", "frame active");
            frame1_2_1.Attributes.Remove("class");
            frame1_3_1.Attributes.Remove("class");
            frame1_4_1.Attributes.Remove("class");
            frame1_5_1_1.Attributes.Remove("class");
            var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));
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
                if (ddlProgramaBeneficio.SelectedValue != "0")
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


        protected void ValidaTextBoxAuxilioReclusao() 
        {
            var erros = new List<string>();

            if (exercicioDesbloqueado == 2024)
            {
                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoFeitos.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos feitos em 2024', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoAprovados.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram aprovados', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoNegado.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram negados', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }

            }
            else if (exercicioDesbloqueado == 2025)
            {
                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoFeitosExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos feitos em 2025', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoAprovadosExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram aprovados', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancaAuxilioReclusaoNegadoExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram negados', para a opção, atende crianças aptas para receber o auxílio-reclusão é obrigatório.");
                }              
            }
            

            if (erros.Count > 0)
                throw new Exception(Extensions.Concat(erros, System.Environment.NewLine));
        }


        private void ValidaTextBoxPensaoMorte()
        {
            var erros = new List<string>();

            if (exercicioDesbloqueado == 2024)
            {
                if (String.IsNullOrEmpty(txtCriancasPensaoMorteFeitos.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos feitos em 2024', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancasPensaoMorteAprovados.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram aprovados', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancasPensaoMorteNegado.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram negados', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }
            }
            else if (exercicioDesbloqueado == 2025)
            {

                if (String.IsNullOrEmpty(txtCriancasPensaoMorteFeitosExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos feitos em 2025', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancasPensaoMorteAprovadosExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram aprovados', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }

                if (String.IsNullOrEmpty(txtCriancasPensaoMorteNegadoExercicio2025.Text))
                {
                    erros.Add("O preenchimento do campo 'Quantos requerimentos foram negados', para a opção, atende crianças aptas para receber o pensao por morte é obrigatório.");
                }                  
            }

            if (erros.Count > 0)
                throw new Exception(Extensions.Concat(erros, System.Environment.NewLine));
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();
            var idLocalExecucao = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));
            var servico = new ServicoRecursoFinanceiroPrivadoInfo();
            var consorcio = new ConsorcioPrivadoInfo();

            String action = "I";
            try
            {

                #region  Preencher: Caracterizacao do Servico
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    servico.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                }

                servico.IdLocalExecucao = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));

                servico.UsuarioTipoServico = new UsuarioTipoServicoInfo();
                if (ddlTipoServico.SelectedIndex != -1)
                {
                    servico.UsuarioTipoServico.IdTipoServico = Convert.ToInt32(ddlTipoServico.SelectedValue);
                }

                if (ddlPublicoAlvo.SelectedIndex != -1)
                {
                    servico.IdUsuarioTipoServico = Convert.ToInt32(ddlPublicoAlvo.SelectedValue);
                }


                if (ddlTipoServico.SelectedValue == "146" && ddlPublicoAlvo.SelectedValue == "40" ||
                    ddlTipoServico.SelectedValue == "148" && ddlPublicoAlvo.SelectedValue == "43" ||
                    ddlTipoServico.SelectedValue == "150" && ddlPublicoAlvo.SelectedValue == "47" ||
                    ddlTipoServico.SelectedValue == "150" && ddlPublicoAlvo.SelectedValue == "48")
                {
                    if (!String.IsNullOrEmpty(rblAtendeDependentes.SelectedValue))
                    {
                        servico.AtendeDependentes = rblAtendeDependentes.SelectedValue == "1";
                        if (servico.AtendeDependentes.Value)
                        {
                            if (!String.IsNullOrEmpty(rblProgramaRecomeco.SelectedValue))
                            {
                                servico.AtendeProgramaRecomeco = rblProgramaRecomeco.SelectedValue == "1" ? true : false;
                            }
                            else
                            {
                                servico.AtendeProgramaRecomeco = false;
                            }

                        }
                    }
                }


                #region Carregar: [Tipo Servico]
                servico.UsuarioTipoServico.TipoServico = new TipoServicoInfo();
                if (rblTipoProtecao.SelectedIndex != -1)
                {
                    servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial = Convert.ToInt16(rblTipoProtecao.SelectedValue);
                }
                if (ddlTipoServico.SelectedValue == "138" || ddlTipoServico.SelectedValue == "145")
                {
                    servico.IdTipoServicoNaoTipificado = ddlTipoServicoNaoTipificado.SelectedValue != "0"
                        ? Convert.ToInt32(ddlTipoServicoNaoTipificado.SelectedValue) : new Nullable<Int32>();
                    if (servico.IdTipoServicoNaoTipificado.HasValue &&
                        (servico.IdTipoServicoNaoTipificado.Value == 156 || servico.IdTipoServicoNaoTipificado.Value == 159)) //Outro tipo de serviço não tipificado
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
                #endregion

                if (ddlAbrangencia.SelectedValue != null)
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
                    
                    consorcio.IdServicosRecursosFinanceirosPrivado = servico.Id;
                    consorcio.NomeConsorcio = txtNomeConsorcio.Text;
                    consorcio.MunicipioSede = txtMunicipioSedeConsorcio.Text;
                    consorcio.MunicipioEnvolvido = txtMunicipiosEnvolvidos.Text;
                    consorcio.CNPJ = txtCNPJConsorcio.Text;
                }

                servico.PossuiTecnicoResponsavel = !chkNaoPossuiTecnicoResponsavel.Checked;
                servico.NomeTecnicoResponsavel = txtTecnicoResponsavel.Text;
                servico.IdAbrangenciaServico = Convert.ToInt32(ddlAbrangencia.SelectedValue);
                servico.IdCaracteristicasTerritorio = Convert.ToInt32(rblCaracteristicasTerritorio.SelectedValue);
                
                servico.IdCaracteristicaOfertaServico = Convert.ToInt32(rblCaracteristicaOferta.SelectedValue);

                servico.IndicaMunicipiosParticipamOfertaServico = txtMunicipioParticipaOferta.Text;

                servico.IndicaMunicipiosSedeServico = txtMunicipioSede.Text;

                #endregion

                #region Preencher: Usuarios
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

                #region Preencher: Funcionamento [Capacidade | Media Mensal]

                if (servico.UsuarioTipoServico.IdTipoServico == R_TIPO_SERVICO.SERVICO_PROTECAO_SOCIAL_ADOLESC_CUMPR_MEDIDA_SOCIOEDUCATIVA_LA_PSC)
                {

                    #region Carregar: Capacidade  LA
                    servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA = new List<ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo>();
                    ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio1 = new ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio1.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio1.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio1.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[0];

                    ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio2 = new ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio2.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio2.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio2.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[1];

                    ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio3 = new ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio3.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio3.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio3.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[2];

                    ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio4 = new ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio4.Capacidade = !String.IsNullOrEmpty(txtCapacidadeLAExercicio4.Text) ? Convert.ToInt32(txtCapacidadeLAExercicio4.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[3];

                    servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA.Add(servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA.Add(servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA.Add(servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA.Add(servicoRecursoFinanceiroPrivadoCapacidadeLAInfoExercicio4);
                    #endregion

                    #region Carregar: Capacidade PSC
                    servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC = new List<ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo>();
                    ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio1 = new ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio1.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio1.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio1.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[0];

                    ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio2 = new ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio2.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio2.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio2.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[1];

                    ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio3 = new ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio3.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio3.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio3.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[2];

                    ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio4 = new ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio4.Capacidade = !String.IsNullOrEmpty(txtCapacidadePSCExercicio4.Text) ? Convert.ToInt32(txtCapacidadePSCExercicio4.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[3];

                    servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC.Add(servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC.Add(servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC.Add(servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC.Add(servicoRecursoFinanceiroPrivadoCapacidadePSCInfoExercicio4);
                    #endregion

                    #region Carregar: MM LA
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA = new List<ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo>();

                    #region Exercicio 1 
                    ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio1 = new ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio1.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio1.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio1.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[0]; 
                    #endregion

                    #region Exercicio 2
                    ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio2 = new ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio2.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio2.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio2.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[1]; 
                    #endregion

                    #region Exercicio 3
                    ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio3 = new ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio3.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio3.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio3.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[2]; 
                    #endregion

                    #region Exercicio 4
                    ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio4 = new ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalLAExercicio4.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio4.MediaMensal = Convert.ToInt32(txtMediaMensalLAExercicio4.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[3]; 
                    #endregion

                    servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA.Add(servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA.Add(servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA.Add(servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA.Add(servicoRecursoFinanceiroPrivadoMediaMensalLAInfoExercicio4);
                    #endregion

                    #region Carregar: MM PSC
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC = new List<ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo>();

                    #region Exercicio 1 
                    ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio1 = new ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio1.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio1.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio1.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[0]; 
                    #endregion

                    #region Exercicio 2
                    ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio2 = new ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio2.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio2.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio2.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[1]; 
                    #endregion

                    #region Exercicio 3
                    ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio3 = new ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio3.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio3.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio3.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[2]; 
                    #endregion

                    #region Exercicio 4
                    ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio4 = new ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalPSCExercicio4.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio4.MediaMensal = Convert.ToInt32(txtMediaMensalPSCExercicio4.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[3]; 
                    #endregion

                    servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC.Add(servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC.Add(servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC.Add(servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC.Add(servicoRecursoFinanceiroPrivadoMediaMensalPSCInfoExercicio4);
                    #endregion
                }
                else
                {

                    #region Carregar: Capacidade
                    servico.ServicosRecursosFinanceiroPrivadoCapacidade = new List<ServicoRecursoFinanceiroPrivadoCapacidadeInfo>();
                    ServicoRecursoFinanceiroPrivadoCapacidadeInfo servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio1 = new ServicoRecursoFinanceiroPrivadoCapacidadeInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio1.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio1.Text) ? Convert.ToInt32(txtCapacidadeExercicio1.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[0];

                    ServicoRecursoFinanceiroPrivadoCapacidadeInfo servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio2 = new ServicoRecursoFinanceiroPrivadoCapacidadeInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio2.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio2.Text) ? Convert.ToInt32(txtCapacidadeExercicio2.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[1];

                    ServicoRecursoFinanceiroPrivadoCapacidadeInfo servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio3 = new ServicoRecursoFinanceiroPrivadoCapacidadeInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio3.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio3.Text) ? Convert.ToInt32(txtCapacidadeExercicio3.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[2];

                    ServicoRecursoFinanceiroPrivadoCapacidadeInfo servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio4 = new ServicoRecursoFinanceiroPrivadoCapacidadeInfo();
                    servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio4.Capacidade = !String.IsNullOrEmpty(txtCapacidadeExercicio4.Text) ? Convert.ToInt32(txtCapacidadeExercicio4.Text) : 0;
                    servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[3];

                    servico.ServicosRecursosFinanceiroPrivadoCapacidade.Add(servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPrivadoCapacidade.Add(servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPrivadoCapacidade.Add(servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPrivadoCapacidade.Add(servicoRecursoFinanceiroPrivadoCapacidadeInfoExercicio4);
                    #endregion

                    #region Carregar: MM
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensal = new List<ServicoRecursoFinanceiroPrivadoMediaMensalInfo>();

                    #region Exercicio 1 
                    ServicoRecursoFinanceiroPrivadoMediaMensalInfo servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio1 = new ServicoRecursoFinanceiroPrivadoMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio1.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio1.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio1.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio1.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[0]; 
                    #endregion

                    #region Exercicio 2
                    ServicoRecursoFinanceiroPrivadoMediaMensalInfo servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio2 = new ServicoRecursoFinanceiroPrivadoMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio2.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio2.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio2.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio2.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[1]; 
                    #endregion

                    #region Exercicio 3
                    ServicoRecursoFinanceiroPrivadoMediaMensalInfo servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio3 = new ServicoRecursoFinanceiroPrivadoMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio3.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio3.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio3.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio3.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[2];
                    #endregion

                    #region Exercicio 4
                    ServicoRecursoFinanceiroPrivadoMediaMensalInfo servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio4 = new ServicoRecursoFinanceiroPrivadoMediaMensalInfo();
                    if (!String.IsNullOrEmpty(txtMediaMensalExercicio4.Text))
                    {
                        servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio4.MediaMensal = Convert.ToInt32(txtMediaMensalExercicio4.Text);
                    }
                    servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio4.Exercicio = FServicoRecursoFinanceiroPrivado.Exercicios[3]; 
                    #endregion

                    servico.ServicosRecursosFinanceiroPrivadoMediaMensal.Add(servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio1);
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensal.Add(servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio2);
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensal.Add(servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio3);
                    servico.ServicosRecursosFinanceiroPrivadoMediaMensal.Add(servicoRecursoFinanceiroPrivadoMediaMensalInfoExercicio4);
                    #endregion

                }



                #endregion

                #region  Preencher: [Hora semana]
                servico.IdHorasSemana = Convert.ToInt32(rblHorasSemana.SelectedValue);
                servico.QuantidadeDiasSemana = Convert.ToInt32(rblDiasSemana.SelectedValue);
                #endregion

                #region Preencher: [Atividade Socio Assistenciais]

                servico.AtividadesSocioAssistenciais = new List<AtividadeSocioAssistencialInfo>();
                foreach (ListItem atividade in lstAtividades.Items)
                {
                    if (atividade.Selected)
                    {
                        servico.AtividadesSocioAssistenciais.Add(new AtividadeSocioAssistencialInfo() { Id = Convert.ToInt32(atividade.Value) });
                    }
                }

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

                #region Preencher: [Recursos Financeiros]

                #region Exercicio 1
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPrivado.Exercicios[0])
                {
                    ServicoRecursoFinanceiroFundosPrivadoInfo fundo = new ServicoRecursoFinanceiroFundosPrivadoInfo();
                    fundo.ServicoRecursoFinanceiroPrivadoInfoId = servico.Id;
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
                    fundo.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio1.Text) ? string.Empty : txtObjetoDemandaExercicio1.Text;
                    fundo.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio1.Text) ? string.Empty : txtCodigoDemandaExercicio1.Text;
                    fundo.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio1.Text) ? Convert.ToDecimal(txtValorContraExercicio1.Text) : 0M;
                    fundo.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;

                    //Outras fontes financeiras
                    //Valor dos recursos da própria Organização
                    fundo.ValorRecursoExclusivoServico = !String.IsNullOrEmpty(txtValorRecursoExclusivoExercicio1.Text)
                        ? Convert.ToDecimal(txtValorRecursoExclusivoExercicio1.Text) : 0M;

                    #region outra fonte de financiamento
                    //outra fonte de financiamento (valor estadualizado)
                    fundo.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio1.SelectedValue == "1" ? true : false;
                    //Carrega valor "estadualizado"
                    if (fundo.ExisteOutraFonteFinanciamento.HasValue && fundo.ExisteOutraFonteFinanciamento.Value)
                    {
                        fundo.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo = this.SessaoFontesRecursosExercicio1;
                    }
                    #endregion

                    #region convenio
                    fundo.ConvenioEstadualizado = rblConvenioEstadualizadoExercicio1.SelectedValue == "1";
                    if (fundo.ConvenioEstadualizado.HasValue && fundo.ConvenioEstadualizado.Value)
                    {
                        fundo.MotivoEstadualizadoInfo = new MotivoEstadualizadoInfo();

                        fundo.MotivoEstadualizadoInfo.Id = (rblMotivoEstadualizadoExercicio1.SelectedValue == string.Empty)
                                                         ? new Int32() : Convert.ToInt32(rblMotivoEstadualizadoExercicio1.SelectedValue);

                        fundo.MotivoEstadualizadoInfo.Nome = String.IsNullOrEmpty(rblMotivoEstadualizadoExercicio1.SelectedItem.Text) ? " " : rblMotivoEstadualizadoExercicio1.SelectedItem.Text;
                        
                        fundo.ValorEstadualizado = !String.IsNullOrEmpty(txtValorEstadualizadoExercicio1.Text)
                                                    ? Convert.ToDecimal(txtValorEstadualizadoExercicio1.Text) : 0M;
                    }
                    #endregion

                    fundo.Exercicio = Convert.ToInt32(hdnExercicio.Value);

                    servico.ServicosRecursosFinanceirosFundosPrivadoInfo =
                        servico.ServicosRecursosFinanceirosFundosPrivadoInfo ?? new List<ServicoRecursoFinanceiroFundosPrivadoInfo>();
                    servico.ServicosRecursosFinanceirosFundosPrivadoInfo.Add(fundo);
                } 
                #endregion

                #region Exercicio 2
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPrivado.Exercicios[1])
                {
                    ServicoRecursoFinanceiroFundosPrivadoInfo fundo2 = new ServicoRecursoFinanceiroFundosPrivadoInfo();
                    fundo2.ServicoRecursoFinanceiroPrivadoInfoId = servico.Id;
                    fundo2.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio2.Text)) ? Convert.ToDecimal(txtFEASExercicio2.Text) : 0M;
                    fundo2.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio2.Text)) ? Convert.ToDecimal(txtFEDCAExercicio2.Text) : 0M;
                    fundo2.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio2.Text)) ? Convert.ToDecimal(txtFMASExercicio2.Text) : 0M;
                    fundo2.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio2.Text)) ? Convert.ToDecimal(txtFMDCAExercicio2.Text) : 0M;
                    fundo2.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio2.Text)) ? Convert.ToDecimal(txtFNASExercicio2.Text) : 0M;
                    fundo2.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio2.Text)) ? Convert.ToDecimal(txtFNDCAExercicio2.Text) : 0M;
                    fundo2.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio2.Text) ? Convert.ToDecimal(txtFMIExercicio2.Text) : 0M;
                    fundo2.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio2.Text) ? Convert.ToDecimal(txtFEIExercicio2.Text) : 0M;
                    fundo2.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio2.Text) ? Convert.ToDecimal(txtFNIExercicio2.Text) : 0M;
                    fundo2.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio2.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio2.Text) : 0M;
                    fundo2.ValorEstadualAssistenciaAnoAnterior = !String.IsNullOrEmpty(txtFEASAnoAnteriorExercicio2.Text) ? Convert.ToDecimal(txtFEASAnoAnteriorExercicio2.Text) : 0M;
                    fundo2.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio2.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio2.Text) : 0M;
                    fundo2.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio2.Text) ? string.Empty : txtObjetoDemandaExercicio2.Text;
                    fundo2.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio2.Text) ? string.Empty : txtCodigoDemandaExercicio2.Text;
                    fundo2.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio2.Text) ? Convert.ToDecimal(txtValorContraExercicio2.Text) : 0M;
                    fundo2.ContrapartidaMunicipal = rblContraPartida2.SelectedValue == "1" ? true : false;

                    //Existe Valor Exclusivo?
                    fundo2.ValorRecursoExclusivoServico = !String.IsNullOrEmpty(txtValorRecursoExclusivoExercicio2.Text)
                        ? Convert.ToDecimal(txtValorRecursoExclusivoExercicio2.Text) : 0M;

                    #region outra fonte de financiamento
                    //outra fonte de financiamento (valor estadualizado)
                    fundo2.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio2.SelectedValue == "1" ? true : false;
                    //Carrega valor "estadualizado"
                    if (fundo2.ExisteOutraFonteFinanciamento.HasValue && fundo2.ExisteOutraFonteFinanciamento.Value)
                    {
                        fundo2.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo = this.SessaoFontesRecursosExercicio2;
                    }
                    #endregion

                    #region convenio
                    fundo2.ConvenioEstadualizado = rblConvenioEstadualizadoExercicio2.SelectedValue == "1";
                    if (fundo2.ConvenioEstadualizado.HasValue && fundo2.ConvenioEstadualizado.Value)
                    {
                        fundo2.MotivoEstadualizadoInfo = new MotivoEstadualizadoInfo();

                        fundo2.MotivoEstadualizadoInfo.Id = (rblMotivoEstadualizadoExercicio2.SelectedValue == string.Empty)
                                                         ? new Int32() : Convert.ToInt32(rblMotivoEstadualizadoExercicio2.SelectedValue);

                        if (rblMotivoEstadualizadoExercicio2.SelectedItem != null)
                        {
                             fundo2.MotivoEstadualizadoInfo.Nome = String.IsNullOrEmpty(rblMotivoEstadualizadoExercicio2.SelectedItem.Text) ? " " : rblMotivoEstadualizadoExercicio2.SelectedItem.Text;
                        }

                        fundo2.ValorEstadualizado = !String.IsNullOrEmpty(txtValorEstadualizadoExercicio2.Text)
                                                    ? Convert.ToDecimal(txtValorEstadualizadoExercicio2.Text) : 0M;
                    }
                    #endregion
                    fundo2.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    servico.ServicosRecursosFinanceirosFundosPrivadoInfo = servico.ServicosRecursosFinanceirosFundosPrivadoInfo ?? new List<ServicoRecursoFinanceiroFundosPrivadoInfo>();
                    servico.ServicosRecursosFinanceirosFundosPrivadoInfo.Add(fundo2);
                } 
                #endregion

                #region Exercicio 3
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPrivado.Exercicios[2])
                {
                    ServicoRecursoFinanceiroFundosPrivadoInfo fundo3 = new ServicoRecursoFinanceiroFundosPrivadoInfo();
                    fundo3.ServicoRecursoFinanceiroPrivadoInfoId = servico.Id;
                    fundo3.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio3.Text)) ? Convert.ToDecimal(txtFEASExercicio3.Text) : 0M;
                    fundo3.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio3.Text)) ? Convert.ToDecimal(txtFEDCAExercicio3.Text) : 0M;
                    fundo3.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio3.Text)) ? Convert.ToDecimal(txtFMASExercicio3.Text) : 0M;
                    fundo3.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio3.Text)) ? Convert.ToDecimal(txtFMDCAExercicio3.Text) : 0M;
                    fundo3.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio3.Text)) ? Convert.ToDecimal(txtFNASExercicio3.Text) : 0M;
                    fundo3.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio3.Text)) ? Convert.ToDecimal(txtFNDCAExercicio3.Text) : 0M;
                    fundo3.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio3.Text) ? Convert.ToDecimal(txtFMIExercicio3.Text) : 0M;
                    fundo3.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio3.Text) ? Convert.ToDecimal(txtFEIExercicio3.Text) : 0M;
                    fundo3.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio3.Text) ? Convert.ToDecimal(txtFNIExercicio3.Text) : 0M;
                    fundo3.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio3.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio3.Text) : 0M;
                    fundo3.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio3.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio3.Text) : 0M;
                    fundo3.ValorEstadualAssistenciaAnoAnterior = !String.IsNullOrEmpty(txtFEASAnoAnteriorExercicio3.Text) ? Convert.ToDecimal(txtFEASAnoAnteriorExercicio3.Text) : 0M;
                    fundo3.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio3.Text) ? string.Empty : txtObjetoDemandaExercicio3.Text;
                    fundo3.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio3.Text) ? string.Empty : txtCodigoDemandaExercicio3.Text;
                    fundo3.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio3.Text) ? Convert.ToDecimal(txtValorContraExercicio3.Text) : 0M;
                    fundo3.ContrapartidaMunicipal = rblContraPartida3.SelectedValue == "1" ? true : false;

                    //Existe Valor Exclusivo?
                    fundo3.ValorRecursoExclusivoServico = !String.IsNullOrEmpty(txtValorRecursoExclusivoExercicio3.Text)
                        ? Convert.ToDecimal(txtValorRecursoExclusivoExercicio3.Text) : 0M;

                    #region outra fonte de financiamento
                    //outra fonte de financiamento (valor estadualizado)
                    fundo3.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio3.SelectedValue == "1" ? true : false;
                    //Carrega valor "estadualizado"
                    if (fundo3.ExisteOutraFonteFinanciamento.HasValue && fundo3.ExisteOutraFonteFinanciamento.Value)
                    {
                        fundo3.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo = this.SessaoFontesRecursosExercicio3;
                    }
                    #endregion

                    #region convenio
                    fundo3.ConvenioEstadualizado = rblConvenioEstadualizadoExercicio3.SelectedValue == "1";
                    if (fundo3.ConvenioEstadualizado.HasValue && fundo3.ConvenioEstadualizado.Value)
                    {
                        fundo3.MotivoEstadualizadoInfo = new MotivoEstadualizadoInfo();

                        fundo3.MotivoEstadualizadoInfo.Id = (rblMotivoEstadualizadoExercicio3.SelectedValue == string.Empty)
                                                         ? new Int32() : Convert.ToInt32(rblMotivoEstadualizadoExercicio3.SelectedValue);
                        fundo3.MotivoEstadualizadoInfo.Nome = (rblMotivoEstadualizadoExercicio3.SelectedItem != null) ? rblMotivoEstadualizadoExercicio3.SelectedItem.Text : "" ;
                        fundo3.ValorEstadualizado = !String.IsNullOrEmpty(txtValorEstadualizadoExercicio3.Text)
                                                    ? Convert.ToDecimal(txtValorEstadualizadoExercicio3.Text) : 0M;
                    }
                    #endregion
                    fundo3.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    servico.ServicosRecursosFinanceirosFundosPrivadoInfo = servico.ServicosRecursosFinanceirosFundosPrivadoInfo ?? new List<ServicoRecursoFinanceiroFundosPrivadoInfo>();
                    servico.ServicosRecursosFinanceirosFundosPrivadoInfo.Add(fundo3);
                } 
                #endregion

                #region Exercicio 4
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPrivado.Exercicios[3])
                {
                    ServicoRecursoFinanceiroFundosPrivadoInfo fundo4 = new ServicoRecursoFinanceiroFundosPrivadoInfo();
                    fundo4.ServicoRecursoFinanceiroPrivadoInfoId = servico.Id;
                    fundo4.ValorEstadualAssistencia = (!String.IsNullOrEmpty(txtFEASExercicio4.Text)) ? Convert.ToDecimal(txtFEASExercicio4.Text) : 0M;
                    fundo4.ValorEstadualFEDCA = (!String.IsNullOrEmpty(txtFEDCAExercicio4.Text)) ? Convert.ToDecimal(txtFEDCAExercicio4.Text) : 0M;
                    fundo4.ValorMunicipalAssistencia = (!String.IsNullOrEmpty(txtFMASExercicio4.Text)) ? Convert.ToDecimal(txtFMASExercicio4.Text) : 0M;
                    fundo4.ValorMunicipalFMDCA = (!String.IsNullOrEmpty(txtFMDCAExercicio4.Text)) ? Convert.ToDecimal(txtFMDCAExercicio4.Text) : 0M;
                    fundo4.ValorFederalAssistencia = (!String.IsNullOrEmpty(txtFNASExercicio4.Text)) ? Convert.ToDecimal(txtFNASExercicio4.Text) : 0M;
                    fundo4.ValorFederalFNDCA = (!String.IsNullOrEmpty(txtFNDCAExercicio4.Text)) ? Convert.ToDecimal(txtFNDCAExercicio4.Text) : 0M;
                    fundo4.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMIExercicio4.Text) ? Convert.ToDecimal(txtFMIExercicio4.Text) : 0M;
                    fundo4.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEIExercicio4.Text) ? Convert.ToDecimal(txtFEIExercicio4.Text) : 0M;
                    fundo4.ValorFederalFNI = !String.IsNullOrEmpty(txtFNIExercicio4.Text) ? Convert.ToDecimal(txtFNIExercicio4.Text) : 0M;
                    fundo4.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio4.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio4.Text) : 0M;
                    fundo4.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio4.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio4.Text) : 0M;
                    fundo4.ValorEstadualAssistenciaAnoAnterior = !String.IsNullOrEmpty(txtFEASAnoAnteriorExercicio4.Text) ? Convert.ToDecimal(txtFEASAnoAnteriorExercicio4.Text) : 0M;
                    fundo4.ObjetoDemandaParlamentar = textoVazioNuloComEspaco(txtObjetoDemandaExercicio4.Text) ? string.Empty : txtObjetoDemandaExercicio4.Text;
                    fundo4.CodigoDemandaParlamentar = textoVazioNuloComEspaco(txtCodigoDemandaExercicio4.Text) ? string.Empty : txtCodigoDemandaExercicio4.Text;
                    fundo4.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio4.Text) ? Convert.ToDecimal(txtValorContraExercicio4.Text) : 0M;
                    fundo4.ContrapartidaMunicipal = rblContraPartida4.SelectedValue == "1" ? true : false;

                    //Existe Valor Exclusivo?
                    fundo4.ValorRecursoExclusivoServico = !String.IsNullOrEmpty(txtValorRecursoExclusivoExercicio4.Text)
                        ? Convert.ToDecimal(txtValorRecursoExclusivoExercicio4.Text) : 0M;

                    #region outra fonte de financiamento
                    //outra fonte de financiamento (valor estadualizado)
                    fundo4.ExisteOutraFonteFinanciamento = rblOutrasFontesExercicio4.SelectedValue == "1" ? true : false;
                    //Carrega valor "estadualizado"
                    if (fundo4.ExisteOutraFonteFinanciamento.HasValue && fundo4.ExisteOutraFonteFinanciamento.Value)
                    {
                        fundo4.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo = this.SessaoFontesRecursosExercicio4;
                    }
                    #endregion

                    #region convenio
                    fundo4.ConvenioEstadualizado = rblConvenioEstadualizadoExercicio4.SelectedValue == "1";
                    if (fundo4.ConvenioEstadualizado.HasValue && fundo4.ConvenioEstadualizado.Value)
                    {
                        fundo4.MotivoEstadualizadoInfo = new MotivoEstadualizadoInfo();

                        fundo4.MotivoEstadualizadoInfo.Id = (rblMotivoEstadualizadoExercicio4.SelectedValue == string.Empty)
                                                         ? new Int32() : Convert.ToInt32(rblMotivoEstadualizadoExercicio4.SelectedValue);
                        fundo4.MotivoEstadualizadoInfo.Nome = rblMotivoEstadualizadoExercicio4.SelectedItem.Text;
                        fundo4.ValorEstadualizado = !String.IsNullOrEmpty(txtValorEstadualizadoExercicio4.Text)
                                                    ? Convert.ToDecimal(txtValorEstadualizadoExercicio4.Text) : 0M;
                    }
                    #endregion
                    fundo4.Exercicio = Convert.ToInt32(hdnExercicio.Value);
                    servico.ServicosRecursosFinanceirosFundosPrivadoInfo = servico.ServicosRecursosFinanceirosFundosPrivadoInfo ?? new List<ServicoRecursoFinanceiroFundosPrivadoInfo>();
                    servico.ServicosRecursosFinanceirosFundosPrivadoInfo.Add(fundo4);
                }
                #endregion

                #endregion

                #region Preencher: Demandas

                var demandas = new DemandasParlamentaresServicosPrivadosInfo();

                #region Exercicio1
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPrivado.Exercicios[0])
                {
                    DemandasParlamentaresServicosPrivadosInfo demandas1 = new DemandasParlamentaresServicosPrivadosInfo();
                    demandas1.ServicoRecursoFinanceiroPrivado = servico.Id;
                    demandas1.ObjetoDemandaParlamentar = !String.IsNullOrEmpty(txtObjetoDemandaExercicio1.Text) ? txtObjetoDemandaExercicio1.Text : "";
                    demandas1.CodigoDemandaParlamentar = !String.IsNullOrEmpty(txtCodigoDemandaExercicio1.Text) ? txtCodigoDemandaExercicio1.Text : "";
                    demandas1.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio1.Text) ? Convert.ToDecimal(txtValorContraExercicio1.Text) : 0M;
                    demandas1.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;
                    demandas = demandas1;
                }
                #endregion
                #region Exercicio2
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPrivado.Exercicios[1])
                {
                    DemandasParlamentaresServicosPrivadosInfo demandas2 = new DemandasParlamentaresServicosPrivadosInfo();
                    demandas2.ServicoRecursoFinanceiroPrivado = servico.Id;
                    demandas2.ObjetoDemandaParlamentar = !String.IsNullOrEmpty(txtObjetoDemandaExercicio2.Text) ? txtObjetoDemandaExercicio2.Text : "";
                    demandas2.CodigoDemandaParlamentar = !String.IsNullOrEmpty(txtCodigoDemandaExercicio2.Text) ? txtCodigoDemandaExercicio2.Text : "";
                    demandas2.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio2.Text) ? Convert.ToDecimal(txtValorContraExercicio2.Text) : 0M;
                    demandas2.ContrapartidaMunicipal = rblContraPartida1.SelectedValue == "1" ? true : false;
                    demandas = demandas2;

                }
                #endregion
                #region Exercicio3
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPrivado.Exercicios[2])
                {
                    DemandasParlamentaresServicosPrivadosInfo demandas3 = new DemandasParlamentaresServicosPrivadosInfo();
                    demandas3.ServicoRecursoFinanceiroPrivado = servico.Id;
                    demandas3.ObjetoDemandaParlamentar = !String.IsNullOrEmpty(txtObjetoDemandaExercicio3.Text) ? txtObjetoDemandaExercicio3.Text : "";
                    demandas3.CodigoDemandaParlamentar = !String.IsNullOrEmpty(txtCodigoDemandaExercicio3.Text) ? txtCodigoDemandaExercicio3.Text : "";
                    demandas3.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio3.Text) ? Convert.ToDecimal(txtValorContraExercicio3.Text) : 0M;
                    demandas3.ContrapartidaMunicipal = rblContraPartida3.SelectedValue == "1" ? true : false;
                    demandas = demandas3;
                }
                #endregion
                #region Exercicio4
                if (Convert.ToInt32(hdnExercicio.Value) == FServicoRecursoFinanceiroPrivado.Exercicios[3])
                {
                    DemandasParlamentaresServicosPrivadosInfo demandas4 = new DemandasParlamentaresServicosPrivadosInfo();
                    demandas4.ServicoRecursoFinanceiroPrivado = servico.Id;
                    demandas4.ObjetoDemandaParlamentar = !String.IsNullOrEmpty(txtObjetoDemandaExercicio4.Text) ? txtObjetoDemandaExercicio4.Text : "";
                    demandas4.CodigoDemandaParlamentar = !String.IsNullOrEmpty(txtCodigoDemandaExercicio4.Text) ? txtCodigoDemandaExercicio4.Text : "";
                    demandas4.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio4.Text) ? Convert.ToDecimal(txtValorContraExercicio4.Text) : 0M;
                    demandas4.ContrapartidaMunicipal = rblContraPartida4.SelectedValue == "1" ? true : false;
                    demandas = demandas4;
                }
                #endregion

                #endregion		

                #region Preencher: [Programa e Servico]
                if (servico.Id != 0)
                {
                    if (String.IsNullOrEmpty(rblIntegracaoRede.SelectedValue))
                    {
                        throw new Exception("Informe se o usuário deste serviço é beneficiário de algum programa, projeto ou benefício!");
                    }
                    else
                    {
                        servico.PossuiProgramaBeneficio = Convert.ToBoolean(Convert.ToInt32(rblIntegracaoRede.SelectedValue));
                    }
                }
                #endregion

                #region Preencher: RH
                var recursosHumanos = CarregarRH();
                #endregion


                #region Crianças auxilio reclusao
                if (!String.IsNullOrEmpty(rblCriancasAuxilioReclusao.SelectedValue))
                {
                    if (rblCriancasAuxilioReclusao.SelectedValue == "1")
                    {
                        ValidaTextBoxAuxilioReclusao();

                        servico.AtendeCriancasAuxilioReclusao = true;
                        servico.CriancaAuxilioReclusaoFeitos = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoFeitos.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoFeitos.Text);
                        servico.CriancaAuxilioReclusaoAprovados = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoAprovados.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoAprovados.Text);
                        servico.CriancaAuxilioReclusaoNegado = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoNegado.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoNegado.Text);
                    }
                    else
                    {
                        servico.AtendeCriancasAuxilioReclusao = false;

                        servico.CriancaAuxilioReclusaoFeitos = 0;
                        servico.CriancaAuxilioReclusaoAprovados = 0;
                        servico.CriancaAuxilioReclusaoNegado = 0;

                    }
                }
                #endregion

                #region Crianças Pensão Morte
                if (!String.IsNullOrEmpty(rblCriancasPensaoMorte.SelectedValue))
                {
                    if (rblCriancasPensaoMorte.SelectedValue == "1")
                    {
                        ValidaTextBoxPensaoMorte();
                        servico.AtendeCriancasPensaoMorte = true;

                        servico.CriancaPensaoMorteFeitos = String.IsNullOrEmpty(txtCriancasPensaoMorteFeitos.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteFeitos.Text);
                        servico.CriancaPensaoMorteAprovados = String.IsNullOrEmpty(txtCriancasPensaoMorteAprovados.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteAprovados.Text);
                        servico.CriancaPensaoMorteNegado = String.IsNullOrEmpty(txtCriancasPensaoMorteNegado.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteNegado.Text);
                    }
                    else
                    {
                        servico.AtendeCriancasPensaoMorte = false;

                        servico.CriancaPensaoMorteFeitos = 0;
                        servico.CriancaPensaoMorteAprovados = 0;
                        servico.CriancaPensaoMorteNegado = 0;
                    }
                }
                #endregion

                #region Crianças auxilio reclusao Exercicio 2025
                if (!String.IsNullOrEmpty(rblCriancasAuxilioReclusaoExercicio2025.SelectedValue))
                {
                    if (rblCriancasAuxilioReclusaoExercicio2025.SelectedValue == "1")
                    {
                        ValidaTextBoxAuxilioReclusao();

                        servico.AtendeCriancasAuxilioReclusaoExercicio2025 = true;
                        servico.CriancaAuxilioReclusaoFeitosExercicio2025 = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoFeitosExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoFeitosExercicio2025.Text);
                        servico.CriancaAuxilioReclusaoAprovadosExercicio2025 = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoAprovadosExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoAprovadosExercicio2025.Text);
                        servico.CriancaAuxilioReclusaoNegadoExercicio2025 = String.IsNullOrEmpty(txtCriancaAuxilioReclusaoNegadoExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancaAuxilioReclusaoNegadoExercicio2025.Text);

                    }
                    else
                    {
                        servico.AtendeCriancasAuxilioReclusaoExercicio2025 = false;

                        servico.CriancaAuxilioReclusaoFeitosExercicio2025 = 0;
                        servico.CriancaAuxilioReclusaoAprovadosExercicio2025 = 0;
                        servico.CriancaAuxilioReclusaoNegadoExercicio2025 = 0;

                    }
                }
                else
                {
                    servico.AtendeCriancasAuxilioReclusaoExercicio2025 = null;

                    servico.CriancaAuxilioReclusaoFeitosExercicio2025 = 0;
                    servico.CriancaAuxilioReclusaoAprovadosExercicio2025 = 0;
                    servico.CriancaAuxilioReclusaoNegadoExercicio2025 = 0;
                }
                #endregion

                #region Crianças Pensão Morte Exercicio 2025
                if (!String.IsNullOrEmpty(rblCriancasPensaoMorteExercicio2025.SelectedValue))
                {
                    if (rblCriancasPensaoMorteExercicio2025.SelectedValue == "1")
                    {
                        ValidaTextBoxPensaoMorte();

                        servico.AtendeCriancasPensaoMorteExercicio2025 = true;

                        servico.CriancaPensaoMorteFeitosExercicio2025 = String.IsNullOrEmpty(txtCriancasPensaoMorteFeitosExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteFeitosExercicio2025.Text);
                        servico.CriancaPensaoMorteAprovadosExercicio2025 = String.IsNullOrEmpty(txtCriancasPensaoMorteAprovadosExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteAprovadosExercicio2025.Text);
                        servico.CriancaPensaoMorteNegadoExercicio2025 = String.IsNullOrEmpty(txtCriancasPensaoMorteNegadoExercicio2025.Text) ? 0 : Convert.ToInt32(txtCriancasPensaoMorteNegadoExercicio2025.Text);
                    }
                    else
                    {
                        servico.AtendeCriancasPensaoMorteExercicio2025 = false;

                        servico.CriancaPensaoMorteFeitosExercicio2025 = 0;
                        servico.CriancaPensaoMorteAprovadosExercicio2025 = 0;
                        servico.CriancaPensaoMorteNegadoExercicio2025 = 0;
                    }
                }
                else
                {
                    servico.AtendeCriancasPensaoMorteExercicio2025 = null;

                    servico.CriancaPensaoMorteFeitosExercicio2025 = 0;
                    servico.CriancaPensaoMorteAprovadosExercicio2025 = 0;
                    servico.CriancaPensaoMorteNegadoExercicio2025 = 0;
                }
                #endregion	

                #region Aplicar: [Validacao]

                new ValidadorServicoRecursoFinanceiro().ValidarServicoPrivado(servico);
                var validaRh = new ValidadorRecursosHumanos().ValidaRHPrivado(recursosHumanos);
                if (validaRh.Count > 0)
                {
                    throw new Exception(Extensions.Concat(validaRh, System.Environment.NewLine));
                }
                #endregion

                #region Aplicar Ação [Incluir/Alterar]
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    if (servico.Id == 0)
                    {
                        #region Adicionar: Servico Financeiro [Privado]
                        proxy.Service.AddServicoRecursoFinanceiroPrivado(servico);

                        recursosHumanos.IdServicosRecursosFinanceirosPrivado = servico.Id;
                        consorcio.IdServicosRecursosFinanceirosPrivado = servico.Id;

                        using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
                        {
                            proxySocial.Service.SalvarConsorcioPrivado(consorcio);
                        }

                        if (recursosHumanos.Id == 0)
                        {
                            proxy.Service.AddServicoRecursoFinanceiroPrivadoRH(recursosHumanos);
                        }

                        AdicionarProgramaProjeto(idLocalExecucao, servico);

                        #endregion

                        this.ClearSessao();
                        this.ClearPrograma();
                    }
                    else
                    {
                        #region Atualizar: Servico Financeiro [Privado]
                        action = "A";
                        proxy.Service.UpdateServicoRecursoFinanceiroPrivado(servico);


                        using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
                        {
                            if (consorcio.Id == 0)
                            {
                                consorcio.IdServicosRecursosFinanceirosPrivado = servico.Id;
                                proxySocial.Service.SalvarConsorcioPrivado(consorcio);
                            }
                            else
                            {
                                consorcio.IdServicosRecursosFinanceirosPrivado = servico.Id;
                                proxySocial.Service.SalvarConsorcioPrivado(consorcio);
                            }
                        }


                        using (var proxySocial = new ProxyEstruturaAssistenciaSocial())
                        {
                            proxySocial.Service.SalvarConsorcioPrivado(consorcio);
                        }

                        if (recursosHumanos.Id == 0)
                        {
                            recursosHumanos.IdServicosRecursosFinanceirosPrivado = servico.Id;
                            proxy.Service.AddServicoRecursoFinanceiroPrivadoRH(recursosHumanos);
                        }
                        else
                        {
                            recursosHumanos.IdServicosRecursosFinanceirosPrivado = servico.Id;
                            proxy.Service.UpdateServicoRecursoFinanceiroPrivadoRH(recursosHumanos);
                        }



                        #endregion
                    }
                }
                #endregion

                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    // caso usuario não beneficiário de algum programa, projeto ou benefício, limpar serviços
                    if (!Convert.ToBoolean(Convert.ToInt32(rblIntegracaoRede.SelectedValue)))
                    {
                        var idServicoBeneficio = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                        //var idLocal = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));
                        using (var ProxyProgramas = new ProxyProgramas())
                        {
                            var lst = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(idServicoBeneficio, idLocalExecucao)
                                .Where(s1 => s1.Exercicio == Exercicios[0]) //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
                                .OrderBy(t => t.IdTipoProtecao)
                                .GroupBy(s => s.ProtecaoSocial)
                                .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) })
                                .ToList();

                            foreach (var tab in lst)
                                foreach (var item in tab.Items)
                                    ProxyProgramas.Service.DeleteProgramaProjetoCofinanciamento(Convert.ToInt32(item.Id), Convert.ToInt32(item.TipoCofinanciamento));

                            trRendaCidadaBeneficioIdoso.Visible = true;
                            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
                            lstRecursos.DataSource = lst;
                            lstRecursos.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            var idLocal = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
            var id = servico.Id;
            if (action == "I")
            {
                Response.Redirect("~/BlocoIII/FServicoRecursoFinanceiroPrivado.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString())) + "&idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)) + "&msg=" + action);
            }
            else if (action == "A")
            {

                Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPrivado.aspx?idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)) + "&msg=" + action);
            }
        }

        private void AdicionarProgramaProjeto(int idLocalExecucao, ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            if (SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio != null)
            {
                foreach (var item in SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio)
                {
                    if (item.Nome.ToLower().Contains("beneficio de prestação continuada - bpc idosos")
                          || item.Nome.ToLower().Contains("beneficio de prestação continuada - bpc pessoas com deficiência")
                          || item.Nome.Contains("BOLSA FAMÍLIA")
                          || item.Nome.ToLower().Contains("peti")
                          || item.Nome.ToLower().Contains("ação jovem")
                          || item.Nome.ToLower().Contains("renda cidadã")
                          || item.Nome.ToLower().Contains("teste transferencia renda")
                          )
                    {
                        AdicionaTransferenciaRenda(idLocalExecucao, servico.Id, item.IdProgramaProjeto);

                    }
                    else if (item.Nome.ToLower().Contains("auxílio natalidade")
                       || item.Nome.ToLower().Contains("auxílio funeral")
                       || item.Nome.ToLower().Contains("calamidades públicas e emergências")
                       || item.Nome.ToLower().Contains("vulnerabilidade temporária")
                   )
                    {
                        AdicionaPrefeituraBeneficio(idLocalExecucao, servico.Id, item.IdProgramaProjeto);
                    }
                    else
                    {
                        AdicionarProgramaConfinamento(idLocalExecucao, servico.Id, item.IdProgramaProjeto);
                    }

                }
            }
        }
        protected void btnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            int idPrograma = Convert.ToInt32(btn.CommandArgument);
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.RemoveAll(x => x.Id == idPrograma);
            if (SessaoTransferenciaRendaCofinanciamentoExercicio != null)
            {
                var transferencia = SessaoTransferenciaRendaCofinanciamentoExercicio.Where(x => x.Id == idPrograma);
                if (transferencia != null)
                {
                    SessaoTransferenciaRendaCofinanciamentoExercicio.RemoveAll(x => x.Id == idPrograma);
                }
            }
            if (SessaoPrefeituraBeneficioEventualServicoExercicio != null)
            {
                var prefeitura = SessaoPrefeituraBeneficioEventualServicoExercicio.Where(x => x.Id == idPrograma);
                if (prefeitura != null)
                {
                    SessaoPrefeituraBeneficioEventualServicoExercicio.RemoveAll(x => x.Id == idPrograma);
                }
            }
            if (SessaoProgramaProjetoCofinanciamentoExercicio != null)
            {
                var programa = SessaoProgramaProjetoCofinanciamentoExercicio.Where(x => x.Id == idPrograma);
                if (programa != null)
                {
                    SessaoProgramaProjetoCofinanciamentoExercicio.RemoveAll(x => x.Id == idPrograma);
                }
            }
            rptProgramaTemp.DataSource = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio.ToList();
            rptProgramaTemp.DataBind();
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            var idLocal = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
            Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPrivado.aspx?idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }
        #endregion

        protected void rblTipoProtecao_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            DefinirFrame1ComoPrincipal();

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarTiposServicos(proxy);
                //carregarTiposServicosNaoTipificados(proxy);
                frame1_5_1_1.Attributes.Remove("class");
                frame1_1_1.Attributes.Add("class", "active");
                tbNaoTipificado.Visible = false;
                tbNaoTipificadoObjetivo.Visible = tbNaoTipificadoDetalhado.Visible = false;
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
        protected void ddlPublicoAlvo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            DefinirFrame1ComoPrincipal();

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarSituacoes(proxy);
            }

            if (ddlTipoServico.SelectedValue == "146" && ddlPublicoAlvo.SelectedValue == "40" || ddlTipoServico.SelectedValue == "148" && ddlPublicoAlvo.SelectedValue == "43"
              || ddlTipoServico.SelectedValue == "150" && ddlPublicoAlvo.SelectedValue == "47" || ddlTipoServico.SelectedValue == "150" && ddlPublicoAlvo.SelectedValue == "48")
            {
                tdAtendimentoDependente.Visible = true;
            }
            else
            {
                tdAtendimentoDependente.Visible = false;
                tdProgramaRecomeco.Visible = false;
            }


            if (ddlTipoServico.SelectedValue == "146" && ddlPublicoAlvo.SelectedValue == "37")
            {
                trAuxilioReclusaoPensaoMorte.Visible = true;
            }
            else
            {
                trAuxilioReclusaoPensaoMorte.Visible = false;
            }

            if (ddlTipoServicoNaoTipificado.SelectedValue == "154" && ddlPublicoAlvo.SelectedValue == "67" ||
                ddlTipoServicoNaoTipificado.SelectedValue == "155" && ddlPublicoAlvo.SelectedValue == "68" ||
                ddlTipoServicoNaoTipificado.SelectedValue == "157" && ddlPublicoAlvo.SelectedValue == "70" ||
                ddlTipoServicoNaoTipificado.SelectedValue == "158" && ddlPublicoAlvo.SelectedValue == "71" ||
                ddlTipoServicoNaoTipificado.SelectedValue == "153" && ddlPublicoAlvo.SelectedValue == "54")
            {
                
                lblIndicadorPeriodoCapacidade.Text = " famílias";
                lblIndicadorPeriodoMediaMensal.Text = " famílias";
            }
            else   
            {
                lblIndicadorPeriodoCapacidadeLAPSC.Text = " pessoas";
                lblIndicadorPeriodoMediaMensalLAPSC.Text = " pessoas";
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
                txtNaotipificado.Text = "";
                txtObjetivoNaoTipificado.Text = "";
            }

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarUsuarios(proxy, true);
                CarregarAtividades(proxy, true);
            }

            //LIMPAR SITUAÇÕES
            lstSituacoesEspecificas.DataTextField = "Nome";
            lstSituacoesEspecificas.DataValueField = "Id";
            lstSituacoesEspecificas.DataSource = new List<SituacaoEspecificaInfo>();
            lstSituacoesEspecificas.DataBind();
        }
        protected void ddlTipoServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            DefinirFrame1ComoPrincipal();

            this.ClearTipoServico();

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                CarregarUsuarios(proxy);
                CarregarAtividades(proxy);
                CarregarTiposServicosNaoTipificados(proxy);
            }

            this.AplicarTipoServicoChanged();
            this.ClearSituacoes();
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
            var idLocal = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]));
            if (e.CommandName == "Excluir")
            {
                using (var proxy = new ProxyProgramas())
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string id = commandArgs[0];
                    string tipoCofinanciamento = commandArgs[1];
                    proxy.Service.DeleteProgramaProjetoCofinanciamento(Convert.ToInt32(id), Convert.ToInt32(tipoCofinanciamento));
                    CarregarProgramas(proxy, idServicosRecursosFinanceiros, idLocal);
                }
            }
        }
        protected void rblIntegracaoRede_SelectedIndexChanged(object sender, EventArgs e)
        {
            trProgramasBeneficios.Visible = false;


            if (rblIntegracaoRede.SelectedValue == "1")
            {
                trProgramasBeneficios.Visible = true;
                using (var proxy = new ProxyProgramas())
                {
                    CarregarComboProgramas(proxy);
                }
            }
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
        protected void rblAtendeDependentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAtendeDependentes.SelectedValue == "1")
                tdProgramaRecomeco.Visible = true;
            else
                tdProgramaRecomeco.Visible = false;
        }

        private void AdicionarProgramaConfinamento(int idCentro, int IdServicosRecursosFinanceirosPrivado, int idPrograma)
        {
            var item1 = SessaoProgramaProjetoCofinanciamentoExercicio.Where(x => x.IdProgramaProjeto == idPrograma).SingleOrDefault();

            var obj = new ProgramaProjetoCofinanciamentoInfo();
            obj.IdProgramaProjeto = Convert.ToInt32(item1.IdProgramaProjeto);
            obj.IdServicosRecursosFinanceirosPrivado = IdServicosRecursosFinanceirosPrivado;
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
                        CarregarProgramas(proxy, obj.IdServicosRecursosFinanceirosPrivado.Value, idCentro);
                    }
                }
                var idServicoBeneficio = IdServicosRecursosFinanceirosPrivado;

                using (var ProxyProgramas = new ProxyProgramas())
                {
                    
                    var programas = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(obj.IdServicosRecursosFinanceirosPrivado.Value, idCentro);

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
                    var obj2 = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

                    obj2.IdTransferenciaRenda = Convert.ToInt32(obj.IdProgramaProjeto);
                    obj2.IdServicosRecursosFinanceirosPrivado = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                    obj2.NumeroUsuarios = Convert.ToInt32(item1.NumeroUsuarios);
                    if (trRendaCidadaBeneficioIdoso.Visible && obj2.NumeroUsuarios <= 0)
                    {
                        throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
                    }
                    using (var proxy2 = new ProxyProgramas())
                    {
                        proxy2.Service.AddTransferenciaRendaCofinanciamento(obj2);
                        CarregarProgramas(proxy2, obj2.IdServicosRecursosFinanceirosPrivado.Value, idCentro);
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
                obj.IdServicosRecursosFinanceirosPrivado = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));


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
                    var programaprojeto = proxy.Service.GetProgramaProjetoById(Convert.ToInt32(ddlProgramaBeneficio.SelectedValue));

                    if (programaprojeto != null)
                    {
                        if (programaprojeto.IdPrefeitura == SessaoPmas.UsuarioLogado.Prefeitura.Id)
                        {
                            proxy.Service.AddProgramaProjetoCofinanciamento(obj);
                            CarregarProgramas(proxy, obj.IdServicosRecursosFinanceirosPrivado.Value, idCentro);
                        }
                    }
                    var idServicoBeneficio = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                    using (var ProxyProgramas = new ProxyProgramas())
                    {
                        var programas = ProxyProgramas.Service.GetConsultaProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(obj.IdServicosRecursosFinanceirosPrivado.Value, idCentro);
                        if (programas != null)
                        {
                            var lst = programas.OrderBy(t => t.IdTipoProtecao)
                                        .GroupBy(s => s.ProtecaoSocial)
                                        .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) })
                                        .ToList();

                            foreach (var programa in lst)
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

                        transferenciaRendaCofinanciamento.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
                        transferenciaRendaCofinanciamento.IdServicosRecursosFinanceirosPrivado = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

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

                        using (var proxy2 = new ProxyProgramas())
                        {
                            proxy2.Service.AddTransferenciaRendaCofinanciamento(transferenciaRendaCofinanciamento);
                            CarregarProgramas(proxy2, transferenciaRendaCofinanciamento.IdServicosRecursosFinanceirosPrivado.Value, idCentro);
                        }
                    }
                }
            }
            else
            {
                AdicionaListaProgramaProjetoCofinanciamentoInfo();

            }


        }
        private void AdicionaListaProgramaProjetoCofinanciamentoInfo()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
            var obj = new ProgramaProjetoCofinanciamentoInfo();
            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();

            SessaoProgramaProjetoCofinanciamentoExercicio = SessaoProgramaProjetoCofinanciamentoExercicio ?? new List<ProgramaProjetoCofinanciamentoInfo>();

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
            this.SessaoProgramaProjetoCofinanciamentoExercicio.Add(obj);
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
                entidadeBeneficio.IdServicosRecursosFinanceirosPrivado = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                if (!String.IsNullOrEmpty(txtNumeroUsuarios.Text))
                {
                    entidadeBeneficio.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
                }

                new ValidadorServicoBeneficioEventual().Validar(entidadeBeneficio);

                using (var proxy = new ProxyProgramas())
                {
                    proxy.Service.AddBeneficioEventualServico(entidadeBeneficio);
                    CarregarProgramas(proxy, entidadeBeneficio.IdServicosRecursosFinanceirosPrivado.Value, idCentro);
                }
            }
            else
            {

                AdicionaListaPrefeituraBeneficio();


            }
        }
        private void AdicionaListaPrefeituraBeneficio()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano
            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();
            SessaoPrefeituraBeneficioEventualServicoExercicio = SessaoPrefeituraBeneficioEventualServicoExercicio ?? new List<PrefeituraBeneficioEventualServicoInfo>();


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
            this.SessaoPrefeituraBeneficioEventualServicoExercicio.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";

        }
        private void AdicionaPrefeituraBeneficio(int idCentro, int IdServicosRecursosFinanceirosPrivado, int idPrograma)
        {

            var item2 = SessaoPrefeituraBeneficioEventualServicoExercicio.Where(x => x.IdPrefeituraBeneficioEventual == idPrograma).SingleOrDefault();

            var entidadeBeneficio = new PrefeituraBeneficioEventualServicoInfo();


            entidadeBeneficio.NumeroBeneficiarios = Convert.ToInt32(item2.NumeroBeneficiarios);


            entidadeBeneficio.IdPrefeituraBeneficioEventual = item2.IdPrefeituraBeneficioEventual;
            entidadeBeneficio.IdServicosRecursosFinanceirosPrivado = IdServicosRecursosFinanceirosPrivado;


            new ValidadorServicoBeneficioEventual().Validar(entidadeBeneficio);

            using (var proxy = new ProxyProgramas())
            {
                proxy.Service.AddBeneficioEventualServico(entidadeBeneficio);
                CarregarProgramas(proxy, entidadeBeneficio.IdServicosRecursosFinanceirosPrivado.Value, idCentro);
            }


        }
        private void AtualizaTransferenciaRenda(int idCentro)
        {

            if (Request.QueryString["id"] != null)
            {

                var entidadeTransferenciaRenda = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

                entidadeTransferenciaRenda.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
                entidadeTransferenciaRenda.IdServicosRecursosFinanceirosPrivado = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

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
                    CarregarProgramas(proxy, entidadeTransferenciaRenda.IdServicosRecursosFinanceirosPrivado.Value, idCentro);
                }
            }
            else
            {

                AdicionaListaTransferenciaRenda();


            }
        }
        private void AdicionaTransferenciaRenda(int idCentro, int IdServicosRecursosFinanceirosPrivado, int idPrograma)
        {
            var item2 = SessaoTransferenciaRendaCofinanciamentoExercicio.Where(x => x.IdTransferenciaRenda == idPrograma).SingleOrDefault();

            var entidadeTransferenciaRenda = new ServicoRecursoFinanceiroTransferenciaRendaInfo();
            entidadeTransferenciaRenda.IdTransferenciaRenda = item2.IdTransferenciaRenda;

            entidadeTransferenciaRenda.IdServicosRecursosFinanceirosPrivado = IdServicosRecursosFinanceirosPrivado;



            entidadeTransferenciaRenda.NumeroUsuarios = Convert.ToInt32(item2.NumeroUsuarios);

            if (trRendaCidadaBeneficioIdoso.Visible && entidadeTransferenciaRenda.NumeroUsuarios <= 0)
            {
                throw new Exception(String.Concat("O número de beneficiários deste serviço deve ser maior que 0.", System.Environment.NewLine));
            }
            // var idCentro = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]));
            using (var proxy = new ProxyProgramas())
            {
                proxy.Service.AddTransferenciaRendaCofinanciamento(entidadeTransferenciaRenda);
                CarregarProgramas(proxy, entidadeTransferenciaRenda.IdServicosRecursosFinanceirosPrivado.Value, idCentro);

            }

        }
        private void AdicionaListaTransferenciaRenda()
        {
            //DBM: A Procedure duplica programas por causa da previsão orçamentária (usada em algum lugar?) feito distinct por ano

            ConsultaProgramaProjetoServicoCofinanciamentoInfo consultaProgramaProjetoServicoCofinanciamentoInfo = new ConsultaProgramaProjetoServicoCofinanciamentoInfo();
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio ?? new List<ConsultaProgramaProjetoServicoCofinanciamentoInfo>();
            SessaoTransferenciaRendaCofinanciamentoExercicio = SessaoTransferenciaRendaCofinanciamentoExercicio ?? new List<ServicoRecursoFinanceiroTransferenciaRendaInfo>();

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
            this.SessaoTransferenciaRendaCofinanciamentoExercicio.Add(obj);
            trRendaCidadaBeneficioIdoso.Visible = true;
            lblBenificiarios.Text = "Quantos beneficiários deste programa / benefício são atendidos por este serviço? ";
            //btnSalvarRecursoPrograma.Visible = lstRecursos.Items.Count > 0;
        }
        private void AdicionarPrefeituraBeneficioEventualServico(int idLocal)
        {
            var prefeituraBeneficioEventual = new PrefeituraBeneficioEventualServicoInfo();
            if (!String.IsNullOrEmpty(txtNumeroUsuarios.Text))
            {
                prefeituraBeneficioEventual.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            }

            prefeituraBeneficioEventual.IdPrefeituraBeneficioEventual = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            prefeituraBeneficioEventual.IdServicosRecursosFinanceirosPrivado = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

            if (!String.IsNullOrWhiteSpace(txtNumeroUsuarios.Text))
            {
                prefeituraBeneficioEventual.NumeroBeneficiarios = Convert.ToInt32(txtNumeroUsuarios.Text);
            }

            new ValidadorServicoBeneficioEventual().Validar(prefeituraBeneficioEventual);

            using (var proxy = new ProxyProgramas())
            {
                proxy.Service.AddBeneficioEventualServico(prefeituraBeneficioEventual);
                CarregarProgramas(proxy, prefeituraBeneficioEventual.IdServicosRecursosFinanceirosPrivado.Value, idLocal);
            }
        }
        private void AdicionarTransferenciaRendaCofinanciamento(int idLocal)
        {
            var transferenciaRendaCofinanciamento = new ServicoRecursoFinanceiroTransferenciaRendaInfo();

            transferenciaRendaCofinanciamento.IdTransferenciaRenda = Convert.ToInt32(ddlProgramaBeneficio.SelectedValue);
            transferenciaRendaCofinanciamento.IdServicosRecursosFinanceirosPrivado = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

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

            using (var proxy = new ProxyProgramas())
            {
                proxy.Service.AddTransferenciaRendaCofinanciamento(transferenciaRendaCofinanciamento);
                CarregarProgramas(proxy, transferenciaRendaCofinanciamento.IdServicosRecursosFinanceirosPrivado.Value, idLocal);
            }
        }

        #region Demais Actions: BTN | RBL | DATABOUND [Recursos | Exercicio 1]
        protected void btnAdicionarRecursoExercicio1_Click(object sender, EventArgs e)
        {
            var recurso = new ServicoRecursoFinanceiroPrivadoFonteRecursoInfo();
            recurso.NomeFonteRecurso = txtNomeRecursoExercicio1.Text;
            recurso.ValorFonteRecurso = String.IsNullOrEmpty(txtValorRecursoExercicio1.Text) ? 0M : Convert.ToDecimal(txtValorRecursoExercicio1.Text);
            recurso.Liberado = RetornaValidacaoBloqueioDesbloqueio(2022);

            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Add("class", "active");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Remove("class");

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

            SessaoFontesRecursosExercicio1 = SessaoFontesRecursosExercicio1 ?? new List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>();
            SessaoFontesRecursosExercicio1.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio1();

            txtNomeRecursoExercicio1.Text = String.Empty;
            txtValorEstadualizadoExercicio1.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio1.Visible = true;

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
                    SessaoFontesRecursosExercicio1.RemoveAt(e.Item.DataItemIndex);
                    CarregarRecursosFinanceirosFonteRecursosExercicio1();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        protected void rblConvenioEstadualizadoExercicio1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Add("class", "active");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Remove("class");

            if (rblConvenioEstadualizadoExercicio1.SelectedValue == "0")
            {
                txtValorEstadualizadoExercicio1.Text = "0,00";
                rblMotivoEstadualizadoExercicio1.ClearSelection();
            }

            trValorAnualConvenioExercicio1.Visible = trMotivoConvenioEstadualizadoExercicio1.Visible = rblConvenioEstadualizadoExercicio1.SelectedValue == "1";
        }
        protected void rblOutrasFontesExercicio1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Add("class", "active");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Remove("class");
            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio1.SelectedValue == "0")
            {
                txtValorEstadualizadoExercicio1.Text = "0,00";
                txtNomeRecursoExercicio1.Text = String.Empty;
                this.SessaoFontesRecursosExercicio1 = null;
                lstRecursosAdicionadosExercicio1.DataSource = this.SessaoFontesRecursosExercicio1;
                lstRecursosAdicionadosExercicio1.DataBind();
                lstRecursosAdicionadosExercicio1.Visible = false;
                //rblMotivoEstadualizado.ClearSelection();
            }

            trValorEstadualizadoExercicio1.Visible = trMotivoEstadualizadoExercicio1.Visible = rblOutrasFontesExercicio1.SelectedValue == "1";
        }
        #endregion

        #region Demais Actions: BTN | RBL | DATABOUND [Recursos | Exercicio 2]
        protected void btnAdicionarRecursoExercicio2_Click(object sender, EventArgs e)
        {
            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Add("class", "active");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Remove("class");

            var recurso = new ServicoRecursoFinanceiroPrivadoFonteRecursoInfo();
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

            SessaoFontesRecursosExercicio2 = SessaoFontesRecursosExercicio2 ?? new List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>();
            SessaoFontesRecursosExercicio2.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio2();

            txtNomeRecursoExercicio2.Text = String.Empty;
            txtValorEstadualizadoExercicio2.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio2.Visible = true;
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
                    SessaoFontesRecursosExercicio2.RemoveAt(e.Item.DataItemIndex);
                    CarregarRecursosFinanceirosFonteRecursosExercicio2();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        protected void rblConvenioEstadualizadoExercicio2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Add("class", "active");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Remove("class");

            if (rblConvenioEstadualizadoExercicio2.SelectedValue == "0")
            {
                txtValorEstadualizadoExercicio2.Text = "0,00";
                rblMotivoEstadualizadoExercicio2.ClearSelection();
            }

            trValorAnualConvenioExercicio2.Visible = trMotivoConvenioEstadualizadoExercicio2.Visible = rblConvenioEstadualizadoExercicio2.SelectedValue == "1";
        }
        protected void rblOutrasFontesExercicio2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            CarregarAbaInicialRecursosFinanceiros();
            /*frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Add("class", "active");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Remove("class");*/

            if (rblOutrasFontesExercicio2.SelectedValue == "0")
            {
                txtValorEstadualizadoExercicio2.Text = "0,00";
                txtNomeRecursoExercicio2.Text = String.Empty;
                this.SessaoFontesRecursosExercicio2 = null;
                lstRecursosAdicionadosExercicio2.DataSource = this.SessaoFontesRecursosExercicio2;
                lstRecursosAdicionadosExercicio2.DataBind();
                lstRecursosAdicionadosExercicio2.Visible = false;
                //rblMotivoEstadualizado.ClearSelection();
            }

            trValorEstadualizadoExercicio2.Visible = trMotivoEstadualizadoExercicio2.Visible = rblOutrasFontesExercicio2.SelectedValue == "1";
            ValidaBloqueioDesbloqueio();
        }
        #endregion



        #region Demais Actions: BTN | RBL | DATABOUND [Recursos | Exercicio 3]
        protected void btnAdicionarRecursoExercicio3_Click(object sender, EventArgs e)
        {
            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Add("class", "active");
            frame1_5_Ano4.Attributes.Remove("class");



            var recurso = new ServicoRecursoFinanceiroPrivadoFonteRecursoInfo();
            recurso.NomeFonteRecurso = txtNomeRecursoExercicio3.Text;
            recurso.ValorFonteRecurso = String.IsNullOrEmpty(txtValorRecursoExercicio3.Text) ? 0M : Convert.ToDecimal(txtValorRecursoExercicio3.Text);
            recurso.Liberado = RetornaValidacaoBloqueioDesbloqueio(2024);

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

            SessaoFontesRecursosExercicio3 = SessaoFontesRecursosExercicio3 ?? new List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>();
            SessaoFontesRecursosExercicio3.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio3();

            txtNomeRecursoExercicio3.Text = String.Empty;
            txtValorEstadualizadoExercicio3.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio3.Visible = true;
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
                    SessaoFontesRecursosExercicio3.RemoveAt(e.Item.DataItemIndex);
                    CarregarRecursosFinanceirosFonteRecursosExercicio3();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        protected void rblConvenioEstadualizadoExercicio3_SelectedIndexChanged(object sender, EventArgs e)
        {

            SessaoPmas.VerificarSessao(this);
            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Add("class", "active");
            frame1_5_Ano4.Attributes.Remove("class");


            if (rblConvenioEstadualizadoExercicio3.SelectedValue == "0")
            {
                txtValorEstadualizadoExercicio3.Text = "0,00";
                rblMotivoEstadualizadoExercicio3.ClearSelection();
            }

            trValorAnualConvenioExercicio3.Visible = trMotivoConvenioEstadualizadoExercicio3.Visible = rblConvenioEstadualizadoExercicio3.SelectedValue == "1";
        }
        protected void rblOutrasFontesExercicio3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Add("class", "active");
            frame1_5_Ano4.Attributes.Remove("class");

            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio3.SelectedValue == "0")
            {
                txtValorEstadualizadoExercicio3.Text = "0,00";
                txtNomeRecursoExercicio3.Text = String.Empty;
                this.SessaoFontesRecursosExercicio3 = null;
                lstRecursosAdicionadosExercicio3.DataSource = this.SessaoFontesRecursosExercicio3;
                lstRecursosAdicionadosExercicio3.DataBind();
                lstRecursosAdicionadosExercicio3.Visible = false;
                //rblMotivoEstadualizado.ClearSelection();
            }

            trValorEstadualizadoExercicio3.Visible = trMotivoEstadualizadoExercicio3.Visible = rblOutrasFontesExercicio3.SelectedValue == "1";
            ValidaBloqueioDesbloqueio();
        }
        #endregion


        #region Demais Actions: BTN | RBL | DATABOUND [Recursos | Exercicio 4]
        protected void btnAdicionarRecursoExercicio4_Click(object sender, EventArgs e)
        {
            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Add("class", "active");

            
            var recurso = new ServicoRecursoFinanceiroPrivadoFonteRecursoInfo();
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

            SessaoFontesRecursosExercicio4 = SessaoFontesRecursosExercicio4 ?? new List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>();
            SessaoFontesRecursosExercicio4.Add(recurso);

            CarregarRecursosFinanceirosFonteRecursosExercicio4();

            txtNomeRecursoExercicio4.Text = String.Empty;
            txtValorEstadualizadoExercicio4.Text = String.Empty;
            tbInconsistencias.Visible = false;
            tdlstRecursosAdicionadosExercicio4.Visible = true;
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
                    SessaoFontesRecursosExercicio4.RemoveAt(e.Item.DataItemIndex);
                    CarregarRecursosFinanceirosFonteRecursosExercicio4();
                    var script = Util.GetJavaScriptDialogOK("Fonte de Recurso removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        protected void rblConvenioEstadualizadoExercicio4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Add("class", "active");

            if (rblConvenioEstadualizadoExercicio4.SelectedValue == "0")
            {
                txtValorEstadualizadoExercicio4.Text = "0,00";
                rblMotivoEstadualizadoExercicio4.ClearSelection();
            }

            trValorAnualConvenioExercicio4.Visible = trMotivoConvenioEstadualizadoExercicio4.Visible = rblConvenioEstadualizadoExercicio4.SelectedValue == "1";
        }
        protected void rblOutrasFontesExercicio4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5_1_1.Attributes.Add("class", "active");
            frame1_5_Ano1.Attributes.Remove("class");
            frame1_5_Ano2.Attributes.Remove("class");
            frame1_5_Ano3.Attributes.Remove("class");
            frame1_5_Ano4.Attributes.Add("class", "active");
            
            CarregarAbaInicialRecursosFinanceiros();

            if (rblOutrasFontesExercicio4.SelectedValue == "0")
            {
                txtValorEstadualizadoExercicio4.Text = "0,00";
                txtNomeRecursoExercicio4.Text = String.Empty;
                this.SessaoFontesRecursosExercicio4 = null;
                lstRecursosAdicionadosExercicio4.DataSource = this.SessaoFontesRecursosExercicio4;
                lstRecursosAdicionadosExercicio4.DataBind();
                lstRecursosAdicionadosExercicio4.Visible = false;
            }

            trValorEstadualizadoExercicio4.Visible = trMotivoEstadualizadoExercicio4.Visible = rblOutrasFontesExercicio4.SelectedValue == "1";
            ValidaBloqueioDesbloqueio();
        }
        #endregion



        #region Changed: [Tipo Servico]
        private void AplicarTipoServicoChanged()
        {
            this.AplicarRegraExibicaoLayoutServicosChanged();
            this.AplicarRegraExibicaoDeLabels();
        }
        private void AplicarRegraExibicaoDeLabels()
        {

            if (ddlTipoServico.SelectedValue == "138" || ddlTipoServico.SelectedValue == "153" || ddlTipoServico.SelectedValue == "145")
            {
                lblIndicadorPeriodoCapacidade.Text = " famílias";
                lblIndicadorPeriodoMediaMensal.Text = " famílias";
            }
            else {
                lblIndicadorPeriodoCapacidadeLAPSC.Text = " pessoas";
                lblIndicadorPeriodoMediaMensalLAPSC.Text = " pessoas";
            }
        }
        private void AplicarRegraExibicaoLayoutServicosChanged()
        {
            #region Servico [LA PSC]
            bool oServicoEhDeLAPSC = (ddlTipoServico.SelectedValue == R_TIPO_SERVICO.SERVICO_PROTECAO_SOCIAL_ADOLESC_CUMPR_MEDIDA_SOCIOEDUCATIVA_LA_PSC.ToString());
            if (oServicoEhDeLAPSC)
            {
                layout_capacidade_la_psc.Visible = true;
                layout_media_mensal_la_psc.Visible = true;

                layout_capacidade.Visible = false;
                layout_media_mensal.Visible = false;
            }
            else
            {
                layout_capacidade_la_psc.Visible = false;
                layout_media_mensal_la_psc.Visible = false;

                layout_capacidade.Visible = true;
                layout_media_mensal.Visible = true;
            }
            #endregion

            #region Servico [NÃO TIPIFICADO]
            tbNaoTipificado.Visible = tbNaoTipificadoObjetivo.Visible = ddlTipoServico.SelectedValue == "138" || ddlTipoServico.SelectedValue == "145";
            tbNaoTipificadoDetalhado.Visible = tbNaoTipificadoObjetivo.Visible = ddlTipoServico.SelectedValue == "153";
            #endregion

        }
        #endregion

        private void AplicarRegraSalvarRecursoPrograma()
        {
            //DBM: Validar
            #region Regra Salvar Recurso programa
            
            var desbloqueadoExercicio1 = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .Single(x => x.Exercicio == Exercicios[0] && x.IdRefBloqueio == 19).Desbloqueado.Value;

            var desbloqueadoExercicio2 = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .Single(x => x.Exercicio == Exercicios[1] && x.IdRefBloqueio == 19).Desbloqueado.Value;

            var desbloqueadoExercicio3 = SessaoPmas.UsuarioLogado.Prefeitura.PrefeiturasExerciciosBloqueio
                            .Single(x => x.Exercicio == Exercicios[2] && x.IdRefBloqueio == 19).Desbloqueado.Value;

            if (desbloqueadoExercicio1)
            {
                hdnExercicio.Value = FServicoRecursoFinanceiroPrivado.Exercicios[0].ToString();
            }
            else if (desbloqueadoExercicio2)
            {
                hdnExercicio.Value = FServicoRecursoFinanceiroPrivado.Exercicios[1].ToString();
            }
            else if (desbloqueadoExercicio3)
            {
                hdnExercicio.Value = FServicoRecursoFinanceiroPrivado.Exercicios[3].ToString();
            }

            #endregion
        }

        #region Helper [Funcionamento] [Servicos]
        private void DistribuirCapacidade(ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            ServicoRecursoFinanceiroPrivadoCapacidadeInfo capacidadeExercicio1 = servico.ServicosRecursosFinanceiroPrivadoCapacidade.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtCapacidadeExercicio1.Text = capacidadeExercicio1 != null ? capacidadeExercicio1.Capacidade != null ? capacidadeExercicio1.Capacidade.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoCapacidadeInfo capacidadeExercicio2 = servico.ServicosRecursosFinanceiroPrivadoCapacidade.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtCapacidadeExercicio2.Text = capacidadeExercicio2 != null ? capacidadeExercicio2.Capacidade != null ? capacidadeExercicio2.Capacidade.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoCapacidadeInfo capacidadeExercicio3 = servico.ServicosRecursosFinanceiroPrivadoCapacidade.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtCapacidadeExercicio3.Text = capacidadeExercicio3 != null ? capacidadeExercicio3.Capacidade != null ? capacidadeExercicio3.Capacidade.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoCapacidadeInfo capacidadeExercicio4 = servico.ServicosRecursosFinanceiroPrivadoCapacidade.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtCapacidadeExercicio4.Text = capacidadeExercicio4 != null ? capacidadeExercicio4.Capacidade != null ? capacidadeExercicio4.Capacidade.ToString() : "" : "";
        }
        private void DistribuirCapacidadeLA(ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo capacidadeLAExercicio1 = servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtCapacidadeLAExercicio1.Text = capacidadeLAExercicio1 != null ? capacidadeLAExercicio1.Capacidade != null ? capacidadeLAExercicio1.Capacidade.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo capacidadeLAExercicio2 = servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtCapacidadeLAExercicio2.Text = capacidadeLAExercicio2 != null ? capacidadeLAExercicio2.Capacidade != null ? capacidadeLAExercicio2.Capacidade.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo capacidadeLAExercicio3 = servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtCapacidadeLAExercicio3.Text = capacidadeLAExercicio3 != null ? capacidadeLAExercicio3.Capacidade != null ? capacidadeLAExercicio3.Capacidade.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo capacidadeLAExercicio4 = servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtCapacidadeLAExercicio4.Text = capacidadeLAExercicio4 != null ? capacidadeLAExercicio4.Capacidade != null ? capacidadeLAExercicio4.Capacidade.ToString() : "" : "";
        }
        private void DistribuirCapacidadePSC(ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo capacidadePSCExercicio1 = servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtCapacidadePSCExercicio1.Text = capacidadePSCExercicio1 != null ? capacidadePSCExercicio1.Capacidade != null ? capacidadePSCExercicio1.Capacidade.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo capacidadePSCExercicio2 = servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtCapacidadePSCExercicio2.Text = capacidadePSCExercicio2 != null ? capacidadePSCExercicio2.Capacidade != null ? capacidadePSCExercicio2.Capacidade.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo capacidadePSCExercicio3 = servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtCapacidadePSCExercicio3.Text = capacidadePSCExercicio3 != null ? capacidadePSCExercicio3.Capacidade != null ? capacidadePSCExercicio3.Capacidade.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo capacidadePSCExercicio4 = servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtCapacidadePSCExercicio4.Text = capacidadePSCExercicio4 != null ? capacidadePSCExercicio4.Capacidade != null ? capacidadePSCExercicio4.Capacidade.ToString() : "" : "";
        }

        private void DistribuirMediaMensal(ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            ServicoRecursoFinanceiroPrivadoMediaMensalInfo mediaMensalExercicio1 = servico.ServicosRecursosFinanceiroPrivadoMediaMensal.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtMediaMensalExercicio1.Text = mediaMensalExercicio1 != null ? mediaMensalExercicio1.MediaMensal != null ? mediaMensalExercicio1.MediaMensal.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoMediaMensalInfo mediaMensalExercicio2 = servico.ServicosRecursosFinanceiroPrivadoMediaMensal.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtMediaMensalExercicio2.Text = mediaMensalExercicio2 != null ? mediaMensalExercicio2.MediaMensal != null ? mediaMensalExercicio2.MediaMensal.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoMediaMensalInfo mediaMensalExercicio3 = servico.ServicosRecursosFinanceiroPrivadoMediaMensal.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtMediaMensalExercicio3.Text = mediaMensalExercicio3 != null ? mediaMensalExercicio3.MediaMensal != null ? mediaMensalExercicio3.MediaMensal.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoMediaMensalInfo mediaMensalExercicio4 = servico.ServicosRecursosFinanceiroPrivadoMediaMensal.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtMediaMensalExercicio4.Text = mediaMensalExercicio4 != null ? mediaMensalExercicio4.MediaMensal != null ? mediaMensalExercicio4.MediaMensal.ToString() : "" : "";
        }
        private void DistribuirMediaMensalLA(ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo mediaMensalLAExercicio1 = servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtMediaMensalLAExercicio1.Text = mediaMensalLAExercicio1 != null ? mediaMensalLAExercicio1.MediaMensal != null ? mediaMensalLAExercicio1.MediaMensal.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo mediaMensalLAExercicio2 = servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtMediaMensalLAExercicio2.Text = mediaMensalLAExercicio2 != null ? mediaMensalLAExercicio2.MediaMensal != null ? mediaMensalLAExercicio2.MediaMensal.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo mediaMensalLAExercicio3 = servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtMediaMensalLAExercicio3.Text = mediaMensalLAExercicio3 != null ? mediaMensalLAExercicio3.MediaMensal != null ? mediaMensalLAExercicio3.MediaMensal.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo mediaMensalLAExercicio4 = servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtMediaMensalLAExercicio4.Text = mediaMensalLAExercicio4 != null ? mediaMensalLAExercicio4.MediaMensal != null ? mediaMensalLAExercicio4.MediaMensal.ToString() : "" : "";

        }
        private void DistribuirMediaMensalPSC(ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo mediaMensalPSCExercicio1 = servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
            txtMediaMensalPSCExercicio1.Text = mediaMensalPSCExercicio1 != null ? mediaMensalPSCExercicio1.MediaMensal != null ? mediaMensalPSCExercicio1.MediaMensal.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo mediaMensalPSCExercicio2 = servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
            txtMediaMensalPSCExercicio2.Text = mediaMensalPSCExercicio2 != null ? mediaMensalPSCExercicio2.MediaMensal != null ? mediaMensalPSCExercicio2.MediaMensal.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo mediaMensalPSCExercicio3 = servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
            txtMediaMensalPSCExercicio3.Text = mediaMensalPSCExercicio3 != null ? mediaMensalPSCExercicio3.MediaMensal != null ? mediaMensalPSCExercicio3.MediaMensal.ToString() : "" : "";

            ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo mediaMensalPSCExercicio4 = servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC.Where(x => x.Exercicio == Exercicios[3]).FirstOrDefault();
            txtMediaMensalPSCExercicio4.Text = mediaMensalPSCExercicio4 != null ? mediaMensalPSCExercicio4.MediaMensal != null ? mediaMensalPSCExercicio4.MediaMensal.ToString() : "" : "";
        }
        #endregion

        #region helpers
        protected void verificarAlteracoes(Int32 idProgramaProjeto)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro42.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 42, idProgramaProjeto);
                    linkAlteracoesQuadro42.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("42")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idProgramaProjeto.ToString()));
                }
            }
        }
        protected void CamposBindEventos()
        {
            #region Recursos financeiros (quadrienal)

            #region Exercicio 1
            txtFMASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            //SOMENTE NO PRIVADO
            txtValorRecursoExclusivoExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorEstadualizadoExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #region Exercicio 2
            txtFMASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCAExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCAExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCAExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnteriorExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMIExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExclusivoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorEstadualizadoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
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
            txtFMIExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExclusivoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorEstadualizadoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

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
            txtFMIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNIExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExclusivoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorEstadualizadoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorRecursoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

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


            #endregion

            txtSemEscolaridade.Attributes.Add("onblur", "CalculateTotal()");
            txtNivelFundamental.Attributes.Add("onblur", "CalculateTotal()");
            txtNivelMedio.Attributes.Add("onblur", "CalculateTotal()");
            txtSuperior.Attributes.Add("onblur", "CalculateTotal()");

        }
        private void ClearTipoServico()
        {
            txtMediaMensalExercicio1.Text = String.Empty;
            txtMediaMensalExercicio2.Text = String.Empty;
            txtMediaMensalExercicio3.Text = String.Empty;
            txtMediaMensalExercicio4.Text = String.Empty;

            txtMediaMensalLAExercicio1.Text = String.Empty;
            txtMediaMensalLAExercicio2.Text = String.Empty;
            txtMediaMensalLAExercicio3.Text = String.Empty;
            txtMediaMensalLAExercicio4.Text = String.Empty;

            txtMediaMensalPSCExercicio1.Text = String.Empty;
            txtMediaMensalPSCExercicio2.Text = String.Empty;
            txtMediaMensalPSCExercicio3.Text = String.Empty;
            txtMediaMensalPSCExercicio4.Text = String.Empty;

            txtCapacidadeExercicio1.Text = String.Empty;
            txtCapacidadeExercicio2.Text = String.Empty;
            txtCapacidadeExercicio3.Text = String.Empty;
            txtCapacidadeExercicio4.Text = String.Empty;

            txtCapacidadeLAExercicio1.Text = String.Empty;
            txtCapacidadeLAExercicio2.Text = String.Empty;
            txtCapacidadeLAExercicio3.Text = String.Empty;
            txtCapacidadeLAExercicio4.Text = String.Empty;

            txtCapacidadePSCExercicio1.Text = String.Empty;
            txtCapacidadePSCExercicio2.Text = String.Empty;
            txtCapacidadePSCExercicio3.Text = String.Empty;
            txtCapacidadePSCExercicio4.Text = String.Empty;
        }
        private void ClearPrograma()
        {
            rptProgramaTemp.DataSource = null;
            rptProgramaTemp.DataBind();
        }
        private void ClearSessao()
        {

            SessaoProgramaProjetoCofinanciamentoExercicio = null;
            SessaoTransferenciaRendaCofinanciamentoExercicio = null;
            SessaoConsultaProgramaProjetoServicoCofinanciamentoExercicio = null;
            SessaoPrefeituraBeneficioEventualServicoExercicio = null;
        }
        private void DefinirFrame1ComoPrincipal()
        {

            frame1_1_1.Attributes.Add("class", "frame active");
            frame1_2_1.Attributes.Remove("class");
            frame1_3_1.Attributes.Remove("class");
            frame1_4_1.Attributes.Remove("class");
            frame1_5_1_1.Attributes.Remove("class");
        }
        private void ClearSituacoes()
        {
            lstSituacoesEspecificas.DataTextField = "Nome";
            lstSituacoesEspecificas.DataValueField = "Id";
            lstSituacoesEspecificas.DataSource = new List<SituacaoEspecificaInfo>();
            lstSituacoesEspecificas.DataBind();
        }
        private void CarregarAbaInicialRecursosFinanceiros()
        {
            hdnExercicio.Value = (hdnExercicio.Value == string.Empty) ? FServicoRecursoFinanceiroPrivado.Exercicios[0].ToString() : hdnExercicio.Value;
            frame1_5_1_1.Attributes.Add("class", "active");

            if (hdnExercicio.Value == FServicoRecursoFinanceiroPrivado.Exercicios[0].ToString())
            {
                frame1_5_Ano1.Attributes.Add("class", "active");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Remove("class");
            }
            else if (hdnExercicio.Value == FServicoRecursoFinanceiroPrivado.Exercicios[1].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Add("class", "active");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Remove("class");
            }

            else if (hdnExercicio.Value == FServicoRecursoFinanceiroPrivado.Exercicios[2].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Add("class", "active"); 
                frame1_5_Ano4.Attributes.Remove("class");
            }
            else if (hdnExercicio.Value == FServicoRecursoFinanceiroPrivado.Exercicios[3].ToString())
            {
                frame1_5_Ano1.Attributes.Remove("class");
                frame1_5_Ano2.Attributes.Remove("class");
                frame1_5_Ano3.Attributes.Remove("class");
                frame1_5_Ano4.Attributes.Add("class", "active"); 
            }
        
        }
        protected string MontarBotao(ConsultaProgramaProjetoServicoCofinanciamentoInfo item)
        {
            var idProjeto = item.IdServicoRecursoFinanceiro;// Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));

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

        private void AposSalvoNaoPermitirEdicaoCamposCaracterizacao(ServicoRecursoFinanceiroPrivadoInfo servico)
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
        #region WebControls : Selecionar : Bloqueio Desbloqueio
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
                                 txtNomeRecursoExercicio1,
                                 txtValorRecursoExercicio1,
                                 txtValorRecursoExclusivoExercicio1,
                                 txtValorEstadualizadoExercicio1,
                                 rblOutrasFontesExercicio1,
                                 rblMotivoEstadualizadoExercicio1,
                                 rblConvenioEstadualizadoExercicio1,
                                 btnAdicionarRecursoExercicio1,

                                 #region Funcionamento
                                 txtCapacidadeExercicio1,
                                 txtCapacidadeLAExercicio1,
                                 txtCapacidadePSCExercicio1, 
	                                      
                                 txtMediaMensalExercicio1,
                                 txtMediaMensalLAExercicio1,
                                 txtMediaMensalPSCExercicio1
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

                                             txtNomeRecursoExercicio2,
                                             txtValorRecursoExercicio2,

                                             txtValorRecursoExclusivoExercicio2,
                                             txtValorEstadualizadoExercicio2,

                                             rblOutrasFontesExercicio2,
                                             rblMotivoEstadualizadoExercicio2,
                                             rblConvenioEstadualizadoExercicio2,

                                             btnAdicionarRecursoExercicio2,

                                             #region Funcionamento
                                             txtCapacidadeExercicio2,
                                             txtCapacidadeLAExercicio2,
                                             txtCapacidadePSCExercicio2, 
	                                      
                                             txtMediaMensalExercicio2,
                                             txtMediaMensalLAExercicio2,
                                             txtMediaMensalPSCExercicio2
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

                                             txtNomeRecursoExercicio3,
                                             txtValorRecursoExercicio3,

                                             txtValorRecursoExclusivoExercicio3,
                                             txtValorEstadualizadoExercicio3,

                                             rblOutrasFontesExercicio3,
                                             rblMotivoEstadualizadoExercicio3,
                                             rblConvenioEstadualizadoExercicio3,

                                             rblCriancasAuxilioReclusao,
                                             txtCriancaAuxilioReclusaoFeitos,
                                             txtCriancaAuxilioReclusaoAprovados,
                                             txtCriancaAuxilioReclusaoNegado,
                                             
                                             rblCriancasPensaoMorte,
                                             txtCriancasPensaoMorteFeitos,
                                             txtCriancasPensaoMorteAprovados,
                                             txtCriancasPensaoMorteNegado,

                                             btnAdicionarRecursoExercicio3,

                                             #region Funcionamento
                                             txtCapacidadeExercicio3,
                                             txtCapacidadeLAExercicio3,
                                             txtCapacidadePSCExercicio3, 
	                                      
                                             txtMediaMensalExercicio3,
                                             txtMediaMensalLAExercicio3,
                                             txtMediaMensalPSCExercicio3
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

                                             txtNomeRecursoExercicio4,
                                             txtValorRecursoExercicio4,

                                             txtValorRecursoExclusivoExercicio4,
                                             txtValorEstadualizadoExercicio4,

                                             rblOutrasFontesExercicio4,
                                             rblMotivoEstadualizadoExercicio4,
                                             rblConvenioEstadualizadoExercicio4,
                                             
			                                 rblCriancasAuxilioReclusaoExercicio2025,
                                             txtCriancaAuxilioReclusaoFeitosExercicio2025,
                                             txtCriancaAuxilioReclusaoAprovadosExercicio2025,
                                             txtCriancaAuxilioReclusaoNegadoExercicio2025,
                                             
                                             rblCriancasPensaoMorteExercicio2025,
                                             txtCriancasPensaoMorteFeitosExercicio2025,
                                             txtCriancasPensaoMorteAprovadosExercicio2025,
                                             txtCriancasPensaoMorteNegadoExercicio2025,

                                             btnAdicionarRecursoExercicio4,

                                             #region Funcionamento
                                             txtCapacidadeExercicio4,
                                             txtCapacidadeLAExercicio4,
                                             txtCapacidadePSCExercicio4, 
	                                      
                                             txtMediaMensalExercicio4,
                                             txtMediaMensalLAExercicio4,
                                             txtMediaMensalPSCExercicio4
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

        #endregion

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
            WebControl[] controleDemandasExercicio1 = SelecionarControlesDemandasExercicio1();
            WebControl[] controleDemandasExercicio2 = SelecionarControlesDemandasExercicio2();
            WebControl[] controleDemandasExercicio3 = SelecionarControlesDemandasExercicio3();
            WebControl[] controleDemandasExercicio4 = SelecionarControlesDemandasExercicio4();
            #endregion

            #region Regra: Bloqueio: Campos Recursos Financeiros
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIRecursosFinanceiros(controles1, FServicoRecursoFinanceiroPrivado.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIRecursosFinanceiros(controles2, FServicoRecursoFinanceiroPrivado.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIRecursosFinanceiros(controles3, FServicoRecursoFinanceiroPrivado.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIRecursosFinanceiros(controles4, FServicoRecursoFinanceiroPrivado.Exercicios[3]);
            #endregion

            #region Regra: Bloqueio: Campos Reprogramacao
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio1, FServicoRecursoFinanceiroPrivado.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio2, FServicoRecursoFinanceiroPrivado.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio3, FServicoRecursoFinanceiroPrivado.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIReprogramacao(controlesReprogramacaoExercicio4, FServicoRecursoFinanceiroPrivado.Exercicios[3]);
            #endregion

            #region Regra: Bloqueio: Campos Demandas
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIDemandas(controleDemandasExercicio1,FServicoRecursoFinanceiroPrivado.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIDemandas(controleDemandasExercicio2, FServicoRecursoFinanceiroPrivado.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIDemandas(controleDemandasExercicio3, FServicoRecursoFinanceiroPrivado.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIDemandas(controleDemandasExercicio4, FServicoRecursoFinanceiroPrivado.Exercicios[3]);
            #endregion

            #region Regra: Bloqueio: Botao Salvar
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIBotaoSalvar(btnSalvarExercicio1, FServicoRecursoFinanceiroPrivado.Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIBotaoSalvar(btnSalvarExercicio2, FServicoRecursoFinanceiroPrivado.Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIBotaoSalvar(btnSalvarExercicio3, FServicoRecursoFinanceiroPrivado.Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIBotaoSalvar(btnSalvarExercicio4, FServicoRecursoFinanceiroPrivado.Exercicios[3]);

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
                trConsorcioPrivado.Visible = false;
            }
        }
        protected void ddlFormaJuridica_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormaJuridica.SelectedValue == "1")
            {
                trConsorcioPrivado.Visible = true;
            }
            else
            {
                trConsorcioPrivado.Visible = false;
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

        protected void rblContraPartida1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_5_1_1.Attributes.Add("class", "active");
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
            frame1_5_1_1.Attributes.Add("class", "active");
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
            frame1_5_1_1.Attributes.Add("class", "active");
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
            frame1_5_1_1.Attributes.Add("class", "active");
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

        protected void rblCriancasAuxilioReclusao_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_1_1.Attributes.Remove("class");
            frame1_2_1.Attributes.Add("class", "frame active");
            frame1_3_1.Attributes.Remove("class");
            frame1_4_1.Attributes.Remove("class");
            frame1_5_1_1.Attributes.Remove("class");

            if (rblCriancasAuxilioReclusao.SelectedValue == "1")
            {
                trValCriancasAuxilioReclusao.Visible = true;
            }
            else
            {
                trValCriancasAuxilioReclusao.Visible = false;
            }
        }


        protected void rblCriancasPensaoMorte_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_1_1.Attributes.Remove("class");
            frame1_2_1.Attributes.Add("class", "frame active");
            frame1_3_1.Attributes.Remove("class");
            frame1_4_1.Attributes.Remove("class");
            frame1_5_1_1.Attributes.Remove("class");

            if (rblCriancasPensaoMorte.SelectedValue == "1")
            {
                trValCriancasPensaoMorte.Visible = true;
            }
            else
            {
                trValCriancasPensaoMorte.Visible = false;
            }
        }

        protected void rblCriancasAuxilioReclusaoExercicio2025_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_1_1.Attributes.Remove("class");
            frame1_2_1.Attributes.Add("class", "frame active");
            frame1_3_1.Attributes.Remove("class");
            frame1_4_1.Attributes.Remove("class");
            frame1_5_1_1.Attributes.Remove("class");
            frame2_5_ano2.Attributes.Add("class", "active");

            if (rblCriancasAuxilioReclusaoExercicio2025.SelectedValue == "1")
            {
                trValCriancasAuxilioReclusaoExercicio2025.Visible = true;
            }
            else
            {
                trValCriancasAuxilioReclusaoExercicio2025.Visible = false;
            }
        }

        protected void rblCriancasPensaoMorteExercicio2025_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_1_1.Attributes.Remove("class");
            frame1_2_1.Attributes.Add("class", "frame active");
            frame1_3_1.Attributes.Remove("class");
            frame1_4_1.Attributes.Remove("class");
            frame1_5_1_1.Attributes.Remove("class");
            frame2_5_ano2.Attributes.Add("class", "active");

            if (rblCriancasPensaoMorteExercicio2025.SelectedValue == "1")
            {
                trValCriancasPensaoMorteExercicio2025.Visible = true;
            }
            else
            {
                trValCriancasPensaoMorteExercicio2025.Visible = false;
            }

        }
    }
}
