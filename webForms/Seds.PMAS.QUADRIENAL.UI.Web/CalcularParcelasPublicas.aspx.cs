using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class CalcularParcelas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.IdUsuario != 6180)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                List<parcelas> lstparcelas = new List<parcelas>();
                List<parcelas> lstparcelasprivadas = new List<parcelas>();
                using (var proxy = new ProxyPrefeitura())
                {

                    //carregar as prefeituras em uma lista
                    var prefeituras = proxy.Service.GetPrefeituras();

                    ////foreach para resgatar o confinanciamento e fazer o calculo das parcelas
                    foreach (var item in prefeituras)
                    {
                        int idPrefeitura = item.Id;
                        //filtrar por Tipo de proteção social
                        using (var proxyAssistenciais = new ProxyEstruturaAssistenciaSocial())
                        {
                            var tipoprotecao = proxyAssistenciais.Service.GetTiposProtecaoSocial();
                            foreach (var tipo in tipoprotecao)
                            {
                                if (tipo.Id != 1)
                                {

                                    var lst = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, tipo.Id, 2018).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);
                                    decimal totalpublica = Convert.ToDecimal(lst.Where(t => t.IdTipoUnidade != 2).Sum(t => t.PrevisaoOrcamentaria).ToString("n2"));

                                    var objparcelas = RetornarParcelasPublica(totalpublica, idPrefeitura, tipo.Id);
                                    lstparcelas.Add(objparcelas);
                                }
                            }
                        }
                    }
                }
                listParcelasPublicas.DataSource = lstparcelas;
                listParcelasPublicas.DataBind();
            }
        }

        public parcelas RetornarParcelasPublica(decimal varValor, int idPrefeitura, int idProtecao)
        {
            //lstparcelas = new List<parcelas>();
            parcelas objparcelas = new parcelas();

            decimal varValorPrimeiraParcela;
            decimal varValorRestante;
            decimal varValorParcela;
            decimal divisivel;

            var cronogramaPublica = new CronogramaDesembolsoInfo();
            cronogramaPublica.IdPrefeitura = idPrefeitura;
            cronogramaPublica.IdTipoProtecaoSocial = Convert.ToInt16(idProtecao);

            // lblTotalPublica.Text = varValor.ToString("n2");
            // calculo da divisão por 12
            divisivel = varValor / 12;
            // condição para ver se divisivel ou não

            objparcelas.idPrefeitura = idPrefeitura;
            objparcelas.idTipoProtecao = idProtecao;

            if (divisivel % 2 == 0 || divisivel % 2 == 1)
            {
                varValorParcela = varValor / 12;

                objparcelas.valorServicosTerceirosMes1 = varValorParcela;
                objparcelas.valorServicosTerceirosMes2 = varValorParcela;
                objparcelas.valorServicosTerceirosMes3 = varValorParcela;
                objparcelas.valorServicosTerceirosMes4 = varValorParcela;
                objparcelas.valorServicosTerceirosMes5 = varValorParcela;
                objparcelas.valorServicosTerceirosMes6 = varValorParcela;
                objparcelas.valorServicosTerceirosMes7 = varValorParcela;
                objparcelas.valorServicosTerceirosMes8 = varValorParcela;
                objparcelas.valorServicosTerceirosMes9 = varValorParcela;
                objparcelas.valorServicosTerceirosMes10 = varValorParcela;
                objparcelas.valorServicosTerceirosMes11 = varValorParcela;
                objparcelas.valorServicosTerceirosMes12 = varValorParcela;


                //cronogramaPublica.ValorServicosTerceirosMes1 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes2 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes3 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes4 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes5 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes6 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes7 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes8 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes9 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes10 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes11 = varValorParcela;
                //cronogramaPublica.ValorServicosTerceirosMes12 = varValorParcela;

            }
            else
            {
                decimal valor = decimal.Round(varValor, 0);

                //Recupero a dízima
                var dizima = varValor.ToString().Split(',')[1].ToCharArray();

                //verifico se a dzima é maior que 0
                if (int.Parse(dizima[0].ToString()) > 0 || int.Parse(dizima[1].ToString()) > 0)
                {
                    varValorParcela = valor / 12;
                    varValorRestante = Math.Round(varValorParcela, 0) * 11;
                    // varValorPrimeiraParcela = varValor - varValorRestante;
                    varValorPrimeiraParcela = varValor - decimal.Round(varValorParcela, 2) * 11;
                    if (varValorParcela < varValorPrimeiraParcela)
                    {
                        objparcelas.valorServicosTerceirosMes1 = varValorPrimeiraParcela;
                        objparcelas.valorServicosTerceirosMes2 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes3 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes4 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes5 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes6 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes7 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes8 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes9 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes10 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes11 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes12 = Math.Round(varValorParcela, 2);

                        //cronogramaPublica.ValorServicosTerceirosMes1 = varValorPrimeiraParcela;
                        //cronogramaPublica.ValorServicosTerceirosMes2 = Math.Round(varValorParcela, 2);
                        //cronogramaPublica.ValorServicosTerceirosMes3 = Math.Round(varValorParcela, 2);
                        //cronogramaPublica.ValorServicosTerceirosMes4 = Math.Round(varValorParcela, 2);
                        //cronogramaPublica.ValorServicosTerceirosMes5 = Math.Round(varValorParcela, 2);
                        //cronogramaPublica.ValorServicosTerceirosMes6 = Math.Round(varValorParcela, 2);
                        //cronogramaPublica.ValorServicosTerceirosMes7 = Math.Round(varValorParcela, 2);
                        //cronogramaPublica.ValorServicosTerceirosMes8 = Math.Round(varValorParcela, 2);
                        //cronogramaPublica.ValorServicosTerceirosMes9 = Math.Round(varValorParcela, 2);
                        //cronogramaPublica.ValorServicosTerceirosMes10 = Math.Round(varValorParcela, 2);
                        //cronogramaPublica.ValorServicosTerceirosMes11 = Math.Round(varValorParcela, 2);
                        //cronogramaPublica.ValorServicosTerceirosMes12 = Math.Round(varValorParcela, 2);
                    }
                    else
                    {

                        objparcelas.valorServicosTerceirosMes1 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes2 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes3 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes4 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes5 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes6 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes7 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes8 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes9 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes10 = Math.Round(varValorParcela, 2);
                        objparcelas.valorServicosTerceirosMes11 = Math.Round(varValorParcela, 2); ;
                        objparcelas.valorServicosTerceirosMes12 = varValorPrimeiraParcela;
                    }
                }
                else
                {

                    varValorParcela = decimal.Round(varValor, 2) / 12;
                    varValorParcela = varValorParcela - 0.01m;
                    varValorPrimeiraParcela = varValor - decimal.Round(varValorParcela, 2) * 11;

                    objparcelas.valorServicosTerceirosMes1 = varValorPrimeiraParcela;
                    objparcelas.valorServicosTerceirosMes2 = Math.Round(varValorParcela, 2);
                    objparcelas.valorServicosTerceirosMes3 = Math.Round(varValorParcela, 2);
                    objparcelas.valorServicosTerceirosMes4 = Math.Round(varValorParcela, 2);
                    objparcelas.valorServicosTerceirosMes5 = Math.Round(varValorParcela, 2);
                    objparcelas.valorServicosTerceirosMes6 = Math.Round(varValorParcela, 2);
                    objparcelas.valorServicosTerceirosMes7 = Math.Round(varValorParcela, 2);
                    objparcelas.valorServicosTerceirosMes8 = Math.Round(varValorParcela, 2);
                    objparcelas.valorServicosTerceirosMes9 = Math.Round(varValorParcela, 2);
                    objparcelas.valorServicosTerceirosMes10 = Math.Round(varValorParcela, 2);
                    objparcelas.valorServicosTerceirosMes11 = Math.Round(varValorParcela, 2);
                    objparcelas.valorServicosTerceirosMes12 = Math.Round(varValorParcela, 2);

                }


            }
            return objparcelas;
        }

        public void GravarParcelasPublica(decimal varValor, int idPrefeitura, int idProtecao)
        {
            var msg = String.Empty;
            int exercicio = 2022;

            var cronogramaPublica = new CronogramaDesembolsoInfo();
            cronogramaPublica.IdPrefeitura = idPrefeitura;
            cronogramaPublica.IdTipoUnidade = 1;
            cronogramaPublica.IdTipoProtecaoSocial = Convert.ToInt16(idProtecao);

            if (cronogramaPublica.IdTipoProtecaoSocial != 1)
            {
                ////lstparcelas = new List<parcelas>();
                //parcelas objparcelas = new parcelas();

                decimal varValorPrimeiraParcela;
                decimal varValorRestante;
                decimal varValorParcela;
                decimal divisivel;


                // lblTotalPublica.Text = varValor.ToString("n2");
                // calculo da divisão por 12
                divisivel = varValor / 12;
                // condição para ver se divisivel ou não

                if (divisivel % 2 == 0 || divisivel % 2 == 1)
                {
                    varValorParcela = varValor / 12;

                    cronogramaPublica.ValorServicosTerceirosMes1 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes2 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes3 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes4 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes5 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes6 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes7 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes8 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes9 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes10 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes11 = varValorParcela;
                    cronogramaPublica.ValorServicosTerceirosMes12 = varValorParcela;

                }
                else
                {
                    decimal valor = decimal.Round(varValor, 0);

                    //Recupero a dízima
                    var dizima = varValor.ToString().Split(',')[1].ToCharArray();

                    //verifico se a dzima é maior que 0
                    if (int.Parse(dizima[0].ToString()) > 0 || int.Parse(dizima[1].ToString()) > 0)
                    {
                        varValorParcela = valor / 12;
                        varValorRestante = Math.Round(varValorParcela, 0) * 11;
                        // varValorPrimeiraParcela = varValor - varValorRestante;
                        varValorPrimeiraParcela = varValor - decimal.Round(varValorParcela, 2) * 11;
                        if (varValorParcela < varValorPrimeiraParcela)
                        {
                            cronogramaPublica.ValorServicosTerceirosMes1 = varValorPrimeiraParcela;
                            cronogramaPublica.ValorServicosTerceirosMes2 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes3 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes4 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes5 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes6 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes7 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes8 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes9 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes10 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes11 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes12 = Math.Round(varValorParcela, 2);
                        }
                        else
                        {

                            cronogramaPublica.ValorServicosTerceirosMes1 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes2 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes3 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes4 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes5 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes6 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes7 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes8 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes9 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes10 = Math.Round(varValorParcela, 2);
                            cronogramaPublica.ValorServicosTerceirosMes11 = Math.Round(varValorParcela, 2); ;
                            cronogramaPublica.ValorServicosTerceirosMes12 = varValorPrimeiraParcela;
                        }
                    }
                    else
                    {

                        varValorParcela = decimal.Round(varValor, 2) / 12;
                        varValorParcela = varValorParcela - 0.01m;
                        varValorPrimeiraParcela = varValor - decimal.Round(varValorParcela, 2) * 11;

                        cronogramaPublica.ValorServicosTerceirosMes1 = varValorPrimeiraParcela;
                        cronogramaPublica.ValorServicosTerceirosMes2 = Math.Round(varValorParcela, 2);
                        cronogramaPublica.ValorServicosTerceirosMes3 = Math.Round(varValorParcela, 2);
                        cronogramaPublica.ValorServicosTerceirosMes4 = Math.Round(varValorParcela, 2);
                        cronogramaPublica.ValorServicosTerceirosMes5 = Math.Round(varValorParcela, 2);
                        cronogramaPublica.ValorServicosTerceirosMes6 = Math.Round(varValorParcela, 2);
                        cronogramaPublica.ValorServicosTerceirosMes7 = Math.Round(varValorParcela, 2);
                        cronogramaPublica.ValorServicosTerceirosMes8 = Math.Round(varValorParcela, 2);
                        cronogramaPublica.ValorServicosTerceirosMes9 = Math.Round(varValorParcela, 2);
                        cronogramaPublica.ValorServicosTerceirosMes10 = Math.Round(varValorParcela, 2);
                        cronogramaPublica.ValorServicosTerceirosMes11 = Math.Round(varValorParcela, 2);
                        cronogramaPublica.ValorServicosTerceirosMes12 = Math.Round(varValorParcela, 2);
                    }
                }

                try
                {
                    using (var proxy = new ProxyPrefeitura())
                    {

                        var obj = proxy.Service.GetCronogramaDesembolsoRedePublicaByPrefeituraETipoProtecaoSocial(idPrefeitura, idProtecao, exercicio);
                        if (obj == null)
                            return;

                        cronogramaPublica.ValorMaterialConsumoMes1 = obj.ValorMaterialConsumoMes1;
                        cronogramaPublica.ValorMaterialConsumoMes2 = obj.ValorMaterialConsumoMes2;
                        cronogramaPublica.ValorMaterialConsumoMes3 = obj.ValorMaterialConsumoMes3;
                        cronogramaPublica.ValorMaterialConsumoMes4 = obj.ValorMaterialConsumoMes4;
                        cronogramaPublica.ValorMaterialConsumoMes5 = obj.ValorMaterialConsumoMes5;
                        cronogramaPublica.ValorMaterialConsumoMes6 = obj.ValorMaterialConsumoMes6;
                        cronogramaPublica.ValorMaterialConsumoMes7 = obj.ValorMaterialConsumoMes7;
                        cronogramaPublica.ValorMaterialConsumoMes8 = obj.ValorMaterialConsumoMes8;
                        cronogramaPublica.ValorMaterialConsumoMes9 = obj.ValorMaterialConsumoMes9;
                        cronogramaPublica.ValorMaterialConsumoMes10 = obj.ValorMaterialConsumoMes10;
                        cronogramaPublica.ValorMaterialConsumoMes11 = obj.ValorMaterialConsumoMes11;
                        cronogramaPublica.ValorMaterialConsumoMes12 = obj.ValorMaterialConsumoMes12;

                        if (idProtecao == 4)
                        {
                            cronogramaPublica.ValorServicosTerceirosMes1 = obj.ValorServicosTerceirosMes1;
                            cronogramaPublica.ValorServicosTerceirosMes2 = obj.ValorServicosTerceirosMes2;
                            cronogramaPublica.ValorServicosTerceirosMes3 = obj.ValorServicosTerceirosMes3;
                            cronogramaPublica.ValorServicosTerceirosMes4 = obj.ValorServicosTerceirosMes4;
                            cronogramaPublica.ValorServicosTerceirosMes5 = obj.ValorServicosTerceirosMes5;
                            cronogramaPublica.ValorServicosTerceirosMes6 = obj.ValorServicosTerceirosMes6;
                            cronogramaPublica.ValorServicosTerceirosMes7 = obj.ValorServicosTerceirosMes7;
                            cronogramaPublica.ValorServicosTerceirosMes8 = obj.ValorServicosTerceirosMes8;
                            cronogramaPublica.ValorServicosTerceirosMes9 = obj.ValorServicosTerceirosMes9;
                            cronogramaPublica.ValorServicosTerceirosMes10 = obj.ValorServicosTerceirosMes10;
                            cronogramaPublica.ValorServicosTerceirosMes11 = obj.ValorServicosTerceirosMes11;
                            cronogramaPublica.ValorServicosTerceirosMes12 = obj.ValorServicosTerceirosMes12;
                        }

                        //proxy.Service.SaveCronogramaDesembolsoRedePrivada(cronogramaPrivada);
                        proxy.Service.SaveCronogramaDesembolsoRedePublica(cronogramaPublica, exercicio);
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                if (String.IsNullOrEmpty(msg))
                {
                    msg = "Cronograma registrado com sucesso!";
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

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int exercicio = 2022; //TODO:DBM - adicionar fluxo para 2019
            using (var proxy = new ProxyPrefeitura())
            {

                //carregar as prefeituras em uma lista
                var prefeituras = proxy.Service.GetPrefeituras();

                //foreach para resgatar o confinanciamento e fazer o calculo das parcelas
                foreach (var item in prefeituras)
                {
                    int idPrefeitura = item.Id;


                    //filtrar por Tipo de proteção social
                    using (var proxyAssistenciais = new ProxyEstruturaAssistenciaSocial())
                    {
                        var tipoprotecao = proxyAssistenciais.Service.GetTiposProtecaoSocial();
                        foreach (var tipo in tipoprotecao)
                        {
                            var lst = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, tipo.Id, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);
                            decimal totalpublica = Convert.ToDecimal(lst.Where(t => t.IdTipoUnidade != 2).Sum(t => t.PrevisaoOrcamentaria).ToString("n2"));
                            //decimal totalprivada = Convert.ToDecimal(lst.Where(t => t.IdTipoUnidade == 2).Sum(t => t.PrevisaoOrcamentaria).ToString("n2"));
                            if (tipo.Id != 1)
                            {
                                GravarParcelasPublica(totalpublica, idPrefeitura, tipo.Id);
                            }
                        }
                    }
                }

            }
        }

        public class parcelas
        {
            public int idPrefeitura { get; set; }

            public int idTipoProtecao { get; set; }

            public decimal valorServicosTerceirosMes1 { get; set; }

            public decimal valorServicosTerceirosMes2 { get; set; }

            public decimal valorServicosTerceirosMes3 { get; set; }

            public decimal valorServicosTerceirosMes4 { get; set; }

            public decimal valorServicosTerceirosMes5 { get; set; }

            public decimal valorServicosTerceirosMes6 { get; set; }

            public decimal valorServicosTerceirosMes7 { get; set; }

            public decimal valorServicosTerceirosMes8 { get; set; }

            public decimal valorServicosTerceirosMes9 { get; set; }

            public decimal valorServicosTerceirosMes10 { get; set; }

            public decimal valorServicosTerceirosMes11 { get; set; }

            public decimal valorServicosTerceirosMes12 { get; set; }


        }
    }
}