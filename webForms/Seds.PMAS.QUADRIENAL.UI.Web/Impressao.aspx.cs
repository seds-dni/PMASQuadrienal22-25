using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class Impressao : System.Web.UI.Page
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

                if ((ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.Aprovado || (ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.Rejeitado)
                {
                    lblTitulo.Text = "Impressão do Plano Municipal de Assistência Social";
                    trTextoExplicativo.Visible = false;
                }
                else {
                    using(var proxy = new ProxyPrefeitura()){
                        var orgaoGestor = new ProxyPrefeitura().Service.GetOrgaoGestorByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                        if(orgaoGestor == null){
                        lblMensagemOrgaoGestor.Text = "A visualização da Impressão funcionará somente se a Identificação do Orgão Gestor na Assistência Social estiver preenchida.";
                }
                    lblTitulo.Text = "Impressão Provisória do Plano Municipal de Assistência Social";
                    trTextoExplicativo.Visible = true;
                    }
                }
         
               // trPlano.Visible = (ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.Aprovado || (ESituacao)SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == ESituacao.Rejeitado;
                //trNao.Visible = !trPlano.Visible;
            }
        }
    }
}