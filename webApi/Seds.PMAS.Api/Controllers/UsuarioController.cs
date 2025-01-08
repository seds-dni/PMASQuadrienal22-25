using Seds.PMAS.Aplicacao;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using Seds.PMAS.Dominio.Interfaces.Services;
using Seds.PMAS.Dominio.Services;
using Seds.PMAS.Infra.Data.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace Seds.PMAS.Api.Controllers
{
    //[RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private IUsuarioService _service;
    
        public UsuarioController(IUsuarioService service)
        {
            this._service = service;
        }


        public static Int32? GetIdUsuarioLogado()
        {

            ClaimsPrincipal principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            ClaimsIdentity identity = (ClaimsIdentity)principal.Identities.FirstOrDefault();
            return Convert.ToInt32(identity.Claims.Where(c => c.Type == "id").FirstOrDefault().Value);

        }

        public static string GetLoginUsuarioLogado()
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            ClaimsIdentity identity = (ClaimsIdentity)principal.Identities.FirstOrDefault();
            return identity.Claims.Where(c => c.Type == "http://seds.sp.gov.br/identity/claims/login").FirstOrDefault().Value;
        }

        //[Authorize]
        [HttpGet]
        [Route("api/usuario")]
        public HttpResponseMessage GetUsuarioLogado()
        {

            
            HttpResponseMessage response = new HttpResponseMessage();
            var IdUsuarioLogado = GetIdUsuarioLogado();

            if (IdUsuarioLogado != null)
            {

                var usuario = _service.GetByIdUsuario(IdUsuarioLogado.Value);

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
                usuario.Perfil = (EPerfil)usuario.IdPerfil;
                usuario.Funcionalidades = new RecursoRepository().GetByIdPerfil(usuario.IdPerfil);

                response = Request.CreateResponse(HttpStatusCode.OK, usuario);

            }
            return response;

        }
    }
}
