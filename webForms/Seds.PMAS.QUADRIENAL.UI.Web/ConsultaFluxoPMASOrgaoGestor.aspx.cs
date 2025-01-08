using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Genericos;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Microsoft.IdentityModel.Claims;
using System.Threading;
using Seds.PMAS.QUADRIENAL.Pendencia;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class ConsultaFluxoPMASOrgaoGestor : System.Web.UI.Page
    {
        #region propriedades
        private static List<int> OrgaoGestorExercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);
                if(Request.QueryString["msg"] == "OK")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Plano finalizado com sucesso!"), true);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                load();             
            }
        }

        void load()
        {
            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault();

            if ((ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.Parafinalizacao)
            {
                GestorMunicipalInfo gestor;

                using (var proxy = new ProxyPrefeitura())
                {
                    gestor = proxy.Service.GetAtualGestorMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                }
                
                lnkFinalizar.Visible = gestor != null && gestor.IdUsuarioGestor == Convert.ToInt32(id.Value);

                lnkEnviarDrads.Visible = false;
            }

            lblMunicipio.Text = SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Nome;
            lblSituacao.Text = SessaoPmas.UsuarioLogado.Prefeitura.Situacao.Nome;            
           
             
            //lnkEnviarDrads.Visible = (ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.DevolvidoDrads
            //    || (ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.DevolvidopeloCMAS
            //    || (ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.Desbloqueado;
            if ((ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.DevolvidoDrads
                || (ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.DevolvidopeloCMAS
                || (ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.Desbloqueado)
            {
                GestorMunicipalInfo gestor;

                using (var proxy = new ProxyPrefeitura())
                {
                    gestor = proxy.Service.GetAtualGestorMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                }

                lnkEnviarDrads.Visible = gestor != null && gestor.IdUsuarioGestor == Convert.ToInt32(id.Value);
            }

            if(!lnkFinalizar.Visible && !lnkEnviarDrads.Visible)
            {
                lblSituacaoFluxo.Visible = true;
                switch((ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao){
                    case ESituacao.Desbloqueado: lblSituacaoFluxo.Text = "Em preenchimento"; break;
                    case ESituacao.DevolvidoDrads:
                    case ESituacao.DevolvidopeloCMAS: lblSituacaoFluxo.Text = "Em preenchimento das alterações"; break;
                    case ESituacao.EmAnaliseDrads: lblSituacaoFluxo.Text = "Aguardando retorno da DRADS"; break;
                    case ESituacao.EmAnalisedoCMAS: lblSituacaoFluxo.Text = "Aguardando análise do CMAS"; break;
                    case ESituacao.Parafinalizacao: lblSituacaoFluxo.Text = "Somente o Gestor Municipal pode finalizar o plano"; break;
                    case ESituacao.AutorizaDesbloqueioGestor: lblSituacaoFluxo.Text = "Aguardando liberação de desbloqueio pela DRADS"; break;
                }
            }

            using (var proxy = new ProxyPlanoMunicipal())
            {
                lstHistorico.DataSource = proxy.Service.GetHistoricoPlanoMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                lstHistorico.DataBind();
            }

            using (var proxy = new ProxyPrefeitura())
            {

                #region EF: Exercicio 1
                var quadroEFExercicio1 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == ConsultaFluxoPMASOrgaoGestor.OrgaoGestorExercicios[0] -1).FirstOrDefault();
                if (quadroEFExercicio1 != null)
                {
                    switch (quadroEFExercicio1.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            lblSituacaoQuadroEFExercicio1.Text = "Pendente";
                            break;
                        case (int)ESituacaoQuadro.Preenchido:
                            lblSituacaoQuadroEFExercicio1.Text = "Preenchido";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            lblSituacaoQuadroEFExercicio1.Text = "Em análise CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            lblSituacaoQuadroEFExercicio1.Text = "Aprovado CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoDRADS:
                            lblSituacaoQuadroEFExercicio1.Text = "Devolvido DRADS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            lblSituacaoQuadroEFExercicio1.Text = "Devolvido CMAS";
                            break;
                    }
                }
                else
                {
                    lblSituacaoQuadroEFExercicio1.Text = "Pendente";
                }
                #endregion

                #region EF: Exercicio 2
                var quadroEFExercicio2 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == ConsultaFluxoPMASOrgaoGestor.OrgaoGestorExercicios[1] -1 ).FirstOrDefault();
                if (quadroEFExercicio2 != null)
                {
                    switch (quadroEFExercicio2.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            lblSituacaoQuadroEFExercicio2.Text = "Pendente";
                            break;
                        case (int)ESituacaoQuadro.Preenchido:
                            lblSituacaoQuadroEFExercicio2.Text = "Preenchido";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            lblSituacaoQuadroEFExercicio2.Text = "Em análise CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            lblSituacaoQuadroEFExercicio2.Text = "Aprovado CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoDRADS:
                            lblSituacaoQuadroEFExercicio2.Text = "Devolvido DRADS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            lblSituacaoQuadroEFExercicio2.Text = "Devolvido CMAS";
                            break;
                    }
                }
                else
                {
                    lblSituacaoQuadroEFExercicio2.Text = "Pendente";
                } 
                #endregion

                #region EF: Exercicio 3
                var quadroEFExercicio3 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == ConsultaFluxoPMASOrgaoGestor.OrgaoGestorExercicios[2] - 1).FirstOrDefault();
                if (quadroEFExercicio3 != null)
                {
                    switch (quadroEFExercicio3.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            lblSituacaoQuadroEFExercicio3.Text = "Em Preenchimento";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            lblSituacaoQuadroEFExercicio3.Text = "Em análise pelo CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            lblSituacaoQuadroEFExercicio3.Text = "Aprovado pelo CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            lblSituacaoQuadroEFExercicio3.Text = "Devolvido CMAS";
                            break;
                        case (int)ESituacaoQuadro.RejeitadoCMAS:
                            lblSituacaoQuadroEFExercicio3.Text = "Rejeitado pelo CMAS";
                            break;
                        default:
                            lblSituacaoQuadroEFExercicio3.Text = "Bloqueado";
                            break;
                    }
                }
                else
                {
                    lblSituacaoQuadroEFExercicio3.Text = "Bloqueado";
                }
                #endregion

                #region EF: Exercicio 4
                var quadroEFExercicio4 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == ConsultaFluxoPMASOrgaoGestor.OrgaoGestorExercicios[3] - 1).FirstOrDefault();
                if (quadroEFExercicio4 != null)
                {
                    switch (quadroEFExercicio4.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            lblSituacaoQuadroEFExercicio4.Text = "Em Preenchimento";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            lblSituacaoQuadroEFExercicio4.Text = "Em análise pelo CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            lblSituacaoQuadroEFExercicio4.Text = "Aprovado pelo CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            lblSituacaoQuadroEFExercicio4.Text = "Devolvido CMAS";
                            break;
                        case (int)ESituacaoQuadro.RejeitadoCMAS:
                            lblSituacaoQuadroEFExercicio4.Text = "Rejeitado pelo CMAS";
                            break;
                        default:
                            lblSituacaoQuadroEFExercicio4.Text = "Bloqueado";
                            break;
                    }
                }
                else
                {
                    lblSituacaoQuadroEFExercicio4.Text = "Bloqueado";
                }
                #endregion

                #region LO: Exercicio 1
                var quadroLOExercicio1 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == ConsultaFluxoPMASOrgaoGestor.OrgaoGestorExercicios[0]).FirstOrDefault();
                if (quadroLOExercicio1 != null)
                {
                    switch (quadroLOExercicio1.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            lblSituacaoQuadroLOExercicio1.Text = "Pendente";
                            break;
                        case (int)ESituacaoQuadro.Preenchido:
                            lblSituacaoQuadroLOExercicio1.Text = "Preenchido";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            lblSituacaoQuadroLOExercicio1.Text = "Em análise CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            lblSituacaoQuadroLOExercicio1.Text = "Aprovado CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoDRADS:
                            lblSituacaoQuadroLOExercicio1.Text = "Devolvido DRADS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            lblSituacaoQuadroLOExercicio1.Text = "Devolvido CMAS";
                            break;
                    }
                }
                else
                {
                    lblSituacaoQuadroLOExercicio1.Text = "Pendente";
                } 
                #endregion

                #region LO: Exercicio 2
                var quadroLOExercicio2 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == ConsultaFluxoPMASOrgaoGestor.OrgaoGestorExercicios[1]).FirstOrDefault();
                if (quadroLOExercicio2 != null)
                {
                    switch (quadroLOExercicio2.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            lblSituacaoQuadroLOExercicio2.Text = "Pendente";
                            break;
                        case (int)ESituacaoQuadro.Preenchido:
                            lblSituacaoQuadroLOExercicio2.Text = "Preenchido";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            lblSituacaoQuadroLOExercicio2.Text = "Em análise CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            lblSituacaoQuadroLOExercicio2.Text = "Aprovado CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoDRADS:
                            lblSituacaoQuadroLOExercicio2.Text = "Devolvido DRADS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            lblSituacaoQuadroLOExercicio2.Text = "Devolvido CMAS";
                            break;
                    }
                }
                else
                {
                    lblSituacaoQuadroLOExercicio2.Text = "Pendente";
                }
                #endregion

                #region LO: Exercicio 3
                var quadroLOExercicio3 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == ConsultaFluxoPMASOrgaoGestor.OrgaoGestorExercicios[2]).FirstOrDefault();
                if (quadroLOExercicio3 != null)
                {
                    switch (quadroLOExercicio3.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            lblSituacaoQuadroLOExercicio3.Text = "Pendente";
                            break;
                        case (int)ESituacaoQuadro.Preenchido:
                            lblSituacaoQuadroLOExercicio3.Text = "Preenchido";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            lblSituacaoQuadroLOExercicio3.Text = "Em análise CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            lblSituacaoQuadroLOExercicio3.Text = "Aprovado CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoDRADS:
                            lblSituacaoQuadroLOExercicio3.Text = "Devolvido DRADS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            lblSituacaoQuadroLOExercicio3.Text = "Devolvido CMAS";
                            break;
                    }
                }
                else
                {
                    lblSituacaoQuadroLOExercicio3.Text = "Pendente";
                } 
                #endregion

                #region LO: Exercicio 4
                var quadroLOExercicio4 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == ConsultaFluxoPMASOrgaoGestor.OrgaoGestorExercicios[3]).FirstOrDefault();
                if (quadroLOExercicio4 != null)
                {
                    switch (quadroLOExercicio4.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            lblSituacaoQuadroLOExercicio4.Text = "Pendente";
                            break;
                        case (int)ESituacaoQuadro.Preenchido:
                            lblSituacaoQuadroLOExercicio4.Text = "Preenchido";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            lblSituacaoQuadroLOExercicio4.Text = "Em análise CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            lblSituacaoQuadroLOExercicio4.Text = "Aprovado CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoDRADS:
                            lblSituacaoQuadroLOExercicio4.Text = "Devolvido DRADS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            lblSituacaoQuadroLOExercicio4.Text = "Devolvido CMAS";
                            break;
                    }
                }
                else
                {
                    lblSituacaoQuadroLOExercicio4.Text = "Pendente";
                } 
                #endregion

            }
        }                  

        protected void lnkEnviarDrads_Click(object sender, EventArgs e)
        {
            trComentariosOrgaoGestor.Visible = true;
            lnkEnviarDrads.Visible = false;
        }

        protected void lnkFinalizar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            using (var proxy = new ProxyPlanoMunicipal())
            {
                proxy.Service.FinalizarPlanoMunicipal(SessaoPmas.UsuarioLogado.Prefeitura.Id, "");
            }

            Util.CarregarPrefeitura();

            Response.Redirect("~/ConsultaFluxoPMASOrgaoGestor.aspx?msg=OK");                       
        }

        protected void btnEnviarDrads_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtComentariosOrgaoGestor.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("O campo de comentário deve ser preenchido!"), true);
                    return;
                }

                if (txtComentariosOrgaoGestor.Text.Length > 3000)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("O campo de comentário não deve ultrapassar 3000 caracteres!"), true);
                    return;
                }

                SessaoPmas.VerificarSessao(this);

                using (var proxy = new ProxyPlanoMunicipal())
                {
                    //Removido a pedido do João DER 16-08-2019
                    //if (new VerificadorPendenciaPMAS().PlanoMunicipalPossuiPendencia(SessaoPmas.UsuarioLogado.Prefeitura.Id, EPerfil.OrgaoGestor))
                    //{
                    //    throw new Exception("O Plano Municipal possui pendências!");
                    //}
                    //else
                    //{
                        proxy.Service.EnviarPlanoMunicipalParaDrads(SessaoPmas.UsuarioLogado.Prefeitura.Id, txtComentariosOrgaoGestor.Text, EPerfil.OrgaoGestor);
                    //}
                    
                }

                Util.CarregarPrefeitura();

                load();
                this.Master.CarregarDadosPlano();
                lnkEnviarDrads.Visible = false;
                trComentariosOrgaoGestor.Visible = false;

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Plano enviado para Drads com sucesso!"), true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
            }
        }

        protected void btnCancelarEnvioDrads_Click(object sender, EventArgs e)
        {
            lnkEnviarDrads.Visible = true;
            trComentariosOrgaoGestor.Visible = false;
        }
    }
}