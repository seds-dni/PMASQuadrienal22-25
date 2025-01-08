using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Seds.PMAS.Api.Controllers
{

    public class RecursoController : ApiController
    {
        private readonly IRecursoService _service;


        public RecursoController(IRecursoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/recurso")]
        //[CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)] //Install-Package Strathweb.CacheOutput.WebApi2
        public List<RecursoEntity> Get(UsuarioEntity usuario)
        {
            //JsonResult response = new JsonResult();

            //try
            //{
            //    var result = _service.GetByIdPerfil(usuario.IdPerfil);
            //    response = Request.CreateResponse(HttpStatusCode.OK, result);
            //}
            //catch (Exception ex)
            //{
            //    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            //}

            //var tsc = new TaskCompletionSource<HttpResponseMessage>();
            //tsc.SetResult(response);
            //return tsc.Task;

            return _service.GetByIdPerfil(usuario.IdPerfil).ToList();
        }
    }
}
