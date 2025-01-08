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
    public partial class FPrevisaoInstalacaoCREAS : System.Web.UI.Page
    {
        protected List<PrevisaoInstalacaoCREASInfo> Datas
        {
            get { return Session["DATA_INSTALACAO_CREAS"] as List<PrevisaoInstalacaoCREASInfo>; }
            set { Session["DATA_INSTALACAO_CREAS"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {

                    carregarEstruturas(proxy);

                    using (var proxyRede = new ProxyRedeProtecaoSocial())
                    {
                        load(proxyRede);
                        carregarMotivosNaoInstalacao(proxyRede);
                        carregarDemandasAtendimento(proxyRede,proxy);
                    }
                }

                if (trDataPrevista.Visible == false)
                    trMotivosNaoInstalacao.Visible = true;

                if (rblDemandaAtendimento.SelectedIndex == 0)
                    trTipoDemanda.Visible = true;

                txtDataPrevista.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);txtBoxFormat(this,'99/99/9999', event);");                

                WebControl[] controles = { lstMotivosNaoInstalacao, lstDatas, rblPossuiPrevisaoInstalacao, txtDataPrevista, btnSalvar, lstDemandas, rblDemandaAtendimento };
                Permissao.VerificarPermissaoControles(controles, Session);

                verificarAlteracoes(idUnidade);
            }

        }


        void load(ProxyRedeProtecaoSocial proxy)
        {
            var obj = proxy.Service.GetPrevisaoCREASByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null || obj.Count() == 0)
                return;
            lstDatas.DataSource = obj;
            lstDatas.DataBind();
            trDatas.Visible = true;
            trDataPrevista.Visible = true;
            rblPossuiPrevisaoInstalacao.SelectedIndex = 0;
            Datas = obj.ToList();


        }

        void verificarAlteracoes(Int32 idUnidade)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesPrevisaoImplantacaoCREAS.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 30, idUnidade);
                    linkAlteracoesPrevisaoImplantacaoCREAS.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("30")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade.ToString()));
                }
            }
        }

        void carregarEstruturas(ProxyEstruturaAssistenciaSocial proxy)
        {        
                lstMotivosNaoInstalacao.DataValueField = "Id";
                lstMotivosNaoInstalacao.DataTextField = "Nome";
                lstMotivosNaoInstalacao.DataSource = proxy.Service.GetMotivoNaoInstalacaoCREAS();
                lstMotivosNaoInstalacao.DataBind();                

        }

        void carregarMotivosNaoInstalacao(ProxyRedeProtecaoSocial proxy)
        {

            var lst = proxy.Service.GetMotivoNaoInstalacaoCREASByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            if (lst == null || lst.Count() == 0)
                return;

            foreach (ListItem i in lstMotivosNaoInstalacao.Items)
                i.Selected = lst.Any(m => m.Id == Convert.ToInt32(i.Value));

            trMotivosNaoInstalacao.Visible = true;
            rblPossuiPrevisaoInstalacao.SelectedIndex = 1;

        }

        void carregarDemandasAtendimento(ProxyRedeProtecaoSocial proxy, ProxyEstruturaAssistenciaSocial proxyEstrutura)
        {

            var lst = proxy.Service.GetDemandaAtendimentoCREASByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            var lstTiposDemandas = proxyEstrutura.Service.GetTipoDemandaAtendimento();
            var lstPrefeituraDemanda = new List<Demanda>();
            foreach (var d in lstTiposDemandas)
            {
                var demanda = new Demanda(){ IdTipoDemanda = d.Id, TipoDemanda = d.Nome };
                demanda.Checked = lst != null && lst.Any(t => t.IdDemandaAtendimento == demanda.IdTipoDemanda);
                if (demanda.Checked)
                    demanda.Quantidade = lst.First(t => t.IdDemandaAtendimento == demanda.IdTipoDemanda).QuantidadeDemanda;
                lstPrefeituraDemanda.Add(demanda);
            }


            lstDemandas.DataSource = lstPrefeituraDemanda;
            lstDemandas.DataBind();

            if (lst.ToList().Count != 0)
            {
                trTipoDemanda.Visible = true;
                rblDemandaAtendimento.SelectedIndex = 0;
            }
            else
            {
                trTipoDemanda.Visible = false;
                rblDemandaAtendimento.SelectedIndex = 1;
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

            var previsao = new PrevisaoInstalacaoCREASInfo();

            DateTime dt;
            if (!String.IsNullOrEmpty(txtDataPrevista.Text) && DateTime.TryParse(txtDataPrevista.Text, out dt))
                previsao.Data = Convert.ToDateTime(txtDataPrevista.Text);

            previsao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;

            try
            {
                new ValidadorPrevisaoInstalacaoCREAS().Validar(true, null, new List<PrevisaoInstalacaoCREASInfo>() { previsao }, false,  null );
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

            Datas = Datas ?? new List<PrevisaoInstalacaoCREASInfo>();
            Datas.Add(previsao);

            carregaDatas();

            txtDataPrevista.Text = string.Empty;
            tbInconsistencias.Visible = false;

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            string msg = string.Empty;
            var script = Util.GetJavaScriptDialogOK(msg);
            var idUnidade = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]));

            var lstDemanda = new List<PrefeituraDemandaAtendimentoInfo>();
            if (rblDemandaAtendimento.SelectedValue == "1")
            {
                foreach (var i in lstDemandas.Items)
                {
                    if (i.ItemType != ListViewItemType.DataItem)
                        continue;
                    var chk = (CheckBox)i.FindControl("chkTipoDemanda");

                    if (!chk.Checked)
                        continue;
                    var demanda = new PrefeituraDemandaAtendimentoInfo();
                    var hdf = (HiddenField)i.FindControl("hdfIdTipoDemanda");
                    demanda.IdDemandaAtendimento = Convert.ToInt32(hdf.Value);
                    demanda.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
                    var txt = (TextBox)i.FindControl("txtQuantidade");
                    if (!String.IsNullOrEmpty(txt.Text) && Util.TryParseInt32(txt.Text) != 0) 
                        demanda.QuantidadeDemanda = Convert.ToInt32(txt.Text);
                    else
                    {
                        var tDem = new TipoDemandaAtendimentoInfo();
                        tDem.Nome = chk.Text;
                        demanda.TipoDemandaAtendimento = tDem;
                    }
                    
                    lstDemanda.Add(demanda);

                }
            }
            var lst = new List<MotivoNaoInstalacaoInfo>();

            foreach (ListItem i in lstMotivosNaoInstalacao.Items)
                if (i.Selected)
                    lst.Add(new MotivoNaoInstalacaoInfo() { Id = Convert.ToInt32(i.Value), Nome = i.Text });

            Datas = Datas ?? new List<PrevisaoInstalacaoCREASInfo>();

            try
            {
                new ValidadorPrevisaoInstalacaoCREAS().Validar(rblPossuiPrevisaoInstalacao.SelectedValue == "1", lst, Datas, rblDemandaAtendimento.SelectedValue == "0", lstDemanda);
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    proxy.Service.SavePrevisaoInstalacaoCREAS(rblPossuiPrevisaoInstalacao.SelectedValue == "1", Datas, lst, SessaoPmas.UsuarioLogado.Prefeitura.Id, rblDemandaAtendimento.SelectedValue == "0", lstDemanda, idUnidade);
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
            msg = "Previsão de implantação de CREAS registrada com sucesso!";
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

        protected void rblDemandaAtendimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblDemandaAtendimento.SelectedIndex == 0)
            {
                trTipoDemanda.Visible = true;
            }
            else
            {
                trTipoDemanda.Visible = false;
            }
        }

        protected void lstDemandas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (Demanda)e.Item.DataItem;
                var chk = (CheckBox)e.Item.FindControl("chkTipoDemanda");
                chk.Checked = item.Checked;
                var hdf = (HiddenField)e.Item.FindControl("hdfIdTipoDemanda");
                hdf.Value = item.IdTipoDemanda.ToString();
                var txt = (TextBox)e.Item.FindControl("txtQuantidade");
                txt.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txt.Enabled = chk.Checked;
                if (item.Checked)
                    txt.Text = item.Quantidade.ToString();
                chk.Text = item.TipoDemanda;
            }
        }

        public void chkTipoDemanda_CheckedChanged(object sender, EventArgs e)
        {
            var target = (CheckBox)sender;
            ListViewDataItem di = target.NamingContainer as ListViewDataItem;
            var txt = (TextBox)di.FindControl("txtQuantidade");
            txt.Enabled = target.Checked;
            if (!target.Checked)
            {
                txt.Text = "";
                return;
            }

            this.Master.ScriptManagerControl.SetFocus(txt);
        }
    }

    public struct Demanda
    {
        public Boolean Checked { get; set; }
        public String TipoDemanda { get; set; }
        public Int32 IdTipoDemanda { get; set; }
        public Int32 Quantidade { get; set; }
    }
}