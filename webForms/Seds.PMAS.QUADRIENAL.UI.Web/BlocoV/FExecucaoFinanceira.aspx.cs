using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Web.UI.HtmlControls;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoV
{
    public partial class FExecucaoFinanceira : System.Web.UI.Page
    {

        #region properties
        private static List<int> Exercicios = new List<int>() { 2021, 2022, 2023, 2024 };
        private List<ExecucaoFinanceiraInfo> sessionExecucaoFinanceira;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? "2021" : this.hdfAno.Value;

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                adicionarEventos();

                Clear();

                LoadFMAS();
                LoadFEAS();
                LoadFNAS();
                LoadCMAS();
                LoadTotais();
                
                LoadExercicios();

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    CarregarExecucaoFinanceira(prefeituras);
                    AplicarBloqueioControles(proxy);
                }

            }
        }

        private void LoadFMAS() 
        {
            trFMASBeneficiosEventuais.Visible = true;
            trFMASProgramasProjetos.Visible = true;
            tdFMASTitulo.RowSpan = 6;           
        }

        private void LoadFEAS()
        {
            trEstadual.Visible = false;
            trEstadualBasica.Visible = false;
            trEstadualMedia.Visible = false;
            trEstadualAlta.Visible = false;
            trEstadualTotais.Visible = false;

        }

        private void LoadFNAS() 
        {
           
            trFNASProgramasProjetos.Visible = true;
            trFNASIncentivoGestao.Visible = true;
            trFNASExercicioAnterior.Visible = false;
            trFNASProtecaoSocialEspecial.Visible = true;
            trFNASMedia.Visible = false;
            trFNASAlta.Visible = false;
            tdRecursosFederais.RowSpan = 5;
 
        }

        private void LoadTotais()
        {
           trProgramasProjetosTotal.Visible = true;
           trIncentivoTotal.Visible = true;
           trBeneficiosEventuaisTotal.Visible = true;
           trEspecialTotal.Visible = true;
           trTotalMedia.Visible = false;
           trTotalAlta.Visible = false;
           tdTotal.RowSpan = 6;
        }

        private void LoadCMAS() 
        {
           trComentarioCMAS.Visible = true;
           trDeliberacao.Visible = true;
        }

        #region equipes especiais - exercicio botões Load Exercicios

        private void LoadExercicios()
        {
            this.btnExercicio1.Text = FExecucaoFinanceira.Exercicios[0].ToString();
            this.btnExercicio2.Text = FExecucaoFinanceira.Exercicios[1].ToString();
            this.btnExercicio3.Text = FExecucaoFinanceira.Exercicios[2].ToString();
            this.btnExercicio4.Text = FExecucaoFinanceira.Exercicios[3].ToString();

            if (FExecucaoFinanceira.Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn btn-info";
                this.btnExercicio2.CssClass = "btn btn-primary";
                this.btnExercicio3.CssClass = "btn btn-primary";
                this.btnExercicio4.CssClass = "btn btn-primary";
            }
            if (FExecucaoFinanceira.Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn btn-primary";
                this.btnExercicio2.CssClass = "btn btn-info";
                this.btnExercicio3.CssClass = "btn btn-primary";
                this.btnExercicio4.CssClass = "btn btn-primary";
            }

            if (FExecucaoFinanceira.Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn btn-primary";
                this.btnExercicio2.CssClass = "btn btn-primary";
                this.btnExercicio3.CssClass = "btn btn-info";
                this.btnExercicio4.CssClass = "btn btn-primary";
            }

            if (FExecucaoFinanceira.Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn btn-primary";
                this.btnExercicio2.CssClass = "btn btn-primary";
                this.btnExercicio3.CssClass = "btn btn-primary";
                this.btnExercicio4.CssClass = "btn btn-info";
            }

        }

        protected void btnLoadExercicio1_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio1.Text;
            lblExecucaoDisponibilidade.Text = "Quadro de execução financeira de 2017.";
            pInformacoes.Visible = false;

            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                this.CarregarExecucaoFinanceira(prefeituras);
                this.AplicarBloqueioControles(proxy);
            }
            #endregion

        }

        protected void btnLoadExercicio2_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio2.Text;
            pInformacoes.Visible = false;

            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                this.CarregarExecucaoFinanceira(prefeituras);
                this.AplicarBloqueioControles(proxy);

            }
            #endregion
        }

        protected void btnLoadExercicio3_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio3.Text;
            pInformacoes.Visible = false;

            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                this.CarregarExecucaoFinanceira(prefeituras);
                this.AplicarBloqueioControles(proxy);

            }
            #endregion
        }

        protected void btnLoadExercicio4_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio4.Text;
            pInformacoes.Visible = true;

            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                this.CarregarExecucaoFinanceira(prefeituras);
                this.AplicarBloqueioControles(proxy);

            }
            #endregion
        }

        #endregion

        void adicionarEventos()
        {
            txtFMASPrevisaoInicialBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event, false ) );");
            txtFMASPrevisaoInicialMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASPrevisaoInicialAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPrevisaoInicialBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPrevisaoInicialMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPrevisaoInicialAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASPrevisaoInicialBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASPrevisaoInicialMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASPrevisaoInicialAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASRecursosDisponibilizadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASRecursosDisponibilizadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASRecursosDisponibilizadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASRecursosDisponibilizadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASRecursosDisponibilizadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASRecursosDisponibilizadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASResultadoAppFinanceirasBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASResultadoAppFinanceirasMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASResultadoAppFinanceirasAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASResultadoAppFinanceirasBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASResultadoAppFinanceirasMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASResultadoAppFinanceirasAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresExecutadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresExecutadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresExecutadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresExecutadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresExecutadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresExecutadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresReprogramadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresReprogramadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresReprogramadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresReprogramadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresReprogramadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresReprogramadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresDevolvidosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresDevolvidosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresDevolvidosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresDevolvidosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresDevolvidosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresDevolvidosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtPrevisaoInicialReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRecursosDisponibilizadosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtResultadosAplicacaoReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValoresExecutadosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValoresReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASPrevisaoInicialBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASRecursosDisponibilizadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASResultadoAppFinanceirasBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresExecutadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresReprogramadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresDevolvidosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASPorcentagensExecucaoBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASPrevisaoInicialProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASRecursosDisponibilizadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASResultadoAppFinanceirasProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresExecutadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresReprogramadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASValoresDevolvidosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMASPorcentagensExecucaoProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFNASPrevisaoInicialProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASRecursosDisponibilizadosProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASResultadoAppFinanceirasProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresExecutadosProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresReprogramadosProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresDevolvidosProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASPorcentagensExecucaoProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFNASPrevisaoInicialProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASRecursosDisponibilizadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASResultadoAppFinanceirasProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresExecutadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresReprogramadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresDevolvidosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASPorcentagensExecucaoProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFNASPrevisaoInicialIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASRecursosDisponibilizadosIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASResultadoAppFinanceirasIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresExecutadosIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresReprogramadosIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresDevolvidosIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASPorcentagensExecucaoIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFNASPrevisaoInicialExercicioAnterior.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASRecursosDisponibilizadosExercicioAnterior.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASResultadoAppFinanceirasExercicioAnterior.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresExecutadosExercicioAnterior.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresReprogramadosExercicioAnterior.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASValoresDevolvidosExercicioAnterior.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNASPorcentagensExecucaoExercicioAnterior.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            //Totais

            txtTotalPrevisaoInicialBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalRecursosDisponibilizadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalResultadoAppFinanceirasBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresExecutadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresReprogramadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresDevolvidosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalPorcentagensExecucaoBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            
            txtTotalPrevisaoInicialProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalRecursosDisponibilizadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalResultadoAppFinanceirasProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresExecutadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresReprogramadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresDevolvidosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalPorcentagensExecucaoProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            
            txtTotalPrevisaoInicialIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalRecursosDisponibilizadosIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalResultadoAppFinanceirasIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresExecutadosIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresReprogramadosIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresDevolvidosIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalPorcentagensExecucaoIncentivoGestao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            
            txtTotalPrevisaoInicialProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalRecursosDisponibilizadosProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalResultadoAppFinanceirasProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresExecutadosProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresReprogramadosProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalValoresDevolvidosProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtTotalPorcentagensExecucaoProtecaoSocialEspecial.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

        }

        void AplicarBloqueioControles(ProxyPrefeitura proxy)
        {
            var controles1 = obterControlesProtecaoBasica();
            var controles2 = obterControlesProtecaoEspecialMedia();
            var controles3 = obterControlesProtecaoEspecialAlta();
            var controles4 = obterControlesTotalizarFMAS();
            var controles5 = obterControlesTotalizarFEAS();
            var controles6 = obterControlesTotalizarFNAS();
            var controles7 = obterControlesTotalizar();
            var controles8 = ObterControleBeneficioseventuais();
            var controles9 = ObterControleProgramasProjetos();
            var controles10 = ObterControleEspecial();
            var controles11 = ObterControleIncentivoGestao();
            var controles12 = ObterControleExAnterior();

            HtmlGenericControl[] htmlControls = {trFinalizarGestorCalculo};
            BloqueioEFPorSituacaoQuadro(proxy);  
        }


        private void ListarControlesDeTela()
        {
            #region Controles
            WebControl[] controles = {  txtFMASPrevisaoInicialBasica,
                                        txtFMASPrevisaoInicialMedia,
                                        txtFMASPrevisaoInicialAlta,
                                        txtFEASPrevisaoInicialBasica,
                                        txtFEASPrevisaoInicialMedia,
                                        txtFEASPrevisaoInicialAlta,
                                        txtFNASPrevisaoInicialBasica,
                                        txtFNASPrevisaoInicialMedia,
                                        txtFNASPrevisaoInicialAlta,

                                        txtFMASRecursosDisponibilizadosBasica,
                                        txtFMASRecursosDisponibilizadosMedia,
                                        txtFMASRecursosDisponibilizadosAlta,
                                        txtFEASRecursosDisponibilizadosBasica,
                                        txtFEASRecursosDisponibilizadosMedia,
                                        txtFEASRecursosDisponibilizadosAlta,
                                        txtFNASRecursosDisponibilizadosBasica,
                                        txtFNASRecursosDisponibilizadosMedia,
                                        txtFNASRecursosDisponibilizadosAlta,

                                        txtFMASResultadoAppFinanceirasBasica,
                                        txtFMASResultadoAppFinanceirasMedia,
                                        txtFMASResultadoAppFinanceirasAlta,
                                        txtFEASResultadoAppFinanceirasBasica,
                                        txtFEASResultadoAppFinanceirasMedia,
                                        txtFEASResultadoAppFinanceirasAlta,
                                        txtFNASResultadoAppFinanceirasBasica,
                                        txtFNASResultadoAppFinanceirasMedia,
                                        txtFNASResultadoAppFinanceirasAlta,

                                        txtFMASValoresExecutadosBasica,
                                        txtFMASValoresExecutadosMedia,
                                        txtFMASValoresExecutadosAlta,
                                        txtFEASValoresExecutadosBasica,
                                        txtFEASValoresExecutadosMedia,
                                        txtFEASValoresExecutadosAlta,
                                        txtFNASValoresExecutadosBasica,
                                        txtFNASValoresExecutadosMedia,
                                        txtFNASValoresExecutadosAlta,
                                        txtFMASValoresExecutadosBasica,
                                        txtFMASValoresExecutadosMedia,
                                        txtFMASValoresExecutadosAlta,

                                        txtFMASValoresReprogramadosBasica,
                                        txtFMASValoresReprogramadosMedia,
                                        txtFMASValoresReprogramadosAlta,
                                        txtFEASValoresReprogramadosBasica,
                                        txtFEASValoresReprogramadosMedia,
                                        txtFEASValoresReprogramadosAlta,
                                        txtFNASValoresReprogramadosBasica,
                                        txtFNASValoresReprogramadosMedia,
                                        txtFNASValoresReprogramadosAlta,

                                        txtFNASPrevisaoInicialProgramasProjetos,
                                        txtFNASRecursosDisponibilizadosProgramasProjetos,
                                        txtFNASResultadoAppFinanceirasProgramasProjetos,
                                        txtFNASValoresExecutadosProgramasProjetos,
                                        txtFNASValoresReprogramadosProgramasProjetos,
                                        txtFNASValoresDevolvidosProgramasProjetos,
                                        txtFNASPorcentagensExecucaoProgramasProjetos,
                                        txtFNASPrevisaoInicialIncentivoGestao,
                                        txtFNASRecursosDisponibilizadosIncentivoGestao,
                                        txtFNASResultadoAppFinanceirasIncentivoGestao,
                                        txtFNASValoresExecutadosIncentivoGestao,
                                        txtFNASValoresReprogramadosIncentivoGestao,
                                        txtFNASValoresDevolvidosIncentivoGestao,
                                        txtFNASPorcentagensExecucaoIncentivoGestao,
                                        txtFNASPrevisaoInicialProtecaoSocialEspecial,
                                        txtFNASRecursosDisponibilizadosProtecaoSocialEspecial,
                                        txtFNASResultadoAppFinanceirasProtecaoSocialEspecial,
                                        txtFNASValoresExecutadosProtecaoSocialEspecial,
                                        txtFNASValoresReprogramadosProtecaoSocialEspecial,
                                        txtFNASValoresDevolvidosProtecaoSocialEspecial,
                                        txtFNASPorcentagensExecucaoProtecaoSocialEspecial,
                                        txtFNASPrevisaoInicialExercicioAnterior, 
                                        txtFNASRecursosDisponibilizadosExercicioAnterior,
                                        txtFNASResultadoAppFinanceirasExercicioAnterior,
                                        txtFNASValoresExecutadosExercicioAnterior,
                                        txtFNASValoresReprogramadosExercicioAnterior,
                                        txtFNASValoresDevolvidosExercicioAnterior,
                                        txtFNASPorcentagensExecucaoExercicioAnterior,
                                        
                                        txtTotalPrevisaoInicialBeneficiosEventuais, 
                                        txtTotalRecursosDisponibilizadosBeneficiosEventuais,
                                        txtTotalResultadoAppFinanceirasBeneficiosEventuais,
                                        txtTotalValoresExecutadosBeneficiosEventuais,
                                        txtTotalValoresReprogramadosBeneficiosEventuais,
                                        txtTotalValoresDevolvidosBeneficiosEventuais,
                                        txtTotalPorcentagensExecucaoBeneficiosEventuais,
                                        txtTotalPrevisaoInicialProgramasProjetos,
                                        txtTotalRecursosDisponibilizadosProgramasProjetos,
                                        txtTotalResultadoAppFinanceirasProgramasProjetos,
                                        txtTotalValoresExecutadosProgramasProjetos,
                                        txtTotalValoresReprogramadosProgramasProjetos,
                                        txtTotalValoresDevolvidosProgramasProjetos,
                                        txtTotalPorcentagensExecucaoProgramasProjetos,
                                        txtTotalPrevisaoInicialIncentivoGestao,
                                        txtTotalRecursosDisponibilizadosIncentivoGestao,
                                        txtTotalResultadoAppFinanceirasIncentivoGestao,
                                        txtTotalValoresExecutadosIncentivoGestao,
                                        txtTotalValoresReprogramadosIncentivoGestao,
                                        txtTotalValoresDevolvidosIncentivoGestao,
                                        txtTotalPorcentagensExecucaoIncentivoGestao,
                                        txtTotalPrevisaoInicialProtecaoSocialEspecial,
                                        txtTotalRecursosDisponibilizadosProtecaoSocialEspecial,
                                        txtTotalResultadoAppFinanceirasProtecaoSocialEspecial,
                                        txtTotalValoresExecutadosProtecaoSocialEspecial,
                                        txtTotalValoresReprogramadosProtecaoSocialEspecial,
                                        txtTotalValoresDevolvidosProtecaoSocialEspecial,
                                        txtTotalPorcentagensExecucaoProtecaoSocialEspecial,

                                        txtFMASPrevisaoInicialBeneficiosEventuais,
                                        txtFMASRecursosDisponibilizadosBeneficiosEventuais,
                                        txtFMASResultadoAppFinanceirasBeneficiosEventuais,
                                        txtFMASValoresExecutadosBeneficiosEventuais,
                                        txtFMASValoresReprogramadosBeneficiosEventuais,
                                        txtFMASValoresDevolvidosBeneficiosEventuais,
                                        txtFMASPorcentagensExecucaoBeneficiosEventuais,
                                        
                                        txtFMASPrevisaoInicialProgramasProjetos,
                                        txtFMASRecursosDisponibilizadosProgramasProjetos,
                                        txtFMASResultadoAppFinanceirasProgramasProjetos,
                                        txtFMASValoresExecutadosProgramasProjetos,
                                        txtFMASValoresReprogramadosProgramasProjetos,
                                        txtFMASValoresDevolvidosProgramasProjetos,
                                        txtFMASPorcentagensExecucaoProgramasProjetos,

                                        txtPrevisaoInicialReprogramacao,
                                        txtRecursosDisponibilizadosReprogramacao,
                                        txtResultadosAplicacaoReprogramacao,
                                        txtValoresExecutadosReprogramacao,
                                        txtValoresReprogramacao,
                                        
                                        //txtFMASValoresDevolvidosBasica,
                                        //txtFMASValoresDevolvidosMedia,
                                        //txtFMASValoresDevolvidosAlta,
                                        //txtFEASValoresDevolvidosBasica,
                                        //txtFEASValoresDevolvidosMedia,
                                        //txtFEASValoresDevolvidosAlta,
                                        //txtFNASValoresDevolvidosBasica,
                                        //txtFNASValoresDevolvidosMedia,
                                        //txtFNASValoresDevolvidosAlta,
                                        btnFinalizarCalculo };
            #endregion
        }

        void CarregarExecucaoFinanceira(Prefeituras prefeituras)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);
            CarregarLabelsPorExercicio(exercicio);

            var comentarios = prefeituras.GetComentarioExecucaoFinanceira(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (comentarios != null)
            {
                var comentario = comentarios.Where(x => x.Exercicio == Convert.ToInt32(hdfAno.Value)).FirstOrDefault();
                if (comentario != null)
                {
                    txtComentario.Text = comentario.Comentario;
                }
            }

            #region carregamento das Execucoes

            List<ExecucaoFinanceiraInfo> execucoes = prefeituras.GetExecucaoFinanceira(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (execucoes == null) return;

            var execucao = execucoes.Where(x => x.Exercicio == exercicio);
            if (execucao == null) return;


            List<DeliberacaoCMASInfo> deliberacao = prefeituras.GetDeliberacao(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
            var deliberacaoCMAS = deliberacao.FirstOrDefault();
            CarregaDeliberacao(deliberacaoCMAS);

            List<ComentarioExecucaoFinanceiraCMASInfo> CMAS = prefeituras.GetComentarioCMAS(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
            var comentarioCMAS = CMAS.FirstOrDefault();
            CarregaComentarioCMAS(comentarioCMAS);
            
            
            var basica = execucao.FirstOrDefault(e => e.IdTipoProtecao == 1);
            var especialMedia = execucao.FirstOrDefault(e => e.IdTipoProtecao == 2);
            var especialAlta = execucao.FirstOrDefault(e => e.IdTipoProtecao == 3);

            var especialProgramasProjetos = execucao.FirstOrDefault(e => e.IdTipoProtecao == 4);
            var especialBeneficiosEventuais = execucao.FirstOrDefault(e => e.IdTipoProtecao == 5);
            var especialProtecaoSocialEspecial = execucao.FirstOrDefault(e => e.IdTipoProtecao == 6);
            var especialIncentivoGestao = execucao.FirstOrDefault(e => e.IdTipoProtecao == 7);
            var especialExercicioAnterior = execucao.FirstOrDefault(e => e.IdTipoProtecao == 8);

            if (exercicio >= 2020)
            {
                if (basica == null || especialMedia == null || especialAlta == null || especialBeneficiosEventuais == null || especialProgramasProjetos == null || especialProtecaoSocialEspecial == null || especialIncentivoGestao == null || especialExercicioAnterior == null) return;
            }
            else
            {
                if (basica == null || especialMedia == null || especialAlta == null) return;
            }
            if (exercicio >= 2020)
            {
               Session["sessionExecucaoFinanceira"] = execucoes;

                carregarProtecaoBasica(basica);
                carregarProtecaoEspecialMedia(especialMedia);
                carregarProtecaoEspecialAlta(especialAlta);

                carregarProtecaoEspecialBeneficiosEventuais(especialBeneficiosEventuais);
            
                
                carregarProtecaoEspecialProgramasProjetos(especialProgramasProjetos);
                carregarProtecaoIncentivoGestao(especialIncentivoGestao);
                carregarProtecaoSocialEspecial(especialProtecaoSocialEspecial,especialMedia,especialAlta);
                carregarProtecaoExercicioAnterior(especialExercicioAnterior);

                totalizarFMASPrestacaoDeContas(basica, especialMedia, especialAlta, especialBeneficiosEventuais, especialProgramasProjetos);
                totalizarFEAS(basica, especialMedia, especialAlta);
                totalizarFNASPrestacaoDeContas(basica, especialMedia, especialAlta, especialProgramasProjetos, especialIncentivoGestao, especialProtecaoSocialEspecial, especialExercicioAnterior);
                totalizarPrestacaoDeContas(basica, especialMedia, especialAlta, especialBeneficiosEventuais, especialProgramasProjetos, especialIncentivoGestao, especialProtecaoSocialEspecial);
            }
            else
            {
                carregarProtecaoBasica(basica);
                carregarProtecaoEspecialMedia(especialMedia);
                carregarProtecaoEspecialAlta(especialAlta);

                totalizarFMAS(basica, especialMedia, especialAlta);
                totalizarFEAS(basica, especialMedia, especialAlta);
                totalizarFNAS(basica, especialMedia, especialAlta);
                totalizar(basica, especialMedia, especialAlta);
            }
            

            #endregion
        }

        private void CarregarLabelsPorExercicio(int exercicio)
        {
            if (FExecucaoFinanceira.Exercicios[0] == exercicio)
            {
                lblExecucaoDisponibilidade.Text = string.Format("Quadro de execução financeira de {0}.", Exercicios[0]);
                btnExercicio1.CssClass = "btn-seds btn-info-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";

                btnCalcular.Enabled = true;
                btnSalvar.Enabled = true;
                txtComentario.Enabled = true;
                tbInconsistencias.Visible = false;
            }
            if (FExecucaoFinanceira.Exercicios[1] == exercicio)
            {
                lblExecucaoDisponibilidade.Text = string.Format("Quadro de execução financeira de {0}.", Exercicios[1]);
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-info-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";

                btnCalcular.Enabled = true;
                btnSalvar.Enabled = true;
                txtComentario.Enabled = true;
                tbInconsistencias.Visible = false;
            }

            if (FExecucaoFinanceira.Exercicios[2] == exercicio)
            {
                lblExecucaoDisponibilidade.Text = string.Format("O preenchimento deste quadro só estará disponível a partir de janeiro de {0}.", (Exercicios[2] + 1).ToString() );
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-info-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";

                btnCalcular.Enabled = true;
                btnSalvar.Enabled = true;
                txtComentario.Enabled = true;
                tbInconsistencias.Visible = false;
            }

            if (FExecucaoFinanceira.Exercicios[3] == exercicio)
            {
                lblExecucaoDisponibilidade.Text = string.Format("O preenchimento deste quadro só estará disponível a partir de janeiro de {0}.", (Exercicios[3]+ 1).ToString());
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-info-seds";
                                             
                btnCalcular.Enabled = true;
                btnSalvar.Enabled = true;
                txtComentario.Enabled = true;
                tbInconsistencias.Visible = false;
            }
        }

        void CarregaDeliberacao(DeliberacaoCMASInfo d) 
        {
            if (d == null)
            {
                txtDataPublicacao.Text = "";
                txtDataReuniao.Text = "";
                txtNumeroAta.Text = "";
                txtNumeroConselheiros.Text = "";
                txtNumeroResolucao.Text = "";
            }
            else
            {
                txtDataPublicacao.Text = d.DataPublicacao.ToString("dd/MM/yyyy");
                txtDataReuniao.Text = d.DataReuniao.ToString("dd/MM/yyyy");
                txtNumeroAta.Text = d.NumeroAta;
                txtNumeroConselheiros.Text = d.NumeroConselheiros;
                txtNumeroResolucao.Text = d.NumeroResolucao;
            }
        }

        void CarregaComentarioCMAS(ComentarioExecucaoFinanceiraCMASInfo c) 
        {
            if (c == null)
            {
                txtComentario2.Text = "";
            }
            else
            {
                txtComentario2.Text = c.Comentario;
            }

        }

        void carregarProtecaoBasica(ExecucaoFinanceiraInfo e)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            //FMAS
            txtFMASPrevisaoInicialBasica.Text = e.PrevisaoInicialFMAS.ToString("N2");
            txtFMASResultadoAppFinanceirasBasica.Text = e.ResultadoAplicacaoFinanceiraFMAS.ToString("N2");
            txtFMASRecursosDisponibilizadosBasica.Text = e.RecursoDisponibilizadoFMAS.ToString("N2");
            txtFMASValoresExecutadosBasica.Text = e.ValoresExecutadosFMAS.ToString("N2");
            txtFMASValoresReprogramadosBasica.Text = e.ValoresReprogramadosFMAS.ToString("N2");
            txtFMASValoresDevolvidosBasica.Text = e.ValoresDevolvidosFMAS.ToString("N2");

            if (exercicio >= 2020)
            {
                txtFMASPorcentagensExecucaoBasica.Text = e.PorcentagemPrestacaoDeContasFMAS.ToString("P2");
            }
            else
            {
                txtFMASPorcentagensExecucaoBasica.Text = e.PorcentagemDevolucaoFMAS.ToString("P2");
            }

            var ValoresDevolvidosFEASReprogramado = e.ValoresDevolvidosFEASReprogramado.HasValue ? e.ValoresDevolvidosFEASReprogramado.Value : 0M;
            //FEAS
            txtFEASPrevisaoInicialBasica.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasBasica.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASRecursosDisponibilizadosBasica.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASValoresExecutadosBasica.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosBasica.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosBasica.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtValoresDevolvidosReprogramacao.Text = ValoresDevolvidosFEASReprogramado.ToString("N2");
            txtFEASPorcentagensExecucaoBasica.Text = e.PorcentagemDevolucaoFEAS.ToString("P2");
            txtPorcentagensDevolucaoReprogramacao.Text = e.PorcentagemDevolucaoFEASReprogramado.ToString("P2");
            txtPrevisaoInicialReprogramacao.Text = e.PrevisaoInicialFEASReprogramado.HasValue ? e.PrevisaoInicialFEASReprogramado.Value.ToString("N2") : "0,00";
            txtRecursosDisponibilizadosReprogramacao.Text = e.RecursoDisponibilizadoFEASReprogramado.HasValue ? e.RecursoDisponibilizadoFEASReprogramado.Value.ToString("N2") : "0,00";
            txtResultadosAplicacaoReprogramacao.Text = e.ResultadoAplicacaoFinanceiraFEASReprogramado.HasValue ? e.ResultadoAplicacaoFinanceiraFEASReprogramado.Value.ToString("N2") : "0,00";
            txtValoresExecutadosReprogramacao.Text = e.ValoresExecutadosFEASReprogramado.HasValue ? e.ValoresExecutadosFEASReprogramado.Value.ToString("N2") : "0,00";
            txtValoresReprogramacao.Text = "0,00";

            //FNAS
            txtFNASPrevisaoInicialBasica.Text = e.PrevisaoInicialFNAS.ToString("N2");
            txtFNASResultadoAppFinanceirasBasica.Text = e.ResultadoAplicacaoFinanceiraFNAS.ToString("N2");
            txtFNASRecursosDisponibilizadosBasica.Text = e.RecursoDisponibilizadoFNAS.ToString("N2");
            txtFNASValoresExecutadosBasica.Text = e.ValoresExecutadosFNAS.ToString("N2");
            txtFNASValoresReprogramadosBasica.Text = e.ValoresReprogramadosFNAS.ToString("N2");
            txtFNASValoresDevolvidosBasica.Text = e.ValoresDevolvidosFNAS.ToString("N2");

            if (exercicio >= 2020)
            {
                txtFNASPorcentagensExecucaoBasica.Text = e.PorcentagemPrestacaoDeContasFNAS.ToString("P2");
            }
            else
            {
                txtFNASPorcentagensExecucaoBasica.Text = e.PorcentagemDevolucaoFNAS.ToString("P2");        
            }
           
            var RecursoDisponibilizadoFEASReprogramado = e.RecursoDisponibilizadoFEASReprogramado.HasValue ? e.RecursoDisponibilizadoFEASReprogramado.Value : 0M;

            var PrevisaoInicialFEASReprogramado = e.PrevisaoInicialFEASReprogramado.HasValue ? e.PrevisaoInicialFEASReprogramado.Value : 0M;
            var ResultadoAplicacaoFinanceiraReprogramado = e.ResultadoAplicacaoFinanceiraFEASReprogramado.HasValue ? e.ResultadoAplicacaoFinanceiraFEASReprogramado.Value : 0M;
            var valoresExecutadosFEASReprogramado = e.ValoresDevolvidosFEASReprogramado.HasValue ? e.ValoresDevolvidosFEASReprogramado.Value : 0M;
            //TOTAL

            txtPrevisaoInicialBasica.Text = (e.PrevisaoInicialFMAS + e.PrevisaoInicialFNAS).ToString("N2");
            var resultadoAppFinanceira = (e.ResultadoAplicacaoFinanceiraFMAS + e.ResultadoAplicacaoFinanceiraFNAS);
            txtResultadoAppFinanceirasBasica.Text = resultadoAppFinanceira.ToString("N2");
            
            var recursosDisponibilizados = e.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFNAS;
            txtRecursosDisponibilizadosBasica.Text = recursosDisponibilizados.ToString("N2");

            txtValoresExecutadosBasica.Text = (e.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS).ToString("N2");
            txtValoresReprogramadosBasica.Text = (e.ValoresReprogramadosFMAS + e.ValoresReprogramadosFNAS).ToString("N2");
            txtValoresDevolvidosBasica.Text = (e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFNAS).ToString("N2");

            if (exercicio >= 2020)
            {
                txtPorcentagensExecucaoBasica.Text = ((e.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
            }
            else
            {
                txtPorcentagensExecucaoBasica.Text = ((e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
            }
        }

        void carregarProtecaoEspecialMedia(ExecucaoFinanceiraInfo e)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            //FMAS
            txtFMASPrevisaoInicialMedia.Text = e.PrevisaoInicialFMAS.ToString("N2");
            txtFMASResultadoAppFinanceirasMedia.Text = e.ResultadoAplicacaoFinanceiraFMAS.ToString("N2");
            txtFMASRecursosDisponibilizadosMedia.Text = e.RecursoDisponibilizadoFMAS.ToString("N2");
            txtFMASValoresExecutadosMedia.Text = e.ValoresExecutadosFMAS.ToString("N2");
            txtFMASValoresReprogramadosMedia.Text = e.ValoresReprogramadosFMAS.ToString("N2");
            txtFMASValoresDevolvidosMedia.Text = e.ValoresDevolvidosFMAS.ToString("N2");

            if (exercicio >= 2020)
            {
                txtFMASPorcentagensExecucaoMedia.Text = e.PorcentagemPrestacaoDeContasFMAS.ToString("P2");
            }
            else
            {
                txtFMASPorcentagensExecucaoMedia.Text = e.PorcentagemDevolucaoFMAS.ToString("P2");
            }
            

            //FEAS
            txtFEASPrevisaoInicialMedia.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasMedia.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASRecursosDisponibilizadosMedia.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASValoresExecutadosMedia.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosMedia.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosMedia.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoMedia.Text = e.PorcentagemDevolucaoFEAS.ToString("P2");

            //FNAS
            txtFNASPrevisaoInicialMedia.Text = e.PrevisaoInicialFNAS.ToString("N2");
            txtFNASResultadoAppFinanceirasMedia.Text = e.ResultadoAplicacaoFinanceiraFNAS.ToString("N2");
            txtFNASRecursosDisponibilizadosMedia.Text = e.RecursoDisponibilizadoFNAS.ToString("N2");
            txtFNASValoresExecutadosMedia.Text = e.ValoresExecutadosFNAS.ToString("N2");
            txtFNASValoresReprogramadosMedia.Text = e.ValoresReprogramadosFNAS.ToString("N2");
            txtFNASValoresDevolvidosMedia.Text = e.ValoresDevolvidosFNAS.ToString("N2");

            if (exercicio >= 2020)
            {
                txtFNASPorcentagensExecucaoMedia.Text = e.PorcentagemPrestacaoDeContasFNAS.ToString("P2");
            }
            else
            {
                txtFNASPorcentagensExecucaoMedia.Text = e.PorcentagemDevolucaoFNAS.ToString("P2");
            }
            

            //TOTAL
            txtPrevisaoInicialMedia.Text = (e.PrevisaoInicialFMAS + e.PrevisaoInicialFEAS + e.PrevisaoInicialFNAS).ToString("N2");
            var resultadoAppFinanceira = (e.ResultadoAplicacaoFinanceiraFMAS + e.ResultadoAplicacaoFinanceiraFEAS + e.ResultadoAplicacaoFinanceiraFNAS);
            txtResultadoAppFinanceirasMedia.Text = resultadoAppFinanceira.ToString("N2");
            var recursosDisponibilizados = e.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFEAS + e.RecursoDisponibilizadoFNAS;
            txtRecursosDisponibilizadosMedia.Text = recursosDisponibilizados.ToString("N2");
            txtValoresExecutadosMedia.Text = (e.ValoresExecutadosFMAS + e.ValoresExecutadosFEAS + e.ValoresExecutadosFNAS).ToString("N2");
            txtValoresReprogramadosMedia.Text = (e.ValoresReprogramadosFMAS + e.ValoresReprogramadosFEAS + e.ValoresReprogramadosFNAS).ToString("N2");
            txtValoresDevolvidosMedia.Text = (e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFEAS + e.ValoresDevolvidosFNAS).ToString("N2");

            if (exercicio >= 2020)
            {
                txtPorcentagensExecucaoMedia.Text = ((e.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
            }
            else
            {
                txtPorcentagensExecucaoMedia.Text = ((e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFEAS + e.ValoresDevolvidosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
            }

            

        }

        void carregarProtecaoEspecialAlta(ExecucaoFinanceiraInfo e)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);
            //FMAS
            txtFMASPrevisaoInicialAlta.Text = e.PrevisaoInicialFMAS.ToString("N2");
            txtFMASResultadoAppFinanceirasAlta.Text = e.ResultadoAplicacaoFinanceiraFMAS.ToString("N2");
            txtFMASRecursosDisponibilizadosAlta.Text = e.RecursoDisponibilizadoFMAS.ToString("N2");
            txtFMASValoresExecutadosAlta.Text = e.ValoresExecutadosFMAS.ToString("N2");
            txtFMASValoresReprogramadosAlta.Text = e.ValoresReprogramadosFMAS.ToString("N2");
            txtFMASValoresDevolvidosAlta.Text = e.ValoresDevolvidosFMAS.ToString("N2");
            txtFMASPorcentagensExecucaoAlta.Text = e.PorcentagemDevolucaoPrestacaoDeContasFMAS.ToString("P2");

            //FEAS
            txtFEASPrevisaoInicialAlta.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasAlta.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASRecursosDisponibilizadosAlta.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASValoresExecutadosAlta.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosAlta.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosAlta.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoAlta.Text = e.PorcentagemDevolucaoFEAS.ToString("P2");

            //FNAS
            txtFNASPrevisaoInicialAlta.Text = e.PrevisaoInicialFNAS.ToString("N2");
            txtFNASResultadoAppFinanceirasAlta.Text = e.ResultadoAplicacaoFinanceiraFNAS.ToString("N2");
            txtFNASRecursosDisponibilizadosAlta.Text = e.RecursoDisponibilizadoFNAS.ToString("N2");
            txtFNASValoresExecutadosAlta.Text = e.ValoresExecutadosFNAS.ToString("N2");
            txtFNASValoresReprogramadosAlta.Text = e.ValoresReprogramadosFNAS.ToString("N2");
            txtFNASValoresDevolvidosAlta.Text = e.ValoresDevolvidosFNAS.ToString("N2");
            txtFNASPorcentagensExecucaoAlta.Text = e.PorcentagemDevolucaoFNAS.ToString("P2");

            //TOTAL
            txtPrevisaoInicialAlta.Text = (e.PrevisaoInicialFMAS + e.PrevisaoInicialFEAS + e.PrevisaoInicialFNAS).ToString("N2");
            var resultadoAppFinanceira = (e.ResultadoAplicacaoFinanceiraFMAS + e.ResultadoAplicacaoFinanceiraFEAS + e.ResultadoAplicacaoFinanceiraFNAS);
            txtResultadoAppFinanceirasAlta.Text = resultadoAppFinanceira.ToString("N2");
            var recursosDisponibilizados = e.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFEAS + e.RecursoDisponibilizadoFNAS;
            txtRecursosDisponibilizadosAlta.Text = recursosDisponibilizados.ToString("N2");
            txtValoresExecutadosAlta.Text = (e.ValoresExecutadosFMAS + e.ValoresExecutadosFEAS + e.ValoresExecutadosFNAS).ToString("N2");
            txtValoresReprogramadosAlta.Text = (e.ValoresReprogramadosFMAS + e.ValoresReprogramadosFEAS + e.ValoresReprogramadosFNAS).ToString("N2");
            txtValoresDevolvidosAlta.Text = (e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFEAS + e.ValoresDevolvidosFNAS).ToString("N2");
            txtPorcentagensExecucaoAlta.Text = ((e.ValoresDevolvidosFMAS  + e.ValoresDevolvidosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
        }

        void carregarProtecaoEspecialBeneficiosEventuais(ExecucaoFinanceiraInfo e)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            #region FMAS
            txtFMASPrevisaoInicialBeneficiosEventuais.Text = e.PrevisaoInicialFMAS.ToString("N2");
            txtFMASResultadoAppFinanceirasBeneficiosEventuais.Text = e.ResultadoAplicacaoFinanceiraFMAS.ToString("N2");
            txtFMASRecursosDisponibilizadosBeneficiosEventuais.Text = e.RecursoDisponibilizadoFMAS.ToString("N2");
            txtFMASValoresExecutadosBeneficiosEventuais.Text = e.ValoresExecutadosFMAS.ToString("N2");
            txtFMASValoresReprogramadosBeneficiosEventuais.Text = e.ValoresReprogramadosFMAS.ToString("N2");
            txtFMASValoresDevolvidosBeneficiosEventuais.Text = e.ValoresDevolvidosFMAS.ToString("N2");

            if (exercicio >= 2020)
            {
                txtFMASPorcentagensExecucaoBeneficiosEventuais.Text = e.PorcentagemPrestacaoDeContasFMAS.ToString("P2");
            }
            else
            {
                txtFMASPorcentagensExecucaoBeneficiosEventuais.Text = e.PorcentagemDevolucaoFMAS.ToString("P2");
            }

            

            #endregion

            #region Total

            txtTotalPrevisaoInicialBeneficiosEventuais.Text = (e.PrevisaoInicialFMAS + e.PrevisaoInicialFNAS).ToString("N2");
            var resultadoAppFinanceira = (e.ResultadoAplicacaoFinanceiraFMAS + e.ResultadoAplicacaoFinanceiraFNAS);
            txtTotalResultadoAppFinanceirasBeneficiosEventuais.Text = resultadoAppFinanceira.ToString("N2");
            var recursosDisponibilizados = e.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFNAS;
            txtTotalRecursosDisponibilizadosBeneficiosEventuais.Text = recursosDisponibilizados.ToString("N2");
            txtTotalValoresExecutadosBeneficiosEventuais.Text = (e.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS).ToString("N2");
            txtTotalValoresReprogramadosBeneficiosEventuais.Text = (e.ValoresReprogramadosFMAS + e.ValoresReprogramadosFNAS).ToString("N2");
            txtTotalValoresDevolvidosBeneficiosEventuais.Text = (e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFNAS).ToString("N2");

            if (exercicio >= 2020)
            {
                txtTotalPorcentagensExecucaoBeneficiosEventuais.Text = ((e.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
            }
            else
            {
                txtTotalPorcentagensExecucaoBeneficiosEventuais.Text = ((e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFEAS + e.ValoresDevolvidosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
            }

            

            #endregion


        }

        void carregarProtecaoEspecialProgramasProjetos(ExecucaoFinanceiraInfo e)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            #region FMAS
            txtFMASPrevisaoInicialProgramasProjetos.Text = e.PrevisaoInicialFMAS.ToString("N2");
            txtFMASResultadoAppFinanceirasProgramasProjetos.Text = e.ResultadoAplicacaoFinanceiraFMAS.ToString("N2");
            txtFMASRecursosDisponibilizadosProgramasProjetos.Text = e.RecursoDisponibilizadoFMAS.ToString("N2");
            txtFMASValoresExecutadosProgramasProjetos.Text = e.ValoresExecutadosFMAS.ToString("N2");
            txtFMASValoresReprogramadosProgramasProjetos.Text = e.ValoresReprogramadosFMAS.ToString("N2");
            txtFMASValoresDevolvidosProgramasProjetos.Text = e.ValoresDevolvidosFMAS.ToString("N2");
            if (exercicio >= 2020)
            {
                txtFMASPorcentagensExecucaoProgramasProjetos.Text = e.PorcentagemPrestacaoDeContasFMAS.ToString("P2");
            }
            else
            {
                txtFMASPorcentagensExecucaoProgramasProjetos.Text = e.PorcentagemDevolucaoFMAS.ToString("P2");
            }
            
            #endregion

            #region FNAS
            txtFNASPrevisaoInicialProgramasProjetos.Text = e.PrevisaoInicialFNAS.ToString("N2");
            txtFNASResultadoAppFinanceirasProgramasProjetos.Text = e.ResultadoAplicacaoFinanceiraFNAS.ToString("N2");
            txtFNASRecursosDisponibilizadosProgramasProjetos.Text = e.RecursoDisponibilizadoFNAS.ToString("N2");
            txtFNASValoresExecutadosProgramasProjetos.Text = e.ValoresExecutadosFNAS.ToString("N2");
            txtFNASValoresReprogramadosProgramasProjetos.Text = e.ValoresReprogramadosFNAS.ToString("N2");
            txtFNASValoresDevolvidosProgramasProjetos.Text = e.ValoresDevolvidosFNAS.ToString("N2");

            if (exercicio >= 2020)
            {
                txtFNASPorcentagensExecucaoProgramasProjetos.Text = e.PorcentagemPrestacaoDeContasFNAS.ToString("P2");
            }
            else
            {
                txtFNASPorcentagensExecucaoProgramasProjetos.Text = e.PorcentagemDevolucaoFNAS.ToString("P2");
            }
            
            #endregion

            #region TOTAIS
            txtTotalPrevisaoInicialProgramasProjetos.Text = (e.PrevisaoInicialFMAS  + e.PrevisaoInicialFNAS).ToString("N2");
            var resultadoAppFinanceira = (e.ResultadoAplicacaoFinanceiraFMAS + e.ResultadoAplicacaoFinanceiraFNAS);
            txtTotalResultadoAppFinanceirasProgramasProjetos.Text = resultadoAppFinanceira.ToString("N2");
            var recursosDisponibilizados = e.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFNAS;
            txtTotalRecursosDisponibilizadosProgramasProjetos.Text = recursosDisponibilizados.ToString("N2");
            txtTotalValoresExecutadosProgramasProjetos.Text = (e.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS).ToString("N2");
            txtTotalValoresReprogramadosProgramasProjetos.Text = (e.ValoresReprogramadosFMAS  + e.ValoresReprogramadosFNAS).ToString("N2");
            txtTotalValoresDevolvidosProgramasProjetos.Text = (e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFNAS).ToString("N2");

            if (exercicio >= 2020)
            {
                txtTotalPorcentagensExecucaoProgramasProjetos.Text = ((e.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");    
            }
            else
            {
                txtTotalPorcentagensExecucaoProgramasProjetos.Text = ((e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFEAS + e.ValoresDevolvidosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
            }

            #endregion

        }
        
        void carregarProtecaoIncentivoGestao(ExecucaoFinanceiraInfo e)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            #region FNAS
            txtFNASPrevisaoInicialIncentivoGestao.Text = e.PrevisaoInicialFNAS.ToString("N2");
            txtFNASResultadoAppFinanceirasIncentivoGestao.Text = e.ResultadoAplicacaoFinanceiraFNAS.ToString("N2");
            txtFNASRecursosDisponibilizadosIncentivoGestao.Text = e.RecursoDisponibilizadoFNAS.ToString("N2");
            txtFNASValoresExecutadosIncentivoGestao.Text = e.ValoresExecutadosFNAS.ToString("N2");
            txtFNASValoresReprogramadosIncentivoGestao.Text = e.ValoresReprogramadosFNAS.ToString("N2");
            txtFNASValoresDevolvidosIncentivoGestao.Text = e.ValoresDevolvidosFNAS.ToString("N2");

            if (exercicio >= 2020)
            {
                txtFNASPorcentagensExecucaoIncentivoGestao.Text = e.PorcentagemPrestacaoDeContasFNAS.ToString("P2");
            }
            else
            {
                txtFNASPorcentagensExecucaoIncentivoGestao.Text = e.PorcentagemDevolucaoFNAS.ToString("P2");
            }
            
            #endregion

            #region Total

            txtTotalPrevisaoInicialIncentivoGestao.Text = (e.PrevisaoInicialFMAS + e.PrevisaoInicialFEAS + e.PrevisaoInicialFNAS).ToString("N2");
            var resultadoAppFinanceira = (e.ResultadoAplicacaoFinanceiraFMAS + e.ResultadoAplicacaoFinanceiraFEAS + e.ResultadoAplicacaoFinanceiraFNAS);
            txtTotalResultadoAppFinanceirasIncentivoGestao.Text = resultadoAppFinanceira.ToString("N2");
            var recursosDisponibilizados = e.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFEAS + e.RecursoDisponibilizadoFNAS;
            txtTotalRecursosDisponibilizadosIncentivoGestao.Text = recursosDisponibilizados.ToString("N2");
            txtTotalValoresExecutadosIncentivoGestao.Text = (e.ValoresExecutadosFMAS + e.ValoresExecutadosFEAS + e.ValoresExecutadosFNAS).ToString("N2");
            txtTotalValoresReprogramadosIncentivoGestao.Text = (e.ValoresReprogramadosFMAS + e.ValoresReprogramadosFEAS + e.ValoresReprogramadosFNAS).ToString("N2");
            txtTotalValoresDevolvidosIncentivoGestao.Text = (e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFEAS + e.ValoresDevolvidosFNAS).ToString("N2");

            if (exercicio >= 2020)
            {
                txtTotalPorcentagensExecucaoIncentivoGestao.Text = e.PorcentagemPrestacaoDeContasFNAS.ToString("P2");
            }
            else
            {
                txtTotalPorcentagensExecucaoIncentivoGestao.Text = ((e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFEAS + e.ValoresDevolvidosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
            }
            

            #endregion

        }
        
        void carregarProtecaoSocialEspecial(ExecucaoFinanceiraInfo e, ExecucaoFinanceiraInfo m,ExecucaoFinanceiraInfo a)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            #region FNAS
            txtFNASPrevisaoInicialProtecaoSocialEspecial.Text = e.PrevisaoInicialFNAS.ToString("N2");
            txtFNASResultadoAppFinanceirasProtecaoSocialEspecial.Text = e.ResultadoAplicacaoFinanceiraFNAS.ToString("N2");
            txtFNASRecursosDisponibilizadosProtecaoSocialEspecial.Text = e.RecursoDisponibilizadoFNAS.ToString("N2");
            txtFNASValoresExecutadosProtecaoSocialEspecial.Text = e.ValoresExecutadosFNAS.ToString("N2");
            txtFNASValoresReprogramadosProtecaoSocialEspecial.Text = e.ValoresReprogramadosFNAS.ToString("N2");
            txtFNASValoresDevolvidosProtecaoSocialEspecial.Text = e.ValoresDevolvidosFNAS.ToString("N2");

            if (exercicio >= 2020)
            {
                txtFNASPorcentagensExecucaoProtecaoSocialEspecial.Text = e.PorcentagemPrestacaoDeContasFNAS.ToString("P2");
            }
            else
            {
                txtFNASPorcentagensExecucaoProtecaoSocialEspecial.Text = e.PorcentagemDevolucaoFNAS.ToString("P2");
            }


            #endregion

            #region Total

            txtTotalPrevisaoInicialProtecaoSocialEspecial.Text = (e.PrevisaoInicialFMAS +m.PrevisaoInicialFMAS + a.PrevisaoInicialFMAS + e.PrevisaoInicialFNAS).ToString("N2");
            var resultadoAppFinanceira = (e.ResultadoAplicacaoFinanceiraFMAS +m.ResultadoAplicacaoFinanceiraFMAS+a.ResultadoAplicacaoFinanceiraFMAS+ e.ResultadoAplicacaoFinanceiraFNAS);
            txtTotalResultadoAppFinanceirasProtecaoSocialEspecial.Text = resultadoAppFinanceira.ToString("N2");
            var recursosDisponibilizados = e.RecursoDisponibilizadoFMAS + m.RecursoDisponibilizadoFMAS+ a.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFNAS;
            txtTotalRecursosDisponibilizadosProtecaoSocialEspecial.Text = recursosDisponibilizados.ToString("N2");
            txtTotalValoresExecutadosProtecaoSocialEspecial.Text = (e.ValoresExecutadosFMAS + m.ValoresExecutadosFMAS + a.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS).ToString("N2");
            txtTotalValoresReprogramadosProtecaoSocialEspecial.Text = (e.ValoresReprogramadosFMAS + m.ValoresReprogramadosFMAS + a.ValoresReprogramadosFMAS + e.ValoresReprogramadosFNAS).ToString("N2");
            txtTotalValoresDevolvidosProtecaoSocialEspecial.Text = (e.ValoresDevolvidosFMAS + m.ValoresDevolvidosFMAS + a.ValoresDevolvidosFMAS + e.ValoresDevolvidosFNAS).ToString("N2");
            
            if (exercicio >= 2020)
            {
                txtTotalPorcentagensExecucaoProtecaoSocialEspecial.Text = ((e.ValoresExecutadosFMAS + m.ValoresExecutadosFMAS + a.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
            }
            else
            {
                txtTotalPorcentagensExecucaoProtecaoSocialEspecial.Text = ((e.ValoresDevolvidosFMAS + m.ValoresDevolvidosFMAS + a.ValoresDevolvidosFMAS + e.ValoresDevolvidosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
            }

            #endregion
        }
        
        void carregarProtecaoExercicioAnterior(ExecucaoFinanceiraInfo e)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            #region FNAS
            txtFNASPrevisaoInicialExercicioAnterior.Text = e.PrevisaoInicialFNAS.ToString("N2");
            txtFNASResultadoAppFinanceirasExercicioAnterior.Text = e.ResultadoAplicacaoFinanceiraFNAS.ToString("N2");
            txtFNASRecursosDisponibilizadosExercicioAnterior.Text = e.RecursoDisponibilizadoFNAS.ToString("N2");
            txtFNASValoresExecutadosExercicioAnterior.Text = e.ValoresExecutadosFNAS.ToString("N2");
            txtFNASValoresReprogramadosExercicioAnterior.Text = e.ValoresReprogramadosFNAS.ToString("N2");
            txtFNASValoresDevolvidosExercicioAnterior.Text = e.ValoresDevolvidosFNAS.ToString("N2");

            if (exercicio >= 2020)
            {
                txtFNASPorcentagensExecucaoExercicioAnterior.Text = e.PorcentagemPrestacaoDeContasFNAS.ToString("P2");
            }
            else
            {
                txtFNASPorcentagensExecucaoExercicioAnterior.Text = e.PorcentagemDevolucaoFNAS.ToString("P2");
            }
            #endregion
        }


        void totalizarFMAS(ExecucaoFinanceiraInfo basica, ExecucaoFinanceiraInfo especialMedia, ExecucaoFinanceiraInfo especialAlta)
        {
            lblFMASPrevisaoInicial.Text = (basica.PrevisaoInicialFMAS + especialMedia.PrevisaoInicialFMAS + especialAlta.PrevisaoInicialFMAS).ToString("N2");
            var recursosDisponibilizados = basica.RecursoDisponibilizadoFMAS + especialMedia.RecursoDisponibilizadoFMAS + especialAlta.RecursoDisponibilizadoFMAS;
            lblFMASRecursosDisponibilizados.Text = recursosDisponibilizados.ToString("N2");
            var resultadoAppFinanceira = (basica.ResultadoAplicacaoFinanceiraFMAS + especialMedia.ResultadoAplicacaoFinanceiraFMAS + especialAlta.ResultadoAplicacaoFinanceiraFMAS);
            lblFMASResultadoAppFinanceiras.Text = resultadoAppFinanceira.ToString("N2");
            lblFMASValoresExecutados.Text = (basica.ValoresExecutadosFMAS + especialMedia.ValoresExecutadosFMAS + especialAlta.ValoresExecutadosFMAS).ToString("N2");
            lblFMASValoresReprogramados.Text = (basica.ValoresReprogramadosFMAS + especialMedia.ValoresReprogramadosFMAS + especialAlta.ValoresReprogramadosFMAS).ToString("N2");
            lblFMASValoresDevolvidos.Text = (basica.ValoresDevolvidosFMAS + especialMedia.ValoresDevolvidosFMAS + especialAlta.ValoresDevolvidosFMAS).ToString("N2");
            lblFMASPorcentagensExecucao.Text = ((basica.ValoresDevolvidosFMAS + especialMedia.ValoresDevolvidosFMAS + especialAlta.ValoresDevolvidosFMAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
        }

        void totalizarFEAS(ExecucaoFinanceiraInfo basica, ExecucaoFinanceiraInfo especialMedia, ExecucaoFinanceiraInfo especialAlta)
        {
            var PrevisaoInicialFEASReprogramado = basica.PrevisaoInicialFEASReprogramado.HasValue ? basica.PrevisaoInicialFEASReprogramado.Value : 0M;
            var RecursoDisponibilizadoFEASReprogramado = basica.RecursoDisponibilizadoFEASReprogramado.HasValue ? basica.RecursoDisponibilizadoFEASReprogramado.Value : 0M;
            var ResultadoAplicacoesFEASReprogramado = basica.ResultadoAplicacaoFinanceiraFEASReprogramado.HasValue ? basica.ResultadoAplicacaoFinanceiraFEASReprogramado.Value : 0M;
            var ValoresExecutadosFEASReprogramado = basica.ValoresExecutadosFEASReprogramado.HasValue ? basica.ValoresExecutadosFEASReprogramado.Value : 0M;
            var ValoresDevolvidosFEASReprogramado = basica.ValoresDevolvidosFEASReprogramado.HasValue ? basica.ValoresDevolvidosFEASReprogramado.Value : 0M;

            lblFEASPrevisaoInicial.Text = (basica.PrevisaoInicialFEAS + PrevisaoInicialFEASReprogramado + especialMedia.PrevisaoInicialFEAS + especialAlta.PrevisaoInicialFEAS).ToString("N2");
            var recursosDisponibilizados = basica.RecursoDisponibilizadoFEAS + RecursoDisponibilizadoFEASReprogramado + especialMedia.RecursoDisponibilizadoFEAS + especialAlta.RecursoDisponibilizadoFEAS;
            lblFEASRecursosDisponibilizados.Text = recursosDisponibilizados.ToString("N2");
            var resultadoAppFinanceira = (basica.ResultadoAplicacaoFinanceiraFEAS + ResultadoAplicacoesFEASReprogramado + especialMedia.ResultadoAplicacaoFinanceiraFEAS + especialAlta.ResultadoAplicacaoFinanceiraFEAS);
            lblFEASResultadoAppFinanceiras.Text = resultadoAppFinanceira.ToString("N2");
            lblFEASValoresExecutados.Text = (basica.ValoresExecutadosFEAS + ValoresExecutadosFEASReprogramado + especialMedia.ValoresExecutadosFEAS + especialAlta.ValoresExecutadosFEAS).ToString("N2");
            lblFEASValoresReprogramados.Text = (basica.ValoresReprogramadosFEAS + especialMedia.ValoresReprogramadosFEAS + especialAlta.ValoresReprogramadosFEAS).ToString("N2");
            lblFEASValoresDevolvidos.Text = (basica.ValoresDevolvidosFEAS + ValoresDevolvidosFEASReprogramado + especialMedia.ValoresDevolvidosFEAS + especialAlta.ValoresDevolvidosFEAS).ToString("N2");
            lblFEASPorcentagensExecucao.Text = ((basica.ValoresDevolvidosFEAS + ValoresDevolvidosFEASReprogramado + especialMedia.ValoresDevolvidosFEAS + especialAlta.ValoresDevolvidosFEAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
        }

        void totalizarFNAS(ExecucaoFinanceiraInfo basica, ExecucaoFinanceiraInfo especialMedia, ExecucaoFinanceiraInfo especialAlta)
        {
            lblFNASPrevisaoInicial.Text = (basica.PrevisaoInicialFNAS + especialMedia.PrevisaoInicialFNAS + especialAlta.PrevisaoInicialFNAS).ToString("N2");
            var recursosDisponibilizados = basica.RecursoDisponibilizadoFNAS + especialMedia.RecursoDisponibilizadoFNAS + especialAlta.RecursoDisponibilizadoFNAS;
            lblFNASRecursosDisponibilizados.Text = recursosDisponibilizados.ToString("N2");
            var resultadoAppFinanceira = (basica.ResultadoAplicacaoFinanceiraFNAS + especialMedia.ResultadoAplicacaoFinanceiraFNAS + especialAlta.ResultadoAplicacaoFinanceiraFNAS);
            lblFNASResultadoAppFinanceiras.Text = resultadoAppFinanceira.ToString("N2");
            lblFNASValoresExecutados.Text = (basica.ValoresExecutadosFNAS + especialMedia.ValoresExecutadosFNAS + especialAlta.ValoresExecutadosFNAS).ToString("N2");
            lblFNASValoresReprogramados.Text = (basica.ValoresReprogramadosFNAS + especialMedia.ValoresReprogramadosFNAS + especialAlta.ValoresReprogramadosFNAS).ToString("N2");
            lblFNASValoresDevolvidos.Text = (basica.ValoresDevolvidosFNAS + especialMedia.ValoresDevolvidosFNAS + especialAlta.ValoresDevolvidosFNAS).ToString("N2");
            lblFNASPorcentagensExecucao.Text = ((basica.ValoresDevolvidosFNAS + especialMedia.ValoresDevolvidosFNAS + especialAlta.ValoresDevolvidosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
        }

        void totalizar(ExecucaoFinanceiraInfo basica, ExecucaoFinanceiraInfo especialMedia, ExecucaoFinanceiraInfo especialAlta)
        {
            var lst = new[] { basica, especialMedia, especialAlta };
            var previsaoInicial = lst.Sum(e => e.PrevisaoInicialFMAS + e.PrevisaoInicialFEAS + e.PrevisaoInicialFNAS + (e.PrevisaoInicialFEASReprogramado.HasValue ? e.PrevisaoInicialFEASReprogramado.Value : 0));
            lblPrevisaoInicial.Text = previsaoInicial.ToString("N2");
            var recursosDisponibilizados = lst.Sum(e => e.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFEAS + e.RecursoDisponibilizadoFNAS + (e.RecursoDisponibilizadoFEASReprogramado.HasValue ? e.RecursoDisponibilizadoFEASReprogramado.Value : 0));
            lblRecursosDisponibilizados.Text = recursosDisponibilizados.ToString("N2");
            var resultadoAppFinanceira = lst.Sum(e => e.ResultadoAplicacaoFinanceiraFMAS + e.ResultadoAplicacaoFinanceiraFEAS + e.ResultadoAplicacaoFinanceiraFNAS + (e.ResultadoAplicacaoFinanceiraFEASReprogramado.HasValue ? e.ResultadoAplicacaoFinanceiraFEASReprogramado.Value : 0));
            lblResultadoAppFinanceiras.Text = resultadoAppFinanceira.ToString("N2");
            lblValoresExecutados.Text = lst.Sum(e => e.ValoresExecutadosFMAS + e.ValoresExecutadosFEAS + e.ValoresExecutadosFNAS + (e.ValoresExecutadosFEASReprogramado.HasValue ? e.ValoresExecutadosFEASReprogramado.Value : 0)).ToString("N2");
            lblValoresReprogramados.Text = lst.Sum(e => e.ValoresReprogramadosFMAS + e.ValoresReprogramadosFEAS + e.ValoresReprogramadosFNAS).ToString("N2");
            lblValoresDevolvidos.Text = lst.Sum(e => e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFEAS + (e.ValoresDevolvidosFEASReprogramado.HasValue ? e.ValoresDevolvidosFEASReprogramado.Value : 0) + e.ValoresDevolvidosFNAS).ToString("N2");
            lblPorcentagensExecucao.Text = (lst.Sum(e => e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFEAS + e.ValoresDevolvidosFNAS + (e.ValoresDevolvidosFEASReprogramado.HasValue ? e.ValoresDevolvidosFEASReprogramado.Value : 0)) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
        }

        void totalizarFMASPrestacaoDeContas(ExecucaoFinanceiraInfo basica, ExecucaoFinanceiraInfo especialMedia, ExecucaoFinanceiraInfo especialAlta, ExecucaoFinanceiraInfo especialBeneficiosEventuais, ExecucaoFinanceiraInfo especialProgramasProjetos)
        {
            lblFMASPrevisaoInicial.Text = (basica.PrevisaoInicialFMAS + especialMedia.PrevisaoInicialFMAS + especialAlta.PrevisaoInicialFMAS + especialBeneficiosEventuais.PrevisaoInicialFMAS + especialProgramasProjetos.PrevisaoInicialFMAS).ToString("N2");

            var recursosDisponibilizados = basica.RecursoDisponibilizadoFMAS + especialMedia.RecursoDisponibilizadoFMAS + especialAlta.RecursoDisponibilizadoFMAS + especialBeneficiosEventuais.RecursoDisponibilizadoFMAS + especialProgramasProjetos.RecursoDisponibilizadoFMAS;
            lblFMASRecursosDisponibilizados.Text = recursosDisponibilizados.ToString("N2");

            var resultadoAppFinanceira = (basica.ResultadoAplicacaoFinanceiraFMAS + especialMedia.ResultadoAplicacaoFinanceiraFMAS + especialAlta.ResultadoAplicacaoFinanceiraFMAS + especialBeneficiosEventuais.ResultadoAplicacaoFinanceiraFMAS + especialProgramasProjetos.ResultadoAplicacaoFinanceiraFMAS);
            lblFMASResultadoAppFinanceiras.Text = resultadoAppFinanceira.ToString("N2");

            lblFMASValoresExecutados.Text = (basica.ValoresExecutadosFMAS + especialMedia.ValoresExecutadosFMAS + especialAlta.ValoresExecutadosFMAS + especialBeneficiosEventuais.ValoresExecutadosFMAS + especialProgramasProjetos.ValoresExecutadosFMAS).ToString("N2");

            lblFMASValoresReprogramados.Text = (basica.ValoresReprogramadosFMAS + especialMedia.ValoresReprogramadosFMAS + especialAlta.ValoresReprogramadosFMAS + especialBeneficiosEventuais.ValoresReprogramadosFMAS + especialProgramasProjetos.ValoresReprogramadosFMAS).ToString("N2");

            lblFMASValoresDevolvidos.Text = (basica.ValoresDevolvidosFMAS + especialMedia.ValoresDevolvidosFMAS + especialAlta.ValoresDevolvidosFMAS + especialBeneficiosEventuais.ValoresDevolvidosFMAS + especialProgramasProjetos.ValoresDevolvidosFMAS).ToString("N2");

            lblFMASPorcentagensExecucao.Text = ((basica.ValoresExecutadosFMAS + especialMedia.ValoresExecutadosFMAS + especialAlta.ValoresExecutadosFMAS + especialBeneficiosEventuais.ValoresExecutadosFMAS + especialProgramasProjetos.ValoresExecutadosFMAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
        }

        void totalizarFNASPrestacaoDeContas(ExecucaoFinanceiraInfo basica, ExecucaoFinanceiraInfo especialMedia, ExecucaoFinanceiraInfo especialAlta, ExecucaoFinanceiraInfo especialProgramasProjetos, ExecucaoFinanceiraInfo especialIncentivoGestao, ExecucaoFinanceiraInfo especialProtecaoSocialEspecial,ExecucaoFinanceiraInfo exercicioAnterior)
        {
            lblFNASPrevisaoInicial.Text = (basica.PrevisaoInicialFNAS + especialMedia.PrevisaoInicialFNAS + especialAlta.PrevisaoInicialFNAS + especialProtecaoSocialEspecial.PrevisaoInicialFNAS + especialProgramasProjetos.PrevisaoInicialFNAS + especialIncentivoGestao.PrevisaoInicialFNAS).ToString("N2");
            var recursosDisponibilizados = basica.RecursoDisponibilizadoFNAS + especialMedia.RecursoDisponibilizadoFNAS + especialAlta.RecursoDisponibilizadoFNAS + especialProtecaoSocialEspecial.RecursoDisponibilizadoFNAS + especialProgramasProjetos.RecursoDisponibilizadoFNAS + especialIncentivoGestao.RecursoDisponibilizadoFNAS;
            lblFNASRecursosDisponibilizados.Text = recursosDisponibilizados.ToString("N2");
            var resultadoAppFinanceira = (basica.ResultadoAplicacaoFinanceiraFNAS + especialMedia.ResultadoAplicacaoFinanceiraFNAS + especialAlta.ResultadoAplicacaoFinanceiraFNAS + especialProtecaoSocialEspecial.ResultadoAplicacaoFinanceiraFNAS + especialProgramasProjetos.ResultadoAplicacaoFinanceiraFNAS + especialIncentivoGestao.ResultadoAplicacaoFinanceiraFNAS);
            lblFNASResultadoAppFinanceiras.Text = resultadoAppFinanceira.ToString("N2");
            lblFNASValoresExecutados.Text = (basica.ValoresExecutadosFNAS + especialMedia.ValoresExecutadosFNAS + especialAlta.ValoresExecutadosFNAS + especialProtecaoSocialEspecial.ValoresExecutadosFNAS + especialProgramasProjetos.ValoresExecutadosFNAS + especialIncentivoGestao.ValoresExecutadosFNAS).ToString("N2");
            lblFNASValoresReprogramados.Text = (basica.ValoresReprogramadosFNAS + especialMedia.ValoresReprogramadosFNAS + especialAlta.ValoresReprogramadosFNAS + especialProtecaoSocialEspecial.ValoresReprogramadosFNAS + especialProgramasProjetos.ValoresReprogramadosFNAS + especialIncentivoGestao.ValoresReprogramadosFNAS).ToString("N2");
            lblFNASValoresDevolvidos.Text = (basica.ValoresDevolvidosFNAS + especialMedia.ValoresDevolvidosFNAS + especialAlta.ValoresDevolvidosFNAS + especialProtecaoSocialEspecial.ValoresDevolvidosFNAS + especialProgramasProjetos.ValoresDevolvidosFNAS + especialIncentivoGestao.ValoresDevolvidosFNAS).ToString("N2");
            lblFNASPorcentagensExecucao.Text = ((basica.ValoresExecutadosFNAS + especialMedia.ValoresExecutadosFNAS + especialAlta.ValoresExecutadosFNAS + especialProtecaoSocialEspecial.ValoresExecutadosFNAS + especialProgramasProjetos.ValoresExecutadosFNAS + especialIncentivoGestao.ValoresExecutadosFNAS) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
        }

        void totalizarPrestacaoDeContas(ExecucaoFinanceiraInfo basica, ExecucaoFinanceiraInfo especialMedia, ExecucaoFinanceiraInfo especialAlta, ExecucaoFinanceiraInfo especialBeneficiosEventuais, ExecucaoFinanceiraInfo especialProgramasProjetos, ExecucaoFinanceiraInfo especialIncentivoGestao, ExecucaoFinanceiraInfo especialProtecaoSocialEspecial)
        {
            var lst = new[] { basica, especialMedia, especialAlta, especialBeneficiosEventuais, especialProtecaoSocialEspecial, especialProgramasProjetos, especialIncentivoGestao};

            var previsaoInicial = lst.Sum(e => e.PrevisaoInicialFMAS + e.PrevisaoInicialFNAS);
            lblPrevisaoInicial.Text = previsaoInicial.ToString("N2");

            var recursosDisponibilizados = lst.Sum(e => e.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFNAS);
            lblRecursosDisponibilizados.Text = recursosDisponibilizados.ToString("N2");

            var resultadoAppFinanceira = lst.Sum(e => e.ResultadoAplicacaoFinanceiraFMAS + e.ResultadoAplicacaoFinanceiraFNAS);
            lblResultadoAppFinanceiras.Text = resultadoAppFinanceira.ToString("N2");

            lblValoresExecutados.Text = lst.Sum(e => e.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS + (e.ValoresExecutadosFEASReprogramado.HasValue ? e.ValoresExecutadosFEASReprogramado.Value : 0)).ToString("N2");
            lblValoresReprogramados.Text = lst.Sum(e => e.ValoresReprogramadosFMAS + e.ValoresReprogramadosFNAS).ToString("N2");
            lblValoresDevolvidos.Text = lst.Sum(e => e.ValoresDevolvidosFMAS + e.ValoresDevolvidosFNAS).ToString("N2");
            lblPorcentagensExecucao.Text = (lst.Sum(e => e.ValoresExecutadosFMAS + e.ValoresExecutadosFNAS ) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1)).ToString("P2");
        }


        ExecucaoFinanceiraInfo PreencherProtecaoBasica(int exercicio)
        {
            var protecaoBasica = new ExecucaoFinanceiraInfo();
            protecaoBasica.Exercicio = exercicio;
            protecaoBasica.IdSituacao = 1;
            protecaoBasica.Desbloqueado = true;
            protecaoBasica.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoBasica.IdTipoProtecao = 1;

            #region FMAS
            if (!String.IsNullOrEmpty(txtFMASPrevisaoInicialBasica.Text))
            {
                protecaoBasica.PrevisaoInicialFMAS = Convert.ToDecimal(txtFMASPrevisaoInicialBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASRecursosDisponibilizadosBasica.Text))
            {
                protecaoBasica.RecursoDisponibilizadoFMAS = Convert.ToDecimal(txtFMASRecursosDisponibilizadosBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASResultadoAppFinanceirasBasica.Text))
            {
                protecaoBasica.ResultadoAplicacaoFinanceiraFMAS = Convert.ToDecimal(txtFMASResultadoAppFinanceirasBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASValoresExecutadosBasica.Text))
            {
                protecaoBasica.ValoresExecutadosFMAS = Convert.ToDecimal(txtFMASValoresExecutadosBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASValoresReprogramadosBasica.Text))
            {
                protecaoBasica.ValoresReprogramadosFMAS = Convert.ToDecimal(txtFMASValoresReprogramadosBasica.Text);
            }
            protecaoBasica.ValoresDevolvidosFMAS = (protecaoBasica.RecursoDisponibilizadoFMAS + protecaoBasica.ResultadoAplicacaoFinanceiraFMAS) - (protecaoBasica.ValoresExecutadosFMAS + protecaoBasica.ValoresReprogramadosFMAS);
            #endregion

            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialBasica.Text))
            {
                protecaoBasica.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosBasica.Text))
            {
                protecaoBasica.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasBasica.Text))
            {
                protecaoBasica.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosBasica.Text))
            {
                protecaoBasica.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosBasica.Text);
            }
            
            #region reprogramado / devolvidos
            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosBasica.Text))
            {
                protecaoBasica.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosBasica.Text);
            }

            protecaoBasica.ValoresDevolvidosFEAS = (protecaoBasica.RecursoDisponibilizadoFEAS + protecaoBasica.ResultadoAplicacaoFinanceiraFEAS) - (protecaoBasica.ValoresExecutadosFEAS + protecaoBasica.ValoresReprogramadosFEAS);

            if (!String.IsNullOrEmpty(txtPrevisaoInicialReprogramacao.Text))
            {
                protecaoBasica.PrevisaoInicialFEASReprogramado = Convert.ToDecimal(txtPrevisaoInicialReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtRecursosDisponibilizadosReprogramacao.Text))
            {
                protecaoBasica.RecursoDisponibilizadoFEASReprogramado = Convert.ToDecimal(txtRecursosDisponibilizadosReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtResultadosAplicacaoReprogramacao.Text))
            {
                protecaoBasica.ResultadoAplicacaoFinanceiraFEASReprogramado = Convert.ToDecimal(txtResultadosAplicacaoReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtValoresExecutadosReprogramacao.Text))
            {
                protecaoBasica.ValoresExecutadosFEASReprogramado = Convert.ToDecimal(txtValoresExecutadosReprogramacao.Text);
            }

            protecaoBasica.ValoresDevolvidosFEASReprogramado = (protecaoBasica.RecursoDisponibilizadoFEASReprogramado + protecaoBasica.ResultadoAplicacaoFinanceiraFEASReprogramado) - (protecaoBasica.ValoresExecutadosFEASReprogramado);
            #endregion

            #endregion

            #region FNAS
            if (!String.IsNullOrEmpty(txtFNASPrevisaoInicialBasica.Text))
            {
                protecaoBasica.PrevisaoInicialFNAS = Convert.ToDecimal(txtFNASPrevisaoInicialBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASRecursosDisponibilizadosBasica.Text))
            {
                protecaoBasica.RecursoDisponibilizadoFNAS = Convert.ToDecimal(txtFNASRecursosDisponibilizadosBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASResultadoAppFinanceirasBasica.Text))
            {
                protecaoBasica.ResultadoAplicacaoFinanceiraFNAS = Convert.ToDecimal(txtFNASResultadoAppFinanceirasBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASValoresExecutadosBasica.Text))
            {
                protecaoBasica.ValoresExecutadosFNAS = Convert.ToDecimal(txtFNASValoresExecutadosBasica.Text);
            }

            #region reprogramados / devolvidos
            if (!String.IsNullOrEmpty(txtFNASValoresReprogramadosBasica.Text))
            {
                protecaoBasica.ValoresReprogramadosFNAS = Convert.ToDecimal(txtFNASValoresReprogramadosBasica.Text);
            }
            protecaoBasica.ValoresDevolvidosFNAS = (protecaoBasica.RecursoDisponibilizadoFNAS + protecaoBasica.ResultadoAplicacaoFinanceiraFNAS) - (protecaoBasica.ValoresExecutadosFNAS + protecaoBasica.ValoresReprogramadosFNAS);
            #endregion

            #endregion

            return protecaoBasica;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoProtecaoBasica(int exercicio)
        {
            var protecaoBasica = new ExecucaoFinanceiraInfo();
            protecaoBasica.Exercicio = exercicio;
            protecaoBasica.IdSituacao = 1;
            protecaoBasica.Desbloqueado = true;
            protecaoBasica.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoBasica.IdTipoProtecao = 9;

            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];

            if (sessionExecucaoFinanceira != null)
            {
                var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 9 && e.Exercicio == exercicio);

                if (preencher != null)
                {
                    protecaoBasica.PrevisaoInicialFEAS = preencher.PrevisaoInicialFEAS != null ? preencher.PrevisaoInicialFEAS : 0M;

                    protecaoBasica.RecursoDisponibilizadoFEAS = preencher.RecursoDisponibilizadoFEAS != null ? preencher.RecursoDisponibilizadoFEAS : 0M;

                    protecaoBasica.ResultadoAplicacaoFinanceiraFEAS = preencher.ResultadoAplicacaoFinanceiraFEAS != null ? preencher.ResultadoAplicacaoFinanceiraFEAS : 0M;

                    protecaoBasica.ValoresExecutadosFEAS = preencher.ValoresExecutadosFEAS != null ? preencher.ValoresExecutadosFEAS : 0M;

                    protecaoBasica.ValoresReprogramadosFEAS = preencher.ValoresReprogramadosFEAS != null ? preencher.ValoresReprogramadosFEAS : 0M;

                    protecaoBasica.ValoresDevolvidosFEAS = (protecaoBasica.RecursoDisponibilizadoFEAS + protecaoBasica.ResultadoAplicacaoFinanceiraFEAS) - (protecaoBasica.ValoresExecutadosFEAS + protecaoBasica.ValoresReprogramadosFEAS);                    
                }
            }
            return protecaoBasica;
        }

        ExecucaoFinanceiraInfo PreencherProtecaoEspecialMedia(int exercicio)
        {
            var protecaoMedia = new ExecucaoFinanceiraInfo();
            protecaoMedia.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoMedia.Exercicio = exercicio;
            protecaoMedia.IdSituacao = 1;
            protecaoMedia.Desbloqueado = true;
            protecaoMedia.IdTipoProtecao = 2;

            #region FMAS
            if (!String.IsNullOrEmpty(txtFMASPrevisaoInicialMedia.Text))
            {
                protecaoMedia.PrevisaoInicialFMAS = Convert.ToDecimal(txtFMASPrevisaoInicialMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASRecursosDisponibilizadosMedia.Text))
            {
                protecaoMedia.RecursoDisponibilizadoFMAS = Convert.ToDecimal(txtFMASRecursosDisponibilizadosMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASResultadoAppFinanceirasMedia.Text))
            {
                protecaoMedia.ResultadoAplicacaoFinanceiraFMAS = Convert.ToDecimal(txtFMASResultadoAppFinanceirasMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASValoresExecutadosMedia.Text))
            {
                protecaoMedia.ValoresExecutadosFMAS = Convert.ToDecimal(txtFMASValoresExecutadosMedia.Text);
            }
            #region reprogramado / devolvido
            if (!String.IsNullOrEmpty(txtFMASValoresReprogramadosMedia.Text))
            {
                protecaoMedia.ValoresReprogramadosFMAS = Convert.ToDecimal(txtFMASValoresReprogramadosMedia.Text);
            }
            protecaoMedia.ValoresDevolvidosFMAS = (protecaoMedia.RecursoDisponibilizadoFMAS + protecaoMedia.ResultadoAplicacaoFinanceiraFMAS) - (protecaoMedia.ValoresExecutadosFMAS + protecaoMedia.ValoresReprogramadosFMAS);
            #endregion
            #endregion

            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialMedia.Text))
            {
                protecaoMedia.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosMedia.Text))
            {
                protecaoMedia.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasMedia.Text))
            {
                protecaoMedia.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosMedia.Text))
            {
                protecaoMedia.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosMedia.Text);
            }
            #region reprogramado e devolvido
            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosMedia.Text))
            {
                protecaoMedia.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosMedia.Text);
            }
            protecaoMedia.ValoresDevolvidosFEAS = (protecaoMedia.RecursoDisponibilizadoFEAS + protecaoMedia.ResultadoAplicacaoFinanceiraFEAS) - (protecaoMedia.ValoresExecutadosFEAS + protecaoMedia.ValoresReprogramadosFEAS);
            #endregion
            #endregion

            #region FNAS
            if (!String.IsNullOrEmpty(txtFNASPrevisaoInicialMedia.Text))
            {
                protecaoMedia.PrevisaoInicialFNAS = Convert.ToDecimal(txtFNASPrevisaoInicialMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASRecursosDisponibilizadosMedia.Text))
            {
                protecaoMedia.RecursoDisponibilizadoFNAS = Convert.ToDecimal(txtFNASRecursosDisponibilizadosMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASResultadoAppFinanceirasMedia.Text))
            {
                protecaoMedia.ResultadoAplicacaoFinanceiraFNAS = Convert.ToDecimal(txtFNASResultadoAppFinanceirasMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASValoresExecutadosMedia.Text))
            {
                protecaoMedia.ValoresExecutadosFNAS = Convert.ToDecimal(txtFNASValoresExecutadosMedia.Text);
            }
            #region reprogramado e devolvido
            if (!String.IsNullOrEmpty(txtFNASValoresReprogramadosMedia.Text))
            {
                protecaoMedia.ValoresReprogramadosFNAS = Convert.ToDecimal(txtFNASValoresReprogramadosMedia.Text);
            }
            protecaoMedia.ValoresDevolvidosFNAS = (protecaoMedia.RecursoDisponibilizadoFNAS + protecaoMedia.ResultadoAplicacaoFinanceiraFNAS) - (protecaoMedia.ValoresExecutadosFNAS + protecaoMedia.ValoresReprogramadosFNAS);
            #endregion

            #endregion

            return protecaoMedia;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoProtecaoMedia(int exercicio)
        {
            var protecaoMedia = new ExecucaoFinanceiraInfo();
            protecaoMedia.Exercicio = exercicio;
            protecaoMedia.IdSituacao = 1;
            protecaoMedia.Desbloqueado = true;
            protecaoMedia.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoMedia.IdTipoProtecao = 10;

            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];

            if (sessionExecucaoFinanceira != null)
            {
                var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 10 && e.Exercicio == exercicio);

                if (preencher != null)
                {
                    protecaoMedia.PrevisaoInicialFEAS = preencher.PrevisaoInicialFEAS != null ? preencher.PrevisaoInicialFEAS : 0M;

                    protecaoMedia.RecursoDisponibilizadoFEAS = preencher.RecursoDisponibilizadoFEAS != null ? preencher.RecursoDisponibilizadoFEAS : 0M;

                    protecaoMedia.ResultadoAplicacaoFinanceiraFEAS = preencher.ResultadoAplicacaoFinanceiraFEAS != null ? preencher.ResultadoAplicacaoFinanceiraFEAS : 0M;

                    protecaoMedia.ValoresExecutadosFEAS = preencher.ValoresExecutadosFEAS != null ? preencher.ValoresExecutadosFEAS : 0M;

                    protecaoMedia.ValoresReprogramadosFEAS = preencher.ValoresReprogramadosFEAS != null ? preencher.ValoresReprogramadosFEAS : 0M;

                    protecaoMedia.ValoresDevolvidosFEAS = (protecaoMedia.RecursoDisponibilizadoFEAS + protecaoMedia.ResultadoAplicacaoFinanceiraFEAS) - (protecaoMedia.ValoresExecutadosFEAS + protecaoMedia.ValoresReprogramadosFEAS);
                    
                }

            }

            return protecaoMedia;
        }

        ExecucaoFinanceiraInfo PreencherProtecaoEspecialAlta(int exercicio)
        {
            var protecaoEspecial = new ExecucaoFinanceiraInfo();
            protecaoEspecial.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoEspecial.Exercicio = exercicio;
            protecaoEspecial.IdSituacao = 1;
            protecaoEspecial.Desbloqueado = true;
            protecaoEspecial.IdTipoProtecao = 3;

            #region FMAS
            if (!String.IsNullOrEmpty(txtFMASPrevisaoInicialAlta.Text))
            {
                protecaoEspecial.PrevisaoInicialFMAS = Convert.ToDecimal(txtFMASPrevisaoInicialAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASRecursosDisponibilizadosAlta.Text))
            {
                protecaoEspecial.RecursoDisponibilizadoFMAS = Convert.ToDecimal(txtFMASRecursosDisponibilizadosAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASResultadoAppFinanceirasAlta.Text))
            {
                protecaoEspecial.ResultadoAplicacaoFinanceiraFMAS = Convert.ToDecimal(txtFMASResultadoAppFinanceirasAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASValoresExecutadosAlta.Text))
            {
                protecaoEspecial.ValoresExecutadosFMAS = Convert.ToDecimal(txtFMASValoresExecutadosAlta.Text);
            }
            
            #region reprogramado e devolvido
            if (!String.IsNullOrEmpty(txtFMASValoresReprogramadosAlta.Text))
            {
                protecaoEspecial.ValoresReprogramadosFMAS = Convert.ToDecimal(txtFMASValoresReprogramadosAlta.Text);
            }
            protecaoEspecial.ValoresDevolvidosFMAS = (protecaoEspecial.RecursoDisponibilizadoFMAS + protecaoEspecial.ResultadoAplicacaoFinanceiraFMAS) - (protecaoEspecial.ValoresExecutadosFMAS + protecaoEspecial.ValoresReprogramadosFMAS);
            #endregion

            #endregion

            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialAlta.Text))
            {
                protecaoEspecial.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosAlta.Text))
            {
                protecaoEspecial.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasAlta.Text))
            {
                protecaoEspecial.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosAlta.Text))
            {
                protecaoEspecial.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosAlta.Text);
            }
            #region reprogramado e devolvidos
            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosAlta.Text))
            {
                protecaoEspecial.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosAlta.Text);
            }
            protecaoEspecial.ValoresDevolvidosFEAS = (protecaoEspecial.RecursoDisponibilizadoFEAS + protecaoEspecial.ResultadoAplicacaoFinanceiraFEAS) - (protecaoEspecial.ValoresExecutadosFEAS + protecaoEspecial.ValoresReprogramadosFEAS);
            #endregion
            #endregion

            #region FNAS
            if (!String.IsNullOrEmpty(txtFNASPrevisaoInicialAlta.Text))
            {
                protecaoEspecial.PrevisaoInicialFNAS = Convert.ToDecimal(txtFNASPrevisaoInicialAlta.Text);
            }

            if (!String.IsNullOrEmpty(txtFNASRecursosDisponibilizadosAlta.Text))
            {
                protecaoEspecial.RecursoDisponibilizadoFNAS = Convert.ToDecimal(txtFNASRecursosDisponibilizadosAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASResultadoAppFinanceirasAlta.Text))
            {
                protecaoEspecial.ResultadoAplicacaoFinanceiraFNAS = Convert.ToDecimal(txtFNASResultadoAppFinanceirasAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASValoresExecutadosAlta.Text))
            {
                protecaoEspecial.ValoresExecutadosFNAS = Convert.ToDecimal(txtFNASValoresExecutadosAlta.Text);
            }
            #region reprogramado e devolvidos
            if (!String.IsNullOrEmpty(txtFNASValoresReprogramadosAlta.Text))
            {
                protecaoEspecial.ValoresReprogramadosFNAS = Convert.ToDecimal(txtFNASValoresReprogramadosAlta.Text);
            }
            protecaoEspecial.ValoresDevolvidosFNAS = (protecaoEspecial.RecursoDisponibilizadoFNAS + protecaoEspecial.ResultadoAplicacaoFinanceiraFNAS) - (protecaoEspecial.ValoresExecutadosFNAS + protecaoEspecial.ValoresReprogramadosFNAS);
            #endregion
            #endregion

            return protecaoEspecial;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoProtecaoAlta(int exercicio)
        {
            var protecaoAlta = new ExecucaoFinanceiraInfo();
            protecaoAlta.Exercicio = exercicio;
            protecaoAlta.IdSituacao = 1;
            protecaoAlta.Desbloqueado = true;
            protecaoAlta.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoAlta.IdTipoProtecao = 11;

            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];

            if (sessionExecucaoFinanceira != null)
            {
                var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 11 && e.Exercicio == exercicio);

                if (preencher != null)
                {
                    protecaoAlta.PrevisaoInicialFEAS = preencher.PrevisaoInicialFEAS != null ? preencher.PrevisaoInicialFEAS : 0M;

                    protecaoAlta.RecursoDisponibilizadoFEAS = preencher.RecursoDisponibilizadoFEAS != null ? preencher.RecursoDisponibilizadoFEAS : 0M;

                    protecaoAlta.ResultadoAplicacaoFinanceiraFEAS = preencher.ResultadoAplicacaoFinanceiraFEAS != null ? preencher.ResultadoAplicacaoFinanceiraFEAS : 0M;

                    protecaoAlta.ValoresExecutadosFEAS = preencher.ValoresExecutadosFEAS != null ? preencher.ValoresExecutadosFEAS : 0M;

                    protecaoAlta.ValoresReprogramadosFEAS = preencher.ValoresReprogramadosFEAS != null ? preencher.ValoresReprogramadosFEAS : 0M;

                    protecaoAlta.ValoresDevolvidosFEAS = (protecaoAlta.RecursoDisponibilizadoFEAS + protecaoAlta.ResultadoAplicacaoFinanceiraFEAS) - (protecaoAlta.ValoresExecutadosFEAS + protecaoAlta.ValoresReprogramadosFEAS);                    
                }
            }

            return protecaoAlta;
        }

        ExecucaoFinanceiraInfo PreencherBeneficiosEventuais(int exercicio) 
        {
            var beneficiosEventuais = new ExecucaoFinanceiraInfo();
            beneficiosEventuais.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            beneficiosEventuais.Exercicio = exercicio;
            beneficiosEventuais.IdSituacao = 1;
            beneficiosEventuais.Desbloqueado = true;
            beneficiosEventuais.IdTipoProtecao = 5;            
            
            
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];

            if (sessionExecucaoFinanceira != null)
            {
                var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 5 && e.Exercicio == exercicio);

                #region FEAS


                if (preencher != null)
                {
                    beneficiosEventuais.PrevisaoInicialFEAS = preencher.PrevisaoInicialFEAS != null ? preencher.PrevisaoInicialFEAS : 0M;

                    beneficiosEventuais.RecursoDisponibilizadoFEAS = preencher.RecursoDisponibilizadoFEAS != null ? preencher.RecursoDisponibilizadoFEAS : 0M;

                    beneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS = preencher.ResultadoAplicacaoFinanceiraFEAS != null ? preencher.ResultadoAplicacaoFinanceiraFEAS : 0M;

                    beneficiosEventuais.ValoresExecutadosFEAS = preencher.ValoresExecutadosFEAS != null ? preencher.ValoresExecutadosFEAS : 0M;

                    beneficiosEventuais.ValoresReprogramadosFEAS = preencher.ValoresReprogramadosFEAS != null ? preencher.ValoresReprogramadosFEAS : 0M;

                    beneficiosEventuais.ValoresDevolvidosFEAS = preencher.ValoresDevolvidosFEAS != null ? preencher.ValoresDevolvidosFEAS : 0M;                    
                }


                #endregion

            }

            #region FMAS
            if (!String.IsNullOrEmpty(txtFMASPrevisaoInicialBeneficiosEventuais.Text))
            {
                beneficiosEventuais.PrevisaoInicialFMAS = Convert.ToDecimal(txtFMASPrevisaoInicialBeneficiosEventuais.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASRecursosDisponibilizadosBeneficiosEventuais.Text))
            {
                beneficiosEventuais.RecursoDisponibilizadoFMAS = Convert.ToDecimal(txtFMASRecursosDisponibilizadosBeneficiosEventuais.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASResultadoAppFinanceirasBeneficiosEventuais.Text))
            {
                beneficiosEventuais.ResultadoAplicacaoFinanceiraFMAS = Convert.ToDecimal(txtFMASResultadoAppFinanceirasBeneficiosEventuais.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASValoresExecutadosBeneficiosEventuais.Text))
            {
                beneficiosEventuais.ValoresExecutadosFMAS = Convert.ToDecimal(txtFMASValoresExecutadosBeneficiosEventuais.Text);
            }

            if (!String.IsNullOrEmpty(txtFMASValoresReprogramadosBeneficiosEventuais.Text))
            {
                beneficiosEventuais.ValoresReprogramadosFMAS = Convert.ToDecimal(txtFMASValoresReprogramadosBeneficiosEventuais.Text);
            }
            beneficiosEventuais.ValoresDevolvidosFMAS = (beneficiosEventuais.RecursoDisponibilizadoFMAS + beneficiosEventuais.ResultadoAplicacaoFinanceiraFMAS) - (beneficiosEventuais.ValoresExecutadosFMAS + beneficiosEventuais.ValoresReprogramadosFMAS);
            
            

            #endregion
                
            return beneficiosEventuais;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoProtecaoBeneficiosEventuais(int exercicio)
        {
            var protecaoBeneficiosEventuais = new ExecucaoFinanceiraInfo();
            protecaoBeneficiosEventuais.Exercicio = exercicio;
            protecaoBeneficiosEventuais.IdSituacao = 1;
            protecaoBeneficiosEventuais.Desbloqueado = true;
            protecaoBeneficiosEventuais.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoBeneficiosEventuais.IdTipoProtecao = 12;   

            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];

            if (sessionExecucaoFinanceira != null)
            {
                var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 12 && e.Exercicio == exercicio);

                if (preencher != null)
                {
                    protecaoBeneficiosEventuais.PrevisaoInicialFEAS = preencher.PrevisaoInicialFEAS != null ? preencher.PrevisaoInicialFEAS : 0M;
                    protecaoBeneficiosEventuais.RecursoDisponibilizadoFEAS = preencher.RecursoDisponibilizadoFEAS != null ? preencher.RecursoDisponibilizadoFEAS : 0M;
                    protecaoBeneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS = preencher.ResultadoAplicacaoFinanceiraFEAS != null ? preencher.ResultadoAplicacaoFinanceiraFEAS : 0M;
                    protecaoBeneficiosEventuais.ValoresExecutadosFEAS = preencher.ValoresExecutadosFEAS != null ? preencher.ValoresExecutadosFEAS : 0M;
                    protecaoBeneficiosEventuais.ValoresReprogramadosFEAS = preencher.ValoresReprogramadosFEAS != null ? preencher.ValoresReprogramadosFEAS : 0M;
                    protecaoBeneficiosEventuais.ValoresDevolvidosFEAS = (protecaoBeneficiosEventuais.RecursoDisponibilizadoFEAS + protecaoBeneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS) - (protecaoBeneficiosEventuais.ValoresExecutadosFEAS + protecaoBeneficiosEventuais.ValoresReprogramadosFEAS);                    
                }
            }

            return protecaoBeneficiosEventuais;
        }

        ExecucaoFinanceiraInfo PreencherProgramasProjetos(int exercicio) 
        {
            var programasProjetos = new ExecucaoFinanceiraInfo();

            programasProjetos.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            programasProjetos.Exercicio = exercicio;
            programasProjetos.IdSituacao = 1;
            programasProjetos.Desbloqueado = true;
            programasProjetos.IdTipoProtecao = 4;
            
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
           
            if (sessionExecucaoFinanceira != null)
            {
                var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 4 && e.Exercicio == exercicio);

                #region FEAS

                if (preencher != null)
                {
                    programasProjetos.PrevisaoInicialFEAS = preencher.PrevisaoInicialFEAS != null ? preencher.PrevisaoInicialFEAS : 0M;

                    programasProjetos.RecursoDisponibilizadoFEAS = preencher.RecursoDisponibilizadoFEAS != null ? preencher.RecursoDisponibilizadoFEAS : 0M;

                    programasProjetos.ResultadoAplicacaoFinanceiraFEAS = preencher.ResultadoAplicacaoFinanceiraFEAS != null ? preencher.ResultadoAplicacaoFinanceiraFEAS : 0M;

                    programasProjetos.ValoresExecutadosFEAS = preencher.ValoresExecutadosFEAS != null ? preencher.ValoresExecutadosFEAS : 0M;

                    programasProjetos.ValoresReprogramadosFEAS = preencher.ValoresReprogramadosFEAS != null ? preencher.ValoresReprogramadosFEAS : 0M;

                    programasProjetos.ValoresDevolvidosFEAS = preencher.ValoresDevolvidosFEAS != null ? preencher.ValoresDevolvidosFEAS : 0M;                    
                }

                #endregion                
            }

            #region FMAS
            if (!String.IsNullOrEmpty(txtFMASPrevisaoInicialProgramasProjetos.Text))
            {
                programasProjetos.PrevisaoInicialFMAS = Convert.ToDecimal(txtFMASPrevisaoInicialProgramasProjetos.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASRecursosDisponibilizadosProgramasProjetos.Text))
            {
                programasProjetos.RecursoDisponibilizadoFMAS = Convert.ToDecimal(txtFMASRecursosDisponibilizadosProgramasProjetos.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASResultadoAppFinanceirasProgramasProjetos.Text))
            {
                programasProjetos.ResultadoAplicacaoFinanceiraFMAS = Convert.ToDecimal(txtFMASResultadoAppFinanceirasProgramasProjetos.Text);
            }
            if (!String.IsNullOrEmpty(txtFMASValoresExecutadosProgramasProjetos.Text))
            {
                programasProjetos.ValoresExecutadosFMAS = Convert.ToDecimal(txtFMASValoresExecutadosProgramasProjetos.Text);
            }

            if (!String.IsNullOrEmpty(txtFMASValoresReprogramadosProgramasProjetos.Text))
            {
                programasProjetos.ValoresReprogramadosFMAS = Convert.ToDecimal(txtFMASValoresReprogramadosProgramasProjetos.Text);
            }

            programasProjetos.ValoresDevolvidosFMAS = (programasProjetos.RecursoDisponibilizadoFMAS + programasProjetos.ResultadoAplicacaoFinanceiraFMAS) - (programasProjetos.ValoresExecutadosFMAS + programasProjetos.ValoresReprogramadosFMAS);
            #endregion

            #region FNAS

            if (!String.IsNullOrEmpty(txtFNASPrevisaoInicialProgramasProjetos.Text))
            {
                programasProjetos.PrevisaoInicialFNAS = Convert.ToDecimal(txtFNASPrevisaoInicialProgramasProjetos.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASRecursosDisponibilizadosProgramasProjetos.Text))
            {
                programasProjetos.RecursoDisponibilizadoFNAS = Convert.ToDecimal(txtFNASRecursosDisponibilizadosProgramasProjetos.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASResultadoAppFinanceirasProgramasProjetos.Text))
            {
                programasProjetos.ResultadoAplicacaoFinanceiraFNAS = Convert.ToDecimal(txtFNASResultadoAppFinanceirasProgramasProjetos.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASValoresExecutadosProgramasProjetos.Text))
            {
                programasProjetos.ValoresExecutadosFNAS = Convert.ToDecimal(txtFNASValoresExecutadosProgramasProjetos.Text);
            }

            if (!String.IsNullOrEmpty(txtFNASValoresReprogramadosProgramasProjetos.Text))
            {
                programasProjetos.ValoresReprogramadosFNAS = Convert.ToDecimal(txtFNASValoresReprogramadosProgramasProjetos.Text);
            }

            programasProjetos.ValoresDevolvidosFNAS = (programasProjetos.RecursoDisponibilizadoFNAS + programasProjetos.ResultadoAplicacaoFinanceiraFNAS) - (programasProjetos.ValoresExecutadosFNAS + programasProjetos.ValoresReprogramadosFNAS);
            
            #endregion

            return programasProjetos;
        }

        ExecucaoFinanceiraInfo PreencherServicosProtecaoSocialEspecial(int exercicio) 
        {
            var protecaoSocialEspecial = new ExecucaoFinanceiraInfo();
            protecaoSocialEspecial.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoSocialEspecial.Exercicio = exercicio;
            protecaoSocialEspecial.IdSituacao = 1;
            protecaoSocialEspecial.Desbloqueado = true;
            protecaoSocialEspecial.IdTipoProtecao = 6;

            
            #region FNAS
            if (!String.IsNullOrEmpty(txtFNASPrevisaoInicialProtecaoSocialEspecial.Text))
            {
                protecaoSocialEspecial.PrevisaoInicialFNAS = Convert.ToDecimal(txtFNASPrevisaoInicialProtecaoSocialEspecial.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASRecursosDisponibilizadosProtecaoSocialEspecial.Text))
            {
                protecaoSocialEspecial.RecursoDisponibilizadoFNAS = Convert.ToDecimal(txtFNASRecursosDisponibilizadosProtecaoSocialEspecial.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASResultadoAppFinanceirasProtecaoSocialEspecial.Text))
            {
                protecaoSocialEspecial.ResultadoAplicacaoFinanceiraFNAS = Convert.ToDecimal(txtFNASResultadoAppFinanceirasProtecaoSocialEspecial.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASValoresExecutadosProtecaoSocialEspecial.Text))
            {
                protecaoSocialEspecial.ValoresExecutadosFNAS = Convert.ToDecimal(txtFNASValoresExecutadosProtecaoSocialEspecial.Text);
            }

            if (!String.IsNullOrEmpty(txtFNASValoresReprogramadosProtecaoSocialEspecial.Text))
            {
                protecaoSocialEspecial.ValoresReprogramadosFNAS = Convert.ToDecimal(txtFNASValoresReprogramadosProtecaoSocialEspecial.Text);
            }

            protecaoSocialEspecial.ValoresDevolvidosFNAS = (protecaoSocialEspecial.RecursoDisponibilizadoFNAS + protecaoSocialEspecial.ResultadoAplicacaoFinanceiraFNAS) - (protecaoSocialEspecial.ValoresExecutadosFNAS + protecaoSocialEspecial.ValoresReprogramadosFNAS);
            #endregion
            
            return protecaoSocialEspecial;
        }

        ExecucaoFinanceiraInfo PreencherIncentivoGestao(int exercicio) 
        {
            var IncentivoGestao = new ExecucaoFinanceiraInfo();
            IncentivoGestao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            IncentivoGestao.Exercicio = exercicio;
            IncentivoGestao.IdSituacao = 1;
            IncentivoGestao.Desbloqueado = true;
            IncentivoGestao.IdTipoProtecao = 7;
           
            #region FNAS

            if (!String.IsNullOrEmpty(txtFNASPrevisaoInicialIncentivoGestao.Text))
            {
                IncentivoGestao.PrevisaoInicialFNAS = Convert.ToDecimal(txtFNASPrevisaoInicialIncentivoGestao.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASRecursosDisponibilizadosIncentivoGestao.Text))
            {
                IncentivoGestao.RecursoDisponibilizadoFNAS = Convert.ToDecimal(txtFNASRecursosDisponibilizadosIncentivoGestao.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASResultadoAppFinanceirasIncentivoGestao.Text))
            {
                IncentivoGestao.ResultadoAplicacaoFinanceiraFNAS = Convert.ToDecimal(txtFNASResultadoAppFinanceirasIncentivoGestao.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASValoresExecutadosIncentivoGestao.Text))
            {
                IncentivoGestao.ValoresExecutadosFNAS = Convert.ToDecimal(txtFNASValoresExecutadosIncentivoGestao.Text);
            }

            if (!String.IsNullOrEmpty(txtFNASValoresReprogramadosIncentivoGestao.Text))
            {
                IncentivoGestao.ValoresReprogramadosFNAS = Convert.ToDecimal(txtFNASValoresReprogramadosIncentivoGestao.Text);
            }

            IncentivoGestao.ValoresDevolvidosFNAS = (IncentivoGestao.RecursoDisponibilizadoFNAS + IncentivoGestao.ResultadoAplicacaoFinanceiraFNAS) - (IncentivoGestao.ValoresExecutadosFNAS + IncentivoGestao.ValoresReprogramadosFNAS);

            #endregion
                        
            return IncentivoGestao;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoExercicioAnterior(int exercicio) 
        {
            var reprogramacaoExercicioAnterior = new ExecucaoFinanceiraInfo();
            reprogramacaoExercicioAnterior.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            reprogramacaoExercicioAnterior.Exercicio = exercicio;
            reprogramacaoExercicioAnterior.IdSituacao = 1;
            reprogramacaoExercicioAnterior.Desbloqueado = true;
            reprogramacaoExercicioAnterior.IdTipoProtecao = 8;

            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];

            if (sessionExecucaoFinanceira != null)
            {
                var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 8 && e.Exercicio == exercicio);

                #region FEAS

                if (preencher != null)
                {
                    reprogramacaoExercicioAnterior.PrevisaoInicialFEAS = preencher.PrevisaoInicialFEAS != null ? preencher.PrevisaoInicialFEAS : 0M;

                    reprogramacaoExercicioAnterior.RecursoDisponibilizadoFEAS = preencher.RecursoDisponibilizadoFEAS != null ? preencher.RecursoDisponibilizadoFEAS : 0M;

                    reprogramacaoExercicioAnterior.ResultadoAplicacaoFinanceiraFEAS = preencher.ResultadoAplicacaoFinanceiraFEAS != null ? preencher.ResultadoAplicacaoFinanceiraFEAS : 0M;

                    reprogramacaoExercicioAnterior.ValoresExecutadosFEAS = preencher.ValoresExecutadosFEAS != null ? preencher.ValoresExecutadosFEAS : 0M;

                    reprogramacaoExercicioAnterior.ValoresReprogramadosFEAS = preencher.ValoresReprogramadosFEAS != null ? preencher.ValoresReprogramadosFEAS : 0M;

                    reprogramacaoExercicioAnterior.ValoresDevolvidosFEAS = preencher.ValoresDevolvidosFEAS != null ? preencher.ValoresDevolvidosFEAS : 0M;                    
                }
                #endregion
            }

            #region FNAS
            if (!String.IsNullOrEmpty(txtFNASPrevisaoInicialExercicioAnterior.Text))
            {
                reprogramacaoExercicioAnterior.PrevisaoInicialFNAS = Convert.ToDecimal(txtFNASPrevisaoInicialExercicioAnterior.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASRecursosDisponibilizadosExercicioAnterior.Text))
            {
                reprogramacaoExercicioAnterior.RecursoDisponibilizadoFNAS = Convert.ToDecimal(txtFNASRecursosDisponibilizadosExercicioAnterior.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASResultadoAppFinanceirasExercicioAnterior.Text))
            {
                reprogramacaoExercicioAnterior.ResultadoAplicacaoFinanceiraFNAS = Convert.ToDecimal(txtFNASResultadoAppFinanceirasExercicioAnterior.Text);
            }
            if (!String.IsNullOrEmpty(txtFNASValoresExecutadosExercicioAnterior.Text))
            {
                reprogramacaoExercicioAnterior.ValoresExecutadosFNAS = Convert.ToDecimal(txtFNASValoresExecutadosExercicioAnterior.Text);
            }

            if (!String.IsNullOrEmpty(txtFNASValoresReprogramadosExercicioAnterior.Text))
            {
                reprogramacaoExercicioAnterior.ValoresReprogramadosFNAS = Convert.ToDecimal(txtFNASValoresReprogramadosExercicioAnterior.Text);
            }

            reprogramacaoExercicioAnterior.ValoresDevolvidosFNAS = (reprogramacaoExercicioAnterior.RecursoDisponibilizadoFNAS + reprogramacaoExercicioAnterior.ResultadoAplicacaoFinanceiraFNAS) - (reprogramacaoExercicioAnterior.ValoresExecutadosFNAS + reprogramacaoExercicioAnterior.ValoresReprogramadosFNAS);
            #endregion
            
            return reprogramacaoExercicioAnterior;
        }


        private string ValidarCMAS() 
        {
            string msg = String.Empty;


            if (String.IsNullOrEmpty(txtDataPublicacao.Text))
            {
                msg = "Insira a data de publicação.";
            }
            else if (String.IsNullOrEmpty(txtDataReuniao.Text))
            {
                msg = "Insira a data de reunião.";
            }
            else if (String.IsNullOrEmpty(txtNumeroConselheiros.Text))
            {
                msg = "Insira o número de conselheiros.";
            }
            else if (String.IsNullOrEmpty(txtNumeroAta.Text))
            {
                msg = "Insira o número da ata.";
            }

            return msg;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);


            var basica = PreencherProtecaoBasica(exercicio);
            var reprogramacaoBasica = PreencherReprogramacaoProtecaoBasica(exercicio);
            var especialMedia = PreencherProtecaoEspecialMedia(exercicio);
            var reprogramacaoMedia = PreencherReprogramacaoProtecaoMedia(exercicio);
            var especialAlta = PreencherProtecaoEspecialAlta(exercicio);
            var reprogramacaoAlta = PreencherReprogramacaoProtecaoAlta(exercicio);
            var beneficiosEventuais = PreencherBeneficiosEventuais(exercicio);
            var reprogramacaoBeneficiosEventuais = PreencherReprogramacaoProtecaoBeneficiosEventuais(exercicio);
            var protecaoSocialEspecial = PreencherServicosProtecaoSocialEspecial(exercicio);
            var programasProjetos = PreencherProgramasProjetos(exercicio);
            var incentivoGestao = PreencherIncentivoGestao(exercicio);
            var exercicioAnterior = PreencherReprogramacaoExercicioAnterior(exercicio);

            String msg = String.Empty;
            


            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    ComentarioExecucaoFinanceiraInfo comentario = new ComentarioExecucaoFinanceiraInfo();
                    
                    if (!String.IsNullOrEmpty(txtComentario.Text))
                    {
                        comentario.Comentario = (txtComentario.Text.Length > 1000) ? txtComentario.Text.Substring(0, 1000) : txtComentario.Text;    
                    
                        comentario.Exercicio = exercicio;
                        comentario.Desbloqueado = true;
                        comentario.IdSituacao = 1;

                        prefeituras.SaveExecucaoFinanceira(comentario, basica,reprogramacaoBasica, especialMedia,reprogramacaoMedia, especialAlta,reprogramacaoAlta, beneficiosEventuais,reprogramacaoBeneficiosEventuais, protecaoSocialEspecial, programasProjetos, incentivoGestao, exercicioAnterior);

                        var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == exercicio).FirstOrDefault();
                        
                        if (quadro == null)
                        {
                            quadro = new PrefeituraSituacaoQuadroInfo()
                            {
                                IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id,
                                IdRecurso = 143,
                                IdSituacaoQuadro = 1
                            };

                            proxy.Service.SavePrefeituraSituacaoQuadro(quadro);

                        }

                        CarregarExecucaoFinanceira(prefeituras);
                    }
                    else
                    {
                        throw new ArgumentException("Comentário do órgão gestor municipal esta vazio, favor preencher !"); 
                    }
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Execução Financeira registrada com sucesso!";
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

        private bool SalvarComentarioEDeliberacaoCMAS(int idSituacao) 
        {
            String msg = String.Empty;
            try
            {

                var comentario = new ComentarioExecucaoFinanceiraCMASInfo();
                comentario.Exercicio = Convert.ToInt32(hdfAno.Value);

                if (!String.IsNullOrEmpty(txtComentario2.Text))
                {
                    comentario.Comentario = (txtComentario2.Text.Length > 1000) ? txtComentario2.Text.Substring(0, 1000) : txtComentario2.Text;
                }
                else
                {
                    throw new ArgumentException("Comentários e Parecer do Conselho Municipal de Assistência Social não foi preenchido.");
                }
                
                comentario.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                comentario.Desbloqueado = true;
                comentario.IdSituacao = idSituacao;

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    prefeituras.SaveComentarioCMAS(comentario);
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            if (!String.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
                lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SalvarDeliberacaoCMAS()
        {

            string msg = String.Empty;

            try
            {
                var deliberacao = new DeliberacaoCMASInfo();

                if (String.IsNullOrEmpty(ValidarCMAS()))
                {
                    deliberacao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                    deliberacao.Exercicio = Convert.ToInt32(hdfAno.Value);
                    deliberacao.DataPublicacao = Convert.ToDateTime(txtDataPublicacao.Text);
                    deliberacao.DataReuniao = Convert.ToDateTime(txtDataReuniao.Text);
                    deliberacao.NumeroConselheiros = txtNumeroConselheiros.Text;
                    deliberacao.NumeroAta = txtNumeroAta.Text;
                    deliberacao.NumeroResolucao = txtNumeroResolucao.Text;
                }
                else
                {
                    throw new ArgumentException(ValidarCMAS());
                }

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    prefeituras.SaveDeliberacaoCMAS(deliberacao);
                    AlterarSituacaoQuadro(4);
                }
            }
            catch (Exception e)
            {
                msg = e.Message;                
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Execução Financeira registrada com sucesso!";
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
            int exercicio = Convert.ToInt32(hdfAno.Value);
            var basica = PreencherProtecaoBasica(exercicio);
            var media = PreencherProtecaoEspecialMedia(exercicio);
            var alta = PreencherProtecaoEspecialAlta(exercicio);
            var beneficiosEventuais = PreencherBeneficiosEventuais(exercicio);
            var protecaoEspecialSocial = PreencherServicosProtecaoSocialEspecial(exercicio);
            var programasProjetos = PreencherProgramasProjetos(exercicio);
            var incentivoGestao = PreencherIncentivoGestao(exercicio);
            var exercicioAnterior = PreencherReprogramacaoExercicioAnterior(exercicio);

            if (exercicio >= 2020)
            {
                carregarProtecaoBasica(basica);
                carregarProtecaoEspecialMedia(media);
                carregarProtecaoEspecialAlta(alta);

                carregarProtecaoEspecialBeneficiosEventuais(beneficiosEventuais);
                carregarProtecaoEspecialProgramasProjetos(programasProjetos);
                carregarProtecaoIncentivoGestao(incentivoGestao);
                carregarProtecaoSocialEspecial(protecaoEspecialSocial, media, alta);
                carregarProtecaoExercicioAnterior(exercicioAnterior);

                totalizarFMASPrestacaoDeContas(basica, media, alta, beneficiosEventuais, programasProjetos);
                totalizarFNASPrestacaoDeContas(basica, media, alta, programasProjetos, incentivoGestao, protecaoEspecialSocial,exercicioAnterior);
                totalizarPrestacaoDeContas(basica, media, alta, beneficiosEventuais, programasProjetos, incentivoGestao, protecaoEspecialSocial);

            }
            else
            {
                carregarProtecaoBasica(basica);
                carregarProtecaoEspecialMedia(media);
                carregarProtecaoEspecialAlta(alta);

                totalizarFMAS(basica, media, alta);
                totalizarFEAS(basica, media, alta);
                totalizarFNAS(basica, media, alta);
                totalizar(basica, media, alta);
            }
        }

        private void BloqueioEFPorSituacaoQuadro(ProxyPrefeitura proxy)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);
            var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == exercicio).FirstOrDefault();

            LoadFMAS();
            LoadFEAS();
            LoadFNAS();
            LoadCMAS();
            LoadTotais();

            if(quadro != null)
            {
                if (exercicio >= 2021)
                {
                    switch (quadro.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                        default:
                            trFinalizarGestorCalculo.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                            btnCalcular.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                            btnCalcular.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                            btnSalvar.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                            btnSalvar.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                            txtComentario.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                            txtComentario.Visible = true;
                            trComentarioCMAS.Visible = true;
                            txtComentario2.Visible = true;
                            rblDeliberacao.SelectedIndex = -1;
                            
                            obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            ObterControleBeneficioseventuais().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            ObterControleEspecial().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            ObterControleProgramasProjetos().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            ObterControleIncentivoGestao().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            ObterControleExAnterior().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            ObterControlesComentarioCMAS().ToList().ForEach(x => x.Enabled = false);
                            
                            #region proibicoes
                            trDeliberacao.Visible = false;
                            trAprovacaoCMAS.Visible = false;
                            trAprovacaoDRADS.Visible = false;
                            trCancelarAprovacao.Visible = false;
                            btnCancelarAprovacao.Visible = false;
                            btnCancelarAprovacao.Enabled = false;
                            trDeliberacao.Visible = false;
                            #endregion                        
                        
                            break;
                        case(int)ESituacaoQuadro.EmAnaliseCMAS:

                            trComentarioCMAS.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                            trDeliberacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;

                            btnDevolverCMAS.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                            btnDevolverCMAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;

                            btnSalvarCMAS.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                            btnSalvarCMAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;

                            obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = false);
                            ObterControleBeneficioseventuais().ToList().ForEach(x => x.Enabled = false);
                            ObterControleEspecial().ToList().ForEach(x => x.Enabled = false);
                            ObterControleProgramasProjetos().ToList().ForEach(x => x.Enabled = false);
                            ObterControleIncentivoGestao().ToList().ForEach(x => x.Enabled = false);
                            ObterControleExAnterior().ToList().ForEach(x => x.Enabled = false);
                            ObterControlesComentarioCMAS().ToList().ForEach(x => x.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);
                            
                            #region proibicoes
                            txtComentario.Enabled = false;
                            txtComentario.Visible = true;
                            btnSalvar.Enabled = false;
                            btnSalvar.Visible = false;
                            btnCalcular.Enabled = false;
                            btnCalcular.Visible = false;
                            trAprovacaoDRADS.Visible = false;
                            trAprovacaoCMAS.Visible = false;
                            trFinalizarGestorCalculo.Visible = false;
                            trCancelarAprovacao.Visible = false;
                            btnCancelarAprovacao.Visible = false;
                            btnCancelarAprovacao.Enabled = false;
                            #endregion  

                            break;
                        case(int)ESituacaoQuadro.AprovadoCMAS:
                            
                            trComentarioCMAS.Visible = true;
                            trDeliberacao.Visible = true;
                            trCancelarAprovacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            btnCancelarAprovacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            btnCancelarAprovacao.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            rblDeliberacao.SelectedValue = "1";

                            obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = false);
                            ObterControleBeneficioseventuais().ToList().ForEach(x => x.Enabled = false);
                            ObterControleEspecial().ToList().ForEach(x => x.Enabled = false);
                            ObterControleProgramasProjetos().ToList().ForEach(x => x.Enabled = false);
                            ObterControleIncentivoGestao().ToList().ForEach(x => x.Enabled = false);
                            ObterControleExAnterior().ToList().ForEach(x => x.Enabled = false);
                            ObterControlesComentarioCMAS().ToList().ForEach(x => x.Enabled = false);
                            
                            
                            #region proibicoes
                            txtComentario.Enabled = false;
                            txtComentario.Visible = true;

                            btnSalvar.Enabled = false;
                            btnSalvar.Visible = false;

                            btnCalcular.Enabled = false;
                            btnCalcular.Visible = false;
                            
                            btnSalvarCMAS.Visible = false;
                            btnSalvarAprovacaoCMAS.Visible = false;
                            btnDevolverCMAS.Visible = false;

                            trAprovacaoDRADS.Visible = false;
                            trAprovacaoCMAS.Visible = false;
                            trFinalizarGestorCalculo.Visible = false;
                            #endregion  

                            break;
                        case(int)ESituacaoQuadro.RejeitadoCMAS:

                            trComentarioCMAS.Visible = true;
                            trDeliberacao.Visible = true;
                            trCancelarAprovacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            btnCancelarAprovacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            btnCancelarAprovacao.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            rblDeliberacao.SelectedValue = "3";

                            obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = false);
                            ObterControleBeneficioseventuais().ToList().ForEach(x => x.Enabled = false);
                            ObterControleEspecial().ToList().ForEach(x => x.Enabled = false);
                            ObterControleProgramasProjetos().ToList().ForEach(x => x.Enabled = false);
                            ObterControleIncentivoGestao().ToList().ForEach(x => x.Enabled = false);
                            ObterControleExAnterior().ToList().ForEach(x => x.Enabled = false);
                            ObterControlesComentarioCMAS().ToList().ForEach(x => x.Enabled = false);
                            
                            
                            #region proibicoes
                            txtComentario.Enabled = false;
                            txtComentario.Visible = true;

                            btnSalvar.Enabled = false;
                            btnSalvar.Visible = false;

                            btnCalcular.Enabled = false;
                            btnCalcular.Visible = false;

                            btnSalvarCMAS.Visible = false;
                            btnSalvarAprovacaoCMAS.Visible = false;
                            btnDevolverCMAS.Visible = false;
                            
                            trAprovacaoDRADS.Visible = false;
                            trAprovacaoCMAS.Visible = false;
                            trFinalizarGestorCalculo.Visible = false;
                            #endregion 
                            
                            break;
                        case (int)ESituacaoQuadro.BloqueioInicialAdministrativo:
                            
                            trCancelarAprovacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            btnCancelarAprovacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            btnCancelarAprovacao.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;

                            obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = false);
                            ObterControleBeneficioseventuais().ToList().ForEach(x => x.Enabled = false);
                            ObterControleEspecial().ToList().ForEach(x => x.Enabled = false);
                            ObterControleProgramasProjetos().ToList().ForEach(x => x.Enabled = false);
                            ObterControleIncentivoGestao().ToList().ForEach(x => x.Enabled = false);
                            ObterControleExAnterior().ToList().ForEach(x => x.Enabled = false);
                            ObterControlesComentarioCMAS().ToList().ForEach(x => x.Enabled = false);

                            #region proibicoes

                            txtComentario.Enabled = false;
                            txtComentario.Visible = true;

                            btnSalvar.Enabled = false;
                            btnSalvar.Visible = false;

                            btnCalcular.Enabled = false;
                            btnCalcular.Visible = false;

                            btnDevolverCMAS.Enabled = false;
                            btnDevolverCMAS.Visible = false;

                            btnSalvarCMAS.Visible = false;
                            btnSalvarAprovacaoCMAS.Visible = false;
                            btnDevolverCMAS.Visible = false;

                            trDeliberacao.Visible = false;
                            trAprovacaoDRADS.Visible = false;
                            trAprovacaoCMAS.Visible = false;
                            trFinalizarGestorCalculo.Visible = false;

                            #endregion

                            break;
                    }

                    ObterControlesCamposCalculados().ToList().ForEach(x => x.Enabled = false);

                }
                else 
                {
                    switch (quadro.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                        case (int)ESituacaoQuadro.DevolvidoDRADS:
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                        default:
                            trFinalizarGestorCalculo.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                            btnCalcular.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                            btnCalcular.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                            btnSalvar.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                            btnSalvar.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                            txtComentario.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                            txtComentario.Visible = true;

                            obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                            
                            #region proibicoes
                            trCancelarAprovacao.Visible = false;
                            trAprovacaoCMAS.Visible = false;
                            trAprovacaoDRADS.Visible = false;
                            btnCancelarAprovacao.Visible = false;
                            btnCancelarAprovacao.Enabled = false;
                            #endregion

                            break;
                        case (int)ESituacaoQuadro.Preenchido:
                            trAprovacaoDRADS.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador);

                            obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = false);

                            #region proibicoes
                            
                            txtComentario.Enabled = false;
                            txtComentario.Visible = true;

                            btnSalvar.Enabled = false;
                            btnSalvar.Visible = false;

                            btnCalcular.Enabled = false;
                            btnCalcular.Visible = false;

                            trAprovacaoCMAS.Visible = false;
                            trFinalizarGestorCalculo.Visible = false;
                            trCancelarAprovacao.Visible = false;
                            btnCancelarAprovacao.Visible = false;
                            btnCancelarAprovacao.Enabled = false;
                            
                            #endregion

                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            trAprovacaoCMAS.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;

                            obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = false);

                            #region proibicoes
                            txtComentario.Enabled = false;
                            txtComentario.Visible = true;

                            btnSalvar.Enabled = false;
                            btnSalvar.Visible = false;

                            btnCalcular.Enabled = false;
                            btnCalcular.Visible = false;

                            trAprovacaoDRADS.Visible = false;
                            trFinalizarGestorCalculo.Visible = false;
                            trCancelarAprovacao.Visible = false;
                            btnCancelarAprovacao.Visible = false;
                            btnCancelarAprovacao.Enabled = false;

                            #endregion

                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            trCancelarAprovacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;

                            btnCancelarAprovacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            btnCancelarAprovacao.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;

                            obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = false);

                            #region proibicoes
                            txtComentario.Enabled = false;
                            txtComentario.Visible = true;

                            btnSalvar.Enabled = false;
                            btnSalvar.Visible = false;

                            btnCalcular.Enabled = false;
                            btnCalcular.Visible = false;

                            trAprovacaoDRADS.Visible = false;
                            trAprovacaoCMAS.Visible = false;
                            trFinalizarGestorCalculo.Visible = false;
                            #endregion

                            break;
                        case (int)ESituacaoQuadro.BloqueioInicialAdministrativo:
                            trCancelarAprovacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            btnCancelarAprovacao.Visible = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            btnCancelarAprovacao.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador;
                            obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = false);
                            obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = false);

                            #region proibicoes

                            txtComentario.Enabled = false;
                            txtComentario.Visible = true;

                            btnSalvar.Enabled = false;
                            btnSalvar.Visible = false;

                            btnCalcular.Enabled = false;
                            btnCalcular.Visible = false;

                            trAprovacaoDRADS.Visible = false;
                            trAprovacaoCMAS.Visible = false;
                            trFinalizarGestorCalculo.Visible = false;

                            #endregion

                            break;
                    }
                    ObterControlesCamposCalculados().ToList().ForEach(x => x.Enabled = false);
                }

            }
            else
            {
                trFinalizarGestorCalculo.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                btnCalcular.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                btnCalcular.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                btnSalvar.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                btnSalvar.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                txtComentario.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                txtComentario.Visible = true;

                if (exercicio >= 2021)
                {
                    obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    ObterControleBeneficioseventuais().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    ObterControleEspecial().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    ObterControleProgramasProjetos().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    ObterControleIncentivoGestao().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    ObterControleExAnterior().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                }
                else
                {
                    obterControlesProtecaoBasica().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    obterControlesProtecaoEspecialMedia().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    obterControlesProtecaoEspecialAlta().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                        
                }


                #region proibicoes
                trAprovacaoCMAS.Visible = false;
                trAprovacaoDRADS.Visible = false;
                #endregion
            }
        }

        protected void btnFinalizarCalculo_Click(object sender, EventArgs e)
        {
            AlterarSituacaoQuadro(3);
        }

        protected void btnSalvarAprovacaoDRADS_Click(object sender, EventArgs e)
        {
            AlterarSituacaoQuadro(rblAprovacaoDRADS.SelectedValue == "1" ? 3 : 5);
        }

        protected void btnSalvarAprovacaoCMAS_Click(object sender, EventArgs e)
        {
            AlterarSituacaoQuadro(rblAprovacaoCMAS.SelectedValue == "1" ? 4 : 6);
        }

        protected void btnCancelarAprovacao_Click(object sender, EventArgs e)
        {
            AlterarSituacaoQuadro(1);
        }

        void AlterarSituacaoQuadro(int idSituacaoQuadro)
        {
            String msg = String.Empty;
            var exercicio = Convert.ToInt32(hdfAno.Value);
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == exercicio).FirstOrDefault();
                    if (quadro != null)
                    {
                        quadro.IdSituacaoQuadro = idSituacaoQuadro;
                        proxy.Service.SavePrefeituraSituacaoQuadro(quadro);
                        AplicarBloqueioControles(proxy);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                switch (idSituacaoQuadro)
                {
                    //case 1:
                    //    break;
                    case 2:
                        msg = "Registro da Execução Financeira finalizada com sucesso.";
                        break;
                    case 3:
                        msg = "Quadro de informações sobre a execução financeira foi encaminhado para aprovação pelo CMAS!";
                        break;
                    case 4:
                        msg = "Execução financeira aprovada com sucesso!";
                        break;
                    case 5:
                        msg = "Quadro de informações sobre a execução financeira foi disponibilizado para retificação do preenchimento pelo Órgão Gestor!";
                        break;
                    case 6:
                        msg = "Quadro de informações sobre a execução financeira foi novamente disponibilizado para retificação do preenchimento pelo Órgão Gestor!";
                        break;
                    default:
                        msg = "Desbloqueio efetuado com sucesso!";
                        break;
                }
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                trFinalizarGestorCalculo.Visible = trAprovacaoDRADS.Visible = trAprovacaoCMAS.Visible = trCancelarAprovacao.Visible = false;
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }

        protected void lstPrevisaoOrcamentaria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region helper

        #region clear

        private void Clear()
        {
            txtComentario.Text = string.Empty;
            this.clearProtecaoBasica();
            this.clearProtecaoEspecialMedia();
            this.clearProtecaoEspecialAlta();
            this.clearBeneficiosEventuais();
            this.clearProgramasProjetos();
            this.clearProtecaoEspecial();
            this.clearIncentivoGestao();
            this.clearExAnterior();

            this.clearTotalizarFMAS();
            this.clearTotalizarFEAS();
            this.clearTotalizarFNAS();
            this.clearTotalizar();

        }


        private void clearProtecaoBasica()
        {
            #region FMAS
            txtFMASPrevisaoInicialBasica.Text = "0,00";
            txtFMASResultadoAppFinanceirasBasica.Text = "0,00";
            txtFMASRecursosDisponibilizadosBasica.Text = "0,00";
            txtFMASValoresExecutadosBasica.Text = "0,00";
            txtFMASValoresReprogramadosBasica.Text = "0,00";
            txtFMASValoresDevolvidosBasica.Text = "0,00";
            txtFMASPorcentagensExecucaoBasica.Text = (0).ToString("P2");
            #endregion
            //var ValoresDevolvidosFEASReprogramado = "0,00";
            #region FEAS
            txtFEASPrevisaoInicialBasica.Text = "0,00";
            txtFEASResultadoAppFinanceirasBasica.Text = "0,00";
            txtFEASRecursosDisponibilizadosBasica.Text = "0,00";
            txtFEASValoresExecutadosBasica.Text = "0,00";
            txtFEASValoresReprogramadosBasica.Text = "0,00";
            txtFEASValoresDevolvidosBasica.Text = "0,00";
            txtValoresDevolvidosReprogramacao.Text = "0,00";
            txtFEASPorcentagensExecucaoBasica.Text = "0,00";
            txtPorcentagensDevolucaoReprogramacao.Text = "0,00";
            txtPrevisaoInicialReprogramacao.Text = "0,00";
            txtRecursosDisponibilizadosReprogramacao.Text = "0,00";
            txtResultadosAplicacaoReprogramacao.Text = "0,00";
            txtValoresExecutadosReprogramacao.Text = "0,00";
            #endregion

            #region FNAS
            txtFNASPrevisaoInicialBasica.Text = "0,00";
            txtFNASResultadoAppFinanceirasBasica.Text = "0,00";
            txtFNASRecursosDisponibilizadosBasica.Text = "0,00";
            txtFNASValoresExecutadosBasica.Text = "0,00";
            txtFNASValoresReprogramadosBasica.Text = "0,00";
            txtFNASValoresDevolvidosBasica.Text = "0,00";
            txtFNASPorcentagensExecucaoBasica.Text = (0).ToString("P2");
            #endregion
            //var RecursoDisponibilizadoFEASReprogramado = "0,00";
            //var PrevisaoInicialFEASReprogramado = "0,00";
            //var ResultadoAplicacaoFinanceiraReprogramado = "0,00";
            //var valoresExecutadosFEASReprogramado = "0,00";
            #region TOTAIS
            //TOTAL
            txtPrevisaoInicialBasica.Text = "0,00";
            //var resultadoAppFinanceira = (e.ResultadoAplicacaoFinanceiraFMAS + e.ResultadoAplicacaoFinanceiraFEAS + e.ResultadoAplicacaoFinanceiraFNAS);
            txtResultadoAppFinanceirasBasica.Text = "0,00";
            //var recursosDisponibilizados = e.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFEAS + e.RecursoDisponibilizadoFNAS;
            txtRecursosDisponibilizadosBasica.Text = "0,00";
            txtValoresExecutadosBasica.Text = "0,00";
            txtValoresReprogramadosBasica.Text = "0,00";
            txtValoresDevolvidosBasica.Text = "0,00";
            txtPorcentagensExecucaoBasica.Text = (0).ToString("P2");
            #endregion
        }

        private void clearProtecaoEspecialMedia()
        {
            #region FMAS
            txtFMASPrevisaoInicialMedia.Text = "0,00";
            txtFMASResultadoAppFinanceirasMedia.Text = "0,00";
            txtFMASRecursosDisponibilizadosMedia.Text = "0,00";
            txtFMASValoresExecutadosMedia.Text = "0,00";
            txtFMASValoresReprogramadosMedia.Text = "0,00";
            txtFMASValoresDevolvidosMedia.Text = "0,00";
            txtFMASPorcentagensExecucaoMedia.Text = (0).ToString("P2");
            #endregion

            #region FEAS
            txtFEASPrevisaoInicialMedia.Text = "0,00";
            txtFEASResultadoAppFinanceirasMedia.Text = "0,00";
            txtFEASRecursosDisponibilizadosMedia.Text = "0,00";
            txtFEASValoresExecutadosMedia.Text = "0,00";
            txtFEASValoresReprogramadosMedia.Text = "0,00";
            txtFEASValoresDevolvidosMedia.Text = "0,00";
            txtFEASPorcentagensExecucaoMedia.Text = (0).ToString("P2");
            #endregion

            #region FNAS
            txtFNASPrevisaoInicialMedia.Text = "0,00";
            txtFNASResultadoAppFinanceirasMedia.Text = "0,00";
            txtFNASRecursosDisponibilizadosMedia.Text = "0,00";
            txtFNASValoresExecutadosMedia.Text = "0,00";
            txtFNASValoresReprogramadosMedia.Text = "0,00";
            txtFNASValoresDevolvidosMedia.Text = "0,00";
            txtFNASPorcentagensExecucaoMedia.Text = (0).ToString("P2");
            #endregion

            #region TOTAL
            txtPrevisaoInicialMedia.Text = "0,00";
            //var resultadoAppFinanceira = "0,00";
            txtResultadoAppFinanceirasMedia.Text = "0,00";
            //var recursosDisponibilizados = "0,00";
            txtRecursosDisponibilizadosMedia.Text = "0,00";
            txtValoresExecutadosMedia.Text = "0,00";
            txtValoresReprogramadosMedia.Text = "0,00";
            txtValoresDevolvidosMedia.Text = "0,00";
            txtPorcentagensExecucaoMedia.Text = (0).ToString("P2");
            #endregion
        }

        private void clearProtecaoEspecialAlta()
        {
            #region FMAS
            txtFMASPrevisaoInicialAlta.Text = "0,00";
            txtFMASResultadoAppFinanceirasAlta.Text = "0,00";
            txtFMASRecursosDisponibilizadosAlta.Text = "0,00";
            txtFMASValoresExecutadosAlta.Text = "0,00";
            txtFMASValoresReprogramadosAlta.Text = "0,00";
            txtFMASValoresDevolvidosAlta.Text = "0,00";
            txtFMASPorcentagensExecucaoAlta.Text = (0).ToString("P2");
            #endregion

            #region FEAS
            txtFEASPrevisaoInicialAlta.Text = "0,00";
            txtFEASResultadoAppFinanceirasAlta.Text = "0,00";
            txtFEASRecursosDisponibilizadosAlta.Text = "0,00";
            txtFEASValoresExecutadosAlta.Text = "0,00";
            txtFEASValoresReprogramadosAlta.Text = "0,00";
            txtFEASValoresDevolvidosAlta.Text = "0,00";
            txtFEASPorcentagensExecucaoAlta.Text = (0).ToString("P2");
            #endregion

            #region FNAS
            txtFNASPrevisaoInicialAlta.Text = "0,00";
            txtFNASResultadoAppFinanceirasAlta.Text = "0,00";
            txtFNASRecursosDisponibilizadosAlta.Text = "0,00";
            txtFNASValoresExecutadosAlta.Text = "0,00";
            txtFNASValoresReprogramadosAlta.Text = "0,00";
            txtFNASValoresDevolvidosAlta.Text = "0,00";
            txtFNASPorcentagensExecucaoAlta.Text = (0).ToString("P2");
            #endregion

            #region TOTAL
            txtPrevisaoInicialAlta.Text = "0,00";
            //var resultadoAppFinanceira = "0,00";
            txtResultadoAppFinanceirasAlta.Text = "0,00";
            //var recursosDisponibilizados = "0,00";
            txtRecursosDisponibilizadosAlta.Text = "0,00";
            txtValoresExecutadosAlta.Text = "0,00";
            txtValoresReprogramadosAlta.Text = "0,00";
            txtValoresDevolvidosAlta.Text = "0,00";
            txtPorcentagensExecucaoAlta.Text = (0).ToString("P2");
            #endregion
        }

        private void clearBeneficiosEventuais()
        {

            #region FMAS
            txtFMASPrevisaoInicialBeneficiosEventuais.Text = "0,00";
            txtFMASRecursosDisponibilizadosBeneficiosEventuais.Text = "0,00";
            txtFMASResultadoAppFinanceirasBeneficiosEventuais.Text = "0,00";
            txtFMASValoresExecutadosBeneficiosEventuais.Text = "0,00";
            txtFMASValoresExecutadosBeneficiosEventuais.Text = "0,00";
            txtFMASValoresReprogramadosBeneficiosEventuais.Text = "0,00";
            txtFMASValoresDevolvidosBeneficiosEventuais.Text = "0,00";
            txtFMASPorcentagensExecucaoBeneficiosEventuais.Text = (0).ToString("P2");
            #endregion

            #region TOTAIS
            txtTotalPrevisaoInicialBeneficiosEventuais.Text = "0,00";
            txtTotalRecursosDisponibilizadosBeneficiosEventuais.Text = "0,00";
            txtTotalResultadoAppFinanceirasBeneficiosEventuais.Text = "0,00";
            txtTotalValoresExecutadosBeneficiosEventuais.Text = "0,00";
            txtTotalValoresReprogramadosBeneficiosEventuais.Text = "0,00";
            txtTotalValoresDevolvidosBeneficiosEventuais.Text = "0,00";
            txtTotalPorcentagensExecucaoBeneficiosEventuais.Text = (0).ToString("P2");
            #endregion
        }

        private void clearProgramasProjetos()
        {

            #region FMAS
            txtFMASPrevisaoInicialProgramasProjetos.Text = "0,00";
            txtFMASRecursosDisponibilizadosProgramasProjetos.Text = "0,00";
            txtFMASResultadoAppFinanceirasProgramasProjetos.Text = "0,00";
            txtFMASValoresExecutadosProgramasProjetos.Text = "0,00";
            txtFMASValoresExecutadosProgramasProjetos.Text = "0,00";
            txtFMASValoresReprogramadosProgramasProjetos.Text = "0,00";
            txtFMASValoresDevolvidosProgramasProjetos.Text = "0,00";
            txtFMASPorcentagensExecucaoProgramasProjetos.Text = (0).ToString("P2");
            #endregion

            #region FNAS
            txtFNASPrevisaoInicialProgramasProjetos.Text = "0,00";
            txtFNASRecursosDisponibilizadosProgramasProjetos.Text = "0,00";
            txtFNASResultadoAppFinanceirasProgramasProjetos.Text = "0,00";
            txtFNASValoresExecutadosProgramasProjetos.Text = "0,00";
            txtFNASValoresReprogramadosProgramasProjetos.Text = "0,00";
            txtFNASValoresDevolvidosProgramasProjetos.Text = "0,00";
            txtFNASPorcentagensExecucaoProgramasProjetos.Text = (0).ToString("P2");
            #endregion

            #region TOTAIS
            txtTotalPrevisaoInicialProgramasProjetos.Text = "0,00";
            txtTotalRecursosDisponibilizadosProgramasProjetos.Text = "0,00";
            txtTotalResultadoAppFinanceirasProgramasProjetos.Text = "0,00";
            txtTotalValoresExecutadosProgramasProjetos.Text = "0,00";
            txtTotalValoresReprogramadosProgramasProjetos.Text = "0,00";
            txtTotalValoresDevolvidosProgramasProjetos.Text = "0,00";
            txtTotalPorcentagensExecucaoProgramasProjetos.Text = (0).ToString("P2");
            #endregion
        }

        private void clearExAnterior()
        {
            #region FENAS
            txtFNASPrevisaoInicialExercicioAnterior.Text = "0,00";
            txtFNASRecursosDisponibilizadosExercicioAnterior.Text = "0,00";
            txtFNASResultadoAppFinanceirasExercicioAnterior.Text = "0,00";
            txtFNASValoresExecutadosExercicioAnterior.Text = "0,00";
            txtFNASValoresReprogramadosExercicioAnterior.Text = "0,00";
            txtFNASValoresDevolvidosExercicioAnterior.Text = "0,00";
            txtFNASPorcentagensExecucaoExercicioAnterior.Text = (0).ToString("P2");
            #endregion 
        
            #region TOTAIS
            #endregion
        }

        private void clearIncentivoGestao()
        {
            #region FENAS
            txtFNASPrevisaoInicialIncentivoGestao.Text = "0,00";
            txtFNASRecursosDisponibilizadosIncentivoGestao.Text = "0,00";
            txtFNASResultadoAppFinanceirasIncentivoGestao.Text = "0,00";
            txtFNASValoresExecutadosIncentivoGestao.Text = "0,00";
            txtFNASValoresReprogramadosIncentivoGestao.Text = "0,00";
            txtFNASValoresDevolvidosIncentivoGestao.Text = "0,00";
            txtFNASPorcentagensExecucaoIncentivoGestao.Text = (0).ToString("P2");
            #endregion

            #region TOTAIS
            txtTotalPrevisaoInicialIncentivoGestao.Text = "0,00";
            txtTotalRecursosDisponibilizadosIncentivoGestao.Text = "0,00";
            txtTotalResultadoAppFinanceirasIncentivoGestao.Text = "0,00";
            txtTotalValoresExecutadosIncentivoGestao.Text = "0,00";
            txtTotalValoresReprogramadosIncentivoGestao.Text = "0,00";
            txtTotalValoresDevolvidosIncentivoGestao.Text = "0,00";
            txtTotalPorcentagensExecucaoIncentivoGestao.Text = (0).ToString("P2");
            #endregion
        }

        private void clearProtecaoEspecial()
        {
            #region FENAS
            txtFNASPrevisaoInicialProtecaoSocialEspecial.Text = "0,00";
            txtFNASRecursosDisponibilizadosProtecaoSocialEspecial.Text = "0,00";
            txtFNASResultadoAppFinanceirasProtecaoSocialEspecial.Text = "0,00";
            txtFNASValoresExecutadosProtecaoSocialEspecial.Text = "0,00";
            txtFNASValoresReprogramadosProtecaoSocialEspecial.Text = "0,00";
            txtFNASValoresDevolvidosProtecaoSocialEspecial.Text = "0,00";
            txtFNASPorcentagensExecucaoProtecaoSocialEspecial.Text = (0).ToString("P2");
            #endregion

            #region TOTAIS
            txtTotalPrevisaoInicialProtecaoSocialEspecial.Text = "0,00";
            txtTotalRecursosDisponibilizadosProtecaoSocialEspecial.Text = "0,00";
            txtTotalResultadoAppFinanceirasProtecaoSocialEspecial.Text = "0,00";
            txtTotalValoresExecutadosProtecaoSocialEspecial.Text = "0,00";
            txtTotalValoresReprogramadosProtecaoSocialEspecial.Text = "0,00";
            txtTotalValoresDevolvidosProtecaoSocialEspecial.Text = "0,00";
            txtTotalPorcentagensExecucaoProtecaoSocialEspecial.Text = (0).ToString("P2");
            #endregion
        }


        void clearTotalizarFMAS()
        {
            lblFMASPrevisaoInicial.Text = "0,00";
            lblFMASRecursosDisponibilizados.Text = "0,00";
            lblFMASResultadoAppFinanceiras.Text = "0,00";
            lblFMASValoresExecutados.Text = "0,00";
            lblFMASValoresReprogramados.Text = "0,00";
            lblFMASValoresDevolvidos.Text = "0,00";
            lblFMASPorcentagensExecucao.Text = "0,00";
        }

        void clearTotalizarFEAS()
        {
            lblFEASPrevisaoInicial.Text = "0,00";
            lblFEASRecursosDisponibilizados.Text = "0,00";
            lblFEASResultadoAppFinanceiras.Text = "0,00";
            lblFEASValoresExecutados.Text = "0,00";
            lblFEASValoresReprogramados.Text = "0,00";
            lblFEASValoresDevolvidos.Text = "0,00";
            lblFEASPorcentagensExecucao.Text = "0,00";
        }

        void clearTotalizarFNAS()
        {
            lblFNASPrevisaoInicial.Text = "0,00";
            lblFNASRecursosDisponibilizados.Text = "0,00";
            lblFNASResultadoAppFinanceiras.Text = "0,00";
            lblFNASValoresExecutados.Text = "0,00";
            lblFNASValoresReprogramados.Text = "0,00";
            lblFNASValoresDevolvidos.Text = "0,00";
            lblFNASPorcentagensExecucao.Text = "0,00";
        }

        void clearTotalizar()
        {
            lblPrevisaoInicial.Text = "0,00";
            lblRecursosDisponibilizados.Text = "0,00";
            lblResultadoAppFinanceiras.Text = "0,00";
            lblValoresExecutados.Text = "0,00";
            lblValoresReprogramados.Text = "0,00";
            lblValoresDevolvidos.Text = "0,00";
            lblPorcentagensExecucao.Text = "0,00";
        }
        #endregion

        #region controles

        //Text

        private WebControl[] obterControlesProtecaoBasica()
        {
            WebControl[] controles = {
            #region FMAS
            txtFMASPrevisaoInicialBasica
            , txtFMASResultadoAppFinanceirasBasica
            , txtFMASRecursosDisponibilizadosBasica
            , txtFMASValoresExecutadosBasica
            , txtFMASValoresReprogramadosBasica
            #endregion
            //var ValoresDevolvidosFEASReprogramado = "0,00";
            #region FEAS
            , txtFEASPrevisaoInicialBasica
            , txtFEASResultadoAppFinanceirasBasica
            , txtFEASRecursosDisponibilizadosBasica
            , txtFEASValoresExecutadosBasica
            , txtFEASValoresReprogramadosBasica
            #endregion



            , txtPrevisaoInicialReprogramacao
            , txtRecursosDisponibilizadosReprogramacao
            , txtResultadosAplicacaoReprogramacao
            , txtValoresExecutadosReprogramacao
            ,txtValoresReprogramacao
            

            #region FNAS
            , txtFNASPrevisaoInicialBasica
            , txtFNASResultadoAppFinanceirasBasica
            , txtFNASRecursosDisponibilizadosBasica
            , txtFNASValoresExecutadosBasica
            , txtFNASValoresReprogramadosBasica

            #endregion
            //var RecursoDisponibilizadoFEASReprogramado = "0,00";
            //var PrevisaoInicialFEASReprogramado = "0,00";
            //var ResultadoAplicacaoFinanceiraReprogramado = "0,00";
            //var valoresExecutadosFEASReprogramado = "0,00";
            #region TOTAIS
            //TOTAL
            ,txtPrevisaoInicialBasica
            //var resultadoAppFinanceira = (e.ResultadoAplicacaoFinanceiraFMAS + e.ResultadoAplicacaoFinanceiraFEAS + e.ResultadoAplicacaoFinanceiraFNAS);
            , txtResultadoAppFinanceirasBasica
            //var recursosDisponibilizados = e.RecursoDisponibilizadoFMAS + e.RecursoDisponibilizadoFEAS + e.RecursoDisponibilizadoFNAS;
            , txtRecursosDisponibilizadosBasica
            , txtValoresExecutadosBasica
            , txtValoresReprogramadosBasica
              };
            #endregion
            return controles;
        }

        private WebControl[] obterControlesProtecaoEspecialMedia()
        {
            WebControl[] controles = {
                #region FMAS
                  txtFMASPrevisaoInicialMedia
                , txtFMASResultadoAppFinanceirasMedia
                , txtFMASRecursosDisponibilizadosMedia
                , txtFMASValoresExecutadosMedia
                , txtFMASValoresReprogramadosMedia
                #endregion

                #region FEAS
                , txtFEASPrevisaoInicialMedia
                , txtFEASResultadoAppFinanceirasMedia
                , txtFEASRecursosDisponibilizadosMedia
                , txtFEASValoresExecutadosMedia
                , txtFEASValoresReprogramadosMedia
                #endregion

                #region FNAS
                , txtFNASPrevisaoInicialMedia
                , txtFNASResultadoAppFinanceirasMedia
                , txtFNASRecursosDisponibilizadosMedia
                , txtFNASValoresExecutadosMedia
                , txtFNASValoresReprogramadosMedia
                #endregion

                #region TOTAL
                , txtPrevisaoInicialMedia
                //var resultadoAppFinanceira = "0,00";
                , txtResultadoAppFinanceirasMedia
                //var recursosDisponibilizados = "0,00";
                , txtRecursosDisponibilizadosMedia
                , txtValoresExecutadosMedia
                , txtValoresReprogramadosMedia
                };
                #endregion
            return controles;
        }

        private WebControl[] obterControlesProtecaoEspecialAlta()
        {
            WebControl[] controles = {
                #region FMAS
                txtFMASPrevisaoInicialAlta
                , txtFMASResultadoAppFinanceirasAlta
                , txtFMASRecursosDisponibilizadosAlta
                , txtFMASValoresExecutadosAlta
                , txtFMASValoresReprogramadosAlta

                #endregion

                #region FEAS
                , txtFEASPrevisaoInicialAlta
                , txtFEASResultadoAppFinanceirasAlta
                , txtFEASRecursosDisponibilizadosAlta
                , txtFEASValoresExecutadosAlta
                , txtFEASValoresReprogramadosAlta
                #endregion

                #region FNAS
                , txtFNASPrevisaoInicialAlta
                , txtFNASResultadoAppFinanceirasAlta
                , txtFNASRecursosDisponibilizadosAlta
                , txtFNASValoresExecutadosAlta
                , txtFNASValoresReprogramadosAlta
                #endregion

                #region TOTAL
                , txtPrevisaoInicialAlta
                //var resultadoAppFinanceira = "0,00";
                , txtResultadoAppFinanceirasAlta
                //var recursosDisponibilizados = "0,00";
                , txtRecursosDisponibilizadosAlta
                , txtValoresExecutadosAlta
                , txtValoresReprogramadosAlta
            };
                #endregion
            return controles;
        }

        private WebControl[] ObterControleBeneficioseventuais()
        {
            WebControl[] controles = {
               
                txtFMASPrevisaoInicialBeneficiosEventuais
               , txtFMASRecursosDisponibilizadosBeneficiosEventuais
               , txtFMASResultadoAppFinanceirasBeneficiosEventuais
               , txtFMASValoresExecutadosBeneficiosEventuais
               , txtFMASValoresReprogramadosBeneficiosEventuais
                                     };
               
            return controles;
        }
        
        private WebControl[] ObterControleProgramasProjetos()
        {
            WebControl[] controles ={
            #region FMAS
             txtFMASPrevisaoInicialProgramasProjetos
            , txtFMASRecursosDisponibilizadosProgramasProjetos
            , txtFMASResultadoAppFinanceirasProgramasProjetos
            , txtFMASValoresExecutadosProgramasProjetos
            , txtFMASValoresReprogramadosProgramasProjetos

            #endregion

            #region FNAS
            ,txtFNASPrevisaoInicialProgramasProjetos
            ,txtFNASRecursosDisponibilizadosProgramasProjetos
            ,txtFNASResultadoAppFinanceirasProgramasProjetos
            ,txtFNASValoresExecutadosProgramasProjetos
            ,txtFNASValoresReprogramadosProgramasProjetos
            #endregion
            
            #region TOTAIS

            #endregion
                                        };
            return controles;
        }

        private WebControl[] ObterControleEspecial() 
        {
            WebControl[] controles ={
            
            #region FNAS
            txtFNASPrevisaoInicialProtecaoSocialEspecial
            ,txtFNASRecursosDisponibilizadosProtecaoSocialEspecial
            ,txtFNASResultadoAppFinanceirasProtecaoSocialEspecial
            ,txtFNASValoresExecutadosProtecaoSocialEspecial
            ,txtFNASValoresReprogramadosProtecaoSocialEspecial
            #endregion
            };
            return controles;
        }

        private WebControl[] ObterControleIncentivoGestao() 
        {
            WebControl[] controles ={

            #region FNAS
            txtFNASPrevisaoInicialIncentivoGestao
            ,txtFNASRecursosDisponibilizadosIncentivoGestao
            ,txtFNASResultadoAppFinanceirasIncentivoGestao
            ,txtFNASValoresExecutadosIncentivoGestao
            ,txtFNASValoresReprogramadosIncentivoGestao
            #endregion
            
            };
            return controles;
        }

        private WebControl[] ObterControleExAnterior() 
        {
            WebControl[] controles ={
            #region FNAS
            txtFNASPrevisaoInicialExercicioAnterior
            ,txtFNASRecursosDisponibilizadosExercicioAnterior
            ,txtFNASResultadoAppFinanceirasExercicioAnterior
            ,txtFNASValoresExecutadosExercicioAnterior
            ,txtFNASValoresReprogramadosExercicioAnterior
            #endregion 
            
            #region TOTAIS
            #endregion

            };
            return controles;
        }

        private WebControl[] ObterControlesCamposCalculados() 
        {
            WebControl[] controles ={
              txtFMASValoresDevolvidosBasica
            , txtFMASPorcentagensExecucaoBasica

			, txtFEASValoresDevolvidosBasica
            , txtFEASPorcentagensExecucaoBasica
							
            , txtFMASValoresDevolvidosMedia
            , txtFMASPorcentagensExecucaoMedia

            , txtFEASValoresDevolvidosMedia
            , txtFEASPorcentagensExecucaoMedia

            , txtFNASValoresDevolvidosMedia
            , txtFNASPorcentagensExecucaoMedia

            , txtValoresDevolvidosMedia
            , txtPorcentagensExecucaoMedia

            , txtFMASValoresDevolvidosAlta
            , txtFMASPorcentagensExecucaoAlta

            , txtFEASValoresDevolvidosAlta
            , txtFEASPorcentagensExecucaoAlta				

            , txtFNASValoresDevolvidosAlta
            , txtFNASPorcentagensExecucaoAlta
				
				
            , txtValoresDevolvidosBasica
            , txtPorcentagensExecucaoBasica 			
			
			, txtValoresDevolvidosReprogramacao
            , txtPorcentagensDevolucaoReprogramacao				
			
			, txtFMASValoresDevolvidosBeneficiosEventuais
            , txtFMASPorcentagensExecucaoBeneficiosEventuais
			
			, txtFNASValoresDevolvidosProgramasProjetos
            , txtFNASPorcentagensExecucaoProgramasProjetos
			
		    , txtFMASValoresDevolvidosProgramasProjetos
            , txtFMASPorcentagensExecucaoProgramasProjetos
							
		    ,txtTotalPrevisaoInicialProgramasProjetos
            ,txtTotalRecursosDisponibilizadosProgramasProjetos
            ,txtTotalResultadoAppFinanceirasProgramasProjetos
            ,txtTotalValoresExecutadosProgramasProjetos
            ,txtTotalValoresReprogramadosProgramasProjetos
            ,txtTotalValoresDevolvidosProgramasProjetos
            ,txtTotalPorcentagensExecucaoProgramasProjetos
			
            ,txtFNASValoresDevolvidosProtecaoSocialEspecial
            ,txtFNASPorcentagensExecucaoProtecaoSocialEspecial


            ,txtTotalPrevisaoInicialProtecaoSocialEspecial
            ,txtTotalRecursosDisponibilizadosProtecaoSocialEspecial
            ,txtTotalResultadoAppFinanceirasProtecaoSocialEspecial
            ,txtTotalValoresExecutadosProtecaoSocialEspecial
            ,txtTotalValoresReprogramadosProtecaoSocialEspecial
            ,txtTotalValoresDevolvidosProtecaoSocialEspecial
            ,txtTotalPorcentagensExecucaoProtecaoSocialEspecial
			
			,txtFNASValoresDevolvidosIncentivoGestao
            ,txtFNASPorcentagensExecucaoIncentivoGestao
			
            ,txtTotalPrevisaoInicialIncentivoGestao
            ,txtTotalRecursosDisponibilizadosIncentivoGestao
            ,txtTotalResultadoAppFinanceirasIncentivoGestao
            ,txtTotalValoresExecutadosIncentivoGestao
            ,txtTotalValoresReprogramadosIncentivoGestao
            ,txtTotalValoresDevolvidosIncentivoGestao
            ,txtTotalPorcentagensExecucaoIncentivoGestao

            ,txtFNASValoresDevolvidosExercicioAnterior
            ,txtFNASPorcentagensExecucaoExercicioAnterior

            ,txtPrevisaoInicialBasica
            ,txtRecursosDisponibilizadosBasica
            ,txtResultadoAppFinanceirasBasica
            ,txtValoresExecutadosBasica
            ,txtValoresReprogramadosBasica

            ,txtPrevisaoInicialMedia
            ,txtRecursosDisponibilizadosMedia
            ,txtResultadoAppFinanceirasMedia
            ,txtValoresExecutadosMedia
            ,txtValoresReprogramadosMedia

            ,txtPrevisaoInicialAlta
            ,txtRecursosDisponibilizadosAlta
            ,txtResultadoAppFinanceirasAlta
            ,txtValoresExecutadosAlta
            ,txtValoresReprogramadosAlta

            ,txtTotalPrevisaoInicialBeneficiosEventuais
            ,txtTotalRecursosDisponibilizadosBeneficiosEventuais
            ,txtTotalResultadoAppFinanceirasBeneficiosEventuais
            ,txtTotalValoresExecutadosBeneficiosEventuais
            ,txtTotalValoresReprogramadosBeneficiosEventuais

                                   };
            return controles;

        }

        private WebControl[] ObterControlesComentarioCMAS() 
        {
            WebControl[] controles = {
              txtComentario2
              ,txtDataReuniao
              ,txtNumeroConselheiros
              ,txtNumeroAta
              ,txtNumeroResolucao
              ,txtDataPublicacao
              ,rblDeliberacao
            };
            return controles;
        }

        //Label

        private WebControl[] obterControlesTotalizarFMAS()
        {
            WebControl[] controles = {
                 lblFMASPrevisaoInicial
                , lblFMASRecursosDisponibilizados
                , lblFMASResultadoAppFinanceiras
                , lblFMASValoresExecutados
                , lblFMASValoresReprogramados
                , lblFMASValoresDevolvidos
                , lblFMASPorcentagensExecucao
        };
            return controles;
        }

        private WebControl[] obterControlesTotalizarFEAS()
        {
            WebControl[] controles = {
                    lblFEASPrevisaoInicial
                    , lblFEASRecursosDisponibilizados
                    , lblFEASResultadoAppFinanceiras
                    , lblFEASValoresExecutados
                    , lblFEASValoresReprogramados
                    , lblFEASValoresDevolvidos
                    , lblFEASPorcentagensExecucao             
            };

            return controles;
        }

        private WebControl[] obterControlesTotalizarFNAS()
        {
            WebControl[] controles = {
                    lblFNASPrevisaoInicial
                    , lblFNASRecursosDisponibilizados
                    , lblFNASResultadoAppFinanceiras
                    , lblFNASValoresExecutados
                    , lblFNASValoresReprogramados
                    , lblFNASValoresDevolvidos
                    , lblFNASPorcentagensExecucao
            };
            return controles;
        }

        private WebControl[] obterControlesTotalizar()
        {
            WebControl[] controles =
            {
             lblPrevisaoInicial
            , lblRecursosDisponibilizados
            , lblResultadoAppFinanceiras
            , lblValoresExecutados
            , lblValoresReprogramados
            , lblValoresDevolvidos
            , lblPorcentagensExecucao
            };
            return controles;
        } 
        #endregion

        protected void btnSalvarCMAS_Click(object sender, EventArgs e)
        {
            string msg = String.Empty;
            try
            {

                if (SalvarComentarioEDeliberacaoCMAS(3) != true)
                {
                    throw new ArgumentException("Comentários e Parecer do Conselho Municipal de Assistência Social não foi preenchido.");
                } 


                if (rblDeliberacao.SelectedValue == "1")
                {

                    SalvarDeliberacaoCMAS();

                }
                else if (rblDeliberacao.SelectedValue == "3")
                {
                    SalvarDeliberacaoCMAS();
                    AlterarSituacaoQuadro(8);
                }
            }
            catch (Exception i)
            {

                msg = i.Message;
            }

            if (!String.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
                lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
            }
            
        }
        #endregion

        protected void btnDevolverCMAS_Click(object sender, EventArgs e)
        {
            SalvarComentarioEDeliberacaoCMAS(6);
            //SalvarDeliberacaoCMAS();
            AlterarSituacaoQuadro(6);
        }

 
    }
}