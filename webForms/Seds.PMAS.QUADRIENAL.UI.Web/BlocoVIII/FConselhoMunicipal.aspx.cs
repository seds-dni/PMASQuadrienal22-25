using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoVIII
{
    public partial class FConselhoMunicipal : System.Web.UI.Page
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
                carregarUsuarios();
                using (var proxy = new ProxyPrefeitura())
                {
                    load(new Prefeituras(proxy));
                }
                #region Bloqueia , Desbloqueia

                WebControl[] controles = {  
                                             txtNumeroLeiCriacao,
                                             txtAnoLeiCriacao,
                                             rblAlteracaoLei,
                                             txtNumeroLei,
                                             txtNumeroRepresentanteSociedadeCivil,
                                             txtNumeroRepresentateGovernamental,
                                             txtSuperiorServicoSocial,
                                             txtSuperiorAdministracao,
                                             txtSuperiorPsicologia,
                                             txtSuperiorAntropologia,
                                             txtSuperiorPedagogia,
                                             txtSuperiorContabilidade,
                                             txtSociologia,
                                             txtSuperiorEconomia,
                                             txtDireito,
                                             txtSuperiorTerapiaOcupacional,
                                             txtSuperiorEconomiaDomestica,
                                             txtMusicoterapia,
                                             rblSecretariaExecutiva,
                                             txtEmail, 
                                             txtTecnicoSecretariaExecutiva,
                                             txtAdministrativoSecretariaExecutiva,
                                             btnSalvar,
                                             txtUsuarios,
                                             txtTrabalhadores,
                                             txtEntidades,
                                             txtNomeSecretario
                                             };

                Permissao.BlocoVIII.VerificarPermissaoCMAS(controles);
                Permissao.BlocoVIII.VerificarPermissaoCMAS(txtDtCriacao.Controles);
                Permissao.BlocoVIII.VerificarPermissaoCMAS(txtDataAlteracao.Controles);
                Permissao.BlocoVIII.VerificarPermissaoCMAS(txtCelular.Controles);
                Permissao.BlocoVIII.VerificarPermissaoCMAS(txtTelefone.Controles);
                Permissao.BlocoVIII.VerificarPermissaoCMAS(cep1.Controles);
                #endregion

                verificarAlteracoes();
            }
        }

        void carregarUsuarios()
        {
            Int32? idMunicipio = SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio;
            Int32? idPerfil = (Int32)EPerfil.CMAS;
            using (var proxy = new ProxyUsuarioPMAS())
            {
                ddlUsuario.DataTextField = "Nome";
                ddlUsuario.DataValueField = "IdUsuario";
                ddlUsuario.DataSource = new Usuarios().GetConsultaUsuariosCadastrados("", "", null, idPerfil, idMunicipio, "", proxy).OrderBy(u => u.Nome);
                ddlUsuario.DataBind();
                ddlUsuario.Items.Insert(0, new ListItem("[Indique o nome do presidente]", "0"));
            }
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    alteracoesQuadro.Visible = linkAlteracoesQuadro72.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 72);
                    linkAlteracoesQuadro72.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("72"));
                }
            }
        }

        void load(Prefeituras prefeituras)
        {
            ConselhoMunicipalInfo obj = prefeituras.GetConselhoMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj != null)
            {
                hdfId.Value = obj.Id.ToString();

                cep1.Txtcep = obj.CEP;
                cep1.Txtendereco = obj.Logradouro;
                cep1.Txtnumero = obj.Numero;
                cep1.Txtcomplemento = obj.Complemento;
                cep1.Txtbairro = obj.Bairro;
                cep1.Txtcidade = obj.Cidade;
                txtTelefone.Text = obj.Telefone;
                txtCelular.Text = obj.Celular;

                if (!String.IsNullOrEmpty(obj.NumeroLei))
                {
                    txtNumeroLeiCriacao.Text = obj.NumeroLei.Split('/')[0];
                    txtAnoLeiCriacao.Text = obj.NumeroLei.Split('/')[1];
                }

                txtEmail.Text = obj.Email;
                txtNumeroRepresentateGovernamental.Text = obj.NumeroRepresentanteGovernamentais.ToString();
                txtNumeroRepresentanteSociedadeCivil.Text = obj.NumeroRepresentanteSociedadeCivil.ToString();
                txtEntidades.Text = obj.NumeroEntidades.ToString();
                txtTrabalhadores.Text = obj.NumeroTrabalhadores.ToString();
                txtUsuarios.Text = obj.NumeroUsuarios.ToString();

                var possuiSecretaria = obj.PossuiSecretariaExecutivaEstruturada.HasValue ? Convert.ToByte(obj.PossuiSecretariaExecutivaEstruturada).ToString() : "0";

                if (possuiSecretaria == "1")
                {

                    if (!String.IsNullOrEmpty(obj.NomeSecretarioExecutivo))
                    {
                        txtNomeSecretario.Text = obj.NomeSecretarioExecutivo.ToString();
                    }
                    else
                    {
                        txtNomeSecretario.Text = "";
                    }
                    
                }
                else
                {
                    txtNomeSecretario.Text = "";
                }


                txtSuperiorServicoSocial.Text = obj.ComposicaoServicoSocial.ToString();
                txtSuperiorAdministracao.Text = obj.ComposicaoAdministracao.ToString();
                txtSuperiorPsicologia.Text = obj.ComposicaoPsicologia.ToString();
                txtSuperiorAntropologia.Text = obj.ComposicaoAntropologia.ToString();
                txtSuperiorPedagogia.Text  = obj.ComposicaoPedagogia.ToString();
                txtSuperiorContabilidade.Text = obj.ComposicaoContabilidade.ToString();
                txtSociologia.Text = obj.ComposicaoSociologia.ToString();
                txtSuperiorEconomia.Text = obj.ComposicaoEconomia.ToString();
                txtDireito.Text = obj.ComposicaoDireito.ToString();
                txtSuperiorTerapiaOcupacional.Text = obj.ComposicaoTerapiaOcupacional.ToString();
                txtSuperiorEconomiaDomestica.Text = obj.ComposicaoEconomiaDomestica.ToString();
                txtMusicoterapia.Text = obj.ComposicaoMusicoterapia.ToString();

                rblAlteracaoLei.SelectedValue = Convert.ToByte(obj.AlteracaoNaLei).ToString();
                if (obj.AlteracaoNaLei.HasValue && obj.AlteracaoNaLei.Value)
                {
                    txtNumeroLei.Text = obj.NumeroLeiAlterada;
                    if (obj.DataLeiAlterada.HasValue)
                        txtDataAlteracao.Text = obj.DataLeiAlterada.Value.ToShortDateString();
                }

                rblSecretariaExecutiva.SelectedValue = obj.PossuiSecretariaExecutivaEstruturada.HasValue ? Convert.ToByte(obj.PossuiSecretariaExecutivaEstruturada).ToString() : "0";
                rblSecretariaExecutiva_SelectedIndexChanged(null, null);
                if (obj.PossuiSecretariaExecutivaEstruturada.HasValue && obj.PossuiSecretariaExecutivaEstruturada.Value)
                {
                    txtTecnicoSecretariaExecutiva.Text = obj.TotalFuncionariosTecnicoSecretariaExecutiva.ToString();
                    txtAdministrativoSecretariaExecutiva.Text = obj.TotalFuncionariosAdministrativoSecretariaExecutiva.ToString();
                }

                //Presidente
                ddlUsuario.SelectedValue = obj.IdUsuarioPresidente.ToString();
                if (ddlUsuario.SelectedValue != "0")
                    BloquearCampos();
                else
                    DesbloquearCampos();

                if (!String.IsNullOrEmpty(obj.RG))
                {
                    txtRG.Txtrg = obj.RG;
                    txtRG.Txtdigito = obj.RGDigito;
                }
                txtDataEmissao.Text = obj.DataEmissao.HasValue ? obj.DataEmissao.Value.ToShortDateString() : String.Empty;
                ddlUFEmissor.SelectedValue = obj.IdUf.HasValue ? obj.IdUf.Value.ToString() : "0";

                if (!String.IsNullOrEmpty(obj.CPF))
                    txtCPF.Text = obj.CPF;

                if (!String.IsNullOrEmpty(obj.SiglaEmissor))
                    txtOrgEmissor.Text = obj.SiglaEmissor;

                if (!String.IsNullOrEmpty(obj.TelefonePresidente))
                    txtTelefonePresidenteCMAS.Text = obj.TelefonePresidente;

                if (!String.IsNullOrEmpty(obj.CelularPresidente))
                    txtCelularPresidenteCMAS.Text = obj.CelularPresidente;

                if (!String.IsNullOrEmpty(obj.EmailPresidente))
                    txtEmailPresidenteCMAS.Text = obj.EmailPresidente;

                txtNumeroDecreto.Text = obj.NumeroDecreto;
                txtDataDecreto.Text = obj.DataDecreto.HasValue ? obj.DataDecreto.Value.ToShortDateString() : "";

                txtDtCriacao.Text = obj.DataLei.ToShortDateString();


                txtDataInicio.Text = obj.DataMandatoInicio.HasValue ? obj.DataMandatoInicio.Value.ToShortDateString() : "";
                txtDataTermino.Text = obj.DataMandatoTerminio.HasValue ? obj.DataMandatoTerminio.Value.ToShortDateString() : "";


                carregarPresidentesAnteriores(prefeituras, obj);


            }
            else
            {
                DesbloquearCampos();
            }
            rblAlteracaoLei_SelectedIndexChanged(null, null);
        }

        void carregarPresidentesAnteriores(Prefeituras prefeituras, ConselhoMunicipalInfo conselho)
        {
            var usuarios = new Usuarios();
            var usuario = new UsuarioPMASInfo();
            var proxy = new ProxyUsuarioPMAS();

            var lst = prefeituras.GetPresidentesAnteriores(conselho.Id);
            lst.ForEach(c =>
            {
                usuario = usuarios.GetUsuarioById(c.IdUsuarioPresidente, proxy);
                c.Nome = usuario != null ? usuario.Nome : "";
            });
            lstPresidentesAnteriores.DataSource = lst;
            lstPresidentesAnteriores.DataBind();
        }

        protected void rblAlteracaoLei_SelectedIndexChanged(object sender, EventArgs e)
        {
            tdDataLeiAlterada.Visible = tdLeiAlterada.Visible = rblAlteracaoLei.SelectedValue == "1";
            if (rblAlteracaoLei.SelectedValue == "0")
            {
                txtNumeroLei.Text = string.Empty;
                txtDataAlteracao.Text = string.Empty;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var obj = new ConselhoMunicipalInfo();
            obj.Id = Convert.ToInt32(hdfId.Value);
            obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            obj.Telefone = txtTelefone.Text.Trim();
            obj.Celular = txtCelular.Text.Trim();
            obj.Logradouro = cep1.Txtendereco;
            obj.Numero = cep1.Txtnumero;
            obj.Bairro = cep1.Txtbairro;
            obj.Cidade = cep1.Txtcidade;
            obj.CEP = cep1.Txtcep;
            obj.Complemento = cep1.Txtcomplemento;
            obj.Email = txtEmail.Text;

            if (!String.IsNullOrEmpty(txtNumeroLeiCriacao.Text) && !String.IsNullOrEmpty(txtAnoLeiCriacao.Text))
                obj.NumeroLei = txtNumeroLeiCriacao.Text + "/" + txtAnoLeiCriacao.Text;

            DateTime dt;
            if (!String.IsNullOrEmpty(txtDtCriacao.Text) && DateTime.TryParse(txtDtCriacao.Text, out dt))
                obj.DataLei = Convert.ToDateTime(txtDtCriacao.Text);

            obj.AlteracaoNaLei = Convert.ToBoolean(Convert.ToInt32(rblAlteracaoLei.SelectedValue));

            if (obj.AlteracaoNaLei.Value)
            {
                obj.NumeroLeiAlterada = txtNumeroLei.Text;
                if (DateTime.TryParse(txtDataAlteracao.Text, out dt))
                    obj.DataLeiAlterada = Convert.ToDateTime(txtDataAlteracao.Text);
            }

            if (!String.IsNullOrEmpty(txtNumeroRepresentateGovernamental.Text))
                obj.NumeroRepresentanteGovernamentais = Convert.ToInt32(txtNumeroRepresentateGovernamental.Text);
            
            if (!String.IsNullOrEmpty(txtNumeroRepresentanteSociedadeCivil.Text))
                obj.NumeroRepresentanteSociedadeCivil = Convert.ToInt32(txtNumeroRepresentanteSociedadeCivil.Text);

            if (!String.IsNullOrEmpty(txtTrabalhadores.Text))
                obj.NumeroTrabalhadores = Convert.ToInt32(txtTrabalhadores.Text);

            if (!String.IsNullOrEmpty(txtNomeSecretario.Text))
                obj.NomeSecretarioExecutivo = txtNomeSecretario.Text;

            if (!String.IsNullOrEmpty(txtUsuarios.Text))
                obj.NumeroUsuarios = Convert.ToInt32(txtUsuarios.Text);

            if (!String.IsNullOrEmpty(txtEntidades.Text))
                obj.NumeroEntidades = Convert.ToInt32(txtEntidades.Text);

            if (!String.IsNullOrEmpty(txtSuperiorServicoSocial.Text))
                obj.ComposicaoServicoSocial = Convert.ToInt32(txtSuperiorServicoSocial.Text);

            if (!String.IsNullOrEmpty(txtSuperiorPsicologia.Text))
                obj.ComposicaoPsicologia = Convert.ToInt32(txtSuperiorPsicologia.Text);

            if (!String.IsNullOrEmpty(txtSuperiorPedagogia.Text))
                obj.ComposicaoPedagogia = Convert.ToInt32(txtSuperiorPedagogia.Text);

            if (!String.IsNullOrEmpty(txtSociologia.Text))
                obj.ComposicaoSociologia = Convert.ToInt32(txtSociologia.Text);

            if (!String.IsNullOrEmpty(txtDireito.Text))
                obj.ComposicaoDireito = Convert.ToInt32(txtDireito.Text);

            if (!String.IsNullOrEmpty(txtSuperiorEconomiaDomestica.Text))
                obj.ComposicaoEconomiaDomestica = Convert.ToInt32(txtSuperiorEconomiaDomestica.Text);

            if (!String.IsNullOrEmpty(txtSuperiorAdministracao.Text))
                obj.ComposicaoAdministracao = Convert.ToInt32(txtSuperiorAdministracao.Text);

            if (!String.IsNullOrEmpty(txtSuperiorAntropologia.Text))
                obj.ComposicaoAntropologia = Convert.ToInt32(txtSuperiorAntropologia.Text);
            
            if (!String.IsNullOrEmpty(txtSuperiorContabilidade.Text))
                obj.ComposicaoContabilidade = Convert.ToInt32(txtSuperiorContabilidade.Text);
            
            if (!String.IsNullOrEmpty(txtSuperiorEconomia.Text))
                obj.ComposicaoEconomia = Convert.ToInt32(txtSuperiorEconomia.Text);
            
            if (!String.IsNullOrEmpty(txtSuperiorTerapiaOcupacional.Text))
                obj.ComposicaoTerapiaOcupacional = Convert.ToInt32(txtSuperiorTerapiaOcupacional.Text);
            
            if (!String.IsNullOrEmpty(txtMusicoterapia.Text))
                obj.ComposicaoMusicoterapia = Convert.ToInt32(txtMusicoterapia.Text);

            //PRESIDENTE
            if (ddlUsuario.SelectedIndex > 0)
                obj.IdUsuarioPresidente = Convert.ToInt32(ddlUsuario.SelectedValue);


            if (!String.IsNullOrEmpty(txtCPF.Text))
                obj.CPF = txtCPF.Text;

            if (!String.IsNullOrEmpty(txtRG.Txtrg) || !String.IsNullOrEmpty(txtRG.Txtdigito))
            {
                obj.RG = txtRG.Txtrg;
                obj.RGDigito = txtRG.Txtdigito;
            }
            if (!String.IsNullOrEmpty(txtDataEmissao.Text) && DateTime.TryParse(txtDataEmissao.Text, out dt))
                obj.DataEmissao = Convert.ToDateTime(txtDataEmissao.Text);

            if (!String.IsNullOrEmpty(txtOrgEmissor.Text))
                obj.SiglaEmissor = txtOrgEmissor.Text;

            if (ddlUFEmissor.SelectedValue != "0")
                obj.IdUf = Convert.ToInt16(ddlUFEmissor.SelectedValue);

            if (!String.IsNullOrEmpty(txtTelefonePresidenteCMAS.Text))
                obj.TelefonePresidente = txtTelefonePresidenteCMAS.Text;

            if (!String.IsNullOrEmpty(txtCelularPresidenteCMAS.Text))
                obj.CelularPresidente = txtCelularPresidenteCMAS.Text;

            if (!String.IsNullOrEmpty(txtEmailPresidenteCMAS.Text))
                obj.EmailPresidente = txtEmailPresidenteCMAS.Text;

            if (!String.IsNullOrEmpty(txtNumeroDecreto.Text))
                obj.NumeroDecreto = txtNumeroDecreto.Text;

            if (!String.IsNullOrEmpty(txtDataDecreto.Text) && DateTime.TryParse(txtDataDecreto.Text, out dt))
                obj.DataDecreto = Convert.ToDateTime(txtDataDecreto.Text);

            if (!String.IsNullOrEmpty(txtDataInicio.Text) && DateTime.TryParse(txtDataInicio.Text, out dt))
                obj.DataMandatoInicio = Convert.ToDateTime(txtDataInicio.Text);
            
            if (!String.IsNullOrEmpty(txtDataTermino.Text) && DateTime.TryParse(txtDataTermino.Text, out dt))
                obj.DataMandatoTerminio = Convert.ToDateTime(txtDataTermino.Text);

            obj.PossuiSecretariaExecutivaEstruturada = Convert.ToBoolean(Convert.ToInt32(rblSecretariaExecutiva.SelectedValue));
            if (obj.PossuiSecretariaExecutivaEstruturada.HasValue && obj.PossuiSecretariaExecutivaEstruturada.Value)
            {
                if (!String.IsNullOrEmpty(txtTecnicoSecretariaExecutiva.Text))
                    obj.TotalFuncionariosTecnicoSecretariaExecutiva = Convert.ToInt32(txtTecnicoSecretariaExecutiva.Text);

                if (!String.IsNullOrEmpty(txtAdministrativoSecretariaExecutiva.Text))
                    obj.TotalFuncionariosAdministrativoSecretariaExecutiva = Convert.ToInt32(txtAdministrativoSecretariaExecutiva.Text);
            }

            String msg = String.Empty;
            try
            {
                //if (String.IsNullOrEmpty(txtDataDecreto.Text) || !DateTime.TryParse(txtDataDecreto.Text, out dt))
                //    msg += "Data de publicação do decreto é obrigatória" + System.Environment.NewLine;

                if (msg == "")
                {
                    new ValidadorConselhoMunicipal().Validar(obj);
                    using (var proxy = new ProxyPrefeitura())
                    {
                        var prefeituras = new Prefeituras(proxy);

                        var presidentesAnteriores = proxy.Service.GetPresidentesAnterioresByConselhoMunicipal(obj.Id);

                        foreach (var pres in presidentesAnteriores)
                        {
                            if (pres.DataInicioMandato > Convert.ToDateTime(txtDataInicio.Text) ||
                                pres.DataTerminoMandato > Convert.ToDateTime(txtDataInicio.Text))
                            {
                                throw new Exception("O período de mandato do presidente anterior não pode ser superior ao período de mandato do presidente atual!");
                            }
                        }

                        prefeituras.SaveConselhoMunicipal(obj);
                        load(prefeituras);
                    }
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = obj.Id == 0 ? "Conselho Municipal registrado com sucesso!" : "Conselho Municipal atualizado com sucesso!";
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

        protected void rblSecretariaExecutiva_SelectedIndexChanged(object sender, EventArgs e)
        {
            trAdministrativoSecretariaExecutiva.Visible = trTecnicoSecretariaExecutiva.Visible = rblSecretariaExecutiva.SelectedValue == "1";
            txtTecnicoSecretariaExecutiva.Text = txtAdministrativoSecretariaExecutiva.Text = String.Empty;

            foreach (ListItem item in rblSecretariaExecutiva.Items)
            {
                if (item.Value == "1" && item.Selected)
                {
                    trNomeSecretario.Visible = true;
                }
                else if (item.Value == "0" && item.Selected)
                {
                    trNomeSecretario.Visible = false;
                }
            }

        }

        protected void lstPresidentesAnteriores_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstPresidentesAnteriores.DataKeys[e.Item.DataItemIndex];
            try
            {
                switch (e.CommandName)
                {
                    case "Excluir_Presidente":
                        using (var proxy = new ProxyPrefeitura())
                        {
                            var conselho = proxy.Service.GetConselhoMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                            var prefeituras = new Prefeituras(proxy);
                            var presidenteAnterior = prefeituras.GetPresidentesAnteriores(conselho.Id).SingleOrDefault(c => c.Id == Convert.ToInt32(key["Id"]));
                            prefeituras.DeletePresidenteAnteriorConselhoMunicipal(presidenteAnterior);
                            carregarPresidentesAnteriores(prefeituras, conselho);
                            var script = Util.GetJavaScriptDialogOK("Presidente do CMAS removido com sucesso!");
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

        protected void lstPresidentesAnteriores_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;

                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();

                bool habilitadoControlesPrefeito = (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS) && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS);

                if (habilitadoControlesPrefeito)
                {
                    ImageButton controle = (ImageButton)e.Item.FindControl("btnExcluir");
                    controle.Enabled = controle != null;
                }
                else 
                {
                    ImageButton controle = (ImageButton)e.Item.FindControl("btnExcluir");
                    controle.Enabled = !(controle != null) ;
                }
            }
        }

        protected void btnSubstituir_Click(object sender, EventArgs e)  
        {
            SessaoPmas.VerificarSessao(this);

            String msg = String.Empty;
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);

                    var conselhoMunicipal = proxy.Service.GetConselhoMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                    if (conselhoMunicipal.IdUsuarioPresidente == 0 || String.IsNullOrWhiteSpace(conselhoMunicipal.NumeroDecreto) || !conselhoMunicipal.DataDecreto.HasValue
                        || !conselhoMunicipal.DataMandatoInicio.HasValue || !conselhoMunicipal.DataMandatoTerminio.HasValue)
                    {
                        throw new Exception("Os campos do presidente atual não estão preenchidos!");
                    }

                    var presidenteAnterior = new ConselhoMunicipalPresidenteAnteriorInfo();
                    presidenteAnterior.IdConselhoMunicipal = conselhoMunicipal.Id;
                    presidenteAnterior.IdUsuarioPresidente = Convert.ToInt32(ddlUsuario.SelectedValue);
                    presidenteAnterior.DataInicioMandato = Convert.ToDateTime(txtDataInicio.Text);
                    presidenteAnterior.DataTerminoMandato = Convert.ToDateTime(txtDataTermino.Text);
                    presidenteAnterior.RG = txtRG.Txtrg.ToString();
                    presidenteAnterior.RGDigito = txtRG.Txtdigito.ToString();
                    presidenteAnterior.CPF = txtCPF.Text.ToString();
                    presidenteAnterior.DataEmissao = Convert.ToDateTime(txtDataEmissao.Text);
                    presidenteAnterior.SiglaEmissor = txtOrgEmissor.Text;
                    presidenteAnterior.IdUF = Convert.ToInt16(ddlUFEmissor.Text);


                    prefeituras.SavePresidenteAnteriorConselhoMunicipal(presidenteAnterior);
                    carregarPresidentesAnteriores(prefeituras, conselhoMunicipal);

                    conselhoMunicipal.IdUsuarioPresidente = 0;
                    conselhoMunicipal.CPF = null;
                    conselhoMunicipal.RG = null;
                    conselhoMunicipal.RGDigito = null;
                    conselhoMunicipal.DataEmissao = null;
                    conselhoMunicipal.SiglaEmissor = null;
                    conselhoMunicipal.IdUf = 0;
                    conselhoMunicipal.EmailPresidente = null;
                    conselhoMunicipal.TelefonePresidente = null;
                    conselhoMunicipal.CelularPresidente = null;
                    conselhoMunicipal.NumeroDecreto = null;
                    conselhoMunicipal.DataDecreto = null;
                    conselhoMunicipal.DataMandatoInicio = null;
                    conselhoMunicipal.DataMandatoTerminio = null;
                    prefeituras.SaveConselhoMunicipal(conselhoMunicipal, true);

                    DesbloquearCampos();
                }
            }

            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
            lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
            tbInconsistencias.Visible = true;
        }

        protected void btnSalvarPresidente_Click(object sender, EventArgs e)
        {
            frmPresidenteCMAS.Attributes.Add("class", "frame active");
            SessaoPmas.VerificarSessao(this);
            if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS)
            {
                var obj = new ConselhoMunicipalInfo();
                obj.Id = Convert.ToInt32(hdfId.Value);
                obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                obj.Telefone = txtTelefone.Text.Trim();
                obj.Celular = txtCelular.Text.Trim();
                obj.Logradouro = cep1.Txtendereco;
                obj.Numero = cep1.Txtnumero;
                obj.Bairro = cep1.Txtbairro;
                obj.Cidade = cep1.Txtcidade;
                obj.CEP = cep1.Txtcep;
                obj.Complemento = cep1.Txtcomplemento;
                obj.Email = txtEmail.Text;

                if (!String.IsNullOrEmpty(txtNumeroLeiCriacao.Text) && !String.IsNullOrEmpty(txtAnoLeiCriacao.Text))
                    obj.NumeroLei = txtNumeroLeiCriacao.Text + "/" + txtAnoLeiCriacao.Text;

                DateTime dt;
                if (!String.IsNullOrEmpty(txtDtCriacao.Text) && DateTime.TryParse(txtDtCriacao.Text, out dt))
                    obj.DataLei = Convert.ToDateTime(txtDtCriacao.Text);

                obj.AlteracaoNaLei = Convert.ToBoolean(Convert.ToInt32(rblAlteracaoLei.SelectedValue));

                if (obj.AlteracaoNaLei.Value)
                {
                    obj.NumeroLeiAlterada = txtNumeroLei.Text;
                    if (DateTime.TryParse(txtDataAlteracao.Text, out dt))
                        obj.DataLeiAlterada = Convert.ToDateTime(txtDataAlteracao.Text);
                }

                if (!String.IsNullOrEmpty(txtNumeroRepresentateGovernamental.Text))
                    obj.NumeroRepresentanteGovernamentais = Convert.ToInt32(txtNumeroRepresentateGovernamental.Text);
                if (!String.IsNullOrEmpty(txtNumeroRepresentanteSociedadeCivil.Text))
                    obj.NumeroRepresentanteSociedadeCivil = Convert.ToInt32(txtNumeroRepresentanteSociedadeCivil.Text);

                //PRESIDENTE
                if (ddlUsuario.SelectedIndex > 0)
                    obj.IdUsuarioPresidente = Convert.ToInt32(ddlUsuario.SelectedValue);
                if (!String.IsNullOrEmpty(txtNumeroDecreto.Text))
                    obj.NumeroDecreto = txtNumeroDecreto.Text;

                if (!String.IsNullOrEmpty(txtCPF.Text))
                    obj.CPF = txtCPF.Text;

                if (!String.IsNullOrEmpty(txtRG.Txtrg) || !String.IsNullOrEmpty(txtRG.Txtdigito))
                {
                    obj.RG = txtRG.Txtrg;
                    obj.RGDigito = txtRG.Txtdigito;
                }
                if (!String.IsNullOrEmpty(txtDataEmissao.Text))
                    obj.DataEmissao = Convert.ToDateTime(txtDataEmissao.Text);

                if (!String.IsNullOrEmpty(txtOrgEmissor.Text))
                    obj.SiglaEmissor = txtOrgEmissor.Text;

                if (ddlUFEmissor.SelectedValue != "0")
                    obj.IdUf = Convert.ToInt16(ddlUFEmissor.SelectedValue);

                if (!String.IsNullOrEmpty(txtDataDecreto.Text) && DateTime.TryParse(txtDataDecreto.Text, out dt))
                    obj.DataDecreto = Convert.ToDateTime(txtDataDecreto.Text);


                if (!String.IsNullOrEmpty(txtDataInicio.Text) && DateTime.TryParse(txtDataInicio.Text, out dt))
                    obj.DataMandatoInicio = Convert.ToDateTime(txtDataInicio.Text);
                if (!String.IsNullOrEmpty(txtDataTermino.Text) && DateTime.TryParse(txtDataTermino.Text, out dt))
                    obj.DataMandatoTerminio = Convert.ToDateTime(txtDataTermino.Text);

                if (!String.IsNullOrEmpty(txtTelefonePresidenteCMAS.Text))
                    obj.TelefonePresidente = txtTelefonePresidenteCMAS.Text;

                if (!String.IsNullOrEmpty(txtCelularPresidenteCMAS.Text))
                    obj.CelularPresidente = txtCelularPresidenteCMAS.Text;

                if (!String.IsNullOrEmpty(txtEmailPresidenteCMAS.Text))
                    obj.EmailPresidente = txtEmailPresidenteCMAS.Text;

                obj.PossuiSecretariaExecutivaEstruturada = Convert.ToBoolean(Convert.ToInt32(rblSecretariaExecutiva.SelectedValue));
                if (obj.PossuiSecretariaExecutivaEstruturada.HasValue && obj.PossuiSecretariaExecutivaEstruturada.Value)
                {
                    if (!String.IsNullOrEmpty(txtTecnicoSecretariaExecutiva.Text))
                        obj.TotalFuncionariosTecnicoSecretariaExecutiva = Convert.ToInt32(txtTecnicoSecretariaExecutiva.Text);
                    if (!String.IsNullOrEmpty(txtAdministrativoSecretariaExecutiva.Text))
                        obj.TotalFuncionariosAdministrativoSecretariaExecutiva = Convert.ToInt32(txtAdministrativoSecretariaExecutiva.Text);
                }

        
                String msg = String.Empty;
                try
                {
                    //if (String.IsNullOrEmpty(txtDataDecreto.Text) || !DateTime.TryParse(txtDataDecreto.Text, out dt))
                    //    msg += "Data de publicação do decreto é obrigatória" + System.Environment.NewLine;

                    if (msg == "")
                    {
                        new ValidadorConselhoMunicipal().Validar(obj);
                    
                        using (var proxy = new ProxyPrefeitura())
                        {
                            var prefeituras = new Prefeituras(proxy);
                            var presidentesAnteriores = proxy.Service.GetPresidentesAnterioresByConselhoMunicipal(obj.Id);
                            foreach (var pres in presidentesAnteriores)
                            {
                                if (pres.DataInicioMandato > Convert.ToDateTime(txtDataInicio.Text) ||
                                    pres.DataTerminoMandato > Convert.ToDateTime(txtDataInicio.Text))
                                {
                                    throw new Exception("O período de mandato do presidente anterior não pode ser superior ao período de mandato do presidente atual!");
                                }
                            }
                            prefeituras.SaveConselhoMunicipal(obj);
                            load(prefeituras);
                        }
                    }

                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    msg = obj.Id == 0 ? "Presidente do Conselho Municipal registrado com sucesso!" : "Presidente do Conselho Municipal atualizado com sucesso!";
                    lblInconsistencias.Text = "";
                    tbInconsistencias.Visible = false;
                    var script = Util.GetJavaScriptDialogOK(msg);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    BloquearCampos();
                    return;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
                lblInconsistencias.Text = msg.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
            }
        }
        private void BloquearCampos()
        {
            ddlUsuario.Enabled = false;
            txtDataEmissao.Enabled = false;
            txtOrgEmissor.Enabled = false;
            ddlUFEmissor.Enabled = false;
            txtTelefonePresidenteCMAS.Enabled = false;
            txtCelularPresidenteCMAS.Enabled = false;
            txtEmailPresidenteCMAS.Enabled = false;
            txtNumeroDecreto.Enabled = false;
            txtDataDecreto.Enabled = false;
            txtDataInicio.Enabled = false;
            txtDataTermino.Enabled = false;
            btnSalvarPresidente.Enabled = false;

            btnSubstituir.Enabled = SessaoPmas.UsuarioLogado.Prefeitura.Situacao.Id == (int)ESituacao.EmAnalisedoCMAS;
            btnEditar.Enabled = SessaoPmas.UsuarioLogado.Prefeitura.Situacao.Id == (int)ESituacao.EmAnalisedoCMAS;

            txtCPF.Enabled = false;
            txtRG.Enabled = false;

        }

        private void EditarCampos()
        {
            ddlUsuario.Enabled = txtCPF.Enabled = false;
            txtRG.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            txtDataEmissao.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            txtOrgEmissor.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            txtTelefonePresidenteCMAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            txtCelularPresidenteCMAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            ddlUFEmissor.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            txtDataInicio.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            txtDataTermino.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            txtEmailPresidenteCMAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            txtNumeroDecreto.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            txtDataDecreto.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            btnSalvarPresidente.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
            btnSubstituir.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void DesbloquearCampos()
        {
                txtRG.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                txtCPF.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                ddlUsuario.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                txtDataEmissao.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                txtOrgEmissor.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                ddlUFEmissor.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                txtTelefonePresidenteCMAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                txtCelularPresidenteCMAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                txtEmailPresidenteCMAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                txtNumeroDecreto.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                txtDataDecreto.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                txtDataInicio.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                txtDataTermino.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;
                btnSalvarPresidente.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS;

            //  Util.VerificaPermissaoCMAS(txtRG.Controles);
            //Util.VerificaPermissaoCMAS(txtCPF.Controles);
            //Util.VerificaPermissaoCMAS(txtDataInicio.Controles);
            //Util.VerificaPermissaoCMAS(txtDataTermino.Controles);
            //Util.VerificaPermissaoCMAS(txtDtCriacao.Controles);
            //Util.VerificaPermissaoCMAS(txtDataEmissao.Controles);
            //Util.VerificaPermissaoCMAS(txtTelefonePresidenteCMAS.Controles);
            //Util.VerificaPermissaoCMAS(txtCelularPresidenteCMAS.Controles);
            btnSubstituir.Enabled = btnEditar.Enabled = false;

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            EditarCampos();
            frmPresidenteCMAS.Attributes.Add("class", "frame active");
            this.Master.ScriptManagerControl.SetFocus(ddlUsuario);
        }
    }
}