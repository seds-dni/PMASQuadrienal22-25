using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.Entidades;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FCREAS : System.Web.UI.Page
    {
        protected List<CREASMunicipioInfo> MunicipiosDisponiveis
        {
            get { return Session["MUNICIPIOSDISPONIVEIS"] as List<CREASMunicipioInfo>; }
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

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                    if (Request.QueryString["msg"] == "D")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço desativado com sucesso!"), true);

                #region Bloqueia , Desbloqueia
                WebControl[] controles = { 
                                                 txtNome, 
                                                 txtIDCREAS,
                                                 txtCoordenador,
                                                 chkNaoPossuiCoordenador,                                                 
                                                 txtEmailInstitucional,
                                                 txtNumeroAtendidos,                                                  
                                                     ddlEscolaridade,
                                                     ddlFormacaoAcademica,
                                                     txtOutraAreaFormacao,
                                                 txtEstagiarios,                                                  
                                                 rblImovel,                                                 
                                                rbAtendeUsuarios,
                                                ddlMunicipioConveniado,
                                                txtNumeroAtendidos,
                                                rbTipoAtendimento,
                                                btnAdicionarAtendimento,
                                                lstMunicipiosAtendidos,
                                                 lstAcoesSocioAssistenciais,
                                                 rblServicoPAEFI,
                                                 btnSalvar,
                                                     txtTrabalhadoresRemunerados,
                                                 txtEstagiarios,
                                                 txtVoluntarios,
                                                 txtQuantosAtendidos,
                                                 rblAvaliacaoLocalExecucao,
                                                 txtJustificativaPAEFI
                                         };
                Permissao.VerificarPermissaoControles(cep1.Controles, Session);
                Permissao.VerificarPermissaoControles(txtTelefone.Controles, Session);
                Permissao.VerificarPermissaoControles(txtCelular.Controles, Session);
                Permissao.VerificarPermissaoControles(controles, Session);
                Permissao.VerificarPermissaoControles(txtDataImplantacao.Controles, Session);
                #endregion
                rblServicoPAEFI_SelectedIndexChanged(null, null);
                this.Master.ScriptManagerControl.SetFocus(txtNome);
            }
        }

        void verificarAlteracoes(Int32 idCREAS)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro26.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 26, idCREAS);
                    linkAlteracoesQuadro26.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("26")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCREAS.ToString()));
                    linkAlteracoesQuadro27.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 27, idCREAS);
                    linkAlteracoesQuadro27.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("27")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCREAS.ToString()));
                    //linkAlteracoesQuadro28.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 28, idCREAS);
                    //linkAlteracoesQuadro28.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("28")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCREAS.ToString()));

                }
            }
        }

        void adicionarEventos()
        {
            txtIDCREAS.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

            txtEstagiarios.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtVoluntarios.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");


            txtQuantosAtendidos.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
            txtNumeroAtendidos.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
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
                lstAcoesSocioAssistenciais.DataSource = proxy.Service.GetAcoesSocioAssistenciaisCREAS();
                lstAcoesSocioAssistenciais.DataBind();

                rbTipoAtendimento.DataTextField = "TipoAtendimento";
                rbTipoAtendimento.DataValueField = "Id";
                rbTipoAtendimento.DataSource = proxy.GetTipoAtendimento();
                rbTipoAtendimento.DataBind();
            }

            //lsMunicipioDisp.DataTextField = "Nome";
            //lsMunicipioDisp.DataValueField = "Id";
            //lsMunicipioDisp.DataSource = SessaoPmas.MunicipiosEstaduais;
            //lsMunicipioDisp.DataBind();

            //lstMunicipiosSel.DataTextField = "Nome";
            //lstMunicipiosSel.DataValueField = "Id";

        }
        void carregarMunicipiosConveniados()
        {
            ddlMunicipioConveniado.DataTextField = "Nome";
            ddlMunicipioConveniado.DataValueField = "Id";
            ddlMunicipioConveniado.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
            ddlMunicipioConveniado.DataBind();
            ddlMunicipioConveniado.Items.Insert(0, new ListItem("[Escolha uma Opção]", "0"));
        }

        void carregarAvaliacoes()
        {
            rblAvaliacaoLocalExecucao.DataTextField = "Descricao";
            rblAvaliacaoLocalExecucao.DataValueField = "Id";
            rblAvaliacaoLocalExecucao.DataSource = new ProxyEstruturaAssistenciaSocial().Service.GetAvaliacoesLocal();
            rblAvaliacaoLocalExecucao.DataBind();

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
            MunicipiosDisponiveis = new List<CREASMunicipioInfo>();
            carregarDistritosSP();
            carregarAvaliacoes();
            carregarMunicipiosConveniados();
            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio == 565) //Mostrar os distritos para São Paulo
            {
                trDistritoSP.Visible = true;
            }

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
                return;
            var centro = proxy.Service.GetCREASPorId(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            //•	Mostrar texto “Localização sigilosa” para usuários com perfil Consulta no campo de endereço nos locais de execução em que funcionam serviço de Acolhimento Institucional – Abrigo (146), 
            //quando usuários forem mulheres em situação de violência (39) ou crianças e adolescentes (37) e 
            //•	Tipo de serviço Abrigo Institucional - Casa Lar(147), quando usuários forem crianças e adolescentes (41). 
            var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosByCREAS(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Where(p => p.IdUsuarioTipoServico == 39 || p.IdUsuarioTipoServico == 37 || p.IdUsuarioTipoServico == 41).ToList();
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
                            trOutraFormacao.Visible = true;
                        }
                    }
                }

                txtCoordenador.Text = centro.Coordenador;
            }

            if (SessaoPmas.UsuarioLogado.Perfil == Convert.ToString(EPerfil.Convidados) && servicos.Count > 0)
            {
                cep1.Txtcep = string.Empty;
                cep1.Txtendereco = "Localização sigilosa";
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


            //cep1.Txtcep = centro.CEP;
            //cep1.Txtendereco = centro.Logradouro;
            //cep1.Txtnumero = centro.Numero;
            //cep1.Txtcomplemento = centro.Complemento;
            //cep1.Txtbairro = centro.Bairro;
            //cep1.Txtcidade = centro.Cidade;
            //txtTelefone.Text = centro.Telefone;
            txtCelular.Text = centro.Celular;
            txtNumeroAtendidos.Text = centro.NumeroAtendidos.ToString();
            txtCapacidadeAtendimento.Text = centro.CapacidadeAtendimento.ToString();

            rblImovel.SelectedValue = centro.IdTipoImovel.ToString();
            txtEmailInstitucional.Text = centro.Email;

            txtEstagiarios.Text = centro.TotalEstagiarios.ToString();
            txtVoluntarios.Text = centro.TotalVoluntarios.ToString();
            txtTrabalhadoresRemunerados.Text = centro.TotalRemunerados.ToString();

            rblAvaliacaoLocalExecucao.SelectedValue = centro.IdAvaliacaoLocalExecucao.ToString();

            rblServicoPAEFI.SelectedValue = Convert.ToSByte(centro.PossuiPAEFI).ToString();
            if (!centro.PossuiPAEFI)
                txtJustificativaPAEFI.Text = centro.JustificativaPAEFI;
            else
                trJustificativaPAEFI.Visible = false;

            txtDataImplantacao.Text = centro.DataImplantacao.HasValue ? centro.DataImplantacao.Value.ToShortDateString() : "";

            if (centro.AcoesSocioAssistenciais != null && centro.AcoesSocioAssistenciais.Count > 0)
            {
                foreach (ListItem i in lstAcoesSocioAssistenciais.Items)
                    i.Selected = centro.AcoesSocioAssistenciais.Any(s => s.Id == Convert.ToInt32(i.Value));
            }

            rbAtendeUsuarios.SelectedValue = Convert.ToSByte(centro.AtendeOutrosMunicipios).ToString();
            if (centro.AtendeOutrosMunicipios)
            {
                trMunicipios.Visible = trMunicipiosBotao.Visible = true;
                txtQuantosAtendidos.Text = centro.NumeroAtendidosOutrosMunicipios.ToString();
                if (centro.AbrangenciaMunicipios != null && centro.AbrangenciaMunicipios.Count > 0)
                {
                    lstAtendidos.Visible = true;
                    MunicipiosDisponiveis = centro.AbrangenciaMunicipios;
                    carregarParcerias();
                }
            }

            ddlDistrito.SelectedValue = centro.IdDistritoSaoPaulo.HasValue ? centro.IdDistritoSaoPaulo.Value.ToString() : "0";
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            String msg = String.Empty;
            var CapacidadeAtendimento = 0M;
            if (!String.IsNullOrEmpty(txtCapacidadeAtendimento.Text))
                if (Util.TryParseDecimal(txtCapacidadeAtendimento.Text).HasValue)
                    CapacidadeAtendimento = Util.TryParseDecimal(txtCapacidadeAtendimento.Text).Value;
            //if (Request.QueryString["id"] != null)
            //{
            //    using (var proxy = new ProxyRedeProtecaoSocial())
            //    {
            //        var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosByCREAS(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            //        foreach (var servico in servicos)
            //        {
            //            if (servico.PrevisaoAnualNumeroAtendidos > CapacidadeAtendimento)
            //            {
            //                msg += (msg != "" ? System.Environment.NewLine : "") + "Valor da Previsão anual do número de pessoas atendidas neste local de execução está menor que a Previsão Anual de Atendidos do  " + servico.Descricao;
            //            }
            //        }
            //    }
            //}
            //TotalRh();

            var centro = new CREASInfo();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                centro.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            centro.IdUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            centro.IDCREAS = txtIDCREAS.Text;
            centro.Nome = txtNome.Text;

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

            if (!String.IsNullOrEmpty(txtNumeroAtendidos.Text))
                centro.NumeroAtendidos = Convert.ToInt32(txtNumeroAtendidos.Text);
            if (!String.IsNullOrEmpty(txtCapacidadeAtendimento.Text))
                centro.CapacidadeAtendimento = Convert.ToInt32(txtCapacidadeAtendimento.Text);


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

            if (!String.IsNullOrEmpty(txtTrabalhadoresRemunerados.Text))
                centro.TotalRemunerados = Convert.ToInt32(txtTrabalhadoresRemunerados.Text);

            if (!String.IsNullOrEmpty(txtEstagiarios.Text))
                centro.TotalEstagiarios = Convert.ToInt32(txtEstagiarios.Text);

            if (!String.IsNullOrEmpty(txtVoluntarios.Text))
                centro.TotalVoluntarios = Convert.ToInt32(txtVoluntarios.Text);

            if (!String.IsNullOrEmpty(rblAvaliacaoLocalExecucao.SelectedValue))
                centro.IdAvaliacaoLocalExecucao = Convert.ToInt32(rblAvaliacaoLocalExecucao.SelectedValue);



            centro.PossuiPAEFI = rblServicoPAEFI.SelectedValue == "1";
            if (!centro.PossuiPAEFI)
            {
                if (txtJustificativaPAEFI.Text.Length > 300)
                    txtJustificativaPAEFI.Text = txtJustificativaPAEFI.Text.Substring(0, 300);
                centro.JustificativaPAEFI = txtJustificativaPAEFI.Text;
            }

            DateTime dt;
            if (!String.IsNullOrEmpty(txtDataImplantacao.Text) && DateTime.TryParse(txtDataImplantacao.Text, out dt))
            {
                centro.DataImplantacao = Convert.ToDateTime(txtDataImplantacao.Text);
            }

            //ACOES SOCIOASSISTENCIAIS
            centro.AcoesSocioAssistenciais = new List<AcaoSocioAssistencialInfo>();
            foreach (ListItem i in lstAcoesSocioAssistenciais.Items)
                if (i.Selected)
                    centro.AcoesSocioAssistenciais.Add(new AcaoSocioAssistencialInfo() { Id = Convert.ToInt32(i.Value) });

            centro.AtendeOutrosMunicipios = rbAtendeUsuarios.SelectedValue == "1" ? true : false;
            if (centro.AtendeOutrosMunicipios)
            {
                centro.AbrangenciaMunicipios = MunicipiosDisponiveis;
            }


            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio == 565) //Mostrar os distritos para São Paulo
            {
                centro.IdDistritoSaoPaulo = ddlDistrito.SelectedValue != "0" ? Convert.ToInt32(ddlDistrito.SelectedValue) : new Nullable<Int32>();
                if (!centro.IdDistritoSaoPaulo.HasValue || centro.IdDistritoSaoPaulo.Value == 0)
                    msg = "O preenchimento do campo Distrito é obrigatório";
            }

            if (msg == "")
            {
                String action = "CREASI";
                try
                {
                    new ValidadorCREAS().Validar(centro);

                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {
                        if (centro.Id == 0)
                            proxy.Service.AddCREAS(centro);
                        else
                        {
                            action = "CREASU";
                            proxy.Service.UpdateCREAS(centro);
                        }
                    }

                }
                catch (Exception ex)
                {
                    msg += ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?msg=" + action + "&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(centro.IdUnidade.ToString())));
                    return;
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
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

        protected void rblServicoPAEFI_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    var IdCreas = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
                    //Verificar se existe PAEFI
                    var ServicoPaefi = proxy.Service.GetConsultaServicosRecursosFinanceirosByCREAS(IdCreas).Where(s => s.IdTipoServico == 139).FirstOrDefault();
                    if (ServicoPaefi != null)
                    {
                        if (rblServicoPAEFI.SelectedValue == "0")
                        {
                            trJustificativaPAEFI.Visible = false;
                            var idCentro = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
                            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
                            // deverá ser redirecionado para a tela de Motivo Exclusão do Serviço CRAS
                            if (!ServicoPaefi.Desativado)
                                Response.Redirect("~/BlocoIII/FMotivoExclusaoServicoCREAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(ServicoPaefi.Id.ToString())) + "&idCentro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                        }
                    }
                    else
                    {
                        trJustificativaPAEFI.Visible = rblServicoPAEFI.SelectedValue == "0";
                    }
                }
            }
            else
            {
                trJustificativaPAEFI.Visible = rblServicoPAEFI.SelectedValue == "0";
                if (!trJustificativaPAEFI.Visible)
                {
                    txtJustificativaPAEFI.Text = string.Empty;
                    this.Master.ScriptManagerControl.SetFocus(rblServicoPAEFI);
                    return;
                }
                this.Master.ScriptManagerControl.SetFocus(txtJustificativaPAEFI);
            }
        }

        //protected void voltarum_Click(object sender, EventArgs e)
        //{
        //    Util.MoveSelectedItems(lstMunicipiosSel, lsMunicipioDisp);
        //}
        //protected void voltartodos_Click(object sender, EventArgs e)
        //{
        //    Util.MoveAllItems(lstMunicipiosSel, lsMunicipioDisp);
        //}
        //protected void incluium_Click(object sender, EventArgs e)
        //{
        //    Util.MoveSelectedItems(lsMunicipioDisp, lstMunicipiosSel);
        //}
        //protected void incluitodos_Click(object sender, EventArgs e)
        //{
        //    Util.MoveAllItems(lsMunicipioDisp, lstMunicipiosSel);
        //}

        protected void rbAtendeUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            trMunicipios.Visible = trMunicipiosBotao.Visible = rbAtendeUsuarios.SelectedItem.Value == "1";
            if (!trMunicipios.Visible)
            {
                //this.voltartodos_Click(sender, e);
                txtQuantosAtendidos.Text = string.Empty;
            }
        }
        //protected void rbAtendeMunicipioConveniado_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SessaoPmas.VerificarSessao(this);
        //    trMunicipioConveniado.Visible = rbAtendeMunicipioConveniado.SelectedItem.Value == "1";
        //    if (!trMunicipioConveniado.Visible)
        //    {

        //    }
        //}

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

            var creasMunicipio = new CREASMunicipioInfo();
            creasMunicipio.IdTipoAtendimento = Convert.ToInt32(rbTipoAtendimento.SelectedValue);
            creasMunicipio.IdMunicipio = Convert.ToInt32(ddlMunicipioConveniado.SelectedValue);
            creasMunicipio.NumeroAtendidos = Convert.ToInt32(txtQuantosAtendidos.Text);

            creasMunicipio.Municipio = ddlMunicipioConveniado.SelectedItem.Text;
            creasMunicipio.TipoAtendimento = new TipoAtendimentoInfo() { TipoAtendimento = rbTipoAtendimento.SelectedItem.Text };
            MunicipiosDisponiveis = MunicipiosDisponiveis ?? new List<CREASMunicipioInfo>();
            MunicipiosDisponiveis.Add(creasMunicipio);

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