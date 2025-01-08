using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Collections.Generic;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class FBloqueioQuadroFinanceiro : System.Web.UI.Page
    {

        #region Atributos
        public readonly int QUADRO_BLOQUEIO_ADMINISTRATIVO = 7;
        public readonly int QUADRO_PENDENTE = 1;
        #endregion

        #region propriedades
        public static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        #endregion

        #region Resources
        private readonly int ID_LEI_ORCAMENTARIA = 160;
        private readonly int ID_EXECUCAO_FINANCEIRA = 143;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            hdnExercicioEF1.Value = (Exercicios[0]-1).ToString();
            hdnExercicioEF2.Value = (Exercicios[1]-1).ToString();
            hdnExercicioEF3.Value = (Exercicios[2]-1).ToString();
            hdnExercicioEF4.Value = (Exercicios[3]-1).ToString();

            hdnExercicioPC1.Value = (Exercicios[0]-1).ToString();
            hdnExercicioPC2.Value = (Exercicios[1]-1).ToString();
            hdnExercicioPC3.Value = (Exercicios[2]-1).ToString();
            hdnExercicioPC4.Value = (Exercicios[3]-1).ToString();

            lblExercicioPC1.Text = (Exercicios[0] -1).ToString();
            lblExercicioPC2.Text = (Exercicios[1] -1).ToString();
            lblExercicioPC3.Text = (Exercicios[2] -1).ToString();
            lblExercicioPC4.Text = (Exercicios[3] -1).ToString();

            lblExercicioEF1.Text = (Exercicios[0]-1).ToString(); //Para exibicao apenas
            lblExercicioEF2.Text = (Exercicios[1]-1).ToString(); //Para exibicao apenas
            lblExercicioEF3.Text = (Exercicios[2]-1).ToString(); //Para exibicao apenas
            lblExercicioEF4.Text = (Exercicios[3]-1).ToString(); //Para exibicao apenas

            lblExercicioLO1.Text = Exercicios[0].ToString(); //Para exibicao apenas
            lblExercicioLO2.Text = Exercicios[1].ToString(); //Para exibicao apenas
            lblExercicioLO3.Text = Exercicios[2].ToString(); //Para exibicao apenas
            lblExercicioLO4.Text = Exercicios[3].ToString(); //Para exibicao apenas

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Perfil == null && SessaoPmas.UsuarioLogado.Perfil != "Administrador")
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                Load();

            }
        }
        void ObterPrefeitura(int idMunicipio)
        {
            using (var proxy = new ProxyPrefeitura())
            {

                PrefeituraInfo prefeitura = proxy.Service.GetByMunicipioQuadrosFinanceiros(idMunicipio);
                List<PrefeituraInfo> lst = new List<PrefeituraInfo>();
                lst.Add(prefeitura);



                var agrupados = lst.Select(x => new
                {
                    Id = x.Id,
                    IdMunicipio = x.IdMunicipio,
                    IdSituacao = x.IdSituacao,
                    Cidade = x.Cidade,
                    Situacao = x.Situacao.Nome,
                    PrefeiturasSituacoesQuadrosEF = proxy.Service.GetPrefeituraSituacaoQuadro(x.Id, 143).Where(s => s.Exercicio >= 2021)
                    .GroupBy(xx => xx.IdRecurso).Select(g =>
                        new
                        {
                            IdRecurso = g.First().IdRecurso
                        ,
                            Exercicios = g.Select(xxx => new
                            {
                                IdPrefeitura = xxx.IdPrefeitura
                                ,
                                Exercicio = xxx.Exercicio 
                                ,
                                IdRecurso = xxx.IdRecurso
                                ,
                                IdSituacao = xxx.IdSituacaoQuadro
                            })
                        })
                    ,
                    PrefeiturasSituacoesQuadrosLO = proxy.Service.GetPrefeituraSituacaoQuadro(x.Id, 160).Where(s => s.Exercicio >= 2022)
                        .GroupBy(xx => xx.IdRecurso).Select(g =>
                      new
                      {
                          IdRecurso = g.First().IdRecurso
                      ,
                          Exercicios = g.Select(xxx => new
                          {
                              IdPrefeitura = xxx.IdPrefeitura
                              ,
                              Exercicio = xxx.Exercicio
                              ,
                              IdRecurso = xxx.IdRecurso
                              ,
                              IdSituacao = xxx.IdSituacaoQuadro
                          })
                      })
                });


                lstPrefeituras.DataSource = agrupados;
                lstPrefeituras.DataBind();
            }
        }

        private void Load()
        {
            carregarCombos();
        }

        void carregarCombos()
        {
            tdSituacao.Visible = tdLstSituacao.Visible = SessaoPmas.UsuarioLogado.Perfil == "Administrador";

            ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
            ddlMunicipio.DataTextField = "Nome";
            ddlMunicipio.DataValueField = "Id";
            ddlMunicipio.DataBind();
            ddlMunicipio.Items.Insert(0, new ListItem("Selecione", "0", true));
            ddlMunicipio.SelectedIndex = 0;
        }

        protected void btnSalvar_ExecucaoFinanceiraBloquearDesbloquearClick(object sender, EventArgs e)
        {
            int exercicio = Convert.ToInt32(hdnEFExercicio.Value);
            //AlterarSituacaoQuadroEF(Convert.ToBoolean(hdnEFDebloqueado.Value) ? 1 : 0, exercicio);
            AlterarSituacaoQuadroEF(Convert.ToBoolean(hdnEFDebloqueado.Value), exercicio);
            btnBuscar_Click(sender, e);
        }

        protected void btnSalvar_PrestacaoDeContasBloquearDesbloquearClick(object sender, ImageClickEventArgs e)
        {
            int exercicio = Convert.ToInt32(hdnPCExercicio.Value);
            AlteraQaudroSituacaoPC(Convert.ToBoolean(hdnPCDesbloqueado.Value), exercicio);
            btnBuscar_Click(sender, e);
        }

        protected void btnSalvar_LeiOrcamentariaBloquearDesbloquearClick(object sender, EventArgs e)
        {
            int exercicio = Convert.ToInt32(hdnLOExercicio.Value);
            AlterarSituacaoQuadroLO(Convert.ToBoolean(hdnLODebloqueado.Value), exercicio);
            btnBuscar_Click(sender, e);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (ddlMunicipio.SelectedIndex != 0 && ddlMunicipio.SelectedIndex != -1)
            {
                ObterPrefeitura(ddlMunicipio.SelectedIndex);
            }
        }

        protected void lstPrefeiturasRecursos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            ListView recursos = ((ListView)sender);
            var key = recursos.DataKeys[e.Item.DataItemIndex];

           

            switch (e.CommandName)
            {
                case "Bloqueio_Desbloqueio_Recursos":
                    
                  //var prefeitura = proxy.Service.GetPrefeituraById(Convert.ToInt32(key["Id"]));
                  int idPrefeitura = Convert.ToInt32(key["IdPrefeitura"]);
                  int exercicio = Convert.ToInt32(key["Exercicio"]);
                  int idRecurso = Convert.ToInt32(key["IdRecurso"]);
                  int idSituacao = Convert.ToInt32(key["IdSituacao"]);

                        using (var proxy = new ProxyPrefeitura())
                        {
                            var prefeitura = proxy.Service.GetPrefeituraById(idPrefeitura);
                            if (prefeitura.PrefeiturasExerciciosBloqueio != null)
                            {
                                var quadroEspecifico = proxy.Service.GetPrefeituraSituacaoQuadro(idPrefeitura, idRecurso);
                                if (quadroEspecifico != null)
                                {
                                    var quadroEspecificoPorExercicio = quadroEspecifico.FirstOrDefault(x => x.Exercicio == exercicio);
                                    if (quadroEspecificoPorExercicio != null)
                                    {
                                        quadroEspecificoPorExercicio.IdSituacaoQuadro = (idSituacao == QUADRO_PENDENTE) ? QUADRO_BLOQUEIO_ADMINISTRATIVO : QUADRO_PENDENTE;
                                        proxy.Service.UpdatePrefeitura(prefeitura, true);
                                        ObterPrefeitura(prefeitura.IdMunicipio);
                                    }
                                }
                            }
                        }


                    break;
                default:
                    break;
            }
        }
        protected string MontarRecursosLabel(string idRecurso)
        {
            switch (Convert.ToInt32(idRecurso))
            {
                case 143:
                    return "Execução Financeira"; //"Bloco 5 - Execução Financeira";
                case 160:
                    return "Lei Orçamentária"; //"Bloco 5 - Lei Orçamentária";

                default:
                    return string.Empty;
            }
        }


        void AlterarSituacaoQuadroEF(bool desbloquear, int exercicio)
        {
            String msg = String.Empty;
           
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                        proxy.Service.SavePrefeiturasSituacoesQuadrosEFLO(143, desbloquear ? QUADRO_PENDENTE : QUADRO_BLOQUEIO_ADMINISTRATIVO, exercicio);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                if (desbloquear)
                {
                    msg = "Desbloqueio efetuado com sucesso!";
                }
                else
                {
                    msg = "Bloqueio efetuado com sucesso!";
                }

                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
        }

        void AlteraQaudroSituacaoPC(bool desbloquear, int exercicio) 
        {
            String msg = String.Empty;
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    proxy.Service.SavePrefeiturasSituacoesQuadrosEFLO(168, desbloquear ? QUADRO_PENDENTE : QUADRO_BLOQUEIO_ADMINISTRATIVO, exercicio);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                if (desbloquear)
                {
                    msg = "Desbloqueio efetuado com sucesso!";
                }
                else
                {
                    msg = "Bloqueio efetuado com sucesso!";
                }

                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
        }

        void AlterarSituacaoQuadroLO(bool desbloquear, int exercicio)
        {
            String msg = String.Empty;
            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    proxy.Service.SavePrefeiturasSituacoesQuadrosEFLO(160, desbloquear ? QUADRO_PENDENTE : QUADRO_BLOQUEIO_ADMINISTRATIVO, exercicio);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                if (desbloquear)
                {
                    msg = "Desbloqueio efetuado com sucesso!";
                }
                else
                {
                    msg = "Bloqueio efetuado com sucesso!";
                }

                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(msg), true);
        }



    }
}