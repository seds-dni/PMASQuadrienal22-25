using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoII
{
    public partial class FAtualizacaoDiagnostica : System.Web.UI.Page
    {
        #region propriedades
        private static List<int> Exercicios = new List<int> { 2022, 2023, 2024 , 2025};
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

                using (var proxy = new ProxyPrefeitura())
                {
                    loadCaracterizacao(proxy);
                }
            }
            this.AplicarRegraBloqueioDesbloqueio();
        }

        private void AplicarRegraBloqueioDesbloqueio()
        {
            WebControl[] controlesExercicio1 = { txtAtualizacaoExercicio1 };
            var desbloqueadoExercicio1 = Permissao.BlocoII.VerificaPermissaoExercicioBlocoII(controlesExercicio1, Exercicios[1]);

            WebControl[] controlesExercicio2 = { txtAtualizacaoExercicio2 };
            var desbloqueadoExercicio2 = Permissao.BlocoII.VerificaPermissaoExercicioBlocoII(controlesExercicio2, Exercicios[2]);

            WebControl[] controlesExercicio3 = { txtAtualizacaoExercicio3 };
            var desbloqueadoExercicio3 = Permissao.BlocoII.VerificaPermissaoExercicioBlocoII(controlesExercicio3, Exercicios[3]);

            if (desbloqueadoExercicio1 || desbloqueadoExercicio2 || desbloqueadoExercicio3)
            {
                btnSalvarAtualizacoes.Enabled = true;
            }
            else
            {
                btnSalvarAtualizacoes.Enabled = false;
            }

        }

        void loadCaracterizacao(ProxyPrefeitura proxy)
        {
            var prefeitura = proxy.Service.GetPrefeituraById(SessaoPmas.UsuarioLogado.Prefeitura.Id);

            if (prefeitura == null)
            {
                return;
            }

            if (prefeitura.PrefeituraAtualizacoesAnuais == null)
            {
                return;
            }

            foreach (var atualizacaoAnual in prefeitura.PrefeituraAtualizacoesAnuais)
            {
                string atualizacaoTexto = !String.IsNullOrEmpty(atualizacaoAnual.AtualizacaoAnual) ? atualizacaoAnual.AtualizacaoAnual : String.Empty;

                if (atualizacaoAnual.Exercicio.Value == Exercicios[1])
                {
                    txtAtualizacaoExercicio1.Text = atualizacaoTexto;
                }

                if (atualizacaoAnual.Exercicio.Value == Exercicios[2])
                {
                    txtAtualizacaoExercicio2.Text = atualizacaoTexto;
                }

                if (atualizacaoAnual.Exercicio.Value == Exercicios[3])
                {
                    txtAtualizacaoExercicio3.Text = atualizacaoTexto;
                }
            }        
        }

        protected void btnSalvarAtualizacoes_Click(object sender, EventArgs e)
        {
            var msg = String.Empty;
           
            try
            {

                using (var proxy = new ProxyPrefeitura())
                {
                    var blocoI = new Seds.PMAS.QUADRIENAL.UI.Processos.Prefeituras(proxy);

                    PrefeituraInfo prefeitura = blocoI.GetPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);

                    WebControl[] controlesExercicio1 = { txtAtualizacaoExercicio1 };
                    var desbloqueadoExercicio1 = Permissao.BlocoII.VerificaPermissaoExercicioBlocoII(controlesExercicio1, Exercicios[1]);

                    WebControl[] controlesExercicio2 = { txtAtualizacaoExercicio2 };
                    var desbloqueadoExercicio2 = Permissao.BlocoII.VerificaPermissaoExercicioBlocoII(controlesExercicio2, Exercicios[2]);

                    WebControl[] controlesExercicio3 = { txtAtualizacaoExercicio3 };
                    var desbloqueadoExercicio3 = Permissao.BlocoII.VerificaPermissaoExercicioBlocoII(controlesExercicio3, Exercicios[3]);

                    if (desbloqueadoExercicio1)
                    {
                        PrefeituraAtualizacaoAnualInfo prefeituraAtualizacaoAnualInfoExercicio1 = new PrefeituraAtualizacaoAnualInfo();
                        prefeituraAtualizacaoAnualInfoExercicio1.IdPrefeitura = prefeitura.Id;
                        prefeituraAtualizacaoAnualInfoExercicio1.IdSituacao = (int)ESituacao.Desbloqueado;
                        prefeituraAtualizacaoAnualInfoExercicio1.Exercicio = FAtualizacaoDiagnostica.Exercicios[1];
                        prefeituraAtualizacaoAnualInfoExercicio1.AtualizacaoAnual = txtAtualizacaoExercicio1.Text;
                        blocoI.SavePrefeituraAtualizacaoAnual(prefeituraAtualizacaoAnualInfoExercicio1, true);
                    }
                    if (desbloqueadoExercicio2)
                    {
                        PrefeituraAtualizacaoAnualInfo prefeituraAtualizacaoAnualInfoExercicio2 = new PrefeituraAtualizacaoAnualInfo();
                        prefeituraAtualizacaoAnualInfoExercicio2.IdPrefeitura = prefeitura.Id;
                        prefeituraAtualizacaoAnualInfoExercicio2.IdSituacao = (int)ESituacao.Desbloqueado;
                        prefeituraAtualizacaoAnualInfoExercicio2.Exercicio = FAtualizacaoDiagnostica.Exercicios[2];
                        prefeituraAtualizacaoAnualInfoExercicio2.AtualizacaoAnual = txtAtualizacaoExercicio2.Text;
                        blocoI.SavePrefeituraAtualizacaoAnual(prefeituraAtualizacaoAnualInfoExercicio2, true);
                    }

                    if (desbloqueadoExercicio3)
                    {
                        PrefeituraAtualizacaoAnualInfo prefeituraAtualizacaoAnualInfoExercicio3 = new PrefeituraAtualizacaoAnualInfo();
                        prefeituraAtualizacaoAnualInfoExercicio3.IdPrefeitura = prefeitura.Id;
                        prefeituraAtualizacaoAnualInfoExercicio3.IdSituacao = (int)ESituacao.Desbloqueado;
                        prefeituraAtualizacaoAnualInfoExercicio3.Exercicio = FAtualizacaoDiagnostica.Exercicios[3];
                        prefeituraAtualizacaoAnualInfoExercicio3.AtualizacaoAnual = txtAtualizacaoExercicio3.Text;
                        blocoI.SavePrefeituraAtualizacaoAnual(prefeituraAtualizacaoAnualInfoExercicio3, true);
                    }

                    //EXERCICIO 4 NAO TEM
                    //EXERCICIO 4 NAO TEM
                    //EXERCICIO 4 NAO TEM
                    //EXERCICIO 4 NAO TEM

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Atualização Anual atualizada com sucesso!"), true);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }


        }


    }

}
