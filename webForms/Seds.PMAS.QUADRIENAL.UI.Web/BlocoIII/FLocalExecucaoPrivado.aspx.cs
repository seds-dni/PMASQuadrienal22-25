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
    public partial class FLocalExecucaoPrivado : System.Web.UI.Page
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
                    Response.Redirect("~/BlocoIII/CUnidadesPrivadas.aspx");
                    return;
                }

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    carregarUnidade(proxy);
                    load(proxy);
                }

                txtNumeroAtendidos.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

                #region Bloqueia , Desbloqueia
                WebControl[] controles = {                                                 
                                                 txtNomeLocalExecucao,
                                                 txtTecnicoResponsavel,
                                                 chkNaoPossuiTecnico,
                                                 txtEmailInstitucional,
                                                 txtNumeroAtendidos,                                                  
                                                 rblImovel,
                                                 rblAvaliacaoLocalExecucao,
                                                 btnSalvar
                                         };
                Permissao.VerificarPermissaoControles(cep1.Controles, Session);
                Permissao.VerificarPermissaoControles(txtTelefone.Controles, Session);
                Permissao.VerificarPermissaoControles(txtCelular.Controles, Session);
                Permissao.VerificarPermissaoControles(controles, Session);
                txtCNPJ.controleCNPJ.ReadOnly = true;
                txtNome.ReadOnly = true;
                #endregion
            }
        }

        void verificarAlteracoes(Int32 idLocal)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro38.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 38, idLocal);
                    linkAlteracoesQuadro38.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("38")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal.ToString()));
                }
            }
        }

        void carregarUnidade(ProxyRedeProtecaoSocial proxy)
        {
            var unidade = proxy.Service.GetUnidadePrivadaById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
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
            var local = proxy.Service.GetLocalExecucaoPrivadoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            if (local == null)
                return;

            var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosPrivadoByLocalExecucao(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Where(p => p.IdUsuarioTipoServico == 39 || p.IdUsuarioTipoServico == 37 || p.IdUsuarioTipoServico == 41).ToList();
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
            txtNumeroAtendidos.Text = local.NumeroAtendidos.ToString();
            rblImovel.SelectedValue = local.IdTipoImovel.ToString();
            txtEmailInstitucional.Text = local.Email;
            chkNaoPossuiTecnico.Checked = !local.PossuiTecnicoResponsavel;
            ddlDistrito.SelectedValue = local.IdDistritoSaoPaulo.HasValue ? local.IdDistritoSaoPaulo.Value.ToString() : "0";
            rblAvaliacaoLocalExecucao.SelectedValue = local.IdAvaliacaoLocalExecucao.HasValue ? local.IdAvaliacaoLocalExecucao.Value.ToString() : "0";
        }

        void carregarAvaliacoes()
        {
            rblAvaliacaoLocalExecucao.DataTextField = "Descricao";
            rblAvaliacaoLocalExecucao.DataValueField = "Id";
            rblAvaliacaoLocalExecucao.DataSource = new ProxyEstruturaAssistenciaSocial().Service.GetAvaliacoesLocal();
            rblAvaliacaoLocalExecucao.DataBind();

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            String msg = String.Empty;
            //if (Request.QueryString["id"] != null)
            //{
            //    using (var proxy = new ProxyRedeProtecaoSocial())
            //    {
            //        var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosPrivadoByLocalExecucao(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            //        foreach (var servico in servicos)
            //        {
            //            if (servico.PrevisaoAnualNumeroAtendidos > CapacidadeAtendimento)
            //            {
            //                msg += (msg != "" ? System.Environment.NewLine : "") + "Valor da Previsão anual do número de pessoas atendidas neste local de execução está menor que a Previsão Anual de Atendidos do  " + servico.Descricao;
            //            }
            //        }
            //    }
            //}


            var local = new LocalExecucaoPrivadoInfo();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                local.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            local.IdUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            //Welington P.
            //Tipo de Oferta de atendimento oferecido pelo local de Execução
            // 1 - Oferta de Serviço SocioAssistenciais 
            // 3 - Bom Prato ou Viva Leite
            //local.TipoOfertaAtendimento = 1;

            local.Nome = txtNomeLocalExecucao.Text;

            local.PossuiTecnicoResponsavel = !chkNaoPossuiTecnico.Checked;
            if (local.PossuiTecnicoResponsavel)
                local.TecnicoResponsavel = txtTecnicoResponsavel.Text;

            if (!String.IsNullOrEmpty(txtNumeroAtendidos.Text))
                local.NumeroAtendidos = Convert.ToInt32(txtNumeroAtendidos.Text);

            local.Telefone = txtTelefone.Text.Trim();
            local.Celular = txtCelular.Text.Trim();
            local.Logradouro = cep1.Txtendereco;
            local.Numero = cep1.Txtnumero;
            local.Cidade = cep1.Txtcidade;
            local.Bairro = cep1.Txtbairro;
            if (!String.IsNullOrEmpty(cep1.Txtcep))
                local.CEP = cep1.Txtcep;
            local.Complemento = cep1.Txtcomplemento;
            local.Email = txtEmailInstitucional.Text;
            local.IdTipoImovel = Convert.ToInt16(rblImovel.SelectedValue);

            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio == 565)
            {//Mostrar os distritos para São Paulo
                local.IdDistritoSaoPaulo = ddlDistrito.SelectedValue != "0" ? Convert.ToInt32(ddlDistrito.SelectedValue) : new Nullable<Int32>();
                if (!local.IdDistritoSaoPaulo.HasValue || local.IdDistritoSaoPaulo.Value == 0)
                    msg = "O preenchimento do campo Distrito é obrigatório";
            }

            if (!String.IsNullOrEmpty(rblAvaliacaoLocalExecucao.SelectedValue))
                local.IdAvaliacaoLocalExecucao = Convert.ToInt32(rblAvaliacaoLocalExecucao.SelectedValue);

            if (msg == "")
            {
                String action = "LI";
                try
                {
                    new ValidadorLocalExecucaoPrivado().Validar(local);

                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {
                        if (local.Id == 0)
                            proxy.Service.AddLocalExecucaoPrivado(local);
                        else
                        {
                            action = "LA";
                            proxy.Service.UpdateLocalExecucaoPrivado(local);
                        }
                    }

                }
                catch (Exception ex)
                {
                    msg += ex.Message;
                }
                if (String.IsNullOrEmpty(msg))
                {
                    Response.Redirect("~/BlocoIII/FUnidadePrivada.aspx?msg=" + action + "&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(local.IdUnidade.ToString())));
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
            Response.Redirect("~/BlocoIII/FUnidadePrivada.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));

        }

        protected void chkNaoPossuiTecnico_CheckedChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            if (chkNaoPossuiTecnico.Checked)
            {
                txtTecnicoResponsavel.Text = string.Empty;
                txtTecnicoResponsavel.Enabled = false;
            }
            else
                txtTecnicoResponsavel.Enabled = true;
        }

    }
}