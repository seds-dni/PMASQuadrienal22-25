using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoVII
{
    public partial class FAspectosGerais : System.Web.UI.Page
    {
        protected Int32 NumPesquisa
        {
            get { return Convert.ToInt32(Session["PESQUISAS"]); }
            set { Session["PESQUISAS"] = value; }
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

                using (var proxy = new ProxyAcoes())
                {
                    load(proxy);
                }

                verificarAlteracoes();

                #region Bloqueia , Desbloqueia e ordena Controles
                WebControl[] controles = { chkAprimoramentos,
                                             txtMetodologia1,
                                             txtMetodologia2,
                                             txtMetodologia3,
                                             txtObjetivo1,
                                             txtObjetivo2,
                                             txtObjetivo3,
                                             txtPeriodo1,
                                             txtPeriodo2,
                                             txtPeriodo3,
                                             txtResultados1,
                                             txtResultados2,
                                             txtResultados3,
                                             rblSistemaInformatizado,
                                             rblMSE,
                                             btnSalvar,
                                             btnAdicionarPesquisa };
                Permissao.VerificarPermissaoControles(controles, Session);
                #endregion
            }
        }

        void load(ProxyAcoes proxy)
        {
            var possui = proxy.Service.PrefeituraPossuiVigilanciaMonitoramentoAvalicao(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            trSim.Visible = possui;
            trNao.Visible = !possui;

            if (!possui)
                return;

            txtMetodologia1.Text = txtMetodologia2.Text = txtMetodologia3.Text = String.Empty;
            txtPeriodo1.Text = txtPeriodo2.Text = txtPeriodo3.Text = String.Empty;
            txtObjetivo1.Text = txtObjetivo2.Text = txtObjetivo3.Text = String.Empty;
            txtResultados1.Text = txtResultados2.Text = txtResultados3.Text = String.Empty;
            hdfPesquisa1.Value = hdfPesquisa2.Value = hdfPesquisa3.Value = "0";

            using (var proxyEstrutura = new ProxyEstruturaAssistenciaSocial())
            {
                carregarEstruturas(proxyEstrutura);
            }

            var obj = proxy.Service.GetAspectosGeraisVigilanciaMonitoramentoAvaliacaoByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null)
                return;
            hdfId.Value = obj.Id.ToString();

            if (obj.Aprimoramentos != null && obj.Aprimoramentos.Count > 0)
            {
                foreach (ListItem i in chkAprimoramentos.Items)
                    i.Selected = obj.Aprimoramentos.Any(t => t.Id == Convert.ToInt32(i.Value));
            }

            if (!obj.PossuiAdesaoMse)
            {
                rblMSE.SelectedValue = Convert.ToByte(obj.PossuiAdesaoMse).ToString();
                trExpliqueNaoMse.Visible = true;
                txtExpliqueNaoMse.Text = obj.DetalhamentoNaoAdesaoMse;
            }
            else
            {
                rblMSE.SelectedValue = Convert.ToByte(obj.PossuiAdesaoMse).ToString();
                trExpliqueNaoMse.Visible = false;
            }

            rblSistemaInformatizado.SelectedValue = Convert.ToSByte(obj.PossuiSistemaInformatizadoProprio).ToString();

            NumPesquisa = obj.Pesquisas != null ? obj.Pesquisas.Count : 0;
            if (obj.Pesquisas == null || obj.Pesquisas.Count < 3)
            {
                trAdicionarPesquisa.Visible = true;
            }

            if (obj.Pesquisas != null && obj.Pesquisas.Count > 0)
            {
                var pesquisa = obj.Pesquisas[0];
                txtPeriodo1.Text = pesquisa.Periodo;
                txtMetodologia1.Text = pesquisa.Metodologia;
                txtObjetivo1.Text = pesquisa.Objetivo;
                txtResultados1.Text = pesquisa.Resultados;
                hdfPesquisa1.Value = pesquisa.Id.ToString();
                trPesquisa1.Visible = true;
            }

            if (obj.Pesquisas != null && obj.Pesquisas.Count > 1)
            {
                var pesquisa = obj.Pesquisas[1];
                txtPeriodo2.Text = pesquisa.Periodo;
                txtMetodologia2.Text = pesquisa.Metodologia;
                txtObjetivo2.Text = pesquisa.Objetivo;
                txtResultados2.Text = pesquisa.Resultados;
                hdfPesquisa2.Value = pesquisa.Id.ToString();
                trPesquisa2.Visible = true;
            }

            if (obj.Pesquisas != null && obj.Pesquisas.Count > 2)
            {
                var pesquisa = obj.Pesquisas[2];
                txtPeriodo3.Text = pesquisa.Periodo;
                txtMetodologia3.Text = pesquisa.Metodologia;
                txtObjetivo3.Text = pesquisa.Objetivo;
                txtResultados3.Text = pesquisa.Resultados;
                hdfPesquisa3.Value = pesquisa.Id.ToString();
                trPesquisa3.Visible = true;
                trAdicionarPesquisa.Visible = false;
            }
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro71.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 71);
                    linkAlteracoesQuadro71.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("71"));
                }
            }
        }

        void carregarEstruturas(ProxyEstruturaAssistenciaSocial proxy)
        {
            chkAprimoramentos.DataTextField = "Nome";
            chkAprimoramentos.DataValueField = "Id";
            chkAprimoramentos.DataSource = proxy.Service.GetAprimoramentosAcoes();
            chkAprimoramentos.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var obj = new PrefeituraVigilanciaMonitoramentoAvaliacaoInfo();
            obj.Id = Convert.ToInt32(hdfId.Value);
            obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            obj.Aprimoramentos = new List<AprimoramentoAcaoInfo>();
            obj.Pesquisas = new List<PrefeituraVigilanciaMonitoramentoAvaliacaoPesquisaInfo>();

            obj.PossuiSistemaInformatizadoProprio = rblSistemaInformatizado.SelectedValue == "1";

            if (rblMSE.SelectedValue == "1")
            {
                obj.PossuiAdesaoMse = true;
            }
            else
            {
                obj.PossuiAdesaoMse = false;
                obj.DetalhamentoNaoAdesaoMse = txtExpliqueNaoMse.Text;
            }


            foreach (ListItem i in chkAprimoramentos.Items)
                if (i.Selected)
                    obj.Aprimoramentos.Add(new AprimoramentoAcaoInfo() { Id = Convert.ToInt32(i.Value) });

            if (!String.IsNullOrEmpty(txtMetodologia1.Text) || !String.IsNullOrEmpty(txtPeriodo1.Text) || !String.IsNullOrEmpty(txtObjetivo1.Text) || !String.IsNullOrEmpty(txtResultados1.Text))
            {
                var pesquisa = new PrefeituraVigilanciaMonitoramentoAvaliacaoPesquisaInfo();
                pesquisa.Id = Convert.ToInt32(hdfPesquisa1.Value);
                pesquisa.IdPrefeituraVigilanciaMonitoramentoAvaliacao = Convert.ToInt32(hdfId.Value);
                if (txtMetodologia1.Text.Length > 300)
                    txtMetodologia1.Text = txtMetodologia1.Text.Substring(0, 300);
                pesquisa.Metodologia = txtMetodologia1.Text;
                if (txtObjetivo1.Text.Length > 150)
                    txtObjetivo1.Text = txtObjetivo1.Text.Substring(0, 150);
                pesquisa.Objetivo = txtObjetivo1.Text;
                pesquisa.Periodo = txtPeriodo1.Text;
                if (txtResultados1.Text.Length > 300)
                    txtResultados1.Text = txtResultados1.Text.Substring(0, 300);
                pesquisa.Resultados = txtResultados1.Text;
                obj.Pesquisas.Add(pesquisa);
            }

            if (!String.IsNullOrEmpty(txtMetodologia2.Text) || !String.IsNullOrEmpty(txtPeriodo2.Text) || !String.IsNullOrEmpty(txtObjetivo2.Text) || !String.IsNullOrEmpty(txtResultados2.Text))
            {
                var pesquisa = new PrefeituraVigilanciaMonitoramentoAvaliacaoPesquisaInfo();
                pesquisa.Id = Convert.ToInt32(hdfPesquisa2.Value);
                pesquisa.IdPrefeituraVigilanciaMonitoramentoAvaliacao = Convert.ToInt32(hdfId.Value);
                if (txtMetodologia2.Text.Length > 300)
                    txtMetodologia2.Text = txtMetodologia2.Text.Substring(0, 300);
                pesquisa.Metodologia = txtMetodologia2.Text;
                if (txtObjetivo2.Text.Length > 150)
                    txtObjetivo2.Text = txtObjetivo2.Text.Substring(0, 150);
                pesquisa.Objetivo = txtObjetivo2.Text;
                pesquisa.Periodo = txtPeriodo2.Text;
                if (txtResultados2.Text.Length > 300)
                    txtResultados2.Text = txtResultados2.Text.Substring(0, 300);
                pesquisa.Resultados = txtResultados2.Text;
                obj.Pesquisas.Add(pesquisa);
            }

            if (!String.IsNullOrEmpty(txtMetodologia3.Text) || !String.IsNullOrEmpty(txtPeriodo3.Text) || !String.IsNullOrEmpty(txtObjetivo3.Text) || !String.IsNullOrEmpty(txtResultados3.Text))
            {
                var pesquisa = new PrefeituraVigilanciaMonitoramentoAvaliacaoPesquisaInfo();
                pesquisa.Id = Convert.ToInt32(hdfPesquisa3.Value);
                pesquisa.IdPrefeituraVigilanciaMonitoramentoAvaliacao = Convert.ToInt32(hdfId.Value);
                if (txtMetodologia3.Text.Length > 300)
                    txtMetodologia3.Text = txtMetodologia3.Text.Substring(0, 300);
                pesquisa.Metodologia = txtMetodologia3.Text;
                if (txtObjetivo3.Text.Length > 150)
                    txtObjetivo3.Text = txtObjetivo3.Text.Substring(0, 150);
                pesquisa.Objetivo = txtObjetivo3.Text;
                pesquisa.Periodo = txtPeriodo3.Text;
                if (txtResultados3.Text.Length > 300)
                    txtResultados3.Text = txtResultados3.Text.Substring(0, 300);
                pesquisa.Resultados = txtResultados3.Text;
                obj.Pesquisas.Add(pesquisa);
            }

            String msg = String.Empty;
            try
            {
                new ValidadorPrefeituraVigilanciaMonitoramentoAvaliacao().Validar(obj);
                foreach (var p in obj.Pesquisas)
                    new ValidadorPrefeituraVigilanciaMonitoramentoAvaliacaoPesquisa().Validar(p);

                using (var proxy = new ProxyAcoes())
                {
                    proxy.Service.SaveAspectosGeraisVigilanciaMonitoramentoAvaliacao(obj);
                    load(proxy);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Aspectos Gerais registrado com sucesso!";
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

        protected void btnAdicionarPesquisa_Click(object sender, EventArgs e)
        {
            NumPesquisa++;
            switch (NumPesquisa)
            {
                case 1:
                    trPesquisa1.Visible = true;
                    break;
                case 2:
                    trPesquisa2.Visible = true;
                    break;
                case 3:
                    trPesquisa3.Visible = true;
                    break;
            }

            if (NumPesquisa == 3)
            {
                trAdicionarPesquisa.Visible = false;
            }
        }

        protected void rblMSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var proxy = new ProxyAcoes())
            {
                var obj = proxy.Service.GetAspectosGeraisVigilanciaMonitoramentoAvaliacaoByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                foreach (ListItem item in rblMSE.Items)
                {
                    if (item.Value == "1" && item.Selected)
                    {
                        trExpliqueNaoMse.Visible = false;
                    }
                    else if (item.Value == "0" && item.Selected)
                    {
                        trExpliqueNaoMse.Visible = true;
                        txtExpliqueNaoMse.Text = obj.DetalhamentoNaoAdesaoMse;
                    }
                }
            }
        }
    }
}