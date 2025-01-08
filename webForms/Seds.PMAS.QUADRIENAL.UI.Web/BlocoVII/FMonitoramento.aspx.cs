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
    public partial class FMonitoramento : System.Web.UI.Page
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
                WebControl[] controles = { chkFocoRedePrivada1,
                                             chkFocoRedePrivada2,
                                             chkFocoRedePrivada3,
                                             chkFocoRedePrivada4,
                                             chkFocoRedePrivada5,
                                             chkFocoRedePrivada6,
                                             chkFocoRedePrivada7,
                                             chkFocoRedePublica1,
                                             chkFocoRedePublica2,
                                             chkFocoRedePublica3,
                                             chkFocoRedePublica4,
                                             chkFocoRedePublica5,
                                             chkFocoRedePublica6,
                                             chkFocoRedePublica7,
                                             chkInstrumentos1,
                                             chkInstrumentos2,
                                             chkInstrumentos3,
                                             chkInstrumentos4,
                                             chkMeiosDivulgacao,
                                             chkNaoHaMonitoramento,
                                             chkOrgaoGestor,
                                             chkProcedimento1,
                                             chkProcedimento2,
                                             chkProcedimento3,
                                             chkProcedimento4,
                                             chkRedePrivada,
                                             chkRedePublica,
                                             chkServicoTerceirizado,
                                             rblInformacoesSistematizadas,
                                             rblMonitora,
                                             rblOrgaoGestor,
                                             rblPeriodicidadeFocoRedePrivada1,
                                             rblPeriodicidadeFocoRedePrivada2,
                                             rblPeriodicidadeFocoRedePrivada3,
                                             rblPeriodicidadeFocoRedePrivada4,
                                             rblPeriodicidadeFocoRedePrivada5,
                                             rblPeriodicidadeFocoRedePrivada6,
                                             rblPeriodicidadeFocoRedePrivada7,
                                             rblPeriodicidadeFocoRedePublica1,
                                             rblPeriodicidadeFocoRedePublica2,
                                             rblPeriodicidadeFocoRedePublica3,
                                             rblPeriodicidadeFocoRedePublica4,
                                             rblPeriodicidadeFocoRedePublica5,
                                             rblPeriodicidadeFocoRedePublica6,
                                             rblPeriodicidadeFocoRedePublica7,
                                             rblPMASMonitoramento,
                                             rblProximoAno,
                                             rblResultados,
                                             btnSalvar};
                Permissao.VerificarPermissaoControles(controles, Session);
                #endregion
            }
        }

        void limparHiddenFiels()
        {
            hdfFocoRedePrivada1.Value = "0";
            hdfFocoRedePrivada2.Value = "0";
            hdfFocoRedePrivada3.Value = "0";
            hdfFocoRedePrivada4.Value = "0";
            hdfFocoRedePrivada5.Value = "0";
            hdfFocoRedePrivada6.Value = "0";
            hdfFocoRedePrivada7.Value = "0";
            hdfFocoRedePublica1.Value = "0";
            hdfFocoRedePublica2.Value = "0";
            hdfFocoRedePublica3.Value = "0";
            hdfFocoRedePublica4.Value = "0";
            hdfFocoRedePublica5.Value = "0";
            hdfFocoRedePublica6.Value = "0";
            hdfFocoRedePublica7.Value = "0";
        }

        void verificarAlteracoes()
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro69.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadro(SessaoPmas.UsuarioLogado.Prefeitura.Id, 69);
                    linkAlteracoesQuadro69.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt("69"));
                }
            }
        }

        void load(ProxyAcoes proxy)
        {
            var obj = proxy.Service.GetMonitoramentoByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null)
                return;

            hdfId.Value = obj.Id.ToString();

            rblMonitora.SelectedValue = Convert.ToSByte(obj.RealizaMonitoramento).ToString();
            rblMonitora_SelectedIndexChanged(null, null);

            limparHiddenFiels();

            rblPMASMonitoramento.SelectedValue = Convert.ToSByte(obj.PMASObjetoMonitoramento).ToString();

            if (!obj.RealizaMonitoramento)
            {
                rblProximoAno.SelectedValue = Convert.ToSByte(obj.PretendeRealizarProximoAno).ToString();
                return;
            }

            rblResultados.SelectedValue = Convert.ToSByte(obj.ResultadosDivulgados).ToString();
            rblResultados_SelectedIndexChanged(null, null);
            if (obj.ResultadosDivulgados)
            {
                if (obj.MeiosDivulgacao != null)
                    foreach (ListItem i in chkMeiosDivulgacao.Items)
                        i.Selected = obj.MeiosDivulgacao.Any(t => t.Id == Convert.ToInt32(i.Value));
            }

            if (obj.Procedimentos != null && obj.Procedimentos.Any(p => p.Id == 1))
            {
                chkProcedimento1.Checked = true;
                trInstrumentos1.Visible = true;
                if (obj.Instrumentos != null)
                    foreach (ListItem i in chkInstrumentos1.Items)
                        i.Selected = obj.Instrumentos.Any(t => t.Id == Convert.ToInt32(i.Value));
            }

            if (obj.Procedimentos != null && obj.Procedimentos.Any(p => p.Id == 2))
            {
                chkProcedimento2.Checked = true;
                trInstrumentos2.Visible = true;
                if (obj.Instrumentos != null)
                    foreach (ListItem i in chkInstrumentos2.Items)
                        i.Selected = obj.Instrumentos.Any(t => t.Id == Convert.ToInt32(i.Value));
            }

            if (obj.Procedimentos != null && obj.Procedimentos.Any(p => p.Id == 3))
            {
                chkProcedimento3.Checked = true;
                trInstrumentos3.Visible = true;
                if (obj.Instrumentos != null)
                    foreach (ListItem i in chkInstrumentos3.Items)
                        i.Selected = obj.Instrumentos.Any(t => t.Id == Convert.ToInt32(i.Value));
            }

            if (obj.Procedimentos != null && obj.Procedimentos.Any(p => p.Id == 4))
            {
                chkProcedimento4.Checked = true;
                trInstrumentos4.Visible = true;
                if (obj.Instrumentos != null)
                    foreach (ListItem i in chkInstrumentos4.Items)
                        i.Selected = obj.Instrumentos.Any(t => t.Id == Convert.ToInt32(i.Value));
            }

            if (obj.Focos != null && obj.Focos.Count > 0)
            {
                if (obj.Focos.Any(f => f.IdTipoRede == 1))
                {
                    chkRedePublica.Checked = true;
                    chkRedePublica_CheckedChanged(null, null);

                    if (obj.Focos.Any(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 1))
                    {
                        chkFocoRedePublica1.Checked = true;
                        chkFocoRedePublica1_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 1);
                        hdfFocoRedePublica1.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePublica1.SelectedValue = foco.IdPeriodicidade.ToString();
                    }

                    if (obj.Focos.Any(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 2))
                    {
                        chkFocoRedePublica2.Checked = true;
                        chkFocoRedePublica2_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 2);
                        hdfFocoRedePublica2.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePublica2.SelectedValue = foco.IdPeriodicidade.ToString();
                    }

                    if (obj.Focos.Any(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 3))
                    {
                        chkFocoRedePublica3.Checked = true;
                        chkFocoRedePublica3_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 3);
                        hdfFocoRedePublica3.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePublica3.SelectedValue = foco.IdPeriodicidade.ToString();
                    }
                    if (obj.Focos.Any(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 4))
                    {
                        chkFocoRedePublica4.Checked = true;
                        chkFocoRedePublica4_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 4);
                        hdfFocoRedePublica4.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePublica4.SelectedValue = foco.IdPeriodicidade.ToString();
                    }

                    if (obj.Focos.Any(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 5))
                    {
                        chkFocoRedePublica5.Checked = true;
                        chkFocoRedePublica5_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 5);
                        hdfFocoRedePublica5.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePublica5.SelectedValue = foco.IdPeriodicidade.ToString();
                    }

                    if (obj.Focos.Any(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 6))
                    {
                        chkFocoRedePublica6.Checked = true;
                        chkFocoRedePublica6_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 6);
                        hdfFocoRedePublica6.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePublica6.SelectedValue = foco.IdPeriodicidade.ToString();
                    }

                    if (obj.Focos.Any(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 7))
                    {
                        chkFocoRedePublica7.Checked = true;
                        chkFocoRedePublica7_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 1 && f.IdFocoMonitoramento == 7);
                        hdfFocoRedePublica7.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePublica7.SelectedValue = foco.IdPeriodicidade.ToString();
                    }
                }

                if (obj.Focos.Any(f => f.IdTipoRede == 2))
                {
                    chkRedePrivada.Checked = true;
                    chkRedePrivada_CheckedChanged(null, null);

                    if (obj.Focos.Any(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 1))
                    {
                        chkFocoRedePrivada1.Checked = true;
                        chkFocoRedePrivada1_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 1);
                        hdfFocoRedePrivada1.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePrivada1.SelectedValue = foco.IdPeriodicidade.ToString();
                    }

                    if (obj.Focos.Any(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 2))
                    {
                        chkFocoRedePrivada2.Checked = true;
                        chkFocoRedePrivada2_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 2);
                        hdfFocoRedePrivada2.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePrivada2.SelectedValue = foco.IdPeriodicidade.ToString();
                    }

                    if (obj.Focos.Any(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 3))
                    {
                        chkFocoRedePrivada3.Checked = true;
                        chkFocoRedePrivada3_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 3);
                        hdfFocoRedePrivada3.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePrivada3.SelectedValue = foco.IdPeriodicidade.ToString();
                    }
                    if (obj.Focos.Any(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 4))
                    {
                        chkFocoRedePrivada4.Checked = true;
                        chkFocoRedePrivada4_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 4);
                        hdfFocoRedePrivada4.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePrivada4.SelectedValue = foco.IdPeriodicidade.ToString();
                    }

                    if (obj.Focos.Any(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 5))
                    {
                        chkFocoRedePrivada5.Checked = true;
                        chkFocoRedePrivada5_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 5);
                        hdfFocoRedePrivada5.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePrivada5.SelectedValue = foco.IdPeriodicidade.ToString();
                    }

                    if (obj.Focos.Any(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 6))
                    {
                        chkFocoRedePrivada6.Checked = true;
                        chkFocoRedePrivada6_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 6);
                        hdfFocoRedePrivada6.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePrivada6.SelectedValue = foco.IdPeriodicidade.ToString();
                    }

                    if (obj.Focos.Any(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 7))
                    {
                        chkFocoRedePrivada7.Checked = true;
                        chkFocoRedePrivada7_CheckedChanged(null, null);
                        var foco = obj.Focos.First(f => f.IdTipoRede == 2 && f.IdFocoMonitoramento == 7);
                        hdfFocoRedePrivada7.Value = foco.Id.ToString();
                        rblPeriodicidadeFocoRedePrivada7.SelectedValue = foco.IdPeriodicidade.ToString();
                    }
                }
            }

            chkNaoHaMonitoramento.Checked = obj.NaoHaMonitoramentoRedeSocioAssistencial;
            chkNaoHaMonitoramento_CheckedChanged(null, null);


            chkOrgaoGestor.Checked = obj.OperacionalizadoOrgaoGestor;
            if (obj.OperacionalizadoOrgaoGestor)
            {
                trOrgaoGestor.Visible = true;
                if (obj.OperacionalizadoOrgaoGestorEquipeEspecifica)
                    rblOrgaoGestor.SelectedValue = "EquipeEspecifica";
                if (obj.OperacionalizadoOrgaoGestorEquipeTecnicoProtecaoSocial)
                    rblOrgaoGestor.SelectedValue = "EquipeTecnicoProtecaoSocial";
                if (obj.OperacionalizadoOrgaoGestorTecnicosOutrasEquipes)
                    rblOrgaoGestor.SelectedValue = "TecnicosOutrasEquipes";
            }

            chkServicoTerceirizado.Checked = obj.OperacionalizadoTerceirizado;

            rblInformacoesSistematizadas.SelectedValue = Convert.ToSByte(obj.InformacoesSistematizadas).ToString();
        }

        void carregarEstruturas(ProxyEstruturaAssistenciaSocial proxy)
        {
            chkMeiosDivulgacao.DataTextField = "Nome";
            chkMeiosDivulgacao.DataValueField = "Id";
            chkMeiosDivulgacao.DataSource = proxy.Service.GetMeiosDivulgacao();
            chkMeiosDivulgacao.DataBind();

            chkInstrumentos1.DataTextField = "Nome";
            chkInstrumentos1.DataValueField = "Id";
            chkInstrumentos1.DataSource = proxy.Service.GetInstrumentosMonitoramentoByProcedimento(1);
            chkInstrumentos1.DataBind();

            chkInstrumentos2.DataTextField = "Nome";
            chkInstrumentos2.DataValueField = "Id";
            chkInstrumentos2.DataSource = proxy.Service.GetInstrumentosMonitoramentoByProcedimento(2);
            chkInstrumentos2.DataBind();

            chkInstrumentos3.DataTextField = "Nome";
            chkInstrumentos3.DataValueField = "Id";
            chkInstrumentos3.DataSource = proxy.Service.GetInstrumentosMonitoramentoByProcedimento(3);
            chkInstrumentos3.DataBind();

            chkInstrumentos4.DataTextField = "Nome";
            chkInstrumentos4.DataValueField = "Id";
            chkInstrumentos4.DataSource = proxy.Service.GetInstrumentosMonitoramentoByProcedimento(4);
            chkInstrumentos4.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var obj = new MonitoramentoInfo();
            obj.Id = Convert.ToInt32(hdfId.Value);
            obj.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            obj.RealizaMonitoramento = rblMonitora.SelectedValue == "1";
            obj.Instrumentos = new List<InstrumentoMonitoramentoInfo>();
            obj.Procedimentos = new List<ProcedimentoMonitoramentoInfo>();
            obj.MeiosDivulgacao = new List<MeioDivulgacaoInfo>();
            obj.Focos = new List<PrefeituraMonitoramentoFocoInfo>();
            if (obj.RealizaMonitoramento)
            {
                obj.PMASObjetoMonitoramento = rblPMASMonitoramento.SelectedValue == "1";
                obj.InformacoesSistematizadas = rblInformacoesSistematizadas.SelectedValue == "1";
                obj.ResultadosDivulgados = rblResultados.SelectedValue == "1";
                if (obj.ResultadosDivulgados)
                {
                    foreach (ListItem i in chkMeiosDivulgacao.Items)
                        if (i.Selected)
                            obj.MeiosDivulgacao.Add(new MeioDivulgacaoInfo() { Id = Convert.ToInt32(i.Value) });
                }

                if (chkProcedimento1.Checked)
                {
                    obj.Procedimentos.Add(new ProcedimentoMonitoramentoInfo() { Id = 1 });
                    foreach (ListItem i in chkInstrumentos1.Items)
                        if (i.Selected)
                            obj.Instrumentos.Add(new InstrumentoMonitoramentoInfo() { Id = Convert.ToInt32(i.Value), IdProcedimentoMonitoramento = 1 });
                }

                if (chkProcedimento2.Checked)
                {
                    obj.Procedimentos.Add(new ProcedimentoMonitoramentoInfo() { Id = 2 });
                    foreach (ListItem i in chkInstrumentos2.Items)
                        if (i.Selected)
                            obj.Instrumentos.Add(new InstrumentoMonitoramentoInfo() { Id = Convert.ToInt32(i.Value), IdProcedimentoMonitoramento = 2 });
                }

                if (chkProcedimento3.Checked)
                {
                    obj.Procedimentos.Add(new ProcedimentoMonitoramentoInfo() { Id = 3 });
                    foreach (ListItem i in chkInstrumentos3.Items)
                        if (i.Selected)
                            obj.Instrumentos.Add(new InstrumentoMonitoramentoInfo() { Id = Convert.ToInt32(i.Value), IdProcedimentoMonitoramento = 3 });
                }

                if (chkProcedimento4.Checked)
                {
                    obj.Procedimentos.Add(new ProcedimentoMonitoramentoInfo() { Id = 4 });
                    foreach (ListItem i in chkInstrumentos4.Items)
                        if (i.Selected)
                            obj.Instrumentos.Add(new InstrumentoMonitoramentoInfo() { Id = Convert.ToInt32(i.Value), IdProcedimentoMonitoramento = 4 });
                }


                obj.OperacionalizadoOrgaoGestor = chkOrgaoGestor.Checked;
                if (obj.OperacionalizadoOrgaoGestor)
                {
                    obj.OperacionalizadoOrgaoGestorEquipeEspecifica = rblOrgaoGestor.SelectedValue == "EquipeEspecifica";
                    obj.OperacionalizadoOrgaoGestorEquipeTecnicoProtecaoSocial = rblOrgaoGestor.SelectedValue == "EquipeTecnicoProtecaoSocial";
                    obj.OperacionalizadoOrgaoGestorTecnicosOutrasEquipes = rblOrgaoGestor.SelectedValue == "TecnicosOutrasEquipes";
                }
                obj.OperacionalizadoTerceirizado = chkServicoTerceirizado.Checked;

                if (chkRedePublica.Checked)
                {
                    if (chkFocoRedePublica1.Checked && rblPeriodicidadeFocoRedePublica1.SelectedIndex != -1)
                        obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica1.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 1, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePublica1.SelectedValue) });

                    if (chkFocoRedePublica2.Checked)
                    {
                        if (rblPeriodicidadeFocoRedePublica2.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica2.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 2, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePublica2.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica2.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 2, IdPeriodicidade = 0 });
                    }

                    if (chkFocoRedePublica3.Checked)
                        if (rblPeriodicidadeFocoRedePublica3.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica3.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 3, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePublica3.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica3.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 3, IdPeriodicidade = 0 });

                    if (chkFocoRedePublica4.Checked)
                        if (rblPeriodicidadeFocoRedePublica4.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica4.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 4, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePublica4.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica4.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 4, IdPeriodicidade = 0 });

                    if (chkFocoRedePublica5.Checked)
                        if (rblPeriodicidadeFocoRedePublica5.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica5.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 5, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePublica5.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica5.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 5, IdPeriodicidade = 0 });

                    if (chkFocoRedePublica6.Checked)
                        if (rblPeriodicidadeFocoRedePublica6.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica6.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 6, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePublica6.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica6.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 6, IdPeriodicidade = 0 });

                    if (chkFocoRedePublica7.Checked)
                        if (rblPeriodicidadeFocoRedePublica7.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica7.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 7, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePublica7.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePublica7.Value), IdTipoRede = 1, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 7, IdPeriodicidade = 0 });
                }

                if (chkRedePrivada.Checked)
                {
                    if (chkFocoRedePrivada1.Checked)
                        if (rblPeriodicidadeFocoRedePrivada1.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada1.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 1, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePrivada1.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada1.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 1, IdPeriodicidade = 0 });

                    if (chkFocoRedePrivada2.Checked)
                        if (rblPeriodicidadeFocoRedePrivada2.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada2.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 2, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePrivada2.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada2.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 2, IdPeriodicidade = 0 });

                    if (chkFocoRedePrivada3.Checked)
                        if (rblPeriodicidadeFocoRedePrivada3.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada3.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 3, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePrivada3.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada3.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 3, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePrivada3.SelectedValue) });

                    if (chkFocoRedePrivada4.Checked)
                        if (rblPeriodicidadeFocoRedePrivada4.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada4.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 4, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePrivada4.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada4.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 4, IdPeriodicidade = 0 });


                    if (chkFocoRedePrivada5.Checked)
                        if (rblPeriodicidadeFocoRedePrivada5.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada5.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 5, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePrivada5.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada5.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 5, IdPeriodicidade = 0 });

                    if (chkFocoRedePrivada6.Checked)
                        if (rblPeriodicidadeFocoRedePrivada6.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada6.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 6, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePrivada6.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada6.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 6, IdPeriodicidade = 0 });

                    if (chkFocoRedePrivada7.Checked)
                        if (rblPeriodicidadeFocoRedePrivada7.SelectedIndex != -1)
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada7.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 7, IdPeriodicidade = Convert.ToInt32(rblPeriodicidadeFocoRedePrivada7.SelectedValue) });
                        else
                            obj.Focos.Add(new PrefeituraMonitoramentoFocoInfo() { Id = Convert.ToInt32(hdfFocoRedePrivada7.Value), IdTipoRede = 2, IdMonitoramento = Convert.ToInt32(hdfId.Value), IdFocoMonitoramento = 7, IdPeriodicidade = 0 });
                }

                obj.NaoHaMonitoramentoRedeSocioAssistencial = chkNaoHaMonitoramento.Checked;
            }
            else
            {
                obj.PretendeRealizarProximoAno = rblProximoAno.SelectedValue == "1";
            }

            String msg = String.Empty;
            try
            {
                new ValidadorMonitoramento().Validar(obj);

                using (var proxy = new ProxyAcoes())
                {
                    proxy.Service.SaveMonitoramento(obj);
                    load(proxy);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Monitoramento registrado com sucesso!";
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

        protected void rblMonitora_SelectedIndexChanged(object sender, EventArgs e)
        {
            trSim.Visible = rblMonitora.SelectedValue == "1";
            trNao.Visible = rblMonitora.SelectedValue == "0";
        }

        protected void chkOrgaoGestor_CheckedChanged(object sender, EventArgs e)
        {
            trOrgaoGestor.Visible = chkOrgaoGestor.Checked;
        }

        protected void chkRedePublica_CheckedChanged(object sender, EventArgs e)
        {
            trFocosRedePublica.Visible = chkRedePublica.Checked;
            if (!trFocosRedePublica.Visible)
            {
                chkFocoRedePublica1.Checked = false;
                chkFocoRedePublica2.Checked = false;
                chkFocoRedePublica3.Checked = false;
                chkFocoRedePublica4.Checked = false;
                chkFocoRedePublica5.Checked = false;
                chkFocoRedePublica6.Checked = false;
                chkFocoRedePublica7.Checked = false;
                chkFocoRedePublica1_CheckedChanged(null, null);
                chkFocoRedePublica2_CheckedChanged(null, null);
                chkFocoRedePublica3_CheckedChanged(null, null);
                chkFocoRedePublica4_CheckedChanged(null, null);
                chkFocoRedePublica5_CheckedChanged(null, null);
                chkFocoRedePublica6_CheckedChanged(null, null);
                chkFocoRedePublica7_CheckedChanged(null, null);
            }

        }

        protected void chkRedePrivada_CheckedChanged(object sender, EventArgs e)
        {
            trFocosRedePrivada.Visible = chkRedePrivada.Checked;
            if (!trFocosRedePrivada.Visible)
            {
                chkFocoRedePrivada1.Checked = false;
                chkFocoRedePrivada2.Checked = false;
                chkFocoRedePrivada3.Checked = false;
                chkFocoRedePrivada4.Checked = false;
                chkFocoRedePrivada5.Checked = false;
                chkFocoRedePrivada6.Checked = false;
                chkFocoRedePrivada7.Checked = false;
                chkFocoRedePrivada1_CheckedChanged(null, null);
                chkFocoRedePrivada2_CheckedChanged(null, null);
                chkFocoRedePrivada3_CheckedChanged(null, null);
                chkFocoRedePrivada4_CheckedChanged(null, null);
                chkFocoRedePrivada5_CheckedChanged(null, null);
                chkFocoRedePrivada6_CheckedChanged(null, null);
                chkFocoRedePrivada7_CheckedChanged(null, null);
            }
        }

        protected void chkNaoHaMonitoramento_CheckedChanged(object sender, EventArgs e)
        {
            chkRedePublica.Enabled = chkRedePrivada.Enabled = !chkNaoHaMonitoramento.Checked;
            if (chkNaoHaMonitoramento.Checked)
            {
                chkRedePublica.Checked = chkRedePrivada.Checked = false;
                chkRedePublica_CheckedChanged(null, null);
                chkRedePrivada_CheckedChanged(null, null);
            }
        }

        protected void chkFocoRedePublica1_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePublica1.Visible = chkFocoRedePublica1.Checked;
            if (!chkFocoRedePublica1.Checked)
                rblPeriodicidadeFocoRedePublica1.SelectedIndex = -1;

        }

        protected void chkFocoRedePublica2_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePublica2.Visible = chkFocoRedePublica2.Checked;
            if (!chkFocoRedePublica2.Checked)
                rblPeriodicidadeFocoRedePublica2.SelectedIndex = -1;
        }

        protected void chkFocoRedePublica3_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePublica3.Visible = chkFocoRedePublica3.Checked;
            if (!chkFocoRedePublica3.Checked)
                rblPeriodicidadeFocoRedePublica3.SelectedIndex = -1;
        }

        protected void chkFocoRedePublica4_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePublica4.Visible = chkFocoRedePublica4.Checked;
            if (!chkFocoRedePublica4.Checked)
                rblPeriodicidadeFocoRedePublica4.SelectedIndex = -1;
        }

        protected void chkFocoRedePublica5_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePublica5.Visible = chkFocoRedePublica5.Checked;
            if (!chkFocoRedePublica5.Checked)
                rblPeriodicidadeFocoRedePublica5.SelectedIndex = -1;
        }

        protected void chkFocoRedePublica6_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePublica6.Visible = chkFocoRedePublica6.Checked;
            if (!chkFocoRedePublica6.Checked)
                rblPeriodicidadeFocoRedePublica6.SelectedIndex = -1;
        }

        protected void chkFocoRedePublica7_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePublica7.Visible = chkFocoRedePublica7.Checked;
            if (!chkFocoRedePublica7.Checked)
                rblPeriodicidadeFocoRedePublica7.SelectedIndex = -1;
        }

        protected void chkFocoRedePrivada1_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePrivada1.Visible = chkFocoRedePrivada1.Checked;
            if (!chkFocoRedePrivada1.Checked)
                rblPeriodicidadeFocoRedePrivada1.SelectedIndex = -1;

        }

        protected void chkFocoRedePrivada2_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePrivada2.Visible = chkFocoRedePrivada2.Checked;
            if (!chkFocoRedePrivada2.Checked)
                rblPeriodicidadeFocoRedePrivada2.SelectedIndex = -1;
        }

        protected void chkFocoRedePrivada3_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePrivada3.Visible = chkFocoRedePrivada3.Checked;
            if (!chkFocoRedePrivada3.Checked)
                rblPeriodicidadeFocoRedePrivada3.SelectedIndex = -1;
        }

        protected void chkFocoRedePrivada4_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePrivada4.Visible = chkFocoRedePrivada4.Checked;
            if (!chkFocoRedePrivada4.Checked)
                rblPeriodicidadeFocoRedePrivada4.SelectedIndex = -1;
        }

        protected void chkFocoRedePrivada5_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePrivada5.Visible = chkFocoRedePrivada5.Checked;
            if (!chkFocoRedePrivada5.Checked)
                rblPeriodicidadeFocoRedePrivada5.SelectedIndex = -1;
        }

        protected void chkFocoRedePrivada6_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePrivada6.Visible = chkFocoRedePrivada6.Checked;
            if (!chkFocoRedePrivada6.Checked)
                rblPeriodicidadeFocoRedePrivada6.SelectedIndex = -1;
        }

        protected void chkFocoRedePrivada7_CheckedChanged(object sender, EventArgs e)
        {
            trPeriodicidadeFocoRedePrivada7.Visible = chkFocoRedePrivada7.Checked;
            if (!chkFocoRedePrivada7.Checked)
                rblPeriodicidadeFocoRedePrivada7.SelectedIndex = -1;
        }

        protected void chkProcedimento1_CheckedChanged(object sender, EventArgs e)
        {
            trInstrumentos1.Visible = chkProcedimento1.Checked;
        }

        protected void chkProcedimento2_CheckedChanged(object sender, EventArgs e)
        {
            trInstrumentos2.Visible = chkProcedimento2.Checked;
        }

        protected void chkProcedimento3_CheckedChanged(object sender, EventArgs e)
        {
            trInstrumentos3.Visible = chkProcedimento3.Checked;
        }

        protected void chkProcedimento4_CheckedChanged(object sender, EventArgs e)
        {
            trInstrumentos4.Visible = chkProcedimento4.Checked;
        }

        protected void rblResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            trMeiosDivulgacao.Visible = rblResultados.SelectedValue == "1";
        }
    }
}