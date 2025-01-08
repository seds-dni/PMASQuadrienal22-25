using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoVI
{
    public partial class FDiretrizes : System.Web.UI.Page
    {

        #region properties
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024,2025 };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
           this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? "2022" : this.hdfAno.Value;
           
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    loadAnaliseDiagnostica(proxy,Convert.ToInt32(hdfAno.Value));
                    loadIntencaoAcao(proxy);
                }
                loadRHOrgaoGestor();
            }

            LoadExercicios();
        }

        private void LoadExercicios()
        {
            this.btnExercicio1.Text = FDiretrizes.Exercicios[0].ToString();
            this.btnExercicio2.Text = FDiretrizes.Exercicios[1].ToString();
            this.btnExercicio3.Text = FDiretrizes.Exercicios[2].ToString();
            this.btnExercicio4.Text = FDiretrizes.Exercicios[3].ToString();

            //exercicioTitulo();

            if (FDiretrizes.Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }
            if (FDiretrizes.Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FDiretrizes.Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FDiretrizes.Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-info-seds";
            }

        }

        private void CarregarLabelsPorExercicio(int exercicio)
        {
            if (FDiretrizes.Exercicios[0] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-info-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";

            }
            if (FDiretrizes.Exercicios[1] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-info-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FDiretrizes.Exercicios[2] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-info-seds";
                btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FDiretrizes.Exercicios[3] == exercicio)
            {
                btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                btnExercicio4.CssClass = "btn-seds btn-info-seds";
            }


        }

        private void loadRHOrgaoGestor()
        {
            using (var proxyPrefeituras = new ProxyPrefeitura())
            {


                //Somente 2018
                var orgaoGestor = proxyPrefeituras.Service.GetOrgaoGestorByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

               

                if (orgaoGestor.EquipesEspecificasTotais.Where(s => s.Exercicio == Convert.ToInt32(hdfAno.Value)).Count() != 0)
                {
                    
                
                    var equipeEspecifica = orgaoGestor.EquipesEspecificasTotais.Where(s => s.Exercicio == Convert.ToInt32(hdfAno.Value)).First();

                    
                    if (orgaoGestor.IntencoesEstruturacaoEquipe != null && equipeEspecifica != null)
                    {

                        var intencaoEstruturacao = orgaoGestor.IntencoesEstruturacaoEquipe.Where(x => x.Exercicio == Convert.ToInt32(hdfAno.Value)).FirstOrDefault();

                        if (intencaoEstruturacao != null)
                        {

                            if (equipeEspecifica.PossuiEquipeProtecaoBasica == 0)
                            {
                                if (intencaoEstruturacao.IntencaoAcaoEquipeBasica.HasValue && intencaoEstruturacao.IntencaoAcaoEquipeBasica.Value)
                                {
                                    lblEstrurarEquipeBasica.Text = "Foi informado que existe intenção de estruturar a equipe de proteção social básica no órgão gestor nos próximos anos.";
                                    trEquipeBasica.Visible = true;
                                }
                                else
                                {
                                    lblEstrurarEquipeBasica.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a proteção social básica";
                                    trEquipeBasica.Visible = true;// "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a proteção social básica";
                                }
                            }
                            else
                            {
                                trEquipeBasica.Visible = false;
                            }

                            if (equipeEspecifica.PossuiEquipeProtecaoEspecial == 0)
                            { 
                                     if (intencaoEstruturacao.IntencaoAcaoEquipeEspecial.HasValue && intencaoEstruturacao.IntencaoAcaoEquipeEspecial.Value)
                                     {
                                         lblEstrurarEquipeEspecial.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a proteção social especial";
                                         trEquipeEspecial.Visible = true;
                                     }
                                     else
                                     {
                                         lblEstrurarEquipeEspecial.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a proteção social especial";
                                         trEquipeEspecial.Visible = true;
                                     }
                            }
                            else
                            {
                                trEquipeEspecial.Visible = false;
                            }


                            if (equipeEspecifica.PossuiEquipeVigilanciaSocioassistencial == 0)
                            {
                                if (intencaoEstruturacao.IntencaoAcaoEquipeVigilanciaSocioAssistencial.HasValue && intencaoEstruturacao.IntencaoAcaoEquipeVigilanciaSocioAssistencial.Value)
                                {
                                    lblEstrurarEquipeVigilancia.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a vigilância socioassistencial";
                                    trEquipeVigilancia.Visible = true;
                                }
                                else
                                {
                                    lblEstrurarEquipeVigilancia.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a vigilância socioassistencial";
                                    trEquipeVigilancia.Visible = true; //lblEstrurarEquipeVigilancia.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a vigilância socioassistencial";
                                }
                            }
                            else
                            {
                                trEquipeVigilancia.Visible = false;
                            }


                            if (equipeEspecifica.PossuiEquipeGestorSUAS == 0)
                            {
                                if (intencaoEstruturacao.IntencaoAcaoEquipeGestorSUAS.HasValue && intencaoEstruturacao.IntencaoAcaoEquipeGestorSUAS.Value)
                                {
                                    trEquipeGestaodoSuas.Visible = true;
                                    lblEstrurarEquipeGestaoSuas.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a Gestão Suas.";
                                }
                                else
                                {
                                    trEquipeGestaodoSuas.Visible = true;
                                    lblEstrurarEquipeGestaoSuas.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a Gestão Suas.";
                                }
                            }
                            else
                            {
                                trEquipeGestaodoSuas.Visible = false;
                            }

                            if (equipeEspecifica.PossuiEquipeGestaoTransferenciaRenda == 0)
                            {
                                if (intencaoEstruturacao.IntencaoAcaoEquipeGestaoBeneficios.HasValue && intencaoEstruturacao.IntencaoAcaoEquipeGestaoBeneficios.Value)
                                {
                                    trEquipeGestaoBeneficios.Visible = true;
                                    lblEstrurarEquipeGestaoBeneficios.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão de benefícios/transferência de renda";
                                }
                                else
                                {
                                    trEquipeGestaoBeneficios.Visible = true;
                                    lblEstrurarEquipeGestaoBeneficios.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão de benefícios/transferência de renda";
                                }
                            }
                            else
                            {
                                trEquipeGestaoBeneficios.Visible = false;
                            }


                            if (equipeEspecifica.PossuiEquipeCadUnico == 0)
                            {

                                if (intencaoEstruturacao.IntencaoAcaoEquipeGestaoCadUnico.HasValue && intencaoEstruturacao.IntencaoAcaoEquipeGestaoCadUnico.Value)
                                {
                                    trEquipeCadUnico.Visible = true;
                                    lblEstrurarEquipeCadUnico.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão do cadastro único";
                                }
                                else
                                {
                                    trEquipeCadUnico.Visible = true;
                                    lblEstrurarEquipeCadUnico.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão do cadastro único";
                                }
                            }
                            else
                            {
                                trEquipeCadUnico.Visible = false;
                            }


                            if (equipeEspecifica.PossuiEquipeGestaoFinanceira == 0)
                            {
                                if (intencaoEstruturacao.IntencaoAcaoEquipeGestaoFinanceira.HasValue && intencaoEstruturacao.IntencaoAcaoEquipeGestaoFinanceira.Value)
                                {
                                    trEquipeGestaoFinanceira.Visible = true;
                                    lblEstrurarEquipeGestaoFinanceira.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão financeira e orçamentária";
                                }
                                else
                                {
                                    trEquipeGestaoFinanceira.Visible = true;
                                    lblEstrurarEquipeGestaoFinanceira.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão financeira e orçamentária";
                                }
                            }
                            else
                            {
                                trEquipeGestaoFinanceira.Visible = false;
                            }


                            if (equipeEspecifica.PossuiEquipeGestaoSUAS == 0)
                            {
                                if (intencaoEstruturacao.IntencaoAcaoEquipeGestaoSUAS.HasValue && intencaoEstruturacao.IntencaoAcaoEquipeGestaoSUAS.Value)
                                {
                                    trEquipeGestaoSuas.Visible = true;
                                    lblEstrurarEquipeTrabalho.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão do Trabalho no SUAS";
                                }
                                else
                                {
                                    trEquipeGestaoSuas.Visible = true;
                                    lblEstrurarEquipeTrabalho.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão do Trabalho no SUAS";
                                }
                            }
                            else
                            {
                                trEquipeGestaoSuas.Visible = false;
                            }


                            if (equipeEspecifica.PossuiEquipeRegulacaoSUAS == 0)
                            {
                                if (intencaoEstruturacao.IntencaoAcaoEquipeRegulacaoSUAS.HasValue && intencaoEstruturacao.IntencaoAcaoEquipeRegulacaoSUAS.Value)
                                {
                                    trEquipeRegulacaoSuas.Visible = true;
                                    lblEstrurarEquipeRegulacaoSuas.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a regulação do SUAS";
                                }
                                else
                                {
                                    trEquipeRegulacaoSuas.Visible = true;
                                    lblEstrurarEquipeRegulacaoSuas.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a regulação do SUAS";
                                }
                            }
                            else
                            {
                                trEquipeRegulacaoSuas.Visible = false;
                            }


                            if (intencaoEstruturacao.IntencaoAcaoEquipeRedeDireta.HasValue && intencaoEstruturacao.IntencaoAcaoEquipeRedeDireta.Value)
                            {
                                trEquipeRedeDireta.Visible = false;
                             
                            }
                            else
                            {
                                trEquipeRedeDireta.Visible = false;
                             
                            }

                            if (intencaoEstruturacao.IntencaoAcaoOrgaoGestor.HasValue && intencaoEstruturacao.IntencaoAcaoOrgaoGestor.Value)
                            {
                                trEquipeOrgaoGestor.Visible = true;
                                lblAumentarEquipeOrgaoGestor.Text = "Foi informado que existe intenção de aumentar o número de trabalhadores do órgão gestor nos próximos anos.";
                            }
                            else
                            {
                                trEquipeOrgaoGestor.Visible = true;
                                lblAumentarEquipeOrgaoGestor.Text = "Não existe intenção de aumentar o número de trabalhadores do órgão gestor nos próximos anos";
                            }
                        }
                        else
                        {
                            lblEstrurarEquipeBasica.Text = "";
                            trEquipeBasica.Visible = false;

                            lblEstrurarEquipeEspecial.Text = "";
                            trEquipeEspecial.Visible = false;

                            lblEstrurarEquipeVigilancia.Text = "";
                            trEquipeVigilancia.Visible = false;

                            trEquipeGestaoBeneficios.Visible = false;
                            lblEstrurarEquipeGestaoBeneficios.Text = "";

                            trEquipeCadUnico.Visible = false;
                            lblEstrurarEquipeCadUnico.Text = "";

                            trEquipeGestaoFinanceira.Visible = false;
                            lblEstrurarEquipeGestaoFinanceira.Text = "";

                            trEquipeGestaoSuas.Visible = false;
                            lblEstrurarEquipeTrabalho.Text = "";

                            trEquipeRegulacaoSuas.Visible = false;
                            lblEstrurarEquipeRegulacaoSuas.Text = "";

                            trEquipeRedeDireta.Visible = false;
                            lblAumentarEquipeRedeDireta.Text = "";

                            trEquipeOrgaoGestor.Visible = false;
                            lblAumentarEquipeOrgaoGestor.Text = "";

                        }
                    }
                }
            }
        }

        void LimparComunidades() 
        {
            lblCigano.Text = "";
            lblExtrativista.Text = "";
            lblPescadores.Text = "";
            lblAfro.Text = "";
            lblRibeirinha.Text = "";
            lblIndigenas.Text = "";
            lblQuilombolas.Text = "";

            trCiganos.Visible = false;
            trExtrativistas.Visible = false;
            trPescadores.Visible = false;
            trAfro.Visible = false;
            trRibeirinha.Visible = false;
            trIndigenas.Visible = false;
            trQuilombolas.Visible = false;
        }

        void LimparGrupos() 
        {
            lblAgricultores.Text = "";
            lblAcampamentos.Text = "";
            lblPopulacaoPrisional.Text = "";
            lblTrabalhadoresSazonais.Text = "";
            lblAglomeradosSubnormais.Text = "";
            lblAssentamentos.Text = "";

            trAgricultores.Visible = false;
            trAcampamentos.Visible = false;
            trPopulacaoPrisional.Visible = false;
            trTrabalhadoresSazonais.Visible = false;
            trAglomeradosSubnormais.Visible = false;
            trAssentamentos.Visible = false;
        }

        void loadAnaliseDiagnostica(ProxyRedeProtecaoSocial proxy,int Exercicio)
        {
            int idExercicio = 0; 

            switch (Exercicio)
            {
                case 2022:
                    idExercicio = 5;
                    break;

                case 2023:
                    idExercicio = 6;
                    break;

                case 2024:
                    idExercicio = 7;
                    break;
                
                case 2025:
                    idExercicio = 8;
                    break;

                default:
                    idExercicio = 5;
                    break;
             }

            /*var lst = proxy.Service.GetConsultaAnaliseDiagnosticaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(s => s.IdExercicio == idExercicio).OrderBy(t => t.Classificacao);
            lstAnaliseDiagnostica.DataSource = lst;
            lstAnaliseDiagnostica.DataBind();*/

            var lst = proxy.Service.GetAnaliseDiagnosticaByPrefeituraExercicio(SessaoPmas.UsuarioLogado.Prefeitura.Id,Exercicio);
            lstAnaliseDiagnostica.DataSource = lst;
            lstAnaliseDiagnostica.DataBind();



            var comunidade = proxy.Service.GetAnaliseDiagnosticaComunidadeByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id,idExercicio);
            if (comunidade != null)
            {
                if (comunidade.ExisteCigano)
                {
                    trCiganos.Visible = true;
                    if (comunidade.NumeroCiganos > 1)
                        lblCigano.Text = "Ciganos - estimativa de " + comunidade.NumeroCiganos  + " famílias no município.";
                    else
                        lblCigano.Text = "Ciganos - estimativa de " + comunidade.NumeroCiganos + " família no município.";
                }
                if (comunidade.ExisteExtrativista)
                {
                    trExtrativistas.Visible = true;
                    if (comunidade.NumeroExtrativistas > 1)
                        lblExtrativista.Text = "Extrativistas - estimativa de " + comunidade.NumeroExtrativistas + " famílias no município.";
                    else
                        lblExtrativista.Text = "Extrativistas - estimativa de " + comunidade.NumeroExtrativistas + " família no município.";
                }
                if (comunidade.ExistePescador)
                {
                    trPescadores.Visible = true;
                    if (comunidade.NumeroPescadores > 1)
                        lblPescadores.Text = "Pescadores Artesanais - estimativa de " + comunidade.NumeroPescadores + " famílias no município.";
                    else
                        lblPescadores.Text = "Pescadores Artesanais - estimativa de " + comunidade.NumeroPescadores + " família no município.";
                }
                if (comunidade.ExisteAfro)
                {
                    trAfro.Visible = true;
                    if (comunidade.NumeroAfros > 1)
                        lblAfro.Text = "Comunidade tradicional de matriz africana - estimativa de " + comunidade.NumeroAfros + " famílias no município.";
                    else
                        lblAfro.Text = "Comunidade tradicional de matriz africana - estimativa de " + comunidade.NumeroAfros + " família no município.";
                }
                if (comunidade.ExisteRibeirinha)
                {
                    trRibeirinha.Visible = true;
                    if (comunidade.NumeroRibeirinhas > 1)
                        lblRibeirinha.Text = "Comunidade ribeirinha - estimativa de " + comunidade.NumeroRibeirinhas + " famílias no município.";
                    else
                        lblRibeirinha.Text = "Comunidade ribeirinha - estimativa de " + comunidade.NumeroRibeirinhas + " família no município.";
                }
                if (comunidade.ExisteIndigena)
                {
                    trIndigenas.Visible = true;
                    if (comunidade.NumeroIndigenas > 1)
                        lblIndigenas.Text = "Indígenas - estimativa de " + comunidade.NumeroIndigenas + " famílias no município.";
                    else
                        lblIndigenas.Text = "Indígenas - estimativa de " + comunidade.NumeroIndigenas + " família no município.";
                }
                if (comunidade.ExisteQuilombola)
                {
                    trQuilombolas.Visible = true;
                    if (comunidade.NumeroQuilombolas > 1)
                        lblQuilombolas.Text = "Quilombolas - estimativa de " + comunidade.NumeroQuilombolas + " famílias no município.";
                    else
                        lblQuilombolas.Text = "Quilombolas - estimativa de " + comunidade.NumeroQuilombolas + " família no município.";
                }
                if (comunidade.NaoExisteComunidade)
                {
                    trNenhumaComunidade.Visible = true;
                    lblNenhumaComunidade.Text = "Não existe no município nenhuma comunidade tradicional.";
                    LimparComunidades();
                }
                else
                {
                    trNenhumaComunidade.Visible = false;   
                }

                if (comunidade.NaoExisteGrupo)
                {
                    trNaoExisteGrupo.Visible = true;
                    lblNaoExisteGrup.Text = "Não existe no município nenhum grupo específico.";
                    LimparGrupos();
                }
                else
                {
                    trNaoExisteGrupo.Visible = false;
                }

                if (comunidade.ExisteAgricultor)
                {
                    trAgricultores.Visible = true;
                    if (comunidade.NumeroAgricultores > 1)
                        lblAgricultores.Text = "Agricultores familiares - estimativa de " + comunidade.NumeroAgricultores + " famílias no município.";
                    else
                        lblAgricultores.Text = "Agricultores familiares - estimativa de " + comunidade.NumeroAgricultores + " família no município.";

                }
                if (comunidade.ExisteAcampamento)
                {
                    trAcampamentos.Visible = true;
                    if (comunidade.NumeroAcampamentos > 1)
                        lblAcampamentos.Text = "Acampamentos - estimativa de  " + comunidade.NumeroAcampamentos + " famílias no município.";
                    else
                        lblAcampamentos.Text = "Acampamentos - estimativa de  " + comunidade.NumeroAcampamentos + " família no município.";

                }
                if (comunidade.ExisteInstalacaoPrisional)
                {
                    trPopulacaoPrisional.Visible = true;
                    if (comunidade.NumeroInstalacoesPrisionais > 1)
                        lblPopulacaoPrisional.Text = "População flutuante decorrente de instalação prisional - estimativa de " + comunidade.NumeroInstalacoesPrisionais + " famílias no município.";
                    else
                        lblPopulacaoPrisional.Text = "População flutuante decorrente de instalação prisional - estimativa de " + comunidade.NumeroInstalacoesPrisionais + " família no município.";

                }
                if (comunidade.ExisteTrabalhoSazonal)
                {
                    trTrabalhadoresSazonais.Visible = true;
                    if (comunidade.NumeroTrabalhoSazonais > 1)
                        lblTrabalhadoresSazonais.Text = "Trabalhadores sazonais - estimativa de " + comunidade.NumeroTrabalhoSazonais + " famílias no município.";
                    else
                        lblTrabalhadoresSazonais.Text = "Trabalhadores sazonais - estimativa de " + comunidade.NumeroTrabalhoSazonais + " família no município.";

                }
                if (comunidade.ExisteAglomeradoSubnormal)
                {
                    trAglomeradosSubnormais.Visible = true;
                    if (comunidade.NumeroAglomeradoSubnormais > 1)
                        lblAglomeradosSubnormais.Text = "Aglomerados subnormais - estimativa de " + comunidade.NumeroAglomeradoSubnormais + " famílias no município.";
                    else
                        lblAglomeradosSubnormais.Text = "Aglomerados subnormais - estimativa de  " + comunidade.NumeroAglomeradoSubnormais + " família no município.";

                }
                if (comunidade.ExisteAssentamentoPrecario)
                {
                    trAssentamentos.Visible = true;
                    if (comunidade.NumeroAssentamentoPrecarios > 1)
                        lblAssentamentos.Text = "Assentamentos precários e/ou irregulares - estimativa de " + comunidade.NumeroAssentamentoPrecarios + " famílias no município.";
                    else
                        lblAssentamentos.Text = "Assentamentos precários e/ou irregulares - estimativa de " + comunidade.NumeroAssentamentoPrecarios + " família no município.";

                }

            }
            else
            {

                trCiganos.Visible = false;
                trExtrativistas.Visible = false;
                trPescadores.Visible = false;
                trAfro.Visible = false;
                trRibeirinha.Visible = false;
                trIndigenas.Visible = false;
                trQuilombolas.Visible = false;
                trAgricultores.Visible = false;
                trAcampamentos.Visible = false;
                trPopulacaoPrisional.Visible = false;
                trTrabalhadoresSazonais.Visible = false;
                trAglomeradosSubnormais.Visible = false;
                trAssentamentos.Visible = false;

                trNenhumaComunidade.Visible = true;
                lblNenhumaComunidade.Text = "Não existe no município nenhuma comunidade tradicional.";

                trNaoExisteGrupo.Visible = true;
                lblNaoExisteGrup.Text = "Não existe no município nenhum grupo específico.";
            }

        }

        void loadIntencaoAcao(ProxyRedeProtecaoSocial proxy)
        {
            var lst = proxy.Service.GetConsultaConsultaIntencaoAcoesByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(s => s.Desativado != true).OrderBy(t => t.IdUnidade);
            lstIntencaoAcao.DataSource = lst;
            lstIntencaoAcao.DataBind();

            var lstServico = proxy.Service.GetConsultaIntecaoServicosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id).GroupBy(s => s.ProtecaoSocial).Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) }).OrderBy(s => s.Key).ToList();     //.OrderBy(t => t.IdTipoProtecao);
            lstServicosSocioassistencias.DataSource = lstServico;
            lstServicosSocioassistencias.DataBind();
        }

        protected void lstAnaliseDiagnostica_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstAnaliseDiagnostica.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idAnalise = Genericos.clsCrypto.Decrypt(Convert.ToString(id));
            var exercicio = hdfAno.Value;
            var idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            Response.Redirect("~/BlocoVI/FDiretrizesServicos.aspx?id=" + Server.UrlEncode(id) + "&idAnalise=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idAnalise)) + "&idPrefeitura=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(Convert.ToString(idPrefeitura))) + "&Exercicio=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(Convert.ToString(exercicio))));
        }

        protected void btnExercicio1_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[0].ToString();

            CarregarLabelsPorExercicio(Exercicios[0]);

            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadAnaliseDiagnostica(proxy, Convert.ToInt32(hdfAno.Value));
                loadRHOrgaoGestor();
            }

        }

        protected void btnExercicio2_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[1].ToString();

            CarregarLabelsPorExercicio(Exercicios[1]);

            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadAnaliseDiagnostica(proxy, Convert.ToInt32(hdfAno.Value));
                loadRHOrgaoGestor();
            }
        }

        protected void btnExercicio3_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[2].ToString();

            CarregarLabelsPorExercicio(Exercicios[2]);

            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadAnaliseDiagnostica(proxy, Convert.ToInt32(hdfAno.Value));
                loadRHOrgaoGestor();
            }
        }

        protected void btnExercicio4_Click(object sender, EventArgs e)
        {
            hdfAno.Value = Exercicios[3].ToString();

            CarregarLabelsPorExercicio(Exercicios[3]);

            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadAnaliseDiagnostica(proxy, Convert.ToInt32(hdfAno.Value));
                loadRHOrgaoGestor();
            }
        }


    }
}