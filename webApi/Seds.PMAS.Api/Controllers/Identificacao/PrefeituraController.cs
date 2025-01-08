using Seds.PMAS.Common;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Seds.PMAS.Api.Controllers
{
    [Authorize]
    public class PrefeituraController : ApiController
    {
        readonly IPrefeituraService _service;
        public PrefeituraController(IPrefeituraService service)
        {
            _service = service;
        }

        [Route("api/prefeitura")]
        [HttpGet]
        public HttpResponseMessage Get(PrefeituraEntity obj)
        {
            try
            {
                 _service.update(obj);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [Route("api/prefeitura")]
        [HttpPut]
        public HttpResponseMessage UpdatePrefeitura(PrefeituraEntity obj)
        {
            try
            {
                 //_service.Update(obj);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }


        //[HttpGet]
        //[Route("api/prefeitura")]
        //public HttpResponseMessage GetPrefeitura(int Id)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    var prefeitura = _service.GetById(Id);

        //    response = Request.CreateResponse(HttpStatusCode.OK, prefeitura);


        //    return response;
        //}

        //public HttpResponseMessage Update(PrefeituraEntity prefeitura)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    var prefeitura = _service.update(prefeitura);

        //    response = Res
        //}

    }
}
