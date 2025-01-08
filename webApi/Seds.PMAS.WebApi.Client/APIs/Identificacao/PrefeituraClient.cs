using Seds.PMAS.Common;

namespace Seds.PMAS.WebApi.Client.APIs.Identificacao
{
    public abstract class PrefeituraClient : Client
    {
        public PrefeituraClient() : base(Extensions.GetTokenCurrentUser())
        {

        }

        //[Route("prefeitura")]
        //[HttpGet]
        //[ResponseType(typeof(GetPrefeituraResponse))]
        //public abstract T GetById([FromUri]GetPrefeituraRequest prefeituraRequest);

        //public abstract T InsertPrefeitura(InsertPrefeituraRequest prefeituraRequest);
    }
}
