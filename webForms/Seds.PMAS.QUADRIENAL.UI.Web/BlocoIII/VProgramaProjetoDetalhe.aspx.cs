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
    public partial class VProgramaProjetoDetalhe : System.Web.UI.Page
    {
        protected List<TransferenciaRendaParceriaInfo> Parcerias
        {
            get { return Session["PARCERIAS"] as List<TransferenciaRendaParceriaInfo>; }
            set { Session["PARCERIAS"] = value; }
        }

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

                if (String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx");
                    return;
                }

                using (var proxy = new ProxyProgramas())
                {
                    load(proxy);
                }
            }
        }

        void load(ProxyProgramas proxy)
        {
            var obj = proxy.Service.GetTransferenciaRendaById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            if (obj == null)
                return;
            lblTituloPrograma.Text = lblPrograma.Text = obj.Nome.ToString();
            txtNome.Text = obj.Objetivo.ToString();
            divBeneficiariosBPCIdosoPessoaDeficiencia.Visible = true;
            txtBPCIdosoPessoaDeficienciaNumeroAtendidos.Text = obj.BPCNumeroBeneficiarios.ToString();
            txtBPCIdosoPessoaDeficienciaNumeroAtendidos.Enabled = false;
            if (obj.BPCRepasseAnual.HasValue)
                lblBPCIdosoPessoaDeficienciaValor.Text = obj.BPCRepasseAnual.Value.ToString("N2");
            if (obj.AderiuBPCNaEscola.HasValue)
            {
                trBPCEscola.Visible = true;
                rbAderiuBPCnaEscola.SelectedValue = Convert.ToSByte(obj.AderiuBPCNaEscola.Value).ToString();
                rbAderiuBPCnaEscola.Enabled = false;
                if (obj.AderiuBPCNaEscola.Value == true)
                {
                    trAdesaoBPCEscola.Visible = true;
                    txtDataImplantacao.Text = Convert.ToDateTime(obj.DataAdesaoBPCNaEscola).Date.ToString();
                    txtDataImplantacao.Enabled = false;
                    txtNumeroBeneficiariosBPC.Text = obj.NumeroBeneficiariosBPCNaEscola.ToString();
                    txtNumeroBeneficiariosBPC.Enabled = false;
                }


            }
            rblParcerias.SelectedValue = Convert.ToSByte(obj.PossuiParceriaFormal).ToString();
            rblParcerias.Enabled = false;
            tbParcerias.Visible = obj.PossuiParceriaFormal;
            if (obj.PossuiParceriaFormal)
            {
                Parcerias = obj.Parcerias;
                carregarParcerias();
            }

        }

        void carregarParcerias()
        {
            lstParcerias.DataSource = Parcerias;
            lstParcerias.DataBind();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CProgramasProjetos.aspx");
            //if (!String.IsNullOrEmpty(Request.QueryString["idCentro"]))
            //{
            //    var idCentro = Genericos.clsCrypto.Decrypt(Request.QueryString["idCentro"]);
            //    Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosCentroPOP.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idCentro)));
            //    return;
            //}

            //if (!String.IsNullOrEmpty(Request.QueryString["idProjeto"]))
            //{
            //    var idProjeto = Genericos.clsCrypto.Decrypt(Request.QueryString["idProjeto"]);
            //    Response.Redirect("~/BlocoIII/FProgramaProjetoCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idProjeto)));
            //    return;
            //}

            //if (!String.IsNullOrEmpty(Request.QueryString["idTransferenciaRenda"]))
            //{
            //    var idTransferenciaRenda = Genericos.clsCrypto.Decrypt(Request.QueryString["idTransferenciaRenda"]);
            //    Response.Redirect("~/BlocoIII/FTransferenciaRendaCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idTransferenciaRenda)));
            //    return;
            //}
        }
    }
}