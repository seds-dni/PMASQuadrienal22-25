using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Entidades.Acoes;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoVI
{
    public partial class FAcaoPlanejamento : System.Web.UI.Page
    {

        #region Exercicios
        private int PrevisaoDoExercicio = 2022;
        private List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            hdfAno.Value = String.IsNullOrEmpty(hdfAno.Value) ? Exercicios[0].ToString() : hdfAno.Value;
            this.btnExercicio1.CssClass = String.IsNullOrEmpty(hdfAno.Value) ? "btn-seds btn-info-seds" : "btn-seds btn-primary-seds";
            int exercicio = Convert.ToInt32(hdfAno.Value);

            CarregarExercicios();

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                adicionarEventos();
                using (var proxyEstrutura = new ProxyEstruturaAssistenciaSocial())
                {
                    carregarEixos(proxyEstrutura);
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        using (var proxy = new ProxyAcoes())
                        {
                            load(proxy, proxyEstrutura);
                        }
                    }
                }



                #region Bloqueia , Desbloqueia
                /*
                WebControl[] controles = ListarControlesBloqueio();
                Permissao.VerificarPermissaoControles(controles, Session); */
                AplicarBloqueioControlesRecursosFinanceiros(exercicio);
                AplicarBloqueioControlesRecursosFinanceirosReprogramacao(exercicio);
                InibeOuExibereprogramados(exercicio);
                #endregion
            }
        }

        private WebControl[] ListarControlesBloqueio()
        {
            WebControl[] controles = { txtDescricao,
                                             txtNome,
                                             txtOutrosEnvolvidos,
                                             txtObjetivos,
                                             ddlAcao,
                                             ddlEixo,
                                             /*chkFEAS,
                                             chkFMAS,
                                             chkFNAS,
                                             chkIGDPBF,
                                             chkIGDSUAS,
                                             chkOrcamentoEstadual,
                                             chkOrcamentoFederal,
                                             chkOrcamentoMunicipal,
                                             chkOutrosFundosEstaduais,
                                             chkOutrosFundosFederais,
                                             chkOutrosFundosMunicipais,*/
                                             txtEstimativaCusto,  
                                             ddlMesPrevistoInicio,  
                                             ddlAnoPrevistoInicio,  
                                             ddlMesPrevistoTermino,  
                                             ddlAnoPrevistoTermino,  
                                             btnSalvar
                                             };
            return controles;
        }

        private WebControl[] ListarControlesBloqueioRecursos()
        {
            WebControl[] controles = {
                                             chkFEAS,
                                             chkFMAS,
                                             chkFNAS,
                                             chkIGDPBF,
                                             chkIGDSUAS,
                                             chkOrcamentoEstadual,
                                             chkOrcamentoFederal,
                                             chkOrcamentoMunicipal,
                                             chkOutrosFundosEstaduais,
                                             chkOutrosFundosFederais,
                                             chkOutrosFundosMunicipais

                                             };
            return controles;
        }

        private WebControl[] ListarControlesBloqueioRecursosReprogramacao() 
        {
            WebControl[] controles = {
                                         chkFEASReprogramado
                                     };
            
            return controles;
        }

        private void AplicarBloqueioControlesRecursosFinanceirosReprogramacao(int exercicio)
        {
            WebControl[] controles = ListarControlesBloqueioRecursosReprogramacao();

            bool desbloqueio = Permissao.BlocoVI.VerificaPermissaoRedeDiretaBlocoVIReprogramacao(controles, exercicio);

            Session["DesbloqueioReprogramacao"] = desbloqueio;

            verificaDesbloqueioTextBox(desbloqueio);
        }


        private void AplicarBloqueioControlesRecursosFinanceiros(int exercicio)
        {
            WebControl[] controles = ListarControlesBloqueioRecursos();

            bool desbloqueio =  Permissao.BlocoVI.VerificaPermissaoRecursosAcaoPlanejamentoBlocoVI(controles, exercicio);

            Session["Desbloqueio"] = desbloqueio;

            verificaDesbloqueioTextBox(desbloqueio);
        }

        void verificaDesbloqueioTextBox(bool desbloqueio)
        {
            if (chkFMAS.Checked)
            {
                txtFMAS.Enabled = desbloqueio;
            }
            else
            {
                txtFMAS.Enabled = false;
            }

            if (chkOrcamentoMunicipal.Checked)
            {
                txtOrcamentoMunicipal.Enabled = desbloqueio;
            }
            else
            {
                txtOrcamentoMunicipal.Enabled = false;
            }

            if (chkOutrosFundosMunicipais.Checked)
            {
                txtOutrosFundosMunicipais.Enabled = desbloqueio;
            }
            else
            {
                txtOutrosFundosMunicipais.Enabled = false;
            }

            if (chkFEAS.Checked)
            {
                txtFEAS.Enabled = desbloqueio;
            }
            else
            {
                txtFEAS.Enabled = false;
            }

            if (chkFEASReprogramado.Checked)
            {
                txtFEASReprogramado.Enabled = desbloqueio;
            }
            else
            {
                txtFEASReprogramado.Enabled = false;
            }

            if (chkOrcamentoEstadual.Checked)
            {
                txtOrcamentoEstadual.Enabled = desbloqueio;
            }
            else
            {
                txtOrcamentoEstadual.Enabled = false;
            }

            if (chkOutrosFundosEstaduais.Checked)
            {
                txtOutrosFundosEstaduais.Enabled = desbloqueio;
            }
            else
            {
                txtOutrosFundosEstaduais.Enabled = false;
            }

            if (chkFNAS.Checked)
            {
                txtFNAS.Enabled = desbloqueio;
            }
            else
            {
                txtFNAS.Enabled = false;
            }

            if (chkOrcamentoFederal.Checked)
            {
                txtOrcamentoFederal.Enabled = desbloqueio;
            }
            else
            {
                txtOrcamentoFederal.Enabled = false;
            }

            if (chkOutrosFundosFederais.Checked)
            {
                txtOutrosFundosFederais.Enabled = desbloqueio;
            }
            else
            {
                txtOutrosFundosFederais.Enabled = false;
            }

            if (chkIGDSUAS.Checked)
            {
                txtIGDSUAS.Enabled = desbloqueio;
            }
            else
            {
                txtIGDSUAS.Enabled = false;
            }

            if (chkIGDPBF.Checked)
            {
                txtIGDPBF.Enabled = desbloqueio;
            }
            else
            {
                txtIGDPBF.Enabled = false;
            }

        }

        void InibeOuExibereprogramados(int exericicio) 
        {

            if (exericicio == 2024 || exericicio == 2025)
            {
                chkFEASReprogramado.Visible = true;
                txtFEASReprogramado.Visible = true;
            }
            else
            {
                chkFEASReprogramado.Visible = false;
                txtFEASReprogramado.Visible = false;                    
            }

        }

        private void AplicarBloqueioControles(int exercicio)
        {
             WebControl[] controles = ListarControlesBloqueio();

             var enabled = Permissao.VerificarPermissao();

             Permissao.BlocoVI.VerificaPermissaoExercicioAcaoPlanejamentoBlocoVI(controles, enabled);
        }

        void adicionarEventos()
        {
            txtFMAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoMunicipal.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoEstadual.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoFederal.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosMunicipais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosEstaduais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOutrosFundosFederais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDSUAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDPBF.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtEstimativaCusto.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
        }

        void verificarAlteracoes(Int32 idAcao)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro61.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 61, idAcao);
                    linkAlteracoesQuadro61.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("61")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idAcao.ToString()));
                }
            }
        }

        void load(ProxyAcoes proxy, ProxyEstruturaAssistenciaSocial proxyEstrutura)
        {

            hdfAno.Value = String.IsNullOrEmpty(hdfAno.Value) ? Exercicios[0].ToString() : hdfAno.Value;
            int exercicio = Convert.ToInt32(hdfAno.Value);

            this.AplicarBloqueioControles(exercicio);
            AplicarBloqueioControlesRecursosFinanceiros(exercicio);
            AplicarBloqueioControlesRecursosFinanceirosReprogramacao(exercicio);
            
            var obj = proxy.Service.GetAcaoPlanejamentoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            if (obj == null)
                return;

            ddlEixo.SelectedValue = obj.AcaoPlanejamento.IdEixoAcaoPlanejamento.ToString();
            carregarAcoes(proxyEstrutura);
            ddlAcao.SelectedValue = obj.IdAcaoPlanejamento.ToString();

            txtNome.Text = obj.Nome;
            txtObjetivos.Text = obj.Objetivos;
            txtDescricao.Text = obj.Descricao;
            txtOutrosEnvolvidos.Text = obj.OutrosEnvolvidos;
            
            verificarAlteracoes(obj.Id);

            if (obj.ValorEstimativaCusto.HasValue)
            {
                txtEstimativaCusto.Text = obj.ValorEstimativaCusto.Value.ToString("N2");
            }

            if (obj.MesPrevistoInicio.HasValue)
            {
                ddlMesPrevistoInicio.SelectedIndex = obj.MesPrevistoInicio.Value;
            }

            if (obj.AnoPrevistoInicio.HasValue)
            {
                ddlAnoPrevistoInicio.Text = obj.AnoPrevistoInicio.Value.ToString();
            }

            if (obj.MesPrevistoTermino.HasValue)
            {
                ddlMesPrevistoTermino.SelectedIndex = obj.MesPrevistoTermino.Value;
            }

            if (obj.AnoPrevistoTermino.HasValue)
            {
                ddlAnoPrevistoTermino.Text = obj.AnoPrevistoTermino.Value.ToString();
            }

            /*
            if (DateTime.Now.Year <= obj.AnoPrevistoInicio)
            {
                WebControl[] controles = ListarControlesBloqueio();
                Permissao.BlocoVI.VerificaPermissaoExercicioAcaoPlanejamentoBlocoVI(controles, true);
            }
            else
            {
                WebControl[] controles = ListarControlesBloqueio();
                Permissao.BlocoVI.VerificaPermissaoExercicioAcaoPlanejamentoBlocoVI(controles, false);
            }*/


            var recursos = proxy.Service.GetRecursoAcaoPlanejamentoById(obj.Id, obj.IdPrefeitura).Where(s => s.Exercicio == Convert.ToInt32(hdfAno.Value)).FirstOrDefault();

            if (recursos == null)
                return;

            chkFMAS.Checked = recursos.FonteFMAS;
            if (recursos.FonteFMAS && recursos.ValorFMAS.HasValue)
            {
                txtFMAS.Text = recursos.ValorFMAS.Value.ToString("N2");
                //txtFMAS.Enabled = enabled;
            }

            chkFEAS.Checked = recursos.FonteFEAS;
            if (recursos.FonteFEAS && recursos.ValorFEAS.HasValue)
            {
                txtFEAS.Text = recursos.ValorFEAS.Value.ToString("N2");
                //txtFEAS.Enabled = enabled;
            }

            chkFEASReprogramado.Checked = recursos.FonteFEASReprogramado;
            if (recursos.FonteFEASReprogramado && recursos.ValorFEASReprogramado.HasValue)
            {
                txtFEASReprogramado.Text = recursos.ValorFEASReprogramado.Value.ToString("N2");
                //txtFEAS.Enabled = enabled;
            }

            chkFNAS.Checked = recursos.FonteFNAS;
            if (recursos.FonteFNAS && recursos.ValorFNAS.HasValue)
            {
                txtFNAS.Text = recursos.ValorFNAS.Value.ToString("N2");
                //txtFNAS.Enabled = enabled;
            }

            chkOrcamentoMunicipal.Checked = recursos.FonteOrcamentoMunicipal;
            if (recursos.FonteOrcamentoMunicipal && recursos.ValorOrcamentoMunicipal.HasValue)
            {
                txtOrcamentoMunicipal.Text = recursos.ValorOrcamentoMunicipal.Value.ToString("N2");
                //txtOrcamentoMunicipal.Enabled = enabled;
            }

            chkOrcamentoEstadual.Checked = recursos.FonteOrcamentoEstadual;
            if (recursos.FonteOrcamentoEstadual && recursos.ValorOrcamentoEstadual.HasValue)
            {
                txtOrcamentoEstadual.Text = recursos.ValorOrcamentoEstadual.Value.ToString("N2");
                //txtOrcamentoEstadual.Enabled = enabled;
            }

            chkOrcamentoFederal.Checked = recursos.FonteOrcamentoFederal;
            if (recursos.FonteOrcamentoFederal && recursos.ValorOrcamentoFederal.HasValue)
            {
                txtOrcamentoFederal.Text = recursos.ValorOrcamentoFederal.Value.ToString("N2");
                //txtOrcamentoFederal.Enabled = enabled;
            }

            chkOutrosFundosMunicipais.Checked = recursos.FonteOutrosFundosMunicipais;
            if (recursos.FonteOutrosFundosMunicipais && recursos.ValorOutrosFundosMunicipais.HasValue)
            {
                txtOutrosFundosMunicipais.Text = recursos.ValorOutrosFundosMunicipais.Value.ToString("N2");
                //txtOutrosFundosMunicipais.Enabled = enabled;
            }

            chkOutrosFundosEstaduais.Checked = recursos.FonteOutrosFundosEstaduais;
            if (recursos.FonteOutrosFundosEstaduais && recursos.ValorOutrosFundosEstaduais.HasValue)
            {
                txtOutrosFundosEstaduais.Text = recursos.ValorOutrosFundosEstaduais.Value.ToString("N2");
                //txtOutrosFundosEstaduais.Enabled = enabled;
            }

            chkOutrosFundosFederais.Checked = recursos.FonteOutrosFundosFederais;
            if (recursos.FonteOutrosFundosFederais && recursos.ValorOutrosFundosFederais.HasValue)
            {
                txtOutrosFundosFederais.Text = recursos.ValorOutrosFundosFederais.Value.ToString("N2");
                //txtOutrosFundosFederais.Enabled = enabled;
            }

            chkIGDSUAS.Checked = obj.FonteIGDSUAS;
            if (recursos.FonteIGDSUAS && recursos.ValorIGDSUAS.HasValue)
            {
                txtIGDSUAS.Text = recursos.ValorIGDSUAS.Value.ToString("N2");
                //txtIGDSUAS.Enabled = enabled;
            }

            chkIGDPBF.Checked = recursos.FonteIGDPBF;
            if (recursos.FonteIGDPBF && recursos.ValorIGDPBF.HasValue)
            {
                txtIGDPBF.Text = recursos.ValorIGDPBF.Value.ToString("N2");
                //txtIGDPBF.Enabled = enabled;
            }

            AplicarBloqueioControlesRecursosFinanceiros(exercicio);

        }

        private void LoadRecursosFinanceiros(ProxyAcoes proxy)
        {

            ClearRecursosFinanceirosAplicados();

            int exercicio = String.IsNullOrEmpty(hdfAno.Value) ? 2022 : Convert.ToInt32(hdfAno.Value);

            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                var obj = proxy.Service.GetAcaoPlanejamentoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
                if (obj == null)
                    return;

                var enabled = Permissao.VerificarPermissao();

                //this.AplicarBloqueioControles(exercicio);
                AplicarBloqueioControlesRecursosFinanceiros(exercicio);
                AplicarBloqueioControlesRecursosFinanceirosReprogramacao(exercicio);
                var recursos = proxy.Service.GetRecursoAcaoPlanejamentoById(obj.Id, obj.IdPrefeitura).Where(s => s.Exercicio == Convert.ToInt32(hdfAno.Value)).FirstOrDefault();
                if (recursos == null)
                    return;

                #region recursos financeiros
                chkFMAS.Checked = recursos.FonteFMAS;
                if (recursos.FonteFMAS && recursos.ValorFMAS.HasValue)
                {
                    txtFMAS.Text = recursos.ValorFMAS.Value.ToString("N2");
                    //txtFMAS.Enabled = enabled;
                }

                chkFEAS.Checked = recursos.FonteFEAS;
                if (recursos.FonteFEAS && recursos.ValorFEAS.HasValue)
                {
                    txtFEAS.Text = recursos.ValorFEAS.Value.ToString("N2");
                    //txtFEAS.Enabled = enabled;
                }

                chkFEASReprogramado.Checked = recursos.FonteFEASReprogramado;
                if (recursos.FonteFEASReprogramado && recursos.ValorFEASReprogramado.HasValue)
                {
                    txtFEASReprogramado.Text = recursos.ValorFEASReprogramado.Value.ToString("N2");
                    //txtFEAS.Enabled = enabled;
                }

                chkFNAS.Checked = recursos.FonteFNAS;
                if (recursos.FonteFNAS && recursos.ValorFNAS.HasValue)
                {
                    txtFNAS.Text = recursos.ValorFNAS.Value.ToString("N2");
                    //txtFNAS.Enabled = enabled;
                }

                chkOrcamentoMunicipal.Checked = recursos.FonteOrcamentoMunicipal;
                if (recursos.FonteOrcamentoMunicipal && recursos.ValorOrcamentoMunicipal.HasValue)
                {
                    txtOrcamentoMunicipal.Text = recursos.ValorOrcamentoMunicipal.Value.ToString("N2");
                    //txtOrcamentoMunicipal.Enabled = enabled;
                }

                chkOrcamentoEstadual.Checked = recursos.FonteOrcamentoEstadual;
                if (recursos.FonteOrcamentoEstadual && recursos.ValorOrcamentoEstadual.HasValue)
                {
                    txtOrcamentoEstadual.Text = recursos.ValorOrcamentoEstadual.Value.ToString("N2");
                    //txtOrcamentoEstadual.Enabled = enabled;
                }

                chkOrcamentoFederal.Checked = recursos.FonteOrcamentoFederal;
                if (recursos.FonteOrcamentoFederal && recursos.ValorOrcamentoFederal.HasValue)
                {
                    txtOrcamentoFederal.Text = recursos.ValorOrcamentoFederal.Value.ToString("N2");
                    //txtOrcamentoFederal.Enabled = enabled;
                }

                chkOutrosFundosMunicipais.Checked = recursos.FonteOutrosFundosMunicipais;
                if (recursos.FonteOutrosFundosMunicipais && recursos.ValorOutrosFundosMunicipais.HasValue)
                {
                    txtOutrosFundosMunicipais.Text = recursos.ValorOutrosFundosMunicipais.Value.ToString("N2");
                    //txtOutrosFundosMunicipais.Enabled = enabled;
                }

                chkOutrosFundosEstaduais.Checked = recursos.FonteOutrosFundosEstaduais;
                if (recursos.FonteOutrosFundosEstaduais && recursos.ValorOutrosFundosEstaduais.HasValue)
                {
                    txtOutrosFundosEstaduais.Text = recursos.ValorOutrosFundosEstaduais.Value.ToString("N2");
                    //txtOutrosFundosEstaduais.Enabled = enabled;
                }

                chkOutrosFundosFederais.Checked = recursos.FonteOutrosFundosFederais;
                if (recursos.FonteOutrosFundosFederais && recursos.ValorOutrosFundosFederais.HasValue)
                {
                    txtOutrosFundosFederais.Text = recursos.ValorOutrosFundosFederais.Value.ToString("N2");
                    //txtOutrosFundosFederais.Enabled = enabled;
                }

                chkIGDSUAS.Checked = obj.FonteIGDSUAS;
                if (recursos.FonteIGDSUAS && recursos.ValorIGDSUAS.HasValue)
                {
                    txtIGDSUAS.Text = recursos.ValorIGDSUAS.Value.ToString("N2");
                    //txtIGDSUAS.Enabled = enabled;
                }

                chkIGDPBF.Checked = recursos.FonteIGDPBF;
                if (recursos.FonteIGDPBF && recursos.ValorIGDPBF.HasValue)
                {
                    txtIGDPBF.Text = recursos.ValorIGDPBF.Value.ToString("N2");
                    //txtIGDPBF.Enabled = enabled;
                }
               
            }
            AplicarBloqueioControlesRecursosFinanceiros(exercicio);

            #endregion
        }


        private void CarregarExercicios()
        {
            this.btnExercicio1.Text = Exercicios[0].ToString();
            this.btnExercicio2.Text = Exercicios[1].ToString();
            this.btnExercicio3.Text = Exercicios[2].ToString();
            this.btnExercicio4.Text = Exercicios[3].ToString();

            this.SelecionarCorAba();
        }

        private void SelecionarCorAba()
        {
            if (Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";

            }

            if (Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-info-seds";
            }
        }


        void carregarEixos(ProxyEstruturaAssistenciaSocial proxy)
        {
            ddlEixo.DataTextField = "Nome";
            ddlEixo.DataValueField = "Id";
            ddlEixo.DataSource = proxy.Service.GetEixosAcaoPlanejamento();
            ddlEixo.DataBind();
            Util.InserirItemEscolha(ddlEixo);
        }

        private void ClearRecursosFinanceirosAplicados()
        {
            txtFMAS.Text = String.Empty;
            txtFNAS.Text = String.Empty;
            txtFEAS.Text = String.Empty;
            txtFEASReprogramado.Text = String.Empty;
            txtOrcamentoMunicipal.Text = String.Empty;
            txtOrcamentoEstadual.Text = String.Empty;
            txtOrcamentoFederal.Text = String.Empty;
            txtOutrosFundosMunicipais.Text = String.Empty;
            txtOutrosFundosEstaduais.Text = String.Empty;
            txtOutrosFundosFederais.Text = String.Empty;
            txtIGDSUAS.Text = String.Empty;
            txtIGDPBF.Text = String.Empty;

            chkFEAS.Checked = false;
            chkFEASReprogramado.Checked = false;
            chkFMAS.Checked = false;
            chkFNAS.Checked = false;
            chkFEASReprogramado.Checked = false;
            chkIGDPBF.Checked = false;
            chkIGDSUAS.Checked = false;
            chkOrcamentoEstadual.Checked = false;
            chkOrcamentoFederal.Checked = false;
            chkOrcamentoMunicipal.Checked = false;
            chkOutrosFundosEstaduais.Checked = false;
            chkOutrosFundosFederais.Checked = false;
            chkOutrosFundosMunicipais.Checked = false;
        }

        void carregarAcoes(ProxyEstruturaAssistenciaSocial proxy)
        {
            ddlAcao.DataTextField = "Nome";
            ddlAcao.DataValueField = "Id";
            ddlAcao.DataSource = proxy.Service.GetAcoesPlanejamentoByEixo(Convert.ToInt32(ddlEixo.SelectedValue));
            ddlAcao.DataBind();
            Util.InserirItemEscolha(ddlAcao);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            bool desbloquioReprogramacao = (bool)Session["DesbloqueioReprogramacao"];
            bool desbloqueio = (bool)Session["Desbloqueio"]; 


            var obj = new PrefeituraAcaoPlanejamentoInfo();
            var rec = new RecursosPrefeituraAcaoPlanejamentoInfo();
            try
            {
                obj.Exercicio = Convert.ToInt32(this.hdfAno.Value);

                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    obj.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

                obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                if (ddlAcao.SelectedIndex != -1)
                    obj.IdAcaoPlanejamento = Convert.ToInt32(ddlAcao.SelectedValue);
                obj.Nome = txtNome.Text;
                if (txtObjetivos.Text.Length > 500)
                    txtObjetivos.Text = txtObjetivos.Text.Substring(0, 500);
                obj.Objetivos = txtObjetivos.Text;
                if (txtOutrosEnvolvidos.Text.Length > 150)
                    txtOutrosEnvolvidos.Text = txtOutrosEnvolvidos.Text.Substring(0, 150);
                obj.OutrosEnvolvidos = txtOutrosEnvolvidos.Text;
                if (txtDescricao.Text.Length > 1000)
                    txtDescricao.Text = txtDescricao.Text.Substring(0, 1000);
                obj.Descricao = txtDescricao.Text;


                rec.IdPrefeitura = obj.IdPrefeitura;
                rec.IdPrefeituraAcaoPlanejamento = obj.Id;
               
                if (chkFMAS.Checked)
                {
                    rec.FonteFMAS = true;
                    if (!String.IsNullOrEmpty(txtFMAS.Text))
                        rec.ValorFMAS = Convert.ToDecimal(txtFMAS.Text);
                }

                if (chkFEAS.Checked)
                {
                    rec.FonteFEAS = true;
                    if (!String.IsNullOrEmpty(txtFEAS.Text))
                        rec.ValorFEAS = Convert.ToDecimal(txtFEAS.Text);
                }

                if (chkFEASReprogramado.Checked)
                {
                    rec.FonteFEASReprogramado = true;
                    if (!String.IsNullOrEmpty(txtFEASReprogramado.Text))
                        rec.ValorFEASReprogramado = Convert.ToDecimal(txtFEASReprogramado.Text);
                }

                if (chkFNAS.Checked)
                {
                    rec.FonteFNAS = true;
                    if (!String.IsNullOrEmpty(txtFNAS.Text))
                        rec.ValorFNAS = Convert.ToDecimal(txtFNAS.Text);
                }

                if (chkOrcamentoMunicipal.Checked)
                {
                    rec.FonteOrcamentoMunicipal = true;
                    if (!String.IsNullOrEmpty(txtOrcamentoMunicipal.Text))
                        rec.ValorOrcamentoMunicipal = Convert.ToDecimal(txtOrcamentoMunicipal.Text);
                }

                if (chkOrcamentoEstadual.Checked)
                {
                    rec.FonteOrcamentoEstadual = true;
                    if (!String.IsNullOrEmpty(txtOrcamentoEstadual.Text))
                        rec.ValorOrcamentoEstadual = Convert.ToDecimal(txtOrcamentoEstadual.Text);
                }

                if (chkOrcamentoFederal.Checked)
                {
                    rec.FonteOrcamentoFederal = true;
                    if (!String.IsNullOrEmpty(txtOrcamentoFederal.Text))
                        rec.ValorOrcamentoFederal = Convert.ToDecimal(txtOrcamentoFederal.Text);
                }

                if (chkIGDPBF.Checked)
                {
                    rec.FonteIGDPBF = true;
                    if (!String.IsNullOrEmpty(txtIGDPBF.Text))
                        rec.ValorIGDPBF = Convert.ToDecimal(txtIGDPBF.Text);
                }

                if (chkIGDSUAS.Checked)
                {
                    rec.FonteIGDSUAS = true;
                    if (!String.IsNullOrEmpty(txtIGDSUAS.Text))
                        rec.ValorIGDSUAS = Convert.ToDecimal(txtIGDSUAS.Text);
                }

                if (chkOutrosFundosMunicipais.Checked)
                {
                    rec.FonteOutrosFundosMunicipais = true;
                    if (!String.IsNullOrEmpty(txtOutrosFundosMunicipais.Text))
                        rec.ValorOutrosFundosMunicipais = Convert.ToDecimal(txtOutrosFundosMunicipais.Text);
                }
                if (chkOutrosFundosEstaduais.Checked)
                {
                    rec.FonteOutrosFundosEstaduais = true;
                    if (!String.IsNullOrEmpty(txtOutrosFundosEstaduais.Text))
                        rec.ValorOutrosFundosEstaduais = Convert.ToDecimal(txtOutrosFundosEstaduais.Text);
                }
                if (chkOutrosFundosFederais.Checked)
                {
                    rec.FonteOutrosFundosFederais = true;
                    if (!String.IsNullOrEmpty(txtOutrosFundosFederais.Text))
                        rec.ValorOutrosFundosFederais = Convert.ToDecimal(txtOutrosFundosFederais.Text);
                }

                rec.Exercicio = Convert.ToInt32(this.hdfAno.Value);

                 
                if (!String.IsNullOrEmpty(txtEstimativaCusto.Text))
                {
                    obj.ValorEstimativaCusto = Convert.ToDecimal(txtEstimativaCusto.Text);
                }

                if (Convert.ToInt32(ddlMesPrevistoInicio.SelectedValue) > 0)
                {
                    obj.MesPrevistoInicio = Convert.ToInt32(ddlMesPrevistoInicio.SelectedValue);
                }

                if (Convert.ToInt32(ddlAnoPrevistoInicio.SelectedValue) > 0)
                {
                    obj.AnoPrevistoInicio = Convert.ToInt32(ddlAnoPrevistoInicio.SelectedValue);
                }

                if (Convert.ToInt32(ddlMesPrevistoTermino.SelectedValue) > 0)
                {
                    obj.MesPrevistoTermino = Convert.ToInt32(ddlMesPrevistoTermino.SelectedValue);
                }

                if (Convert.ToInt32(ddlAnoPrevistoTermino.SelectedValue) > 0)
                {
                    obj.AnoPrevistoTermino = Convert.ToInt32(ddlAnoPrevistoTermino.SelectedValue);
                }
                

                new ValidadorPrefeituraAcaoPlanejamento().Validar(obj);

                if (obj.IdAcaoPlanejamento == 26)
                {
                    if (desbloqueio)
                    {
                        new ValidadorPrefeituraAcaoPlanejamento().ValidarRecurso(rec);    
                    }
                        
                }

               
                using (var proxy = new ProxyAcoes())
                {
                    if (obj.Id == 0)
                    {
                        proxy.Service.AddAcaoPlanejamento(obj);
                        rec.IdPrefeituraAcaoPlanejamento = obj.Id;
                        proxy.Service.SaveRecursoAcaoPlanejamento(rec);                      
                    }
                    else
                    {
                        proxy.Service.UpdateAcaoPlanejamento(obj);
                        proxy.Service.SaveRecursoAcaoPlanejamento(rec);
                    }
                }

                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.ToString()), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Cadastro concluído com sucesso"), true);
            Response.Redirect("~/BlocoVI/CAcaoPlanejamento.aspx?msg=" + (obj.Id == 0 ? "AI" : "AU"));
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoVI/CAcaoPlanejamento.aspx");
        }

        protected void ddlEixo_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                carregarAcoes(proxy);
            }
        }

        protected void ddlAcao_SelectedIndexChanged(object sender, EventArgs e)  
        {
            if (Convert.ToInt32(ddlAcao.SelectedValue) == 26)
            {
                chkFEAS.Checked = txtFEAS.Enabled = true;
                // txtFEAS.Text = "60.000,00";
            }
        }

        protected void chkFMAS_CheckedChanged(object sender, EventArgs e)
        {
            txtFMAS.Enabled = chkFMAS.Checked;
        }

        protected void chkOrcamentoMunicipal_CheckedChanged(object sender, EventArgs e)
        {
            txtOrcamentoMunicipal.Enabled = chkOrcamentoMunicipal.Checked;
        }

        protected void chkOutrosFundosMunicipais_CheckedChanged(object sender, EventArgs e)
        {
            txtOutrosFundosMunicipais.Enabled = chkOutrosFundosMunicipais.Checked;
        }

        protected void chkFEAS_CheckedChanged(object sender, EventArgs e)
        {
            txtFEAS.Enabled = chkFEAS.Checked;
        }

        protected void chkFEASReprogramado_CheckedChanged(object sender, EventArgs e)
        {
            txtFEASReprogramado.Enabled = chkFEASReprogramado.Checked;
        }

        protected void chkOrcamentoEstadual_CheckedChanged(object sender, EventArgs e)
        {
            txtOrcamentoEstadual.Enabled = chkOrcamentoEstadual.Checked;
        }

        protected void chkOutrosFundosEstaduais_CheckedChanged(object sender, EventArgs e)
        {
            txtOutrosFundosEstaduais.Enabled = chkOutrosFundosEstaduais.Checked;
        }

        protected void chkFNAS_CheckedChanged(object sender, EventArgs e)
        {
            txtFNAS.Enabled = chkFNAS.Checked;
        }

        protected void chkOrcamentoFederal_CheckedChanged(object sender, EventArgs e)
        {
            txtOrcamentoFederal.Enabled = chkOrcamentoFederal.Checked;
        }

        protected void chkOutrosFundosFederais_CheckedChanged(object sender, EventArgs e)
        {
            txtOutrosFundosFederais.Enabled = chkOutrosFundosFederais.Checked;
        }

        protected void chkIGDPAIF_CheckedChanged(object sender, EventArgs e)
        {
            txtIGDPBF.Enabled = chkIGDPBF.Checked;
        }

        protected void chkIGDSUAS_CheckedChanged(object sender, EventArgs e)
        {
            txtIGDSUAS.Enabled = chkIGDSUAS.Checked;
        }

        protected void btnExercicio1_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio1.Text;
            int exercicio1 = Convert.ToInt32(hdfAno.Value);
            ClearRecursosFinanceirosAplicados();
            InibeOuExibereprogramados(exercicio1);
            using (var proxy = new ProxyAcoes())
            {
                LoadRecursosFinanceiros(proxy);
            }

            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }

        protected void btnExercicio2_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio2.Text;
            int exercicio2 = Convert.ToInt32(hdfAno.Value);
            ClearRecursosFinanceirosAplicados();
            InibeOuExibereprogramados(exercicio2);
            using (var proxy = new ProxyAcoes())
            {
                LoadRecursosFinanceiros(proxy);
            }
            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }

        protected void btnExercicio3_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio3.Text;
            int exercicio3 = Convert.ToInt32(hdfAno.Value);
            ClearRecursosFinanceirosAplicados();
            InibeOuExibereprogramados(exercicio3);
            using (var proxy = new ProxyAcoes())
            {
                LoadRecursosFinanceiros(proxy);
            }
            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }

        protected void btnExercicio4_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio4.Text;
            int exercicio4 = Convert.ToInt32(hdfAno.Value);
            ClearRecursosFinanceirosAplicados();
            InibeOuExibereprogramados(exercicio4);
            using (var proxy = new ProxyAcoes())
            {
                LoadRecursosFinanceiros(proxy);
            }
            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }
    }
}