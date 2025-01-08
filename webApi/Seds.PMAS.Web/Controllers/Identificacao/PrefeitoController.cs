using AutoMapper;
using Seds.PMAS.Aplicacao.Interface;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Web.ViewModels;
using Seds.PMAS.Web;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Seds.PMAS.Web.Controllers
{
    public class PrefeitoController : ApiController
    {
        private readonly IPrefeitoAppService _prefeitoApp;
        public PrefeitoController() { }

        public PrefeitoController(IPrefeitoAppService prefeitoApp)
        {
            _prefeitoApp = prefeitoApp;
        }

        // GET: api/Prefeito
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var prefeitoViewModel = Mapper.Map<PrefeitoEntity, PrefeitoViewModel>(_prefeitoApp.GetByIdPrefeitura(Sessao.UsuarioLogado.Prefeitura.Id));
            if (prefeitoViewModel == null)
                prefeitoViewModel = new PrefeitoViewModel();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, prefeitoViewModel);
            return response;
        }
        //// GET: api/Prefeito/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //POST: api/Prefeito
        [HttpPost]
        public HttpResponseMessage Post(PrefeitoViewModel prefeito)
        {
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                var prefeitoDomain = Mapper.Map<PrefeitoViewModel, PrefeitoEntity>(prefeito);
                _prefeitoApp.Add(prefeitoDomain);
                response = Request.CreateResponse(HttpStatusCode.OK, prefeito);
            }
            //else
            {
                // var errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray();
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            return response;
        }

        // PUT: api/Prefeito/5
        [HttpPut]
        public HttpResponseMessage Put(int id, PrefeitoViewModel prefeito)
        {
            var prefeitoDomain = Mapper.Map<PrefeitoViewModel, PrefeitoEntity>(prefeito);
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                _prefeitoApp.Update(prefeitoDomain);
                response = Request.CreateResponse(HttpStatusCode.OK, prefeito);
            }
            else
            {
                //var errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray();
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            return response;
            //return prefeito;
        }

        // DELETE: api/Prefeito/5
        public void Delete(int id)
        {
        }
    }
}
