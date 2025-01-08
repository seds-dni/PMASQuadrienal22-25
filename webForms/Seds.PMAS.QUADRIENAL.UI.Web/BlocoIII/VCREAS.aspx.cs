using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class VCREAS : System.Web.UI.Page
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

                if (String.IsNullOrEmpty(Request.QueryString["idUnidade"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx");
                    return;
                }


                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    load(proxy);
                }

            }
        }


        void load(ProxyRedeProtecaoSocial proxy)
        {
            if (SessaoPmas.UsuarioLogado.Prefeitura != null && SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio == 565) //Mostrar os distritos para São Paulo
                trDistritoSP.Visible = true;

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
                return;
            var creas = proxy.Service.GetCREASPorId(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosByCREAS(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Where(p => p.IdUsuarioTipoServico == 39 || p.IdUsuarioTipoServico == 37 || p.IdUsuarioTipoServico == 41).ToList();

            if (creas == null)
                return;

            txtNome.Text = creas.Nome;
            txtIDCRAS.Text = creas.IDCREAS;
            txtCoordenador.Text = creas.PossuiCoordenador ? creas.Coordenador : "Não Possui coordenador";

            tdEscolaridade.Visible = tdFormacaoAcademica.Visible = creas.PossuiCoordenador;
            if (creas.IdEscolaridadeCoordenador.HasValue)
            {
                tdEscolaridade.Visible = true;
                txtEscolaridade.Text = new ProxyEstruturaAssistenciaSocial().Service.GetEscolaridades().Where(c => c.Id == creas.IdEscolaridadeCoordenador).FirstOrDefault().Nome;
                tdFormacaoAcademica.Visible = creas.IdEscolaridadeCoordenador == 4;
                if (creas.IdEscolaridadeCoordenador == 4)
                {
                    txtFormacaoAcademica.Text = new ProxyEstruturaAssistenciaSocial().Service.GetFormacoesAcademicas().Where(c => c.Id == creas.IdFormacaoCoordenador).FirstOrDefault().Nome;
                    //Outra Formação
                    if (creas.IdFormacaoCoordenador == 7)
                    {
                        txtOutraAreaFormacao.Text = creas.OutraFormacaoCoordenador;
                        trOutraFormacao.Visible = true;
                    }
                }
            }

            if (SessaoPmas.UsuarioLogado.Perfil == Convert.ToString(EPerfil.Convidados) && servicos.Count > 0)
            {
                lblCep.Text = string.Empty;
                lblLogradouro.Text = "Endereço Sigiloso";
                lblNumero.Text =
                lblComplemento.Text =
                lblBairro.Text =
                lblCidade.Text =
                lblTelefone.Text =
                lblCelular.Text =
                txtEmailInstitucional.Text =
                lblDistrito.Text = String.Empty;
            }
            else
            {
                lblCep.Text = creas.CEP.Substring(creas.CEP.Length - 8, 8);
                lblCep.Text = creas.CEP.Insert(5, "-");
                lblLogradouro.Text = creas.Logradouro;
                lblNumero.Text = creas.Numero;
                lblComplemento.Text = creas.Complemento;
                lblBairro.Text = creas.Bairro;
                lblCidade.Text = creas.Cidade;
                if (!String.IsNullOrEmpty(creas.Telefone))
                {
                    string sDDD, sTelefone;
                    string sTelefoneCompleto = creas.Telefone;
                    sTelefoneCompleto = "0000000000" + sTelefoneCompleto;
                    sTelefoneCompleto = sTelefoneCompleto.Substring(sTelefoneCompleto.Length - 10, 10);
                    sDDD = sTelefoneCompleto.Substring(0, 2);
                    sTelefone = sTelefoneCompleto.Substring(2, 8);
                    sTelefone = sTelefone.Insert(4, "-");
                    lblTelefone.Text = "(" + sDDD + ") " + sTelefone;
                }

                if (!string.IsNullOrEmpty(creas.Celular))
                {
                    string sCelularCompleto = creas.Celular;
                    string sDDDCelular, sCelular = "";

                    sCelularCompleto = "00000000000" + sCelularCompleto;
                    sCelularCompleto = sCelularCompleto.Substring(sCelularCompleto.Length - 11, 11);

                    sDDDCelular = sCelularCompleto.Substring(0, 2);
                    sCelular = sCelularCompleto.Substring(2, 9);
                    sCelular = sCelular.Insert(5, "-");

                    lblCelular.Text = "(" + sDDDCelular + ") " + sCelular;
                }

                txtEmailInstitucional.Text = creas.Email;
                if (creas.IdDistritoSaoPaulo.HasValue)
                    lblDistrito.Text = new ProxyEstruturaAssistenciaSocial().Service.GetDistritosSP().Where(s => s.Id == creas.IdDistritoSaoPaulo.Value).FirstOrDefault().Nome;
            }

            txtCapacidadeAtendimento.Text = creas.CapacidadeAtendimento.ToString();
            txtNumeroAtendidos.Text = creas.NumeroAtendidos.ToString();
            switch (creas.IdTipoImovel)
            {
                case 1:
                    lblImovel.Text = "Próprio";
                    break;
                case 2:
                    lblImovel.Text = "Cedido";
                    break;
                case 3:
                    lblImovel.Text = "Alugado";
                    break;
            }
            txtEmailInstitucional.Text = creas.Email;
            txtTrabalhadoresRemunerados.Text = creas.TotalRemunerados.HasValue ? creas.TotalRemunerados.ToString() : String.Empty;
            txtVoluntarios.Text = creas.TotalVoluntarios.HasValue ? creas.TotalVoluntarios.ToString() : String.Empty;
            txtEstagiarios.Text = creas.TotalEstagiarios.HasValue ? creas.TotalEstagiarios.ToString() : String.Empty;
            txtDataImplantacao.Text = creas.DataImplantacao.HasValue ? creas.DataImplantacao.Value.ToShortDateString() : "";
            if (creas.Desativado)
            {
                lblDataExclusaoRegistro.Text = creas.DataDesativacao.HasValue ? creas.DataDesativacao.Value.ToShortDateString() : creas.DataRegistroLog.Value.ToShortDateString();
                lblMotivoExclusao.Text = new ProxyRedeProtecaoSocial().Service.GetMotivoDesativacaoLocalById(creas.IdMotivoDesativacao.Value).Descricao;
                if (creas.IdMotivoDesativacao == 16)
                {
                    trMotivoEncerramento.Visible = trDetalhamento.Visible = true;
                    lblMotivoEncerramento.Text = new ProxyRedeProtecaoSocial().Service.GetMotivoDesativacaoLocalById(creas.IdMotivoEncerramento.Value).Descricao;
                    lblDetalhamentoEncerramento.Text = creas.Detalhamento;
                }
            }

            lblAtendeUsuarios.Text = creas.AtendeOutrosMunicipios ? "Sim" : "Não";
            if (creas.AtendeOutrosMunicipios)
            {
                lstAtendidos.Visible = creas.AtendeOutrosMunicipios;
                lstMunicipiosAtendidos.DataSource = creas.AbrangenciaMunicipios;
                lstMunicipiosAtendidos.DataBind();
            }
            lblServicoPAIF.Text = creas.PossuiPAEFI ? "Sim" : "Não";

            var servicoPAEFI = proxy.Service.GetConsultaServicosRecursosFinanceirosByCREAS(creas.Id).Where(s => s.IdTipoServico == 135).Count();
            if (!creas.PossuiPAEFI & servicoPAEFI == 0 && creas.JustificativaPAEFI != null)
                txtJustificativaPAIF.Text = creas.JustificativaPAEFI;
            else
                trJustificativaPAIF.Visible = false;

            if (creas.AcoesSocioAssistenciais != null && creas.AcoesSocioAssistenciais.Count > 0)
            {
                lstAcoesSocioAssistenciais.DataSource = creas.AcoesSocioAssistenciais;
                lstAcoesSocioAssistenciais.DataBind();
            }

            if (creas.IdAvaliacaoLocalExecucao.HasValue)
                lblAvaliacaoLocalExecucao.Text = new ProxyEstruturaAssistenciaSocial().Service.GetAvaliacoesLocal().Where(s => s.Id == creas.IdAvaliacaoLocalExecucao.Value).FirstOrDefault().Descricao;

            if (creas.IdDistritoSaoPaulo.HasValue)
                lblDistrito.Text = new ProxyEstruturaAssistenciaSocial().Service.GetDistritosSP().Where(s => s.Id == creas.IdDistritoSaoPaulo.Value).FirstOrDefault().Nome;
        }




        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CCREASDesativados.aspx?IdUnidade=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }

    }
}