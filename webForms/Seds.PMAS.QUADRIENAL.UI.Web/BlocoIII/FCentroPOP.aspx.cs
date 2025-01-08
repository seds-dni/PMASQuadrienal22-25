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
    public partial class FCentroPOP : System.Web.UI.Page
    {
        protected List<CentroPOPMunicipioInfo> MunicipiosDisponiveis
        {
            get { return Session["MUNICIPIOSDISPONIVEIS"] as List<CentroPOPMunicipioInfo>; }
            set { Session["MUNICIPIOSDISPONIVEIS"] = value; }
        }


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

                #region Bloqueia , Desbloqueia
                WebControl[] controles = { 
                                                 txtNome,  
                                                 txtIDCREAS,
                                                 txtCoordenador,
                                                 chkNaoPossuiCoordenador,
                                                 txtCapacidadeAtendimento, 
                                                 txtEmailInstitucional,
                                                 txtNumeroAtendidos,                                                  
                                                 txtTrabalhadoresRemunerados,
                                                 txtVoluntarios,
                                                 txtEstagiarios,                                                  
                                                 rblImovel,
                                                 rblAvaliacaoLocalExecucao,
                                                 rbAtendeUsuarios,
                                                 ddlMunicipioConveniado,
                                                 txtNumeroAtendidos, 
                                                 rbTipoAtendimento,
                                                 lstAcoesSocioAssistenciais,
                                                 rblServicoESR,
                                                 btnSalvar,
                                                     txtTrabalhadoresRemunerados,
                                                 txtEstagiarios,
                                                 txtVoluntarios,
                                                 rblAvaliacaoLocalExecucao,
                                                  ddlEscolaridade,
                                                  txtQuantosAtendidos,
                                                     ddlFormacaoAcademica,
                                                     txtOutraAreaFormacao,
                                                     lstMunicipiosAtendidos,
                                                     ddlMunicipioConveniado,
                                                     txtNumeroAtendidos,
                                                     rbTipoAtendimento,
                                                     btnAdicionarAtendimento,
                                                     txtJustificativaESR
                                         };
                Permissao.VerificarPermissaoControles(cep1.Controles, Session);
                Permissao.VerificarPermissaoControles(txtTelefone.Controles, Session);
                Permissao.VerificarPermissaoControles(txtCelular.Controles, Session);
                Permissao.VerificarPermissaoControles(controles, Session);
                Permissao.VerificarPermissaoControles(txtDataImplantacao.Controles, Session);
                #endregion

                this.Master.ScriptManagerControl.SetFocus(txtNome);
            }
        }

        void verificarAlteracoes(Int32 idCentroPOP)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro32.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 32, idCentroPOP);
                    linkAlteracoesQuadro32.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("32")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentroPOP.ToString()));
                    linkAlteracoesQuadro33.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 33, idCentroPOP);
                    linkAlteracoesQuadro33.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("33")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentroPOP.ToString()));

                }
            }
        }

        void adicionarEventos()
        {
            txtIDCREAS.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroAtendidos.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtCapacidadeAtendimento.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtQuantosAtendidos.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
        }

        void carregarAvaliacoes()
        {
            rblAvaliacaoLocalExecucao.DataTextField = "Descricao";
            rblAvaliacaoLocalExecucao.DataValueField = "Id";
            rblAvaliacaoLocalExecucao.DataSource = new ProxyEstruturaAssistenciaSocial().Service.GetAvaliacoesLocal();
            rblAvaliacaoLocalExecucao.DataBind();

        }
        void carregarMunicipiosConveniados()
        {
            ddlMunicipioConveniado.DataTextField = "Nome";
            ddlMunicipioConveniado.DataValueField = "Id";
            ddlMunicipioConveniado.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
            ddlMunicipioConveniado.DataBind();
            ddlMunicipioConveniado.Items.Insert(0, new ListItem("[Escolha uma Opção]", "0"));
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
                lstAcoesSocioAssistenciais.DataSource = proxy.Service.GetAcoesSocioAssistenciaisCentroPOP();
                lstAcoesSocioAssistenciais.DataBind();


                lstUsuarios.DataTextField = "Nome";
                lstUsuarios.DataValueField = "Id";

                lstUsuarios.DataSource = proxy.Service.GetUsuariosByTipoServico(144).Where(x => x.Id != 27);
                lstUsuarios.DataBind();

                rbTipoAtendimento.DataTextField = "TipoAtendimento";
                rbTipoAtendimento.DataValueField = "Id";
                rbTipoAtendimento.DataSource = proxy.GetTipoAtendimento();
                rbTipoAtendimento.DataBind();
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

        void carregarParcerias()
        {
            lstMunicipiosAtendidos.DataSource = MunicipiosDisponiveis;
            lstMunicipiosAtendidos.DataBind();
            carregarMunicipiosConveniados();
            if (MunicipiosDisponiveis.Count > 0)
            {
                foreach (var item in MunicipiosDisponiveis)
                {
                    ddlMunicipioConveniado.Items.Remove(ddlMunicipioConveniado.Items.FindByValue(item.IdMunicipio.ToString()));
                }
            }
        }

        void load(ProxyRedeProtecaoSocial proxy)
        {
            MunicipiosDisponiveis = new List<CentroPOPMunicipioInfo>();
            carregarDistritosSP();
            carregarAvaliacoes();
            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio == 565) //Mostrar os distritos para São Paulo
                trDistritoSP.Visible = true;

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
                return;
            var centro = proxy.Service.GetCentroPOPById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosByCentroPOP(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Where(p => p.IdUsuarioTipoServico == 39 || p.IdUsuarioTipoServico == 37 || p.IdUsuarioTipoServico == 41).ToList();

            if (centro == null)
                return;

            verificarAlteracoes(centro.Id);

            txtNome.Text = centro.Nome;
            txtIDCREAS.Text = centro.IDCREAS;
            chkNaoPossuiCoordenador.Checked = !centro.PossuiCoordenador;
            if (!centro.PossuiCoordenador)
            {
                ddlEscolaridade.Enabled = false;
                tdFormacaoAcademica.Visible = false;
            }
            else
            {
                if (centro.IdEscolaridadeCoordenador.HasValue)
                {
                    ddlEscolaridade.SelectedValue = centro.IdEscolaridadeCoordenador.ToString();
                    tdFormacaoAcademica.Visible = centro.IdEscolaridadeCoordenador == 4;
                    if (centro.IdEscolaridadeCoordenador == 4)
                    {
                        ddlFormacaoAcademica.SelectedValue = centro.IdFormacaoCoordenador.Value.ToString();
                        //Outra Formação
                        if (centro.IdFormacaoCoordenador == 7)
                        {
                            txtOutraAreaFormacao.Text = centro.OutraFormacaoCoordenador;
                            tdOutraFormacao.Visible = true;
                        }
                    }
                }
                txtCoordenador.Text = centro.Coordenador;
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
                cep1.Txtcep = centro.CEP;
                cep1.Txtendereco = centro.Logradouro;
                cep1.Txtnumero = centro.Numero;
                cep1.Txtcomplemento = centro.Complemento;
                cep1.Txtbairro = centro.Bairro;
                cep1.Txtcidade = centro.Cidade;
                txtTelefone.Text = centro.Telefone;
                txtCelular.Text = centro.Celular;
                txtEmailInstitucional.Text = centro.Email;
                ddlDistrito.SelectedValue = centro.IdDistritoSaoPaulo.HasValue ? centro.IdDistritoSaoPaulo.Value.ToString() : "0";
            }

            txtEmailInstitucional.Text = centro.Email;
            rblImovel.SelectedValue = centro.IdTipoImovel.ToString();
            txtDataImplantacao.Text = centro.DataImplantacao.HasValue ? centro.DataImplantacao.Value.ToShortDateString() : "";
            txtCapacidadeAtendimento.Text = centro.CapacidadeAtendimento.ToString();
            txtNumeroAtendidos.Text = centro.NumeroAtendidos.ToString();
            txtTrabalhadoresRemunerados.Text = centro.TotalRemunerados.HasValue ? centro.TotalRemunerados.ToString() : String.Empty;
            txtEstagiarios.Text = centro.TotalEstagiarios.HasValue ? centro.TotalEstagiarios.ToString() : String.Empty;
            txtVoluntarios.Text = centro.TotalVoluntarios.HasValue ? centro.TotalVoluntarios.ToString() : String.Empty;
            rblAvaliacaoLocalExecucao.SelectedValue = centro.IdAvaliacaoLocalExecucao.ToString();
            rbAtendeUsuarios.SelectedValue = Convert.ToSByte(centro.AtendeOutrosMunicipios).ToString();
            if (centro.AtendeOutrosMunicipios)
            {
                trMunicipios.Visible = trMunicipiosBotao.Visible = true;
                txtQuantosAtendidos.Text = centro.NumeroAtendidosOutrosMunicipios.ToString();
                centro.AbrangenciaMunicipios = proxy.Service.GetMunicipiosAssociadosCentroPOP(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
                if (centro.AbrangenciaMunicipios != null && centro.AbrangenciaMunicipios.Count > 0)
                {
                    lstAtendidos.Visible = true;
                    MunicipiosDisponiveis = centro.AbrangenciaMunicipios;
                    carregarParcerias();
                }
            }

            CarregarServicoEspecializadoSituacaoRua(proxy, centro);


            foreach (ListItem itemCombo in lstUsuarios.Items)
            {
                if (centro.Usuarios != null)
                {
                    itemCombo.Selected = centro.Usuarios.Any(s => s.Id == Convert.ToInt32(itemCombo.Value));
                }
                else {
                    itemCombo.Selected = false;
                }
            }

            if (centro.AcoesSocioAssistenciais != null && centro.AcoesSocioAssistenciais.Count > 0)
            {
                foreach (ListItem i in lstAcoesSocioAssistenciais.Items)
                    i.Selected = centro.AcoesSocioAssistenciais.Any(s => s.Id == Convert.ToInt32(i.Value));
            }
        }

        private void CarregarServicoEspecializadoSituacaoRua(ProxyRedeProtecaoSocial proxy, CentroPOPInfo centro)
        {
            #region Servico ESR

            if (centro.PossuiServicoEspecializadoSituacaoRua.Value)
            {
                rblServicoESR.SelectedValue = "1";
                trJustificativaESR.Visible = false;
                txtJustificativaESR.Visible = false;
                trUsuarios.Visible = true;
            }
            else {
                rblServicoESR.SelectedValue = "0";
                trJustificativaESR.Visible = true;
                txtJustificativaESR.Visible = true;
                txtJustificativaESR.Text = centro.JustificativaServicoEspecializadoSituacaoRua;
                trUsuarios.Visible = false;
            }
            
            #endregion
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {


            String msg = String.Empty;
            var CapacidadeAtendimento = 0M;
            if (!String.IsNullOrEmpty(txtCapacidadeAtendimento.Text))
            {
                if (Util.TryParseDecimal(txtCapacidadeAtendimento.Text).HasValue)
                {
                    CapacidadeAtendimento = Util.TryParseDecimal(txtCapacidadeAtendimento.Text).Value;
                }
            }


            if (Request.QueryString["id"] != null)
            {
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosByCentroPOP(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
                    foreach (var servico in servicos)
                    {
                        if (servico.PrevisaoAnualNumeroAtendidos > CapacidadeAtendimento)
                        {
                            msg += (msg != "" ? System.Environment.NewLine : "") + "Valor da Média mensal de pessoas atendidas neste local de execução está menor que a Previsão Anual de Atendidos do  " + servico.Descricao;
                        }
                    }
                }
            }
            var centro = new CentroPOPInfo();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                centro.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            }
            centro.IdUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            centro.Nome = txtNome.Text;
            centro.IDCREAS = txtIDCREAS.Text;

            centro.PossuiCoordenador = !chkNaoPossuiCoordenador.Checked;
            if (centro.PossuiCoordenador)
            {
                centro.Coordenador = txtCoordenador.Text;
                centro.IdEscolaridadeCoordenador = Convert.ToInt32(ddlEscolaridade.SelectedValue);
                if (centro.IdEscolaridadeCoordenador == 4)
                {
                    centro.IdFormacaoCoordenador = Convert.ToInt32(ddlFormacaoAcademica.SelectedValue);
                    if (centro.IdFormacaoCoordenador == 7)
                        centro.OutraFormacaoCoordenador = txtOutraAreaFormacao.Text;
                }
            }
            if (!String.IsNullOrEmpty(txtTrabalhadoresRemunerados.Text))
                centro.TotalRemunerados = Convert.ToInt32(txtTrabalhadoresRemunerados.Text);
            if (!String.IsNullOrEmpty(txtEstagiarios.Text))
                centro.TotalEstagiarios = Convert.ToInt32(txtEstagiarios.Text);
            if (!String.IsNullOrEmpty(txtVoluntarios.Text))
                centro.TotalVoluntarios = Convert.ToInt32(txtVoluntarios.Text);

            if (!String.IsNullOrEmpty(txtNumeroAtendidos.Text))
                centro.NumeroAtendidos = Convert.ToInt32(txtNumeroAtendidos.Text);
            if (!String.IsNullOrEmpty(txtCapacidadeAtendimento.Text))
            {
                centro.CapacidadeAtendimento = Convert.ToInt32(txtCapacidadeAtendimento.Text);
            }

            centro.Telefone = txtTelefone.Text.Trim();
            centro.Celular = txtCelular.Text.Trim();
            centro.Logradouro = cep1.Txtendereco;
            centro.Numero = cep1.Txtnumero;
            centro.Bairro = cep1.Txtbairro;
            centro.Cidade = cep1.Txtcidade;
            centro.CEP = cep1.Txtcep;
            centro.Complemento = cep1.Txtcomplemento;
            centro.Email = txtEmailInstitucional.Text;

            centro.IdTipoImovel = Convert.ToInt16(rblImovel.SelectedValue);
            if (!String.IsNullOrEmpty(rblAvaliacaoLocalExecucao.SelectedValue))
                centro.IdAvaliacaoLocalExecucao = Convert.ToInt32(rblAvaliacaoLocalExecucao.SelectedValue);

            centro.PossuiServicoEspecializadoSituacaoRua = rblServicoESR.SelectedValue == "1";
            if (!centro.PossuiServicoEspecializadoSituacaoRua.Value)
            {
                if (txtJustificativaESR.Text.Length > 300)
                    txtJustificativaESR.Text = txtJustificativaESR.Text.Substring(0, 300);
                centro.JustificativaServicoEspecializadoSituacaoRua = txtJustificativaESR.Text;
            }

            DateTime dt;
            if (!String.IsNullOrEmpty(txtDataImplantacao.Text) && DateTime.TryParse(txtDataImplantacao.Text, out dt))
                centro.DataImplantacao = Convert.ToDateTime(txtDataImplantacao.Text);


            centro.AtendeOutrosMunicipios = rbAtendeUsuarios.SelectedValue == "1" ? true : false;
            if (centro.AtendeOutrosMunicipios)
                centro.AbrangenciaMunicipios = MunicipiosDisponiveis;

            //ACOES SOCIOASSISTENCIAIS
            centro.AcoesSocioAssistenciais = new List<AcaoSocioAssistencialInfo>();
            foreach (ListItem i in lstAcoesSocioAssistenciais.Items)
                if (i.Selected)
                    centro.AcoesSocioAssistenciais.Add(new AcaoSocioAssistencialInfo() { Id = Convert.ToInt32(i.Value) });

            //USUÁRIOS
            centro.Usuarios = new List<UsuarioTipoServicoInfo>();
            if (centro.PossuiServicoEspecializadoSituacaoRua.HasValue && centro.PossuiServicoEspecializadoSituacaoRua.Value)
            {
                foreach (ListItem i in lstUsuarios.Items)
                    if (i.Selected)
                        centro.Usuarios.Add(new UsuarioTipoServicoInfo() { Id = Convert.ToInt32(i.Value) });
            }

            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio == 565) //Mostrar os distritos para São Paulo
            {
                centro.IdDistritoSaoPaulo = ddlDistrito.SelectedValue != "0" ? Convert.ToInt32(ddlDistrito.SelectedValue) : new Nullable<Int32>();
                if (!centro.IdDistritoSaoPaulo.HasValue || centro.IdDistritoSaoPaulo.Value == 0)
                    msg = "O preenchimento do campo Distrito é obrigatório";
            }

            if (msg == "")
            {
                String action = "POPI";
                try
                {
                    new ValidadorCentroPOP().Validar(centro);

                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {
                        if (centro.Id == 0)
                        {
                            proxy.Service.AddCentroPOP(centro);
                        }
                        else
                        {
                            action = "POPU";
                            proxy.Service.UpdateCentroPOP(centro);
                        }
                    }

                }
                catch (Exception ex)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg = ex.Message), true);
                    //lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
                    tbInconsistencias.Visible = true;
                    msg += ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK(msg), true);
                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?msg=" + action + "&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(centro.IdUnidade.ToString())));
                    return;
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;

        }

        protected void rbAtendeUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            trMunicipios.Visible = trMunicipiosBotao.Visible = rbAtendeUsuarios.SelectedItem.Value == "1";
            carregarMunicipiosConveniados();
            if (!trMunicipios.Visible)
            {

                //this.voltartodos_Click(sender, e);
                txtQuantosAtendidos.Text = string.Empty;
            }
        }


        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }

        protected void ddlFormacaoAcademica_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Outra
            tdOutraFormacao.Visible = Convert.ToInt32(ddlFormacaoAcademica.SelectedValue) == 7;
            if (!tdOutraFormacao.Visible)
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
            txtCoordenador.Enabled = ddlEscolaridade.Enabled = !chkNaoPossuiCoordenador.Checked;
            if (chkNaoPossuiCoordenador.Checked)
            {
                ddlEscolaridade.SelectedValue = ddlFormacaoAcademica.SelectedValue = "0";
                txtCoordenador.Text = String.Empty;
                ddlEscolaridade_SelectedIndexChanged(null, null);
            }
            this.Master.ScriptManagerControl.SetFocus(chkNaoPossuiCoordenador);
        }

        protected void rblServicoESR_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            //se é um cras existente e a resposta tenha sido criado o PAIF deverá redirecionar para a tela de exclusão do serviço
            //if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            //{
            //    //recupero o ID do Serviço PSR que pertence ao Centro POP informado
            //    var idCentro = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
            //    // deverá ser redirecionado para a tela de Motivo Exclusão do Centro POP
            //    var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            //    using (var proxy = new ProxyRedeProtecaoSocial())
            //    {
            //        var IdCentroPOP = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            //        var ServicoESR = proxy.Service.GetConsultaServicosRecursosFinanceirosByCentroPOP(IdCentroPOP).Where(s => s.IdTipoServico == 144).ToList();
            //        if (rblServicoESR.SelectedValue == "0" && ServicoESR.Count > 1)
            //        {
            //            trUsuarios.Visible = true;


            //        }
            //        else
            //        {
            //            var item = ServicoESR.FirstOrDefault();
            //            if (!item.Desativado)
            //                Response.Redirect("~/BlocoIII/FMotivoExclusaoServicoCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.Id.ToString())) + "&idCentro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));

            //        }
            //    }
            //}
            //else
            //{


            trJustificativaESR.Visible = rblServicoESR.SelectedValue == "0";
            txtJustificativaESR.Visible = rblServicoESR.SelectedValue == "0";
            trUsuarios.Visible = rblServicoESR.SelectedValue == "1";
            if (!rblServicoESR.Visible)
            {
                txtJustificativaESR.Text = string.Empty;
                this.Master.ScriptManagerControl.SetFocus(rblServicoESR);
                return;
            }
            //this.Master.ScriptManagerControl.SetFocus(txtJustificativaESR);
            //}
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

        protected void btnAdicionarAtendimento_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var centroPOPMunicipio = new CentroPOPMunicipioInfo();
            centroPOPMunicipio.IdTipoAtendimento = Convert.ToInt32(rbTipoAtendimento.SelectedValue);
            centroPOPMunicipio.IdMunicipio = Convert.ToInt32(ddlMunicipioConveniado.SelectedValue);
            centroPOPMunicipio.NumeroAtendidos = Convert.ToInt32(txtQuantosAtendidos.Text);

            centroPOPMunicipio.Municipio = ddlMunicipioConveniado.SelectedItem.Text;

            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                centroPOPMunicipio.TipoAtendimento = proxy.Service.GetTipoAtendimentoCentroById(centroPOPMunicipio.IdTipoAtendimento);
            }
            MunicipiosDisponiveis = MunicipiosDisponiveis ?? new List<CentroPOPMunicipioInfo>();
            MunicipiosDisponiveis.Add(centroPOPMunicipio);

            lstAtendidos.Visible = true;
            carregarParcerias();

            txtNumeroAtendidos.Text = String.Empty;
            rbTipoAtendimento.SelectedIndex = 0;
            ddlMunicipioConveniado.Items.Remove(ddlMunicipioConveniado.SelectedItem);
            ddlMunicipioConveniado.SelectedIndex = 0;
        }
        protected void lstMunicipiosAtendidos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                Permissao.VerificarPermissaoControles(new[] { ((ImageButton)e.Item.FindControl("btnExcluir")) }, Session);
            }
        }

        protected void lstMunicipiosAtendidos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Excluir_Atendimento":
                        if (MunicipiosDisponiveis == null || MunicipiosDisponiveis.Count == 0)
                            break;
                        MunicipiosDisponiveis.RemoveAt(e.Item.DataItemIndex);
                        carregarParcerias();
                        var script = Util.GetJavaScriptDialogOK("Município excluído com sucesso!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
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

    }
}