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
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.UI.Web.BlocoI;
using Seds.PMAS.QUADRIENAL.UI.Web.Usuario;
using Seds.PMAS.QUADRIENAL.UI.Processos;


namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoV
{
    public partial class FPrestacaoDeContas : System.Web.UI.Page
    {
        #region properties
        private static List<int> Exercicios = new List<int>() { 2021, 2022, 2023, 2024 };
        private List<ExecucaoFinanceiraInfo> sessionExecucaoFinanceira;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? "2021" : this.hdfAno.Value;

            if (Session["ExercicioDespesa"] != null)
            {
                this.hdfAno.Value = Session["ExercicioDespesa"].ToString();
                Session["ExercicioDespesa"] = null;
            }

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);


                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                AdicionarEventosJs();


                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    CarregarPrestacaoDeContas(prefeituras);
                    AplicarBloqueioControles(proxy);
                    loadGestor(prefeituras);
                }

            }


            LoadExercicios();
        }

        public void loadGestor(Processos.Prefeituras prefeituras)
        {

            var gestor = prefeituras.GetAtualGestorMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            string nomeGestorMunincipal = gestor.Nome.ToString();

            var presidente = prefeituras.GetConselhoMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            var presidenteAtual = presidente.IdUsuarioPresidente;

        }

        private void AplicarBloqueioControles(ProxyPrefeitura proxy)
        {
            BloqueioPorQuadro(proxy);
        }



        private void LoadExercicios()
        {
            this.btnExercicio1.Text = FPrestacaoDeContas.Exercicios[0].ToString();
            this.btnExercicio2.Text = FPrestacaoDeContas.Exercicios[1].ToString();
            this.btnExercicio3.Text = FPrestacaoDeContas.Exercicios[2].ToString();
            this.btnExercicio4.Text = FPrestacaoDeContas.Exercicios[3].ToString();

            exercicioTitulo();

            if (FPrestacaoDeContas.Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
                InibeReprogramacaoProgramasProjetos();
            }
            if (FPrestacaoDeContas.Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
                InibeReprogramacaoProgramasProjetos();
            }

            if (FPrestacaoDeContas.Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
                ExibeReprogramacaoProgramasProjetos();
            }

            if (FPrestacaoDeContas.Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-info-seds";
                ExibeReprogramacaoProgramasProjetos();
            }

        }



        protected void btnExercicio1_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[0].ToString();
            CarregarLabelsPorExercicio(Exercicios[0]);
            exercicioTitulo();
            clearHDN();
            InibeReprogramacaoProgramasProjetos();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                CarregarPrestacaoDeContas(prefeituras);
            }
        }

        protected void btnExercicio2_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[1].ToString();
            CarregarLabelsPorExercicio(Exercicios[1]);
            exercicioTitulo();
            clearHDN();
            InibeReprogramacaoProgramasProjetos();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                CarregarPrestacaoDeContas(prefeituras);
            }
        }

        protected void btnExercicio3_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[2].ToString();
            CarregarLabelsPorExercicio(Exercicios[2]);
            exercicioTitulo();
            clearHDN();
            ExibeReprogramacaoProgramasProjetos();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                CarregarPrestacaoDeContas(prefeituras);
            }
        }

        protected void btnExercicio4_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[3].ToString();
            CarregarLabelsPorExercicio(Exercicios[3]);
            exercicioTitulo();
            clearHDN();
            ExibeReprogramacaoProgramasProjetos();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                CarregarPrestacaoDeContas(prefeituras);
            }
        }

        private void CarregarLabelsPorExercicio(int exercicio)
        {
            if (FPrestacaoDeContas.Exercicios[0] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-info-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";

                btnCalcular.Enabled = false;
                btnSalvar.Enabled = false;
                tbInconsistencias.Visible = false;
            }
            if (FPrestacaoDeContas.Exercicios[1] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-info-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";
                btnCalcular.Enabled = true;
                btnSalvar.Enabled = true;
                tbInconsistencias.Visible = false;
            }

            if (FPrestacaoDeContas.Exercicios[2] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-info-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";
                btnCalcular.Enabled = true;
                btnSalvar.Enabled = true;
                tbInconsistencias.Visible = false;
            }

            if (FPrestacaoDeContas.Exercicios[3] == exercicio)
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

        private void exercicioTitulo()
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            if (FPrestacaoDeContas.Exercicios[0] == exercicio)
            {
                lblExercicio.Text = "2021";
                lblExercicio2.Text = "2021";
                lblExercicio3.Text = "2021";
                lblExercicio4.Text = "2021";
                lblExercicio5.Text = "2021";

            }

            if (FPrestacaoDeContas.Exercicios[1] == exercicio)
            {
                lblExercicio.Text = "2022";
                lblExercicio2.Text = "2022";
                lblExercicio3.Text = "2022";
                lblExercicio4.Text = "2022";
                lblExercicio5.Text = "2022";
            }

            if (FPrestacaoDeContas.Exercicios[2] == exercicio)
            {
                lblExercicio.Text = "2023";
                lblExercicio2.Text = "2023";
                lblExercicio3.Text = "2023";
                lblExercicio4.Text = "2023";
                lblExercicio5.Text = "2023";
            }

            if (FPrestacaoDeContas.Exercicios[3] == exercicio)
            {
                lblExercicio.Text = "2024";
                lblExercicio2.Text = "2024";
                lblExercicio3.Text = "2024";
                lblExercicio4.Text = "2024";
                lblExercicio5.Text = "2024";
            }
        }


        private void InibeReprogramacaoProgramasProjetos() 
        {
            tdSubTitulo.RowSpan = 9;
            trReprogramacaoProgramasProjetos.Visible = false;

            trDemandasParlamentaresBasica.Visible = false;
            trReprogramacaoDemandasParlamentaresBasica.Visible = false;

            trDemandasParlamentaresMedia.Visible = false;
            trReprogramacaoDemandasParlamentaresMedia.Visible = false;

            trDemandasParlamentaresAlta.Visible = false;
            trReprogramacaoDemandasParlamentaresAlta.Visible = false;

            trDemandasParlamentaresBeneficiosEventuais.Visible = false;
            trReprogramacaoDemandasParlamentaresBeneficiosEventuais.Visible = false;

        }

        private void ExibeReprogramacaoProgramasProjetos() 
        {
            tdSubTitulo.RowSpan = 18;
            trReprogramacaoProgramasProjetos.Visible = true;
            
            trDemandasParlamentaresBasica.Visible = true;
            trReprogramacaoDemandasParlamentaresBasica.Visible = true;
            
            trDemandasParlamentaresMedia.Visible = true;
            trReprogramacaoDemandasParlamentaresMedia.Visible = true;
            
            trDemandasParlamentaresAlta.Visible = true;
            trReprogramacaoDemandasParlamentaresAlta.Visible = true;
            
            trDemandasParlamentaresBeneficiosEventuais.Visible = true;
            trReprogramacaoDemandasParlamentaresBeneficiosEventuais.Visible = true;
        }

        private void CarregarPrestacaoDeContas(Prefeituras prefeituras)
        {

            int exercicio = Convert.ToInt32(hdfAno.Value);
            int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            var b = carregarDemostrativoRecursosEstaduaisProtecaoBasica(idPrefeitura);
            var m = carregarDemostrativoRecursosEstaduaisProtecaoMedia(idPrefeitura);
            var a = carregarDemostrativoRecursosEstaduaisProtecaoAlta(idPrefeitura);
            var pp = carregarDemonstrativoRecursosEstaduaisProgramasProjetos(idPrefeitura);
            var be = carregarDemonstrativoRecursosEstaduaisBeneficiosEventuais(idPrefeitura);

            List<ExecucaoFinanceiraInfo> execucoes = prefeituras.GetExecucaoFinanceira(idPrefeitura);
            Session["sessionExecucaoFinanceira"] = execucoes;

            if (execucoes != null)
            {
                var execucao = execucoes.Where(x => x.Exercicio == exercicio);

                if (execucao != null)
                {
                    var basica = execucao.FirstOrDefault(e => e.IdTipoProtecao == 1);
                    var media = execucao.FirstOrDefault(e => e.IdTipoProtecao == 2);
                    var alta = execucao.FirstOrDefault(e => e.IdTipoProtecao == 3);

                    var ProgramasProjetos = execucao.FirstOrDefault(e => e.IdTipoProtecao == 4);
                    var BeneficiosEventuais = execucao.FirstOrDefault(e => e.IdTipoProtecao == 5);
                    var ExercicioAnterior = execucao.FirstOrDefault(e => e.IdTipoProtecao == 8);

                    var reprogramadoBasica = execucao.FirstOrDefault(e => e.IdTipoProtecao == 9);
                    var reprogramadoMedia = execucao.FirstOrDefault(e => e.IdTipoProtecao == 10);
                    var reprogramadoAlta = execucao.FirstOrDefault(e => e.IdTipoProtecao == 11);
                    var reprogramadoBeneficiosEventuais = execucao.FirstOrDefault(e => e.IdTipoProtecao == 12);
                    var reprogramacaoProgramasProjetos = execucao.FirstOrDefault(e => e.IdTipoProtecao == 13);

                    var demandasBasica = execucao.FirstOrDefault(e => e.IdTipoProtecao == 14);
                    var demandasMedia = execucao.FirstOrDefault(e => e.IdTipoProtecao == 16);
                    var demandasAlta = execucao.FirstOrDefault(e => e.IdTipoProtecao == 18);

                    var reprogramacaoDemandasBasica = execucao.FirstOrDefault(e => e.IdTipoProtecao == 15);
                    var reprogramacaoDemandasMedia = execucao.FirstOrDefault(e => e.IdTipoProtecao == 17);
                    var reprogramacaoDemandasAlta = execucao.FirstOrDefault(e => e.IdTipoProtecao == 19);

                    var demandasBeneficiosEventuais = execucao.FirstOrDefault(e => e.IdTipoProtecao == 20);
                    var reprogramacaoDemandasBeneficiosEventuais = execucao.FirstOrDefault(e => e.IdTipoProtecao == 21);


                    if (basica != null)
                    {
                        carregarBasica(basica);
                    }
                    else
                    {
                        clearBasica();
                        basica = new ExecucaoFinanceiraInfo(); 
                    }

                    if (reprogramadoBasica != null)
                    {
                        carregarReprogramacaoBasica(reprogramadoBasica);
                    }
                    else
                    {
                        clearReprogramacaoBasica();
                        reprogramadoBasica = new ExecucaoFinanceiraInfo(); 
                    }

                    if (demandasBasica != null)
                    {
                        carregarDemandasParlamentaresBasica(demandasBasica);
                    }
                    else
                    {
                        clearDemandasParlamentaresBasica();
                        demandasBasica = new ExecucaoFinanceiraInfo(); 
                    }

                    if (reprogramacaoDemandasBasica != null)
                    {
                        carregarReprogramacaoDemandasParlamentaresBasica(reprogramacaoDemandasBasica);
                    }
                    else
                    {
                        clearReprogramacaoDemandasParlamentaresBasica();
                        reprogramacaoDemandasBasica = new ExecucaoFinanceiraInfo();
                    }


                    if (media != null)
                    {
                        carregarMedia(media);
                    }
                    else
                    {
                        clearMedia();
                        media = new ExecucaoFinanceiraInfo();
                    }

                    if (reprogramadoMedia != null)
                    {
                        carregarReprogramacaoMedia(reprogramadoMedia);
                    }
                    else
                    {
                        clearReprogramacaoMedia();
                        reprogramadoMedia = new ExecucaoFinanceiraInfo(); 
                    }

                    if (demandasMedia != null)
                    {
                        carregarDemandasParlamentaresMedia(demandasMedia);
                    }
                    else
                    {
                        clearDemandasParlamentaresMedia();
                        demandasMedia = new ExecucaoFinanceiraInfo();
                    }

                    if (reprogramacaoDemandasMedia != null)
                    {
                        carregarReprogramacaoDemandasParlamentaresMedia(reprogramacaoDemandasMedia);
                    }
                    else
                    {
                        clearReprogramacaoDemandasParlamentaresMedia();
                        reprogramacaoDemandasMedia = new ExecucaoFinanceiraInfo();
                    }

                    if (alta != null)
                    {
                        carregarAlta(alta);
                    }
                    else
                    {
                        clearAlta();
                        alta = new ExecucaoFinanceiraInfo();  
                    }

                    if (reprogramadoAlta != null)
                    {
                        carregarReprogramacaoAlta(reprogramadoAlta);
                    }
                    else
                    {
                        clearReprogramacaoAlta();
                        reprogramadoAlta = new ExecucaoFinanceiraInfo();
                    }

                    if (demandasAlta != null)
                    {
                        carregarDemandasParlamentaresAlta(demandasAlta);
                    }
                    else
                    {
                        clearDemandasParlamentaresAlta();
                        demandasAlta = new ExecucaoFinanceiraInfo();
                    }

                    if (reprogramacaoDemandasAlta != null)
                    {
                        carregarReprogramacaoDemandasParlamentaresAlta(reprogramacaoDemandasAlta);
                    }
                    else
                    {
                        clearReprogramacaoDemandasParlamentaresAlta();
                        reprogramacaoDemandasAlta = new ExecucaoFinanceiraInfo();
                    }

                    if (BeneficiosEventuais != null)
                    {
                        carregarBeneficiosEventuais(BeneficiosEventuais);
                    }
                    else
                    {
                        clearBeneficiosEventuais();
                        BeneficiosEventuais = new ExecucaoFinanceiraInfo();
                    }

                    if (reprogramadoBeneficiosEventuais != null)
                    {
                        carregarReprogramacaoBeneficiosEventuais(reprogramadoBeneficiosEventuais);
                    }
                    else
                    {
                        clearReprogramacaoBeneficiosEventuais();
                        reprogramadoBeneficiosEventuais = new ExecucaoFinanceiraInfo();
                    }

                    if (demandasBeneficiosEventuais != null)
                    {
                        carregarDemandasParlamentaresBeneficiosEventuais(demandasBeneficiosEventuais);
                    }
                    else
                    {
                        clearDemandasParlamentaresBeneficiosEventuais();
                        demandasBeneficiosEventuais = new ExecucaoFinanceiraInfo();
                    }

                    if (reprogramacaoDemandasBeneficiosEventuais != null)
                    {
                        carregarReprogramacaoDemandasParlamentaresBeneficiosEventuais(reprogramacaoDemandasBeneficiosEventuais);
                    }
                    else
                    {
                        clearReprogramacaoDemandasParlamentaresBeneficiosEventuais();
                        reprogramacaoDemandasBeneficiosEventuais = new ExecucaoFinanceiraInfo();
                    }

                    if (ProgramasProjetos != null)
                    {
                        carregarProgramasProjetos(ProgramasProjetos);
                    }
                    else
                    {
                        ClearProgramasProjetos();
                        ProgramasProjetos = new ExecucaoFinanceiraInfo();
                    }

                    if (reprogramacaoProgramasProjetos != null)
                    {
                        carregarProgramasProjetosReprogramacao(reprogramacaoProgramasProjetos);
                    }
                    else
                    {
                        clearProgramasProjetosReprogramacao();

                        reprogramacaoProgramasProjetos = new ExecucaoFinanceiraInfo();
                    }



                    if (ExercicioAnterior != null)
                    {
                        carregarReprogramado(ExercicioAnterior);
                    }
                    else
                    {
                        ExercicioAnterior = new ExecucaoFinanceiraInfo();
                    }


                    if (basica != null || demandasBasica != null || reprogramacaoDemandasBasica != null || media != null || demandasMedia != null || reprogramacaoDemandasMedia != null || alta != null || demandasAlta != null || reprogramacaoDemandasAlta != null || BeneficiosEventuais != null || demandasBeneficiosEventuais != null || reprogramacaoDemandasBeneficiosEventuais != null || ProgramasProjetos != null || ExercicioAnterior != null || reprogramadoBasica != null || reprogramadoMedia != null || reprogramadoAlta != null || reprogramadoBeneficiosEventuais != null || reprogramacaoProgramasProjetos != null)
                    {
                        carregaPrevisaoInicial(basica, reprogramadoBasica, demandasBasica,reprogramacaoDemandasBasica, media, reprogramadoMedia, demandasMedia, reprogramacaoDemandasMedia, alta, reprogramadoAlta, demandasAlta, reprogramacaoDemandasAlta, BeneficiosEventuais, reprogramadoBeneficiosEventuais, demandasBeneficiosEventuais, reprogramacaoDemandasBeneficiosEventuais, ProgramasProjetos, reprogramacaoProgramasProjetos);
                    }


                    if (basica != null || demandasBasica != null || reprogramacaoDemandasBasica != null || media != null || demandasMedia != null || reprogramacaoDemandasMedia != null || alta != null || demandasAlta != null || reprogramacaoDemandasAlta != null || BeneficiosEventuais != null || demandasBeneficiosEventuais != null || reprogramacaoDemandasBeneficiosEventuais != null || ProgramasProjetos != null || ExercicioAnterior != null || reprogramadoBasica != null || reprogramadoMedia != null || reprogramadoAlta != null || reprogramadoBeneficiosEventuais != null || reprogramacaoProgramasProjetos != null)
                    {
                        totalizar(basica, reprogramadoBasica, demandasBasica, reprogramacaoDemandasBasica, ExercicioAnterior, media, reprogramadoMedia, demandasMedia, reprogramacaoDemandasMedia, alta, reprogramadoAlta, demandasAlta, reprogramacaoDemandasAlta, BeneficiosEventuais, reprogramadoBeneficiosEventuais, demandasBeneficiosEventuais, reprogramacaoDemandasBeneficiosEventuais, ProgramasProjetos, reprogramacaoProgramasProjetos);
                    }
                    else
                    {
                        clearTotalizar();

                        if (b == true || m == true || a == true || be == true || pp == true)
                        {
                            carregaPrevisaoInicialDemonstrativo();
                        }

                    }

                    using (var proxy = new ProxyPrefeitura())
                    {
                        AplicarBloqueioControles(proxy);
                    }

                }
            }

            List<ComentarioPrestacaoDeContasInfo> comentario = prefeituras.GetComentarioPrestacaoDeContas(idPrefeitura, exercicio);
            var comentarioPrestacaoDeContas = comentario.FirstOrDefault();

            carregarComentarioPrestacaoDeContas(comentarioPrestacaoDeContas);

            List<ComentarioPrestacaoDeContasCMASInfo> comentarioCMAS = prefeituras.GetComentarioPrestacaoDeContasCMAS(idPrefeitura, exercicio);
            var comentarioPrestacaoDeContasCMAS = comentarioCMAS.FirstOrDefault();

            carregarComentarioCMAS(comentarioPrestacaoDeContasCMAS);

            List<ComentarioPrestacaoDeContasDRADSInfo> comentarioDRADS = prefeituras.GetComentarioPrestacaoDeContasDRADS(idPrefeitura, exercicio);
            var comentarioPrestacaoDeContasDRADS = comentarioDRADS.FirstOrDefault();

            carregarComentarioDrads(comentarioPrestacaoDeContasDRADS);

            List<DeliberacaoPrestacaoDeContasCMASInfo> deliberacaoCMAS = prefeituras.GetDeliberacaoPrestacaoDeContasCMAS(idPrefeitura, exercicio);
            var deliberacaoPrestacaoDeContasCMAS = deliberacaoCMAS.FirstOrDefault();

            if (deliberacaoPrestacaoDeContasCMAS != null)
            {
                carregarDeliberacaoCMAS(deliberacaoPrestacaoDeContasCMAS);
            }
            else
            {
                zerarDeliberacaoCMAS();
            }

            List<DeliberacaoPrestacaoDeContasDRADSInfo> deliberacaoDRADS = prefeituras.GetDeliberacaoPrestacaoDeContasDRADS(idPrefeitura, exercicio);
            var deliberacaoPrestacaoDeContasDRADS = deliberacaoDRADS.FirstOrDefault();
            if (deliberacaoPrestacaoDeContasDRADS != null)
            {
                carregarDeliberacaoDrads(deliberacaoPrestacaoDeContasDRADS);
            }

            List<QuestoesCMASinfo> questionarioCMAS = prefeituras.GetQuestionarioPrestacaoDeContasCMAS(idPrefeitura, exercicio);
            var questionarioPrestacaoDeContasCMAS = questionarioCMAS.FirstOrDefault();
            if (questionarioPrestacaoDeContasCMAS != null)
            {
                carregarQuestionarioCMAS(questionarioPrestacaoDeContasCMAS);
            }
            else
            {
                zerarQuestionarioCMAS();
                controleQuestaoSeisCMAS(false);
                controleQuestaoSeteCMAS(false);
            }
            List<QuestoesDRADSInfo> questionarioDRADS = prefeituras.GetQuestionarioPrestacaoDeContasDRADS(idPrefeitura, exercicio);
            var questionarioPrestacaoDeContasDrads = questionarioDRADS.FirstOrDefault();
            if (questionarioPrestacaoDeContasDrads != null)
            {
                carregarQuestionarioDrads(questionarioPrestacaoDeContasDrads);
            }
            else
            {
                zerarQuestionarioDRADS();
                controleQuestaoUmDRADS(false);
                controleQuestaoCincoDRADS(false);
            }

            List<HistoricoPrestacaoDeContasInfo> historico = prefeituras.GetHistoricoPrestacaoDeContasDetalhes(idPrefeitura, exercicio);

            var historicoGestor = historico.LastOrDefault(g => g.IdPerfil == 64);
            if (historicoGestor != null)
            {
                carregarDadosOrgaoGestor(historicoGestor);
            }
            else
            {
                zerarDadosOGestor();
            }

            var historicoPresidenteCmas = historico.LastOrDefault(g => g.IdPerfil == 71);
            if (historicoPresidenteCmas != null)
            {
                carregarDadosCmas(historicoPresidenteCmas);
            }
            else
            {
                zerarDadosCmas();
            }

            var historicoAdministradoDrads = historico.LastOrDefault(g => g.IdPerfil == 65);
            if (historicoAdministradoDrads != null)
            {
                carregarDadosDrads(historicoAdministradoDrads);
            }
            else
            {
                zerarDadosDrads();
            }

            carregarHistorico(idPrefeitura, exercicio);
        }


        void AlterarSituacaoQuadro(int idSituacaoQuadro)
        {
            String msg = String.Empty;
            var exercicio = Convert.ToInt32(hdfAno.Value);
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 168).Where(x => x.Exercicio == exercicio).FirstOrDefault();
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
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }

        private String validaCMAS()
        {
            string msg = "";

            if (String.IsNullOrEmpty(rblQuestaoUmCMAS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 1 do quadro questões auxiliares.";
            }

            if (String.IsNullOrEmpty(rblQuestaoDoisCMAS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 2 do quadro questões auxiliares.";
            }

            if (String.IsNullOrEmpty(rblQuestaoTresCMAS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 3 do quadro questões auxiliares.";
            }

            if (String.IsNullOrEmpty(rblQuestaoQuatroCMAS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 4 do quadro questões auxiliares.";
            }

            if (String.IsNullOrEmpty(rblQuestaoCincoCMAS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 5 do quadro questões auxiliares.";
            }

            if (String.IsNullOrEmpty(rblQuestaoSeisCMAS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 6 do quadro questões auxiliares.";
            }

            if (!String.IsNullOrEmpty(rblQuestaoSeisCMAS.SelectedValue))
            {
                if (rblQuestaoSeisCMAS.SelectedValue == "2")
                {
                    if (txtQuestaoSeisCMAS.Text.Count() == 0)
                    {
                        msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o texto da questão 6.A do quadro questões auxiliares.";
                    }
                }
            }

            if (String.IsNullOrEmpty(rblQuestaoSeteCMAS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 7 do quadro questões auxiliares.";
            }


            if (!String.IsNullOrEmpty(rblQuestaoSeteCMAS.SelectedValue))
            {
                if (rblQuestaoSeteCMAS.SelectedValue == "1")
                {
                    if (txtQuestaoSeteCMAS.Text.Count() == 0)
                    {
                        msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o texto da questão 7.A do quadro questões auxiliares.";
                    }
                }
            }

            if (String.IsNullOrEmpty(rblQuestaoOitoCMAS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 8 do quadro questões auxiliares.";
            }

            if (String.IsNullOrEmpty(rblQuestaoNoveCMAS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 9 do quadro questões auxiliares.";
            }

            if (String.IsNullOrEmpty(txtComentarioCMAS.Text))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o campo comentário CMAS.";
            }

            if (String.IsNullOrEmpty(txtDataReuniaoCMAS.Text) || txtDataReuniaoCMAS.Text == "01/01/0001")
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o Data Reunião CMAS.";
            }

            if (String.IsNullOrEmpty(txtNumeroConselheirosCMAS.Text))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o número conselheiros CMAS.";
            }

            if (String.IsNullOrEmpty(txtNumeroAtaCMAS.Text))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o número da ata CMAS.";
            }

            if (String.IsNullOrEmpty(txtNumeroResolucaoCMAS.Text))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o campo número da resolução CMAS.";
            }

            if (String.IsNullOrEmpty(txtDataPublicacaoCMAS.Text) || txtDataPublicacaoCMAS.Text == "01/01/0001")
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o campo data publicação CMAS.";
            }

            return msg;
        }

        private String validarDRADS()
        {
            string msg = "";

            if (String.IsNullOrEmpty(rblQuestaoUmDRADS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 1 do quadro questões auxiliares.";
            }


            if (String.IsNullOrEmpty(rblQuestaoUmDRADS.SelectedValue))
            {

                if (!String.IsNullOrEmpty(rblQuestaoUmDRADS.SelectedValue))
                {
                    if (rblQuestaoUmDRADS.SelectedValue == "1")
                    {
                        if (txtQuestaoUmDRADS.Text.Count() == 0)
                        {
                            msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o texto da questão 1.A do quadro questões auxiliares.";
                        }
                    }
                }
            }


            if (String.IsNullOrEmpty(rblQuestaoTresDRADS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 2 do quadro questões auxiliares.";
            }

            if (String.IsNullOrEmpty(rblQuestaoQuatroDRADS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 3 do quadro questões auxiliares.";
            }

            if (String.IsNullOrEmpty(rblQuestaoCincoDRADS.SelectedValue))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha a questão 4 do quadro questões auxiliares.";
            }

            if (String.IsNullOrEmpty(rblQuestaoCincoDRADS.SelectedValue))
            {

                if (!String.IsNullOrEmpty(rblQuestaoCincoDRADS.SelectedValue))
                {
                    if (rblQuestaoCincoDRADS.SelectedValue == "1")
                    {
                        if (txtQuestaoCincoDRADS.Text.Count() == 0)
                        {
                            msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o texto da questão 4.A do quadro questões auxiliares.";
                        }
                    }
                }
            }

            if (String.IsNullOrEmpty(txtComentarioDRADS.Text))
            {
                msg += (String.IsNullOrEmpty(msg) ? "" : "</br>") + "Por favor, preencha o campo comentário DRADS.";
            }

            return msg;
        }

        private void BloqueioPorQuadro(ProxyPrefeitura proxy)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);
            int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            int idUsuario = SessaoPmas.UsuarioLogado.IdUsuario;

            var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(idPrefeitura, 168).Where(x => x.Exercicio == exercicio).FirstOrDefault();

            switch (quadro.IdSituacaoQuadro)
            {
                case (int)ESituacaoQuadro.Pendente:
                case (int)ESituacaoQuadro.DevolvidoDRADS:
                case (int)ESituacaoQuadro.DevolvidoCMAS:
                default:

                    ObterControleBasica().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    ObterControleMedia().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    ObterControleAlta().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    ObterControleReprogramacao().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    ObterControleBeneficiosEventuais().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));
                    ObterControleProgramaProjeto().ToList().ForEach(x => x.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor));

                    txtComentario.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                    btnCalcular.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                    btnSalvar.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                    if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor)
                    {
                        preencherResponsávelGestor();
                    }

                    #region proibicoes
                    habilitaQuestionarioCMAS(false);
                    habilitaquestionarioDRADS(false);

                    btnSalvarCMAS.Enabled = false;
                    btnDevolverCMAS.Enabled = false;
                    btnFinalizarCMAS.Enabled = false;

                    btnDevolverDRADS.Enabled = false;
                    btnSalvarDRADS.Enabled = false;
                    btnFinalizarDrads.Enabled = false;
                    chkDeAcordo.Enabled = false;

                    btnDesbloqueio.Enabled = false;
                    divDesbloqueio.Visible = false;
                    #endregion

                    break;

                case (int)ESituacaoQuadro.EmAnaliseCMAS:

                    if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS)
                    {
                        habilitaQuestionarioCMAS(true);
                        preencherResponsavelCmas(idUsuario);
                    }
                    else
                    {
                        habilitaQuestionarioCMAS(false);
                    }


                    ObterControleBasica().ToList().ForEach(x => x.Enabled = false);
                    ObterControleMedia().ToList().ForEach(x => x.Enabled = false);
                    ObterControleAlta().ToList().ForEach(x => x.Enabled = false);
                    ObterControleReprogramacao().ToList().ForEach(x => x.Enabled = false);
                    ObterControleBeneficiosEventuais().ToList().ForEach(x => x.Enabled = false);
                    ObterControleProgramaProjeto().ToList().ForEach(x => x.Enabled = false);

                    habilitaquestionarioDRADS(false);

                    #region proibicoes
                    txtComentario.Enabled = false;
                    btnCalcular.Enabled = false;
                    btnSalvar.Enabled = false;
                    btnFinalizar.Enabled = false;

                    btnDevolverDRADS.Enabled = false;
                    btnSalvarDRADS.Enabled = false;
                    btnFinalizarDrads.Enabled = false;
                    chkDeAcordo.Enabled = false;

                    btnDesbloqueio.Enabled = false;
                    divDesbloqueio.Visible = false;
                    #endregion

                    break;
                case (int)ESituacaoQuadro.AprovadoCMAS:

                    if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador)
                    {
                        habilitaquestionarioDRADS(true);
                        preencherResponsavelDrads(idUsuario);
                    }
                    else
                    {
                        habilitaquestionarioDRADS(false);
                    }


                    ObterControleBasica().ToList().ForEach(x => x.Enabled = false);
                    ObterControleMedia().ToList().ForEach(x => x.Enabled = false);
                    ObterControleAlta().ToList().ForEach(x => x.Enabled = false);
                    ObterControleReprogramacao().ToList().ForEach(x => x.Enabled = false);
                    ObterControleBeneficiosEventuais().ToList().ForEach(x => x.Enabled = false);
                    ObterControleProgramaProjeto().ToList().ForEach(x => x.Enabled = false);

                    habilitaQuestionarioCMAS(false);

                    #region proibicoes
                    txtComentario.Enabled = false;
                    btnCalcular.Enabled = false;
                    btnSalvar.Enabled = false;
                    btnFinalizar.Enabled = false;

                    btnSalvarCMAS.Enabled = false;
                    btnDevolverCMAS.Enabled = false;
                    btnFinalizarCMAS.Enabled = false;

                    btnDesbloqueio.Enabled = false;
                    divDesbloqueio.Visible = false;
                    #endregion

                    break;
                case (int)ESituacaoQuadro.RejeitadoCMAS:


                    btnDesbloqueio.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);
                    divDesbloqueio.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);

                    ObterControleBasica().ToList().ForEach(x => x.Enabled = false);
                    ObterControleMedia().ToList().ForEach(x => x.Enabled = false);
                    ObterControleAlta().ToList().ForEach(x => x.Enabled = false);
                    ObterControleReprogramacao().ToList().ForEach(x => x.Enabled = false);
                    ObterControleBeneficiosEventuais().ToList().ForEach(x => x.Enabled = false);
                    ObterControleProgramaProjeto().ToList().ForEach(x => x.Enabled = false);
                    habilitaQuestionarioCMAS(false);
                    habilitaquestionarioDRADS(false);

                    #region proibicoes
                    txtComentario.Enabled = false;
                    btnCalcular.Enabled = false;
                    btnSalvar.Enabled = false;
                    btnFinalizar.Enabled = false;

                    btnSalvarCMAS.Enabled = false;
                    btnDevolverCMAS.Enabled = false;
                    btnFinalizarCMAS.Enabled = false;

                    btnDevolverDRADS.Enabled = false;
                    btnSalvarDRADS.Enabled = false;
                    btnFinalizarDrads.Enabled = false;
                    chkDeAcordo.Enabled = false;

                    #endregion

                    break;
                case (int)ESituacaoQuadro.AprovadoDRADS:

                    btnDesbloqueio.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);
                    divDesbloqueio.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);

                    ObterControleBasica().ToList().ForEach(x => x.Enabled = false);
                    ObterControleMedia().ToList().ForEach(x => x.Enabled = false);
                    ObterControleAlta().ToList().ForEach(x => x.Enabled = false);
                    ObterControleReprogramacao().ToList().ForEach(x => x.Enabled = false);
                    ObterControleBeneficiosEventuais().ToList().ForEach(x => x.Enabled = false);
                    ObterControleProgramaProjeto().ToList().ForEach(x => x.Enabled = false);

                    habilitaQuestionarioCMAS(false);
                    habilitaquestionarioDRADS(false);

                    #region proibicoes
                    txtComentario.Enabled = false;
                    btnCalcular.Enabled = false;
                    btnSalvar.Enabled = false;
                    btnFinalizar.Enabled = false;

                    btnSalvarCMAS.Enabled = false;
                    btnDevolverCMAS.Enabled = false;
                    btnFinalizarCMAS.Enabled = false;

                    btnDevolverDRADS.Enabled = false;
                    btnSalvarDRADS.Enabled = false;
                    btnFinalizarDrads.Enabled = false;
                    chkDeAcordo.Enabled = false;


                    #endregion

                    break;
                case (int)ESituacaoQuadro.RejeitadoDRADS:
                    btnDesbloqueio.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);
                    divDesbloqueio.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);

                    ObterControleBasica().ToList().ForEach(x => x.Enabled = false);
                    ObterControleMedia().ToList().ForEach(x => x.Enabled = false);
                    ObterControleAlta().ToList().ForEach(x => x.Enabled = false);
                    ObterControleReprogramacao().ToList().ForEach(x => x.Enabled = false);
                    ObterControleBeneficiosEventuais().ToList().ForEach(x => x.Enabled = false);
                    ObterControleProgramaProjeto().ToList().ForEach(x => x.Enabled = false);

                    habilitaQuestionarioCMAS(false);
                    habilitaquestionarioDRADS(false);

                    #region proibicoes
                    txtComentario.Enabled = false;
                    btnCalcular.Enabled = false;
                    btnSalvar.Enabled = false;
                    btnFinalizar.Enabled = false;

                    btnSalvarCMAS.Enabled = false;
                    btnDevolverCMAS.Enabled = false;
                    btnFinalizarCMAS.Enabled = false;

                    btnDevolverDRADS.Enabled = false;
                    btnSalvarDRADS.Enabled = false;
                    btnFinalizarDrads.Enabled = false;
                    chkDeAcordo.Enabled = false;
                    #endregion

                    break;
                case (int)ESituacaoQuadro.BloqueioInicialAdministrativo:

                    btnDesbloqueio.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);
                    divDesbloqueio.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);

                    ObterControleBasica().ToList().ForEach(x => x.Enabled = false);
                    ObterControleMedia().ToList().ForEach(x => x.Enabled = false);
                    ObterControleAlta().ToList().ForEach(x => x.Enabled = false);
                    ObterControleReprogramacao().ToList().ForEach(x => x.Enabled = false);
                    ObterControleBeneficiosEventuais().ToList().ForEach(x => x.Enabled = false);
                    ObterControleProgramaProjeto().ToList().ForEach(x => x.Enabled = false);

                    habilitaQuestionarioCMAS(false);
                    habilitaquestionarioDRADS(false);

                    #region proibicoes
                    txtComentario.Enabled = false;
                    btnCalcular.Enabled = false;
                    btnSalvar.Enabled = false;
                    btnFinalizar.Enabled = false;

                    btnSalvarCMAS.Enabled = false;
                    btnDevolverCMAS.Enabled = false;
                    btnFinalizarCMAS.Enabled = false;

                    btnDevolverDRADS.Enabled = false;
                    btnSalvarDRADS.Enabled = false;
                    btnFinalizarDrads.Enabled = false;
                    chkDeAcordo.Enabled = false;

                    #endregion

                    break;
            }
            ObterControleCalculaveis().ToList().ForEach(x => x.Enabled = false);

        }


        ExecucaoFinanceiraInfo PreencherProtecaoBasica(int exercicio)
        {
            var protecaoBasica = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 1 && e.Exercicio == exercicio);
            protecaoBasica.Exercicio = exercicio;
            protecaoBasica.IdSituacao = 1;
            protecaoBasica.Desbloqueado = true;
            protecaoBasica.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoBasica.IdTipoProtecao = 1;
            protecaoBasica.Atualizado = true;

            if (preencher != null)
            {

                #region FMAS
                protecaoBasica.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                protecaoBasica.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                protecaoBasica.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                protecaoBasica.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                protecaoBasica.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                protecaoBasica.ValoresDevolvidosFMAS = (protecaoBasica.RecursoDisponibilizadoFMAS + protecaoBasica.ResultadoAplicacaoFinanceiraFMAS) - (protecaoBasica.ValoresExecutadosFMAS + protecaoBasica.ValoresReprogramadosFMAS);

                #endregion

                #region FNAS

                protecaoBasica.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                protecaoBasica.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                protecaoBasica.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                protecaoBasica.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                protecaoBasica.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                protecaoBasica.ValoresDevolvidosFNAS = (protecaoBasica.RecursoDisponibilizadoFNAS + protecaoBasica.ResultadoAplicacaoFinanceiraFNAS) - (protecaoBasica.ValoresExecutadosFNAS + protecaoBasica.ValoresReprogramadosFNAS);
                #endregion

            }

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
            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosBasica.Text))
            {
                protecaoBasica.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosBasica.Text);
            }

            protecaoBasica.ValoresDevolvidosFEAS = (protecaoBasica.RecursoDisponibilizadoFEAS + protecaoBasica.ResultadoAplicacaoFinanceiraFEAS) - (protecaoBasica.ValoresExecutadosFEAS + protecaoBasica.ValoresReprogramadosFEAS);

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
            protecaoBasica.Atualizado = true;

            #region FEAS
            if (!String.IsNullOrEmpty(txtReprogramacaoPrevisaoInicialBasica.Text))
            {
                protecaoBasica.PrevisaoInicialFEAS = Convert.ToDecimal(txtReprogramacaoPrevisaoInicialBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoRecursosDisponibilizadosBasica.Text))
            {
                protecaoBasica.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtReprogramacaoRecursosDisponibilizadosBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoResultadoAppFinanceirasBasica.Text))
            {
                protecaoBasica.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtReprogramacaoResultadoAppFinanceirasBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoValoresExecutadosBasica.Text))
            {
                protecaoBasica.ValoresExecutadosFEAS = Convert.ToDecimal(txtReprogramacaoValoresExecutadosBasica.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoValoresReprogramadosBasica.Text))
            {
                protecaoBasica.ValoresReprogramadosFEAS = Convert.ToDecimal(txtReprogramacaoValoresReprogramadosBasica.Text);
            }

            protecaoBasica.ValoresDevolvidosFEAS = (protecaoBasica.RecursoDisponibilizadoFEAS + protecaoBasica.ResultadoAplicacaoFinanceiraFEAS) - (protecaoBasica.ValoresExecutadosFEAS + protecaoBasica.ValoresReprogramadosFEAS);

            #endregion

            return protecaoBasica;
        }


        ExecucaoFinanceiraInfo PreencherDemandasProtecaoBasica(int exercicio)
        {
            var demandasProtecaoBasica = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 14 && e.Exercicio == exercicio);
            demandasProtecaoBasica.Exercicio = exercicio;
            demandasProtecaoBasica.IdSituacao = 1;
            demandasProtecaoBasica.Desbloqueado = true;
            demandasProtecaoBasica.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            demandasProtecaoBasica.IdTipoProtecao = 14;
            demandasProtecaoBasica.Atualizado = true;

            /*if (preencher != null)
            {

                #region FMAS
                demandasdemandasProtecaoBasica.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                demandasdemandasProtecaoBasica.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                demandasdemandasProtecaoBasica.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                demandasdemandasProtecaoBasica.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                demandasdemandasProtecaoBasica.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                demandasdemandasProtecaoBasica.ValoresDevolvidosFMAS = (demandasdemandasProtecaoBasica.RecursoDisponibilizadoFMAS + demandasdemandasProtecaoBasica.ResultadoAplicacaoFinanceiraFMAS) - (demandasdemandasProtecaoBasica.ValoresExecutadosFMAS + demandasdemandasProtecaoBasica.ValoresReprogramadosFMAS);

                #endregion

                #region FNAS

                demandasProtecaoBasica.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                demandasProtecaoBasica.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                demandasProtecaoBasica.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                demandasProtecaoBasica.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                demandasProtecaoBasica.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                demandasProtecaoBasica.ValoresDevolvidosFNAS = (demandasProtecaoBasica.RecursoDisponibilizadoFNAS + demandasProtecaoBasica.ResultadoAplicacaoFinanceiraFNAS) - (demandasProtecaoBasica.ValoresExecutadosFNAS + demandasProtecaoBasica.ValoresReprogramadosFNAS);
                #endregion

            }
            */
            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialBasicaDemandas.Text))
            {
                demandasProtecaoBasica.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialBasicaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosBasicaDemandas.Text))
            {
                demandasProtecaoBasica.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosBasicaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasBasicaDemandas.Text))
            {
                demandasProtecaoBasica.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasBasicaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosBasicaDemandas.Text))
            {
                demandasProtecaoBasica.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosBasicaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosBasicaDemandas.Text))
            {
                demandasProtecaoBasica.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosBasicaDemandas.Text);
            }

            demandasProtecaoBasica.ValoresDevolvidosFEAS = (demandasProtecaoBasica.RecursoDisponibilizadoFEAS + demandasProtecaoBasica.ResultadoAplicacaoFinanceiraFEAS) - (demandasProtecaoBasica.ValoresExecutadosFEAS + demandasProtecaoBasica.ValoresReprogramadosFEAS);

            #endregion

            return demandasProtecaoBasica;
        }


        ExecucaoFinanceiraInfo PreencherReprogramacaoDemandasProtecaoBasica(int exercicio)
        {
            var reprogramacaoDemandasProtecaoBasica = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 15 && e.Exercicio == exercicio);
            reprogramacaoDemandasProtecaoBasica.Exercicio = exercicio;
            reprogramacaoDemandasProtecaoBasica.IdSituacao = 1;
            reprogramacaoDemandasProtecaoBasica.Desbloqueado = true;
            reprogramacaoDemandasProtecaoBasica.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            reprogramacaoDemandasProtecaoBasica.IdTipoProtecao = 15;
            reprogramacaoDemandasProtecaoBasica.Atualizado = true;

            /*if (preencher != null)
            {

                #region FMAS
                demandasdemandasProtecaoBasica.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                demandasdemandasProtecaoBasica.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                demandasdemandasProtecaoBasica.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                demandasdemandasProtecaoBasica.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                demandasdemandasProtecaoBasica.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                demandasdemandasProtecaoBasica.ValoresDevolvidosFMAS = (demandasdemandasProtecaoBasica.RecursoDisponibilizadoFMAS + demandasdemandasProtecaoBasica.ResultadoAplicacaoFinanceiraFMAS) - (demandasdemandasProtecaoBasica.ValoresExecutadosFMAS + demandasdemandasProtecaoBasica.ValoresReprogramadosFMAS);

                #endregion

                #region FNAS

                demandasProtecaoBasica.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                demandasProtecaoBasica.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                demandasProtecaoBasica.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                demandasProtecaoBasica.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                demandasProtecaoBasica.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                demandasProtecaoBasica.ValoresDevolvidosFNAS = (demandasProtecaoBasica.RecursoDisponibilizadoFNAS + demandasProtecaoBasica.ResultadoAplicacaoFinanceiraFNAS) - (demandasProtecaoBasica.ValoresExecutadosFNAS + demandasProtecaoBasica.ValoresReprogramadosFNAS);
                #endregion

            }
            */
            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialBasicaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoBasica.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialBasicaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosBasicaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoBasica.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosBasicaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasBasicaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoBasica.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasBasicaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosBasicaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoBasica.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosBasicaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosBasicaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoBasica.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosBasicaReprogramacaoDemandas.Text);
            }

            reprogramacaoDemandasProtecaoBasica.ValoresDevolvidosFEAS = (reprogramacaoDemandasProtecaoBasica.RecursoDisponibilizadoFEAS + reprogramacaoDemandasProtecaoBasica.ResultadoAplicacaoFinanceiraFEAS) - (reprogramacaoDemandasProtecaoBasica.ValoresExecutadosFEAS + reprogramacaoDemandasProtecaoBasica.ValoresReprogramadosFEAS);

            #endregion

            return reprogramacaoDemandasProtecaoBasica;
        }


        ExecucaoFinanceiraInfo PreencherProtecaoEspecialReprogramado(int exercicio)
        {
            var reprogramacao = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 8 && e.Exercicio == exercicio);
            reprogramacao.Exercicio = exercicio;
            reprogramacao.IdSituacao = 1;
            reprogramacao.Desbloqueado = true;
            reprogramacao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            reprogramacao.IdTipoProtecao = 8;

            if (preencher != null)
            {
                #region FNAS

                reprogramacao.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                reprogramacao.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                reprogramacao.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                reprogramacao.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                reprogramacao.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                reprogramacao.ValoresDevolvidosFNAS = (reprogramacao.RecursoDisponibilizadoFNAS + reprogramacao.ResultadoAplicacaoFinanceiraFNAS) - (reprogramacao.ValoresExecutadosFNAS + reprogramacao.ValoresReprogramadosFNAS);

                #endregion
            }

            #region FEAS
            if (!String.IsNullOrEmpty(txtPrevisaoInicialReprogramacao.Text))
            {
                reprogramacao.PrevisaoInicialFEAS = Convert.ToDecimal(txtPrevisaoInicialReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtRecursosDisponibilizadosReprogramacao.Text))
            {
                reprogramacao.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtRecursosDisponibilizadosReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtResultadosAplicacaoReprogramacao.Text))
            {
                reprogramacao.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtResultadosAplicacaoReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtValoresExecutadosReprogramacao.Text))
            {
                reprogramacao.ValoresExecutadosFEAS = Convert.ToDecimal(txtValoresExecutadosReprogramacao.Text);
            }

            if (!String.IsNullOrEmpty(txtValoresReprogramacao.Text))
            {
                reprogramacao.ValoresReprogramadosFEAS = Convert.ToDecimal(txtValoresReprogramacao.Text);
            }
            reprogramacao.ValoresDevolvidosFEAS = (reprogramacao.RecursoDisponibilizadoFEAS + reprogramacao.ResultadoAplicacaoFinanceiraFEAS) - (reprogramacao.ValoresExecutadosFEAS + reprogramacao.ValoresReprogramadosFEAS);
            #endregion


            return reprogramacao;
        }

        ExecucaoFinanceiraInfo PreencherProtecaoEspecialMedia(int exercicio)
        {
            var protecaoMedia = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 2 && e.Exercicio == exercicio);
            protecaoMedia.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoMedia.Exercicio = exercicio;
            protecaoMedia.IdSituacao = 1;
            protecaoMedia.Desbloqueado = true;
            protecaoMedia.IdTipoProtecao = 2;
            protecaoMedia.Atualizado = true;

            if (preencher != null)
            {
                #region FMAS
                protecaoMedia.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                protecaoMedia.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                protecaoMedia.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                protecaoMedia.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                protecaoMedia.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                protecaoMedia.ValoresDevolvidosFMAS = (protecaoMedia.RecursoDisponibilizadoFMAS + protecaoMedia.ResultadoAplicacaoFinanceiraFMAS) - (protecaoMedia.ValoresExecutadosFMAS + protecaoMedia.ValoresReprogramadosFMAS);
                #endregion
            }

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

            return protecaoMedia;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoProtecaoMedia(int exercicio)
        {
            var protecaoMedia = new ExecucaoFinanceiraInfo();
            protecaoMedia.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoMedia.Exercicio = exercicio;
            protecaoMedia.IdSituacao = 1;
            protecaoMedia.Desbloqueado = true;
            protecaoMedia.IdTipoProtecao = 10;
            protecaoMedia.Atualizado = true;

            #region FEAS
            if (!String.IsNullOrEmpty(txtReprogramacaoPrevisaoInicialMedia.Text))
            {
                protecaoMedia.PrevisaoInicialFEAS = Convert.ToDecimal(txtReprogramacaoPrevisaoInicialMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoRecursosDisponibilizadosMedia.Text))
            {
                protecaoMedia.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtReprogramacaoRecursosDisponibilizadosMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoResultadosAppFinanceirasMedia.Text))
            {
                protecaoMedia.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtReprogramacaoResultadosAppFinanceirasMedia.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoValoresExecutadosMedia.Text))
            {
                protecaoMedia.ValoresExecutadosFEAS = Convert.ToDecimal(txtReprogramacaoValoresExecutadosMedia.Text);
            }

            #region reprogramado e devolvido
            if (!String.IsNullOrEmpty(txtReprogramacaoValoresReprogramadosMedia.Text))
            {
                protecaoMedia.ValoresReprogramadosFEAS = Convert.ToDecimal(txtReprogramacaoValoresReprogramadosMedia.Text);
            }
            protecaoMedia.ValoresDevolvidosFEAS = (protecaoMedia.RecursoDisponibilizadoFEAS + protecaoMedia.ResultadoAplicacaoFinanceiraFEAS) - (protecaoMedia.ValoresExecutadosFEAS + protecaoMedia.ValoresReprogramadosFEAS);
            #endregion
            #endregion

            return protecaoMedia;
        }

        ExecucaoFinanceiraInfo PreencherDemandasProtecaoMedia(int exercicio)
        {
            var demandasProtecaoMedia = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 16 && e.Exercicio == exercicio);
            demandasProtecaoMedia.Exercicio = exercicio;
            demandasProtecaoMedia.IdSituacao = 1;
            demandasProtecaoMedia.Desbloqueado = true;
            demandasProtecaoMedia.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            demandasProtecaoMedia.IdTipoProtecao = 16;
            demandasProtecaoMedia.Atualizado = true;

            /*if (preencher != null)
            {

                #region FMAS
                demandasdemandasProtecaoMedia.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                demandasdemandasProtecaoMedia.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                demandasdemandasProtecaoMedia.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                demandasdemandasProtecaoMedia.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                demandasdemandasProtecaoMedia.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                demandasdemandasProtecaoMedia.ValoresDevolvidosFMAS = (demandasdemandasProtecaoMedia.RecursoDisponibilizadoFMAS + demandasdemandasProtecaoMedia.ResultadoAplicacaoFinanceiraFMAS) - (demandasdemandasProtecaoMedia.ValoresExecutadosFMAS + demandasdemandasProtecaoMedia.ValoresReprogramadosFMAS);

                #endregion

                #region FNAS

                demandasProtecaoMedia.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                demandasProtecaoMedia.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                demandasProtecaoMedia.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                demandasProtecaoMedia.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                demandasProtecaoMedia.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                demandasProtecaoMedia.ValoresDevolvidosFNAS = (demandasProtecaoMedia.RecursoDisponibilizadoFNAS + demandasProtecaoMedia.ResultadoAplicacaoFinanceiraFNAS) - (demandasProtecaoMedia.ValoresExecutadosFNAS + demandasProtecaoMedia.ValoresReprogramadosFNAS);
                #endregion

            }
            */
            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialMediaDemandas.Text))
            {
                demandasProtecaoMedia.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialMediaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosMediaDemandas.Text))
            {
                demandasProtecaoMedia.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosMediaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasMediaDemandas.Text))
            {
                demandasProtecaoMedia.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasMediaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosMediaDemandas.Text))
            {
                demandasProtecaoMedia.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosMediaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosMediaDemandas.Text))
            {
                demandasProtecaoMedia.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosMediaDemandas.Text);
            }

            demandasProtecaoMedia.ValoresDevolvidosFEAS = (demandasProtecaoMedia.RecursoDisponibilizadoFEAS + demandasProtecaoMedia.ResultadoAplicacaoFinanceiraFEAS) - (demandasProtecaoMedia.ValoresExecutadosFEAS + demandasProtecaoMedia.ValoresReprogramadosFEAS);

            #endregion

            return demandasProtecaoMedia;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoDemandasProtecaoMedia(int exercicio)
        {
            var reprogramacaoDemandasProtecaoMedia = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 17 && e.Exercicio == exercicio);
            reprogramacaoDemandasProtecaoMedia.Exercicio = exercicio;
            reprogramacaoDemandasProtecaoMedia.IdSituacao = 1;
            reprogramacaoDemandasProtecaoMedia.Desbloqueado = true;
            reprogramacaoDemandasProtecaoMedia.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            reprogramacaoDemandasProtecaoMedia.IdTipoProtecao = 17;
            reprogramacaoDemandasProtecaoMedia.Atualizado = true;

            /*if (preencher != null)
            {

                #region FMAS
                demandasdemandasProtecaoMedia.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                demandasdemandasProtecaoMedia.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                demandasdemandasProtecaoMedia.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                demandasdemandasProtecaoMedia.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                demandasdemandasProtecaoMedia.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                demandasdemandasProtecaoMedia.ValoresDevolvidosFMAS = (demandasdemandasProtecaoMedia.RecursoDisponibilizadoFMAS + demandasdemandasProtecaoMedia.ResultadoAplicacaoFinanceiraFMAS) - (demandasdemandasProtecaoMedia.ValoresExecutadosFMAS + demandasdemandasProtecaoMedia.ValoresReprogramadosFMAS);

                #endregion

                #region FNAS

                demandasProtecaoMedia.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                demandasProtecaoMedia.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                demandasProtecaoMedia.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                demandasProtecaoMedia.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                demandasProtecaoMedia.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                demandasProtecaoMedia.ValoresDevolvidosFNAS = (demandasProtecaoMedia.RecursoDisponibilizadoFNAS + demandasProtecaoMedia.ResultadoAplicacaoFinanceiraFNAS) - (demandasProtecaoMedia.ValoresExecutadosFNAS + demandasProtecaoMedia.ValoresReprogramadosFNAS);
                #endregion

            }
            */
            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialMediaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoMedia.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialMediaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosMediaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoMedia.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosMediaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasMediaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoMedia.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasMediaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosMediaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoMedia.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosMediaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosMediaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoMedia.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosMediaReprogramacaoDemandas.Text);
            }

            reprogramacaoDemandasProtecaoMedia.ValoresDevolvidosFEAS = (reprogramacaoDemandasProtecaoMedia.RecursoDisponibilizadoFEAS + reprogramacaoDemandasProtecaoMedia.ResultadoAplicacaoFinanceiraFEAS) - (reprogramacaoDemandasProtecaoMedia.ValoresExecutadosFEAS + reprogramacaoDemandasProtecaoMedia.ValoresReprogramadosFEAS);

            #endregion

            return reprogramacaoDemandasProtecaoMedia;
        }

        ExecucaoFinanceiraInfo PreencherProtecaoEspecialAlta(int exercicio)
        {
            var protecaoEspecial = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 3 && e.Exercicio == exercicio);
            protecaoEspecial.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoEspecial.Exercicio = exercicio;
            protecaoEspecial.IdSituacao = 1;
            protecaoEspecial.Desbloqueado = true;
            protecaoEspecial.IdTipoProtecao = 3;
            protecaoEspecial.Atualizado = true;

            if (preencher != null)
            {

                #region FMAS
                protecaoEspecial.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                protecaoEspecial.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                protecaoEspecial.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                protecaoEspecial.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                protecaoEspecial.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                protecaoEspecial.ValoresDevolvidosFMAS = (protecaoEspecial.RecursoDisponibilizadoFMAS + protecaoEspecial.ResultadoAplicacaoFinanceiraFMAS) - (protecaoEspecial.ValoresExecutadosFMAS + protecaoEspecial.ValoresReprogramadosFMAS);

                #endregion
            }

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

            return protecaoEspecial;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoProtecaoAlta(int exercicio)
        {
            var protecaoEspecial = new ExecucaoFinanceiraInfo();
            protecaoEspecial.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoEspecial.Exercicio = exercicio;
            protecaoEspecial.IdSituacao = 1;
            protecaoEspecial.Desbloqueado = true;
            protecaoEspecial.IdTipoProtecao = 11;
            protecaoEspecial.Atualizado = true;

            #region FEAS
            if (!String.IsNullOrEmpty(txtReprogramacaoPrevisaoInicialAlta.Text))
            {
                protecaoEspecial.PrevisaoInicialFEAS = Convert.ToDecimal(txtReprogramacaoPrevisaoInicialAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoRecursosDisponibilizadosAlta.Text))
            {
                protecaoEspecial.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtReprogramacaoRecursosDisponibilizadosAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoResultadoAppFinanceirasAlta.Text))
            {
                protecaoEspecial.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtReprogramacaoResultadoAppFinanceirasAlta.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoValoresExecutadosAlta.Text))
            {
                protecaoEspecial.ValoresExecutadosFEAS = Convert.ToDecimal(txtReprogramacaoValoresExecutadosAlta.Text);
            }
            #region reprogramado e devolvidos
            if (!String.IsNullOrEmpty(txtReprogramacaoValoresReprogramadosAlta.Text))
            {
                protecaoEspecial.ValoresReprogramadosFEAS = Convert.ToDecimal(txtReprogramacaoValoresReprogramadosAlta.Text);
            }
            protecaoEspecial.ValoresDevolvidosFEAS = (protecaoEspecial.RecursoDisponibilizadoFEAS + protecaoEspecial.ResultadoAplicacaoFinanceiraFEAS) - (protecaoEspecial.ValoresExecutadosFEAS + protecaoEspecial.ValoresReprogramadosFEAS);
            #endregion
            #endregion

            return protecaoEspecial;
        }

        ExecucaoFinanceiraInfo PreencherDemandasProtecaoAlta(int exercicio)
        {
            var demandasProtecaoAlta = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 18 && e.Exercicio == exercicio);
            demandasProtecaoAlta.Exercicio = exercicio;
            demandasProtecaoAlta.IdSituacao = 1;
            demandasProtecaoAlta.Desbloqueado = true;
            demandasProtecaoAlta.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            demandasProtecaoAlta.IdTipoProtecao = 18;
            demandasProtecaoAlta.Atualizado = true;

            /*if (preencher != null)
            {

                #region FMAS
                demandasdemandasProtecaoAlta.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                demandasdemandasProtecaoAlta.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                demandasdemandasProtecaoAlta.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                demandasdemandasProtecaoAlta.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                demandasdemandasProtecaoAlta.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                demandasdemandasProtecaoAlta.ValoresDevolvidosFMAS = (demandasdemandasProtecaoAlta.RecursoDisponibilizadoFMAS + demandasdemandasProtecaoAlta.ResultadoAplicacaoFinanceiraFMAS) - (demandasdemandasProtecaoAlta.ValoresExecutadosFMAS + demandasdemandasProtecaoAlta.ValoresReprogramadosFMAS);

                #endregion

                #region FNAS

                demandasProtecaoAlta.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                demandasProtecaoAlta.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                demandasProtecaoAlta.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                demandasProtecaoAlta.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                demandasProtecaoAlta.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                demandasProtecaoAlta.ValoresDevolvidosFNAS = (demandasProtecaoAlta.RecursoDisponibilizadoFNAS + demandasProtecaoAlta.ResultadoAplicacaoFinanceiraFNAS) - (demandasProtecaoAlta.ValoresExecutadosFNAS + demandasProtecaoAlta.ValoresReprogramadosFNAS);
                #endregion

            }
            */
            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialAltaDemandas.Text))
            {
                demandasProtecaoAlta.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialAltaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosAltaDemandas.Text))
            {
                demandasProtecaoAlta.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosAltaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasAltaDemandas.Text))
            {
                demandasProtecaoAlta.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasAltaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosAltaDemandas.Text))
            {
                demandasProtecaoAlta.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosAltaDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosAltaDemandas.Text))
            {
                demandasProtecaoAlta.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosAltaDemandas.Text);
            }

            demandasProtecaoAlta.ValoresDevolvidosFEAS = (demandasProtecaoAlta.RecursoDisponibilizadoFEAS + demandasProtecaoAlta.ResultadoAplicacaoFinanceiraFEAS) - (demandasProtecaoAlta.ValoresExecutadosFEAS + demandasProtecaoAlta.ValoresReprogramadosFEAS);

            #endregion

            return demandasProtecaoAlta;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoDemandasProtecaoAlta(int exercicio)
        {
            var reprogramacaoDemandasProtecaoAlta = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 19 && e.Exercicio == exercicio);
            reprogramacaoDemandasProtecaoAlta.Exercicio = exercicio;
            reprogramacaoDemandasProtecaoAlta.IdSituacao = 1;
            reprogramacaoDemandasProtecaoAlta.Desbloqueado = true;
            reprogramacaoDemandasProtecaoAlta.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            reprogramacaoDemandasProtecaoAlta.IdTipoProtecao = 19;
            reprogramacaoDemandasProtecaoAlta.Atualizado = true;

            /*if (preencher != null)
            {

                #region FMAS
                demandasdemandasProtecaoAlta.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                demandasdemandasProtecaoAlta.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                demandasdemandasProtecaoAlta.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                demandasdemandasProtecaoAlta.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                demandasdemandasProtecaoAlta.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                demandasdemandasProtecaoAlta.ValoresDevolvidosFMAS = (demandasdemandasProtecaoAlta.RecursoDisponibilizadoFMAS + demandasdemandasProtecaoAlta.ResultadoAplicacaoFinanceiraFMAS) - (demandasdemandasProtecaoAlta.ValoresExecutadosFMAS + demandasdemandasProtecaoAlta.ValoresReprogramadosFMAS);

                #endregion

                #region FNAS

                demandasProtecaoAlta.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                demandasProtecaoAlta.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                demandasProtecaoAlta.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                demandasProtecaoAlta.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                demandasProtecaoAlta.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                demandasProtecaoAlta.ValoresDevolvidosFNAS = (demandasProtecaoAlta.RecursoDisponibilizadoFNAS + demandasProtecaoAlta.ResultadoAplicacaoFinanceiraFNAS) - (demandasProtecaoAlta.ValoresExecutadosFNAS + demandasProtecaoAlta.ValoresReprogramadosFNAS);
                #endregion

            }
            */
            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialAltaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoAlta.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialAltaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosAltaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoAlta.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosAltaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasAltaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoAlta.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasAltaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosAltaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoAlta.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosAltaReprogramacaoDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosAltaReprogramacaoDemandas.Text))
            {
                reprogramacaoDemandasProtecaoAlta.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosAltaReprogramacaoDemandas.Text);
            }

            reprogramacaoDemandasProtecaoAlta.ValoresDevolvidosFEAS = (reprogramacaoDemandasProtecaoAlta.RecursoDisponibilizadoFEAS + reprogramacaoDemandasProtecaoAlta.ResultadoAplicacaoFinanceiraFEAS) - (reprogramacaoDemandasProtecaoAlta.ValoresExecutadosFEAS + reprogramacaoDemandasProtecaoAlta.ValoresReprogramadosFEAS);

            #endregion

            return reprogramacaoDemandasProtecaoAlta;
        }


        ExecucaoFinanceiraInfo PreencherBeneficiosEventuais(int exercicio)
        {
            var beneficiosEventuais = new ExecucaoFinanceiraInfo();

            beneficiosEventuais.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 5 && e.Exercicio == exercicio);
            beneficiosEventuais.Exercicio = exercicio;
            beneficiosEventuais.IdSituacao = 1;
            beneficiosEventuais.Desbloqueado = true;
            beneficiosEventuais.IdTipoProtecao = 5;
            beneficiosEventuais.Atualizado = true;

            if (preencher != null)
            {
                #region FMAS
                beneficiosEventuais.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                beneficiosEventuais.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                beneficiosEventuais.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                beneficiosEventuais.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                beneficiosEventuais.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                beneficiosEventuais.ValoresDevolvidosFMAS = (beneficiosEventuais.RecursoDisponibilizadoFMAS + beneficiosEventuais.ResultadoAplicacaoFinanceiraFMAS) - (beneficiosEventuais.ValoresExecutadosFMAS + beneficiosEventuais.ValoresReprogramadosFMAS);
                #endregion
            }

            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialBeneficiosEventuais.Text))
            {
                beneficiosEventuais.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialBeneficiosEventuais.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosBeneficiosEventuais.Text))
            {
                beneficiosEventuais.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosBeneficiosEventuais.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasBeneficiosEventuais.Text))
            {
                beneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasBeneficiosEventuais.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosBeneficiosEventuais.Text))
            {
                beneficiosEventuais.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosBeneficiosEventuais.Text);
            }

            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosBeneficiosEventuais.Text))
            {
                beneficiosEventuais.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosBeneficiosEventuais.Text);
            }
            beneficiosEventuais.ValoresDevolvidosFEAS = (beneficiosEventuais.RecursoDisponibilizadoFEAS + beneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS) - (beneficiosEventuais.ValoresExecutadosFEAS + beneficiosEventuais.ValoresReprogramadosFEAS);

            #endregion

            return beneficiosEventuais;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoProtecaoBeneficiosEventuais(int exercicio)
        {
            var beneficiosEventuais = new ExecucaoFinanceiraInfo();

            beneficiosEventuais.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            beneficiosEventuais.Exercicio = exercicio;
            beneficiosEventuais.IdSituacao = 1;
            beneficiosEventuais.Desbloqueado = true;
            beneficiosEventuais.IdTipoProtecao = 12;
            beneficiosEventuais.Atualizado = true;

            #region FEAS
            if (!String.IsNullOrEmpty(txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text))
            {
                beneficiosEventuais.PrevisaoInicialFEAS = Convert.ToDecimal(txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoRecursosDisponiveisBeneficiosEventuais.Text))
            {
                beneficiosEventuais.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtReprogramacaoRecursosDisponiveisBeneficiosEventuais.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoResultadosAppFinanceirasBeneficiosEventuais.Text))
            {
                beneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtReprogramacaoResultadosAppFinanceirasBeneficiosEventuais.Text);
            }
            if (!String.IsNullOrEmpty(txtReprogramacaoValoresExecutadosBeneficiosEventuais.Text))
            {
                beneficiosEventuais.ValoresExecutadosFEAS = Convert.ToDecimal(txtReprogramacaoValoresExecutadosBeneficiosEventuais.Text);
            }

            if (!String.IsNullOrEmpty(txtReprogramacaoValoresReprogramadosBeneficiosEventuais.Text))
            {
                beneficiosEventuais.ValoresReprogramadosFEAS = Convert.ToDecimal(txtReprogramacaoValoresReprogramadosBeneficiosEventuais.Text);
            }
            beneficiosEventuais.ValoresDevolvidosFEAS = (beneficiosEventuais.RecursoDisponibilizadoFEAS + beneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS) - (beneficiosEventuais.ValoresExecutadosFEAS + beneficiosEventuais.ValoresReprogramadosFEAS);

            #endregion

            return beneficiosEventuais;
        }

        ExecucaoFinanceiraInfo PreencherDemandasBeneficiosEventuais(int exercicio)
        {
            var demandasBeneficiosEventuais = new ExecucaoFinanceiraInfo();

            demandasBeneficiosEventuais.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 20 && e.Exercicio == exercicio);
            demandasBeneficiosEventuais.Exercicio = exercicio;
            demandasBeneficiosEventuais.IdSituacao = 1;
            demandasBeneficiosEventuais.Desbloqueado = true;
            demandasBeneficiosEventuais.IdTipoProtecao = 20;
            demandasBeneficiosEventuais.Atualizado = true;

            /*if (preencher != null)
            {
                #region FMAS
                demandasBeneficiosEventuais.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                demandasBeneficiosEventuais.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                demandasBeneficiosEventuais.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                demandasBeneficiosEventuais.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                demandasBeneficiosEventuais.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                demandasBeneficiosEventuais.ValoresDevolvidosFMAS = (demandasBeneficiosEventuais.RecursoDisponibilizadoFMAS + demandasBeneficiosEventuais.ResultadoAplicacaoFinanceiraFMAS) - (demandasBeneficiosEventuais.ValoresExecutadosFMAS + demandasBeneficiosEventuais.ValoresReprogramadosFMAS);
                #endregion
            }*/

            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialBeneficiosEventuaisDemandas.Text))
            {
                demandasBeneficiosEventuais.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialBeneficiosEventuaisDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandas.Text))
            {
                demandasBeneficiosEventuais.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandas.Text))
            {
                demandasBeneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandas.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosBeneficiosEventuaisDemandas.Text))
            {
                demandasBeneficiosEventuais.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosBeneficiosEventuaisDemandas.Text);
            }

            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosBeneficiosEventuaisDemandas.Text))
            {
                demandasBeneficiosEventuais.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosBeneficiosEventuaisDemandas.Text);
            }
            demandasBeneficiosEventuais.ValoresDevolvidosFEAS = (demandasBeneficiosEventuais.RecursoDisponibilizadoFEAS + demandasBeneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS) - (demandasBeneficiosEventuais.ValoresExecutadosFEAS + demandasBeneficiosEventuais.ValoresReprogramadosFEAS);

            #endregion

            return demandasBeneficiosEventuais;
        }

        ExecucaoFinanceiraInfo PreencherReprogramacaoDemandasBeneficiosEventuais(int exercicio)
        {
            var reprogramacaoDemandasBeneficiosEventuais = new ExecucaoFinanceiraInfo();

            reprogramacaoDemandasBeneficiosEventuais.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 21 && e.Exercicio == exercicio);
            reprogramacaoDemandasBeneficiosEventuais.Exercicio = exercicio;
            reprogramacaoDemandasBeneficiosEventuais.IdSituacao = 1;
            reprogramacaoDemandasBeneficiosEventuais.Desbloqueado = true;
            reprogramacaoDemandasBeneficiosEventuais.IdTipoProtecao = 21;
            reprogramacaoDemandasBeneficiosEventuais.Atualizado = true;

            /*if (preencher != null)
            {
                #region FMAS
                demandasBeneficiosEventuais.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                demandasBeneficiosEventuais.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                demandasBeneficiosEventuais.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                demandasBeneficiosEventuais.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                demandasBeneficiosEventuais.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                demandasBeneficiosEventuais.ValoresDevolvidosFMAS = (demandasBeneficiosEventuais.RecursoDisponibilizadoFMAS + demandasBeneficiosEventuais.ResultadoAplicacaoFinanceiraFMAS) - (demandasBeneficiosEventuais.ValoresExecutadosFMAS + demandasBeneficiosEventuais.ValoresReprogramadosFMAS);
                #endregion
            }*/

            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialBeneficiosEventuaisDemandasReprogramacao.Text))
            {
                reprogramacaoDemandasBeneficiosEventuais.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialBeneficiosEventuaisDemandasReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandasReprogramacao.Text))
            {
                reprogramacaoDemandasBeneficiosEventuais.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandasReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandasReprogramacao.Text))
            {
                reprogramacaoDemandasBeneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandasReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosBeneficiosEventuaisDemandasReprogramacao.Text))
            {
                reprogramacaoDemandasBeneficiosEventuais.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosBeneficiosEventuaisDemandasReprogramacao.Text);
            }

            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosBeneficiosEventuaisDemandasReprogramacao.Text))
            {
                reprogramacaoDemandasBeneficiosEventuais.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosBeneficiosEventuaisDemandasReprogramacao.Text);
            }
            reprogramacaoDemandasBeneficiosEventuais.ValoresDevolvidosFEAS = (reprogramacaoDemandasBeneficiosEventuais.RecursoDisponibilizadoFEAS + reprogramacaoDemandasBeneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS) - (reprogramacaoDemandasBeneficiosEventuais.ValoresExecutadosFEAS + reprogramacaoDemandasBeneficiosEventuais.ValoresReprogramadosFEAS);

            #endregion

            return reprogramacaoDemandasBeneficiosEventuais;
        }


        ExecucaoFinanceiraInfo PreencherProgramasProjetos(int exercicio)
        {
            var programasProjetos = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 4 && e.Exercicio == exercicio);
            programasProjetos.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            programasProjetos.Exercicio = exercicio;
            programasProjetos.IdSituacao = 1;
            programasProjetos.Desbloqueado = true;
            programasProjetos.IdTipoProtecao = 4;
            programasProjetos.Atualizado = true;

            if (preencher != null)
            {
                #region FMAS
                programasProjetos.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                programasProjetos.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                programasProjetos.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                programasProjetos.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                programasProjetos.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                programasProjetos.ValoresDevolvidosFMAS = (programasProjetos.RecursoDisponibilizadoFMAS + programasProjetos.ResultadoAplicacaoFinanceiraFMAS) - (programasProjetos.ValoresExecutadosFMAS + programasProjetos.ValoresReprogramadosFMAS);
                #endregion

                #region FNAS

                programasProjetos.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                programasProjetos.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                programasProjetos.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                programasProjetos.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                programasProjetos.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                programasProjetos.ValoresDevolvidosFNAS = (programasProjetos.RecursoDisponibilizadoFNAS + programasProjetos.ResultadoAplicacaoFinanceiraFNAS) - (programasProjetos.ValoresExecutadosFNAS + programasProjetos.ValoresReprogramadosFNAS);

                #endregion
            }

            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialProgramasProjetos.Text))
            {
                programasProjetos.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialProgramasProjetos.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosProgramasProjetos.Text))
            {
                programasProjetos.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosProgramasProjetos.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasProgramasProjetos.Text))
            {
                programasProjetos.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasProgramasProjetos.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosProgramasProjetos.Text))
            {
                programasProjetos.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosProgramasProjetos.Text);
            }

            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosProgramasProjetos.Text))
            {
                programasProjetos.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosProgramasProjetos.Text);
            }

            programasProjetos.ValoresDevolvidosFEAS = (programasProjetos.RecursoDisponibilizadoFEAS + programasProjetos.ResultadoAplicacaoFinanceiraFEAS) - (programasProjetos.ValoresExecutadosFEAS + programasProjetos.ValoresReprogramadosFEAS);

            #endregion

            return programasProjetos;
        }

        ExecucaoFinanceiraInfo PreencherProgramasProjetosReprogramacao(int exercicio)
        {
            var programasProjetosReprogramacao = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 13 && e.Exercicio == exercicio);
            programasProjetosReprogramacao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            programasProjetosReprogramacao.Exercicio = exercicio;
            programasProjetosReprogramacao.IdSituacao = 1;
            programasProjetosReprogramacao.Desbloqueado = true;
            programasProjetosReprogramacao.IdTipoProtecao = 13;
            programasProjetosReprogramacao.Atualizado = true;

            if (preencher != null)
            {
                #region FMAS
                programasProjetosReprogramacao.PrevisaoInicialFMAS = preencher.PrevisaoInicialFMAS;

                programasProjetosReprogramacao.RecursoDisponibilizadoFMAS = preencher.RecursoDisponibilizadoFMAS;

                programasProjetosReprogramacao.ResultadoAplicacaoFinanceiraFMAS = preencher.ResultadoAplicacaoFinanceiraFMAS;

                programasProjetosReprogramacao.ValoresExecutadosFMAS = preencher.ValoresExecutadosFMAS;

                programasProjetosReprogramacao.ValoresReprogramadosFMAS = preencher.ValoresReprogramadosFMAS;

                programasProjetosReprogramacao.ValoresDevolvidosFMAS = (programasProjetosReprogramacao.RecursoDisponibilizadoFMAS + programasProjetosReprogramacao.ResultadoAplicacaoFinanceiraFMAS) - (programasProjetosReprogramacao.ValoresExecutadosFMAS + programasProjetosReprogramacao.ValoresReprogramadosFMAS);
                #endregion

                #region FNAS

                programasProjetosReprogramacao.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                programasProjetosReprogramacao.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                programasProjetosReprogramacao.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                programasProjetosReprogramacao.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                programasProjetosReprogramacao.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                programasProjetosReprogramacao.ValoresDevolvidosFNAS = (programasProjetosReprogramacao.RecursoDisponibilizadoFNAS + programasProjetosReprogramacao.ResultadoAplicacaoFinanceiraFNAS) - (programasProjetosReprogramacao.ValoresExecutadosFNAS + programasProjetosReprogramacao.ValoresReprogramadosFNAS);

                #endregion
            }

            #region FEAS
            if (!String.IsNullOrEmpty(txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Text))
            {
                programasProjetosReprogramacao.PrevisaoInicialFEAS = Convert.ToDecimal(txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASRecursosDisponibilizadosProgramasProjetosReprogramacao.Text))
            {
                programasProjetosReprogramacao.RecursoDisponibilizadoFEAS = Convert.ToDecimal(txtFEASRecursosDisponibilizadosProgramasProjetosReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASResultadoAppFinanceirasProgramasProjetosReprogramacao.Text))
            {
                programasProjetosReprogramacao.ResultadoAplicacaoFinanceiraFEAS = Convert.ToDecimal(txtFEASResultadoAppFinanceirasProgramasProjetosReprogramacao.Text);
            }
            if (!String.IsNullOrEmpty(txtFEASValoresExecutadosProgramasProjetosReprogramacao.Text))
            {
                programasProjetosReprogramacao.ValoresExecutadosFEAS = Convert.ToDecimal(txtFEASValoresExecutadosProgramasProjetosReprogramacao.Text);
            }

            if (!String.IsNullOrEmpty(txtFEASValoresReprogramadosProgramasProjetosReprogramacao.Text))
            {
                programasProjetosReprogramacao.ValoresReprogramadosFEAS = Convert.ToDecimal(txtFEASValoresReprogramadosProgramasProjetosReprogramacao.Text);
            }

            programasProjetosReprogramacao.ValoresDevolvidosFEAS = (programasProjetosReprogramacao.RecursoDisponibilizadoFEAS + programasProjetosReprogramacao.ResultadoAplicacaoFinanceiraFEAS) - (programasProjetosReprogramacao.ValoresExecutadosFEAS + programasProjetosReprogramacao.ValoresReprogramadosFEAS);

            #endregion

            return programasProjetosReprogramacao;
        }


        ExecucaoFinanceiraInfo PreencherProtecaoEspecial(int exercicio)
        {
            var protecaoEspecial = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 6 && e.Exercicio == exercicio);
            protecaoEspecial.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            protecaoEspecial.Exercicio = exercicio;
            protecaoEspecial.IdSituacao = 1;
            protecaoEspecial.Desbloqueado = true;
            protecaoEspecial.IdTipoProtecao = 6;

            if (preencher != null)
            {
                #region FNAS

                protecaoEspecial.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                protecaoEspecial.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                protecaoEspecial.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                protecaoEspecial.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                protecaoEspecial.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                protecaoEspecial.ValoresDevolvidosFNAS = (protecaoEspecial.RecursoDisponibilizadoFNAS + protecaoEspecial.ResultadoAplicacaoFinanceiraFNAS) - (protecaoEspecial.ValoresExecutadosFNAS + protecaoEspecial.ValoresReprogramadosFNAS);
                #endregion
            }
            return protecaoEspecial;
        }

        ExecucaoFinanceiraInfo PreencherIncentivo(int exercicio)
        {
            var incentivo = new ExecucaoFinanceiraInfo();
            sessionExecucaoFinanceira = (List<ExecucaoFinanceiraInfo>)Session["sessionExecucaoFinanceira"];
            var preencher = sessionExecucaoFinanceira.FirstOrDefault(e => e.IdTipoProtecao == 7 && e.Exercicio == exercicio);
            incentivo.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            incentivo.Exercicio = exercicio;
            incentivo.IdSituacao = 1;
            incentivo.Desbloqueado = true;
            incentivo.IdTipoProtecao = 7;

            if (preencher != null)
            {
                #region FNAS

                incentivo.PrevisaoInicialFNAS = preencher.PrevisaoInicialFNAS;

                incentivo.RecursoDisponibilizadoFNAS = preencher.RecursoDisponibilizadoFNAS;

                incentivo.ResultadoAplicacaoFinanceiraFNAS = preencher.ResultadoAplicacaoFinanceiraFNAS;

                incentivo.ValoresExecutadosFNAS = preencher.ValoresExecutadosFNAS;

                incentivo.ValoresReprogramadosFNAS = preencher.ValoresReprogramadosFNAS;

                incentivo.ValoresDevolvidosFNAS = (incentivo.RecursoDisponibilizadoFNAS + incentivo.ResultadoAplicacaoFinanceiraFNAS) - (incentivo.ValoresExecutadosFNAS + incentivo.ValoresReprogramadosFNAS);

                #endregion
            }
            return incentivo;
        }

        QuestoesCMASinfo PreencherQuestionarioCMAS()
        {
            var questionario = new QuestoesCMASinfo();

            questionario.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            questionario.Exercicio = Convert.ToInt32(hdfAno.Value);


            if (rblQuestaoUmCMAS.SelectedValue != "0" && rblQuestaoUmCMAS.SelectedValue != "")
            {
                questionario.QuestaoUm = Convert.ToInt32(rblQuestaoUmCMAS.SelectedValue);
            }
            if (rblQuestaoDoisCMAS.SelectedValue != "0" && rblQuestaoDoisCMAS.SelectedValue != "")
            {
                questionario.QuestaoDois = Convert.ToInt32(rblQuestaoDoisCMAS.SelectedValue);
            }
            if (rblQuestaoTresCMAS.SelectedValue != "0" && rblQuestaoTresCMAS.SelectedValue != "")
            {
                questionario.QuestaoTres = Convert.ToInt32(rblQuestaoTresCMAS.SelectedValue);
            }
            if (rblQuestaoQuatroCMAS.SelectedValue != "0" && rblQuestaoQuatroCMAS.SelectedValue != "")
            {
                questionario.QuestaoQuatro = Convert.ToInt32(rblQuestaoQuatroCMAS.SelectedValue);
            }
            if (rblQuestaoCincoCMAS.SelectedValue != "0" && rblQuestaoCincoCMAS.SelectedValue != "")
            {
                questionario.QuestaoCinco = Convert.ToInt32(rblQuestaoCincoCMAS.SelectedValue);
            }
            if (rblQuestaoSeisCMAS.SelectedValue != "0" && rblQuestaoSeisCMAS.SelectedValue != "")
            {
                questionario.QuestaoSeis = Convert.ToInt32(rblQuestaoSeisCMAS.SelectedValue);
            }
            if (rblQuestaoSeisCMAS.SelectedValue == "2")
            {
                questionario.QuestaoSeisEscrita = txtQuestaoSeisCMAS.Text != "" || txtQuestaoSeisCMAS.Text != null ? txtQuestaoSeisCMAS.Text : "";
            }
            if (rblQuestaoSeteCMAS.SelectedValue != "0" && rblQuestaoSeteCMAS.SelectedValue != "")
            {
                questionario.QuestaoSete = Convert.ToInt32(rblQuestaoSeteCMAS.SelectedValue);
            }
            if (rblQuestaoSeteCMAS.SelectedValue == "1")
            {
                questionario.QuestaoSeteEscrita = txtQuestaoSeteCMAS.Text != "" || txtQuestaoSeteCMAS.Text != null ? txtQuestaoSeteCMAS.Text : "";
            }
            if (rblQuestaoOitoCMAS.SelectedValue != "0" && rblQuestaoOitoCMAS.SelectedValue != "")
            {
                questionario.QuestaoOito = Convert.ToInt32(rblQuestaoOitoCMAS.SelectedValue);
            }
            if (rblQuestaoNoveCMAS.SelectedValue != "0" && rblQuestaoNoveCMAS.SelectedValue != "")
            {
                questionario.QuestaoNove = Convert.ToInt32(rblQuestaoNoveCMAS.SelectedValue);
            }

            return questionario;

        }

        QuestoesDRADSInfo PreencherQuestionarioDRADS()
        {
            var questionario = new QuestoesDRADSInfo();

            questionario.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            questionario.Exercicio = Convert.ToInt32(hdfAno.Value);


            if (rblQuestaoUmDRADS.SelectedValue != "0" && rblQuestaoUmDRADS.SelectedValue != "")
            {
                questionario.QuestaoUm = Convert.ToInt32(rblQuestaoUmDRADS.SelectedValue);
            }
            if (rblQuestaoUmDRADS.SelectedValue == "1")
            {
                questionario.QuestaoUmEscrita = txtQuestaoUmDRADS.Text != "" || txtQuestaoUmDRADS.Text != null ? txtQuestaoUmDRADS.Text : "";
            }
            //if (rblQuestaoDoisDRADS.SelectedValue != "0")
            // {
            questionario.QuestaoDois = 0;//Convert.ToInt32(rblQuestaoDoisDRADS.SelectedValue);
            // }
            if (rblQuestaoTresDRADS.SelectedValue != "0" && rblQuestaoTresDRADS.SelectedValue != "")
            {
                questionario.QuestaoTres = Convert.ToInt32(rblQuestaoTresDRADS.SelectedValue);
            }
            if (rblQuestaoQuatroDRADS.SelectedValue != "0" && rblQuestaoQuatroDRADS.SelectedValue != "")
            {
                questionario.QuestaoQuatro = Convert.ToInt32(rblQuestaoQuatroDRADS.SelectedValue);
            }
            if (rblQuestaoCincoDRADS.SelectedValue != "0" && rblQuestaoCincoDRADS.SelectedValue != "")
            {
                questionario.QuestaoCinco = Convert.ToInt32(rblQuestaoCincoDRADS.SelectedValue);
            }
            if (rblQuestaoCincoDRADS.SelectedValue == "2")
            {
                questionario.QuestaoCincoEscrita = txtQuestaoCincoDRADS.Text != "" || txtQuestaoCincoDRADS.Text != null ? txtQuestaoCincoDRADS.Text : "";
            }

            return questionario;
        }

        ComentarioPrestacaoDeContasCMASInfo PreencherComentarioCMAS()
        {
            var comentario = new ComentarioPrestacaoDeContasCMASInfo();

            comentario.Desbloqueado = true;
            comentario.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            comentario.Exercicio = Convert.ToInt32(hdfAno.Value);

            if (!String.IsNullOrEmpty(txtComentarioCMAS.Text))
            {
                comentario.Comentario = txtComentarioCMAS.Text;
            }
            return comentario;
        }

        ComentarioPrestacaoDeContasDRADSInfo PreecherComentarioDRADS()
        {
            var comentario = new ComentarioPrestacaoDeContasDRADSInfo();
            comentario.Desbloqueado = true;
            comentario.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            comentario.Exercicio = Convert.ToInt32(hdfAno.Value);

            if (!String.IsNullOrEmpty(txtComentarioDRADS.Text))
            {
                comentario.Comentario = txtComentarioDRADS.Text;
            }
            return comentario;

        }

        DeliberacaoPrestacaoDeContasCMASInfo PreencherDeliberacaoCMAS()
        {
            var deliberacao = new DeliberacaoPrestacaoDeContasCMASInfo();

            deliberacao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            deliberacao.Exercicio = Convert.ToInt32(hdfAno.Value);

            if (rblDeliberacaoCMAS.SelectedValue != "0" && rblDeliberacaoCMAS.SelectedValue != "")
            {
                deliberacao.QuestaoDeliberacao = Convert.ToInt32(rblDeliberacaoCMAS.SelectedValue);
            }

            if (!String.IsNullOrEmpty(txtDataReuniaoCMAS.Text))
            {
                deliberacao.DataReuniao = Convert.ToDateTime(txtDataReuniaoCMAS.Text);
            }

            if (!String.IsNullOrEmpty(txtNumeroConselheirosCMAS.Text))
            {
                deliberacao.NumeroConselheiros = txtNumeroConselheirosCMAS.Text;
            }

            if (!String.IsNullOrEmpty(txtNumeroAtaCMAS.Text))
            {
                deliberacao.NumeroAta = txtNumeroAtaCMAS.Text;
            }

            if (!String.IsNullOrEmpty(txtNumeroResolucaoCMAS.Text))
            {
                deliberacao.NumeroResolucao = txtNumeroResolucaoCMAS.Text;
            }

            if (!String.IsNullOrEmpty(txtDataPublicacaoCMAS.Text))
            {
                deliberacao.DataPublicacao = Convert.ToDateTime(txtDataPublicacaoCMAS.Text);
            }

            return deliberacao;
        }

        DeliberacaoPrestacaoDeContasDRADSInfo PreencherDeliberacaoDRADS()
        {
            var deliberacao = new DeliberacaoPrestacaoDeContasDRADSInfo();

            deliberacao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            deliberacao.Exercicio = Convert.ToInt32(hdfAno.Value);

            if (rblDeliberacaoDRADS.SelectedValue != "0" && rblDeliberacaoDRADS.SelectedValue != "")
            {
                deliberacao.QuestaoDeliberacao = Convert.ToInt32(rblDeliberacaoDRADS.SelectedValue);
            }

            if (String.IsNullOrEmpty(txtDataReuniaoDRADS.Text))
            {
                deliberacao.DataReuniao = Convert.ToDateTime("01/01/1900 00:00:00");
            }

            if (String.IsNullOrEmpty(txtNumeroConselheirosDRADS.Text))
            {
                deliberacao.NumeroConselheiros = "";
            }

            if (String.IsNullOrEmpty(txtNumeroAtaDRADS.Text))
            {
                deliberacao.NumeroAta = "";
            }

            if (String.IsNullOrEmpty(txtNumeroResolucaoDRADS.Text))
            {
                deliberacao.NumeroResolucao = "";
            }

            if (String.IsNullOrEmpty(txtDataPublicacaoDRADS.Text))
            {
                deliberacao.DataPublicacao = Convert.ToDateTime("01/01/1900 00:00:00");
            }

            return deliberacao;
        }

        void preencherResponsávelGestor()
        {
            using (var proxy = new ProxyPrefeitura())
            {
                var p = new Prefeituras(proxy);
                var gestor = p.GetAtualGestorMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                lblNomeOrgaoGestor.Text = gestor.Nome != null ? gestor.Nome : "Nome não cadastrado.";
                lblCPFOrgaoGestor.Text = gestor.CPF != null ? gestor.CPF : "000.000.000-00";
                lblDataOrgaoGestor.Text = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
            }
        }

        void preencherResponsavelCmas(int idUsuario)
        {
            using (var proxy = new ProxyUsuarioPMAS())
            {
                var usuario = new Usuarios();
                var cmas = usuario.GetUsuarioById(idUsuario, proxy);

                lblNomeCMAS.Text = cmas.Nome != null ? cmas.Nome : "Nome não cadastrado.";
                lblCpfCMAS.Text = cmas.CPF != null ? cmas.CPF : "000.000.000-00";
                lblDataCMAS.Text = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
            }
        }

        void preencherResponsavelDrads(int idUsuario)
        {
            using (var proxy = new ProxyUsuarioPMAS())
            {
                var usuario = new Usuarios();
                var drads = usuario.GetUsuarioById(idUsuario, proxy);

                lblNomeDRADS.Text = drads.Nome != null ? drads.Nome : "Nome não cadastrado.";
                lblCPFDRADS.Text = drads.CPF != null ? drads.CPF : "000.000.000-00";
                lblDataDRADS.Text = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
            }
        }

        bool carregarDemostrativoRecursosEstaduaisProtecaoBasica(int idPrefeitura)
        {
            var proxy = new ProxyPrefeitura();

            int idTipoProtecao = 1;

            int exercicio = Convert.ToInt32(hdfAno.Value);

            var lstBasica = proxy.Service.GetLocaisExecucaoPrestacaoDeContas(idPrefeitura, idTipoProtecao, exercicio);


            hdnSomaCapacidadeMediaAtendimentoBasica.Value = lstBasica.Sum(s => s.CapacidadeDeAtendimento).ToString();
            lblTotalCapacidadeMensalBasica.Text = hdnSomaCapacidadeMediaAtendimentoBasica.Value;

            hdnMediaMensalAtendimentoBasica.Value = lstBasica.Sum(s => s.MediaMensalDeAtendimento).ToString();
            lblTotalMediaMensalBasica.Text = hdnMediaMensalAtendimentoBasica.Value;

            hdnCofinanciamentoEstadualBasica.Value = lstBasica.Sum(s => s.CofinanciamentoEstadual).ToString("N2");
            lblTotalCofinanciamentoEstadualBasica.Text = hdnCofinanciamentoEstadualBasica.Value;

            hdnRecursosReprogramadosBasica.Value = lstBasica.Sum(s => s.RecursosReprogramadosAnoAnterior).ToString("N2");
            lblReprogramacaoBasica.Text = hdnRecursosReprogramadosBasica.Value;

            hdnDemandasBasica.Value = lstBasica.Sum(s => s.ValoresDemandasParlamentares).ToString("N2");
            lblTotalDemandasBasica.Text = hdnDemandasBasica.Value;

            hdnDemandasReprogramacaoBasica.Value = lstBasica.Sum(s => s.ValoresDemandasParlamentaresReprogramados).ToString("N2");
            lblTotalDemandasReprogramacaoBasica.Text = hdnDemandasReprogramacaoBasica.Value;

            hdnValoresAplicacoesBasica.Value = lstBasica.Sum(s => s.ValorAplicacoesFinanceiras).ToString("N2");
            lblTotalValoresAplicacoesBasica.Text = hdnValoresAplicacoesBasica.Value;

            hdnRecursosHumanosBasica.Value = lstBasica.Sum(s => s.RecursosHumanos).ToString("N2");
            lblTotalRecursosHumanosBasica.Text = hdnRecursosHumanosBasica.Value;

            hdnMaterialDeConsumoBasica.Value = lstBasica.Sum(s => s.MaterialDeConsumo).ToString("N2");
            lblTotalMaterialConsumoBasica.Text = hdnMaterialDeConsumoBasica.Value;

            hdnOutrasDespesasBasica.Value = lstBasica.Sum(s => s.OutrasDespesas).ToString("N2");
            lblTotalOutrasDespesasBasica.Text = hdnOutrasDespesasBasica.Value;


            decimal CofinanciamentoEstadualMaisDemandasBasica = Convert.ToDecimal(hdnCofinanciamentoEstadualBasica.Value) + Convert.ToDecimal(hdnDemandasBasica.Value);
            decimal SomaDosReprogramadosBasica = Convert.ToDecimal(hdnRecursosReprogramadosBasica.Value) + Convert.ToDecimal(hdnDemandasReprogramacaoBasica.Value);

            hdnCofinanciamentoEstadualMaisDemandasBasica.Value = CofinanciamentoEstadualMaisDemandasBasica.ToString("N2");

            hdnSomaDosReprogramadosBasica.Value = SomaDosReprogramadosBasica.ToString();
            if (lstBasica != null && lstBasica.Count != 0)
            {
                tbTotalBasica.Visible = true;

            }
            else
            {
                tbTotalBasica.Visible = false;

            }


            lstProtecaoBasica.DataSource = lstBasica;
            lstProtecaoBasica.DataBind();

            if (lstBasica != null && lstBasica.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        bool carregarDemostrativoRecursosEstaduaisProtecaoMedia(int idPrefeitura)
        {
            var proxy = new ProxyPrefeitura();

            int idTipoProtecao = 2;

            int exercicio = Convert.ToInt32(hdfAno.Value);

            var lstMedia = proxy.Service.GetLocaisExecucaoPrestacaoDeContas(idPrefeitura, idTipoProtecao, exercicio);

            hdnSomaCapacidadeMediaAtendimentoMedia.Value = lstMedia.Sum(s => s.CapacidadeDeAtendimento).ToString();
            lblTotalCapacidadeMensalMedia.Text = hdnSomaCapacidadeMediaAtendimentoMedia.Value;

            hdnMediaMensalAtendimentoMedia.Value = lstMedia.Sum(s => s.MediaMensalDeAtendimento).ToString();
            lblTotalMediaMensalMedia.Text = hdnMediaMensalAtendimentoMedia.Value;

            hdnCofinanciamentoEstadualMedia.Value = lstMedia.Sum(s => s.CofinanciamentoEstadual).ToString("N2");
            lblTotalCofinanciamentoEstadualMedia.Text = hdnCofinanciamentoEstadualMedia.Value;

            hdnRecursosReprogramadosMedia.Value = lstMedia.Sum(s => s.RecursosReprogramadosAnoAnterior).ToString("N2");
            lblReprogramacaoMedia.Text = hdnRecursosReprogramadosMedia.Value;

            hdnDemandasMedia.Value = lstMedia.Sum(s => s.ValoresDemandasParlamentares).ToString("N2");
            lblTotalDemandasMedia.Text = hdnDemandasMedia.Value;

            hdnDemandasReprogramacaoMedia.Value = lstMedia.Sum(s => s.ValoresDemandasParlamentaresReprogramados).ToString("N2");
            lblTotalDemandasReprogramacaoMedia.Text = hdnDemandasReprogramacaoMedia.Value;

            hdnValoresAplicacoesMedia.Value = lstMedia.Sum(s => s.ValorAplicacoesFinanceiras).ToString("N2");
            lblTotalValoresAplicacoesMedia.Text = hdnValoresAplicacoesMedia.Value;

            hdnRecursosHumanosMedia.Value = lstMedia.Sum(s => s.RecursosHumanos).ToString("N2");
            lblTotalRecursosHumanosMedia.Text = hdnRecursosHumanosMedia.Value;

            hdnMaterialDeConsumoMedia.Value = lstMedia.Sum(s => s.MaterialDeConsumo).ToString("N2");
            lblTotalMaterialConsumoMedia.Text = hdnMaterialDeConsumoMedia.Value;

            hdnOutrasDespesasMedia.Value = lstMedia.Sum(s => s.OutrasDespesas).ToString("N2");
            lblTotalOutrasDespesasMedia.Text = hdnOutrasDespesasMedia.Value;

            decimal CofinanciamentoEstadualMaisDemandasMedia = Convert.ToDecimal(hdnCofinanciamentoEstadualMedia.Value) + Convert.ToDecimal(hdnDemandasMedia.Value);
            decimal SomaDosReprogramadosMedia = Convert.ToDecimal(hdnRecursosReprogramadosMedia.Value) + Convert.ToDecimal(hdnDemandasReprogramacaoMedia.Value);

            hdnCofinanciamentoEstadualMaisDemandasMedia.Value = CofinanciamentoEstadualMaisDemandasMedia.ToString();

            hdnSomaDosReprogramadosMedia.Value = SomaDosReprogramadosMedia.ToString();

            if (lstMedia != null && lstMedia.Count != 0)
            {
                tbTotalMedia.Visible = true;
            }
            else
            {
                tbTotalMedia.Visible = false;
            }

            lstProtecaoMedia.DataSource = lstMedia;
            lstProtecaoMedia.DataBind();

            if (lstMedia != null && lstMedia.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool carregarDemostrativoRecursosEstaduaisProtecaoAlta(int idPrefeitura)
        {
            var proxy = new ProxyPrefeitura();

            int idTipoProtecao = 3;

            int exercicio = Convert.ToInt32(hdfAno.Value);

            var lstAlta = proxy.Service.GetLocaisExecucaoPrestacaoDeContas(idPrefeitura, idTipoProtecao, exercicio);

            hdnSomaCapacidadeMediaAtendimentoAlta.Value = lstAlta.Sum(s => s.CapacidadeDeAtendimento).ToString();
            lblTotalCapacidadeMensalAlta.Text = hdnSomaCapacidadeMediaAtendimentoAlta.Value;

            hdnMediaMensalAtendimentoAlta.Value = lstAlta.Sum(s => s.MediaMensalDeAtendimento).ToString();
            lblTotalMediaMensalAlta.Text = hdnMediaMensalAtendimentoAlta.Value;

            hdnCofinanciamentoEstadualAlta.Value = lstAlta.Sum(s => s.CofinanciamentoEstadual).ToString("N2");
            lblTotalCofinanciamentoEstadualAlta.Text = hdnCofinanciamentoEstadualAlta.Value;

            hdnRecursosReprogramadosAlta.Value = lstAlta.Sum(s => s.RecursosReprogramadosAnoAnterior).ToString("N2");
            lblReprogramacaoAlta.Text = hdnRecursosReprogramadosAlta.Value;

            hdnDemandasAlta.Value = lstAlta.Sum(s => s.ValoresDemandasParlamentares).ToString("N2");
            lblTotalDemandasAlta.Text = hdnDemandasAlta.Value;

            hdnDemandasReprogramacaoAlta.Value = lstAlta.Sum(s => s.ValoresDemandasParlamentaresReprogramados).ToString("N2");
            lblTotalDemandasReprogramacaoAlta.Text = hdnDemandasReprogramacaoAlta.Value;

            hdnValoresAplicacoesAlta.Value = lstAlta.Sum(s => s.ValorAplicacoesFinanceiras).ToString("N2");
            lblTotalValoresAplicacoesAlta.Text = hdnValoresAplicacoesAlta.Value;

            hdnRecursosHumanosAlta.Value = lstAlta.Sum(s => s.RecursosHumanos).ToString("N2");
            lblTotalRecursosHumanosAlta.Text = hdnRecursosHumanosAlta.Value;

            hdnMaterialDeConsumoAlta.Value = lstAlta.Sum(s => s.MaterialDeConsumo).ToString("N2");
            lblTotalMaterialConsumoAlta.Text = hdnMaterialDeConsumoAlta.Value;

            hdnOutrasDespesasAlta.Value = lstAlta.Sum(s => s.OutrasDespesas).ToString("N2");
            lblTotalOutrasDespesasAlta.Text = hdnOutrasDespesasAlta.Value;

            decimal CofinanciamentoEstadualMaisDemandasAlta = Convert.ToDecimal(hdnCofinanciamentoEstadualAlta.Value) + Convert.ToDecimal(hdnDemandasAlta.Value);
            decimal SomaDosReprogramadosAlta = Convert.ToDecimal(hdnRecursosReprogramadosAlta.Value) + Convert.ToDecimal(hdnDemandasReprogramacaoAlta.Value);

            hdnCofinanciamentoEstadualMaisDemandasAlta.Value = CofinanciamentoEstadualMaisDemandasAlta.ToString();

            hdnSomaDosReprogramadosAlta.Value = SomaDosReprogramadosAlta.ToString();

            if (lstAlta != null && lstAlta.Count != 0)
            {
                tbTotalAlta.Visible = true;
            }
            else
            {
                tbTotalAlta.Visible = false;
            }


            lstProtecaoAlta.DataSource = lstAlta;
            lstProtecaoAlta.DataBind();

            if (lstAlta != null && lstAlta.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        bool carregarDemonstrativoRecursosEstaduaisProgramasProjetos(int idPrefeitura)
        {
            var proxy = new ProxyPrefeitura();
            int exercicio = Convert.ToInt32(hdfAno.Value);

            Session["Exercicio"] = exercicio;

            var lstPrograma = proxy.Service.GetPrestacaoDeContasProgramaProjeto(idPrefeitura, exercicio);



                lblTotalDemandasEstimadas.Text = lstPrograma.Sum(s => s.DemandaEstimada).ToString();

                lblTotalNumeroAtendidos.Text = lstPrograma.Sum(s => s.NumeroAtendidos).ToString();

                hdnCofinanciamentoEstadualProgramasProjetos.Value = lstPrograma.Sum(s => s.CofinanciamentoEstadual).ToString("N2");
                lbltotalCofinanciamentoEstadualProgramasProjetos.Text = hdnCofinanciamentoEstadualProgramasProjetos.Value;

                hdnRecursosReprogramadosProgramasProjetos.Value = lstPrograma.Sum(s => s.RecursosReprogramadosAnoAnterior).ToString("N2");
                lbltotalReprogramacaoProgramasProjetos.Text = hdnRecursosReprogramadosProgramasProjetos.Value;

                hdnValoresAplicacoesProgramasProjetos.Value = lstPrograma.Sum(s => s.ValorAplicacoesFinanceiras).ToString("N2");
                lbltotalValoresAplicacoesProgramasProjetos.Text = hdnValoresAplicacoesProgramasProjetos.Value;

                hdnRecursosHumanosProgramasProjetos.Value = lstPrograma.Sum(s => s.RecursosHumanos).ToString("N2");
                lbltotalRecursosHumanosProgramasProjetos.Text = hdnRecursosHumanosProgramasProjetos.Value;

                hdnMaterialDeConsumoProgramasProjetos.Value = lstPrograma.Sum(s => s.MaterialDeConsumo).ToString("N2");
                lbltotalMaterialConsumoProgramasProjetos.Text = hdnMaterialDeConsumoProgramasProjetos.Value;

                hdnOutrasDespesasProgramasProjetos.Value = lstPrograma.Sum(s => s.OutrasDespesas).ToString("N2");
                lbltotalOutrasDespesasProgramasProjetos.Text = hdnOutrasDespesasProgramasProjetos.Value;

            if (lstPrograma != null && lstPrograma.Count != 0)
            {
                tbTotalProgramaProjeto.Visible = true;
            }

            else
            {
                tbTotalProgramaProjeto.Visible = false;

            }

            lstProgramaProjetos.DataSource = lstPrograma;
            lstProgramaProjetos.DataBind();

            if (lstPrograma != null && lstPrograma.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        bool carregarDemonstrativoRecursosEstaduaisBeneficiosEventuais(int idPrefeitura)
        {
            var proxy = new ProxyPrefeitura();
            int exercicio = Convert.ToInt32(hdfAno.Value);
            var lstBeneficios = proxy.Service.GetPrestacaoDeContasBeneficiosEventuais(idPrefeitura, exercicio);



                hdnQuantidadeBeneficiariosBeneficiosEventuais.Value = lstBeneficios.Sum(s => s.QuantidadeAnualBeneficiarios).ToString();
                lblTotalQuantidadeBeneficiariosBeneficiosEventuais.Text = hdnQuantidadeBeneficiariosBeneficiosEventuais.Value;

                hdnQuantidadeBeneficiariosConcedidosBeneficiosEventuais.Value = lstBeneficios.Sum(s => s.QuantidadeAnualBeneficiariosConcedidos).ToString();
                lblTotalQuantidadeBeneficiariosConcedidosBeneficiosEventuais.Text = hdnQuantidadeBeneficiariosConcedidosBeneficiosEventuais.Value;

                hdnCofinanciamentoEstadualBeneficiosEventuais.Value = lstBeneficios.Sum(s => s.CofinanciamentoEstadual).ToString("N2");
                lblTotalCofinanciamentoEstadualBeneficiosEventuais.Text = hdnCofinanciamentoEstadualBeneficiosEventuais.Value;

                hdnReprogramadosBeneficiosEventuais.Value = lstBeneficios.Sum(s => s.ValorRecursosReprogramados).ToString("N2");
                lblTotalReprogramadosBeneficiosEventuais.Text = hdnReprogramadosBeneficiosEventuais.Value;

                hdnDemandasReprogramacaoBeneficiosEventuais.Value = lstBeneficios.Sum(s => s.ValoresDemandasParlamentaresReprogramacao).ToString("N2");
                lblTotalDemandasBeneficiosEventuaisReprogramacao.Text = hdnDemandasReprogramacaoBeneficiosEventuais.Value;


                hdnDemandasBeneficiosEventuais.Value = lstBeneficios.Sum(s => s.ValoresDemandasParlamentares).ToString("N2");
                lblTotalDemandasBeneficiosEventuais.Text = hdnDemandasBeneficiosEventuais.Value;

                hdnValoresAplicacoesFinanceirasBeneficiosEventuais.Value = lstBeneficios.Sum(s => s.ValorAplicacoesFinanceiras).ToString("N2");
                lblTotalValoresAplicacoesFinanceirasBeneficiosEventuais.Text = hdnValoresAplicacoesFinanceirasBeneficiosEventuais.Value;

                hdnRecursosHumanosBeneficiosEventuais.Value = lstBeneficios.Sum(s => s.RecursosHumanos).ToString("N2");
                lblTotalRecursosHumanosBeneficiosEventuais.Text = hdnRecursosHumanosBeneficiosEventuais.Value;

                hdnMaterialConsumoBeneficiosEventuais.Value = lstBeneficios.Sum(s => s.MaterialDeConsumo).ToString("N2");
                lblTotalMaterialConsumoBeneficiosEventuais.Text = hdnMaterialConsumoBeneficiosEventuais.Value;

                hdnOutrasDespesasBeneficiosEventuais.Value = lstBeneficios.Sum(s => s.OutrasDespesas).ToString("N2");
                lblTotalOutrasDespesasBeneficiosEventuais.Text = hdnOutrasDespesasBeneficiosEventuais.Value;

                decimal CofinanciamentoEstadualMaisDemandasBeneficiosEventuais = Convert.ToDecimal(hdnCofinanciamentoEstadualBeneficiosEventuais.Value) + Convert.ToDecimal(hdnDemandasBeneficiosEventuais.Value);
                hdnCofinanciamentoEstadualMaisDemandasBeneficiosEventuais.Value = CofinanciamentoEstadualMaisDemandasBeneficiosEventuais.ToString();


            if (lstBeneficios != null && lstBeneficios.Count != 0)
            {
                tbTotalBeneficiosEventuais.Visible = true;
            }
            else
            {
                tbTotalBeneficiosEventuais.Visible = false;

            }

            lstBeneficiosEventuais.DataSource = lstBeneficios;
            lstBeneficiosEventuais.DataBind();

            if (lstBeneficios != null && lstBeneficios.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        void carregarBasica(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialBasica.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasBasica.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASRecursosDisponibilizadosBasica.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASValoresExecutadosBasica.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosBasica.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosBasica.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoBasica.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEAS.ToString("P2");

        }

        void clearBasica()
        {
            txtFEASPrevisaoInicialBasica.Text = "0,00";
            txtFEASResultadoAppFinanceirasBasica.Text = "0,00";
            txtFEASRecursosDisponibilizadosBasica.Text = "0,00";
            txtFEASValoresExecutadosBasica.Text = "0,00";
            txtFEASValoresReprogramadosBasica.Text = "0,00";
            txtFEASValoresDevolvidosBasica.Text = "0,00";
            txtFEASPorcentagensExecucaoBasica.Text = "0,00";

        }

        void carregarReprogramacaoBasica(ExecucaoFinanceiraInfo e)
        {
            //txtReprogramacaoPrevisaoInicialBasica.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtReprogramacaoResultadoAppFinanceirasBasica.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtReprogramacaoRecursosDisponibilizadosBasica.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtReprogramacaoValoresExecutadosBasica.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtReprogramacaoValoresReprogramadosBasica.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtReprogramacaoValoresDevolvidosBasica.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtReprogramacaoPorcentagensBasica.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");

        }

        void carregarDemandasParlamentaresBasica(ExecucaoFinanceiraInfo e) 
        {
            //txtFEASPrevisaoInicialBasicaDemandas
            txtFEASRecursosDisponibilizadosBasicaDemandas.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasBasicaDemandas.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASValoresExecutadosBasicaDemandas.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosBasicaDemandas.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosBasicaDemandas.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoBasicaDemandas.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void carregarReprogramacaoDemandasParlamentaresBasica(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialBasicaReprogramacaoDemandas.Text
            txtFEASRecursosDisponibilizadosBasicaReprogramacaoDemandas.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasBasicaReprogramacaoDemandas.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASValoresExecutadosBasicaReprogramacaoDemandas.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosBasicaReprogramacaoDemandas.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosBasicaReprogramacaoDemandas.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoBasicaReprogramacaoDemandas.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void clearDemandasParlamentaresBasica()
        {
            txtFEASRecursosDisponibilizadosBasicaDemandas.Text = "0,00";
            txtFEASResultadoAppFinanceirasBasicaDemandas.Text = "0,00";
            txtFEASValoresExecutadosBasicaDemandas.Text = "0,00";
            txtFEASValoresReprogramadosBasicaDemandas.Text = "0,00";
            txtFEASValoresDevolvidosBasicaDemandas.Text = "0,00";
            txtFEASPorcentagensExecucaoBasicaDemandas.Text = "0,00";
        }

        void clearReprogramacaoDemandasParlamentaresBasica()
        {
            txtFEASRecursosDisponibilizadosBasicaReprogramacaoDemandas.Text = "0,00";
            txtFEASResultadoAppFinanceirasBasicaReprogramacaoDemandas.Text = "0,00";
            txtFEASValoresExecutadosBasicaReprogramacaoDemandas.Text = "0,00";
            txtFEASValoresReprogramadosBasicaReprogramacaoDemandas.Text = "0,00";
            txtFEASValoresDevolvidosBasicaReprogramacaoDemandas.Text = "0,00";
            txtFEASPorcentagensExecucaoBasicaReprogramacaoDemandas.Text = "0,00";
        }

        void clearReprogramacaoBasica()
        {
            txtReprogramacaoPrevisaoInicialBasica.Text = "0,00";
            txtReprogramacaoResultadoAppFinanceirasBasica.Text = "0,00";
            txtReprogramacaoRecursosDisponibilizadosBasica.Text = "0,00";
            txtReprogramacaoValoresExecutadosBasica.Text = "0,00";
            txtReprogramacaoValoresReprogramadosBasica.Text = "0,00";
            txtReprogramacaoValoresDevolvidosBasica.Text = "0,00";
            txtReprogramacaoPorcentagensBasica.Text = "0,00";

        }

        void carregarReprogramado(ExecucaoFinanceiraInfo e)
        {

            txtPrevisaoInicialReprogramacao.Text = e.PrevisaoInicialFEAS.ToString("N2");//e.PrevisaoInicialFEASReprogramado.HasValue ? e.PrevisaoInicialFEASReprogramado.Value.ToString("N2") : "0,00";
            txtRecursosDisponibilizadosReprogramacao.Text = e.RecursoDisponibilizadoFEAS.ToString("N2"); //e.RecursoDisponibilizadoFEASReprogramado.HasValue ? e.RecursoDisponibilizadoFEASReprogramado.Value.ToString("N2") : "0,00";
            txtResultadosAplicacaoReprogramacao.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");//e.ResultadoAplicacaoFinanceiraFEASReprogramado.HasValue ? e.ResultadoAplicacaoFinanceiraFEASReprogramado.Value.ToString("N2") :"0,00";
            txtValoresExecutadosReprogramacao.Text = e.ValoresExecutadosFEAS.ToString("N2");//e.ValoresExecutadosFEASReprogramado.HasValue ? e.ValoresExecutadosFEASReprogramado.Value.ToString("N2") : "0,00";
            txtValoresReprogramacao.Text = e.ValoresReprogramadosFEAS.ToString("N2"); //e.ValoresReprogramadosFEASReprogramado.HasValue ? e.ValoresReprogramadosFEASReprogramado.Value.ToString("N2") : "0,00";

            var ValoresDevolvidosFEAS = e.ValoresDevolvidosFEAS;
            txtValoresDevolvidosReprogramacao.Text = ValoresDevolvidosFEAS.ToString("N2");

            txtPorcentagensDevolucaoReprogramacao.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");

        }

        void carregarMedia(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialMedia.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasMedia.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASRecursosDisponibilizadosMedia.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASValoresExecutadosMedia.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosMedia.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosMedia.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoMedia.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEAS.ToString("P2");
        }

        void clearMedia()
        {
            txtFEASPrevisaoInicialMedia.Text = "0,00";
            txtFEASResultadoAppFinanceirasMedia.Text = "0,00";
            txtFEASRecursosDisponibilizadosMedia.Text = "0,00";
            txtFEASValoresExecutadosMedia.Text = "0,00";
            txtFEASValoresReprogramadosMedia.Text = "0,00";
            txtFEASValoresDevolvidosMedia.Text = "0,00";
            txtFEASPorcentagensExecucaoMedia.Text = "0,00";
        }

        void carregarReprogramacaoMedia(ExecucaoFinanceiraInfo e)
        {
            //txtReprogramacaoPrevisaoInicialMedia.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtReprogramacaoResultadosAppFinanceirasMedia.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtReprogramacaoRecursosDisponibilizadosMedia.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtReprogramacaoValoresExecutadosMedia.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtReprogramacaoValoresReprogramadosMedia.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtReprogramacaoValoresDevolvidosMedia.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtReprogramacaoPorcentagensMedia.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void carregarDemandasParlamentaresMedia(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialMediaDemandas
            txtFEASRecursosDisponibilizadosMediaDemandas.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasMediaDemandas.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASValoresExecutadosMediaDemandas.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosMediaDemandas.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosMediaDemandas.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoMediaDemandas.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void carregarReprogramacaoDemandasParlamentaresMedia(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialMediaReprogramacaoDemandas.Text
            txtFEASRecursosDisponibilizadosMediaReprogramacaoDemandas.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasMediaReprogramacaoDemandas.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASValoresExecutadosMediaReprogramacaoDemandas.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosMediaReprogramacaoDemandas.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosMediaReprogramacaoDemandas.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoMediaReprogramacaoDemandas.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void clearDemandasParlamentaresMedia()
        {
            txtFEASRecursosDisponibilizadosMediaDemandas.Text = "0,00";
            txtFEASResultadoAppFinanceirasMediaDemandas.Text = "0,00";
            txtFEASValoresExecutadosMediaDemandas.Text = "0,00";
            txtFEASValoresReprogramadosMediaDemandas.Text = "0,00";
            txtFEASValoresDevolvidosMediaDemandas.Text = "0,00";
            txtFEASPorcentagensExecucaoMediaDemandas.Text = "0,00";
        }

        void clearReprogramacaoDemandasParlamentaresMedia()
        {
            txtFEASRecursosDisponibilizadosMediaReprogramacaoDemandas.Text = "0,00";
            txtFEASResultadoAppFinanceirasMediaReprogramacaoDemandas.Text = "0,00";
            txtFEASValoresExecutadosMediaReprogramacaoDemandas.Text = "0,00";
            txtFEASValoresReprogramadosMediaReprogramacaoDemandas.Text = "0,00";
            txtFEASValoresDevolvidosMediaReprogramacaoDemandas.Text = "0,00";
            txtFEASPorcentagensExecucaoMediaReprogramacaoDemandas.Text = "0,00";
        }

        void clearReprogramacaoMedia()
        {
            txtReprogramacaoPrevisaoInicialMedia.Text = "0,00";
            txtReprogramacaoResultadosAppFinanceirasMedia.Text = "0,00";
            txtReprogramacaoRecursosDisponibilizadosMedia.Text = "0,00";
            txtReprogramacaoValoresExecutadosMedia.Text = "0,00";
            txtReprogramacaoValoresReprogramadosMedia.Text = "0,00";
            txtReprogramacaoValoresDevolvidosMedia.Text = "0,00";
            txtReprogramacaoPorcentagensMedia.Text = "0,00";
        }

        void carregarAlta(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialAlta.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasAlta.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASRecursosDisponibilizadosAlta.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASValoresExecutadosAlta.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosAlta.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosAlta.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoAlta.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEAS.ToString("P2");
        }

        void clearAlta()
        {
            txtFEASPrevisaoInicialAlta.Text = "0,00";
            txtFEASResultadoAppFinanceirasAlta.Text = "0,00";
            txtFEASRecursosDisponibilizadosAlta.Text = "0,00";
            txtFEASValoresExecutadosAlta.Text = "0,00";
            txtFEASValoresReprogramadosAlta.Text = "0,00";
            txtFEASValoresDevolvidosAlta.Text = "0,00";
            txtFEASPorcentagensExecucaoAlta.Text = "0,00";
        }

        void carregarReprogramacaoAlta(ExecucaoFinanceiraInfo e)
        {
            //txtReprogramacaoPrevisaoInicialAlta.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtReprogramacaoResultadoAppFinanceirasAlta.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtReprogramacaoRecursosDisponibilizadosAlta.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtReprogramacaoValoresExecutadosAlta.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtReprogramacaoValoresReprogramadosAlta.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtReprogramacaoValoresDevolvidosAlta.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtReprogramacaoPorcentagensAlta.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void carregarDemandasParlamentaresAlta(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialAltaDemandas
            txtFEASRecursosDisponibilizadosAltaDemandas.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasAltaDemandas.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASValoresExecutadosAltaDemandas.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosAltaDemandas.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosAltaDemandas.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoAltaDemandas.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void carregarReprogramacaoDemandasParlamentaresAlta(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialAltaReprogramacaoDemandas.Text
            txtFEASRecursosDisponibilizadosAltaReprogramacaoDemandas.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasAltaReprogramacaoDemandas.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASValoresExecutadosAltaReprogramacaoDemandas.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosAltaReprogramacaoDemandas.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosAltaReprogramacaoDemandas.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoAltaReprogramacaoDemandas.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void clearDemandasParlamentaresAlta()
        {
            txtFEASRecursosDisponibilizadosAltaDemandas.Text = "0,00";
            txtFEASResultadoAppFinanceirasAltaDemandas.Text = "0,00";
            txtFEASValoresExecutadosAltaDemandas.Text = "0,00";
            txtFEASValoresReprogramadosAltaDemandas.Text = "0,00";
            txtFEASValoresDevolvidosAltaDemandas.Text = "0,00";
            txtFEASPorcentagensExecucaoAltaDemandas.Text = "0,00";
        }

        void clearReprogramacaoDemandasParlamentaresAlta()
        {
            txtFEASRecursosDisponibilizadosAltaReprogramacaoDemandas.Text = "0,00";
            txtFEASResultadoAppFinanceirasAltaReprogramacaoDemandas.Text = "0,00";
            txtFEASValoresExecutadosAltaReprogramacaoDemandas.Text = "0,00";
            txtFEASValoresReprogramadosAltaReprogramacaoDemandas.Text = "0,00";
            txtFEASValoresDevolvidosAltaReprogramacaoDemandas.Text = "0,00";
            txtFEASPorcentagensExecucaoAltaReprogramacaoDemandas.Text = "0,00";
        }

        void clearReprogramacaoAlta()
        {
            txtReprogramacaoPrevisaoInicialAlta.Text = "0,00";
            txtReprogramacaoResultadoAppFinanceirasAlta.Text = "0,00";
            txtReprogramacaoRecursosDisponibilizadosAlta.Text = "0,00";
            txtReprogramacaoValoresExecutadosAlta.Text = "0,00";
            txtReprogramacaoValoresReprogramadosAlta.Text = "0,00";
            txtReprogramacaoValoresDevolvidosAlta.Text = "0,00";
            txtReprogramacaoPorcentagensAlta.Text = "0,00";
        }

        void carregarBeneficiosEventuais(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialBeneficiosEventuais.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasBeneficiosEventuais.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASRecursosDisponibilizadosBeneficiosEventuais.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASValoresExecutadosBeneficiosEventuais.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosBeneficiosEventuais.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosBeneficiosEventuais.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoBeneficiosEventuais.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEAS.ToString("P2");
        }

        void clearBeneficiosEventuais()
        {
            txtFEASPrevisaoInicialBeneficiosEventuais.Text = "0,00";
            txtFEASResultadoAppFinanceirasBeneficiosEventuais.Text = "0,00";
            txtFEASRecursosDisponibilizadosBeneficiosEventuais.Text = "0,00";
            txtFEASValoresExecutadosBeneficiosEventuais.Text = "0,00";
            txtFEASValoresReprogramadosBeneficiosEventuais.Text = "0,00";
            txtFEASValoresDevolvidosBeneficiosEventuais.Text = "0,00";
            txtFEASPorcentagensExecucaoBeneficiosEventuais.Text = "0,00";
        }

        void carregarReprogramacaoBeneficiosEventuais(ExecucaoFinanceiraInfo e)
        {
            //txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtReprogramacaoResultadosAppFinanceirasBeneficiosEventuais.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtReprogramacaoRecursosDisponiveisBeneficiosEventuais.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtReprogramacaoValoresExecutadosBeneficiosEventuais.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtReprogramacaoValoresReprogramadosBeneficiosEventuais.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtReprogramacaoValoresDevolvidosBeneficiosEventuais.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtReprogramacaoPorcentagensBeneficiosEventuais.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void clearReprogramacaoBeneficiosEventuais()
        {
            txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text = "0,00";
            txtReprogramacaoResultadosAppFinanceirasBeneficiosEventuais.Text = "0,00";
            txtReprogramacaoRecursosDisponiveisBeneficiosEventuais.Text = "0,00";
            txtReprogramacaoValoresExecutadosBeneficiosEventuais.Text = "0,00";
            txtReprogramacaoValoresReprogramadosBeneficiosEventuais.Text = "0,00";
            txtReprogramacaoValoresDevolvidosBeneficiosEventuais.Text = "0,00";
            txtReprogramacaoPorcentagensBeneficiosEventuais.Text = "0,00";
        }

        void carregarDemandasParlamentaresBeneficiosEventuais(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialBeneficiosEventuaisDemandas
            txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandas.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandas.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASValoresExecutadosBeneficiosEventuaisDemandas.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosBeneficiosEventuaisDemandas.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosBeneficiosEventuaisDemandas.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandas.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void carregarReprogramacaoDemandasParlamentaresBeneficiosEventuais(ExecucaoFinanceiraInfo e)
        {
            //txtFEASPrevisaoInicialBeneficiosEventuaisReprogramacaoDemandas.Text
            txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandasReprogramacao.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandasReprogramacao.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASValoresExecutadosBeneficiosEventuaisDemandasReprogramacao.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosBeneficiosEventuaisDemandasReprogramacao.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosBeneficiosEventuaisDemandasReprogramacao.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandasReprogramacao.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEASReprogramado.ToString("P2");
        }

        void clearDemandasParlamentaresBeneficiosEventuais()
        {
            txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandas.Text = "0,00";
            txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandas.Text = "0,00";
            txtFEASValoresExecutadosBeneficiosEventuaisDemandas.Text = "0,00";
            txtFEASValoresReprogramadosBeneficiosEventuaisDemandas.Text = "0,00";
            txtFEASValoresDevolvidosBeneficiosEventuaisDemandas.Text = "0,00";
            txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandas.Text = "0,00";
        }

        void clearReprogramacaoDemandasParlamentaresBeneficiosEventuais()
        {
            txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandasReprogramacao.Text = "0,00";
            txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandasReprogramacao.Text = "0,00";
            txtFEASValoresExecutadosBeneficiosEventuaisDemandasReprogramacao.Text = "0,00";
            txtFEASValoresReprogramadosBeneficiosEventuaisDemandasReprogramacao.Text = "0,00";
            txtFEASValoresDevolvidosBeneficiosEventuaisDemandasReprogramacao.Text = "0,00";
            txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandasReprogramacao.Text = "0,00";
        }

        void carregarProgramasProjetos(ExecucaoFinanceiraInfo e)
        {
            txtFEASPrevisaoInicialProgramasProjetos.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasProgramasProjetos.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASRecursosDisponibilizadosProgramasProjetos.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASValoresExecutadosProgramasProjetos.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosProgramasProjetos.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosProgramasProjetos.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoProgramasProjetos.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEAS.ToString("P2");
        }

        void carregarProgramasProjetosReprogramacao(ExecucaoFinanceiraInfo e)
        {
            txtFEASPrevisaoInicialProgramasProjetos.Text = e.PrevisaoInicialFEAS.ToString("N2");
            txtFEASResultadoAppFinanceirasProgramasProjetosReprogramacao.Text = e.ResultadoAplicacaoFinanceiraFEAS.ToString("N2");
            txtFEASRecursosDisponibilizadosProgramasProjetosReprogramacao.Text = e.RecursoDisponibilizadoFEAS.ToString("N2");
            txtFEASValoresExecutadosProgramasProjetosReprogramacao.Text = e.ValoresExecutadosFEAS.ToString("N2");
            txtFEASValoresReprogramadosProgramasProjetosReprogramacao.Text = e.ValoresReprogramadosFEAS.ToString("N2");
            txtFEASValoresDevolvidosProgramasProjetosReprogramacao.Text = e.ValoresDevolvidosFEAS.ToString("N2");
            txtFEASPorcentagensExecucaoProgramasProjetosReprogramacao.Text = e.PorcentagemDevolucaoPrestacaoDeContasFEAS.ToString("P2");
        }

        void clearProgramasProjetos()
        {
            txtFEASPrevisaoInicialProgramasProjetos.Text = "0,00";
            txtFEASResultadoAppFinanceirasProgramasProjetos.Text = "0,00";
            txtFEASRecursosDisponibilizadosProgramasProjetos.Text = "0,00";
            txtFEASValoresExecutadosProgramasProjetos.Text = "0,00";
            txtFEASValoresReprogramadosProgramasProjetos.Text = "0,00";
            txtFEASValoresDevolvidosProgramasProjetos.Text = "0,00";
            txtFEASPorcentagensExecucaoProgramasProjetos.Text = "0,00";
        }

        void clearProgramasProjetosReprogramacao()
        {
            txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASResultadoAppFinanceirasProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASRecursosDisponibilizadosProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASValoresExecutadosProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASValoresReprogramadosProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASValoresDevolvidosProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASPorcentagensExecucaoProgramasProjetosReprogramacao.Text = "0,00";
        }


        void clearHDN()
        {
            hdnCofinanciamentoEstadualBasica.Value = String.Empty;
            hdnDemandasBasica.Value = String.Empty;
            hdnRecursosReprogramadosBasica.Value = String.Empty;
            hdnDemandasReprogramacaoBasica.Value = String.Empty;

            hdnCofinanciamentoEstadualMedia.Value = String.Empty;
            hdnDemandasMedia.Value = String.Empty;
            hdnRecursosReprogramadosMedia.Value = String.Empty;
            hdnDemandasReprogramacaoMedia.Value = String.Empty;
            
            hdnCofinanciamentoEstadualAlta.Value = String.Empty;
            hdnDemandasAlta.Value = String.Empty;
            hdnRecursosReprogramadosAlta.Value = String.Empty;
            hdnDemandasReprogramacaoAlta.Value = String.Empty;
            
            hdnCofinanciamentoEstadualBeneficiosEventuais.Value = String.Empty;
            hdnReprogramadosBeneficiosEventuais.Value = String.Empty;
            hdnDemandasBeneficiosEventuais.Value = String.Empty;
            hdnDemandasReprogramacaoBeneficiosEventuais.Value = String.Empty;

            hdnCofinanciamentoEstadualProgramasProjetos.Value = String.Empty;
            hdnRecursosReprogramadosProgramasProjetos.Value = String.Empty;
            
            hdnSomaCapacidadeMediaAtendimentoBasica.Value = String.Empty;
            hdnMediaMensalAtendimentoBasica.Value = String.Empty;
        }

        void carregaPrevisaoInicial(ExecucaoFinanceiraInfo b, ExecucaoFinanceiraInfo bR, ExecucaoFinanceiraInfo bD, ExecucaoFinanceiraInfo bRD, ExecucaoFinanceiraInfo m, ExecucaoFinanceiraInfo mR, ExecucaoFinanceiraInfo mD, ExecucaoFinanceiraInfo mRD, ExecucaoFinanceiraInfo a, ExecucaoFinanceiraInfo aR, ExecucaoFinanceiraInfo aD, ExecucaoFinanceiraInfo aRD, ExecucaoFinanceiraInfo bE, ExecucaoFinanceiraInfo beR, ExecucaoFinanceiraInfo bED, ExecucaoFinanceiraInfo bERD, ExecucaoFinanceiraInfo pp, ExecucaoFinanceiraInfo ppR)
        {


            if (Convert.ToInt32(hdfAno.Value) <= 2022)
            {
                
                    if (b != null)
                    {
                        if (b.Atualizado == true)
                        {

                            decimal totalBasica = Convert.ToDecimal(!String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasBasica.Value) ? hdnCofinanciamentoEstadualMaisDemandasBasica.Value : "0,00");

                            txtFEASPrevisaoInicialBasica.Text = totalBasica.ToString("N2");
                        }
                        else
                        {
                            txtFEASPrevisaoInicialBasica.Text = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualBasica.Value) ? hdnCofinanciamentoEstadualBasica.Value : "0,00";
                            b.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualBasica.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualBasica.Value) : 0M;
                        }
                    }


                    if (bR != null)
                    {
                        if (bR.Atualizado == true)
                        {
                            decimal totalBasicaRep = Convert.ToDecimal(!String.IsNullOrEmpty(hdnSomaDosReprogramadosBasica.Value) ? hdnSomaDosReprogramadosBasica.Value : "0,00");

                            txtReprogramacaoPrevisaoInicialBasica.Text = totalBasicaRep.ToString("N2");

                        }
                        else
                        {
                            txtReprogramacaoPrevisaoInicialBasica.Text = !String.IsNullOrEmpty(hdnRecursosReprogramadosBasica.Value) ? hdnRecursosReprogramadosBasica.Value : "0,00";
                            bR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnRecursosReprogramadosBasica.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosBasica.Value) : 0M;
                        }
                    }

                    if (bD != null)
                    {
                        if (bD.Atualizado == true)
                        {

                            decimal totalBasica = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasBasica.Value) ? hdnDemandasBasica.Value : "0,00");

                            txtFEASPrevisaoInicialBasicaDemandas.Text = totalBasica.ToString("N2");


                        }
                        else
                        {
                            txtFEASPrevisaoInicialBasicaDemandas.Text = !String.IsNullOrEmpty(hdnDemandasBasica.Value) ? hdnDemandasBasica.Value : "0,00";
                            bD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasBasica.Value) ? Convert.ToDecimal(hdnDemandasBasica.Value) : 0M;
                        }
                    }

                    if (bRD != null)
                    {
                        if (bRD.Atualizado == true)
                        {

                            decimal totalBasica = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasReprogramacaoBasica.Value) ? hdnDemandasReprogramacaoBasica.Value : "0,00");

                            txtFEASPrevisaoInicialBasicaReprogramacaoDemandas.Text = totalBasica.ToString("N2");


                        }
                        else
                        {
                            txtFEASPrevisaoInicialBasicaReprogramacaoDemandas.Text = !String.IsNullOrEmpty(hdnDemandasReprogramacaoBasica.Value) ? hdnDemandasReprogramacaoBasica.Value : "0,00";
                            bRD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasReprogramacaoBasica.Value) ? Convert.ToDecimal(hdnDemandasReprogramacaoBasica.Value) : 0M;
                        }
                    }


                    if (m != null)
                    {
                        if (m.Atualizado == true )
                        {
                            decimal totalMedia = Convert.ToDecimal(!String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasMedia.Value) ? hdnCofinanciamentoEstadualMaisDemandasMedia.Value : "0,00");
                            txtFEASPrevisaoInicialMedia.Text = totalMedia.ToString("N2");
                            m.PrevisaoInicialFEAS = Convert.ToDecimal(!String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasMedia.Value) ? hdnCofinanciamentoEstadualMaisDemandasMedia.Value : "0,00");
                        }
                        else
                        {
                            txtFEASPrevisaoInicialMedia.Text = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualMedia.Value) ? hdnCofinanciamentoEstadualMedia.Value : "0,00";
                        }
                    }



                    if (mR != null)
                    {
                        if (mR.Atualizado == true)
                        {
                            decimal totalMediaRep = Convert.ToDecimal(!String.IsNullOrEmpty(hdnSomaDosReprogramadosMedia.Value) ? hdnSomaDosReprogramadosMedia.Value : "0,00");
                            txtReprogramacaoPrevisaoInicialMedia.Text = totalMediaRep.ToString("N2");
                            mR.PrevisaoInicialFEAS = Convert.ToDecimal(!String.IsNullOrEmpty(hdnSomaDosReprogramadosMedia.Value) ? hdnSomaDosReprogramadosMedia.Value : "0,00");
                        }
                        else
                        {
                            txtReprogramacaoPrevisaoInicialMedia.Text = !String.IsNullOrEmpty(hdnRecursosReprogramadosMedia.Value) ? hdnRecursosReprogramadosMedia.Value : "0,00";
                            mR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnRecursosReprogramadosMedia.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosMedia.Value) : 0M;
                        }
                    }


                    if (mD != null)
                    {
                        if (mD.Atualizado == true)
                        {

                            decimal totalMedia = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasMedia.Value) ? hdnDemandasMedia.Value : "0,00");

                            txtFEASPrevisaoInicialMediaDemandas.Text = totalMedia.ToString("N2");


                        }
                        else
                        {
                            txtFEASPrevisaoInicialMediaDemandas.Text = !String.IsNullOrEmpty(hdnDemandasMedia.Value) ? hdnDemandasMedia.Value : "0,00";
                            mD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasMedia.Value) ? Convert.ToDecimal(hdnDemandasMedia.Value) : 0M;
                        }
                    }

                    if (mRD != null)
                    {
                        if (mRD.Atualizado == true)
                        {

                            decimal totalMedia = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasReprogramacaoMedia.Value) ? hdnDemandasReprogramacaoMedia.Value : "0,00");

                            txtFEASPrevisaoInicialMediaReprogramacaoDemandas.Text = totalMedia.ToString("N2");


                        }
                        else
                        {
                            txtFEASPrevisaoInicialMediaReprogramacaoDemandas.Text = !String.IsNullOrEmpty(hdnDemandasReprogramacaoMedia.Value) ? hdnDemandasReprogramacaoMedia.Value : "0,00";
                            mRD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasReprogramacaoMedia.Value) ? Convert.ToDecimal(hdnDemandasReprogramacaoMedia.Value) : 0M;
                        }
                    }

                    if (a != null)
                    {
                        if (a.Atualizado == true )
                        {
                            decimal totalAlta = Convert.ToDecimal(hdnCofinanciamentoEstadualMaisDemandasAlta.Value != "" ? hdnCofinanciamentoEstadualMaisDemandasAlta.Value : "0,00");

                            txtFEASPrevisaoInicialAlta.Text = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualAlta.Value) ? hdnCofinanciamentoEstadualAlta.Value : "0,00";  //totalAlta.ToString("N2");
                        }
                        else
                        {
                            txtFEASPrevisaoInicialAlta.Text = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualAlta.Value) ? hdnCofinanciamentoEstadualAlta.Value : "0,00";
                            a.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualAlta.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualAlta.Value) : 0M;
                        }
                    }



                    if (aR != null)
                    {
                        if (aR.Atualizado == true )
                        {
                            decimal totalAltaRep = Convert.ToDecimal(!String.IsNullOrEmpty(hdnSomaDosReprogramadosAlta.Value) ? hdnSomaDosReprogramadosAlta.Value : "0,00");

                            txtReprogramacaoPrevisaoInicialAlta.Text = totalAltaRep.ToString("N2");
                        }
                        else
                        {
                            txtReprogramacaoPrevisaoInicialAlta.Text = !String.IsNullOrEmpty(hdnRecursosReprogramadosAlta.Value) ? hdnRecursosReprogramadosAlta.Value : "0,00";
                            aR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnRecursosReprogramadosAlta.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosAlta.Value) : 0M;
                        }
                    }
                    

                    if (aD != null)
                    {
                        if (aD.Atualizado == true)
                        {
                            decimal totalAlta = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasAlta.Value) ? hdnDemandasAlta.Value : "0,00");

                            txtFEASPrevisaoInicialAltaDemandas.Text = totalAlta.ToString("N2");
                        }
                        else
                        {
                            txtFEASPrevisaoInicialAltaDemandas.Text = !String.IsNullOrEmpty(hdnDemandasAlta.Value) ? hdnDemandasAlta.Value : "0,00";
                            aD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasAlta.Value) ? Convert.ToDecimal(hdnDemandasAlta.Value) : 0M;
                        }
                    }

                    if (aRD != null)
                    {
                        if (aRD.Atualizado == true)
                        {

                            decimal totalAlta = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasReprogramacaoAlta.Value) ? hdnDemandasReprogramacaoAlta.Value : "0,00");

                            txtFEASPrevisaoInicialAltaReprogramacaoDemandas.Text = totalAlta.ToString("N2");


                        }
                        else
                        {
                            txtFEASPrevisaoInicialAltaReprogramacaoDemandas.Text = !String.IsNullOrEmpty(hdnDemandasReprogramacaoAlta.Value) ? hdnDemandasReprogramacaoAlta.Value : "0,00";
                            aRD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasReprogramacaoAlta.Value) ? Convert.ToDecimal(hdnDemandasReprogramacaoAlta.Value) : 0M;
                        }
                    }


                    if (bE != null)
                    {
                        if (bE.Atualizado == true)
                        {
                            decimal totalBeneficios = Convert.ToDecimal(!String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasBeneficiosEventuais.Value) ? hdnCofinanciamentoEstadualMaisDemandasBeneficiosEventuais.Value : "0,00");

                            txtFEASPrevisaoInicialBeneficiosEventuais.Text = totalBeneficios.ToString("N2");
                        }
                        else
                        {
                            txtFEASPrevisaoInicialBeneficiosEventuais.Text = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualBeneficiosEventuais.Value) ? hdnCofinanciamentoEstadualBeneficiosEventuais.Value : "0,00";
                            bE.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualBeneficiosEventuais.Value) : 0M;
                        }

                    }

                    if (beR != null)
                    {
                        if (beR.Atualizado == true)
                        {
                            txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text = beR.PrevisaoInicialFEAS.ToString("N2");
                        }
                        else
                        {
                            txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text = !String.IsNullOrEmpty(hdnReprogramadosBeneficiosEventuais.Value) ? hdnReprogramadosBeneficiosEventuais.Value : "0,00";
                            beR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnReprogramadosBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnReprogramadosBeneficiosEventuais.Value) : 0M;

                        }
                    }

                    if (bED != null)
                    {
                        if (bED.Atualizado == true)
                        {
                            decimal totalBeneficios = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasBeneficiosEventuais.Value) ? hdnDemandasBeneficiosEventuais.Value : "0,00");

                            txtFEASPrevisaoInicialBeneficiosEventuaisDemandas.Text = totalBeneficios.ToString("N2");
                        }
                        else
                        {
                            txtFEASPrevisaoInicialBeneficiosEventuaisDemandas.Text = !String.IsNullOrEmpty(hdnDemandasBeneficiosEventuais.Value) ? hdnDemandasBeneficiosEventuais.Value : "0,00";
                            bED.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnDemandasBeneficiosEventuais.Value) : 0M;
                        }
                    }

                    if (bERD != null)
                    {
                        if (bERD.Atualizado == true)
                        {
                            decimal totalBeneficios = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasReprogramacaoBeneficiosEventuais.Value) ? hdnDemandasReprogramacaoBeneficiosEventuais.Value : "0,00");

                            txtFEASPrevisaoInicialBeneficiosEventuaisDemandasReprogramacao.Text = totalBeneficios.ToString("N2");
                        }
                        else
                        {
                            txtFEASPrevisaoInicialBeneficiosEventuaisDemandasReprogramacao.Text = !String.IsNullOrEmpty(hdnDemandasReprogramacaoBeneficiosEventuais.Value) ? hdnDemandasReprogramacaoBeneficiosEventuais.Value : "0,00";
                            bERD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasReprogramacaoBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnDemandasReprogramacaoBeneficiosEventuais.Value) : 0M;
                        }
                    }


                    if (pp != null)
                    {
                        if (pp.Atualizado == true)
                        {
                            txtFEASPrevisaoInicialProgramasProjetos.Text = hdnCofinanciamentoEstadualProgramasProjetos.Value;//pp.PrevisaoInicialFEAS.ToString("N2");
                            pp.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualProgramasProjetos.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualProgramasProjetos.Value) : 0M;
                        }
                        else
                        {
                            txtFEASPrevisaoInicialProgramasProjetos.Text = hdnCofinanciamentoEstadualProgramasProjetos.Value;
                            pp.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualProgramasProjetos.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualProgramasProjetos.Value) : 0M;
                        }
                    }


                    if (ppR != null)
                    {
                        if (ppR.Atualizado == true)
                        {
                            txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Text = ppR.PrevisaoInicialFEAS.ToString("N2");
                        }
                        else
                        {
                            txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Text = hdnRecursosReprogramadosProgramasProjetos.Value;
                            ppR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnRecursosReprogramadosProgramasProjetos.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosProgramasProjetos.Value) : 0M;
                        }
                    }
            }
            else
            {
                if (b != null)
                {
                    if (b.Atualizado == true)
                    {

                        decimal totalBasica = Convert.ToDecimal(!String.IsNullOrEmpty(hdnCofinanciamentoEstadualBasica.Value) ? hdnCofinanciamentoEstadualBasica.Value : "0,00");

                        txtFEASPrevisaoInicialBasica.Text = totalBasica.ToString("N2");
                    }
                    else
                    {
                        txtFEASPrevisaoInicialBasica.Text = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualBasica.Value) ? hdnCofinanciamentoEstadualBasica.Value : "0,00";
                        b.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualBasica.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualBasica.Value) : 0M;
                    }
                }


                if (bR != null)
                {
                    if (bR.Atualizado == true)
                    {
                        decimal totalBasicaRep = Convert.ToDecimal(!String.IsNullOrEmpty(hdnRecursosReprogramadosBasica.Value) ? hdnRecursosReprogramadosBasica.Value : "0,00");

                        txtReprogramacaoPrevisaoInicialBasica.Text = totalBasicaRep.ToString("N2");

                    }
                    else
                    {
                        txtReprogramacaoPrevisaoInicialBasica.Text = !String.IsNullOrEmpty(hdnRecursosReprogramadosBasica.Value) ? hdnRecursosReprogramadosBasica.Value : "0,00";
                        bR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnRecursosReprogramadosBasica.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosBasica.Value) : 0M;
                    }
                }

                if (bD != null)
                {
                    if (bD.Atualizado == true)
                    {

                        decimal totalBasica = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasBasica.Value) ? hdnDemandasBasica.Value : "0,00");

                        txtFEASPrevisaoInicialBasicaDemandas.Text = totalBasica.ToString("N2");


                    }
                    else
                    {
                        txtFEASPrevisaoInicialBasicaDemandas.Text = !String.IsNullOrEmpty(hdnDemandasBasica.Value) ? hdnDemandasBasica.Value : "0,00";
                        bD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasBasica.Value) ? Convert.ToDecimal(hdnDemandasBasica.Value) : 0M;
                    }
                }

                if (bRD != null)
                {
                    if (bRD.Atualizado == true)
                    {

                        decimal totalBasica = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasReprogramacaoBasica.Value) ? hdnDemandasReprogramacaoBasica.Value : "0,00");

                        txtFEASPrevisaoInicialBasicaReprogramacaoDemandas.Text = totalBasica.ToString("N2");


                    }
                    else
                    {
                        txtFEASPrevisaoInicialBasicaReprogramacaoDemandas.Text = !String.IsNullOrEmpty(hdnDemandasReprogramacaoBasica.Value) ? hdnDemandasReprogramacaoBasica.Value : "0,00";
                        bRD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasReprogramacaoBasica.Value) ? Convert.ToDecimal(hdnDemandasReprogramacaoBasica.Value) : 0M;
                    }
                }


                if (m != null)
                {
                    if (m.Atualizado == true)
                    {
                        decimal totalMedia = Convert.ToDecimal(!String.IsNullOrEmpty(hdnCofinanciamentoEstadualMedia.Value) ? hdnCofinanciamentoEstadualMedia.Value : "0,00");
                        txtFEASPrevisaoInicialMedia.Text = totalMedia.ToString("N2");
                        m.PrevisaoInicialFEAS = totalMedia;
                    }
                    else
                    {
                        txtFEASPrevisaoInicialMedia.Text = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualMedia.Value) ? hdnCofinanciamentoEstadualMedia.Value : "0,00";
                    }
                }



                if (mR != null)
                {
                    if (mR.Atualizado == true)
                    {
                        decimal totalMediaRep = Convert.ToDecimal(!String.IsNullOrEmpty(hdnRecursosReprogramadosMedia.Value) ? hdnRecursosReprogramadosMedia.Value : "0,00");
                        txtReprogramacaoPrevisaoInicialMedia.Text = totalMediaRep.ToString("N2");
                        mR.PrevisaoInicialFEAS = totalMediaRep;
                    }
                    else
                    {
                        txtReprogramacaoPrevisaoInicialMedia.Text = !String.IsNullOrEmpty(hdnRecursosReprogramadosMedia.Value) ? hdnRecursosReprogramadosMedia.Value : "0,00";
                        mR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnRecursosReprogramadosMedia.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosMedia.Value) : 0M;
                    }
                }


                if (mD != null)
                {
                    if (mD.Atualizado == true)
                    {

                        decimal totalMedia = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasMedia.Value) ? hdnDemandasMedia.Value : "0,00");

                        txtFEASPrevisaoInicialMediaDemandas.Text = totalMedia.ToString("N2");


                    }
                    else
                    {
                        txtFEASPrevisaoInicialMediaDemandas.Text = !String.IsNullOrEmpty(hdnDemandasMedia.Value) ? hdnDemandasMedia.Value : "0,00";
                        mD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasMedia.Value) ? Convert.ToDecimal(hdnDemandasMedia.Value) : 0M;
                    }
                }

                if (mRD != null)
                {
                    if (mRD.Atualizado == true)
                    {

                        decimal totalMedia = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasReprogramacaoMedia.Value) ? hdnDemandasReprogramacaoMedia.Value : "0,00");

                        txtFEASPrevisaoInicialMediaReprogramacaoDemandas.Text = totalMedia.ToString("N2");


                    }
                    else
                    {
                        txtFEASPrevisaoInicialMediaReprogramacaoDemandas.Text = !String.IsNullOrEmpty(hdnDemandasReprogramacaoMedia.Value) ? hdnDemandasReprogramacaoMedia.Value : "0,00";
                        mRD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasReprogramacaoMedia.Value) ? Convert.ToDecimal(hdnDemandasReprogramacaoMedia.Value) : 0M;
                    }
                }

                if (a != null)
                {
                    if (a.Atualizado == true)
                    {
                        decimal totalAlta = Convert.ToDecimal(hdnCofinanciamentoEstadualAlta.Value != "" ? hdnCofinanciamentoEstadualAlta.Value : "0,00");

                        txtFEASPrevisaoInicialAlta.Text = totalAlta.ToString("N2");
                    }
                    else
                    {
                        txtFEASPrevisaoInicialAlta.Text = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualAlta.Value) ? hdnCofinanciamentoEstadualAlta.Value : "0,00";
                        a.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualAlta.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualAlta.Value) : 0M;
                    }
                }



                if (aR != null)
                {
                    if (aR.Atualizado == true)
                    {
                        decimal totalAltaRep = Convert.ToDecimal(!String.IsNullOrEmpty(hdnRecursosReprogramadosAlta.Value) ? hdnRecursosReprogramadosAlta.Value : "0,00");

                        txtReprogramacaoPrevisaoInicialAlta.Text = totalAltaRep.ToString("N2");
                    }
                    else
                    {
                        txtReprogramacaoPrevisaoInicialAlta.Text = !String.IsNullOrEmpty(hdnRecursosReprogramadosAlta.Value) ? hdnRecursosReprogramadosAlta.Value : "0,00";
                        aR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnRecursosReprogramadosAlta.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosAlta.Value) : 0M;
                    }
                }


                if (aD != null)
                {
                    if (aD.Atualizado == true)
                    {
                        decimal totalAlta = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasAlta.Value) ? hdnDemandasAlta.Value : "0,00");

                        txtFEASPrevisaoInicialAltaDemandas.Text = totalAlta.ToString("N2");
                    }
                    else
                    {
                        txtFEASPrevisaoInicialAltaDemandas.Text = !String.IsNullOrEmpty(hdnDemandasAlta.Value) ? hdnDemandasAlta.Value : "0,00";
                        aD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasAlta.Value) ? Convert.ToDecimal(hdnDemandasAlta.Value) : 0M;
                    }
                }

                if (aRD != null)
                {
                    if (aRD.Atualizado == true)
                    {

                        decimal totalAlta = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasReprogramacaoAlta.Value) ? hdnDemandasReprogramacaoAlta.Value : "0,00");

                        txtFEASPrevisaoInicialAltaReprogramacaoDemandas.Text = totalAlta.ToString("N2");


                    }
                    else
                    {
                        txtFEASPrevisaoInicialAltaReprogramacaoDemandas.Text = !String.IsNullOrEmpty(hdnDemandasReprogramacaoAlta.Value) ? hdnDemandasReprogramacaoAlta.Value : "0,00";
                        aRD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasReprogramacaoAlta.Value) ? Convert.ToDecimal(hdnDemandasReprogramacaoAlta.Value) : 0M;
                    }
                }


                if (bE != null)
                {
                    if (bE.Atualizado == true)
                    {
                        decimal totalBeneficios = Convert.ToDecimal(!String.IsNullOrEmpty(hdnCofinanciamentoEstadualBeneficiosEventuais.Value) ? hdnCofinanciamentoEstadualBeneficiosEventuais.Value : "0,00");

                        txtFEASPrevisaoInicialBeneficiosEventuais.Text = totalBeneficios.ToString("N2");
                    }
                    else
                    {
                        txtFEASPrevisaoInicialBeneficiosEventuais.Text = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualBeneficiosEventuais.Value) ? hdnCofinanciamentoEstadualBeneficiosEventuais.Value : "0,00";
                        bE.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualBeneficiosEventuais.Value) : 0M;
                    }

                }

                if (beR != null)
                {
                    if (beR.Atualizado == true)
                    {
                        //txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text = beR.PrevisaoInicialFEAS.ToString("N2");
                        txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text = !String.IsNullOrEmpty(hdnReprogramadosBeneficiosEventuais.Value) ? hdnReprogramadosBeneficiosEventuais.Value : "0,00";
                        beR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnReprogramadosBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnReprogramadosBeneficiosEventuais.Value) : 0M;
                    }
                    else
                    {
                        txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text = !String.IsNullOrEmpty(hdnReprogramadosBeneficiosEventuais.Value) ? hdnReprogramadosBeneficiosEventuais.Value : "0,00";
                        beR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnReprogramadosBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnReprogramadosBeneficiosEventuais.Value) : 0M;

                    }
                }

                if (bED != null)
                {
                    if (bED.Atualizado == true)
                    {
                        decimal totalBeneficios = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasBeneficiosEventuais.Value) ? hdnDemandasBeneficiosEventuais.Value : "0,00");

                        txtFEASPrevisaoInicialBeneficiosEventuaisDemandas.Text = totalBeneficios.ToString("N2");
                    }
                    else
                    {
                        txtFEASPrevisaoInicialBeneficiosEventuaisDemandas.Text = !String.IsNullOrEmpty(hdnDemandasBeneficiosEventuais.Value) ? hdnDemandasBeneficiosEventuais.Value : "0,00";
                        bED.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnDemandasBeneficiosEventuais.Value) : 0M;
                    }
                }

                if (bERD != null)
                {
                    if (bERD.Atualizado == true)
                    {
                        decimal totalBeneficios = Convert.ToDecimal(!String.IsNullOrEmpty(hdnDemandasReprogramacaoBeneficiosEventuais.Value) ? hdnDemandasReprogramacaoBeneficiosEventuais.Value : "0,00");

                        txtFEASPrevisaoInicialBeneficiosEventuaisDemandasReprogramacao.Text = totalBeneficios.ToString("N2");
                    }
                    else
                    {
                        txtFEASPrevisaoInicialBeneficiosEventuaisDemandasReprogramacao.Text = !String.IsNullOrEmpty(hdnDemandasReprogramacaoBeneficiosEventuais.Value) ? hdnDemandasReprogramacaoBeneficiosEventuais.Value : "0,00";
                        bERD.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnDemandasReprogramacaoBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnDemandasReprogramacaoBeneficiosEventuais.Value) : 0M;
                    }
                }


                if (pp != null)
                {
                    if (pp.Atualizado == true)
                    {
                        txtFEASPrevisaoInicialProgramasProjetos.Text = hdnCofinanciamentoEstadualProgramasProjetos.Value;//pp.PrevisaoInicialFEAS.ToString("N2");
                        pp.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualProgramasProjetos.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualProgramasProjetos.Value) : 0M;
                    }
                    else
                    {
                        txtFEASPrevisaoInicialProgramasProjetos.Text = hdnCofinanciamentoEstadualProgramasProjetos.Value;
                        pp.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualProgramasProjetos.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualProgramasProjetos.Value) : 0M;
                    }
                }


                if (ppR != null)
                {
                    if (ppR.Atualizado == true)
                    {
                        //txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Text = ppR.PrevisaoInicialFEAS.ToString("N2");
                        txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Text = hdnRecursosReprogramadosProgramasProjetos.Value;
                        ppR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnRecursosReprogramadosProgramasProjetos.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosProgramasProjetos.Value) : 0M;
                    }
                    else
                    {
                        txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Text = hdnRecursosReprogramadosProgramasProjetos.Value;
                        ppR.PrevisaoInicialFEAS = !String.IsNullOrEmpty(hdnRecursosReprogramadosProgramasProjetos.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosProgramasProjetos.Value) : 0M;
                    }
                }
            }


        }

        void carregaPrevisaoInicialDemonstrativo()
        {

            decimal basica = 0;
            decimal basicaReprogramado = 0;
            decimal basicaDemandas = 0;
            decimal basicaDemandasReprogramacao = 0;
            decimal media = 0;
            decimal mediaReprogramado = 0;
            decimal mediaDemandas = 0;
            decimal mediaDemandasReprogramacao = 0;
            decimal alta = 0;
            decimal altaReprogramado = 0;
            decimal altaDemandas = 0;
            decimal altaDemandasReprogramacao = 0;
            decimal bEventuais = 0;
            decimal bEventuaisReprogramado = 0;
            decimal bEventuaisDemandas = 0;
            decimal bEventuaisDemandasReprogramacao = 0;
            decimal programasProjetos = 0;
            decimal programasProjetosReprogramacao = 0;


            basica = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasBasica.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualMaisDemandasBasica.Value) : 0M;
            txtFEASPrevisaoInicialBasica.Text = basica.ToString("N2");


            basicaReprogramado = !String.IsNullOrEmpty(hdnSomaDosReprogramadosBasica.Value) ? Convert.ToDecimal(hdnSomaDosReprogramadosBasica.Value) : 0M;
            txtReprogramacaoPrevisaoInicialBasica.Text = basicaReprogramado.ToString("N2");


            media = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasMedia.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualMaisDemandasMedia.Value) : 0M;
            txtFEASPrevisaoInicialMedia.Text = media.ToString("N2");


            mediaReprogramado = !String.IsNullOrEmpty(hdnSomaDosReprogramadosMedia.Value) ? Convert.ToDecimal(hdnSomaDosReprogramadosMedia.Value) : 0M;
            txtReprogramacaoPrevisaoInicialMedia.Text = mediaReprogramado.ToString("N2");


            alta = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasAlta.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualMaisDemandasAlta.Value) : 0M;
            txtFEASPrevisaoInicialAlta.Text = alta.ToString("N2");

            altaReprogramado = !String.IsNullOrEmpty(hdnSomaDosReprogramadosAlta.Value) ? Convert.ToDecimal(hdnSomaDosReprogramadosAlta.Value) : 0M;
            txtReprogramacaoPrevisaoInicialAlta.Text = altaReprogramado.ToString("N2");

            bEventuais = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualMaisDemandasBeneficiosEventuais.Value) : 0M;
            txtFEASPrevisaoInicialBeneficiosEventuais.Text = bEventuais.ToString("N2");


            bEventuaisReprogramado = !String.IsNullOrEmpty(hdnReprogramadosBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnReprogramadosBeneficiosEventuais.Value) : 0M;
            txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text = bEventuaisReprogramado.ToString("N2");


            programasProjetos = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualProgramasProjetos.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualProgramasProjetos.Value) : 0M;
            txtFEASPrevisaoInicialProgramasProjetos.Text = programasProjetos.ToString("N2");


            programasProjetosReprogramacao = !String.IsNullOrEmpty(hdnRecursosReprogramadosProgramasProjetos.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosProgramasProjetos.Value) : 0M;
            txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Text = programasProjetos.ToString("N2");


            lblFEASPrevisaoInicial.Text = (basica + basicaReprogramado + media + mediaReprogramado + alta + altaReprogramado + bEventuais + bEventuaisReprogramado + programasProjetos + programasProjetosReprogramacao).ToString("N2");



        }

        void totalizar(ExecucaoFinanceiraInfo basica, ExecucaoFinanceiraInfo reprogramacaoBasica, ExecucaoFinanceiraInfo basicaDemandas, ExecucaoFinanceiraInfo basicaReprogramacaoDemandas, ExecucaoFinanceiraInfo reprogramado, ExecucaoFinanceiraInfo especialMedia, ExecucaoFinanceiraInfo especialReprogramacaoMedia, ExecucaoFinanceiraInfo mediaDemandas, ExecucaoFinanceiraInfo mediaReprogramacaoDemandas, ExecucaoFinanceiraInfo especialAlta, ExecucaoFinanceiraInfo especialReprogramacaoAlta, ExecucaoFinanceiraInfo altaDemandas, ExecucaoFinanceiraInfo altaDemandasReprogramacao, ExecucaoFinanceiraInfo beneficiosEventuais, ExecucaoFinanceiraInfo reprogramacaoBeneficiosEventuais, ExecucaoFinanceiraInfo beneficiosEventuaisDemandas, ExecucaoFinanceiraInfo beneficiosEventuaisReprogramacaoDemandas, ExecucaoFinanceiraInfo programaProjeto, ExecucaoFinanceiraInfo programaProjetoReprogramacao)
        {


            decimal CofinanciamentoEstadualMaisDemandasBasica = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasBasica.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualMaisDemandasBasica.Value) : 0M;
            decimal CofinanciamentoEstadualMaisDemandasMedia = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasMedia.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualMaisDemandasMedia.Value) : 0M;
            decimal CofinanciamentoEstadualMaisDemandasAlta = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasAlta.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualMaisDemandasAlta.Value) : 0M;
            decimal CofinanciamentoEstadualMaisDemandasBeneficiosEventuais = !String.IsNullOrEmpty(hdnCofinanciamentoEstadualMaisDemandasBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnCofinanciamentoEstadualMaisDemandasBeneficiosEventuais.Value) : 0M;
            decimal reprogramacaohdnBasica = !String.IsNullOrEmpty(hdnRecursosReprogramadosBasica.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosBasica.Value) : 0M;
            decimal reprogramacaohdnMedia = !String.IsNullOrEmpty(hdnRecursosReprogramadosMedia.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosMedia.Value) : 0M;
            decimal reprogramacaohdnAlta = !String.IsNullOrEmpty(hdnRecursosReprogramadosAlta.Value) ? Convert.ToDecimal(hdnRecursosReprogramadosAlta.Value) : 0M;
            decimal reprogramacaohdnBeneficiosEventuais = !String.IsNullOrEmpty(hdnReprogramadosBeneficiosEventuais.Value) ? Convert.ToDecimal(hdnReprogramadosBeneficiosEventuais.Value) : 0M;



            if (basica != null && reprogramacaoBasica != null && basicaDemandas != null && basicaReprogramacaoDemandas != null && especialMedia != null && especialReprogramacaoMedia != null && mediaDemandas != null && mediaReprogramacaoDemandas != null && especialAlta != null && especialReprogramacaoAlta != null && altaDemandas != null && altaDemandasReprogramacao != null && beneficiosEventuais != null && reprogramacaoBeneficiosEventuais != null && beneficiosEventuaisDemandas != null && beneficiosEventuaisReprogramacaoDemandas != null && programaProjeto != null && programaProjetoReprogramacao != null)
            {
                lblFEASPrevisaoInicial.Text = (CofinanciamentoEstadualMaisDemandasBasica +
                                               reprogramacaohdnBasica +
                                               basicaReprogramacaoDemandas.PrevisaoInicialFEAS + 
                                               
                                               CofinanciamentoEstadualMaisDemandasMedia  +
                                               reprogramacaohdnMedia +
                                               mediaReprogramacaoDemandas.PrevisaoInicialFEAS + 
                                               
                                               CofinanciamentoEstadualMaisDemandasAlta +
                                               reprogramacaohdnAlta +
                                               altaDemandasReprogramacao.PrevisaoInicialFEAS +                                                
                                               
                                               CofinanciamentoEstadualMaisDemandasBeneficiosEventuais +
                                               reprogramacaohdnBeneficiosEventuais +
                                               beneficiosEventuaisReprogramacaoDemandas.PrevisaoInicialFEAS + 
                                               
                                               programaProjeto.PrevisaoInicialFEAS + 
                                               programaProjetoReprogramacao.PrevisaoInicialFEAS).ToString("N2");

            }


            var recursosDisponibilizados = (basica.RecursoDisponibilizadoFEAS != null ? basica.RecursoDisponibilizadoFEAS : 0) + 
                                           (basicaDemandas.RecursoDisponibilizadoFEAS != null ? basicaDemandas.RecursoDisponibilizadoFEAS : 0) + 
                                           (especialMedia.RecursoDisponibilizadoFEAS != null ? especialMedia.RecursoDisponibilizadoFEAS : 0) + 
                                           (mediaDemandas.RecursoDisponibilizadoFEAS != null ? mediaDemandas.RecursoDisponibilizadoFEAS : 0) + 
                                           (especialAlta.RecursoDisponibilizadoFEAS != null ? especialAlta.RecursoDisponibilizadoFEAS : 0) + 
                                           (altaDemandas.RecursoDisponibilizadoFEAS != null ? altaDemandas.RecursoDisponibilizadoFEAS : 0) + 
                                           (beneficiosEventuais.RecursoDisponibilizadoFEAS != null ? beneficiosEventuais.RecursoDisponibilizadoFEAS : 0) + 
                                           (beneficiosEventuaisDemandas.RecursoDisponibilizadoFEAS != null ? beneficiosEventuaisDemandas.RecursoDisponibilizadoFEAS : 0) +
                                           (programaProjeto.RecursoDisponibilizadoFEAS != null ? programaProjeto.RecursoDisponibilizadoFEAS : 0) + 
                                           (reprogramacaoBasica.RecursoDisponibilizadoFEAS != null ? reprogramacaoBasica.RecursoDisponibilizadoFEAS : 0) + 
                                           (basicaReprogramacaoDemandas.RecursoDisponibilizadoFEAS != null ? basicaReprogramacaoDemandas.RecursoDisponibilizadoFEAS : 0) + 
                                           (especialReprogramacaoMedia.RecursoDisponibilizadoFEAS != null ? especialReprogramacaoMedia.RecursoDisponibilizadoFEAS : 0) + 
                                           (mediaReprogramacaoDemandas.RecursoDisponibilizadoFEAS != null ? mediaReprogramacaoDemandas.RecursoDisponibilizadoFEAS : 0) + 
                                           (especialReprogramacaoAlta.RecursoDisponibilizadoFEAS != null ? especialReprogramacaoAlta.RecursoDisponibilizadoFEAS : 0) + 
                                           (altaDemandasReprogramacao.RecursoDisponibilizadoFEAS != null ? altaDemandasReprogramacao.RecursoDisponibilizadoFEAS : 0) +
                                           (reprogramacaoBeneficiosEventuais.RecursoDisponibilizadoFEAS != null ? reprogramacaoBeneficiosEventuais.RecursoDisponibilizadoFEAS : 0) + 
                                           (beneficiosEventuaisReprogramacaoDemandas.RecursoDisponibilizadoFEAS != null ? beneficiosEventuaisReprogramacaoDemandas.RecursoDisponibilizadoFEAS : 0) + 
                                           (programaProjetoReprogramacao.RecursoDisponibilizadoFEAS != null ? programaProjetoReprogramacao.RecursoDisponibilizadoFEAS : 0);

            lblFEASRecursosDisponibilizados.Text = recursosDisponibilizados.ToString("N2");


            var resultadoAppFinanceira = (basica.ResultadoAplicacaoFinanceiraFEAS != null ? basica.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (basicaDemandas.ResultadoAplicacaoFinanceiraFEAS != null ? basicaDemandas.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (especialMedia.ResultadoAplicacaoFinanceiraFEAS != null ? especialMedia.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (mediaDemandas.ResultadoAplicacaoFinanceiraFEAS != null ? mediaDemandas.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (especialAlta.ResultadoAplicacaoFinanceiraFEAS != null ? especialAlta.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (altaDemandas.ResultadoAplicacaoFinanceiraFEAS != null ? altaDemandas.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (beneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS != null ? beneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (beneficiosEventuaisDemandas.ResultadoAplicacaoFinanceiraFEAS != null ? beneficiosEventuaisDemandas.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (programaProjeto.ResultadoAplicacaoFinanceiraFEAS != null ? programaProjeto.ResultadoAplicacaoFinanceiraFEAS : 0) +
                                         (reprogramacaoBasica.ResultadoAplicacaoFinanceiraFEAS != null ? reprogramacaoBasica.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (basicaReprogramacaoDemandas.ResultadoAplicacaoFinanceiraFEAS != null ? basicaReprogramacaoDemandas.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (especialReprogramacaoMedia.ResultadoAplicacaoFinanceiraFEAS != null ? especialReprogramacaoMedia.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (mediaReprogramacaoDemandas.ResultadoAplicacaoFinanceiraFEAS != null ? mediaReprogramacaoDemandas.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (especialReprogramacaoAlta.ResultadoAplicacaoFinanceiraFEAS != null ? especialReprogramacaoAlta.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (altaDemandasReprogramacao.ResultadoAplicacaoFinanceiraFEAS != null ? altaDemandasReprogramacao.ResultadoAplicacaoFinanceiraFEAS : 0) + 
                                         (reprogramacaoBeneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS != null ? reprogramacaoBeneficiosEventuais.ResultadoAplicacaoFinanceiraFEAS : 0) +
                                         (beneficiosEventuaisReprogramacaoDemandas.ResultadoAplicacaoFinanceiraFEAS != null ? beneficiosEventuaisReprogramacaoDemandas.ResultadoAplicacaoFinanceiraFEAS : 0) +
                                         (programaProjetoReprogramacao.ResultadoAplicacaoFinanceiraFEAS != null ? programaProjetoReprogramacao.ResultadoAplicacaoFinanceiraFEAS : 0);

            lblFEASResultadoAppFinanceiras.Text = resultadoAppFinanceira.ToString("N2");


            var valoresExecutados = (basica.ValoresExecutadosFEAS != null ? basica.ValoresExecutadosFEAS : 0) +
                                    (basicaDemandas.ValoresExecutadosFEAS != null ? basicaDemandas.ValoresExecutadosFEAS : 0) + 
                                    (especialMedia.ValoresExecutadosFEAS != null ? especialMedia.ValoresExecutadosFEAS : 0) +
                                    (mediaDemandas.ValoresExecutadosFEAS != null ? mediaDemandas.ValoresExecutadosFEAS : 0) + 
                                    (especialAlta.ValoresExecutadosFEAS != null ? especialAlta.ValoresExecutadosFEAS : 0) +
                                    (altaDemandas.ValoresExecutadosFEAS != null ? altaDemandas.ValoresExecutadosFEAS : 0) +
                                    (beneficiosEventuais.ValoresExecutadosFEAS != null ? beneficiosEventuais.ValoresExecutadosFEAS : 0) +
                                    (beneficiosEventuaisDemandas.ValoresExecutadosFEAS != null ? beneficiosEventuaisDemandas.ValoresExecutadosFEAS : 0) + 
                                    (programaProjeto.ValoresExecutadosFEAS != null ? programaProjeto.ValoresExecutadosFEAS : 0) + 
                                    (reprogramacaoBasica.ValoresExecutadosFEAS != null ? reprogramacaoBasica.ValoresExecutadosFEAS : 0) +
                                    (basicaReprogramacaoDemandas.ValoresExecutadosFEAS != null ? basicaReprogramacaoDemandas.ValoresExecutadosFEAS : 0) + 
                                    (especialReprogramacaoMedia.ValoresExecutadosFEAS != null ? especialReprogramacaoMedia.ValoresExecutadosFEAS : 0) +
                                    (mediaReprogramacaoDemandas.ValoresExecutadosFEAS != null ? mediaReprogramacaoDemandas.ValoresExecutadosFEAS : 0) + 
                                    (especialReprogramacaoAlta.ValoresExecutadosFEAS != null ? especialReprogramacaoAlta.ValoresExecutadosFEAS : 0) +
                                    (altaDemandasReprogramacao.ValoresExecutadosFEAS != null ? altaDemandasReprogramacao.ValoresExecutadosFEAS : 0) + 
                                    (reprogramacaoBeneficiosEventuais.ValoresExecutadosFEAS != null ? reprogramacaoBeneficiosEventuais.ValoresExecutadosFEAS : 0) +
                                    (beneficiosEventuaisReprogramacaoDemandas.ValoresExecutadosFEAS != null ? beneficiosEventuaisReprogramacaoDemandas.ValoresExecutadosFEAS : 0) + 
                                    (programaProjetoReprogramacao.ValoresExecutadosFEAS != null ? programaProjetoReprogramacao.ValoresExecutadosFEAS : 0);

            lblFEASValoresExecutados.Text = valoresExecutados.ToString("N2");



            var valoresReprogramados = (basica.ValoresReprogramadosFEAS != null ? basica.ValoresReprogramadosFEAS : 0) +
                                       (basicaDemandas.ValoresReprogramadosFEAS != null ? basicaDemandas.ValoresReprogramadosFEAS : 0) +
                                       (especialMedia.ValoresReprogramadosFEAS != null ? especialMedia.ValoresReprogramadosFEAS : 0) +
                                       (mediaDemandas.ValoresReprogramadosFEAS != null ? mediaDemandas.ValoresReprogramadosFEAS : 0) +
                                       (especialAlta.ValoresReprogramadosFEAS != null ? especialAlta.ValoresReprogramadosFEAS : 0) +
                                       (altaDemandas.ValoresReprogramadosFEAS != null ? altaDemandas.ValoresReprogramadosFEAS : 0) +
                                       (beneficiosEventuais.ValoresReprogramadosFEAS != null ? beneficiosEventuais.ValoresReprogramadosFEAS : 0) +
                                       (beneficiosEventuaisDemandas.ValoresReprogramadosFEAS != null ? beneficiosEventuaisDemandas.ValoresReprogramadosFEAS : 0) + 
                                       (programaProjeto.ValoresReprogramadosFEAS != null ? programaProjeto.ValoresReprogramadosFEAS : 0) + 
                                       (reprogramacaoBasica.ValoresReprogramadosFEAS != null ? reprogramacaoBasica.ValoresReprogramadosFEAS : 0) +
                                       (basicaReprogramacaoDemandas.ValoresReprogramadosFEAS != null ? basicaReprogramacaoDemandas.ValoresReprogramadosFEAS : 0) +
                                       (especialReprogramacaoMedia.ValoresReprogramadosFEAS != null ? especialReprogramacaoMedia.ValoresReprogramadosFEAS : 0) +
                                       (mediaReprogramacaoDemandas.ValoresReprogramadosFEAS != null ? mediaReprogramacaoDemandas.ValoresReprogramadosFEAS : 0) +
                                       (especialReprogramacaoAlta.ValoresReprogramadosFEAS != null ? especialReprogramacaoAlta.ValoresReprogramadosFEAS : 0) +
                                       (altaDemandasReprogramacao.ValoresReprogramadosFEAS != null ? altaDemandasReprogramacao.ValoresReprogramadosFEAS : 0) +
                                       (reprogramacaoBeneficiosEventuais.ValoresReprogramadosFEAS != null ? reprogramacaoBeneficiosEventuais.ValoresReprogramadosFEAS : 0) +
                                       (beneficiosEventuaisReprogramacaoDemandas.ValoresReprogramadosFEAS != null ? beneficiosEventuaisReprogramacaoDemandas.ValoresReprogramadosFEAS : 0) + 
                                       (programaProjetoReprogramacao.ValoresReprogramadosFEAS != null ? programaProjetoReprogramacao.ValoresReprogramadosFEAS : 0);

            lblFEASValoresReprogramados.Text = valoresReprogramados.ToString("N2");


            var valoresDevolvidos = (basica.ValoresDevolvidosFEAS != null ? basica.ValoresDevolvidosFEAS : 0) +
                                    (basicaDemandas.ValoresDevolvidosFEAS != null ? basicaDemandas.ValoresDevolvidosFEAS : 0) + 
                                    (especialMedia.ValoresDevolvidosFEAS != null ? especialMedia.ValoresDevolvidosFEAS : 0) +
                                    (mediaDemandas.ValoresDevolvidosFEAS != null ? mediaDemandas.ValoresDevolvidosFEAS : 0) + 
                                    (especialAlta.ValoresDevolvidosFEAS != null ? especialAlta.ValoresDevolvidosFEAS : 0) +
                                    (altaDemandas.ValoresDevolvidosFEAS != null ? altaDemandas.ValoresDevolvidosFEAS : 0) + 
                                    (beneficiosEventuais.ValoresDevolvidosFEAS != null ? beneficiosEventuais.ValoresDevolvidosFEAS : 0) +
                                    (beneficiosEventuaisDemandas.ValoresDevolvidosFEAS != null ? beneficiosEventuaisDemandas.ValoresDevolvidosFEAS : 0) + 
                                    (programaProjeto.ValoresDevolvidosFEAS != null ? programaProjeto.ValoresDevolvidosFEAS : 0) + 
                                    (reprogramacaoBasica.ValoresDevolvidosFEAS != null ? reprogramacaoBasica.ValoresDevolvidosFEAS : 0) +
                                    (basicaReprogramacaoDemandas.ValoresDevolvidosFEAS != null ? basicaReprogramacaoDemandas.ValoresDevolvidosFEAS : 0) + 
                                    (especialReprogramacaoMedia.ValoresDevolvidosFEAS != null ? especialReprogramacaoMedia.ValoresDevolvidosFEAS : 0) +
                                    (mediaReprogramacaoDemandas.ValoresDevolvidosFEAS != null ? mediaReprogramacaoDemandas.ValoresDevolvidosFEAS : 0) + 
                                    (especialReprogramacaoAlta.ValoresDevolvidosFEAS != null ? especialReprogramacaoAlta.ValoresDevolvidosFEAS : 0) +
                                    (altaDemandasReprogramacao.ValoresDevolvidosFEAS != null ? altaDemandasReprogramacao.ValoresDevolvidosFEAS : 0) + 
                                    (reprogramacaoBeneficiosEventuais.ValoresDevolvidosFEAS != null ? reprogramacaoBeneficiosEventuais.ValoresDevolvidosFEAS : 0) +
                                    (beneficiosEventuaisReprogramacaoDemandas.ValoresDevolvidosFEAS != null ? beneficiosEventuaisReprogramacaoDemandas.ValoresDevolvidosFEAS : 0) + 
                                    (programaProjetoReprogramacao.ValoresDevolvidosFEAS != null ? programaProjetoReprogramacao.ValoresDevolvidosFEAS : 0);

            lblFEASValoresDevolvidos.Text = valoresDevolvidos.ToString("N2");



            var porcentagens = (valoresExecutados != 0 ? valoresExecutados : 0) / (recursosDisponibilizados != 0 || resultadoAppFinanceira != 0 ? recursosDisponibilizados + resultadoAppFinanceira : 1);

            lblFEASPorcentagensExecucao.Text = porcentagens.ToString("P2");
        }

        void clearTotalizar()
        {
            lblFEASPrevisaoInicial.Text = "0,00";
            lblFEASRecursosDisponibilizados.Text = "0,00";
            lblFEASResultadoAppFinanceiras.Text = "0,00";
            lblFEASValoresExecutados.Text = "0,00";
            lblFEASValoresReprogramados.Text = "0,00";
            lblFEASValoresDevolvidos.Text = "0,00";
            lblFEASPorcentagensExecucao.Text = "0,00";
        }

        void carregarComentarioPrestacaoDeContas(ComentarioPrestacaoDeContasInfo c)
        {
            if (c != null)
            {
                if (!String.IsNullOrEmpty(c.Comentario))
                {
                    txtComentario.Text = c.Comentario.ToString();
                }
                else
                {
                    txtComentario.Text = "";
                }
            }
            else
            {
                txtComentario.Text = "";
            }

        }

        void carregarQuestionarioCMAS(QuestoesCMASinfo q)
        {

            if (q.QuestaoUm != null)
            {
                rblQuestaoUmCMAS.SelectedValue = q.QuestaoUm.HasValue ? Convert.ToString(q.QuestaoUm.Value) : "";
            }

            if (q.QuestaoDois != null)
            {
                rblQuestaoDoisCMAS.SelectedValue = q.QuestaoDois.HasValue ? Convert.ToString(q.QuestaoDois.Value) : "";
            }

            if (q.QuestaoTres != null)
            {
                rblQuestaoTresCMAS.SelectedValue = q.QuestaoTres.HasValue ? Convert.ToString(q.QuestaoTres.Value) : "";
            }

            if (q.QuestaoQuatro != null)
            {
                rblQuestaoQuatroCMAS.SelectedValue = q.QuestaoQuatro.HasValue ? Convert.ToString(q.QuestaoQuatro.Value) : "";
            }


            if (q.QuestaoCinco != null)
            {
                rblQuestaoCincoCMAS.SelectedValue = q.QuestaoCinco.HasValue ? Convert.ToString(q.QuestaoCinco.Value) : "";
            }


            if (q.QuestaoSeis != null)
            {
                rblQuestaoSeisCMAS.SelectedValue = q.QuestaoSeis.HasValue ? Convert.ToString(q.QuestaoSeis.Value) : "";
            }

            if (q.QuestaoSeis != null)
            {
                if (q.QuestaoSeis == 2)
                {
                    if (!String.IsNullOrEmpty(q.QuestaoSeisEscrita))
                    {
                        controleQuestaoSeisCMAS(true);

                        txtQuestaoSeisCMAS.Text = q.QuestaoSeisEscrita.ToString();
                    }
                    else
                    {
                        controleQuestaoSeisCMAS(true);
                    }
                }
                else
                {
                    controleQuestaoSeisCMAS(false);
                }
            }

            if (q.QuestaoSete != null)
            {
                rblQuestaoSeteCMAS.SelectedValue = q.QuestaoSete.ToString();
            }

            if (q.QuestaoSete != null)
            {
                if (q.QuestaoSete == 1)
                {
                    if (!String.IsNullOrEmpty(q.QuestaoSeteEscrita))
                    {
                        controleQuestaoSeteCMAS(true);

                        txtQuestaoSeteCMAS.Text = q.QuestaoSeteEscrita.ToString();
                    }
                    else
                    {
                        controleQuestaoSeteCMAS(true);
                    }
                }
                else
                {
                    controleQuestaoSeteCMAS(false);

                }
            }

            if (q.QuestaoOito != null)
            {
                rblQuestaoOitoCMAS.SelectedValue = q.QuestaoOito.ToString();
            }

            if (q.QuestaoNove != null)
            {
                rblQuestaoNoveCMAS.SelectedValue = q.QuestaoNove.ToString();
            }
        }

        void zerarQuestionarioCMAS()
        {
            rblQuestaoUmCMAS.SelectedValue = null;
            rblQuestaoDoisCMAS.SelectedValue = null;
            rblQuestaoTresCMAS.SelectedValue = null;
            rblQuestaoQuatroCMAS.SelectedValue = null;
            rblQuestaoCincoCMAS.SelectedValue = null;
            rblQuestaoSeisCMAS.SelectedValue = null;
            rblQuestaoSeteCMAS.SelectedValue = null;
            rblQuestaoOitoCMAS.SelectedValue = null;
            rblQuestaoNoveCMAS.SelectedValue = null;
            controleQuestaoSeisCMAS(false);
            controleQuestaoSeteCMAS(false);
        }


        void carregarQuestionarioDrads(QuestoesDRADSInfo d)
        {
            if (d.QuestaoUm != null)
            {
                rblQuestaoUmDRADS.SelectedValue = d.QuestaoUm.ToString();

                if (d.QuestaoUm == 1)
                {
                    if (!String.IsNullOrEmpty(d.QuestaoUmEscrita))
                    {
                        controleQuestaoUmDRADS(true);

                        txtQuestaoUmDRADS.Text = d.QuestaoUmEscrita.ToString();
                    }
                    else
                    {
                        controleQuestaoUmDRADS(false);
                    }
                }
                else
                {
                    controleQuestaoUmDRADS(false);
                }
            }

            if (d.QuestaoDois != null)
            {
                if (d.QuestaoDois.Value != 0)
                {
                    rblQuestaoDoisDRADS.SelectedValue = d.QuestaoDois.Value.ToString();
                }
            }


            if (d.QuestaoTres != null)
            {
                if (d.QuestaoTres.Value != 0)
                {
                    rblQuestaoTresDRADS.SelectedValue = d.QuestaoTres.Value.ToString();
                }
            }

            if (d.QuestaoQuatro != null)
            {
                if (d.QuestaoQuatro.Value != 0)
                {
                    rblQuestaoQuatroDRADS.SelectedValue = d.QuestaoQuatro.Value.ToString();
                }
            }

            if (d.QuestaoCinco != null)
            {
                if (d.QuestaoCinco.Value != 0)
                {
                    rblQuestaoCincoDRADS.SelectedValue = d.QuestaoCinco.Value.ToString();
                }

            }

            if (d.QuestaoCinco != null)
            {
                if (d.QuestaoCinco == 2)
                {
                    if (!String.IsNullOrEmpty(d.QuestaoCincoEscrita))
                    {
                        controleQuestaoCincoDRADS(true);
                        txtQuestaoCincoDRADS.Text = d.QuestaoCincoEscrita.ToString();
                    }
                    else
                    {
                        controleQuestaoCincoDRADS(false);
                    }
                }
                else
                {
                    controleQuestaoCincoDRADS(false);
                }
            }
        }

        void zerarQuestionarioDRADS()
        {
            rblQuestaoUmDRADS.SelectedValue = null;
            rblQuestaoDoisDRADS.SelectedValue = null;
            rblQuestaoTresDRADS.SelectedValue = null;
            rblQuestaoQuatroDRADS.SelectedValue = null;
            rblQuestaoCincoDRADS.SelectedValue = null;
            controleQuestaoUmDRADS(false);
            controleQuestaoCincoDRADS(false);
        }


        void carregarComentarioCMAS(ComentarioPrestacaoDeContasCMASInfo c)
        {

            if (c != null)
            {
                if (!String.IsNullOrEmpty(c.Comentario))
                {
                    txtComentarioCMAS.Text = c.Comentario.ToString();
                }
                else
                {
                    txtComentarioCMAS.Text = "";
                }

            }
            else
            {
                txtComentarioCMAS.Text = "";
            }
        }

        void carregarComentarioDrads(ComentarioPrestacaoDeContasDRADSInfo c)
        {

            if (c != null)
            {
                if (!String.IsNullOrEmpty(c.Comentario))
                {
                    txtComentarioDRADS.Text = c.Comentario.ToString();
                }
                else
                {
                    txtComentarioDRADS.Text = "";
                }
            }
            else
            {
                txtComentarioDRADS.Text = "";
            }

        }

        void carregarDeliberacaoCMAS(DeliberacaoPrestacaoDeContasCMASInfo d)
        {

            txtDataReuniaoCMAS.Text = !String.IsNullOrEmpty(d.DataReuniao.ToString("dd/MM/yyyy")) ? d.DataReuniao.ToString("dd/MM/yyyy") : "";
            txtDataPublicacaoCMAS.Text = !String.IsNullOrEmpty(d.DataPublicacao.ToString("dd/MM/yyyy")) ? d.DataPublicacao.ToString("dd/MM/yyyy") : "";

            if (d.QuestaoDeliberacao != null || d.QuestaoDeliberacao != 0)
            {
                rblDeliberacaoCMAS.SelectedValue = d.QuestaoDeliberacao.ToString();

                if (d.QuestaoDeliberacao == 4)
                {
                    if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS)
                    {
                        btnDevolverCMAS.Enabled = true;
                    }
                    else
                    {
                        btnDevolverCMAS.Enabled = false;
                    }
                }
            }

            txtNumeroConselheirosCMAS.Text = !String.IsNullOrEmpty(d.NumeroConselheiros) ? d.NumeroConselheiros.ToString() : "";

            txtNumeroAtaCMAS.Text = !String.IsNullOrEmpty(d.NumeroAta) ? d.NumeroAta.ToString() : "";

            txtNumeroResolucaoCMAS.Text = !String.IsNullOrEmpty(d.NumeroResolucao) ? d.NumeroResolucao.ToString() : "";

        }

        void zerarDeliberacaoCMAS()
        {

            txtDataReuniaoCMAS.Text = "";
            txtDataPublicacaoCMAS.Text = "";
            txtNumeroConselheirosCMAS.Text = "";
            txtNumeroAtaCMAS.Text = "";
            txtNumeroResolucaoCMAS.Text = "";

        }


        void carregarDeliberacaoDrads(DeliberacaoPrestacaoDeContasDRADSInfo d)
        {
            txtDataReuniaoDRADS.Text = !String.IsNullOrEmpty(d.DataReuniao.ToString("dd/MM/yyyy")) ? d.DataReuniao.ToString("dd/MM/yyyy") : " ";
            txtDataPublicacaoDRADS.Text = !String.IsNullOrEmpty(d.DataPublicacao.ToString("dd/MM/yyyy")) ? d.DataPublicacao.ToString("dd/MM/yyyy") : " ";

            if (d.QuestaoDeliberacao != null || d.QuestaoDeliberacao != 0)
            {
                rblDeliberacaoDRADS.SelectedValue = d.QuestaoDeliberacao.ToString();
            }

            if (!String.IsNullOrEmpty(d.NumeroConselheiros))
            {
                txtNumeroConselheirosDRADS.Text = d.NumeroConselheiros.ToString();
            }

            if (!String.IsNullOrEmpty(d.NumeroAta))
            {
                txtNumeroAtaDRADS.Text = d.NumeroAta.ToString();
            }

            if (!String.IsNullOrEmpty(d.NumeroResolucao))
            {
                txtNumeroResolucaoDRADS.Text = d.NumeroResolucao.ToString();
            }
        }

        public bool validarDadosResponsaveisPreenchimentoGestor(int idPrefeitura, int idUsuario, int idPerfil)
        {
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);

                bool retorno;

                if (idPerfil == 64)
                {
                    var orgaoGestor = prefeituras.GetGestorMunicipalPrestacaoDeContas(idPrefeitura, idUsuario);

                    if (orgaoGestor != null)
                    {
                        retorno = true;
                    }
                    else
                    {
                        retorno = false;
                    }

                }
                else
                {
                    //lblDataOrgaoGestor.Text = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
                    retorno = false;
                }

                return retorno;
            }

        }

        public bool validarDadosResponsaveisPreenchimentoCMAS(int idPrefeitura, int idUsuario, int idPerfil)
        {

            int idMunicipio = SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio;
            bool retorno;

            var usuarioCMAS = new Usuarios();

            using (var proxy = new ProxyUsuarioPMAS())
            {
                if (idPerfil == 71)
                {
                    var usuario = usuarioCMAS.GetUsuarioById(idUsuario, proxy);

                    if (usuario != null)
                    {
                        retorno = true;
                    }
                    else
                    {
                        retorno = false;
                    }
                }
                else
                {
                    retorno = false;
                }

                return retorno;
            }
        }




        public bool validarDadosResponsaveisPreenchimentoDRADS(int idPerfil, int idUsuario)
        {

            var usuarioDRADS = new Usuarios();
            bool retorno;

            using (var proxy = new ProxyUsuarioPMAS())
            {
                if (idPerfil == 65)
                {
                    var usuario = usuarioDRADS.GetUsuarioById(idUsuario, proxy);

                    if (usuario != null)
                    {
                        retorno = true;
                    }
                    else
                    {

                    } retorno = false;

                }
                else
                {
                    retorno = false;
                    //lblDataDRADS.Text = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
                }

                return retorno;
            }
        }

        void carregarHistorico(int idPrefeitura, int exercicio)
        {
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);

                List<HistoricoPrestacaoDeContasInfo> historico = prefeituras.GetHistoricoPrestacaoDeContasDetalhes(idPrefeitura, exercicio);

                if (historico.Count != 0)
                {
                    lblSemDadosHistoricos.Visible = false;
                    lstHistorico.Visible = true;
                    lstHistorico.DataSource = historico.OrderByDescending(s => s.Data);
                    lstHistorico.DataBind();
                }
                else
                {
                    lstHistorico.Visible = false;
                    lblSemDadosHistoricos.Visible = true;
                }


            }
        }

        void carregarDadosOrgaoGestor(HistoricoPrestacaoDeContasInfo e)
        {
            using (var proxy = new ProxyPrefeitura())
            {
                var p = new Prefeituras(proxy);
                var gestor = p.GetAtualGestorMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                lblNomeOrgaoGestor.Text = gestor.Nome != null ? gestor.Nome.ToString() : "Nome Gestor Municipal";
                lblCPFOrgaoGestor.Text = gestor.CPF != null ? gestor.CPF.ToString() : "xxx.xxx.xxx-xx";
            }

            lblDataOrgaoGestor.Text = e.Data != null ? e.Data.ToString("dd/MM/yyyy") : DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
        }

        void zerarDadosOGestor()
        {
            lblNomeOrgaoGestor.Text = "Nome Gestor Municipal";
            lblCPFOrgaoGestor.Text = "xxx.xxx.xxx-xx";
            lblDataOrgaoGestor.Text = "xx/xx/xxxx";
        }

        void carregarDadosCmas(HistoricoPrestacaoDeContasInfo c)
        {

            lblNomeCMAS.Text = c.NomeResponsavel != null ? c.NomeResponsavel : "Nome Presidente CMAS";
            lblCpfCMAS.Text = c.CPFResponsavel != null ? c.CPFResponsavel : "xxx.xxx.xxx-xx";
            lblDataCMAS.Text = c.Data != null ? c.Data.ToString("dd/MM/yyyy") : DateTime.UtcNow.Date.ToString("dd/MM/yyyy");

        }

        void zerarDadosCmas()
        {

            lblNomeCMAS.Text = "Nome Presidente CMAS";
            lblCpfCMAS.Text = "xxx.xxx.xxx-xx";
            lblDataCMAS.Text = "xx/xx/xxxx"; //DateTime.UtcNow.Date.ToString("dd/MM/yyyy");

        }

        void carregarDadosDrads(HistoricoPrestacaoDeContasInfo d)
        {

            lblNomeDRADS.Text = d.NomeResponsavel != null ? d.NomeResponsavel : "Nome Administrador Drads";
            lblCPFDRADS.Text = d.CPFResponsavel != null ? d.CPFResponsavel : "xxx.xxx.xxx-xx";
            chkDeAcordo.Checked = Convert.ToBoolean(d.PosicaoFinal);
            lblDataDRADS.Text = d.Data != null ? d.Data.ToString("dd/MM/yyyy") : DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
        }

        void zerarDadosDrads()
        {

            lblNomeDRADS.Text = "Nome Administrador Drads";
            lblCPFDRADS.Text = "xxx.xxx.xxx-xx";
            chkDeAcordo.Checked = false;
            lblDataDRADS.Text = "xx/xx/xxxx";//DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
        }


        void registrarHistorico(int idPrefeitura, int idPerfil, int idSituacaoQuadro, string nomeResponsavel, string cpfResponsavel, string descricaoMotivo, int posicaoFinal, int deAcordo, DateTime data, int exercicio)
        {

            var historico = new HistoricoPrestacaoDeContasInfo();

            historico.IdPrefeitura = idPrefeitura;
            historico.IdPerfil = idPerfil;
            historico.IdSituacaoQuadro = idSituacaoQuadro;
            historico.NomeResponsavel = nomeResponsavel;
            historico.CPFResponsavel = cpfResponsavel;
            historico.DescricaoMotivo = descricaoMotivo;
            historico.PosicaoFinal = posicaoFinal;
            historico.DeAcordo = deAcordo;
            historico.Data = data;
            historico.Exercicio = exercicio;

            String msg = String.Empty;

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    prefeituras.SaveHistoricoPrestacaoDeContas(historico);

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
            }


        }

        private void AdicionarEventosJs()
        {
            txtFEASPrevisaoInicialBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFEASPrevisaoInicialMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFEASPrevisaoInicialAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtPrevisaoInicialReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtRecursosDisponibilizadosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtResultadosAplicacaoReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValoresExecutadosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValoresReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValoresDevolvidosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtPorcentagensDevolucaoReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtReprogramacaoPrevisaoInicialBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoPrevisaoInicialMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoPrevisaoInicialAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtReprogramacaoRecursosDisponibilizadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoRecursosDisponibilizadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoRecursosDisponibilizadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoRecursosDisponiveisBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtReprogramacaoResultadoAppFinanceirasBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoResultadosAppFinanceirasMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoResultadoAppFinanceirasAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoResultadosAppFinanceirasBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtReprogramacaoValoresExecutadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoValoresExecutadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoValoresExecutadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoValoresExecutadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtReprogramacaoValoresReprogramadosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoValoresReprogramadosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoValoresReprogramadosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoValoresReprogramadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtReprogramacaoValoresDevolvidosBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoValoresDevolvidosMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoValoresDevolvidosAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoValoresDevolvidosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtReprogramacaoPorcentagensBasica.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoPorcentagensMedia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoPorcentagensAlta.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoPorcentagensBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFEASPrevisaoInicialBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoBeneficiosEventuais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFEASPrevisaoInicialProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoProgramasProjetos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosProgramasProjetosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasProgramasProjetosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosProgramasProjetosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosProgramasProjetosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosProgramasProjetosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoProgramasProjetosReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");


            txtFEASPrevisaoInicialBasicaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosBasicaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasBasicaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosBasicaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosBasicaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosBasicaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoBasicaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            
            txtFEASPrevisaoInicialBasicaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosBasicaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasBasicaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosBasicaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosBasicaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosBasicaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoBasicaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            
            
            txtFEASPrevisaoInicialMediaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosMediaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasMediaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosMediaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosMediaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosMediaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoMediaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            
            txtFEASPrevisaoInicialMediaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosMediaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasMediaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosMediaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosMediaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosMediaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoMediaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            
            txtFEASPrevisaoInicialAltaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosAltaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasAltaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosAltaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosAltaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosAltaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoAltaDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            
            txtFEASPrevisaoInicialAltaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosAltaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasAltaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosAltaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosAltaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosAltaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoAltaReprogramacaoDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            
            
            txtFEASPrevisaoInicialBeneficiosEventuaisDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosBeneficiosEventuaisDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosBeneficiosEventuaisDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosBeneficiosEventuaisDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            
            
            txtFEASPrevisaoInicialBeneficiosEventuaisDemandasReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandasReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandasReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresExecutadosBeneficiosEventuaisDemandasReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresReprogramadosBeneficiosEventuaisDemandasReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASValoresDevolvidosBeneficiosEventuaisDemandasReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandasReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");





        }

        public WebControl[] ObterControleBasica()
        {
            WebControl[] controles = {
             txtFEASPrevisaoInicialBasica
            , txtFEASResultadoAppFinanceirasBasica
            , txtFEASRecursosDisponibilizadosBasica
            , txtFEASValoresExecutadosBasica
            , txtFEASValoresReprogramadosBasica
            ,txtFEASPrevisaoInicialBasicaDemandas
            ,txtFEASRecursosDisponibilizadosBasicaDemandas
            ,txtFEASResultadoAppFinanceirasBasicaDemandas
            ,txtFEASValoresExecutadosBasicaDemandas
            ,txtFEASValoresReprogramadosBasicaDemandas
            ,txtFEASValoresDevolvidosBasicaDemandas
            ,txtFEASPorcentagensExecucaoBasicaDemandas

                                       };
            return controles;
        }

        public WebControl[] ObterControleMedia()
        {
            WebControl[] controles = {
                  txtFEASPrevisaoInicialMedia
                , txtFEASResultadoAppFinanceirasMedia
                , txtFEASRecursosDisponibilizadosMedia
                , txtFEASValoresExecutadosMedia
                , txtFEASValoresReprogramadosMedia
                , txtFEASPrevisaoInicialMediaDemandas
                , txtFEASRecursosDisponibilizadosMediaDemandas
                , txtFEASResultadoAppFinanceirasMediaDemandas
                , txtFEASValoresExecutadosMediaDemandas
                , txtFEASValoresReprogramadosMediaDemandas
                , txtFEASValoresDevolvidosMediaDemandas
                , txtFEASPorcentagensExecucaoMediaDemandas
                                       };
            return controles;
        }

        public WebControl[] ObterControleAlta()
        {
            WebControl[] controles = {
                  txtFEASPrevisaoInicialAlta
                , txtFEASResultadoAppFinanceirasAlta
                , txtFEASRecursosDisponibilizadosAlta
                , txtFEASValoresExecutadosAlta
                , txtFEASValoresReprogramadosAlta
                , txtFEASPrevisaoInicialAltaDemandas
                , txtFEASRecursosDisponibilizadosAltaDemandas
                , txtFEASResultadoAppFinanceirasAltaDemandas
                , txtFEASValoresExecutadosAltaDemandas
                , txtFEASValoresReprogramadosAltaDemandas
                , txtFEASValoresDevolvidosAltaDemandas
                , txtFEASPorcentagensExecucaoAltaDemandas
                                       };
            return controles;
        }

        public WebControl[] ObterControleReprogramacao()
        {
            WebControl[] controles = {
                txtPrevisaoInicialReprogramacao,
                txtRecursosDisponibilizadosReprogramacao,
                txtResultadosAplicacaoReprogramacao,
                txtValoresExecutadosReprogramacao,
                txtValoresReprogramacao,

                 txtReprogramacaoPrevisaoInicialBasica,
                 txtReprogramacaoPrevisaoInicialMedia,
                 txtReprogramacaoPrevisaoInicialAlta,
                 txtReprogramacaoPrevisaoInicialBeneficiosEventuais,

                 txtReprogramacaoRecursosDisponibilizadosBasica,
                 txtReprogramacaoRecursosDisponibilizadosMedia,
                 txtReprogramacaoRecursosDisponibilizadosAlta,
                 txtReprogramacaoRecursosDisponiveisBeneficiosEventuais,

                 txtReprogramacaoResultadoAppFinanceirasBasica,
                 txtReprogramacaoResultadosAppFinanceirasMedia,
                 txtReprogramacaoResultadoAppFinanceirasAlta,
                 txtReprogramacaoResultadosAppFinanceirasBeneficiosEventuais,

                 txtReprogramacaoValoresExecutadosBasica,
                 txtReprogramacaoValoresExecutadosMedia,
                 txtReprogramacaoValoresExecutadosAlta,
                 txtReprogramacaoValoresExecutadosBeneficiosEventuais,

                 txtReprogramacaoValoresReprogramadosBasica,
                 txtReprogramacaoValoresReprogramadosMedia,
                 txtReprogramacaoValoresReprogramadosAlta,
                 txtReprogramacaoValoresReprogramadosBeneficiosEventuais,

                 txtFEASPrevisaoInicialProgramasProjetosReprogramacao,
                 txtFEASRecursosDisponibilizadosProgramasProjetosReprogramacao,
                 txtFEASResultadoAppFinanceirasProgramasProjetosReprogramacao,
                 txtFEASValoresExecutadosProgramasProjetosReprogramacao,
                 txtFEASValoresReprogramadosProgramasProjetosReprogramacao,

                 txtFEASPrevisaoInicialBasicaReprogramacaoDemandas,
                 txtFEASRecursosDisponibilizadosBasicaReprogramacaoDemandas,
                 txtFEASResultadoAppFinanceirasBasicaReprogramacaoDemandas,
                 txtFEASValoresExecutadosBasicaReprogramacaoDemandas,
                 txtFEASValoresReprogramadosBasicaReprogramacaoDemandas,
                 txtFEASValoresDevolvidosBasicaReprogramacaoDemandas,
                 txtFEASPorcentagensExecucaoBasicaReprogramacaoDemandas,

                 txtFEASPrevisaoInicialMediaReprogramacaoDemandas,
                 txtFEASRecursosDisponibilizadosMediaReprogramacaoDemandas,
                 txtFEASResultadoAppFinanceirasMediaReprogramacaoDemandas,
                 txtFEASValoresExecutadosMediaReprogramacaoDemandas,
                 txtFEASValoresReprogramadosMediaReprogramacaoDemandas,
                 txtFEASValoresDevolvidosMediaReprogramacaoDemandas,
                 txtFEASPorcentagensExecucaoMediaReprogramacaoDemandas,

                 txtFEASPrevisaoInicialAltaReprogramacaoDemandas,
                 txtFEASRecursosDisponibilizadosAltaReprogramacaoDemandas,
                 txtFEASResultadoAppFinanceirasAltaReprogramacaoDemandas,
                 txtFEASValoresExecutadosAltaReprogramacaoDemandas,
                 txtFEASValoresReprogramadosAltaReprogramacaoDemandas,
                 txtFEASValoresDevolvidosAltaReprogramacaoDemandas,
                 txtFEASPorcentagensExecucaoAltaReprogramacaoDemandas,

                 txtFEASPrevisaoInicialBeneficiosEventuaisDemandasReprogramacao,
                 txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandasReprogramacao,
                 txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandasReprogramacao,
                 txtFEASValoresExecutadosBeneficiosEventuaisDemandasReprogramacao,
                 txtFEASValoresReprogramadosBeneficiosEventuaisDemandasReprogramacao,
                 txtFEASValoresDevolvidosBeneficiosEventuaisDemandasReprogramacao,
                 txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandasReprogramacao

                                       };
            return controles;
        }

        public WebControl[] ObterControleBeneficiosEventuais()
        {
            WebControl[] controles = {
                txtFEASPrevisaoInicialBeneficiosEventuais
                ,txtFEASRecursosDisponibilizadosBeneficiosEventuais
                ,txtFEASResultadoAppFinanceirasBeneficiosEventuais
                ,txtFEASValoresExecutadosBeneficiosEventuais
                ,txtFEASValoresReprogramadosBeneficiosEventuais
                ,txtFEASPrevisaoInicialBeneficiosEventuaisDemandas
                ,txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandas
                ,txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandas
                ,txtFEASValoresExecutadosBeneficiosEventuaisDemandas
                ,txtFEASValoresReprogramadosBeneficiosEventuaisDemandas
                ,txtFEASValoresDevolvidosBeneficiosEventuaisDemandas
                ,txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandas
                                       };
            return controles;
        }

        public WebControl[] ObterControleProgramaProjeto()
        {
            WebControl[] controles = {
                txtFEASPrevisaoInicialProgramasProjetos
                ,txtFEASRecursosDisponibilizadosProgramasProjetos
                ,txtFEASResultadoAppFinanceirasProgramasProjetos
                ,txtFEASValoresExecutadosProgramasProjetos
                ,txtFEASValoresReprogramadosProgramasProjetos
                                       };
            return controles;
        }

        public WebControl[] ObterControleCalculaveis()
        {
            WebControl[] controles = {

                txtFEASValoresDevolvidosBasica
                ,txtFEASPorcentagensExecucaoBasica
                ,txtFEASValoresDevolvidosMedia
                ,txtFEASPorcentagensExecucaoMedia
                ,txtFEASValoresDevolvidosAlta
                ,txtFEASPorcentagensExecucaoAlta
                ,txtValoresDevolvidosReprogramacao
                ,txtPorcentagensDevolucaoReprogramacao
                ,txtFEASPorcentagensExecucaoBeneficiosEventuais                 
                ,txtFEASValoresDevolvidosBeneficiosEventuais
                ,txtFEASPorcentagensExecucaoBeneficiosEventuais
                ,txtFEASValoresDevolvidosProgramasProjetos                
                ,txtFEASPorcentagensExecucaoProgramasProjetos
                ,txtFEASValoresDevolvidosProgramasProjetosReprogramacao
                ,txtFEASPorcentagensExecucaoProgramasProjetosReprogramacao
                
                ,txtReprogramacaoValoresDevolvidosBasica
                ,txtReprogramacaoValoresDevolvidosMedia
                ,txtReprogramacaoValoresDevolvidosAlta
                ,txtReprogramacaoValoresDevolvidosBeneficiosEventuais

                ,txtReprogramacaoPorcentagensBasica
                ,txtReprogramacaoPorcentagensMedia
                ,txtReprogramacaoPorcentagensAlta
                ,txtReprogramacaoPorcentagensBeneficiosEventuais

                ,txtFEASValoresDevolvidosBasicaDemandas
                ,txtFEASPorcentagensExecucaoBasicaDemandas
                ,txtFEASValoresDevolvidosBasicaReprogramacaoDemandas
                ,txtFEASPorcentagensExecucaoBasicaReprogramacaoDemandas
                ,txtFEASValoresDevolvidosMediaDemandas
                ,txtFEASPorcentagensExecucaoMediaDemandas
                ,txtFEASValoresDevolvidosMediaReprogramacaoDemandas
                ,txtFEASPorcentagensExecucaoMediaReprogramacaoDemandas
                ,txtFEASValoresDevolvidosAltaDemandas
                ,txtFEASPorcentagensExecucaoAltaDemandas
                ,txtFEASValoresDevolvidosAltaReprogramacaoDemandas
                ,txtFEASPorcentagensExecucaoAltaReprogramacaoDemandas
                ,txtFEASValoresDevolvidosBeneficiosEventuaisDemandas
                ,txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandas
                ,txtFEASValoresDevolvidosBeneficiosEventuaisDemandasReprogramacao
                ,txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandasReprogramacao

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

        private void controleQuestaoUmDRADS(Boolean c)
        {
            lgdQuestaoUmDRADS.Visible = c;
            txtQuestaoUmDRADS.Visible = c;
        }

        private void controleQuestaoCincoDRADS(Boolean c)
        {
            lgdQuestaoCincoDRADS.Visible = c;
            txtQuestaoCincoDRADS.Visible = c;
        }

        private void controleQuestaoSeisCMAS(Boolean c)
        {
            lgdQuestaoSeisCMAS.Visible = c;
            txtQuestaoSeisCMAS.Visible = c;
        }

        private void controleQuestaoSeteCMAS(Boolean c)
        {
            lgdQuestaoSeteCMAS.Visible = c;
            txtQuestaoSeteCMAS.Visible = c;
        }

        private void habilitaQuestionarioCMAS(Boolean c)
        {
            rblQuestaoUmCMAS.Enabled = c;
            rblQuestaoDoisCMAS.Enabled = c;
            rblQuestaoTresCMAS.Enabled = c;
            rblQuestaoQuatroCMAS.Enabled = c;
            rblQuestaoCincoCMAS.Enabled = c;
            rblQuestaoSeisCMAS.Enabled = c;
            rblQuestaoSeteCMAS.Enabled = c;
            rblQuestaoOitoCMAS.Enabled = c;
            rblQuestaoNoveCMAS.Enabled = c;

            txtQuestaoSeisCMAS.Enabled = c;
            txtQuestaoSeteCMAS.Enabled = c;
            txtComentarioCMAS.Enabled = c;

            rblDeliberacaoCMAS.Enabled = c;

            txtDataReuniaoCMAS.Enabled = c;
            txtNumeroAtaCMAS.Enabled = c;
            txtNumeroConselheirosCMAS.Enabled = c;
            txtNumeroResolucaoCMAS.Enabled = c;
            txtDataPublicacaoCMAS.Enabled = c;

            btnSalvarCMAS.Enabled = c;
        }

        private void habilitaquestionarioDRADS(Boolean c)
        {
            rblQuestaoUmDRADS.Enabled = c;
            rblQuestaoDoisDRADS.Enabled = c;
            rblQuestaoTresDRADS.Enabled = c;
            rblQuestaoQuatroDRADS.Enabled = c;
            rblQuestaoCincoDRADS.Enabled = c;
            rblDeliberacaoDRADS.Enabled = c;

            txtQuestaoUmDRADS.Enabled = c;
            txtQuestaoCincoDRADS.Enabled = c;
            txtComentarioDRADS.Enabled = c;

            rblDeliberacaoDRADS.Enabled = c;

            txtDataReuniaoDRADS.Enabled = c;
            txtNumeroAtaDRADS.Enabled = c;
            txtNumeroConselheirosDRADS.Enabled = c;
            txtNumeroResolucaoDRADS.Enabled = c;
            txtDataPublicacaoDRADS.Enabled = c;

            btnSalvarDRADS.Enabled = c;
        }

        private WebControl[] obterControleQuestoesEscritasDRADS()
        {
            WebControl[] controles = { };
            return controles;
        }

        void ClearBasica()
        {

            txtFEASPrevisaoInicialBasica.Text = "0,00";
            txtFEASRecursosDisponibilizadosBasica.Text = "0,00";
            txtFEASResultadoAppFinanceirasBasica.Text = "0,00";
            txtFEASValoresExecutadosBasica.Text = "0,00";
            txtFEASValoresReprogramadosBasica.Text = "0,00";
            txtFEASValoresDevolvidosBasica.Text = "0,00";
            txtFEASPorcentagensExecucaoBasica.Text = "0,00";

        }

        void ClearMedia()
        {
            txtFEASPrevisaoInicialMedia.Text = "0,00";
            txtFEASRecursosDisponibilizadosMedia.Text = "0,00";
            txtFEASResultadoAppFinanceirasMedia.Text = "0,00";
            txtFEASValoresExecutadosMedia.Text = "0,00";
            txtFEASValoresReprogramadosMedia.Text = "0,00";
            txtFEASValoresDevolvidosMedia.Text = "0,00";
            txtFEASPorcentagensExecucaoMedia.Text = "0,00";
        }

        void ClearAlta()
        {
            txtFEASPrevisaoInicialAlta.Text = "0,00";
            txtFEASRecursosDisponibilizadosAlta.Text = "0,00";
            txtFEASResultadoAppFinanceirasAlta.Text = "0,00";
            txtFEASValoresExecutadosAlta.Text = "0,00";
            txtFEASValoresReprogramadosAlta.Text = "0,00";
            txtFEASValoresDevolvidosAlta.Text = "0,00";
            txtFEASPorcentagensExecucaoAlta.Text = "0,00";
        }

        void ClearReprogramacao()
        {
            txtPrevisaoInicialReprogramacao.Text = "0,00";
            txtRecursosDisponibilizadosReprogramacao.Text = "0,00";
            txtResultadosAplicacaoReprogramacao.Text = "0,00";
            txtValoresExecutadosReprogramacao.Text = "0,00";
            txtValoresReprogramacao.Text = "0,00";
            txtValoresDevolvidosReprogramacao.Text = "0,00";
            txtPorcentagensDevolucaoReprogramacao.Text = "0,00";

            txtReprogramacaoPrevisaoInicialBasica.Text = "0,00";
            txtReprogramacaoPrevisaoInicialMedia.Text = "0,00";
            txtReprogramacaoPrevisaoInicialAlta.Text = "0,00";
            txtReprogramacaoPrevisaoInicialBeneficiosEventuais.Text = "0,00";

            txtReprogramacaoRecursosDisponibilizadosBasica.Text = "0,00";
            txtReprogramacaoRecursosDisponibilizadosMedia.Text = "0,00";
            txtReprogramacaoRecursosDisponibilizadosAlta.Text = "0,00";
            txtReprogramacaoRecursosDisponiveisBeneficiosEventuais.Text = "0,00";

            txtReprogramacaoResultadoAppFinanceirasBasica.Text = "0,00";
            txtReprogramacaoResultadosAppFinanceirasMedia.Text = "0,00";
            txtReprogramacaoResultadoAppFinanceirasAlta.Text = "0,00";
            txtReprogramacaoResultadosAppFinanceirasBeneficiosEventuais.Text = "0,00";

            txtReprogramacaoValoresExecutadosBasica.Text = "0,00";
            txtReprogramacaoValoresExecutadosMedia.Text = "0,00";
            txtReprogramacaoValoresExecutadosAlta.Text = "0,00";
            txtReprogramacaoValoresExecutadosBeneficiosEventuais.Text = "0,00";

            txtReprogramacaoValoresReprogramadosBasica.Text = "0,00";
            txtReprogramacaoValoresReprogramadosMedia.Text = "0,00";
            txtReprogramacaoValoresReprogramadosAlta.Text = "0,00";
            txtReprogramacaoValoresReprogramadosBeneficiosEventuais.Text = "0,00";

            txtReprogramacaoValoresDevolvidosBasica.Text = "0,00";
            txtReprogramacaoValoresDevolvidosMedia.Text = "0,00";
            txtReprogramacaoValoresDevolvidosAlta.Text = "0,00";
            txtReprogramacaoValoresDevolvidosBeneficiosEventuais.Text = "0,00";

            txtReprogramacaoPorcentagensBasica.Text = "0,00";
            txtReprogramacaoPorcentagensMedia.Text = "0,00";
            txtReprogramacaoPorcentagensAlta.Text = "0,00";
            txtReprogramacaoPorcentagensBeneficiosEventuais.Text = "0,00";

        }

        void ClearBeneficiosEventuais()
        {
            txtFEASPrevisaoInicialBeneficiosEventuais.Text = "0,00";
            txtFEASRecursosDisponibilizadosBeneficiosEventuais.Text = "0,00";
            txtFEASResultadoAppFinanceirasBeneficiosEventuais.Text = "0,00";
            txtFEASValoresExecutadosBeneficiosEventuais.Text = "0,00";
            txtFEASValoresReprogramadosBeneficiosEventuais.Text = "0,00";
            txtFEASValoresDevolvidosBeneficiosEventuais.Text = "0,00";
            txtFEASPorcentagensExecucaoBeneficiosEventuais.Text = "0,00";
        }

        void ClearProgramasProjetos()
        {
            txtFEASPrevisaoInicialProgramasProjetos.Text = "0,00";
            txtFEASRecursosDisponibilizadosProgramasProjetos.Text = "0,00";
            txtFEASResultadoAppFinanceirasProgramasProjetos.Text = "0,00";
            txtFEASValoresExecutadosProgramasProjetos.Text = "0,00";
            txtFEASValoresReprogramadosProgramasProjetos.Text = "0,00";
            txtFEASValoresDevolvidosProgramasProjetos.Text = "0,00";
            txtFEASPorcentagensExecucaoProgramasProjetos.Text = "0,00";
        }

        void ClearProgramasProjetosReprogramacao()
        {
            txtFEASPrevisaoInicialProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASRecursosDisponibilizadosProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASResultadoAppFinanceirasProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASValoresExecutadosProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASValoresReprogramadosProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASValoresDevolvidosProgramasProjetosReprogramacao.Text = "0,00";
            txtFEASPorcentagensExecucaoProgramasProjetosReprogramacao.Text = "0,00";
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




        void QuestionarioCMAS()
        {
            String msg = String.Empty;

            var questoesCMAS = PreencherQuestionarioCMAS();

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    prefeituras.SaveQuestionarioPrestacaoDeContasCMAS(questoesCMAS);
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
            }

        }

        void QuestionarioDRADS()
        {
            var questoesDRADS = PreencherQuestionarioDRADS();
            String msg = String.Empty;

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    if (questoesDRADS != null)
                    {
                        prefeituras.SaveQuestionarioPrestacaoDeContasDRADS(questoesDRADS);
                    }

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
            }
        }

        void ComentarioCMAS()
        {


            var comentarioCMAS = PreencherComentarioCMAS();

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    if (comentarioCMAS.Comentario.Length > 5000)
                    {
                        throw new InvalidOperationException("O número máximo de caracteres foi ultrapassado no comentário CMAS.");
                    }
                    else
                    {
                        prefeituras.SaveComentarioPrestacaoDeContasCMAS(comentarioCMAS);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        void ComentarioDRADS()
        {

            var comentarioDRADS = PreecherComentarioDRADS();

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    if (comentarioDRADS != null)
                    {
                        if (comentarioDRADS.Comentario != null)
                        {
                            if (comentarioDRADS.Comentario.Length > 5000)
                            {
                                throw new InvalidOperationException("O número máximo de caracteres foi ultrapassado no comentário DRADS.");
                            }
                            else
                            {
                                prefeituras.SaveComentarioPrestacaoDeContasDRADS(comentarioDRADS);
                            }
                        }

                    }
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        void DeliberacaoCMAS()
        {
            var deliberacaoCMAS = PreencherDeliberacaoCMAS();
            String msg = String.Empty;

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    prefeituras.SaveDeliberacaoPrestacaoDeContasCMAS(deliberacaoCMAS);
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
            }
        }

        void DeliberacaoDRADS()
        {
            var deliberacaoDRADS = PreencherDeliberacaoDRADS();
            String msg = String.Empty;

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    if (deliberacaoDRADS != null)
                    {
                        prefeituras.SaveDeliberacaoPrestacaoDeContasDRADS(deliberacaoDRADS);
                    }
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
            }
        }

        protected void btnDesbloqueio_Click(object sender, EventArgs e)
        {
            String msg = String.Empty;

            try
            {
                using (var proxy = new ProxyUsuarioPMAS())
                {
                    int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                    int idPerfil = Convert.ToInt32(SessaoPmas.UsuarioLogado.EnumPerfil);
                    int idSituacaoQuadro = 1;
                    int idUsuario = SessaoPmas.UsuarioLogado.IdUsuario;

                    var usuario = new Usuarios();

                    var adm = usuario.GetUsuarioById(idUsuario, proxy);

                    var nome = adm.Nome != null ? adm.Nome : "Nome não cadastrado para o orgão gestor";
                    var CPF = adm.CPF != null ? adm.CPF : "000.000.000-00";
                    string comentario = "Plano desbloqueado para registro das informações referentes ao exercício de" + hdfAno.Value;

                    int exercicio = Convert.ToInt32(hdfAno.Value);

                    AlterarSituacaoQuadro(idSituacaoQuadro);

                    registrarHistorico(idPrefeitura, idPerfil, idSituacaoQuadro, nome, CPF, comentario, 0, 0, DateTime.Now, exercicio);

                }
            }
            catch (Exception ex)
            {

                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Plano desbloqueado com sucesso !";
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
            var reprogramadoBasica = PreencherReprogramacaoProtecaoBasica(exercicio);
            var basicaDemandas = PreencherDemandasProtecaoBasica(exercicio);
            var basicaReprogramacaoDemandas = PreencherReprogramacaoDemandasProtecaoBasica(exercicio);
            var reprogramado = PreencherProtecaoEspecialReprogramado(exercicio);
            var media = PreencherProtecaoEspecialMedia(exercicio);
            var mediaDemandas = PreencherDemandasProtecaoMedia(exercicio);
            var mediaReprogramacaoDemandas = PreencherReprogramacaoDemandasProtecaoMedia(exercicio);
            var reprogramadoMedia = PreencherReprogramacaoProtecaoMedia(exercicio);
            var alta = PreencherProtecaoEspecialAlta(exercicio);
            var altaDemandas = PreencherDemandasProtecaoAlta(exercicio);
            var altaReprogramacaoDemandas = PreencherReprogramacaoDemandasProtecaoAlta(exercicio);
            var reprogramadoAlta = PreencherReprogramacaoProtecaoAlta(exercicio);
            var beneficiosEventuais = PreencherBeneficiosEventuais(exercicio);
            var reprogramadoBeneficiosEventuais = PreencherReprogramacaoProtecaoBeneficiosEventuais(exercicio);
            var beneficiosEventuaisDemandas = PreencherDemandasBeneficiosEventuais(exercicio);
            var beneficiosEventuaisReprogramacaoDemandas = PreencherReprogramacaoDemandasBeneficiosEventuais(exercicio);
            var programaProjeto = PreencherProgramasProjetos(exercicio);
            var reprogramacaoProgramasProjetos = PreencherProgramasProjetosReprogramacao(exercicio);

            carregarBasica(basica);
            carregarReprogramacaoBasica(reprogramadoBasica);
            carregarDemandasParlamentaresBasica(basicaDemandas);
            carregarReprogramacaoDemandasParlamentaresBasica(basicaReprogramacaoDemandas);
            carregarReprogramado(reprogramado);
            carregarMedia(media);
            carregarReprogramacaoMedia(reprogramadoMedia);
            carregarDemandasParlamentaresMedia(mediaDemandas);
            carregarReprogramacaoDemandasParlamentaresMedia(mediaReprogramacaoDemandas);
            carregarAlta(alta);
            carregarReprogramacaoAlta(reprogramadoAlta);
            carregarDemandasParlamentaresAlta(altaDemandas);
            carregarReprogramacaoDemandasParlamentaresAlta(altaReprogramacaoDemandas);
            carregarBeneficiosEventuais(beneficiosEventuais);
            carregarReprogramacaoBeneficiosEventuais(reprogramadoBeneficiosEventuais);
            carregarDemandasParlamentaresBeneficiosEventuais(beneficiosEventuaisDemandas);
            carregarReprogramacaoDemandasParlamentaresBeneficiosEventuais(beneficiosEventuaisReprogramacaoDemandas);
            carregarProgramasProjetos(programaProjeto);
            carregarProgramasProjetosReprogramacao(reprogramacaoProgramasProjetos);


            totalizar(basica, reprogramadoBasica, basicaDemandas, basicaReprogramacaoDemandas, reprogramado, media, reprogramadoMedia, mediaDemandas, mediaReprogramacaoDemandas, alta, reprogramadoAlta, altaDemandas, altaReprogramacaoDemandas, beneficiosEventuais, reprogramadoBeneficiosEventuais, beneficiosEventuaisDemandas, beneficiosEventuaisReprogramacaoDemandas, programaProjeto, reprogramacaoProgramasProjetos);

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            var basica = PreencherProtecaoBasica(exercicio);
            var reprogramacaoBasica = PreencherReprogramacaoProtecaoBasica(exercicio);
            var basicaDemandas = PreencherDemandasProtecaoBasica(exercicio);
            var basicaReprogramacaoDemandas = PreencherReprogramacaoDemandasProtecaoBasica(exercicio);
            var especialMedia = PreencherProtecaoEspecialMedia(exercicio);
            var reprogramacaoMedia = PreencherReprogramacaoProtecaoMedia(exercicio);
            var mediaDemandas = PreencherDemandasProtecaoMedia(exercicio);
            var mediaReprogramacaoDemandas = PreencherReprogramacaoDemandasProtecaoMedia(exercicio);
            var especialAlta = PreencherProtecaoEspecialAlta(exercicio);
            var reprogramacaoAlta = PreencherReprogramacaoProtecaoAlta(exercicio);
            var altaDemandas = PreencherDemandasProtecaoAlta(exercicio);
            var altaReprogramacaoDemandas = PreencherReprogramacaoDemandasProtecaoAlta(exercicio);
            var beneficiosEventuais = PreencherBeneficiosEventuais(exercicio);
            var reprogramacaoBeneficiosEventuais = PreencherReprogramacaoProtecaoBeneficiosEventuais(exercicio);
            var beneficiosEventuaisDemandas = PreencherDemandasBeneficiosEventuais(exercicio);
            var beneficiosEventuaisReprogramacaoDemandas = PreencherReprogramacaoDemandasBeneficiosEventuais(exercicio);
            var programasProjetos = PreencherProgramasProjetos(exercicio);
            var reprogramacaoProgramasProjetos = PreencherProgramasProjetosReprogramacao(exercicio);
            var exercicioAnterior = PreencherProtecaoEspecialReprogramado(exercicio);
            var protecaoEspecial = PreencherProtecaoEspecial(exercicio);
            var incentivo = PreencherIncentivo(exercicio);


            String msg = String.Empty;

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                    int idSituacaoQuadro = 1;

                    ComentarioPrestacaoDeContasInfo comentario = new ComentarioPrestacaoDeContasInfo();

                    if (txtComentario.Text.Length > 5000)
                    {
                        throw new InvalidOperationException("O número máximo de caracteres foi ultrapassado no comentário órgão gestor.");
                    }
                    else if (String.IsNullOrEmpty(txtComentario.Text))
                    {
                        throw new InvalidOperationException("Favor inserir o comentário do órgão gestor.");
                    }
                    else
                    {
                        comentario.Comentario = txtComentario.Text;
                    }

                    comentario.Exercicio = exercicio;
                    comentario.Desbloqueado = true;
                    comentario.IdSituacao = idSituacaoQuadro;

                    prefeituras.SavePrestacaoDeContas(comentario, basica, reprogramacaoBasica, basicaDemandas, basicaReprogramacaoDemandas, especialMedia, reprogramacaoMedia, mediaDemandas, mediaReprogramacaoDemandas, especialAlta, reprogramacaoAlta, altaDemandas, altaReprogramacaoDemandas, beneficiosEventuais, reprogramacaoBeneficiosEventuais, beneficiosEventuaisDemandas, beneficiosEventuaisReprogramacaoDemandas, programasProjetos, reprogramacaoProgramasProjetos, exercicioAnterior, protecaoEspecial, incentivo);

                    var gestor = prefeituras.GetAtualGestorMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                    int nomeGestorMunincipal = Convert.ToInt32(gestor.IdUsuarioGestor);

                    if (SessaoPmas.UsuarioLogado.IdUsuario == nomeGestorMunincipal)
                    {
                        btnFinalizar.Enabled = true;
                    }
                    else
                    {
                        btnFinalizar.Enabled = false;
                    }

                    var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 168).Where(x => x.Exercicio == exercicio).FirstOrDefault();

                    if (quadro == null)
                    {
                        quadro = new PrefeituraSituacaoQuadroInfo()
                        {
                            IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id,
                            IdRecurso = 168,
                            IdSituacaoQuadro = idSituacaoQuadro
                        };

                        proxy.Service.SavePrefeituraSituacaoQuadro(quadro);
                    }

                    CarregarPrestacaoDeContas(prefeituras);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "O Resumo da execução financeira dos recursos estaduais foi salvo com sucesso !";
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


        protected void btnFinalizar_Click(object sender, EventArgs e)
        {

            if (txtComentario.Text.Length >= 1)
            {

                int exercicio = Convert.ToInt32(hdfAno.Value);
                String msg = String.Empty;

                try
                {
                    using (var proxy = new ProxyUsuarioPMAS())
                    {

                        int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                        int idPerfil = Convert.ToInt32(SessaoPmas.UsuarioLogado.EnumPerfil);
                        int idUsuario = SessaoPmas.UsuarioLogado.IdUsuario;
                        int idSituacaoQuadro = 3;



                        var usuario = new Usuarios();
                        var gestor = usuario.GetUsuarioById(idUsuario, proxy);

                        var nome = gestor.Nome != null ? gestor.Nome : "Nome não cadastrado.";
                        var CPF = gestor.CPF != null ? gestor.CPF : "000.000.000-00";
                        string comentario = txtComentario.Text;

                        registrarHistorico(idPrefeitura, idPerfil, idSituacaoQuadro, nome, CPF, comentario, 0, 0, DateTime.Now, exercicio);
                        carregarHistorico(idPrefeitura, exercicio);
                    }

                    AlterarSituacaoQuadro(3);
                }
                catch (Exception ex)
                {
                    msg = ex.Message;

                }
                if (String.IsNullOrEmpty(msg))
                {
                    msg = "O Resumo da execução financeira dos recursos estaduais foi finalizado com sucesso !";
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
            else
            {

                comentarioVazio(mensagem: "Por favor, preencha o campo comentário.");
            }
        }

        protected void btnSalvarCMAS_Click(object sender, EventArgs e)
        {
            if (txtComentarioCMAS.Text.Length >= 1)
            {

                int exercicio = Convert.ToInt32(hdfAno.Value);
                String msg = String.Empty;

                try
                {

                    if (rblDeliberacaoCMAS.SelectedValue != "4")
                    {
                        string validacao = validaCMAS();

                        if (!String.IsNullOrEmpty(validacao))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(validacao), true);
                            lblInconsistencias.Text = validacao;
                            tbInconsistencias.Visible = true;
                            return;
                        }
                    }

                    using (var proxy = new ProxyPrefeitura())
                    {

                        var prefeituras = new Prefeituras(proxy);
                        var presidente = prefeituras.GetConselhoMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                        var presidenteAtual = presidente.IdUsuarioPresidente;
                        if (rblDeliberacaoCMAS.SelectedValue == "4")
                        {
                            btnDevolverCMAS.Enabled = true;
                            btnFinalizarCMAS.Enabled = false;
                        }

                        else
                        {
                            btnDevolverCMAS.Enabled = false;

                            if (SessaoPmas.UsuarioLogado.IdUsuario == presidenteAtual)
                            {
                                btnFinalizarCMAS.Enabled = true;
                            }
                            else
                            {
                                btnFinalizarCMAS.Enabled = false;
                            }
                        }

                        QuestionarioCMAS();
                        ComentarioCMAS();
                        DeliberacaoCMAS();
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    msg = "Preenchimento salvos com sucesso!";
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
            else
            {
                comentarioVazio(mensagem: "Por favor, preencha o campo comentário CMAS.");
            }

        }

        protected void btnDevolverCMAS_Click(object sender, EventArgs e)
        {

            if (txtComentarioCMAS.Text.Length >= 1)
            {

                int exercicio = Convert.ToInt32(hdfAno.Value);
                String msg = String.Empty;

                try
                {

                    using (var proxy = new ProxyUsuarioPMAS())
                    {
                        int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                        int idPerfil = Convert.ToInt32(SessaoPmas.UsuarioLogado.EnumPerfil);
                        int idUsuario = SessaoPmas.UsuarioLogado.IdUsuario;
                        int idSituacaoQuadro = 6;
                        int ResultadoDeliberacao = Convert.ToInt32(rblDeliberacaoCMAS.SelectedValue);

                        var usuario = new Usuarios();
                        var cmas = usuario.GetUsuarioById(idUsuario, proxy);

                        var nome = cmas.Nome != null ? cmas.Nome : "Nome não cadastrado.";
                        var CPF = cmas.CPF != null ? cmas.CPF : "000.000.000-00";
                        string comentario = txtComentarioCMAS.Text;

                        QuestionarioCMAS();
                        ComentarioCMAS();
                        DeliberacaoCMAS();

                        AlterarSituacaoQuadro(idSituacaoQuadro);

                        registrarHistorico(idPrefeitura, idPerfil, idSituacaoQuadro, nome, CPF, comentario, ResultadoDeliberacao, 0, DateTime.Now, exercicio);
                        carregarHistorico(idPrefeitura, exercicio);
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    msg = "Devolução registrada com sucesso !";
                    lblInconsistencias.Text = "";
                    tbInconsistencias.Visible = false;
                    var script = Util.GetJavaScriptDialogOK(msg);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
                lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");

            }
            else
            {
                comentarioVazio(mensagem: "Por favor, preencha o campo comentário CMAS.");
            }

        }

        protected void btnFinalizarCMAS_Click(object sender, EventArgs e)
        {

            if (txtComentarioCMAS.Text.Length >= 1)
            {
                int exercicio = Convert.ToInt32(hdfAno.Value);
                String msg = String.Empty;

                try
                {
                    using (var proxy = new ProxyUsuarioPMAS())
                    {

                        int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                        int idPerfil = Convert.ToInt32(SessaoPmas.UsuarioLogado.EnumPerfil);
                        int idUsuario = SessaoPmas.UsuarioLogado.IdUsuario;
                        int idSituacaoQuadro;
                        int resultadoDeliberacao;

                        var usuario = new Usuarios();
                        var cmas = usuario.GetUsuarioById(idUsuario, proxy);

                        var nome = cmas.Nome != null ? cmas.Nome : "Nome não cadastrado.";
                        var CPF = cmas.CPF != null ? cmas.CPF : "000.000.000-00";

                        string comentario = txtComentarioCMAS.Text;

                        if (rblDeliberacaoCMAS.SelectedValue == "3")
                        {
                            idSituacaoQuadro = 10;
                            resultadoDeliberacao = Convert.ToInt32(rblDeliberacaoCMAS.SelectedValue);
                        }
                        else
                        {
                            idSituacaoQuadro = 4;
                            resultadoDeliberacao = Convert.ToInt32(rblDeliberacaoCMAS.SelectedValue);
                        }

                        QuestionarioCMAS();
                        ComentarioCMAS();
                        DeliberacaoCMAS();
                        AlterarSituacaoQuadro(idSituacaoQuadro);

                        registrarHistorico(idPrefeitura, idPerfil, idSituacaoQuadro, nome, CPF, comentario, resultadoDeliberacao, 0, DateTime.Now, exercicio);
                        carregarHistorico(idPrefeitura, exercicio);
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    msg = "Preenchimento Finalizado com Sucesso!";
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
            else
            {
                comentarioVazio(mensagem: "Por favor, preencha o campo comentário CMAS.");
            }
        }

        protected void btnSalvarDRADS_Click(object sender, EventArgs e)
        {

            int exercicio = Convert.ToInt32(hdfAno.Value);

            String msg = String.Empty;

            try
            {


                if (rblDeliberacaoDRADS.SelectedValue != "3")
                {
                    string validacao = validarDRADS();

                    if (!String.IsNullOrEmpty(validacao))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(validacao), true);
                        lblInconsistencias.Text = validacao;
                        tbInconsistencias.Visible = true;
                        return;
                    }
                }

                QuestionarioDRADS();
                ComentarioDRADS();
                DeliberacaoDRADS();

                if (rblDeliberacaoDRADS.SelectedValue == "3")
                {
                    btnFinalizarDrads.Enabled = false;
                    btnDevolverDRADS.Enabled = true;
                    chkDeAcordo.Enabled = false;
                }
                else
                {
                    btnFinalizarDrads.Enabled = true;
                    btnDevolverDRADS.Enabled = false;
                    chkDeAcordo.Enabled = true;
                }


            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Preencimento salvo com sucesso !";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");

        }

        protected void btnDevolverDRADS_Click(object sender, EventArgs e)
        {
            if (txtComentarioDRADS.Text.Length >= 1)
            {

                int exercicio = Convert.ToInt32(hdfAno.Value);
                String msg = String.Empty;

                try
                {
                    using (var proxy = new ProxyUsuarioPMAS())
                    {
                        int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                        int idPerfil = Convert.ToInt32(SessaoPmas.UsuarioLogado.EnumPerfil);
                        int idUsuario = SessaoPmas.UsuarioLogado.IdUsuario;
                        int idSituacaoQuadro = 5;
                        int resultadoDeliberacao = Convert.ToInt32(rblDeliberacaoDRADS.SelectedValue);

                        var usuario = new Usuarios();

                        var drads = usuario.GetUsuarioById(idUsuario, proxy);

                        var nome = drads.Nome != null ? drads.Nome : "Nome não cadastrado.";
                        var CPF = drads.CPF != null ? drads.CPF : "000.000.000-00";
                        string comentario = txtComentarioDRADS.Text;

                        QuestionarioDRADS();
                        ComentarioDRADS();
                        DeliberacaoDRADS();


                        registrarHistorico(idPrefeitura, idPerfil, idSituacaoQuadro, nome, CPF, comentario, resultadoDeliberacao, 0, DateTime.Now, exercicio);
                        carregarHistorico(idPrefeitura, exercicio);

                        AlterarSituacaoQuadro(idSituacaoQuadro);
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    msg = "Devolução registrada com sucesso !";
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
            else
            {
                comentarioVazio(mensagem: "Por favor, preencha o campo comentário DRADS.");
            }
        }

        protected void btnFinalizarDrads_Click(object sender, EventArgs e)
        {
            if (txtComentarioDRADS.Text.Length >= 1)
            {

                int exercicio = Convert.ToInt32(hdfAno.Value);
                String msg = String.Empty;

                try
                {
                    using (var proxy = new ProxyUsuarioPMAS())
                    {
                        int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                        int idPerfil = Convert.ToInt32(SessaoPmas.UsuarioLogado.EnumPerfil);
                        int idUsuario = SessaoPmas.UsuarioLogado.IdUsuario;
                        int resultadoDeliberacao = Convert.ToInt32(rblDeliberacaoDRADS.SelectedValue);
                        int deAcordo = chkDeAcordo.Checked == true ? Convert.ToInt32(chkDeAcordo.Checked) : 0;
                        int idSituacaoQuadro;

                        var usuario = new Usuarios();
                        var drads = usuario.GetUsuarioById(idUsuario, proxy);

                        var nome = drads.Nome != null ? drads.Nome : "Nome não cadastrado.";
                        var CPF = drads.CPF != null ? drads.CPF : "000.000.000-00";
                        string comentario = txtComentarioDRADS.Text;

                        if (resultadoDeliberacao == 2)
                        {
                            idSituacaoQuadro = 9;
                        }
                        else
                        {
                            idSituacaoQuadro = 8;
                        }


                        registrarHistorico(idPrefeitura, idPerfil, idSituacaoQuadro, nome, CPF, comentario, resultadoDeliberacao, deAcordo, DateTime.Now, exercicio);
                        carregarHistorico(idPrefeitura, exercicio);

                        AlterarSituacaoQuadro(idSituacaoQuadro);
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    msg = " Preenchimento finalizado com sucesso !";
                    lblInconsistencias.Text = "";
                    tbInconsistencias.Visible = false;
                    var script = Util.GetJavaScriptDialogOK(msg);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    return;
                }

            }
            else
            {
                comentarioVazio(mensagem: "Por favor, preencha o campo comentário DRADS.");
            }

        }

        protected void rblDeliberacaoCMAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblDeliberacaoCMAS.SelectedValue == "4")
            {
                btnDevolverCMAS.Enabled = true;
                btnFinalizar.Enabled = false;
            }
            else
            {
                btnDevolverCMAS.Enabled = false;


                btnFinalizar.Enabled = true;
            }

        }

        protected void rblQuestaoSeisCMAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQuestaoSeisCMAS.SelectedValue == "2")
            {
                controleQuestaoSeisCMAS(true);
            }
            else
            {
                controleQuestaoSeisCMAS(false);
            }
        }

        protected void rblQuestaoSeteCMAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQuestaoSeteCMAS.SelectedValue == "1")
            {
                controleQuestaoSeteCMAS(true);
            }
            else
            {
                controleQuestaoSeteCMAS(false);
            }
        }

        protected void rblDeliberacaoDRADS_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rblDeliberacaoDRADS.SelectedValue == "3")
            {
                btnDevolverDRADS.Enabled = true;
            }
            else
            {
                btnDevolverDRADS.Enabled = false;
            }

        }

        protected void rblQuestaoUmDRADS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQuestaoUmDRADS.SelectedValue == "1")
            {
                controleQuestaoUmDRADS(true);
            }
            else
            {
                controleQuestaoUmDRADS(false);
            }

        }

        protected void rblQuestaoCincoDRADS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQuestaoCincoDRADS.SelectedValue == "2")
            {
                controleQuestaoCincoDRADS(true);
            }
            else
            {
                controleQuestaoCincoDRADS(false);
            }
        }

        public Processos.Prefeituras gestor { get; set; }

        public void comentarioVazio(string mensagem)
        {
            var msg = mensagem;
            lblInconsistencias.Text = "";
            tbInconsistencias.Visible = false;
            var script = Util.GetJavascriptDialogError(msg);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }

    }
}