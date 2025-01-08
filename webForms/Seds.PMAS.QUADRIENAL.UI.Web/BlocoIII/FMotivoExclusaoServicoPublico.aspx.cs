using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Resources;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FMotivoExclusaoServicoPublico : System.Web.UI.Page
    {
        #region propriedades
        private static List<int> RecursoFinanceiroExercicios = new List<int>() { 2022, 2023, 2024, 2025 };
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

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                        load(proxy);
                }
            }
            adicionarEventos();

        }
        private void adicionarEventos()
        {
            txtFEAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEDCA.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMDCA.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNDCA.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFMI.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEI.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFNI.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASAnoAnterior.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASDemandasExercicio.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
            txtFEASReprogramacaoDemandasParlamentaresExercicio.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

        }
        private void load(ProxyRedeProtecaoSocial proxy)
        {
            rblMotivoExclusao.DataSource = proxy.Service.GetMotivoDesativacaoServico();
            rblMotivoExclusao.DataValueField = "Id";
            rblMotivoExclusao.DataTextField = "Descricao";
            rblMotivoExclusao.DataBind();

            lblDataExclusaoRegistro.Text = DateTime.Now.Date.ToShortDateString();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("~/");
            }
            var idServico = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
            var idLocal = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            try
            {
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    var entidade = proxy.Service.GetServicoRecursoFinanceiroPublicoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));

                    //DateTime.Now.Year (Ficou pelo ano desbloqueado
                    var exercicioDesbloqueado = 0;
                     

                    var z = Permissao.BlocoIII.ObterExercicioDesbloqueado(19);
                    var y = Permissao.BlocoIII.ObterExercicioDesbloqueado(1041);

                    if (z != null && z != 0)
                    {
                        exercicioDesbloqueado = z; 
                    }

                    if (y != null && y != 0)
                    {
                        exercicioDesbloqueado = y;
                    }


                    #region Atualizacao
                    if (!String.IsNullOrEmpty(hdfExercicio.Value))
                    {
                        if (Convert.ToInt32(hdfExercicio.Value) != 0)
                        {
                            if (Convert.ToInt32(hdfExercicio.Value) != exercicioDesbloqueado)
                            {
                                string script = CarregarAnoNaoPermitidoAlterar();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                                return;
                            }                            
                        }
                    }

                    var servico = entidade.ServicosRecursosFinanceirosFundosPublicoInfo.Where(x => x.Exercicio == exercicioDesbloqueado).FirstOrDefault(); //DateTime.Now.Year).FirstOrDefault();

                    if (servico != null)
                    {
                        servico.ValorEstadualAssistencia = !String.IsNullOrEmpty(txtFEAS.Text) ? Convert.ToDecimal(txtFEAS.Text) : 0m;
                        servico.ValorEstadualFEDCA = !String.IsNullOrEmpty(txtFEDCA.Text) ? Convert.ToDecimal(txtFEDCA.Text) : 0m;
                        servico.ValorMunicipalAssistencia = !String.IsNullOrEmpty(txtFMAS.Text) ? Convert.ToDecimal(txtFMAS.Text) : 0m;
                        servico.ValorMunicipalFMDCA = !String.IsNullOrEmpty(txtFMDCA.Text) ? Convert.ToDecimal(txtFMDCA.Text) : 0m;
                        servico.ValorFederalAssistencia = !String.IsNullOrEmpty(txtFNAS.Text) ? Convert.ToDecimal(txtFNAS.Text) : 0m;
                        servico.ValorFederalFNDCA = !String.IsNullOrEmpty(txtFNDCA.Text) ? Convert.ToDecimal(txtFNDCA.Text) : 0m;
                        servico.ValorMunicipalFMI = !String.IsNullOrEmpty(txtFMI.Text) ? Convert.ToDecimal(txtFMI.Text) : 0m;
                        servico.ValorEstadualFEI = !String.IsNullOrEmpty(txtFEI.Text) ? Convert.ToDecimal(txtFEI.Text) : 0m;
                        servico.ValorFederalFNI = !String.IsNullOrEmpty(txtFNI.Text) ? Convert.ToDecimal(txtFNI.Text) : 0m;
                        servico.ValorEstadualAssistenciaAnoAnterior = !String.IsNullOrEmpty(txtFEASAnoAnterior.Text) ? Convert.ToDecimal(txtFEASAnoAnterior.Text) : 0m;
                        servico.ValorEstadualDemandasParlamentares = !String.IsNullOrEmpty(txtFEASDemandasExercicio.Text) ? Convert.ToDecimal(txtFEASDemandasExercicio.Text) : 0m;
                        servico.ValorEstadualDemandasParlamentaresReprogramacao = !String.IsNullOrEmpty(txtFEASReprogramacaoDemandasParlamentaresExercicio.Text) ? Convert.ToDecimal(txtFEASReprogramacaoDemandasParlamentaresExercicio.Text) : 0m;
                    }

                    entidade.Desativado = true;
                    if (!String.IsNullOrEmpty(txtDataEncerramento.Text))
                    {
                        entidade.DataDesativacao = Convert.ToDateTime(txtDataEncerramento.Text);
                    }
                    if (!String.IsNullOrEmpty(rblMotivoExclusao.SelectedValue))
                    {
                        entidade.IdMotivoDesativacao = Convert.ToInt32(rblMotivoExclusao.SelectedValue);
                    }
                    entidade.Detalhamento = txtDetalhamento.Text;
                    entidade.DataRegistroLog = DateTime.Now;
                    #endregion

                    proxy.Service.UpdateServicoRecursoFinanceiroPublico(entidade); 
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.ToString()), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPublico.aspx?idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)) + "&msg=D");
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("~/");
            }
            var idServico = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
            var idLocal = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPublico.aspx?idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }

        protected void rblMotivoExclusao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMotivoExclusao.SelectedValue == "2")
            {
                trDataEncerramento.Visible = trDetalhamento.Visible = true;
                lblDataEncerramentoServico.Text = "Data do encerramento das atividades deste serviço:";
                lblDetalhamento.Text = "Detalhamento sobre o motivo do encerramento das atividades deste serviço:";
            }
            else if (rblMotivoExclusao.SelectedValue == "3")
            {
                trDataEncerramento.Visible = trDetalhamento.Visible = true;
                lblDataEncerramentoServico.Text = "Data de vigência das alterações na oferta deste serviço:";
                lblDetalhamento.Text = "Detalhamento sobre as alterações na oferta deste serviço:";
            }
            else
            {
                trDataEncerramento.Visible = trDetalhamento.Visible = false;
            }

            txtDataEncerramento.Text = string.Empty;
            txtDetalhamento.Text = string.Empty;

        }

        protected void btnAlerta_Click(object sender, EventArgs e)
        {
            try
            {
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    var entidade = proxy.Service.GetServicoRecursoFinanceiroPublicoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
                    var script = string.Empty;
                    int exercicioDesbloqueado = Permissao.BlocoIII.ObterExercicioDesbloqueado(19);
                    exercicioDesbloqueado = Permissao.BlocoIII.ObterExercicioDesbloqueado(1041);
                    
                    //if (entidade.PossuiProgramaBeneficio.HasValue)
                    //{
                        if (entidade.ServicosRecursosFinanceirosFundosPublicoInfo != null && entidade.ServicosRecursosFinanceirosFundosPublicoInfo.Count > 0)
                        {
                            var servico = entidade.ServicosRecursosFinanceirosFundosPublicoInfo.Last();
                            if (servico.Exercicio == exercicioDesbloqueado)// FMotivoExclusaoServicoPublico.RecursoFinanceiroExercicios[1])
                            {
                                txtFEAS.Text = (0).ToString("N2");
                                txtFEDCA.Text = (0).ToString("N2");
                                txtFMAS.Text = (0).ToString("N2");
                                txtFMDCA.Text = (0).ToString("N2");
                                txtFNAS.Text = (0).ToString("N2");
                                txtFNDCA.Text = (0).ToString("N2");
                                txtFMI.Text = (0).ToString("N2");
                                txtFEI.Text = (0).ToString("N2");
                                txtFNI.Text = (0).ToString("N2");
                                txtFEASAnoAnterior.Text = (0).ToString("N2");
                                //txtFEASAnoAnterior.Enabled = false;
                                txtFEASDemandasExercicio.Text = (0).ToString("N2");
                                txtFEASReprogramacaoDemandasParlamentaresExercicio.Text = (0).ToString("N2");

                                //txtFEAS.Text = servico.ValorEstadualAssistencia.ToString("N2");
                                //txtFEDCA.Text = servico.ValorEstadualFEDCA.ToString("N2");
                                //txtFMAS.Text = servico.ValorMunicipalAssistencia.ToString("N2");
                                //txtFMDCA.Text = servico.ValorMunicipalFMDCA.ToString("N2");
                                //txtFNAS.Text = servico.ValorFederalAssistencia.ToString("N2");
                                //txtFNDCA.Text = servico.ValorFederalFNDCA.ToString("N2");
                                //txtFMI.Text = servico.ValorMunicipalFMI.ToString("N2");
                                //txtFEI.Text = servico.ValorEstadualFEI.ToString("N2");
                                //txtFNI.Text = servico.ValorFederalFNI.ToString("N2");
                                //txtFEASAnoAnterior.Text = servico.ValorEstadualAssistenciaAnoAnterior.ToString("N2");
                                //txtFEASAnoAnterior.Enabled = false;
                            }

                            hdfExercicio.Value = exercicioDesbloqueado.ToString(); //FMotivoExclusaoServicoPublico.RecursoFinanceiroExercicios[1].ToString();

                        }

                        btnSalvar.Visible = trRecursosFinanceiros.Visible = true;
                        btnAlerta.Visible = false;
                        script = CarregarMensagemAlerta();
                    //}
                    //else
                    //{
                    //    script = CarregarMensagemErro();
                    //}

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                }
            }
            catch (Exception ex)
            {
                trRecursosFinanceiros.Visible = false;
            }

        }

        

        #region mensagens

        private static string CarregarAnoNaoPermitidoAlterar()
        {
            string script = Util.GetJavascriptDialogError("Ano divergente do permitido.");
            return script;
        }
        private static string CarregarMensagemErro()
        {
            string script = Util.GetJavascriptDialogError("Antes de desativar este serviço deverá ser informado se possui um programa ou benefício na edição do serviço aba de Caracterização do Serviço.");
            return script;
        }

        private string CarregarMensagemAlerta()
        {
            //return Util.GetJavaScriptDialogWarning("Verifique atentamente se os valores dos recursos financeiros registrados para este serviço estão corretamente informados para o período em que o serviço esteve ativo, considerando que estes valores continuarão a ser migrados para o respectivo cronograma de desembolso.");
            return Util.GetJavaScriptDialogWarning(R_BLOCO_MSG.R_BLOCO_MSG_III.MSG_EXCLUSAO);
        }
        #endregion
    }

}