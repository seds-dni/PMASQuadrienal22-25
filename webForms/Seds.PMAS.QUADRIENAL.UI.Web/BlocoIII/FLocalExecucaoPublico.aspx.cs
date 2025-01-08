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
    public partial class FLocalExecucaoPublico : System.Web.UI.Page
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

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    carregarUnidade(proxy);
                    load(proxy);
                }


                //txtNumeroAtendidos.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                //txtCapacidadeAtendimento.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

                #region Bloqueia , Desbloqueia
                WebControl[] controles = {                                                  
                                                 txtNomeLocalExecucao,
                                                 txtTecnicoResponsavel,
                                                 chkNaoPossuiTecnico,
                                                 //txtCapacidadeAtendimento, 
                                                 txtEmailInstitucional,
                                                 //txtNumeroAtendidos,                                                  
                                                 //txtNivelFundamental, 
                                                 //txtNivelMedio, 
                                                 //txtSuperior,                                                  
                                                 //txtEstagiarios, 
                                                 rblAvaliacaoLocalExecucao,
                                                 rblImovel,
                                                 //rblHorasSemana,
                                                 //rblDiasSemana,                                                                                                                                                   
                                                 //txtSemEscolaridade,
                                                 //txtSuperiorServicoSocial,
                                                 //txtSuperiorPsicologia,
                                                 //txtSuperiorPedagogia,
                                                 //txtSociologia,
                                                 //txtDireito,
                                                 //txtPosGraduacao,
                                                 //txtVoluntarios,                                                                                                                                                   
                                                 btnSalvar                                                 
                                         };
                Permissao.VerificarPermissaoControles(cep1.Controles, Session);
                Permissao.VerificarPermissaoControles(txtTelefone.Controles, Session);
                Permissao.VerificarPermissaoControles(txtCelular.Controles, Session);
                Permissao.VerificarPermissaoControles(controles, Session);
                txtNome.ReadOnly = true;
                txtCNPJ.controleCNPJ.ReadOnly = true;
                #endregion


            }
        }

        void verificarAlteracoes(Int32 idLocal)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro18.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 18, idLocal);
                    linkAlteracoesQuadro18.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("18")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal.ToString()));
                }
            }
        }

        void carregarUnidade(ProxyRedeProtecaoSocial proxy)
        {
            var unidade = proxy.Service.GetUnidadePublicaById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
            if (unidade == null)
                return;

            txtCNPJ.Text = unidade.CNPJ;
            txtNome.Text = unidade.RazaoSocial;

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
            var local = proxy.Service.GetLocalExecucaoPublicoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));

            var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosPublicoByLocalExecucao(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Where(p => p.IdUsuarioTipoServico == 39 || p.IdUsuarioTipoServico == 37 || p.IdUsuarioTipoServico == 41).ToList();

            if (local == null)
                return;

            verificarAlteracoes(local.Id);

            txtCNPJ.Enabled = false;
            txtNome.Enabled = false;

            txtNomeLocalExecucao.Text = local.Nome;

            chkNaoPossuiTecnico.Checked = !local.PossuiTecnicoResponsavel;
            txtTecnicoResponsavel.Text = local.TecnicoResponsavel;

            chkNaoPossuiTecnico_CheckedChanged(null, null);
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
                cep1.Txtcep = local.CEP;
                cep1.Txtendereco = local.Logradouro;
                cep1.Txtnumero = local.Numero;
                cep1.Txtcomplemento = local.Complemento;
                cep1.Txtbairro = local.Bairro;
                cep1.Txtcidade = local.Cidade;
                txtTelefone.Text = local.Telefone;
                txtCelular.Text = local.Celular;
                txtEmailInstitucional.Text = local.Email;

                ddlDistrito.SelectedValue = local.IdDistritoSaoPaulo.HasValue ? local.IdDistritoSaoPaulo.Value.ToString() : "0";
            }

            //txtCapacidadeAtendimento.Text = local.CapacidadeAtendimento.ToString();
            //txtNumeroAtendidos.Text = local.NumeroAtendidos.ToString();

            rblImovel.SelectedValue = local.IdTipoImovel.ToString();
            rblAvaliacaoLocalExecucao.SelectedValue = local.IdAvaliacaoLocalExecucao.HasValue ? local.IdAvaliacaoLocalExecucao.Value.ToString() : String.Empty;




        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            String msg = String.Empty;
            var CapacidadeAtendimento = 0M;
            //if (!String.IsNullOrEmpty(txtCapacidadeAtendimento.Text))
            //    if (Util.TryParseDecimal(txtCapacidadeAtendimento.Text).HasValue)
            //        CapacidadeAtendimento = Util.TryParseDecimal(txtCapacidadeAtendimento.Text).Value;

            //if (Request.QueryString["id"] != null)
            //{
            //    using (var proxy = new ProxyRedeProtecaoSocial())
            //    {
            //        var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosPublicoByLocalExecucao(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
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

            var local = new LocalExecucaoPublicoInfo();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                local.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            local.IdUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));

            local.Nome = txtNomeLocalExecucao.Text;

            local.PossuiTecnicoResponsavel = !chkNaoPossuiTecnico.Checked;
            if (local.PossuiTecnicoResponsavel)
                local.TecnicoResponsavel = txtTecnicoResponsavel.Text;

            local.Telefone = txtTelefone.Text.Trim();
            local.Celular = txtCelular.Text.Trim();
            local.Logradouro = cep1.Txtendereco;
            local.Numero = cep1.Txtnumero;
            local.Bairro = cep1.Txtbairro;
            local.Cidade = cep1.Txtcidade;
            local.CEP = cep1.Txtcep;
            local.Complemento = cep1.Txtcomplemento;
            local.Email = txtEmailInstitucional.Text;
            local.IdTipoImovel = Convert.ToInt16(rblImovel.SelectedValue);
            if (rblAvaliacaoLocalExecucao.SelectedValue != String.Empty)
                local.IdAvaliacaoLocalExecucao = Convert.ToInt32(rblAvaliacaoLocalExecucao.SelectedValue);

            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio == 565) //Mostrar os distritos para São Paulo
            {
                local.IdDistritoSaoPaulo = ddlDistrito.SelectedValue != "0" ? Convert.ToInt32(ddlDistrito.SelectedValue) : new Nullable<Int32>();
                if (!local.IdDistritoSaoPaulo.HasValue || local.IdDistritoSaoPaulo.Value == 0)
                    msg = "O preenchimento do campo Distrito é obrigatório";
            }

            if (msg == "")
            {
                String action = "LI";
                try
                {
                    new ValidadorLocalExecucaoPublico().Validar(local);

                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {
                        if (local.Id == 0)
                            proxy.Service.AddLocalExecucaoPublico(local);
                        else
                        {
                            action = "LA";
                            proxy.Service.UpdateLocalExecucaoPublico(local);
                        }
                    }

                }
                catch (Exception ex)
                {
                    msg += ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?msg=" + action + "&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(local.IdUnidade.ToString())));
                    return;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;

        }



        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }

        protected void chkNaoPossuiTecnico_CheckedChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            if (chkNaoPossuiTecnico.Checked)
                txtTecnicoResponsavel.Text = String.Empty;
            txtTecnicoResponsavel.Enabled = !chkNaoPossuiTecnico.Checked;
        }
    }
}