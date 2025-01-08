using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading;
using Seds.PMAS.Dominio.Entities;
using Microsoft.IdentityModel.Claims;
using Seds.PMAS.Aplicacao.Interface;
using Seds.PMAS.Aplicacao;
using Seds.PMAS.Dominio.Interfaces.Services;
using Seds.PMAS.Infra.Data.Repositories;
using System.Data;
using Seds.PMAS.Infra.Data.Context;
using System.Web.Mvc;


namespace Seds.PMAS.Web
{
    public static class Extensions
    {
        private static IUsuarioService _usuarioLogado;

        public static Int32 GetIdUsuarioLogado()
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            ClaimsIdentity identity = (ClaimsIdentity)principal.Identities[0];
            return Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);
        }

        public static string GetLoginUsuarioLogado()
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            ClaimsIdentity identity = (ClaimsIdentity)principal.Identities[0];
            return identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/login").FirstOrDefault().Value;
        }


        public static UsuarioEntity GetUsuarioLogado()
        {
            UsuarioEntity usuario = new UsuarioRepository().GetByIdUsuario(Extensions.GetIdUsuarioLogado());

            if (usuario == null)
                return null;

            if (usuario.IdDrads.HasValue)
            {
                // usuario.Drads = Sessao.Drads.FirstOrDefault(d => d.Id == usuario.IdDrads.Value);
            }
            var cadUnico = new CadastroUsuario.Usuario();
            DataSet ds = cadUnico.RetornaDadosID(usuario.IdUsuario.ToString());
            usuario.Nome = ds.Tables[0].Rows[0]["USU_NOME"].ToString();
            usuario.Login = ds.Tables[0].Rows[0]["USU_LOGIN"].ToString();
            usuario.RG = ds.Tables[0].Rows[0]["USU_RG"].ToString();
            usuario.OrgaoEmissor = ds.Tables[0].Rows[0]["USU_ORGEMISSOR"].ToString();
            usuario.UFCidade = usuario.UFRG = ds.Tables[0].Rows[0]["USU_UF"].ToString();
            usuario.Email = ds.Tables[0].Rows[0]["USU_EMAIL"].ToString();
            usuario.Telefone = ds.Tables[0].Rows[0]["USU_TELEFONE"].ToString();
            usuario.Cidade = ds.Tables[0].Rows[0]["USU_CIDADE"].ToString();
            usuario.CEP = ds.Tables[0].Rows[0]["USU_CEP"].ToString();
            usuario.Endereco = ds.Tables[0].Rows[0]["USU_ENDERECO"].ToString();
            usuario.Complemento = ds.Tables[0].Rows[0]["USU_COMPLEMENTO"].ToString();
            usuario.Numero = ds.Tables[0].Rows[0]["USU_NROENDERECO"].ToString();
            usuario.Bairro = ds.Tables[0].Rows[0]["USU_BAIRRO"].ToString();
            usuario.TrocarSenha = Convert.ToBoolean(ds.Tables[0].Rows[0]["USU_FL_SENHA_ALTERADA"]);


            var perfis = new List<string>();
            //using(var proxy = new ProxyPerfil())
            //{
            //    perfis = proxy.Service.GetPerfisByUsuario(usuario.IdUsuario).ToList();
            //}

            //var perfil = perfis.FirstOrDefault(c => c.Contains("@") && c.Contains("PMAS 2017"));
            //if (perfil == null)
            //    return null;

            //usuario.Perfil = perfil.Split('@')[1];
            //switch (usuario.Perfil)
            //{
            //    case "Orgão Gestor": usuario.EnumPerfil = EPerfil.OrgaoGestor; break;
            //    case "DRADS Administrador": usuario.EnumPerfil = EPerfil.DRADSAdministrador; break;
            //    case "SEDS": usuario.EnumPerfil = EPerfil.SEDS; break;
            //    case "CAS": usuario.EnumPerfil = EPerfil.CAS; break;
            //    case "Administrador": usuario.EnumPerfil = EPerfil.Administrador; break;
            //    case "Convidados": usuario.EnumPerfil = EPerfil.Convidados; break;
            //    case "DRADS": usuario.EnumPerfil = EPerfil.DRADS; break;
            //    case "CMAS": usuario.EnumPerfil = EPerfil.CMAS; break;
            //}
            //if (usuario != null)
            //{
            //    usuario.Funcionalidades = new RecursoRepository().GetByIdPerfil(Convert.ToInt32(usuario.EnumPerfil)).ToList();
            //}
            return usuario;

        }

        public static MvcHtmlString RenderMenu(this HtmlHelper html, List<RecursoEntity> funcionalidades)
        {
            string menu = string.Empty;
            foreach (var item in funcionalidades.Where(x => x.IdPai == null).ToList())
            {
                CarregarMenu(item, ref menu, funcionalidades);
            }

            if (menu.Length > 0)
            {
                menu = string.Format("<ul  class='nav navbar-nav'> {0} </ul>", menu);
            }

            return new MvcHtmlString(menu);
        }

        public static void CarregarMenu(RecursoEntity func, ref string menu, List<RecursoEntity> funcionalidades)
        {

            if (funcionalidades.Any(x => x.IdPai == func.Id))
            {
                string filhos = string.Empty;

                foreach (var sub in funcionalidades.Where(x => x.IdPai == func.Id).ToList())
                {
                    CarregarMenu(sub, ref filhos, funcionalidades);
                }

                menu += string.Format("<li class=\"dropdown\"> <a href=\"{0}\" class='dropdown-toggle' data-toggle='dropdown'>{1} </a> <ul class=\"dropdown-menu\"> {2} </ul></li>", (String.IsNullOrEmpty(func.Pagina) ? "#" : func.Pagina), func.Nome, filhos);
            }
            else
            {
                menu += string.Format("<li><a href=\"{0}\" data-toggle='dropdown'>{1}</a> </li>", (String.IsNullOrEmpty(func.Pagina) ? "#" : func.Pagina), func.Nome);
            }
        }

        //void Session_End(object sender, EventArgs e)
        //{
        //    // Code that runs when a session ends. 
        //    // Note: The Session_End event is raised only when the sessionstate mode
        //    // is set to InProc in the Web.config file. If session mode is set to StateServer 
        //    // or SQLServer, the event is not raised.

        //}

        /// <summary>
        /// Verifica usuários do tipo SEDS para obrigação do filtro
        /// </summary>
        /// <returns>ActionResult</returns>
        //public static ActionResult CheckSedsUser()
        //{
        //    RedirectResult Redirect = new RedirectResult("~/Home/SelecaoDrads");
        //    //Verifica perfis
        //    switch (Sessao.UsuarioLogado.EnumPerfil)
        //    {
        //        case EPerfil.AdministradorSEDS:
        //        case EPerfil.UsuarioSEDS:
        //        case EPerfil.LeitorSEDS:
        //        {   //Verifica se existe filtro no usuário
        //            if (Sessao.UsuarioLogado.Drads == null && Sessao.UsuarioLogado.Municipio == null &&
        //                Sessao.UsuarioLogado.Instituicao == null)
        //                return Redirect;
        //            else
        //                break;
        //        }
        //    }
        //    //
        //    retu
        //}
    }
}