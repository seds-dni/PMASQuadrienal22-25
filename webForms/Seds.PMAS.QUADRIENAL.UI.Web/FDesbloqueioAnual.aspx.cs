using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class FDesbloqueioAnual : System.Web.UI.Page
    {
        #region propriedades
        public static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            lblExercicio1.Text = Exercicios[0].ToString();
            lblExercicio2.Text = Exercicios[1].ToString();
            lblExercicio3.Text = Exercicios[2].ToString();
            lblExercicio4.Text = Exercicios[3].ToString();

            lblExercicioReprogramados1.Text = Exercicios[0].ToString();
            lblExercicioReprogramados2.Text = Exercicios[1].ToString();
            lblExercicioReprogramados3.Text = Exercicios[2].ToString();
            lblExercicioReprogramados4.Text = Exercicios[3].ToString();

            lblExercicioDemandasParlamentares1.Text = Exercicios[0].ToString();
            lblExercicioDemandasParlamentares2.Text = Exercicios[1].ToString();
            lblExercicioDemandasParlamentares3.Text = Exercicios[2].ToString();
            lblExercicioDemandasParlamentares4.Text = Exercicios[3].ToString();



            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Perfil == null && SessaoPmas.UsuarioLogado.Perfil != "Administrador")
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                load();

            }
        }
        protected void btnSalvar_BloquearDesbloquearClick(object sender, EventArgs e)
        {
            string msg = string.Empty;

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    if (Convert.ToBoolean(hdnEhdesbloqueio.Value))
                    {
                        proxy.Service.UpdateDesbloquearRecursosIgnorandoOsReprogramadosPrefeituraExercicios(true, Convert.ToInt32(hdnExercicioParaBloquearDesbloquear.Value));
                        msg = "Desbloqueio realizado com sucesso";
                    }
                    else
                    {
                        proxy.Service.UpdateDesbloquearRecursosIgnorandoOsReprogramadosPrefeituraExercicios(false, Convert.ToInt32(hdnExercicioParaBloquearDesbloquear.Value));
                        msg = "Bloqueio realizado com sucesso";
                    }
                }


                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
                if (ex.InnerException != null)
                {
                    lblErro.Text += ex.InnerException.Message;
                }
                return;
            }

        }

         protected void btnSalvar_BloquearDesbloquearReprogramadosClick(object sender, EventArgs e)
        {
            string msg = string.Empty;

            try
            {
                using (var proxy = new ProxyPrefeitura())
                {
                    var prefeituras = new Prefeituras(proxy);
                    if (Convert.ToBoolean(hdnEhdesbloqueio.Value))
                    {
                        proxy.Service.UpdateDesbloquearRecursosReprogramadosPrefeituraExercicios(true, Convert.ToInt32(hdnExercicioParaBloquearDesbloquear.Value));
                        msg = "Desbloqueio realizado com sucesso";
                    }
                    else
                    {
                        proxy.Service.UpdateDesbloquearRecursosReprogramadosPrefeituraExercicios(false, Convert.ToInt32(hdnExercicioParaBloquearDesbloquear.Value));
                        msg = "Bloqueio realizado com sucesso";
                    }
                }


                var script = Util.GetJavaScriptDialogOK(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
                if (ex.InnerException != null)
                {
                    lblErro.Text += ex.InnerException.Message;
                }
                return;
            }

        }

         protected void btnSalvar_BloquearDesbloquearDemandasParlamentaresClick(object sender, ImageClickEventArgs e)
         {

             string msg = string.Empty;

             try
             {
                 using (var proxy = new ProxyPrefeitura())
                 {
                     var prefeituras = new Prefeituras(proxy);
                     if (Convert.ToBoolean(hdnEhdesbloqueio.Value))
                     {
                         proxy.Service.UpdateDesbloquearDemandasParlamentaresPrefeituraExercicios(true, Convert.ToInt32(hdnExercicioParaBloquearDesbloquear.Value));
                         msg = "Desbloqueio realizado com sucesso";
                     }
                     else
                     {
                         proxy.Service.UpdateDesbloquearDemandasParlamentaresPrefeituraExercicios(false, Convert.ToInt32(hdnExercicioParaBloquearDesbloquear.Value));
                         msg = "Bloqueio realizado com sucesso";
                     }
                 }


                 var script = Util.GetJavaScriptDialogOK(msg);
                 ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
             }
             catch (Exception ex)
             {
                 lblErro.Text = ex.Message;
                 if (ex.InnerException != null)
                 {
                     lblErro.Text += ex.InnerException.Message;
                 }
                 return;
             }

         }
      

        void ObterPrefeitura(int idMunicipio)
        {
            using (var proxy = new ProxyPrefeitura())
            {
                PrefeituraInfo prefeitura = proxy.Service.GetByIdMunicipio(idMunicipio);
                List<PrefeituraInfo> lst = new List<PrefeituraInfo>();
                lst.Add(prefeitura);
                var agrupados = lst.Select(x => new
                {
                    Id = x.Id,
                    IdMunicipio = x.IdMunicipio,
                    IdSituacao = x.IdSituacao,
                    Cidade = x.Cidade,
                    Situacao = x.Situacao.Nome,
                    PrefeiturasExerciciosBloqueio = x.PrefeiturasExerciciosBloqueio
                                                     .Where(pref => pref.IdRefBloqueio != 76 && pref.IdRefBloqueio != 78)
                                                     .GroupBy(g => g.IdRefBloqueio)
                                                     .Select(gg => new
                                                     {
                                                         Key = gg.First().IdRefBloqueio
                                                        ,
                                                         Valor = new
                                                             {
                                                                 Exercicios = gg.Select(ggg => new
                                                                 {
                                                                     IdRefBloqueio = gg.First().IdRefBloqueio, 
                                                                     IdPrefeitura = gg.First().IdPrefeitura,
                                                                     Exercicio = ggg.Exercicio,
                                                                     Desbloqueado = ggg.Desbloqueado
                                                                 })
                                                             }
                                                     })


                });


                lstPrefeituras.DataSource = agrupados;
                lstPrefeituras.DataBind();
            }
        }
        void load()
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
                    int idRefBloqueio = Convert.ToInt32(key["IdRefBloqueio"]);
                    bool desbloqueado = Convert.ToBoolean(key["Desbloqueado"]);
                    using (var proxy = new ProxyPrefeitura())
                    {
                        var prefeitura = proxy.Service.GetPrefeituraById(idPrefeitura);
                        if (prefeitura.PrefeiturasExerciciosBloqueio != null)
                        {
                            prefeitura.PrefeiturasExerciciosBloqueio.Single(x => x.IdPrefeitura == idPrefeitura
                                                                                 && x.Exercicio == exercicio
                                                                                 && x.IdRefBloqueio == idRefBloqueio).Desbloqueado = !desbloqueado;
                        }
                        proxy.Service.UpdatePrefeitura(prefeitura, true);
                        ObterPrefeitura(prefeitura.IdMunicipio);
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
                case 10:
                    return "RH do órgão gestor"; //"Bloco 1 - RH do órgão gestor
                case 19:
                    return "Serviços da rede direta"; //"Bloco 3 - Servicos rede direta
                case 20:
                    return "Serviços da rede indireta"; //"Bloco 3 - Servicos rede indireta 
                case 26:
                    return "Cronograma PSB"; //"Bloco 5 - Proteção Social Básica";
                case 27:
                    return "Cronograma PSEMC"; //Bloco 5 - Proteção Social Especial de média complexidade";
                case 28:
                    return "Cronograma PSEAC"; //"Bloco 5 - Proteção Social Especial de alta complexidade";
                case 29:
                    return "Cronograma PP"; //"Bloco 5 - Programa e Projeto";
                case 70:
                    return "Fontes de recursos do FMAS"; //Bloco 5 - Fontes de recursos do FMAS
                case 75:
                    return "Atualização do Diagnóstico"; //"Atualizações Anuais";
                //case 76:
                //    return "Execução Financeira"; //"Bloco 5 - Execução Financeira";
                //case 78:
                //    return "Lei Orçamentária"; //"Bloco 5 - Lei Orçamentária";
                case 22:
                    return "Recursos financeiros do BE"; //"Bloco 3 - Servicos "B"eneficios "E"ventuais
                case 23:
                    return "Programas e Projetos"; //"Bloco 3 - Programa e Projetos";
                case 30:
                    return "Cronograma Benefícios"; //"Bloco 5 - Beneficios Eventuais";
                case 40:
                    return "Início - Parecer Drads"; //"Bloco "Inicio" - Parecer Drads";
                case 25:
                    return "Programa com Transf. Direta de Renda + BPC";
                case 1019:
                    return "Gestor Reprogramacao - Rede Direta"; //"Bloco 3 - Reprogramacao Rede Direta"
                case 1020:
                    return "Gestor Reprogramacao - Rede Indireta"; //"Bloco 3 - Reprogramacao Rede Indireta"
                case 1040:
                    return "Drads Reprogramacao"; //"Bloco "Inicio" - Reprogramacao Parecer Drads";
                case 1041:
                    return "Gestor Demandas Parlamentares - Rede Direta"; //"Bloco 3 - Demandas Parlamentares Rede Direta"
                case 1042:
                    return "Gestor Demandas Parlamentares - Rede Indireta"; //"Bloco 3 - Demandas Parlamentares Rede Indireta"
                case 1043:
                    return "Drads Demandas Parlamentares"; //"Bloco "Inicio" - Demandas Parlamentares Parecer Drads";
                case 1044:
                    return "Recursos Açõe de Planejamento"; //"Bloco "6" - Planejamento";
                default:
                    return string.Empty;
            }
        }


    }
}