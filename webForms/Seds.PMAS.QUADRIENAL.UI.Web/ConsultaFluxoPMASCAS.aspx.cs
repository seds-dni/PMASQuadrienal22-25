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

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class ConsultaFluxoPMASCAS : System.Web.UI.Page
    {
        #region Propriedades
        private List<int> Exercicios = new List<int> { 2022, 2023, 2024, 2025 }; 
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);
                carregarCombos();

                if (Request.QueryString.AllKeys.Any(t => t == "msg"))
                {
                    if (Request.QueryString["msg"] == "DO")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Desbloqueio do Orgão Gestor realizado com sucesso!"), true);
                    else if (Request.QueryString["msg"] == "DC")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK("Desbloqueio do CMAS realizado com sucesso!"), true);
                }
            }
        }

        void load()
        {
            var lst = new List<ConsultaFluxoInfo>();
            var municipios = new List<Int32>();

            if (ddlMunicipio.SelectedIndex != 0)
                municipios.Add(Convert.ToInt32(ddlMunicipio.SelectedValue));
            else if (ddlDrads.SelectedIndex != 0)
                municipios.AddRange(ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.IdDrads == Convert.ToInt32(ddlDrads.SelectedValue)).Select(m => m.Id).ToList());

            using (var proxy = new ProxyPrefeitura())
            {
                if ((SessaoPmas.UsuarioLogado.Perfil == "Administrador" && Convert.ToInt32(ddlSituacao.SelectedValue) > 0) || (SessaoPmas.UsuarioLogado.Perfil == "CAS" && Convert.ToInt32(ddlSituacao.SelectedValue) > 0))
                {
                    lst = proxy.Service.GetConsultaFluxo(municipios.ToList()).Where(c => c.Situacao.Id == Convert.ToInt32(ddlSituacao.SelectedValue)).ToList();
                }
                else
                {
                    lst = proxy.Service.GetConsultaFluxo(municipios.ToList()).ToList();
                }

                lst.ForEach((c) =>
                {
                    #region Execucao Financeira
                    int idPrefeitura = c.IdPrefeitura;
                    var quadrosDeExecucaoFinanceira = proxy.Service.GetPrefeituraSituacaoQuadro(c.IdPrefeitura, 143);
                    
                    if (quadrosDeExecucaoFinanceira != null && quadrosDeExecucaoFinanceira.Count() > 0)
                    {
                        ObterSituacaoExecucaoFinanceira(c, quadrosDeExecucaoFinanceira);
                    }
                    else 
                    {
                        quadrosDeExecucaoFinanceira.Add(new PrefeituraSituacaoQuadroInfo
                        {
                              Exercicio = Exercicios[0]
                            , IdSituacaoQuadro = 0
                            , IdPrefeitura = c.IdPrefeitura
                        });

                        quadrosDeExecucaoFinanceira.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[1]
                            ,
                            IdSituacaoQuadro = 0
                            , IdPrefeitura = c.IdPrefeitura
                        });
                        quadrosDeExecucaoFinanceira.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[2]
                            ,
                            IdSituacaoQuadro = 0
                            ,
                            IdPrefeitura = c.IdPrefeitura
                        });
                        quadrosDeExecucaoFinanceira.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[3]
                            ,
                            IdSituacaoQuadro = 0
                            ,
                            IdPrefeitura = c.IdPrefeitura
                        });
                        ObterSituacaoExecucaoFinanceira(c, quadrosDeExecucaoFinanceira);

                    }

                    #endregion

                    #region Lei Orcamentaria

                    var quadrosLeiOrcamentaria = proxy.Service.GetPrefeituraSituacaoQuadro(c.IdPrefeitura, 160);

                    if (quadrosLeiOrcamentaria != null && quadrosLeiOrcamentaria.Count() > 0)
                    {
                        ObterSituacaoLeiOrcamentaria(c, quadrosLeiOrcamentaria);
                    }
                    else
                    {
                        quadrosLeiOrcamentaria.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[0]
                            ,
                            IdSituacaoQuadro = 0
                            ,
                            IdPrefeitura = c.IdPrefeitura
                        });

                        quadrosLeiOrcamentaria.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[1]
                            ,
                            IdSituacaoQuadro = 0
                            ,
                            IdPrefeitura = c.IdPrefeitura
                        });

                        quadrosLeiOrcamentaria.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[2]
                            ,
                            IdSituacaoQuadro = 0
                            ,
                            IdPrefeitura = c.IdPrefeitura
                        });

                        quadrosLeiOrcamentaria.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[3]
                            ,
                            IdSituacaoQuadro = 0
                            ,
                            IdPrefeitura = c.IdPrefeitura
                        });

                        ObterSituacaoLeiOrcamentaria(c, quadrosLeiOrcamentaria);
                    }
                    #endregion

                    #region [Prestação de contas]
                    var quadrosPrestacaoDeContas = proxy.Service.GetPrefeituraSituacaoQuadro(c.IdPrefeitura, 168);
                    if (quadrosPrestacaoDeContas != null && quadrosPrestacaoDeContas.Count() > 0)
                    {
                        ObterSituacaoPrestacaoDeContas(c, quadrosPrestacaoDeContas);
                    }
                    else
                    {
                        quadrosPrestacaoDeContas.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[0]
                            ,
                            IdSituacaoQuadro = 0
                            ,
                            IdPrefeitura = c.IdPrefeitura
                        });

                        quadrosPrestacaoDeContas.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[1]
                            ,
                            IdSituacaoQuadro = 0
                            ,
                            IdPrefeitura = c.IdPrefeitura
                        });

                        quadrosPrestacaoDeContas.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[2]
                            ,
                            IdSituacaoQuadro = 0
                            ,
                            IdPrefeitura = c.IdPrefeitura
                        });

                        quadrosPrestacaoDeContas.Add(new PrefeituraSituacaoQuadroInfo
                        {
                            Exercicio = Exercicios[3]
                            ,
                            IdSituacaoQuadro = 0
                            ,
                            IdPrefeitura = c.IdPrefeitura
                        });

                        ObterSituacaoPrestacaoDeContas(c, quadrosPrestacaoDeContas);
                    }

                    #endregion

                });
            }

            lst.ForEach(c => c.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.First(m => m.Id == c.IdMunicipio).Nome);
            lst.ForEach(c => c.Drads = ProxyDivisaoAdministrativa.Drads.First(d => d.Id == ProxyDivisaoAdministrativa.MunicipiosEstaduais.First(m => m.Id == c.IdMunicipio).IdDrads).Nome);

            //Session["ConsultaFluxo"] = lst.OrderBy(c => c.Municipio);

            if (rdbPMAS.Checked)
            {
                lstPMAS.DataSource = null;
                lstPMAS.DataBind();

                lstPMAS2.DataSource = null;
                lstPMAS2.DataBind();

                lstPMAS3.DataSource = null;
                lstPMAS3.DataBind();

                lstPMAS.DataSource = lst.OrderBy(c => c.Municipio);
                lstPMAS.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "ConsultarPMASdisplayBuscador", "seds.inicio.ConsultarPMAS.displayBuscador()", true);
            }

            if (rdbEFLO.Checked)
            {
                lstPMAS.DataSource = null;
                lstPMAS.DataBind();

                lstPMAS3.DataSource = null;
                lstPMAS3.DataBind();


                lstPMAS2.DataSource = lst.OrderBy(c => c.Municipio);
                lstPMAS2.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "ConsultarPMAShideBuscador", "seds.inicio.ConsultarPMAS.hideBuscador()", true);
            }
            if (rdbPrestacaoDeContas.Checked)
            {
                lstPMAS.DataSource = null;
                lstPMAS.DataBind();

                lstPMAS2.DataSource = null;
                lstPMAS2.DataBind();

                lstPMAS3.DataSource = lst.OrderBy(c => c.Municipio);
                lstPMAS3.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "ConsultarPMAShideBuscador", "seds.inicio.ConsultarPMAS.hideBuscador()", true);                
            }


        }


        private static void ObterSituacaoLeiOrcamentaria(ConsultaFluxoInfo c, List<PrefeituraSituacaoQuadroInfo> quadrosLeiOrcamentaria)
        {
            
            foreach (var quadroLeiOrcamentaria in quadrosLeiOrcamentaria)
            {
                ConsultaFluxoInfo.QuadroLeiOrcamentariaInner quadro = new ConsultaFluxoInfo.QuadroLeiOrcamentariaInner();
                quadro.Exercicio = quadroLeiOrcamentaria.Exercicio;

                if (quadro.Exercicio >= 2020)
                {
                    switch (quadroLeiOrcamentaria.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            quadro.SituacaoQuadroLeiOrcamentaria = "Em Preenchimento";
                            break;
                        case (int)ESituacaoQuadro.EmAnaliseCMAS:
                            quadro.SituacaoQuadroLeiOrcamentaria = "Em análise pelo CMAS";
                            break;
                        case (int)ESituacaoQuadro.AprovadoCMAS:
                            quadro.SituacaoQuadroLeiOrcamentaria = "Aprovado pelo CMAS";
                            break;
                        case (int)ESituacaoQuadro.DevolvidoCMAS:
                            quadro.SituacaoQuadroLeiOrcamentaria = "Devolvido CMAS";
                            break;
                        case (int)ESituacaoQuadro.RejeitadoCMAS:
                            quadro.SituacaoQuadroLeiOrcamentaria = "Rejeitado pelo CMAS";
                            break;
                        default:
                            quadro.SituacaoQuadroLeiOrcamentaria = "Bloqueado";
                            break;
                    }
                }
                else
                {
                    switch (quadroLeiOrcamentaria.IdSituacaoQuadro)
                    {
                        case (int)ESituacaoQuadro.Pendente:
                            quadro.SituacaoQuadroLeiOrcamentaria = "Pendente";
                            break;
                        case (int)ESituacaoQuadro.Preenchido:
                            quadro.SituacaoQuadroLeiOrcamentaria = "Em análise DRADS";
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
                            quadro.SituacaoQuadroLeiOrcamentaria = "Não informado";
                            break;
                    }
                }
                c.QuadrosLeiOrcamentariaInner.Add(quadro);
            }
        }

        private static void ObterSituacaoPrestacaoDeContas(ConsultaFluxoInfo c, List<PrefeituraSituacaoQuadroInfo> quadrosPrestacaoDeContas) 
        {
            foreach (var  quadroPrestacaoDeContas in quadrosPrestacaoDeContas)
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

        private static void ObterSituacaoExecucaoFinanceira(ConsultaFluxoInfo c, List<PrefeituraSituacaoQuadroInfo> quadrosExecucaoFinanceira)
        {
            foreach (var quadroExecucaoFinanceira in quadrosExecucaoFinanceira)
            {
                ConsultaFluxoInfo.QuadroExecucaoFinanceiraInner quadro = new ConsultaFluxoInfo.QuadroExecucaoFinanceiraInner();
                quadro.Exercicio = quadroExecucaoFinanceira.Exercicio;

                if (quadro.Exercicio >= 2021)
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
                            quadro.SituacaoQuadroExecucaoFinanceira = "Devolvido pelo CMAS";
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
                            quadro.SituacaoQuadroExecucaoFinanceira = "Em análise DRADS";
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
                        default:
                            quadro.SituacaoQuadroExecucaoFinanceira = "Não informado";
                            break;
                    }
                }



                c.QuadrosExecucaoFinanceiraInner.Add(quadro);
            }
        }

        void carregarCombos()
        {
            //tdSituacao.Visible = tdLstSituacao.Visible = SessaoPmas.UsuarioLogado.Perfil == "Administrador";

            ddlDrads.DataSource = ProxyDivisaoAdministrativa.Drads;
            ddlDrads.DataTextField = "Nome";
            ddlDrads.DataValueField = "Id";
            ddlDrads.DataBind();
            ddlDrads.Items.Insert(0, new ListItem("Todos", "0", true));
            ddlDrads.SelectedIndex = 0;

            ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
            ddlMunicipio.DataTextField = "Nome";
            ddlMunicipio.DataValueField = "Id";
            ddlMunicipio.DataBind();
            ddlMunicipio.Items.Insert(0, new ListItem("Todos", "0", true));
            ddlMunicipio.SelectedIndex = 0;
        }

        void carregarComboMunicipioByDrads()
        {
            if (ddlDrads.SelectedIndex == 0)
                ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
            else
                ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.IdDrads == Convert.ToInt32(ddlDrads.SelectedValue)).ToList();
            ddlMunicipio.DataBind();
            ddlMunicipio.Items.Insert(0, new ListItem("Todos", "0", true));
            ddlMunicipio.SelectedIndex = 0;
        }

        protected void ddlDrads_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregarComboMunicipioByDrads();
        }

        protected void lstExecucaoLeiOrcamentaria_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (ConsultaFluxoInfo)e.Item.DataItem;
                //int sequencia = e.Item.DataItemIndex + 1;
            }           
        }

        

        protected void lstPMAS_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (ConsultaFluxoInfo)e.Item.DataItem;
                int sequencia = e.Item.DataItemIndex + 1;

                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                //  Label lblParecerDrads = (Label)e.Item.FindControl("lblParecerDrads");

                

                if ((item.Situacao.Id == (int)ESituacao.Aprovado || item.Situacao.Id == (int)ESituacao.Rejeitado) && SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.Administrador)
                {
                    e.Item.FindControl("lkbDesbloqueioOrgaoGestor").Visible = true;
                    e.Item.FindControl("lkbDesbloqueioCMAS").Visible = true;
                    e.Item.FindControl("lkdDesbloqueioReprogramacao").Visible = true;
                }
                else if (SessaoPmas.UsuarioLogado.EnumPerfil == EPerfil.CAS)
                {
                    e.Item.FindControl("pnlPerfilADMItemTemplate").Visible = false;
                    lstPMAS.FindControl("pnlPerfilADMHeader").Visible = false;
                    e.Item.FindControl("lkbDesbloqueioCMAS").Visible = false;
                    e.Item.FindControl("lkbDesbloqueioOrgaoGestor").Visible = false;
                    e.Item.FindControl("lkdDesbloqueioReprogramacao").Visible = false;
                }
                else {
                    e.Item.FindControl("pnlPerfilADMItemTemplate").Visible = false;
                    lstPMAS.FindControl("pnlPerfilADMHeader").Visible = false;
                    e.Item.FindControl("lkbDesbloqueioCMAS").Visible = false;
                    e.Item.FindControl("lkbDesbloqueioOrgaoGestor").Visible = false;
                    e.Item.FindControl("lkdDesbloqueioReprogramacao").Visible = false;

                }

                if (item.DesbloquearValoresDrads.HasValue && item.DesbloquearValoresDrads.Value == true)
                {
                    //  lblParecerDrads.Text = "Desbloqueado";
                    e.Item.FindControl("lkdDesbloqueioReprogramacao").Visible = false;
                }

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            load();
        }

        protected void lstPMAS_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var key = lstPMAS.DataKeys[e.Item.DataItemIndex];

            switch (e.CommandName)
            {
                case "Visualizar_Municipio":
                    using (var proxy = new ProxyPrefeitura())
                    {
                        SessaoPmas.UsuarioLogado.Prefeitura = proxy.Service.GetPrefeituraById(Convert.ToInt32(key["IdPrefeitura"]));
                        SessaoPmas.UsuarioLogado.Prefeitura.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.FirstOrDefault(m => m.Id == Convert.ToInt32(key["IdMunicipio"]));
                        SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Drads = ProxyDivisaoAdministrativa.Drads.FirstOrDefault(d => d.Id == SessaoPmas.UsuarioLogado.Prefeitura.Municipio.IdDrads);
                    }
                    Response.Redirect("~/Default.aspx");
                    break;

                case "Autorizar_Desbloqueio_OrgaoGestor":
                    Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlEncode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.AutorizaDesbloqueioGestor).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
                    break;

                case "Autorizar_Desbloqueio_CMAS":
                    Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlEncode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.AutorizaDesbloqueioCMAS).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
                    break;

                case "Autorizar_Desbloqueio_Reprogramacao":
                    Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlDecode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.AutorizaDesbloqueioReprogramacao).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
                    break;
                case "Autorizar_Desbloqueio_Demandas":
                    Response.Redirect("~/FFluxo.aspx?idSituacao=" + Server.UrlDecode(clsCrypto.Encrypt(Convert.ToInt32(ESituacao.AutorizaDesbloqueioDemandas).ToString())) + "&idPrefeitura=" + Server.UrlEncode(clsCrypto.Encrypt(key["IdPrefeitura"].ToString())));
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

    }
}