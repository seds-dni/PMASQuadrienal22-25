using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Collections;
using System.Web.UI.HtmlControls;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoV
{
    public partial class FCronogramaDesembolso : System.Web.UI.Page
    {
        #region propriedades
        private Boolean possuiEtapaAlemDaRenda = false;
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        #endregion

        #region start
        protected void Page_Load(object sender, EventArgs e)
        {
            this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? "2022" : this.hdfAno.Value;

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null || String.IsNullOrEmpty(Request.QueryString["idTipo"]))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                adicionarEventos();

                preencherTitulo();

                carregarLinksNavegacao();

                using (var proxy = new ProxyPrefeitura())
                {
                    load(proxy);
                }

                #region Nome dos quadros para abas 5-Financiamento
                Int32 idQuadro = 0;

                switch (lblNumeracao.Text)
                {
                    case "5.5.A":
                        idQuadro = 62;
                        break;
                    case "5.5.B":
                        idQuadro = 64;
                        break;
                    case "5.5.C":
                        idQuadro = 66;
                        break;
                    case "5.5.D":
                        idQuadro = 88;
                        break;
                    case "5.5.E":
                        idQuadro = 90;
                        break;
                }
                #endregion

                verificarAlteracoes(idQuadro);


                LoadExercicios();

            }

        }


        #endregion


        private void LoadExercicios()
        {
            this.btnExercicio1.Text = FCronogramaDesembolso.Exercicios[0].ToString();
            this.btnExercicio2.Text = FCronogramaDesembolso.Exercicios[1].ToString();
            this.btnExercicio3.Text = FCronogramaDesembolso.Exercicios[2].ToString();
            this.btnExercicio4.Text = FCronogramaDesembolso.Exercicios[3].ToString();

            if (FCronogramaDesembolso.Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }
            if (FCronogramaDesembolso.Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FCronogramaDesembolso.Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FCronogramaDesembolso.Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-info-seds";
            }
        }

        public void btnLoadExercicio1_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[0].ToString();
            CarregarLabelsPorExercicio(Exercicios[0]);
            ClearExercicio();
            using (var proxy = new ProxyPrefeitura())
            {
                load(proxy);
            }

        }

        protected void btnLoadExercicio2_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[1].ToString();
            CarregarLabelsPorExercicio(Exercicios[1]);
            using (var proxy = new ProxyPrefeitura())
            {
                load(proxy);
            }
        }

        protected void btnLoadExercicio3_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[2].ToString();
            CarregarLabelsPorExercicio(Exercicios[2]);
            using (var proxy = new ProxyPrefeitura())
            {
                load(proxy);
            }
        }

        protected void btnLoadExercicio4_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[3].ToString();
            CarregarLabelsPorExercicio(Exercicios[3]);
            using (var proxy = new ProxyPrefeitura())
            {
                load(proxy);
            }
        }




        private void ClearExercicio()
        {
            #region Aba - Protecao basica - 5.5.A
            #region Rede Indireta
            #region [Indireta] Reprogramacao - Parcela Única
            txtRecursosReprogramadoPrivado.Text = "0,00";
            txtRecursosHumanosReprogramadoPrivado.Text = "0,00";
            txtOutrasCusteioReprogramadoPrivado.Text = "0,00";
            txtEquipamentosPrivadoReprogramado.Text = "0,00";

			txtEquipamentosPrivadoReprogramado.Text = "0,00";
			txtObrasReprogramadoPrivado.Text = "0,00";

			txtDemandasParlamentaresPrivado.Text = "0,00";
			txtRecursosHumanosDemandasPrivado.Text = "0,00";
			txtOutrasCusteioDemandasPrivado.Text = "0,00";
			txtEquipamentosPrivadoDemandas.Text = "0,00";
            txtObrasDemandasPrivado.Text = "0,00";

            txtReprogramadoDemandasParlamentaresPrivado.Text = "0,00";
            txtRecursosHumanosReprogramadoDemandasPrivado.Text = "0,00";
            txtOutrasCusteioReprogramadoDemandasPrivado.Text = "0,00";
            txtEquipamentosPrivadoReprogramadoDemandas.Text = "0,00";
            txtObrasReprogramadoDemandasPrivado.Text = "0,00";

            #endregion

            #region [Indireta] col 1 - TOT
            txtTot1.Text = "0,00";
            txtTot2.Text = "0,00";
            txtTot3.Text = "0,00";
            txtTot4.Text = "0,00";
            txtTot5.Text = "0,00";
            txtTot6.Text = "0,00";
            txtTot7.Text = "0,00";
            txtTot8.Text = "0,00";
            txtTot9.Text = "0,00";
            txtTot10.Text = "0,00";
            txtTot11.Text = "0,00";
            txtTot12.Text = "0,00";
            #endregion

            #region [Indireta] col 2 - RH
            txtRH1.Text = "0,00";
            txtRH2.Text = "0,00";
            txtRH3.Text = "0,00";
            txtRH4.Text = "0,00";
            txtRH5.Text = "0,00";
            txtRH6.Text = "0,00";
            txtRH7.Text = "0,00";
            txtRH8.Text = "0,00";
            txtRH9.Text = "0,00";
            txtRH10.Text = "0,00";
            txtRH11.Text = "0,00";
            txtRH12.Text = "0,00";
            #endregion

            #region [Indireta] col 3 - MC
            txtMC1.Text = "0,00";
            txtMC2.Text = "0,00";
            txtMC3.Text = "0,00";
            txtMC4.Text = "0,00";
            txtMC5.Text = "0,00";
            txtMC6.Text = "0,00";
            txtMC7.Text = "0,00";
            txtMC8.Text = "0,00";
            txtMC9.Text = "0,00";
            txtMC10.Text = "0,00";
            txtMC11.Text = "0,00";
            txtMC12.Text = "0,00";
            #endregion

            #region [Indireta] col 4 - ST
            txtST1.Text = "0,00";
            txtST2.Text = "0,00";
            txtST3.Text = "0,00";
            txtST4.Text = "0,00";
            txtST5.Text = "0,00";
            txtST6.Text = "0,00";
            txtST7.Text = "0,00";
            txtST8.Text = "0,00";
            txtST9.Text = "0,00";
            txtST10.Text = "0,00";
            txtST11.Text = "0,00";
            txtST12.Text = "0,00";
            #endregion

            #endregion
            #region Rede Direta
            #region [Direta] Reprogramacao - Parcela Única
            txtReprogramacaoRH.Text = "0,00";
            txtReprogramacaoCusteio.Text = "0,00";
            txtReprogramacaoInvestimento.Text = "0,00";
            txtReprogramacaoRecursosDisponibilizados.Text = "0,00";
            txtReprogramacaoObras.Text = "0,00";
			txtReprogramacaoInvestimento.Text = "0,00";
			
			txtDemandasParlamentaresDisponibilizados.Text = "0,00";
			txtDemandasRH.Text = "0,00";
			txtDemandasCusteio.Text = "0,00";
			txtDemandasInvestimento.Text = "0,00";
            txtDemandasObras.Text = "0,00";

            txtReprogramadoDemandasParlamentaresDisponibilizados.Text = "0,00";
            txtReprogramadoDemandasRH.Text = "0,00";
            txtReprogramadoDemandasCusteio.Text = "0,00";
            txtReprogramadoDemandasInvestimento.Text = "0,00";
            txtReprogramadoDemandasObras.Text = "0,00";

            lblTotalRecursosReprogramadoAnoAnterior.Text = "0,00";
            lblTotalDemandasParlamentares.Text = "0,00";
            lblTotalDemandasParlamentaresPrivada.Text = "0,00";
            lblTotalRecursosReprogramadoPrivada.Text = "0,00";
            lblTotalReprogramadoDemandasParlamentares.Text = "0,00";
            lblTotalReprogramadoDemandasParlamentaresPrivada.Text = "0,00";
            #endregion

            #region [direta] col 1 - Tot Publica
            txtTot1Publica.Text = "0,00";
            txtTot2Publica.Text = "0,00";
            txtTot3Publica.Text = "0,00";
            txtTot4Publica.Text = "0,00";
            txtTot5Publica.Text = "0,00";
            txtTot6Publica.Text = "0,00";
            txtTot7Publica.Text = "0,00";
            txtTot8Publica.Text = "0,00";
            txtTot9Publica.Text = "0,00";
            txtTot10Publica.Text = "0,00";
            txtTot11Publica.Text = "0,00";
            txtTot12Publica.Text = "0,00";
            #endregion

            #region [direta] col 2 - Custeio
            txtCusteio1.Text = "0,00";
            txtCusteio2.Text = "0,00";
            txtCusteio3.Text = "0,00";
            txtCusteio4.Text = "0,00";
            txtCusteio5.Text = "0,00";
            txtCusteio6.Text = "0,00";
            txtCusteio7.Text = "0,00";
            txtCusteio8.Text = "0,00";
            txtCusteio9.Text = "0,00";
            txtCusteio10.Text = "0,00";
            txtCusteio11.Text = "0,00";
            txtCusteio12.Text = "0,00";
            #endregion

            #region [direta] col 3 - outro custeio
            txtOutroCusteio1.Text = "0,00";
            txtOutroCusteio2.Text = "0,00";
            txtOutroCusteio3.Text = "0,00";
            txtOutroCusteio4.Text = "0,00";
            txtOutroCusteio5.Text = "0,00";
            txtOutroCusteio6.Text = "0,00";
            txtOutroCusteio7.Text = "0,00";
            txtOutroCusteio8.Text = "0,00";
            txtOutroCusteio9.Text = "0,00";
            txtOutroCusteio10.Text = "0,00";
            txtOutroCusteio11.Text = "0,00";
            txtOutroCusteio12.Text = "0,00";
            #endregion

            #region [direta] col 4 - investimento
            txtInv1Publica.Text = "0,00";
            txtInv2Publica.Text = "0,00";
            txtInv3Publica.Text = "0,00";
            txtInv4Publica.Text = "0,00";
            txtInv5Publica.Text = "0,00";
            txtInv6Publica.Text = "0,00";
            txtInv7Publica.Text = "0,00";
            txtInv8Publica.Text = "0,00";
            txtInv9Publica.Text = "0,00";
            txtInv10Publica.Text = "0,00";
            txtInv11Publica.Text = "0,00";
            txtInv12Publica.Text = "0,00";
            #endregion

            #endregion
            #endregion
            #region 5.5.B

            lblTotalExecPublica1.Text = "0,00";
            lblTotalExecPublica2.Text = "0,00";
            lblTotalExecPublica3.Text = "0,00";
            lblTotalExecPublica4.Text = "0,00";
            lblTotalExecPublica5.Text = "0,00";
            lblTotalExecPublica6.Text = "0,00";
            lblTotalExecPublica7.Text = "0,00";
            lblTotalExecPublica8.Text = "0,00";
            lblTotalExecPublica9.Text = "0,00";
            lblTotalExecPublica10.Text = "0,00";
            lblTotalExecPublica11.Text = "0,00";
            lblTotalExecPublica12.Text = "0,00";
            lblTotalPublica.Text = "0,00";
            lblTotalOC.Text = "0,00";
            lblTotalCusteio.Text = "0,00";
            lblTotalExecPublica.Text = "0,00";
            lblTotalCofinanciamentoPublica.Text = "0,00";

            lblTotalExecPrivada1.Text = "0,00";
            lblTotalExecPrivada2.Text = "0,00";
            lblTotalExecPrivada3.Text = "0,00";
            lblTotalExecPrivada4.Text = "0,00";
            lblTotalExecPrivada5.Text = "0,00";
            lblTotalExecPrivada6.Text = "0,00";
            lblTotalExecPrivada7.Text = "0,00";
            lblTotalExecPrivada8.Text = "0,00";
            lblTotalExecPrivada9.Text = "0,00";
            lblTotalExecPrivada10.Text = "0,00";
            lblTotalExecPrivada11.Text = "0,00";
            lblTotalExecPrivada12.Text = "0,00";
            lblTotal.Text = "0,00";
            lblTotalRecursosHumanos.Text = "0,00";
            lblTotalMateriaisConsumo.Text = "0,00";
            lbltotalServicos.Text = "0,00";
            lblTotalObrasPrivada.Text = "0,00";
            lblTotalExecPrivada.Text = "0,00";
            lblTotalObraPublica.Text = "0,00";
            lblTotalInvestimentoPublica.Text = "0,00";

            #endregion

            #region Aba - Programas e projetos 5.6.D
            #region Obras publicas
            txtObras1.Text = "0,00";
            txtObras2.Text = "0,00";
            txtObras3.Text = "0,00";
            txtObras4.Text = "0,00";
            txtObras5.Text = "0,00";
            txtObras6.Text = "0,00";
            txtObras7.Text = "0,00";
            txtObras8.Text = "0,00";
            txtObras9.Text = "0,00";
            txtObras10.Text = "0,00";
            txtObras11.Text = "0,00";
            txtObras12.Text = "0,00";
            #endregion

            #region Obras Privadas
            txtObrasPrivada1.Text = "0,00";
            txtObrasPrivada2.Text = "0,00";
            txtObrasPrivada3.Text = "0,00";
            txtObrasPrivada4.Text = "0,00";
            txtObrasPrivada5.Text = "0,00";
            txtObrasPrivada6.Text = "0,00";
            txtObrasPrivada7.Text = "0,00";
            txtObrasPrivada8.Text = "0,00";
            txtObrasPrivada9.Text = "0,00";
            txtObrasPrivada10.Text = "0,00";
            txtObrasPrivada11.Text = "0,00";
            txtObrasPrivada12.Text = "0,00";
            #endregion
            #endregion

            lblValorExercicioAtualPrivado.Text = "0,00";
            lblRecursosExercicioAtual.Text = "0,00";
            lblTotalCofinanciamentoPublica.Text = "0,00";
            lblTotalCofinanciamento.Text = "0,00";
        }




        private void CarregarLabelsPorExercicio(int exercicio)
        {
            preencherTitulo();

            if (FCronogramaDesembolso.Exercicios[0] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-info-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";

                btnCalcular.Enabled = true;
                btnSalvar.Enabled = true;
                tbInconsistencias.Visible = false;
            }
            if (FCronogramaDesembolso.Exercicios[1] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-info-seds"; 
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";
                btnCalcular.Enabled = true;
                btnSalvar.Enabled = true;
                tbInconsistencias.Visible = false;
            }

            if (FCronogramaDesembolso.Exercicios[2] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-info-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";
                btnCalcular.Enabled = true;
                btnSalvar.Enabled = true;
                tbInconsistencias.Visible = false;
            }

            if (FCronogramaDesembolso.Exercicios[3] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-info-seds";
                btnCalcular.Enabled = true;
                btnSalvar.Enabled = true;
                tbInconsistencias.Visible = false;
            }


        }

        private void load(ProxyPrefeitura proxy)
        {
            ClearExercicio();
            loadPrivada(proxy);
            loadPublica(proxy);
            int exercicio = Convert.ToInt32(hdfAno.Value);

            int tipoProtecao = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));

            var lst = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(SessaoPmas.UsuarioLogado.Prefeitura.Id, tipoProtecao, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

            decimal totalpublica = Convert.ToDecimal(lst.Where(t => t.IdTipoUnidade != 2 && t.Desativado == false).Sum(t => t.PrevisaoOrcamentaria).ToString("n2"));
            decimal totalprivada = Convert.ToDecimal(lst.Where(t => t.IdTipoUnidade == 2 && t.Desativado == false).Sum(t => t.PrevisaoOrcamentaria).ToString("n2"));

            if (tipoProtecao != 4 && tipoProtecao != 5)
            {
                lstProgramasBeneficios.Visible = false;
                lstProgramasProjetos.Visible = false;
                lstRecursos.Visible = true;

                lstRecursos.DataSource = lst;
                lstRecursos.DataBind();

                tdReprogramacao.Visible = true;
                trProgramas.Visible = true;
                tdInvestimentoPrivado.RowSpan = 1;
                possuiEtapaAlemDaRenda = false;

                ajusteGridBasicaMediaAlta(); 

                tdTotalRedePublica.ColSpan = 6;
                lblValorCofinanciamentoText.Text = "Valor Total do Cofinanciamento da rede pública (exercício atual + reprogramação + Demandas Parlamentares + Reprogramação Demandas Parlamentares):&nbsp ";
                lblValorCofinanciamentoPrivadoText.Text = "Valor do cofinanciamento da rede privada (exercício atual + reprogramação + Demandas Parlamentares + Reprogramação Demandas Parlamentares):&nbsp;";

                txtTot1Publica.Visible = txtTot2Publica.Visible = txtTot3Publica.Visible = txtTot4Publica.Visible = txtTot5Publica.Visible = txtTot6Publica.Visible =
                txtTot7Publica.Visible = txtTot8Publica.Visible = txtTot9Publica.Visible = txtTot10Publica.Visible = txtTot11Publica.Visible = txtTot12Publica.Visible = true;

                //var reprogramado = proxy.Service.GetPrefeituraById(SessaoPmas.UsuarioLogado.Prefeitura.Id).ValoresReprogramadosDrads;
                //ExibirReprogramacao(reprogramado);


                //tdTotalExecucao.Visible = false;
                tdCusteio.Visible = false;
                tdInvestimento.Visible = false;
                trRHDC.Visible = true;
                trBeneficio.Visible = false;
                trHeaderBenenifios.Visible = false;
                trHeaderProgramas.Visible = true;
                tdOC01.Visible
                    = tdOC02.Visible
                    = tdOC03.Visible
                    = tdOC04.Visible
                    = tdOC05.Visible
                    = tdOC06.Visible
                    = tdOC07.Visible
                    = tdOC08.Visible
                    = tdOC09.Visible
                    = tdOC10.Visible
                    = tdOC11.Visible
                    = tdTotalOC.Visible
                    = tdOC12.Visible = true;

                //tdTotalRedePublica.ColSpan = 5;
                txtTot1.Visible = txtTot2.Visible = txtTot3.Visible = txtTot4.Visible = txtTot5.Visible = txtTot6.Visible =
                 txtTot7.Visible = txtTot8.Visible = txtTot9.Visible = txtTot10.Visible = txtTot11.Visible = txtTot12.Visible = true;
                if (Request.QueryString["idTipo"] != null && Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) == 1)
                {
                    lst.ToList().ForEach(m =>
                    {
                        if (m.Id == 0 && m.TipoUnidade.ToLower().Contains("além da renda"))
                        {
                            possuiEtapaAlemDaRenda = true;
                        }
                    });
                }

                if (possuiEtapaAlemDaRenda)
                {
                    tdRedePublica.ColSpan = 5;
                    tdItensDespesa.ColSpan = 3;
                    tdTotalRedePublica.ColSpan = 4;
                    tdLimparRedePublica.ColSpan = 5;
                    txtInv1Publica.Enabled = txtInv2Publica.Enabled = txtInv3Publica.Enabled = txtInv4Publica.Enabled = txtInv5Publica.Enabled = txtInv6Publica.Enabled =
                    txtInv7Publica.Enabled = txtInv8Publica.Enabled = txtInv9Publica.Enabled = txtInv10Publica.Enabled = txtInv11Publica.Enabled = txtInv12Publica.Enabled = true;
                }
                //else { 
                //txtInv1Publica.Enabled = txtInv2Publica.Enabled = txtInv3Publica.Enabled = txtInv4Publica.Enabled = txtInv5Publica.Enabled = txtInv6Publica.Enabled =
                //txtInv7Publica.Enabled = txtInv8Publica.Enabled = txtInv9Publica.Enabled = txtInv10Publica.Enabled = txtInv11Publica.Enabled = txtInv12Publica.Enabled = false;
                //
                //}
            }
            else if (tipoProtecao == 4)
            {
                trRecursosExercicioAtual.Visible = true;
                trRecursosReprogramados.Visible = true;
                trHeaderRecursosReprogramados.Visible = true;
                trHeaderRecursosReprogramadosPrivado.Visible = false;
                trRecursosReprogramadosPrivado.Visible = false;
                trRecursosExercicioAtualPrivado.Visible = false;
                trTotalReprogramacaoGeral.Visible = false;
                trExercicioAtualGeral.Visible = false;
                trHeaderDemandasParalamentares.Visible = false;
                trHeaderDemandasParlamentaresPrivado.Visible = false;
                trDemandasParlamentaresParcela.Visible = false;
                trDemandasParlamentaresParcelaPrivado.Visible = false;
                trHeaderReprogramadoDemandasParalamentares.Visible = false;
                trReprogramadoDemandasParlamentaresParcela.Visible = false;
                trHeaderReprogramadoDemandasParlamentaresPrivado.Visible = false;
                trReprogramadoDemandasParlamentaresParcelaPrivado.Visible = false;
                trTotalGeralDemandasParlamentares.Visible = false;
                trTotalGeralReprogramacaoDemandasParlamentares.Visible = false;
                tr2.Visible = false;
                tdTotalObraPublica.Visible = false;
                tdReprogramacaoObras.Visible = true;
                tdReprogramacaoRH.Visible = false;

                tdCofinanciamentoDemandas.Visible = false;
                tdCofinanciamentoReprogramado.Visible = true;
                tdCofinanciamentoReprogramadoDemandas.Visible = false;
                tdStatus.Visible = false;
                tdRecursosReprogramadosAnoAnterior.Visible = true;

                trRedeIndireta.Visible = false;
                trTotalIndireta.Visible = false;

                ajusteGridProgramasProjetos();

                calcularReprogramadoProgramasProjetos();

                lstProgramasProjetos.Visible = true;
                lstProgramasBeneficios.Visible = false;
                lstRecursos.Visible = false;

                if (lstRecursos.FindControl("tdTotalCofinanciamento") != null)
                {
                    (lstRecursos.FindControl("tdTotalCofinanciamento") as TableRow).Visible = false;
                }
                if (lstRecursos.FindControl("tdCofinanciamentoReprogramado") != null)
                {
                    (lstRecursos.FindControl("tdCofinanciamentoReprogramado") as TableRow).Visible = false;
                }
                if (lstRecursos.FindControl("tdCofinanciamentoDemandas") != null)
                {
                    (lstRecursos.FindControl("tdCofinanciamentoDemandas") as TableRow).Visible = false;
                }

                if (lstRecursos.FindControl("tdStatus") != null)
                {
                    (lstRecursos.FindControl("tdStatus") as TableRow).Visible = false;
                }

                lstProgramasProjetos.DataSource = lst.Where(S => S.PrevisaoOrcamentaria > 0 || S.RecursoReprogramadoAnoAnterior > 0);
                lstProgramasProjetos.DataBind();
                tdTotalRedePublica.ColSpan = 4;
                lblValorCofinanciamentoText.Text = "Valor total do cofinanciamento rede pública: ";
                lblValorCofinanciamentoPrivadoText.Text = "Valor total do confinanciamento rede privada: ";

                txtTot1Publica.Visible = txtTot2Publica.Visible = txtTot3Publica.Visible = txtTot4Publica.Visible = txtTot5Publica.Visible = txtTot6Publica.Visible =
                    txtTot7Publica.Visible = txtTot8Publica.Visible = txtTot9Publica.Visible = txtTot10Publica.Visible = txtTot11Publica.Visible = txtTot12Publica.Visible = true;

                //txtInv1Publica.Enabled = txtInv2Publica.Enabled = txtInv3Publica.Enabled = txtInv4Publica.Enabled = txtInv5Publica.Enabled = txtInv6Publica.Enabled =
                //    txtInv7Publica.Enabled = txtInv8Publica.Enabled = txtInv9Publica.Enabled = txtInv10Publica.Enabled = txtInv11Publica.Enabled = txtInv12Publica.Enabled = false;

                txtTot1.Visible = txtTot2.Visible = txtTot3.Visible = txtTot4.Visible = txtTot5.Visible = txtTot6.Visible =
                    txtTot7.Visible = txtTot8.Visible = txtTot9.Visible = txtTot10.Visible = txtTot11.Visible = txtTot12.Visible = true;

                BloquearCamposPrivados();

                if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) == 4)
                {
                    txtObras1.Enabled = txtObras2.Enabled = txtObras3.Enabled = txtObras4.Enabled = txtObras5.Enabled = txtObras6.Enabled =
                    txtObras7.Enabled = txtObras8.Enabled = txtObras9.Enabled = txtObras10.Enabled = txtObras11.Enabled = txtObras12.Enabled = true;

                    txtTot1.Enabled = txtTot2.Enabled = txtTot3.Enabled = txtTot4.Enabled = txtTot5.Enabled = txtTot6.Enabled =
                    txtTot7.Enabled = txtTot8.Enabled = txtTot9.Enabled = txtTot10.Enabled = txtTot11.Enabled = txtTot12.Enabled = false;

                    txtMC1.Enabled = txtMC2.Enabled = txtMC3.Enabled = txtMC4.Enabled = txtMC5.Enabled = txtMC6.Enabled =
                    txtMC7.Enabled = txtMC8.Enabled = txtMC9.Enabled = txtMC10.Enabled = txtMC11.Enabled = txtMC12.Enabled = true;

                    txtST1.Enabled = txtST2.Enabled = txtST3.Enabled = txtST4.Enabled = txtST5.Enabled = txtST6.Enabled =
                    txtST7.Enabled = txtST8.Enabled = txtST9.Enabled = txtST10.Enabled = txtST11.Enabled = txtST12.Enabled = false;

                    txtRH1.Enabled = txtRH2.Enabled = txtRH3.Enabled = txtRH4.Enabled = txtRH5.Enabled = txtRH6.Enabled =
                    txtRH7.Enabled = txtRH8.Enabled = txtRH9.Enabled = txtRH10.Enabled = txtRH11.Enabled = txtRH12.Enabled = true;

                    txtObrasPrivada1.Enabled = txtObrasPrivada2.Enabled = txtObrasPrivada3.Enabled = txtObrasPrivada4.Enabled =
                    txtObrasPrivada5.Enabled = txtObrasPrivada6.Enabled = txtObrasPrivada7.Enabled = txtObrasPrivada8.Enabled =
                    txtObrasPrivada9.Enabled = txtObrasPrivada10.Enabled = txtObrasPrivada11.Enabled = txtObrasPrivada12.Enabled = true;

                    tdRedePublica.ColSpan = 6;
                    tdTotalRedePublica.ColSpan = 5;
                    tdRecursosExercicioAtual.ColSpan = 5;

                    tdParcelas.RowSpan = 3;
                    //tdTotalExecucao.Visible = false;
                    tdRecursosEstaduais.RowSpan = 3;
                    tdCusteio.Visible = false;
                    tdInvestimento.Visible = false;
                    tdEquipamentosPrivado.Visible = tdEquipamentos.Visible = true;
                    tdRecursosReprogramadosAnoAnterior.ColSpan = 5;

                    tdObras.Visible = tdObras1.Visible = tdObras2.Visible = tdObras3.Visible = tdObras4.Visible =
                    tdObras5.Visible = tdObras6.Visible = tdObras7.Visible = tdObras8.Visible =
                    tdObras9.Visible = tdObras10.Visible = tdObras11.Visible = tdObras12.Visible = tdTotalObraPublica.Visible = true;

                    tdObrasPrivadas.Visible = tdObrasPrivada1.Visible = tdObrasPrivada2.Visible = tdObrasPrivada3.Visible = tdObrasPrivada4.Visible =
                    tdObrasPrivada5.Visible = tdObrasPrivada6.Visible = tdObrasPrivada7.Visible = tdObrasPrivada8.Visible = tdObrasPrivada9.Visible =
                    tdObrasPrivada10.Visible = tdObrasPrivada11.Visible = tdObrasPrivada12.Visible = tdTotalObrasUnidadePrivada.Visible = true;
                    
                    trProgramaProjeto.Visible = true;
                    tdItensDespesa.ColSpan = 4;
                    // tdfirstRowObras.Visible = true;
                    tdLimparRedePublica.ColSpan = 6;
                    tdInvestimentoPrivado.ColSpan = 2;
                    thRedePrivada.ColSpan = 7;
                    tdPrevisaoExecPrivada.ColSpan = 5;
                    tdValorCofinanciamentoPrivada.ColSpan = 6;
                    tdLimparRedePrivada.ColSpan = 6;
                }
                else
                {
                    txtST1.Enabled = txtST2.Enabled = txtST3.Enabled = txtST4.Enabled = txtST5.Enabled = txtST6.Enabled =
                        txtST7.Enabled = txtST8.Enabled = txtST9.Enabled = txtST10.Enabled = txtST11.Enabled = txtST12.Enabled = false;

                }

            }
            else 
            {
                trRedeIndireta.Visible = false;
                trTotalIndireta.Visible = false;
                tdRedePublica.Visible = false;
                tdCusteio.Visible = false;
                tdInvestimento.Visible = false;
                trBeneficiosEventuais.Visible = true;
                trRecursosExercicioAtual.Visible = true;
                trRecursosReprogramados.Visible = true;
                trHeaderRecursosReprogramados.Visible = true;
                trHeaderRecursosReprogramadosPrivado.Visible = true;
                trRecursosReprogramadosPrivado.Visible = true;
                trRecursosExercicioAtualPrivado.Visible = true;
                trTotalReprogramacaoGeral.Visible = true;
                trExercicioAtualGeral.Visible = true;
                tdReprogramacaoRH.Visible = false;
                tdRecursosEstaduais.RowSpan = 3;
                tdParcelas.RowSpan = 3;
                tdRecursosReprogramadosAnoAnterior.ColSpan = 5;
                tdRecursosExercicioAtual.ColSpan = 5;
                trHeaderDemandasParalamentares.Visible = true;
                tdDemandasParlamentares.ColSpan = 5;
                td13.ColSpan = 5;
                trHeaderDemandasParlamentaresPrivado.Visible = false;
                trDemandasParlamentaresParcela.Visible = true;
                trDemandasParlamentaresParcelaPrivado.Visible = false;
                trHeaderReprogramadoDemandasParalamentares.Visible = true;
                trReprogramadoDemandasParlamentaresParcela.Visible = true;
                trHeaderReprogramadoDemandasParlamentaresPrivado.Visible = false;
                trReprogramadoDemandasParlamentaresParcelaPrivado.Visible = false;
                trTotalGeralDemandasParlamentares.Visible = false;
                trTotalGeralReprogramacaoDemandasParlamentares.Visible = false;
                td12.Visible = false;
                td14.Visible = false;
                tdCofinanciamentoReprogramadoDemandas.Visible = true;
                tdStatus.Visible = false;
                /*tdTotalObraPublica.Visible = false;
                tdReprogramacaoObras.Visible = false;*/
                ajusteGridBeneficiosEventuais();

                lstProgramasProjetos.Visible = false;
                lstProgramasBeneficios.Visible = true;
                lstRecursos.Visible = false;
                if (lstRecursos.FindControl("tdTotalCofinanciamento") != null)
                {
                    (lstRecursos.FindControl("tdTotalCofinanciamento") as TableRow).Visible = false;
                }
                if (lstRecursos.FindControl("tdCofinanciamentoReprogramado") != null)
                {
                    (lstRecursos.FindControl("tdCofinanciamentoReprogramado") as TableRow).Visible = false;
                }
                if (lstRecursos.FindControl("tdCofinanciamentoDemandas") != null)
                {
                    (lstRecursos.FindControl("tdCofinanciamentoDemandas") as TableRow).Visible = false;
                }

                if (lstRecursos.FindControl("tdStatus") != null)
                {
                    (lstRecursos.FindControl("tdStatus") as TableRow).Visible = false;
                }
                
                lstProgramasBeneficios.DataSource = lst;
                lstProgramasBeneficios.DataBind();
                tdTotalRedePublica.ColSpan = 5;
                lblValorCofinanciamentoText.Text = "Valor total do cofinanciamento rede pública: ";
                lblValorCofinanciamentoPrivadoText.Text = "Valor total do confinanciamento rede privada: ";
                
                txtTot1Publica.Visible = txtTot2Publica.Visible = txtTot3Publica.Visible = txtTot4Publica.Visible = txtTot5Publica.Visible = txtTot6Publica.Visible =
                    txtTot7Publica.Visible = txtTot8Publica.Visible = txtTot9Publica.Visible = txtTot10Publica.Visible = txtTot11Publica.Visible = txtTot12Publica.Visible = true;
                                
                txtTot1.Visible = txtTot2.Visible = txtTot3.Visible = txtTot4.Visible = txtTot5.Visible = txtTot6.Visible =
                    txtTot7.Visible = txtTot8.Visible = txtTot9.Visible = txtTot10.Visible = txtTot11.Visible = txtTot12.Visible = true;
                
                txtST1.Enabled = txtST2.Enabled = txtST3.Enabled = txtST4.Enabled = txtST5.Enabled = txtST6.Enabled =
                    txtST7.Enabled = txtST8.Enabled = txtST9.Enabled = txtST10.Enabled = txtST11.Enabled = txtST12.Enabled = false;

                

                BloquearCamposPrivados();

            }
            
            lblValorExercicioAtualPrivado.Text = lst.Where(t => t.IdTipoUnidade == 2).Sum(t => t.PrevisaoOrcamentaria).ToString("n2");
            lblValorReprogramadoPrivado.Text = lst.Where(t => t.IdTipoUnidade == 2).Sum(t => t.RecursoReprogramadoAnoAnterior).ToString("n2");
            lblValorDemandasPrivado.Text = lst.Where(t => t.IdTipoUnidade == 2).Sum(t => t.ValorEstadualDemandasParlamentares).ToString("n2");
            lblTotalCofinanciamento.Text = lst.Where(t => t.IdTipoUnidade == 2).Sum(t => t.PrevisaoOrcamentaria + t.RecursoReprogramadoAnoAnterior  + t.ValorEstadualDemandasParlamentares + t.ValorEstadualReprogramacaoDemandasParlamentares).ToString("n2");
            lblValorReprogramado.Text = lst.Where(t => t.IdTipoUnidade != 2).Sum(t => t.RecursoReprogramadoAnoAnterior).ToString("n2");
            lblValorDemandas.Text = lst.Where(t => t.IdTipoUnidade != 2).Sum(t => t.ValorEstadualDemandasParlamentares).ToString("n2");
            lblValorReprogramadoDemandas.Text = lst.Where(t => t.IdTipoUnidade != 2).Sum(t => t.ValorEstadualReprogramacaoDemandasParlamentares).ToString("n2");
            lblValorReprogramadoDemandasPrivado.Text = lst.Where(t => t.IdTipoUnidade == 2).Sum(t => t.ValorEstadualReprogramacaoDemandasParlamentares).ToString("n2");
            lblRecursosExercicioAtual.Text = lst.Where(t => t.IdTipoUnidade != 2 /*&& t.IdTipoUnidade != 4*/).Sum(t => t.PrevisaoOrcamentaria).ToString("n2");

            lblTotalCofinanciamentoPublica.Text = lst.Where(t => t.IdTipoUnidade != 2 /* && t.IdTipoUnidade != 4*/).Sum(t => t.PrevisaoOrcamentaria + t.RecursoReprogramadoAnoAnterior + t.ValorEstadualDemandasParlamentares + t.ValorEstadualReprogramacaoDemandasParlamentares).ToString("n2");

           /* foreach (var i in lst.Where(t => t.IdTipoUnidade != 2 && t.IdTipoUnidade != 4))
            {
                decimal PrevisaoOrcamentaria = 0;
                decimal RecursoReprogramadoAnoAnterior = 0;
                decimal ValorEstadualDemandasParlamentares = 0;
                decimal ValorEstadualReprogramacaoDemandasParlamentares = 0;

            }*/
            
            
            
            calcularRede();
            calcularReprogramadoBeneficiosEventuais();

            calcularDemandasReprogramadoBeneficiosEventuais();
            calcularDemandasParlamentaresBeneficiosEventuais();

            if (lst.Count() > 0)
            {
                tblTotalCofinanciamentoEstadual.Visible = true;
                hdnSomaNumeroAtendidos.Value = lst.Sum(c => c.NumeroAtendidos).ToString();
                //(tblTotalCofinanciamentoEstadual.FindControl("lblTotalCapacidadePessoas") as Label).Text = hdnSomaNumeroAtendidos.Value;
                //hdnSomaPrevisaoOrcamentaria.Value =
                (tblTotalCofinanciamentoEstadual.FindControl("lblTotalCofinanciamentoEstadual") as Label).Text = lst.Sum(c => c.PrevisaoOrcamentaria).ToString("N2");//hdnSomaPrevisaoOrcamentaria.Value;
                hdnSomaRecursoReprogramadoAnoAnterior.Value = lst.Sum(c => c.RecursoReprogramadoAnoAnterior).ToString("N2");
                (tblTotalCofinanciamentoEstadual.FindControl("lblCofinanciamentoReprogramado") as Label).Text = hdnSomaRecursoReprogramadoAnoAnterior.Value;

                hdnSomaDemandasParlamentares.Value = lst.Sum(c => c.ValorEstadualDemandasParlamentares).ToString("N2");
                (tblTotalCofinanciamentoEstadual.FindControl("lblCofinanciamentoDemandas") as Label).Text = hdnSomaDemandasParlamentares.Value;

                hdnSomaReprogramadoDemandasParlamentares.Value = lst.Sum(c => c.ValorEstadualReprogramacaoDemandasParlamentares).ToString("N2");
                (tblTotalCofinanciamentoEstadual.FindControl("lblCofinanciamentoReprogramadoDemandas") as Label).Text = hdnSomaReprogramadoDemandasParlamentares.Value;

                hdnTotalCofinanciamentoEstadual.Value = (lst.Sum(c => c.PrevisaoOrcamentaria) + lst.Sum(c => c.RecursoReprogramadoAnoAnterior) + lst.Sum(c => c.ValorEstadualDemandasParlamentares) + lst.Sum(c => c.ValorEstadualReprogramacaoDemandasParlamentares)).ToString("n2");
                (tblTotalCofinanciamentoEstadual.FindControl("lblTotalCofinanciamentos") as Label).Text = hdnTotalCofinanciamentoEstadual.Value;
                // (tblTotalCofinanciamentoEstadual.FindControl("tblTotalCofinanciamentoEstadual") as HtmlTableRow).Visible = true;
            }
            else
            {
                tblTotalCofinanciamentoEstadual.Visible = false;
            }

            this.AplicarRegraBloqueioDesbloqueio();
        }

        private void ajusteGridBasicaMediaAlta() 
        {
            tdTotalGeralCofinanciamentos.Width = "880px";
            tdTotalGeralCofinanciamentos.Style.Value = "text-align: right;width:880px";
            tdCofinanciamentoEstadual.Width = "150px";
            tdCofinanciamentoReprogramado.Width = "175px";
            tdCofinanciamentoDemandas.Width = "96px";
            tdCofinanciamentoReprogramadoDemandas.Width = "96px";
            tdTotalCofinanciamento.Width = "100px";
            tdStatus.Width = "65px";
        }

        private void ajusteGridProgramasProjetos()
        {
            tdTotalGeralCofinanciamentos.Style.Value = "text-align: right;width:800px";
            tdCofinanciamentoEstadual.Width = "120px";
            tdCofinanciamentoReprogramado.Width = "125px";
            tdTotalCofinanciamento.Width = "280px";
         
        }

        private void ajusteGridBeneficiosEventuais()
        {
            tdTotalGeralCofinanciamentos.Width = "875px";
            //tdTotalGeralCofinanciamentos.Style.Value = "text-align: right;width:970px";
            tdCofinanciamentoEstadual.Width = "128px";
            tdCofinanciamentoReprogramado.Width = "125px";
            tdCofinanciamentoDemandas.Width = "83px";
            tdCofinanciamentoReprogramadoDemandas.Width = "86px";
            tdTotalCofinanciamento.Width = "156px";
            //tdStatus.Width = "65px";
        }



        private void verificarAlteracoes(Int32 idQuadro)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, idQuadro);
                    linkAlteracoesQuadro.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idQuadro.ToString()));
                }
            }
        }

        private void adicionarEventos()
        {
            #region Aba - Protecao basica - 5.5.A
            #region Rede Indireta
            #region [Indireta] Reprogramacao - Parcela Única
            txtRecursosReprogramadoPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRecursosHumanosReprogramadoPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrasCusteioReprogramadoPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtEquipamentosPrivadoReprogramado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

			txtEquipamentosPrivadoReprogramado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
			txtObrasReprogramadoPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
			
			txtDemandasParlamentaresPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
			txtRecursosHumanosDemandasPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
			txtOutrasCusteioDemandasPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
			txtEquipamentosPrivadoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasDemandasPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtReprogramadoDemandasParlamentaresPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRecursosHumanosReprogramadoDemandasPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrasCusteioReprogramadoDemandasPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtEquipamentosPrivadoReprogramadoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasReprogramadoDemandasPrivado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #region [Indireta] col 1 - TOT
            txtTot1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot5.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot6.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot7.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot8.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot9.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot10.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot11.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot12.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #region [Indireta] col 2 - RH
            txtRH1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH5.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH6.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH7.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH8.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH9.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH10.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH11.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRH12.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            #endregion

            #region [Indireta] col 3 - MC
            txtMC1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC5.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC6.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC7.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC8.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC9.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC10.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC11.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtMC12.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #region [Indireta] col 4 - ST
            txtST1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST5.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST6.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST7.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST8.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST9.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST10.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST11.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtST12.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #endregion
            #region Rede Direta
            #region [Direta] Reprogramacao - Parcela Única
            txtReprogramacaoRH.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoCusteio.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoInvestimento.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoRecursosDisponibilizados.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
			txtReprogramacaoObras.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
			txtReprogramacaoInvestimento.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
			
			txtDemandasParlamentaresDisponibilizados.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
			txtDemandasRH.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
			txtDemandasCusteio.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
			txtDemandasInvestimento.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
            txtDemandasObras.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");

            txtReprogramadoDemandasParlamentaresDisponibilizados.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
            txtReprogramadoDemandasRH.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
            txtReprogramadoDemandasCusteio.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
            txtReprogramadoDemandasInvestimento.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
            txtReprogramadoDemandasObras.Attributes.Add("oninput", "return( currencyFormat(this, '.',',', event));");
            #endregion

            #region [direta] col 1 - Tot Publica
            txtTot1Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot2Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot3Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot4Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot5Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot6Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot7Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot8Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot9Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot10Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot11Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTot12Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #region [direta] col 2 - Custeio
            txtCusteio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio5.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio6.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio7.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio8.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio9.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio10.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio11.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtCusteio12.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

        
            #endregion

            #region [direta] col 3 - outro custeio
            txtOutroCusteio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio5.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio6.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio7.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio8.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio9.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio10.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio11.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutroCusteio12.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #region [direta] col 4 - investimento
            txtInv1Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv2Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv3Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv4Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv5Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv6Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv7Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv8Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv9Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv10Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv11Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtInv12Publica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion

            #endregion
            #endregion

            #region Aba - Programas e projetos 5.6.D
            #region Obras publicas
            txtObras1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras5.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras6.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras7.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras8.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras9.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras10.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras11.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObras12.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

      
            #endregion

            #region Obras Privadas
            txtObrasPrivada1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada5.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada6.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada7.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada8.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada9.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada10.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada11.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtObrasPrivada12.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            #endregion
            #endregion
        }


        #region tabela privada

        private void loadPrivada(ProxyPrefeitura proxy)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            var obj = proxy.Service.GetCronogramaDesembolsoRedePrivadaByPrefeituraETipoProtecaoSocial(
                  SessaoPmas.UsuarioLogado.Prefeitura.Id
                , Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]))
                , exercicio
                );

            if (obj == null)
            {
                return;
            }

            PreencherValorMateriaConsumoPrivado(obj);
            PreecnherValorRHPrivado(obj);
            PreencherValorServicosTerceiroPrivado(obj);
            PreecherValoresObrasPrivada(obj);
            PreencherValorAquisicaoEquipamentosPrivado(obj);
            PreencherValoresReprogramadosPrivado(obj);
            PreencherValoresDemandasPrivado(obj);
            PreencherValoresReprogramacaoDemandasPrivado(obj);
            calcularRedePrivada();

            if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) == 4 || Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) == 5)
            {
                Th1.Height = "100px";
            }
            else
            {
                Th1.Height = "139px";
            }
        }

        private void PreencherValoresReprogramadosPrivado(CronogramaDesembolsoInfo cronograma)
        {
            txtRecursosReprogramadoPrivado.Text = cronograma.ReprogramacaoRecursosDisponibilizados.HasValue ? cronograma.ReprogramacaoRecursosDisponibilizados.Value.ToString("N2") : "0,00";
            txtRecursosHumanosReprogramadoPrivado.Text = cronograma.RecursosHumanosReprogramados.HasValue ? cronograma.RecursosHumanosReprogramados.Value.ToString("N2") : "0,00";
            txtOutrasCusteioReprogramadoPrivado.Text = cronograma.OutrasDespesasReprogramados.HasValue ? cronograma.OutrasDespesasReprogramados.Value.ToString("N2") : "0,00";
            txtEquipamentosPrivadoReprogramado.Text = cronograma.ReprogramacaoEquipamentosInvestimento.HasValue ? cronograma.ReprogramacaoEquipamentosInvestimento.Value.ToString("N2") : "0,00";
            txtObrasReprogramadoPrivado.Text = cronograma.ReprogramacaoObras.HasValue ? cronograma.ReprogramacaoObras.Value.ToString("N2") : "0,00";
        }

        private void PreencherValoresDemandasPrivado(CronogramaDesembolsoInfo cronograma)
        {
            txtDemandasParlamentaresPrivado.Text = cronograma.DemandasParlamentaresDisponibilizados.HasValue ? cronograma.DemandasParlamentaresDisponibilizados.Value.ToString("N2") : "0,00";
            txtRecursosHumanosDemandasPrivado.Text = cronograma.RecursosHumanosDemandasParlamentares.HasValue ? cronograma.RecursosHumanosDemandasParlamentares.Value.ToString("N2") : "0,00";
            txtOutrasCusteioDemandasPrivado.Text = cronograma.OutrasDespesasDemandasParlamentares.HasValue ? cronograma.OutrasDespesasDemandasParlamentares.Value.ToString("N2") : "0,00";
            txtEquipamentosPrivadoDemandas.Text = cronograma.DemandasParlamentaresEquipamentosInvestimento.HasValue ? cronograma.DemandasParlamentaresEquipamentosInvestimento.Value.ToString("N2") : "0,00";
            txtObrasDemandasPrivado.Text = cronograma.DemandasParlamentaresObras.HasValue ? cronograma.DemandasParlamentaresObras.Value.ToString("N2") : "0,00";
        }

        private void PreencherValoresReprogramacaoDemandasPrivado(CronogramaDesembolsoInfo cronograma)
        {
            txtReprogramadoDemandasParlamentaresPrivado.Text = cronograma.ReprogramacaoDemandasParlamentaresDisponibilizados.HasValue ? cronograma.ReprogramacaoDemandasParlamentaresDisponibilizados.Value.ToString("N2") : "0,00";
            txtRecursosHumanosReprogramadoDemandasPrivado.Text = cronograma.RecursosHumanosReprogramacaoDemandasParlamentares.HasValue ? cronograma.RecursosHumanosReprogramacaoDemandasParlamentares.Value.ToString("N2") : "0,00";
            txtOutrasCusteioReprogramadoDemandasPrivado.Text = cronograma.OutrasDespesasReprogramacaoDemandasParlamentares.HasValue ? cronograma.OutrasDespesasReprogramacaoDemandasParlamentares.Value.ToString("N2") : "0,00";
            txtEquipamentosPrivadoReprogramadoDemandas.Text = cronograma.ReprogramacaoDemandasParlamentaresEquipamentosInvestimento.HasValue ? cronograma.ReprogramacaoDemandasParlamentaresEquipamentosInvestimento.Value.ToString("N2") : "0,00";
            txtObrasReprogramadoDemandasPrivado.Text = cronograma.ReprogramacaoDemandasParlamentaresObras.HasValue ? cronograma.ReprogramacaoDemandasParlamentaresObras.Value.ToString("N2") : "0,00";
        }

        private void PreecherValoresObrasPrivada(CronogramaDesembolsoInfo cronograma)
        {
            if (cronograma != null)
            {
                txtObrasPrivada1.Text = cronograma.ObrasMes1.HasValue ? cronograma.ObrasMes1.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada2.Text = cronograma.ObrasMes2.HasValue ? cronograma.ObrasMes2.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada3.Text = cronograma.ObrasMes3.HasValue ? cronograma.ObrasMes3.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada4.Text = cronograma.ObrasMes4.HasValue ? cronograma.ObrasMes4.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada5.Text = cronograma.ObrasMes5.HasValue ? cronograma.ObrasMes5.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada6.Text = cronograma.ObrasMes6.HasValue ? cronograma.ObrasMes6.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada7.Text = cronograma.ObrasMes7.HasValue ? cronograma.ObrasMes7.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada8.Text = cronograma.ObrasMes8.HasValue ? cronograma.ObrasMes8.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada9.Text = cronograma.ObrasMes9.HasValue ? cronograma.ObrasMes9.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada10.Text = cronograma.ObrasMes10.HasValue ? cronograma.ObrasMes10.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada11.Text = cronograma.ObrasMes11.HasValue ? cronograma.ObrasMes11.Value.ToString("n2") : (0M).ToString("n2");
                txtObrasPrivada12.Text = cronograma.ObrasMes12.HasValue ? cronograma.ObrasMes12.Value.ToString("n2") : (0M).ToString("n2");                
            }
        }

        private void PreencherValorServicosTerceiroPrivado(CronogramaDesembolsoInfo cronograma)
        {
            if (cronograma != null)
            {
                txtTot1.Text = cronograma.ValorServicosTerceirosMes1 != null ? cronograma.ValorServicosTerceirosMes1.ToString("n2") : (0M).ToString("n2");
                txtTot2.Text = cronograma.ValorServicosTerceirosMes2 != null ? cronograma.ValorServicosTerceirosMes2.ToString("n2") : (0M).ToString("n2");
                txtTot3.Text = cronograma.ValorServicosTerceirosMes3 != null ? cronograma.ValorServicosTerceirosMes3.ToString("n2") : (0M).ToString("n2");
                txtTot4.Text = cronograma.ValorServicosTerceirosMes4 != null ? cronograma.ValorServicosTerceirosMes4.ToString("n2") : (0M).ToString("n2");
                txtTot5.Text = cronograma.ValorServicosTerceirosMes5 != null ? cronograma.ValorServicosTerceirosMes5.ToString("n2") : (0M).ToString("n2");
                txtTot6.Text = cronograma.ValorServicosTerceirosMes6 != null ? cronograma.ValorServicosTerceirosMes6.ToString("n2") : (0M).ToString("n2");
                txtTot7.Text = cronograma.ValorServicosTerceirosMes7 != null ? cronograma.ValorServicosTerceirosMes7.ToString("n2") : (0M).ToString("n2");
                txtTot8.Text = cronograma.ValorServicosTerceirosMes8 != null ? cronograma.ValorServicosTerceirosMes8.ToString("n2") : (0M).ToString("n2");
                txtTot9.Text = cronograma.ValorServicosTerceirosMes9 != null ? cronograma.ValorServicosTerceirosMes9.ToString("n2") : (0M).ToString("n2");
                txtTot10.Text = cronograma.ValorServicosTerceirosMes10 != null ? cronograma.ValorServicosTerceirosMes10.ToString("n2") : (0M).ToString("n2");
                txtTot11.Text = cronograma.ValorServicosTerceirosMes11 != null ? cronograma.ValorServicosTerceirosMes11.ToString("n2") : (0M).ToString("n2");
                txtTot12.Text = cronograma.ValorServicosTerceirosMes12 != null ? cronograma.ValorServicosTerceirosMes12.ToString("n2") : (0M).ToString("n2"); 
            }

        }

        private void PreecnherValorRHPrivado(CronogramaDesembolsoInfo cronograma)
        {
            if (cronograma != null)
            {
                txtRH1.Text = cronograma.ValorRHMes1 != null ? cronograma.ValorRHMes1.ToString("n2") : (0M).ToString("n2");
                txtRH2.Text = cronograma.ValorRHMes2 != null ? cronograma.ValorRHMes2.ToString("n2") : (0M).ToString("n2");
                txtRH3.Text = cronograma.ValorRHMes3 != null ? cronograma.ValorRHMes3.ToString("n2") : (0M).ToString("n2");
                txtRH4.Text = cronograma.ValorRHMes4 != null ? cronograma.ValorRHMes4.ToString("n2") : (0M).ToString("n2");
                txtRH5.Text = cronograma.ValorRHMes5 != null ? cronograma.ValorRHMes5.ToString("n2") : (0M).ToString("n2");
                txtRH6.Text = cronograma.ValorRHMes6 != null ? cronograma.ValorRHMes6.ToString("n2") : (0M).ToString("n2");
                txtRH7.Text = cronograma.ValorRHMes7 != null ? cronograma.ValorRHMes7.ToString("n2") : (0M).ToString("n2");
                txtRH8.Text = cronograma.ValorRHMes8 != null ? cronograma.ValorRHMes8.ToString("n2") : (0M).ToString("n2");
                txtRH9.Text = cronograma.ValorRHMes9 != null ? cronograma.ValorRHMes9.ToString("n2") : (0M).ToString("n2");
                txtRH10.Text = cronograma.ValorRHMes10 != null ? cronograma.ValorRHMes10.ToString("n2") : (0M).ToString("n2");
                txtRH11.Text = cronograma.ValorRHMes11 != null ? cronograma.ValorRHMes11.ToString("n2") : (0M).ToString("n2");
                txtRH12.Text = cronograma.ValorRHMes12 != null ? cronograma.ValorRHMes12.ToString("n2") : (0M).ToString("n2");
            }

        }

        private void PreencherValorAquisicaoEquipamentosPrivado(CronogramaDesembolsoInfo cronograma) 
        {
            if (cronograma != null)
            {
                txtST1.Text = cronograma.ValorInvestimentoMes1.HasValue ? cronograma.ValorInvestimentoMes1.Value.ToString("n2") : (0M).ToString("n2");
                txtST2.Text = cronograma.ValorInvestimentoMes2.HasValue ? cronograma.ValorInvestimentoMes2.Value.ToString("n2") : (0M).ToString("n2");
                txtST3.Text = cronograma.ValorInvestimentoMes3.HasValue ? cronograma.ValorInvestimentoMes3.Value.ToString("n2") : (0M).ToString("n2");
                txtST4.Text = cronograma.ValorInvestimentoMes4.HasValue ? cronograma.ValorInvestimentoMes4.Value.ToString("n2") : (0M).ToString("n2");
                txtST5.Text = cronograma.ValorInvestimentoMes5.HasValue ? cronograma.ValorInvestimentoMes5.Value.ToString("n2") : (0M).ToString("n2");
                txtST6.Text = cronograma.ValorInvestimentoMes6.HasValue ? cronograma.ValorInvestimentoMes6.Value.ToString("n2") : (0M).ToString("n2");
                txtST7.Text = cronograma.ValorInvestimentoMes7.HasValue ? cronograma.ValorInvestimentoMes7.Value.ToString("n2") : (0M).ToString("n2");
                txtST8.Text = cronograma.ValorInvestimentoMes8.HasValue ? cronograma.ValorInvestimentoMes8.Value.ToString("n2") : (0M).ToString("n2");
                txtST9.Text = cronograma.ValorInvestimentoMes9.HasValue ? cronograma.ValorInvestimentoMes9.Value.ToString("n2") : (0M).ToString("n2");
                txtST10.Text = cronograma.ValorInvestimentoMes10.HasValue ? cronograma.ValorInvestimentoMes10.Value.ToString("n2") : (0M).ToString("n2");
                txtST11.Text = cronograma.ValorInvestimentoMes11.HasValue ? cronograma.ValorInvestimentoMes11.Value.ToString("n2") : (0M).ToString("n2");
                txtST12.Text = cronograma.ValorInvestimentoMes12.HasValue ? cronograma.ValorInvestimentoMes12.Value.ToString("n2") : (0M).ToString("n2");                
            }

        }
        
        private void PreencherValorMateriaConsumoPrivado(CronogramaDesembolsoInfo cronograma)
        {
            if (cronograma != null)
            {
                txtMC1.Text = cronograma.ValorMaterialConsumoMes1 != null ? cronograma.ValorMaterialConsumoMes1.ToString("n2") : (0M).ToString("n2");
                txtMC2.Text = cronograma.ValorMaterialConsumoMes2 != null ? cronograma.ValorMaterialConsumoMes2.ToString("n2") : (0M).ToString("n2");
                txtMC3.Text = cronograma.ValorMaterialConsumoMes3 != null ? cronograma.ValorMaterialConsumoMes3.ToString("n2") : (0M).ToString("n2");
                txtMC4.Text = cronograma.ValorMaterialConsumoMes4 != null ? cronograma.ValorMaterialConsumoMes4.ToString("n2") : (0M).ToString("n2");
                txtMC5.Text = cronograma.ValorMaterialConsumoMes5 != null ? cronograma.ValorMaterialConsumoMes5.ToString("n2") : (0M).ToString("n2");
                txtMC6.Text = cronograma.ValorMaterialConsumoMes6 != null ? cronograma.ValorMaterialConsumoMes6.ToString("n2") : (0M).ToString("n2");
                txtMC7.Text = cronograma.ValorMaterialConsumoMes7 != null ? cronograma.ValorMaterialConsumoMes7.ToString("n2") : (0M).ToString("n2");
                txtMC8.Text = cronograma.ValorMaterialConsumoMes8 != null ? cronograma.ValorMaterialConsumoMes8.ToString("n2") : (0M).ToString("n2");
                txtMC9.Text = cronograma.ValorMaterialConsumoMes9 != null ? cronograma.ValorMaterialConsumoMes9.ToString("n2") : (0M).ToString("n2");
                txtMC10.Text = cronograma.ValorMaterialConsumoMes10 != null ? cronograma.ValorMaterialConsumoMes10.ToString("n2") : (0M).ToString("n2");
                txtMC11.Text = cronograma.ValorMaterialConsumoMes11 != null ? cronograma.ValorMaterialConsumoMes11.ToString("n2") : (0M).ToString("n2");
                txtMC12.Text = cronograma.ValorMaterialConsumoMes12 != null ? cronograma.ValorMaterialConsumoMes12.ToString("n2") : (0M).ToString("n2");
            }
        }

        #endregion


        #region tabela publica
        void loadPublica(ProxyPrefeitura proxy)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            int tipoProtecao = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            var obj = proxy.Service.GetCronogramaDesembolsoRedePublicaByPrefeituraETipoProtecaoSocial(SessaoPmas.UsuarioLogado.Prefeitura.Id, tipoProtecao, exercicio);
            if (obj == null)
            {
                return;
            }
            PreencherCusteioPublico(obj);
            PreencherInvestimentoRedePublica(obj);
            PreencherOutrasDespesasCusteioPublica(obj);
            PreencherObrasRedePublica(obj);
            PreencherTotalRedePublica(obj);
            PreecherReprogramacaoPublica(obj);
            PreencherDemandasPublicas(obj);
            PreencherReprogramacaoDemandasPublicas(obj);

            if (tipoProtecao == 4)
            {
                PreencherObrasRedePublica(obj);
            }
            calcularRedePublica();
        }

        private void PreecherReprogramacaoPublica(CronogramaDesembolsoInfo cronograma)
        {
            txtReprogramacaoRecursosDisponibilizados.Text = cronograma.ReprogramacaoRecursosDisponibilizados.HasValue ? cronograma.ReprogramacaoRecursosDisponibilizados.Value.ToString("N2") : "0,00";
            txtReprogramacaoRH.Text = cronograma.RecursosHumanosReprogramados.HasValue ? cronograma.RecursosHumanosReprogramados.Value.ToString("N2") : "0,00";
            txtReprogramacaoCusteio.Text = cronograma.OutrasDespesasReprogramados.HasValue ? cronograma.OutrasDespesasReprogramados.Value.ToString("N2") : "0,00";
            txtReprogramacaoInvestimento.Text = cronograma.ReprogramacaoEquipamentosInvestimento.HasValue ? cronograma.ReprogramacaoEquipamentosInvestimento.Value.ToString("N2") : "0,00";
            txtReprogramacaoObras.Text = cronograma.ReprogramacaoObras.HasValue ? cronograma.ReprogramacaoObras.Value.ToString("N2") : "0,00";
        }

        private void PreencherDemandasPublicas(CronogramaDesembolsoInfo cronograma) 
        {
            txtDemandasParlamentaresDisponibilizados.Text = cronograma.DemandasParlamentaresDisponibilizados.HasValue ? cronograma.DemandasParlamentaresDisponibilizados.Value.ToString("N2") : "0,00";
            txtDemandasRH.Text = cronograma.RecursosHumanosDemandasParlamentares.HasValue ? cronograma.RecursosHumanosDemandasParlamentares.Value.ToString("N2") : "0,00";
            txtDemandasCusteio.Text = cronograma.OutrasDespesasDemandasParlamentares.HasValue ? cronograma.OutrasDespesasDemandasParlamentares.Value.ToString("N2") : "0,00";
            txtDemandasInvestimento.Text = cronograma.DemandasParlamentaresEquipamentosInvestimento.HasValue ? cronograma.DemandasParlamentaresEquipamentosInvestimento.Value.ToString("N2") : "0,00";
            txtDemandasObras.Text = cronograma.DemandasParlamentaresObras.HasValue ? cronograma.DemandasParlamentaresObras.Value.ToString("N2") : "0,00";
        }

        private void PreencherReprogramacaoDemandasPublicas(CronogramaDesembolsoInfo cronograma)
        { 
            txtReprogramadoDemandasParlamentaresDisponibilizados.Text = cronograma.ReprogramacaoDemandasParlamentaresDisponibilizados.HasValue ? cronograma.ReprogramacaoDemandasParlamentaresDisponibilizados.Value.ToString("N2") : "0,00";
            txtReprogramadoDemandasRH.Text = cronograma.RecursosHumanosReprogramacaoDemandasParlamentares.HasValue ? cronograma.RecursosHumanosReprogramacaoDemandasParlamentares.Value.ToString("N2") : "0,00";
            txtReprogramadoDemandasCusteio.Text = cronograma.OutrasDespesasReprogramacaoDemandasParlamentares.HasValue ? cronograma.OutrasDespesasReprogramacaoDemandasParlamentares.Value.ToString("N2") : "0,00";
            txtReprogramadoDemandasInvestimento.Text = cronograma.ReprogramacaoDemandasParlamentaresEquipamentosInvestimento.HasValue ? cronograma.ReprogramacaoDemandasParlamentaresEquipamentosInvestimento.Value.ToString("N2") : "0,00";
            txtReprogramadoDemandasObras.Text = cronograma.ReprogramacaoDemandasParlamentaresObras.HasValue ? cronograma.ReprogramacaoDemandasParlamentaresObras.Value.ToString("N2") : "0,00";
        }

        private void PreencherObrasRedePublica(CronogramaDesembolsoInfo cronograma)
        {
            if (cronograma != null)
            {
                txtObras1.Text = cronograma.ObrasMes1.HasValue ? cronograma.ObrasMes1.Value.ToString("n2") : (0M).ToString("n2");
                txtObras2.Text = cronograma.ObrasMes2.HasValue ? cronograma.ObrasMes2.Value.ToString("n2") : (0M).ToString("n2");
                txtObras3.Text = cronograma.ObrasMes3.HasValue ? cronograma.ObrasMes3.Value.ToString("n2") : (0M).ToString("n2");
                txtObras4.Text = cronograma.ObrasMes4.HasValue ? cronograma.ObrasMes4.Value.ToString("n2") : (0M).ToString("n2");
                txtObras5.Text = cronograma.ObrasMes5.HasValue ? cronograma.ObrasMes5.Value.ToString("n2") : (0M).ToString("n2");
                txtObras6.Text = cronograma.ObrasMes6.HasValue ? cronograma.ObrasMes6.Value.ToString("n2") : (0M).ToString("n2");
                txtObras7.Text = cronograma.ObrasMes7.HasValue ? cronograma.ObrasMes7.Value.ToString("n2") : (0M).ToString("n2");
                txtObras8.Text = cronograma.ObrasMes8.HasValue ? cronograma.ObrasMes8.Value.ToString("n2") : (0M).ToString("n2");
                txtObras9.Text = cronograma.ObrasMes9.HasValue ? cronograma.ObrasMes9.Value.ToString("n2") : (0M).ToString("n2");
                txtObras10.Text = cronograma.ObrasMes10.HasValue ? cronograma.ObrasMes10.Value.ToString("n2") : (0M).ToString("n2");
                txtObras11.Text = cronograma.ObrasMes11.HasValue ? cronograma.ObrasMes11.Value.ToString("n2") : (0M).ToString("n2");
                txtObras12.Text = cronograma.ObrasMes12.HasValue ? cronograma.ObrasMes12.Value.ToString("n2") : (0M).ToString("n2");                
            }

        }

        private void PreencherTotalRedePublica(CronogramaDesembolsoInfo cronograma)
        {
            if (cronograma != null)
            {
                txtTot1Publica.Text = cronograma.ValorServicosTerceirosMes1 != null ? cronograma.ValorServicosTerceirosMes1.ToString("n2") : (0M).ToString("n2");
                txtTot2Publica.Text = cronograma.ValorServicosTerceirosMes2 != null ? cronograma.ValorServicosTerceirosMes2.ToString("n2") : (0M).ToString("n2");
                txtTot3Publica.Text = cronograma.ValorServicosTerceirosMes3 != null ? cronograma.ValorServicosTerceirosMes3.ToString("n2") : (0M).ToString("n2");
                txtTot4Publica.Text = cronograma.ValorServicosTerceirosMes4 != null ? cronograma.ValorServicosTerceirosMes4.ToString("n2") : (0M).ToString("n2");
                txtTot5Publica.Text = cronograma.ValorServicosTerceirosMes5 != null ? cronograma.ValorServicosTerceirosMes5.ToString("n2") : (0M).ToString("n2");
                txtTot6Publica.Text = cronograma.ValorServicosTerceirosMes6 != null ? cronograma.ValorServicosTerceirosMes6.ToString("n2") : (0M).ToString("n2");
                txtTot7Publica.Text = cronograma.ValorServicosTerceirosMes7 != null ? cronograma.ValorServicosTerceirosMes7.ToString("n2") : (0M).ToString("n2");
                txtTot8Publica.Text = cronograma.ValorServicosTerceirosMes8 != null ? cronograma.ValorServicosTerceirosMes8.ToString("n2") : (0M).ToString("n2");
                txtTot9Publica.Text = cronograma.ValorServicosTerceirosMes9 != null ? cronograma.ValorServicosTerceirosMes9.ToString("n2") : (0M).ToString("n2");
                txtTot10Publica.Text = cronograma.ValorServicosTerceirosMes10 != null ? cronograma.ValorServicosTerceirosMes10.ToString("n2") : (0M).ToString("n2");
                txtTot11Publica.Text = cronograma.ValorServicosTerceirosMes11 != null ? cronograma.ValorServicosTerceirosMes11.ToString("n2") : (0M).ToString("n2");
                txtTot12Publica.Text = cronograma.ValorServicosTerceirosMes12 != null ? cronograma.ValorServicosTerceirosMes12.ToString("n2") : (0M).ToString("n2");
            }

        }

        private void PreencherOutrasDespesasCusteioPublica(CronogramaDesembolsoInfo cronograma)
        {
            if (cronograma != null)
            {
                txtOutroCusteio1.Text = cronograma.ValorOutrasDespesasCusteioMes01.HasValue ? cronograma.ValorOutrasDespesasCusteioMes01.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio2.Text = cronograma.ValorOutrasDespesasCusteioMes02.HasValue ? cronograma.ValorOutrasDespesasCusteioMes02.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio3.Text = cronograma.ValorOutrasDespesasCusteioMes03.HasValue ? cronograma.ValorOutrasDespesasCusteioMes03.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio4.Text = cronograma.ValorOutrasDespesasCusteioMes04.HasValue ? cronograma.ValorOutrasDespesasCusteioMes04.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio5.Text = cronograma.ValorOutrasDespesasCusteioMes05.HasValue ? cronograma.ValorOutrasDespesasCusteioMes05.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio6.Text = cronograma.ValorOutrasDespesasCusteioMes06.HasValue ? cronograma.ValorOutrasDespesasCusteioMes06.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio7.Text = cronograma.ValorOutrasDespesasCusteioMes07.HasValue ? cronograma.ValorOutrasDespesasCusteioMes07.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio8.Text = cronograma.ValorOutrasDespesasCusteioMes08.HasValue ? cronograma.ValorOutrasDespesasCusteioMes08.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio9.Text = cronograma.ValorOutrasDespesasCusteioMes09.HasValue ? cronograma.ValorOutrasDespesasCusteioMes09.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio10.Text = cronograma.ValorOutrasDespesasCusteioMes10.HasValue ? cronograma.ValorOutrasDespesasCusteioMes10.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio11.Text = cronograma.ValorOutrasDespesasCusteioMes11.HasValue ? cronograma.ValorOutrasDespesasCusteioMes11.Value.ToString("n2") : (0M).ToString("n2");
                txtOutroCusteio12.Text = cronograma.ValorOutrasDespesasCusteioMes12.HasValue ? cronograma.ValorOutrasDespesasCusteioMes12.Value.ToString("n2") : (0M).ToString("n2");
            }
        }

        private void PreencherInvestimentoRedePublica(CronogramaDesembolsoInfo cronograma)
        {
            txtInv1Publica.Text = cronograma.ValorInvestimentoMes1.HasValue   ? cronograma.ValorInvestimentoMes1.Value.ToString("n2") : (0M).ToString("n2");
            txtInv2Publica.Text = cronograma.ValorInvestimentoMes2.HasValue   ? cronograma.ValorInvestimentoMes2.Value.ToString("n2") : (0M).ToString("n2");
            txtInv3Publica.Text = cronograma.ValorInvestimentoMes3.HasValue   ? cronograma.ValorInvestimentoMes3.Value.ToString("n2") : (0M).ToString("n2");
            txtInv4Publica.Text = cronograma.ValorInvestimentoMes4.HasValue   ? cronograma.ValorInvestimentoMes4.Value.ToString("n2") : (0M).ToString("n2");
            txtInv5Publica.Text = cronograma.ValorInvestimentoMes5.HasValue   ? cronograma.ValorInvestimentoMes5.Value.ToString("n2") : (0M).ToString("n2");
            txtInv6Publica.Text = cronograma.ValorInvestimentoMes6.HasValue   ? cronograma.ValorInvestimentoMes6.Value.ToString("n2") : (0M).ToString("n2");
            txtInv7Publica.Text = cronograma.ValorInvestimentoMes7.HasValue   ? cronograma.ValorInvestimentoMes7.Value.ToString("n2") : (0M).ToString("n2");
            txtInv8Publica.Text = cronograma.ValorInvestimentoMes8.HasValue   ? cronograma.ValorInvestimentoMes8.Value.ToString("n2") : (0M).ToString("n2");
            txtInv9Publica.Text = cronograma.ValorInvestimentoMes9.HasValue   ? cronograma.ValorInvestimentoMes9.Value.ToString("n2") : (0M).ToString("n2");
            txtInv10Publica.Text = cronograma.ValorInvestimentoMes10.HasValue ? cronograma.ValorInvestimentoMes10.Value.ToString("n2") : (0M).ToString("n2");
            txtInv11Publica.Text = cronograma.ValorInvestimentoMes11.HasValue ? cronograma.ValorInvestimentoMes11.Value.ToString("n2") : (0M).ToString("n2");
            txtInv12Publica.Text = cronograma.ValorInvestimentoMes12.HasValue ? cronograma.ValorInvestimentoMes12.Value.ToString("n2") : (0M).ToString("n2");
        }

        private void PreencherCusteioPublico(CronogramaDesembolsoInfo cronograma)
        {
            if (cronograma != null)
            {
                txtCusteio1.Text = cronograma.ValorMaterialConsumoMes1 != null ? cronograma.ValorMaterialConsumoMes1.ToString("n2") : (0M).ToString("n2");
                txtCusteio2.Text = cronograma.ValorMaterialConsumoMes2 != null ? cronograma.ValorMaterialConsumoMes2.ToString("n2") : (0M).ToString("n2");
                txtCusteio3.Text = cronograma.ValorMaterialConsumoMes3 != null ? cronograma.ValorMaterialConsumoMes3.ToString("n2") : (0M).ToString("n2");
                txtCusteio4.Text = cronograma.ValorMaterialConsumoMes4 != null ? cronograma.ValorMaterialConsumoMes4.ToString("n2") : (0M).ToString("n2");
                txtCusteio5.Text = cronograma.ValorMaterialConsumoMes5 != null ? cronograma.ValorMaterialConsumoMes5.ToString("n2") : (0M).ToString("n2");
                txtCusteio6.Text = cronograma.ValorMaterialConsumoMes6 != null ? cronograma.ValorMaterialConsumoMes6.ToString("n2") : (0M).ToString("n2");
                txtCusteio7.Text = cronograma.ValorMaterialConsumoMes7 != null ? cronograma.ValorMaterialConsumoMes7.ToString("n2") : (0M).ToString("n2");
                txtCusteio8.Text = cronograma.ValorMaterialConsumoMes8 != null ? cronograma.ValorMaterialConsumoMes8.ToString("n2") : (0M).ToString("n2");
                txtCusteio9.Text = cronograma.ValorMaterialConsumoMes9 != null ? cronograma.ValorMaterialConsumoMes9.ToString("n2") : (0M).ToString("n2");
                txtCusteio10.Text = cronograma.ValorMaterialConsumoMes10 != null ? cronograma.ValorMaterialConsumoMes10.ToString("n2") : (0M).ToString("n2");
                txtCusteio11.Text = cronograma.ValorMaterialConsumoMes11 != null ? cronograma.ValorMaterialConsumoMes11.ToString("n2") : (0M).ToString("n2");
                txtCusteio12.Text = cronograma.ValorMaterialConsumoMes12 != null ? cronograma.ValorMaterialConsumoMes12.ToString("n2") : (0M).ToString("n2");
            }

        }
        #endregion


        //private void ExibirReprogramacao(bool? reprogramado)
        //{
        //    if (reprogramado.HasValue && reprogramado.Value == true)
        //    {
        //        trRecursosReprogramados.Visible = true;
        //        tdReprogramacao.Visible = true;
        //        lstRecursosReprogramados.Visible = true;
        //    }
        //    else
        //    {
        //        tdParcelasPrograma.Visible = true;
        //        lstRecursos.Visible = true;
        //    }
        //}


        #region eventos

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int tipoProtecao = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            int exercicio = Convert.ToInt32(hdfAno.Value);
            SessaoPmas.VerificarSessao(this);

            var msg = String.Empty;

            var cronogramaPrivada = PreencherCronogramaPrivada();
            var cronogramaPublica = PreencherCronogramaPublica();

            ETipoProtecao tipoProtecaoEnum = (ETipoProtecao)tipoProtecao;

            btnCalcular_Click(null, null);


            decimal totalCusteio = decimal.Parse(lblTotalCusteio.Text);
            decimal totalInvestimento = decimal.Parse(lblTotalInvestimentoPublica.Text);
            decimal totalObras = decimal.Parse(lblTotalObraPublica.Text);
            decimal totalCusteioInvestimento = decimal.Round(totalCusteio, 2) + decimal.Round(totalInvestimento, 2) + decimal.Round(totalObras, 2);

            decimal totalReprogramacao = decimal.Round(Convert.ToDecimal(lblTotalRecursosReprogramadoAnoAnterior.Text),2);
            decimal totalRecursosAtual = decimal.Round(Convert.ToDecimal(lblRecursosExercicioAtual.Text),2);
            decimal totalGeralCofinanciamento = decimal.Round(Convert.ToDecimal(lblTotalCofinanciamentoPublica.Text), 2);

            //Recuperar a soma dos campos Reprogramados Recursos Humanos e Outras despesas de custeio para validação da linha Total Reprogramado Rede Publica
            decimal totalCusteioReprogramadoPublico = Convert.ToDecimal(txtReprogramacaoRH.Text) + Convert.ToDecimal(txtReprogramacaoCusteio.Text);

            //Recuperar a soma dos campos Reprogramados Recursos Humanos e Outras despesas de custeio para validação da linha Total Reprogramado Rede Privada
            decimal totalCusteioReprogramadoPrivado = Convert.ToDecimal(txtRecursosHumanosReprogramadoPrivado.Text) + Convert.ToDecimal(txtOutrasCusteioReprogramadoPrivado.Text);

            decimal totalRecursoDisponibilizado = 0;

            if (tipoProtecao == 4)
            {

                totalCusteioReprogramadoPublico = Convert.ToDecimal(txtReprogramacaoInvestimento.Text) + Convert.ToDecimal(txtReprogramacaoCusteio.Text) + Convert.ToDecimal(txtReprogramacaoObras.Text);
                totalRecursoDisponibilizado = Convert.ToDecimal(txtReprogramacaoRecursosDisponibilizados.Text);

                if (totalCusteioReprogramadoPublico != totalRecursoDisponibilizado)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de execução dos recursos reprogramados para " + (tipoProtecaoEnum == ETipoProtecao.Basica ? "Básica" : tipoProtecaoEnum == ETipoProtecao.EspecialMediaComplexidade ? "Especial de Média Complexidade" : tipoProtecaoEnum == ETipoProtecao.ProgramasEProjetos ? "programas e projetos" : tipoProtecaoEnum == ETipoProtecao.BeneficiosEventuais ? "Beneficios eventuais" : "especial de alta complexidade") + " deve ser igual ao valor total dos recursos estaduais disponibilizados.";

                if (txtReprogramacaoRecursosDisponibilizados.Text != lblValorReprogramado.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos Recursos Disponibilizados para reprogramação da rede pública deve ser igual ao valor total destes recurso";

                if (tipoProtecaoEnum == ETipoProtecao.ProgramasEProjetos && (totalReprogramacao + totalRecursosAtual) != totalGeralCofinanciamento)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da Previsão de Execução dos Recursos destinados em Programas e projetos deve ser igual ao valor total dos Recursos estaduais disponibilizado.";

                if (lblTotal.Text != lblTotalExecPrivada.Text || lblTotalExecPrivada.Text != lblTotalCofinanciamento.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de execução dos recursos para " + (tipoProtecaoEnum == ETipoProtecao.Basica ? "Básica" : tipoProtecaoEnum == ETipoProtecao.EspecialMediaComplexidade ? "Especial de Média Complexidade" : tipoProtecaoEnum == ETipoProtecao.ProgramasEProjetos ? "programas e projetos" : tipoProtecaoEnum == ETipoProtecao.BeneficiosEventuais ? "Beneficios eventuais" : "especial de alta complexidade") + " deve ser igual ao valor total dos recursos estaduais disponibilizados.";
                
                if (lblTotalExecPublica.Text != lblRecursosExercicioAtual.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de execução dos recursos da rede de programas e projetos deve ser igual ao valor total dos recursos estaduais disponibilizados.";
            }
            else
            {
                if (lblTotalExecPublica.Text != lblRecursosExercicioAtual.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de execução dos recursos para " + (tipoProtecaoEnum == ETipoProtecao.Basica ? "proteção Básica" : tipoProtecaoEnum == ETipoProtecao.EspecialMediaComplexidade ? "proteção Especial de Média Complexidade" : tipoProtecaoEnum == ETipoProtecao.ProgramasEProjetos ? "programas e projetos" : tipoProtecaoEnum == ETipoProtecao.BeneficiosEventuais ? "Beneficios eventuais" : " proteção especial de alta complexidade") + " pública deve ser igual ao valor total dos recursos estaduais disponibilizados.";

                if (txtReprogramacaoRecursosDisponibilizados.Text != lblTotalRecursosReprogramadoAnoAnterior.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos Recursos Disponibilizados para reprogramação do ano anterior da rede pública deve ser igual ao total da Previsão de Recursos Reprogramados do ano anterior";

                if (txtReprogramacaoRecursosDisponibilizados.Text != lblValorReprogramado.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos Recursos Disponibilizados para reprogramação da rede pública deve ser igual ao valor total destes recurso";

                if (lblTotalRecursosReprogramadoAnoAnterior.Text != lblValorReprogramado.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de execução dos recursos reprogramados da rede pública deve ser igual ao valor total destes recursos";
                
                if(txtDemandasParlamentaresDisponibilizados.Text != lblValorDemandas.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos recursos disponibilizados das demandas parlamentares da rede pública deve ser igual ao valor total destes recursos";

                if (lblTotalDemandasParlamentares.Text != lblValorDemandas.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de execução das demandas parlamentares da rede pública deve ser igual ao valor total destes recursos";

                if (txtDemandasParlamentaresDisponibilizados.Text != lblTotalDemandasParlamentares.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos recursos disponibilizados das demandas parlamentares da rede pública deve ser igual ao valor total destes recursos";


                if (txtReprogramadoDemandasParlamentaresDisponibilizados.Text != lblTotalReprogramadoDemandasParlamentares.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos recursos disponibilizados reprogramados das demandas parlamentares da rede pública deve ser igual ao valor total destes recursos";

                if (lblTotalReprogramadoDemandasParlamentares.Text != lblValorReprogramadoDemandas.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de execução reprogramados das demandas parlamentares da rede pública deve ser igual ao valor total destes recursos";

                if (txtReprogramadoDemandasParlamentaresDisponibilizados.Text != lblValorReprogramadoDemandas.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos recursos disponibilizados reprogramados das demandas parlamentares da rede pública deve ser igual ao valor total destes recursos";



                decimal totalCofinanciamento = Convert.ToDecimal(lblTotalExecPublica.Text) + Convert.ToDecimal(lblTotalRecursosReprogramadoAnoAnterior.Text) + Convert.ToDecimal(lblTotalDemandasParlamentares.Text) + Convert.ToDecimal(lblTotalReprogramadoDemandasParlamentares.Text);
                
                if (totalCofinanciamento.ToString("n2") != lblTotalCofinanciamentoPublica.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + " A soma das Parcelas dos recursos disponibilizados e reprogramados deve ser igual ao total do cofinanciamento estadual (Rede Pública)";

                if (txtRecursosReprogramadoPrivado.Text != lblValorReprogramadoPrivado.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos recursos disponibilizados para reprogramação da rede privada deve ser igual ao valor total destes recursos";

                if (lblTotalRecursosReprogramadoPrivada.Text != lblValorReprogramadoPrivado.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de Execução dos recursos reprogramados da rede privada deve ser igual ao valor total destes recursos";

                if (txtDemandasParlamentaresPrivado.Text != lblValorDemandasPrivado.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos recursos disponibilizados das demandas parlamentares da rede privada deve ser igual ao valor total destes recursos";
                
                if (lblTotalDemandasParlamentaresPrivada.Text != lblValorDemandasPrivado.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de Execução das demandas parlamentares da rede privada deve ser igual ao valor total destes recursos";

                if (txtDemandasParlamentaresPrivado.Text != lblTotalDemandasParlamentaresPrivada.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos recursos disponibilizados das demandas parlamentares da rede privada deve ser igual ao valor total destes recursos";



                if (txtReprogramadoDemandasParlamentaresPrivado.Text != lblValorReprogramadoDemandasPrivado.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos recursos disponibilizados reprogramados das demandas parlamentares da rede privada deve ser igual ao valor total destes recursos";

                if (lblTotalReprogramadoDemandasParlamentaresPrivada.Text != lblValorReprogramadoDemandasPrivado.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total reprogramados da previsão de Execução das demandas parlamentares da rede privada deve ser igual ao valor total destes recursos";

                if (txtReprogramadoDemandasParlamentaresPrivado.Text != lblTotalReprogramadoDemandasParlamentaresPrivada.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor dos recursos disponibilizados reprogramados das demandas parlamentares da rede privada deve ser igual ao valor total destes recursos";



                if (lblTotalPublica.Text != lblTotalExecPublica.Text || totalCofinanciamento.ToString("n2") != lblTotalCofinanciamentoPublica.Text)//lblTotalCusteio.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de execução dos recursos para " + (tipoProtecaoEnum == ETipoProtecao.Basica ? "proteção Básica" : tipoProtecaoEnum == ETipoProtecao.EspecialMediaComplexidade ? "proteção Especial de Média Complexidade" : tipoProtecaoEnum == ETipoProtecao.ProgramasEProjetos ? "programas e projetos" : tipoProtecaoEnum == ETipoProtecao.BeneficiosEventuais ? "Beneficios eventuais" : "proteção especial de alta complexidade") + " deve ser igual ao valor total dos recursos estaduais disponibilizados.";

                if (lblTotalExecPrivada.Text != lblValorExercicioAtualPrivado.Text)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total da previsão de execução dos recursos para " + (tipoProtecaoEnum == ETipoProtecao.Basica ? "proteção Básica" : tipoProtecaoEnum == ETipoProtecao.EspecialMediaComplexidade ? "proteção Especial de Média Complexidade" : tipoProtecaoEnum == ETipoProtecao.ProgramasEProjetos ? "programas e projetos" : tipoProtecaoEnum == ETipoProtecao.BeneficiosEventuais ? "Beneficios eventuais" : "proteção especial de alta complexidade") + " deve ser igual ao valor total dos recursos estaduais disponibilizados.";
            }

            if (Request.QueryString["idTipo"] != null && tipoProtecao != 4 && tipoProtecao != 5)
            {
                decimal totalcusteio = 0;
                decimal totalcp = 0;
                if (Util.TryParseDecimal(lblTotalCusteio.Text).HasValue)
                    totalcusteio = Util.TryParseDecimal(lblTotalCusteio.Text).Value;

                if (Util.TryParseDecimal(lblTotalCofinanciamentoPublica.Text).HasValue)
                    totalcp = Util.TryParseDecimal(lblTotalCofinanciamentoPublica.Text).Value;

                if (totalcusteio > 0 && totalcp > 0)
                    if (Util.CalcularPercentualDecimal(totalcp, totalcusteio) > 100)
                        msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total previsto para Recursos Humanos na Rede Pública não pode ser superior a 100% do valor total do Cofinanciamento para Rede Pública de Proteção de Social " + (tipoProtecaoEnum == ETipoProtecao.Basica ? "Básica" : tipoProtecaoEnum == ETipoProtecao.EspecialMediaComplexidade ? "Especial de Média Complexidade" : tipoProtecaoEnum == ETipoProtecao.ProgramasEProjetos ? "programas e benefícios" : tipoProtecaoEnum == ETipoProtecao.BeneficiosEventuais ? "Beneficios eventuais" : "especial de alta complexidade") + ".";

                if (totalcusteio > 0 && totalcp == 0)
                    msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O valor total previsto para Recursos Humanos na Rede Pública não pode ser superior a 100% do valor total do Cofinanciamento para Rede Pública de Proteção de Social " + (tipoProtecaoEnum == ETipoProtecao.Basica ? "Básica" : tipoProtecaoEnum == ETipoProtecao.EspecialMediaComplexidade ? "Especial de Média Complexidade" : tipoProtecaoEnum == ETipoProtecao.ProgramasEProjetos ? "programas e benefícios" : tipoProtecaoEnum == ETipoProtecao.BeneficiosEventuais ? "Beneficios eventuais" : "especial de alta complexidade") + ".";
            }

            if (Request.QueryString["idTipo"] != null && tipoProtecao == 4)
            {
                var temp = new ProxyPrefeitura().Service
                    .GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(SessaoPmas.UsuarioLogado.Prefeitura.Id
                                                                              , tipoProtecao
                                                                              , exercicio)
                                                                              .Where(t => t.Unidade.ToLower()
                                                                              .Contains("além da renda") && t.Id == 0)
                                                                              .SingleOrDefault();
                if (temp != null)
                    if ((Convert.ToDecimal(txtInv1Publica.Text) + Convert.ToDecimal(txtInv2Publica.Text) + Convert.ToDecimal(txtInv3Publica.Text) + Convert.ToDecimal(txtInv4Publica.Text) +
                       Convert.ToDecimal(txtInv5Publica.Text) + Convert.ToDecimal(txtInv6Publica.Text) + Convert.ToDecimal(txtInv7Publica.Text) + Convert.ToDecimal(txtInv8Publica.Text) +
                       Convert.ToDecimal(txtInv9Publica.Text) + Convert.ToDecimal(txtInv10Publica.Text) + Convert.ToDecimal(txtInv11Publica.Text) +
                       Convert.ToDecimal(txtInv12Publica.Text)) > temp.PrevisaoOrcamentaria)
                        msg += (String.IsNullOrEmpty(msg) ? "" : "<br/>") + "O  valor total do investimento é maior que a previsão orçamentaria da etapa Além da Renda.";
            }


            if (!String.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
                lblInconsistencias.Text = msg;
                tbInconsistencias.Visible = true;
                return;
            }

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    proxy.Service.SaveCronogramaDesembolsoRedePrivada(cronogramaPrivada, exercicio);
                    proxy.Service.SaveCronogramaDesembolsoRedePublica(cronogramaPublica, exercicio);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Cronograma registrado com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            frmCronogramaDesembolso.Attributes.Add("class", "frame active");
            calcularRedePrivada();
            calcularRedePublica();
            calcularRede();
            calcularReprogramadoBeneficiosEventuais();
            calcularReprogramadoProgramasProjetos();
            calcularDemandasReprogramadoBeneficiosEventuais();
            calcularDemandasParlamentaresBeneficiosEventuais();
        }

        protected void lstRecursos_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.EmptyItem)
            {
                Label lblEmpty = e.Item.FindControl("lblEmpty") as Label;
                ETipoProtecao tipoProtecao = (ETipoProtecao)Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
                switch (tipoProtecao)
                {
                    case ETipoProtecao.Basica:
                        lblEmpty.Text = "Não existe registro de cofinanciamento estadual para a proteção social básica.";
                        break;
                    case ETipoProtecao.EspecialMediaComplexidade:
                        lblEmpty.Text = "Não existe registro de cofinanciamento estadual para a proteção social especial de média complexidade.";
                        break;
                    case ETipoProtecao.EspecialAltaComplexidade:
                        lblEmpty.Text = "Não existe registro de cofinanciamento estadual para a proteção social especial de alta complexidade.";
                        break;
                }
            }
        }

        protected void btnLimparRedePublica_Click(object sender, EventArgs e)
        {
            txtTot1Publica.Text = "0,00";
            txtTot2Publica.Text = "0,00";
            txtTot3Publica.Text = "0,00";
            txtTot4Publica.Text = "0,00";
            txtTot5Publica.Text = "0,00";
            txtTot6Publica.Text = "0,00";
            txtTot7Publica.Text = "0,00";
            txtTot8Publica.Text = "0,00";
            txtTot9Publica.Text = "0,00";
            txtTot10Publica.Text = "0,00";
            txtTot11Publica.Text = "0,00";
            txtTot12Publica.Text = "0,00";

            txtObras1.Text = "0,00";
            txtObras2.Text = "0,00";
            txtObras3.Text = "0,00";
            txtObras4.Text = "0,00";
            txtObras5.Text = "0,00";
            txtObras6.Text = "0,00";
            txtObras7.Text = "0,00";
            txtObras8.Text = "0,00";
            txtObras9.Text = "0,00";
            txtObras10.Text = "0,00";
            txtObras11.Text = "0,00";
            txtObras12.Text = "0,00";

            txtCusteio1.Text = "0,00";
            txtCusteio2.Text = "0,00";
            txtCusteio3.Text = "0,00";
            txtCusteio4.Text = "0,00";
            txtCusteio5.Text = "0,00";
            txtCusteio6.Text = "0,00";
            txtCusteio7.Text = "0,00";
            txtCusteio8.Text = "0,00";
            txtCusteio9.Text = "0,00";
            txtCusteio10.Text = "0,00";
            txtCusteio11.Text = "0,00";
            txtCusteio12.Text = "0,00";
            lblTotalCusteio.Text = "0,00";

            txtOutroCusteio1.Text = "0,00";
            txtOutroCusteio2.Text = "0,00";
            txtOutroCusteio3.Text = "0,00";
            txtOutroCusteio4.Text = "0,00";
            txtOutroCusteio5.Text = "0,00";
            txtOutroCusteio6.Text = "0,00";
            txtOutroCusteio7.Text = "0,00";
            txtOutroCusteio8.Text = "0,00";
            txtOutroCusteio9.Text = "0,00";
            txtOutroCusteio10.Text = "0,00";
            txtOutroCusteio11.Text = "0,00";
            txtOutroCusteio12.Text = "0,00";
            lblTotalOC.Text = "0,00";

            txtInv1Publica.Text = "0,00";
            txtInv2Publica.Text = "0,00";
            txtInv3Publica.Text = "0,00";
            txtInv4Publica.Text = "0,00";
            txtInv5Publica.Text = "0,00";
            txtInv6Publica.Text = "0,00";
            txtInv7Publica.Text = "0,00";
            txtInv8Publica.Text = "0,00";
            txtInv9Publica.Text = "0,00";
            txtInv10Publica.Text = "0,00";
            txtInv11Publica.Text = "0,00";
            txtInv12Publica.Text = "0,00";
            lblTotalInvestimentoPublica.Text = "0,00";

            lblTotalExecPublica1.Text = "0,00";
            lblTotalExecPublica2.Text = "0,00";
            lblTotalExecPublica3.Text = "0,00";
            lblTotalExecPublica4.Text = "0,00";
            lblTotalExecPublica5.Text = "0,00";
            lblTotalExecPublica6.Text = "0,00";
            lblTotalExecPublica7.Text = "0,00";
            lblTotalExecPublica8.Text = "0,00";
            lblTotalExecPublica9.Text = "0,00";
            lblTotalExecPublica10.Text = "0,00";
            lblTotalExecPublica11.Text = "0,00";
            lblTotalExecPublica12.Text = "0,00";

            lblTotalExecPublica.Text = "0,00";
        }

        protected void btnLimparRedePrivada_Click(object sender, EventArgs e)
        {

            txtTot1.Text = "0,00";
            txtTot2.Text = "0,00";
            txtTot3.Text = "0,00";
            txtTot4.Text = "0,00";
            txtTot5.Text = "0,00";
            txtTot6.Text = "0,00";
            txtTot7.Text = "0,00";
            txtTot8.Text = "0,00";
            txtTot9.Text = "0,00";
            txtTot10.Text = "0,00";
            txtTot11.Text = "0,00";
            txtTot12.Text = "0,00";

            txtObrasPrivada1.Text = "0,00";
            txtObrasPrivada2.Text = "0,00";
            txtObrasPrivada3.Text = "0,00";
            txtObrasPrivada4.Text = "0,00";
            txtObrasPrivada5.Text = "0,00";
            txtObrasPrivada6.Text = "0,00";
            txtObrasPrivada7.Text = "0,00";
            txtObrasPrivada8.Text = "0,00";
            txtObrasPrivada9.Text = "0,00";
            txtObrasPrivada10.Text = "0,00";
            txtObrasPrivada11.Text = "0,00";
            txtObrasPrivada12.Text = "0,00";


            txtRH1.Text = "0,00";
            txtRH2.Text = "0,00";
            txtRH3.Text = "0,00";
            txtRH4.Text = "0,00";
            txtRH5.Text = "0,00";
            txtRH6.Text = "0,00";
            txtRH7.Text = "0,00";
            txtRH8.Text = "0,00";
            txtRH9.Text = "0,00";
            txtRH10.Text = "0,00";
            txtRH11.Text = "0,00";
            txtRH12.Text = "0,00";
            lblTotalRecursosHumanos.Text = "0,00";

            txtMC1.Text = "0,00";
            txtMC2.Text = "0,00";
            txtMC3.Text = "0,00";
            txtMC4.Text = "0,00";
            txtMC5.Text = "0,00";
            txtMC6.Text = "0,00";
            txtMC7.Text = "0,00";
            txtMC8.Text = "0,00";
            txtMC9.Text = "0,00";
            txtMC10.Text = "0,00";
            txtMC11.Text = "0,00";
            txtMC12.Text = "0,00";
            lblTotalMateriaisConsumo.Text = "0,00";

            txtST1.Text = "0,00";
            txtST2.Text = "0,00";
            txtST3.Text = "0,00";
            txtST4.Text = "0,00";
            txtST5.Text = "0,00";
            txtST6.Text = "0,00";
            txtST7.Text = "0,00";
            txtST8.Text = "0,00";
            txtST9.Text = "0,00";
            txtST10.Text = "0,00";
            txtST11.Text = "0,00";
            txtST12.Text = "0,00";
            lbltotalServicos.Text = "0,00";

            lblTotalExecPrivada.Text = "0,00";

            //lblTot1.Text = "0,00";
            //lblTot2.Text = "0,00";
            //lblTot3.Text = "0,00";
            //lblTot4.Text = "0,00";
            //lblTot5.Text = "0,00";
            //lblTot6.Text = "0,00";
            //lblTot7.Text = "0,00";
            //lblTot8.Text = "0,00";
            //lblTot9.Text = "0,00";
            //lblTot10.Text = "0,00";
            //lblTot11.Text = "0,00";
            //lblTot12.Text = "0,00";
            //lblTotal.Text = "0,00";
        }
        #endregion

        #region helpers
        private void BloquearCamposPrivados()
        {
            if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) == 5)
            {
                tdInvestimentoPrivado.RowSpan = 2;

                txtMC1.Enabled = txtMC2.Enabled = txtMC3.Enabled = txtMC4.Enabled = txtMC5.Enabled = txtMC6.Enabled =
                txtMC7.Enabled = txtMC8.Enabled = txtMC9.Enabled = txtMC10.Enabled = txtMC11.Enabled = txtMC12.Enabled = false;
                txtST1.Enabled = txtST2.Enabled = txtST3.Enabled = txtST4.Enabled = txtST5.Enabled = txtST6.Enabled =
                txtST7.Enabled = txtST8.Enabled = txtST9.Enabled = txtST10.Enabled = txtST11.Enabled = txtST12.Enabled = false;
                txtRH1.Enabled = txtRH2.Enabled = txtRH3.Enabled = txtRH4.Enabled = txtRH5.Enabled = txtRH6.Enabled =
                txtRH7.Enabled = txtRH8.Enabled = txtRH9.Enabled = txtRH10.Enabled = txtRH11.Enabled = txtRH12.Enabled = false;
                txtTot1.Enabled = txtTot2.Enabled = txtTot3.Enabled = txtTot4.Enabled = txtTot5.Enabled = txtTot6.Enabled =
                   txtTot7.Enabled = txtTot8.Enabled = txtTot9.Enabled = txtTot10.Enabled = txtTot11.Enabled = txtTot12.Enabled = false;
            }
        }

        private void calcularRedePrivada()
        {
            try
            {
                decimal totalRecursosHumanos = 0, totalServicosTerceiros = 0, totalObrasPrivada = 0, totalMateriaisConsumo = 0, totalGeral = 0, total1 = 0, total2 = 0, total3 = 0, total4 = 0, total5 = 0, total6 = 0, total7 = 0, total8 = 0, total9 = 0, total10 = 0, total11 = 0, total12 = 0, totalReprogramadoPrivado = 0, totalDemandasPrivado = 0, totalReprogramadoDemandasPrivado = 0;



                if (Util.TryParseDecimal(txtMC1.Text).HasValue)
                {
                    total1 = total1 + Util.TryParseDecimal(txtMC1.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC1.Text).Value;
                }
                if (Util.TryParseDecimal(txtST1.Text).HasValue)
                {
                    total1 = total1 + Util.TryParseDecimal(txtST1.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST1.Text).Value;
                }



                if (Util.TryParseDecimal(txtRH1.Text).HasValue)
                {
                    total1 = total1 + Util.TryParseDecimal(txtRH1.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH1.Text).Value;
                }

                if (Util.TryParseDecimal(txtObrasPrivada1.Text).HasValue)
                {
                    total1 = total1 + Util.TryParseDecimal(txtObrasPrivada1.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada1.Text).Value;
                }



                lblTotalExecPrivada1.Text = total1.ToString();
                // lblTot1.Text = total1.ToString("n2");

                if (Util.TryParseDecimal(txtMC2.Text).HasValue)
                {
                    total2 = total2 + Util.TryParseDecimal(txtMC2.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC2.Text).Value;
                }
                if (Util.TryParseDecimal(txtST2.Text).HasValue)
                {
                    total2 = total2 + Util.TryParseDecimal(txtST2.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST2.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH2.Text).HasValue)
                {
                    total2 = total2 + Util.TryParseDecimal(txtRH2.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH2.Text).Value;
                }

                if (Util.TryParseDecimal(txtObrasPrivada2.Text).HasValue)
                {
                    total2 = total2 + Util.TryParseDecimal(txtObrasPrivada2.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada2.Text).Value;
                }
                lblTotalExecPrivada2.Text = total2.ToString();
                //  lblTot2.Text = total2.ToString("n2");

                if (Util.TryParseDecimal(txtMC3.Text).HasValue)
                {
                    total3 = total3 + Util.TryParseDecimal(txtMC3.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC3.Text).Value;
                }
                if (Util.TryParseDecimal(txtST3.Text).HasValue)
                {
                    total3 = total3 + Util.TryParseDecimal(txtST3.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST3.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH3.Text).HasValue)
                {
                    total3 = total3 + Util.TryParseDecimal(txtRH3.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH3.Text).Value;
                }
                if (Util.TryParseDecimal(txtObrasPrivada3.Text).HasValue)
                {
                    total3 = total3 + Util.TryParseDecimal(txtObrasPrivada3.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada3.Text).Value;
                }
                lblTotalExecPrivada3.Text = total3.ToString();
                // lblTot3.Text = total3.ToString("n2");

                if (Util.TryParseDecimal(txtMC4.Text).HasValue)
                {
                    total4 = total4 + Util.TryParseDecimal(txtMC4.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC4.Text).Value;
                }
                if (Util.TryParseDecimal(txtST4.Text).HasValue)
                {
                    total4 = total4 + Util.TryParseDecimal(txtST4.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST4.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH4.Text).HasValue)
                {
                    total4 = total4 + Util.TryParseDecimal(txtRH4.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH4.Text).Value;
                }
                if (Util.TryParseDecimal(txtObrasPrivada4.Text).HasValue)
                {
                    total4 = total4 + Util.TryParseDecimal(txtObrasPrivada4.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada4.Text).Value;
                }
                lblTotalExecPrivada4.Text = total4.ToString();
                //  lblTot4.Text = total4.ToString("n2");

                if (Util.TryParseDecimal(txtMC5.Text).HasValue)
                {
                    total5 = total5 + Util.TryParseDecimal(txtMC5.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC5.Text).Value;
                }
                if (Util.TryParseDecimal(txtST5.Text).HasValue)
                {
                    total5 = total5 + Util.TryParseDecimal(txtST5.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST5.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH5.Text).HasValue)
                {
                    total5 = total5 + Util.TryParseDecimal(txtRH5.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH5.Text).Value;
                }
                if (Util.TryParseDecimal(txtObrasPrivada5.Text).HasValue)
                {
                    total5 = total5 + Util.TryParseDecimal(txtObrasPrivada5.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada5.Text).Value;
                }
                lblTotalExecPrivada5.Text = total5.ToString();
                // lblTot5.Text = total5.ToString("n2");

                if (Util.TryParseDecimal(txtMC6.Text).HasValue)
                {
                    total6 = total6 + Util.TryParseDecimal(txtMC6.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC6.Text).Value;
                }
                if (Util.TryParseDecimal(txtST6.Text).HasValue)
                {
                    total6 = total6 + Util.TryParseDecimal(txtST6.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST6.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH6.Text).HasValue)
                {
                    total6 = total6 + Util.TryParseDecimal(txtRH6.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH6.Text).Value;
                }
                if (Util.TryParseDecimal(txtObrasPrivada6.Text).HasValue)
                {
                    total6 = total6 + Util.TryParseDecimal(txtObrasPrivada6.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada6.Text).Value;
                }
                lblTotalExecPrivada6.Text = total6.ToString();
                // lblTot6.Text = total6.ToString("n2");

                if (Util.TryParseDecimal(txtMC7.Text).HasValue)
                {
                    total7 = total7 + Util.TryParseDecimal(txtMC7.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC7.Text).Value;
                }
                if (Util.TryParseDecimal(txtST7.Text).HasValue)
                {
                    total7 = total7 + Util.TryParseDecimal(txtST7.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST7.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH7.Text).HasValue)
                {
                    total7 = total7 + Util.TryParseDecimal(txtRH7.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH7.Text).Value;
                }
                if (Util.TryParseDecimal(txtObrasPrivada7.Text).HasValue)
                {
                    total7 = total7 + Util.TryParseDecimal(txtObrasPrivada7.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada7.Text).Value;
                }
                lblTotalExecPrivada7.Text = total7.ToString();
                //  lblTot7.Text = total7.ToString("n2");

                if (Util.TryParseDecimal(txtMC8.Text).HasValue)
                {
                    total8 = total8 + Util.TryParseDecimal(txtMC8.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC8.Text).Value;
                }
                if (Util.TryParseDecimal(txtST8.Text).HasValue)
                {
                    total8 = total8 + Util.TryParseDecimal(txtST8.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST8.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH8.Text).HasValue)
                {
                    total8 = total8 + Util.TryParseDecimal(txtRH8.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH8.Text).Value;
                }
                if (Util.TryParseDecimal(txtObrasPrivada8.Text).HasValue)
                {
                    total8 = total8 + Util.TryParseDecimal(txtObrasPrivada8.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada8.Text).Value;
                }
                lblTotalExecPrivada8.Text = total8.ToString();
                //   lblTot8.Text = total8.ToString("n2");

                if (Util.TryParseDecimal(txtMC9.Text).HasValue)
                {
                    total9 = total9 + Util.TryParseDecimal(txtMC9.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC9.Text).Value;
                }
                if (Util.TryParseDecimal(txtST9.Text).HasValue)
                {
                    total9 = total9 + Util.TryParseDecimal(txtST9.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST9.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH9.Text).HasValue)
                {
                    total9 = total9 + Util.TryParseDecimal(txtRH9.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH9.Text).Value;
                }
                if (Util.TryParseDecimal(txtObrasPrivada9.Text).HasValue)
                {
                    total9 = total9 + Util.TryParseDecimal(txtObrasPrivada9.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada9.Text).Value;
                }
                lblTotalExecPrivada9.Text = total9.ToString();
                //  lblTot9.Text = total9.ToString("n2");

                if (Util.TryParseDecimal(txtMC10.Text).HasValue)
                {
                    total10 = total10 + Util.TryParseDecimal(txtMC10.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC10.Text).Value;
                }
                if (Util.TryParseDecimal(txtST10.Text).HasValue)
                {
                    total10 = total10 + Util.TryParseDecimal(txtST10.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST10.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH10.Text).HasValue)
                {
                    total10 = total10 + Util.TryParseDecimal(txtRH10.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH10.Text).Value;
                }
                if (Util.TryParseDecimal(txtObrasPrivada10.Text).HasValue)
                {
                    total10 = total10 + Util.TryParseDecimal(txtObrasPrivada10.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada10.Text).Value;
                }
                lblTotalExecPrivada10.Text = total10.ToString();
                //    lblTot10.Text = total10.ToString("n2");

                if (Util.TryParseDecimal(txtMC11.Text).HasValue)
                {
                    total11 = total11 + Util.TryParseDecimal(txtMC11.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC11.Text).Value;
                }
                if (Util.TryParseDecimal(txtST11.Text).HasValue)
                {
                    total11 = total11 + Util.TryParseDecimal(txtST11.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST11.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH11.Text).HasValue)
                {
                    total11 = total11 + Util.TryParseDecimal(txtRH11.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH11.Text).Value;
                }
                if (Util.TryParseDecimal(txtObrasPrivada11.Text).HasValue)
                {
                    total11 = total11 + Util.TryParseDecimal(txtObrasPrivada11.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada11.Text).Value;
                }
                lblTotalExecPrivada11.Text = total11.ToString();
                //     lblTot11.Text = total11.ToString("n2");

                if (Util.TryParseDecimal(txtMC12.Text).HasValue)
                {
                    total12 = total12 + Util.TryParseDecimal(txtMC12.Text).Value;
                    totalMateriaisConsumo = totalMateriaisConsumo + Util.TryParseDecimal(txtMC12.Text).Value;
                }
                if (Util.TryParseDecimal(txtST12.Text).HasValue)
                {
                    total12 = total12 + Util.TryParseDecimal(txtST12.Text).Value;
                    totalServicosTerceiros = totalServicosTerceiros + Util.TryParseDecimal(txtST12.Text).Value;
                }
                if (Util.TryParseDecimal(txtRH12.Text).HasValue)
                {
                    total12 = total12 + Util.TryParseDecimal(txtRH12.Text).Value;
                    totalRecursosHumanos = totalRecursosHumanos + Util.TryParseDecimal(txtRH12.Text).Value;
                }
                if (Util.TryParseDecimal(txtObrasPrivada12.Text).HasValue)
                {
                    total12 = total12 + Util.TryParseDecimal(txtObrasPrivada12.Text).Value;
                    totalObrasPrivada = totalObrasPrivada + Util.TryParseDecimal(txtObrasPrivada12.Text).Value;
                }

                lblTotalExecPrivada12.Text = total12.ToString();
                lblTotalObrasPrivada.Text = totalObrasPrivada.ToString("N2");

                decimal totalRecursos = 0;

                if (Util.TryParseDecimal(txtTot1.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot1.Text).Value;

                if (Util.TryParseDecimal(txtTot2.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot2.Text).Value;

                if (Util.TryParseDecimal(txtTot3.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot3.Text).Value;

                if (Util.TryParseDecimal(txtTot4.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot4.Text).Value;

                if (Util.TryParseDecimal(txtTot5.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot5.Text).Value;

                if (Util.TryParseDecimal(txtTot6.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot6.Text).Value;

                if (Util.TryParseDecimal(txtTot7.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot7.Text).Value;

                if (Util.TryParseDecimal(txtTot8.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot8.Text).Value;

                if (Util.TryParseDecimal(txtTot9.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot9.Text).Value;

                if (Util.TryParseDecimal(txtTot10.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot10.Text).Value;

                if (Util.TryParseDecimal(txtTot11.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot11.Text).Value;

                if (Util.TryParseDecimal(txtTot12.Text).HasValue)
                    totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot12.Text).Value;

                totalReprogramadoPrivado = Util.TryParseDecimal(lblTotalRecursosReprogramadoPrivada.Text).Value;

                lblTotal.Text = totalRecursos.ToString("n2");

                if (Util.TryParseDecimal(txtRecursosHumanosReprogramadoPrivado.Text).HasValue)
                    totalReprogramadoPrivado = Util.TryParseDecimal(txtRecursosHumanosReprogramadoPrivado.Text).Value;

                if (Util.TryParseDecimal(txtOutrasCusteioReprogramadoPrivado.Text).HasValue)
                    totalReprogramadoPrivado += Util.TryParseDecimal(txtOutrasCusteioReprogramadoPrivado.Text).Value;

                if (Util.TryParseDecimal(txtEquipamentosPrivadoReprogramado.Text).HasValue)
                    totalReprogramadoPrivado += Util.TryParseDecimal(txtEquipamentosPrivadoReprogramado.Text).Value;

                if (Util.TryParseDecimal(txtObrasReprogramadoPrivado.Text).HasValue)
                    totalReprogramadoPrivado += Util.TryParseDecimal(txtObrasReprogramadoPrivado.Text).Value;

                lblTotalRecursosReprogramadoPrivada.Text = totalReprogramadoPrivado.ToString("n2");


                if (Util.TryParseDecimal(txtRecursosHumanosDemandasPrivado.Text).HasValue)
                    totalDemandasPrivado = Util.TryParseDecimal(txtRecursosHumanosDemandasPrivado.Text).Value;

                if (Util.TryParseDecimal(txtOutrasCusteioDemandasPrivado.Text).HasValue)
                    totalDemandasPrivado += Util.TryParseDecimal(txtOutrasCusteioDemandasPrivado.Text).Value;

                if (Util.TryParseDecimal(txtEquipamentosPrivadoDemandas.Text).HasValue)
                    totalDemandasPrivado += Util.TryParseDecimal(txtEquipamentosPrivadoDemandas.Text).Value;

                if (Util.TryParseDecimal(txtObrasDemandasPrivado.Text).HasValue)
                    totalDemandasPrivado += Util.TryParseDecimal(txtObrasDemandasPrivado.Text).Value;

                lblTotalDemandasParlamentaresPrivada.Text = totalDemandasPrivado.ToString("n2");


                if (Util.TryParseDecimal(txtRecursosHumanosReprogramadoDemandasPrivado.Text).HasValue)
                    totalReprogramadoDemandasPrivado = Util.TryParseDecimal(txtRecursosHumanosReprogramadoDemandasPrivado.Text).Value;

                if (Util.TryParseDecimal(txtOutrasCusteioReprogramadoDemandasPrivado.Text).HasValue)
                    totalReprogramadoDemandasPrivado += Util.TryParseDecimal(txtOutrasCusteioReprogramadoDemandasPrivado.Text).Value;

                if (Util.TryParseDecimal(txtEquipamentosPrivadoReprogramadoDemandas.Text).HasValue)
                    totalReprogramadoDemandasPrivado += Util.TryParseDecimal(txtEquipamentosPrivadoReprogramadoDemandas.Text).Value;

                if (Util.TryParseDecimal(txtObrasReprogramadoDemandasPrivado.Text).HasValue)
                    totalReprogramadoDemandasPrivado += Util.TryParseDecimal(txtObrasReprogramadoDemandasPrivado.Text).Value;


                lblTotalReprogramadoDemandasParlamentaresPrivada.Text = totalReprogramadoDemandasPrivado.ToString("N2");


                lblTotalMateriaisConsumo.Text = totalMateriaisConsumo.ToString("n2");
                lbltotalServicos.Text = totalServicosTerceiros.ToString("n2");
                lblTotalRecursosHumanos.Text = totalRecursosHumanos.ToString("n2");

                totalGeral = total1 + total2 + total3 + total4 + total5 + total6 + total7 + total8 + total9 + total10 + total11 + total12;

                lblTotalExecPrivada.Text = totalGeral.ToString("n2");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void calcularRedePublica()
        {
            try
            {
                decimal totalCusteio = 0,
                    totalInvestimento = 0,
                    totalObras = 0,
                    totalGeral = 0,
                    totalOC = 0,
                    total1 = 0,
                    total2 = 0,
                    total3 = 0,
                    total4 = 0,
                    total5 = 0,
                    total6 = 0,
                    total7 = 0,
                    total8 = 0,
                    total9 = 0,
                    total10 = 0,
                    total11 = 0,
                    total12 = 0,
                    totalObrasPublicas = 0,
                    totalReprogramacaoPublica = 0,
                    totalDemandasPublica = 0,
                    totalReprogramadoDemandasPublica = 0;

                int exercicio = Convert.ToInt32(hdfAno.Value);


                if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) != 4 && (Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) != 5))
                {
                    if (Util.TryParseDecimal(txtCusteio1.Text).HasValue)
                    {
                        total1 = total1 + Util.TryParseDecimal(txtCusteio1.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio1.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv1Publica.Text).HasValue)
                    {
                        total1 = total1 + Util.TryParseDecimal(txtInv1Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv1Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio1.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio1.Text).Value;
                        total1 += Util.TryParseDecimal(txtOutroCusteio1.Text).Value; 
                    }

                    if (Util.TryParseDecimal(txtObras1.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras1.Text).Value;
                        total1 += Util.TryParseDecimal(txtObras1.Text).Value;
                        totalObras = totalObras +  Util.TryParseDecimal(txtObras1.Text).Value;
                    }


                    lblTotalExecPublica1.Text = total1.ToString("n2");

                    if (Util.TryParseDecimal(txtCusteio2.Text).HasValue)
                    {
                        total2 = total2 + Util.TryParseDecimal(txtCusteio2.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio2.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv2Publica.Text).HasValue)
                    {
                        total2 = total2 + Util.TryParseDecimal(txtInv2Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv2Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio2.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio2.Text).Value;
                        total2 += Util.TryParseDecimal(txtOutroCusteio2.Text).Value;


                    }


                    if (Util.TryParseDecimal(txtObras2.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras2.Text).Value;
                        total2 += Util.TryParseDecimal(txtObras2.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras2.Text).Value;
                    }

                    lblTotalExecPublica2.Text = total2.ToString("n2");

                    if (Util.TryParseDecimal(txtCusteio3.Text).HasValue)
                    {
                        total3 = total3 + Util.TryParseDecimal(txtCusteio3.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio3.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv3Publica.Text).HasValue)
                    {
                        total3 = total3 + Util.TryParseDecimal(txtInv3Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv3Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio3.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio3.Text).Value;
                        total3 += Util.TryParseDecimal(txtOutroCusteio3.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras3.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras3.Text).Value;
                        total3 += Util.TryParseDecimal(txtObras3.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras3.Text).Value;
                    }

                    lblTotalExecPublica3.Text = total3.ToString("n2");

                    if (Util.TryParseDecimal(txtCusteio4.Text).HasValue)
                    {
                        total4 = total4 + Util.TryParseDecimal(txtCusteio4.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio4.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv4Publica.Text).HasValue)
                    {
                        total4 = total4 + Util.TryParseDecimal(txtInv4Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv4Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio4.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio4.Text).Value;
                        total4 += Util.TryParseDecimal(txtOutroCusteio4.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras4.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras4.Text).Value;
                        total4 += Util.TryParseDecimal(txtObras4.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras4.Text).Value;
                    }

                    lblTotalExecPublica4.Text = total4.ToString("n2");
                    
                    
                    if (Util.TryParseDecimal(txtCusteio5.Text).HasValue)
                    {
                        total5 = total5 + Util.TryParseDecimal(txtCusteio5.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio5.Text).Value;
                    }
                    if (Util.TryParseDecimal(txtInv5Publica.Text).HasValue)
                    {
                        total5 = total5 + Util.TryParseDecimal(txtInv5Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv5Publica.Text).Value;
                    }
                    if (Util.TryParseDecimal(txtOutroCusteio5.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio5.Text).Value;
                        total5 += Util.TryParseDecimal(txtOutroCusteio5.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras5.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtObras5.Text).Value;
                        total5 += Util.TryParseDecimal(txtObras5.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras5.Text).Value;
                    }

                    lblTotalExecPublica5.Text = total5.ToString("n2");

                    if (Util.TryParseDecimal(txtCusteio6.Text).HasValue)
                    {
                        total6 = total6 + Util.TryParseDecimal(txtCusteio6.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio6.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv6Publica.Text).HasValue)
                    {
                        total6 = total6 + Util.TryParseDecimal(txtInv6Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv6Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio6.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio6.Text).Value;
                        total6 += Util.TryParseDecimal(txtOutroCusteio6.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras6.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras6.Text).Value;
                        total6 += Util.TryParseDecimal(txtObras6.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras6.Text).Value;
                    }


                    lblTotalExecPublica6.Text = total6.ToString("n2");

                    if (Util.TryParseDecimal(txtCusteio7.Text).HasValue)
                    {
                        total7 = total7 + Util.TryParseDecimal(txtCusteio7.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio7.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv7Publica.Text).HasValue)
                    {
                        total7 = total7 + Util.TryParseDecimal(txtInv7Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv7Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio7.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio7.Text).Value;
                        total7 += Util.TryParseDecimal(txtOutroCusteio7.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras7.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras7.Text).Value;
                        total7 += Util.TryParseDecimal(txtObras7.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras7.Text).Value;
                    }

                    lblTotalExecPublica7.Text = total7.ToString("n2");

                    if (Util.TryParseDecimal(txtCusteio8.Text).HasValue)
                    {
                        total8 = total8 + Util.TryParseDecimal(txtCusteio8.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio8.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv8Publica.Text).HasValue)
                    {
                        total8 = total8 + Util.TryParseDecimal(txtInv8Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv8Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio8.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio8.Text).Value;
                        total8 += Util.TryParseDecimal(txtOutroCusteio8.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras8.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras8.Text).Value;
                        total8 += Util.TryParseDecimal(txtObras8.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras8.Text).Value;
                    }

                    lblTotalExecPublica8.Text = total8.ToString("n2");

                    if (Util.TryParseDecimal(txtCusteio9.Text).HasValue)
                    {
                        total9 = total9 + Util.TryParseDecimal(txtCusteio9.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio9.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv9Publica.Text).HasValue)
                    {
                        total9 = total9 + Util.TryParseDecimal(txtInv9Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv9Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio9.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio9.Text).Value;
                        total9 += Util.TryParseDecimal(txtOutroCusteio9.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras9.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras9.Text).Value;
                        total9 += Util.TryParseDecimal(txtObras9.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras9.Text).Value;
                    }

                    lblTotalExecPublica9.Text = total9.ToString("n2");

                    if (Util.TryParseDecimal(txtCusteio10.Text).HasValue)
                    {
                        total10 = total10 + Util.TryParseDecimal(txtCusteio10.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio10.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv10Publica.Text).HasValue)
                    {
                        total10 = total10 + Util.TryParseDecimal(txtInv10Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv10Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio10.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio10.Text).Value;
                        total10 += Util.TryParseDecimal(txtOutroCusteio10.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras10.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras10.Text).Value;
                        total10 += Util.TryParseDecimal(txtObras10.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras10.Text).Value;
                    }

                    lblTotalExecPublica10.Text = total10.ToString("n2");

                    if (Util.TryParseDecimal(txtCusteio11.Text).HasValue)
                    {
                        total11 = total11 + Util.TryParseDecimal(txtCusteio11.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio11.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv11Publica.Text).HasValue)
                    {
                        total11 = total11 + Util.TryParseDecimal(txtInv11Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv11Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio11.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio11.Text).Value;
                        total11 += Util.TryParseDecimal(txtOutroCusteio11.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras11.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras11.Text).Value;
                        total11 += Util.TryParseDecimal(txtObras11.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras11.Text).Value;
                    }

                    lblTotalExecPublica11.Text = total11.ToString("n2");

                    if (Util.TryParseDecimal(txtCusteio12.Text).HasValue)
                    {
                        total12 = total12 + Util.TryParseDecimal(txtCusteio12.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio12.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv12Publica.Text).HasValue)
                    {
                        total12 = total12 + Util.TryParseDecimal(txtInv12Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv12Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtOutroCusteio12.Text).HasValue)
                    {
                        totalOC += Util.TryParseDecimal(txtOutroCusteio12.Text).Value;
                        total12 += Util.TryParseDecimal(txtOutroCusteio12.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras12.Text).HasValue)
                    {
                        //totalOC += Util.TryParseDecimal(txtObras12.Text).Value;
                        total12 += Util.TryParseDecimal(txtObras12.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras12.Text).Value;
                    }

                    lblTotalExecPublica12.Text = total12.ToString("N2");
                    lblTotalObraPublica.Text = totalObras.ToString("N2");


                    decimal totalRecursos = 0;

                    if (Util.TryParseDecimal(txtTot1Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot1Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot2Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot2Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot3Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot3Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot4Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot4Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot5Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot5Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot6Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot6Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot7Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot7Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot8Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot8Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot9Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot9Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot10Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot10Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot11Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot11Publica.Text).Value;

                    if (Util.TryParseDecimal(txtTot12Publica.Text).HasValue)
                        totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot12Publica.Text).Value;


                    //Verificar regra para reprogramação de valores
                    //if (Util.TryParseDecimal(txtReprogramacaoRecursosDisponibilizados.Text).HasValue)
                    //    totalRecursos = totalRecursos + Util.TryParseDecimal(txtReprogramacaoRecursosDisponibilizados.Text).Value;

                    if (Util.TryParseDecimal(txtReprogramacaoRH.Text).HasValue)
                        totalReprogramacaoPublica = Util.TryParseDecimal(txtReprogramacaoRH.Text).Value;


                    if (Util.TryParseDecimal(txtReprogramacaoCusteio.Text).HasValue)
                        totalReprogramacaoPublica += Util.TryParseDecimal(txtReprogramacaoCusteio.Text).Value;

                    if(Util.TryParseDecimal(txtReprogramacaoInvestimento.Text).HasValue)
                        totalReprogramacaoPublica += Util.TryParseDecimal(txtReprogramacaoInvestimento.Text).Value;

                    if (Util.TryParseDecimal(txtReprogramacaoObras.Text).HasValue)
                        totalReprogramacaoPublica += Util.TryParseDecimal(txtReprogramacaoObras.Text).Value;

                    lblTotalRecursosReprogramadoAnoAnterior.Text = totalReprogramacaoPublica.ToString("n2");

                    if (Util.TryParseDecimal(txtDemandasRH.Text).HasValue)
                        totalDemandasPublica = Util.TryParseDecimal(txtDemandasRH.Text).Value;

                    if (Util.TryParseDecimal(txtDemandasCusteio.Text).HasValue)
                        totalDemandasPublica += Util.TryParseDecimal(txtDemandasCusteio.Text).Value;

                    if (Util.TryParseDecimal(txtDemandasInvestimento.Text).HasValue)
                        totalDemandasPublica += Util.TryParseDecimal(txtDemandasInvestimento.Text).Value;

                    if (Util.TryParseDecimal(txtDemandasObras.Text).HasValue)
                        totalDemandasPublica += Util.TryParseDecimal(txtDemandasObras.Text).Value;

                    lblTotalDemandasParlamentares.Text = totalDemandasPublica.ToString("n2");


                    if (Util.TryParseDecimal(txtReprogramadoDemandasRH.Text).HasValue)
                        totalReprogramadoDemandasPublica = Util.TryParseDecimal(txtReprogramadoDemandasRH.Text).Value;

                    if (Util.TryParseDecimal(txtReprogramadoDemandasCusteio.Text).HasValue)
                        totalReprogramadoDemandasPublica += Util.TryParseDecimal(txtReprogramadoDemandasCusteio.Text).Value;

                    if (Util.TryParseDecimal(txtReprogramadoDemandasInvestimento.Text).HasValue)
                        totalReprogramadoDemandasPublica += Util.TryParseDecimal(txtReprogramadoDemandasInvestimento.Text).Value;

                    if (Util.TryParseDecimal(txtReprogramadoDemandasObras.Text).HasValue)
                        totalReprogramadoDemandasPublica += Util.TryParseDecimal(txtReprogramadoDemandasObras.Text).Value;

                    lblTotalReprogramadoDemandasParlamentares.Text = totalReprogramadoDemandasPublica.ToString("N2");


                    lblTotalCusteio.Text = totalCusteio.ToString("n2");
                    lblTotalInvestimentoPublica.Text = totalInvestimento.ToString("n2");

                    lblTotalOC.Text = totalOC.ToString("n2");
                    lblTotalPublica.Text = totalRecursos.ToString("n2");


                    var ReprogramacaoPublicaTotal = Convert.ToDecimal(lblTotalRecursosReprogramadoAnoAnterior.Text);
                    totalGeral = total1 + total2 + total3 + total4 + total5 + total6 + total7 + total8 + total9 + total10 + total11 + total12;
                    lblTotalExecPublica.Text = totalGeral.ToString("n2");
                    //lblTotalCofinanciamentoPublica.Text = totalGeral.ToString("n2");

                }
                else if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) == 4)
                {

                    decimal totalPublica = 0, totalPublica1 = 0, totalPublica2 = 0, totalPublica3 = 0, totalPublica4 = 0, totalPublica5 = 0, totalPublica6 = 0, totalPublica7 = 0, totalPublica8 = 0, totalPublica9 = 0, totalPublica10 = 0, totalPublica11 = 0, totalPublica12 = 0;

                    decimal totalReprogramacaoProgramasProjetos = 0;

                    if (Util.TryParseDecimal(txtTot1Publica.Text).HasValue)
                    {
                        totalPublica1 = totalPublica1 + Util.TryParseDecimal(txtTot1Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot1Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio1.Text).HasValue)
                    {
                        total1 = total1 + Util.TryParseDecimal(txtCusteio1.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio1.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv1Publica.Text).HasValue)
                    {
                        total1 = total1 + Util.TryParseDecimal(txtInv1Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv1Publica.Text).Value;
                    }


                    if (Util.TryParseDecimal(txtObras1.Text).HasValue)
                    {
                        total1 = total1 + Util.TryParseDecimal(txtObras1.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras1.Text).Value;
                    }



                    lblTotalExecPublica1.Text = total1.ToString("n2");
                    //lblTot1Publica.Text = total1.ToString("n2");

                    if (Util.TryParseDecimal(txtTot2Publica.Text).HasValue)
                    {
                        totalPublica2 = totalPublica2 + Util.TryParseDecimal(txtTot2Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot2Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio2.Text).HasValue)
                    {
                        total2 = total2 + Util.TryParseDecimal(txtCusteio2.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio2.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv2Publica.Text).HasValue)
                    {
                        total2 = total2 + Util.TryParseDecimal(txtInv2Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv2Publica.Text).Value;
                    }


                    if (Util.TryParseDecimal(txtObras2.Text).HasValue)
                    {
                        total2 = total2 + Util.TryParseDecimal(txtObras2.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras2.Text).Value;
                    }

                    lblTotalExecPublica2.Text = total2.ToString("n2");
                    //lblTot2Publica.Text = total2.ToString("n2");

                    if (Util.TryParseDecimal(txtTot3Publica.Text).HasValue)
                    {
                        totalPublica3 = totalPublica3 + Util.TryParseDecimal(txtTot3Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot3Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio3.Text).HasValue)
                    {
                        total3 = total3 + Util.TryParseDecimal(txtCusteio3.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio3.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv3Publica.Text).HasValue)
                    {
                        total3 = total3 + Util.TryParseDecimal(txtInv3Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv3Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras3.Text).HasValue)
                    {
                        total3 = total3 + Util.TryParseDecimal(txtObras3.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras3.Text).Value;
                    }
                    lblTotalExecPublica3.Text = total3.ToString("n2");
                    //lblTot3Publica.Text = total3.ToString("n2");

                    if (Util.TryParseDecimal(txtTot4Publica.Text).HasValue)
                    {
                        totalPublica4 = totalPublica4 + Util.TryParseDecimal(txtTot4Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot4Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio4.Text).HasValue)
                    {
                        total4 = total4 + Util.TryParseDecimal(txtCusteio4.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio4.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv4Publica.Text).HasValue)
                    {
                        total4 = total4 + Util.TryParseDecimal(txtInv4Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv4Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras4.Text).HasValue)
                    {
                        total4 = total4 + Util.TryParseDecimal(txtObras4.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras4.Text).Value;
                    }

                    lblTotalExecPublica4.Text = total4.ToString("n2");
                    //lblTot4Publica.Text = total4.ToString("n2");

                    if (Util.TryParseDecimal(txtTot5Publica.Text).HasValue)
                    {
                        totalPublica5 = totalPublica5 + Util.TryParseDecimal(txtTot5Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot5Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio5.Text).HasValue)
                    {
                        total5 = total5 + Util.TryParseDecimal(txtCusteio5.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio5.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv5Publica.Text).HasValue)
                    {
                        total5 = total5 + Util.TryParseDecimal(txtInv5Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv5Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras5.Text).HasValue)
                    {
                        total5 = total5 + Util.TryParseDecimal(txtObras5.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras5.Text).Value;
                    }
                    lblTotalExecPublica5.Text = total5.ToString("n2");
                    //lblTot5Publica.Text = total5.ToString("n2");

                    if (Util.TryParseDecimal(txtTot6Publica.Text).HasValue)
                    {
                        totalPublica6 = totalPublica6 + Util.TryParseDecimal(txtTot6Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot6Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio6.Text).HasValue)
                    {
                        total6 = total6 + Util.TryParseDecimal(txtCusteio6.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio6.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv6Publica.Text).HasValue)
                    {
                        total6 = total6 + Util.TryParseDecimal(txtInv6Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv6Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras6.Text).HasValue)
                    {
                        total6 = total6 + Util.TryParseDecimal(txtObras6.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras6.Text).Value;
                    }
                    lblTotalExecPublica6.Text = total6.ToString("n2");
                    //lblTot6Publica.Text = total6.ToString("n2");

                    if (Util.TryParseDecimal(txtTot7Publica.Text).HasValue)
                    {
                        totalPublica7 = totalPublica7 + Util.TryParseDecimal(txtTot7Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot7Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio7.Text).HasValue)
                    {
                        total7 = total7 + Util.TryParseDecimal(txtCusteio7.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio7.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv7Publica.Text).HasValue)
                    {
                        total7 = total7 + Util.TryParseDecimal(txtInv7Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv7Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras7.Text).HasValue)
                    {
                        total7 = total7 + Util.TryParseDecimal(txtObras7.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras7.Text).Value;
                    }
                    lblTotalExecPublica7.Text = total7.ToString("n2");
                    //lblTot7Publica.Text = total7.ToString("n2");

                    if (Util.TryParseDecimal(txtTot8Publica.Text).HasValue)
                    {
                        totalPublica8 = totalPublica8 + Util.TryParseDecimal(txtTot8Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot8Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio8.Text).HasValue)
                    {
                        total8 = total8 + Util.TryParseDecimal(txtCusteio8.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio8.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv8Publica.Text).HasValue)
                    {
                        total8 = total8 + Util.TryParseDecimal(txtInv8Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv8Publica.Text).Value;
                    }
                    if (Util.TryParseDecimal(txtObras8.Text).HasValue)
                    {
                        total8 = total8 + Util.TryParseDecimal(txtObras8.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras8.Text).Value;
                    }
                    lblTotalExecPublica8.Text = total8.ToString("n2");
                    //lblTot8Publica.Text = total8.ToString("n2");

                    if (Util.TryParseDecimal(txtTot9Publica.Text).HasValue)
                    {
                        totalPublica9 = totalPublica9 + Util.TryParseDecimal(txtTot9Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot9Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio9.Text).HasValue)
                    {
                        total9 = total9 + Util.TryParseDecimal(txtCusteio9.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio9.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv9Publica.Text).HasValue)
                    {
                        total9 = total9 + Util.TryParseDecimal(txtInv9Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv9Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras9.Text).HasValue)
                    {
                        total9 = total9 + Util.TryParseDecimal(txtObras9.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras9.Text).Value;
                    }
                    lblTotalExecPublica9.Text = total9.ToString("n2");
                    //lblTot9Publica.Text = total9.ToString("n2");
                    if (Util.TryParseDecimal(txtTot10Publica.Text).HasValue)
                    {
                        totalPublica10 = totalPublica10 + Util.TryParseDecimal(txtTot10Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot10Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio10.Text).HasValue)
                    {
                        total10 = total10 + Util.TryParseDecimal(txtCusteio10.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio10.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv10Publica.Text).HasValue)
                    {
                        total10 = total10 + Util.TryParseDecimal(txtInv10Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv10Publica.Text).Value;
                    }
                    if (Util.TryParseDecimal(txtObras10.Text).HasValue)
                    {
                        total10 = total10 + Util.TryParseDecimal(txtObras10.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras10.Text).Value;
                    }
                    lblTotalExecPublica10.Text = total10.ToString("n2");

                    if (Util.TryParseDecimal(txtTot11Publica.Text).HasValue)
                    {
                        totalPublica11 = totalPublica11 + Util.TryParseDecimal(txtTot11Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot11Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio11.Text).HasValue)
                    {
                        total11 = total11 + Util.TryParseDecimal(txtCusteio11.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio11.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv11Publica.Text).HasValue)
                    {
                        total11 = total11 + Util.TryParseDecimal(txtInv11Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv11Publica.Text).Value;
                    }
                    if (Util.TryParseDecimal(txtObras11.Text).HasValue)
                    {
                        total11 = total11 + Util.TryParseDecimal(txtObras11.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras11.Text).Value;
                    }
                    lblTotalExecPublica11.Text = total11.ToString("n2");

                    if (Util.TryParseDecimal(txtTot12Publica.Text).HasValue)
                    {
                        totalPublica12 = totalPublica12 + Util.TryParseDecimal(txtTot12Publica.Text).Value;
                        totalPublica = totalPublica + Util.TryParseDecimal(txtTot12Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtCusteio12.Text).HasValue)
                    {
                        total12 = total12 + Util.TryParseDecimal(txtCusteio12.Text).Value;
                        totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio12.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtInv12Publica.Text).HasValue)
                    {
                        total12 = total12 + Util.TryParseDecimal(txtInv12Publica.Text).Value;
                        totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv12Publica.Text).Value;
                    }

                    if (Util.TryParseDecimal(txtObras12.Text).HasValue)
                    {
                        total12 = total12 + Util.TryParseDecimal(txtObras12.Text).Value;
                        totalObras = totalObras + Util.TryParseDecimal(txtObras12.Text).Value;
                    }


                    if (Util.TryParseDecimal(txtReprogramacaoCusteio.Text).HasValue)
                        totalReprogramacaoProgramasProjetos += Util.TryParseDecimal(txtReprogramacaoCusteio.Text).Value;

                    if (Util.TryParseDecimal(txtReprogramacaoInvestimento.Text).HasValue)
                        totalReprogramacaoProgramasProjetos += Util.TryParseDecimal(txtReprogramacaoInvestimento.Text).Value;

                    if (Util.TryParseDecimal(txtReprogramacaoObras.Text).HasValue)
                        totalReprogramacaoProgramasProjetos += Util.TryParseDecimal(txtReprogramacaoObras.Text).Value;

                   

                    lblTotalExecPublica12.Text = total12.ToString("n2");

                    totalGeral = total1 + total2 + total3 + total4 + total5 + total6 + total7 + total8 + total9 + total10 + total11 + total12;

                    lblTotalExecPublica.Text = totalGeral.ToString("n2");
                    lblTotalPublica.Text = totalPublica.ToString("n2");
                    lblTotalCusteio.Text = totalCusteio.ToString("n2");
                    lblTotalObraPublica.Text = totalObras.ToString("n2");
                    lblTotalInvestimentoPublica.Text = totalInvestimento.ToString("n2");
                    totalGeral += totalReprogramacaoProgramasProjetos;


                    lblTotalCofinanciamentoPublica.Text = totalGeral.ToString("n2") ; 

                }
                else
                {
                    if (exercicio == 2021)
                    {
                        if (Util.TryParseDecimal(txtCusteio1.Text).HasValue)
                        {
                            total1 = total1 + Util.TryParseDecimal(txtCusteio1.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio1.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv1Publica.Text).HasValue)
                        {
                            total1 = total1 + Util.TryParseDecimal(txtInv1Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv1Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio1.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio1.Text).Value;
                            total1 += Util.TryParseDecimal(txtOutroCusteio1.Text).Value; ;
                        }

                        lblTotalExecPublica1.Text = total1.ToString("n2");

                        if (Util.TryParseDecimal(txtCusteio2.Text).HasValue)
                        {
                            total2 = total2 + Util.TryParseDecimal(txtCusteio2.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio2.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv2Publica.Text).HasValue)
                        {
                            total2 = total2 + Util.TryParseDecimal(txtInv2Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv2Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio2.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio2.Text).Value;
                            total2 += Util.TryParseDecimal(txtOutroCusteio2.Text).Value; ;
                        }

                        lblTotalExecPublica2.Text = total2.ToString("n2");

                        if (Util.TryParseDecimal(txtCusteio3.Text).HasValue)
                        {
                            total3 = total3 + Util.TryParseDecimal(txtCusteio3.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio3.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv3Publica.Text).HasValue)
                        {
                            total3 = total3 + Util.TryParseDecimal(txtInv3Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv3Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio3.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio3.Text).Value;
                            total3 += Util.TryParseDecimal(txtOutroCusteio3.Text).Value;
                        }

                        lblTotalExecPublica3.Text = total3.ToString("n2");

                        if (Util.TryParseDecimal(txtCusteio4.Text).HasValue)
                        {
                            total4 = total4 + Util.TryParseDecimal(txtCusteio4.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio4.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv4Publica.Text).HasValue)
                        {
                            total4 = total4 + Util.TryParseDecimal(txtInv4Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv4Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio4.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio4.Text).Value;
                            total4 += Util.TryParseDecimal(txtOutroCusteio4.Text).Value;
                        }
                        lblTotalExecPublica4.Text = total4.ToString("n2");
                        if (Util.TryParseDecimal(txtCusteio5.Text).HasValue)
                        {
                            total5 = total5 + Util.TryParseDecimal(txtCusteio5.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio5.Text).Value;
                        }
                        if (Util.TryParseDecimal(txtInv5Publica.Text).HasValue)
                        {
                            total5 = total5 + Util.TryParseDecimal(txtInv5Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv5Publica.Text).Value;
                        }
                        if (Util.TryParseDecimal(txtOutroCusteio5.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio5.Text).Value;
                            total5 += Util.TryParseDecimal(txtOutroCusteio5.Text).Value;
                        }
                        lblTotalExecPublica5.Text = total5.ToString("n2");

                        if (Util.TryParseDecimal(txtCusteio6.Text).HasValue)
                        {
                            total6 = total6 + Util.TryParseDecimal(txtCusteio6.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio6.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv6Publica.Text).HasValue)
                        {
                            total6 = total6 + Util.TryParseDecimal(txtInv6Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv6Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio6.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio6.Text).Value;
                            total6 += Util.TryParseDecimal(txtOutroCusteio6.Text).Value;
                        }

                        lblTotalExecPublica6.Text = total6.ToString("n2");

                        if (Util.TryParseDecimal(txtCusteio7.Text).HasValue)
                        {
                            total7 = total7 + Util.TryParseDecimal(txtCusteio7.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio7.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv7Publica.Text).HasValue)
                        {
                            total7 = total7 + Util.TryParseDecimal(txtInv7Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv7Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio7.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio7.Text).Value;
                            total7 += Util.TryParseDecimal(txtOutroCusteio7.Text).Value;
                        }

                        lblTotalExecPublica7.Text = total7.ToString("n2");

                        if (Util.TryParseDecimal(txtCusteio8.Text).HasValue)
                        {
                            total8 = total8 + Util.TryParseDecimal(txtCusteio8.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio8.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv8Publica.Text).HasValue)
                        {
                            total8 = total8 + Util.TryParseDecimal(txtInv8Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv8Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio8.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio8.Text).Value;
                            total8 += Util.TryParseDecimal(txtOutroCusteio8.Text).Value;
                        }

                        lblTotalExecPublica8.Text = total8.ToString("n2");

                        if (Util.TryParseDecimal(txtCusteio9.Text).HasValue)
                        {
                            total9 = total9 + Util.TryParseDecimal(txtCusteio9.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio9.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv9Publica.Text).HasValue)
                        {
                            total9 = total9 + Util.TryParseDecimal(txtInv9Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv9Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio9.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio9.Text).Value;
                            total9 += Util.TryParseDecimal(txtOutroCusteio9.Text).Value;
                        }

                        lblTotalExecPublica9.Text = total9.ToString("n2");

                        if (Util.TryParseDecimal(txtCusteio10.Text).HasValue)
                        {
                            total10 = total10 + Util.TryParseDecimal(txtCusteio10.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio10.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv10Publica.Text).HasValue)
                        {
                            total10 = total10 + Util.TryParseDecimal(txtInv10Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv10Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio10.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio10.Text).Value;
                            total10 += Util.TryParseDecimal(txtOutroCusteio10.Text).Value;
                        }

                        lblTotalExecPublica10.Text = total10.ToString("n2");

                        if (Util.TryParseDecimal(txtCusteio11.Text).HasValue)
                        {
                            total11 = total11 + Util.TryParseDecimal(txtCusteio11.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio11.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv11Publica.Text).HasValue)
                        {
                            total11 = total11 + Util.TryParseDecimal(txtInv11Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv11Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio11.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio11.Text).Value;
                            total11 += Util.TryParseDecimal(txtOutroCusteio11.Text).Value;
                        }

                        lblTotalExecPublica11.Text = total11.ToString("n2");

                        if (Util.TryParseDecimal(txtCusteio12.Text).HasValue)
                        {
                            total12 = total12 + Util.TryParseDecimal(txtCusteio12.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio12.Text).Value;
                        }



                        if (Util.TryParseDecimal(txtInv12Publica.Text).HasValue)
                        {
                            total12 = total12 + Util.TryParseDecimal(txtInv12Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv12Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtOutroCusteio12.Text).HasValue)
                        {
                            totalOC += Util.TryParseDecimal(txtOutroCusteio12.Text).Value;
                            total12 += Util.TryParseDecimal(txtOutroCusteio12.Text).Value;
                        }

                        lblTotalExecPublica12.Text = total12.ToString("n2");

                        decimal totalRecursos = 0;

                        if (Util.TryParseDecimal(txtTot1Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot1Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot2Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot2Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot3Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot3Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot4Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot4Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot5Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot5Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot6Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot6Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot7Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot7Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot8Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot8Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot9Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot9Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot10Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot10Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot11Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot11Publica.Text).Value;

                        if (Util.TryParseDecimal(txtTot12Publica.Text).HasValue)
                            totalRecursos = totalRecursos + Util.TryParseDecimal(txtTot12Publica.Text).Value;


                        //Verificar regra para reprogramação de valores
                        //if (Util.TryParseDecimal(txtReprogramacaoRecursosDisponibilizados.Text).HasValue)
                        //    totalRecursos = totalRecursos + Util.TryParseDecimal(txtReprogramacaoRecursosDisponibilizados.Text).Value;

                        if (Util.TryParseDecimal(txtReprogramacaoRH.Text).HasValue)
                            totalReprogramacaoPublica = Util.TryParseDecimal(txtReprogramacaoRH.Text).Value;


                        if (Util.TryParseDecimal(txtReprogramacaoCusteio.Text).HasValue)
                            totalReprogramacaoPublica += Util.TryParseDecimal(txtReprogramacaoCusteio.Text).Value;

                        lblTotalRecursosReprogramadoAnoAnterior.Text = totalReprogramacaoPublica.ToString("n2");

                        lblTotalCusteio.Text = totalCusteio.ToString("n2");
                        lblTotalInvestimentoPublica.Text = totalInvestimento.ToString("n2");

                        lblTotalOC.Text = totalOC.ToString("n2");
                        lblTotalPublica.Text = totalRecursos.ToString("n2");


                        var ReprogramacaoPublicaTotal = Convert.ToDecimal(lblTotalRecursosReprogramadoAnoAnterior.Text);
                        totalGeral = total1 + total2 + total3 + total4 + total5 + total6 + total7 + total8 + total9 + total10 + total11 + total12;
                        lblTotalExecPublica.Text = totalGeral.ToString("n2");
                        //lblTotalCofinanciamentoPublica.Text = totalGeral.ToString("n2");                    

   
                    }
                    else
                    {
                        decimal totalPublica = 0, totalPublica1 = 0, totalPublica2 = 0, totalPublica3 = 0, totalPublica4 = 0, totalPublica5 = 0, totalPublica6 = 0, totalPublica7 = 0, totalPublica8 = 0, totalPublica9 = 0, totalPublica10 = 0, totalPublica11 = 0, totalPublica12 = 0;

                        if (Util.TryParseDecimal(txtTot1Publica.Text).HasValue)
                        {
                            totalPublica1 = totalPublica1 + Util.TryParseDecimal(txtTot1Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot1Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio1.Text).HasValue)
                        {
                            total1 = total1 + Util.TryParseDecimal(txtCusteio1.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio1.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv1Publica.Text).HasValue)
                        {
                            total1 = total1 + Util.TryParseDecimal(txtInv1Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv1Publica.Text).Value;
                        }


                        if (Util.TryParseDecimal(txtObras1.Text).HasValue)
                        {
                            total1 = total1 + Util.TryParseDecimal(txtObras1.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras1.Text).Value;
                        }



                        lblTotalExecPublica1.Text = total1.ToString("n2");
                        //lblTot1Publica.Text = total1.ToString("n2");

                        if (Util.TryParseDecimal(txtTot2Publica.Text).HasValue)
                        {
                            totalPublica2 = totalPublica2 + Util.TryParseDecimal(txtTot2Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot2Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio2.Text).HasValue)
                        {
                            total2 = total2 + Util.TryParseDecimal(txtCusteio2.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio2.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv2Publica.Text).HasValue)
                        {
                            total2 = total2 + Util.TryParseDecimal(txtInv2Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv2Publica.Text).Value;
                        }


                        if (Util.TryParseDecimal(txtObras2.Text).HasValue)
                        {
                            total2 = total2 + Util.TryParseDecimal(txtObras2.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras2.Text).Value;
                        }

                        lblTotalExecPublica2.Text = total2.ToString("n2");
                        //lblTot2Publica.Text = total2.ToString("n2");

                        if (Util.TryParseDecimal(txtTot3Publica.Text).HasValue)
                        {
                            totalPublica3 = totalPublica3 + Util.TryParseDecimal(txtTot3Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot3Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio3.Text).HasValue)
                        {
                            total3 = total3 + Util.TryParseDecimal(txtCusteio3.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio3.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv3Publica.Text).HasValue)
                        {
                            total3 = total3 + Util.TryParseDecimal(txtInv3Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv3Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtObras3.Text).HasValue)
                        {
                            total3 = total3 + Util.TryParseDecimal(txtObras3.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras3.Text).Value;
                        }
                        lblTotalExecPublica3.Text = total3.ToString("n2");
                        //lblTot3Publica.Text = total3.ToString("n2");

                        if (Util.TryParseDecimal(txtTot4Publica.Text).HasValue)
                        {
                            totalPublica4 = totalPublica4 + Util.TryParseDecimal(txtTot4Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot4Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio4.Text).HasValue)
                        {
                            total4 = total4 + Util.TryParseDecimal(txtCusteio4.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio4.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv4Publica.Text).HasValue)
                        {
                            total4 = total4 + Util.TryParseDecimal(txtInv4Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv4Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtObras4.Text).HasValue)
                        {
                            total4 = total4 + Util.TryParseDecimal(txtObras4.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras4.Text).Value;
                        }

                        lblTotalExecPublica4.Text = total4.ToString("n2");
                        //lblTot4Publica.Text = total4.ToString("n2");

                        if (Util.TryParseDecimal(txtTot5Publica.Text).HasValue)
                        {
                            totalPublica5 = totalPublica5 + Util.TryParseDecimal(txtTot5Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot5Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio5.Text).HasValue)
                        {
                            total5 = total5 + Util.TryParseDecimal(txtCusteio5.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio5.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv5Publica.Text).HasValue)
                        {
                            total5 = total5 + Util.TryParseDecimal(txtInv5Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv5Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtObras5.Text).HasValue)
                        {
                            total5 = total5 + Util.TryParseDecimal(txtObras5.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras5.Text).Value;
                        }
                        lblTotalExecPublica5.Text = total5.ToString("n2");
                        //lblTot5Publica.Text = total5.ToString("n2");

                        if (Util.TryParseDecimal(txtTot6Publica.Text).HasValue)
                        {
                            totalPublica6 = totalPublica6 + Util.TryParseDecimal(txtTot6Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot6Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio6.Text).HasValue)
                        {
                            total6 = total6 + Util.TryParseDecimal(txtCusteio6.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio6.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv6Publica.Text).HasValue)
                        {
                            total6 = total6 + Util.TryParseDecimal(txtInv6Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv6Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtObras6.Text).HasValue)
                        {
                            total6 = total6 + Util.TryParseDecimal(txtObras6.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras6.Text).Value;
                        }
                        lblTotalExecPublica6.Text = total6.ToString("n2");
                        //lblTot6Publica.Text = total6.ToString("n2");

                        if (Util.TryParseDecimal(txtTot7Publica.Text).HasValue)
                        {
                            totalPublica7 = totalPublica7 + Util.TryParseDecimal(txtTot7Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot7Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio7.Text).HasValue)
                        {
                            total7 = total7 + Util.TryParseDecimal(txtCusteio7.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio7.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv7Publica.Text).HasValue)
                        {
                            total7 = total7 + Util.TryParseDecimal(txtInv7Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv7Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtObras7.Text).HasValue)
                        {
                            total7 = total7 + Util.TryParseDecimal(txtObras7.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras7.Text).Value;
                        }
                        lblTotalExecPublica7.Text = total7.ToString("n2");
                        //lblTot7Publica.Text = total7.ToString("n2");

                        if (Util.TryParseDecimal(txtTot8Publica.Text).HasValue)
                        {
                            totalPublica8 = totalPublica8 + Util.TryParseDecimal(txtTot8Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot8Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio8.Text).HasValue)
                        {
                            total8 = total8 + Util.TryParseDecimal(txtCusteio8.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio8.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv8Publica.Text).HasValue)
                        {
                            total8 = total8 + Util.TryParseDecimal(txtInv8Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv8Publica.Text).Value;
                        }
                        if (Util.TryParseDecimal(txtObras8.Text).HasValue)
                        {
                            total8 = total8 + Util.TryParseDecimal(txtObras8.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras8.Text).Value;
                        }
                        lblTotalExecPublica8.Text = total8.ToString("n2");
                        //lblTot8Publica.Text = total8.ToString("n2");

                        if (Util.TryParseDecimal(txtTot9Publica.Text).HasValue)
                        {
                            totalPublica9 = totalPublica9 + Util.TryParseDecimal(txtTot9Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot9Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio9.Text).HasValue)
                        {
                            total9 = total9 + Util.TryParseDecimal(txtCusteio9.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio9.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv9Publica.Text).HasValue)
                        {
                            total9 = total9 + Util.TryParseDecimal(txtInv9Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv9Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtObras9.Text).HasValue)
                        {
                            total9 = total9 + Util.TryParseDecimal(txtObras9.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras9.Text).Value;
                        }
                        lblTotalExecPublica9.Text = total9.ToString("n2");
                        //lblTot9Publica.Text = total9.ToString("n2");
                        if (Util.TryParseDecimal(txtTot10Publica.Text).HasValue)
                        {
                            totalPublica10 = totalPublica10 + Util.TryParseDecimal(txtTot10Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot10Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio10.Text).HasValue)
                        {
                            total10 = total10 + Util.TryParseDecimal(txtCusteio10.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio10.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv10Publica.Text).HasValue)
                        {
                            total10 = total10 + Util.TryParseDecimal(txtInv10Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv10Publica.Text).Value;
                        }
                        if (Util.TryParseDecimal(txtObras10.Text).HasValue)
                        {
                            total10 = total10 + Util.TryParseDecimal(txtObras10.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras10.Text).Value;
                        }
                        lblTotalExecPublica10.Text = total10.ToString("n2");

                        if (Util.TryParseDecimal(txtTot11Publica.Text).HasValue)
                        {
                            totalPublica11 = totalPublica11 + Util.TryParseDecimal(txtTot11Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot11Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio11.Text).HasValue)
                        {
                            total11 = total11 + Util.TryParseDecimal(txtCusteio11.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio11.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv11Publica.Text).HasValue)
                        {
                            total11 = total11 + Util.TryParseDecimal(txtInv11Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv11Publica.Text).Value;
                        }
                        if (Util.TryParseDecimal(txtObras11.Text).HasValue)
                        {
                            total11 = total11 + Util.TryParseDecimal(txtObras11.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras11.Text).Value;
                        }
                        lblTotalExecPublica11.Text = total11.ToString("n2");

                        if (Util.TryParseDecimal(txtTot12Publica.Text).HasValue)
                        {
                            totalPublica12 = totalPublica12 + Util.TryParseDecimal(txtTot12Publica.Text).Value;
                            totalPublica = totalPublica + Util.TryParseDecimal(txtTot12Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtCusteio12.Text).HasValue)
                        {
                            total12 = total12 + Util.TryParseDecimal(txtCusteio12.Text).Value;
                            totalCusteio = totalCusteio + Util.TryParseDecimal(txtCusteio12.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtInv12Publica.Text).HasValue)
                        {
                            total12 = total12 + Util.TryParseDecimal(txtInv12Publica.Text).Value;
                            totalInvestimento = totalInvestimento + Util.TryParseDecimal(txtInv12Publica.Text).Value;
                        }

                        if (Util.TryParseDecimal(txtObras12.Text).HasValue)
                        {
                            total12 = total12 + Util.TryParseDecimal(txtObras12.Text).Value;
                            totalObras = totalObras + Util.TryParseDecimal(txtObras12.Text).Value;
                        }

                        lblTotalExecPublica12.Text = total12.ToString("n2");

                        totalGeral = total1 + total2 + total3 + total4 + total5 + total6 + total7 + total8 + total9 + total10 + total11 + total12;

                        lblTotalExecPublica.Text = totalGeral.ToString("n2");
                        lblTotalPublica.Text = totalPublica.ToString("n2");
                        lblTotalCusteio.Text = totalCusteio.ToString("n2");
                        lblTotalObraPublica.Text = totalObras.ToString("n2");
                        lblTotalInvestimentoPublica.Text = totalInvestimento.ToString("n2");

                        //lblTotalCofinanciamentoPublica.Text = totalPublica.ToString("n2");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void calcularRede()
        {
            try
            {
                decimal totalExecRede = 0,
                    totalReprogramado = 0,
                    total1 = 0,
                    total2 = 0,
                    total3 = 0,
                    total4 = 0,
                    total5 = 0,
                    total6 = 0,
                    total7 = 0,
                    total8 = 0,
                    total9 = 0,
                    total10 = 0,
                    total11 = 0,
                    total12 = 0;
                totalReprogramado = (String.IsNullOrEmpty(lblTotalRecursosReprogramadoAnoAnterior.Text)
                                                         ? 0M : Convert.ToDecimal(lblTotalRecursosReprogramadoAnoAnterior.Text))
                                + (String.IsNullOrEmpty(lblTotalRecursosReprogramadoPrivada.Text)
                                                         ? 0M : Convert.ToDecimal(lblTotalRecursosReprogramadoPrivada.Text));
                
                decimal totalDemandas = (String.IsNullOrEmpty(lblTotalDemandasParlamentares.Text)
                                                         ? 0M : Convert.ToDecimal(lblTotalDemandasParlamentares.Text))
                                + (String.IsNullOrEmpty(lblTotalDemandasParlamentaresPrivada.Text)
                                                         ? 0M : Convert.ToDecimal(lblTotalDemandasParlamentaresPrivada.Text));

                decimal totalReprogramadoDemandas = (String.IsNullOrEmpty(lblTotalReprogramadoDemandasParlamentares.Text)
                                         ? 0M : Convert.ToDecimal(lblTotalReprogramadoDemandasParlamentares.Text))
                + (String.IsNullOrEmpty(lblTotalReprogramadoDemandasParlamentaresPrivada.Text)
                                         ? 0M : Convert.ToDecimal(lblTotalReprogramadoDemandasParlamentaresPrivada.Text));


                total1 = String.IsNullOrEmpty(lblTotalExecPublica1.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica1.Text) + Convert.ToDecimal(lblTotalExecPrivada1.Text);
                total2 = String.IsNullOrEmpty(lblTotalExecPublica2.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica2.Text) + Convert.ToDecimal(lblTotalExecPrivada2.Text);
                total3 = String.IsNullOrEmpty(lblTotalExecPublica3.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica3.Text) + Convert.ToDecimal(lblTotalExecPrivada3.Text);
                total4 = String.IsNullOrEmpty(lblTotalExecPublica4.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica4.Text) + Convert.ToDecimal(lblTotalExecPrivada4.Text);
                total5 = String.IsNullOrEmpty(lblTotalExecPublica5.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica5.Text) + Convert.ToDecimal(lblTotalExecPrivada5.Text);
                total6 = String.IsNullOrEmpty(lblTotalExecPublica6.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica6.Text) + Convert.ToDecimal(lblTotalExecPrivada6.Text);
                total7 = String.IsNullOrEmpty(lblTotalExecPublica7.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica7.Text) + Convert.ToDecimal(lblTotalExecPrivada7.Text);
                total8 = String.IsNullOrEmpty(lblTotalExecPublica8.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica8.Text) + Convert.ToDecimal(lblTotalExecPrivada8.Text);
                total9 = String.IsNullOrEmpty(lblTotalExecPublica9.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica9.Text) + Convert.ToDecimal(lblTotalExecPrivada9.Text);
                total10 = String.IsNullOrEmpty(lblTotalExecPublica10.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica10.Text) + Convert.ToDecimal(lblTotalExecPrivada10.Text);
                total11 = String.IsNullOrEmpty(lblTotalExecPublica11.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica11.Text) + Convert.ToDecimal(lblTotalExecPrivada11.Text);
                total12 = String.IsNullOrEmpty(lblTotalExecPublica12.Text) ? 0M : Convert.ToDecimal(lblTotalExecPublica12.Text) + Convert.ToDecimal(lblTotalExecPrivada12.Text);



                totalExecRede = total1 + total2 + total3 + total4 + total5 + total6 + total7 + total8 + total9 + total10 + total11 + total12 + totalReprogramado;

                lblTotalRede1.Text = total1.ToString("n2");
                lblTotalRede2.Text = total2.ToString("n2");
                lblTotalRede3.Text = total3.ToString("n2");
                lblTotalRede4.Text = total4.ToString("n2");
                lblTotalRede5.Text = total5.ToString("n2");
                lblTotalRede6.Text = total6.ToString("n2");
                lblTotalRede7.Text = total7.ToString("n2");
                lblTotalRede8.Text = total8.ToString("n2");
                lblTotalRede9.Text = total9.ToString("n2");
                lblTotalRede10.Text = total10.ToString("n2");
                lblTotalRede11.Text = total11.ToString("n2");
                lblTotalRede12.Text = total12.ToString("n2");
                lblTotalReprogramacao.Text = totalReprogramado.ToString("n2");
                lblTotalDemandas.Text = totalDemandas.ToString("n2");
                lblTotalReprogramacaoDemandas.Text = totalReprogramadoDemandas.ToString("n2");

                //lblTotalExecRede.Text = totalExecRede.ToString("n2");
                //lblTotalCofinanciamentoRede.Text = totalExecRede.ToString("n2");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void calcularReprogramadoBeneficiosEventuais() 
        {
           decimal custeio,investimentos,obras,total;
           if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) == 5)
           {
            custeio = Convert.ToDecimal(txtReprogramacaoCusteio.Text);
            investimentos = Convert.ToDecimal(txtReprogramacaoInvestimento.Text);
            obras = Convert.ToDecimal(txtReprogramacaoObras.Text);

            total = custeio + investimentos + obras;

            lblTotalRecursosReprogramadoAnoAnterior.Text = total.ToString("N2");
               
           }
        }

        private void calcularReprogramadoProgramasProjetos()
        {
            decimal custeio, investimentos, obras, total;
            if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) == 4)
            {
                custeio = Convert.ToDecimal(txtReprogramacaoCusteio.Text);
                investimentos = Convert.ToDecimal(txtReprogramacaoInvestimento.Text);
                obras = Convert.ToDecimal(txtReprogramacaoObras.Text);

                total = custeio + investimentos + obras;

                lblTotalRecursosReprogramadoAnoAnterior.Text = total.ToString("N2");

            }
        }


        private void calcularDemandasReprogramadoBeneficiosEventuais()
        {
            decimal custeio, investimentos, obras, total;
            if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) == 5)
            {
                custeio = Convert.ToDecimal(txtReprogramadoDemandasCusteio.Text);
                investimentos = Convert.ToDecimal(txtReprogramadoDemandasInvestimento.Text);
                obras = Convert.ToDecimal(txtReprogramadoDemandasObras.Text);

                total = custeio + investimentos + obras;

                lblTotalReprogramadoDemandasParlamentares.Text = total.ToString("N2");
            }
        }

        private void calcularDemandasParlamentaresBeneficiosEventuais()
        {
            decimal custeio, investimentos, obras, total;
            if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"])) == 5)
            {
                custeio = Convert.ToDecimal(txtDemandasCusteio.Text);
                investimentos = Convert.ToDecimal(txtDemandasInvestimento.Text);
                obras = Convert.ToDecimal(txtDemandasObras.Text);

                total = custeio + investimentos + obras;

                lblTotalDemandasParlamentares.Text = total.ToString("N2");

            }
        }

        private void carregarLinksNavegacao()
        {
            ETipoProtecao tipoProtecao = (ETipoProtecao)Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            switch (tipoProtecao)
            {
                case ETipoProtecao.Basica:
                    aAnterior.Visible = true;
                    aAnterior.HRef = "FExecucaoFinanceira.aspx";
                    aProximo.HRef = "FCronogramaDesembolso.aspx?idTipo=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("2"));
                    break;
                case ETipoProtecao.EspecialMediaComplexidade:
                    aAnterior.HRef = "FCronogramaDesembolso.aspx?idTipo=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("1"));
                    aProximo.HRef = "FCronogramaDesembolso.aspx?idTipo=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("3"));
                    break;
                case ETipoProtecao.EspecialAltaComplexidade:
                    aProximo.Visible = true;
                    aAnterior.HRef = "FCronogramaDesembolso.aspx?idTipo=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("2"));
                    aProximo.HRef = "FCronogramaDesembolso.aspx?idTipo=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("4"));
                    break;
                case ETipoProtecao.ProgramasEProjetos:
                    aAnterior.HRef = "FCronogramaDesembolso.aspx?idTipo=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("3"));
                    aProximo.HRef = "FCronogramaDesembolso.aspx?idTipo=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("5"));
                    aProximo.Visible = true;
                    break;
                case ETipoProtecao.BeneficiosEventuais:
                    aAnterior.HRef = "FCronogramaDesembolso.aspx?idTipo=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("4"));
                    aProximo.Visible = false;
                    break;

            }
        }

        private void preencherTitulo()
        {
            ETipoProtecao tipoProtecao = (ETipoProtecao)Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            int exercicio = Convert.ToInt32(hdfAno.Value);

            switch (tipoProtecao)
            {
                case ETipoProtecao.Basica:
                    if (exercicio == FCronogramaDesembolso.Exercicios[0])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Básica em {0}", Exercicios[0].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Básica - Previsões Mensais de Desembolso para {0}", Exercicios[0].ToString());
                    }
                    if (exercicio == FCronogramaDesembolso.Exercicios[1])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Básica em {0}", Exercicios[1].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Básica - Previsões Mensais de Desembolso para {0}", Exercicios[1].ToString());
                    }

                    if (exercicio == FCronogramaDesembolso.Exercicios[2])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Básica em {0}", Exercicios[2].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Básica - Previsões Mensais de Desembolso para {0}", Exercicios[2].ToString());
                    }

                    if (exercicio == FCronogramaDesembolso.Exercicios[3])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Básica em {0}", Exercicios[3].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Básica - Previsões Mensais de Desembolso para {0}", Exercicios[3].ToString());
                    }

                    lblNumeracao.Text = "5.5.a";
                    lblNumeracao2.Text = "5.6.a ";
                    break;
                case ETipoProtecao.EspecialMediaComplexidade:
                    if (exercicio == FCronogramaDesembolso.Exercicios[0])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Especial Média Complexidade em {0}", Exercicios[0].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Especial Média Complexidade - Previsões Mensais de Desembolso para {0}", Exercicios[0].ToString());
                    }
                    if (exercicio == FCronogramaDesembolso.Exercicios[1])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Especial Média Complexidade em {0}", Exercicios[1].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Especial Média Complexidade - Previsões Mensais de Desembolso para {0}", Exercicios[1].ToString());
                    }

                    if (exercicio == FCronogramaDesembolso.Exercicios[2])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Especial Média Complexidade em {0}", Exercicios[2].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Especial Média Complexidade - Previsões Mensais de Desembolso para {0}", Exercicios[2].ToString());
                    }

                    if (exercicio == FCronogramaDesembolso.Exercicios[3])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Especial Média Complexidade em {0}", Exercicios[3].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Especial Média Complexidade - Previsões Mensais de Desembolso para {0}", Exercicios[3].ToString());
                    }
                    lblNumeracao.Text = "5.5.b";
                    lblNumeracao2.Text = "5.6.b";
                    break;
                case ETipoProtecao.EspecialAltaComplexidade:
                    if (exercicio == FCronogramaDesembolso.Exercicios[0])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Especial Alta Complexidade em {0}", Exercicios[0].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Especial Alta Complexidade - Previsões Mensais de Desembolso para {0}", Exercicios[0].ToString());
                    }
                    if (exercicio == FCronogramaDesembolso.Exercicios[1])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Especial Alta Complexidade em {0}", Exercicios[1].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Especial Alta Complexidade - Previsões Mensais de Desembolso para {0}", Exercicios[1].ToString());
                    }

                    if (exercicio == FCronogramaDesembolso.Exercicios[2])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Especial Alta Complexidade em {0}", Exercicios[2].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Especial Alta Complexidade - Previsões Mensais de Desembolso para {0}", Exercicios[2].ToString());
                    }

                    if (exercicio == FCronogramaDesembolso.Exercicios[3])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para a Rede de Proteção Social Especial Alta Complexidade em {0}", Exercicios[3].ToString());
                        lblCabecalho.Text = string.Format("Proteção Social Especial Alta Complexidade - Previsões Mensais de Desembolso para {0}", Exercicios[3].ToString());
                    }
                    lblNumeracao.Text = "5.5.c";
                    lblNumeracao2.Text = "5.6.c";
                    break;
                case ETipoProtecao.ProgramasEProjetos:

                    lblTitulo.Text = "Cofinanciamento Estadual para Programa";
                    lblCabecalho.Text = " Cofinanciamento Estadual para Programa";

                    if (exercicio == FCronogramaDesembolso.Exercicios[0])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para Programa em {0}", Exercicios[0].ToString());
                        lblCabecalho.Text = string.Format("Cofinanciamento Estadual para Programa em {0}", Exercicios[0].ToString());
                    }
                    if (exercicio == FCronogramaDesembolso.Exercicios[1])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para Programa em {0}", Exercicios[1].ToString());
                        lblCabecalho.Text = string.Format("Cofinanciamento Estadual para Programa em {0}", Exercicios[1].ToString());
                    }

                    if (exercicio == FCronogramaDesembolso.Exercicios[2])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para Programa em {0}", Exercicios[2].ToString());
                        lblCabecalho.Text = string.Format("Cofinanciamento Estadual para Programa em {0}", Exercicios[2].ToString());
                    }

                    if (exercicio == FCronogramaDesembolso.Exercicios[3])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para Programa em {0}", Exercicios[3].ToString());
                        lblCabecalho.Text = string.Format("Cofinanciamento Estadual para Programa em {0}", Exercicios[3].ToString());
                    }

                    lblNumeracao.Text = "5.5.d";
                    lblNumeracao2.Text = "5.6.d";
                    break;
                case ETipoProtecao.BeneficiosEventuais:

                    if (exercicio == FCronogramaDesembolso.Exercicios[0])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para Beneficio em {0}", Exercicios[0].ToString());
                        lblCabecalho.Text = string.Format("Cofinanciamento Estadual para Beneficio em {0}", Exercicios[0].ToString());
                    }
                    if (exercicio == FCronogramaDesembolso.Exercicios[1])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para Beneficio em {0}", Exercicios[1].ToString());
                        lblCabecalho.Text = string.Format("Cofinanciamento Estadual para Beneficio em {0}", Exercicios[1].ToString());
                    }
                    if (exercicio == FCronogramaDesembolso.Exercicios[2])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para Beneficio em {0}", Exercicios[2].ToString());
                        lblCabecalho.Text = string.Format("Cofinanciamento Estadual para Beneficio em {0}", Exercicios[2].ToString());
                    }
                    if (exercicio == FCronogramaDesembolso.Exercicios[3])
                    {
                        lblTitulo.Text = string.Format("Cofinanciamento Estadual para Beneficio em {0}", Exercicios[3].ToString());
                        lblCabecalho.Text = string.Format("Cofinanciamento Estadual para Beneficio em {0}", Exercicios[3].ToString());
                    }

                    lblNumeracao.Text = "5.5.e";
                    lblNumeracao2.Text = "5.6.e";

                    break;

                default:
                    break;
            }
        }

        private CronogramaDesembolsoInfo PreencherCronogramaPrivada()
        {
            var cronogramaPrivada = new CronogramaDesembolsoInfo();
            cronogramaPrivada.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            cronogramaPrivada.IdTipoProtecaoSocial = Convert.ToInt16(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            cronogramaPrivada.IdTipoUnidade = 2;
            cronogramaPrivada.Exercicio = Convert.ToInt32(hdfAno.Value);


            if (cronogramaPrivada.IdTipoProtecaoSocial == 1 || cronogramaPrivada.IdTipoProtecaoSocial == 2 || cronogramaPrivada.IdTipoProtecaoSocial == 3)
            {
                cronogramaPrivada.ValorServicosTerceirosMes1 = Convert.ToDecimal(txtTot1.Text);
                cronogramaPrivada.ValorServicosTerceirosMes2 = Convert.ToDecimal(txtTot2.Text);
                cronogramaPrivada.ValorServicosTerceirosMes3 = Convert.ToDecimal(txtTot3.Text);
                cronogramaPrivada.ValorServicosTerceirosMes4 = Convert.ToDecimal(txtTot4.Text);
                cronogramaPrivada.ValorServicosTerceirosMes5 = Convert.ToDecimal(txtTot5.Text);
                cronogramaPrivada.ValorServicosTerceirosMes6 = Convert.ToDecimal(txtTot6.Text);
                cronogramaPrivada.ValorServicosTerceirosMes7 = Convert.ToDecimal(txtTot7.Text);
                cronogramaPrivada.ValorServicosTerceirosMes8 = Convert.ToDecimal(txtTot8.Text);
                cronogramaPrivada.ValorServicosTerceirosMes9 = Convert.ToDecimal(txtTot9.Text);
                cronogramaPrivada.ValorServicosTerceirosMes10 = Convert.ToDecimal(txtTot10.Text);
                cronogramaPrivada.ValorServicosTerceirosMes11 = Convert.ToDecimal(txtTot11.Text);
                cronogramaPrivada.ValorServicosTerceirosMes12 = Convert.ToDecimal(txtTot12.Text);
            }


            if (!String.IsNullOrEmpty(txtST1.Text))
                cronogramaPrivada.ValorServicosTerceirosMes1 = Convert.ToDecimal(txtTot1.Text);
            if (!String.IsNullOrEmpty(txtST2.Text))
                cronogramaPrivada.ValorServicosTerceirosMes2 = Convert.ToDecimal(txtTot2.Text);
            if (!String.IsNullOrEmpty(txtST3.Text))
                cronogramaPrivada.ValorServicosTerceirosMes3 = Convert.ToDecimal(txtTot3.Text);
            if (!String.IsNullOrEmpty(txtST4.Text))
                cronogramaPrivada.ValorServicosTerceirosMes4 = Convert.ToDecimal(txtTot4.Text);
            if (!String.IsNullOrEmpty(txtST5.Text))
                cronogramaPrivada.ValorServicosTerceirosMes5 = Convert.ToDecimal(txtTot5.Text);
            if (!String.IsNullOrEmpty(txtST6.Text))
                cronogramaPrivada.ValorServicosTerceirosMes6 = Convert.ToDecimal(txtTot6.Text);
            if (!String.IsNullOrEmpty(txtST7.Text))
                cronogramaPrivada.ValorServicosTerceirosMes7 = Convert.ToDecimal(txtTot7.Text);
            if (!String.IsNullOrEmpty(txtST8.Text))
                cronogramaPrivada.ValorServicosTerceirosMes8 = Convert.ToDecimal(txtTot8.Text);
            if (!String.IsNullOrEmpty(txtST9.Text))
                cronogramaPrivada.ValorServicosTerceirosMes9 = Convert.ToDecimal(txtTot9.Text);
            if (!String.IsNullOrEmpty(txtST10.Text))
                cronogramaPrivada.ValorServicosTerceirosMes10 = Convert.ToDecimal(txtTot10.Text);
            if (!String.IsNullOrEmpty(txtST11.Text))
                cronogramaPrivada.ValorServicosTerceirosMes11 = Convert.ToDecimal(txtTot11.Text);
            if (!String.IsNullOrEmpty(txtST12.Text))
                cronogramaPrivada.ValorServicosTerceirosMes12 = Convert.ToDecimal(txtTot12.Text);
            //}


            if (!String.IsNullOrEmpty(txtRH1.Text))
                cronogramaPrivada.ValorRHMes1 = Convert.ToDecimal(txtRH1.Text);
            if (!String.IsNullOrEmpty(txtRH2.Text))
                cronogramaPrivada.ValorRHMes2 = Convert.ToDecimal(txtRH2.Text);
            if (!String.IsNullOrEmpty(txtRH3.Text))
                cronogramaPrivada.ValorRHMes3 = Convert.ToDecimal(txtRH3.Text);
            if (!String.IsNullOrEmpty(txtRH4.Text))
                cronogramaPrivada.ValorRHMes4 = Convert.ToDecimal(txtRH4.Text);
            if (!String.IsNullOrEmpty(txtRH5.Text))
                cronogramaPrivada.ValorRHMes5 = Convert.ToDecimal(txtRH5.Text);
            if (!String.IsNullOrEmpty(txtRH6.Text))
                cronogramaPrivada.ValorRHMes6 = Convert.ToDecimal(txtRH6.Text);
            if (!String.IsNullOrEmpty(txtRH7.Text))
                cronogramaPrivada.ValorRHMes7 = Convert.ToDecimal(txtRH7.Text);
            if (!String.IsNullOrEmpty(txtRH8.Text))
                cronogramaPrivada.ValorRHMes8 = Convert.ToDecimal(txtRH8.Text);
            if (!String.IsNullOrEmpty(txtRH9.Text))
                cronogramaPrivada.ValorRHMes9 = Convert.ToDecimal(txtRH9.Text);
            if (!String.IsNullOrEmpty(txtRH10.Text))
                cronogramaPrivada.ValorRHMes10 = Convert.ToDecimal(txtRH10.Text);
            if (!String.IsNullOrEmpty(txtRH11.Text))
                cronogramaPrivada.ValorRHMes11 = Convert.ToDecimal(txtRH11.Text);
            if (!String.IsNullOrEmpty(txtRH12.Text))
                cronogramaPrivada.ValorRHMes12 = Convert.ToDecimal(txtRH12.Text);

            if (!String.IsNullOrEmpty(txtMC1.Text))
                cronogramaPrivada.ValorMaterialConsumoMes1 = Convert.ToDecimal(txtMC1.Text);
            if (!String.IsNullOrEmpty(txtMC2.Text))
                cronogramaPrivada.ValorMaterialConsumoMes2 = Convert.ToDecimal(txtMC2.Text);
            if (!String.IsNullOrEmpty(txtMC3.Text))
                cronogramaPrivada.ValorMaterialConsumoMes3 = Convert.ToDecimal(txtMC3.Text);
            if (!String.IsNullOrEmpty(txtMC4.Text))
                cronogramaPrivada.ValorMaterialConsumoMes4 = Convert.ToDecimal(txtMC4.Text);
            if (!String.IsNullOrEmpty(txtMC5.Text))
                cronogramaPrivada.ValorMaterialConsumoMes5 = Convert.ToDecimal(txtMC5.Text);
            if (!String.IsNullOrEmpty(txtMC6.Text))
                cronogramaPrivada.ValorMaterialConsumoMes6 = Convert.ToDecimal(txtMC6.Text);
            if (!String.IsNullOrEmpty(txtMC7.Text))
                cronogramaPrivada.ValorMaterialConsumoMes7 = Convert.ToDecimal(txtMC7.Text);
            if (!String.IsNullOrEmpty(txtMC8.Text))
                cronogramaPrivada.ValorMaterialConsumoMes8 = Convert.ToDecimal(txtMC8.Text);
            if (!String.IsNullOrEmpty(txtMC9.Text))
                cronogramaPrivada.ValorMaterialConsumoMes9 = Convert.ToDecimal(txtMC9.Text);
            if (!String.IsNullOrEmpty(txtMC10.Text))
                cronogramaPrivada.ValorMaterialConsumoMes10 = Convert.ToDecimal(txtMC10.Text);
            if (!String.IsNullOrEmpty(txtMC11.Text))
                cronogramaPrivada.ValorMaterialConsumoMes11 = Convert.ToDecimal(txtMC11.Text);
            if (!String.IsNullOrEmpty(txtMC12.Text))
                cronogramaPrivada.ValorMaterialConsumoMes12 = Convert.ToDecimal(txtMC12.Text);

            if (!String.IsNullOrEmpty(txtST1.Text))
                cronogramaPrivada.ValorInvestimentoMes1 = Convert.ToDecimal(txtST1.Text);
            if (!String.IsNullOrEmpty(txtST2.Text))
                cronogramaPrivada.ValorInvestimentoMes2 = Convert.ToDecimal(txtST2.Text);
            if (!String.IsNullOrEmpty(txtST3.Text))
                cronogramaPrivada.ValorInvestimentoMes3 = Convert.ToDecimal(txtST3.Text);
            if (!String.IsNullOrEmpty(txtST4.Text))
                cronogramaPrivada.ValorInvestimentoMes4 = Convert.ToDecimal(txtST4.Text);
            if (!String.IsNullOrEmpty(txtST5.Text))
                cronogramaPrivada.ValorInvestimentoMes5 = Convert.ToDecimal(txtST5.Text);
            if (!String.IsNullOrEmpty(txtST6.Text))
                cronogramaPrivada.ValorInvestimentoMes6 = Convert.ToDecimal(txtST6.Text);
            if (!String.IsNullOrEmpty(txtST7.Text))
                cronogramaPrivada.ValorInvestimentoMes7 = Convert.ToDecimal(txtST7.Text);
            if (!String.IsNullOrEmpty(txtST8.Text))
                cronogramaPrivada.ValorInvestimentoMes8 = Convert.ToDecimal(txtST8.Text);
            if (!String.IsNullOrEmpty(txtST9.Text))
                cronogramaPrivada.ValorInvestimentoMes9 = Convert.ToDecimal(txtST9.Text);
            if (!String.IsNullOrEmpty(txtST10.Text))
                cronogramaPrivada.ValorInvestimentoMes10 = Convert.ToDecimal(txtST10.Text);
            if (!String.IsNullOrEmpty(txtST11.Text))
                cronogramaPrivada.ValorInvestimentoMes11 = Convert.ToDecimal(txtST11.Text);
            if (!String.IsNullOrEmpty(txtST12.Text))
                cronogramaPrivada.ValorInvestimentoMes12 = Convert.ToDecimal(txtST12.Text);



            if (!String.IsNullOrEmpty(txtObrasPrivada1.Text))
                cronogramaPrivada.ObrasMes1 = Convert.ToDecimal(txtObrasPrivada1.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada2.Text))
                cronogramaPrivada.ObrasMes2 = Convert.ToDecimal(txtObrasPrivada2.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada3.Text))
                cronogramaPrivada.ObrasMes3 = Convert.ToDecimal(txtObrasPrivada3.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada4.Text))
                cronogramaPrivada.ObrasMes4 = Convert.ToDecimal(txtObrasPrivada4.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada5.Text))
                cronogramaPrivada.ObrasMes5 = Convert.ToDecimal(txtObrasPrivada5.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada6.Text))
                cronogramaPrivada.ObrasMes6 = Convert.ToDecimal(txtObrasPrivada6.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada7.Text))
                cronogramaPrivada.ObrasMes7 = Convert.ToDecimal(txtObrasPrivada7.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada8.Text))
                cronogramaPrivada.ObrasMes8 = Convert.ToDecimal(txtObrasPrivada8.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada9.Text))
                cronogramaPrivada.ObrasMes9 = Convert.ToDecimal(txtObrasPrivada9.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada10.Text))
                cronogramaPrivada.ObrasMes10 = Convert.ToDecimal(txtObrasPrivada10.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada11.Text))
                cronogramaPrivada.ObrasMes11 = Convert.ToDecimal(txtObrasPrivada11.Text);
            if (!String.IsNullOrEmpty(txtObrasPrivada12.Text))
                cronogramaPrivada.ObrasMes12 = Convert.ToDecimal(txtObrasPrivada12.Text);


            if (!String.IsNullOrEmpty(txtRecursosReprogramadoPrivado.Text))
                cronogramaPrivada.ReprogramacaoRecursosDisponibilizados = Convert.ToDecimal(txtRecursosReprogramadoPrivado.Text);

            if (!String.IsNullOrEmpty(txtRecursosHumanosReprogramadoPrivado.Text))
                cronogramaPrivada.RecursosHumanosReprogramados = Convert.ToDecimal(txtRecursosHumanosReprogramadoPrivado.Text);

            if (!String.IsNullOrEmpty(txtOutrasCusteioReprogramadoPrivado.Text))
                cronogramaPrivada.OutrasDespesasReprogramados = Convert.ToDecimal(txtOutrasCusteioReprogramadoPrivado.Text);

            if (!String.IsNullOrEmpty(txtEquipamentosPrivadoReprogramado.Text))
                cronogramaPrivada.ReprogramacaoEquipamentosInvestimento = Convert.ToDecimal(txtEquipamentosPrivadoReprogramado.Text);

            if (!String.IsNullOrEmpty(txtObrasReprogramadoPrivado.Text))
                cronogramaPrivada.ReprogramacaoObras = Convert.ToDecimal(txtObrasReprogramadoPrivado.Text);

            if (!String.IsNullOrEmpty(txtDemandasParlamentaresPrivado.Text))
                cronogramaPrivada.DemandasParlamentaresDisponibilizados = Convert.ToDecimal(txtDemandasParlamentaresPrivado.Text);

            if (!String.IsNullOrEmpty(txtRecursosHumanosDemandasPrivado.Text))
                cronogramaPrivada.RecursosHumanosDemandasParlamentares = Convert.ToDecimal(txtRecursosHumanosDemandasPrivado.Text);

            if (!String.IsNullOrEmpty(txtOutrasCusteioDemandasPrivado.Text))
                cronogramaPrivada.OutrasDespesasDemandasParlamentares = Convert.ToDecimal(txtOutrasCusteioDemandasPrivado.Text);

            if (!String.IsNullOrEmpty(txtEquipamentosPrivadoDemandas.Text))
                cronogramaPrivada.DemandasParlamentaresEquipamentosInvestimento = Convert.ToDecimal(txtEquipamentosPrivadoDemandas.Text);

            if (!String.IsNullOrEmpty(txtObrasDemandasPrivado.Text))
                cronogramaPrivada.DemandasParlamentaresObras = Convert.ToDecimal(txtObrasDemandasPrivado.Text);



            if (!String.IsNullOrEmpty(txtReprogramadoDemandasParlamentaresPrivado.Text))
                cronogramaPrivada.ReprogramacaoDemandasParlamentaresDisponibilizados = Convert.ToDecimal(txtReprogramadoDemandasParlamentaresPrivado.Text);

            if (!String.IsNullOrEmpty(txtRecursosHumanosReprogramadoDemandasPrivado.Text))
                cronogramaPrivada.RecursosHumanosReprogramacaoDemandasParlamentares = Convert.ToDecimal(txtRecursosHumanosReprogramadoDemandasPrivado.Text);

            if (!String.IsNullOrEmpty(txtOutrasCusteioReprogramadoDemandasPrivado.Text))
                cronogramaPrivada.OutrasDespesasReprogramacaoDemandasParlamentares = Convert.ToDecimal(txtOutrasCusteioReprogramadoDemandasPrivado.Text);

            if (!String.IsNullOrEmpty(txtEquipamentosPrivadoReprogramadoDemandas.Text))
                cronogramaPrivada.ReprogramacaoDemandasParlamentaresEquipamentosInvestimento = Convert.ToDecimal(txtEquipamentosPrivadoReprogramadoDemandas.Text);

            if (!String.IsNullOrEmpty(txtObrasReprogramadoDemandasPrivado.Text))
                cronogramaPrivada.ReprogramacaoDemandasParlamentaresObras = Convert.ToDecimal(txtObrasReprogramadoDemandasPrivado.Text);

            return cronogramaPrivada;
        }

        private CronogramaDesembolsoInfo PreencherCronogramaPublica()
        {
            var cronogramaPublica = new CronogramaDesembolsoInfo();
            cronogramaPublica.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            cronogramaPublica.IdTipoUnidade = 1;
            cronogramaPublica.IdTipoProtecaoSocial = Convert.ToInt16(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            cronogramaPublica.Exercicio = Convert.ToInt32(hdfAno.Value);

            if (!String.IsNullOrEmpty(txtReprogramacaoRecursosDisponibilizados.Text))
                cronogramaPublica.ReprogramacaoRecursosDisponibilizados = Convert.ToDecimal(txtReprogramacaoRecursosDisponibilizados.Text);

            if (!String.IsNullOrEmpty(txtReprogramacaoRH.Text))
                cronogramaPublica.RecursosHumanosReprogramados = Convert.ToDecimal(txtReprogramacaoRH.Text);

            if (!String.IsNullOrEmpty(txtReprogramacaoCusteio.Text))
                cronogramaPublica.OutrasDespesasReprogramados = Convert.ToDecimal(txtReprogramacaoCusteio.Text);

            if (!String.IsNullOrEmpty(txtReprogramacaoInvestimento.Text))
                cronogramaPublica.ReprogramacaoEquipamentosInvestimento = Convert.ToDecimal(txtReprogramacaoInvestimento.Text);

            if (!String.IsNullOrEmpty(txtReprogramacaoObras.Text))
                cronogramaPublica.ReprogramacaoObras = Convert.ToDecimal(txtReprogramacaoObras.Text);

            if (!String.IsNullOrEmpty(txtDemandasParlamentaresDisponibilizados.Text))
                cronogramaPublica.DemandasParlamentaresDisponibilizados = Convert.ToDecimal(txtDemandasParlamentaresDisponibilizados.Text);

            if (!String.IsNullOrEmpty(txtDemandasRH.Text))
                cronogramaPublica.RecursosHumanosDemandasParlamentares = Convert.ToDecimal(txtDemandasRH.Text);

            if (!String.IsNullOrEmpty(txtDemandasCusteio.Text))
                cronogramaPublica.OutrasDespesasDemandasParlamentares = Convert.ToDecimal(txtDemandasCusteio.Text);

            if (!String.IsNullOrEmpty(txtDemandasInvestimento.Text))
                cronogramaPublica.DemandasParlamentaresEquipamentosInvestimento = Convert.ToDecimal(txtDemandasInvestimento.Text);

            if (!String.IsNullOrEmpty(txtDemandasObras.Text))
                cronogramaPublica.DemandasParlamentaresObras = Convert.ToDecimal(txtDemandasObras.Text);

           
            
            if (!String.IsNullOrEmpty(txtReprogramadoDemandasParlamentaresDisponibilizados.Text))
                cronogramaPublica.ReprogramacaoDemandasParlamentaresDisponibilizados = Convert.ToDecimal(txtReprogramadoDemandasParlamentaresDisponibilizados.Text);

            if (!String.IsNullOrEmpty(txtReprogramadoDemandasRH.Text))
                cronogramaPublica.RecursosHumanosReprogramacaoDemandasParlamentares = Convert.ToDecimal(txtReprogramadoDemandasRH.Text);

            if (!String.IsNullOrEmpty(txtReprogramadoDemandasCusteio.Text))
                cronogramaPublica.OutrasDespesasReprogramacaoDemandasParlamentares = Convert.ToDecimal(txtReprogramadoDemandasCusteio.Text);

            if (!String.IsNullOrEmpty(txtReprogramadoDemandasInvestimento.Text))
                cronogramaPublica.ReprogramacaoDemandasParlamentaresEquipamentosInvestimento = Convert.ToDecimal(txtReprogramadoDemandasInvestimento.Text);

            if (!String.IsNullOrEmpty(txtReprogramadoDemandasObras.Text))
                cronogramaPublica.ReprogramacaoDemandasParlamentaresObras = Convert.ToDecimal(txtReprogramadoDemandasObras.Text);


            if (!String.IsNullOrEmpty(txtCusteio1.Text))
                cronogramaPublica.ValorMaterialConsumoMes1 = Convert.ToDecimal(txtCusteio1.Text);
            if (!String.IsNullOrEmpty(txtCusteio2.Text))
                cronogramaPublica.ValorMaterialConsumoMes2 = Convert.ToDecimal(txtCusteio2.Text);
            if (!String.IsNullOrEmpty(txtCusteio3.Text))
                cronogramaPublica.ValorMaterialConsumoMes3 = Convert.ToDecimal(txtCusteio3.Text);
            if (!String.IsNullOrEmpty(txtCusteio4.Text))
                cronogramaPublica.ValorMaterialConsumoMes4 = Convert.ToDecimal(txtCusteio4.Text);
            if (!String.IsNullOrEmpty(txtCusteio5.Text))
                cronogramaPublica.ValorMaterialConsumoMes5 = Convert.ToDecimal(txtCusteio5.Text);
            if (!String.IsNullOrEmpty(txtCusteio6.Text))
                cronogramaPublica.ValorMaterialConsumoMes6 = Convert.ToDecimal(txtCusteio6.Text);
            if (!String.IsNullOrEmpty(txtCusteio7.Text))
                cronogramaPublica.ValorMaterialConsumoMes7 = Convert.ToDecimal(txtCusteio7.Text);
            if (!String.IsNullOrEmpty(txtCusteio8.Text))
                cronogramaPublica.ValorMaterialConsumoMes8 = Convert.ToDecimal(txtCusteio8.Text);
            if (!String.IsNullOrEmpty(txtCusteio9.Text))
                cronogramaPublica.ValorMaterialConsumoMes9 = Convert.ToDecimal(txtCusteio9.Text);
            if (!String.IsNullOrEmpty(txtCusteio10.Text))
                cronogramaPublica.ValorMaterialConsumoMes10 = Convert.ToDecimal(txtCusteio10.Text);
            if (!String.IsNullOrEmpty(txtCusteio11.Text))
                cronogramaPublica.ValorMaterialConsumoMes11 = Convert.ToDecimal(txtCusteio11.Text);
            if (!String.IsNullOrEmpty(txtCusteio12.Text))
                cronogramaPublica.ValorMaterialConsumoMes12 = Convert.ToDecimal(txtCusteio12.Text);


            if (cronogramaPublica.IdTipoProtecaoSocial < 4)
            {
                if (!String.IsNullOrEmpty(txtOutroCusteio1.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes01 = Convert.ToDecimal(txtOutroCusteio1.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio2.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes02 = Convert.ToDecimal(txtOutroCusteio2.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio3.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes03 = Convert.ToDecimal(txtOutroCusteio3.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio4.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes04 = Convert.ToDecimal(txtOutroCusteio4.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio5.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes05 = Convert.ToDecimal(txtOutroCusteio5.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio6.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes06 = Convert.ToDecimal(txtOutroCusteio6.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio7.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes07 = Convert.ToDecimal(txtOutroCusteio7.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio8.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes08 = Convert.ToDecimal(txtOutroCusteio8.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio9.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes09 = Convert.ToDecimal(txtOutroCusteio9.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio10.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes10 = Convert.ToDecimal(txtOutroCusteio10.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio11.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes11 = Convert.ToDecimal(txtOutroCusteio11.Text);
                if (!String.IsNullOrEmpty(txtOutroCusteio12.Text))
                    cronogramaPublica.ValorOutrasDespesasCusteioMes12 = Convert.ToDecimal(txtOutroCusteio12.Text);
            }


            if (cronogramaPublica.IdTipoProtecaoSocial == 1)
            {
                cronogramaPublica.ValorInvestimentoMes1 = string.IsNullOrEmpty(txtInv1Publica.Text) ? 0M : Convert.ToDecimal(txtInv1Publica.Text);
                cronogramaPublica.ValorInvestimentoMes2 = string.IsNullOrEmpty(txtInv2Publica.Text) ? 0M : Convert.ToDecimal(txtInv2Publica.Text);
                cronogramaPublica.ValorInvestimentoMes3 = string.IsNullOrEmpty(txtInv3Publica.Text) ? 0M : Convert.ToDecimal(txtInv3Publica.Text);
                cronogramaPublica.ValorInvestimentoMes4 = string.IsNullOrEmpty(txtInv4Publica.Text) ? 0M : Convert.ToDecimal(txtInv4Publica.Text);
                cronogramaPublica.ValorInvestimentoMes5 = string.IsNullOrEmpty(txtInv5Publica.Text) ? 0M : Convert.ToDecimal(txtInv5Publica.Text);
                cronogramaPublica.ValorInvestimentoMes6 = string.IsNullOrEmpty(txtInv6Publica.Text) ? 0M : Convert.ToDecimal(txtInv6Publica.Text);
                cronogramaPublica.ValorInvestimentoMes7 = string.IsNullOrEmpty(txtInv7Publica.Text) ? 0M : Convert.ToDecimal(txtInv7Publica.Text);
                cronogramaPublica.ValorInvestimentoMes8 = string.IsNullOrEmpty(txtInv8Publica.Text) ? 0M : Convert.ToDecimal(txtInv8Publica.Text);
                cronogramaPublica.ValorInvestimentoMes9 = string.IsNullOrEmpty(txtInv9Publica.Text) ? 0M : Convert.ToDecimal(txtInv9Publica.Text);
                cronogramaPublica.ValorInvestimentoMes10 = string.IsNullOrEmpty(txtInv10Publica.Text) ? 0M : Convert.ToDecimal(txtInv10Publica.Text);
                cronogramaPublica.ValorInvestimentoMes11 = string.IsNullOrEmpty(txtInv11Publica.Text) ? 0M : Convert.ToDecimal(txtInv11Publica.Text);
                cronogramaPublica.ValorInvestimentoMes12 = string.IsNullOrEmpty(txtInv12Publica.Text) ? 0M : Convert.ToDecimal(txtInv12Publica.Text);

                cronogramaPublica.ObrasMes1 = string.IsNullOrEmpty(txtObras1.Text) ? 0M : Convert.ToDecimal(txtObras1.Text);
                cronogramaPublica.ObrasMes2 = string.IsNullOrEmpty(txtObras2.Text) ? 0M : Convert.ToDecimal(txtObras2.Text);
                cronogramaPublica.ObrasMes3 = string.IsNullOrEmpty(txtObras3.Text) ? 0M : Convert.ToDecimal(txtObras3.Text);
                cronogramaPublica.ObrasMes4 = string.IsNullOrEmpty(txtObras4.Text) ? 0M : Convert.ToDecimal(txtObras4.Text);
                cronogramaPublica.ObrasMes5 = string.IsNullOrEmpty(txtObras5.Text) ? 0M : Convert.ToDecimal(txtObras5.Text);
                cronogramaPublica.ObrasMes6 = string.IsNullOrEmpty(txtObras6.Text) ? 0M : Convert.ToDecimal(txtObras6.Text);
                cronogramaPublica.ObrasMes7 = string.IsNullOrEmpty(txtObras7.Text) ? 0M : Convert.ToDecimal(txtObras7.Text);
                cronogramaPublica.ObrasMes8 = string.IsNullOrEmpty(txtObras8.Text) ? 0M : Convert.ToDecimal(txtObras8.Text);
                cronogramaPublica.ObrasMes9 = string.IsNullOrEmpty(txtObras9.Text) ? 0M : Convert.ToDecimal(txtObras9.Text);
                cronogramaPublica.ObrasMes10 = string.IsNullOrEmpty(txtObras10.Text) ? 0M : Convert.ToDecimal(txtObras10.Text);
                cronogramaPublica.ObrasMes11 = string.IsNullOrEmpty(txtObras11.Text) ? 0M : Convert.ToDecimal(txtObras11.Text);
                cronogramaPublica.ObrasMes12 = string.IsNullOrEmpty(txtObras12.Text) ? 0M : Convert.ToDecimal(txtObras12.Text);

                cronogramaPublica.ValorServicosTerceirosMes1 = string.IsNullOrEmpty(txtTot1Publica.Text) ? 0M : Convert.ToDecimal(txtTot1Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes2 = string.IsNullOrEmpty(txtTot2Publica.Text) ? 0M : Convert.ToDecimal(txtTot2Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes3 = string.IsNullOrEmpty(txtTot3Publica.Text) ? 0M : Convert.ToDecimal(txtTot3Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes4 = string.IsNullOrEmpty(txtTot4Publica.Text) ? 0M : Convert.ToDecimal(txtTot4Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes5 = string.IsNullOrEmpty(txtTot5Publica.Text) ? 0M : Convert.ToDecimal(txtTot5Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes6 = string.IsNullOrEmpty(txtTot6Publica.Text) ? 0M : Convert.ToDecimal(txtTot6Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes7 = string.IsNullOrEmpty(txtTot7Publica.Text) ? 0M : Convert.ToDecimal(txtTot7Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes8 = string.IsNullOrEmpty(txtTot8Publica.Text) ? 0M : Convert.ToDecimal(txtTot8Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes9 = string.IsNullOrEmpty(txtTot9Publica.Text) ? 0M : Convert.ToDecimal(txtTot9Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes10 = string.IsNullOrEmpty(txtTot10Publica.Text) ? 0M : Convert.ToDecimal(txtTot10Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes11 = string.IsNullOrEmpty(txtTot11Publica.Text) ? 0M : Convert.ToDecimal(txtTot11Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes12 = string.IsNullOrEmpty(txtTot12Publica.Text) ? 0M : Convert.ToDecimal(txtTot12Publica.Text);


            }

            if (cronogramaPublica.IdTipoProtecaoSocial == 2)
            {
                cronogramaPublica.ValorInvestimentoMes1 = string.IsNullOrEmpty(txtInv1Publica.Text) ? 0M : Convert.ToDecimal(txtInv1Publica.Text);
                cronogramaPublica.ValorInvestimentoMes2 = string.IsNullOrEmpty(txtInv2Publica.Text) ? 0M : Convert.ToDecimal(txtInv2Publica.Text);
                cronogramaPublica.ValorInvestimentoMes3 = string.IsNullOrEmpty(txtInv3Publica.Text) ? 0M : Convert.ToDecimal(txtInv3Publica.Text);
                cronogramaPublica.ValorInvestimentoMes4 = string.IsNullOrEmpty(txtInv4Publica.Text) ? 0M : Convert.ToDecimal(txtInv4Publica.Text);
                cronogramaPublica.ValorInvestimentoMes5 = string.IsNullOrEmpty(txtInv5Publica.Text) ? 0M : Convert.ToDecimal(txtInv5Publica.Text);
                cronogramaPublica.ValorInvestimentoMes6 = string.IsNullOrEmpty(txtInv6Publica.Text) ? 0M : Convert.ToDecimal(txtInv6Publica.Text);
                cronogramaPublica.ValorInvestimentoMes7 = string.IsNullOrEmpty(txtInv7Publica.Text) ? 0M : Convert.ToDecimal(txtInv7Publica.Text);
                cronogramaPublica.ValorInvestimentoMes8 = string.IsNullOrEmpty(txtInv8Publica.Text) ? 0M : Convert.ToDecimal(txtInv8Publica.Text);
                cronogramaPublica.ValorInvestimentoMes9 = string.IsNullOrEmpty(txtInv9Publica.Text) ? 0M : Convert.ToDecimal(txtInv9Publica.Text);
                cronogramaPublica.ValorInvestimentoMes10 = string.IsNullOrEmpty(txtInv10Publica.Text) ? 0M : Convert.ToDecimal(txtInv10Publica.Text);
                cronogramaPublica.ValorInvestimentoMes11 = string.IsNullOrEmpty(txtInv11Publica.Text) ? 0M : Convert.ToDecimal(txtInv11Publica.Text);
                cronogramaPublica.ValorInvestimentoMes12 = string.IsNullOrEmpty(txtInv12Publica.Text) ? 0M : Convert.ToDecimal(txtInv12Publica.Text);

                cronogramaPublica.ObrasMes1 = string.IsNullOrEmpty(txtObras1.Text) ? 0M : Convert.ToDecimal(txtObras1.Text);
                cronogramaPublica.ObrasMes2 = string.IsNullOrEmpty(txtObras2.Text) ? 0M : Convert.ToDecimal(txtObras2.Text);
                cronogramaPublica.ObrasMes3 = string.IsNullOrEmpty(txtObras3.Text) ? 0M : Convert.ToDecimal(txtObras3.Text);
                cronogramaPublica.ObrasMes4 = string.IsNullOrEmpty(txtObras4.Text) ? 0M : Convert.ToDecimal(txtObras4.Text);
                cronogramaPublica.ObrasMes5 = string.IsNullOrEmpty(txtObras5.Text) ? 0M : Convert.ToDecimal(txtObras5.Text);
                cronogramaPublica.ObrasMes6 = string.IsNullOrEmpty(txtObras6.Text) ? 0M : Convert.ToDecimal(txtObras6.Text);
                cronogramaPublica.ObrasMes7 = string.IsNullOrEmpty(txtObras7.Text) ? 0M : Convert.ToDecimal(txtObras7.Text);
                cronogramaPublica.ObrasMes8 = string.IsNullOrEmpty(txtObras8.Text) ? 0M : Convert.ToDecimal(txtObras8.Text);
                cronogramaPublica.ObrasMes9 = string.IsNullOrEmpty(txtObras9.Text) ? 0M : Convert.ToDecimal(txtObras9.Text);
                cronogramaPublica.ObrasMes10 = string.IsNullOrEmpty(txtObras10.Text) ? 0M : Convert.ToDecimal(txtObras10.Text);
                cronogramaPublica.ObrasMes11 = string.IsNullOrEmpty(txtObras11.Text) ? 0M : Convert.ToDecimal(txtObras11.Text);
                cronogramaPublica.ObrasMes12 = string.IsNullOrEmpty(txtObras12.Text) ? 0M : Convert.ToDecimal(txtObras12.Text);


                cronogramaPublica.ValorServicosTerceirosMes1 = string.IsNullOrEmpty(txtTot1Publica.Text) ? 0M : Convert.ToDecimal(txtTot1Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes2 = string.IsNullOrEmpty(txtTot2Publica.Text) ? 0M : Convert.ToDecimal(txtTot2Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes3 = string.IsNullOrEmpty(txtTot3Publica.Text) ? 0M : Convert.ToDecimal(txtTot3Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes4 = string.IsNullOrEmpty(txtTot4Publica.Text) ? 0M : Convert.ToDecimal(txtTot4Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes5 = string.IsNullOrEmpty(txtTot5Publica.Text) ? 0M : Convert.ToDecimal(txtTot5Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes6 = string.IsNullOrEmpty(txtTot6Publica.Text) ? 0M : Convert.ToDecimal(txtTot6Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes7 = string.IsNullOrEmpty(txtTot7Publica.Text) ? 0M : Convert.ToDecimal(txtTot7Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes8 = string.IsNullOrEmpty(txtTot8Publica.Text) ? 0M : Convert.ToDecimal(txtTot8Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes9 = string.IsNullOrEmpty(txtTot9Publica.Text) ? 0M : Convert.ToDecimal(txtTot9Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes10 = string.IsNullOrEmpty(txtTot10Publica.Text) ? 0M : Convert.ToDecimal(txtTot10Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes11 = string.IsNullOrEmpty(txtTot11Publica.Text) ? 0M : Convert.ToDecimal(txtTot11Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes12 = string.IsNullOrEmpty(txtTot12Publica.Text) ? 0M : Convert.ToDecimal(txtTot12Publica.Text);

            }

            if (cronogramaPublica.IdTipoProtecaoSocial == 3)
            {
                cronogramaPublica.ValorInvestimentoMes1 = string.IsNullOrEmpty(txtInv1Publica.Text) ? 0M : Convert.ToDecimal(txtInv1Publica.Text);
                cronogramaPublica.ValorInvestimentoMes2 = string.IsNullOrEmpty(txtInv2Publica.Text) ? 0M : Convert.ToDecimal(txtInv2Publica.Text);
                cronogramaPublica.ValorInvestimentoMes3 = string.IsNullOrEmpty(txtInv3Publica.Text) ? 0M : Convert.ToDecimal(txtInv3Publica.Text);
                cronogramaPublica.ValorInvestimentoMes4 = string.IsNullOrEmpty(txtInv4Publica.Text) ? 0M : Convert.ToDecimal(txtInv4Publica.Text);
                cronogramaPublica.ValorInvestimentoMes5 = string.IsNullOrEmpty(txtInv5Publica.Text) ? 0M : Convert.ToDecimal(txtInv5Publica.Text);
                cronogramaPublica.ValorInvestimentoMes6 = string.IsNullOrEmpty(txtInv6Publica.Text) ? 0M : Convert.ToDecimal(txtInv6Publica.Text);
                cronogramaPublica.ValorInvestimentoMes7 = string.IsNullOrEmpty(txtInv7Publica.Text) ? 0M : Convert.ToDecimal(txtInv7Publica.Text);
                cronogramaPublica.ValorInvestimentoMes8 = string.IsNullOrEmpty(txtInv8Publica.Text) ? 0M : Convert.ToDecimal(txtInv8Publica.Text);
                cronogramaPublica.ValorInvestimentoMes9 = string.IsNullOrEmpty(txtInv9Publica.Text) ? 0M : Convert.ToDecimal(txtInv9Publica.Text);
                cronogramaPublica.ValorInvestimentoMes10 = string.IsNullOrEmpty(txtInv10Publica.Text) ? 0M : Convert.ToDecimal(txtInv10Publica.Text);
                cronogramaPublica.ValorInvestimentoMes11 = string.IsNullOrEmpty(txtInv11Publica.Text) ? 0M : Convert.ToDecimal(txtInv11Publica.Text);
                cronogramaPublica.ValorInvestimentoMes12 = string.IsNullOrEmpty(txtInv12Publica.Text) ? 0M : Convert.ToDecimal(txtInv12Publica.Text);

                cronogramaPublica.ObrasMes1 = string.IsNullOrEmpty(txtObras1.Text) ? 0M : Convert.ToDecimal(txtObras1.Text);
                cronogramaPublica.ObrasMes2 = string.IsNullOrEmpty(txtObras2.Text) ? 0M : Convert.ToDecimal(txtObras2.Text);
                cronogramaPublica.ObrasMes3 = string.IsNullOrEmpty(txtObras3.Text) ? 0M : Convert.ToDecimal(txtObras3.Text);
                cronogramaPublica.ObrasMes4 = string.IsNullOrEmpty(txtObras4.Text) ? 0M : Convert.ToDecimal(txtObras4.Text);
                cronogramaPublica.ObrasMes5 = string.IsNullOrEmpty(txtObras5.Text) ? 0M : Convert.ToDecimal(txtObras5.Text);
                cronogramaPublica.ObrasMes6 = string.IsNullOrEmpty(txtObras6.Text) ? 0M : Convert.ToDecimal(txtObras6.Text);
                cronogramaPublica.ObrasMes7 = string.IsNullOrEmpty(txtObras7.Text) ? 0M : Convert.ToDecimal(txtObras7.Text);
                cronogramaPublica.ObrasMes8 = string.IsNullOrEmpty(txtObras8.Text) ? 0M : Convert.ToDecimal(txtObras8.Text);
                cronogramaPublica.ObrasMes9 = string.IsNullOrEmpty(txtObras9.Text) ? 0M : Convert.ToDecimal(txtObras9.Text);
                cronogramaPublica.ObrasMes10 = string.IsNullOrEmpty(txtObras10.Text) ? 0M : Convert.ToDecimal(txtObras10.Text);
                cronogramaPublica.ObrasMes11 = string.IsNullOrEmpty(txtObras11.Text) ? 0M : Convert.ToDecimal(txtObras11.Text);
                cronogramaPublica.ObrasMes12 = string.IsNullOrEmpty(txtObras12.Text) ? 0M : Convert.ToDecimal(txtObras12.Text);

                cronogramaPublica.ValorServicosTerceirosMes1 = string.IsNullOrEmpty(txtTot1Publica.Text) ? 0M : Convert.ToDecimal(txtTot1Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes2 = string.IsNullOrEmpty(txtTot2Publica.Text) ? 0M : Convert.ToDecimal(txtTot2Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes3 = string.IsNullOrEmpty(txtTot3Publica.Text) ? 0M : Convert.ToDecimal(txtTot3Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes4 = string.IsNullOrEmpty(txtTot4Publica.Text) ? 0M : Convert.ToDecimal(txtTot4Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes5 = string.IsNullOrEmpty(txtTot5Publica.Text) ? 0M : Convert.ToDecimal(txtTot5Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes6 = string.IsNullOrEmpty(txtTot6Publica.Text) ? 0M : Convert.ToDecimal(txtTot6Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes7 = string.IsNullOrEmpty(txtTot7Publica.Text) ? 0M : Convert.ToDecimal(txtTot7Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes8 = string.IsNullOrEmpty(txtTot8Publica.Text) ? 0M : Convert.ToDecimal(txtTot8Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes9 = string.IsNullOrEmpty(txtTot9Publica.Text) ? 0M : Convert.ToDecimal(txtTot9Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes10 = string.IsNullOrEmpty(txtTot10Publica.Text) ? 0M : Convert.ToDecimal(txtTot10Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes11 = string.IsNullOrEmpty(txtTot11Publica.Text) ? 0M : Convert.ToDecimal(txtTot11Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes12 = string.IsNullOrEmpty(txtTot12Publica.Text) ? 0M : Convert.ToDecimal(txtTot12Publica.Text);
            }

            if (cronogramaPublica.IdTipoProtecaoSocial == 4)
            {
                cronogramaPublica.ValorInvestimentoMes1 = string.IsNullOrEmpty(txtInv1Publica.Text) ? 0M : Convert.ToDecimal(txtInv1Publica.Text);
                cronogramaPublica.ValorInvestimentoMes2 = string.IsNullOrEmpty(txtInv2Publica.Text) ? 0M : Convert.ToDecimal(txtInv2Publica.Text);
                cronogramaPublica.ValorInvestimentoMes3 = string.IsNullOrEmpty(txtInv3Publica.Text) ? 0M : Convert.ToDecimal(txtInv3Publica.Text);
                cronogramaPublica.ValorInvestimentoMes4 = string.IsNullOrEmpty(txtInv4Publica.Text) ? 0M : Convert.ToDecimal(txtInv4Publica.Text);
                cronogramaPublica.ValorInvestimentoMes5 = string.IsNullOrEmpty(txtInv5Publica.Text) ? 0M : Convert.ToDecimal(txtInv5Publica.Text);
                cronogramaPublica.ValorInvestimentoMes6 = string.IsNullOrEmpty(txtInv6Publica.Text) ? 0M : Convert.ToDecimal(txtInv6Publica.Text);
                cronogramaPublica.ValorInvestimentoMes7 = string.IsNullOrEmpty(txtInv7Publica.Text) ? 0M : Convert.ToDecimal(txtInv7Publica.Text);
                cronogramaPublica.ValorInvestimentoMes8 = string.IsNullOrEmpty(txtInv8Publica.Text) ? 0M : Convert.ToDecimal(txtInv8Publica.Text);
                cronogramaPublica.ValorInvestimentoMes9 = string.IsNullOrEmpty(txtInv9Publica.Text) ? 0M : Convert.ToDecimal(txtInv9Publica.Text);
                cronogramaPublica.ValorInvestimentoMes10 = string.IsNullOrEmpty(txtInv10Publica.Text) ? 0M : Convert.ToDecimal(txtInv10Publica.Text);
                cronogramaPublica.ValorInvestimentoMes11 = string.IsNullOrEmpty(txtInv11Publica.Text) ? 0M : Convert.ToDecimal(txtInv11Publica.Text);
                cronogramaPublica.ValorInvestimentoMes12 = string.IsNullOrEmpty(txtInv12Publica.Text) ? 0M : Convert.ToDecimal(txtInv12Publica.Text);

                cronogramaPublica.ValorServicosTerceirosMes1 = string.IsNullOrEmpty(txtTot1Publica.Text) ? 0M : Convert.ToDecimal(txtTot1Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes2 = string.IsNullOrEmpty(txtTot2Publica.Text) ? 0M : Convert.ToDecimal(txtTot2Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes3 = string.IsNullOrEmpty(txtTot3Publica.Text) ? 0M : Convert.ToDecimal(txtTot3Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes4 = string.IsNullOrEmpty(txtTot4Publica.Text) ? 0M : Convert.ToDecimal(txtTot4Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes5 = string.IsNullOrEmpty(txtTot5Publica.Text) ? 0M : Convert.ToDecimal(txtTot5Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes6 = string.IsNullOrEmpty(txtTot6Publica.Text) ? 0M : Convert.ToDecimal(txtTot6Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes7 = string.IsNullOrEmpty(txtTot7Publica.Text) ? 0M : Convert.ToDecimal(txtTot7Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes8 = string.IsNullOrEmpty(txtTot8Publica.Text) ? 0M : Convert.ToDecimal(txtTot8Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes9 = string.IsNullOrEmpty(txtTot9Publica.Text) ? 0M : Convert.ToDecimal(txtTot9Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes10 = string.IsNullOrEmpty(txtTot10Publica.Text) ? 0M : Convert.ToDecimal(txtTot10Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes11 = string.IsNullOrEmpty(txtTot11Publica.Text) ? 0M : Convert.ToDecimal(txtTot11Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes12 = string.IsNullOrEmpty(txtTot12Publica.Text) ? 0M : Convert.ToDecimal(txtTot12Publica.Text);

                cronogramaPublica.ObrasMes1 = string.IsNullOrEmpty(txtObras1.Text) ? 0M : Convert.ToDecimal(txtObras1.Text);
                cronogramaPublica.ObrasMes2 = string.IsNullOrEmpty(txtObras2.Text) ? 0M : Convert.ToDecimal(txtObras2.Text);
                cronogramaPublica.ObrasMes3 = string.IsNullOrEmpty(txtObras3.Text) ? 0M : Convert.ToDecimal(txtObras3.Text);
                cronogramaPublica.ObrasMes4 = string.IsNullOrEmpty(txtObras4.Text) ? 0M : Convert.ToDecimal(txtObras4.Text);
                cronogramaPublica.ObrasMes5 = string.IsNullOrEmpty(txtObras5.Text) ? 0M : Convert.ToDecimal(txtObras5.Text);
                cronogramaPublica.ObrasMes6 = string.IsNullOrEmpty(txtObras6.Text) ? 0M : Convert.ToDecimal(txtObras6.Text);
                cronogramaPublica.ObrasMes7 = string.IsNullOrEmpty(txtObras7.Text) ? 0M : Convert.ToDecimal(txtObras7.Text);
                cronogramaPublica.ObrasMes8 = string.IsNullOrEmpty(txtObras8.Text) ? 0M : Convert.ToDecimal(txtObras8.Text);
                cronogramaPublica.ObrasMes9 = string.IsNullOrEmpty(txtObras9.Text) ? 0M : Convert.ToDecimal(txtObras9.Text);
                cronogramaPublica.ObrasMes10 = string.IsNullOrEmpty(txtObras10.Text) ? 0M : Convert.ToDecimal(txtObras10.Text);
                cronogramaPublica.ObrasMes11 = string.IsNullOrEmpty(txtObras11.Text) ? 0M : Convert.ToDecimal(txtObras11.Text);
                cronogramaPublica.ObrasMes12 = string.IsNullOrEmpty(txtObras12.Text) ? 0M : Convert.ToDecimal(txtObras12.Text);

            }


            if (cronogramaPublica.IdTipoProtecaoSocial == 5)
            {
                cronogramaPublica.ValorInvestimentoMes1 = string.IsNullOrEmpty(txtInv1Publica.Text) ? 0M : Convert.ToDecimal(txtInv1Publica.Text);
                cronogramaPublica.ValorInvestimentoMes2 = string.IsNullOrEmpty(txtInv2Publica.Text) ? 0M : Convert.ToDecimal(txtInv2Publica.Text);
                cronogramaPublica.ValorInvestimentoMes3 = string.IsNullOrEmpty(txtInv3Publica.Text) ? 0M : Convert.ToDecimal(txtInv3Publica.Text);
                cronogramaPublica.ValorInvestimentoMes4 = string.IsNullOrEmpty(txtInv4Publica.Text) ? 0M : Convert.ToDecimal(txtInv4Publica.Text);
                cronogramaPublica.ValorInvestimentoMes5 = string.IsNullOrEmpty(txtInv5Publica.Text) ? 0M : Convert.ToDecimal(txtInv5Publica.Text);
                cronogramaPublica.ValorInvestimentoMes6 = string.IsNullOrEmpty(txtInv6Publica.Text) ? 0M : Convert.ToDecimal(txtInv6Publica.Text);
                cronogramaPublica.ValorInvestimentoMes7 = string.IsNullOrEmpty(txtInv7Publica.Text) ? 0M : Convert.ToDecimal(txtInv7Publica.Text);
                cronogramaPublica.ValorInvestimentoMes8 = string.IsNullOrEmpty(txtInv8Publica.Text) ? 0M : Convert.ToDecimal(txtInv8Publica.Text);
                cronogramaPublica.ValorInvestimentoMes9 = string.IsNullOrEmpty(txtInv9Publica.Text) ? 0M : Convert.ToDecimal(txtInv9Publica.Text);
                cronogramaPublica.ValorInvestimentoMes10 = string.IsNullOrEmpty(txtInv10Publica.Text) ? 0M : Convert.ToDecimal(txtInv10Publica.Text);
                cronogramaPublica.ValorInvestimentoMes11 = string.IsNullOrEmpty(txtInv11Publica.Text) ? 0M : Convert.ToDecimal(txtInv11Publica.Text);
                cronogramaPublica.ValorInvestimentoMes12 = string.IsNullOrEmpty(txtInv12Publica.Text) ? 0M : Convert.ToDecimal(txtInv12Publica.Text);

                cronogramaPublica.ObrasMes1 = string.IsNullOrEmpty(txtObras1.Text) ? 0M : Convert.ToDecimal(txtObras1.Text);
                cronogramaPublica.ObrasMes2 = string.IsNullOrEmpty(txtObras2.Text) ? 0M : Convert.ToDecimal(txtObras2.Text);
                cronogramaPublica.ObrasMes3 = string.IsNullOrEmpty(txtObras3.Text) ? 0M : Convert.ToDecimal(txtObras3.Text);
                cronogramaPublica.ObrasMes4 = string.IsNullOrEmpty(txtObras4.Text) ? 0M : Convert.ToDecimal(txtObras4.Text);
                cronogramaPublica.ObrasMes5 = string.IsNullOrEmpty(txtObras5.Text) ? 0M : Convert.ToDecimal(txtObras5.Text);
                cronogramaPublica.ObrasMes6 = string.IsNullOrEmpty(txtObras6.Text) ? 0M : Convert.ToDecimal(txtObras6.Text);
                cronogramaPublica.ObrasMes7 = string.IsNullOrEmpty(txtObras7.Text) ? 0M : Convert.ToDecimal(txtObras7.Text);
                cronogramaPublica.ObrasMes8 = string.IsNullOrEmpty(txtObras8.Text) ? 0M : Convert.ToDecimal(txtObras8.Text);
                cronogramaPublica.ObrasMes9 = string.IsNullOrEmpty(txtObras9.Text) ? 0M : Convert.ToDecimal(txtObras9.Text);
                cronogramaPublica.ObrasMes10 = string.IsNullOrEmpty(txtObras10.Text) ? 0M : Convert.ToDecimal(txtObras10.Text);
                cronogramaPublica.ObrasMes11 = string.IsNullOrEmpty(txtObras11.Text) ? 0M : Convert.ToDecimal(txtObras11.Text);
                cronogramaPublica.ObrasMes12 = string.IsNullOrEmpty(txtObras12.Text) ? 0M : Convert.ToDecimal(txtObras12.Text);

                cronogramaPublica.ValorServicosTerceirosMes1 = string.IsNullOrEmpty(txtTot1Publica.Text) ? 0M : Convert.ToDecimal(txtTot1Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes2 = string.IsNullOrEmpty(txtTot2Publica.Text) ? 0M : Convert.ToDecimal(txtTot2Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes3 = string.IsNullOrEmpty(txtTot3Publica.Text) ? 0M : Convert.ToDecimal(txtTot3Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes4 = string.IsNullOrEmpty(txtTot4Publica.Text) ? 0M : Convert.ToDecimal(txtTot4Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes5 = string.IsNullOrEmpty(txtTot5Publica.Text) ? 0M : Convert.ToDecimal(txtTot5Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes6 = string.IsNullOrEmpty(txtTot6Publica.Text) ? 0M : Convert.ToDecimal(txtTot6Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes7 = string.IsNullOrEmpty(txtTot7Publica.Text) ? 0M : Convert.ToDecimal(txtTot7Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes8 = string.IsNullOrEmpty(txtTot8Publica.Text) ? 0M : Convert.ToDecimal(txtTot8Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes9 = string.IsNullOrEmpty(txtTot9Publica.Text) ? 0M : Convert.ToDecimal(txtTot9Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes10 = string.IsNullOrEmpty(txtTot10Publica.Text) ? 0M : Convert.ToDecimal(txtTot10Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes11 = string.IsNullOrEmpty(txtTot11Publica.Text) ? 0M : Convert.ToDecimal(txtTot11Publica.Text);
                cronogramaPublica.ValorServicosTerceirosMes12 = string.IsNullOrEmpty(txtTot12Publica.Text) ? 0M : Convert.ToDecimal(txtTot12Publica.Text);

            }

            return cronogramaPublica;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueio()
        {
            #region Bloqueia , Desbloqueia e ordena Controles
            WebControl[] controles = { 

                                             txtInv1Publica,
                                             txtInv2Publica,
                                             txtInv3Publica,
                                             txtInv4Publica,
                                             txtInv5Publica,
                                             txtInv6Publica,
                                             txtInv7Publica,
                                             txtInv8Publica,
                                             txtInv9Publica,
                                             txtInv10Publica,
                                             txtInv11Publica,
                                             txtInv12Publica,

                                             txtST1,
                                             txtST2,
                                             txtST3,
                                             txtST4,
                                             txtST5,
                                             txtST6,
                                             txtST7,
                                             txtST8,
                                             txtST9,
                                             txtST10,
                                             txtST11,
                                             txtST12,                                         

                                             txtRH1, 
                                             txtMC1, 
                                             txtRH2, 
                                             txtMC2, 
                                             txtRH3, 
                                             txtMC3, 
                                             txtRH4, 
                                             txtMC4, 
                                             txtRH5, 
                                             txtMC5, 
                                             txtRH6, 
                                             txtMC6, 
                                             txtRH7, 
                                             txtMC7, 
                                             txtRH8, 
                                             txtMC8, 
                                             txtRH9, 
                                             txtMC9, 
                                             txtRH10, 
                                             txtMC10, 
                                             txtRH11, 
                                             txtMC11, 
                                             txtRH12, 
                                             txtMC12, 
                                             txtCusteio1, 
                                             txtCusteio2, 
                                             txtCusteio3, 
                                             txtCusteio4, 
                                             txtCusteio5, 
                                             txtCusteio6, 
                                             txtCusteio7, 
                                             txtCusteio8, 
                                             txtCusteio9, 
                                             txtCusteio10, 
                                             txtCusteio11,
                                             txtCusteio12,
                                          
                                             txtOutroCusteio1,
                                             txtOutroCusteio2,
                                             txtOutroCusteio3,
                                             txtOutroCusteio4,
                                             txtOutroCusteio5,
                                             txtOutroCusteio6,
                                             txtOutroCusteio7,
                                             txtOutroCusteio8,
                                             txtOutroCusteio9,
                                             txtOutroCusteio10,
                                             txtOutroCusteio11,
                                             txtOutroCusteio12,
                                             txtTot1Publica,
                                             txtTot2Publica,
                                             txtTot3Publica,
                                             txtTot4Publica,
                                             txtTot5Publica,
                                             txtTot6Publica,
                                             txtTot7Publica,
                                             txtTot8Publica,
                                             txtTot9Publica,
                                             txtTot10Publica,
                                             txtTot11Publica,
                                             txtTot12Publica,
                                             //txtReprogramacaoRecursosDisponibilizados,
                                             //txtReprogramacaoCusteio,
                                             //txtReprogramacaoInvestimento,
                                             //txtReprogramacaoRH,
                                             txtTot1,
                                             txtTot2,
                                             txtTot3,
                                             txtTot4,
                                             txtTot5,
                                             txtTot6,
                                             txtTot7,
                                             txtTot8,
                                             txtTot9,
                                             txtTot10,
                                             txtTot11,
                                             txtTot12,
                                             txtObras1,
                                             txtObras2,
                                             txtObras3,
                                             txtObras4,
                                             txtObras5,
                                             txtObras6,
                                             txtObras7,
                                             txtObras8,
                                             txtObras9,
                                             txtObras10,
                                             txtObras11,
                                             txtObras12,
                                             txtObrasPrivada1,
                                             txtObrasPrivada2,
                                             txtObrasPrivada3,
                                             txtObrasPrivada4,
                                             txtObrasPrivada5,
                                             txtObrasPrivada6,
                                             txtObrasPrivada7,
                                             txtObrasPrivada8,
                                             txtObrasPrivada9,
                                             txtObrasPrivada10,
                                             txtObrasPrivada11,
                                             txtObrasPrivada12,
                                             //txtRecursosReprogramadoPrivado,
                                             //txtRecursosHumanosReprogramadoPrivado,
                                             //txtOutrasCusteioReprogramadoPrivado,
                                             txtEquipamentosPrivadoReprogramado,
                                             
                                             //btnCalcular,
                                             btnSalvar,
                                             btnLimparRedePublica,
                                             btnLimparRedePrivada };
            #endregion

            return controles;
        }

        #endregion


        #region bloqueio desbloqueio
        private void AplicarRegraBloqueioDesbloqueio()
        {

            WebControl[] controles = this.ObterControlesBloqueioDesbloqueio();
            WebControl[] controlesReprogramacaoDireta = this.ObterControlesBloqueioDesbloqueioReprogramacao();
            WebControl[] controlesReprogramacaoIndireta = this.ObterControlesBloqueioDesbloqueioReprogramacaoPrivada();
            WebControl[] controlesDemandasPublicas = this.ObterControlesBloqueioDesbloqueioDemandasPublicas();
            WebControl[] controlesDemandasPrivadas = this.ObterControlesBloqueioDesbloqueioDemandasPrivadas();

            int tipoProtecao = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            if (tipoProtecao == 1)
            {
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoV(controles, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVReprogramacao(controlesReprogramacaoDireta, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVReprogramacaoPrivada(controlesReprogramacaoIndireta, Convert.ToInt32(hdfAno.Value));

                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVDemandasPublicas(controlesDemandasPublicas, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVDemandasPrivadas(controlesDemandasPrivadas, Convert.ToInt32(hdfAno.Value));
                
                var permissao = Permissao.BlocoV.VerificaPermissaoReprogramacaoBotaoSalvarRedeDireta(btnSalvar, Convert.ToInt32(hdfAno.Value));
                
                if (!permissao)
                {
                    Permissao.BlocoV.VerificaPermissaoDemandasBotaoSalvar(btnSalvar, Convert.ToInt32(hdfAno.Value));    
                }
               
            }
            if (tipoProtecao == 2)
            {
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSEMediaComplexidadeBlocoV(controles, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSEMediaComplexidadeVReprogramacao(controlesReprogramacaoDireta, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVReprogramacaoPrivada(controlesReprogramacaoIndireta, Convert.ToInt32(hdfAno.Value));

                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSEMediaComplexidadeVDemandasPublicas(controlesDemandasPublicas, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSEMediaComplexidadeVDemandasPrivadas(controlesDemandasPrivadas, Convert.ToInt32(hdfAno.Value));

                var permissao = Permissao.BlocoV.VerificaPermissaoReprogramacaoBotaoSalvarRedeDireta(btnSalvar, Convert.ToInt32(hdfAno.Value));

                if (!permissao)
                {
                    Permissao.BlocoV.VerificaPermissaoDemandasBotaoSalvar(btnSalvar, Convert.ToInt32(hdfAno.Value));
                }

            }
            if (tipoProtecao == 3)
            {
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSEAltaComplexidadeBlocoV(controles, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSEAltaComplexidadeVReprogramacao(controlesReprogramacaoDireta, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVReprogramacaoPrivada(controlesReprogramacaoIndireta, Convert.ToInt32(hdfAno.Value));

                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSEAltaComplexidadeVDemandasPublicas(controlesDemandasPublicas, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSEAltaComplexidadeVDemandasPrivadas(controlesDemandasPrivadas, Convert.ToInt32(hdfAno.Value));

                var permissao = Permissao.BlocoV.VerificaPermissaoReprogramacaoBotaoSalvarRedeDireta(btnSalvar, Convert.ToInt32(hdfAno.Value));

                if (!permissao)
                {
                    Permissao.BlocoV.VerificaPermissaoDemandasBotaoSalvar(btnSalvar, Convert.ToInt32(hdfAno.Value));
                }

            }
            if (tipoProtecao == 4)
            {
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaProgramasProjetosBlocoV(controles, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoBeneficiosEventuaisBlocoVReprogramacao(controlesReprogramacaoDireta, Convert.ToInt32(hdfAno.Value));

                var permissao = Permissao.BlocoV.VerificaPermissaoExercicioCrogramaProgramasProjetosBlocoVBtnSalvar(btnSalvar, Convert.ToInt32(hdfAno.Value));

                if (!permissao)
                {
                    Permissao.BlocoV.VerificaPermissaoDemandasBotaoSalvar(btnSalvar, Convert.ToInt32(hdfAno.Value));
                }  

            }
            if (tipoProtecao == 5)
            {
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoBeneficiosEventuaisBlocoV(controles, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVDemandasPublicas(controlesDemandasPublicas, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVDemandasPrivadas(controlesDemandasPrivadas, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoBeneficiosEventuaisBlocoVReprogramacao(controlesReprogramacaoDireta, Convert.ToInt32(hdfAno.Value));
                Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoPSBasicaBlocoVReprogramacaoPrivada(controlesReprogramacaoIndireta, Convert.ToInt32(hdfAno.Value));

                var permissao = Permissao.BlocoV.VerificaPermissaoExercicioCrogramaDesembolsoBeneficiosEventuaisBlocoVBtnSalvar(btnSalvar, Convert.ToInt32(hdfAno.Value));

                if (!permissao)
                {
                    Permissao.BlocoV.VerificaPermissaoDemandasBotaoSalvar(btnSalvar, Convert.ToInt32(hdfAno.Value));
                }
 
            }
        }


        private WebControl[] ObterControlesBloqueioDesbloqueioReprogramacao()
        {
            WebControl[] controles = {
                      txtReprogramacaoRecursosDisponibilizados
                    , txtReprogramacaoRH
                    , txtReprogramacaoCusteio
                    , txtReprogramacaoInvestimento
                    , txtReprogramacaoObras

                    ,txtReprogramadoDemandasParlamentaresDisponibilizados
			        ,txtReprogramadoDemandasRH
			        ,txtReprogramadoDemandasCusteio
			        ,txtReprogramadoDemandasInvestimento
			        ,txtReprogramadoDemandasObras     
                 
                };
            return controles;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioReprogramacaoPrivada()
        {
            WebControl[] controles = { 
                   txtRecursosReprogramadoPrivado
                  , txtRecursosHumanosReprogramadoPrivado
                  , txtOutrasCusteioReprogramadoPrivado
                  , txtEquipamentosPrivadoReprogramado
                  , txtObrasReprogramadoPrivado

			      ,txtReprogramadoDemandasParlamentaresPrivado
			      ,txtRecursosHumanosReprogramadoDemandasPrivado
			      ,txtOutrasCusteioReprogramadoDemandasPrivado
			      ,txtEquipamentosPrivadoReprogramadoDemandas
			      ,txtObrasReprogramadoDemandasPrivado  

                                     };

            return controles;
        }

        private WebControl[] ObterControlesBloqueioDesbloqueioDemandasPublicas ()
        {
            WebControl[] controles = {
			txtDemandasParlamentaresDisponibilizados
			,txtDemandasRH
			,txtDemandasCusteio
			,txtDemandasInvestimento
			,txtDemandasObras
                                     };

            return controles;
        }


        private WebControl[] ObterControlesBloqueioDesbloqueioDemandasPrivadas()
        {
            WebControl[] controles = {
			txtDemandasParlamentaresPrivado
			,txtRecursosHumanosDemandasPrivado
			,txtOutrasCusteioDemandasPrivado
			,txtEquipamentosPrivadoDemandas
			,txtObrasDemandasPrivado
               
                                     };

            return controles;
        }

        #endregion

        protected void lstRecursos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if ((lstRecursos.FindControl("lblTotalCofinanciamentoEstadual") as Label) != null)
            {
                (lstRecursos.FindControl("lblTotalCofinanciamentoEstadual") as Label).Text = hdnSomaPrevisaoOrcamentaria.Value;
            }
            if ((lstRecursos.FindControl("lblCofinanciamentoReprogramado") as Label) != null)
            {
                (lstRecursos.FindControl("lblCofinanciamentoReprogramado") as Label).Text = hdnSomaRecursoReprogramadoAnoAnterior.Value;
            }

            if ((lstRecursos.FindControl("lblCofinanciamentoDemandas") as Label) != null)
            {
                (lstRecursos.FindControl("lblCofinanciamentoDemandas") as Label).Text = hdnSomaDemandasParlamentares.Value;
            }

            if ((lstRecursos.FindControl("lblCofinanciamentoReprogramadoDemandas") as Label) != null)
            {
                (lstRecursos.FindControl("lblCofinanciamentoReprogramadoDemandas") as Label).Text = hdnSomaReprogramadoDemandasParlamentares.Value;
            }

            if ((lstRecursos.FindControl("lblTotalCapacidadePessoas") as Label) != null)
            {
                (lstRecursos.FindControl("lblTotalCapacidadePessoas") as Label).Text = hdnSomaNumeroAtendidos.Value;
            }

            if ((lstRecursos.FindControl("lblTotalCofinancimento") as Label) != null)
            {
                (lstRecursos.FindControl("lblTotalCofinancimento") as Label).Text =
                (Convert.ToDecimal((lstRecursos.FindControl("lblTotalCofinanciamentoEstadual") as Label).Text)
                + Convert.ToDecimal((lstRecursos.FindControl("lblCofinanciamentoReprogramado") as Label).Text)).ToString("n2");
            }

            if ((lstRecursos.FindControl("tblTotalCofinanciamentoEstadual") as Label) != null)
            {
                (lstRecursos.FindControl("tblTotalCofinanciamentoEstadual") as HtmlTableRow).Visible = true;
            }
        }
    }
}