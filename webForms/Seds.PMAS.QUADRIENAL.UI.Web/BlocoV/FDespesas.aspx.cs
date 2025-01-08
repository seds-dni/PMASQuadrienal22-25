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
    public partial class FDespesas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

             this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? "2021" : this.hdfAno.Value;

             this.hdfAno.Value = Session["Exercicio"].ToString();
             Session["ExercicioDespesa"] = Session["Exercicio"].ToString();

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                int idServicosRecursosFinanceiros = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["Id"]));

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                AdicionarEventosJs();
                carregarOpcao(idServicosRecursosFinanceiros);
                
            }

        }

        void AdicionarEventosJs() 
        {
            txtMaterialDeConsumo.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event, false ) );");
            txtOutrasDespesas.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event, false ) );");
            txtRecursosHumanos.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event, false ) );");
            txtValorAplicacoesFinanceiras.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event, false ) );");
        }

        private void BloqueioPorQuadro(ProxyPrefeitura proxy)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);
            int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            var quadro = proxy.Service.GetPrefeituraSituacaoQuadro(idPrefeitura, 168).Where(x => x.Exercicio == exercicio).FirstOrDefault();

            switch (quadro.IdSituacaoQuadro)
            {
                case (int)ESituacaoQuadro.Pendente:
                case (int)ESituacaoQuadro.DevolvidoDRADS:
                case (int)ESituacaoQuadro.DevolvidoCMAS:
                default:

                    habilitarPrestacaoDeContasDespesas(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                    habilitarPrestacaoDeContasExecucaoFisicaProgramaProjeto(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                    habilitarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);
                    habilitarPrestacaoDeContasAplicacoesFinanceiras(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.OrgaoGestor);

                    #region proibicoes
                    #endregion

                    break;
                case (int)ESituacaoQuadro.EmAnaliseCMAS:

                    habilitarPrestacaoDeContasDespesas(false);
                    habilitarPrestacaoDeContasExecucaoFisicaProgramaProjeto(false);
                    habilitarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(false);
                    habilitarPrestacaoDeContasAplicacoesFinanceiras(false);

                    #region proibicoes
                    #endregion

                    break;
                case (int)ESituacaoQuadro.AprovadoCMAS:

                    habilitarPrestacaoDeContasDespesas(false);
                    habilitarPrestacaoDeContasExecucaoFisicaProgramaProjeto(false);
                    habilitarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(false);
                    habilitarPrestacaoDeContasAplicacoesFinanceiras(false);

                    #region proibicoes
                    #endregion

                    break;
                case (int)ESituacaoQuadro.BloqueioInicialAdministrativo:

                    habilitarPrestacaoDeContasDespesas(false);
                    habilitarPrestacaoDeContasExecucaoFisicaProgramaProjeto(false);
                    habilitarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(false);
                    habilitarPrestacaoDeContasAplicacoesFinanceiras(false);

                    #region proibicoes
                    #endregion

                    break;
            }

        }

        PrestacaoDeContasDespesasInfo PreencherDespesas(int idServicoRecursoFinanceiro,int idTipoProtecao)
        {
            var d = new PrestacaoDeContasDespesasInfo();

            d.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            d.IdServicosRecursosFinanceiros = idServicoRecursoFinanceiro;
            d.IdTipoProtecao = idTipoProtecao;
            d.Exercicio = Convert.ToInt32(hdfAno.Value);
            
            if (!String.IsNullOrEmpty(txtMaterialDeConsumo.Text))
            {
                d.MaterialDeConsumo = Convert.ToDecimal(txtMaterialDeConsumo.Text);
            }
            
            if (!String.IsNullOrEmpty(txtOutrasDespesas.Text))
            {
                d.OutrasDespesas = Convert.ToDecimal(txtOutrasDespesas.Text);
            } 
            
            if (!String.IsNullOrEmpty(txtRecursosHumanos.Text))
            {
                d.RecursosHumanos = Convert.ToDecimal(txtRecursosHumanos.Text);
            }

            return d;
        }

        PrestacaoDeContasAplicacoesFinanceirasInfo PreencherAplicacoesFinanceiras(int idServicoRecursoFinanceiro, int idTipoProtecao) 
        {
            var aplicacoes = new PrestacaoDeContasAplicacoesFinanceirasInfo();

            aplicacoes.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            aplicacoes.IdServicosRecursosFinanceiros = idServicoRecursoFinanceiro;
            aplicacoes.IdTipoProtecao = idTipoProtecao;
            aplicacoes.Exercicio = Convert.ToInt32(hdfAno.Value);

            if (!String.IsNullOrEmpty(txtValorAplicacoesFinanceiras.Text))
            {
                aplicacoes.ValorAplicacoesFinanceiras = Convert.ToDecimal(txtValorAplicacoesFinanceiras.Text);
            }

            return aplicacoes;
        }

        PrestacaoDeContasExecucaoFisicaProgramasProjetosInfo PreencherExecucaoFisicaProgramaProjeto(int idProgramaProjeto,int idTipoProtecao, int idTipoProgramaProjeto) 
        {
            var p = new PrestacaoDeContasExecucaoFisicaProgramasProjetosInfo();

            p.IdServicosRecursosFinanceiros = idProgramaProjeto;
            p.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            p.Exercicio = Convert.ToInt32(hdfAno.Value);
            p.IdTipoProtecao = idTipoProtecao;
            p.IdTipoProgramaProjeto = idTipoProgramaProjeto;

            if (!String.IsNullOrEmpty(txtDemandaEstimada.Text))
            {
                p.DemandaEstimada = Convert.ToInt32(txtDemandaEstimada.Text);
            }
            else
            {
                p.DemandaEstimada = 0;
            }

            if (!String.IsNullOrEmpty(txtNumeroAtendidos.Text))
            {
                p.NumeroAtendidos = Convert.ToInt32(txtNumeroAtendidos.Text);
            }
            else
            {
                p.NumeroAtendidos = 0;
            }

            return p;
        }

        PrestacaoDeContasExecucaoFisicaInfo PreencherExecucaoFisicaBeneficiosEventuais(int idBeneficiosEventuais, int idTipoProtecao,int idTipoBeneficioEventual)
        {
            var p = new PrestacaoDeContasExecucaoFisicaInfo();

            p.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            p.Exercicio = Convert.ToInt32(hdfAno.Value);
            p.IdServicosRecursosFinanceiros = idBeneficiosEventuais;
            p.IdTipoBeneficioEventual = idTipoBeneficioEventual;
                       
            p.IdTipoProtecao = idTipoProtecao;

            if (!String.IsNullOrEmpty(txtDataImplantacao.Text))
            {
                p.DataDeImplantacao = Convert.ToDateTime(txtDataImplantacao.Text);
            }
            else
            {
                p.DataDeImplantacao = Convert.ToDateTime("01/01/1900 00:00:00");
            }

            if (!String.IsNullOrEmpty(txtQuantidadeAnualBeneficiarios.Text))
            {
                p.QuantidadeAnualBeneficiario = Convert.ToInt32(txtQuantidadeAnualBeneficiarios.Text);
            }
            if (!String.IsNullOrEmpty(txtQuantidadeAnualBeneficiariosConcedidos.Text))
            {
                p.QuantidadeAnualBeneficiariosConcedidos = Convert.ToInt32(txtQuantidadeAnualBeneficiariosConcedidos.Text);
            }

            return p;
        }

        void carregarOpcao(int idServicosRecursosFinanceiros)
        {
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                int exercicio = Convert.ToInt32(hdfAno.Value);

                var protecao = prefeituras.GetLocaisExecucaoPrestacaoDeContasDespesas(idServicosRecursosFinanceiros, idPrefeitura);
                var aplicacoes = prefeituras.GetLocaisExecucaoPrestacaoDeContasDespesas(idServicosRecursosFinanceiros, idPrefeitura).Where(s => s.Exercicio == exercicio);
                var programaProjeto = prefeituras.GetPrestacaoDeContasProgramaProjetoDespesas(idServicosRecursosFinanceiros, idPrefeitura, exercicio);
                var beneficiosEventuais = prefeituras.GetPrestacaoDeContasBeneficiosEventuaisDespesas(idServicosRecursosFinanceiros, idPrefeitura, exercicio);

                if (protecao.Count != 0)
                {
                    int idTipoProtecao = protecao.FirstOrDefault().IdTipoProtecao;

                    decimal materialConsumo = protecao.FirstOrDefault(s => s.Exercicio == exercicio).MaterialDeConsumo;
                    decimal outrasDespesas = protecao.FirstOrDefault(s => s.Exercicio == exercicio).OutrasDespesas;
                    decimal recursosHumano = protecao.FirstOrDefault(s => s.Exercicio == exercicio).RecursosHumanos;

                    carregarPrestacaoDeContasDespesas(materialConsumo, outrasDespesas, recursosHumano);
                    visualizarPrestacaoDeContasExecucaoFisicaProgramaProjeto(false);
                    visualizarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(false);
                    divExecucaoFisica.Visible = false;
                }
                else if (programaProjeto.Count != 0)
                {
                    decimal materialConsumo = programaProjeto.FirstOrDefault(s => s.Exercicio == exercicio).MaterialDeConsumo;
                    decimal outrasDespesas = programaProjeto.FirstOrDefault(s => s.Exercicio == exercicio).OutrasDespesas;
                    decimal recursosHumano = programaProjeto.FirstOrDefault(s => s.Exercicio == exercicio).RecursosHumanos;

                    //DateTime dataImplantacao = programaProjeto.FirstOrDefault().DataImplantacao;
                    //int NaoImplantado = programaProjeto.FirstOrDefault().NaoImplantado;
                    
                    decimal aplicacaoFinanceira = programaProjeto.FirstOrDefault().ValorAplicacoesFinanceiras;
                    int numeroAtendidos = programaProjeto.FirstOrDefault(s => s.Exercicio == exercicio).NumeroAtendidos;
                    int demandaEstimada = programaProjeto.FirstOrDefault(s => s.Exercicio == exercicio).DemandaEstimada;

                    carregarPrestacaoDeContasDespesas(materialConsumo, outrasDespesas, recursosHumano);
                    carregarPrestacaoDeContasAplicacoesFinanceiras(aplicacaoFinanceira);
                    carregarPrestacaDeContasExecucaoFisicaProgramaProjeto(numeroAtendidos,demandaEstimada);
                    visualizarPrestacaoDeContasExecucaoFisicaProgramaProjeto(true);
                    visualizarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(false);
                    divExecucaoFisica.Visible = true;
                }
                else if (beneficiosEventuais.Count != 0)
                {
                    decimal materialConsumo = beneficiosEventuais.FirstOrDefault(s => s.Exercicio == exercicio).MaterialDeConsumo;
                    decimal outrasDespesas = beneficiosEventuais.FirstOrDefault(s => s.Exercicio == exercicio).OutrasDespesas;
                    decimal recursosHumano = beneficiosEventuais.FirstOrDefault(s => s.Exercicio == exercicio).RecursosHumanos;
                    decimal aplicacaoFinanceira = beneficiosEventuais.FirstOrDefault().ValorAplicacoesFinanceiras;

                    int quantidadeAnualBeneficiarios = beneficiosEventuais.FirstOrDefault(s => s.Exercicio == exercicio).QuantidadeAnualBeneficiarios;
                    int QuantidadeAnualBeneficiariosConcedidos = beneficiosEventuais.FirstOrDefault(s => s.Exercicio == exercicio).QuantidadeAnualBeneficiariosConcedidos;

                    carregarPrestacaoDeContasDespesas(materialConsumo, outrasDespesas, recursosHumano);
                    carregarPrestacaoDeContasAplicacoesFinanceiras(aplicacaoFinanceira);
                    carregarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(quantidadeAnualBeneficiarios, QuantidadeAnualBeneficiariosConcedidos);
                    visualizarPrestacaoDeContasExecucaoFisicaProgramaProjeto(false);
                    visualizarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(true);
                    divExecucaoFisica.Visible = true;
                }


                if (aplicacoes != null && aplicacoes.Count() > 0)
                {
                    decimal valoresAplicacoes = aplicacoes.FirstOrDefault(s => s.Exercicio == exercicio).ValorAplicacoesFinanceiras;
                    carregarPrestacaoDeContasAplicacoesFinanceiras(valoresAplicacoes);

                }

                BloqueioPorQuadro(proxy);

            }
        }

        void carregarPrestacaoDeContasDespesas(decimal materialConsumo,decimal outrasDespesas,decimal recursosHumanos)
        {
            txtMaterialDeConsumo.Text = materialConsumo.ToString("N2");
            txtOutrasDespesas.Text = outrasDespesas.ToString("N2");
            txtRecursosHumanos.Text = recursosHumanos.ToString("N2");
        }

        void carregarPrestacaoDeContasAplicacoesFinanceiras(decimal aplicacoesFinanceiras)
        {
            txtValorAplicacoesFinanceiras.Text = aplicacoesFinanceiras.ToString("N2");
        }

        void carregarPrestacaDeContasExecucaoFisicaProgramaProjeto(int numeroAtendidos, int DemandaEstimada) 
        {
            /*if (NaoImplantado == 1)
            {
                chkNaoImplantado.Checked = true;
                txtDataImplantacao.Text = "";
            }
            else
            {
                chkNaoImplantado.Checked = false;
                txtDataImplantacao.Text = dataImplantacao.ToString("dd/MM/yyyy");
            }*/

            if (DemandaEstimada > 0)
            {
                txtDemandaEstimada.Text = DemandaEstimada.ToString();
            }
            else
            {
                txtDemandaEstimada.Text = "0";
            }

            if (numeroAtendidos > 0)
            {
                txtNumeroAtendidos.Text = numeroAtendidos.ToString();
            }
            else
            {
                txtNumeroAtendidos.Text = "0";
            }

        }

        void carregarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(int QuantidadeAnualBeneficiarios, int QuantidadeAnualBeneficiariosConcedidos) 
        {
            txtQuantidadeAnualBeneficiarios.Text = QuantidadeAnualBeneficiarios.ToString();
            txtQuantidadeAnualBeneficiariosConcedidos.Text = QuantidadeAnualBeneficiariosConcedidos.ToString();

        }

        void habilitarPrestacaoDeContasDespesas(Boolean c)
        {
            txtMaterialDeConsumo.Enabled = c;
            txtOutrasDespesas.Enabled = c;
            txtRecursosHumanos.Enabled = c;
            btnSalvar.Enabled = c;
        }

        void habilitarPrestacaoDeContasExecucaoFisicaProgramaProjeto(Boolean c) 
        {
            lblDemandaEstimada.Enabled = c;
            lblNumeroAtendidos.Enabled = c;
            txtDemandaEstimada.Enabled = c;
            txtNumeroAtendidos.Enabled = c;
        }

        void habilitarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(Boolean c) 
        {
            txtQuantidadeAnualBeneficiarios.Enabled = c;
            txtQuantidadeAnualBeneficiariosConcedidos.Enabled = c;
            lblQuantidadeAnualBeneficiarios.Enabled = c;
            lblQuantidadeAnualBeneficiariosConcedidos.Enabled = c; 
        }

        void habilitarPrestacaoDeContasAplicacoesFinanceiras(Boolean c)
        {
            txtValorAplicacoesFinanceiras.Enabled = c;
        }

        void visualizarPrestacaoDeContasExecucaoFisicaProgramaProjeto(Boolean c) 
        {
            lblDemandaEstimada.Visible = c;
            lblNumeroAtendidos.Visible = c;
            txtDemandaEstimada.Visible = c;
            txtNumeroAtendidos.Visible = c;
        }

        void visualizarPrestacaoDeContasExecucaoFisicaBeneficiosEventuais(Boolean c)
        {
            txtQuantidadeAnualBeneficiarios.Visible = c;
            txtQuantidadeAnualBeneficiariosConcedidos.Visible = c;
            lblQuantidadeAnualBeneficiarios.Visible = c;
            lblQuantidadeAnualBeneficiariosConcedidos.Visible = c;
        }


        void salvar() 
        {
            String msg = String.Empty;

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    int idPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                    int exercicio = Convert.ToInt32(hdfAno.Value);
                    int idServicosRecursosFinanceiros = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["Id"]));

                    var protecao = prefeituras.GetLocaisExecucaoPrestacaoDeContasDespesas(idServicosRecursosFinanceiros, idPrefeitura);

                    var programaProjeto = prefeituras.GetPrestacaoDeContasProgramaProjetoDespesas(idServicosRecursosFinanceiros, idPrefeitura, exercicio);

                    var beneficiosEventuais = prefeituras.GetPrestacaoDeContasBeneficiosEventuaisDespesas(idServicosRecursosFinanceiros, idPrefeitura, exercicio);

                    if (protecao.Count != 0)
                    {
                        int idTipoProtecao = protecao.FirstOrDefault().IdTipoProtecao;

                        var preencherDespesas = PreencherDespesas(idServicosRecursosFinanceiros, idTipoProtecao);

                        var preencherAplicacoes = PreencherAplicacoesFinanceiras(idServicosRecursosFinanceiros, idTipoProtecao);

                        var despesas = prefeituras.GetPrestacaodeContasDespesas(idPrefeitura, exercicio).Where(d => d.IdServicosRecursosFinanceiros == idServicosRecursosFinanceiros).FirstOrDefault();

                        var aplicacoes = prefeituras.GetPrestacaodeContasAplicacoesFinanceiras(idPrefeitura, exercicio).Where(d => d.IdServicosRecursosFinanceiros == idServicosRecursosFinanceiros).FirstOrDefault();

                        if (despesas != null)
                        {
                            preencherDespesas.Id = despesas.Id;
                        }

                        if (aplicacoes != null)
                        {
                            preencherAplicacoes.Id = aplicacoes.Id;
                        }


                        prefeituras.SavePrestacaoDeContasDespesas(preencherDespesas);
                        prefeituras.SavePrestacaoDeContasAplicacoesFinanceiras(preencherAplicacoes);
                    }

                    if (programaProjeto.Count != 0)
                    {
                        int idTipoProtecao = 4;
                        int idProgramaProjeto = programaProjeto.FirstOrDefault().Id;
                        int idTipoPprogramaProjeto = 0;


                        if (programaProjeto.FirstOrDefault().Nome == "Prospera Família")
                        {
                            idTipoPprogramaProjeto = 10;
                        }
                        else if (programaProjeto.FirstOrDefault().Nome == "Fortalecimento das Ações do CadÚnico")
                        {
                            idTipoPprogramaProjeto = 11;
                        }
                        else
                        {
                            idTipoPprogramaProjeto = 26;
                        }


                        var execucaoFisisca = prefeituras.GetPrestacaoDeContasExecucaoFisicaProgramaProjeto(idPrefeitura, exercicio).Where(s => s.IdPrefeitura == idPrefeitura && s.Exercicio == exercicio && s.IdTipoProtecao == idTipoProtecao && s.IdServicosRecursosFinanceiros == idProgramaProjeto).FirstOrDefault();
                        
                        var despesas = prefeituras.GetPrestacaodeContasDespesas(idPrefeitura, exercicio).Where(d => d.IdServicosRecursosFinanceiros == idServicosRecursosFinanceiros).FirstOrDefault();

                        var aplicacoes = prefeituras.GetPrestacaodeContasAplicacoesFinanceiras(idPrefeitura, exercicio).Where(d => d.IdServicosRecursosFinanceiros == idServicosRecursosFinanceiros).FirstOrDefault();

                        var preencherAplicacoes = PreencherAplicacoesFinanceiras(idServicosRecursosFinanceiros, idTipoProtecao);
                        
                        var preencherDespesas = PreencherDespesas(idProgramaProjeto, idTipoProtecao);

                        var preencherExecucaoFisica = PreencherExecucaoFisicaProgramaProjeto(idProgramaProjeto, idTipoProtecao,idTipoPprogramaProjeto);

                       /* if (chkNaoImplantado.Checked == false)
                        {
                            if (String.IsNullOrEmpty(txtDataImplantacao.Text))
                            {
                                msg = "Favor Inserir a Data de Implantação";

                                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
                                lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
                                tbInconsistencias.Visible = true;
                                throw new ArgumentException(msg);
                            }
                        }*/

                        if (execucaoFisisca != null)
                        {
                            preencherExecucaoFisica.Id = execucaoFisisca.Id;
                        }

                        if (despesas != null)
                        {
                            preencherDespesas.Id = despesas.Id;
                        }

                        if (aplicacoes != null)
                        {
                            preencherAplicacoes.Id = aplicacoes.Id;
                        }

                        prefeituras.SavePrestacaoDeContasDespesas(preencherDespesas);
                        prefeituras.SavePrestacaoDeContasExecucaoFisicaProgramaProjeto(preencherExecucaoFisica);
                        prefeituras.SavePrestacaoDeContasAplicacoesFinanceiras(preencherAplicacoes);
                    }

                    if (beneficiosEventuais.Count != 0)
                    {
                        int idTipoProtecao = 5;
                        int idBeneficiosEventuais = beneficiosEventuais.FirstOrDefault().Id;
                        int idTipoBeneficioEventual = beneficiosEventuais.FirstOrDefault().IdTipoBeneficioEventual;

                        var execucaoFisisca = prefeituras.GetPrestacaoDeContasExecucaoFisica(idPrefeitura, exercicio).Where(s => s.IdPrefeitura == idPrefeitura && s.Exercicio == exercicio && s.IdTipoProtecao == idTipoProtecao && s.IdTipoBeneficioEventual == idTipoBeneficioEventual).FirstOrDefault();
                        
                        var despesas = prefeituras.GetPrestacaodeContasDespesas(idPrefeitura, exercicio).Where(d => d.IdServicosRecursosFinanceiros == idServicosRecursosFinanceiros).FirstOrDefault();

                        var aplicacoes = prefeituras.GetPrestacaodeContasAplicacoesFinanceiras(idPrefeitura, exercicio).Where(d => d.IdServicosRecursosFinanceiros == idServicosRecursosFinanceiros).FirstOrDefault();

                        var preencherAplicacoes = PreencherAplicacoesFinanceiras(idServicosRecursosFinanceiros, idTipoProtecao);
                        
                        var preencherDespesas = PreencherDespesas(idBeneficiosEventuais, idTipoProtecao);
                        
                        var preencherExecucaoFisica = PreencherExecucaoFisicaBeneficiosEventuais(idBeneficiosEventuais, idTipoProtecao,idTipoBeneficioEventual);

                        if (execucaoFisisca != null)
                        {
                            preencherExecucaoFisica.Id = execucaoFisisca.Id;
                        }

                        if (despesas != null)
                        {
                            preencherDespesas.Id = despesas.Id;
                        }

                        if (aplicacoes != null)
                        {
                            preencherAplicacoes.Id = aplicacoes.Id;
                        }

                        prefeituras.SavePrestacaoDeContasDespesas(preencherDespesas);
                        prefeituras.SavePrestacaoDeContasExecucaoFisica(preencherExecucaoFisica);
                        prefeituras.SavePrestacaoDeContasAplicacoesFinanceiras(preencherAplicacoes);

                    }
                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            String msg = String.Empty;

            try
            {
                salvar();
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
    }
}