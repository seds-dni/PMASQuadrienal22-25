
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Seds.PMAS.Common
{
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                var exception = Extensions.GetExceptionMessage(actionExecutedContext.Exception);
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(exception),
                    ReasonPhrase = exception.Replace(System.Environment.NewLine, "")
                };

                actionExecutedContext.Response = msg;
            }
        }
    }
}
