using Seds.Entidades;

using Seds.WebApiClient;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using Seds.PMAS.Dominio.Entities;
using System;


namespace Seds.PMAS.Web
{
    public class Sessao
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

        public static List<DradsInfo> Drads
        {
            get
            {
                var lst = (List<DradsInfo>)HttpContext.Current.Cache["Drads"];
                if (lst == null)
                {
                    var client = new DivisaoAdministrativaClient();

                    lst = client.GetDrads().OrderBy(c => c.Nome).ToList();
                    HttpContext.Current.Cache["Drads"] = lst;

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

                    var client = new DivisaoAdministrativaClient();
                    lst = client.GetMunicipiosByUF(26).ToList();
                    HttpContext.Current.Cache["MunicipiosEstaduais"] = lst;

                }
                return lst;
            }
        }

        public static void VerificarSessao()
        {
            if (UsuarioLogado == null)
            {
                throw new Exception("Sessão expirada!");
            }
        }
    }
}