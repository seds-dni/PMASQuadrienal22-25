using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Seds.PMAS.WebSite
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(actionExecutedContext.Exception.Message)
            };
            //send logs to big thing where logs are kept

            base.OnException(actionExecutedContext);
        }
    }
}