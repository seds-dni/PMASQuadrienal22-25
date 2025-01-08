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
    public partial class FCRAS : System.Web.UI.Page
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

                if (String.IsNullOrEmpty(Request.QueryString["idUnidade"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx");
                    return;
                }

                carregarEstruturas();

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    load(proxy);
                }

                adicionarEventos();

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                    if (Request.QueryString["msg"] == "D")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço desativado com sucesso!"), true);

                #region Bloqueia , Desbloqueia
                WebControl[] controles = { 
                                                 txtNome,
                                                 txtIDCRAS, 
                                                 txtCoordenador,
                                                 chkNaoPossuiCoordenador,
                                                 txtCapacidadeAtendimento, 
                                                 txtEmailInstitucional,
                                                 txtNumeroAtendidos,                                                  
                                                 txtVolanteAntropologia,
                                                 txtVolanteDireito,
                                                 txtVolanteEconomia,
                                                 txtVolanteMusicoterapia,
                                                 txtVolanteNivelMedio,
                                                 txtVolanteNivelSuperior,
                                                 txtVolantePedagogia,
                                                 txtVolantePsicologia,
                                                 txtVolanteServicoSocial,
                                                 txtVolanteSociologia,
                                                 txtVolanteTerapiaOcupacional,
                                                 rblEquipeVolante,
                                                 lstAcoesSocioAssistenciais,
                                                 rblServicoPAIF,
                                                 ddlEscolaridade,
                                                 ddlFormacaoAcademica,
                                                 txtOutraAreaFormacao,
                                                 txtTrabalhadoresRemunerados,
                                                 txtEstagiarios,
                                                 txtVoluntarios,
                                                 rblAvaliacaoLocalExecucao,
                                                 btnSalvar,
                                                 txtJustificativaPAIF
                                         };
                Permissao.VerificarPermissaoControles(cep1.Controles, Session);
                Permissao.VerificarPermissaoControles(txtTelefone.Controles, Session);
                Permissao.VerificarPermissaoControles(txtCelular.Controles, Session);
                Permissao.VerificarPermissaoControles(controles, Session);
                Permissao.VerificarPermissaoControles(txtDataImplantacao.Controles, Session);
                #endregion
                rblServicoPAIF_SelectedIndexChanged(null, null);
                this.Master.ScriptManagerControl.SetFocus(txtNome);
            }
        }

        void adicionarEventos()
        {

            txtIDCRAS.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

            txtVolanteServicoSocial.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolantePsicologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolantePedagogia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolanteSociologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolanteDireito.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolanteAntropologia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolanteEconomia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolanteEconomiaDomestica.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolanteTerapiaOcupacional.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolanteMusicoterapia.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolanteNivelMedio.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVolanteNivelSuperior.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

            txtNumeroAtendidos.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtCapacidadeAtendimento.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

        }

        void carregarEstruturas()
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {

                //Formação Acadêmica
                ddlFormacaoAcademica.DataTextField = "Nome";
                ddlFormacaoAcademica.DataValueField = "Id";
                ddlFormacaoAcademica.DataSource = proxy.Service.GetFormacoesAcademicas().OrderBy(t => t.Ordem);
                ddlFormacaoAcademica.DataBind();
                ddlFormacaoAcademica.Items.Insert(0, new ListItem("[Escolha uma Opção]", "0"));

                //Escolaridade
                ddlEscolaridade.DataTextField = "Nome";
                ddlEscolaridade.DataValueField = "Id";
                ddlEscolaridade.DataSource = proxy.Service.GetEscolaridades();
                ddlEscolaridade.DataBind();
                ddlEscolaridade.Items.Insert(0, new ListItem("[Escolha uma Opção]", "0"));

                lstAcoesSocioAssistenciais.DataTextField = "Nome";
                lstAcoesSocioAssistenciais.DataValueField = "Id";
                lstAcoesSocioAssistenciais.DataSource = proxy.Service.GetAcoesSocioAssistenciaisCRAS();
                lstAcoesSocioAssistenciais.DataBind();
            }
        }

        void verificarAlteracoes(Int32 idCRAS)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro21.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 21, idCRAS);
                    linkAlteracoesQuadro21.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("21")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCRAS.ToString()));
                    //linkAlteracoesQuadro22.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 22, idCRAS);
                    //linkAlteracoesQuadro22.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("22")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCRAS.ToString()));
                }
            }
        }

        void carregarDistritosSP()
        {
            ddlDistrito.DataTextField = "Nome";
            ddlDistrito.DataValueField = "Id";
            ddlDistrito.DataSource = new ProxyEstruturaAssistenciaSocial().Service.GetDistritosSP();
            ddlDistrito.DataBind();
            Util.InserirItemEscolha(ddlDistrito);
        }

        void carregarAvaliacoes()
        {
            rblAvaliacaoLocalExecucao.DataTextField = "Descricao";
            rblAvaliacaoLocalExecucao.DataValueField = "Id";
            rblAvaliacaoLocalExecucao.DataSource = new ProxyEstruturaAssistenciaSocial().Service.GetAvaliacoesLocal();
            rblAvaliacaoLocalExecucao.DataBind();

        }

        void load(ProxyRedeProtecaoSocial proxy)
        {
            carregarDistritosSP();
            carregarAvaliacoes();
            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio == 565) //Mostrar os distritos para São Paulo
            {
                trDistritoSP.Visible = true;
            }

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
                return;
            var cras = proxy.Service.GetCRASById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosByCRAS(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Where(p => p.IdUsuarioTipoServico == 39 || p.IdUsuarioTipoServico == 37 || p.IdUsuarioTipoServico == 41).ToList();

            if (cras == null)
                return;

            verificarAlteracoes(cras.Id);

            txtNome.Text = cras.Nome;
            txtIDCRAS.Text = cras.IDCRAS;

            chkNaoPossuiCoordenador.Checked = !cras.PossuiCoordenador;
            if (!cras.PossuiCoordenador)
            {
                ddlEscolaridade.Enabled = false;
                tdFormacaoAcademica.Visible = false;
            }
            else
            {
                if (cras.IdEscolaridadeCoordenador.HasValue)
                {
                    ddlEscolaridade.SelectedValue = cras.IdEscolaridadeCoordenador.ToString();
                    tdFormacaoAcademica.Visible = cras.IdEscolaridadeCoordenador == 4;
                    if (cras.IdEscolaridadeCoordenador == 4)
                    {
                        ddlFormacaoAcademica.SelectedValue = cras.IdFormacaoCoordenador.Value.ToString();
                        //Outra Formação
                        if (cras.IdFormacaoCoordenador == 7)
                        {
                            txtOutraAreaFormacao.Text = cras.OutraFormacaoCoordenador;
                            trOutraFormacao.Visible = true;
                        }
                    }
                }

                txtCoordenador.Text = cras.Coordenador;
            }

            if (SessaoPmas.UsuarioLogado.Perfil == Convert.ToString(EPerfil.Convidados) && servicos.Count > 0)
            {
                cep1.Txtcep = string.Empty;
                cep1.Txtendereco = "Endereço Sigiloso";
                cep1.Txtnumero = string.Empty;
                cep1.Txtcomplemento = string.Empty;
                cep1.Txtbairro = string.Empty;
                cep1.Txtcidade = string.Empty;
                txtTelefone.Text = string.Empty;
                txtCelular.Text = string.Empty;
                txtEmailInstitucional.Text = string.Empty;
                ddlDistrito.SelectedValue = "0";
                ddlDistrito.Enabled = false;
            }
            else
            {
                cep1.Txtcep = cras.CEP;
                cep1.Txtendereco = cras.Logradouro;
                cep1.Txtnumero = cras.Numero;
                cep1.Txtcomplemento = cras.Complemento;
                cep1.Txtbairro = cras.Bairro;
                cep1.Txtcidade = cras.Cidade;
                txtTelefone.Text = cras.Telefone;
                txtCelular.Text = cras.Celular;
                txtEmailInstitucional.Text = cras.Email;

                ddlDistrito.SelectedValue = cras.IdDistritoSaoPaulo.HasValue ? cras.IdDistritoSaoPaulo.Value.ToString() : "0";
            }


            //cep1.Txtcep = cras.CEP;
            //cep1.Txtendereco = cras.Logradouro;
            //cep1.Txtnumero = cras.Numero;
            //cep1.Txtcomplemento = cras.Complemento;
            //cep1.Txtbairro = cras.Bairro;
            //cep1.Txtcidade = cras.Cidade;
            //txtTelefone.Text = cras.Telefone;
            //txtFax.Text = cras.Celular;
            txtCapacidadeAtendimento.Text = cras.CapacidadeAtendimento.ToString();
            txtNumeroAtendidos.Text = cras.NumeroAtendidos.ToString();
            rblImovel.SelectedValue = cras.IdTipoImovel.ToString();
            txtEmailInstitucional.Text = cras.Email;
            txtTrabalhadoresRemunerados.Text = cras.TotalRemunerados.HasValue ? cras.TotalRemunerados.ToString() : String.Empty;
            txtVoluntarios.Text = cras.TotalVoluntarios.HasValue ? cras.TotalVoluntarios.ToString() : String.Empty;
            txtEstagiarios.Text = cras.TotalEstagiarios.HasValue ? cras.TotalEstagiarios.ToString() : String.Empty;



            //RECURSOS HUMANOS
            //TotalRh();
            //RECURSOS HUMANOS EQUIPE VOLANTE
            rblEquipeVolante.SelectedValue = Convert.ToSByte(cras.PossuiEquipeVolante).ToString();
            trEquipeVolante.Visible = cras.PossuiEquipeVolante;
            if (cras.PossuiEquipeVolante)
            {
                txtVolanteAntropologia.Text = cras.TotalFuncionariosVolanteSuperiorAntropologia.ToString();
                txtVolanteDireito.Text = cras.TotalFuncionariosVolanteSuperiorDireito.ToString();
                txtVolanteEconomia.Text = cras.TotalFuncionariosVolanteSuperiorEconomia.ToString();
                txtVolanteEconomiaDomestica.Text = cras.TotalFuncionariosVolanteSuperiorEconomiaDomestica.ToString();
                txtVolanteMusicoterapia.Text = cras.TotalFuncionariosVolanteSuperiorMusicoterapia.ToString();
                txtVolanteNivelMedio.Text = cras.TotalFuncionariosVolanteNivelMedio.ToString();
                txtVolanteNivelSuperior.Text = cras.TotalFuncionariosVolanteNivelSuperior.ToString();
                txtVolantePedagogia.Text = cras.TotalFuncionariosVolanteSuperiorPedagogia.ToString();
                txtVolantePsicologia.Text = cras.TotalFuncionariosVolanteSuperiorPsicologia.ToString();
                txtVolanteServicoSocial.Text = cras.TotalFuncionariosVolanteSuperiorServicoSocial.ToString();
                txtVolanteSociologia.Text = cras.TotalFuncionariosVolanteSuperiorSociologia.ToString();
                txtVolanteTerapiaOcupacional.Text = cras.TotalFuncionariosVolanteSuperiorTerapiaOcupacional.ToString();

                txtNomeLocaisEquipeVolante.Text = cras.NomeLocaisAbrangenciaEquipeVolante;

            }

            rblAvaliacaoLocalExecucao.SelectedValue = cras.IdAvaliacaoLocalExecucao.ToString();
            rblServicoPAIF.SelectedValue = Convert.ToSByte(cras.PossuiPAIF).ToString();
            var servicoPAIF = proxy.Service.GetConsultaServicosRecursosFinanceirosByCRAS(cras.Id).Where(s => s.IdTipoServico == 135).Count();

            if (!cras.PossuiPAIF & servicoPAIF == 0)
                txtJustificativaPAIF.Text = cras.JustificativaPAIF;
            else
                trJustificativaPAIF.Visible = false;


            txtDataImplantacao.Text = cras.DataImplantacao.HasValue ? cras.DataImplantacao.Value.ToShortDateString() : "";

            if (cras.AcoesSocioAssistenciais != null && cras.AcoesSocioAssistenciais.Count > 0)
            {
                foreach (ListItem i in lstAcoesSocioAssistenciais.Items)
                    i.Selected = cras.AcoesSocioAssistenciais.Any(s => s.Id == Convert.ToInt32(i.Value));
            }

            ddlDistrito.SelectedValue = cras.IdDistritoSaoPaulo.HasValue ? cras.IdDistritoSaoPaulo.Value.ToString() : "0";
        }



        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            String msg = String.Empty;


            //if (txtJustificativaPAIF.Text.Length > 300)
            //    txtJustificativaPAIF.Text = txtJustificativaPAIF.Text.Substring(0, 300);

            //TotalRh();

            var cras = new CRASInfo();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                cras.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            cras.IdUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));

            cras.Nome = txtNome.Text;
            cras.IDCRAS = txtIDCRAS.Text;

            cras.PossuiCoordenador = !chkNaoPossuiCoordenador.Checked;
            if (cras.PossuiCoordenador)
            {
                cras.Coordenador = txtCoordenador.Text;
                cras.IdEscolaridadeCoordenador = Convert.ToInt32(ddlEscolaridade.SelectedValue);
                if (cras.IdEscolaridadeCoordenador == 4)
                {
                    cras.IdFormacaoCoordenador = Convert.ToInt32(ddlFormacaoAcademica.SelectedValue);
                    if (cras.IdFormacaoCoordenador == 7)
                        cras.OutraFormacaoCoordenador = txtOutraAreaFormacao.Text;
                }
            }

            if (!String.IsNullOrEmpty(txtEstagiarios.Text))
                cras.TotalEstagiarios = Convert.ToInt32(txtEstagiarios.Text);
            if (!String.IsNullOrEmpty(txtVoluntarios.Text))
                cras.TotalVoluntarios = Convert.ToInt32(txtVoluntarios.Text);
            if (!String.IsNullOrEmpty(txtTrabalhadoresRemunerados.Text))
                cras.TotalRemunerados = Convert.ToInt32(txtTrabalhadoresRemunerados.Text);

            if (!String.IsNullOrEmpty(txtNumeroAtendidos.Text))
                cras.NumeroAtendidos = Convert.ToInt32(txtNumeroAtendidos.Text);
            if (!String.IsNullOrEmpty(txtCapacidadeAtendimento.Text))
                cras.CapacidadeAtendimento = Convert.ToInt32(txtCapacidadeAtendimento.Text);

            cras.Telefone = txtTelefone.Text.Trim();
            cras.Celular = txtCelular.Text.Trim();
            cras.Logradouro = cep1.Txtendereco;
            cras.Numero = cep1.Txtnumero;
            cras.Bairro = cep1.Txtbairro;
            cras.CEP = cep1.Txtcep;
            cras.Complemento = cep1.Txtcomplemento;
            cras.Cidade = cep1.Txtcidade;
            cras.Email = txtEmailInstitucional.Text;
            cras.IdTipoImovel = Convert.ToInt16(rblImovel.SelectedValue);

            //EQUIPE VOLANTE
            cras.PossuiEquipeVolante = rblEquipeVolante.SelectedValue == "1";
            if (cras.PossuiEquipeVolante)
            {
                if (!String.IsNullOrEmpty(txtVolanteNivelMedio.Text))
                    cras.TotalFuncionariosVolanteNivelMedio = Convert.ToInt32(txtVolanteNivelMedio.Text);
                if (!String.IsNullOrEmpty(txtVolanteNivelSuperior.Text))
                    cras.TotalFuncionariosVolanteNivelSuperior = Convert.ToInt32(txtVolanteNivelSuperior.Text);
                if (!String.IsNullOrEmpty(txtVolanteServicoSocial.Text))
                    cras.TotalFuncionariosVolanteSuperiorServicoSocial = Convert.ToInt32(txtVolanteServicoSocial.Text);
                if (!String.IsNullOrEmpty(txtVolantePsicologia.Text))
                    cras.TotalFuncionariosVolanteSuperiorPsicologia = Convert.ToInt32(txtVolantePsicologia.Text);
                if (!String.IsNullOrEmpty(txtVolantePedagogia.Text))
                    cras.TotalFuncionariosVolanteSuperiorPedagogia = Convert.ToInt32(txtVolantePedagogia.Text);
                if (!String.IsNullOrEmpty(txtVolanteSociologia.Text))
                    cras.TotalFuncionariosVolanteSuperiorSociologia = Convert.ToInt32(txtVolanteSociologia.Text);
                if (!String.IsNullOrEmpty(txtVolanteDireito.Text))
                    cras.TotalFuncionariosVolanteSuperiorDireito = Convert.ToInt32(txtVolanteDireito.Text);
                if (!String.IsNullOrEmpty(txtVolanteTerapiaOcupacional.Text))
                    cras.TotalFuncionariosVolanteSuperiorTerapiaOcupacional = Convert.ToInt32(txtVolanteTerapiaOcupacional.Text);
                if (!String.IsNullOrEmpty(txtVolanteMusicoterapia.Text))
                    cras.TotalFuncionariosVolanteSuperiorMusicoterapia = Convert.ToInt32(txtVolanteMusicoterapia.Text);
                if (!String.IsNullOrEmpty(txtVolanteEconomia.Text))
                    cras.TotalFuncionariosVolanteSuperiorEconomia = Convert.ToInt32(txtVolanteEconomia.Text);
                if (!String.IsNullOrEmpty(txtVolanteEconomiaDomestica.Text))
                    cras.TotalFuncionariosVolanteSuperiorEconomiaDomestica = Convert.ToInt32(txtVolanteEconomiaDomestica.Text);
                if (!String.IsNullOrEmpty(txtVolanteAntropologia.Text))
                    cras.TotalFuncionariosVolanteSuperiorAntropologia = Convert.ToInt32(txtVolanteAntropologia.Text);

                cras.NomeLocaisAbrangenciaEquipeVolante = txtNomeLocaisEquipeVolante.Text;
            }

            if (rblAvaliacaoLocalExecucao != null && (!String.IsNullOrEmpty(rblAvaliacaoLocalExecucao.SelectedValue)))
            {
                cras.IdAvaliacaoLocalExecucao = Convert.ToInt32(rblAvaliacaoLocalExecucao.SelectedValue);
            }

            cras.PossuiPAIF = rblServicoPAIF.SelectedValue == "1";
            if (!cras.PossuiPAIF)
                cras.JustificativaPAIF = txtJustificativaPAIF.Text;

            DateTime dt;
            if (!String.IsNullOrEmpty(txtDataImplantacao.Text) && DateTime.TryParse(txtDataImplantacao.Text, out dt))
            {
                cras.DataImplantacao = Convert.ToDateTime(txtDataImplantacao.Text);
            }

            // ACOES SOCIOASSISTENCIAIS
            cras.AcoesSocioAssistenciais = new List<AcaoSocioAssistencialInfo>();
            foreach (ListItem i in lstAcoesSocioAssistenciais.Items)
                if (i.Selected)
                    cras.AcoesSocioAssistenciais.Add(new AcaoSocioAssistencialInfo() { Id = Convert.ToInt32(i.Value) });

            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio == 565) //Mostrar os distritos para São Paulo
            {
                cras.IdDistritoSaoPaulo = ddlDistrito.SelectedValue != "0" ? Convert.ToInt32(ddlDistrito.SelectedValue) : new Nullable<Int32>();
                if (!cras.IdDistritoSaoPaulo.HasValue || cras.IdDistritoSaoPaulo.Value == 0)
                    msg = "O preenchimento do campo Distrito é obrigatório";
            }

            if (msg == "")
            {
                String action = "CRASI";
                try
                {
                    new ValidadorCRAS().Validar(cras);
                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {
                        if (cras.Id == 0)
                            proxy.Service.AddCRAS(cras);
                        else
                        {
                            action = "CRASU";
                            proxy.Service.UpdateCRAS(cras);
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg += ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?msg=" + action + "&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(cras.IdUnidade.ToString())));
                    return;
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("Verifique as inconsistências!"), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }

        protected void ddlFormacaoAcademica_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            //Outra
            trOutraFormacao.Visible = Convert.ToInt32(ddlFormacaoAcademica.SelectedValue) == 7;
            if (!trOutraFormacao.Visible)
            {
                txtOutraAreaFormacao.Text = string.Empty;
                this.Master.ScriptManagerControl.SetFocus(ddlFormacaoAcademica);
            }
            else
            {
                this.Master.ScriptManagerControl.SetFocus(txtOutraAreaFormacao);
            }

        }

        protected void chkNaoPossuiCoordenador_CheckedChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            txtCoordenador.Enabled = ddlEscolaridade.Enabled = !chkNaoPossuiCoordenador.Checked;
            if (chkNaoPossuiCoordenador.Checked)
            {
                ddlEscolaridade.SelectedValue = ddlFormacaoAcademica.SelectedValue = "0";
                txtCoordenador.Text = String.Empty;
                ddlEscolaridade_SelectedIndexChanged(null, null);
            }
            this.Master.ScriptManagerControl.SetFocus(chkNaoPossuiCoordenador);
        }

        protected void rblEquipeVolante_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            trEquipeVolante.Visible = rblEquipeVolante.SelectedValue == "1";
            if (trEquipeVolante.Visible)
            {
                this.Master.ScriptManagerControl.SetFocus(txtVolanteNivelMedio);
                return;
            }
            this.Master.ScriptManagerControl.SetFocus(rblEquipeVolante);
        }

        protected void rblServicoPAIF_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            //se é um cras existente e a resposta tenha sido criado o PAIF deverá redirecionar para a tela de exclusão do serviço
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    var IdCras = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                    var ServicoPaif = proxy.Service.GetConsultaServicosRecursosFinanceirosByCRAS(IdCras).Where(s => s.IdTipoServico == 135).FirstOrDefault();
                    if (ServicoPaif != null)
                    {
                        if (rblServicoPAIF.SelectedValue == "0")
                        {
                            trJustificativaPAIF.Visible = false;
                            //recupero o ID do Serviço PAIF que pertence ao CRAS informado
                            var idCentro = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
                            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
                            // deverá ser redirecionado para a tela de Motivo Exclusão do Serviço CRAS
                            if (ServicoPaif != null)
                                if (!ServicoPaif.Desativado)
                                    Response.Redirect("~/BlocoIII/FMotivoExclusaoServicoCRAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(ServicoPaif.Id.ToString())) + "&idCentro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                        }
                    }
                    else
                    {
                        trJustificativaPAIF.Visible = rblServicoPAIF.SelectedValue == "0";
                    }
                }
            }
            else
            {
                trJustificativaPAIF.Visible = rblServicoPAIF.SelectedValue == "0";
                if (!trJustificativaPAIF.Visible)
                {
                    txtJustificativaPAIF.Text = string.Empty;
                    this.Master.ScriptManagerControl.SetFocus(rblServicoPAIF);
                    return;
                }
                this.Master.ScriptManagerControl.SetFocus(txtJustificativaPAIF);
            }
        }

        protected void ddlEscolaridade_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            //Outra
            tdFormacaoAcademica.Visible = Convert.ToInt32(ddlEscolaridade.SelectedValue) == 4;
            if (!tdFormacaoAcademica.Visible)
            {
                this.Master.ScriptManagerControl.SetFocus(cep1.controleCEP);
                return;
            }
            this.Master.ScriptManagerControl.SetFocus(ddlFormacaoAcademica);
        }
    }
}