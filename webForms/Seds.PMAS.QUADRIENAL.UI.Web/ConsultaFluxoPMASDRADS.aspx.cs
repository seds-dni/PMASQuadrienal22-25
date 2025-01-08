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
using Seds.PMAS.QUADRIENAL.Pendencia;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class ConsultaFluxoPMASDRADS : System.Web.UI.Page
    {

        #region propriedades
        private static List<int> QuadroExercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rdbPMAS.Checked = true;
                SessaoPmas.VerificarSessao(this);
                load();
            }
        }

        void load()
        {
            var lst = new List<ConsultaFluxoInfo>();
            var municipios = new List<Int32>();
            municipios.AddRange(ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.IdDrads == SessaoPmas.UsuarioLogado.IdDrads).Select(m => m.Id).ToList());

            using (var proxy = new ProxyPrefeitura())
            {
                lst = proxy.Service.GetConsultaFluxo(municipios).ToList();

                lst.ForEach((c) =>
                {
                    //var quadroExecucaoFinanceira = proxy.Service.GetPrefeituraSituacaoQuadro(c.IdPrefeitura, 143).Where(x => x.Exercicio == ConsultaFluxoPMASDRADS.QuadroExercicios[1]).FirstOrDefault();

                    #region Execucao Financeira
                    var quadrosDeExecucaoFinanceira = proxy.Service.GetPrefeituraSituacaoQuadro(c.IdPrefeitura, 143);
                    if (quadrosDeExecucaoFinanceira != null)
                    {
                        ObterSituacaoExecucaoFinanceira(c, quadrosDeExecucaoFinanceira);
                    }
                    #endregion

                    #region Lei Orcamentaria
                    var quadrosLeiOrcamentaria = proxy.Service.GetPrefeituraSituacaoQuadro(c.IdPrefeitura, 160);
                    if (quadrosLeiOrcamentaria != null)
                    {
                        ObterSituacaoLeiOrcamentaria(c, quadrosLeiOrcamentaria);
                    }
                    #endregion

                    #region PrestacaoDeContas
                    var quadrosPrestacaoDeContas = proxy.Service.GetPrefeituraSituacaoQuadro(c.IdPrefeitura, 168);
                    if (quadrosPrestacaoDeContas != null)
                    {
                        ObterSituacaoPrestacaoDeContas(c,quadrosPrestacaoDeContas);
                    }
                    #endregion

                    //var quadroLeiOrcamentaria = proxy.Service.GetPrefeituraSituacaoQuadro(c.IdPrefeitura, 160).Where(x => x.Exercicio == ConsultaFluxoPMASDRADS.QuadroExercicios[1]).FirstOrDefault();

                });
            }

            lst.ForEach(c => c.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.First(m => m.Id == c.IdMunicipio).Nome);

            if (rdbPMAS.Checked)
            {
                lstPMAS3.DataSource = null;
                lstPMAS3.DataBind();

                lstPMAS2.DataSource = null;
                lstPMAS2.DataBind();

                lstPMAS.DataSource = lst.OrderBy(c => c.Municipio);
                lstPMAS.DataBind();
            }

            if (rdbEFLO.Checked)
            {
                lstPMAS.DataSource = null;
                lstPMAS.DataBind();

                lstPMAS3.DataSource = null;
                lstPMAS3.DataBind();

                lstPMAS2.DataSource = lst.OrderBy(c => c.Municipio);
                lstPMAS2.DataBind();
            }

            if (rdbPrestacaoDeContas.Checked)
            {
                lstPMAS.DataSource = null;
                lstPMAS.DataBind();

                lstPMAS2.DataSource = null;
                lstPMAS2.DataBind();

                lstPMAS3.DataSource = lst.OrderBy(c => c.Municipio);
                lstPMAS3.DataBind();
            }
        }

        protected void lstExecucaoLeiOrcamentaria_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (ConsultaFluxoInfo)e.Item.DataItem;
                //int sequencia = e.Item.DataItemIndex + 1;
            }
        }


        private static void ObterSituacaoLeiOrcamentaria(ConsultaFluxoInfo c, List<PrefeituraSituacaoQuadroInfo> quadrosLeiOrcamentaria)
        {

            foreach (var quadroLeiOrcamentaria in quadrosLeiOrcamentaria)
            {
                ConsultaFluxoInfo.QuadroLeiOrcamentariaInner quadro = new ConsultaFluxoInfo.QuadroLeiOrcamentariaInner();
                quadro.Exercicio = quadroLeiOrcamentaria.Exercicio;
                switch (quadroLeiOrcamentaria.IdSituacaoQuadro)
                {
                    case (int)ESituacaoQuadro.Pendente:
                        quadro.SituacaoQuadroLeiOrcamentaria = "Pendente";
                        break;
                    case (int)ESituacaoQuadro.Preenchido:
                        quadro.SituacaoQuadroLeiOrcamentaria = "Preenchido";
                        break;
                    case (int)ESituacaoQuadro.EmAnaliseCMAS:
                        quadro.SituacaoQuadroLeiOrcamentaria = "Em análise CMAS";
                        break;
                    case (int)ESituacaoQuadro.AprovadoCMAS:
                        quadro.SituacaoQuadroLeiOrcamentaria = "Aprovado CMAS";
                        break;
                    case (int)ESituacaoQuadro.DevolvidoDRADS:
                        quadro.SituacaoQuadroLeiOrcamentaria = "Devolvido DRADS";
                        break;
                    case (int)ESituacaoQuadro.DevolvidoCMAS:
                        quadro.SituacaoQuadroLeiOrcamentaria = "Devolvido CMAS";
                        break;
                    default:
                        quadro.SituacaoQuadroLeiOrcamentaria = "Bloqueado";
                        break;

                }

                c.QuadrosLeiOrcamentariaInner.Add(quadro);
            }
        }

        private static void ObterSituacaoExecucaoFinanceira(ConsultaFluxoInfo c, List<PrefeituraSituacaoQuadroInfo> quadrosExecucaoFinanceira)
        {
            foreach (var quadroExecucaoFinanceira in quadrosExecucaoFinanceira)
            {
                ConsultaFluxoInfo.QuadroExecucaoFinanceiraInner quadro = new ConsultaFluxoInfo.QuadroExecucaoFinanceiraInner();
                if (quadroExecucaoFinanceira.Exercicio >= 2020)
                {
                    switch (quadroExecucaoFinanceira.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Em Preenchimento";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Em análise pelo CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Aprovado pelo CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Devolvido CMAS";
                            break;
                        case (int)ESituacaoQuadro.RejeitadoCMAS:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Rejeitado pelo CMAS";
                            break;
                        default:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Bloqueado";
                            break;
                    }

                }
                else
                {
                    switch (quadroExecucaoFinanceira.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Pendente";
                            break;
                        case (int)ESituacaoQuadro.Preenchido:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Preenchido";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Em análise CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Aprovado CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoDRADS:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Devolvido DRADS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Devolvido CMAS";
                            break;
                        case (int)ESituacaoQuadro.BloqueioInicialAdministrativo:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Bloqueado";
                            break;
                    }

                }
                c.QuadrosExecucaoFinanceiraInner.Add(quadro);
            }
        }

        private static void ObterSituacaoPrestacaoDeContas(ConsultaFluxoInfo c, List<PrefeituraSituacaoQuadroInfo> _quadroPrestacaoDeContas)
        {

            foreach (var quadroPrestacaoDeContas in _quadroPrestacaoDeContas)
            {
                ConsultaFluxoInfo.QuadroPrestacaoDeContasInner quadro = new ConsultaFluxoInfo.QuadroPrestacaoDeContasInner();
                quadro.Exercicio = quadroPrestacaoDeContas.Exercicio;
                switch (quadroPrestacaoDeContas.IdSituacaoQuadro)
                {
                    case (int)ESituacaoQuadro.Pendente:
                        quadro.SituacaoQuadroPrestacaoDeContas = "Em Preenchimento";
                        break;
                    case (int)ESituacaoQuadro.EmAnaliseCMAS:
                        quadro.SituacaoQuadroPrestacaoDeContas = "Em análise pelo CMAS";
                        break;
                    case (int)ESituacaoQuadro.AprovadoCMAS:
                        quadro.SituacaoQuadroPrestacaoDeContas = "Em análise DRADS";
                        break;
                    case (int)ESituacaoQuadro.DevolvidoDRADS:
                        quadro.SituacaoQuadroPrestacaoDeContas = "Devolvido DRADS";
                        break;
                    case (int)ESituacaoQuadro.DevolvidoCMAS:
                        quadro.SituacaoQuadroPrestacaoDeContas = "Devolvido pelo CMAS";
                        break;
                    case (int)ESituacaoQuadro.RejeitadoCMAS:
                        quadro.SituacaoQuadroPrestacaoDeContas = "Rejeitado pelo CMAS";
                        break;
                    case (int)ESituacaoQuadro.AprovadoDRADS:
                        quadro.SituacaoQuadroPrestacaoDeContas = "Aprovado";
                        break;
                    default:
                        quadro.SituacaoQuadroPrestacaoDeContas = "Bloqueado";
                        break;
                }
             
                c.QuadrosPrestacaoDeContasInner.Add(quadro);
            }
        }

        protected void lstPMAS_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (ConsultaFluxoInfo)e.Item.DataItem;
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();

                LinkButton lkbEnviarFinalizacao = (LinkButton)e.Item.FindControl("lkbEnviarFinalizacao");
                //LinkButton lkbParecerDrads = (LinkButton)e.Item.FindControl("lkbParecerDrads");
                LinkButton lkbDevolverOrgaoGestor = (LinkButton)e.Item.FindControl("lkbDevolverOrgaoGestor");
                LinkButton lkbAutorizaDevolverOrgaoGestor = (LinkButton)e.Item.FindControl("lkbAutorizaDevolverOrgaoGestor");
                LinkButton lkbAutorizaDevolverCMAS = (LinkButton)e.Item.FindControl("lkbAutorizaDevolverCMAS");
                LinkButton lkbDevolverCAS = (LinkButton)e.Item.FindControl("lkbDevolverCAS");

                //if (item.DesbloquearValoresDrads.HasValue)
                //{

                //    lkbParecerDrads.Visible = item.DesbloquearValoresDrads.Value;
                //    lkbParecerDrads.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador;
                //    lkbParecerDrads.Text = "Desbloqueado";
                //}


                switch ((ESituacao)item.Situacao.Id)
                {
                    case ESituacao.EmAnaliseDrads:
                        lkbEnviarFinalizacao.Visible = true;
                        lkbEnviarFinalizacao.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador;
                        if (lkbEnviarFinalizacao.Enabled)
                            lkbEnviarFinalizacao.Attributes.Add("OnClick", "return confirm('Tem certeza que deseja Enviar para finalização o município de " + item.Municipio + " ?');");
                        lkbEnviarFinalizacao.Text = "Enviar para finalização";

                        lkbDevolverOrgaoGestor.Visible = true;
                        lkbDevolverOrgaoGestor.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador || SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADS;
                        if (lkbDevolverOrgaoGestor.Enabled)
                            lkbDevolverOrgaoGestor.Attributes.Add("OnClick", "return confirm('Tem certeza que deseja devolver para o Órgão gestor o município de " + item.Municipio + " ?');");
                        lkbDevolverOrgaoGestor.Text = "Devolver Órgão Gestor";
                        break;

                    case ESituacao.AutorizaDesbloqueioCMAS:
                        lkbAutorizaDevolverCMAS.Visible = true;
                        lkbAutorizaDevolverCMAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador;
                        if (lkbAutorizaDevolverCMAS.Enabled)
                            lkbAutorizaDevolverCMAS.Attributes.Add("OnClick", "return confirm('Tem certeza que deseja devolver o plano para o CMAS de " + item.Municipio + " ?');");
                        lkbAutorizaDevolverCMAS.Text = "Desbloquear para CMAS";

                        lkbDevolverCAS.Visible = true;
                        lkbDevolverCAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador;
                        if (lkbDevolverCAS.Enabled)
                            lkbDevolverCAS.Attributes.Add("OnClick", "return confirm('Tem certeza que deseja devolver o plano de " + item.Municipio + " para a CAS?');");
                        lkbDevolverCAS.Text = "Devolver para CAS";
                        break;


                    case ESituacao.AutorizaDesbloqueioGestor:
                        lkbAutorizaDevolverOrgaoGestor.Visible = true;
                        lkbAutorizaDevolverOrgaoGestor.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador;
                        if (lkbAutorizaDevolverOrgaoGestor.Enabled)
                            lkbAutorizaDevolverOrgaoGestor.Attributes.Add("OnClick", "return confirm('Tem certeza que deseja desbloquear o plano para o Órgão gestor o município de " + item.Municipio + " ?');");
                        lkbAutorizaDevolverOrgaoGestor.Text = "Desbloquear para Órgão Gestor";

                        lkbDevolverCAS.Visible = true;
                        lkbDevolverCAS.Enabled = SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.DRADSAdministrador;
                        if (lkbDevolverCAS.Enabled)
                            lkbDevolverCAS.Attributes.Add("OnClick", "return confirm('Tem certeza que deseja devolver o plano de " + item.Municipio + " para a CAS?');");
                        lkbDevolverCAS.Text = "Devolver para CAS";
                        break;

                    default:
                        lkbEnviarFinalizacao.Visible = false;
                        lkbEnviarFinalizacao.Text = "";
                        lkbEnviarFinalizacao.Visible = false;
                        lkbEnviarFinalizacao.Text = "";
                        break;
                }
            }
        }

        protected void lstPMAS_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstPMAS.DataKeys[e.Item.DataItemIndex];

            switch (e.CommandName)
            {
                case "Visualizar_Municipio":
                    using (var proxy = new ProxyPrefeitura())
                    {

                        int idPrefeitura = Convert.ToInt32(key["IdPrefeitura"]);
                        int idMunicipio = Convert.ToInt32(key["IdMunicipio"]);

                        SessaoPmas.UsuarioLogado.Prefeitura = proxy.Service.GetPrefeituraById(idPrefeitura);
                        SessaoPmas.UsuarioLogado.Prefeitura.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.FirstOrDefault(m => m.Id == idMunicipio);
                        SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Drads = ProxyDivisaoAdministrativa.Drads.FirstOrDefault(d => d.Id == SessaoPmas.UsuarioLogado.Prefeitura.Municipio.IdDrads);
                    }
                    Response.Redirect("~/Default.aspx");
                    break;
                case "DevolverOrgaoGestor":
                    Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlEncode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.DevolvidoDrads).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
                    break;
                case "EnviarFinalizacao":
                    using (var proxy = new ProxyPrefeitura())
                    {
                        int idPrefeitura = Convert.ToInt32(key["IdPrefeitura"]);
                        int idMunicipio = Convert.ToInt32(key["IdMunicipio"]);

                        if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                        {
                            SessaoPmas.UsuarioLogado.Prefeitura = proxy.Service.GetPrefeituraById(idPrefeitura);
                            SessaoPmas.UsuarioLogado.Prefeitura.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.FirstOrDefault(m => m.Id == idMunicipio);
                            SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Drads = ProxyDivisaoAdministrativa.Drads.FirstOrDefault(d => d.Id == SessaoPmas.UsuarioLogado.Prefeitura.Municipio.IdDrads);
                        }

                        if (!new VerificadorPendenciaPMAS().PlanoMunicipalPossuiPendenciaOrgaoGestor(Convert.ToInt32(key["IdPrefeitura"]), SessaoPmas.UsuarioLogado.EnumPerfil.Value))
                        {
                            //if (!proxy.Service.ValidarPlanoMunicipalPendenciaOrgaoGestor(Convert.ToInt32(key["IdPrefeitura"]), SessaoPmas.UsuarioLogado.EnumPerfil.Value))
                            Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlEncode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.Parafinalizacao).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
                        }
                        else
                        {
                            var script = Util.GetJavascriptDialogError("O Plano Municipal possui pendências! O mesmo não pode ser enviado para finalização.");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        }
                    }
                    break;

                case "ParecerDrads":
                    using (var proxy = new ProxyPrefeitura())
                    {
                        if (!new VerificadorPendenciaPMAS().PlanoMunicipalPossuiPendenciaOrgaoGestor(Convert.ToInt32(key["IdPrefeitura"]), SessaoPmas.UsuarioLogado.EnumPerfil.Value))
                        {
                        //if (!proxy.Service.ValidarPlanoMunicipalPendenciaOrgaoGestor(Convert.ToInt32(key["IdPrefeitura"]), SessaoPmas.UsuarioLogado.EnumPerfil.Value))
                        //{
                            Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlEncode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.Parafinalizacao).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
                        }
                        else
                        {
                            var script = Util.GetJavascriptDialogError("O Plano Municipal possui pendências! O mesmo não pode ser enviado para finalização.");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        }
                    }
                    break;

                case "DevolverCAS":
                    /*try
                    {
                        using (var proxy = new ProxyPlanoMunicipal())
                        {
                            proxy.Service.DevolverPlanoMunicipalDradsParaCAS(Convert.ToInt32(key["IdPrefeitura"]));
                        }
                    }
                    catch (Exception ex)
                    {
                        var script = Util.GetJavascriptDialogError(ex.Message.Replace(System.Environment.NewLine, "<br/>"));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    }
                    load();*/
                    Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlEncode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.DevolverParaCas).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
                    break;
                case "AutorizarDevolucaoOrgaoGestor":
                    Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlEncode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.Desbloqueado).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
                    break;

                case "AutorizarDevolucaoCMAS":
                    Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlEncode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.EmAnalisedoCMAS).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
                    break;

                case "Visualizar_Historico":
                    using (var proxy = new ProxyPrefeitura())
                    {
                        SessaoPmas.UsuarioLogado.Prefeitura = proxy.Service.GetPrefeituraById(Convert.ToInt32(key["IdPrefeitura"]));
                        SessaoPmas.UsuarioLogado.Prefeitura.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.FirstOrDefault(m => m.Id == Convert.ToInt32(key["IdMunicipio"]));
                        SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Drads = ProxyDivisaoAdministrativa.Drads.FirstOrDefault(d => d.Id == SessaoPmas.UsuarioLogado.Prefeitura.Municipio.IdDrads);
                    }
                    Response.Redirect("~/FluxoHistorico.aspx?idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
                    break;
                default:
                    break;
            }
        }

        protected void rdbPMAS_CheckedChanged(object sender, EventArgs e)
        {
            load();
        }
    }
}