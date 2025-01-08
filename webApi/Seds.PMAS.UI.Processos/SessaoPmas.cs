using Seds.Entidades;
using Seds.PMAS.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web;

namespace Seds.PMAS.UI.Processos
{
    public static class SessaoPmas
    {
        public static UsuarioEntity UsuarioLogado
        {
            get
            {
                return (UsuarioEntity)HttpContext.Current.Session["UsuarioPMAS"];
            }
            set
            {
                HttpContext.Current.Session["UsuarioPMAS"] = value;
            }
        }

        public static String VersaoPMAS { get { return WebConfigurationManager.AppSettings["VersaoPMAS"]; } }

        public static String Versao { get { return WebConfigurationManager.AppSettings["Versao"]; } }

        public static void VerificarSessao(System.Web.UI.Page page)
        {
            if (UsuarioLogado == null)
            {
                page.Response.Redirect("~/LogOff.aspx");
                return;
            }
            if (UsuarioLogado.Ativo > 0)
                page.Response.Redirect("~/UsuarioInativo.aspx");

        }
        public static List<DradsInfo> Drads
        {
            get
            {
                var lst = (List<DradsInfo>)HttpContext.Current.Cache["Drads"];
                if (lst == null)
                {
                    using (var proxy = new ProxyDivisaoAdministrativa())
                    {
                        lst = proxy.Service.GetDrads().OrderBy(c => c.Nome).ToList();
                        HttpContext.Current.Cache["Drads"] = lst;
                    }
                }
                return lst;
            }
        }

        public static List<MunicipioInfo> MunicipiosEstaduais
        {
            get
            {
                var lst = (List<MunicipioInfo>)HttpContext.Current.Cache["MunicipiosEstaduais"];
                if (lst == null)
                {
                    using (var proxy = new ProxyDivisaoAdministrativa())
                    {
                        lst = proxy.Service.GetMunicipiosByUF(26).ToList();
                        HttpContext.Current.Cache["MunicipiosEstaduais"] = lst;
                    }
                }
                return lst;
            }
        }
    }
}
