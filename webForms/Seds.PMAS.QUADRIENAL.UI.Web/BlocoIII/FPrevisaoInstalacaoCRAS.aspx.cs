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
    public partial class FPrevisaoInstalacaoCRAS : System.Web.UI.Page
    {
        protected List<PrevisaoInstalacaoCRASInfo> Datas
        {
            get { return Session["DATA_INSTALACAO"] as List<PrevisaoInstalacaoCRASInfo>; }
            set { Session["DATA_INSTALACAO"] = value; }
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
                var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));

                carregarEstruturas();

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    load(proxy);
                    carregarMotivosNaoInstalacao(proxy);
                }

                if (trDataPrevista.Visible == false)
                    trMotivosNaoInstalacao.Visible = true;

                txtDataPrevista.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);txtBoxFormat(this,'99/99/9999', event);");

                WebControl[] controles = { lstMotivosNaoInstalacao, lstDatas, rblPossuiPrevisaoInstalacao, txtDataPrevista, btnSalvar };
                Permissao.VerificarPermissaoControles(controles, Session);

                verificarAlteracoes(idUnidade);
            }

        }

        void load(ProxyRedeProtecaoSocial proxy) 
        {
            var obj = proxy.Service.GetPrevisoesCRASByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null || obj.Count() == 0)
                return;
            lstDatas.DataSource = obj;
            lstDatas.DataBind();
            trDatas.Visible = true;
            trDataPrevista.Visible = true;
            rblPossuiPrevisaoInstalacao.SelectedIndex = 0;
            Datas = obj.ToList();


        }

        void carregarEstruturas()
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                lstMotivosNaoInstalacao.DataValueField = "Id";
                lstMotivosNaoInstalacao.DataTextField = "Nome";
                lstMotivosNaoInstalacao.DataSource = proxy.Service.GetMotivoNaoInstalacaoCRAS();
                lstMotivosNaoInstalacao.DataBind();
            }
        }

        void carregarMotivosNaoInstalacao(ProxyRedeProtecaoSocial proxy)
        {

            var lst = proxy.Service.GetMotivosDeNaoInstalacaoDeCRAS(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            if (lst == null || lst.Count() == 0)
                return;

            foreach (ListItem i in lstMotivosNaoInstalacao.Items)
                i.Selected = lst.Any(m => m.Id == Convert.ToInt32(i.Value));

            trMotivosNaoInstalacao.Visible = true;
            rblPossuiPrevisaoInstalacao.SelectedIndex = 1;

        }

        void verificarAlteracoes(Int32 idUnidade)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesPrevisaoImplantacaoCRAS.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 24, idUnidade);
                    linkAlteracoesPrevisaoImplantacaoCRAS.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("24")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade.ToString()));
                }
            }
        }

        protected void rblPossuiPrevisaoInstalacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblPossuiPrevisaoInstalacao.SelectedIndex == 1)
            {
                trMotivosNaoInstalacao.Visible = true;
                lstMotivosNaoInstalacao.ClearSelection();
                trDatas.Visible = false;
                trDataPrevista.Visible = false;
            }
            else
            {
                trMotivosNaoInstalacao.Visible = false;
                lstDatas.DataSource = null;
                lstDatas.Items.Clear();
                trDatas.Visible = true;
                trDataPrevista.Visible = true;
            }
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {

            var previsao = new PrevisaoInstalacaoCRASInfo();

            DateTime dt;
            if (!String.IsNullOrEmpty(txtDataPrevista.Text) && DateTime.TryParse(txtDataPrevista.Text, out dt))
                previsao.Data = Convert.ToDateTime(txtDataPrevista.Text);

            previsao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
         
            try
            {
                new ValidadorPrevisaoInstalacaoCRAS().Validar(true, null, new List<PrevisaoInstalacaoCRASInfo>() { previsao });
            }
            catch (Exception ex)
            {
                string msg = "Verifique as inconsistências!";
                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Datas = Datas ?? new List<PrevisaoInstalacaoCRASInfo>();
            Datas.Add(previsao);

            carregaDatas();

            txtDataPrevista.Text = string.Empty;
            tbInconsistencias.Visible = false;

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            var lst = new List<MotivoNaoInstalacaoInfo>();
            string msg = string.Empty;
            var script = Util.GetJavaScriptDialogOK(msg);

            foreach (ListItem i in lstMotivosNaoInstalacao.Items)
                if (i.Selected)
                    lst.Add(new MotivoNaoInstalacaoInfo() { Id = Convert.ToInt32(i.Value), Nome = i.Text });

            Datas = Datas ?? new List<PrevisaoInstalacaoCRASInfo>();
            Int32 idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));
            try
            {
                new ValidadorPrevisaoInstalacaoCRAS().Validar(rblPossuiPrevisaoInstalacao.SelectedValue == "1" , lst, Datas );

                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    proxy.Service.SalvarPrevisaoInstalacaoCRAS(rblPossuiPrevisaoInstalacao.SelectedValue == "1", Datas, lst, SessaoPmas.UsuarioLogado.Prefeitura.Id, idUnidade);
                }

            }
            catch (Exception ex)
            {
                msg = "Verifique as inconsistências!";
                script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }
            msg = "Previsão de implantação de CRAS registrada com sucesso!";
            lblInconsistencias.Text = "";
            tbInconsistencias.Visible = false;
            script = Util.GetJavaScriptDialogOK(msg);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);


        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/FUnidadePublica.aspx?id=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }

        void carregaDatas()
        {
            lstDatas.DataSource = Datas;
            lstDatas.DataBind();
        }

        protected void lstDatas_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Excluir":
                        if (Datas == null || Datas.Count == 0)
                            break;
                        Datas.RemoveAt(e.Item.DataItemIndex);
                        carregaDatas();
                        var script = Util.GetJavaScriptDialogOK("Previsão de implantação removida com sucesso!");
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

        protected void lstDatas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }

        }


    }
}