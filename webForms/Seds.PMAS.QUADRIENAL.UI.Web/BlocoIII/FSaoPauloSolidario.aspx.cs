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
    public partial class FSaoPauloSolidario : System.Web.UI.Page
    {
        protected List<TransferenciaRendaParceriaInfo> Parcerias
        {
            get { return Session["PARCERIAS"] as List<TransferenciaRendaParceriaInfo>; }
            set { Session["PARCERIAS"] = value; }
        }

        protected List<SPSolidarioAgendaFamiliaParceriaInfo> ParceriasAgendaFamilia
        {
            get { return Session["PARCERIAS_SP_SOLIDARIO_AGENDA_FAMILIA"] as List<SPSolidarioAgendaFamiliaParceriaInfo>; }
            set { Session["PARCERIAS_SP_SOLIDARIO_AGENDA_FAMILIA"] = value; }
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

                if (String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Response.Redirect("~/BlocoIII/CTransferenciaRenda.aspx");
                    return;
                }

                carregarEstruturas();

                using (var proxy = new ProxyProgramas())
                {
                    load(proxy);
                }

                txtPrevisaoAnualRepasse.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtFMAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

                txtFNAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtOrcamentoMunicipal.Attributes.Add("onkeyup", "return( currencyFormat( this, '.', ',', event ) );");
                txtIGDPBF.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtIGDSUAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

                txtFMASAgendaFamilia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

                txtFNASAgendaFamilia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtOrcamentoMunicipalAgendaFamilia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtIGDPBFAgendaFamilia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtIGDSUASAgendaFamilia.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

                #region Bloqueia , Desbloqueia e ordena Controles
                WebControl[] controles = { txtNome 
                                             ,txtObjetivo                                                                                           
                                             ,btnSalvar                                             
                                             ,rblParcerias
                                             ,txtPrevisaoAnualRepasse
                                             ,txtMeta                                             
                                             ,txtFMAS
                                             ,txtFNAS
                                             ,txtOrcamentoMunicipal
                                             ,txtIGDPBF
                                             ,txtIGDSUAS                                               
                                             ,txtFMASAgendaFamilia
                                             ,txtFNASAgendaFamilia
                                             ,txtOrcamentoMunicipalAgendaFamilia
                                             ,txtIGDPBFAgendaFamilia
                                             ,txtIGDSUASAgendaFamilia
                                             ,txtAnoInicio
                                             ,txtAnoTermino
                                             ,ddlMesInicio
                                             ,ddlMesTermino
                                             ,txtNomeOrgao
                                             ,ddlBeneficiarios
                                             ,ddlParceria
                                             ,ddlTipoParceria
                                             ,chkOrgaosAgendaFamilia
                                             ,chkOrgaosExecutores
                                             ,rblFase
                                             ,rblIntegracaoRede
                                             ,rblParceriasAgendaFamilia
                                             };
                Permissao.VerificarPermissaoControles(controles, Session);                
                #endregion
            }
        }      

        void load(ProxyProgramas proxy)
        {
            
            var obj = proxy.Service.GetTransferenciaRendaById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            Parcerias = new List<TransferenciaRendaParceriaInfo>();
            ParceriasAgendaFamilia = new List<SPSolidarioAgendaFamiliaParceriaInfo>();
            if (obj == null)
                return;           

            txtNome.Text = obj.Nome;
            txtObjetivo.Text = obj.Objetivo;            
            ddlBeneficiarios.SelectedValue = obj.IdUsuarioTransferenciaRenda.ToString();
            rblIntegracaoRede.SelectedValue = Convert.ToSByte(obj.BeneficiarioAtendidoRedeSocioAssistencial).ToString();

            if (!obj.IdFaseProgramaSaoPauloSolidario.HasValue)
                return;
            
            rblFase.SelectedValue = obj.IdFaseProgramaSaoPauloSolidario.Value.ToString();
            rblFase_SelectedIndexChanged(null, null);

            if (obj.SaoPauloSolidarioMesInicioBuscaAtiva.HasValue)
                ddlMesInicio.SelectedValue = obj.SaoPauloSolidarioMesInicioBuscaAtiva.Value.ToString();
            if (obj.SaoPauloSolidarioAnoInicioBuscaAtiva.HasValue)
                txtAnoInicio.Text = obj.SaoPauloSolidarioAnoInicioBuscaAtiva.Value.ToString();
            if (obj.SaoPauloSolidarioMesTerminoBuscaAtiva.HasValue)
                ddlMesTermino.SelectedValue = obj.SaoPauloSolidarioMesTerminoBuscaAtiva.Value.ToString();
            if (obj.SaoPauloSolidarioAnoTerminoBuscaAtiva.HasValue)
                txtAnoTermino.Text = obj.SaoPauloSolidarioAnoTerminoBuscaAtiva.Value.ToString();
            
            if (obj.SaoPauloSolidarioValorFMASBuscaAtiva.HasValue)
                txtFMAS.Text = obj.SaoPauloSolidarioValorFMASBuscaAtiva.Value.ToString("N2");
            if (obj.SaoPauloSolidarioValorFNASBuscaAtiva.HasValue)
                txtFNAS.Text = obj.SaoPauloSolidarioValorFNASBuscaAtiva.Value.ToString("N2");
            if (obj.SaoPauloSolidarioValorOrcamentoMunicipalBuscaAtiva.HasValue)
                txtOrcamentoMunicipal.Text = obj.SaoPauloSolidarioValorOrcamentoMunicipalBuscaAtiva.Value.ToString("N2");
            if (obj.SaoPauloSolidarioValorIGDPBFBuscaAtiva.HasValue)
                txtIGDPBF.Text = obj.SaoPauloSolidarioValorIGDPBFBuscaAtiva.Value.ToString("N2");
            if (obj.SaoPauloSolidarioValorIGDSUASBuscaAtiva.HasValue)
                txtIGDSUAS.Text = obj.SaoPauloSolidarioValorIGDSUASBuscaAtiva.Value.ToString("N2");

            if (obj.SaoPauloSolidarioValorFMASAgendaFamilia.HasValue)
                txtFMASAgendaFamilia.Text = obj.SaoPauloSolidarioValorFMASAgendaFamilia.Value.ToString("N2");
            if (obj.SaoPauloSolidarioValorFNASAgendaFamilia.HasValue)
                txtFNASAgendaFamilia.Text = obj.SaoPauloSolidarioValorFNASAgendaFamilia.Value.ToString("N2");
            if (obj.SaoPauloSolidarioValorOrcamentoMunicipalAgendaFamilia.HasValue)
                txtOrcamentoMunicipalAgendaFamilia.Text = obj.SaoPauloSolidarioValorOrcamentoMunicipalAgendaFamilia.Value.ToString("N2");
            if (obj.SaoPauloSolidarioValorIGDPBFAgendaFamilia.HasValue)
                txtIGDPBFAgendaFamilia.Text = obj.SaoPauloSolidarioValorIGDPBFAgendaFamilia.Value.ToString("N2");
            if (obj.SaoPauloSolidarioValorIGDSUASAgendaFamilia.HasValue)
                txtIGDSUASAgendaFamilia.Text = obj.SaoPauloSolidarioValorIGDSUASAgendaFamilia.Value.ToString("N2");

            //if (obj.SaoPauloSolidarioRepasseAnual.HasValue)            
            //    txtPrevisaoAnualRepasse.Text = obj.SaoPauloSolidarioRepasseAnual.Value.ToString("N2");

            //if (obj.SaoPauloSolidarioMeta.HasValue)
            //    txtMeta.Text = obj.SaoPauloSolidarioMeta.Value.ToString();

            if (obj.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2012.HasValue)
                txtNumeroFamilias.Text = obj.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2012.Value.ToString();
            if (obj.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013.HasValue)
                txtNumeroFamilias2013.Text = obj.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013.Value.ToString();

            var lstExecutores = new List<int>();
            if (obj.SaoPauloSolidarioOrgaoGestorExecutaBuscaAtiva.HasValue && obj.SaoPauloSolidarioOrgaoGestorExecutaBuscaAtiva.Value)
                lstExecutores.Add(1);
            if (obj.SaoPauloSolidarioCRASExecutaBuscaAtiva.HasValue && obj.SaoPauloSolidarioCRASExecutaBuscaAtiva.Value)
                lstExecutores.Add(2);
            if (obj.SaoPauloSolidarioCREASExecutaBuscaAtiva.HasValue && obj.SaoPauloSolidarioCREASExecutaBuscaAtiva.Value)
                lstExecutores.Add(3);
            if (obj.SaoPauloSolidarioUnidadePrivadaExecutaBuscaAtiva.HasValue && obj.SaoPauloSolidarioUnidadePrivadaExecutaBuscaAtiva.Value)
                lstExecutores.Add(4);

            foreach(ListItem i in chkOrgaosExecutores.Items)                
                    i.Selected = lstExecutores.Any(t=> Convert.ToInt32(i.Value) == t);

            var lstExecutoresAgendaFamilia = new List<int>();
            if (obj.SaoPauloSolidarioOrgaoGestorExecutaAgendaFamilia.HasValue && obj.SaoPauloSolidarioOrgaoGestorExecutaAgendaFamilia.Value)
                lstExecutoresAgendaFamilia.Add(1);
            if (obj.SaoPauloSolidarioCRASExecutaAgendaFamilia.HasValue && obj.SaoPauloSolidarioCRASExecutaAgendaFamilia.Value)
                lstExecutoresAgendaFamilia.Add(2);
            if (obj.SaoPauloSolidarioCREASExecutaAgendaFamilia.HasValue && obj.SaoPauloSolidarioCREASExecutaAgendaFamilia.Value)
                lstExecutoresAgendaFamilia.Add(3);            

            foreach (ListItem i in chkOrgaosAgendaFamilia.Items)
                i.Selected = lstExecutoresAgendaFamilia.Any(t => Convert.ToInt32(i.Value) == t);


            rblParcerias.SelectedValue = Convert.ToSByte(obj.PossuiParceriaFormal).ToString();
            tbParcerias.Visible = obj.PossuiParceriaFormal;
            if (obj.PossuiParceriaFormal)
            {
                Parcerias = obj.Parcerias;
                carregarParcerias();
            }

            if (obj.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.HasValue)
            {
                rblParceriasAgendaFamilia.SelectedValue = Convert.ToSByte(obj.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia).ToString();
                tbParceriasAgendaFamilia.Visible = obj.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.Value;
                if (obj.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.Value)
                {
                    ParceriasAgendaFamilia = obj.ParceriasSaoPauloSolidarioAgendaFamilia;
                    carregarParceriasAgendaFamilia();
                }
            }

            verificarAlteracoes(obj.Id);
        }

        void verificarAlteracoes(Int32 idTransferenciaRenda)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro49.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 49, idTransferenciaRenda);
                    linkAlteracoesQuadro49.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("49")) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idTransferenciaRenda.ToString()));
                }
            }
        }

        void carregarParcerias()
        {
            lstParcerias.DataSource = Parcerias;
            lstParcerias.DataBind();
        }

        void carregarParceriasAgendaFamilia()
        {
            lstParceriasAgendaFamilia.DataSource = ParceriasAgendaFamilia;
            lstParceriasAgendaFamilia.DataBind();
        }

        void carregarEstruturas()
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                ddlParceria.DataValueField = "Id";
                ddlParceria.DataTextField = "Nome";
                ddlParceriaAgendaFamilia.DataValueField = "Id";
                ddlParceriaAgendaFamilia.DataTextField = "Nome";
                ddlParceria.DataSource = ddlParceriaAgendaFamilia.DataSource = proxy.Service.GetParcerias();
                ddlParceria.DataBind();
                ddlParceriaAgendaFamilia.DataBind();
                Util.InserirItemEscolha(ddlParceria);
                Util.InserirItemEscolha(ddlParceriaAgendaFamilia);

                ddlTipoParceria.DataValueField = "Id";
                ddlTipoParceria.DataTextField = "Nome";
                ddlTipoParceriaAgendaFamilia.DataValueField = "Id";
                ddlTipoParceriaAgendaFamilia.DataTextField = "Nome";
                ddlTipoParceria.DataSource = ddlTipoParceriaAgendaFamilia.DataSource = proxy.Service.GetTiposParceria();                
                ddlTipoParceria.DataBind();
                ddlTipoParceriaAgendaFamilia.DataBind();
                Util.InserirItemEscolha(ddlTipoParceria);
                Util.InserirItemEscolha(ddlTipoParceriaAgendaFamilia);

                ddlBeneficiarios.DataValueField = "Id";
                ddlBeneficiarios.DataTextField = "Nome";
                ddlBeneficiarios.DataSource = proxy.Service.GetUsuarioTransferenciaRenda();
                ddlBeneficiarios.DataBind();                
                Util.InserirItemEscolha(ddlBeneficiarios);
                ddlBeneficiarios.Enabled = false;
            }
        }       

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var obj = new TransferenciaRendaInfo();
            using (ProxyProgramas proxy = new ProxyProgramas())
            {
                obj = proxy.Service.GetTransferenciaRendaById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            }       
     
            obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            obj.IdUsuarioTransferenciaRenda = Convert.ToInt32(ddlBeneficiarios.SelectedValue);
            obj.IdTipoTransferenciaRenda = Convert.ToInt32(ETipoTransferenciaRenda.SaoPauloSolidario);
            obj.Nome = txtNome.Text;
            obj.Objetivo = txtObjetivo.Text;           
            obj.PossuiParceriaFormal = rblParcerias.SelectedValue == "1";            
            obj.IdFaseProgramaSaoPauloSolidario = Convert.ToInt32(rblFase.SelectedValue);

            obj.SaoPauloSolidarioAnoInicioBuscaAtiva = null;
            if(!String.IsNullOrEmpty(txtAnoInicio.Text))
                obj.SaoPauloSolidarioAnoInicioBuscaAtiva = Convert.ToInt32(txtAnoInicio.Text);
            obj.SaoPauloSolidarioAnoTerminoBuscaAtiva = null;
            if (!String.IsNullOrEmpty(txtAnoTermino.Text))
                obj.SaoPauloSolidarioAnoTerminoBuscaAtiva = Convert.ToInt32(txtAnoTermino.Text);
            obj.SaoPauloSolidarioMesInicioBuscaAtiva = null;
            if (ddlMesInicio.SelectedValue != "0")
                obj.SaoPauloSolidarioMesInicioBuscaAtiva = Convert.ToInt32(ddlMesInicio.SelectedValue);
            obj.SaoPauloSolidarioMesTerminoBuscaAtiva = null;
            if (ddlMesTermino.SelectedValue != "0")
                obj.SaoPauloSolidarioMesTerminoBuscaAtiva = Convert.ToInt32(ddlMesTermino.SelectedValue);

            obj.SaoPauloSolidarioValorFMASBuscaAtiva = null;
            if (!String.IsNullOrEmpty(txtFMAS.Text))
                obj.SaoPauloSolidarioValorFMASBuscaAtiva = Convert.ToDecimal(txtFMAS.Text);
            obj.SaoPauloSolidarioValorFNASBuscaAtiva = null;
            if (!String.IsNullOrEmpty(txtFNAS.Text))
                obj.SaoPauloSolidarioValorFNASBuscaAtiva = Convert.ToDecimal(txtFNAS.Text);
            obj.SaoPauloSolidarioValorOrcamentoMunicipalBuscaAtiva = null;
            if (!String.IsNullOrEmpty(txtOrcamentoMunicipal.Text))
                obj.SaoPauloSolidarioValorOrcamentoMunicipalBuscaAtiva = Convert.ToDecimal(txtOrcamentoMunicipal.Text);
            obj.SaoPauloSolidarioValorIGDPBFBuscaAtiva = null;
            if (!String.IsNullOrEmpty(txtIGDPBF.Text))
                obj.SaoPauloSolidarioValorIGDPBFBuscaAtiva = Convert.ToDecimal(txtIGDPBF.Text);
            obj.SaoPauloSolidarioValorIGDSUASBuscaAtiva = null;
            if (!String.IsNullOrEmpty(txtIGDSUAS.Text))
                obj.SaoPauloSolidarioValorIGDSUASBuscaAtiva = Convert.ToDecimal(txtIGDSUAS.Text);


            obj.SaoPauloSolidarioOrgaoGestorExecutaBuscaAtiva = null;
            obj.SaoPauloSolidarioCRASExecutaBuscaAtiva = null;
            obj.SaoPauloSolidarioCREASExecutaBuscaAtiva = null;
            obj.SaoPauloSolidarioUnidadePrivadaExecutaBuscaAtiva = null;
            foreach (ListItem i in chkOrgaosExecutores.Items)
            {
                if (i.Selected)
                {
                    if (i.Value == "1")
                        obj.SaoPauloSolidarioOrgaoGestorExecutaBuscaAtiva = true;
                    else if (i.Value == "2")
                        obj.SaoPauloSolidarioCRASExecutaBuscaAtiva = true;
                    else if (i.Value == "3")
                        obj.SaoPauloSolidarioCREASExecutaBuscaAtiva = true;
                    else if (i.Value == "4")
                        obj.SaoPauloSolidarioUnidadePrivadaExecutaBuscaAtiva = true;
                }
            }

            if (obj.PossuiParceriaFormal)
            {
                obj.Parcerias = Parcerias;
            }

            obj.BeneficiarioAtendidoRedeSocioAssistencial = rblIntegracaoRede.SelectedValue == "1";

            obj.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2012 = null;
            obj.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013 = null;
            obj.SaoPauloSolidarioValorFMASAgendaFamilia = null;
            obj.SaoPauloSolidarioValorFNASAgendaFamilia = null;
            obj.SaoPauloSolidarioValorOrcamentoMunicipalAgendaFamilia = null;
            obj.SaoPauloSolidarioValorIGDPBFAgendaFamilia = null;
            obj.SaoPauloSolidarioValorIGDSUASAgendaFamilia = null;
            obj.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia = null;
            obj.SaoPauloSolidarioOrgaoGestorExecutaAgendaFamilia = null;
            obj.SaoPauloSolidarioCRASExecutaAgendaFamilia = null;
            obj.SaoPauloSolidarioCREASExecutaAgendaFamilia = null;
            obj.ParceriasSaoPauloSolidarioAgendaFamilia = new List<SPSolidarioAgendaFamiliaParceriaInfo>();

            if (rblFase.SelectedValue == "2")
            {
                //if (!String.IsNullOrEmpty(txtMeta.Text))
                //    obj.SaoPauloSolidarioMeta = Convert.ToInt32(txtMeta.Text);
                if (!String.IsNullOrEmpty(txtNumeroFamilias.Text))
                    obj.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2012 = Convert.ToInt32(txtNumeroFamilias.Text);
                if (!String.IsNullOrEmpty(txtNumeroFamilias2013.Text))
                    obj.SaoPauloSolidarioNumeroFamiliasAgendaFamilia2013 = Convert.ToInt32(txtNumeroFamilias2013.Text);
                //if (!String.IsNullOrEmpty(txtPrevisaoAnualRepasse.Text))
                //    obj.SaoPauloSolidarioRepasseAnual = Convert.ToDecimal(txtPrevisaoAnualRepasse.Text);

                if (!String.IsNullOrEmpty(txtFMASAgendaFamilia.Text))
                    obj.SaoPauloSolidarioValorFMASAgendaFamilia = Convert.ToDecimal(txtFMASAgendaFamilia.Text);                               

                if (!String.IsNullOrEmpty(txtFNASAgendaFamilia.Text))
                    obj.SaoPauloSolidarioValorFNASAgendaFamilia = Convert.ToDecimal(txtFNASAgendaFamilia.Text);
                if (!String.IsNullOrEmpty(txtOrcamentoMunicipalAgendaFamilia.Text))
                    obj.SaoPauloSolidarioValorOrcamentoMunicipalAgendaFamilia = Convert.ToDecimal(txtOrcamentoMunicipalAgendaFamilia.Text);
                if (!String.IsNullOrEmpty(txtIGDPBFAgendaFamilia.Text))
                    obj.SaoPauloSolidarioValorIGDPBFAgendaFamilia = Convert.ToDecimal(txtIGDPBFAgendaFamilia.Text);
                if (!String.IsNullOrEmpty(txtIGDSUASAgendaFamilia.Text))
                    obj.SaoPauloSolidarioValorIGDSUASAgendaFamilia = Convert.ToDecimal(txtIGDSUASAgendaFamilia.Text);                
                
                foreach (ListItem i in chkOrgaosAgendaFamilia.Items)
                {
                    if (i.Selected)
                    {
                        if (i.Value == "1")
                            obj.SaoPauloSolidarioOrgaoGestorExecutaAgendaFamilia = true;
                        else if (i.Value == "2")
                            obj.SaoPauloSolidarioCRASExecutaAgendaFamilia = true;
                        else if (i.Value == "3")
                            obj.SaoPauloSolidarioCREASExecutaAgendaFamilia = true;
                    }
                }

                obj.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia = rblParceriasAgendaFamilia.SelectedValue == "1";
                if (obj.PossuiParceriaFormalSaoPauloSolidarioAgendaFamilia.Value)
                {
                    obj.ParceriasSaoPauloSolidarioAgendaFamilia = ParceriasAgendaFamilia;
                }
            }

            try
            {
                new ValidadorTransferenciaRenda().Validar(obj);

                using (var proxy = new ProxyProgramas())
                {                    
                    proxy.Service.UpdateTransferenciaRenda(obj);                    
                }
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }            

            Response.Redirect("~/BlocoIII/CTransferenciaRenda.aspx?msg=TU");
        }

        protected void lstParcerias_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluirParceria")) };
                if (!(SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador)                )
                    Permissao.VerificarPermissaoControles(controles, Session);                
            }
        }

        protected void lstParcerias_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            try
            {
                switch (e.CommandName)
                {
                    case "Excluir":
                        if (Parcerias == null || Parcerias.Count == 0)
                            break;
                        Parcerias.RemoveAt(e.Item.DataItemIndex);
                        carregarParcerias();
                        var script = Util.GetJavaScriptDialogOK("Parceria excluída com sucesso!");
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

        protected void lstParceriasAgendaFamilia_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            try
            {
                switch (e.CommandName)
                {
                    case "Excluir":
                        if (ParceriasAgendaFamilia == null || ParceriasAgendaFamilia.Count == 0)
                            break;
                        ParceriasAgendaFamilia.RemoveAt(e.Item.DataItemIndex);
                        carregarParceriasAgendaFamilia();
                        var script = Util.GetJavaScriptDialogOK("Parceria excluída com sucesso!");
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

        protected void btnAdicionarParceria_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var parceria = new TransferenciaRendaParceriaInfo();
            parceria.IdTipoParceria = Convert.ToInt32(ddlTipoParceria.SelectedValue);
            parceria.NomeOrgao = txtNomeOrgao.Text;
            parceria.IdParceria = Convert.ToInt32(ddlParceria.SelectedValue);
            parceria.Parceria = new ParceriaInfo() { Nome = ddlParceria.SelectedItem.Text };
            parceria.TipoParceria = new TipoParceriaInfo() { Nome = ddlTipoParceria.SelectedItem.Text };

            try
            {
                new ValidadorTransferenciaRendaParceria().Validar(parceria);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Parcerias = Parcerias ?? new List<TransferenciaRendaParceriaInfo>();
            Parcerias.Add(parceria);

            carregarParcerias();

            txtNomeOrgao.Text = String.Empty;
            ddlTipoParceria.SelectedIndex = ddlParceria.SelectedIndex = 0;
        }

        protected void btnAdicionarParceriaAgendaFamilia_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var parceria = new SPSolidarioAgendaFamiliaParceriaInfo();
            parceria.IdTipoParceria = Convert.ToInt32(ddlTipoParceriaAgendaFamilia.SelectedValue);
            parceria.NomeOrgao = txtNomeOrgaoAgendaFamilia.Text;
            parceria.IdParceria = Convert.ToInt32(ddlParceriaAgendaFamilia.SelectedValue);
            parceria.Parceria = new ParceriaInfo() { Nome = ddlParceriaAgendaFamilia.SelectedItem.Text };
            parceria.TipoParceria = new TipoParceriaInfo() { Nome = ddlTipoParceriaAgendaFamilia.SelectedItem.Text };

            try
            {
                new ValidadorTransferenciaRendaParceria().Validar(parceria);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            ParceriasAgendaFamilia = ParceriasAgendaFamilia ?? new List<SPSolidarioAgendaFamiliaParceriaInfo>();
            ParceriasAgendaFamilia.Add(parceria);

            carregarParceriasAgendaFamilia();

            txtNomeOrgaoAgendaFamilia.Text = String.Empty;
            ddlTipoParceriaAgendaFamilia.SelectedIndex = ddlParceriaAgendaFamilia.SelectedIndex = 0;

        }       

        protected void rblParcerias_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbParcerias.Visible = rblParcerias.SelectedValue == "1";
        }

        protected void rblParceriasAgendaFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbParceriasAgendaFamilia.Visible = rblParceriasAgendaFamilia.SelectedValue == "1";
        }

        protected void rblFase_SelectedIndexChanged(object sender, EventArgs e)
        {
            trAgendaFamilia.Visible = rblFase.SelectedValue == "2";
            lblOrgaoExecutam.Text = rblFase.SelectedValue == "2" ? "executaram" : "executam";
            lblParceriasExistem.Text = rblFase.SelectedValue == "2" ? "Existiram" : "Existem";
        }
    }
}