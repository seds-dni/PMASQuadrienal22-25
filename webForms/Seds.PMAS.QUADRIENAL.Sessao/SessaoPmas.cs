using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Web.SessionState;
using System.Web;
using Seds.Entidades;
using System.Web.Configuration;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public static class SessaoPmas
    {                
        public static UsuarioPMASInfo UsuarioLogado
        {
            get
            {
                return (UsuarioPMASInfo)HttpContext.Current.Session["UsuarioPMAS"];
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
      
    }
}
