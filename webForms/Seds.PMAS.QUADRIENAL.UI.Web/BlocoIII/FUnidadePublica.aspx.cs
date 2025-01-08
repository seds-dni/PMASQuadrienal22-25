using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FUnidadePublica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                mostrarMensagens();

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                load();
            }
        }

        void mostrarMensagens()
        {
            if (Request.QueryString.AllKeys.Any(t => t == "msg"))
            {
                if (Request.QueryString["msg"] == "UI")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Unidade Pública registrada com sucesso!"), true);
                else if (Request.QueryString["msg"] == "LI")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Local de execução da rede direta reigstrado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "LA")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Local de execução da rede direta atualizado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "LD")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Local de execução da rede direta desativado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "LE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Local de Execução Público excluído com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CRASI")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CRAS registrado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CRASU")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CRAS atualizado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CRASD")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CRAS desativado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CRASE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CRAS excluído com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CREASI")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CREAS registrado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CREASU")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CREAS atualizado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CREASD")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CREAS desativado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "CREASE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("CREAS excluído com sucesso!"), true);
                else if (Request.QueryString["msg"] == "POPI")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Centro POP registrado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "POPU")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Centro POP atualizado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "POPD")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Centro POP desativado com sucesso!"), true);
                else if (Request.QueryString["msg"] == "POPE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Centro POP excluído com sucesso!"), true);

            }
        }

        void verificarAlteracoes(Int32 idUnidade)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro17.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 17, idUnidade);
                    linkAlteracoesQuadro17.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("17")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade.ToString()));

                    linkAlteracoesQuadroLocalPublico.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 18, idUnidade);
                    linkAlteracoesQuadroLocalPublico.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("18")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade.ToString()));

                    linkAlteracoesQuadroCRAS.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 20);
                    linkAlteracoesQuadroCRAS.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("20"));

                    linkAlteracoesQuadroCREAS.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 25);
                    linkAlteracoesQuadroCREAS.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("25"));

                    linkAlteracoesQuadroCentroPOP.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 31);
                    linkAlteracoesQuadroCentroPOP.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("31"));
                }
            }
        }

        void load()
        {
            #region Bloqueia , Desbloqueia e ordena Controles
            WebControl[] controles = {  txtNome
                                        ,btnSalvar                                         
                                        ,btnLocalExecucao
                                        ,btnIncluirCRAS
                                        ,btnIncluirCentroPOP
                                        , btnIncluirCREAS };
            Permissao.VerificarPermissaoControles(controles, Session);
            Permissao.VerificarPermissaoControles(txtCNPJ.Controles, Session);

            #endregion

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                // trLocalizar.Visible = false;
                trLocaisExecucao.Visible = false;
                return;
            }

            btnLocalExecucao.Visible = true;
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            UnidadePublicaInfo unidade;
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                unidade = proxy.Service.GetUnidadePublicaById(id);
                if (unidade.IdPrefeitura != SessaoPmas.UsuarioLogado.Prefeitura.Id)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                if (unidade == null)
                    return;
                carregarLocaisExecucao(proxy, id);
                loadCRAS(proxy, id);
                loadCREAS(proxy, id);
                loadCentroPOP(proxy, id);
            }
            if (unidade.Desativado)
                Response.Redirect("~/BlocoIII/VUnidadePublica.aspx?id=" + Server.UrlEncode(Request.QueryString["id"]));

            verificarAlteracoes(unidade.Id);

            txtCNPJ.Text = unidade.CNPJ;
            txtNome.Text = unidade.RazaoSocial;
            lblCodigoUnidade.Text = unidade.Id.ToString();
        }

        void carregarLocaisExecucao(ProxyRedeProtecaoSocial proxy, Int32 idUnidade, String nome = null) 
        {
            var locais = proxy.Service.GetIdentificacaoLocalExecucaoPublicoByUnidade(idUnidade, nome).Where(c => c.Desativado != true);

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

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                var unidadesPublicas = proxy.Service.GetIdentificacaoUnidadesPublicaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, "");
                var unidadesPrivadas = proxy.Service.GetIdentificacaoUnidadesPrivadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, "");

                List<String> lista = new List<string>();
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    lista.AddRange(unidadesPublicas.Where(f => f.Id != Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Select(c => c.CNPJ));
                }
                else
                {
                    lista.AddRange(unidadesPublicas.Select(c => c.CNPJ));
                }
                lista.AddRange(unidadesPrivadas.Select(c => c.CNPJ));

                if (lista.Contains(txtCNPJ.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("Já existe uma unidade cadastrada no plano com este CNPJ"), true);
                    lblInconsistencias.Text = "Já existe uma unidade cadastrada no plano com este CNPJ!";
                    tbInconsistencias.Visible = true;
                    return;
                }
            }

            var unidade = new UnidadePublicaInfo();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                unidade.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            unidade.CNPJ = txtCNPJ.Text;
            unidade.RazaoSocial = txtNome.Text;
            unidade.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            var msg = String.Empty;

            try
            {
                new ValidadorUnidadePublica().Validar(unidade);

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    if (unidade.Id == 0)
                        unidade.Id = proxy.Service.AddUnidadePublica(unidade);
                    else
                        proxy.Service.UpdateUnidadePublica(unidade);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                if (String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?msg=UI&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(unidade.Id.ToString())));
                    return;
                }
                msg = "Unidade Pública atualizada com sucesso!";
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

        protected void btnLocalExecucao_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            Response.Redirect("~/BlocoIII/FLocalExecucaoPublico.aspx?idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString())));
        }

        protected void lstLocais_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstLocais.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);
            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/FLocalExecucaoPublico.aspx?id=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPublico.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            //if (SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0)
                            //{
                            var s = proxy.Service.GetConsultaServicosRecursosFinanceirosPublicoByLocalExecucao(Convert.ToInt32(Genericos.clsCrypto.Decrypt(id))).Where(c => c.Desativado != true);
                            if (s.Count() > 0)
                                throw new Exception("Esse local de execução ainda possui serviços ativos.<br/>Desative primeiro os serviços para depois desativar o local de execução.");

                            Response.Redirect("~/BlocoIII/FMotivoExclusaoLocalPublico.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                            //}
                            //else
                            // proxy.Service.DeleteLocalExecucaoPublico(Convert.ToInt32(key["Id"].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }

                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?msg=LE&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                default:
                    break;
            }
        }

        protected void lstLocais_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                WebControl controle = (ImageButton)e.Item.FindControl("btnExcluir");
                //Permissao.VerificarPermissaoControles(controles, Session);
                Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoExcluir(controle);
               
                //Permissao.VerificarPermissaoControles(new[] { ((ImageButton)e.Item.FindControl("btnExcluir")) }, Session);

               
            }
        }


        protected string MontarBotaoExcluirLocalPublico(ConsultaLocalExecucaoPublicoInfo item)
        {
            if (!Permissao.VerificarPermissao())
                return null;
            if (!item.Desativado)
            {
                return "<a href='FMotivoExclusaoLocalPublico.aspx?idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.Id.ToString())) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.IdUnidade.ToString())) + "'><img src='../Styles/Icones/editdelete.png' alt='Desativar Local' border='0' /></a>";
            }
            else
            {
                return null;
            }
        }


        void loadCRAS(ProxyRedeProtecaoSocial proxy, Int32 idUnidade, String nome = null) 
        {
             var locais = proxy.Service.GetIdentificacaoCRASByUnidade(idUnidade, nome).
                Where(c => c.Desativado != true);

             #region pivotagem exercicio

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

            lstCRAS.DataSource = locaisSource;
            lstCRAS.DataBind();
        }

        void loadCREAS(ProxyRedeProtecaoSocial proxy, Int32 idUnidade, String nome = null) 
        {
            var locais = proxy.Service.GetIdentificacoesCREASByUnidade(idUnidade, nome).
             Where(c => c.Desativado != true);

            #region pivotagem exercicio

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

            lstCREAS.DataSource = locaisSource;
            lstCREAS.DataBind();
        }

        void loadCentroPOP(ProxyRedeProtecaoSocial proxy, Int32 idUnidade, String nome = null) 
        {
            var locais = proxy.Service.GetIdentificacaoCentroPOPByUnidade(idUnidade, nome).
            Where(c => c.Desativado != true);

            #region pivotagem exercicio

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

            lstCentroPOP.DataSource = locaisSource;
            lstCentroPOP.DataBind();

        }

        protected void btnPrevisaoInstalacaoCentroPOP_Click(object sender, EventArgs e)
        {
            Response.Redirect("FPrevisaoInstalacaoCentroPOP.aspx?idUnidade=" + Server.UrlEncode(Request.QueryString["id"]));
        }

        protected void btnPrevisaoInstalacaoCREAS_Click(object sender, EventArgs e)
        {
            Response.Redirect("FPrevisaoInstalacaoCREAS.aspx?idUnidade=" + Server.UrlEncode(Request.QueryString["id"]));
        }

        protected void lstCRAS_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();

                WebControl controle = (ImageButton)e.Item.FindControl("btnExcluir");
                Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoExcluir(controle);
            }
        }

        protected void lstCREAS_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl controle = (ImageButton)e.Item.FindControl("btnExcluir");
                Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoExcluir(controle);
            }
        }

        protected void lstCentroPOP_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl controle = (ImageButton)e.Item.FindControl("btnExcluir");
                Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIBotaoExcluir(controle);
            }
        }

        protected void btnIncluirCRAS_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            Response.Redirect("~/BlocoIII/FCRAS.aspx?idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString())));
        }

        protected void btnIncluirCREAS_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            Response.Redirect("~/BlocoIII/FCREAS.aspx?idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString())));
        }

        protected void btnIncluirCentroPOP_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            Response.Redirect("~/BlocoIII/FCentroPOP.aspx?idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(id.ToString())));
        }

        protected void btnLocalizarCRAS_Click(object sender, EventArgs e)
        {
            var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCRAS(proxy, idUnidade);
            }
        }

        protected void btnLocalizarCREAS_Click(object sender, EventArgs e)
        {
            var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCREAS(proxy, idUnidade);
            }
        }

        protected void btnLocalizarCentroPOP_Click(object sender, EventArgs e)
        {
            var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCentroPOP(proxy, idUnidade);
            }
        }

        protected void lstCRAS_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstCRAS.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/FCRAS.aspx?id=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCRAS.aspx?id=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Excluir":
                    //try
                    //{
                    //    using (var proxy = new ProxyRedeProtecaoSocial())
                    //    {
                    //        proxy.Service.DeleteCRAS(Convert.ToInt32(key["Id"].ToString()));
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    //    break;
                    //}
                    try
                    {
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            //if (SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0)
                            //{
                            var s = proxy.Service.GetConsultaServicosRecursosFinanceirosByCRAS(Convert.ToInt32(Genericos.clsCrypto.Decrypt(id))).Where(c => c.Desativado != true);
                            if (s.Count() > 0)
                                throw new Exception("Esse CRAS ainda possui serviços ativos.<br/>Desative primeiro os serviços para depois desativar o CRAS.");

                            Response.Redirect("~/BlocoIII/FMotivoExclusaoCRAS.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                            //}
                            //else
                            //    proxy.Service.DeleteLocalExecucaoPublico(Convert.ToInt32(key["Id"].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?msg=CRAS&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                default:
                    break;
            }
        }

        protected void lstCREAS_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstCREAS.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/FCREAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCREAS.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Excluir":
                    //try
                    //{
                    //    using (var proxy = new ProxyRedeProtecaoSocial())
                    //    {
                    //        proxy.Service.DeleteCREAS(Convert.ToInt32(key["Id"].ToString()));
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    //    break;
                    //}
                    try
                    {
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            //if (SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0)
                            //{
                            var s = proxy.Service.GetConsultaServicosRecursosFinanceirosByCREAS(Convert.ToInt32(Genericos.clsCrypto.Decrypt(id))).Where(c => c.Desativado != true);
                            if (s.Count() > 0)
                                throw new Exception("Esse CREAS ainda possui serviços ativos.<br/>Desative primeiro os serviços para depois desativar o CREAS.");

                            Response.Redirect("~/BlocoIII/FMotivoExclusaoCREAS.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                            //}
                            //else
                            //    proxy.Service.DeleteLocalExecucaoPublico(Convert.ToInt32(key["Id"].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }


                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?msg=CREASE&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                default:
                    break;
            }
        }

        protected void lstCentroPOP_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstCentroPOP.DataKeys[e.Item.DataItemIndex];
            var id = Genericos.clsCrypto.Encrypt(key["Id"].ToString());
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["id"]);

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/BlocoIII/FCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Servicos":
                    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(key["Id"].ToString())) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                case "Excluir":
                    try
                    {
                        using (var proxy = new ProxyRedeProtecaoSocial())
                        {
                            //if (SessaoPmas.UsuarioLogado.Prefeitura.Revisao > 0)
                            //{
                            var s = proxy.Service.GetConsultaServicosRecursosFinanceirosByCentroPOP(Convert.ToInt32(Genericos.clsCrypto.Decrypt(id))).Where(c => c.Desativado != true);
                            if (s.Count() > 0)
                                throw new Exception("Esse Centro POP ainda possui serviços ativos.<br/> Desative primeiro os serviços para depois desativar o Centro POP.");

                            Response.Redirect("~/BlocoIII/FMotivoExclusaoCentroPOP.aspx?idLocal=" + Server.UrlEncode(id) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                        }
                        //else
                        //    proxy.Service.DeleteLocalExecucaoPublico(Convert.ToInt32(key["Id"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    //try
                    //{
                    //    using (var proxy = new ProxyRedeProtecaoSocial())
                    //    {

                    //        proxy.Service.DeleteCentroPOP(Convert.ToInt32(key["Id"].ToString()));
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    //    break;
                    //}

                    Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?msg=POPE&id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                    break;

                default:
                    break;
            }
        }

        protected void btnLimparBuscaCRAS_Click(object sender, EventArgs e)
        {
            //txtLocalizarCRAS.Text = String.Empty;
            var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCRAS(proxy, idUnidade);
            }
        }

        protected void btnLimparBuscaCREAS_Click(object sender, EventArgs e)
        {
            //txtLocalizarCREAS.Text = String.Empty;
            var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCREAS(proxy, idUnidade);
            }
        }

        protected void btnLimparBuscaCentroPOP_Click(object sender, EventArgs e)
        {
            //txtLocalizarCentroPOP.Text = String.Empty;
            var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                loadCentroPOP(proxy, idUnidade);
            }
        }

         
        protected void btnLocalizar_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                carregarLocaisExecucao(proxy, id, txtLocalizar.Text);
                loadCRAS(proxy, id, txtLocalizar.Text);
                loadCREAS(proxy, id, txtLocalizar.Text);
                loadCentroPOP(proxy, id, txtLocalizar.Text);
            }
        }

        protected void btnLimparBusca_Click(object sender, EventArgs e)
        {
            txtLocalizar.Text = String.Empty;
            var id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            using (var proxy = new ProxyRedeProtecaoSocial())
            {
                carregarLocaisExecucao(proxy, id, txtLocalizar.Text);
                loadCRAS(proxy, id, txtLocalizar.Text);
                loadCREAS(proxy, id, txtLocalizar.Text);
                loadCentroPOP(proxy, id, txtLocalizar.Text);
            }
        }

        protected void btnLocalizarLocal_Click(object sender, EventArgs e)
        {
            trLocalizar.Visible = true;
        }

        protected void btnLocalExecucaoDesativado_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CLocaisPublicoDesativados.aspx?IdUnidade=" + Server.UrlEncode(Request.QueryString["id"]));
        }

        protected void btnCRASDesativado_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CCRASDesativados.aspx?IdUnidade=" + Server.UrlEncode(Request.QueryString["id"]));
        }


        protected void btnCREASDesativado_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CCREASDesativados.aspx?IdUnidade=" + Server.UrlEncode(Request.QueryString["id"]));
        }

        protected void btnCentroPOPDesativado_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CCentroPOPDesativados.aspx?IdUnidade=" + Server.UrlEncode(Request.QueryString["id"]));
        }
    }
}