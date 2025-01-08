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
    public partial class FFonteFinanciamento : System.Web.UI.Page
    {
        private static List<int> FonteFinanciamentoExercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        protected void Page_Load(object sender, EventArgs e)
        {
            this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? "2022" : this.hdfAno.Value;


            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    //carregarLeiOrcamentaria(prefeituras);
                    loadIndice(proxy);
                    load(prefeituras);
                }
                LoadExercicios();
                adicionarEventos();
                verificarAlteracoes();
            }

            AplicarBloqueioControles();

        }

        #region loaders

        void AplicarBloqueioControles()
        {
            WebControl[] controles = {
              btnSalvar
            , txtFMAS
            , txtCusteio
            , txtFEAS
            , txtFNAS
            , txtIGDPBFValorMensal
            , txtIGDPBF
            , txtIGDSUAS
            , txtIGDSUASValorMensal
            , txtComentario
            , txtTotalFMAS
            , lblIGDPBFValorAnual
            , lblIGDSUASValorAnual
            };

            Permissao.BlocoV.VerificaPermissaoExercicioFundoMunicipalBlocoV(controles, Convert.ToInt32(hdfAno.Value));

        }


        private void loadIndice(ProxyPrefeitura proxy)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            var indiceGestaoDescentralizada = proxy.Service.GetIndiceGestaoDescentralizadaByPrefeituraByExercicio(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
            CarregarIncentivosLabelReferencia(exercicio);



            if (indiceGestaoDescentralizada == null)
            {
                return;
            }


            if (indiceGestaoDescentralizada.IGDPBF.HasValue)
            {
                txtIGDPBF.Text = indiceGestaoDescentralizada.IGDPBF.Value.ToString("N2");
            }
            if (indiceGestaoDescentralizada.IGDPBFValorMensal.HasValue)
            {
                txtIGDPBFValorMensal.Text = indiceGestaoDescentralizada.IGDPBFValorMensal.Value.ToString("N2");
            }
            if (indiceGestaoDescentralizada.IGDPBFValorAnual.HasValue)
            {
                lblIGDPBFValorAnual.Text = indiceGestaoDescentralizada.IGDPBFValorAnual.Value.ToString("N2");
            }

            if (indiceGestaoDescentralizada.IGDSUAS.HasValue)
            {
                txtIGDSUAS.Text = indiceGestaoDescentralizada.IGDSUAS.Value.ToString("N2");
            }
            if (indiceGestaoDescentralizada.IGDSUASValorMensal.HasValue)
            {
                txtIGDSUASValorMensal.Text = indiceGestaoDescentralizada.IGDSUASValorMensal.Value.ToString("N2");
            }
            if (indiceGestaoDescentralizada.IGDSUASValorAnual.HasValue)
            {
                lblIGDSUASValorAnual.Text = indiceGestaoDescentralizada.IGDSUASValorAnual.Value.ToString("N2");
            }
            if (!String.IsNullOrEmpty(indiceGestaoDescentralizada.ComentariosExecucaoFinanceira))
            {
                txtComentario.Text = indiceGestaoDescentralizada.ComentariosExecucaoFinanceira;
            }
        }


        private void load(Prefeituras prefeituras)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);

            FundoMunicipalInfo fmas = prefeituras.GetFMAS(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            FundoMunicipalValoresInfo fmasValores = fmas.FundosMunicipaisValoresInfo.FirstOrDefault(x => x.Exercicio == exercicio);

            if (fmasValores != null)
            {
                if (fmasValores.ValorFMAS.HasValue)
                {
                    txtFMAS.Text = fmasValores.ValorFMAS.Value.ToString("N2");
                }
                if (fmasValores.ValorFEAS.HasValue)
                {
                    txtFEAS.Text = fmasValores.ValorFEAS.Value.ToString("N2");
                }
                if (fmasValores.ValorFNAS.HasValue)
                {
                    txtFNAS.Text = fmasValores.ValorFNAS.Value.ToString("N2");
                }
                if (fmasValores.ValorCusteio.HasValue)
                {
                    txtCusteio.Text = fmasValores.ValorCusteio.Value.ToString("N2");
                }

                txtTotalFMAS.Text = (Convert.ToDecimal(txtFMAS.Text) + Convert.ToDecimal(txtFEAS.Text) + Convert.ToDecimal(txtFNAS.Text)).ToString("N2");
            }
            CarregarLabelsPorExercicio(exercicio);
            AplicarBloqueioControles();

        }

        #endregion

        #region Fonte Financiamento - exercicio
        private void LoadExercicios()
        {
            this.btnExercicio1.Text = FFonteFinanciamento.FonteFinanciamentoExercicios[0].ToString();
            this.btnExercicio2.Text = FFonteFinanciamento.FonteFinanciamentoExercicios[1].ToString();
            this.btnExercicio3.Text = FFonteFinanciamento.FonteFinanciamentoExercicios[2].ToString();
            this.btnExercicio4.Text = FFonteFinanciamento.FonteFinanciamentoExercicios[3].ToString();

            if (FFonteFinanciamento.FonteFinanciamentoExercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                lblInformePrevisaoFMAS.InnerText = "Informe nos campos abaixo a previsão dos valores dos recursos que serão alocados no Fundo Municipal de Assistência Social (FMAS) em 2022:";
                this.btnExercicio1.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";

            }
            if (FFonteFinanciamento.FonteFinanciamentoExercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                lblInformePrevisaoFMAS.InnerText = "Informe nos campos abaixo a previsão dos valores dos recursos que serão alocados no Fundo Municipal de Assistência Social (FMAS) em 2023:";
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FFonteFinanciamento.FonteFinanciamentoExercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                lblInformePrevisaoFMAS.InnerText = "Informe nos campos abaixo a previsão dos valores dos recursos que serão alocados no Fundo Municipal de Assistência Social (FMAS) em 2024:";
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (FFonteFinanciamento.FonteFinanciamentoExercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                lblInformePrevisaoFMAS.InnerText = "Informe nos campos abaixo a previsão dos valores dos recursos que serão alocados no Fundo Municipal de Assistência Social (FMAS) em 2025:";
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-info-seds";
            }
        }



        protected void btnLoadExercicio1_Click(object sender, EventArgs e)
        {

            hdfAno.Value = btnExercicio1.Text;
            
            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                //carregarLeiOrcamentaria(prefeituras);
                loadIndice(proxy);
                load(prefeituras);
            }
            #endregion

        }
        protected void btnLoadExercicio2_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio2.Text;

            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                //carregarLeiOrcamentaria(prefeituras);
                loadIndice(proxy);
                load(prefeituras);

            }
            #endregion
        }

        protected void btnLoadExercicio3_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio3.Text;
            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                //carregarLeiOrcamentaria(prefeituras);
                loadIndice(proxy);
                load(prefeituras);

            }
            #endregion
        }

        protected void btnLoadExercicio4_Click(object sender, EventArgs e)
        {
            hdfAno.Value = btnExercicio4.Text;
            #region reload
            Clear();
            using (var proxy = new ProxyPrefeitura())
            {
                var prefeituras = new Prefeituras(proxy);
                //carregarLeiOrcamentaria(prefeituras);
                loadIndice(proxy);
                load(prefeituras);

            }
            #endregion
        }



        private void Clear()
        {
            txtFMAS.Text = "0,00";
            txtCusteio.Text = "0,00";
            txtFEAS.Text = "0,00";
            txtFNAS.Text = "0,00";
            txtIGDPBFValorMensal.Text = "0,00";
            txtIGDSUASValorMensal.Text = "0,00";
            txtComentario.Text = string.Empty;
            txtTotalFMAS.Text = "0,00";

            #region Incentivos à gestão
            lblIGDPBFValorAnual.Text = "0,00";
            lblIGDSUASValorAnual.Text = "0,00";
            #endregion


        }
        #endregion



        private void adicionarEventos()
        {
            txtFMAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMAS.Attributes.Add("onblur", "CalculateTotalFMAS()");

            txtFNAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNAS.Attributes.Add("onblur", "CalculateTotalFMAS()");

            txtCusteio.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

            txtFEAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEAS.Attributes.Add("onblur", "CalculateTotalFMAS()");

            txtTotalFMAS.Attributes.Add("onload", "CalculateTotalFMAS()");

            txtIGDPBF.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDPBFValorMensal.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDSUAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtIGDSUASValorMensal.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int exercicio = Convert.ToInt32(hdfAno.Value);
            String msg = String.Empty;
            var script = Util.GetJavaScriptDialogOK(msg);
            SessaoPmas.VerificarSessao(this);
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    if (txtComentario.Text.Trim().Length > 1000)
                    {
                        msg = "O texto digitado ultrapassou o limite de 1000 caracteres e não poderá ser salvo desta forma. Por favor, reduza-o até o limite de 1000 caracteres." + System.Environment.NewLine;
                    }

                    var fmas = prefeituras.GetFMAS(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    if (fmas != null)
                    {

                        FundoMunicipalValoresInfo fmasValoresExistentes = fmas.FundosMunicipaisValoresInfo.FirstOrDefault(x => x.Exercicio == exercicio);

                        var previsao = prefeituras.GetPrevisaoOrcamentariaMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                        decimal recursosSocioassistenciais = previsao.Sum(p => p.RedePublicaMunicipal + p.RedePrivadaMunicipal);


                        if (fmasValoresExistentes != null)
                        {
                            fmasValoresExistentes.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                            fmasValoresExistentes.ValorFMAS = !String.IsNullOrEmpty(txtFMAS.Text) ? Convert.ToDecimal(txtFMAS.Text) : 0.0M;
                            fmasValoresExistentes.ValorFEAS = !String.IsNullOrEmpty(txtFEAS.Text) ? Convert.ToDecimal(txtFEAS.Text) : 0.0M;
                            fmasValoresExistentes.ValorFNAS = !String.IsNullOrEmpty(txtFNAS.Text) ? Convert.ToDecimal(txtFNAS.Text) : 0.0M;
                            fmasValoresExistentes.ValorCusteio = !String.IsNullOrEmpty(txtCusteio.Text) ? Convert.ToDecimal(txtCusteio.Text) : 0.00M;
                        }
                        else
                        {
                            FundoMunicipalValoresInfo fmasValoresNovo = new FundoMunicipalValoresInfo();
                            fmasValoresNovo.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                            fmasValoresNovo.ValorFMAS = !String.IsNullOrEmpty(txtFMAS.Text) ? Convert.ToDecimal(txtFMAS.Text) : 0.0M;
                            fmasValoresNovo.ValorFEAS = !String.IsNullOrEmpty(txtFEAS.Text) ? Convert.ToDecimal(txtFEAS.Text) : 0.0M;
                            fmasValoresNovo.ValorFNAS = !String.IsNullOrEmpty(txtFNAS.Text) ? Convert.ToDecimal(txtFNAS.Text) : 0.0M;
                            fmasValoresNovo.ValorCusteio = !String.IsNullOrEmpty(txtCusteio.Text) ? Convert.ToDecimal(txtCusteio.Text) : 0.00M;
                            fmasValoresNovo.Exercicio = exercicio;
                            fmas.FundosMunicipaisValoresInfo.Add(fmasValoresNovo);
                        }

                        if (!String.IsNullOrEmpty(msg))
                        {
                            throw new Exception(msg);
                        }

                        IndiceGestaoDescentralizadaInfo indiceGestao = new IndiceGestaoDescentralizadaInfo();

                        List<PrevisaoOrcamentariaInfo> previsoesOrcamentarias = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio);
                        new ValidadorFonteRecursoFMAS().ValidarFonteRecursosFMAS(fmas, previsoesOrcamentarias, exercicio, true);

                        CarregarIndiceGestaoDescentralizada(proxy, indiceGestao);
                        new ValidadorIndiceGestaoDescentralizada().Validar(indiceGestao);
                        prefeituras.SaveFontesRecursosFMAS(fmas, previsoesOrcamentarias, Convert.ToInt32(hdfAno.Value));
                        SalvarIndiceGestaoDescentralizada(proxy, indiceGestao);

                    }

                    load(prefeituras);
                }
            }
            catch (Exception ex)
            {
                msg = "Verifique as inconsistências!";
                script = Util.GetJavascriptDialogError(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }
            if (String.IsNullOrEmpty(msg))
            {
                msg = "Fontes de financiamento atualizadas com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                return;
            }
        }

        private void CarregarIndiceGestaoDescentralizada(ProxyPrefeitura proxy, IndiceGestaoDescentralizadaInfo indiceGestao)
        {

            indiceGestao.Exercicio = Convert.ToInt32(hdfAno.Value);
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                indiceGestao.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            }

            indiceGestao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            if (!String.IsNullOrEmpty(txtIGDPBF.Text))
            {
                indiceGestao.IGDPBF = Convert.ToDouble(txtIGDPBF.Text);
            }
            else
            {
                indiceGestao.IGDPBF = 0;
            }

            if (!String.IsNullOrEmpty(txtIGDPBFValorMensal.Text))
            {
                indiceGestao.IGDPBFValorMensal = Convert.ToDecimal(txtIGDPBFValorMensal.Text);
            }
            else
            {
                indiceGestao.IGDPBFValorMensal = 0;
            }

            if (!String.IsNullOrEmpty(txtIGDSUAS.Text))
            {
                indiceGestao.IGDSUAS = Convert.ToDouble(txtIGDSUAS.Text);
            }
            else
            {
                indiceGestao.IGDSUAS = 0;
            }

            if (!String.IsNullOrEmpty(txtIGDSUASValorMensal.Text))
            {
                indiceGestao.IGDSUASValorMensal = Convert.ToDecimal(txtIGDSUASValorMensal.Text);
            }
            else
            {
                indiceGestao.IGDSUASValorMensal = 0;
            }

            if (!String.IsNullOrEmpty(txtComentario.Text))
            {
                indiceGestao.ComentariosExecucaoFinanceira = txtComentario.Text;
            }
            else
            {
                indiceGestao.ComentariosExecucaoFinanceira = "";
            }


        }

        private void SalvarIndiceGestaoDescentralizada(ProxyPrefeitura proxy, IndiceGestaoDescentralizadaInfo indiceGestao)
        {
            proxy.Service.SaveIndiceGestaoDescentralizada(indiceGestao);
        }

        protected void btnSalvarIGD_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var obj = new IndiceGestaoDescentralizadaInfo();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                obj.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;


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

        #region helper
        private void verificarAlteracoes()
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
        private void CarregarIncentivosLabelReferencia(int exercicio)
        {
            #region Incentivos à gestão - label de referencia
            /*if (exercicio == FFonteFinanciamento.FonteFinanciamentoExercicios[0])
            {
                txtReferencia1.Text = "(referência: maio/2017)";
                txtReferencia2.Text = "(referência: maio/2017)";
            }
            if (exercicio == FFonteFinanciamento.FonteFinanciamentoExercicios[1])
            {
                txtReferencia1.Text = "(referência: maio/2018)";
                txtReferencia2.Text = "(referência: maio/2018)";
            }*/
            #endregion
        }

        private void CarregarLabelsPorExercicio(int exercicio)
        {
            if (FFonteFinanciamento.FonteFinanciamentoExercicios[0] == exercicio)
            {
                lblHeader.Text = "5.3.a - Fontes de recursos do FMAS para o exercício de 2022";
                tbInconsistencias.Visible = false;
                LoadExercicios();

            }
            if (FFonteFinanciamento.FonteFinanciamentoExercicios[1] == exercicio)
            {
                lblHeader.Text = "5.3.a - Fontes de recursos do FMAS para o exercício de 2023";
                tbInconsistencias.Visible = false;
                LoadExercicios();
            }
            if (FFonteFinanciamento.FonteFinanciamentoExercicios[2] == exercicio)
            {
                lblHeader.Text = "5.3.a - Fontes de recursos do FMAS para o exercício de 2024";
                tbInconsistencias.Visible = false;
                LoadExercicios();
            }
            if (FFonteFinanciamento.FonteFinanciamentoExercicios[3] == exercicio)
            {
                lblHeader.Text = "5.3.a - Fontes de recursos do FMAS para o exercício de 2025";
                tbInconsistencias.Visible = false;
                LoadExercicios();
            }
        }
        #endregion

        #region bloqueio e desbloqueio
        private void bloquearControles()
        {
            WebControl[] controles = {
                                         txtFMAS,
                                         txtCusteio,
                                         txtFEAS,
                                         txtFNAS,
                                         txtIGDPBFValorMensal,
                                         txtIGDSUAS,
                                         txtIGDSUASValorMensal,
                                         txtComentario,
                                         btnSalvar };
            Permissao.VerificarPermissaoControles(controles, Session);
            Boolean bloqueado = true;
            using (var proxy = new ProxyPlanoMunicipal())
            {
                int situacaoQuadro = proxy.Service.GetQuadroLeiOrcamentariaBloqueado();
                bloqueado = (situacaoQuadro == 1);
                var gestor = new Prefeituras(new ProxyPrefeitura()).GetAtualGestorMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                if (gestor == null)
                {
                    bloqueado = true;
                }
                else
                {
                    if (SessaoPmas.UsuarioLogado.IdUsuario != gestor.IdUsuarioGestor)
                    {
                        bloqueado = true;
                    }
                }
            }
        }
        #endregion

        #region WebMethod

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

        #endregion

    }
}