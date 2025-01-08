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
    public partial class FVigilancia : System.Web.UI.Page
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

                using (var proxy = new ProxyAcoes())
                {
                    load(proxy);
                }

                verificarAlteracoes();

                #region Bloqueia , Desbloqueia e ordena Controles
                WebControl[] controles = { chkAcoes1,
                                             chkAcoes2,
                                             chkBaseDados,
                                             chkEixo1,
                                             chkEixo2,
                                             //chkOutraBaseDados,  
                                             rblOferece,
                                             txtEspecifiqueBaseDados,
                                             btnSalvar};
                Permissao.VerificarPermissaoControles(controles, Session);
                #endregion      
            }
        }

        void load(ProxyAcoes proxy)
        {
            var obj = proxy.Service.GetVigilanciaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null)
                return;

            hdfId.Value = obj.Id.ToString();

            rblOferece.SelectedValue = Convert.ToSByte(obj.OfereceVigilancia).ToString();
            rblOferece_SelectedIndexChanged(null,null);

            if (!obj.OfereceVigilancia)
                return;

            if(obj.Acoes != null && obj.Acoes.Count > 0)
            {
                chkEixo1.Checked = trAcoes1.Visible = obj.Acoes.Any(t => t.IdAcaoVigilanciaSocioAssistencial == 1);
                if (chkEixo1.Checked)
                    foreach (ListItem i in chkAcoes1.Items)
                        i.Selected = obj.Acoes.Any(t => t.Id == Convert.ToInt32(i.Value));

                chkEixo2.Checked = trAcoes2.Visible = obj.Acoes.Any(t => t.IdAcaoVigilanciaSocioAssistencial == 2);
                if (chkEixo2.Checked)
                    foreach (ListItem i in chkAcoes2.Items)
                        i.Selected = obj.Acoes.Any(t => t.Id == Convert.ToInt32(i.Value));
            }           

            if (obj.BasesDados != null && obj.BasesDados.Count > 0)
            {
                foreach (ListItem i in chkBaseDados.Items)
                    i.Selected = obj.BasesDados.Any(t => t.Id == Convert.ToInt32(i.Value));
                //chkOutraBaseDados.Checked = obj.BasesDados.Any(t => t.Id == 6);
                //if (chkOutraBaseDados.Checked)
                //{
                //    txtEspecifiqueBaseDados.Visible = lblEspecifiqueBaseDados.Visible = true;
                //    txtEspecifiqueBaseDados.Text = obj.OutraBaseDados;
                //}
            }          
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro68.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 68);
                    linkAlteracoesQuadro68.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("68"));
                }
            }
        }

        void carregarEstruturas(ProxyEstruturaAssistenciaSocial proxy)
        {
            chkAcoes1.DataTextField = "Nome";
            chkAcoes1.DataValueField = "Id";
            chkAcoes1.DataSource = proxy.Service.GetAcoesVigilanciaSocioAssistencialByEixo(1);
            chkAcoes1.DataBind();

            chkAcoes2.DataTextField = "Nome";
            chkAcoes2.DataValueField = "Id";
            chkAcoes2.DataSource = proxy.Service.GetAcoesVigilanciaSocioAssistencialByEixo(2);
            chkAcoes2.DataBind();      
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var obj = new VigilanciaSocioAssistencialInfo();
            obj.Id = Convert.ToInt32(hdfId.Value);
            obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            obj.OfereceVigilancia = rblOferece.SelectedValue == "1";
            obj.BasesDados = new List<BaseDadosInfo>();            
            obj.Acoes = new List<AcaoVigilanciaSocioAssistencialInfo>();
            if (obj.OfereceVigilancia)
            {               
                if (chkEixo1.Checked)
                {
                    foreach (ListItem i in chkAcoes1.Items)
                        if (i.Selected)
                            obj.Acoes.Add(new AcaoVigilanciaSocioAssistencialInfo() { Id = Convert.ToInt32(i.Value) });
                }

                if (chkEixo2.Checked)
                {
                    foreach (ListItem i in chkAcoes2.Items)
                        if (i.Selected)
                            obj.Acoes.Add(new AcaoVigilanciaSocioAssistencialInfo() { Id = Convert.ToInt32(i.Value) });
                }

                foreach (ListItem i in chkBaseDados.Items)
                    if (i.Selected)
                        obj.BasesDados.Add(new BaseDadosInfo() { Id = Convert.ToInt32(i.Value) });
                
                //OUTRA BASE DE DADOS
                //if (chkOutraBaseDados.Checked)
                //{
                //    obj.BasesDados.Add(new BaseDadosInfo() { Id = 6 });
                //    obj.OutraBaseDados = txtEspecifiqueBaseDados.Text;
                //}
            }        

            String msg = String.Empty;
            try
            {
                new ValidadorVigilanciaSocioAssistencial().Validar(obj);

                using (var proxy = new ProxyAcoes())
                {
                    proxy.Service.SaveVigilancia(obj);
                    load(proxy);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Vigilância socioassistencial registrada com sucesso!";
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

        protected void rblOferece_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            trSim.Visible = rblOferece.SelectedValue == "1";
            if (trSim.Visible)
            {
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    carregarEstruturas(proxy);
                }
            }
        }

        protected void chkEixo1_CheckedChanged(object sender, EventArgs e)
        {
            trAcoes1.Visible = chkEixo1.Checked;
        }

        protected void chkEixo2_CheckedChanged(object sender, EventArgs e)
        {
            trAcoes2.Visible = chkEixo2.Checked;
        }

        protected void chkOutraBaseDados_CheckedChanged(object sender, EventArgs e)
        {
            //lblEspecifiqueBaseDados.Visible = txtEspecifiqueBaseDados.Visible = chkOutraBaseDados.Checked;
        }
    }
}