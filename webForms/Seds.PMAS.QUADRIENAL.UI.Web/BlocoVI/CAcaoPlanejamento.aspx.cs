using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using System.Globalization;
using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.UI.Web.Helper;
using System.Web.UI.HtmlControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoVI
{
    public partial class CAcaoPlanejamento : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "AI")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Ação registrada com sucesso!"), true);
                    }
                    else if (Request.QueryString["msg"] == "AU")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Ação atualizada com sucesso!"), true);
                    }
                }

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                using (var proxy = new ProxyAcoes())
                {
                    load(proxy);
                }


                verificarAlteracoes();
                using (var proxy = new ProxyPrefeitura())
                {
                    carregarJustificativa(proxy);
                }
                WebControl[] controles = { btnAdicionar, btnJustificar, txtJustificativa, btnCancelarJustificativa, btnSalvarJusitificativa };//, btnSalvar, txtIGDPBFValorMensal, txtIGDSUASValorMensal};

                if (lst.Items.Count > 0)
                {
                    btnAdicionar.Enabled = true;
                    btnJustificar.Enabled = false;
                }
                else if (!string.IsNullOrEmpty(txtJustificativa.Text))
                {
                    trjustificativa.Visible = true;
                    btnAdicionar.Enabled = false;
                    btnJustificar.Enabled = true;
                }

                Permissao.VerificarPermissaoControles(controles, Session);

            }

        }

        void carregarJustificativa(ProxyPrefeitura proxy)
        {
            var prefeitura = proxy.Service.GetPrefeituraById(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (!String.IsNullOrEmpty(prefeitura.JustificativaAcaoPlanejamento))
            {
                txtJustificativa.Text = prefeitura.JustificativaAcaoPlanejamento;
            }
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro60.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 60);
                    linkAlteracoesQuadro60.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("60"));
                }
            }
        }

        protected void btnAnchor_Click(object sender, ImageClickEventArgs e)
        {
            // Lógica para lidar com o clique no botão aqui
            Response.Redirect("FAcaoPlanejamento.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, Id).ToString())) %>&amp;idCentro=<%=Request.QueryString[id]%>");
        }

        void load(ProxyAcoes proxy)
        {
            var planejamentos = proxy.Service.GetConsultaPlanejamentoAcoesByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id)
                                     .Where(s => s.Exercicio >= 2022)
                                     .OrderBy(t => t.IdEixoAcaoPlanejamento)
                                     .GroupBy(s => s.Eixo)
                                     .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdAcaoPlanejamento) })
                                     .OrderBy(s => s.Key).ToList();


          
            lst.DataSource = planejamentos;
            lst.DataBind();
            if (lst.Items.Count <= 0)
            {
                btnJustificar.Enabled = true;
            }
        }


        protected void lst_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
         
            if (e.CommandName == "Excluir")
            {
                using (var proxy = new ProxyAcoes())
                {                    
                    proxy.Service.DeleteAcaoPlanejamento(Convert.ToInt32(e.CommandArgument));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Ação excluída com sucesso!"), true);
                    load(proxy);
                }

            }

        }

 

        protected void lstItems_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
        
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var lstViewAcoes = (ListView)sender;

                Permissao.VerificarPermissaoControles(new[] { ((ImageButton)e.Item.FindControl("btnExcluir")) }, Session);

                //var imgEditarSituacao = ((ImageButton)WebHelper.FindControlRecursive(lstViewAcoes, "btnEditarSituacao"));
                //var imgExibirSituacao = ((ImageButton)WebHelper.FindControlRecursive(lstViewAcoes, "btnExibirSituacao"));
        
                var pnlEditarSituacao = ((Panel)WebHelper.FindControlRecursive(e.Item, "pnlEditarSituacao"));
                var pnlExibirSituacao = ((Panel)WebHelper.FindControlRecursive(e.Item, "pnlExibirSituacao"));
                var btnSalvar = ((Button)WebHelper.FindControlRecursive(e.Item, "btnSalvar"));
        
        
        
                var rdSituacao1 = (RadioButton)WebHelper.FindControlRecursive(e.Item, "rdSituacao1");
                var rdSituacao2 = (RadioButton)WebHelper.FindControlRecursive(e.Item, "rdSituacao2");
                var rdSituacao3 = (RadioButton)WebHelper.FindControlRecursive(e.Item, "rdSituacao3");
                var rdSituacao4 = (RadioButton)WebHelper.FindControlRecursive(e.Item, "rdSituacao4");

                
        
                var txtSituacaoComentario = (TextBox)WebHelper.FindControlRecursive(e.Item, "txtAreaSituacaoComentario");
        
                Permissao.BlocoVI.VerificarPermissaoSituacaoComentario(new WebControl[] { rdSituacao1, rdSituacao2, rdSituacao3, rdSituacao4, txtSituacaoComentario, btnSalvar });
        
                Permissao.BlocoVI.VerificarPermissaoSituacaoComentarioAcaoEditar(pnlEditarSituacao);
                Permissao.BlocoVI.VerificarPermissaoSituacaoComentarioAcaoExibir(pnlExibirSituacao);
        
        
        
        
            }
        }
        protected void btnJustificar_Click(object sender, EventArgs e)
        {
            trjustificativa.Visible = true;
            btnAdicionar.Enabled = false;

        }

        protected void btnSalvarSituacao_Click(object sender, EventArgs e)
        {
            var comando = (Button)sender;
            var parente = comando.Parent;
            switch (comando.CommandName)
            {
                case "StatusCommand":
                    var rdSituacao1 = (RadioButton)WebHelper.FindControlRecursive(parente, "rdSituacao1");
                    var rdSituacao2 = (RadioButton)WebHelper.FindControlRecursive(parente, "rdSituacao2");
                    var rdSituacao3 = (RadioButton)WebHelper.FindControlRecursive(parente, "rdSituacao3");
                    var rdSituacao4 = (RadioButton)WebHelper.FindControlRecursive(parente, "rdSituacao4");

                    var txtAreaSituacaoComentario = (TextBox)WebHelper.FindControlRecursive(parente, "txtAreaSituacaoComentario");



                    //var obj = new PrefeituraAcaoPlanejamentoInfo();
                    using (var proxy = new ProxyAcoes())
                    {
                        int idAcaoPlanejamento = int.Parse(comando.CommandArgument);

                        var acaoPlanejamentoModel = proxy.Service.GetAcaoPlanejamentoById(idAcaoPlanejamento);
                        int selected = 0;
                        if (rdSituacao1.Checked)
                        {
                            selected = 1;
                        }
                        else if (rdSituacao2.Checked)
                        {
                            selected = 2;
                        }
                        else if (rdSituacao3.Checked)
                        {
                            selected = 3;
                        }
                        else if (rdSituacao4.Checked)
                        {
                            selected = 4;
                        }
                        if (selected == 0)
                        {
                            var script = Util.GetJavaScriptDialogWarning("Selecione uma opção de status!");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        }

                        acaoPlanejamentoModel.Situacao = selected;
                        acaoPlanejamentoModel.SituacaoComentario = txtAreaSituacaoComentario.Text;

                        acaoPlanejamentoModel.Exercicio = 2022;
                        proxy.Service.UpdateAcaoPlanejamento(acaoPlanejamentoModel);
                    }
                    break;
                default:
                    break;
            }
        }


        protected void btnSalvarJusitificativa_Click(object sender, EventArgs e)
        {
            var msg = String.Empty;

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var blocoV = new Seds.PMAS.QUADRIENAL.UI.Processos.Prefeituras(proxy);
                    PrefeituraInfo pre = blocoV.GetPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    pre.JustificativaAcaoPlanejamento = txtJustificativa.Text;
                    new ValidadorPrefeitura().ValidarJustificativaAcaoPlanejamento(pre);
                    blocoV.UpdatePrefeitura(pre, false);

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Justificativa registrada com sucesso";
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = String.Empty;
                tbInconsistencias.Visible = false;
                return;
            }
        }

        protected void btnCancelarJustificativa_Click(object sender, EventArgs e)
        {
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var blocoVI = new Seds.PMAS.QUADRIENAL.UI.Processos.Prefeituras(proxy);
                    PrefeituraInfo pre = blocoVI.GetPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    pre.JustificativaAcaoPlanejamento = String.Empty;
                    blocoVI.UpdatePrefeitura(pre, false);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Justificativa excluída com sucesso!"), true);
                }
            }
            catch (Exception ex)
            {
                var script = Util.GetJavaScriptDialogOK(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
            Response.Redirect("~/BlocoVI/CAcaoPlanejamento.aspx");
        }



    }
}