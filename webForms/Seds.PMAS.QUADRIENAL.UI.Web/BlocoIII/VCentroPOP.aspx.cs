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
    public partial class VCentroPOP : System.Web.UI.Page
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
            var centroPOP = proxy.Service.GetCentroPOPById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosByCentroPOP(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Where(p => p.IdUsuarioTipoServico == 39 || p.IdUsuarioTipoServico == 37 || p.IdUsuarioTipoServico == 41).ToList();

            if (centroPOP == null)
                return;

            txtNome.Text = centroPOP.Nome;
            txtIDCRAS.Text = centroPOP.IDCREAS;
            txtCoordenador.Text = centroPOP.PossuiCoordenador ? centroPOP.Coordenador : "Não Possui coordenador";

            tdEscolaridade.Visible = tdFormacaoAcademica.Visible = centroPOP.PossuiCoordenador;
            if (centroPOP.IdEscolaridadeCoordenador.HasValue)
            {
                tdEscolaridade.Visible = true;
                txtEscolaridade.Text = new ProxyEstruturaAssistenciaSocial().Service.GetEscolaridades().Where(c => c.Id == centroPOP.IdEscolaridadeCoordenador).FirstOrDefault().Nome;
                tdFormacaoAcademica.Visible = centroPOP.IdEscolaridadeCoordenador == 4;
                if (centroPOP.IdEscolaridadeCoordenador == 4)
                {
                    txtFormacaoAcademica.Text = new ProxyEstruturaAssistenciaSocial().Service.GetFormacoesAcademicas().Where(c => c.Id == centroPOP.IdFormacaoCoordenador).FirstOrDefault().Nome;
                    //Outra Formação
                    if (centroPOP.IdFormacaoCoordenador == 7)
                    {
                        txtOutraAreaFormacao.Text = centroPOP.OutraFormacaoCoordenador;
                        trOutraFormacao.Visible = true;
                    }
                }
            }

            //if (SessaoPmas.UsuarioLogado.Perfil == Convert.ToString(EPerfil.Convidados) && servicos.Count > 0)
            //{
            //    lblCep.Text = string.Empty;
            //    lblLogradouro.Text = "Endereço Sigiloso";
            //    lblNumero.Text =
            //    lblComplemento.Text =
            //    lblBairro.Text =
            //    lblCidade.Text =
            //    lblTelefone.Text =
            //    lblCelular.Text =
            //    txtEmailInstitucional.Text =
            //    lblDistrito.Text = String.Empty;
            //}
            //else
            //{
            lblCep.Text = centroPOP.CEP.Substring(centroPOP.CEP.Length - 8, 8);
            lblCep.Text = centroPOP.CEP.Insert(5, "-");
            lblLogradouro.Text = centroPOP.Logradouro;
            lblNumero.Text = centroPOP.Numero;
            lblComplemento.Text = centroPOP.Complemento;
            lblBairro.Text = centroPOP.Bairro;
            lblCidade.Text = centroPOP.Cidade;
            if (!String.IsNullOrEmpty(centroPOP.Telefone))
            {
                string sDDD, sTelefone;
                string sTelefoneCompleto = centroPOP.Telefone;
                sTelefoneCompleto = "0000000000" + sTelefoneCompleto;
                sTelefoneCompleto = sTelefoneCompleto.Substring(sTelefoneCompleto.Length - 10, 10);
                sDDD = sTelefoneCompleto.Substring(0, 2);
                sTelefone = sTelefoneCompleto.Substring(2, 8);
                sTelefone = sTelefone.Insert(4, "-");
                lblTelefone.Text = "(" + sDDD + ") " + sTelefone;
            }

            if (!string.IsNullOrEmpty(centroPOP.Celular))
            {
                string sCelularCompleto = centroPOP.Celular;
                string sDDDCelular, sCelular = "";

                sCelularCompleto = "00000000000" + sCelularCompleto;
                sCelularCompleto = sCelularCompleto.Substring(sCelularCompleto.Length - 11, 11);

                sDDDCelular = sCelularCompleto.Substring(0, 2);
                sCelular = sCelularCompleto.Substring(2, 9);
                sCelular = sCelular.Insert(5, "-");

                lblCelular.Text = "(" + sDDDCelular + ") " + sCelular;
            }

            txtEmailInstitucional.Text = centroPOP.Email;
            if (centroPOP.IdDistritoSaoPaulo.HasValue)
                lblDistrito.Text = new ProxyEstruturaAssistenciaSocial().Service.GetDistritosSP().Where(s => s.Id == centroPOP.IdDistritoSaoPaulo.Value).FirstOrDefault().Nome;

            txtCapacidadeAtendimento.Text = centroPOP.CapacidadeAtendimento.ToString();
            txtNumeroAtendidos.Text = centroPOP.NumeroAtendidos.ToString();
            switch (centroPOP.IdTipoImovel)
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
            txtEmailInstitucional.Text = centroPOP.Email;
            txtTrabalhadoresRemunerados.Text = centroPOP.TotalRemunerados.HasValue ? centroPOP.TotalRemunerados.ToString() : String.Empty;
            txtVoluntarios.Text = centroPOP.TotalVoluntarios.HasValue ? centroPOP.TotalVoluntarios.ToString() : String.Empty;
            txtEstagiarios.Text = centroPOP.TotalEstagiarios.HasValue ? centroPOP.TotalEstagiarios.ToString() : String.Empty;
            txtDataImplantacao.Text = centroPOP.DataImplantacao.HasValue ? centroPOP.DataImplantacao.Value.ToShortDateString() : "";
            if (centroPOP.Desativado)
            {
                lblDataExclusaoRegistro.Text = centroPOP.DataDesativacao.HasValue ? centroPOP.DataDesativacao.Value.ToShortDateString() : centroPOP.DataRegistroLog.Value.ToShortDateString();
                lblMotivoExclusao.Text = new ProxyRedeProtecaoSocial().Service.GetMotivoDesativacaoLocalById(centroPOP.IdMotivoDesativacao.Value).Descricao;
                if (centroPOP.IdMotivoDesativacao == 22)
                {
                    trMotivoEncerramento.Visible = trDetalhamento.Visible = true;
                    lblMotivoEncerramento.Text = new ProxyRedeProtecaoSocial().Service.GetMotivoDesativacaoLocalById(centroPOP.IdMotivoEncerramento.Value).Descricao;
                    lblDetalhamentoEncerramento.Text = centroPOP.Detalhamento;
                }
            }

            lblAtendeUsuarios.Text = centroPOP.AtendeOutrosMunicipios ? "Sim" : "Não";
            if (centroPOP.AtendeOutrosMunicipios)
            {
                lstAtendidos.Visible = centroPOP.AtendeOutrosMunicipios;
                lstMunicipiosAtendidos.DataSource = centroPOP.AbrangenciaMunicipios;
                lstMunicipiosAtendidos.DataBind();
            }
            lblServicoPSR.Text = centroPOP.PossuiServicoEspecializadoSituacaoRua.Value ? "Sim" : "Não";

            var servicoPSR = proxy.Service.GetConsultaServicosRecursosFinanceirosByCentroPOP(centroPOP.Id).Where(s => s.IdTipoServico == 144).Count();
            if (!centroPOP.PossuiServicoEspecializadoSituacaoRua.Value & servicoPSR == 0 && centroPOP.JustificativaServicoEspecializadoSituacaoRua.Length > 0)
                txtJustificativaPSR.Text = centroPOP.JustificativaServicoEspecializadoSituacaoRua;
            else
                trJustificativaPSR.Visible = false;

            if (centroPOP.AcoesSocioAssistenciais != null && centroPOP.AcoesSocioAssistenciais.Count > 0)
            {
                lstAcoesSocioAssistenciais.DataSource = centroPOP.AcoesSocioAssistenciais;
                lstAcoesSocioAssistenciais.DataBind();
            }

            if (centroPOP.IdAvaliacaoLocalExecucao.HasValue)
                lblAvaliacaoLocalExecucao.Text = new ProxyEstruturaAssistenciaSocial().Service.GetAvaliacoesLocal().Where(s => s.Id == centroPOP.IdAvaliacaoLocalExecucao.Value).FirstOrDefault().Descricao;

            if (centroPOP.IdDistritoSaoPaulo.HasValue)
                lblDistrito.Text = new ProxyEstruturaAssistenciaSocial().Service.GetDistritosSP().Where(s => s.Id == centroPOP.IdDistritoSaoPaulo.Value).FirstOrDefault().Nome;
        }




        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CCentroPOPDesativados.aspx?IdUnidade=" + Server.UrlEncode(Request.QueryString["idUnidade"]));
        }

    }
}