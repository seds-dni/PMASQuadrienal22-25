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
    public partial class VLocalExecucaoPublico : System.Web.UI.Page
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

            var local = proxy.Service.GetLocalExecucaoPublicoById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));

            var unidade = proxy.Service.GetUnidadePublicaById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"])));
            if (unidade == null)
                return;

            string cnpj = unidade.CNPJ;
            cnpj = "00000000000000" + cnpj;
            cnpj = cnpj.Substring(cnpj.Length - 14, 14);
            cnpj = cnpj.Insert(2, ".");
            cnpj = cnpj.Insert(6, ".");
            cnpj = cnpj.Insert(10, "/");
            cnpj = cnpj.Insert(15, "-");

            lblCNPJ.Text = cnpj;
            lblNome.Text = unidade.RazaoSocial;

            var servicos = proxy.Service.GetConsultaServicosRecursosFinanceirosPublicoByLocalExecucao(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]))).Where(p => p.IdUsuarioTipoServico == 39 || p.IdUsuarioTipoServico == 37 || p.IdUsuarioTipoServico == 41).ToList();

            if (local == null)
                return;

            lblNomeLocalExecucao.Text = local.Nome;
            txtTecnicoResponsavel.Text = local.PossuiTecnicoResponsavel ? local.TecnicoResponsavel : "Não possui técnico responsável";
            lblCep.Text = local.CEP.Substring(local.CEP.Length - 8, 8);
            lblCep.Text = local.CEP.Insert(5, "-");
            lblLogradouro.Text = local.Logradouro;
            lblNumero.Text = local.Numero;
            lblComplemento.Text = local.Complemento;
            lblBairro.Text = local.Bairro;
            lblCidade.Text = local.Cidade;

            if (!String.IsNullOrEmpty(local.Telefone))
            {
                string sDDD, sTelefone;
                string sTelefoneCompleto = local.Telefone;
                sTelefoneCompleto = "0000000000" + sTelefoneCompleto;
                sTelefoneCompleto = sTelefoneCompleto.Substring(sTelefoneCompleto.Length - 10, 10);
                sDDD = sTelefoneCompleto.Substring(0, 2);
                sTelefone = sTelefoneCompleto.Substring(2, 8);
                sTelefone = sTelefone.Insert(4, "-");
                lblTelefone.Text = "(" + sDDD + ") " + sTelefone;
            }

            if (!string.IsNullOrEmpty(local.Celular))
            {
                string sCelularCompleto = local.Celular;
                string sDDDCelular, sCelular = "";

                sCelularCompleto = "00000000000" + sCelularCompleto;
                sCelularCompleto = sCelularCompleto.Substring(sCelularCompleto.Length - 11, 11);

                sDDDCelular = sCelularCompleto.Substring(0, 2);
                sCelular = sCelularCompleto.Substring(2, 9);
                sCelular = sCelular.Insert(5, "-");

                lblCelular.Text = "(" + sDDDCelular + ") " + sCelular;
            }

            if (local.IdDistritoSaoPaulo.HasValue)
                lblDistrito.Text = new ProxyEstruturaAssistenciaSocial().Service.GetDistritosSP().Where(s => s.Id == local.IdDistritoSaoPaulo.Value).FirstOrDefault().Nome;

            switch (local.IdTipoImovel)
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

            if (local.IdAvaliacaoLocalExecucao.HasValue)
                lblAvaliacaoLocalExecucao.Text = new ProxyEstruturaAssistenciaSocial().Service.GetAvaliacoesLocal().Where(s => s.Id == local.IdAvaliacaoLocalExecucao.Value).FirstOrDefault().Descricao;


            lblEmailInstitucional.Text = local.Email;

            if (local.Desativado)
            {
                lblDataExclusaoRegistro.Text = local.DataRegistroLog.Value.ToShortDateString();
                if (local.IdMotivoDesativacao.HasValue)
                {
                    lblMotivoExclusao.Text = new ProxyRedeProtecaoSocial().Service.GetMotivoDesativacaoLocalById(local.IdMotivoDesativacao.Value).Descricao;

                    if (local.IdMotivoDesativacao.Value != 27)
                    {
                        trDetalhamento.Visible = trDataEncerramento.Visible = true;
                        lblDataEncerramentoServico.Text = local.DataDesativacao.Value.ToShortDateString();
                        lblDetalhamento.Text = local.Detalhamento;

                        if (local.IdMotivoDesativacao.Value == 28)
                        {
                            lblDescEncerramentoServico.Text = "Data do encerramento das atividades deste Local de execução:";
                            lblDescDetalhamento.Text = "Detalhamento sobre o motivo do encerramento deste do Local de execução:";
                            trMotivoEncerramento.Visible = true;
                            lblMotivoEncerramento.Text = new ProxyRedeProtecaoSocial().Service.GetMotivoDesativacaoLocalById(local.IdMotivoEncerramento.Value).Descricao;
                        }


                    }
                    else
                    {
                        trMotivoEncerramento.Visible = false;
                    }
                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["IdUnidade"]);
            Response.Redirect("~/BlocoIII/CLocaisPublicoDesativados.aspx?IdUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
        }

    }
}