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

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class ConsultaFluxoPMASCMAS : System.Web.UI.Page
    {
        #region propriedades
        private static List<int> QuadroExercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        #endregion

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

                load();
            }
        }

        void load()
        {

            lblMunicipio.Text = SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Nome;
            lblSituacao.Text = SessaoPmas.UsuarioLogado.Prefeitura.Situacao.Nome;

            var lst = new List<ConsultaPlanoMunicipalHistoricoInfo>();
            using (var proxy = new ProxyPlanoMunicipal())
            {
                lst = proxy.Service.GetHistoricoPlanoMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id).ToList();
                lstHistorico.DataSource = lst;
                lstHistorico.DataBind();
            }

            

            lnkDevolverOrgaoGestor.Visible = (ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.EmAnalisedoCMAS;
            lnkParecer.Visible = lnkDevolverOrgaoGestor.Visible && lst.Any(t => t.IdSituacao == Convert.ToInt32(ESituacao.Aprovado) || t.IdSituacao == Convert.ToInt32(ESituacao.Rejeitado));

            //Verificação para CMAS
            if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CMAS)
            {
                IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
                IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
                var idUsuario = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault();

                using (var proxyPrefeitura = new ProxyPrefeitura())
                {

                    var presidente = proxyPrefeitura.Service.GetConselhoMunicipalByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    lnkParecer.Visible = presidente != null && presidente.IdUsuarioPresidente == Convert.ToInt32(idUsuario.Value) && (ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.EmAnalisedoCMAS && lst.Any(t => t.IdSituacao == Convert.ToInt32(ESituacao.Aprovado));
                }
            }

            if (!lnkDevolverOrgaoGestor.Visible)
            {
                lblSituacaoFluxo.Visible = true;
                switch ((ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao)
                {
                    case ESituacao.Desbloqueado: lblSituacaoFluxo.Text = "Aguardando preenchimento pelo Órgão Gestor"; break;
                    case ESituacao.DevolvidoDrads:
                    case ESituacao.DevolvidopeloCMAS: lblSituacaoFluxo.Text = "Aguardando preenchimento das alterações pelo Orgão Gestor"; break;
                    case ESituacao.EmAnaliseDrads: lblSituacaoFluxo.Text = "Aguardando retorno da DRADS para o Órgão Gestor"; break;
                    case ESituacao.EmAnalisedoCMAS: lblSituacaoFluxo.Text = "Aguardando parecer"; break;
                    case ESituacao.Parafinalizacao: lblSituacaoFluxo.Text = "Aguardando finalização do plano"; break;
                }
            }

            using (var proxy = new ProxyPrefeitura())
            {

                #region EF: Exercicio 1
                var quadroEFExercicio1 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == ConsultaFluxoPMASCMAS.QuadroExercicios[0]).FirstOrDefault();
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
                var quadroEFExercicio2 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == ConsultaFluxoPMASCMAS.QuadroExercicios[1]).FirstOrDefault();
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
                var quadroEFExercicio3 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == ConsultaFluxoPMASCMAS.QuadroExercicios[2]).FirstOrDefault();
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

                }
                #endregion
            
                #region EF: Exercicio 4
                var quadroEFExercicio4 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 143).Where(x => x.Exercicio == ConsultaFluxoPMASCMAS.QuadroExercicios[3]).FirstOrDefault();
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
                var quadroLOExercicio1 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == ConsultaFluxoPMASCMAS.QuadroExercicios[0]).FirstOrDefault();
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
                var quadroLOExercicio2 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == ConsultaFluxoPMASCMAS.QuadroExercicios[1]).FirstOrDefault();
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
                var quadroLOExercicio3 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == ConsultaFluxoPMASCMAS.QuadroExercicios[2]).FirstOrDefault();
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
                var quadroLOExercicio4 = proxy.Service.GetPrefeituraSituacaoQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 160).Where(x => x.Exercicio == ConsultaFluxoPMASCMAS.QuadroExercicios[3]).FirstOrDefault();
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

        protected void lnkDevolverOrgaoGestor_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlEncode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.DevolvidopeloCMAS).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(SessaoPmas.UsuarioLogado.Prefeitura.Id.ToString())));
        }

        protected void lnkParecer_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlEncode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.Aprovado).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(SessaoPmas.UsuarioLogado.Prefeitura.Id.ToString())));
        }
    }
}