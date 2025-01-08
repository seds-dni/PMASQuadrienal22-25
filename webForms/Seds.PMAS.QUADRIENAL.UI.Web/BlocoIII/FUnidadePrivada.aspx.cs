using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Microsoft.IdentityModel.Claims;
using System.Threading;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FUnidadePrivada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "UI")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Unidade da Rede Indireta registrada com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "LI")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Local de Execução da Rede Indireta registrado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "LA")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Local de Execução da Rede Indireta atualizado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "LE")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Local de Execução da Rede Indireta excluído com sucesso!"), true);
                    if (Request.QueryString["msg"] == "LD")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Local de Execução da Rede Indireta desativado com sucesso!"), true);
                }

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                load();
            }

        }

        void verificarAlteracoes(Int32 idUnidade)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro37.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 37, idUnidade);
                    linkAlteracoesQuadro37.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("37")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade.ToString()));

                    lnkAlteracoesLocais.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 38, idUnidade);
                    lnkAlteracoesLocais.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("38")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade.ToString()));
                }
            }
        }

        void load()
        {
            #region Bloqueia , Desbloqueia e ordena Controles
            WebControl[] controles = {  txtRazaoSocial
                                         ,txtNomeFantasia
                                         ,ddlAreaAtuacao
                                         ,txtEmail
                                         ,txtHomePage
                                         ,chkPossuiSite
                                         ,txtResponsavel
                                         ,txtCargo
                                         ,chkAtendimento
                                         ,chkAssessoramento
                                         ,chkDefesaDireitos
                                         ,chkCaracterizacaoAtividades
                                         ,chkCaracterizacaoAtividadesDefesa
                                         ,chkSedeAdministrativa
                                         ,chkPublicoAlvo
                                         ,chkPublicoAlvoDefesa
                                         ,btnSalvar                                         
                                         ,ddlAreaAtuacao
                                         //chkFormaAtuacao
                                         //chkBeneficioEventual
                                         //,chkVivaLeiteBomPrato
                                         ,chkServicosSocioassistencial
                                         ,chkCaracterizacaoAtividades
                                         //,btnEditar
                                         ,chkPublicoAlvo};
            Permissao.VerificarPermissaoControles(controles, Session);
            Permissao.VerificarPermissaoControles(txtCNPJ.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataInicioMandato.Controles, Session);
            Permissao.VerificarPermissaoControles(txtDataTerminoMandato.Controles, Session);

            Permissao.BlocoIII.VerificarPermissaoBlocoIIICMAS(new WebControl[] { ddlSituacaoInscricao });
            Permissao.BlocoIII.VerificarPermissaoBlocoIIICMAS(new WebControl[] { txtInscricaoCMAS });
            Permissao.BlocoIII.VerificarPermissaoBlocoIIICMAS(txtDataPublicacao.Controles);
            Permissao.BlocoIII.VerificarPermissaoBlocoIIICMAS(new WebControl[] { ddlSituacaoAtual });
            
            

            //Liberar para Presidente do CMAS alterar os dados de inscrição da unidade
            if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS)
            {
                IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
                IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
                var idUsuario = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault();

                using (var proxyPrefeitura = new ProxyPrefeitura())
                {
                    var presidente = proxyPrefeitura.Service.GetConselhoMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    txtInscricaoCMAS.Enabled = txtDataPublicacao.Enabled = ddlSituacaoAtual.Enabled = btnSalvar.Enabled =
                        presidente != null && presidente.IdUsuarioPresidente == Convert.ToInt32(idUsuario.Value);
                }
            }
            #endregion

            carregarCombos();

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                btnLocaisDesativados.Visible = false;
                return;
            }
            else {
                btnLocaisDesativados.Visible = true;
            }


            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            UnidadePrivadaInfo unidade;
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                unidade = proxy.Service.GetUnidadePrivadaById(id);

                if (unidade.IdPrefeitura != SessaoPmas.UsuarioLogado.Prefeitura.Id)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                if (unidade.Desativado)
                    Response.Redirect("~/BlocoIII/VUnidadePrivada.aspx?id=" + Server.UrlEncode(Request.QueryString["id"]));

                if (unidade == null)
                    return;
                carregarLocaisExecucao(proxy, id);
            }

            verificarAlteracoes(unidade.Id);

            trCaracterizacaoEntidade.Visible = false;



            if (unidade.IdSituacaoInscricao == 1)
            {
                ddlSituacaoInscricao.SelectedValue = unidade.IdSituacaoInscricao.ToString();
                txtDataPublicacao.Visible = true;
                txtDataPublicacao.Text = unidade.DataPublicacao.HasValue ? unidade.DataPublicacao.Value.ToShortDateString() : "";
                txtInscricaoCMAS.Text = unidade.InscricaoCMAS;
                txtInscricaoCMAS.Visible = true;

                ddlSituacaoAtual.SelectedValue = unidade.IdSituacaoAtualInscricao.HasValue ? unidade.IdSituacaoAtualInscricao.ToString() : "0";
                ddlSituacaoAtual.Visible = true;
            }
            if (unidade.IdSituacaoInscricao == 2)
            {
                ddlSituacaoInscricao.SelectedValue = unidade.IdSituacaoInscricao.ToString();
                txtDataPublicacao.Visible = true;
                txtDataPublicacao.Text = unidade.DataPublicacao.HasValue ? unidade.DataPublicacao.Value.ToShortDateString() : "";
                lblSituacaoAtual.Visible = false;
                ddlSituacaoAtual.Visible = false;
                lblNumeroInscricao.Visible = false;
                txtInscricaoCMAS.Visible = false;
            }
            if (unidade.IdSituacaoInscricao == 3)
            {
                lblNumeroInscricao.Visible = false;
                txtInscricaoCMAS.Visible = false;
                txtDataPublicacao.Visible = false;
                lblSituacaoAtual.Visible = false;
                ddlSituacaoAtual.Visible = false;
            }
            if (!unidade.IdSituacaoInscricao.HasValue)
            {
                lblNumeroInscricao.Visible = false;
                txtInscricaoCMAS.Visible = false;
                txtDataPublicacao.Visible = false;
                lblSituacaoAtual.Visible = false;
                ddlSituacaoAtual.Visible = false;
            }

            txtCNPJ.Text = unidade.CNPJ;
            txtRazaoSocial.Text = unidade.RazaoSocial;
            carregarDadosMantenedora();

            ///Informações sobre a organização
            lblCodigoUnidade.Text = unidade.Id.ToString();
            txtNomeFantasia.Text = !String.IsNullOrEmpty(unidade.NomeFantasia) ? unidade.NomeFantasia.ToString() : String.Empty;
            cep1.Txtcep = !String.IsNullOrEmpty(unidade.CEP) ? unidade.CEP.ToString() : String.Empty;
            cep1.Txtendereco = !String.IsNullOrEmpty(unidade.Logradouro) ? unidade.Logradouro.ToString() : String.Empty;
            cep1.Txtnumero = !String.IsNullOrEmpty(unidade.Numero) ? unidade.Numero.ToString() : String.Empty;
            cep1.Txtcidade = !String.IsNullOrEmpty(unidade.Cidade) ? unidade.Cidade.ToString() : String.Empty;
            cep1.Txtbairro = !String.IsNullOrEmpty(unidade.Bairro) ? unidade.Bairro.ToString() : String.Empty;
            txtTelefone.Text = !String.IsNullOrEmpty(unidade.Telefone) ? unidade.Telefone.ToString() : String.Empty;
            txtCelular.Text = !String.IsNullOrEmpty(unidade.Celular) ? unidade.Celular.ToString() : String.Empty;
            txtEmail.Text = !String.IsNullOrEmpty(unidade.Email) ? unidade.Email.ToString() : String.Empty;
            chkPossuiSite.Checked = unidade.PossuiSite.HasValue ? unidade.PossuiSite.Value : false;
            chkPossuiSite_CheckedChanged(null, null);
            txtHomePage.Text = !String.IsNullOrEmpty(unidade.Site) ? unidade.Site.ToString() : String.Empty;

            ///Dados do responsável
            txtResponsavel.Text = !String.IsNullOrEmpty(unidade.Responsavel) ? unidade.Responsavel.ToString() : String.Empty; ;
            txtCargo.Text = !String.IsNullOrEmpty(unidade.Cargo) ? unidade.Cargo.ToString() : String.Empty;
            txtDataInicioMandato.Text = unidade.DataInicio.HasValue ? Convert.ToDateTime(unidade.DataInicio).ToShortDateString() : String.Empty;
            txtDataTerminoMandato.Text = unidade.DataTermino.HasValue ? Convert.ToDateTime(unidade.DataTermino).ToShortDateString() : String.Empty;


            ///Caracterização da entidade
            ///
            if (unidade.FormasAtuacoes != null && unidade.FormasAtuacoes.Count > 0)
            {
                foreach (var i in unidade.FormasAtuacoes)
                {

                    if (i.Id == 1)
                    {
                        chkAtendimento.Checked = i.Id == 1;

                        foreach (ListItem item in chkServicosSocioassistencial.Items)
                            item.Selected = unidade.CaracterizacaoAtividades.Any(s => s.Id == Convert.ToInt32(item.Value) && s.IdFormaAtuacao == i.Id);

                        bool caracterizado = unidade.CaracterizacaoAtividades.Any(s => s.Id == 1);
                        bool visivel = (caracterizado && Permissao.BlocoIII.VerificaPermissaoAdicionarLocalExecucao(btnLocalExecucao));
                        btnLocalExecucao.Visible = visivel;


                    }
                    if (i.Id == 2)
                    {
                        chkAssessoramento.Checked = i.Id == 2;
                        if (unidade.CaracterizacaoAtividades != null && unidade.CaracterizacaoAtividades.Count > 0)  
                            foreach (ListItem item in chkCaracterizacaoAtividades.Items)
                                item.Selected = unidade.CaracterizacaoAtividades.Any(s => s.Id == Convert.ToInt32(item.Value) && s.IdFormaAtuacao == i.Id);

                        if (unidade.PublicoAlvos != null && unidade.PublicoAlvos.Count > 0)
                            foreach (ListItem item in chkPublicoAlvo.Items)
                                item.Selected = unidade.PublicoAlvos.Any(s => s.Id == Convert.ToInt32(item.Value) && s.IdFormaAtuacao == i.Id);



                    }
                    if (i.Id == 3)
                    {
                        chkDefesaDireitos.Checked = i.Id == 3;
                        if (unidade.CaracterizacaoAtividades != null && unidade.CaracterizacaoAtividades.Count > 0)  
                            foreach (ListItem item in chkCaracterizacaoAtividadesDefesa.Items)
                                item.Selected = unidade.CaracterizacaoAtividades.Any(s => s.Id == Convert.ToInt32(item.Value) && s.IdFormaAtuacao == i.Id);

                        if (unidade.PublicoAlvos != null && unidade.PublicoAlvos.Count > 0)
                            foreach (ListItem item in chkPublicoAlvoDefesa.Items)
                                item.Selected = unidade.PublicoAlvos.Any(s => s.Id == Convert.ToInt32(item.Value) && s.IdFormaAtuacao == i.Id);


                    }
                    chkSedeAdministrativa.Checked = i.Id == 4;
                    chkAtendimento_CheckedChanged(null, null);
                    chkAssessoramento_CheckedChanged(null, null);
                    chkDefesaDireitos_CheckedChanged(null, null);
                }
            }
            ////Inscrição no CMAS
            ddlAreaAtuacao.SelectedValue = unidade.IdAreaAtuacao.HasValue ? unidade.IdAreaAtuacao.ToString() : "0";
            txtInscricaoCMAS.Text = !String.IsNullOrWhiteSpace(unidade.InscricaoCMAS) ? unidade.InscricaoCMAS : "";
            txtDataPublicacao.Text = unidade.DataPublicacao.HasValue ? unidade.DataPublicacao.Value.ToShortDateString() : "";
            ddlSituacaoInscricao.SelectedValue = unidade.IdSituacaoInscricao.HasValue ? unidade.IdSituacaoInscricao.ToString() : "0";
            ddlSituacaoAtual.SelectedValue = unidade.IdSituacaoAtualInscricao.HasValue ? unidade.IdSituacaoAtualInscricao.ToString() : "0";
            frmInfoOrganizacao.Attributes.Add("class", "frame");
        }

        void carregarCombos()
        {
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                ddlAreaAtuacao.DataTextField = "Descricao";
                ddlAreaAtuacao.DataValueField = "Id";
                ddlAreaAtuacao.DataSource = proxy.Service.GetAreaAtuacao().Where(a => a.Exibe == true);
                ddlAreaAtuacao.DataBind();
                Util.InserirItemEscolha(ddlAreaAtuacao, "[Nenhuma opção informada]");

                //WPereira
                ddlSituacaoInscricao.DataTextField = "Nome";
                ddlSituacaoInscricao.DataValueField = "Id";
                ddlSituacaoInscricao.DataSource = proxy.Service.GetSituacaoInscricao();
                ddlSituacaoInscricao.DataBind();
                Util.InserirItemEscolha(ddlSituacaoInscricao, "[Nenhuma opção informada]");

                ddlSituacaoAtual.DataTextField = "Nome";
                ddlSituacaoAtual.DataValueField = "Id";
                ddlSituacaoAtual.DataSource = proxy.Service.GetSituacaoAtualInscricao();
                ddlSituacaoAtual.DataBind();
                Util.InserirItemEscolha(ddlSituacaoAtual, "[Nenhuma opção informada]");

            }

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                chkServicosSocioassistencial.DataTextField = "Nome";
                chkServicosSocioassistencial.DataValueField = "Id";
                chkServicosSocioassistencial.DataSource = proxy.Service.GetCaracterizacaoAtividades().Where(s => s.IdFormaAtuacao == 1);
                chkServicosSocioassistencial.DataBind();

                chkCaracterizacaoAtividades.DataTextField = "Nome";
                chkCaracterizacaoAtividades.DataValueField = "Id";
                chkCaracterizacaoAtividades.DataSource = proxy.Service.GetCaracterizacaoAtividades().Where(s => s.IdFormaAtuacao == 2);
                chkCaracterizacaoAtividades.DataBind();

                chkCaracterizacaoAtividadesDefesa.DataTextField = "Nome";
                chkCaracterizacaoAtividadesDefesa.DataValueField = "Id";
                chkCaracterizacaoAtividadesDefesa.DataSource = proxy.Service.GetCaracterizacaoAtividades().Where(s => s.IdFormaAtuacao == 3);
                chkCaracterizacaoAtividadesDefesa.DataBind();

                chkPublicoAlvoDefesa.DataSource = proxy.Service.GetPublicoAlvos().Where(s => s.IdFormaAtuacao == 3);
                chkPublicoAlvoDefesa.DataTextField = "Nome";
                chkPublicoAlvoDefesa.DataValueField = "Id";
                chkPublicoAlvoDefesa.DataBind();

                chkPublicoAlvo.DataTextField = "Nome";
                chkPublicoAlvo.DataValueField = "Id";
                chkPublicoAlvo.DataSource = proxy.Service.GetPublicoAlvos().Where(s => s.IdFormaAtuacao == 2);
                chkPublicoAlvo.DataBind();

            }
        }

        void carregarDadosMantenedora()
        {

            var obj = new ProxyRedeProtecaoSocial().Service.GetMantenedoraByCNPJ(txtCNPJ.Text);
            frmInfoOrganizacao.Visible = true;
            frmInfoOrganizacao.Attributes.Add("class", "frame");
            if (obj != null)
            {
                //btnEditar.Visible = SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0;
                lblSituacao.Text = obj.Situacao;
                lblMotivoSituacao.Text = obj.MotivoSituacao;
                lblRazaoSocial.Text = "Razão Social:";
                txtRazaoSocial.Text = obj.RazaoSocial;
                txtNomeFantasia.Text = obj.NomeFantasia;
                cep1.Txtendereco = obj.Endereco;
                cep1.Txtcidade = obj.Municipio;
                cep1.Txtcep = obj.CEP;
                cep1.Txtnumero = obj.Numero;
                cep1.Txtcomplemento = obj.Complemento;
                cep1.Txtbairro = obj.Bairro;
                txtTelefone.Text = obj.Telefone;
                txtCelular.Text = obj.Celular;
                txtEmail.Text = obj.Email;
                txtHomePage.Text = obj.HomePage;
                txtResponsavel.Text = obj.Responsavel;
                txtCargo.Text = obj.Cargo;
                txtDataInicioMandato.Text = !String.IsNullOrWhiteSpace(obj.DataInicioMandato) ? obj.DataInicioMandato.ToString() : String.Empty; //: "-";
                txtDataTerminoMandato.Text = !String.IsNullOrWhiteSpace(obj.DataTerminoMandato) ? obj.DataTerminoMandato : String.Empty; // "-";

                if (!String.IsNullOrWhiteSpace(obj.InscricaoCMAS))
                    txtInscricaoCMAS.Text = obj.InscricaoCMAS;

                if (obj.DataPublicacao.HasValue)
                {
                    ddlSituacaoInscricao.SelectedValue = "1";
                    //ddlSituacaoInscricao.Enabled = false;
                    txtDataPublicacao.Text = obj.DataPublicacao.Value.ToShortDateString();
                    lblSituacaoAtual.Visible = true;
                    ddlSituacaoAtual.Visible = true;
                }
                ddlAreaAtuacao.SelectedValue = obj.IdAreaAtuacao.HasValue ? obj.IdAreaAtuacao.Value.ToString() : "0";
                if (ddlAreaAtuacao.SelectedValue == "2")
                    trUnidadeAtendimento.Visible = true;
                //Habilita / Desabilita Campos
                txtCNPJ.Enabled = btnBuscar.Enabled = txtRazaoSocial.Enabled = ddlAreaAtuacao.Enabled = false;
                trSituacao.Visible = trDadosGerais.Visible = trDadosResponsavel.Visible = true;
                tdMotivo.Visible = obj.Situacao.ToLower() == "inativa";
            }
            else
            {
                //  btnEditar.Visible = trAreaAtuacao.Visible = 
                trSituacao.Visible = trDadosGerais.Visible = trDadosResponsavel.Visible = false;
            }
            trNome.Visible = true;
            trDadosResponsavel.Visible = trDadosGerais.Visible = trCaracterizacaoEntidade.Visible = trCMAS.Visible = true;
            txtRazaoSocial.Enabled = lblNaoEncontrado.Visible = trInformacoesNaoEncontrado.Visible = obj == null;
        }

        void carregarLocaisExecucao(ProxyRedeProtecaoSocial proxy, Int32 idUnidade)
        {
            var locais = proxy.Service.GetIdentificacaoLocalExecucaoPrivadoByUnidade(idUnidade).Where(c => c.Desativado != true);

            #region Exibicao Recursos e pivotagem dos cofinanciamentos

            var locaisSource = locais.GroupBy(x => x.Id).Select(g => new
            {
                Id = g.First().Id
                ,
                obj = g.First()
                ,
                Cofinanciamentos = locais.Where(p => p.Id == g.First().Id).Select(x => new
                {
                    ValorCofinanciamentoEstadual = x.ValorCofinanciamentoEstadual
                                                                ,
                    Exercicio = x.Exercicio
                })
            }).ToList();
            #endregion



            lstLocais.DataSource = locaisSource;
            lstLocais.DataBind();
        }

        protected void btnLocalExecucao_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            Response.Redirect("~/BlocoIII/FLocalExecucaoPrivado.aspx?idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString()))); //+ "&idTipoOferta=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("1")));
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            //Gerar inconsistência para Presidente do CMAS caso o mesmo não preencha os dados de inscrição da unidade
            if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS)
            {
                if (ddlSituacaoInscricao.SelectedValue != "0")
                {
                    if (ddlSituacaoInscricao.SelectedValue == "1")
                    {
                        if (String.IsNullOrWhiteSpace(txtInscricaoCMAS.Text) || String.IsNullOrWhiteSpace(txtDataPublicacao.Text) || ddlSituacaoInscricao.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("Todos os campos sobre a inscrição da Unidade devem ser preenchidos!"), true);
                            lblInconsistencias.Text = "Todos os campos sobre a inscrição da Unidade devem ser preenchidos!";
                            tbInconsistencias.Visible = true;
                            return;
                        }
                        else if (String.IsNullOrWhiteSpace(txtInscricaoCMAS.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("A Inscrição do CMAS deve ser informada!"), true);
                            lblInconsistencias.Text = "A Inscrição do CMAS deve ser informada!";
                            tbInconsistencias.Visible = true;
                            return;
                        }
                        else if (ddlSituacaoAtual.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("A Situação Atual da Inscrição deve ser informada!"), true);
                            lblInconsistencias.Text = "A Situação Atual da Inscrição deve ser informada!";
                            tbInconsistencias.Visible = true;
                            return;
                        }
                        else if (String.IsNullOrWhiteSpace(txtDataPublicacao.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("O data do número de inscrição deve ser preenchida!"), true);
                            lblInconsistencias.Text = "O data do número de inscrição deve ser preenchida!";
                            tbInconsistencias.Visible = true;
                            return;
                        }
                    }

                    if (ddlSituacaoInscricao.SelectedValue == "2" && String.IsNullOrWhiteSpace(txtDataPublicacao.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("O Campo Data de Publicação deve ser preenchido!"), true);
                        lblInconsistencias.Text = "O Campo Data de Publicação deve ser preenchido!";
                        tbInconsistencias.Visible = true;
                        return;
                    }
                    if (ddlSituacaoInscricao.SelectedValue != "3" && ddlSituacaoInscricao.SelectedValue != "0")
                    {
                        if (Convert.ToDateTime(txtDataPublicacao.Text) > DateTime.Now)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("A data do número de inscrição do CMAS deve ser anterior à data atual!"), true);
                            lblInconsistencias.Text = "A data do número de inscrição do CMAS deve ser anterior à data atual!";
                            tbInconsistencias.Visible = true;
                            return;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("Selecione uma situação de Inscrição!"), true);
                    lblInconsistencias.Text = "Selecione uma situação de Inscrição!";
                    tbInconsistencias.Visible = true;
                    return;
                }
            }
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                var unidadesPublicas = proxy.Service.GetIdentificacaoUnidadesPublicaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, "");
                var unidadesPrivadas = proxy.Service.GetIdentificacaoUnidadesPrivadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, "");

                List<String> lista = new List<string>();
                lista.AddRange(unidadesPublicas.Select(c => c.CNPJ));
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    lista.AddRange(unidadesPrivadas.Where(f => f.Id != Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Select(c => c.CNPJ));
                }
                else
                {
                    lista.AddRange(unidadesPrivadas.Select(c => c.CNPJ));
                }

                if (lista.Contains(txtCNPJ.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("Já existe uma unidade cadastrada no plano com este CNPJ!"), true);
                    lblInconsistencias.Text = "Já existe uma unidade cadastrada no plano com este CNPJ!";
                    tbInconsistencias.Visible = true;
                    return;
                }
            }

            var unidade = new UnidadePrivadaInfo();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                unidade.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            unidade.CNPJ = txtCNPJ.Text;
            unidade.RazaoSocial = txtRazaoSocial.Text;
            unidade.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            ///Informações sobrea a Organização
            ///
            unidade.NomeFantasia = txtNomeFantasia.Text;
            if (ddlAreaAtuacao.SelectedValue != "0")
                unidade.IdAreaAtuacao = Convert.ToInt32(ddlAreaAtuacao.SelectedValue);
            unidade.CEP = cep1.Txtcep;
            unidade.Logradouro = cep1.Txtendereco;
            unidade.Numero = cep1.Txtnumero;
            unidade.Complemento = cep1.Txtcomplemento;
            unidade.Bairro = cep1.Txtbairro;
            unidade.Cidade = cep1.Txtcidade;
            unidade.PossuiSite = chkPossuiSite.Checked;

            if (!String.IsNullOrEmpty(txtHomePage.Text))
                unidade.Site = txtHomePage.Text;

            if (!String.IsNullOrEmpty(txtTelefone.Text))
                unidade.Telefone = txtTelefone.Text;
            if (!String.IsNullOrEmpty(txtCelular.Text))
                unidade.Celular = txtCelular.Text;
            if (!String.IsNullOrEmpty(txtEmail.Text))
                unidade.Email = txtEmail.Text;

            unidade.Responsavel = txtResponsavel.Text;
            unidade.Cargo = txtCargo.Text;

            if (!String.IsNullOrEmpty(txtDataTerminoMandato.Text))
                unidade.DataInicio = Convert.ToDateTime(txtDataInicioMandato.Text);
            if (!String.IsNullOrEmpty(txtDataTerminoMandato.Text))
                unidade.DataTermino = Convert.ToDateTime(txtDataTerminoMandato.Text);


            unidade.FormasAtuacoes = new List<FormaAtuacaoInfo>();
            unidade.CaracterizacaoAtividades = new List<CaracterizacaoAtividadesInfo>();
            unidade.PublicoAlvos = new List<PublicoAlvoInfo>();
            var msg = String.Empty;
            int contador = 0;

            if (chkAtendimento.Checked)
            {
                unidade.FormasAtuacoes.Add(new FormaAtuacaoInfo() { Id = 1 });
                unidade.CaracterizacaoAtividades = new List<CaracterizacaoAtividadesInfo>();

                foreach (ListItem i in chkServicosSocioassistencial.Items)
                {
                    if (i.Selected)
                    {
                        unidade.CaracterizacaoAtividades.Add(new CaracterizacaoAtividadesInfo() { Id = Convert.ToInt32(i.Value), IdFormaAtuacao = 1 });
                        contador += 1;  
                    }
                }

                if (contador == 0 )
                {
                    msg += "Esta organização foi caracterizada como de atendimento, favor escolher um servico socioassistencial." + System.Environment.NewLine;
                }
            }

            if (chkAssessoramento.Checked)
            {
                unidade.FormasAtuacoes.Add(new FormaAtuacaoInfo() { Id = 2 });
                foreach (ListItem i in chkCaracterizacaoAtividades.Items)
                    if (i.Selected)
                        unidade.CaracterizacaoAtividades.Add(new CaracterizacaoAtividadesInfo() { Id = Convert.ToInt32(i.Value), IdFormaAtuacao = 2 });

                foreach (ListItem i in chkPublicoAlvo.Items)
                    if (i.Selected)
                        unidade.PublicoAlvos.Add(new PublicoAlvoInfo() { Id = Convert.ToInt32(i.Value), IdFormaAtuacao = 2 });
            }
            if (chkDefesaDireitos.Checked)
            {
                unidade.FormasAtuacoes.Add(new FormaAtuacaoInfo() { Id = 3 });
                foreach (ListItem i in chkCaracterizacaoAtividadesDefesa.Items)
                    if (i.Selected)
                        unidade.CaracterizacaoAtividades.Add(new CaracterizacaoAtividadesInfo() { Id = Convert.ToInt32(i.Value), IdFormaAtuacao = 3 });

                foreach (ListItem i in chkPublicoAlvoDefesa.Items)
                    if (i.Selected)
                        unidade.PublicoAlvos.Add(new PublicoAlvoInfo() { Id = Convert.ToInt32(i.Value), IdFormaAtuacao = 3 });
            }
            if (chkSedeAdministrativa.Checked)
                unidade.FormasAtuacoes.Add(new FormaAtuacaoInfo() { Id = 4 });

            unidade.InscricaoCMAS = txtInscricaoCMAS.Text;
            unidade.DataPublicacao = !String.IsNullOrWhiteSpace(txtDataPublicacao.Text) ? Convert.ToDateTime(txtDataPublicacao.Text) : new Nullable<DateTime>();
            unidade.IdSituacaoInscricao = Convert.ToInt32(ddlSituacaoInscricao.SelectedValue) > 0 ?
                Convert.ToInt32(ddlSituacaoInscricao.SelectedValue) : new Nullable<Int32>();
            unidade.IdSituacaoAtualInscricao = Convert.ToInt32(ddlSituacaoAtual.SelectedValue) > 0 ?
                Convert.ToInt32(ddlSituacaoAtual.SelectedValue) : new Nullable<Int32>();
            unidade.UnidadesTipoAtendimentos = new List<UnidadeTipoAtendimentoInfo>();

            
            if (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS)
                        && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS)
            {
                switch (ddlSituacaoInscricao.SelectedValue)
                {
                    case "1":
                        if (String.IsNullOrEmpty(txtInscricaoCMAS.Text))
                            msg += "O preenchimento do número de inscrição no CMAS é Obrigatório" + System.Environment.NewLine;

                        if (String.IsNullOrEmpty(txtDataPublicacao.Text))
                            msg += "O preenchimenmento da data de publicação no CMS é Obrigatória" + System.Environment.NewLine;

                        if (ddlSituacaoAtual.SelectedIndex == 0)
                            msg += "O preenchimento da Situação Atual no CMAS é Obrigatório" + System.Environment.NewLine;

                        break;
                    case "2":
                        if (String.IsNullOrEmpty(txtDataPublicacao.Text))
                            msg += "O preenchimento da data de publicação no CMAS é Obrigatória" + System.Environment.NewLine;
                        break;

                    default:
                        break;
                }
            }

            if (String.IsNullOrEmpty(msg))
            {
                try
                {
                    new ValidadorUnidadePrivada().Validar(unidade);

                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {
                        if (unidade.Id == 0)
                            unidade.Id = proxy.Service.AddUnidadePrivada(unidade);
                        else
                            proxy.Service.UpdateUnidadePrivada(unidade);
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }

            if (String.IsNullOrEmpty(msg))
            {
                if (String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Response.Redirect("~/BlocoIII/FUnidadePrivada.aspx?msg=UI&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(unidade.Id.ToString())));
                    return;
                }
                msg = "Unidade Privada atualizada com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                load();
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CUnidadesPrivadas.aspx");
        }

        protected void lstLocais_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var key = lstLocais.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/FLocalExecucaoPrivado.aspx?id=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPrivado.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            //    proxy.Service.DeleteLocalExecucaoPrivado(Convert.ToInt32(key["Id"].ToString()));

                            //if (SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0)
                            //{
                            var s = proxy.Service.GetConsultaServicosRecursosFinanceirosPrivadoByLocalExecucao(Convert.ToInt32(Genericos.clsCrypto.Decrypt(id))).Where(c => c.Desativado != true);
                            if (s.Count() > 0)
                                throw new Exception("Esse local de execução ainda possui serviços ativos.<br/> Desative primeiro os serviços para desativar o local de execução.");
                            Response.Redirect("~/BlocoIII/FMotivoExclusaoLocalPrivado.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                            //}
                            //else
                            //    proxy.Service.DeleteLocalExecucaoPrivado(Convert.ToInt32(key["Id"].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }

                    Response.Redirect("~/BlocoIII/FUnidadePrivada.aspx?msg=LE&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                default:
                    break;
            }
        }

        protected void lstLocais_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                if (SessaoPmas.UsuarioLogado.EnumPerfil.Value == EPerfil.OrgaoGestor)
                {
                    ((ImageButton)e.Item.FindControl("btnEditUnidade")).Visible = true;
                }
                else
                {
                    ((ImageButton)e.Item.FindControl("btnVisUnidade")).Visible = true;
                    ((ImageButton)e.Item.FindControl("btnVisUnidade")).Enabled = true;
                }

                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();

                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")), ((ImageButton)e.Item.FindControl("btnEditUnidade")) };

                Permissao.VerificarPermissaoControles(controles, Session);
                Permissao.BlocoIII.VerificaPermissaoRedeIndiretaBlocoIIIBotaoExcluirEditar(controles);
            }
        }

        protected void lstLocaisVivaLeiteBomPrato_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluirUnidade")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            carregarDadosMantenedora();
        }


        protected void chkPossuiSite_CheckedChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            txtHomePage.Enabled = !chkPossuiSite.Checked;
            if (chkPossuiSite.Checked)
            {
                txtHomePage.Text = "";
                frmInfoOrganizacao.Attributes.Add("class", "frame active");
            }
            else
            {
                frmInfoOrganizacao.Attributes.Add("class", "frame");
                this.Master.ScriptManagerControl.SetFocus(txtHomePage);
            }
        }
        //protected void chkFormaAtuacao_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SessaoPmas.VerificarSessao(this);
        //    frmInfoOrganizacao.Visible = true;
        //    frmInfoOrganizacao.Attributes.Add("class", "frame active");
        //    trUnidadeAtendimento.Visible = false;
        //    if (chkFormaAtuacao.SelectedValue == "1")
        //    {
        //        trUnidadeAtendimento.Visible = true;
        //    }

        //    if (chkFormaAtuacao.SelectedValue == "2" || chkFormaAtuacao.SelectedValue == "3")
        //    {


        //        trPublicoAlvo.Visible = trCaracterizacaoAtividades.Visible = true;
        //        using (var proxy = new ProxyEstruturaAssistenciaSocial())
        //        {
        //            chkCaracterizacaoAtividades.DataTextField = "Nome";
        //            chkCaracterizacaoAtividades.DataValueField = "Id";
        //            chkCaracterizacaoAtividades.DataSource = chkFormaAtuacao.SelectedValue == "2" ? proxy.Service.GetCaracterizacaoAtividades().Where(c => c.Id > 0 && c.Id < 5).ToList() :
        //                proxy.Service.GetCaracterizacaoAtividades().Where(c => c.Id > 4).ToList();
        //            chkCaracterizacaoAtividades.DataBind();
        //        }
        //    }
        //    else
        //    {
        //        trPublicoAlvo.Visible = trCaracterizacaoAtividades.Visible = false;
        //        //txtPublicoAlvo.Text = "";
        //    }
        //}

        protected void ddlSituacaoInscricao_SelectedIndexChanged(object sender, EventArgs e)
        {
            int situacao = Convert.ToInt32(ddlSituacaoInscricao.SelectedValue);
            switch (situacao)
            {
                case 1:
                    txtDataPublicacao.Visible = true;
                    lblNumeroInscricao.Visible = true;
                    txtInscricaoCMAS.Visible = true;
                    txtInscricaoCMAS.Text = String.Empty;
                    lblSituacaoAtual.Visible = true;
                    ddlSituacaoAtual.Visible = true;
                    break;

                case 2:
                    txtDataPublicacao.Visible = true;
                    txtDataPublicacao.Text = null;
                    lblNumeroInscricao.Visible = false;
                    txtInscricaoCMAS.Text = string.Empty;
                    txtInscricaoCMAS.Visible = false;
                    lblSituacaoAtual.Visible = false;
                    ddlSituacaoAtual.Visible = false;
                    ddlSituacaoAtual.SelectedValue = "0";
                    break;

                case 3:
                    txtDataPublicacao.Visible = false;
                    txtDataPublicacao.Text = null;
                    lblNumeroInscricao.Visible = false;
                    txtInscricaoCMAS.Visible = false;
                    txtInscricaoCMAS.Text = string.Empty;
                    lblSituacaoAtual.Visible = false;
                    ddlSituacaoAtual.Visible = false;
                    ddlSituacaoAtual.SelectedValue = "0";
                    break;

                default:
                    break;
            }
        }

        protected void btnLocalExecucaoVivaLeite_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            Response.Redirect("~/BlocoIII/FLocalExecucaoPrivado.aspx?idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString())) + "&idTipoOferta=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("3")));
        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            btnBuscar.Enabled = txtCNPJ.Enabled = true;
            txtDataPublicacao.Text = txtInscricaoCMAS.Text = String.Empty;
            ddlSituacaoInscricao.SelectedValue = "0";
        }

        protected void chkAtendimento_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAtendimento.Checked)
            {
                btnLocalExecucao.Enabled = trUnidadeAtendimento.Visible = true;
                trAtendimento.Attributes.Add("class", "row bg-cyan fg-black no-margin-bottom");
                trUnidadeAtendimento.Attributes.Add("class", "row bg-cyan fg-black padding10 no-margin-bottom");
                frmInfoOrganizacao.Attributes.Add("class", "active");

            }
            else
            {
                if (lstLocais.Items.Count() > 0)
                {
                   var script = Util.GetJavaScriptDialogWarning("Existem locais de execução ativos, desative-os primeiro para depois desmarcar esta opção.");
                   ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                   chkAtendimento.Checked = true;
                }
                else
                {
                    btnLocalExecucao.Enabled = trUnidadeAtendimento.Visible = false;
                    trAtendimento.Attributes.Add("class", "row");
                }
            }
        }

        protected void chkAssessoramento_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAssessoramento.Checked)
            {
                trCaracterizacaoAtividades.Visible = trPublicoAlvo.Visible = true;
                trAssessoramento.Attributes.Add("class", "row bg-lighterBlue fg-black no-margin-bottom");
                trCaracterizacaoAtividades.Attributes.Add("class", "row bg-lighterBlue fg-black padding10 no-margin-bottom");
                trPublicoAlvo.Attributes.Add("class", "row bg-lighterBlue fg-black padding10 no-margin-bottom");
            }
            else
            {
                trCaracterizacaoAtividades.Visible = trPublicoAlvo.Visible = false;
                trAssessoramento.Attributes.Add("class", "row");
            }

        }

        protected void chkDefesaDireitos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefesaDireitos.Checked)
            {
                trPublicoAlvoDefesa.Visible = trCaracterizacaoDefesa.Visible = true;
                trDefesaDireitos.Attributes.Add("class", "row bg-lightBlue fg-black no-margin-bottom");
                trCaracterizacaoDefesa.Attributes.Add("class", "row bg-lightBlue fg-black padding10 no-margin-bottom");
                trPublicoAlvoDefesa.Attributes.Add("class", "row bg-lightBlue fg-black padding10 no-margin-bottom");
            }
            else
            {
                trPublicoAlvoDefesa.Visible = trCaracterizacaoDefesa.Visible = false;
                trDefesaDireitos.Attributes.Add("class", "row");
            }
        }

        protected void btnLocaisDesativados_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CLocaisPrivadosDesativados.aspx?IdUnidade=" + Server.UrlEncode(Request.QueryString["id"]));
        }

        protected void chkServicosSocioassistencial_SelectedIndexChanged(object sender, EventArgs e)
        {
            var servicoSocioassistencial = chkServicosSocioassistencial.Items.Cast<ListItem>().Where(item => item.Selected && item.Value == "1");

            if (servicoSocioassistencial.Count() == 0 && lstLocais.Items.Count > 0)
            {
                var script = Util.GetJavaScriptDialogWarning("Existem locais de execução ativos, desative-os primeiro para depois desmarcar esta opção.");
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                chkServicosSocioassistencial.Items[0].Selected = true;
                return;
            }

        }
    }
}