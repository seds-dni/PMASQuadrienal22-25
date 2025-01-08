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
    public partial class VUnidadePrivada : System.Web.UI.Page
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



        void load()
        {
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
                return;

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
                if (!unidade.Desativado)
                    Response.Redirect("~/BlocoIII/CLocaisPrivadosDesativados.aspx?IdUnidade="+Request.QueryString["id"]);

                if (unidade == null)
                    return;
                carregarLocaisExecucao(proxy, id);
            }

            trCaracterizacaoEntidade.Visible = false;

            if (unidade.IdSituacaoInscricao == 1)
            {
                // ddlSituacaoInscricao.SelectedValue = unidade.IdSituacaoInscricao.ToString();
                txtDataPublicacao.Visible = true;
                txtDataPublicacao.Text = unidade.DataPublicacao.HasValue ? unidade.DataPublicacao.Value.ToShortDateString() : "";
                txtInscricaoCMAS.Text = unidade.InscricaoCMAS;
                txtInscricaoCMAS.Visible = true;

                // ddlSituacaoAtual.SelectedValue = unidade.IdSituacaoAtualInscricao.HasValue ? unidade.IdSituacaoAtualInscricao.ToString() : "0";
                ddlSituacaoAtual.Visible = true;
            }
            if (unidade.IdSituacaoInscricao == 2)
            {
                //ddlSituacaoInscricao.SelectedValue = unidade.IdSituacaoInscricao.ToString();
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
            var obj = new ProxyRedeProtecaoSocial().Service.GetMantenedoraByCNPJ(unidade.CNPJ);
            frmInfoOrganizacao.Visible = true;
            if (obj != null)
            {
                //btnEditar.Visible = SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0;
                txtNomeFantasia.Text = obj.NomeFantasia;
                ddlAreaAtuacao.Text = new ProxyRedeProtecaoSocial().Service.GetAreaAtuacao().Where(a => a.Exibe == true).FirstOrDefault().Descricao;
                lblSituacao.Text = obj.Situacao;
                lblMotivoSituacao.Text = obj.MotivoSituacao;
                lblRazaoSocial.Text = "Razão Social:";
                txtRazaoSocial.Text = obj.RazaoSocial;
                txtNomeFantasia.Text = obj.NomeFantasia;
                txtNomeFantasia.Text = obj.NomeFantasia;
                txtResponsavel.Text = obj.Responsavel;
                
                if (obj.CEP.Length == 8)
                {
                    lblCep.Text = obj.CEP.Substring(obj.CEP.Length -8, 8);
                }
                else
                {
                    lblCep.Text = obj.CEP.ToString();
                }

                if (obj.CEP.Length == 8)
                {
                    lblCep.Text = obj.CEP.Insert(5, "-");
                }
                else
                {
                    lblCep.Text = obj.CEP.Insert(5, "-000");
                }

                
                lblLogradouro.Text = obj.Endereco;
                lblNumero.Text = obj.Numero;
                lblComplemento.Text = obj.Complemento;
                lblBairro.Text = obj.Bairro;
                lblCidade.Text = obj.Municipio;

                if (!String.IsNullOrEmpty(obj.Telefone))
                {
                    string sDDD, sTelefone;
                    string sTelefoneCompleto = obj.Telefone;
                    sTelefoneCompleto = "0000000000" + sTelefoneCompleto;
                    sTelefoneCompleto = sTelefoneCompleto.Substring(sTelefoneCompleto.Length - 10, 10);
                    sDDD = sTelefoneCompleto.Substring(0, 2);
                    sTelefone = sTelefoneCompleto.Substring(2, 8);
                    sTelefone = sTelefone.Insert(4, "-");
                    lblTelefone.Text = "(" + sDDD + ") " + sTelefone;
                }

                if (!string.IsNullOrEmpty(obj.Celular))
                {
                    string sCelularCompleto = obj.Celular;
                    string sDDDCelular, sCelular = "";

                    sCelularCompleto = "00000000000" + sCelularCompleto;
                    sCelularCompleto = sCelularCompleto.Substring(sCelularCompleto.Length - 11, 11);

                    sDDDCelular = sCelularCompleto.Substring(0, 2);
                    sCelular = sCelularCompleto.Substring(2, 9);
                    sCelular = sCelular.Insert(5, "-");

                    lblCelular.Text = "(" + sDDDCelular + ") " + sCelular;
                }
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
                    // ddlSituacaoInscricao.SelectedValue = "1";
                    ddlSituacaoInscricao.Enabled = false;
                    txtDataPublicacao.Text = obj.DataPublicacao.Value.ToShortDateString();
                    lblSituacaoAtual.Visible = true;
                    ddlSituacaoAtual.Visible = true;
                }
                if (obj.IdAreaAtuacao.HasValue && obj.IdAreaAtuacao.Value == 2)
                    trUnidadeAtendimento.Visible = true;
                //Habilita / Desabilita Campos
                trSituacao.Visible = trDadosGerais.Visible = trDadosResponsavel.Visible = true;
                tdMotivo.Visible = obj.Situacao.ToLower() == "inativa";
            }
            else
            {
                ///Informações sobre a organização
                txtNomeFantasia.Text = unidade.NomeFantasia;
                ddlAreaAtuacao.Text = unidade.IdAreaAtuacao.HasValue ? new ProxyRedeProtecaoSocial().Service.GetAreaAtuacao().Where(a => a.Id == unidade.IdAreaAtuacao).FirstOrDefault().Descricao : String.Empty;
                lblCep.Text = unidade.CEP.Substring(unidade.CEP.Length - 8, 8);
                lblCep.Text = unidade.CEP.Insert(5, "-");
                lblLogradouro.Text = unidade.Logradouro;
                lblNumero.Text = unidade.Numero;
                lblComplemento.Text = unidade.Complemento;
                lblBairro.Text = unidade.Bairro;
                lblCidade.Text = unidade.Cidade;

                if (!String.IsNullOrEmpty(unidade.Telefone))
                {
                    string sDDD, sTelefone;
                    string sTelefoneCompleto = unidade.Telefone;
                    sTelefoneCompleto = "0000000000" + sTelefoneCompleto;
                    sTelefoneCompleto = sTelefoneCompleto.Substring(sTelefoneCompleto.Length - 10, 10);
                    sDDD = sTelefoneCompleto.Substring(0, 2);
                    sTelefone = sTelefoneCompleto.Substring(2, 8);
                    sTelefone = sTelefone.Insert(4, "-");
                    lblTelefone.Text = "(" + sDDD + ") " + sTelefone;
                }

                if (!string.IsNullOrEmpty(unidade.Celular))
                {
                    string sCelularCompleto = unidade.Celular;
                    string sDDDCelular, sCelular = "";

                    sCelularCompleto = "00000000000" + sCelularCompleto;
                    sCelularCompleto = sCelularCompleto.Substring(sCelularCompleto.Length - 11, 11);

                    sDDDCelular = sCelularCompleto.Substring(0, 2);
                    sCelular = sCelularCompleto.Substring(2, 9);
                    sCelular = sCelular.Insert(5, "-");

                    lblCelular.Text = "(" + sDDDCelular + ") " + sCelular;
                }
                txtEmail.Text = unidade.Email;
                txtResponsavel.Text = unidade.Responsavel;
                txtCargo.Text = unidade.Cargo;
                txtDataInicioMandato.Text = unidade.DataInicio.HasValue ? unidade.DataInicio.Value.ToShortDateString() : String.Empty; //: "-";
                txtDataTerminoMandato.Text = unidade.DataTermino.HasValue ? unidade.DataTermino.Value.ToShortDateString() : String.Empty; // "-";
                txtHomePage.Text = !String.IsNullOrEmpty(unidade.Site) ? unidade.Site.ToString() : "Não possui site";
                trSituacao.Visible = trDadosGerais.Visible = trDadosResponsavel.Visible = false;
            }
            trNome.Visible = true;
            trDadosResponsavel.Visible = trDadosGerais.Visible = trCaracterizacaoEntidade.Visible = trCMAS.Visible = true;
            ///Dados do responsável
            txtResponsavel.Text = !String.IsNullOrEmpty(unidade.Responsavel) ? unidade.Responsavel.ToString() : String.Empty; ;
            txtCargo.Text = !String.IsNullOrEmpty(unidade.Cargo) ? unidade.Cargo.ToString() : String.Empty;
            txtDataInicioMandato.Text = unidade.DataInicio.HasValue ? Convert.ToDateTime(unidade.DataInicio).ToShortDateString() : String.Empty;
            txtDataTerminoMandato.Text = unidade.DataTermino.HasValue ? Convert.ToDateTime(unidade.DataTermino).ToShortDateString() : String.Empty;


            ///Caracterização da entidade
            if (unidade.FormasAtuacoes != null && unidade.FormasAtuacoes.Count > 0)
            {
                foreach (var i in unidade.FormasAtuacoes)
                {

                    if (i.Id == 1)
                    {
                        trAtendimento.Visible = trUnidadeAtendimento.Visible = i.Id == 1;
                        trUnidadeAtendimento.Attributes.Add("class", "row bg-cyan fg-black padding10 no-margin-bottom");
                        if (unidade.CaracterizacaoAtividades != null && unidade.CaracterizacaoAtividades.Count > 0)
                            foreach (var item in unidade.CaracterizacaoAtividades.Where(c => c.IdFormaAtuacao == 1))
                                chkServicosSocioassistencial.Text += item.Nome + "<br/>";
                    }
                    if (i.Id == 2)
                    {
                        trAssessoramento.Visible = trCaracterizacaoAtividades.Visible = trPublicoAlvo.Visible = i.Id == 2;
                        trAssessoramento.Attributes.Add("class", "row bg-lighterBlue fg-black no-margin-bottom");
                        trCaracterizacaoAtividades.Attributes.Add("class", "row bg-lighterBlue fg-black padding10 no-margin-bottom");
                        trPublicoAlvo.Attributes.Add("class", "row bg-lighterBlue fg-black padding10 no-margin-bottom");

                        if (unidade.CaracterizacaoAtividades != null && unidade.CaracterizacaoAtividades.Count > 0)
                            foreach (var item in unidade.CaracterizacaoAtividades.Where(c => c.IdFormaAtuacao == 2))
                                chkCaracterizacaoAtividades.Text = item.Nome;

                        if (unidade.PublicoAlvos != null && unidade.PublicoAlvos.Count > 0)
                            foreach (var item in unidade.PublicoAlvos.Where(c => c.IdFormaAtuacao == 2))
                                chkPublicoAlvo.Text += item.Nome + "<br/>";
                    }
                    if (i.Id == 3)
                    {
                        trDefesaDireitos.Visible = trPublicoAlvoDefesa.Visible = trCaracterizacaoDefesa.Visible = i.Id == 3;
                        trDefesaDireitos.Attributes.Add("class", "row bg-lightBlue fg-black no-margin-bottom");
                        trCaracterizacaoDefesa.Attributes.Add("class", "row bg-lightBlue fg-black padding10 no-margin-bottom");
                        trPublicoAlvoDefesa.Attributes.Add("class", "row bg-lightBlue fg-black padding10 no-margin-bottom");
                        if (unidade.CaracterizacaoAtividades != null && unidade.CaracterizacaoAtividades.Count > 0)  
                            foreach (var item in unidade.CaracterizacaoAtividades.Where(c => c.IdFormaAtuacao == 3))
                                chkCaracterizacaoAtividadesDefesa.Text += item.Nome + "</br>";

                        if (unidade.PublicoAlvos != null && unidade.PublicoAlvos.Count > 0)
                            foreach (var item in unidade.PublicoAlvos.Where(c => c.IdFormaAtuacao == 3))
                                chkPublicoAlvoDefesa.Text += item.Nome + "</br>";
                    }
                    trSedeAdministrativa.Visible = i.Id == 4;
                }
            }
            ////Inscrição no CMAS
            ddlSituacaoInscricao.Text = unidade.IdSituacaoInscricao.HasValue ? new ProxyRedeProtecaoSocial().Service.GetSituacaoAtualInscricao().Where(s => s.Id == unidade.IdSituacaoInscricao).SingleOrDefault().Nome : "Nenhuma opção foi informada";
            txtInscricaoCMAS.Text = !String.IsNullOrWhiteSpace(unidade.InscricaoCMAS) ? unidade.InscricaoCMAS : "";
            txtDataPublicacao.Text = unidade.DataPublicacao.HasValue ? unidade.DataPublicacao.Value.ToShortDateString() : "";
            if (unidade.IdSituacaoAtualInscricao.HasValue)
                ddlSituacaoAtual.Text = unidade.IdSituacaoAtualInscricao.HasValue ? new ProxyRedeProtecaoSocial().Service.GetSituacaoAtualInscricao().Where(s => s.Id == unidade.IdSituacaoAtualInscricao).SingleOrDefault().Nome : "Nenhuma opção foi informada";

            //Informações da Organização desativada
            lblDataDesativacao.Text = unidade.DataRegistroLog.HasValue ? unidade.DataRegistroLog.Value.ToShortDateString() : string.Empty;
            lblMotivoDesativacao.Text = unidade.IdMotivoDesativacao.HasValue ? new ProxyRedeProtecaoSocial().Service.GetMotivoDesativacaoLocalById(unidade.IdMotivoDesativacao.Value).Descricao : String.Empty;
            if (unidade.IdMotivoDesativacao.HasValue && unidade.IdMotivoDesativacao.Value != 1)
            {
                if (unidade.IdMotivoDesativacao.Value == 2)
                {
                    trDataEncerramento.Visible = trDetalhamento.Visible = trMotivoEncerramento.Visible = true;
                    lblEncerramento.Text = new ProxyRedeProtecaoSocial().Service.GetMotivoDesativacaoLocalById(unidade.IdMotivoEncerramento.Value).Descricao;
                    lblDescricaDataEncerramento.Text = "Data do encerramento do termo de convênio, parceria ou fomento:";
                    lblDescricaoDetalhamento.Text = "Detalhamento sobre o motivo do encerramento termo de convênio, parceria ou fomento:";

                }
                else
                {
                    trDataEncerramento.Visible = trDetalhamento.Visible = true;
                    trMotivoEncerramento.Visible = false;
                    lblDescricaDataEncerramento.Text = "Data do encerramento das atividades da organização:";
                    lblDescricaoDetalhamento.Text = "Detalhamento sobre o motivo do encerramento das atividades da organização:";
                }
                lblDetalhamento.Text = unidade.Detalhamento;
                lblDataEncerramento.Text = unidade.DataDesativacao.HasValue ?unidade.DataDesativacao.Value.ToShortDateString() : "";
            }
        }


        void carregarLocaisExecucao(ProxyRedeProtecaoSocial proxy, Int32 idUnidade)
        {
            var LEP = proxy.Service.GetIdentificacaoLocalExecucaoPrivadoByUnidade(idUnidade).Where(c => c.Desativado == true);

            var localExecucaoPrivado = LEP.GroupBy(x => x.Id).Select(y => new
            {
                Id = y.First().Id
                ,
                IdUnidade = y.First().IdUnidade
                ,
                Nome = y.First().Nome
                ,
                Responsavel = y.First().Responsavel
                ,
                TotalServicos = y.First().TotalServicos
                ,
                TotalServicosDesativados = y.First().TotalServicosDesativados
                ,
                PrevisaoOrcamentaria = y.First().PrevisaoOrcamentaria
                ,
                ValorCofinanciamentoEstadual = y.First().ValorCofinanciamentoEstadual
                ,
                Desativado = y.First().Desativado
                ,
                IdMotivoDesativacao = y.First().IdMotivoDesativacao
                ,
                Descricao = y.First().Descricao
                ,
                DataEncerramento = y.First().DataEncerramento
                ,
                DetalhamentoEncerramento = y.First().DetalhamentoEncerramento
            });


            lstLocais.DataSource = localExecucaoPrivado;
            lstLocais.DataBind();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CUnidadesPrivadasDesativadas.aspx");
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
                    Response.Redirect("~/BlocoIII/VLocalExecucaoPrivado.aspx?id=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPrivadoDesativado.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
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
            }
        }

    }
}