using Seds.PMAS.WebSite.Classes;
using System.Web.Http;

namespace Seds.PMAS.WebSite
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new CustomAuthorizeAttribute());
            config.Filters.Add(new ExceptionHandler());
        }
    }
}
