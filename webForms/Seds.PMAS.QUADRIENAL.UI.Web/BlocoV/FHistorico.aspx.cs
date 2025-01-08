using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Web.UI.HtmlControls;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoV
{
    public partial class FHistorico : System.Web.UI.Page
    {

        #region properties
        private static List<int> Exercicios = new List<int>() { 2017, 2018, 2019, 2020 };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.hdfAno.Value = string.IsNullOrEmpty(this.hdfAno.Value) ? "2020" : this.hdfAno.Value;

            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);
                int idHistorico = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["Id"]));
                carregaHistorico(idHistorico);
            }
        }

        void carregaHistorico(int id)
        {
            var proxy = new ProxyPrefeitura();

            List<HistoricoPrestacaoDeContasInfo> historico = proxy.Service.GetHistoricoPrestacaoDeContasID(id);

            var h = historico.FirstOrDefault();


            lblData.Text = h.Data.ToString("dd/MM/yyyy HH:mm");

            lblSituacao.Text = h.SituacaoStatus;

            lblNome.Text = h.NomeResponsavel;

            lblDescricaoMotivo.Text = h.DescricaoMotivo;
        }

    }
}