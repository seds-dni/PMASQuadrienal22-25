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
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoI
{
    public partial class FFundoMunicipal : System.Web.UI.Page
    {
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

                bloquearControles();

                //adicionarEventos();

                using (var proxy = new ProxyPrefeitura())
                {
                    load(new Prefeituras(proxy));
                    loadGestor(new Prefeituras(proxy));
                }

                verificarAlteracoes();

                this.Master.ScriptManagerControl.SetFocus(txtCNPJ.controleCNPJ);
            }

        }

        void bloquearControles()
        {
            WebControl[] controles = {          
											 txtAnoDecreto,
											 txtAnoLeiCriacao,                                             
											 txtNumeroDecreto,
											 txtNumeroLeiCriacao,
											 rblLeiRegulamenta,
											 rblUnidade,  
											 //txtFMAS,
											 //txtFEAS,
											 //txtFNAS,
											 rblAlteracaoLei , 
											 txtNumeroLei ,
                                             txtAnoLei,
											 btnSalvar
											 };

            Permissao.VerificarPermissaoControles(controles, Session);
            Permissao.VerificarPermissaoControles(txtCNPJ.Controles, Session);
            Permissao.VerificarPermissaoControles(txtdatadecreto.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataPublicacaoLei.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataAlteracao.Controles, Session);
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro9.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 9);
                    linkAlteracoesQuadro9.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("9"));
                }
            }
        }

        void load(Prefeituras prefeituras)
        {
            //Verificar
            //int exercicio = String.IsNullOrEmpty(hdfAno.Value) ? 2018 : Convert.ToInt32(hdfAno.Value);

            FundoMunicipalInfo fmas = prefeituras.GetFMAS(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            if (fmas != null)
            {
                hdfIdFMAS.Value = fmas.Id.ToString();

                if (!String.IsNullOrEmpty(fmas.Lei))
                {
                    txtNumeroLeiCriacao.Text = fmas.Lei.Split('/')[0];
                    txtAnoLeiCriacao.Text = fmas.Lei.Split('/')[1];
                }

                if (fmas.DataCriacao.HasValue)
                    txtDataPublicacaoLei.Text = fmas.DataCriacao.Value.ToShortDateString();

                rblLeiRegulamenta.SelectedValue = Convert.ToSByte(fmas.Regulamenta).ToString();
                if (fmas.Regulamenta)
                {
                    if (!String.IsNullOrEmpty(fmas.NumeroDecreto))
                    {
                        txtNumeroDecreto.Text = fmas.NumeroDecreto.Split('/')[0];
                        txtAnoDecreto.Text = fmas.NumeroDecreto.Split('/')[1];
                    }

                    if (fmas.DataDecreto.HasValue)
                        txtdatadecreto.Text = fmas.DataDecreto.Value.ToShortDateString();
                }
                var fmasValor = fmas.FundosMunicipaisValoresInfo.Single(x => x.Exercicio == 2018);


                //Campos comentados que irão compor o bloco IV
                if (fmasValor.ValorFMAS.HasValue)
                    hidValorFMAS.Value = fmasValor.ValorFMAS.Value.ToString("N2");
                if (fmasValor.ValorFEAS.HasValue)
                    hidValorFEAS.Value = fmasValor.ValorFEAS.Value.ToString("N2");
                if (fmasValor.ValorFNAS.HasValue)
                    hidValorFNAS.Value = fmasValor.ValorFNAS.Value.ToString("N2");
                if (fmasValor.ValorCusteio.HasValue)
                    hidValorCusteio.Value = fmasValor.ValorCusteio.Value.ToString("N2");

                //txtTotalFMAS.Text = (Convert.ToDecimal(txtFMAS.Text) + Convert.ToDecimal(txtFEAS.Text) + Convert.ToDecimal(txtFNAS.Text)).ToString("N2");

                txtCNPJ.Text = fmas.CNPJ;
                rblUnidade.SelectedValue = Convert.ToSByte(fmas.Orcamentaria).ToString();
                if (Convert.ToSByte(fmas.Regulamenta).ToString() == "1")
                {
                    trLeiRegulamenta1.Visible = trLeiRegulamenta2.Visible = txtNumeroDecreto.Visible = txtAnoDecreto.Visible = txtdatadecreto.Visible = true;
                }

                rblAlteracaoLei.SelectedValue = Convert.ToByte(fmas.AlteracaoLei).ToString();
                if (fmas.AlteracaoLei.HasValue && fmas.AlteracaoLei.Value)
                {
                    if (!String.IsNullOrEmpty(fmas.LeiAlterada))
                    {
                        txtNumeroLei.Text = fmas.LeiAlterada.Split('/')[0];
                        txtAnoLei.Text = fmas.LeiAlterada.Split('/')[1];
                    }
                    if (fmas.DataLeiAlterada.HasValue)
                        txtDataAlteracao.Text = fmas.DataLeiAlterada.Value.ToShortDateString();
                }


                //trAlerta.Visible = fmas.Filial;
                var orgaoGestor = new ProxyPrefeitura().Service.GetOrgaoGestorByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                var cnpjRaizOrgaoGEstor = "cnpjRaizOrgaoGEstor";
                var cnpjRaizFMAS = "cnpjRaizFMAS";
                var cnpjRaizPrefeitura = "cnpjRaizPrefeitura";

                if (orgaoGestor != null)
                    if (!String.IsNullOrEmpty(orgaoGestor.CNPJ))
                        cnpjRaizOrgaoGEstor = orgaoGestor.CNPJ.Substring(0, 9);

                if (fmas != null)
                    if (!String.IsNullOrEmpty(fmas.CNPJ))
                        cnpjRaizFMAS = fmas.CNPJ.Substring(0, 9);

                if (SessaoPmas.UsuarioLogado.Prefeitura != null)
                    if (!String.IsNullOrEmpty(SessaoPmas.UsuarioLogado.Prefeitura.CNPJ))
                        cnpjRaizPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.CNPJ.Substring(0, 9);


                trAlerta.Visible = cnpjRaizPrefeitura == cnpjRaizFMAS ||
                    cnpjRaizOrgaoGEstor == cnpjRaizFMAS;

            }

            #region Bloqueia , Desbloqueia e ordena Controles
            WebControl[] controles = { txtnome , 
										 txtNumeroLeiCriacao , 
										 txtAnoLeiCriacao ,                                          
										 rblLeiRegulamenta , 
										 txtNumeroDecreto ,                                          
										 rblUnidade ,  
										 btnSalvar,
                                         btnEditar,
                                         btnSubstituir
					   };

            Permissao.VerificarPermissaoControles(controles, Session);
            Permissao.VerificarPermissaoControles(txtCNPJ.Controles, Session);
            Permissao.VerificarPermissaoControles(txtdatadecreto.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataPublicacaoLei.Controles, Session);
            #endregion

            rblAlteracaoLei_SelectedIndexChanged(null, null);

            fraGestor.Attributes.Add("Class", "frame");
            fraFMAS.Attributes.Add("Class", "frame");
        }

        void loadGestor(Prefeituras prefeituras)
        {
            rblVinculo.DataTextField = "TipoGestor";
            rblVinculo.DataValueField = "Id";
            rblVinculo.DataSource = prefeituras.GetTiposGestoresMunicipal();
            rblVinculo.DataBind();
            carregarGestoresAnteriores(prefeituras);
            var gestor = prefeituras.GetGestorFundoMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (gestor != null)
            {
                hdfIdGestor.Value = gestor.Id.ToString();
                txtnome.Text = gestor.Nome;
                txtCPF.Text = gestor.CPF;
                txtRG.Txtrg = gestor.RG;
                txtRG.Txtdigito = gestor.RGDigito;
                txtDataEmissao.Text = gestor.DataEmissao.Value.ToShortDateString();
                txtdata.Text = gestor.InicioGestao.ToShortDateString();
                txtOrgEmissor.Text = gestor.SiglaEmissor;
                ddlUFEmissor.SelectedValue = gestor.IdUFRG.ToString();
                txtTelefone.Text = gestor.Telefone;
                txtCelular.Text = gestor.Celular;
                txtEmailGestor.Text = gestor.Email;
                rblVinculo.SelectedValue = gestor.IdTipoGestor.ToString();
                rblVinculo_SelectedIndexChanged(null, null);
                txtPortaria.Text = !String.IsNullOrEmpty(gestor.NumeroDecreto) ? gestor.NumeroDecreto : String.Empty;
                txtDecretoGestor.Text = gestor.DataDecreto.HasValue ? gestor.DataDecreto.Value.ToShortDateString() : null;
                InibirCampos();
            }
            else
            {
                ExibirCampos();
            }

            WebControl[] controles = { txtnome , 
                                         txtPortaria,
                                         txtEmailGestor,
                                         lstGestores,
										 txtNumeroLeiCriacao , 
                                         rblVinculo,
                                         txtOrgEmissor, 
                                         ddlUFEmissor,
										 txtAnoLeiCriacao ,                                          
										 rblLeiRegulamenta , 
										 txtNumeroDecreto ,                                          
										 rblUnidade ,  
										 btnSalvarGestor,
                                         btnEditar,
                                         btnSubstituir
					   };


            Permissao.VerificarPermissaoControles(controles, Session);
            Permissao.VerificarPermissaoControles(txtTelefone.Controles, Session);
            Permissao.VerificarPermissaoControles(txtCelular.Controles, Session);
            Permissao.VerificarPermissaoControles(txtCPF.Controles, Session);
            Permissao.VerificarPermissaoControles(txtRG.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataEmissao.Controles, Session);
            Permissao.VerificarPermissaoControles(txtdata.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataTerminoGestao.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDecretoGestor.Controles, Session);
        }
        protected void rblLeiRegulamenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            trLeiRegulamenta1.Visible = trLeiRegulamenta2.Visible = txtNumeroDecreto.Visible = txtdatadecreto.Visible = rblLeiRegulamenta.SelectedItem.Value == "1";
            if (rblLeiRegulamenta.SelectedItem.Value == "0")
            {
                txtNumeroDecreto.Text = string.Empty;
                this.Master.ScriptManagerControl.SetFocus(rblUnidade);
                return;
            }
            fraFMAS.Attributes.Add("Class", "frame active");
            this.Master.ScriptManagerControl.SetFocus(txtNumeroDecreto);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            fraFMAS.Attributes.Add("Class", "frame active");
            String msg = String.Empty;
            SessaoPmas.VerificarSessao(this);
            var fmas = new FundoMunicipalInfo();
            fmas.Id = Convert.ToInt32(hdfIdFMAS.Value);
            fmas.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            if (!String.IsNullOrEmpty(txtCNPJ.Text))
                fmas.CNPJ = txtCNPJ.Text;

            if (!String.IsNullOrEmpty(txtNumeroLeiCriacao.Text) && !String.IsNullOrEmpty(txtAnoLeiCriacao.Text))
                fmas.Lei = txtNumeroLeiCriacao.Text + "/" + txtAnoLeiCriacao.Text;

            DateTime dt;
            if (!String.IsNullOrEmpty(txtDataPublicacaoLei.Text) && DateTime.TryParse(txtDataPublicacaoLei.Text, out dt))
                fmas.DataCriacao = Convert.ToDateTime(txtDataPublicacaoLei.Text);

            fmas.NomeGestor = txtnome.Text;

            fmas.Regulamenta = Convert.ToBoolean(Convert.ToInt32(rblLeiRegulamenta.SelectedValue));

            if (fmas.Regulamenta)
            {
                if (!String.IsNullOrEmpty(txtNumeroDecreto.Text) && !String.IsNullOrEmpty(txtAnoDecreto.Text))
                    fmas.NumeroDecreto = txtNumeroDecreto.Text + "/" + txtAnoDecreto.Text;
                if (!String.IsNullOrEmpty(txtdatadecreto.Text) && DateTime.TryParse(txtdatadecreto.Text, out dt))
                    fmas.DataDecreto = Convert.ToDateTime(txtdatadecreto.Text);
            }
            fmas.AlteracaoLei = Convert.ToBoolean(Convert.ToInt32(rblAlteracaoLei.SelectedValue));
            try
            {
                if (fmas.AlteracaoLei.Value)
                {
                    if (string.IsNullOrEmpty(txtNumeroLei.Text) || String.IsNullOrEmpty(txtAnoLei.Text))
                        msg += "Nº da Lei de Alteração é Obrigatória" + System.Environment.NewLine;

                    if (String.IsNullOrEmpty(txtDataAlteracao.Text) || !DateTime.TryParse(txtDataAlteracao.Text, out dt))
                        msg += "Data da Lei de Alteração é Obrigatória" + System.Environment.NewLine;

                    if (!String.IsNullOrEmpty(txtNumeroLei.Text) && !String.IsNullOrEmpty(txtAnoLei.Text))
                        fmas.LeiAlterada = txtNumeroLei.Text + "/" + txtAnoLei.Text;
                    if (!String.IsNullOrEmpty(txtDataAlteracao.Text) && DateTime.TryParse(txtDataAlteracao.Text, out dt))
                        fmas.DataLeiAlterada = Convert.ToDateTime(txtDataAlteracao.Text);
                }

                fmas.Orcamentaria = Convert.ToBoolean(Convert.ToInt32(rblUnidade.SelectedValue));
                fmas.AlteracaoLei = Convert.ToBoolean(Convert.ToInt32(rblAlteracaoLei.SelectedValue));
                fmas.Bloco = 1;

                new ValidadorFonteRecursoFMAS().ValidarFMAS(fmas);

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    prefeituras.SaveFMAS(fmas);
                    load(prefeituras);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = fmas.Id == 0 ? "Identificação do Fundo Municipal de Assistência Social registrada com sucesso!" : "Identificação do Fundo Municipal de Assistência Social atualizada com sucesso!";
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

        public Decimal RetornaTotalCofinanciamentoRepasseMunicipal()
        {
            int exercicio = 2018;
            Decimal total = 0.0M;

            var prefeituras = (new Prefeituras(new ProxyPrefeitura()));

            var beneficio = prefeituras.GetBeneficioEventual(SessaoPmas.UsuarioLogado.Prefeitura.Id).Where(x => x.Exercicio == exercicio).FirstOrDefault();

            Decimal servicosSocioAssMunicipal = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id,exercicio).Sum(p => p.RedePublicaMunicipal + p.RedePrivadaMunicipal);
            Decimal beneficiosMunicipal = beneficio.ValorAnualMunicipal;
            

            var transf = prefeituras.GetTransferenciaRenda(SessaoPmas.UsuarioLogado.Prefeitura.Id).FirstOrDefault(t => t.TipoTransferencia == 8);
            Decimal transferenciaRendaMunicipal = transf == null ? 0.0M : transf.ValorAnualMunicipal;
            Decimal programaMunicipal = 0.0M;
            var programas = new ProxyProgramas().Service.GetConsultaProgramasProjetosByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            programas.ForEach(a =>
            {
                var programa = new ProxyProgramas().Service.GetProgramaProjetoById(a.Id);
                if(programa.ProgramasProjetosRecursoFinanceiro != null)
                {
                    var recursoExercicio = programa.ProgramasProjetosRecursoFinanceiro.Where(x => x.Exercicio == exercicio).FirstOrDefault();
                    if (recursoExercicio != null)
                    {
                        programaMunicipal += (recursoExercicio.ValorFMAS.HasValue ? recursoExercicio.ValorFMAS.Value : 0M)
                        + (recursoExercicio.ValorOrcamentoMunicipal.HasValue ? recursoExercicio.ValorOrcamentoMunicipal.Value : 0M)
                        + (recursoExercicio.ValorFundoMunicipal.HasValue ? recursoExercicio.ValorFundoMunicipal.Value : 0M);
                    }
                }
            });

            total = servicosSocioAssMunicipal + beneficiosMunicipal + transferenciaRendaMunicipal + programaMunicipal;
            return total;
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
        private void EditarCampos()
        {
            txtdata.Enabled =
            txtEmailGestor.Enabled =
            txtTelefone.Enabled =
            txtCelular.Enabled =
            txtRG.Enabled =
            txtDataEmissao.Enabled =
            txtOrgEmissor.Enabled =
            ddlUFEmissor.Enabled =
            txtDataTerminoGestao.Enabled =
            txtdata.Enabled = rblVinculo.Enabled =
            txtPortaria.Enabled =
            txtDecretoGestor.Enabled =
            btnSalvarGestor.Enabled = true;
            btnEditar.Enabled = false;
            btnSubstituir.Enabled = false;
            fraGestor.Attributes.Add("Class", "frame active");
            fraFMAS.Attributes.Add("Class", "frame");
        }
        private void InibirCampos()
        {
            txtnome.Enabled =
            txtCPF.Enabled =
            txtRG.Enabled =
            txtDataEmissao.Enabled =
            txtOrgEmissor.Enabled =
            ddlUFEmissor.Enabled =
            txtEmailGestor.Enabled =
            txtdata.Enabled =
            txtDataEmissao.Enabled =
            txtDataTerminoGestao.Enabled =
            txtTelefone.Enabled =
            txtCelular.Enabled =
            rblVinculo.Enabled =
            txtPortaria.Enabled =
            txtDecretoGestor.Enabled =
            btnSalvarGestor.Enabled = false;
            btnEditar.Enabled = true;
            btnSubstituir.Enabled = true;
        }
        protected void rblAlteracaoLei_SelectedIndexChanged(object sender, EventArgs e)
        {

            tdDataLeiAlterada.Visible = tdLeiAlterada.Visible = rblAlteracaoLei.SelectedValue == "1";
            if (rblAlteracaoLei.SelectedValue == "0")
            {
                txtNumeroLei.Text = string.Empty;
                txtDataAlteracao.Text = string.Empty;
                //this.Master.ScriptManagerControl.SetFocus(txtFMAS);
                return;
            }
            fraGestor.Attributes.Add("Class", "frame");
            fraFMAS.Attributes.Add("Class", "frame active");
            this.Master.ScriptManagerControl.SetFocus(txtNumeroLei);
        }
        protected void btnSalvarGestor_Click(object sender, EventArgs e)
        {
            fraFMAS.Attributes.Add("Class", "frame");
            fraGestor.Attributes.Add("Class", "frame");
            SessaoPmas.VerificarSessao(this);

            var gestor = new GestorFundoMunicipalInfo();
            gestor.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            gestor.Id = Convert.ToInt32(hdfIdGestor.Value);

            if (!String.IsNullOrEmpty(txtnome.Text))
                gestor.Nome = txtnome.Text;

            if (!String.IsNullOrEmpty(txtCPF.Text))
                gestor.CPF = txtCPF.Text;

            if (!String.IsNullOrEmpty(txtTelefone.DDD.Text) && !String.IsNullOrEmpty(txtTelefone.TELEFONE.Text))
                gestor.Telefone = txtTelefone.Text;
            if (!String.IsNullOrEmpty(txtCelular.DDD.Text) && !String.IsNullOrEmpty(txtCelular.CELULAR.Text))
                gestor.Celular = txtCelular.Text;

            if (!String.IsNullOrEmpty(txtEmailGestor.Text))
                gestor.Email = txtEmailGestor.Text;

            if (!String.IsNullOrEmpty(txtRG.Txtrg.ToString()))
                gestor.RG = txtRG.Txtrg;

            if (!String.IsNullOrEmpty(txtRG.Txtdigito.ToString()))
                gestor.RGDigito = txtRG.Txtdigito;

            if (!String.IsNullOrEmpty(txtOrgEmissor.Text))
                gestor.SiglaEmissor = txtOrgEmissor.Text;

            if (ddlUFEmissor.SelectedValue != "0")
                gestor.IdUFRG = Convert.ToInt32(ddlUFEmissor.SelectedValue);

            DateTime dt;


            if (!String.IsNullOrEmpty(txtPortaria.Text))
                gestor.NumeroDecreto = txtPortaria.Text;

            if (!String.IsNullOrEmpty(txtDecretoGestor.Text))
                gestor.DataDecreto = Convert.ToDateTime(txtDecretoGestor.Text);


            if (!String.IsNullOrEmpty(txtdata.Text) && DateTime.TryParse(txtdata.Text, out dt))
                gestor.InicioGestao = Convert.ToDateTime(txtdata.Text);

            if (!String.IsNullOrEmpty(txtDataEmissao.Text) && DateTime.TryParse(txtDataEmissao.Text, out dt))
                gestor.DataEmissao = Convert.ToDateTime(txtDataEmissao.Text);

            if (!String.IsNullOrEmpty(txtdata.Text) && DateTime.TryParse(txtdata.Text, out dt))
                gestor.InicioGestao = Convert.ToDateTime(txtdata.Text);
            if (!String.IsNullOrEmpty(rblVinculo.SelectedValue))
                gestor.IdTipoGestor = Convert.ToInt32(rblVinculo.SelectedValue);

            gestor.IdStatus = 1;

            String msg = String.Empty;
            try
            {
                new ValidadorGestorFundoMunicipal().Validar(gestor);

                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    if (prefeituras.GetGestoresFundoMunicipalAnteriores(SessaoPmas.UsuarioLogado.Prefeitura.Id).Count() > 0)
                        foreach (var anterior in prefeituras.GetGestoresFundoMunicipalAnteriores(SessaoPmas.UsuarioLogado.Prefeitura.Id).OrderByDescending(t => t.TerminoGestao))
                        {
                            if ((anterior.InicioGestao > Convert.ToDateTime(txtdata.Text))
                                || (anterior.TerminoGestao > Convert.ToDateTime(txtdata.Text)))
                            {
                                throw new Exception("O período de mandato do gestor anterior não pode ser superior ao período de mandato do gestor atual!");
                            }
                        }

                    if (gestor.Id == 0)
                    {
                        prefeituras.AddGestorFundoMunicipal(gestor);
                        msg = "Dados do Gestor Municipal registrado com sucesso!";
                        loadGestor(prefeituras);
                    }
                    else
                    {
                        prefeituras.UpdateGestorFundoMunicipal(gestor);
                        msg = "Dados do Gestor Municipal atualizados com sucesso!";
                    }

                    InibirCampos();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>")), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }
            lblInconsistencias.Text = "";
            tbInconsistencias.Visible = false;
            var script = Util.GetJavaScriptDialogOK(msg);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);


        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            fraGestor.Attributes.Add("Class", "frame active");
            fraFMAS.Attributes.Add("Class", "frame");
            EditarCampos();
        }

        protected void btnSubstituir_Click(object sender, EventArgs e)
        {
            fraFMAS.Attributes.Add("Class", "frame");
            fraGestor.Attributes.Add("Class", "frame active");

            txtDataTerminoGestao.Enabled = btnSalvarTerminoGestao.Enabled = true;
            var script = Util.GetJavaScriptDialogWarning("Para finalizar a substituição do novo gestor, preencha o campo Data final da gestão.");

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        protected void btnSalvarTerminoGestao_Click(object sender, EventArgs e)  
        {
            SessaoPmas.VerificarSessao(this);

            if (Convert.ToDateTime(txtDataTerminoGestao.Text) > DateTime.Today)
            {
                lblInconsistencias.Text = "A data de final da gestão não pode ser posterior à data atual!<br />";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(lblInconsistencias.Text), true);
                tbInconsistencias.Visible = true;
                return;
            }

            if (Convert.ToDateTime(txtDataTerminoGestao.Text) < Convert.ToDateTime(txtdata.Text))
            {
                lblInconsistencias.Text = "A data de final da gestão não pode ser inferior à data de nomeação!<br />";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(lblInconsistencias.Text), true);
                tbInconsistencias.Visible = true;
                return;
            }

            String msg = String.Empty;
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    prefeituras.SubstituirGestorFundoMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id, Convert.ToDateTime(txtDataTerminoGestao.Text));
                    carregarGestoresAnteriores(prefeituras);
                }

                btnSubstituir.Enabled = false;
                btnEditar.Enabled = false;
                btnSalvarGestor.Enabled = true;

                hdfIdGestor.Value = "0";
                ddlUFEmissor.SelectedValue = "0";
                rblVinculo.SelectedValue = null;
                txtnome.Text =
                txtCPF.Text =
                txtRG.Txtrg =
                txtRG.Txtdigito =
                txtDataEmissao.Text =
                txtdata.Text =
                txtDataTerminoGestao.Text =
                txtTelefone.Text = txtCelular.Text = txtOrgEmissor.Text = txtEmailGestor.Text = string.Empty;

                ExibirCampos();

                btnSalvarTerminoGestao.Enabled = txtDataTerminoGestao.Enabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private void ExibirCampos()
        {
            txtnome.Enabled =
            txtCPF.Enabled =
            txtdata.Enabled =
            txtRG.Enabled =
            txtDataEmissao.Enabled =
            txtOrgEmissor.Enabled =
            ddlUFEmissor.Enabled =
            txtEmailGestor.Enabled =
            txtTelefone.Enabled =
            txtDecretoGestor.Enabled =
            txtPortaria.Enabled =
            txtCelular.Enabled = true;
            rblVinculo.SelectedValue = null;
            btnSalvarGestor.Enabled = true;
            txtDataTerminoGestao.Enabled = btnSalvarTerminoGestao.Enabled = btnEditar.Enabled = btnSubstituir.Enabled = false;
        }

        void carregarGestoresAnteriores(Prefeituras prefeituras)
        {
            lstGestores.DataSource = prefeituras.GetGestoresFundoMunicipalAnteriores(SessaoPmas.UsuarioLogado.Prefeitura.Id).OrderByDescending(t => t.InicioGestao);
            lstGestores.DataBind();
        }
        protected void lstGestores_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstGestores.DataKeys[e.Item.DataItemIndex];
            try
            {
                switch (e.CommandName)
                {
                    case "Excluir_Gestor":
                        using (var proxy = new ProxyPrefeitura())
                        {
                            var prefeituras = new Prefeituras(proxy);
                            prefeituras.DeleteGestorFundoMunicipal(Convert.ToInt32(key["Id"]));
                            carregarGestoresAnteriores(prefeituras);
                            var script = Util.GetJavaScriptDialogOK("Gestor do Fundo Municipal removido com sucesso!");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var script = Util.GetJavaScriptDialogOK(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
        }
        protected void lstGestores_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

        protected void rblVinculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tralertaGestor.Visible = false;
            if (rblVinculo.SelectedValue == "1" || rblVinculo.SelectedValue == "4")
                tralertaGestor.Visible = true;
        }
    }
}