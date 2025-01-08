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
    public partial class FAvaliacao : System.Web.UI.Page
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

                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    carregarEstruturas(proxy);
                }

                using (var proxy = new ProxyAcoes())
                {
                    load(proxy);
                }

                verificarAlteracoes();

                #region Bloqueia , Desbloqueia e ordena Controles
                WebControl[] controles = { chkConselhosMunicipais,
                                             chkEmpresasPrivadas,
                                             chkGovernoEstadual,
                                             chkGovernoFederal,
                                             chkMotivos,
                                             chkObjetivos,
                                             chkONGs,
                                             chkOrgaoGestor,
                                             chkProcedimentos,
                                             chkServicoTerceirizado,
                                             rblAvalia,
                                             chkEspecificacaoConselhosMunicipais,
                                             rblDadosMonitoramento,
                                             chkEspecificacaoOrgaoGestor,                                             
                                             btnSalvar};
                Permissao.VerificarPermissaoControles(controles, Session);
                #endregion      
            }
        }

        void load(ProxyAcoes proxy)
        {
            var obj = proxy.Service.GetAvaliacaoByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null)
                return;

            hdfId.Value = obj.Id.ToString();

            rblAvalia.SelectedValue = Convert.ToSByte(obj.AvaliaAcoes).ToString();
            rblAvalia_SelectedIndexChanged(null, null);

            if (!obj.AvaliaAcoes)
            {
                if (obj.MotivosNaoAvaliacao != null && obj.MotivosNaoAvaliacao.Count > 0)
                {
                    foreach (ListItem i in chkMotivos.Items)
                        i.Selected = obj.MotivosNaoAvaliacao.Any(t => t.Id == Convert.ToInt32(i.Value));
                }
                return;
            }

            if (obj.Procedimentos != null && obj.Procedimentos.Count > 0)
            {
                foreach (ListItem i in chkProcedimentos.Items)
                    i.Selected = obj.Procedimentos.Any(t => t.Id == Convert.ToInt32(i.Value));              
            }

            if (obj.Objetivos != null && obj.Objetivos.Count > 0)
            {
                foreach (ListItem i in chkObjetivos.Items)
                    i.Selected = obj.Objetivos.Any(t => t.Id == Convert.ToInt32(i.Value));
            }

            chkOrgaoGestor.Checked = obj.AvaliadoOrgaoGestor;
            if (obj.AvaliadoOrgaoGestor)
            {
                trOrgaoGestor.Visible = true;
                var lstAvaliadores = new List<String>();
                if (obj.AvaliadoOrgaoGestorEquipeEspecifica)
                    lstAvaliadores.Add("EquipeEspecifica");
                if (obj.AvaliadoOrgaoGestorEquipeTecnicoProtecaoSocial)
                    lstAvaliadores.Add("EquipeTecnicoProtecaoSocial");
                if (obj.AvaliadoOrgaoGestorTecnicosOutrasEquipes)
                    lstAvaliadores.Add("TecnicosOutrasEquipes");

                foreach (ListItem i in chkEspecificacaoOrgaoGestor.Items)
                    i.Selected = lstAvaliadores.Any(t => i.Value == t);                
            }

            chkServicoTerceirizado.Checked = obj.AvaliadoTerceirizado;

            rblDadosMonitoramento.SelectedValue = Convert.ToSByte(obj.UtilizaDadosMonitoramento).ToString();

            chkGovernoEstadual.Checked = obj.AvaliacaoGovernoEstadual;

            if (obj.AvaliacaoGovernoEstadual)
            {
                trGovernoEstadual.Visible = true;
                var lstConselhos = new List<String>();
                if (obj.AvaliacaoSedsDrads)
                    lstConselhos.Add("SedsDrads");
                if (obj.AvaliacaoTribunalDeContasDoEstado)
                    lstConselhos.Add("TribunalDeContas");
                if (obj.AvaliacaoSecretariaDaFazenda)
                    lstConselhos.Add("SecretariaDaFazenda");
                if (obj.AvaliacaoMinisterioPublicoEstado)
                    lstConselhos.Add("MinisterioPublico");
                if (obj.AvaliacaoDefensoriaPublicaEstado)
                    lstConselhos.Add("DefensoriaPublica");
                if (obj.AvaliacaoOutrosConselhosEstaduais)
                {
                    lstConselhos.Add("Outros");
                    trOutros.Visible = true;
                    txtOutros.Text = obj.AvaliacaoOutrosQuais; 
                }
                else
                {
                    trOutros.Visible = false;
                    txtOutros.Text = String.Empty;
                }
                
                foreach (ListItem i in chkSecretariasGovernoEstadual.Items)
                    i.Selected = lstConselhos.Any(t => i.Value == t);

            }

            
            chkGovernoFederal.Checked = obj.AvaliacaoGovernoFederal;
            chkEmpresasPrivadas.Checked = obj.AvaliacaoEmpresasPrivadas;
            chkONGs.Checked = obj.AvaliacaoONGs;
            chkConselhosMunicipais.Checked = obj.AvaliacaoConselhosMunicipais;
            
            if (obj.AvaliacaoConselhosMunicipais)
            {
                trConselhosMunicipais.Visible = true;
                var lstConselhos = new List<String>();
                if (obj.AvaliacaoCMAS)
                    lstConselhos.Add("CMAS");
                if (obj.AvaliacaoCMDCA)
                    lstConselhos.Add("CMDCA");
                if (obj.AvaliacaoOutrosConselhosMunicipais)
                    lstConselhos.Add("OutrosConselhos");                

                foreach (ListItem i in chkEspecificacaoConselhosMunicipais.Items)
                    i.Selected = lstConselhos.Any(t => i.Value == t);
            }
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro70.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 70);
                    linkAlteracoesQuadro70.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("70"));
                }
            }
        }

        void carregarEstruturas(ProxyEstruturaAssistenciaSocial proxy)
        {
            chkMotivos.DataTextField = "Nome";
            chkMotivos.DataValueField = "Id";
            chkMotivos.DataSource = proxy.Service.GetMotivosNaoAvaliacao();
            chkMotivos.DataBind();

            chkObjetivos.DataTextField = "Nome";
            chkObjetivos.DataValueField = "Id";
            chkObjetivos.DataSource = proxy.Service.GetObjetivosAvaliacao();
            chkObjetivos.DataBind();

            chkProcedimentos.DataTextField = "Nome";
            chkProcedimentos.DataValueField = "Id";
            chkProcedimentos.DataSource = proxy.Service.GetProcedimentosAvaliacao();
            chkProcedimentos.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var obj = new AvaliacaoInfo();
            obj.Id = Convert.ToInt32(hdfId.Value);
            obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            obj.AvaliaAcoes = rblAvalia.SelectedValue == "1";
            obj.Procedimentos = new List<ProcedimentoAvaliacaoInfo>();
            obj.MotivosNaoAvaliacao = new List<MotivoNaoAvaliacaoInfo>();
            obj.Objetivos = new List<ObjetivoAvaliacaoInfo>();
            if (obj.AvaliaAcoes)
            {
                obj.UtilizaDadosMonitoramento = rblDadosMonitoramento.SelectedValue == "1";
                foreach (ListItem i in chkObjetivos.Items)
                    if (i.Selected)
                        obj.Objetivos.Add(new ObjetivoAvaliacaoInfo() { Id = Convert.ToInt32(i.Value) });

                foreach (ListItem i in chkProcedimentos.Items)
                    if (i.Selected)
                        obj.Procedimentos.Add(new ProcedimentoAvaliacaoInfo() { Id = Convert.ToInt32(i.Value) });
                obj.AvaliadoOrgaoGestor = chkOrgaoGestor.Checked;
                if (obj.AvaliadoOrgaoGestor)
                {
                    foreach (ListItem i in chkEspecificacaoOrgaoGestor.Items)
                    {
                        if (!i.Selected)
                            continue;
                        if (i.Value == "EquipeEspecifica")
                            obj.AvaliadoOrgaoGestorEquipeEspecifica = true;
                        if (i.Value == "EquipeTecnicoProtecaoSocial")
                            obj.AvaliadoOrgaoGestorEquipeTecnicoProtecaoSocial = true;
                        if (i.Value == "TecnicosOutrasEquipes")
                            obj.AvaliadoOrgaoGestorTecnicosOutrasEquipes = true;
                    }
                }
                obj.AvaliadoTerceirizado = chkServicoTerceirizado.Checked;

                obj.AvaliacaoGovernoEstadual = chkGovernoEstadual.Checked;

                if (obj.AvaliacaoGovernoEstadual)
                {
                    foreach (ListItem i in chkSecretariasGovernoEstadual.Items)
                    {
                        if (!i.Selected)
                            continue;
                        if (i.Value == "SedsDrads")
                            obj.AvaliacaoSedsDrads = true;

                        if (i.Value == "TribunalDeContas")
                            obj.AvaliacaoTribunalDeContasDoEstado = true;


                        if (i.Value == "SecretariaDaFazenda")
                            obj.AvaliacaoSecretariaDaFazenda = true;


                        if (i.Value == "MinisterioPublico")
                            obj.AvaliacaoMinisterioPublicoEstado = true;

                        if (i.Value == "DefensoriaPublica")
                            obj.AvaliacaoDefensoriaPublicaEstado = true;

                        if (i.Value == "Outros")
                        {
                            obj.AvaliacaoOutrosConselhosEstaduais = true;
                            
                            if (!String.IsNullOrEmpty(txtOutros.Text))
                            {
                                obj.AvaliacaoOutrosQuais = txtOutros.Text;
                            }
                        }
                    }
                }
                
                
                obj.AvaliacaoGovernoFederal = chkGovernoFederal.Checked;
                obj.AvaliacaoONGs = chkONGs.Checked;
                obj.AvaliacaoEmpresasPrivadas = chkEmpresasPrivadas.Checked;
                obj.AvaliacaoConselhosMunicipais = chkConselhosMunicipais.Checked;
                if (obj.AvaliacaoConselhosMunicipais)
                {
                    foreach (ListItem i in chkEspecificacaoConselhosMunicipais.Items)
                    {
                        if (!i.Selected)
                            continue;
                        if(i.Value == "CMAS")
                            obj.AvaliacaoCMAS = true;
                        if (i.Value == "CMDCA")
                            obj.AvaliacaoCMDCA = true;
                        if (i.Value == "OutrosConselhos")
                            obj.AvaliacaoOutrosConselhosMunicipais = true;
                    }
                }
            }
            else
            {
                foreach (ListItem i in chkMotivos.Items)
                    if (i.Selected)
                        obj.MotivosNaoAvaliacao.Add(new MotivoNaoAvaliacaoInfo() { Id = Convert.ToInt32(i.Value) });
            }

            String msg = String.Empty;
            try
            {
                new ValidadorAvaliacao().Validar(obj);

                using (var proxy = new ProxyAcoes())
                {
                    proxy.Service.SaveAvaliacao(obj);
                    load(proxy);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Avaliação registrada com sucesso!";
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

        protected void rblAvalia_SelectedIndexChanged(object sender, EventArgs e)
        {
            trSim.Visible = rblAvalia.SelectedValue == "1";
            trNao.Visible = rblAvalia.SelectedValue == "0";
        }

        protected void chkOrgaoGestor_CheckedChanged(object sender, EventArgs e)
        {
            trOrgaoGestor.Visible = chkOrgaoGestor.Checked;
        }

        protected void chkConselhosMunicipais_CheckedChanged(object sender, EventArgs e)
        {
            trConselhosMunicipais.Visible = chkConselhosMunicipais.Checked;
        }

        protected void chkGovernoEstadual_CheckedChanged(object sender, EventArgs e)
        {
            trGovernoEstadual.Visible = chkGovernoEstadual.Checked;
        }

        protected void chkSecretariasGovernoEstadual_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var proxy = new ProxyAcoes())
            {

                var obj = proxy.Service.GetAvaliacaoByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                if (obj == null)
                    return;

                foreach (ListItem item in chkSecretariasGovernoEstadual.Items)
                {
                    if (item.Value == "Outros")
                    {
                        if (item.Selected)
                        {
                            trOutros.Visible = true;
                            txtOutros.Text = obj.AvaliacaoOutrosQuais;
                        }
                        else
                        {
                            trOutros.Visible = false;
                        }
                    }
                }
            }
        }


    }
}