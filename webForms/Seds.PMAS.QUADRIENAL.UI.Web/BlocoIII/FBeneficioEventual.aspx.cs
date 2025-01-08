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

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FBeneficioEventual : System.Web.UI.Page
    {

        #region properties
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        #endregion

        #region VariaveisGlobais
        public int idBeneficio;
        public int idPrefeituraBeneficioEventual;
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

                if (String.IsNullOrEmpty(Request.QueryString["idTipo"]))
                {
                    Response.Redirect("~/BlocoIII/CBeneficioEventual.aspx");
                    return;
                }

                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    carregarEstruturas(proxy);
                }

                using (var proxy = new ProxyProgramas())
                {
                    load(proxy);
                }

                adicionarEventos();

                #region Bloqueia , Desbloqueia
                WebControl[] controles = {   txtAnoLei,
                                             txtNumeroLei,
                                             txtAnoResolucao,
                                             txtNumeroResolucao,
                                             rblFormaAuxilio,
                                             rblRegulamentacao,                                             
                                             txtFEAS,
                                             txtFMAS,
                                             txtFNAS,
                                             txtFundoEstadualSolidariedade,
                                             txtFundoMunicipalSolidariedade,
                                             txtOrcamentoMunicipal,
                                             txtMediaSemestralBeneficiarios,
                                             txtMediaSemestralBeneficiariosConcedidos,
                                             chkBeneficiosOferecidos,
                                             chklTipoLegislacao,
                                             chkResolucao,
                                             chkDecreto,
                                             chkCriterios,
                                             chkNecessidades,
                                             chkResponsaveis,
                                             chkUnidadeExecutora,
                                             btnSalvar,
                                             rblAlteracaoLei,
                                             txtNumeroLeiAlteracao,
                                             txtNumeroLeiAlteracaoComplemento,
                                             rblAlteracaoDecreto,
                                             txtDecretoAlteracao,
                                             txtDecretoAlteracaoComplemento,
                                             rblAlteracaoResolucao,
                                             txtResocaoAlteracao,
                                             txtResocaoAlteracaoComplemento
                                         };

                Permissao.VerificarPermissaoControles(controles, Session);
                Permissao.VerificarPermissaoControles(txtDataPublicacao.Controles, Session);
                Permissao.VerificarPermissaoControles(txtDataResolucao.Controles, Session);
                

                rblRegulamentacao_SelectedIndexChanged(null, null);

                #endregion
            }
            LoadExercicios();
        }
        private void LoadExercicios()
        {
            this.btnExercicio1.Text = Exercicios[0].ToString();
            this.btnExercicio2.Text = Exercicios[1].ToString();
            this.btnExercicio3.Text = Exercicios[2].ToString();
            this.btnExercicio4.Text = Exercicios[3].ToString();

            this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? "2022" : this.hdfAno.Value;
            this.SelecionarCorAba();
                
        }




        private void SelecionarCorAba()
        {
            if (FBeneficioEventual.Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FBeneficioEventual.Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FBeneficioEventual.Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FBeneficioEventual.Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-info-seds";
            }
        }
        void adicionarEventos()
        {
            txtFMAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasParlamentares.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtValorContraExercicio.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFundoMunicipalSolidariedade.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFundoEstadualSolidariedade.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtReprogramacaoAnoAnterior.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasParlamentaresReprogramacao.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtOrcamentoMunicipal.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
        }


        
        void carregarEstruturas(ProxyEstruturaAssistenciaSocial proxy)
        {
            chkCriterios.DataValueField = "Id";
            chkCriterios.DataTextField = "Nome";
            chkCriterios.DataSource = proxy.Service.GetCriteriosConcessaoParaBeneficiosEventuais();
            chkCriterios.DataBind();

            chkResponsaveis.DataValueField = "Id";
            chkResponsaveis.DataTextField = "Nome";
            chkResponsaveis.DataSource = proxy.Service.GetOrgaosReponsaveisParaBeneficiosEventuais();
            chkResponsaveis.DataBind();

            var tipoBeneficioEventual = (ETipoBeneficioEventual)Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            if (tipoBeneficioEventual == ETipoBeneficioEventual.AuxilioNatalidade || tipoBeneficioEventual == ETipoBeneficioEventual.AuxilioFuneral)
            {
                chkNecessidades.DataValueField = "Id";
                chkNecessidades.DataTextField = "Nome";
                chkNecessidades.DataSource = proxy.Service.GetNecessidadesBeneficiosEventuaisByTipoBeneficioEventual(Convert.ToInt32(tipoBeneficioEventual));
                chkNecessidades.DataBind();
            }

            if (tipoBeneficioEventual == ETipoBeneficioEventual.CalamidadePublica || tipoBeneficioEventual == ETipoBeneficioEventual.VulnerabilidadeTemporaria)
            {
                chkBeneficiosOferecidos.DataValueField = "Id";
                chkBeneficiosOferecidos.DataTextField = "Nome";
                chkBeneficiosOferecidos.DataSource = proxy.Service.GetBeneficiosEventuaisByTipoBeneficioEventual(Convert.ToInt32(tipoBeneficioEventual));
                chkBeneficiosOferecidos.DataBind();
            }

            //ddlTipoLegislacao.Items.Add(new ListItem("Lei", "1"));
            //ddlTipoLegislacao.Items.Add(new ListItem("Resolução", "3"));
            //
            //Util.InserirItemEscolha(ddlTipoLegislacao);
           
            chkUnidadeExecutora.DataValueField = "Id";
            chkUnidadeExecutora.DataTextField = "RazaoSocial";
            chkUnidadeExecutora.DataSource = new ProxyRedeProtecaoSocial().Service.GetIdentificacaoUnidadesPrivadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, String.Empty, 2);
            chkUnidadeExecutora.DataBind();
        }

        void load(ProxyProgramas proxy)
        {
            ClearRecursosFinanceirosAplicados();
            int exercicio = String.IsNullOrEmpty(hdfAno.Value) ? 2022 : Convert.ToInt32(hdfAno.Value);

            if (String.IsNullOrEmpty(Request.QueryString["idTipo"]))
                return;

            var idTipoBeneficioEventual = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            var tipoBeneficioEventual = (ETipoBeneficioEventual)idTipoBeneficioEventual;
            bool necessidade = trNecessidades.Visible = tipoBeneficioEventual == ETipoBeneficioEventual.AuxilioNatalidade || tipoBeneficioEventual == ETipoBeneficioEventual.AuxilioFuneral;
            trBeneficiosOferecidos.Visible = tipoBeneficioEventual == ETipoBeneficioEventual.CalamidadePublica || tipoBeneficioEventual == ETipoBeneficioEventual.VulnerabilidadeTemporaria;

            preencheTitulo(tipoBeneficioEventual);

            var obj = proxy.Service.GetBeneficioEventualByPrefeituraETipoBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id, idTipoBeneficioEventual);
            
            if (obj != null)
            {

                rblFormaAuxilio.SelectedValue = obj.IdFormaAuxilio.ToString();

                int regulamentacao = Convert.ToInt32(obj.Regulamentacao);

                rblRegulamentacao.SelectedValue = Convert.ToString(regulamentacao);

                if (obj.Regulamentacao)
                {
                   trLegislacao.Visible = true;
                   
                   int tipoLegisalacao = obj.IdTipoLegislacao != null ? Convert.ToInt32(obj.IdTipoLegislacao) : 0;

                   if (tipoLegisalacao == 1)
                   {
                        if (!String.IsNullOrEmpty(obj.Lei))
                        {
                            chklTipoLegislacao.Checked = obj.IdTipoLegislacao == 1 ? true : false;

                            txtNumeroLei.Text = obj.Lei.Split('/')[0];
                            txtAnoLei.Text = obj.Lei.Split('/')[1];
                            txtDataPublicacao.Text = obj.DataPublicacaoLei.HasValue ? obj.DataPublicacaoLei.Value.ToShortDateString(): null;

                            if (obj.AlterouLei == true)
                            {
                                rblAlteracaoLei.SelectedValue = "1";

                                txtNumeroLeiAlteracao.Text = obj.NumeroLeiAlterada != null ? obj.NumeroLeiAlterada.Split('/')[0] : "";
                                txtNumeroLeiAlteracaoComplemento.Text = obj.NumeroLeiAlterada != null ? obj.NumeroLeiAlterada.Split('/')[1] : "";
                                txtDataAlteracaoLei.Text = obj.DataAlteracaoLei.HasValue ? obj.DataAlteracaoLei.Value.ToShortDateString() : "";
                                rblAlteracaoLei_SelectedIndexChanged(null, null);

                            }
                            else
                            {
                                rblAlteracaoLei.SelectedValue = "0";
                                txtNumeroLeiAlteracao.Text = String.Empty;
                                txtNumeroLeiAlteracaoComplemento.Text = String.Empty;
                                txtDataAlteracaoLei.Text = String.Empty;
                            }


                            chklTipoLegislacao_CheckedChanged(null, null);
                        }    
                   }
                   
                   if (obj.Resolucao == true)
                   {
                       chkResolucao.Checked = obj.Resolucao == true ? true : false;

                       txtNumeroResolucao.Text = !String.IsNullOrEmpty(obj.NumeroResolucao) ? obj.NumeroResolucao.Split('/')[0] : "";
                       txtAnoResolucao.Text = !String.IsNullOrEmpty(obj.NumeroResolucao) ? obj.NumeroResolucao.Split('/')[1] : "";
                       txtDataResolucao.Text = obj.DataResolucao.HasValue ? obj.DataResolucao.Value.ToShortDateString() : "";

                       if (obj.AlterouResolucao == true)
                       {
                           rblAlteracaoResolucao.SelectedValue = "1";
                           txtResocaoAlteracao.Text = !String.IsNullOrEmpty(obj.NumeroResolucaoAlterada) ? obj.NumeroResolucaoAlterada.Split('/')[0] : "";
                           txtResocaoAlteracaoComplemento.Text = !String.IsNullOrEmpty(obj.NumeroResolucaoAlterada) ? obj.NumeroResolucaoAlterada.Split('/')[1] : "";
                           txtDataAlteracaoResolucao.Text = obj.DataAlteracaoResolucao.HasValue ? obj.DataAlteracaoResolucao.Value.ToShortDateString(): "";

                           rblAlteracaoResolucao_SelectedIndexChanged(null, null);
                       }
                       else
                       {
                           rblAlteracaoResolucao.SelectedValue = "0";
                           txtResocaoAlteracao.Text = String.Empty;
                           txtDataAlteracaoResolucao.Text = String.Empty;
                       }
                       
                       chkResolucao_CheckedChanged(null, null);
                   }

                   if (obj.Decreto == true)
                   {
                       chkDecreto.Checked = obj.Decreto == true ? true : false;

                       if (obj.AlterouDecreto == true)
                       {
                           rblAlteracaoDecreto.SelectedValue = "1";
                           txtDecretoAlteracao.Text = !String.IsNullOrEmpty(obj.NumeroDecretoAlterado) ? obj.NumeroDecretoAlterado.Split('/')[0] : "";
                           txtDecretoAlteracaoComplemento.Text = !String.IsNullOrEmpty(obj.NumeroDecretoAlterado) ? obj.NumeroDecretoAlterado.Split('/')[1] : "";
                           txtDataAlteracaoDecreto.Text = obj.DataAlteracaoDecreto.HasValue ? obj.DataAlteracaoDecreto.Value.ToShortDateString() : "";

                           rblAlteracaoDecreto_SelectedIndexChanged(null, null);
                       }
                       else
                       {
                           rblAlteracaoDecreto.SelectedValue = "0";
                           txtDecretoAlteracao.Text = String.Empty;
                           txtDataAlteracaoDecreto.Text = String.Empty;
                       }

                   }
                   else
                   {
                       chkDecreto.Checked = false;
                   }

                   txtDecretoPortaria.Text = !String.IsNullOrEmpty(obj.NumeroDecreto) ? obj.NumeroDecreto.Split('/')[0] : "";
                   txtAnoDecretoPortaria.Text = !String.IsNullOrEmpty(obj.NumeroDecreto) ? obj.NumeroDecreto.Split('/')[1] : "";
                   txtDataDecretoPortaria.Text = obj.DataDecreto.HasValue ? obj.DataDecreto.Value.ToShortDateString() : "";

                   chkDecreto_CheckedChanged(null, null);
                  
                }

                txtMediaSemestralBeneficiarios.Text = obj.MediaSemestralBeneficiarios.ToString();
                txtMediaSemestralBeneficiariosConcedidos.Text = obj.MediaSemestralBeneficiariosConcedidos.ToString();

                #region recursos financeiros
                if (obj.PrefeituraBeneficiosEventuaisRecursosFinanceiros != null)
                {
                    var recurso = obj.PrefeituraBeneficiosEventuaisRecursosFinanceiros.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                    if (recurso != null)
                    {
                        txtFMAS.Text = recurso.ValorFMAS.ToString("N2");
                        txtReprogramacaoAnoAnterior.Text = recurso.ValorReprogramacaoAnoAnterior.ToString("N2");
                        txtFEASDemandasParlamentaresReprogramacao.Text = recurso.ValorReprogramacaoDemandasParlamentares.ToString("N2");
                        txtFundoEstadualSolidariedade.Text = recurso.ValorFundoEstadualSolidariedade.ToString("N2");
                        txtOrcamentoMunicipal.Text = recurso.ValorOrcamentoMunicipal.ToString("N2");
                        txtFEAS.Text = recurso.ValorFEAS.ToString("N2");
                        txtFEASDemandasParlamentares.Text = recurso.ValorDemandasParlamentares.ToString("N2");
                        txtFundoMunicipalSolidariedade.Text = recurso.ValorFundoMunicipalSolidariedade.ToString("N2");
                        txtFNAS.Text = recurso.ValorFNAS.ToString("N2");

                        /*Recursos financeiros Demandas Parlamentares*/

                        txtCodigoDemandaExercicio.Text = recurso.CodigoDemandaParlamentar;
                        txtObjetoDemandaExercicio.Text = recurso.ObjetoDemandaParlamentar;

                        if (recurso.ContrapartidaMunicipal == true)
                        {
                            trValorContraExercicio.Visible = true;
                            rblContraPartida.SelectedValue = "1";
                            txtValorContraExercicio.Text = recurso.ValorContrapartidaMunicipal.HasValue ? recurso.ValorContrapartidaMunicipal.Value.ToString("N2") : "0,00";
                        }
                        else
                        {
                            rblContraPartida.SelectedValue = "0";
                            trValorContraExercicio.Visible = false;
                        }

                        Session["idBeneficio"] = recurso.Id;
                        Session["idPrefeituraBeneficioEventual"] = recurso.IdPrefeituraBeneficioEventual;      
                    }
                }
                #endregion

                if (necessidade)
                {
                    if (obj.Necessidades != null && obj.Necessidades.Count > 0)
                    {
                        foreach (ListItem i in chkNecessidades.Items)
                            i.Selected = obj.Necessidades.Any(s => s.Id == Convert.ToInt32(i.Value));
                    }
                }


                if (obj.BeneficiosOferecidos != null && obj.BeneficiosOferecidos.Count > 0)
                {
                    foreach (ListItem i in chkBeneficiosOferecidos.Items)
                        i.Selected = obj.BeneficiosOferecidos.Any(s => s.Id == Convert.ToInt32(i.Value));
                }
                
                if (obj.Criterios != null && obj.Criterios.Count > 0)
                {
                    foreach (ListItem i in chkCriterios.Items)
                        i.Selected = obj.Criterios.Any(s => s.Id == Convert.ToInt32(i.Value));
                }

                if (obj.OrgaosResponsaveis != null && obj.OrgaosResponsaveis.Count > 0)
                {
                    foreach (ListItem i in chkResponsaveis.Items)
                        i.Selected = obj.OrgaosResponsaveis.Any(s => s.Id == Convert.ToInt32(i.Value));
                }

                if (obj.UnidadesExecutoras != null && obj.UnidadesExecutoras.Count > 0) //Welington P.
                {
                    foreach (ListItem i in chkUnidadeExecutora.Items)
                        i.Selected = obj.UnidadesExecutoras.Any(s => s.Id == Convert.ToInt32(i.Value));
                }
                int idQuadro = 0;
                switch (tipoBeneficioEventual)
                {
                    case ETipoBeneficioEventual.AuxilioNatalidade:
                        idQuadro = 53;
                        break;
                    case ETipoBeneficioEventual.AuxilioFuneral:
                        idQuadro = 54;
                        break;
                    case ETipoBeneficioEventual.CalamidadePublica:
                        idQuadro = 55;
                        break;
                    case ETipoBeneficioEventual.VulnerabilidadeTemporaria:
                        idQuadro = 56;
                        break;
                    default:
                        break;
                }

                verificarAlteracoes(obj.Id, idQuadro);
            }
            chkResponsaveis_SelectedIndexChanged(null, null);
            AplicarBloqueioDesbloqueio(exercicio);
            AplicarBloqueioDesbloqueioReprogramacao(exercicio);
            AplicarBloqueioDesbloqueioDemandas(exercicio);
            //ExibeReprogramacao(exercicio);
        }

        private void LoadRecursosFinanceiros(ProxyProgramas proxy) 
        {

            ClearRecursosFinanceirosAplicados();
            int exercicio = String.IsNullOrEmpty(hdfAno.Value) ? 2022 : Convert.ToInt32(hdfAno.Value);
            var idTipoBeneficioEventual = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            var obj = proxy.Service.GetBeneficioEventualByPrefeituraETipoBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id, idTipoBeneficioEventual);

            #region recursos financeiros

            if (obj != null)
            {
                if (obj.PrefeituraBeneficiosEventuaisRecursosFinanceiros != null)
                {
                    var recurso = obj.PrefeituraBeneficiosEventuaisRecursosFinanceiros.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                    if (recurso != null)
                    {
                        txtFMAS.Text = recurso.ValorFMAS.ToString("N2");
                        txtReprogramacaoAnoAnterior.Text = recurso.ValorReprogramacaoAnoAnterior.ToString("N2");
                        txtFEASDemandasParlamentaresReprogramacao.Text = recurso.ValorReprogramacaoDemandasParlamentares.ToString("N2");
                        txtFundoEstadualSolidariedade.Text = recurso.ValorFundoEstadualSolidariedade.ToString("N2");
                        txtOrcamentoMunicipal.Text = recurso.ValorOrcamentoMunicipal.ToString("N2");
                        txtFEAS.Text = recurso.ValorFEAS.ToString("N2");
                        txtFEASDemandasParlamentares.Text = recurso.ValorDemandasParlamentares.ToString("N2");
                        txtFundoMunicipalSolidariedade.Text = recurso.ValorFundoMunicipalSolidariedade.ToString("N2");
                        txtFNAS.Text = recurso.ValorFNAS.ToString("N2");

                        /*Recursos financeiros Demandas Parlamentares*/

                        txtCodigoDemandaExercicio.Text = recurso.CodigoDemandaParlamentar;
                        txtObjetoDemandaExercicio.Text = recurso.ObjetoDemandaParlamentar;

                        if (recurso.ContrapartidaMunicipal == true)
                        {
                            trValorContraExercicio.Visible = true;
                            rblContraPartida.SelectedValue = "1";
                            txtValorContraExercicio.Text = recurso.ValorContrapartidaMunicipal.HasValue ? recurso.ValorContrapartidaMunicipal.Value.ToString("N2") : "0,00";
                        }
                        else
                        {
                            rblContraPartida.SelectedValue = "0";
                            trValorContraExercicio.Visible = false;
                        }

                        Session["idBeneficio"] = recurso.Id;
                        Session["idPrefeituraBeneficioEventual"] = recurso.IdPrefeituraBeneficioEventual;
                    }
                }                
            }
            
            #endregion

            AplicarBloqueioDesbloqueio(exercicio);
            AplicarBloqueioDesbloqueioReprogramacao(exercicio);
            AplicarBloqueioDesbloqueioDemandas(exercicio);

        }

        //private void ExibeReprogramacao(int exercicio) 
        //{
        //    if (exercicio == 2025)
        //    {
        //        trReprogramacao.Visible = true;
        //    }
        //    else
        //    {
        //        trReprogramacao.Visible = false;
        //    }
        //}

        private void AplicarBloqueioDesbloqueio(int exercicio)
        {
            WebControl[] controlesExercicio = ObterControlesBeneficiosEventuaisRecursosFinanceiros();
            Permissao.BlocoIII.VerificaPermissaoExercicioBeneficiosEventuaisBlocoIII(controlesExercicio, exercicio);
        }

        private void AplicarBloqueioDesbloqueioReprogramacao(int exercicio) 
        {
            WebControl[] controlesExercicioReprogramacao = ObterControlesReprogramacao();
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesExercicioReprogramacao,exercicio);
        }

        private WebControl[] ObterControlesBeneficiosEventuaisRecursosFinanceiros()
        {
            WebControl[] controlesExercicio = 
            {
                  txtFMAS
                 , txtFundoMunicipalSolidariedade
                 , txtOrcamentoMunicipal
                 , txtFEAS
                 , txtFundoEstadualSolidariedade
                 , txtFNAS
            };
            return controlesExercicio;
        }

        private void AplicarBloqueioDesbloqueioDemandas(int exercicio)
        {
            WebControl[] controlesDemandas = ObterControlesDemandas();
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIDemandas(controlesDemandas, exercicio);
            
        }


        private WebControl[] ObterControlesDemandas() 
        {
            WebControl[] controlesDemandas = {
                                                     txtFEASDemandasParlamentares,
                                                     txtCodigoDemandaExercicio,
                                                     txtObjetoDemandaExercicio,
                                                     txtValorContraExercicio,
                                                     rblContraPartida
                                              };
            return controlesDemandas;
        }

        private WebControl[] ObterControlesReprogramacao() 
        {
            WebControl[] controles =
            {
                txtReprogramacaoAnoAnterior,
                txtFEASDemandasParlamentaresReprogramacao
            };
            return controles;
        }

        private WebControl[] ObterControles() {

            WebControl[] controles = {   txtAnoLei,
                                             txtNumeroLei,
                                             txtAnoResolucao,
                                             txtNumeroResolucao,
                                             rblFormaAuxilio,
                                             rblRegulamentacao,                                             
                                             txtFEAS,
                                             txtFMAS,
                                             txtFNAS,
                                             txtFundoEstadualSolidariedade,
                                             txtFundoMunicipalSolidariedade,
                                             txtOrcamentoMunicipal,
                                             txtMediaSemestralBeneficiarios,
                                             txtMediaSemestralBeneficiariosConcedidos,
                                             chkBeneficiosOferecidos,
                                             chklTipoLegislacao,
                                             chkResolucao,
                                             chkDecreto,
                                             chkCriterios,
                                             chkNecessidades,
                                             chkResponsaveis,
                                             chkUnidadeExecutora,
                                             btnSalvar,
                                            
                                         };
            return controles;
        }

        private void AplicarBloqueioControles()
        {
            WebControl[] controles = ObterControles();

            Permissao.VerificarPermissaoControles(controles, Session);
        }

                     



        private void ClearRecursosFinanceirosAplicados()
        {
            txtFMAS.Text = "0,00";
            txtFundoMunicipalSolidariedade.Text = "0,00";
            txtOrcamentoMunicipal.Text = "0,00";
            txtFEAS.Text = "0,00";
            txtFEASDemandasParlamentares.Text = "0,00";
            txtFundoEstadualSolidariedade.Text = "0,00";
            txtReprogramacaoAnoAnterior.Text = "0,00";
            txtFEASDemandasParlamentaresReprogramacao.Text = "0,00";
            txtFNAS.Text = "0,00";
        }

        void verificarAlteracoes(Int32 idPrefeituraBeneficioEventual, Int32 idQuadro)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, idQuadro, idPrefeituraBeneficioEventual);
                    linkAlteracoesQuadro.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idQuadro.ToString())) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idPrefeituraBeneficioEventual.ToString()));
                }
            }
        }

        private void preencheTitulo(ETipoBeneficioEventual tipo)
        {
            try
            {
               // string numeracao;
                switch (tipo)
                {
                    case ETipoBeneficioEventual.AuxilioNatalidade:
                        lblTitulo.Text = "Benefício Eventual - Auxílio Natalidade";
                        lblNumeracao.Text = "3.25";
                         //= 42;   
                    // numeracao = 16;
                        break;
                    case ETipoBeneficioEventual.AuxilioFuneral:
                        lblTitulo.Text = "Benefício Eventual - Auxílio Funeral";
                        lblNumeracao.Text = "3.26";    
                    //numeracao = 17;
                        break;
                    case ETipoBeneficioEventual.CalamidadePublica:
                        lblTitulo.Text = "Benefícios Eventuais em casos de Calamidade Pública";
                        lblNumeracao.Text = "3.27";    
                    //numeracao = 18;
                        break;
                    case ETipoBeneficioEventual.VulnerabilidadeTemporaria:
                        lblTitulo.Text = "Benefícios Eventuais em casos de Vulnerabilidade Temporária";
                        lblNumeracao.Text = "3.28";
                        break;
                    default:
                        break;
                }

             //    = numeracao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rblRegulamentacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblRegulamentacao.SelectedValue == "1")
            {
                trGeral.Visible = true;
                
                trLegislacao.Visible = true;

                //chkDecreto.Enabled = true;
                
            }
            else
            {
                trGeral.Visible = false;

                trLegislacao.Visible = false;
            }
        }

        private bool Aderiu()
        {
            using (var proxy = new ProxyProgramas())
            {
                var idTipoBeneficioEventual = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
                bool retorno;

                var obj = proxy.Service.GetBeneficioEventualByPrefeituraETipoBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id, idTipoBeneficioEventual);

                if (obj != null)
                {
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }

                return retorno;
            }
        
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int exercicio = String.IsNullOrEmpty(hdfAno.Value) ? 2022 : Convert.ToInt32(hdfAno.Value);
            SessaoPmas.VerificarSessao(this);

            var obj = new PrefeituraBeneficioEventualInfo();
            obj.IdTipoBeneficioEventual = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idTipo"]));
            obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            bool aderiu;

            aderiu = Aderiu();

            #region recurso financeiro
            obj.PrefeituraBeneficiosEventuaisRecursosFinanceiros = new List<PrefeituraBeneficioEventualRecursosFinanceirosInfo>();
            PrefeituraBeneficioEventualRecursosFinanceirosInfo recurso = new PrefeituraBeneficioEventualRecursosFinanceirosInfo();
            recurso.Exercicio = exercicio;
            if (!String.IsNullOrEmpty(txtFMAS.Text))
            {
                recurso.ValorFMAS = Convert.ToDecimal(txtFMAS.Text);
            }
            if (!String.IsNullOrEmpty(txtFundoMunicipalSolidariedade.Text))
            {
                recurso.ValorFundoMunicipalSolidariedade = Convert.ToDecimal(txtFundoMunicipalSolidariedade.Text);
            }
            if (!String.IsNullOrEmpty(txtOrcamentoMunicipal.Text))
            {
                recurso.ValorOrcamentoMunicipal = Convert.ToDecimal(txtOrcamentoMunicipal.Text);
            }

            if (!String.IsNullOrEmpty(txtFEAS.Text))
            {
                recurso.ValorFEAS = Convert.ToDecimal(txtFEAS.Text);
            }

            if (!String.IsNullOrEmpty(txtFEASDemandasParlamentares.Text))
            {
                recurso.ValorDemandasParlamentares = Convert.ToDecimal(txtFEASDemandasParlamentares.Text);
            }

            if (!String.IsNullOrEmpty(txtFundoEstadualSolidariedade.Text))
            {
                recurso.ValorFundoEstadualSolidariedade = Convert.ToDecimal(txtFundoEstadualSolidariedade.Text);
            }

            if (!String.IsNullOrEmpty(txtReprogramacaoAnoAnterior.Text))
            {
                recurso.ValorReprogramacaoAnoAnterior = Convert.ToDecimal(txtReprogramacaoAnoAnterior.Text);
            }

            if (!String.IsNullOrEmpty(txtFEASDemandasParlamentaresReprogramacao.Text))
            {
                recurso.ValorReprogramacaoDemandasParlamentares = Convert.ToDecimal(txtFEASDemandasParlamentaresReprogramacao.Text);
            }

            if (!String.IsNullOrEmpty(txtFNAS.Text))
            {
                recurso.ValorFNAS = Convert.ToDecimal(txtFNAS.Text);
            }


            #region Preencher: Demandas

            recurso.ObjetoDemandaParlamentar = !String.IsNullOrEmpty(txtObjetoDemandaExercicio.Text) ? txtObjetoDemandaExercicio.Text : "";
            recurso.CodigoDemandaParlamentar = !String.IsNullOrEmpty(txtCodigoDemandaExercicio.Text) ? txtCodigoDemandaExercicio.Text : "";
            recurso.ValorContrapartidaMunicipal = !String.IsNullOrEmpty(txtValorContraExercicio.Text) ? Convert.ToDecimal(txtValorContraExercicio.Text) : 0M;
            recurso.ContrapartidaMunicipal = rblContraPartida.SelectedValue == "1" ? true : false;

            #endregion


            recurso.Id = Convert.ToInt32(Session["idBeneficio"]);
            recurso.IdPrefeituraBeneficioEventual =  Convert.ToInt32(Session["idPrefeituraBeneficioEventual"]);

            obj.PrefeituraBeneficiosEventuaisRecursosFinanceiros.Add(recurso); 
            #endregion


         //   obj.BeneficiarioAtendidoRedeSocioAssistencial = rblIntegracaoRede.SelectedValue == "1";
            obj.IdFormaAuxilio = Convert.ToInt32(rblFormaAuxilio.SelectedValue);
            
            obj.Regulamentacao = rblRegulamentacao.SelectedValue == "1" ? true : false;


            if (obj.Regulamentacao)
            {
                if (chklTipoLegislacao.Checked)
                {
                    obj.IdTipoLegislacao = 1;

                    if (!String.IsNullOrEmpty(txtAnoLei.Text) && !String.IsNullOrEmpty(txtNumeroLei.Text))
                        obj.Lei = txtNumeroLei.Text + "/" + txtAnoLei.Text;
                    else
                        obj.Lei = "";

                    if (!String.IsNullOrEmpty(txtDataPublicacao.Text))
                        obj.DataPublicacaoLei = Convert.ToDateTime(txtDataPublicacao.Text);

                    if (rblAlteracaoLei.SelectedValue == "1")
                    {
                        obj.AlterouLei = true;


                        if (!String.IsNullOrEmpty(txtNumeroLeiAlteracao.Text) && !String.IsNullOrEmpty(txtNumeroLeiAlteracaoComplemento.Text))
                        {
                            obj.NumeroLeiAlterada = txtNumeroLeiAlteracao.Text + "/" + txtNumeroLeiAlteracaoComplemento.Text;
                        }
                        else
                        {
                            obj.NumeroLeiAlterada = String.Empty;
                        }

                        if (!String.IsNullOrEmpty(txtDataAlteracaoLei.Text))
                        {
                            obj.DataAlteracaoLei = Convert.ToDateTime(txtDataAlteracaoLei.Text);
                        }
                        else
                        {
                            obj.DataAlteracaoLei = null;
                        }
                            


                    }
                    else
                    {
                        obj.AlterouLei = null;
                        obj.NumeroLeiAlterada = String.Empty;
                        obj.DataAlteracaoLei = null;
                    }

                }
                else
                {
                    obj.Lei = "";
                    obj.DataPublicacaoLei = null;
                }

                if (chkResolucao.Checked)
                {
                   obj.Resolucao = true;

                    if (!String.IsNullOrEmpty(txtAnoResolucao.Text) && !String.IsNullOrEmpty(txtNumeroResolucao.Text))
                        obj.NumeroResolucao = txtNumeroResolucao.Text + "/" + txtAnoResolucao.Text;

                    if (!String.IsNullOrEmpty(txtDataResolucao.Text))
                        obj.DataResolucao = Convert.ToDateTime(txtDataResolucao.Text);

                    if (rblAlteracaoResolucao.SelectedValue == "1")
                    {
                        obj.AlterouResolucao = true;
                        
                        if (!String.IsNullOrEmpty(txtResocaoAlteracao.Text) && !String.IsNullOrEmpty(txtResocaoAlteracaoComplemento.Text))
	                    {
                            obj.NumeroResolucaoAlterada = txtResocaoAlteracao.Text + "/" + txtResocaoAlteracaoComplemento.Text;
                        }
                        else
                        {
                            obj.NumeroResolucaoAlterada = String.Empty;
                        }

                        if (!String.IsNullOrEmpty(txtDataAlteracaoResolucao.Text))
                        {
                             obj.DataAlteracaoResolucao = Convert.ToDateTime(txtDataAlteracaoResolucao.Text);
                        }
                        else
                        {
                            obj.DataAlteracaoResolucao = null;
                        }
                    }
                    else
                    {
                        obj.AlterouResolucao = false;
                        obj.NumeroResolucaoAlterada = String.Empty;
                        obj.DataAlteracaoResolucao = null;
                    }

                }
                else
                {
                    obj.NumeroResolucao = "";
                    obj.DataResolucao = null;     
                }

                if (chkDecreto.Checked)
                {
                    obj.Decreto = true;

                    if (!String.IsNullOrEmpty(txtDecretoPortaria.Text) && !String.IsNullOrEmpty(txtAnoDecretoPortaria.Text))
                        obj.NumeroDecreto = txtDecretoPortaria.Text + "/" + txtAnoDecretoPortaria.Text;
                    
                    if (!String.IsNullOrEmpty(txtDataDecretoPortaria.Text))
                        obj.DataDecreto = Convert.ToDateTime(txtDataDecretoPortaria.Text);

                    if (rblAlteracaoDecreto.SelectedValue == "1")
                    {
                        obj.AlterouDecreto = true;

                        if (!String.IsNullOrEmpty(txtDecretoAlteracao.Text) && !String.IsNullOrEmpty(txtDecretoAlteracaoComplemento.Text))
                        {
                            obj.NumeroDecretoAlterado =  txtDecretoAlteracao.Text + "/" + txtDecretoAlteracaoComplemento.Text;
                        }
                        else
                        {
                            obj.NumeroDecretoAlterado = String.Empty;
                        }

                        if (!String.IsNullOrEmpty(txtDataAlteracaoDecreto.Text))
                        {
                            obj.DataAlteracaoDecreto = Convert.ToDateTime(txtDataAlteracaoDecreto.Text);
                        }
                        else
                        {
                            obj.DataAlteracaoDecreto = null;
                        }


                    }
                    else
                    {
                        obj.AlterouDecreto = false;
                        obj.NumeroDecretoAlterado = String.Empty;
                        obj.DataAlteracaoDecreto = null;
                    }
                }
            }

            if (!String.IsNullOrEmpty(txtMediaSemestralBeneficiarios.Text))
                obj.MediaSemestralBeneficiarios = Convert.ToInt32(txtMediaSemestralBeneficiarios.Text);
            if (!String.IsNullOrEmpty(txtMediaSemestralBeneficiariosConcedidos.Text))
                obj.MediaSemestralBeneficiariosConcedidos = Convert.ToInt32(txtMediaSemestralBeneficiariosConcedidos.Text);

            //CRITÉRIOS
            obj.Criterios = new List<CriterioConcessaoInfo>();
            foreach (ListItem i in chkCriterios.Items)
                if (i.Selected)
                    obj.Criterios.Add(new CriterioConcessaoInfo() { Id = Convert.ToInt32(i.Value) });

            obj.Necessidades = new List<NecessidadeBeneficioEventualInfo>();
            foreach (ListItem i in chkNecessidades.Items)
            {
                if (i.Selected)
                {
                    obj.Necessidades.Add(new NecessidadeBeneficioEventualInfo() { Id = Convert.ToInt32(i.Value)});
                }
            }

            //RESPONSÁVEIS
            obj.OrgaosResponsaveis = new List<OrgaoResponsavelInfo>();
            foreach (ListItem i in chkResponsaveis.Items)
                if (i.Selected)
                    obj.OrgaosResponsaveis.Add(new OrgaoResponsavelInfo() { Id = Convert.ToInt32(i.Value) });

            //Executoras
            obj.UnidadesExecutoras = new List<UnidadePrivadaInfo>();
            foreach (ListItem i in chkUnidadeExecutora.Items)
                if (i.Selected)
                    obj.UnidadesExecutoras.Add(new UnidadePrivadaInfo() { Id = Convert.ToInt32(i.Value) });
            //obj.IdUnidadeExecutora = ddlUnidadeExecutora.SelectedValue != "0" ? Convert.ToInt32(ddlUnidadeExecutora.SelectedValue) : new Nullable<Int32>();

            //NECESSIDADES
            obj.Necessidades = new List<NecessidadeBeneficioEventualInfo>();
            if ((ETipoBeneficioEventual)obj.IdTipoBeneficioEventual == ETipoBeneficioEventual.AuxilioFuneral || (ETipoBeneficioEventual)obj.IdTipoBeneficioEventual == ETipoBeneficioEventual.AuxilioNatalidade)
            {
                foreach (ListItem i in chkNecessidades.Items)
                    if (i.Selected)
                        obj.Necessidades.Add(new NecessidadeBeneficioEventualInfo() { Id = Convert.ToInt32(i.Value) });
            }

            //BENEFICIOS
            obj.BeneficiosOferecidos = new List<BeneficioEventualInfo>();
            if ((ETipoBeneficioEventual)obj.IdTipoBeneficioEventual == ETipoBeneficioEventual.CalamidadePublica || (ETipoBeneficioEventual)obj.IdTipoBeneficioEventual == ETipoBeneficioEventual.VulnerabilidadeTemporaria)
            {
                foreach (ListItem i in chkBeneficiosOferecidos.Items)
                    if (i.Selected)
                        obj.BeneficiosOferecidos.Add(new BeneficioEventualInfo() { Id = Convert.ToInt32(i.Value), IdTipoBeneficioEventual = obj.IdTipoBeneficioEventual });
            }

            try
            {
                new ValidadorBeneficioEventual().Validar(obj);
                new ValidadorBeneficioEventual().ValidarRecurso(obj.PrefeituraBeneficiosEventuaisRecursosFinanceiros.FirstOrDefault());

                using (var proxy = new ProxyProgramas())
                {                    
                    proxy.Service.SaveBeneficioEventual(obj);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Response.Redirect("~/BlocoIII/CBeneficioEventual.aspx?msg=" + (obj.Id == 0 ? "BI" : "BU"));
        }

        protected void chkResponsaveis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkResponsaveis.Items[2].Selected)
            {
                trUnidadeExecutora.Visible = true;
                if (Page.IsPostBack) 
                {
                    chkUnidadeExecutora.DataValueField = "Id";
                    chkUnidadeExecutora.DataTextField = "RazaoSocial";
                    chkUnidadeExecutora.DataSource = new ProxyRedeProtecaoSocial().Service.GetIdentificacaoUnidadesPrivadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, String.Empty, 2);
                    chkUnidadeExecutora.DataBind();
                }
            }
            else
            {
                trUnidadeExecutora.Visible = false;
                chkUnidadeExecutora.Items.Clear();
            }
        }

        #region helper


        protected void btnLoadExercicio1_Click(object sender, EventArgs e)
        {

            hdfAno.Value = btnExercicio1.Text;

            #region reload
            ClearRecursosFinanceirosAplicados();
            int exercicioSolicitado = (String.IsNullOrEmpty(hdfAno.Value)) ? 2022 : Convert.ToInt32(hdfAno.Value);
            using (var proxy = new ProxyProgramas())
            {
                //AdicionarEventos();
                LoadRecursosFinanceiros(proxy);
            }
            #endregion

            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }
        protected void btnLoadExercicio2_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio2.Text;

            #region reload
            ClearRecursosFinanceirosAplicados();
            using (var proxy = new ProxyProgramas())
            {
                //AdicionarEventos();
                LoadRecursosFinanceiros(proxy);
            }

            #endregion

            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }

        protected void btnLoadExercicio3_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio3.Text;

            #region reload
            ClearRecursosFinanceirosAplicados();
            using (var proxy = new ProxyProgramas())
            {
                //AdicionarEventos();
                LoadRecursosFinanceiros(proxy);
            }

            #endregion

            //btnExercicio2.CssClass = "btn btn-info";
            //btnExercicio1.CssClass = "btn btn-primary";
            //btnExercicio3.CssClass = "btn btn-primary";
            //btnExercicio4.CssClass = "btn btn-primary";
            SelecionarCorAba();
            tbInconsistencias.Visible = false;
        }

        protected void btnLoadExercicio4_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio4.Text;

            #region reload
            ClearRecursosFinanceirosAplicados();
            using (var proxy = new ProxyProgramas())
            {
                //AdicionarEventos();
                LoadRecursosFinanceiros(proxy);
            }

            #endregion

            SelecionarCorAba();

            tbInconsistencias.Visible = false;
        }
        #endregion

        protected void chkDecreto_CheckedChanged(object sender, EventArgs e)
        {

            if (chkDecreto.Checked)
            {
                if (SessaoPmas.UsuarioLogado.EnumPerfil != EPerfil.Administrador)
                {
                    txtDecretoPortaria.Enabled = true;
                    txtAnoDecretoPortaria.Enabled = true;
                    txtDataDecretoPortaria.Enabled = true;                    
                }
            }
            else
            {
                txtDecretoPortaria.Enabled = false;
                txtAnoDecretoPortaria.Enabled = false;
                txtDataDecretoPortaria.Enabled = false;
            }

        }

        //protected void ddlTipoLegislacao_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlTipoLegislacao.SelectedValue == "1")
        //    {
        //        lblTipoLegislacao.Text = "Número da Lei :";
        //        lblTipoDataPublicacao.Text = "Data de publicação da Lei :";
        //    }
        //    else if (ddlTipoLegislacao.SelectedValue == "4")
        //    {
        //        lblTipoLegislacao.Text = "Número da Resolução :";
        //        lblTipoDataPublicacao.Text = "Data de publicação da Resolução :";
        //    }
        //}

        protected void chkResolucao_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResolucao.Checked)
            {
                if (SessaoPmas.UsuarioLogado.EnumPerfil != EPerfil.Administrador)
                {
                    txtAnoResolucao.Enabled = true;
                    txtNumeroResolucao.Enabled = true;
                    txtDataResolucao.Enabled = true;
                }
            }
            else
            {
                txtAnoResolucao.Enabled = false;
                txtNumeroResolucao.Enabled = false;
                txtDataResolucao.Enabled = false;

            }
        }

        protected void chklTipoLegislacao_CheckedChanged(object sender, EventArgs e)
        {
            if (chklTipoLegislacao.Checked)
            {
                if (SessaoPmas.UsuarioLogado.EnumPerfil != EPerfil.Administrador)
                {
                    txtAnoLei.Enabled = true;
                    txtNumeroLei.Enabled = true;
                    txtDataPublicacao.Enabled = true;
                }
            }
            else
            {
                txtAnoLei.Enabled = false;
                txtNumeroLei.Enabled = false;
                txtDataPublicacao.Enabled = false;
            }
        }

        protected void rblContraPartida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblContraPartida.SelectedValue == "1")
            {
                trValorContraExercicio.Visible = true;
            }
            else
            {
                trValorContraExercicio.Visible = false;
            }
        }

        protected void rblAlteracaoLei_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            if (rblAlteracaoLei.SelectedValue == "1")
            {
                tdAlteracaoLei.Visible = true;
                tdAlteracaoLeiData.Visible = true;
            }
            else
            {
                tdAlteracaoLei.Visible = false;
                tdAlteracaoLeiData.Visible = false;
            }


        }

        protected void rblAlteracaoResolucao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAlteracaoResolucao.SelectedValue == "1")
            {
                tdResolucaoAlterada.Visible = true;
                tdDataResolucaoAlterada.Visible = true;
            }
            else
            {
                tdResolucaoAlterada.Visible = false;
                tdDataResolucaoAlterada.Visible = false;
            }
        }

        protected void rblAlteracaoDecreto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAlteracaoDecreto.SelectedValue == "1")
            {
                tdDecretoAlterado.Visible = true;
                tdDataDecretoAlterada.Visible = true;
            }
            else
            {
                tdDecretoAlterado.Visible = false;
                tdDataDecretoAlterada.Visible = false;
            }
        }
    }
}