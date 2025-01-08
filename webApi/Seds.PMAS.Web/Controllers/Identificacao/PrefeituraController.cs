using AutoMapper;
using Seds.PMAS.Aplicacao.Interface;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Web.ViewModels;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Seds.PMAS.Web.Controllers
{
    public class PrefeituraController : ApiController
    {
        // GET: /Prefeitura/
        private readonly IPrefeituraAppService _prefeituraApp;
        //private readonly IPrefeitoAppService _prefeitoApp;

        public PrefeituraController() 
        {
        }

        public PrefeituraController(IPrefeituraAppService prefeituraApp)
        {
            //   _prefeitoApp = prefeitoApp;
            _prefeituraApp = prefeituraApp;
        }


        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            Sessao.VerificarSessao();
            var prefeituraViewModel = Mapper.Map<PrefeituraEntity, PrefeituraViewModel>(_prefeituraApp.GetById(Sessao.UsuarioLogado.Prefeitura.Id));
            if (prefeituraViewModel == null)
                prefeituraViewModel = new PrefeituraViewModel();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, prefeituraViewModel);
            return response;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
