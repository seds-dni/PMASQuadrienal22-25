using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoVI
{
    public partial class FDiretrizesServicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var idAnalise = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            var idPrefeitura = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["idPrefeitura"]));
            var exercicio = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["Exercicio"]));


            load(idAnalise, idPrefeitura,exercicio);

        }

        public void load(int idAnalise,int idPrefeitura,int exercicio) 
        {
            using (var proxy = new ProxyProgramas())
            {
                var lst = proxy.Service.GetConsultaServicosDiretrizes(idPrefeitura, idAnalise,exercicio).OrderBy(t => t.IdTipoProtecao).GroupBy(s => s.ProtecaoSocial).Select(s => new { Key = s.Key, Items = s.OrderBy(i => i.IdTipoServico) }).ToList();
                lstRecursos.DataSource = lst;
                lstRecursos.DataBind();

            }
        }

        protected string MontarBotao(ConsultarServicosDiretrizesInfo item)
        {
            var idProjeto = Server.UrlEncode(Genericos.clsCrypto.Encrypt(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            var page = String.Empty;
            switch (item.TipoUnidade)
            {
                case "Rede Direta": page = "../BlocoIII/VServicoRecursoFinanceiroPublico.aspx"; break;
                case "Rede Indireta": page = "../BlocoIII/VServicoRecursoFinanceiroPrivado.aspx"; break;
                case "CRAS": page = "../BlocoIII/VServicoRecursoFinanceiroCRAS.aspx"; break;
                case "CREAS": page = "../BlocoIII/VServicoRecursoFinanceiroCREAS.aspx"; break;
                case "Centro Pop": page = "../BlocoIII/VServicoRecursoFinanceiroCentroPOP.aspx"; break;
            }
            return "<a href='" + page + "?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(item.IdServicoRecursosFinanceiros.ToString())) + "&idProjeto=" + idProjeto + "'><img src='../Styles/Icones/find.png' alt='Visualizar' border='0' /></a>";
        }

    }
}