using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class VCRAS : System.Web.UI.Page
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


                //if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                //    if (Request.QueryString["msg"] == "D")
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Serviço desativado com sucesso!"), true);

                //    rblServicoPAIF_SelectedIndexChanged(null, null);
                this.Master.ScriptManagerControl.SetFocus(txtNome);
            }
        }

        void carregarEstruturas()
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {

                ////Formação Acadêmica
                //ddlFormacaoAcademica.DataTextField = "Nome";
                //ddlFormacaoAcademica.DataValueField = "Id";
                //ddlFormacaoAcademica.DataSource = proxy.Service.GetFormacoesAcademicas().OrderBy(t => t.Ordem);

                ////Escolaridade
                //ddlEscolaridade.DataTextField = "Nome";
                //ddlEscolaridade.DataValueField = "Id";
                //ddlEscolaridade.DataSource = ;
                //ddlEscolaridade.DataBind();
                //ddlEscolaridade.Items.Insert(0, new ListItem("[Escolha uma Opção]", "0"));

                //lstAcoesSocioAssistenciais.DataTextField = "Nome";
                //lstAcoesSocioAssistenciais.DataValueField = "Id";
                //lstAcoesSocioAssistenciais.DataSource = proxy.Service.GetAcoesSocioAssistenciaisCRAS();
                //lstAcoesSocioAssistenciais.DataBind();
            }
        }

        //void verificarAlteracoes(Int32 idCRAS)
        //{
        //    if (Util.VerificarAlteracoes())
        //    {
        //        using (var proxy = new ProxyPlanoMunicipal())
        //        {
        //            linkAlteracoesQuadro21.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 21, idCRAS);
        //            linkAlteracoesQuadro21.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("21")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCRAS.ToString()));
        //            //linkAlteracoesQuadro22.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 22, idCRAS);
        //            //linkAlteracoesQuadro22.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("22")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCRAS.ToString()));
        //        }
        //    }
        //}

        //void carregarDistritosSP()
        //{
        //    ddlDistrito.DataTextField = "Nome";
        //    ddlDistrito.DataValueField = "Id";
        //    ddlDistrito.DataSource = new ProxyEstruturaAssistenciaSocial().Service.GetDistritosSP();
        //    ddlDistrito.DataBind();
        //    Util.InserirItemEscolha(ddlDistrito);
        //}

        //void carregarAvaliacoes()
        //{
        //    rblAvaliacaoLocalExecucao.DataTextField = "Descricao";
        //    rblAvaliacaoLocalExecucao.DataValueField = "Id";
        //    rblAvaliacaoLocalExecucao.DataSource = new ProxyEstruturaAssistenciaSocial().Service.GetAvaliacoesLocal();
        //    rblAvaliacaoLocalExecucao.DataBind();

        //}

        void load(ProxyRedeProtecaoSocial proxy)
        {
            //carregarDistritosSP();
            //carregarAvaliacoes();
            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio == 565) //Mostrar os distritos para São Paulo
                trDistritoSP.Visible = true;

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
                return;
            var cras = proxy.Service.GetCRASById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosByCRAS(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Where(p => p.IdUsuarioTipoServico == 39 || p.IdUsuarioTipoServico == 37 || p.IdUsuarioTipoServico == 41).ToList();

            if (cras == null)
                return;

            //verificarAlteracoes(cras.Id);

            txtNome.Text = cras.Nome;
            txtIDCRAS.Text = cras.IDCRAS;
            txtCoordenador.Text = cras.PossuiCoordenador ? cras.Coordenador : "Não Possui coordenador";

            tdEscolaridade.Visible = tdFormacaoAcademica.Visible = cras.PossuiCoordenador;
            if (cras.IdEscolaridadeCoordenador.HasValue)
            {
                tdEscolaridade.Visible = true;
                txtEscolaridade.Text = new ProxyEstruturaAssistenciaSocial().Service.GetEscolaridades().Where(c => c.Id == cras.IdEscolaridadeCoordenador).FirstOrDefault().Nome;
                tdFormacaoAcademica.Visible = cras.IdEscolaridadeCoordenador == 4;
                if (cras.IdEscolaridadeCoordenador == 4)
                {
                    txtFormacaoAcademica.Text = new ProxyEstruturaAssistenciaSocial().Service.GetFormacoesAcademicas().Where(c => c.Id == cras.IdFormacaoCoordenador).FirstOrDefault().Nome;
                    //Outra Formação
                    if (cras.IdFormacaoCoordenador == 7)
                    {
                        txtOutraAreaFormacao.Text = cras.OutraFormacaoCoordenador;
                        trOutraFormacao.Visible = true;
                    }
                }
            }

            if (SessaoPmas.UsuarioLogado.Perfil == Convert.ToString(EPerfil.Convidados) && servicos.Count > 0)
            {
                lblCep.Text = string.Empty;
                lblLogradouro.Text = "Endereço Sigiloso";
                lblNumero.Text =
                lblComplemento.Text =
                lblBairro.Text =
                lblCidade.Text =
                lblTelefone.Text =
                lblCelular.Text =
                txtEmailInstitucional.Text =
                lblDistrito.Text = String.Empty;
            }
            else
            {
                lblCep.Text = cras.CEP.Substring(cras.CEP.Length - 8, 8);
                lblCep.Text = cras.CEP.Insert(5, "-");
                lblLogradouro.Text = cras.Logradouro;
                lblNumero.Text = cras.Numero;
                lblComplemento.Text = cras.Complemento;
                lblBairro.Text = cras.Bairro;
                lblCidade.Text = cras.Cidade;
                if (!String.IsNullOrEmpty(cras.Telefone))
                {
                    string sDDD, sTelefone;
                    string sTelefoneCompleto = cras.Telefone;
                    sTelefoneCompleto = "0000000000" + sTelefoneCompleto;
                    sTelefoneCompleto = sTelefoneCompleto.Substring(sTelefoneCompleto.Length - 10, 10);
                    sDDD = sTelefoneCompleto.Substring(0, 2);
                    sTelefone = sTelefoneCompleto.Substring(2, 8);
                    sTelefone = sTelefone.Insert(4, "-");
                    lblTelefone.Text = "(" + sDDD + ") " + sTelefone;
                }

                if (!string.IsNullOrEmpty(cras.Celular))
                {
                    string sCelularCompleto = cras.Celular;
                    string sDDDCelular, sCelular = "";

                    sCelularCompleto = "00000000000" + sCelularCompleto;
                    sCelularCompleto = sCelularCompleto.Substring(sCelularCompleto.Length - 11, 11);

                    sDDDCelular = sCelularCompleto.Substring(0, 2);
                    sCelular = sCelularCompleto.Substring(2, 9);
                    sCelular = sCelular.Insert(5, "-");

                    lblCelular.Text = "(" + sDDDCelular + ") " + sCelular;
                }

                txtEmailInstitucional.Text = cras.Email;
                if (cras.IdDistritoSaoPaulo.HasValue)
                    lblDistrito.Text = new ProxyEstruturaAssistenciaSocial().Service.GetDistritosSP().Where(s => s.Id == cras.IdDistritoSaoPaulo.Value).FirstOrDefault().Nome;
            }

            txtCapacidadeAtendimento.Text = cras.CapacidadeAtendimento.ToString();
            txtNumeroAtendidos.Text = cras.NumeroAtendidos.ToString();
            switch (cras.IdTipoImovel)
            {
                case 1:
                    lblImovel.Text = "Próprio";
                    break;
                case 2:
                    lblImovel.Text = "Cedido";
                    break;
                case 3:
                    lblImovel.Text = "Alugado";
                    break;
            }
            txtEmailInstitucional.Text = cras.Email;
            txtTrabalhadoresRemunerados.Text = cras.TotalRemunerados.HasValue ? cras.TotalRemunerados.ToString() : String.Empty;
            txtVoluntarios.Text = cras.TotalVoluntarios.HasValue ? cras.TotalVoluntarios.ToString() : String.Empty;
            txtEstagiarios.Text = cras.TotalEstagiarios.HasValue ? cras.TotalEstagiarios.ToString() : String.Empty;
            txtDataImplantacao.Text = cras.DataImplantacao.HasValue ? cras.DataImplantacao.Value.ToShortDateString() : "";
            if (cras.Desativado)
            {
                lblDataExclusaoRegistro.Text = cras.DataDesativacao.HasValue ? cras.DataDesativacao.Value.ToShortDateString() : cras.DataRegistroLog.Value.ToShortDateString();
                lblMotivoExclusao.Text = new ProxyRedeProtecaoSocial().Service.GetMotivoDesativacaoLocalById(cras.IdMotivoDesativacao.Value).Descricao;
                if (cras.IdMotivoDesativacao == 10)
                {
                    trMotivoEncerramento.Visible = trDetalhamento.Visible = true;
                    lblMotivoEncerramento.Text = new ProxyRedeProtecaoSocial().Service.GetMotivoDesativacaoLocalById(cras.IdMotivoEncerramento.Value).Descricao;
                    lblDetalhamentoEncerramento.Text = cras.Detalhamento;
                }
            }
            //RECURSOS HUMANOS
            //TotalRh();
            //RECURSOS HUMANOS EQUIPE VOLANTE
            rblEquipeVolante.Text = cras.PossuiEquipeVolante ? "Sim" : "Não";
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


            lblServicoPAIF.Text = cras.PossuiPAIF ? "Sim" : "Não";
            var servicoPAIF = proxy.Service.GetConsultaServicosRecursosFinanceirosByCRAS(cras.Id).Where(s => s.IdTipoServico == 135).Count();
            if (!cras.PossuiPAIF & servicoPAIF == 0)
                txtJustificativaPAIF.Text = cras.JustificativaPAIF;
            else
                trJustificativaPAIF.Visible = false;

            if (cras.AcoesSocioAssistenciais != null && cras.AcoesSocioAssistenciais.Count > 0)
            {
                lstAcoesSocioAssistenciais.DataSource = cras.AcoesSocioAssistenciais;
                lstAcoesSocioAssistenciais.DataBind();
            }

            if (cras.IdAvaliacaoLocalExecucao.HasValue)
                lblAvaliacaoLocalExecucao.Text = new ProxyEstruturaAssistenciaSocial().Service.GetAvaliacoesLocal().Where(s => s.Id == cras.IdAvaliacaoLocalExecucao.Value).FirstOrDefault().Descricao;

            if (cras.IdDistritoSaoPaulo.HasValue)
                lblDistrito.Text = new ProxyEstruturaAssistenciaSocial().Service.GetDistritosSP().Where(s => s.Id == cras.IdDistritoSaoPaulo.Value).FirstOrDefault().Nome;
        }




        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }

        //protected void ddlFormacaoAcademica_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SessaoPmas.VerificarSessao(this);

        //    //Outra
        //    trOutraFormacao.Visible = Convert.ToInt32(ddlFormacaoAcademica.SelectedValue) == 7;
        //    if (!trOutraFormacao.Visible)
        //    {
        //        txtOutraAreaFormacao.Text = string.Empty;
        //        this.Master.ScriptManagerControl.SetFocus(ddlFormacaoAcademica);
        //    }
        //    else
        //    {
        //        this.Master.ScriptManagerControl.SetFocus(txtOutraAreaFormacao);
        //    }

        //}

        //protected void chkNaoPossuiCoordenador_CheckedChanged(object sender, EventArgs e)
        //{
        //    SessaoPmas.VerificarSessao(this);

        //    txtCoordenador.Enabled = ddlEscolaridade.Enabled = !chkNaoPossuiCoordenador.Checked;
        //    if (chkNaoPossuiCoordenador.Checked)
        //    {
        //        ddlEscolaridade.SelectedValue = ddlFormacaoAcademica.SelectedValue = "0";
        //        txtCoordenador.Text = String.Empty;
        //        ddlEscolaridade_SelectedIndexChanged(null, null);
        //    }
        //    this.Master.ScriptManagerControl.SetFocus(chkNaoPossuiCoordenador);
        //}

        //protected void rblEquipeVolante_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SessaoPmas.VerificarSessao(this);

        //    trEquipeVolante.Visible = rblEquipeVolante.SelectedValue == "1";
        //    if (trEquipeVolante.Visible)
        //    {
        //        this.Master.ScriptManagerControl.SetFocus(txtVolanteNivelMedio);
        //        return;
        //    }
        //    this.Master.ScriptManagerControl.SetFocus(rblEquipeVolante);
        //}

        //protected void rblServicoPAIF_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SessaoPmas.VerificarSessao(this);
        //    //se é um cras existente e a resposta tenha sido criado o PAIF deverá redirecionar para a tela de exclusão do serviço
        //    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
        //    {
        //        using (var proxy = new ProxyRedeProtecaoSocial())
        //        {
        //            var IdCras = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
        //            var ServicoPaif = proxy.Service.GetConsultaServicosRecursosFinanceirosByCRAS(IdCras).Where(s => s.IdTipoServico == 135).FirstOrDefault();
        //            if (ServicoPaif != null)
        //            {
        //                if (rblServicoPAIF.SelectedValue == "0")
        //                {
        //                    trJustificativaPAIF.Visible = false;
        //                    //recupero o ID do Serviço PAIF que pertence ao CRAS informado
        //                    var idCentro = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
        //                    var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
        //                    // deverá ser redirecionado para a tela de Motivo Exclusão do Serviço CRAS
        //                    if (ServicoPaif != null)
        //                        if (!ServicoPaif.Desativado)
        //                            Response.Redirect("~/BlocoIII/FMotivoExclusaoServicoCRAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(ServicoPaif.Id.ToString())) + "&idCentro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        //                }
        //            }
        //            else
        //            {
        //                trJustificativaPAIF.Visible = rblServicoPAIF.SelectedValue == "0";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        trJustificativaPAIF.Visible = rblServicoPAIF.SelectedValue == "0";
        //        if (!trJustificativaPAIF.Visible)
        //        {
        //            txtJustificativaPAIF.Text = string.Empty;
        //            this.Master.ScriptManagerControl.SetFocus(rblServicoPAIF);
        //            return;
        //        }
        //        this.Master.ScriptManagerControl.SetFocus(txtJustificativaPAIF);
        //    }
        //}

        //protected void ddlEscolaridade_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SessaoPmas.VerificarSessao(this);
        //    //Outra
        //    tdFormacaoAcademica.Visible = Convert.ToInt32(ddlEscolaridade.SelectedValue) == 4;
        //    if (!tdFormacaoAcademica.Visible)
        //    {
        //        this.Master.ScriptManagerControl.SetFocus(cep1.controleCEP);
        //        return;
        //    }
        //    this.Master.ScriptManagerControl.SetFocus(ddlFormacaoAcademica);
        //}
    }
}