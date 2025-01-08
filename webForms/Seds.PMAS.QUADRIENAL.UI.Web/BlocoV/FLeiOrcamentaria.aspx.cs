using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoV
{
    public partial class FLeiOrcamentaria : System.Web.UI.Page
    {
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };

        protected void Page_Load(object sender, EventArgs e)
        {
            this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? FLeiOrcamentaria.Exercicios[0].ToString() : this.hdfAno.Value;
            var exercicio = Convert.ToInt32(hdfAno.Value);

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);
                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                LoadExercicios();

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    this.CarregarLeiOrcamentaria(prefeituras, exercicio);
                    this.CarregarIndice(proxy);

                    this.load(prefeituras);

                    this.AplicarRegraBloqueioADMMaiorFluxoPMAS(proxy);
                }

                this.AdicionarEventos();
                this.VerificarAlteracoes();

            }
            else
            {
                txtTotalRecursos.Text = (Convert.ToDecimal(txtAquisicaoBens.Text)
                            + Convert.ToDecimal(txtConstrucaoUnidades.Text)
                            + Convert.ToDecimal(txtManutencaoEquipamentos.Text)
                            + Convert.ToDecimal(txtRecursosHumanos.Text)).ToString("N2");
            }



        }

        private void load(Prefeituras prefeituras)
        {
            int exercicio = Convert.ToInt32(this.hdfAno.Value);
            CarregarLabelsPorExercicio(exercicio);
        }

        private void AplicarRegraBloqueioADMMaiorFluxoPMAS(ProxyPrefeitura proxy)
        {
            var controles = this.ObterControlesOrgaoGestor();

            var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == Convert.ToInt32(hdfAno.Value)).FirstOrDefault();
            
            bool desbloqueadosControles = Permissao.BlocoV.VerificaPermissaoExercicioLOFLuxoAdministrativoBlocoVQuadro(controles, null,quadro.IdSituacaoQuadro);

            if (desbloqueadosControles || SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador )
            {
              AtualizarCamposEBotoesLO(proxy);
            }

        }

        private WebControl[] ObterControlesOrgaoGestor()
        {
            WebControl[] webControles = new WebControl[]{
            
                  txtValorAprovadoLei
                , txtLei
                
                , txtRecursosHumanos
                , txtAquisicaoBens
                , txtConstrucaoUnidades
                , txtManutencaoEquipamentos
                , txtVeiculoComunicacao
                , txtValorRecursosFMAS
                , txtValorRecursoNaoAlocadosFMAS
                , txtConstrucaoUnidades
                , txtComentario
                , btnSalvar
            };

            webControles = webControles.Union(txtDataLei.Controles).ToArray();

            return webControles;

        }

        //private WebControl[] obterControles()
        //{



        //    WebControl[] controles = 
        //    {
        //            txtValorAprovadoLei
        //        ,
        //            txtLei
        //        ,
        //            txtVeiculoComunicacao
        //        ,
        //            txtValorRecursosFMAS
        //        ,
        //            txtValorRecursoNaoAlocadosFMAS
        //        ,
        //            txtComentario
        //        ,
        //            txtRecursosHumanos
        //        , 
        //            txtManutencaoEquipamentos
        //        , 
        //            txtConstrucaoUnidades
        //        , 
        //            txtAquisicaoBens
        //    };

        //    return controles;
        //}

        #region Bloqueio|Desbloqueio [Orgão Gestor]
        private void RegraBloqueioDesbloqueioCamposPerfilOrgaoGestor()
        {
            //Obs.: Desbloqueia tudo
            var ehOrgaoGestor = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

            txtValorAprovadoLei.ReadOnly = !ehOrgaoGestor;
            txtLei.ReadOnly = !ehOrgaoGestor;
            txtDataLei.ReadOnly = !ehOrgaoGestor;
            txtRecursosHumanos.ReadOnly = !ehOrgaoGestor;
            txtAquisicaoBens.ReadOnly = !ehOrgaoGestor;
            txtConstrucaoUnidades.ReadOnly = !ehOrgaoGestor;
            txtManutencaoEquipamentos.ReadOnly = !ehOrgaoGestor;
            txtVeiculoComunicacao.ReadOnly = !ehOrgaoGestor;
            txtValorRecursosFMAS.ReadOnly = !ehOrgaoGestor;
            txtValorRecursoNaoAlocadosFMAS.ReadOnly = !ehOrgaoGestor;
            txtConstrucaoUnidades.ReadOnly = !ehOrgaoGestor;
            txtComentario.ReadOnly = !ehOrgaoGestor;

            txtValorAprovadoLei.Enabled = ehOrgaoGestor;
            txtLei.Enabled = ehOrgaoGestor;
            txtDataLei.Enabled = ehOrgaoGestor;
            txtRecursosHumanos.Enabled = ehOrgaoGestor;
            txtAquisicaoBens.Enabled = ehOrgaoGestor;
            txtManutencaoEquipamentos.Enabled = ehOrgaoGestor;
            txtVeiculoComunicacao.Enabled = ehOrgaoGestor;
            txtValorRecursosFMAS.Enabled = ehOrgaoGestor;
            txtValorRecursoNaoAlocadosFMAS.Enabled = ehOrgaoGestor;
            txtConstrucaoUnidades.Enabled = ehOrgaoGestor;
            txtComentario.Enabled = ehOrgaoGestor;

            txtComentario.Visible = ehOrgaoGestor;

        }
        private void RegraBloqueioDesbloqueioBotoesPerfilOrgaoGestor()
        {
            //Botoes que permitem salvar e finalizar Calculo
            trFinalizarCalculo.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
            btnFinalizarCalculo.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
            btnFinalizarCalculo.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

            btnSalvar.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
            btnSalvar.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

            //Botao de Controle status do quadro
            btnCancelarAprovacao.Visible = false;
            btnSalvarAprovacaoCMAS.Visible = false;
            btnSalvarAprovacaoDRADS.Visible = false;

            btnCancelarAprovacao.Enabled = false;
            btnSalvarAprovacaoCMAS.Enabled = false;
            btnSalvarAprovacaoDRADS.Enabled = false;
        } 
        #endregion

        #region Bloqueio|Desbloqueio [Orgão DRADS]
        private void RegraBloqueioDesbloqueioCamposPerfilDRADS()
        {
            #region [bloqueio] [desbloqueio] > controles
            txtValorAprovadoLei.ReadOnly = true;
            txtLei.ReadOnly = true;
            txtDataLei.ReadOnly = true;
            txtRecursosHumanos.ReadOnly = true;
            txtAquisicaoBens.ReadOnly = true;
            txtConstrucaoUnidades.ReadOnly = true;
            txtManutencaoEquipamentos.ReadOnly = true;
            txtVeiculoComunicacao.ReadOnly = true;
            txtValorRecursosFMAS.ReadOnly = true;
            txtValorRecursoNaoAlocadosFMAS.ReadOnly = true;
            txtConstrucaoUnidades.ReadOnly = true;
            txtComentario.ReadOnly = true;

            txtValorAprovadoLei.Enabled = false;
            txtLei.Enabled = false;
            txtDataLei.Enabled = false;
            txtRecursosHumanos.Enabled = false;
            txtAquisicaoBens.Enabled = false;
            txtManutencaoEquipamentos.Enabled = false;
            txtVeiculoComunicacao.Enabled = false;
            txtValorRecursosFMAS.Enabled = false;
            txtValorRecursoNaoAlocadosFMAS.Enabled = false;
            txtConstrucaoUnidades.Enabled = false;
            txtComentario.Enabled = false;
            txtComentario.Visible = true;
            #endregion
        }
        private void RegraBloqueioDesbloqueioBotoesPerfilDRADS()
        {
            trAprovacaoRecursos.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador);
            trAprovacaoDRADS.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador);
            btnSalvarAprovacaoDRADS.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador); 
            btnSalvarAprovacaoDRADS.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador); 

            //Botoes que permitem salvar e finalizar Calculo
            btnFinalizarCalculo.Enabled = false;
            btnFinalizarCalculo.Visible = false;

            btnSalvar.Visible = false;
            btnSalvar.Enabled = false;

            //Botao de Controle status do quadro
            btnCancelarAprovacao.Visible = false;
            btnCancelarAprovacao.Enabled = false;

            btnSalvarAprovacaoCMAS.Visible = false;
            btnSalvarAprovacaoCMAS.Enabled = false;
        }
        #endregion

        #region Bloqueio|Desbloqueio [Orgão CMAS]
        private void RegraBloqueioDesbloqueioCamposPerfilCMAS()
        {
            #region [bloqueio] [desbloqueio] > controles
            txtValorAprovadoLei.ReadOnly = true;
            txtLei.ReadOnly = true;
            txtDataLei.ReadOnly = true;
            txtRecursosHumanos.ReadOnly = true;
            txtAquisicaoBens.ReadOnly = true;
            txtConstrucaoUnidades.ReadOnly = true;
            txtManutencaoEquipamentos.ReadOnly = true;
            txtVeiculoComunicacao.ReadOnly = true;
            txtValorRecursosFMAS.ReadOnly = true;
            txtValorRecursoNaoAlocadosFMAS.ReadOnly = true;
            txtConstrucaoUnidades.ReadOnly = true;
            txtComentario.ReadOnly = true;

            txtValorAprovadoLei.Enabled = false;
            txtLei.Enabled = false;
            txtDataLei.Enabled = false;
            txtRecursosHumanos.Enabled = false;
            txtAquisicaoBens.Enabled = false;
            txtManutencaoEquipamentos.Enabled = false;
            txtVeiculoComunicacao.Enabled = false;
            txtValorRecursosFMAS.Enabled = false;
            txtValorRecursoNaoAlocadosFMAS.Enabled = false;
            txtConstrucaoUnidades.Enabled = false;
            txtComentario.Enabled = false;
            txtComentario.Visible = true;
            #endregion
        }
        private void RegraBloqueioDesbloqueioBotoesPerfilCMAS()
        {
            trAprovacaoCMAS.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);
            btnSalvarAprovacaoCMAS.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);
            btnSalvarAprovacaoCMAS.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);

            //Botoes que permitem salvar e finalizar Calculo
            btnFinalizarCalculo.Enabled = false;
            btnFinalizarCalculo.Visible = false;

            btnSalvar.Visible = false;
            btnSalvar.Enabled = false;

            //Botao de Controle status do quadro
            btnCancelarAprovacao.Visible = false;
            btnCancelarAprovacao.Enabled = false;

            btnSalvarAprovacaoDRADS.Visible = false;
            btnSalvarAprovacaoDRADS.Enabled = false;
        }
        #endregion

        #region Bloqueio|Desbloqueio [Orgão DRADS]
        private void RegraBloqueioDesbloqueioCamposPerfilADM()
        {
            #region [bloqueio] [desbloqueio] > controles
            txtValorAprovadoLei.ReadOnly = true;
            txtLei.ReadOnly = true;
            txtDataLei.ReadOnly = true;
            txtRecursosHumanos.ReadOnly = true;
            txtAquisicaoBens.ReadOnly = true;
            txtConstrucaoUnidades.ReadOnly = true;
            txtManutencaoEquipamentos.ReadOnly = true;
            txtVeiculoComunicacao.ReadOnly = true;
            txtValorRecursosFMAS.ReadOnly = true;
            txtValorRecursoNaoAlocadosFMAS.ReadOnly = true;
            txtConstrucaoUnidades.ReadOnly = true;
            txtComentario.ReadOnly = true;

            txtValorAprovadoLei.Enabled = false;
            txtLei.Enabled = false;
            txtDataLei.Enabled = false;
            txtRecursosHumanos.Enabled = false;
            txtAquisicaoBens.Enabled = false;
            txtManutencaoEquipamentos.Enabled = false;
            txtVeiculoComunicacao.Enabled = false;
            txtValorRecursosFMAS.Enabled = false;
            txtValorRecursoNaoAlocadosFMAS.Enabled = false;
            txtConstrucaoUnidades.Enabled = false;
            txtComentario.Enabled = false;
            txtComentario.Visible = true;
            #endregion
        }
        private void RegraBloqueioDesbloqueioBotoesPerfilADM()
        {
            trCancelarAprovacao.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);
            btnCancelarAprovacao.Visible = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);
            btnCancelarAprovacao.Enabled = (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador);

            //Botoes que permitem salvar e finalizar Calculo
            btnFinalizarCalculo.Enabled = false;
            btnFinalizarCalculo.Visible = false;

            btnSalvar.Visible = false;
            btnSalvar.Enabled = false;

            //Botao de Controle status do quadro
            btnSalvarAprovacaoDRADS.Visible = false;
            btnSalvarAprovacaoDRADS.Enabled = false;

            btnSalvarAprovacaoCMAS.Visible = false;
            btnSalvarAprovacaoCMAS.Enabled = false;
        }
        #endregion

        #region Bloqueio|Desbloqueio|Auxiliares
        private void RegraBloquearCamposPorAusenciaDeUsuarioGestorDoPlano()
        {

            bool usuarioEhOGestorDoPlano = true;
            #region [Quadro sem Gestor || Quadro Usuario != Gestor]
            var gestor = new Prefeituras(new ProxyPrefeitura()).GetAtualGestorMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (gestor == null)
            {
                usuarioEhOGestorDoPlano = false;
            }
            else
            {
                if (SessaoPmas.UsuarioLogado.IdUsuario != gestor.IdUsuarioGestor)
                {
                    usuarioEhOGestorDoPlano = false;
                }
            }
            #endregion
            if (!usuarioEhOGestorDoPlano)
            {
                txtValorAprovadoLei.ReadOnly = true;
                txtLei.ReadOnly = true;
                txtDataLei.ReadOnly = true;
                txtRecursosHumanos.ReadOnly = true;
                txtAquisicaoBens.ReadOnly = true;
                txtConstrucaoUnidades.ReadOnly = true;
                txtManutencaoEquipamentos.ReadOnly = true;
                txtVeiculoComunicacao.ReadOnly = true;
                txtValorRecursosFMAS.ReadOnly = true;
                txtValorRecursoNaoAlocadosFMAS.ReadOnly = true;
                txtConstrucaoUnidades.ReadOnly = true;
                txtComentario.ReadOnly = true;
                txtComentario.Visible = true;

                txtValorAprovadoLei.Enabled = false;
                txtLei.Enabled = false;
                txtDataLei.Enabled = false;
                txtRecursosHumanos.Enabled = false;
                txtAquisicaoBens.Enabled = false;
                txtManutencaoEquipamentos.Enabled = false;
                txtVeiculoComunicacao.Enabled = false;
                txtValorRecursosFMAS.Enabled = false;
                txtValorRecursoNaoAlocadosFMAS.Enabled = false;
                txtConstrucaoUnidades.Enabled = false;
                txtComentario.Enabled = false;
                btnSalvar.Enabled = false;
                btnSalvar.Visible = false;
            }
        }
        #endregion 

        #region Fonte Financiamento - exercicio
        private void LoadExercicios()
        {
            this.btnExercicio1.Text = FLeiOrcamentaria.Exercicios[0].ToString();
            this.btnExercicio2.Text = FLeiOrcamentaria.Exercicios[1].ToString();
            this.btnExercicio3.Text = FLeiOrcamentaria.Exercicios[2].ToString();
            this.btnExercicio4.Text = FLeiOrcamentaria.Exercicios[3].ToString();

            if (FLeiOrcamentaria.Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                lgndLom.InnerText = string.Format("  Lei Orçamentária Municipal - valor aprovado para a Assistência Social para o ano de {0}:", Exercicios[0]);
                this.btnExercicio1.CssClass = "btn btn-info";
                this.btnExercicio2.CssClass = "btn btn-primary";
                this.btnExercicio3.CssClass = "btn btn-primary";
                this.btnExercicio4.CssClass = "btn btn-primary";

            }
            if (FLeiOrcamentaria.Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                lgndLom.InnerText = string.Format("  Lei Orçamentária Municipal - valor aprovado para a Assistência Social para o ano de {0}:", Exercicios[1]);
                this.btnExercicio1.CssClass = "btn btn-primary";
                this.btnExercicio2.CssClass = "btn btn-info";
                this.btnExercicio3.CssClass = "btn btn-primary";
                this.btnExercicio4.CssClass = "btn btn-primary";
            }

            if (FLeiOrcamentaria.Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                lgndLom.InnerText = string.Format("  Lei Orçamentária Municipal - valor aprovado para a Assistência Social para o ano de {0}:", Exercicios[2]);
                this.btnExercicio1.CssClass = "btn btn-primary";
                this.btnExercicio2.CssClass = "btn btn-primary";
                this.btnExercicio3.CssClass = "btn btn-info";
                this.btnExercicio4.CssClass = "btn btn-primary";
            }

            if (FLeiOrcamentaria.Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                lgndLom.InnerText = string.Format("  Lei Orçamentária Municipal - valor aprovado para a Assistência Social para o ano de {0}:", Exercicios[3]);
                this.btnExercicio1.CssClass = "btn btn-primary";
                this.btnExercicio2.CssClass = "btn btn-primary";
                this.btnExercicio3.CssClass = "btn btn-primary";
                this.btnExercicio4.CssClass = "btn btn-info";
            }

        }

        #endregion


        void AdicionarEventos()
        {
            if (!txtValorRecursosFMAS.ReadOnly)
            {
                txtValorRecursosFMAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            }

            if (!txtValorRecursoNaoAlocadosFMAS.ReadOnly)
            {
                txtValorRecursoNaoAlocadosFMAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            }

            if (!txtValorAprovadoLei.ReadOnly)
            {
                txtValorAprovadoLei.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            }

            if (!txtRecursosHumanos.ReadOnly)
            {
                txtRecursosHumanos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

                txtRecursosHumanos.Attributes.Add("onblur", "CalculateTotal()");
            }

            if (!txtManutencaoEquipamentos.ReadOnly)
            {
                txtManutencaoEquipamentos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtManutencaoEquipamentos.Attributes.Add("onblur", "CalculateTotal()");
            }

            if (!txtConstrucaoUnidades.ReadOnly)
            {
                txtConstrucaoUnidades.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtConstrucaoUnidades.Attributes.Add("onblur", "CalculateTotal()");
            }

            if (!txtAquisicaoBens.ReadOnly)
            {
                txtAquisicaoBens.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtAquisicaoBens.Attributes.Add("onblur", "CalculateTotal()");
            }
        }

        protected void btnLoadExercicio1_Click(object sender, EventArgs e)
        {
            lgndLom.InnerText = string.Format("  Lei Orçamentária Municipal - valor aprovado para a Assistência Social para o ano de {0}:", Exercicios[0]);
            hdfAno.Value = btnExercicio1.Text;
            #region reload
            Clear();

            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                CarregarLeiOrcamentaria(prefeituras, Convert.ToInt32(hdfAno.Value));
                CarregarIndice(proxy);
                load(prefeituras);

                AplicarRegraBloqueioADMMaiorFluxoPMAS(proxy);
            }

            AdicionarEventos();
            VerificarAlteracoes();
            #endregion
        }

        protected void btnLoadExercicio2_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio2.Text;
            lgndLom.InnerText = string.Format("  Lei Orçamentária Municipal - valor aprovado para a Assistência Social para o ano de {0}:", Exercicios[1]); 

            #region reload
            Clear();

            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                CarregarLeiOrcamentaria(prefeituras, Convert.ToInt32(hdfAno.Value));
                CarregarIndice(proxy);
                load(prefeituras);
                
                AplicarRegraBloqueioADMMaiorFluxoPMAS(proxy);

            }

            AdicionarEventos();
            VerificarAlteracoes();
            #endregion
        }

        protected void btnLoadExercicio3_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio3.Text;
            lgndLom.InnerText = string.Format("  Lei Orçamentária Municipal - valor aprovado para a Assistência Social para o ano de {0}:", Exercicios[2]);

            #region reload
            Clear();

            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                CarregarLeiOrcamentaria(prefeituras, Convert.ToInt32(hdfAno.Value));
                CarregarIndice(proxy);
                load(prefeituras);

                AplicarRegraBloqueioADMMaiorFluxoPMAS(proxy);

            }

            AdicionarEventos();
            VerificarAlteracoes();
            #endregion
        }

        protected void btnLoadExercicio4_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio4.Text;
            lgndLom.InnerText = string.Format("  Lei Orçamentária Municipal - valor aprovado para a Assistência Social para o ano de {0}:", Exercicios[3]);

            #region reload
            Clear();

            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                CarregarLeiOrcamentaria(prefeituras, Convert.ToInt32(hdfAno.Value));
                CarregarIndice(proxy);
                load(prefeituras);

                AplicarRegraBloqueioADMMaiorFluxoPMAS(proxy);

            }

            AdicionarEventos();
            VerificarAlteracoes();
            #endregion
        }
        
        void CarregarLeiOrcamentaria(Prefeituras prefeituras, Int32 exercicio)
        {
            var lei = prefeituras.GetLeiOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
            if (lei != null)
            {
                txtLei.Text = lei.Lei;
                txtValorAprovadoLei.Text = lei.ValorAprovado.ToString("N2");
                txtDataLei.Text = lei.DataPublicacao.ToShortDateString();

                //rblOutrosRecursos.SelectedValue = lei.OutrosRecursos.HasValue && lei.OutrosRecursos.Value ? "1" : "0";
                //rblOutrosRecursos_SelectedIndexChanged(null, null);
                txtValorRecursosFMAS.Text = lei.ValorRecursosFMAS.HasValue ? lei.ValorRecursosFMAS.Value.ToString("N2") : (0M).ToString();
                txtVeiculoComunicacao.Text = lei.NomeVeiculoComunicacao;
                trOutrosRecursos.Visible = lei.ValorRecursosFMAS.HasValue;
                txtValorRecursoNaoAlocadosFMAS.Text = lei.ValorRecursosNaoAlocadosFMAS.HasValue ? lei.ValorRecursosNaoAlocadosFMAS.Value.ToString("N2") : (0M).ToString("N2");

                txtRecursosHumanos.Text = lei.ValorRecursosHumanos.HasValue ? lei.ValorRecursosHumanos.Value.ToString("N2") : (0M).ToString("N2");
                txtManutencaoEquipamentos.Text = lei.ValorManutencaoEquipamentos.HasValue ? lei.ValorManutencaoEquipamentos.Value.ToString("N2") : (0M).ToString("N2");
                txtConstrucaoUnidades.Text = lei.ValorConstrucaoUnidades.HasValue ? lei.ValorConstrucaoUnidades.Value.ToString("N2") : (0M).ToString("N2");
                txtAquisicaoBens.Text = lei.ValorAquisicaoBens.HasValue ? lei.ValorAquisicaoBens.Value.ToString("N2") : (0M).ToString("N2");

                txtTotalRecursos.Text = (Convert.ToDecimal(txtAquisicaoBens.Text)
                                        + Convert.ToDecimal(txtConstrucaoUnidades.Text)
                                        + Convert.ToDecimal(txtManutencaoEquipamentos.Text)
                                        + Convert.ToDecimal(txtRecursosHumanos.Text)).ToString("N2");

                txtValorRecursnoNaoAlocadosFMAS_TextChanged(null, null);
                

            }
            else
            {
                ClearFmas();
            }

        }
        
        void CarregarIndice(ProxyPrefeitura proxy)
        {
            var obj = proxy.Service.GetIndiceGestaoDescentralizadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, Convert.ToInt32(hdfAno.Value));
            if (obj == null)
                return;
            if (!String.IsNullOrEmpty(obj.ComentariosLeiOrcamentaria))
                txtComentario.Text = obj.ComentariosLeiOrcamentaria;
        }

        void VerificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 9);
                    linkAlteracoesQuadro.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("9"));
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!btnSalvarLeiOrcamentaria_Click())
            {
                return;
            }
            String msg = String.Empty;
            // var script = Util.GetJavaScriptDialogOK();
            SessaoPmas.VerificarSessao(this);
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    var exercicio = Convert.ToInt32(hdfAno.Value);
                    if (txtComentario.Text.Trim().Length > 500)
                        msg = "O texto digitado ultrapassou o limite de 500 caracteres e não poderá ser salvo desta forma. Por favor, reduza-o até o limite de 500 caracteres." + System.Environment.NewLine;

                    var obj = proxy.Service.GetIndiceGestaoDescentralizadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                    if (obj != null)
                        if (!String.IsNullOrEmpty(txtComentario.Text))
                            obj.ComentariosLeiOrcamentaria = txtComentario.Text;

                    proxy.Service.SaveIndiceGestaoDescentralizada(obj);

                    load(prefeituras);
                }
            }
            catch (Exception ex)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }
            if (String.IsNullOrEmpty(msg))
            {
                msg = "Lei orçamentária registrada com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK(msg), true);
                return;
            }
        }

        private void CarregarLabelsPorExercicio(int exercicio)
        {
            if (FLeiOrcamentaria.Exercicios[0] == exercicio)
            {
                lblHeader.Text = "5.2.a - Lei Orçamentária Municipal para " + exercicio;
                btnExercicio1.CssClass = "btn-seds btn-info-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";
                //btnSalvar.Enabled = false;
                //txtComentario.Enabled = false;
                tbInconsistencias.Visible = false;
            }
            if (FLeiOrcamentaria.Exercicios[1] == exercicio)
            {
                lblHeader.Text = "5.2.a - Lei Orçamentária Municipal para " + exercicio;
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-info-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";
                //btnSalvar.Enabled = true;
                //txtComentario.Enabled = true;
                tbInconsistencias.Visible = false;
            }

            if (FLeiOrcamentaria.Exercicios[2] == exercicio)
            {
                lblHeader.Text = "5.2.a - Lei Orçamentária Municipal para " + exercicio;
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-info-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";
                //btnSalvar.Enabled = true;
                //txtComentario.Enabled = true;
                tbInconsistencias.Visible = false;
            }

            if (FLeiOrcamentaria.Exercicios[3] == exercicio)
            {
                lblHeader.Text = "5.2.a - Lei Orçamentária Municipal para " + exercicio;
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-info-seds";
                //btnSalvar.Enabled = true;
                //txtComentario.Enabled = true;
                tbInconsistencias.Visible = false;
            }

        }

        [System.Web.Services.WebMethod]
        public static String CalcularValores(String[] valores)
        {
            decimal total = 0M;
            foreach (String val in valores)
            {
                total += Convert.ToDecimal(val);
            }
            return total.ToString("N2");
        }

        private void AtualizarCamposEBotoesLO(ProxyPrefeitura proxy)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);
            var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == exercicio).FirstOrDefault();

            if (quadro != null)
            {
                if (quadro.IdSituacaoQuadro != 7)
                {
                     
                    switch (quadro.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                        case (int)ESituacaoQuadro.DevolvidoDRADS:
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                        default:
                            {
                                RegraBloqueioDesbloqueioCamposPerfilOrgaoGestor();
                                RegraBloqueioDesbloqueioBotoesPerfilOrgaoGestor();
                                RegraBloquearCamposPorAusenciaDeUsuarioGestorDoPlano();

                                break;
                            }
                        case (int)ESituacaoQuadro.Preenchido:
                            {
                                RegraBloqueioDesbloqueioCamposPerfilDRADS();
                                RegraBloqueioDesbloqueioBotoesPerfilDRADS();

                                break;
                            }
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            {
                                RegraBloqueioDesbloqueioCamposPerfilCMAS();
                                RegraBloqueioDesbloqueioBotoesPerfilCMAS();

                                break;
                            }
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            {
                                RegraBloqueioDesbloqueioCamposPerfilADM();
                                RegraBloqueioDesbloqueioBotoesPerfilADM();

                                break;
                            }
                    }
                }
            }
            else {
                RegraBloqueioDesbloqueioCamposPerfilOrgaoGestor();
                RegraBloqueioDesbloqueioBotoesPerfilOrgaoGestor();
                RegraBloquearCamposPorAusenciaDeUsuarioGestorDoPlano();
            
            }


        }

        protected void btnFinalizarCalculo_Click(object sender, EventArgs e)
        {
            AlterarSituacaoQuadro(2);
            AtualizarCamposEBotoesLO(new ProxyPrefeitura());
        }

        protected void btnSalvarAprovacaoDRADS_Click(object sender, EventArgs e)
        {
            AlterarSituacaoQuadro(rblAprovacaoDRADS.SelectedValue == "1" ? 3 : 5);
            AtualizarCamposEBotoesLO(new ProxyPrefeitura());
        }

        protected void btnSalvarAprovacaoCMAS_Click(object sender, EventArgs e)
        {
            AlterarSituacaoQuadro(rblAprovacaoCMAS.SelectedValue == "1" ? 4 : 6);
            AtualizarCamposEBotoesLO(new ProxyPrefeitura());
        }

        protected void btnCancelarAprovacao_Click(object sender, EventArgs e)
        {
            AlterarSituacaoQuadro(1);
            AtualizarCamposEBotoesLO(new ProxyPrefeitura());
            
        }

        void AlterarSituacaoQuadro(int idSituacaoQuadro)
        {
            String msg = String.Empty;
            int exercicio = Convert.ToInt32(hdfAno.Value);
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == exercicio).FirstOrDefault();
                    if (quadro != null)
                    {
                        quadro.IdSituacaoQuadro = idSituacaoQuadro;
                        proxy.Service.SavePrefeituraSituacaoQuadro(quadro);
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
                        msg = "Registro da Lei Orçamentária finalizado com sucesso."; //"Cálculo finalizado com sucesso!";
                        break;
                    case 3:
                        msg = "Quadro de informações sobre a Lei Orçamentária foi encaminhado para deliberação do CMAS!";
                        break;
                    case 4:
                        msg = "Lei Orçamentária aprovada com sucesso!";
                        break;
                    case 5:
                        msg = "Quadro de informações sobre a Lei Orçamentária foi disponibilizado para retificação do preenchimento pelo Órgão Gestor!";
                        break;
                    case 6:
                        msg = "Quadro de informações sobre a Lei Orçamentária foi novamente disponibilizado para retificação do preenchimento pelo Órgão Gestor!";
                        break;
                    default:
                        msg = "Desbloqueio efetuado com sucesso!";
                        break;
                }
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                trAprovacaoRecursos.Visible = trFinalizarCalculo.Visible = trAprovacaoDRADS.Visible = trAprovacaoCMAS.Visible = trCancelarAprovacao.Visible = false;
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }

        protected void btnSalvarIGD_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var obj = new IndiceGestaoDescentralizadaInfo();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                obj.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            //if (!String.IsNullOrEmpty(txtIGDPBF.Text))
            //    obj.IGDPBF = Convert.ToDouble(txtIGDPBF.Text);
            //if (!String.IsNullOrEmpty(txtIGDPBFValorMensal.Text))
            //    obj.IGDPBFValorMensal = Convert.ToDecimal(txtIGDPBFValorMensal.Text);            
            //if (!String.IsNullOrEmpty(txtIGDSUAS.Text))
            //    obj.IGDSUAS = Convert.ToDouble(txtIGDSUAS.Text);
            //if (!String.IsNullOrEmpty(txtIGDSUASValorMensal.Text))
            //    obj.IGDSUASValorMensal = Convert.ToDecimal(txtIGDSUASValorMensal.Text);            

            try
            {
                new ValidadorIndiceGestaoDescentralizada().Validar(obj);

                using (var proxy = new ProxyPrefeitura())
                {
                    proxy.Service.SaveIndiceGestaoDescentralizada(obj);
                    //  loadIndice(proxy);
                }
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            //lblInconsistencias.Text = String.Empty;
            //tbInconsistencias.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Índices de Gestão Descentralizada atualizados com sucesso!"), true);

        }

        protected void txtValorRecursnoNaoAlocadosFMAS_TextChanged(object sender, EventArgs e)
        {
            trOutrosRecursos.Visible = false;

            if (!string.IsNullOrEmpty(txtValorRecursoNaoAlocadosFMAS.Text) && Convert.ToDecimal(txtValorRecursoNaoAlocadosFMAS.Text) > 0)
            {
                trOutrosRecursos.Visible = true;
                txtRecursosHumanos.Enabled = true;
                txtManutencaoEquipamentos.Enabled = true;
                txtConstrucaoUnidades.Enabled = true;
                txtAquisicaoBens.Enabled = true;
            }
            else
            {
                txtRecursosHumanos.Text = "0,00";
                txtManutencaoEquipamentos.Text = "0,00";
                txtConstrucaoUnidades.Text = "0,00";
                txtAquisicaoBens.Text = "0,00";
            }
        }

        protected bool btnSalvarLeiOrcamentaria_Click()
        {
            String msg = String.Empty;
            var script = Util.GetJavaScriptDialogOK(msg);
            //if (String.IsNullOrEmpty(txtVeiculoComunicacao.Text))
            //    msg += "Campo Nome do veículo de comunicação em que foi publicada deve ser preenchido" + System.Environment.NewLine;

            //if (Convert.ToDecimal(txtValorRecursosFMAS.Text) > Convert.ToDecimal(txtTotalFMAS.Text))
            //    msg += "O Valor de recursos destinados a Política de Assistência Social alocados no FMAS não pode ser maior que o valor do campo Total de recursos municipais alocados no FMAS: do quadro Recursos financeiros alocados no FMAS" + System.Environment.NewLine;
            try
            {
                if (string.IsNullOrEmpty(msg))
                {
                    var obj = new LeiOrcamentariaInfo();
                    obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                    if (!String.IsNullOrEmpty(txtValorAprovadoLei.Text))
                        obj.ValorAprovado = Convert.ToDecimal(txtValorAprovadoLei.Text);

                    if (!String.IsNullOrEmpty(txtLei.Text))
                        obj.Lei = txtLei.Text;

                    DateTime dt;
                    if (!String.IsNullOrEmpty(txtDataLei.Text) && DateTime.TryParse(txtDataLei.Text, out dt))
                        obj.DataPublicacao = Convert.ToDateTime(txtDataLei.Text);

                    if (!String.IsNullOrEmpty(txtValorRecursosFMAS.Text))
                        obj.ValorRecursosFMAS = Convert.ToDecimal(txtValorRecursosFMAS.Text);

                    if (!String.IsNullOrEmpty(txtValorRecursoNaoAlocadosFMAS.Text))
                        obj.ValorRecursosNaoAlocadosFMAS = Convert.ToDecimal(txtValorRecursoNaoAlocadosFMAS.Text);

                    if (!String.IsNullOrEmpty(txtRecursosHumanos.Text))
                        obj.ValorRecursosHumanos = Convert.ToDecimal(txtRecursosHumanos.Text);

                    if (!String.IsNullOrEmpty(txtManutencaoEquipamentos.Text))
                        obj.ValorManutencaoEquipamentos = Convert.ToDecimal(txtManutencaoEquipamentos.Text);

                    if (!String.IsNullOrEmpty(txtConstrucaoUnidades.Text))
                        obj.ValorConstrucaoUnidades = Convert.ToDecimal(txtConstrucaoUnidades.Text);

                    if (!String.IsNullOrEmpty(txtAquisicaoBens.Text))
                        obj.ValorAquisicaoBens = Convert.ToDecimal(txtAquisicaoBens.Text);


                    if (!String.IsNullOrEmpty(txtVeiculoComunicacao.Text))
                        obj.NomeVeiculoComunicacao = txtVeiculoComunicacao.Text;

                    if (!String.IsNullOrEmpty(txtComentario.Text))
                        obj.ComentariosOrgaoGestor = txtComentario.Text;
                    //if (!String.IsNullOrEmpty(txtTotalFMAS.Text))
                    //    obj.TotalFMAS = Convert.ToDecimal(txtTotalFMAS.Text);

                    obj.Exercicio = Convert.ToInt32(hdfAno.Value);

                    using (var proxy = new ProxyPrefeitura())
                    {
                        proxy.Service.SaveLeiOrcamentaria(obj);

                        var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == obj.Exercicio).FirstOrDefault();
                        if (quadro == null)
                        {
                            quadro = new PrefeituraSituacaoQuadroInfo()
                            {
                                IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id,
                                IdRecurso = 160,
                                IdSituacaoQuadro = 1
                            };

                            proxy.Service.SavePrefeituraSituacaoQuadro(quadro);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Verifique as inconsistências!";
                script = Util.GetJavascriptDialogError(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return false;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Lei orçamentária PMAS " + Convert.ToInt32(hdfAno.Value) + " registrada com sucesso!";
                lblInconsistencias.Text = "";
                lblInconsistencias.Visible = false;
                tbInconsistencias.Visible = false;
                script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                return true;
            }
            else
            {

                script = Util.GetJavascriptDialogError("Verifique as inconsistências!");
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return false;
            }
        }

        protected void btnSalvarAprovacaoRecursosDRADS_Click(object sender, EventArgs e)
        {

        }

        private void Clear()
        {
            txtValorAprovadoLei.Text = "0,00";
            txtValorRecursosFMAS.Text = "0,00";
            txtValorRecursoNaoAlocadosFMAS.Text = "0,00";
            txtLei.Text = string.Empty;
            txtDataLei.Text = string.Empty;
            txtVeiculoComunicacao.Text = string.Empty;
            txtComentario.Text = string.Empty;

        }
        private void ClearFmas()
        {
            txtLei.Text = string.Empty;
            txtValorAprovadoLei.Text = (0M).ToString();
            txtDataLei.Text = string.Empty;

            //rblOutrosRecursos.SelectedValue = lei.OutrosRecursos.HasValue && lei.OutrosRecursos.Value ? "1" : "0";
            //rblOutrosRecursos_SelectedIndexChanged(null, null);
            txtValorRecursosFMAS.Text = (0M).ToString();
            txtVeiculoComunicacao.Text = string.Empty;
            trOutrosRecursos.Visible = false;
            txtValorRecursoNaoAlocadosFMAS.Text = (0M).ToString();

            txtRecursosHumanos.Text = (0M).ToString();
            txtManutencaoEquipamentos.Text = (0M).ToString();
            txtConstrucaoUnidades.Text = (0M).ToString();
            txtAquisicaoBens.Text = (0M).ToString();

            txtTotalRecursos.Text = (0M).ToString();
            txtValorRecursnoNaoAlocadosFMAS_TextChanged(null, null);
        }

    }
}