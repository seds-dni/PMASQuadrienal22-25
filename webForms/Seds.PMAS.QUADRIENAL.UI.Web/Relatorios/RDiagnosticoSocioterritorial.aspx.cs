using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Relatorios
{
    public partial class RDiagnosticoSocioterritorial : System.Web.UI.Page
    {
        NumberFormatInfo nfi_nodigit = new CultureInfo("pt-BR", false).NumberFormat;
        #region propriedades
        private static List<int> AtualizacoesExercicio = new List<int> { 2022, 2023, 2024, 2025 };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            carregarPagina();
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);
                carregarDados();
            }
        }

        private void carregarDados()
        {

            var filtro = new RelatorioFiltroInfo();
            filtro.IdMunicipio = Session["RELATORIO_MUNICIPIO"] as int?;
            var id = new ProxyDivisaoAdministrativa().Service.GetMunicipioById(Convert.ToInt32(filtro.IdMunicipio));
            var idDrad = new ProxyDivisaoAdministrativa().Service.GetDradsById(Convert.ToInt32(id.IdDrads));



            filtro.IdDrad = idDrad.Id;
            Master.mostrarFiltros(filtro, ETipoRelatorio.Descritivo);
            if (filtro.IdMunicipio.HasValue)
            {
                //  Int32 versaoPMAS = Convert.ToInt32(SessaoPmas.VersaoPMAS);
                var demografia = new Seds.PMAS.QUADRIENAL.Negocio.AnaliseDiagnostica().GetDemografiaIndicadoresByMunicipio(filtro.IdMunicipio.Value, 2018);
                preencherDemografia(demografia);
                var ind = new Seds.PMAS.QUADRIENAL.Negocio.AnaliseDiagnostica().GetPopulacaoVulnerabilidadeByMunicipio(filtro.IdMunicipio.Value, 2018);
                preencherPopulacaoVulerabilidade(ind);
                var evo = new Seds.PMAS.QUADRIENAL.Negocio.AnaliseDiagnostica().GetIndicadoresRedeSocioAssistencial(filtro.IdMunicipio.Value, 2018);
                preencherEvolucaoAtendimento(evo);

                using (var proxy = new ProxyPrefeitura())
                {
                    var pre = proxy.Service.GetByIdMunicipio(filtro.IdMunicipio.Value);
                    var prefeitura = proxy.Service.GetPrefeituraById(pre.Id);
                    foreach (var atualizacaoAnual in prefeitura.PrefeituraAtualizacoesAnuais)
                    {
                        string atualizacaoTexto = !String.IsNullOrEmpty(atualizacaoAnual.AtualizacaoAnual) ? atualizacaoAnual.AtualizacaoAnual : String.Empty;

                        //if (atualizacaoAnual.Exercicio.Value == FAtualizacaoDiagnostica.AtualizacoesExercicio[0])
                        //{
                        //    txtAtualizacaoExercicio1.Text = atualizacaoTexto;
                        //}
                        if (atualizacaoAnual.Exercicio.Value ==RDiagnosticoSocioterritorial.AtualizacoesExercicio[1])
                        {
                           lblAualizacao2018.Text = atualizacaoTexto;
                        }
                   
                        //if (atualizacaoAnual.Exercicio.Value == FAtualizacaoDiagnostica.AtualizacoesExercicio[3])
                        //{
                        //    //txtAtualizacaoExercicio4.Text = atualizacaoTexto;
                        //}
                    }



                   

                
                    if (pre == null)
                        return;
                    lblCaracterizacaoEvolucaoRede.Text = pre.CaracterizacaoRedeSocioassistencial;
                    lblCaracterizacaoPopulacao.Text = pre.CaracterizacaoPopulacao;
                    lblCaracterizacao.Text = pre.Caracterizacao;
                    lblAnaliseInterpretacao.Text = pre.CaracterizacaoAnaliseInterpretacao;
                    using (var redeprotecao = new ProxyRedeProtecaoSocial())
                    {
                        var lst = redeprotecao.Service.GetConsultaAnaliseDiagnosticaByPrefeitura(pre.Id).OrderBy(t => t.Classificacao);
                        lstAnaliseDiagnostica.DataSource = lst;
                        lstAnaliseDiagnostica.DataBind();

                        var lstIntencao = redeprotecao.Service.GetConsultaConsultaIntencaoAcoesByPrefeitura(pre.Id).OrderBy(t => t.IdUnidade);
                        lstIntencaoAcao.DataSource = lstIntencao;
                        lstIntencaoAcao.DataBind();

                        var servicos = redeprotecao.Service.GetConsultaIntecaoServicosByPrefeitura(pre.Id).GroupBy(s => s.ProtecaoSocial).Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) }).OrderBy(s => s.Key).ToList();     //.OrderBy(t => t.IdTipoProtecao);
                        lstServicosSocioassistencias.DataSource = servicos;
                        lstServicosSocioassistencias.DataBind();

                        var comunidade = redeprotecao.Service.GetAnaliseDiagnosticaComunidadeByPrefeitura(pre.Id,5);
                        if (comunidade != null)
                            preencherAnaliseDiagnostica(comunidade);

                        var rh = proxy.Service.GetOrgaoGestorByPrefeitura(pre.Id);
                        if (rh != null)
                            loadRHOrgaoGestor(rh);

                    }




                }


            }

        }

        private void preencherEvolucaoAtendimento(ConsultaMunicipioRedeSocioAssistencialIndicadoresInfo evo)
        {
            lblNumServicosBasica2013.Text = evo.NumeroServicosSociosAssisteciaisPSB2018.ToString();
            lblNumServicosBasica2014.Text = evo.NumeroServicosSociosAssisteciaisPSB2019.ToString();
            lblNumServicosBasica2015.Text = evo.NumeroServicosSociosAssisteciaisPSB2020.ToString();
            lblNumServicosMedia2013.Text = evo.NumeroServicosPSEMediaComplexidade2018.ToString();
            lblNumServicosMedia2014.Text = evo.NumeroServicosPSEMediaComplexidade2019.ToString();
            lblNumServicosMedia2015.Text = evo.NumeroServicosPSEMediaComplexidade2020.ToString();
            lblNumServicosAlta2013.Text = evo.NumeroServicoPSEAltaComplexidade2018.ToString();
            lblNumServicosAlta2014.Text = evo.NumeroServicoPSEAltaComplexidade2019.ToString();
            lblNumServicosAlta2015.Text = evo.NumeroServicoPSEAltaComplexidade2020.ToString();
            lblServicoNaoTipificados2013.Text = evo.NumeroServicoNaoTipificados2018.ToString();
            lblServicoNaoTipificados2014.Text = evo.NumeroServicoNaoTipificados2019.ToString();
            lblServicoNaoTipificados2015.Text = evo.NumeroServicoNaoTipificados2020.ToString();
            lblCRAS2013.Text = evo.NumeroCRASImplantadosMunicipios2018.HasValue ? evo.NumeroCRASImplantadosMunicipios2018.Value.ToString("N", nfi_nodigit) : "-";
            lblCRAS2014.Text = evo.NumeroCRASImplantadosMunicipios2019.HasValue ? evo.NumeroCRASImplantadosMunicipios2019.Value.ToString("N", nfi_nodigit) : "-";
            lblCRAS2015.Text = evo.NumeroCRASImplantadosMunicipios2020.HasValue ? evo.NumeroCRASImplantadosMunicipios2020.Value.ToString("N", nfi_nodigit) : "-";
            lblCRAS2013.Text = evo.NumeroCREASImplantadosMunicipios2018.HasValue ? evo.NumeroCREASImplantadosMunicipios2018.Value.ToString("N", nfi_nodigit) : "-";
            lblCREAS2014.Text = evo.NumeroCREASImplantadosMunicipios2019.HasValue ? evo.NumeroCREASImplantadosMunicipios2019.Value.ToString("N", nfi_nodigit) : "-";
            lblCREAS2015.Text = evo.NumeroCREASImplantadosMunicipios2020.HasValue ? evo.NumeroCREASImplantadosMunicipios2020.Value.ToString("N", nfi_nodigit) : "-";
            lblCentroPOP2013.Text = evo.NumeroCENTROPOPImplantadosMunicipios2020.HasValue ? evo.NumeroCENTROPOPImplantadosMunicipios2020.Value.ToString("N", nfi_nodigit) : "-";

            lblBPCIdosos2013.Text = evo.TotalBeneficiariosBPCIdoso2018.ToString();
            lblBPCIdosos2014.Text = evo.TotalBeneficiariosBPCIdoso2019.ToString();
            lblBPCIdosos2015.Text = evo.TotalBeneficiariosBPCIdoso2020.ToString();
            lblBPCDeficentes2013.Text = evo.TotalBeneficiariosBPCDeficientes2018.ToString();
            lblBPCDeficentes2014.Text = evo.TotalBeneficiariosBPCDeficientes2019.ToString();
            lblBPCDeficentes2015.Text = evo.TotalBeneficiariosBPCDeficientes2020.ToString();
        }

        private void preencherPopulacaoVulerabilidade(ConsultaMunicipioPopulacaoVulnerabilidadeIndicadoresInfo populacaoVulnerabilidade)
        {
            nfi_nodigit.NumberDecimalDigits = 0;


            lblNumPessoasAbaixoQuinzeAnosMunicipio.Text = populacaoVulnerabilidade.PessoasAbaixo15Anos.HasValue ? populacaoVulnerabilidade.PessoasAbaixo15Anos.Value.ToString("N", nfi_nodigit) : "-";
            lblNumPessoasAbaixoQuinzeAnosDRADS.Text = populacaoVulnerabilidade.PessoasAbaixo15AnosDRADS.HasValue ? populacaoVulnerabilidade.PessoasAbaixo15AnosDRADS.Value.ToString("N", nfi_nodigit) : "-";

            lblPercPessoasAbaixoQuinzeAnosMunicipio.Text = populacaoVulnerabilidade.PessoasAbaixo15AnosPercentual.HasValue ? populacaoVulnerabilidade.PessoasAbaixo15AnosPercentual.Value.ToString("N2") : "-";
            lblPercPessoasAbaixoQuinzeAnosDRADS.Text = populacaoVulnerabilidade.PessoasAbaixo15AnosPercentualDRADS.HasValue ? populacaoVulnerabilidade.PessoasAbaixo15AnosPercentualDRADS.Value.ToString("N2") : "-";

            lblNumPopulacaoSessentaAnosMunicipio.Text = populacaoVulnerabilidade.PessoasAcima60Anos.HasValue ? populacaoVulnerabilidade.PessoasAcima60Anos.Value.ToString("N", nfi_nodigit) : "-";
            lblNumPopulacaoSessentaAnosDRADS.Text = populacaoVulnerabilidade.PessoasAcima60AnosDRADS.HasValue ? populacaoVulnerabilidade.PessoasAcima60AnosDRADS.Value.ToString("N", nfi_nodigit) : "-";

            lblPerPopulacaoSessentaAnosMunicipio.Text = populacaoVulnerabilidade.PessoasAcima60AnosPercentual.HasValue ? populacaoVulnerabilidade.PessoasAcima60AnosPercentual.Value.ToString("N2") : "-";
            lblPercPopulacaoSessentaAnosDRADS.Text = populacaoVulnerabilidade.PessoasAcima60AnosPercentualDRADS.HasValue ? populacaoVulnerabilidade.PessoasAcima60AnosPercentualDRADS.Value.ToString("N2") : "-";

            lblRazaoDependenciaMunicipio.Text = populacaoVulnerabilidade.PercTotalRazaoDependencia.HasValue ? populacaoVulnerabilidade.PercTotalRazaoDependencia.Value.ToString("N2") : "-";
            lblRazaoDependenciaDRADS.Text = populacaoVulnerabilidade.PercTotalRazaoDependenciaDRADS.HasValue ? populacaoVulnerabilidade.PercTotalRazaoDependenciaDRADS.Value.ToString("N2") : "-";

            lblIndiceEnvelhecimentoMunicipio.Text = populacaoVulnerabilidade.IndiceEnvelhecimento.HasValue ? populacaoVulnerabilidade.IndiceEnvelhecimento.Value.ToString("N2") : "-";
            lblIndiceEnvelhecimentoDRADS.Text = populacaoVulnerabilidade.IndiceEnvelhecimentoDRADS.HasValue ? populacaoVulnerabilidade.IndiceEnvelhecimentoDRADS.Value.ToString("N2") : "-";

            lblRazaoDependenciaMunicipio.Text = populacaoVulnerabilidade.PercTotalRazaoDependencia.HasValue ? populacaoVulnerabilidade.PercTotalRazaoDependencia.Value.ToString("N2") : "-";
            lblRazaoDependenciaEstado.Text = populacaoVulnerabilidade.PercTotalRazaoDependenciaDRADS.HasValue ? populacaoVulnerabilidade.PercTotalRazaoDependenciaDRADS.Value.ToString("N2") : "-";

        }

        private void preencherDemografia(ConsultaDemografiaTerritorioIndicadoresInfo demografia)
        {
            nfi_nodigit.NumberDecimalDigits = 0;
            lblAreaTerritorial.Text = demografia.AreaTerritorial.ToString();
            lblNumeroHabitantes.Text = demografia.NumeroHabitantes.ToString("N", nfi_nodigit);
            lblTaxaGeometrica.Text = demografia.TaxaGeometricaCrescimento.HasValue ? demografia.TaxaGeometricaCrescimento.Value.ToString("N2") : "-";
            lblDomicilios.Text = demografia.TotalDomiciliosParticularesPermanentes.ToString("N", nfi_nodigit);
            lblDensidade.Text = demografia.DensidadeDemografica.HasValue ? demografia.DensidadeDemografica.Value.ToString("N2") : "-";
            // lblPessoasDeficienciaMunicipio.Text = demografia.NumeroPessoasDomicilios.HasValue ? demografia.NumeroPessoasDomicilios.Value.ToString("N2") : "-";
            lblGrauUrbanizacao.Text = demografia.GrauUrbanizacao.HasValue ? demografia.GrauUrbanizacao.Value.ToString("N2") : "-";
            lblAreaTerritorialDRADS.Text = demografia.AreaTerritorialDRADS.Value.ToString("N2");
            lblNumeroHabitantesDRADS.Text = demografia.NumeroHabitantesDRADS.ToString("N", nfi_nodigit);
            lblTaxaGeometricaDRADS.Text = demografia.TaxaGeometricaCrescimentoDRADS.HasValue ? demografia.TaxaGeometricaCrescimentoDRADS.Value.ToString("N2") : "-";
            lblDomiciliosDRADS.Text = demografia.TotalDomiciliosParticularesPermanentesDRADS.ToString("N", nfi_nodigit);
            lblDensidadeDRADS.Text = demografia.DensidadeDemograficaDRADS.HasValue ? demografia.DensidadeDemograficaDRADS.Value.ToString("N2") : "-";
            lblGrauUrbanizacaoDRADS.Text = demografia.GrauUrbanizacaoDRADS.HasValue ? demografia.GrauUrbanizacaoDRADS.Value.ToString("N2") : "-";
            lblPessoasDomiciliosDrads.Text = demografia.NumeroPessoasDomiciliosDRADS.HasValue ? demografia.NumeroPessoasDomiciliosDRADS.Value.ToString("N2") : "-";
        }

        void preencherAnaliseDiagnostica(AnaliseDiagnosticaComunidadeInfo comunidade)
        {

            if (comunidade != null)
            {
                if (comunidade.ExisteCigano)
                {
                    trCiganos.Visible = true;
                    if (comunidade.NumeroCiganos > 1)
                        lblCigano.Text = "Ciganos - estimativa de " + comunidade.NumeroCiganos + " famílias no município.";
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
                    trCiganos.Visible = true;
                    if (comunidade.NumeroIndigenas > 1)
                        lblIndigenas.Text = "Indígenas - estimativa de " + comunidade.NumeroRibeirinhas + " famílias no município.";
                    else
                        lblIndigenas.Text = "Indígenas - estimativa de " + comunidade.NumeroRibeirinhas + " família no município.";
                }
                if (comunidade.ExisteQuilombola)
                {
                    trQuilombolas.Visible = true;
                    if (comunidade.NumeroQuilombolas > 1)
                        lblQuilombolas.Text = "Quilombolas - estimativa de " + comunidade.NumeroQuilombolas + " famílias no município.";
                    else
                        lblQuilombolas.Text = "Quilombolas - estimativa de " + comunidade.NumeroIndigenas + " família no município.";
                }
                if (comunidade.NaoExisteComunidade)
                {
                    trNenhumaComunidade.Visible = true;
                    lblNenhumaComunidade.Text = "Não existe no município nenhuma comunidade tradicional.";
                }

                if (comunidade.NaoExisteGrupo)
                {
                    trNaoExisteGrupo.Visible = true;
                    lblNaoExisteGrup.Text = "Não existe no município nenhum grupo específico.";
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

        }

        private void loadRHOrgaoGestor(OrgaoGestorInfo orgaoGestor)
        {

            //Somente 2018
            var estruturacaoEquipe = orgaoGestor.IntencoesEstruturacaoEquipe.Where(x => x.Exercicio == 2018).FirstOrDefault();

            using (var proxyPrefeituras = new ProxyPrefeitura())
            {


                if (estruturacaoEquipe != null)
                {
                    if (estruturacaoEquipe.IntencaoAcaoEquipeBasica.HasValue && estruturacaoEquipe.IntencaoAcaoEquipeBasica.Value)
                    {
                        lblEstrurarEquipeBasica.Text = "Foi informado que existe intenção de estruturar a equipe de proteção social básica no órgão gestor nos próximos anos.";
                        trEquipeBasica.Visible = true;
                    }
                    else
                    {
                        trEquipeBasica.Visible = false;// "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a proteção social básica";
                    }

                    if (estruturacaoEquipe.IntencaoAcaoEquipeEspecial.HasValue && estruturacaoEquipe.IntencaoAcaoEquipeEspecial.Value)
                    {
                        lblEstrurarEquipeEspecial.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a proteção social especial";
                        trEquipeEspecial.Visible = true;
                    }
                    else
                    {
                        trEquipeEspecial.Visible = false;
                    }
                    // lblEstrurarEquipeEspecial.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a proteção social especial";

                    if (estruturacaoEquipe.IntencaoAcaoEquipeVigilanciaSocioAssistencial.HasValue && estruturacaoEquipe.IntencaoAcaoEquipeVigilanciaSocioAssistencial.Value)
                    {
                        lblEstrurarEquipeVigilancia.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a vigilância socioassistencial";
                        trEquipeVigilancia.Visible = true;
                    }
                    else
                        trEquipeVigilancia.Visible = false; //lblEstrurarEquipeVigilancia.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a vigilância socioassistencial";

                    if (estruturacaoEquipe.IntencaoAcaoEquipeGestaoBeneficios.HasValue && estruturacaoEquipe.IntencaoAcaoEquipeGestaoBeneficios.Value)
                    {
                        trEquipeGestaoBeneficios.Visible = true;
                        lblEstrurarEquipeGestaoBeneficios.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão de benefícios/transferência de renda";
                    }
                    else
                        trEquipeGestaoBeneficios.Visible = false;
                    //   lblEstrurarEquipeGestaoBeneficios.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão de benefícios/transferência de renda";

                    if (estruturacaoEquipe.IntencaoAcaoEquipeGestaoCadUnico.HasValue && estruturacaoEquipe.IntencaoAcaoEquipeGestaoCadUnico.Value)
                    {
                        trEquipeCadUnico.Visible = true;
                        lblEstrurarEquipeCadUnico.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão do cadastro único";
                    }
                    else
                        trEquipeCadUnico.Visible = false;//lblEstrurarEquipeCadUnico.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão do cadastro único";

                    if (estruturacaoEquipe.IntencaoAcaoEquipeGestaoFinanceira.HasValue && estruturacaoEquipe.IntencaoAcaoEquipeGestaoFinanceira.Value)
                    {
                        trEquipeGestaoFinanceira.Visible = true;
                        lblEstrurarEquipeGestaoFinanceira.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão financeira e orçamentária";
                    }
                    else
                        trEquipeGestaoFinanceira.Visible = false; // lblEstrurarEquipeGestaoFinanceira.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão financeira e orçamentária";

                    if (estruturacaoEquipe.IntencaoAcaoEquipeGestaoSUAS.HasValue && estruturacaoEquipe.IntencaoAcaoEquipeGestaoSUAS.Value)
                    {
                        trEquipeGestaoSuas.Visible = true;
                        lblEstrurarEquipeTrabalho.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão do Trabalho no SUAS";
                    }
                    else
                        trEquipeGestaoSuas.Visible = false;// lblEstrurarEquipeTrabalho.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a gestão do Trabalho no SUAS";

                    if (estruturacaoEquipe.IntencaoAcaoEquipeRegulacaoSUAS.HasValue && estruturacaoEquipe.IntencaoAcaoEquipeRegulacaoSUAS.Value)
                    {
                        trEquipeRegulacaoSuas.Visible = true;
                        lblEstrurarEquipeRegulacaoSuas.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a regulação do SUAS";
                    }
                    else
                        trEquipeRegulacaoSuas.Visible = false; // lblEstrurarEquipeRegulacaoSuas.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a regulação do SUAS";

                    if (estruturacaoEquipe.IntencaoAcaoEquipeRedeDireta.HasValue && estruturacaoEquipe.IntencaoAcaoEquipeRedeDireta.Value)
                    {
                        trEquipeRedeDireta.Visible = true;
                        lblAumentarEquipeRedeDireta.Text = "Foi informado que existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a rede direta";
                    }
                    else
                        trEquipeRedeDireta.Visible = false;
                    // lblAumentarEquipeRedeDireta.Text = "Não existe intenção de estruturar esta equipe no órgão gestor nos próximos anos para a proteção social básica";

                    if (estruturacaoEquipe.IntencaoAcaoOrgaoGestor.HasValue && estruturacaoEquipe.IntencaoAcaoOrgaoGestor.Value)
                    {
                        trEquipeOrgaoGestor.Visible = true;
                        lblAumentarEquipeOrgaoGestor.Text = "Foi informado que existe intenção de aumentar o número de trabalhadores do órgão gestor nos próximos anos.";
                    }
                    else
                        trEquipeOrgaoGestor.Visible = false;//  lblAumentarEquipeRedeDireta.Text = "Não existe intenção de aumentar o número de trabalhadores do órgão gestor nos próximos anos";

                }
            }
        }
        void carregarPagina()
        {
            Master.Titulo = "Relat&#243;rio descritivo 8 - Diagn&#243;stico socioterritorial";
            Master.WidthRelatorio = "600";
            Master.GerarExcel.Click += new EventHandler(GerarExcel_Click);
            Master.GeraXLSX.Visible = false;
        }

        private void GerarExcel_Click(object sender, EventArgs e)
        {
            carregarDados();
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            var tb = ((HtmlTable)tbReport.FindControl("tbReport"));
            tb.CellPadding = 1;
            tb.CellSpacing = 1;
            tb.Border = 1;
            tb.BorderColor = "black";

            var tbAnalise = ((HtmlTable)tbReport.FindControl("tbAnaliseDiagnostica"));
            tbAnalise.CellPadding = 1;
            tbAnalise.CellSpacing = 1;
            tbAnalise.Border = 1;
            tbAnalise.BorderColor = "black";

            var tbVulnerabilidade = ((HtmlTable)tbReport.FindControl("tbPopulacaoVulnerabilidade"));
            tbVulnerabilidade.CellPadding = 1;
            tbVulnerabilidade.CellSpacing = 1;
            tbVulnerabilidade.Border = 1;
            tbVulnerabilidade.BorderColor = "black";

            var tbEvolucao = ((HtmlTable)tbReport.FindControl("tbEvolucaoRede"));
            tbEvolucao.CellSpacing = 1;
            tbEvolucao.CellPadding = 1;
            tbEvolucao.BorderColor = "black";
            tbEvolucao.Border = 1;


            var tbComunidades = ((HtmlTable)tbReport.FindControl("tbComunidades"));
            tbComunidades.CellPadding = 1;
            tbComunidades.CellSpacing = 1;
            tbComunidades.Border = 1;
            tbComunidades.BorderColor = "black";

            var tbIntencaoAcao = ((HtmlTable)lstIntencaoAcao.FindControl("tbIntencaoAcao"));
            if (tbIntencaoAcao != null)
            {
                tbIntencaoAcao.CellPadding = 1;
                tbIntencaoAcao.CellSpacing = 1;
                tbIntencaoAcao.Border = 1;
                tbIntencaoAcao.BorderColor = "black";
            }

            var tbServicosSocioAssistenciais = ((HtmlTable)lstServicosSocioassistencias.FindControl("tbServicosSocioAssistenciais"));
            if (tbServicosSocioAssistenciais != null)
            {
                tbServicosSocioAssistenciais.CellPadding = 1;
                tbServicosSocioAssistenciais.CellSpacing = 1;
                tbServicosSocioAssistenciais.Border = 1;
                tbServicosSocioAssistenciais.BorderColor = "black";
            }

            var tbRhOrgaoGestor = ((HtmlTable)tbReport.FindControl("tbRhOrgaoGestor"));
            if (tbRhOrgaoGestor != null)
            {
                tbRhOrgaoGestor.CellPadding = 1;
                tbRhOrgaoGestor.CellSpacing = 1;
                tbRhOrgaoGestor.Border = 1;
                tbRhOrgaoGestor.BorderColor = "black";
            }

            var tbAnaliseDiagnostica = ((HtmlTable)lstAnaliseDiagnostica.FindControl("tbSituacoesVulnerabilidade"));
            if (tbAnaliseDiagnostica != null)
            {
                tbAnaliseDiagnostica.CellSpacing = 1;
                tbAnaliseDiagnostica.CellPadding = 1;
                tbAnaliseDiagnostica.BorderColor = "black";
                tbAnaliseDiagnostica.Border = 1;
            }


            var tbAnaliseDemografica = ((HtmlTable)tbReport.FindControl("tbAnaliseDemografica"));
            if (tbAnaliseDemografica != null)
            {
                tbAnaliseDemografica.CellSpacing = 1;
                tbAnaliseDemografica.CellPadding = 1;
                tbAnaliseDemografica.BorderColor = "black";
                tbAnaliseDemografica.Border = 1;
            }


            var tbAnalisePopulacao = ((HtmlTable)tbReport.FindControl("tbAnalisePopulacao"));
            if (tbAnalisePopulacao != null)
            {
                tbAnalisePopulacao.CellSpacing = 1;
                tbAnalisePopulacao.CellPadding = 1;
                tbAnalisePopulacao.BorderColor = "black";
                tbAnalisePopulacao.Border = 1;
            }

            var tbAnaliseSituacaoVulnerabilidade = ((HtmlTable)tbReport.FindControl("tbAnaliseSituacaoVulnerabilidade"));
            if (tbAnaliseSituacaoVulnerabilidade != null)
            {
                tbAnaliseSituacaoVulnerabilidade.CellSpacing = 1;
                tbAnaliseSituacaoVulnerabilidade.CellPadding = 1;
                tbAnaliseSituacaoVulnerabilidade.BorderColor = "black";
                tbAnaliseSituacaoVulnerabilidade.Border = 1;
            }

            var tbAnaliseInterpretacao = ((HtmlTable)tbReport.FindControl("tbAnaliseInterpretacao"));
            if (tbAnaliseInterpretacao != null)
            {
                tbAnaliseInterpretacao.CellSpacing = 1;
                tbAnaliseInterpretacao.CellPadding = 1;
                tbAnaliseInterpretacao.BorderColor = "black";
                tbAnaliseInterpretacao.Border = 1;
            }

            Master.Report.RenderControl(htmlWrite);
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=RelatorioDescritivo8.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write(this.Master.replaceCaracteresEspeciais(stringWrite.ToString()));
            Response.End();

            tb.CellPadding = 0;
            tb.CellSpacing = 0;
            tb.Border = 0;
            tb.BorderColor = "";

            tbAnalise.CellPadding = 0;
            tbAnalise.CellSpacing = 0;
            tbAnalise.Border = 0;
            tbAnalise.BorderColor = "";

            tbVulnerabilidade.CellPadding = 0;
            tbVulnerabilidade.CellSpacing = 0;
            tbVulnerabilidade.Border = 0;
            tbVulnerabilidade.BorderColor = "";

            tbEvolucao.CellSpacing = 0;
            tbEvolucao.CellPadding = 0;
            tbEvolucao.BorderColor = "";
            tbEvolucao.Border = 0;

            tbAnaliseDiagnostica.CellSpacing = 0;
            tbAnaliseDiagnostica.CellPadding = 0;
            tbAnaliseDiagnostica.BorderColor = "";
            tbAnaliseDiagnostica.Border = 0;

            tbServicosSocioAssistenciais.CellPadding = 0;
            tbServicosSocioAssistenciais.CellSpacing = 0;
            tbServicosSocioAssistenciais.Border = 0;
            tbServicosSocioAssistenciais.BorderColor = "";

            tbComunidades.CellPadding = 0;
            tbComunidades.CellSpacing = 0;
            tbComunidades.Border = 0;
            tbComunidades.BorderColor = "";

            tbRhOrgaoGestor.CellPadding = 0;
            tbRhOrgaoGestor.CellSpacing = 0;
            tbRhOrgaoGestor.Border = 0;
            tbRhOrgaoGestor.BorderColor = "";

            tbIntencaoAcao.CellPadding = 0;
            tbIntencaoAcao.CellSpacing = 0;
            tbIntencaoAcao.Border = 0;
            tbIntencaoAcao.BorderColor = "";


            tbAnaliseDemografica.CellPadding = 0;
            tbAnaliseDemografica.CellSpacing = 0;
            tbAnaliseDemografica.Border = 0;
            tbAnaliseDemografica.BorderColor = "";

            tbAnalisePopulacao.CellPadding = 0;
            tbAnalisePopulacao.CellSpacing = 0;
            tbAnalisePopulacao.Border = 0;
            tbAnalisePopulacao.BorderColor = "";

            tbAnaliseSituacaoVulnerabilidade.CellPadding = 0;
            tbAnaliseSituacaoVulnerabilidade.CellSpacing = 0;
            tbAnaliseSituacaoVulnerabilidade.Border = 0;
            tbAnaliseSituacaoVulnerabilidade.BorderColor = "";

            tbAnaliseInterpretacao.CellPadding = 0;
            tbAnaliseInterpretacao.CellSpacing = 0;
            tbAnaliseInterpretacao.Border = 0;
            tbAnaliseInterpretacao.BorderColor = "";

            

        }



        //demografia = new Seds.PMAS.QUADRIENAL.Negocio.AnaliseDiagnostica().GetDemografiaIndicadoresByMunicipio(SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio);

    }
}