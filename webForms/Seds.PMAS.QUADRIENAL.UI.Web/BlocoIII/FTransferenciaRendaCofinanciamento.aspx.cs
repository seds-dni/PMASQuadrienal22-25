using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FTransferenciaRendaCofinanciamento : System.Web.UI.Page
    {
        private List<Int32> _lstTipoUnidade = new List<int>();  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["lstTipoUnidade"] != null)  
            {
                _lstTipoUnidade = (List<Int32>)ViewState["lstTipoUnidade"];
            }

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
                    Response.Redirect("~/BlocoIII/CProgramasProjetos.aspx");
                    return;
                }

                if (!String.IsNullOrEmpty(Request.QueryString["acao"]))
                {
                    btnVoltar.PostBackUrl = "~/BlocoIII/CBeneficiosContinuados.aspx";
                }

                using (var proxy = new ProxyProgramas())
                {
                    load(proxy);
                }

            }

        }

        void load(ProxyProgramas proxy)
        {
            var idTransferenciaRenda = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

            var apenasServicosComTranferenciaRenda = proxy.Service.GetConsultaTransferenciaRendaCofinanciamentoByTransferenciaRenda(idTransferenciaRenda)
                .OrderBy(t => t.IdTipoProtecao)
                .GroupBy(s => s.ProtecaoSocial)
                .Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) }).ToList();

            lstRecursos.DataSource = apenasServicosComTranferenciaRenda;
            lstRecursos.DataBind();

            var transferenciaRenda = proxy.Service.GetTransferenciaRendaById(idTransferenciaRenda);
            if (transferenciaRenda != null)
            {
                lblTransferenciaRenda.Text = transferenciaRenda.Nome;

                if (transferenciaRenda.IdTipoTransferenciaRenda == 4)
                    btnVoltar.PostBackUrl = "~/BlocoIII/CProgramasProjetos.aspx";

                verificarAlteracoes(idTransferenciaRenda, GetQuadro(transferenciaRenda));
            }
        }

        void verificarAlteracoes(Int32 idTransferenciaRenda, Int32 idQuadro)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, idQuadro, idTransferenciaRenda);
                    linkAlteracoesQuadro.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idQuadro.ToString())) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idTransferenciaRenda.ToString()));
                }
            }
        }

        private Int32 GetQuadro(TransferenciaRendaInfo transferenciaRenda)
        {
            switch ((ETipoTransferenciaRenda)transferenciaRenda.IdTipoTransferenciaRenda)
            {
                case ETipoTransferenciaRenda.BolsaFamilia:
                case ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil: return 75;
                case ETipoTransferenciaRenda.AcaoJovem:
                case ETipoTransferenciaRenda.RendaCidada:
                case ETipoTransferenciaRenda.SaoPauloSolidario: return 76;
                case ETipoTransferenciaRenda.Outros: return 77;
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia:
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso: return 79;

            }
            return 0;
        }

        protected void lstRecursos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                using (var proxy = new ProxyProgramas())
                {
                    proxy.Service.DeleteTransferenciaRendaCofinanciamento(Convert.ToInt32(e.CommandArgument));
                    load(proxy);
                }
            }
        }

        protected void lstItems_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Permissao.VerificarPermissaoControles(new[] { ((ImageButton)e.Item.FindControl("btnExcluir")) }, Session);
            }
        }

        //protected void ddlTipoExecutora_SelectedIndexChanged(object sender, EventArgs e)  
        //{          
        //    using (var proxy = new ProxyRedeProtecaoSocial())
        //    {
        //        switch (Convert.ToInt32(ddlTipoExecutora.SelectedValue))
        //        {
        //            case 1: carregarUnidadesPublicas(proxy);
        //                tbLocalExecucao.Visible = true;
        //                break;
        //            case 2: carregarUnidadesPrivadas(proxy);
        //                tbLocalExecucao.Visible = true;
        //                break;
        //            //case 3: carregarCRAS(proxy);
        //            //    tbLocalExecucao.Visible = false;
        //            //    break;
        //            //case 4: carregarCREAS(proxy);
        //            //    tbLocalExecucao.Visible = false;
        //            //    break;
        //            //case 5: carregarCentroPOP(proxy);
        //            //    tbLocalExecucao.Visible = false;
        //            //    break;
        //        }
        //    }

        //    ddlLocalExecucao.DataValueField = "Id";
        //    ddlLocalExecucao.DataTextField = "Descricao";
        //    ddlLocalExecucao.DataSource = new List<LocalExecucaoPublicoInfo>();
        //    ddlLocalExecucao.DataBind();
        //    Util.InserirItemEscolha(ddlLocalExecucao);

        //    ddlServico.DataValueField = "Id";
        //    ddlServico.DataTextField = "Descricao";
        //    ddlServico.DataSource = new List<ServicoRecursoFinanceiroPublicoInfo>();
        //    ddlServico.DataBind();
        //    Util.InserirItemEscolha(ddlServico);
        //}

        //void carregarUnidadesPublicas(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlExecutora.DataValueField = "Id";
        //    ddlExecutora.DataTextField = "Descricao";
        //    ddlExecutora.DataSource = proxy.Service.GetIdentificacaoUnidadesPublicaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, null);
        //    ddlExecutora.DataBind();
        //    Util.InserirItemEscolha(ddlExecutora);
        //}

        //void carregarUnidadesPrivadas(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlExecutora.DataValueField = "Id";
        //    ddlExecutora.DataTextField = "Descricao";
        //    ddlExecutora.DataSource = proxy.Service.GetIdentificacaoUnidadesPrivadaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id, null, null);
        //    ddlExecutora.DataBind();
        //    Util.InserirItemEscolha(ddlExecutora);
        //}

        //void carregarLocalExecucaoPrivado(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlLocalExecucao.DataValueField = "Id";
        //    ddlLocalExecucao.DataTextField = "Descricao";
        //    ddlLocalExecucao.DataSource = proxy.Service.GetIdentificacaoLocalExecucaoPrivadoByUnidade(Convert.ToInt32(ddlExecutora.SelectedValue));
        //    ddlLocalExecucao.DataBind();
        //    Util.InserirItemEscolha(ddlLocalExecucao);
        //}
        void carregarLocalExecucaoPublico(ProxyRedeProtecaoSocial proxy)
        {
            //ddlLocalExecucao.DataValueField = "Id";
            //ddlLocalExecucao.DataTextField = "Descricao";


            //ddlLocalExecucao.DataSource = proxy.Service.GetIdentificacaoLocalExecucaoPublicoByUnidade(Convert.ToInt32(ddlExecutora.SelectedValue));            
            //ddlLocalExecucao.DataBind();
            //Util.InserirItemEscolha(ddlLocalExecucao);

            //Bruno .V
            //var localExecucaoPulico = proxy.Service.GetIdentificacaoLocalExecucaoPublicoByUnidade(Convert.ToInt32(ddlExecutora.SelectedValue));
            //var localExecucaoCRAS = proxy.Service.GetIdentificacaoCRASByUnidade(Convert.ToInt32(ddlExecutora.SelectedValue), null);
            //var localExecucaoCREAS = proxy.Service.GetIdentificacaoCREASByUnidade(Convert.ToInt32(ddlExecutora.SelectedValue), null);
            //var localExecucaoCentroPOP = proxy.Service.GetIdentificacaoCentroPOPByUnidade(Convert.ToInt32(ddlExecutora.SelectedValue), null);

            var listaGeral = new[] { new { Id = 0, Descricao = "0", Tipo = 0 } }.ToList();

            listaGeral.Clear();
            //listaGeral.AddRange(localExecucaoPulico.Select(c => new { Id = c.Id, Descricao = c.Descricao, Tipo = 1 }).ToList());
            //listaGeral.AddRange(localExecucaoCRAS.Select(c => new { Id = c.Id, Descricao = c.Descricao, Tipo = 3 }).ToList());
            //listaGeral.AddRange(localExecucaoCREAS.Select(c => new { Id = c.Id, Descricao = c.Descricao, Tipo = 4 }).ToList());
            //listaGeral.AddRange(localExecucaoCentroPOP.Select(c => new { Id = c.Id, Descricao = c.Descricao, Tipo = 5 }).ToList());

            _lstTipoUnidade.Clear();
            for (int i = 0; i < listaGeral.Count; i++)
            {
                _lstTipoUnidade.Add(listaGeral[i].Tipo);
            }
            ViewState["lstTipoUnidade"] = _lstTipoUnidade;

            //ddlLocalExecucao.DataSource = listaGeral;
            //ddlLocalExecucao.DataBind();
            //Util.InserirItemEscolha(ddlLocalExecucao);
        }

        //void carregarCRAS(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlExecutora.DataValueField = "Id";
        //    ddlExecutora.DataTextField = "Descricao";
        //    ddlExecutora.DataSource = proxy.Service.GetIdentificacaoCRASByUnidade(SessaoPmas.UsuarioLogado.Prefeitura.Id, null);
        //    ddlExecutora.DataBind();
        //    Util.InserirItemEscolha(ddlExecutora);
        //}

        //void carregarCREAS(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlExecutora.DataValueField = "Id";
        //    ddlExecutora.DataTextField = "Descricao";
        //    ddlExecutora.DataSource = proxy.Service.GetIdentificacaoCREASByUnidade(SessaoPmas.UsuarioLogado.Prefeitura.Id, null);
        //    ddlExecutora.DataBind();
        //    Util.InserirItemEscolha(ddlExecutora);
        //}

        //void carregarCentroPOP(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlExecutora.DataValueField = "Id";
        //    ddlExecutora.DataTextField = "Descricao";
        //    ddlExecutora.DataSource = proxy.Service.GetIdentificacaoCentroPOPByUnidade(SessaoPmas.UsuarioLogado.Prefeitura.Id, null);
        //    ddlExecutora.DataBind();
        //    Util.InserirItemEscolha(ddlExecutora);
        //}

        //void carregarServicosCRAS(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlServico.DataValueField = "Id";
        //    ddlServico.DataTextField = "Descricao";
        //    ddlServico.DataSource = proxy.Service.GetConsultaServicosRecursosFinanceirosByCRAS(Convert.ToInt32(ddlLocalExecucao.SelectedValue)).OrderBy(t => t.IdTipoProtecao).ThenBy(t => t.TipoServico);  
        //    ddlServico.DataBind();
        //    Util.InserirItemEscolha(ddlServico);
        //}

        //void carregarServicosCREAS(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlServico.DataValueField = "Id";
        //    ddlServico.DataTextField = "Descricao";
        //    ddlServico.DataSource = proxy.Service.GetConsultaServicosRecursosFinanceirosByCREAS(Convert.ToInt32(ddlLocalExecucao.SelectedValue)).OrderBy(t => t.IdTipoProtecao).ThenBy(t => t.TipoServico);  
        //    ddlServico.DataBind();
        //    Util.InserirItemEscolha(ddlServico);
        //}

        //void carregarServicosCentroPOP(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlServico.DataValueField = "Id";
        //    ddlServico.DataTextField = "Descricao";
        //    ddlServico.DataSource = proxy.Service.GetConsultaServicosRecursosFinanceirosByCentroPOP(Convert.ToInt32(ddlLocalExecucao.SelectedValue)).OrderBy(t => t.IdTipoProtecao).ThenBy(t => t.TipoServico);  
        //    ddlServico.DataBind();
        //    Util.InserirItemEscolha(ddlServico);
        //}

        //void carregarServicosLocalExecucaoPrivado(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlServico.DataValueField = "Id";
        //    ddlServico.DataTextField = "Descricao";
        //    ddlServico.DataSource = proxy.Service.GetConsultaServicosRecursosFinanceirosPrivadoByLocalExecucao(Convert.ToInt32(ddlLocalExecucao.SelectedValue)).OrderBy(t => t.IdTipoProtecao).ThenBy(t => t.TipoServico);
        //    ddlServico.DataBind();
        //    Util.InserirItemEscolha(ddlServico);
        //}

        //void carregarServicosLocalExecucaoPublico(ProxyRedeProtecaoSocial proxy)
        //{
        //    ddlServico.DataValueField = "Id";
        //    ddlServico.DataTextField = "Descricao";
        //    ddlServico.DataSource = proxy.Service.GetConsultaServicosRecursosFinanceirosPublicoByLocalExecucao(Convert.ToInt32(ddlLocalExecucao.SelectedValue)).OrderBy(t => t.IdTipoProtecao).ThenBy(t => t.TipoServico);
        //    ddlServico.DataBind();
        //    Util.InserirItemEscolha(ddlServico);
        //}

        //protected void ddlExecutora_SelectedIndexChanged(object sender, EventArgs e)  
        //{
        //    using (var proxy = new ProxyRedeProtecaoSocial())
        //    {
        //        switch (Convert.ToInt32(ddlTipoExecutora.SelectedValue))
        //        {
        //            case 1:
        //                carregarLocalExecucaoPublico(proxy);
        //                break;
        //            case 2:
        //                carregarLocalExecucaoPrivado(proxy);
        //                break;
        //            //case 3: carregarServicosCRAS(proxy);
        //            //    break;
        //            //case 4: carregarServicosCREAS(proxy);
        //            //    break;
        //            //case 5: carregarServicosCentroPOP(proxy);
        //            //    break;
        //        }
        //    }
        //}

        //protected void ddlLocalExecucao_SelectedIndexChanged(object sender, EventArgs e)  
        //{
        //    //using (var proxy = new ProxyRedeProtecaoSocial())
        //    //{
        //    //    switch (Convert.ToInt32(ddlTipoExecutora.SelectedValue))
        //    //    {
        //    //        case 1: carregarServicosLocalExecucaoPublico(proxy);
        //    //            break;
        //    //        case 2: carregarServicosLocalExecucaoPrivado(proxy);
        //    //            break;
        //    //    }
        //    //}
        //    using (var proxy = new ProxyRedeProtecaoSocial())
        //    {
        //        switch (Convert.ToInt32(ddlTipoExecutora.SelectedValue))
        //        {
        //            case 1:
        //                if (ddlLocalExecucao.SelectedIndex == 0)
        //                {
        //                    carregarServicosLocalExecucaoPublico(proxy);
        //                }
        //                else
        //                {
        //                    switch (_lstTipoUnidade[ddlLocalExecucao.SelectedIndex - 1])
        //                    {
        //                        case 1:
        //                            carregarServicosLocalExecucaoPublico(proxy);
        //                            break;
        //                        case 3:
        //                            carregarServicosCRAS(proxy);
        //                            break;
        //                        case 4:
        //                            carregarServicosCREAS(proxy);
        //                            break;
        //                        case 5:
        //                            carregarServicosCentroPOP(proxy);
        //                            break;
        //                    }
        //                }
        //                break;
        //            case 2: carregarServicosLocalExecucaoPrivado(proxy);
        //                break;
        //        }
        //    }
        //}

        //protected void btnAdicionar_Click(object sender, EventArgs e)  
        //{
        //    try
        //    {
        //        var obj = new TransferenciaRendaCofinanciamentoInfo();
        //        obj.IdTransferenciaRenda = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));

        //        //if (ddlServico.SelectedValue != "0")
        //        //{
        //        //    switch (Convert.ToInt32(ddlTipoExecutora.SelectedValue))
        //        //    {
        //        //        case 1: obj.IdServicosRecursosFinanceirosPublico = Convert.ToInt32(ddlServico.SelectedValue);
        //        //            break;
        //        //        case 2: obj.IdServicosRecursosFinanceirosPrivado = Convert.ToInt32(ddlServico.SelectedValue);
        //        //            break;
        //        //        case 3: obj.IdServicosRecursosFinanceirosCRAS = Convert.ToInt32(ddlServico.SelectedValue);
        //        //            break;
        //        //        case 4: obj.IdServicosRecursosFinanceirosCREAS = Convert.ToInt32(ddlServico.SelectedValue);
        //        //            break;
        //        //        case 5: obj.IdServicosRecursosFinanceirosCentroPOP = Convert.ToInt32(ddlServico.SelectedValue);
        //        //            break;
        //        //    }
        //        //}

        //        if (ddlServico.SelectedValue != "0")
        //        {
        //            switch (Convert.ToInt32(ddlTipoExecutora.SelectedValue))
        //            {
        //                case 1:
        //                    switch (_lstTipoUnidade[ddlLocalExecucao.SelectedIndex - 1])  
        //                    {
        //                        case 1:
        //                            obj.IdServicosRecursosFinanceirosPublico = Convert.ToInt32(ddlServico.SelectedValue);
        //                            break;
        //                        case 3: obj.IdServicosRecursosFinanceirosCRAS = Convert.ToInt32(ddlServico.SelectedValue);
        //                            break;
        //                        case 4: obj.IdServicosRecursosFinanceirosCREAS = Convert.ToInt32(ddlServico.SelectedValue);
        //                            break;
        //                        case 5: obj.IdServicosRecursosFinanceirosCentroPOP = Convert.ToInt32(ddlServico.SelectedValue);
        //                            break;
        //                    }
        //                    break;
        //                case 2: obj.IdServicosRecursosFinanceirosPrivado = Convert.ToInt32(ddlServico.SelectedValue);
        //                    break;
        //            }
        //        }

        //        if (!String.IsNullOrEmpty(txtNumeroUsuarios.Text))
        //            obj.NumeroUsuarios = Convert.ToInt32(txtNumeroUsuarios.Text);

        //        using (var proxy = new ProxyProgramas())
        //        {
        //            proxy.Service.AddTransferenciaRendaCofinanciamento(obj);
        //            load(proxy);
        //        }

        //        ddlServico.SelectedIndex = 0;
        //        txtNumeroUsuarios.Text = String.Empty;

        //    }
        //    catch (Exception ex)
        //    {
        //        lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
        //        tbInconsistencias.Visible = true;
        //        return;
        //    }
        //    lblInconsistencias.Text = String.Empty;
        //    tbInconsistencias.Visible = false;
        //}

        protected string MontarBotao(ConsultaTransferenciaRendaCofinanciamentoInfo item)
        {
            var idTransferenciaRenda = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var page = String.Empty;
            switch (item.TipoUnidade)
            {
                case "Rede direta": page = "../BlocoIII/VServicoRecursoFinanceiroPublico.aspx"; break;
                case "Rede indireta": page = "../BlocoIII/VServicoRecursoFinanceiroPrivado.aspx"; break;
                case "CRAS": page = "../BlocoIII/VServicoRecursoFinanceiroCRAS.aspx"; break;
                case "CREAS": page = "../BlocoIII/VServicoRecursoFinanceiroCREAS.aspx"; break;
                case "Centro Pop": page = "../BlocoIII/VServicoRecursoFinanceiroCentroPOP.aspx"; break;
            }
            return "<a href='" + page + "?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.IdServicoRecursoFinanceiro.ToString())) + "&idTransferenciaRenda=" + idTransferenciaRenda + "'><img src='../Styles/Icones/find.png' alt='Visualizar' border='0' /></a>";
        }
    }
}